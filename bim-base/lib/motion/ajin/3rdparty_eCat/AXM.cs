/**
 * @file AXM.cs
 * 
 * @brief 아진엑스텍 모션 라이브러리 헤더 파일
 *
 * @author 아진엑스텍 주식회사
 * 
 * @copyright 저작권 (c) 아진엑스텍 주식회사
 *
 * @website http://www.ajinextek.com
 *
 * @last_update 2024-12-15
 * 
 * @details 자세한 정보는 매뉴얼 참고해 주세요.
 */


using System.Runtime.InteropServices;

public class CAXM
{
    // 보드 및 모듈 확인 함수
	
    /**
     * @brief 해당 축의 보드번호, 모듈 위치, 모듈 아이디 확인
     * 
     * @param lAxisNo 축 번호
     * @param lpBoardNo 보드 번호 저장
     * @param lpModulePos 모듈 위치 저장
     * @param upModuleID 모듈 아이디 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmInfoGetAxis(int nAxisNo, ref int npBoardNo, ref int npModulePos, ref uint upModuleID);
	
    /**
     * @brief 지정한 모듈 번호 해당 모듈의 Sub ID, 모듈 이름, 모듈 설명 확인
     * 
     * @param lAxisNo 축 번호
     * @param upModuleSubID EtherCAT 모듈을 구분하기 위한 SubID 저장
     * @param szModuleName 모듈의 모델명 저장 (50 Bytes)
     * @param szModuleDescription 모듈 설명 저장 (80 Bytes)
     *
	 * @note 지원 제품: PCIe-RxxIF-ECAT 전용
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmInfoGetAxisEx(int nAxisNo, ref uint upModuleSubID, System.Text.StringBuilder szModuleName, System.Text.StringBuilder szModuleDescription);

    /**
     * @brief 모션 모듈 존재 여부 확인
     * 
     * @param upStatus 모션 모듈 존재 여부 정보 저장 (0: 모듈 없음, 1: 모듈 존재)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmInfoIsMotionModule(ref uint upStatus);
    
	/**
     * @brief 해당 축이 유효한지 확인
     * 
     * @param nAxisNo 축 번호
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmInfoIsInvalidAxisNo(int nAxisNo);

	/**
     * @brief 해당 축이 제어 가능 상태인지 확인
     * 
     * @param nAxisNo 축 번호
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmInfoGetAxisStatus(int nAxisNo);
    
	/**
     * @brief 시스템 내 유효한 모션 축 수 반환
     * 
     * @param npAxisCount 유효한 모션 축 수 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmInfoGetAxisCount(ref int npAxisCount);
    
	/**
     * @brief 해당 보드/모듈의 첫 번째 축 번호 반환
     * 
     * @param nBoardNo 보드 번호
     * @param nModulePos 모듈 위치
     * @param npAxisNo 첫 번째 축 번호 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmInfoGetFirstAxisNo(int nBoardNo, int nModulePos, ref int npAxisNo);
    
	/**
      * @brief 해당 보드의 첫 번째 축 번호 반환
      *
      * @param nBoardNo 보드 번호
      * @param nModulePos 모듈 위치
      * @param npAxisNo 첫 번째 축 번호 저장
      *
      * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
      */
    [DllImport("AXL.dll")] public static extern uint AxmInfoGetBoardFirstAxisNo(int nBoardNo, int nModulePos, ref int npAxisNo);

	// 가상 축 함수

    /**
     * @brief 가상 축 설정
     * 
     * @param nReanAxisNo 실제 축 번호
     * @param nVirtuanAxisNo 설정 가상 축 번호
	 *
	 * @note
	 * 초기 상태에서 AXM 모든 함수의 축 번호 설정은 0 ~ (실제 시스템에 장착된 축수 - 1) 범위에서 유효.
	 * 하지만 이 함수를 사용하여 실제 장착된 축 번호 대신 임의의 축 번호로 바꿀 수 있음.
	 * 이 함수는 제어 시스템의 H/W 변경 사항 발생 시 기존 프로그램에 할당 된 축 번호를 그대로 유지하고 실제 제어 축의 물리적인 위치를 변경하여 사용을 위해 만들어진 함수.
	 * 주의사항: 여러 개의 실제 축 번호에 대하여 같은 번호로 가상 축을 중복해서 맵핑하지 말아야 함.
	 *        중복 맵핑된 경우 실제 축 번호가 낮은 축만 가상 축 번호로 제어 할 수 있으며 나머지 같은 가상축 번호로 맵핑된 축은 제어가 불가능 함.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmVirtualSetAxisNoMap(int nReanAxisNo, int nVirtuanAxisNo);
    
	/**
     * @brief 설정한 가상 축 확인
     * 
     * @param nReanAxisNo 실제 축 번호
     * @param nVirtuanAxisNo 설정한 가상 축 번호 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmVirtualGetAxisNoMap(int nReanAxisNo, ref int npVirtuanAxisNo);
    
	/**
     * @brief 멀티 가상축 설정
     * 
     * @param lSize 가상 축 개수
     * @param lpRealAxesNo 실제 축 번호 배열
     * @param lpVirtualAxesNo 가상축 번호 배열
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmVirtualSetMultiAxisNoMap(int nSize, int[] npRealAxesNo, int[] npVirtualAxesNo);
    
	/**
     * @brief 설정한 멀티 가상축 번호 확인
     * 
     * @param lSize 가상축 개수
     * @param lpRealAxesNo 실제 축 번호 배열 저장
     * @param lpVirtualAxesNo 가상축 번호 배열 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmVirtualGetMultiAxisNoMap(int nSize, int[] npRealAxesNo, int[] npVirtualAxesNo);
    
	/**
     * @brief 가상축 설정 초기화
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmVirtualResetAxisMap();

    /**
     * @brief 축 번호 재정의

     * @param lSize 축 개수
     * @param lpReDefineAxesNo 재정의할 축 번호 배열
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmVirtualSetMultiReDefineAxisNo(int lSize, int[] lpReDefineAxesNo);
    
	/**
     * @brief 재정의된 축 번호 확인
     *
     * @param lSize 축 개수
     * @param lpReDefineAxesNo 재정의된 축 번호 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmVirtualGetMultiReDefineAxisNo(int lSize, int[] lpReDefineAxesNo);

	// 인터럽트 관련 함수

    /**
     * @brief 축에 인터럽트 설정
     * 
     * @param nAxisNo 축 번호
     * @param hWnd 윈도우 핸들. (윈도우 메세지 받을 때 사용. 미사용: NULL 입력)
     * @param uMessage 윈도우 핸들 메세지 (사용하지 않거나 디폴트값을 사용하려면 0 입력)
     * @param pProc 인터럽트 발생 시 호출될 함수 포인터 (미사용: NULL 입력)
     * @param pEvent 이벤트 방법 사용 시 이벤트 핸들
     *
	 * @note
	 * 1. 콜백 함수 방식
	 *    1) 장점: 이벤트 발생 시점에 즉시 콜백 함수가 호출 됨으로 가장 빠르게 이벤트를 통지받을 수 있음
	 *    2) 단점: 콜백 함수가 완전히 종료 될 때까지 메인 프로세스 정체. 
	 *       콜백 함수 내에 부하가 걸리는 작업이 있을 경우 사용에 주의를 요함.
	 * 2. 이벤트 방식
	 *    1) 장점: 쓰레드를 이용하여 인터럽트 발생 여부를 감시하고 있다가 인터럽트 발생 시 처리
	 *       가장 빠르게 인터럽트를 검출 & 처리
	 *       MultiProcessor 시스템에서 자원을 가장 효율적으로 사용할 수 있어 특히 권장하는 방식
	 *    2) 단점: 쓰레드 등으로 인해 시스템 자원 점유
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmInterruptSetAxis(int nAxisNo, uint hWnd, uint uMessage, CAXHS.AXT_INTERRUPT_PROC pProc, ref uint pEvent);

    /**
     * @brief 지정한 축의 인터럽트 사용 유무 설정
     *
     * @param nAxisNo 축 번호
     * @param uUse 인터럽트 사용 여부 (0: DISABLE, 1: ENABLE)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmInterruptSetAxisEnable(int nAxisNo, uint uUse);

	/**
     * @brief 지정한 축의 인터럽트 사용 유무 설정 확인
     *
     * @param nAxisNo 축 번호
     * @param upUse 인터럽트 사용 여부 확인 (0: DISABLE, 1: ENABLE)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmInterruptGetAxisEnable(int nAxisNo, ref uint upUse);

    /**
     * @brief 인터럽트 발생 여부 확인
     *
     * @param npAxisNo 축 번호
     * @param upFlag 인터럽트 발생 여부 (0: 인터럽트 미발생, 1: 인터럽트 발생)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmInterruptRead(ref int npAxisNo, ref uint upFlag);

    /**
     * @brief 해당 축의 인터럽트 플래그 값 확인
     * 
     * @param nAxisNo 축 번호
     * @param lBank 해당 축의 인터럽트 뱅크 번호 (0: Bank1, 1: Bank2)
     * @param upFlag 인터럽트 발생 상태 값 (0: 인터럽트 미발생, 1: 인터럽트 발생)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmInterruptReadAxisFlag(int nAxisNo, int nBank, ref uint upFlag);

    /**
     * @brief 지정한 축의 사용자가 설정한 인터럽트 발생 여부 설정
     * 
     * @param nAxisNo 축 번호
     * @param lBank 해당 축의 인터럽트 뱅크 번호 (0: Bank1, 1: Bank2)
     * @param uInterruptNum 사용자가 원하는 인터럽트 Bit 조합 (0~31 bit)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmInterruptSetUserEnable(int nAxisNo, int nBank, uint uInterruptNum);

    /**
     * @brief 지정한 축의 사용자가 설정한 인터럽트 발생 여부 설정 확인
     * 
     * @param nAxisNo 축 번호
     * @param lBank 해당 축의 인터럽트 뱅크 번호 (0: Bank1, 1: Bank2)
     * @param uInterruptNum 사용자가 원하는 인터럽트 Bit 조합 정보 저장 (0~31 bit)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmInterruptGetUserEnable(int nAxisNo, int nBank, ref uint upInterruptNum);

    // 모션 파라메타 설정
	
	/*
       AxmMotLoadParaAll로 파일을 Load 시키지 않으면 초기 파라메타 설정시 기본 파라메타 설정. 
       현재 PC에 사용되는 모든축에 똑같이 적용된다. 기본파라메타는 아래와 같다. 
       00: AXIS_NO.             =0          01: PULSE_OUT_METHOD.    =4         02: ENC_INPUT_METHOD.    =3     03: INPOSITION.          =2
       04: ALARM.               =1          05: NEG_END_LIMIT.       =1         06: POS_END_LIMIT.       =1     07: MIN_VELOCITY.        =1
       08: MAX_VELOCITY.        =700000     09: HOME_SIGNAL.         =4         10: HOME_LEVEL.          =1     11: HOME_DIR.            =0
       12: ZPHASE_LEVEL.        =1          13: ZPHASE_USE.          =0         14: STOP_SIGNAL_MODE.    =0     15: STOP_SIGNAL_LEVEL.   =1
       16: HOME_FIRST_VELOCITY. =100        17: HOME_SECOND_VELOCITY.=100       18: HOME_THIRD_VELOCITY. =20    19: HOME_LAST_VELOCITY.  =1
       20: HOME_FIRST_ACCEL.    =400        21: HOME_SECOND_ACCEL.   =400       22: HOME_END_CLEAR_TIME. =1000  23: HOME_END_OFFSET.     =0
       24: NEG_SOFT_LIMIT.      =-134217728 25: POS_SOFT_LIMIT.      =134217727 26: MOVE_PULSE.          =1     27: MOVE_UNIT.           =1
       28: INIT_POSITION.       =1000       29: INIT_VELOCITY.       =200       30: INIT_ACCEL.          =400   31: INIT_DECEL.          =400
       32: INIT_ABSRELMODE.     =0          33: INIT_PROFILEMODE.    =4         34: SVON_LEVEL.          =1     35: ALARM_RESET_LEVEL.   =1
       36: ENCODER_TYPE.        =1          37: SOFT_LIMIT_SEL.      =0         38: SOFT_LIMIT_STOP_MODE.=0     39: SOFT_LIMIT_ENABLE.   =0

       00=[AXIS_NO             ] 축 (0축 부터 시작함)
       01=[PULSE_OUT_METHOD    ] Pulse out method TwocwccwHigh = 6
       02=[ENC_INPUT_METHOD    ] disable = 0, 1체배 = 1, 2체배 = 2, 4체배 = 3, 결선 관련방향 교체시(-).1체배 = 11  2체배 = 12  4체배 = 13
       03=[INPOSITION          ] 
	   04=[ALARM               ]
	   05=[NEG_END_LIMIT       ] 0: B접점, 1: A접점, 2: 사용 안함, 3: 기존 상태 유지
	   06=[POS_END_LIMIT       ] 0: B접점, 1: A접점, 2: 사용 안함, 3: 기존 상태 유지
       07=[MIN_VELOCITY        ] 시작 속도(START VELOCITY)
       08=[MAX_VELOCITY        ] 드라이버가 지령을 받아들일수 있는 지령 속도. 보통 일반 Servo는 700k
                                 Ex> screw : 20mm pitch drive: 10000 pulse 모터: 400w
       09=[HOME_SIGNAL         ] 4 - Home in0 , 0: PosEndLimit , 1: NegEndLimit (_HOME_SIGNAL참조)
       10=[HOME_LEVEL          ] 0: B접점, 1: A접점, 2: 사용 안함, 3: 기존 상태 유지
       11=[HOME_DIR            ] 홈 방향(HOME DIRECTION) 1:+방향, 0:-방향
       12=[ZPHASE_LEVEL        ] 0: B접점, 1: A접점, 2: 사용 안함, 3: 기존 상태 유지
       13=[ZPHASE_USE          ] Z상사용여부. 0: 사용안함, 1: +방향, 2: -방향
       14=[STOP_SIGNAL_MODE    ] ESTOP, SSTOP 사용시 모드. 0: 감속정지, 1: 급정지 
       15=[STOP_SIGNAL_LEVEL   ] ESTOP, SSTOP 사용 레벨. 0: B접점, 1: A접점, 2: 사용 안함, 3: 기존 상태 유지
       16=[HOME_FIRST_VELOCITY ] 1차 구동 속도 
       17=[HOME_SECOND_VELOCITY] 검출 후 속도 
       18=[HOME_THIRD_VELOCITY ] 마지막 속도 
       19=[HOME_LAST_VELOCITY  ] index 검색 및 정밀하게 검색하기 위한 속도 
       20=[HOME_FIRST_ACCEL    ] 1차 가속도
	   21=[HOME_SECOND_ACCEL   ] 2차 가속도 
       22=[HOME_END_CLEAR_TIME ] 원점 검색 Enc 값 Set하기 위한 대기시간
	   23=[HOME_END_OFFSET     ] 원점 검출 후 Offset 만큼 이동.
       24=[NEG_SOFT_LIMIT      ] - SoftWare Limit 같게 설정하면 사용안함
	   25=[POS_SOFT_LIMIT      ] + SoftWare Limit 같게 설정하면 사용안함.
       26=[MOVE_PULSE          ] 드라이버의 1회전 당 펄스량
	   27=[MOVE_UNIT           ] 드라이버 1회전당 이동량 즉:스크류 Pitch
       28=[INIT_POSITION       ] 에이젼트 사용시 초기위치, 사용자가 임의로 사용가능
       29=[INIT_VELOCITY       ] 에이젼트 사용시 초기속도, 사용자가 임의로 사용가능
       30=[INIT_ACCEL          ] 에이젼트 사용시 초기가속도, 사용자가 임의로 사용가능
       31=[INIT_DECEL          ] 에이젼트 사용시 초기감속도, 사용자가 임의로 사용가능
       32=[INIT_ABSRELMODE     ] 절대(0)/상대(1) 위치 설정
       33=[INIT_PROFILEMODE    ] 프로파일모드(0~4) 까지 설정
                                 0: 대칭 Trapezode, 1: 비대칭 Trapezode, 2: 대칭 Quasi-S Curve, 3:대칭 S Curve, 4:비대칭 S Curve
       34=[SVON_LEVEL          ] 0: B접점, 1: A접점
       35=[ALARM_RESET_LEVEL   ] 0: B접점, 1: A접점
       36=[ENCODER_TYPE        ] 0: TYPE_INCREMENTAL, 1: TYPE_ABSOLUTE, 2: TYPE_NONE
       37=[SOFT_LIMIT_SEL      ] 0: COMMAND, 1: ACTUAL
       38=[SOFT_LIMIT_STOP_MODE] 0: EMERGENCY_STOP, 1: SLOWDOWN_STOP
       39=[SOFT_LIMIT_ENABLE   ] 0: DISABLE, 1: ENABLE
    */

    /**
     * @brief AxmMotSaveParaAll로 저장 되어진 mot 파일을 불러 옴
     * 
     * @param szFilePath mot 파일 경로
	 *
	 * @note 해당 파일은 사용자가 Edit 하여 사용 가능
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotLoadParaAll(string szFilePath);           // string
    [DllImport("AXL.dll")] public static extern uint AxmMotLoadParaAll(char[] szFilePath);           // char[]
    
	/**
     * @brief AxmMotLoadParaAll 함수로 Load한 mot 파일명 확인
     * 
     * @param szFileName mot 파일명 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
	[DllImport("AXL.dll")] public static extern uint AxmMotGetFileName(string szFileName);           // string
	[DllImport("AXL.dll")] public static extern uint AxmMotGetFileName(char[] szFileName);		     // char[]
	
	/**
     * @brief 모든 축에 대한 파라미터를 mot 파일로 저장
     * 
     * @param szFilePath 저장할 파일 경로
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSaveParaAll(string szFilePath);

    /**
     * @brief 파라메타 28-31번에 해당하는 파라미터 값 설정
     * 
     * @param nAxisNo 축 번호
     * @param dInitPos 초기 위치값
     * @param dInitVel 초기 속도값
     * @param dInitAccel 초기 가속도값
     * @param dInitDecel 초기 감속도값
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetParaLoad(int nAxisNo, double InitPos, double InitVel, double InitAccel, double InitDecel);
    
	/**
     * @brief 파라메타 28-31번에 해당하는 파라미터 값 설정 확인
     * 
     * @param nAxisNo 축 번호
     * @param dInitPos 초기 위치값 저장
     * @param dInitVel 초기 속도값 저장
     * @param dInitAccel 초기 가속도값 저장
     * @param dInitDecel 초기 감속도값 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetParaLoad(int nAxisNo, ref double InitPos, ref double InitVel, ref double InitAccel, ref double InitDecel);

    /**
     * @brief 지정 축의 펄스 출력 방식 설정
     * 
     * @param nAxisNo 축 번호
     * @param uMethod 펄스 출력 방식
	 *
	 * @details
	 * uMethod
	 *    OneHighLowHigh  = 0x0,	// 1펄스 방식, PULSE(Active High), 정방향(DIR=Low)  / 역방향(DIR=High)
	 *    OneHighHighLow  = 0x1,	// 1펄스 방식, PULSE(Active High), 정방향(DIR=High) / 역방향(DIR=Low)
	 *    OneLowLowHigh   = 0x2,	// 1펄스 방식, PULSE(Active Low),  정방향(DIR=Low)  / 역방향(DIR=High)
	 *    OneLowHighLow   = 0x3,	// 1펄스 방식, PULSE(Active Low),  정방향(DIR=High) / 역방향(DIR=Low)
	 *    TwoCcwCwHigh    = 0x4,	// 2펄스 방식, PULSE(CCW:역방향),  DIR(CW:정방향),  Active High     
 	 *    TwoCcwCwLow     = 0x5,	// 2펄스 방식, PULSE(CCW:역방향),  DIR(CW:정방향),  Active Low     
	 *    TwoCwCcwHigh    = 0x6,	// 2펄스 방식, PULSE(CW:정방향),   DIR(CCW:역방향), Active High
	 *    TwoCwCcwLow     = 0x7,	// 2펄스 방식, PULSE(CW:정방향),   DIR(CCW:역방향), Active Low
	 *    TwoPhase        = 0x8,	// 2상(90' 위상차),  PULSE lead DIR(CW: 정방향), PULSE lag DIR(CCW:역방향)
	 *    TwoPhaseReverse = 0x9,    // 2상(90' 위상차),  PULSE lead DIR(CCW: 정방향), PULSE lag DIR(CW:역방향)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetPulseOutMethod(int nAxisNo, uint uMethod);
    
	/**
     * @brief 지정 축의 펄스 출력 방식 설정 확인
     * 
     * @param nAxisNo 축 번호
     * @param uMethod 펄스 출력 방식 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetPulseOutMethod(int nAxisNo, ref uint upMethod);

    /**
     * @brief 지정 축의 외부(Actual) 카운트의 증가 방향 설정을 포함하여 지정 축의 Encoder 입력 방식 설정
     * 
     * @param nAxisNo 축 번호
     * @param uMethod Encoder 입력 방식 설정 (0~7)
	 *
	 * @details
	 * uMethod
	 *    ObverseUpDownMode    = 0x0,	// 정방향 Up/Down
	 *    ObverseSqr1Mode      = 0x1,	// 정방향 1체배
	 *    ObverseSqr2Mode      = 0x2,	// 정방향 2체배
	 *    ObverseSqr4Mode      = 0x3,	// 정방향 4체배
	 *    ReverseUpDownMode    = 0x4,	// 역방향 Up/Down
	 *    ReverseSqr1Mode      = 0x5,	// 역방향 1체배
	 *    ReverseSqr2Mode      = 0x6,	// 역방향 2체배
	 *    ReverseSqr4Mode      = 0x7,	// 역방향 4체배
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetEncInputMethod(int nAxisNo, uint uMethod);
    
	/**
     * @brief 지정 축의 외부(Actual) 카운트의 증가 방향 설정을 포함하여 지정 축의 Encoder 입력 방식 설정 확인
     * 
     * @param nAxisNo 축 번호
     * @param uMethod Encoder 입력 방식 설정 저장 (0~7)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetEncInputMethod(int nAxisNo, ref uint upMethod);

    /**
     * @brief 지정 축의 펄스 당 움직이는 거리 설정
     * 
     * @param nAxisNo 축 번호
     * @param dUnit 펄스 당 움직이는 거리의 단위 설정
     * @param nPulse 펄스 당 움직이는 거리의 펄스 수 설정
     *
	 * @details 자세한 설정 방법 및 예제는 매뉴얼 참고
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetMoveUnitPerPulse(int nAxisNo, double dUnit, int nPulse);
    
	/**
     * @brief 지정 축의 펄스 당 움직이는 거리 설정 정보 확인
     * 
     * @param nAxisNo 축 번호
     * @param dpUnit 펄스 당 움직이는 거리의 단위 설정 저장
     * @param npPulse 펄스 당 움직이는 거리의 펄스 수 설정 저장
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetMoveUnitPerPulse(int nAxisNo, ref double dpUnit, ref int npPulse);

    /**
     * @brief 지정 축의 감속 시작 포인트 검출 방식 설정
     * 
     * @param nAxisNo 축 번호
     * @param uMethod 감속 시작 포인트 검출 방식 설정
	 *
	 * @details
	 * uMethod
	 *    0: AutoDetect (자동 검출 방식)
	 *    1: RestPulse (나머지 검출 방식)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetDecelMode(int nAxisNo, uint uMethod);
    
	/**
     * @brief 지정 축의 감속 시작 포인트 검출 방식 확인
     * 
     * @param nAxisNo 축 번호
     * @param uMethod 감속 시작 포인트 검출 방식 저장
	 *
	 * @details
	 * uMethod
	 *    0: AutoDetect (자동 검출 방식)
	 *    1: RestPulse (나머지 검출 방식)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetDecelMode(int nAxisNo, ref uint upMethod);

    /**
     * @brief 지정 축에 수동 감속 모드에서 잔량 펄스 설정
     * 
     * @param nAxisNo 축 번호
     * @param uData 잔량 펄스 설정
     *
	 * @note
	 * 만약 AxmMotSetRemainPulse를 500 펄스 설정 시 AxmMoveStartPos를 위치 10000으로 보냈을 경우 
	 * 9500 펄스부터 남은 펄스 500은 AxmMotSetMinVel로 설정한 속도로 유지하면서 감속
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetRemainPulse(int nAxisNo, uint uData);
	
    /**
     * @brief 지정 축에 수동 감속 모드에서 잔량 펄스 설정 확인
     * 
     * @param nAxisNo 축 번호
     * @param upData 잔량 펄스 설정 저장
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */       
    [DllImport("AXL.dll")] public static extern uint AxmMotGetRemainPulse(int nAxisNo, ref uint upData);

    /**
     * @brief 모든 구동 함수의 최대 구동 속도를 지정 속도로 제한
     * 
     * @param nAxisNo 축 번호
     * @param dVel 최고 구동 속도 (Unit/Sec)
	 *
	 * @note
	 * 주의사항: 입력 최대 속도 값이 PPS가 아니라 UNIT 임
	 *    ex) 최대 출력 주파수(PCI-N804/404: 10 MPPS)
	 *    ex) 최대 출력 Unit/Sec(PCI-N804/404: 10MPPS * Unit/Pulse)
	 * 주의사항: A5Nx, A6Nx의 경우 최대 50 MPPS로 제한됨
	 *    ex) 최대 출력 주파수(A5Nx, A6Nx: 50 MPPS)
	 *    ex) 최대 출력 Unit/Sec(A5Nx, A6Nx : 50 MPPS * Unit/Pulse)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetMaxVel(int nAxisNo, double dVel);
    
	/**
     * @brief 모든 구동 함수의 최대 구동 속도를 지정 속도 확인
     * 
     * @param nAxisNo 축 번호
     * @param dpVel 최고 구동 속도 저장 (Unit/Sec)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetMaxVel(int nAxisNo, ref double dpVel);

    /**
     * @brief 지정 축의 이동 거리 계산 모드 설정
     * 
     * @param nAxisNo 축 번호
     * @param uAbsRelMode 모드 설정
	 *
	 * @details
	 * uAbsRelMode
	 *    0: POS_ABS_MODE (절대 좌표계)
	 *    1: POS_REL_MODE (상대 좌표계)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetAbsRelMode(int nAxisNo, uint uAbsRelMode);
    
	/**
     * @brief 지정 축의 이동 거리 계산 모드 확인
     * 
     * @param nAxisNo 축 번호
     * @param upAbsRelMode 설정 모드 저장
	 *
	 * @details
	 * uAbsRelMode
	 *    0: POS_ABS_MODE (절대 좌표계)
	 *    1: POS_REL_MODE (상대 좌표계)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetAbsRelMode(int nAxisNo, ref uint upAbsRelMode);

    /**
     * @brief 지정 축의 구동 속도 프로파일 설정
     * 
     * @param nAxisNo 축 번호
     * @param uProfileMode 속도 프로파일 모드
	 *
	 * @details
	 * uProfileMode
	 *    SYM_TRAPEZOIDE_MODE    '0' - 대칭 Trapezode
	 *    ASYM_TRAPEZOIDE_MODE   '1' - 비대칭 Trapezode
	 *    QUASI_S_CURVE_MODE     '2' - 대칭 Quasi-S Curve
	 *    SYM_S_CURVE_MODE       '3' - 대칭 S Curve
	 *    ASYM_S_CURVE_MODE      '4' - 비대칭 S Curve
	 *    SYM_TRAP_M3_SW_MODE    '5' - 대칭 Trapezode : MLIII 내부 S/W Profile
	 *    ASYM_TRAP_M3_SW_MODE   '6' - 비대칭 Trapezode : MLIII 내부 S/W Profile
	 *    SYM_S_M3_SW_MODE       '7' - 대칭 S Curve : MLIII 내부 S/W Profile
 	 *    ASYM_S_M3_SW_MODE      '8' - asymmetric S Curve : MLIII 내부 S/W Profile
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetProfileMode(int nAxisNo, uint uProfileMode);
    
	/**
     * @brief 지정 축의 구동 속도 프로파일 확인
     * 
     * @param nAxisNo 축 번호
     * @param uProfileMode 속도 프로파일 모드 저장 (0~8)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetProfileMode(int nAxisNo, ref uint upProfileMode);

    /**
     * @brief 지정 축의 가속도 단위 설정
     * 
     * @param nAxisNo 축 번호
     * @param uAccelUnit 가감속 단위
	 *
	 * @details
	 * uAccelUnit
	 *    0: UNIT_SEC2 (unit/sec2)
	 *    1: SEC (sec)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetAccelUnit(int nAxisNo, uint uAccelUnit);
    
	/**
     * @brief 지정 축의 가속도 단위 확인
     * 
     * @param nAxisNo 축 번호
     * @param upAccelUnit 가감속 단위 저장
	 *
	 * @details
	 * uAccelUnit
	 *    0: UNIT_SEC2 (unit/sec2)
	 *    1: SEC (sec)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetAccelUnit(int nAxisNo, ref uint upAccelUnit);

    /**
     * @brief 지정 축의 초기 속도 설정
     * 
     * @param nAxisNo 축 번호
     * @param dMinVel 초기 속도값 설정
	 *
	 * @note
	 * 주의사항: 최소 속도를 UNIT/PULSE 보다 작게 할 경우 최소 단위가 UNIT/PULSE로 맞춰지기 때문에 최소 속도가 UNIT/PULSE가 됨
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetMinVel(int nAxisNo, double dMinVelocity);
    
	/**
     * @brief 지정 축의 초기 속도 확인
     * 
     * @param nAxisNo 축 번호
     * @param dpMinVel 초기 속도값 저장
	 *
	 * @note
	 * 주의사항: 최소 속도를 UNIT/PULSE 보다 작게 할 경우 최소 단위가 UNIT/PULSE로 맞춰지기 때문에 최소 속도가 UNIT/PULSE가 됨
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetMinVel(int nAxisNo, ref double dpMinVelocity);

    /**
     * @brief 지정 축의 가속 저크값 설정
     * 
     * @param nAxisNo 축 번호
     * @param dAccelJerk 가속 저크값 설정 (단위: %)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetAccelJerk(int nAxisNo, double dAccelJerk);
    
	/**
     * @brief 지정 축의 가속 저크값 확인
     * 
     * @param nAxisNo 축 번호
     * @param dpAccelJerk 가속 저크값 저장 (단위: %)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetAccelJerk(int nAxisNo, ref double dpAccelJerk);

    /**
     * @brief 지정 축의 감속 저크값 설정
     * 
     * @param nAxisNo 축 번호
     * @param dDecelJerk 감속 저크값 설정 (단위: %)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetDecelJerk(int nAxisNo, double dDecelJerk);
    
	/**
     * @brief 지정 축의 감속 저크값 확인
     * 
     * @param nAxisNo 축 번호
     * @param dpDecelJerk 감속 저크값 저장 (단위: %)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetDecelJerk(int nAxisNo, ref double dpDecelJerk);

	/**
     * @brief 지정 축의 속도 프로파일 결정 시 우선순위 설정
     *
     * @param nAxisNo 축 번호
     * @param uPriority 속도 프로파일 결정 우선순위 설정 값
	 *
	 * @details
	 * uPriority
	 *    [0] PRIORITY_VELOCITY  - 속도 Profile 결정 시 지정한 속도값에 가깝도록 계산 (일반 장비 및 Spinner에 사용)
	 *    [1] PRIORITY_ACCELTIME - 속도 Profile 결정 시 지정한 가감속 시간에 가깝도록 계산 (고속 장비에 사용)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetProfilePriority(int nAxisNo, uint uPriority);
    
	/**
     * @brief 지정 축의 속도 프로파일 결정 시 우선순위 확인
     *
     * @param nAxisNo 축 번호
     * @param upPriority 속도 프로파일 결정 우선순위 설정 값 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetProfilePriority(int nAxisNo, ref uint upPriority);

    // 입출력 신호 관련 설정함수
    
	/**
     * @brief 지정 축의 Z 상 Level 설정
     * 
     * @param nAxisNo 축 번호
     * @param uLevel Z 상 Level 설정 값 (0: LOW, 1: HIGH)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetZphaseLevel(int nAxisNo, uint uLevel);
    
	/**
     * @brief 지정 축의 Z 상 Level 확인
     * 
     * @param nAxisNo 축 번호
     * @param upLevel Z 상 Level 저장 (0: LOW, 1: HIGH)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetZphaseLevel(int nAxisNo, ref uint upLevel);

    /**
     * @brief 지정 축의 Servo On 신호 출력 레벨 설정
     * 
     * @param nAxisNo 축 번호
     * @param uLevel Servo-On 신호 출력 레벨 값 (0: LOW, 1: HIGH)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetServoOnLevel(int nAxisNo, uint uLevel);
    
	/**
     * @brief 지정 축의 Servo On 신호 출력 레벨 확인
     * 
     * @param nAxisNo 축 번호
     * @param upLevel Servo-On 신호 출력 레벨 저장 (0: LOW, 1: HIGH)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */       
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetServoOnLevel(int nAxisNo, ref uint upLevel);

    /**
     * @brief 지정 축의 Servo-Alarm Reset 신호의 출력 레벨 설정
     * 
     * @param nAxisNo 축 번호
     * @param uLevel Servo-Alarm Reset 신호의 출력 레벨 값 (0: LOW, 1: HIGH)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetServoAlarmResetLevel(int nAxisNo, uint uLevel);
    
	/**
     * @brief 지정 축의 Servo-Alarm Reset 신호의 출력 레벨 확인
     * 
     * @param nAxisNo 축 번호
     * @param upLevel Servo-Alarm Reset 신호의 출력 레벨 값 저장 (0: LOW, 1: HIGH)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetServoAlarmResetLevel(int nAxisNo, ref uint upLevel);

    /**
     * @brief 지정 축의 Inpositon 신호 사용 여부 및 신호 입력 레벨 설정
     * 
     * @param nAxisNo 축 번호
     * @param uUse Inposition 신호 사용 여부 (0: LOW, 1: HIGH, 2: UNUSED, 3: USED)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */       
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetInpos(int nAxisNo, uint uUse);
    
	/**
     * @brief 지정 축의 Inpositon 신호 사용 여부 및 신호 입력 레벨 확인
     * 
     * @param nAxisNo 축 번호
     * @param upUse Inposition 신호 사용 여부 확인 (0: LOW, 1: HIGH, 2: UNUSED, 3: USED)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */     
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetInpos(int nAxisNo, ref uint upUse);
    
	/**
     * @brief 지정 축의 Inpositon 신호 입력 상태 반환
     * 
     * @param nAxisNo 축 번호
     * @param upStatus Inposition 신호 입력 상태 (0: 비활성화, 1: 활성화)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalReadInpos(int nAxisNo, ref uint upStatus);

	/**
     * @brief 지정 축의 Inposition Settling Time 설정
     * 
     * @param nAxisNo 축 번호
     * @param dwSettlingTime Settling Time 설정 값 (단위: mSec)
     *
	 * @details Settling Time 값 설정 시 Cycle Time보다 크고, Cycle Time의 배수여야 함
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
	[DllImport("AXL.dll")] public static extern uint AxmSignalSetSettlingTimeInpos(int nAxisNo, uint dwSettlingTime);
	
	/**
     * @brief 지정 축의 Inposition Settling Time 설정 확인
     * 
     * @param nAxisNo 축 번호
     * @param dwpSettlingTime Settling Time 설정 값 저장 (단위: mSec)
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
	[DllImport("AXL.dll")] public static extern uint AxmSignalGetSettlingTimeInpos(int nAxisNo, ref uint dwpSettlingTime);

    /**
     * @brief 지정 축의 알람 신호 입력 시 비상 정지의 사용 여부 및 신호 입력 레벨 설정
     * 
     * @param nAxisNo 축 번호
     * @param uUse 알람 신호 사용 여부 (0: LOW, 1: HIGH, 2: UNUSED, 3: USED)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetServoAlarm(int nAxisNo, uint uUse);
    
	/**
     * @brief 지정 축의 알람 신호 입력 시 비상 정지의 사용 여부 및 신호 입력 레벨 설정 값 확인
     * 
     * @param nAxisNo 축 번호
     * @param upUse 알람 신호 사용 여부 저장 (0: LOW, 1: HIGH, 2: UNUSED, 3: USED)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetServoAlarm(int nAxisNo, ref uint upUse);
    
	/**
     * @brief 지정 축의 알람 신호의 입력 레벨 반환
     *
     * @param nAxisNo 축 번호
     * @param upStatus 알람 신호 입력 레벨 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalReadServoAlarm(int nAxisNo, ref uint upStatus);

    /**
     * @brief 지정 축의 end limit sensor의 사용 유무 및 신호의 입력 레벨 설정
     * 
     * @param nAxisNo 축 번호
     * @param uStopMode 리미트 센서 검출 후 정지 모드 설정 (0: EMERGENCY_STOP, 1: SLOWDOWN_STOP)
     * @param uPositiveLevel (+) 리미트 센서 Active Level (0: LOW, 1: HIGH, 2: UNUSED, 3: USED)
     * @param uNegativeLevel (-) 리미트 센서 Active Level (0: LOW, 1: HIGH, 2: UNUSED, 3: USED)
     *
	 * @details
	 * PCIe-RxxIF-ECAT 제품일 경우
     * @param uPositiveLevel (+) 리미트 센서 Active Level (0: LOW, 1: HIGH, 2: UNUSED, 3: USED, 4: LOW_ONLYSTOP, 5: HIGH_ONLYSTOP)
     * @param uNegativeLevel (-) 리미트 센서 Active Level (0: LOW, 1: HIGH, 2: UNUSED, 3: USED, 4: LOW_ONLYSTOP, 5: HIGH_ONLYSTOP)
	 * Positive/Negative Level을 4: LOW_ONLYSTOP, 5: HIGH_ONLYSTOP으로 설정할 경우 Limit 신호에 도달해도 별도의 Limit 신호가 ON 되지 않으며, 설정한 Stop Mode 로 정지
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetLimit(int nAxisNo, uint uStopMode, uint uPositiveLevel, uint uNegativeLevel);
    
	/**
     * @brief 지정 축의 end limit sensor의 사용 유무 및 신호의 입력 레벨 확인
     * 
     * @param nAxisNo 축 번호
     * @param uStopMode 리미트 센서 검출 후 정지 모드 값 저장 (0: EMERGENCY_STOP, 1: SLOWDOWN_STOP)
     * @param upPositiveLevel (+) 리미트 센서 Active Level 저장 (0: LOW, 1: HIGH, 2: UNUSED, 3: USED)
     * @param upNegativeLevel (-) 리미트 센서 Active Level 저장 (0: LOW, 1: HIGH, 2: UNUSED, 3: USED)
     *
	 * @details
	 * PCIe-RxxIF-ECAT 제품일 경우
     * @param upPositiveLevel (+) 리미트 센서 Active Level (0: LOW, 1: HIGH, 2: UNUSED, 3: USED, 4: LOW_ONLYSTOP, 5: HIGH_ONLYSTOP)
     * @param upNegativeLevel (-) 리미트 센서 Active Level (0: LOW, 1: HIGH, 2: UNUSED, 3: USED, 4: LOW_ONLYSTOP, 5: HIGH_ONLYSTOP)
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetLimit(int nAxisNo, ref uint upStopMode, ref uint upPositiveLevel, ref uint upNegativeLevel);
    
	/**
     * @brief 지정축의 limit sensor의 입력 상태 반환
     * 
     * @param nAxisNo 축 번호
     * @param upPositiveStatus (+) 리미트 센서 신호 입력 상태 값 저장 (0: 비활성화, 1: 활성화)
     * @param upNegativeStatus (-) 리미트 센서 신호 입력 상태 값 저장 (0: 비활성화, 1: 활성화)
     *
	 * @details
	 * PCIe-RxxIF-ECAT 제품의 경우
	 * AxmSignalSetLimit 함수에서 Positive/Negative Level을 4: LOW_ONLYSTOP, 5: HIGH_ONLYSTOP으로 설정했을 경우 실제 Limit 신호가 감지되어도 Positive/Negative Status가 0: 비활성화로 반환
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalReadLimit(int nAxisNo, ref uint upPositiveStatus, ref uint upNegativeStatus);

    /**
     * @brief 지정 축의 Software limit의 사용 유무, 사용할 카운트,  정지 방법 설정
     * 
     * @param nAxisNo 축 번호
     * @param uUse Soft-limit 사용 여부 (0: 미사용, 1: 사용)
     * @param uStopMode 정지 방법 (0: 급 정지, 1: 감속 정지)
     * @param uSelection 현재 위치 입력 소스 선택 (0: COMMAND, 1: ACTUAL)
     * @param dPositivePos 지정 축의 (+) 방향 Soft-limit 위치 값
     * @param dNegativePos 지정 축의 (-) 방향 Soft-limit 위치 값
     *
	 * @note
	 * 주의 사항: 원점 검색 시 위 함수를 이용하여 소프트웨어 리밋을 미리 설정해서 구동 시 원점 검색을 도중에 멈춰졌을 경우에도 Enable 됨
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetSoftLimit(int nAxisNo, uint uUse, uint uStopMode, uint uSelection, double dPositivePos, double dNegativePos);
    
	/**
     * @brief 지정 축의 Software limit의 사용 유무, 사용할 카운트,  정지 방법 설정
     * 
     * @param nAxisNo 축 번호
     * @param upUse Soft-limit 사용 여부 저장 (0: 미사용, 1: 사용)
     * @param upStopMode 정지 방법 저장 (0: 급 정지, 1: 감속 정지)
     * @param upSelection 현재 위치 입력 소스 저장 (0: COMMAND, 1: ACTUAL)
     * @param dpPositivePos 지정 축의 (+) 방향 Soft-limit 위치 값 저장
     * @param dpNegativePos 지정 축의 (-) 방향 Soft-limit 위치 값 저장
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetSoftLimit(int nAxisNo, ref uint upUse, ref uint upStopMode, ref uint upSelection, ref double dpPositivePos, ref double dpNegativePos);
    
	/**
     * @brief 지정 축의 Software limit의 현재 상태 반환
     * 
     * @param nAxisNo 축 번호
     * @param upPositiveStatus Soft-limit (+) 상태 저장 (0: Off, 1: On)
     * @param upNegativeStatus Soft-limit (-) 상태 저장 (0: Off, 1: On)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalReadSoftLimit(int nAxisNo, ref uint upPositiveStatus, ref uint upNegativeStatus);

    /**
     * @brief 비상 정지 신호의 정지 방법 (급정지/감속정지) 또는 사용 유무 설정
     * 
     * @param nAxisNo 축 번호
     * @param uStopMode 정지 방법 (0: 급 정지, 1: 감속 정지)
     * @param uLevel 사용 유무 설정 (0: LOW, 1: HIGH, 2: UNUSED, 3: USED)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetStop(int nAxisNo, uint uStopMode, uint uLevel);
    
	/**
     * @brief 비상 정지 신호의 정지 방법 (급정지/감속정지) 또는 사용 유무 확인
     * 
     * @param nAxisNo 축 번호
     * @param upStopMode 정지 방법 저장 (0: 급 정지, 1: 감속 정지)
     * @param upLevel 사용 유무 저장 (0: LOW, 1: HIGH, 2: UNUSED, 3: USED)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetStop(int nAxisNo, ref uint upStopMode, ref uint upLevel);
    
	/**
     * @brief 비상 정지 신호의 입력 상태 반환
     * 
     * @param nAxisNo 축 번호
     * @param upStatus 입력 상태(0: 정지 신호 OFF, 1: 정지 신호 ON)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */       
    [DllImport("AXL.dll")] public static extern uint AxmSignalReadStop(int nAxisNo, ref uint upStatus);

    /**
     * @brief 지정 축의 Servo-On 신호 출력
     * 
     * @param nAxisNo 축 번호
     * @param uOnOff Servo-On 신호 (0: OFF, 1: ON)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalServoOn(int nAxisNo, uint uOnOff);
    
	/**
     * @brief 지정 축의 Servo-On 신호 확인
     * 
     * @param nAxisNo 축 번호
     * @param upOnOff Servo-On 신호 저장 (0: OFF, 1: ON)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalIsServoOn(int nAxisNo, ref uint upOnOff);

    /**
     * @brief 지정 축의 Servo-Alarm Reset 신호 출력
     * 
     * @param nAxisNo 축 번호
     * @param uOnOff Servo-Alarm Reset 신호 (0: OFF, 1: ON)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalServoAlarmReset(int nAxisNo, uint uOnOff);

    /**
     * @brief 범용 출력값 설정
     * 
     * @param nAxisNo 축 번호
     * @param uValue 범용 출력 Hex 값 (0 bit: 서보 On/Off, 1: 알람 제거 신호, 2~5 bit: 범용 출력)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalWriteOutput(int nAxisNo, uint uValue);
    
	/**
     * @brief 범용 출력값 확인
     * 
     * @param nAxisNo 축 번호
     * @param upValue 범용 출력 Hex 값 저장 (0 bit: 서보 On/Off, 1: 알람 제거 신호, 2~5 bit: 범용 출력)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalReadOutput(int nAxisNo, ref uint upValue);
    
    /**
     * @brief 지정 축의 Brake sensor 상태 반환
     *
     * @param nAxisNo 축 번호
     * @param upOnOff 브레이크 센서의 상태 값
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalReadBrakeOn(int nAxisNo, ref uint upOnOff);

    /**
     * @brief 범용 출력값 비트 별 설정
     * 
     * @param nAxisNo 축 번호
     * @param nBitNo 비트 번호 (0~4)
     * @param uOnOff 빌트 별 출력 값 (0: OFF, 1: ON)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalWriteOutputBit(int nAxisNo, int nBitNo, uint uOnOff);
    
	/**
     * @brief 지정 축의 범용 출력 중 지정한 비트의 On/Off 상태 반환
     * 
     * @param nAxisNo 축 번호
     * @param nBitNo 비트 번호 (0~4)
     * @param uOnOff 빌트 별 출력 값 저장 (0: OFF, 1: ON)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalReadOutputBit(int nAxisNo, int nBitNo, ref uint uOnOff);

    /**
     * @brief 범용 입력값 Hex값으로 반환
     * 
     * @param nAxisNo 축 번호
     * @param upValue 범용 입력 값
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalReadInput(int nAxisNo, ref uint upValue);

    /**
     * @brief 범용 입력값 비트 별 반환
     * 
     * @param nAxisNo 축 번호
     * @param nBitNo 비트 번호 (0~4)
     * @param upOn 입력 비트의 On/Off 상태 (0: OFF, 1: ON)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalReadInputBit(int nAxisNo, int nBitNo, ref uint upOn);

    /**
     * @brief 입력 신호들의 디지털 필터 계수를 설정
     *
     * @param nAxisNo 축 번호
     * @param uSignal 입력 신호 옵션 (0: END_LIMIT, 1: INP_ALARM, 2: UIN_00_01, 3: UIN_02_04)
     * @param dBandwidthUsec 디지털 필터 계수 값 입력 (0.2 ~ 26666 uSec)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetFilterBandwidth(int nAxisNo, uint uSignal, double dBandwidthUsec);
    
    /**
     * @brief 범용 입력의 비트 수 반환
     *
     * @param nAxisNo 축 번호
     * @param upInputCount 범용 입력의 비트 수 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetInputBitCount(int nAxisNo, ref uint upInputCount);
    
    /**
     * @brief 범용 출력의 비트 수 반환
     *
     * @param nAxisNo 축 번호
     * @param upOutputCount 범용 출력의 비트 수 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetOutputBitCount(int nAxisNo, ref uint upOutputCount);

    // 모션 구동 중 및 구동 후에 상태 확인 함수
    
	/**
     * @brief 지정 축의 펄스 출력 상태 반환
     *
     * @param nAxisNo 축 번호
     * @param upStatus 출력 상태 (0: 모션 미구동, 1: 모션 구동 중)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadInMotion(int nAxisNo, ref uint upStatus);

    /**
     * @brief 구동 시작 이후 지정 축의 구동 펄스 카운터 값 반환
     *
     * @param nAxisNo 축 번호
     * @param npPulse 구동한 펄스 카운트 값 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadDrivePulseCount(int nAxisNo, ref int npPulse);

    /**
     * @brief 지정 축의 DriveStatus(모션 중 상태) 레지스터 값 반환
     *
     * @param nAxisNo 축 번호
     * @param upStatus 축의 모션 구동 상태 값 저장
	 *
	 * @note
	 * 주의 사항: 각 제품 별로 하드웨어적인 신호가 다르기 때문에 매뉴얼 및 AXHS.xxx 파일 참고
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadMotion(int nAxisNo, ref uint upStatus);

    /**
     * @brief 지정 축의 EndStatus(정지 상태) 레지스터 반환
     *
     * @param nAxisNo 축 번호
     * @param upStatus 정지 상태 레지스터 값 저장
	 *
 	 * @note
	 * 주의 사항: 각 제품 별로 하드웨어적인 신호가 다르기 때문에 매뉴얼 및 AXHS.xxx 파일 참고
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadStop(int nAxisNo, ref uint upStatus);

    /**
     * @brief 지정 축의 Mechanical Signal Data(현재 기계적인 신호 상태) 반환
     *
     * @param nAxisNo 축 번호
     * @param upStatus Mechanical Signal Data 값 저장
	 *
 	 * @note
	 * 주의 사항: 각 제품 별로 하드웨어적인 신호가 다르기 때문에 매뉴얼 및 AXHS.xxx 파일 참고
	 *
	 * @datails
	 * PCIe-RxxIF-ECAT일 경우 
	 * AxmSignalSetLimit 함수에서 2: UNUSED, 4: LOW_ONLYSTOP, 5: HIGH_ONLYSTOP 으로 설정했을 경우 실제 신호가 감지되어도 Positive/Negative Bit에 Signal Data가 반환되지 않음
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadMechanical(int nAxisNo, ref uint upStatus);

    /**
     * @brief 지정 축의 현재 구동 속도 반환
     *
     * @param nAxisNo 축 번호
     * @param dpVel 현재 구동 속도 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadVel(int nAxisNo, ref double dpVelocity);

    /**
     * @brief 지정 축의 Command Pos과 Actual Pos의 차를 반환
     *
     * @param nAxisNo 축 번호
     * @param dpError 차이 값 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadPosError(int nAxisNo, ref double dpError);

    /**
     * @brief 지정 축의 현재 구동 중인 모션의 시작부터 종료 시점까지의 위치 이동 거리 반환
     *
     * @param nAxisNo 축 번호
     * @param dpUnit 이동 거리 값 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadDriveDistance(int nAxisNo, ref double dpUnit);

    /**
     * @brief 지정 축의 위치 정보 사용 방법 설정
     *
     * @param nAxisNo 축 번호
     * @param uPosType Actual position 과 Command position의 표시 방법
     * @param dPositivePos 위치 정보 최대 표시값 (0 ~ 양수)
     * @param dNegativePos 위치 정보 최소 표시값 (음수 ~ 0)
	 *
	 * @details
	 * uPosType
	 *    [0] POSITION_LIMIT - 기본 동작, 전체 범위 내에서 동작
	 *    [1] POSITION_BOUND - 위치 범위 주기형, dNegativePos ~ dPositivePos 범위로 동작
     *
	 * @note
	 * 주의 사항 (PCI-Nx04 해당)
	 *    BOUND 설정 시 카운트 값이 Max값을 초과 할 때 Min값이 되며 반대로 Min값을 초과 할 때 Max값이 됨
	 *    다시 말해 현재 위치값이 설정한 값 밖에서 카운트 될 때는 위의 Min, Max값이 적용되지 않음
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusSetPosType(int nAxisNo, uint uPosType, double dPositivePos, double dNegativePos);
    
	/**
     * @brief 지정 축의 위치 정보 사용 방법 확인
     *
     * @param nAxisNo 축 번호
     * @param upPosType Actual position 과 Command position의 표시 방법 저장
     * @param dpPositivePos 위치 정보 최대 표시값 저장 (0 ~ 양수)
     * @param dpNegativePos 위치 정보 최소 표시값 저장 (음수 ~ 0)
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusGetPosType(int nAxisNo, ref uint upPosType, ref double dpPositivePos, ref double dpNegativePos);
    
	/**
     * @brief 지정 축의 절대치 엔코더 원점 오프셋 위치 설정
     *
     * @param nAxisNo 축 번호
     * @param dOrgOffsetPos 원점 오프셋 위치 값
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusSetAbsOrgOffset(int nAxisNo, double dOrgOffsetPos);
    
	/**
     * @brief 지정 축의 절대치 엔코더 원점 오프셋 위치 확인
     *
     * @param nAxisNo 축 번호
     * @param dpOrgOffsetPos 원점 오프셋 위치 값 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusGetAbsOrgOffset(int nAxisNo, ref double dpOrgOffsetPos);

    /**
     * @brief 지정 축의 Actual 위치 설정
     *
     * @param nAxisNo 축 번호
     * @param dPos Actual 위치 값
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusSetActPos(int nAxisNo, double dPos);
    
	/**
     * @brief 지정 축의 Actual 위치 확인
     *
     * @param nAxisNo 축 번호
     * @param dpPos Actual 위치 값 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusGetActPos(int nAxisNo, ref double dpPos);
    
	/**
     * @brief 서보측에서 올라오는 지정 축의 Actual 위치 반환
     *
     * @param nAxisNo 축 번호
     * @param dpPos 서보 Actual 위치 값 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusGetAmpActPos(int nAxisNo, ref double dpPos);

    /**
     * @brief 지정 축의 Command 위치 설정
     *
     * @param nAxisNo 축 번호
     * @param dPos Command 위치 값
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusSetCmdPos(int nAxisNo, double dPos);
    
	/**
     * @brief 지정 축의 Command 위치 확인
     *
     * @param nAxisNo 축 번호
     * @param dpPos Command 위치 값 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusGetCmdPos(int nAxisNo, ref double dpPos);
    
	/**
     * @brief 지정 축의 Command 위치와 Actual 위치를 dPos 값으로 일치
     *
     * @param nAxisNo 축 번호
     * @param dPos 일치 시킬 위치 값
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusSetPosMatch(int nAxisNo, double dPos);

    /**
     * @brief 지정 축의 모션 상태(Cmd, Act, Driver Status, Mechanical Signal, Universal Signal)를 한번에 확인 
     *
     * @param nAxisNo 축 번호
     * @param MI MOTION_INFO 구조체의 dwMask 설정으로 모션 상태 정보 지정
	 *
	 * @details
	 * dwMask: 모션 상태 표시 (6bit)
	 * ex) dwMask = 0x1F 설정 시 모든 상태 표시
	 * 사용자가 설정한 Level(In/Out)은 반영되지 않음
	 *    [0] | Command Position Read
	 *    [1] | Actual Position Read
	 *    [2] | Mechanical Signal Read
	 *    [3] | Driver Status Read
	 *    [4] | Universal Signal Input Read
	 *    [5] | Universal Signal Output Read
	 *    [6] | 읽기 설정 Mask
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadMotionInfo(int nAxisNo, ref MOTION_INFO MI);

    // Network 제품 전용함수
    
	/**
     * @brief 지정한 축의 서보팩에 AlarmCode를 읽어오도록 명령
     *
     * @param nAxisNo 축 번호
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusRequestServoAlarm(int nAxisNo);
    
	/**
     * @brief 지정한 축의 서보팩 AlarmCode 확인
     *
     * @param nAxisNo 축 번호
     * @param uReturnMode 함수의 반환 동작조건 설정 (SIIIH(MR-J4-xxB)는 사용하지 않음)
     * @param upAlarmCode 해당 서보팩의 Alarm Code 참조
	 *
	 * @details
	 * MR_J4_xxB 알람 코드
	 *    상위 16Bit: 알람코드 2 digit의 10진수 값
	 *    하위 16Bit: 알람 상세 코드 1 digit 10진수 값
	 * uReturnMode
	 *    [0] Immediate   : 함수 실행 후 바로 반환
	 *    [1] Blocking    : 서보팩으로 부터 알람 코드를 읽을때까지 반환하지 않음
	 *    [2] Non Blocking: 서보팩으로 부터 알람 코드를 읽을때까지 반환하지않으나 프로그램 Blocking 되지 않음
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadServoAlarm(int nAxisNo, uint uReturnMode, ref uint upAlarmCode);
    
	/**
     * @brief 지정한 에러 코드에 해당하는 Alarm String을 받아오는 함수
     *
     * @param nAxisNo 축 번호
     * @param uAlarmCode 에러 코드
     * @param nAlarmStringSize Alarm String 문자열 크기
     * @param szAlarmString Alarm 문자열
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusGetServoAlarmString(int nAxisNo, uint uAlarmCode, int nAlarmStringSize, byte[] szAlarmString);
    
	/**
     * @brief 지정한 축에서 현재 표시 중인 Alarm String을 받아오는 함수
     *
     * @param nAxisNo 축 번호
     * @param nAlarmStringSize 알람 문자열의 크기 (표시될 문자열보다 작은 공간일 경우 초과하는 부분은 자동으로 생략)
     * @param szAlarmString 알람 문자열
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadServoAlarmString(int nAxisNo, int nAlarmStringSize, char[] szAlarmString);

    /**
     * @brief 지정한 축의 서보팩에 Alarm History를 읽어오도록 명령하는 함수
     *
     * @param nAxisNo 축 번호
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusRequestServoAlarmHistory(int nAxisNo);
	
    /**
     * @brief 지정한 축의 서보팩 Alarm History를 읽어오는 함수
     *
     * @param nAxisNo 축 번호
     * @param uReturnMode 함수의 반환 동작 조건 설정
     * @param npCount 읽은 Alarm History의 개수 
     * @param upAlarmCode Alarm History를 반환할 배열 (MAX: 15개)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadServoAlarmHistory(int nAxisNo, uint uReturnMode, ref int npCount, uint[] upAlarmCode);
    
	/**
     * @brief 지정한 축의 서보팩 Alarm History를 Clear
     *
     * @param nAxisNo 축 번호
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusClearServoAlarmHistory(int nAxisNo);
	
	/**
     * @brief 지정된 축의 남은 큐(Queue) 카운트 반환
     *
     * @param nAxisNo 축 번호
     * @param pdwRemainQueueCount 남은 큐 카운트 저장
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
	[DllImport("AXL.dll")] public static extern uint AxmStatusReadRemainQueueCount(int nAxisNo, ref uint pdwRemainQueueCount);

    // 홈 관련 함수
	
    /**
     * @brief  지정 축의 Home 센서 레벨 설정
     * 
     * @param nAxisNo 축 번호
     * @param uLevel 센서 레벨 값 (0: LOW, 1: HIGH)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeSetSignalLevel(int nAxisNo, uint uLevel);
    
	/**
     * @brief  지정 축의 Home 센서 레벨 확인
     * 
     * @param nAxisNo 축 번호
     * @param upLevel 센서 레벨 값 저장 (0: LOW, 1: HIGH)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeGetSignalLevel(int nAxisNo, ref uint upLevel);
    
	/**
     * @brief  현재 홈 신호 입력 상태 반환
     * 
     * @param nAxisNo 축 번호
     * @param upStatus 신호 입력 상태 (0: 비활성화, 1: 활성화)
     *
	 * @note
	 * 홈 신호는 사용자가 임의로 AxmHomeSetMethod 함수를 이용하여 설정할 수 있음
	 * 일반적으로 홈 신호는 범용 입력 0를 사용하고 있지만 AxmHomeSetMethod 이용해서 바꾸면 +/- Limit을 사용할수도 있다
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeReadSignal(int nAxisNo, ref uint upStatus);

    /**
     * @brief 원점 검색 관련 파라미터(검색 진행 방향, 원점으로 사용할 신호, 원점 센서 Active Level, 엔코더 Z 상 검출) 설정
     * 
     * @param nAxisNo 축 번호
     * @param nHmDir 홈 방향 ([0] DIR_CCW: -방향, [1] DIR_CW: +방향)
     * @param uHomeSignal 원점으로 사용할 신호
     * @param uZphas 1차 원점 검색 완료 후 엔코더 Z상 검출 유무 설정
     * @param dHomeClrTime 원점 검색 Encoder 값 Set하기 위한 대기 시간
     * @param dHomeOffset 원점 검출 후 이동 거리
     *
	 * @details
	 * uHomeSignal
	 *    [0] PosEndLimit: +Limit
	 *    [1] NegEndLimit: -Limit
	 *    [4] HomeSensor: 원점 센서 (범용 입력: 0)
	 * uZphas
	 *    0: 사용 안함
	 *    1: HmDir과 반대 방향
	 *    2: HmDir과 같은 방향
	 *
	 * @note
	 * 해당 축의 원점 검색을 수행하기 위해서는 반드시 원점 검색 관련 파라메타들이 설정되어 있어야 함
	 * 만약 Mot 설정 파일을 이용해 초기화가 정상적으로 수행됐다면 별도의 설정은 필요하지 않음
	 * 주의 사항: 레벨을 잘못 설정 시 -방향으로 설정해도 +방향으로 동작할수 있으며, 홈을 찾는데 있어 문제가 될수 있음
	 * 자세한 내용은 AxmMotSaveParaAll 함수 및 매뉴얼 참조
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeSetMethod(int nAxisNo, int nHmDir, uint uHomeSignal, uint uZphas, double dHomeClrTime, double dHomeOffset);
    
	/**
     * @brief 원점 검색 관련 파라미터(검색 진행 방향, 원점으로 사용할 신호, 원점 센서 Active Level, 엔코더 Z 상 검출) 설정 값 확인
     * 
     * @param nAxisNo 축 번호
     * @param lHmDir 홈 방향 저장 ([0] DIR_CCW: -방향, [1] DIR_CW: +방향)
     * @param uHomeSignal 원점으로 사용 신호 저장
     * @param uZphas 1차 원점 검색 완료 후 엔코더 Z상 검출 유무 저장
     * @param dHomeClrTime 원점 검색 Encoder 값 Set하기 위한 대기 시간 저장
     * @param dHomeOffset 원점 검출 후 이동 거리 저장
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeGetMethod(int nAxisNo, ref int nHmDir, ref uint uHomeSignal, ref uint uZphas, ref double dHomeClrTime, ref double dHomeOffset);

    /**
     * @brief 원점 검색 방법의 미세 조정 수행 (기본적으로 설정하지 않아도 됨)
     *
     * @param nAxisNo 축 번호
     * @param dHomeDogLength 첫번째 Step에서 HomeDog가 센서를 지나쳤는지 확인하기위한 Dog 길이 입력 (Default: 500 pulse)
     * @param uLevelScanTime 2번째 Step(원점센서를 빠져나가는 동작)에서 Level상태를 확인할 Scan 시간 설정 (msec, 범위: 1~1000, Default: 100 msec)
     * @param uFineSearchUse 기본 원점 검색 시 5 Step를 사용하는데 3 Step만 사용하도록 변경할 때 0으로 설정 (Default: USE)
     * @param uHomeClrUse 원점 검색 후 지령값과 Encoder값을 0으로 자동 설정 여부 설정 (Default: USE)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeSetFineAdjust(int nAxisNo, double dHomeDogLength, uint uLevelScanTime, uint uFineSearchUse, uint uHomeClrUse);
    
	/**
     * @brief 원점 검색 방법의 미세 조정 값 확인
     *
     * @param nAxisNo 축 번호
     * @param dHomeDogLength Dog 입력 길이 저장 (Default: 500 pulse)
     * @param upLevelScanTime Scan 시간 저장 (msec, 범위: 1~1000, Default: 100 msec)
     * @param upFineSearchUse Step 변경 여부 설정 값 저장 (Default: USE)
     * @param upHomeClrUse 원점 검색 후 지령값과 Encoder값을 0으로 자동 설정 여부 값 저장 (Default: USE)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeGetFineAdjust(int nAxisNo, ref double dpHomeDogLength, ref uint upLevelScanTime, ref uint upFineSearchUse, ref uint upHomeClrUse);

    /**
     * @brief 원점 검색 시 Interlock 설정 함수 (기본적으로 설정하지 않아도 됨)
     *
     * @param nAxisNo 축 번호
     * @param uInterlockMode Interlock 설정 모드
     * @param dInterlockData Interlock Mode 설정 값
	 *
	 * @details
	 * uInterlockMode
	 *    [0] HOME_INTERLOCK_UNUSED          : Home Interlock 사용하지 않음
     *    [1] HOME_INTERLOCK_SENSOR_CHECK    : 원점 검색 진행 방향에 설치된 리미트 센서가 감지 되었을 때 원점 센서가 같이 감지되지 않은 경우 INTERLOCK 에러 발생
	 *    [2] HOME_INTERLOCK_DISTANCE        : 원점 검색 진행 방향에 설치된 리미트 센서가 감지 된 후 원점 센서까지의 거리가 지정한 거리보다 클 경우 INTERLOCK 에러 발생
     * dInterlockData
	 *    [0] HOME_INTERLOCK_UNUSED          : 사용안함
	 *    [1] HOME_INTERLOCK_SENSOR_CHECK    : 사용안함
	 *    [2] HOME_INTERLOCK_DISTANCE        : 원점검색 진행 방향에 설치된 리미트와 원점 센서까지의 거리(실제 거리보다 약간 크게 설정 함)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeSetInterlock(int nAxisNo, uint uInterlockMode, double dInterlockData);
    
	/**
     * @brief 원점 검색 시 Interlock 설정 확인
     *
     * @param nAxisNo 축 번호
     * @param upInterlockMode Interlock 설정 모드 저장
     * @param dpInterlockData Interlock Mode 설정 값 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeGetInterlock(int nAxisNo, ref uint upInterlockMode, ref double dpInterlockData);

    /**
     * @brief  원점검색 시, 각 스탭에 사용 될 속도 설정
     * 
     * @param nAxisNo 축 번호
     * @param dVelFirst 1차 구동 속도
     * @param dVelSecond 검출 후 속도
     * @param dVelThird 마지막 속도
     * @param dVelLast 1차 원점 센서 검출 시 사용될 구동 속도
     * @param dAccFirst 1차 구동 가속도
     * @param dAccSecond 검출 후 가속도
     *
	 * @note
	 * 원점을 빠르고 정밀하게 검색하기 위해 여러 단계의 스탭으로 검출 함. 이때 각 스탭에 사용 될 속도 설정.
	 * 이 속도들의 설정 값에 따라 원점 검색 시간과 원점 검색 정밀도가 결정 됨.
	 * 각 스탭 별 속도들을 적절히 바꿔가면서 각 축의 원점 검색 속도를 설정하면 됨.
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeSetVel(int nAxisNo, double dVelFirst, double dVelSecond, double dVelThird, double dVelLast, double dAccFirst, double dAccSecond);
    
	/**
     * @brief  원점검색 시, 각 스탭에 사용 될 속도 확인
     * 
     * @param nAxisNo 축 번호
     * @param dpVelFirst 1차 구동 속도 저장
     * @param dpVelSecond 검출 후 속도 저장
     * @param dpVelThird 마지막 속도 저장
     * @param dpVelLast 1차 원점 센서 검출 시 사용될 구동 속도 저장
     * @param dpAccFirst 1차 구동 가속도 저장
     * @param dpAccSecond 검출 후 가속도 저장
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeGetVel(int nAxisNo, ref double dpVelFirst, ref double dpVelSecond, ref double dpVelThird, ref double dpVelLast, ref double dpAccFirst, ref double dpAccSecond);

    /**
     * @brief 원점 검색 시작
     * 
     * @param nAxisNo 축 번호
	 *
	 * @note
	 * 원점 검색 시작 함수를 실행하면 라이브러리 내부에서 해당 축의 원점 검색을 수행 할 쓰레드가 자동 생성되어 원점 검색을 순차적으로 수행한 후 자동 종료.
     * 주의 사항: 진행 방향과 반대 방향의 리미트 센서가 들어와도 진행방향의 센서가 ACTIVE 되지 않으면 동작 함.
	 *        원점 검색이 시작되어 진행 방향이 리밋트 센서가 들어오면 리밋트 센서가 감지되었다고 생각하고 다음 단계로 진행.
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeSetStart(int nAxisNo);
    
	/**
     * @brief 원점검색 결과를 사용자가 임의 설정
     * 
     * @param nAxisNo 축 번호
     * @param uHomeResult 원점 검색 결과
	 *
	 * @details
	 * uHomeResult
     *    HOME_SUCCESS          = 0x01    // 홈 완료
     *    HOME_SEARCHING        = 0x02    // 홈 검색중
     *    HOME_ERR_GNT_RANGE    = 0x10    // 홈 검색 범위를 벗어났을 경우
     *    HOME_ERR_USER_BREAK   = 0x11    // 속도 유저가 임의로 정지 명령을 내렸을 경우
     *    HOME_ERR_VELOCITY     = 0x12    // 속도 설정 잘못했을 경우
     *    HOME_ERR_AMP_FAULT    = 0x13    // 서보팩 알람 발생 에러
     *    HOME_ERR_NEG_LIMIT    = 0x14    // (-)방향 구동중 (+)리미트 센서 감지 에러    
     *    HOME_ERR_POS_LIMIT    = 0x15    // (+)방향 구동중 (-)리미트 센서 감지 에러
     *    HOME_ERR_NOT_DETECT   = 0x16    // 지정한 신호 검출하지 못 할 경우 에러
     *    HOME_ERR_UNKNOWN      = 0xFF
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeSetResult(int nAxisNo, uint uHomeResult);
    
	/**
     * @brief 원점검색 결과를 사용자가 임의 설정 확인
     * 
     * @param nAxisNo 축 번호
     * @param upHomeResult 원점 검색 결과 저장
	 *
	 * @note
	 * 원점 검색이 시작되면 HOME_SEARCHING으로 설정되며 원점 검색에 실패하면 실패 원인이 설정 됨. 
	 * 실패 원인을 제거한 후 다시 원점 검색을 진행하면 됨.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeGetResult(int nAxisNo, ref uint upHomeResult);

    /**
     * @brief 원점 검색 진행률 반환
     * 
     * @param nAxisNo 축 번호
     * @param upHomeMainStepNumber Main Step 진행율 저장
     * @param upHomeStepNumber 선택한 축에 대한 진행율 저장
     *
	 * @note
	 * 원점 검색 시작되면 진행율을 확인할 수 있음. 원점 검색이 완료되면 성공 여부와 관계 없이 100을 반환 함.
	 * 원점 검색 성공 여부는 AxmHomeGetResult 함수를 이용해 확인할 수 있음.
	 * upHomeMainStepNumber
	 *    [겐트리 FALSE 일 경우] upHomeMainStepNumber : 0 일때면 선택한 축만 진행사항이고 홈 진행율은 upHomeStepNumber 표시
	 *    [겐트리 TRUE 일 경우]  upHomeMainStepNumber : 0 일때면 마스터 홈을 진행사항이고 마스터 홈 진행율은 upHomeStepNumber 표시
	 *    [겐트리 TRUE 일 경우]  upHomeMainStepNumber : 10 일때면 슬레이브 홈을 진행사항이고 마스터 홈 진행율은 upHomeStepNumber 표시
	 * upHomeStepNumber
	 *    [겐트리 FALSE 일 경우] 선택한 축만 진행율 표시
	 *    [겐트리 TRUE 일 경우]  마스터 축, 슬레이브 축 순서로 진행율 표시
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeGetRate(int nAxisNo, ref uint upHomeMainStepNumber, ref uint upHomeStepNumber);

    // 위치 구동 함수
    
	/**
     * @brief 설정한 거리만큼 또는 위치까지 이동
     *
     * @param nAxisNo 축 번호
     * @param dPos 설정한 거리 또는 위치값
     * @param dVel 미리 세팅한 속도값 (양수: CW, 음수: CCW 방향으로 구동)
     * @param dAccel 가감속도 설정값
     * @param dDecel 가감속도 설정값
	 *
	 * @note
	 * 지정 축의 절대/상대 좌표로 설정 된 위치까지 설정된 속도와 가속율로 구동.
	 * 속도 프로파일은 AxmMotSetProfileMode 함수에서 설정.
	 * 펄스가 출력되는 시점에서 함수를 벗어남.
	 * AxmMotSetAccelUnit(nAxisNo, 1) 일경우 dAccel -> dAccelTime , dDecel -> dDecelTime 으로 바뀜.
	 * 주의 사항: 위치를 설정할 경우 반드시 UNIT/PULSE를 맞추어 설정
	 *        위치를 UNIT/PULSE 보다 작게할 경우 최소 단위가 UNIT/PULSE로 맞추어지기 때문에 그 위치까지 구동이 될수 없음
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveStartPos(int nAxisNo, double dPos, double dVel, double dAccel, double dDecel);

    /**
     * @brief 설정한 거리만큼 또는 위치까지 이동
     *
     * @param nAxisNo 축 번호
     * @param dPos 설정한 거리 또는 위치값
     * @param dVel 미리 세팅한 속도값 (양수: CW, 음수: CCW 방향으로 구동)
     * @param dAccel 가감속도 설정값
     * @param dDecel 가감속도 설정값
     *
	 * @note
	 * 지정 축의 절대/상대 좌표로 설정 된 위치까지 설정된 속도와 가속율로 구동.
	 * 속도 프로파일은 AxmMotSetProfileMode 함수에서 설정.
	 * 펄스가 출력되는 시점에서 함수를 벗어남.
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMovePos(int nAxisNo, double dPos, double dVel, double dAccel, double dDecel);

    /**
     * @brief 설정한 속도 구동
     *
     * @param nAxisNo 축 번호
     * @param dVel 미리 세팅한 속도값 (양수: CW, 음수: CCW 방향으로 구동)
     * @param dAccel 가감속도 설정값
     * @param dDecel 가감속도 설정값
     *
	 * @note
	 * 지정 축에 대하여 설정된 속도와 가속율로 지속적으로 속도 모드 구동
	 * 펄스가 출력되는 시점에서 함수를 벗어남
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveVel(int nAxisNo, double dVel, double dAccel, double dDecel);

    /**
     * @brief 지정된 다축에 대하여 설정된 속도와 가속율 지속적으로 속도 모드 구동
     *
     * @param nArraySize 다축 개수
     * @param npAxesNo 축 번호 배열
     * @param dpVel 축의 속도 배열 (양수: CW, 음수: CCW 방향으로 구동)
     * @param dpAccel 가속율 배열
     * @param dpDecel 감속율 배열
     *
	 * @note
	 * 지정된 다축에 대하여 설정된 속도와 가속율로 지속적으로 속도 모드 구동
	 * 펄스가 출력되는 시점에서 함수를 벗어남
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveStartMultiVel(int nArraySize, int[] npAxesNo, double[] dVel, double[] dAccel, double[] dDecel);

    /**
     * @brief 지정된 다축에 대하여 설정된 속도와 가속율, SyncMode에 따라 지속적으로 속도 모드 구동 
     *
     * @param nArraySize 축 배열의 크기
     * @param npAxesNo 축 번호 배열
     * @param dpVel 속도 배열 (양수: CW, 음수: CCW 방향으로 구동)
     * @param dpAccel 가속도 배열
     * @param dpDecel 감속도 배열
     * @param uSyncMode 동기 정지 기능 사용 여부 (0: 사용 안함, 1: 동기 정지 기능만 사용, 2: 알람에 대해서도 동기 정기 기능 사용)
     *
	 * @note 펄스가 출력되는 시점에서 함수를 벗어남
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveStartMultiVelEx(int nArraySize, int[] npAxesNo, double[] dpVel, double[] dpAccel, double[] dpDecel, uint uSyncMode);

    /**
     * @brief 지정된 다축에 대하여 설정된 속도와 가속율 지속적으로 속도 모드 구동
     *
     * @param nArraySize 다축 개수
     * @param npAxesNo 축 번호 배열
     * @param dpDis 이동 거리 배열
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     *
	 * @note 
	 * 펄스가 출력되는 시점에서 함수를 벗어남
	 * 펄스 출력이 시작되는 시점에서 함수를 벗어나며 Master축은(Distance가 가장 큰) dVel속도로 움직이며, 나머지 축들의 Distance 비율로 움직임
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveStartLineVel(int nArraySize, int[] npAxesNo, double[] dpDis, double dVel, double dAccel, double dDecel);

    /**
     * @brief 지정 축에 설정된 신호 검출
     *
     * @param nAxisNo 축 번호
     * @param dVel 구동 속도 설정 (양수: CW, 음수: CCW 방향으로 구동)
     * @param dAccel 구동 가속도 설정
     * @param dDecel 구동 감속도 설정
     * @param nDetectSignal edge 검출 할 입력 신호 선택
     * @param nSignalEdge 선택한 입력 신호의 edge 방향 선택 (rising or falling edge)
	 * @param nSignalMethod 정지 모드 설정
	 *
	 * @details
	 * nDetectSignal
	 *    PosEndLimit(0), NegEndLimit(1), HomeSensor(4), EncodZPhase(5), UniInput02(6), UniInput03(7)
	 * nSignalEdge
	 *    SIGNAL_DOWN_EDGE(0), SIGNAL_UP_EDGE(1)
	 * nSignalMethod
	 *    급정지 EMERGENCY_STOP(0), 감속정지 SLOWDOWN_STOP(1)
	 *
	 * @note
	 * 주의 사항: SignalMethod를 EMERGENCY_STOP(0)로 사용할경우 가감속이 무시되며 지정된 속도로 가속 급정지하게 됨
	 *         PCI-Nx04: lDetectSignal이 PosEndLimit , NegEndLimit(0,1)을 찾을 경우 신호의 레벨 Active 상태를 검출하게 됨
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveSignalSearch(int nAxisNo, double dVel, double dAccel, int nDetectSignal, int nSignalEdge, int nSignalMethod);

    /**
     * @brief 특정 Input 신호의 Edge를 검출하여 사용자가 지정한 위치 값만큼 이동 (MLIII : Sigma-5/7 전용)
     *
     * @param nAxisNo 축 번호
     * @param dVel 구동 속도 설정 (양수: CW, 음수: CCW 방향으로 구동)
     * @param dAccel 구동 가속도 설정
     * @param dDecel 구동 감속도 설정. 일반적으로 dAccel의 50배로 설정
     * @param nDetectSignal HomeSensor(4)
     * @param dDis 입력 신호의 검출 위치를 기준으로 사용자가 지정한 위치만큼 상대 구동
     *
	 * @note
	 * 주의 사항
	 *    구동 방향과 반대 방향으로 dDis 값 입력 시 역방향으로 구동 될 수 있음.
	 *    속도가 빠르고, dDis 값이 작은 경우 모터가 신호 감지해서 정지한 이후에 최종 위치로 가기 위해서 역방향으로 구동될 수 있음.
	 *    해당 함수를 사용하기 전에 원점 센서는 반드시 LOW 또는 HIGH로 설정 되어 있어야 함.
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveSignalSearchAtDis(int nAxisNo, double dVel, double dAccel, double dDecel, int nDetectSignal, double dDis);

    /**
     * @brief 지정 축에서 설정된 신호를 검출하고 그 위치를 저장하기 위해 이동
     *
     * @param nAxisNo 축 번호
     * @param dVel 구동 속도 (양수: CW, 음수: CCW 방향으로 구동)
     * @param dAccel 가감속도 설정값
     * @param nDetectSignal edge 검출할 입력 신호 선택 (0: SIGNAL_DOWN_EDGE, 1: SIGNAL_UP_EDGE)
     * @param nSignalEdge 선택한 입력 신호의 edge 방향 선택 (0: SIGNAL_DOWN_EDGE, 1: SIGNAL_UP_EDGE)
     * @param nTarget 조건 발생 위치 기준 선택 (0: COMMAND, 1: ACTUAL)
     * @param nSignalMethod 정지 모드 (0: 급정지 EMERGENCY_STOP, 1: 감속정지 SLOWDOWN_STOP)
     *
	 * @details
	 * nDetectSignal: 상위 8bit에 대하여 기본 구동(0), Software 구동(1) 을 선택할 수 있다. SMP Board(PCIe-Rxx05-MLIII) 전용
	 *
	 * @note
	 * 주의 사항: SignalMethod를 EMERGENCY_STOP(0)로 사용할경우 가감속이 무시되며 지정된 속도로 가속 급정지하게 됨
	 *         PCI-Nx04: lDetectSignal이 PosEndLimit , NegEndLimit(0,1)을 찾을 경우 신호의 레벨 Active 상태를 검출하게 됨
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveSignalCapture(int nAxisNo, double dVel, double dAccel, int nDetectSignal, int nSignalEdge, int nTarget, int nSignalMethod);
    
	/**
     * @brief 지정 축에서 설정된 신호를 검출하고 그 위치값 확인
     *
     * @param nAxisNo 축 번호
     * @param dpCapPosition 위치 값 저장
	 *
	 * @note
	 * 주의 사항: 함수 실행 결과가 "AXT_RT_SUCCESS"일 때 저장된 위치가 유효하며, 이 함수를 한번 실행하면 저장 위치값이 초기화 됨
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveGetCapturePos(int nAxisNo, ref double dpCapPosition);

    /**
     * @brief 설정한 거리만큼 또는 위치까지 이동
     *
     * @param nArraySize 이동할 축의 개수
     * @param nAxisNo   이동할 축의 축 번호 배열
     * @param dpPos      이동할 거리나 위치값 배열
     * @param dpVel      설정할 이동 속도값 배열
     * @param dpAccel    설정할 가속도 배열
     * @param dpDecel    설정할 감속도 배열
	 *
     * @note
	 * 함수를 실행하면 해당 Motion 동작을 시작한 후 Motion 이 완료 될때까지 기다리지 않고 바로 함수를 빠져나감
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
    */
    [DllImport("AXL.dll")] public static extern uint AxmMoveStartMultiPos(int nArraySize, int[] nAxisNo, double[] dPos, double[] dVel, double[] dAccel, double[] dDecel);

    /**
     * @brief 다축을 설정한 거리만큼 또는 위치까지 이동
     *
     * @param nArraySize 이동할 축의 개수
     * @param nAxisNo   이동할 축의 축 번호 배열
     * @param dpPos      이동할 거리나 위치값 배열
     * @param dpVel      설정할 이동 속도값 배열
     * @param dpAccel    설정할 가속도 배열
     * @param dpDecel    설정할 감속도 배열
     *
     * @note
	 * 지정 축들의 절대 좌표로 설정된 위치까지 설정된 속도와 가속율로 구동
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveMultiPos(int nArraySize, int[] nAxisNo, double[] dPos, double[] dVel, double[] dAccel, double[] dDecel);

    /**
     * @brief 설정한 토크 및 속도 값으로 모터 구동 (PCI-R1604-MLII/SIIIH, PCIe-Rxx04-SIIIH  전용 함수)
     *
     * @param nAxisNo        구동할 축의 축 번호
     * @param dTorque        최대 출력 토크에 대한 %값 (양수: CW, 음수: CCW 방향으로 구동)
     * @param dVel           최대 모터 구동 속도에 대한 %값
     * @param uAccFilterSel 가속도 필터링 방식 선택 (0: LINEAR_ACCDCEL, 1: EXPO_ACCELDCEL, 2: SCURVE_ACCELDECEL)
     * @param uGainSel      Gain 선택 (0: GAIN_1ST, 1: GAIN_2ND)
     * @param uSpdLoopSel   속도 제어 방식 선택 (0: PI_LOOP, 1: P_LOOP)
     *
	 * @details
	 * PCIe-Rxx05-MLIII 제품일 경우
	 *    dVel: 구동 속도 (단위: pps)
	 *    dwAccFilterSel, dwGainSel, dwSpdLoopSel: 사용하지 않음
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveStartTorque(int nAxisNo, double dTorque, double dVel, uint uAccFilterSel, uint uGainSel, uint uSpdLoopSel);

    /**
     * @brief 지정 축의 토크 구동 정지
     *
     * @param nAxisNo 구동 정지할 축 번호
     * @param uMethod 정지 방식 선택
	 *
	 * @details
	 * dwMethod
	 *    [00h] 1ST 또는 2ND의 일정한 리니어 감속률에 따른 정지
	 *    [01h] 즉시 정지
	 *    [02h] 정지를 위한 일정한 리니어 감속률에 따른 정지
	 *
	 * @note AxmMoveStartTorque 후 반드시 AxmMoveTorqueStop를 실행해야 함
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveTorqueStop(int nAxisNo, uint uMethod);

    /**
     * @brief 설정한 거리나 위치까지 이동
     *
     * @param nAxisNo 축 번호
     * @param dPosition 위치 값
     * @param dpVel 속도 배열
     * @param dpAccel 가속도 배열
     * @param dpDecel 감속도 배열
     * @param nListNum 리스트 번호
	 *
	 * @note
	 * 지정 축의 절대/상대 좌표로 설정된 위치까지 설정된 속도/가속율로 구동
	 * 속도 프로파일은 비대칭 사다리꼴로 고정
	 * 펄스 출력이 종료되는 시점에서 함수를 벗어남
	 * 가감속도 설정 단위는 기울기로 고정
	 * dAccel != 0.0 이고 dDecel == 0.0 일 경우 이전 속도에서 감속 없이 지정 속도까지 가속
	 * dAccel != 0.0 이고 dDecel != 0.0 일 경우 이전 속도에서 지정 속도까지 가속후 등속 이후 감속
	 * dAccel == 0.0 이고 dDecel != 0.0 일 경우 이전 속도에서 다음 속도까지 감속
	 * 다음의 조건을 만족하여야 함
	 *    dVel[1] == dVel[3]을 반드시 만족해야 함
	 *    dVel[2]로 정속 구동 구간이 발생할 수 있도록 dPosition이 충분히 큰값이어야 함
	 * Ex) dPosition = 10000
	 *    dVel[0] = 300., dAccel[0] = 200., dDecel[0] = 0.;    <== 가속
	 *    dVel[1] = 500., dAccel[1] = 100., dDecel[1] = 0.;    <== 가속
	 *    dVel[2] = 700., dAccel[2] = 200., dDecel[2] = 250.;  <== 가속, 등속, 감속
	 *    dVel[3] = 500., dAccel[3] = 0.,   dDecel[3] = 150.;  <== 감속
	 *    dVel[4] = 200., dAccel[4] = 0.,   dDecel[4] = 350.;  <== 감속
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveStartPosWithList(int nAxisNo, double dPosition, double[] dpVel, double[] dpAccel, double[] dpDecel, int nListNum);

    /**
     * @brief 지정한 위치까지 대상 축의 위치가 증감할 때 이동 시작
     *
     * @param nAxisNo          이동할 축 번호
     * @param dPos             이동할 거리나 위치 값
     * @param dVel             설정할 이동 속도 값
     * @param dAccel           설정할 가속도 값
     * @param dDecel           설정할 감속도 값
     * @param nEventAxisNo     시작 조건 발생 축
     * @param dComparePosition 시작 조건 발생 축의 조건 발생 위치
     * @param uPositionSource  시작 조건 발생 축의 조건 발생 위치 기준 선택 (0: COMMAND, 1: ACTUAL)
     *
	 * @note
	 * 예약 후 취소는 AxmMoveStop, AxmMoveEStop, AxmMoveSStop 사용
	 * 이동 축과 시작 조건 발생 축은 4축 단위 하나의 그룹(2V04의 경우 같은 모듈)에 존재해야 합
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveStartPosWithPosEvent(int nAxisNo, double dPos, double dVel, double dAccel, double dDecel, int nEventAxisNo, double dComparePosition, uint uPositionSource);

    /**
     * @brief 지정 축 지정 감속도로 감속 정지
     *
     * @param nAxisNo 정지할 축 번호
     * @param dDecel  정지 시 감속율 값
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveStop(int nAxisNo, double dDecel);
    
	/**
     * @brief 지정 축 지정 감속도로 감속 정지 (PCI-Nx04 제품 전용)
     *
     * @param nAxisNo 정지할 축 번호
     * @param dDecel  정지 시 감속율 값
	 *
	 * @note
	 * 현재 가감속 상태와 관계없이 즉시 감속 가능 함수이며 제한된 구동에 대하여 사용 가능
	 * 사용 가능 구동: AxmMoveStartPos, AxmMoveVel, AxmLineMoveEx2
	 * 주의 사항
	 *    감속율 값은 최초 설정 감속율보다 크거나 같아야 함
	 *    감속 설정을 시간으로 하였을 경우 최초 설정 감속 시간보다 작거나 같아야 함
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveStopEx(int nAxisNo, double dDecel);
    
	/**
     * @brief 지정 축 급 정지
     *
     * @param nAxisNo 정지할 축 번호
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveEStop(int nAxisNo);
    
	/**
     * @brief 지정 축 감속 정지
     *
     * @param nAxisNo 정지할 축 번호
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveSStop(int nAxisNo);

    // 오버라이드 함수
	
    /**
     * @brief 위치 오버라이드
     *
     * @param nAxisNo 축 번호
     * @param dOverridePos 오버라이드 할 위치
     *
	 * @note
	 * 지정 축의 구동이 종료되기 전 지정된 출력 펄스 수 조정
	 * PCI-Nx04 제품 사용 시 주의 사항
	 *    오버라이드할 위치를 넣을 때는 구동 시점의 위치를 기준으로 한 Relative 형태의 위치 값으로 넣어줘야 함
	 *    구동 시작 후 같은 방향의 경우 오버라이드를 계속 할 수 있지만 반대 방향으로 오버라이드 할 경우에는 오버라이드를 계속 할 수 없음
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmOverridePos(int nAxisNo, double dOverridePos);

    /**
     * @brief 지정 축의 속도 오버라이드 전 최고 속도 설정
     *
     * @param nAxisNo 축 번호
     * @param dOverrideMaxVel 최대 속도 값
	 *
	 * @note 주의 사항: 속도 오버라이드를 5번 한다면 그 중에 최고 속도를 설정해야 함
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmOverrideSetMaxVel(int nAxisNo, double dOverrideMaxVel);
    
	/**
     * @brief 속도 오버라이드 
     *
     * @param nAxisNo 축 번호
     * @param dOverrideVel 오버라이드 할 속도 값
     *
	 * @note
	 * 지정 축의 구동 중에 속도를 가변 설정 (반드시 모션 중에 가변 설정)
	 * 주의 사항: AxmOverrideVel 함수를 사용하기 전 AxmOverrideMaxVel로 최고로 설정할 수 있는 속도를 설정
	 * Ex) 속도 오버라이드를 두 번 한다면
	 *     1. 두개 중에 높은 속도를 AxmOverrideMaxVel 설정 최고 속도값 설정
	 *     2. AxmMoveStartPos 실행 지정 축의 구동 중(Move 함수 모두 포함)에 속도를 첫 번째 속도로 AxmOverrideVel 가변 설정
	 *     3. 지정 축의 구동 중(Move 함수 모두 포함)에 속도를 두 번째 속도로 AxmOverrideVel 가변 설정
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmOverrideVel(int nAxisNo, double dOverrideVel);
    
	/**
     * @brief 가속도, 속도, 감속도를 오버라이드
     *
     * @param nAxisNo 축 번호
     * @param dOverrideVelocity 가변 속도(속도 오버라이드 값)
     * @param dMaxAccel 최대 가속도 값
     * @param dMaxDecel 최대 감속도 값
	 *
	 * @note
	 * 지정 축의 구동 중에 속도를 가변 설정 (반드시 모션 중에 가변 설정)
	 * 주의 사항: AxmOverrideAccelVelDecel 함수를 사용하기 전 AxmOverrideMaxVel 최고로 설정할 수 있는 속도를 설정
	 * Ex) 속도 오버라이드를 두 번 한다면
	 *     1. 두개 중에 높은 속도를 AxmOverrideMaxVel 설정 최고 속도값 설정
	 *     2. AxmMoveStartPos 실행 지정 축의 구동 중(Move 함수 모두 포함)에 속도를 첫 번째 속도로 AxmOverrideAccelVelDecel 가변 설정
	 *     3. 지정 축의 구동 중(Move 함수 모두 포함)에 속도를 두 번째 속도로 AxmOverrideAccelVelDecel 가변 설정
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmOverrideAccelVelDecel(int nAxisNo, double dOverrideVelocity, double dMaxAccel, double dMaxDecel);
    
	/**
     * @brief 속도 오버라이드를 지정 위치에서 수행
     *
     * @param nAxisNo 축 번호
     * @param dPos 구동 거리
     * @param dVel 구동 속도
     * @param dAccel 가속도 값
     * @param dDecel 감속도 값
     * @param dOverridePos 속도 오버라이드를 수행할 위치
     * @param dOverrideVel 속도 오버라이드 값
     * @param nTarget 속도 오버라이드 대상 (0: COMMAND, 1: ACTUAL)
	 *
	 * @note 주의 사항: AxmOverrideVelAtPos 함수를 사용하기 전에 AxmOverrideMaxVel 최고로 설정 할 수 있는 속도를 설정
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmOverrideVelAtPos(int nAxisNo, double dPos, double dVel, double dAccel, double dDecel, double dOverridePos, double dOverrideVelocity, int nTarget);
    
	/**
     * @brief 지정한 위치에서 지정한 속도로 오버라이드 수행
     *
     * @param nAxisNo 축 번호
     * @param dPos 구동 거리
     * @param dVel 구동 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param nArraySize 오버라이드 할 위치 개수
     * @param dpOverridePos 오버라이드 할 위치 배열
     * @param dpOverrideVel 오버라이드 할 위치에서 변경 될 속도 배열
     * @param nTarget 속도 오버라이드 대상 (0: COMMAND, 1: ACTUAL)
     * @param uOverrideMode 오버라이드 시작 방법
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmOverrideVelAtMultiPos(int nAxisNo, double dPos, double dVel, double dAccel, double dDecel, int nArraySize, double[] dpOverridePos, double[] dpOverrideVel, int nTarget, uint uOverrideMode);

    /**
     * @brief 지정한 시점들에서 지정한 속도/가감속도로 오버라이드 (MLII 전용)
     *
     * @param nAxisNo 축 번호
     * @param dPos 구동 거리
     * @param dVel 구동 속도
     * @param dAccel 가속도 값
     * @param dDecel 감속도 값
     * @param nArraySize 오버라이드 할 위치 개수 (MAX: 5)
     * @param dpOverridePos 오버라이드 할 위치 배열 (nArraySize에서 설정한 개수보다 같거나 크게 선언)
     * @param dpOverrideVel 오버라이드 할 위치에서 변경 될 속도 배열 (nArraySize에서 설정한 개수보다 같거나 크게 선언)
     * @param nTarget 속도 오버라이드 대상 (0: COMMAND, 1: ACTUAL)
     * @param uOverrideMode 오버라이드 시작 방법
	 *
	 * @details
	 * uOverrideMode
	 *    OVERRIDE_POS_START(0): 지정한 위치에서 지정한 속도로 오버라이드 시작
	 *    OVERRIDE_POS_END(1)  : 지정한 위치에서 지정한 속도가 되도록 미리 오버라이드 시작
	 *    OVERRIDE_POS_AUTO(2) : 지정한 위치의 가속 구간에서는 지정한 속도로, 감속 구간에서는 지정한 속도가 되도록 미리 오버라이드 시작
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
	 [DllImport("AXL.dll")] public static extern uint AxmOverrideVelAtMultiPos2(int nAxisNo, double dPos, double dVel, double dAccel, double dDecel, int nArraySize, double[] dpOverridePos, double[] dpOverrideVel, double[] dpOverrideAccelDecel, int nTarget, uint uOverrideMode);
    
    // 마스터, 슬레이브  기어비로 구동 함수
	
    /**
     * @brief Electric Gear 모드에서 Master 축과 Slave 축과의 기어비 설정
     *
     * @param nMasterAxisNo 마스터 축 번호
     * @param nSlaveAxisNo 슬레이브 축 번호
     * @param dSlaveRatio 마스터축에 대한 슬레이브의 기어비 (0: 0%, 0.5: 50%, 1: 100%)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmLinkSetMode(int nMasterAxisNo, int nSlaveAxisNo, double dSlaveRatio);
    
	/**
     * @brief Electric Gear 모드에서 Master 축과 Slave 축과의 기어비 확인
     *
     * @param nMasterAxisNo 마스터 축 번호
     * @param nSlaveAxisNo 슬레이브 축 번호 저장
     * @param dpGearRatio 마스터축에 대한 슬레이브의 기어비 저장 (0: 0%, 0.5: 50%, 1: 100%)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmLinkGetMode(int nMasterAxisNo, ref uint uSlaveAxisNo, ref double dpGearRatio);
    
	/**
     * @brief Master 축과 Slave 축 간의 전자 기어비 설정 해제
     *
     * @param nMasterAxisNo 마스터 축 번호
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmLinkResetMode(int nMasterAxisNo);

	// 겐트리 관련 함수
	
    /**
     * @brief Master 축을 Gantry 제어로 설정해 Slave 축을 Master 축과 동기화
     *
     * @param nMasterAxisNo 마스터 축 번호
     * @param nSlaveAxisNo 슬레이브 축 번호
     * @param uSlHomeUse aster와 같이 Slave 축도 원점 검색을 할 것인지 선택 (0~2)
     * @param dSlOffset Master 축의 원점 Sensor와 Slave 축의 원점 Sensor 간의 기구적인 오차 값
     * @param dSlOffsetRange 원점 검색 시 Master 축의 원점 Sensor와 Slave 축의 원점 Sensor 간 허용할 최대 오차 값
     *
	 * @details
	 * uSlHomeUse
	 *    0: Master 축만 원점 검색
	 *    1: Master 축과 Slave 축 모두 원점 검색. 단 Slave 축에 dSlOffset 값을 적용하여 보정
	 *    2: Master 축과 Slave 축의 Sensor 오차 값 확인
	 * 
	 * @note
	 * 이 함수를 이용하여 Master 축을 겐트리 제어로 설정하면 해당 Slave 축은 Master 축과 동기되어 구동
	 * Gantry 제어 기능을 활성화 시킨 이후 Slave 축에 구동이나 정지 명령 등을 내려도 모두 무시
	 * 주의 사항: AxmGantrySetEnable 함수는 Master 축과 Slave 축의 Servo On 상태가 동일할 때만 정상 설정 가능
	 *    (예시1) Master 축의 Servo On 상태: FALSE, Slave 축의 Servo On 상태: FALSE -> Gantry 설정 가능
	 *    (예시2) Master 축의 Servo On 상태: TRUE , Slave 축의 Servo On 상태: FALSE -> Gantry 설정 불가
	 *    (예시3) Master 축의 Servo On 상태: FALSE, Slave 축의 Servo On 상태: TRUE  -> Gantry 설정 불가
	 *    (예시4) Master 축의 Servo On 상태: TRUE , Slave 축의 Servo On 상태: TRUE  -> Gantry 설정 가능
	 * PCI-Nx04 사용 시 주의 사항: 
	 *    Gantry ENABLE 시 Slave 축은 모션 중 AxmStatusReadMotion 함수로 확인하면 True(Motion 구동 중)로 확인되어야 정상 동작
	 *    Slave 축을 AxmStatusReadMotion 함수로 확인했을 때, InMotion이 False면 Gantry ENABLE이 되지 않은 것이므로 Alarm 혹은 Limit Sensor 등을 확인
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGantrySetEnable(int nMasterAxisNo, int nSlaveAxisNo, uint uSlHomeUse, double dSlOffset, double dSlOffsetRange);

    /**
     * @brief 겐트리 구동에 있어 사용자가 설정한 파라메타 반환
     *
     * @param nMasterAxisNo 마스터 축 번호
     * @param upSlHomeUse Slave 축 원점 검색 여부 선택
     * @param dpSlOffset Master 축의 원점 센서와 Slave 축 원점 센서 간의 기구적인 오차 값
     * @param dpSlORange 원점 검색 시 Master 축의 원점 센서와 Slave 축 원점 센서 간에 허용할 최대 오차 값
	 * @param upGatryOn 겐트리 ON/OFF 설정 (0: OFF, 1: ON)
     *
	 * @details
	 * upSlHomeUse
     *    [00h] Master 축만 원점 검색 선택 되었는지 여부
	 *    [01h] Master 축과 Slave 축 모두 원점 검색 선택 되었는지 여부
     *    [02h] Master 축과 Slave 축 센서의 오차 값 확인
	 *
	 * @note
	 * Slave 축의 Offset 값을 알아내는 방법
	 *    1. 마스터, 슬레이브를 모두 서보 온
	 *    2. AxmGantrySetEnable 함수에서 uSlHomeUse = 2로 설정 후 AxmHomeSetStart 함수를 이용해서 홈을 찾음
	 *    3. 홈을 찾고 나면 마스터 축의 Command 값을 읽어보면 마스터 축과 슬레이브 축의 틀어진 Offset 값을 볼수 있음
	 *    4. Offset 값을 읽어 AxmGantrySetEnable 함수의 dSlOffset 인자에 넣어 줌
	 *    5. dSlOffset 값을 넣어줄 때 마스터 축에 대한 슬레이브 축 값이기 때문에 부호를 반대로 -dSlOffset 넣어 줌
	 *    6. dSIOffsetRange는 Slave Offset의 Range 범위를 말하는데 Range의 한계를 지정하여 한계를 벗어나면 에러를 발생시킬 때 사용
	 *    7. AxmGantrySetEnable 함수에 Offset 값을 넣었으면 AxmGantrySetEnable 함수에서 uSlHomeUse = 1로 설정 후 AxmHomeSetStart 함수를 이용해서 홈을 찾음
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGantryGetEnable(int nMasterAxisNo, ref uint upSlHomeUse, ref double dpSlOffset, ref double dpSlORange, ref uint upGatryOn);
    
	/**
     * @brief 두 축이 기구적으로 Link 되어 있는 갠트리 구동시스템 제어 해제
     *
     * @param nMasterAxisNo 마스터 축 번호
     * @param nSlaveAxisNo 슬레이브 축 번호
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGantrySetDisable(int nMasterAxisNo, int nSlaveAxisNo);

    /**
     * @brief 두 축이 기구적으로 Link 되어 있는 겐트리 구동시스템 제어 중 동기 보상 기능 설정 (PCI-Rxx04-MLII 전용)
     *
     * @param nMasterAxisNo 마스터 축 번호
     * @param nMasterGain 마스터 Gain 값
     * @param nSlaveGain 슬레이브 Gain 값
	 *
	 * @note
	 * 두 축간 위치 편차에 대한 보상 값 반영 비율을 % 값으로 입력
	 * Gain 값을 0으로 입력하면 두 축간 위치 편차 보상 기능을 사용하지 않음 (Default: 0%)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGantrySetCompensationGain(int nMasterAxisNo, int nMasterGain, int nSlaveGain);
    
	/**
     * @brief 두 축이 기구적으로 Link 되어 있는 겐트리 구동시스템 제어 중 동기 보상 기능 확인 (PCI-Rxx04-MLII 전용)
     *
     * @param nMasterAxisNo 마스터 축 번호
     * @param nMasterGain 마스터 Gain 값 저장
     * @param nSlaveGain 슬레이브 Gain 값 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGantryGetCompensationGain(int nMasterAxisNo, ref int nMasterGain, ref int nSlaveGain);

    /**
     * @brief Master와 Slave 간 위치 편차 범위 설정 (PCI-R1604 / PCI-R3200-MLIII 전용)
     *
     * @param nMasterAxisNo Gantry Master 축 번호
     * @param dErrorRange 위치 편차 범위 설정 값
     * @param uUse 모드 설정
	 *
	 * @details
	 * uUse
	 *    0: Disable
	 *    1: Normal 모드
	 *    2: Flag Latch 모드
	 *    3: Flag Latch 모드 + Error 발생 시 SSTOP
	 *    4: Flag Latch 모드 + Error 발생 시 ESTOP
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGantrySetErrorRange(int nMasterAxisNo, double dErrorRange, uint uUse);
    
	/**
     * @brief Master와 Slave 간 위치 편차 범위 값 확인 (PCI-R1604 / PCI-R3200-MLIII 전용)
     *
     * @param nMasterAxisNo Gantry Master 축 번호
     * @param dpErrorRange 위치 편차 범위 설정 값 저장
     * @param upUse 모드 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGantryGetErrorRange(int nMasterAxisNo, ref double dpErrorRange, ref uint upUse);
    
	/**
     * @brief Master와 Slave 간의 위치편차값 비교 결과 반환
     *
     * @param nMasterAxisNo Gantry Master 축 번호
     * @param dpStatus 위치 편차 비교 결과
	 *
	 * @details
	 * dwpStatus
	 *    FALSE(0): Master와 Slave 사이의 위치 편차 범위가 설정한 범위 보다 작음 (정상 상태)
	 *    TRUE(1) : Master와 Slave 사이의 위치 편차 범위가 설정한 범위 보다 큼  (비정상 상태)
	 *
	 * @note
	 * Gantry Enable && Master/Slave Servo On 상태를 만족 할 때만 AXT_RT_SUCCESS를 Return
	 * Latch 모드의 경우 AxmGantryReadErrorRangeComparePos를 호출 해야 Latch Flag가 Reset
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGantryReadErrorRangeStatus(int nMasterAxisNo, ref uint dpStatus);
    
	/**
     * @brief Master와 Slave 간의 위치 편차값 반환 
     *
     * @param nMasterAxisNo Gantry Master 축 번호
     * @param dpComparePos 위치 편차값 저장
	 *
	 * @note
	 * Flag Latch 모드 일 때 다음 Error가 발생 되기 전까지 이전 Error가 발생 했을 때의 위치 편차값 유지
	 * dwpStatus가 1일 때만 Read 해야 함. 계속 ComparePos를 Read 하면 부하가 많이 걸려 함수 응답속도가 느려지게 됨
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGantryReadErrorRangeComparePos(int nMasterAxisNo, ref double dpComparePos);

    // 일반 보간 함수
	
    /**
     * @brief 지정된 좌표계에 시작점과 종료점을 지정하여 다축 직선 보간 구동
     *
     * @param nCoord 좌표계 번호
     * @param dpEndPos 위치 배열
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
	 *
	 * @note
	 * 구동 시작 후 함수를 벗어 남
	 * AxmContiBeginNode, AxmContiEndNode와 같이 사용 시 지정된 좌표계에 시작점과 종료점을 지정하여 직선 보간 구동하는 Queue에 함수 저장 됨
	 * 직선 프로파일 연속 보간 구동을 위해 내부 Queue에 저장하여 AxmContiStart 함수를 사용해서 시작
	 * 주의 사항 1:
	 *    AxmContiSetAxisMap 함수를 이용하여 축 맵핑 후에 낮은 순서 축부터 맵핑을 하면서 사용해야 함
	 *    원호 보간의 경우에는 반드시 낮은 순서 축부터 축 배열에 넣어야 동작 가능
	 * 주의 사항 2:
	 *    위치를 설정 할 경우 반드시 마스터 축과 슬레이브 축의 UNIT/PULSE를 맞춰서 설정
	 *    위치를 UNIT/PULSE 보다 작게 설정 할 경우 최소 단위가 UNIT/PULSE로 맞춰지기 때문에 그 위치까지 구동이 될 수 없음
	 * 주의 사항 3:
	 *    원호 보간을 할 경우 반드시 한 칩내에서 구동이 될 수 있으므로 4축 내에서만 선택해서 사용해야 함
	 * 주의 사항 4:
	 *    보간 구동 시작 중에 비 정상 정지 조건(+/- Limit신호, 서보 알람, 비상정지 등)이 발생하면 구동 방향에 상관없이 구동을 시작하지 않거나 정지 됨
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmLineMove(int nCoord, double[] dpEndPos, double dVel, double dAccel, double dDecel);

    /**
     * @brief 2축 단위 직선 보간 수행 (Software 방식)
     *
     * @param nCoord 좌표계 번호
     * @param dpEndPos 종료점의 위치 지정 배열
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
	 *
	 * @note 시작점과 종료점을 지정하여 다축 직선 보간 구동하는 함수. 구동 시작 후 함수를 벗어남
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmLineMoveEx2(int nCoord, double[] dpEndPos, double dVel, double dAccel, double dDecel);

    /**
     * @brief 2차 원호 보간 수행
     *
     * @param nCoord 좌표계 번호
     * @param nAxisNo 보간에 사용되는 두 축 배열
     * @param dpCenterPos 중심점 X, Y 배열
     * @param dpEndPos 종료점 X, Y 배열
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
     *
	 * @note 
	 * 시작점, 종료점과 중심점을 지정하여 원호 보간 구동하는 함수. 구동 시작 후 함수를 벗어남
	 * AxmContiBeginNode, AxmContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 종료점과 중심점을 지정하여 구동하는 원호 보간 Queue에 함수 저장
	 * 프로파일 원호 연속 보간 구동을 위해 내부 Queue에 저장하여 AxmContiStart 함수를 사용해서 시작
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCircleCenterMove(int nCoord, int[] nAxisNo, double[] dpCenterPos, double[] dpEndPos, double dVel, double dAccel, double dDecel, uint uCWDir);

    /**
     * @brief 중간점, 종료점 지정하여 원호 보간 구동
     *
     * @param nCoord 좌표계 번호
     * @param nAxisNo 보간에 사용되는 두 축 배열
     * @param dpMidPos 중간점 X, Y 배열
     * @param dpEndPos 종료점 X, Y 배열
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param lArcCircle 원호 종류 (0: 아크, 1: 원)
     *
	 * @note
	 * 중간점, 종료점을 지정하여 원호 보간 구동하는 함수. 구동 시작 후 함수를 벗어남
	 * AxmContiBeginNode, AxmContiEndNode와 같이 사용 시 지정된 좌표계에 중간점, 종료점을 지정하여 구동하는 원호 보간 Queue에 함수 저장
	 * 프로파일 원호 연속 보간 구동을 위해 내부 Queue에 저장하여 AxmContiStart 함수를 사용해서 시작
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCirclePointMove(int nCoord, int[] nAxisNo, double[] dpMidPos, double[] dpEndPos, double dVel, double dAccel, double dDecel, int nArcCircle);

    /**
     * @brief 중간점, 종료점 지정하여 3차원 원/원호 보간 구동
     *
     * @param nCoordNo 좌표계 번호
     * @param nAxisNo 축 번호 배열
     * @param dpMidPos 중간점 배열
     * @param dpEndPos 종료점 배열
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param nArcCircle 원호 종류 (0: 아크, 1: 원)
     * @param nArraySize 보간에 사용되는 축 개수
	 *
	 * @note 
	 * 구동 시작 후 함수를 벗어남
	 * 축 매핑은 3축 이상 가능하며 3축 이상의 축들은 Linear Interpolation 됨
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCirclePointMoveEx(int nCoordNo, int[] nAxisNo,  double[] dpMidPos, double[] dpEndPos, double dVel, double dAccel, double dDecel, int nArcCircle, int nArraySize);
    
	/**
     * @brief 시작점, 종료점과 반지름 지정하여 원호 보간 구동
     *
     * @param nCoord 좌표계 번호
     * @param nAxisNo 보간에 사용되는 두 축 배열
     * @param dRadius 반지름
     * @param dpEndPos 종료점 X, Y 배열
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
     * @param uShortDistance 중점까지 가는 원의 이동 거리 크기 설정 값 (0: 작은원, 1: 큰원)
     *
	 * @note
	 * 구동 시작 후 함수를 벗어남
	 * AxmContiBeginNode, AxmContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 종료점과 반지름을 지정하여 구동하는 원호 보간 Queue에 함수 저장
	 * 프로파일 원호 연속 보간 구동을 위해 내부 Queue에 저장하여 AxmContiStart 함수를 사용해서 시작
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCircleRadiusMove(int nCoord, int[] nAxisNo, double dRadius, double[] dpEndPos, double dVel, double dAccel, double dDecel, uint uCWDir, uint uShortDistance);

    /**
     * @brief 시작점, 회전각도와 반지름 지정하여 원호 보간 구동
     *
     * @param nCoord 좌표계 번호
     * @param nAxisNo 보간에 사용되는 두 축 배열
     * @param dpCenterPos 중심점 X, Y 배열
     * @param dAngle 각도
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
     *
	 * @note
	 * 구동 시작 후 함수를 벗어남
	 * AxmContiBeginNode, AxmContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 회전 각도와 반지름을 지정하여 구동하는 원호 보간 Queue에 함수 저장
	 * 프로파일 원호 연속 보간 구동을 위해 내부 Queue에 저장하여 AxmContiStart 함수를 사용해서 시작
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCircleAngleMove(int nCoord, int[] nAxisNo, double[] dpCenterPos, double dAngle, double dVel, double dAccel, double dDecel, uint uCWDir);

    // 연속 보간 함수
	
    /**
     * @brief 지정된 좌표계에 연속 보간 축 맵핑 설정
     *
     * @param nCoord 좌표계 번호
     * @param uSize 배열 크기
     * @param npRealAxesNo 축 번호 배열
	 *
	 * @note
	 * 축 맵핑 번호는 0 부터 시작
	 * 주의 사항: 
	 *    축 맵핑 할 때는 반드시 실제 축번호가 작은 숫자부터 큰숫자 순서로 넣어야 함
	 *    가상 축 맵핑 함수를 사용하였을 때 가상 축 번호를 실제 축번호가 작은 값 부터 lpAxesNo의 낮은 인텍스에 입력해야 함
	 *    가상 축 맵핑 함수를 사용하였을 때 가상 축 번호에 해당하는 실제 축 번호가 다른 값이어야 함
	 *    같은 축을 다른 Coordinate에 중복 맵핑하면 안됨
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiSetAxisMap(int nCoord, uint uSize, int[] npRealAxesNo);
    
	/**
     * @brief 지정된 좌표계에 연속 보간 축 맵핑 설정 정보 확인
     *
     * @param nCoord 좌표계 번호
     * @param uSize 배열 크기 저장
     * @param npRealAxesNo 축 번호 배열 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiGetAxisMap(int nCoord, ref uint uSize, int[] npRealAxesNo);
    
	/**
     * @brief 지정된 좌표계에 연속 보간 축 맵핑 초기화
     *
     * @param nCoord 좌표계 번호
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiResetAxisMap(int nCoord);

    /**
     * @brief 지정된 좌표계에 연속보간 축 절대/상대 모드 설정
     *
     * @param nCoord 좌표계 번호
     * @param uAbsRelMode 이동 거리 계산 모드
	 *
	 * @details
	 * uAbsRelMode
	 *    [0] POS_ABS_MODE: 절대 좌표계
	 *    [1] POS_REL_MODE: 상대 좌표계
	 *
     * @note 주의 사항: 반드시 축 맵핑 하고 사용 가능
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiSetAbsRelMode(int nCoord, uint uAbsRelMode);
    
	/**
     * @brief 지정된 좌표계에 연속보간 축 절대/상대 모드 설정 값 확인
     *
     * @param nCoord 좌표계 번호
     * @param upAbsRelMode 이동 거리 계산 모드 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiGetAbsRelMode(int nCoord, ref uint upAbsRelMode);

    /**
     * @brief 지정된 좌표계에 보간 구동 위한 내부 Queue가 비어 있는지 확인
     *
     * @param nCoord 좌표계 번호
     * @param upQueueFree 내부 Queue 상태 값 (0: Queue 내부가 비어있지 않음, 1: Queue 내부가 비어 있음)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiReadFree(int nCoord, ref uint upQueueFree);
    
	/**
     * @brief 지정된 좌표계에 보간 구동 위한 내부 Queue에 저장되어 있는 보간 구동 개수 확인
     *
     * @param nCoord 좌표계 번호
     * @param npQueueIndex 내부 Queue에 저장되어 있는 보간 구동 개수 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiReadIndex(int nCoord, ref int npQueueIndex);

    /**
     * @brief 지정된 좌표계에 연속 보간 구동 위해 저장된 내부 Queue를 모두 삭제
     *
     * @param nCoord 좌표계 번호
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiWriteClear(int nCoord);

    /**
     * @brief 지정된 좌표계에 연속 보간에서 수행 할 작업들 등록 시작
     *
     * @param nCoord 좌표계 번호
	 *
	 * @note
	 * 이 함수 호출 후 AxmContiEndNode 함수가 호출되기 전까지 수행되는 모든 모션 작업은 실제 모션을 수행하는 것이 아니라
	 * 연속 보간 모션으로 등록 되는 것이며 AxmContiStart 함수가 호출될 때 비로소 등록된 모션이 실제로 수행
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiBeginNode(int nCoord);
    
	/**
     * @brief 지정된 좌표계에서 연속보간 수행할 작업들 등록 종료
     *
     * @param nCoord 좌표계 번호
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiEndNode(int nCoord);

    /**
     * @brief 저장된 내부 연속 보간 Queue의 구동 시작 함수
     *
     * @param nCoord 좌표계 번호
     * @param uProfileset 프로파일 모드
     * @param nAngle 자동 가감속 모드 사용 시 Angle 값 (0~360도)
	 *
	 * @details
	 * dwProfileset
	 *    [0] CONTI_NODE_VELOCITY: 속도 지정 보간 모드
	 *    [1] CONTI_NODE_MANUAL  : 노드 가감속 보간 모드
	 *    [2] CONTI_NODE_AUTO    : 자동 가감속 보간 모드
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiStart(int nCoord, uint uProfileset, int nAngle);
    
	/**
     * @brief 지정된 좌표계에 연속 보간 구동 중인지 확인
     *
     * @param nCoord 좌표계 번호
     * @param upInMotion 연속 보간 구동 중 여부 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiIsMotion(int nCoord, ref uint upInMotion);

    /**
     * @brief 지정된 좌표계에 연속 보간 구동 중 현재 구동중인 연속 보간 인덱스 번호 확인
     *
     * @param nCoord 좌표계 번호
     * @param npNodeNum 현재 구동중인 연속 보간 인덱스 번호 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiGetNodeNum(int nCoord, ref int npNodeNum);
    
	/**
     * @brief 지정된 좌표계에 설정한 연속 보간 구동 총 인덱스 개수 확인
     *
     * @param nCoord 좌표계 번호
     * @param npNodeNum 연속 보간 구동 총 인덱스 개수 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiGetTotalNodeNum(int nCoord, ref int npNodeNum);

    /**
     * @brief 특정 모션 세그먼트에서 I/O 출력 수행
     *
     * @param nCoord 좌표계 번호
     * @param nSize 동시에 출력할 IO 접점 수 (1 ~ 8)
     * @param nModuleType 모듈 타입 (0: Motion I/O Output(Slave 자체 출력), 1: Digital I/O Output)
     * @param npModuleNo 모듈 번호 배열 (lModuleType이 0일 경우 축 번호, lModuleType이 1일 경우 Digital I/O Module No)
     * @param npBit 출력 접점에 대한 Offset 위치 배열
     * @param npOffOn 해당 출력 접점의 출력값 배열 (0: LOW, 1: HIGH)
     * @param dpDistTime 거리 값(pulse) 또는 시간 값(msec) 저장 배열. 모션 프로파일 종점 기준으로 함
     * @param npDistTimeMode 출력 방식 모드 배열
     *
	 * @note
	 * AxmContiBeginNode 함수와 AxmContiEndNode 함수 사이에서 사용해야 함
	 * 다음 연속 보간 구동 함수(ex: AxmLineMove, AxmCircleCenterMove, etc...)에 대해서만 유효 함
	 * Digital I/O 출력 시점은 다음 연속 보간 구동 함수의 종점을 기준으로 조건(dpDistTime, lpDistTimeMode)만큼 이전에 출력 함
	 * 마지막 4개 param은 lSize 만큼의 벼열로 입력해서 여러 출력 접점을 동시에 제어할 수 있음
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiDigitalOutputBit(int nCoord, int nSize, int nModuleType, int[] npModuleNo, int[] npBit, int[] npOffOn, double[] dpDistTime, int[] npDistTimeMode);

    /**
     * @brief 지정된 좌표계의 EndStatus(정지 상태) 레지스터 반환
     *
     * @param nCoord 좌표계 번호
     * @param upStatus EndStatus 레지스터 값
	 *
	 * @note
	 * 축 모션 종료 상태 값
	 *    [00000000h] 구동하지 않음 또는 초기 상태
	 *    [00000001h] Bit 0:  정방향 리미트 신호(PELM)에 의한 종료
	 *    [00000002h] Bit 1:  역방향 리미트 신호(NELM)에 의한 종료
	 *    [00000010h] Bit 4:  정방향 소프트 리미트 급정지 기능에 의한 구동 종료
	 *    [00000020h] Bit 5:  역방향 소프트 리미트 급정지 기능에 의한 구동 종료
	 *    [00000040h] Bit 6:  정방향 소프트 리미트 감속정지 기능에 의한 구동 종료
	 *    [00000080h] Bit 7:  역방향 소프트 리미트 감속정지 기능에 의한 구동 종료
	 *    [00000100h] Bit 8:  서보 알람 기능에 의한 구동 종료
	 *    [00000200h] Bit 9:  비상 정지 신호 입력에 의한 구동 종료
	 *    [00000400h] Bit 10: 급 정지 명령에 의한 구동 종료
	 *    [00000800h] Bit 11: 감속 정지 명령에 의한 구동 종료
	 *    [00001000h] Bit 12: 전축 급정지 명령에 의한 구동 종료
	 *    [00040000h] Bit 18: 신호 검색 성공 종료
	 *    [00080000h] Bit 19: 보간 데이터 이상으로 구동 종료
	 *    [00100000h] Bit 20: 비정상 구동 정지 발생(EMG, Alarm, Limit, Software Limit)
	 *    [10000000h] Bit 28: 현재/마지막 구동 드라이브 방향
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiReadStop(int nCoord, ref uint upStatus);

    /**
     * @brief LineMove 함수들에 대해 속도 지정 방식 설정
     *
     * @param nCoord 좌표계 번호
     * @param uVelMode 속도 모드 (0: 백터 속도, 1: 지정축 속도, 2: 장축 속도(Pulse 기준))
     * @param nMasterAxisNo 지정 축 (uVelMode가 1일 때 지정 축)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiSetLineMoveVelMode(int nCoord, uint uVelMode, int nMasterAxisNo);
	
	/**
     * @brief LineMove 함수들에 대해 속도 지정 방식 확인
     *
     * @param nCoord 좌표계 번호
     * @param uVelMode 속도 모드 저장 (0: 백터 속도, 1: 지정축 속도, 2: 장축 속도(Pulse 기준))
     * @param npMasterAxisNo 지정 축 저장 (uVelMode가 1일 때 지정 축)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiGetLineMoveVelMode(int nCoord, ref uint upVelMode, ref int npMasterAxisNo);

    /**
     * @brief LineMove 함수로 구동 및 예약할 때 소프트 리밋 체크 기능 사용 여부 설정
     *
     * @param nCoord 좌표계 번호
     * @param uUse 소프트 리밋 체크 기능 사용 여부 (0: 사용하지 않음, 1: 사용함)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiSetCheckSoftLimit(int nCoord, uint uUse);
	
	/**
     * @brief LineMove 함수로 구동 및 예약할 때 소프트 리밋 체크 기능 사용 여부 확인
     *
     * @param nCoord 좌표계 번호
     * @param upUse 소프트 리밋 체크 기능 사용 여부 저장 (0: 사용하지 않음, 1: 사용함)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiGetCheckSoftLimit(int nCoord, ref uint upUse);

	/**
     * @brief 지정된 좌표 시스템에 대해 연속 운동(곡선이나 라운딩되는 경로)을 위한 연결 반경(Connection Radius) 설정
     *
     * @param nCoord 좌표계 번호
     * @param dRadius 반지름 값
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiSetConnectionRadius(int nCoord, double dRadius);
	
	/**
     * @brief 지정된 좌표 시스템에 대해 연속적인 경로 이동 중 허용할 최대 구심 가속도(centripetal acceleration) 설정
     *
     * @param nCoord 좌표계 번호
     * @param dMaxAccel 최대 가속도 값
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiSetConnectionMaxCentripetalAccel(int nCoord, double dMaxAccel);

    /**
     * @brief 연속 보간 중 일정 시간 대기
     *
     * @param lCoord 좌표계 번호
     * @param uDwellTime 대기 시간 (단위: usec)
     *
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     *
     * @warning 
     * 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiDwell(int nCoord, uint uDwellTime);
	
	// 트리거 함수
	
    /**
     * @brief 지정 축에 트리거 신호 지속 시간 및 트리거 출력 레벨, 트리거 출력 방법 설정
     * 
     * @param nAxisNo 지정 축 번호
     * @param dTrigTime 트리거 출력 시간 (최소 1uec ~ 최대 50msec, 1~50000으로 설정)
     * @param uTriggerLevel 트리거 출력 레벨 유무 (0: LOW, 1: HIGH)
     * @param uSelect 사용할 기준 위치 (0: COMMAND, 1: ACTUAL)
     * @param uInterrupt 인터럽트 설정 (0: DISABLE, 1: ENABLE)
     *
	 * @note
	 * 트리거 기능 사용을 위해서는 먼저 AxmTriggerSetTimeLevel를 사용하여 관련 기능을 먼저 설정해야 함
	 * 주의 사항:
	 *    트리거 위치를 설정 할 경우 반드시 UNIT/PULSE를 맞춰 설정
	 *    위치를 UNIT/PULSE보다 작게 할 경우 최소 단위가 UNIT/PULSE로 맞춰지기 때문에 그 위치에 출력 할 수 없음
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmTriggerSetTimeLevel(int nAxisNo, double dTrigTime, uint uTriggerLevel, uint uSelect, uint uInterrupt);
    
	/**
     * @brief 지정 축에 트리거 신호 지속 시간 및 트리거 출력 레벨, 트리거 출력 방법 확인
     * 
     * @param nAxisNo 지정 축 번호
     * @param dpTrigTime 트리거 출력 시간 저장 (최소 1uec ~ 최대 50msec, 1~50000으로 설정)
     * @param upTriggerLevel 트리거 출력 레벨 유무 저장 (0: LOW, 1: HIGH)
     * @param upSelect 사용할 기준 위치 저장 (0: COMMAND, 1: ACTUAL)
     * @param upInterrupt 인터럽트 설정 저장 (0: DISABLE, 1: ENABLE)
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmTriggerGetTimeLevel(int nAxisNo, ref double dpTrigTime, ref uint upTriggerLevel, ref uint upSelect, ref uint upInterrupt);

    /**
     * @brief 지정 축의 트리거 출력 기능 설정
     *
     * @param nAxisNo 지정 축 번호
     * @param uMethod 트리거 출력 모드
     * @param dPos 트리거 신호를 출력 할 위치 값 (주기 사용 시: 위치마다 트리거 출력)
	 *
	 * @details
	 * uMethod
	 *    [0x0] PERIOD_MODE: 현재 위치를 기준으로 dPos를 위치 주기로 사용한 주기 트리거 방식
	 *    [0x1] ABS_POS_MODE: 트리거 절대 위치에서 트리거 발생, 절대 위치 방식
     *
	 * @note
	 * 주의 사항: AxmTriggerSetAbsPeriod의 주기 모드로 설정 할 경우 처음 그 위치가 범위 안에 있으므로 트리거 출력이 한번 발생 함
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmTriggerSetAbsPeriod(int nAxisNo, uint uMethod, double dPos);
    
	/**
     * @brief 지정 축의 트리거 출력 기능 설정 정보 확인
     *
     * @param nAxisNo 지정 축 번호
     * @param upMethod 트리거 출력 모드 저장
     * @param dpPos 트리거 신호를 출력 할 위치 값 저장 (주기 사용 시: 위치마다 트리거 출력)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmTriggerGetAbsPeriod(int nAxisNo, ref uint upMethod, ref double dpPos);

    /**
     * @brief 사용자가 지정한 시작 위치부터 종료 위치까지 일정 구간마다 트리거 출력
     *
     * @param nAxisNo 지정 축 번호
     * @param dStartPos 시작 위치
     * @param dEndPos 종료 위치
     * @param dPeriodPos 구간 간격
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmTriggerSetBlock(int nAxisNo, double dStartPos, double dEndPos, double dPeriodPos);
    
	/**
     * @brief 사용자가 지정한 시작 위치부터 종료 위치까지 일정 구간마다 트리거 출력 설정 값 확인
     *
     * @param nAxisNo 지정 축 번호
     * @param dpStartPos 시작 위치 저장
     * @param dpEndPos 종료 위치 저장
     * @param dpPeriodPos 구간 간격 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmTriggerGetBlock(int nAxisNo, ref double dpStartPos, ref double dpEndPos, ref double dpPeriodPos);

    /**
     * @brief 사용자가 한 개의 트리거 펄스 출력
     *
     * @param nAxisNo 지정 축 번호
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmTriggerOneShot(int nAxisNo);
    
	/**
     * @brief 사용자가 한 개의 트리거 펄스를 지정 시간 후에 출력
     *
     * @param nAxisNo 지정 축 번호
     * @param nMSec 지정 시간 설정 (단위: mSec)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmTriggerSetTimerOneshot(int nAxisNo, int nMSec);
    
	/**
     * @brief 입력한 절대 위치값의 순서로 해당 위치를 지날 때 트리거 신호 출력
     *
     * @param nAxisNo 지정 축 번호
     * @param nTrigNum 트리거 출력 위치 개수 (1000개)
     * @param dpTrigPos 트리거 위치 배열
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmTriggerOnlyAbs(int nAxisNo, int nTrigNum, double[] dpTrigPos);
    
	/**
     * @brief 트리거 기능 설정 초기화
     *
     * @param nAxisNo 지정 축 번호
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmTriggerSetReset(int nAxisNo);

    // CRC (잔여 펄스 클리어 함수)
	
    /**
     * @brief 지정 축에 CRC 신호 사용 여부 및 출력 레벨 설정
     *
     * @param nAxisNo 지정 축 번호
     * @param uLevel CRC 신호 사용 여부 및 출력 레벨 (0: LOW, 1: HIGH, 2: UNUSED, 3: USED)
     * @param uMethod 잔여 펄스 제거 출력 신호 펄스 폭
	 *
	 * @details
	 * uMethod
	 *    0: 10  uSec
	 *    1: 100 uSec
	 *    2: 500 uSec
	 *    3: 1   mSec
	 *    4: 10  mSec
	 *    5: 50  mSec
	 *    6: 100 mSec
	 *    7: Logic Level
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCrcSetMaskLevel(int nAxisNo, uint uLevel, uint uMethod);
    
	/**
     * @brief 지정 축에 CRC 신호 사용 여부 및 출력 레벨 확인
     *
     * @param nAxisNo 지정 축 번호
     * @param upLevel CRC 신호 사용 여부 및 출력 레벨 저장 (0: LOW, 1: HIGH, 2: UNUSED, 3: USED)
     * @param upMethod 잔여 펄스 제거 출력 신호 펄스 폭 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCrcGetMaskLevel(int nAxisNo, ref uint upLevel, ref uint upMethod);

    /**
     * @brief 지정 축에 CRC 신호 강제 발생
     *
     * @param nAxisNo 지정 축 번호
     * @param uOnOff CRC 신호를 Program으로 발생 여부 (0: FALSE, 1: TRUE)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCrcSetOutput(int nAxisNo, uint uOnOff);
    
	/**
     * @brief 지정 축에 CRC 신호 강제 발생 여부 확인
     *
     * @param nAxisNo 지정 축 번호
     * @param upOnOff CRC 신호를 Program으로 발생 여부 저장 (0: FALSE, 1: TRUE)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCrcGetOutput(int nAxisNo, ref uint upOnOff);

    // MPG(Manual Pulse Generation) 함수
	
    /**
     * @brief 지정 축에 MPG 입력 방식, 드라이브 구동 모드, 이동 거리, MPG 속도 등 설정
     *
     * @param nAxisNo 지정 축 번호
     * @param nInputMethod 입력 신호 방식
     * @param nDriveMode MPG 동작 모드 (0만 설정 가능. 0: MPG 연속 모드)
     * @param dMPGPos MPG 입력 신호마다 이동하는 거리 (MPG_PRESET_MODE 모드일 경우)
     * @param dVel MPG 속도
     * @param dAccel 가감속도 (Reserved)
	 *
	 * @details
	 * nInputMethod
	 *    0: OnePhase
	 *    1: TwoPhase1 (IP만 가능, QI 지원 안함)
	 *    2: TwoPhase2
	 *    3: TwoPhase4
	 *
	 * @note 주의 사항: AxmStatusReadInMotion 함수 실행 결과에 유의. (AxmMPGReset 하기 전까지 정상 상태에서는 모션 구동 중 상태)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMPGSetEnable(int nAxisNo, int nInputMethod, int nDriveMode, double dMPGPos, double dVel, double dAccel);
    
	/**
     * @brief 지정 축에 MPG 입력 방식, 드라이브 구동 모드, 이동 거리, MPG 속도 등 설정 확인
     *
     * @param nAxisNo 지정 축 번호
     * @param npInputMethod 입력 신호 방식 저장
     * @param npDriveMode MPG 동작 모드 저장 (0만 설정 가능. 0: MPG 연속 모드)
     * @param dpMPGPos MPG 입력 신호마다 이동하는 거리 저장 (MPG_PRESET_MODE 모드일 경우)
     * @param dpVel MPG 속도 저장
     * @param dpAccel 가감속도 저장 (Reserved)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMPGGetEnable(int nAxisNo, ref int npInputMethod, ref int npDriveMode, ref double dpMPGPos, ref double dpVel, ref double dpAccel);

    /**
     * @brief 지정 축에 MPG 드라이브 구동 모드에서 한 펄스당 이동할 펄스 비율 설정 (PCI-Nx04 전용 함수)
     *
     * @param nAxisNo 지정 축 번호
     * @param dMPGnumerator   MPG(수동 펄스 발생 장치 입력)구동 시 곱하기 값 (0~64)
     * @param dMPGdenominator MPG(수동 펄스 발생 장치 입력)구동 시 나누기 값 (0~4096)
     *
	 * @note
	 * dMPGdenominator = 4096, MPGnumerator = 1가 의미하는 것은 MPG 한바퀴에 200 펄스면 그대로 1:1로 1 펄스씩 출력을 의미.
	 * 만약 dMPGdenominator = 4096, MPGnumerator = 2로 했을 경우는 1:2로 2 펄스 씩 출력을 내보낸다는 의미.
	 * 칩 내부에 출력 나가는 계산식: MPG PULSE = (Numerator) * (Denominator) / 4096
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMPGSetRatio(int nAxisNo, uint dMPGnumerator, uint dMPGdenominator);
    
	/**
     * @brief 지정 축에 MPG 드라이브 구동 모드에서 한 펄스당 이동할 펄스 비율 확인 (PCI-Nx04 전용 함수)
     *
     * @param nAxisNo 지정 축 번호
     * @param dpMPGnumerator   MPG(수동 펄스 발생 장치 입력)구동 시 곱하기 값 저장 (0~64)
     * @param dpMPGdenominator MPG(수동 펄스 발생 장치 입력)구동 시 나누기 값 저장 (0~4096)
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMPGGetRatio(int nAxisNo, ref uint dpMPGnumerator, ref uint dpMPGdenominator);

    /**
     * @brief 지정 축에 MPG 드라이브 설정 해지
     *
     * @param nAxisNo 지정 축 번호
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMPGReset(int nAxisNo);

    // 헬리컬 이동

    /**
     * @brief 지정된 좌표계에 시작점, 종료점과 중심점을 지정하여 헬리컬 보간 구동
     *
     * @param nCoord 좌표계 번호
     * @param dCenterXPos 중심점 X 위치
     * @param dCenterYPos 중심점 Y 위치
     * @param dEndXPos 종료점 X 위치
     * @param dEndYPos 종료점 Y 위치
     * @param dZPos Z 위치
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
     *
     * @note
     * AxmContiBeginNode, AxmContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 종료점과 중심점을 지정하여 헬리컬 연속 보간 구동하는 함수
     * 원호 연속 보간 구동을 위해 내부 Queue에 저장하는 함수. AxmContiStart 함수를 사용해서 시작 (연속 보간 함수와 같이 이용)
     * 주의 사항: Helix를 연속 보간 사용 시 Spline, 직선보간과 원호보간을 같이 사용할 수 없음
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     *
     * @warning 
     * 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmHelixCenterMove(int nCoord, double dCenterXPos, double dCenterYPos, double dEndXPos, double dEndYPos, double dZPos, double dVel, double dAccel, double dDecel, uint uCWDir);

    /**
     * @brief 지정된 좌표계에 중간점, 종료점을 지정하여 헬리컬 보간 구동
     *
     * @param nCoord 좌표계 번호
     * @param dMidXPos 중간점 X 위치
     * @param dMidYPos 중간점 Y 위치
     * @param dEndXPos 종료점 X 위치
     * @param dEndYPos 종료점 Y 위치
     * @param dZPos Z 위치
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     *
	 * @note
	 * AxmContiBeginNode, AxmContiEndNode와 같이 사용 시 지정된 좌표계에 중간점, 종료점을 지정하여 헬리컬 연속 보간 구동하는 함수
	 * 원호 연속 보간 구동을 위해 내부 Queue에 저장하는 함수. AxmContiStart 함수를 사용해서 시작 (연속 보간 함수와 같이 이용)
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
	 *
	 * @warning 
	 * 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmHelixPointMove(int nCoord, double dMidXPos, double dMidYPos, double dEndXPos, double dEndYPos, double dZPos, double dVel, double dAccel, double dDecel);

    /**
     * @brief 지정된 좌표계에 시작점, 종료점과 반지름을 지정하여 헬리컬 보간 구동
     *
     * @param nCoord 좌표계 번호
     * @param dRadius 반지름
     * @param dEndXPos 종료점 X 위치
     * @param dEndYPos 종료점 Y 위치
     * @param dZPos Z 위치
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
     * @param uShortDistance 중점까지 가는 원의 이동 거리 크기 설정 (0: 작은원, 1: 큰원)
     *
	 * @note
	 * AxmContiBeginNode, AxmContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 종료점과 반지름을 지정하여 헬리컬 연속 보간 구동하는 함수
	 * 원호 연속 보간 구동을 위해 내부 Queue에 저장하는 함수. AxmContiStart 함수를 사용해서 시작 (연속 보간 함수와 같이 이용)
	 *
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHelixRadiusMove(int nCoord, double dRadius, double dEndXPos, double dEndYPos, double dZPos, double dVel, double dAccel, double dDecel, uint uCWDir, uint uShortDistance);

    /**
     * @brief 주어진 좌표계에 시작점, 회전 각도와 반지름 지정하여 헬리컬 보간 구동
     *
     * @param nCoord 좌표계 번호
     * @param dCenterXPos 중심점 X 위치
     * @param dCenterYPos 중심점 Y 위치
     * @param dAngle 각도
     * @param dZPos Z 위치
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
     *
	 * @note
	 * AxmContiBeginNode, AxmContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 회전 각도와 반지름을 지정하여 헬리컬 연속 보간 구동하는 함수
	 * 원호 연속 보간 구동을 위해 내부 Queue에 저장하는 함수. AxmContiStart 함수를 사용해서 시작 (연속 보간 함수와 같이 이용)
	 *
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHelixAngleMove(int nCoord, double dCenterXPos, double dCenterYPos, double dAngle, double dZPos, double dVel, double dAccel, double dDecel, uint uCWDir);

    // 스플라인 이동

    /**
     * @brief 스플라인 연속 보간 구동 함수
     *
     * @param nCoord 좌표계 번호
     * @param nPosSize 이동 시키는 Point 점의 개수 (최소 3개 이상)
     * @param dpPosX 이동시키는 X의 위치값 배열
     * @param dpPosY 이동시키는 Y의 위치값 배열
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param dPosZ Z 위치
     * @param nPointFactor Point 점의 Factor (최소 10 이상. Factor에 따라서 점의 길이가 달라 짐)
	 *
	 * @note
	 * AxmContiBeginNode, AxmContiEndNode와 같이 사용 안함
	 * 원호 연속 보간 구동을 위해 내부 Queue에 저장하는 함수
	 * AxmContiStart 함수를 사용해서 시작 (연속 보간 함수와 같이 이용)
	 * 2축으로 사용 시 dPoZ값을 0으로 넣어주면 되며 3축으로 사용 시 축 맵핑을 3개 및 dPosZ 값을 넣어 줌
	 * 주의 사항: Spline를 연속 보간 사용 시 Helix, 직선 보간과 원호 보간을 같이 사용할 수 없음
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSplineWrite(int nCoord, int nPosSize, double[] dpPosX, double[] dpPosY, double dVel, double dAccel, double dDecel, double dPosZ, int nPointFactor);

	// 기구 오차 보정 테이블 함수 (Compensation Geometric Errors)

    /**
     * @brief 위치 보정 테이블 기능에 필요한 내용 설정
     * 
     * @param nAxisNo 축 번호
     * @param nNumEntry 오차 보정 테이블 데이터 개수
     * @param dStartPos 오차 보정 테이블 위치 offset
     * @param dpPosition 기구 오차 보정 테이블 위치 정보 배열
     * @param dpCorrection 기구 오차 보정 데이터 정보 배열
     * @param uRollOver 롤오버 기능 설정 값 (0: 롤오버 기능 사용 안함, 1: 롤오버 기능 사용)
     *
	 * @note
	 * 롤오버 기능: 기구 오차 보정 테이블 적용 구간을 dpPosition[lNumEntry] 거리만큼 주기적으로 반복하는 기능
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationSet(int nAxisNo, int nNumEntry, double dStartPos, double[] dpPosition, double[] dpCorrection, uint uRollOver);
    
	/**
     * @brief 위치 보정 테이블 기능에 필요한 내용 설정 확인
     * 
     * @param nAxisNo 축 번호
     * @param npNumEntry 오차 보정 테이블 데이터 개수 저장
     * @param dpStartPos 오차 보정 테이블 위치 offset 저장
     * @param dpPosition 기구 오차 보정 테이블 위치 정보 배열 저장
     * @param dpCorrection 기구 오차 보정 데이터 정보 배열 저장
     * @param upRollOver 롤오버 기능 설정 값 저장 (0: 롤오버 기능 사용 안함, 1: 롤오버 기능 사용)
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationGet(int nAxisNo, ref int npNumEntry, ref double dpStartPos, double[] dpPosition, double[] dpCorrection, ref uint upRollOver);

    /**
     * @brief 위치 보정 테이블 기능의 사용 유무 설정
     * 
     * @param nAxisNo 축 번호
     * @param uEnable 기구 오차 보정 테이블 기능 적용/해제 (0: 기능 사용 안함, 1: 기능 사용)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationEnable(int nAxisNo, uint uEnable);
    
	/**
     * @brief 위치 보정 테이블 기능의 사용 유무에 대한 설정 상태 반환
     * 
     * @param nAxisNo 축 번호
     * @param upEnable 기구 오차 보정 테이블 기능 적용/해제 (0: 기능 사용 안함, 1: 기능 사용)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationIsEnable(int nAxisNo, ref uint upEnable);
    
	/**
     * @brief 현재 지령 위치에서의 기구 오차 보정값 반환
     *
     * @param nAxisNo 축 번호
     * @param dpCorrection 기구 오차 보정 데이터 배열
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationGetCorrection(int nAxisNo, double[] dpCorrection);


    /**
     * @brief Backlash 관련 설정 함수
     *
     * @param nAxisNo Backlash 기능 설정 축 번호
     * @param nBacklashDir Backlash 보상을 적용 할 구동 방향 설정 (원점 검색 방향과 동일하게 설정)
     * @param dBacklash 기구부에서 진행 방향과 반대방향으로 방향전환 시 발생되는 Backlash 양 설정
     * 
	 * @details
	 * nBacklashDir
	 *    [0] Command Position 값이 (+)방향으로 구동할 때 지정한 backlash 적용
	 *    [1] Command Position 값이 (-)방향으로 구동할 때 지정한 backlash 적용
	 *    Ex1) lBacklashDir이 0, backlash가 0.01일 때 0.0 -> 100.0으로 위치 이동 할 때 실제 이동하는 위치는 100.01이 됨
	 *    Ex2) lBacklashDir이 0, backlash가 0.01일 때 0.0 -> -100.0으로 위치 이동 할 때 실제 이동하는 위치는 -100.0이 됨
	 *
	 * @note
	 * 정확한 Backlash 보상을 위해서는 원점 검색 시 마지막에 Backlash 양 만큼 (+) or (-)방향으로 이동 한 후 원점을 완료하고 Backlash 보정 사용.
	 * 이 때 Backlash 양 만큼 (+)구동을 했다면 backlash_dir을 [1](-)로, (-)구동을 했다면 backlash_dir을 [0](+)로 설정하면 됨.
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationSetBacklash(int nAxisNo, int nBacklashDir, double dBacklash);
	
	/**
     * @brief Backlash 관련 설정 확인
     *
     * @param nAxisNo Backlash 기능 설정 축 번호
     * @param nBacklashDir Backlash 보상을 적용 할 구동 방향 설정 저장 (원점 검색 방향과 동일하게 설정)
     * @param dpBacklash 기구부에서 진행 방향과 반대방향으로 방향전환 시 발생되는 Backlash 양 설정 저장
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationGetBacklash(int nAxisNo, ref int nBacklashDir, ref double dpBacklash);
	
	/**
     * @brief Backlash 사용 유무 설정
     *
     * @param nAxisNo 축 번호
     * @param uEnable Backlash 보정 사용 유무 지정 (0: DISABLE(보정 사용 안함), 1: ENABLE(보정 사용))
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationEnableBacklash(int nAxisNo, uint uEnable);
	
	/**
     * @brief Backlash 사용 유무 확인
     *
     * @param nAxisNo 축 번호
     * @param upEnable Backlash 보정 사용 유무 저장 (0: DISABLE(보정 사용 안함), 1: ENABLE(보정 사용))
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationIsEnableBacklash(int nAxisNo, ref uint upEnable);
   	
	/**
     * @brief Backlash 보정 기능을 사용할 때 Backlash 양 만큼 좌우로 이동하여 기구물의 위치 자동 정렬 (서보 온 동작 이후 한번 사용)
     *
     * @param nAxisNo 축 번호
     * @param dVel 이동 속도 [unit / sec]
     * @param dAccel 이동 가속도 [unit / sec^2]
     * @param dDecel 이동 감속도 [unit / sec^2]
     * @param dWaitTime Backlash 양 만큼 구동 후 원래의 위치로 되돌아오기까지의 대기 시간 [msec]
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationSetLocating(int nAxisNo, double dVel, double dAccel, double dDecel, double dWaitTime);	

    /**
     * @brief ECAM 기능에 필요한 내용 설정
     * 
     * @param nAxisNo 축 번호
     * @param nMasterAxisNo ECAM 마스터(기준) 축 번호 (nAxisNo ≠ lMasterAxis)
     * @param nNumEntry ECAM 위치 정보 데이터 개수
     * @param dMasterStartPos ECAM 위치 정보 시작 데이터가 적용 될 마스터 축 위치 정보
     * @param dpMasterPos ECAM 마스터 축의 dMasterStartPos를 기준으로 한 상대 위치 배열
     * @param dpSlavePos ECAM 시작 시점을 기준으로 한 ECAM 구동 축의 상대 위치 배열
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmEcamSet(int nAxisNo, int nMasterAxisNo, int nNumEntry, double dMasterStartPos, double[] dpMasterPos, double[] dpSlavePos);
    
		/**
     * @brief ECAM 기능에 필요한 내용 설정 확인
     * 
     * @param nAxisNo 축 번호
     * @param npMasterAxisNo ECAM 마스터(기준) 축 번호 저장 (nAxisNo ≠ lMasterAxis)
     * @param npNumEntry ECAM 위치 정보 데이터 개수 저장
     * @param dpMasterStartPos ECAM 위치 정보 시작 데이터가 적용 될 마스터 축 위치 정보 저장
     * @param dpMasterPos ECAM 마스터 축의 dMasterStartPos를 기준으로 한 상대 위치 배열 저장
     * @param dpSlavePos ECAM 시작 시점을 기준으로 한 ECAM 구동 축의 상대 위치 배열 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmEcamGet(int nAxisNo, ref int npMasterAxisNo, ref int npNumEntry, ref double dpMasterStartPos, double[] dpMasterPos, double[] dpSlavePos);
	
	/**
     * @brief ECAM 기능에 필요한 내용 CMD/ACT Source와 함께 설정 (PCIe-Rxx04-SIIIH 전용 함수)
     *
     * @param nAxisNo 축 번호
     * @param nMasterAxis ECAM 마스터(기준) 축 번호 (nAxisNo ≠ lMasterAxis)
     * @param nNumEntry ECAM 위치 정보 데이터 개수
     * @param dMasterStartPos ECAM 위치 정보 시작 데이터가 적용 될 마스터 축 위치 정보
     * @param dpMasterPos ECAM 마스터 축의 dMasterStartPos를 기준으로 한 상대 위치 배열
     * @param dpSlavePos ECAM 시작 시점을 기준으로 한 ECAM 구동 축의 상대 위치 배열
     * @param uSource ECAM 위치 기준 설정 (0: 목표 위치 기준, 1: 실제 위치 기준)
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmEcamSetWithSource(int nAxisNo, int nMasterAxis, int nNumEntry, double dMasterStartPos, double[] dpMasterPos, double[] dpSlavePos, uint uSource);
    
    /**
     * @brief ECAM 기능에 필요한 내용 CMD/ACT Source와 함께 설정 확인 (PCIe-Rxx04-SIIIH 전용 함수)
     *
     * @param nAxisNo 축 번호
     * @param npMasterAxis ECAM 마스터(기준) 축 번호 저장 (nAxisNo ≠ lMasterAxis)
     * @param npNumEntry ECAM 위치 정보 데이터 개수 저장
     * @param dMasterStartPos ECAM 위치 정보 시작 데이터가 적용 될 마스터 축 위치 정보 저장
     * @param dpMasterPos ECAM 마스터 축의 dMasterStartPos를 기준으로 한 상대 위치 배열 저장
     * @param dpSlavePos ECAM 시작 시점을 기준으로 한 ECAM 구동 축의 상대 위치 배열 저장
     * @param upSource ECAM 위치 기준 정보 저장 (0: 목표 위치 기준, 1: 실제 위치 기준)
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmEcamGetWithSource(int nAxisNo, ref int npMasterAxis, ref int npNumEntry, ref double dpMasterStartPos, double[] dpMasterPos, double[] dpSlavePos, ref uint upSource);

    /**
     * @brief ECAM 기능 사용 유무 설정
	 *
     * @param nAxisNo 축 번호
     * @param uEnable ECAM 기능 사용 유무 (0: ECAM 기능 미사용, 1: ECAM 기능 사용)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmEcamEnableBySlave(int nAxisNo, uint uEnable);
    
	/**
     * @brief ECAM 기능의 사용 유무를 지정한 Master 축에 연결된 모든 Slave 축에 대하여 설정
	 *
     * @param nAxisNo 축 번호
     * @param uEnable ECAM 기능 사용 유무 (0: ECAM 기능 미사용, 1: ECAM 기능 사용)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmEcamEnableByMaster(int nAxisNo, uint uEnable);
    
	/**
     * @brief ECAM 기능의 사용 유무에 대한 설정 상태 반환
	 *
     * @param nAxisNo 축 번호
     * @param upEnable ECAM 기능 사용 유무 (0: ECAM 기능 미사용, 1: ECAM 기능 사용)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmEcamIsSlaveEnable(int nAxisNo, ref uint upEnable);

    // Servo Status Monitor
	
    /**
     * @brief 지정 축의 예외 처리 기능에 대해 설정 (MLII: Sigma-5, SIIIH: MR_J4_xxB 전용)
     * 
     * @param nAxisNo 축 번호
     * @param uSelMon 감시 정보 선택 (0~4)
     * @param dActionValue 이상 동작 판정 기준 값 설정. 각 정보에 따라 설정 값의 의미가 다음.
     * @param uAction dActionValue 이상으로 감시 정보가 확인 되었을 때 예외 처리 방법 설정 (0~3)
     *
	 * @details
	 * uSelMon
	 *    [0] Torque 
	 *    [1] Velocity of motor
	 *    [2] Accel of motor
	 *    [3] Decel of motor
	 *    [4] Position error between Command position and Actual position
	 * dActionValue
	 *    [0]  dwSelMon에서 선택한 감시 정보에 대하여 예외 처리 하지 않음
	 *    [>0] dwSelMon에서 선택한 감시 정보에 대하여 예외 처리 기능 적용
	 * uAction
	 *    [0] Warning(setting flag only)
	 *    [1] Warning(setting flag) + Slow-down stop
	 *    [2] Warning(setting flag) + Emergency stop
	 *    [3] Warning(setting flag) + Emergency stop + Servo-Off
	 *
	 * @note
	 * 주의 사항: 5개의 SelMon 정보에 대해 각각 예외 처리 설정이 가능하며, 사용 중 예외 처리를 원하지 않을 경우
	 *         반드시 해당 SelMon 정보의 ActionValue 값을 0으로 설정해 감시 기능을 Disable 해야함
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusSetServoMonitor(int nAxisNo, uint uSelMon, double dActionValue, uint uAction);
    
	/**
     * @brief 지정 축의 예외 처리 기능에 대해 설정 확인 (MLII: Sigma-5, SIIIH: MR_J4_xxB 전용)
     * 
     * @param nAxisNo 축 번호
     * @param uSelMon 감시 정보 저장 (0~4)
     * @param dpActionValue 이상 동작 판정 기준 값 저장. 각 정보에 따라 설정 값의 의미가 다음.
     * @param upAction dActionValue 이상으로 감시 정보가 확인 되었을 때 예외 처리 방법 저장 (0~3)
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusGetServoMonitor(int nAxisNo, uint uSelMon, ref double dpActionValue, ref uint upAction);
    
	/**
     * @brief 지정 축의 예외 처리 기능에 대한 사용 유무 설정 (MLII: Sigma-5, SIIIH: MR_J4_xxB 전용)
     * 
     * @param nAxisNo 축 번호
     * @param uEnable 사용 유무 (0: 비활성화, 1: 활성화)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusSetServoMonitorEnable(int nAxisNo, uint uEnable);
    
	/**
     * @brief 지정 축의 예외 처리 기능에 대한 사용 유무 확인 (MLII: Sigma-5, SIIIH: MR_J4_xxB 전용)
     * 
     * @param nAxisNo 축 번호
     * @param upEnable 사용 유무 정보 저장 (0: 비활성화, 1: 활성화)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusGetServoMonitorEnable(int nAxisNo, ref uint upEnable);

    /**
     * @brief 지정 축의 예외 처리 기능 실행 결과 플래그 값 반환 (MLII: Sigma-5, SIIIH: MR_J4_xxB 전용)
     * 
     * @param nAxisNo 축 번호
     * @param uSelMon 감시 정보 선택 (0~4)
     * @param upMonitorFlag 모니터 실행 결과 플래그 값 
     * @param dpMonitorValue 모니터 실행 결과 값 
     *
	 * @note 함수 실행 후 자동 초기화
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadServoMonitorFlag(int nAxisNo, uint uSelMon, ref uint upMonitorFlag, ref double dpMonitorValue);
    
	/**
     * @brief 지정 축의 예외 처리 기능 실행 결과 플래그 값 확인 (MLII: Sigma-5, SIIIH: MR_J4_xxB 전용)
     * 
     * @param nAxisNo 축 번호
     * @param uSelMon 감시 정보 저장 (0~4)
     * @param dpMonitorValue 모니터 실행 결과 값 저장
     *
	 * @note 함수 실행 후 자동 초기화
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadServoMonitorValue(int nAxisNo, uint uSelMon, ref double dpMonitorValue);
    
	/**
     * @brief 지정 축의 부하율 읽을 수 있도록 설정 (MLII: Sigma-5, SIIIH: MR_J4_xxB, RTEX: A5N, A6N 전용)
     * 
     * @param nAxisNo 축 번호
     * @param uSelMon 부하율 선택
     *
	 * @note
	 * MLII: Sigma-5 (uSelMon: 0 ~ 2)
	 *    [0] Accumulated load ratio(%)
	 *    [1] Regenerative load ratio(%)
	 *    [2] Reference Torque load ratio
	 *    [3] Motor rotation speed (rpm)
	 * SIIIH: MR_J4_xxB (uSelMon: 0 ~ 4)
	 *    [0] Assumed load inertia ratio(0.1times)
	 *    [1] Regeneration load factor(%)
	 *    [2] Effective load factor(%)
	 *    [3] Peak load factor(%)
	 *    [4] Current feedback(0.1%)	
	 *    [5] Speed feedback(rpm)
	 * RTEX: A5Nx, A6Nx (uSelMon: 0 ~ 4)
	 *    [0] Command Torque(0.1%)
	 *    [1] Regenerative load ratio(0.1%)
	 *    [2] Overload ratio(0.1%)
	 *    [3] Inertia ratio(%)
	 *    [4] Actual speed(rpm)
	 * ECAT (uSelMon: 2)
	 *    [2] Actual Torque(0.1%)
	 *    
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusSetReadServoLoadRatio(int nAxisNo, uint uSelMon);
    
	/**
     * @brief 지정 축의 부하율 정보 확인 (MLII: Sigma-5, SIIIH: MR_J4_xxB, RTEX: A5N, A6N 전용)
     * 
     * @param nAxisNo 축 번호
     * @param dpMonitorValue 부하율 값 저장
	 *    
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadServoLoadRatio(int nAxisNo, ref double dpMonitorValue);

    // PCI-R1604-RTEX 전용 함수
	
    /**
     * @brief RTEX A4Nx 관련 Scale Coefficient 설정 (RTEX: A4Nx 전용)
     * 
     * @param nAxisNo 축 번호
     * @param nScaleCoeff Scale Coefficient 값
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetScaleCoeff(int nAxisNo, int nScaleCoeff);
    
	/**
     * @brief RTEX A4Nx 관련 Scale Coefficient 값 확인 (RTEX: A4Nx 전용)
     * 
     * @param nAxisNo 축 번호
     * @param npScaleCoeff Scale Coefficient 값 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetScaleCoeff(int nAxisNo, ref int npScaleCoeff);
    /**
     * @brief 지정 축에 설정된 신호 검출
     *
     * @param nAxisNo 축 번호
     * @param dVel 구동 속도 설정 (양수: CW, 음수: CCW 방향으로 구동)
     * @param dAccel 구동 가속도 설정
     * @param dDecel 구동 감속도 설정
     * @param nDetectSignal edge 검출 할 입력 신호 선택
     * @param nSignalEdge 선택한 입력 신호의 edge 방향 선택 (rising or falling edge)
	 * @param nSignalMethod 정지 모드 설정
	 *
	 * @details
	 * nDetectSignal
	 *    PosEndLimit(0), NegEndLimit(1), HomeSensor(4), EncodZPhase(5), UniInput02(6), UniInput03(7)
	 * nSignalEdge
	 *    SIGNAL_DOWN_EDGE(0), SIGNAL_UP_EDGE(1)
	 * nSignalMethod
	 *    급정지 EMERGENCY_STOP(0), 감속정지 SLOWDOWN_STOP(1)
	 *
	 * @note
	 * 주의 사항: nSignalMethod EMERGENCY_STOP(0)로 사용할경우 가감속이 무시되며 지정된 속도로 가속 급정지하게 됨
	 *         PCI-Nx04: lDetectSignal이 PosEndLimit , NegEndLimit(0,1)을 찾을 경우 신호의 레벨 Active 상태를 검출하게 됨
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveSignalSearchEx(int nAxisNo, double dVel, double dAccel, int nDetectSignal, int nSignalEdge, int nSignalMethod);

    // PCI-R1604-MLII/SIIIH, PCIe-Rxx04-SIIIH 전용 함수
	
    /**
     * @brief 설정한 절대 위치 이동
     * 
     * @param nAxisNo 축 번호
     * @param dPos 목표 위치
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
	 *
	 * @note
	 * 속도 프로파일: 사다리꼴 전용 구동
	 * 펄스 출력 시점에 함수 벗어 남
	 * 항상 위치 및 속도, 가감속도 변경 가능하며 반대 방향 위치 변경 기능 포함
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveToAbsPos(int nAxisNo, double dPos, double dVel, double dAccel, double dDecel);
    
	/**
     * @brief 지정 축의 현재 구동 속도 확인
     *
     * @param nAxisNo 축 번호
     * @param dpVel 현재 구동 속도 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadVelEx(int nAxisNo, ref double dpVel);

    // PCI-R1604-SIIIH, PCIe-Rxx04-SIIIH 전용 함수

    /**
     * @brief 지정 축의 전자 기어비 설정. 설정 후 비 휘발성 메모리에 저장
     * 
     * @param nAxisNo 축 번호
     * @param nNumerator 전자 기어비 분자 값
     * @param nDenominator 전자 기어비 분모 값
	 * 
     * @note
	 * 초기 값 (nNumerator: 4194304(2^22), lDenominator: 10000)
	 * MR-J4-B는 전자 기어비를 설정할 수 없으며 상위 제어기에서 아래의 함수를 사용하여 설정해야 함
	 * 기존 펄스 입력 방식 서보 드라이버(MR-J4-A)의 파라미터 No.PA06, No.PA07에 해당
	 * Ex1) 1um를 제어 단위로 가정. 감속기 비율: 1/1. Rotary motor를 장착한 Linear stage
	 *    Encoder resulotion: 2^22
	 *    Ball screw pitch: 6 mm 일 경우
	 *    ==> nNumerator: 2^22, lDenominator: 6000 (6/0.001)
	 *    AxmMotSetMoveUnitPerPulse에서 Unit/Pulse = 1/1로 설정 했다면, 모든 함수의 위치 단위: um, 속도 단위: um/sec, 가감속도 단위: um/sec^2 됨
	 *    AxmMotSetMoveUnitPerPulse에서 Unit/Pulse = 1/1000로 설정 했다면, 모든 함수의 위치 단위: mm, 속도 단위: mm/sec, 가감속도 단위: mm/sec^2 됨
	 * Ex2) 0.01도 회전을 제어 단위로 가정. 감속기 비율: 1/1. Rotary motor를 장착한 회전체 구조물
	 *    Encoder resulotion: 2^22
	 *    1 회전: 360 일 경우
	 *    ==> nNumerator: 2^22, lDenominator: 36000 (360/0.01)
	 *    AxmMotSetMoveUnitPerPulse에서 Unit/Pulse = 1/1로 설정 했다면, 모든 함수의 위치 단위: 0.01도, 속도 단위: 0.01도/sec, 가감속도 단위: 0.01도/sec^2 됨
	 *    AxmMotSetMoveUnitPerPulse에서 Unit/Pulse = 1/1000로 설정 했다면, 모든 함수의 위치 단위: 1도, 속도 단위: 1도/sec, 가감속도 단위: 1도/sec^2 됨
	 *
     * 주의사항(PCIe-Rxx05-SIIIH 절대치 엔코더 사용시)
     * Feed back Position * 전자기어비 = Actual Position 으로 출력되며, 전자기어비를 10000 : 4194304로 설정한 경우
     * Servo Driver에서 피드백되는 Actual Position이 2147483648(2^31)에서 한펄스 증가 시 ((2147483648 + 1) * 10000 / 4194304 = 5120001 이 되지않고,
     * 오버플로우가 발생하여 아래와 같이 Actual Position이 반환된다. (-2147483647 * 10000) / 4194304 = -5120000
     * 해당 상황에서 서보드라이버 전원 On/Off 또는 보드 전원 On/Off 시 -5120000의 값이 Actual Position이 출력되는데,
     * 출력되는 Actual Position이 실제 위치가 -5120000 위치이기 때문인지, 오버플로우로 인해 출력되는 것인지 확인할 수 없기때문에 주의하여야한다.
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetElectricGearRatio(int nAxisNo, int nNumerator, int nDenominator);
    
	/**
     * @brief 지정 축의 전자 기어비 설정 값 확인
     * 
     * @param nAxisNo 축 번호
     * @param npNumerator 전자 기어비 분자 값 저장
     * @param npDenominator 전자 기어비 분모 값 저장
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetElectricGearRatio(int nAxisNo, ref int npNumerator, ref int npDenominator);

    /**
     * @brief 지정 축의 토크 리미트 값 설정
     * 
     * @param nAxisNo 축 번호
     * @param dbPlusDirTorqueLimit 정방향 토크 제한 값
     * @param dbMinusDirTorqueLimit 역방향 토크 제한 값 
	 *
	 * @note 
	 * 정방향, 역방향 구동 시 토크 값을 제한하는 함수
	 * SSCNET
	 *    설정 값은:1 ~ 3000 (0.1 ~ 300.0%)
	 *    최대 토크의 0.1% 단위로 제어
	 * RTEX
	 *    설정 값: 1 ~ 500 (1 ~ 500%)
	 *    최대 토크의 1% 단위로 제어
	 *    Torque Limit 기능을 사용할 축의 Servo Drive Parameter Pr5.21(토크 한계 선택 설정)을 4로 변경 후 사용해야 함
	 * ML-III
	 *    설정 값: 0 ~ 800 (0 ~ 800%)
	 *    Rotary Servo 앰프 모드만 지원
	 *    PCI-Rxx00-MLIII 제품만 지원
	 *    단위는 1%로 제어
	 *    PlusDirTorqueLimit(Forwared Torque Limit)는 Servo Drive Parameter Pn402 설정
	 *    MinusDirTorqueLimit(Reverse Torque Limit)는 Servo Drive Parameter Pn403 설정
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetTorqueLimit(int nAxisNo, double dbPlusDirTorqueLimit, double dbMinusDirTorqueLimit);

    /**
     * @brief 지정 축의 토크 리미트 값 확인
     * 
     * @param nAxisNo 축 번호
     * @param dbpPlusDirTorqueLimit 정방향 토크 제한 값 저장
     * @param dbpMinusDirTorqueLimit 역방향 토크 제한 값 저장
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetTorqueLimit(int nAxisNo, ref double dbpPlusDirTorqueLimit, ref double dbpMinusDirTorqueLimit);

    /**
     * @brief 지정 축의 토크 리미트 값 설정
     *
     * @param nAxisNo 축 번호
     * @param dbPlusDirTorqueLimit 정방향 토크 리미트 값
     * @param dbMinusDirTorqueLimit 역방향 토크 리미트 값
	 *
	 * @note 아래 표시된 제품만 해당 기능 지원
	 * ML-III
	 *    설정 값: 0 ~ 800 (0 ~ 800%)
	 *    Liner Servo 앰프 모드만 지원(Only SGD7S, SGD7W)
	 *    PCI-Rxx00-MLIII 제품만 지원
	 *    단위는 1%로 제어
	 *    PlusDirTorqueLimit(Forwared Torque Limit)는 Servo Drive Parameter Pn483 설정
	 *    MinusDirTorqueLimit(Reverse Torque Limit)는 Servo Drive Parameter Pn484 설정
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetTorqueLimitEx(int nAxisNo, double dbPlusDirTorqueLimit, double dbMinusDirTorqueLimit);

    /**
     * @brief 지정 축의 토크 리미트 값 확인
     *
     * @param nAxisNo 축 번호
     * @param dbpPlusDirTorqueLimit 정방향 토크 리미트 값 저장
     * @param dbpMinusDirTorqueLimit 역방향 토크 리미트 값 저장
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetTorqueLimitEx(int nAxisNo, ref double dbpPlusDirTorqueLimit, ref double dbpMinusDirTorqueLimit);

    /**
     * @brief 지정 축의 토크 리미트 값 설정
     *
     * @param nAxisNo 축 번호
     * @param dbPlusDirTorqueLimit 정방향 토크 리미트 값
     * @param dbMinusDirTorqueLimit 역방향 토크 리미트 값
     * @param dPosition 토크 리미트 값 변경 할 위치 정보 (해당 위치 정보와 대상 위치가 같은 이벤트 발생 시 적용)
     * @param nTarget 위치 정보의 대상 (0: COMMAND, 1: ACTUAL)
     * 
	 * @note 
	 * 정방향, 역방향 구동 시의 토크 값 제한 함수
	 * 설정 값: 1 ~ 3000 (0.1 ~ 300.0%)
	 * 최대 토크의 0.1% 단위로 제어
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetTorqueLimitAtPos(int nAxisNo, double dbPlusDirTorqueLimit, double dbMinusDirTorqueLimit, double dPosition, int nTarget);

    /**
     * @brief 지정 축의 토크 리미트 값 확인
     *
     * @param nAxisNo 축 번호
     * @param dbpPlusDirTorqueLimit 정방향 토크 리미트 값 저장
     * @param dbpMinusDirTorqueLimit 역방향 토크 리미트 값 저장
     * @param dpPosition 토크 리미트 값 변경 할 위치 정보 저장 (해당 위치 정보와 대상 위치가 같은 이벤트 발생 시 적용)
     * @param npTarget 위치 정보의 대상 값 저장 (0: COMMAND, 1: ACTUAL)
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetTorqueLimitAtPos(int nAxisNo, ref double dbpPlusDirTorqueLimit, ref double dbpMinusDirTorqueLimit, ref double dpPosition, ref int npTarget);

    /**
     * @brief 토크 리미트 기능 사용 여부 설정 (PCI-R1604 RTEX 전용 함수)
     * 
     * @param nAxisNo 축 번호
     * @param uUse 토크 리미트 기능 사용 여부 설정 (0: Disable, 1: Enable)
	 *
	 * @note PCI-R1604의 경우 토크 리미트 값을 설정하고 기능을 Enable 해야 토크 리미트 기능이 동작 함
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetTorqueLimitEnable(int nAxisNo, uint uUse);

    /**
     * @brief 토크 리미트 기능 사용 여부 설정 확인 (PCI-R1604 RTEX 전용 함수)
     * 
     * @param nAxisNo 축 번호
     * @param upUse 토크 리미트 기능 사용 여부 설정 값 확인 (0: Disable, 1: Enable)
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetTorqueLimitEnable(int nAxisNo, ref uint upUse);

    /**
     * @brief 지정 축 AxmOverridePos 함수에 대한 위치 판단 특수 기능 사용 유무 설정
     * 
     * @param nAxisNo 축 번호
     * @param uUsage 특수 기능 사용 유무 (0: DISABLE, 1: ENABLE)
     * @param nDecelPosRatio 감속 거리에 대한 %값
     * @param dReserved 예약된 매개 변수 (미사용)
     *
	 * @note
	 * uUsage 값 enable 시 AxmMoveStartPos 설정한 구동 중 위치 변경 가능 위치를 감속 거리의 nDecelPosRatio(%)을 기준으로 판단 함
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmOverridePosSetFunction(int nAxisNo, uint uUsage, int nDecelPosRatio, double dReserved);
    
	/**
     * @brief 지정 축 AxmOverridePos 함수에 대한 위치 판단 특수 기능 사용 유무 설정 값 확인
     * 
     * @param nAxisNo 축 번호
     * @param upUsage 특수 기능 사용 유무 값 저장 (0: DISABLE, 1: ENABLE)
     * @param npDecelPosRatio 감속 거리에 대한 %값 저장
     * @param dpReserved 예약된 매개 변수 값 저장 (미사용)
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmOverridePosGetFunction(int nAxisNo, ref uint upUsage, ref int npDecelPosRatio, ref double dpReserved);

    /**
     * @brief 지정 축의 특정 위치에서 설정한 디지털 출력 값 제어
     *
     * @param nAxisNo 축 번호
     * @param nModuleNo 모듈 번호
     * @param nOffset 출력 접점에 대한 Offset 위치
     * @param uValue 출력 값 설정 (0: Off, 1: On, 0xFF: Function Clear)
     * @param dPosition DO 출력 값 변경할 위치 정보 (해당 위치 정보와 대상 위치가 같아지는 이벤트 발생 시 실행 됨)
     * @param nTarget 위치 정보의 대상 (0: COMMAND, 1: ACTUAL)
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetWriteOutputBitAtPos(int nAxisNo, int nModuleNo, int nOffset, uint uValue, double dPosition, int nTarget);
    
	/**
     * @brief 지정 축의 특정 위치에서 설정한 디지털 출력 값 확인
     *
     * @param nAxisNo 축 번호
     * @param npModuleNo 모듈 번호 저장
     * @param npOffset 출력 접점에 대한 Offset 위치 값 저장
     * @param upValue 출력 값 저장 (0: Off, 1: On, 0xFF: Function Clear)
     * @param dpPosition DO 출력 값 변경할 위치 값 저장 (해당 위치 정보와 대상 위치가 같아지는 이벤트 발생 시 실행 됨)
     * @param npTarget 위치 정보의 대상 값 저장 (0: COMMAND, 1: ACTUAL)
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetWriteOutputBitAtPos(int nAxisNo, ref int npModuleNo, ref int npOffset, ref uint upValue, ref double dpPosition, ref int npTarget);

    // PCI-R3200-MLIII 전용 함수
	
    /**
     * @brief 잔류 진동 억제(VST)를 위한 파라미터 설정
     *
     * @param nCoord 입력 성형 적용 코디 번호
     * @param uISTSize 입력 성형 사용 주파수 개수 (1로 값을 고정 사용)
     * @param dbpFrequency 주파수 (10 ~ 500Hz, 1차 주파수부터 순서대로 입력, 저주파->고주파)
     * @param dbpDampingRatio 감쇠 비율 (0.001 ~ 0.9)
     * @param upImpulseCount 임펄스 개수 (2 ~ 5)
	 *
	 * @details
	 * nCoord
	 *    각 보드 별 첫 번째부터 10번째의 코디에 축을 할당해서 사용
	 *    ML-III 마스터 보드는 보드 번호를 기준으로 16 ~ 31까지 보드 별 순차적으로 16씩 증가 됨
	 *    ML-III B/D 0: 16 ~ 31
	 *    ML-III B/D 1: 31 ~ 47
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvVSTSetParameter(int nCoord, uint uISTSize, double dbpFrequency, double dbpDampingRatio, ref uint upImpulseCount);
	
	/**
     * @brief 잔류 진동 억제(VST)를 위한 파라미터 설정 값 확인
     *
     * @param nCoord 입력 성형 적용 코디 번호
     * @param upISTSize 입력 성형 사용 주파수 개수 저장 (1로 값을 고정 사용)
     * @param dbpFrequency 주파수 저장 (10 ~ 500Hz, 1차 주파수부터 순서대로 입력, 저주파->고주파)
     * @param dbpDampingRatio 감쇠 비율 저장 (0.001 ~ 0.9)
     * @param upImpulseCount 임펄스 개수 저장 (2 ~ 5)
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvVSTGetParameter(int nCoord, ref uint upISTSize, ref double dbpFrequency, ref double dbpDampingRatio, ref uint upImpulseCount);
    
	/**
     * @brief 잔류 진동 억제(VST) 기능 사용 유무 설정
     *
     * @param nCoord 입력 성형 코디 번호
     * @param uISTEnable 입력 성형 사용 유무 값 (0: DISABLE, 1: ENABLE)
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvVSTSetEnabele(int nCoord, uint uISTEnable);
	
	/**
     * @brief 잔류 진동 억제(VST) 기능 사용 유무 확인
     *
     * @param nCoord 입력 성형 코디 번호
     * @param upISTEnable 입력 성형 사용 유무 값 확인 (0: DISABLE, 1: ENABLE)
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvVSTGetEnabele(int nCoord, ref uint upISTEnable);

    // 일반 보간 함수 (PCI-Rxx00-MLIII 전용 함수)
	
    /**
     * @brief 시작점과 종료점 지정하여 다축 직선 보간 구동
     * 
     * @param nCoord 좌표계 번호
     * @param dpPosition 위치 배열
     * @param dMaxVelocity 최대 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dMaxAccel 최대 가속도
     * @param dMaxDecel 최대 감속도
	 *
	 * @note
	 * 구동 시작 후 함수를 벗어남
	 * AxmAdvContiBeginNode, AxmAdvContiEndNode와 같이 사용 시 지정된 좌표계에 시작점과 종료점을 지정하여 직선 보간 구동하는 Queue에 함수 저장
	 * 직선 프로파일 연속 보간 구동을 위해 내부 Queue에 저장하여 AxmAdvContiStart 함수를 사용하여 시작
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvLineMove(int nCoord, double[] dpPosition, double dMaxVelocity, double dStartVel, double dStopVel, double dMaxAccel, double dMaxDecel);
    
	/**
     * @brief 지정된 좌표계에 시작점과 종료점 지정하여 다축 직선 보간 오버라이드
     * 
     * @param nCoord 좌표계 번호
     * @param dpPosition 위치 배열
     * @param dMaxVelocity 최대 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dMaxAccel 최대 가속도
     * @param dMaxDecel 최대 감속도
     * @param nOverrideMode 오버라이드 모드 (0: 즉시 오버라이드, 1: 예약 오버라이드)
	 *
	 * @details
	 * lOverrideMode
	 *    [0] 즉시 오버라이드: 구동 중인 보간이 직선, 원호 보간에 관계없이 현재 구동 노드에서 직선 보간으로 즉시 오버라이드 됨
	 *    [1] 예약 오버라이드:
	 *        현재 구동 노드 다음의 노드부터 직선 보간이 차례로 예약 됨.
	 *        1로 본 함수를 호출 할때마다 최소 1개에서 최대 8개까지 오버라이드 큐에 증가되면서 자동적으로 예약이 됨.
	 *        예약 후 마지막에 IOverrideMode = 0으로 본 함수가 호출되면 내부적으로 오버라이드 큐에 있는 예약 보간들이 연속 보간 큐로 저장되고
	 *        직선 오버라이드 구동과 이후의 예약된 연속 보간이 순차적으로 실행 됨.
     *
	 * @note 현재 진행중인 보간 구동을 지정된 속도 및 위치로 직선 보간 오버라이드 하며 다음 노드에 대한 직선 보간 구동 예약도 가능함
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvOvrLineMove(int nCoord, double[] dpPosition, double dMaxVelocity, double dStartVel, double dStopVel, double dMaxAccel, double dMaxDecel, int nOverrideMode);
    
	/**
     * @brief 시작점, 종료점, 중심점 지정하여 2축 원호 보간 구동
     * 
     * @param nCoord 좌표계 번호
     * @param nAxisNo 두 축 배열
     * @param dpCenterPos 중심점 X, Y 배열
     * @param dpEndPos 종료점 X, Y 배열
     * @param dVel 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계 방향), 1: DIR_CW(시계 방향))
     *
	 * @note 
	 * 구동 시작 후 함수를 벗어남
	 * AxmAdvContiBeginNode, AxmAdvContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 종료점과 중심점을 지정하여 원호 보간 Queue에 함수 저장
	 * 프로파일 원호 연속 보간 구동을 위해 내부 Queue에 저장하여 AxmAdvContiStart 함수를 사용하여 시작
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvCircleCenterMove(int nCoord, int[] nAxisNo, double[] dpCenterPos, double[] dpEndPos, double dVel, double dStartVel, double dStopVel, double dAccel, double dDecel, uint uCWDir);
    
	/**
     * @brief 중간점, 종료점 지정하여 원호 보간 구동
     *
     * @param nCoord 좌표계 번호
     * @param nAxisNo 보간에 사용되는 두 축 배열
     * @param dpMidPos 중간점 X, Y 배열
     * @param dpEndPos 종료점 X, Y 배열
     * @param dVel 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param nArcCircle 원호 종류 (0: 아크, 1: 원)
     *
	 * @note
	 * 중간점, 종료점을 지정하여 원호 보간 구동하는 함수. 구동 시작 후 함수를 벗어 남
	 * AxmAdvContiBeginNode, AxmAdvContiEndNode와 같이 사용 시 지정된 좌표계에 중간점, 종료점을 지정하여 구동하는 원호 보간 Queue에 함수 저장
	 * 프로파일 원호 연속 보간 구동을 위해 내부 Queue에 저장하여 AxmAdvContiStart 함수를 사용해서 시작
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvCirclePointMove(int nCoord, int[] nAxisNo, double[] dpMidPos, double[] dpEndPos, double dVel, double dStartVel, double dStopVel, double dAccel, double dDecel, int nArcCircle);
    
	/**
     * @brief 시작점, 회전각도와 반지름 지정하여 원호 보간 구동
     *
     * @param nCoord 좌표계 번호
     * @param nAxisNo 보간에 사용되는 두 축 배열
     * @param dpCenterPos 중심점 X, Y 배열
     * @param dAngle 각도
     * @param dVel 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
     *
	 * @note
	 * 구동 시작 후 함수를 벗어남
	 * AxmAdvContiBeginNode, AxmAdvContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 회전 각도와 반지름을 지정하여 구동하는 원호 보간 Queue에 함수 저장
	 * 프로파일 원호 연속 보간 구동을 위해 내부 Queue에 저장하여 AxmAdvContiStart함수를 함수를 사용해서 시작
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvCircleAngleMove(int nCoord, int[] nAxisNo, double[] dpCenterPos, double dAngle, double dVel, double dStartVel, double dStopVel, double dAccel, double dDecel, uint uCWDir);
    
	/**
     * @brief 시작점, 종료점과 반지름 지정하여 원호 보간 구동
     *
     * @param nCoord 좌표계 번호
     * @param nAxisNo 보간에 사용되는 두 축 배열
     * @param dRadius 반지름
     * @param dpEndPos 종료점 X, Y 배열
     * @param dVel 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
     * @param uShortDistance 중점까지 가는 원의 이동 거리 크기 설정 값 (0: 작은원, 1: 큰원)
     *
	 * @note
	 * 구동 시작 후 함수를 벗어남
	 * AxmAdvContiBeginNode, AxmAdvContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 종료점과 반지름을 지정하여 구동하는 원호 보간 Queue에 함수 저장
	 * 프로파일 원호 연속 보간 구동을 위해 내부 Queue에 저장하여 AxmAdvContiStart 함수를 사용해서 시작
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvCircleRadiusMove(int nCoord, int[] nAxisNo, double dRadius, double[] dpEndPos, double dVel, double dStartVel, double dStopVel, double dAccel, double dDecel, uint uCWDir, uint uShortDistance);
    
	/**
     * @brief 지정된 좌표계에 시작점과 종료점 지정하여 2축 원호 보간 오버라이드 구동
     * 
     * @param nCoord 좌표계 번호
     * @param nAxisNo 축 번호 배열
     * @param dRadius 반지름
     * @param dpEndPos 종료점 X, Y 배열
     * @param dVel 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
     * @param uShortDistance 중점까지 가는 원의 이동 거리 크기 설정 값 (0: 작은원, 1: 큰원)
     * @param nOverrideMode 오버라이드 모드 (0: 즉시 오버라이드, 1: 예약 오버라이드)
     *
	 * @details
	 * nOverrideMode
	 *    [0] 즉시 오버라이드: 구동 중인 보간이 직선, 원호 보간에 관계없이 현재 구동 노드에서 원호 보간으로 즉시 오버라이드 됨
	 *    [1] 예약 오버라이드:
	 *        현재 구동 노드 다음의 노드부터 원호 보간이 차례로 예약 됨.
	 *        1로 본 함수를 호출 할때마다 최소 1개에서 최대 8개까지 오버라이드 큐에 증가되면서 자동적으로 예약이 됨.
	 *        예약 후 마지막에 nOverrideMode = 0으로 본 함수가 호출되면 내부적으로 오버라이드 큐에 있는 예약 보간들이 연속 보간 큐로 저장되고
	 *        원호 오버라이드 구동과 이후의 예약된 연속 보간이 순차적으로 실행 됨.
     *
	 * @note 현재 진행중인 보간 구동을 지정된 속도 및 위치로 원호 보간 오버라이드 하며 다음 노드에 대한 원호 보간 구동 예약도 가능함
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvOvrCircleRadiusMove(int nCoord, int[] nAxisNo, double dRadius, double[] dpEndPos, double dVel, double dStartVel, double dStopVel, double dAccel, double dDecel, uint uCWDir, uint uShortDistance, int nOverrideMode);

    // 헬리컬 이동

    /**
     * @brief 지정된 좌표계에 시작점, 종료점과 중심점을 지정하여 헬리컬 보간 구동
     *
     * @param nCoord 좌표계 번호
     * @param dCenterXPos 중심점 X 위치
     * @param dCenterYPos 중심점 Y 위치
     * @param dEndXPos 종료점 X 위치
     * @param dEndYPos 종료점 Y 위치
     * @param dZPos Z 위치
     * @param dVel 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
     *
	 * @note
	 * AxmAdvContiBeginNode, AxmAdvContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 종료점과 중심점을 지정하여 헬리컬 연속 보간 구동하는 함수
	 * 원호 연속 보간 구동을 위해 내부 Queue에 저장하는 함수. AxmAdvContiStart 함수를 사용해서 시작 (연속 보간 함수와 같이 이용)
	 * 주의 사항: Helix를 연속 보간 사용 시 Spline, 직선 보간과 원호 보간을 같이 사용할 수 없음
	 *
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvHelixCenterMove(int nCoord, double dCenterXPos, double dCenterYPos, double dEndXPos, double dEndYPos, double dZPos, double dVel, double dStartVel, double dStopVel, double dAccel, double dDecel, uint uCWDir);

    /**
     * @brief 지정된 좌표계에 중간점, 종료점을 지정하여 헬리컬 보간 구동
     *
     * @param nCoord 좌표계 번호
     * @param dMidXPos 중간점 X 위치
     * @param dMidYPos 중간점 Y 위치
     * @param dEndXPos 종료점 X 위치
     * @param dEndYPos 종료점 Y 위치
     * @param dZPos Z 위치
     * @param dVel 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     *
	 * @note
	 * AxmContiBeginNode, AxmContiEndNode와 같이 사용 시 지정된 좌표계에 중간점, 종료점을 지정하여 헬리컬 연속 보간 구동하는 함수
	 * 원호 연속 보간 구동을 위해 내부 Queue에 저장하는 함수. AxmContiStart 함수를 사용해서 시작 (연속 보간 함수와 같이 이용)
	 *
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvHelixPointMove(int nCoord, double dMidXPos, double dMidYPos, double dEndXPos, double dEndYPos, double dZPos, double dVel, double dStartVel, double dStopVel, double dAccel, double dDecel);

    /**
     * @brief 주어진 좌표계에 시작점, 회전 각도와 반지름 지정하여 헬리컬 보간 구동
     *
     * @param nCoord 좌표계 번호
     * @param dCenterXPos 중심점 X 위치
     * @param dCenterYPos 중심점 Y 위치
     * @param dAngle 각도
     * @param dZPos Z 위치
     * @param dVel 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
     *
	 * @note
	 * AxmAdvContiBeginNode, AxmAdvContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 회전 각도와 반지름을 지정하여 헬리컬 연속 보간 구동하는 함수
	 * 원호 연속 보간 구동을 위해 내부 Queue에 저장하는 함수. AxmAdvContiStart 함수를 사용해서 시작 (연속 보간 함수와 같이 이용)
	 *
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvHelixAngleMove(int nCoord, double dCenterXPos, double dCenterYPos, double dAngle, double dZPos, double dVel, double dStartVel, double dStopVel, double dAccel, double dDecel, uint uCWDir);

    /**
     * @brief 지정된 좌표계에 시작점, 종료점과 반지름을 지정하여 헬리컬 보간 구동
     *
     * @param nCoord 좌표계 번호
     * @param dRadius 반지름
     * @param dEndXPos 종료점 X 위치
     * @param dEndYPos 종료점 Y 위치
     * @param dZPos Z 위치
     * @param dVel 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
     * @param uShortDistance 중점까지 가는 원의 이동 거리 크기 설정 (0: 작은원, 1: 큰원)
     *
	 * @note
	 * AxmAdvContiBeginNode, AxmAdvContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 종료점과 반지름을 지정하여 헬리컬 연속 보간 구동하는 함수
	 * 원호 연속 보간 구동을 위해 내부 Queue에 저장하는 함수. AxmAdvContiStart 함수를 사용해서 시작 (연속 보간 함수와 같이 이용)
	 *
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvHelixRadiusMove(int nCoord, double dRadius, double dEndXPos, double dEndYPos, double dZPos, double dVel, double dStartVel, double dStopVel, double dAccel, double dDecel, uint uCWDir, uint uShortDistance);

    /**
     * @brief 지정된 좌표계에 시작점, 종료점과 반지름을 지정하여 3축 헬리컬 보간 오버라이드 구동
     *
     * @param nCoord 좌표계 번호
     * @param dRadius 반지름
     * @param dEndXPos 종료점 X 위치
     * @param dEndYPos 종료점 Y 위치
     * @param dZPos Z 위치
     * @param dVel 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
     * @param uShortDistance 중점까지 가는 원의 이동 거리 크기 설정 (0: 작은원, 1: 큰원)
	 * @param nOverrideMode 오버라이드 모드 (0: 즉시 오버라이드, 1: 예약 오버라이드)
     *
	 * @details
	 * nOverrideMode
	 *    [0] 즉시 오버라이드: 구동 중인 보간이 직선, 원호 보간에 관계없이 현재 구동 노드에서 헬리컬 보간으로 즉시 오버라이드 됨
	 *    [1] 예약 오버라이드:
	 *        현재 구동 노드 다음의 노드부터 헬리컬 보간이 차례로 예약 됨.
	 *        1로 본 함수를 호출 할때마다 최소 1개에서 최대 8개까지 오버라이드 큐에 증가되면서 자동적으로 예약이 됨.
	 *        예약 후 마지막에 nOverrideMode = 0으로 본 함수가 호출되면 내부적으로 오버라이드 큐에 있는 예약 보간들이 연속 보간 큐로 저장되고
	 *        헬리컬 오버라이드 구동과 이후의 예약된 연속 보간이 순차적으로 실행 됨.
     *
	 * @note 현재 진행중인 보간 구동을 지정된 속도 및 위치로 헬리컬 보간 오버라이드 하며 다음 노드에 대한 헬리컬 보간 구동 예약도 가능함
	 *
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvOvrHelixRadiusMove(int nCoord, double dRadius, double dEndXPos, double dEndYPos, double dZPos, double dVel, double dStartVel, double dStopVel, double dAccel, double dDecel, uint uCWDir, uint uShortDistance, int nOverrideMode);

    // 일반 보간 함수
    
	/**
     * @brief 시작점과 종료점 지정하여 다축 직선 보간 예약 구동
     * @note 아직 메뉴얼에 추가되지 않은 함수
     * @param nCoord 좌표계 번호
     * @param dpPosition 위치 배열
     * @param dMaxVelocity 최대 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dMaxAccel 최대 가속도
     * @param dMaxDecel 최대 감속도
     * @param uScript 스크립트 번호
     * @param nScriptAxisNo 스크립트 축 번호
     * @param dScriptPos 스크립트 위치
     *
	 * @note
	 * 구동 시작 후 함수를 벗어남
	 * AxmAdvContiBeginNode, AxmAdvContiEndNode와 같이 사용 시 지정된 좌표계에 시작점과 종료점을 지정하여 직선 보간 구동하는 Queue에 함수 저장
	 * 직선 프로파일 연속 보간 구동을 위해 내부 Queue에 저장하여 AxmAdvContiStart 함수를 사용해서 시작
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvScriptLineMove(int nCoord, double[] dpPosition, double dMaxVelocity, double dStartVel, double dStopVel, double dMaxAccel, double dMaxDecel, uint uScript, int nScriptAxisNo, double dScriptPos);
    
	/**
     * @brief 현재 진행 중인 보간 구동 지정된 속도 및 위치로 직선 보간 오버라이드 예약 구동
     * 
     * @param nCoord 좌표계 번호
     * @param dpPosition 위치 배열
     * @param dMaxVelocity 최대 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dMaxAccel 최대 가속도
     * @param dMaxDecel 최대 감속도
     * @param nOverrideMode 오버라이드 모드 (0: 즉시 오버라이드, 1: 예약 오버라이드)
     * @param uScript 스크립트 번호
     * @param nScriptAxisNo 스크립트 축 번호
     * @param dScriptPos 스크립트 위치
     *
	 * @details
	 * nOverrideMode
	 *    [0] 즉시 오버라이드: 구동 중인 보간이 직선, 원호 보간에 관계없이 현재 구동 노드에서 직선 보간으로 즉시 오버라이드 됨
	 *    [1] 예약 오버라이드:
	 *        현재 구동 노드 다음의 노드부터 직선 보간이 차례로 예약 됨.
	 *        1로 본 함수를 호출 할때마다 최소 1개에서 최대 8개까지 오버라이드 큐에 증가되면서 자동적으로 예약이 됨.
	 *        예약 후 마지막에 nOverrideMode = 0으로 본 함수가 호출되면 내부적으로 오버라이드 큐에 있는 예약 보간들이 연속 보간 큐로 저장되고
	 *        직선 오버라이드 구동과 이후의 예약된 연속 보간이 순차적으로 실행 됨.
     *
	 * @note 현재 진행중인 보간 구동을 지정된 속도 및 위치로 직선 보간 오버라이드 하며 다음 노드에 대한 직선 보간 구동 예약도 가능함
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvScriptOvrLineMove(int nCoord, double[] dpPosition, double dMaxVelocity, double dStartVel, double dStopVel, double dMaxAccel, double dMaxDecel, int nOverrideMode, uint uScript, int nScriptAxisNo, double dScriptPos);
    
	/**
     * @brief 시작점, 종료점과 중심점 지정하여 원호 보간 예약 구동
     * 
     * @param nCoord 좌표계 번호
     * @param npAxisNo 두 축 배열
     * @param dpCenterPos 중심점 X, Y 배열
     * @param dpEndPos 종료점 X, Y 배열
     * @param dVel 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
     * @param uScript 스크립트 번호
     * @param nScriptAxisNo 스크립트 축 번호
     * @param dScriptPos 스크립트 위치
     *
	 * @note
	 * 구동 시작 후 함수를 벗어남
	 * AxmAdvContiBeginNode, AxmAdvContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 종료점과 중심점을 지정하여 구동하는 원호 보간 Queue에 함수 저장
	 * 직선 프로파일 원호 연속 보간 구동을 위해 내부 Queue에 저장하여 AxmAdvContiStart 함수를 사용해서 시작
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvScriptCircleCenterMove(int nCoord, int[] npAxisNo, double[] dpCenterPos, double[] dpEndPos, double dVel, double dStartVel, double dStopVel, double dAccel, double dDecel, uint uCWDir, uint uScript, int nScriptAxisNo, double dScriptPos);
    
	/**
     * @brief 중간점과 종료점 지정하여 원호 보간 예약 구동
     * 
     * @param nCoord 좌표계 번호
     * @param nAxisNo 두 축 배열
     * @param dpMidPos 중간점 X, Y 배열
     * @param dpEndPos 종료점 X, Y 배열
     * @param dVel 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param nArcCircle 원호 종류 (0: 아크, 1: 원)
     * @param uScript 스크립트 번호
     * @param nScriptAxisNo 스크립트 축 번호
     * @param dScriptPos 스크립트 위치
     *
	 * @note
	 * 구동 시작 후 함수를 벗어남
	 * AxmAdvContiBeginNode, AxmAdvContiEndNode와 같이 사용 시 지정된 좌표계에 중간점, 종료점을 지정하여 구동하는 원호 보간 Queue에 함수 저장
	 * 직선 프로파일 원호 연속 보간 구동을 위해 내부 Queue에 저장하여 AxmAdvContiStart 함수를 사용해서 시작
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvScriptCirclePointMove(int nCoord, int[] npAxisNo, double[] dpMidPos, double[] dpEndPos, double dVel, double dStartVel, double dStopVel, double dAccel, double dDecel, int nArcCircle, uint uScript, int nScriptAxisNo, double dScriptPos);
    
	/**
     * @brief 시작점, 회전 각도와 반지름 지정하여 원호 보간 예약 구동
     * 
     * @param nCoord 좌표계 번호
     * @param npAxisNo 두 축 배열
     * @param dpCenterPos 중심점 X, Y 배열
     * @param dAngle 각도
     * @param dVel 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
     * @param uScript 스크립트 번호
     * @param nScriptAxisNo 스크립트 축 번호
     * @param dScriptPos 스크립트 위치
     *
	 * @note
	 * 구동 시작 후 함수를 벗어남
	 * AxmAdvContiBeginNode, AxmAdvContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 회전각도와 반지름을 지정하여 원호 보간 구동하는 Queue에 함수 저장
	 * 프로파일 원호 연속 보간 구동을 위해 내부 Queue에 저장하여 AxmAdvContiStart 함수를 사용해서 시작
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvScriptCircleAngleMove(int nCoord, int[] npAxisNo, double[] dpCenterPos, double dAngle, double dVel, double dStartVel, double dStopVel, double dAccel, double dDecel, uint uCWDir, uint uScript, int nScriptAxisNo, double dScriptPos);
    
	/**
     * @brief 시작점, 종료점과 반지름 지정하여 원호 보간 예약 구동
     * 
     * @param nCoord 좌표계 번호
     * @param npAxisNo 두 축 배열
     * @param dRadius 반지름
     * @param dpEndPos 종료점 X, Y 배열
     * @param dVel 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
     * @param uShortDistance 중점까지 가는 원의 이동 거리 크기 설정 (0: 작은원, 1: 큰원)
     * @param uScript 스크립트 번호
     * @param nScriptAxisNo 스크립트 축 번호
     * @param dScriptPos 스크립트 위치
     *
	 * @note
	 * 구동 시작 후 함수를 벗어남
	 * AxmAdvContiBeginNode, AxmAdvContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 종료점과 반지름을 지정하여 원호 보간 구동하는 Queue에 함수 저장
	 * 프로파일 원호 연속 보간 구동을 위해 내부 Queue에 저장하여 AxmAdvContiStart 함수를 사용해서 시작
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvScriptCircleRadiusMove(int nCoord, int[] npAxisNo, double dRadius, double[] dpEndPos, double dVel, double dStartVel, double dStopVel, double dAccel, double dDecel, uint uCWDir, uint uShortDistance, uint uScript, int nScriptAxisNo, double dScriptPos);
    
	/**
     * @brief 현재 진행 중인 보간 구동 지정된 속도 및 위치로 2축 원호 보간 오버라이드 예약 구동
     * 
     * @param nCoord 좌표계 번호
     * @param nAxisNo 두 축 배열
     * @param dRadius 반지름
     * @param dpEndPos 종료점 X, Y 배열
     * @param dVel 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
     * @param uShortDistance 중점까지 가는 원의 이동 거리 크기 설정 (0: 작은원, 1: 큰원)
     * @param nOverrideMode 오버라이드 모드 (0: 즉시 오버라이드, 1: 예약 오버라이드)
     * @param uScript 스크립트 번호
     * @param nScriptAxisNo 스크립트 축 번호
     * @param dScriptPos 스크립트 위치
     *
	 * @details
	 * nOverrideMode
	 *    [0] 즉시 오버라이드: 구동 중인 보간이 직선, 원호 보간에 관계없이 현재 구동 노드에서 원호 보간으로 즉시 오버라이드 됨
	 *    [1] 예약 오버라이드:
	 *        현재 구동 노드 다음의 노드부터 원호 보간이 차례로 예약 됨.
	 *        1로 본 함수를 호출 할때마다 최소 1개에서 최대 8개까지 오버라이드 큐에 증가되면서 자동적으로 예약이 됨.
	 *        예약 후 마지막에 nOverrideMode = 0으로 본 함수가 호출되면 내부적으로 오버라이드 큐에 있는 예약 보간들이 연속 보간 큐로 저장되고
	 *        원호 오버라이드 구동과 이후의 예약된 연속 보간이 순차적으로 실행 됨.
     *
	 * @note 현재 진행중인 보간 구동을 지정된 속도 및 위치로 원호 보간 오버라이드 하며 다음 노드에 대한 원호 보간 구동 예약도 가능함
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvScriptOvrCircleRadiusMove(int nCoord, int[] nAxisNo, double dRadius, double[] dpEndPos, double dVel, double dStartVel, double dStopVel, double dAccel, double dDecel, uint uCWDir, uint uShortDistance, int nOverrideMode, uint uScript, int nScriptAxisNo, double dScriptPos);

    // 헬리컬 이동

    /**
     * @brief 지정된 좌표계에 시작점, 종료점과 중심점 지정하여 헬리컬 보간 예약 구동
     *
     * @param nCoord 좌표계 번호
     * @param dCenterXPos 중심점 X 위치
     * @param dCenterYPos 중심점 Y 위치
     * @param dEndXPos 종료점 X 위치
     * @param dEndYPos 종료점 Y 위치
     * @param dZPos Z 위치
     * @param dVel 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
     * @param uScript 스크립트 번호
     * @param nScriptAxisNo 스크립트 축 번호
     * @param dScriptPos 스크립트 위치
	 *
	 * @note
	 * AxmAdvContiBeginNode, AxmAdvContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 종료점과 중심점을 지정하여 헬리컬 연속보간을 예약 구동하는 함수
	 * 원호 연속 보간 구동을 위해 내부 Queue에 저장하는 함수. AxmAdvContiStart 함수를 사용해서 시작 (연속 보간 함수와 같이 이용)
	 * 주의 사항: Helix를 연속 보간 사용 시 Spline, 직선 보간과 원호 보간을 같이 사용 할 수 없음
	 *
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvScriptHelixCenterMove(int nCoord, double dCenterXPos, double dCenterYPos, double dEndXPos, double dEndYPos, double dZPos, double dVel, double dStartVel, double dStopVel, double dAccel, double dDecel, uint uCWDir, uint uScript, int nScriptAxisNo, double dScriptPos);

    /**
     * @brief 지정된 좌표계에 시작점, 종료점과 반지름 지정하여 헬리컬 보간 예약 구동
     *
     * @param nCoord 좌표계 번호
     * @param dMidXPos 중간점 X 위치
     * @param dMidYPos 중간점 Y 위치
     * @param dEndXPos 종료점 X 위치
     * @param dEndYPos 종료점 Y 위치
     * @param dZPos Z 위치
     * @param dVel 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uScript 스크립트 번호
     * @param nScriptAxisNo 스크립트 축 번호
     * @param dScriptPos 스크립트 위치
	 *
	 * @note
	 * AxmAdvContiBeginNode, AxmAdvContiEndNode와 같이 사용 시 지정된 좌표계에 중간점, 종료점을 지정하여 헬리컬 연속 보간을 예약 구동하는 함수
	 * 원호 연속 보간 구동을 위해 내부 Queue에 저장하는 함수. AxmAdvContiStart 함수를 사용해서 시작 (연속 보간 함수와 같이 이용)
	 *
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvScriptHelixPointMove(int nCoord, double dMidXPos, double dMidYPos, double dEndXPos, double dEndYPos, double dZPos, double dVel, double dStartVel, double dStopVel, double dAccel, double dDecel, uint uScript, int nScriptAxisNo, double dScriptPos);

    /**
     * @brief 지정된 좌표계에 시작점, 회전각도와 반지름 지정하여 헬리컬 보간 예약 구동
     *
     * @param nCoord 좌표계 번호
     * @param dCenterXPos 중심점 X 위치
     * @param dCenterYPos 중심점 Y 위치
     * @param dAngle 회전 각도
     * @param dZPos Z 위치
     * @param dVel 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
     * @param uScript 스크립트 번호
     * @param nScriptAxisNo 스크립트 축 번호
     * @param dScriptPos 스크립트 위치
	 *
	 * @note
	 * AxmAdvContiBeginNode, AxmAdvContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 회전 각도와 반지름을 지정하여 헬리컬 연속 보간을 예약 구동하는 함수
	 * 원호 연속 보간 구동을 위해 내부 Queue에 저장하는 함수. AxmAdvContiStart 함수를 사용해서 시작 (연속 보간 함수와 같이 이용)
	 *
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvScriptHelixAngleMove(int nCoord, double dCenterXPos, double dCenterYPos, double dAngle, double dZPos, double dVel, double dStartVel, double dStopVel, double dAccel, double dDecel, uint uCWDir, uint uScript, int nScriptAxisNo, double dScriptPos);

    /**
     * @brief 지정된 좌표계에 시작점, 종료점과 반지름 지정하여 헬리컬 보간 예약 구동
     *
     * @param nCoord 좌표계 번호
     * @param dRadius 반지름
     * @param dEndXPos 종료점 X 위치
     * @param dEndYPos 종료점 Y 위치
     * @param dZPos Z 위치
     * @param dVel 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
     * @param uShortDistance 중점까지 가는 원의 이동 거리 크기 설정 (0: 작은원, 1: 큰원)
     * @param uScript 스크립트 번호
     * @param nScriptAxisNo 스크립트 축 번호
     * @param dScriptPos 스크립트 위치
	 *
	 * @note
	 * AxmAdvContiBeginNode, AxmAdvContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 종료점과 반지름을 지정하여 헬리컬 연속 보간을 예약 구동하는 함수
	 * 원호 연속 보간 구동을 위해 내부 Queue에 저장하는 함수. AxmAdvContiStart 함수를 사용해서 시작 (연속 보간 함수와 같이 이용)
	 *
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvScriptHelixRadiusMove(int nCoord, double dRadius, double dEndXPos, double dEndYPos, double dZPos, double dVel, double dStartVel, double dStopVel, double dAccel, double dDecel, uint uCWDir, uint uShortDistance, uint uScript, int nScriptAxisNo, double dScriptPos);

    /**
     * @brief 지정된 좌표계에 시작점과 종료점 지정하여 3축 헬리컬 보간 오버라이드 예약 구동
     *
     * @param nCoord 좌표계 번호
     * @param dRadius 반지름
     * @param dEndXPos 종료점 X 위치
     * @param dEndYPos 종료점 Y 위치
     * @param dZPos Z 위치
     * @param dVel 속도
     * @param dStartVel 시작 속도
     * @param dStopVel 정지 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
     * @param uShortDistance 중점까지 가는 원의 이동 거리 크기 설정 (0: 작은원, 1: 큰원)
     * @param nOverrideMode 오버라이드 모드 (0: 즉시 오버라이드, 1: 예약 오버라이드)
     * @param uScript 스크립트 번호
     * @param nScriptAxisNo 스크립트 축 번호
     * @param dScriptPos 스크립트 위치
	 *
* @details
	 * nOverrideMode
	 *    [0] 즉시 오버라이드: 구동 중인 보간이 직선, 원호 보간에 관계없이 현재 구동 노드에서 헬리컬 보간으로 즉시 오버라이드 됨
	 *    [1] 예약 오버라이드:
	 *        현재 구동 노드 다음의 노드부터 헬리컬 보간이 차례로 예약 됨.
	 *        1로 본 함수를 호출 할때마다 최소 1개에서 최대 8개까지 오버라이드 큐에 증가되면서 자동적으로 예약이 됨.
	 *        예약 후 마지막에 nOverrideMode = 0으로 본 함수가 호출되면 내부적으로 오버라이드 큐에 있는 예약 보간들이 연속 보간 큐로 저장되고
	 *        헬리컬 오버라이드 구동과 이후의 예약된 연속 보간이 순차적으로 실행 됨.
     *
	 * @note 현재 진행중인 보간 구동을 지정된 속도 및 위치로 헬리컬 보간 오버라이드 예약 구동
	 *
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvScriptOvrHelixRadiusMove(int nCoord, double dRadius, double dEndXPos, double dEndYPos, double dZPos, double dVel, double dStartVel, double dStopVel, double dAccel, double dDecel, uint uCWDir, uint uShortDistance, int nOverrideMode, uint uScript, int nScriptAxisNo, double dScriptPos);

    // 연속 보간 함수
	
    /**
     * @brief 지정된 좌표계에 연속 보간 구동 중 현재 구동중인 연속 보간 인덱스 번호 확인
     *
     * @param nCoord 좌표계 번호
     * @param npNodeNum 현재 구동중인 연속 보간 인덱스 번호 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvContiGetNodeNum(int nCoord, ref int npNodeNum);
	
	/**
     * @brief 지정된 좌표계에 설정한 연속 보간 구동 총 인덱스 개수 확인
     *
     * @param nCoord 좌표계 번호
     * @param npNodeNum 연속 보간 구동 총 인덱스 개수 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvContiGetTotalNodeNum(int nCoord, ref int npNodeNum);
    
	/**
     * @brief 지정된 좌표계에 보간 구동 위한 내부 Queue에 저장되어 있는 보간 구동 개수 확인
     *
     * @param nCoord 좌표계 번호
     * @param npQueueIndex 내부 Queue에 저장되어 있는 보간 구동 개수 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvContiReadIndex(int nCoord, ref int npQueueIndex);
    
	/**
     * @brief 지정된 좌표계에 보간 구동 위한 내부 Queue가 비어 있는지 확인
     *
     * @param nCoord 좌표계 번호
     * @param upQueueFree 내부 Queue 상태 값 (0: Queue 내부가 비어있지 않음, 1: Queue 내부가 비어 있음)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvContiReadFree(int nCoord, ref uint upQueueFree);
    
	/**
     * @brief 지정된 좌표계에 연속 보간 구동 위해 저장된 내부 Queue를 모두 삭제
     *
     * @param nCoord 좌표계 번호
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvContiWriteClear(int nCoord);
    
	/**
     * @brief 지정된 좌표계에 연속 보간 오버라이드 구동 위해 예약된 오버라이드용 큐 모두 삭제
     *
     * @param nCoord 좌표계 번호
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvOvrContiWriteClear(int nCoord);
    
	/**
     * @brief 저장된 내부 연속 보간 Queue의 구동 시작 함수
     *
     * @param nCoord 좌표계 번호
     * @param uProfileset 프로파일 모드
     * @param nAngle 자동 가감속 모드 사용 시 Angle 값 (0~360도)
	 *
	 * @details
	 * dwProfileset
	 *    [0] CONTI_NODE_VELOCITY: 속도 지정 보간 모드
	 *    [1] CONTI_NODE_MANUAL  : 노드 가감속 보간 모드
	 *    [2] CONTI_NODE_AUTO    : 자동 가감속 보간 모드
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvContiStart(int nCoord, uint uProfileset, int nAngle); 
    
	/**
     * @brief 연속 보간 정지
     *
     * @param nCoord 좌표계 번호
     * @param dDecel 감속도
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvContiStop(int nCoord, double dDecel);
    
	/**
     * @brief 지정된 좌표계에 연속 보간 축 맵핑 설정
     *
     * @param nCoord 좌표계 번호
     * @param nSize 배열 크기
     * @param npAxesNo 축 번호 배열
	 *
	 * @note
	 * 축 맵핑 번호는 0 부터 시작
	 * 주의 사항: 
	 *    축 맵핑 할 때는 반드시 실제 축번호가 작은 숫자부터 큰숫자 순서로 넣어야 함
	 *    가상 축 맵핑 함수를 사용하였을 때 가상 축 번호를 실제 축번호가 작은 값 부터 lpAxesNo의 낮은 인텍스에 입력해야 함
	 *    가상 축 맵핑 함수를 사용하였을 때 가상 축 번호에 해당하는 실제 축 번호가 다른 값이어야 함
	 *    같은 축을 다른 Coordinate에 중복 맵핑하면 안됨
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvContiSetAxisMap(int nCoord, int nSize, int[] npAxesNo);
    
	/**
     * @brief 지정된 좌표계에 연속 보간 축 맵핑 설정 정보 확인
     *
     * @param nCoord 좌표계 번호
     * @param npSize 배열 크기 저장
     * @param npAxesNo 축 번호 배열 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvContiGetAxisMap(int nCoord, ref int npSize, ref int npAxesNo);
    
	/**
     * @brief 지정된 좌표계에 연속보간 축 절대/상대 모드 설정
     *
     * @param nCoord 좌표계 번호
     * @param uAbsRelMode 이동 거리 계산 모드
	 *
	 * @details
	 * uAbsRelMode
	 *    [0] POS_ABS_MODE: 절대 좌표계
	 *    [1] POS_REL_MODE: 상대 좌표계
	 *
     * @note 주의 사항: 반드시 축 맵핑 하고 사용 가능
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvContiSetAbsRelMode(int nCoord, uint uAbsRelMode);
    
	/**
     * @brief 지정된 좌표계에 연속보간 축 절대/상대 모드 설정 값 확인
     *
     * @param nCoord 좌표계 번호
     * @param upAbsRelMode 이동 거리 계산 모드 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvContiGetAbsRelMode(int nCoord, ref uint upAbsRelMode);
    
	/**
     * @brief 지정된 좌표계에 연속 보간 구동 중인지 확인
     *
     * @param nCoord 좌표계 번호
     * @param upInMotion 연속 보간 구동 중 여부 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvContiIsMotion(int nCoord, ref uint upInMotion);
    
	/**
     * @brief 지정된 좌표계에 연속 보간에서 수행 할 작업들 등록 시작
     *
     * @param nCoord 좌표계 번호
	 *
	 * @note
	 * 이 함수 호출 후 AxmAdvContiEndNode 함수가 호출되기 전까지 수행되는 모든 모션 작업은 실제 모션을 수행하는 것이 아니라
	 * 연속 보간 모션으로 등록 되는 것이며 AxmAdvContiStart 함수가 호출될 때 비로소 등록된 모션이 실제로 수행
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvContiBeginNode(int nCoord);
    
	/**
     * @brief 지정된 좌표계에서 연속보간 수행할 작업들 등록 종료
     *
     * @param nCoord 좌표계 번호
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvContiEndNode(int nCoord);
    
    /**
     * @brief 지정한 다축을 설정한 감속도로 동기 감속 정지
     *
     * @param nArraySize 축 배열 크기
     * @param npAxesNo 축 번호 배열 
     * @param dpMaxDecel 감속도 설정 값 배열
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveMultiStop(int nArraySize, int[] npAxesNo, double[] dpMaxDecel);
    
	/**
     * @brief 지정한 다축 동기 급 정지
     *
     * @param nArraySize 축 배열 크기
     * @param npAxesNo 축 번호 배열 
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveMultiEStop(int nArraySize, int[] npAxesNo);
    
	/**
     * @brief 지정한 다축 동기 감속 정지
     *
     * @param nArraySize 축 배열 크기
     * @param lpAxesNo 축 번호 배열
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveMultiSStop(int nArraySize, int[] npAxesNo);

    /**
     * @brief 지정한 축의 실제 구동 속도 반환
     *
     * @param nAxisNo 축 번호
     * @param dpVel 실제 구동 속도 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadActVel(int nAxisNo, ref double dpVel);
    
	/**
     * @brief 서보 타입 슬레이브 기기의 SVCMD_STAT 커맨드 값 확인
     *
     * @param nAxisNo 축 번호
     * @param upStatus 커맨드 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadServoCmdStat(int nAxisNo, ref uint upStatus);
    
	/**
     * @brief 서보 타입 슬레이브 기기의 SVCMD_CTRL 커맨드 값 확인
     *
     * @param nAxisNo 축 번호
     * @param upStatus 커맨드 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadServoCmdCtrl(int nAxisNo, ref uint upStatus);
    
    /**
     * @brief 겐트리 구동 시 마스터 축과 슬레이브 축 간의 위치 차에 대한 설정된 오차 한계 값 반환
     *
     * @param nAxisNo 축 번호
     * @param dpPosition 설정된 오차 한계 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGantryGetMstToSlvOverDist(int nAxisNo, ref double dpPosition);
    
	/**
     * @brief 겐트리 구동 시 마스터 축과 슬레이브 축 간의 위치 차에 대한 오차 한계 값 설정
     *
     * @param nAxisNo 축 번호
     * @param dPosition 설정할 오차 한계 값
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGantrySetMstToSlvOverDist(int nAxisNo, double dPosition);

    /**
     * @brief 지정 축 알람 신호의 코드 상태 반환
     *
     * @param nAxisNo 축 번호
     * @param upCodeStatus 알람 신호의 코드 상태 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalReadServoAlarmCode(int nAxisNo, ref ushort upCodeStatus);
    
    /**
     * @brief 서보 타입 슬레이브 기기의 좌표계 설정 (MLIII 전용)
     *
     * @param nAxisNo 축 번호
     * @param uPosData 위치 데이터
     * @param uPosSel 좌표 선택 모드
     * @param uRefe 참조 원점 설정
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoCoordinatesSet(int nAxisNo, uint uPosData, uint uPosSel, uint uRefe);
    
	/**
     * @brief 서보 타입 슬레이브 기기의 브레이크 작동 신호 출력 (MLIII 전용)
     *
     * @param nAxisNo 축 번호
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoBreakOn(int nAxisNo);
    
	/**
     * @brief 서보 타입 슬레이브 기기의 브레이크 작동 신호 해제 (MLIII 전용)
     *
     * @param nAxisNo 축 번호
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoBreakOff(int nAxisNo);
    
	/**
     * @brief 서보 타입 슬레이브 기기의 설정 모드 변경
     *
     * @param nAxisNo 축 번호
     * @param uCfMode 설정 모드 값
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoConfig(int nAxisNo, uint uCfMode);
    
	/**
     * @brief 서보 타입 슬레이브 기기의 센서 정보 초기화
     *
     * @param nAxisNo 축 번호
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSensOn(int nAxisNo);
    
	/**
     * @brief 서보 타입 슬레이브 기기의 센서 전원 OFF
     *
     * @param nAxisNo 축 번호
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSensOff(int nAxisNo);
    
	/**
     * @brief 서보 타입 슬레이브 기기의 SMON 커맨드 실행
     *
     * @param nAxisNo 축 번호
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSmon(int nAxisNo);
    
	/**
     * @brief 서보 타입 슬레이브 기기의 모니터 정보나 입출력 신호의 상태 확인
     *
     * @param nAxisNo 축 번호
     * @param pbParam 모니터 정보나 입출력 신호의 상태 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoGetSmon(int nAxisNo, ref uint pbParam);
    
	/**
     * @brief 서보 타입 슬레이브 기기의 서보 ON 설정
     *
     * @param nAxisNo 축 번호
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSvOn(int nAxisNo);
    
	/**
     * @brief 서보 타입 슬레이브 기기의 서보 OFF 설정
     *
     * @param nAxisNo 축 번호
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSvOff(int nAxisNo);
    
	/**
     * @brief 서보 타입 슬레이브 기기가 지정된 보간 위치로 보간 이동 실시
     *
     * @param nAxisNo 축 번호
     * @param uTPOS 보간 위치 값
     * @param uVFF VFF 값
     * @param uTFF TFF 값
     * @param uTLIM TLIM 값
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoInterpolate(int nAxisNo, uint uTPOS, uint uVFF, uint uTFF, uint uTLIM);
    
	/**
     * @brief 서보 타입 슬레이브 기기가 지정한 위치로 위치 이동 실시
     *
     * @param nAxisNo 축 번호
     * @param uTPOS 위치 값
     * @param uSPD 속도
     * @param uACCR 가속도
     * @param uDECR 감속도
     * @param uTLIM TLIM 값
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoPosing(int nAxisNo, uint uTPOS, uint uSPD, uint uACCR, uint uDECR, uint uTLIM);
    
	/**
     * @brief 서보 타입 슬레이브 기기가 지정된 이동 속도로 정속 이송 실시
     *
     * @param nAxisNo 축 번호
     * @param uSPD 속도
     * @param uACCR 가속도
     * @param uDECR 감속도
     * @param uTLIM TLIM 값
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoFeed(int nAxisNo, uint uSPD, uint uACCR, uint uDECR, uint uTLIM);
    
	/**
     * @brief 서보 타입 슬레이브 기기가 이송 중 외부 위치 결정 신호의 입력에 의해 위치 이송 실시
     *
     * @param nAxisNo 축 번호
     * @param uSPD 속도
     * @param uACCR 가속도
     * @param uDECR 감속도
     * @param uTLIM TLIM 값
     * @param uExSig1 외부 신호 1
     * @param uExSig2 외부 신호 2
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoExFeed(int nAxisNo, uint uSPD, uint uACCR, uint uDECR, uint uTLIM, uint uExSig1, uint uExSig2);
    
	/**
     * @brief 서보 타입 슬레이브 기기가 외부 위치 결정 신호 입력에 의해 위치 이송 실시
     *
     * @param nAxisNo 축 번호
     * @param uTPOS 위치 값
     * @param uSPD 속도
     * @param uACCR 가속도
     * @param uDECR 감속도
     * @param TLIM TLIM 값
     * @param uExSig1 외부 신호 1
     * @param uExSig2 외부 신호 2
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoExPosing(int nAxisNo, uint uTPOS, uint uSPD, uint uACCR, uint uDECR, uint uTLIM, uint uExSig1, uint uExSig2);
    
	/**
     * @brief 서보 타입 슬레이브 기기가 원점 복귀 실시
     *
     * @param nAxisNo 축 번호
     * @param uSPD 속도
     * @param uACCR 가속도
     * @param uDECR 감속도
     * @param uTLIM TLIM 값
     * @param uExSig1 외부 신호 1
     * @param uExSig2 외부 신호 2
     * @param bHomeDir 원점 방향
     * @param bHomeType 원점 타입
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoZret(int nAxisNo, uint uSPD, uint uACCR, uint uDECR, uint uTLIM, uint uExSig1, uint uExSig2, uint bHomeDir, uint bHomeType);
    
	/**
     * @brief 서보 타입 슬레이브 기기가 속도 제어 실시
     *
     * @param nAxisNo 축 번호
     * @param uTFF TFF 값
     * @param uVREF VREF 값
     * @param uACCR 가속도
     * @param uDECR 감속도
     * @param uTLIM TLIM 값
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoVelctrl(int nAxisNo, uint uTFF, uint uVREF, uint uACCR, uint uDECR, uint uTLIM);
    
	/**
     * @brief 서보 타입 슬레이브 기기가 토크 제어 실시
     *
     * @param nAxisNo 축 번호
     * @param uVLIM VLIM 값
     * @param nTQREF TQREF 값
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */   
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoTrqctrl(int nAxisNo, uint uVLIM, int nTQREF);
    
	/**
     * @brief 서보 타입 슬레이브 기기의 서보팩 특정 파라미터 설정 값 반환
     *
     * @param nAxisNo 축 번호
     * @param uNo 파라미터 번호
     * @param uSize 파라미터 크기
     * @param uMode 파라미터 모드
     * @param upParam 파라미터 값 저장
	 *
	 * @details
	 * bMode
	 *    0x00: common parameters ram
	 *    0x01: common parameters flash
	 *    0x10: device parameters ram
	 *    0x11: device parameters flash
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoGetParameter(int nAxisNo, uint uNo, uint uSize, uint uMode, ref uint upParam);
    
	/**
     * @brief 서보 타입 슬레이브 기기의 서보팩 특정 파라미터 값 설정
     *
     * @param nAxisNo 축 번호
     * @param uNo 파라미터 번호
     * @param uSize 파라미터 크기
     * @param uMode 파라미터 모드
     * @param upParam 파라미터 값
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSetParameter(int nAxisNo, uint uNo, uint uSize, uint uMode, ref uint upParam);
    
	/**
     * @brief M3 서보팩에 Command Execution 실행
     *
     * @param nAxisNo 축 번호
     * @param uCommand 커맨드 값
     * @param uSize 사용할 변수 개수
     * @param upExcData 실행 데이터 배열
	 *
	 * @details
	 * dwSize
	 *    Ex1) M3StatNop:        AxmServoCmdExecution(m_lAxis, 0, NULL)
	 *    Ex2) M3GetStationIdRd: AxmServoCmdExecution(m_lAxis, 3, pbIdRd)
	 *
     * @note
     * M3StationNop(int lNodeNum)                                                                                               : bwSize = 0
     * M3GetStationIdRd(int lNodeNum, BYTE bIdCode, BYTE bOffset, BYTE bSize, BYTE *pbIdRd)                                     : bwSize = 3
     * M3ServoSetConfig(int lNodeNum, BYTE bMode)                                                                               : bwSize = 1
     * M3SetStationAlarmClear(int lNodeNum, WORD wAlarmClrMod)                                                                  : bwSize = 1
     * M3ServoSyncSet(int lNodeNum)                                                                                             : bwSize = 0
     * M3SetStationConnect(int lNodeNum, BYTE bVer, uByteComMod ubComMode, BYTE bComTime, BYTE bProfileType)                    : bwSize = 4
     * M3SetStationDisconnect(int lNodeNum)                                                                                     : bwSize = 0
     * M3ServoSmon(int lNodeNum)                                                                                                : bwSize = 0
     * M3ServoSvOn(int lNodeNum)                                                                                                : bwSize = 0
     * M3ServoSvOff(int lNodeNum)                                                                                               : bwSize = 0
     * M3ServoInterpolate(int lNodeNum, LONG lTPOS, LONG lVFF, LONG lTFF)                                                       : bwSize = 3
     * M3ServoPosing(int lNodeNum, LONG lTPOS, LONG lSPD, LONG lACCR, LONG lDECR, LONG lTLIM)                                   : bwSize = 5
     * M3ServoFeed(int lNodeNum, LONG lSPD, LONG lACCR, LONG lDECR, LONG lTLIM)                                                 : bwSize = 4
     * M3ServoExFeed(int lNodeNum, LONG lSPD, LONG lACCR, LONG lDECR, LONG lTLIM, DWORD dwExSig1, DWORD dwExSig2)               : bwSize = 6
     * M3ServoExPosing(int lNodeNum, LONG lTPOS, LONG lSPD, LONG lACCR, LONG lDECR, LONG lTLIM, DWORD dwExSig1, DWORD dwExSig2) : bwSize = 7
     * M3ServoTrqctrl(int lNodeNum, LONG lVLIM, LONG lTQREF)                                                                    : bwSize = 2
     * M3ServoGetParameter(int lNodeNum, WORD wNo, BYTE bSize, BYTE bMode, BYTE *pbParam)                                       : bwSize = 3
     * M3ServoSetParameter(int lNodeNum, WORD wNo, BYTE bSize, BYTE bMode, BYTE *pbParam)                                       : bwSize = 7
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmServoCmdExecution(int nAxisNo, uint uCommand, uint uSize, uint[] upExcData);
    
	/**
     * @brief 서보 타입 슬레이브 기기의 설정된 토크 제한 값 반환
     *
     * @param nAxisNo 축 번호
     * @param upTorqLimit 토크 제한 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoGetTorqLimit(int nAxisNo, ref uint upTorqLimit);
    
	/**
     * @brief 서보 타입 슬레이브 기기에 토크 제한 값 설정
     *
     * @param nAxisNo 축 번호
     * @param uTorqLimit 토크 제한 값
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSetTorqLimit(int nAxisNo, uint uTorqLimit);

    /**
     * @brief 서보 타입 슬레이브 기기에 설정된 SVCMD_IO 커맨드 값 반환
     *
     * @param nAxisNo 축 번호
     * @param upData 커맨드 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoGetSendSvCmdIOOutput(int nAxisNo, ref uint upData);
    
	/**
     * @brief 서보 타입 슬레이브 기기에 설정된 SVCMD_IO 커맨드 값 설정
     *
     * @param nAxisNo 축 번호
     * @param uData 커맨드 값
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSetSendSvCmdIOOutput(int nAxisNo, uint uData);
  
    /**
     * @brief 서보 타입 슬레이브 기기에 설정된 SVCMD_CTRL 커맨드 값 반환
     *
     * @param nAxisNo 축 번호
     * @param upData 커맨드 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoGetSvCmdCtrl(int nAxisNo, ref uint upData);
    
	/**
     * @brief 서보 타입 슬레이브 기기에 설정된 SVCMD_CTRL 커맨드 값 설정
     *
     * @param nAxisNo 축 번호
     * @param uData 커맨드 값
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSetSvCmdCtrl(int nAxisNo, uint uData);

    /**
     * @brief MLIII adjustment operation 설정
     *
     * @param nAxisNo 축 번호
     * @param uReqCode 요청 코드
	 *
	 * @details
	 * dwReqCode
	 *    0x1005: parameter initialization (20sec)
	 *    0x1008: absolute encoder reset (5sec)
	 *    0x100E: automatic offset adjustment of motor current detection signals (5sec)
	 *    0x1013: Multiturn limit setting (5sec)
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3AdjustmentOperation(int nAxisNo, uint uReqCode);
    
	/**
     * @brief 서보 축 추가 모니터링 채널별 선택 값 설정
     *
     * @param nAxisNo 축 번호
     * @param uMon0 모니터링 채널 0
     * @param uMon1 모니터링 채널 1
     * @param uMon2 모니터링 채널 2
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSetMonSel(int nAxisNo, uint uMon0, uint uMon1, uint uMon2);
    
	/**
     * @brief 서보 축 추가 모니터링 채널별 선택 값 확인
     *
     * @param nAxisNo 축 번호
     * @param upMon0 모니터링 채널 0 값 저장
     * @param upMon1 모니터링 채널 1 값 저장
     * @param upMon2 모니터링 채널 2 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoGetMonSel(int nAxisNo, ref uint upMon0, ref uint upMon1, ref uint upMon2);
    
	/**
     * @brief 서보 축 추가 모니터링 채널별 설정 값 기준으로 현재 상태 반환
     *
     * @param nAxisNo 축 번호
     * @param uMonSel 모니터링 채널 선택 값
     * @param upMonData 모니터링 데이터 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoReadMonData(int nAxisNo, uint uMonSel, ref uint upMonData);
    
	/**
     * @brief 제어할 토크 축 설정
     *
     * @param nCoord 좌표계 번호
     * @param nSize 축 개수
     * @param npAxesNo 축 번호 배열
     * @param uTLIM TLIM 값
     * @param uConMode 제어 모드
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmAdvTorqueContiSetAxisMap(int nCoord, int nSize, int[] npAxesNo, uint uTLIM, uint uConMode);
    
	/**
     * @brief 토크 프로파일 설정 파라미터 설정
     *
     * @param nCoord 좌표계 번호
     * @param nAxisNo 축 번호
     * @param nTorqueSign 토크 부호
     * @param uVLIM VLIM 값
     * @param uProfileMode 프로파일 모드
     * @param uStdTorq 표준 토크 값
     * @param uStopTorq 정지 토크 값
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSetTorqProfile(int nCoord, int nAxisNo, int nTorqueSign, uint uVLIM, uint uProfileMode, uint uStdTorq, uint uStopTorq);
    
	/**
     * @brief 토크 프로파일 설정 파라미터 확인
     *
     * @param nCoord 좌표계 번호
     * @param nAxisNo 축 번호
     * @param npTorqueSign 토크 부호 값 저장
     * @param upVLIM VLIM 값 저장
     * @param upProfileMode 프로파일 모드 저장
     * @param upStdTorq 표준 토크 값 저장
     * @param upStopTorq 정지 토크 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoGetTorqProfile(int nCoord, int nAxisNo, ref int npTorqueSign, ref uint upVLIM, ref uint upProfileMode, ref uint upStdTorq, ref uint upStopTorq);

    // EtherCAT 전용 함수
	
    /**
     * @brief Inposition 신호의 Range 설정
     * 
     * @param nAxisNo 축 번호
     * @param dInposRange Inposition 범위 값 (dInposRange > 0)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetInposRange(int nAxisNo, double dInposRange);
    
	/**
     * @brief Inposition 신호의 Range 값 확인
     * 
     * @param nAxisNo 축 번호
     * @param dpInposRange Inposition 범위 값 저장 (dInposRange > 0)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetInposRange(int nAxisNo, ref double dpInposRange);    
	
    /**
     * @brief 절대 엔코더 위치를 dPositiond로 지정
     * 
     * @param nAxisNo 축 번호
     * @param dPosition 절대치 엔코더의 현재 위치 설정 값
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusSetEncClear(int nAxisNo, double dPosition);
    
    /**
     * @brief 지정 축에 해당하는 서보팩의 지령 위치와 실체 위치를 dPos 값으로 일치
     *
     * @param nAxisNo 축 번호
	 *
	 * @note Reserved
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusSetMotorPosMatch(int nAxisNo);
    
    /**
     * @brief Work 좌표계 설정 (Default: World Coordinate와 일치)
     * 
     * @param nCoordNo 좌표계 번호 (0~7)
     * @param dpOrigin World Coordinate를 기준으로하는 Work 좌표계 원점 위치
     * @param dpXPos World Coordinate를 기준으로하는 Work 좌표계 X축 상의 임의의 위치
     * @param dpYPos World Coordinate를 기준으로하는 Work 좌표계 Y축 상의 임의의 위치
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetWorkCoordinate(int nCoordNo, ref double dpOrigin, ref double dpXPos, ref double dpYPos);
    
	/**
     * @brief Work 좌표계 설정 값 확인 (Default: World Coordinate와 일치)
     * 
     * @param nCoordNo 좌표계 번호 (0~7)
     * @param dpOrigin World Coordinate를 기준으로하는 Work 좌표계 원점 위치 저장
     * @param dpXPos World Coordinate를 기준으로하는 Work 좌표계 X축 상의 임의의 위치 저장
     * @param dpYPos World Coordinate를 기준으로하는 Work 좌표계 Y축 상의 임의의 위치 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetWorkCoordinate(int nCoordNo, ref double dpOrigin, ref double dpXPos, ref double dpYPos);
    
	/**
     * @brief 오버라이드할 때 위치 속성(절대/상대) 설정
     *
     * @param nAxisNo 축 번호
     * @param uAbsRelMode 위치 속성(절대/상대) 값
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetOverridePosMode(int nAxisNo, uint uAbsRelMode);
    
	/**
     * @brief 오버라이드할 때 위치 속성(절대/상대) 값 확인
     *
     * @param nAxisNo 축 번호
     * @param upAbsRelMode 위치 속성(절대/상대) 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetOverridePosMode(int nAxisNo, ref uint upAbsRelMode);
    
	/**
     * @brief LineMove 오버라이드할 때 위치 속성(절대/상대) 설정
     *
     * @param nCoordNo 좌표계 번호
     * @param uAbsRelMode 위치 속성(절대/상대) 값
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetOverrideLinePosMode(int nCoordNo, uint uAbsRelMode);
    
	/**
     * @brief LineMove 오버라이드할 때 위치 속성(절대/상대) 값 확인
     *
     * @param nCoordNo 좌표계 번호
     * @param upAbsRelMode 위치 속성(절대/상대) 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetOverrideLinePosMode(int nCoordNo, ref uint upAbsRelMode);
    
    /**
     * @brief 지정된 축의 Vibration Control 설정값(주파수, 댐핑계수, 임펄스 개수) 설정
     *
     * @param nAxisNo 축 번호
     * @param nSize 설정 배열 크기
     * @param pdFrequency 주파수 배열
     * @param pdDampingRatio 댐핑 계수 배열
     * @param npImpulseCount 임펄스 개수 배열
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetVibrationControl(int nAxisNo, int nSize, ref double pdFrequency, ref double pdDampingRatio, ref int npImpulseCount);
    
    /**
     * @brief 지정된 축의 Vibration Control 활성화 여부 설정
     *
     * @param nAxisNo 축 번호
     * @param uEnable 활성화 여부 설정 값 (0: Disable, 1: Enable)
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotVibrationControlEnable(int nAxisNo, uint uEnable);
    
    /**
     * @brief 지정된 축의 Vibration Control 활성화 여부 확인
     *
     * @param nAxisNo 축 번호
     * @param upEnable Vibration Control 활성화 상태 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotVibrationControlIsEnable(int nAxisNo, ref uint upEnable);
    
    /**
     * @brief 지정된 좌표계의 Vibration Control 활성화 여부 설정
     *
     * @param nCoordNo 좌표계 번호
     * @param uEnable Vibration Control 활성화 여부 설정 값
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotVibrationContronCoordEnable(int nCoordNo, uint uEnable);
    
    /**
     * @brief 지정된 좌표계의 Vibration Control 활성화 여부 확인
     *
     * @param nCoordNo 좌표계 번호
     * @param upEnable Vibration Control 활성화 상태 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotVibrationContronCoordIsEnable(int nCoordNo, ref uint upEnable);
    
    /**
     * @brief 지정된 축의 Position 범위 마다 TorqueLimit 설정
     * 
     * @param nAxisNo 축 번호
     * @param nSize 설정할 위치의 개수(2개 이상)
     * @param dpPosition 위치 배열 (오름차순으로 입력)
     * @param dpPlusTorqueLimit 토크 제한율 (천분율, lSize - 1)
     * @param dpMinusTorqueLimit Reserved 
     * @param nTarget CRC 신호를 Program으로 발생 여부 (0: COMMAND, 1: ACTUAL)
     *
	 * @note EtherCAT은 PlusTorqueLimit 값만으로 양쪽 다 같은 TorqueLimit 설정
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetMultiTorqueLimit(int nAxisNo, int nSize, double[] dpPosition, double[] dpPlusTorqueLimit, double[] dpMinusTorqueLimit, int nTarget);

    /**
     * @brief 지정된 축의 Multi TorqueLimit 설정
     * 
     * @param nAxisNo 축 번호
     * @param uEnable 사용 유무 (0: Disable, 1: Enable)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotMultiTorqueLimitEnable(int nAxisNo, uint uEnable);

    /**
     * @brief 지정된 축의 Multi TorqueLimit Enable 여부 확인
     * 
     * @param nAxisNo 축 번호
     * @param upEnable 사용 유무 저장  (0: Disable, 1: Enable)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotMultiTorqueLimitIsEnable(int nAxisNo, ref uint upEnable);

    /**
     * @brief AxmMoveStartPos 함수와 동일. 종료 속도(EndVel) 추가
     *
     * @param nAxisNo 축 번호
     * @param dPos 위치
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param dEndVel 종료 속도
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveStartPosEx(int nAxisNo, double dPos, double dVel, double dAccel, double dDecel, double dEndVel);
    
	/**
     * @brief AxmMovePos 함수와 동일. 종료 속도(EndVel) 추가
     *
     * @param nAxisNo 축 번호
     * @param dPos 위치
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param dEndVel 종료 속도
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMovePosEx(int nAxisNo, double dPos, double dVel, double dAccel, double dDecel, double dEndVel);
    
	/**
     * @brief AxmMoveStartMultiPos 함수와 동일. 종료 속도(EndVel) 추가
     *
     * @param nArraySize 이동할 축의 개수
     * @param npAxisNo 축 번호 배열
     * @param dpPos 위치 배열
     * @param dpVel 속도 배열
     * @param dpAccel 가속도 배열
     * @param dpDecel 감속도 배열
     * @param dpEndVel 종료 속도 배열
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    //[DllImport("AXL.dll")] public static extern uint AxmMoveStartMultiPosEx(int nArraySize, int[] npAxisNo, double[] dpPos, double[] dpVel, double[] dpAccel, double[] dpDecel, double[] dpEndVel);
    
    /**
     * @brief Coordinate Motion 경로 상에서 감속 정지
     * 
     * @param nCoordNo 좌표계 번호
     * @param dDecel 감속도
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveCoordStop(int nCoordNo, double dDecel);
    
	/**
     * @brief Coordinate Motion 경로 상에서 급 정지
     * 
     * @param nCoordNo 좌표계 번호
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveCoordEStop(int nCoordNo);
    
	/**
     * @brief Coordinate Motion 경로 상에서 감속 정지
     * 
     * @param nCoordNo 좌표계 번호
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveCoordSStop(int nCoordNo);
    
    /**
     * @brief AxmLineMove 모션 위치 오버라이드
     * 
     * @param nCoordNo 좌표계 번호
     * @param dpOverridePos 오버라이드 위치 배열
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmOverrideLinePos(int nCoordNo, double[] dpOverridePos);
    
	/**
     * @brief AxmLineMove 모션 속도 오버라이드
     * 
     * @param nCoordNo 좌표계 번호
     * @param dOverrideVel 오버라이드 속도
     * @param dpDistance 축 별 속력 비율 배열
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmOverrideLineVel(int nCoordNo, double dOverrideVel, double[] dpDistance);
    
    /**
     * @brief AxmLineMove 모션 속도 오버라이드
     * 
     * @param nCoordNo 좌표계 번호
     * @param dOverrideVelocity 오버라이드 속도
     * @param dMaxAccel 오버라이드 감속도
     * @param dMaxDecel 오버라이드 감속도
     * @param dpDistance 각축의 속도 비율 배열
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmOverrideLineAccelVelDecel(int nCoordNo, double dOverrideVelocity, double dMaxAccel, double dMaxDecel, double[] dpDistance);
	
	/**
     * @brief AxmOverrideVelAtPos에 추가적으로 AccelDecel 오버라이드
     * 
     * @param nAxisNo 축 번호
     * @param dPos 위치
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param dOverridePos 오버라이드 위치
     * @param dOverrideVel 오버라이드 속도
     * @param dOverrideAccel 오버라이드 가속도
     * @param dOverrideDecel 오버라이드 감속도
     * @param nTarget 오버라이드 대상 (0: COMMAND, 1: ACTUAL)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmOverrideAccelVelDecelAtPos(int nAxisNo, double dPos, double dVel, double dAccel, double dDecel, double dOverridePos, double dOverrideVel, double dOverrideAccel, double dOverrideDecel, int nTarget);
    
    /**
     * @brief 하나의 마스터 축에 다수의 슬레이브 축들 연결 (Electronic Gearing)
     * 
     * @param nMasterAxisNo 마스터 축 번호
     * @param nSize 슬레이브 축 개수 (MAX: 8)
     * @param npSlaveAxisNo 슬레이브 축 번호 배열
     * @param dpGearRatio 마스터 축 기준으로 하는 슬레이브 축 비율 배열 (0은 제외, 1 = 100%)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmEGearSet(int nMasterAxisNo, int nSize, int[] npSlaveAxisNo, double[] dpGearRatio);
    
	/**
     * @brief Electronic Gearing 정보 확인
     * 
     * @param nMasterAxisNo 마스터 축 번호
     * @param npSize 슬레이브 축 개수 저장 (MAX: 8)
     * @param npSlaveAxisNo 슬레이브 축 번호 배열 저장
     * @param dpGearRatio 마스터 축 기준으로 하는 슬레이브 축 비율 배열 저장 (0은 제외, 1 = 100%)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmEGearGet(int nMasterAxisNo, ref int npSize, int[] npSlaveAxisNo, double[] dpGearRatio);
    
	/**
     * @brief Electronic Gearing 해제
     * 
     * @param nMasterAxisNo 마스터 축 번호
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmEGearReset(int nMasterAxisNo);
    
	/**
     * @brief Electronic Gearing 활성화 여부 설정
     * 
     * @param nMasterAxisNo 마스터 축 번호
     * @param uEnable 활성화 여부 (0: Disable, 1: Enable)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmEGearEnable(int nMasterAxisNo, uint uEnable);
    
	/**
     * @brief Electronic Gearing 활성화 여부 확인
     * 
     * @param nMasterAxisNo 마스터 축 번호
     * @param upEnable 활성화 여부 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmEGearIsEnable(int nMasterAxisNo, ref uint upEnable);

    /**
     * @brief 임의의 축을 중심으로 회전하는 헬리컬 보간 구동 지원
     * 
     * @param nCoordNo 좌표계 번호
     * @param dpFirstCenterPos 중심 위치 1 배열
     * @param dpSecondCenterPos 중심 위치 2 배열
     * @param dPitch 이동량(mm) / 1 Revolution
     * @param dTraverseDistance 이동량(mm)
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param uCWDir 방향 값
     *
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHelixPitchMove(int nCoordNo, double[] dpFirstCenterPos, double[] dpSecondCenterPos, double dPitch, double dTraverseDistance, double dVel, double dAccel, double dDecel, uint uCWDir);
	// 1차원 보정 함수
    
    /**
     * @brief 1차원 보정 테이블 설정
     * 
     * @param nTableNo 1차원 보정 테이블 번호
     * @param nSourceAxis 기준 축
     * @param nTargetAxis 목표 축
	 * @param nSize 보정 위치 개수
     * @param dpMotorPosition 보정 기준 위치
     * @param dpLoadPosition 보정 위치
	 *
	 * @note
	 * 보정 테이블 범위를 넘어서 구동 할 경우: 보상 테이블의 맨 처음과 맨 마지막의 보정값은 항상 "0"으로 설정. 즉 pdMotorPosition과 pdLoadPosition 값이 동일해야 함
	 * 보정값 측정 축(lSourceAxis)과 보정값 적용 축(lTargetAxis)에 같은 축, 혹은 다른 축을 설정 할 수 있음
	 * 보정 테이블의 위치 값은 절대 위치로 설정
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationOneDimSet(int nTableNo, int nSourceAxis, int nTargetAxis, int nSize, double[] dpMotorPosition, double[] dpLoadPosition);
    [DllImport("AXL.dll")] public static extern uint AxmCompensationOneDimSet(int nTableNo, int nSourceAxis, int nTargetAxis, int nSize, double[] dpMotorPosition, double[] dpLoadPosition, uint dwInterpolationMethod);
    [DllImport("AXL.dll")] public static extern uint AxmCompensationOneDimSet(int nTableNo, int nSourceAxis, int nTargetAxis, int nSize, double[] dpMotorPosition, double[] dpLoadPosition, uint dwInterpolationMethod, uint dwUseReverse);
    [DllImport("AXL.dll")] public static extern uint AxmCompensationOneDimSet(int nTableNo, int nSourceAxis, int nTargetAxis, int nSize, double[] dpMotorPosition, double[] dpLoadPosition, uint dwInterpolationMethod, uint dwUseReverse, double[] dpLoadReversePosition);
    [DllImport("AXL.dll")] public static extern uint AxmCompensationOneDimGet(int nTableNo, ref int npSourceAxis, ref int npTargetAxis, ref int npSize, double[] dpMotorPosition, double[] dpLoadPosition);
	
	/**
     * @brief 1 차원 보정 테이블 설정 해제
     * 
     * @param nTableNo 1차원 보정 테이블 번호
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationOneDimReset(int nTableNo);
    [DllImport("AXL.dll")] public static extern uint AxmCompensationOneDimIsSet(int lTableNo, ref uint dwpSet);
	
	/**
     * @brief 1 차원 보정 테이블 활성화 여부 설정
     * 
     * @param nTableNo 1차원 보정 테이블 번호
     * @param dwEnable 활성화 여부 (0: Disable, 1: Enable)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationOneDimEnable(int nTableNo, uint dwEnable);
	
	/**
     * @brief 서보 타입 슬레이브 기기의 보정 활성화 여부 확인
     * 
     * @param nTableNo 1차원 보정 테이블 번호
     * @param dwpEnable 활성화 여부 값 저장 (0: Disable, 1: Enable)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationOneDimIsEnable(int nTableNo, ref uint dwpEnable);
    
    /**
     * @brief 현재 지령 위치의 보정값 반환
     *
     * @param nTableNo 1차원 보정 테이블 번호
     * @param pdCorrection 현재 지령 위치에서의 보정 위치 값 저장
     *
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationOneDimGetCorrection(int nTableNo, ref double pdCorrection);

    /**
     * @brief 역방향 구동 보정 테이블 설정
     *
     * @param nTableNo 1차원 보정 테이블 번호
     * @param dpLoadPosition 역방향 구동 보정 테이블의 보정 위치 배열
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationOneDimSetReverse(int lTableNo, double[] dpLoadPosition);
    
	/**
     * @brief 역방향 구동 보정 테이블 설정 확인
     *
     * @param nTableNo 1차원 보정 테이블 번호
     * @param dpLoadPosition 역방향 구동 보정 테이블의 보정 위치 배열 확인
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationOneDimGetReverse(int lTableNo, double[] dpLoadPosition);
    
	/**
     * @brief 현재 지령 위치에서의 역방향 구동 보정값 반환
     *
     * @param nTableNo 1차원 보정 테이블 번호
     * @param pdCorrection 현재 지령 위치에서의 역방향 보정 위치 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationOneDimGetCorrectionReverse(int lTableNo, ref double pdCorrection);

    /**
     * @brief 1차원 보정 테이블을 File 참조하여 설정 (파일 확장자: cmp1)
     *
     * @param nTableNo 1차원 보정 테이블 번호
     * @param lSourceAxis 보정값 측정 축
     * @param lTargetAxis 보정값 적용 축
     * @param szFilePath 파일 경로
     * @param dwInterpolationMethod 보간 방법 설정
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationOneDimFileLoad(int nTableNo, int nSourceAxis, int nTargetAxis, string szFilePath);
    [DllImport("AXL.dll")] public static extern uint AxmCompensationOneDimFileLoad(int nTableNo, int nSourceAxis, int nTargetAxis, string szFilePath, uint dwInterpolationMethod);
    [DllImport("AXL.dll")] public static extern uint AxmCompensationOneDimFileLoad(int nTableNo, int nSourceAxis, int nTargetAxis, char[] szFilePath);
    [DllImport("AXL.dll")] public static extern uint AxmCompensationOneDimFileLoad(int nTableNo, int nSourceAxis, int nTargetAxis, char[] szFilePath, uint dwInterpolationMethod);
    
	/**
     * @brief 1차원 보정 테이블을 File로 저장 (파일 확장자: cmp1)
     *
     * @param nTableNo 1차원 보정 테이블 번호
     * @param szFilePath 파일 경로
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationOneDimFileSave(int nTableNo, string szFilePath);
    [DllImport("AXL.dll")] public static extern uint AxmCompensationOneDimFileSave(int nTableNo, char[] szFilePath);

    /**
     * @brief 1차원 보정 테이블의 보간 방법 설정
     *
     * @param nTableNo 1차원 보정 테이블 번호
     * @param uMethod 설정 할 보간 방법
	 *
	 * @details
	 * dwMethod
	 *    [0] INTERPOLATION_LINEAR: Linear interpolation
	 *    [1] INTERPOLATION_CUBIC_SPLINE: Cubic spline interpolation
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationOneDimSetInterpolationMethod(int lTableNo, uint uMethod);
    
	/**
     * @brief 1차원 보정 테이블의 보간 방법 확인
     *
     * @param nTableNo 1차원 보정 테이블 번호
     * @param upMethod 설정 할 보간 방법 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationOneDimGetInterpolationMethod(int lTableNo, ref uint upMethod);

    // 2차원 보정함수 

    /**
     * @brief 2차원 보정 테이블 설정
     *
     * @param nTableNo 2차원 보정 테이블 번호
     * @param nSourceAxis1 보정 기준 위치 축 1
     * @param nSourceAxis2 보정 기준 위치 축 2
     * @param nTargetAxis1 보정값 적용 축 1
     * @param nTargetAxis2 보정값 적용 축 2
     * @param nSize1 보정 기준 위치 정보(dpMotorPosition1) 개수 
     * @param nSize2 보정 기준 위치 정보(dpMotorPosition2) 개수
     * @param dpMotorPosition1 lSourceAxis1의 지령 위치와 비교되는 보정 기준 위치
     * @param dpMotorPosition2 lSourceAxis2의 지령 위치와 비교되는 보정 기준 위치
     * @param dpLoadPosition1 lTargetAxis1 축에 적용 될 보정량 (개수: lSize1 * lSize2)
     * @param dpLoadPosition2 lTargetAxis2 축에 적용 될 보정량 (개수: lSize1 * lSize2)
     * 
	 * @note
	 * 보정 테이블 범위를 넘어서 구동 할 경우: 보상 테이블의 테두리 보정 값은 항상 "0"으로 설정. 즉 pdMotorPosition과 pdLoadPosition 값이 동일해야 함
	 * pdMotorPosition1는 lAxis[0]의 모터 위치,  pdMotorPosition2는 lAxis[1]의 모터 위치
	 * lSize 개수 범위: 2 < lSize < 512
	 * dpLoadPosition 보정값 개수: lSize1 * lSize2
	 * 보정값 측정 축(lSourceAxis)과 보정값 적용 축(lTargetAxis)에 같은 축, 혹은 다른 축을 설정 할 수 있음
	 * 보정 테이블의 위치 값은 절대 위치로 설정
	 * ECAT-HW의 경우:
	 *    최대 보드 4장, 각 보드마다 4 테이블을 지원하며, 테이블 번호는 (보드 번호 * 4 + 보드 내 테이블 번호) 임
	 *    Board 0: 0 ~ 3, Board 1: 4 ~ 7, Board 2: 8 ~ 11, Board 3: 12 ~ 15 Table
	 * Reserved
	 *
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationTwoDimSet(int nTableNo, int nSourceAxis1, int nSourceAxis2, int nTargetAxis1, int nTargetAxis2, int nSize1, int nSize2, double[] dpMotorPosition1, double[] dpMotorPosition2, double[] dpLoadPosition1, double[] dpLoadPosition2);

    /**
     * @brief 2차원 보정 테이블 설정 정보 확인
     *
     * @param nTableNo 2차원 보정 테이블 번호
     * @param nSourceAxis1 보정 기준 위치 축 1 저장
     * @param nSourceAxis2 보정 기준 위치 축 2 저장
     * @param nTargetAxis1 보정값 적용 축 1 저장
     * @param nTargetAxis2 보정값 적용 축 2 저장
     * @param nSize1 보정 기준 위치 정보(dpMotorPosition1) 개수 저장
     * @param nSize2 보정 기준 위치 정보(dpMotorPosition2) 개수 저장
     * @param dpMotorPosition1 lSourceAxis1의 지령 위치와 비교되는 보정 기준 위치 저장
     * @param dpMotorPosition2 lSourceAxis2의 지령 위치와 비교되는 보정 기준 위치 저장
     * @param dpLoadPosition1 lTargetAxis1 축에 적용 될 보정량 저장 (개수: lSize1 * lSize2)
     * @param dpLoadPosition2 lTargetAxis2 축에 적용 될 보정량 저장 (개수: lSize1 * lSize2)
	 *
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationTwoDimGet(int nTableNo, ref int npSourceAxis1, ref int npSourceAxis2, ref int npTargetAxis1, ref int npTargetAxis2, ref int npSize1, ref int npSize2, double[] dpMotorPosition1, double[] dpMotorPosition2, double[] dpLoadPosition1, double[] dpLoadPosition2);

    /**
     * @brief 2차원 보정 테이블 초기화
     *
     * @param nTableNo 2차원 보정 테이블 번호
     * 
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationTwoDimReset(int nTableNo);

    /**
     * @brief 2차원 보정 테이블 설정 여부 확인
     *
     * @param nTableNo 2차원 보정 테이블 번호
     * @param upSet 설정 여부 값 저장
     * 
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationTwoDimIsSet(int nTableNo, ref uint upSet);

    /**
     * @brief 2차원 보정 테이블 활성화 여부 설정
     *
     * @param nTableNo 2차원 보정 테이블 번호
     * @param uEnable 활성화 여부 값 (0: DISABLE, 1: ENABLE)
     * 
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationTwoDimEnable(int nTableNo, uint uEnable);

    /**
     * @brief 2차원 보정 테이블 활성 여부 확인
     *
     * @param nTableNo 2차원 보정 테이블 번호
     * @param upEnable 활성화 여부 정보 저장
     * 
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationTwoDimIsEnable(int nTableNo, ref uint upEnable);

    /**
     * @brief 2차원 보정 테이블 설정 결과 반환
     *
     * @param nTableNo 2차원 보정 테이블 번호
     * @param upTwoDimSetResult 설정 결과 값 저장
	 *
	 * @details
	 * upTwoDimSetResult
     *    0: TWODIM_SET_DEFAULT
	 *    1: TWODIM_SET_SUCCESS
	 *    2: TWODIM_SET_SEND_TABLE_INFO_FAIL
	 *    3: TWODIM_SET_SEND_MOTORPOS1_FAIL
	 *    4: TWODIM_SET_SEND_MOTORPOS2_FAIL
	 *    5: TWODIM_SET_SEND_LOADPOS1_FAIL
	 *    6: TWODIM_SET_SEND_LOADPOS2_FAIL
	 *    7: TWODIM_SET_FAIL
     * 
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationTwoDimGetResult(int nTableNo, ref uint upTwoDimSetResult);

    /**
     * @brief 현재 지령 위치에서의 보정값 반환
     *
     * @param nTableNo 2차원 보정 테이블 번호
     * @param pdCorrection 현재 지령 위치에서의 보정 위치 배열 값 저장
     * 
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationTwoDimGetCorrection(int nTableNo, double[] pdCorrection);

    /**
     * @brief 특정 지령 위치에서의 보정 값 반환
     *
     * @param lTableNo 2차원 보정 테이블 번호
     * @param pdTargetPosition 보정 값을 확인 하고자 하는 위치 지령값 배열(X, Y)
     * @param pdInterpolationValue 해당 지령 위치에서의 보정값 배열(X, Y)
     * 
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationTwoDimGetInterpolationValue(int lTableNo, double[] pdTargetPosition, double[] pdInterpolationValue);

    /**
     * @brief 2차원 보정 테이블을 File 참조하여 설정 (파일 확장자: cmp2)
     *
     * @param nTableNo 2차원 보정 테이블 번호
     * @param nSourceAxis1 보정 기준 위치 축 1
     * @param nSourceAxis2 보정 기준 위치 축 2
     * @param nTargetAxis1 보정값 적용 축 1
     * @param nTargetAxis2 보정값 적용 축 2
     * @param szFilePath 파일 경로
     * 
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationTwoDimFileLoad(int nTableNo, int nSourceAxis1, int nSourceAxis2, int nTargetAxis1, int nTargetAxis2, string szFilePath);
    [DllImport("AXL.dll")] public static extern uint AxmCompensationTwoDimFileLoad(int nTableNo, int nSourceAxis1, int nSourceAxis2, int nTargetAxis1, int nTargetAxis2, char[] szFilePath);

    /**
     * @brief 2차원 보정 테이블 File로 저장 (파일 확장자: cmp2)
     *
     * @param nTableNo 2차원 보정 테이블 번호
     * @param szFilePath 파일 경로
     * 
     * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmCompensationTwoDimFileSave(int nTableNo, string szFilePath);
    [DllImport("AXL.dll")] public static extern uint AxmCompensationTwoDimFileSave(int nTableNo, char[] szFilePath);
    
    /**
     * @brief Blending 모션 설정
     *
     * @param nCoordNo 좌표계 번호
     * @param dValue 첫 번째 모션 세그먼트의 종료 시점 기준으로하는 블렌딩이 시작되는 시점
     * @param uMethod 블렌딩 모드 설정 (0: 거리(mm), 1: 시간(msec), 2: 거리 비율(%))
     * 
	 * @note 자동 가감속, 자동 원호 삽입 모드에서는 Blending 모드가 해제 됨
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmBlendingCoordSet(int nCoordNo, double dValue, uint uMethod);
	
	/**
     * @brief Blending 모션 설정 확인
     *
     * @param nCoordNo 좌표계 번호
     * @param dValue 첫 번째 모션 세그먼트의 종료 시점 기준으로하는 블렌딩이 시작되는 시점 저장
     * @param upMethod 블렌딩 모드 설정 값 저장 (0: 거리(mm), 1: 시간(msec), 2: 거리 비율(%))
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmBlendingCoordGet(int nCoordNo, ref double dpValue, ref uint upMethod);
	
	/**
     * @brief Blending 모션 설정 초기화
     *
     * @param nCoordNo 좌표계 번호
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmBlendingCoordReset(int nCoordNo);
	
	/**
     * @brief Blending 모션 활성화 여부 설정
     *
     * @param nCoordNo 좌표계 번호
     * @param uEnable 활성화 여부 (0: Disable(비활성화), 1: 활성화(Enable))
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmBlendingCoordEnable(int nCoordNo, uint uEnable);
	
	/**
     * @brief Blending 모션이 활성화 여부 확인
     *
     * @param nCoordNo 좌표계 번호
     * @param upEnable 활성화 여부 저장 (0: Disable(비활성화), 1: 활성화(Enable))
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmBlendingCoordIsEnable(int nCoordNo, ref uint upEnable);
    
    /**
     * @brief 해당 축의 Torque 값 반환
     *
     * @param nAxisNo 축 번호
     * @param dpTorque 반환 할 Torque 값 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadTorque(int nAxisNo, ref double dpTorque);
    
    /**
     * @brief 지정 축의 종료 속도 설정
     *
     * @param nAxisNo 축 번호
     * @param dEndVelocity 종료 속도 값
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetEndVel(int nAxisNo, double dEndVelocity);
    
    /**
     * @brief 지정 축의 종료 속도 설정 확인
     *
     * @param nAxisNo 축 번호
     * @param dpEndVelocity 종료 속도 값 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetEndVel(int nAxisNo, ref double dpEndVelocity);


    /**
	 * @brief 시작점과 종료점 지정하여 다축 직선 보간 구동
	 *
	 * @param nCoord 좌표계 번호
	 * @param nArraySize 축 개수
	 * @param npAxisNo 축 번호 배열
	 * @param dpEndPos 종료점 배열
	 * @param dVel 속도
	 * @param dAccel 가속도
	 * @param dDecel 감속도
	 * 
	 * @note
	 * 구동 시작 후 함수를 벗어 남
	 * AxmContiBeginNode, AxmContiEndNode와 같이 사용 시 지정된 좌표계에 시작점과 종료점을 지정하여 직선 보간 구동 Queue에 함수 저장
	 * 직선 프로파일 연속 보간 구동을 위해 내부 Queue에 저장하여 AxmContiStart 함수를 사용해서 시작
	 * lpAxisNo의 축 배열에 해당하는 축들은 직선 보간을 하며, 나머지 Coordinate의 축들은 직선 보간 비율에 맞게 단축 구동 수행
	 *
	 * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
	 */
    [DllImport("AXL.dll")] public static extern uint AxmLineMoveWithAxes(int nCoord, int nArraySize, ref int npAxisNo, ref double dpEndPos, double dVel, double dAccel, double dDecel);

	/**
	 * @brief 2차원/3차원 원호보간 및 그 이상의 축에 대해서 직선보간 
	 *
	 * @param nCoord 좌표계 번호
	 * @param nArraySize 원호보간을 할 축 개수 (2 or 3)
	 * @param npAxisNo 원호 보간을 할 축 배열
	 * @param dpCenterPosition 원호 보간에 사용 할 중심점 배열
	 * @param dpEndPosition 원호 보간에 사용 할 종료점 배열
	 * @param dMaxVelocity 최대 속도
	 * @param dMaxAccel 최대 가속도
	 * @param dMaxDecel 최대 감속도
	 * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
	 * @param b3DCircle 2차원 원호 보간 및 그 이상의 축에 대해 직선 보간 여부 (0: 2차원, 1: 3차원)
	 * 
	 * @note
	 * 시작점, 종료점과 중심점을 지정하여 원호 보간 구동하는 함수. 구동 시작 후 함수를 벗어 남
	 * AxmContiBeginNode, AxmContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 종료점과 중심점을 지정하여 구동하는 원호 보간 구동 Queue에 함수 저장
	 * 프로파일 원호 연속 보간 구동을 위해 내부 Queue에 저장하여 AxmContiStart 함수를 사용해서 시작
	 * 2차원, 3차원 이상의 축에 대해서는 직선 보간을 할 때 dEndPosition의 값을 Targetposition으로 사용
	 * dCenterPosition: AxmContiSetAxisMap에서 맵핑한 축 번호 순서에 맞는 배열 위치 값 입력
	 * dEndPos: AxmContiSetAxisMap에서 맵핑한 축 번호 순서에 맞는 배열 위치 값 입력
	 *
	 * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
	 */
    [DllImport("AXL.dll")] public static extern uint AxmCircleCenterMoveWithAxes(int nCoord, int nArraySize, ref int npAxisNo, ref double dpCenterPosition, ref double dpEndPosition, double dMaxVelocity, double dMaxAccel, double dMaxDecel, uint uCWDir, bool b3DCircle);
    
	/**
	 * @brief 시작점, 종료점과 반지름 지정하여 원호 보간 구동
	 *
	 * @param nCoord 좌표계 번호
	 * @param nArraySize 원호보간을 할 축 개수 (2 or 3)
	 * @param npAxisNo 원호 보간을 할 축 배열
	 * @param dRadius 원의 반지름
	 * @param dpEndPos 원호 보간 시 종료점 배열
	 * @param dVel 속도
	 * @param dAccel 가속도
	 * @param dDecel 감속도
	 * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
	 * @param uShortDistance 작은원 or 큰원 설정 (0: 작은원, 1: 큰원)
	 * 
	 * @note
	 * 구동 시작 후 함수를 벗어 남
	 * AxmContiBeginNode, AxmContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 종료점과 반지름을 지정하여 원호 보간 구동 Queue에 함수 저장
	 * 프로파일 원호 연속 보간 구동을 위해 내부 Queue에 저장하여 AxmContiStart 함수를 사용해서 시작
	 * dEndPos: AxmContiSetAxisMap에서 맵핑한 축 번호 순서에 맞는 배열 위치 값 입력
	 *
	 * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
	 */
    [DllImport("AXL.dll")] public static extern uint AxmCircleRadiusMoveWithAxes(int nCoord, int nArraySize, ref int npAxisNo, double dRadius, ref double dpEndPos, double dVel, double dAccel, double dDecel, uint uCWDir, uint uShortDistance);
    
	/**
	 * @brief 시작점, 회전 각도와 반지름 지정하여 원호 보간 구동 구동
	 *
	 * @param nCoord 좌계표 번호
	 * @param nArraySize 원호보간을 할 축 개수 (2 or 3)
	 * @param npAxisNo 원호 보간을 할 축 배열
	 * @param dpCenterPos 원호 보간 시 중심점 배열
	 * @param dAngle 회전 각도
	 * @param dVel 속도
	 * @param dAccel 가속도
	 * @param dDecel 감속도
	 * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
	 * 
	 * @note
	 * 구동 시작 후 함수를 벗어 남
	 * AxmContiBeginNode, AxmContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 회전 각도와 반지름을 지정하여 원호 보간 구동 Queue에 함수 저장
	 * 프로파일 원호 연속 보간 구동을 위해 내부 Queue에 저장하여 AxmContiStart 함수를 사용해서 시작
	 *
	 * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
	 */
    [DllImport("AXL.dll")] public static extern uint AxmCircleAngleMoveWithAxes(int nCoord, int nArraySize, ref int npAxisNo, ref double dpCenterPos, double dAngle, double dVel, double dAccel, double dDecel, uint uCWDir);
    
	/**
	 * @brief 2차원/3차원 원호보간 및 그 이상의 축에 대해서 직선 보간
	 *
	 * @param nCoordNo 좌표계 번호
	 * @param nArraySize 원호보간을 할 축 개수 (2 or 3)
	 * @param npAxisNo 원호 보간을 할 축 배열
	 * @param dpMidPos 원호 보간 시 중간점 배열
	 * @param dpEndPos 원호 보간 시 종료점 배열
	 * @param dVel 속도
	 * @param dAccel 가속도
	 * @param dDecel 감속도
	 * @param nArcCircle 원호 종류 (0: 아크, 1: 원)
	 *
	 * @note
	 * 시작 위치에서 중간점, 종료점을 지정하여 원호 보간 구동하는 함수. 구동 시작 후 함수를 벗어 남
	 * AxmContiBeginNode, AxmContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 종료점과 중심점을 지정하여 구동하는 원호 보간 구동 Queue에 함수 저장
	 * 프로파일 원호 연속 보간 구동을 위해 내부 Queue에 저장하여 AxmContiStart 함수를 사용해서 시작
	 * 2차원/3차원 이상의 축에 대해서는 직선 보간을 할 때 dEndPosition의 값을 Targetposition으로 사용
	 * dpMidPos/dpEndPos: AxmContiSetAxisMap에서 맵핑한 축 번호 순서에 맞는 배열 위치 입력
	 * 
	 * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
	 */
    [DllImport("AXL.dll")] public static extern uint AxmCirclePointMoveWithAxes(int nCoord, int nArraySize, ref int npAxisNo, ref double dpMidPos, ref double dpEndPos, double dVel, double dAccel, double dDecel, int nArcCircle);
    
	/**
	 * @brief 시작점과 종료점 지정하여 다축 직선 보간 구동 구동
	 *
	 * @param nCoord 좌표계 번호
	 * @param nArraySize 보간에 사용되는 축 개수
	 * @param npAxisNo 보간에 사용되는 축 배열
	 * @param dpEndPos 종료점 배열
	 * @param dVel 속도
	 * @param dAccel 가속도
	 * @param dDecel 감속도
	 * @param uEventFlag EzMonitor 프로그램을 통한 관측용 EventFlag (0: 비활성화, 1: 보간 종료 시점 Event 1회)
	 *
	 * @note
	 * 구동 시작 후 함수를 벗어 남 
	 * AxmContiBeginNode, AxmContiEndNode와 같이 사용 시 지정된 좌표계에 시작점, 종료점과 중심점을 지정하여 구동하는 원호 보간 구동 Queue에 함수 저장
	 * 직선 프로파일 연속 보간 구동을 위해 내부 Queue에 저장, AxmContiStart 함수를 사용해서 시작
	 * lpAxisNo의 축 배열에 해당하는 축들은 직선 보간을 하며 나머지 Coordinate의 축들은 직선 보간 비율에 맞게 단축 구동 수행
	 * 
	 * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
	 */
    [DllImport("AXL.dll")] public static extern uint AxmLineMoveWithAxesWithEvent(int nCoord, int nArraySize, ref int npAxisNo, ref double dpEndPos, double dVel, double dAccel, double dDecel, uint uEventFlag);
    
	/**
	 * @brief 2차원/3차원 원호보간 및 그 이상의 축에 대해서 직선보간 
	 *
	 * @param nCoord 좌표계 번호
	 * @param nArraySize 보간에 사용되는 축 개수
	 * @param npAxisNo 보간에 사용되는 축 배열
	 * @param dCenterPosition 중심점 배열
	 * @param dEndPosition 종료점 배열
	 * @param dMaxVelocity 최대 속도
	 * @param dMaxAccel 최대 가속도
	 * @param dMaxDecel 최대 감속도
	 * @param uCWDir 회전 방향 (0: DIR_CCW(반시계방향), 1: DIR_CW(시계방향))
	 * @param u3DCircle 2차원 원호보간 및 그 이상의 축에 대해서 직선보간 여부 (0: 2차원, 1: 3차원)
	 * @param uEventFlag EzMonitor 프로그램을 통한 관측용 EventFlag (0: 비활성화, 1: 보간 종료 시점 Event 1회)
	 * 
	 * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
	 */
    [DllImport("AXL.dll")] public static extern uint AxmCircleCenterMoveWithAxesWithEvent(int nCoord, int nArraySize, ref int npAxisNo, ref double dpCenterPosition, ref double dpEndPosition, double dMaxVelocity, double dMaxAccel, double dMaxDecel, uint uCWDir, uint u3DCircle, uint uEventFlag);

    /**
	 * @brief 단축 PVT 구동 
	 *
	 * @param nAxisNo 구동 축
	 * @param uArraySize PVT Table size
	 * @param pdPos Position 배열
	 * @param pdVel Velocity 배열
	 * @param upUsec Time 배열 (단위: usec)
	 *
	 * @note
	 * 사용자가 Position, Velocity, Time Table을 이용하여 생성한 프로파일 구동
	 * AxmSyncBegin, AxmSyncEnd API와 함께 사용시 여러 축의 PVT 구동 예약
	 * 예약된 PVT 구동 프로파일은 AxmSyncStart 명령을 받게되면 동시 시작
	 * 주의 사항: Time 배열 값은 Cycle의 배수여야 함
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
	 */
    [DllImport("AXL.dll")] public static extern uint AxmMovePVT(int nAxisNo, uint uArraySize, ref double dpPos, ref double dpVel, ref uint upUsec);
	
	// Sync 함수
	
	/**
     * @brief Sync 구동에 사용 될 유효 축 설정
     *
     * @param nSyncNo Sync Index 번호
     * @param nSize 맵핑 축 개수
     * @param npAxesNo 맵핑 축 배열
	 *
	 * @note
	 * 지정된 Sync No에서 사용 할 축 맵핑 (맵핑 번호는 0부터 시작)
	 * SyncBegin과 SyncEnd 사이에 사용되는 PVT Motion의 지정 축이 맵핑되지 않은 축일 경우 예약되지 않고 즉시 구동.
	 * 즉, 맵핑된 축만 Begin과 End사이에서 구동 예약이 되며 SyncStart를 호출하면 지정된 Sync Index에서 예약된 구동 동시 시작.
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
	[DllImport("AXL.dll")] public static extern uint AxmSyncSetAxisMap(int nSyncNo, int nSize, ref int npAxesNo);

	/**
     * @brief 지정된 Sync Index에 할당된 축 맵핑과 예약 프로파일 초기화
     *
     * @param nSyncNo Sync Index 번호
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
	[DllImport("AXL.dll")] public static extern uint AxmSyncClear(int nSyncNo);

	/**
     * @brief 지정된 Sync Index에 수행할 구동 예약 시작
     *
     * @param nSyncNo Sync Index 번호
	 *
	 * @note
	 * 이 함수 호출 후 AxmSyncEnd 함수가 호출되기 전까지 실행되는 유효 축의 PVT 구동은 실제 구동을 즉시 수행하지 않고
	 * 구동 예약이 되며 AxmSyncStart 함수가 호출될 때 비로소 예약된 구동 수행
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
	[DllImport("AXL.dll")] public static extern uint AxmSyncBegin(int nSyncNo);

	/**
     * @brief 지정된 Sync Index에서 수행할 구동 예약 종료
     *
     * @param nSyncNo Sync Index 번호
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
	[DllImport("AXL.dll")] public static extern uint AxmSyncEnd(int nSyncNo);

	/**
     * @brief 지정된 Sync Index에서 예약된 구동 시작
     *
     * @param nSyncNo Sync Index 번호
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
	[DllImport("AXL.dll")] public static extern uint AxmSyncStart(int nSyncNo);


	// Soft Landing 함수
	
	/**
     * @brief 단축 Soft Landing 구동 수행 정보 설정
     *
     * @param nAxisNo 구동 축
     * @param dPos1 Down 동작의 목표 위치
     * @param dPos2 Up 동작의 목표 위치
     * @param dVel 구동 속도
     * @param dAccel 구동 가속도
     * @param dDecel 구동 감속도
     * @param dOverridePos1 Down 동작 중 Override 위치
     * @param dOverrideVel1 Down 동작 중 Override 속도
     * @param uTorqueLimit1 Down 동작 중 설정할 Torque Limit [0.1%]
     * @param uTorqueLimitDelay1 Down 동작 중 OverridePos1 도달 이후 TorqueLimit 설정까지의 Delay [msec]
     * @param dOverridePos2 Up 동작 중 Override 위치
     * @param dOverrideVel2 Up 동작 중 Override 속도
     * @param uTorqueLimit2 Up 동작 중 설정할 Torque Limit [0.1%]
     * @param uTouchTimeOut Touch 동작 확인에 대한 TimeOut [msec]
     * @param uPushTime Touch 확인 후 Push할 시간 [msec]
     * @param dPushOffsetPos Touch 후 표면에서 Position Gap 조절할 OffsetPos
     * @param uTorqueMargin Touch 확인에서 TorqueLimit1 대비 Actual Torque Range 설정 [%]
     * @param dTouchVel Touch 확인에서 최대 Velocity 설정
     * 
	 * @note Soft Landing 동작 설명
	 *    1. dPos1으로 Down 동작 수행
	 *    2. Down 중 OverridePos1에서 OverrideVel1로 속도 Override
	 *    3. OverridePos1 도달 후 TorqueLimitDelay[msec] 후에 TorqueLimit1[0.1%]에 해당하는 Torque Limit 설정
	 *    4. dPos1에 도달하면 Touch 확인 수행 (Torque Margin[%], TouchVel 조건을 만족하면 Touch Ok)
	 *    5. Touch가 확인되면 Push Time[msec] 동안 대기
	 *    6. Push Time이 지나면 dPos2로 Up 동작 수행
	 *    7. Up 중 OverridePos2에서 OverrideVel2로 속도 Override
	 *    8. OverridePos2에 도달하면 TorqueLimit2로 Torque Limit 설정
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
	[DllImport("AXL.dll")] public static extern uint AxmMotSetSoftLandingInfo(int nAxisNo, double dOverridePos1, double dOverrideVel1, ushort uTorqueLimit1, uint uTorqueLimitDelay1, double dOverridePos2, double dOverrideVel2, ushort uTorqueLimit2, uint uTouchTimeOut, uint uPushTime, uint dPushOffsetPos, ushort uTorqueMargin, double dTouchVel);
	
	/**
     * @brief Soft Landing 동작 중 발생한 에러 상태와 에러 코드 반환
     *
     * @param nAxisNo 구동 축
     * @param npStatus 에러 상태 저장
     * @param npErrorCode 에러 코드 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
	[DllImport("AXL.dll")] public static extern uint AxmMotGetSoftLandingError(int nAxisNo, ref int npStatus, ref int npErrorCode);
	
	/**
     * @brief 단축 Soft Landing 구동 수행
     *
     * @param nAxisNo 구동 축
     * @param dPos1 Down 동작의 목표 위치
     * @param dPos2 Up 동작의 목표 위치
     * @param dVel 구동 속도
     * @param dAccel 구동 가속도
     * @param dDecel 구동 감속도
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
	[DllImport("AXL.dll")] public static extern uint AxmMoveSoftLanding(int nAxisNo, double dPos1, double dPos2, double dVel, double dAccel, double dDecel);

	// Digital output with pos event 함수
	
	/**
     * @brief 해당 축의 위치를 기반으로 Digital Output의 출력 동작 수행
     *
     * @param nAxisNo 타겟 축
     * @param dComparePosition 타겟 위치
     * @param uPositionSource Position의 Source(0: COMMAND, 1: ACTUAL)
     * @param uModuleType 출력할 Digital Output의 Type
     * @param uModuleNo 출력할 Module의 Index
     * @param uBit 출력할 Module 내 bit offset
     * @param uOutputMode 출력 종류
     * @param uDelayTime Delay 시간
	 *
	 * @details
	 * dwModuleType: 0: Motion I/O module, 1: Digital output module
	 * dwModuleNo: Motion I/O module의 경우 축 번호, Digital output module의 경우 모듈 번호
	 * dwOutputMode
	 *    [0] Off: 출력 bit off
     *    [1] On: 출력 bit on
	 *    [2] OffOn: 출력 bit off -> Delay time -> bit on
	 *    [3] OnOff: 출력 bit on -> Delay time -> bit off
	 * dwDelayTime: OutputMode 중 OffOn, OnOff에서 사용할 Delay 시간
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
	[DllImport("AXL.dll")] public static extern uint AxmDigitalOutputBitWithPosEvent(int nAxisNo, double dComparePosition, uint uPositionSource, uint uModuleType, uint uModuleNo, uint uBit, uint uOutputMode, uint uDelayTime);

    // Move 함수 파라미터 Get 함수 추가 2023.10.31 LeeSeokGi
	
	/**
     * @brief AxmMoveStartPos 함수 구동 설정 값 확인
     *
     * @param nAxisNo 구동 축 저장
     * @param dpPos 위치 값 저장
     * @param dpVel 속도 값 저장
     * @param dpAccel 가속도 값 저장
     * @param dpDecel 감속도 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveGetStartPosParam(ref int npAxisNo, ref double dpPos, ref double dpVel, ref double dpAccel, ref double dpDecel);
	
	/**
     * @brief  AxmMovePos 함수 구동 설정 값 확인
     *
     * @param nAxisNo 구동 축 저장
     * @param dpPos 위치 값 저장
     * @param dpVel 속도 값 저장
     * @param dpAccel 가속도 값 저장
     * @param dpDecel 감속도 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveGetPosParam(ref int npAxisNo, ref double dpPos, ref double dpVel, ref double dpAccel, ref double dpDecel);
	
	/**
     * @brief  AxmMoveVel 함수 구동 설정 값 확인
     *
     * @param nAxisNo 구동 축 저장
     * @param dpVel 속도 값 저장
     * @param dpAccel 가속도 값 저장
     * @param dpDecel 감속도 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveGetVelParam(ref int npAxisNo, ref double dpVel, ref double dpAccel, ref double dpDecel);
	
	/**
     * @brief  AxmMoveStartMultiPos 함수 구동 설정 값 확인
     *
     * @param npArraySize 구동 축 배열 저장
     * @param lpAxesNoArrGet 속도 배열 값 저장
     * @param dpPosArrGet 위치 배열 값 저장
     * @param dpVelArrGet 속도 배열 값 저장
	 * @param dpAccelArrGet 가속도 배열 값 저장
     * @param dpDecelArrGet 감속도 배열 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveGetStartMultiPosParam(ref int npArraySize, double[] lpAxesNoArrGet, double[] dpPosArrGet, double[] dpVelArrGet, double[] dpAccelArrGet, double[] dpDecelArrGet);
	
	/**
     * @brief  AxmMoveToAbsPos 함수 구동 설정 값 확인
     *
     * @param npAxisNo 구동 축 저장
     * @param dpPos 위치 값 저장
     * @param dpVel 속도 값 저장
     * @param dpAccel 가속도 값 저장
     * @param dpDecel 감속도 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveGetToAbsPosParam(ref int npAxisNo, ref double dpPos, ref double dpVel, ref double dpAccel, ref double dpDecel);
	
	/**
     * @brief 최종 move 함수 구동 설정 값 확인
     *
     * @param nAxisNo 구동 축
     * @param dpPos 위치 값 저장
     * @param dpVel 속도 값 저장
     * @param dpAccel 가속도 값 저장
     * @param dpDecel 감속도 값 저장
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveGetLastParam(int nAxisNo, ref double dpPos, ref double dpVel, ref double dpAccel, ref double dpDecel);

	/**
     * @brief AxmSignalWriteOutputBitToggleDelay 함수 사용 전 초기화
     *
     * @param nAxisNo 축 번호
     *	
	 * @note 지원 제품: N404/N804(CAMC-QI) 보드 전용     
	 * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
	[DllImport("AXL.dll")] public static extern uint AxmSignalWriteOutputBitToggleDelayInit (int lAxisNo);

	/**
     * @brief 모션의 범용 출력 접점(2~4번) 중 지정된 접점을 즉시 반전(Toggle) 출력하고, 지정된 시간 동안(1msec ~ 10sec) 유지후 자동으로 기존 상태로 출력
     *
     * @param nAxisNo 축 번호
     * @param dwIONum 범용 출력 접점 번호 (2 ~ 4번만 가능)
     * @param dDelayTimeMs 반전 유지시간 (범위: 1 ~ 10,000, 단위: msec)
     *	
	 * @note 지원 제품: N404/N804(CAMC-QI) 보드 전용
	 * 
	 * @warning 주의 사항: 본 기능 사용 시, 동일 범용 출력 접점을 다른 용도로 동시에 사용하면 안 됩니다.
	 * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
	[DllImport("AXL.dll")] public static extern uint AxmSignalWriteOutputBitToggleDelay(int lAxisNo, uint dwIONum, uint dDelayTimeMs);
}
