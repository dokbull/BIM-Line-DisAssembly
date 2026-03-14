using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


#if USE_MX_COMPONENT_UTIL_TYPE
using ActUtlTypeLib;
#endif

/// <summary>
/// with MXComponent
/// </summary>
///     
public class CommPLCUtil
{
    public enum Address
    {
        X = 0,
        Y = 1,
        W = 2,
        B = 3,
        D = 4,
        R = 5,
    }

#if USE_MX_COMPONENT_UTIL_TYPE
    private ActUtlType ActUtilType1 = null;
#endif

    private bool m_connected = false;

    public CommPLCUtil(int logicalStationNo = 0)
    {
#if USE_MX_COMPONENT_UTIL_TYPE
        ActUtilType1 = new ActUtlType();
        ActUtilType1.ActLogicalStationNumber = logicalStationNo;
#endif

        connect();
    }

    public void connect()
    {
#if USE_MX_COMPONENT_UTIL_TYPE
        long ret = ActUtilType1.Open();

        if (ret == 0)
            m_connected = true;
        else
            m_connected = false;
#endif
    }

    public bool isConnected()
    {
        return m_connected;
    }

    public bool read(Address addressEnum, string address, int cnt, ref int[] array)
    {
#if USE_MX_COMPONENT_UTIL_TYPE
        if (m_connected == false)
        {
            connect();
            return false;
        }

        string readDevice = addressEnum.ToString() + address;

        try
        {
            long ret = ActUtilType1.ReadDeviceBlock(readDevice, cnt, out array[0]);
            
            if (ret != 0)
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            m_connected = false;
            return false;
        }
#endif
        return true;
    }

    public bool write(Address addressEnum, string address, int[] array, int cnt)
    {
        return write(addressEnum, address, ref array, cnt);
    }

    public bool write(Address addressEnum, string address, ref int[] array, int cnt)
    {
#if USE_MX_COMPONENT_UTIL_TYPE
        if (m_connected == false)
        {
            Debug.debug("################## MelsecMxComponentUtil::write m_connected == false");
            connect();
            return false;
        }

        string writeDevice = addressEnum.ToString() + address;

        try
        {
            long ret = ActUtilType1.WriteDeviceBlock(writeDevice, cnt, ref array[0]);

            if (ret != 0)
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.debug("################## MelsecMxComponentUtil::write exception: " + ex);
            m_connected = false;
            return false;
        }
#endif
        return true;
    }
}
