using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NMCMotionSDK;
using static NMCMotionSDK.NMCSDKLib;

using System.IO;

public class RSAMMCEDIO
{
    int m_id = -1;
    RSAMMCEBoard m_board = null;

    bool m_simulation = false;

    public RSAMMCEDIO(RSAMMCEBoard board, int ecatAddr)
    {
        m_board = board;
        m_id = ecatAddr;

        string path = Common.PATH + "\\simulation";
        m_simulation = File.Exists(path);
    }

    public void readOutputAll(ref bool[] output)
    {
        if (m_simulation == true)
            return;

        int cnt = output.Length;
        int cardCnt = cnt / 8;

        for (int i = 0; i < cardCnt; i++)
        {
            byte value = 0;
            m_board.ioReadByte((ushort)m_id, RSA_IO.OUT, i, ref value);

            Util.toBit(value, output, i * 8);
        }
    }

    public void readInputAll(ref bool[] input)
    {
        if (m_simulation == true)
            return;

        int cnt = input.Length;
        int cardCnt = cnt / 8;

        for (int i = 0; i < cardCnt; i++)
        {
            byte value = 0;
            m_board.ioReadByte((ushort)m_id, RSA_IO.IN, i, ref value);

            Util.toBit(value, input, i * 8);
        }
    }

    public bool setOutput(int index, bool value)
    {
        if (m_simulation == true)
            return true;

        return m_board.ioWriteBit((ushort)m_id, index / 8, index % 8, 1, value);
    }

    public bool isConnectDIO()
    {
        return m_board.isRunMode();
    }
}
