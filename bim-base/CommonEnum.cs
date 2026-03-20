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

// Motor Delay List
public enum MOTOR_DELAY
{
    MOLD_LD_CV_RUN = 3,
    ALIGN_CV_RUN,
    MOLD_ULD_CV_RUN,
    UB_ULD_CV_RUN,
    UB_CV_1_RUN,
    UB_CV_2_RUN,
    MOLD_CV_1_RUN,
    MOLD_CV_2_RUN,
    MOLD_CV_3_RUN,
    MOLD_CV_4_RUN,
    MOLD_CV_5_RUN,
}

// Cylinder Delay List
public enum CYLINDER_DELAY
{
    //UB_PP_Z_UP = 0,
    //UB_PP_Z_DOWN,
    //UB_PP_Y_FWD,
    //UB_PP_Y_BWD,
    //MOLD_PP_ZL_UP,
    //MOLD_PP_ZL_DOWN,
    //MOLD_PP_ZR_UP,
    //MOLD_PP_ZR_DOWN,
    //MOLD_PP_X_FWD,
    //MOLD_PP_X_BWD,
    //BASE_X_FWD,
    //BASE_X_BWD,
    //IN_PP_Z_UP,
    //IN_PP_Z_DOWN,
    //IN_PP_Y_FWD,
    //IN_PP_Y_BWD,
}

// Vacuum Delay List
public enum VACUUM_DELAY
{
    //UB_PP_Z_VAC_ON = 0,
    //UB_PP_Z_VAC_OFF,
    //MOLD_PP_ZL_VAC_ON,
    //MOLD_PP_ZL_VAC_OFF,
    //MOLD_PP_ZR_VAC_ON,
    //MOLD_PP_ZR_VAC_OFF,
    //IN_PP_Z_VAC_ON,
    //IN_PP_Z_VAC_OFF,
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