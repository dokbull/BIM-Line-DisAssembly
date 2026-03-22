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
}

// Motor Delay List
public enum MOTOR_DELAY
{
    // LD CV
    MOLD_LD_CV_RUN = 3,

    // ALIGN CV
    ALIGN_CV_RUN,

    // ULD CV
    MOLD_ULD_CV_RUN,
    UB_ULD_CV_RUN,

    // UB CV
    UB_CV_1_RUN,
    UB_CV_2_RUN,

    // MOLD CV
    MOLD_CV_1_RUN,
    MOLD_CV_2_RUN,
    MOLD_CV_3_RUN,
    MOLD_CV_4_RUN,
    MOLD_CV_5_RUN,
}

// Cylinder Delay List
public enum CYLINDER_DELAY
{
    // ALIGN CV
    ALIGN_CV_ALIGN_FWD = 14,
    ALIGN_CV_ALIGN_BWD,
    ALIGN_CV_MOLD_UP,
    ALIGN_CV_MOLD_DOWN,

    // MOLD IN PP
    MOLD_IN_PP_GRIP,
    MOLD_IN_PP_UNGRIP,

    // MOLD IN REVERSE
    MOLD_IN_REVERSE_TURN,
    MOLD_IN_REVERSE_RETURN,
    MOLD_IN_REVERSE_GRIP,
    MOLD_IN_REVERSE_UNGRIP,

    // MOLD SHUTTLE
    MOLD_SHUTTLE_UP,
    MOLD_SHUTTLE_DOWN,
    MOLD_SHUTTLE_UNLOCK_FWD_1,
    MOLD_SHUTTLE_UNLOCK_BWD_1,
    MOLD_SHUTTLE_UNLOCK_FWD_2,
    MOLD_SHUTTLE_UNLOCK_BWD_2,
    MOLD_SHUTTLE_PUSHER_FWD_1,
    MOLD_SHUTTLE_PUSHER_BWD_1,
    MOLD_SHUTTLE_PUSHER_FWD_2,
    MOLD_SHUTTLE_PUSHER_BWD_2,

    // UB OUT PP
    UB_OUT_PP_FWD,
    UB_OUT_PP_BWD,

    // UB OUT REVERSE
    UB_OUT_REVERSE_UP_1,
    UB_OUT_REVERSE_DOWN_1,
    UB_OUT_REVERSE_UP_2,
    UB_OUT_REVERSE_DOWN_2,
    UB_OUT_REVERSE_TURN_1,
    UB_OUT_REVERSE_RETURN_1,
    UB_OUT_REVERSE_TURN_2,
    UB_OUT_REVERSE_RETURN_2,

    // MOLD OUT PP
    MOLD_OUT_PP_GRIP_1,
    MOLD_OUT_PP_UNGRIP_1,
    MOLD_OUT_PP_GRIP_2,
    MOLD_OUT_PP_UNGRIP_2,

    // MOLD CV 3
    MOLD_CV_3_TURN,
    MOLD_CV_3_RETURN,

    // MOLD ULD CV
    MOLD_ULD_CV_ALIGN_FWD,
    MOLD_ULD_CV_ALIGN_BWD,
    MOLD_ULD_CV_UP,
    MOLD_ULD_CV_DOWN,

    // MOLD NG OUT
    MOLD_NG_OUT_UNLOCK_FWD_1,
    MOLD_NG_OUT_UNLOCK_BWD_1,
    MOLD_NG_OUT_UNLOCK_FWD_2,
    MOLD_NG_OUT_UNLOCK_BWD_2,
}

// Vacuum Delay List
public enum VACUUM_DELAY
{
    // ALIGN CV
    ALIGN_CV_VAC = 54,

    // UB OUT PP
    UB_OUT_PP_VAC,

    // UB OUT REVERSE
    UB_OUT_REVERSE_VAC_1,
    UB_OUT_REVERSE_VAC_2,
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