using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum PAIX_RET
{
    NMC_CANNOT_APPLY         = -18,  /*!< 현재 진행 중인 모션에서 지원하지 않는 명령어를 보냈을 경우 */
    NMC_NO_CONSTSPDDRV       = -17,  /*!< 정속 구간이 아닌 가속,감속 중 명령어가 입력된 경우 */
    NMC_SET_1ST_SPDPPS       = -16,  /*!< 속도 프로파일을 먼저 입력하십시오 */
    NMC_CONTI_BUF_FULL       = -15,  /*!< 무제한 연속보간의 버퍼가 모두 채워진 상태 */
    NMC_CONTI_BUF_EMPTY      = -14,  /*!< 무제한 연속보간의 버퍼에 데이터가 없는 상태 */
    NMC_INTERPOLATION        = -13,  /*!< 연속(일반) 보간 구동 중에 동작 명령어가 입력된 경우 */
    NMC_FILE_LOAD_FAIL       = -12,  /*!< F/W file 로드 실패 */
    NMC_ICMP_LOAD_FAIL       = -11,  /*!< ICMP.dLL 로드 실패, nmc_PingCheck 호출시 발생. PC에 DLL유무를 확인 */
    NMC_NOT_EXISTS           = -10,  /*!< 네트워크 장치가 식별되지 않는 경우, 방화벽이나 연결 상태를 확인 */
    NMC_CMDNO_ERROR          = -9,   /*!< 함수 호출 시, 식별코드에 오류 발생 */
    NMC_NOTRESPONSE          = -8,   /*!< 함수 호출 시, 응답이 없는 경우 */
    NMC_BUSY                 = -7,   /*!< 현재 축이 구동 중인 경우 */
    NMC_COMMERR              = -6,   /*!< 함수 호출 시, 통신 오류 발생 */
    NMC_SYNTAXERR            = -5,   /*!< 함수 호출 시, 구문 오류 발생 */
    NMC_INVALID              = -4,   /*!< 함수 인자값에 오류발생 */
    NMC_UNKOWN               = -3,   /*!< 지원되지 않는 함수 호출 */
    NMC_SOCKINITERR          = -2,   /*!< 소켓 초기화 실패 */
    NMC_NOTCONNECT           = -1,   /*!< 장치와 연결이 끊어진 경우 */
    NMC_OK                   = 0,    /*!< 정상 */
}

public enum PAIX_STOPMODE
{
    DEACCELACTION = 0,
    EMERGENCY = 1
}

public enum PAIX_STOPMODE_NMCX
{
    EMERGENCY = 0,
    DEACCELACTION = 1,
}

public enum PAIX_LEVEL
{
    LOW = 0,
    HIGH = 1
}

public enum PAIX_SW_LIMIT
{
    COMMAND = 0,
    ENCODER = 1
}

public enum PAIX_ENCODER_DIR
{
    A_B = 0,
    B_A = 1,
    UP_DOWN = 2,
    DOWN_UP = 3
}

public enum PAIX_ENCODER_COUNT
{
    COUNT_4 = 0,
    COUNT_2 = 1,
    COUNT_1 = 2
}

public enum PAIX_PULSE_LOGIC
{
    PULSE2_L_CW_CCW = 0,
    PULSE2_L_CCW_CW = 1,
    PULSE2_H_CW_CCW = 2,
    PULSE2_H_CCW_CW = 3,
    PULSE1_L_CW_CCW = 4,
    PULSE1_L_CCW_CW = 5,
    PULSE1_H_CW_CCW = 6,
    PULSE1_H_CCW_CW = 7,
}

public enum PAIX_MOVE
{
    REL_MOVE = 0,
    ABS_MOVE = 1
}

public enum PAIX_DIR
{
    CW = 0,
    CCW = 1
}

public enum PAIX_HOME_MODE
{
    POS_LIMIT = 0,
    NEG_LIMIT = 1,
    CCW_NEAR = 2,
    CW_NEAR = 3,
    CCW_Z = 4,
    CW_Z =5,
    POS_LIMIT_Z = 0x80,
    NEG_LIMIT_Z = 0x81,
    CCW_NEAR_Z = 0x82,
    CW_NEAR_Z = 0x83
}

public enum PAIX_HOME_END_BIT
{
    OFFSET_END_CMD = 1,
    OFFSET_END_ENC = 2,
    OFFSET_START_CMD = 4,
    OFFSET_START_ENC = 8
}

public enum PAIX_ACCEL_UNIT
{
    MM_PER_SECOND = 0,
    SECOND
}

public class PAIX_HOME_AUTOCANCEL
{
    public bool alarm;
    public bool servoOff;
    public bool currentOff;
    public bool servoReady;
}

public class PAIX_GANTRY_INFO
{
    public bool[] use = new bool[2];
    public short[] masterAxisNo = new short[2];
    public short[] slaveAxisNo = new short[2]; 
}

#if false
public struct NMCCONTISTATUS
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public  short [] nContiRun;                 /*<! 연속 보간 실행 여부(0=Stop, 1=Run) */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public  short [] nContiWait;                /*<! Queue상태(0=Queue에 실행 할 Data가 있음, \n
                                                                       1=큐버퍼의 노드를 모두 소진하여 다음 노드 전송을 대기) */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public  short [] nContiRemainBuffNum;       /*<! 비어 있는 큐 버퍼의 수(0 ~ 1024) */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public  uint  [] uiContiExecutionNum;       /*<! 실행중인 큐 버퍼의 위치(0 ~ 4,294,967,295) */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public  short [] nContiStopReason;          /*<! 실행중인 연속 보간의 정지 원인 (E-Version only) */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public  short [] nDummy;                    /*<! 예비공간 */
        };
#endif

public class PAIX_CONTI_STATUS_DATA
{
    public bool isRun = false;
    public bool isWait = false;
    public int remainBufferCount = 0;
    public int nowNodeNo = 0;
    public int stopReadon = 0;
}

public class PAIX_CONTI_STATUS
{
    public PAIX_CONTI_STATUS_DATA[] group = new PAIX_CONTI_STATUS_DATA[2];

    public PAIX_CONTI_STATUS()
    {
        group[0] = new PAIX_CONTI_STATUS_DATA();
        group[1] = new PAIX_CONTI_STATUS_DATA();
    }
}