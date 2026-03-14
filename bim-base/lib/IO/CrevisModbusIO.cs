using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using static CModbus;

public class CrevisModbusIO
{
    public enum FUNC
    {
        //TODO@tmdwn..나머지 부분 작성 할 것

        READ_INPUT = 0x04,
        READ_HOLDING = 0x03,
        WRITE_SINGLE = 0x06,
        WRITE_MULTI = 0x10
    }

    public enum CMD_INDEX
    {
        WRITE = 0,
        READ = 1,
    }

    string m_ip = "";
    int m_port = 0;

    bool m_simulation = false;

    bool[] m_input = null;
    bool[] m_output = null;
    bool[] m_agoOutput = null;

    TcpClient m_client = null;
    NetworkStream m_stream = null;
    Thread m_thread = null;
    bool m_running = false;
    bool m_isConnected = false;
    readonly object m_sendLock = new object();

    public CrevisModbusIO(int inBoardCount, int outBoardCount, string ip, int port = 502)
    {
        m_ip = ip;
        m_port = port;

        m_input = new bool[inBoardCount * 16];
        m_output = new bool[outBoardCount * 16];
        m_agoOutput = new bool[outBoardCount * 16];

        for (int i = 0; i < m_input.Length; i++)
            m_input[i] = false;

        for (int i = 0; i < m_output.Length; i++)
        {
            m_output[i] = false;
            m_agoOutput[i] = false;
        }

        Connect();
    }


    private void Connect()
    {
        try
        {
            m_client = new TcpClient();
            m_client.Connect(m_ip, m_port);
            m_stream = m_client.GetStream();
            m_isConnected = true;

            m_running = true;
            m_thread = new Thread(run);
            m_thread.IsBackground = true;
            m_thread.Start();
        }
        catch (Exception)
        {
            m_isConnected = false;
            m_running = false;
        }
    }

    public bool isConnected()
    {
        return m_isConnected && m_client != null && m_client.Connected;
    }

    public void stop()
    {
        m_running = false;
        m_isConnected = false;

        try
        {
            if (m_stream != null)
                m_stream.Close();
            if (m_client != null)
                m_client.Close();
        }
        catch
        {
        }

        m_stream = null;
        m_client = null;
    }

    private void Send(byte[] data)
    {
        if (isConnected() == false)
            return;

        try
        {
            lock (m_sendLock)
            {
                m_stream.Write(data, 0, data.Length);
                m_stream.Flush();
            }
        }
        catch (Exception)
        {
            m_isConnected = false;
            m_running = false;
        }
    }

    private void run()
    {
        var buffer = new List<byte>();

        try
        {
            byte[] temp = new byte[1024];

            while (m_running && isConnected())
            {
                int read = 0;
                try
                {
                    read = m_stream.Read(temp, 0, temp.Length);
                }
                catch
                {
                    m_isConnected = false;
                    break;
                }

                if (read <= 0)
                {
                    m_isConnected = false;
                    break;
                }

                buffer.AddRange(temp.Take(read));

                while (true)
                {
                    if (buffer.Count < 6)
                        break;

                    int length = (buffer[4] << 8) | buffer[5];
                    int frameLen = 6 + length;

                    if (buffer.Count < frameLen)
                        break;

                    byte[] frame = buffer.GetRange(0, frameLen).ToArray();
                    buffer.RemoveRange(0, frameLen);

                    HandleReceivedFrame(frame);
                }
            }
        }
        finally
        {
            m_isConnected = false;
            m_running = false;
        }
    }

    private void HandleReceivedFrame(byte[] data)
    {
        string text = BitConverter.ToString(data);
        string[] divText = text.Split('-');

        string debugText = "";

        for (int i = 0; i < divText.Length; i++)
            debugText += divText[i] + " ";

        // Debug.debug("CrevisModbusIO::recvData length:" + data.Length + " text:" + debugText);

        int length = (data[4] << 8) | data[5];

        if (data.Length != 6 + length)
            return;

        byte fc = data[7];

        if ((fc & 0x80) != 0)
            return;

        if (fc == (byte)FUNC.READ_HOLDING)
        {
            if (data[1] == 0x01) // INPUT ARRAY
            {
                int byteCount = data[8];
                if (data.Length < 9 + byteCount)
                    return;

                if ((byteCount % 2) != 0)
                    return;

                int n = byteCount / 2;

                int[] input = new int[n];
                int idx = 9;

                for (int i = 0; i < n; i++)
                {
                    input[i] = (int)((data[idx] << 8) | data[idx + 1]);
                    idx += 2;
                }

                int bitIndex = 0;

                for (int i = 0; i < input.Length; i++)
                {
                    int reg = input[i];

                    for (int bit = 0; bit < 16; bit++)
                    {
                        if (bitIndex > m_input.Length - 1)
                            return;

                        m_input[bitIndex++] = ((reg >> bit) & 0x01) == 1;
                    }
                }

                return;
            }

            if (data[1] == 0x00) // OUTPUT ARRAY
            {
                int byteCount = data[8];
                if (data.Length < 9 + byteCount)
                    return;

                if ((byteCount % 2) != 0)
                    return;

                int n = byteCount / 2;

                int[] output = new int[n];
                int idx = 9;

                for (int i = 0; i < n; i++)
                {
                    output[i] = (int)((data[idx] << 8) | data[idx + 1]);
                    idx += 2;
                }

                int bitIndex = 0;

                for (int i = 0; i < output.Length; i++)
                {
                    int reg = output[i];

                    for (int bit = 0; bit < 16; bit++)
                    {
                        if (bitIndex > m_output.Length - 1)
                            return;

                        m_output[bitIndex++] = ((reg >> bit) & 0x01) == 1;
                    }
                }

                return;
            }
        }
    }

    public bool input(int index)
    {
        return m_input[index];
    }

    public bool output(int index)
    {
        return m_output[index];
    }

    public void setOutput(int index, bool value)
    {
        if (isConnected() == false)
            return;

        m_output[index] = value;

        // 실제 WRITE는 updateOut()에서 변경 여부 체크 후 한번에 보냄
    }

    public void update()
    {
        updateIn();
        updateOut();
    }

    void updateIn()
    {
        if (isConnected() == false)
            return;

        // READ
        int[] array = toArray(m_input);
        byte[] packet = makeTcpReadPacket(CMD_INDEX.READ, 1, 0x0000, array.Length);

        //string text = BitConverter.ToString(packet);
        Send(packet);
    }

    void updateOut()
    {
        if (isConnected() == false)
            return;

        bool checkRefresh = false;

        for (int i = 0; i < m_output.Length; i++)
        {
            if (m_agoOutput[i] != m_output[i])
                checkRefresh = true;

            m_agoOutput[i] = m_output[i];
        }

        if (checkRefresh == false)
            return;

        // WRITE
        int[] array = toArray(m_output);
        byte[] packet = makeTcpWritePacket(1, 0x0800, array);

        Send(packet);
    }

    public bool refreshOutput()
    {
        if (isConnected() == false)
            return false;

        // READ
        int[] array = toArray(m_output);
        byte[] packet = makeTcpReadPacket(CMD_INDEX.WRITE, 1, 0x0800, array.Length);

        //string text = BitConverter.ToString(packet);
        Send(packet);

        return true;
    }

    public static int[] toArray(bool[] value)
    {
        int count = (value.Length + 15) / 16;
        int[] ret = new int[count];

        for (int i = 0; i < value.Length; i++)
        {
            if (value[i])
            {
                int idx = i / 16;
                int pos = i % 16;
                ret[idx] |= (1 << pos);
            }
        }

        return ret;
    }

    public static byte[] makeTcpReadPacket(CMD_INDEX index, int slaveID, int address, int count)
    {
        FUNC funcCode = FUNC.READ_HOLDING;

        byte[] data = new byte[12];

        data[0] = (byte)(((int)index >> 8) & 0xFF);
        data[1] = (byte)((int)index & 0xFF);

        int modbusProtocal = 0x0000; // FIX data

        data[2] = (byte)((modbusProtocal >> 8) & 0xFF);
        data[3] = (byte)(modbusProtocal & 0xFF);

        int dataLength = 6; // FIX data

        data[4] = (byte)((dataLength >> 8) & 0xFF);
        data[5] = (byte)(dataLength & 0xFF);

        data[6] = (byte)slaveID;
        data[7] = (byte)funcCode;
        data[8] = (byte)((address >> 8) & 0xFF);
        data[9] = (byte)(address & 0xFF);
        data[10] = (byte)((count >> 8) & 0xFF);
        data[11] = (byte)(count & 0xFF);

        return data;
    }

    public static byte[] makeTcpWritePacket(int slaveID, int address, int[] values)
    {
        CMD_INDEX index = CMD_INDEX.WRITE;
        FUNC funcCode = FUNC.WRITE_MULTI;

        int count = values.Length;
        int byteCount = count * 2;

        int dataLength = 1 + 1 + 2 + 2 + 1 + byteCount;

        byte[] data = new byte[6 + dataLength];

        data[0] = (byte)(((int)index >> 8) & 0xFF);
        data[1] = (byte)((int)index & 0xFF);

        int modbusProtocal = 0x0000; // FIX data

        data[2] = (byte)((modbusProtocal >> 8) & 0xFF);
        data[3] = (byte)(modbusProtocal & 0xFF);

        data[4] = (byte)((dataLength >> 8) & 0xFF);
        data[5] = (byte)(dataLength & 0xFF);

        data[6] = (byte)slaveID;
        data[7] = (byte)funcCode;
        data[8] = (byte)((address >> 8) & 0xFF);
        data[9] = (byte)(address & 0xFF);
        data[10] = (byte)((count >> 8) & 0xFF);
        data[11] = (byte)(count & 0xFF);
        data[12] = (byte)byteCount;

        int indexData = 13;

        for (int i = 0; i < count; i++)
        {
            data[indexData++] = (byte)((values[i] >> 8) & 0xFF);
            data[indexData++] = (byte)(values[i] & 0xFF);
        }

        return data;
    }
}
