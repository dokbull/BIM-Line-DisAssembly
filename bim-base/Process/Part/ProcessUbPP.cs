using MySqlConnector.Logging;
using System;
using System.Linq.Expressions;

namespace bim_base
{
    public class ProcessUbPP : CProcess
    {
        public enum TARGET
        {
            READY,
            PICK,
            PLACE_F,
            PLACE_R,
        }

        public enum ACTION
        {
            WAIT,
            PICK,
            PLACE,
        }

        public enum STEP
        {
            START = 0,

            CHECK_CONDITION,

            MOVE_Z_READY,
            CHECK_Z_READY,

            SET_ACTION_CYL,
            MOVE_Y_TARGET,
            CHECK_Y_TARGET,
            CHECK_ACTION_CYL,

            MOVE_Z_TARGET,
            CHECK_Z_TARGET,

            SET_ACTION_VAC,
            CHECK_ACTION_VAC,

            MOVE_Z_AFTER_READY,
            CHECK_Z_AFTER_READY,

            END,
        }

        STEP m_step = STEP.START;
        STEP m_agoStep = STEP.END;

        ExtAxis Y, Z;

        TARGET m_target = TARGET.READY;
        ACTION m_action = ACTION.WAIT;

        CElaspedTimer m_actionDelay = new CElaspedTimer(250);
        CElaspedTimer m_cylTimeout = new CElaspedTimer(5 * 1000);

        public ProcessUbPP(ProcessMain procMain) : base(procMain)
        {
            Y = main.axis(AXIS.UB_PP_Y);
            Z = main.axis(AXIS.UB_PP_Z);
        }

        public bool start(TARGET target, ACTION act)
        {
            if (m_isRun == true)
                return false;

            m_target = target;
            m_action = act;

            m_step = STEP.START;

            return base.start();
        }

        public override void stop()
        {
            base.stop();
        }

        public STEP step() { return m_step; }
        public STEP agoStep() { return m_agoStep; }

        public TARGET lastTarget()
        {
            return m_target;
        }

        public override void run()
        {
            if (m_isRun == false)
                return;

            if (main.isAlarm())
                return;

            if (main.isAuto() == false)
                return;

            if (m_step != m_agoStep)
                Debug.debug("ProcessUbPP::run STEP:" + m_step);

            m_agoStep = m_step;

            ModelInfo mc = Common.MC;
            ModelInfo model = Common.MODEL_INFO(Conf.CURR_MODEL_IDX);

            POS mc_readyPos = mc.teachPos(TEACH_POS.UB_PP_WAIT);
            POS mc_teachPos = mc.teachPos(TEACH_POS.NONE);

            POS readyPos = model.teachPos(TEACH_POS.UB_PP_WAIT);
            POS teachPos = model.teachPos(TEACH_POS.NONE);

            bool isDry = main.isDryRun();

            switch (m_target)
            {
                case TARGET.READY:
                    mc_teachPos = mc.teachPos(TEACH_POS.UB_PP_WAIT);
                    teachPos = model.teachPos(TEACH_POS.UB_PP_WAIT);
                    break;

                case TARGET.PICK:
                    mc_teachPos = mc.teachPos(TEACH_POS.UB_PP_PICK);
                    teachPos = model.teachPos(TEACH_POS.UB_PP_PICK);
                    break;

                case TARGET.PLACE_F:
                    mc_teachPos = mc.teachPos(TEACH_POS.UB_PP_PLACE_FRONT);
                    teachPos = model.teachPos(TEACH_POS.UB_PP_PLACE_FRONT);
                    break;

                case TARGET.PLACE_R:
                    mc_teachPos = mc.teachPos(TEACH_POS.UB_PP_PLACE_REAR);
                    teachPos = model.teachPos(TEACH_POS.UB_PP_PLACE_REAR);
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
                        if (Y.inpos() == false)
                            return;

                        if (Z.inpos() == false)
                            return;

                        main.setOutput(OUTPUT.MOLD_SHUTTLE_BLOWER, false);
                        main.setOutput(OUTPUT.UB_OUT_PP_BLOWER, false);

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

                        m_step = STEP.SET_ACTION_CYL;
                    }
                    break;

                case STEP.SET_ACTION_CYL:
                    {
                        switch (m_action)
                        {
                            case ACTION.PICK:
                                main.setOutput(OUTPUT.UB_OUT_PP_FWD, true);
                                main.setOutput(OUTPUT.UB_OUT_PP_BWD, false);
                                break;

                            case ACTION.WAIT:
                            case ACTION.PLACE:
                                main.setOutput(OUTPUT.UB_OUT_PP_FWD, false);
                                main.setOutput(OUTPUT.UB_OUT_PP_BWD, true);
                                break;
                        }

                        m_actionDelay.start();

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

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_ACTION_CYL;
                    }
                    break;

                case STEP.CHECK_ACTION_CYL:
                    {
                        if (m_actionDelay.isElasped() == false)
                            return;

                        switch (m_action)
                        {
                            case ACTION.PICK:
                                if (main.input(INPUT.UB_OUT_PP_FWD) == false)
                                {
                                    if (m_cylTimeout.isElasped() == true)
                                    {
                                        addAlarm(ALARM.CY_UNLOADER_UBPP_PICKER_CYL_FWD);
                                        m_step = STEP.SET_ACTION_CYL;
                                        return;
                                    }

                                    return;
                                }
                                break;
                                
                            case ACTION.PLACE:

                                if (main.input(INPUT.UB_OUT_PP_BWD) == false)
                                {
                                    if (m_cylTimeout.isElasped() == true)
                                    {
                                        addAlarm(ALARM.CY_UNLOADER_UBPP_PICKER_CYL_BWD);
                                        m_step = STEP.SET_ACTION_CYL;
                                        return;
                                    }

                                    return;
                                }
                                break;
                        }

                        if (m_action == ACTION.WAIT)
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

                        if (m_action == ACTION.PICK)
                            main.setOutput(OUTPUT.UB_OUT_PP_VAC, true);

                        m_step = STEP.CHECK_Z_TARGET;
                    }
                    break;

                case STEP.CHECK_Z_TARGET:
                    {
                        if (Z.inpos() == false)
                            return;

                        m_step = STEP.SET_ACTION_VAC;
                    }
                    break;

                case STEP.SET_ACTION_VAC:
                    {
                        switch (m_action)
                        {
                            case ACTION.PICK:
                                main.setOutput(OUTPUT.MOLD_SHUTTLE_BLOWER, true);
                                main.setOutput(OUTPUT.UB_OUT_PP_BLOWER, true);
                                break;

                            case ACTION.PLACE:
                                main.setOutput(OUTPUT.UB_OUT_PP_VAC, false);

                                if (m_target == TARGET.PLACE_F)
                                    setReverseVac1();

                                if (m_target == TARGET.PLACE_R)
                                    setReverseVac2();

                                break;
                        }

                        if (m_action == ACTION.PICK)
                        {
                            m_actionDelay.setTime(2000);
                        }
                        else
                        {
                            m_actionDelay.setTime(250);
                        }

                        m_cylTimeout.start();
                        m_actionDelay.start();
                        
                        m_step = STEP.CHECK_ACTION_VAC;
                    }
                    break;

                case STEP.CHECK_ACTION_VAC:
                    {
                        if (m_actionDelay.isElasped() == false)
                            return;

                        if (isDry == false)
                        {
                            switch (m_action)
                            {
                                case ACTION.PICK:
                                    if (main.input(INPUT.UB_OUT_PP_VAC) == false)
                                    {
                                        if (m_cylTimeout.isElasped() == true)
                                        {
                                            addAlarm(ALARM.VC_UNLOADER_UBPP_PICKER_ON);
                                            m_step = STEP.SET_ACTION_VAC;
                                            return;
                                        }

                                        return;
                                    }
                                    break;

                                case ACTION.PLACE:
                                    if (isVac() == true)
                                    {
                                        if (m_cylTimeout.isElasped() == true)
                                        {
                                            addAlarm(ALARM.VC_UNLOADER_UBPP_PICKER_OFF);
                                            m_step = STEP.SET_ACTION_VAC;
                                            return;
                                        }

                                        return;
                                    }

                                    if (m_target == TARGET.PLACE_F)
                                    {
                                        if (isReverseVac1() == false)
                                        {
                                            if (m_cylTimeout.isElasped() == true)
                                            {
                                                addAlarm(ALARM.VC_REVERSE_UBREVERSE_HOLDER1_ON);
                                                m_step = STEP.SET_ACTION_VAC;
                                                return;
                                            }

                                            return;
                                        }
                                    }

                                    if (m_target == TARGET.PLACE_R)
                                    {
                                        if (isReverseVac2() == false)
                                        {
                                            if (m_cylTimeout.isElasped() == true)
                                            {
                                                addAlarm(ALARM.VC_REVERSE_UBREVERSE_HOLDER2_ON);
                                                m_step = STEP.SET_ACTION_VAC;
                                                return;
                                            }

                                            return;
                                        }
                                    }

                                    break;
                            }
                        }
                        
                        m_step = STEP.MOVE_Z_AFTER_READY;
                    }
                    break;

                case STEP.MOVE_Z_AFTER_READY:
                    {
                        bool ret = Z.absMove(mc_readyPos.z + readyPos.z);

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_Z_AFTER_READY;
                    }
                    break;

                case STEP.CHECK_Z_AFTER_READY:
                    {
                        if (Z.inpos() == false)
                            return;

                        if (m_action == ACTION.PICK)
                        {
                            main.setOutput(OUTPUT.MOLD_SHUTTLE_BLOWER, false);
                            main.setOutput(OUTPUT.UB_OUT_PP_BLOWER, false);
                        }

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

        public void setVac()
        {
            setOutput(OUTPUT.UB_OUT_PP_VAC, true);
        }

        public void setUnvac()
        {
            setOutput(OUTPUT.UB_OUT_PP_VAC, false);
        }

        public bool isVac()
        {
            return input(INPUT.UB_OUT_PP_VAC);
        }

        public void setReverseVac1()
        {
            setOutput(OUTPUT.UB_OUT_REVERSE_VAC_1, true);
        }
        public void setReverseVac2()
        {
            setOutput(OUTPUT.UB_OUT_REVERSE_VAC_2, true);
        }

        public bool isReverseVac1()
        {
            return input(INPUT.UB_OUT_REVERSE_VAC_1);
        }

        public bool isReverseVac2()
        {
            return input(INPUT.UB_OUT_REVERSE_VAC_2);
        }
    }//class
}//namespace
