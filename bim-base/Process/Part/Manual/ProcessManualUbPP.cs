using MySqlConnector.Logging;
using System;
using System.Linq.Expressions;

namespace bim_base
{
    public class ProcessManualUbPP : CProcess
    {
        public enum TARGET
        {
            READY,
            PICK,
            PLACE_F,
            PLACE_R,
        }

        public enum STEP
        {
            START = 0,

            CHECK_CONDITION,

            MOVE_Z_READY,
            CHECK_Z_READY,

            MOVE_Y_TARGET,
            CHECK_Y_TARGET,

            MOVE_Z_TARGET,
            CHECK_Z_TARGET,

            END,
        }

        STEP m_step = STEP.START;
        STEP m_agoStep = STEP.END;

        ExtAxis Y, Z;

        TEACH_POS m_teachPos = TEACH_POS.UB_PP_WAIT;

        bool m_moveAxisZ = false;

        public ProcessManualUbPP(ProcessMain procMain) : base(procMain)
        {
            Y = main.axis(AXIS.UB_PP_Y);
            Z = main.axis(AXIS.UB_PP_Z);
        }

        public bool start(TEACH_POS pos, bool moveAxisZ = false)
        {
            if (m_isRun == true)
                return false;

            m_teachPos = pos;
            m_moveAxisZ = moveAxisZ;

            m_step = STEP.START;

            return base.start();
        }

        public override void stop()
        {
            base.stop();
        }

        public STEP step() { return m_step; }
        public STEP agoStep() { return m_agoStep; }

        public override void run()
        {
            if (m_isRun == false)
                return;

            if (main.isAlarm())
                return;

            if (main.isAuto() == true)
                return;

            if (m_step != m_agoStep)
                Debug.debug("ProcessManualUbPP::run STEP:" + m_step);

            m_agoStep = m_step;

            ModelInfo mc = Common.MC;
            ModelInfo model = Common.MODEL_INFO(Conf.CURR_MODEL_IDX);

            POS mc_readyPos = mc.teachPos(TEACH_POS.UB_PP_WAIT);
            POS mc_teachPos = mc.teachPos(m_teachPos);

            POS readyPos = model.teachPos(TEACH_POS.UB_PP_WAIT);
            POS teachPos = model.teachPos(m_teachPos);

            switch (m_step)
            {
                case STEP.START:
                    {
                        m_step = STEP.CHECK_CONDITION;
                    }
                    break;

                case STEP.CHECK_CONDITION:
                    {
                        if (Y.inpos() == false)
                            return;

                        if (Z.inpos() == false)
                            return;

                        Y.setAbsSpeed(100, 0.25, 0.25);
                        Z.setAbsSpeed(100, 0.25, 0.25);

                        m_step = STEP.MOVE_Z_READY;
                    }
                    break;

                case STEP.MOVE_Z_READY:
                    {
                        bool ret = Z.absMove(mc_readyPos.z + readyPos.z);

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_Z_READY;
                    }
                    break;

                case STEP.CHECK_Z_READY:
                    {
                        if (Z.inpos() == false)
                            return;

                        m_step = STEP.MOVE_Y_TARGET;
                    }
                    break;

                case STEP.MOVE_Y_TARGET:
                    {
                        bool ret = Y.absMove(mc_teachPos.y + teachPos.y);

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_Y_TARGET;
                    }
                    break;

                case STEP.CHECK_Y_TARGET:
                    {
                        if (Y.inpos() == false)
                            return;

                        if (m_moveAxisZ == false)
                        {
                            m_step = STEP.END;
                            return;
                        }

                        m_step = STEP.MOVE_Z_TARGET;
                    }
                    break;

                case STEP.MOVE_Z_TARGET:
                    {
                        bool ret = Z.absMove(mc_teachPos.z + teachPos.z);

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_Z_TARGET;
                    }
                    break;

                case STEP.CHECK_Z_TARGET:
                    {
                        if (Z.inpos() == false)
                            return;

                        m_step = STEP.END;
                    }
                    break;

                case STEP.END:
                    {
                        m_isRun = false;
                    }
                    break;
            }
        }//run
    }//class
}//namespace
