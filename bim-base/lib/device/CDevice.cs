using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class CDevice
{
    protected bool m_isError = false;
    protected bool m_errorConfirm = false;

    protected List<EContact> m_inputList = new List<EContact>();
    protected List<EContact> m_outputList = new List<EContact>();

    int m_onTickCount = 0;

    public List<EContact> inputList()
    {
        return m_inputList;
    }

    public List<EContact> outputList()
    {
        return m_outputList;
    }

    // ON-OFF 관련
    public void setOnTickCount(int value)
    {
        m_onTickCount = value;
    }

    public int onTickCount()
    {
        return m_onTickCount;
    }

    public void errorReset()
    {
        m_errorConfirm = false;
        m_isError = false;
        m_onTickCount = 0;
    }

    public void setErrorConfirm()
    {
        m_errorConfirm = true;
    }

    public bool errorConfirm()
    {
        return m_errorConfirm;
    }

    public bool isError()
    {
        return m_isError;
    }

    public abstract void run();
}
