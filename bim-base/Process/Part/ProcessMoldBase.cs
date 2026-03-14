using MySqlConnector.Logging;
using System;
using System.Linq.Expressions;

namespace bim_base
{
    public class ProcessMoldBase : CProcess
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

            MOVE_X_TARGET,
            CHECK_X_TARGET,

            END,
        }

        STEP m_step = STEP.START;
        STEP m_agoStep = STEP.END;

        ExtAxis X;

        TARGET m_target = TARGET.READY;

        public ProcessMoldBase(ProcessMain procMain) : base(procMain)
        {
            X = main.axis(AXIS.BASE_X);
        }

        public bool start(TARGET target)
        {
            if (m_isRun == true)
                return false;

            m_target = target;

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

            if (main.isAuto() == false)
                return;

            if (m_step != m_agoStep)
                Debug.debug("ProcessMoldPP::run STEP:" + m_step);

            m_agoStep = m_step;

            ModelInfo mc = Common.MC_INFO;
            ModelInfo model = Common.MODEL_INFO;

            POS mc_readyPos = mc.teachPos(TEACH_POS.MOLD_PP_WAIT);
            POS mc_teachPos = mc.teachPos(TEACH_POS.NONE);

            POS readyPos = model.teachPos(TEACH_POS.MOLD_PP_WAIT);
            POS teachPos = model.teachPos(TEACH_POS.NONE);

            switch (m_target)
            {
                case TARGET.READY:
                    mc_teachPos = mc.teachPos(TEACH_POS.MOLD_PP_WAIT);
                    teachPos = model.teachPos(TEACH_POS.MOLD_PP_WAIT);
                    break;

                case TARGET.LEFT:
                    mc_teachPos = mc.teachPos(TEACH_POS.MOLD_PP_LEFT);
                    teachPos = model.teachPos(TEACH_POS.MOLD_PP_LEFT);
                    break;

                case TARGET.RIGHT:
                    mc_teachPos = mc.teachPos(TEACH_POS.MOLD_PP_RIGHT);
                    teachPos = model.teachPos(TEACH_POS.MOLD_PP_RIGHT);
                    break;
            }

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

                        m_step = STEP.MOVE_X_TARGET;
                    }
                    break;

                case STEP.MOVE_X_TARGET:
                    {
                        bool ret = X.absMove(mc_teachPos.xB + teachPos.xB);

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_X_TARGET;
                    }
                    break;

                case STEP.CHECK_X_TARGET:
                    {
                        if (X.inpos() == false)
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
