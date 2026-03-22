using System;
using System.Drawing;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormVacuumDelay : Form
    {
        ProcessMain main = null;

        const int PAGE_SIZE = 10;
        int m_currentPage = 0;
        int m_totalPage = 0;

        string[] m_names = null;
        VACUUM_DELAY[] m_values = null;
        int[] m_delayData = null;

        public FormVacuumDelay(ProcessMain main)
        {
            InitializeComponent();
            this.main = main;

            m_names = Enum.GetNames(typeof(VACUUM_DELAY));
            m_values = (VACUUM_DELAY[])Enum.GetValues(typeof(VACUUM_DELAY));
            m_delayData = new int[m_names.Length];

            m_totalPage = (m_names.Length - 1) / PAGE_SIZE;

            // 페이지 이동 버튼 이벤트 등록
            NextButton.Click += NextButton_Click;
            PreviouslyButton.Click += PreviouslyButton_Click;

            gridInit();         // 그리드 초기화
            load();             // 저장된 딜레이 값 로드
            refreshGrid();      // 그리드에 데이터 표시
        }

        // Grid Initialization
        private void gridInit()
        {
            CSourceGrid grid = VacuumListGrid;

            grid.Selection.EnableMultiSelection = false;
            grid.setRowCol(PAGE_SIZE, 2, true, false);
            grid.setTextAlignment(DevAge.Drawing.ContentAlignment.MiddleCenter);
            grid.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);

            // 열 너비 비율 설정
            grid.Columns[0].Width = grid.Width * 65 / 100;
            grid.Columns[1].Width = grid.Width * 35 / 100;

            // 모든 셀에 클릭 컨트롤러 등록
            for (int r = 0; r < PAGE_SIZE; r++)
            {
                for (int c = 0; c < 2; c++)
                {
                    var controller = new CellClickController();
                    controller.CellClicked += OnVacuumValue_Click;
                    grid.cell(r, c).AddController(controller);
                }
            }
        }

        // Grid Refresh
        private void refreshGrid()
        {
            CSourceGrid grid = VacuumListGrid;
            int startIdx = m_currentPage * PAGE_SIZE;   // 현재 페이지의 시작 데이터 인덱스

            for (int r = 0; r < PAGE_SIZE; r++)
            {
                int dataIdx = startIdx + r;

                if (dataIdx < m_names.Length)
                {
                    grid.setValue(r, 0, m_names[dataIdx].Replace("_", " "));
                    grid.setColors(r, 0, Color.White, Color.Black);
                    grid.setValue(r, 1, m_delayData[dataIdx].ToString());
                    grid.setColors(r, 1, Color.White, Color.Black);
                }
                else
                {
                    grid.setValue(r, 0, "");
                    grid.setBackColor(r, 0, Color.LightGray);
                    grid.setValue(r, 1, "");
                    grid.setBackColor(r, 1, Color.LightGray);
                }
            }

            // 페이지 이동 버튼 활성, 비활성
            PreviouslyButton.Enabled = m_currentPage > 0;
            NextButton.Enabled = m_currentPage < m_totalPage;
        }

        // Vacuum Value Click Event
        private void OnVacuumValue_Click(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (col != 1) return;

            int dataIdx = m_currentPage * PAGE_SIZE + row;
            if (dataIdx >= m_names.Length) return;

            // Numpad 팝업
            string curValue = m_delayData[dataIdx].ToString();
            FormNumpad dlg = new FormNumpad(curValue, false);
            DialogResult res = dlg.ShowDialog();

            if (res == DialogResult.OK)
            {
                int value = Util.toInt32(dlg.getNewValue());
                m_delayData[dataIdx] = value;
                VacuumListGrid.setValue(row, 1, value.ToString());
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

        // Save Button Click
        private void saveButton_Click(object sender, EventArgs e)
        {
            save();
            main.writeBottomHistory("Vacuum Delay parameter change.");
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

        // Exit Button Click
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
