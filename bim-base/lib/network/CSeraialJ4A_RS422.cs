using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.IO;

public class CSerialJ4A_RS422
{
    // 통신 패러티 EVEN 사용

    static byte SOH = 0x01;
    static byte STX = 0x02;
    static byte ETX = 0x03;
    //static byte EOT = 0x04;

    static int READ_BUFF_SIZE = 1024;

    public event EventHandler IncomingData;

    SerialPort m_serialPort = null;

    CircularBuffer m_recvBuffer = new CircularBuffer(1024 * 1024); // 1MB
    Queue<byte[]> m_sendQueue = new Queue<byte[]>();

    byte[] m_readBuffer = new byte[READ_BUFF_SIZE];

    Thread m_thread = null;
    bool m_stop = false;

    bool m_simulation = false;

    bool m_recv = false;

    public CSerialJ4A_RS422(SerialPort serialPort, string portName)
    {
        m_serialPort = serialPort;
        m_serialPort.PortName = portName;

        m_serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

        if (File.Exists(pathUtil.savePath() + "\\simulation"))
            m_simulation = true;

        m_thread = new Thread(run);
    }

    ~CSerialJ4A_RS422()
    {
        m_stop = true;
    }

    public bool isOpen()
    {
        return m_serialPort.IsOpen;
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
        Debug.debug("CSerialJ4A_RS422::run START name:" + m_serialPort.PortName + " baudrate:" + m_serialPort.BaudRate + " parity:" + m_serialPort.Parity);

        while (true)
        {
            if (m_stop)
            {
                Debug.debug("CSerialJ4A_RS422::run STOP");
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

                    int n = m_serialPort.BytesToRead;
                    byte[] trashBuffer = new byte[n];
                    m_serialPort.Read(trashBuffer, 0, n);
                }
                catch (Exception ex)
                {
                    Debug.debug("CSerialJ4A_RS422::run connect exception reason:" + ex.Message);
                    Thread.Sleep(1000);
                }
            }

            sendProcess();
            recvProcess();

            //Thread.Yield();
            Thread.Sleep(1);
        }

        Debug.debug("CSerialJ4A_RS422::run END");
    }

    public void send(string text)
    {
        send(Encoding.ASCII.GetBytes(text));
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

            try
            {
                m_serialPort.Write(buff, 0, buff.Length);
            }
            catch (Exception /*e*/)
            {
            }
        }
    }
        
    private bool recvProcess()
    {
        lock (m_recvBuffer)
        {
            uint size = m_recvBuffer.size();

            if (size == 0)
                return false;

            byte[] tempData = new byte[m_recvBuffer.size()];
            m_recvBuffer.peek(ref tempData, (uint)m_recvBuffer.size());

            int stxIndex = -1;
            int etxIndex = -1;

            for (int i = 0; i < tempData.Length; i++)
            {
                if (tempData[i] == STX)
                {
                    stxIndex = i;
                    continue;
                }

                if (tempData[i] == ETX)
                {
                    etxIndex = i;
                    break;
                }
            }

            if (stxIndex < 0)
            {
                m_recvBuffer.read(ref tempData, (uint)m_recvBuffer.size()); // 오류 데이터 버림

                string errorText = ASCIIEncoding.ASCII.GetString(tempData);
                Debug.debug("CSerialJ4A::recvAllErrorData recv text:" + errorText);

                return false;
            }

            byte[] data = new byte[etxIndex + 1];
            m_recvBuffer.read(ref data, (uint)data.Length); // 데이터 취득

            processIncmonig(data);
        }

        return true;
    }

    private void processIncmonig(byte[] data)
    {
        string text = "";

        for (int i = 0; i < data.Length; i++)
            text += data[i].ToString("X2") + " ";

        Debug.debug("CSerialJ4A_RS422::processIncoming data:" + text);

        m_recv = true;

        if (IncomingData != null)
            IncomingData(data, null);
    }

    public void sendStatusRequest(int index)
    {
        byte[] data = new byte[10];
        byte station = (Convert.ToByte(index + 48));

        // 파라미터 그룹 설정
        data[0] = SOH;
        data[1] = station; // station no

        data[2] = 0x30; // "0"
        data[3] = 0x31; // "1"
        data[4] = STX;

        data[5] = 0x38; // "0"
        data[6] = 0x38; // "8"

        data[7] = ETX;

        byte[] checksum = makeChecksum(data, 1, 7);

        data[8] = checksum[0];
        data[9] = checksum[1];

        string packet = ASCIIEncoding.ASCII.GetString(data);

        send(data);
    }
    private byte[] makeChecksum(byte[] data, int startPos, int endPos)
    {
        byte sum = 0;

        for (int i = startPos; i <= endPos; i++)
            sum += data[i];

        string hexString = sum.ToString("X2");
        return ASCIIEncoding.ASCII.GetBytes(hexString);
    }

    public bool recv()
    {
        return m_recv;
    }
}
