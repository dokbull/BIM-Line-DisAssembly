using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SingleCyl : CDevice
{
    int m_timeout = 10 * 1000; // 타임아웃 시간 5s
    //bool m_agoCheckError = false;

    int m_onDelay = 1000;
    int m_offDelay = 1000;

    int m_onTick = 0;
    int m_offTick = 0;

    string m_name = "";
    bool m_reverseSensor = false;

    EContact m_inputOn = null;
    EContact m_inputOff = null;

    public SingleCyl(string name, int outputAddr, int inputOn = -1, int inputOff = -1)
    {
        m_name = name;

        EContact output = new EContact(EContact.ContactType.OUTPUT, outputAddr);
        m_outputList.Add(output);

        if (inputOn > -1)
        {
            m_inputOn = new EContact(EContact.ContactType.INPUT, inputOn);
            m_inputList.Add(m_inputOn);
        }

        if (inputOff > -1)
        {
            m_inputOff = new EContact(EContact.ContactType.INPUT, inputOff);
            m_inputList.Add(m_inputOff);
        }
    }

    // 센서 조건이 실린더 조건과 반대일 경우에 지정
    public void setReverseSensor(bool value = false)
    {
        m_reverseSensor = value;
    }
    
#region getter setter

    public int inputCount()
    {
        return m_inputList.Count;
    }

    public void setTimeout(int time)
    {
        m_timeout = time;
    }

    public void setOnOffDelay(int time)
    {
        m_onDelay = time;
        m_offDelay = time;
    }

    public bool input(int index=0)
    {
        if (index < 0 || index > m_inputList.Count - 1)
        {
            throw new System.IndexOutOfRangeException
                ("SingleCyl::input invalid index:" + index);
        }

        return m_inputList[index].value;
    }

    public bool output()
    {
        return m_outputList[0].value;
    }

    public bool on()
    {
        bool value = true;

        if (m_inputOn != null)
            value = m_inputOn.value;

        if (m_reverseSensor)
            value = !value;
        
        if (value &&  Environment.TickCount - m_onTick > m_onDelay)
        {
            return true;
        }

        return false;
    }

    public bool off()
    {
        bool value = true;

        if (m_inputOff != null)
            value = m_inputOff.value;
        else
        {
            if (m_inputOn != null)
                value = !m_inputOn.value;
        }

        if (m_reverseSensor)
            value = !value;

        if (value && Environment.TickCount - m_offTick > m_offDelay)
        {
            return true;
        }

        return false;
    }

    public void setOutput(bool value)
    {
        bool agoValue = m_outputList[0].value;

        m_outputList[0].value = value;

        if (value)
        {
            setOnTickCount(Environment.TickCount);
            m_onTick = Environment.TickCount;
            m_offTick = 0;
        }
        else
        {
            setOnTickCount(0);
            m_onTick = 0;
            m_offTick = Environment.TickCount;
        }
    }

#endregion

  

    

    public override void run()
    {
        //Debug.debug("SingleSyl::run checkError result:" + ret);
    }
}
