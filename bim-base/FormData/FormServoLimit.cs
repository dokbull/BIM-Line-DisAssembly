using bim_base.lib.Interfaces;
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
    public partial class FormServoLimit : Form, IViewCloseable
    {

        bool m_checkChange = false;

        ProcessMain main = null;
        public event Action OnCloseRequested;

        public FormServoLimit(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            OnCloseRequested?.Invoke();
        }

        private void FormServoLimit_Load(object sender, EventArgs e)
        {
            InitMotorGrid();
            LoadVelocity();

            MotorServoLimitGrid.MouseDoubleClick += MotorServoLimitGrid_DoubleClick;
        }

        private void InitMotorGrid()
        {
            CSourceGrid g = MotorServoLimitGrid;

            g.Selection.EnableMultiSelection = false;
            //g.Edit.EnableEdit = false;

            g.setRowCol((int)AXIS.MAX + 1, 3, true, true);

            g.setTextAlignment(DevAge.Drawing.ContentAlignment.MiddleCenter);

            // ==============================
            // Header
            // ==============================
            g.setHeader(new string[]
            {
                "",             // Axis 이름 자리
                "+ Limit",
                "- Limit",
            });

            g.setHeaderColor(Color.Black, Color.White);

            // ==============================
            // Row (Axis)
            // ==============================
            for (int i = 0; i < (int)AXIS.MAX; i++)
            {
                string axisName = ((AXIS)i).ToString();

                // 첫 컬럼 (Axis 이름)
                g.setValue(i + 1, 0, axisName);

                // 스타일 유지 (니 기존 스타일)
                g.setColors(i + 1, 0, Color.Black, Color.White);
            }

            // ==============================
            // 기본 값 세팅 (옵션)
            // ==============================
            for (int r = 1; r <= 9; r++)
            {
                for (int c = 1; c <= 6; c++)
                {
                    g.setValue(r, c, "0"); // 기본값
                }
            }
        }

        private void LoadVelocity()
        {
            CSourceGrid g = MotorServoLimitGrid;

            for (int i = 0; i < (int)AXIS.MAX; i++)
            {
                AXIS axis = (AXIS)i;
                string axisName = ((AXIS)i).ToString();
                int row = i + 1;

                // =========================
                // Axis 이름
                // =========================
                g.setValue(row, 0, axisName);

                // =========================
                // 값 세팅
                // =========================
                g.setValue(row, 1, Conf.posLimit(axis).ToString("0.##"));      // 로딩 속도
                g.setValue(row, 2, Conf.negLimit(axis).ToString("0.##"));      // 가속 시간
            }
        }

        private void MotorServoLimitGrid_DoubleClick(object sender, EventArgs e)
        {
            CSourceGrid g = MotorServoLimitGrid;

            int row = g.Selection.ActivePosition.Row;
            int col = g.Selection.ActivePosition.Column;

            // =========================
            // 조건: 데이터 영역만 허용
            // =========================
            if (row <= 0) return;   // 헤더 제외
            if (col <= 0) return;   // Axis 이름 제외

            object cellValue = g.cell(row, col).Value;
            string curValue = cellValue?.ToString() ?? "0";

            // =========================
            // Numpad 팝업
            // =========================
            FormNumpad dlg = new FormNumpad(curValue, true);
            DialogResult res = dlg.ShowDialog();

            if (res == DialogResult.OK)
            {
                string newText = dlg.getNewValue();
                double value = Util.toDouble(newText);

                // =========================
                // 범위 제한
                // =========================
                if (value > 1000) value = 1000;
                if (value < 0) value = 0;

                // =========================
                // 변경 체크
                // =========================
                if (curValue != value.ToString())
                    m_checkChange = true;

                // =========================
                // 값 적용
                // =========================
                g.cell(row, col).Value = value.ToString("0.##");

                // =========================
                // 변경 표시 (추천 👍)
                // =========================
                g.setColors(row, col, Color.Yellow, Color.Black);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveGridToConf();
        }

        private void SaveGridToConf()
        {
            CSourceGrid g = MotorServoLimitGrid;

            for (int i = 0; i < (int)AXIS.MAX; i++)
            {
                AXIS axis = (AXIS)i;
                int row = i + 1;

                double PLimit = Util.toDouble(g.cell(row, 1).Value?.ToString());
                double MLimit = Util.toDouble(g.cell(row, 2).Value?.ToString());

                // =========================
                // Conf 저장
                // =========================
                Conf.setPosLimit(axis, PLimit);
                Conf.setNegLimit(axis, MLimit);
                
            }
        }
    }
}
