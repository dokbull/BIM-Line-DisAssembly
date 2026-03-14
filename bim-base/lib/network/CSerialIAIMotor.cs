using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.IO;

public class CSerialIAIMotor
{
    byte STX = 0X3A;
    byte ETX = 0X0A;

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

    int m_axisNo = 0;

    int m_lastSendAddress = 0x0;

    int m_lastRecvTick = 0;

    // 정보
    long m_pos;
    long cmd;

    long m_speed;

    Information m_info = new Information();

    int m_pos_tick = Environment.TickCount;

    bool m_pause = false;

    // 설명 페이지 p.131 부터 ASCII 모드
    // PIO 기능 무효화 ?


#region DOCUMENT_DESCRIPTION

    // Alarm Reset - p.181
    // PIO / Modbus 전환 - p. 203

#endregion


    public enum FC // FunctionCode
    {
        readCoilStatus_01 = 0x01, // Coil, DO 읽기
        readInputStatus_02 = 0x02, // 입력 Status, DI 읽기
        readHoldingRegisters_03 = 0x03, // 보유 Register 읽기
        readInputRegisters_04 = 0x04, // 입력 Register 읽기
        forceSingleCoil_05 = 0x05, // Coil, DO 1점 기입
        presetSingleRegister_06 = 0x06, // 보유 Register로 기입
        readExceptionStatus_07 = 0x07, // 예외 Register 읽기
        forceMultipleCoils_0F = 0x0F, // 복수 Coil, DO로 일괄 기입
        presetMultipleRegisters_10 = 0x10, // 복수 보유 Register 로 일괄 기입
        reportSlaveID_11 = 0x11, // slave ID 조회
        readWriterRegisters_17 = 0x17 // Register 로 읽기, 쓰기
    }

    public class DeviceStatus1 // p.22 주소 : 9005
    {
        public bool emergency; // EMGS : 15bit
        public bool safety; // SFTY : 14bit
        public bool ready; // PWR : 13bit
        public bool servoOn; // SV : 12 bit
        public bool pushResonance; // PSFL : 11 bit
        public bool error; // ALMH : 10 bit
        public bool alarm; // ALML : 9 bit
        public bool absError; // ABER : 8 bit
        public bool isBreak; // BKRL : 7 bit
        // 6 - 사용안함
        public bool pause; // STP : 5 bit
        public bool home; // HEND : 4 bit
        public bool inpos; // PEND : 3 bit
        // 2 ~ 0 사용 안함
    }

    public class DeviceStatus2
    {
    }

    public class ExtDeviceStatus
    {
    }

    // 0x9000 ~ 0x9001 : 현재 위치
    // 0x9002 : 발생 알람 코드
    // 0x9003 : I/O 입력 상태 조회
    // 0x9004 : I/O 출력 상태 조회
    // 0x9005 : Device Status Register 1 
    // 0x9006 : Device Status Register 2
    // 0x9007 : Ext Device Status Register 
    // 0x9008 : System Status Register 
    // 0x900A ~ 0x900B : 현재 속도 조회
    // 0x900C ~ 0x900D : 전류치 조회
    // 0x900E ~ 0x900F : 편차 조회
    public class Information : DeviceStatus1
    {
        public long pos;
        public int alarmCode;

        public bool[] input = new bool[16];
        public bool[] output = new bool[16];

        // DSS1
        // DSS2
        // EDSS

        public long speed;
        public long ampare; // VNOW
        public long deviation; // DEVI
    }

    public CSerialIAIMotor(SerialPort serialPort)
    {
        m_serialPort = serialPort;

        m_serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

        if (File.Exists(pathUtil.savePath() + "\\simulation"))
            m_simulation = true;

        m_thread = new Thread(run);
    }

    ~CSerialIAIMotor()
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
        Debug.debug("CSerialIAIMotor::run START name:" + m_serialPort.PortName);

        while (true)
        {
            if (m_stop)
            {
                Debug.debug("CSerialIAIMotor::run STOP");
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

            if (Environment.TickCount - m_pos_tick > 100)
            {
                m_pos_tick = Environment.TickCount;
                readInformation();
            }

            if (Environment.TickCount - m_lastRecvTick > 1000)
                m_isConnected = false;

            try
            {
                sendProcess();
                recvProcess();
            }
            catch (Exception ex)
            {
                Debug.critical(ex,
                    "CSerialIAIMotor::run port:" + m_serialPort.PortName);
            }

            Thread.Yield();
            Thread.Sleep(50);
        }

        Debug.debug("CSerialIAIMotor::run END");
    }

    public bool isConnected()
    {
        return m_isConnected;
    }

    public void setSpeed(long speed)
    {
        m_speed = speed;
    }

    public void testSend()
    {
        string packet = "";
        packet += ":"; // Header
        packet += "01"; // Slave Address
        packet += "05"; // Function Code
        packet += "0403"; // Start Address
        packet += "FF00"; // Address Count

        byte LRC = makeLRC(packet);

        packet += LRC.ToString("X2");
        packet += "\r\n";

        send(ASCIIEncoding.ASCII.GetBytes(packet));
    }

    public byte makeLRC(string text)
    {
        byte lrc = 0;

        for (int i = 0; i < text.Length - 1; i += 2)
        {
            string t = text.Substring(i, 2);
            lrc += Convert.ToByte(t, 16);
        }

        lrc = (byte)(~lrc);
        return (byte)(lrc + 1);
    }

    public bool isPause()
    {
        return m_pause;
    }

    public void pause()
    {
        m_pause = true;
        decStop();
    }

    public bool resume()
    {
        if (m_pause == false)
            return false;

        if (checkPos(cmd))
        {
            m_pause = false;
            return true;
        }

        if (checkPos(cmd) == false)
        {
            moveAbsPosition(cmd);

            int tick = Environment.TickCount;

            while (true)
            {
                if (inpos() == true)
                {
                    m_pause = false;
                    break;
                }

                if (Environment.TickCount - tick > 5000)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void decStop()
    {
        string packet = "";
        packet += makeHeader(FC.forceSingleCoil_05, 0x042C);
        packet += "FF00";

        sendIAIPacket(packet);
    }

    public void servoOnOff(bool value)
    {
        string packet = "";
        packet += makeHeader(FC.forceSingleCoil_05, 0x0403);

        if (value)
            packet += "FF00";
        else
            packet += "0000";

        sendIAIPacket(packet);
    }

    public void home()
    {
        alarmReset();

        home(false);

        Util.waitTick(200);

        home(true);
    }

    private void home(bool value)
    {
        string packet = "";
        packet += makeHeader(FC.forceSingleCoil_05, 0x040B);

        if (value)
            packet += "FF00";
        else
            packet += "0000";

        sendIAIPacket(packet);
    }

    public void usePioMode(bool value)
    {
        string packet = "";
        packet += makeHeader(FC.forceSingleCoil_05, 0x0427);

        if (value)
            packet += "FF00";
        else
            packet += "0000";

        sendIAIPacket(packet);
    }

    public void moveStorePosition(int index)
    {
        string packet = "";
        packet += ":"; // Header
        packet += "01"; // Slave Address
        packet += "05"; // Function Code
        packet += "041F"; // Address - idx 0 

        packet += "0000";

        byte LRC = makeLRC(packet);

        packet += LRC.ToString("X2");
        packet += "\r\n";

        send(ASCIIEncoding.ASCII.GetBytes(packet));
    }

    public void absMove(long pos)
    {
        moveAbsPosition(pos);
    }

    public void moveAbsPosition(long pos)
    {
        cmd = pos;

        string packet = "";

        int count = 9;

        packet += makeHeader(FC.presetMultipleRegisters_10, 0x9900);
        packet += count.ToString("X4");
        packet += (count * 2).ToString("X2");

        //packet += "00001388"; // abs pos
        packet += pos.ToString("X8");

        int accVal = (int)(0.3f * 100);

        packet += "0000000A"; // 위치 결정 폭
        packet += m_speed.ToString("X8"); // 속도
        packet += accVal.ToString("X4"); // 가감속도
        packet += "0000"; // 밀어넣기
        packet += "0000"; // 제어 flag

        sendIAIPacket(packet);
    }

    // 현재 위치 모니터 p.142
    public void readInformation()
    {
        int count = 0x000F;

        // mm 단위를 반환
        string packet = "";
        packet += makeHeader(FC.readHoldingRegisters_03, 0x9000);
        packet += count.ToString("X4");

        sendIAIPacket(packet);
    }

    // p. 177
    public void setSafetySpeedMode(bool value)
    {
        // mm 단위를 반환
        string packet = "";
        packet += makeHeader(FC.forceSingleCoil_05, 0x0401);

        if (value) // 변경 DATA
            packet += "FF00";
        else
            packet += "0000";

        sendIAIPacket(packet);
    }

    public void jogPlusMove(bool move)
    {
        string packet = "";
        packet += makeHeader(FC.forceSingleCoil_05, 0x0416);

        if (move)
            packet += "FF00";
        else
            packet += "0000";

        sendIAIPacket(packet);
    }

    public void jogMinusMove(bool move)
    {
        string packet = "";

        packet += makeHeader(FC.forceSingleCoil_05, 0x0417);

        if (move)
            packet += "FF00";
        else
            packet += "0000";

        sendIAIPacket(packet);
    }

    public void alarmReset()
    {
        alarmReset(true);
        alarmReset(false);
    }

    private void alarmReset(bool value)
    {
        string packet = "";
        packet += makeHeader(FC.forceSingleCoil_05, 0x0407);

        if (value) packet += "FF00";
        else packet += "0000";

        sendIAIPacket(packet);
    }

    private string makeHeader(FC fc, int addr)
    {
        m_lastSendAddress = addr;

        string packet = "";
        packet += (m_axisNo + 1).ToString("X2");
        packet += ((int)fc).ToString("X2");
        packet += addr.ToString("X4");

        return packet;
    }

    private void sendIAIPacket(string text)
    {
        string packet = ":" + text;

        byte LRC = makeLRC(text);
        packet += LRC.ToString("X2");
        packet += "\r\n";

        send(ASCIIEncoding.ASCII.GetBytes(packet));
    }

    public bool inpos()
    {
        if (m_pos < cmd + 10 && m_pos > cmd - 10)
            return true;

        return false;
    }

    public bool checkPos(long value)
    {
        if (m_pos < value + 10 && m_pos > value - 10)
            return true;

        return false;
    }

    public long pos()
    {
        return m_pos;
    }

    public Information info()
    {
        return m_info;
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

            if (m_serialPort.IsOpen == false)
                return;

            byte[] buff = m_sendQueue.Dequeue();
            byte[] packet = new byte[buff.Length + 1];

            string text = "";

            for (int i = 0; i < buff.Length; i++)
                text += buff[i].ToString("X2") + " ";

            for (int i = 0; i < buff.Length; i++)
            {
                packet[i] = buff[i];
            }

            packet[packet.Length - 1] = 0;

            //Debug.debug("CSerialIAIMotor::sendProcess data:" + text);

            try
            {
                m_serialPort.Write(buff, 0, buff.Length);
            }
            catch (Exception e)
            {
                Debug.warning("CSerialIAIMotor::sendProces error reason:" + e.Message);
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

        //Debug.debug("CSerialIAIMotor::processIncoming data:" + text);

        if (IncomingData != null)
            IncomingData(data, null);

        string packet = ASCIIEncoding.ASCII.GetString(data);

        int idx = 0;

        idx += 1;

        m_isConnected = true;
        m_lastRecvTick = Environment.TickCount;

        try
        {
            int axisNo = Convert.ToInt32(packet.Substring(idx, 2), 16); idx += 2;
            int fc = Convert.ToInt32(packet.Substring(idx, 2), 16); idx += 2;

            if (fc == (int)FC.readHoldingRegisters_03)
            {
                int count = Convert.ToInt32(packet.Substring(idx, 2), 16); idx += 2;

                count /= 2;

                int[] recvData = new int[count];

                for (int i = 0; i < count; i++)
                {
                    recvData[i] = Convert.ToInt32(packet.Substring(idx, 4), 16); idx += 4;
                }

                if (m_lastSendAddress == 0x9000)
                {
                    // 0x9000 ~ 0x9001 : 현재 위치
                    // 0x9002 : 발생 알람 코드
                    // 0x9003 : I/O 입력 상태 조회
                    // 0x9004 : I/O 출력 상태 조회
                    // 0x9005 : Device Status Register 1 
                    // 0x9006 : Device Status Register 2
                    // 0x9007 : Ext Device Status Register 
                    // 0x9008 : System Status Register 
                    // 0x900A ~ 0x900B : 현재 속도 조회
                    // 0x900C ~ 0x900D : 전류치 조회
                    // 0x900E ~ 0x900F : 편차 조회

                    m_pos = ((recvData[0] << 16) | recvData[1]);

                    m_info.pos = Util.toLong(recvData[0], recvData[1]);
                    m_info.alarmCode = recvData[2];

                    // input 3
                    // output 4

#if false
                    public bool emergency; // EMGS : 15bit
                    public bool safety; // SFTY : 14bit
                    public bool ready; // PWR : 13bit
                    public bool servoOn; // SV : 12 bit
                    public bool pushResonance; // PSFL : 11 bit
                    public bool error; // ALMH : 10 bit
                    public bool alarm; // ALML : 9 bit
                    public bool absError; // ABER : 8 bit
                    public bool isBreak; // BKRL : 7 bit
                    // 6 - 사용안함
                    public bool pause; // STP : 5 bit
                    public bool home; // HEND : 4 bit
                    public bool inpos; // PEND : 3 bit
                    // 2 ~ 0 사용 안함
#endif

                    bool[] bit = new bool[16];
                    Util.intToBit(recvData[5], ref bit);

                    m_info.inpos = bit[3];
                    m_info.home = bit[4];
                    m_info.pause = bit[5];

                    m_info.isBreak = bit[7];
                    m_info.absError = bit[8];
                    m_info.alarm = bit[9];
                    m_info.error = bit[10];
                    m_info.pushResonance = bit[11];
                    m_info.servoOn = bit[12];
                    m_info.ready = bit[13];
                    m_info.safety = bit[14];
                    m_info.emergency = bit[15];

                    if (m_info.servoOn == false)
                        servoOnOff(true);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.debug("CSerialIAIMotor::processIncoming error reason:" + ex.Message);
        }
    }
}
