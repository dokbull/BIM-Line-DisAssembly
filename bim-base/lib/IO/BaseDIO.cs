using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class BaseDIO
{
    public abstract bool[] allInput();
    public abstract bool[] allOutput();

    public abstract bool input(int index);
    public abstract bool output(int index);

    public abstract void setOutput(int index, bool value);
    public abstract void setOutput(bool[] valueArray);
}

#if false // example code
    bool[] allInput();
#endif
