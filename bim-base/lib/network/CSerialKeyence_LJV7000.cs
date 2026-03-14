using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.IO;

public class CSerialKeyence_LJ_V7000
{
    byte STX = 0x02;
    byte ETX = 0x03;
    byte CR = 0x0D;

    //static string ACQ_ALL = "MA";

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

    //int m_sendTick = 0;
    //long m_recvTick = 0;

    //int m_dataCount = 0;
    int m_agoTick = 0;

    CStopWatch m_stopWatch = new CStopWatch();

    public CSerialKeyence_LJ_V7000(SerialPort serialPort)
    {
        m_serialPort = serialPort;

        m_serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

        if (File.Exists(pathUtil.savePath() + "\\simulation"))
            m_simulation = true;

        m_thread = new Thread(run);
    }

    ~CSerialKeyence_LJ_V7000()
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

            Debug.debug("CSerial::dataReceived error reason:" + ex.Message);
        }

        m_isRecv = true;
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
        Debug.debug("CSerialKeyence_LJ_V7000::run START name:" + m_serialPort.PortName);

        while (true)
        {
            if (m_stop)
            {
                Debug.debug("CSerialKeyence_LJ_V7000::run STOP");
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
                    string garbageText = m_serialPort.ReadExisting();
                }
                catch (Exception)
                {
                    Thread.Sleep(1000);
                }
            }

            if (m_serialPort.IsOpen == true)
            {
                try
                {
                    sendProcess();
                    recvProcess();
                }
                catch (Exception ex)
                {
                    Debug.critical(ex,
                    "CSerialKeyence_LJV7000::run port:" + m_serialPort.PortName);
                }
            }

            Thread.Yield();
            Thread.Sleep(10);
        }

        Debug.debug("CSerialKeyence_LJ_V7000::run END");
    }

    public void sendTrigger()
    {
        sendString("TG");
    }

    public void sendBatchMeasureStart()
    {
        sendString("BS");
    }

    public void sendBatchMeasureStop()
    {
        sendString("BP");
    }

    public void sendAutoZero(bool onOff, int outNo)
    {
    }

    public void sendEnableMeasureValue()
    {
        //측정값 출력 (단일 OUT) MS,m,xx
        //측정값 출력 (다중 OUT) MM,m,xx ... xx
        //측정값 출력 (모든 OUT) MA,m

        // m 번호에 따른 출력 
        // 0 : 측정값만
        // 1 : 측정값 + 측정결과정보
        // 2 : 측정값 + 측정 결과
        // 3 : 측정값 + 측정 결과 정보 + 결정 결과
       // sendString("MM,3,1100100000000000"); //1,2,5번만 취득
        sendString("MM,3,1111100000000000"); //1,2,5번만 취득
       // sendString("MA,3");
    }

    public void sendString(string text)
    {
        byte[] data = new byte[text.Length + 1];
        byte[] textData = ASCIIEncoding.ASCII.GetBytes(text);

        for (int i = 0; i < textData.Length; i++)
        {
            data[i] = textData[i];
        }

        data[text.Length] = CR;

      //  Debug.debug("CSerial::sendString text:" + text);

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

            if (m_isRecv == false)
                return;

            //byte[] buff = m_sendQueue.Peek();
            byte[] buff = m_sendQueue.Dequeue();
            
            
            m_isRecv = false;
            m_stopWatch.Start();
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
			
			byte[] buff = new byte[m_recvBuffer.size()];
			m_recvBuffer.peek(ref buff, m_recvBuffer.size());

			for (int i = 0; i < buff.Length; i++)
			{
				if (buff[i] == CR)
				{
					byte[] data = new byte[i + 1];
					m_recvBuffer.read(ref data, (uint)i + 1);

					processIncmonig(data);

					return true;
				}
			}
            
        }

        return true;
    }

    private void processIncmonig(byte[] data)
    {
        string text = ASCIIEncoding.ASCII.GetString(data);

#if false
        for (int i = 0; i < data.Length; i++)
            text += data[i].ToString("X2") + " ";
#endif

        //Debug.debug("CSerial::processIncoming data:" + text);

        int agoTick = Environment.TickCount;

        if (m_agoTick == 0)
            m_agoTick = Environment.TickCount;

        string[] splitText = text.Split(',');

        int cnt = 0;
        string header = splitText[cnt++];

        //Debug.debug("CSerial_LJ_V7000::processIncmonig START ================");

#if false
        List<MeasureValue> list = new List<MeasureValue>();

        Debug.debug("CSerial_LJ_V7000::processIncoming recvTick:" + m_recvTick);

        if (header == "MM")//"MA")
        {
            for (int i = 0; i < (splitText.Length - 1)/3; i++)
            {
                string valueText = splitText[cnt++];
                string measResultText = splitText[cnt++];
                string judgeResultText = splitText[cnt++];

                MeasureValue measValue = new MeasureValue();
                bool ret = measValue.setData(m_recvTick, valueText, measResultText, judgeResultText);

                if (ret == false)
                {
                    Debug.warning("CSerial_LJ_V7000::processIncoming text:" + text);

                    for (int j = 0; j < splitText.Length / 3; j++)
                    {
                        Debug.warning("CSerial_LJ_V7000::processIncoming data index:" + j +
                            " value:" + splitText[j * 3 + 0] +
                            " result:" + splitText[j * 3 + 1] +
                            " judge:" + splitText[j * 3 + 2]);
                    }
                    return;
                }

                //Debug.debug("#" + (i+1) + " valueText:" + measValue.value + " meas:" + measValue.dataType + " judge:" + measValue.judge);

                list.Add(measValue);
            }
        }
        //1234
        //if()
        MeasureValue POS_IN = list[(int)MEAS_INDEX.POS_IN];
        MeasureValue POS_OUT = list[(int)MEAS_INDEX.POS_OUT];
      //  MeasureValue HEIGHT = list[(int)MEAS_INDEX.HEIGHT];

        //Debug.debug("CSerial::processIncoming POS_IN RESULT:" + POS_IN.dataType + " value:" + POS_IN.value);
        //Debug.debug("CSerial::processIncoming POS_OUT RESULT:" + POS_OUT.dataType + " value:" + POS_OUT.value);
        //Debug.debug("CSerial::processIncoming HEIGHT RESULT:" + HEIGHT.dataType + " value:" + HEIGHT.value);

        m_dataCount++;

        //Debug.debug("tick: " + (Environment.TickCount - agoTick));
#endif

        int tick = Environment.TickCount;

        if (tick - m_agoTick > 1000)
        {
            //Debug.warning("1 seconds ACQ count:" + m_dataCount);
            m_agoTick = tick;
        }

#if true
        if (IncomingData != null)
            IncomingData(data, null);
#endif
    }
}
