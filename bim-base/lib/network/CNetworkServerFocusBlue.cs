using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;

using System.Drawing;

class CNetworkServerFocusBlue
{
    public event EventHandler connectedFromClient;
    public event EventHandler disconnectedFromClient;

    static CNetworkServerFocusBlue m_instance = null;
    static object m_object = new object();

    byte[] bytes = new Byte[1024*1024];

    private List<CNetworkClientFocusBlue> m_clientList = new List<CNetworkClientFocusBlue>();

    private ManualResetEvent manualResetEvent = new ManualResetEvent(false);

    private bool m_stop = false;
    private Thread m_thread = null;

    EventHandler m_recvNetworkHandler;

    string m_ip;
    int m_port;

    public CNetworkServerFocusBlue(string ip, int port, EventHandler recvDataHandler)
    {
        m_ip = ip;
        m_port = port;

        m_instance = this;
        m_recvNetworkHandler = recvDataHandler;

        m_thread = new Thread(run);
        m_thread.Start();
    }

    /// <summary> NetworkServerClient 반환 </summary>
    /// <param name="index">G/W 의 경우는 0, 1 ~ 27 은 LMS PC</param>
    /// <returns></returns>
    public CNetworkClientFocusBlue client(int index)
    {
        if (index < 0 || index > m_clientList.Count)
        {
            Debug.debug("CNetworkServer::client invalid index:" + index);
            return null;
        }

        if (m_clientList.Count < index + 1)
            return null;

        return m_clientList[index];
    }

    public void connectedFromServerClient(CNetworkClientFocusBlue client)
    {
        lock (m_object)
        {
            Debug.debug("CNetworkServer::connectedFromServerClient");
            client.recvData += m_recvNetworkHandler;

            if (connectedFromClient != null)
                connectedFromClient(client, null);
        }
    }

    public void disconnectedFromServerClient(CNetworkClientFocusBlue client)
    {

        lock (m_object)
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

        foreach (CNetworkClient client in m_clientList)
        {
            if (client == null)
                continue;

            client.stop();
        }

        Application.DoEvents();
        m_thread.Join();
    }

    public void run()
    {
        Debug.debug("CNetworkServer::run START");

        // ipHostInfo = Dns.Resolve(Dns.GetHostName());

        //IPAddress ipAddress = IPAddress.Parse("192.168.1.100");
        IPAddress ipAddress = IPAddress.Parse(m_ip);
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, m_port);
            
        Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        listener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

        //listener.NoDelay = true;

        try
        {
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

        }
        catch (Exception e)
        {
            Debug.warning("NetworkServer::listen error:" + e.ToString());
        }

        for (int i = 0; i < m_clientList.Count; i++)
        {
            m_clientList[i].sendString("CLOSE");

            int tickCount = Environment.TickCount;

            //100ms delay
            while (true)
            {
                if (Environment.TickCount - tickCount > 100)
                    break;
            }

            Thread.Sleep(1000);
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

            m_clientList[i].stop();
        }

        //100ms delay
        while (true)
        {
            bool result = true;

            foreach (CNetworkClientFocusBlue client in m_clientList)
            {
                if (client == null) continue;

                if (result && client.isStop() == false)
                    result = false;
            }

            if (result)
                break;
        }

        try
        {
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

        Debug.debug("NetworkServer::acceptCallBack socket:" + socket.Handle);

        string text = socket.RemoteEndPoint.ToString();
        string[] splitText = text.Split(':');

        string ip = splitText[0];
        string[] splitIP = ip.Split('.');

        lock (m_object)
        {
            foreach (CNetworkClient client in m_clientList)
            {
                client.stop();
            }

            m_clientList.Clear();

            string remoteIP = ((IPEndPoint)socket.RemoteEndPoint).Address.ToString();
            string remotePort = ((IPEndPoint)socket.RemoteEndPoint).Port.ToString();

            int remotePortNo = Util.toInt32(remotePort);

            CNetworkClientFocusBlue networkClient = new CNetworkClientFocusBlue(socket, remoteIP, remotePortNo);
            m_clientList.Add(networkClient);

            networkClient.recvData += m_recvNetworkHandler;
            networkClient.start();
        }

        // 소켓은 handler 임
    }

    public bool sendString(int index, string text)
    {
        if (m_clientList.Count == 0)
            return false;

        m_clientList[index].sendString(text);
        return true;
    }
}
