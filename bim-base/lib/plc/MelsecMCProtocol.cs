using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

public enum AddrType
{
    SM = 0x91,
    SD = 0xA9,

    X = 0x9C,
    Y = 0x9D,
    M = 0x90,
    L = 0x92,
    F = 0x93,
    V = 0x94,
    B = 0xA0,

    D = 0xA8,
    W = 0xB4,

    ZR = 0xB0,
}

public class MelsecMCProtocol
{
    CNetworkClient m_client = null;

    byte m_networkNo = 0;
    byte m_stationNo = 0;

    bool m_hasRecv = false;
    bool m_stop = false;

    int[] m_readArray = null;
    int[] m_writeArray = null;

    static MelsecMCProtocol m_inst = null;
    object m_lockObject = null;

    public EventHandler recvData;

    class LastReadInfo
    {
        public AddrType addrType;
        public int adderss;
        public int count;
        public int[] array;
        public int offset;

        public bool isRead = false;
    }

    LastReadInfo m_lastCommandInfo = new LastReadInfo();

    public MelsecMCProtocol(string ip, int port, int networkNo, int stationNo)
    {
        if (m_inst != null)
            return;

        m_inst = this;

        m_networkNo = (byte)networkNo;
        m_stationNo = (byte)stationNo;

        m_client = new CNetworkClient(ip, port);
        m_client.recvData += recvHandler;
    }

    public static MelsecMCProtocol inst()
    {
        return m_inst;
    }

    public void setParameter(ref int[] readArray, ref int[] writeArray, object lockObject)
    {
        m_readArray = readArray;
        m_writeArray = writeArray;

        m_lockObject = lockObject;
    }

    public void start()
    {
        m_client.start();
    }

    public void stop()
    {
        m_stop = true;
        m_client.stop();
    }

    public bool isConnect()
    {
        return m_client.isConnected();
    }

    public bool connect()
    {
        return true;
    }

    public bool disconnect()
    {
        return true;
    }

    public bool read(AddrType addrType, int addressNo, int count, ref int[] array, int offset = 0)
    {
        if (m_client.isConnected() == false)
            return false;

        byte[] sendArray = new byte[21];

        // Sub header
        sendArray[0] = 0x50;
        sendArray[1] = 0x00;

        // Access route
        sendArray[2] = m_networkNo; // network no
        sendArray[3] = 0xFF; // pc no
        sendArray[4] = 0xFF; // request dest module i/o nomodule i/o no
        sendArray[5] = 0x03; // request dest 
        sendArray[6] = m_stationNo; // request dest module station no

        // Request data length
        sendArray[7] = 12; // 12
        sendArray[8] = 0x00;

        // Monitoring Timer
        sendArray[9] = 0x10; // 4s
        sendArray[10] = 0x00;

        // Command READ
        sendArray[11] = 0x01;
        sendArray[12] = 0x04;

        // Sub command
        sendArray[13] = 0x00;
        sendArray[14] = 0x00;

        // Head device number
        sendArray[15] = (byte)(addressNo & 0xff);
        sendArray[16] = (byte)((addressNo >> 8) & 0xff);
        sendArray[17] = (byte)((addressNo >> 16) & 0xff);

        // Device code
        sendArray[18] = (byte)addrType;

        // head device number
        Util.setShort(ref sendArray, 19, count);

        m_lastCommandInfo.addrType = addrType;
        m_lastCommandInfo.adderss = addressNo;

        m_lastCommandInfo.count = count;
        m_lastCommandInfo.offset = offset;
        m_lastCommandInfo.array = array;

        m_lastCommandInfo.isRead = true;

        m_hasRecv = false;

        //Debug.debug("MelsecMCProtocol::read addr:" + addressNo.ToString("X4"));

        m_client.send(sendArray);

        return true;
    }

    public bool write(AddrType addrType, int addressNo, int count, int[] data, int offset = 0)
    {
        if (m_client.isConnected() == false)
            return false;

        byte[] sendArray = new byte[21 + (count * 2)];

        // Sub header
        sendArray[0] = 0x50;
        sendArray[1] = 0x00;

        // Access route
        sendArray[2] = m_networkNo;
        sendArray[3] = 0xFF;
        sendArray[4] = 0xFF;
        sendArray[5] = 0x03;
        sendArray[6] = m_stationNo;

        // Request data length
        Util.setShort(ref sendArray, 7, 12 + count * 2);

        // Monitoring Timer
        sendArray[9] = 0x10;
        sendArray[10] = 0x00;

        // Command WRITE
        sendArray[11] = 0x01;
        sendArray[12] = 0x14;

        // Sub command
        sendArray[13] = 0x00;
        sendArray[14] = 0x00;

        // Head device number
        sendArray[15] = (byte)(addressNo & 0xff);
        sendArray[16] = (byte)((addressNo >> 8) & 0xff);
        sendArray[17] = (byte)((addressNo >> 16) & 0xff);

        // Device code
        sendArray[18] = (byte)addrType;

        // 영역에 따라 count 가 틀려야 함
        int dataCount = count;

        if (addrType == AddrType.B)
        {
            if (count < 16)
                dataCount = 1;
            else
                dataCount = count / 16 + 1;
        }

        // Number of device points
        int cnt = 19;
        Util.setShort(ref sendArray, 19, count); cnt += 2;

        for (int i = 0; i < count; i++)
        {
            Util.setShort(ref sendArray, cnt, data[offset + i]);
            cnt += 2;
        }

        m_lastCommandInfo.addrType = addrType;
        m_lastCommandInfo.adderss = addressNo;

        m_lastCommandInfo.count = count;
        m_lastCommandInfo.offset = offset;
        m_lastCommandInfo.array = data;

        m_lastCommandInfo.isRead = false;

        m_hasRecv = false;

        m_client.send(sendArray);

        //Debug.debug("MelsecMCProtocol::write");

        return true;
    }

    public bool waitRecv(int maxTime)
    {
        int tickCount = Environment.TickCount;

        while (true)
        {
            if (Environment.TickCount - tickCount > maxTime)
            {
                return false;
            }

            if (m_hasRecv)
            {
                return true;
            }

            if (m_stop)
                return false;

            Thread.Sleep(32);
        }
    }

    public class RECV_PLC
    {
        public AddrType addrType;
        public int addr;
        public int dataCount = 0;
        public int[] data = null;
    }

    public void recvHandler(object sender, EventArgs e)
    {
        byte[] data = (byte[])sender; // byte data

#if false
        string byteText = "";
        for (int i = 0; i < data.Length; i++)
            byteText += data[i].ToString("X2") + " ";

        Debug.debug("MelsecMCProtocol::recvHandler size:" + data.Length + " text:" + byteText);
#endif

        int cnt = 0;

        int subHeader = Util.toInt(data, cnt); cnt += 2;
        int networkNo = data[cnt++];
        int pcNo = data[cnt++];
        int moduleIONo = Util.toInt(data, cnt); cnt += 2;
        int stationNo = data[cnt++];
        int responseLength = Util.toInt(data, cnt); cnt += 2;
        cnt += 2; // End Code

        m_hasRecv = true;

        int dataCount = responseLength / 2 - 1;
        byte[] responseData = new byte[dataCount];
        // "00 00" 의 값을 가지는 end code 도 response 길이에 영향을 주기 때문에

        RECV_PLC recvPLC = new RECV_PLC();

        recvPLC.addrType = m_lastCommandInfo.addrType;
        recvPLC.addr = m_lastCommandInfo.adderss;
        recvPLC.dataCount = dataCount;
        recvPLC.data = new int[dataCount];

        if (m_lastCommandInfo.isRead == false)
            return;

        if (m_lastCommandInfo.count != recvPLC.dataCount)
            return;

        for (int i = 0; i < dataCount; i++)
        {
            //m_readArray[i] = data[cnt++];
            recvPLC.data[i] = data[cnt + 0] + (data[cnt + 1] << 8);
            cnt += 2;
        }

        if (recvData != null)
        {
            recvData(recvPLC, null);
        }

        data = null;
    }
}
