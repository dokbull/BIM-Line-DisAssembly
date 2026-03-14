using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ProximitySensor : CDevice
{
    bool m_agoInputStatus = false;
    int m_checkTick;
    int m_emptyTick;
    int m_confirmTime;

    bool m_confirm;

    public ProximitySensor(string name, int inputAddr, int confirmTime)
    {
        m_confirmTime = confirmTime;

        EContact contact = new EContact(EContact.ContactType.INPUT, inputAddr);
        m_inputList.Add(contact);
    }

    public void setConfirmTime(int confirmTime)
    {
        m_confirmTime = confirmTime;
    }

    public void resetConfirm()
    {
        m_confirm = false;
        m_agoInputStatus = false;
        m_checkTick = Environment.TickCount;
    }

    public bool confirm()
    {
        return m_confirm;
    }

    public bool isEmpty()
    {
        if (isOn()) return false;

        if (m_confirm)
            return false;

        if (Environment.TickCount - m_emptyTick > m_confirmTime)
            return true;

        return false;
    }

    public bool isOn()
    {
        return m_inputList[0].value;
    }

    public override void run()
    {
        if (m_inputList.Count == 0)
            return;

        EContact contactInput = m_inputList[0];

        if (contactInput.value)
        {
            m_emptyTick = 0;

            if (m_agoInputStatus == false)
            {
                m_checkTick = Environment.TickCount;
            }
            else
            {
                if (Environment.TickCount - m_checkTick > m_confirmTime)
                {
                    m_confirm = true;
                }
            }
        }
        else
        {
            if (m_agoInputStatus == true)
            {
                if (m_emptyTick == 0)
                    m_emptyTick = Environment.TickCount;
            }

            m_confirm = false;
        }

        m_agoInputStatus = contactInput.value;
    }
}
