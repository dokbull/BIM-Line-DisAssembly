using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

///============================================================================================================================== 
/// <summary> 
/// 시간을 측정하는 기능을 구현한 class. 
/// </summary> 
/// <author>Sung-jin Yun</author> 
/// <created_date>2006-05-16</created_date> 
/// <last_updated>2006-05-16</last_updated> 
public class CStopWatch { 
    [DllImport("kernel32.dll", CallingConvention = CallingConvention.Winapi)] 
    private static extern bool QueryPerformanceFrequency(out long frequency);

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.Winapi)] 
    private static extern bool QueryPerformanceCounter(out long counter);

    /// <summary> 
    /// GetElapsedTime() 함수가 반환하는 시간의 단위를 지정할 때 쓰이는 enum. 
    /// </summary> 
    public enum TIME_UNIT { 
        /// <summary>초 단위.</summary> 
        SECOND,

        /// <summary>밀리초 단위. (1/1000초)</summary> 
        MILLISECOND,

        /// <summary>마이크로초 단위. (1/1000000초)</summary> 
        MICROSECOND, 
        /// <summary>나노초 단위. (1/1000000000초)</summary> 
        NANOSECOND 
    }

    private long frequency;                                        // high-resolution counter의 frequency를 저장하는 변수. 
    private long m_startTick;                                    // 시간을 재기 시작한 순간의 counter 값.

    ///-------------------------------------------------------------------------------------------------------------------------- 
    /// <summary> 
    /// constructor. 
    /// </summary> 
    /// <last_updated>2006-05-16</last_updated> 
    public CStopWatch() { 
        QueryPerformanceFrequency(out frequency); 
        Start(); 
    }

    public CStopWatch(int msec)
    {
        m_tick = msec;
        QueryPerformanceFrequency(out frequency);
        Start(); 
    }

    ///-------------------------------------------------------------------------------------------------------------------------- 
    /// <summary> 
    /// 시간을 재기 시작하는 function. 
    /// </summary> 
    /// <last_updated>2006-05-16</last_updated> 
    public void Start() {
        m_start = true;
        QueryPerformanceCounter(out m_startTick); 
    }

    /// <summary>
    /// 시작 여부를 판단하는 bool. 이미 시작된 경우 true.
    /// </summary>
    /// <returns></returns>
    public bool isStart()
    {
        return m_start;
    }

    ///-------------------------------------------------------------------------------------------------------------------------- 
    /// <summary> 
    /// Start() 함수를 호출한 시각부터 흐른 시간을 반환해주는 function. 
    /// </summary> 
    /// <param name="timeUnit">반환할 시간의 단위.</param> 
    /// <param name="resetTimer">진행된 시간을 반환함과 동시에 타임를 리셋할 것인가를 나타낸다. 만약 타이머를 리셋하면 
    /// 다음번 이 함수를 호출할 때에는 이 함수를 호출한 시점부터 진행된 시간을 반환하게 된다.</param> 
    /// <returns>진행된 시간을 반환한다.</returns> 
    /// <last_updated>2006-05-16</last_updated> 
    public long GetElapsedTime(TIME_UNIT timeUnit, bool resetTimer) { 
        // 현재 카운터 값 구하기. 
        long currentCount; 
        QueryPerformanceCounter(out currentCount);

        // 진행된 시간을 초 단위로 구하기. 
        double elapsedTime = (currentCount - m_startTick) / (double)frequency;

        // 타이머를 리셋하도록 한 경우. 
        if (resetTimer) { 
            m_startTick = currentCount; 
        }

        // 진행된 시간 반환. 
        switch (timeUnit) { 
            case TIME_UNIT.SECOND:                    return (long)(elapsedTime * 1); 
            case TIME_UNIT.MILLISECOND:                return (long)(elapsedTime * 1000); 
            case TIME_UNIT.MICROSECOND:                return (long)(elapsedTime * 1000000); 
            case TIME_UNIT.NANOSECOND:                return (long)(elapsedTime * 1000000000); 
        }

        return 0; 
    }

    public int ms()
    {
        return (int)GetElapsedTime(TIME_UNIT.MILLISECOND, true);
    }

    bool m_start = false;
    int m_tick = 0;

    public void setTime(int msec)
    {
        m_tick = msec;
        Start();
    }

    public bool isElasped()
    {
        if (m_start == false)
            return false;

        int elaspedTime = (int)GetElapsedTime(TIME_UNIT.MILLISECOND, false);

        if (elaspedTime >= m_tick)
            return true;

        return false;
    }

    public void stop()
    {
        m_start = false;
    }

    public void reset()
    {
        long currentCount;
        QueryPerformanceCounter(out currentCount);

        m_startTick = currentCount; 
    }
} 
