using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Paix_MotionController;

/// <summary>
/// init() 함수는 반드시 상속으로 재정의 받아서 Limit 센서 및 원점 방향을 설정해 주도록 할 것.
/// 기타 사항은 ExamplePaixMotion 을 참고할 것.
/// </summary>
class PaixMotion : BaseMotion
{
    string m_ip = "";
    short m_no = 0;

    bool m_isConnect = false;

    short m_axisNo = 0;

    public PaixMotion(string ip, int axisNo)
    {
        m_axisNo = (short)axisNo;
        m_ip = ip;

        string[] split = m_ip.Split('.');

        if (split.Length != 4)
            throw new InvalidCastException("PaixMotion::PaixMotion invalid ip");

        try
        {
            m_no = Convert.ToInt16(split[3]);
        }
        catch (Exception)
        {
            throw new InvalidCastException("PaixMotion::PaixMotion invalid ip");
        }
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
        string[] splitStr = m_ip.Split('.');
        short[] ipAddr = new short[4];

        for (int i = 0; i < 4; i++)
        {
            ipAddr[i] = Convert.ToInt16(splitStr[i]);
        }

        NMC2.nmc_SetIPAddress(m_no, ipAddr[0], ipAddr[1], ipAddr[2]);

        if (NMC2.nmc_PingCheck(m_no, ipAddr[3]) != 0)
        {
            Debug.warning("PaixMotion::open ping check error. ip:" + m_ip);
            return false;
        }

        if (NMC2.nmc_OpenDevice(m_no) == 0)
            m_isConnect = true;
        else
            m_isConnect = false;

        return m_isConnect;
    }

    public bool close()
    {
        NMC2.nmc_CloseDevice(m_no);
        m_isConnect = false;
        return true;
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
        if (ret == NMC2.NMC_OK)
            return true;

        if (ret == NMC2.NMC_NOTCONNECT)
            m_isConnect = false;

        return false;
    }

    public bool initiial()
    {
        if (checkConnect() == false) return false;

        short ret;

        // CURRENT ON
        ret = NMC2.nmc_SetCurrentOn(m_no, m_axisNo, 1);
        
        if (checkResult(ret) == false) 
            return false;

        // SERVO ON
        ret = NMC2.nmc_SetServoOn(m_no, m_axisNo, 1);

        if (checkResult(ret) == false)
            return false;

        ret = NMC2.nmc_SetAlarmResetOn(m_no, m_axisNo, 1);

        if (checkResult(ret) == false)
            return false;

        // 알람 리셋, dcc 리셋

        return true;
    }

    public bool GetNmcStatus(ref NMC2.NMCAXESEXPR pNmcData)
    {
        if (m_isConnect == false)
        {
            if (open() == false) return false;
        }
        short nRet = NMC2.nmc_GetAxesExpress(m_no, out pNmcData);

        switch (nRet)
        {
            case NMC2.NMC_NOTCONNECT:
                m_isConnect = false;
                return false;
            case 0:
                return true;
        }

        return false;

    }

    public bool Stop(short nAxis)
    {
        if (m_isConnect == false)
        {
            if (open() == false) return false;
        }
        short nRet = NMC2.nmc_DecStop(m_no, nAxis); // nmc_DecStop 으로 변경
        switch (nRet)
        {
            case NMC2.NMC_NOTCONNECT:
                m_isConnect = false;
                return false;
            case 0:
                return true;
        }

        return false;
    }

    public bool AllStop()
    {
        if (m_isConnect == false)
        {
            if (open() == false) return false;
        }
        short nRet = NMC2.nmc_AllAxisStop(m_no, 0);
        switch (nRet)
        {
            case NMC2.NMC_NOTCONNECT:
                m_isConnect = false;
                return false;
            case 0:
                return true;
        }

        return false;
    }

    public bool JogMove(short nAxis, short nDir)
    {
        if (m_isConnect == false)
        {
            if (open() == false) return false;
        }
        short nRet = NMC2.nmc_JogMove(m_no, nAxis, nDir);
        switch (nRet)
        {
            case NMC2.NMC_NOTCONNECT:
                m_isConnect = false;
                return false;
            case 0:
                return true;
        }

        return false;
    }

    public bool HomeMove(short nAxis, int nHomeMode)
    {
        if (m_isConnect == false)
        {
            if (open() == false) return false;
        }
        short nRet = NMC2.nmc_HomeMove(m_no, nAxis, (short)nHomeMode, 3, 0, 0);
        switch (nRet)
        {
            case NMC2.NMC_NOTCONNECT:
                m_isConnect = false;
                return false;
            case 0:
                return true;
        }

        return false;

    }

    public bool SetSpeed(int nAxis, double dStart, double dAcc, double dDec, double dMax)
    {
        if (m_isConnect == false)
        {
            if (open() == false) return false;
        }
    
        short nRet = NMC2.nmc_SetSpeed(m_no, (short)nAxis, dStart, dAcc, dDec, dMax);
        switch (nRet)
        {
            case NMC2.NMC_NOTCONNECT:
                m_isConnect = false;
                return false;
            case 0:
                return true;
        }

        return false;
    }

    public bool getHomeFlag(ref NMC2.NMCHOMEFLAG homeFlag)
    {
        if (m_isConnect == false)
        {
            if (open() == false) return false;
        }

        short ret = NMC2.nmc_GetHomeStatus(m_no, out homeFlag);

        if (ret == NMC2.NMC_NOTCONNECT)
        {
            m_isConnect = false;
            return false;
        }

        return true;
    }

    /// <summary>
    /// Home 속도 조절
    /// </summary>
    /// <param name="nAxisNo"></param>
    /// <param name="dHomeSpeed0"></param>
    /// <param name="dHomeSpeed1"></param>
    /// <param name="dHomeSpeed2"></param>
    /// <param name="dOffsetSpeed"></param>
    /// <returns></returns>
    public bool SetHomeSpeedEx(short nAxisNo,double dHomeSpeed0, double dHomeSpeed1, double dHomeSpeed2,double dOffsetSpeed)
    {
        if (m_isConnect == false)
        {
            if (open() == false) return false;
        }

        short nRet = NMC2.nmc_SetHomeSpeedEx(m_no, nAxisNo, dHomeSpeed0, dHomeSpeed1, dHomeSpeed2, dOffsetSpeed);
        switch (nRet)
        {
            case NMC2.NMC_NOTCONNECT:
                m_isConnect = false;
                return false;
            case 0:
                return true;
        }

        return false;
    }

    public bool absMove(int axisNo, double pos)
    {
        if (m_isConnect == false)
        {
            if (open() == false)
                return false;
        }

        short nRet = NMC2.nmc_AbsMove(m_no, (short)axisNo, pos);

        if (nRet == NMC2.NMC_NOTCONNECT)
        {
            m_isConnect = false;
            return false;
        }

        return true;
    }
	
    public bool setAlarmResetOn(int axisNo)
    {
        if (m_isConnect == false)
        {
            if (open() == false)
                return false;
        }

        short ret = NMC2.nmc_SetAlarmResetOn(m_no, (short)axisNo, 1);

        Thread.Sleep(500);

        ret = NMC2.nmc_SetAlarmResetOn(m_no, (short)axisNo, 0);

        if (ret == NMC2.NMC_NOTCONNECT)
        {
            m_isConnect = false;
            return false;
        }

        return true;
    }

    public bool setSWLimit(int axisNo, bool use, double minusPos, double plusPos)
    {
        if (checkConnect() == false)
            return false;

        int ret = NMC2.nmc_SetSWLimitLogic(m_no, (short)axisNo, (short)((use == true) ? 1 : 0), minusPos, plusPos);

        return checkResult(ret);
    }

    public bool setUnitPerPulse(int axisNo, double value)
    {
        if (checkConnect() == false)
            return false;

        if (value > 0.999d)
        {
            throw new ArgumentOutOfRangeException("1 이상의 값은 들어갈 수 없습니다.");
        }

        int ret = NMC2.nmc_SetUnitPerPulse(m_no, (short)axisNo, value);

        return checkResult(ret);
    }

    public bool setServoOn(int axisNo, bool value)
    {
        if (checkConnect() == false)
            return false;

        short val = (short)(value ? 1 : 0);

        int ret = NMC2.nmc_SetServoOn(m_no, (short)axisNo, val);

        return checkResult(ret);
        
    }

    public bool setServoCmdPos(int axisNo, double value)
    {
        if (checkConnect() == false)
            return false;

        int ret = NMC2.nmc_AbsOver(m_no, (short)axisNo, value);

        return checkResult(ret);
        
    }

    public bool setCurrentOn(int axisNo, bool value)
    {
        if (checkConnect() == false)
            return false;

        short val = (short)(value ? 1 : 0);

        int ret = NMC2.nmc_SetCurrentOn(m_no, (short)axisNo, val);

        return checkResult(ret);
    }
}
