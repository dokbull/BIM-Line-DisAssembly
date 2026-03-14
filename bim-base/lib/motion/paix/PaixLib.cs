using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paix_MotionController;

public class PaixLib
{
    short m_no = 0;

    PAIX_ACCEL_UNIT m_accelUnit = PAIX_ACCEL_UNIT.MM_PER_SECOND;

    public PaixLib(int no)
    {
        m_no = (short)no;
    }

    public int no()
    {
        return m_no;
    }

    public bool open()
    {
        return openDevice();
    }

    public void setAccelUnit(PAIX_ACCEL_UNIT unit)
    {
        m_accelUnit = unit;
    }

    public void close()
    {
    }

    public void setIPAddress(int classA, int classB, int classC)
    {
        NMC2.nmc_SetIPAddress((short)m_no, (short)classA, (short)classB, (short)classC);
    }

    public bool writeIPAddress(int[] ipAddr, int[] subnetAddr, int gatewayClassD)
    {
        short[] ip = new short[] { (short)ipAddr[0], (short)ipAddr[1], (short)ipAddr[2] };
        short[] subnet = new short[] { (short)subnetAddr[0], (short)subnetAddr[1], (short)subnetAddr[2] };

        short ret = NMC2.nmc_WriteIPAddress(m_no, ip, subnet, (short)gatewayClassD);

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixLib::writeIPAddress failed. no:" + m_no);
            return false;
        }

        return true;
    }

    public bool pingCheck(int waitTime)
    {
        short ret = NMC2.nmc_PingCheck(m_no, waitTime);

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixLib::pingCheck failed. no:" + m_no);
            return false;
        }

        return true;
    }

    public bool openDevice()
    {
        short ret = NMC2.nmc_OpenDevice(m_no);

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixLib::openDevice failed. no:" + m_no);
            return false;
        }

        return true;
    }

    public bool openDeviceEx(int waitTime)
    {
        short ret = NMC2.nmc_OpenDeviceEx(m_no, waitTime);

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixLib::openDeviceEx failed. no:" + m_no);
            return false;
        }

        return true;
    }

    public bool closeDevice()
    {
        NMC2.nmc_CloseDevice(m_no);
        return true;
    }

    public bool setDisconnectedStopMode(int timeInterval, PAIX_STOPMODE stopMode)
    {
        short ret = NMC2.nmc_SetDisconnectedStopMode(m_no, timeInterval, (short)stopMode);

        if (checkFuncRet(ret))
        {
            Debug.warning("PaixLib::setDisconnectedStopMode failed. no:" + m_no);
            return false;
        }

        return true;
    }

    public bool setEmgLogic(int groupNo, PAIX_LEVEL level)
    {
        short ret = NMC2.nmc_SetEmgLogic(m_no, (short)groupNo, (short)level);

        if (checkFuncRet(ret))
        {
            Debug.warning("PaixLib::setEmgLogic failed. no:" + m_no + " groupNo:" + groupNo);
            return false;
        }

        return true;
    }

    public bool setEmgEnable(bool enable)
    {
        int value = (enable == true) ? 1 : 0;

        short ret = NMC2.nmc_SetEmgEnable(m_no, (short)value);

        if (checkFuncRet(ret))
        {
            Debug.warning("PaixLib::setEmgEnable failed. no:" + m_no);
            return false;
        }

        return true;
    }

    public bool setParaLogicFile(string text)
    {
        byte[] data = ASCIIEncoding.ASCII.GetBytes(text);
        short ret = NMC2.nmc_SetParaLogicFile(m_no, data);

        if (checkFuncRet(ret))
        {
            Debug.warning("PaixLib::setParaLogicFile failed. no:" + m_no);
            return false;
        }

        return true;
    }

    // 11. 다축 - 모션 전용 출력 신호
    public bool setMultiCurrentOn(int[] axisArr, bool value)
    {
        short[] axisArray = new short[axisArr.Length];

        for (int i=0; i<axisArr.Length; i++)
            axisArray[i] = (short)axisArr[i];

        int val = value ? 1 : 0;
        short ret = NMC2.nmc_SetMultiCurrentOn(m_no, (short)axisArray.Length, axisArray, (short)val);

        if (checkFuncRet(ret))
        {
            Debug.warning("PaixLib::setMultiCurrentOn failed. no:" + m_no);
            return false;
        }

        return true;
    }

    public bool setMultiServoOn(int[] axisArr, bool value)
    {
        short[] axisArray = new short[axisArr.Length];

        for (int i = 0; i < axisArr.Length; i++)
            axisArray[i] = (short)axisArr[i];

        int val = value ? 1 : 0;
        short ret = NMC2.nmc_SetMultiServoOn(m_no, (short)axisArray.Length, axisArray, (short)val);

        if (checkFuncRet(ret))
        {
            Debug.warning("PaixLib::setMultiServoOn failed. no:" + m_no);
            return false;
        }

        return true;
    }

    public bool setMultiAlarmResetOn(int[] axisArr, bool value)
    {
        short[] axisArray = new short[axisArr.Length];

        for (int i = 0; i < axisArr.Length; i++)
            axisArray[i] = (short)axisArr[i];

        int val = value ? 1 : 0;
        short ret = NMC2.nmc_SetMultiAlarmResetOn(m_no, (short)axisArray.Length, axisArray, (short)val);

        if (checkFuncRet(ret))
        {
            Debug.warning("PaixLib::setMultiAlarmResetOn failed. no:" + m_no);
            return false;
        }

        return true;
    }

    public bool setMultiDccOn(int[] axisArr, bool value)
    {
        short[] axisArray = new short[axisArr.Length];

        for (int i = 0; i < axisArr.Length; i++)
            axisArray[i] = (short)axisArr[i];

        int val = value ? 1 : 0;
        short ret = NMC2.nmc_SetMultiDccOn(m_no, (short)axisArray.Length, axisArray, (short)val);

        if (checkFuncRet(ret))
        {
            Debug.warning("PaixLib::setMultiDccOn failed. no:" + m_no);
            return false;
        }

        return true;
    }

    // 15. 다축 이동
    public bool varAbsMove(int[] axisArr, double[] posArr)
    {
        short ret = NMC2.nmc_VarAbsMove(m_no, (short)axisArr.Length,
            convertIntArr(axisArr), posArr);

        if (checkFuncRet(ret))
        {
            Debug.warning("PaixLib::varAbsMove failed. no:" + m_no);
            return false;
        }

        return true;
    }

    public bool varRelMove(int[] axisArr, double[] posArr)
    {
        short ret = NMC2.nmc_VarRelMove(m_no, (short)axisArr.Length,
            convertIntArr(axisArr), posArr);

        if (checkFuncRet(ret))
        {
            Debug.warning("PaixLib::varRelMove failed. no:" + m_no);
            return false;
        }

        return true;
    }

    public bool getBusyStatusAll(ref bool[] arr)
    {
        short[] value = new short[8];
        short ret = NMC2.nmc_GetBusyStatusAll(m_no, value);

        if (checkFuncRet(ret))
        {
            Debug.warning("PaixLib::getBusyStatusAll failed. no:" + m_no);
            return false;
        }

        for (int i = 0; i < 8; i++)
        {
            arr[i] = (value[i] == 1) ? true : false;
        }

        return true;
    }

    public bool multiAxisStop(int[] axisArr, PAIX_STOPMODE mode)
    {
        short ret = NMC2.nmc_MultiAxisStop(m_no, (short)axisArr.Length,
            convertIntArr(axisArr), (short)mode);

        if (checkFuncRet(ret))
        {
            Debug.warning("PaixLib::multiAxisStop failed. no:" + m_no);
            return false;
        }
        return true;
    }

    public bool allAxisStop(PAIX_STOPMODE mode)
    {
        short ret = NMC2.nmc_AllAxisStop(m_no, (short)mode);

        if (checkFuncRet(ret))
        {
            Debug.warning("PaixLib::allAxisStop failed. no:" + m_no);
            return false;
        }
        return true;
    }

    public bool setHomeDelay(int delay)
    {
        short ret = NMC2.nmc_SetHomeDelay(m_no, delay);

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixLib::setHomeDelay failed. no:" + m_no);
            return false;
        }
        return true;
    }

    public bool getHomeStatus(out NMC2.NMCHOMEFLAG flag)
    {
        short ret = NMC2.nmc_GetHomeStatus(m_no, out flag);

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixLib::getHomeStatus failed. no:" + m_no);
            return false;
        }
        return true;
    }

    short[] convertIntArr(int[] arr)
    {
        short[] result = new short[arr.Length];

        for (int i = 0; i < arr.Length; i++)
        {
            result[i] = (short)arr[i];
        }

        return result;
    }

    //21. 보간이동
    //22. 연속보간
    public bool contiSetNodeClear(int groupNo)
    {
        Debug.debug("PaixLib::contiSetNodeClear no:" + m_no + " groupNo:" + groupNo);
        short ret = NMC2.nmc_ContiSetNodeClear(m_no, (short)groupNo);

        if (checkFuncRet(ret) == false)
        {
            throw new Exception("");
        }

        m_nodeCount[groupNo] = 0;

        return true;
    }

    public bool contiSetMode(int groupNo, bool AVTRIMode, bool emptyWait,
        int axis1No, int axis2No, int axis3No, double vel)
    {
        short ret = NMC2.nmc_ContiSetMode(m_no, (short)groupNo,
            (short)(AVTRIMode ? 1 : 0), (short)(emptyWait ? 1 : 0),
            (short)axis1No, (short)axis2No, (short)axis3No,
            vel, 2, 0, 0);

        if (checkFuncRet(ret) == false)
        {
            throw new Exception("");
        }
        return true;
    }

    int[] m_nodeCount = new int[2];

    public bool contiGetStatus(ref PAIX_CONTI_STATUS status)
    {
        NMC2.NMCCONTISTATUS structStatus = new NMC2.NMCCONTISTATUS();
        short ret = NMC2.nmc_ContiGetStatus(m_no, out structStatus);

        for (int i = 0; i < 2; i++)
        {
            status.group[i].isRun = structStatus.nContiRun[i] == 1 ? true : false;
            status.group[i].isWait = structStatus.nContiWait[i] == 1 ? true : false;
            status.group[i].nowNodeNo = (int)structStatus.uiContiExecutionNum[i];
            status.group[i].remainBufferCount = (int)structStatus.nContiRemainBuffNum[i];
            status.group[i].stopReadon = structStatus.nContiStopReason[i];
        }

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixLib::contiGetStatus failed. no:" + m_no);
            return false;
        }
        return true;
    }

    public bool contiAddNodeLine2Axis(int groupNo, double pos0, double pos1,
        double startVel, double acc, double dec, double driveVel)
    {
#if false
        Debug.debug("PaixLib::contiAddNodeLine2Axis no:" + m_no + " groupNo:" + groupNo +
            " pos0:" + pos0.ToString("0.000") +
            " pos1:" + pos1.ToString("0.000") +
            " startVel:" + startVel.ToString("0.000") +
            " acc:" + acc.ToString("0.000") +
            " dec:" + dec.ToString("0.000") +
            " driveVel:" + driveVel.ToString("0.000"));
#endif

        short ret = NMC2.nmc_ContiAddNodeLine2Axis(m_no, (short)groupNo, pos0, pos1, 
            startVel, acc, dec, driveVel, 0);

        m_nodeCount[groupNo]++;

        if (checkFuncRet(ret) == false)
        {
            throw new Exception("");
        }
        return true;
    }

    public bool contiAddNodeArc(int groupNo, double centerX, double centerY, double angle, 
        double startVel, double acc, double dec, double driveVel, int nIOCtrl = -1)
    {
        Debug.debug("PaixLib::contiAddNodeArc no:" + m_no + " groupNo:" + groupNo +
            " centerX:" + centerX.ToString("0.000") +
            " centerY:" + centerY.ToString("0.000") +
            " angle:" + angle.ToString("0.000") +
            " startVel:" + startVel.ToString("0.000") +
            " acc:" + acc.ToString("0.000") +
            " dec:" + dec.ToString("0.000") +
            " driveVel:" + driveVel.ToString("0.000"));

        short ret = NMC2.nmc_ContiAddNodeArc(m_no, (short)groupNo, centerX, centerY, angle,
            startVel, acc, dec, driveVel, nIOCtrl);

        m_nodeCount[groupNo]++;

        if (checkFuncRet(ret) == false)
        {
            throw new Exception("");
        }
        return true;
    }

    public bool contiAddNodeArcCE(int groupNo, double centerX, double centerY, double endX, double endY, PAIX_DIR dir,
        double startVel, double acc, double dec, double driveVel, int nIOCtrl = -1)
    {
        Debug.debug("PaixLib::contiAddNodeArcCE no:" + m_no + " groupNo:" + groupNo +
            " centerX:" + centerX.ToString("0.000") +
            " centerY:" + centerY.ToString("0.000") +
            " endX:" + endX.ToString("0.000") +
            " endY:" + endY.ToString("0.000") +
            " startVel:" + startVel.ToString("0.000") +
            " acc:" + acc.ToString("0.000") +
            " dec:" + dec.ToString("0.000") +
            " driveVel:" + driveVel.ToString("0.000"));

        short ret = NMC2.nmc_ContiAddNodeArcCE(m_no, (short)groupNo, centerX, centerY, endX, endY, (short)dir,
            startVel, acc, dec, driveVel, nIOCtrl);

        m_nodeCount[groupNo]++;

        if (checkFuncRet(ret) == false)
        {
            throw new Exception("");
        }
        return true;
    }

    public bool interpolation3Axis(int axis0, int axis1, int axis2, double pos0, double pos1, double pos2)
    {
        short ret = NMC2.nmc_Interpolation3Axis(m_no,
            (short)axis0, pos0,
            (short)axis1, pos1,
            (short)axis2, pos2, 1); // 0 : 상대이동

        if (checkFuncRet(ret) == false)
        {
            throw new Exception("");
        }
        return true;
    }

    //3축 직선 연속 보간
    public bool contiAddNodeLine3Axis(int groupNo, double pos0, double pos1, double pos2,
        double startVel, double acc, double dec, double driveVel)
    {
        double calcAcc = acc;
        double calcDec = dec;

        if (m_accelUnit == PAIX_ACCEL_UNIT.SECOND)
        {
            calcAcc = (1.0d / acc) * driveVel;
            calcDec = (1.0d / dec) * driveVel;
        }

#if true
        Debug.debug("PaixLib::contiAddNodeLine3Axis no:" + m_no + " groupNo:" + groupNo +
            " p0:" + pos0.ToString("0.000") + 
            " p1:" + pos1.ToString("0.000") + 
            " p2:" + pos2.ToString("0.000") +
            " start:" + startVel.ToString("0.000") +
            " acc:" + acc.ToString("0.000") + 
            " dec:" + dec.ToString("0.000") + 
            " drive:" + driveVel.ToString("0.000"));
#endif

        short ret = NMC2.nmc_ContiAddNodeLine3Axis(m_no, (short)groupNo, pos0, pos1, pos2,
            startVel, driveVel * 4000, driveVel * 4000, driveVel, 0);

        m_nodeCount[groupNo]++;

        //PAIX_CONTI_STATUS status = new PAIX_CONTI_STATUS();
        //contiGetStatus(ref status);

        if (checkFuncRet(ret) == false)
        {
            throw new Exception("");
        }
        return true;
    }
    //4축 직선 보간
    //22.10 원호 노드 등록
    //22.11 헬리컬 노드 등록

    public bool contiSetCloseNode(int groupNo)
    {
        Debug.debug("PaixLib::contiSetCloseNode no:" + m_no + " groupNo:" + groupNo);
        short ret = NMC2.nmc_ContiSetCloseNode(m_no, (short)groupNo);

        if (checkFuncRet(ret) == false)
        {
            throw new Exception("");
        }
        return true;
    }

    public bool contiRunStop(int groupNo, bool run)
    {
        Debug.debug("PaixLib::contiRunStop no:" + m_no + " groupNo:" + groupNo + " run:" + run);
        short ret = NMC2.nmc_ContiRunStop(m_no, (short)groupNo, (short)(run ? 1 : 0));

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixLib::contiRunStop failed. no:" + m_no);
            return false;
        }
        return true;
    }

    // 25.6 GANTRY 구동
    public bool setGantryAxis(int groupNo, int mainAxisNo, int slaveAxisNo)
    {
        short ret = NMC2.nmc_SetGantryAxis(m_no, (short)groupNo,
            (short)mainAxisNo, (short)slaveAxisNo);

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixLib::setGantryAxis failed. no:" + m_no + " groupNo:" + groupNo);
            return false;
        }
        return true;
    }

    public bool getGantryInfo(PAIX_GANTRY_INFO info)
    {
        short[] arr = new short[2];
        short ret = NMC2.nmc_GetGantryInfo(m_no,
            arr, info.masterAxisNo, info.slaveAxisNo);

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixLib::getGantryInfo failed. no:" + m_no);
            return false;
        }

        for (int i = 0; i < 2; i++)
        {
            info.use[i] = (arr[i] == 1) ? true : false;
        }

        return true;
    }

    public bool setGantryEnable(int groupNo, bool enable)
    {
        short value = (short)(enable ? 1 : 0);
        short ret = NMC2.nmc_SetGantryEnable(m_no, (short)groupNo, value);

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixLib::setGantryEnable failed. no:" + m_no + " groupNo:" + groupNo);
            return false;
        }
        return true;
    }

    bool[] m_isContiRun = new bool[4];

    public bool isContiRun(int groupNo)
    {
        return m_isContiRun[groupNo];
    }

    public bool getAxesExpress(out NMC2.NMCAXESEXPR status)
    {
        short ret = NMC2.nmc_GetAxesExpress(m_no, out status);

        for (int i = 0; i < 2; i++)
        {
            m_isContiRun[i] = status.nContStatus[i] == 1 ? true : false;
        }

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixLib::getAxesExpress failed. no:" + m_no);
            return false;
        }
        return true;
    }

    public bool getAxesMotionOut(out NMC2.NMCAXESMOTIONOUT status)
    {
        short ret = NMC2.nmc_GetAxesMotionOut(m_no, out status);

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixLib::getAxesMotionOut failed. no:" + m_no);
            return false;
        }

        return true;
    }

    public bool getStateInfo(out NMC2.NMCSTATEINFO status)
    {
        short ret = NMC2.nmc_GetStateInfo(m_no, out status, 1);

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixLib::getStateInfo failed. no:" + m_no);
            return false;
        }

        return true;
    }

    public bool checkFuncRet(short ret)
    {
        PAIX_RET retEnum = (PAIX_RET)ret;

        if (retEnum == PAIX_RET.NMC_OK)
            return true;

        Debug.warning("PaixLib::checkFuncRet failed. ret:" + retEnum);
        return false;
    }

    public void updateState(NMC2.NMCSTATEINFO status)
    {
        for (int i = 0; i < 2; i++)
        {
            m_isContiRun[i] = status.NmcAxesExpr.nContStatus[i] == 0 ? false : true;
        }
    }
}
