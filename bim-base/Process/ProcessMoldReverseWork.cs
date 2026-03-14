using MySqlConnector.Logging;
using System;
using System.Linq.Expressions;

namespace bim_base
{
    public class ProcessMoldReverseWork : CProcess
    {
        public enum STEP
        {
            START = 0,

            CHECK_CONDITION,

            DOWN_READY,
            CHECK_DOWN_READY,

            RETURN_READY,
            CHECK_RETURN_READY,

            WAIT_MOLD,
            CHECK_STAGE,

            GRIP_MOLD,
            CHECK_GRIP_MOLD,

            TURN_MOLD,
            CHECK_TURN_MOLD,

            UP_MOLD_CYL,
            CHECK_UP_MOLD_CYL,

            UNGRIP_MOLD,
            CHECK_UNGRIP_MOLD,

            DOWN_MOLD_CYL,
            CHECK_DOWN_MOLD_CYL,

            SEND_SHUTTLE_COMPLETE,
            WAIT_SHUTTLE,

            RETURN,
            CHECK_RETURN,

            END,
        }

        STEP m_step = STEP.START;
        STEP m_agoStep = STEP.END;

        CElaspedTimer m_cylTimeout = new CElaspedTimer(2 * 1000);

        public ProcessMoldReverseWork(ProcessMain procMain) : base(procMain)
        {
        }

        public override bool start()
        {
            if (m_isRun == true)
                return false;

            m_step = STEP.START;

            return base.start();
        }

        public override void stop()
        {
            base.stop();
        }

        public void resetWork()
        {
            m_step = STEP.START;
        }

        public STEP step() { return m_step; }
        public STEP agoStep() { return m_agoStep; }

        public bool isWaitMold()
        {
            if (m_step == STEP.WAIT_MOLD)
                return true;

            return false;
        }

        public void setCompleteMold()
        {
            if (isWaitMold() == false)
                return;

            m_step = STEP.CHECK_STAGE;
        }

        public bool isWaitShuttle()
        {
            if (m_step == STEP.WAIT_SHUTTLE)
                return true;

            return false;
        }

        public void setCompleteShuttle()
        {
            if (isWaitShuttle() == false)
                return;

            m_step = STEP.RETURN;
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
                Debug.debug("ProcessMoldReverseWork::run STEP:" + m_step);

            m_agoStep = m_step;

            ProcessInPP IN_PP;
            ProcessShuttleWork SHUTTLE;
            
            IN_PP = main.procInPP();
            SHUTTLE = main.procShuttleWork();


            CSTATION ST_REVERSE = main.station(CSTATION.STATION.MOLD_REVERSE);
            CSTATION ST_STAGE_1 = main.station(CSTATION.STATION.SHUTTLE_ST_1);

            switch (m_step)
            {
                case STEP.START:
                    {

                        m_step = STEP.CHECK_CONDITION;
                    }
                    break;

                case STEP.CHECK_CONDITION:
                    {
                        if (IN_PP.isRun() == true)
                            return;

                        m_step = STEP.DOWN_READY;
                    }
                    break;

                case STEP.DOWN_READY:
                    {
                        setDown();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_DOWN_READY;
                    }
                    break;

                case STEP.CHECK_DOWN_READY:
                    {
                        if (isDown() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_REVERSE_MOLDREVERSE_UBDOWN_CYL_DOWN);

                                m_step = STEP.DOWN_READY;
                                return;
                            }
                            return;
                        }

                        m_step = STEP.RETURN_READY;
                    }
                    break;

                case STEP.RETURN_READY:
                    {
                        setReturn();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_RETURN_READY;
                    }
                    break;

                case STEP.CHECK_RETURN_READY:
                    {
                        if (isReturn() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_REVERSE_MOLDREVERSE_TURN_CYL_RETURN);

                                m_step = STEP.RETURN_READY;
                                return;
                            }
                            return;
                        }

                        m_step = STEP.WAIT_MOLD;
                    }
                    break;

                case STEP.WAIT_MOLD:
                    {
                        // WAIT MOLD
                    }
                    break;

                case STEP.CHECK_STAGE:
                    {
                        if (main.isLastWork() == true)
                            return;

                        if (input(INPUT.MOLD_SHUTTLE_STAGE_1_DETECT) == true)
                            return;

                        if (input(INPUT.MOLD_SHUTTLE_SERVO_DOWN_1) == false || input(INPUT.MOLD_SHUTTLE_SERVO_DOWN_2) == false)
                            return;

                        m_step = STEP.GRIP_MOLD;
                    }
                    break;

                case STEP.GRIP_MOLD:
                    {
                        setGrip();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_GRIP_MOLD;
                    }
                    break;

                case STEP.CHECK_GRIP_MOLD:
                    {
                        if (main.isDryRun() == false)
                        {
                            if (isGrip() == false)
                            {
                                if (m_cylTimeout.isElasped() == true)
                                {
                                    addAlarm(ALARM.CY_REVERSE_MOLDREVERSE_GRIPPER_CYL_GRIP);

                                    m_step = STEP.GRIP_MOLD;
                                    return;
                                }
                                return;
                            }
                        }

                        m_step = STEP.TURN_MOLD;
                    }
                    break;

                case STEP.TURN_MOLD:
                    {
                        setTurn();

                        m_step = STEP.CHECK_TURN_MOLD;
                    }
                    break;

                case STEP.CHECK_TURN_MOLD:
                    {
                        if (isTurn() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_REVERSE_MOLDREVERSE_TURN_CYL_TURN);

                                m_step = STEP.TURN_MOLD;
                                return;
                            }
                            return;
                        }

                        m_step = STEP.UP_MOLD_CYL;
                    }
                    break;

                case STEP.UP_MOLD_CYL:
                    {
                        setUp();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_UP_MOLD_CYL;
                    }
                    break;

                case STEP.CHECK_UP_MOLD_CYL:
                    {
                        if (isUp() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_REVERSE_MOLDREVERSE_UBDOWN_CYL_UP);

                                m_step = STEP.UP_MOLD_CYL;
                                return;
                            }
                            return;
                        }

                        m_step = STEP.UNGRIP_MOLD;

                    }
                    break;

                case STEP.UNGRIP_MOLD:
                    {
                        setUngrip();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_UNGRIP_MOLD;
                    }
                    break;

                case STEP.CHECK_UNGRIP_MOLD:
                    {
                        if (isUngrip() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_REVERSE_MOLDREVERSE_GRIPPER_CYL_UNGRIP);

                                m_step = STEP.UNGRIP_MOLD;
                                return;
                            }
                            return;
                        }

                        m_step = STEP.DOWN_MOLD_CYL;
                    }
                    break;

                case STEP.DOWN_MOLD_CYL:
                    {
                        setDown();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_DOWN_MOLD_CYL;
                    }
                    break;
                case STEP.CHECK_DOWN_MOLD_CYL:
                    {
                        if (isDown() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_REVERSE_MOLDREVERSE_UBDOWN_CYL_DOWN);

                                m_step = STEP.DOWN_MOLD_CYL;
                                return;
                            }
                            return;
                        }

                        ST_REVERSE.move(ST_STAGE_1);

                        m_step = STEP.SEND_SHUTTLE_COMPLETE;
                    }
                    break;

                case STEP.SEND_SHUTTLE_COMPLETE:
                    {
                        if (SHUTTLE.isWaitReverse() == false)
                            return;

                        SHUTTLE.setReverseComplete();

                        m_step = STEP.WAIT_SHUTTLE;
                    }
                    break;

                case STEP.WAIT_SHUTTLE:
                    {
                        // WAIT SHUTTLE
                    }
                    return;

                case STEP.RETURN:
                    {
                        setReturn();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_RETURN;
                    }
                    break;

                case STEP.CHECK_RETURN:
                    {
                        if (isReturn() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_REVERSE_MOLDREVERSE_TURN_CYL_RETURN);

                                m_step = STEP.RETURN;
                                return;
                            }
                            return;
                        }

                        m_step = STEP.END;
                    }
                    break;

                case STEP.END:
                    {
                        m_step = STEP.START;
                    }
                    break;
            }
        }//run

        public void setTurn()
        {
            setOutput(OUTPUT.MOLD_IN_REVERSE_TURN, true);
            setOutput(OUTPUT.MOLD_IN_REVERSE_RETURN, false);
        }

        public bool isTurn()
        {
            return input(INPUT.MOLD_IN_REVERSE_TURN);
        }

        public void setReturn()
        {
            setOutput(OUTPUT.MOLD_IN_REVERSE_RETURN, true);
            setOutput(OUTPUT.MOLD_IN_REVERSE_TURN, false);
        }

        public bool isReturn()
        {
            return input(INPUT.MOLD_IN_REVERSE_RETURN);
        }

        public void setGrip()
        {
            setOutput(OUTPUT.MOLD_IN_REVERSE_GRIP, true);
            setOutput(OUTPUT.MOLD_IN_REVERSE_UNGRIP, false);
        }

        public bool isGrip()
        {
            return input(INPUT.MOLD_IN_REVERSE_GRIP);
        }

        public void setUngrip()
        {
            setOutput(OUTPUT.MOLD_IN_REVERSE_UNGRIP, true);
            setOutput(OUTPUT.MOLD_IN_REVERSE_GRIP, false);
        }

        public bool isUngrip()
        {
            return input(INPUT.MOLD_IN_REVERSE_UNGRIP);
        }

        public void setUp()
        {
            setOutput(OUTPUT.SHUTTLE_MOLD_UP, true);
            setOutput(OUTPUT.SHUTTLE_MOLD_DOWN, false);
        }

        public bool isUp()
        {
            return input(INPUT.MOLD_SHUTTLE_UP);
        }

        public void setDown()
        {
            setOutput(OUTPUT.SHUTTLE_MOLD_DOWN, true);
            setOutput(OUTPUT.SHUTTLE_MOLD_UP, false);
        }

        public bool isDown()
        {
            return input(INPUT.MOLD_SHUTTLE_DOWN);
        }
    }//class
}//namespace
