using MySqlConnector.Logging;
using System;
using System.Linq.Expressions;

namespace bim_base
{
    public class ProcessAlignCvWork : CProcess
    {
        public enum STEP
        {
            START = 0,

            CHECK_CONDITION,

            WAIT_CV,
            RUN_CV,
            CHECK_CV,
            OVERRUN_CV,

            ALIGN_FWD,
            CHECK_ALIGN_FWD,

            STOP_CV,

            ALIGN_BWD,
            CHECK_ALIGN_BWD,

            ALIGN_UP,
            CHECK_ALIGN_UP,

            TRIGGER_BCR,
            CHECK_BCR,

            WAIT_PICK,

            ALIGN_DOWN,
            CHECK_ALIGN_DOWN,
            
            END,
        }

        STEP m_step = STEP.START;
        STEP m_agoStep = STEP.END;

        CElaspedTimer m_cvRunTimer = new CElaspedTimer(10 * 1000);
        CElaspedTimer m_cvOverRunTime = new CElaspedTimer(1 * 1000);
        CElaspedTimer m_cylTimeout = new CElaspedTimer(2 * 1000);
        CElaspedTimer m_actDelay = new CElaspedTimer(500);

        public ProcessAlignCvWork(ProcessMain procMain) : base(procMain)
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

        public bool isWaitCv()
        {
            if (m_step == STEP.WAIT_CV)
                return true;

            return false;
        }

        public void callCv()
        {
            if (isWaitCv() == false)
                return;

            m_cvRunTimer.start();
        }

        public bool isWaitPick()
        {
            if (m_step == STEP.WAIT_PICK)
                return true;

            return false;
        }

        public void setCompletePick()
        {
            if (isWaitPick() == false)
                return;

            m_step = STEP.ALIGN_DOWN;
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
                Debug.debug("ProcessAlignCvWork::run STEP:" + m_step);

            m_agoStep = m_step;

            bool isDry = main.isDryRun();

            CSTATION ST_ALIGN_CV = main.station(CSTATION.STATION.ALIGN_CV);

            switch (m_step)
            {
                case STEP.START:
                    {
                        setBwd();
                        setDown();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_CONDITION;
                    }
                    break;

                case STEP.CHECK_CONDITION:
                    {
                        if (isBwd() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_LOADER_ALIGNCV_ALIGN_CYL_BWD);

                                m_step = STEP.START;
                                return;
                            }
                            return;
                        }

                        if (isDown() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_LOADER_ALIGNCV_UPDOWN_CYL_DOWN);

                                m_step = STEP.START;
                                return;
                            }
                            return;
                        }

                        m_step = STEP.WAIT_CV;
                    }
                    break;

                case STEP.WAIT_CV:
                    {
                        if (isDry == true)
                        {
                            m_step = STEP.RUN_CV;
                            return;
                        }

                        if (input(INPUT.ALIGN_CV_IN) == false)
                            return;

                        m_cvRunTimer.start();

                        m_step = STEP.RUN_CV;
                    }
                    break;

                case STEP.RUN_CV:
                    {
                        setOutput(OUTPUT.ALIGN_CV_RUN, true);

                        m_step = STEP.CHECK_CV;
                    }
                    break;

                case STEP.CHECK_CV:
                    {
                        if (m_cvRunTimer.isElasped() == true)
                        {
                            setOutput(OUTPUT.ALIGN_CV_RUN, false);
                            m_step = STEP.WAIT_CV;
                            return;
                        }

                        if (isDry == false)
                        {
                            if (input(INPUT.ALIGN_CV_OUT) == false)
                                return;
                        }

                        m_cvOverRunTime.start();

                        m_step = STEP.OVERRUN_CV;
                    }
                    break;

                case STEP.OVERRUN_CV:
                    {
                        if (m_cvOverRunTime.isElasped() == false)
                            return;

                        m_step = STEP.ALIGN_FWD;
                    }
                    break;

                case STEP.ALIGN_FWD:
                    {
                        setFwd();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_ALIGN_FWD;
                    }
                    break;

                case STEP.CHECK_ALIGN_FWD:
                    {
                        if (isDry == false)
                        {
                            if (isFwd() == false)
                            {
                                if (m_cylTimeout.isElasped() == true)
                                {
                                    addAlarm(ALARM.CY_LOADER_ALIGNCV_ALIGN_CYL_FWD);

                                    m_step = STEP.ALIGN_FWD;
                                    return;
                                }
                                return;
                            }
                        }

                        m_actDelay.start();

                        m_step = STEP.STOP_CV;
                    }
                    break;

                case STEP.STOP_CV:
                    {
                        if (m_actDelay.isElasped() == false)
                            return;

                        setOutput(OUTPUT.ALIGN_CV_RUN, false);

                        if (ST_ALIGN_CV.type() != CSTATION.TYPE.MOLD)
                            ST_ALIGN_CV.setType(CSTATION.TYPE.MOLD);

                        m_step = STEP.ALIGN_BWD;
                    }
                    break;

                case STEP.ALIGN_BWD:
                    {
                        setBwd();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_ALIGN_BWD;
                    }
                    break;

                case STEP.CHECK_ALIGN_BWD:
                    {
                        if (isBwd() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_LOADER_ALIGNCV_ALIGN_CYL_BWD);

                                m_step = STEP.ALIGN_BWD;
                                return;
                            }
                            return;
                        }

                        m_step = STEP.ALIGN_UP;
                    }
                    break;

                case STEP.ALIGN_UP:
                    {
                        setUp();

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_ALIGN_UP;
                    }
                    break;

                case STEP.CHECK_ALIGN_UP:
                    {
                        if (isUp() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_LOADER_ALIGNCV_UPDOWN_CYL_UP);

                                m_step = STEP.ALIGN_UP;
                                return;
                            }
                            return;
                        }

                        m_step = STEP.TRIGGER_BCR;
                    }
                    break;

                case STEP.TRIGGER_BCR:
                    {

                        m_step = STEP.CHECK_BCR;
                    }
                    break;

                case STEP.CHECK_BCR:
                    {
                        ST_ALIGN_CV.setBcr("TEST_BCR");
                        m_step = STEP.WAIT_PICK;
                    }
                    break;

                case STEP.WAIT_PICK:
                    {
                        // WAIT PICK
                    }
                    break;

                case STEP.ALIGN_DOWN:
                    {
                        setDown();

                        m_cylTimeout.start();


                        m_step = STEP.CHECK_ALIGN_DOWN;
                    }
                    break;

                case STEP.CHECK_ALIGN_DOWN:
                    {
                        if (isDown() == false)
                        {
                            if (m_cylTimeout.isElasped() == true)
                            {
                                addAlarm(ALARM.CY_LOADER_ALIGNCV_UPDOWN_CYL_DOWN);

                                m_step = STEP.ALIGN_DOWN;
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

        public void setFwd()
        {
            setOutput(OUTPUT.ALIGN_CV_ALIGN_FWD, true);
            setOutput(OUTPUT.ALIGN_CV_ALIGN_BWD, false);
        }

        public void setBwd()
        {
            setOutput(OUTPUT.ALIGN_CV_ALIGN_FWD, false);
            setOutput(OUTPUT.ALIGN_CV_ALIGN_BWD, true);
        }   

        public bool isFwd()
        {
            return input(INPUT.ALIGN_CV_ALIGN_FWD);
        }

        public bool isBwd()
        {
            return input(INPUT.ALIGN_CV_ALIGN_BWD);
        }

        public void setUp()
        {
            setOutput(OUTPUT.ALIGN_CV_MOLD_UP, true);
            setOutput(OUTPUT.ALIGN_CV_MOLD_DOWN, false);
        }

        public void setDown()
        {
            setOutput(OUTPUT.ALIGN_CV_MOLD_UP, false);
            setOutput(OUTPUT.ALIGN_CV_MOLD_DOWN, true);
        }

        public bool isUp()
        {
            return input(INPUT.ALIGN_CV_MOLD_UP);
        }   

        public bool isDown()
        {
            return input(INPUT.ALIGN_CV_MOLD_DOWN);
        }


    }//class
}//namespace
