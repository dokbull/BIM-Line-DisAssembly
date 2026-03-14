using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

public partial class LSFenet_MOBIS
{
	Thread m_thread = null;

    CNetworkClient m_client = null;

    public event EventHandler sendDataEvent;
    public event EventHandler recvDataEvent;

	int m_clientIndex = 0;

    string[] readAddr = new string[32];
    int[] readCount = new int[32];

    string m_lastReadAddr = "";

    public LSFenet_MOBIS(string ip, int port)
    {
        m_thread = new Thread(run);

        m_client = new CNetworkClient(ip, port);
        m_client.start();

        m_client.recvData += recvPLC;
    }

    public void reConnect()
    {
        m_client.disconnected(); // disconnected가 완료되면 재접속됨.
        Debug.debug("LSFenet_MOBIS::reConnect socket disconnected complete.");
    }

    public void start()
    {
        m_thread.Start();
    }

    public void stop()
    {
        m_stop = true;
    }

    public bool isConnected()
    {
        return m_client.isConnected();
    }

	int insertAddrText(byte[] buff, int offset, string text)
	{
		byte[] textBytes = ASCIIEncoding.ASCII.GetBytes(text);
		for (int i = 0; i < textBytes.Length; i++)
		{
			buff[offset + i] = textBytes[i];
		}

		return offset + textBytes.Length;
	}

	public class FNET
	{
		public enum BLOCK
		{
			READ_ONE = 0x0,
			READ_CONTI = 0x1,
			WRITE_ONE = 0x02,
			WRITE_CONTI = 0x1400,
		}

		public enum CMD
		{
			READ_ONE = 0x0,
			READ_CONTI = 0x1,
			WRITE_ONE = 0x02,
			WRITE_CONTI = 0x1400,
		}
	}

	string[] m_writePLCBCR = new string[2];

	public void writeData(string addr, int[] arr, int count, bool isAddrDB = false)
	{
        int dataCount = count * 2; // int 형일 경우 한개의 주소가 0xFFFFF 이기 때문에
		
        byte[] packet = new byte[24 + addr.Length + count * 2];

		// 5800 0200 0000 0100 0800 2557 5734 3632 3031 0200 1111 
		// 쓰기 개별 예약 블록 주소 %WW46201            길이 데이터  
		// 5800 0200 0000 0100 0800 2557 5735 3631 3031 0200 1111 
		// 쓰기 개별 예약 블록 주소 %WW56201            길이 데이터  

        if (isAddrDB == true)
        {
            string addrKind = addr.Substring(0, 3);
            string addrText = addr.Substring(3);

            if (addrKind == "%DB")
            {
                int value = Util.toInt32(addrText);
                addr = addrKind + (value * 2).ToString("D");
            }
        }

		packet[0] = 0x58; packet[1] = 0x00;
		packet[2] = 0x14; packet[3] = 0x00; // 연속 쓰기
		packet[4] = 0x00; packet[5] = 0x00;
		packet[6] = 0x01; packet[7] = 0x00; // 블록 수
		packet[8] = (byte)addr.Length; packet[9] = 0x00; // 어드레스 길이

		int cnt = insertAddrText(packet, 10, addr); // 10 ~ 17
		packet[cnt++] = (byte)dataCount; packet[cnt++] = 0x00; // data 갯수

        byte[] data = new byte[dataCount];

        for (int i=0; i<count; i++)
        {
            data[(i * 2) + 0] = (byte)((arr[i]) & 0xFF);
            data[(i * 2) + 1] = (byte)((arr[i] >> 8) & 0xFF);
        }

		for (int i = 0; i < data.Length; i++)
			packet[cnt + i] = data[i];

		byte[] header = makeFNETHeader(packet.Length);
		int totalLenth = header.Length + packet.Length;

		byte[] sendData = new byte[totalLenth];

		int length = 0;
		Array.Copy(header, 0, sendData, length, header.Length);
		length += header.Length;

		Array.Copy(packet, 0, sendData, length, packet.Length);

        m_hasRecv = false;

        string dddddddd = ASCIIEncoding.ASCII.GetString(sendData);

		m_client.send(sendData);
	}

    public void writeDataBool(string addr, bool value)
    {
        byte[] data = new byte[12 + addr.Length + 2];

        // 5800 0200 0000 0100 0800 2557 5734 3632 3031 0200 1111 
        // 쓰기 개별 예약 블록 주소 %WW46201            길이 데이터  

        data[0] = 0x58; data[1] = 0x00;
        data[2] = 0x00; data[3] = 0x00; // 개별 쓰기 BIT

        data[4] = 0x00; data[5] = 0x00;
        data[6] = 0x01; data[7] = 0x00; // 블록 수
        data[8] = (byte)addr.Length; data[9] = 0x00; // 어드레스 길이

        int cnt = insertAddrText(data, 10, addr); // 10 ~ 17
        data[cnt++] = 0x01; data[cnt++] = 0x00; // data 갯수

        byte valueData = 0x00;

        if (value == true)
            valueData = 0x01;

        data[cnt++] = valueData;

        byte[] header = makeFNETHeader(data.Length);
        int totalLenth = header.Length + data.Length;

        byte[] sendData = new byte[totalLenth];

        int length = 0;
        Array.Copy(header, 0, sendData, length, header.Length);
        length += header.Length;

        Array.Copy(data, 0, sendData, length, data.Length);

        m_hasRecv = false;
        m_client.send(sendData);
    }

    int m_lastReadAddress = 0x0;
    int m_lastReadCount = 0x0;

    /// <summary>
    /// LS산전 PLC FENET 통신
    /// </summary>
    /// <param name="addr">"%WW46201" 형식으로 입력</param>
    /// <param name="count">갯수 입력</param>
    public void readData(string addr, int count)
    {
        m_hasRecv = false;
        // 5400 0200 0000 0100 0800 2557 5734 3632 3031 0200 1111 
        // 읽기 개별 예약 블록 주소 %WW46201            길이 데이터  

        // 5400 1400 0000 0100 0800 2557 5735 3631 3031 0400 1111 
        // 읽기 다중 예약 블록 주소 %WW56201            길이 데이터  
        
        if (addr.Substring(1, 1) == "D")
        {
            string addrHeader = addr.Substring(0, 3);
            int addressConvert = Convert.ToInt32(addr.Substring(3));

            addressConvert = addressConvert * 2;

            addr = addrHeader + addressConvert.ToString();
        }

        byte[] data = new byte[12 + addr.Length];

        data[0] = 0x54; data[1] = 0x00; // read command
        data[2] = 0x02; data[3] = 0x00; // read type

        if (count > 1) // 다중 읽기
            data[2] = 0x14;

        data[4] = 0x00; data[5] = 0x00; // dummy
        data[6] = 0x01; data[7] = 0x00; // block size

        data[8] = (byte)addr.Length; data[9] = 0x00; // 어드레스 길이

        int cnt = insertAddrText(data, 10, addr); // 10 ~ 17

        m_lastReadAddress = Util.toInt32(addr.Substring(3));
        m_lastReadCount = count;

        data[cnt++] = (byte)(count & 0xff); 
        data[cnt++] = (byte)((count >> 8) & 0xff); // data 갯수

        byte[] header = makeFNETHeader(data.Length);
        int totalLenth = header.Length + data.Length;

        byte[] sendData = new byte[totalLenth];

        int length = 0;
        Array.Copy(header, 0, sendData, length, header.Length);
        length += header.Length;

        Array.Copy(data, 0, sendData, length, data.Length);

        byte[] debugData = new byte[sendData.Length];

        for (int i = 0; i < sendData.Length; i++)
        {
            debugData[i] = sendData[i];

            if (debugData[i] == 0x0)
                debugData[i] = 0x20;
        }

        string packetText = ASCIIEncoding.ASCII.GetString(debugData);
        //Debug.debug("LSFenet_MOBIS::readData text:" + packetText + "\r\n");

        m_client.send(sendData);

        if (sendDataEvent != null)
        {
            sendDataEvent(sendData, null);
        }
    }

    bool m_stop = false;

	private void run()
	{
		bool[,] agoValue = new bool[2, 10];

        int readIndex = 0;
        m_hasRecv = true;

		while (true)
		{
			if (m_stop)
			{
				break;
			}

			if (m_client.isConnected() == false || m_client == null)
			{
				Thread.Sleep(100);
				continue;
			}

			int tick = Environment.TickCount;

            if (readIndex > (readAddr.Count() - 1))
                readIndex = 0;

            if (readAddr[readIndex] == null || readAddr[readIndex] == "" || readCount[readIndex] == 0)
            {
                readIndex++;
                continue;
            }

            if (m_hasRecv)
            {
                m_hasRecv = false;

                m_lastReadAddr = readAddr[readIndex];
                readData(readAddr[readIndex], readCount[readIndex]);
                readIndex++;

                waitRecv(3000);
            }

            Thread.Sleep(100);
		}

        m_client.stop();
	}

    public void setReadAddr(int index, string addr, int count)
    {
        readAddr[index] = addr;
        readCount[index] = count;
    }

	bool m_hasRecv = false;

	public bool waitRecv(int maxTime)
	{
		int tickCount = Environment.TickCount;

		while (true)
		{
			if (Environment.TickCount - tickCount > maxTime)
			{
				Debug.debug("LSFenet_MOBIS::waitRecv timeout Error. socket disconnected.");
                m_hasRecv = true;
                reConnect();
				return false;
			}

			if (m_hasRecv)
			{
				return true;
			}

			if (m_stop)
				return false;

			Thread.Sleep(10);
		}
	}

	private void recvPLC(object sender, EventArgs e)
	{
		int index = m_clientIndex;

		byte[] data = (byte[])sender;

		string text = BitConverter.ToString(data);
		string[] divText = text.Split('-');
        
        string debugText = "";
        for (int i = 0; i < divText.Length; i++)
            debugText += divText[i] + " ";

        //Debug.debug("LSFenet_MOBIS::recvPLC data length:" + data.Length);

        byte[] recvData = null;

        if (data.Length > 32)
        {
            recvData = new byte[data.Length - 32];
        }

		string header = "";
		string code = "";
		string error = "";
		string recv = "";

		for (int i = 0; i < divText.Length; i++)
		{
			if (i < 20)
				header += divText[i];

			if (20 <= i && i <= 23) // 응답 코드 분류
				code += divText[i];

			if (26 <= i && i < (26 + 2)) // 에러 코드 분류
				error += divText[i];

            if (i > 31)
            {
                recv += divText[i];

                if (recvData != null)
                    recvData[i - 32] = data[i];
            }
		}

        if (error != "0000")
        {
            Debug.warning("LSFenet_MOBIS::recvPLC recv has error. code:" + error);
        }

		// SINGLE WRITE RETURN
        if (code == "59000000" || code == "59000100" || code == "59000200" || code == "59000300")
		{
			m_hasRecv = true;
			return;
		}

        // MULTI WRITE RETURN
		if (code == "59001400")
		{
			m_hasRecv = true;
			return;
		}

		// SINGLE READ
        if (code == "55000000" || code == "55000100" || code == "55000200" || code == "55000300")
		{
            string ret = code + "," + m_lastReadAddr + "," + recv;

            m_hasRecv = true;
            recvDataEvent(recvData, null);
            return;
        }

        // MULTI READ
        if (code == "55001400")
        {
            string ret = code + "," + m_lastReadAddr + "," + recv;

            m_hasRecv = true;
            recvDataEvent(recvData, null);
            return;
        }

		m_hasRecv = true;
	}

	private string replaceAsciiData(string text)
	{
		string ret = "";
		ret = text.Replace("\0", "");
		return ret;
	}

	private byte byteCheckSum(byte[] buff, int startIndex, int endIndex)
	{
		int checksum = 0x0;

		for (int i = startIndex; i < endIndex; i++)
		{
			checksum += buff[i];
			if (checksum > 255)
				checksum = checksum - 256;
		}

		return (byte)checksum;
	}

	private byte[] makeFNETHeader(int lenth)
	{
		byte[] company = new byte[8];
		company = Encoding.UTF8.GetBytes("LSIS-XGT"); // 4C53 4953 2D58 4754

		byte[] reserved = new byte[2];
		reserved = BitConverter.GetBytes('\0');  // 0000

		byte[] plcInfo = new byte[2];
		plcInfo = BitConverter.GetBytes('\0');  // 0000

		byte cpuInfo = 0xA4; // 00
		byte source = 0x33; // 33

		byte[] invoke = new byte[2];
		invoke = BitConverter.GetBytes('\0'); // 0000

		byte[] checkSum = BitConverter.GetBytes(lenth);

		byte fenet = 0x00;
		//byte bcc = 0x00;

		byte[] header = new byte[20];

		int index = 0;

		Array.Copy(company, 0, header, index, company.Length);
		index += company.Length;

		Array.Copy(reserved, 0, header, index, reserved.Length);
		index += reserved.Length;

		Array.Copy(plcInfo, 0, header, index, plcInfo.Length);
		index += plcInfo.Length;

		header[12] = cpuInfo;
		index++;

		header[13] = source;
		index++;

		Array.Copy(invoke, 0, header, index, invoke.Length);  // 14 15
		index += invoke.Length;

		Array.Copy(checkSum, 0, header, index, checkSum.Length);
		index += checkSum.Length;

		header[18] = fenet;
		index++;

		header[19] = byteCheckSum(header, 0, 18);
		index++;

		return header;
	}

    public string lastReadAddress()
    {
        return m_lastReadAddr;
    }

    public void writeDataByJP(string addr, int value)
    {
        byte[] data = new byte[14 + addr.Length];

        // 5800 0200 0000 0100 0800 2557 5734 3632 3031 0200 1111 
        // 쓰기 개별 예약 블록 주소 %WW46201            길이 데이터  
        // 5800 0200 0000 0100 0800 2557 5735 3631 3031 0200 1111 
        // 쓰기 개별 예약 블록 주소 %WW56201            길이 데이터  

        data[0] = 0x58; data[1] = 0x00;
        data[2] = 0x02; data[3] = 0x00; // 단일 쓰기
        data[4] = 0x00; data[5] = 0x00;
        data[6] = 0x01; data[7] = 0x00; // 블록 수
        data[8] = (byte)addr.Length; data[9] = 0x00; // 어드레스 길이

        int cnt = insertAddrText(data, 10, addr); // 10 ~ +addr
        data[cnt++] = 02; data[cnt++] = 0x00; // data 갯수

        data[cnt++] = (byte)(value);
        data[cnt++] = (byte)(value >> 8);

        byte[] header = makeFNETHeader(data.Length);
        int totalLenth = header.Length + data.Length;

        byte[] sendData = new byte[totalLenth];

        int length = 0;
        Array.Copy(header, 0, sendData, length, header.Length);
        length += header.Length;

        Array.Copy(data, 0, sendData, length, data.Length);
        //string debugData = "";
        m_client.send(sendData);
    }

    public int lastReadAddr()
    {
        return m_lastReadAddress; // addr (X) address (O)
    }

    public int lastReadCount()
    {
        return m_lastReadCount;
    }

}
