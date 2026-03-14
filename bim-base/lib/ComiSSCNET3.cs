#if USE_COMI_SSCNET
using System;
using System.Runtime.InteropServices;

namespace ComiSSCNET3
{
    /// <summary>
    /// ComiLX540_SDK에 대한 요약 설명입니다.
    /// </summary>

    public unsafe class CMS
    {

        // -------------------------------------------------------------------
        // Declare Const Values in LX540 Library
        // -------------------------------------------------------------------
        const int LX540_MAX_SUPPORT_MASTER_BOARDS				= 16;
		const int LX540_MAX_SUPPORT_SLAVE_AXES					= 32;
        const int LX540_MAX_SUPPORT_INTERPOLATION_MOTION		= 16;
        const int LX540_MAX_SUPPORT_LISTED_MOTION				= 16;
        const int LX540_MAX_SUPPORT_LISTED_MOTION_USER_PARAM	= 4;
        const int LX540_MAX_MODULE_IN_SLAVE						= 9;
        const int LX540_MAX_SUPPORT_IN_NODE_AXES				= 2;
        const int MAX_NUM_PLATFORM	                            = 5;   // RTEX, MECHA, SSCNET
        const int MAX_NUM_FUNC                                  = 600;
        
        public enum SWITCH
        {
                OFF                       = 0,
                ON                        = 1
        }
               

        // -------------------------------------------------------------------
        // Types of Machine I/O Property ID (using to CfgSetMioProperty, CfgGetMioProperty)
        // -------------------------------------------------------------------
        public enum MIOID
        {
	        PEL_LOGIC =			0,	// Property Val [ LOGIC_A    | LOGIC_B ] (Default : LOGIC_A)
	        NEL_LOGIC =			1,	// Property Val [ LOGIC_A    | LOGIC_B ] (Default : LOGIC_A)
	        ORG_LOGIC =			2,	// Property Val [ LOGIC_A    | LOGIC_B ] (Default : LOGIC_A)
	        EL_MODE	=			3,	// Property Val [ 0 : Emg Stop | 1 : Stop  ] (Default : Emg Stop )
	        INP_EN	=			4,	// Property Val [ 0 : INP[D]   | 1 : INP[E]] (Default : INP[D]   )
	        CFSYNC_EN = 		    5,	// Property Val [ 0 : Disable  | 1 : Enable] (Default : Disable  )
			SVON_MODE =			6,	
			VELCTRL_MODE =		7,	// Property Val [ 0 : Position Control Mode  | 1 : Velocity Control Mode  (Default : Position Control Mode ) 
			VELCTRL_MAXRPM =		8,	///<속도 제어 모드 시 모터 최대 RPM
			VELCTRL_PPR =		9,	///<속도 제어 모드 시 모터 회전 당 펄스 수
			SWL_MODE	=			10,	// Property Val [ 0 : Emg Stop | 1 : Stop  ] (Default : Emg Stop )

	        /////////////////////////////////////////////////////////////////////
	        INVALID					// Used for enumerated type range checking
        }

        // -------------------------------------------------------------------
        // Types of Machine I/O State Bit Index (using to StReadMioStatuses)
        // -------------------------------------------------------------------
        public enum IoST
        {
	        INP			    =	   0,
	        RESERVED_00		=	    1,
	        HOMECOMPLETED	=   	2,
	        TORQUELIMITED	=   	3,
	        WARNING			=		4,
	        ALARM			=		5,
	        SVRDY			=		6,
	        SVON			=		7,
	        ELN			    =		8,
	        ELP			    =		9,
	        ORG			    =		10,
	        EX_IN1			=		11,
	        EX_IN2			=		12,
	        EX_IN3			=		13,
	        EX_IN4			=		14,
	        EMG_STP			=		15,
	        RESERVED_01		=		16,
	        RESERVED_02		=		20,
	        ZSPD			=		28,
	        DEN			    =		29,
	        BREAKON			=		30,
	        ZPOINT			=		31,

	        /////////////////////////////////////////////////////////////////////////
	        INVALID					// Used for enumerated type range checking
        }

        // -------------------------------------------------------------------
        // Types of Machine I/O Property Value (using to CfgSetMioProperty, CfgGetMioProperty)
        // -------------------------------------------------------------------
        public enum MIOVALUE
        {
	        LOGIC_A		=			0,
	        LOGIC_B		=			1,
	
	        //////////////////////////////////////////////////////////////////////////
	        LOGIC_INVALID					// Used for enumerated type range checking
        }

        // -------------------------------------------------------------------
        // Types of Motion State(using to StSxReadMotionState, StIxReadMotionState)
        // -------------------------------------------------------------------
        public enum MotorState
        {
	        STOP		    =			0,
	        IN_ACC	    =			1,
	        IN_WORKSPD   =			2,
	        IN_DEC	    =    		3,
	        IN_INISPD	=			4,
	        IN_WAIT	    =			5,
	
	        //////////////////////////////////////////////////////////////////////////
	        INVALID				// Used for enumerated type range checking
        }

        // -------------------------------------------------------------------
        // Types of Linear Operation Direction (using to SxVMoveStart, MxVMoveStart)
        // -------------------------------------------------------------------
        public enum Dir
        {
	        N			=			0,
	        P			=			1,

	        //////////////////////////////////////////////////////////////////////////
	        INVALID					// Used for enumerated type range checking
        }

        // Counter name //
        public enum CmCntr
        { 
	        COMM = 0, /*Command*/
	        FEED,		/*Feedback*/
	        DEV,		/*Deviation*/
	        GEN,		/*General*/ 

	        //////////////////////////////////////////////////////////////////////////
	        INVALID					// Used for enumerated type range checking
        }

        // Encoder Mode//
        public enum EncoderMode
        {
	        ABS,
	        INC
        }

        // Control Mode//
        public enum ControlMode
        {
	        POS,
	        VEL,
	        TOR
        }

        // -------------------------------------------------------------------
        // Types of Speed Mode Value (using to CfgSetSpeedPattern, CfgGetSpeedPattern)
        // -------------------------------------------------------------------
        public enum SpeedMode
        {
	        CONSTANT	    =		0,
	        TRAPEZOIDAL 	=		1,
	        SCURVE		    =		2,

	        //////////////////////////////////////////////////////////////////////////
	        INVALID					// Used for enumerated type range checking
        }

        // -------------------------------------------------------------------
        // Types of Backlash/Slip Correction Mode Value (using to SxSetCorrection, SxGetCorrection)
        // -------------------------------------------------------------------
        public enum CorrMode
        {
	        DISABLE		    =		0,
	        BACKLASH		=		1,
	        SLIP			=		2,

	        //////////////////////////////////////////////////////////////////////////
	        INVALID					// Used for enumerated type range checking
        }

        // -------------------------------------------------------------------
        // Types of Interpolation Map Index (using to IxMapAxes, IxUnMapAxes, ...)
        // -------------------------------------------------------------------
        public enum IxMAP
        {
	        MAP0			=		0,
	        MAP1			=		1,
	        MAP2			=		2,
	        MAP3			=		3,
	        MAP4			=		4,
	        MAP5			=		5,
	        MAP6			=		6,
	        MAP7			=		7,
	        MAP8			=		8,
	        MAP9			=		9,
	        MAP10			=		10,
	        MAP11			=		11,
	        MAP12			=		12,
	        MAP13			=		13,
	        MAP14			=		14,
	        MAP15			=		15,
	        //////////////////////////////////////////////////////////////////////////
	        INVALID	=       LX540_MAX_SUPPORT_INTERPOLATION_MOTION	// Used for enumerated type range checking
        }

        // -------------------------------------------------------------------
        // Types of interpolation mode(using to IxMapAxes)
        // -------------------------------------------------------------------
        public enum IxMODE
        {
	        LINEAR		= 0,
	        CIRCULAR	    = 1,
	        HELICARL	    = 2,
	        SPLINE		= 3,
		
	        //////////////////////////////////////////////////////////////////////////
	        INVALID					// Used for enumerated type range checking
        }

        // -------------------------------------------------------------------
        // Types of interpolation mode(using to ArcA, ArcP)
        // -------------------------------------------------------------------
        public enum IxARC_DIR
        {
	        ARC_CW		= 0,
	        ARC_CCW		= 1,

	        //////////////////////////////////////////////////////////////////////////
	        INVALID					// Used for enumerated type range checking
        }

		// -------------------------------------------------------------------
		// Types of S5 Home operation parameter ID
		// -------------------------------------------------------------------
		public enum HomeParamId
		{
			MODE				= 0,
			DIR				    = 1,
			OFFSET				= 2,
			EZ_CNT				= 3,
			
			//////////////////////////////////////////////////////////////////////////
			INVALID				// Used for enumerated type range checking
		} 

        // -------------------------------------------------------------------
        // Types of return to home mode(using to HomeSetConfig, HomeGetConfig)
        // -------------------------------------------------------------------
        public enum HomeMode
        {
            //PCI : 0번 모드
            //ORG On -> Stop
	        MODE0	= 0,
            //PCI : 1번 모드
            //ORG On -> Stop -> Back(Vr) -> ORG OFF -> Foward(Vr) -> ORG ON -> Stop
	        MODE1	= 1,
            //PCI : 2번 모드
            //ORG ON -> Slow Down(Vini) -> Stop in EZ Count
	        MODE2	= 2,
            //PCI : 6번 모드
            //EL ON -> Stop -> Back(Vr) -> EL OFF -> Stop
	        MODE3	= 3,
            //PCI : 7번 모드
            //EL ON -> Stop -> Back(Vr) -> Stop on EZ Count
	        MODE4	= 4,

	        //////////////////////////////////////////////////////////////////////////
	        INVALID				// Used for enumerated type range checking
        }

        // -------------------------------------------------------------------
        // Types of return to home position clear mode (using to HomeSetPosClrMode, HomeGetPosClrMode)
        // -------------------------------------------------------------------
        public enum PosClrMode
        {
	        MODE0		= 0,
	        MODE1		= 1,
	        MODE2		= 2,
	        KEEP	= 3,

	        //////////////////////////////////////////////////////////////////////////
	        INVALID				// Used for enumerated type range checking
        }

        // -------------------------------------------------------------------
        // Types of Listed Motion Map Index (using to LmStart, ...)
        // -------------------------------------------------------------------
        public enum LMMAP
        {
	        MAP0			=		0,
	        MAP1			=		1,
	        MAP2			=		2,
	        MAP3			=		3,
	        MAP4			=		4,
	        MAP5			=		5,
	        MAP6			=		6,
	        MAP7			=		7,
	        MAP8			=		8,
	        MAP9			=		9,
	        MAP10			=		10,
	        MAP11			=		11,
	        MAP12			=		12,
	        MAP13			=		13,
	        MAP14			=		14,
	        MAP15			=		15,
	        //////////////////////////////////////////////////////////////////////////
	        INVALID	=		LX540_MAX_SUPPORT_LISTED_MOTION			// Used for enumerated type range checking
        }

        // -------------------------------------------------------------------
        // Types of Listed Motion LmStsId (using to LmxGetStates, ...)
        // -------------------------------------------------------------------
        public enum LmStsId
        {
	        STARTED			= 0,		// 리스트 모션이 시작되었으면, TRUE 가 된다.
	        BUSY				= 1,		// 리스트 모션이 시작되었고, 해당 축이 구동 중 일 경우 TRUE 가 된다.
	        RUN_ITEM_SEQ_ID	= 2,		// 현재 구동중인 리스트 모션 시퀀스 아이디를 반환한다.
	        NC_BUFFER_STATUS	= 3,		// TRUE : [Buffer FREE], FALSE : [Buffer FULL]
	        FREE_SPACE			= 4,		
	        USED_SPACE			= 5,

	        //////////////////////////////////////////////////////////////////////////
	        INVALID				// Used for enumerated type range checking
        }

        public enum ERROR_CODES
        {
            ERR_NONE = 0,
            ERR_INVALID_BUFFER = -1,	// Invalid Buffer range
            ERR_AXIS_MOT_QUEUE_FULL = -2,	// Listed Queue Full
            ERR_CALLWDM_ERROR = -3, // WDM 드라이버와의 데이터 교환 실패  
            ERR_IMD_REPLY_FAIL = -5,	// Immediately Reply Fail
            ERR_IMD_QUEUE_FULL = -6,	// Immediately Queue Full

            ERR_NOT_ENOUGH_MEMORY = -8,	// Not Enough memory.
            ERR_INVALID_PARAMETER = -10,	// Some of the function parameters are invalid
            ERR_INVALID_AXIS = -11,	// The axis setting parameter(s) is (are) invalid
            ERR_INVALID_SPEED_SET = -12,	// Speed setting value is not valid

            ERR_INVALID_IXMAP = -13,	// Invalid Interpolation Map
            ERR_INVALID_LMMAP = -14,	// Invalid Listed-Motion Map
            ERR_INVALID_NUMAXIS = -15,	// Invalid number of axis(Mx)

            ERR_STOP_BY_SLP = -50,	// Abnormally stopped by positive soft limit
            ERR_STOP_BY_SLN = -51,	// Abnormally stopped by negative soft limit
            ERR_STOP_BY_EPL = -52,	// Abnormally stopped by (-) external limit
            ERR_STOP_BY_ELN = -53,	// Abnormally stopped by (+) external limit
            ERR_STOP_BY_ALM = -54,	// Abnormally stopped by alarm input signal
            ERR_STOP_BY_CER = -55,	// Abnormally stopped by communication error between NC and Slave nodes

            ERR_MOT_SEQ_SKIPPED = -60,	// Motion Command has been skipped because the axis is already running
            ERR_SKIP_BY_ALM = -61,	// Motion Command has been skipped by ALM signal.
            ERR_SKIP_BY_SERVO_OFF = -62,	// Motion Command has been skipped by the ServoPack state is OFF.
            ERR_FAILED_COMMAND_SERVO_ON = -63,	// Motion Command ServoOn has been failed in retry command.
            ERR_FAILED_COMMAND_SERVO_OFF = -64,	// Motion Command ServoOff has been failed in retry command.

            ERR_FAILED_TORQUE_LIMIT_MODE = -65,

            ERR_SKIP_BY_SEMG = -71,	// Motion Command has been skipped by software emergency.

            ERR_SLAVE_MODULE_INDEX = -77,	// Slave module Index is invalid.
            ERR_SLAVE_LOCAL_CHANNEL = -78,	// Local Channel is out of range.
            ERR_SLAVE_TYPE_IS_NOT_NIO = -79,	// Command Axis type is not Network-IO module.
            ERR_INVALID_LIBPARAM = -87, // Library Command Parameter is Invalid.
            ERR_DIO_INVALID_LOC_CHAN = -90,	// DIO Local channel is invalid.
            ERR_DIO_MODULE_ID_FAILED = -91,	// DO Module Id is less then 0.
            ERR_DIO_MODULE_IS_NOT_DIO_OR_DO = -92,	// Module is not output type.
            ERR_DIO_CHANNLE_IS_INCORRECT = -93,	// Channel no. is overflow.
            ERR_DIO_RTDO_BUF_IS_NULL = -94, // RTDO Buf Allocation failed.
            ERR_DIO_OUTPUT_IS_FAILED = -95, // Output DO but Channel state is not same as command state.
            ERR_DEVICE_NOT_FOUND = -1168,	// SSCNET3 Device is not detected.
            ERR_DEVICE_NOT_AVAILABLE = -4319		// SSCNET3 Device is not available.
        }
        
        //==================== Low Level API for Debugging ===================================//
        // 1. GetResources(__in LONG BoardId, __out PULONG pdwIntVect, __out PULONG pdwIoPorts, __in INT nNumPorts, __out PULONG pdwMemPorts, __in INT nNumMemPorts)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GetResources")]
        internal static extern unsafe int GetResources([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] ref int pdwIntVect, [MarshalAs(UnmanagedType.I4)] ref int pdwIoPorts, [MarshalAs(UnmanagedType.I4)] ref int pdwMemPorts, [MarshalAs(UnmanagedType.I4)]int nNumMemPorts);

        // 2. WriteMemPortDW(__in LONG BoardId, __in  ULONG dwPortBase, __in ULONG nOffset, __in LONG dwWriteVal)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "WriteMemPortDW")]
        internal static extern unsafe int WriteMemPortDW([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int dwPortBase, [MarshalAs(UnmanagedType.I4)] int nOffset, [MarshalAs(UnmanagedType.I4)] int dwWriteVal);

        // 3. ReadMemPortDW(__in LONG BoardId, __in  ULONG dwPortBase, __in ULONG nOffset, __out PLONG pdwReadVal)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "ReadMemPortDW")]
        internal static extern unsafe int ReadMemPortDW([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int dwPortBase, [MarshalAs(UnmanagedType.I4)] int nOffset, [MarshalAs(UnmanagedType.I4)] ref int pdwReadVal);
        
        // 4. DpramRead(__in LONG BoardId, __in LONG StartAddr, __in LONG Size, __out PBYTE pBuffer);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "DpramRead")]
		internal static extern unsafe int DpramRead([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int StartAddr, [MarshalAs(UnmanagedType.I4)] int Size, [MarshalAs(UnmanagedType.I4)] ref int pBuffer);
        
		// 5. DpramWrite(__in LONG BoardId, __in LONG StartAddr, __in LONG Size, __in PBYTE pBuffer);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "DpramWrite")]
		internal static extern unsafe int DpramWrite([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int StartAddr, [MarshalAs(UnmanagedType.I4)] int Size, [MarshalAs(UnmanagedType.I4)] int pBuffer);
        
		// 6. DpramBusyEnable(__in LONG BoardId, __in LONG SectId, __in LONG IsWaitEnable, __in LONG TimeoutVal);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "DpramBusyEnable")]
		internal static extern unsafe int DpramBusyEnable([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int SectId, [MarshalAs(UnmanagedType.I4)] int IsWaitEnable, [MarshalAs(UnmanagedType.I4)] int TimeoutVal);
        
		// 7. DpramBusyDisable(__in LONG BoardId, __in LONG SectId);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "DpramBusyDisable")]
		internal static extern unsafe int DpramBusyDisable([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int SectId);

        //==================== Device Load / Unload ===================================//
        // 1. GnLoadDevice(__out PLONG NumDevices, __out PLONG PLONG BoardIdList, __out_opt PLONG NumServos)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnLoadDevice")]
        internal static extern unsafe int GnLoadDevice([MarshalAs(UnmanagedType.I4)] ref int NumDevices, [MarshalAs(UnmanagedType.I4)] ref int BoardIdList, [MarshalAs(UnmanagedType.I4)] ref int NumServos);

        // 2. GnUnloadDevice()
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnUnloadDevice")]
        internal static extern unsafe int GnUnloadDevice();

        // 3. GnLoadParameter(__in LONG BoardId)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnLoadParameter")]
        internal static extern unsafe int GnLoadParameter([MarshalAs(UnmanagedType.I4)] int BoardId);

        // 4. GnSetLogMode(__in  LONG LogMode)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnSetLogMode")]
        internal static extern unsafe int GnSetLogMode([MarshalAs(UnmanagedType.I4)] int LogMode);

        // 5. GnGetLogMode(__out PLONG LogMode)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnGetLogMode")]
        internal static extern unsafe int GnGetLogMode([MarshalAs(UnmanagedType.I4)] ref int LogMode);

        // 6. GnSetLogLevel(__in  LONG LogLevel)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnSetLogLevel")]
        internal static extern unsafe int GnSetLogLevel([MarshalAs(UnmanagedType.I4)] int LogLevel);

        // 7. GnGetLogLevel(__out PLONG LogLevel)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnGetLogLevel")]
        internal static extern unsafe int GnGetLogLevel([MarshalAs(UnmanagedType.I4)] ref int LogLevel);

        // 8. GnSetFuncLevel(__in LONG FuncIndex, __in LONG LogLevel)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnSetFuncLevel")]
        internal static extern unsafe int GnSetFuncLevel([MarshalAs(UnmanagedType.I4)] int FuncIndex, [MarshalAs(UnmanagedType.I4)] int LogLevel);

        // 9. GnRestoreFuncLevel(__in LONG FuncIndex)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnRestoreFuncLevel")]
        internal static extern unsafe int GnRestoreFuncLevel([MarshalAs(UnmanagedType.I4)] int FuncIndex);

        // 10. GnGetFuncLevel(__in LONG FuncIndex, __out PLONG LogLevel)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnGetFuncLevel")]
        internal static extern unsafe int GnGetFuncLevel([MarshalAs(UnmanagedType.I4)] int FuncIndex, [MarshalAs(UnmanagedType.I4)] ref int LogLevel);

        // 11. GnGetAlarmCode(__in LONG BoardId, __in LONG Axis, __out PLONG AlmCode)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnGetAlarmCode")]
        internal static extern unsafe int GnGetAlarmCode([MarshalAs(UnmanagedType.I4)] int BoardID, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] ref int AlarmCode);
        
        // 12. GnGetEncResolution(__in LONG BoardId, __in LONG Axis, __out PLONG Resolution)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnGetEncResolution")]
        internal static extern unsafe int GnGetEncResolution([MarshalAs(UnmanagedType.I4)] int BoardID, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] ref int Resolution);

        //==================== General Functions ===================================//
        // 1. GnSetCommStates(__in LONG BoardId, __in LONG Axis, __in LONG tsId, __in  LONG CommStsVal)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnSetCommStates")]
        internal static extern unsafe int GnSetCommStates([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] int tsId, [MarshalAs(UnmanagedType.I4)] int CommStsVal);

		// 2. GnGetCommStates(__in LONG BoardId, __in LONG Axis, __in LONG tsId, __out  PLONG CommStsVal)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnGetCommStates")]
        internal static extern unsafe int GnGetCommStates([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] int tsId, [MarshalAs(UnmanagedType.I4)] ref int CommStsVal);
		
		// 3. GnResetComm(__in LONG BoardId)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnResetComm")]
        internal static extern unsafe int GnResetComm([MarshalAs(UnmanagedType.I4)] int BoardId);
		
        // 4. GnSetServoOn(__in LONG BoardId, __in	LONG Axis, __in LONG  dwIsOn)
        // [IN] VT_I4 Axis : 축번호, [IN] VT_I4 dwIsOn : 서보 상태를 설정합니다.   (1: 서보온, 0: 서보오프)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnSetServoOn")]
        internal static extern unsafe int GnSetServoOn([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] int dwIsOn);

        // 5. GnGetServoOn(__in LONG BoardId, __in	LONG Axis, __out PLONG pdwIsOn)
        // [IN] VT_I4 Axis : 축번호, [OUT] VT_PI4 dwIsOn : 서보 상태를 반환합니다. (1: 서보온, 0: 서보오프)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnGetServoOn")]
        internal static extern unsafe int GnGetServoOn([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] ref int dwIsOn);

        // 6. GnSetAlarmRes(__in LONG BoardId, __in   LONG Axis, __in  LONG  IsOn)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnSetAlarmRes")]
        internal static extern unsafe int GnSetAlarmRes([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis);

        // 7. GnGetAlarmRes(__in LONG BoardId, __in	LONG Axis, __out LONG * IsOn)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnGetAlarmRes")]
        internal static extern unsafe int GnGetAlarmRes([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] ref int IsOn);

        // 8. GnSetEmergency(__in LONG BoardId, __in	LONG Axis, __in LONG IsDecStop, __in LONG IsEnable)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnSetEmergency")]
        internal static extern unsafe int GnSetEmergency([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] int IsDecStop, [MarshalAs(UnmanagedType.I4)] int IsEnable);

        // 9. GnGetEmergency(__in LONG BoardId, __in	LONG Axis, __out PLONG IsDecStopped, __out PLONG IsEnabled)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnGetEmergency")]
        internal static extern unsafe int GnGetEmergency([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] ref int IsDecStopped, [MarshalAs(UnmanagedType.I4)] ref int IsEnabled);

        // 10. GnSetEmergencyAll(__in   LONG IsDecStop, __in	LONG IsEnable)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnSetEmergencyAll")]
        internal static extern unsafe int GnSetEmergencyAll([MarshalAs(UnmanagedType.I4)] int IsDecStop, [MarshalAs(UnmanagedType.I4)] int IsEnable);

        // 11. GnGetEmergencyAll(__out  LONG * IsDecStopped, __out	PLONG IsEnabled)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnGetEmergencyAll")]
        internal static extern unsafe int GnGetEmergencyAll([MarshalAs(UnmanagedType.I4)] ref int IsDecStopped, [MarshalAs(UnmanagedType.I4)] ref int IsEnabled);

        // (NEMO) Sampling Period를 설정 또는 반환합니다.
        // 12. GnSetCommPeriod(__in LONG BoardId, __in	LONG nPeriod)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnSetCommPeriod")]
        internal static extern unsafe int GnSetCommPeriod([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int nPeriod);

        // 13. GnGetCommPeriod(__in LONG BoardId, __out	PLONG nPeriod)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnGetCommPeriod")]
        internal static extern unsafe int GnGetCommPeriod([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] ref int nPeriod);

        // 14. GnResetDevice(__in LONG BoardId, __in	LONG ResetMask)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnResetDevice")]
        internal static extern unsafe int GnResetDevice([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int ResetMask);

        // [IN] VT_I4 dwInterval : 업데이트 주기 (500usec Unit)
        // 15. GnSetStatusUpdateInterval(__in LONG BoardId, __in	LONG dwInterval)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnSetStatusUpdateInterval")]
        internal static extern unsafe int GnSetStatusUpdateInterval([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int dwInterval);

        // 16. GnGetStatusUpdateInterval(__in LONG BoardId, __out	PLONG dwInterval)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnGetStatusUpdateInterval")]
        internal static extern unsafe int GnGetStatusUpdateInterval([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] ref int dwInterval);

        // 17. GnGetAxisMap(__in LONG BoardId, __out	LONG* AxisMapMask)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnGetAxisMap")]
        internal static extern unsafe int GnGetAxisMap([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] ref int AxisMapMask);

        //==================== Network Servo 파라미터를 설정/얻기 ================//
        //// 18. GnSetParam(__in LONG BoardId, __in LONG Axis, __in LONG PrmNo1, __in LONG PrmData1, __in LONG PrmNo2, __in LONG PrmData2)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnSetParam540")]
        internal static extern unsafe int GnSetParam([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] int PrmNo1, [MarshalAs(UnmanagedType.I4)] int PrmData1, [MarshalAs(UnmanagedType.I4)] int PrmNo2, [MarshalAs(UnmanagedType.I4)] int PrmData2);

        //// 19. GnGetParam(__in LONG BoardId, __in LONG Axis, __in LONG PrmNo1, __out PLONG pPrmData1, __in LONG PrmNo2, __out PLONG pPrmData2)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnGetParam540")]
        internal static extern unsafe int GnGetParam([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] int PrmNo1, [MarshalAs(UnmanagedType.I4)] ref int PrmData1, [MarshalAs(UnmanagedType.I4)] int PrmNo2, [MarshalAs(UnmanagedType.I4)] ref int PrmData2);

        // Network Servo 절대치 엔코더를 설정합니다.(서보 파라미터도 변경됩니다.)
        // 20. GnSetABSMode(__in LONG BoardId, __in LONG Axis, __in LONG EncoderMode)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnSetABSMode")]
        internal static extern unsafe int GnSetABSMode([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] int EncoderMode);

        // Network Servo 절대치 엔코더를 반환합니다.
        // 21. GnGetABSMode(__in LONG BoardId, __in LONG Axis, __out PLONG EncoderMode)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnGetABSMode")]
        internal static extern unsafe int GnGetABSMode([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] ref int EncoderMode);

        // Network Servo 절대치 홈을 설정합니다..
        // 22. GnSetABSHome(__in LONG BoardId, __in LONG Axis)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnSetABSHome")]
        internal static extern unsafe int GnSetABSHome([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis);
        
        // Network Servo Driver의 절대치 설정을 읽어와 갱신합니다.
        // 22. GnABSUpdate(__in LONG BoardId)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnABSUpdate")]
        internal static extern unsafe int GnABSUpdate([MarshalAs(UnmanagedType.I4)] int BoardId);

        // Network Servo 제어 모드를 변경합니다.(위치, 토크)
        // 23. GnSetControlMode(__in LONG BoardId, __in LONG Axis, __in LONG ControlMode)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnSetControlMode")]
        internal static extern unsafe int GnSetControlMode([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] int ControlMode);
        
        // Network Servo 제어 모드를 반환합니다.(위치, 토크)
        // 24. GnGetControlMode(__in LONG BoardId, __in LONG Axis, __out PLONG ControlMode)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnGetControlMode")]
        internal static extern unsafe int GnGetControlMode([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] ref int ControlMode);
        
        // Network Servo의 위치 모드 중 토크 제한 모드를 설정합니다.
        // 25. GnSetPositionTorqueMode(__in LONG BoardId, __in LONG Axis, __in LONG Enable);
		[DllImport("ComiSSCNET3.DLL", EntryPoint = "GnSetPositionTorqueMode")]
        internal static extern unsafe int GnSetPositionTorqueMode([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] int Enable);
        
        // Network Servo의 위치 모드 중 토크 제한 모드를 반환합니다.
        // 26. GnGetPositionTorqueMode(__in LONG BoardId, __in LONG Axis, __out PLONG Enable);
		[DllImport("ComiSSCNET3.DLL", EntryPoint = "GnGetPositionTorqueMode")]
        internal static extern unsafe int GnGetPositionTorqueMode([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] ref int Enable);

        // Network Servo의 부하율을 반환합니다.
        // 27. cmsGnGetServoLoad(__in LONG BoardId, __in LONG Axis, __out PLONG RegenLoad, __out PLONG EffectLoad, __out PLONG PeakLoad);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnGetServoLoad")]
        internal static extern unsafe int GnGetServoLoad([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] ref int RegenLoad, [MarshalAs(UnmanagedType.I4)] ref int EffectLoad, [MarshalAs(UnmanagedType.I4)] ref int PeakLoad);

        // Network Servo의 전자기어비를 설정합니다.
        // 27. cmsGnSetElectronicGearRatio (__in LONG BoardId, __in LONG Axis, __in LONG CMX, __in LONG CDV);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnSetElectronicGearRatio")]
        internal static extern unsafe int GnSetElectronicGearRatio([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] int CMX, [MarshalAs(UnmanagedType.I4)] int CDV);

        // Network Servo의 전자기어비를 반환합니다.
        // 28. cmsGnGetElectronicGearRatio (__in LONG BoardId, __in LONG Axis, __out LONG CMX, __out LONG CDV);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "GnGetElectronicGearRatio")]
        internal static extern unsafe int GnGetElectronicGearRatio([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] ref int CMX, [MarshalAs(UnmanagedType.I4)] ref int CDV);

        
        //==================== Cfg Functions ===================================//
        // 1. CfgSetMioProperty(__in LONG BoardId, __in	LONG Axis, __in LONG PropId, __in  LONG  PropVal)
        // [IN] BoardId, [IN] VT_I4 Axis : 축번호, [IN] VT_I4 PropId : 설정값의 id를 지정합니다, [IN] VT_I4 PropVal : 지정한 id의 설정값
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "CfgSetMioProperty")]
        internal static extern unsafe int CfgSetMioProperty([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] int PropId, [MarshalAs(UnmanagedType.I4)] int PropVal);

        // 2. CfgGetMioProperty(__in LONG BoardId, __in	LONG Axis, __in LONG PropId, __out PLONG PropVal)
        // [IN] BoardId, [IN] VT_I4 Axis : 축번호, [IN] VT_I4 PropId : 설정값의 id를 지정합니다, [OUT] VT_PI4 PropVal : 지정한 id의 설정값
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "CfgGetMioProperty")]
        internal static extern unsafe int CfgGetMioProperty([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] int PropId, [MarshalAs(UnmanagedType.I4)] ref int PropVal);

        // 3. CfgSetUnitDist(__in LONG BoardId, __in LONG Axis, __in DOUBLE UnitDist)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "CfgSetUnitDist")]
        internal static extern unsafe int CfgSetUnitDist([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.R8)] double UnitDist);

        // 4. CfgGetUnitDist(__in LONG BoardId, __in LONG Axis, __out DOUBLE * UnitDist)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "CfgGetUnitDist")]
        internal static extern unsafe int CfgGetUnitDist([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.R8)] ref double UnitDist);

        // 5. CfgSetUnitSpeed(__in LONG BoardId, __in LONG Axis, __in DOUBLE UnitSpeed)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "CfgSetUnitSpeed")]
        internal static extern unsafe int CfgSetUnitSpeed([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.R8)] double UnitSpeed);

        // 6. CfgGetUnitSpeed(__in LONG BoardId, __in LONG Axis, __out DOUBLE * UnitSpeed)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "CfgGetUnitSpeed")]
        internal static extern unsafe int CfgGetUnitSpeed([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.R8)] ref double UnitSpeed);

        // 7. CfgSetSpeedPattern(__in LONG BoardId, __in LONG Axis, __in LONG SpeedMode, __in DOUBLE Work, __in DOUBLE Acc, __in DOUBLE Dec, __in DOUBLE Ini, __in DOUBLE End)
        // [IN] BoardId, [IN] VT_I4 Axis : 축번호, [IN] VT_I4 SpeedMode([0]Constant, [1]Trapezoidal, [2]S-Curve) [IN] VT_R8 Work/Workspeed, [IN] VT_R8 Acc/Acceleration , [IN] VT_R8 Dec/Deceleration, [IN] VT_R8 Ini/IniSpeed, [IN] VT_R8 End/EndSpeed 
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "CfgSetSpeedPattern")]
        internal static extern unsafe int CfgSetSpeedPattern([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] int SpeedMode, [MarshalAs(UnmanagedType.R8)] double Work, [MarshalAs(UnmanagedType.R8)] double Acc, [MarshalAs(UnmanagedType.R8)] double Dec, [MarshalAs(UnmanagedType.R8)] double Ini, [MarshalAs(UnmanagedType.R8)] double End);

        // 8. CfgGetSpeedPattern(__in LONG BoardId, __in LONG Axis, __out PLONG SpeedMode, __out DOUBLE * Work, __out DOUBLE * Acc, __out DOUBLE * Dec, __out DOUBLE * Ini, __out DOUBLE * End)
        // [IN] BoardId, [IN] VT_I4 Axis : 축번호, [OUT] VT_PI4 SpeedMode([0]Constant, [1]Trapezoidal, [2]S-Curve) [OUT] VT_PR8 Work/Workspeed, [OUT] VT_PR8 Acc/Acceleration , [OUT] VT_PR8 Dec/Deceleration, [OUT] VT_PR8 Ini/IniSpeed, [OUT] VT_PR8 End/EndSpeed 
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "CfgGetSpeedPattern")]
        internal static extern unsafe int CfgGetSpeedPattern([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] ref int SpeedMode, [MarshalAs(UnmanagedType.R8)] ref double Work, [MarshalAs(UnmanagedType.R8)] ref double Acc, [MarshalAs(UnmanagedType.R8)] ref double Dec, [MarshalAs(UnmanagedType.R8)] ref double Ini, [MarshalAs(UnmanagedType.R8)] ref double End);

		// 9. CfgSetSpeedPattern_T(__in LONG BoardId, __in LONG Axis, __in LONG SpeedMode, __in DOUBLE Work, __in DOUBLE AccTime, __in DOUBLE DecTime, __in DOUBLE Ini, __in DOUBLE End)
        // [IN] BoardId, [IN] VT_I4 Axis : 축번호, [IN] VT_I4 SpeedMode([0]Constant, [1]Trapezoidal, [2]S-Curve) [IN] VT_R8 Work/Workspeed, [IN] VT_R8 AccTime/Acceleration Time, [IN] VT_R8 DecTime/Deceleration Time, [IN] VT_R8 Ini/IniSpeed, [IN] VT_R8 End/EndSpeed 
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "CfgSetSpeedPattern_T")]
        internal static extern unsafe int CfgSetSpeedPattern_T([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] int SpeedMode, [MarshalAs(UnmanagedType.R8)] double Work, [MarshalAs(UnmanagedType.R8)] double AccTime, [MarshalAs(UnmanagedType.R8)] double DecTime, [MarshalAs(UnmanagedType.R8)] double Ini, [MarshalAs(UnmanagedType.R8)] double End);

        // 10. CfgGetSpeedPattern_T(__in LONG BoardId, __in LONG Axis, __out PLONG SpeedMode, __out DOUBLE * Work, __out DOUBLE * AccTime, __out DOUBLE * DecTime, __out DOUBLE * Ini, __out DOUBLE * End)
        // [IN] BoardId, [IN] VT_I4 Axis : 축번호, [OUT] VT_PI4 SpeedMode([0]Constant, [1]Trapezoidal, [2]S-Curve) [OUT] VT_PR8 Work/Workspeed, [OUT] VT_PR8 AccTime/Acceleration Time, [OUT] VT_PR8 DecTime/Deceleration Time, [OUT] VT_PR8 Ini/IniSpeed, [OUT] VT_PR8 End/EndSpeed 
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "CfgGetSpeedPattern_T")]
        internal static extern unsafe int CfgGetSpeedPattern_T([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] ref int SpeedMode, [MarshalAs(UnmanagedType.R8)] ref double Work, [MarshalAs(UnmanagedType.R8)] ref double AccTime, [MarshalAs(UnmanagedType.R8)] ref double DecTime, [MarshalAs(UnmanagedType.R8)] ref double Ini, [MarshalAs(UnmanagedType.R8)] ref double End);

        // 11. CfgSetSoftLimit(__in LONG BoardId, __in LONG Axis, __in LONG IsEnable, __in DOUBLE LimitN, __in DOUBLE LimitP)
        // [IN] BoardId, [IN] VT_I4 Axis : 축번호, [OUT] VT_I4 IsEnable : 소프트웨어 리밋 기능의 활성화 여부를 설정합니다, [IN] VT_R8 LimitN : (-)방향 Limit, [IN] VT_R8 LimitP : (+)방향 Limit
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "CfgSetSoftLimit")]
        internal static extern unsafe int CfgSetSoftLimit([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] int IsEnable, [MarshalAs(UnmanagedType.R8)] double LimitN, [MarshalAs(UnmanagedType.R8)] double LimitP);

        // 12. CfgGetSoftLimit(__in LONG BoardId, __in LONG Axis, __out PLONG IsEnabled, __out DOUBLE * LimitN, __out DOUBLE * LimitP)
        // [IN] BoardId, [IN] VT_I4 Axis : 축번호, [OUT] VT_I4 IsEnabled : 소프트웨어 리밋 기능의 활성화 여부를 반환합니다, [OUT] VT_R8 LimitN : (-)방향 Limit, [OUT] VT_R8 LimitP : (+)방향 Limit
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "CfgGetSoftLimit")]
        internal static extern unsafe int CfgGetSoftLimit([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] ref int IsEnabled, [MarshalAs(UnmanagedType.R8)] ref double LimitN, [MarshalAs(UnmanagedType.R8)] ref double LimitP);

        // 13. CfgSetVelCorrRatio(__in LONG BoardId, __in LONG Axis, __in DOUBLE CorrRatio)        
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "CfgSetVelCorrRatio")]
        internal static extern unsafe int CfgSetVelCorrRatio([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.R8)] double CorrRatio);

        // 14. CfgGetVelCorrRatio(__in LONG BoardId, __in LONG Axis, __out DOUBLE *CorrRatio)        
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "CfgGetVelCorrRatio")]
        internal static extern unsafe int CfgGetVelCorrRatio([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.R8)] ref double CorrRatio);

        // 15. CfgSetRingCntr(__in LONG BoardId, __in LONG Axis, __in LONG Enable, __in LONG nMinCnt, __in LONG nMaxCnt)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "CfgSetRingCntr")]
        internal static extern unsafe int CfgSetRingCntr([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] int IsEnable, [MarshalAs(UnmanagedType.I4)] int nMinCnt, [MarshalAs(UnmanagedType.I4)] int nMaxCnt);

        // 16. CfgGetRingCntr(__in LONG BoardId, __in LONG Axis, __out PLONG Enable,  __out PLONG nMinCnt, __out PLONG nMaxCnt)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "CfgGetRingCntr")]
        internal static extern unsafe int CfgGetRingCntr([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] ref int IsEnable, [MarshalAs(UnmanagedType.I4)] ref int nMinCnt, [MarshalAs(UnmanagedType.I4)] ref int nMaxCnt);

        //==================== Sx Control ===================================//
        // 1. SxMove(__in LONG BoardId, __in	LONG Axis, __in	DOUBLE Distance, __in LONG IsBlocking)
        // [IN] BoardId, [IN] VT_I4 Axis : 축번호, [IN] VT_R8 Distance : 거리, [IN] VT_I4  IsBlocking : 윈도우 메시지 블럭킹 여부 
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "SxMove")]
        internal static extern unsafe int SxMove([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.R8)] double Distance, [MarshalAs(UnmanagedType.I4)] int IsBlocking);

        // 2. SxMoveStart(__in LONG BoardId, __in	LONG Axis, __in	DOUBLE Distance)
        // [IN] BoardId, [IN] VT_I4 Axis : 축번호, [IN] VT_R8 Distance : 거리
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "SxMoveStart")]
        internal static extern unsafe int SxMoveStart([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.R8)] double Distance);

        // 3. SxMoveTo(__in LONG BoardId, __in	LONG Axis, __in	DOUBLE Position, __in LONG IsBlocking)
        // [IN] BoardId, [IN] VT_I4 Axis : 축번호, [IN] VT_R8 Position : 위치, [IN] VT_I4  IsBlocking : 윈도우 메시지 블럭킹 여부
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "SxMoveTo")]
        internal static extern unsafe int SxMoveTo([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.R8)] double Position, [MarshalAs(UnmanagedType.I4)] int IsBlocking);

        // 4. SxMoveToStart(__in LONG BoardId, __in	LONG Axis, __in	DOUBLE Position)
        // [IN] BoardId, [IN] VT_I4 Axis : 축번호, [IN] VT_R8 Position : 위치
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "SxMoveToStart")]
        internal static extern unsafe int SxMoveToStart([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.R8)] double Position);

        // 5. SxVMoveStart(__in LONG BoardId, __in	LONG Axis, __in LONG Dir)
        // [IN] BoardId, [IN] VT_I4 Axis : 축번호, [IN] VT_I4 Dir : 방향
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "SxVMoveStart")]
        internal static extern unsafe int SxVMoveStart([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] int Dir);

        // 6. SxStop(__in LONG BoardId, __in	LONG Axis)
        // [IN] BoardId, [IN] VT_I4 Axis : 축번호
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "SxStop")]
        internal static extern unsafe int SxStop([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis);

        // 7. SxStopEmg(__in LONG BoardId, __in	LONG Axis)
        // [IN] BoardId, [IN] VT_I4 Axis : 축번호
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "SxStopEmg")]
        internal static extern unsafe int SxStopEmg([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis);

        // 8. SxIsDone(__in LONG BoardId, __in	LONG Axis, __out PLONG pdwIsDone)
        // [IN] BoardId, [IN] VT_I4 Axis : 축번호, [OUT] VT_PI4 pdwIsDone : 이송이 완료되었는지를 반환(0:이송중, 1:이송완료)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "SxIsDone")]
        internal static extern unsafe int SxIsDone([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] ref int pdwIsDone);

        // 9. SxWaitDone(__in LONG BoardId, __in	LONG Axis, __in LONG IsBlocking)
        // [IN] BoardId, [IN] VT_I4 Axis : 축번호, [IN] VT_I4 IsBlocking : 윈도우 메세지 블럭킹 여부
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "SxWaitDone")]
        internal static extern unsafe int SxWaitDone([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] int IsBlocking);

        // 10. SxSetCorrection(__in LONG BoardId, __in	LONG Axis, __in  LONG CorrMode,	__in  DOUBLE   CorrAmount,__in   DOUBLE   CorrVel, __in  LONG  CntrMask)
        // [IN] BoardId, [IN] VT_I4 Axis : 축번호, [IN] VT_I4 CorrMode : 보정 모드 설정값  (0: 보정기능 비활성화, 1: 백래쉬 보정 모드, 2: 슬립 보정 모드), [IN] VT_R8 CorrAmount : 보정 펄스의 수 (논리적 거리 단위), [IN] VT_I4 CntrMask : 보정 펄스 출력시에 각 카운터의 동작 여부  (BIT0 1: Command Counter 동작, BIT1 1: Feedback Counter 동작, BIT2 1: Deviation Counter 동작, BIT3 1: General Counter 동작)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "SxSetCorrection")]
        internal static extern unsafe int SxSetCorrection([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] int CorrMode, [MarshalAs(UnmanagedType.R8)] double CorrAmount, [MarshalAs(UnmanagedType.R8)] double CorrVel, [MarshalAs(UnmanagedType.I4)] int CntrMask);

        // 11. SxGetCorrection(__in LONG BoardId, __in	LONG Axis, __out PLONG CorrMode,__out DOUBLE * CorrAmount, __out DOUBLE * CorrVel, __out PLONG CntrMask)
        // [IN] BoardId, [IN] VT_I4 Axis : 축번호, [IN] VT_I4 CorrMode : 보정 모드 반환값  (0: 보정기능 비활성화, 1: 백래쉬 보정 모드, 2: 슬립 보정 모드), [IN] VT_R8 CorrAmount : 보정 펄스의 수 (논리적 거리 단위), [IN] VT_I4 CntrMask : 보정 펄스 출력시에 각 카운터의 동작 여부  (BIT0 1: Command Counter 동작, BIT1 1: Feedback Counter 동작, BIT2 1: Deviation Counter 동작, BIT3 1: General Counter 동작)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "SxGetCorrection")]
        internal static extern unsafe int SxGetCorrection([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] ref int CorrMode, [MarshalAs(UnmanagedType.R8)] ref double CorrAmount, [MarshalAs(UnmanagedType.R8)] ref double CorrVel, [MarshalAs(UnmanagedType.I4)] ref int CntrMask);

        //==================== Mx Control ===================================//
        // 1. MxMove(__in LONG BoardId, __in LONG NumAxes, __in PLONG AxisList, __in DOUBLE * DistList, __in LONG IsBlocking)
        // [IN] BoardId, [IN] VT_I4 NumAxes : 참여하는 축 개수, [IN] VT_PI4 AxisList : 참여하는 축 번호의 리스트, [IN] VT_PR8 DistList : 거리의 리스트, [IN] VT_I4  IsBlocking : 윈도우 메시지 블럭킹 여부 
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "MxMove")]
        internal static extern unsafe int MxMove([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int NumAxes, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4)] int[] AxisList, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] double[] DistList, [MarshalAs(UnmanagedType.I4)] int IsBlocking);

        // 2. MxMoveStart(__in LONG BoardId, __in LONG NumAxes, __in PLONG AxisList, __in DOUBLE * DistList)
        // [IN] BoardId, [IN] VT_I4 NumAxes : 참여하는 축 개수, [IN] VT_PI4 AxisList : 참여하는 축 번호의 리스트, [IN] VT_PR8 DistList : 거리의 리스트
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "MxMoveStart")]
        internal static extern unsafe int MxMoveStart([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int NumAxes, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4)] int[] AxisList, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] double[] DistList);

        // 3. MxMoveTo(__in LONG BoardId, __in LONG NumAxes, __in PLONG AxisList, __in DOUBLE * PosList, __in LONG IsBlocking)
        // [IN] BoardId, [IN] VT_I4 NumAxes : 참여하는 축 개수, [IN] VT_PI4 AxisList : 참여하는 축 번호의 리스트, [IN] VT_PR8 PosList : 위치의 리스트, [IN] VT_I4  IsBlocking : 윈도우 메시지 블럭킹 여부 
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "MxMoveTo")]
        internal static extern unsafe int MxMoveTo([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int NumAxes, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4)] int[] AxisList, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] double[] PostList, [MarshalAs(UnmanagedType.I4)] int IsBlocking);

        // 4. MxMoveToStart(__in LONG BoardId, __in LONG NumAxes, __in PLONG AxisList, __in DOUBLE * PosList)
        // [IN] BoardId, [IN] VT_I4 NumAxes : 참여하는 축 개수, [IN] VT_PI4 AxisList : 참여하는 축 번호의 리스트, [IN] VT_PR8 PosList : 위치의 리스트
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "MxMoveToStart")]
        internal static extern unsafe int MxMoveToStart([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int NumAxes, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4)] int[] AxisList, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] double[] PosList);

        // 5. MxVMoveStart(__in LONG BoardId, __in LONG NumAxes, __in PLONG AxisList, __in PLONG DirList)
        // [IN] BoardId, [IN] VT_I4 NumAxes : 참여하는 축 개수, [IN] VT_PI4 AxisList : 참여하는 축 번호의 리스트, [IN] VT_PI4 DirList : 방향의 리스트
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "MxVMoveStart")]
        internal static extern unsafe int MxVMoveStart([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int NumAxes, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4)] int[] AxisList, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4)] int[] DirList);

        // 6. MxStop(__in LONG BoardId, __in LONG NumAxes, __in PLONG AxisList)
        // [IN] BoardId, [IN] VT_I4 NumAxes : 참여하는 축 개수, [IN] VT_PI4 AxisList : 정지할 축 번호의 리스트
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "MxStop")]
        internal static extern unsafe int MxStop([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int NumAxes, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4)] int[] AxisList);

        // 7. MxStopEmg(__in LONG BoardId, __in LONG NumAxes, __in PLONG AxisList)
        // [IN] BoardId, [IN] VT_I4 NumAxes : 참여하는 축 개수, [IN] VT_PI4 AxisList : 정지할 축 번호의 리스트
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "MxStopEmg")]
        internal static extern unsafe int MxStopEmg([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int NumAxes, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4)] int[] AxisList);

        // 8. MxIsDone(__in LONG BoardId, __in LONG NumAxes, __in PLONG AxisList, __out PLONG IsDone)
        // [IN] BoardId, [IN] VT_I4 NumAxes : 참여하는 축 개수, [IN] VT_PI4 AxisList : 참여하는 축 번호의 리스트, [IN] VT_I4 IsDone: 완료 여부
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "MxIsDone")]
        internal static extern unsafe int MxIsDone([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int NumAxes, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4)] int[] AxisList, [MarshalAs(UnmanagedType.I4)] ref int IsDone);

        // 9. MxWaitDone(__in LONG BoardId, __in LONG NumAxes, __in PLONG AxisList, __in LONG IsBlocking)
        // [IN] BoardId, [IN] VT_I4 NumAxes : 참여하는 축 개수, [IN] VT_PI4 AxisList : 참여하는 축 번호의 리스트, [IN] VT_I4  IsBlocking : 윈도우 메시지 블럭킹 여부 
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "MxWaitDone")]
        internal static extern unsafe int MxWaitDone([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int NumAxes, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4)] int[] AxisList, [MarshalAs(UnmanagedType.I4)] int IsBlocking);

        //==================== Ix Control ===================================//
        // 1. IxMAPAxes)			(__in LONG BoardId, __in LONG MapIndex, __in LONG MapMask, __in LONG IxMode)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호, [IN] VT_I4 MapMask : 맵마스크, [IN] VT_I4 IxMode : 보간이송 모드 (0:직선보간, 1:원호보간, 2:헬리컬보간, 3:스플라인보간)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxMapAxes")]
        internal static extern unsafe int IxMapAxes([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.I4)] int MapMask, [MarshalAs(UnmanagedType.I4)] int IxMode);

        // 2. IxUnMapAxes(__in LONG BoardId, __in LONG MapIndex)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxUnMapAxes")]
        internal static extern unsafe int IxUnMapAxes([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex);

        // 3. IxSetSpeedPattern(__in LONG BoardId, __in LONG MapIndex, __in  LONG  IsVectorSpeed, __in  LONG  SpeedMode, __in  DOUBLE   Ini, __in  DOUBLE   End, __in  DOUBLE   Vel, __in  DOUBLE Acc, __in  DOUBLE Dec)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호, [IN] VT_I4 IsVectorSpeed : 스피드 모드(0:마스터스피드 모드, 1:벡터스피드 모드), [IN] VT_I4 SpeedMode : 속도모드(0:Constant, 1:Trapezoidal, 2:S-Curve), [IN] VT_R8 Ini : 초기속도, [IN] VT_R8 End : 최종속도, [IN] VT_R8 Vel : 작업속도, [IN] VT_R8 Acc : 가속도, [IN] VT_R8 Dec : 감속도
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxSetSpeedPattern")]
        internal static extern unsafe int IxSetSpeedPattern([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.I4)] int IsVectorSpeed, [MarshalAs(UnmanagedType.I4)] int SpeedMode, [MarshalAs(UnmanagedType.R8)] double Ini, [MarshalAs(UnmanagedType.R8)] double End, [MarshalAs(UnmanagedType.R8)] double Vel, [MarshalAs(UnmanagedType.R8)] double Acc, [MarshalAs(UnmanagedType.R8)] double Dec);

        // 4. IxGetSpeedPattern(__in LONG BoardId, __in LONG MapIndex, __out PLONG IsVectorSpeed, __out PLONG SpeedMode, __out DOUBLE * Ini, __out DOUBLE * End, __out DOUBLE * Vel, __out DOUBLE * Acc, __out DOUBLE * Dec)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호, [OUT] VT_PI4 IsVectorSpeed : 스피드 모드(0:마스터스피드 모드, 1:벡터스피드 모드), [OUT] VT_PI4 SpeedMode : 속도모드(0:Constant, 1:Trapezoidal, 2:S-Curve), [OUT] VT_PR8 Ini : 초기속도, [OUT] VT_PR8 End : 최종속도, [OUT] VT_PR8 Vel : 작업속도, [OUT] VT_PR8 Acc : 가속도, [OUT] VT_PR8 Dec : 감속도
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxGetSpeedPattern")]
        internal static extern unsafe int IxGetSpeedPattern([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.I4)] ref int IsVectorSpeed, [MarshalAs(UnmanagedType.I4)] ref int SpeedMode, [MarshalAs(UnmanagedType.R8)] ref double Ini, [MarshalAs(UnmanagedType.R8)] ref double End, [MarshalAs(UnmanagedType.R8)] ref double Vel, [MarshalAs(UnmanagedType.R8)] ref double Acc, [MarshalAs(UnmanagedType.R8)] ref double Dec);

		// 5. IxSetSpeedPattern_T(__in LONG BoardId, __in LONG MapIndex, __in  LONG  IsVectorSpeed, __in  LONG  SpeedMode, __in  DOUBLE   Ini, __in  DOUBLE   End, __in  DOUBLE   Vel, __in  DOUBLE AccTime, __in  DOUBLE DecTime)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호, [IN] VT_I4 IsVectorSpeed : 스피드 모드(0:마스터스피드 모드, 1:벡터스피드 모드), [IN] VT_I4 SpeedMode : 속도모드(0:Constant, 1:Trapezoidal, 2:S-Curve), [IN] VT_R8 Ini : 초기속도, [IN] VT_R8 End : 최종속도, [IN] VT_R8 Vel : 작업속도, [IN] VT_R8 AccTime : 가속 시간, [IN] VT_R8 DecTime : 감속 시간
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxSetSpeedPattern_T")]
        internal static extern unsafe int IxSetSpeedPattern_T([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.I4)] int IsVectorSpeed, [MarshalAs(UnmanagedType.I4)] int SpeedMode, [MarshalAs(UnmanagedType.R8)] double Ini, [MarshalAs(UnmanagedType.R8)] double End, [MarshalAs(UnmanagedType.R8)] double Vel, [MarshalAs(UnmanagedType.R8)] double AccTime, [MarshalAs(UnmanagedType.R8)] double DecTime);

        // 6. IxGetSpeedPattern_T(__in LONG BoardId, __in LONG MapIndex, __out PLONG IsVectorSpeed, __out PLONG SpeedMode, __out DOUBLE * Ini, __out DOUBLE * End, __out DOUBLE * Vel, __out DOUBLE * Acc, __out DOUBLE * Dec)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호, [OUT] VT_PI4 IsVectorSpeed : 스피드 모드(0:마스터스피드 모드, 1:벡터스피드 모드), [OUT] VT_PI4 SpeedMode : 속도모드(0:Constant, 1:Trapezoidal, 2:S-Curve), [OUT] VT_PR8 Ini : 초기속도, [OUT] VT_PR8 End : 최종속도, [OUT] VT_PR8 Vel : 작업속도, [OUT] VT_PR8 AccTime : 가속 시간, [OUT] VT_PR8 DecTime : 감속 시간
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxGetSpeedPattern_T")]
        internal static extern unsafe int IxGetSpeedPattern_T([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.I4)] ref int IsVectorSpeed, [MarshalAs(UnmanagedType.I4)] ref int SpeedMode, [MarshalAs(UnmanagedType.R8)] ref double Ini, [MarshalAs(UnmanagedType.R8)] ref double End, [MarshalAs(UnmanagedType.R8)] ref double Vel, [MarshalAs(UnmanagedType.R8)] ref double AccTime, [MarshalAs(UnmanagedType.R8)] ref double DecTime);

        // 7. IxLine(__in LONG BoardId, __in LONG MapIndex, __in DOUBLE * DistList, __in LONG IsBlocking)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호, [IN] VT_PR8 DistList : 거리 리스트, [IN] VT_I4 IsBlocking : 윈도우 메시지 블록(0:블록하지 않음, 1:블록)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxLine")]
        internal static extern unsafe int IxLine([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] double[] DistList, [MarshalAs(UnmanagedType.I4)] int IsBlocking);

        // 8. IxLineStart(__in LONG BoardId, __in LONG MapIndex, __in DOUBLE * DistList)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호, [IN] VT_PR8 DistList : 거리 리스트
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxLineStart")]
        internal static extern unsafe int IxLineStart([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] double[] DistList);

        // 9. IxLineTo(__in LONG BoardId, __in LONG MapIndex, __in DOUBLE * PosList, __in LONG IsBlocking)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호, [IN] VT_PR8 PosList : 위치 리스트, [IN] VT_I4 IsBlocking : 윈도우 메시지 블록(0:블록하지 않음, 1:블록)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxLineTo")]
        internal static extern unsafe int IxLineTo([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] double[] PosList, [MarshalAs(UnmanagedType.I4)] int IsBlocking);

        // 10. IxLineToStart(__in LONG BoardId, __in LONG MapIndex, __in DOUBLE * PosList)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호, [IN] VT_PR8 PosList : 위치 리스트
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxLineToStart")]
        internal static extern unsafe int IxLineToStart([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] double[] PosList);

        // 11. IxArcA(__in LONG BoardId, __in LONG MapIndex, __in DOUBLE XCentOffset, __in DOUBLE YCentOffset, __in DOUBLE EndAngle, __in LONG IsBlocking)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호, [IN] VT_R8 XCentOffset : 현재 위치(시작 위치)로부터 원의 중심까지의 X축 상대좌표값, [IN] VT_R8 YCentOffset : 현재 위치(시작 위치)로부터 원의 중심까지의 X축 상대좌표값, [IN] VT_R8 EndAngle : 이송각도(Degree), [IN] VT_I4 IsBlocking : 윈도우 메시지 블록(0:블록하지 않음, 1:블록)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxArcA")]
        internal static extern unsafe int IxArcA([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.R8)] double XCentOffset, [MarshalAs(UnmanagedType.R8)] double YCentOffset, [MarshalAs(UnmanagedType.R8)] double EndAngle, [MarshalAs(UnmanagedType.I4)] int IsBlocking);

        // 12. IxArcAStart(__in LONG BoardId, __in LONG MapIndex, __in DOUBLE XCentOffset, __in DOUBLE YCentOffset, __in DOUBLE EndAngle)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호, [IN] VT_R8 XCentOffset : 현재 위치(시작 위치)로부터 원의 중심까지의 X축 상대좌표값, [IN] VT_R8 YCentOffset : 현재 위치(시작 위치)로부터 원의 중심까지의 X축 상대좌표값, [IN] VT_R8 EndAngle : 이송각도(Degree)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxArcAStart")]
        internal static extern unsafe int IxArcAStart([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.R8)] double XCentOffset, [MarshalAs(UnmanagedType.R8)] double YCentOffset, [MarshalAs(UnmanagedType.R8)] double EndAngle);

        // 13. IxArcATo(__in LONG BoardId, __in LONG MapIndex, __in DOUBLE XCent, __in DOUBLE YCent, __in DOUBLE EndAngle, __in LONG IsBlocking)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호, [IN] VT_R8 XCent : 중심점의 X축 절대좌표, [IN] VT_R8 YCent : 중심점의 Y축 절대좌표, [IN] VT_R8 EndAngle : 이송각도(Degree), [IN] VT_I4 IsBlocking : 윈도우 메시지 블록(0:블록하지 않음, 1:블록)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxArcATo")]
        internal static extern unsafe int IxArcATo([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.R8)] double XCent, [MarshalAs(UnmanagedType.R8)] double YCent, [MarshalAs(UnmanagedType.R8)] double EndAngle, [MarshalAs(UnmanagedType.I4)] int IsBlocking);

        // 14. IxArcAToStart(__in LONG BoardId, __in LONG MapIndex, __in DOUBLE XCent, __in DOUBLE YCent, __in DOUBLE EndAngle)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호, [IN] VT_R8 XCent : 중심점의 X축 절대좌표, [IN] VT_R8 YCent : 중심점의 Y축 절대좌표, [IN] VT_R8 EndAngle : 이송각도(Degree)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxArcAToStart")]
        internal static extern unsafe int IxArcAToStart([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.R8)] double XCent, [MarshalAs(UnmanagedType.R8)] double YCent, [MarshalAs(UnmanagedType.R8)] double EndAngle);

        // 15. IxArcP(__in LONG BoardId, __in LONG MapIndex, __in DOUBLE XCentOffset, __in DOUBLE YCentOffset, __in DOUBLE XEndPointDist, __in DOUBLE YEndPointDist, __in LONG Direction, __in LONG IsBlocking)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호, [IN] VT_R8 XCentOffset : 현재 위치(시작 위치)로부터 원의 중심까지의 X축 상대좌표값, [IN] VT_R8 YCentOffset : 현재 위치(시작 위치)로부터 원의 중심까지의 X축 상대좌표값, [IN] VT_R8 XEndPointDist : 목표지점의 현재위치로부터 X축상 거리값, [IN] VT_R8 YEndPointDist : 목표지점의 현재위치로부터 Y축상 거리값, [IN] VT_I4 IsBlocking : 윈도우 메시지 블록(0:블록하지 않음, 1:블록)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxArcP")]
        internal static extern unsafe int IxArcP([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.R8)] double XCentOffset, [MarshalAs(UnmanagedType.R8)] double YCentOffset, [MarshalAs(UnmanagedType.R8)] double XEndPointDist, [MarshalAs(UnmanagedType.R8)] double YEndPointDist, [MarshalAs(UnmanagedType.I4)] int Direction, [MarshalAs(UnmanagedType.I4)] int IsBlocking);

        // 16. IxArcPStart(__in LONG BoardId, __in LONG MapIndex, __in DOUBLE XCentOffset, __in DOUBLE YCentOffset, __in DOUBLE XEndPointDist, __in DOUBLE YEndPointDist, __in LONG Direction)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호, [IN] VT_R8 XCentOffset : 현재 위치(시작 위치)로부터 원의 중심까지의 X축 상대좌표값, [IN] VT_R8 YCentOffset : 현재 위치(시작 위치)로부터 원의 중심까지의 X축 상대좌표값, [IN] VT_R8 XEndPointDist : 목표지점의 현재위치로부터 X축상 거리값, [IN] VT_R8 YEndPointDist : 목표지점의 현재위치로부터 Y축상 거리값
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxArcPStart")]
        internal static extern unsafe int IxArcPStart([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.R8)] double XCentOffset, [MarshalAs(UnmanagedType.R8)] double YCentOffset, [MarshalAs(UnmanagedType.R8)] double XEndPointDist, [MarshalAs(UnmanagedType.R8)] double YEndPointDist, [MarshalAs(UnmanagedType.I4)] int Direction);

        // 17. IxArcPTo(__in LONG BoardId, __in LONG MapIndex, __in DOUBLE XCent, __in DOUBLE YCent, __in DOUBLE XEndPos, __in DOUBLE YEndPos, __in LONG Direction, __in LONG IsBlocking)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호, [IN] VT_R8 XCent : 중심점의 X축 절대좌표, [IN] VT_R8 YCent : 중심점의 Y축 절대좌표, [IN] VT_R8 XEndPos : 목표지점의 X축 절대좌표, [IN] VT_R8 YEndPos : 목표지점의 Y축 절대좌표, [IN] VT_I4 Direction : 회전방향(0:시계방향(CW), 1:반시계방향(CCW)), [IN] VT_I4 IsBlocking : 윈도우 메시지 블록(0:블록하지 않음, 1:블록)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxArcPTo")]
        internal static extern unsafe int IxArcPTo([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.R8)] double XCent, [MarshalAs(UnmanagedType.R8)] double YCent, [MarshalAs(UnmanagedType.R8)] double XEndPos, [MarshalAs(UnmanagedType.R8)] double YEndPos, [MarshalAs(UnmanagedType.I4)] int Direction, [MarshalAs(UnmanagedType.I4)] int IsBlocking);

        // 18. IxArcPToStart(__in LONG BoardId, __in LONG MapIndex, __in DOUBLE XCent, __in DOUBLE YCent, __in DOUBLE XEndPos, __in DOUBLE YEndPos, __in LONG Direction);
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호, [IN] VT_R8 XCent : 중심점의 X축 절대좌표, [IN] VT_R8 YCent : 중심점의 Y축 절대좌표, [IN] VT_R8 XEndPos : 목표지점의 X축 절대좌표, [IN] VT_R8 YEndPos : 목표지점의 Y축 절대좌표, [IN] VT_I4 Direction : 회전방향(0:시계방향(CW), 1:반시계방향(CCW))
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxArcPToStart")]
        internal static extern unsafe int IxArcPToStart([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.R8)] double XCent, [MarshalAs(UnmanagedType.R8)] double YCent, [MarshalAs(UnmanagedType.R8)] double XEndPos, [MarshalAs(UnmanagedType.R8)] double YEndPos, [MarshalAs(UnmanagedType.I4)] int Direction);

        // 19. IxArc3P(__in LONG BoardId, __in LONG MapIndex, __in DOUBLE P2[], __in DOUBLE P3[], __in DOUBLE EndAngle, __in LONG IsBlocking);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxArc3P")]
        internal static extern unsafe int IxArc3P([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] double[] P2, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] double[] P3, [MarshalAs(UnmanagedType.R8)] double EndAngle, [MarshalAs(UnmanagedType.I4)] int IsBlocking);

        // 20. IxArc3PStart(__in LONG BoardId, __in LONG MapIndex, __in DOUBLE P2[], __in DOUBLE P3[], __in DOUBLE EndAngle)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxArc3PStart")]
        internal static extern unsafe int IxArc3PStart([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] double[] P2, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] double[] P3, [MarshalAs(UnmanagedType.R8)] double EndAngle);

        // 21. IxArc3PTo(__in LONG BoardId, __in LONG MapIndex, __in DOUBLE P2[], __in DOUBLE P3[], __in DOUBLE EndAngle, __in LONG IsBlocking);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxArc3PTo")]
        internal static extern unsafe int IxArc3PTo([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] double[] P2, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] double[] P3, [MarshalAs(UnmanagedType.R8)] double EndAngle, [MarshalAs(UnmanagedType.I4)] int IsBlocking);

        // 22. IxArc3PToStart(__in LONG BoardId, __in LONG MapIndex, __in DOUBLE P2[], __in DOUBLE P3[], __in DOUBLE EndAngle)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxArc3PToStart")]
        internal static extern unsafe int IxArc3PToStart([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] double[] P2, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] double[] P3, [MarshalAs(UnmanagedType.R8)] double EndAngle);

        // 23. IxIsDone(__in LONG BoardId, __in LONG MapIndex, __out LONG *IsDone)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호, [OUT] VT_PI4 IsDone : 이송이 완료되었는지를 반환(0:이송중, 1:이송완료)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxIsDone")]
        internal static extern unsafe int IxIsDone([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.I4)] ref int IsDone);

        // 24. IxWaitDone(__in LONG BoardId, __in LONG MapIndex, __in  LONG IsBlocking)
        // [IN] BoardId, [IN] VT_I4 MapINdex : 맵번호, [IN] VT_I4 IsBlocking : 윈도우 메시지 블록(0:블록하지 않음, 1:블록)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxWaitDone")]
        internal static extern unsafe int IxWaitDone([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.I4)] int IsBlocking);

        // 25. IxStop(__in LONG BoardId, __in LONG MapIndex, __in  LONG IsWaitComplete, __in LONG IsBlocking)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호, [IN] VT_I4 IsWaitComplete : 완료될 때까지 기다리는 지의 여부, [IN] VT_I4 IsBlocking : 윈도우 메시지 블록(0:블록하지 않음, 1:블록) 
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxStop")]
        internal static extern unsafe int IxStop([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.I4)] int IsWaitComplete, [MarshalAs(UnmanagedType.I4)] int IsBlocking);

        // 26. IxStopEmg(__in LONG BoardId, __in LONG MapIndex)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxStopEmg")]
        internal static extern unsafe int IxStopEmg([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex);

        // 27. IxHelOnceStart(__in LONG BoardId, __in LONG MapIndex, __in LONG * HelCoord, __in LONG ArcAngle)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호, [IN] VT_PI4 Helcoord : 3축의 좌표 배열, [IN] VT_I4 ArcAngle : 이송각도(degree)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxHelOnceStart")]
        internal static extern unsafe int IxHelOnceStart([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4)] int[] HelCoord, [MarshalAs(UnmanagedType.I4)] int ArcAngle);
		
		// 28. IxSplineStart(__in LONG BoardId, __in LONG MapIndex, __in DOUBLE InArray[20][2], __in LONG  NumInArray, __in LONG NumOutArray)
        // [IN] BoardId, [IN] VT_I4 MapIndex : 맵번호, [IN] VT_PI4 Helcoord : 3축의 좌표 배열, [IN] VT_I4 ArcAngle : 이송각도(degree)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxSplineStart")]
        internal static extern unsafe int IxSplineStart([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] double[][] InArray, [MarshalAs(UnmanagedType.I4)] int NumInArray, [MarshalAs(UnmanagedType.I4)] int NumOutArray);

        // 29. IxGetMapIndex(__in LONG BoardId, __in LONG Axis, __out PLONG MapIndex)
        // [IN] BoardId, [IN] VT_I4 Axis : 축번호, [OUT] VT_PI4 MapIndex : 해당 축이 사용하고 있는 MapIndex 번호
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "IxGetMapIndex")]
        internal static extern unsafe int IxGetMapIndex([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] ref int MapIndex);

        //==================== Return to Home ===================================//
        // 1. HomeSetOffset(__in LONG BoardId, __in LONG Channel, __in DOUBLE Offset)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "HomeSetOffset")]
        internal static extern unsafe int HomeSetOffset([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.R8)] double Offset);

        // 2. HomeGetOffset(__in LONG BoardId, __in LONG Channel, __out DOUBLE *Offset)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "HomeGetOffset")]
        internal static extern unsafe int HomeGetOffset([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.R8)] ref double Offset);

        // 3. HomeSetConfig(__in LONG BoardId, __in LONG Channel, __in LONG ParamId, __in LONG ParamVal);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "HomeSetConfig")]
        internal static extern unsafe int HomeSetConfig([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] int ParamId, [MarshalAs(UnmanagedType.I4)] int ParamVal);
        
        // 4. HomeGetConfig(__in LONG BoardId, __in LONG Channel, __in LONG ParamId, __out LONG * ParamVal);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "HomeGetConfig")]
        internal static extern unsafe int HomeGetConfig([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] int ParamId, [MarshalAs(UnmanagedType.I4)] ref int ParamVal);
        
        // 5. HomeSetPosClrMode(__in LONG BoardId, __in LONG Channel, __in LONG PosClrMode);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "HomeSetPosClrMode")]
        internal static extern unsafe int HomeSetPosClrMode([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] int PosClrMode);
        
        // 6. HomeGetPosClrMode(__in LONG BoardId, __in LONG Channel, __out PLONG PosClrMode);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "HomeGetPosClrMode")]
        internal static extern unsafe int HomeGetPosClrMode([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] ref int PosClrMode);

        // [IN] VT_I4 Channel : 축번호, [IN] VT_I4 PhaseID, [IN] VT_I4 SpeedMode : 속도모드(0:Constant, 1:Trapezoidal, 2:S-Curve), [IN] VT_R8 Vel : 작업속도, [IN] VT_R8 Accel : 가속도, [IN] VT_R8 Decel : 감속도
        // 7. HomeSetSpeedPattern(__in LONG BoardId, __in LONG Channel, __in LONG PhaseID, __in LONG SpeedMode, __in DOUBLE Vel, __in DOUBLE Accel, __in DOUBLE Decel);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "HomeSetSpeedPattern")]
        internal static extern unsafe int HomeSetSpeedPattern([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] int PhaseID, [MarshalAs(UnmanagedType.I4)] int SpeedMode, [MarshalAs(UnmanagedType.R8)] double Vel, [MarshalAs(UnmanagedType.R8)] double Accel, [MarshalAs(UnmanagedType.R8)] double Decel);
        
        // [IN] VT_I4 Channel : 축번호, [IN] VT_I4 PhaseID, [IN] VT_I4 SpeedMode : 속도모드(0:Constant, 1:Trapezoidal, 2:S-Curve), [IN] VT_R8 Vel : 작업속도, [IN] VT_R8 Accel : 가속도, [IN] VT_R8 Decel : 감속도
        // 8. HomeGetSpeedPattern(__in LONG BoardId, __in LONG Channel, __in LONG PhaseID, __out PLONG SpeedMode, __out DOUBLE * Vel, __out DOUBLE * Accel, __out DOUBLE * Decel);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "HomeGetSpeedPattern")]
        internal static extern unsafe int HomeGetSpeedPattern([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] int PhaseID, [MarshalAs(UnmanagedType.I4)] ref int SpeedMode, [MarshalAs(UnmanagedType.R8)] ref double Vel, [MarshalAs(UnmanagedType.R8)] ref double Accel, [MarshalAs(UnmanagedType.R8)] ref double Decel);

		// [IN] VT_I4 Channel : 축번호, [IN] VT_I4 PhaseID, [IN] VT_I4 SpeedMode : 속도모드(0:Constant, 1:Trapezoidal, 2:S-Curve), [IN] VT_R8 Vel : 작업속도, [IN] VT_R8 AccelTime : 가속 시간, [IN] VT_R8 DecelTime : 감속 시간
        // 9. HomeSetSpeedPattern_T(__in LONG BoardId, __in LONG Channel, __in LONG PhaseID, __in LONG SpeedMode, __in DOUBLE Vel, __in DOUBLE AccelTime, __in DOUBLE DecelTime);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "HomeSetSpeedPattern_T")]
        internal static extern unsafe int HomeSetSpeedPattern_T([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] int PhaseID, [MarshalAs(UnmanagedType.I4)] int SpeedMode, [MarshalAs(UnmanagedType.R8)] double Vel, [MarshalAs(UnmanagedType.R8)] double AccelTime, [MarshalAs(UnmanagedType.R8)] double DecelTime);
        
        // [IN] VT_I4 Channel : 축번호, [IN] VT_I4 PhaseID, [IN] VT_I4 SpeedMode : 속도모드(0:Constant, 1:Trapezoidal, 2:S-Curve), [IN] VT_R8 Vel : 작업속도, [IN] VT_R8 AccelTime : 가속 시간, [IN] VT_R8 DecelTime : 감속 시간
        // 10. HomeGetSpeedPattern_T(__in LONG BoardId, __in LONG Channel, __in LONG PhaseID, __out PLONG SpeedMode, __out DOUBLE * Vel, __out DOUBLE * AccelTime, __out DOUBLE * DecelTime);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "HomeGetSpeedPattern_T")]
        internal static extern unsafe int HomeGetSpeedPattern_T([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] int PhaseID, [MarshalAs(UnmanagedType.I4)] ref int SpeedMode, [MarshalAs(UnmanagedType.R8)] ref double Vel, [MarshalAs(UnmanagedType.R8)] ref double AccelTime, [MarshalAs(UnmanagedType.R8)] ref double DecelTime);
   
        // [IN] VT_I4 Channel : 축번호, [IN] VT_I4 IsBlocking : 윈도우 메시지 블록(0:블록하지 않음, 1:블록) 
        // 11. HomeMove(__in LONG BoardId, __in LONG Channel, __in LONG IsBlocking);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "HomeMove")]
        internal static extern unsafe int HomeMove([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] int IsBlocking);
        
        // [IN] VT_I4 Channel : 축번호,
        // 12. HomeMoveStart(__in LONG BoardId, __in LONG Channel);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "HomeMoveStart")]
        internal static extern unsafe int HomeMoveStart([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel);
        
        // [IN] VT_I4 NumAxes : 참여하는 축 개수, [IN] VT_PI4 ChannelList : 참여하는 축 번호의 리스트, [IN] VT_I4 IsBlocking : 윈도우 메시지 블록(0:블록하지 않음, 1:블록) 
        // 13. HomeMoveAll(__in LONG BoardId, __in LONG NumAxes, __in PLONG ChannelList, __in LONG IsBlocking);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "HomeMoveAll")]
        internal static extern unsafe int HomeMoveAll([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int NumAxes, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4)] int[] ChannelList, [MarshalAs(UnmanagedType.I4)] int IsBlocking);
        
        // [IN] VT_I4 NumAxes : 참여하는 축 개수, [IN] VT_PI4 ChannelList : 참여하는 축 번호의 리스트
        // 14. HomeMoveAllStart(__in LONG BoardId, __in LONG NumAxes, __in PLONG ChannelList);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "HomeMoveAllStart")]
        internal static extern unsafe int HomeMoveAllStart([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int NumAxes, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4)] int[] ChannelList);
        
        // [IN] VT_I4 Channel : 축번호, [IN] VT_PI4 : IsBusy : 현재 원점복귀가 진행중인지 반환(0:진행중이지 않음, 1:진행중)
        // 15. HomeIsBusy(__in LONG BoardId, __in LONG Channel, __out PLONG IsBusy);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "HomeIsBusy")]
        internal static extern unsafe int HomeIsBusy([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] ref int IsBusy);

        // [IN] VT_I4 Channel : 축번호, [IN] VT_I4 IsBlocking : 윈도우 메시지 블록(0:블록하지 않음, 1:블록)   
        // 16. HomeWaitDone(__in LONG BoardId, __in LONG Channel, __in LONG IsBlocking);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "HomeWaitDone")]
        internal static extern unsafe int HomeWaitDone([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] int IsBlocking);

        // 17. HomeSetSuccess(__in LONG BoardId, __in LONG Channel, __in LONG IsSuccess);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "HomeSetSuccess")]
        internal static extern unsafe int HomeSetSuccess([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] int IsSuccess);

        // 18. HomeGetSuccess(__in LONG BoardId, __in LONG Channel, __out PLONG IsSuccess);
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "HomeGetSuccess")]
        internal static extern unsafe int HomeGetSuccess([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] ref int IsSuccess);

        //==================== Override Motion ===================================//
        // 1. OverrideSpeedSet(__in LONG BoardId, __in LONG Channel);
        // [IN] VT_I4 BoardId, [IN] VT_I4 Channel : 축번호
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "OverrideSpeedSet")]
        internal static extern unsafe int OverrideSpeedSet([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel);

        // 2. OverrideMove(__in LONG BoardId, __in LONG Channel, __in DOUBLE NewDistance, __out PLONG IsIgnored)
        // [IN] VT_I4 BoardId, [IN] VT_I4 Channel : 축번호, [IN] VT_R8 NewDistance : 새로운 목표 거리값, [OUT] VT_PI4 IsIgnored : OverrideMove 의 적용/실패 여부 반환  (0: 적용되지 않음, 1: 적용됨)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "OverrideMove")]
        internal static extern unsafe int OverrideMove([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.R8)] double NewDistance, [MarshalAs(UnmanagedType.I4)] ref int IsIgnored);

        // 3. OverrideMoveTo(__in LONG BoardId, __in LONG Channel, __in DOUBLE NewPosition, __out PLONG IsIgnored)
        // [IN] VT_I4 BoardId, [IN] VT_I4 Channel : 축번호, [IN] VT_R8 NewPosition : 새로운 목표 거리값, [OUT] VT_PI4 IsIgnored : OverrideMove 의 적용/실패 여부 반환  (0: 적용되지 않음, 1: 적용됨)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "OverrideMoveTo")]
        internal static extern unsafe int OverrideMoveTo([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.R8)] double NewPosition, [MarshalAs(UnmanagedType.I4)] ref int IsIgnored);

        //==================== Listed Motion ===================================//
        // 1. LmxStart(__in LONG BoardId, __in LONG	LmIdx, __in LONG LmStartMode, __in LONG AxisMask)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "LmxStart")]
        internal static extern unsafe int LmxStart([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int LmIdx, [MarshalAs(UnmanagedType.I4)] int LmStartMode, [MarshalAs(UnmanagedType.I4)] int AxisMask);

        // 2. LmxSuspend(__in LONG BoardId, __in LONG	LmIdx, __in LONG SuspendMode)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "LmxSuspend")]
        internal static extern unsafe int LmxSuspend([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int LmIdx, [MarshalAs(UnmanagedType.I4)] int SuspendMode);

        // 3. LmxResume(__in LONG BoardId, __in LONG	LmIdx, __in	LONG ResumeMode)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "LmxResume")]
        internal static extern unsafe int LmxResume([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int LmIdx, [MarshalAs(UnmanagedType.I4)] int ResumeMode);

        // 4. LmxEnd(__in LONG BoardId, __in LONG	LmIdx)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "LmxEnd")]
        internal static extern unsafe int LmxEnd([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int LmIdx);

        // 5. LmxGetStates(__in LONG BoardId, __in LONG	LmIdx, __in LONG LmStsId, __out PLONG LmxStsVal)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "LmxGetStates")]
        internal static extern unsafe int LmxGetStates([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int LmIdx, [MarshalAs(UnmanagedType.I4)] int LmStsId, [MarshalAs(UnmanagedType.I4)] ref int LmxStsVal);

        // 6. LmxSetSeqMode(__in LONG  LmIdx, __in  LONG SeqMode)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "LmxSetSeqMode")]
        internal static extern unsafe int LmxSetSeqMode([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int LmIdx, [MarshalAs(UnmanagedType.I4)] int SeqMode);

        // 7. LmxGetSeqMode(__in LONG  LmIdx, __out  LONG SeqMode)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "LmxGetSeqMode")]
        internal static extern unsafe int LmxGetSeqMode([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int LmIdx, [MarshalAs(UnmanagedType.I4)] ref int SeqMode);

        // 8. LmxSetNextItemId(__in LONG	LmIdx, __in  LONG SeqId)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "LmxSetNextItemId")]
        internal static extern unsafe int LmxSetNextItemId([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int LmIdx, [MarshalAs(UnmanagedType.I4)] int SeqId);

        // 9. LmxGetNextItemId(__in LONG	LmIdx, __out LONG *SeqId)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "LmxGetNextItemId")]
        internal static extern unsafe int LmxGetNextItemId([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int LmIdx, [MarshalAs(UnmanagedType.I4)] ref int SeqId);

        // 10. LmxSetNextItemParam(__in LONG	LmIdx, __in  LONG ParamIdx, __in  LONG ParamData)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "LmxSetNextItemParam")]
        internal static extern unsafe int LmxSetNextItemParam([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int LmIdx, [MarshalAs(UnmanagedType.I4)] int ParamIdx, [MarshalAs(UnmanagedType.I4)] int ParamData);

        // 11. LmxGetNextItemParam(__in LONG	LmIdx, __in  LONG ParamIdx, __out  PLONG ParamData)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "LmxGetNextItemParam")]
        internal static extern unsafe int LmxGetNextItemParam([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int LmIdx, [MarshalAs(UnmanagedType.I4)] int ParamIdx, [MarshalAs(UnmanagedType.I4)] ref int ParamData);

        // 12. LmxGetRunItemParam(__in LONG	LmIdx, __in  LONG ParamIdx, __out  PLONG ParamData)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "LmxGetRunItemParam")]
        internal static extern unsafe int LmxGetRunItemParam([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int LmIdx, [MarshalAs(UnmanagedType.I4)] int ParamIdx, [MarshalAs(UnmanagedType.I4)] ref int ParamData);

        // 13. LmxGetRunItemStaPos(__in LONG 	LmIdx, __in  LONG Axis,     __out DOUBLE * Position)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "LmxGetRunItemStaPos")]
        internal static extern unsafe int LmxGetRunItemStaPos([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int LmIdx, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.R8)] ref double Position);

        // 14. LmxGetRunItemTargPos(__in LONG	LmIdx, __in  LONG Axis,     __out DOUBLE * Position)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "LmxGetRunItemTargPos")]
        internal static extern unsafe int LmxGetRunItemTargPos([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int LmIdx, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.R8)] ref double Position);

        // 15. LmxSetSeqId(__in LONG BoardId, __in LONG LmIdx, __in  LONG SeqId)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "LmxSetSeqId")]
        internal static extern unsafe int LmxSetSeqId([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int LmIdx, [MarshalAs(UnmanagedType.I4)] int SeqId);

        // 16. LmxGetSeqId(__in LONG BoardId, __in LONG LmIdx, __out PLONG pSeqId)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "LmxGetSeqId")]
        internal static extern unsafe int LmxGetSeqId([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int LmIdx, [MarshalAs(UnmanagedType.I4)] ref int pSeqId);

        //==================== Status Monitoring ===================================//
        // 1. StSetCount(__in LONG BoardId, __in LONG Channel, __in LONG Source, __in  LONG pdwCount)
        // [IN] VT_I4 BoardId : 보드ID, [IN] VT_I4 Channel : 축번호, [IN] VT_I4 Source : 카운터 번호(0:Command Counter, 1:Feedback Counter), [IN] VT_I4 pdwCount : 설정할 카운터 값(펄스 카운트)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "StSetCount")]
        internal static extern unsafe int StSetCount([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] int Source, [MarshalAs(UnmanagedType.I4)] int pdwCount);

        // 2. StGetCount(__in LONG BoardId, __in LONG Channel, __in LONG Source, __out PLONG pdwCount)
        // [IN] VT_I4 BoardId : 보드ID, [IN] VT_I4 Channel : 축번호, [IN] VT_I4 Source : 카운터 번호(0:Command Counter, 1:Feedback Counter), [IN] VT_PI4 pdwcount : 카운터 값(펄스 카운트)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "StGetCount")]
        internal static extern unsafe int StGetCount([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] int Source, [MarshalAs(UnmanagedType.I4)] ref int pdwCount);

        // 3. StSetPosition(__in LONG BoardId, __in LONG Channel, __in LONG Source, __in  DOUBLE Count)
        // [IN] VT_I4 BoardId : 보드ID, [IN] VT_I4 Channel : 축번호, [IN] VT_I4 Source : 카운터 번호(0:Command Counter, 1:Feedback Counter), [IN] VT_PI4 pdwcount : 카운터 값(논리 카운트)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "StSetPosition")]
        internal static extern unsafe int StSetPosition([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] int Source, [MarshalAs(UnmanagedType.R8)] double Count);

        // 4. StGetPosition(__in LONG BoardId, __in LONG Channel, __in LONG Source, __out DOUBLE * Count)
        // [IN] VT_I4 BoardId : 보드ID, [IN] VT_I4 Channel : 축번호, [IN] VT_I4 Source : 카운터 번호(0:Command Counter, 1:Feedback Counter), [IN] VT_PI4 pdwcount : 카운터 값(논리 카운트)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "StGetPosition")]
        internal static extern unsafe int StGetPosition([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] int Source, [MarshalAs(UnmanagedType.R8)] ref double Count);

        // 5. StGetSpeed(__in LONG BoardId, __in LONG Channel, __in LONG Source, __out DOUBLE * Speed)
        // [IN] VT_I4 BoardId : 보드ID, [IN] VT_I4 Channel : 축번호, [IN] VT_I4 Source : 카운터 번호(0:Command Counter, 1:Feedback Counter), [IN] VT_PR8 Speed : 카운터 값(논리 카운트)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "StGetSpeed")]
        internal static extern unsafe int StGetSpeed([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] int Source, [MarshalAs(UnmanagedType.R8)] ref double Speed);

        // 6. StSetTorque(__in LONG BoardId, __in LONG Channel, __in LONG Torque)
        // [IN] VT_I4 BoardId : 보드ID, [IN] VT_I4 Channel : 축번호, [IN] VT_I4 Torque
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "StSetTorque")]
        internal static extern unsafe int StSetTorque([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] int Torque);

        // 7. StGetTorque(__in LONG BoardId, __in LONG Channel, __out PLONG Torque)
        // [IN] VT_I4 BoardId : 보드ID, [IN] VT_I4 Channel : 축번호, [OUT] VT_PI4 Torque
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "StGetTorque")]
        internal static extern unsafe int StGetTorque([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] ref int Torque);

        // 8. StSetTorqueVelLimit(__in LONG BoardId, __in LONG Channel, __in LONG TorqueVelLimit)
        // [IN] VT_I4 BoardId : 보드ID, [IN] VT_I4 Channel : 축번호, [IN] VT_I4 TorqueVelLimit
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "StSetTorqueVelLimit")]
        internal static extern unsafe int StSetTorqueVelLimit([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] int TorqueVelLimit);

        // 9. StGetTorqueVelLimit(__in LONG BoardId, __in LONG Channel, __out PLONG TorqueVelLimit)
        // [IN] VT_I4 BoardId : 보드ID, [IN] VT_I4 Channel : 축번호, [OUT] VT_PI4 TorqueVelLimit
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "StGetTorqueVelLimit")]
        internal static extern unsafe int StGetTorqueVelLimit([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] ref int TorqueVelLimit);

		// 10. StSetTorqueLimit(__in LONG BoardId, __in LONG Channel, __in LONG nDir, __in LONG TorqueLimit);
		// [IN] VT_I4 BoardId : 보드ID, [IN] VT_I4 Channel : 축번호, [IN] VT_I4 nDir : 방향, [IN] VT_I4 TorqueLimit : 토크 제한치
		[DllImport("ComiSSCNET3.DLL", EntryPoint = "StSetTorqueLimit")]
        internal static extern unsafe int StSetTorqueLimit([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] int nDir, [MarshalAs(UnmanagedType.I4)] int TorqueLimit);
        
        // 11. StGetTorqueLimit(__in LONG BoardId, __in LONG Channel, __in LONG nDir, __out PLONG TorqueLimit);
		// [IN] VT_I4 BoardId : 보드ID, [IN] VT_I4 Channel : 축번호, [IN] VT_I4 nDir : 방향, [IN] VT_PI4 TorqueLimit : 토크 제한치
		[DllImport("ComiSSCNET3.DLL", EntryPoint = "StGetTorqueLimit")]
        internal static extern unsafe int StGetTorqueLimit([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] int nDir, [MarshalAs(UnmanagedType.I4)] ref int TorqueLimit);
		
		// 12. StSetVelocity(__in LONG BoardId, __in LONG Channel, __in LONG Velocity);
		// [IN] VT_I4 BoardId : 보드ID, [IN] VT_I4 Channel : 축번호, [IN] VT_I4 Velocity : 설정 속도값
		[DllImport("ComiSSCNET3.DLL", EntryPoint = "StSetVelocity")]
        internal static extern unsafe int StSetVelocity([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] int Velocity);
        
        // 13. StGetVelocity(__in LONG BoardId, __in LONG Channel, __out PLONG Velocity);
		// [IN] VT_I4 BoardId : 보드ID, [IN] VT_I4 Channel : 축번호, [IN] VT_PI4 Velocity : 설정 속도값
		[DllImport("ComiSSCNET3.DLL", EntryPoint = "StGetVelocity")]
        internal static extern unsafe int StGetVelocity([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] ref int Velocity);
		
        // 14. StSxReadMotionState(__in LONG BoardId, __in LONG Channel, __out PLONG MotStates)
        // [IN] VT_I4 BoardId : 보드ID, [IN] VT_I4 Channel : 축번호, [OUT] VT_PI4 MotStates
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "StSxReadMotionState")]
        internal static extern unsafe int StSxReadMotionState([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] ref int MotStates);

        // 15. StIxReadMotionState(__in LONG BoardId, __in LONG MapIndex, __out PLONG MotStates)
        // [IN] VT_I4 BoardId : 보드ID, [IN] VT_I4 MapIndex : 맵번호, [OUT] VT_PI4 MotStates
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "StIxReadMotionState")]
        internal static extern unsafe int StIxReadMotionState([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.I4)] ref int MotStates);

        // 16. StReadMioStatuses(__in LONG BoardId, __in LONG Channel, __out PLONG MioStates)
        // [IN] VT_I4 BoardId : 보드ID, [IN] VT_I4 Channel : 축번호, [OUT] VT_PI4 MioStates : Machine I/O 상태
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "StReadMioStatuses")]
        internal static extern unsafe int StReadMioStatuses([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] ref int MioStates);

        // 17. StGetMotionMode(__in LONG BoardId, __in LONG Channel, __out PLONG Mode)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "StGetMotionMode")]
        internal static extern unsafe int StGetMotionMode([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] ref int Mode);

        // 18. StSxGetLastError(__in LONG BoardId, __in LONG Channel, __out PLONG LastError)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "StSxGetLastError")]
        internal static extern unsafe int StSxGetLastError([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Channel, [MarshalAs(UnmanagedType.I4)] ref int LastError);

        // 19. StIxGetLastError(__in LONG BoardId, __in LONG MapIndex,__out PLONG LastError)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "StIxGetLastError")]
        internal static extern unsafe int StIxGetLastError([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int MapIndex, [MarshalAs(UnmanagedType.I4)] ref int LastError);

        // 20. StSetMultiRevCnt(__in LONG BoardId, __in LONG Axis, __in LONG MultiRevCnt)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "StSetMultiRevCnt")]
        internal static extern unsafe int StSetMultiRevCnt([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] int MultiRevCnt);

        // 21. StGetMultiRevCnt(__in LONG BoardId, __in LONG Axis, __out PLONG MultiRevCnt)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "StGetMultiRevCnt")]
        internal static extern unsafe int StGetMultiRevCnt([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] ref int MultiRevCnt);

        // 22. StSetOneRevPos(__in LONG BoardId, __in LONG Axis, __in LONG OneRevPos)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "StSetOneRevPos")]
        internal static extern unsafe int StSetOneRevPos([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] int OneRevPos);

        // 23. StGetOneRevPos(__in LONG BoardId, __in LONG Axis, __out PLONG pOneRevPos)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "StGetOneRevPos")]
        internal static extern unsafe int StGetOneRevPos([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Axis, [MarshalAs(UnmanagedType.I4)] ref int pOneRevPos);
        
        
        
        //==================== Advanced Function ===================================//
        // [RTS Update Functions]
        // RTS Update 기능을 활성화 할것인지에 대하여 설정하고 현재 RTS Update 활성 상태를 반환합니다.
        // 1. AdvSetRtsEnable(__in LONG BoardId, __in LONG IsEnable)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "AdvSetRtsEnable")]
        internal static extern unsafe int AdvSetRtsEnable([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int IsEnable);

        // 2. AdvGetRtsEnable(__in LONG BoardId, __out LONG *pIsEnable)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "AdvGetRtsEnable")]
        internal static extern unsafe int AdvGetRtsEnable([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] ref int pIsEnable);
        
        // 해당 Node의 RTS Update를 수행할 것인지에 대하여 설정하고 현재 Node의 RTS Update 상태를 반환합니다.
        // 3. AdvSetRtsMode(__in LONG BoardId, __in LONG NodeId, __in LONG IsEnable)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "AdvSetRtsMode")]
        internal static extern unsafe int AdvSetRtsMode([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int NodeId, [MarshalAs(UnmanagedType.I4)] int IsEnable);

        // 4. AdvGetRtsMode(__in LONG BoardId, __in LONG NodeId, __out LONG *pIsEnable)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "AdvGetRtsMode")]
        internal static extern unsafe int AdvGetRtsMode([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int NodeId, [MarshalAs(UnmanagedType.I4)] ref int IsEnable);
        
        // RTS Structure의 Update Interval을 설정 또는 반환합니다.
        // 5. AdvSetRtsUpdateInterval(__in LONG BoardId, __in LONG RtsUpdateInterval)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "AdvSetRtsUpdateInterval")]
        internal static extern unsafe int AdvSetRtsUpdateInterval([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int RtsUpdateInterval);

        // 6. AdvGetRtsUpdateInterval(__in LONG BoardId, __out PLONG pRtsUpdateInterval)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "AdvGetRtsUpdateInterval")]
        internal static extern unsafe int AdvGetRtsUpdateInterval([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] ref int RtsUpdateInterval);
        
        // [Command Acknowledge Setting Functions]
        // API 함수의 응답 모드를 설정하고 현재 응답 모드를 반환합니다.
        // 7. AdvSetCmdAckMode(__in LONG BoardId, __in LONG AckMode)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "AdvSetCmdAckMode")]
        internal static extern unsafe int AdvSetCmdAckMode([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int AckMode);

        // 8. AdvGetCmdAckMode(__in LONG BoardId, __out PLONG pAckMode)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "AdvGetCmdAckMode")]
        internal static extern unsafe int AdvGetCmdAckMode([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] ref int pAckMode);
        
        // [NEMO Firmware Functions(Undocumented)]
        // 9. AdvFwGetVersion(__in LONG BoardId, __out PLONG VersionMS, __out PLONG VersionLS)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "AdvFwGetVersion")]
        internal static extern unsafe int AdvFwGetVersion([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] ref int VersionMS, [MarshalAs(UnmanagedType.I4)] ref int VersionLS);

        // 10. AdvFwGetSystemState(__in LONG BoardId, __out PLONG State)
        // 현재 펌웨어 업데이트 / 일반 모드로 동작하고 있는지를 상태를 얻어온다.
        // [IN] VT_I4 BoardId : 보드ID, [IN] IsAnswerBit,	[OUT] State
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "AdvFwGetSystemState")]
        internal static extern unsafe int AdvFwGetSystemState([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] ref int State);

        // 11. AdvFwDnFrame(__in LONG BoardId, __in LONG FrameType, __in PLONG FrameData, __in LONG FrameSize)
        // START FRAME을 전송한다. frame data는 어플리케이션에서 조합 생성하며, data는 DWORD array로 전달한다.
        // [IN] VT_I4 BoardId : 보드ID, [IN] IsAnswerBit,	[IN] FrameData, [IN] FrameSize
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "AdvFwDnFrame")]
        internal static extern unsafe int AdvFwDnFrame([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int FrameType, [MarshalAs(UnmanagedType.I4)] ref int FrameData, [MarshalAs(UnmanagedType.I4)] int FrameSize);

        // 12. AdvFwDnFrameVerify(__in LONG BoardId, __in LONG FrameType, __in PLONG FrameData, __in LONG FrameSize)
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "AdvFwDnFrameVerify")]
        internal static extern unsafe int AdvFwDnFrameVerify([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int FrameType, [MarshalAs(UnmanagedType.I4)] ref int FrameData, [MarshalAs(UnmanagedType.I4)] int FrameSize);

        // 13. AdvFwSystemReset(__in LONG BoardId, __in LONG IsReset)
        // NC을 리셋한다.
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "AdvFwSystemReset")]
        internal static extern unsafe int AdvFwSystemReset([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int IsReset);

        // 14. AdvFwSetFwuBit(__in LONG BoardId, __in LONG IsAnswer, __in LONG Value)
        // FWU/FWUA bit값을 변경한다.
        // [IN] VT_I4 BoardId : 보드ID, [IN] IsAnswerBit,	[IN] IsAnswerBit,	[IN] Value
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "AdvFwSetFwuBit")]
        internal static extern unsafe int AdvFwSetFwuBit([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int IsAnswer, [MarshalAs(UnmanagedType.I4)] int Value);

        // 15. AdvFwGetFwuBit(__in LONG BoardId, __in LONG IsAnswer, __out PLONG pValue)
        // FWU/FWUA bit값을 읽어온다.
        // [IN] VT_I4 BoardId : 보드ID, [IN] IsAnswerBit,	[IN] IsAnswerBit,	[OUT] pValue
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "AdvFwGetFwuBit")]
        internal static extern unsafe int AdvFwGetFwuBit([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int IsAnswer, [MarshalAs(UnmanagedType.I4)] ref int pValue);

        // 16. AdvFwSetBootFlag(__in LONG BoardId, __in LONG Value)
        // BOTFLAG 값을 변경한다.
        // [IN] VT_I4 BoardId : 보드ID, [IN] IsAnswerBit,	[OUT] pValue
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "AdvFwSetBootFlag")]
        internal static extern unsafe int AdvFwSetBootFlag([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int Value);

        // 17. AdvFwGetBootFlag(__in LONG BoardId, __out PLONG pValue)
        // BOTFLAG 값을 읽어온다.
        // [IN] VT_I4 BoardId : 보드ID, [IN] IsAnswerBit,	[OUT] pValue
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "AdvFwGetBootFlag")]
        internal static extern unsafe int AdvFwGetBootFlag([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] ref int pValue);

        // 18. AdvFwUpdateMode(__in LONG BoardId, __in LONG IsEnable)
        // 펌웨어 업데이트 모드를 변경한다.
        // [IN] VT_I4 BoardId : 보드ID, [IN] IsAnswerBit,	[IN] Value
        [DllImport("ComiSSCNET3.DLL", EntryPoint = "AdvFwUpdateMode")]
        internal static extern unsafe int AdvFwUpdateMode([MarshalAs(UnmanagedType.I4)] int BoardId, [MarshalAs(UnmanagedType.I4)] int IsEnable);
    }
}
#endif