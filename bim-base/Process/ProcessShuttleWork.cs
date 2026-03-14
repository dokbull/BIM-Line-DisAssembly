using MySqlConnector.Logging;
using System;
using System.Linq.Expressions;

namespace bim_base
{
    public class ProcessShuttleWork : CProcess
    {
        public enum STEP
        {
            START = 0,

            CHECK_CONDITION,

            CYL_READY,
            CHECK_CYL_READY,

            DOWN_READY,
            CHECK_DOWN_READY,

            MOVE_STAGE_READY,
            CHECK_MOVE_STAGE_READY,

            WAIT_REVERSE,
        
            UP_STAGE,
            CHECK_UP_STAGE,

            MOVE_STAGE_RIGHT,
            CHECK_MOVE_STAGE_RIGHT,

            DOWN_STAGE,
            CHECK_DOWN_STAGE,

            SET_COMPLETE_REVERSE,
            CALL_MOLD_PLACE,

            LOCK_PRDDUCT,
            CHECK_LOCK_PRODUCT,

            OPENER_BWD,
            CHECK_OPENER_BWD,

            OPENER_RETRY_FWD,
            CHECK_OPENER_RETRY_FWD,

            MOVE_STAGE_LEFT,

            WAIT_MOLD_WORK,
            WAIT_UB_WORK,

            OPENER_FWD,
            CHECK_OPENER_FWD,

            UNLOCK_PRODUCT,
            CHECK_UNLOCK_PRODUCT,

            END,
        }

        STEP m_step = STEP.START;
        STEP m_agoStep = STEP.END;

        CElaspedTimer m_cylTimeout = new CElaspedTimer(2 * 1000);
        CElaspedTimer m_actDelay = new CElaspedTimer(250);

        int m_retryOpener = 0;
        int m_maxRetryOpener = 3;

        public ProcessShuttleWork(ProcessMain procMain) : base(procMain)
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

        public bool isWaitReverse()
        {
            if (m_step == STEP.WAIT_REVERSE)
                return true;

            return false;
        }

        public void setReverseComplete()
        {
            if (isWaitReverse() == false)
                return;

            m_step = STEP.UP_STAGE;
        }

        public bool isWaitMoldWork()
        {
            if (m_step == STEP.WAIT_MOLD_WORK)
                return true;

            return false;
        }

        public void setMoldWorkComplete()
        {
            if (isWaitMoldWork() == false)
                return;

            if (main.isLastWork() == true)
            {
                m_step = STEP.END;
                return;
            }

            m_step = STEP.WAIT_UB_WORK;
        }

        public bool isWaitUbWork()
        {
            if (m_step == STEP.WAIT_UB_WORK)
                return true;

            return false;
        }

        public void setUbWorkComplete()
        {
            if (isWaitUbWork() == false)
                return;

            m_step = STEP.OPENER_FWD;
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
                Debug.debug("ProcessShuttleWork::run STEP:" + m_step);

            m_agoStep = m_step;

            bool isDry = main.isDryRun();

            ProcessMoldBase MOLD_BASE;
            ProcessMoldWork MOLD_WORK;
            ProcessMoldReverseWork REVERSE;

            MOLD_BASE = main.procMoldBase();
            MOLD_WORK = main.procMoldWork();
            REVERSE = main.procMoldReverseWork();

            CSTATION ST_STAGE_1 = main.station(CSTATION.STATION.SHUTTLE_ST_1);
            CSTATION ST_STAGE_2 = main.station(CSTATION.STATION.SHUTTLE_ST_2);
            CSTATION ST_STAGE_3 = main.station(CSTATION.STATION.SHUTTLE_ST_3);
            CSTATION ST_LEFT_PP = main.station(CSTATION.STATION.MOLD_PP_LEFT);
            CSTATION ST_RIGHT_PP = main.station(CSTATION.STATION.MOLD_PP_RIGHT);

            switch (m_step)
            {
                case STEP.START:
                    {
                        m_step = STEP.CHECK_CONDITION;
                    }
                    break;

                case STEP.CHECK_CONDITION:
                    {

                        m_step = STEP.CYL_READY;
                    }
                    break;

                case STEP.CYL_READY:
                    {
                        setOpenerFwd();
                        setLockBwd();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_CYL_READY;
                    }
                    break;

                case STEP.CHECK_CYL_READY:
                    {
                        if (isOpenerFwd() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_BASE_MOLDBASE_UNLOCK_CYL_FWD);

                                m_step = STEP.CYL_READY;
                                return;
                            }
                            return;
                        }

                        if (isLockBwd() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_BASE_MOLDBASE_HOLD_CYL_BWD);

                                m_step = STEP.CYL_READY;
                                return;
                            }
                            return;
                        }

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
                                addAlarm(ALARM.CY_BASE_MOLDBASE_UPDOWN_CYL_DOWN);

                                m_step = STEP.DOWN_READY;
                                return;
                            }
                            return;
                        }

                        m_step = STEP.MOVE_STAGE_READY;
                    }
                    break;

                case STEP.MOVE_STAGE_READY:
                    {
                        bool ret = MOLD_BASE.start(ProcessMoldBase.TARGET.LEFT);

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_MOVE_STAGE_READY;
                    }
                    break;

                case STEP.CHECK_MOVE_STAGE_READY:
                    {
                        if (MOLD_BASE.isRun() == true)
                            return;

                        m_step = STEP.WAIT_REVERSE;
                    }
                    break;

                case STEP.WAIT_REVERSE:
                    {
                        if (REVERSE.isWaitMold() == true && main.isLastWork() == true)
                        {
                            m_step = STEP.UP_STAGE;
                            return;
                        }

                        // WAIT REVERSE
                    }
                    break;

                case STEP.UP_STAGE:
                    {
                        setUp();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_UP_STAGE;
                    }
                    break;

                case STEP.CHECK_UP_STAGE:
                    {
                        if (isUp() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_BASE_MOLDBASE_UPDOWN_CYL_UP);

                                m_step = STEP.UP_STAGE;
                                return;
                            }
                            return;
                        }

                        m_step = STEP.MOVE_STAGE_RIGHT;
                    }
                    break;

                case STEP.MOVE_STAGE_RIGHT:
                    {
                        bool ret = MOLD_BASE.start(ProcessMoldBase.TARGET.RIGHT);

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_MOVE_STAGE_RIGHT;
                    }
                    break;

                case STEP.CHECK_MOVE_STAGE_RIGHT:
                    {
                        if (MOLD_BASE.isRun() == true)
                            return;

                        m_step = STEP.DOWN_STAGE;
                    }
                    break;

                case STEP.DOWN_STAGE:
                    {
                        setDown();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_DOWN_STAGE;
                    }
                    break;

                case STEP.CHECK_DOWN_STAGE:
                    {
                        if (isDown() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_BASE_MOLDBASE_UPDOWN_CYL_DOWN);

                                m_step = STEP.DOWN_STAGE;
                                return;
                            }
                            return;
                        }

                        ST_STAGE_2.move(ST_STAGE_3);
                        ST_STAGE_1.move(ST_STAGE_2);

                        m_actDelay.start();

                        m_step = STEP.SET_COMPLETE_REVERSE;
                    }
                    break;

                case STEP.SET_COMPLETE_REVERSE:
                    {
                        if (REVERSE.isWaitShuttle() == false)
                        {
                            if (main.isLastWork() == true)
                            {
                                m_step = STEP.CALL_MOLD_PLACE;
                                return;
                            }

                            return;
                        }
                            
                        REVERSE.setCompleteShuttle();

                        m_step = STEP.CALL_MOLD_PLACE;
                    }
                    break;

                case STEP.CALL_MOLD_PLACE:
                    {
                        if (MOLD_WORK.isWaitShuttle() == true)
                            MOLD_WORK.setShuttleCall();

                        m_step = STEP.LOCK_PRDDUCT;
                    }
                    break;

                case STEP.LOCK_PRDDUCT:
                    {
                        if (m_actDelay.isElasped() == false)
                            return;

                        if (main.isLastWork() == true && input(INPUT.MOLD_SHUTTLE_STAGE_2_DETECT) == false)
                        {
                            m_step = STEP.MOVE_STAGE_LEFT;
                            return;
                        }

                        setLockFwd();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_LOCK_PRODUCT;
                    }
                    break;

                case STEP.CHECK_LOCK_PRODUCT:
                    {
                        if (isDry == false)
                        {
                            if (isLockFwd() == false)
                            {
                                if (m_cylTimeout.isElasped() == true)
                                {
                                    addAlarm(ALARM.CY_BASE_MOLDBASE_HOLD_CYL_FWD);

                                    m_step = STEP.LOCK_PRDDUCT;
                                    return;
                                }
                                return;
                            }
                        }

                        m_actDelay.start();

                        m_retryOpener = 0;

                        m_step = STEP.OPENER_BWD;
                    }
                    break;

                case STEP.OPENER_BWD:
                    {
                        if (m_actDelay.isElasped() == false)
                            return;

                        setOpenerBwd();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_OPENER_BWD;
                    }
                    break;

                case STEP.CHECK_OPENER_BWD:
                    {
                        if (isOpenerBwd() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                m_retryOpener++; 

                                if (m_retryOpener > m_maxRetryOpener)
                                {
                                    addAlarm(ALARM.CY_BASE_MOLDBASE_UNLOCK_CYL_BWD);
                                    m_retryOpener = 0;
                                }
                               
                                m_step = STEP.OPENER_RETRY_FWD;
                                return;
                            }
                            return;
                        }

                        m_step = STEP.MOVE_STAGE_LEFT;
                    }
                    break;

                case STEP.OPENER_RETRY_FWD:
                    {
                        setOpenerFwd();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_OPENER_RETRY_FWD;
                    }
                    break;

                case STEP.CHECK_OPENER_RETRY_FWD:
                    {
                        if (isOpenerFwd() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_BASE_MOLDBASE_UNLOCK_CYL_FWD);
                                m_step = STEP.OPENER_RETRY_FWD;
                                return;
                            }

                            return;
                        }

                        m_step = STEP.OPENER_BWD;
                    }
                    break;

                case STEP.MOVE_STAGE_LEFT:
                    {
                        bool ret = MOLD_BASE.start(ProcessMoldBase.TARGET.LEFT);

                        if (ret == false)
                            return;

                        m_step = STEP.WAIT_MOLD_WORK;
                    }
                    break;

                case STEP.WAIT_MOLD_WORK:
                    {
                        if (MOLD_WORK.isWaitShuttle() == true)
                            MOLD_WORK.setShuttleCall();

                        // WAIT MOLD WORK
                    }
                    break;

                case STEP.WAIT_UB_WORK:
                    {
                        // WAIT MOLD WORK
                    }
                    break;

                case STEP.OPENER_FWD:
                    {
                        setOpenerFwd();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_OPENER_FWD;
                    }
                    break;

                case STEP.CHECK_OPENER_FWD:
                    {
                        if (isOpenerFwd() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_BASE_MOLDBASE_UNLOCK_CYL_FWD);

                                m_step = STEP.OPENER_FWD;
                                return;
                            }
                            return;
                        }

                        m_actDelay.start();

                        m_step = STEP.UNLOCK_PRODUCT;
                    }
                    break;

                case STEP.UNLOCK_PRODUCT:
                    {
                        if (m_actDelay.isElasped() == true)
                            return;

                        setLockBwd();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_UNLOCK_PRODUCT;
                    }
                    break;

                case STEP.CHECK_UNLOCK_PRODUCT:
                    {
                        if (isLockBwd() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_BASE_MOLDBASE_HOLD_CYL_BWD);

                                m_step = STEP.UNLOCK_PRODUCT;
                                return;
                            }
                            return;
                        }

                        m_step = STEP.END;
                    }
                    break;

                case STEP.END:
                    {
                        main.setLastWork(false);
                        m_step = STEP.START;
                    }
                    break;
            }
        }//run

        public void setOpenerFwd()
        {
            setOutput(OUTPUT.SHUTTLE_MOLD_UNLOCK_FWD, true);
            setOutput(OUTPUT.SHUTTLE_MOLD_UNLOCK_BWD, false);
        }

        public void setOpenerBwd()
        {
            setOutput(OUTPUT.SHUTTLE_MOLD_UNLOCK_BWD, true);
            setOutput(OUTPUT.SHUTTLE_MOLD_UNLOCK_FWD, false);
        }

        public bool isOpenerFwd()
        {
            return input(INPUT.MOLD_SHUTTLE_UNLOCK_FWD_1) && input(INPUT.MOLD_SHUTTLE_UNLOCK_FWD_2);
        }

        public bool isOpenerBwd()
        {
            return input(INPUT.MOLD_SHUTTLE_UNLOCK_BWD_1) && input(INPUT.MOLD_SHUTTLE_UNLOCK_BWD_2);
        }

        public void setLockFwd()
        {
            setOutput(OUTPUT.SHUTTLE_MOLD_PUSHER_FWD, true);
            setOutput(OUTPUT.SHUTTLE_MOLD_PUSHER_BWD, false);
        }

        public void setLockBwd()
        {
            setOutput(OUTPUT.SHUTTLE_MOLD_PUSHER_BWD, true);
            setOutput(OUTPUT.SHUTTLE_MOLD_PUSHER_FWD, false);
        }

        public bool isLockFwd()
        {
            return input(INPUT.MOLD_SHUTTLE_PUSHER_FWD_1) && input(INPUT.MOLD_SHUTTLE_PUSHER_FWD_2);
        }

        public bool isLockBwd()
        {
            return input(INPUT.MOLD_SHUTTLE_PUSHER_BWD_1) && input(INPUT.MOLD_SHUTTLE_PUSHER_BWD_2);
        }

        public void setUp()
        {
            setOutput(OUTPUT.SHUTTLE_SERVO_MOLD_UP, true);
            setOutput(OUTPUT.SHUTTLE_SERVO_MOLD_DOWN, false);
        }

        public void setDown()
        {
            setOutput(OUTPUT.SHUTTLE_SERVO_MOLD_DOWN, true);
            setOutput(OUTPUT.SHUTTLE_SERVO_MOLD_UP, false);
        }

        public bool isUp()
        {
            return input(INPUT.MOLD_SHUTTLE_SERVO_UP_1) && input(INPUT.MOLD_SHUTTLE_SERVO_UP_2);
        }

        public bool isDown()
        {
            return input(INPUT.MOLD_SHUTTLE_SERVO_DOWN_1) && input(INPUT.MOLD_SHUTTLE_SERVO_DOWN_2);
        }
    }//class
}//namespace
