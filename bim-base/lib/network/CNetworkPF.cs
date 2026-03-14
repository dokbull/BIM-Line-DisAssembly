using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

public class CNetworkPF
{
    public enum SEND_MID
    {
        COMMUNICATION_START,
        TIGHTENING_LAST_DATA,
        TIGHTENING_LAST_DATA_RESULT,     //결과 데이터를 받은 후 받았다는 응답에 쓰임.
        SELECT_P_SET,
        KEEP_ALIVE,
    }

    public enum PSet
    {
        LEFT = 1,
        RIGHT = 2,
        LEFT_CENTERING = 17,
        RIGHT_CENTERING = 16,
        LEFT_REVERSE = 5,
        RIGHT_REVERSE = 6,

        LEFT_MULTI = 7,
        RIGHT_MULTI = 8
    }

    public class Result
    {
        //  체결 데이터..
        public int p_id;        // 90-92
        public bool tighteningStatus;  // 107
        public double torque;   // 140-145
        public double angle;    // 169-173
        public double sec; // 체결 시간
        public double torqueMinLimit;     // 116-121
        public double torqueMaxLimit;     // 124-129
    }

    public class SendInfo
    {
        public bool isStringPacket;
        public string text;
        public byte[] data = null;
    }

    public event EventHandler recvNetworkPacket;

    string m_ip = "";
    int m_port = 0;

    bool m_stop = false;
    Socket m_socket = null;

    CircularBuffer m_recvBuffer = new CircularBuffer(1024 * 1024);
    Queue<SendInfo> m_sendQueue = new Queue<SendInfo>();

    ManualResetEvent m_manualResetEvent = new ManualResetEvent(false);

    Thread m_thread = null;

    object m_lockObject = new object();

    PSet m_pset = 0;
    CElaspedTimer m_pSetChangeTimer = new CElaspedTimer(200);

    bool m_isEndTightData = false;

    public CNetworkPF(string ip, int port)
    {
        m_ip = ip;
        m_port = port;

        m_thread = new Thread(run);
    }

    public void start()
    {
        if (m_thread != null)
            m_thread.Start();
    }

    public void stop()
    {
        Debug.debug("CNetworkPF::stop");
        m_stop = true;

        m_thread.Join(100);
    }

    public bool isConnected()
    {
        if (m_socket == null)
            return false;

        try
        {
            bool retPoll = m_socket.Poll(1, SelectMode.SelectRead);
            int socketAvailable = m_socket.Available;

            bool connected = !(retPoll && (socketAvailable == 0));

            return !(m_socket.Poll(1, SelectMode.SelectRead) && m_socket.Available == 0);
        }
        catch (Exception e)
        {
            Debug.debug("CNetworkPF::isConnected error:" + e.Message);
            return false;
        }
    }

    public void run()
    {
        Debug.warning("CNetworkPF::run START" + " IP:" + m_ip + " port:" + m_port.ToString());

        bool heartBeatCheck = false;
        int heartBeatTick = 0;

        while (true)
        {
            //Debug.debug("NetworkClient::run RUN #1");

            if (m_stop)
            {
                Debug.warning("CNetworkPF::run thread is stop");
                break;
            }

            if (m_socket == null)
            {
                m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_socket.ReceiveTimeout = 100;
                //m_socket.NoDelay = true;

                try
                {
                    m_socket.Connect(m_ip, m_port);
                    Thread.Sleep(100);
                    //SocketExtensions.Connect(m_socket, m_ip, m_port, new TimeSpan(100));
                }
                catch (Exception e)
                {
                    Debug.debug("CNetworkPF::run connected error:" + e.Message);

                    try
                    {
                        m_socket.Close();
                    }
                    catch (Exception)
                    {
                    }

                    m_socket = null;

                    Thread.Sleep(2000);
                }

                send_MID(CNetworkPF.SEND_MID.COMMUNICATION_START);
            }

            if (isConnected())
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
                            keepAlive();
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
                Debug.debug("CNetworkPF::run disconncted");

                if (m_socket != null)
                {
                    m_socket.Shutdown(SocketShutdown.Both);
                    m_socket.Close();
                    m_socket = null;
                }

                Thread.Sleep(1000);
            }

            //Debug.debug("NetworkClient::run RUN #2");

            Thread.Yield();
            Thread.Sleep(50);
        }

        Debug.warning("CNetworkPF::run END");
    }

    public bool connected()
    {
        try
        {
            return m_socket.Connected;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public void disconnected()
    {
        //m_socket.Disconnect(true);
        m_socket.Shutdown(SocketShutdown.Both);
        m_socket.Close();
        m_socket = null;
    }

    private void connectCallback(IAsyncResult result)
    {
        Debug.debug("CNetworkPF::connectCallback");

        Socket socket = (Socket)result.AsyncState;

        try
        {
            socket.EndConnect(result);
        }
        catch (Exception)
        {
        }
    }

    private bool recvProcess()
    {
        int len = m_socket.Available;

        if (len > 0)
        {
            byte[] data = new byte[len];
            int n = m_socket.Receive(data);

            if (m_recvBuffer.size() + len > m_recvBuffer.capacity() - 1)
            {
                Debug.warning("CNetworkPF::recvProcess m_recvBuffer is overflow");
                return false;
            }

            m_recvBuffer.write(data, (uint)len);
        }

        return processIncomingData();
    }

    private bool send(byte[] data, bool isStringPacket = false, string text = "")
    {
        Monitor.Enter(m_lockObject);

        //Debug.debug("CNetworkPF send START");

        string debugText = "";

        for (int i = 0; i < data.Length; i++)
        {
            debugText += data[i].ToString("02X") + " ";
        }

        //Debug.debug("CNetworkPF::send length:" + data.Length + " data:" + debugText);

        try
        {

            SendInfo info = new SendInfo();

            info.isStringPacket = isStringPacket;
            info.text = text;
            info.data = data;

            m_sendQueue.Enqueue(info);

            if (data == null)
            {
                Debug.warning("CNetworkPF::send data is null!");
                return false;
            }

            //Debug.debug("CNetworkPF::send length:" + data.Length + " text:" + text);

            return true;
        }
        finally
        {
            //Debug.debug("CNetworkPF send END");
            Monitor.Exit(m_lockObject);
        }
    }

    public void sendString(string packet)
    {
        byte[] data = ASCIIEncoding.ASCII.GetBytes(packet);

        byte[] data2 = new byte[data.Length + 1];

        Util.memcpy(ref data2, 0, data, 0, data.Length);

        data2[data.Length] = 0x0;

        send(data2);
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
                    Debug.warning("CNetworkPF::sendProcess data is null");
                    m_sendQueue.Dequeue();
                    return false;
                }

                try
                {
                    if (info.isStringPacket)
                        Debug.debug("CNetworkPF::sendProcess sendStringPacket text:" + info.text);

                    int n = m_socket.Send(info.data);
                    //Debug.debug("NetworkCClient::sendProcess n:" + n);

                    if (n != info.data.Length)
                    {
                        Debug.warning("CNetworkPF::sendProcess send failed");
                        return false;
                    }

                    m_sendQueue.Dequeue();
                }
                catch (Exception e)
                {
                    Debug.debug("CNetworkPF::sendProcess error:" + e.Message);
                    Debug.debug("CNetworkPF::sendProcess stackTrace:" + e.StackTrace);
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
            int len = (int)m_recvBuffer.size();

            if (len == 0) return true;
            if (len < 4) return true;

            string lengthText = "";
            string MIDText = "";
            string RevText = "";
            //string spareText = "";

            int length;
            int MID;
            //int REV;

            byte[] lengthBuff = new byte[4];
            m_recvBuffer.peek(ref lengthBuff, 4);

            lengthText = ASCIIEncoding.ASCII.GetString(lengthBuff);

            length = Convert.ToInt32(lengthText);

            if (m_recvBuffer.size() < length + 1)
                return false;

            byte[] packetData = new byte[length + 1];
            m_recvBuffer.read(ref packetData, (uint)(length + 1));

            MIDText = ASCIIEncoding.ASCII.GetString(packetData, 4, 4);
            RevText = ASCIIEncoding.ASCII.GetString(packetData, 8, 4);

            MID = Convert.ToInt32(MIDText);

            if (MID != 9999)        // not keep alive
            {
                Debug.debug("CNetworkPF::processIncomingData recv MID:" + MID.ToString() + " length:" + length.ToString());
                Debug.debug("CNetworkPF::processIncomingData recv MIDText:" + MIDText);
            }
           
            if (MID == 61)
            {
                // 데이터를 받았다는 응답을 해줘야함.
                send_MID(SEND_MID.TIGHTENING_LAST_DATA_RESULT);                
                
                //public int p_id;        // 90-92
                //bool tighteningStatus;  // 107
                //public double torque;   // 140-145
                //public double angle;    // 169-173
                //public double torqueMinLimit;     // 116-121
                //public double torqueMaxLimit;     // 124-129

                string req_p_id = ASCIIEncoding.ASCII.GetString(packetData, 90, 3);
                string req_tighteningStatus = ASCIIEncoding.ASCII.GetString(packetData, 107, 1);
                string req_torque = ASCIIEncoding.ASCII.GetString(packetData, 140, 6);
                string req_angle = ASCIIEncoding.ASCII.GetString(packetData, 169, 5);
                string req_torqueMin = ASCIIEncoding.ASCII.GetString(packetData, 116, 6);
                string req_torqueMax = ASCIIEncoding.ASCII.GetString(packetData, 124, 6);

                string dateTimeStr = ASCIIEncoding.ASCII.GetString(packetData, 176, 19);
                DateTime dateTime = DateTime.ParseExact(dateTimeStr, "yyyy-MM-dd:HH:mm:ss", null);
                TimeSpan result = DateTime.Now - dateTime;

                //Debug.debug("CNetworkPF::processIncomingData recv req_p_id:" + req_p_id);
                //Debug.debug("CNetworkPF::processIncomingData recv req_tighteningStatus:" + req_tighteningStatus);
                //Debug.debug("CNetworkPF::processIncomingData recv req_torque:" + req_torque);
                //Debug.debug("CNetworkPF::processIncomingData recv req_angle:" + req_angle);
                //Debug.debug("CNetworkPF::processIncomingData recv date....1:" + dateTimeStr); //2017-07-01:19:46:20
                //Debug.debug("CNetworkPF::processIncomingData recv date....2:" + dateTime);
                //Debug.debug("CNetworkPF::processIncomingData recv date....3 result: " + result+ " mm:" + result.TotalSeconds);

                Result packet = new Result();
                packet.p_id = Convert.ToInt32(req_p_id);
                packet.angle = Convert.ToDouble(req_angle);
                packet.torque = Convert.ToDouble(req_torque) / 100.0f;
                packet.torqueMinLimit = Convert.ToDouble(req_torqueMin) / 100.0f;
                packet.torqueMaxLimit = Convert.ToDouble(req_torqueMax) / 100.0f;
                packet.sec = result.TotalSeconds;
                
                if (req_tighteningStatus == "1")
                    packet.tighteningStatus = true;
                else
                    packet.tighteningStatus = false;

#if false
                if (m_isEndTightData == false)
                    return true;

                m_isEndTightData = false;
#endif

#if true // 이벤트 호출 부분
                try
                {
                    if (recvNetworkPacket != null)
                        recvNetworkPacket(packet, null);
                }
                catch (Exception ex)
                {
                    Debug.critical(ex,
                    "CNetworkPF::processIncomingData ip:" + m_ip + " port:" + m_port);
                }
#endif
            }
        }
    }

    public void send_MID(SEND_MID mid)
    {
        //Debug.debug("CNetworkPF::send_MID mid:" + mid);
        if (mid == SEND_MID.COMMUNICATION_START)
        {
            sendString("00200001003         ");
        }
        else if (mid == SEND_MID.TIGHTENING_LAST_DATA)
        {
            m_isEndTightData = true;
            sendString("002000600010        ");
        }
        else if (mid == SEND_MID.TIGHTENING_LAST_DATA_RESULT)     //결과 데이터를 받은 후 받았다는 응답에 쓰임.
        {
            sendString("002000620010        ");
        }
        else if (mid == SEND_MID.KEEP_ALIVE)
        {
            sendString("002099990010        ");
        }
    }

    public void sendPSetChange(PSet pSetNo)
    {
        sendString("002000100011        ");

        Util.waitTick(500);

        //Debug.debug("###########@@@@@ CNetworkPF::send_Pset p_Num:" + pSetNo);
        m_pset = pSetNo;

        if ((int)pSetNo < 10) 
            sendString("002300180010    00  00" + Convert.ToInt32(pSetNo));
        else
            sendString("002300180010    00  0" + Convert.ToInt32(pSetNo));

        m_pSetChangeTimer.start();
    }

    public void sendPSetChange(int pSetNo)
    {
        sendString("002000100011        ");

        Util.waitTick(500);

        //Debug.debug("###########@@@@@ CNetworkPF::send_Pset p_Num:" + pSetNo);
        m_pset = (PSet)pSetNo;

        if (pSetNo < 10)
            sendString("002300180010    00  00" + pSetNo);
        else
            sendString("002300180010    00  0" + pSetNo);

        m_pSetChangeTimer.start();
    }

    public bool isChangedPSet()
    {
        return m_pSetChangeTimer.isElasped();
    }

    private void keepAlive()
    {
        //sendString("002099990010        ");
        send_MID(SEND_MID.KEEP_ALIVE);
    }

    public bool isEndTightData()
    {
        return m_isEndTightData;
    }
}
