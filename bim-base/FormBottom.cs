using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using System.IO;

namespace bim_base
{
    public partial class FormBottom : Form, IForm
    {
        ProcessMain main;
        public event EventHandler bottomButtonClick;

        Button[] m_buttonArg = null;

        Queue<string> m_historyQueue = new Queue<string>();

        bool m_simulation = false;

        public FormBottom(ProcessMain procMain)
        {
            InitializeComponent();

            m_simulation = File.Exists(Common.PATH + "\\simulation");

            main = procMain;

            int cnt = 0;

            m_buttonArg = new Button[] {
                autoButton,
                teachButton,
                databutton,
                logButton,
                hideButton };

            foreach (Button button in m_buttonArg)
            {
                button.Tag = cnt++;
                button.Click += btnClick;
            }
        }

        private void btnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idx = Convert.ToInt32(btn.Tag);

            if (m_simulation == false)
            {   
                if (idx == 1 || idx == 2)
                {
                    if (main.isAdmin() == false)
                    {
                        CMessageBox msgBox = new CMessageBox(Common.TITLE,
                            "administrator permission required", MessageBoxButtons.OK);

                        msgBox.showDialog();
                        return;
                    }
                }
            }

            foreach (Button button in m_buttonArg)
            {
                if (btn == button)
                    continue;
            }

            if (bottomButtonClick != null) 
                bottomButtonClick(idx, null);
        }

        public void onShow(bool enable)
        {
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            CMessageBox msgBox = new CMessageBox(Common.TITLE, PopupMessage.message(POPUP.EXIT_PROGRAM), MessageBoxButtons.OKCancel);
            if (msgBox.showDialog() == false)
                return;

            main.stop();
            Application.Exit();
        }

        private void hideButton_Click(object sender, EventArgs e)
        {
            FormMain.inst().WindowState = FormWindowState.Minimized;
        }

        public void addHistory(string history)
        {
            string timeString = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]";
            string text = timeString + history;

            m_historyQueue.Enqueue(text);
        }

        private void ui_timer_Tick(object sender, EventArgs e)
        {
            bool state = main.isAuto();

            teachButton.Enabled = !state;
            databutton.Enabled = !state;

            if (m_historyQueue.Count > 0)
            {
                string text = m_historyQueue.Dequeue();
                historyList.Items.Insert(0, text);
                if (historyList.Items.Count > 200)
                    historyList.Items.RemoveAt(200);
            }
        }

        private void FormBottom_Load(object sender, EventArgs e)
        {
            ui_timer.Enabled = true;
        }
    }
}
