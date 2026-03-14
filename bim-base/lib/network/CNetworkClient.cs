using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;

public class CNetworkClient
{
    public class SendInfo
    {
        public bool isStringPacket;
        public string text;
        public byte[] data = null;
    }

    public event EventHandler recvData;
    public event EventHandler sendData;
    public event EventHandler close;

    string m_ip = "";
    int m_port = 0;

    bool m_stop = false;
    Socket m_socket = null;

    Socket m_existSocket = null;

    CircularBuffer m_recvBuffer = new CircularBuffer(1024 * 1024 * 10);
    Queue<SendInfo> m_sendQueue = new Queue<SendInfo>();

    ManualResetEvent m_manualResetEvent = new ManualResetEvent(false);

    Thread m_thread = null;

    byte STX = 0x02;
    byte ETX = 0x03;

    uint stxEtxLength = 0;

    public bool m_useRecvStxEtx = false; // 기존 코드
    public bool m_useStxEtx = false; // 신규 코드

    object m_lockObject = new object();

    bool m_simulation = false;

    public CNetworkClient(string ip, int port)
    {
        m_ip = ip;
        m_port = port;

        m_existSocket = null;

        if (File.Exists(pathUtil.savePath() + "\\simulation"))
            m_simulation = true;

        m_thread = new Thread(run);
    }

    public CNetworkClient(Socket socket, string ip, int port)
    {
        m_ip = ip;
        m_port = port;

        m_existSocket = socket;
        m_socket = socket;
        m_thread = new Thread(run);
    }

    public void start()
    {
        if (m_thread != null)
            m_thread.Start();
    }

    public void reStart()
    {
        disconnected();
        m_thread = null;
        m_thread = new Thread(run);
        m_thread.Start();
    }

    public bool isStop()
    {
        return m_stop;
    }

    public void stop()
    {
        Debug.debug("CNetworkClient::stop");
        m_stop = true;

        if (m_socket != null)
        {
            try
            {
                m_socket.Close();
            }
            catch (Exception /*ex*/)
            {
            }
        }

        m_thread.Join(100);
    }

    public void run()
    {
        Debug.warning("CNetworkClient::run START IP:" + m_ip + " port:" + m_port.ToString());

        bool heartBeatCheck = false;
        int heartBeatTick = 0;

        while (true)
        {
            //Debug.debug("NetworkClient::run RUN #1");

            if (m_stop)
            {
                Debug.warning("CNetworkClient::run thread is stop. IP:" + m_ip + " port:" + m_port.ToString());
                break;
            }

            if (m_socket == null)
            {
                if (m_existSocket == null)
                {
                    m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    m_socket.ReceiveTimeout = 100;
                }
                
                //m_socket.NoDelay = true;

                try
                {
                    if (m_socket == null)
                    {
                        stop();
                        return;
                    }

                    m_socket.Connect(m_ip, m_port);
                    Thread.Sleep(100);
                    //SocketExtensions.Connect(m_socket, m_ip, m_port, new TimeSpan(100));
                }
                catch (Exception /*e*/)
                {
                    //Debug.debug("CNetworkClient::run connected error:" + e.Message);

                    try
                    {
                        disconnected();
                    }
                    catch (Exception /*ex*/)
                    {
                    }

                    if (m_existSocket != null)
                       m_socket = null;

                    Thread.Sleep(500);
                }

                continue;
            }

            if (connected())
            {
                recvProcess();
                sendProcess();

                if (m_recvBuffer.size() == 0 && m_sendQueue.Count == 0)
                {
                    if (heartBeatCheck == false)
                    {
                        heartBeatCheck = true;
                        heartBeatTick = Environment.TickCount;
                    }
                    else
                    {
                        if (Environment.TickCount - heartBeatTick > 5000)
                        {
                            //sendAlivePacket();
                            heartBeatCheck = false;
                            heartBeatTick = 0;
                        }
                    }
                }
                else
                {
                    heartBeatCheck = false;
                    heartBeatTick = 0;
                }
            }
            else
            {
                //Debug.debug("CNetworkClient::run disconncted");

                if (m_socket != null)
                {
                    try
                    {
                        m_socket.Disconnect(false);
                        m_socket.Shutdown(SocketShutdown.Both);
                        m_socket.Close();
                    }
                    catch (Exception /*ex*/)
                    {
                    }
                    m_socket = null;   
                }

                Thread.Sleep(1000);

            }
            //Debug.debug("NetworkClient::run RUN #2");

            Thread.Sleep(10);
        }

        disconnected();

        Debug.warning("CNetworkClient::run END");
    }

    public bool connected()
    {
        if (m_simulation)
            return true;

        if (m_socket == null)
            return false;

#if true
        try
        {
            return m_socket.Connected;
        }
        catch (Exception /*ex*/)
        {
            return false;
        }
#else
        try
        {
            if (m_socket.Connected == false)
                return false;

            bool ret = !(m_socket.Poll(1, SelectMode.SelectRead) && m_socket.Available == 0);

            if (ret == false)
            {
                int a = 0;
            }

            return ret;
        }
        catch (SocketException e) {

            if (e.NativeErrorCode.Equals(10035)) // 10035 == WSAEWOULDBLOCK
            {
                Debug.warning("CNetworkClient::connected ip:" + m_ip + " port:" + m_port + " WSAEWOULDBLOCK");
                return true;
            }

            return false; 
        }

#endif
    }

    public bool isConnected()
    {
        return connected();
    }

    public void disconnected()
    {
        if (m_socket != null)
            try { m_socket.Disconnect(false); } catch (Exception /*ex*/) { }
        if (m_socket != null)
            try { m_socket.Shutdown(SocketShutdown.Both); } catch (Exception /*ex*/) { }
        if (m_socket != null)
            try { m_socket.Close(); } catch (Exception /*ex*/) { }

        m_socket = null;

        if (close != null)
            close(this, null);
    }


    private void connectCallback(IAsyncResult result)
    {
        Debug.debug("NetworkClient::connectCallback");

        Socket socket = (Socket)result.AsyncState;

        try
        {
            socket.EndConnect(result);
        }
        catch (Exception /*ex*/)
        {
        }
    }

    private bool recvProcess()
    {
        if (m_socket == null)
            return false;

        try
        {
            int len = m_socket.Available;

            if (len > 0)
            {
                byte[] data = new byte[len];
                int n = m_socket.Receive(data);

                if (m_recvBuffer.size() + len > m_recvBuffer.capacity() - 1)
                {
                    Debug.warning("NetworkServerThread::recvProcess m_recvBuffer is overflow");
                    return false;
                }

                m_recvBuffer.write(data, (uint)len);
            }
        }
        catch (Exception ex)
        {
            Debug.warning("CNetworkClient::recvProcess Message:" + ex.Message + " Trace:" + ex.StackTrace);
        }

        return processIncomingData();
    }

    private bool send(byte[] data, bool isStringPacket = false, string text = "")
    {
        Monitor.Enter(m_lockObject);

        //Debug.debug("NetworkClient send START");

#if false
        string debugText = "";

        for (int i = 0; i < data.Length; i++)
        {
            debugText += data[i].ToString("02X") + " ";
        }

        Debug.debug("NetworkClient::send length:" + data.Length + " data:" + debugText);
#endif

        try
        {
            if (data == null)
            {
                Debug.warning("NetworkClient::send data is null!");
                return false;
            }

            //Debug.debug("NetworkClient::send length:" + data.Length + " text:" + text);

            SendInfo info = new SendInfo();

            byte[] sendData = null;

            if (m_useStxEtx == true)
            {
                sendData = new byte[data.Length + 2];

                sendData[0] = STX;
                sendData[sendData.Length - 1] = ETX;

                for (int i = 0; i < data.Length; i++)
                {
                    sendData[i + 1] = data[i];
                }
            }
            else
            {
                sendData = data;
            }

            info.isStringPacket = isStringPacket;
            info.text = text;
            info.data = sendData;

            m_sendQueue.Enqueue(info);

            return true;
        }
        finally
        {
            //Debug.debug("NetworkClient send END");
            Monitor.Exit(m_lockObject);
        }
    }

    public void clearSendQueue()
    {
        Monitor.Enter(m_lockObject);

        m_sendQueue.Clear();

        Monitor.Exit(m_lockObject);
    }

    private void sendStringPacket(string packet)
    {
#if false
        Debug.debug("NetworkClient::sendStringPacket PC:" + m_lmsNo + 
            " packetNo:" + packetNo + 
            " text:" + packet);
#endif

        byte[] data = Encoding.ASCII.GetBytes(packet);
        send(data, true);
    }

    public void sendString(string packet)
    {
        //Debug.debug("CNetworkCilent::sendString packet:" + packet);
        sendStringPacket(packet);
    }

    public void sendStringUtf8WithStxEtx(string packet)
    {
        sendStringUtf8PacketWithStxEtx(packet);
    }
    
    private void sendStringUtf8PacketWithStxEtx(string packet)
    {
        byte[] data = Encoding.UTF8.GetBytes(packet);

        if (m_useStxEtx) // 모든 통신이 STX,ETX 사용 시
        {
            send(data, true);
            return;
        }

        byte[] sendData = new byte[data.Length + 2];

        sendData[0] = STX;

        for (int i = 0; i < data.Length; i++)
        {
            int index = i + 1;
            sendData[index] = data[i];
        }

        sendData[(sendData.Length - 1)] = ETX;

        send(sendData, true);
    }

    public void sendStringUtf8(string packet)
    {
        byte[] data = Encoding.UTF8.GetBytes(packet);
        send(data, true);
    }

    public void sendStringWithStxEtx(string packet)
    {
        sendStringPacketWithStxEtx(packet);
    }

    private void sendStringPacketWithStxEtx(string packet)
    {
        byte[] data = Encoding.ASCII.GetBytes(packet);

        if (m_useStxEtx) // 모든 통신이 STX,ETX 사용 시
        {
            send(data, true);
            return;
        }

        byte[] sendData = new byte[data.Length + 2];

        sendData[0] = STX;

        for (int i = 0; i < data.Length; i++)
        {
            int index = i + 1;
            sendData[index] = data[i];
        }

        sendData[(sendData.Length - 1)] = ETX;

        send(sendData, true);
    }

    public void send(byte[] data)
    {
        send(data, false, "");
    }

    private void sendArrayPacket(int packetNo, byte[] packet)
    {
        int length = packet.Length + 8;
        byte[] data = new Byte[length];

        int offset = 0;
        Util.setInt(ref data, offset, length - 4); offset += 4;
        Util.setInt(ref data, offset, packetNo); offset += 4;
        Util.memcpy(ref data, offset, packet, 0, packet.Length); offset += packet.Length;

        send(data);
    }

    private bool sendProcess()
    {
        Monitor.Enter(m_lockObject);

        try
        {
            if (m_sendQueue.Count > 0)
            {
                SendInfo info = m_sendQueue.Peek();

                if (info.data == null)
                {
                    //Debug.warning("NetworkClient::sendProcess data is null");
                    m_sendQueue.Dequeue();
                    return false;
                }

                try
                {
                    /*
                    if (info.isStringPacket)
                        Debug.debug("NetworkClient::sendProcess sendStringPacket text:" + info.text);
                    */
                     
                    int n = m_socket.Send(info.data);
                    //Debug.debug("NetworkCClient::sendProcess n:" + n);

                    if (n != info.data.Length)
                    {
                        Debug.warning("NetworkClient::sendProcess send failed");
                        return false;
                    }

                    if (sendData != null)
                    {
                        sendData(info.data, null);
                    }

                    m_sendQueue.Dequeue();
                }
                catch (Exception e)
                {
                    Debug.debug("NetworkClient::sendProcess error:" + e.Message);
                    Debug.debug("NetworkClient::sendProcess stackTrace:" + e.StackTrace);
                }
            }

            return true;
        }
        finally
        {
            Monitor.Exit(m_lockObject);
        }
    }

    private bool processIncomingData()
    {
        while (true)
        {
            try
            {
                int len = (int)m_recvBuffer.size();

                if (len == 0) return true;

                if (m_useRecvStxEtx == false && m_useStxEtx == false) 
                {
                    byte[] data = new byte[m_recvBuffer.size()];
                    m_recvBuffer.read(ref data, (uint)m_recvBuffer.size());

                    try
                    {
                        recvData(data, null);
                    }
                    catch (Exception ex)
                    {
                        Debug.critical(ex,
                            "CNetworkClient::processIncomingData recvData ip:" + m_ip + " port:" + m_port);
                    }
                }
                else
                {
                    byte[] tempData = new byte[m_recvBuffer.size()];
                    m_recvBuffer.peek(ref tempData, (uint)m_recvBuffer.size());

                    int stxIndex = -1;
                    int etxIndex = -1;

                    for (int i = 0; i < tempData.Length; i++)
                    {
                        if (tempData[i] == STX && stxIndex == -1)
                        {
                            stxIndex = i;
                            continue;
                        }

                        if (tempData[i] == ETX && etxIndex == -1)
                        {
                            if (stxEtxLength > 0)
                            {
                                if ((i + stxIndex) > (stxEtxLength - 1))
                                {
                                    etxIndex = i;
                                    break;
                                }
                            }
                            else
                            {
                                etxIndex = i;
                                break;
                            }
                        }
                    }

                    if (stxIndex > 0)
                    {
                        m_recvBuffer.read(ref tempData, (uint)stxIndex); // STX 전 데이터 버림

                        //string errorText = ASCIIEncoding.ASCII.GetString(tempData);
                        //Debug.debug("CNetworkClientS::recvErrorData recv text:" + errorText);
                    }

                    if (etxIndex < 1) // 데이터 추가 수신 대기
                        return false;

                    byte[] data = new byte[etxIndex + 1];

                    m_recvBuffer.read(ref data, (uint)data.Length); // 데이터 취득

                    try
                    {
                        recvData(data, null);
                    }
                    catch (Exception ex)
                    {
                        Debug.critical(ex,
                            "CNetworkClient::processIncomingData recvData ip:" + m_ip + " port:" + m_port);
                    }

                }

            }
            catch (Exception ex)
            {
                Debug.critical(ex,
                        "CNetworkClient::processIncomingData ip:" + m_ip + " port:" + m_port);
            }
        }
    }

    public void setUseRecvStxEtx(bool value)
    {
        m_useRecvStxEtx = value;
    }

    public void useStxEtx(bool value)
    {
        m_useStxEtx = value;
    }
    public void setStx(byte stx)
    {
        STX = stx;
    }
    public void setEtx(byte etx)
    {
        ETX = etx;
    }
    public void setStxEtxLength(uint length)
    {
        stxEtxLength = length;
    }

    public void bufferClear()
    {
        m_recvBuffer.clear();
    }
}
