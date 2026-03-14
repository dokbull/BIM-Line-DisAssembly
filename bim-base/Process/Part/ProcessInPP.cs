using MySqlConnector.Logging;
using System;
using System.Linq.Expressions;

namespace bim_base
{
    public class ProcessInPP : CProcess
    {
        public enum TARGET
        {
            READY,
            PICK,
            PLACE,            
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

            MOVE_Y_TARGET,
            CHECK_Y_TARGET,
    
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

        ExtAxis Y, Z;

        TARGET m_target = TARGET.READY;
        ACTION m_action = ACTION.WAIT;

        CElaspedTimer m_actionDelay = new CElaspedTimer(500);
        CElaspedTimer m_cylTimeout = new CElaspedTimer(2 * 1000);

        public ProcessInPP(ProcessMain procMain) : base(procMain)
        {
            Y = main.axis(AXIS.IN_PP_Y);
            Z = main.axis(AXIS.IN_PP_Z);
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

        public override void run()
        {
            if (m_isRun == false)
                return;

            if (main.isAlarm())
                return;

            if (main.isAuto() == false)
                return;

            if (m_step != m_agoStep)
                Debug.debug("ProcessInPP::run STEP:" + m_step);

            m_agoStep = m_step;

            ModelInfo mc = Common.MC_INFO;
            ModelInfo model = Common.MODEL_INFO;

            POS mc_readyPos = mc.teachPos(TEACH_POS.PICK_PP_WAIT);
            POS mc_teachPos = mc.teachPos(TEACH_POS.NONE);

            POS readyPos = model.teachPos(TEACH_POS.PICK_PP_WAIT);
            POS teachPos = model.teachPos(TEACH_POS.NONE);

            bool isDry = main.isDryRun();

            switch (m_target)
            {
                case TARGET.READY:
                    mc_teachPos = mc.teachPos(TEACH_POS.PICK_PP_WAIT);
                    teachPos = model.teachPos(TEACH_POS.PICK_PP_WAIT);
                    break;

                case TARGET.PICK:
                    mc_teachPos = mc.teachPos(TEACH_POS.PICK_PP_PICK);
                    teachPos = model.teachPos(TEACH_POS.PICK_PP_PICK);
                    break;

                case TARGET.PLACE:
                    mc_teachPos = mc.teachPos(TEACH_POS.PICK_PP_PLACE);
                    teachPos = model.teachPos(TEACH_POS.PICK_PP_PLACE);
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
                        if (m_target == TARGET.PLACE)
                        {
                            if (input(INPUT.MOLD_IN_REVERSE_DETECT) == true)
                            {
                                addAlarm(ALARM.SE_REVERSE_MOLDREVERSE_DETECT_PRODUCT);
                                return;
                            }
                        }

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
                        if (m_target == TARGET.PLACE)
                        {
                            if (input(INPUT.MOLD_IN_REVERSE_UNGRIP) == false)
                            {
                                addAlarm(ALARM.CY_REVERSE_MOLDREVERSE_GRIPPER_CYL_UNGRIP);
                                return;
                            }
                            if (input(INPUT.MOLD_IN_REVERSE_RETURN) == false)
                            {
                                addAlarm(ALARM.CY_REVERSE_MOLDREVERSE_TURN_CYL_RETURN);
                                return;
                            }
                        }

                        if (m_target == TARGET.PICK)
                        {
                            if (input(INPUT.MOLD_IN_PP_UNGRIP) == false)
                            {
                                addAlarm(ALARM.CY_LOADER_INPP_GRIPPER_CYL_UNGRIP);
                                return;
                            }
                        }

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

                        m_step = STEP.SET_ACTION;
                    }
                    break;

                case STEP.SET_ACTION:
                    {
                        switch (m_action)
                        {
                            case ACTION.PICK:
                                main.setOutput(OUTPUT.MOLD_IN_PP_GRIP, true);
                                main.setOutput(OUTPUT.MOLD_IN_PP_UNGRIP, false);
                                break;

                            case ACTION.PLACE:
                                main.setOutput(OUTPUT.MOLD_IN_PP_GRIP, false);
                                main.setOutput(OUTPUT.MOLD_IN_PP_UNGRIP, true);
                                main.setOutput(OUTPUT.MOLD_IN_REVERSE_GRIP, true);
                                main.setOutput(OUTPUT.MOLD_IN_REVERSE_UNGRIP, false);
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
                                case ACTION.PICK:
                                    if (main.input(INPUT.MOLD_IN_PP_GRIP) == false)
                                    {
                                        if (m_cylTimeout.isElasped() == true)
                                        {
                                            addAlarm(ALARM.CY_LOADER_INPP_GRIPPER_CYL_GRIP);
                                            m_step = STEP.SET_ACTION;
                                            return;
                                        }

                                        return;
                                    }
                                    break;

                                case ACTION.PLACE:
                                    if (main.input(INPUT.MOLD_IN_PP_UNGRIP) == false)
                                    {
                                        if (m_cylTimeout.isElasped() == true)
                                        {
                                            addAlarm(ALARM.CY_LOADER_INPP_GRIPPER_CYL_UNGRIP);
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
