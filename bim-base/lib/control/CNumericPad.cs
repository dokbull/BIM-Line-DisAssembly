using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class CNumericPad : UserControl
{
    int m_value = 0;
    int m_minValue = 0;
    int m_maxValue = 10;

    bool m_active = false;
    bool m_ignoreActive = false;

    public CNumericPad()
    {
        InitializeComponent();
        updateNameLabelColor();
    }

    public string _NAME
    {
        get { return nameLabel.Text; }
        set { nameLabel.Text = value; }
    }

    public int _MIN
    {
        get { return m_minValue; }
        set { m_minValue = value; }
    }

    public int _MAX
    {
        get { return m_maxValue; }
        set { m_maxValue = value; }
    }

    public bool _IGNORE_ACTIVE
    {
        get { return m_ignoreActive; }
        set { 
            m_ignoreActive = value;
            updateNameLabelColor();
        }
    }

    public void setValue(int value)
    {
        m_value = value;

        if (m_value < m_minValue)
            m_value = m_minValue;

        if (m_value > m_maxValue)
            m_value = m_maxValue;

        valueLabel.Text = m_value.ToString();
    }

    public int value()
    {
        return m_value;
    }

    private void plusButton_Click(object sender, EventArgs e)
    {
        if (m_ignoreActive == false && m_active == false)
            return;

        m_value++;

        if (m_value > m_maxValue)
            m_value = m_maxValue;

        valueLabel.Text = m_value.ToString();
    }

    private void minusButton_Click(object sender, EventArgs e)
    {
        if (m_ignoreActive == false && m_active == false)
            return;

        m_value--;

        if (m_value < m_minValue)
            m_value = m_minValue;

        valueLabel.Text = m_value.ToString();
    }

    private void nameLabel_Click(object sender, EventArgs e)
    {
        if (m_ignoreActive == false)
            m_active = !m_active;

        updateNameLabelColor();
    }

    void updateNameLabelColor()
    {
        Color color = Color.LightYellow;

        if (m_ignoreActive == false)
        {
            if (m_active)
                color = Color.LimeGreen;
            else
                color = Color.Gray;
        }

        if (nameLabel.BackColor != color)
            nameLabel.BackColor = color;
    }
}