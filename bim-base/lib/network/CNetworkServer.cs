using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;

using System.Drawing;

public class CNetworkServer
{
    public event EventHandler connectedFromClient;
    public event EventHandler disconnectedFromClient;

    static CNetworkServer m_instance = null;

    byte[] bytes = new Byte[1024*1024];

    private List<CNetworkClient> m_clientList = new List<CNetworkClient>();

    private static ManualResetEvent manualResetEvent = new ManualResetEvent(false);

    private bool m_stop = false;
    private Thread m_thread = null;

    EventHandler m_recvNetworkHandler;

    string m_ip;
    int m_port;

    bool m_useSingleConnection = false;

    bool m_recvUseStxEtx = false;

    byte STX = 0x02;
    byte ETX = 0x03;
    uint stxEtxLength = 0;

    public CNetworkServer(string ip, int port, EventHandler recvDataHandler, bool useSingleConnection = false)
    {
        m_ip = ip;
        m_port = port;

        m_useSingleConnection = useSingleConnection;

        m_instance = this;
        m_recvNetworkHandler = recvDataHandler;

        m_thread = new Thread(run);
        m_thread.Start();
    }

    /// <summary> NetworkServerClient 반환 </summary>
    /// <param name="index">G/W 의 경우는 0, 1 ~ 27 은 LMS PC</param>
    /// <returns></returns>
    public CNetworkClient client(int index)
    {
        if (index < 0 || index > m_clientList.Count)
        {
            //Debug.debug("CNetworkServer::client invalid index:" + index);
            return null;
        }

        if (m_clientList.Count < index + 1)
            return null;

        return m_clientList[index];
    }

    public int clientCount()
    {
        if (m_clientList == null)
            return 0;

        return m_clientList.Count;
    }

    public void connectedFromServerClient(CNetworkClient client)
    {
        Debug.debug("CNetworkServer::connectedFromServerClient");
        client.recvData += m_recvNetworkHandler;
        client.close += client_close;

        if (connectedFromClient != null)
            connectedFromClient(client, null);
    }

    void client_close(object sender, EventArgs e)
    {
        CNetworkClient client = (CNetworkClient)sender;

        lock (m_clientList)
        {
            m_clientList.Remove(client);
        }
    }

    public void disconnectedFromServerClient(CNetworkClient client)
    {

        lock (m_clientList)
        {
            Debug.debug("CNetworkServer::disconnectedFromServerClient");

            if (disconnectedFromClient != null)
                disconnectedFromClient(client, null);

            m_clientList.Remove(client);
        }
    }

    public void stop()
    {
        m_stop = true;
        manualResetEvent.Set();

        lock (m_clientList)
        {
            foreach (CNetworkClient client in m_clientList)
            {
                if (client == null)
                    continue;

                client.stop();
            }
        }

        Application.DoEvents();
        m_thread.Join();
    }

    public void run()
    { 
        Debug.debug("CNetworkServer::run START ip:" + m_ip + " port:" + m_port);

        try
        {
            // ipHostInfo = Dns.Resolve(Dns.GetHostName());

            //IPAddress ipAddress = IPAddress.Parse("192.168.1.100");
            IPEndPoint localEndPoint = null;

            if (m_ip == "localhost")
            {
                localEndPoint = new IPEndPoint(IPAddress.Any, m_port);
            }
            else
            {
                IPAddress ipAddress = IPAddress.Parse(m_ip);
                localEndPoint = new IPEndPoint(ipAddress, m_port);
            }

            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            //listener.NoDelay = true;


            listener.Bind(localEndPoint);
            listener.Listen(100);

            while (true)
            {
                //Debug.warning("NetworkServer::run");

                if (m_stop)
                {
                    Debug.warning("NetworkServer::run thread is stop");
                    break;
                }

                manualResetEvent.Reset();
                listener.BeginAccept(new AsyncCallback(acceptCallBack), listener);
                manualResetEvent.WaitOne();

                // Thread.Sleep(1);
            }


            for (int i = 0; i < m_clientList.Count; i++)
            {
                Debug.debug("NetworkServer::NetworkServer thread close index:" + i);

                int tickCount = Environment.TickCount;

                //100ms delay
                while (true)
                {
                    if (Environment.TickCount - tickCount > 100)
                        break;
                }

                if ((m_clientList.Count - 1) < i)
                    return;

                if (m_clientList[i] != null)
                    m_clientList[i].stop();
            }

            //100ms delay
            while (true)
            {
                bool result = true;

                foreach (CNetworkClient client in m_clientList)
                {
                    if (client == null) continue;

                    if (result && client.isStop() == false)
                        result = false;
                }

                if (result)
                    break;
            }

            m_clientList.Clear();

            listener.Shutdown(SocketShutdown.Both);
            listener.Close();
        }
        catch (Exception ex)
        {
            Debug.warning("CNetworkServer::run error reason:" + ex.Message);
        }

        //listener.Close();

        Debug.debug("CNetworkServer::run END");
    }

    public void setUseRecvStxEtx(bool value)
    {
        m_recvUseStxEtx = value;
    }

    private void broadCast(string text)
    {
        for (int i = 0; i < m_clientList.Count; i++)
        {
            byte[] data = Encoding.ASCII.GetBytes(text);
            m_clientList[i].send(data);
        }
    }

    private void acceptCallBack(IAsyncResult result)
    {
        manualResetEvent.Set();

        if (m_stop)
            return;

        Socket listener = (Socket)result.AsyncState;
        Socket socket = listener.EndAccept(result);

        Debug.debug("NetworkServer::acceptCallBack socket:" + socket.Handle + " ip:" + socket.RemoteEndPoint + " port:" + m_port);

        if (m_useSingleConnection)
        {
            lock (m_clientList)
            {
                Debug.debug("CNetworkServer::acceptCallBack nowClient count:" + m_clientList.Count);

                if (m_clientList.Count > 5)
                {
                    for (int i = 0; i < m_clientList.Count - 5; i++)
                    {
                        if (m_clientList[i] != null)
                        {
                            m_clientList[i].stop();
                            m_clientList[i].disconnected();
                        }
                    }

                    for (int i = 0; i < m_clientList.Count - 5; i++)
                    {
                        m_clientList.RemoveAt(i);
                    }
                }
            }

            System.GC.Collect();
        }

        string text = socket.RemoteEndPoint.ToString();
        string[] splitText = text.Split(':');

        string ip = splitText[0];
        string[] splitIP = ip.Split('.');

        string remoteIP = ((IPEndPoint)socket.RemoteEndPoint).Address.ToString();
        string remotePort = ((IPEndPoint)socket.RemoteEndPoint).Port.ToString();

        int remotePortNo = Util.toInt32(remotePort);

        CNetworkClient networkClient = new CNetworkClient(socket, remoteIP, remotePortNo);

        if (m_recvUseStxEtx)
        {
            networkClient.useStxEtx(m_recvUseStxEtx);
            networkClient.setStxEtxLength(stxEtxLength);
        }

        lock (m_clientList)
        {
            m_clientList.Add(networkClient);
        }

        networkClient.start();

        connectedFromServerClient(networkClient);
    }

    public bool sendStringBraodcast(string text, bool isUtf8 = false)
    {
        if (m_clientList.Count == 0)
            return false;

        foreach (CNetworkClient client in m_clientList)
        {
            if (client.isConnected() == false)
                continue;

            if (isUtf8)
                client.sendStringUtf8(text);
            else
                client.sendString(text);
        }

        return true;
    }

    public bool sendStringBraodcastWithStxEtx(string text)
    {
        if (m_clientList.Count == 0)
            return false;

        foreach (CNetworkClient client in m_clientList)
        {
            if (client.isConnected() == false)
                continue;

            client.sendStringWithStxEtx(text);
        }

        return true;
    }

    public bool sendString(int index, string text)
    {
        if (m_clientList.Count == 0)
            return false;

        m_clientList[index].sendString(text);
        return true;
    }

    public bool sendBraodcast(byte[] data)
    {
        if (m_clientList.Count == 0)
            return false;


        try
        {
            foreach (CNetworkClient client in m_clientList)
            {
                if (client.isConnected() == false)
                    continue;

                client.send(data);
            }
        }catch(Exception /*ex*/)
        {

        }

        return true;
    }

    public bool send(int index, byte[] data)
    {
        if (m_clientList.Count == 0)
            return false;

        m_clientList[index].send(data);
        return true;
    }

    public bool isConnected()
    {
        lock (m_clientList)
        {
            foreach (CNetworkClient client in m_clientList)
            {
                if (client == null)
                    continue;

                if (client.isConnected() == true)
                {
                    return true;
                }
            }
        }

        return false;
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
        foreach (CNetworkClient client in m_clientList)
        {
            client.bufferClear();
        }
    }
} // Class
