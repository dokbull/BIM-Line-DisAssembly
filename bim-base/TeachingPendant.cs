#if USE_SE_TP 
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TeachingPendant
{
    public enum CONTROLLER
    {
        NONE,
        AJIN,
        PAIX,
        RSA,
    }
    public enum TP_DIR
    {
        MINUS = 0,
        PLUS = 1,
    }

    CserialTP m_serial = null;

    bool m_isAuto = false;

    CONTROLLER m_motion = CONTROLLER.NONE;
    public event EventHandler JogSpeedChangeEvnet;

    PaixLib m_paix = null;
    List<PaixMotionAxis> m_paixAxis = new List<PaixMotionAxis>();

    AjinLib m_ajin = null;
    List<AjinMotionAxis> m_ajinAxis = new List<AjinMotionAxis>();

    List<RSAMMCEAxis> m_rsaAxis = new List<RSAMMCEAxis>();

    TP_MOTION[] m_motionList = new TP_MOTION[16];

    // PARAMETER
    List<int> m_jogVelocityList = null;
    int m_jogVelocity = 0;

    byte[] m_lastRecvData = null;

    bool m_setMotion = false;
    bool m_use = false;

    public class TP_MOTION
    {
        public AXIS axisId;
        public TP_DIR dir;

        public TP_MOTION(AXIS axis, TP_DIR dir)
        {
            this.axisId = axis;
            this.dir = dir;
        }
    }
    

    /// <summary>
    /// AXIS NEG, POS 개별로 최대 16개까지 입력할 것. setPaix & setAjin 과 함께 사용해야 함. 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="axisList"></param>
    /// <param name="port"></param>
    public TeachingPendant(CONTROLLER type, TP_MOTION[] axisList, SerialPort port)
    {
        m_motion = type;

        for (int i = 0; i < axisList.Length; i++)
        {
            if (i > (m_motionList.Length - 1))
                return;

            m_motionList[i] = axisList[i];
        }

        m_serial = new CserialTP(port);
        m_serial.start();

        m_serial.IncomingData += new EventHandler(dataReceived);
    }

    /// <summary>
    /// PAIX 축 번호 순서대로 입력
    /// </summary>
    /// <param name="lib"></param>
    /// <param name="axisList"></param>
    public void setPaixAxis(PaixLib lib, List<PaixMotionAxis> axisList)
    {
        m_paix = lib;
        m_paixAxis = axisList;

        m_setMotion = true;
    }

    /// <summary>
    /// AJIN 축 번호 순서대로 입력
    /// </summary>
    /// <param name="lib"></param>
    /// <param name="axisList"></param>
    public void setAjinAxis(AjinLib lib, List<AjinMotionAxis> axisList)
    {
        m_ajin = lib;
        m_ajinAxis = axisList;

        m_setMotion = true;
    }

    public void setRsaAxis(RSAMMCELib lib, List<RSAMMCEAxis> axisList)
    {
        m_rsaAxis = axisList;

        m_setMotion = true;
    }

    public void setAuto(bool value)
    {
        m_isAuto = value;
    }

    public void setJogSpeedList(int[] jogVel)
    {
        if (jogVel.Length == 0)
            return;

        m_jogVelocityList = new List<int>();

        for (int i = 0; i < jogVel.Length; i++)
            m_jogVelocityList.Add(jogVel[i]);
    }

    /// <summary>
    /// 조작 가능한 상황에서만 setUse 를 true 선언해주어야 함.
    /// </summary>
    /// <param name="value"></param>
    public void setUse(bool value)
    {
        m_use = value;
    }

    public void close()
    {
        m_serial.stop();
    }

    private string receivePacket(byte[] data)
    {
        string receive = ASCIIEncoding.ASCII.GetString(data);

        return receive;
    }

    public void jogStop()
    {
        if (m_motion == CONTROLLER.AJIN)
        {
            m_ajin.allAxisStop();
        }

        if (m_motion == CONTROLLER.PAIX)
        {
            m_paix.allAxisStop(PAIX_STOPMODE.DEACCELACTION);
        }

        if (m_motion == CONTROLLER.RSA)
        {
            foreach (RSAMMCEAxis axis in m_rsaAxis)
            {
                axis.stop();
            }
        }
    }

    void dataReceived(object sender, EventArgs e)
    {
        Debug.debug("TeachingPendent::dataReceived");

        if (m_motion == CONTROLLER.NONE)
            return;

        if (m_setMotion == false)
            return;

        if (m_use == false) // 사용 불가 상태
            return;

        if (m_isAuto == false)
            return;

        byte[] data = (byte[])sender;
        if (data.Length < 3) // data currpted
        {
            string dataStr = "";

            for (int i = 0; i < data.Length; i++)
                dataStr += data[i].ToString("X2");

            Debug.debug("TeachingPendent::dataReceived currpted. length:" + data.Length + " data:" + dataStr);
            return;
        }

        if (data[0] == 0x0F) // JOG SPEED SET
        {
            int index = (int)data[2];
            int count = m_jogVelocityList.Count();

            if (index > (count - 1))
            {
                Debug.debug("TeachingPendent::dataReceived currpted index jog velocity. length:" + count + " index:" + index);
                return;
            }

            if (JogSpeedChangeEvnet != null)
                JogSpeedChangeEvnet(index, null);

            return;
        }

        if (m_jogVelocityList == null)
        {
            Debug.debug("TeachingPendent::dataReceived not set jogVelocity.");
            return;
        }

        if (data[1] == 0x00 && data[2] == 0x00) // JOG STOP
        {
            jogStop();

            return;
        }

        int axis = -1;

        if (data[2] != 0x00) // AXIS 0~7 조작
        {
            axis = (int)Math.Log(data[2], 2);
        }
        else if (data[1] != 0x00) // AXIS 8~15 조작
        {
            axis = 8 + (int)Math.Log(data[1], 2);
        }

        if (axis < 0 || axis > 15)
            return;

        TP_MOTION motion = m_motionList[axis];

        if (motion == null)
            return;

        int id = (int)motion.axisId;
        TP_DIR dir = motion.dir;

        if (id < 0)
            return;

        if (m_motion == CONTROLLER.AJIN)
        {
            if (id > (m_ajinAxis.Count - 1))
                return;

            if (id >= m_jogVelocityList.Count)
                return;

            int sign = 1;
            if (dir == TP_DIR.MINUS)
                sign = -1;

            int vel = m_jogVelocityList[id] * sign;
            double acc = m_jogVelocityList[id] * 4.0d;

            if (m_ajinAxis[id].accelUnit() == AXM_ACCEL_UNIT.SEC)
                acc = 0.25;

            m_ajinAxis[id].moveVel(vel, acc, acc);
        }

        if (m_motion == CONTROLLER.PAIX)
        {
            if (id > (m_paixAxis.Count - 1))
                return;

            int vel = m_jogVelocity;
            double acc = m_jogVelocity * 4.0d;

            if (m_paixAxis[id].accelUnit() == PAIX_ACCEL_UNIT.SECOND)
                acc = 0.25;

            PAIX_DIR direction = PAIX_DIR.CW; // POS

            if (dir == TP_DIR.MINUS)
                direction = PAIX_DIR.CCW; // NEG

            m_paixAxis[id].setSpeed(0, acc, acc, vel);
            m_paixAxis[id].jogMove(direction);
        }

        if (m_motion == CONTROLLER.RSA)
        {
            if (id > (m_rsaAxis.Count - 1))
                return;

            if (id >= m_jogVelocityList.Count)
                return;

            int sign = 1;
            if (dir == TP_DIR.MINUS)
                sign = -1;

            int vel = m_jogVelocityList[id] * sign;
            double acc = m_jogVelocityList[id] * 4.0d;

            if (m_rsaAxis[id].isCalcSecToAcc() == true)
                acc = 0.25;

            m_rsaAxis[id].moveVel(vel, acc, acc);
        }
    }
}
#endif
