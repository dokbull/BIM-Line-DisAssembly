using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormNumpad : Form
    {
        bool m_useDouble = true; //true : double , false : int
        double m_minValue = Double.MinValue;
        double m_maxValue = Double.MaxValue;

        public FormNumpad(string currValue, bool useDouble = true)
        {
            InitializeComponent();
            //BtOK.DialogResult = DialogResult.OK;
            TxCurrent.Text = currValue;
            m_useDouble = useDouble;

            BTdot.Enabled = m_useDouble;
            BtSub.Enabled = false;
        }

        public string getNewValue()
        {
            return TxValue.Text;
        }
        
        //public void setMinMaxValue(double min, double max)
        //{
        //    m_minValue = min;
        //    m_maxValue = max;
        //}

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    CheckDataInput("Exit");
                    break;
                case Keys.Return:
                    BtOK.Focus();
                    break;
                case Keys.Back:
                    CheckDataInput("Back");
                    break;
                case Keys.D0:
                    CheckDataInput("0");
                    BT0.Focus();
                    break;
                case Keys.D1:
                    CheckDataInput("1");
                    BT1.Focus();
                    break;
                case Keys.D2:
                    CheckDataInput("2");
                    BT2.Focus();
                    break;
                case Keys.D3:
                    CheckDataInput("3");
                    BT3.Focus();
                    break;
                case Keys.D4:
                    CheckDataInput("4");
                    BT4.Focus();
                    break;
                case Keys.D5:
                    CheckDataInput("5");
                    BT5.Focus();
                    break;
                case Keys.D6:
                    CheckDataInput("6");
                    BT6.Focus();
                    break;
                case Keys.D7:
                    CheckDataInput("7");
                    BT7.Focus();
                    break;
                case Keys.D8:
                    CheckDataInput("8");
                    BT8.Focus();
                    break;
                case Keys.D9:
                    CheckDataInput("9");
                    BT9.Focus();
                    break;
                case Keys.NumPad0:
                    CheckDataInput("0");
                    BT0.Focus();
                    break;
                case Keys.NumPad1:
                    CheckDataInput("1");
                    BT1.Focus();
                    break;
                case Keys.NumPad2:
                    CheckDataInput("2");
                    BT2.Focus();
                    break;
                case Keys.NumPad3:
                    CheckDataInput("3");
                    BT3.Focus();
                    break;
                case Keys.NumPad4:
                    CheckDataInput("4");
                    BT4.Focus();
                    break;
                case Keys.NumPad5:
                    CheckDataInput("5");
                    BT5.Focus();
                    break;
                case Keys.NumPad6:
                    CheckDataInput("6");
                    BT6.Focus();
                    break;
                case Keys.NumPad7:
                    CheckDataInput("7");
                    BT7.Focus();
                    break;
                case Keys.NumPad8:
                    CheckDataInput("8");
                    BT8.Focus();
                    break;
                case Keys.NumPad9:
                    CheckDataInput("9");
                    BT9.Focus();
                    break;
                case Keys.OemPeriod:
                    CheckDataInput(".");
                    BTdot.Focus();
                    break;
                case Keys.Decimal:
                    BTdot.Focus();
                    CheckDataInput(".");
                    break;
                default: break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void PreEvent(object sender, EventArgs e)
        {
            CheckDataInput((sender as Button).Text);
        }

        private bool checkValid(string value, bool useDouble = true)
        {
            if (useDouble)
            {
                if (double.TryParse(value, out double d) && !(double.IsNaN(d) || double.IsInfinity(d)))
                {
                    return true;
                }
            }
            else
            {
                if (int.TryParse(value, out int d))
                {
                    return true;
                }
            }


            return false;
        }

        private void CheckDataInput(string keydata)
        {
            if (keydata == "Clear")
            {
                TxValue.Text = "";
                return;
            }
            else if (keydata == "Back")
            {
                try
                {
                    if (TxValue.Text.Length > 1)
                    {
                        TxValue.Text = TxValue.Text.Substring(0, TxValue.Text.Length - 1);
                    }
                    else
                    {
                        TxValue.Text = "";
                    }
                    BtBack.Focus();
                    return;
                }
                catch
                {

                }
            }
            else
            {
                if (TxValue.Text == ".") TxValue.Text = "";
                if (TxValue.Text == "0" || TxValue.Text == "-0")
                {
                    if (keydata == "0") return;
                }
                try
                {
                    float flTemp = 0;
                    if (keydata == "+/-")
                    {
                        if (TxValue.Text == "") TxValue.Text = "-0";
                        else if (TxValue.Text == "-") TxValue.Text = "";
                        else if (float.TryParse(TxValue.Text, out flTemp))
                        {
                            if (flTemp != 0)
                            {
                                flTemp = 0 - flTemp;
                                TxValue.Text = flTemp.ToString();
                            }
                            else
                            {
                                foreach (char c in TxValue.Text)
                                {
                                    if (c != '-') TxValue.Text = "-" + TxValue.Text;
                                    else TxValue.Text = TxValue.Text.Substring(1);
                                    break;
                                }
                            }
                        }
                    }
                    else if (keydata == ".")
                    {
                        if (TxValue.Text == "")
                        {
                            TxValue.Text = "0.";
                        }
                    }

                    string temp = TxValue.Text + keydata;
                    float.Parse(temp);
                    TxValue.Text += keydata;
                }
                catch
                {

                }
            }
        }

        private void BtExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtOK_Click(object sender, EventArgs e)
        {
            bool valid = checkValid(TxValue.Text, m_useDouble);

            if (valid == false)
            {
                CMessageBox msgBox = new CMessageBox(Common.TITLE, PopupMessage.message(POPUP.INVALID_VALUE), MessageBoxButtons.OK);
                msgBox.ShowDialog();
                return;
            }

            double value = Util.toDouble(TxValue.Text);
            if (value < m_minValue || value > m_maxValue)
            {
                string msg = String.Format(PopupMessage.message(POPUP.INPUT_RANGE), m_minValue, m_maxValue);
                CMessageBox msgBox = new CMessageBox(Common.TITLE, msg, MessageBoxButtons.OK);
                msgBox.ShowDialog();
                return;
            }

            DialogResult = DialogResult.OK;
        }
    }
}
