using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormAlarmList : Form
    {
        ProcessMain main = null;
        CFileManager m_logFile = null;
        List <AlarmData> m_lightAlarm = null;
        List<AlarmData> m_heavyAlarm = null;

        public FormAlarmList(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;
            m_lightAlarm = new List<AlarmData>();
            m_heavyAlarm = new List<AlarmData>();

            Init();
        }

        // 그리드 헤더 및 열 너비 초기화
        private void Init()
        {
            alarmListGrid.setRowCol(1, 6);
            string[] header = new string[] { "No", "Occurrence Time", "I/O", "Code", "Level", "Message" };
            alarmListGrid.setHeader(header);

            alarmListGrid.setHeaderColor(Color.LightSkyBlue, Color.White);
            alarmListGrid.FixedRows = 1;

            int totalWidth = alarmListGrid.Width - 2;

            alarmListGrid.Columns[0].Width = 50; totalWidth -= 50;
            alarmListGrid.Columns[1].Width = 150; totalWidth -= 150;
            alarmListGrid.Columns[2].Width = 50; totalWidth -= 50;
            alarmListGrid.Columns[3].Width = 50; totalWidth -= 50;
            alarmListGrid.Columns[4].Width = 70; totalWidth -= 70;
            alarmListGrid.Columns[5].Width = totalWidth;

            alarmListGrid.setTextAlignment(0, 0, DevAge.Drawing.ContentAlignment.MiddleCenter);
            alarmListGrid.setTextAlignment(0, 1, DevAge.Drawing.ContentAlignment.MiddleCenter);
            alarmListGrid.setTextAlignment(0, 2, DevAge.Drawing.ContentAlignment.MiddleCenter);
            alarmListGrid.setTextAlignment(0, 3, DevAge.Drawing.ContentAlignment.MiddleCenter);
            alarmListGrid.setTextAlignment(0, 4, DevAge.Drawing.ContentAlignment.MiddleCenter);
            alarmListGrid.setTextAlignment(0, 5, DevAge.Drawing.ContentAlignment.MiddleLeft);
        }

        // 알람 로그 파일에서 AlarmData 불러오기
        private void loadLogData()
        {
            m_lightAlarm.Clear();
            m_heavyAlarm.Clear();

            string logPath = Common.LOG_PATH + "\\alarm\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";
            m_logFile = new CFileManager(logPath);
            List<string> lines = m_logFile.readAll(true);

            if (lines == null || lines.Count == 0)
                return;

            for (int i = 0; i < lines.Count; i++)
            {
                string[] s = lines[i].Split(',');
                if (s.Length < 5) continue;

                AlarmData data = new AlarmData();
                data.datetime = s[0].Trim() + " " + s[1].Trim();
                data.code = Util.toInt32(s[2].Trim());
                data.alarm = (ALARM)data.code;
                data.desc = s[4].Trim();

                string level = s[3].Trim();
                if (level == ALARM_TYPE.LIGHT.ToString())
                {
                    data.type = ALARM_TYPE.LIGHT;
                    m_lightAlarm.Add(data);
                }
                else
                {
                    data.type = ALARM_TYPE.HEAVY;
                    m_heavyAlarm.Add(data);
                }
            }
        }

        // 알람 데이터를 그리드에 행 단위로 표시
        private void displayAlarmRows(List<AlarmData> alarms)
        {
            for (int i = 0; i < alarms.Count; i++)
            {
                AlarmData data = alarms[i];
                int row = alarmListGrid.RowsCount;
                alarmListGrid.addRow(1);

                alarmListGrid.setValue(row, 0, (i + 1).ToString());
                alarmListGrid.setValue(row, 1, data.datetime);
                alarmListGrid.setValue(row, 2, ((int)data.alarm).ToString());
                alarmListGrid.setValue(row, 3, data.code.ToString("0000"));
                alarmListGrid.setValue(row, 4, data.type.ToString());
                alarmListGrid.setValue(row, 5, data.desc);

                alarmListGrid.setTextAlignment(row, 0, DevAge.Drawing.ContentAlignment.MiddleCenter);
                alarmListGrid.setTextAlignment(row, 1, DevAge.Drawing.ContentAlignment.MiddleCenter);
                alarmListGrid.setTextAlignment(row, 2, DevAge.Drawing.ContentAlignment.MiddleCenter);
                alarmListGrid.setTextAlignment(row, 3, DevAge.Drawing.ContentAlignment.MiddleCenter);
                alarmListGrid.setTextAlignment(row, 4, DevAge.Drawing.ContentAlignment.MiddleCenter);
                alarmListGrid.setTextAlignment(row, 5, DevAge.Drawing.ContentAlignment.MiddleLeft);
            }

            descMaximumLength(alarms);
        }

        // 최대 desc 길이에 맞춤
        private void descMaximumLength(List<AlarmData> alarms)
        {
            int maxWidth = 300;

            using (Graphics g = alarmListGrid.CreateGraphics())
            {
                for (int i = 0; i < alarms.Count; i++)
                {
                    int textWidth = (int)g.MeasureString(alarms[i].desc, alarmListGrid.Font).Width + 20;
                    if (textWidth > maxWidth)
                        maxWidth = textWidth;
                }
            }

            alarmListGrid.Columns[5].Width = maxWidth;
        }

        // 그리드 데이터 클리어
        private void gridDataClear()
        {
            int rowCount = alarmListGrid.RowsCount;
            int colCount = alarmListGrid.ColumnsCount;

            for (int i = rowCount - 1; i > 0; i--)
            {
                for (int j = 0; j < colCount; j++)
                    alarmListGrid[i, j].Value = null;

                alarmListGrid.RowsCount -= 1;
            }
        }

        // 전체 알람 표시
        private void currentAlarmButton_Click(object sender, EventArgs e)
        {
            gridDataClear();
            loadLogData();

            List<AlarmData> all = new List<AlarmData>();
            all.AddRange(m_lightAlarm);
            all.AddRange(m_heavyAlarm);

            displayAlarmRows(all);
        }

        // Heavy 알람만 표시
        private void heavyAlarmButton_Click(object sender, EventArgs e)
        {
            gridDataClear();
            loadLogData();
            displayAlarmRows(m_heavyAlarm);
        }

        // Light 알람만 표시
        private void lightAlarmButton_Click(object sender, EventArgs e)
        {
            gridDataClear();
            loadLogData();
            displayAlarmRows(m_lightAlarm);
        }

        // 알람 리셋
        private void alarmResetButton_Click(object sender, EventArgs e)
        {
            main.clearAlarm();
            m_lightAlarm.Clear();
            m_heavyAlarm.Clear();
            gridDataClear();
        }
    }
}
