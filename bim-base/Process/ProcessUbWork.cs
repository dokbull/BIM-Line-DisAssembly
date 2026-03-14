using MySqlConnector.Logging;
using System;
using System.Linq.Expressions;

namespace bim_base
{
    public class ProcessUbWork : CProcess
    {
        public enum STEP
        {
            START = 0,

            CHECK_CONDITION,

            MOVE_READY,
            CHECK_MOVE_READY,

            WAIT_SHUTTLE,

            PICK_UB,
            CHECK_PICK_UB,

            SEND_SHUTTLE_COMPLETE,

            CHECK_REVERSE,

            MOVE_AVOID,
            CHECK_MOVE_AVOID,

            WAIT_REVERSE,

            PLACE_UB,
            CHECK_PLACE_UB,

            SEND_REVERSE_COMPLETE,

            END,
        }

        STEP m_step = STEP.START;
        STEP m_agoStep = STEP.END;

        int m_dryRunIdx = 0;

     
        public ProcessUbWork(ProcessMain procMain) : base(procMain)
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
                Debug.debug("ProcessUbWork::run STEP:" + m_step);

            m_agoStep = m_step;

            ProcessShuttleWork SHUTTLE;
            ProcessUbReverseWork REVERSE;
            ProcessUbPP UB_PP;

            SHUTTLE = main.procShuttleWork();
            REVERSE = main.procUbReverseWork();
            UB_PP = main.procUbPP();

            bool isDry = main.isDryRun();

            CSTATION ST_STAGE_2 = main.station(CSTATION.STATION.SHUTTLE_ST_2);
            CSTATION ST_UB_PP = main.station(CSTATION.STATION.UB_PP);
            CSTATION ST_REVERSE_FRONT = main.station(CSTATION.STATION.UB_REVERSE_FRONT);
            CSTATION ST_REVERSE_REAR = main.station(CSTATION.STATION.UB_REVERSE_REAR);

            switch (m_step)
            {
                case STEP.START:
                    {

                        m_step = STEP.CHECK_CONDITION;
                    }
                    break;

                case STEP.CHECK_CONDITION:
                    {

                        m_step = STEP.MOVE_READY;
                    }
                    break;

                case STEP.MOVE_READY:
                    {
                        bool ret = UB_PP.start(ProcessUbPP.TARGET.READY, ProcessUbPP.ACTION.WAIT);

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_MOVE_READY;
                    }
                    break;

                case STEP.CHECK_MOVE_READY:
                    {
                        if (UB_PP.isRun() == true)
                            return;

                        m_step = STEP.WAIT_SHUTTLE;
                    }
                    break;

                case STEP.WAIT_SHUTTLE:
                    {
                        if (SHUTTLE.isWaitUbWork() == false)
                            return;

                        m_step = STEP.PICK_UB;
                    }
                    break;

                case STEP.PICK_UB:
                    {
                        bool ret = UB_PP.start(ProcessUbPP.TARGET.PICK, ProcessUbPP.ACTION.PICK);

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_PICK_UB;
                    }
                    break;

                case STEP.CHECK_PICK_UB:
                    {
                        if (UB_PP.isRun() == true)
                            return;

                        if (isDry == false)
                        {
                            if (UB_PP.isVac() == false)
                            {
                                addAlarm(ALARM.VC_UNLOADER_UBPP_PICKER_ON);

                                m_step = STEP.PICK_UB;
                                return;
                            }
                        }

                        m_step = STEP.SEND_SHUTTLE_COMPLETE;
                    }
                    break;

                case STEP.SEND_SHUTTLE_COMPLETE:
                    {
                        ST_STAGE_2.setType(CSTATION.TYPE.BASE);
                        ST_UB_PP.setType(CSTATION.TYPE.UB);

                        SHUTTLE.setUbWorkComplete();

                        m_step = STEP.CHECK_REVERSE;
                    }
                    break;

                case STEP.CHECK_REVERSE:
                    {
                        if (REVERSE.isWaitUb() == false)
                        {
                            m_step = STEP.MOVE_AVOID;
                            return;
                        }

                        m_step = STEP.PLACE_UB;
                    }
                    break;

                case STEP.MOVE_AVOID:
                    {
                        bool ret = UB_PP.start(ProcessUbPP.TARGET.READY, ProcessUbPP.ACTION.WAIT);

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_MOVE_AVOID;
                    }
                    break;

                case STEP.CHECK_MOVE_AVOID:
                    {
                        if (UB_PP.isRun() == true)
                            return;

                        if (isDry == false)
                        {
                            if (UB_PP.isVac() == false)
                            {
                                addAlarm(ALARM.VC_UNLOADER_UBPP_PICKER_ON);
                                return;
                            }
                        }

                        m_step = STEP.WAIT_REVERSE;
                    }
                    break;

                case STEP.WAIT_REVERSE:
                    {
                        if (REVERSE.isWaitUb() == false)
                            return;

                        if (input(INPUT.UB_OUT_REVERSE_DETECT_1) == true && input(INPUT.UB_OUT_REVERSE_DETECT_2) == true)
                            return;

                        m_step = STEP.PLACE_UB;
                    }
                    break;

                case STEP.PLACE_UB:
                    {
                        bool ret = false;

                        if (isDry == true)
                        {
                            if (m_dryRunIdx == 0)
                            {
                                ret = UB_PP.start(ProcessUbPP.TARGET.PLACE_F, ProcessUbPP.ACTION.PLACE);
                            }
                            else
                            {
                                ret = UB_PP.start(ProcessUbPP.TARGET.PLACE_R, ProcessUbPP.ACTION.PLACE);
                            }

                            if (ret == false)
                                return;

                            m_step = STEP.CHECK_PLACE_UB;
                            return;
                        }

                        if (input(INPUT.UB_OUT_REVERSE_DETECT_1) == false)
                        {
                            ret = UB_PP.start(ProcessUbPP.TARGET.PLACE_F, ProcessUbPP.ACTION.PLACE);
                        }
                        else if (input(INPUT.UB_OUT_REVERSE_DETECT_2) == false)
                        {
                            ret = UB_PP.start(ProcessUbPP.TARGET.PLACE_R, ProcessUbPP.ACTION.PLACE);
                        }
                        else
                        {
                            m_step = STEP.MOVE_AVOID;
                            return;
                        }

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_PLACE_UB;
                    }
                    break;

                case STEP.CHECK_PLACE_UB:
                    {
                        if (UB_PP.isRun() == true)
                            return;

                        if (isDry)
                            m_dryRunIdx++;

                        if (UB_PP.lastTarget() == ProcessUbPP.TARGET.PLACE_F)
                            ST_UB_PP.move(ST_REVERSE_FRONT);

                        if (UB_PP.lastTarget() == ProcessUbPP.TARGET.PLACE_R)
                            ST_UB_PP.move(ST_REVERSE_REAR);

                        m_step = STEP.SEND_REVERSE_COMPLETE;
                    }
                    break;

                case STEP.SEND_REVERSE_COMPLETE:
                    {
                        if (isDry == true)
                        {
                            if (m_dryRunIdx > 1)
                            {
                                m_dryRunIdx = 0;
                                REVERSE.setCompleteUb();
                            }
                        }
                        else
                        {
                            REVERSE.setCompleteUb();
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
    }//class
}//namespace
