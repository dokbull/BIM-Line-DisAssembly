using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if USE_SE_TP 
public class SEPandent
{
    public enum CONTROLLER
    {
        NONE,
        AJIN,      
        PAIX,
    }
    public enum TP_DIR
    {
        MINUS = 0,
        PLUS = 1,
    }

    CSerial_SE_TP m_serial = null;

    CONTROLLER m_motion = CONTROLLER.NONE;

    PaixLib m_paix = null;
    List<PaixMotionAxis> m_paixAxis = new List<PaixMotionAxis>();
    
    AjinLib m_ajin = null;
    List<AjinMotionAxis> m_ajinAxis = new List<AjinMotionAxis>();

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

    TP_MOTION[] m_motionList = new TP_MOTION[16];

    // PARAMETER
    List<int> m_jogVelocityList = null;
    int m_jogVelocity = 0;

    byte[] m_lastRecvData = null;

    /// <summary>
    /// AXIS NEG, POS 개별로 최대 16개까지 입력할 것. setPaix & setAjin 과 함께 사용해야 함. 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="axisList"></param>
    /// <param name="port"></param>
    public SEPandent(CONTROLLER type, TP_MOTION[] axisList, SerialPort port)
    {
        m_motion = type;

        for (int i = 0; i < axisList.Length; i++)
        {
            if (i > (m_motionList.Length - 1))
                return;

            m_motionList[i] = axisList[i];
        }

        m_serial = new CSerial_SE_TP(port);
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

    void dataReceived(object sender, EventArgs e)
    {
        if (m_motion == CONTROLLER.NONE)
            return;

        if (m_setMotion == false)
            return;

        if (m_use == false) // 사용 불가 상태
            return;

        byte[] data = (byte[])sender;

        if (data.Length < 3) // data currpted
        {
            string dataStr = "";

            for (int i = 0; i < data.Length; i++)
                dataStr += data[i].ToString("X2");

            Debug.debug("SE_Teaching_Pendent::dataReceived currpted. length:" + data.Length + " data:" + dataStr);
            return;
        }

        bool checkData = false;

        if (m_lastRecvData != null)
        {
            for (int i = 0; i < m_lastRecvData.Length; i++)
            {
                if (i > data.Length - 1)
                    continue;

                if (m_lastRecvData[i] != data[i])
                    checkData = true;
            }
        }

        if (checkData == false) // 중복 명령 처리
            return;

        m_lastRecvData = new byte[data.Length];

        for (int i = 0; i < data.Length; i++)
            m_lastRecvData[i] = data[i];

        if (data[0] == 0x0F) // JOG SPEED SET
        {
            int index = (int)data[2];
            int count = m_jogVelocityList.Count();

            if (index > (count - 1))
            {
                Debug.debug("SE_Teaching_Pendent::dataReceived currpted index jog velocity. length:" + count + " index:" + index);
                return;
            }

            m_jogVelocity = m_jogVelocityList[index];
            return;
        }

        if (m_jogVelocityList == null)
        {
            Debug.debug("SE_Teaching_Pendent::dataReceived not set jogVelocity.");
            return;
        }

        if (data[1] == 0x00 && data[2] == 0x00) // JOG STOP
        {
            if (m_motion == CONTROLLER.AJIN)
            {
                m_ajin.allAxisStop();
            }

            if (m_motion == CONTROLLER.PAIX)
            {
                m_paix.allAxisStop(PAIX_STOPMODE.DEACCELACTION);
            }

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

        int id = (int)motion.axisId;
        TP_DIR dir = motion.dir;

        if (m_motion == CONTROLLER.AJIN)
        {
            if (id > (m_ajinAxis.Count - 1))
                return;

            int sign = 1;
            if (dir == TP_DIR.MINUS)
                sign = -1;

            int vel = m_jogVelocity * sign;
            double acc = m_jogVelocity * 4.0d;

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
    }
}
#endif
