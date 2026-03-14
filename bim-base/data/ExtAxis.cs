namespace bim_base
{
    public class ExtAxis : AjinMotionAxis
    {
        ProcessMain main = null;
        AjinMotionAxis m_axis = null;

        AXIS m_axisEnum = AXIS.MAX;
        AXT_MOTION_MOVE_DIR m_homeDir = AXT_MOTION_MOVE_DIR.DIR_CCW;

        CElaspedTimer m_moveTimer = new CElaspedTimer(60 * 1000);

        public ExtAxis(ProcessMain procMain, int no, string axisName) : 
            base(no, axisName)
        {
            main = procMain;
            m_axisEnum = (AXIS)no;

            m_axis = this;
        }

        public AjinMotionAxis axis()
        {
            return m_axis;
        }

        public void setHomeDir(AXT_MOTION_MOVE_DIR dir)
        {
            m_homeDir = dir;
        }

        public AXT_MOTION_MOVE_DIR homeDir()
        {
            return m_homeDir;
        }

        bool checkInterlock()
        {
            ModelInfo model = Common.MODEL_INFO(Conf.CURR_MODEL_IDX);

            if (m_axisEnum == AXIS.MOLD_PP_X)
            {
                ExtAxis ZR = main.axis(AXIS.MOLD_PP_ZR);
                ExtAxis ZL = main.axis(AXIS.MOLD_PP_ZL);
                ExtAxis CHECK_Y = main.axis(AXIS.UB_PP_Y);

                double readyPosL = model.teachData(TEACH_POS.MOLD_PP_WAIT).zL - 1.0d;
                double readyPosR = model.teachData(TEACH_POS.MOLD_PP_WAIT).zR - 1.0d;

                double crashArea = model.teachData(TEACH_POS.UB_PP_WAIT).y + 10.0d;

                if (ZL.pos() < readyPosL)
                    return false;

                if (ZR.pos() < readyPosR)
                    return false;

                if (CHECK_Y.pos() > crashArea)
                    return false;

                if (main.input(INPUT.UB_OUT_PP_BWD) == false)
                    return false;
            }

            if (m_axisEnum == AXIS.UB_PP_Y)
            {
                ExtAxis Z = main.axis(AXIS.UB_PP_Z);
                ExtAxis CHECK_X = main.axis(AXIS.MOLD_PP_X);

                double readyPos = model.teachData(TEACH_POS.UB_PP_WAIT).z - 1.0d;
                double crashArea = model.teachData(TEACH_POS.MOLD_PP_RIGHT).x - 10.0d;

                if (Z.pos() < readyPos)
                    return false;

                if (CHECK_X.pos() < crashArea)
                    return false;
            }

            if (m_axisEnum == AXIS.IN_PP_Y)
            {
                ExtAxis Z = main.axis(AXIS.IN_PP_Z);

                double readyPos = model.teachData(TEACH_POS.PICK_PP_WAIT).z - 1.0d;

                if (Z.pos() < readyPos)
                    return false;
            }

            return true;
        }

        public bool isMoving()
        {
            return m_moveTimer.isStart();
        }

        public override bool absMove(double pos)
        {
            if (m_axis.isServoOn() == false)
                return false;

            if (m_axis.orgComplete() == false)
                return false;

            if (checkInterlock() == false)
                return false;

            m_moveTimer.start();

            return base.absMove(pos);
        }

        public bool absMove(double pos, double vel)
        {
            if (m_axis.isServoOn() == false)
                return false;

            if (m_axis.orgComplete() == false)
                return false;

            if (checkInterlock() == false)
                return false;

            m_moveTimer.start();

            double acc = Conf.acc(m_axisEnum);
            double dec = Conf.dec(m_axisEnum);

            return base.absMove(pos, vel, acc, dec);
        }

        public override void run()
        {
            base.run();

            if (this.inpos())
            {
                m_moveTimer.stop();
            }
            
            if (m_moveTimer.isStart() == false)
                return;

            if (m_moveTimer.isElasped() == false)
                return;

            if (main.isAuto() == false)
            {
                m_moveTimer.stop();
                return;
            }

            main.addAlarm(ALARM.MO_UNLOADER_UBPP_AXIS_Z_MOVE_TIMEOUT + no());
            m_moveTimer.stop();
        }
    }//class
}//namespace
