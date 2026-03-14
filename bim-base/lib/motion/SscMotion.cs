using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

#if USE_COMI_SSCNET
using ComiSSCNET3;

static class SSC
{
    public const int MOTION_BOARD_ID = 0;
}

/// <summary>
/// 
/// </summary>
/// 
public class SscMotion : BaseMotionSsc
{
    bool m_isConnect = false;

    short m_axisNo = 0;

    int numDevices = 0;
    int boardIdList = 0;
    int numServos = 0;

    public SscMotion(int axisNo)
    {
        m_axisNo = (short)axisNo;
    }

    public override void init()
    {
        //각 제어기의 상태를 세팅한다..
        //반드시 상속받아서 재정의하여 쓸 것.
    }

    public override bool isConnected()
    {
        return m_isConnect;
    }

    public override void run()
    {
        throw new NotImplementedException();
    }

    public bool open()
    {
        int open = CMS.GnLoadDevice(ref numDevices, ref boardIdList, ref numServos);

        if (open != 0)
        {
            m_isConnect = false;
            return false;
            //throw new SystemException("SscMotion::SscMotion fail to connect COMINET3");
        }
        return true;
    }

    public bool allServoStart()
    {
        for (int i = 0; i < numServos; i++)
        {
            int servoState = 0;
            CMS.GnGetServoOn(SSC.MOTION_BOARD_ID, i, ref servoState);
            if (servoState == 0)
            {
                int ret = 0;
                ret = CMS.GnSetServoOn(SSC.MOTION_BOARD_ID, i, (int)CMS.SWITCH.ON);
                if (ret == 0)
                {
                    Debug.warning("SscMotion::SScmotion allServoOn fail. servo no : " + i);
                    m_isConnect = false;
                    return false;
                }
            }
        }

        m_isConnect = true;
        return true;
    }

    public void motionConfig(int axis, int speedMode, double pulse, double homeSpeed, int homeMode, 
                            double acc, double dec, int orgType, int pLimType, int mLimType, int inPosition)
    {
        double fRvsSpeed;
        fRvsSpeed = homeSpeed / 10;

        CMS.CfgSetUnitDist(SSC.MOTION_BOARD_ID, axis, pulse);
        CMS.HomeSetConfig(SSC.MOTION_BOARD_ID, axis, (int)CMS.HomeParamId.DIR, homeMode); // homeMode : -Near(0), +Near(1)
        CMS.HomeSetConfig(SSC.MOTION_BOARD_ID, axis, (int)CMS.HomeParamId.MODE, homeMode);
        CMS.CfgSetMioProperty(SSC.MOTION_BOARD_ID, axis, (int)CMS.MIOID.ORG_LOGIC, orgType); //ORG : A접점(0), B접점(1)
        CMS.CfgSetMioProperty(SSC.MOTION_BOARD_ID, axis, (int)CMS.MIOID.PEL_LOGIC, pLimType); //+LMT : A접점(0), B접점(1)
        CMS.CfgSetMioProperty(SSC.MOTION_BOARD_ID, axis, (int)CMS.MIOID.NEL_LOGIC, mLimType); //-LMT : A접점(0), B접점(1)
        CMS.CfgSetMioProperty(SSC.MOTION_BOARD_ID, axis, (int)CMS.MIOID.INP_EN, inPosition); //Inposition : X(0), 사용(1)
        CMS.HomeSetSpeedPattern(SSC.MOTION_BOARD_ID, axis, 0, speedMode, homeSpeed, acc, dec); //speedMode : 정속(0), 사다리꼴(1), S-curve(2)
        CMS.HomeSetSpeedPattern(SSC.MOTION_BOARD_ID, axis, 1, speedMode, fRvsSpeed, acc, dec);
        CMS.HomeSetSpeedPattern(SSC.MOTION_BOARD_ID, axis, 2, speedMode, fRvsSpeed, acc, dec);

        Debug.debug("ComiMotion::motionConfig set axis:" + axis + " pulse:" + pulse + " acc:" + acc + " dec:" + dec);
    }

    public bool close()
    {
        int ret = 0;
        ret = CMS.GnUnloadDevice();
        m_isConnect = false;
        return (ret == 0) ? false : true;;
    }

    public bool checkConnect()
    {
        if (m_isConnect == false)
        {
            if (open() == false)
                return false;
        }

        return true;
    }

    public bool checkResult(int ret)
    {
        return false;
    }

    public bool initiial()
    {
        if (checkConnect() == false) 
            return false;

        return true;
    }

    public bool Stop(short nAxis)
    {
        if (m_isConnect == false)
        {
            if (open() == false) 
                return false;
        }

        int[] axis = new int[1];
        axis[0] = (int)nAxis;

        int ret = 0;
        ret = CMS.MxStop(SSC.MOTION_BOARD_ID, 1, axis);
        return (ret == 0) ? false : true;
    }

    public bool jogStop(short nAxis)
    {
        if (m_isConnect == false)
        {
            if (open() == false)
                return false;
        }

        int[] axis = new int[1];

        int ret = 0;
        ret = CMS.SxStop(SSC.MOTION_BOARD_ID, (int)nAxis);
        return (ret == 0) ? false : true;
    }

    public bool AllStop()
    {
        if (m_isConnect == false)
        {
            if (open() == false) return false;
        }

        int[] axis = new int[numServos];

        for (int i = 0; i < numServos; i++)
        {
            axis[i] = i;
        }

        int ret = 0;
        ret = CMS.MxStop(SSC.MOTION_BOARD_ID, axis.Length, axis);
        return (ret == 0) ? false : true;
    }

    public bool JogMove(short nAxis, short nDir)
    {
        if (m_isConnect == false)
        {
            if (open() == false) 
                return false;
        }

        int ret = 0;
        ret = CMS.SxVMoveStart(SSC.MOTION_BOARD_ID, nAxis, nDir);
        return (ret == 0) ? false : true;
    }

    public bool HomeMove(int nAxis)
    {
        if (m_isConnect == false)
        {
            if (open() == false) 
                return false;
        }

        int ret = 0;
        ret = CMS.HomeMoveStart(SSC.MOTION_BOARD_ID, nAxis);
        return (ret == 0) ? false : true;
    }

    public bool SetSpeed(int nAxis, int speedMode, double pulse, double speed, double acc, double dec)
    {
        if (m_isConnect == false)
        {
            if (open() == false) 
                return false;
        }

        double pulseSpeed = 0;
        pulseSpeed = speed * pulse;

        int ret = 0;
        ret = CMS.CfgSetSpeedPattern(SSC.MOTION_BOARD_ID, nAxis, speedMode, pulseSpeed, acc, dec, 0, 0);
        return (ret == 0) ? false : true;
    }

    public bool getHomeFlag(int axis)
    {
        if (m_isConnect == false)
        {
            if (open() == false) 
                return false;
        }

        int complete = 0;
        CMS.HomeGetSuccess(SSC.MOTION_BOARD_ID, axis, ref complete);

        return (complete == 0) ? false : true;
    }


    public bool absMove(int axisNo, double pos)
    {
        if (m_isConnect == false)
        {
            if (open() == false)
                return false;
        }

        int ret = 0;
        ret = CMS.SxMoveToStart(SSC.MOTION_BOARD_ID, axisNo, pos);
        return (ret != 0) ? false : true;
    }

    public bool isMove(int axisNo)
    {
        if (m_isConnect == false)
        {
            if (open() == false)
                return false;
        }
        
        int move = 0;
        CMS.SxIsDone(SSC.MOTION_BOARD_ID, axisNo, ref move);

        return (move == 0) ? false : true;
    }
	
    public bool setAlarmResetOn(int axisNo)
    {
        if (m_isConnect == false)
        {
            if (open() == false)
                return false;
        }

        int ret = 0;
        ret = CMS.GnSetAlarmRes(SSC.MOTION_BOARD_ID, axisNo);

        return (ret != 0) ? false : true;
    }

    public bool setServoOn(int axisNo, bool value)
    {
        if (checkConnect() == false)
            return false;

        int val = (int)(value ? 1 : 0);

        int ret = 0;
        ret = CMS.GnSetServoOn(SSC.MOTION_BOARD_ID, axisNo, val);
        return (ret == 0) ? false : true; 
    }
}
#endif