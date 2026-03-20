
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormData : Form, IForm
    {
        ProcessMain main;
        bool swLimitClicked = false;
        CElaspedTimer swLimitTimer = new CElaspedTimer(1000 * 3);

        Panel pnlView;


        public FormData(ProcessMain procMain)
        {
            InitializeComponent();
            main = procMain;
        }

        private void FormData_Load(object sender, EventArgs e)
        {
            pnlView = new Panel();
            pnlView.Name = "pnlView";
            pnlView.Dock = DockStyle.Fill;
            pnlView.BackColor = Color.Transparent; // 필요시

            this.Controls.Add(pnlView);
            pnlView.SendToBack(); // 👈 중요 (버튼 위로 올라오면 안됨)
        }

        public void onShow(bool enable)
        {

        }

        void LoadView(Form form)
        {
            pnlView.Controls.Clear();

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            if (form is FormServoVelocity f)
            {
                f.OnCloseRequested += () =>
                {
                    pnlView.Controls.Clear();
                    pnlView.SendToBack(); // 👈 여기서 처리
                };
            }

            pnlView.Controls.Add(form);
            form.Show();

            pnlView.BringToFront(); // 👈 보여줄 때만 앞으로
        }
        //void LoadView(Form form)
        //{
        //    pnlView.Controls.Clear();

        //    form.TopLevel = false;
        //    form.FormBorderStyle = FormBorderStyle.None;
        //    form.Dock = DockStyle.Fill;

        //    // 🔥 여기 핵심
        //    if (form is FormServoVelocity f)
        //    {
        //        f.OnCloseRequested += () =>
        //        {
        //            f.Hide(); // 👈 추가
        //            pnlView.SendToBack();
        //            return;
        //            //pnlFormData.Controls.Clear();
        //        };
        //    }

        //    pnlView.Controls.Add(form);
        //    form.Show();
        //    pnlView.BringToFront();
        //}

        //void ShowMainMenu()
        //{
        //    pnlFormData.Controls.Clear();

        //    // 👉 아무것도 안 넣으면 = 현재(FormDataMain)의 버튼 UI가 그대로 보임
        //}

        private void modelButton_Click(object sender, EventArgs e)
        {
            FormDataModel dlg = new FormDataModel(main);
            dlg.ShowDialog();
        }

        private void systemManagerButton_Click(object sender, EventArgs e)
        {

        }

        private void motorVelButton_Click(object sender, EventArgs e)
        {
            FormMotorVelocity dlg = new FormMotorVelocity(main);
            dlg.ShowDialog();

            if (main.isSimulation() == false)
            {
                main.setAbsSpeedConf(AXIS.IN_PP_Z);
                main.setAbsSpeedConf(AXIS.MOLD_PP_ZL);
                main.setAbsSpeedConf(AXIS.MOLD_PP_ZR);
            }
        }

        private void jogVelButton_Click(object sender, EventArgs e)
        {
            FormJogVelocity dlg = new FormJogVelocity(main);
            dlg.ShowDialog();
        }

        private void pressMCButton_Click(object sender, EventArgs e)
        {
        }

        private void portSettingButton_Click(object sender, EventArgs e)
        {
            FormPortSetting dlg = new FormPortSetting(main);
            dlg.ShowDialog();
        }

        private void ui_timer_Tick(object sender, EventArgs e)
        {
            if (main == null)
                return;

            if (swLimitClicked)
            {
                if (swLimitTimer.isElasped() == true)
                {
                    swLimitClicked = false;
                    //clickCount = 0;
                    swLimitTimer.stop();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void softLimitLabel_DoubleClick(object sender, EventArgs e)
        {
            FormAxisLimit dlg = new FormAxisLimit(main);
            dlg.ShowDialog();
        }

        private void timerSettingButton_Click(object sender, EventArgs e)
        {
            FormTimer dlg = new FormTimer(main);
            dlg.ShowDialog();
        }
        private void label1_Click_1(object sender, EventArgs e)
        {
            FormSetLimit formSetLimit = new FormSetLimit(main);
            formSetLimit.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnServoVelocity_Click(object sender, EventArgs e)
        {
            LoadView(new FormServoVelocity(main));
        }

        private void motorDelayButton_Click(object sender, EventArgs e)
        {
            FormMotorDelay dlg = new FormMotorDelay(main);
            dlg.ShowDialog();
        }

        private void cylinderDelayButton_Click(object sender, EventArgs e)
        {
            FormCylinderDelay dlg = new FormCylinderDelay(main);
            dlg.ShowDialog();
        }

        private void vacuumDelayButton_Click(object sender, EventArgs e)
        {
            FormVacuumDelay dlg = new FormVacuumDelay(main);
            dlg.ShowDialog();
        }
    }
}

