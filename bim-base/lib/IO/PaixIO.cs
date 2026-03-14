using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Paix_MotionController;
using PAIX_NMF_DEV;

public class PaixIO
{
    string m_ip = "";
    short m_no = 0;

    bool m_isConnect = false;

    public PaixIO(string ip)
    {
        m_ip = ip;

        string[] split = m_ip.Split('.');

        if (split.Length != 4)
            throw new InvalidCastException("PaixIO::PaixIO invalid ip");

        m_no = Convert.ToInt16(split[3]);
    }

    public bool isConnected()
    {
        return m_isConnect;
    }

    public bool pingCheck()
    {
        NMC2.nmc_SetIPAddress(m_no, 192, 168, 0);

        if (NMC2.nmc_PingCheck(m_no, 100) != 0)
            m_isConnect = false;
        else
            m_isConnect = true;

        return m_isConnect;
    }

    public bool open()
    {
        string[] splitStr = m_ip.Split('.');
        short[] ipAddr = new short[4];

        for (int i = 0; i < 4; i++)
        {
            ipAddr[i] = Convert.ToInt16(splitStr[i]);
        }

        NMC2.nmc_SetIPAddress(m_no, ipAddr[0], ipAddr[1], ipAddr[2]);

        if (NMC2.nmc_PingCheck(m_no, ipAddr[3]) != 0)
        {
            Debug.warning("PaixIO::open ping check error. ip:" + m_ip);
            return false;
        }

        if (NMC2.nmc_OpenDevice(m_no) == 0)
            m_isConnect = true;
        else
            m_isConnect = false;

        return m_isConnect;
    }

    public bool close()
    {
        NMC2.nmc_CloseDevice(m_no);
        m_isConnect = false;

        return true;
    }

    public void setOutPutTog(short bitNo)
    {
        if (!m_isConnect) return;

        NMC2.nmc_SetDIOOutputTog(m_no, bitNo);
    }

    public void setOutPut(short bitNo, bool _on)
    {
        if (!m_isConnect) return;

        //Debug.debug("Paix::IO bitNo:" + bitNo + " value:" + _on);

        short on = Convert.ToInt16(_on);

        NMC2.nmc_SetDIOOutputBit(m_no, bitNo, on);
    }

    public bool updateIn(ref short[] inputArray)
    {
        if (m_isConnect == false) return false;
        short nret;

        nret = NMC2.nmc_GetDIOInput128(m_no, inputArray);

        if (nret != 0)
            return false;

        return true;
    }

    public bool updateOut(ref short[] outputArray)
    {
        if (m_isConnect == false) return false;
        short nret;

        nret = NMC2.nmc_GetDIOOutput128(m_no, outputArray);

        if (nret != 0)
            return false;

        return true;
    }

    public bool updateOutIn(ref short[] inputArray, ref short[] outputArray)
    {
        if (m_isConnect == false) return false;
        short nret;

        nret = NMC2.nmc_GetDIOInput128(m_no, inputArray);

        if (nret != 0)
            return false;

        nret = NMC2.nmc_GetDIOOutput128(m_no, outputArray);

        if (nret != 0)
            return false;

        return true;
    }


    public bool updateIn(ref bool[] inputArray)
    {
        short[] inArray = new short[inputArray.Length];

        int inputCount = inputArray.Length;

        bool ret = updateIn(ref inArray);

        if (ret == false)
            m_isConnect = false;

        for (int i = 0; i < inArray.Length; i++)
        {
            inputArray[i] = (inArray[i] == 1) ? true : false;
        }

        return true;
    }


    public bool updateOut(ref bool[] outputArray)
    {
        short[] outArray = new short[outputArray.Length];

        int outputCount = outputArray.Length;

        bool ret = updateOut(ref outArray);

        if (ret == false)
            m_isConnect = false;

        for (int i = 0; i < outArray.Length; i++)
        {
            outputArray[i] = (outArray[i] == 1) ? true : false;
        }

        return true;
    }

    public bool updateOutIn(ref bool[] inputArray, ref bool[] outputArray)
    {
        short[] inArray = new short[inputArray.Length];
        short[] outArray = new short[outputArray.Length];

        int inputCount = inputArray.Length;
        int outputCount = outputArray.Length;

        updateOutIn(ref inArray, ref outArray);

        for (int i = 0; i < inArray.Length; i++)
        {
            inputArray[i] = (inArray[i] == 1) ? true : false;
        }

        for (int i = 0; i < outArray.Length; i++)
        {
            outputArray[i] = (outArray[i] == 1) ? true : false;
        }

        return true;
    }
}
