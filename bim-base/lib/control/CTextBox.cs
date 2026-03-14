using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


public class CTextBox : TextBox
{
    public event EventHandler numberPadEvent;
    public bool m_minusEnable = true;

    public bool _NumberPad
    {
        get;
        set;
    }

    public string _NumberPadText
    {
        get;
        set;
    }

    public CNumberKeyPad.Style _NumberStyle
    {
        get;
        set;
    }

    public bool _MinusEnable
    {
        get { return m_minusEnable; }
        set { m_minusEnable = value; }
    }

    public CTextBox()
    {
        this.ReadOnly = true;
        this.Click += click; 
    }

    public void click(object sender, EventArgs e)
    {
        Debug.debug("CTextBox::click obj name:" + sender.ToString());

        if (numberPadEvent != null && _NumberPad)
            numberPadEvent(this, null);

        if (_NumberPad)
        {
            CNumberKeyPad keyPad = new CNumberKeyPad(_NumberStyle, _NumberPadText);
            keyPad.setMinusEnable(m_minusEnable);

            keyPad.enterEvent += setText;

            keyPad.setText(this.Text);

            keyPad.StartPosition = FormStartPosition.CenterParent;
            keyPad.ShowDialog();
        }
    }

    private void setText(object sender, EventArgs e)
    {
        string text = "";

        try
        {
            double value = (double)sender;

            Debug.debug("CTextBox::setText.. value: " + value);

            text = value.ToString();
        }
        catch
        {
            text = (string)sender;
        }

        if (text.Length > this.MaxLength)
        {
            text = text.Substring(0, this.MaxLength);
        }

        this.Text = text;
    }

}

