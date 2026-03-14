using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

#if USE_COMI_SSCNET
using ComiDll;

public enum COMI_DEVICE_ID
{
    // CP-Seriese
    CP101 = 0xC101,
    CP201 = 0xC201,
    CP301 = 0xC301,
    CP302 = 0xC302,
    CP401 = 0xC401,
    CP501 = 0xC501,
    SD101 = 0xB101,
    // SD-Seriese
    SD102 = 0xB102,
    SD103 = 0xB103,
    SD104 = 0xB104,
    SD201 = 0xB201,
    SD202 = 0xB202,
    SD203 = 0xB203,
    SD301 = 0xB301,
    SD302 = 0xB302,
    SD401 = 0xB401,
    SD402 = 0xB402,
    SD403 = 0xB403,
    SD404 = 0xB404,
    SD414 = 0xB414,
    SD424 = 0xB424,
    SD434 = 0xB434,
    SD501 = 0xB501,
    SD502 = 0xB502,
    LX101 = 0xA101,
    // LX-Seriese
    LX102 = 0xA102,
    LX103 = 0xA103,
    LX201 = 0xA201,
    LX202 = 0xA202,
    LX203 = 0xA203,
    LX301 = 0xA301,
    LX401 = 0xA401,
    LX402 = 0xA402,
}

public class ComiInput
{
    int deviceId = 0;
    int deviceNo = 0;
    IntPtr[] openPtr = new IntPtr[4];
    
    public ComiInput(int id, int no)
    {
        deviceId = id;
        deviceNo = no;
    }

    public bool open()
    {
        openPtr[deviceNo] = CMMSDK.COMI_LoadDevice(deviceId, deviceNo);

        if ((int)openPtr[deviceNo] == -1)
        {
            return false;
        }
        return true;
    }

    public void close()
    {
        CMMSDK.COMI_UnloadDevice(openPtr[deviceNo]);
    }

    public bool updateInput(ref bool[] inputArray, int groupCount)
    {
        long inputStatue = 0;

        for (int i = 0; i < groupCount; i++)
        {
            inputStatue = CMMSDK.COMI_DI_GetAllEx(openPtr[deviceNo], i);
            if (inputStatue == -1)
                return false;

            for (int j = 0; j < 32; j++)
            {
                int index = j + (32 * i);
                inputArray[index] = (inputStatue >> j & 0x01) == 0 ? false : true;
            }
        }

        return true;
    }
}
#endif