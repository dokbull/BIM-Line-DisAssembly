using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.IO;

public class CSerialBCRSick_CLV63x
{
    static byte STX = 0x02;
    static byte ETX = 0x03;

    static int READ_BUFF_SIZE = 1024;

    public event EventHandler IncomingData;

    SerialPort m_serialPort = null;

    CircularBuffer m_recvBuffer = new CircularBuffer(1024 * 1024); // 1MB
    Queue<byte[]> m_sendQueue = new Queue<byte[]>();

    byte[] m_readBuffer = new byte[READ_BUFF_SIZE];

    Thread m_thread = null;
    bool m_stop = false;

    bool m_simulation = false;

    public CSerialBCRSick_CLV63x(SerialPort serialPort)
    {
        m_serialPort = serialPort;
        m_serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

        if (File.Exists(pathUtil.myDocumnent() + "\\simulation"))
            m_simulation = true;

        m_thread = new Thread(run);
    }

    ~CSerialBCRSick_CLV63x()
    {
        m_stop = true;
    }

    public bool isConnected()
    {
        return m_serialPort.IsOpen;
    }

    private void dataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        SerialPort serialPort = (SerialPort)sender;

        int n = serialPort.BytesToRead;

        for (int i = 0; i < 1024; i++)
            m_readBuffer[i] = 0;

        serialPort.Read(m_readBuffer, 0, n);

        lock (m_recvBuffer)
            m_recvBuffer.write(m_readBuffer, (uint)n);
    }

    public void setSimulation(bool value)
    {
        m_simulation = value;
    }

    public void start()
    {
        m_thread.Start();
    }

    public void stop()
    {
        m_stop = true;
    }

    private void run()
    {
        Debug.debug("CSerialBCRSick_CLV63x::run START port:" + m_serialPort.PortName);

        while (true)
        {
            if (m_stop)
            {
                Debug.debug("CSerialBCRSick_CLV63x::run STOP port:" + m_serialPort.PortName);
                break;
            }

            if (m_simulation)
            {
                sendProcess();
                recvProcess();

                Thread.Sleep(50);

                continue;
            }

            if (!m_serialPort.IsOpen)
            {
                try
                {
                    m_serialPort.Open();
                }
                catch (Exception)
                {
                    Thread.Sleep(1000);
                }
            }

            try
            {
                sendProcess();
                recvProcess();
            }
            catch (Exception ex)
            {
                Debug.critical(ex,
                    "CSerialBCRSick_CLV63x::run port:" + m_serialPort.PortName);
            }

            Thread.Yield();
            Thread.Sleep(50);
        }

        Debug.debug("CSerialBCRSick_CLV63x::run END port:" + m_serialPort.PortName);
    }

    private void send(byte[] data)
    {
        lock (m_sendQueue)
        {
            m_sendQueue.Enqueue(data);
        }
    }

    public void sendString(string text)
    {
        Debug.debug("CSerialBCRSick_CLV63x::sendString text:" + text);

        byte[] data = ASCIIEncoding.ASCII.GetBytes(text);
        byte[] packet = new byte[data.Length + 2];

        for (int i = 0; i < data.Length; i++)
        {
            packet[i + 1] = data[i];
        }

        packet[0] = STX;
        packet[packet.Length - 1] = ETX;

        send(packet);
    }

    public void sendTrigger()
    {
        sendString("K");
    }

    private void sendProcess()
    {
        lock (m_sendQueue)
        {
            if (m_sendQueue.Count == 0)
                return;

            byte[] buff = m_sendQueue.Dequeue();
            m_serialPort.Write(buff, 0, buff.Length);
        }
    }

    private bool recvProcess()
    {
        lock (m_recvBuffer)
        {
            uint size = m_recvBuffer.size();

            if (size == 0)
                return false;

            bool isStx = false;

            byte[] buff = new byte[m_recvBuffer.size()];
            m_recvBuffer.peek(ref buff, m_recvBuffer.size());

            for (int i = 0; i < buff.Length; i++)
            {
                if (buff[i] == STX)
                    isStx = true;
                else if (buff[i] == ETX)
                {
                    if (isStx == false)
                    {
                        byte[] garbageBuff = new byte[i + 1];
                        m_recvBuffer.read(ref garbageBuff, (uint)i + 1);

                        return false;
                    }
                    else
                    {
                        byte[] data = new byte[i + 1];
                        m_recvBuffer.read(ref data, (uint)i + 1);
                        processIncmonig(data);

                        return true;
                    }
                }
            }
        }

        return true;
    }

    private void processIncmonig(byte[] data)
    {
        string text = "";

        for (int i = 0; i < data.Length; i++)
            text += data[i].ToString("X2") + " ";

        Debug.debug("CSerialBCRSick_CLV63x::processIncoming port:" + m_serialPort.PortName + " data:" + text);

        if (IncomingData != null)
            IncomingData(data, null);
    }
}
