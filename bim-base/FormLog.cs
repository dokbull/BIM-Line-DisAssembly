using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormLog : Form
    {
        ProcessMain main = null;

        CFileManager m_fileManager = null;

        Dictionary<int, int> m_errCntDic = new Dictionary<int, int>();
        Queue<string[]> m_logQueue = new Queue<string[]>();

        public FormLog(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;

            init();
        }

        private void init()
        {
            grid.setRowCol(1, 3);
            string[] header = new string[] { "Time", "Code", "Alarm Name" };
            grid.setHeader(header);

            grid.setHeaderColor(Color.Black, Color.White);
            grid.FixedRows = 1;

            int w = grid.Width - 2;
            grid.Columns[0].Width = 80; w -= 80;
            grid.Columns[1].Width = 60; w -= 60;
            grid.Columns[2].Width = w;

            grid.setTextAlignment(0, 0, DevAge.Drawing.ContentAlignment.MiddleCenter);
            grid.setTextAlignment(0, 1, DevAge.Drawing.ContentAlignment.MiddleCenter);
            grid.setTextAlignment(0, 2, DevAge.Drawing.ContentAlignment.MiddleCenter);

            countGrid.setRowCol(1, 3);
            string[] errorCountHeader = new string[] { "Count", "Error Num", "Contents" };
            countGrid.setHeader(errorCountHeader);

            countGrid.setHeaderColor(Color.Black, Color.White);
            countGrid.FixedRows = 1;

            w = countGrid.Width - 2;
            countGrid.Columns[0].Width = 60; w -= 60;
            countGrid.Columns[1].Width = 80; w -= 80;
            countGrid.Columns[2].Width = w;

            countGrid.setTextAlignment(0, 0, DevAge.Drawing.ContentAlignment.MiddleCenter);
            countGrid.setTextAlignment(0, 1, DevAge.Drawing.ContentAlignment.MiddleCenter);
            countGrid.setTextAlignment(0, 2, DevAge.Drawing.ContentAlignment.MiddleCenter);

            dateChange();
        }

        private void calendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            dateChange();
        }

        private void dateChange()
        {
            DateTime datetime = calendar.SelectionStart;
            updateSelectDateLabel(datetime);
            gridDataClear();

            loadLogData(datetime);
            errorCountGridUpdate();
        }

        private void updateSelectDateLabel(DateTime datetime)
        {
            selectedDateLabel.Text = datetime.ToString("yyyy-MM-dd");
        }

        private void loadLogData(DateTime datetime)
        {
            if (m_fileManager != null)
                m_fileManager = null;
            m_fileManager = new CFileManager(Common.LOG_PATH + "\\alarm\\" + datetime.ToString("yyyyMMdd") + ".log");

            List<string> alarmLogList = m_fileManager.readAll(true);

            if (alarmLogList == null || alarmLogList.Count == 0)
                return;

            //for (int i = alarmLogList.Count - 1; i >= 0; i--)
            for (int i = 0; i < alarmLogList.Count; i++)
            {
                string[] splitText = alarmLogList[i].Split(',');

                if (splitText.Length < 5)
                    continue;

                string date = splitText[0];
                string time = splitText[1];
                int code = Util.toInt32(splitText[2]);
                int type = Util.toInt32(splitText[3]);
                string text = splitText[4];

                addLog(date, time, code, type, text, false);
            }
        }

        private void gridDataClear()
        {
            int rowCount = grid.RowsCount;
            int colCount = grid.ColumnsCount;

            for (int i = rowCount - 1; i > 0; i--)
            {
                for (int j = 0; j < colCount; j++)
                    grid[i, j].Value = null;

                grid.RowsCount -= 1;
            }

            errorCountGridClear();

            m_errCntDic.Clear();
        }

        private void errorCountGridClear()
        {
            int rowCount = countGrid.RowsCount;
            int colCount = countGrid.ColumnsCount;

            for (int i = rowCount - 1; i > 0; i--)
            {
                for (int j = 0; j < colCount; j++)
                    countGrid[i, j].Value = null;

                countGrid.RowsCount -= 1;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (m_logQueue.Count > 0)
            {
                string[] texts = m_logQueue.Dequeue();

                if (texts == null || texts.Length != grid.ColumnsCount)
                    return;

                if (calendar.SelectionStart.Date != DateTime.Now.Date)
                    return;

                errorCountGridClear();
                addGridRowData(texts);
                errorCountGridUpdate();
            }
        }

        public void addLog(ALARM code, ALARM_TYPE type, string text)
        {
            DateTime datetime = DateTime.Now;
            addLog(datetime.ToString("yyyy-MM-dd"), datetime.ToString("HH:mm:ss"), (int)code, (int)type, text);
        }

        public void addLog(AlarmData data)
        {
            DateTime dt = Convert.ToDateTime(data.datetime);
            addLog(dt.ToString("yyyy-MM-dd"), dt.ToString("HH:mm:ss"), data.code, data.type, data.desc);
        }

        public void addLog(string date, string time, int code, int type, string text, bool useQueue = true)
        {
            string[] texts = new string[3];

            texts[0] = time;
            texts[1] = code.ToString("0000");
            texts[2] = text;

            if (useQueue)
                m_logQueue.Enqueue(texts);
            else
                addGridRowData(texts);
        }

        private void addGridRowData(string[] texts)
        {
            int row = 1;
            int colCount = grid.ColumnsCount;
            grid.Rows.Insert(row);

            for (int i = 0; i < colCount; i++)
            {
                CCell cell = new CCell();
                CViewCell viewCell = new CViewCell();

                grid[row, i] = cell;
                grid[row, i].View = viewCell;
            }

            grid[row, 0].Value = texts[0];
            grid[row, 1].Value = texts[1];
            grid[row, 2].Value = texts[2];

            grid.setTextAlignment(row, 0, DevAge.Drawing.ContentAlignment.MiddleCenter);
            grid.setTextAlignment(row, 1, DevAge.Drawing.ContentAlignment.MiddleCenter);

            int code = Util.toInt32(texts[1]);

            if (m_errCntDic.ContainsKey(code))
                m_errCntDic[code] = m_errCntDic[code] + 1;
            else
                m_errCntDic.Add(code, 1);
        }

        private void todayButton_Click(object sender, EventArgs e)
        {
            Debug.debug("FormLog::todayButton_Click");
            calendar.SetDate(DateTime.Now);
        }

        private void errorCountGridUpdate()
        {
            IOrderedEnumerable<KeyValuePair<int, int>> sortDic = m_errCntDic.OrderByDescending(pair => pair.Value);

            foreach (KeyValuePair<int, int> pair in sortDic)
            {
                int row = countGrid.RowsCount;
                countGrid.addRow(1);

                countGrid[row, 0].Value = pair.Value;
                countGrid[row, 1].Value = pair.Key.ToString("0000");
                countGrid[row, 2].Value = Alarm.messageEng((ALARM)pair.Key);

                countGrid.setTextAlignment(row, 0, DevAge.Drawing.ContentAlignment.MiddleCenter);
                countGrid.setTextAlignment(row, 1, DevAge.Drawing.ContentAlignment.MiddleCenter);
            }
        }

        private void clearAlarmButton_Click(object sender, EventArgs e)
        {
            Debug.debug("FormLog::clearAlarmButton_Click");
            gridDataClear();
        }

        private void readAlarmButton_Click(object sender, EventArgs e)
        {
            Debug.debug("FormLog::readAlarmButton_Click");
            DateTime start = startDateTimePicker.Value.Date;
            DateTime end = endDateTimePicker.Value.Date;

            if (start > end)
            {
                CMessageBox msgBox = new CMessageBox(Common.TITLE, "시작 시간이 잘못되었습니다.", MessageBoxButtons.OK);
                msgBox.ShowDialog();
                return;
            }

            gridDataClear();

            for (DateTime date = start; date <= end; date = date.AddDays(1))
            {
                loadLogData(date);
            }
            errorCountGridUpdate();

            Invalidate();
        }

        private void FormLog_VisibleChanged(object sender, EventArgs e)
        {
            Form f = (Form)sender;
            if (f.Visible == false)
                return;

            DateTime selectDate = calendar.SelectionStart.Date;
            DateTime nowDate = DateTime.Now.Date;

            if (selectDate != nowDate)
            {
                calendar.SelectionStart = nowDate;
            }
        }
    }
}
