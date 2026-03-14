using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.IO;
using Microsoft.Win32;

public class CSerialZPL
{
    static int READ_BUFF_SIZE = 1024;

    public event EventHandler IncomingData;

    SerialPort m_serialPort = null;

    CircularBuffer m_recvBuffer = new CircularBuffer(1024 * 1024); // 1MB
    Queue<byte[]> m_sendQueue = new Queue<byte[]>();

    byte[] m_readBuffer = new byte[READ_BUFF_SIZE];

    Thread m_thread = null;
    bool m_stop = false;
    bool m_simulation = false;

    public CSerialZPL(SerialPort serialPort)
    {
        m_serialPort = serialPort;
        m_serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

        if (File.Exists(pathUtil.myDocumnent() + "\\simulation"))
            m_simulation = true;

        m_thread = new Thread(run);
    }

    ~CSerialZPL()
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

    public void setPort(string portName)
    {
        m_serialPort.PortName = portName;
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
        Debug.debug("CSerial::run START COM:" + m_serialPort.PortName);

        while (true)
        {
            if (m_stop)
            {
                Debug.debug("CSerial::run STOP");
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
                    "CSerialZPL::run port:" + m_serialPort.PortName);
            }

            Thread.Yield();
            Thread.Sleep(50);
        }

        Debug.debug("CSerial::run END");
    }

    public void send(byte[] data)
    {
        if (isConnected() == false)
            return;

        lock (m_sendQueue)
        {
            m_sendQueue.Enqueue(data);
        }
    }

    public void send(string text)
    {
        if (isConnected() == false)
            return;

        byte[] data = Encoding.UTF8.GetBytes(text);

        lock (m_sendQueue)
        {
            m_sendQueue.Enqueue(data);
        }
    }

    private void sendProcess()
    {
        lock (m_sendQueue)
        {
            if (isConnected() == false)
                return;

            if (m_sendQueue.Count == 0)
                return;

            foreach (var queue in m_sendQueue)
            {
                m_serialPort.Write(queue, 0, queue.Count());
            }

            m_sendQueue.Clear();
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
            }
        }

        return true;
    }

    private void processIncmonig(byte[] data)
    {
        string text = "";

        for (int i = 0; i < data.Length; i++)
            text += data[i].ToString("X2") + " ";

        Debug.debug("CSerialBCR::processIncoming data:" + text);

        if (IncomingData != null)
            IncomingData(data, null);
    }

    public List<string> getPortList()
    {
        List<string> lstPorts = new List<string>();
        RegistryKey rkRoot = Registry.LocalMachine.OpenSubKey("HARDWARE");
        RegistryKey rkSubKey = rkRoot.OpenSubKey("DEVICEMAP\\SERIALCOMM");

        if (rkSubKey == null || rkSubKey.ValueCount == 0)
        {
            lstPorts.Add("none");
        }
        else
        {
            string[] tmpCom = rkSubKey.GetValueNames();
            for (int i = 0; i < rkSubKey.ValueCount; i++)
            {
                lstPorts.Insert(0, (rkSubKey.GetValue(tmpCom[i]).ToString()));
            }
        }
        return lstPorts;
    }
}
