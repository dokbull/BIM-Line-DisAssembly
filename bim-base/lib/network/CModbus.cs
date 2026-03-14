using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.IO;
using Microsoft.Win32;

public class CModbus
{
    public enum FUNC
    {
        //TODO@tmdwn..나머지 부분 작성 할 것

        READ_INPUT = 0x04,
        READ_HOLDING = 0x03,
        WRITE_SINGLE = 0x06,
        WRITE_MULTI = 0x10
    }

    public enum TYPE
    {
        RTU,
        TCP,
        ASCII,
        UNKNOWN,
    }

    public class READ_DATA
    {
        public string name = "";

        public int addr = 0;
        public int length = 0;
        public int[] value;

        public READ_DATA(string _name, int _addr, int _length)
        {
            name = _name;

            addr = _addr;
            length = _length;

            value = new int[length];
        }
    }

    public class WRITE_DATA
    {
        public int addr = 0;
        public byte[] value;

        public WRITE_DATA(int _addr, byte[] _value)
        {
            addr = _addr;
            value = _value;
        }
    }

    TYPE m_type = TYPE.UNKNOWN;

    CSerialModbus m_serial = null;
    SerialPort m_port = null;
    byte m_slave = 0;

    public event EventHandler recvData;

    Thread m_thread = null;
    bool m_stop = false;

    bool m_simulation = false;

    Queue<WRITE_DATA> m_writeMultiDataQueue = new Queue<WRITE_DATA>();
    Queue<WRITE_DATA> m_writeSingleDataQueue = new Queue<WRITE_DATA>();
    List<READ_DATA> m_readDataList = new List<READ_DATA>();

    READ_DATA m_lastReadData = null;
    CElaspedTimer m_recvTimer = new CElaspedTimer(1000);
    bool m_waitRecv = false;
    int m_lastReadIndex = 0;


    public CModbus(TYPE type, SerialPort port, byte slave)
    {
        if (File.Exists(pathUtil.myDocumnent() + "\\simulation"))
            m_simulation = true;

        m_type = type;
        m_port = port;
        m_slave = slave;

        m_serial = new CSerialModbus(m_port);
        m_serial.recvData += recvData;
        m_serial.start();

        m_thread = new Thread(run);
        m_thread.Start();
    }

    ~CModbus()
    {
        m_stop = true;
    }

    public bool isConnected()
    {
        return m_serial.isConnected();
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
        Debug.debug("CModbus::run START Modbus:" + m_port.PortName);

        while (true)
        {
            if (m_stop)
            {
                Debug.debug("CModbus::run STOP");
                break;
            }

            if (m_simulation)
            {
                recvCheck();
                writeSingleRegisters();
                writeMultipleRegisters();
                readInputRegisters();

                Thread.Sleep(50);

                continue;
            }

            if (m_serial.isConnected() == true)
            {
                try
                {
                    recvCheck();
                    writeSingleRegisters();
                    writeMultipleRegisters();
                    readInputRegisters();
                }
                catch (Exception ex)
                {
                    Debug.critical(ex, "CModbus::run Modbus:" + m_port.PortName);
                }
            }

            Thread.Yield();
            Thread.Sleep(10);
        }

        if (m_serial != null)
            m_serial.stop();

        Debug.debug("CModbus::run END");
    }


    void recvCheck()
    {
        if (m_serial.isRecv() == true)
            clearReadWait();
    }

    private void writeMultipleRegisters()
    {
        if (m_waitRecv == true)
            return;
        
        try
        {
            if (m_writeMultiDataQueue.Count > 0)
            {
                WRITE_DATA writeData = m_writeMultiDataQueue.Dequeue();

                byte[] sendData = null;

                if (m_type == TYPE.RTU)
                {
                    int count = (writeData.value.Length / 2); // 1레지스터 당 2바이트 기록됨
                    sendData = makeRtuWritePacket(FUNC.WRITE_MULTI, m_slave, writeData.value, writeData.addr, count) ;
                }

                m_serial.send(sendData);

                m_waitRecv = true;
                m_recvTimer.start();

                Util.waitTick(50);
            }
        }
        catch (Exception ex)
        {
            Debug.debug("CModbus exception:" + ex.Message + " trace:" + ex.StackTrace);
            Thread.Sleep(1000);
        }
    }


    private void writeSingleRegisters()
    {
        if (m_waitRecv == true)
            return;

        try
        {
            if (m_writeSingleDataQueue.Count > 0)
            {
                WRITE_DATA writeData = m_writeSingleDataQueue.Dequeue();

                byte[] sendData = null;

                if (m_type == TYPE.RTU)
                {
                    sendData = makeRtuWritePacket(FUNC.WRITE_SINGLE, m_slave, writeData.value, writeData.addr);
                }

                m_serial.send(sendData);

                m_waitRecv = true;
                m_recvTimer.start();

                Util.waitTick(50);
            }
        }
        catch (Exception ex)
        {
            Debug.debug("CModbus exception:" + ex.Message + " trace:" + ex.StackTrace);
            Thread.Sleep(1000);
        }
    }

    private void readInputRegisters()
    {
        if (m_recvTimer.isElasped() == true)
        {
            m_waitRecv = false;
            m_recvTimer.stop();
            Debug.debug("CModbus::readInputResiter timeout.");
        }    

        if (m_waitRecv == true)
            return;

        lock (m_readDataList)
        {
            try
            {
                int readCount = m_readDataList.Count;

                if (readCount > 0)
                {
                    if (m_lastReadIndex > (readCount - 1))
                        m_lastReadIndex = 0;

                    READ_DATA readData = m_readDataList[m_lastReadIndex];

                    byte[] sendData = makeRtuReadPacket(FUNC.READ_INPUT, m_slave, readData.addr, readData.length);

                    m_serial.send(sendData);

                    m_lastReadData = readData;
                    m_lastReadIndex++;
                    m_waitRecv = true;
                    m_recvTimer.start();
                }

                Util.waitTick(50);
            }
            catch (Exception ex)
            {
                Debug.debug("CModbus::readInputRegister exception:" + ex.Message + " trace:" + ex.StackTrace);
                Thread.Sleep(1000);
            }
        }
    }

    public bool addWriteMultiData(WRITE_DATA data)
    {
        if (m_waitRecv == true)
            return false;

        m_writeMultiDataQueue.Enqueue(data);
        return true;
    }

    public void addWriteSingleData(WRITE_DATA data)
    {
        m_writeSingleDataQueue.Enqueue(data);
    }

    public bool addReadData(READ_DATA data)
    {
        if (m_waitRecv == true)
            return false;

        m_readDataList.Add(data);

        return true;
    }

    public List<READ_DATA> readDataList()
    {
        return m_readDataList;
    }

    public READ_DATA lastReadData()
    {
        return m_lastReadData;
    }

    public void clearReadWait()
    {
        m_waitRecv = false;
        m_recvTimer.stop();
    }

    public READ_DATA readData(string name)
    {
        READ_DATA data = null;

        for (int i = 0; i < m_readDataList.Count; i++)
        {
            if (m_readDataList[i].name == name)
                data = m_readDataList[i];
        }

        return data;
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

    public static byte[] makeRtuReadPacket(FUNC funcCode, int slaveID, int address, int count)
    {
        byte[] data = new byte[8];

        data[0] = (byte)slaveID; // slave ID
        data[1] = (byte)funcCode;
        data[2] = (byte)((address >> 8) & 0xFF);
        data[3] = (byte)(address & 0xFF);
        data[4] = (byte)((count >> 8) & 0xFF);
        data[5] = (byte)(count & 0xFF);

        byte[] checksum = ModbusCRC.makeCRC16(data, 6);
        data[6] = checksum[0];
        data[7] = checksum[1];

        return data;
    }

    public static byte[] makeRtuWritePacket(FUNC funcCode, int slaveID, byte[] value, int address, int count = 1)
    {
        int length = 8;

        if (funcCode == FUNC.WRITE_MULTI) // 다중 쓰기 모드
            length = 9 + value.Length;

        byte[] data = new byte[length];
        int idx = 0;

        data[idx++] = (byte)slaveID; // slave ID
        data[idx++] = (byte)funcCode;
        data[idx++] = (byte)((address >> 8) & 0xFF);
        data[idx++] = (byte)(address & 0xFF);

        if (funcCode == FUNC.WRITE_MULTI) // 다중 쓰기 추가 영역
        {
            data[idx++] = (byte)((count >> 8) & 0xFF);
            data[idx++] = (byte)(count & 0xFF);
            data[idx++] = (byte)(value.Length);
        }

        for (int i = 0; i < value.Length; i++)
        {
            data[idx++] = value[i];
        }

        byte[] checksum = ModbusCRC.makeCRC16(data, length - 2);
        data[idx++] = checksum[0];
        data[idx++] = checksum[1];

        return data;
    }

    public static byte[] makeTcpPacket(int index, int funcCode, int slaveID, int address, int count)
    {
        byte[] data = new byte[12];

        data[0] = (byte)((index >> 8) & 0xFF);
        data[1] = (byte)(index & 0xFF);

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

    public static string makeAsciiPacket(int funcCode, int slaveID, int address, int count)
    {
        string ret = ":";

        byte slave = Convert.ToByte(slaveID);
        ret += slave.ToString("X2");

        byte func = Convert.ToByte(funcCode);
        ret += func.ToString("X2");

        byte[] addr = new Byte[2];
        addr[0] = (byte)((address >> 8) & 0xFF);
        addr[1] = (byte)(address & 0xFF);

        ret += addr[0].ToString("X2");
        ret += addr[1].ToString("X2");

        byte[] length = new Byte[2];
        length[0] = (byte)((count >> 8) & 0xFF);
        length[1] = (byte)(count & 0xFF);

        ret += length[0].ToString("X2");
        ret += length[1].ToString("X2");

        // make LRC
        byte[] data = new byte[6];
        data[0] = slave;
        data[1] = func;
        data[2] = addr[0];
        data[3] = addr[1];
        data[4] = length[0];
        data[5] = length[1];

        string checkLRC = ModbusLRC.makeLRC(data).ToString("X2");
        ret += checkLRC;

        ret += "CR";
        ret += "LF";

        return ret;
    }



}
