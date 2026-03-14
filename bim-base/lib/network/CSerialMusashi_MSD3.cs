using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.IO;

public class CSerialMusashi_MSD3
{
    byte STX = 0x02;
    byte ETX = 0x03;
    byte EOT = 0x04;
    byte ENQ = 0x05;
    byte ACK = 0x06;

    #region MANUAL DATA 정의

    public enum CMD_DL
    {
        DI, // 토출
        PE, // 셋업 위치로 이동
        TM, // 타임 모드
        MA, // 매뉴얼 모드
        IN, // INT 모드
        EX, // EXT 모드
        ST, // 스텝
        PR, // 프리셋
        RS, // 에러리셋
        //PO, // 파라미터 삭제
        //PC, // 전체 메모리 삭제
    }

    public enum CMD_COUNTER
    {
        C1, // 카운터1 삭제
        C2, // 카운터2 삭제
    }

    public enum CMD_SCREW
    {
        DT, // 토출시간 설정
        SD, // 토출회전 수 설정
        AC, // 토출가속 시간 설정
        DD // 토출설정
    }

    public enum CMD_SUCKBACK
    {
        BJ, // 석백 시간량 설정
        BS, // 석백 회전 수
        BT, // 석백 지연 시간
        SJ, // 석백 파라미터
    }

    public enum CMD_SYRINGE
    {
        TT, // 시린지 타이머 설정
    }

    public enum CMD_DELAY_TIME
    {
        OD, // ON 지연 타이머
        OF, // OFF 지연 타이머
    }

    public enum CMD_AUTO_INC
    {
        AI, // 자동인크리먼트 ONOFF
        IC, // 자동인크리먼트 시작 종료 CH
        IM, // 자동인크리먼트 전환 조건
        IP, // 자동인크리먼트 파라메터
    }

    public enum CMD_SS_FUNC
    {
        SS, // SS 기능 ON/OFF
        SR, // SS 기능 동작 조건 설정
        TB //SS 기능 터보 기능 설정
    }

    public enum CMD_DETAIL
    {
        LS, // 언어설정
        DS, // DSO 출력
        TS, // TMS/SETUP 신호의 선택
        BO, // 경보음 ON/OFF
    }

    public enum CMD_CH
    {
        LH, // 로드 채널
        SH // 복사 채널
    }

    public enum CMD_UPLOAD
    {
        STATUS = 0x0,
        DISCHARGE = 0x01, // 토출
        SUCKBACK = 0x02, // 석백
        SYRINGE_TMR = 0x03, // 시린지 TIMER
        DELAY_TMR = 0x04, // DELAY TIMER
        AUTO_INC_SET = 0x05, // 자동인크리먼트 설정
        AUTO_INC_STATUS = 0x06, // 자동인크리먼트의 상태
        COUNTER = 0x07, // 카운터 데이터
        STOP_WATCH = 0x08, // 실 토출 시간
        DETAIL_SET = 0x09, // 상세 설정
        SS_SET = 0x10, // SS 기능 설정
        SS_TURBO_SET = 0x11, // SS 기능 터보 설정
        SW_VER = 0x12, // SW VER
    }

    public enum UL_STATUS
    {
        WAIT_DISCHARGE = 0x00, // 토출 대기 중
        
        DISCHARGE_ING_TIME = 0x07, // 타임 토출 중
        DISCHARGE_ING_MANUAL = 0x08, // 매뉴얼 토출 중

        MOVING_SETUP = 0x10, // SETUP 위치로 이동 중
        WAIT_SETUP = 0x11, // SETUP 위치에서 대기 중

        ALARM = 0x18,

        DATA_READ_ERR = 0x19,
        PANER_CONTROL = 0x20

        //MOVE_ING_
    }

    public enum ERROR_CODE
    {
        A0,
        A2,
        A3,
        A4,
        A5,
        A6m

    }
    #endregion

    public class DataControl
    {
#if false
        int time;
        int rpm;
        int acc;
#endif
    }

    public class DataCurrent
    {
        DataControl discharge = new DataControl();
        DataControl suckback = new DataControl();
    }

    DataCurrent m_currentData = new DataCurrent();

    static int READ_BUFF_SIZE = 1024;

    public event EventHandler IncomingData;

    SerialPort m_serialPort = null;

    CircularBuffer m_recvBuffer = new CircularBuffer(1024 * 1024); // 1MB
    Queue<byte[]> m_sendQueue = new Queue<byte[]>();

    byte[] m_readBuffer = new byte[READ_BUFF_SIZE];

    Thread m_thread = null;
    bool m_stop = false;

    bool m_simulation = false;

    public CSerialMusashi_MSD3(SerialPort serialPort)
    {
        m_serialPort = serialPort;

        m_serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

        if (File.Exists(pathUtil.savePath() + "\\simulation"))
            m_simulation = true;

        m_thread = new Thread(run);
    }

    ~CSerialMusashi_MSD3()
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

            Debug.debug("CSerialMusashi_MSD3::dataReceived error reason:" + ex.Message);
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
        Debug.debug("CSerialMusashi_MSD3::run START name:" + m_serialPort.PortName);

        while (true)
        {
            if (m_stop)
            {
                Debug.debug("CSerialMusashi_MSD3::run STOP");
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
                    "CSerialMusashi_MSD3::run port:" + m_serialPort.PortName);
            }

            Thread.Yield();
            Thread.Sleep(30);
        }

        Debug.debug("CSerialMusashi_MSD3::run END");
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

    void sendString(string text)
    {
        byte[] data = ASCIIEncoding.ASCII.GetBytes(text);
        byte[] packet = new byte[data.Length + 4];

        byte checksum = 0x00;
        
        for (int i = 0; i < data.Length; i++)
        {
            packet[i + 1] = data[i];
            checksum -= data[i];
        }

        string checksumStr = checksum.ToString("X2");
        byte[] checksumData = new byte[2];
        checksumData = ASCIIEncoding.ASCII.GetBytes(checksumStr);

        packet[0] = STX;
        packet[packet.Length - 3] = checksumData[0];
        packet[packet.Length - 2] = checksumData[1];
        packet[packet.Length - 1] = ETX;

        send(packet);
    }

    void send(string text)
    {
        int len = text.Length;
        sendString(len.ToString("00") + text);
    }

    void send(byte data)
    {
        send(new byte[] { data });
    }

    public void setRPM(int rpm)
    {
        send(CMD_SCREW.SD + rpm.ToString());
    }

    public void setBackTime(int ms)
    {
        send(CMD_SUCKBACK.BJ + ms.ToString());
    }

    public void sendENQ()
    {
        send(ENQ);
    }

    public void sendEOT()
    {
        send(EOT);
    }

    public void setCh(int ch)
    {
        send(CMD_CH.LH + ch.ToString("0.000"));
    }

    public void sendInfo(int ch)
    {
        send("UL" + ch.ToString("000") + "," + CMD_UPLOAD.DISCHARGE);
    }

    public void sendGetRpmPacket(int no)
    {
        sendENQ();
        send("UL" + no.ToString("000") + ",01");
        sendEOT();
    }

    public void sendGetBackPacket(int no)
    {
        sendENQ();
        send("UL" + no.ToString("000") + ",02");
        sendEOT();
    }

    public void sendModelNo(int no)
    {
        sendENQ();
        send("LH" + no.ToString("000"));
        sendEOT();
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
                if (buff[i] == ACK || buff[i] == EOT)
                {
                    byte[] ackData = new byte[i + 1];
                    m_recvBuffer.read(ref ackData, (uint)i + 1);
                    processIncmonig(ackData);

                    return true;
                }

                if (buff[i] == STX)
                {
                    isStx = true;
                    continue;
                }

                if (buff[i] != ETX)
                    continue;

                if (isStx == false)
                {
                    byte[] garbageBuff = new byte[i + 1];
                    m_recvBuffer.read(ref garbageBuff, (uint)i + 1);

                    return false;
                }

                byte[] data = new byte[i + 1];
                m_recvBuffer.read(ref data, (uint)i + 1);
                processIncmonig(data);

                return true;
            }
        }

        return true;
    }

    private void processIncmonig(byte[] data)
    {
        string text = "";

        for (int i = 0; i < data.Length; i++)
            text += data[i].ToString("X2") + " ";

        //Debug.debug("CSerialMusashi_MSD3::processIncoming data:" + text);

        //string cmd = "DA";
        string subCmd ="00";

        string[] splitStr = text.Split(',');

        if (subCmd == "00")
        {
            //int nowCh = Util.toInt32(splitStr[arrCnt++]);
            //int inExt = Util.toInt32(splitStr[arrCnt++]); // 0 - IN, 1 - EXT
        }

        if (IncomingData != null)
            IncomingData(data, null);
    }
}
