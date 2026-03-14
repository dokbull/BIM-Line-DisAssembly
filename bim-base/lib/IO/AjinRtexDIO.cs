using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

public class AjinRtexDIO
{
    AjinLib lib = null;

    public class AjinRtexIOInfo
    {
        public int no;
        public int pos;
        public AXT_MODULE model;
    }

    List<AjinRtexIOInfo> m_inputList = new List<AjinRtexIOInfo>();
    List<AjinRtexIOInfo> m_outputList = new List<AjinRtexIOInfo>();

    public AjinRtexDIO(AjinLib _lib)
    {
        lib = _lib;

        int count = lib.getModuleBoardCount();

        for (int i = 0; i < count; i++)
        {
            int no = -1;
            int pos = -1;
            uint id = 0;

            if (lib.getModuleBoardInfo(i, ref no, ref pos, ref id) == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                AXT_MODULE model = (AXT_MODULE)id;

                AjinRtexIOInfo board = new AjinRtexIOInfo();
                board.no = no;
                board.pos = pos;
                board.model = model;

                switch (model)
                {
                    case AXT_MODULE.AXT_SIO_RDO32RTEX:

                        m_outputList.Add(board);
                        break;

                    case AXT_MODULE.AXT_SIO_RDI32RTEX:

                        m_inputList.Add(board);
                        break;
                    default: break;
                }
            }
        }
    }

    public int inputBoardCount()
    {
        int count = m_inputList.Count();
        return count;
    }

    public int outputBoardCount()
    {
        int count = m_outputList.Count();
        return count;
    }

    public bool input(int index)
    {
        int list = m_inputList.Count();

        if (index > ((list * 32) - 1))
            return false;

        int id = (int)(index / 32);
        int addr = (int)(index % 32);

        uint val = 0;

        AjinRtexIOInfo info = m_inputList[id];

        CAXD.AxdiReadInportBit(info.pos, addr, ref val);

        if (val == 1)
            return true;

        return false;
    }

    public void readInputAll(ref bool[] inputList)
    {
        int boardCount = (inputList.Length / 32);

        int offset = 0;

        for (int i = 0; i < boardCount; i++)
        {
            if (i > (m_inputList.Count() - 1))
                continue;

            uint val = 0;
            bool[] input = new bool[32];

            AjinRtexIOInfo info = m_inputList[i];

            CAXD.AxdiReadInportDword(info.pos, 0, ref val);
            Util.wordToBit(val, ref input, 0);

            for (int j = 0; j < input.Length; j++)
            {
                inputList[offset] = input[j];
                offset++;
            }
        }
    }

    public void readOutputAll(ref bool[] outputList)
    {
        int boardCount = (outputList.Length / 32);

        int offset = 0;

        for (int i = 0; i < boardCount; i++)
        {
            if (i > (m_outputList.Count() - 1))
                continue;

            uint val = 0;
            bool[] input = new bool[32];

            AjinRtexIOInfo info = m_outputList[i];

            CAXD.AxdoReadOutportDword(info.pos, 0, ref val);
            Util.wordToBit(val, ref input, 0);

            for (int j = 0; j < input.Length; j++)
            {
                outputList[offset] = input[j];
                offset++;
            }
        }
    }


    public bool output(int index)
    {
        int list = m_outputList.Count();

        if (index > ((list * 32) - 1))
            return false;

        int id = (int)(index / 32);
        int addr = (int)(index % 32);

        uint val = 0;

        AjinRtexIOInfo info = m_outputList[id];

        CAXD.AxdoReadOutportBit(info.pos, addr, ref val);

        if (val == 1)
            return true;

        return false;
    }

    public void setOutput(int index, bool value)
    {
        int list = m_outputList.Count();

        if (index > ((list * 32) - 1))
            return;

        int id = (int)(index / 32);
        int addr = (int)(index % 32);

        uint val = 0;

        if (value)
            val = 1;

        AjinRtexIOInfo info = m_outputList[id];

        uint ret = CAXD.AxdoWriteOutportBit(info.pos, index, val);

        AXT_FUNC_RESULT retStr = (AXT_FUNC_RESULT)ret;

        if (retStr != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
        {
            Debug.debug("AjinRtexDIO::setOutput fail. message" + retStr.ToString());
        }
    }
}
