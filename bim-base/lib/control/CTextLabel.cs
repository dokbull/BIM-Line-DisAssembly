using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


class CTextLabel : Label
{
    public event EventHandler numberPadEvent;

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

    public CTextLabel()
    {
        this.Click += click; 
    }

    public void click(object sender, EventArgs e)
    {
        Debug.debug("CTextLabel::click obj name:" + sender.ToString());

        if (numberPadEvent != null && _NumberPad)
            numberPadEvent(this, null);

        if (_NumberPad)
        {
            CNumberKeyPad keyPad = new CNumberKeyPad(_NumberStyle, _NumberPadText);

            keyPad.enterEvent += setText;

            keyPad.setText(this.Text);

            keyPad.StartPosition = FormStartPosition.CenterParent;
            keyPad.ShowDialog();
        }
    }

    private void setText(object sender, EventArgs e)
    {

        if (_NumberStyle == CNumberKeyPad.Style.Style_Text || _NumberStyle == CNumberKeyPad.Style.Style_Text_Password)
        {
            string value = (string)sender;

            Debug.debug("CTextLabel::setText.. value: " + value);
            this.Text = value;
        }
        else
        {
            double value = (double)sender;

            Debug.debug("CTextLabel::setText.. value: " + value);
            this.Text = value.ToString();
        }
    }
}

