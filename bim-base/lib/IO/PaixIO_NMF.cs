using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using PAIX_NMF_DEV;

public class PaixIO_NMF
{
    string m_ip = "";
    short m_no = 0;

    public bool m_isConnect = false;

    public PaixIO_NMF(string ip)
    {
        m_ip = ip;

        string[] split = m_ip.Split('.');

        if (split.Length != 4)
            throw new InvalidCastException("PaixIO_NMF::PaixIO_NMF invalid ip");

        m_no = Convert.ToInt16(split[3]);
    }

    public bool isConnected()
    {        
        return m_isConnect;
    }

    public bool pingCheck()
    {
        string[] splitStr = m_ip.Split('.'); 
        short[] ipAddr = new short[4];

        for (int i = 0; i < 4; i++)
        {
            ipAddr[i] = Convert.ToInt16(splitStr[i]);
        }

        if (NMF.nmf_PingCheck(m_no, ipAddr[0], ipAddr[1], ipAddr[2], ipAddr[3]) != 0)
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

        if (NMF.nmf_PingCheck(m_no, ipAddr[0], ipAddr[1], ipAddr[2], ipAddr[3]) != 0)
        {
            Debug.warning("PaixIO_NMF::open ping check error. ip:" + m_ip);
            return false;
        }

        if (NMF.nmf_Connect(m_no, ipAddr[0], ipAddr[1], ipAddr[2]) == 0)
            m_isConnect = true;
        else
            m_isConnect = false;

        return m_isConnect;
    }

    public bool close()
    {
        NMF.nmf_Disconnect(m_no);

        return true;
    }


    public void setOutPut(short bitNo, bool _on)
    {
        if (m_isConnect == false) 
            return; 

        //Debug.debug("Paix::IO bitNo:" + bitNo + " value:" + _on);

        short on = Convert.ToInt16(_on);
        NMF.nmf_DOSetPin(m_no, bitNo, on);
    }

    public bool updateOutIn(ref short[] inputArray, ref short[] outputArray)
    {
        if (m_isConnect == false) 
            return false;

        short nret;

        nret = NMF.nmf_DIGet(m_no, inputArray);

        if (nret != 0)
            return false;

        nret = NMF.nmf_DOGet(m_no, outputArray);

        if (nret != 0)
            return false;

        return true;
    }

    short[] inArray = new short[128];
    short[] outArray = new short[128];

    public bool updateOutIn(ref bool[] inputArray, ref bool[] outputArray)
    {
        updateOutIn(ref inArray, ref outArray);

        for (int i = 0; i < inputArray.Length; i++)
        {
            inputArray[i] = (inArray[i] == 1) ? true : false;
            outputArray[i] = (outArray[i] == 1) ? true : false;
        }

        return true;
    }
}
