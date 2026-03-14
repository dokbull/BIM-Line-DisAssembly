using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if USE_S7
using Sharp7;
#endif

public class CSimensePLC
{
#if USE_S7
    S7Client m_client = new S7Client();
#endif

    string m_ip;
    int m_rackNo;
    int m_slotNo;

    bool m_isConnected = false;

    public bool isConnected()
    {
        return m_isConnected;
    }

    public bool connect(string ip, int rackNo, int slotNo)
    {
        Debug.debug("CSimensePLC::connect ip:" + ip + " rackNo:" + rackNo + " slotNo:" + slotNo);

        m_ip = ip;
        m_rackNo = rackNo;
        m_slotNo = slotNo;

#if USE_S7
        int ret = m_client.ConnectTo(ip, rackNo, slotNo);

        if (ret != 0)
        {
            Debug.debug("CSimensePLC::connect is failed. ret:" + ret + " text:" + m_client.ErrorText(ret));
            m_isConnected = false;
            return false;
        }

        m_isConnected = true;

        return true;
#else
        return false;
#endif
    }

    public bool reconnect()
    {
        return connect(m_ip, m_rackNo, m_slotNo);
    }

    public void disconnect()
    {
#if USE_S7
        m_client.Disconnect();
        m_isConnected = true;
#endif
    }

    public bool read(int dbNo, int start, int count, byte[] buff)
    {
#if USE_S7
        int ret = m_client.DBRead(dbNo, start, count, buff);

        if (ret != 0)
        {
            Debug.debug("CSimensePLC::read failed. dbNo:" + dbNo + " start:" + start + " count:" + count + " ret:" + ret + " errorText:" + m_client.ErrorText(ret));
            m_isConnected = false;
            return false;
        }

        return true;
#else
        return false;
#endif


    }

    public bool write(int dbNo, int start, int count, byte[] buff)
    {
#if USE_S7
        int ret = m_client.DBWrite(dbNo, start, count, buff);

        if (ret != 0)
        {
            Debug.debug("CSimensePLC::write failed. dbNo:" + dbNo + " start:" + start + " count:" + count);
            m_isConnected = false;
            return false;
        }

        return true;
#else
        return false;
#endif
    }
}