using Modbus.Device;
using System;
using System.IO;
using System.IO.Ports;
using System.Reflection;
using System.Threading;

public class CSerialFRENIC
{
    ModbusSerialMaster m_master = null;
    SerialPort m_port = null;

    bool m_simulation = false;
    bool m_stop = false;

    Thread m_thread = null;

    int m_count = 0;

    double[] m_freq = null;
    double[] m_setFreq = null;
    int[] m_status = null;
    
    public CSerialFRENIC(SerialPort serialPort, int count)
    {
        if (File.Exists(Common.PATH + "\\simulation"))
            m_simulation = true;

        m_port = serialPort;

        m_count = count;
        m_freq = new double[count];
        m_setFreq = new double[count];
        m_status = new int[count];

        m_master = ModbusSerialMaster.CreateRtu(m_port);
        m_master.Transport.ReadTimeout = 100;
        m_master.Transport.WriteTimeout = 100;

        m_thread = new Thread(run);
        m_thread.Start();
    }

    public void stop()
    {
        m_stop = true;
    }

    void run()
    {
        while (true)
        {
            if (m_stop)
            {
                Debug.debug("CSerialFRENIC: stop");
                break;
            }

            if (m_port.IsOpen == false && m_simulation == false)
            {
                m_port.Open();
            }

            for (int i = 0; i < m_count; i++)
            {
                if (m_simulation)
                    continue;

                try
                { 
                    ushort[] data = m_master.ReadHoldingRegisters((byte)(i + 1), 0x0801, 6);

                    m_setFreq[i] = (data[0] / 20000.0d) * 60.0d;
                    m_freq[i] = (data[5] / 20000.0d) * 60.0d;

                    data = m_master.ReadHoldingRegisters((byte)(i + 1), 0x0706, 1);
                    m_status[i] = data[0];
                    // Debug.debug("CSerialFRENIC: read data. slave:" + i + 1 + " freq:" + m_freq[i] + " setFreq:" + m_setFreq[i] + " state:" + m_status[i]);
                }
                catch (Exception e)
                {
                    Debug.debug("CSerialFRENIC: exception message:" + e.Message);
                }
            }

            Thread.Sleep(100);
        }

        if (m_master != null)
            m_master.Dispose();
    }

    private void writeSingleRegister(int slave, ushort address, ushort value)
    {
        if (m_simulation)
            return;

        try
        {
            m_master.WriteSingleRegister((byte)slave, address, value);

            Debug.debug("CSerialFRENIC: write slave:" + slave + " addr:" + address + " value:" + value);
        }
        catch (Exception ex)
        {
            Debug.debug("CSerialFRENIC: write error slave:" + slave + " addr:" + address + " err:" + ex.Message);
        }
    }

    public void cvRun(int slave)
    {
        writeSingleRegister(slave, 0x0706, 1);
    }

    public void cvReverseRun(int slave)
    {
        writeSingleRegister(slave, 0x0706, 2);
    }

    public void cvStop(int slave)
    {
        writeSingleRegister(slave, 0x0706, 0);
    }

    public void setFrequency(int slave, double hz)
    {
        if (hz < 0) 
            hz = 0;

        ushort value = (ushort)((hz * 20000.0d) / 60.0d);
        writeSingleRegister(slave, 0x0701, value);
    }

    public bool isConnect()
    {
        return m_port.IsOpen;
    }

    public double freq(int index)
    {
        if (index > m_freq.Length - 1)
            return -1.0d;
 
        return m_freq[index];
    }
    
    public double setFreq(int index)
    {
        if (index > m_setFreq.Length - 1)
            return -1.0d;

        return m_setFreq[index];
    }

    public int status(int index)
    {
        if (index > m_status.Length - 1)
            return -1;

        return m_status[index];

    }

}
