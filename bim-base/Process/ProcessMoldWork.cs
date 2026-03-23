using MySqlConnector.Logging;
using System;
using System.Linq.Expressions;

namespace bim_base
{
    public class ProcessMoldWork : CProcess
    {
        public enum STEP
        {
            START = 0,

            CHECK_CONDITION,

            WAIT_SHUTTLE_RIGHT,

            CHECK_PLACE,

            PLACE_RIGHT_COVER,
            CHECK_PLACE_RIGHT_COVER,

            WAIT_SHUTTLE_LEFT,

            CALL_LEFT,
            CHECK_CALL_LEFT,

            PICK_LEFT,
            CHECK_PICK_LEFT,

            MOVE_RIGHT,
            CHECK_MOVE_RIGHT,

            SET_COMPLETE_SHUTTLE,

            WAIT_OUT_CV,

            PLACE_RIGHT_MOLD,
            CHECK_PLACE_RIGHT_MOLD,

            END,
        }

        STEP m_step = STEP.START;
        STEP m_agoStep = STEP.END;

        CStopWatch m_tactTimeCheck = new CStopWatch();
        
        public ProcessMoldWork(ProcessMain procMain) : base(procMain)
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

        public bool isWaitShuttleRight()
        {
            if (m_step == STEP.WAIT_SHUTTLE_RIGHT)
                return true;

            return false;
        }

        public void setShuttleRightCall()
        {
            if (isWaitShuttleRight() == false)
                return;

            m_step = STEP.CHECK_PLACE;
        }

        public long elaspedTactTime()
        {
            return m_tactTimeCheck.GetElapsedTime(CStopWatch.TIME_UNIT.MILLISECOND, false);
        }

        public bool isWaitShuttleLeft()
        {
            if (m_step == STEP.WAIT_SHUTTLE_LEFT)
                return true;

            return false;
        }

        public void setShuttleLeftCall()
        {
            if (isWaitShuttleLeft() == false)
                return;

            m_step = STEP.CALL_LEFT;
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
                Debug.debug("ProcessMoldWork::run STEP:" + m_step);

            m_agoStep = m_step;

            bool isDry = main.isDryRun();

            ProcessShuttleWork SHUTTLE;
            ProcessMoldPP MOLD_PP;
            ProcessUbPP UB_PP;
            ProcessOutMoldCvWork OUT_CV;

            SHUTTLE = main.procShuttleWork();
            MOLD_PP = main.procMoldPP();
            UB_PP = main.procUbPP();
            OUT_CV = main.procOutMoldCvWork();

            CSTATION ST_STAGE_2 = main.station(CSTATION.STATION.SHUTTLE_ST_2);
            CSTATION ST_STAGE_3 = main.station(CSTATION.STATION.SHUTTLE_ST_3);
            CSTATION ST_LEFT_PP = main.station(CSTATION.STATION.MOLD_PP_LEFT);
            CSTATION ST_RIGHT_PP = main.station(CSTATION.STATION.MOLD_PP_RIGHT);
            CSTATION ST_OUT_CV = main.station(CSTATION.STATION.OUT_MOLD_CV);

            switch (m_step)
            {
                case STEP.START:
                    {

                        m_step = STEP.CHECK_CONDITION;
                    }
                    break;

                case STEP.CHECK_CONDITION:
                    {
                        m_step = STEP.WAIT_SHUTTLE_RIGHT;
                    }
                    break;

                case STEP.WAIT_SHUTTLE_RIGHT:
                    {
                        // WAIT CALL SHUTTLE RIGHT
                    }
                    break;

                case STEP.CHECK_PLACE:
                    {
                        if (ST_LEFT_PP.type() == CSTATION.TYPE.COVER)
                        {
                            if (isDry == true || ST_STAGE_3.type() == CSTATION.TYPE.BASE)
                            {
                                m_step = STEP.PLACE_RIGHT_COVER;
                                return;
                            }
                            else
                            {
                                addAlarm(ALARM.SE_BASE_MOLDBASE_ST3_NOT_PRODUCT);
                                return;
                            }
                        }

                        m_step = STEP.WAIT_SHUTTLE_LEFT;
                    }
                    break;

                case STEP.PLACE_RIGHT_COVER:
                    {
                        bool ret = MOLD_PP.start(ProcessMoldPP.TARGET.RIGHT, ProcessMoldPP.ACTION.PLACE_LEFT);

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_PLACE_RIGHT_COVER;
                    }
                    break;

                case STEP.CHECK_PLACE_RIGHT_COVER:
                    {
                        if (MOLD_PP.isRun() == true)
                            return;

                        ST_STAGE_3.setType(CSTATION.TYPE.MOLD);
                        ST_LEFT_PP.clear();

                        m_step = STEP.WAIT_SHUTTLE_LEFT;
                    }
                    break;

                case STEP.WAIT_SHUTTLE_LEFT:
                    {
                        if (SHUTTLE.isWaitMoldWork() == false)
                            return;

                        m_step = STEP.PICK_LEFT;
                    }
                    break;

                case STEP.CALL_LEFT:
                    {
                        bool ret = MOLD_PP.start(ProcessMoldPP.TARGET.LEFT, ProcessMoldPP.ACTION.WAIT);

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_CALL_LEFT;
                    }
                    break;

                case STEP.CHECK_CALL_LEFT:
                    {
                        if (MOLD_PP.isRun() == true)
                            return;                      

                        m_step = STEP.WAIT_SHUTTLE_LEFT;
                    }
                    break;

                case STEP.PICK_LEFT:
                    {
                        bool ret = true;

                        if (isDry == true)
                        {
                            ret &= MOLD_PP.start(ProcessMoldPP.TARGET.LEFT, ProcessMoldPP.ACTION.PICK_BOTH);
                        }
                        else
                        {
                            if (input(INPUT.MOLD_SHUTTLE_STAGE_2_DETECT) == true && input(INPUT.MOLD_SHUTTLE_STAGE_3_DETECT) == false)
                                ret &= MOLD_PP.start(ProcessMoldPP.TARGET.LEFT, ProcessMoldPP.ACTION.PICK_LEFT);
                            else if (input(INPUT.MOLD_SHUTTLE_STAGE_3_DETECT) == true && input(INPUT.MOLD_SHUTTLE_STAGE_2_DETECT) == false)
                                ret &= MOLD_PP.start(ProcessMoldPP.TARGET.LEFT, ProcessMoldPP.ACTION.PICK_RIGHT);
                            else
                                ret &= MOLD_PP.start(ProcessMoldPP.TARGET.LEFT, ProcessMoldPP.ACTION.PICK_BOTH);
                        }

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_PICK_LEFT;
                    }
                    break;

                case STEP.CHECK_PICK_LEFT:
                    {
                        if (MOLD_PP.isRun() == true)
                            return;

                        if (isDry == false)
                        {
                            if (MOLD_PP.lastAction() == ProcessMoldPP.ACTION.PICK_LEFT || MOLD_PP.lastAction() == ProcessMoldPP.ACTION.PICK_BOTH)
                            {
                                if (input(INPUT.MOLD_SHUTTLE_STAGE_2_DETECT) == false)
                                {
                                    addAlarm(ALARM.SE_BASE_MOLDBASE_ST2_NOT_PRODUCT);
                                    return;
                                }
                            }
                        }

                        if (MOLD_PP.lastAction() == ProcessMoldPP.ACTION.PICK_LEFT || MOLD_PP.lastAction() == ProcessMoldPP.ACTION.PICK_BOTH)
                        {
                            ST_STAGE_2.setType(CSTATION.TYPE.BASE_UB);
                            ST_LEFT_PP.setType(CSTATION.TYPE.COVER);
                        }

                        if (MOLD_PP.lastAction() == ProcessMoldPP.ACTION.PICK_RIGHT || MOLD_PP.lastAction() == ProcessMoldPP.ACTION.PICK_BOTH)
                        {
                            ST_STAGE_3.move(ST_RIGHT_PP);
                        }

                        m_step = STEP.MOVE_RIGHT;
                    }
                    break;

                case STEP.MOVE_RIGHT:
                    {
                        bool ret = MOLD_PP.start(ProcessMoldPP.TARGET.RIGHT, ProcessMoldPP.ACTION.WAIT);

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_MOVE_RIGHT;
                    }
                    break;

                case STEP.CHECK_MOVE_RIGHT:
                    {
                        if (MOLD_PP.isRun() == true)
                            return;

                        m_step = STEP.SET_COMPLETE_SHUTTLE;
                    }
                    break;

                case STEP.SET_COMPLETE_SHUTTLE:
                    {
                        SHUTTLE.setMoldWorkComplete();

                        if (ST_RIGHT_PP.type() == CSTATION.TYPE.EMPTY)
                        {
                            m_step = STEP.END;
                            return;
                        }

                        m_step = STEP.WAIT_OUT_CV;
                    }
                    break;

                case STEP.WAIT_OUT_CV:
                    {
                        if (input(INPUT.MOLD_ULD_CV_IN) == true)
                            return;

                        OUT_CV.setLock(true);

                        m_step = STEP.PLACE_RIGHT_MOLD;
                    }
                    break;

                case STEP.PLACE_RIGHT_MOLD:
                    {
                        bool ret = MOLD_PP.start(ProcessMoldPP.TARGET.RIGHT, ProcessMoldPP.ACTION.PLACE_RIGHT);

                        if (ret == false)
                            return;

                        m_step = STEP.CHECK_PLACE_RIGHT_MOLD;
                    }
                    break;

                case STEP.CHECK_PLACE_RIGHT_MOLD:
                    {
                        if (MOLD_PP.isRun() == true)
                            return;
                     
                        ST_RIGHT_PP.move(ST_OUT_CV);
                        OUT_CV.setLock(false);

                        if (m_tactTimeCheck.isStart() == false)
                        {
                            m_tactTimeCheck.Start();
                        }
                        else
                        {
                            long time = m_tactTimeCheck.GetElapsedTime(CStopWatch.TIME_UNIT.MILLISECOND, true);
                            main.addCycleTime(time);
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
