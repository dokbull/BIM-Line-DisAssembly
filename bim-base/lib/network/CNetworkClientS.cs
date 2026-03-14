using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;

#if USE_SUPERSOCKET // nuget SuperSocket.ClientEngine 설치
using System.Net;
using SuperSocket.ClientEngine;
using System.Net.NetworkInformation;

public class CNetworkClientS : CNetwork
{
    public class SendInfo
    {
        public bool isStringPacket;
        public string text;
        public byte[] data = null;
    }

    byte STX = 0x02;
    byte ETX = 0x03;

    uint stxEtxLength = 0;

    public bool m_useStxEtx = false;

    public event EventHandler recvData;

    string m_ip = "";
    int m_port = 0;

    IPEndPoint endPoint = null;

    bool m_stop = false;

    AsyncTcpSession m_session = null;

    CircularBuffer m_recvBuffer = new CircularBuffer(1024 * 1024 * 10);
    Queue<SendInfo> m_sendQueue = new Queue<SendInfo>();

    Thread m_thread = null;

    object m_lockObject = new object();

    Ping m_ping = null;
    bool m_usePingCheck = false;
    bool m_lastPingStatus = false;
    CElaspedTimer m_pingTimer = new CElaspedTimer(1 * 1000);

    int m_sleepTime = 30;

    public CNetworkClientS(string ip, int port)
    {
        m_ip = ip;
        m_port = port;

        m_ping = new Ping();
        m_pingTimer.start();

        m_thread = new Thread(run);
    }

    public CNetworkClientS(IPEndPoint _endPoint)
    {
        endPoint = _endPoint;

        m_ping = new Ping();
        m_pingTimer.start();

        m_thread = new Thread(run);
    }

    public void setSleepTime(int ms)
    {
        m_sleepTime = ms;
    }

    public void start()
    {
        if (m_thread != null)
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

        try
        {
            if (m_session != null)
                m_session.Close();
        }
        catch (Exception /*ex*/)
        {
        }

        if (m_thread.IsAlive)
            m_thread.Join(100);
    }

    public void run()
    {
        Debug.warning("CNetworkClientS::run START IP:" + m_ip + " port:" + m_port.ToString());

        while (true)
        {
            if (m_stop)
            {
                Debug.warning("CNetworkClient::run thread is stop");
                break;
            }

            if (m_session == null)
            {
                try
                {
                    if (endPoint == null)
                        endPoint = new IPEndPoint(IPAddress.Parse(m_ip), m_port);

                    m_session = new AsyncTcpSession();

                    m_session.DataReceived += dataReceived;
                    m_session.Connect(endPoint);

                    Thread.Sleep(100);
                }
                catch (Exception e)
                {
                    Debug.debug("CNetworkClientS::run connected error:" + e.Message);

                    try
                    {
                        disconnected();
                    }
                    catch (Exception /*ex*/)
                    {
                    }

                    m_session = null;

                    Thread.Sleep(500);
                }
            }

            if (connected())
            {
                recvProcess();
                sendProcess();
            }
            else
            {
                if (m_session != null)
                {
                    try
                    {
                        if (endPoint != null)
                            m_session.Connect(endPoint);
                    }
                    catch (Exception /*ex*/)
                    {

                    }
                }

                Thread.Sleep(1000);

            }

            Thread.Yield();
            Thread.Sleep(m_sleepTime);
        }

        disconnected();

        Debug.warning("CNetworkClient::run END");
    }

    public bool connected()
    {
        if (m_session == null)
            return false;

        if (m_usePingCheck)
        {
            if (m_ping != null)
            {
                if (m_pingTimer.isElasped())
                {
                    m_pingTimer.start();
                    IPAddress address = endPoint.Address;

                    try
                    {
                        PingReply ret = m_ping.Send(address, 500);

                        if (ret.Status == IPStatus.Success)
                            m_lastPingStatus = true;
                        else
                            m_lastPingStatus = false;
                    }
                    catch
                    {
                        m_lastPingStatus = false;
                    }
                }
            }

            return m_lastPingStatus;
        }

        try
        {
            return m_session.IsConnected;
        }
        catch (Exception /*ex*/)
        {
            return false;
        }
    }

    public bool isConnected()
    {
        return connected();
    }

    public void disconnected()
    {
        if (m_session == null)
            return;

        try
        {
            m_session.Close();
            m_session = null;
        }
        catch (Exception /*ex*/)
        {
        }
    }

    private bool recvProcess()
    {
        int len = (int)m_recvBuffer.size();

        if (len > 0)
        {
            try
            {
                return processIncomingData();
            }
            catch (Exception /*ex*/)
            {
                return false;
            }
        }

        return false;
    }

    private bool send(byte[] data, bool isStringPacket = false, string text = "")
    {
        Monitor.Enter(m_lockObject);

#if false
        string debugText = "";

        for (int i = 0; i < data.Length; i++)
        {
            debugText += data[i].ToString("02X") + " ";
        }
#endif

        try
        {
            if (data == null)
            {
                Debug.warning("NetworkClient::send data is null!");
                return false;
            }

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
        byte[] data = Encoding.ASCII.GetBytes(packet);
        send(data, true);
    }

    public void sendString(string packet)
    {
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
                    Debug.warning("CNetworkClientS::sendProcess data is null");
                    m_sendQueue.Dequeue();
                    return false;
                }

                try
                {
                    m_session.Send(info.data, 0, info.data.Length);
                    m_sendQueue.Dequeue();
                }
                catch (Exception e)
                {
                    Debug.debug("CNetworkClientS::sendProcess error:" + e.Message);
                    Debug.debug("CNetworkClientS::sendProcess stackTrace:" + e.StackTrace);
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

            if (m_useStxEtx == false)
            {
                byte[] data = new byte[len];
                m_recvBuffer.read(ref data, (uint)len);

                try
                {
                    recvData(data, null);
                }
                catch (Exception ex)
                {
                    Debug.critical(ex,
                        "CNetworkClientS::processIncomingData ip:" + m_ip + " port:" + m_port);
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
                        "CNetworkClientS::processIncomingData recvData ip:" + m_ip + " port:" + m_port);
                }
            }
        }
    }

    void dataReceived(object sender, DataEventArgs e)
    {
        if (m_session != null)
        {
            m_recvBuffer.write(e.Data, (uint)e.Length);
        }
    }

    public void pingCheck(bool use)
    {
        m_usePingCheck = use;
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
#endif