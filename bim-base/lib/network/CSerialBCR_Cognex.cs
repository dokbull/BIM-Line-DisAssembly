using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
public class CSerialBCR_Cognex
{
    static byte STX = 0x16;
    static byte ETX = 0x0D;

    static int READ_BUFF_SIZE = 1024;

    public event EventHandler IncomingData;

    SerialPort m_serialPort = null;

    CircularBuffer m_recvBuffer = new CircularBuffer(1024 * 1024); // 1MB
    Queue<byte[]> m_sendQueue = new Queue<byte[]>();

    byte[] m_readBuffer = new byte[READ_BUFF_SIZE];

    Thread m_thread = null;
    bool m_stop = false;

    bool m_simulation = false;
    bool m_isRecv = true;

    bool m_useETX = true;
    public CSerialBCR_Cognex(SerialPort serialPort)
    {
        m_serialPort = serialPort;
        m_serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

        if (File.Exists(pathUtil.myDocumnent() + "\\simulation"))
            m_simulation = true;

        m_thread = new Thread(run);
    }
    ~CSerialBCR_Cognex()
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
    public string sendTriggerOn()
    {
        string data = "||>trigger on\r\n";
        byte[] packet = MakePacket(data);
        send(packet);

        return data;
    }
    public string sendTriggerOff()
    {
        string data = "||>trigger off\r\n";
        byte[] packet = MakePacket(data);
        send(packet);

        return data;
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
    public void setUseETX(bool value)
    {
        m_useETX = value;
    }

    public void setETX(byte value)
    {
        ETX = value;
    }
    public void setSTX(byte value)
    {
        STX = value;
    }
    public bool isRecv() { return m_isRecv; }

    private byte[] MakePacket(string data)
    {
        //Debug.debug("send data : " + data);
        int length = data.Length;

        byte[] packet = new byte[length];
        packet = Encoding.UTF8.GetBytes(data);

        return packet;
    }

    private void run()
    {
        Debug.debug("CSerialBCR_Cognex::run START COM:" + m_serialPort.PortName);

        while (true)
        {
            if (m_stop)
            {
                Debug.debug("CSerialBCR_Cognex::run STOP");
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
                    "CSerialBCR_Cognex::run port:" + m_serialPort.PortName);
            }

            Thread.Yield();
            Thread.Sleep(50);
        }

        Debug.debug("CSerialBCR_Cognex::run END");
    }
    public void send(byte[] data)
    {
        lock (m_sendQueue)
        {
            m_sendQueue.Enqueue(data);
        }
    }
    public void sendWithStxEtx(byte[] data)
    {
        int count = data.Length;
        byte[] sendData = new byte[count + 2];

        sendData[0] = STX;

        for (int i = 0; i < count; i++)
        {
            sendData[i + 1] = data[i];
        }

        sendData[count + 1] = ETX;

        lock (m_sendQueue)
        {
            m_sendQueue.Enqueue(sendData);
        }
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
            // STX 이전 데이터 삭제
            byte[] buff = new byte[m_recvBuffer.size()];
            m_recvBuffer.peek(ref buff, m_recvBuffer.size());

            for (int i = 0; i < (buff.Length - 1); i++)
            {
                if (buff[i] == STX)
                {
                    byte[] readData = new byte[i + 1];
                    m_recvBuffer.read(ref readData, (uint)i + 1);
                }
            }

            // 실제 데이터 수집
            buff = new byte[m_recvBuffer.size()];
            m_recvBuffer.peek(ref buff, m_recvBuffer.size());

            if (m_useETX == true)
            {
                for (int i = 0; i < buff.Length; i++)
                {
                    string text = ASCIIEncoding.ASCII.GetString(buff);

                    if (buff[i] == ETX)
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
            else
            {
                byte[] data = new byte[m_recvBuffer.size()];
                m_recvBuffer.read(ref data, (uint)data.Length);

                if (data.Length > 0)
                    processIncmonig(data);
            }
        }

        return true;
    }
    private void processIncmonig(byte[] data)
    {
        string text = "";

        for (int i = 0; i < data.Length; i++)
            text += data[i].ToString("X2") + " ";

        //Debug.debug("CSerialBCR::processIncoming data:" + text);

        if (IncomingData != null)
            IncomingData(data, null);
        m_isRecv = true;
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