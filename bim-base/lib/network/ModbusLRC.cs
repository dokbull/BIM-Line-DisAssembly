using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ModbusLRC
{
    //==========================================================================//

    //=========================================================================//
    //LRC 를 BYTE 로 리턴하는 함수
    //=========================================================================//

    public static byte makeLRC(byte[] bytes)
    {
        int LRC = 0;
        for (int i = 0; i < bytes.Length; i++)
        {
            LRC -= bytes[i];
        }
        return (byte)LRC;
    }
}
