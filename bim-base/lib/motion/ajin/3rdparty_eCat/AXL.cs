/**
 * @file AXL.cs
 * 
 * @brief 아진엑스텍 라이브러리 헤더 파일
 *
 * @author 아진엑스텍 주식회사
 * 
 * @copyright 저작권 (c) 아진엑스텍 주식회사
 *
 * @website http://www.ajinextek.com
 *
 * @last_update 2024-12-15
 * 
 * @details 자세한 정보는 매뉴얼을 참고해 주세요.
 */


using System.Runtime.InteropServices;

public class CAXL
{
	// 라이브러리 초기화
	
	/**
     * @brief 라이브러리 초기화
     * 
     * @param lIrqNo ISA Type 보드 사용 시 IRQ 번호 (0 ~ 64). PCI 보드 사용 시 의미 없음.
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlOpen(int lIrqNo);
	
    /**
     * @brief 라이브러리 초기화 시 하드웨어 칩에 리셋을 하지 않음
     * 
     * @param lIrqNo ISA Type 보드 사용 시 IRQ 번호 (0 ~ 64). PCI 보드 사용 시 의미 없음.
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlOpenNoReset(uint lIrqNo);
	
    /**
     * @brief 라이브러리 사용 종료
     * 
     * @return 라이브러리 종료 실패 시 0을 반환하며, 성공 시 1을 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern int  AxlClose();
	
    /**
     * @brief 라이브러리가 초기화 여부 확인
     * 
     * @return 라이브러리가 초기화 되어 있지 않을 시 0을 반환하며, 초기화 되어 있을 시 1을 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern int  AxlIsOpened();

    /**
     * @brief 인터럽트 기능 활성화
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlInterruptEnable();
    /**
     * @brief 인터럽트 기능 비활성화
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlInterruptDisable();

	// 라이브러리 및 베이스 보드 정보

	/**
     * @brief 등록된 베이스 보드 개수 확인
     * 
     * @param lpBoardCount 베이스 보드 개수 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlGetBoardCount(ref int lpBoardCount);
    
	/**
     * @brief 라이브러리 버전 확인 (szVersion[64])
     * 
     * @param szVersion 버전 정보
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlGetLibVersion(ref byte szVersion);
	
    /**
     * @brief Network제품의 각 모듈 별 연결 상태 확인
     * 
     * @param lBoardNo 보드 번호
     * @param lModulePos 모듈 위치
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlGetModuleNodeStatus(int nBoardNo, int nModulePos);
	
    /**
     * @brief 해당 보드 제어 가능한 상태 확인
     * 
     * @param lBoardNo 보드 번호
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlGetBoardStatus(int nBoardNo);
	
    /**
     * @brief Network 제품의 Configuration Lock 상태 확인
     * 
     * @param lBoardNo 보드 번호
     * @param wpLockMode Lock 모드 저장 (0: DISABLE, 1: ENABLE)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlGetLockMode(int nBoardNo, ref uint upLockMode);

    /**
      * @brief EtherCAT Network Type의 모든 Slave들이 동작 가능한 상태인지 확인
      *      
      * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
      */
    [DllImport("AXL.dll")] public static extern uint AxlIsConnectedAllSlaves(uint uNetworkType = 5);

    /**
     * @brief 반환 값에 대한 설명 확인
     * 
     * @param dwReturnCode 반환 코드
     * @param lReturnInfoSize 반환 정보 크기
     * @param lpRecivedSize 실제로 받은 크기 저장
     * @param szReturnInfo 반환 정보 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlGetReturnCodeInfo(uint dwReturnCode, int lReturnInfoSize, ref int lpRecivedSize, char[] szReturnInfo);
    [DllImport("AXL.dll")] public static extern uint AxlGetReturnCodeInfo(uint dwReturnCode, int lReturnInfoSize, ref int lpRecivedSize, ref string szReturnInfo);
    
	// 로그 레벨
	
	/**
     * @brief EzSpy에 출력할 메시지 레벨 설정
     * 
     * @param uLevel 메시지 레벨 (0 ~ 3)
	 *
	 * @details
	 * uLevel
	 *    LEVEL_NONE(0)    : 모든 메시지를 출력하지 않음
	 *    LEVEL_ERROR(1)   : 에러가 발생한 메시지만 출력
	 *    LEVEL_RUNSTOP(2) : 모션에서 Run / Stop 관련 메시지 출력
	 *    LEVEL_FUNCTION(3): 모든 메시지 출력
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlSetLogLevel(uint uLevel);
    
	/**
     * @brief EzSpy에 출력할 메시지 레벨 설정 확인
     * 
     * @param uLevel 메시지 레벨 저장 (0 ~ 3)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlGetLogLevel(ref uint upLevel);
	
	// MLIII
	
    /**
     * @brief Network 제품의 각 모듈 검색 시작 (MLIII 전용 함수)
     * 
     * @param lBoardNo 보드 번호
     * @param lNet 마스터 보드와 연결되는 네트워크 연결 번호
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlScanStart(int lBoardNo, long lNet);
    
	/**
     * @brief Network 제품 각 보드의 모든 모듈 Connect (MLIII 전용 함수)
     * 
     * @param lBoardNo 보드 번호
     * @param lNet 마스터 보드와 연결되는 네트워크 연결 번호
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlBoardConnect(int lBoardNo, long lNet);
    
	/**
     * @brief Network 제품 각 보드의 모든 모듈 Disconnect (MLIII 전용 함수)
     * 
     * @param lBoardNo 보드 번호
     * @param lNet 마스터 보드와 연결되는 네트워크 연결 번호
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlBoardDisconnect(int lBoardNo, long lNet);

	/**
     * @brief SIIIH 마스터 보드에 연결된 모듈에 대한 검색 시작 (SIIIH 마스터 보드 전용)
     * 
     * @param pScanResult 검색 결과를 저장할 SCAN_RESULT 구조체 (Default: NULL)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxlScanStartSIIIH(ref SCAN_RESULT? pScanResult);

    /**
     * @brief 보드에 장착된 Fan의 속도(RPM) 확인
     * 
     * @param lBoardNo 보드 번호
     * @param dpFanSpeed 팬 속도 저장 (단위: RPM)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlReadFanSpeed(int lBoardNo, ref double dpFanSpeed);

    /**
     * @brief EzSpy 사용자 로그 작성 (최대 길이: 200 Bytes)
     * 
     * @param szUserLog 작성할 사용자 로그
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlEzSpyUserLog(string szUserLog);
	
	// Background Log
	
    /**
     * @brief EzBlackBox 기능 활성화
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlEzBlackBoxEnable();

    /**
     * @brief EzBlackBox 기능 비활성화
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlEzBlackBoxDisable();

    /**
     * @brief BlackBox의 최대 Log Count 개수 설정 (Default: 10000, MAX: 100000)
     * 
     * @param nCount 설정 로그 개수
	 *
	 * @details 함수 사용 시 AXL Library를 사용하는 다른 프로그램들은 종료해야 함
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlEzBlackBoxChangeLogCount(int nCount);

    /**
     * @brief EzBlackBox의 State Machine Index 설정 (Index 0 ~ 31)
     * 
     * @param lIndex 설정 인덱스 번호
     * @param dwValue 설정 값
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlEzBlackBoxSetStateIndex(long lIndex, uint dwValue);

    /**
     * @brief EzBlackBox의 State Machine Index 설정 확인 (Index 0 ~ 31)
     * 
     * @param lIndex 설정 인덱스 번호
     * @param dwValue 설정 값 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlEzBlackBoxGetStateIndex(long lIndex, ref uint dwValue);

    /**
     * @brief EzBlackBox에 저장된 로그를 파일로 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlEzBlackBoxSaveLog();

    // SW License
}

