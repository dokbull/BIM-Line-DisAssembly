using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CElaspedTimer
{
    int m_time = 0;
    int m_startTime = 0;
    bool m_isStart = false;
    bool m_pause = false;

    int m_pauseTick = 0;

    int m_stopTick = 0;

    public CElaspedTimer(int time)
    {
        m_time = time;
    }

    public int time()
    {
        return m_time;
    }

    public void setTime(int time)
    {
        m_time = time;

        if (m_isStart)
            start();
    }

    public void setTime(double time)
    {
        m_time = (int)time;

        if (m_isStart)
            start();
    }

    public void start()
    {
        m_isStart = true;
        m_pause = false;
        m_startTime = Environment.TickCount;
    }

    public void stop()
    {
        m_isStart = false;

        m_stopTick = Environment.TickCount;
    }

    public bool isStart()
    {
        return m_isStart;
    }

    public bool isElasped()
    {
        if (!m_isStart)
            return false;

        if (Environment.TickCount - m_startTime > m_time)
            return true;

        return false;
    }

    public int spendTime()
    {
        if (m_pause)
        {
            return m_pauseTick - m_startTime;
        }

        if (m_isStart == false)
            return 0;

        return Environment.TickCount - m_startTime;
    }

    public int elapsedTime()
    {
        if (m_isStart)
            return Environment.TickCount - m_startTime;

        return m_stopTick - m_startTime;
    }

    public void pause()
    {
        if (m_pause)
            return;

        m_pause = true;
        m_pauseTick = Environment.TickCount;
    }

    public void resume()
    {
        if (m_pause == false)
            return;

        m_startTime = Environment.TickCount - spendTime();
        m_pause = false;
    }

    public bool isPause()
    {
        return m_pause;
    }
}
