using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Paix_MotionController;

public class PaixMotionAxis : MotionAxis
{
    short m_libNo = -1;
    int m_arrNo = -1;
    bool m_isConnected = false;

    PAIX_ACCEL_UNIT m_accelUnit = PAIX_ACCEL_UNIT.MM_PER_SECOND;

    public double m_absMaxSpeed;
    public double m_absAcc;
    public double m_absDec;
    public PAIX_DIR m_direction;

    public PaixMotionAxis(PaixLib lib, int axisNo, string name, int arrNo)
    {
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
        short ret = NMC2.nmc_SetUnitPerPulse(m_libNo, (short)m_no, pulseMoveUnit);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setUnitPerPulse failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }

        return true;
    }

    public bool getParaLogic(NMC2.NMCPARALOGIC logic)
    {
        short ret = NMC2.nmc_GetParaLogic(m_libNo, (short)m_no, out logic);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::getParaLogic failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }

        return true;
    }

    public bool getParaLogicEx(NMC2.NMCPARALOGICEX logicEx)
    {
        short ret = NMC2.nmc_GetParaLogicEx(m_libNo, (short)m_no, out logicEx);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::getParaLogicEx failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }

        return true;
    }

    public bool setPlusLimitLogic(PAIX_LEVEL level)
    {
        short ret = NMC2.nmc_SetPlusLimitLogic(m_libNo, (short)m_no, (short)level);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setPlusLimitLogic failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }

        return true;
    }

    public bool setMinusLimitLogic(PAIX_LEVEL level)
    {
        short ret = NMC2.nmc_SetMinusLimitLogic(m_libNo, (short)m_no, (short)level);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setMinusLimitLogic failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }

        return true;
    }

    public bool setSwLimitLogic(bool use, double negPos, double posPos)
    {
        Debug.debug("PaixMotionAxis::setSwLimitLogic axisNo:" + m_no + 
            " use:" + use + 
            " negPos:" + negPos.ToString("0.000") + 
            " posPos:" + posPos.ToString("0.000"));

        int value = (use == true) ? 1 : 0;

        short ret = NMC2.nmc_SetSWLimitLogic(m_libNo, (short)m_no, (short)value, negPos, posPos);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setSwLimitLogic failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }

        return true;
    }

    public bool setSwLimitLogicEx(bool use, double negPos, double posPos, PAIX_SW_LIMIT opt)
    {
        int value = (use == true) ? 1 : 0;

        short ret = NMC2.nmc_SetSWLimitLogicEx(m_libNo, (short)m_no, (short)value, negPos, posPos, (short)opt);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setSwLimitLogicEx failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }

        return true;
    }

    public bool setAlarmLogic(PAIX_LEVEL level)
    {
        short ret = NMC2.nmc_SetAlarmLogic(m_libNo, (short)m_no, (short)level);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setAlarmLogic failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }

        return true;
    }

    public bool setNearLogic(PAIX_LEVEL level)
    {
        short ret = NMC2.nmc_SetNearLogic(m_libNo, (short)m_no, (short)level);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setNearLogic failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }

        return true;
    }

    public bool setInposLogic(PAIX_LEVEL level)
    {
        short ret = NMC2.nmc_SetInPoLogic(m_libNo, (short)m_no, (short)level);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setInposLogic failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }

        return true;
    }

    public bool setServoReadyLogic(PAIX_LEVEL level)
    {
        short ret = NMC2.nmc_SetSReadyLogic(m_libNo, (short)m_no, (short)level);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setServoReadyLogic failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }

        return true;
    }

    public bool setEncoderZLogic(PAIX_LEVEL level)
    {
        short ret = NMC2.nmc_SetEncoderZLogic(m_libNo, (short)m_no, (short)level);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setServoReadyLogic failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }

        return true;
    }

    public bool setEncoderDir(PAIX_ENCODER_DIR dir)
    {
        short ret = NMC2.nmc_SetEncoderDir(m_libNo, (short)m_no, (short)dir);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setEncoderDir failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }

        return true;
    }

    public bool setEncoderCount(PAIX_ENCODER_COUNT count)
    {
        short ret = NMC2.nmc_SetEncoderCount(m_libNo, (short)m_no, (short)count);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setEncoderCount failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }

        return true;
    }

    public bool setPulseLogic(PAIX_PULSE_LOGIC logic)
    {
        short ret = NMC2.nmc_SetPulseLogic(m_libNo, (short)m_no, (short)logic);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setPulseLogic failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }

        return true;
    }

    public bool setHomeDoneAutoCancel(bool alarm, bool servoOff, bool currentOff, bool servoReady)
    {
        int alarmValue = alarm ? 1 : 0;
        int servoOffValue = servoOff ? 1 : 0;
        int currentOffValue = currentOff ? 1 : 0;
        int servoReadyValue = servoOff ? 1 : 0;

        short ret = NMC2.nmc_SetHomeDoneAutoCancel(
            m_libNo, (short)m_no, 
            (short)alarmValue, (short)servoOffValue,
            (short)currentOffValue, (short)servoReadyValue);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setHomeDoneAutoCancel failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }

        return true;
    }

    public bool getHomeDoneAutoCancel(PAIX_HOME_AUTOCANCEL data)
    {
        short[] value = new short[4];

        short ret = NMC2.nmc_GetHomeDoneAutoCancel(m_libNo, (short)m_no, out value[0], out value[1],
            out value[2], out value[3]);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::getHomeDoneAutoCancel failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }

        data.alarm = (value[0] == 1) ? true : false;
        data.servoOff = (value[1] == 1) ? true : false;
        data.currentOff = (value[2] == 1) ? true : false;
        data.servoReady = (value[3] == 1) ? true : false;

        return true;
    }

    public bool setParaLogic(NMC2.NMCPARALOGIC logic)
    {
        short ret = NMC2.nmc_SetParaLogic(m_libNo, (short)m_no, ref logic);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setParaLogic failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool setParaLogicEx(NMC2.NMCPARALOGICEX logicEx)
    {
        short ret = NMC2.nmc_SetParaLogicEx(m_libNo, (short)m_no, ref logicEx);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setParaLogicExs failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    // 10. 단축 모션전용 출력 신호
    public bool setCurrentOn(bool value)
    {
        int val = value ? 1 : 0;

        short ret = NMC2.nmc_SetCurrentOn(m_libNo, (short)m_no, (short)val);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setCurrentOn failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool setServoOn(bool value)
    {
        int val = value ? 1 : 0;

        short ret = NMC2.nmc_SetServoOn(m_libNo, (short)m_no, (short)val);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setServoOn failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool setAlarmResetOn(bool value)
    {
        int val = value ? 1 : 0;

        short ret = NMC2.nmc_SetAlarmResetOn(m_libNo, (short)m_no, (short)val);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setAlarmResetOn failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool setDccOn(bool value)
    {
        int val = value ? 1 : 0;

        short ret = NMC2.nmc_SetDccOn(m_libNo, (short)m_no, (short)val);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setDccOn failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool setSpeed(double startVel, double acc, double dec, double driveVel)
    {
        double calcAcc = acc;
        double calcDec = dec;

        if (m_accelUnit == PAIX_ACCEL_UNIT.SECOND)
        {
            calcAcc = 1.0d / acc * driveVel;
            calcDec = 1.0d / dec * driveVel;
        }

        short ret = NMC2.nmc_SetSpeed(m_libNo, (short)m_no, startVel, calcAcc, calcDec, driveVel);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setSpeed failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool setSCurveSpeed(double startVel, double acc, double dec, double driveVel)
    {
        short ret = NMC2.nmc_SetSCurveSpeed(m_libNo, (short)m_no, startVel, acc, dec, driveVel);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setSCurveSpeed failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool setAccSpeed(double acc)
    {
        short ret = NMC2.nmc_SetAccSpeed(m_libNo, (short)m_no, acc);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setAccSpeed failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool setDecSpeed(double acc)
    {
        short ret = NMC2.nmc_SetDecSpeed(m_libNo, (short)m_no, acc);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setDecSpeed failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
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

        short ret = NMC2.nmc_SetOverrideRunSpeed(m_libNo, (short)m_no, calcAcc, calcDec, driveVel);

        if (checkFuncRet(ret) == false)
        {
            Debug.warning("PaixMotionAxis::setSpeedOverride failed. no:" + m_no + " axisNo:" + m_no + 
                " acc:" + calcAcc + " dec:" + calcDec + " driveVel:" + driveVel);
            return false;
        }
        return true;
    }

    // 14. 단축 이동
    public bool absMove(double pos)
    {
        Debug.debug("PaixMotionAxis::absMove axisNo:" + m_no + " pos:" + pos.ToString("0.000"));

        setSpeed(1.0d, m_absAcc, m_absDec, m_absMaxSpeed);
        short ret = NMC2.nmc_AbsMove(m_libNo, (short)m_no, pos);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::absMove failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }

        m_lastCmd = pos;

        return true;
    }

    public bool absMoveWithSpeed(double pos, double speed)
    {
        setSpeed(1, m_absAcc, m_absDec, speed);
        short ret = NMC2.nmc_AbsMove(m_libNo, (short)m_no, pos);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::absMoveWithSpeed failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }

        m_lastCmd = pos;

        return true;
    }

    public bool relMove(double pos)
    {
        short ret = NMC2.nmc_RelMove(m_libNo, (short)m_no, pos);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::relMove failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool velMove(double pos, double vel, PAIX_MOVE moveType)
    {
        short ret = NMC2.nmc_VelMove(m_libNo, (short)m_no, pos, vel, (short)moveType);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::velMove failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool getBusyStatus()
    {
        short value = 0;
        short ret = NMC2.nmc_GetBusyStatus(m_libNo, (short)m_no, out value);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::getBusyStatus failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }

        if (value == 0)
            return false;

        return true;
    }

    //16. 위치 오버라이드 (추후 구현할 것) TODO@tmdwn
    //17. 속도 이동
    public bool jogMove(PAIX_DIR dir)
    {
        m_direction = dir;

        short ret = NMC2.nmc_JogMove(m_libNo, (short)m_no, (short)dir);

        Debug.debug("PaixMotionAxis::jogMove libNo:" + m_libNo + " no:" + m_no + " dir:" + dir);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::jogMove failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    //18. 모션 정지
    public bool suddenStop()
    {
        short ret = NMC2.nmc_SuddenStop(m_libNo, (short)m_no);
        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::suddenStop failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool decStop()
    {
        short ret = NMC2.nmc_DecStop(m_libNo, (short)m_no);
        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::decStop failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    //19. 위치 변경
    public bool setCmdPos(double pos)
    {
        short ret = NMC2.nmc_SetCmdPos(m_libNo, (short)m_no, pos);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setCmdPos failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool setEncPos(double pos)
    {
        short ret = NMC2.nmc_SetEncPos(m_libNo, (short)m_no, pos);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setEncPos failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool setCmdEncPos(double pos)
    {
        m_lastCmd = pos;
        short ret = NMC2.nmc_SetCmdEncPos(m_libNo, (short)m_no, pos);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setCmdEncPos failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool homeMove(PAIX_HOME_MODE mode, double offset)
    {
        short ret = NMC2.nmc_HomeMove(m_libNo, (short)m_no, (short)mode, 0xF, offset, 0);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::homeMove failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool setHomeSpeed(double vel1, double vel2, double vel3)
    {
        short ret = NMC2.nmc_SetHomeSpeed(m_libNo, (short)m_no, vel1, vel2, vel3);
        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setHomeSpeed failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool setHomeSpeedEx(double vel1, double vel2, double vel3, double offset)
    {
        short ret = NMC2.nmc_SetHomeSpeedEx(m_libNo, (short)m_no, vel1, vel2, vel3, offset);
        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setHomeSpeedEx failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool setHomeSpeedAccDec(
        double vel1, double start1, double acc1, double dec1,
        double vel2, double start2, double acc2, double dec2,
        double vel3, double start3, double acc3, double dec3,
        double offVel, double offStartVel, double offAcc, double offDec)
    {

        short ret = NMC2.nmc_SetHomeSpeedAccDec(m_libNo, (short)m_no,
            vel1, start1, acc1, dec1,
            vel2, start2, acc2, dec2,
            vel3, start3, acc3, dec3,
            offVel, offStartVel, offAcc, offDec);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setHomeSpeedAccDec failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    // 2축 직선 보간
    public bool interpolationMove(double pos, int subAxis, double subPos, bool absolute = true)
    {
        short type = 0;
        if (absolute)
            type = 1;

        short ret = NMC2.nmc_Interpolation2Axis(m_libNo, (short)m_no, pos, (short)subAxis, subPos, type);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::interpolationMove failed. no:" + m_libNo + " axisNo1:" + m_no + " axisNo2:" + subAxis);
            return false;
        }
        return true;
    }


    // 연속 보간 초기화
    public bool contiSetNodeClear()
    {
        short group = 0;
        if (m_no > 3)
            group = 1;

        short ret = NMC2.nmc_ContiSetNodeClear(m_libNo, (short)group);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::contiSetNodeClear failed. no:" + m_libNo + " group:" + group);
            return false;
        }
        return true;
    }


    // 연속 보간 설정
    public bool contiSetMode(double maxSpeed, int subAxis1, int subAxis2 = -1)
    {
        short group = 0;
        if (m_no > 3)
            group = 1;

        short disable = 0; // 1 = 헬리컬 & 4축 보간 사용
        short waitQue = 1; // 1 = 보간 큐 종료시 추가입력 대기, 0 = 종료
        short nloType = 2; // IO 제어 (미사용)
        int nloCtrlPin = 0; // IO 제어 (미사용)
        int nloCtrlEnd = 0; // IO 제어 (미사용)

        short ret = NMC2.nmc_ContiSetMode(m_libNo, group, disable, waitQue, (short)m_no, (short)subAxis1, (short)subAxis2, maxSpeed, nloType, nloCtrlPin, nloCtrlEnd);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::contiSetMode failed. no:" + m_libNo + " group:" + group);
            return false;
        }
        return true;
    }

    // 연속 2축 직선 보간
    public bool contiAddLine(double pos1, double pos2, double start, double acc, double dec, double speed)
    {
        short group = 0;
        if (m_no > 3)
            group = 1;

        int nloCtrlVal = 2; // IO 제어 (미사용)

        short ret = NMC2.nmc_ContiAddNodeLine2Axis(m_libNo, group, pos1, pos2, start, acc, dec, speed, nloCtrlVal);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::contiAddLine failed. no:" + m_libNo + " group:" + group);
            return false;
        }
        return true;
    }

    // 연속 2축 원호 보간
    public bool contiAddArc(double center1, double center2, double angle, double start, double acc, double dec, double speed)
    {
        short group = 0;
        if (m_no > 3)
            group = 1;

        int nloCtrlVal = 2; // IO 제어 (미사용)

        short ret = NMC2.nmc_ContiAddNodeArc(m_libNo, group, center1, center2, angle, start, acc, dec, speed, nloCtrlVal);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::contiAddArc failed. no:" + m_libNo + " group:" + group);
            return false;
        }
        return true;
    }

    // 연속 2축 원호 보간 (E버전 전용)
    public bool contiAddArcCE(double center1, double center2, double end1, double end2, short dir, double start, double acc, double dec, double speed)
    {
        short group = 0;
        if (m_no > 3)
            group = 1;

        int nloCtrlVal = 2; // IO 제어 (미사용)

        short ret = NMC2.nmc_ContiAddNodeArcCE(m_libNo, group, center1, center2, end1, end2, dir, start, acc, dec, speed, nloCtrlVal);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::contiAddArcCE failed. no:" + m_libNo + " group:" + group);
            return false;
        }
        return true;
    }

    // 연속 보간 노드 종료
    public bool contiSetNodeClose()
    {
        short group = 0;
        if (m_no > 3)
            group = 1;

        short ret = NMC2.nmc_ContiSetCloseNode(m_libNo, group);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::contiSetNodeClose failed. no:" + m_libNo + " group:" + group);
            return false;
        }
        return true;
    }

    // 연속 보간 시작 & 종료
    public bool contiRunStop(bool run = true)
    {
        short group = 0;
        if (m_no > 3)
            group = 1;

        short mode = 0;
        if (run == false)
            mode = 1;

        short ret = NMC2.nmc_ContiRunStop(m_libNo, group, mode);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::contiRunStop failed. no:" + m_libNo + " group:" + group);
            return false;
        }
        return true;
    }

    // 원점 완료 취소
    public bool homeDoneCancel()
    {
        short ret = NMC2.nmc_HomeDoneCancel(m_libNo, (short)m_no);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::homeDoneCancel failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    //25. 링카운터(원운동) 설정
    public bool setRingCountMode(double pulse, bool use)
    {
        short useMode = 0;

        if (use)
            useMode = 1;

        short ret = NMC2.nmc_SetRingCountMode(m_libNo, (short)m_no, (int)pulse, (int)pulse, useMode);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setRingCountMode failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    // 원 이동
    public bool moveRing(double pos, PAIX_DIR dir)
    {
        short turn = 0;

        if (dir == PAIX_DIR.CCW)
            turn = 1;

        Debug.debug("PaixMotionAxis::ringMove axisNo:" + m_no + " pos:" + pos.ToString("0.000"));

        short ret = NMC2.nmc_MoveRing(m_libNo, (short)m_no, pos, turn);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::ringMove failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }

        m_lastCmd = pos;

        return true;
    }

    //26. MDIO 입출력
    public bool setOutputMDIO(int index, bool value)
    {
        short status = 0;
        if (value)
            status = 1;

        short ret = NMC2.nmc_SetMDIOOutPin(m_libNo, (short)index, status);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::setOutputMDIO failed. no:" + m_no + " axisNo:" + m_no);
            return false;
        }
        return true;
    }

    short[] inputArray = new short[8];
    short[] outputArray = new short[8];

    public void updateOutInMDIO(ref bool[] inArray, ref bool[] outArray)
    {
        updateOutIn(ref inputArray, ref outputArray);

        for (int i = 0; i < 8; i++)
        {
            inArray[i] = (inputArray[i] == 1) ? true : false;
            outArray[i] = (outputArray[i] == 1) ? true : false;
        }
    }

    private bool updateOutIn(ref short[] inputArray, ref short[] outputArray)
    {
        short ret = NMC2.nmc_GetMDIOInput(m_libNo, inputArray);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::updateOutIn nmc_GetMDIOInput failed. libNo:" + m_libNo + " axisNo:" + m_no);
            return false;
        }

        ret = NMC2.nmc_GetMDIOOutput(m_libNo, outputArray);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::updateOutIn nmc_GetMDIOOutput failed. libNo:" + m_libNo + " axisNo:" + m_no);
            return false;
        }

        return false;
    }

    //33. Trigger
    public bool triggerSet(bool activeHigh, short delay, short pulseWidth, short type = 1)
    {
        short logic = 0;
        if (activeHigh)
            logic = 1;
        short ret = NMC2.nmc_SetTriggerIO(m_libNo, (short)m_no, type, logic, delay, pulseWidth);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::triggerSet failed. libNo:" + m_libNo + " aixsNo:" + m_no);
            return false;
        }

        return true;
    }

    public bool triggerOutAbs(double[] posList)
    {
        short count = Convert.ToInt16(posList.Count());

        short ret = NMC2.nmc_TriggerOutAbsPos(m_libNo, (short)m_no, count, posList);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::triggerOutAbs failed. libNo:" + m_libNo + " aixsNo:" + m_no);
            return false;
        }

        return true;
    }

    public bool triggerOutStop()
    {
        short ret = NMC2.nmc_TriggerOutStop(m_libNo, (short)m_no);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::triggerOutStop failed. libNo:" + m_libNo + " aixsNo:" + m_no);
            return false;
        }

        return true;
    }

    public bool triggerOneShot()
    {
        short ret = NMC2.nmc_TriggerOutOneShot(m_libNo, (short)m_no);

        if (checkFuncRet(ret) == false)
        {
            Debug.debug("PaixMotionAxis::triggerOneShot failed. libNo:" + m_libNo + " aixsNo:" + m_no);
            return false;
        }

        return true;
    }

    public int triggerCount()
    {
        NMC2.NMCTRIGSTATUS triggerStatus = new NMC2.NMCTRIGSTATUS();
        short ret = NMC2.nmc_GetTriggerStatus(m_libNo, out triggerStatus);

        int count = triggerStatus.nCount[m_no];

        return count;
    }

    public void update(NMC2.NMCAXESEXPR status, NMC2.NMCHOMEFLAG homeStatus)
    {
        int axisNo = m_no;

        m_pos = status.dEnc[axisNo];
        m_cmd = status.dCmd[axisNo];

        m_busy = status.nBusy[axisNo] == 1 ? true : false;

        m_orgSensor = status.nNear[axisNo] == 1 ? true : false;
        m_negLimitSensor = status.nMLimit[axisNo] == 1 ? true : false;
        m_posLimitSensor = status.nPLimit[axisNo] == 1 ? true : false;

        m_alarm = status.nAlarm[axisNo] == 1 ? true : false;
        m_inpos = status.nInpo[axisNo] == 1 ? true : false;

        m_ready = status.nSReady[axisNo] == 1 ? true : false;
        m_servoOn = status.nSReady[axisNo] == 1 ? true : false;

        if (homeStatus.nSrchFlag[axisNo] == 0 && homeStatus.nStatusFlag[axisNo] == 0)
            m_orgComplete = true;
        else
            m_orgComplete = false;

        if (status.nEmer[0] == 1 || status.nEmer[1] == 1)
            m_emergency = true;
        else
            m_emergency = false;

        //FIXME@tmdwn..라이브러리에 존재해야함. 그룹 단위
        //m_contiRun = status.nContStatus[axisNo] == 1 ? true : false;
    }

    public void updatePosition(NMC2.NMCAXESEXPR status) // 포지션 갱신만을 빠르게 처리
    {
        int axisNo = m_no;

        m_pos = status.dEnc[axisNo];
    }

    public void updateByState(NMC2.NMCSTATEINFO stateInfo)
    {
        int axisNo = m_no;

        update(stateInfo.NmcAxesExpr, stateInfo.HomeFlag);

        m_servoOn = stateInfo.NmcAxesMotOut.nServoOn[axisNo] == 0 ? false : true;
    }

    public void stop()
    {
        this.decStop();
    }

    public void alarmClear()
    {
        this.setAlarmResetOn(true);
        Util.waitTick(100);
        this.setAlarmResetOn(false);
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
    double m_agoNegPos = -1.111d;
    double m_agoPogPos = 1.111d;

    public bool setSoftLimit(bool use, double pogPos, double negPos)
    {
        if (m_orgComplete == false)
        {
            releaseSoftLimit();
            return false;
        }

        if (m_agoSwUse == use &&
            m_agoPogPos == pogPos &&
            m_agoNegPos == negPos)
        {
            return false;
        }
        
        setSwLimitLogic(use, negPos, pogPos);

        m_agoSwUse = use;
        m_agoPogPos = pogPos;
        m_agoNegPos = negPos;

        return true;
    }

    public void releaseSoftLimit()
    {
        bool use = false;
        double negPos = -100.0d;
        double pogPos = 999.99d;

        if (m_agoSwUse == use &&
            m_agoPogPos == pogPos &&
            m_agoNegPos == negPos)
        {
            return;
        }

        setSwLimitLogic(use, negPos, pogPos);

        m_agoSwUse = use;
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

        Debug.warning("PaixMotionAxis::checkFuncRet failed. ret:" + retEnum);
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
