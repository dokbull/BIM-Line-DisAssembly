using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.IO;
using static CSerialAFC_SehanHdc40i.Result;

public class CSerialAFC_SehanHdc40i
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

    bool m_isConnected = false;
    bool m_simulation = false;

    public CSerialAFC_SehanHdc40i(SerialPort serialPort)
    {
        m_serialPort = serialPort;

        m_serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

        if (File.Exists(pathUtil.savePath() + "\\simulation"))
            m_simulation = true;

        m_thread = new Thread(run);
        m_thread.Start();
    }

    ~CSerialAFC_SehanHdc40i()
    {
        m_stop = true;
    }

    public bool isConnected()
    {
        return m_serialPort.IsOpen;
    }

    private void dataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        //Debug.debug("CSerialAFC::dataReceived");
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

    public void stop()
    {
        m_stop = true;
    }

    private void run()
    {
        Debug.debug("CSerialAFC::run START");

        while (true)
        {
            if (m_stop)
            {
                Debug.debug("CSerialAFC::run STOP");
                break;
            }

            if (m_simulation)
            {
                try
                {
                    sendProcess();
                    recvProcess();
                }
                catch (Exception ex)
                {
                    Debug.critical(ex,
                        "CSerialAFC::run port:" + m_serialPort.PortName);
                }

                Thread.Sleep(50);

                continue;
            }

            if (!m_serialPort.IsOpen)
            {
                try
                {
                    m_serialPort.Close();

                    Thread.Sleep(500);

                    m_serialPort.Open();
                }
                catch (Exception e)
                {
                    Debug.debug("CSerialAFC::run error:" + e.Message);
                    Thread.Sleep(1000);
                }
            }

            sendProcess();
            recvProcess();

            Thread.Yield();
            Thread.Sleep(50);
        }

        m_serialPort.Close();

        Debug.debug("CSerialAFC::run END");
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
            if (!m_isConnected)
                return;

            if (m_sendQueue.Count == 0)
                return;

            byte[] buff = m_sendQueue.Peek();
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

        return true;
    }

    public class Result
    { 
        public enum Judge
        {
            NONE,
            REJECT, // REJECT
            ACCEPT, // ACCEPT
        }

        public int workTime;
        public int preset;
        public double setTorque;
        public double peakTorque;
        public int rpm;
        public double spinCount;
        public double temperature;
        public string errorCode;
        public int screwCount;
        public double current;
        public int dir;
        public Judge result;
    }

    private void processIncmonig(byte[] data)
    {
        string debugText = "";

        for (int i = 0; i < data.Length; i++)
            debugText += data[i].ToString("X2") + " ";

        Debug.debug("CSerialAFC::processIncoming data:" + debugText);

        // M 160107001,01350,1,085,084,1700,033,0358,000,04,032,1,1,9
        string text = ASCIIEncoding.ASCII.GetString(data);

        string[] splitText = text.Split(',');

        if (splitText.Length < 12)
            return;

        if (splitText[0].Substring(0, 1) != "m") // 체결 결과 판독
            return;

        try
        {
            Result result = new Result();

            int index = 1;

            result.workTime = Util.toInt32(splitText[index++]);
            result.preset = Util.toInt32(splitText[index++]);
            result.setTorque = (Util.toDouble(splitText[index++]) / 10.0d);
            result.peakTorque = (Util.toDouble(splitText[index++]) / 10.0d);
            result.rpm = Util.toInt32(splitText[index++]);
            result.spinCount = (Util.toDouble(splitText[index++]) / 10.0d);
            result.temperature = (Util.toDouble(splitText[index++]) / 10.0d);
            result.errorCode = splitText[index++];
            result.screwCount = Util.toInt32(splitText[index++]);
            result.current = (Util.toDouble(splitText[index++]) / 10.0d);
            result.dir = Util.toInt32(splitText[index++]);
            result.result = getJudge(splitText[index++]);

            if (IncomingData != null)
                IncomingData(result, null);
        }
        catch (Exception e)
        {
            Debug.warning("CSerialAFC_SehanHdc40i:processIncmonig data currepted message" + e.Message);
        }
    }

    private Result.Judge getJudge(string text)
    {
        if (text == "0") return Result.Judge.REJECT;
        else if (text == "1") return Result.Judge.ACCEPT;

        return Result.Judge.NONE;
    }
}
