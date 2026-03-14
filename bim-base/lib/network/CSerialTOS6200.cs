using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.IO;

public class CSerialTOS6200
{
    //static byte ETX = 0x0D;
    static byte LF = 0x0A;

    static int READ_BUFF_SIZE = 1024;

    public event EventHandler IncomingData;

    SerialPort m_serialPort = null;

    CircularBuffer m_recvBuffer = new CircularBuffer(1024 * 1024); // 1MB
    Queue<byte[]> m_sendQueue = new Queue<byte[]>();

    byte[] m_readBuffer = new byte[READ_BUFF_SIZE];

    Thread m_thread = null;
    bool m_stop = false;

    bool m_simulation = false;

    string m_lastSendCommnad = "";

    public CSerialTOS6200(SerialPort serialPort)
    {
        m_serialPort = serialPort;
        m_serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

        if (File.Exists(pathUtil.myDocumnent() + "\\simulation"))
            m_simulation = true;

        m_thread = new Thread(run);
    }

    ~CSerialTOS6200()
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
        Debug.debug("CSerialTOS6200::run START COM:" + m_serialPort.PortName);

        while (true)
        {
            if (m_stop)
            {
                Debug.debug("CSerialTOS6200::run STOP");
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
                    "CSerialTOS6200::run port:" + m_serialPort.PortName);
            }

            Thread.Yield();
            Thread.Sleep(50);
        }

        Debug.debug("CSerialTOS6200::run END");
    }

    public void send(string text)
    {
        m_lastSendCommnad = text;
        Debug.debug("CSerialTOS6200::send text:" + text);
        byte[] data = ASCIIEncoding.ASCII.GetBytes(text);
        send(data);
    }

    public void send(byte[] data)
    {
        byte[] packet = new byte[data.Length + 1];

        for (int i = 0; i < data.Length; i++)
        {
            packet[i] = data[i];
        }

        packet[packet.Length - 1] = LF;

        lock (m_sendQueue)
        {
            m_sendQueue.Enqueue(packet);
        }
    }

    private void sendProcess()
    {
        lock (m_sendQueue)
        {
            if (!m_serialPort.IsOpen)
                return;

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
            byte[] buff = new byte[m_recvBuffer.size()];
            m_recvBuffer.peek(ref buff, m_recvBuffer.size());

            for (int i = 0; i < buff.Length; i++)
            {
                string text = ASCIIEncoding.ASCII.GetString(buff);

                if (buff[i] == LF)
                {
                    byte[] readData = new byte[i + 1];
                    m_recvBuffer.read(ref readData, (uint)i + 1);

                    if (i > 0)
                    {
                        byte[] data = new byte[i];
                        Util.memcpy(ref data, 0, readData, 0, i);
                        processIncmonig(data);
                    }

                    return true;
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

        //Debug.debug("CSerialTOS6200::processIncoming data:" + text);

        if (IncomingData != null)
            IncomingData(data, null);
    }

    public string lastCommand()
    {
        return m_lastSendCommnad;
    }
}
