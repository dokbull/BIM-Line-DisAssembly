using MySqlConnector.Logging;
using System;
using System.Linq.Expressions;

namespace bim_base
{
    public class ProcessMoldPP : CProcess
    {
        public enum TARGET
        {
            READY,
            LEFT,
            RIGHT,            
        }

        public enum ACTION
        {
            WAIT,
            PICK_LEFT,
            PICK_RIGHT,
            PICK_BOTH,
            PLACE_LEFT,
            PLACE_RIGHT,
            PLACE_BOTH,
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

            SET_ACTION,
            CHECK_ACTION,

            MOVE_Z_AFTER_READY,
            CHECK_Z_AFTER_READY,

            END,
        }

        STEP m_step = STEP.START;
        STEP m_agoStep = STEP.END;

        ExtAxis X, ZL, ZR;

        TARGET m_target = TARGET.READY;
        ACTION m_action = ACTION.WAIT;

        CElaspedTimer m_actionDelay = new CElaspedTimer(250);
        CElaspedTimer m_cylTimeout = new CElaspedTimer(2 * 1000);

        public ProcessMoldPP(ProcessMain procMain) : base(procMain)
        {
            X = main.axis(AXIS.MOLD_PP_X);
            ZL = main.axis(AXIS.MOLD_PP_ZL);
            ZR = main.axis(AXIS.MOLD_PP_ZR);
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

        public ACTION lastAction() { return m_action; }

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

            ModelInfo mc = Common.MC;
            ModelInfo model = Common.MODEL_INFO(Conf.CURR_MODEL);

            POS mc_readyPos = mc.teachPos(TEACH_POS.MOLD_PP_WAIT);
            POS mc_teachPos = mc.teachPos(TEACH_POS.NONE);

            POS readyPos = model.teachPos(TEACH_POS.MOLD_PP_WAIT);
            POS teachPos = model.teachPos(TEACH_POS.NONE);

            bool isDry = main.isDryRun();

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

                        if (ZL.inpos() == false || ZR.inpos() == false)
                            return;

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
                        bool ret = true;

                        if (m_action == ACTION.PLACE_LEFT || m_action == ACTION.PLACE_BOTH || m_action == ACTION.PICK_LEFT || m_action == ACTION.PICK_BOTH)
                            ret &= ZL.absMove(mc_teachPos.zL + teachPos.zL);

                        if (m_action == ACTION.PLACE_RIGHT || m_action == ACTION.PLACE_BOTH || m_action == ACTION.PICK_RIGHT || m_action == ACTION.PICK_BOTH)
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

                        m_step = STEP.SET_ACTION;
                    }
                    break;

                case STEP.SET_ACTION:
                    {
                        switch (m_action)
                        {
                            case ACTION.PICK_LEFT:
                                main.setOutput(OUTPUT.MOLD_OUT_PP_GRIP_1, true);
                                main.setOutput(OUTPUT.MOLD_OUT_PP_UNGRIP_1, false);
                                break;

                            case ACTION.PICK_RIGHT:
                                main.setOutput(OUTPUT.MOLD_OUT_PP_GRIP_2, true);
                                main.setOutput(OUTPUT.MOLD_OUT_PP_UNGRIP_2, false);
                                break;

                            case ACTION.PICK_BOTH:
                                main.setOutput(OUTPUT.MOLD_OUT_PP_GRIP_1, true);
                                main.setOutput(OUTPUT.MOLD_OUT_PP_UNGRIP_1, false);
                                main.setOutput(OUTPUT.MOLD_OUT_PP_GRIP_2, true);
                                main.setOutput(OUTPUT.MOLD_OUT_PP_UNGRIP_2, false);
                                break;

                            case ACTION.PLACE_LEFT:
                                main.setOutput(OUTPUT.MOLD_OUT_PP_GRIP_1, false);
                                main.setOutput(OUTPUT.MOLD_OUT_PP_UNGRIP_1, true);
                                break;

                            case ACTION.PLACE_RIGHT:
                                main.setOutput(OUTPUT.MOLD_OUT_PP_GRIP_2, false);
                                main.setOutput(OUTPUT.MOLD_OUT_PP_UNGRIP_2, true);
                                break;

                            case ACTION.PLACE_BOTH:
                                main.setOutput(OUTPUT.MOLD_OUT_PP_GRIP_1, false);
                                main.setOutput(OUTPUT.MOLD_OUT_PP_UNGRIP_1, true);
                                main.setOutput(OUTPUT.MOLD_OUT_PP_GRIP_2, false);
                                main.setOutput(OUTPUT.MOLD_OUT_PP_UNGRIP_2, true);
                                break;
                        }

                        m_actionDelay.start();
                        m_cylTimeout.start();

                        m_step = STEP.CHECK_ACTION;
                    }
                    break;

                case STEP.CHECK_ACTION:
                    {
                        if (m_actionDelay.isElasped() == false)
                            return;

                        if (isDry == false)
                        {
                            switch (m_action)
                            {
                                case ACTION.PICK_LEFT:
                                    if (main.input(INPUT.MOLD_OUT_PP_GRIP_1) == false)
                                    {
                                        if (m_cylTimeout.isElasped() == true)
                                        {
                                            addAlarm(ALARM.CY_UNLOADER_MOLDPP_GRIPPER1_CYL_GRIP);
                                            m_step = STEP.SET_ACTION;
                                            return;
                                        }

                                        return;
                                    }
                                    break;

                                case ACTION.PICK_RIGHT:
                                    if (main.input(INPUT.MOLD_OUT_PP_GRIP_2) == false)
                                    {
                                        if (m_cylTimeout.isElasped() == true)
                                        {
                                            addAlarm(ALARM.CY_UNLOADER_MOLDPP_GRIPPER2_CYL_GRIP);
                                            m_step = STEP.SET_ACTION;
                                            return;
                                        }

                                        return;
                                    }
                                    break;

                                case ACTION.PICK_BOTH:
                                    if (main.input(INPUT.MOLD_OUT_PP_GRIP_1) == false || main.input(INPUT.MOLD_OUT_PP_GRIP_2) == false)
                                    {
                                        if (m_cylTimeout.isElasped() == true)
                                        {
                                            if (main.input(INPUT.MOLD_OUT_PP_GRIP_1) == false)
                                                addAlarm(ALARM.CY_UNLOADER_MOLDPP_GRIPPER1_CYL_GRIP);

                                            if (main.input(INPUT.MOLD_OUT_PP_GRIP_2) == false)
                                                addAlarm(ALARM.CY_UNLOADER_MOLDPP_GRIPPER2_CYL_GRIP);

                                            m_step = STEP.SET_ACTION;
                                            return;
                                        }

                                        return;
                                    }
                                    break;

                                case ACTION.PLACE_LEFT:
                                    if (main.input(INPUT.MOLD_OUT_PP_UNGRIP_1) == false)
                                    {
                                        if (m_cylTimeout.isElasped() == true)
                                        {
                                            addAlarm(ALARM.CY_UNLOADER_MOLDPP_GRIPPER1_CYL_UNGRIP);
                                            m_step = STEP.SET_ACTION;
                                            return;
                                        }

                                        return;
                                    }
                                    break;

                                case ACTION.PLACE_RIGHT:
                                    if (main.input(INPUT.MOLD_OUT_PP_UNGRIP_2) == false)
                                    {
                                        if (m_cylTimeout.isElasped() == true)
                                        {
                                            addAlarm(ALARM.CY_UNLOADER_MOLDPP_GRIPPER2_CYL_UNGRIP);
                                            m_step = STEP.SET_ACTION;
                                            return;
                                        }

                                        return;
                                    }
                                    break;

                                case ACTION.PLACE_BOTH:
                                    if (main.input(INPUT.MOLD_OUT_PP_UNGRIP_1) == false || main.input(INPUT.MOLD_OUT_PP_UNGRIP_2) == false)
                                    {
                                        if (m_cylTimeout.isElasped() == true)
                                        {
                                            if (main.input(INPUT.MOLD_OUT_PP_UNGRIP_1) == false)
                                                addAlarm(ALARM.CY_UNLOADER_MOLDPP_GRIPPER1_CYL_UNGRIP);

                                            if (main.input(INPUT.MOLD_OUT_PP_UNGRIP_2) == false)
                                                addAlarm(ALARM.CY_UNLOADER_MOLDPP_GRIPPER2_CYL_UNGRIP);

                                            m_step = STEP.SET_ACTION;
                                            return;
                                        }

                                        return;
                                    }
                                    break;
                            }
                        }

                        m_step = STEP.MOVE_Z_AFTER_READY;
                    }
                    break;

                case STEP.MOVE_Z_AFTER_READY:
                    {
                        bool ret = true;
                        ret &= ZL.absMove(mc_readyPos.zL + readyPos.zL);
                        ret &= ZR.absMove(mc_readyPos.zR + readyPos.zR);

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_Z_AFTER_READY;
                    }
                    break;

                case STEP.CHECK_Z_AFTER_READY:
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
