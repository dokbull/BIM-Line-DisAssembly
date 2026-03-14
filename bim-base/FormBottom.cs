using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace bim_base
{
    public enum PAGE
    {
        AUTO = 0,
        MANUAL = 1,
        TEACH = 2,
        DATA = 3,
        MONITOR = 4,
        LOG = 5,
        //VISION = 6,
        MAX,
    }

    public partial class FormBottom : Form, IForm
    {
        ProcessMain main;
        public event EventHandler bottomButtonClick;

        SUserControls.ColorButton[] m_buttonArg = null;

        Queue<string> m_historyQueue = new Queue<string>();
        private Dictionary<SUserControls.ColorButton, (Color top, Color bottom)> _originColors = new Dictionary<SUserControls.ColorButton, (Color, Color)>();

        private readonly Color SELECT_TOP = Color.LightBlue;
        private readonly Color SELECT_BOTTOM = Color.LightBlue;
        public FormBottom(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;

            int cnt = 0;

            m_buttonArg = new SUserControls.ColorButton[] {
                BT_AUTO,
                BT_MANUAL,
                BT_TEACH,
                BT_DATA,
                BT_MONITOR,
                BT_ALARM,
                BT_HIDE,
                BT_EXIT
            };

            foreach (SUserControls.ColorButton button in m_buttonArg)
            {
                button.Tag = cnt++;
                button.Click += btnClick;
                _originColors[button] = (button.GradientTop, button.GradientBottom);
            }
            SetSelectedButton(BT_AUTO);
        }
        private void SetSelectedButton(SUserControls.ColorButton selected)
        {
            foreach (SUserControls.ColorButton button in m_buttonArg)
            {
                int idx = Convert.ToInt32(button.Tag);

                if (idx >= 0 && idx <= 5)
                {
                    if (button == selected)
                    {
                        button.GradientTop = SELECT_TOP;
                        button.GradientBottom = SELECT_BOTTOM;
                    }
                    else
                    {
                        button.GradientTop = _originColors[button].top;
                        button.GradientBottom = _originColors[button].bottom;
                    }
                }
                else
                {
                    button.GradientTop = _originColors[button].top;
                    button.GradientBottom = _originColors[button].bottom;
                }

                button.Invalidate();
            }
        }
        private void btnClick(object sender, EventArgs e)
        {
            if (bottomButtonClick == null)
                return;

            SUserControls.ColorButton btn = (SUserControls.ColorButton)sender;
            int idx = Convert.ToInt32(btn.Tag);

            if ((idx == 1 || idx == 2 || idx == 3) && main.isAdmin() == false)
            {
                string _Password = "";
                FormKeyboard dlg = new FormKeyboard();
                dlg._TYPE = KEYBOARD_TYPE.Password;
                DialogResult res = dlg.ShowDialog();

                if (res == DialogResult.OK)
                {
                    _Password = dlg.getKeyword();
                    if (_Password != Conf.PASSWORD) return;
                    main._TimSetup.StartTimer();
                    main.setAdmin(true);
                }
                else
                {
                    return;
                }
            }
            //else if (idx == 4 && !main.m_bSetupVS)
            //{
            //    string _Password = "";
            //    FormKeyboard dlg = new FormKeyboard();
            //    dlg._TYPE = KEYBOARD_TYPE.Password;
            //    DialogResult res = dlg.ShowDialog();

            //    if (res == DialogResult.OK)
            //    {
            //        _Password = dlg.getKeyword();
            //        if (_Password != main._PassVision) return;
            //        main._TimSetupVS.StartTimer();
            //        main.m_bSetupVS = true;
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}
            SetSelectedButton(btn);
            foreach (SUserControls.ColorButton buttonn in m_buttonArg)
            {
                if (btn == buttonn)
                    continue;
            }
            bottomButtonClick(idx, null);
        }

        public void onShow(bool enable)
        {
        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            CMessageBox msgBox = new CMessageBox(Common.TITLE, "DO YOU WANT TO EXIT PROGRAM?", MessageBoxButtons.OKCancel);
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
            try
            {
                string timeString = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]";
                string text = timeString + history;

                m_historyQueue.Enqueue(text);
            }
            catch (Exception ex)
            {
                Debug.debug($"Add log bottom fail : {ex.ToString()}");
            }
        }

        private void ui_timer_Tick(object sender, EventArgs e)
        {
            bool state = main.isAuto();

            BT_TEACH.Enabled = !state;
            BT_DATA.Enabled = !state;
            BT_MANUAL.Enabled = !state;
            try
            {
                if (m_historyQueue.Count > 0)
                {
                    string text = m_historyQueue.Dequeue();

                    historyList.Items.Insert(0, text);

                    if (historyList.Items.Count > 100)
                        historyList.Items.RemoveAt(100);
                }
            }
            catch (Exception ex)
            {
                Debug.debug($"{ex}");
                Debug.debug("Add messenger to bottom fail logic");
            }
        }

        private void FormBottom_Load(object sender, EventArgs e)
        {
            ui_timer.Enabled = true;
        }

        private void BT_DATA_Click(object sender, EventArgs e)
        {

        }
    }
}
