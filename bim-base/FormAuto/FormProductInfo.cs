using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormProductInfo : Form
    {
        ProcessMain main = null;
        CFileManager m_fileManager = null;

        Queue<string[]> m_dataQueue = new Queue<string[]>();
        bool m_isDay = true;

        public FormProductInfo(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;
            init();
        }

        private void init()
        {
            DateTime now = DateTime.Now;
            if (now.Hour >= 8 && now.Hour < 20)
                m_isDay = true;
            else
                m_isDay = false;

            grid.setRowCol(1, 6);
            string[] header = new string[] { "Time", "#1 Output", "#2 Output", "#1 NG", "#2 NG", "Total Output" };
            grid.setHeader(header);

            grid.setHeaderColor(Color.Black, Color.White);
            grid.FixedRows = 1;

            int w = grid.Width - 2; //565
            grid.Columns[0].Width = 103; w -= 103;
            grid.Columns[1].Width = 90; w -= 90;
            grid.Columns[2].Width = 90; w -= 90;
            grid.Columns[3].Width = 90; w -= 90;
            grid.Columns[4].Width = 90; w -= 90;
            grid.Columns[5].Width = w;

            for (int i = 0; i < grid.ColumnsCount; i++)
                grid.setTextAlignment(0, i, DevAge.Drawing.ContentAlignment.MiddleCenter);

            if (now.Hour < 8)
                calendar.SetDate(now.AddDays(-1));
            else
                calendar.SetDate(now);
        }

        private void FormSubProductInfo_Load(object sender, EventArgs e)
        {
            ui_Timer.Enabled = true;
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            Conf.PRODUCT_INFO_VISIBLE = true;
        }

        private void hideButton_Click(object sender, EventArgs e)
        {
            Conf.PRODUCT_INFO_VISIBLE = false;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
        }

        private void uiTimer_Tick(object sender, EventArgs e)
        {
            if (main == null)
                return;

            showButton.BackColor = (Conf.PRODUCT_INFO_VISIBLE == true) ? Color.Lime : Color.White;
            hideButton.BackColor = (Conf.PRODUCT_INFO_VISIBLE == false) ? Color.Lime : Color.White;

            dayButton.BackColor = m_isDay ? Color.Lime : Color.White;
            nightButton.BackColor = (m_isDay == false) ? Color.Lime : Color.White;

            if (m_dataQueue.Count > 0)
            {
                string[] texts = m_dataQueue.Dequeue();

                if (texts == null || texts.Length != grid.ColumnsCount)
                    return;

                if (calendar.SelectionStart.Date != DateTime.Now.Date)
                    return;

                addGridRowData(texts);
            }
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

            loadData(datetime);
        }

        private void updateSelectDateLabel(DateTime datetime)
        {
            selectedDateLabel.Text = datetime.ToString("yyyy-MM-dd");
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
        }

        private void loadData(DateTime datetime)
        {
            if (m_fileManager != null)
                m_fileManager = null;

            m_fileManager = new CFileManager(Common.PRODUCT_PATH + "log_" + datetime.ToString("yyyyMMdd") + (m_isDay ? "_1" : "_2") + ".log");
            List<string> dataList = m_fileManager.readAll(true);

            if (dataList == null || dataList.Count == 0)
                return;

            for (int i = 0; i < dataList.Count; i++)
            {
                string[] splitText = dataList[i].Split(',');

                if (splitText.Length < 7)
                    continue;

                string startTime = splitText[0];
                string endTime = splitText[1];
                string time = startTime + "~" + endTime;
                int slot1 = Util.toInt32(splitText[2]);
                int slot2 = Util.toInt32(splitText[3]);
                int slot3 = Util.toInt32(splitText[4]);
                int slot4 = Util.toInt32(splitText[5]);
                int total = Util.toInt32(splitText[6]);

                addLog(time, slot1, slot2, slot3, slot4, total, false);
            }
        }

        public void addLog(string time, int slot1, int slot2, int slot3, int slot4, int total, bool useQueue = true)
        {
            string[] texts = new string[6];

            texts[0] = time;
            texts[1] = slot1.ToString();
            texts[2] = slot2.ToString();
            texts[3] = slot3.ToString();
            texts[4] = slot4.ToString();
            texts[5] = total.ToString();

            if (useQueue)
                m_dataQueue.Enqueue(texts);
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

                grid[row, i].Value = texts[i];

                grid.setTextAlignment(row, i, DevAge.Drawing.ContentAlignment.MiddleCenter);
            }
        }

        private void dayButton_Click(object sender, EventArgs e)
        {
            if (m_isDay)
                return;
            m_isDay = true;
            dateChange();
        }

        private void nightButton_Click(object sender, EventArgs e)
        {
            if (m_isDay == false)
                return;
            m_isDay = false;
            dateChange();
        }
    }
}
