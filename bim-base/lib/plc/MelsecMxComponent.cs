// #define USE_MX_COMPONENT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


#if USE_MX_COMPONENT
using ACTMULTILib;
#endif

/// <summary>
/// with MXComponent
/// </summary>
///     
public class CommPLC
{
    public enum Address
    {
        X = 0,
        Y = 1,
        W = 2,
        B = 3,
        D = 4
    }

#if USE_MX_COMPONENT
    private ActEasyIF ActEasyIF1 = null;
#endif

    private bool m_connected = false;

    public CommPLC(int logicalStationNo = 0)
    {
#if USE_MX_COMPONENT
        ActEasyIF1 = new ActEasyIF();
        ActEasyIF1.ActLogicalStationNumber = logicalStationNo;
#endif

        connect();
    }

    public void connect()
    {
#if USE_MX_COMPONENT
        long ret = ActEasyIF1.Open();

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
#if USE_MX_COMPONENT
        if (m_connected == false)
        {
            connect();
            return false;
        }

        string readDevice = addressEnum.ToString() + address;

        try
        {
            long ret = ActEasyIF1.ReadDeviceBlock(readDevice, cnt, out array[0]);
            //long ret = ActEasyIF1.ReadDeviceRandom
            
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
#if USE_MX_COMPONENT
        if (m_connected == false)
        {
            Debug.debug("################## MelsecMxComponent::write m_connected == false");
            connect();
            return false;
        }

        string writeDevice = addressEnum.ToString() + address;

        try
        {
            long ret = ActEasyIF1.WriteDeviceBlock(writeDevice, cnt, ref array[0]);

            if (ret != 0)
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.debug("################## MelsecMxComponent::write exception: " + ex);
            m_connected = false;
            return false;
        }
#endif
        return true;
    }
}
