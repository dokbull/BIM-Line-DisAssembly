using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.IO;
using System.Net.Sockets;
using System.Xml.Linq;
using lib.plc;

public class CSerialModbus
{
    public enum FUNC
    {
        //TODO@tmdwn..나머지 부분 작성 할 것

        READ_INPUT = 0x04,
        READ_HOLDING = 0x03,
        WRITE_SINGLE = 0x06,
        WRITE_MULTI = 0x10
    }

    public enum DATA_FUNC
    {
        PURSE_LEVEL = 0x00, //1:purse, 0:low
        MODE = 0x01,        //1:automatic, 2:manual, 3:steps
        LINK_STATUS = 0x02, //0:independence, 1:linkage
        LED_TEMP = 0x03,    //0~200 dgree
        LED_STATUS = 0x04,  //0:LED Off, 1: LED on
        ALARM_STATUS = 0x05,    //0:normal, 1:LED failure, 2:over temp, 6:temp critial, 7:over set life
        CURRENT_INTENSITY = 0x07,
        MANUAL_INTENSITY = 0x08,    //0~100
        AUTO_INTENSITY = 0x09,      //0~100
        ST1_INTENSITY = 0x0A,
        ST2_INTENSITY = 0x0B,
        ST3_INTENSITY = 0x0C,
        ST4_INTENSITY = 0x0D,
        ST5_INTENSITY = 0x0E,
        ST6_INTENSITY = 0x0F,
        ST7_INTENSITY = 0x10,
        LIFE_ALARM_VALUE = 0x11,
        AUTO_TIME = 0x12,
        ST1_TIME = 0x13,
        ST2_TIME = 0x14,
        ST3_TIME = 0x15,
        ST4_TIME = 0x16,
        ST5_TIME = 0x17,
        ST6_TIME = 0x18,
        ST7_TIME = 0x19,
        STEP_NUMVER = 0x1A,
        CYCLE_NUMBER = 0x1B,
        LIGHT_SWITCH = 0x2A,    //0:Off, 1:On
        LIGHT_ILLUMINANCE = 0x31,
    }

    static int READ_BUFF_SIZE = 1024;

    public event EventHandler IncomingData;
    public event EventHandler recvData;

    SerialPort m_serialPort = null;

    CircularBuffer m_recvBuffer = new CircularBuffer(1024 * 1024); // 1MB
    Queue<byte[]> m_sendQueue = new Queue<byte[]>();

    byte[] m_readBuffer = new byte[READ_BUFF_SIZE];

    Thread m_thread = null;
    bool m_stop = false;

    bool m_simulation = false;

    int m_lastSendReadAddr = 0x0;
    int m_lastSendReadCount = 0;

    bool m_isRecv = true;

    public CSerialModbus(SerialPort serialPort)
    {
        m_serialPort = serialPort;

        m_serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

        if (File.Exists(pathUtil.savePath() + "\\simulation"))
            m_simulation = true;

        m_thread = new Thread(run);
        this.IncomingData += CSerialModbus_IncomingData;
    }

    private void CSerialModbus_IncomingData(object sender, EventArgs e)
    {
        byte[] packet = (byte[])sender;
        int function = packet[1];

#if false
        int dataCount = packet[2] / 2;
        int[] data = new int[dataCount]; // byte -> int

        int cnt = 2;

        for (int i = 0; i < dataCount; i++)
        {
            data[i] = (packet[cnt] >> 8) + packet[cnt + 1];
            cnt += 2;
        }
#else

        int dataCount = packet.Length - 2 - 2;  //slave id, FUNC, CRC 제외
        if (dataCount <= 0)
            return;

        int channel = (m_lastSendReadAddr >> 8) & 0xFF;
        int len = dataCount + 1;
        byte[] data = new byte[len];

        data[0] = (byte)channel;

        for (int i = 0; i < dataCount; i++)
        {
            data[i+1] = packet[i + 2];
        }

#endif
        if (recvData != null)
        {
            recvData(data, null);
        }
    }

    ~CSerialModbus()
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

            Debug.debug("CSerialModbus::dataReceived error reason:" + ex.Message);
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
        Debug.debug("CSerialModbus::run START name:" + m_serialPort.PortName);

        while (true)
        {
            if (m_stop)
            {
                Debug.debug("CSerialModbus::run STOP");
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
                        "CSerialModbus::run port:" + m_serialPort.PortName);
            }

            Thread.Yield();
            Thread.Sleep(10);
        }

        Debug.debug("CSerialModbus::run END");
    }

    public void send(byte[] data)
    {
        lock (m_sendQueue)
        {
            m_sendQueue.Enqueue(data);
        }
    }

    private void sendProcess()
    {
        if (m_simulation)
            return;

        lock (m_sendQueue)
        {
            if (m_sendQueue.Count == 0)
                return;

            byte[] buff = m_sendQueue.Dequeue();
            m_serialPort.Write(buff, 0, buff.Length);

            //if (buff[0] == 0x23)
            //{
            //    string hexText = "";
            //    for (int i = 0; i < buff.Length; i++)
            //        hexText += buff[i].ToString("X2") + " ";

            //    Debug.debug("CSerialModbus::sendProcess write data : " + hexText);
            //}
        }
    }

    private bool recvProcess()
    {
        lock (m_recvBuffer)
        {
            uint size = m_recvBuffer.size();

            if (size == 0)
                return false;

            byte[] data = new byte[size];
            m_recvBuffer.read(ref data, size);

            if (data.Length < 9)
                return false;

            //if (data[0] == 0x23)
            //{
            //    string hexText = "";
            //    for (int i = 0; i < data.Length; i++)
            //        hexText += data[i].ToString("X2") + " ";
            //    Debug.debug("CSerialModbus::recvProcess read data : " + hexText);
            //}

            m_isRecv = true;
            processIncmonig(data);

        }

        return true;
    }

    private void processIncmonig(byte[] data)
    {
        string text = "";

        for (int i = 0; i < data.Length; i++)
            text += data[i].ToString("X2") + " ";

        if (IncomingData != null)
            IncomingData(data, null);
    }

    public bool isRecv() { return m_isRecv; }

    public void sendReadPacket(int slaveId, int addr, int count)
    {
        m_lastSendReadAddr = addr;
        m_lastSendReadCount = count;

        byte[] data = new byte[8];

        data[0] = (byte)slaveId; // slave ID
        data[1] = (byte)FUNC.READ_INPUT;
        data[2] = (byte)((addr >> 8) & 0xFF);
        data[3] = (byte)(addr & 0xFF);
        data[4] = 0x00;// (byte)((count >> 8) & 0xFF);
        data[5] = 0x02;// (byte)(count & 0xFF);

        byte[] checksum = ModbusCRC.makeCRC16(data, 6);
        data[6] = checksum[0];
        data[7] = checksum[1];

        m_isRecv = false;
        send(data);
    }

    public bool sendWriteOnOff(int slaveId, int addr, int onOff)
    {
        if (m_isRecv == false)
            return false;

        m_lastSendReadAddr = addr;

        byte[] data = new byte[11];

        data[0] = (byte)slaveId; // slave ID
        data[1] = (byte)FUNC.WRITE_MULTI;
        data[2] = (byte)((addr >> 8) & 0xFF);
        data[3] = (byte)DATA_FUNC.LIGHT_SWITCH;
        data[4] = 0x00;
        data[5] = 0x01;
        data[6] = 0x02;
        data[7] = (byte)((onOff >> 8) & 0xFF);
        data[8] = (byte)(onOff & 0xFF);

        byte[] checksum = ModbusCRC.makeCRC16(data, data.Length - 2);
        data[9] = checksum[0];
        data[10] = checksum[1];

        m_isRecv = false;
        send(data);
        return true;
    }

    public bool sendReadTemp(int slaveId, int addr, int count = 1)
    {
        if (m_isRecv == false)
            return false;

        m_lastSendReadAddr = addr;

        byte[] data = new byte[8];

        data[0] = (byte)slaveId; // slave ID
        data[1] = (byte)FUNC.READ_HOLDING;
        data[2] = (byte)((addr >> 8) & 0xFF);
        data[3] = (byte)(addr & 0xFF);
        data[4] = (byte)((count >> 8) & 0xFF);
        data[5] = (byte)(count & 0xFF);
        byte[] checksum = ModbusCRC.makeCRC16(data, data.Length - 2);
        data[6] = checksum[0];
        data[7] = checksum[1];

        m_isRecv = false;
        send(data);
        return true;
    }

    public void sendReadOnOff(int slaveId, int addr)
    {
        m_lastSendReadAddr = addr;

        byte[] data = new byte[8];

        data[0] = (byte)slaveId; // slave ID
        data[1] = (byte)FUNC.READ_HOLDING;
        data[2] = (byte)((addr >> 8) & 0xFF);
        data[3] = (byte)(addr & 0xFF);
        data[4] = 0x00;
        data[5] = 0x01;
        byte[] checksum = ModbusCRC.makeCRC16(data, data.Length - 2);
        data[6] = checksum[0];
        data[7] = checksum[1];

        m_isRecv = false;
        send(data);
    }

    public void sendReadAlarm(int slaveId, int addr)
    {
        byte[] data = new byte[8];

        data[0] = (byte)slaveId; // slave ID
        data[1] = (byte)FUNC.READ_HOLDING;
        data[2] = (byte)((addr >> 8) & 0xFF);
        data[3] = 0x05;
        data[4] = 0x00;
        data[5] = 0x01;
        byte[] checksum = ModbusCRC.makeCRC16(data, data.Length - 2);
        data[6] = checksum[0];
        data[7] = checksum[1];

        m_isRecv = false;
        send(data);
    }
}
