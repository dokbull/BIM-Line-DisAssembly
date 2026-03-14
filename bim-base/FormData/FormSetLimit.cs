using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormSetLimit : Form
    {
        ProcessMain main = null;

        public FormSetLimit(ProcessMain procMain)
        {
            InitializeComponent();
            main = procMain;
        }

        private void FormStep_Load(object sender, EventArgs e)
        {
            if (main.isSimulation() == false)
            {
                List<ExtAxis> axisList = new List<ExtAxis>();

                axisList.Add(main.axis(AXIS.IN_PP_Z));
                axisList.Add(main.axis(AXIS.MOLD_PP_ZL));
                axisList.Add(main.axis(AXIS.MOLD_PP_ZR));

                jogControl2.setMain(main);
                jogControl2.setAxis(axisList);
            }

            //VSData.Instance.LoadSetting();
            SelPos(0);
        }

        private void uiTimer_Tick(object sender, EventArgs e)
        {
            if (main == null)
            {
                return;
            }
            try
            {
                //lbcurrentX.Text = main.axis(AXIS.TRANSFER_X).readActualPos().ToString("f03");
                //lbcurrentY.Text = main.axis(AXIS.TRANSFER_Y).readActualPos().ToString("f03");
            }
            catch (Exception ex)
            {
                Debug.debug($"{ex}");
            }

        }
        void clearSel()
        {
            lbLimitXMinus.BackColor = Color.White;
            lbLimitXPlus.BackColor = Color.White;
            lbLimitYMinus.BackColor = Color.White;
            lbLimitYPlus.BackColor = Color.White;
        }
        void SetColor(Label _lbTarget)
        {
            lbTargetX.BackColor = Color.WhiteSmoke;
            lbTargetX.ForeColor = Color.Black;
            lbTargetY.BackColor = Color.WhiteSmoke;
            lbTargetY.ForeColor = Color.Black;

            _lbTarget.BackColor = Color.Honeydew;
            _lbTarget.ForeColor = Color.Red;

        }
        int GetPosSel()
        {
            if (lbLimitXMinus.BackColor == Color.Bisque)
                return 0;
            else if (lbLimitXPlus.BackColor == Color.Bisque)
                return 1;
            else if (lbLimitYMinus.BackColor == Color.Bisque)
                return 2;
            else if (lbLimitYPlus.BackColor == Color.Bisque)
                return 3;
            return -1;
        }

        void SelPos(int _index)
        {
            //if (_index == 0)
            //{
            //    clearSel();
            //    SetColor(lbTargetX);
            //    lbLimitXMinus.BackColor = Color.Bisque;
            //    lbTargetX.Text = VSData.Instance.m_dLimitX[0].ToString("f03");
            //}
            //else if (_index == 1)
            //{
            //    clearSel();
            //    SetColor(lbTargetX);
            //    lbLimitXPlus.BackColor = Color.Bisque;
            //    lbTargetX.Text = VSData.Instance.m_dLimitX[1].ToString("f03");
            //}
            //else if (_index == 2)
            //{
            //    clearSel();
            //    SetColor(lbTargetY);
            //    lbLimitYMinus.BackColor = Color.Bisque;
            //    lbTargetY.Text = VSData.Instance.m_dLimitY[0].ToString("f03");
            //}
            //else if (_index == 3)
            //{
            //    clearSel();
            //    SetColor(lbTargetY);
            //    lbLimitYPlus.BackColor = Color.Bisque;
            //    lbTargetY.Text = VSData.Instance.m_dLimitY[1].ToString("f03");
            //}
        }
        private void lbLimitXMinus_Click(object sender, EventArgs e)
        {
            SelPos(0);
        }
        private void lbLimitXPlus_Click(object sender, EventArgs e)
        {
            SelPos(1);
        }
        private void lbLimitYMinus_Click(object sender, EventArgs e)
        {
            SelPos(2);
        }

        private void lbLimitYPlus_Click(object sender, EventArgs e)
        {
            SelPos(3);
        }

        private void btSaveLimitX_Click(object sender, EventArgs e)
        {
            int _tmpPoint = GetPosSel();
            if (_tmpPoint == 0)
            {
                //VSData.Instance.m_dLimitX[0] = main.axis(AXIS.TRANSFER_X).readActualPos();
                //VSData.Instance.SaveSettings();
                SelPos(1);
            }
            else if (_tmpPoint == 1)
            {
                //VSData.Instance.m_dLimitX[1] = main.axis(AXIS.TRANSFER_X).readActualPos();
                //VSData.Instance.SaveSettings();
                SelPos(1);
            }
        }

        private void btSaveLimitY_Click(object sender, EventArgs e)
        {
            int _tmpPoint = GetPosSel();
            if (_tmpPoint == 2)
            {
                //VSData.Instance.m_dLimitY[0] = main.axis(AXIS.TRANSFER_Y).readActualPos();
                //VSData.Instance.SaveSettings();
                SelPos(2);
            }
            else if (_tmpPoint == 3)
            {
                //VSData.Instance.m_dLimitY[1] = main.axis(AXIS.TRANSFER_Y).readActualPos();
                //VSData.Instance.SaveSettings();
                SelPos(3);
            }
        }

        private void lbTargetX_Click(object sender, EventArgs e)
        {
            if (lbTargetX.ForeColor != Color.Red) return;
            FormNumpad dlg = new FormNumpad(lbTargetX.Text, true);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                int _idx = GetPosSel();
                if (_idx != -1)
                {
                }
            }
            // VSData.Instance.SaveSettings();
        }
        private void lbTargetY_Click(object sender, EventArgs e)
        {
            if (lbTargetY.ForeColor != Color.Red) return;
            FormNumpad dlg = new FormNumpad(lbTargetY.Text, true);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //int _idx = GetPosSel();
                //if (_idx != -1)
                //{
                //    if (_idx == 2)
                //    {
                //        VSData.Instance.m_dLimitY[0] = Util.toInt32(dlg.getNewValue());
                //        lbTargetY.Text = VSData.Instance.m_dLimitY[0].ToString("f03");
                //    }
                //    else if (_idx == 3)
                //    {
                //        VSData.Instance.m_dLimitY[1] = Util.toInt32(dlg.getNewValue());
                //        lbTargetY.Text = VSData.Instance.m_dLimitY[1].ToString("f03");
                //    }
                //}
            }
            //VSData.Instance.SaveSettings();
        }
    }
}
