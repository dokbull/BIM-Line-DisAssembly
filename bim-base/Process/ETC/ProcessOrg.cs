using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bim_base
{
    public class ProcessOrg : CProcess
    {
        public enum STEP
        {
            START = 0,

            SET_SERVO_ON,
            CHECK_SERVO_ON,

            SET_CYL_FIRST,
            CHECK_CYL_FIRST,

            MOVE_FIRST_ORG,
            CHECK_FIRST_ORG,
            CHECK_FIRST_POS,

            SET_CYL_SECOND,
            CHECK_CYL_SECOND,

            MOVE_SECOND_ORG,
            CHECK_SECOND_ORG,
            CHECK_SECOND_POS,

            SET_CYL_LAST,
            CHECK_CYL_LAST,

            MOVE_LAST_ORG,
            CHECK_LAST_ORG,
            CHECK_LAST_POS,

            ERROR,
            END,
        }

        STEP m_step = STEP.START;
        STEP m_agoStep = STEP.END;

        List<ExtAxis> axisList = new List<ExtAxis>();
        List<ExtAxis> firstAxisList = new List<ExtAxis>();
        List<ExtAxis> secondAxisList = new List<ExtAxis>();
        List<ExtAxis> lastAxisList = new List<ExtAxis>();

        CElaspedTimer[] m_homeTimer = new CElaspedTimer[(int)AXIS.MAX];
        CElaspedTimer m_servoOnTimer = new CElaspedTimer(1000);
        CElaspedTimer m_cylTimeout = new CElaspedTimer(1 * 5000);

        bool m_checkServoOn = false;

        public ProcessOrg(ProcessMain procMain) : base(procMain)
        {
            for (int i = 0; i < m_homeTimer.Length; i++)
            {
                m_homeTimer[i] = new CElaspedTimer(60 * 1000);
            }
        }

        public STEP step() { return m_step; }
        public STEP agoStep() { return m_agoStep; }

        public void start(List<ExtAxis> list)
        {
            if (list.Count == 0)
                return;

            clearAxisList();

            foreach (ExtAxis axis in list)
            {
                AXIS axisEnum = (AXIS)axis.no();

                switch (axisEnum)
                {
                    case AXIS.IN_PP_Z:
                    case AXIS.MOLD_PP_ZL:
                    case AXIS.MOLD_PP_ZR:
                    case AXIS.UB_PP_Z:
                        {
                            firstAxisList.Add(axis);
                        }
                        break;

                    case AXIS.IN_PP_Y:
                    case AXIS.UB_PP_Y:
                        {
                            secondAxisList.Add(axis);
                        }
                        break;

                    case AXIS.MOLD_PP_X:
                    case AXIS.BASE_X:
                        {
                            lastAxisList.Add(axis);
                        }
                        break;
                }

                axisList.Add(axis);
            }

            m_complete = false;
            m_step = STEP.START;
            base.start();
        }

        void clearAxisList()
        {
            axisList.Clear();
            firstAxisList.Clear();
            secondAxisList.Clear();
            lastAxisList.Clear();
        }   

        bool homeMove(List<ExtAxis> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                ExtAxis axis = list[i];

                if (axis.orgComplete() == true)
                    continue;

                m_homeTimer[axis.no()].start();
                axis.homeMove(axis.homeDir());
            }

            return true;
        }

        bool homeCheck(List<ExtAxis> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                ExtAxis axis = list[i];

                if (axis.orgComplete() == false)
                {
                    if (m_homeTimer[axis.no()].isElasped())
                    {
                        main.addAlarm(ALARM.MO_UNLOADER_UBPP_AXIS_Z_HOME_FAIL + i);
                    }

                    return false;
                }
            }

            return true;
        }

        void moveOrgPos(List<ExtAxis> list)
        {
            foreach (ExtAxis axis in list)
            {
                if (axis.name().Contains("Z") == true)
                    axis.absMove(20.0d);
                else
                    axis.absMove(0);
            }
        }

        bool checkInpos(List<ExtAxis> list)
        {
            foreach (ExtAxis axis in list)
            {
                if (axis.inpos() == false)
                    return false;
            }

            return true;
        }

        public override void run()
        {
            if (m_isRun == false)
                return;

            if (main.isAlarm())
            {
                m_step = STEP.ERROR;
                return;
            }

            if (input(INPUT.OP_BOX_STOP_SW) == true)
            {
                for (int i = 0; i < axisList.Count; i++)
                {
                    axisList[i].stop();
                }

                m_isRun = false;
                return;
            }

            if (m_step != m_agoStep)
            {
                Debug.debug("ProcessOrg::run STEP:" + m_step);
            }

            m_agoStep = m_step;

            switch (m_step)
            {
                case STEP.START:
                    {
                        main.setIsNeedOrging(true);
                        m_complete = false;

                        m_step = STEP.SET_SERVO_ON;
                    }
                    break;

                case STEP.SET_SERVO_ON:
                    {
                        m_checkServoOn = false;

                        for (int i = 0; i < axisList.Count; i++)
                        {
                            if (axisList[i].isServoOn() == false)
                            {
                                axisList[i].setServoOn(true);
                                m_checkServoOn = true;
                            }
                        }

                        if (m_checkServoOn == true)
                            m_servoOnTimer.start();

                        m_step = STEP.CHECK_SERVO_ON;
                    }
                    break;

                case STEP.CHECK_SERVO_ON:
                    {
                        if (m_checkServoOn == true && m_servoOnTimer.isElasped() == false)
                            return;

                        for (int i = 0; i < axisList.Count; i++)
                        {
                            if (axisList[i].isServoOn() == false)
                            {
                                addAlarm(ALARM.MO_UNLOADER_UBPP_AXIS_Z_SERVO_OFF + i, ALARM_TYPE.CRITICAL);
                                return;
                            }
                        }

                        m_step = STEP.SET_CYL_FIRST;
                    }
                    break;

                case STEP.SET_CYL_FIRST:
                    {
                        setOutput(OUTPUT.MOLD_ULD_CV_MOLD_DOWN, true);
                        setOutput(OUTPUT.MOLD_ULD_CV_MOLD_UP, false);

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_CYL_FIRST;
                    }
                    break;

                case STEP.CHECK_CYL_FIRST:
                    {
                        if (m_cylTimeout.isElasped() == true)
                        {
                            if (input(INPUT.MOLD_ULD_CV_DOWN) == false)
                                addAlarm(ALARM.CY_UNLOADER_RETURNCV_UPDOWN_CYL_DOWN);

                            m_step = STEP.SET_CYL_FIRST;
                            return;
                        }

                        if (input(INPUT.MOLD_ULD_CV_DOWN) == false)
                            return;


                        m_step = STEP.MOVE_FIRST_ORG;
                    }
                    break;

                case STEP.MOVE_FIRST_ORG:
                    {
                        homeMove(firstAxisList);

                        m_step = STEP.CHECK_FIRST_ORG;
                    }
                    break;

                case STEP.CHECK_FIRST_ORG:
                    {
                        if (homeCheck(firstAxisList) == false)
                            return;

                        moveOrgPos(firstAxisList);

                        m_step = STEP.CHECK_FIRST_POS;
                    }
                    break;

                case STEP.CHECK_FIRST_POS:
                    {
                        if (checkInpos(firstAxisList) == false)
                            return;

                        m_step = STEP.SET_CYL_SECOND;
                    }
                    break;

                case STEP.SET_CYL_SECOND:
                    {
                        setOutput(OUTPUT.ALIGN_CV_MOLD_DOWN, true);
                        setOutput(OUTPUT.ALIGN_CV_MOLD_UP, false);

                        setOutput(OUTPUT.SHUTTLE_MOLD_UP, false);
                        setOutput(OUTPUT.SHUTTLE_MOLD_DOWN, true);

                        setOutput(OUTPUT.SHUTTLE_SERVO_MOLD_UP, false);
                        setOutput(OUTPUT.SHUTTLE_SERVO_MOLD_DOWN, true);

                        setOutput(OUTPUT.UB_OUT_REVERSE_UP_1, true);
                        setOutput(OUTPUT.UB_OUT_REVERSE_DOWN_1, false);
                        setOutput(OUTPUT.UB_OUT_REVERSE_UP_2, true);
                        setOutput(OUTPUT.UB_OUT_REVERSE_DOWN_2, false);

                        setOutput(OUTPUT.UB_OUT_PP_BWD, true);
                        setOutput(OUTPUT.UB_OUT_PP_FWD, false);

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_CYL_SECOND;
                    }
                    break;

                case STEP.CHECK_CYL_SECOND:
                    {
                        if (m_cylTimeout.isElasped() == true)
                        {
                            if (input(INPUT.ALIGN_CV_MOLD_DOWN) == false)
                                addAlarm(ALARM.CY_LOADER_ALIGNCV_UPDOWN_CYL_DOWN);

                            if (input(INPUT.MOLD_SHUTTLE_DOWN) == false)
                                addAlarm(ALARM.CY_REVERSE_MOLDREVERSE_UBDOWN_CYL_DOWN);

                            if (input(INPUT.MOLD_SHUTTLE_SERVO_DOWN_1) == false)
                                addAlarm(ALARM.CY_BASE_MOLDBASE_UPDOWN_CYL_DOWN);

                            if (input(INPUT.MOLD_SHUTTLE_SERVO_DOWN_2) == false)
                                addAlarm(ALARM.CY_BASE_MOLDBASE_UPDOWN_CYL_DOWN);

                            if (input(INPUT.UB_OUT_REVERSE_UP_1) == false)
                                addAlarm(ALARM.CY_REVERSE_UBREVERSE_UPDOWN1_CYL_UP);

                            if (input(INPUT.UB_OUT_REVERSE_UP_2) == false)
                                addAlarm(ALARM.CY_REVERSE_UBREVERSE_UPDOWN2_CYL_UP);

                            if (input(INPUT.UB_OUT_PP_BWD) == false)
                                addAlarm(ALARM.CY_UNLOADER_UBPP_PICKER_CYL_BWD);

                            m_step = STEP.SET_CYL_SECOND;
                            return;
                        }

                        if (input(INPUT.MOLD_SHUTTLE_DOWN) == false)
                            return;

                        if (input(INPUT.MOLD_SHUTTLE_SERVO_DOWN_1) == false)
                            return;

                        if (input(INPUT.MOLD_SHUTTLE_SERVO_DOWN_2) == false)
                            return;

                        if (input(INPUT.UB_OUT_REVERSE_UP_1) == false)
                            return;

                        if (input(INPUT.UB_OUT_REVERSE_UP_2) == false)
                            return;

                        if (input(INPUT.UB_OUT_PP_BWD) == false)
                            return;

                        m_step = STEP.MOVE_SECOND_ORG;
                    }
                    break;

                case STEP.MOVE_SECOND_ORG:
                    {
                        homeMove(secondAxisList);

                        m_step = STEP.CHECK_SECOND_ORG;
                    }
                    break;

                case STEP.CHECK_SECOND_ORG:
                    {
                        if (homeCheck(secondAxisList) == false)
                            return;

                        moveOrgPos(secondAxisList);

                        m_step = STEP.CHECK_SECOND_POS;
                    }
                    break;

                case STEP.CHECK_SECOND_POS:
                    {
                        if (checkInpos(secondAxisList) == false)
                            return;

                        m_step = STEP.SET_CYL_LAST;
                    }
                    break;

                case STEP.SET_CYL_LAST:
                    {
                        setOutput(OUTPUT.UB_OUT_REVERSE_RETURN_1, true);
                        setOutput(OUTPUT.UB_OUT_REVERSE_TURN_1, false);
                        setOutput(OUTPUT.UB_OUT_REVERSE_RETURN_2, true);
                        setOutput(OUTPUT.UB_OUT_REVERSE_TURN_2, false);

                        m_cylTimeout.start();

                        m_step = STEP.CHECK_CYL_LAST;
                    }
                    break;

                case STEP.CHECK_CYL_LAST:
                    {
                        if (m_cylTimeout.isElasped() == true)
                        {
                            if (input(INPUT.UB_OUT_REVERSE_RETURN_1) == false)
                                addAlarm(ALARM.CY_REVERSE_UBREVERSE_TURN1_CYL_RETURN);

                            if (input(INPUT.UB_OUT_REVERSE_RETURN_2) == false)
                                addAlarm(ALARM.CY_REVERSE_UBREVERSE_TURN2_CYL_RETURN);

                            m_step = STEP.SET_CYL_LAST;
                            return;
                        }

                        if (input(INPUT.UB_OUT_REVERSE_RETURN_1) == false)
                            return;

                        if (input(INPUT.UB_OUT_REVERSE_RETURN_2) == false)
                            return;

                        m_step = STEP.MOVE_LAST_ORG;
                    }
                    break;

                case STEP.MOVE_LAST_ORG:
                    {
                        homeMove(lastAxisList);

                        m_step = STEP.CHECK_LAST_ORG;
                    }
                    break;

                case STEP.CHECK_LAST_ORG:
                    {
                        if (homeCheck(lastAxisList) == false)
                            return;

                        moveOrgPos(lastAxisList);

                        m_step = STEP.CHECK_LAST_POS;
                    }
                    break;

                case STEP.CHECK_LAST_POS:
                    {
                        if (checkInpos(lastAxisList) == false)
                            return;

                        m_step = STEP.END;
                    }
                    break;

                case STEP.ERROR:
                    {
                        m_complete = false;
                        m_isRun = false;
                    }
                    break;

                case STEP.END:
                    {
                        main.setIsNeedOrging(false);

                        main.procLoaderCvWork().resetWork();
                        main.procAlignCvWork().resetWork();
                        main.procOutMoldCvWork().resetWork();
                        main.processOutUbCvWork().resetWork();

                        main.procInWork().resetWork();
                        main.procMoldReverseWork().resetWork();
                        main.procMoldWork().resetWork();
                        main.procShuttleWork().resetWork();
                        main.procUbReverseWork().resetWork();
                        main.procUbWork().resetWork();

                        main.procInPP().stop();
                        main.procMoldPP().stop();
                        main.procUbPP().stop();
                        main.procMoldBase().stop();

                        m_complete = true;
                        m_isRun = false;
                    }
                    break;
            }
        }
    }//class
}//namespace
