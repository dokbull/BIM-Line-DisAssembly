using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum AXM_LEVEL
{
    LOW = 0,
    HIGH,
    UNUSED,
    USED
}

public enum AXM_IN
{
}

public enum AXM_OUT
{
    SERVO_ON = 0x00,
    ALARM_CLEAR = 0x01
}

public enum AXM_Z_PHASE
{
    DISABLE = 0,
    POS = 1,
    NEG = 2
}

public enum AXM_USE
{
    DISABLE = 0,
    ENABLE
}

public enum AXM_SL_HOMEUSE
{
    MASTER = 0x00,
    MASTER_WITH_SLAVE = 0x01,
    MASTER_SLAVE_DIFF_VALUE = 0x02
}

public enum AXM_ENCODER_TYPE
{
    INCREMENTAL = 0,
    ABSOLUTE
}

public enum AXM_ARC_CIRCLE
{
    ARC = 0,
    CIRCLE = 1,
}

public enum AXM_CONTI_AXIS
{
    AXIS2 = 2,
    AXIS3 = 3,
    AXIS4 = 4
}

public class AXM_SOFT_LIMIT
{
    public bool use;
    public AXT_MOTION_STOPMODE stopMode;
    public AXT_MOTION_SELECTION selection;
    public double positivePos;
    public double negativePos;

    public AXM_SOFT_LIMIT()
    {
        stopMode = AXT_MOTION_STOPMODE.EMERGENCY_STOP;
        selection = AXT_MOTION_SELECTION.ACTUAL;

        use = false;
        positivePos = 0.0d;
        negativePos = 0.0d;
    }

    public AXM_SOFT_LIMIT(bool _use, double _positivePos, double _negativePos)
    {
        stopMode = AXT_MOTION_STOPMODE.EMERGENCY_STOP;
        selection = AXT_MOTION_SELECTION.ACTUAL;

        use = _use;
        positivePos = _positivePos;
        negativePos = _negativePos;
    }

    public void setValue(AXM_SOFT_LIMIT src)
    {
        use = src.use;
        stopMode = src.stopMode;
        selection = src.selection;

        positivePos = src.positivePos;
        negativePos = src.negativePos;
    }
}

public class AXM_GEAR_RATIO
{
    public uint masterValue;
    public double slaveValue;
}

public class AXM_ELECTRIC_GEAR_RATIO
{
    public int numerator;
    public int denominator;
}

public enum AXM_ACCEL_UNIT
{
    UNIT_SEC = 0,
    SEC = 1
}

public class AXM_GANTRY_INFO
{
    public AXM_SL_HOMEUSE homeUse;
    public double offset;
    public double offsetRange;
}