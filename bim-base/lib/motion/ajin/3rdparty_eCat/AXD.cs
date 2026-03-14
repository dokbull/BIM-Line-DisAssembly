/**
 * @file AXD.cs
 *
 * @brief 아진익스텍 디지털 라이브러리 헤더 파일.
 *
 * @author 아진엑스텍 주식회사
 *
 * @copyright 저작권 (c) 아진엑스텍 주식회사
 *
 * @website http://www.ajinextek.com
 *
 * last_update 2024-12-15
 *
 * @details 자세한 정보는 메뉴얼을 참고해 주세요.
 */


using System;
using System.Runtime.InteropServices;

public class CAXD
{
	// 보드 및 모듈 정보 
	
	/**
     * @brief DIO 모듈 존재 확인
     *
     * @param upStatus 모듈 상태 저장 (0: 없음, 1: 있음)
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdInfoIsDIOModule(ref uint upStatus);
    
    /**
     * @brief DIO 모듈 번호 확인
     *
     * @param lBoardNo 보드 번호
     * @param lModulePos 모듈 위치
     * @param lpModuleNo 모듈 번호 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdInfoGetModuleNo(int lBoardNo, int lModulePos, ref int lpModuleNo);
    
    /**
     * @brief DIO 입출력 모듈 개수 확인
     *
     * @param lpModuleCount 모듈 개수 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdInfoGetModuleCount(ref int lpModuleCount);
    
    /**
     * @brief 지정한 모듈의 입력 접점 개수 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lpCount 입력 접점 개수 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdInfoGetInputCount(int lModuleNo, ref int lpCount);
    
    /**
     * @brief 지정한 모듈의 출력 접점 개수 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lpCount 출력 접점 개수 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdInfoGetOutputCount(int lModuleNo, ref int lpCount);
    
    /**
     * @brief 지정한 모듈 번호로 베이스 보드 번호, 모듈 위치, 모듈 ID 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lpBoardNo 보드 번호 저장
     * @param lpModulePos 모듈 위치 저장
     * @param upModuleID 모듈 ID 저장
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdInfoGetModule(int lModuleNo, ref int lpBoardNo, ref int lpModulePos, ref uint upModuleID);
    
    /**
     * @brief 지정한 모듈 번호로 해당 모듈의 Sub ID, Name 설명 확인
     *
     * @param lModuleNo 모듈 번호
     * @param upModuleSubID 모듈 SubID 저장
     * @param szModuleName 모듈명 저장 (50 Bytes)
     * @param szModuleDescription 모듈 설명 저장 (80 Bytes)
     *
	 * @details 지원 제품: EtherCAT
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdInfoGetModuleEx(int lModuleNo, ref uint upModuleSubID, System.Text.StringBuilder szModuleName, System.Text.StringBuilder szModuleDescription);

    /**
     * @brief 해당 모듈이 제어가 가능 상태 확인
     *
     * @param lModuleNo 모듈 번호
     *
     * @return 함수 호출 성공 시 모듈 상태 값을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdInfoGetModuleStatus(int lModuleNo);

	// 인터럽트 설정 확인

	/**
     * @brief 지정된 모듈에 대한 인터럽트 설정
     *
     * @param lChannelNo 모듈 번호
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
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptSetModule(int lModuleNo, IntPtr hWnd, uint uMessage, CAXHS.AXT_INTERRUPT_PROC pProc, ref uint pEvent);
    
    /**
     * @brief 지정한 모듈의 인터럽트 사용 유무 설정
     *
     * @param lModuleNo 모듈 번호
     * @param uUse 인터럽트 사용 여부 (0: 해제, 1: 설정)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptSetModuleEnable(int lModuleNo, uint uUse);
    
    /**
     * @brief 지정한 모듈의 인터럽트 사용 유무 설정 확인
     *
     * @param lModuleNo 모듈 번호
     * @param uUse 인터럽트 사용 여부 확인 (0: 해제, 1: 설정)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptGetModuleEnable(int lModuleNo, ref uint upUse);

    /**
     * @brief 이벤트 방식 인터럽트 사용시 인터럽트 발생 위치 확인
     *
     * @param lpModuleNo 모듈 번호 저장
     * @param upFlag 인터럽트 발생 위치 저장
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptRead(ref int lpModuleNo, ref uint upFlag);

	// 인터럽트 상승 / 하강 에지 설정 확인

	/**
     * @brief 지정한 입력 접점 모듈 Interrupt Rising / Falling Edge register의 Offset 위치에서 bit 단위로 상승 또는 하강 에지 값 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param uMode 인터럽트 엣지 동작 모드 (0: DOWN EDGE, 1: UP EDGE)
     * @param uValue 인터럽트 엣지 설정 값 (0: DISABLE, 1: ENABLE)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptEdgeSetBit(int lModuleNo, int lOffset, uint uMode, uint uValue);
    
    /**
     * @brief 지정한 입력 접점 모듈 Interrupt Rising / Falling Edge register의 Offset 위치에서 byte 단위로 상승 또는 하강 에지 값 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param uMode 인터럽트 엣지 동작 모드 (0: DOWN EDGE, 1: UP EDGE)
     * @param uValue 인터럽트 엣지 설정 값 (0x00 ~ 0xFF: '1'로 Setting 된 부분 인터럽트 설정)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptEdgeSetByte(int lModuleNo, int lOffset, uint uMode, uint uValue);
    
    /**
     * @brief 지정한 입력 접점 모듈 Interrupt Rising / Falling Edge register의 Offset 위치에서 word 단위로 상승 또는 하강 에지 값 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param uMode 인터럽트 엣지 동작 모드 (0: DOWN EDGE, 1: UP EDGE)
     * @param uValue 인터럽트 엣지 설정 값 (0x0000 ~ 0xFFFF: '1'로 Setting 된 부분 인터럽트 설정)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptEdgeSetWord(int lModuleNo, int lOffset, uint uMode, uint uValue);
    
    /**
     * @brief 지정한 입력 접점 모듈 Interrupt Rising / Falling Edge register의 Offset 위치에서 double word 단위로 상승 또는 하강 에지 값 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param uMode 인터럽트 엣지 동작 모드 (0: DOWN EDGE, 1: UP EDGE)
     * @param uValue 인터럽트 엣지 설정 값 (0x00000000 ~ 0xFFFFFFFF: '1'로 Setting 된 부분 인터럽트 설정)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptEdgeSetDword(int lModuleNo, int lOffset, uint uMode, uint uValue);
    
    /**
     * @brief 지정한 입력 접점 모듈 Interrupt Rising / Falling Edge register의 Offset 위치에서 bit 단위로 상승 또는 하강 에지 값 설정 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param uMode 인터럽트 엣지 동작 모드 (0: DOWN EDGE, 1: UP EDGE)
     * @param uValue 인터럽트 엣지 설정 값 (0: DISABLE, 1: ENABLE)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptEdgeGetBit(int lModuleNo, int lOffset, uint uMode, ref uint upValue);
    
    /**
     * @brief 지정한 입력 접점 모듈 Interrupt Rising / Falling Edge register의 Offset 위치에서 byte 단위로 상승 또는 하강 에지 값 설정 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param uMode 인터럽트 엣지 동작 모드 (0: DOWN EDGE, 1: UP EDGE)
     * @param uValue 인터럽트 엣지 설정 값 저장 (0x00 ~ 0xFF: '1'로 Setting 된 부분 인터럽트 설정)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptEdgeGetByte(int lModuleNo, int lOffset, uint uMode, ref uint upValue);
    
    /**
     * @brief 지정한 입력 접점 모듈 Interrupt Rising / Falling Edge register의 Offset 위치에서 word 단위로 상승 또는 하강 에지 값 설정 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param uMode 인터럽트 엣지 동작 모드 (0: DOWN EDGE, 1: UP EDGE)
     * @param uValue 인터럽트 엣지 설정 값 확인 (0x0000 ~ 0xFFFF: '1'로 Setting 된 부분 인터럽트 설정)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptEdgeGetWord(int lModuleNo, int lOffset, uint uMode, ref uint upValue);
    
    /**
     * @brief 지정한 입력 접점 모듈 Interrupt Rising / Falling Edge register의 Offset 위치에서 double word 단위로 상승 또는 하강 에지 값 설정 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param uMode 인터럽트 엣지 동작 모드 (0: DOWN EDGE, 1: UP EDGE)
     * @param uValue 인터럽트 엣지 설정 값 확인 (0x00000000 ~ 0xFFFFFFFF: '1'로 Setting 된 부분 인터럽트 설정)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptEdgeGetDword(int lModuleNo, int lOffset, uint uMode, ref uint upValue);
    
    /**
     * @brief 전체 입력 접점 모듈, Interrupt Rising / Falling Edge register의 Offset 위치에서 bit 단위로 상승 또는 하강 에지 값 설정
     *
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param uMode 인터럽트 엣지 동작 모드 (0: DOWN EDGE, 1: UP EDGE)
     * @param uValue 인터럽트 엣지 설정 값 (0: DISABLE, 1: ENABLE)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptEdgeSet(int lOffset, uint uMode, uint uValue);
    
    /**
     * @brief 전체 입력 접점 모듈, Interrupt Rising / Falling Edge register의 Offset 위치에서 bit 단위로 상승 또는 하강 에지 값 설정 확인
     *
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param uMode 인터럽트 엣지 동작 모드 (0: DOWN EDGE, 1: UP EDGE)
     * @param uValue 인터럽트 엣지 설정 값 저장 (0: DISABLE, 1: ENABLE)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptEdgeGet(int lOffset, uint uMode, ref uint upValue);

	// 입력 레벨 설정 확인

	/**
     * @brief 지정한 입력 접점 모듈의 Offset 위치에서 bit 단위로 데이터 레벨 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param uLevel 입력 레벨 설정 값 (0: LOW, 1: HIGH)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiLevelSetInportBit(int lModuleNo, int lOffset, uint uLevel);
    
    /**
     * @brief 지정한 입력 접점 모듈의 Offset 위치에서 byte 단위로 데이터 레벨 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param uLevel 입력 레벨 설정 값 (0x00 ~ 0xFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiLevelSetInportByte(int lModuleNo, int lOffset, uint uLevel);
    
    /**
     * @brief 지정한 입력 접점 모듈의 Offset 위치에서 word 단위로 데이터 레벨 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param uLevel 입력 레벨 설정 값 (0x0000 ~ 0xFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiLevelSetInportWord(int lModuleNo, int lOffset, uint uLevel);
    
    /**
     * @brief 지정한 입력 접점 모듈의 Offset 위치에서 double word 단위로 데이터 레벨 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param uLevel 입력 레벨 설정 값 (0x00000000 ~ 0xFFFFFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiLevelSetInportDword(int lModuleNo, int lOffset, uint uLevel);
    
    /**
     * @brief 지정한 입력 접점 모듈의 Offset 위치에서 bit 단위로 데이터 레벨 설정 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param uLevel 입력 레벨 설정 값 저장 (0: LOW, 1: HIGH)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiLevelGetInportBit(int lModuleNo, int lOffset, ref uint upLevel);
    
    /**
     * @brief 지정한 입력 접점 모듈의 Offset 위치에서 byte 단위로 데이터 레벨 설정 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param uLevel 입력 레벨 설정 값 저장 (0x00 ~ 0xFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiLevelGetInportByte(int lModuleNo, int lOffset, ref uint upLevel);
    
    /**
     * @brief 지정한 입력 접점 모듈의 Offset 위치에서 word 단위로 데이터 레벨 설정 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param uLevel 입력 레벨 설정 값 저장 (0x0000 ~ 0xFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiLevelGetInportWord(int lModuleNo, int lOffset, ref uint upLevel);
    
    /**
     * @brief 지정한 입력 접점 모듈의 Offset 위치에서 double word 단위로 데이터 레벨 설정 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param uLevel 입력 레벨 설정 값 저장 (0x00000000 ~ 0xFFFFFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiLevelGetInportDword(int lModuleNo, int lOffset, ref uint upLevel);
    
    /**
     * @brief 전체 입력 접점 모듈의 Offset 위치에서 bit 단위로 데이터 레벨 설정
     *
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param uLevel 입력 레벨 설정 값 (0: LOW, 1: HIGH)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiLevelSetInport(int lOffset, uint uLevel);
    
    /**
     * @brief 전체 입력 접점 모듈의 Offset 위치에서 bit 단위로 데이터 레벨 설정 확인
     *
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param uLevel 입력 레벨 설정 값 저장 (0: LOW, 1: HIGH)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiLevelGetInport(int lOffset, ref uint upLevel);
    
	// 출력 레벨 설정 확인
	
	/**
     * @brief 지정한 출력 접점 모듈의 Offset 위치에서 bit 단위로 데이터 레벨 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param uLevel 출력 레벨 설정 값 (0: LOW, 1: HIGH)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoLevelSetOutportBit(int lModuleNo, int lOffset, uint uLevel);
    
    /**
     * @brief 지정한 출력 접점 모듈의 Offset 위치에서 byte 단위로 데이터 레벨 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param uLevel 출력 레벨 설정 값 (0x00 ~ 0xFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoLevelSetOutportByte(int lModuleNo, int lOffset, uint uLevel);
    
    /**
     * @brief 지정한 출력 접점 모듈의 Offset 위치에서 word 단위로 데이터 레벨 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param uLevel 출력 레벨 설정 값 (0x0000 ~ 0xFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoLevelSetOutportWord(int lModuleNo, int lOffset, uint uLevel);
    
    /**
     * @brief 지정한 출력 접점 모듈의 Offset 위치에서 double word 단위로 데이터 레벨 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param uLevel 출력 레벨 설정 값 (0x00000000 ~ 0xFFFFFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoLevelSetOutportDword(int lModuleNo, int lOffset, uint uLevel);
    
    /**
     * @brief 지정한 출력 접점 모듈의 Offset 위치에서 bit 단위로 데이터 레벨 설정 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param uLevel 출력 레벨 설정 값 저장 (0: LOW, 1: HIGH)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoLevelGetOutportBit(int lModuleNo, int lOffset, ref uint upLevel);
    
    /**
     * @brief 지정한 출력 접점 모듈의 Offset 위치에서 byte 단위로 데이터 레벨 설정 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param uLevel 출력 레벨 설정 값 저장 (0x00 ~ 0xFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoLevelGetOutportByte(int lModuleNo, int lOffset, ref uint upLevel);
    
    /**
     * @brief 지정한 출력 접점 모듈의 Offset 위치에서 word 단위로 데이터 레벨 설정 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param uLevel 출력 레벨 설정 값 저장 (0x0000 ~ 0xFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoLevelGetOutportWord(int lModuleNo, int lOffset, ref uint upLevel);
    
    /**
     * @brief 지정한 출력 접점 모듈의 Offset 위치에서 double word 단위로 데이터 레벨 설정 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param uLevel 출력 레벨 설정 값 저장 (0x00000000 ~ 0xFFFFFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoLevelGetOutportDword(int lModuleNo, int lOffset, ref uint upLevel);
    
    /**
     * @brief 전체 출력 접점 모듈의 Offset 위치에서 bit 단위로 데이터 레벨 설정
     *
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param uLevel 출력 레벨 설정 값 (0: LOW, 1: HIGH)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoLevelSetOutport(int lOffset, uint uLevel);
    
    /**
     * @brief 전체 출력 접점 모듈의 Offset 위치에서 bit 단위로 데이터 레벨 설정 확인
     *
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param uLevel 출력 레벨 설정 값 저장 (0: LOW, 1: HIGH)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoLevelGetOutport(int lOffset, ref uint upLevel);

    // 출력 포트 쓰기

    /**
     * @brief 전체 출력 접점 모듈의 Offset 위치에서 bit 단위로 데이터 출력
     *
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param uValue 출력 값 (0: LOW, 1: HIGH)
     *
     * @details
     * lOffset
     *    범위: 0 ~ (전체 출력 접점 - 1)
     *    출력 접점 최대 개수: 전체 출력 접점 개수
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoWriteOutport(int lOffset, uint uValue);

    /**
     * @brief 전체 출력 접점 모듈의 Offset 위치에서 bit 단위로 데이터 출력
     *
	 * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param uValue 출력 값 (0: LOW, 1: HIGH)
     *
     * @details
     * lOffset
     *    범위: 0 ~ (출력 접점 - 1)
     *    출력 접점 최대 개수: 2160
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoWriteOutportBit(int lModuleNo, int lOffset, uint uValue);

    /**
     * @brief 전체 출력 접점 모듈의 Offset 위치에서 byte 단위로 데이터 출력
     *
	 * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param uValue 출력 값 (0x00 ~ 0xFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @details
     * lOffset
     *    범위: 0 ~ ((출력 접점 / 8) - 1)
     *    출력 접점 최대 개수: 2160
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoWriteOutportByte(int lModuleNo, int lOffset, uint uValue);

    /**
     * @brief 전체 출력 접점 모듈의 Offset 위치에서 word 단위로 데이터 출력
     *
	 * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param uValue 출력 값 (0x0000 ~ 0xFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @details
     * lOffset
     *    범위: 0 ~ ((출력 접점 / 16) - 1)
     *    출력 접점 최대 개수: 2160
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoWriteOutportWord(int lModuleNo, int lOffset, uint uValue);

    /**
     * @brief 전체 출력 접점 모듈의 Offset 위치에서 double word 단위로 데이터 출력
     *
	 * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param uValue 출력 값 (0x00000000 ~ 0xFFFFFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @details
     * lOffset
     *    범위: 0 ~ ((출력 접점 / 32) - 1)
     *    출력 접점 최대 개수: 2160
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoWriteOutportDword(int lModuleNo, int lOffset, uint uValue);

    // 출력 포트 읽기

    /**
     * @brief 전체 출력 접점 모듈의 Offset 위치에서 bit 단위로 데이터 확인
     *
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param uValue 출력 값 저장 (0: LOW, 1: HIGH)
     *
     * @details
     * lOffset
     *    범위: 0 ~ (전체 출력 접점 - 1)
     *    출력 접점 최대 개수: 전체 출력 접점 개수
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoReadOutport(int lOffset, ref uint upValue);

    /**
     * @brief 전체 출력 접점 모듈의 Offset 위치에서 bit 단위로 데이터 확인
     *
	 * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param uValue 출력 값 저장 (0: LOW, 1: HIGH)
     *
     * @details
     * lOffset
     *    범위: 0 ~ (출력 접점 - 1)
     *    출력 접점 최대 개수: 2160
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoReadOutportBit(int lModuleNo, int lOffset, ref uint upValue);

    /**
     * @brief 전체 출력 접점 모듈의 Offset 위치에서 byte 단위로 데이터 확인
     *
	 * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param uValue 출력 값 저장 (0x00 ~ 0xFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @details
     * lOffset
     *    범위: 0 ~ ((출력 접점 / 8) - 1)
     *    출력 접점 최대 개수: 2160
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoReadOutportByte(int lModuleNo, int lOffset, ref uint upValue);

    /**
     * @brief 전체 출력 접점 모듈의 Offset 위치에서 word 단위로 데이터 확인
     *
	 * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param uValue 출력 값 확인 (0x0000 ~ 0xFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @details
     * lOffset
     *    범위: 0 ~ ((출력 접점 / 16) - 1)
     *    출력 접점 최대 개수: 2160
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoReadOutportWord(int lModuleNo, int lOffset, ref uint upValue);

    /**
     * @brief 전체 출력 접점 모듈의 Offset 위치에서 double word 단위로 데이터 확인
     *
	 * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param uValue 출력 값 확인 (0x00000000 ~ 0xFFFFFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @details
     * lOffset
     *    범위: 0 ~ ((출력 접점 / 32) - 1)
     *    출력 접점 최대 개수: 2160
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoReadOutportDword(int lModuleNo, int lOffset, ref uint upValue);

    // 입력 포트 읽기

    /**
     * @brief 전체 입력 접점 모듈의 Offset 위치에서 bit 단위로 데이터 확인
     *
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param upValue 입력 값 저장 (0: LOW, 1: HIGH)
     *
     * @details
     * lOffset
     *    범위: 0 ~ (전체 입력 접점 - 1)
     *    입력 접점 최대 개수: 전체 입력 접점 개수
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiReadInport(int lOffset, ref uint upValue);

    /**
     * @brief 전체 입력 접점 모듈의 Offset 위치에서 bit 단위로 데이터 확인
     *
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param upValue 입력 값 저장 (0: LOW, 1: HIGH)
     *
     * @details
     * lOffset
     *    범위: 0 ~ (입력 접점 - 1)
     *    입력 접점 최대 개수: 2160
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiReadInportBit(int lModuleNo, int lOffset, ref uint upValue);

    /**
     * @brief 전체 입력 접점 모듈의 Offset 위치에서 byte 단위로 데이터 확인
     *
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param upValue 입력 값 저장 (0x00 ~ 0xFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @details
     * lOffset
     *    범위: 0 ~ ((입력 접점 / 8) - 1)
     *    입력 접점 최대 개수: 2160
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiReadInportByte(int lModuleNo, int lOffset, ref uint upValue);

    /**
     * @brief 전체 입력 접점 모듈의 Offset 위치에서 word 단위로 데이터 확인
     *
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param upValue 입력 값 저장 (0x0000 ~ 0xFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @details
     * lOffset
     *    범위: 0 ~ ((입력 접점 / 16) - 1)
     *    입력 접점 최대 개수: 2160
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiReadInportWord(int lModuleNo, int lOffset, ref uint upValue);

    /**
     * @brief 전체 입력 접점 모듈의 Offset 위치에서 double word 단위로 데이터 확인
     *
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param upValue 입력 값 저장 (0x00000000 ~ 0xFFFFFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @details
     * lOffset
     *    범위: 0 ~ ((입력 접점 / 32) - 1)
     *    입력 접점 최대 개수: 2160
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiReadInportDword(int lModuleNo, int lOffset, ref uint upValue);

    // MLII 용 M-Systems DIO(R7 series) 전용 함수
	
    /**
     * @brief 지정한 모듈에 장착된 입력 접점용 확장 기능 모듈의 Offset 위치에서 bit 단위로 데이터 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치 (0 ~ 15)
     * @param upValue 입력 값 저장 (0: LOW, 1: HIGH)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdReadExtInportBit(int lModuleNo, int lOffset, ref uint upValue);

    /**
     * @brief 지정한 모듈에 장착된 입력 접점용 확장 기능 모듈의 Offset 위치에서 byte 단위로 데이터 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치(0~1)
     * @param upValue 입력 값 저장 (0x00 ~ 0xFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdReadExtInportByte(int lModuleNo, int lOffset, ref uint upValue);

    /**
     * @brief 지정한 모듈에 장착된 입력 접점용 확장 기능 모듈의 Offset 위치에서 word 단위로 데이터 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치(0)
     * @param upValue 입력 값 저장 (0x0000 ~ 0xFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdReadExtInportWord(int lModuleNo, int lOffset, ref uint upValue);

    /**
     * @brief 지정한 모듈에 장착된 입력 접점용 확장 기능 모듈의 Offset 위치에서 dword 단위로 데이터 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치(0)
     * @param upValue 입력 값 저장 (0x00000000 ~ 0xFFFFFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdReadExtInportDword(int lModuleNo, int lOffset, ref uint upValue);

    /**
     * @brief 지정한 모듈에 장착된 출력 접점용 확장 기능 모듈의 Offset 위치에서 bit 단위로 데이터 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치(0~15)
     * @param upValue 출력 값 저장 (0: LOW, 1: HIGH)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdReadExtOutportBit(int lModuleNo, int lOffset, ref uint upValue);

    /**
     * @brief 지정한 모듈에 장착된 출력 접점용 확장 기능 모듈의 Offset 위치에서 byte 단위로 데이터 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치(0~15)
     * @param upValue 출력 값 저장 (0x00 ~ 0xFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdReadExtOutportByte(int lModuleNo, int lOffset, ref uint upValue);

    /**
     * @brief 지정한 모듈에 장착된 출력 접점용 확장 기능 모듈의 Offset 위치에서 word 단위로 데이터 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치(0~15)
     * @param upValue 출력 값 저장 (0x0000 ~ 0xFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdReadExtOutportWord(int lModuleNo, int lOffset, ref uint upValue);

    /**
     * @brief 지정한 모듈에 장착된 출력 접점용 확장 기능 모듈의 Offset 위치에서 dword 단위로 데이터 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치(0~15)
     * @param upValue 출력 값 저장 (0x00000000 ~ 0xFFFFFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdReadExtOutportDword(int lModuleNo, int lOffset, ref uint upValue);

    /**
     * @brief 지정한 모듈에 장착된 출력 접점용 확장 기능 모듈의 Offset 위치에서 bit 단위로 데이터 출력
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param upValue 출력 값 (0: LOW, 1: HIGH)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdWriteExtOutportBit(int lModuleNo, int lOffset, uint uValue);

    /**
     * @brief 지정한 모듈에 장착된 출력 접점용 확장 기능 모듈의 Offset 위치에서 byte 단위로 데이터 출력
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param upValue 출력 값 (0x00 ~ 0xFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdWriteExtOutportByte(int lModuleNo, int lOffset, uint uValue);

    /**
     * @brief 지정한 모듈에 장착된 출력 접점용 확장 기능 모듈의 Offset 위치에서 word 단위로 데이터 출력
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param upValue 출력 값 (0x0000 ~ 0xFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdWriteExtOutportWord(int lModuleNo, int lOffset, uint uValue);

    /**
     * @brief 지정한 모듈에 장착된 출력 접점용 확장 기능 모듈의 Offset 위치에서 dword 단위로 데이터 출력
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param upValue 출력 값 (0x00000000 ~ 0xFFFFFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdWriteExtOutportDword(int lModuleNo, int lOffset, uint uValue);

    /**
     * @brief 지정한 모듈에 장착된 입/출력 접점용 확장 기능 모듈의 Offset 위치에서 bit 단위로 데이터 레벨을 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치(0~15)
     * @param upValue 레벨 설정 값 (0: LOW, 1: HIGH)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdLevelSetExtportBit(int lModuleNo, int lOffset, uint uLevel);

    /**
     * @brief 지정한 모듈에 장착된 입/출력 접점용 확장 기능 모듈의 Offset 위치에서 byte 단위로 데이터 레벨 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치(0~1)
     * @param upValue 레벨 설정 값 (0x00 ~ 0xFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdLevelSetExtportByte(int lModuleNo, int lOffset, uint uLevel);

    /**
     * @brief 지정한 모듈에 장착된 입/출력 접점용 확장 기능 모듈의 Offset 위치에서 word 단위로 데이터 레벨 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치(0)
     * @param upValue 레벨 설정 값 (0x0000 ~ 0xFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdLevelSetExtportWord(int lModuleNo, int lOffset, uint uLevel);

    /**
     * @brief 지정한 모듈에 장착된 입/출력 접점용 확장 기능 모듈의 Offset 위치에서 dword 단위로 데이터 레벨 설정
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치(0)
     * @param upValue 레벨 설정 값 (0x00000000 ~ 0xFFFFFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdLevelSetExtportDword(int lModuleNo, int lOffset, uint uLevel);

    /**
     * @brief 지정한 모듈에 장착된 입/출력 접점용 확장 기능 모듈의 Offset 위치에서 bit 단위로 데이터 레벨 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치(0~15)
     * @param upValue 레벨 설정 값 저장 (0: LOW, 1: HIGH)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdLevelGetExtportBit(int lModuleNo, int lOffset, ref uint upLevel);

    /**
     * @brief 지정한 모듈에 장착된 입/출력 접점용 확장 기능 모듈의 Offset 위치에서 byte 단위로 데이터 레벨 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치(0~1)
     * @param upValue 레벨 설정 값 저장 (0x00 ~ 0xFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdLevelGetExtportByte(int lModuleNo, int lOffset, ref uint upLevel);

    /**
     * @brief 지정한 모듈에 장착된 입/출력 접점용 확장 기능 모듈의 Offset 위치에서 word 단위로 데이터 레벨 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치(0)
     * @param upValue 레벨 설정 값 (0x0000 ~ 0xFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdLevelGetExtportWord(int lModuleNo, int lOffset, ref uint upLevel);

    /**
     * @brief 지정한 모듈에 장착된 입/출력 접점용 확장 기능 모듈의 Offset 위치에서 dword 단위로 데이터 레벨 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치(0)
     * @param upValue 레벨 설정 값 (0x00000000 ~ 0xFFFFFFFF: '1'로 설정 된 비트는 HIGH, '0'으로 설정 된 비트는 LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdLevelGetExtportDword(int lModuleNo, int lOffset, ref uint upLevel);
    
	// 고급 함수
	/**
     * @brief 지정한 입력 접점 모듈의 Offset 위치에서 신호가 Off에서 On으로 바뀌었는지 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param upValue 변경 여부 값 (0: FALSE, 1: TRUE)
     *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiIsPulseOn(int lModuleNo, int lOffset, ref uint upValue);
    
    /**
     * @brief 지정한 입력 접점 모듈의 Offset 위치에서 신호가 On에서 Off으로 바뀌었는지 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param upValue 변경 여부 값 (0: FALSE, 1: TRUE)
     *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiIsPulseOff(int lModuleNo, int lOffset, ref uint upValue);
    
    /**
     * @brief 지정한 입력 접점 모듈의 Offset 위치에서 신호가 count 만큼 호출될 동안 On 상태로 유지하는지 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param lCount 호출 횟수 (0 ~ 0x7FFFFFFF(2147483647))
     * @param upValue On 상태 유지 여부 (0: FLASE, 1: TRUE)
     * @param lStart On 신호 유지 확인 시작 (최초 호출: 1, 반복 호출: 0)
     *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiIsOn(int lModuleNo, int lOffset, int lCount, ref uint upValue, int lStart);
    
    /**
     * @brief 지정한 입력 접점 모듈의 Offset 위치에서 신호가 count 만큼 호출될 동안 Off 상태로 유지하는지 확인
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 입력 접점에 대한 Offset 위치
     * @param lCount 호출 횟수 (0 ~ 0x7FFFFFFF(2147483647))
     * @param upValue Off 상태 유지 여부 (0: FLASE, 1: TRUE)
     * @param lStart Off 신호 유지 확인 시작 (최초 호출: 1, 반복 호출: 0)
     *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiIsOff(int lModuleNo, int lOffset, int lCount, ref uint upValue, int lStart);
    
    /**
     * @brief 지정한 출력 접점 모듈의 Offset 위치에서 설정한 mSec동안 On을 유지하다가 Off 시킴
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param lmSec 유지 시간 (1 ~ 30000 ms)
     *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoOutPulseOn(int lModuleNo, int lOffset, int lmSec);
    
    /**
     * @brief 지정한 출력 접점 모듈의 Offset 위치에서 설정한 mSec동안 Off를 유지하다가 On 시킴
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param lmSec 유지 시간 (1 ~ 30000 ms)
     *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoOutPulseOff(int lModuleNo, int lOffset, int lmSec);
    
    /**
     * @brief 지정한 출력 접점 모듈의 Offset 위치에서 설정한 횟수, 설정한 간격으로 토글한 후 원래의 출력 상태 유지
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param lInitState 최초 시작 신호 상태 설정(Off: 0, On: 1)
     * @param lmSecOn 토글 간격 중 On Time 시간 간격 (1 ~ 30000 ms)
     * @param lmSecOff Off 토글 간격 중 Off Time 시간 간격 (1 ~ 30000 ms)
     * @param lCount 토글 횟수 (-1: 무한 토글)
     *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoToggleStart(int lModuleNo, int lOffset, int lInitState, int lmSecOn, int lmSecOff, int lCount);
    
    /**
     * @brief 지정한 출력 접점 모듈의 Offset 위치에서 토글중인 출력을 설정한 신호 상태로 정지
     *
     * @param lModuleNo 모듈 번호
     * @param lOffset 출력 접점에 대한 Offset 위치
     * @param uOnOff 출력 접점 값 (Off: 0, On: 1)
     *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoToggleStop(int lModuleNo, int lOffset, uint uOnOff);

    /**
     * @brief 지정한 출력 모듈의 Network이 끊어 졌을 경우 출력 상태를 Byte 단위로 설정 (SIII / ML3 / ECAT 전용)
     *
     * @param lModuleNo 모듈 번호 (분산형 슬레이브 제품만 지원)
     * @param dwSize 설정 할 Byte 수 (ex: RTEX-DB32: 2, RTEX-DO32: 4)
     * @param dwaSetValue 설정 할 변수 값 (Default: Network 끊어 지기 전 상태 유지)
     *
	 * @details
	 * dwaSetValue (설정 값)
	 *    0: Network 끊어 지기 전 상태 유지
	 *    1: On
	 *    2: Off
	 *    3: User Value (Default: Off, AxdoSetNetworkErrorUserValue() 함수로 변경 가능)
	 *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoSetNetworkErrorAct(int lModuleNo, uint dwSize, ref uint dwaSetValue);

    /**
     * @brief 지정한 출력 모듈의 Network이 끊어 졌을 경우 출력 값을 사용자가 정의한 출력값을 Byte 단위로 설정 (SIII / ML3 / ECAT 전용)
     *
     * @param lModuleNo 모듈 번호 (분산형 슬레이브 제품만 지원 함)
     * @param dwOffset 출력 접점에 대한 Offset 위치, BYTE 단위로 증가 (지정범위:0, 1, 2, 3)
     * @param dwValue 출력 접점 값 (0x00 ~ 0xFFh)
     *
	 * @details AxdoSetNetworkErrorAct() 함수로 해당 Offset에 대해서 User Value로 설정되어야 동작 함
	 *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoSetNetworkErrorUserValue(int lModuleNo, uint dwOffset, uint dwValue);

    /**
     * @brief Simple IO Type 의 Input/Output 접점 개수를 설정
     *
     * @param lModuleNo 모듈 번호
     * @param dwInputNum 설정할 Input 접점 개수
     * @param dwOutputNum 설정할 Output 접점 개수
	 *
	 * @details PCI-R1604-MLII와 연결된 Simple IO Type 출력 Slave Node에서 사용되는 함수
     *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdSetContactNum(int lModuleNo, uint dwInputNum, uint dwOutputNum);

    /**
     * @brief Simple IO Type 의 Input/Output 접점 개수를 설정 확인
     *
     * @param lModuleNo 모듈 번호
     * @param dwInputNum 설정할 Input 접점 개수 저장
     * @param dwOutputNum 설정할 Output 접점 개수 저장
	 *
	 * @details PCI-R1604-MLII와 연결된 Simple IO Type 출력 Slave Node에서 사용되는 함수
     *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdGetContactNum(int lModuleNo, ref uint dwpInputNum, ref uint dwpOutputNum);
    
    // EtherCAT 전용 함수

    /**
     * @brief 지정한 출력 모듈의 Bit Offset 위치에서 Bit Length 단위로 데이터 출력
     *
     * @param lModuleNo 모듈 번호
     * @param dwBitOffset 출력 접점에 대한 Bit Offset 위치
     * @param dwDataBitLength 출력할 Data의 Bit Length
     * @param pbyData 출력할 Data의 포인터 ('1'로 설정 된 비트: HIGH, '0'으로 설정 된 비트: LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoWriteOutportByBitOffset(int lModuleNo, uint dwBitOffset, uint dwDataBitLength, byte[] pbyData);
    
    /**
     * @brief 지정한 출력 모듈의 Bit Offset 위치에서 Bit Length 단위로 데이터 확인
     *
     * @param lModuleNo 모듈 번호
     * @param dwBitOffset 입력 접점에 대한 Bit Offset 위치
     * @param dwDataBitLength 입력 받을 Data의 Bit Length
     * @param pbyData 입력 받을 Data 포인터 ('1'로 설정 된 비트: HIGH, '0'으로 설정 된 비트: LOW)
     *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdiReadInportByBitOffset(int lModuleNo, uint dwBitOffset, uint dwDataBitLength, byte[] pbyData);
    
    /**
     * @brief 지정한 출력 모듈의 Bit Offset 위치에서 Bit Length 단위로 데이터 읽기
     *
     * @param lModuleNo 모듈 번호
     * @param dwBitOffset 출력 접점에 대한 Bit Offset 위치
     * @param dwDataBitLength 입력 받을 출력 접점들의 총 bit 길이
     * @param pbyData 출력 접점 값 (dwDataBitLength 만큼 읽음)
     *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoReadOutportByBitOffset(int lModuleNo, uint dwBitOffset, uint dwDataBitLength, byte[] pbyData);

    /**
     * @brief 지정한 입출력 데이타를 다른 모듈과 Link
     *
     * @param lLinkNo 링크 번호 (0~31)
     * @param lDstModuleNo Link될 출력 포트의 모듈 번호
     * @param uDstBitOffset Link될 출력 포트의 시작 Bit Offset (Process Image 탭의 Bit Offset 열 참조)
     * @param lSrcModuleNo Link시킬 입출력 포트를 포함한 모듈 번호
     * @param uSrcBitOffset Link시킬 입출력 포트의 시작 Bit Offset (Process Image 탭의 Bit Offset 열 참조)
     * @param uSrcDataBitLength Link시킬 데이타 Length (Process Image 탭의 Bit Offset 열 참조)
     * @param uSrcPortType 소스 포트의 타입 (0: 출력 포트, 1: 입력 포트)
     *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoLinkSetOutport(int lLinkNo, int lDstModuleNo, uint uDstBitOffset, int lSrcModuleNo, uint uSrcBitOffset, uint uSrcDataBitLength, uint uSrcPortType);
	
	/**
     * @brief 지정한 입출력 데이타를 다른 모듈과 Link 정보 확인
     *
     * @param lLinkNo 링크 번호 (0~31)
     * @param lDstModuleNo Link될 출력 포트의 모듈 번호 확인
     * @param uDstBitOffset Link될 출력 포트의 시작 Bit Offset 확인 (Process Image 탭의 Bit Offset 열 참조)
     * @param lSrcModuleNo Link시킬 입출력 포트를 포함한 모듈 번호 확인
     * @param uSrcBitOffset Link시킬 입출력 포트의 시작 Bit Offset 확인 (Process Image 탭의 Bit Offset 열 참조)
     * @param uSrcDataBitLength Link시킬 데이타 Length 확인 (Process Image 탭의 Bit Offset 열 참조)
     * @param uSrcPortType 소스 포트의 타입 확인 (0: 출력 포트, 1: 입력 포트)
     *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoLinkGetOutport(int lLinkNo, ref int lpDstModuleNo, ref uint upDstBitOffset, ref int lpSrcModuleNo, ref uint upSrcBitOffset, ref uint upSrcDataBitLength, ref uint upSrcPortType);
	
	/**
     * @brief 다른 모듈과 Link Reset
     *
     * @param lLinkNo 링크 번호 (0~31)
     *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoLinkResetOutport(int lLinkNo);

    /**
     * @brief 지정한 입출력 접점 조건 및 출력 방식에 따라 다른 DO 모듈의 출력 접점 제어
     *
     * @param lBoardNo Source 모듈과 Destination 모듈이 존재하는 Board의 번호
     * @param lInterLockNo 인터락 번호 (0~31)
     * @param lDstModuleNo InterLock 기능을 사용할 출력 포트의 모듈 번호
     * @param uDstBitOffset InterLock 기능을 사용할 출력 포트의 시작 Bit Offset
     * @param lSrcModuleNo InterLock 기능의 조건이 되는 입출력 포트를 포함한 모듈 번호
     * @param uSrcBitOffset InterLock 기능의 조건이 되는 입출력 포트의 시작 Bit Offset
     * @param uSrcPortType Source 모듈에서 사용할 입력 or 출력 접점 결정
     * @param uSrcCondition uSrcCondition에 설정한 값에 따라 InterLock 기능의 트리거 조건 결정
     * @param uOutputMode InterLock 기능이 트리거 되었을 때 출력 대상 접점의 출력 형태 결정
     * @param uOutputModeData InterLock 기능이 트리거 되었을 때 uOutputMode 별 부가 기능 값 입력 (uOutputMode에 따라 각각의 값이 가지는 기능이 상이함)
     *
	 * @details
	 * uSrcPortType
	 *    0: INTERLOCK_PORT_TYPE_OUTPUT
	 *    1: INTERLOCK_PORT_TYPE_INPUT
	 * uSrcCondition
	 *    0: INTERLOCK_CONDITION_LEVEL_LOW
	 *    1: INTERLOCK_CONDITION_LEVEL_HIGH
	 *    2: INTERLOCK_CONDITION_EDGE_FALLING
	 *    3: INTERLOCK_CONDITION_EDGE_RISING
	 * uOutputMode
	 *    0: INTERLOCK_OUTPUT_MODE_OFF
	 *    1: INTERLOCK_OUTPUT_MODE_ON
	 *    2: INTERLOCK_OUTPUT_MODE_TOGGLE
	 * uOutputModeData
	 *    INTERLOCK_OUTPUT_MODE_OFF: 트리거 후 실제 출력이 되기까지의 Delay(msec)
	 *    INTERLOCK_OUTPUT_MODE_ON: 트리거 후 실제 출력이 되기까지의 Delay(msec)
	 *    INTERLOCK_OUTPUT_MODE_TOGGLE: 트리거 후 실제 출력이 되기까지의 Delay(msec)
	 *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoInterLockSetOutport(int lBoardNo, int lInterLockNo, int lDstModuleNo, uint uDstBitOffset, int lSrcModuleNo, uint uSrcBitOffset, uint uSrcPortType, uint uSrcCondition, uint uOutputMode, uint uOutputModeData);
	
	/**
     * @brief 지정한 입출력 접점 조건 및 출력 방식에 따라 다른 DO 모듈의 출력 접점 정보 확인
     *
     * @param lBoardNo 보드 번호
     * @param lInterLockNo 인터락 번호 (0~31)
     * @param lpDstModuleNo 대상 모듈 번호 저장
     * @param upDstBitOffset 대상 모듈의 비트 오프셋 저장
     * @param lpSrcModuleNo 소스 모듈 번호 저장
     * @param upSrcBitOffset 소스 모듈의 비트 오프셋 저장
     * @param upSrcPortType 소스 포트의 타입 저장
     * @param upSrcCondition 소스 조건 저장
     * @param upOutputMode 출력 모드 저장
     * @param upOutputModeData 출력 모드 데이터 저장
     *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoInterLockGetOutport(int lBoardNo, int lInterLockNo, ref int lpDstModuleNo, ref uint upDstBitOffset, ref int lpSrcModuleNo, ref uint upSrcBitOffset, ref uint upSrcPortType, ref uint upSrcCondition, ref uint upOutputMode, ref uint upOutputModeData);
	
	/**
     * @brief 지정된 인터락(Interlock)이 활성화 여부 확인
     *
     * @param lBoardNo 보드 번호
     * @param lInterLockNo 인터락 번호 (0~31)
     * @param upEnabled 활성화 여부 저장
     *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoInterLockIsEnabled(int lBoardNo, int lInterLockNo, ref uint upEnabled);
	
	/**
     * @brief 지정된 인터락(Interlock) 정보 초기화
     *
     * @param lBoardNo 보드 번호
     * @param lInterLockNo 인터락 번호 (0~31)
     *
     * @return 함수 호출 성공 시 0을 반환하고, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdoInterLockResetOutport(int lBoardNo, int lInterLockNo);


}
