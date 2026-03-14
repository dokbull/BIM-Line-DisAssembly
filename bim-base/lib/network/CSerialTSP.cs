using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.IO;

public class CSerialTSP
{
    byte STX = 0xFF;
    byte ETX = 0xFE;
    bool m_useSTX = true;

    static int READ_BUFF_SIZE = 1024;

    public event EventHandler IncomingData;

    SerialPort m_serialPort = null;

    CircularBuffer m_recvBuffer = new CircularBuffer(1024 * 1024); // 1MB
    Queue<byte[]> m_sendQueue = new Queue<byte[]>();

    byte[] m_readBuffer = new byte[READ_BUFF_SIZE];

    Thread m_thread = null;
    bool m_stop = false;

    bool m_simulation = false;

    public CSerialTSP(SerialPort serialPort)
    {
        m_serialPort = serialPort;

        m_serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

        if (File.Exists(pathUtil.savePath() + "\\simulation"))
            m_simulation = true;

        m_thread = new Thread(run);
    }

    ~CSerialTSP()
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

    public void useSTX(bool value)
    {
        m_useSTX = value;
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

        try 
        {
            serialPort.Read(m_readBuffer, 0, n);

            lock (m_recvBuffer)
                m_recvBuffer.write(m_readBuffer, (uint)n);
        }
        catch (Exception ex)
        {
            serialPort.DiscardInBuffer();
            serialPort.DiscardOutBuffer();

            Debug.debug("CSerialTSP::dataReceived error reason:" + ex.Message);
        }
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
        Debug.debug("CSerialTSP::run START name:" + m_serialPort.PortName);

        while (true)
        {
            if (m_stop)
            {
                Debug.debug("CSerialTSP::run STOP");
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
                        "CSerialTSP::run port:" + m_serialPort.PortName);
            }

            Thread.Yield();
            Thread.Sleep(30);
        }

        Debug.debug("CSerialTSP::run END");
    }

    bool v = false;

    int ccnt = 0;

    public void sendOutput(int no, bool value)
    {
        byte[] data = new byte[13];

        data[0] = 0xFF;
        data[1] = 0xD0; // CMD

        int vv = (value == true) ? 0 : 1; //TSP 보드는 출력도 반대~ 입력도 반대~

        int cnt = 2;
        Util.setInt(ref data, cnt, no, true); cnt += 4;
        Util.setInt(ref data, cnt, vv, true); cnt += 4;

        byte pp = 0;
        byte xorPP = 0;

        for (int i = 0; i < cnt; i++)
        {
            pp += data[i];
            xorPP ^= data[i];
        }

        data[cnt++] = (byte)(pp); // and
        data[cnt++] = (byte)(xorPP); // xor
        data[cnt++] = 0xFE; // tail

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
                {
                    isStx = true;
                }
                else if (buff[i] == ETX)
                {
                    if (isStx == false && m_useSTX)
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

        //Debug.debug("CSerialTSP::processIncoming data:" + text);

        if (IncomingData != null)
            IncomingData(data, null);
    }
}
