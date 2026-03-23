using System;
using System.Drawing;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormMain : Form
    {
        FormMain m_form;
        public static Point P_Pos = new Point();
        Form[] m_formMainArg = null;
        Form[] m_formCenterPageArg = null;

        ProcessMain main = null;
        CElaspedTimer timershowAlarm = new CElaspedTimer(5000);

        static FormMain m_inst = null;

        FormTop m_formTop = null;
        public readonly FormBottom m_formBottom = null;

        static FormAuto m_formAuto = null;
        FormManual m_formManual = null;
        FormTeach m_formTeach = null;
        FormData m_formData = null;
        FormMonitor m_formMonitor = null;
        FormLog m_formLog = null;
        FormAlarmList m_formAlarmList = null;

        Form m_nowForm = null;



        bool m_showAlarm = false;
        bool m_showWarnning = false;

        int m_currPageIdx = 0;
        public static FormMain getForm()
        {
            return m_inst;
        }
        static public FormAuto GetformAuto()
        {
            return m_formAuto;
        }
        public FormMain()
        {
            if (m_inst != null)
                return;

            m_inst = this;

            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            Common.inst();

            main = new ProcessMain();
            main.start();

            m_formTop = new FormTop(main);
            m_formBottom = new FormBottom(main);

            m_formAuto = new FormAuto(main);
            m_formManual = new FormManual(main);
            m_formTeach = new FormTeach(main);
            m_formData = new FormData(main);
            m_formMonitor = new FormMonitor(main);
            m_formAlarmList = new FormAlarmList(main);

            m_formLog = new FormLog(main);

            m_formMainArg = new Form[] { m_formTop, null, m_formBottom };
            m_formCenterPageArg = new Form[] { m_formAuto, m_formManual, m_formTeach, m_formData, m_formMonitor, m_formAlarmList };
            P_Pos.X = this.Top;
            P_Pos.X = this.Left;
            m_form = this;

        }

        public static FormMain inst()
        {
            return m_inst;
        }
        public static Point GetLocation()
        {
            Point T = new Point(P_Pos.X, P_Pos.Y);
            return T;
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
                form.Padding = new Padding(1, 1, 1, 1);
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
                form.Padding = new Padding(1, 1, 1, 1);
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
                form.AutoScaleMode = AutoScaleMode.None;
            }

            m_formBottom.bottomButtonClick += new EventHandler(bottomButtonClick);

            mainTableLayout.Dock = DockStyle.Fill;
            mainTableLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            //showPage(PAGE.VISION); // 폼 타이머 로드
            showPage(PAGE.TEACH); // 폼 타이머 로드
            showPage(PAGE.AUTO); // 폼 타이머 로드

            uiTimer.Enabled = true;
        }

        private void bottomButtonClick(object sender, EventArgs e)
        {
            showPage((PAGE)sender);
        }

        private void showPage(PAGE page)
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
            try
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
            catch (Exception ex)
            {
                Debug.debug($"{ex}");
                Debug.debug("Exit Fail");
            }
        }

        public void clearAlarm()
        {
            m_showAlarm = false;
        }

        public void setAlarm()
        {
            m_showAlarm = true;
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

        public void currModelChange()
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

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }//class
}//namespace
