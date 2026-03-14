using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class CMessageBox : Form
{
    MessageBoxButtons m_buttons = MessageBoxButtons.OK;

    //public event EventHandler 

    public CMessageBox()
    {
        InitializeComponent();
        set(Common.TITLE, "Do you want to process?", MessageBoxButtons.OKCancel);

#if USE_SEC_FONT
        this.ControlBox = false;
        this.BackColor = Color.AntiqueWhite;
#endif
    }

    public CMessageBox(string title, string message, MessageBoxButtons buttons, ContentAlignment align = ContentAlignment.MiddleCenter, bool setFont = false)
    {
        InitializeComponent();
        set(title, message, buttons, align);
    }

    public static bool showInfo(string message)
    {
        CMessageBox msgBox = new CMessageBox(Common.TITLE, message, MessageBoxButtons.OK);
        return msgBox.showDialog();
    }

    public static bool showMessage(string message)
    {
        CMessageBox msgBox = new CMessageBox(Common.TITLE, message, MessageBoxButtons.OKCancel  );
        return msgBox.showDialog();
    }

    void set(string title, string message, MessageBoxButtons buttons, ContentAlignment align = ContentAlignment.MiddleCenter, bool setFont = false)
    {
        this.Text = title;

#if USE_SEC_FONT
        messageLabel.Font = new Font("SamsungOne 800C", 20);

        button1.Font = new Font("SamsungOne 800C", 16);
        button2.Font = new Font("SamsungOne 800C", 16);
        button3.Font = new Font("SamsungOne 800C", 16);
#endif

        messageLabel.Text = message;
        messageLabel.TextAlign = align;

        if (setFont) // 폰트 변경
            messageLabel.Font = this.Font;

        m_buttons = buttons;

        tabControl1.Appearance = TabAppearance.FlatButtons;
        tabControl1.ItemSize = new Size(0, 1);
        tabControl1.SizeMode = TabSizeMode.Fixed;

        Button okBtn = button1;

        if (buttons == MessageBoxButtons.YesNo)
        {
            button1.Text = "Yes (&Y)";
            button2.Text = "No (&N)";
            tabControl1.SelectedIndex = 0;
        }
        else if (buttons == MessageBoxButtons.OKCancel)
        {
            button1.Text = "OK (&O)";
            button2.Text = "Cancel (&C)";
            tabControl1.SelectedIndex = 0;
        }
        else
        {
            button3.Text = "OK (&O)";
            tabControl1.SelectedIndex = 1;

            okBtn = button3;
        }

        this.AcceptButton = okBtn;
        this.CancelButton = button2;
    }

    public void setSize(int height, int width)
    {
        this.Height = height;
        this.Width = width;
    }

    public void setFont(int fontSize, string fontName = "나눔바른고딕")
    {
        Font textFont = new Font(fontName, fontSize);
        messageLabel.Font = textFont;

        button1.Font = textFont;
        button2.Font = textFont;
        button3.Font = textFont;
    }

    public void setMessageFont(int fontSize, string fontName = "나눔바른고딕", bool isBold = false)
    {
        FontStyle style = FontStyle.Regular;
        if (isBold)
            style = FontStyle.Bold;

        Font textFont = new Font(fontName, fontSize, style);
        messageLabel.Font = textFont;
    }

    public void setButtonFont(int fontSize, string fontName = "나눔바른고딕", bool isBold = false)
    {
        FontStyle style = FontStyle.Regular;
        if (isBold) 
            style = FontStyle.Bold;

        Font textFont = new Font(fontName, fontSize, style);

        button1.Font = textFont;
        button2.Font = textFont;
        button3.Font = textFont;
    }

    public void disableCloseWindow()
    {
        this.TopMost = true;
        this.ControlBox = false;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (m_buttons == MessageBoxButtons.YesNo)
            this.DialogResult = DialogResult.Yes;
        else
            this.DialogResult = DialogResult.OK;
    }

    private void button2_Click(object sender, EventArgs e)
    {
        if (m_buttons == MessageBoxButtons.YesNo)
            this.DialogResult = DialogResult.No;
        else
            this.DialogResult = DialogResult.Cancel;
    }

    private void button3_Click(object sender, EventArgs e)
    {
        Close();
    }

    public bool showDialog()
    {
        DialogResult result = this.ShowDialog();

        if (result == DialogResult.Cancel ||
            result == DialogResult.No)
        {
            return false;
        }

        return true;
    }

    public bool showTopMost(Form ownerForm)
    {
        DialogResult result = this.ShowDialog();

        this.Owner = ownerForm;
        this.TopMost = true;

        if (result == DialogResult.Cancel ||
            result == DialogResult.No)
        {
            return false;
        }

        return true;
    }

    private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
    {

    }
}
