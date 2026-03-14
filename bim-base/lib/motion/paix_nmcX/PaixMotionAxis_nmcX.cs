using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PAIX_RTEX_MotionController;

public class PaixRtexMotionAxis : MotionAxis
{
    PaixRtexLib m_lib = null;
    short m_libNo = -1;
    int m_arrNo = -1;
    bool m_isConnected = false;

    PAIX_ACCEL_UNIT m_accelUnit = PAIX_ACCEL_UNIT.MM_PER_SECOND;

    public double m_absMaxSpeed;
    public double m_absAcc;
    public double m_absDec;
    public PAIX_DIR m_direction;

    short m_lastRetCode = (short)PAIX_RET.NMC_OK;

    public PaixRtexMotionAxis(PaixRtexLib lib, int axisNo, string name, int arrNo)
    {
        m_lib = lib;
        m_libNo = (short)lib.no();
        m_no = (short)axisNo;
        m_name = name;

        m_arrNo = arrNo;
    }

    public int arrNo()
    {
        return m_arrNo;
    }

    public int libNo()
    {
        return m_libNo;
    }

    public bool isConnected()
    {
        return m_isConnected;
    }

    public bool setUnitPerPulse(double pulseMoveUnit)
    {
        short ret = NMCXR.nmcX_UnitPerPulseSet(m_libNo, (short)m_no, pulseMoveUnit);

        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.debug("PaixRtexMotionAxis::setUnitPerPulse failed. no:" + m_no + " axisNo:" + m_no + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastRetCode = ret;
        return true;
    }

    public bool resetOriginComplete()
    {
        Debug.debug("PaixRtexMotionAxis::resetOriginComplete axisNo:" + m_no);

        short ret = NMCXR.nmcX_HomeDoneCancel(m_libNo, (short)m_no);

        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.debug("PaixRtexMotionAxis::resetOriginComplete failed. no:" + m_no + " axisNo:" + m_no + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastRetCode = ret;
        return true;
    }

    /// <summary>
    /// mode 0=EMG_STOP 1=DEC_STOP 2=NO_USE
    /// </summary>
    /// <param name="mode"></param>
    /// <param name="negPos"></param>
    /// <param name="posPos"></param>
    /// <returns></returns>
    public bool setSwLimitLogic(short mode, double negPos, double posPos)
    {
        Debug.debug("PaixRtexMotionAxis::setSwLimitLogic axisNo:" + m_no +
            " mode:" + mode +
            " negPos:" + negPos.ToString("0.000") +
            " posPos:" + posPos.ToString("0.000"));

        short ret = NMCXR.nmcX_StopModeSwLimit(m_libNo, (short)m_no, mode, negPos, mode, posPos);

        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.debug("PaixRtexMotionAxis::setSwLimitLogic failed. no:" + m_no + " axisNo:" + m_no + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastRetCode = ret;
        return true;
    }


    // 10. 단축 모션전용 출력 신호
    public bool setServoOn(bool value)
    {
        int val = value ? 1 : 0;

        short ret = NMCXR.nmcX_ServoOn(m_libNo, (short)m_no, (short)val);

        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.debug("PaixRtexMotionAxis::setServoOn failed. no:" + m_no + " axisNo:" + m_no + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastRetCode = ret;
        return true;
    }

    public bool setAlarmResetReq()
    {
        short ret = NMCXR.nmcX_AlarmResetReq(m_libNo, (short)m_no);

        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.debug("PaixRtexMotionAxis::setAlarmResetReq failed. no:" + m_no + " axisNo:" + m_no + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastRetCode = ret;
        return true;
    }

    public bool setSpeed(double startVel, double acc, double dec, double driveVel, int sp = 0)
    {
        short nProfile = (short)sp; // 사다리꼴
        double calcAcc = acc;
        double calcDec = dec;

        if (m_accelUnit == PAIX_ACCEL_UNIT.SECOND)
        {
            calcAcc = 1.0d / acc * driveVel;
            calcDec = 1.0d / dec * driveVel;
        }

        short ret = NMCXR.nmcX_SpeedSet(m_libNo, (short)m_no, nProfile, startVel, calcAcc, driveVel, calcDec);

        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.debug("PaixRtexMotionAxis::setSpeed failed. no:" + m_no + " axisNo:" + m_no + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastRetCode = ret;
        return true;
    }

    public bool cvVelMove(PAIX_DIR dir, double acc, double dec, double driveVel)
    {
        /**
  * @brief      [CV모드] 속도를 설정하여 구동합니다.
  * @param[in]  nXNo            연결된 컨트롤러 번호
  * @param[in]  nSID            Slave ID
  * @param[in]  dAcc            가속 설정값(pps)(음수값이면 설정안함)
  * @param[in]  dDrive          구동 설정값(pps)(음수일때 역방향)
  * @param[in]  dDec            감속 설정값(pps)(음수값이면 설정안함)
  * @param[in]  nSCurveTm       S-Curve시간(0~1000ms)(음수값이면 설정안함)
  * @return     EnmcX_FUNC_RESULT
  */

        short nProfile = 0; // s-curve 미사용
        double calcAcc = acc;
        double calcDec = dec;

        double velocity = driveVel;

        if (dir == PAIX_DIR.CCW)
            velocity = -driveVel;

        if (m_accelUnit == PAIX_ACCEL_UNIT.SECOND)
        {
            calcAcc = 1.0d / acc * driveVel;
            calcDec = 1.0d / dec * driveVel;
        }

        short ret = NMCXR.nmcX_CV_SetVelocity(m_libNo, (short)m_no, acc, velocity, dec, nProfile);

        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.debug("PaixRtexMotionAxis::cvVelMove failed. no:" + m_no + " axisNo:" + m_no + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastRetCode = ret;
        return true;
    }

    public bool setSCurveSpeed(double startVel, double acc, double dec, double driveVel)
    {
        short nProfile = 1; // S-Curve
        double calcAcc = acc;
        double calcDec = dec;

        if (m_accelUnit == PAIX_ACCEL_UNIT.SECOND)
        {
            calcAcc = 1.0d / acc * driveVel;
            calcDec = 1.0d / dec * driveVel;
        }

        short ret = NMCXR.nmcX_SpeedSet(m_libNo, (short)m_no, nProfile, startVel, calcAcc, driveVel, calcDec);

        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.debug("PaixRtexMotionAxis::setSCurveSpeed failed. no:" + m_no + " axisNo:" + m_no + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastRetCode = ret;
        return true;
    }

    public bool setAccSpeed(double acc)
    {
        short ret = NMCXR.nmcX_SpeedSetAcc(m_libNo, (short)m_no, acc);

        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.debug("PaixRtexMotionAxis::setAccSpeed failed. no:" + m_no + " axisNo:" + m_no + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastRetCode = ret;
        return true;
    }

    public bool setDecSpeed(double dec)
    {
        short ret = NMCXR.nmcX_SpeedSetDec(m_libNo, (short)m_no, dec);

        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.debug("PaixRtexMotionAxis::setDecSpeed failed. no:" + m_no + " axisNo:" + m_no + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastRetCode = ret;
        return true;
    }

    // 13. 속도 오버라이드
    public bool setOverrideRunSpeed(double acc, double dec, double driveVel)
    {
        double calcAcc = acc;
        double calcDec = dec;

        if (m_accelUnit == PAIX_ACCEL_UNIT.SECOND)
        {
            calcAcc = 1.0d / acc * driveVel;
            calcDec = 1.0d / dec * driveVel;
        }

        short ret = NMCXR.nmcX_OverrideSpeed(m_libNo, (short)m_no, calcAcc, driveVel, calcDec);

        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.warning("PaixRtexMotionAxis::setSpeedOverride failed. no:" + m_no + " axisNo:" + m_no +
                " acc:" + calcAcc + " dec:" + calcDec + " driveVel:" + driveVel + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastRetCode = ret;
        return true;
    }

    // 14. 단축 이동
    public bool absMove(double pos, int sp = 0)
    {
        Debug.debug("PaixRtexMotionAxis::absMove axisNo:" + m_no + " pos:" + pos.ToString("0.000") + " profile:" + sp);

        setSpeed(1.0d, m_absAcc, m_absDec, m_absMaxSpeed, sp);
        short ret = NMCXR.nmcX_MoveAbs(m_libNo, (short)m_no, pos);

        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.debug("PaixRtexMotionAxis::absMove failed. no:" + m_no + " axisNo:" + m_no + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastCmd = pos;
        m_lastRetCode = ret;

        return true;
    }

    public bool overrideAbsMove(double pos)
    {
        Debug.debug("PaixRtexMotionAxis::overrideAbsMove axisNo:" + m_no + " pos:" + pos.ToString("0.000"));

        short ret = NMCXR.nmcX_OverrideAbs(m_libNo, (short)m_no, pos);

        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.debug("PaixRtexMotionAxis::overrideAbsMove failed. no:" + m_no + " axisNo:" + m_no + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastCmd = pos;
        m_lastRetCode = ret;

        return true;
    }

    public bool relMove(double pos)
    {
        short ret = NMCXR.nmcX_MoveRel(m_libNo, (short)m_no, pos);

        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.debug("PaixRtexMotionAxis::relMove failed. no:" + m_no + " axisNo:" + m_no + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastRetCode = ret;
        return true;
    }

    public bool velMove(PAIX_DIR dir)
    {
        short ret = NMCXR.nmcX_MoveVel(m_libNo, (short)m_no, (short)dir);

        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.debug("PaixRtexMotionAxis::velMove failed. no:" + m_no + " axisNo:" + m_no + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastRetCode = ret;
        return true;
    }

    //18. 모션 정지
    public bool suddenStop()
    {
        short ret = NMCXR.nmcX_StopSudden(m_libNo, (short)m_no);
        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.debug("PaixRtexMotionAxis::suddenStop failed. no:" + m_no + " axisNo:" + m_no + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastRetCode = ret;
        return true;
    }

    public bool decStop()
    {
        short ret = NMCXR.nmcX_StopDec(m_libNo, (short)m_no);
        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.debug("PaixRtexMotionAxis::decStop failed. no:" + m_no + " axisNo:" + m_no + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastRetCode = ret;
        return true;
    }

    //19. 위치 변경
    public bool setCmdEncPos(double pos)
    {
        short ret = NMCXR.nmcX_CmdEncPosSet(m_libNo, (short)m_no, pos);

        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.debug("PaixRtexMotionAxis::setCmdPos failed. no:" + m_no + " axisNo:" + m_no + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastRetCode = ret;
        return true;
    }

    public bool setEncToCmdPos()
    {
        short ret = NMCXR.nmcX_EncToCmdPos(m_libNo, (short)m_no);

        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.debug("PaixRtexMotionAxis::setEncToCmdPos failed. no:" + m_no + " axisNo:" + m_no + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastRetCode = ret;
        return true;
    }

    // 원점 관련
    public bool homeMoveHome(PAIX_DIR dir, double offset)
    {
        short decStop = 1; // DEC
        short zPhase = 2; // NONE
        short posClear = 1; // USE
        short posClearDelay = 10; // ms

        short ret = NMCXR.nmcX_HomeMoveHome(m_libNo, (short)m_no, (short)dir, decStop, zPhase, posClear, posClearDelay, offset);

        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.debug("PaixRtexMotionAxis::homeMove failed. no:" + m_no + " axisNo:" + m_no + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastRetCode = ret;
        return true;
    }

    public bool setHomeSpeed(double vel1, double vel2, double vel3, short accRatio)
    {
        short profile = 0; // 사다리

        short ret = NMCXR.nmcX_HomeSetSpeed(m_libNo, (short)m_no, profile, 0, vel1, vel2, vel3, vel1, accRatio);

        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.debug("PaixRtexMotionAxis::setHomeSpeed failed. no:" + m_no + " axisNo:" + m_no + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastRetCode = ret;
        return true;
    }

    // 원점 완료 취소
    public bool homeDoneCancel()
    {
        short ret = NMCXR.nmcX_HomeDoneCancel(m_libNo, (short)m_no);

        if (checkFuncRet(ret) == false)
        {
            if (m_lastRetCode != ret)
            {
                Debug.debug("PaixRtexMotionAxis::homeDoneCancel failed. no:" + m_no + " axisNo:" + m_no + " ret:" + ret);
                m_lastRetCode = ret;
            }
            return false;
        }

        m_lastRetCode = ret;
        return true;
    }
    public void update()
    {
        NMCXR.TnmcX_SERVO_STATUS status = new NMCXR.TnmcX_SERVO_STATUS();

        m_lib.getStateInfo(m_no, out status);

        m_pos = status.dENCPos;
        m_cmd = status.dCMDPos;

        m_curSpeed = status.dENCSpeed;
        m_cmdSpeed = status.dCMDSpeed;

        m_loadRatio = status.dTorque;

        m_busy = status.nBusy == 1 ? true : false;

        m_orgSensor = status.nHome == 1 ? true : false;
        m_negLimitSensor = status.nLimitM == 1 ? true : false;
        m_posLimitSensor = status.nLimitP == 1 ? true : false;

        m_alarm = status.nAlarm == 1 ? true : false;
        m_inpos = status.nINP == 1 ? true : false;

        m_ready = status.nServoReady == 1 ? true : false;
        m_servoOn = status.nServoOn == 1 ? true : false;


        NMCXR.EHomeStatus homeStatus = (NMCXR.EHomeStatus)status.nHomeStatus;

        m_homeStatus = homeStatus.ToString();

        if (homeStatus == NMCXR.EHomeStatus.hstsDone)
            m_orgComplete = true;
        else
            m_orgComplete = false;

        if (status.nEStop == 1)
            m_emergency = true;
        else
            m_emergency = false;
    }

    public void stop()
    {
        decStop();
    }

    public void setAccelUnit(PAIX_ACCEL_UNIT unit)
    {
        m_accelUnit = unit;
    }

    public PAIX_ACCEL_UNIT accelUnit()
    {
        return m_accelUnit;
    }

    bool m_agoSwUse = false;
    double m_agoNegPos = -999999.0d;
    double m_agoPogPos = 999999.0d;

    public bool setSoftLimit(bool use, double negPos, double pogPos)
    {
        if (m_agoSwUse == use &&
            m_agoPogPos == pogPos &&
            m_agoNegPos == negPos)
        {
            return false;
        }

        int mode = use ? 1 : 2; // dec : no use

        setSwLimitLogic((short)mode, negPos, pogPos);

        m_agoSwUse = use;
        m_agoPogPos = pogPos;
        m_agoNegPos = negPos;

        return true;
    }

    public void restoreSoftLimit()
    {
        double negPos = m_agoNegPos;
        double pogPos = m_agoPogPos;

        int mode = m_agoSwUse ? 1 : 2; // dec : no use

        setSwLimitLogic((short)mode, negPos, pogPos);
    }

    public void releaseSoftLimit()
    {
        double negPos = -100.0d;
        double pogPos = 999.99d;

        if (m_agoSwUse == false)
            return;

        int mode = 2; // no use

        setSwLimitLogic((short)mode, negPos, pogPos);

        m_agoSwUse = false;
        m_agoPogPos = pogPos;
        m_agoNegPos = negPos;

    }

    public void setAbsSpeed(double speed, double acc, double dec)
    {
        m_absMaxSpeed = speed;
        m_absAcc = acc;
        m_absDec = dec;
    }

    public bool checkFuncRet(short ret)
    {
        PAIX_RET retEnum = (PAIX_RET)ret;

        if (retEnum == PAIX_RET.NMC_OK)
            return true;
        else
            Debug.warning("PaixRtexMotionAxis::checkFuncRet failed. ret:" + retEnum);

        return false;
    }

    public double lastAcc()
    {
        return m_absAcc;
    }

    public double lastDec()
    {
        return m_absDec;
    }

    public double lastVelocity()
    {
        return m_absMaxSpeed;
    }

    public PAIX_DIR lastDirection()
    {
        return m_direction;
    }
}
