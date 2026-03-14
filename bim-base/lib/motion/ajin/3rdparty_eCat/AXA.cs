/**
 * @file AXA.cs
 *
 * @brief 아진익스텍 아날로그 라이브러리 헤더 파일.
 *
 * @author 아진엑스텍 주식회사
 * 
 * @copyright 저작권 (c) 아진엑스텍 주식회사
 * 
 * @website http://www.ajinextek.com
 * 
 * last_update 2024-12-15
 * 
 * @details 자세한 정보는 메뉴얼 참고
 */


using System;
using System.Runtime.InteropServices;

public class CAXA
{	 
    // 보드 및 모듈 정보 확인 함수 
	
	/**
     * @brief AIO 모듈 있는지 확인
	 *
     * @param upStatus AIO 모듈 존재 여부 (존재: 1, 없음: 0)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaInfoIsAIOModule(ref uint upStatus);
    
	/**
     * @brief AIO 모듈 번호 확인
	 *
     * @param lBoardNo 보드 번호
     * @param lModulePos 모듈 위치
     * @param lpModuleNo 모듈 번호 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaInfoGetModuleNo(int lBoardNo, int lModulePos, ref int lpModuleNo);
    
    /**
     * @brief AIO 모듈 개수 확인
     *
     * @param lpModuleCount 모듈 개수 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaInfoGetModuleCount(ref int lpModuleCount);
    
    /**
     * @brief 지정 모듈의 입력 채널 개수 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lpCount 입력 채널 개수 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaInfoGetInputCount(int lModuleNo, ref int lpCount);
    
    /**
     * @brief 지정 모듈의 출력 채널 개수 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lpCount 출력 채널 개수 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaInfoGetOutputCount(int lModuleNo, ref int lpCount);

    /**
     * @brief 지정 모듈의 첫 번째 채널 번호 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lpChannelNo 채널 번호 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaInfoGetChannelNoOfModuleNo(int lModuleNo, ref int lpChannelNo);
    
    /**
     * @brief 지정 모듈의 첫 번째 입력 채널 번호 확인 (입력 모듈, 입력/출력 통합 모듈용)
     *
     * @param lModuleNo 모듈 번호
     * @param lpChannelNo 채널 번호 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaInfoGetChannelNoAdcOfModuleNo(int lModuleNo, ref int lpChannelNo);

    /**
     * @brief 지정 모듈의 첫 번째 출력 채널 번호 확인 (입력 모듈, 입력/출력 통합 모듈용)
     *
     * @param lModuleNo 모듈 번호
     * @param lpChannelNo 채널 번호 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaInfoGetChannelNoDacOfModuleNo(int lModuleNo, ref int lpChannelNo);
    
    /**
     * @brief 지정 모듈 번호로 베이스 보드 번호, 모듈 위치, 모듈 ID 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lpBoardNo 보드 번호 저장
     * @param lpModulePos 모듈 위치 저장
     * @param upModuleID 모듈 ID 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaInfoGetModule(int lModuleNo, ref int lpBoardNo, ref int lpModulePos, ref uint upModuleID);
    
	/**
     * @brief 지정 모듈 번호로 Sub ID, 모듈 Name, 모듈 설명 확인
     *
     * @param lModuleNo EtherCAT 모듈 구분 위한 Sub ID
     * @param upModuleSubID 모듈 하위 ID 저장
     * @param szModuleName 모듈 이름 저장 (50 Bytes)
     * @param szModuleDescription 모듈 설명 저장 (80 Bytes)
     * 
	 * @details 지원 제품: EtherCAT
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaInfoGetModuleEx(int lModuleNo, ref uint upModuleSubID, System.Text.StringBuilder szModuleName, System.Text.StringBuilder szModuleDescription);
    
    /**
     * @brief 모듈 제어 가능 상태 확인
     *
     * @param lModuleNo 모듈 번호
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaInfoGetModuleStatus(int lModuleNo);
	
	// 입력 모듈 정보 검색 함수
	
	/**
     * @brief 지정 입력 채널 번호로 모듈 번호 확인
     *
     * @param lChannelNo 입력 채널 번호
     * @param lpModuleNo 모듈 번호 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiInfoGetModuleNoOfChannelNo(int lChannelNo, ref int lpModuleNo);
    
    /**
     * @brief 아날로그 입력 모듈의 전체 채널 개수 확인
     *
     * @param lpChannelCount 채널 개수 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiInfoGetChannelCount(ref int lpChannelCount);
	
	// 입력 모듈 인터럽트/채널 이벤트 설정 및 확인 함수
	
    /**
     * @brief 지정한 입력 채널에 수집된 데이터 처리를 위한 이벤트 수신 방법 설정
     *
     * @param lChannelNo 채널 번호
     * @param hWnd 윈도우 핸들
     * @param uMesssage 윈도우 핸들 메시지
     * @param pProc 인터럽트 발생 시 호출될 함수 포인터
     * @param pEvent 이벤트 핸들 저장
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
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiEventSetChannel(int lChannelNo, IntPtr hWnd, uint uMessage, CAXHS.AXT_INTERRUPT_PROC pProc, ref uint pEvent);
    
    /**
     * @brief 지정한 입력 채널에 이벤트 사용 유무 설정
     *
     * @param lChannelNo 채널 번호
     * @param uUse 이벤트 사용 여부 (1: 사용, 0: 미사용)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiEventSetChannelEnable(int lChannelNo, uint uUse);
    
    /**
     * @brief 지정한 입력 채널의 이벤트 사용 유무 확인
     *
     * @param lChannelNo 채널 번호
     * @param upUse 이벤트 사용 상태 저장 (1: 사용, 0: 미사용)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiEventGetChannelEnable(int lChannelNo, ref uint upUse);
    
    /**
     * @brief 지정한 여러 입력 채널의 이벤트 사용 유무 설정
     *
     * @param lSize 사용할 입력 채널 개수
     * @param lpChannelNo 사용할 채널 번호 배열 
     * @param uUse 이벤트 사용 여부 (1: 사용, 0: 미사용)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiEventSetMultiChannelEnable(int lSize, int[] lpChannelNo, uint uUse);
    
    /**
     * @brief 지정한 입력 채널의 이벤트 종류 설정
     *
     * @param lChannelNo 채널 번호
     * @param uMask 이벤트 종류 설정값
     * 
	 * @details
	 * uMask (이벤트 마스크 설정값)
	 *    DATA_EMPTY(1) --> 버퍼에 데이터가 없을 때
	 *    DATA_MANY(2)  --> 버퍼에 데이터가 상한 설정 값보다 많아질 때
	 *    DATA_SMALL(3) --> 버퍼에 데이터가 하한 설정 값보다 적어질 때
	 *    DATA_FULL(4)  --> 버퍼에 데이터가 꽉 찼을 때
	 * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiEventSetChannelMask(int lChannelNo, uint uMask);
    
    /**
     * @brief 지정한 입력 채널의 이벤트 종류 확인
     *
     * @param lChannelNo 채널 번호
     * @param upMask 이벤트 종류 설정값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiEventGetChannelMask(int lChannelNo, ref uint upMask);
    
    /**
     * @brief 지정한 여러 입력 채널의 이벤트 종류 설정
     *
     * @param lSize 사용 할 입력 채널 개수
     * @param lpChannelNo 사용 할 채널 번호 배열
     * @param uMask 이벤트 종류 설정값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiEventSetMultiChannelMask(int lSize, int[] lpChannelNo, uint uMask);
    
	/**
     * @brief 이벤트 발생 위치 확인
     *
     * @param lpChannelNo 이벤트가 발생한 채널 번호 저장
     * @param upMode 이벤트 종류 저장
     * 
	 * @details
	 * upMode (이벤트 종류)
	 *    AIO_EVENT_DATA_UPPER(1) --> 버퍼에 데이터가 없을 때
	 *    AIO_EVENT_DATA_LOWER(2) --> 버퍼에 데이터가 상한 설정 값보다 많아질 때
	 *    AIO_EVENT_DATA_FULL(3)  --> 버퍼에 데이터가 하한 설정 값보다 적어질 때
	 *    AIO_EVENT_DATA_EMPTY(4) --> 버퍼에 데이터가 꽉 찼을 때 
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiEventRead(ref int lpChannelNo, ref uint upMode);
    
	/**
     * @brief 지정한 모듈의 인터럽트 마스크 설정
     *
     * @param lModuleNo 모듈 번호
     * @param uMask 인터럽트 종류 설정값
     * 
	 * @details
	 * uMask (인터럽트 종류)
	 *    SCAN_END(1)        --> 셋팅된 채널 모두 ADC 변환이 한번 이루어 질 때 마다 인터럽트 발생
	 *    FIFO_HALF_FULL(2)  --> 모듈 내의 FIFO가 HALF이상 찼을 경우 내부 인터럽트 발생
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiInterruptSetModuleMask(int lModuleNo, uint uMask);
    
    /**
     * @brief 지정한 모듈의 인터럽트 마스크 확인
     *
     * @param lModuleNo 모듈 번호
     * @param upMask 인터럽트 마스크 설정값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiInterruptGetModuleMask(int lModuleNo, ref uint upMask);
    
	// 입력 모듈 파라미터 설정 및 확인 함수
	
	/**
     * @brief 지정한 입력 채널의 입력 전압 범위 설정
     *
     * @param lChannelNo 채널 번호
     * @param dMinVolt 최소 전압값
     * @param dMaxVolt 최대 전압값
     * 
	 * @details
	 * 1. AI4RB
	 *    dMinVolt: -10V or -5V로 설정 가능
	 *    dMaxVolt: 10V or 5V로 설정 가능
	 * 2. AI16Hx
	 *    dMinVolt: -10V 고정
	 *    dMaxVolt: 10V 고정
	 * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiSetRange(int lChannelNo, double dMinVolt, double dMaxVolt);
    
    /**
     * @brief 지정한 입력 채널의 입력 전압 범위 확인
     *
     * @param lChannelNo 채널 번호
     * @param dpMinVolt 최소 전압값 저장
     * @param dpMaxVolt 최대 전압값 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiGetRange(int lChannelNo, ref double dpMinVolt, ref double dpMaxVolt);

    /**
     * @brief 지정한 여러 입력 모듈의 허용 입력 전압 범위 설정
     *
     * @param lModuleNo 모듈 번호
     * @param dMinVolt 최소 전압값
     * @param dMaxVolt 최대 전압값
     * 
	 * @details
	 * RTEX-AI16F
	 *    Mode -5 ~ +5  : dMinVolt = -5,  dMaxVolt = +5
	 *    Mode -10 ~ +10: dMinVolt = -10, dMaxVolt = +10
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiSetRangeModule(int lModuleNo, double dMinVolt, double dMaxVolt);

    /**
     * @brief 지정한 여러 입력 모듈의 허용 입력 전압 범위 확인
     *
     * @param lModuleNo 모듈 번호
     * @param dMinVolt 최소 전압값 저장
     * @param dMaxVolt 최대 전압값 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiGetRangeModule(int lModuleNo, ref double dMinVolt, ref double dMaxVolt);
    
    /**
     * @brief 지정한 여러 입력 채널의 허용 입력 전압 범위 설정
     *
     * @param lSize 사용할 입력 채널 개수
     * @param lpChannelNo 사용할 채널 번호 배열
     * @param dMinVolt 최소 전압값
     * @param dMaxVolt 최대 전압값
     * 
	 * @details
	 * 1. AI4RB
	 *    dMinVolt: -10V or -5V로 설정 가능
	 *    dMaxVolt: 10V or 5V로 설정 가능
	 * 2. AI16Hx
	 *    dMinVolt: -10V 고정
	 *    dMaxVolt: 10V 고정
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiSetMultiRange(int lSize, int[] lpChannelNo, double dMinVolt, double dMaxVolt);
    
    /**
     * @brief 지정한 입력 모듈의 트리거 모드 설정
     *
     * @param lModuleNo 모듈 번호
     * @param uTriggerMode 트리거 모드 설정값
     * 
	 * @details
	 * uTriggerMode (트리거 모드 설정값)
	 *    NORMAL_MODE(1)   --> 사용자가 원하는 시점에 A/D변환하는 Software Trigger 방식
	 *    TIMER_MODE(2)    --> H/W의 내부 클럭을 이용해서 A/D변환하는 Trigger 방식
	 *    EXTERNAL_MODE(3) --> 외부 입력단자의 클럭을 이용해서 A/D변환하는 Trigger 방식
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiSetTriggerMode(int lModuleNo, uint uTriggerMode);
    
    /**
     * @brief 지정한 모듈의 트리거 모드 확인
     *
     * @param lModuleNo 모듈 번호
     * @param upTriggerMode 트리거 모드 설정값 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiGetTriggerMode(int lModuleNo, ref uint upTriggerMode);
    
    /**
     * @brief 지정한 입력 모듈의 Offset 설정 (단위: mVolt(mV))
     *
     * @param lModuleNo 모듈 번호
     * @param dMiliVolt mVolt 설정 값 (-100 ~ 100)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiSetModuleOffsetValue(int lModuleNo, double dMiliVolt);
    
    /**
     * @brief 지정한 입력 모듈의 Offset 값 확인 (단위: mVolt(mV))
     *
     * @param lModuleNo 모듈 번호
     * @param dpMiliVolt 밀리볼트 오프셋 값 저장 (-100 ~ 100)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiGetModuleOffsetValue(int lModuleNo, ref double dpMiliVolt); 

	// 입력 모듈 A/D 변환 함수 

    /**
     * @brief 사용자가 지정한 입력 채널의 아날로그 입력 값을 A/D 변환 후 전압 값으로 반환
     *
     * @param lChannelNo 채널 번호
     * @param dpVolt 전압 값 저장
	 *
	 * @details 이 함수 사용 전에 AxaSetTriggerModeAdc 함수를 사용하여 Normal Trigger Mode로 설정되어 있어야 함
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiSwReadVoltage(int lChannelNo, ref double dpVolt);
    
    /**
     * @brief 지정한 입력 채널의 아날로그 입력 값을 Digit 값으로 반환
     *
     * @param lChannelNo 채널 번호
     * @param upDigit 디지털 값 저장
	 *
	 * @details Normal Trigger Mode로 설정되어 있어야 함
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiSwReadDigit(int lChannelNo, ref uint upDigit);
    
    /**
     * @brief 지정한 여러 입력 채널의 아날로그 입력 값을 전압 값으로 반환
     *
     * @param lSize 채널 개수
     * @param lpChannelNo 채널 번호 배열
     * @param dpVolt 읽은 전압 값 저장
     * 
	 * @details Normal Trigger Mode로 설정되어 있어야 함
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiSwReadMultiVoltage(int lSize, int[] lpChannelNo, double[] dpVolt);
    
    /**
     * @brief 지정한 여러 입력 채널의 아날로그 입력 값을 Digit 값으로 반환
     *
     * @param lSize 채널 개수
     * @param lpChannelNo 채널 번호 배열
     * @param upDigit 읽은 디지털 값 저장
     * 
	 * @details Normal Trigger Mode로 설정되어 있어야 함
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiSwReadMultiDigit(int lSize, int[] lpChannelNo, uint[] upDigit);
    
	// Hardware Trigger Mode 함수
	
    /**
     * @brief 지정한 여러 입력 채널에 Immediate모드 사용 설정
     *
     * @param lSize 채널 개수
     * @param lpChannelNo 채널 번호 배열
     * @param lpWordSize 채널당 데이터 개수
	 *
	 * @details 이 함수 사용 전에 AxaSetTriggerModeAdc 함수를 사용하여 Timer Trigger Mode로 설정되어 있어야 함
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwSetMultiAccess(int lSize, int[] lpChannelNo, int[] lpWordSize);
    
    /**
     * @brief Immediate 모드로 설정되어 수집된 데이터 확인
     *
     * @param dpBuffer 데이터(2차원 배열) 저장
	 *
	 * @details
	 * 이 함수를 사용하기 전에 AxaiHwSetMultiAccess함수를 이용하여 설정값을 지정해야 함
	 * AxaSetTriggerModeAdc 함수를 사용하여 Timer Trigger Mode로 설정되어 있어야 함
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwStartMultiAccess(double[,] dpBuffer);
    
    /**
     * @brief 지정한 모듈의 샘플링 간격을 주파수 단위(Hz)로 설정
     *
     * @param lModuleNo 모듈 번호
     * @param dSampleFreq 샘플링 주파수 값 (10 ~ 100000)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwSetSampleFreq(int lModuleNo, double dSampleFreq);
    
    /**
     * @brief 지정한 모듈의 샘플링 간격을 주파수 단위로(Hz) 설정된 값 확인
     *
     * @param lModuleNo 모듈 번호
     * @param dpSampleFreq 샘플링 주파수 값 저장 (10 ~ 100000)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwGetSampleFreq(int lModuleNo, ref double dpSampleFreq);
    
    /**
     * @brief 지정한 모듈의 샘플링 간격을 시간 단위(uSec)로 설정
     *
     * @param lModuleNo 모듈 번호
     * @param dSamplePeriod 샘플링 주기 값 (100000 ~ 1000000000)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwSetSamplePeriod(int lModuleNo, double dSamplePeriod);
    
    /**
     * @brief 지정한 모듈의 샘플링 간격을 시간 단위(uSec)로 설정된 값을 확인
     *
     * @param lModuleNo 모듈 번호
     * @param dpSamplePeriod 샘플링 주기 값 저장 (100000 ~ 1000000000)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwGetSamplePeriod(int lModuleNo, ref double dpSamplePeriod);
    
    /**
     * @brief 지정한 입력 채널의 버퍼가 Full 일 경우 관리 방식 설정
     *
     * @param lChannelNo 채널 번호
     * @param uFullMode 관리 방식 설정값
	 *
	 * @details
	 * uFullMode (관리 방식 설정값)
	 *    NEW_DATA_KEEP(0)  --> 새로운 데이터 유지
	 *    CURR_DATA_KEEP(1) --> 이전 데이터 유지
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwSetBufferOverflowMode(int lChannelNo, uint uFullMode);
    
    /**
     * @brief 지정한 입력 채널의 버퍼가 Full 일 경우 관리 방식 확인
     *
     * @param lChannelNo 채널 번호
     * @param upFullMode 버퍼 오버플로우 모드 설정값 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwGetBufferOverflowMode(int lChannelNo, ref uint upFullMode);
    
    /**
     * @brief 지정한 여러 입력 채널의 버퍼가 Full 일 경우 관리 방식 설정
     *
     * @param lSize 사용할 입력 채널 개수
     * @param lpChannelNo 사용할 채널 번호 배열
     * @param uFullMode 버퍼 오버플로우 모드 설정값
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwSetMultiBufferOverflowMode(int lSize, int[] lpChannelNo, uint uFullMode);
    
    /**
     * @brief 지정한 입력 채널 버퍼의 상한 값과 하한 값 설정
     *
     * @param lChannelNo 채널 번호
     * @param lLowLimit 하한 제한 값
     * @param lUpLimit 상한 제한 값
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwSetLimit(int lChannelNo, int lLowLimit, int lUpLimit);
    
    /**
     * @brief 지정한 입력 채널 버퍼의 상한 값과 하한 값 확인
     *
     * @param lChannelNo 채널 번호
     * @param lpLowLimit 하한 제한 값 저장
     * @param lpUpLimit 상한 제한 값 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwGetLimit(int lChannelNo, ref int lpLowLimit, ref int lpUpLimit);
    
    /**
     * @brief 지정한 여러 입력 채널 버퍼의 상한 값과 하한 값 설정
     *
     * @param lSize 사용할 입력 채널 개수
     * @param lpChannelNo 사용할 채널 번호 배열
     * @param lLowLimit 하한 제한 값
     * @param lUpLimit 상한 제한 값
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwSetMultiLimit(int lSize, int[] lpChannelNo, int lLowLimit, int lUpLimit);
    
    /**
     * @brief 지정한 여러 입력 채널의 H/W 타이머를 이용한 A/D 변환 시작
     *
     * @param lSize 사용할 입력 채널 개수
     * @param lpChannelNo 사용할 채널 번호 배열
     * @param lBuffSize 각 채널에 할당되는 버퍼의 크기
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwStartMultiChannel(int lSize, int[] lpChannelNo, int lBuffSize);
    
    /**
     * @brief 지정한 입력 채널의 H/W 타이머를 이용한 A/D 변환 시작
     *
     * @param lChannelNo 채널 번호
     * @param lBuffSize 버퍼 크기
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwStartSingleChannelAdc(int lChannelNo, int lBuffSize);
    
    /**
     * @brief 지정한 입력 채널의 H/W 타이머를 이용한 연속 신호 A/D 변환 중지
     *
     * @param lChannelNo 채널 번호
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwStopSingleChannelAdc(int lChannelNo);
    
    /**
     * @brief 지정한 여러 입력 채널의 A/D 변환 시작 후 지정한 개수 만큼의 평균 값을 버퍼에 저장
     *
     * @param lSize 사용할 입력 채널 개수
     * @param lpChannelNo 사용할 채널 번호 배열
     * @param lFilterCount Filtering 할 데이터 개수
     * @param lBuffSize 각 채널에 할당되는 버퍼 개수
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwStartMultiFilter(int lSize, int[] lpChannelNo, int lFilterCount, int lBuffSize);
    
    /**
     * @brief 지정한 입력 채널의 H/W 타이머를 이용한 연속 A/D 변환 중지
     *
     * @param lModuleNo 모듈 번호
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwStopMultiChannel(int lModuleNo);
    
    /**
     * @brief 지정한 입력 채널의 버퍼에 저장되어 있는 데이터 개수 확인
     *
     * @param lChannelNo 채널 번호
     * @param lpDataLength 읽은 데이터 길이 저장
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwReadDataLength(int lChannelNo, ref int lpDataLength);
    
    /**
     * @brief 지정한 입력 채널의 H/W 타이머를 이용하여 A/D 변환된 값을 전압값으로 확인
     *
     * @param lChannelNo 채널 번호
     * @param lpSize 읽을 샘플 개수 저장
     * @param dpVolt 읽은 전압 배열 값 저장
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwReadSampleVoltage(int lChannelNo, ref int lpSize, double[] dpVolt);
    
    /**
     * @brief 지정한 입력 채널의 H/W 타이머를 이용하여 A/D 변환된 값을 Digit 값으로 확인
     *
     * @param lChannelNo 채널 번호
     * @param lpsize 읽을 샘플 개수 저장
     * @param upDigit 읽은 Digit 배열 값 저장
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwReadSampleDigit(int lChannelNo, ref int lpSize, uint[] upDigit);

	// 입력 모듈 버퍼 상태 체크 함수

	/**
     * @brief 지정한 입력 채널의 메모리 버퍼에 데이터가 없는 지 검사
     *
     * @param lChannelNo 채널 번호
     * @param upEmpty 비어 있는지 여부 저장 (0: 데이터 있을 경우, 1: 데이터 없을 경우)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwIsBufferEmpty(int lChannelNo, ref uint upEmpty);
    
    /**
     * @brief 지정한 입력 채널의 메모리 버퍼에 설정되어 있는 상한 값보다 데이터가 많은 지 검사
     *
     * @param lChannelNo 채널 번호
     * @param upUpper 상한 초과 여부 저장 (0: 상한 값보다 적을 경우, 1: 상한 값보다 많을 경우)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwIsBufferUpper(int lChannelNo, ref uint upUpper);
    
    /**
     * @brief 지정한 입력 채널의 메모리 버퍼에 설정되어 있는 하한 값보다 데이터가 적은 지 검사
     *
     * @param lChannelNo 채널 번호
     * @param upLower 하한 미만 여부 저장 (0: 하한 값보다 많을 경우, 1: 하한 값보다 적을 경우)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiHwIsBufferLower(int lChannelNo, ref uint upLower);

	// External Trigger Mode 함수

	/**
     * @brief 지정한 입력 모듈의 선택된 채널들의 외부 트리거 모드 시작
     *
     * @param lModuleNo 모듈 번호
     * @param lSize 지정한 입력 모듈에서 외부트리거를 사용 할 채널갯수
     * @param lpChannelPos 지정한 입력 모듈에서 외부 트리거를 사용 할 채널 Index 배열
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiExternalStartADC(int lModuleNo, int lSize, int[] lpChannelPos);

    /**
     * @brief 지정한 입력 모듈의 외부 트리거 모드 정지
     *
     * @param lModuleNo 모듈 번호
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiExternalStopADC(int lModuleNo);

    /**
     * @brief 지정한 입력 모듈의 FIFO 상태 반환
     *
     * @param lModuleNo 모듈 번호
     * @param dwpStatus FIFO 상태 저장
	 *
	 * @details
	 * upStatus (FIFO 상태 저장)
	 *    FIFO_DATA_EXIST(0)
	 *    FIFO_DATA_EMPTY(1)
	 *    FIFO_DATA_HALF(2)
	 *    FIFO_DATA_FULL(6)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiExternalReadFifoStatus(int lModuleNo, ref uint upStatus);

    /**
     * @brief 지정한 입력 모듈의 외부 신호에 의해 변환된 A/D 값을 읽음
     *
     * @param lModuleNo 모듈 번호
     * @param lSize 채널 개수 (AxaiExternalStartADC에 사용한 채널 개수와 동일 해야 함)
     * @param lpChannelPos 채널 Index (AxaiExternalStartADC에 사용한 채널의 Index와 동일 해야 함)
     * @param lDataSize 외부 트리거에 의해 A/D 변환된 값을 한번에 읽어 올 최대 데이타 개수
     * @param lBuffSize 외부에서(사용자 Program) 할당한 Data Buffer Size
     * @param lStartDataPos 외부에서(사용자 Program) 할당한 Data Buffer에 저장 시작 할 위치
     * @param dpVolt A/D 변환된 값을 할당 받을 2차원 배열
     * @param lpRetDataSize A/D 변환된 값이 Data Buffer에 실제 할당된 개수
     * @param dwpStatus A/D 변환된 값을 Fifo(H/W Buffer)로 부터 읽을 때 Fifo 상태 반환
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaiExternalReadVoltage(int lModuleNo, int lSize, ref int lpChannelPos, int lDataSize, int lBuffSize, int lStartDataPos, double[,] dpVolt, ref int lpRetDataSize, ref uint upStatus);
    
	// 출력 모듈 Info
	
	/**
     * @brief 지정한 출력 채널 번호로 모듈 번호 확인
     *
     * @param lChannelNo 채널 번호
     * @param lpModuleNo 해당 채널 번호의 모듈 번호 저장
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaoInfoGetModuleNoOfChannelNo(int lChannelNo, ref int lpModuleNo);
    
    /**
     * @brief 아날로그 출력 모듈의 전체 채널 개수 확인
     *
     * @param lpChannelCount 전체 채널 개수 저장
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaoInfoGetChannelCount(ref int lpChannelCount);

	// 출력 모듈 설정 및 확인 함수
	
	/**
     * @brief 지정한 출력 채널에 출력 전압 범위 설정
     *
     * @param lChannelNo 채널 번호
     * @param dMinVolt 최소 전압 값
     * @param dMaxVolt 최대 전압 값
	 *
	 * @details
	 * AO4R, AO2Hx (dMinVolt: -10V, dMaxVolt: 10V)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaoSetRange(int lChannelNo, double dMinVolt, double dMaxVolt);
    
    /**
     * @brief 지정한 출력 채널의 출력 전압 범위 확인
     *
     * @param lChannelNo 채널 번호
     * @param dpMinVolt 최소 전압 값 저장
     * @param dpMaxVolt 최대 전압 값 저장
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaoGetRange(int lChannelNo, ref double dpMinVolt, ref double dpMaxVolt);
    
    /**
     * @brief 지정한 여러 출력 채널의 출력 전압 범위 설정
     *
     * @param lSize 출력 채널 개수
     * @param lpChannelNo 채널 번호 배열
     * @param dMinVolt 최소 출력 전압 값
     * @param dMaxVolt 최대 출력 전압 값
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaoSetMultiRange(int lSize, int[] lpChannelNo, double dMinVolt, double dMaxVolt);
    
    /**
     * @brief 지정한 출력 채널의 입력된 전압 출력
     *
     * @param lChannelNo 출력 채널 번호
     * @param dVolt 출력 전압값(V)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaoWriteVoltage(int lChannelNo, double dVolt);
    
    /**
     * @brief 지정한 여러 출력 채널의 입력된 전압 출력
     *
     * @param lSize 사용할 출력 채널 개수
     * @param lpChannelNo 사용할 채널 번호 배열 
     * @param dpVolt 출력 전압 배열 
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaoWriteMultiVoltage(int lSize, int[] lpChannelNo, double[] dpVolt);
    
    /**
     * @brief 지정한 출력 채널에 출력하고 있는 전압값 확인
     *
     * @param lChannelNo 출력 채널 번호
     * @param dpVolt 출력 전압값(V)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaoReadVoltage(int lChannelNo, ref double dpVolt);
    
    /**
     * @brief 지정한 여러 출력 채널의 출력되는 전압 값 확인
     *
     * @param lSize 사용할 출력 채널 개수
     * @param lpChannelNo 사용할 채널 번호 배열
     * @param dpVolt 출력 전압 배열
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaoReadMultiVoltage(int lSize, int[] lpChannelNo, double[] dpVolt);
    
	// AXA User Define Pattern Generator
	
    /**
     * @brief Channel User Define Pattern Generator 설정 함수
     *
     * @param lChannelNo 채널 번호
     * @param lLoopCnt 반복 횟수 (0: 입력된 패턴 무한 반복, value: 지정된 횟수 만큼 입력 패컨 출력 후 마지막 패턴 유지, MAX: 60000)
     * @param lPatternSize 입력 패턴 개수 (MAX: 8192)
     * @param dpPattern 패턴 값 저장
     *
	 * @details AxaoPgSetUserInterval에 설정된 시간 마다 Pattern을 순차적으로 출력
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaoPgSetUserPatternGenerator(int lChannelNo, int lLoopCnt, int lPatternSize, double[] dpPattern);
    
    /**
     * @brief User Define Pattern Generator 확인 함수
     *
     * @param lChannelNo 채널 번호
     * @param lpLoopCnt 반복 횟수 저장
     * @param lpPatternSize 입력 패컨 개수 저장
     * @param dpPattern 입력 패턴 값 저장
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaoPgGetUserPatternGenerator(int lChannelNo, ref int lpLoopCnt, ref int lpPatternSize, double[] dpPattern);
    
    /**
     * @brief Channel의 Pattern Generator Interval 설정 함수
     *
     * @param lChannelNo 채널 번호
     * @param dInterval 지연 시간 (단위: us, 기본 Resolution: 500 uSec)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaoPgSetUserInterval(int lChannelNo, double dInterval);
    
    /**
     * @brief Channel의 Pattern Generator Interval 확인 함수
     *
     * @param lChannelNo 채널 번호
     * @param dpInterval 지연 시간 저장 (단위: us)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaoPgGetUserInterval(int lChannelNo, ref double dpInterval);
    
    /**
     * @brief Channel의 Pattern Index, Loop Count 확인 함수
     *
     * @param lChannelNo 채널 번호
     * @param lpIndexNum 현재 User Pattern의 Index 저장
     * @param lpLoopCnt 현재 반복 횟수 저장
     * @param dwpInBusy Pattern Generator의 구동 유무
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaoPgGetStatus(int lChannelNo, ref int lpIndexNum, ref int lpLoopCnt, ref uint dwpInBusy);
    
    /**
     * @brief Channel의 User Define Pattern Generator 시작 함수 (AO 출력 시작)
     *
     * @param lpChannelNo 시작할 채널 번호 배열
     * @param lSize 채널 개수
	 *
	 * @details 입력된 채널에 대하여 동시에 패턴 생성 기능 시작
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaoPgSetUserStart(int[] lpChannelNo, int lSize);
    
    /**
     * @brief Channel의 User Define Pattern Generator 정지 함수 (AO 출력 정지)
     *
     * @param lpChannelNo 정지 할 채널 번호 배열
     * @param lSize 채널 개수
	 *
	 * @details 출력 정지 시 출력값은 0 Volt로 전환
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaoPgSetUserStop(int[] lpChannelNo, int lSize);
    
    /**
     * @brief Pattern Data 초기화 함수 (모든 영역 0x00으로 Reset)
     *
     * @param lChannelNo 채널 번호
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaPgSetUserDataReset(int lChannelNo);
    
    /**
     * @brief 지정한 출력 모듈의 Network가 끊겼을 경우 출력 상태를 채널 별로 설정
     *
     * @param lChannelNo 채널 번호 (분산형 슬레이브 제품만 지원 함)
     * @param dwSetValue 설정 할 변수 값 (Default: 0)
     *
	 * @details
	 * dwSetValue (설정 할 변수 값)
	 *    0 --> Network 끊어 지기 전 상태 유지
	 *    1 --> Analog Max
	 *    2 --> Analog MIN
	 *    3 --> User Value (Default user value는 0V로 설정 됨, AxaoSetNetworkErrorUserValue() 함수로 변경가능)
	 *    4 --> Analog 0 V
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaoSetNetworkErrorAct(int lChannelNo, uint dwSetValue);
    
    /**
     * @brief 지정한 출력 모듈의 Network가 끊겼을 경우 출력 상태를 Byte 단위로 설정
     *
     * @param lChannelNo 채널 번호 (분산형 슬레이브 제품만 지원 함)
     * @param dVolt 사용자 정의 아날로그 출력 전압 값
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaoSetNetworkErrorUserValue(int lChannelNo, uint dVolt);

    

	//지정한 여러 출력 채널에 입력된 전압이 출력 된다.
	[DllImport("AXL.dll")] public static extern uint AxaoWriteMultiDigit(int lSize, int[] lpChannelNo, int[] dpDigit);

	//지정한 출력 채널에 Digit 값으로 반환한다.
    [DllImport("AXL.dll")] public static extern uint AxaoReadDigit(int lChannelNo, ref int upDigit);

	//지정한 여러 출력 채널에 출력되는 전압 값을 확인한다.
    [DllImport("AXL.dll")] public static extern uint AxaoReadMultiDigit(int lSize, int[] lpChannelNo, int[] dpDigit);
    
    
}

