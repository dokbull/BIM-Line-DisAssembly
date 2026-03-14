using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading.Tasks;
using static CModbus;

public class CSerial_SI480E
{
    enum SIGN_BYTE
    {
        PLUS = 0x2B,
        MINUS = 0x2D,
    }

    Object m_lockObj = new Object();

    static byte CR = 0x0D;
    static byte LF = 0x0A;

    static int READ_BUFF_SIZE = 1024;

    SerialPort m_serialPort = null;

    CircularBuffer m_recvBuffer = new CircularBuffer(1024 * 1024); // 1MB
    Queue<byte[]> m_sendQueue = new Queue<byte[]>();

    Thread m_thread = null;
    bool m_stop = false;

    bool m_simulation = false;

    double m_value = 0.0d;
    bool m_stable = false;

    CElaspedTimer m_reciveCheckTimer = new CElaspedTimer(3000);

    public CSerial_SI480E(SerialPort serialPort)
    {
        m_serialPort = serialPort;
        m_serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

        if (File.Exists(pathUtil.myDocumnent() + "\\simulation"))
            m_simulation = true;

        m_reciveCheckTimer.start();

        m_thread = new Thread(run);
    }

    ~CSerial_SI480E()
    {
        m_stop = true;
    }

    public bool isConnected()
    {
        return m_serialPort.IsOpen;
    }

    public bool isLive()
    {
        if (m_simulation)
            return true;
        if (m_serialPort == null)
            return false;
        if (!m_serialPort.IsOpen)
            return false;
        if (m_reciveCheckTimer.isElasped() == true)
            return false;
        return true;
    }

    private void dataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        SerialPort serialPort = (SerialPort)sender;

        int n = serialPort.BytesToRead;

        byte[] readBuff = new byte[n];

        serialPort.Read(readBuff, 0, n);
        
        lock (m_recvBuffer)
            m_recvBuffer.write(readBuff, (uint)n);
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
        Debug.debug("CSerial_SI480E::run START COM:" + m_serialPort.PortName);

        while (true)
        {
            if (m_stop)
            {
                Debug.debug("CSerial_SI480E::run STOP");
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
                    m_serialPort.DiscardInBuffer();
                    m_serialPort.DiscardOutBuffer();
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
                    "CSerial_SI480E::run port:" + m_serialPort.PortName);
            }

            Thread.Yield();
            //Thread.Sleep(10);
            Task.Delay(10);
        }

        Debug.debug("CSerial_SI480E::run END");
    }

    public void send(byte[] data)
    {
        int count = data.Length;
        byte[] sendData = new byte[count];

        for (int i = 0; i < count; i++)
        {
            sendData[i] = data[i];
        }

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
            uint len = m_recvBuffer.size();
            byte[] buff = new byte[len];
            m_recvBuffer.peek(ref buff, len);

            for (int i = 0; i < buff.Length - 1; i++)
            {
                // CR LF를 사용하는 통신 (포맷1, 2)
                if (buff[i] == CR && buff[i + 1] == LF) 
                {
                    int cnt = i + 2;

                    byte[] readData = new byte[cnt];
                    m_recvBuffer.read(ref readData, (uint)cnt);

                    if (i > 0)
                    {
                        byte[] data = new byte[cnt];

                        Util.memcpy(ref data, 0, readData, 0, cnt);
                        processIncmonig(data);
                    }

                    return true;
                }
            }
        }

        return true;
    }

    //CStopWatch m_watch = new CStopWatch();

    private void processIncmonig(byte[] data)
    {
        if (data.Length != 18)
            return;

        m_reciveCheckTimer.start();
        //long v = m_watch.GetElapsedTime(CStopWatch.TIME_UNIT.MILLISECOND, true);

        //Debug.debug("######## V:" + v);

        string text = ASCIIEncoding.ASCII.GetString(data);

        string[] split = text.Split(',');
        string header1 = split[0];
        string hedaer2 = split[1];
        string mark = split[2].Substring(0, 1);
        string strValue = split[2].Substring(1, 7);
        string unit = split[2].Substring(8, 2);

        if (header1 == "ST")
            m_stable = true;
        else
            m_stable = false;

        double getValue = Util.toDouble(strValue);
        
        if (mark == "-")
            getValue = -1 * getValue;

        lock (m_lockObj)
        {
            m_value = getValue * 1.0f;
        }
    }

    public bool isStable()
    {
        return m_stable;
    }
    public void clearStable()
    {
        m_stable = false;
    }

    public double getValue()
    {
        double ret = 0.0d;

        lock (m_lockObj)
        {
            ret = m_value;
        }

        return ret;
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
