using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.IO;
using static CSerialAFC3000.Result;

public class CSerialAFC3000
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

    public CSerialAFC3000(SerialPort serialPort)
    {
        m_serialPort = serialPort;

        m_serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

        if (File.Exists(pathUtil.savePath() + "\\simulation"))
            m_simulation = true;

        m_thread = new Thread(run);
        m_thread.Start();
    }

    ~CSerialAFC3000()
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

        if (n > 1024)
            n = 1024;

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
            ABNORMAL, // ABNORMAL
            STOP, // STOP
            RESET, // RESET STOP
            BYPASS, // BYPASS
            START_OFF, // START OFF
        }

        public enum TorqueResult
        {
            CLEAR,
            LIMIT_L, // 하한 규격 외
            LIMIT_H, // 상한 규격 외
        }

        public Judge judge;

        public double peakLowLimit;
        public double peakHighLimit;
        public double peakTorque;
        public TorqueResult peakTorqueResult;

        public bool result;

        public static void copy(CSerialAFC3000.Result dst, CSerialAFC3000.Result src)
        {
            dst.judge = src.judge;

            dst.peakHighLimit = src.peakHighLimit;
            dst.peakLowLimit = src.peakLowLimit;
            dst.peakTorque = src.peakTorque;
            dst.peakTorqueResult = src.peakTorqueResult;

            dst.result = src.result;
        }
    }

    private void processIncmonig(byte[] data)
    {
        string debugText = "";

        for (int i = 0; i < data.Length; i++)
            debugText += data[i].ToString("X2") + " ";

        Debug.debug("CSerialAFC::processIncoming data:" + debugText);

        //REJECT packet
        //string text = "REJ    4.0  10.0   1.2L "
        string text = ASCIIEncoding.ASCII.GetString(data);

        int cnt = 0;

        try
        {
            string judgeText = text.Substring(0, 4); cnt += 4;
            string lowTorqueText = text.Substring(cnt, 6); cnt += 6;
            string highTorqueText = text.Substring(cnt, 6); cnt += 6;
            string peakTouqueText = text.Substring(cnt, 6); cnt += 6;

            Result result = new Result();

            result.judge = getJudge(judgeText.Trim());

            result.peakLowLimit = Convert.ToDouble(lowTorqueText.Trim());
            result.peakHighLimit = Convert.ToDouble(highTorqueText.Trim());

            peakTouqueText = peakTouqueText.Replace(" ", "");
            result.peakTorque = Convert.ToDouble(peakTouqueText.Trim());

            if (result.judge == Judge.ACCEPT && result.peakTorqueResult == TorqueResult.CLEAR)
                result.result = true;
            else
                result.result = false;

            if (IncomingData != null)
                IncomingData(result, null);
        }
        catch (Exception e)
        {
            Debug.warning("CSerialAFC3000:processIncmonig data currupted. message:" + e.Message);
        }
    }

    private Result.Judge getJudge(string text)
    {
        if (text == "REJ") return Result.Judge.REJECT;
        else if (text == "ACC") return Result.Judge.ACCEPT;
        else if (text == "ABN") return Result.Judge.ABNORMAL;
        else if (text == "STOP") return Result.Judge.STOP;
        else if (text == "RST") return Result.Judge.RESET;
        else if (text == "BYP") return Result.Judge.BYPASS;
        else if (text == "SOFF") return Result.Judge.START_OFF;

        return Result.Judge.NONE;
    }

    private Result.TorqueResult getResult(string text)
    {
        if (text == "H") return Result.TorqueResult.LIMIT_H;
        else if (text == "L") return Result.TorqueResult.LIMIT_L;

        return Result.TorqueResult.CLEAR;
    }
}
