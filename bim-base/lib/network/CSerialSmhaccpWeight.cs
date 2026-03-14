using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.IO;

public class CSerialSmhaccpWeight
{
    static int READ_BUFF_SIZE = 1024;

    public event EventHandler IncomingData;

    SerialPort m_serialPort = null;

    CircularBuffer m_recvBuffer = new CircularBuffer(1024 * 1024); // 1MB
    Queue<string> m_sendQueueString = new Queue<string>();
    Queue<byte[]> m_sendQueueByte = new Queue<byte[]>();

    byte[] m_readBuffer = new byte[READ_BUFF_SIZE];

    Thread m_thread = null;
    bool m_stop = false;
    bool m_simulation = false;

    public CSerialSmhaccpWeight(SerialPort serialPort)
    {
        m_serialPort = serialPort;
        m_serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

        if (File.Exists(pathUtil.myDocumnent() + "\\simulation"))
            m_simulation = true;

        m_thread = new Thread(run);
    }

    ~CSerialSmhaccpWeight()
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
        Debug.debug("CSerialSmhaccpMicom::run START COM:" + m_serialPort.PortName);

        while (true)
        {
            if (m_stop)
            {
                Debug.debug("CSerialSmhaccpMicom::run STOP");
                break;
            }

            if (m_simulation)
            {
                sendProcessString();
                sendProcessByte();
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
                sendProcessString();
                sendProcessByte();
                recvProcess();
            }
            catch (Exception ex)
            {
                Debug.critical(ex,
                    "CSerialSmhaccpMicom::run port:" + m_serialPort.PortName);
            }

            Thread.Yield();
            Thread.Sleep(50);
        }

        Debug.debug("CSerialSmhaccpMicom::run END");
    }

    public void send(string data)
    {
        if (isConnected() == false)
            return;

        lock (m_sendQueueString)
        {
            m_sendQueueString.Enqueue(data);
        }
    }

    public void send(byte[] data)
    {
        if (isConnected() == false)
            return;

        lock (m_sendQueueByte)
        {
            m_sendQueueByte.Enqueue(data);
        }
    }

    private void sendProcessString()
    {
        lock (m_sendQueueString)
        {
            if (isConnected() == false)
                return;

            if (m_sendQueueString.Count == 0)
                return;

            foreach (string queue in m_sendQueueString)
            {
                Debug.debug("CSerialSmhaccpWeight::sendProcessString queue:" + queue.Length + " " + queue);
                m_serialPort.Write(queue);
            }

            m_sendQueueString.Clear();
        }
    }

    private void sendProcessByte()
    {
        lock (m_sendQueueByte)
        {
            if (isConnected() == false)
                return;

            if (m_sendQueueByte.Count == 0)
                return;

            foreach (byte[] queue in m_sendQueueByte)
            {
                Debug.debug("CSerialSmhaccpWeight::sendProcessByte queue:" + queue.Length + " " + queue.ToString());
                m_serialPort.Write(queue, 0, queue.Length);
            }

            m_sendQueueByte.Clear();
        }
    }

    private bool recvProcess()
    {
        lock (m_recvBuffer)
        {
            byte[] buff = new byte[m_recvBuffer.size()];
            m_recvBuffer.read(ref buff, m_recvBuffer.size());

            if (buff.Length < 1)
                return false;

            Debug.debug("CSerialSmhaccpMicom::recvProcess() Length : " + buff.Length);
            processIncmonig(buff);
        }

        return true;
    }

    private void processIncmonig(byte[] data)
    {
        string text = "";

        for (int i = 0; i < data.Length; i++)
            text += data[i].ToString("X2") + " ";

        Debug.debug("CSerialSmhaccpMicom::processIncoming data:" + text);

        if (IncomingData != null)
            IncomingData(data, null);
    }
}
