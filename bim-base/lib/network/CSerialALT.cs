using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.IO;

public class CSerialALT
{
    byte STX = 0x02;
    byte ETX = 0x03;

    static int READ_BUFF_SIZE = 1024;

    public event EventHandler IncomingData;

    SerialPort m_serialPort = null;

    CircularBuffer m_recvBuffer = new CircularBuffer(1024 * 1024); // 1MB
    Queue<byte[]> m_sendQueue = new Queue<byte[]>();

    byte[] m_readBuffer = new byte[READ_BUFF_SIZE];

    Thread m_thread = null;
    bool m_stop = false;

    bool m_simulation = false;

    public CSerialALT(SerialPort serialPort)
    {
        m_serialPort = serialPort;

        m_serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

        if (File.Exists(pathUtil.savePath() + "\\simulation"))
            m_simulation = true;

        m_thread = new Thread(run);
    }

    ~CSerialALT()
    {
        m_stop = true;
    }

    public void setSTX(byte value)
    {
        STX = value;
    }

    public void setETX(byte value)
    {
        ETX = value;
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
        Debug.debug("CSerialALT::run START name:" + m_serialPort.PortName);

        while (true)
        {
            if (m_stop)
            {
                Debug.debug("CSerialALT::run STOP");
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
                    "CSerialALT::run port:" + m_serialPort.PortName);
            }

            Thread.Yield();
            Thread.Sleep(50);
        }

        Debug.debug("CSerialALT::run END");
    }

    public void setBright(byte ch, byte brightness)
    {
        if (ch < 0 || ch > 4)
        {
            throw new IndexOutOfRangeException("채널범위 0~4");
        }
        
        byte[] data = new byte[10];

        data[0] = 0xEF;
        data[1] = 0xEF;

        data[2] = 0; // COMMAND

        data[3 + ch] = brightness;
        
        byte checksum = 0x0;

        for (int i = 2; i < 5; i++)
        {
            checksum ^= data[i];
        }

        checksum ^= (byte)(data[6] + 0x01);

        data[7] = checksum;

        data[8] = 0xEE;
        data[9] = 0xEE;

        send(data);
    }

    public void setOmronBrightness(string brightness)
    {

        byte[] start = new byte[2];
        start[0] = 0x02; // Start Bit
        start[1] = 0x57; // W = 밝기 조절

        byte[] str = Encoding.UTF8.GetBytes("CHA"); // CHA = 전체 채널

        brightness = brightness.PadLeft(3, '0');

        byte[] bright = Encoding.UTF8.GetBytes(brightness);
        
        byte[] end = new byte[1]; 
        end[0] = 0x03; // End Bit

        byte[] data = new byte[9];
        start.CopyTo(data, 0);
        str.CopyTo(data, start.Length);
        bright.CopyTo(data, start.Length + str.Length);
        end.CopyTo(data, start.Length + str.Length + bright.Length);

        Debug.debug("CSerialALT::setOmronBrightness : " + data); 
        send(data);
    }

    public void setOmronLight(bool status)
    {

        byte[] start = new byte[2];
        start[0] = 0x02; // Start Bit
        start[1] = 0x43; // C = 조명 제어

        byte[] str = Encoding.UTF8.GetBytes("CHA"); // CHA = 전체 채널

        byte[] end = new byte[2];
        end[0] = 0x31; // 1 = ON 
        if (status == false)
            end[0] = 0x30; // 0 = OFF

        end[1] = 0x03; // End Bit

        byte[] data = new byte[7];
        start.CopyTo(data, 0);
        str.CopyTo(data, start.Length);
        end.CopyTo(data, start.Length + str.Length);

        Debug.debug("CSerialALT::setOmronBrightness : " + data);
        send(data);
    }

    public void setOmronLightChannel(int channel, bool status)
    {

        byte[] start = new byte[2];
        start[0] = 0x02; // Start Bit
        start[1] = 0x43; // C = 조명 제어

        byte[] str = Encoding.UTF8.GetBytes("CH" + channel); // CH0, CH1 등등

        byte[] end = new byte[2];
        
        end[0] = 0x31; // 1 = ON
        if (status == false)
            end[0] = 0x30; // 0 = OFF

        end[1] = 0x03; // End Bit

        byte[] data = new byte[7];
        start.CopyTo(data, 0);
        str.CopyTo(data, start.Length);
        end.CopyTo(data, start.Length + str.Length);

        Debug.debug("CSerialALT::setOmronBrightness : " + data);
        send(data);
    }

    private void send(byte[] data)
    {
        lock (m_sendQueue)
        {
            m_sendQueue.Enqueue(data);
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

        Debug.debug("CSerialALT::processIncoming data:" + text);

        if (IncomingData != null)
            IncomingData(data, null);
    }
}
