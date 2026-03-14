using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class CNumberKeyPad : Form
{
    public enum Style
    {
        Style_Unknwon=0,
        Style_Password,
        Style_Int,
        Style_Float,
        Style_Text,
        Style_Text_Password,
    }

    public event EventHandler enterEvent;
    bool m_minusEnable = true;
    bool m_enterState = false;

    public Style m_style;

    public CNumberKeyPad(Style _style, string message)
    {
        InitializeComponent();

        messageLabel.Text = message;

        m_style = _style;

        button_0.Click += new EventHandler(numButton_Click);
        button_1.Click += new EventHandler(numButton_Click);
        button_2.Click += new EventHandler(numButton_Click);
        button_3.Click += new EventHandler(numButton_Click);
        button_4.Click += new EventHandler(numButton_Click);
        button_5.Click += new EventHandler(numButton_Click);
        button_6.Click += new EventHandler(numButton_Click);
        button_7.Click += new EventHandler(numButton_Click);
        button_8.Click += new EventHandler(numButton_Click);
        button_9.Click += new EventHandler(numButton_Click);
        minusButton.Click += new EventHandler(numButton_Click);
        commaButton.Click += new EventHandler(numButton_Click);

        minusButton.Visible = m_minusEnable;
    }

    private void CNumberKeyPad_Load(object sender, EventArgs e)
    {
        if (m_style == Style.Style_Password)
        {
            minusButton.Visible = false;
            textBox1.UseSystemPasswordChar = true;
            textBox1.Multiline = false;
            textBox1.Size = new Size(450, 59);
        }

        if (m_style == Style.Style_Text_Password)
        {
            textBox1.UseSystemPasswordChar = true;
            textBox1.Multiline = false;
            textBox1.Size = new Size(450, 59);

            tableLayoutPanel1.Visible = false;
            this.Height = 150;
        }

        if (m_style == Style.Style_Int)
        {
            commaButton.Visible = false;
        }

        if (m_style == Style.Style_Float)
        {
            commaButton.Visible = true;
        }
    }

    public string _Password
    {
        get;
        set;
    }

    public void setMinusEnable(bool value)
    {
        m_minusEnable = value;
        minusButton.Visible = value;
    }

    private void numButton_Click(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        string numStr = button.Tag.ToString();
        string text = textBox1.Text;

        if (numStr == "-")
        {
            if (!String.IsNullOrEmpty(text))
                return;
        }
        else if (numStr == ".")
        {
            if (text.IndexOf(".") > -1)
                return;

            if (text.IndexOf("-") > -1 && text.Length == 1)
                return;

            if (m_style == Style.Style_Int)
                return;
        }

        textBox1.Text += numStr;
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void enterButton_Click(object sender, EventArgs e)
    {
        string text = textBox1.Text.Trim();

        //Debug.debug("CNumberKeyPad::enterButton_Click text: " + text + " password: " + _Password);

        double num = 0;
        bool isNum = double.TryParse(text, out num);

        Debug.debug("CNumberKeyPad::enterButton_Click isNum: " + isNum + "  Num: " + num);

        if (enterEvent != null)
        {
            if (m_style == Style.Style_Text || m_style == Style.Style_Text_Password) // 텍스트 입력 허용
                enterEvent(text, null);

            else
                enterEvent(num, null);
        }
        if (m_style == Style.Style_Password || m_style == Style.Style_Text_Password)
        {
            if (text != _Password || String.IsNullOrEmpty(text))
            {
                CMessageBox msgBox = new CMessageBox("ERROR", "This passwords is not match", MessageBoxButtons.OK);
                msgBox.ShowDialog();

                this.DialogResult = DialogResult.Cancel;
                return;
            }
        }

        m_enterState = true;
        this.DialogResult = DialogResult.OK;        
    }

    private void backButton_Click(object sender, EventArgs e)
    {
        string text = textBox1.Text;

        if (String.IsNullOrEmpty(text))
            return;

        text = text.Remove(text.Length - 1);
        textBox1.Text = text;
    }

    private void clearButton_Click(object sender, EventArgs e)
    {
        textBox1.Clear();
    }

    private void CNumberKeyPad_KeyDown(object sender, KeyEventArgs e)
    {
        
    }

    private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
    {

        // Enter
        if (e.KeyChar == (char)13)
        {
            e.Handled = true;
            enterButton_Click(enterButton, null);
            return;
        }

        if (m_style == Style.Style_Text || m_style == Style.Style_Text_Password) // 텍스트 입력 허용
        {
            return;
        }

        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-') && (e.KeyChar != '.') && (e.KeyChar != (char)13)) // 숫자키 및 소숫점과 엔터키만 허용
        {
            e.Handled = true;
        }

        // minusEnable
        if (m_minusEnable == false && (e.KeyChar == '-'))
        {
            e.Handled = true;
        }

        // 소숫점 중복처리 방지
        if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
        {
            e.Handled = true;
        }

        // 마이너스 중복처리 방지
        if ((e.KeyChar == '-') && ((sender as TextBox).Text.IndexOf('-') > -1))
        {
            e.Handled = true;
        }

        // uInt 타입의 경우 

        // int 타입의 경우 소숫점 금지
        if ((e.KeyChar == '.') && (m_style == Style.Style_Int))
        {
            e.Handled = true;
        }
    }

    public void setText(string text)
    {
        messageLabel.Text = text;
        //textBox1.Text = text;
    }

    public void setValue (string text)
    {
        textBox1.Text = text;
    }

    public string text()
    {
        return textBox1.Text;
    }

    public bool enterBool()
    {
        return m_enterState;
    }
}
