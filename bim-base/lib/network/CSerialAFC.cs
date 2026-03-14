using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.IO;

public class CSerialAFC
{
    static byte ETX1 = 0x0D; // CR
    static byte ETX2 = 0x0A; // LF

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

    public CSerialAFC(SerialPort serialPort)
    {
        m_serialPort = serialPort;

        m_serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

        if (File.Exists(pathUtil.savePath() + "\\simulation"))
            m_simulation = true;

        m_thread = new Thread(run);
        m_thread.Start();
    }

    ~CSerialAFC()
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
            byte[] buff = new byte[m_recvBuffer.size()];
            m_recvBuffer.peek(ref buff, m_recvBuffer.size());

            for (int i = 0; i < buff.Length; i++)
            {
                if (i < 1)
                    continue;

                if (buff[i - 1] == ETX1 && buff[i] == ETX2)
                {
                    byte[] data = new byte[i + 1];
                    m_recvBuffer.read(ref data, (uint)data.Length);
                    processIncmonig(data);
                }
            }
        }

        return true;
    }

    public class Result
    {
        public enum TouqueResult
        {
            NONE,
            LIMIT_H, // 상한 규격 외
            LIMIT_L, // 하한 규격 외
            ABNORMAL, // ABNORMAL
            STOP, // STOP
            BYPASS // BYPASS (축 분리)
        }

        public enum EnumResult
        {
            NONE,
            LIMIT_H, // 상한 규격 외
            LIMIT_L // 하한 규격 외
        }

        public int count;
        public int axisNo;
        public int parameterNo;

        public double peakTouque;
        public TouqueResult peakTouqueResult;

        public int lastAngle;
        public EnumResult lastAngleResult;

        public double lastTorque;
        public EnumResult lastTorqueResult;

        public double[] rate = new double[3];
        public EnumResult[] rateResult = new EnumResult[3];

        public double[] time = new double[2];
        public EnumResult[] timeResult = new EnumResult[2];

        public bool result;

        public static void copy(CSerialAFC.Result dst, CSerialAFC.Result src)
        {
            dst.count = src.count;
            dst.axisNo = src.axisNo;
            dst.parameterNo = src.parameterNo;

            dst.peakTouque = src.peakTouque;
            dst.peakTouqueResult = src.peakTouqueResult;

            dst.lastAngle = src.lastAngle;
            dst.lastAngleResult = src.lastAngleResult;

            dst.lastTorque = src.lastTorque;
            dst.lastTorqueResult = src.lastTorqueResult;

            for (int i=0; i<3; i++)
            {
                dst.rate[i] = src.rate[i];
                dst.rateResult[i] = src.rateResult[i];
            }

            for (int i=0; i<2; i++)
            {
                dst.time[i] = src.time[i];
                dst.timeResult[i] = src.timeResult[i];
            }
        }
    }

    private void processIncmonig(byte[] data)
    {
        string debugText = "";

        for (int i = 0; i < data.Length; i++)
            debugText += data[i].ToString("X2") + " ";

        Debug.debug("CSerialAFC::processIncoming data:" + debugText);

        //REJECT packet
        //string text = "3566  01    1   0.09L    0L  0.00L     0      0      0    8.1H   0.0    X   \r";
        string text = ASCIIEncoding.ASCII.GetString(data);

        int cnt = 0;

        string countText = text.Substring(0, 4); cnt += 4;
        cnt += 2; // blank
        string axisText = text.Substring(cnt, 2); cnt += 2;
        cnt += 3; // blank
        string parameterText = text.Substring(cnt, 2); cnt += 2;
        cnt += 2; // blank

        string peakTouqueText = text.Substring(cnt, 5); cnt += 5;
        string peakTouqueResultText = text.Substring(cnt, 1); cnt += 1;
        cnt += 1; // blank

        string lastAngleText = text.Substring(cnt, 4); cnt += 4;
        string lastAngleResultText = text.Substring(cnt, 1); cnt += 1;
        cnt += 1; // blank

        string lastTorqueText = text.Substring(cnt, 5); cnt += 5;
        string lastTorqueResultText = text.Substring(cnt, 1); cnt += 1;
        cnt += 1; // blank

        string[] rateText = new string[3];
        string[] rateResultText = new string[3];

        for (int i = 0; i < 3; i++)
        {
            rateText[i] = text.Substring(cnt, 5); cnt += 5;
            rateResultText[i] = text.Substring(cnt, 1); cnt += 1;
            cnt += 1; // blank
        }

        string[] timeText = new string[2];
        string[] timeResultText = new string[2];

        for (int i = 0; i < 2; i++)
        {
            timeText[i] = text.Substring(cnt, 5); cnt += 5;
            timeResultText[i] = text.Substring(61, 1); cnt += 1;
            cnt += 1;
        }

        string resultText = text.Substring(cnt, 1);

        string subText;

        subText = debugText.Substring(0, 4);

        Result result = new Result();

        result.count = Convert.ToInt32(countText.Trim());
        result.axisNo = Convert.ToInt32(axisText.Trim());
        result.parameterNo = Convert.ToInt32(parameterText.Trim());

        result.peakTouque = Convert.ToDouble(peakTouqueText.Trim());
        result.peakTouqueResult = getTouqueResult(peakTouqueResultText);

        result.lastAngle = Convert.ToInt32(lastAngleText.Trim());
        result.lastAngleResult = getResult(lastAngleResultText);

        result.lastTorque = Convert.ToDouble(lastTorqueText.Trim());
        result.lastTorqueResult = getResult(lastTorqueResultText);

        for (int i = 0; i < 3; i++)
        {
            result.rate[i] = Convert.ToDouble(rateText[i].Trim());
            result.rateResult[i] = getResult(rateResultText[i].Trim());
        }

        for (int i = 0; i < 2; i++)
        {
            result.time[i] = Convert.ToDouble(timeText[i].Trim());
            result.timeResult[i] = getResult(timeResultText[i].Trim());
        }

        if (data[72] == 0x4F)
            result.result = true;
        else
            result.result = false;

        if (IncomingData != null)
            IncomingData(result, null);
    }

    private Result.TouqueResult getTouqueResult(string text)
    {
        if (text == "H") return Result.TouqueResult.LIMIT_H;
        else if (text == "L") return Result.TouqueResult.LIMIT_L;
        else if (text == "A") return Result.TouqueResult.ABNORMAL;
        else if (text == "S") return Result.TouqueResult.STOP;
        else if (text == "B") return Result.TouqueResult.BYPASS;

        return Result.TouqueResult.NONE;
    }

    private Result.EnumResult getResult(string text)
    {
        if (text == "H") return Result.EnumResult.LIMIT_H;
        else if (text == "L") return Result.EnumResult.LIMIT_L;

        return Result.EnumResult.NONE;
    }
}
