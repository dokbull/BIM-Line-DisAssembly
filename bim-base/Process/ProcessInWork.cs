using MySqlConnector.Logging;
using System;
using System.Linq.Expressions;

namespace bim_base
{
    public class ProcessInWork : CProcess
    {
        public enum STEP
        {
            START = 0,

            CHECK_CONDITION,

            WAIT_PICK,
            PICK,
            CHECK_PICK,

            WAIT_REVERSE,

            DROP,
            CHECK_DROP,

            AVOID_READY,
            CHECK_AVOID_READY,
            
            SEND_COMPLETE_REVERSE,

            END,
        }

        STEP m_step = STEP.START;
        STEP m_agoStep = STEP.END;

        public ProcessInWork(ProcessMain procMain) : base(procMain)
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
                return;

            if (main.isAuto() == false)
                return;

            if (m_step != m_agoStep)
                Debug.debug("ProcessInWork::run STEP:" + m_step);

            m_agoStep = m_step;

            bool isDry = main.isDryRun();

            ProcessAlignCvWork ALIGN_CV;
            ProcessInPP IN_PP;
            ProcessMoldReverseWork REVERSE;

            ALIGN_CV = main.procAlignCvWork();
            IN_PP = main.procInPP();
            REVERSE = main.procMoldReverseWork();


            CSTATION ST_ALIGN_CV = main.station(CSTATION.STATION.ALIGN_CV);
            CSTATION ST_IN_PP = main.station(CSTATION.STATION.IN_PP);
            CSTATION ST_REVERSE = main.station(CSTATION.STATION.MOLD_REVERSE);

            switch (m_step)
            {
                case STEP.START:
                    {

                        m_step = STEP.CHECK_CONDITION;
                    }
                    break;

                case STEP.CHECK_CONDITION:
                    {

                        m_step = STEP.WAIT_PICK;
                    }
                    break;

                case STEP.WAIT_PICK:
                    {
                        if (ALIGN_CV.isWaitPick() == false)
                            return;

                        m_step = STEP.PICK;
                    }
                    break;

                case STEP.PICK:
                    {
                        bool ret = IN_PP.start(ProcessInPP.TARGET.PICK, ProcessInPP.ACTION.PICK);

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_PICK;
                    }
                    break;

                case STEP.CHECK_PICK:
                    {
                        if (IN_PP.isRun() == true)
                            return;

                        ALIGN_CV.setCompletePick();
                        ST_ALIGN_CV.move(ST_IN_PP);

                        m_step = STEP.WAIT_REVERSE;
                    }
                    break;

                case STEP.WAIT_REVERSE:
                    {
                        if (REVERSE.isWaitMold() == false)
                            return;

                        m_step = STEP.DROP;
                    }
                    break;

                case STEP.DROP:
                    {
                        bool ret = IN_PP.start(ProcessInPP.TARGET.PLACE, ProcessInPP.ACTION.PLACE);

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_DROP;
                    }
                    break;

                case STEP.CHECK_DROP:
                    {
                        if (IN_PP.isRun() == true)
                            return;

                        ST_IN_PP.move(ST_REVERSE);

                        m_step = STEP.AVOID_READY;
                    }
                    break;

                case STEP.AVOID_READY:
                    {
                        bool ret = IN_PP.start(ProcessInPP.TARGET.READY, ProcessInPP.ACTION.WAIT);

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_AVOID_READY;
                    }
                    break;

                case STEP.CHECK_AVOID_READY:
                    {
                        if (IN_PP.isRun() == true)
                            return;

                        m_step = STEP.SEND_COMPLETE_REVERSE;
                    }
                    break;

                case STEP.SEND_COMPLETE_REVERSE:
                    {
                        REVERSE.setCompleteMold();

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
    }//class
}//namespace
