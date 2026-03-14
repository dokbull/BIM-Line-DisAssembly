using MySqlConnector.Logging;
using System;
using System.Linq.Expressions;

namespace bim_base
{
    public class ProcessLoaderCvWork : CProcess
    {
        public enum STEP
        {
            START = 0,

            CHECK_CONDITION,

            WAIT_CV_IN,
            RUN_CV_WORK,
            CHECK_CV_WORK,

            WAIT_BUTTON,
            CHECK_BUTTON,

            RUN_CV_OUT,
            CHECK_CV_OUT,

            WAIT_ALIGN_CV,

            RUN_CV_NEXT,
            CHECK_CV_NEXT,

            OVERRUN_CV,
            CHECK_OVERRUN_CV,
            
            END,
        }

        STEP m_step = STEP.START;
        STEP m_agoStep = STEP.END;

        CElaspedTimer m_cvRunTimer = new CElaspedTimer(10 * 1000);
        CElaspedTimer m_cvOverRunTime = new CElaspedTimer(1 * 1000);
        CElaspedTimer m_cylTimeout = new CElaspedTimer(2 * 1000);

        public ProcessLoaderCvWork(ProcessMain procMain) : base(procMain)
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

        public override void run()
        {
            if (m_isRun == false)
                return;

            if (main.isAlarm())
            {
                cvRun(false);
                return;
            }
            if (main.isAuto() == false)
            {
                cvRun(false);
                return;
            }

            if (m_step != m_agoStep)
                Debug.debug("ProcessLoaderCvWork::run STEP:" + m_step);

            m_agoStep = m_step;

            ProcessAlignCvWork ALIGN_CV;

            ALIGN_CV = main.procAlignCvWork();

            bool isDry = main.isDryRun();

            CSTATION ST_IN_CV = main.station(CSTATION.STATION.IN_WORK_CV);
            CSTATION ST_ALIGN_CV = main.station(CSTATION.STATION.ALIGN_CV);

            switch (m_step)
            {
                case STEP.START:
                    {
                        buttonLamp(false);

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_CONDITION;
                    }
                    break;

                case STEP.CHECK_CONDITION:
                    {
                        m_step = STEP.WAIT_CV_IN;
                    }
                    break;

                case STEP.WAIT_CV_IN:
                    {
                        if (isDry == true)
                        {
                            m_cvRunTimer.start();
                        }
                        else
                        { 
                            if (input(INPUT.MOLD_LD_CV_IN) == true)
                                m_cvRunTimer.start();
                        }

                        if (m_cvRunTimer.isStart() == false || m_cvRunTimer.isElasped() == true)
                            return;

                        ST_IN_CV.clear();

                        m_step = STEP.RUN_CV_WORK;
                    }
                    break;

                case STEP.RUN_CV_WORK:
                    {
                        cvRun(true);

                        m_step = STEP.CHECK_CV_WORK;
                    }
                    break;

                case STEP.CHECK_CV_WORK:
                    {
                        if (m_cvRunTimer.isElasped() == true)
                        {
                            setOutput(OUTPUT.MOLD_LD_CV_RUN, false);
                            m_step = STEP.WAIT_CV_IN;
                            return;
                        }

                        if (isDry == false)
                        {
                            if (input(INPUT.MOLD_LD_CV_MID) == false)
                                return;
                        }

                        cvRun(false);

                        ST_IN_CV.setType(CSTATION.TYPE.PRE_MOLD);

                        m_step = STEP.WAIT_BUTTON;
                    }
                    break;

                case STEP.WAIT_BUTTON:
                    {
                        buttonLamp(true);

                        if (isButtonPress() == false)
                            return;

                        m_step = STEP.CHECK_BUTTON;
                    }
                    break;

                case STEP.CHECK_BUTTON:
                    {
                        if (isButtonRelease() == false)
                            return;

                        buttonLamp(false);

                        ST_IN_CV.setType(CSTATION.TYPE.MOLD);

                        m_step = STEP.RUN_CV_OUT;
                    }
                    break;

                case STEP.RUN_CV_OUT:
                    {
                        cvRun(true);
                        m_cvRunTimer.start();

                        m_step = STEP.CHECK_CV_OUT;
                    }
                    break;

                case STEP.CHECK_CV_OUT:
                    {
                        if (m_cvRunTimer.isElasped() == true)
                        {
                            setOutput(OUTPUT.MOLD_LD_CV_RUN, false);
                            m_step = STEP.WAIT_CV_IN;
                            return;
                        }

                        if (isDry == false)
                        {
                            if (input(INPUT.MOLD_LD_CV_OUT) == false)
                                return;
                        }

                        cvRun(false);

                        m_step = STEP.WAIT_ALIGN_CV;
                    }
                    break;

                case STEP.WAIT_ALIGN_CV:
                    {
                        if (ALIGN_CV.isWaitCv() == false)
                            return;

                        ALIGN_CV.callCv();

                        m_step = STEP.RUN_CV_NEXT;
                    }
                    break;

                case STEP.RUN_CV_NEXT:
                    {
                        cvRun(true);

                        m_cvRunTimer.start();

                        m_step = STEP.CHECK_CV_NEXT;
                    }
                    break;

                case STEP.CHECK_CV_NEXT:
                    {
                        if (m_cvRunTimer.isElasped() == true)
                        {
                            setOutput(OUTPUT.MOLD_LD_CV_RUN, false);
                            m_step = STEP.WAIT_CV_IN;
                            return;
                        }

                        if (isDry == false)
                        {
                            if (input(INPUT.MOLD_LD_CV_OUT) == true)
                                return;
                        }

                        ST_IN_CV.move(ST_ALIGN_CV);

                        m_step = STEP.OVERRUN_CV;
                    }
                    break;

                case STEP.OVERRUN_CV:
                    {
                        m_cvOverRunTime.start();

                        m_step = STEP.CHECK_OVERRUN_CV;
                    }
                    break;

                case STEP.CHECK_OVERRUN_CV:
                    {
                        if (m_cvOverRunTime.isElasped() == false)
                            return;

                        cvRun(false);

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

        public void cvRun(bool value)
        {
            setOutput(OUTPUT.MOLD_LD_CV_RUN, value);
        }

        public void buttonLamp(bool value)
        {
            setOutput(OUTPUT.OP_PANEL_LEFT_START_SW, value);
            setOutput(OUTPUT.OP_PANEL_RIGHT_START_SW, value);
        }

        public bool isButtonPress()
        {
            if (input(INPUT.OP_PANEL_LEFT_START_SW) == true && input(INPUT.OP_PANEL_RIGHT_START_SW) == true)
                return true;

            return false;
        }

        public bool isButtonRelease()
        {
            if (input(INPUT.OP_PANEL_LEFT_START_SW) == false && input(INPUT.OP_PANEL_RIGHT_START_SW) == false)
                return true;

            return false;
        }
    }//class
}//namespace
