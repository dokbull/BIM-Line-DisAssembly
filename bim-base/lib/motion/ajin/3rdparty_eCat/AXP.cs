/**
 * @file AXP.cs
 * 
 * @brief 아진엑스텍 Macro 라이브러리 헤더 파일
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

public class CAXP
{
    /**
     * @brief 지정된 매크로에 노드 등록 시작
     * 
     * @param nMacroNo 노드 등록을 시작 할 매크로 번호 (0~95)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxpMacroBeginNode(int nMacroNo);
	
    /**
     * @brief 지정된 매크로에 노드 등록 종료
     * 
     * @param nMacroNo 노드 등록을 종료 할 매크로 번호 (0~95)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxpMacroEndNode(int nMacroNo);    
	
    /**
     * @brief 지정된 매크로에 등록된 노드들과 구동 정보를 모두 삭제
     * 
     * @param nMacroNo 등록된 노드 및 구동 정보를 삭제할 매크로 번호 지정 (0~95)
     * 
	 * @details
	 * nMacroNo
	 *    -1: 모든 매크로에 등록된 노드들을 모두 삭제
	 *    0~95: 지정한 매크로에 등록된 노드들을 모두 삭제
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxpMacroWriteClear(int nMacroNo);
	    
	/**
     * @brief 지정된 매크로에 노드 등록 (EtherCAT S/W Type 전용 함수)
     * 
     * @param nMacroNo 노드 등록 매크로 번호 (0~95)
	 * @param uFunction 매크로에 등록할 함수 지정 (Macro Function 8개)
	 * @param dpArg[21] 지정한 Function에 대한 인자 배열
	 *
	 * @note 주의 사항: 배열의 개수는 반드시 AXP_MAX_MACRO_SET_DATA(21)개로 해야 됨
	 *
	 * @details
	 * uFunction
     *    [0] MACRO_FUNC_CALL: 다른 매크로 호출 (호출된 매크로가 종료되거나 RETURN 하면 다시 돌아 옴)
     *    [1] MACRO_FUNC_JUMP: 다른 매크로나 노드로 점프 (JUMP 위치로 되돌아오지 않음)
     *    [2] MACRO_FUNC_RETURN: 호출한 매크로로 되돌아감 (호출된 매크로가 없으면 무시됨)
     *    [3] MACRO_FUNC_REPEAT: 현재 위치에서 지정한 노드 사이를 설정한 횟수만큼 반복 구동
     *    [4] MACRO_FUNC_SET_OUTPUT: Digital Output, Analog Output 출력
     *    [5] MACRO_FUNC_WAIT: 지정한 시간만큼 대기함
     *    [6] MACRO_FUNC_STOP: 매크로의 구동을 정지
     *    [7] MACRO_FUNC_PAUSE: 매크로 구동을 일시 정지
	 * dpArg[21]
     *    [0] MACRO_FUNC_CALL
     *        dArg[0] Macro 번호  
     *    [1] MACRO_FUNC_JUMP
     *        dArg[0] Jump Type: [0] MACRO_JUMP_MACRO, [1] MACRO_JUMP_NODE
     *        dArg[1] 매크로 번호 or 노드 번호 
     *    [2] MACRO_FUNC_RETURN: 인자 없음
     *    [3] MACRO_FUNC_REPEAT: 반복구동 지정
     *        dArg[0] 반복 할 회수
     *        dArg[1] 현재 위치에서 반복 할 노드 번호(현재 노드의 번호보다 낮은 번호여야 됨)
     *    [4] MACRO_FUNC_SET_OUTPUT
     *        dArg[0] Output Type: [0] MACRO_OUTPUT_DIGITAL, [1] MACRO_OUTPUT_ANALOG, [2] MACRO_OUTPUT_DIGITAL_PDO
     *        dArg[1] DO 모듈 번호 or AO 채널 번호
     *        dArg[2] Data Type
     *            [0] MACRO_OUTPUT_DIGITAL인 경우:       
	 *                [0] MACRO_DATA_BIT 
	 *                [1] MACRO_DATA_BYTE
	 *                [2] MACRO_DATA_WORD
	 *                [3] MACRO_DATA_DWORD
	 *                [7] MACRO_DATA_BYTE_ARR
	 *                [8] EC_MACRO_DATA_WORD_ARR
	 *                [9] EC_MACRO_DATA_DWORD_ARR
     *            [1] MACRO_OUTPUT_ANALOG인 경우:
	 *                [5] MACRO_DATA_VOLTAGE
	 *                [6] MACRO_DATA_DIGIT
	 *                [10] MACRO_DATA_VOLTAGE_ARR
	 *                [11] MACRO_DATA_DIGIT_ARR
     *            [2] MACRO_OUTPUT_DIGITAL_PDO인 경우
	 *                [0] MACRO_DATA_BIT
	 *                [1] MACRO_DATA_BYTE
	 *                [2] MACRO_DATA_WORD
	 *                [3] MACRO_DATA_DWORD
	 *                [7] MACRO_DATA_BYTE_ARR
	 *                [4] MACRO_DATA_REAL
	 *                [8] EC_MACRO_DATA_WORD_ARR
	 *                [9] EC_MACRO_DATA_DWORD_ARR
     *        dArg[3] Offset
     *            [0] MACRO_DATA_BIT: 시작 Bit Offset, (0) 첫 번쨰 Bit, (1) 두번째 Bit
     *            [1] MACRO_DATA_BYTE: 시작 Byte Offset, (0) 첫 번째 Byte, (1) 두번째 Byte
     *            [...]
     *            [4] MACRO_DATA_REAL: Real 데이타가 시작되는 Bit Offset
     *            [5] MACRO_DATA_VOLTAGE: 사용 안함
     *            [6] MACRO_DATA_DIGIT: 사용 안함
     *            [...]
     *            [7] MACRO_DATA_BYTE_ARR: 시작 Byte Offset, (0) 첫 번째 Byte, (1) 두번째 Byte    
     *            [...]
     *            [10]MACRO_DATA_VOLTAGE_ARR: 해당 모듈을 기준으로 시작 Channel Offset (16채널 모듈의 경우 0~ 15)
     *            [...]
     *        dArg[4] 출력 값 Or Size
     *            [0] MACRO_DATA_BIT: 출력 할 Bit값
     *            [1] MACRO_DATA_BYTE: 출력 할 Byte 값
     *            [...]
     *            [4] MACRO_DATA_REAL: 출력 할 Real 값
     *            [5] MACRO_DATA_VOLTAGE: 출력 할 Voltage 값
     *            [6] MACRO_DATA_DIGIT: 출력 할 Digit 값
     *            [7] MACRO_DATA_BYTE_ARR: Byte 배열 개수 (최대 16개)
     *            [...]
     *            [10] MACRO_DATA_VOLTAGE_ARR: 출력 할 Voltage 배열 개수 (최대 16개)
     *            [...]
     *        dArg[5] ~ dArg[20] Array Type에 따른 출력 값 (MACRO_DATA_XXXX_ARR Type일 경우)
     *            지정한 Type(최대 16개) 값들을 여러개의 출력 모듈에 상관 없이 한번에 출력함
     *    [5] MACRO_FUNC_WAIT
     *        dArg[0] 대기 시간(ms): 지정한 ms 동안 대기함
     *    [6] MACRO_FUNC_STOP
     *        주의점 : Watchdog 으로 등록 된 Macro 의 경우는 MACRO_FUNC_STOP 으로 STOP 시킬 수 없음
     *        dArg[0] 매크로 번호: [-1]해당 매크로 및 Watchdog 으로 등록된 매크로를 제외한 모든 매크로 구동 정지, [0 ~ (AXP_MAX_MACRO_SIZE - 1)] 
     *        dArg[1] 정지 모드: [0] MACRO_QUICK_STOP, [1] MACRO_SLOW_STOP
     *    [7] MACRO_FUNC_PAUSE: 인자 없음
	 * 
	 * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.     
	 */
	[DllImport("AXL.dll")] public static extern uint AxpMacroAddNode(int nMacroNo, uint uFunction, double[] dpArg);
	
	/**
     * @brief 지정한 매크로에 등록된 노드의 설정값 변경
     * 
     * @param nMacroNo 매크로 번호 (0~95)
	 * @param nNodeNo 노드 번호 (0~63)
	 * @param npErrorCode Modify 결과에 대한 에러 코드 반환
	 * @param dpArg[21] 지정한 노드의 Function에 대한 설정값 (자세한 내용은 AxpMacroAddNode 함수 참조)
     * 
	 * @details
	 * Modify는 이미 추가된 Node에 대해서만 변경 가능
	 * 노드 변경은 매크로가 MACRO_STATUS_RUN 상태가 아닐 때 가능
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxpMacroModifyNode(int nMacroNo, int nNodeNo, ref int npErrorCode, double[] dpArg);
	
    /**
     * @brief 지정된 매크로의 노드에 설정된 값 확인 (반환값은 AxpMacroAddNode 함수로 설정한 값)
     * 
     * @param nMacroNo 매크로 번호 (0~95)
	 * @param nNodeNo 노드 번호 (0~63)
	 * @param upFunction 매크로에 등록된 함수 저장
	 * @param dpArg[21] 지정한 Function에 대한 인자 배열 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxpMacroGetNode(int nMacroNo, int nNodeNo, ref uint upFunction, double[] dpArg);
	
    /**
     * @brief 지정된 매크로에 등록된 노드들을 검사한 후 에러가 발생한 노드 위치와 에러 코드 반환
     * 
     * @param nMacroNo 매크로 번호 (0~95)
	 * @param npErrorNodeNo 에러 발생 노트 번호 저장
	 * @param upErrorCode 에러 발생한 노드에 대한 에러 코드 저장
     * 
	 * @details 주의: 반드시 이 함수로 등록된 노드들에 대해 유효성 검사를 완료해야 매크로 구동을 할 수 있음
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxpMacroCheckNodeAll(int nMacroNo, ref int npErrorNodeNo, ref uint upErrorCode);

	/**
     * @brief 지정된 매크로의 체크 결과 확인
     * 
     * @param nMacroNo 매크로 번호 (0~95)
	 * @param bIsChecked 체크 진행여부 저장
	 * @param upErrorNodeNo 에러가 발생한 노드 번호 저장
	 * @param upErrorCode 에러가 발생한 노드에 대한 에러 코드 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxpMacroGetCheckResult(int nMacroNo, ref bool bIsChecked, ref uint upErrorNodeNo, ref uint upErrorCode);

    /**
     * @brief 지정된 매크로 구동 시작
     * 
     * @param nMacroNo 매크로 번호 (0~95)
	 * @param uCondition 매크로 시작 조건 설정
	 * @param bLockResource 매크로에 등록된 디지털 입력 모듈, 아날로그 출력 채널들에 대해 일반 I/O 제어 함수로 제어 금지 시킬지 여부 설정
	 * @param nRepeatCount 매크로 전체 구동의 반복 회수 설정
     * 
	 * @details
	 * uCondition
	 *    [0] MACRO_START_READY       : 대기 상태로 구동
	 *    [1] MACRO_START_IMMEDIATE   : 즉시 구동 또는 대기 상태인 매크로 구동
	 * bLockResource
	 *    [FALSE]                     : 제어허용
	 *    [TRUE]                      : 제어금지
	 * lRepeatCount (주의: 0과 1은 동일하게 1회 구동)
	 *    [-1]                        : 무한반복 구동함
	 *    [0]                         : 반복구동하지 않음
	 *    [1~]                        : 지정한 회수만큼 구동함
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxpMacroStart(int nMacroNo, uint uCondition, bool bLockResource, int nRepeatCount);

	/**
     * @brief 지정된 제품의 I/O를 Macro에서 제어하지 않도록 예외 처리
     * 
     * @param lModuleNo Module No 또는 Channel No
	 * @param uOutputType 예외처리를 할 모듈 Type 선택
	 * @param uDataType 예외처리를 할 출력 Type을 선택
	 * @param lOffset 설정할 Data Type에 따른 Offset
	 * @param uMaskValue 출력 Type에 따른 MaskValue를 지정
     *
	 * @details 예외처리 대한 자세한 param 설명은 매뉴얼 참조
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxpMacroSetException(int lModuleNo, uint uOutputType, uint uDataType, uint lOffset, uint uMaskValue);
	
	/**
     * @brief 지정된 제품의 I/O를 Macro에서 제어하지 않도록 예외 처리 설정값 확인
     * 
     * @param lModuleNo Module No 또는 Channel No
	 * @param uOutputType 예외처리를 할 모듈 Type 선택
	 * @param uDataType 예외처리를 할 출력 Type을 선택
	 * @param lOffset 설정할 Data Type에 따른 Offset
	 * @param upMaskValue 출력 Type에 따른 MaskValue 저장
     *
	 * @details 예외처리 대한 자세한 param 설명은 매뉴얼 참조
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxpMacroGetException(int lModuleNo, uint upOutputType, uint uDataType, uint lOffset, ref uint upMaskValue);

	/**
     * @brief Macro에서 제어하지 않도록 설정한 값 초기화
     *
	 * @details 예외 처리한 설정값은 EtherCAT Master가 재 시작되기 전까지 유지되므로 반드시 원하지 않을 경우 이 함수로 초기화 한 후 매크로 사용해야 함
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxpMacroResetException();    
    
	/**
     * @brief 지정된 매크로의 동작을 Skip
     * 
     * @param nMacroNo 매크로 번호 (0~95)
	 * @param uCondition 매크로 Skip 조건 설정
	 *
	 * @details
	 * uCondition
	 *    [0] MACRO_SKIP_WHEN_FUNC_WAIT : MACRO_FUNC_WAIT일 때 Skip하고 다음 노드 실행
	 *    [1] MACRO_SKIP_WAIT_TO_END    : 매크로가 Run 상태일 때 다음 노드들의 실행을 모두 Skip
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxpMacroSkip(int nMacroNo, uint uCondition);

	/**
     * @brief 지정된 매크로 일시 정지
     * 
     * @param nMacroNo 매크로 번호 (0~95)
	 * @param uCondition 매크로 일시 정지 조건 설정
	 *
	 * @details
	 * uCondition
	 *    [0] MACRO_PAUSE_IMMEDIATELY  : 즉시 정지
	 *    [1] MACRO_PAUSE_AT_FUNC_WAIT : MACRO_FUNC_WAIT문을 실행하는 시점에 일시 정지
	 *    [2] MACRO_PAUSE_AT_END       : 매크로의 마지막 노드에서 일시 정지
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxpMacroPause(int nMacroNo, uint uCondition);

	/**
     * @brief 지정된 매크로가 PAUSE 상태 일 경우 재개
     * 
     * @param nMacroNo 매크로 번호 (0~95)
	 * @param uCondition 매크로 재개 조건 설정
	 *
	 * @details
	 * uCondition
     *    [0] MACRO_RESUME_WHEN_PAUSED    : Pause 상태일 때만 재시작
     *    [1] MACRO_RESUME_RESERVED       : Pause 상태일 때 재 시작하고 Pause 상태가 아닐때는 1회에 한해 Pause를 무시
     *    [2] MACRO_RESUME_IGNORE_WAIT    : WAIT 명령에서 Pause된 상태라면 해당 Wait 명령을 무시하고 재 시작
     *    [3] MACRO_RESUME_FROM_FIRST_NODE: 해당 매크로의 첫번째 노드부터 재시작 (이 경우 Wait Count 는 초기화)
	 * 주의: 이 함수는 PAUSE 명령으로 대기중이거나 대기할 매크로에만 동작
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxpMacroResume(int nMacroNo, uint uCondition);

    /**
     * @brief 지정된 매크로의 구동 조건 반환
     * 
     * @param nMacroNo 매크로 번호 (0~95)
	 * @param upCondition 시작 조건 저장
	 * @param bpLockResource 매크로에 사용된 모듈 또는 채널들에 대해 제어 금지 여부 저장
	 * @param npRepeatCount 지정한 매크로의 전체 반복 구동 횟수 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxpMacroGetStartInfo(int nMacroNo, ref uint upCondition, ref bool bpLockResource, ref int npRepeatCount);
    
	/**
     * @brief 지정된 매크로의 구동 정지
     * 
     * @param nMacroNo 매크로 번호 (0~95) (-1: 모든 매크로의 구동 정지)
	 * @param uStopMode 구동정지 모드를 지정 (등록된 매크로에 모션 구동축이 있을 경우 의미가 있음)
	 *
	 * @details
	 * uStopMode
	 *    [0] MACRO_QUICK_STOP
	 *    [1] MACRO_SLOW_STOP
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxpMacroStop(int nMacroNo, uint uStopMode);
    
	/**
     * @brief 지정된 매크로의 구동 상태 반환
     * 
     * @param nMacroNo 매크로 번호 (0~95)
	 * @param upStatus 매크로의 구동 상태
	 * @param npRepeatCount 매크로의 전체 반복 구동 횟수 저장
	 * @param npCurNodeNo 현재 구동중인 노드 번호 저장
	 *
	 * @details
	 * upStatus
	 *    [0] MACRO_STATUS_STOP   : 매크로 정지상태
	 *    [1] MACRO_STATUS_READY  : 매크로 구동 대기 상태
	 *    [2] MACRO_STATUS_RUN    : 매크로 구동상태
	 *    [3] MACRO_STATUS_ERROR  : 매크로 에러상태(정지상태)
	 *    [4] MACRO_STATUS_PAUSE  : 매크로 일시정지 상태
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxpMacroReadRunStatus(int nMacroNo, ref uint upStatus, ref int npRepeatCount, ref int npCurNodeNo);
    
	/**
     * @brief 지정된 매크로에 등록된 노드 개수 반환
     * 
     * @param nMacroNo 매크로 번호 (0~95)
	 * @param npTotalNodeNum 지정한 매크로에 등록된 전체 노드 개수 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxpMacroGetNodeNum(int nMacroNo, ref int npTotalNodeNum);
    
	/**
     * @brief 지정된 매크로의 함수에 대한 구동 정보 반환
     * 
     * @param nMacroNo 매크로 번호 (0~95)
	 * @param uFunction 정보를 확인할 함수를 지정 (MACRO_FUNCTION)
	 * @param dpReturnData[AXP_MAX_MACRO_GET_DATA(21)]  : 지정한 함수에 대한 반환값 저장
     *
	 * @details
	 * uFunction
     *    [0] MACRO_FUNC_CALL
     *    [1] MACRO_FUNC_JUMP
     *    [2] MACRO_FUNC_RETURN
     *    [3] MACRO_FUNC_REPEAT
     *    [4] MACRO_FUNC_SET_OUTPUT
     *    [5] MACRO_FUNC_WAIT
     *    [6] MACRO_FUNC_STOP
     *    [7] MACRO_FUNC_PAUSE
	 * dpReturnData
     *    [0] MACRO_FUNC_CALL
     *        dArg[0] 최종 호출 된 CALL명령의 노드번호
     *        dArg[1] 지정한 매크로 내에서 최종호출된 CALL명령의 Index를 반환(CALL 명령들중에서의 Index)
     *        dArg[5] 해당 매크로에 포함된 마지막 CALL Function의 노드번호
     *    [1] MACRO_FUNC_JUMP
     *        dArg[0] 최종 호출된 JUMP명령의 노드번호
     *        dArg[1] 지정한 매크로 내에서 JUMP명령들 중의 Index를 반환
     *        dArg[5] 해당 매크로에 포함된 마지막 JUMP Function의 노드번호
     *    [2] MACRO_FUNC_RETURN       : 인자 없음
     *        dArg[0] 최종 호출된 RETURN명령의 노드번호
     *        dArg[1] 지정한 매크로 내에서 RETURN명령들 중의 Index를 반환
     *        dArg[5] 해당 매크로에 포함된 마지막 RETURN Function의 노드번호
     *    [3] MACRO_FUNC_REPEAT       : 반복구동 지정
     *        dArg[0] 최종 호출된 REPEAT명령의 노드번호
     *        dArg[1] 지정한 매크로 내에서 REPEAT명령들 중의 Index를 반환
     *        dArg[2] 최종 호출된 REPEAT명령의 반복구동 설정회수를 반환
     *        dArg[3] 최종 호출된 REPEAT명령의 반복구동한 회수를 반환
     *        dArg[5] 해당 매크로에 포함된 마지막 REPEAT Function의 노드번호
     *    [4] MACRO_FUNC_SET_OUTPUT
     *        dArg[0] 최종 호출된 SET_OUTPUT명령의 노드번호
     *        dArg[1] 지정한 매크로 내에서 SET_OUTPUT명령들 중의 Index를 반환
     *        dArg[2] 최종 호출된 SET_OUTPUT명령의 출력 Type
     *        dArg[3] Offset Or Data
     *          - Offset: [7]MACRO_DATA_BYTE_ARR, [8]MACRO_DATA_WORD_ARR, [9]MACRO_DATA_DWORD_ARR, [10]MACRO_DATA_VOLTAGE_ARR, [11]MACRO_DATA_DIGIT_ARR
     *          - Data  : [0]MACRO_DATA_BIT, [1]MACRO_DATA_BYTE, [2]MACRO_DATA_WORD, [3]MACRO_DATA_DWORD, [4]MACRO_DATA_REAL, [5]MACRO_DATA_VOLTAGE, [6]MACRO_DATA_DIGIT
     *        dArg[4] Size ([4] MACRO_DATA_XXX_ARR"일 경우)
     *        dArg[5~21] Data Type에따른 출력값 "[4] MACRO_DATA_XXX_ARR"일 경우의 출력값
     *    [5] MACRO_FUNC_WAIT
     *        dArg[0] 최종 호출된 WAIT명령의 노드번호
     *        dArg[1] 지정한 매크로 내에서 WAIT명령들 중의 Index를 반환
     *        dArg[2] 최종 호출된 WAIT명령의 설정 시간을 반환
     *        dArg[3] 최종 호출된 WAIT명령의 소요 시간을 반환
     *        dArg[4] 매크로가 동작한 전체 시간을 반환(대기시간 포함, 1 Cycle 동작 후 다시 시작시 초기화 됨[Ready -> Run])
     *        dArg[5] 해당 매크로에 포함된 마지막 WAIT Function의 노드번호
     *    [6] MACRO_FUNC_STOP
     *        dArg[0] 최종 호출된 STOP명령의 노드번호
     *        dArg[1] 지정한 매크로 내에서 STOP명령들 중의 Index를 반환
     *        dArg[2] 최종 호출된 STOP명령의 매크로 번호를 반환
     *        dArg[3] 최종 호출된 STOP명령의 Stop Mode를 반환
     *        dArg[5] 해당 매크로에 포함된 마지막 STOP Function의 노드번호
     *    [7] MACRO_FUNC_PAUSE
     *        dArg[0] 최종 호출된 PAUSE명령의 노드번호
     *        dArg[1] 지정한 매크로 내에서 PAUSE명령들 중의 Index를 반환
     *        dArg[2] 지정한 매크로 내에서 PAUSE중인지 반환
     *        dArg[5] 해당 매크로에 포함된 마지막 PAUSE Function의 노드번호
	 *
     * @note 주의 사항: dpReturnData 배열의 개수는 반드시 21개로 지정
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxpMacroReadFunctionStatus(int nMacroNo, uint uFunction, double[] dpReturnData);

	/**
     * @brief 지정된 매크로의 Watchdog Timer 갱신
     * 
     * @param nMacroNo 매크로 번호 (0~95)
	 * @param uTimeoutMs Watchdog Timeout Value (ms)
     *
	 * @details
	 * uTimeoutMs
	 *    이 시간동안 AxpMacroSetWatchdogTimer 함수가 재 호출되지 않으면 Watchdog Timeout 발생되며
	 *    AxpMacroStart함수의 시작 조건으로 MACRO_START_WATCHDOG_TIMEOUT으로 지정한 해당 매크로 동작
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxpMacroSetWatchdogTimer(int nMacroNo, uint uTimeoutMs);
}