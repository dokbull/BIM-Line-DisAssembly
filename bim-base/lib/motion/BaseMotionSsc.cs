using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#pragma warning disable CS0414

public abstract class BaseMotionSsc
{
    public abstract void init();
    public abstract bool isConnected();

    public abstract void run();
}

public class MotionAxisSsc
{
    double nowPos;

    bool alarm;
    int alarmCode;

    bool lowLimit;
    bool highLimit;

    bool emergency;

    public MotionAxisSsc()
    {
        nowPos = 0.0f;

        alarm = false;
        alarmCode = 0;

        lowLimit = false;
        highLimit = false;

        emergency = false;
    }
}

#pragma warning restore CS0414