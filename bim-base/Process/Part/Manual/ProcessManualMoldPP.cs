using MySqlConnector.Logging;
using System;
using System.Linq.Expressions;

namespace bim_base
{
    public class ProcessManualMoldPP : CProcess
    {
        public enum TARGET
        {
            READY,
            LEFT,
            RIGHT,            
        }

        public enum STEP
        {
            START = 0,

            CHECK_CONDITION,

            MOVE_Z_READY,
            CHECK_Z_READY,

            MOVE_X_TARGET,
            CHECK_X_TARGET,
    
            MOVE_Z_TARGET,
            CHECK_Z_TARGET,

            END,
        }

        STEP m_step = STEP.START;
        STEP m_agoStep = STEP.END;

        ExtAxis X, ZL, ZR;

        TEACH_POS m_teachPos = TEACH_POS.MOLD_PP_WAIT;

        bool m_moveAxisZ = false;

        public ProcessManualMoldPP(ProcessMain procMain) : base(procMain)
        {
            X = main.axis(AXIS.MOLD_PP_X);
            ZL = main.axis(AXIS.MOLD_PP_ZL);
            ZR = main.axis(AXIS.MOLD_PP_ZR);
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
                Debug.debug("ProcessManualMoldPP::run STEP:" + m_step);

            m_agoStep = m_step;

            ModelInfo mc = Common.MC;
            ModelInfo model = Common.MODEL_INFO(Conf.CURR_MODEL_IDX);

            POS mc_readyPos = mc.teachPos(TEACH_POS.MOLD_PP_WAIT);
            POS mc_teachPos = mc.teachPos(m_teachPos);

            POS readyPos = model.teachPos(TEACH_POS.MOLD_PP_WAIT);
            POS teachPos = model.teachPos(m_teachPos);

            bool isDry = main.isDryRun();

            switch (m_step)
            {
                case STEP.START:
                    {
                        m_step = STEP.CHECK_CONDITION;
                    }
                    break;

                case STEP.CHECK_CONDITION:
                    {
                        if (X.inpos() == false)
                            return;

                        if (ZL.inpos() == false || ZR.inpos() == false)
                            return;

                        X.setAbsSpeed(100, 0.25, 0.25);
                        ZL.setAbsSpeed(100, 0.25, 0.25);
                        ZR.setAbsSpeed(100, 0.25, 0.25);

                        m_step = STEP.MOVE_Z_READY;
                    }
                    break;

                case STEP.MOVE_Z_READY:
                    {
                        bool ret = true;
                        ret &= ZL.absMove(mc_readyPos.zL + readyPos.zL);
                        ret &= ZR.absMove(mc_readyPos.zR + readyPos.zR);

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_Z_READY;
                    }
                    break;

                case STEP.CHECK_Z_READY:
                    {
                        if (ZL.inpos() == false || ZR.inpos() == false)
                            return;

                        m_step = STEP.MOVE_X_TARGET;
                    }
                    break;

                case STEP.MOVE_X_TARGET:
                    {
                        bool ret = X.absMove(mc_teachPos.x + teachPos.x);

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_X_TARGET;
                    }
                    break;

                case STEP.CHECK_X_TARGET:
                    {
                        if (X.inpos() == false)
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
                        bool ret = true;

                        ret &= ZL.absMove(mc_teachPos.zL + teachPos.zL);
                        ret &= ZR.absMove(mc_teachPos.zR + teachPos.zR);

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_Z_TARGET;
                    }
                    break;

                case STEP.CHECK_Z_TARGET:
                    {
                        if (ZL.inpos() == false || ZR.inpos() == false)
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
