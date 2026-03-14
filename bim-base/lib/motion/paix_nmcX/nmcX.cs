/**
  **************************************************************************************************
  * @file       nmcX.cs
  * @author     PAIX Lab.
  * @version
  * @date       2019-11-08
  * @brief      본 파일은 NMC-XR Controller를 사용하기 위한 함수파일입니다.
  * @note       C#
  * @copyright  2018. PAIX Co., Ltd. All rights reserved.
  * @see        http://www.paix.co.kr/
  **************************************************************************************************
  * @note       2018-09-20  최초작성
  *             2019-02-11  1차개정 (함수명 수정)
  *             2019-04-10  함수추가 (nmcX_HomeDoneCancel)
  *             2019-05-23  Modbus Protocol 변경, RDO 출력유지 추가
  *             2019-08-22  PP모드 제어함수 추가(nmcX_PP_SetTPosSpeed, nmcX_mPP_SetTPosSpeed)
  *             2019-08-30  SW Limit 설정 확인 함수 추가(nmcX_SwLimitGet, nmcX_mSwLimitGet)
  *             2019-10-08  MPG Counter기능 추가로 nmcX_MPGSetCfg->nmcX_MpgCfgSet으로 변경 및 nmcX_MpgCfgGet,nmcX_MpgGetCounter추가
  *             2019-11-08  nmcX_R_UNMOVABLE_SWLIMIT(-2065) Error code 추가
  *             2021-07-27  전환 기능 입력신호 관련 기능 추가
  **************************************************************************************************
  * @warinig    2019-05-23  Protocol 변경으로 인하여, 이전 DLL, F/W와 호환되지 않습니다.
  **************************************************************************************************
  */
using System.Runtime.InteropServices;     // DLL support
namespace PAIX_RTEX_MotionController
{
  public class NMCXR
  {
    /**********************************************************************************************/
    /* Defines                                                                                    */
    /**********************************************************************************************/
    /**
      * @brief  장치 정의값
      */
    public const short MAX_DEV          = 255;                          /*!< 네트워크 최대 장치 수량 */
    public const short MAX_SLAVE        = 32;                           /*!< 장치당 최대 Slave 수량 */
    public const short MAX_MDIO_PINS    = 8;                            /*!< MDIO Pin 수량 (Controller 내장 IO) */
    public const short MAX_RDIO_PINS    = 32;                           /*!< RTEX DIO Pin 수량 (Slave IO 장치) */

    public const short CP_MAX_GROUP     = 4;                            /*!< 최대 CP Group 수 */
    public const short CP_GROUP_AXES    = 4;                            /*!< CP 1Group당 축 수 */
    public const short CP_MAX_AXES      = (CP_GROUP_AXES*CP_MAX_GROUP); /*!< CP 최대 축수(CP 1Group당 축수 X 최대 Chip수) */

    public const short MAX_I_MAP        = CP_MAX_AXES;                  /*!< 최대 보간 Map 수(CP최대 축수와 동일) */
    public const short MAX_I_AXES       = CP_MAX_AXES;                  /*!< 최대 보간 축 수(CP최대 축수와 동일) */
    public const short MAX_SEQ          = 4;                            /*!< 최대 시퀸스 수행개수 */

    /**********************************************************************************************/
    /**
      * @brief  함수 반환값
      */
    public enum EnmcX_FUNC_RESULT : short
    {
      nmcX_R_OK                       =     0,  /*!< 함수 수행성공 */
      nmcX_R_CONN_ERR                 =    -1,  /*!< 장치 열기 실패 */
      nmcX_R_INVALID_XNO              =    -2,  /*!< 유효하지 않은 NMCX장치번호 */
      nmcX_R_NOT_CONNECT              =    -3,  /*!< 연결되어 있지 않음 */
      nmcX_R_INVALID_SOCKET           =    -4,  /*!< 유효하지 않은 소켓 */
      nmcX_R_SOCKET_ERROR             =    -5,  /*!< 소켓 에러 */
      nmcX_R_SOCKET_TX_TMO            =    -6,  /*!< 소켓 데이터 전송중 Time-out */
      nmcX_R_SOCKET_RX_TMO            =    -7,  /*!< 소켓 데이터 수신중 Time-out */
      nmcX_R_SYNCHRONIZE              =    -8,  /*!< 동기화 오류(뮤텍스,세마포어등에서 대기오류) */
      nmcX_R_BUF_OVERFLOW             =    -9,  /*!< 지정 Buffer크기를 초과(ex.수신버퍼 초과) */
      nmcX_R_MODBUS_RESP_NONE         =   -10,  /*!< MODBUS TCP응답 포맷이 아닌경우 */
      nmcX_R_MODBUS_RESP_ERR          =   -11,  /*!< MODBUS TCP응답이 에러코드일 경우 */
      nmcX_R_MODBUS_RESP_UNDEF        =   -12,  /*!< MODBUS TCP지정되지 않은 응답 */
      nmcX_R_MODBUS_INVALID_UID       =   -13,  /*!< MODBUS TCP의 Unit ID가 유효하지 않음(IP마지막=UnitID인데 ID가 다른경우) */

      nmcX_R_FUNC_UNDEF               = -1000,  /*!< 정의 되어 있지 않은 Function Code */
      nmcX_R_FUNC_FAIL                = -1001,  /*!< 함수수행에 실패한 경우 */
      nmcX_R_FUNC_BUSY                = -1002,  /*!< 함수수행이 Busy한 경우(예:Serial(RS232)전송시 Busy) */
      nmcX_R_FUNC_TMO                 = -1003,  /*!< 함수수행의 Timeout발생된 경우 */
      nmcX_R_FUNC_NOT_ENOUGH          = -1004,  /*!< 함수별 지정된 길이만큼 전송/수신하여야 하나 부족한 경우(예:수행 결과로 수신한 응답 데이터가 원하는 길이보다 부족한 경우) */
      nmcX_R_FUNC_RESP_INVALID        = -1005,  /*!< 함수수행의 결과로 수신한 데이터가 적절치 않은경우(예:0~100사이의 값으로 수신되여야 하는경우인데 1000이 수신된 경우) */

      nmcX_R_FUNC_ARG_NONE            = -1698,  /*!< 함수 인자가 없는 경우 */
      nmcX_R_FUNC_ARG_RNG_OVER        = -1699,  /*!< 함수 인자가 범위를 초과한 경우 */
      nmcX_R_FUNC_ARG_ERR             = -1700,  /*!< 함수 인자에 오류가 있는 경우
                                      ~ -1799        인자의 순서대로 에러코드가 증가함. 예) 3번째 인자 오류 시 (-1703) */

      nmcX_R_NOT_LINKED               = -2001,  /*!< Slave Ring Link가 성립되지 않음 */
      nmcX_R_INVALID_ID               = -2002,  /*!< ID 범위를 벗어나거나 유효하지 않음 */
      nmcX_R_DUPLIATE_ID              = -2003,  /*!< 중복된 ID가 있는 경우(예:Map지정시 중복된 Slave ID가 있는 경우) */
      nmcX_R_SLAVE_CNT_OVER           = -2004,  /*!< Slave Count수량이 초과한 경우 */
      nmcX_R_INVALID_CP_GROUP         = -2005,  /*!< CP Group범위를 벗어나거나 유효하지 않음 */
      nmcX_R_INVALID_CP_AXIS          = -2006,  /*!< CP Axis범위를 벗어나거나 유효하지 않음 */
      nmcX_R_TYPE_SERVO_ERR           = -2007,  /*!< Slave가 없거나 서보 드라이버가 아님(예:Slave가 DIO인데 ServoON명령 전송) */
      nmcX_R_TYPE_RDI_ERR             = -2008,  /*!< Slave가 없거나 RTEX Digital Input이 아님(예:Slave가 Servo인데 DI명령 전송) */
      nmcX_R_TYPE_RDO_ERR             = -2009,  /*!< Slave가 없거나 RTEX Digital Output가 아님(예:Slave가 Servo인데 DO명령 전송) */
      nmcX_R_TYPE_MDIO_ERR            = -2010,  /*!< MDIO가 없는 제품모델 */

      nmcX_R_CM_ONLY_CP               = -2020,  /*!< 컨트럴 모드:CP모드에서만 수행할 수 있음(예:보간운전, 리스트 모션) */
      nmcX_R_CM_ONLY_PP               = -2021,  /*!< 컨트럴 모드:PP모드에서만 수행할 수 있음 */
      nmcX_R_CM_ONLY_CV               = -2022,  /*!< 컨트럴 모드:CV모드에서만 수행할 수 있음 */
      nmcX_R_CM_ONLY_CT               = -2023,  /*!< 컨트럴 모드:CT모드에서만 수행할 수 있음 */
      nmcX_R_CM_ONLY_CP_PP            = -2024,  /*!< 컨트럴 모드:CP,PP모드에서는 수행할 수 있음 */
      nmcX_R_CM_CHG_FAIL              = -2025,  /*!< 컨트럴 모드 변경실패 */
      nmcX_R_CP_FULL_PREREG           = -2026,  /*!< 컨트럴 모드:CP에서 Pre-Register가 가득찬 경우 */

      nmcX_R_S_ON_FAIL_ALARM          = -2030,  /*!< 알람상태여서 서보ON실패 */
      nmcX_R_S_ON_FAIL_RUN            = -2031,  /*!< 모터가 RUN중이어서 서보ON실패 */
      nmcX_R_MOV_FAIL_RUN             = -2032,  /*!< 모터가 RUN중이어서 이동실패 */

      nmcX_R_MOVING                   = -2040,  /*!< 이동중입니다. */
      nmcX_R_MOVING_X                 = -2041,  /*!< 이동중이 아닌경우(예:이동중이 아닌데 속도 Override) */
      nmcX_R_NOT_INP                  = -2042,  /*!< In-Postion상태가 아닙니다. */
      nmcX_R_HOMING                   = -2043,  /*!< 원점이동중입니다. */
      nmcX_R_CONTI_RUN                = -2044,  /*!< 연속보간 실행중입니다. */
      nmcX_R_LMOT_RUN                 = -2045,  /*!< 리스트 모션 실행중입니다. */

      nmcX_R_UNMOVABLE_EMG            = -2060,  /*!< Emergency로 이동이 불가능한 상태 */
      nmcX_R_UNMOVABLE_LIMIT          = -2061,  /*!< ±Limit센서로 이동이 불가능한 상태 */
      nmcX_R_UNMOVABLE_ALARM          = -2062,  /*!< Alarm신호 입력으로 이동이 불가능한 상태 */
      nmcX_R_UNMOVABLE_TLIMIT         = -2063,  /*!< Torque Limit로 이동이 불가능한 상태 */
      nmcX_R_UNMOVABLE_HOME           = -2064,  /*!< Home센서 정지로 이동이 불가능한 상태 */
      nmcX_R_UNMOVABLE_SWLIMIT        = -2065,  /*!< ± S/W Limit센서로 이동이 불가능한 상태 */

      nmcX_R_SERVO_OFF                = -2080,  /*!< 서보OFF상태여서 이동 및 설정이 불가능한 상태 */
      nmcX_R_SERVO_ON                 = -2081,  /*!< 서보ON상태여서 각종 설정이 불가능한 상태 */
      nmcX_R_SERVO_RESP_NONE          = -2082,  /*!< 서보에서 명령에 응답이 없습니다. */
      nmcX_R_SERVO_CMD_ERR            = -2083,  /*!< 서보에서 Command Error발생 */
      nmcX_R_SERVO_CMD_NONE           = -2084,  /*!< 서보에서 Command에 대한 응답이 없습니다. */
      nmcX_R_ALARM_CAN_NOT_CLEAR      = -2085,  /*!< Alarm Clear가 되지 않는 알람인 경우 */
      nmcX_R_ALARM_IS_NOT             = -2086,  /*!< Alarm상태가 아닌경우(예:알람상태가 아닌데 Alarm Clear시) */
      nmcX_R_ENC_ABS                  = -2087,  /*!< Absolute Encoder는 수행할 수 없음(예:위치변경) */
      nmcX_R_ENC_INC                  = -2088,  /*!< Incremental Encoder는 수행할 수 없음(예:Clear multi-turn data) */

      nmcX_R_INVALID_SPEED            = -3003,  /*!< 속도를 설정되어 있지 않거나 유효하지 않습니다.(예:Profile 0=사다리꼴, 1=S-Curve) */
      nmcX_R_NO_COND_POS_OVER         = -3004,  /*!< 위치 오버라이드를 수행할 수 없는 조건입니다. */
      nmcX_R_DRV_SPEED_OVER           = -3005,  /*!< 구동속도를 초과(예:속도Override시에 최초 지정된 구동속도를 초과한 경우) */
      nmcX_R_HOME_1ST_INVALID_SPEED   = -3006,  /*!< 원점 1차이동 속도가 유효하지 않음 */
      nmcX_R_HOME_2ND_INVALID_SPEED   = -3007,  /*!< 원점 2차이동 속도가 유효하지 않음 */
      nmcX_R_HOME_3RD_INVALID_SPEED   = -3008,  /*!< 원점 3차이동 속도가 유효하지 않음 */
      nmcX_R_HOME_OFF_INVALID_SPEED   = -3009,  /*!< 원점 Offset이동 속도가 유효하지 않음 */
      nmcX_R_INVALID_T_LIMIT          = -3010,  /*!< Torque Limit설정이 올바르지 않음(예:원점수행을 Stopper로 설정하고 Torque Limit설정이 이루어 지지 않은 경우) */

      nmcX_R_NOT_ENOUGH_PULSE         = -3021,  /*!< 펄스가 부족한 경우(예:속도Override시 남은 펄스수는 적은데 속도를Up하려고 하면) */
      nmcX_R_NO_ABS_MOV               = -3022,  /*!< 절대 위치 이동이 아닙니다.(예:PP모드의 경우 위치 오버라이드는 절대 위치 이동일 경우에만 가능) */
      nmcX_R_INVALID_SPEED_ACC        = -3023,  /*!< 가속이 0이하 이거나 지정된 범위를(PP:655350000) 초과한 경우 */
      nmcX_R_INVALID_SPEED_DEC        = -3024,  /*!< 감속이 0이하 이거나 지정된 범위를(PP:655350000) 초과한 경우 */
      nmcX_R_INVALID_SPEED_DRV        = -3025,  /*!< 구동속도가 0또는 음수이거나 모터의 최고속도를 초과한경우(모델별 상이) */
      nmcX_R_INVALID_SPEED_START      = -3026,  /*!< 시작 속도가 구동속도보다 큰 경우 */
      nmcX_R_INVALID_SPEED_STOP       = -3027,  /*!< 정지 속도가 구동속도보다 큰 경우 */

      nmcX_R_ARC_INVALID              = -3040,  /*!< 원호의 입력인자에 오류가 있어 원호를 구성할수 없습니다.(예:통과점이 원호에 있지 않을 경우) */
      nmcX_R_HELI_U_AXIS_NOT_ALLOC    = -3041,  /*!< 헨리컬 보간시 CP U축은 Dummy축이므로 Slave를 할당하면 안됨 */

      nmcX_R_EMPTY_NODE               = -3050,  /*!< 등록된 노드가 없습니다.(예:리스트 모션에 노드가 등록되어 있지 않음) */

      nmcX_R_MIRR_NONE_MAIN           = -3060,  /*!< Mirror Main이 없는 경우(예:Sub으로 지정하였으나 Main이 없는 경우(서보ON상태이면 발생)) */
      nmcX_R_MIRR_CAN_NOT_SUB         = -3061,  /*!< Mirror Sub축은 실행할 수 없는 기능의 경우(예:Sub에 원점이동을 지정 경우) */
      nmcX_R_MIRR_MAIN_DUPLICATE      = -3062,  /*!< Mirror Main이 중복된 경우(Mirror이 아닌경우도 포함) */
      nmcX_R_MIRR_MOVING              = -3063,  /*!< Mirror로 지정 Slave가 이동중인 경우(예:이동중인데 Mirror의 Sub으로 변경) */

      nmcX_R_DO_PIN_RANGE_OVER        = -3080,  /*!< DO에 지정된 Pin번호가 Range Over일 경우 */

      nmcX_R_IMAP_DUPLIATE_CP         = -4000,  /*!< 보간 MAP설정시 지정된 Slave가 CP가 같은 그룹과 같은 축일경우 */
      nmcX_R_IMAP_NEED_COUNT          = -4001,  /*!< 보간 MAP설정시나 보간수행에 필요한 Slave개수가 부적합시(예:헨리컬=3, 원호=2, 설정안함) */
      nmcX_R_IMAP_INVALID_CP_GROUP    = -4002,  /*!< 보간 MAP설정시 유효하지 않은 CP그룹일 경우(예:헨리컬,원호시 같은 그룹이 아닐경우) */
      nmcX_R_IMAP_INVALID_CP_AXIS     = -4003,  /*!< 보간 MAP설정시 유효하지 않은 CP축을 지정 경우(예:헨리컬시 각 그룹의 U축은 지정하면 안됨) */
      nmcX_R_IMAP_INVALID_SET         = -4004,  /*!< 보간 MAP설정의 설정에 오류가 있는 경우(예:Map에 Slave들이 지정되지 않은 경우등) */

      nmcX_R_SEQ_BUF_FULL             = -5000,  /*!< 시퀸스 버퍼가 가득찬 경우 */
      nmcX_R_SEQ_BUF_EMPTY            = -5001,  /*!< 시퀸스 버퍼가 비어있는 경우 */
      nmcX_R_SEQ_ALREADY_RUN          = -5002,  /*!< 시퀸스 이미 실행중인 경우 */
      nmcX_R_SEQ_ALREADY_PAUSE        = -5003,  /*!< 시퀸스 이미 일시중지인 경우 */
      nmcX_R_SEQ_ALREADY_STOP         = -5004,  /*!< 시퀸스 이미 정지된 경우 */
      nmcX_R_SEQ_NOT_RUN              = -5005,  /*!< 시퀸스 실행중이 아닌 경우 */
      nmcX_R_SEQ_NOT_PAUSE            = -5006,  /*!< 시퀸스 일시중지 중이 아닌 경우 */
      nmcX_R_SEQ_STOP_REASON          = -5007,  /*!< 시퀸스 정지원인에 의한 정지 */
      nmcX_R_SEQ_WAIT                 = -5008,  /*!< 시퀸스 수행 대기 */
    };

    /**
      * @brief  RTEX Control Mode
      * @note   RTEX Servo의 제어모드
      * @see    nmcX_ServoPropertyCfg, TnmcX_SLAVE_PROPERTY
      */
    public enum ESlaveCtrlMode : short
    {
      cmNOP,
      cmPP,           /*!< Profile Position */
      cmCP,           /*!< Cyclic Position */
      cmCV,           /*!< Cyclic Velocity */
      cmCT,           /*!< Cyclic Torque */
      cmCount
    };
    //------------------------------------------------------------------------------

    /**
      * @brief  RTEX Kind of Slave Node
      * @note   NMC-XR의 Slave로 연결되는 장치의 구분
      * @see    TnmcX_SLAVE_PROPERTY
      */
    public enum ESlaveKind : short
    {
      skNone,
      skServo,        /*!< RTEX Servo */
      skInput,        /*!< RTEX Digital Input */
      skOutput,       /*!< RTEX Digital Output */
      skCount,
    };
    //------------------------------------------------------------------------------

    /**
      * @brief  Mirror Mode
      * @see    nmcX_ServoPropertyCfg, TnmcX_SLAVE_PROPERTY
      */
    public enum EMirror : short
    {
      mirrNone,       /*!< 사용X */
      mirrMain,       /*!< Main */
      mirrSub,        /*!< Sub */
      mirrCount,
    };
    //------------------------------------------------------------------------------

    /**
      * @brief  위치지정 형태
      * @note   이동함수에서 사용되는 위치지정 모드
      * @see    nmcX_ILinear, nmcX_IArc, nmcX_IHelical
      */
    public enum EPosType : short
    {
      ptRelative,     /*!< 상대위치 */
      ptAbsolute,     /*!< 절대위치 */
      ptNonePos,
    };
    //------------------------------------------------------------------------------

    /**
      * @brief  회전 방향
      * @note   이동함수의 이동방향 및 원호의 회전 방향
      * @see    nmcX_MoveVel, nmcX_IArcCE, nmcX_IHelicalCE
      */
    public enum EDirection : short
    {
      dirCW,          /*!< 정방향 (+) */
      dirCCW,         /*!< 역방향 (-) */
      dirCount,
    };
    //------------------------------------------------------------------------------

    /**
      * @brief  정지모드
      * @note   목적위치의 이동완료 등 각 상황에 따른 정지모드
      * @see    nmcX_mStop, nmcX_SearchMove, nmcX_StopModeSwLimit
      */
    public enum EStopMode : short
    {
      stopSudden,     /*!< 긴급정지 */
      stopDec,        /*!< 감속정지 */
      stopNone,       /*!< 사용안함 */
      stopCount,
    };
    //------------------------------------------------------------------------------

    /**
      * @brief  정지원인
      * @note   Servo가 정지하였을 때의 정지 원인
      * @see    nmcX_GetAllStatus, nmcX_GetServoStatus, TnmcX_SERVO_STATUS.nStopReason
      */
    public enum EStopReason : short
    {
      srNone,
      srHome,             /*!<  1 = Home 센서 신호 정지 */
      srZ,                /*!<  2 = Z상 신호 정지 */
      srServoOff,         /*!<  3 = Servo ON 신호 Off 정지 */
      srNOT,              /*!<  4 = -Limit 센서 신호 정지 */
      srPOT,              /*!<  5 = +LImit 센서 신호 정지 */
      srAlarm,            /*!<  6 = Alarm 신호 정지 */
      srSwNOT,            /*!<  7 = SW -Limit 신호 정지 */
      srSwPOT,            /*!<  8 = SW +Limit 신호 정지 */
      srEStop,            /*!<  9 = 비상 정지 신호 정지 */
      srTLimit,           /*!< 10 = 토크 Limit 신호 정지 */
      srConnChkTmOver,    /*!< 11 = 통신연결 체크 시간 경과 */
      srReset,            /*!< 12 = Reset에 의한 정지 */
      srUser,             /*!< 13 = 사용자 정지 */
      srMpgBufOver,       /*!< 14 = MPG PA/PB입력용 Buffer Counter overflow */
      srFuncFail,         /*!< 15 = 함수수행실패 */
      srCount,
    };
    //------------------------------------------------------------------------------

    /**
      * @brief  원점이동 상태
      * @note   원점이동 수행 시 확인되는 원점 이동 상태
      * @see    nmcX_GetAllStatus, nmcX_GetServoStatus, TnmcX_SERVO_STATUS.nHomeStatus
      */
    public enum EHomeStatus : short
    {
      hstsNone,           /*!<  0 = 원점 이동 미실행 */
      hstsDone,           /*!<  1 = 원점 이동 정상 완료 */
      hstsRun,            /*!<  2 = 원점 수행중 */
      hstsErrMov,         /*!<  3 = 이동명령에 오류가 있는 경우(예:해당방향으로 Limit ON) */
      hstsStopUser,       /*!<  4 = 사용자에 의해 중지 */
      hstsStopEmg,        /*!<  5 = 비상정지 */
      hstsStopServoOff,   /*!<  6 = 서보가 Off되어 정지된 경우 */
      hstsStopAlarm,      /*!<  7 = 알람정지 */
      hstsStopNot,        /*!<  8 = -Limit에 의해 정지 */
      hstsStopPot,        /*!<  9 = +Limit에 의해 정지 */
      hstsStopPNot,       /*!< 10 = +-Limit 동시 ON으로 정지 */
      hstsStopTLimit,     /*!< 11 = Torque Limit로 정지된 경우 */
      hstsMovFail,        /*!< 12 = 지정위치로 이동이 실패한 경우 */
      hstsErrPosClear,    /*!< 13 = 위치 Clear중 오류발생 */
    };
    //------------------------------------------------------------------------------

    /**
      * @brief  Parity
      * @note   Serial 통신에 사용되는 Parity Bit
      * @see    nmcX_UARTSetCfg
      */
    public enum EParity : short
    {
      parityNone,
      parityOdd,
      parityEven,
    };
    //------------------------------------------------------------------------------

    /**
      * @brief  시퀸스 실행상태
      * @note   시퀀스 수행 시, 단계별 상태 정의
      * @see    TnmcX_SEQ_STATUS.nRunStatus
      */
    public enum ESeqRunStatus : short
    {
      seqEnd,                 /*!< 종료 */
      seqRun,                 /*!< 실행중 */
      seqWaitEmpty,           /*!< 버퍼Empty로 대기중(실행중) */
      seqReqPause,            /*!< 일시정지 요구(※실행되고 있는 경우 완료되어야 변경되므로) */
      seqPause,               /*!< 일시정지중 */
      seqReqStop,             /*!< 정지 요구(※실행되고 있는 경우 완료되어야 변경되므로) */
      seqStop,                /*!< 정지 */
      seqPrvEndWaitMovDone,   /*!< 종료전 이동완료 대기 */
    };
    //------------------------------------------------------------------------------

    /**
      * @brief  MPG실행 모드
      * @note   MPG실행 모드 정의
      * @see    nmcX_MPGCfgSet, nmcX_MPGCfgGet
      */
    public enum EMpgRunMode : short
    {
        mpgNone,        /*!< 사용 안함 */
        mpgBypass,      /*!< Bypass */
        mpgStepRun,     /*!< Step 실행 */
        mpgCounter,     /*!< Counter로 사용 */
    };
    //------------------------------------------------------------------------------

    /**********************************************************************************************/
    /* Structure                                                                                  */
    /**********************************************************************************************/
    /**
      * @brief  [Slave] Servo 상태 정보
      */
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TnmcX_SERVO_STATUS
    {
      public short      nSID;           /*!< Slave ID */
      public short      nCtrlMode;      /*!< Control Mode      ::ESlaveCtrlMode (1=PP, 2=CP, 3=CV, 4=CT) */
      public short      nCP_Group;      /*!< CP 그룹 0 ~ 3 */
      public short      nCP_Axis;       /*!< CP 축   0 ~ 3 */
      public short      nModel;         /*!< Servo 모델        (0=A4N, 1=A5N, 2=A6N, 3=PS) */
      public short      nMirror;        /*!< Mirror모드        ::EMirror (0=사용X, 1=Main, 2=Slave) */
      public short      nEncType;       /*!< EncoderType
                                        ---Rotary Type인 경우(0=Absolute, 1=Incremental, 2=Absolute 다회전 카운터 무시,
                                                              3=single-turn absolute, 4=continuous roatating absolute)
                                        ---Linear Type인 경우(0=A,B phase output type, 1:Serial communication type(incremental version)
                                                              2=Serial communication type(absolute version), 6=serial communication type (absolute rotary specification) */
      public short      nEStop;         /*!< 비상정지 신호     (0=Off, 1=On) */
      public short      nINP;           /*!< In-Position 신호  (0=Off, 1=On) */
      public short      nBusy;          /*!< Busy 신호         (0=Off, 1=On) */
      public short      nT_Limit;       /*!< Torque Limit      (0=Off, 1=On) */
      public short      nWarning;       /*!< Warning 신호      (0=Off, 1=On) */
      public short      nAlarm;         /*!< Alarm 신호        (0=Off, 1=On) */
      public short      nServoReady;    /*!< Servo Ready 신호  (0=Off, 1=On) */
      public short      nServoOn;       /*!< Servo On 신호     (0=Off, 1=On) */
      public short      nLimitM;        /*!< -Limit 센서 신호  (0=Off, 1=On) */
      public short      nLimitP;        /*!< +LImit 센서 신호  (0=Off, 1=On) */
      public short      nHome;          /*!< Home 신호         (0=Off, 1=On) */
      public short      nSwLimitM;      /*!< SW -Limit 상태    (0=Off, 1=On) */
      public short      nSwLimitP;      /*!< SW +Limit 상태    (0=Off, 1=On) */

      public short      nSI_MON4;       /*!< 서보 SI_MON4 상태 (0=Off, 1=On), [PS]Z상 */
      public short      nSI_MON5;       /*!< 서보 SI_MON5 상태 (0=Off, 1=On) */
      public short      nEX_OUT1;       /*!< 서보 EX_OUT1 상태 (0=Off, 1=On) */
      public short      nEX_OUT2;       /*!< 서보 EX_OUT2 상태 (0=Off, 1=On) */

      public short      nStopReason;    /*!< 정지 원인         ::EStopReason */
      public short      nHomeStatus;    /*!< 원점 이동 상태    ::EHomeStatus */

      public double     dCMDPos;        /*!< 지령 위치  */
      public double     dCMDSpeed;      /*!< 지령 속도 (CP 모드에서만 확인됩니다) */
      public double     dENCPos;        /*!< 엔코더 위치 */
      public double     dENCSpeed;      /*!< 엔코더 속도 (PP 모드에서는 확인되지 않습니다) */
      public double     dTorque;        /*!< 토크/부하율 (토크 리미트(nmcX_TorqueLimitSet, nmcX_mTorqueLimitSet)가 설정 될 경우 토크값, 그 외 평소에는 부하율 표시) */

      public int        lResp1;         /*!< RTEX Response Data1 */
      public int        lResp2;         /*!< RTEX Response Data2 */
      public int        lResp3;         /*!< RTEX Response Data3 */

      public short      nReserved1;     /*!< Reserved */
      public short      nReserved2;     /*!< Reserved */
      public short      nReserved3;     /*!< Reserved */
      public short      nReserved4;     /*!< Reserved */
      public short      nReserved5;     /*!< Reserved */
    };

    /**
      * @brief  [Slave] 속도 프로파일 모드
      */
    enum EnmcX_SPEED_PROFILE
    {
      spTRAPEZOIDAL,                    /*!< 사다리꼴 */
      spS_CURVE,                        /*!< S-Curve */
    };

    /**
      * @brief  [Slave] 속도 정보
      */
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TnmcX_SPEED_INFO
    {
      public short      nProfile;       /*!< 속도 프로파일 모드 ::EnmcX_SPEED_PROFILE */
      public double     dStart;         /*!< 시작속도 */
      public double     dAcc;           /*!< 가속도 */
      public double     dDrive;         /*!< 구동속도 */
      public double     dDec;           /*!< 감속도 */
      public double     dStop;          /*!< 정지속도(초속과 동일하게 설정) */
    };

    /**
      * @brief  [Slave] DI 상태 정보
      */
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TnmcX_RDI_STATUS
    {
      public short      nSID;           /*!< Slave ID */
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_RDIO_PINS)]
      public short[]    nPin;
    };

    /**
      * @brief  [Slave] DO 상태 정보
      */
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TnmcX_RDO_STATUS
    {
      public short      nSID;           /*!< Slave ID */
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_RDIO_PINS)]
      public short[]    nPin;
    };

    /**
      * @brief  [Slave] 속성
      */
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TnmcX_SLAVE_PROPERTY
    {
      public short      nSID;             /*!< Slave ID */
      public short      nKind;            /*!< Kind ::ESlaveKin (0=None, 1=Servo, 2=Input, 3=Output) */
      public short      nCtrlMode;        /*!< Control Mode ::ESlaveCtrlMode(1=PP, 2=CP, 3=CV, 4=CT) */
      public short      nCP_Group;        /*!< CP 그룹 0 ~ 3 */
      public short      nCP_Axis;         /*!< CP 축   0 ~ 3 */
      public short      nModel;           /*!< 서보 모델 (0=A4N, 1=A5N, 2=A6N) */
      public short      nMirror;          /*!< Mirror모드 ::EMirror (0=사용X, 1=Main, 2=Slave) */
      public short      nEncType;         /*!< EncoderType
                                        ---Rotary Type인 경우(0=Absolute, 1=Incremental, 2=Absolute 다회전 카운터 무시,
                                                              3=single-turn absolute, 4=continuous roatating absolute)
                                        ---Linear Type인 경우(0=A,B phase output type, 1:Serial communication type(incremental version)
                                                              2=Serial communication type(absolute version), 6=serial communication type (absolute rotary specification) */
      public short      nUseEStop;        /*!< SI1번 핀이 SI-MON5와 E-STOP겸용이므로 SI1번 핀을 E-Stop으로 사용시=1 */
      public short      nMovPreChkInPos;  /*!< 이동전 In-Postion 체크 유무(0=안함, 1=체크) */
    };

    /**
      * @brief  [Slave] 전체 Slave 속성
      */
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TnmcX_A_SLAVE_PROPERTY
    {
      [MarshalAs(UnmanagedType.ByValArray, ArraySubType=UnmanagedType.Struct, SizeConst = MAX_SLAVE)]
      public TnmcX_SLAVE_PROPERTY[]     tSlave;     /*!< 배열 = Slave ID */
    };

    /**
      * @brief  [Common] MDIO 상태
      * @note   MDIO는 컨트롤러의 전용 입출력입니다.
      */
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TnmcX_MDIO_STATUS
    {
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_MDIO_PINS)]
      public short[]    nInPin;         /*!< 컨트롤러 전용 입력 Pin 상태 */
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_MDIO_PINS)]
      public short[]    nOutPin;        /*!< 컨트롤러 전용 출력 Pin 상태 */
    }

    /**
      * @brief  [Common] Slave ID별 정보
      */
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TnmcX_SIDList
    {
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_SLAVE)]
      public short[]    nSID;           /*!< 정렬된 Slave ID(예:[0]3, [1]5, [2]7..) */
      public short      nCount;         /*!< 정렬된 Slave Count */
    };

    /**
      * @brief  [Common] Slave들의 전체 상태 정보
      * @note   전체 상태는 식별을 위해 종류별로 정렬하여 확인 됩니다.\n
      *         ID 순서로 정렬이 되며, 되도록 ID 순서를 지켜주셔야 식별이 편리합니다.
      */
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TnmcX_ALL_STATUS
    {
      public TnmcX_SIDList          tServoSIDList;  /*!< Servo 별 SID List 구조체 */
      public TnmcX_SIDList          tRDISIDList;    /*!< RDI 별 SID List 구조체 */
      public TnmcX_SIDList          tRDOSIDList;    /*!< RDO 별 SID List 구조체 */
      [MarshalAs(UnmanagedType.ByValArray, ArraySubType=UnmanagedType.Struct, SizeConst = MAX_SLAVE)]
      public TnmcX_SERVO_STATUS[]   tServo;         /*!< 서보 Slave의 정렬 구조체 배열 */
      [MarshalAs(UnmanagedType.ByValArray, ArraySubType=UnmanagedType.Struct, SizeConst = MAX_SLAVE)]
      public TnmcX_RDI_STATUS[]     tRDI;           /*!< Di Slave의 정렬 구조체 배열 */
      [MarshalAs(UnmanagedType.ByValArray, ArraySubType=UnmanagedType.Struct, SizeConst = MAX_SLAVE)]
      public TnmcX_RDO_STATUS[]     tRDO;           /*!< Do Slave의 정렬 구조체 배열 */
      public TnmcX_MDIO_STATUS      tMDIO;          /*!< MDIO */
    };

    /**
      * @brief [Slave] Search상태 정보
      */
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TnmcX_SEARCH_STATUS
    {
        short   nSensor;               /*!< Search대상 신호(0=NOT, 1=POT, 2=Home, 3=Z상) */
        short   nCompleted;            /*!< 완료여부(0=완료안됨, 1=완료됨) */
        short   nStopMode;             /*!< 정지모드 ::EStopMode(0 = 긴급, 1 = 감속) */
        double  dLatchPos;             /*!< 검색되어 Latch된 위치 */
    };

    /**
      * @brief [Slave] 리스트 모션
      */
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TnmcX_LM_STATUS
    {
      public short      nSID;           /*!< Slave ID */
      public short      nRun;           /*!< 실행 상태 (0=정지, 1=실행) */
      public short      nWaitEmpty;     /*!< 대기 모드(0=강제종료, 1=등록 종료/노드 추가 등록을 위해 대기) */
      public short      nCloseNode;     /*!< 노드 등록 종료(더이상 노드 등록이 없으면 종료행위 해야하므로) */
      public short      nRemainNum;     /*!< 비어 있는 버퍼 수 (최대 128) */
      public short      nPinMask;       /*!< MDIO출력 Pin Mask */
      public short      nEndDo;         /*!< 종료 출력 */
      public int        lEndSpeed;      /*!< 마지막 종료 Speed */
      public uint       dwExeNo;        /*!< 실행 중인 리스트모션의 수(0 ~ 4,294,967,295) */
    };

    /**
      * @brief [Slave] 전체 리스트 모션
      */
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TnmcX_A_LM_STATUS
    {
      [MarshalAs(UnmanagedType.ByValArray, ArraySubType=UnmanagedType.Struct, SizeConst = MAX_SLAVE)]
      public TnmcX_LM_STATUS[]  tServo;     /*!< 배열 = Slave ID */
    };

    /**
      * @brief [Slave] 토크 Limit 상태 정보
      */
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TnmcX_T_LIMIT_STATUS
    {
      public short      nActive;        /*!< 활성화 상태 (0=비활성, 1=활성) */
      public short      nStopped;       /*!< Torque Limit로 정지한 경우(0=미적용, 1=적용) */
      public short      nCheckTime;     /*!< Torque Limit발생으로 시간 체크 여부(0=체크안함, 1=체크중) */
      public short      nTimeKeep;      /*!< Torque Limit ON이 유지되어야 하는 시간(ms) */
      public short      nTimePassed;    /*!< Torque Limit ON되어 경과된 시간(ms) */
      public short      nN_Value;       /*!< Negative Direction Torque Limit(0~500%) */
      public short      nP_Value;       /*!< Positive Direction Torque Limit(0~500%) */
      public int        lOnOffMask;     /*!< MDIO On|Off Mask(HiByte:On, LoByte:Off) */
      public int        lDeviExcess;    /*!< 위치 편차 초과 범위 */
    };

    /**
      * @brief [Common] 보간 맵 상태 정보
      */
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TnmcX_I_STATUS
    {
      public short      nSlaveCount;    /*!< Map에 할당 된 축 개수 */
      public short      nReserved;      /*!< Reserved */
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = CP_MAX_AXES)]
      public short[]    nSlaveID;       /*!< Axis에 할당 되는 Slave ID */
    };

    /**
      * @brief [Common] 시퀀스 정보
      */
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TnmcX_SEQ_STATUS
    {
      public short      nRunStatus;     /*!< 실행상태(0=종료, 1=실행중, 2=버퍼Empty로 대기중,3=일시정지 요구,4=일시정지중,5=정지 요구,6=정지) */
      public short      nFuncAddEnd;    /*!< 함수 등록 완료 상태 (0=등록대기, 1=완료) */
      public short      nWaitEmpty;     /*!< Function 등록 대기 상태 (0=강제종료, 1=함수등록 될때까지 대기) */
      public short      nWaitMovDone;   /*!< 이동Function일 경우 이동에 지정된 서보가 이동중일때 대기 설정 (0=대기안함, 1=대기) */
      public short      nWaitInPos;     /*!< 이동Function일 경우 이동에 지정된 서보의 In-Position대기 설정 (0=대기안함, 1=대기) */
      public short      nAutoHold;      /*!< Function수행후 자동 일시중지(0=계속실행, 1=AutoHold) */
      public short      nStopReason;    /*!< F시퀀스의 정지 원인을 기록 */
      public int        dwFuncCnt;      /*!< 시퀀스 등록된 함수 갯수 */
      public int        dwExeNo;        /*!< 시퀀스 함수 실행번호 */
      public short      nResult;        /*!< 시퀀스 수행결과 */
    };

    /**
      * @brief [Common] 보간 맵 상태 정보
      */
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TnmcX_A_SEQ_STATUS
    {
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_SEQ)]
      public TnmcX_SEQ_STATUS[] tSeq;   /*!< 배열 = 시퀸스 번호 */
    };


    /**********************************************************************************************/
    /* Functions                                                                                  */
    /**********************************************************************************************/
    /**
      * @brief      컨트롤러의 네트워크 연결상태를 확인합니다. (Ping Check)
      * @param[in]  nXNo            컨트롤러 번호 (IP Address Field 3(장치 Rotary Switch))
      * @param[in]  nIpAddr0        IP Address Field 0 (최대 255)
      * @param[in]  nIpAddr1        IP Address Field 1 (최대 255)
      * @param[in]  nIpAddr2        IP Address Field 2 (최대 255)
      * @param[in]  lWaitTime       응답대기시간
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_PingCheck(short nXNo, short nIpAddr0, short nIpAddr1, short nIpAddr2, int lWaitTime);

    /**
      * @brief      컨트롤러와 연결을 수행합니다.
      * @param[in]  nXNo            컨트롤러 번호
      * @param[in]  nIpAddr0        IP Address Field 0 (최대 255)
      * @param[in]  nIpAddr1        IP Address Field 1 (최대 255)
      * @param[in]  nIpAddr2        IP Address Field 2 (최대 255)
      * @return     EnmcX_FUNC_RESULT
      * @see        nmcX_Disconnect
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_Connect(short nXNo, short nIpAddr0, short nIpAddr1, short nIpAddr2);

    /**
      * @brief      컨트롤러와 연결을 해제합니다.
      * @param[in]  nXNo            연결을 해제할 컨트롤러 번호
      * @see        nmcX_Connect
      */
    [DllImport("nmcX.dll")]
    public static extern void nmcX_Disconnect(short nXNo);

    /**
      * @brief      컨트롤러와의 연결, 네트워크 데이터 송수신의 제한시간을 설정합니다.(ms단위)
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nTMO_Conn       연결 제한시간(ms)
      * @param[in]  nTMO_Tx         송신 제한시간(ms)
      * @param[in]  nTMO_Rx         수신 제한시간(ms)
      * @see        nmcX_TMOGet
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_TMOSet(short nXNo, short nTMO_Conn, short nTMO_Tx, short nTMO_Rx);

    /**
      * @brief      설정된 제한시간을 확인합니다.(ms단위)
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[out] pnRetTMO_Conn   연결 제한시간 반환 포인터(ms)
      * @param[out] pnRetTMO_Tx     송신 제한시간 반환 포인터(ms)
      * @param[out] pnRetTMO_Rx     수신 제한시간 반환 포인터(ms)
      * @see        nmcX_TMOSet
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_TMOGet(short nXNo, out short pnRetTMO_Conn, out short pnRetTMO_Tx, out short pnRetTMO_Rx);

    /**
      * @brief      이동단위 설정
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[int] pnRetTMO_Tx     이동단위
      * @see        nmcX_UnitPerPulseGet
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_UnitPerPulseSet(short nXNo, short nSID, double dRatio);

    /**
      * @brief      이동단위 확인
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @see        nmcX_UnitPerPulseSet
      * @return     이동단위
      */
    [DllImport("nmcX.dll")]
    public static extern double nmcX_UnitPerPulseGet(short nXNo, short nSID);

    /**
      * @brief      시작속도와 정지 속도가 같은 속도로 설정여부 설정
      * @param[in]  nEqual            시작속도와 정지 속도를 동일하게 설정할 것인지 여부(0=동일X, 1=동일하게 설정)
      * @see        nmcX_SpeedEqualStartAndStopGet
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SpeedEqualStartAndStopSet(short nEqual);

    /**
      * @brief      시작속도와 정지 속도가 같은 속도로 설정여부 설정값 확인
      * @see        nmcX_SpeedEqualStartAndStopSet
      * @return     시작속도와 정지 속도를 동일하게 설정할 것인지 여부(0=동일X, 1=동일하게 설정)
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SpeedEqualStartAndStopGet();

    /**
      * @brief      지정 Servo를 재시작합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @see        nmcX_mServoReset, nmcX_aServoReset
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_ServoReset(short nXNo, short nSID);

    /**
      * @brief      여러 Servo를 지정하여 재시작합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     지정할 Slave 수량
      * @param[in]  pnSIDList       Slave ID 배열 포인터(배열 수 = Slave 수량)
      * @see        nmcX_ServoReset, nmcX_aServoReset
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mServoReset(short nXNo, short nSlaveCount, short[] pnSIDList);

    /**
      * @brief      컨트롤러와 연결된 전체 Servo를 재시작합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @see        nmcX_ServoReset, nmcX_mServoReset
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_aServoReset(short nXNo);

    /**
      * @brief      서보ON후 Active체크 제한시간 설정(ms단위)
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nTMO            제한시간(ms)
      * @see        nmcX_ServoOnAfterActChkTMOGet, nmcX_ServoOn, nmcX_mServoOn
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_ServoOnAfterActChkTMOSet(short nXNo, short nTMO);

    /**
      * @brief      서보ON후 Active체크 대기시간 확인(ms단위)
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[out] pnRetTMO        제한시간 반환 포인터(ms)
      * @see        nmcX_ServoOnAfterActChkTMOSet, nmcX_ServoOn, nmcX_mServoOn
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_ServoOnAfterActChkTMOGet(short nXNo, out short pnRetTMO);

    /**
      * @brief      지정 Servo를 On/Off 합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nOut            출력 상태 (0=Off, 1=On)
      * @see        nmcX_mServoOn, nmcX_ServoOnAfterActChkTMOSet, nmcX_ServoOnAfterActChkTMOGet
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_ServoOn(short nXNo, short nSID, short nOut);

    /**
      * @brief      여러 Servo를 지정하여 On/Off 합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     지정할 Slave 수량
      * @param[in]  pnSIDList       Slave ID 배열 포인터(배열 수 = Slave 수량)
      * @param[in]  nOut            출력 상태 (0=Off, 1=On)
      * @see        nmcX_ServoOn, nmcX_ServoOnAfterActChkTMOSet, nmcX_ServoOnAfterActChkTMOGet
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mServoOn(short nXNo, short nSlaveCount, short[] pnSIDList, short nOut);

    /**
      * @brief      지정 Servo에 속도 프로파일을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nProfile        프로파일 (0=사다리꼴, 1=S-Curve)
      * @param[in]  dStart          시작속도(이동단위/초)
      * @param[in]  dAcc            가속도(이동단위/초²)
      * @param[in]  dDrive          구동속도(이동단위/초)
      * @param[in]  dDec            감속도(이동단위/초²)
      * @see        nmcX_mSpeedSet
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SpeedSet(short nXNo, short nSID, short nProfile, double dStart, double dAcc, double dDrive, double dDec);

    /**
      * @brief      지정 Servo에 정지속도가 포함된 속도 프로파일을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nProfile        프로파일 (0=사다리꼴, 1=S-Curve)
      * @param[in]  dStart          시작속도(이동단위/초)
      * @param[in]  dAcc            가속도(이동단위/초²)
      * @param[in]  dDrive          구동속도(이동단위/초)
      * @param[in]  dDec            감속도(이동단위/초²)
      * @param[in]  dStop           정지속도(이동단위/초)
      * @see        nmcX_mSpeedWithStopSet
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SpeedWithStopSet(short nXNo, short nSID, short nProfile, double dStart, double dAcc, double dDrive, double dDec, double dStop);

    /**
      * @brief      지정 Servo에 속도 프로파일을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     지정할 Slave 수량
      * @param[in]  pnSIDList       Slave ID 배열 포인터(배열 수 = Slave 수량)
      * @param[in]  pnProfile       프로파일 배열 포인터(0=사다리꼴, 1=S-Curve)
      * @param[in]  pdStart         시작속도 배열 포인터(이동단위/초)
      * @param[in]  pdAcc           가속도 배열 포인터(이동단위/초²)
      * @param[in]  pdDrive         구동속도 배열 포인터(이동단위/초)
      * @param[in]  pdDec           감속도 배열 포인터(이동단위/초²)
      * @see        nmcX_SpeedSet
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mSpeedSet(short nXNo, short nSlaveCount, short[] pnSIDList, short[] pnProfile, double[] pdStart, double[] pdAcc, double[] pdDrive, double[] pdDec);

    /**
      * @brief      지정 Servo에 정지속도가 포함된 속도 프로파일을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     지정할 Slave 수량
      * @param[in]  pnSIDList       Slave ID 배열 포인터(배열 수 = Slave 수량)
      * @param[in]  pnProfile       프로파일 배열 포인터(0=사다리꼴, 1=S-Curve)
      * @param[in]  pdStart         시작속도 배열 포인터(이동단위/초)
      * @param[in]  pdAcc           가속도 배열 포인터(이동단위/초²)
      * @param[in]  pdDrive         구동속도 배열 포인터(이동단위/초)
      * @param[in]  pdDec           감속도 배열 포인터(이동단위/초²)
      * @param[in]  pdStop          정지속도 배열 포인터(이동단위/초)
      * @see        nmcX_SpeedWithStopSet
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mSpeedWithStopSet(short nXNo, short nSlaveCount, short[] pnSIDList, short[] pnProfile, double[] pdStart, double[] pdAcc, double[] pdDrive, double[] pdDec, double[] pdStop);

    /**
      * @brief      지정 Servo에 가속도를 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  dAcc            가속도(이동단위/초²)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SpeedSetAcc(short nXNo, short nSID, double dAcc);

    /**
      * @brief      여러 Servo를 지정하여 가속도를 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 설정할 Slave의 개수
      * @param[in]  pnSIDList       설정할 Slave ID 배열 포인터(배열 수 = Slave의 개수)
      * @param[in]  pdAcc           가속도 배열 포인터 (이동단위/초²)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mSpeedSetAcc(short nXNo, short nSlaveCount, short[] pnSIDList, double[] pdAcc);

    /**
      * @brief      지정 Servo에 감속도를 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  dDec            감속도(이동단위/초²)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SpeedSetDec(short nXNo, short nSID, double dDec);

    /**
      * @brief      여러 Servo를 지정하여 감속도를 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 설정할 Slave의 개수
      * @param[in]  pnSIDList       설정할 Slave ID 배열 포인터(배열 수 = Slave의 개수)
      * @param[in]  pdDec           감속도 배열 포인터 (이동단위/초²)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mSpeedSetDec(short nXNo, short nSlaveCount, short[] pnSIDList, double[] pdDec);

    /**
      * @brief      지정 Servo에 설정된 속도 프로파일을 확인합니다.
      * @param[in]  nXNo                연결된 컨트롤러 번호
      * @param[in]  nSID                Slave ID
      * @param[out] pRetSpeed           반환 받을 TnmcX_SPEED_INFO 구조체 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SpeedGet(short nXNo, short nSID, out TnmcX_SPEED_INFO pRetSpeed);

    /**
      * @brief      지정 Servo의 이동전 In-Position 체크 유무 설정(체크설정이면 이동함수 수행시 In-Postion이 아니면 에러Return됨)
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nCheck          체크유무(0=체크안함, 1=체크)
      * @return     EnmcX_FUNC_RESULT
      * @see        nmcX_SlavePropertyGet
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_MovPreChkInPosSet(short nXNo, short nSID, short nCheck);

    /**
      * @brief      여러 Servo를 지정하여 이동전 In-Position 체크 유무 설정
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     설정할 Slave의 개수
      * @param[in]  pnSIDList       설정할 Slave ID 배열 포인터(배열 수 = Slave의 개수)
      * @param[in]  nCheck          체크유무(0=체크안함, 1=체크)
      * @return     EnmcX_FUNC_RESULT
      * @see        nmcX_SlavePropertyGet
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mMovPreChkInPosSet(short nXNo, short nSlaveCount, short[] pnSIDList, short nCheck);

    /**
      * @brief      지정 Servo에 속도 이동을 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nDir            방향 ::EDirection(0 = CW(+방향), 1 = CCW(-방향))
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_MoveVel(short nXNo, short nSID, short nDir);

    /**
      * @brief      여러 Servo를 지정하여 속도 이동을 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 이동할 Slave의 개수
      * @param[in]  pnSIDList       이동할 Slave ID 배열 포인터(배열 수 = Slave의 개수)
      * @param[in]  nDir            방향 ::EDirection(0 = CW(+방향), 1 = CCW(-방향))
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mMoveVel(short nXNo, short nSlaveCount, short[] pnSIDList, short nDir);

    /**
      * @brief      지정 Servo에 절대위치 이동을 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  dPos            절대 위치
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_MoveAbs(short nXNo, short nSID, double dPos);

    /**
      * @brief      여러 Servo를 지정하여 절대위치 이동을 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 이동할 Slave의 개수
      * @param[in]  pnSIDList       이동할 축의 Slave ID 배열 포인터(배열 수 = Slave의 개수)
      * @param[in]  pdPosList       절대위치 배열 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mMoveAbs(short nXNo, short nSlaveCount, short[] pnSIDList, double[] pdPosList);

    /**
      * @brief      지정 Servo에 상대위치 이동을 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  dPos            상대위치
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_MoveRel(short nXNo, short nSID, double dPos);

    /**
      * @brief      여러 Servo를 지정하여 상대위치 이동을 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 이동할 Slave의 개수
      * @param[in]  pnSIDList       이동할 축의 Slave ID 배열 포인터(배열 수 = Slave의 개수)
      * @param[in]  pdPosList       상대위치 배열 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mMoveRel(short nXNo, short nSlaveCount, short[] pnSIDList, double[] pdPosList);

    /**
      * @brief      지정 Servo에 센서의 위치 검색을 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nDir            방향 ::EDirection(0 = CW(+방향), 1 = CCW(-방향))
      * @param[in]  nSensor         검색할 센서 (0 =-Limit, 1 =+Limit, 2=Home, 3=Z상)
      * @param[in]  nEdge           로직 (0 = Falling, 1 = Rising)
      * @param[in]  nStopMode       정지모드 ::EStopMode(0 = 긴급, 1 = 감속)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SearchMove(short nXNo, short nSID, short nDir, short nSensor, short nEdge, short nStopMode);

    /**
      * @brief      지정 Servo의 센서의 검색이동 상태를 확인합니다.
      * @param[in]  nXNo                연결된 컨트롤러 번호
      * @param[in]  nSID                Slave ID
      * @param[out] ptRetSearchStatus   반환받을 TnmcX_SEARCH_STATUS 구조체 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SearchGetStatus(short nXNo, short nSID, out TnmcX_SEARCH_STATUS ptRetSearchStatus);

    /**
      * @brief      여러 Servo의 센서의 검색이동 상태를 확인합니다
      * @param[in]  nXNo                연결된 컨트롤러 번호
      * @param[in]  nSID                Slave ID
      * @param[out] ptRetList           반환받을 TnmcX_SEARCH_STATUS 구조체 배열의 포인터(배열 수 = Slave 수량)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mSearchGetStatus(short nXNo, short nSlaveCount, short[] pnSIDList, out TnmcX_SEARCH_STATUS[] ptRetList);

    /**
      * @brief      지정 Servo에 구동속도 오버라이드를 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  dDrive          구동 속도
      * @return     EnmcX_FUNC_RESULT
      * @warning    구동중인 Servo에만 적용됩니다.
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_OverrideDriveSpeed(short nXNo, short nSID, double dDrive);

    /**
      * @brief      여러 Servo를 지정하여 구동속도 오버라이드를 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 오버라이드할 Slave의 개수
      * @param[in]  pnSIDList       오버라이드할 Slave ID 배열 포인터(배열 수 = Slave의 개수)
      * @param[in]  pdDrive         구동 속도 배열 포인터
      * @return     EnmcX_FUNC_RESULT
      * @warning    구동중인 Servo에만 적용됩니다.
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mOverrideDriveSpeed(short nXNo, short nSlaveCount, short[] pnSIDList, double[] pdDrive);

    /**
      * @brief      지정 Servo에 속도 오버라이드를 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  dAcc            가속도
      * @param[in]  dDrive          구동 속도
      * @param[in]  dDec            감속도
      * @return     EnmcX_FUNC_RESULT
      * @warning    구동중인 Servo에만 적용됩니다.
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_OverrideSpeed(short nXNo, short nSID, double dAcc, double dDrive, double dDec);

    /**
      * @brief      여러 Servo를 지정하여 속도 오버라이드를 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 오버라이드할 Slave의 개수
      * @param[in]  pnSIDList       오버라이드할 Slave ID 배열 포인터(배열 수 = Slave의 개수)
      * @param[in]  pdAcc           가속도 배열 포인터
      * @param[in]  pdDrive         구동 속도 배열 포인터
      * @param[in]  pdDec           감속도 배열 포인터
      * @return     EnmcX_FUNC_RESULT
      * @warning    구동중인 Servo에만 적용됩니다.
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mOverrideSpeed(short nXNo, short nSlaveCount, short[] pnSIDList, double[] pdAcc, double[] pdDrive, double[] pdDec);

    /**
      * @brief      지정 Servo에 절대위치 오버라이드를 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  dPos            오버라이드 위치
      * @return     EnmcX_FUNC_RESULT
      * @warning    구동중인 Servo에만 적용되며, 절대위치만 적용됩니다.
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_OverrideAbs(short nXNo, short nSID, double dPos);

    /**
      * @brief      여러 Servo를 지정하여 절대위치 오버라이드를 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 오버라이드할 Slave의 개수
      * @param[in]  pnSIDList       오버라이드할 Slave ID 배열 포인터(배열 수 = Slave의 개수)
      * @param[in]  pdPosList       오버라이드할 위치의 배열 포인터
      * @return     EnmcX_FUNC_RESULT
      * @warning    구동중인 Servo에만 적용되며, 절대위치만 적용됩니다.
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mOverrideAbs(short nXNo, short nSlaveCount, short[] pnSIDList, double[] pdPosList);

    /**
      * @brief      구동 중인 Servo에 설정된 감속으로 감속 정지를 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_StopDec(short nXNo, short nSID);

    /**
      * @brief      구동 중인 Servo에 긴급 정지를 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_StopSudden(short nXNo, short nSID);

    /**
      * @brief      여러 Servo를 지정하여 긴급/감속 정지를 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 정지할 Slave의 개수
      * @param[in]  pnSIDList       정지할 Slave ID 배열 포인터(배열 수 = Slave의 개수)
      * @param[in]  nMode           정지모드 ::EStopMode(0=긴급 정지, 1=감속 정지)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mStop(short nXNo, short nSlaveCount, short[] pnSIDList, short nMode);

    /**
      * @brief      지정 보간맵에 할당된 Slave에 감속/긴급 정지를 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nMapNo          보간 Map 번호 (0 ~15 Max.16개)
      * @param[in]  nMode           정지모드 ::EStopMode(0=긴급 정지, 1=감속 정지)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_StopIMap(short nXNo, short nMapNo, short nMode);

    /**
      * @brief      [CT모드] 토크값을 설정하여 구동합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  dTorque         Torque설정값(-5000 ~ 5000(0.1%) 이 범위값이 아니면 현재Torque값이 반영되어 정지없는 전환 가능)
      * @param[in]  nSpeedLimit     Speed Limit 설정값(rpm)(값이 음수이거나 NULL이면 설정안함)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_CT_SetTorque(short nXNo, short nSID, double dTorque, short nSpeedLimit);

    /**
      * @brief      [CT모드] 여러 Servo를 지정하여 토크제어 구동을 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 구동할 Slave의 개수
      * @param[in]  pnSIDList       구동할 Slave ID 배열 포인터(배열 수 = Slave의 개수)
      * @param[in]  pdTorque        설정할 토크 값 배열 포인터(-5000 ~ 5000(0.1%))
      * @param[in]  pnSpeedLimit    Speed Limit 설정값 배열 포인터(rpm)(값이 음수이거나 NULL이면 설정안함)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mCT_SetTorque(short nXNo, short nSlaveCount, short[] pnSIDList, double[] pdTorque, short[] pnSpeedLimit);

    /**
      * @brief      [CV모드] 속도를 설정하여 구동합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  dAcc            가속 설정값(pps)(음수값이면 설정안함)
      * @param[in]  dDrive          구동 설정값(pps)(음수일때 역방향)
      * @param[in]  dDec            감속 설정값(pps)(음수값이면 설정안함)
      * @param[in]  nSCurveTm       S-Curve시간(0~1000ms)(음수값이면 설정안함)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_CV_SetVelocity(short nXNo, short nSID, double dAcc, double dDrive, double dDec, short nSCurveTm);

    /**
      * @brief      [CV모드] 여러 Servo를 지정하여 속도제어 구동을 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 정지할 Slave의 개수
      * @param[in]  pnSIDList       정지할 Slave ID 배열 포인터(배열 수 = Slave의 개수)
      * @param[in]  pdAcc           설정할 가속도   값 배열 포인터(pps)(음수값이면 설정안함)
      * @param[in]  pdDrive         설정할 구동속도 값 배열 포인터(pps)(음수일때 역방향)
      * @param[in]  pdDec           설정할 감속도   값 배열 포인터(pps)(음수값이면 설정안함)
      * @param[in]  pnSCurveTm      설정할 S-Curve시간 값 배열 포인터(0~1000ms)(음수값이면 설정안함)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mCV_SetVelocity(short nXNo, short nSlaveCount, short[] pnSIDList,  double[] pdAcc, double[] pdDrive, double[] pdDec,  short[] pnSCurveTm);

    /**
      * @brief      [PP모드] Servo를 지정하여 Target Position 및 Speed을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  dPos            명령할 절대위치
      * @param[in]  dSpeed          명령할 구동속도
      * @return     EnmcX_FUNC_RESULT
      * @warning    Servo의 구동상태에 관계없이 명령된 목표위치에 명령된 속도로 구동합니다.
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_PP_SetTPosSpeed(short nXNo, short nSID, double dPos, double dSpeed);
    /**
      * @brief      [PP모드] 여러 Servo를 지정하여 Target Position 및 Speed을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 명령할 Slave의 개수
      * @param[in]  pnSIDList       Slave ID 배열 포인터(배열 수 = Slave의 개수)
      * @param[in]  pdPosList       절대위치 배열 포인터
      * @param[in]  pdSpeedList     구동속도 배열 포인터
      * @return     EnmcX_FUNC_RESULT
      * @warning    Servo의 구동상태에 관계없이 명령된 목표위치에 명령된 속도로 구동합니다.
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mPP_SetTPosSpeed(short nXNo, short nSlaveCount, short[] pnSIDList, double[] pdPosList, double[] pdSpeedList);

    /**
      * @brief      지령/엔코더 위치를 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  dPos            설정할 위치 값
      * @return     EnmcX_FUNC_RESULT
      * @warning    위치가 동일하게 설정합니다. Absolute Encoder는 변경할 수 없습니다.
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_CmdEncPosSet(short nXNo, short nSID, double dPos);

    /**
      * @brief      여러 Servo를 지정하여 지령/엔코더 위치를 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 설정할 Slave의 개수
      * @param[in]  pnSIDList       설정할 Slave ID 배열 포인터(배열 수 = Slave의 개수)
      * @param[in]  pdPos           설정할 위치 값 배열 포인터
      * @return     EnmcX_FUNC_RESULT
      * @warning    위치가 동일하게 설정합니다. Absolute Encoder는 변경할 수 없습니다.
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mCmdEncPosSet(short nXNo, short nSlaveCount, short[] pnSIDList, double[] pdPos);

    /**
      * @brief      지령위치를 엔코더위치로 변경합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_EncToCmdPos(short nXNo, short nSID);

    /**
      * @brief      여러 Servo를 지정하여 지령위치를 엔코더위치로 변경합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 설정할 Slave의 개수
      * @param[in]  pnSIDList       설정할 Slave ID 배열 포인터(배열 수 = Slave의 개수)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mEncToCmdPos(short nXNo, short nSlaveCount, short[] pnSIDList);

    /**
      * @brief      Absolute encoder의 Multi-Turn data를 초기화합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @return     EnmcX_FUNC_RESULT
      * @warning    Servo Off 상태에서 적용하십시오. Incremental encoder는 사용할 수 없습니다.
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_AbsEncMultiTurnClear(short nXNo, short nSID);

    /**
      * @brief      여러 Servo를 지정하여 Absolute encoder의 Multi-Trun data를 초기화합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 설정할 Slave의 개수
      * @param[in]  pnSIDList       설정할 Slave ID 배열 포인터(배열 수 = Slave의 개수)
      * @return     EnmcX_FUNC_RESULT
      * @warning    Servo Off 상태에서 적용하십시오. Incremental encoder는 사용할 수 없습니다.
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mAbsEncMultiTurnClear(short nXNo, short nSlaveCount, short[] pnSIDList);

    /**
      * @brief      Home Sensor를 이용하여 원점 구동을 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nDir            원점구동 방향 ::EDirection(0 = CW(+방향), 1 = CCW(-방향))
      * @param[in]  n1stStopMode    1차 이동 정지모드 ::EStopMode(0 = 긴급, 1 = 감속)
      * @param[in]  nZPhaseMode     Z상 검색모드(0 = CW, 1 = CCW, 2 = 비사용)
      * @param[in]  nPosClear       원점 완료 후 위치 초기화 설정(0 = 비사용, 1 = 사용)
      * @param[in]  nPosClearDTime  원점 완료 후 위치 초기화 전 지연시간(ms)
      * @param[in]  dOffset         원점 완료 후 Offset 위치 값(절대값으로)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_HomeMoveHome(short nXNo, short nSID, short nDir, short n1stStopMode, short nZPhaseMode, short nPosClear, short nPosClearDTime, double dOffset);

    /**
      * @brief      Z상을 이용한 원점 구동을 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nDir            원점구동 방향 ::EDirection(0 = CW(+방향), 1 = CCW(-방향))
      * @param[in]  n1stStopMode    1차 이동 정지모드 ::EStopMode(0 = 긴급, 1 = 감속)
      * @param[in]  nPosClear       원점 완료 후 위치 초기화 설정(0 = 비사용, 1 = 사용)
      * @param[in]  nPosClearDTime  원점 완료 후 위치 초기화 전 지연시간(ms)
      * @param[in]  dOffset         원점 완료 후 Offset 위치 값(절대값으로)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_HomeMoveZPhase(short nXNo, short nSID, short nDir, short n1stStopMode, short nPosClear, short nPosClearDTime, double dOffset);

    /**
      * @brief      Limit Sensor를 이용한 원점 구동을 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nDir            원점구동 방향 ::EDirection(0 = CW(+방향), 1 = CCW(-방향))
      * @param[in]  n1stStopMode    1차 이동 정지모드 ::EStopMode(0 = 긴급, 1 = 감속)
      * @param[in]  nAddZPhase      Z상 추가(0 = 비사용, 1 = 사용)
      * @param[in]  nPosClear       원점 완료 후 위치 초기화 설정(0 = 비사용, 1 = 사용)
      * @param[in]  nPosClearDTime  원점 완료 후 위치 초기화 전 지연시간(ms)
      * @param[in]  dOffset         원점 완료 후 Offset 위치 값(절대값으로)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_HomeMoveLimit(short nXNo, short nSID, short nDir, short n1stStopMode, short nAddZPhase, short nPosClear, short nPosClearDTime, double dOffset);

    /**
      * @brief      Stopper를 이용한 원점 구동을 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nDir            원점구동 방향 ::EDirection(0 = CW(+방향), 1 = CCW(-방향))
      * @param[in]  nAddZPhase      Z상 추가(0 = 비사용, 1 = 사용)
      * @param[in]  nPosClear       원점 완료 후 위치 초기화 설정(0 = 비사용, 1 = 사용)
      * @param[in]  nPosClearDTime  원점 완료 후 위치 초기화 전 지연시간(ms)
      * @param[in]  dOffset         원점 완료 후 Offset 위치 값(절대값으로)
      * @return     EnmcX_FUNC_RESULT
      * @warning    nmcX_mTorqueLimitSet 로 Torque Limit가 먼저 설정되어 있어야 합니다.
      * @see        nmcX_mTorqueLimitSet
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_HomeMoveStopper(short nXNo, short nSID, short nDir, short nAddZPhase, short nPosClear, short nPosClearDTime, double dOffset);

    /**
      * @brief      원점 구동의 구동속도를 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nProfile        속도 프로파일(0=사다리꼴, 1=S-Curve)
      * @param[in]  dxxxDrive       x차 구동 속도
      * @param[in]  dOffsetDrive    Offset 구동 속도
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_HomeSetDriveSpeed(short nXNo, short nSID, short nProfile, double d1stDrive, double d2ndDrive, double d3rdDrive, double dOffsetDrive);

    /**
      * @brief 원점 이동의 초속, 구동 속도 대비 감속 비율 적용 속도 프로파일을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nProfile        속도 프로파일(0=사다리꼴, 1=S-Curve)
      * @param[in]  dStart          전체 시작 속도
      * @param[in]  dxxxDrive       x차 구동 속도
      * @param[in]  dOffsetDrive    Offset 구동 속도
      * @param[in]  nRatio          구동 속도 대비 가감속의 비율
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_HomeSetSpeed(short nXNo, short nSID, short nProfile, double dStart, double d1stDrive, double d2ndDrive, double d3rdDrive, double dOffsetDrive, short nRatio);

    /**
      * @brief      원점 이동의 상세 속도 프로파일을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nxxxProfile     x차 속도 프로파일(0=사다리꼴, 1=S-Curve)
      * @param[in]  dxxxStart       x차 시작 속도
      * @param[in]  dxxxAcc         x차 가속도
      * @param[in]  dxxxDrive       x차 구동 속도
      * @param[in]  dxxxDec         x차 감속도
      * @param[in]  nOffsetProfile  Offset 속도 프로파일(0=사다리꼴, 1=S-Curve)
      * @param[in]  dOffsetStart    Offset 시작 속도
      * @param[in]  dOffsetAcc      Offset 가속도
      * @param[in]  dOffsetDrive    Offset 구동 속도
      * @param[in]  dOffsetDec      Offset 감속도
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_HomeSetDetailSpeed(short nXNo, short nSID,
                                                       short n1stProfile,    double d1stStart,    double d1stAcc,    double d1stDrive,    double d1stDec,
                                                       short n2ndProfile,    double d2ndStart,    double d2ndAcc,    double d2ndDrive,    double d2ndDec,
                                                       short n3rdProfile,    double d3rdStart,    double d3rdAcc,    double d3rdDrive,    double d3rdDec,
                                                       short nOffsetProfile, double dOffsetStart, double dOffsetAcc, double dOffsetDrive, double dOffsetDec);

    /**
      * @brief      지정 Servo의 원점완료를 취소합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @see        nmcX_mHomeDoneCancel
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_HomeDoneCancel(short nXNo, short nSID);

    /**
      * @brief      여러 Servo를 지정하여 원점완료를 취소합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     지정할 Slave 수량
      * @param[in]  pnSIDList       Slave ID 배열 포인터(배열 수 = Slave 수량)
      * @see        nmcX_HomeDoneCancel
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mHomeDoneCancel(short nXNo, short nSlaveCount, short[] pnSIDList);

    /**
      * @brief      보간 MAP을 설정 합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nMapNo          보간 Map 번호 (0 ~15 Max.16개)
      * @param[in]  nSlaveCount     Slave의 개수
      * @param[in]  pnSIDList       Slave ID의 배열 포인터(축의 개수)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_IMapSet(short nXNo, short nMapNo, short nSlaveCount, short[] pnSIDList);

    /**
      * @brief      보간 MAP을 삭제합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nMapNo          보간 Map 번호 (0 ~15 Max.16개)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_IMapClear(short nXNo, short nMapNo);

    /**
      * @brief      설정된 보간 MAP으로 다축 직선 보간을 합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nMapNo          보간 Map 번호(0~15)
      * @param[in]  nPosType        위치모드(0=상대위치, 1=절대위치)
      * @param[in]  nPosCount       위치 개수
      * @param[in]  pdPos           위치 좌표값 배열 포인터(배열 수 = 위치개수)
      * @param[in]  ptSpeed         속도설정(TnmcX_SPEED_INFO 구조체 포인터)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_ILinear(short nXNo, short nMapNo, short nPosType, short nPosCount, double[] pdPos, ref TnmcX_SPEED_INFO ptSpeed);

    /**
      * @brief      U축을 포함하여 설정된 보간 MAP으로 다축 직선 보간을 수행(Pre-Register를 사용하는 경우 헨리컬 보간 조건때문에)
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nMapNo          보간 Map 번호(0~15)
      * @param[in]  nPosType        위치모드 ::EPosType(0=상대위치, 1=절대위치)
      * @param[in]  nPosCount       위치 개수
      * @param[in]  pdPos           위치 좌표값 배열 포인터(배열 수 = 위치개수)
      * @param[in]  ptSpeed         속도설정(TnmcX_SPEED_INFO 구조체 포인터)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_ILinearWithUAxis(short nXNo, short nMapNo, short nPosType, short nPosCount, double[] pdPos, ref TnmcX_SPEED_INFO ptSpeed);

    /**
      * @brief      설정된 보간 MAP으로 2축 원호(중심점과 회전 각도) 보간을 합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nMapNo          보간 Map 번호(0~15)
      * @param[in]  nPosType        위치모드 ::EPosType(0=상대위치, 1=절대위치)
      * @param[in]  pdCent          중심점 배열 포인터
      * @param[in]  dAngle          회전 각도 소수점 2자리까지 가능(양수:CW(정방향), 음수:CCW(역방향))
      * @param[in]  ptSpeed         속도설정(TnmcX_SPEED_INFO 구조체 포인터)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_IArc(short nXNo, short nMapNo, short nPosType, double[] pdCent, double dAngle, ref TnmcX_SPEED_INFO ptSpeed);

    /**
      * @brief      설정된 보간 MAP으로 2축 원호(중심점과 종료점) 보간을 합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nMapNo          보간 Map 번호(0~15)
      * @param[in]  nPosType        위치모드 ::EPosType(0=상대위치, 1=절대위치)
      * @param[in]  pdCent          중심점 배열 포인터
      * @param[in]  pdEnd           종료점 배열 포인터
      * @param[in]  nDir            회전 방향 ::EDirection(0:CW(정방향), 1:CCW(역방향))
      * @param[in]  ptSpeed         속도설정(TnmcX_SPEED_INFO 구조체 포인터)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_IArcCE(short nXNo, short nMapNo, short nPosType, double[] pdCent, double[] pdEnd, short nDir, ref TnmcX_SPEED_INFO ptSpeed);

    /**
      * @brief      설정된 보간 MAP으로 2축 원호(통과점과 종료점) 보간을 합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nMapNo          보간 Map 번호(0~15)
      * @param[in]  nPosType        위치모드 ::EPosType(0=상대위치, 1=절대위치)
      * @param[in]  pdPass          통과점 배열 포인터
      * @param[in]  pdEnd           종료점 배열 포인터
      * @param[in]  ptSpeed         속도설정(TnmcX_SPEED_INFO 구조체 포인터)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_IArcPE(short nXNo, short nMapNo, short nPosType, double[] pdPass, double[] pdEnd, ref TnmcX_SPEED_INFO ptSpeed);

    /**
      * @brief      설정된 보간 MAP으로 2축 원호(반지름과 종료점) 보간을 합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nMapNo          보간 Map 번호(0~15)
      * @param[in]  dRadius         반지름
      * @param[in]  nDistType       원호 이동 거리 지정(0=짧은거리, 1=긴거리)
      * @param[in]  nPosType        위치모드 ::EPosType(0=상대위치, 1=절대위치)
      * @param[in]  pdEnd           종료점 배열 포인터
      * @param[in]  nDir            회전 방향 ::EDirection(0:CW(정방향), 1:CCW(역방향))
      * @param[in]  ptSpeed         속도설정(TnmcX_SPEED_INFO 구조체 포인터)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_IArcRE(short nXNo, short nMapNo, double dRadius, short nDistType, short nPosType, double[] pdEnd,  short nDir, ref TnmcX_SPEED_INFO ptSpeed);

    /**
      * @brief      설정된 보간 MAP으로 3축 헬리컬(중심점과 회전 각도) 보간을 합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nMapNo          보간 Map 번호(0~15)
      * @param[in]  nPosType        위치모드 ::EPosType(0=상대위치, 1=절대위치)
      * @param[in]  pdCent          중심점 배열 포인터
      * @param[in]  dAngle          회전 각도(양수:CW(정방향), 음수:CCW(역방향))
      * @param[in]  nZPosType       Z축 위치모드 ::EPosType(0=상대위치, 1=절대위치)
      * @param[in]  dZPos           Z 방향 이동 위치
      * @param[in]  ptSpeed         속도설정(TnmcX_SPEED_INFO 구조체 포인터)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_IHelical(short nXNo, short nMapNo, short nPosType, double[] pdCent, double dAngle, short nZPosType, double dZPos, ref TnmcX_SPEED_INFO ptSpeed);

    /**
      * @brief      설정된 보간 MAP으로 3축 헬리컬(중심점과 종료점) 보간을 합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nMapNo          보간 Map 번호(0~15)
      * @param[in]  nPosType        위치모드 ::EPosType(0=상대위치, 1=절대위치)
      * @param[in]  pdCent          중심점 배열 포인터
      * @param[in]  pdEnd           종료점 배열 포인터
      * @param[in]  nZPosType       Z축 위치모드 ::EPosType(0=상대위치, 1=절대위치)
      * @param[in]  dZPos           Z 방향 이동 위치
      * @param[in]  ptSpeed         속도설정(TnmcX_SPEED_INFO 구조체 포인터)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_IHelicalCE(short nXNo, short nMapNo, short nPosType, double[] pdCent, double[] pdEnd, short nZPosType, double dZPos, short nDir, ref TnmcX_SPEED_INFO ptSpeed);

    /**
      * @brief      설정된 보간 MAP으로 3축 헬리컬(통과점과 종료점) 보간을 합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nMapNo          보간 Map 번호(0~15)
      * @param[in]  nPosType        위치모드 ::EPosType(0=상대위치, 1=절대위치)
      * @param[in]  pdPass          통과점 배열 포인터
      * @param[in]  pdEnd           종료점 배열 포인터
      * @param[in]  nZPosType       Z축 위치모드 ::EPosType(0=상대위치, 1=절대위치)
      * @param[in]  dZPos           Z 방향 이동 위치
      * @param[in]  ptSpeed         속도설정(TnmcX_SPEED_INFO 구조체 포인터)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_IHelicalPE(short nXNo, short nMapNo, short nPosType, double[] pdPass, double[] pdEnd, short nZPosType, double dZPos, ref TnmcX_SPEED_INFO ptSpeed);

    /**
      * @brief      설정된 보간 MAP으로 3축 헬리컬(반지름과 종료점) 보간을 합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nMapNo          보간 Map 번호(0~15)
      * @param[in]  dRadius         반지름
      * @param[in]  nDistType       원호 이동 거리 지정(0=짧은거리, 1=긴거리)
      * @param[in]  nPosType        위치모드 ::EPosType(0=상대위치, 1=절대위치)
      * @param[in]  pdEnd           종료점 배열 포인터
      * @param[in]  nZPosType       Z축 위치모드 ::EPosType(0=상대위치, 1=절대위치)
      * @param[in]  ndZPos          Z 방향 이동 위치
      * @param[in]  nDir            회전 방향 ::EDirection(0:CW(정방향), 1:CCW(역방향))
      * @param[in]  ptSpeed         속도설정(TnmcX_SPEED_INFO 구조체 포인터)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_IHelicalRE(short nXNo, short nMapNo, double dRadius, short nDistType, short nPosType, double[] pdEnd, short nZPosType, double dZPos, short nDir, ref TnmcX_SPEED_INFO ptSpeed);

    /**
      * @brief      설정된 보간 MAP을 확인 합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nMapNo          보간 Map 번호(0~15)
      * @param[out] ptIStatus       TnmcX_I_STATUS 구조체 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_IMapGet(short nXNo, short nMapNo, out TnmcX_I_STATUS ptIStatus);

    /**
      * @brief      CP모드 Pre-Register사용유무 설정
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  wUseMask        사용 유무를 16bit(CP최대 16축) Bit Mask형태로 설정(Bit별 0=사용X, 1=사용)
                --------------------------Mask의 Bit별 Group, Axis--------------------
                  Bit|15 |14 |13 |12 |11 |10 | 9 | 8 | 7 | 6 | 5 | 4 | 3 | 2 | 1 | 0 |
                =====|===+===+===+===+===+===+===+===+===+===+===+===+===+===+===+===|
                Group|       3       |       2       |       1       |       0       |
                =====|=======+=======+=======+=======+=======+=======+=======+=======|
                 Axis| 3 | 2 | 1 | 0 | 3 | 2 | 1 | 0 | 3 | 2 | 1 | 0 | 3 | 2 | 1 | 0 |
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_CP_PreRegUseSet(short nXNo, ushort wUseMask);

    /**
      * @brief      CP모드 Pre-Register사용유무 설정 Mask값 읽기
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  pwRetUseMask    사용유무 Mask값 읽어 반환받을 16bit형의 포인터(Bit별 0=사용X, 1=사용)
                    --------------------------Mask의 Bit별 Group, Axis--------------------
                      Bit|15 |14 |13 |12 |11 |10 | 9 | 8 | 7 | 6 | 5 | 4 | 3 | 2 | 1 | 0 |
                    =====|===+===+===+===+===+===+===+===+===+===+===+===+===+===+===+===|
                    Group|       3       |       2       |       1       |       0       |
                    =====|=======+=======+=======+=======+=======+=======+=======+=======|
                     Axis| 3 | 2 | 1 | 0 | 3 | 2 | 1 | 0 | 3 | 2 | 1 | 0 | 3 | 2 | 1 | 0 |
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_CP_PreRegUseGet(short nXNo, ushort[] pwRetUseMask);

    /**
      * @brief      CP모드 남은 Pre-Register갯수 읽기
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  pnSIDList       남은 갯수를 반환받을 배열 포인터(배열 수=16=CP_GROUP_AXES*CP_MAX_GROUP)
                    ----------------------배열 Index별 Group, Axis------------------------
                    Index|15 |14 |13 |12 |11 |10 | 9 | 8 | 7 | 6 | 5 | 4 | 3 | 2 | 1 | 0 |
                    =====|===+===+===+===+===+===+===+===+===+===+===+===+===+===+===+===|
                    Group|       3       |       2       |       1       |       0       |
                    =====|=======+=======+=======+=======+=======+=======+=======+=======|
                     Axis| 3 | 2 | 1 | 0 | 3 | 2 | 1 | 0 | 3 | 2 | 1 | 0 | 3 | 2 | 1 | 0 |
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_CP_PreRegGetRemain(short nXNo, short[] pnRetList);

    /**
      * @brief      [시퀸스] 함수를 등록할 입력대상 시퀸스 번호 지정
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSNo            시퀸스는 4개로 구성되므로 0~3
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SeqSetFocus(short nXNo, short nSNo);

    /**
      * @brief      [시퀸스]함수 입력대상 지정 해제
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SeqKillFocus(short nXNo);

    /**
      * @brief      [시퀸스] 함수 등록버퍼 초기화
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSNo            초기화할 시퀸스 번호(0~3)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SeqFuncBufClear(short nXNo, short nSNo);

    /**
      * @brief      [시퀸스] 함수등록 완료
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSNo            등록완료할 시퀸스 번호(0~3)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SeqFuncAddEnd(short nXNo, short nSNo);

    /**
      * @brief      [시퀸스]환경설정
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSeqNo          환경설정할 시퀀스 번호(0~3)
      * @param[in]  nWaitEmpty      Function 등록 대기 설정 (0=강제종료, 1=함수등록 될때까지 대기)
      * @param[in]  nWaitMovDone    이동Function일 경우 이동에 지정된 서보가 이동중일때 대기 설정 (0=대기안함, 1=이동완료대기, 2=이동완료와 In-Position대기)
      * @param[in]  nAutoHold       함수 수행후 자동 일시중지(0=계속실행, 1=AutoHold)
      * @see        nmcX_SeqStart
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SeqSetCfg(short nXNo, short nSNo, short nWaitEmpty, short nWaitMovDone, short nAutoHold);

    /**
      * @brief      [시퀸스]실행시작
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSeqNo          시작할 시퀀스 번호(0~3)
      * @see        nmcX_SeqStop, nmcX_SeqPuase, nmcX_SeqResume
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SeqStart(short nXNo, short nSNo);

    /**
      * @brief      [시퀸스]정지
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSeqNo          정지할 시퀀스 번호(0~3)
      * @param[in]  nStopMode       정지모드 ::EStopMode(0 = 긴급, 1 = 감속)
      * @see        nmcX_SeqStart, nmcX_SeqPuase, nmcX_SeqResume
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SeqStop(short nXNo, short nSNo, short nStopMode);

    /**
      * @brief      [시퀸스]일시중지
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSeqNo          일시 중지할 시퀀스 번호(0~3)
      * @see        nmcX_SeqStop, nmcX_SeqStart, nmcX_SeqResume
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SeqPause(short nXNo, short nSNo);

    /**
      * @brief      [시퀸스]Resume
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSeqNo          Resume할 시퀀스 번호(0~3)
      * @see        nmcX_SeqStart, nmcX_SeqStop, nmcX_SeqStart
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SeqResume(short nXNo, short nSNo);

    /**
      * @brief      [시퀸스] 전체 시퀀스 상태를 확인합니다.
      * @param[in]  nXNo                연결된 컨트롤러 번호
      * @param[out] ptRetASeqStatus     반환 받을 TnmcX_A_SEQ_STATUS 구조체 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SeqGetAllStatus(short nXNo, out TnmcX_A_SEQ_STATUS ptRetASeqStatus);

    /**
      * @brief      [시퀸스] 시퀀스 상태를 확인합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSeqNo          시퀀스 번호(0~3)
      * @param[out] ptRetStatus     상태 구조체 정보
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SeqGetStatus(short nXNo, short nSeqNo, out TnmcX_SEQ_STATUS ptRetStatus);

    /**
      * @brief      [시퀸스 함수]지정 서보들 이동완료 대기
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nChkInPos       In-Position신호 체크 유무(0=체크 안함, 1=체크)
      * @param[in]  nSlaveCount     지정할 Slave 수량
      * @param[in]  pnSIDList       Slave ID 배열 포인터(배열 수 = Slave 수량)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SeqFuncWaitMovDone(short nXNo, short nChkInPos, short nSlaveCount, short[] pnSIDList);

    /**
      * @brief      [시퀸스 함수]Pre-Register Empty대기
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nWantCount      원하는 비어있는 Pre-Register갯수(1~3)
      * @param[in]  nSlaveCount     지정할 Slave 수량
      * @param[in]  pnSIDList       Slave ID 배열 포인터(배열 수 = Slave 수량)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SeqFuncWaitPreRegEmpty(short nXNo, short nWantCount, short nSlaveCount, short[] pnSIDList);

    /**
      * @brief      [시퀸스 함수]MDI의 복수의 Pin을 지정하여(PinMask) 원하는 입력값과 비교하여 일치할때 까지 대기
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  dwPinMask       비교할 복수의 Pin(Mask형태로 지정)
      * @param[in]  dwValue         비교할 값
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SeqFuncWaitMDIpins(short nXNo, int dwPinMask, int dwValue);

    /**
      * @brief      [시퀸스 함수]MDI의 단일Pin을 지정하여 원하는 입력상태가 될때 까지 대기
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nPinNo          비교할 단일Pin번호
      * @param[in]  nStatus         비교할 상태값(0=OFF, 1=ON)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SeqFuncWaitMDIpin(short nXNo, short nPinNo, short nStatus);

    /**
      * @brief      [시퀸스 함수]지정된 RDI의 복수의 Pin을 지정하여(PinMask) 원하는 입력값과 비교하여 일치할때 까지 대기
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  dwPinMask       비교할 복수의 Pin(Mask형태로 지정)
      * @param[in]  dwValue         비교할 값
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SeqFuncWaitRDIpins(short nXNo, short nSID, int dwPinMask, int dwValue);

    /**
      * @brief      [시퀸스 함수]지정된 RDI의 단일Pin을 지정하여 원하는 입력상태가 될때 까지 대기
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nPinNo          비교할 단일Pin번호
      * @param[in]  nStatus         비교할 상태값(0=OFF, 1=ON)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SeqFuncWaitRDIpin(short nXNo, short nSID, short nPinNo, short nStatus);

    /**
      * @brief      [시퀸스 함수]지정된 서보의 SDI(SI-MON4,5)의 복수의 Pin을 지정하여(PinMask) 원하는 입력값과 비교하여 일치할때 까지 대기
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  dwPinMask       비교할 복수의 Pin(Mask형태로 지정)
      * @param[in]  dwValue         비교할 값
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SeqFuncWaitSDIpins(short nXNo, short nSID, int dwPinMask, int dwValue);

    /**
      * @brief      [시퀸스 함수]지정된 서보의 SDI(SI-MON4,5)의 단일Pin을 지정하여 원하는 입력상태가 될때 까지 대기
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nPinNo          비교할 단일Pin번호(4,5)
      * @param[in]  nStatus         비교할 상태값(0=OFF, 1=ON)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SeqFuncWaitSDIpin(short nXNo, short nSID, short nPinNo, short nStatus);

    /**
      * @brief      [시퀸스 함수] Delay
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  lDelayMs        대기시간(ms)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SeqFuncDelay(short nXNo, int lDelayMs);

    /**
      * @brief      [리스트모션] 환경 설정 및 노드 등록을 시작합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nEmptyWait      구동 할 노드가 비어있을 경우 대기 모드 (0 = 종료, 1 = 대기)
      * @param[in]  nIOPinMask      MDO Pin Mask(8Bit)
      * @param[in]  nPinOutEnd      리스트 모션 종료 시 Mask MDO 출력 (8Bit)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_LmSetCfg(short nXNo, short nSID, short nEmptyWait, short nPinOutMask, short nPinOutEnd);

    /**
      * @brief      [리스트모션] 노드를 등록합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nNodeCount      등록 할 노드 개수(최대 50개 가능)
      * @param[in]  pdPos           위치 배열 포인터
      * @param[in]  pdStart         시작 속도의 배열 포인터
      * @param[in]  pdAcc           가속도의 배열 포인터
      * @param[in]  pdDrive         구동 속도의 배열 포인터
      * @param[in]  pdDec           감속도의 배열 포인터
      * @param[in]  pnMode          등록되는 노드의 이동모드
                                    (0 : 상대위치/사다리꼴,     1 : 절대위치/사다리꼴
                                     2 : 상대위치/S-Curve,      3 : 절대위치/S-Curve)
      * @param[in]  pnPinOut        출력 제어 (8Bit)
      * @param[in]  pnRetResp       반환 받을 응답 값의 포인터
      * @param[in]  nRetRemainN     반환 받을 남은 노드 개수 값의 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_LmAddNode(short nXNo, short nSID, short nNodeCount, double[] pdPos,
                                        double[] pdStart, double[] pdAcc, double[] pdDrive, double[] pdDec,
                                        short[] pnMode, short[] pnPinOut, out short pnRetResp, out short pnRetRemainN);

    /**
      * @brief      [리스트모션] 노드등록을 종료합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  dEndSpeed       리스트 모션 종료 시 속도
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_LmAddNodeEnd(short nXNo, short nSID, double dEndSpeed);

    /**
      * @brief      [리스트모션] 구동 시작/정지를 명령합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nRunMode        리스트 모션 구동 설정 (0 = 정지, 1 = 시작)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_LmRunStop(short nXNo, short nSID, short nRunMode);

    /**
      * @brief      전체 Servo의 List Motion 상태를 확인합니다.
      * @param[in]  nXNo                연결된 컨트롤러 번호
      * @param[out] ptRetALmStatus      반환 받을 TnmcX_A_LM_STATUS 구조체 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_LmGetAllStatus(short nXNo, out TnmcX_A_LM_STATUS ptRetALmStatus);

    /**
      * @brief      지정 Servo의 List Motion 상태를 확인합니다.
      * @param[in]  nXNo                연결된 컨트롤러 번호
      * @param[in]  nSID                Slave ID
      * @param[out] ptRetLmStatus       반환 받을 TnmcX_LM_STATUS 구조체 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_LmGetStatus(short nXNo, short nSlD, out TnmcX_LM_STATUS ptRetLmStatus);

    /**
      * @brief      지정 Servo에 Torque Limit를 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nMValue         - 토크 제한 값
      * @param[in]  nPValue         + 토크 제한 값
      * @param[in]  nKeepTime       유지 시간
      * @param[in]  nIOOnMask       MDO 출력 On Mask(8Bit)
      * @param[in]  nIOOffMask      MDO 출력 Off Mask(8Bit)
      * @param[in]  lDeviExcess     위치 편차 초과 범위
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_TorqueLimitSet(short nXNo, short nSID, short nMValue, short nPValue, short nKeepTime, short nIOOnMask, short nIOOffMask, int lDeviExcess);

    /**
      * @brief      여러 Servo를 지정하여 Torque Limit를 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 설정할 Slave의 개수
      * @param[in]  pnSIDList       설정할 Slave ID의 배열 포인터(배열 수 = Slave의 개수)
      * @param[in]  pnMValue        - 토크 제한 값 배열 포인터
      * @param[in]  pnPValue        + 토크 제한 값 배열 포인터
      * @param[in]  pnKeepTime      유지 시간  배열 포인터
      * @param[in]  pnIOOnMask      MDO 출력 On Mask(8Bit) 배열 포인터
      * @param[in]  pnIOOffMask     MDO 출력 Off Mask(8Bit) 배열 포인터
      * @param[in]  plDevExcess     위치 편차 초과 범위 배열 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mTorqueLimitSet(short nXNo, short nSlaveCount, short[] pnSIDList, short[] pnMValue, short[] pnPValue,
                                                    short[] pnKeepTime, short[] pnIOOnMask, short[] pnIOOffMask, int[] plDeviExcess);

    /**
      * @brief      지정 Servo의 Torque Limit를 해제합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  lDeviExcess     위치 편차 초과 범위
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_TorqueLimitFree(short nXNo, short nSID, int lDeviExcess);

    /**
      * @brief      여러 Servo를 지정하여 Torque Limit를 해제합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 설정할 Slave의 개수
      * @param[in]  pnSIDList       설정할 Slave ID의 배열 포인터(배열 수 = Slave의 개수)
      * @param[in]  plDevExcess     위치 편차 초과 범위 배열 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mTorqueLimitFree(short nXNo, short nSlaveCount, short[] pnSIDList, int[] plDeviExcess);

    /**
      * @brief      지정 Slave의 Torque Limit 상태를 확인합니다.
      * @param[in]  nXNo                연결된 컨트롤러 번호
      * @param[in]  nSID                Slave ID
      * @param[out] ptRetTLimitStatus   반환 받을 TnmcX_T_Limit_STATUS 구조체 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_TorqueLimitGetStatus(short nXNo, short nSID, out TnmcX_T_LIMIT_STATUS ptRetTLimitStatus);
    
    
    /**
      * @brief      지정 Servo에 전환 기능 입력 신호를 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
  	  * @param[in]  nGain			Gain전환 신호(Gain_SW) 설정값(0,1, 그외는 변경안함)
  	  * @param[in]  nTLimit         Torque Limit전환 신호(TL_SW) 설정값(0,1, 그외는 변경안함)
  	  * @param[in]  nSLimit			속도 제한 전환 신호(SL_SW) 설정값(0,1, 그외는 변경안함)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_GainTSLimitSwSet(short nXNo, short nSID,  short nGain, short nTLimit, short nSLimit);

    /**
      * @brief      여러 Servo를 지정하여 전환 기능 입력 신호를 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     설정할 Slave 의 개수
      * @param[in]  pnSIDList       설정할 Slave ID의 배열 포인터(배열 수 = Slave의 개수)
  	  * @param[in]  pnGain			Gain전환 신호(Gain_SW) 설정값 배열 포인터(0,1, 그외는 변경안함)
  	  * @param[in]  pnTLimit        Torque Limit전환 신호(TL_SW) 설정값 배열 포인터(0,1, 그외는 변경안함)
  	  * @param[in]  pnSLimit		속도 제한 전환 신호(SL_SW) 설정값 배열 포인터(0,1, 그외는 변경안함)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mGainTSLimitSwSet(short nXNo, short nSlaveCount, short[] pnSIDList, short[] pnGain, short[] pnTLimit, short[] pnSLimit);
    

    /**
      * @brief      지정 Servo에 설정된 전환 기능 입력신호를 확인합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
  	  * @param[in]  pnRetGain		반환받을 Gain전환 신호(Gain_SW)의 변수 포인터
  	  * @param[in]  pdRetTLimit		반환받을 Torque Limit 전환 신호(TL_SW)의 변수 포인터
  	  * @param[in]  pnRetSLimit		반환받을 속도 제한 전환 신호(SL_SW)의 변수 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_GainTSLimitSwGet(short nXNo, short nSID, out short pnRetGain, out short pnRetTLimit, out short pnRetSLimit);

    /**
      * @brief      여러 Servo를 지정하여 설정된 전환 기능 입력신호를 확인합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     확인할 Slave의 개수
      * @param[in]  pnSIDList       확인할 Slave ID의 배열 포인터(배열 수 = Slave의 개수)
  	  * @param[in]  pnRetGain		반환받을 Gain전환 신호(Gain_SW)의 배열 포인터
  	  * @param[in]  pdRetTLimit		반환받을 Torque Limit 전환 신호(TL_SW)의 배열 포인터
  	  * @param[in]  pnRetSLimit		반환받을 속도 제한 전환 신호(SL_SW)의 배열 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mGainTSLimitSwGet(short nXNo, short nSlaveCount, short[] pnSIDList, short[] pnRetGain, short[] pnRetTLimit, short[] pnRetSLimit);


    /**
      * @brief      지정 Servo에 SW Limit를 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nMStopMode      -SW Limit 정지모드 ::EStopMode(0 = 긴급, 1 = 감속, 2 = 비사용)
      * @param[in]  dMLimitPos      -SW Limit 위치
      * @param[in]  nPStopMode      +SW Limit 정지모드 ::EStopMode(0 = 긴급, 1 = 감속, 2 = 비사용)
      * @param[in]  dPLimitPos      +SW Limit 위치
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_StopModeSwLimit(short nXNo, short nSID, short nMStopMode, double dMLimitPos, short nPStopMode, double dPLimitPos);

    /**
      * @brief      지정 Servo에 설정된 SW Limit를 확인합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  pnMStopMode     확인할 -SW Limit 정지모드 ::EStopMode(0 = 긴급, 1 = 감속, 2 = 비사용)
      * @param[in]  pdMLimitPos     확인할 -SW Limit 위치
      * @param[in]  pnPStopMode     확인할 +SW Limit 정지모드 ::EStopMode(0 = 긴급, 1 = 감속, 2 = 비사용)
      * @param[in]  pdPLimitPos     확인할 +SW Limit 위치
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SwLimitGet(short nXNo, short nSID, out short pnRetMStopMode, out double pdRetMLimitPos, out short pnRetPStopMode, out double pdRetPLimitPos);

    /**
      * @brief      여러 Servo를 지정하여 설정된 SW Limit를 확인합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     확인할 Slave의 개수
      * @param[in]  pnSIDList       확인할 Slave ID의 배열 포인터(배열 수 = Slave의 개수)
      * @param[in]  pnMStopMode     확인할 -SW Limit 정지모드 배열 포인터 ::EStopMode(0 = 긴급, 1 = 감속, 2 = 비사용)
      * @param[in]  pdMLimitPos     확인할 -SW Limit 위치 배열 포인터
      * @param[in]  pnPStopMode     확인할 +SW Limit 정지모드 배열 포인터 ::EStopMode(0 = 긴급, 1 = 감속, 2 = 비사용)
      * @param[in]  pdPLimitPos     확인할 +SW Limit 위치 배열 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mSwLimitGet(short nXNo, short nSlaveCount, short[] pnSIDList, short[] pnRetMStopMode, double[] pdRetMLimitPos, short[] pnRetPStopMode, double[] pdRetPLimitPos);

    /**
      * @brief      센서 On 시 정지모드를 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nSensor         센서 (0 = -Limit, 1= +Limit, 2 = Home, 3 = Stopper Torque Limit)
      * @param[in]  nStopMode       정지모드 ::EStopMode(0 = 긴급, 1 = 감속, 2 = 정지 안함)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_StopModeSensorOn(short nXNo, short nSID, short nSensor, short nStopMode);

    /**
      * @brief      통신 연결 체크 시간 경과 후 정지모드를 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  lCheckTime      통신 체크 시간
      * @param[in]  nStopMode       정지모드 ::EStopMode(0 = 긴급, 1 = 감속)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_StopModeDisconnected(short nXNo, int lCheckTime, short nStopMode);

    /**
      * @brief      MPG 기능을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID(0보다 작으면 CP Axis로 지정)
      * @param[in]  nCPGroup        Slave ID가 0보다 작은 경우 지정 CP 제어 Group (0~3)
      * @param[in]  nCPAxis         Slave ID가 0보다 작은 경우 지정 CP 제어 축 번호(0~3)
      * @param[in]  nRunMode        구동 모드 ::EMpgRunMode(0 = 사용안함, 1 = Bypass, 2 = Step 실행, 3=Counter)
      * @param[in]  nInMode         입력 모드(0 = 1체배, 1 = 2체배, 2 = 4체배, 3 = 2Pulse)
      * @param[in]  nDir            방향 ::EDirection(0 = CW, 1 = CCW)
      * @param[in]  nMul            체배(1~32)
      * @param[in]  nDiv            분주비(1 ~ 2048)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_MpgCfgSet(short nXNo, short nSID, short nCPGroup, short nCPAxis, short nRunMode, short nInMode, short nDir, short nMul, short nDiv);

    /**
      * @brief      모든 CP축의 MPG설정값을 모두 가져옵니다.(배열수[16]=CP그룹[4] X CP축[4])
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  pnRetSID        (배열수=16개)리턴받을 Slave ID 배열 포인터(MPG와 연동될 Slave ID이며 0보다 작으면 Slave가 연동되지 않은 경우)
      * @param[in]  pnRetRunMode    (배열수=16개)리턴받을 구동모드 배열 포인터
                                    ::EMpgRunMode(0 = 사용안함, 1 = Bypass, 2 = Step 실행, 3=Counter)
      * @param[in]  pnRetInMode     (배열수=16개)리턴받을 입력모드 배열 포인터(0 = 1체배, 1 = 2체배, 2 = 4체배, 3 = 2Pulse)
      * @param[in]  pnRetDir        (배열수=16개)리턴받을 방향 배열 포인터 ::EDirection(0 = CW, 1 = CCW)
      * @param[in]  pnRetMul        (배열수=16개)리턴받을 체배 배열 포인터(1~32)
      * @param[in]  pnRetDiv        (배열수=16개)리턴받을 분주비 배열 포인터(1 ~ 2048)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_MpgCfgGet(short nXNo, short[] pnRetSID, short[] pnRetRunMode, short[] pnRetInMode, short[] pnRetDir, short[] pnRetMul, short []pnRetDiv);

    /**
      * @brief      모든 CP축의 MPG Counter값을 모두 가져옵니다.(배열수[16]=CP그룹[4] X CP축[4])
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  plRetCount      리턴받을 카운트 배열 포인터(배열수=16개)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_MpgGetCounter(short nXNo, int[] plRetCount);

    /**
      * @brief      지정 Servo에 Alarm Reset을 요청합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_AlarmResetReq(short nXNo, short nSID);

    /**
      * @brief      여러 Servo를 지정하여 Alarm Reset을 요청합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 요청할 Slave의 배열 개수
      * @param[in]  pnSIDList       요청할 Slave ID 배열 포인터(배열 수 = Slave의 개수)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mAlarmResetReq(short nXNo, short nSlaveCount, short[] pnSIDList);

    /**
      * @brief      지정 Servo의 Alarm List(내역)을 확인합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nIndex          확인할 알람 내역의 Index(0~14)
      * @param[out] pnRetAlarmMain  알람 Code의 Main Number을 반환 받을 포인터
      * @param[out] pnRetAlarmSub   알람 Code의 Sub Number을 반환 받을 포인터
      * @param[out] pnRetWarning    Warning Code을 반환 받을 포인터(nIndex가 0일때에만 유효한 값)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_AlarmListGet(short nXNo, short nSID, short nIndex, out short pnRetAlarmMain, out short pnRetAlarmSub, out short pnRetWarning);

    /**
      * @brief      여러 Servo를 지정하여 Alarm List(내역)을 확인합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 확인할 Slave의 배열 개수
      * @param[in]  pnSIDList       반환할 Slave ID 배열 포인터(배열 수 = Slave의 개수)
      * @param[in]  nIndex          확인할 알람 내역의 Index
      * @param[out] pnRetAlarmMain  알람 Code의 Main Number의 반환 받을 배열의 포인터
      * @param[out] pnRetAlarmSub   알람 Code의 Sub Number의 반환 받을 배열의 포인터
      * @param[out] pnRetWarning    Warning Code을 반환 받을 배열의 포인터(nIndex가 0일때에만 유효한 값)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mAlarmListGet(short nXNo, short nSlaveCount, short[] pnSIDList, short nIndex, short[] pnRetAlarmMain, short[] pnRetAlarmSub, short[] pnRetWarning);

    /**
      * @brief      Alarm List(내역)을 삭제합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_AlarmListClear(short nXNo, short nSID);

    /**
      * @brief      여러 Servo를 지정하여 Alarm List(내역)을 삭제합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 삭제할 Slave의 배열 개수
      * @param[in]  pnSIDList       삭제할 Slave ID 배열 포인터(배열 수 = Slave의 개수)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mAlarmListClear(short nXNo, short nSlaveCount, short[] pnSIDList);

    /**
      * @brief      Servo의 환경을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nSaveROM        ROM에 저장 여부 (0 = 저장 안함, 1 = 저장)
      * @param[in]  nMode           모션 제어 모드 ::ESlaveCtrlMode (0=변경안함, 1=PP, 2=CP, 3=CV, 4=CT)
      * @param[in]  nCPGroup        CP 제어 Group (0~3)
      * @param[in]  nCPAxis         CP 제어 축 (0~3)
      * @param[in]  nMirror         Mirror 축 설정 ::EMirror (0 = 없음, 1 = Main, 2 = Sub)
      * @param[in]  lAddInfo        모드 변화시 추가 정보\n
                                    PP, CP, CV:설정값 없음, CT=-5000~5000:변화시 Torque값, 그외:토크 유지
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_ServoPropertyCfg(short nXNo, short nSID, short nSaveROM, short nMode, short nCPGroup, short nCPAxis, short nMirror, int lAddInfo);

    /**
      * @brief      지정 Slave의 속성을 확인합니다.
      * @param[in]  nXNo                연결된 컨트롤러 번호
      * @param[in]  nSID                Slave 번호
      * @param[out] ptRetSlaveProp      반환 받을 TnmcX_SLAVE_PROPERTY 구조체 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SlavePropertyGet(short nXNo, short nSlD, out TnmcX_SLAVE_PROPERTY ptRetSlaveProp);

    /**
      * @brief      전체 Slave의 속성을 확인합니다.
      * @param[in]  nXNo                연결된 컨트롤러 번호
      * @param[out] ptRetASlaveProp     반환 받을 TnmcX_A_SLAVE_PROPERTY 구조체 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SlavePropertyGetAll(short nXNo, out TnmcX_A_SLAVE_PROPERTY ptRetASlaveProp);

    /**
      * @brief      지정 Servo의 Parameter를 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nClass          Parameter Class
      * @param[in]  nNo             Parameter No
      * @param[in]  lValue          Write 할 Parameter 값
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_ParamSet(short nXNo, short nSID, short nClass, short nNo, int lValue);

    /**
      * @brief      여러 Servo를 지정하여 Parameter를 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 Write할 Slave 의 개수
      * @param[in]  pnSIDList       Write할 Slave ID의 배열 포인터(배열 수 = Slave의 개수)
      * @param[in]  nClass          Parameter Class
      * @param[in]  nNo             Parameter No
      * @param[in]  plValue         Write할 Parameter의 배열 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mParamSet(short nXNo, short nSlaveCount, short[] pnSIDList, short nClass, short nNo, int[] plValue);
    
		/**
  	  * @brief      지정 Servo의 Parameter를 설정하고 응답된 데이터와 Status을 확인
  	  * @param[in]  nXNo            연결된 컨트롤러 번호
  	  * @param[in]  nSID            Slave ID
  	  * @param[in]  nClass          Parameter Class
  	  * @param[in]  nNo             Parameter No
  	  * @param[io]  plValue         Parameter에 Write할 값과 응답될 데이터가 들어갈 포인터
  	  * @param[out] pnRetStatus     Parameter설정에 대한 Status가 반환됩니다.(Bit:7=ERR, 6=WNG, 4=Busy, 3~0:각 파라메터 별로 상이)
  	  * @return     EnmcX_FUNC_RESULT
  	  */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_ParamExSet(short nXNo, short nSID, short nClass, short nNo, out int plValue, out short pnRetStatus);

    /**
  	  * @brief      여러 Servo를 지정하여 Parameter를 설정하고 응답된 데이터와 Status을 확인
  	  * @param[in]  nXNo            연결된 컨트롤러 번호
  	  * @param[in]  nSlaveCount     동시 Write할 Slave 의 개수
  	  * @param[in]  pnSIDList       Write할 Slave ID의 배열 포인터(배열 수 = Slave의 개수)
  	  * @param[in]  nClass          Parameter Class
  	  * @param[in]  nNo             Parameter No
  	  * @param[io]  plValue         지정된 Servo들에 Parameter를 Write할 값과 응답될 데이터가 들어갈 배열 포인터
  	  * @param[out] pnRetStatus     지정된 Servo들의 Parameter설정에 대한 Status가 반환될 배열 포인터.(Bit:7=ERR, 6=WNG, 4=Busy, 3~0:각 파라메터 별로 상이)
  	  * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mParamExSet(short nXNo, short nSlaveCount, short[] pnSIDList, short nClass, short nNo, int[] plValue, short[] pnRetStatus);

    /**
      * @brief      지정 Servo의 Parameter를 확인합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nClass          Parameter Class
      * @param[in]  nNo             Parameter No
      * @param[out] plRet           반환된 값을 받을 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_ParamGet(short nXNo,  short nSID, short nClass, short nNo, out int plRet);

    /**
      * @brief      여러 Servo를 지정하여 Parameter를 확인합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 Read할 Slave 의 개수
      * @param[in]  pnSIDList       Read할 Slave ID의 배열의 포인터(배열 수 = Slave의 개수)
      * @param[in]  nClass          Parameter Class
      * @param[in]  nNo             Parameter No
      * @param[out] plRet           반환된 값을 받을 배열 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mParamGet(short nXNo, short nSlaveCount, short[] pnSIDList, short nClass, short nNo, int[] plRet);
    
    /**
  	  * @brief      지정 Servo의 Parameter값과 상태를 확인
  	  * @param[in]  nXNo            연결된 컨트롤러 번호
  	  * @param[in]  nSID            Slave ID
  	  * @param[in]  nClass          Parameter Class
  	  * @param[in]  nNo             Parameter No
  	  * @param[out] plRetValue      반환된 값을 받을 포인터
  	  * @param[out] pnRetStatus     반횐된 Status를 받을 포인터(Bit:7=ERR, 6=WNG, 4=Busy, 3~0:각 파라메터 별로 상이)
  	  * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_ParamExGet(short nXNo,  short nSID, short nClass, short nNo, out int plRetValue, out short pnRetStatus);

    /**
  	  * @brief      여러 Servo를 지정하여 Parameter값과 상태를 확인
  	  * @param[in]  nXNo            연결된 컨트롤러 번호
  	  * @param[in]  nSlaveCount     동시 Read할 Slave 의 개수
  	  * @param[in]  pnSIDList       Read할 Slave ID의 배열의 포인터(배열 수 = Slave의 개수)
  	  * @param[in]  nClass          Parameter Class
  	  * @param[in]  nNo             Parameter No
  	  * @param[out] plRetValue      반환된 값을 받을 배열 포인터
  	  * @param[out] pnRetStatus     반환된 Status를 받을 배열 포인터(Bit:7=ERR, 6=WNG, 4=Busy, 3~0:각 파라메터 별로 상이)
  	  * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mParamExGet(short nXNo, short nSlaveCount, short[] pnSIDList, short nClass, short nNo, int[] plRetValue, short[] pnRetStatus);

    /**
      * @brief      지정 Servo의 Parameter를 유효 적용합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @return     EnmcX_FUNC_RESULT
      * @warning    Servo Off 상태에서 가능합니다.
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_CParamValid(short nXNo, short nSID);

    /**
      * @brief      여러 Servo를 지정하여 Parameter를 유효 적용합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 리셋할 Slave의 개수
      * @param[in]  pnSIDList       리셋할 Slave ID 배열 포인터(배열 수 = Slave의 개수)
      * @return     EnmcX_FUNC_RESULT
      * @warning    Servo Off 상태에서 가능합니다.
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mCParamValid(short nXNo, short nSlaveCount, short[] pnSIDList);

    /**
      * @brief      Parameter를 EEPROM에 저장합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_ParamWriteEeprom(short nXNo, short nSID);

    /**
      * @brief      여러 Servo를 지정하여 Parameter를 EEPROM에 저장합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 저장할 Slave의 개수
      * @param[in]  pnSIDList       저장할 Slave ID 배열 포인터(배열 수 = Slave의 개수)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mParamWriteEeprom(short nXNo, short nSlaveCount, short[] pnSIDList);

    /**
      * @brief      Servo의 System정보를 확인합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[out] szDesc          정보를 가져올 char형 배열 포인터(5*16Bytes)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_ParamGetSysID(short nXNo, short nSID, byte[] szDesc);

    /**
      * @brief      Servo의 Monitor값을 확인합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nTC             읽고자 하는 TypeCode값
      * @param[out] plRet           반환된 값을 받을 포인터
      * @return     EnmcX_FUNC_RESULT
      * @warning    상세내용은 메뉴얼을 참고하십시오.
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_MonitorRead(short nXNo, short nSID, short nTC, short nIdx, out int plRet);

    /**
      * @brief      여러 Servo를 지정하여 모니터(Monitor)값을 확인합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     동시 Read할 Slave 의 개수
      * @param[in]  pnSIDList       Read할 Slave ID의 배열의 포인터(배열 수 = Slave의 개수)
      * @param[in]  pnTCList        pnSIDList에서 지정된 Slave ID별 읽고자 하는 TypeCode값의 포인터(배열 수 = Slave의 개수)
      * @param[out] plRet           반환된 값을 받을 배열 포인터
      * @return     EnmcX_FUNC_RESULT
      * @warning    상세내용은 메뉴얼을 참고하십시오.
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mMonitorRead(short nXNo, short nSlaveCount, short[] pnSIDList, short[] pnTCList, short[] pnIdxList, int[] plRet);

    /**
      * @brief      지정 Servo의 EX_OUT 출력을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nExOut1         EX_OUT1의 출력 설정값(0~1)
      * @param[in]  nExOut2         EX_OUT2의 출력 설정값(0~1)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SDOSet(short nXNo, short nSID, short nExOut1, short nExOut2);

    /**
      * @brief      여러 Servo를 지정하여 서보의 EX_OUT 출력을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSlaveCount     Slave 의 개수
      * @param[in]  pnSIDList       Slave ID의 배열 포인터(배열 수 = Slave의 개수 통일)
      * @param[in]  pnExOut1        EX_OUT1의 출력 설정 배열 포인터
      * @param[in]  pnExOut2        EX_OUT2의 출력 설정 배열 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_mSDOSet(short nXNo, short nSlaveCount, short[] pnSIDList, short[] pnExOut1, short[] pnExOut2);

    /**
      * @brief      지정 Servo의 Pin을 지정하여 EX_OUT 출력을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nExOutNo        설정할 EX-OUT번호(1~2)
      * @param[in]  nStatus         출력 설정값(0~1)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SDOSetPin(short nXNo, short nSID, short nExOutNo, short nStatus);

    /**
      * @brief      지정 Servo의 EX_OUT 출력을 반전합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nExOut1Tog      EX-OUT1을 토글여부(0~1)
      * @param[in]  nExOut2Tog      EX-OUT2을 토글여부(0~1)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SDOSetTog(short nXNo, short nSID, short nExOut1Tog, short nExOut2Tog);

    /**
      * @brief      RTEX_DO 출력을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  lOutVal         출력 값(32Bit   0~0xFFFFFFFF)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_RDOSet(short nXNo, short nSID, int lOutVal);

    /**
      * @brief      전체 RTEX_DO 모든 출력pin(32pin)을 설정합니다.(전체=RTEX_DO를 ID순서대로 배열)
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nDOCount        설정할 RTEX_DO의 개수
      * @param[in]  plOutVal        출력 값(32Bit   0~0xFFFFFFFF) 배열 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_aRDOSet(short nXNo, short nDOCount, int[] plOutVal);

    /**
      * @brief      RTEX_DO 단일 Pin 출력을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nPinNo          핀 번호(0~31)
      * @param[in]  nStatus         출력 설정(0~1)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_RDOSetPin(short nXNo, short nSID, short nPinNo, short nStatus);

    /**
      * @brief      전체 RTEX_DO의 단일 Pin 출력을 설정합니다.(전체=RTEX_DO를 ID순서대로 배열)
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nIndex_____________
                      nIndex>=0     RTEX_DO의 Index(오름차순으로 정렬한 번호, ex)ID 2,6,1이면 [0]=1, [1]=2, [2]=6)
                      nIndex <0     전체 RTEX_DO의 Pin모두를 순차적으로 지정
      * @param[in]  nPinNo_____________
                      nIndex>=0     0~31 핀 번호
                      nIndex <0     0~(RTEX_DO개수*32pin-1) 핀 번호
      * @param[in]  nStatus         출력 설정(0~1)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_aRDOSetPin(short nXNo, short nIndex, short nPinNo, short nStatus);

    /**
      * @brief      RTEX_DO의 단일 Pin 토글 출력을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nPinNo          핀 번호(0~31)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_RDOSetTogPin(short nXNo, short nSID, short nPinNo);

    /**
      * @brief      전체 RTEX_DO의 단일 Pin 토글 출력을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nIndex_____________
                      nIndex>=0     RTEX_DO의 Index(오름차순으로 정렬한 번호, ex)ID 2,6,1이면 [0]=1, [1]=2, [2]=6)
                      nIndex <0     전체 RTEX_DO의 Pin모두를 순차적으로 지정
      * @param[in]  nPinNo_____________
                      nIndex>=0     0~31 핀 번호
                      nIndex <0     0~(RTEX_DO개수*32pin-1) 핀 번호
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_aRDOSetTogPin(short nXNo, short nIndex, short nPinNo);

    /**
      * @brief      RTEX_DO 여러 Pin의  출력을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nPinCount       핀 개수
      * @param[in]  pnPins          핀 번호(0 ~ 31) 배열 포인터(배열 개수 = 핀 개수)
      * @param[in]  pnStatus        출력 상태(0~1) 배열 포인터(배열 개수 = 핀 개수)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_RDOSetPins(short nXNo, short nSID, short nPinCount, short[] pnPins, short[] pnStatus);

    /**
      * @brief      전체 RTEX_DO의 여러 Pin 출력을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nPinCount       핀 개수
      * @param[in]  pnIndex_____________
                      nIndex[x]>=0  RTEX_DO의 Index배열의 포인터(오름차순으로 정렬한 번호, ex)ID 2,6,1이면 [0]=1, [1]=2, [2]=6)
                      nIndex[x] <0  전체 RTEX_DO의 Pin모두를 순차적으로 지정
      * @param[in]  pnPins_____________
                      nIndex[x]>=0  0~31 핀 번호 배열 포인터
                      nIndex[x] <0  0~(RTEX_DO개수*32pin-1) 핀 번호 배열 포인터
      * @param[in]  pnStatus        출력 상태(0~1) 배열 포인터(배열 개수 = 핀 개수)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_aRDOSetPins(short nXNo, short nPinCount, short[] pnIndex, short[] pnPins, short[] pnStatus);

    /**
      * @brief      RTEX_DO의 여러 Pin을 반전 출력합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nSID            Slave ID
      * @param[in]  nPinCount       핀 개수(최대 32)
      * @param[in]  pnPins          핀 번호(0 ~ 31) 배열 포인터(배열 수 = 핀 개수)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_RDOSetTogPins(short nXNo, short nSID, short nPinCount, short[] pnPins);

    /**
      * @brief      전체 RTEX_DO의 여러 Pin을 반전 출력합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nPinCount       핀 개수
      * @param[in]  pnIndex_____________
                      nIndex[x]>0   RTEX_DO의 Index배열의 포인터(오름차순으로 정렬한 번호, ex)ID 2,6,1이면 [0]=1, [1]=2, [2]=6)
                      nIndex[x]<0   전체 RTEX_DO의 Pin모두를 순차적으로 지정
      * @param[in]  pnPins_____________
                      nIndex[x]>0   0~31 핀 번호 배열 포인터
                      nIndex[x]<0   0~(RTEX_DO개수*32pin-1) 핀 번호 배열 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_aRDOSetTogPins(short nXNo, short nPinCount, short[] pnIndex, short[] pnPins);


    /**
      * @brief      RDO RTEX연결이 끊어져도 출력유지 설정
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  dwMask          출력유지 안함을 32 Slave Bit Mask형태로 설정(Bit별 0=유지안함, 1=유지)
                    -------------32Slave Mask의 Bit별 Slave--------------------
                      Bit | 31|...   .. ...  ..  ...| 5 | 4 | 3 | 2 | 1 | 0 |
                    ======|===+=====================+===+===+===+===+===+===|
                     Slave| 31|...   .. ...  ..  ...| 5 | 4 | 3 | 2 | 1 | 0 |
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_RDOLinkFailKeepOutSet(short nXNo, uint dwMask);

    /**
      * @brief      RDO RTEX연결이 끊어져도 출력유지 설정값 읽기
      * @param[in]  nXNo                연결된 컨트롤러 번호
      * @param[in]  pdwRetMask          출력유지 설정 값을 32Slave Bit Mask형태로 설정 읽어 반환받을 32bit형의 포인터(Bit별 0=유지안함, 1=유지)
                    -------------32Slave Mask의 Bit별 Slave--------------------
                      Bit | 31|...   .. ...  ..  ...| 5 | 4 | 3 | 2 | 1 | 0 |
                    ======|===+=====================+===+===+===+===+===+===|
                     Slave| 31|...   .. ...  ..  ...| 5 | 4 | 3 | 2 | 1 | 0 |
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_RDOLinkFailKeepOutGet(short nXNo, uint[] pdwRetMask);

    /**
      * @brief      컨트롤러의 UART 통신 Bypass 모드를 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nEnable         Bypass 모드 활성 여부 (0 = Disable, 1 = Enable)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_UARTSetBypass(short nXNo, short nEnable);

    /**
      * @brief      컨트롤러의 UART 환경을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  lBaudrate       통신 속도
      * @param[in]  nDataBit        데이터 Bit (7, 8, 9)
      * @param[in]  nStopBit        정지 Bit(1, 2)
      * @param[in]  nParity         Parity Bit (0 = None, 1 = Odd, 2 = Even)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_UARTSetCfg(short nXNo, int lBaudrate, short nDataBit, short nStopBit, short nParity);

    /**
      * @brief      컨트롤러의 UART 통신으로 Data를 송신합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  szTxBuf         Tx로 송신할 Data 배열 포인터
      * @param[in]  nTxLen          Tx로 송신할 Data 길이
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_UARTWrite(short nXNo, byte[] szTxBuf, short nTxLen);

    /**
      * @brief      컨트롤러의 UART 통신으로 수신된 Data를 읽어옵니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[out] szRxBuf         Rx에 수신된 Data를 반환 받을 배열 포인터
      * @param[in]  nRxBufSize      Rx에 수신된 Data를 반환 받을 배열의 크기
      * @param[out] pnRetLen        반환 된 Read Data 길이의 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_UARTRead(short nXNo, byte[] szRxBuf, short nRxBufSize, out short pnRetLen);

    /**
      * @brief      컨트롤러 MDO의 출력을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nOutVal         설정할 출력값(8pin이므로 8bit유효)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_MDOSet(short nXNo, short nOutVal);

    /**
      * @brief      컨트롤러 MDO의 단일 Pin 출력을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nPinNo          핀 번호
      * @param[in]  nStatus         출력 상태
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_MDOSetPin(short nXNo, short nPinNo, short nStatus);

    /**
      * @brief      컨트롤러 MDO의 단일 Pin의 출력을 반전합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nPinNo          핀 번호
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_MDOSetTogPin(short nXNo, short nPinNo);

    /**
      * @brief      컨트롤러 MDO의 여러 Pin의 출력을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nCount          핀 개수
      * @param[in]  pnPins          핀 출력 배열 포인터
      * @param[in]  pnStatus        출력 상태 배열 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_MDOSetPins(short nXNo, short nCount, short[] pnPins, short[] pnStatus);

    /**
      * @brief      컨트롤러 MDO의 여러 Pin의 출력을 반전합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nCount          핀 개수
      * @param[in]  pnPins          핀 출력 배열 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_MDOSetTogPins(short nXNo, short nCount, short[] pnPins);

    /**
      * @brief      컨트롤러 MDO의 Pin 출력 유지 시간 제한을 설정합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nPinNo          핀 번호(0 ~ 8)
      * @param[in]  nStatus         출력 상태(0 = 설정 안됨, 1 = On, 2 = Off)
      * @param[in]  lTime           유지 시간 (ms)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_MDOSetLimitTime(short nXNo, short nPinNo, short nStatus, int lTime);

    /**
      * @brief      컨트롤러 MDO의 Pin에 설정된 출력 유지 시간 제한을 확인합니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  nPinNo          핀 번호
      * @param[out] pnRetPinSet     Pin 설정을 반환받을 포인터(0 = 설정 안됨, 1 = On, 2 = Off)
      * @param[out] pnRetRunStatus  진행 상태 반환받을 포인터(0 = 설정 안됨, 1 = 제한 시간 진행 중, 2 = 제한 시간 완료)
      * @param[out] pnRetPinStatus  Pin의 상태 반환받을 포인터(0 = Off, 1 = On)
      * @param[out] plRetRemainTime 출력 유지 제한 남은 시간의 반환받을 포인터(ms)
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_MDOGetLimitTime(short nXNo, short nPinNo, out short pnRetPinSet, out short pnRetRunStatus, out short pnRetPinStatus, out int plRetRemainTime);

    /**
      * @brief      컨트롤러의 포트를 변경합니다.
      * @note       외부로부터 접속 시 보안상 정해진 포트만을 사용해야 할 경우 포트 번호를 변경하여 사용합니다.
                    포트포워딩을 통한 포트변경 시 사용됩니다.
      * @param[in]  nXNo            연결된 컨트롤러 번호
      * @param[in]  lPortNum        포트 번호
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_SetNetPortNum(short nXNo, int lPortNum);
    
    /**
		  * @brief      명령어들의 Trace 활성 여부
		  * @param[in]  nEnable         Trace 활성 여부(0 = Disable, 1 = Enable)
		  * @return     EnmcX_FUNC_RESULT
		  */
		[DllImport("nmcX.dll")]
    public static extern short nmcX_TraceEnable(short nEnable);

    /**************************************************************************************************/
    /* Status Check Functions                                                                         */
    /**************************************************************************************************/
    /**
      * @brief      Slave 모든상태 확인
      * @note       전체 상태는 식별을 위해 종류별로 정렬하여 확인 됩니다.\n
                    ID 순서로 정렬이 되며, 되도록 ID 순서를 지켜주셔야 응용프로그램 제작에 수월합니다.
      * @param[in]  nXNo                연결된 컨트롤러 번호
      * @param[out] ptRetAllStatus      반환받을 TnmcX_ALL_STATUS 구조체 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_GetAllStatus(short nXNo, out TnmcX_ALL_STATUS ptRetAllStatus);

    /**
      * @brief      지정 Servo의 상태를 확인합니다.
      * @param[in]  nXNo                연결된 컨트롤러 번호
      * @param[in]  nSID                Slave ID
      * @param[out] ptRetServoStatus    반환받을 TnmcX_SERVO_STATUS 구조체 포인터
      * @return     EnmcX_FUNC_RESULT
      */
    [DllImport("nmcX.dll")]
    public static extern short nmcX_GetServoStatus(short nXNo, short nSID, out TnmcX_SERVO_STATUS ptRetServoStatus);

  };  /*<! END Class NMCXR */
}; /*<! END nampspace PAIX_RTEX_MotionController */
/*<! END OF FILE */
/**************************************************************************************************/