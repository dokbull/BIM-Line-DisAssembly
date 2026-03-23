using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormAlarmList : Form
    {
        ProcessMain main = null;
        CFileManager m_fileManager = null;
        Queue<string[]> m_logQueue = new Queue<string[]>();
        List<AlarmData> lightAlarm = null;
        List<AlarmData> heavyAlarm = null;

        public FormAlarmList(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;
            lightAlarm = new List<AlarmData>();
            heavyAlarm = new List<AlarmData>();

            Init();
        }

        // 초기화
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

            dateChange();
        }

        private void calendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            dateChange();
        }

        private void dateChange()
        {
            DateTime date = DateTime.Now;

            gridDataClear();

            loadLogData(date);
        }

        private void loadLogData(DateTime datetime)
        {
            if (m_fileManager != null)
            {
                m_fileManager = null;
            }

            m_fileManager = new CFileManager(Common.LOG_PATH + "\\alarm\\" + datetime.ToString("yyyyMMdd") + ".log");

            if (lightAlarm == null || lightAlarm.Count == 0)
            {
                return;
            }

            for (int i = 0; i < lightAlarm.Count; i++)
            {
                int num = (int)lightAlarm[0].alarm;
                string date = lightAlarm[1].datetime;
                int code = lightAlarm[3].code;
                ALARM_TYPE type = lightAlarm[4].type;
                string text = lightAlarm[5].desc;

                addLog(num, date, code, type, text, false);
            }
        }

        public void addLog(int num, string date, int code, ALARM_TYPE type, string text, bool useQueue = true)
        {
            string[] texts = new string[3];

            texts[0] = num.ToString();
            texts[1] = date;
            texts[3] = code.ToString("0000");
            texts[5] = text;

            if (useQueue)
                m_logQueue.Enqueue(texts);
            else
                addGridRowData(texts);
        }

        private void addGridRowData(string[] texts)
        {
            int row = 1;
            int colCount = alarmListGrid.ColumnsCount;
            alarmListGrid.Rows.Insert(row);

            for (int i = 0; i < colCount; i++)
            {
                CCell cell = new CCell();
                CViewCell viewCell = new CViewCell();

                alarmListGrid[row, i] = cell;
                alarmListGrid[row, i].View = viewCell;
            }

            alarmListGrid[row, 0].Value = texts[0];
            alarmListGrid[row, 1].Value = texts[1];
            alarmListGrid[row, 2].Value = texts[2];

            alarmListGrid.setTextAlignment(row, 0, DevAge.Drawing.ContentAlignment.MiddleCenter);
            alarmListGrid.setTextAlignment(row, 1, DevAge.Drawing.ContentAlignment.MiddleCenter);

            int code = Util.toInt32(texts[1]);
        }

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

        // Current Alarm Button Click
        private void currentAlarmButton_Click(object sender, EventArgs e)
        {
            gridDataClear();

            // 오늘 날짜의 알람 로그 파일 읽기
            DateTime today = DateTime.Now;
            string logPath = Common.LOG_PATH + "\\alarm\\" + today.ToString("yyyyMMdd") + ".log";
            CFileManager fileMgr = new CFileManager(logPath);

            List<string[]> lines = fileMgr.readAll(',');
            if (lines == null || lines.Count == 0)
                return;

            for (int i = 0; i < lines.Count; i++)
            {
                string[] split = lines[i];
                if (split.Length < 5)
                    continue;

                int row = alarmListGrid.RowsCount;
                int colCount = alarmListGrid.ColumnsCount;
                alarmListGrid.addRow(1);

                if (split[3].Trim() == "LIGHT")
                {
                    // No
                    alarmListGrid.setValue(row, 0, (i + 1).ToString());
                    // Occurrence Time
                    alarmListGrid.setValue(row, 1, split[0] + " " + split[1]);
                    // I/O
                    alarmListGrid.setValue(row, 2, split[2].Trim());
                    // Code
                    alarmListGrid.setValue(row, 3, split[2].Trim());
                    // Level
                    alarmListGrid.setValue(row, 4, split[3].Trim());
                    // Message
                    alarmListGrid.setValue(row, 5, split[4].Trim());

                    alarmListGrid.setTextAlignment(row, 0, DevAge.Drawing.ContentAlignment.MiddleCenter);
                    alarmListGrid.setTextAlignment(row, 1, DevAge.Drawing.ContentAlignment.MiddleCenter);
                    alarmListGrid.setTextAlignment(row, 2, DevAge.Drawing.ContentAlignment.MiddleCenter);
                    alarmListGrid.setTextAlignment(row, 3, DevAge.Drawing.ContentAlignment.MiddleCenter);
                    alarmListGrid.setTextAlignment(row, 4, DevAge.Drawing.ContentAlignment.MiddleCenter);
                    alarmListGrid.setTextAlignment(row, 5, DevAge.Drawing.ContentAlignment.MiddleLeft);
                }

                else if (split[3].Trim() == "HEAVY")
                {
                    // No
                    alarmListGrid.setValue(row, 0, (i + 1).ToString());
                    // Occurrence Time
                    alarmListGrid.setValue(row, 1, split[0] + " " + split[1]);
                    // I/O
                    alarmListGrid.setValue(row, 2, split[2].Trim());
                    // Code
                    alarmListGrid.setValue(row, 3, split[2].Trim());
                    // Level
                    alarmListGrid.setValue(row, 4, split[3].Trim());
                    // Message
                    alarmListGrid.setValue(row, 5, split[4].Trim());

                    alarmListGrid.setTextAlignment(row, 0, DevAge.Drawing.ContentAlignment.MiddleCenter);
                    alarmListGrid.setTextAlignment(row, 1, DevAge.Drawing.ContentAlignment.MiddleCenter);
                    alarmListGrid.setTextAlignment(row, 2, DevAge.Drawing.ContentAlignment.MiddleCenter);
                    alarmListGrid.setTextAlignment(row, 3, DevAge.Drawing.ContentAlignment.MiddleCenter);
                    alarmListGrid.setTextAlignment(row, 4, DevAge.Drawing.ContentAlignment.MiddleCenter);
                    alarmListGrid.setTextAlignment(row, 5, DevAge.Drawing.ContentAlignment.MiddleLeft);
                }

                // Message 열 너비를 최대 desc 길이에 맞춤
                autoFitMessageColumn(lines);
            }
        }

        // Message 열 너비를 최대 desc 길이에 맞춤
        private void autoFitMessageColumn(List<string[]> lines)
        {
            int maxWidth = 300;

            using (Graphics g = alarmListGrid.CreateGraphics())
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Length < 5) continue;

                    string desc = lines[i][4].Trim();
                    int textWidth = (int)g.MeasureString(desc, alarmListGrid.Font).Width + 20;

                    if (textWidth > maxWidth)
                        maxWidth = textWidth;
                }
            }

            alarmListGrid.Columns[5].Width = maxWidth;
        }

        // Heavy Alarm Button Click
        private void heavyAlarmButton_Click(object sender, EventArgs e)
        {

        }

        // Light Alarm Button Click
        private void lightAlarmButton_Click(object sender, EventArgs e)
        {

        }

        // Alarm Reset Button Click
        private void alarmResetButton_Click(object sender, EventArgs e)
        {

        }
    }
}
