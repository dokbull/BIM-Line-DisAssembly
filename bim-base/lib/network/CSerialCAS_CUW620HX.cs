using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.IO;

public class CSerialCAS_CUW620HX
{
    byte STX = 0x02;
    byte ETX = 0x03;
    byte CR = 0x0D;
    //byte LF = 0x0A; //unuse

    static int READ_BUFF_SIZE = 1024;

    public event EventHandler IncomingData;

    SerialPort m_serialPort = null;

    CircularBuffer m_recvBuffer = new CircularBuffer(1024 * 1024); // 1MB
    Queue<byte[]> m_sendQueue = new Queue<byte[]>();

    byte[] m_readBuffer = new byte[READ_BUFF_SIZE];

    Thread m_thread = null;
    bool m_stop = false;

    bool m_simulation = false;

    public CSerialCAS_CUW620HX(SerialPort serialPort)
    {
        m_serialPort = serialPort;

        m_serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

        if (File.Exists(pathUtil.savePath() + "\\simulation"))
            m_simulation = true;

        m_thread = new Thread(run);
    }

    ~CSerialCAS_CUW620HX()
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

        if (n > 1024) // 배열 이상의 데이터가 수신되는 경우
            n = 1024;

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
            if (serialPort != null || serialPort.IsOpen) 
            {
                serialPort.DiscardInBuffer();
                serialPort.DiscardOutBuffer();
            }

            Debug.debug("CSerial_CAS_CUW620HX::dataReceived error reason:" + ex.Message);
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
        Debug.debug("CSerial_CAS_CUW620HX::run START name:" + m_serialPort.PortName);

        while (true)
        {
            if (m_stop)
            {
                Debug.debug("CSerial_CAS_CUW620HX::run STOP");
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

            try
            {
                sendProcess();
                recvProcess();
            }
            catch (Exception ex)
            {
                Debug.critical(ex,
                    "CSerialCAS_CUW620HX::run port:" + m_serialPort.PortName);
            }

            Thread.Yield();
            Thread.Sleep(1);
        }

        Debug.debug("CSerial_CAS_CUW620HX::run END");
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

        Debug.debug("CSerial_CAS_CUW620HX::sendString text:" + text);

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
			
			byte[] buff = new byte[m_recvBuffer.size()];
			m_recvBuffer.peek(ref buff, m_recvBuffer.size());

#if false
            m_recvBuffer.read(ref buff, size);
            processIncmonig(buff);
#endif

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

        //Debug.debug("CSerial_CAS_CUW620HX::processIncoming data:" + text);

        string text2 = "";

        for (int i = 0; i < text.Length; i++)
        {
            string charText = "";

            charText = text.Substring(i, 1);

            if (charText != " ")
                text2 += charText;
        }

        double weight = 0.0f;

        bool ret = false;

        if (text2.Length > 2)
        {
            string parseText = text2.Substring(0, text2.Length - 2);
            ret = Double.TryParse(parseText, out weight);
        }

        if (ret == false)
            return;

        if (IncomingData != null)
            IncomingData(weight, null);
    }
}
