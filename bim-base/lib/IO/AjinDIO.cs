using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using static CNetworkPF;

public class AjinDIO : BaseDIO
{
    public enum TYPE
    {
        INPUT,
        OUTPUT,
    }

    bool m_simualtion = false;

    int[] m_no = null;

    bool[] m_input = null;
    bool[] m_output = null;

    public AjinDIO(TYPE type, int[] boardIdList)
    {
        string path = Common.PATH + "\\simulation";

        if (File.Exists(path))
            m_simualtion = true;

        m_no = boardIdList;
        int count = 16 * m_no.Length;

        if (type == TYPE.INPUT)
            m_input = new bool[count];

        if (type == TYPE.OUTPUT)
            m_output = new bool[count];
    }

    public override bool[] allInput()
    {
        if (m_simualtion)
            return m_input;

        for (int i = 0; i < m_no.Length - 1; i++)
        {
            uint val = 0;

            CAXD.AxdiReadInportDword(m_no[i], 0, ref val);

            bool[] inputArr = new bool[32];
            Util.wordToBit(val, ref inputArr, 0);

            for (int j = 0; j < 16; j++)
            {
                int addr = (i * 16) + j;
                m_input[addr] = inputArr[j];
            }
        }

        return m_input;
    }

    public override bool[] allOutput()
    {
        if (m_simualtion)
            return m_output;

        for (int i = 0; i < m_no.Length; i++)
        {
            uint val = 0;

            CAXD.AxdoReadOutportDword(m_no[i], 0, ref val);
            Util.wordToBit(val, ref m_output, i);
        }

        return m_output;
    }

    public override bool input(int index)
    {
        return m_input[index];
    }

    public override bool output(int index)
    {
        return m_output[index];
    }

    public override void setOutput(int index, bool value)
    {
        if (index > (m_output.Length - 1))
            return;

        m_output[index] = value;

        uint val = (value == true) ? (uint)1 : 0;

        int id = m_no[(index / 16)];
        int valueAddr = (index % 16);

        CAXD.AxdoWriteOutportBit(id, valueAddr, val);
    }

    public override void setOutput(bool[] valueArray)
    {
        for (int i = 0; i < m_output.Length; i++)
        {
            m_output[i] = valueArray[i];
        }

        if (m_simualtion)
        {
            return;
        }

        for (int i = 0; i < m_no.Length; i++)
        {
            int id = m_no[i];
            int index = i * 16;

            bool[] value = new bool[32];

            for (int j = 0; j < 16; j++)
            {
                value[j] = valueArray[index + j];
            }

            int val = Util.bitToWord(value, 0);
            uint ret = CAXD.AxdoWriteOutportDword(id, 0, (uint)val);
        }
    }
} // class
