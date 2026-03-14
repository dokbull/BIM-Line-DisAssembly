using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class SingleCylinder: CDevice
{
    CElaspedTimer m_onTimer = new CElaspedTimer(5000);
    CElaspedTimer m_offTimer = new CElaspedTimer(5000);

    //int m_timeout = 10 * 1000; // 10 s

    string m_name = "";

    EContact m_output = null;

    EContact m_inputOn = null;
    EContact m_inputOff = null;

    bool m_outputValue = false;

    public SingleCylinder(string name, int outputAddr,
                                    int inputOnAddr, int inputOffAddr)
    {
        name = m_name;

        if (outputAddr != -1) m_output = new EContact(EContact.ContactType.OUTPUT, outputAddr);
        if (inputOnAddr != -1) m_inputOn = new EContact(EContact.ContactType.INPUT, inputOnAddr);
        if (inputOffAddr != -1) m_inputOff = new EContact(EContact.ContactType.INPUT, inputOffAddr);

        m_inputList.Add(m_inputOn);
        m_inputList.Add(m_inputOff);

        m_outputList.Add(m_output);
    }

    public bool on()
    {
        return m_inputList[0].value;
    }

    public bool off()
    {
        return m_inputList[1].value;
    }

    public void setOutput(bool value)
    {
        bool agoValue = m_outputValue;

        m_outputValue = value;

        //m_outputOn.value = value;
        //m_outputOff.value = !value;

        m_outputList[0].value = value;

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
            if (m_output != null)
            {
                if (m_inputOff.value == false && m_offTimer.isElasped())
                    m_isError = true;
            }
        }
    }
}
