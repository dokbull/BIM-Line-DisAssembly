using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if false
abstract class NutRunner
{
    public abstract void start();

    public abstract bool jobEnd();
    public abstract bool result();
}

abstract class NutRunnerResult
{
    public bool result;

    public double torque;
    public double angle;
}

class NutRunnerDaichiResult : NutRunnerResult
{
    public double result1;
    public double result2;
}

class NutRunnerDaichi : NutRunner
{
    public override void start()
    {
        throw new NotImplementedException();
    }

    public override bool jobEnd()
    {
        throw new NotImplementedException();
    }

    public override bool result()
    {
        throw new NotImplementedException();
    }
}
#endif