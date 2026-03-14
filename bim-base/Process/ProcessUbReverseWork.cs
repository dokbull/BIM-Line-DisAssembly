using MySqlConnector.Logging;
using System;
using System.Linq.Expressions;

namespace bim_base
{
    public class ProcessUbReverseWork : CProcess
    {
        public enum STEP
        {
            START = 0,

            CHECK_CONDITION,

            UP_READY,
            CHECK_UP_READY,

            RETURN_READY,
            CHECK_RETURN_READY,

            WAIT_UB,

            VAC_UB,
            CHECK_VAC_UB,

            CHECK_STAGE,

            TURN_UB,
            CHECK_TURN_UB,

            DOWN_UB_CYL,
            CHECK_DOWN_UB_CYL,

            UNVAC_UB,
            CHECK_UNVAC_UB,

            UP_UB_CYL,
            CHECK_UP_UB_CYL,

            RETURN,
            CHECK_RETURN,

            END,
        }

        STEP m_step = STEP.START;
        STEP m_agoStep = STEP.END;

        CElaspedTimer m_vacTimeout = new CElaspedTimer(2 * 1000);
        CElaspedTimer m_cylTimeout = new CElaspedTimer(2 * 1000);

        public ProcessUbReverseWork(ProcessMain procMain) : base(procMain)
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

        public bool isWaitUb()
        {
            if (m_step == STEP.WAIT_UB)
                return true;

            return false;
        }

        public void setCompleteUb()
        {
            if (isWaitUb() == false)
                return;

            m_step = STEP.CHECK_STAGE;
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
                Debug.debug("ProcessUbReverseWork::run STEP:" + m_step);

            m_agoStep = m_step;

            bool isDry = main.isDryRun();

            ProcessUbPP UB_PP;
            ProcessOutUbCvWork UB_CV;

            UB_PP = main.procUbPP();
            UB_CV = main.processOutUbCvWork();

            CSTATION ST_REVERSE_FRONT = main.station(CSTATION.STATION.UB_REVERSE_FRONT);
            CSTATION ST_REVERSE_REAR = main.station(CSTATION.STATION.UB_REVERSE_REAR);

            CSTATION ST_UB_CV_FRONT = main.station(CSTATION.STATION.OUT_UB_CV_FRONT);
            CSTATION ST_UB_CV_REAR = main.station(CSTATION.STATION.OUT_UB_CV_REAR);

            switch (m_step)
            {
                case STEP.START:
                    {

                        m_step = STEP.CHECK_CONDITION;
                    }
                    break;

                case STEP.CHECK_CONDITION:
                    {
                        if (UB_PP.isRun() == true)
                            return;

                        m_step = STEP.UP_READY;
                    }
                    break;

                case STEP.UP_READY:
                    {
                        setUp();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_UP_READY;
                    }
                    break;

                case STEP.CHECK_UP_READY:
                    {
                        if (isUp1() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_REVERSE_UBREVERSE_UPDOWN1_CYL_UP);

                                m_step = STEP.UP_READY;
                                return;
                            }
                            return;
                        }

                        if (isUp2() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_REVERSE_UBREVERSE_UPDOWN2_CYL_UP);

                                m_step = STEP.UP_READY;
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
                        if (isReturn1() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_REVERSE_UBREVERSE_TURN1_CYL_RETURN);

                                m_step = STEP.RETURN_READY;
                                return;
                            }
                            return;
                        }

                        if (isReturn2() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_REVERSE_UBREVERSE_TURN2_CYL_RETURN);

                                m_step = STEP.RETURN_READY;
                                return;
                            }
                            return;
                        }

                        m_step = STEP.WAIT_UB;
                    }
                    break;

                case STEP.WAIT_UB:
                    {
                        if (main.isLastWork() == true)
                        {
                            m_step = STEP.CHECK_STAGE;
                            return;
                        }
                        // WAIT MOLD
                    }
                    break;

                case STEP.CHECK_STAGE:
                    {
                        if (isDry == false)
                        {
                            if (main.isLastWork() == true)
                            {
                                if (input(INPUT.UB_OUT_REVERSE_DETECT_1) == true || input(INPUT.UB_OUT_REVERSE_DETECT_2) == true)
                                {
                                    m_step = STEP.TURN_UB;
                                    return;
                                }
                            }

                            if (input(INPUT.UB_OUT_REVERSE_DETECT_1) == false || input(INPUT.UB_OUT_REVERSE_DETECT_2) == false)
                            {
                                m_step = STEP.WAIT_UB;
                                return;
                            }
                        }

                        m_step = STEP.VAC_UB;
                    }
                    break;

                case STEP.VAC_UB:
                    {
                        setVac();

                        m_vacTimeout.start();

                        m_step = STEP.CHECK_VAC_UB;
                    }
                    break;

                case STEP.CHECK_VAC_UB:
                    {
                        if (isDry == false)
                        {
                            if (isVac1() == false)
                            {
                                if (m_vacTimeout.isElasped() == true)
                                {
                                    addAlarm(ALARM.VC_REVERSE_UBREVERSE_HOLDER1_ON);

                                    m_step = STEP.VAC_UB;
                                    return;
                                }
                                return;
                            }

                            if (isVac2() == false)
                            {
                                if (m_vacTimeout.isElasped() == true)
                                {
                                    addAlarm(ALARM.VC_REVERSE_UBREVERSE_HOLDER2_ON);

                                    m_step = STEP.VAC_UB;
                                    return;
                                }
                                return;
                            }
                        }

                        m_step = STEP.TURN_UB;
                    }
                    break;

                case STEP.TURN_UB:
                    {
                        if (UB_PP.isRun() == true)
                            return;

                        setTurn();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_TURN_UB;
                    }
                    break;

                case STEP.CHECK_TURN_UB:
                    {
                        if (isTurn1() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_REVERSE_UBREVERSE_TURN1_CYL_TURN);

                                m_step = STEP.TURN_UB;
                                return;
                            }
                            return;
                        }

                        if (isTurn2() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_REVERSE_UBREVERSE_TURN2_CYL_TURN);

                                m_step = STEP.TURN_UB;
                                return;
                            }
                            return;
                        }

                        m_step = STEP.DOWN_UB_CYL;
                    }
                    break;

                case STEP.DOWN_UB_CYL:
                    {
                        if (input(INPUT.UB_ULD_CV_IN_1) == true || input(INPUT.UB_ULD_CV_IN_2) == true)
                            return;

                        UB_CV.setLock(true);

                        setDown();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_DOWN_UB_CYL;
                    }
                    break;

                case STEP.CHECK_DOWN_UB_CYL:
                    {
                        if (isDown1() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_REVERSE_UBREVERSE_UPDOWN1_CYL_DOWN);

                                m_step = STEP.DOWN_UB_CYL;
                                return;
                            }
                            return;
                        }

                        if (isDown2() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_REVERSE_UBREVERSE_UPDOWN2_CYL_DOWN);

                                m_step = STEP.DOWN_UB_CYL;
                                return;
                            }
                            return;
                        }

                        m_step = STEP.UNVAC_UB;

                    }
                    break;

                case STEP.UNVAC_UB:
                    {
                        setUnvac();

                        m_vacTimeout.start();

                        m_step = STEP.CHECK_UNVAC_UB;
                    }
                    break;

                case STEP.CHECK_UNVAC_UB:
                    {
                        if (isUnvac1() == false)
                        {
                            if (m_vacTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.VC_REVERSE_UBREVERSE_HOLDER1_OFF);

                                m_step = STEP.UNVAC_UB;
                                return;
                            }
                            return;
                        }

                        if (isUnvac2() == false)
                        {
                            if (m_vacTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.VC_REVERSE_UBREVERSE_HOLDER2_OFF);

                                m_step = STEP.UNVAC_UB;
                                return;
                            }
                            return;
                        }

                        ST_REVERSE_FRONT.move(ST_UB_CV_FRONT);
                        ST_REVERSE_REAR.move(ST_UB_CV_REAR);

                        m_step = STEP.UP_UB_CYL;
                    }
                    break;

                case STEP.UP_UB_CYL:
                    {
                        setUp();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_UP_UB_CYL;
                    }
                    break;
                case STEP.CHECK_UP_UB_CYL:
                    {
                        if (isUp1() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_REVERSE_UBREVERSE_UPDOWN1_CYL_UP);

                                m_step = STEP.UP_UB_CYL;
                                return;
                            }
                            return;
                        }

                        if (isUp2() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_REVERSE_UBREVERSE_UPDOWN2_CYL_UP);

                                m_step = STEP.UP_UB_CYL;
                                return;
                            }
                            return;
                        }

                        m_step = STEP.RETURN;
                    }
                    break;

                case STEP.RETURN:
                    {
                        UB_CV.setLock(false);

                        setReturn();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_RETURN;
                    }
                    break;
                case STEP.CHECK_RETURN:
                    {
                        if (isReturn1() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_REVERSE_UBREVERSE_TURN1_CYL_RETURN);

                                m_step = STEP.RETURN;
                                return;
                            }
                            return;
                        }

                        if (isReturn2() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_REVERSE_UBREVERSE_TURN2_CYL_RETURN);

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
            setOutput(OUTPUT.UB_OUT_REVERSE_TURN_1, true);
            setOutput(OUTPUT.UB_OUT_REVERSE_TURN_2, true);
            setOutput(OUTPUT.UB_OUT_REVERSE_RETURN_1, false);
            setOutput(OUTPUT.UB_OUT_REVERSE_RETURN_2, false);
        }

        public bool isTurn1()
        {
            return input(INPUT.UB_OUT_REVERSE_TURN_1);
        }

        public bool isTurn2()
        {
            return input(INPUT.UB_OUT_REVERSE_TURN_2);
        }

        public void setReturn()
        {
            setOutput(OUTPUT.UB_OUT_REVERSE_RETURN_1, true);
            setOutput(OUTPUT.UB_OUT_REVERSE_RETURN_2, true);
            setOutput(OUTPUT.UB_OUT_REVERSE_TURN_1, false);
            setOutput(OUTPUT.UB_OUT_REVERSE_TURN_2, false);
        }

        public bool isReturn1()
        {
            return input(INPUT.UB_OUT_REVERSE_RETURN_1);
        }

        public bool isReturn2()
        {
            return input(INPUT.UB_OUT_REVERSE_RETURN_2);
        }

        public void setVac()
        {
            setOutput(OUTPUT.UB_OUT_REVERSE_VAC_1, true);
            setOutput(OUTPUT.UB_OUT_REVERSE_VAC_2, true);
        }

        public bool isVac1()
        {
            return input(INPUT.UB_OUT_REVERSE_VAC_1);
        }
        public bool isVac2()
        {
            return input(INPUT.UB_OUT_REVERSE_VAC_2);
        }

        public void setUnvac()
        {
            setOutput(OUTPUT.UB_OUT_REVERSE_VAC_1, false);
            setOutput(OUTPUT.UB_OUT_REVERSE_VAC_2, false);
        }

        public bool isUnvac1()
        {
            return input(INPUT.UB_OUT_REVERSE_VAC_1) == false;
        }

        public bool isUnvac2()
        {
            return input(INPUT.UB_OUT_REVERSE_VAC_2) == false;
        }

        public void setUp()
        {
            setOutput(OUTPUT.UB_OUT_REVERSE_UP_1, true);
            setOutput(OUTPUT.UB_OUT_REVERSE_UP_2, true);
            setOutput(OUTPUT.UB_OUT_REVERSE_DOWN_1, false);
            setOutput(OUTPUT.UB_OUT_REVERSE_DOWN_2, false);
        }

        public bool isUp1()
        {
            return input(INPUT.UB_OUT_REVERSE_UP_1);
        }

        public bool isUp2()
        {
            return input(INPUT.UB_OUT_REVERSE_UP_2);
        }

        public void setDown()
        {
            setOutput(OUTPUT.UB_OUT_REVERSE_DOWN_1, true);
            setOutput(OUTPUT.UB_OUT_REVERSE_DOWN_2, true);
            setOutput(OUTPUT.UB_OUT_REVERSE_UP_1, false);
            setOutput(OUTPUT.UB_OUT_REVERSE_UP_2, false);
        }

        public bool isDown1()
        {
            return input(INPUT.UB_OUT_REVERSE_DOWN_1);
        }
        public bool isDown2()
        {
            return input(INPUT.UB_OUT_REVERSE_DOWN_2);
        }
    }//class
}//namespace
