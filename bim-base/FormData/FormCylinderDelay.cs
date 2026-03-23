using System;
using System.Drawing;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormCylinderDelay : Form
    {
        ProcessMain main = null;

        int m_pageSize = 10;
        int m_currentPage = 0;
        int m_totalPage = 0;

        string[] m_names = null;
        CYLINDER_DELAY[] m_values = null;
        int[] m_delayData = null;

        public FormCylinderDelay(ProcessMain main)
        {
            InitializeComponent();
            this.main = main;

            m_names = Enum.GetNames(typeof(CYLINDER_DELAY));
            m_values = (CYLINDER_DELAY[])Enum.GetValues(typeof(CYLINDER_DELAY));
            m_delayData = new int[m_names.Length];

            m_totalPage = (m_names.Length - 1) / m_pageSize;

            gridInit();
            load();
            refreshGrid();
        }

        // Grid Initialization
        private void gridInit()
        {
            CSourceGrid grid = CylinderListGrid;

            grid.Selection.EnableMultiSelection = false;
            grid.setRowCol(m_pageSize, 2, true, false);
            grid.setTextAlignment(DevAge.Drawing.ContentAlignment.MiddleCenter);
            grid.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);

            // 열 너비 비율 설정
            grid.Columns[0].Width = grid.Width * 65 / 100;
            grid.Columns[1].Width = grid.Width * 35 / 100;

            // 모든 셀에 클릭 컨트롤러 등록
            for (int i = 0; i < m_pageSize; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    var controller = new CellClickController();
                    controller.CellClicked += OnCylinderValue_Click;
                    grid.cell(i, j).AddController(controller);
                }
            }
        }

        // Grid Refresh
        private void refreshGrid()
        {
            CSourceGrid grid = CylinderListGrid;
            int startIdx = m_currentPage * m_pageSize;

            for (int i = 0; i < m_pageSize; i++)
            {
                int dataIdx = startIdx + i;

                if (dataIdx < m_names.Length)
                {
                    grid.setValue(i, 0, m_names[dataIdx].Replace("_", " "));
                    grid.setColors(i, 0, Color.White, Color.Black);
                    grid.setValue(i, 1, m_delayData[dataIdx].ToString() + " Sec" );
                    grid.setColors(i, 1, Color.White, Color.Black);
                }
                else
                {
                    grid.setValue(i, 0, "");
                    grid.setBackColor(i, 0, Color.White);
                    grid.setValue(i, 1, "");
                    grid.setBackColor(i, 1, Color.White);
                }
            }

            // 페이지 이동 버튼 활성, 비활성
            PreviouslyButton.Enabled = m_currentPage > 0;
            NextButton.Enabled = m_currentPage < m_totalPage;
        }

        // Cylinder Value Click Event
        private void OnCylinderValue_Click(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (col != 1) return;

            int dataIdx = m_currentPage * m_pageSize + row;
            if (dataIdx >= m_names.Length) return;

            // Numpad 팝업
            string curValue = m_delayData[dataIdx].ToString();
            FormNumpad dlg = new FormNumpad(curValue, false);
            DialogResult res = dlg.ShowDialog();

            if (res == DialogResult.OK)
            {
                int value = Util.toInt32(dlg.getNewValue());
                m_delayData[dataIdx] = value;
                CylinderListGrid.setValue(row, 1, value.ToString() + " Sec");
            }
        }

        // Next Button Click
        private void NextButton_Click(object sender, EventArgs e)
        {
            if (m_currentPage < m_totalPage)
            {
                m_currentPage++;
                refreshGrid();
            }
        }

        // Previously Button Click
        private void PreviouslyButton_Click(object sender, EventArgs e)
        {
            if (m_currentPage > 0)
            {
                m_currentPage--;
                refreshGrid();
            }
        }

        // Exit Button Click
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Save Button Click
        private void saveButton_Click(object sender, EventArgs e)
        {
            save();
            main.writeBottomHistory("Cylinder Delay parameter change.");
            CMessageBox.showInfo(MessageText.saveMessage);
        }

        // Save
        private void save()
        {
            for (int i = 0; i < m_values.Length; i++)
            {
                Conf.setDelayTime(m_values[i], m_delayData[i]);
            }
        }

        // Load
        private void load()
        {
            for (int i = 0; i < m_values.Length; i++)
            {
                m_delayData[i] = Conf.delayTime(m_values[i]);
            }
        }
    }
}
