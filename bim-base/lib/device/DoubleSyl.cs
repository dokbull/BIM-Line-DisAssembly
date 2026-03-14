using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DoubleCyl : CDevice
{
    CElaspedTimer m_onTimer = new CElaspedTimer(5000);
    CElaspedTimer m_offTimer = new CElaspedTimer(5000);

    //int m_timeout = 10 * 1000; // 10 s

    string m_name = "";

    EContact m_outputOn = null;
    EContact m_outputOff = null;

    EContact m_inputOn = null;
    EContact m_inputOff = null;

    bool m_outputValue = false;

    public DoubleCyl(string name, int outputOnAddr, int outputOffAddr,
                                    int inputOnAddr, int inputOffAddr)
    {
        name = m_name;

        if (outputOnAddr != -1)     m_outputOn = new EContact(EContact.ContactType.OUTPUT, outputOnAddr);
        if (outputOffAddr != -1)    m_outputOff = new EContact(EContact.ContactType.OUTPUT, outputOffAddr);
        if (inputOnAddr != -1)      m_inputOn = new EContact(EContact.ContactType.INPUT, inputOnAddr);
        if (inputOffAddr != -1)     m_inputOff = new EContact(EContact.ContactType.INPUT, inputOffAddr);

        m_inputList.Add(m_inputOn);
        m_inputList.Add(m_inputOff);

        m_outputList.Add(m_outputOn);
        m_outputList.Add(m_outputOff);
    }

    public bool on()
    {
        return m_inputList[0].value;
    }

    public bool off()
    {
        if (m_inputList[1].address == 0x0)
            return true;

        return m_inputList[1].value;
    }

    public void setOutput(bool value)
    {
        bool agoValue = m_outputValue;

        m_outputValue = value;

        //m_outputOn.value = value;
        //m_outputOff.value = !value;

        m_outputList[0].value = value;
        m_outputList[1].value = !value;

        if (value == agoValue)
            return;

        if (value)
        {
            m_onTimer.start();
        }
        else
        {
            m_offTimer.start();
        }
    }

    public override void run()
    {
        int nowTick = Environment.TickCount;

        if (m_outputValue) // ON
        {
            if (m_inputOn != null)
            {
                if (m_inputOn.value == false && m_onTimer.isElasped())
                    m_isError = true;
            }
        }
        else
        {
            if (m_outputOn != null)
            {
                if (m_inputOff.value == false && m_offTimer.isElasped())
                    m_isError = true;
            }
        }
    }
}
