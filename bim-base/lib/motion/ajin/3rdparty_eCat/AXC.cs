/**
 * @file AXC.cs
 * 
 * @brief 아진엑스텍 카운터 라이브러리 헤더 파일
 *
 * @author 아진엑스텍 주식회사
 * 
 * @copyright 저작권 (c) 아진엑스텍 주식회사
 *
 * @website http://www.ajinextek.com
 *
 * @last_update 2023-12-15
 * 
 * @details 자세한 정보는 매뉴얼을 참고해 주세요.
 */


using System;
using System.Runtime.InteropServices;

public class CAXC
{
	// 보드 및 모듈 정보 
	
    /**
     * @brief CNT 모듈 존재 유무 확인
     * 
     * @param upStatus 존재 정보 (0: 모듈 없음, 1: 모듈 존재)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcInfoIsCNTModule(ref uint upStatus);
    
    /**
     * @brief CNT 모듈 넘버 확인
     * 
     * @param lBoardNo 보드 번호
     * @param lModulePos 모듈 위치
     * @param lpModuleNo 모듈 넘버 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcInfoGetModuleNo(int lBoardNo, int lModulePos, ref int lpModuleNo);
    
    /**
     * @brief CNT 모듈 개수 확인
     * 
     * @param lpModuleCount 모듈 개수 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcInfoGetModuleCount(ref int lpModuleCount);

    /**
     * @brief 지정 모듈의 카운터 입력 채널 개수 확인
     *
     * @param lModuleNo 가져올 모듈의 번호
     * @param lpCount 입력 채널 개수 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcInfoGetChannelCount(int lModuleNo, ref int lpCount);
    
    /**
     * @brief 시스템에 장착된 카운터의 전 채널 개수 확인
     *
     * @param lpChannelCount 전체 채널 개수 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcInfoGetTotalChannelCount(ref int lpChannelCount);    

    /**
     * @brief 지정 모듈 번호로 베이스 보드 번호, 모듈 위치, 모듈 ID 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lpBoardNo 보드 번호 저장
     * @param lpModulePos 모듈 위치 정보 저장
     * @param upModuleID 모듈 ID 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcInfoGetModule(int lModuleNo, ref int lpBoardNo, ref int lpModulePos, ref uint upModuleID);
    
    /**
     * @brief 지정 모듈 번호로 해당 모듈의 Sub ID, 모듈 Name, 모듈 설명 확인
     * 
     * @param lModuleNo 모듈 번호
     * @param upModuleSubID EtherCAT 모듈 구분을 위한 SubID
     * @param szModuleName 모듈명 (50 Bytes)
     * @param szModuleDescription 모듈 설명 (80 Bytes)
	 *
	 * @details 지원 제품: EtherCAT
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcInfoGetModuleEx(int lModuleNo, ref uint upModuleSubID, System.Text.StringBuilder szModuleName, System.Text.StringBuilder szModuleDescription);

    /**
     * @brief 해당 모듈 제어 가능 상태 반환
     * 
     * @param lModuleNo 모듈 번호
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcInfoGetModuleStatus(int lModuleNo);

	/**
     * @brief 지정한 CNT 모듈의 첫번째 채널 번호 확인
     * 
     * @param lModuleNo 모듈 번호
     * @param lpChannelNo 첫번째 채널 번호 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcInfoGetFirstChannelNoOfModuleNo(int lModuleNo, ref int lpChannelNo);
	
	/**
     * @brief 채널 번호로 모듈 번호 검색
     * 
     * @param lChannelNo 채널 번호
     * @param lpModuleNo 검색된 모듈 번호 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcInfoGetModuleNoOfChannelNo(int lChannelNo, ref int lpModuleNo);

    /**
     * @brief CNT 모듈의 엔코더 입력 방식 설정
     *
     * @param lChannelNo 채널 번호
     * @param dwMethod 설정할 엔코더 입력 방식
     * 
	 * @details
	 * dwMethod (카운터 모듈의 Encoder 입력 방식)
	 * SIO_RCNT2MLII의 경우
	 *    0x00: Sign and pulse, x1 multiplication
	 *    0x01: Phase-A and phase-B pulses, x1 multiplication
	 *    0x02: Phase-A and phase-B pulses, x2 multiplication
	 *    0x03: Phase-A and phase-B pulses, x4 multiplication
	 *    0x08: Sign and pulse, x2 multiplication
	 *    0x09: Increment and decrement pulses, x1 multiplication
	 *    0x0A: Increment and decrement pulses, x2 multiplication
	 *
	 * SIO-CN2CH/HPC4/SIO_RCNT2SIIIH/SIO_RCNT2SIIIH_R/SIO_RCNT2RTEX의 경우
	 *    0x00: Up/Down 방식, A phase : 펄스, B phase : 방향
	 *    0x01: Phase-A and phase-B pulses, x1 multiplication
	 *    0x02: Phase-A and phase-B pulses, x2 multiplication
	 *    0x03: Phase-A and phase-B pulses, x4 multiplication
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcSignalSetEncInputMethod(int lChannelNo, uint dwMethod);

    /**
     * @brief CNT 모듈의 엔코더 입력 방식 확인
     *
     * @param lChannelNo 채널 번호
     * @param dwpUpMethod 엔코더 입력 방식 저장
	 *
	 * @details
	 * dwpUpMethod
	 *    0x00: Sign and pulse, x1 multiplication
	 *    0x01: Phase-A and phase-B pulses, x1 multiplication
	 *    0x02: Phase-A and phase-B pulses, x2 multiplication
	 *    0x03: Phase-A and phase-B pulses, x4 multiplication
	 *    0x08: Sign and pulse, x2 multiplication
	 *    0x09: Increment and decrement pulses, x1 multiplication
	 *    0x0A: Increment and decrement pulses, x2 multiplication
	 * SIO-CN2CH/HPC4의 경우
	 *    0x00: Up/Down 방식, A phase : 펄스, B phase : 방향
	 *    0x01: Phase-A and phase-B pulses, x1 multiplication
	 *    0x02: Phase-A and phase-B pulses, x2 multiplication
	 *    0x03: Phase-A and phase-B pulses, x4 multiplication
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcSignalGetEncInputMethod(int lChannelNo, ref uint dwpUpMethod);

    /**
     * @brief 지정 CNT 채널의 트리거 기능 설정
     *
     * @param lChannelNo 트리거 설정 채널 번호
     * @param dwMode 트리거 기능 설정
     * 
	 * @details
	 * dwMode
	 *    0x00: Latch
	 *    0x01: State
	 *    0x02: Special State (SIO-CN2CH 전용)
	 *
	 *    SIO-CN2CH(Version: B0)의 경우
	 *       0x00: 카운터 절대 위치에 따른 트리거 펄스 출력 기능
	 *       0x01: 시간 주기 트리거(AxcTriggerSetFreq로 설정)
	 *    SIO-CN2CH(Version: A6)의 경우
	 *       0x00: 카운터 주기 위치에 따른 트리거 펄스 출력 기능
	 *       0x01: 시간 주기 트리거(AxcTriggerSetFreq로 설정)
	 *    SIO-HPC4의 경우
	 *       0x00: timer mode with counter & frequncy
	 *       0x01: timer mode
	 *       0x02: absolute mode[with fifo]
	 *       0x03: periodic mode[Default]
	 *    CNT_RECAT_SC_10의 경우
	 *       [0] CCGC_CNT_RANGE_TRIGGER: 지정한 트리거 위치에 설정한 허용 범위 안에 위치할 때 트리거를 출력하는 모드
	 *       [2] CCGC_CNT_DISTANCE_PERIODIC_TRIGGER: 지정한 트리거 위치에 설정한 허용 범위 안에 위치 등간격으로 트리거를 출력하는 모드
	 *       [3] CCGC_CNT_PATTERN_TRIGGER: 위치와 무관하게 지정한 개수만큼 설정한 주파수로 트리거를 출력하는 모드
	 *       [4] CCGC_CNT_POSITION_ON_OFF_TRIGGER: 지정한 트리거 위치에서 트리거 출력을 유지하는 모드
	 *           (CCGC_CNT_RANGE_TRIGGER와 같은 방식으로 설정하며 홀수번째 TargetPosition에서 출력이 시작되고 짝수번째 Position에서 출력이 꺼짐)
	 *       [5] CCGC_CNT_AREA_ON_OFF_TRIGGER: 지정한 Low Position부터 Upper Position까지 트리거 출력을 유지하는 모드
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetFunction(int lChannelNo, uint dwMode);

    /**
     * @brief 지정 CNT 채널의 트리거 기능 설정 확인
     *
     * @param lChannelNo 트리거 설정 채널 번호
     * @param dwpMode 트리거 기능 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerGetFunction(int lChannelNo, ref uint dwpMode);

    /**
     * @brief 지정 채널의 노치 기능 활성화/비활성화
     *
     * @param lChannelNo 채널 번호
     * @param dwUsage 노치 기능 사용 여부 지정 값 (0: Trigger Not Use, 1: Trigger Use)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetNotchEnable(int lChannelNo, uint dwUsage);

    /**
     * @brief 지정 채널의 노치 사용 여부 확인
     *
     * @param lChannelNo 노치 사용 여부를 확인할 채널 번호
     * @param dwpUsage 노치 사용 여부 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerGetNotchEnable(int lChannelNo, ref uint dwpUsage);
    
    /**
     * @brief 채널 번호의 캡처 기능 설정
     *
     * @param lChannelNo 설정할 채널 번호
     * @param dwCapturePol 설정할 캡처 극성 (0: Signal Off -> On, 1: Signal On -> Off)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcSignalSetCaptureFunction(int lChannelNo, uint dwCapturePol);

    /**
     * @brief 채널 번호의 캡처 기능 설정 확인
     *
     * @param lChannelNo 설정할 채널 번호
     * @param dwCapturePol 설정할 캡처 기능 저장 (0: Signal Off -> On, 1: Signal On -> Off)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcSignalGetCaptureFunction(int lChannelNo, ref uint dwpCapturePol);

    /**
     * @brief 지정 채널의 캡쳐 위치 값 확인 (External latch)
     *
     * @param lChannelNo 채널 번호
     * @param dbpCapturePos 캡처 위치 값 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcSignalGetCapturePos(int lChannelNo, ref double dbpCapturePos);

    /**
     * @brief 지정 채널의 현재 실제 위치 값 확인
     *
     * @param lChannelNo 채널 번호
     * @param dbpActPos 실제 위치 값 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcStatusGetActPos(int lChannelNo, ref double dbpActPos);

    /**
     * @brief 지정 채널의 현재 위치 값 설정
     *
     * @param lChannelNo 채널 번호
     * @param dbActPos 설정할 위치 값
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcStatusSetActPos(int lChannelNo, double dbActPos);

    /**
     * @brief 지정 채널의 notch position 값 설정
     *
     * @param lChannelNo 채널 번호
     * @param dbLowerPos 하한 값
     * @param dbUpperPos 상한 값
     * 
	 * @details 카운터 모듈의 트리거 위치는 2개까지만 설정 할 수 있음
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetNotchPos(int lChannelNo, double dbLowerPos, double dbUpperPos);

    /**
     * @brief 지정 채널의 notch position 값 반환
     *
     * @param lChannelNo 채널 번호
     * @param dbpLowerPos 하한 값 저장
     * @param dbpUpperPos 상한 값 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerGetNotchPos(int lChannelNo, ref double dbpLowerPos, ref double dbpUpperPos);

    /**
     * @brief 카운터 모듈의 트리거를 강제로 출력
     *
     * @param lChannelNo 채널 번호
     * @param dwOutVal 출력 값 (0x00: 트리거 출력 '0', 0x01: 트리거 출력 '1')
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetOutput(int lChannelNo, uint dwOutVal);

    /**
     * @brief 카운터 모듈의 상태 확인
     * 
     * @param lChannelNo 채널 번호
     * @param dwpChannelStatus 채널 상태 정보 저장
     * 
	 * @deatils
	 * dwpChannelStatus (채널 상태 정보)
	 *    Bit '0' : Carry  (카운터 현재치가 덧셈 펄스에 의해 카운터 상한치를 넘어 0로 바뀌었을 때 1스캔만 ON으로 함)
	 *    Bit '1' : Borrow (카운터 현재치가 뺄셈 펄스에 의해 0을 넘어 카운터 상한치로 바뀌었을 때 1스캔만 ON으로 함)
	 *    Bit '2' : Trigger output status
	 *    Bit '3' : Latch input status
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcStatusGetChannel(int lChannelNo, ref uint dwpChannelStatus);

    // SIO-CN2CH 전용 함수군

    /**
     * @brief 카운터 모듈의 위치 단위 설정
     * 
     * @param lChannelNo 채널 번호
     * @param dMoveUnitPerPulse 위치 단위
     *
	 * @details
	 * 실제 위치 이동량에 대한 펄스 개수를 설정 함
	 * 예) 1mm 이동에 1000이 필요 하다면 dMoveUnitPerPulse = 0.001로 입력하고 
	 *    이후 모든 함수의 위치와 관련된 값을 mm 단위로 설정
	 * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcMotSetMoveUnitPerPulse(int lChannelNo, double dMoveUnitPerPulse);

    /**
     * @brief 카운터 모듈의 위치 단위 확인
     * 
     * @param lChannelNo 채널 번호
     * @param dpMoveUnitPerPuls 위치 단위 저장
     * .
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcMotGetMoveUnitPerPulse(int lChannelNo, ref double dpMoveUnitPerPuls);

    /**
     * @brief 카운터 모듈의 엔코더 입력 카운터 반전 기능 설정
     *
     * @param lChannelNo 채널 번호
     * @param dwReverse 설정 값 (0x00: 반전하지 않음, 0x01: 반전)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcSignalSetEncReverse(int lChannelNo, uint dwReverse);

    /**
     * @brief 카운터 모듈의 엔코더 입력 카운터 반전 기능 설정 확인
     *
     * @param lChannelNo 채널 번호
     * @param dwpReverse 설정 값 저장
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcSignalGetEncReverse(int lChannelNo, ref uint dwpReverse);

    /**
     * @brief 카운터 모듈의 Encoder 입력 신호 설정
     *
     * @param lChannelNo 채널 번호
     * @param dwSource 엔코더 입력 신호 값
	 *
	 * @details
	 * dwSource (엔코더 입력 신호 값)
	 *    0x00 : 2(A/B)-Phase 신호
	 *    0x01 : Z-Phase 신호 (방향성 없음)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcSignalSetEncSource(int lChannelNo, uint dwSource);

    /**
     * @brief 카운터 모듈의 Encoder 입력 신호 설정 확인
     *
     * @param lChannelNo 채널 번호
     * @param dwpSource 엔코더 입력 신호 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcSignalGetEncSource(int lChannelNo, ref uint dwpSource);

    /**
     * @brief 카운터 모듈의 트리거 출력 범위 중 하한 값 설정
     *
     * @param lChannelNo 채널 번호
     * @param dpLowerPosition 하한 위치 값
     * 
	 * @details
	 * 위치 주기 트리거 제품: 위치 주기로 트리거 출력을 발생시킬 범위 중 하한 값 설정
	 * 절대 위치 트리거 제품: Ram 시작 번지의 트리거 정보의 적용 기준 위치 설정
	 * 단위: AxcMotSetMoveUnitPerPulse로 설정한 단위
	 * 하한값은 상한값보다 작은값으로 설정
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetBlockLowerPos(int lChannelNo, double dLowerPosition);

    /**
     * @brief 카운터 모듈의 트리거 출력 범위 중 하한 값 확인
     *
     * @param lChannelNo 채널 번호
     * @param dpLowerPosition 하한 값 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerGetBlockLowerPos(int lChannelNo, ref double dpLowerPosition);

    /**
     * @brief 카운터 모듈의 트리거 출력 범위 중 상한 값 설정
     *
     * @param lChannelNo 채널 번호
     * @param dUpperPosition 상한 위치 값
     * 
	 * @details
	 * 위치 주기 트리거 제품: 위치 주기로 트리거 출력을 발생시킬 범위 중 상한 값 설정
	 * 절대 위치 트리거 제품: 트리거 정보가 설정된 Ram 의 마지막 번지의 트리거 정보가 적용되는 위치로 사용
	 * 단위: AxcMotSetMoveUnitPerPulse로 설정한 단위
	 * 상한값은 하한값보다 큰값으로 설정
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetBlockUpperPos(int lChannelNo, double dUpperPosition);
    
	/**
     * @brief 카운터 모듈의 트리거 출력 범위 중 상한 값 확인
     *
     * @param lChannelNo 채널 번호
     * @param dpUpperrPosition 상한 위치 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerGetBlockUpperPos(int lChannelNo, ref double dpUpperrPosition);

    /**
     * @brief 카운터 모듈의 위치 주기 모드 트리거에 사용되는 위치 주기 설정
     *
     * @param lChannelNo 채널 번호
     * @param dPeriod 위치 주기
	 *
	 * @details 단위: AxcMotSetMoveUnitPerPulse로 설정한 단위
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetPosPeriod(int lChannelNo, double dPeriod);

    /**
     * @brief 카운터 모듈의 위치 주기 모드 트리거에 사용되는 위치 주기 확인
     *
     * @param lChannelNo 채널 번호
     * @param dpPeriod 위치 주기 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerGetPosPeriod(int lChannelNo, ref double dpPeriod);

    /**
     * @brief 카운터 모듈의 위치 주기 모드 트리거 사용 시 위치 증감에 대한 유효 기능 설정
     *
     * @param lChannelNo 채널 번호
     * @param dwDirection 설정 값
	 *
	 * @details
	 * dwDirection (위치 증감에 대한 유효 기능 값)
	 *    0x00 : 카운터의 증/감에 대하여 트리거 주기 마다 출력
	 *    0x01 : 카운터가 증가 할때만 트리거 주기 마다 출력
	 *    0x02 : 카운터가 감소 할때만 트리거 주기 마다 출력
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetDirectionCheck(int lChannelNo, uint dwDirection);
    
	/**
     * @brief 카운터 모듈의 위치 주기 모드 트리거 사용 시 위치 증감에 대한 유효기능 설정 확인
     *
     * @param lChannelNo 채널 번호
     * @param dwpDirection 설정 값 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerGetDirectionCheck(int lChannelNo, ref uint dwpDirection);

    /**
     * @brief lChannelNo 카운터 모듈의 위치 주기 모드 트리거 기능의 범위, 위치 주기를 한번에 설정
     *
     * @param lChannelNo 채널 번호
     * @param dLower 하한 값
     * @param dUpper 상한 값
     * @param dABSod 주기 트리거 기능을 위한 거리 주기 값
	 *
	 * @details 단위: AxcMotSetMoveUnitPerPulse로 설정한 단위
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetBlock(int lChannelNo, double dLower, double dUpper, double dABSod);

    /**
     * @brief lChannelNo 카운터 모듈의 위치 주기 모드 트리거 기능의 범위, 위치 주기를 설정을 한번에 확인
     *
     * @param lChannelNo 채널 번호
     * @param dpLower 하한 값 저장
     * @param dpUpper 상한 값 저장
     * @param dpABSod 주기 트리거 기능을 위한 거리 주기 값 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerGetBlock(int lChannelNo, ref double dpLower, ref double dpUpper, ref double dpABSod);

    /**
     * @brief lChannelNo 트리거 출력 펄스 폭 설정
     *
     * @param lChannelNo 채널 번호
     * @param dTrigTime 트리거 시간 값 (단위: uSec)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetTime(int lChannelNo, double dTrigTime);

    /**
     * @brief 트리거 출력 펄스 폭 설정 확인
     *
     * @param lChannelNo 채널 번호
     * @param dpTrigTime 트리거 시간 값 저장 (단위: uSec)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerGetTime(int lChannelNo, ref double dpTrigTime);

    /**
     * @brief 출력 펄스의 출력 레벨 설정
     *
     * @param lChannelNo 채널 번호
     * @param dwLevel 트리거 레벨 값 (0x00: Low 레벨 출력, 0x01: High 레벨 출력)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetLevel(int lChannelNo, uint dwLevel);
    
	/**
     * @brief 트리거 출력 펄스의 출력 레벨 설정 확인
     *
     * @param lChannelNo 채널 번호
     * @param dwpLevel 트리거 레벨 값 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerGetLevel(int lChannelNo, ref uint dwpLevel);

    /**
     * @brief 주파수 트리거 출력 기능에 필요한 주파수 설정
     * 
     * @param lChannelNo 채널 번호
     * @param dwFreqency 설정 주파수 값 (단위: Hz, 범위: 1Hz ~ 500 kHz)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetFreq(int lChannelNo, uint dwFreqency);
    
	/**
     * @brief 주파수 트리거 출력 기능에 필요한 주파수 설정 확인
     * 
     * @param lChannelNo 채널 번호
     * @param dwFreqency 설정 주파수 값 저장 (단위: Hz, 범위: 1Hz ~ 500 kHz)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerGetFreq(int lChannelNo, ref uint dwpFreqency);

	// CNT_RECAT_SC_10 전용 함수군

	/**
     * @brief 1d Time trigger mode with count 모드에서 트리거 출력 개수 설정값 확인
     * 
     * @param lChannelNo 채널 번호
     * @param dwpTriggerOutCount 트리거 출력 개수 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerGetTriggerOutCount(int lChannelNo, ref uint dwpTriggerOutCount);

	/**
     * @brief 1d Time trigger mode with count 모드에서 트리거 출력 개수 설정
     * 
     * @param lChannelNo 채널 번호
     * @param dwpTriggerOutCount 트리거 출력 개수
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetTriggerOutCount(int lChannelNo, uint dwTriggerOutCount);

	/**
     * @brief 카운터 모듈의 각 채널에 설정된 정보로(트리거 출력 Port, 트리거 펄스 폭) 허용 범위 내에서 지정한 위치 등 간격으로 트리거 발생
     * 
     * @param lChannelNo 채널 번호
     * @param dDistance 트리거 마다의 위치 간격
	 *
	 * @details
	 * 이 함수는 트리거가 Disable되어 있으면 자동으로 Enable 시켜 패턴을 가진 트리거 발생
	 * 이 함수는 Trigger Mode가 CCGC_CNT_DISTANCE_PERIODIC_TRIGGER 모드가 아닐 경우 자동으로 트리거 모드를 CCGC_CNT_DISTANCE_PERIODIC_TRIGGER로 변경
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxcTriggerDistancePatternShot(int lChannelNo,double dDistance);

	/**
     * @brief 카운터 모듈의 각 채널에 설정된 트리거 설정 정보(트리거 마다 간격) 확인
     * 
     * @param lChannelNo 채널 번호
     * @param dpDistance 트리거 마다의 위치 간격
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetDistancePatternShotData(int lChannelNo,ref double dpDistance);

	/**
     * @brief 카운터 모듈의 지정한 채널을 이용해 발생된 트리거 개수 확인
     *
     * @param lChannelNo 채널 번호
     * @param lpTriggerCount 현재까지 출력된 트리거 출력 개수 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxcTriggerReadTriggerCount(int lChannelNo, ref int lpTriggerCount);

	/**
     * @brief 카운터 모듈의 각 채널 별로 출력된 누적 트리거 개수 초기화
     *
     * @param lChannelNo 채널 번호
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxcTriggerSetTriggerCountClear(int lChannelNo);

    /**
     * @brief 카운터 모듈의 각 채널에 설정된 정보로(트리거 출력 Port, 트리거 펄스 폭) 트리거 1개 발생
     *
     * @param lChannelNo 채널 번호
	 *
	 * @details
	 * 이 함수는 트리거가 Disable되어 있으면 자동으로 Enable 시켜 트리거 발생
	 * 이 함수는 하나의 트리거만 발생시키기 위해 Trigger Mode가 HPC4_PATTERN_TRIGGER 모드일 경우 자동으로 HPC4_RANGE_TRIGGER로 변경 함
	 * 이 함수는 하나의 트리거만 발생시키기 위해 Trigger Mode가 CCGC_CNT_PATTERN_TRIGGER 모드일 경우 자동으로 CCGC_CNT_RANGE_TRIGGER 변경 함
	 * CN2CH ABS/PERIOD 제품군도 지원되나 HW에서 처리한 것이 아닌 상위 라이브러리에서 해당 기능을 구현한 것으로 사용자의 PC환경에 따라 성능이 변경될 수 있음. TEST 용도로 사용 요망
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerOneShot(int lChannelNo);

    /**
     * @brief 지정 채널에 대한 범용 출력 값 설정
     *
     * @param lChannelNo 채널 번호
     * @param dwOutput 출력 신호 (0x00 ~ 0x0F, 각 채널 당 4개의 범용 출력)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcSignalWriteOutput(int lChannelNo, uint dwOutput);

    /**
     * @brief 지정 채널에 대한 범용 출력 값 확인
     *
     * @param lChannelNo 채널 번호
     * @param dwOutput 출력 신호 저장 (0x00 ~ 0x0F, 각 채널 당 4개의 범용 출력)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcSignalReadOutput(int lChannelNo, ref uint dwpOutput);

    /**
     * @brief 지정 채널에 대한 범용 출력 값을 비트 별로 설정
     *
     * @param lChannelNo 채널 번호
     * @param lBitNo 비트 번호 (범위: 0 ~ 3, 각 채널 당 4개의 범용 출력)
     * @param uOnOff 출력 신호 값
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcSignalWriteOutputBit(int lChannelNo, int lBitNo, uint uOnOff);
    
	/**
     * @brief 지정 채널에 대한 범용 출력 값을 비트 별로 설정 확인
     *
     * @param lChannelNo 채널 번호
     * @param lBitNo 비트 번호 (범위: 0 ~ 3, 각 채널 당 4개의 범용 출력)
     * @param uOnOff 출력 신호 값 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcSignalReadOutputBit(int lChannelNo, int lBitNo, ref uint upOnOff);

	/**
     * @brief 지정 채널에 대한 범용 입력 값 확인
     * 
     * @param lChannelNo 채널 번호
     * @param dwpInput 입력 값 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcSignalReadInput(int lChannelNo, ref uint dwpInput);

    /**
     * @brief 지정 채널에 대한 범용 입력 값을 비트 별로 확인
     * 
     * @param lChannelNo 채널 번호
     * @param lBitNo 비트 번호 (범위: 0 ~ 3, 각 채널 당 4개의 범용 입력)
     * @param upOnOff 입력 비트의 현재 On 또는 Off 값
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcSignalReadInputBit(int lChannelNo, int lBitNo, ref uint upOnOff);

    /**
     * @brief 지정한 채널의 트리거 출력 활성화
     * 
     * @param lChannelNo 채널 번호
     * @param dwUsage 활성화 여부 지정 값 (0: 비활성화, 1: 활성화)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetEnable(int lChannelNo, uint dwUsage);

    /**
     * @brief 지정한 채널의 트리거 출력 활성화 설정 확인
     * 
     * @param lChannelNo 채널 번호
     * @param dwUsage 활성화 여부 지정 값 저장 (0: 비활성화, 1: 활성화)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerGetEnable(int lChannelNo, ref uint dwpUsage);

    /**
     * @brief 카운터 모듈의 절대위치 트리거 기능을 위해 설정된 RAM 내용 확인
     * 
     * @param lChannelNo 채널 번호
     * @param dwAddr RAM 데이터 값이 저장된 주소 (범위: 0x0000 ~ 0x1FFFF)
     * @param dwpData 읽은 데이터 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerReadAbsRamData(int lChannelNo, uint dwAddr, ref uint dwpData);

    /**
     * @brief 카운터 모듈의 절대위치 트리거 기능을 위해 필요한 RAM 내용 설정
     * 
     * @param lChannelNo 채널 번호
     * @param dwAddr RAM 데이터 값이 저장된 주소 (범위: 0x0000 ~ 0x1FFFF)
     * @param dwpData 설정 데이터
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerWriteAbsRamData(int lChannelNo, uint dwAddr, uint dwData);

    /**
     * @brief 지정 CNT 채널의 절대 위치 트리거 기능을 위한 위치 정보 설정
     *
     * @param lChannelNo 채널 번호
     * @param dwTrigNum 트리거 번호 (범위: ~ 0x20000, RTEX CNT2의 경우: 0x200)
     * @param dwTrigPos 설정 트리거 위치 배열
     * @param dwDirection 트리거 방향
     * 
	 * @details
	 * dwDirection (트리거 방향)
	 *    0x0 : 하한 트리거 위치에 대한 트리거 정보 부터 입력. 위치가 증가하는 방향으로 트리거 출력 시 사용
	 *    0x1 : 상한 카운터에 대한 트리거 정보 부터 입력. 위치가 감소하는 방향으로 트리거 출력 시 사용
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetAbs(int lChannelNo, uint dwTrigNum, uint[] dwTrigPos, uint dwDirection);

    /**
     * @brief 지정 CNT 채널의 절대 위치 트리거 기능을 위한 위치 정보 설정
     *
     * @param lChannelNo 채널 번호
     * @param dwTrigNum 트리거 번호 (범위: ~ 0x20000, RTEX CNT2의 경우: 0x200)
     * @param dTrigPos 설정 트리거 위치 배열 (트리거 Pos를 double 형으로 사용)
     * @param dwDirection 트리거 방향
     * 
	 * @details
	 * dwDirection (트리거 방향)
	 *    0x0 : 하한 트리거 위치에 대한 트리거 정보 부터 입력. 위치가 증가하는 방향으로 트리거 출력 시 사용
	 *    0x1 : 상한 카운터에 대한 트리거 정보 부터 입력. 위치가 감소하는 방향으로 트리거 출력 시 사용
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetAbsDouble(int lChannelNo, uint dwTrigNum, double[] dTrigPos, uint dwDirection);

	/**
     * @brief 카운터 모듈에 할당 할 엔코더 입력 신호 설정
     * 
     * @param lChannelNo 채널 번호
     * @param dwEncoderInput 인코더 신호 값 (범위: 0~3, 카운터 모듈에 입력되는 4개의 엔코더 신호중 하나)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetEncoderInput(int lChannelNo, uint dwEncoderInput);
	
	/**
     * @brief 카운터 모듈에 할당 할 엔코더 입력 신호 확인
     * 
     * @param lChannelNo 채널 번호
     * @param dwEncoderInput 인코더 신호 값 저장 (범위: 0~3, 카운터 모듈에 입력되는 4개의 엔코더 신호중 하나)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerGetEncoderInput(int lChannelNo, ref uint dwpEncoderInput);

    // HPC4_30_Version
    
	/**
     * @brief 카운터 모듈의 각 테이블에 할당된 트리거 출력 레벨 설정
     * 
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치 값
     * @param uLevel 트리거 레벨 값
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableSetTriggerLevel(int lModuleNo, int lTablePos, uint uLevel);
    
	/**
     * @brief 카운터 모듈의 각 테이블에 할당된 트리거 출력 레벨 설정 확인
     * 
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치 값
     * @param uLevel 트리거 레벨 값 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableGetTriggerLevel(int lModuleNo, int lTablePos, ref uint upLevel);

    /**
     * @brief 카운터 모듈의 각 테이블에 할당된 트리거 출력 펄스 폭 설정
     * 
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치 값
     * @param dTriggerTimeUSec 트리거 출력 펄스 값(단위: us, Default: 500000us)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableSetTriggerTime(int lModuleNo, int lTablePos, double dTriggerTimeUSec);
    
	/**
     * @brief 카운터 모듈의 각 테이블에 할당된 트리거 출력 펄스 폭 설정 확인
     * 
     * @param lModuleNo Motion 모듈 번호
     * @param lTablePos 테이블 위치 값
     * @param dTriggerTimeUSec 트리거 출력 펄스 값 저장(단위: us, Default: 500000us)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableGetTriggerTime(int lModuleNo, int lTablePos, ref double dpTriggerTimeUSec);

    /**
     * @brief 카운터 모듈의 각 테이블에 할당 할 2개의 엔코더 입력 신호 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치 값
     * @param uEncoderInput1 인코더 입력값 1
     * @param uEncoderInput2 인코더 입력값 2
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableSetEncoderInput(int lModuleNo, int lTablePos, uint uEncoderInput1, uint uEncoderInput2);
    
	/**
     * @brief 카운터 모듈의 각 테이블에 할당 할 2개의 엔코더 입력 신호 설정 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치 값
     * @param uEncoderInput1 인코더 입력값 1 저장
     * @param uEncoderInput2 인코더 입력값 2 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableGetEncoderInput(int lModuleNo, int lTablePos, ref uint upEncoderInput1, ref uint upEncoderInput2);

	/**
     * @brief 지정 채널에 할당된 H/W 트리거 데이타 FIFO의 상태 확인
     *
     * @param lChannelNo 채널 번호
     * @param lpCount 1D 트리거 위치 데이타 중 첫번째(X) 위치를 저정하고 있는 H/W FIFO에 입력된 데이타 개수
     * @param upStatus 1D 트리거 위치 데이타 중 첫번째(X) 위치를 저정하고 있는 H/W FIFO의 상태
     * 
	 * @details
	 * upStatus (FIFO 상태)
	 *   Bit 0: Data Empty
	 *   Bit 1: Data Full
	 *   Bit 2: Data Valid
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxcTriggerReadFifoStatus(int lChannelNo, ref int lpCount, ref uint upStatus); 

	/**
     * @brief  지정한 채널에 할당된 H/W 트리거 데이타 FIFO의 현재 위치 데이타값 확인
     *
     * @param lChannelNo FIFO 채널 번호
     * @param dpTopData 1D H/W 트리거 데이타 FIFO의 현재 데이타 중 첫번째(X) 위치 데이타 확인
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxcTriggerReadFifoData(int lChannelNo, ref double dpTopData);

	/**
     * @brief 카운터 모듈의 각 채널에 설정된 정보로(트리거 출력 Port, 트리거 펄스 폭) 지정한 개수만큼 설정한 주파수로 트리거 발생
     *
     * @param lChannelNo 채널 번호
     * @param lTriggerCount 지정한 주파수를 유지하며 발생시킬 트리거 출력 개수
     * @param uTriggerFrequency 트리거 발생 주파수
	 *
	 * @details
	 * 이 함수는 트리거가 Disable되어 있으면 자동으로 Enable시켜 패턴을 가진 트리거 발생
	 * 이 함수는 Trigger Mode가 CCGC_CNT_PATTERN_TRIGGER 모드가 아닐 경우 자동으로 트리거 모드를 CCGC_CNT_PATTERN_TRIGGER 변경
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxcTriggerPatternShot(int lChannelNo, int lTriggerCount, uint uTriggerFrequency);

	/**
     * @brief 카운터 모듈의 각 채널에 설정된 패턴 트리거 설정 정보(주파수, 카운터) 확인
     *
     * @param lChannelNo 채널 번호
     * @param lpTriggerCount 트리거 출력 개수
     * @param upTriggerFrequency 트리거 발생 주파수 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetPatternShotData(int lChannelNo, ref int lpTriggerCount, ref uint upTriggerFrequency);

    /**
     * @brief 카운터 모듈의 각 테이블에 할당 할 트리거 출력 포트 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param uTriggerOutport 설정할 트리거 출력 포트 값 (범위: 0x0~0xF)
	 *
	 * @details
	 * uTriggerOutport (트리거 출력 포트)
	 *    Bit 0: 트리거 출력 0
	 *    Bit 1: 트리거 출력 1
	 *    Bit 2: 트리거 출력 2
	 *    Bit 3: 트리거 출력 3
	 *    ex1) 0x3(3)  : 출력 0, 1에 트리거 신호 출력
	 *    ex2) 0xF(255): 출력 0, 1, 2, 3에 트리거 신호 출력
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableSetTriggerOutport(int lModuleNo, int lTablePos, uint uTriggerOutport);
    
	/**
     * @brief 카운터 모듈의 각 테이블에 할당 할 트리거 출력 포트 설정 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param uTriggerOutport 설정할 트리거 출력 포트 값 확인 (범위: 0x0~0xF)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableGetTriggerOutport(int lModuleNo, int lTablePos, ref uint upTriggerOutport);

    /**
     * @brief 카운터 모듈의 각 테이블에 설정된 트리거 위치에 대한 허용 오차 범위 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param dErrorRange 오차 범위 값
	 *
	 * @details dErrorRange: 지정 축의 Unit 단위로 트리거 위치에 대한 허용 오차 범위 설정
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableSetErrorRange(int lModuleNo, int lTablePos, double dErrorRange);
	
    /**
     * @brief 카운터 모듈의 각 테이블에 설정된 트리거 위치에 대한 허용 오차 범위 설정 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param dErrorRange 오차 범위 값 확인
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableGetErrorRange(int lModuleNo, int lTablePos, ref double dpErrorRange);

    /**
     * @brief 카운터 모듈의 각 테이블에 설정된 정보로(트리거 출력 Port, 트리거 펄스 폭) 트리거 1개 발생
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
	 *
	 * @details
	 * 이 함수는 트리거가 Disable 되어 있으면 자동으로 Enable 시켜 트리거 발생
	 * 이 함수는 하나의 트리거만 발생시키기 위해 Trigger Mode가 HPC4_PATTERN_TRIGGER 모드일 경우 자동으로 HPC4_RANGE_TRIGGER로 변경 함
	 * 이 함수는 하나의 트리거만 발생시키기 위해 Trigger Mode가 CCGC_CNT_PATTERN_TRIGGER 모드일 경우 자동으로 CCGC_CNT_RANGE_TRIGGER 변경 함
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableTriggerOneShot(int lModuleNo, int lTablePos);

    /**
     * @brief 카운터 모듈의 각 테이블에 설정된 정보로(트리거 출력 Port, 트리거 펄스 폭) 지정한 개수만큼 설정한 주파수로 트리거 발생
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param lTriggerCount 트리거 출력 개수
     * @param uTriggerFrequency 트리거 발생 시킬 주파수
     * 
	 * @details
	 * 이 함수는 트리거가 Disable 되어 있으면 자동으로 Enable 시켜 트리거 발생
	 * 이 함수는 하나의 트리거만 발생시키기 위해 Trigger Mode가 HPC4_PATTERN_TRIGGER 모드일 경우 자동으로 HPC4_RANGE_TRIGGER로 변경 함
	 * 이 함수는 하나의 트리거만 발생시키기 위해 Trigger Mode가 CCGC_CNT_PATTERN_TRIGGER 모드일 경우 자동으로 CCGC_CNT_RANGE_TRIGGER 변경 함
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableTriggerPatternShot(int lModuleNo, int lTablePos, int lTriggerCount, uint uTriggerFrequency);
    
	/**
     * @brief 카운터 모듈의 각 테이블에 설정된 패턴 트리거 설정 정보(주파수, 카운터) 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param lpTriggerCount 트리거 카운트 저장
     * @param upTriggerFrequency 트리거 주파수 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableGetPatternShotData(int lModuleNo, int lTablePos, ref int lpTriggerCount, ref uint upTriggerFrequency);

    /**
     * @brief 카운터 모듈의 각 테이블에 트리거 출력 방식 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param uTrigMode 트리거 모드
     * 
	 * @details
	 * uTrigMode (트리거 출력 방식)
	 *    SIO-HPC4 의 경우
	 *       [0] HPC4_RANGE_TRIGGER  : 지정한 트리거 위치에 설정한 허용 범위안에 위치할 때 트리거 출력
	 *       [1] HPC4_VECTOR_TRIGGER : 지한 트리거 위치에 설정한 허용 범위와 벡터 방향이 일치할 때 트리거 출력
	 *       [3] HPC4_PATTERN_TRIGGER: 위치와 무관하게 지정한 개수만큼 설정한 주파수로 트리거 출력
     *    CNT_RECAT_SC_10의 경우
	 *       [0] CCGC_CNT_RANGE_TRIGGER: 지정한 트리거 위치에 설정한 허용 범위 안에 위치할 때 트리거를 출력하는 모드
	 *       [2] CCGC_CNT_DISTANCE_PERIODIC_TRIGGER: 지정한 트리거 위치에 설정한 허용 범위 안에 위치 등간격으로 트리거를 출력하는 모드
	 *       [3] CCGC_CNT_PATTERN_TRIGGER: 위치와 무관하게 지정한 개수만큼 설정한 주파수로 트리거를 출력하는 모드
	 *       [4] CCGC_CNT_POSITION_ON_OFF_TRIGGER: 지정한 트리거 위치에서 트리거 출력을 유지하는 모드
	 *           (CCGC_CNT_RANGE_TRIGGER와 같은 방식으로 설정하며 홀수번째 TargetPosition에서 출력이 시작되고 짝수번째 Position에서 출력이 꺼짐)
	 *       [5] CCGC_CNT_AREA_ON_OFF_TRIGGER: 지정한 Low Position부터 Upper Position까지 트리거 출력을 유지하는 모드
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */	
    [DllImport("AXL.dll")] public static extern uint AxcTableSetTriggerMode(int lModuleNo, int lTablePos, uint uTrigMode);
    
	/**
     * @brief 카운터 모듈의 각 테이블에 트리거 출력 방식 설정 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param uTrigMode 트리거 모드 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */	
    [DllImport("AXL.dll")] public static extern uint AxcTableGetTriggerMode(int lModuleNo, int lTablePos, ref uint upTrigMode);
    
	/**
     * @brief 카운터 모듈의 각 테이블 별로 출력된 누적 트리거 개수 초기화
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableSetTriggerCountClear(int lModuleNo, int lTablePos);

    /**
     * @brief 카운터 모듈의 각 채널에 트리거 신호를 출력하기 위해 설정한 트리거 설정 정보 확인
     * 
     * @param lChannelNo 채널 번호
     * @param dwpTrigNum 트리거 개수 저장
     * @param dwpTrigPos 트리거 위치값 저장
	 *
	 * @note
	 * 주의 사항: 각 채널에 등록된 최대 트리거 데이타 개수를 모를 때는 트리거 데이타 개수를 미리 파악 후 사용
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */    
	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetAbs(int lChannelNo, ref uint dwpTrigNum, uint[] dwpTrigPos);

    /**
     * @brief 카운터 모듈의 각 테이블에 2D 절대위치에서 트리거 신호를 출력을 위한 정보 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param lTriggerDataCount 설정 할 트리거 정보의 전체 개수
     * @param dpTriggerData 2D 절대위치 트리거 정보 (배열 개수: lTriggerDataCount * 2가 되어야 함)
     * @param lpTriggerCount 입력한 2D 절대 트리거 위치에서 트리거 조건 만족 시 발생시킬 트리거 개수를 배열로 설정 (배열 개수: lTriggerDataCount)
     * @param dpTriggerInterval TriggerCount 만큼 연속해서 트리거를 발생시킬때 유지 할 간격을 주파수 단위로 설정 (배열 개수: lTriggerDataCount)
	 *
	 * @details
	 * 각 전달 인자의 배열 개수를 주의하여 사용해야 함. 내부에서 사용되는 인자 보다 적은 배열을 지정하면 메모리 참조 오류가 발생 될 수 있음.
	 * Trigger Mode는 HPC4_RANGE_TRIGGER로 자동 변경됨
	 * 함수 내부에서 Trigger를 Disable한 후 모든 설정을 진행하며 완료 후 다시 Enable 시킴
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableSetTriggerData(int lModuleNo, int lTablePos, int lTriggerDataCount, double[] dpTriggerData, int[] lpTriggerCount, double[] dpTriggerInterval);
    
	/**
     * @brief 카운터 모듈의 각 테이블에 트리거 신호를 출력하기 위해 설정한 트리거 설정 정보 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param lpTriggerDataCount 트리거 정보의 전체 개수 저장
     * @param dpTriggerData 2D 절대위치 트리거 정보 저장
     * @param lpTriggerCount 입력한 2D 절대 트리거 위치에서 트리거 조건 만족 시 발생시킬 트리거 개수를 배열 저장
     * @param dpTriggerInterval TriggerCount 만큼 연속해서 트리거를 발생시킬때 유지 할 간격을 주파수 단위로 배열 저장
     *
	 * @note 각 테이블에 등록된 최대 트리거 데이타 개수를 모를 시 트리거 데이타 개수를 미리 파악 후 사용
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableGetTriggerData(int lModuleNo, int lTablePos, ref int lpTriggerDataCount, double[] dpTriggerData, int[] lpTriggerCount, double[] dpTriggerInterval);

	/**
     * @brief [SIO-HPC4] 카운터 모듈의 각 테이블에 2D 절대위치에서 트리거 신호를 출력하기 위해 필요 정보를 AxcTableSetTriggerData함수와 다른 방식으로 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param lTriggerDataCount 설정 할 트리거 정보의 전체 개수
     * @param uOption dpTriggerData 배열의 데이타 입력 방식을 지정
     * @param dpTriggerData 2D 절대위치 트리거 정보
     *
	 * @details
	 * uOption (데이타 입력 방식)
	 *    0: dpTriggerData 배열에 X Pos[0], Y Pos[0], X Pos[1], Y Pos[1] 순서로 입력
	 *    1: dpTriggerData 배열에 X Pos[0], Y Pos[0], Count, Inteval, X Pos[1], Y Pos[1], Count, Inteval 순서로 입력
	 * 각 전달 인자의 배열 개수를 주의하여 사용해야 함. 내부에서 사용되는 인자 보다 적은 배열을 지정하면 메모리 참조 오류가 발생 될 수 있음.
	 * Trigger Mode는 HPC4_RANGE_TRIGGER로 자동 변경됨
	 * 함수 내부에서 Trigger를 Disable한 후 모든 설정을 진행하며 완료 후 다시 Enable 시킴
     *
	 * CNT_RECAT_SC_10 의 경우
	 * uOption: Reserved
	 * dpTriggerData
	 *    CCGC_CNT_RANGE_TRIGGER 인 경우
	 *       dpTriggerData 배열에 X Pos[0], Y Pos[0], X Pos[1], Y Pos[1] 순서로 입력
	 *    CCGC_CNT_VECTOR_TRIGGER 인 경우
	 *       dpTriggerData 배열에 X Pos[0], Y Pos[0], X UnitVector[0], Y UnitVector[0] X Pos[1], Y Pos[1], X UnitVector[1], Y UnitVector[1] 순서로 입력
	 *    CCGC_CNT_POSITION_ON_OFF_TRIGGER 인 경우
	 *       dpTriggerData 배열에 X Pos[0], Y Pos[0], X Pos[1], Y Pos[1] 순서로 입력
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableSetTriggerDataEx(int lModuleNo, int lTablePos, int lTriggerDataCount, uint uOption, double[] dpTriggerData);
	
	/**
     * @brief [SIO-HPC4] 카운터 모듈의 각 테이블에 트리거 신호를 출력하기 위해 설정한 트리거 설정 정보 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param lTriggerDataCount 설정 할 트리거 정보의 전체 개수 저장
     * @param uOption dpTriggerData 배열의 데이타 입력 방식 저장
     * @param dpTriggerData 2D 절대위치 트리거 정보 저장
     *
     * @note
	 * 주의 사항: 각 채널에 등록된 최대 트리거 데이타 개수를 모를 때는 트리거 데이타 개수를 미리 파악 후 사용
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableGetTriggerDataEx(int lModuleNo, int lTablePos, ref int lpTriggerDataCount, ref uint upOption, double[] dpTriggerData);

    /**
     * @brief 카운터 모듈의 지정한 테이블에 설정된 모든 트리거 데이터와 H/W FIFO의 데이터 모두 삭제
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableSetTriggerDataClear(int lModuleNo, int lTablePos);

    /**
     * @brief 카운터 모듈의 지정한 테이블의 트리거 출력 기능 동작
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param uEnable 트리거 출력 기능 활성화 여부
	 *
	 * @details
	 * 트리거 출력 중 DISABLE하면 출력 바로 멈춤
	 * AxcTableTriggerOneShot, AxcTableGetPatternShotData,AxcTableSetTriggerData, AxcTableGetTriggerDataEx 함수 호출 시 자동 ENABLE
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableSetEnable(int lModuleNo, int lTablePos, uint uEnable);
    
	/**
     * @brief 카운터 모듈의 지정한 테이블의 트리거 출력 기능 동작 여부 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param uEnable 트리거 출력 기능 활성화 여부 저장
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableGetEnable(int lModuleNo, int lTablePos, ref uint upEnable);

    /**
     * @brief 카운터 모듈의 지정한 테이블을 이용해 발생된 트리거 개수 확인
     * 
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
	 * @param lpTriggerCount 트리거 개수 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableReadTriggerCount(int lModuleNo, int lTablePos, ref int lpTriggerCount);

    /**
     * @brief 카운터 모듈의 지정한 테이블에 할당된 H/W 트리거 데이타 FIFO의 상태 확인
     * 
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param lpCount1 2D 2D 트리거 위치 데이타 중 첫번째(X) 위치를 저정하고 있는 H/W FIFO에 입력된 데이타 개수
     * @param upStatus1 2D 트리거 위치 데이타 중 첫번째(X) 위치를 저정하고 있는 H/W FIFO의 상태
     * @param lpCount2 2D 트리거 위치 데이타 중 두번째(Y) 위치를 저정하고 있는 H/W FIFO에 입력된 데이타 개수
     * @param upStatus2 2D 트리거 위치 데이타 중 두번째(Y) 위치를 저정하고 있는 H/W FIFO의 상태
     * 
	 * @details
	 * upStatus1/2 (FIFO 상태)
	 *   Bit 0: Data Empty
	 *   Bit 1: Data Full
	 *   Bit 2: Data Valid
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableReadFifoStatus(int lModuleNo, int lTablePos, ref int lpCount1, ref uint upStatus1, ref int lpCount2, ref uint upStatus2);
	
    /**
     * @brief 카운터 모듈의 지정한 테이블에 할당된 H/W 트리거 데이타 FIFO의 현재 위치 데이타 값 확인
     * 
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param dpTopData1 2D H/W 트리거 데이타 FIFO의 현재 데이타 중 첫번째(X) 위치 데이타를 확인
     * @param dpTopData2 2D H/W 트리거 데이타 FIFO의 현재 데이타 중 두번째(Y) 위치 데이타를 확인
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTableReadFifoData(int lModuleNo, int lTablePos, ref double dpTopData1, ref double dpTopData2);
	
	/**
     * @brief 카운터 모듈의 dimension 값 설정
     * 
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param dwDimension 1D/2D 설정값 (0: 1D, 1: 2D)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxcTableSetDimension(int lModuleNo, int lTablePos, int dwDimension);
	
	/**
     * @brief 카운터 모듈의 dimension 값 확인
     * 
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param dwDimension 1D/2D 설정값 저장 (0: 1D, 1: 2D)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxcTableGetDimension(int lModuleNo, int lTablePos, ref uint dwpDimension);

    /**
     * @brief 카운터 모듈의 트리거 출력 범위 중 하한 값 설정
     * 
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param dLowerPosition1 블록 하한 위치의 첫 번째 값
     * @param dLowerPosition2 블록 하한 위치의 두 번째 값
     * 
	 * @details
	 * 위치 주기 트리거 제품: 위치 주기로 트리거 출력을 발생시킬 범위 중 하한 값 설정
	 * 단위: AxcMotSetMoveUnitPerPulse로 설정 단위
	 * 하한값은 상한값보다 작은값을 설정해야 함
	 * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxcTableSetBlockLowerPos(int lModuleNo, int lTablePos, double dLowerPosition1, double dLowerPosition2);
    
	/**
     * @brief 카운터 모듈의 트리거 출력 범위 중 하한 값 설정 확인
     * 
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param dLowerPosition1 블록 하한 위치의 첫 번째 값 저장
     * @param dLowerPosition2 블록 하한 위치의 두 번째 값 저장
	 * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxcTableGetBlockLowerPos(int lModuleNo, int lTablePos, ref double dpLowerPosition1, ref double dpLowerPosition2);
    
	/**
     * @brief 카운터 모듈의 트리거 출력 범위 중 상한 값 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 번호
     * @param dUpperPosition1 블록 상한 위치의 첫 번째 값
     * @param dUpperPosition2 블록 상한 위치의 두 번째 값
	 *
	 * @details
	 * 위치 주기 트리거 제품: 위치 주기로 트리거 출력을 발생시킬 범위 중 상한 값 설정
	 * 단위: AxcMotSetMoveUnitPerPulse로 설정 단위
	 * 상한값은 하한값보다 큰값을 설정해야 함
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxcTableSetBlockUpperPos(int lModuleNo, int lTablePos, double dUpperPosition1, double dUpperPosition2);
    
	/**
     * @brief 카운터 모듈의 트리거 출력 범위 중 상한 값 설정 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 번호
     * @param dUpperPosition1 블록 상한 위치의 첫 번째 값 저장
     * @param dUpperPosition2 블록 상한 위치의 두 번째 값 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxcTableGetBlockUpperPos(int lModuleNo, int lTablePos, ref double dpUpperPosition1, ref double dpUpperPosition2);
	
	/**
     * @brief 카운터 모듈의 각 테이블에 설정된 정보로(트리거 마다 간격) 발생
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param dDistance 트리거 마다 위치 간격
	 *
	 * @details
	 * 이 함수는 트리거가 Disable되어 있으면 자동으로 Enable 시켜 패턴을 가진 트리거 발생
	 * 이 함수는 Trigger Mode가 CCGC_CNT_DISTANCE_PERIODIC_TRIGGER 모드가 아닐 경우 자동으로 트리거 모드를 CCGC_CNT_DISTANCE_PERIODIC_TRIGGER로 변경
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */	
	[DllImport("AXL.dll")] public static extern uint AxcTableTriggerDistancePatternShot(int lModuleNo, int lTablePos, double dDistance);
	
	/**
     * @brief 카운터 모듈의 각 테이블에 설정된 정보(트리거 마다 간격) 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lTablePos 테이블 위치
     * @param dDistance 트리거 마다 위치 간격 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxcTableGetDistancePatternShotData(int lModuleNo, int lTablePos, ref double dpDistance);
	
	/**
     * @brief 카운터 모듈의 각 채널에 할당 할 트리거 출력 포트 설정
     *
     * @param lChannelNo 채널 번호
     * @param dwTriggerOutPort 트리거 포트 번호 (0x0~0xF)
     * 
	 * @details
	 * uTriggerOutport (트리거 출력 포트)
	 *    Bit 0: 트리거 출력 0
	 *    Bit 1: 트리거 출력 1
	 *    Bit 2: 트리거 출력 2
	 *    Bit 3: 트리거 출력 3
	 *    ex1) 0x3(3)  : 출력 0, 1에 트리거 신호 출력
	 *    ex2) 0xF(255): 출력 0, 1, 2, 3에 트리거 신호 출력
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxcTriggerSetTriggerOutport(int lChannelNo, uint dwTriggerOutPort);
	
	/**
     * @brief 카운터 모듈의 각 채널에 할당 할 트리거 출력 포트 설정 확인
     *
     * @param lChannelNo 채널 번호
     * @param dwTriggerOutPort 트리거 포트 번호 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */	
	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetTriggerOutport(int lChannelNo, ref uint dwpTriggerOutPort);
   
}
