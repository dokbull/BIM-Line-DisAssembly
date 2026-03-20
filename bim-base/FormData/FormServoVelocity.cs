using bim_base.lib.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormServoVelocity : Form, IViewCloseable
    {
        bool m_checkChange = false;

        ProcessMain main = null;
        public event Action OnCloseRequested;
        public FormServoVelocity(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;
        }

        private void btnServoVelocityExit_Click(object sender, EventArgs e)
        {
            OnCloseRequested?.Invoke();
        }

        private void FormServoVelocity_Load(object sender, EventArgs e)
        {
            InitMotorGrid();
            LoadVelocity();
        }

        private void InitMotorGrid()
        {
            CSourceGrid g = MotorVelGrid;

            g.Selection.EnableMultiSelection = false;

            g.setRowCol((int)AXIS.MAX + 1, 7, true, true);

            g.setTextAlignment(DevAge.Drawing.ContentAlignment.MiddleCenter);

            // ==============================
            // Header
            // ==============================
            g.setHeader(new string[]
            {
                "",             // Axis 이름 자리
                "로딩 속도",
                "가속 시간",
                "감속 시간",
                "로드 저속",
                "로드 중속",
                "로드 고속"
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
            CSourceGrid g = MotorVelGrid;

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
                g.setValue(row, 1, Conf.vel(axis).ToString("0.##"));      // 로딩 속도
                g.setValue(row, 2, Conf.acc(axis).ToString("0.##"));      // 가속 시간
                g.setValue(row, 3, Conf.dec(axis).ToString("0.##"));      // 감속 시간
                g.setValue(row, 4, Conf.jogLow(axis).ToString("0.##"));   // 저속
                g.setValue(row, 5, Conf.jogMid(axis).ToString("0.##"));   // 중속
                g.setValue(row, 6, Conf.jogHigh(axis).ToString("0.##"));  // 고속
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveGridToConf();
        }

        private void SaveGridToConf()
        {
            CSourceGrid g = MotorVelGrid;

            for (int i = 0; i < (int)AXIS.MAX; i++)
            {
                AXIS axis = (AXIS)i;
                int row = i + 1;

                double vel = Util.toDouble(g.cell(row, 1).Value?.ToString());
                double acc = Util.toDouble(g.cell(row, 2).Value?.ToString());
                double dec = Util.toDouble(g.cell(row, 3).Value?.ToString());
                double jogLow = Util.toDouble(g.cell(row, 4).Value?.ToString());
                double jogMid = Util.toDouble(g.cell(row, 5).Value?.ToString());
                double jogHigh = Util.toDouble(g.cell(row, 6).Value?.ToString());

                // =========================
                // Conf 저장
                // =========================
                Conf.setVel(axis, vel);
                Conf.setAcc(axis, acc);
                Conf.setDec(axis, dec);

                Conf.setJogLow(axis, jogLow);
                Conf.setJogMid(axis, jogMid);
                Conf.setJogHigh(axis, jogHigh);
            }
        }

    }
}
