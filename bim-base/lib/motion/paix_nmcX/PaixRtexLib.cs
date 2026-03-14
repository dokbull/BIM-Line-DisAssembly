using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAIX_RTEX_MotionController;
using static PAIX_RTEX_MotionController.NMCXR;

public class PaixRtexLib
{
    short m_libNo = 0;
    short m_addr1 = 0;
    short m_addr2 = 0;
    short m_addr3 = 0;

    public enum CONTROL_TYPE
    {
        PP = 1,
        CP = 2,
        CV = 3,
    }

    public PaixRtexLib(int no)
    {
        m_libNo = (short)no;
    }

    public int no()
    {
        return m_libNo;
    }

    public bool open(int classA, int classB, int classC, int no)
    {
        return openDevice(no, classA, classB, classC);
    }

    public void close()
    {
        closeDevice();
    }

    public bool pingCheck(int waitTime)
    {
        short ret = NMCXR.nmcX_PingCheck(m_libNo, m_addr1, m_addr2, m_addr3, waitTime);

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixLib_nmcX::pingCheck failed. no:" + m_libNo);
            return false;
        }

        return true;
    }

    bool openDevice(int no, int classA, int classB, int classC)
    {
        short ret = NMCXR.nmcX_Connect((short)no, (short)classA, (short)classB, (short)classC);

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixLib_nmcX::openDevice failed. no:" + m_libNo);
            return false;
        }

        m_libNo = (short)no;
        m_addr1 = (short)classA;
        m_addr2 = (short)classB;
        m_addr3 = (short)classC;

        return true;
    }

    bool closeDevice()
    {
        NMCXR.nmcX_Disconnect(m_libNo);
        return true;
    }

    public bool setDisconnectedStopMode(int timeInterval, PAIX_STOPMODE_NMCX stopMode)
    {
        short ret = NMCXR.nmcX_StopModeDisconnected(m_libNo, timeInterval, (short)stopMode);

        if (checkFuncRet(ret))
        {
            Debug.warning("PaixLib_nmcX::setDisconnectedStopMode failed. no:" + m_libNo);
            return false;
        }

        return true;
    }

    public bool setMultiServoOn(int[] axisArr, bool value)
    {
        int val = value ? 1 : 0;
        short ret = NMCXR.nmcX_mServoOn(m_libNo, (short)axisArr.Length, convertIntArr(axisArr), (short)val);

        if (checkFuncRet(ret))
        {
            Debug.warning("PaixLib_nmcX::setMultiServoOn failed. no:" + m_libNo);
            return false;
        }

        return true;
    }

    public bool setMultiAlarmReset(int[] axisArr)
    {
        short ret = NMCXR.nmcX_mAlarmResetReq(m_libNo, (short)axisArr.Length, convertIntArr(axisArr));

        if (checkFuncRet(ret))
        {
            Debug.warning("PaixLib_nmcX::setMultiAlarmResetOn failed. no:" + m_libNo);
            return false;
        }

        return true;
    }


    public bool multiAxisStop(int[] axisArr, PAIX_STOPMODE mode)
    {
        short ret = NMCXR.nmcX_mStop(m_libNo, (short)axisArr.Length, convertIntArr(axisArr), (short)mode);

        if (checkFuncRet(ret))
        {
            Debug.warning("PaixLib_nmcX::multiAxisStop failed. no:" + m_libNo);
            return false;
        }

        return true;
    }


    public bool getHomeStatus(int axisId, out NMCXR.EHomeStatus flag)
    {
        NMCXR.TnmcX_SERVO_STATUS status = new NMCXR.TnmcX_SERVO_STATUS();
        short ret = NMCXR.nmcX_GetServoStatus((short)m_libNo, (short)axisId, out status);

        flag = (NMCXR.EHomeStatus)status.nHomeStatus;

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixLib_nmcX::getHomeStatus failed. no:" + m_libNo);
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

    // GANTRY 구동 (MIRROR MODE)
    public bool setMirrorAxis(int groupNo, int mainAxisNo, int slaveAxisNo)
    {
        short ret = NMCXR.nmcX_ServoPropertyCfg(m_libNo, (short)mainAxisNo, 0, 2, (short)groupNo, (short)groupNo, 1, 0);

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixLib_nmcX::setGantryAxis failed. no:" + m_libNo + " groupNo:" + groupNo);
            return false;
        }

        ret = NMCXR.nmcX_ServoPropertyCfg(m_libNo, (short)slaveAxisNo, 0, 2, (short)groupNo, (short)groupNo, 2, 0);

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixLib_nmcX::setGantryAxis failed. no:" + m_libNo + " groupNo:" + groupNo);
            return false;
        }

        return true;
    }

    public bool setServoParam(int axis, CONTROL_TYPE type)
    {
        //   nXNo 연결된 컨트롤러 번호
        // nSID Slave ID
        //  nSaveROM ROM에 저장 여부(0 = 저장 안함, 1 = 저장)
        // nMode 모션 제어 모드 ::ESlaveCtrlMode(0 = 변경안함, 1 = PP, 2 = CP, 3 = CV, 4 = CT)
        //  nCPGroup CP 제어 Group(0~3)
        // nCPAxis CP 제어 축(0~3)
        // nMirror Mirror 축 설정 ::EMirror(0 = 없음, 1 = Main, 2 = Sub)
        //  lAddInfo 모드 변화시 추가 정보 PP, CP, CV:설정값 없음, CT = -5000~5000:변화시 Torque값, 그외:토크 유지

        short ret = NMCXR.nmcX_ServoPropertyCfg(m_libNo, (short)axis, 1, (short)type, 0, 0, 0, 0);

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixLib_nmcX::setServoParam failed. no:" + m_libNo + " axis:" + axis + " type:" + type);
            return false;
        }

        return true;
    }

    public bool getStateInfo(int axisId, out NMCXR.TnmcX_SERVO_STATUS status)
    {
        short ret = NMCXR.nmcX_GetServoStatus(m_libNo, (short)axisId, out status);

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixLib_nmcX::getStateInfo failed. no:" + m_libNo);
            return false;
        }

        return true;
    }

    public bool checkFuncRet(short ret)
    {
        PAIX_RET retEnum = (PAIX_RET)ret;

        if (retEnum == PAIX_RET.NMC_OK)
            return true;

        Debug.warning("PaixLib_nmcX::checkFuncRet failed. ret:" + retEnum);
        return false;
    }
}