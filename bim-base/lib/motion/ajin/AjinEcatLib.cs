using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

public class AjinLib
{
    bool m_simulation = false;

    public AjinLib()
    {
        if (File.Exists(pathUtil.savePath() + "\\simulation"))
            m_simulation = true;
    }

    public bool open()
    {
        uint ret = 0;

        if (m_simulation)
            return true;

        if (isOpen() == false)
        {
            ret = CAXL.AxlOpen(7);

            if (ret != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                string text = "AjinLib::open error. ret:" + ret;
                Debug.warning(text);
                throw new System.Exception(text);
            }
        }

        return true;
    }

    public void close()
    {
        if (isOpen() == false)
            return;

        int ret = CAXL.AxlClose();
        Debug.debug("AjinLib::close ret:" + ret);
    }

    bool isOpen()
    {
        if (m_simulation)
            return false;

        int ret = 0;
        ret = CAXL.AxlIsOpened();

        if (ret == 0)
            return false;

        return true;
    }

    public int getBoardCount()
    {
        int value = 0;
        uint ret = CAXL.AxlGetBoardCount(ref value);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinLib::getBoardCocunt failed. AxlGetBoardCount");
            return 0;
        }

        return value;
    }

    public int getAxisCount()
    {
        int value = 0;
        uint ret = CAXM.AxmInfoGetAxisCount(ref value);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinLib::getAxisCount failed. AxmInfoGetAxisCount");
            return 0;
        }

        return value;
    }


    public int getModuleBoardCount()
    {
        int value = 0;
        uint ret = CAXD.AxdInfoGetModuleCount(ref value);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinLib::getModuleBoardCount failed. AxdInfoGetModuleCount");
            return 0;
        }

        return value;
    }

    public uint getModuleBoardInfo(int no, ref int boardNo, ref int modulePos, ref uint moduleId)
    {
        uint ret = CAXD.AxdInfoGetModule(no, ref boardNo, ref modulePos, ref moduleId);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinLib::getModuleBoardInfo failed. AxdInfoGetModule");
            return 0;
        }

        return ret;
    }

    public bool isConnectDIO()
    {
        uint tempValue = 0;
        uint ret = CAXD.AxdoReadOutport(0, ref tempValue);

        if (ret == (int)AXT_FUNC_RESULT.AXT_RT_NETWORK_ERROR)
            return false;

        if (ret == (int)AXT_FUNC_RESULT.AXT_RT_DIO_INVALID_OFFSET_NO)
            return false;

        return true;
    }

    public bool isConnectAxis(int no)
    {
        double value = 0;
        uint ret = CAXM.AxmStatusGetActPos(no, ref value);

        if (ret != (int)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            return false;

        return true;
    }

    public void allAxisStop()
    {
        int count = getAxisCount();

        for (int i = 0; i < count; i++)
        {
            uint ret = CAXM.AxmMoveSStop(i);
        }
    }

    public bool isAllConnectAxis(int totalAxisNo)
    {
        Debug.debug("AjinLib::isAllConnectAxis totalAxisNo:" + totalAxisNo);
        bool ret = isConnectAxis(totalAxisNo - 1);
        return ret;
    }


    public const uint DPRAM_COMMON_CMD_SIIIH_SCAN = 0x74;
    public uint rescan_SSCNET(int m_lAxisNo)
    {
        int boardCount = 0;
        double cmdPos = 0;
        int oldTick = 0, newTick = 0, tickTime = 0;
        uint[] sendData = new uint[22];
        uint uReturn;
        uint axis0Ret = 0;
        uint axis3Ret = 0;

        CAXL.AxlOpen(7);
        CAXL.AxlOpenNoReset(7);
        CAXL.AxlGetBoardCount(ref boardCount);

        axis0Ret = CAXM.AxmStatusGetCmdPos(0, ref cmdPos);
        axis3Ret = CAXM.AxmStatusGetCmdPos(3, ref cmdPos);

        if ((axis0Ret != 0) || (axis3Ret != 0))
        {
            if (boardCount < 1)
                return (uint)AXT_FUNC_RESULT.AXT_RT_OPEN_ERROR;

            System.Array.Clear(sendData, 0, 22);

            //'AxlSetSendBoardCommand' 함수의 첫 번째 인자는 보드 넘버입니다.
            //PC에 장착한 순서에 따라 보드 넘버는 변경됩니다.
            //EzConfig UC을 통해 보드 넘버를 알 수 있습니다.
            uReturn = CAXDev.AxlSetSendBoardCommand(00, DPRAM_COMMON_CMD_SIIIH_SCAN, sendData, 0);
            oldTick = Environment.TickCount;

            while (true)
            {
                //TimeOut Check
                newTick = Environment.TickCount;

                tickTime = newTick - oldTick;
                if (tickTime > 20000)
                {
                    return (uint)AXT_FUNC_RESULT.AXT_RT_OPEN_ERROR;
                }

                //Network Connect Check
                uReturn = CAXM.AxmStatusGetCmdPos(m_lAxisNo, ref cmdPos);
                if (uReturn == 0)
                {
                    break;
                }
            }
        }
        CAXL.AxlClose();
        CAXL.AxlOpenNoReset(7);
        return (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS;
    }

    public bool infoIsDioModule()
    {
        uint value = 0;
        uint ret = CAXD.AxdInfoIsDIOModule(ref value);

        if (checkFuncResult(ret) == false)
        {
            Debug.debug("AjinLib::infoIsDioModule failed");
            return false;
        }

        if (value == 0)
            return false;

        return true;
    }

    bool checkFuncResult(uint result)
    {
        AXT_FUNC_RESULT ret = (AXT_FUNC_RESULT)result;

        if (ret == AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            return true;

        Debug.warning("AjinLib::checkResult failed to func. reason:" +
            ret.ToString());

        return false;
    }
} // class
