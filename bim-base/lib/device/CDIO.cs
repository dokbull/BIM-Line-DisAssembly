using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CDIO : DIO
{
    public CDIO(int inputCount, int outputCount) : base(inputCount, outputCount)
    {
    }

    public override bool input(int index)
    {
        int idx = index;

        if (idx > 0x100)
            idx = idx - (0x100) + 0x40;

        return base.input(idx);
    }

    public override bool output(int index)
    {
        int idx = index;

        if (idx > 192)
            idx -= 192;

        return base.output(idx);
    }

    public override void setOutput(int index, bool value)
    {
        int idx = index;

#if false
        switch ((OUTPUT)index)
        {
            case OUTPUT.towerLamp_R:
            case OUTPUT.towerLamp_G:
            case OUTPUT.towerLamp_Y:
            case OUTPUT.towerLampBuzz:
                break;

            default:
                //Debug.debug("CDIO::setOutput index:" + (OUTPUT)index + " value:" + value);

                break;
        }
#endif

        if (idx > 192)
            idx -= 192;

        base.setOutput(idx, value);
    }
}
