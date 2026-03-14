using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace bim_base
{
    public enum KEYBOARD_TYPE
    {
        Normal = 0,
        Password = 1
    }

    public partial class FormKeyboard : Form
    {
        private string m_keyword = "";
        private KEYBOARD_TYPE m_type = KEYBOARD_TYPE.Normal;
        public FormKeyboard()
        {
            InitializeComponent();
        }

        private void FornSubKeyboard_Load(object sender, EventArgs e)
        {
            //TxValue.Text = "";
        }

        public KEYBOARD_TYPE _TYPE
        {
            get { return m_type; }
            set { m_type = value; }
        }

        public string getKeyword()
        {
            return m_keyword;
        }

        private void PreEvent(object sender, EventArgs e)
        {
            CheckDataInput((sender as Button).Text);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // MessageBox.Show(keyData.ToString());
            //int value = (int)keyData;
            //txtvaluetest.Text = value.ToString();
            switch (keyData)
            {
                case (Keys)0x100BD:
                    CheckDataInput("_");
                    BT_.Focus();
                    break;
                case Keys.A:
                    CheckDataInput("A");
                    BTA.Focus();
                    break;
                case Keys.B:
                    CheckDataInput("B");
                    BTB.Focus();
                    break;
                case Keys.C:
                    CheckDataInput("C");
                    BTC.Focus();
                    break;
                case Keys.D:
                    CheckDataInput("D");
                    BTD.Focus();
                    break;
                case Keys.E:
                    CheckDataInput("E");
                    BTE.Focus();
                    break;
                case Keys.F:
                    CheckDataInput("F");
                    BTF.Focus();
                    break;
                case Keys.G:
                    CheckDataInput("G");
                    BTG.Focus();
                    break;
                case Keys.H:
                    CheckDataInput("H");
                    BTH.Focus();
                    break;
                case Keys.I:
                    CheckDataInput("I");
                    BTI.Focus();
                    break;
                case Keys.J:
                    CheckDataInput("J");
                    BTJ.Focus();
                    break;
                case Keys.K:
                    CheckDataInput("K");
                    BTK.Focus();
                    break;
                case Keys.L:
                    CheckDataInput("L");
                    BTL.Focus();
                    break;
                case Keys.M:
                    CheckDataInput("M");
                    BTM.Focus();
                    break;
                case Keys.N:
                    CheckDataInput("N");
                    BTN.Focus();
                    break;
                case Keys.O:
                    CheckDataInput("O");
                    BTO.Focus();
                    break;
                case Keys.P:
                    CheckDataInput("P");
                    BTP.Focus();
                    break;
                case Keys.Q:
                    CheckDataInput("Q");
                    BTQ.Focus();
                    break;
                case Keys.R:
                    CheckDataInput("R");
                    BTR.Focus();
                    break;
                case Keys.S:
                    CheckDataInput("S");
                    BTS.Focus();
                    break;
                case Keys.T:
                    CheckDataInput("T");
                    BTT.Focus();
                    break;
                case Keys.U:
                    CheckDataInput("U");
                    BTU.Focus();
                    break;
                case Keys.V:
                    CheckDataInput("V");
                    BTV.Focus();
                    break;
                case Keys.W:
                    CheckDataInput("W");
                    BTW.Focus();
                    break;
                case Keys.X:
                    CheckDataInput("X");
                    BTX.Focus();
                    break;
                case Keys.Y:
                    CheckDataInput("Y");
                    BTY.Focus();
                    break;
                case Keys.Z:
                    CheckDataInput("Z");
                    BTZ.Focus();
                    break;

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
                case Keys.OemMinus:
                    CheckDataInput("-");
                    BTSUB.Focus();
                    break;
                case Keys.Decimal:
                    BTdot.Focus();
                    CheckDataInput(".");
                    break;
                default: break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CheckDataInput(string keydata)
        {
            if (keydata == "Exit" || keydata == "OK")
            {
                this.Close();
                return;
            }
            else if (keydata == "Clear")
            {
                m_keyword = "";
                TxValue.Text = "";
                return;
            }
            else if (keydata == "Back")
            {
                try
                {
                    if (TxValue.Text.Length > 1)
                    {
                        if (m_type == KEYBOARD_TYPE.Password)
                        {
                            m_keyword = m_keyword.Substring(0, m_keyword.Length - 1);
                            TxValue.Text = TxValue.Text.Substring(0, TxValue.Text.Length - 1);
                        }
                        else
                        {
                            TxValue.Text = TxValue.Text.Substring(0, TxValue.Text.Length - 1);
                        }
                    }
                    else
                    {
                        m_keyword = "";
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
                if (m_type == KEYBOARD_TYPE.Password)
                {
                    m_keyword += keydata;
                    TxValue.Text += "*";
                }
                else
                {
                    m_keyword += keydata;
                    TxValue.Text += keydata;
                }
            }
        }
    }
}
