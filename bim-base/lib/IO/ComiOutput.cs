using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

#if USE_COMI_SSCNET
using ComiDll;

public class ComiOutput
{
    int deviceId = 0;
    int deviceNo = 0;
    IntPtr[] openPtr = new IntPtr[4];

    public ComiOutput(int id, int no)
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

    public bool setOutPut(int channel, bool status)
    {
        bool ret = false;
        int on = Convert.ToInt16(status);
        ret = CMMSDK.COMI_DO_PutOne(openPtr[deviceNo], channel, on);

        if (status == true)
            return ret;

        return !ret; // 동작시 true, 정지시 false 반환
    }
}
#endif