using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum AXIS
{
    UB_PP_Z,
    UB_PP_Y,
    MOLD_PP_ZL,
    MOLD_PP_ZR,
    MOLD_PP_X,
    BASE_X,
    IN_PP_Z,
    IN_PP_Y,
    MAX,
}

public enum PP_TARGET
{
    READY,
    PICK,
    COMPLETE,
}

enum MES_SUFFIX
{
    CRLF,
    CR,
    NONE,
}

public enum ACT
{
    NONE,

    // PP
    GRIP,
    UNGRIP,
    WAIT
}

/// <summary>
/// Unit 별 10 개 정도의 Buffer 를 가지고 Index 생성 할 것
/// </summary>
public enum DELAY
{
    PP_GRIP = 0,
    PP_UNGRIP,
    PP_TIMEOUT,

    MAX = 512,
}

public enum JOG_SPEED
{
    LOW = 0,
    MIDDLE,
    HIGH,
}

public enum MACHINE_STATE
{
    AUTO,
    DRY_RUN,
    BYPASS,
}

public enum LAMP_STATE
{
    RED,
    YELLOW,
    GREEN,
    NONE,
}
public enum PAGE
{
    AUTO = 0,
    TEACH,
    TEACH_IN_PP,
    TEACH_TRAY_PP,
    TEACH_OUT_PP,
    DATA,
    DATA_MODEL,
    DATA_SYSTEM,
    DATA_MOTOR_VEL,
    DATA_JOG_VEL,
    DATA_PORT,
    LOG,
    MAX,
}