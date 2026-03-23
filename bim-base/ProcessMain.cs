using bim_base.data.CIM;
using DevAge.Windows.Forms;
using lib.plc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static bim_base.data.CIM.CIMEnumeric;
using static CIMWrite;

namespace bim_base
{
    public partial class ProcessMain
    {
        AjinLib m_lib = null;
        AjinDIO m_dioIn = null;
        AjinDIO m_dioOut = null;
        AjinAIO m_aioIn = null;

        CSerialFRENIC m_frenic = null;

        // AXIS LIST
        List<ExtAxis> m_axis = new List<ExtAxis>();
        ExtAxis IN_PP_Y, IN_PP_Z;
        ExtAxis MOLD_PP_X, MOLD_PP_ZL, MOLD_PP_ZR;
        ExtAxis UB_PP_Y, UB_PP_Z;
        ExtAxis BASE_X;

        ProcessLoaderCvWork m_procLoaderCvWork = null;
        ProcessAlignCvWork m_procAlignCvWork = null;
        ProcessInWork m_procInWork = null;
        ProcessMoldReverseWork m_procMoldReverseWork = null;
        ProcessShuttleWork m_procShuttleWork = null;
        ProcessMoldWork m_procMoldWork = null;
        ProcessUbWork m_procUbWork = null;
        ProcessUbReverseWork m_procUbReverseWork = null;
        ProcessOutMoldCvWork m_procOutMoldCvWork = null;
        ProcessOutUbCvWork m_procOutUbCvWork = null;

        ProcessInPP m_procInPP = null;
        ProcessMoldPP m_procMoldPP = null;
        ProcessMoldBase m_procMoldBase = null;
        ProcessUbPP m_procUbPP = null;

        ProcessManualInPP m_procManualInPP = null;
        ProcessManualMoldPP m_procManualMoldPP = null;
        ProcessManualUbPP m_procManualUbPP = null;

        ProcessOrg m_procOrg = null;

        Thread m_thread = null;
        Thread m_frenicThread = null;

        bool m_stop = false;

        bool m_isAuto = false;
        bool m_isCycleStop = false;

        bool m_isAlarm = false;
        bool m_isLightAlarm = false;
        bool m_isShowInitCheckMsg = false;

        bool m_simulation = false;

        List<AlarmData> m_lightAlarmList = new List<AlarmData>();
        List<AlarmData> m_heavyAlarmList = new List<AlarmData>();

        bool[] m_input = null;
        bool[] m_output = null;

        List<long> m_tactTimeList = new List<long>();
        bool m_isTactTimeUpdate = false;

        bool m_inputStop = false;
        bool m_outputStop = false;
        bool m_outOfPPlan = false;

        int m_outputCount = 0;

        CLogManager m_alarmManager = null;
        CLogManager m_setupLogMgr = null;
        CLogManager m_mesLogMgr = null;
        CLogManager m_mcStatusLog = null;

        bool m_once = false;
        bool m_emergencyFlag = false;
        bool m_isNeedOrgin = true;

        bool m_manualTestIO = false;
        bool m_showEmergencyAlarm = false;
        bool m_showMcAlarm = false;

        bool m_lastWorkMode = false;
       
        // BOARD RECONNECT 
        CElaspedTimer m_boardReconnectTimer = new CElaspedTimer(60 * 1000);
        bool m_isBoardConnectWait = false;

        // MANUAL SERVO ALARM
        bool m_servoAlarmRise = false;

        MACHINE_STATE m_state = MACHINE_STATE.AUTO;
        JOG_SPEED m_jogSpeed = JOG_SPEED.LOW;

        // STATION
        CSTATION[] m_station = new CSTATION[(int)CSTATION.STATION.MAX];

        // ADMIN
        bool m_admin = false;

        // INTERFACE
        bool m_forceCheckIF = false;

        // MODE
        public TimerDelay _TimSetup = new TimerDelay();
        public bool m_bSetupVS = false;
        public TimerDelay _TimSetupVS = new TimerDelay();

        // SCAN TIME
        CStopWatch m_stopWatch = new CStopWatch();
        long m_scanTime = 0;

        public ProcessMain()
        {
            if (File.Exists(Common.PATH + "\\simulation") == true)
                m_simulation = true;

            Conf.load();

            Debug.setPath(Common.LOG_PATH, "dev-log");
            m_alarmManager = new CLogManager("alarm", Common.LOG_PATH);
            m_setupLogMgr = new CLogManager("setup", Common.LOG_PATH);
            m_mesLogMgr = new CLogManager("mes", Common.LOG_PATH);
            m_mcStatusLog = new CLogManager("Error Stop Time", "C:\\FA\\", "", "Error Stop Time");

            IN_PP_Y = new ExtAxis(this, (int)AXIS.IN_PP_Y, "IN PP Y");
            IN_PP_Z = new ExtAxis(this, (int)AXIS.IN_PP_Z, "IN PP Z");

            BASE_X = new ExtAxis(this, (int)AXIS.BASE_X, "BASE X");

            MOLD_PP_X = new ExtAxis(this, (int)AXIS.MOLD_PP_X, "MOLD PP X");
            MOLD_PP_ZR = new ExtAxis(this, (int)AXIS.MOLD_PP_ZR, "MOLD PP ZR");
            MOLD_PP_ZL = new ExtAxis(this, (int)AXIS.MOLD_PP_ZL, "MOLD PP ZL");
            
            UB_PP_Y = new ExtAxis(this, (int)AXIS.UB_PP_Y, "UB PP Y");
            UB_PP_Z = new ExtAxis(this, (int)AXIS.UB_PP_Z, "UB PP Z");

            m_axis.Add(UB_PP_Z);
            m_axis.Add(UB_PP_Y);
            m_axis.Add(MOLD_PP_ZL);
            m_axis.Add(MOLD_PP_ZR);
            m_axis.Add(MOLD_PP_X);
            m_axis.Add(BASE_X);
            m_axis.Add(IN_PP_Z);
            m_axis.Add(IN_PP_Y);

            if (m_simulation == false)
            {
                m_lib = new AjinLib();
                m_lib.open();

                for (int i = 0; i < m_axis.Count; i++)
                {
                    m_axis[i].setUnitPerPulse(1, 1000);
                    m_axis[i].setAccelUnit(AXM_ACCEL_UNIT.SEC);
                    m_axis[i].setLimit(AXT_MOTION_STOPMODE.SLOWDOWN_STOP, AXM_LEVEL.LOW, AXM_LEVEL.LOW);
                    
                    if (m_axis[i].name().Contains("Z"))
                    {
                        //m_axis[i].setAbsSpeed(Conf.vel((AXIS)i), Conf.acc((AXIS)i), Conf.dec((AXIS)i));
                        m_axis[i].setAbsSpeed(100.0d, Conf.acc((AXIS)i), Conf.dec((AXIS)i));
                        m_axis[i].setHomeDir(AXT_MOTION_MOVE_DIR.DIR_CW);
                        m_axis[i].homeSetVel(30, 5, 1, 1, 0.25, 0.25);
                    }
                    else
                    {
                        //m_axis[i].setAbsSpeed(Conf.vel((AXIS)i), Conf.acc((AXIS)i), Conf.dec((AXIS)i));
                        m_axis[i].setAbsSpeed(100.0d, Conf.acc((AXIS)i), Conf.dec((AXIS)i));
                        m_axis[i].homeSetVel(50, 15, 1, 1, 0.25, 0.25);
                    }
                }

                m_axis[(int)AXIS.MOLD_PP_X].setAbsSpeed(100.0d, Conf.acc(AXIS.MOLD_PP_X), Conf.dec(AXIS.MOLD_PP_X));

                int[] inputIds = new int[] { 22, 23, 24, 18, 19, 0, 1, 2, 3, 4, 5, 6, 7, 8 };
                int[] outputIds = new int[] { 25, 26, 20, 21, 9, 10, 11, 12, 13, 14, 15, 16, 17 };

                m_input = new bool[16 * inputIds.Length];
                m_output = new bool[16 * outputIds.Length];

                m_dioIn = new AjinDIO(AjinDIO.TYPE.INPUT, inputIds);
                m_dioOut = new AjinDIO(AjinDIO.TYPE.OUTPUT, outputIds);
                m_aioIn = new AjinAIO();
            }
            else
            {
                m_input = new bool[16 * 16];
                m_output = new bool[16 * 16];
            }

            m_procLoaderCvWork = new ProcessLoaderCvWork(this);
            m_procAlignCvWork = new ProcessAlignCvWork(this);
            m_procInWork = new ProcessInWork(this);
            m_procMoldReverseWork = new ProcessMoldReverseWork(this);
            m_procMoldWork = new ProcessMoldWork(this);
            m_procShuttleWork = new ProcessShuttleWork(this);
            m_procUbWork = new ProcessUbWork(this);
            m_procUbReverseWork = new ProcessUbReverseWork(this);
            m_procOutMoldCvWork = new ProcessOutMoldCvWork(this);
            m_procOutUbCvWork = new ProcessOutUbCvWork(this);

            m_procInPP = new ProcessInPP(this);
            m_procMoldPP = new ProcessMoldPP(this);
            m_procMoldBase = new ProcessMoldBase(this);
            m_procUbPP = new ProcessUbPP(this);

            m_procManualInPP = new ProcessManualInPP(this);
            m_procManualMoldPP = new ProcessManualMoldPP(this);
            m_procManualUbPP = new ProcessManualUbPP(this);

            m_procOrg = new ProcessOrg(this);

            for (int i = 0; i < (int)CSTATION.STATION.MAX; i++)
            {
                CSTATION.STATION st = (CSTATION.STATION)i;

                m_station[i] = new CSTATION(st);
            }

            Automation.Instance.Initialize();
            Automation.Instance.GetMonitoringDataEvent += GetFDCData;

            m_thread = new Thread(run);
            m_frenicThread = new Thread(m_frenicRun);
        }

        private (Dictionary<INPUT, bool> Inputs,
         Dictionary<OUTPUT, bool> Outputs,
         //List<TimeSpan> TackTime) GetFDCData()
         List<long> TackTime) GetFDCData()
        {
            var inputs = new Dictionary<INPUT, bool>();
            var outputs = new Dictionary<OUTPUT, bool>();
            //var tack = new List<TimeSpan>();  //편의상 바꿈
            List<long> tack = new List<long>();

            inputs[INPUT.MOLD_LD_CV_IN] = m_input[(int)INPUT.MOLD_LD_CV_IN];
            inputs[INPUT.MOLD_LD_CV_MID] = m_input[(int)INPUT.MOLD_LD_CV_MID];
            inputs[INPUT.MOLD_LD_CV_OUT] = m_input[(int)INPUT.MOLD_LD_CV_OUT];
            inputs[INPUT.ALIGN_CV_IN] = m_input[(int)INPUT.ALIGN_CV_IN];
            inputs[INPUT.ALIGN_CV_OUT] = m_input[(int)INPUT.ALIGN_CV_OUT];
            inputs[INPUT.MOLD_ULD_CV_IN] = m_input[(int)INPUT.MOLD_ULD_CV_IN];
            inputs[INPUT.MOLD_ULD_CV_OUT] = m_input[(int)INPUT.MOLD_ULD_CV_OUT];
            inputs[INPUT.UB_ULD_CV_IN_1] = m_input[(int)INPUT.UB_ULD_CV_IN_1];
            inputs[INPUT.UB_ULD_CV_IN_2] = m_input[(int)INPUT.UB_ULD_CV_IN_2];
            inputs[INPUT.UB_ULD_CV_OUT] = m_input[(int)INPUT.UB_ULD_CV_OUT];


            inputs[INPUT.MOLD_IN_PP_GRIP] = m_input[(int)INPUT.MOLD_IN_PP_GRIP];
            inputs[INPUT.MOLD_IN_REVERSE_DETECT] = m_input[(int)INPUT.MOLD_IN_REVERSE_DETECT];
            inputs[INPUT.MOLD_SHUTTLE_STAGE_2_DETECT] = m_input[(int)INPUT.MOLD_SHUTTLE_STAGE_2_DETECT];
            inputs[INPUT.MOLD_OUT_PP_GRIP_1] = m_input[(int)INPUT.MOLD_OUT_PP_GRIP_1];
            inputs[INPUT.MOLD_OUT_PP_GRIP_2] = m_input[(int)INPUT.MOLD_OUT_PP_GRIP_2];
            inputs[INPUT.MOLD_SHUTTLE_STAGE_3_DETECT] = m_input[(int)INPUT.MOLD_SHUTTLE_STAGE_3_DETECT];
            inputs[INPUT.UB_OUT_PP_VAC] = m_input[(int)INPUT.MOLD_ULD_CV_OUT];
            inputs[INPUT.UB_OUT_PP_VAC] = m_input[(int)INPUT.UB_OUT_PP_VAC];
            inputs[INPUT.UB_OUT_REVERSE_DETECT_1] = m_input[(int)INPUT.UB_OUT_REVERSE_DETECT_1];
            inputs[INPUT.UB_OUT_REVERSE_DETECT_2] = m_input[(int)INPUT.UB_OUT_REVERSE_DETECT_2];

            tack.AddRange(tactTimeList());

            while (tack.Count < 30)
            {
                tack.Add(0);
            }

            tack.Add(getElaspedTactTime());

            return (inputs, outputs, tack);
        }
        

        public void setOutputToggle(OUTPUT _output)
        {
            bool value = output(_output);
            setOutput(_output, !value);
        }

        public void setOutput(OUTPUT output, bool value)
        {
            setOutput((int)output, value);
        }

        public void setOutputForce(OUTPUT output, bool value)
        {
            if (m_simulation == true)
            {
                m_output[(int)output] = value;
                return;
            }

            if (m_dioOut == null)
                return;

            m_output[(int)output] = value;
            m_dioOut.setOutput((int)output, value);
        }

        void setOutput(int index, bool value)
        {
            if (m_isAuto == false && m_manualTestIO == true)
                return;

            if (m_simulation == true)
            {
                m_output[index] = value;
                return;
            }

            if (m_dioOut == null)
                return;

            m_output[index] = value;
            m_dioOut.setOutput(index, value);
        }

        public bool output(OUTPUT _output)
        {
            int index = (int)_output;

            return output(index);
        }

        public bool output(int index)
        {
            if (index > m_output.Length - 1)
                return false;

            return m_output[index];
        }

        public bool input(int index)
        {
            if (index > m_input.Length - 1)
                return false;

            return m_input[index];
        }

        public bool input(INPUT _input)
        {
            int index = (int)_input;

            return input(index);
        }

        public double inputVoltage(int index)
        {
            return m_aioIn.input(index);
        }

        public void start()
        {
            m_thread.Start();
            m_frenicThread.Start();
        }

        public void stop()
        {
            m_stop = true;
        }

        public void addAlarm(int alarmIdx)
        {
            addAlarm((ALARM)alarmIdx);
        }

        public void addAlarm(ALARM alarm, ALARM_TYPE type = ALARM_TYPE.HEAVY, string desc = "", string datetime = "")
        {
            AlarmData riseAlarm = new AlarmData();

            if (datetime == "")
                datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            riseAlarm.alarm = alarm;
            riseAlarm.type = type;
            riseAlarm.code = (int)alarm;
            riseAlarm.desc = alarm.ToString() + "/" + Alarm.messageEng(alarm, desc);
            riseAlarm.datetime = datetime;

            if (type == ALARM_TYPE.HEAVY)
            {
                for (int i = 0; i < m_heavyAlarmList.Count; i++)
                {
                    AlarmData data = m_heavyAlarmList[i];

                    if (data.alarm == riseAlarm.alarm)
                        return;
                }

                lock (m_heavyAlarmList)
                {
                    Debug.debug("ProcessMain::addAlarm alarm:" + alarm + " type:" + type + " desc:" + desc);

                    setAuto(false);
                    pause();
                    m_isAlarm = true;

                    m_heavyAlarmList.Add(riseAlarm);

                    writeAlarmLog(riseAlarm);
                    setBuzzerOn();
                    writeBottomHistory(Alarm.messageEng(alarm));

                    // TODO CHECK LHJ to HJP : 경알람 추가시 처리 필요
                    Automation.Instance.AlarmOccured(CIMEnumeric.EnumAlarmLevel.HeavyAlarm, riseAlarm.code, riseAlarm.desc);
                }
            }

            if (type == ALARM_TYPE.LIGHT)
            {
                for (int i = 0; i < m_lightAlarmList.Count; i++)
                {
                    AlarmData data = m_lightAlarmList[i];

                    if (data.alarm == riseAlarm.alarm)
                        return;
                }

                lock (m_lightAlarmList)
                {
                    Debug.debug("ProcessMain::addAlarm alarm:" + alarm + " type:" + type + " desc:" + desc);

                    m_isLightAlarm = true;

                    m_lightAlarmList.Add(riseAlarm);
                    writeAlarmLog(riseAlarm);

                    setBuzzerOn();
                    writeBottomHistory(Alarm.messageEng(alarm));

                    // TODO CHECK LHJ to HJP : 경알람 추가시 처리 필요
                    Automation.Instance.AlarmOccured(CIMEnumeric.EnumAlarmLevel.LightAlarm, riseAlarm.code, riseAlarm.desc);
                }
            }
        }

        public List<AlarmData> lightAlarmList()
        {
            lock (m_lightAlarmList)
            {
                return m_lightAlarmList;
            }
        }

        public List<AlarmData> heavyAlarmList()
        {
            lock (m_heavyAlarmList)
            {
                return m_heavyAlarmList;
            }
        }

        private void setBuzzerOn() { setOutput(OUTPUT.BUZZER_1, true); }
        private void setBuzzerOff() { setOutput(OUTPUT.BUZZER_1, false); }

        public void writeAlarmLog(AlarmData alarm)
        {
            FormMain.inst().addAlarmLog(alarm);

            DateTime datetime = Convert.ToDateTime(alarm.datetime);
            string[] texts = new string[5];

            texts[0] = datetime.ToString("yyyy-MM-dd");
            texts[1] = datetime.ToString("HH:mm:ss");
            texts[2] = alarm.code.ToString();
            texts[3] = alarm.type.ToString();
            texts[4] = alarm.desc.ToString();

            string message = string.Join(",", texts);
            message = message.Replace(Environment.NewLine, " ");

            m_alarmManager.write(message);
        }

        public void writeBottomHistory(string text)
        {
            FormMain.inst().addHistory(text);
        }

        public void writeSetupLog(string text)
        {
            string timeString = "[" + DateTime.Now.ToString("HHmmss.ffff") + "]";

            string message = timeString + text;
            message = message.Replace(Environment.NewLine, " ");

            m_setupLogMgr.write(message);
        }

        public void writeMesLog(string text)
        {
            string timeString = "[" + DateTime.Now.ToString("HHmmss.ffff") + "]";

            string message = timeString + text;
            message = message.Replace(Environment.NewLine, " ");

            m_mesLogMgr.write(message);
        }


        public void axisAlarmReset()
        {
            for (int i = 0; i < m_axis.Count; i++)
            {
                ExtAxis axis = m_axis[i];

                if (axis.isAlarm())
                    axis.alarmClear();
            }
        }

        public void clearAlarm()
        {
            axisAlarmReset();

            lock (m_heavyAlarmList)
            {
                m_heavyAlarmList.Clear();
            }

            setBuzzerOff();
            setOutput(OUTPUT.OP_PANEL_RESET_SW, false);

            // Automation.Instance.AlarmReleased(this.m_lastAlarm.code, this.m_lastAlarm.desc);
            m_isLightAlarm = false;
            m_isAlarm = false;

            FormMain.inst().clearAlarm();
        }

        public int lastAlarmCode()
        {
            int code = -1;

            if (m_heavyAlarmList.Count == 0)
                return code;

            AlarmData alarm = m_heavyAlarmList.Last();
            code = alarm.code;

            return code;
        }

        public string lastAlarmDesc()
        {
            string desc = "";


            if (m_heavyAlarmList.Count == 0)
                return desc;

            AlarmData alarm = m_heavyAlarmList.Last();
            desc = alarm.desc;

            return desc;
        }

        public void run()
        {
            Debug.debug("ProcessMain::run START");

            m_procLoaderCvWork.start();
            m_procAlignCvWork.start();
            m_procInWork.start();
            m_procMoldReverseWork.start();
            m_procMoldWork.start();
            m_procShuttleWork.start();
            m_procUbWork.start();
            m_procUbReverseWork.start();
            m_procOutMoldCvWork.start();
            m_procOutUbCvWork.start();

            m_once = true;

            m_stopWatch.Start();

            while (true)
            {
                if (m_stop)
                {
                    Debug.debug("ProcessMain::run STOP");
                    break;
                }

                if (m_simulation == false)
                {
                    // motion & IO 연결 상태 체크
                    if (m_lib.isConnectDIO() == false)
                    {
                        if (m_isBoardConnectWait == false)
                        {
                            addAlarm(ALARM.CO_MAIN_SYSTEM_PC_ECAT_DISCONNECT, ALARM_TYPE.HEAVY);

                            m_lib.close();
                            m_lib.open();

                            m_isBoardConnectWait = true;
                            m_boardReconnectTimer.start();
                        }

                        if (m_boardReconnectTimer.isElasped() == true)
                        {
                            m_isBoardConnectWait = false;
                            m_boardReconnectTimer.stop();
                        }
                    }
                    else
                    {
                        m_isBoardConnectWait = false;
                        m_boardReconnectTimer.stop();
                    }

                    commDIO();
                    commAIO();
                    // TODO CHECK LHJ : 사전 테스트 끝나고 복구 필요
                    //Automation.Instance.SyncCommCCIE();
                    foreach (ExtAxis axis in m_axis)
                    {
                        axis.run();
                    }
                    m_once = false;
                }

                //Automation.Instance.PpidListRequest();
                //Automation.Instance.PpidChange();

                watchEmergency();
                watchServo();
                watchSwitch();
                watchSensor();
                watchLamp();

                runWork();

                m_scanTime = m_stopWatch.GetElapsedTime(CStopWatch.TIME_UNIT.MILLISECOND, true);

                Thread.Sleep(10);
            }

            if (m_lib != null) 
                m_lib.close();

            for (int i = 0; i < m_axis.Count; i++)
            { 
                if (m_axis[i] != null)
                    m_axis[i].close();
            }

            Debug.debug("ProcessMain::run END");
        }

        public void m_frenicRun()
        {
            Debug.debug("ProcessMain::frenicRun START");

            m_frenic = new CSerialFRENIC(FormMain.inst().serialFRENIC, 11);

            while (true)
            {
                if (m_stop)
                {
                    Debug.debug("ProcessMain::frenicRun STOP");
                    break;
                }

                if (m_simulation == false)
                {
                    watchFrenic();
                }

                Thread.Sleep(100);
            }

            if (m_frenic != null)
                m_frenic.stop();

            Debug.debug("ProcessMain::frenicRun END");
        }

        public long scanTime()
        {
            return m_scanTime;
        }


        void commDIO()
        {
            m_input = m_dioIn.allInput();

            if (m_once)
            {
                m_output = m_dioOut.allOutput();
            }

            m_dioOut.setOutput(m_output);
        }

        void commAIO()
        {
            m_aioIn.readVoltage();
        }

        public void runWork()
        {
            // READY PROCESS
            m_procOrg.run();

            // RETURN CV PROCESS

            if (m_isAuto == true) // AUTO PROCESS
            {
                for (int i = 0; i < m_axis.Count; i++)
                {
                    if (m_axis[i].name().Contains("Z"))
                    {
                        //m_axis[i].setAbsSpeed(Conf.vel((AXIS)i), Conf.acc((AXIS)i), Conf.dec((AXIS)i));
                        m_axis[i].setAbsSpeed(500.0d, Conf.acc((AXIS)i), Conf.dec((AXIS)i));
                    }
                    else
                    {
                        //m_axis[i].setAbsSpeed(Conf.vel((AXIS)i), Conf.acc((AXIS)i), Conf.dec((AXIS)i));
                        m_axis[i].setAbsSpeed(1000.0d, Conf.acc((AXIS)i), Conf.dec((AXIS)i));
                    }
                }

                // WORK PROCESS
                m_procLoaderCvWork.run();
                m_procAlignCvWork.run();
                m_procInWork.run();
                m_procMoldReverseWork.run();
                m_procMoldWork.run();
                m_procShuttleWork.run();
                m_procUbWork.run();
                m_procUbReverseWork.run();
                m_procOutMoldCvWork.run();
                m_procOutUbCvWork.run();

                // PART PROCESS
                m_procInPP.run();
                m_procMoldPP.run();
                m_procMoldBase.run();
                m_procUbPP.run();
            }
            else // MANUAL PROCESS
            {
                m_procManualInPP.run();
                m_procManualMoldPP.run();
                m_procManualUbPP.run();
            }
        }

        public void setCycleStop()
        {
            if (m_isAuto == false)
                return;
            
            m_isCycleStop = true;

            writeBottomHistory("Req Cycle Stop");
            Debug.debug("ProcessMain::setCycleStop");
        }

        public void completeCycleStop()
        {
            m_isCycleStop = false;

            writeBottomHistory("Cycle Stop Complete");

            Debug.debug("ProcessMain::completeCycleStop");
        }

        public void setAuto(bool value, ALARM alarm = ALARM.NONE)
        {
            if (m_isAuto == true && value == false)
            {
                pause();
            }

            if (value == true)
            {
                if (m_isNeedOrgin == true)
                {
                    CMessageBox msgBox = new CMessageBox(Common.TITLE, "auto run request axis origin.", MessageBoxButtons.OK);
                    msgBox.ShowDialog();
                    return;
                }

                setEmergencyFlag(false);
                setOutOfPPlan(false);
                resume();

                m_admin = false;
            }

            if (m_isAuto != value)
            {
                writeBottomHistory(m_state.ToString() + (value ? " Start" : " Stop"));

                string alarmDesc = "";

                if (alarm != ALARM.NONE)
                {
                    alarmDesc = "[" + alarm.ToString() + "]" + Alarm.messageEng(alarm, "");
                }

                writeStatusLog(m_isAuto, alarmDesc);

                Debug.debug("ProcessMain::setAuto value:" + value);
            }

            m_isAuto = value;

#if false
            Automation.Instance.EqControlMode = (this.m_isAuto ? CIMEnumeric.EnumEqControlMode.Auto : CIMEnumeric.EnumEqControlMode.Manual);
            switch (Automation.Instance.EqControlMode)
            {
                case EnumEqControlMode.Auto: 
                    Automation.Instance.SetEqState(EnumMoveState.Runnning);
                    break;
                case EnumEqControlMode.Manual: 
                    Automation.Instance.SetEqState(EnumMoveState.Pause);

                    // TODO CHECK LHJ -> HJP : EnmumEqStopByOperatorType.Teaching 추가 필요
                    Automation.Instance.EqStopByOperator(EnmumEqStopByOperatorType.Manual);
                    break;
                default: break;
            }
#endif
        }

        public void pause()
        {
            for (int i = 0; i < m_axis.Count; i++)
            {
                axis(i).stop();
            }
        }

        public void resume()
        {
            for (int i = 0; i < m_axis.Count; i++)
            {
                axis(i).resume();
            }
            Util.waitTick(50);
        }

        void watchFrenic()
        {
            for (int i = 0; i < 11; i++)
            {
                if (m_frenic.freq(i) != 60.0d)
                    m_frenic.setFrequency(i, 60.0d);
            }
        }

        void watchEmergency()
        {
            if (input(INPUT.EMERGENCY_LOOP) == false)
            {
                if (m_showEmergencyAlarm == false)
                {
                    addAlarm(ALARM.EM_MAIN_SYSTEM_PLC_EMERGENCY_OFF, ALARM_TYPE.HEAVY);
                    m_showEmergencyAlarm = true;
                }
            }
            if (input(INPUT.EMERGENCY_MAGNETIC) == false)
            {
                if (m_showMcAlarm == false)
                {
                    addAlarm(ALARM.EM_MAIN_SYSTEM_PLC_MC_OFF, ALARM_TYPE.HEAVY);
                    m_showMcAlarm = true;
                }
            }

            if (m_isAuto == false)
                return;

            m_showMcAlarm = false;
            m_showEmergencyAlarm = false;

            if (input(INPUT.DOOR_LOOP) == false)
            {
                if (input(INPUT.FRONT_LEFT_UPPER_DOOR_LOCK) == false)
                    addAlarm(ALARM.DO_MAIN_DOOR_FRONT_LEFT_OPEN);

                if (input(INPUT.FRONT_RIGHT_UPPER_DOOR_LOCK) == false)
                    addAlarm(ALARM.DO_MAIN_DOOR_FRONT_RIGHT_OPEN);

                if (input(INPUT.REAR_LEFT_UPPER_DOOR_LOCK) == false)
                    addAlarm(ALARM.DO_MAIN_DOOR_REAR_LEFT_OPEN);

                if (input(INPUT.REAR_RIGHT_UPPER_DOOR_LOCK) == false)
                    addAlarm(ALARM.DO_MAIN_DOOR_REAR_RIGHT_OPEN);
            }
        }

        void watchServo()
        {
            if (m_isAuto == true && m_servoAlarmRise == true)
                m_servoAlarmRise = false;

            for (int i = 0; i < m_axis.Count; i++)
            {
                ExtAxis axis = m_axis[i];

                if (m_isAuto == true)
                {
                    if (axis.isServoOn() == false)
                    {
                        addAlarm(ALARM.MO_UNLOADER_UBPP_AXIS_Z_SERVO_OFF + i, ALARM_TYPE.HEAVY);
                        return;
                    }

                    if (axis.isAlarm() == true)
                    {
                        string text = "name:" + axis.name() + " code:" + axis.alarmCode();
                        addAlarm(ALARM.MO_UNLOADER_UBPP_AXIS_Z_SERVO_ALARM + i, ALARM_TYPE.HEAVY, text);
                        return;
                    }
                }
                else
                {
                    if (m_servoAlarmRise == false)
                    {
                        if (axis.isAlarm() == true)
                        {
                            string text = "name:" + axis.name() + " code:" + axis.alarmCode();
                            addAlarm(ALARM.MO_UNLOADER_UBPP_AXIS_Z_SERVO_ALARM + i, ALARM_TYPE.HEAVY, text);
                            m_servoAlarmRise = true;
                        }
                    }
                }
            }
        }

        void watchSwitch()
        {
            if (input(INPUT.OP_BOX_START_SW))
            {
                if (FormMain.inst().currentShowPage() != PAGE.AUTO && FormMain.inst().currentShowPage() != PAGE.LOG)
                    return;
                if (axisOriComplete() == false)
                    return;

                if (isAuto() == false)
                {
                    setAuto(true); 
                }
            }
            else if (input(INPUT.OP_BOX_STOP_SW))
            {
                setAuto(false);
                pause();
            }
            else if (input(INPUT.OP_BOX_RESET_SW))
            {
                setOutput(OUTPUT.EMERGENCY_RESET, true);
                clearAlarm();
            }
            else
            {
                setOutput(OUTPUT.EMERGENCY_RESET, false);
            }
        }

        void watchSensor()
        {
            if (isAuto() == true)
            {
                setOutput(OUTPUT.OP_PANEL_START_SW, true);
                setOutput(OUTPUT.OP_PANEL_STOP_SW, false);
                setOutput(OUTPUT.OP_PANEL_RESET_SW, false);
            }
            else
            {
                if (isAlarm() == true)
                {
                    setOutput(OUTPUT.OP_PANEL_START_SW, false);
                    setOutput(OUTPUT.OP_PANEL_STOP_SW, true);
                    setOutput(OUTPUT.OP_PANEL_RESET_SW, true);
                }
                else
                {
                    setOutput(OUTPUT.OP_PANEL_START_SW, false);
                    setOutput(OUTPUT.OP_PANEL_STOP_SW, true);
                    setOutput(OUTPUT.OP_PANEL_RESET_SW, false);
                }
            }
        }

        private void watchLamp()
        {
            if (m_isAlarm == true)
            {
                lampControl(LAMP_STATE.RED);
                return;
            }

            if (m_isAuto == true)
            {
                lampControl(LAMP_STATE.GREEN);
            }
            else if (m_procOrg.isRun() == true)
            {
                lampControl(LAMP_STATE.GREEN);
            }
            else
            {
                lampControl(LAMP_STATE.YELLOW);
            }
        }


        public void lampControl(LAMP_STATE state)
        {
            switch (state)
            {
                case LAMP_STATE.RED:
                    setOutput(OUTPUT.TOWER_R, true);
                    setOutput(OUTPUT.TOWER_Y, false);
                    setOutput(OUTPUT.TOWER_G, false);
                    break;

                case LAMP_STATE.YELLOW:
                    setOutput(OUTPUT.TOWER_R, false);
                    setOutput(OUTPUT.TOWER_Y, true);
                    setOutput(OUTPUT.TOWER_G, false);
                    break;

                case LAMP_STATE.GREEN:
                    setOutput(OUTPUT.TOWER_R, false);
                    setOutput(OUTPUT.TOWER_Y, false);
                    setOutput(OUTPUT.TOWER_G, true);
                    break;
            }
        }

        public void addOutputCount(int cnt) { m_outputCount += cnt; }
        public void outputCountReset() { m_outputCount = 0; }
        public int outputCount() { return m_outputCount; }
        public void setInputStop(bool value) { m_inputStop = value; }
        public void setOutputStop(bool value) { m_outputStop = value; }
        public void setOutOfPPlan(bool value) { m_outOfPPlan = value; }
        public bool isInputStop() { return m_inputStop; }
        public bool isOutputStop() { return m_outputStop; }
        public bool isOutOfPPlan() { return m_outOfPPlan; }

        public void addCycleTime(long tactTime)
        {
            if (tactTime < 0)
                return;

            lock (m_tactTimeList)
            {
                if (m_tactTimeList.Count > 30)
                    m_tactTimeList.RemoveAt(0);

                m_tactTimeList.Add(tactTime);
            }

            m_isTactTimeUpdate = true;
        }

        public bool isCycleTimeUpdate() 
        { 
            return m_isTactTimeUpdate; 
        }

        public void lastCycleTimeClear()
        {
            m_isTactTimeUpdate = false;
            m_tactTimeList.Clear();
        }

        /// <summary>
        /// return at Tact Time List (max 30)
        /// </summary>
        /// <returns></returns>
        public List<long>tactTimeList()
        {
            return m_tactTimeList;
        }

        /// <summary>
        /// return at elasped Tact Time. (ms)
        /// </summary>
        /// <returns></returns>
        public long getElaspedTactTime()
        {
            return m_procMoldWork.elaspedTactTime();
        }

        /// <summary>
        /// return at last Tact Time (ms)
        /// </summary>
        /// <returns></returns>
        public double getLastCycleTime() 
        {
            if (m_tactTimeList.Count == 0)
                return 0;

            return m_tactTimeList.Last();
        }

        /// <summary>
        /// return at average Tact Time (ms)
        /// </summary>
        /// <returns></returns>
        public double getAvgCycleTime()
        {
            if (m_tactTimeList.Count == 0)
                return 0;

            return m_tactTimeList.Average();
        }

        public AjinDIO dioInput() { return m_dioIn; }
        public AjinDIO dioOutput() { return m_dioOut; }

        public ProcessLoaderCvWork procLoaderCvWork() { return m_procLoaderCvWork; }
        public ProcessAlignCvWork procAlignCvWork() {  return m_procAlignCvWork; }
        public ProcessInWork procInWork() { return m_procInWork; }
        public ProcessMoldWork procMoldWork() {  return m_procMoldWork; }
        public ProcessMoldReverseWork procMoldReverseWork() { return m_procMoldReverseWork; }
        public ProcessShuttleWork procShuttleWork() { return m_procShuttleWork; }
        public ProcessUbWork procUbWork() {  return m_procUbWork; }
        public ProcessUbReverseWork procUbReverseWork() {  return m_procUbReverseWork; }
        public ProcessOutMoldCvWork procOutMoldCvWork() { return m_procOutMoldCvWork; } 
        public ProcessOutUbCvWork procOutUbCvWork() { return m_procOutUbCvWork; }

        public ProcessManualInPP procManualInPP() { return m_procManualInPP; }
        public ProcessManualMoldPP procManualMoldPP() {  return m_procManualMoldPP; }
        public ProcessManualUbPP procManualUbPP() { return m_procManualUbPP; }


        public ProcessInPP procInPP() { return m_procInPP; }
        public ProcessMoldPP procMoldPP() { return m_procMoldPP; }
        public ProcessMoldBase procMoldBase() { return m_procMoldBase; }
        public ProcessUbPP procUbPP() { return m_procUbPP; }

        public ProcessOrg procOrg() { return m_procOrg; }
        public bool isAuto() { return m_isAuto; }
        public bool isCycleStop() { return m_isCycleStop; }
        public bool isAlarm() { return m_isAlarm; }
        public bool isLightAlarm() { return m_isLightAlarm; }
        public void setShowInitCheckMsg(bool value) { m_isShowInitCheckMsg = value; }
        public bool isShowInitCheckMsg() { return m_isShowInitCheckMsg; }
        public bool isSimulation() { return m_simulation; }

        public MACHINE_STATE state() { return m_state; }

        public void setJogSpeed(JOG_SPEED speed) { m_jogSpeed = speed; }
        public JOG_SPEED getJogSpeed() { return m_jogSpeed; }

        public bool isByPass() { return m_state == MACHINE_STATE.BYPASS; }
        public bool isDryRun() { return m_state == MACHINE_STATE.DRY_RUN; }

        public void setState(MACHINE_STATE state) { m_state = state; }

        public bool axisOriComplete()
        {
            bool retn = true;
            return retn;
        }

        public ExtAxis axis(int index)
        {
            if (m_axis.Count() < index)
                return null;

            return m_axis[index];
        }

        public ExtAxis axis(AXIS axis)
        {
            int index = (int)axis;

            return this.axis(index);
        }

        public void setAbsSpeedConf(AXIS axis)
        {
            double vel = Conf.vel(axis);
            double acc = Conf.vel(axis);
            double dec = Conf.vel(axis);

            m_axis[(int)axis].setAbsSpeed(vel, acc, dec);
        }

        //ADMIN
        public bool isAdmin()
        {
            if (File.Exists(Common.PATH + "\\admin") == true)
                return true;

            return m_admin;
        }
        public void setAdmin(bool value)
        {
            m_admin = value;
        }


        public void setEmergencyFlag(bool value)
        {
            m_emergencyFlag = value;
        }

        public void setIsNeedOrging(bool value)
        {
            m_isNeedOrgin = value;
        }

        public void writeStatusLog(bool isAuto, string alarm)
        {
            string timeString = DateTime.Now.ToString("HH:mm:ss");
            string status = isAuto ? "Start" : "Stop";

            string message = status + "," + alarm + "," + timeString;

            m_mcStatusLog.write(message);
        }

        public CSerialFRENIC frenic()
        {
            return m_frenic;
        }

        public void setManualTestIO(bool value)
        {
            m_manualTestIO = value;
        }

        public bool isWaitBoardReconnect()
        {
            return m_isBoardConnectWait;
        }

        public bool isLastWork()
        {
            return m_lastWorkMode;
        }

        public void setLastWork(bool value)
        {
            m_lastWorkMode = value;
        }

        public void setCimBit(CIMWrite.WRITE_B addr, bool value)
        {
            Automation.Instance.WriteBit(addr, value);
        }

        // TODO KGW : 입력에 대한 테스트로 추가, 테스트후 삭제
        public void setCimBit(CIMRead.READ_B addr, bool value)
        {
            Automation.Instance.WriteBit(addr, value);
        }

        public bool readCimBit(CIMWrite.WRITE_B addr)
        {
            return Automation.Instance.ReadBit(addr);
        }

        public bool readCimBit(CIMRead.READ_B addr)
        {
            return Automation.Instance.ReadBit(addr);
        }

        public string readCimWord(CIMRead.READ_W addr)
        {
            return Automation.Instance.ReadWord(addr);
        }

        public string readCimWord(CIMWrite.WRITE_W addr)
        {
            return Automation.Instance.ReadWord(addr);
        }

        public void writeCimWord(CIMWrite.WRITE_W addr, string text)
        {
            Automation.Instance.WriteWord(addr, text);
        }

        public CSTATION station(CSTATION.STATION station)
        {
            return m_station[(int)station];
        }

        public CSTATION[] station()
        { 
            return m_station; 
        }

        public enum DOOR_ID
        {
            NONE = -1,

            FR_LEFT,
            FR_RIGHT,

            RR_LEFT,
            RR_RIGHT,

            C_BOX_1_LEFT,
            C_BOX_1_RIGHT,
            C_BOX_2_LEFT,
            C_BOX_2_RIGHT,

            MAX,
        }

        public bool isDoorDetect(DOOR_ID index)
        {
            switch (index)
            {
                case DOOR_ID.FR_LEFT:
                    return !input(INPUT.FRONT_LEFT_UPPER_DOOR_SW);
                case DOOR_ID.FR_RIGHT:
                    return !input(INPUT.FRONT_RIGHT_UPPER_DOOR_SW);
                case DOOR_ID.RR_LEFT:
                    return !input(INPUT.REAR_LEFT_UPPER_DOOR_SW);
                case DOOR_ID.RR_RIGHT:
                    return !input(INPUT.REAR_RIGHT_UPPER_DOOR_SW);
                case DOOR_ID.C_BOX_1_LEFT:
                    return !input(INPUT.BOX_1_LEFT_LOWER_DOOR_SW);
                case DOOR_ID.C_BOX_1_RIGHT:
                    return !input(INPUT.BOX_1_RIGHT_LOWER_DOOR_SW);
                case DOOR_ID.C_BOX_2_LEFT:
                    return !input(INPUT.BOX_2_LEFT_LOWER_DOOR_SW);
                case DOOR_ID.C_BOX_2_RIGHT:
                    return !input(INPUT.BOX_2_RIGHT_LOWER_DOOR_SW);

                default: return false;
            }
        }

        public bool isDetectEMC()
        {
            return !input(INPUT.EMERGENCY_LOOP);
        }
        public bool isDetectSafetyMC()
        {
            return !input(INPUT.EMERGENCY_MAGNETIC);
        }
        public void setForceInterface(bool value)
        {
            m_forceCheckIF = value;
        }

        public bool isForceInterface()
        {
            return m_forceCheckIF;
        }
    }//class
}//namespace
