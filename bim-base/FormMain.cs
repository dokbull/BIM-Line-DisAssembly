using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormMain : Form
    {
        Form[] m_formMainArg = null;
        Form[] m_formCenterPageArg = null;

        ProcessMain main = null;

        static FormMain m_inst = null;

        FormTop m_formTop = null;
        FormBottom m_formBottom = null;

        FormAuto m_formAuto = null;
        FormTeach m_formTeach = null;

        FormTeachInPP m_formTeach_IN_PP = null;
        FormTeachTrayPP m_formTeach_TRAY_PP = null; 
        FormTeachOutPP m_formTeach_OUT_PP = null; 

        FormData m_formData = null;

        FormDataModel m_formData_Model = null;
        FormDataSystem m_formData_System = null;
        FormDataMotorVel m_formData_MotorVel = null;
        FormDataJogVelocity m_formData_JogVel = null;
        FormDataPort m_formData_Port = null;

        FormLog m_formLog = null;

        Form m_nowForm = null;

        bool m_showAlarm = false;
        bool m_showWarnning = false;

        int m_currPageIdx = 0;

        public FormMain()
        {
            if (m_inst != null)
                return;

            m_inst = this;

            InitializeComponent();

            Conf.load();
            Common.init();

            Alarm.load();
            PopupMessage.load();

            main = new ProcessMain();
            main.start();

            m_formTop = new FormTop(main);
            m_formBottom = new FormBottom(main);

            m_formAuto = new FormAuto(main);
            m_formTeach = new FormTeach(main);

            m_formTeach.button0_Click += M_formTeach_button0_Click;
            m_formTeach.button1_Click += M_formTeach_button1_Click; 
            m_formTeach.button2_Click += M_formTeach_button2_Click;

            m_formTeach_IN_PP = new FormTeachInPP(main);
            m_formTeach_TRAY_PP = new FormTeachTrayPP(main);
            m_formTeach_OUT_PP = new FormTeachOutPP(main);

            m_formData = new FormData(main);

            m_formData_Model = new FormDataModel(main);
            m_formData_System = new FormDataSystem(main);
            m_formData_MotorVel = new FormDataMotorVel(main);
            m_formData_JogVel = new FormDataJogVelocity(main);
            m_formData_Port = new FormDataPort(main);

            m_formData.modelButton_Clicked += M_formData_modelButton_Clicked;
            m_formData.systemManagerButton_Clicked += M_formData_systemManagerButton_Clicked;
            m_formData.motorVelButton_Clicked += M_formData_motorVelButton_Clicked;
            m_formData.jogVelButton_Clicked += M_formData_jogVelButton_Clicked;
            m_formData.portSettingButton_Clicked += M_formData_portSettingButton_Clicked;

            m_formLog = new FormLog(main);

            m_formMainArg = new Form[] { m_formTop, null, m_formBottom };

            m_formCenterPageArg = new Form[] { m_formAuto, 
                m_formTeach, m_formTeach_IN_PP, m_formTeach_TRAY_PP, m_formTeach_OUT_PP,
                m_formData, m_formData_Model, m_formData_System, m_formData_MotorVel, m_formData_JogVel, m_formData_Port,
                m_formLog };

            Common.MODEL_INFO.changedModel += currModelChange;
        }

        private void M_formData_modelButton_Clicked(object sender, EventArgs e)
        {
            showPage(PAGE.DATA_MODEL);
        }

        private void M_formData_systemManagerButton_Clicked(object sender, EventArgs e)
        {
            showPage(PAGE.DATA_SYSTEM);
        }

        private void M_formData_motorVelButton_Clicked(object sender, EventArgs e)
        {
            showPage(PAGE.DATA_MOTOR_VEL);
        }

        private void M_formData_jogVelButton_Clicked(object sender, EventArgs e)
        {
            showPage(PAGE.DATA_JOG_VEL);
        }

        private void M_formData_portSettingButton_Clicked(object sender, EventArgs e)
        {
            showPage(PAGE.DATA_PORT);
        }

        private void M_formTeach_button0_Click(object sender, EventArgs e)
        {
            showPage(PAGE.TEACH_IN_PP);
        }

        private void M_formTeach_button1_Click(object sender, EventArgs e)
        {
            showPage(PAGE.TEACH_TRAY_PP);
        }

        private void M_formTeach_button2_Click(object sender, EventArgs e)
        {
            showPage(PAGE.TEACH_OUT_PP);
        }

        public static FormMain inst()
        {
            return m_inst;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;

            Rectangle screenRect = Screen.PrimaryScreen.Bounds;

            if (screenRect.Width > 1024)
            {
                this.Size = new Size(1024, 768);
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }

            // 초기 화면 세팅
            int cnt = 0;
            foreach (Form form in m_formMainArg)
            {
                if (form == null)
                {
                    cnt++;
                    continue;
                }

                form.Margin = new Padding(0);
                form.MdiParent = this;

                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
                mainTableLayout.Controls.Add(form, 0, cnt);
                form.Show();

                cnt++;
            }

            foreach (Form form in m_formCenterPageArg)
            {
                if (form == null)
                    continue;

                form.Margin = new Padding(0);
                form.MdiParent = this;

                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
            }

            m_formBottom.bottomButtonClick += new EventHandler(bottomButtonClick);

            mainTableLayout.Dock = DockStyle.Fill;
            mainTableLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            showPage(PAGE.AUTO); // 폼 타이머 로드

            uiTimer.Enabled = true;
        }

        private void bottomButtonClick(object sender, EventArgs e)
        {
            PAGE p = (PAGE)sender;

            switch ((int)p)
            {
                case 0: showPage(PAGE.AUTO); break;
                case 1: showPage(PAGE.TEACH); break;
                case 2: showPage(PAGE.DATA); break;
                case 3: showPage(PAGE.LOG); break;
                default:
                    return;
            }
        }

        public  void showPage(PAGE page)
        {
            int pageIndex = (int)page;

            if (pageIndex < 0 || pageIndex > (int)PAGE.MAX - 1)
                return;

            Form dstForm = (Form)m_formCenterPageArg[(int)page];

            if (m_nowForm == dstForm)
                return;

            if (dstForm == null)
                return;

            mainTableLayout.SuspendLayout();

            int index = mainTableLayout.Controls.IndexOf(m_nowForm);

            if (index > -1)
                mainTableLayout.Controls.RemoveAt(index);

            foreach (Form form in m_formCenterPageArg)
            {
                if (form == dstForm)
                    continue;

                if (form == null)
                    continue;

                form.Hide();
            }

            if (m_nowForm != null)
            {
                mainTableLayout.Controls.Remove(m_nowForm);
                m_nowForm = null;
            }

            mainTableLayout.Controls.Add(dstForm, 0, 1);
            m_nowForm = dstForm;
            dstForm.Show();

            m_currPageIdx = pageIndex;

            mainTableLayout.ResumeLayout();
        }

        public PAGE currentShowPage() { return (PAGE)m_currPageIdx; }

        private void uiTimer_Tick(object sender, EventArgs e)
        {
            if (main == null)
                return;

            if (main.isAlarm() == true && m_showAlarm == false)
            {
                m_showAlarm = true;
                int code = main.lastAlarmCode();
                string desc = main.lastAlarmDesc();

                if (code < 0)
                    return;

                FormAlarm dlg = new FormAlarm(main);
                dlg.addAlarmInfo(code, desc);
                dlg.ShowDialog();
                m_showAlarm = false;
            }

            if (main.isShowInitCheckMsg() == true && m_showWarnning == false)
            {
                m_showWarnning = true;
                string msg = "";

                CMessageBox msgBox = new CMessageBox(Common.TITLE, msg, MessageBoxButtons.OK);
                msgBox.showDialog();

                main.setShowInitCheckMsg(false);
                m_showWarnning = false;
            }
        }

        public void clearAlarm()
        {
            m_showAlarm = false;
        }

        public void addAlarmLog(AlarmData data)
        {
            if (m_formLog != null)
                m_formLog.addLog(data);
        }

        public void addHistory(string text)
        {
            if (m_formBottom != null)
                m_formBottom.addHistory(text);
        }

        public void currModelChange(object sender, EventArgs e)
        {
            if (m_formTeach == null)
                return;
        }

        public void changeFormBorderStyle()
        {
            FormBorderStyle style = this.FormBorderStyle;

            if (style == FormBorderStyle.None)
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
            else
                this.FormBorderStyle = FormBorderStyle.None;
        }
    }//class
}//namespace
