/**
 * @file AXDev.cs
 * 
 * @brief 아진엑스텍 AXDev 라이브러리 헤더 파일
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

public class CAXDev
{
    // 보드 및 모듈 확인 함수 (Information)

    /**
     * @brief Board Number를 이용하여 Board Address 확인
     *
     * @param nBoardNo Board 번호
     * @param upBoardAddress Board 주소 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlGetBoardAddress(int nBoardNo, ref uint upBoardAddress);
    
	/**
     * @brief Board Number를 이용하여 Board ID 확인
     *
     * @param nBoardNo Board 번호
     * @param upBoardID Board ID 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlGetBoardID(int nBoardNo, ref uint upBoardID);

	/**
     * @brief Board Number를 이용하여 Board Version 확인
     *
     * @param nBoardNo Board 번호
     * @param upBoardVersion Board 버전 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlGetBoardVersion(int nBoardNo, ref uint upBoardVersion);

	/**
     * @brief Board Number와 Module Position을 이용하여 Module ID 확인
     *
     * @param nBoardNo Board 번호
     * @param nModulePos Module 위치
     * @param upModuleID Module ID 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlGetModuleID(int nBoardNo, int nModulePos, ref uint upModuleID);
    
	/**
     * @brief Board Number와 Module Position을 이용하여 Module Version 확인
     *
     * @param nBoardNo Board 번호
     * @param nModulePos Module 위치
     * @param upModuleVersion Module Version 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlGetModuleVersion(int nBoardNo, int nModulePos, ref uint upModuleVersion);
    
	/**
     * @brief Board Number와 Module Position을 이용하여 Network Node 정보 확인
     *
     * @param nBoardNo Board 번호
     * @param nModulePos Module 위치
     * @param upNetNo Network Node 번호 저장
     * @param upNodeAddr Node 주소 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlGetModuleNodeInfo(int nBoardNo, int nModulePos, ref uint upNetNo, ref uint upNodeAddr);

    /**
     * @brief Board에 내장된 범용 Data Flash 쓰기
     *
     * @param nBoardNo Board 번호
     * @param nPageAddr Flash 페이지 주소 (0 ~ 199)
     * @param nBytesNum 바이트 수 (1 ~ 120)
     * @param upSetData 데이터
	 *
	 * @note PCI-R1604[RTEX master board] 전용
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlSetDataFlash(int nBoardNo, int nPageAddr, int nBytesNum, ref byte upSetData);

    /**
     * @brief Board에 내장된 범용 Data Flash 읽기
     *
     * @param nBoardNo Board 번호
     * @param nPageAddr Flash 페이지 주소 (0 ~ 199)
     * @param nBytesNum 바이트 수 (1 ~ 120)
     * @param upGetData 데이터
	 *
	 * @note PCI-R1604[RTEX master board] 전용
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlGetDataFlash(int nBoardNo, int nPageAddr, int nBytesNum, ref uint upGetData);
    
	/**
     * @brief Board에 내장된 ESTOP 외부 입력 신호를 이용한 InterLock 기능 사용 유무 및 디지털 필터 상수값 정의
     *
     * @param nBoardNo Board 번호
     * @param dwInterLock 기능 사용 유무 설정 (0: 사용하지 않음(default), 1: 사용)
     * @param dwDigFilterVal 디지털 필터 값 설정 (입력 필터 상수 설정 범위: 1 ~ 40, 단위: 통신 Cycle time)
	 *
	 * @details
	 * dwInterLock: 기능 사용 설정 후 외부에서 ESTOP 신호 인가 시 보드에 연결된 모션 제어 노드에 대해 ESTOP 구동 명령 실행
	 *
	 * @note PCI-Rxx00[MLIII master board] 전용
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlSetEStopInterLock(int nBoardNo, uint dwInterLock, uint dwDigFilterVal);
    
	/**
     * @brief Board에 설정된 ESTOP 외부 입력 신호를 이용한 InterLock 기능 사용 유무 및 디지털 필터 상수값 확인
     *
     * @param nBoardNo Board 번호
     * @param dwInterLock 기능 사용 유무 설정 값 저장
     * @param dwDigFilterVal 디지털 필터 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlGetEStopInterLock(int nBoardNo, ref uint dwInterLock, ref uint dwDigFilterVal);
    
	/**
     * @brief Board에 입력된 EstopInterLock 신호 확인
     *
     * @param nBoardNo Board 번호
     * @param dwInterLock ESTOP 외부 입력 신호 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlReadEStopInterLock(int nBoardNo, ref uint dwInterLock);
 
    /**
     * @brief Board 번호와 Module 위치를 이용하여 AIO Module 번호 확인
     *
     * @param nBoardNo Board 번호
     * @param nModulePos Module 위치
     * @param npModuleNo AIO Module 번호 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxaInfoGetModuleNo(int nBoardNo, int nModulePos, ref int npModuleNo);
    
	/**
     * @brief Board 번호와 Module 위치를 이용하여 DIO Module 번호 확인
     *
     * @param nBoardNo Board 번호
     * @param nModulePos Module 위치
     * @param npModuleNo DIO Module 번호 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxdInfoGetModuleNo(int nBoardNo, int nModulePos, ref int npModuleNo);

    /**
     * @brief 지정된 축에 IPCOMMAND 설정
     *
     * @param nAxisNo 축 번호
     * @param sCommand 설정할 IPCOMMAND 값 (0~0xFF)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetCommand(int nAxisNo, byte sCommand);
    
	/**
     * @brief 지정된 축에 8bit IPCOMMAND 설정
     *
     * @param nAxisNo 축 번호
     * @param sCommand 설정할 IPCOMMAND 값 (0~0xFF)
	 * @param uData 설정할 8비트 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetCommandData08(int nAxisNo, byte sCommand, uint uData);
    
	/**
     * @brief 지정된 축에 8bit IPCOMMAND 설정 값 확인
     *
     * @param nAxisNo 축 번호
     * @param sCommand 설정할 IPCOMMAND 값 (0~0xFF)
	 * @param uData 설정할 8비트 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGetCommandData08(int nAxisNo, byte sCommand, ref uint upData);
    
	/**
     * @brief 지정된 축에 16bit IPCOMMAND 설정
     *
     * @param nAxisNo 축 번호
     * @param sCommand 설정할 IPCOMMAND 값 (0~0xFF)
	 * @param uData 설정할 16비트 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetCommandData16(int nAxisNo, byte sCommand, uint uData);
    
	/**
     * @brief 지정된 축에 16bit IPCOMMAND 설정 값 확인
     *
     * @param nAxisNo 축 번호
     * @param sCommand 설정할 IPCOMMAND 값 (0~0xFF)
	 * @param uData 설정할 16비트 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGetCommandData16(int nAxisNo, byte sCommand, ref uint upData);

	/**
     * @brief 지정된 축에 24bit IPCOMMAND 설정
     *
     * @param nAxisNo 축 번호
     * @param sCommand 설정할 IPCOMMAND 값 (0~0xFF)
	 * @param uData 설정할 24비트 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetCommandData24(int nAxisNo, byte sCommand, uint uData);
    
	/**
     * @brief 지정된 축에 24bit IPCOMMAND 설정 값 확인
     *
     * @param nAxisNo 축 번호
     * @param sCommand 설정할 IPCOMMAND 값 (0~0xFF)
	 * @param uData 설정할 24비트 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGetCommandData24(int nAxisNo, byte sCommand, ref uint upData);
    
	/**
     * @brief 지정된 축에 32bit IPCOMMAND 설정
     *
     * @param nAxisNo 축 번호
     * @param sCommand 설정할 IPCOMMAND 값 (0~0xFF)
	 * @param uData 설정할 32비트 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetCommandData32(int nAxisNo, byte sCommand, uint uData);
    
	/**
     * @brief 지정된 축에 32bit IPCOMMAND 설정 값 확인
     *
     * @param nAxisNo 축 번호
     * @param sCommand 설정할 IPCOMMAND 값 (0~0xFF)
	 * @param uData 설정할 32비트 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGetCommandData32(int nAxisNo, byte sCommand, ref uint upData);
    
    /**
     * @brief 지정된 축에 QICOMMAND 설정
     *
     * @param nAxisNo 축 번호
     * @param sCommand 설정할 QICOMMAND 값 (0~0xFF)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetCommandQi(int nAxisNo, byte sCommand);
    
	/**
     * @brief 지정된 축에 8bit QICOMMAND 설정
     *
     * @param nAxisNo 축 번호
     * @param sCommand 설정할 QICOMMAND 값 (0~0xFF)
	 * @param uData 설정할 8비트 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetCommandData08Qi(int nAxisNo, byte sCommand, uint uData);
    
	/**
     * @brief 지정된 축에 8bit QICOMMAND 설정 값 확인
     *
     * @param nAxisNo 축 번호
     * @param sCommand 설정할 QICOMMAND 값 (0~0xFF)
	 * @param uData 설정할 8비트 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGetCommandData08Qi(int nAxisNo, byte sCommand, ref uint upData);
    
	/**
     * @brief 지정된 축에 16bit QICOMMAND 설정
     *
     * @param nAxisNo 축 번호
     * @param sCommand 설정할 QICOMMAND 값 (0~0xFF)
	 * @param uData 설정할 16비트 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetCommandData16Qi(int nAxisNo, byte sCommand, uint uData);
    
	/**
     * @brief 지정된 축에 16bit QICOMMAND 설정 값 확인
     *
     * @param nAxisNo 축 번호
     * @param sCommand 설정할 QICOMMAND 값 (0~0xFF)
	 * @param uData 설정할 16비트 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGetCommandData16Qi(int nAxisNo, byte sCommand, ref uint upData);
    
	/**
     * @brief 지정된 축에 24bit QICOMMAND 설정
     *
     * @param nAxisNo 축 번호
     * @param sCommand 설정할 QICOMMAND 값 (0~0xFF)
	 * @param uData 설정할 24비트 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetCommandData24Qi(int nAxisNo, byte sCommand, uint uData);
    
	/**
     * @brief 지정된 축에 24bit QICOMMAND 설정 값 확인
     *
     * @param nAxisNo 축 번호
     * @param sCommand 설정할 QICOMMAND 값 (0~0xFF)
	 * @param uData 설정할 24비트 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGetCommandData24Qi(int nAxisNo, byte sCommand, ref uint upData);
    
	/**
     * @brief 지정된 축에 32bit QICOMMAND 설정
     *
     * @param nAxisNo 축 번호
     * @param sCommand 설정할 QICOMMAND 값 (0~0xFF)
	 * @param uData 설정할 32비트 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetCommandData32Qi(int nAxisNo, byte sCommand, uint uData);
    
	/**
     * @brief 지정된 축에 32bit QICOMMAND 설정 값 확인
     *
     * @param nAxisNo 축 번호
     * @param sCommand 설정할 QICOMMAND 값 (0~0xFF)
	 * @param uData 설정할 32비트 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGetCommandData32Qi(int nAxisNo, byte sCommand, ref uint upData);
    
    /**
     * @brief 지정된 축의 포트 데이터 확인 - IP
     *
     * @param nAxisNo 축 번호
     * @param wOffset 포트 데이터 오프셋
     * @param upData 포트 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGetPortData(int nAxisNo,  uint wOffset, ref uint upData);
    
	/**
     * @brief 지정된 축의 포트 데이터 설정 - IP
     *
     * @param nAxisNo 축 번호
     * @param wOffset 포트 데이터 오프셋
     * @param upData 포트 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetPortData(int nAxisNo, uint wOffset, uint dwData);

    /**
     * @brief 지정된 축의 포트 데이터 확인 - QI
     *
     * @param nAxisNo 축 번호
     * @param wOffset 포트 데이터 오프셋
     * @param upData 포트 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGetPortDataQi(int nAxisNo, uint byOffset, ref uint wData);
    
	/**
     * @brief 지정된 축의 포트 데이터 설정 - QI
     *
     * @param nAxisNo 축 번호
     * @param wOffset 포트 데이터 오프셋
     * @param upData 포트 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetPortDataQi(int nAxisNo, uint byOffset, uint wData);
        
    /**
     * @brief 지정된 축에 스크립트 설정 - IP
     *
     * @param nAxisNo 축 번호
     * @param sc 스크립트 번호 (1 ~ 4)
     * @param uEvent 발생할 이벤트 SCRCON 정의
     * @param data 변경 data 선택
	 *
	 * @details uEvent: 이벤트 설정 축 개수 설정, 이벤트 발생 할 축, 이벤트 내용 1, 2 속성 설정
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetScriptCaptionIp(int nAxisNo, int sc, uint uEvent, uint data);
    
	/**
     * @brief 지정된 축에 스크립트 반환 - IP
     *
     * @param nAxisNo 축 번호
     * @param sc 스크립트 번호 (1 ~ 4)
     * @param uEvent 발생할 이벤트 SCRCON 정의 값 저장
     * @param data 바뀐 data 저장
	 *
	 * @details uEvent: 이벤트 설정 축 개수 설정, 이벤트 발생 할 축, 이벤트 내용 1, 2 속성 설정
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGetScriptCaptionIp(int nAxisNo, int sc, ref uint uEvent, ref uint data);

    /**
     * @brief 지정된 축에 스크립트 설정 - QI
     *
     * @param nAxisNo 축 번호
     * @param sc 스크립트 번호 (1 ~ 4)
     * @param uEvent 발생할 이벤트 SCRCON 정의 값
     * @param cmd 어떤 내용을 바꿀 것인지 선택 SCRCMD 정의
     * @param data 어떤 Data를 바꿀 것인지 선택
	 *
	 * @details uEvent: 이벤트 설정 축 개수 설정, 이벤트 발생 할 축, 이벤트 내용 1, 2 속성 설정
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetScriptCaptionQi(int nAxisNo, int sc, uint uEvent, uint cmd, uint data);
    
	/**
     * @brief 지정된 축에 스크립트 반환 - QI
     *
     * @param nAxisNo 축 번호
     * @param sc 스크립트 번호 (1 ~ 4)
     * @param uEvent 발생할 이벤트 SCRCON 정의 값 저장
     * @param cmd 어떤 내용을 바꿀 것인지 선택 SCRCMD 정의 값 저장
     * @param data 바뀐 data 저장
	 *
	 * @details uEvent: 이벤트 설정 축 개수 설정, 이벤트 발생 할 축, 이벤트 내용 1, 2 속성 설정
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGetScriptCaptionQi(int nAxisNo, int sc, ref uint uEvent, ref uint cmd, ref uint data);

    /**
     * @brief 지정 축에 스크립트 내부 Queue Index를 Clear
     *
     * @param nAxisNo 축 번호
     * @param uSelect IP/QI 선택 값
	 *
	 * @details
	 * uSelect IP
     *    [0] 스크립트 Queue Index Clear
     *    [1] 캡션 Queue Index Clear
     * uSelect QI 
     *    [0] 스크립트 Queue 1 Index Clear
     *    [1] 스크립트 Queue 2 Index Clear
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetScriptCaptionQueueClear(int nAxisNo, uint uSelect);
    
    /**
     * @brief 지정 축에 스크립트 내부 Queue Index 반환
     *
     * @param nAxisNo 축 번호
	 * @param updata Queue index 값 저장
     * @param uSelect IP/QI 선택 값
     *
	 * @details
	 * uSelect IP
     *    [0] 스크립트 Queue Index 읽음
     *    [1] 캡션 Queue Index 읽음
     * uSelect QI 
     *    [0] 스크립트 Queue 1 Index 읽음
     *    [1] 스크립트 Queue 2 Index 읽음
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGetScriptCaptionQueueCount(int nAxisNo, ref uint updata, uint uSelect);

    /**
     * @brief 지정 축에 스크립트 내부 Queue의 Data 개수 반환
     *
     * @param nAxisNo 축 번호
	 * @param updata Queue Data 개수 저장
     * @param uSelect IP/QI 선택 값
     *
	 * @details
	 * uSelect IP
     *    [0] 스크립트 Queue Data 읽음
     *    [1] 캡션 Queue Data 읽음
     * uSelect QI 
     *    [0] 스크립트 Queue 1 Data 읽음
     *    [1] 스크립트 Queue 2 Data 읽음
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGetScriptCaptionQueueDataCount(int nAxisNo, ref uint updata, uint uSelect);

    /**
     * @brief 내부 데이터 읽음
     *
     * @param nAxisNo 축 번호
     * @param dMinVel 최소 속도
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param wRangeData 범위 데이터 저장
     * @param wStartStopSpeedData 시작 및 정지 속도 데이터 저장
     * @param wObjectSpeedData 객체 속도 데이터 저장
     * @param wAccelRate 가속률 데이터 저장
     * @param wDecelRate 감속률 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGetOptimizeDriveData(int nAxisNo, double dMinVel, double dVel, double dAccel, double  dDecel, 
            ref uint wRangeData, ref uint wStartStopSpeedData, ref uint wObjectSpeedData, ref uint wAccelRate, ref uint wDecelRate);

    /**
     * @brief 보드 내의 레지스터를 Byte 단위 쓰기
     *
     * @param nBoardNo 보드 번호
     * @param wOffset 오프셋
     * @param byData 설정 할 Byte 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmBoardWriteByte(int nBoardNo, uint wOffset, byte byData);
	
	/**
     * @brief 보드 내의 레지스터를 Byte 단위 읽기
     *
     * @param nBoardNo 보드 번호
     * @param wOffset 오프셋
     * @param byData 읽은 Byte 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmBoardReadByte(int nBoardNo, uint wOffset, ref byte byData);

    /**
     * @brief 보드 내의 레지스터를 WORD 단위 쓰기
     *
     * @param nBoardNo 보드 번호
     * @param wOffset 오프셋
     * @param byData 설정 할 WORD 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmBoardWriteWord(int nBoardNo, uint wOffset, uint wData);
	
	/**
     * @brief 보드 내의 레지스터를 WORD 단위 읽기
     *
     * @param nBoardNo 보드 번호
     * @param wOffset 오프셋
     * @param byData 읽은 WORD 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmBoardReadWord(int nBoardNo, uint wOffset, ref ushort wData);

    /**
     * @brief 보드 내의 레지스터를 DWORD 단위 쓰기
     *
     * @param nBoardNo 보드 번호
     * @param wOffset 오프셋
     * @param byData 설정 할 DWORD 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmBoardWriteDWord(int nBoardNo, uint wOffset, uint dwData);
	
	/**
     * @brief 보드 내의 레지스터를 DWORD 단위 읽기
     *
     * @param nBoardNo 보드 번호
     * @param wOffset 오프셋
     * @param byData 읽은 DWORD 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmBoardReadDWord(int nBoardNo, uint wOffset, ref uint dwData);

    /**
     * @brief 보드 내 모듈의 레지스터를 Byte 단위 쓰기
     *
     * @param nBoardNo 보드 번호
     * @param nModulePos 모듈 위치
	 * @param wOffset 오프셋
     * @param byData 설정 할 Byte 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmModuleWriteByte(int nBoardNo, int nModulePos, uint wOffset, byte byData);
	
	/**
     * @brief 보드 내 모듈의 레지스터를 Byte 단위 읽기
     *
     * @param nBoardNo 보드 번호
     * @param nModulePos 모듈 위치
	 * @param wOffset 오프셋
     * @param byData 읽은 Byte 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmModuleReadByte(int nBoardNo, int nModulePos, uint wOffset, ref byte byData);

    /**
     * @brief 보드 내 모듈의 레지스터를 WORD 단위 쓰기
     *
     * @param nBoardNo 보드 번호
	 * @param nModulePos 모듈 위치
     * @param wOffset 오프셋
     * @param byData 설정 할 WORD 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmModuleWriteWord(int nBoardNo, int nModulePos, uint wOffset, uint wData);
	
	/**
     * @brief 보드 내 모듈의 레지스터를 WORD 단위 읽기
     *
     * @param nBoardNo 보드 번호
     * @param nModulePos 모듈 위치
	 * @param wOffset 오프셋
     * @param byData 읽은 WORD 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmModuleReadWord(int nBoardNo, int nModulePos, uint wOffset, ref ushort wData);

    /**
     * @brief 보드 내 모듈의 레지스터를 DWORD 단위 쓰기
     *
     * @param nBoardNo 보드 번호
	 * @param nModulePos 모듈 위치
     * @param wOffset 오프셋
     * @param byData 설정 할 DWORD 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmModuleWriteDWord(int nBoardNo, int nModulePos, uint wOffset, uint dwData);
	
	/**
     * @brief 보드 내 모듈의 레지스터를 DWORD 단위 읽기
     *
     * @param nBoardNo 보드 번호
     * @param nModulePos 모듈 위치
	 * @param wOffset 오프셋
     * @param byData 읽은 DWORD 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmModuleReadDWord(int nBoardNo, int nModulePos, uint wOffset, ref uint dwData);

    /**
     * @brief 외부 위치 비교기에 값 설정 (Pos = Unit)
     *
     * @param nAxisNo 축 번호
     * @param dPos 설정 할 위치 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusSetActComparatorPos(int nAxisNo, double dPos);
    
	/**
     * @brief 외부 위치 비교기에 값 반환 (Pos = Unit)
     *
     * @param nAxisNo 축 번호
     * @param dPos 반환 할 위치 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusGetActComparatorPos(int nAxisNo, ref double dpPos);

    /**
     * @brief 내부 위치 비교기에 값 설정 (Pos = Unit)
     *
     * @param nAxisNo 축 번호
     * @param dPos 설정 할 위치 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusSetCmdComparatorPos(int nAxisNo, double dPos);
    
	/**
     * @brief 내부 위치 비교기에 값 반환 (Pos = Unit)
     *
     * @param nAxisNo 축 번호
     * @param dPos 반환 할 위치 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusGetCmdComparatorPos(int nAxisNo, ref double dpPos);
    
	// 추가 함수
    
    /**
     * @brief 직선 보간을 속도만 가지고 무한대로 증가
     *
     * @param nCoord 좌표계 번호
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
	 *
	 * @note 속도 비율대로 거리를 넣어줘야 함
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmLineMoveVel(int nCoord, double dVel, double dAccel, double dDecel);

	// 센서 위치 구동 함수 (필독: IP만 가능, QI에는 기능 없음)
    
    /**
     * @brief 지정 축 Sensor 신호의 사용 유무 및 신호 입력 레벨 설정
     *
     * @param nAxisNo 축 번호
     * @param uLevel 신호 입력 레벨 (0: LOW, 1: HIGH, 2: UNUSED, 3: USED)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSensorSetSignal(int nAxisNo, uint uLevel);
    
	 /**
     * @brief 지정 축 Sensor 신호의 사용 유무 및 신호 입력 레벨 확인
     *
     * @param nAxisNo 축 번호
     * @param uLevel 신호 입력 레벨 값 저장 (0: LOW, 1: HIGH, 2: UNUSED, 3: USED)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSensorGetSignal(int nAxisNo, ref uint upLevel);
    
	/**
     * @brief 지정 축 Sensor 신호의 입력 상태 반환
     *
     * @param nAxisNo 축 번호
     * @param upStatus Sensor 신호 입력 상태 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSensorReadSignal(int nAxisNo, ref uint upStatus);
    
    /**
     * @brief 지정 축을 설정된 속도와 가속율로 센서 위치 드라이버 구동
     *
     * @param nAxisNo 축 번호
     * @param dPos 위치
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param nMethod 구동 방법
	 *
	 * @details
	 * nMethod
	 *    0: 일반 구동
	 *    1: 센서 신호 검출 전 저속 구동, 신호 검출 후 일반 구동
	 *    2: 저속 구동
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSensorMovePos(int nAxisNo, double dPos, double dVel, double dAccel, double dDecel, int nMethod);

    /**
     * @brief 지정 축을 설정된 속도와 가속율로 센서 위치 드라이버 구동하고
     *
     * @param nAxisNo 축 번호
     * @param dPos 위치
     * @param dVel 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param nMethod 구동 방법
	 *
	 * @note 
	 * Sensor 신호의 Active level 입력 이후 상대 좌표로 설정 된 거리만큼 구동 후 정지
	 * 펄스 출력이 종료되는 시점에 함수를 벗어남
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSensorStartMovePos(int nAxisNo, double dPos, double dVel, double dAccel, double dDecel, int nMethod);

    /**
     * @brief 원점 검색 진행 스텝 변화 기록 반환
     *
     * @param nAxisNo 축 번호
     * @param upStepCount 기록된 Step 개수 저장
     * @param upMainStepNumber 기록된 MainStepNumber 정보의 배열 포인터 저장
     * @param upStepNumber 기록된 StepNumber 정보의 배열 포인터 저장
     * @param upStepBranch 기록된 Step 별 Branch 정보의 배열 포인터 저장
	 *
	 * @note 주의 사항: 배열 개수는 50개로 고정
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeGetStepTrace(int nAxisNo, ref uint upStepCount, uint[] upMainStepNumber, uint[] upStepNumber, uint[] upStepBranch); 

	// 추가 홈 서치 (PI-N804/404에만 해당 됨)

    /**
     * @brief 사용자가 지정한 축의 홈 설정 파라미터 설정 (QI 칩 전용 레지스터 이용)
     *
     * @param nAxisNo 축 번호
     * @param uZphasCount 홈 완료 후에 Z상 카운트 (0 ~ 15)
     * @param nHomeMode 홈 설정 모드 (0 ~ 12)
     * @param nClearSet 위치 클리어, 잔여 펄스 클리어 사용 선택 (0 ~ 3)
     * @param dOrgVel 홈 관련 Org Speed 설정
     * @param dLastVel 홈 관련 Last Speed 설정
     * @param dLeavePos 떠날 위치 설정
	 *
	 * @details
	 * nClearSet
	 *    0: 위치 클리어 사용 안함, 잔여 펄스 클리어 사용 안함
	 *    1: 위치 클리어 사용함, 잔여 펄스 클리어 사용 안함
	 *    2: 위치 클리어 사용 안함, 잔여 펄스 클리어 사용함
	 *    3: 위치 클리어 사용함, 잔여 펄스 클리어 사용함
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeSetConfig(int nAxisNo, uint uZphasCount, int nHomeMode, int nClearSet, double dOrgVel, double dLastVel, double dLeavePos);
    
	/**
     * @brief 사용자가 지정한 축의 홈 설정 파라미터 반환 (QI 칩 전용 레지스터 이용)
     *
     * @param nAxisNo 축 번호
     * @param uZphasCount 홈 완료 후에 Z상 카운트 값 저장 (0 ~ 15)
     * @param nHomeMode 홈 설정 모드 저장 (0 ~ 12)
     * @param nClearSet 위치 클리어, 잔여 펄스 클리어 사용 선택 값 저장 (0 ~ 3)
     * @param dOrgVel 홈 관련 Org Speed 설정 값 저장
     * @param dLastVel 홈 관련 Last Speed 설정 값 저장
     * @param dLeavePos 떠날 위치 설정 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeGetConfig(int nAxisNo, ref uint upZphasCount, ref int npHomeMode, ref int npClearSet, ref double dpOrgVel, ref double dpLastVel, ref double dpLeavePos); //KKJ(070215)
    
    /**
     * @brief 사용자가 지정한 축의 홈 서치 시작
     *
     * @param nAxisNo 축 번호
     * @param dVel 속도 (양수: CW, 음수: CCW)
     * @param dAccel 가속도
     * @param dDecel 감속도
	 *
	 * @note
	 * lHomeMode 사용 시 설정: 0 ~ 5 설정 (Move Return 후에 Search 시작)
	 * lHomeMode -1로 그대로 사용 시 HomeConfig에서 사용한 그대로 설정 됨
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeSetMoveSearch(int nAxisNo, double dVel, double dAccel, double dDecel);

    /**
     * @brief 사용자가 지정한 축의 홈 리턴 시작
     *
     * @param nAxisNo 축 번호
     * @param dVel 구동 속도 (양수: CW, 음수: CCW)
     * @param dAccel 가속도
     * @param dDecel 감속도
	 *
	 * @note
	 * lHomeMode 사용 시 설정: 0 ~ 12 설정
	 * lHomeMode -1로 그대로 사용 시 HomeConfig에서 사용한 그대로 설정 됨
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeSetMoveReturn(int nAxisNo, double dVel, double dAccel, double dDecel);
    
    /**
     * @brief 사용자가 지정한 축의 홈 이탈 시작
     *
     * @param nAxisNo 축 번호
     * @param dVel 구동 속도 (양수: CW, 음수: CCW)
     * @param dAccel 가속도
     * @param dDecel 감속도
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeSetMoveLeave(int nAxisNo, double dVel, double dAccel, double dDecel);

    /**
     * @brief 사용자가 지정한 다축의 홈 서치 시작
     *
     * @param nArraySize 축 개수
     * @param npAxesNo 축 번호 배열
     * @param dpVel 구동 속도 배열 (양수: CW, 음수: CCW)
     * @param dpAccel 가속도 배열
     * @param dpDecel 감속도 배열
	 *
	 * @note
	 * lHomeMode 사용 시 설정: 0 ~ 5 설정 (Move Return 후에 Search 시작)
	 * lHomeMode -1로 그대로 사용 시 HomeConfig에서 사용한 그대로 설정 됨
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeSetMultiMoveSearch(int nArraySize, int[] npAxesNo, double[] dpVel, double[] dpAccel, double[] dpDecel);

    /**
     * @brief 지정된 좌표계의 구동 속도 프로파일 모드 설정
     *
     * @param nCoord 좌표계 번호
     * @param uProfileMode 프로파일 모드 설정
	 *
	 * @details
	 * uProfileMode
	 *    0: 대칭 Trapezode
	 *    1: 비대칭 Trapezode
	 *    2: 대칭 Quasi-S Curve
	 *    3: 대칭 S Curve
	 *    4: 비대칭 S Curve
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiSetProfileMode(int nCoord, uint uProfileMode);
    
	/**
     * @brief 지정된 좌표계의 구동 속도 프로파일 모드 확인
     *
     * @param nCoord 좌표계 번호
     * @param uProfileMode 프로파일 모드 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiGetProfileMode(int nCoord, ref uint upProfileMode);

    // DIO 인터럽트 플래그 레지스트 읽기
	
    /**
     * @brief 지정한 입력 접점 모듈의 Interrupt Flag Register의 Offset 위치에서 Bit 단위로 인터럽트 발생 상태 값 확인
     *
     * @param nModuleNo 모듈 번호
     * @param nOffset Offset 위치
     * @param upValue 읽은 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptFlagReadBit(int nModuleNo, int nOffset, ref uint upValue);
    
	/**
     * @brief 지정한 입력 접점 모듈의 Interrupt Flag Register의 Offset 위치에서 Byte 단위로 인터럽트 발생 상태 값 확인
     *
     * @param nModuleNo 모듈 번호
     * @param nOffset Offset 위치
     * @param upValue 읽은 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptFlagReadByte(int nModuleNo, int nOffset, ref uint upValue);
    
	/**
     * @brief 지정한 입력 접점 모듈의 Interrupt Flag Register의 Offset 위치에서 Word 단위로 인터럽트 발생 상태 값 확인
     *
     * @param nModuleNo 모듈 번호
     * @param nOffset Offset 위치
     * @param upValue 읽은 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptFlagReadWord(int nModuleNo, int nOffset, ref uint upValue);
    
	/**
     * @brief 지정한 입력 접점 모듈의 Interrupt Flag Register의 Offset 위치에서 DWORD 단위로 인터럽트 발생 상태 값 확인
     *
     * @param nModuleNo 모듈 번호
     * @param nOffset Offset 위치
     * @param upValue 읽은 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptFlagReadDword(int nModuleNo, int nOffset, ref uint upValue);
    
	/**
     * @brief 전체 입력 접점 모듈의 Interrupt Flag Register의 Offset 위치에서 bit 단위로 인터럽트 발생 상태 값 확인
     *
     * @param nModuleNo 모듈 번호
     * @param nOffset Offset 위치
     * @param upValue 읽은 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptFlagRead(int nOffset, ref uint upValue);

    /**
     * @brief EtherCAT Digital Input Data를 lStartModuleNo 부터 lReadSize만큼 읽어 반환
     *
     * @param lStartModuleNo Input Data를 읽기 시작할 Module 번호
     * @param lReadSize 읽고자 하는 Input Data 크기(byte 단위)
     * @param pbyData 읽어온 Digital Input Data가 저장 될 버퍼
     *
     * @return 
     * - AXT_RT_SUCCESS : 함수 호출 성공
     * - AXT_RT_DIO_NOT_MODULE : lStartModuleNo가 0보다 작은 경우
     * - AXT_RT_DIO_INVALID_MODULE_NO : 유효하지 않은 DIO Module일 경우
     * - AXT_RT_2ND_BELOW_MIN_VALUE : lReadSize가 0일 경우
     * - AXT_RT_2ND_ABOVE_MAX_VALUE : lReadSize가 실제 데이터의 사이즈보다 클 경우
     */
    [DllImport("AXL.dll")] public static extern uint AxdiECatRead(int lStartModuleNo, int lReadSize, ref byte pbyData);

    /**
     * @brief EtherCAT Digital Output Data를 lStartModuleNo 부터 lReadSize만큼 읽어 반환
     *
     * @param lStartModuleNo Output Data를 읽기 시작할 Module 번호
     * @param lReadSize 읽고자 하는 Output Data 크기(byte 단위)
     * @param pbyData 읽어온 Digital Output Data가 저장 될 버퍼
	 *
     * @return 
	 * - AXT_RT_SUCCESS : 함수 호출 성공
	 * - AXT_RT_DIO_NOT_MODULE : lStartModuleNo가 0보다 작은 경우
	 * - AXT_RT_DIO_INVALID_MODULE_NO : 유효하지 않은 DIO Module일 경우
	 * - AXT_RT_2ND_BELOW_MIN_VALUE : lReadSize가 0일 경우
	 * - AXT_RT_2ND_ABOVE_MAX_VALUE : lReadSize가 실제 데이터의 사이즈보다 클 경우
     */
    [DllImport("AXL.dll")] public static extern uint AxdoECatRead(int lStartModuleNo, int lReadSize, ref byte pbyData);

    /**
     * @brief pbyData 버퍼의 EtherCAT Digital Output Data를 lStartModuleNo부터 lWriteSize만큼 출력
     *
     * @param lStartModuleNo Output Data를 쓰기 시작할 Module 번호
     * @param lWriteSize 쓰고자 하는 Output Data 크기(byte 단위)
     * @param pbyData 읽어온 Digital Output Data가 저장된 버퍼
	 *
     * @return 
	 * - AXT_RT_SUCCESS : 함수 호출 성공
	 * - AXT_RT_DIO_NOT_MODULE : lStartModuleNo가 0보다 작은 경우
	 * - AXT_RT_DIO_INVALID_MODULE_NO : 유효하지 않은 DIO Module일 경우
	 * - AXT_RT_2ND_BELOW_MIN_VALUE : lWriteSize가 0일 경우
	 * - AXT_RT_2ND_ABOVE_MAX_VALUE : lWriteSize가 실제 데이터의 사이즈보다 클 경우
     */
    [DllImport("AXL.dll")] public static extern uint AxdoECatWrite(int lStartModuleNo, int lWriteSize, ref byte pbyData);


    // 로그 관련 함수

    /**
     * @brief 설정 축의 함수 실행 결과를 EzSpy에서 모니터링 할 수 있도록 설정 또는 해제
     *
     * @param nAxisNo 축 번호
     * @param uUse 사용 유무 (0: DISABLE, 1: ENABLE)
	 *
	 * @note 현재 자동으로 설정 됨
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmLogSetAxis(int nAxisNo, uint uUse);
    
    /**
     * @brief 설정 축의 함수 실행 결과를 EzSpy에서 모니터링 여부 확인
     *
     * @param nAxisNo 축 번호
     * @param uUse 사용 유무 정보 저장 (0: DISABLE, 1: ENABLE)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmLogGetAxis(int nAxisNo, ref uint upUse);

	// 로그 출력 관련 함수
	
    /**
     * @brief 지정한 입력 채널의 EzSpy에 로그 출력 여부 설정
     *
     * @param nChannelNo 채널 번호
     * @param uUse 사용 유무 (0: DISABLE, 1: ENABLE)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxaiLogSetChannel(int nChannelNo, uint uUse);
    
	/**
     * @brief 지정한 입력 채널의 EzSpy에 로그 출력 여부 확인
     *
     * @param nChannelNo 채널 번호
     * @param uUse 사용 유무 정보 저장 (0: DISABLE, 1: ENABLE)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxaiLogGetChannel(int nChannelNo, ref uint upUse);

	// 지정한 출력 채널의 EzSpy 로그 출력
	 
    /**
     * @brief 지정한 출력 채널의 EzSpy에 로그 출력 여부 설정
     *
     * @param nChannelNo 채널 번호
     * @param upUse 사용 유무 (0: DISABLE, 1: ENABLE)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxaoLogSetChannel(int nChannelNo, uint uUse);
    
	/**
     * @brief 지정한 출력 채널의 EzSpy에 로그 출력 여부 확인
     *
     * @param nChannelNo 채널 번호
     * @param upUse 사용 유무 정보 저장 (0: DISABLE, 1: ENABLE)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxaoLogGetChannel(int nChannelNo, ref uint upUse);

	// Log
	
    /**
     * @brief 지정한 모듈의 EzSpy에 로그 출력 여부 설정
     *
     * @param nModuleNo 모듈 번호
     * @param uUse 사용 유무 (0: DISABLE, 1: ENABLE)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxdLogSetModule(int nModuleNo, uint uUse);
    
	/**
     * @brief 지정한 모듈의 EzSpy에 로그 출력 여부 확인
     *
     * @param nModuleNo 모듈 번호
     * @param uUse 사용 유무 정보 저장 (0: DISABLE, 1: ENABLE)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxdLogGetModule(int nModuleNo, ref uint upUse);

    /**
     * @brief 해당 보드의 firmware 버전 확인
     *
     * @param nBoardNo 보드 번호
     * @param szVersion 버전 정보 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlGetFirmwareVersion(int nBoardNo, ref byte szVersion);
    
	/**
     * @brief 지정한 보드로 Firmware 전송
     *
     * @param nBoardNo 보드 번호
     * @param wData 데이터 배열
     * @param wCmdData 명령 데이터 배열
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlSetFirmwareCopy(int nBoardNo, ushort[] wData, ushort[] wCmdData);
    
	/**
     * @brief 지정한 보드로 Firmware 업데이트 수행
     *
     * @param nBoardNo 보드 번호
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlSetFirmwareUpdate(int nBoardNo);
    
	/**
     * @brief 지정한 보드의 현재 RTEX 초기화 상태 확인
     *
     * @param nBoardNo 보드 번호
     * @param dwStatus 초기화 상태 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlCheckStatus(int nBoardNo, ref uint dwStatus);
	
    /**
     * @brief RTEX Master board의 지정 축에 범용 명령 실행
     *
     * @param nBoardNo 보드 번호
     * @param wCmd 명령
     * @param wOffset 오프셋
     * @param wData 데이터 배열
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlRtexUniversalCmd(int nBoardNo, ushort wCmd, ushort wOffset, ushort[] wData);
    
	/**
     * @brief 지정한 축에 RTEX 통신 명령 실행
     *
     * @param nAxisNo 축 번호
     * @param dwCmdCode 명령 코드
     * @param dwTypeCode 타입 코드
     * @param dwIndexCode 인덱스 코드
     * @param dwCmdConfigure 명령 구성
     * @param dwValue 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmRtexSlaveCmd(int nAxisNo, uint dwCmdCode, uint dwTypeCode, uint dwIndexCode, uint dwCmdConfigure, uint dwValue);
    
	/**
     * @brief 지정한 축에서 실행한 RTEX 통신 명령의 결과 값 확인
     *
     * @param nAxisNo 축 번호
     * @param dwIndex 인덱스 값 저장
     * @param dwValue 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmRtexGetSlaveCmdResult(int nAxisNo, ref uint dwIndex, ref uint dwValue);
    
	/**
     * @brief 지정한 축에서 실행한 RTEX 통신 명령의 결과 값 확인
     *
     * @param nAxisNo 축 번호
     * @param dwpCommand 명령 저장
     * @param dwpType 타입 저장
     * @param dwpIndex 인덱스 저장
     * @param dwpValue 값 저장
	 *
	 * @note PCIE-Rxx04-RTEX 제품 전용
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmRtexGetSlaveCmdResultEx(int nAxisNo, ref uint dwpCommand, ref uint dwpType, ref uint dwpIndex, ref uint dwpValue);
	
    /**
     * @brief 지정한 축의 RTEX 상태 정보 확인
     *
     * @param nAxisNo 축 번호
     * @param dwStatus 상태 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmRtexGetAxisStatus(int nAxisNo, ref uint dwStatus);
	
    /**
     * @brief 지정한 축의 RTEX 통신 리턴 정보 확인 (Actual position, Velocity, Torque)
     *
     * @param nAxisNo 축 번호
     * @param dwReturn1 첫 번째 리턴값 저장
     * @param dwReturn2 두 번째 리턴값 저장
     * @param dwReturn3 세 번째 리턴값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmRtexGetAxisReturnData(int nAxisNo,  ref uint dwReturn1, ref uint dwReturn2, ref uint dwReturn3);
    
	/**
     * @brief RTEX Slave 지정 축의 현재 상태 정보 확인 (mechanical, Inposition 등)
     *
     * @param nAxisNo 축 번호
     * @param dwStatus 상태 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmRtexGetAxisSlaveStatus(int nAxisNo,  ref uint dwStatus);
    /**
     * @brief MLII Slave 지정 축에 범용 네트워크 명령어 기입
     *
     * @param nAxisNo 축 번호
     * @param tagCommand 범용 네트워크 명령어
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetAxisCmd(int nAxisNo, ref uint tagCommand);
    
	/**
     * @brief MLII Slave 지정 축에 범용 네트워크 명령 결과 확인
     *
     * @param nAxisNo 축 번호
     * @param tagCommand 범용 네트워크 명령 결과 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGetAxisCmdResult(int nAxisNo, ref uint tagCommand);

    /**
     * @brief 지정한 SIIIH Digital Slave 모듈에 네트워크 명령 결과를 기입하고 반환
     *
     * @param nModuleNo 모듈 번호
     * @param tagSetCommand 설정 명령어 배열
     * @param tagGetCommand 반환 명령어 배열
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxdSetAndGetSlaveCmdResult(int nModuleNo, uint[] tagSetCommand, uint[] tagGetCommand);
	
	/**
     * @brief 지정한 SIIIH Analog Slave 모듈에 네트워크 명령 결과를 기입하고 반환
     *
     * @param nModuleNo 모듈 번호
     * @param tagSetCommand 설정 명령어 배열
     * @param tagGetCommand 반환 명령어 배열
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxaSetAndGetSlaveCmdResult(int nModuleNo, uint[] tagSetCommand, uint[] tagGetCommand);
	
	/**
     * @brief 지정한 SIIIH Counter Slave 모듈에 네트워크 명령 결과를 기입하고 반환
     *
     * @param nModuleNo 모듈 번호
     * @param tagSetCommand 설정 명령어 배열
     * @param tagGetCommand 반환 명령어 배열
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxcSetAndGetSlaveCmdResult(int nModuleNo, uint[] tagSetCommand, uint[] tagGetCommand);

    /**
     * @brief DPRAM 데이터 확인
     *
     * @param nBoardNo 보드 번호
     * @param uAddress 주소
     * @param upRdData 읽은 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlGetDpRamData(int nBoardNo, ushort uAddress, ref uint upRdData);
	
	/**
     * @brief DPRAM 데이터 Word 단위로 확인
     *
     * @param nBoardNo 보드 번호
     * @param uOffset 오프셋
     * @param upRdData 읽은 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
	[DllImport("AXL.dll")] public static extern uint AxlBoardReadDpramWord(int nBoardNo, ushort uOffset, ref uint upRdData);
	
	/**
     * @brief DPRAM 데이터 Word 단위로 설정
     *
     * @param nBoardNo 보드 번호
     * @param uOffset 오프셋
     * @param upRdData 쓸 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlBoardWriteDpramWord(int nBoardNo, ushort uOffset, uint uWrData);
    
    /**
     * @brief 각 보드의 각 SLAVE 별로 명령 전송
     *
     * @param nBoardNo 보드 번호
     * @param uCommand 명령
     * @param upSendData 전송 할 데이터 저장
     * @param uLength 데이터 길이
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlSetSendBoardEachCommand(int nBoardNo, uint uCommand, uint[] upSendData, uint uLength);

	/**
     * @brief 각 보드로 명령 전송
     *
     * @param nBoardNo 보드 번호
     * @param uCommand 명령
     * @param upSendData 전송 할 데이터 저장
     * @param uLength 데이터 길이
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlSetSendBoardCommand(int nBoardNo, uint uCommand, uint[] upSendData, uint uLength);

	/**
     * @brief 각 보드의 응답 확인
     *
     * @param nBoardNo 보드 번호
     * @param upReadData 읽은 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlGetResponseBoardCommand(int nBoardNo, ref uint upReadData);

    /**
     * @brief Network Type Master 보드에서 Motion Slave들의 Firmware Version 읽음
     *
     * @param nAxisNo 축 번호
     * @param ucaFirmwareVersion Firmware Version이 저장 될 unsigned char 배열 (크기는 4 이상)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmInfoGetFirmwareVersion(int nAxisNo, byte[] ucaFirmwareVersion);
	
	/**
     * @brief Network Type Master 보드에서 Analog Slave들의 Firmware Version 읽음
     *
     * @param nAxisNo 축 번호
     * @param ucaFirmwareVersion Firmware Version이 저장 될 unsigned char 배열 (크기는 4 이상)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxaInfoGetFirmwareVersion(int nModuleNo, byte[] ucaFirmwareVersion);
	
	/**
     * @brief Network Type Master 보드에서 Digital Slave들의 Firmware Version 읽음
     *
     * @param nAxisNo 축 번호
     * @param ucaFirmwareVersion Firmware Version이 저장 될 unsigned char 배열 (크기는 4 이상)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxdInfoGetFirmwareVersion(int nModuleNo, byte[] ucaFirmwareVersion);
	
	/**
     * @brief Network Type Master 보드에서 Counter Slave들의 Firmware Version 읽음
     *
     * @param nAxisNo 축 번호
     * @param ucaFirmwareVersion Firmware Version이 저장 될 unsigned char 배열 (크기는 4 이상)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxcInfoGetFirmwareVersion(int nModuleNo, byte[] ucaFirmwareVersion);

	// PCI-R1604-MLII 전용 함수
	 
    /**
     * @brief Interpolate and Latch Command의 Option Field의 Torq Feed Forward 값 설정
     *
     * @param nAxisNo 축 번호
     * @param uTorqFeedForward Torq Feed Forward 값
	 *
	 * @note
	 * 기본값: MAX로 설정
	 * 설정 값 범위: 0 ~ 4000H
	 * 설정 값을 4000H 이상으로 설정 하면 설정은 그 이상으로 되나 동작은 4000H 값 적용
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetTorqFeedForward(int nAxisNo, uint uTorqFeedForward);
 
    /**
     * @brief Interpolate and Latch Command의 Option Field의 Torq Feed Forward 설정 값 확인
     *
     * @param nAxisNo 축 번호
     * @param uTorqFeedForward Torq Feed Forward 값 저장
	 *
	 * @note
	 * 기본값: MAX로 설정
	 * 설정 값 범위: 0 ~ 4000H
	 * 설정 값을 4000H 이상으로 설정 하면 설정은 그 이상으로 되나 동작은 4000H 값 적용
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGetTorqFeedForward(int nAxisNo, ref uint upTorqFeedForward);
 
    /**
     * @brief Interpolate and Latch Command의 VFF Field의 Velocity Feed Forward 값 설정
     *
     * @param nAxisNo 축 번호
     * @param uVelocityFeedForward Velocity Feed Forward 값
	 *
	 * @note
	 * 기본값: 0 설정
	 * 설정 값 범위: 0 ~ FFFFH
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetVelocityFeedForward(int nAxisNo, uint uVelocityFeedForward);
 
    /**
     * @brief Interpolate and Latch Command의 VFF Field의 Velocity Feed Forward 설정 값 확인
     *
     * @param nAxisNo 축 번호
     * @param uVelocityFeedForward Velocity Feed Forward 값 저장
	 *
	 * @note
	 * 기본값: 0 설정
	 * 설정 값 범위: 0 ~ FFFFH
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmGetVelocityFeedForward(int nAxisNo, ref uint upVelocityFeedForward);

    /**
     * @brief 설정값을 사용하여 Encoder 타입 설정
     *
     * @param nAxisNo 축 번호
     * @param uEncoderType Encoder 타입 설정 값
	 *
	 * @details
	 * uEncoderType
	 *    0: TYPE_INCREMENTAL
	 *    1: TYPE_ABSOLUTE
	 *    2: TYPE_NONE
	 *
	 * @note
	 * 기본 값은 0(TYPE_INCREMENTAL)으로 설정되어 있음
	 * 설정 값은 0 ~ 1까지 설정 할 수 있음
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetEncoderType(int nAxisNo, uint uEncoderType);

    /**
     * @brief Encoder 타입 설정 값 확인
     *
     * @param nAxisNo 축 번호
     * @param uEncoderType Encoder 타입 설정 값 저장
	 *
	 * @details
	 * uEncoderType
	 *    0: TYPE_INCREMENTAL
	 *    1: TYPE_ABSOLUTE
	 *    2: TYPE_NONE
	 *
	 * @note
	 * 기본 값은 0(TYPE_INCREMENTAL)으로 설정되어 있음
	 * 설정 값은 0 ~ 1까지 설정 할 수 있음
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetEncoderType(int nAxisNo, ref uint upEncoderType);

    /**
     * @brief Slave Firmware 업데이트를 위해 축에 명령 전송
     *
     * @param nAxisNo 축 번호
     * @param wCommand 전송할 명령
     * @param dAccel 전송할 데이터 배열
     * @param dDecel 전송할 데이터 배열의 길이
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    //[DllImport("AXL.dll")] public static extern uint AxmSetSendAxisCommand(int nAxisNo, short wCommand, short[] wpSendData, short wLength);

    // PCI-R1604-RTEX, RTEX-PM 전용 함수
	
    /**
     * @brief 범용 입력 2, 3번 입력 시 JOG 구동 속도 설정
     *
     * @param lAxisNo 축 번호
     * @param dVelocity JOG 구동 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
	 *
	 * @note 구동에 관련된 모든 설정(Ex: PulseOutMethod, MoveUnitPerPulse 등)들이 완료된 이후 한번만 실행해야 함
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetUserMotion(int nAxisNo, double dVelocity, double dAccel, double dDecel);

    /**
     * @brief 범용 입력 2, 3번 입력 시 JOG 구동 동작 사용 여부 설정
     *
     * @param nAxisNo 축 번호
     * @param dwUsage 설정 값 (0: DISABLE, 1: ENABLE)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetUserMotionUsage(int nAxisNo, uint dwUsage);

    /**
     * @brief MPGP 입력을 사용하여 Load/UnLoad 위치를 자동으로 이동하는 기능 설정
     *
     * @param nAxisNo 축 번호
     * @param dVelocity 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param dLoadPos Load 위치
     * @param dUnLoadPos UnLoad 위치
     * @param dwFilter 필터 값
     * @param dwDelay 지연 시간
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetUserPosMotion(int nAxisNo, double dVelocity, double dAccel, double dDecel, double dLoadPos, double dUnLoadPos, uint dwFilter, uint dwDelay);

    /**
     * @brief MPGP 입력을 사용하여 Load/UnLoad 위치를 자동으로 이동하는 기능 사용 여부 설정
     *
     * @param nAxisNo 축 번호
     * @param dwUsage 설정 값
	 *
	 * @details
	 * dwUsage
	 *    0: DISABLE
	 *    1: Position 기능 A 사용
	 *    2: Position 기능 B 사용
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetUserPosMotionUsage(int nAxisNo, uint dwUsage);

    // SIO-CN2CH, 절대 위치 트리거 기능 모듈 전용 함수
	
    /**
     * @brief 메모리에 데이터 쓰기
     *
     * @param nChannelNo 채널 번호
     * @param dwAddr 메모리 주소
     * @param dwData 데이터 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxcKeWriteRamDataAddr(int nChannelNo, uint dwAddr, uint dwData);
    
	/**
     * @brief 메모리로부터 데이터 읽기
     *
     * @param nChannelNo 채널 번호
     * @param dwAddr 메모리 주소
     * @param dwData 읽은 데이터 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxcKeReadRamDataAddr(int nChannelNo, uint dwAddr, ref uint dwpData);
    
	/**
     * @brief 메모리 초기화
     *
     * @param nModuleNo 모듈 번호
     * @param dwData 초기화 할 데이터 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxcKeResetRamDataAll(int nModuleNo, uint dwData);
    
	/**
     * @brief 트리거 타임 아웃 설정
     *
     * @param nChannelNo 채널 번호
     * @param dwTimeout 타임아웃 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetTimeout(int nChannelNo, uint dwTimeout);
    
	/**
     * @brief 트리거 타임 아웃 설정 값 확인
     *
     * @param nChannelNo 채널 번호
     * @param dwTimeout 타임아웃 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerGetTimeout(int nChannelNo, ref uint dwpTimeout);
	
    /**
     * @brief 트리거 대기 상태 확인
     *
     * @param nChannelNo 채널 번호
     * @param dwpState 대기 상태 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxcStatusGetWaitState(int nChannelNo, ref uint dwpState);

	/**
     * @brief 트리거 대기 상태 설정
     *
     * @param nChannelNo 채널 번호
     * @param dwpState 대기 상태 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxcStatusSetWaitState(int nChannelNo, uint dwState);
    
    /**
     * @brief 지정된 채널에 32비트 명령어 설정
     *
     * @param nChannelNo 채널 번호
     * @param dwCommand 명령어
     * @param dwData 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxcKeSetCommandData32(int nChannelNo, uint dwCommand, uint dwData);
    
    /**
     * @brief 지정된 채널에 16비트 명령어 설정
     *
     * @param nChannelNo 채널 번호
     * @param dwCommand 명령어
     * @param dwData 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxcKeSetCommandData16(int nChannelNo, uint dwCommand, uint wData); 
    
    /**
     * @brief 지정된 채널에 32비트 명령어 설정 값 확인
     *
     * @param nChannelNo 채널 번호
     * @param dwCommand 명령어
     * @param dwData 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxcKeGetCommandData32(int nChannelNo, uint dwCommand, ref uint dwpData);
    
    /**
     * @brief 지정된 채널에 16비트 명령어 설정 값 확인
     *
     * @param nChannelNo 채널 번호
     * @param dwCommand 명령어
     * @param dwData 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxcKeGetCommandData16(int nChannelNo, uint dwCommand, ref uint wpData);
    
    // PCI-N804/N404 전용, Sequence Motion
	
    /**
     * @brief Sequence Motion의 축 정보 설정
     *
     * @param nSeqMapNo 축 번호 정보를 담는 Sequence Motion Index Point
     * @param nSeqMapSize 축 번호 개수
     * @param nSeqAxesNo 축 번호 배열
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSeqSetAxisMap(int nSeqMapNo, int nSeqMapSize, int[] nSeqAxesNo);
    
	/**
     * @brief Sequence Motion의 축 정보 설정 값 확인
     *
     * @param nSeqMapNo 축 번호 정보를 담는 Sequence Motion Index Point
     * @param nSeqMapSize 축 번호 개수 저장
     * @param nSeqAxesNo 축 번호 배열 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmSeqGetAxisMap(int nSeqMapNo, ref int nSeqMapSize, ref int nSeqAxesNo);

    /**
     * @brief Sequence Motion의 Master 축 설정
     *
     * @param nSeqMapNo 축 번호 정보를 담는 Sequence Motion Index Point
     * @param nMasterAxisNo Master 축 번호
	 *
	 * @note 반드시 AxmSeqSetAxisMap에 설정 된 축 내에서 설정해야 함
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */    
    [DllImport("AXL.dll")] public static extern uint AxmSeqSetMasterAxisNo(int nSeqMapNo, int nMasterAxisNo);

    /**
     * @brief Sequence Motion의 Node 적재 시작을 라이브러리에 알림
     *
     * @param nSeqMapNo 축 번호 정보를 담는 Sequence Motion Index Point
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */    
    [DllImport("AXL.dll")] public static extern uint AxmSeqBeginNode(int nSeqMapNo);

    /**
     * @brief Sequence Motion의 Node 적재 종료를 라이브러리에 알림
     *
     * @param nSeqMapNo 축 번호 정보를 담는 Sequence Motion Index Point
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */     
    [DllImport("AXL.dll")] public static extern uint AxmSeqEndNode(int nSeqMapNo);

    /**
     * @brief Sequence Motion의 구동 시작
     *
     * @param nSeqMapNo 축 번호 정보를 담는 Sequence Motion Index Point
     * @param dwStartOption 구동 시작 옵션 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */    
    [DllImport("AXL.dll")] public static extern uint AxmSeqStart(int nSeqMapNo, uint dwStartOption);

    /**
     * @brief Sequence Motion의 각 Profile Node 정보를 라이브러리에 입력
     *
     * @param nSeqMapNo 축 번호 정보를 담는 Sequence Motion Index Point
     * @param dPosition 위치 배열
     * @param dVelocity 속도
     * @param dAcceleration 가속도
     * @param dDeceleration 감속도
     * @param dNextVelocity 다음 노드로 이동할 속도
	 *
	 * @note 만약 1축 Sequence Motion을 사용하더라도 dPosition는 1개의 Array로 지정
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드 반환
     */    
    [DllImport("AXL.dll")] public static extern uint AxmSeqAddNode(int nSeqMapNo, double[] dPosition, double dVelocity, double dAcceleration, double dDeceleration, double dNextVelocity);

    /**
     * @brief Sequence Motion이 구동 시 현재 실행 중인 Node Index 반환
     *
     * @param nSeqMapNo 축 번호 정보를 담는 Sequence Motion Index Point
     * @param nCurNodeNo 현재 실행 중인 Node의 인덱스 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */    
    [DllImport("AXL.dll")] public static extern uint AxmSeqGetNodeNum(int nSeqMapNo, ref int nCurNodeNo);

    /**
     * @brief Sequence Motion의 총 Node Count 확인
     *
     * @param nSeqMapNo 축 번호 정보를 담는 Sequence Motion Index Point
     * @param nTotalNodeCnt 총 Node 개수 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */    
    [DllImport("AXL.dll")] public static extern uint AxmSeqGetTotalNodeNum(int nSeqMapNo, ref int nTotalNodeCnt);

    /**
     * @brief Sequence Motion이 현재 구동 여부 확인
     *
     * @param nSeqMapNo 축 번호 정보를 담는 Sequence Motion Index Point
     * @param dwInMotion 현재 구동 중 여부 값 저장 (0: 구동 종료, 1: 구동 중)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */    
    [DllImport("AXL.dll")] public static extern uint AxmSeqIsMotion(int nSeqMapNo, ref uint dwInMotion);

    /**
     * @brief Sequence Motion의 Memory Clear
     *
     * @param nSeqMapNo 축 번호 정보를 담는 Sequence Motion Index Point
	 *
	 * @note AxmSeqSetAxisMap, AxmSeqSetMasterAxisNo에서 설정된 값은 유지
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */    
    [DllImport("AXL.dll")] public static extern uint AxmSeqWriteClear(int nSeqMapNo);

    /**
     * @brief Sequence Motion의 구동을 종료하는 함수
     *
     * @param nSeqMapNo 축 번호 정보를 담는 Sequence Motion Index Point
     * @param dwStopMode 구동 정지 모드 (0: EMERGENCY_STOP, 1: SLOWDOWN_STOP)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */    
    [DllImport("AXL.dll")] public static extern uint AxmSeqStop(int nSeqMapNo, uint dwStopMode);
    
    // PCIe-Rxx04-SIIIH 전용 함수
    
	/**
     * @brief 모니터링을 위한 매개변수 설정 함수
     *
     * @param nAxisNo 축 번호
     * @param dwParaNo1 매개변수 1
     * @param dwParaNo2 매개변수 2
     * @param dwParaNo3 매개변수 3
     * @param dwParaNo4 매개변수 4
     * @param dwUse 사용 여부
	 *
	 * @note
	 * SIIIH, MR_J4_xxB, Para: 0 ~ 8
	 *    [0] Command Position
	 *    [1] Actual Position
	 *    [2] Actual Velocity
	 *    [3] Mechanical Signal
	 *    [4] Regeneration load factor(%)
	 *    [5] Effective load factor(%)
	 *    [6] Peak load factor(%)
	 *    [7] Current Feedback
	 *    [8] Command Velocity
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusSetMon(int nAxisNo, uint dwParaNo1, uint dwParaNo2, uint dwParaNo3, uint dwParaNo4, uint dwUse);
	
	/**
     * @brief 모니터링을 위한 매개변수 설정 값 확인
     *
     * @param nAxisNo 축 번호
     * @param dwParaNo1 매개변수 1 값 저장
     * @param dwParaNo2 매개변수 2 값 저장
     * @param dwParaNo3 매개변수 3 값 저장
     * @param dwParaNo4 매개변수 4 값 저장
     * @param dwUse 사용 여부 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusGetMon(int nAxisNo, ref uint dwpParaNo1, ref uint dwpParaNo2, ref uint dwpParaNo3, ref uint dwpParaNo4, ref uint dwpUse);
	
	/**
     * @brief 모니터링 데이터 확인
     *
     * @param nAxisNo 축 번호
     * @param dwpParaNo1 매개변수 1 상태 값 저장
     * @param dwpParaNo2 매개변수 2 상태 값 저장
     * @param dwpParaNo3 매개변수 3 상태 값 저장
     * @param dwpParaNo4 매개변수 4 상태 값 저장
     * @param dwDataValid 데이터 유효성 여부 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadMon(int nAxisNo, ref uint dwpParaNo1, ref uint dwpParaNo2, ref uint dwpParaNo3, ref uint dwpParaNo4, ref uint dwDataValid);
	
	/**
     * @brief 모니터링 데이터 확인 (확장 버전)
     *
     * @param nAxisNo 축 번호
     * @param npDataCnt 데이터 개수 저장
     * @param dwpReadData 읽은 데이터 배열 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadMonEx(int nAxisNo, ref int npDataCnt, uint[] dwpReadData);

    // PCI-R32IOEV-RTEX 전용 함수
	
    /**
     * @brief I/O 포트로 할당된 HPI register 쓰기
     *
     * @param nBoardNo 보드 번호
     * @param dwAddr 주소
     * @param dwData 쓸 데이터
	 *
	 * @note
	 * I/O Registers for HOST interface
	 *    I/O 00h: Host status register (HSR)
	 *    I/O 04h: Host-to-DSP control register (HDCR)
	 *    I/O 08h: DSP page register (DSPP)
	 *    I/O 0Ch: Reserved
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlSetIoPort(int nBoardNo, uint dwAddr, uint dwData);
	
	/**
     * @brief I/O 포트로 할당된 HPI register 읽기
     *
     * @param nBoardNo 보드 번호
     * @param dwAddr 주소
     * @param dwData 읽은 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlGetIoPort(int nBoardNo, uint dwAddr, ref uint dwpData);                    

    // PCI-R3200-MLIII 전용 함수

    /**
     * @brief M-III Master 보드 펌웨어 업데이트를 위한 기본 정보 설정
     *
     * @param nBoardNo 보드 번호
     * @param dwTotalPacketSize 전체 패킷 크기
	 * @param dwProcTotalStepNo 펌웨어 업데이트 절차의 총 단계 수
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3SetFWUpdateInit(int nBoardNo, uint dwTotalPacketSize, uint dwProcTotalStepNo);

    /**
     * @brief M-III Master 보드 펌웨어 업데이트를 위한 기본 정보 설정 값 확인
     *
     * @param nBoardNo 보드 번호
     * @param dwTotalPacketSize 전체 패킷 크기 저장
	 * @param dwProcTotalStepNo 펌웨어 업데이트 절차의 총 단계 수 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3GetFWUpdateInit(int nBoardNo, ref uint dwTotalPacketSize, ref uint dwProcTotalStepNo);

    /** 
     * @brief M-III Master 보드의 펌웨어 업데이트 위한 데이터 전달
     *
     * @param nBoardNo 보드 번호
     * @param lFWUpdataData 펌웨어 업데이트 데이터
     * @param dwLength 데이터 길이
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3SetFWUpdateCopy(int nBoardNo, uint[] pdwPacketData, uint dwPacketSize);

    /** 
     * @brief M-III Master 보드 펌웨어 업데이트 자료 전달 결과 확인
     *
     * @param nBoardNo 보드 번호
     * @param dwPacketSize 패킷 사이즈 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3GetFWUpdateCopy(int nBoardNo, ref uint dwPacketSize);

    /**
     * @brief M-III Master 보드의 펌웨어 업데이트 실행
     *
     * @param nBoardNo 보드 번호
     * @param dwFlashBurnStepNo ?????
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3SetFWUpdate(int nBoardNo, uint dwFlashBurnStepNo);

    /**
     * @brief M-III Master 보드 펌웨어 업데이트 실행 결과 확인
     *
     * @param nBoardNo 보드 번호
     * @param dwFlashBurnStepNo ?????
     * @param dwIsFlashBurnDone 펌웨어 업데이트 완료 여부 저장
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3GetFWUpdate(int nBoardNo, ref uint dwFlashBurnStepNo, ref uint dwIsFlashBurnDone);
    
    /**
     * @brief M-III Master 보드의 EEPROM 데이터 설정
     *
     * @param nBoardNo 보드 번호
     * @param pCmdData 명령어 정보 배열
     * @param CmdDataSize 명령어 데이터 크기
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3SetCFGData(int nBoardNo, uint[] pCmdData, uint CmdDataSize);
    
    /**
     * @brief M-III Master 보드의 EEPROM 데이터 설정 확인
     *
     * @param nBoardNo 보드 번호
     * @param pCmdData 명령어 정보 배열 저장
     * @param CmdDataSize 명령어 데이터 크기
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3GetCFGData(int nBoardNo, uint[] pCmdData, uint CmdDataSize);

    /**
     * @brief M-III Master 보드의 CONNECT PARAMETER 기본 정보 설정
     *
     * @param nBoardNo 보드 번호
     * @param wCh0Slaves ?????
     * @param wCh1Slaves 
     * @param dwCh0CycTime 
     * @param dwCh1CycTime 
     * @param dwChInfoMaxRetry 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3SetMCParaUpdateInit(int nBoardNo, ushort wCh0Slaves, ushort wCh1Slaves, uint dwCh0CycTime, uint dwCh1CycTime, uint dwChInfoMaxRetry);

    /**
     * @brief M-III Master 보드의 CONNECT PARAMETER 기본 정보 설정 결과 확인 함수
     *
     * @param nBoardNo 보드 번호
     * @param wCh0Slaves ?????
     * @param wCh1Slaves 
     * @param dwCh0CycTime 
     * @param dwCh1CycTime 
     * @param dwChInfoMaxRetry 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3GetMCParaUpdateInit(int nBoardNo, ref ushort wCh0Slaves, ref ushort wCh1Slaves, ref uint dwCh0CycTime, ref uint dwCh1CycTime, ref uint dwChInfoMaxRetry);

    /**
     * @brief M-III Master 보드의 CONNECT PARAMETER 기본 정보 전달
     *
     * @param nBoardNo 보드 번호
     * @param wIdx 인덱스 값
     * @param wChannel 채널 번호
     * @param wSlaveAddr 슬레이브 주소
     * @param dwProtoCalType 프로토콜 타입
     * @param dwTransBytes 전송 바이트 수
     * @param dwDeviceCode 장치 코드
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3SetMCParaUpdateCopy(int nBoardNo, ushort wIdx, ushort wChannel, ushort wSlaveAddr, uint dwProtoCalType, uint dwTransBytes, uint dwDeviceCode);

    /**
     * @brief M-III Master 보드의 CONNECT PARAMETER 기본 정보 전달 결과 확인 함수
     *
     * @param nBoardNo 보드 번호
     * @param wIdx 인덱스 값
     * @param wChannel 채널 번호 저장
     * @param wSlaveAddr 슬레이브 주소 저장
     * @param dwProtoCalType 프로토콜 타입 저장
     * @param dwTransBytes 전송 바이트 수 저장
     * @param dwDeviceCode 장치 코드 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3GetMCParaUpdateCopy(int nBoardNo, ushort wIdx, ref ushort wChannel, ref ushort wSlaveAddr, ref uint dwProtoCalType, ref uint dwTransBytes, ref uint dwDeviceCode);

    /**
     * @brief M-III Master 보드 내의 레지스터를 DWord 단위로 확인
     *
     * @param nBoardNo 보드 번호
     * @param wOffset 오프셋
     * @param dwData 읽은 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlBoardReadDWord(int nBoardNo, ushort wOffset, ref uint dwData);

    /**
     * @brief M-III Master 보드 내의 레지스터를 DWord 단위로 설정
     *
     * @param nBoardNo 보드 번호
     * @param wOffset 오프셋
     * @param dwData 쓸 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlBoardWriteDWord(int nBoardNo, ushort wOffset, uint dwData);
    
    /**
     * @brief 보드 내의 확장 레지스터를 DWord 단위로 확인
     *
     * @param nBoardNo 보드 번호
     * @param dwOffset 오프셋
     * @param dwData 읽을 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlBoardReadDWordEx(int nBoardNo, uint dwOffset, ref uint dwData);
	
	/**
     * @brief 보드 내의 확장 레지스터를 DWord 단위로 설정
     *
     * @param nBoardNo 보드 번호
     * @param dwOffset 오프셋
     * @param dwData 쓸 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlBoardWriteDWordEx(int nBoardNo, uint dwOffset, uint dwData);

    /**
     * @brief 서보를 정지 모드로 설정
     *
     * @param nAxisNo 축 번호
     * @param bStopMode 정지 모드
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSetCtrlStopMode(int nAxisNo, byte bStopMode);

    /**
     * @brief 서보를 LT 선택 상태로 설정
     *
     * @param nAxisNo 축 번호
     * @param bLtSel1 LT 선택 값 1
     * @param bLtSel2 LT 선택 값 2
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSetCtrlLtSel(int nAxisNo, byte bLtSel1, byte bLtSel2);

    /**
     * @brief 서보의 IO 입력 상태 확인
     *
     * @param nAxisNo 축 번호
     * @param upStatus 입력 상태 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadServoCmdIOInput(int nAxisNo, ref uint upStatus);

    /**
     * @brief 서보의 보간 구동 실행
     *
     * @param nAxisNo 축 번호
     * @param dwTPOS 목표 위치
     * @param dwVFF 속도 피드 포워드 값
     * @param dwTFF 토크 피드 포워드 값
     * @param dwTLIM 타임 리밋 값
     * @param dwExSig1 서보 엑츄레이터 신호 1
     * @param dwExSig2 서보 엑츄레이터 신호 2
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoExInterpolate(int nAxisNo, uint dwTPOS, uint dwVFF, uint dwTFF, uint dwTLIM, uint dwExSig1, uint dwExSig2);

    /**
     * @brief 서보 엑츄레이터 바이어스 설정
     *
     * @param nAxisNo 축 번호
     * @param wBias 바이어스 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSetExpoAccBias(int nAxisNo, ushort wBias);

    /**
     * @brief 서보 엑츄레이터 시간 설정
     *
     * @param nAxisNo 축 번호
     * @param wTime 시간 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSetExpoAccTime(int nAxisNo, ushort wTime);

    /**
     * @brief 서보의 이동 시간 설정
     *
     * @param nAxisNo 축 번호
     * @param wTime 이동 시간 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSetMoveAvrTime(int nAxisNo, ushort wTime);

    /**
     * @brief 서보의 Acc 필터 값 설정
     *
     * @param nAxisNo 축 번호
     * @param bAccFil Acc 필터 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSetAccFilter(int nAxisNo, byte bAccFil);

    /**
     * @brief 서보의 상태 모니터1 설정
     *
     * @param nAxisNo 축 번호
     * @param bMonSel 모니터1 설정 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSetCprmMonitor1(int nAxisNo, byte bMonSel);

    /**
     * @brief 서보의 상태 모니터2 설정
     *
     * @param nAxisNo 축 번호
     * @param bMonSel 모니터2 설정 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSetCprmMonitor2(int nAxisNo, byte bMonSel);

    /**
     * @brief 서보의 상태 모니터1 확인
     *
     * @param nAxisNo 축 번호
     * @param upStatus 상태 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoStatusReadCprmMonitor1(int nAxisNo, ref uint upStatus);

    /**
     * @brief 서보의 상태 모니터2 확인
     *
     * @param nAxisNo 축 번호
     * @param upStatus 상태 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoStatusReadCprmMonitor2(int nAxisNo, ref uint upStatus);

    /**
     * @brief 서보 엑츄레이터의 Acc 및 Dec 값 설정
     *
     * @param nAxisNo 축 번호
     * @param wAcc1 Acc1 값
     * @param wAcc2 Acc2 값
     * @param wAccSW AccSW 값
     * @param wDec1 Dec1 값
     * @param wDec2 Dec2 값
     * @param wDecSW DecSW 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSetAccDec(int nAxisNo, ushort wAcc1, ushort wAcc2, ushort wAccSW, ushort wDec1, ushort wDec2, ushort wDecSW);

    /**
     * @brief 서보 정지 설정
     *
     * @param nAxisNo 축 번호
     * @param nMaxDecel 최대 감속 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSetStop(int nAxisNo, int nMaxDecel);

    // 표준 I/O 기기 공통 커맨드

    /**
     * @brief Network 제품의 각 슬레이브 기기의 파라미터 설정 값 반환
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param wNo 번호
     * @param bSize 크기
     * @param bModuleType 모듈 타입
     * @param pbParam 파라미터 설정 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3GetStationParameter(int nBoardNo, int nModuleNo, ushort wNo, byte bSize, byte bModuleType, byte[] pbParam);

    /**
     * @brief Network 제품의 각 슬레이브 기기의 파라미터 값 설정
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param wNo 번호
     * @param bSize 크기
     * @param bModuleType 모듈 타입
     * @param pbParam 파라미터 설정 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3SetStationParameter(int nBoardNo, int nModuleNo, ushort wNo, byte bSize, byte bModuleType, byte[] pbParam);

    /**
     * @brief Network 제품의 각 슬레이브 기기의 ID 값 반환
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param bIdCode ID 코드
     * @param bOffset 오프셋
     * @param bSize 크기
     * @param bModuleType 모듈 타입
     * @param pbParam ID 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3GetStationIdRd(int nBoardNo, int nModuleNo, byte bIdCode, byte bOffset, byte bSize, byte bModuleType, byte[] pbParam);

    /**
     * @brief Network 제품의 각 슬레이브 기기에 대해 무효 커맨드로 사용
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param bModuleType 모듈 타입
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3SetStationNop(int nBoardNo, int nModuleNo, byte bModuleType);

    /**
     * @brief Network 제품의 각 슬레이브 기기의 셋업 실시
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param bConfigMode 셋업 모드
     * @param bModuleType 모듈 타입
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3SetStationConfig(int nBoardNo, int nModuleNo, byte bConfigMode, byte bModuleType);

    /**
     * @brief Network 제품의 각 슬레이브 기기의 알람 및 경고 상태 값 반환
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param wAlarmRdMod 알람 읽기 모드
     * @param wAlarmIndex 알람 인덱스
     * @param bModuleType 모듈 타입
     * @param pwAlarmData 알람 및 경고 상태 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3GetStationAlarm(int nBoardNo, int nModuleNo, ushort wAlarmRdMod, ushort wAlarmIndex, byte bModuleType, ushort[] pwAlarmData);

    /**
     * @brief Network 제품의 각 슬레이브 기기의 알람 및 경고 상태 해제
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param wAlarmClrMod 알람 해제 모드
     * @param bModuleType 모듈 타입
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3SetStationAlarmClear(int nBoardNo, int nModuleNo, ushort wAlarmClrMod, byte bModuleType);

    /**
     * @brief Network 제품의 각 슬레이브 기기와의 동기 통신 설정
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param bModuleType 모듈 타입
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3SetStationSyncSet(int nBoardNo, int nModuleNo, byte bModuleType);

    /**
     * @brief Network 제품의 각 슬레이브 기기와의 연결 설정
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param bVer 버전
     * @param bComMode 통신 모드
     * @param bComTime 통신 시간
     * @param bProfileType 프로파일 타입
     * @param bModuleType 모듈 타입
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3SetStationConnect(int nBoardNo, int nModuleNo, byte bVer, byte bComMode, byte bComTime, byte bProfileType, byte bModuleType);

    /**
     * @brief Network 제품의 각 슬레이브 기기와의 연결 해제 설정
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param bModuleType 모듈 타입
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3SetStationDisConnect(int nBoardNo, int nModuleNo, byte bModuleType);

    /**
     * @brief Network 제품의 각 슬레이브 기기의 비휘발성 파라미터 설정 값 반환
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param wNo 번호
     * @param bSize 크기
     * @param bModuleType 모듈 타입
     * @param pbParam 파라미터 설정 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3GetStationStoredParameter(int nBoardNo, int nModuleNo, ushort wNo, byte bSize, byte bModuleType, byte[] pbParam);

    /**
     * @brief Network 제품의 각 슬레이브 기기의 비휘발성 파라미터 값 설정
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param wNo 번호
     * @param bSize 크기
     * @param bModuleType 모듈 타입
     * @param pbParam 파라미터 설정
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3SetStationStoredParameter(int nBoardNo, int nModuleNo, ushort wNo, byte bSize, byte bModuleType, byte[] pbParam);

    /**
     * @brief Network 제품의 각 슬레이브 기기의 메모리 설정 값 반환
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param wSize 크기
     * @param dwAddress 주소
     * @param bModuleType 모듈 타입
     * @param bMode 모드
     * @param bDataType 데이터 타입
     * @param pbData 메모리 설정 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3GetStationMemory(int nBoardNo, int nModuleNo, ushort wSize, uint dwAddress, byte bModuleType, byte bMode, byte bDataType, byte[] pbData);

    /**
     * @brief Network 제품의 각 슬레이브 기기의 메모리 값 설정
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param wSize 크기
     * @param dwAddress 주소
     * @param bModuleType 모듈 타입
     * @param bMode 모드
     * @param bDataType 데이터 타입
     * @param pbData 메모리 값 설정
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3SetStationMemory(int nBoardNo, int nModuleNo, ushort wSize, uint dwAddress, byte bModuleType, byte bMode, byte bDataType, byte[] pbData);

    // 표준 I/O 기기 커넥션 커맨드

    /**
     * @brief Network 제품의 각 재정렬된 슬레이브 기기의 자동 액세스 모드 값 설정
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param bModuleType 모듈 타입
     * @param bRWSMode 자동 액세스 모드 값 설정
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3SetStationAccessMode(int nBoardNo, int nModuleNo, byte bModuleType, byte bRWSMode);

    /**
     * @brief Network 제품의 각 재정렬된 슬레이브 기기의 자동 액세스 모드 설정 값 반환
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param bModuleType 모듈 타입
     * @param bRWSMode 자동 액세스 모드 설정 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3GetStationAccessMode(int nBoardNo, int nModuleNo, byte bModuleType, ref byte bRWSMode);

    /**
     * @brief Network 제품의 각 슬레이브 기기의 동기 자동 연결 모드 설정
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param bModuleType 모듈 타입
     * @param dwAutoSyncConnectMode 동기 자동 연결 모드 값 설정
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3SetAutoSyncConnectMode(int nBoardNo, int nModuleNo, byte bModuleType, uint dwAutoSyncConnectMode);

    /**
     * @brief Network 제품의 각 슬레이브 기기의 동기 자동 연결 모드 설정 값 반환
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param bModuleType 모듈 타입
     * @param dwAutoSyncConnectMode 동기 자동 연결 모드 설정 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3GetAutoSyncConnectMode(int nBoardNo, int nModuleNo, byte bModuleType, ref uint dwpAutoSyncConnectMode);

    /**
     * @brief Network 제품의 각 슬레이브 기기에 대한 단일 동기화 연결 설정
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param bModuleType 모듈 타입
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3SyncConnectSingle(int nBoardNo, int nModuleNo, byte bModuleType);

    /**
     * @brief Network 제품의 각 슬레이브 기기에 대한 단일 동기화 연결 해제 설정
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param bModuleType 모듈 타입
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3SyncDisconnectSingle(int nBoardNo, int nModuleNo, byte bModuleType);

    /**
     * @brief Network 제품의 각 슬레이브 기기와의 연결 상태 확인
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param dwData 연결 상태 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3IsOnLine(int nBoardNo, int nModuleNo, ref uint dwData);

    // 표준 I/O 프로파일 커맨드

    /**
     * @brief Network 제품의 각 동기화 상태의 슬레이브 I/O 기기에 대한 데이터 설정값 반환
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param bModuleType 모듈 타입
     * @param pdwParam 데이터 설정 값 저장
     * @param bSize 데이터 설정 값 크기
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3GetStationRWS(int nBoardNo, int nModuleNo, byte bModuleType, uint[] pdwParam, byte bSize);

    /**
     * @brief Network 제품의 각 동기화 상태의 슬레이브 I/O 기기에 대한 데이터 설정
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param bModuleType 모듈 타입
     * @param pdwParam 데이터 설정
     * @param bSize 데이터 설정 값 크기
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3SetStationRWS(int nBoardNo, int nModuleNo, byte bModuleType, uint[] pdwParam, byte bSize);

    /**
     * @brief Network 제품의 각 비동기화 상태의 슬레이브 I/O 기기에 대한 데이터 설정값 반환
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param bModuleType 모듈 타입
     * @param pdwParam 데이터 설정 값 저장
     * @param bSize 데이터 설정 값 크기
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3GetStationRWA(int nBoardNo, int nModuleNo, byte bModuleType, uint[] pdwParam, byte bSize);

    /**
     * @brief Network 제품의 각 비동기화 상태의 슬레이브 I/O 기기에 대한 데이터 설정
     *
     * @param nBoardNo 보드 번호
     * @param nModuleNo 모듈 번호
     * @param bModuleType 모듈 타입
     * @param pdwParam 데이터 설정
     * @param bSize 데이터 설정 값 크기
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlM3SetStationRWA(int nBoardNo, int nModuleNo, byte bModuleType, uint[] pdwParam, byte bSize);

    /**
     * @brief MLIII adjustment operation 설정
     *
     * @param nAxisNo 축 번호
     * @param dwReqCode 요청 코드
	 *
	 * @details
	 * dwReqCode
	 *    0x1005: parameter initialization (20sec)
	 *    0x1008: absolute encoder reset (5sec)
	 *    0x100E: automatic offset adjustment of motor current detection signals (5sec)
	 *    0x1013: Multiturn limit setting (5sec)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3AdjustmentOperation(int nAxisNo, uint dwReqCode);

    /**
     * @brief M3 전용, 원점 검색 진행 상태 진단용 함수
     *
     * @param nAxisNo 축 번호
     * @param upHomeMainStepNumber 원점 검색 주요 단계 번호 저장
     * @param upHomeSubStepNumber 원점 검색 보조 단계 번호 저장
     * @param upHomeLastMainStepNumber 마지막 원점 검색 주요 단계 번호 저장
     * @param upHomeLastSubStepNumber 마지막 원점 검색 보조 단계 번호 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeGetM3FWRealRate(int nAxisNo, ref uint upHomeMainStepNumber, ref uint upHomeSubStepNumber, ref uint upHomeLastMainStepNumber, ref uint upHomeLastSubStepNumber);

    /**
     * @brief M3 전용, 원점 검색 시 센서존에서 탈출 시 보정되는 위치 값 반환
     *
     * @param nAxisNo 축 번호
     * @param dPos 위치 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeGetM3OffsetAvoideSenArea(int nAxisNo, ref double dPos);

    /**
     * @brief M3 전용, 원점 검색 시 센서존에서 탈출 시 보정되는 위치 값 설정
     *
     * @param nAxisNo 축 번호
     * @param dPos 위치 값
	 *
	 * @note
	 * dPos 설정 값이 0이면 자동으로 탈출 시 보정되는 위치 값은 자동으로 설정
	 * dPos 설정 값은 양수만 입력
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmHomeSetM3OffsetAvoideSenArea(int nAxisNo, double dPos);
    
    /**
     * @brief M3 전용, 절대치 엔코더 사용 기준으로 원점 검색 완료 후 CMD/ACT POS 초기화 여부 설정
     *
     * @param nAxisNo 축 번호
     * @param dwSel CMD/ACT POS 초기화 여부 설정 값
	 *
	 * @details
	 * dwSel
	 *    0: 원점 검색 후 CMD/ACTPOS 0으로 초기화
	 *    1: 원점 검색 후 CMD/ACTPOS 값 설정되지 않음
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3SetAbsEncOrgResetDisable(int nAxisNo, uint dwSel);
    
    /**
     * @brief M3 전용, 절대치 엔코더 사용 기준으로 원점 검색 완료 후 CMD/ACT POS 초기화 여부 설정 값 확인
     *
     * @param nAxisNo 축 번호
     * @param dwSel CMD/ACT POS 초기화 여부 설정 값 저장
	 *
	 * @details
	 * dwSel
	 *    0: 원점 검색 후 CMD/ACTPOS 0으로 설정됨 (초기값)
	 *    1: 원점 검색 후 CMD/ACTPOS 값 설정되지 않음
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3GetAbsEncOrgResetDisable(int nAxisNo, ref uint upSel);
    
    /**
     * @brief M3 전용, 슬레이브 OFFLINE 전환 시 알람 유지 기능 사용 여부 설정
     *
     * @param nAxisNo 축 번호
     * @param dwSel 알람 유지 기능 사용 여부 설정 값
	 *
	 * @details
	 * dwSel
	 *    0: ML3 슬레이브 ONLINE->OFFLINE 알람 처리 사용하지 않음 (초기값)
	 *    1: ML3 슬레이브 ONLINE->OFFLINE 알람 처리 사용
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3SetOfflineAlarmEnable(int nAxisNo, uint dwSel);
    
    /**
     * @brief M3 전용, 슬레이브 OFFLINE 전환 시 알람 유지 기능 사용 여부 설정 값 확인
     *
     * @param nAxisNo 축 번호
     * @param dwSel 알람 유지 기능 사용 여부 설정 값 저장
	 *
	 * @details
	 * dwSel
	 *    0: ML3 슬레이브 ONLINE->OFFLINE 알람 처리 사용하지 않음 (초기값)
	 *    1: ML3 슬레이브 ONLINE->OFFLINE 알람 처리 사용
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3GetOfflineAlarmEnable(int nAxisNo, ref uint upSel);
    
    /**
     * @brief M3 전용, 슬레이브 OFFLINE 전환 여부 상태 값 확인
     *
     * @param nAxisNo 축 번호
     * @param upStatus OFFLINE 전환 여부 상태 값 저장
	 *
	 * @details
	 * upStatus
	 *    0: ML3 슬레이브 ONLINE->OFFLINE 전환 되지 않음
	 *    1: ML3 슬레이브 ONLINE->OFFLINE 전환 되었음
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmM3ReadOnlineToOfflineStatus(int nAxisNo, ref uint upStatus);

    /**
     * @brief Network 제품의 Configuration Lock 상태 설정
     *
     * @param lBoardNo 보드 번호
     * @param wLockMode Configuration Lock 상태 설정 값 (0: DISABLE, 1: ENABLE)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlSetLockMode(int lBoardNo, uint wLockMode);
    
	/**
     * @brief Lock 정보 설정
     *
     * @param lBoardNo 보드 번호
     * @param dwTotalNodeNum 전체 노드 수
     * @param dwpNodeNo 노드 번호 저장
     * @param dwpNodeID 노드 ID 저장
     * @param dwpLockData Lock 정보 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlSetLockData(int lBoardNo, uint dwTotalNodeNum, uint[] dwpNodeNo, uint[] dwpNodeID, uint[] dwpLockData);
	
	/**
     * @brief AVC(Acceleration Velocity Control)를 사용하여 지정한 위치로 축 이동 시작
     *
     * @param nAxisNo 축 번호
     * @param dPos 목표 위치
     * @param dMaxVelocity 최대 속도
     * @param dMaxAccel 최대 가속도
     * @param dMinJerk 최소 저크
     * @param dpMoveVelocity 속도
     * @param dpMoveAccel 가속도
     * @param dpMoveJerk 저크 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxmMoveStartPosWithAVC(int nAxisNo, double dPos, double dMaxVelocity, double dMaxAccel, double dMinJerk, ref double dpMoveVelocity, ref double dpMoveAccel, ref double dpMoveJerk);
    
	// EtherCAT 전용 함수
	
    /**
     * @brief StationAddress를 이용하여 EtherCAT Slave 제품의 VendorID, ProductCode, RevisionNo 정보 확인
     *
     * @param dwStationAddress 스테이션 주소
     * @param upVendorID Vendor ID 저장
     * @param upProductCode Product Code 저장
     * @param upRevisionNo Revision 번호 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatGetProductInfo(uint dwStationAddress, ref uint upVendorID, ref uint upProductCode, ref uint upRevisionNo);
	
	/**
     * @brief StationAddress를 이용하여 EtherCAT Slave 제품의 VendorID, ProductCode, RevisionNo를 읽어오는 함수입니다.
     *
     * @param lBoardNo 보드 번호
     * @param dwStationAddress 스테이션 주소
     * @param pdwVendorID Vendor ID 저장
     * @param pdwProductCode Product Code 저장
     * @param pdwRevisionNo Revision 번호 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatGetProductInfoEx(int lBoardNo, uint dwStationAddress, ref uint pdwVendorID, ref uint pdwProductCode, ref uint pdwRevisionNo);

    /**
     * @brief StationAddress를 이용하여 EtherCAT Slave 제품의 Network Status 확인
     *
     * @param dwStationAddress 스테이션 주소
	 *
     * @return Network Status 값 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatGetModuleStatus(uint dwStationAddress);

    /**
     * @brief Input PDO(Process Data Objects) 읽음
     *
     * @param dwBitOffset ProcessImage inputs의 bit offset 값
     * @param dwDataBitLength 읽을 input pdo 데이터의 bit 크기
     * @param pbyData 읽은 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatReadPdoInput(uint dwBitOffset, uint dwDataBitLength, byte[] pbyData);
	
	/**
     * @brief Input PDO(Process Data Objects) 읽음 (확장 버전)
     *
     * @param lBoardNo 보드 번호
     * @param dwBitOffset ProcessImage inputs의 bit offset 값
     * @param dwDataBitLength 읽을 input pdo 데이터의 bit 크기
     * @param pbyData 읽은 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatReadPdoInputEx(int lBoardNo, uint dwBitOffset, uint dwDataBitLength, byte[] pbyData);

    /**
     * @brief Output PDO(Process Data Objects) 읽음
     *
     * @param dwBitOffset ProcessImage outputs의 bit offset 값
     * @param dwDataBitLength 읽을 output pdo 데이터의 bit 크기
     * @param pbyData 읽은 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatReadPdoOutput(uint dwBitOffset, uint dwDataBitLength, byte[] pbyData);
	
	/**
     * @brief Output PDO(Process Data Objects) 읽음 (확장 버전)
     *
     * @param lBoardNo 보드 번호
     * @param dwBitOffset ProcessImage outputs의 bit offset 값
     * @param dwDataBitLength 읽어올 output pdo 데이터의 bit 크기
     * @param pbyData 읽은 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatReadPdoOutputEx(int lBoardNo, uint dwBitOffset, uint dwDataBitLength, byte[] pbyData);

    /**
     * @brief Output Process Data에 값 쓰기
     *
     * @param dwBitOffset ProcessImage outputs의 bit offset 값
     * @param dwDataBitLength 쓸 output pdo 데이터의 bit 크기
     * @param pbyData 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatWritePdoOutput(uint dwBitOffset, uint dwDataBitLength, byte[] pbyData);

	/**
     * @brief Output Process Data에 값 쓰기 (확장 버전)
     *
     * @param lBoardNo 보드 번호
     * @param dwBitOffset ProcessImage outputs의 bit offset 값
     * @param dwDataBitLength 쓸 output pdo 데이터의 bit 크기
     * @param pbyData 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxlECatWritePdoOutputEx(int lBoardNo, uint dwBitOffset, uint dwDataBitLength, byte[] pbyData);

#if !__EZ_MANAGER__
	/**
     * @brief Station Address를 이용해 구조체의 시작 주소, PDO의 Input/Output Data 개수 확인
     *
     * @param lBoardNo 보드 번호
     * @param dwStationAddress 스테이션 주소
     * @param pPdoOffsetData PDO offset data 구조체
     * @param pdwInputSize PDO Input data 개수 저장
	 * @param pdwOutputSize PDO Output data 개수 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxlECatReadPdoBitOffsetInfo(int lBoardNo, uint dwStationAddress, ref PdoOffsetData pPdoOffsetData, ref uint pdwInputSize, ref uint pdwOutputSize);
#endif

	/**
     * @brief Analog ModuleNo의 Input/Output Size를 Bit 단위로 반환
     *
     * @param lBoardNo 보드 번호
     * @param lModuleNo 모듈 주소
     * @param pdwInputSize PDO Input Size 저장
	 * @param pdwOutputSize PDO Output Size 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxaECatReadModuleSizebyBit(int lBoardNo, int lModuleNo, ref uint pdwInputSize, ref uint pdwOutputSize);
	
	/**
     * @brief Digital ModuleNo의 Input/Output Size를 Bit 단위로 반환
     *
     * @param lBoardNo 보드 번호
     * @param lModuleNo 모듈 주소
     * @param pdwInputSize PDO Input Size 저장
	 * @param pdwOutputSize PDO Output Size 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxdECatReadModuleSizebyBit(int lBoardNo, int lModuleNo, ref uint pdwInputSize, ref uint pdwOutputSize);
	
	/**
     * @brief Coupler Base ModuleNo의 Input/Output Size를 Bit 단위로 반환
     *
     * @param lBoardNo 보드 번호
     * @param lStationAddress 스테이션 주소
     * @param pdwInputSize PDO Input Size 저장
	 * @param pdwOutputSize PDO Output Size 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxCoupBaseECatReadModuleSizebyBit(int lBoardNo, int lStationAddress, ref uint pdwInputSize, ref uint pdwOutputSize);

	/**
     * @brief Analog ModuleNo의 Input/Output Size를 Byte 단위로 반환
     *
     * @param lBoardNo 보드 번호
     * @param lModuleNo 모듈 번호
     * @param pdwInputSize PDO Input Size 저장
	 * @param pdwOutputSize PDO Output Size 저장
	 *
	 * @note Input/Output Size가 Byte 단위가 아닐 경우 나머지를 제외한 몫만 반환
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxaECatReadModuleSizebyByte(int lBoardNo, int lModuleNo, ref uint pdwInputSize, ref uint pdwOutputSize);
	
	/**
     * @brief Digital ModuleNo의 Input/Output Size를 Byte 단위로 반환
     *
     * @param lBoardNo 보드 번호
     * @param lModuleNo 모듈 번호
     * @param pdwInputSize PDO Input Size 저장
	 * @param pdwOutputSize PDO Output Size 저장
	 *
	 * @note Input/Output Size가 Byte 단위가 아닐 경우 나머지를 제외한 몫만 반환
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxdECatReadModuleSizebyByte(int lBoardNo, int lModuleNo, ref uint pdwInputSize, ref uint pdwOutputSize);
	
	/**
     * @brief Coupler Base ModuleNo의 Input/Output Size를 Byte 단위로 반환
     *
     * @param lBoardNo 보드 번호
     * @param lStationAddress 스테이션 주소
     * @param pdwInputSize PDO Input Size 저장
	 * @param pdwOutputSize PDO Output Size 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxCoupBaseECatReadModuleSizebyByte(int lBoardNo, int lStationAddress, ref uint pdwInputSize, ref uint pdwOutputSize);

    /**
     * @brief COE를 이용하여 SDO(Service Data Objects) 읽음
     *
     * @param dwStationAddress 스테이션 주소
     * @param wObjectIndex Object 인덱스
     * @param byObjectSubIndex Object 서브 인덱스
     * @param pbyData 읽은 데이터 저장
     * @param dwDataLength 읽어 올 데이터 길이
     * @param pdwReadDataLength 실제로 읽은 데이터의 길이 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatReadSdo(uint dwStationAddress, ushort wObjectIndex, byte byObjectSubIndex, byte[] pbyData, uint dwDataLength, ref uint pdwReadDataLength);
	
	/**
	 * @ingroup EtherCAT
     * 
     * @brief COE를 이용하여 SDO(Service Data Objects) 읽음 (확장 버전)
     *
     * @param lBoardNo 보드 번호
     * @param dwStationAddress 스테이션 주소
     * @param wObjectIndex Object 인덱스
     * @param byObjectSubIndex Object 서브 인덱스
     * @param pbyData 읽은 데이터 저장
     * @param dwDataLength 읽어 올 데이터 길이
     * @param pdwReadDataLength 실제로 읽은 데이터의 길이 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatReadSdoEx(int lBoardNo, uint dwStationAddress, ushort wObjectIndex, byte byObjectSubIndex, byte[] pbyData, uint dwDataLength, ref uint pdwReadDataLength);

    /**
     * @brief COE를 이용하여 SDO(Service Data Objects)에 값 저장
     *
     * @param dwStationAddress 스테이션 주소
     * @param wObjectIndex Object 인덱스
     * @param byObjectSubIndex Object 서브 인덱스
     * @param pbyData 저장할 데이터
     * @param dwDataLength 저장 할 데이터 길이
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatWriteSdo(uint dwStationAddress, ushort wObjectIndex, byte byObjectSubIndex, byte[] pbyData, uint dwDataLength);
	
	/**
     * @brief COE를 이용하여 SDO(Service Data Objects)에 값 저장 (확장 버전)
     *
     * @param lBoardNo 보드 번호
     * @param dwStationAddress 스테이션 주소
     * @param wObjectIndex Object 인덱스
     * @param byObjectSubIndex Object 서브 인덱스
     * @param pbyData 저장할 데이터
     * @param dwDataLength 저장 할 데이터 길이
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatWriteSdoEx(int lBoardNo, uint dwStationAddress, ushort wObjectIndex, byte byObjectSubIndex, byte[] pbyData, uint dwDataLength);
    
    /**
     * @brief 축 번호를 통해 double Type의 SDO(Service Data Objects) 읽음
     *
     * @param lAxisNo 축 번호
     * @param wObjectIndex Object 인덱스
     * @param byObjectSubIndex Object 서브 인덱스
     * @param pdData 읽은 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatReadSdoFromAxisDouble(int lAxisNo, uint wObjectIndex, byte byObjectSubIndex, ref double pdData);

    /**
     * @brief 축 번호를 통해 double Type의 SDO(Service Data Objects)에 데이터 저장
     *
     * @param lAxisNo 축 번호
     * @param wObjectIndex Object 인덱스
     * @param byObjectSubIndex Object 서브 인덱스
     * @param pdData 저장 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatWriteSdoFromAxisDouble(int lAxisNo, uint wObjectIndex, byte byObjectSubIndex, ref double dData);
    
	/**
     * @brief 축 번호를 통해 DWORD Type의 SDO 읽음
     *
     * @param lAxisNo 축 번호
     * @param wObjectIndex Object 인덱스
     * @param byObjectSubIndex Object 서브 인덱스
     * @param pdwData 읽은 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatReadSdoFromAxisDword(int lAxisNo, ushort wObjectIndex, byte byObjectSubIndex, ref uint pdwData);

    /**
     * @brief 축 번호를 통해 DWORD Type의 SDO(Service Data Objects)에 데이터 저장
     *
     * @param lAxisNo 축 번호
     * @param wObjectIndex Object 인덱스
     * @param byObjectSubIndex Object 서브 인덱스
     * @param pdData 저장 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatWriteSdoFromAxisDword(int lAxisNo, ushort wObjectIndex, byte byObjectSubIndex, ref uint dwData);

    /**
     * @brief 축 번호를 통해 WORD Type의 SDO 읽음
     *
     * @param lAxisNo 축 번호
     * @param wObjectIndex Object 인덱스
     * @param byObjectSubIndex Object 서브 인덱스
     * @param pdwData 읽은 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatReadSdoFromAxisWord(int lAxisNo, ushort wObjectIndex, byte byObjectSubIndex, ref ushort pwData);
    
    /**
     * @brief 축 번호를 통해 WORD Type의 SDO(Service Data Objects)에 데이터 저장
     *
     * @param lAxisNo 축 번호
     * @param wObjectIndex Object 인덱스
     * @param byObjectSubIndex Object 서브 인덱스
     * @param pdData 저장 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatWriteSdoFromAxisWord(int lAxisNo, ushort wObjectIndex, byte byObjectSubIndex, ref ushort wData);
    
    /**
     * @brief 축 번호를 통해 BYTE Type의 SDO 읽음
     *
     * @param lAxisNo 축 번호
     * @param wObjectIndex Object 인덱스
     * @param byObjectSubIndex Object 서브 인덱스
     * @param pdwData 읽은 데이터 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatReadSdoFromAxisByte(int lAxisNo, ushort wObjectIndex, byte byObjectSubIndex, ref byte pbyData);

    /**
     * @brief 축 번호를 통해 BYTE Type의 SDO(Service Data Objects)에 데이터 저장
     *
     * @param lAxisNo 축 번호
     * @param wObjectIndex Object 인덱스
     * @param byObjectSubIndex Object 서브 인덱스
     * @param pdData 저장 데이터
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatWriteSdoFromAxisByte(int lAxisNo, ushort wObjectIndex, byte byObjectSubIndex, ref byte byData);

    /**
     * @brief EEPRom에 값 읽음
     *
     * @param dwStationAddress 스테이션 주소
     * @param wEEPRomStartOffset EEPRom 시작 오프셋
     * @param pwData 읽은 값 저장
     * @param dwDataLength 읽어 올 데이터 길이
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatReadEEPRom(uint dwStationAddress, ushort wEEPRomStartOffset, ushort[] pwData, uint dwDataLength);

    /**
     * @brief EEPRom에 값 쓰기
     *
     * @param dwStationAddress 스테이션 주소
     * @param wEEPRomStartOffset EEPRom 시작 오프셋
     * @param pwData 쓸 데이터
     * @param dwDataLength 쓸 데이터 길이
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatWriteEEPRom(uint dwStationAddress, ushort wEEPRomStartOffset, ushort[] pwData, uint dwDataLength);
    
    /**
     * @brief EEPRom의 값 읽음 (확장 버전)
     *
     * @param lBoardNo 보드 번호
     * @param dwStationAddress 스테이션 주소
     * @param wEEPRomStartOffset EEPRom 시작 오프셋
     * @param pwData 읽은 값 저장
     * @param dwDataLength 읽어올 데이터 길이
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatReadEEPRomEx(int lBoardNo, uint dwStationAddress, uint wEEPRomStartOffset, uint[] pwData, uint dwDataLength);

    /**
     * @brief EEPRom에 값 쓰기 (확장 버전)
     *
	 * @param lBoardNo 보드 번호
     * @param dwStationAddress 스테이션 주소
     * @param wEEPRomStartOffset EEPRom 시작 오프셋
     * @param pwData 쓸 데이터
     * @param dwDataLength 쓸 데이터 길이
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatWriteEEPRomEx(int lBoardNo, uint dwStationAddress, uint wEEPRomStartOffset, uint[] pwData, uint dwDataLength);

    /**
     * @brief Register의 값 읽음
     *
     * @param dwStationAddress 스테이션 주소
     * @param wRegisterOffset Register 오프셋
     * @param pvData 읽은 데이터 저장
     * @param wLen 읽어 올 데이터 길이
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatReadRegister(uint dwStationAddress, ushort wRegisterOffset, byte[] pvData, ushort wLen);
    
    /**
     * @brief Register의 값 쓰기
     *
     * @param dwStationAddress 스테이션 주소
     * @param wRegisterOffset Register 오프셋
     * @param pvData 쓸 데이터
     * @param wLen 쓸 데이터 길이
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatWriteRegister(uint dwStationAddress, ushort wRegisterOffset, byte[] pvData, ushort wLen);

    /**
     * @brief Register의 값 읽음
     *
     * @param lBoardNo 보드 번호
     * @param dwStationAddress 스테이션 주소
     * @param wRegisterOffset Register 오프셋
     * @param pvData 읽은 데이터 저장
     * @param wLen 읽어 올 데이터 길이
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatReadRegisterEx(uint lBoardNo, uint dwStationAddress, ushort wRegisterOffset, byte[] pvData, ushort wLen);

    /**
     * @brief Register의 값 쓰기
     *
     * @param lBoardNo 보드 번호
     * @param dwStationAddress 스테이션 주소
     * @param wRegisterOffset Register 오프셋
     * @param pvData 쓸 데이터
     * @param wLen 쓸 데이터 길이
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatWriteRegisterEx(uint lBoardNo, uint dwStationAddress, ushort wRegisterOffset, byte[] pvData, ushort wLen);

    /**
     * @brief EtherCAT Slave의 Object Dictionary 중 Backup Data를 파일로 저장
     *
     * @param dwStationAddress 스테이션 주소
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatSaveHotSwapData(uint dwStationAddress);

    /**
     * @brief 파일로 저장된 Backup Data를 해당 EtherCAT Slave로 로드
     *
     * @param dwStationAddress 스테이션 주소
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatLoadHotSwapData(uint dwStationAddress);
    
    /**
     * @brief HotSwap Start API 사용 시 필요한 HotSwapConfig에 StationAddress 저장
     *
     * @param dwStationAddress 스테이션 주소
	 *
	 * @note HotSwap Start API: 등록된 StationAddress들에 한해서 HotSwap을 진행하는 함수
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatSetHotSwap(uint dwStationAddress);
	
	/**
     * @brief HotSwapConfig에 해당 StationAddress가 존재하는지 확인
     *
     * @param dwStationAddress 스테이션 주소
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatIsSetHotSwap(uint dwStationAddress);
	
	/**
     * @brief HotSwapConfig에서 해당 StationAddress 삭제
     *
     * @param dwStationAddress 스테이션 주소
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatReSetHotSwap(uint dwStationAddress);
    
    /**
     * @brief EtherCAT Master의 Mode 설정 (0: ConfigMode, 1: RunMode)
     *
     * @param dwMasterMode Master Mode 설정 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatSetMasterMode(uint dwMasterMode);
    
    /**
     * @brief EhterCAT Master의 Mode 상태 확인
     *
     * @param pdMasterMode 가져온 Master Mode 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatGetMasterMode(ref uint pdMasterMode);
    
    /**
     * @brief EtherCAT Master의 Master Operation Mode 설정
     *
     * @param dwOperationMode Master Operation Mode 설정 값
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatSetMasterOperationMode(uint dwOperationMode);
    
    /**
     * @brief EtherCAT Master의 Master Operation Mode 설정 값 확인
     *
     * @param pdwOperationMode Master Operation Mode 설정 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatGetMasterOperationMode(ref uint pdwOperationMode);
    
    /**
     * @brief EtherCAT Master에 Scan 명령을 내리고 Scan된 Data를 SHM에 저장하도록 명령
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatRequestScanData();
    
    /**
     * @brief Scan된 Slave의 정보를 인덱스를 통해 가져 옴
     *
     * @param lIndex 가져올 Slave의 인덱스
     * @param dwpRevisionNumber Vendor ID 정보 저장
     * @param dwpProductCode Product Code 정보 저장
     * @param dwpRevisionNumber Revision Number 정보 저장
     * @param dwpSerialNumber Serial Number 정보 저장
     * @param dwpPhysAddress Physical Address 정보 저장
     * @param dwpAliasAddress Alias Address 정보 저장
	 *
	 * @details lIndex: 0 ~ Slave 연결 수 (물리적인 연결 순서대로 Index 번호 할당)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatGetSlaveInfoByIndex(int lIndex, ref uint dwpVendorID, ref uint dwpProductCode, ref uint dwpRevisionNumber, ref uint dwpSerialNumber, ref uint dwpPhysAddress, ref uint dwpAliasAddress);
	
    /**
     * @brief Scan된 Slave의 총 개수 반환
     *
     * @param pdwSlaveCount Slave 총 개수 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatGetScanSlaveCount(ref uint pdwSlaveCount);
    
    /**
     * @brief 현재 EtherCAT Master의 상태 반환
     *
     * @param pnECMasterStatus EtherCAT Master 상태 저장
     * @param pnECSlaveStatus EtherCAT Slave 상태 저장
     * @param pnECConnectedSlave 연결된 EtherCAT Slave의 개수 저장
     * @param pnECConfiguredSlave 설정된 EtherCAT Slave의 개수 저장
     * @param pnJobTaskCycleCnt Job Task Cycle Count 저장
     * @param pdwECMasterNotification EtherCAT Master의 알림 상태 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatGetStatus(ref int pnECMasterStatus, ref int pnECSlaveStatus, ref int pnECConnectedSlave, ref int pnECConfiguredSlave, ref int pnJobTaskCycleCnt, ref uint pdwECMasterNotification);

    /**
     * @brief 현재 EtherCAT Master의 상태 반환(HW Type 전용)
     *
     * @param lBoardNo Board 번호
     * @param pnECMasterStatus EtherCAT Master 상태 저장
     * @param pnECSlaveStatus EtherCAT Slave 상태 저장
     * @param pnECConnectedSlave 연결된 EtherCAT Slave의 개수 저장
     * @param pnECConfiguredSlave 설정된 EtherCAT Slave의 개수 저장     
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlECatGetStatusEx(int nBoardNo, ref int pnECMasterStatus, ref int pnECSlaveStatus, ref int pnECConnectedSlave, ref int pnECConfiguredSlave);

    /**
     * @brief 문제가 발생된 네트워크 재 연결
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlEcatReConnect();

    /**
     * @brief Motion Slave의 설정된 Address 정보 확인
     *
     * @param nAxisNo 축 번호
     * @param dwpStationAddress Station 주소 저장
     * @param npAutoIncAddress Auto Increment 주소 저장
     * @param dwpAliasAddress Alias 주소 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmECatReadAddress(int nAxisNo, ref uint dwpStationAddress, ref int npAutoIncAddress, ref uint dwpAliasAddress);
	
	/**
     * @brief Digital Slave의 설정된 Address 정보 확인
     *
     * @param nModuleNo 모듈 번호
     * @param dwpStationAddress Station 주소 저장
     * @param npAutoIncAddress Auto Increment 주소 저장
     * @param dwpAliasAddress Alias 주소 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdECatReadAddress(int nModuleNo, ref uint dwpStationAddress, ref int npAutoIncAddress, ref uint dwpAliasAddress);
	
	/**
     * @brief Analog Slave의 설정된 Address 정보 확인
     *
     * @param nModuleNo 모듈 번호
     * @param dwpStationAddress Station 주소 저장
     * @param npAutoIncAddress Auto Increment 주소 저장
     * @param dwpAliasAddress Alias 주소 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaECatReadAddress(int nModuleNo, ref uint dwpStationAddress, ref int npAutoIncAddress, ref uint dwpAliasAddress);
	
	/**
     * @brief Serial Slave의 설정된 Address 정보 확인
     *
     * @param lPortNo 포트 번호
     * @param dwpStationAddress Station 주소 저장
     * @param npAutoIncAddress Auto Increment 주소 저장
     * @param dwpAliasAddress Alias 주소 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxsECatReadAddress(int lPortNo, ref uint dwpStationAddress, ref int lpAutoIncAddress, ref uint dwpAliasAddress);

    /**
     * @brief EtherCAT Cycle Time 정보 확인
     *
     * @param nBoardNo 보드 번호
     * @param dwpCycleTime Cycle Time 정보 저장 (단위: uSec)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxlECatGetCycleTime(int nBoardNo, ref uint dwpCycleTime);

	/**
     * @brief Slave의 DC control error 정보 확인
     *
     * @param nBoardNo 보드 번호
	 * @param nNode 노드 번호
     * @param npDcCtrlError DC control error 정보 저장
	 *
	 * @author LeeSeokGi
	 * @date 2023.11.14
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlReadDcCtrlError(int nBoardNo, int nNode, ref int npDcCtrlError);

    // Monitor
	
    /**
     * @brief 데이터 수집을 진행 할 항목 추가
     *
     * @param nBoardNo 보드 번호
     * @param nItemIndex 항목 인덱스
     * @param dwSignalType 신호 타입
     * @param nSignalNo 신호 번호
     * @param nSubSignalNo 서브 신호 번호
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorSetItem(int nBoardNo, int nItemIndex, uint dwSignalType, int nSignalNo, int nSubSignalNo);
    
    /**
     * @brief 데이터 수집을 진행할 항목들에 대한 정보 반환
     *
     * @param nBoardNo 보드 번호
     * @param npItemSize 항목 크기 저장
     * @param npItemIndex 항목 인덱스 저장
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorGetIndexInfo(int nBoardNo, ref int npItemSize, int[] npItemIndex);
    
    /**
     * @brief 데이터 수집을 진행할 각 항목의 세부 설정 반환
     *
     * @param nBoardNo 보드 번호
     * @param nItemIndex 항목 인덱스
     * @param dwpSignalType 신호 타입 저장
     * @param npSignalNo 신호 번호 저장
     * @param npSubSignalNo 서브 신호 번호 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorGetItemInfo(int nBoardNo, int nItemIndex, ref uint dwpSignalType, ref int npSignalNo, ref int npSubSignalNo);
    
    /**
     * @brief 모든 데이터 수집 항목의 설정 초기화
     *
     * @param nBoardNo 보드 번호
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorResetAllItem(int nBoardNo);
    
    /**
     * @brief 선택된 데이터 수집 항목의 설정 초기화
     *
     * @param nBoardNo 보드 번호
     * @param nItemIndex 항목 인덱스
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorResetItem(int nBoardNo, int nItemIndex);
    
    /**
     * @brief 데이터 수집의 트리거 조건 설정
     *
     * @param nBoardNo 보드 번호
     * @param dwSignalType 신호 타입
     * @param nSignalNo 신호 번호
     * @param nSubSignalNo 서브 신호 번호
     * @param dwOperatorType 연산자 타입
     * @param dValue1 값 1
     * @param dValue2 값 2
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorSetTriggerOption(int nBoardNo, uint dwSignalType, int nSignalNo, int nSubSignalNo, uint dwOperatorType, double dValue1, double dValue2);
    
    /**
     * @brief 데이터 수집의 트리거 조건 확인
     *
     * @param nBoardNo 신호 타입 저장
     * @param lpSignalNo 신호 번호 저장
     * @param lpSubSignalNo 서브 신호 번호 저장
     * @param dwpOperatorType 연산자 타입 저장
     * @param dpValue1 값 1 저장
     * @param dpValue2 값 2 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    //[DllImport("AXL.dll")] public static extern uint AxlMonitorGetTriggerOption(ref uint dwpSignalType, ref int npSignalNo, ref int npSubSignalNo, ref uint dwpOperatorType, ref double dpValue1, ref double dpValue2);
    
    /**
     * @brief 데이터 수집의 트리거 조건 초기화
     *
     * @param lBoardNo 보드 번호
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorResetTriggerOption(int nBoardNo);
    
    /**
     * @brief 데이터 수집 시작
     *
     * @param nBoardNo 보드 번호
     * @param dwStartOption 시작 옵션
     * @param dwOverflowOption 오버 플로우 옵션
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorStart(int nBoardNo, uint dwStartOption, uint dwOverflowOption);
    
    /**
     * @brief 데이터 수집 정지
     *
     * @param nBoardNo 보드 번호
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorStop(int nBoardNo);
    
    /**
     * @brief 수집된 데이터 가져오기
     *
     * @param nBoardNo 보드 번호
     * @param npItemSize 항목 크기 저장
     * @param npDataCount 데이터 개수 저장
     * @param dpReadData 읽은 데이터 저장 배열
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorReadData(int nBoardNo, ref int npItemSize, ref int npDataCount, double[] dpReadData);
    
    /**
     * @brief 데이터 수집의 주기 확인
     *
     * @param nBoardNo 보드 번호
     * @param dwpPeriod 주기 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorReadPeriod(int nBoardNo, ref uint dwpPeriod);
    
    // MonitorEx
	
    /**
     * @brief 데이터 수집을 진행할 항목 추가
     *
     * @param lItemIndex 항목 인덱스
     * @param dwSignalType 신호 타입
     * @param lSignalNo 신호 번호
     * @param lSubSignalNo 서브 신호 번호
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorExSetItem(int lItemIndex, uint dwSignalType, int lSignalNo, int lSubSignalNo);

    /**
     * @brief 데이터 수집을 진행할 항목들에 관한 정보 반환
     *
     * @param lpItemSize 항목 크기 저장
     * @param lpItemIndex 항목 인덱스 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorExGetIndexInfo(ref int lpItemSize, ref int lpItemIndex);

    /**
     * @brief 데이터 수집을 진행할 각 항목의 세부 설정 가져오기
     *
     * @param lItemIndex 항목 인덱스
     * @param dwpSignalType 신호 타입 저장
     * @param lpSignalNo 신호 번호 저장
     * @param lpSubSignalNo 서브 신호 번호 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorExGetItemInfo(int lItemIndex, ref uint dwpSignalType, ref int lpSignalNo, ref int lpSubSignalNo);

    /**
     * @brief 모든 데이터 수집 항목의 설정 초기화
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorExResetAllItem();

    /**
     * @brief 선택된 데이터 수집 항목의 설정 초기화
     *
     * @param lItemIndex 항목 인덱스
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorExResetItem(int lItemIndex);

    /**
     * @brief 데이터 수집의 트리거 조건 설정
     *
     * @param dwSignalType 신호 타입
     * @param lSignalNo 신호 번호
     * @param lSubSignalNo 서브 신호 번호
     * @param dwOperatorType Operator 타입
     * @param dValue1 값 1
     * @param dValue2 값 2
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorExSetTriggerOption(uint dwSignalType, int lSignalNo, int lSubSignalNo, uint dwOperatorType, double dValue1, double dValue2);

    /**
     * @brief 데이터 수집의 트리거 설정 조건 확인
     *
     * @param dwSignalType 신호 타입 저장
     * @param lSignalNo 신호 번호 저장
     * @param lSubSignalNo 서브 신호 번호 저장
     * @param dwOperatorType Operator 타입 저장
     * @param dValue1 값 1 저장
     * @param dValue2 값 2 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    //[DllImport("AXL.dll")] public static extern uint AxlMonitorExGetTriggerOption(ref uint dwpSignalType, ref int lpSignalNo, ref int lpSubSignalNo, ref uint dwpOperatorType, ref double dpValue1, ref double dpValue2);

    /**
      * @brief 데이터 수집의 트리거 조건 초기화 (확장 버전)
      *
      * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
      */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorExResetTriggerOption();

    /**
     * @brief 데이터 수집 시작 (확장 버전)
     *
     * @param dwStartOption 시작 옵션
     * @param dwOverflowOption 오버 플로우 옵션
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorExStart(uint dwStartOption, uint dwOverflowOption);

    /**
     * @brief 데이터 수집 정지 (확장 버전)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorExStop();

    /**
     * @brief 수집된 데이터 가져오기 (확장 버전)
     *
     * @param lpItemSize 항목 크기 저장
     * @param lpDataCount 데이터 개수 저장
     * @param dpReadData 읽은 데이터 저장 배열
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorExReadData(ref int lpItemSize, ref int lpDataCount, double[] dpReadData);

    /**
     * @brief 데이터 수집의 주기 확인 (확장 버전)
     *
     * @param dwpPeriod 주기 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlMonitorExReadPeriod(ref uint dwpPeriod);

    /**
     * @brief X2, Y2 축에 대한 Offset 위치 정보를 포함한 2축 직선 보간 #01
     *
     * @param nCoordNo 좌표계 번호
     * @param dpEndPosition 종료 위치
     * @param dVelocity 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param dOffsetLength Offset 길이
     * @param dTotalLength 총 길이
     * @param dpStartOffsetPosition 시작 Offset 위치
     * @param dpEndOffsetPosition 종료 Offset 위치
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmLineMoveDual01(int nCoordNo, double[] dpEndPosition, double dVelocity, double dAccel, double dDecel, double dOffsetLength, double dTotalLength, double[] dpStartOffsetPosition, double[] dpEndOffsetPosition);
    
    /**
     * @brief X2, Y2 축에 대한 Offset 위치 정보를 포함한 2축 원호 보간 #01
     *
     * @param nCoordNo 좌표계 번호
     * @param npAxes 축 저장 배열
     * @param dpCenterPosition 중심 위치
     * @param dpEndPosition 종료 위치
     * @param dVelocity 속도
     * @param dAccel 가속도
     * @param dDecel 감속도
     * @param dwCWDir 구동 방향
     * @param dTotalLength 총 길이
     * @param dpStartOffsetPosition 시작 Offset 위치
     * @param dpEndOffsetPosition 종료 Offset 위치
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmCircleCenterMoveDual01(int nCoordNo, ref int npAxes, double[] dpCenterPosition, double[] dpEndPosition, double dVelocity, double dAccel, double dDecel, uint dwCWDir, double dTotalLength, double[] dpStartOffsetPosition, double[] dpEndOffsetPosition); 
    
    /**
     * @brief ECAT Digital Slave 펌웨어 업데이트를 위한 정보 설정
     *
     * @param nModuleNo 모듈 번호
     * @param dwTotalDataSize 전체 데이터 크기
     * @param dwTotalPacketSize 전체 패킷 크기
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdSetFirmwareUpdateInfo(int nModuleNo, uint dwTotalDataSize, uint dwTotalPacketSize);
	
	/**
     * @brief ECAT Digital Slave 펌웨어 업데이트를 위한 데이터 전송
     *
     * @param nModuleNo 모듈 번호
     * @param dwPacketIndex 패킷 인덱스
     * @param dwaPacketData 패킷 데이터 배열
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdSetFirmwareDataTrans(int nModuleNo, uint dwPacketIndex, uint[] dwaPacketData);
	
	/**
     * @brief ECAT Digital Slave 펌웨어 업데이트 시작
     *
     * @param nModuleNo 모듈 번호
     * @param szFileName 파일 이름
     * @param dwFileNameLen 파일 이름 길이
     * @param dwPassWord 암호
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxdSetFirmwareUpdate(int nModuleNo, char[] szFileName, uint dwFileNameLen, uint dwPassWord);
    [DllImport("AXL.dll")] public static extern uint AxdSetFirmwareUpdate(int nModuleNo, string szFileName, uint dwFileNameLen, uint dwPassWord);

	/**
     * @brief ECAT Analog Slave 펌웨어 업데이트를 위한 정보 설정
     *
     * @param nModuleNo 모듈 번호
     * @param dwTotalDataSize 전체 데이터 크기
     * @param dwTotalPacketSize 전체 패킷 크기
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaSetFirmwareUpdateInfo(int nModuleNo, uint dwTotalDataSize, uint dwTotalPacketSize);
	
	/**
     * @brief ECAT Analog Slave 펌웨어 업데이트를 위한 데이터 전송
     *
     * @param nModuleNo 모듈 번호
     * @param dwPacketIndex 패킷 인덱스
     * @param dwaPacketData 패킷 데이터 배열
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaSetFirmwareDataTrans(int nModuleNo, uint dwPacketIndex, ref uint dwaPacketData);
	
	/**
     * @brief ECAT Analog Slave 펌웨어 업데이트 시작
     *
     * @param nModuleNo 모듈 번호
     * @param szFileName 파일 이름
     * @param dwFileNameLen 파일 이름 길이
     * @param dwPassWord 암호
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxaSetFirmwareUpdate(int nModuleNo, char[] szFileName, uint dwFileNameLen, uint dwPassWord);
    [DllImport("AXL.dll")] public static extern uint AxaSetFirmwareUpdate(int nModuleNo, string szFileName, uint dwFileNameLen, uint dwPassWord);

	/**
     * @brief ECAT Motion Slave 펌웨어 업데이트를 위한 정보 설정
     *
     * @param nModuleNo 모듈 번호
     * @param dwTotalDataSize 전체 데이터 크기
     * @param dwTotalPacketSize 전체 패킷 크기
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetFirmwareUpdateInfo(int nAxisNo, uint dwTotalDataSize, uint dwTotalPacketSize);
	
	/**
     * @brief ECAT Motion Slave 펌웨어 업데이트를 위한 데이터 전송
     *
     * @param nModuleNo 모듈 번호
     * @param dwPacketIndex 패킷 인덱스
     * @param dwaPacketData 패킷 데이터 배열
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetFirmwareDataTrans(int nAxisNo, uint dwPacketIndex, ref uint dwaPacketData);
	
	/**
     * @brief ECAT Motion Slave 펌웨어 업데이트 시작
     *
     * @param nModuleNo 모듈 번호
     * @param szFileName 파일 이름
     * @param dwFileNameLen 파일 이름 길이
     * @param dwPassWord 암호
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetFirmwareUpdate(int nAxisNo, char[] szFileName, uint dwFileNameLen, uint dwPassWord);
    [DllImport("AXL.dll")] public static extern uint AxmSetFirmwareUpdate(int nAxisNo, string szFileName, uint dwFileNameLen, uint dwPassWord);
	
	/**
     * @brief ECAT Slave 파일 전송을 위한 정보 설정
     *
     * @param nAxisNo 축 번호
     * @param dwTotalDataSize 전체 데이터 크기
     * @param dwTotalPacketSize 전체 패킷 크기
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetFoeUploadInfo(int lAxisNo, uint dwTotalDataSize, uint dwTotalPacketSize);
	
	/**
     * @brief ECAT Slave 파일 전송을 위한 정보 설정 값 확인
     *
     * @param nAxisNo 축 번호
     * @param dwPacketIndex 패킷 인덱스
     * @param dwaPacketData 패킷 데이터 배열
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmGetFoeUploadData(int lAxisNo, uint dwPacketIndex, uint[] dwaPacketData);
	
	/**
     * @brief ECAT Slave 파일 전송 시작
     *
     * @param nAxisNo 축 번호
     * @param szFileName 파일 이름
     * @param dwFileNameLen 파일 이름 길이
     * @param dwPassWord 암호
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmSetFoeUpload(int lAxisNo, char[] szFileName, uint dwFileNameLen, uint dwPassWord);
    [DllImport("AXL.dll")] public static extern uint AxmSetFoeUpload(int lAxisNo, string szFileName, uint dwFileNameLen, uint dwPassWord);

    /**
     * @brief 모션의 동작 모드 설정
     *
     * @param nAxisNo 축 번호
     * @param dwOperationMode 동작 모드
	 *
	 * @details
	 * dwOperationMode
	 *    1: ProfilePosition Mode
	 *    2: Velocity Mode
	 *    3: ProfileVelocity Mode
	 *    4: ProfileTorque Mode
	 *    6: Homing Mode
	 *    7: InterpolatedPosition Mode
	 *    8: CyclicSyncPosition Mode
	 *    9: CyclicSyncVelocity Mode
	 *   10: CyclicSyncTorque Mode
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetOperationMode(int nAxisNo, uint dwOperationMode);
	
	/**
     * @brief 모션의 동작 모드 설정 값 확인
     *
     * @param nAxisNo 축 번호
     * @param dwOperationMode 동작 모드 값 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetOperationMode(int nAxisNo, ref uint pdwOperationMode);

    /**
     * @brief 카운터 모듈의 2-D 절대위치 트리거 기능을 위해 필요한 트리거 위치 정보 설정 (SIO-HPC4)
     *
     * @param lChannelNo 채널 번호
     * @param nDataCnt 데이터 개수
     * @param dwOption 옵션 (Reserved)
     * @param dpPatternData 패턴 데이터 배열 (X1, Y1)
	 *
	 * @details
	 * lChannelNo
	 *    0: Channel 0, 1 일 경우
	 *    2: Channel 2, 3 일 경우
	 * nDataCnt
	 *    > 0: 데이터 등록
	 *   <= 0: 등록된 데이터 초기화
	 *
	 * @note
   	 * [trigger mode == 0x04] : Range Trigger mode.[with fifo] 인 경우
	 *    case [dwOption == 0]
	 *       [dpPatternData] : nDataCnt * (X1 Position, Y1 Position) ....
	 * 	  case [dwOption == 1]
	 *	     [dpPatternData] : nDataCnt * (X1 Position, Y1 Position, trigger Cnt , frequency) ....
	 *		 
	 * [trigger mode  == 0x05] : Vector Trigger mode[with fifo] 인 경우
     *	  case [dwOption == 0]
	 *	     [dpPatternData] : nDataCnt * (X1 Position, Y1 Position, UnitVector X1 Position, UnitVector Y1 Position) ....
	 *	  case [dwOption == 1]
	 *	     [dpPatternData] : nDataCnt * (X1 Position, Y1 Position, UnitVector X1 Position, UnitVector Y1 Position, trigger Cnt , frequency) ....
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetPatternData(int lChannelNo, int nDataCnt, uint dwOption, double[] dpPatternData);
    
	/**
     * @brief 카운터 모듈의 2-D 절대위치 트리거 기능을 위해 필요한 트리거 위치 정보 설정 값 확인 (SIO-HPC4)
     *
     * @param lChannelNo 채널 번호
     * @param nDataCnt 데이터 개수 저장
     * @param dwOption 옵션 값 저장 (Reserved)
     * @param dpPatternData 패턴 데이터 배열 저장 (X1, Y1)
	 *
	 * @details
	 * lChannelNo
	 *    0: Channel 0, 1 일 경우
	 *    2: Channel 2, 3 일 경우
	 * nDataCnt
	 *    > 0: 데이터 등록
	 *   <= 0: 등록된 데이터 초기화
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerGetPatternData(int lChannelNo, ref int npDataCnt, ref uint dwpOption, double[] dpPatternData);

	/**
     * @brief 스파이럴 이동 수행
     *
     * @param lCoordNo 좌표계 번호
     * @param dSpiralPitch 스파이럴 피치
     * @param dTurningCount 회전 횟수
     * @param dAngleOfPose 자세 각도
     * @param dwIsInnerDirection 내부 방향 여부
     * @param dVelocity 속도
     * @param dAcceleration 가속도
     * @param dDeceleration 감속도
	 *
	 * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmSpiralMoveEx(int lCoordNo, double dSpiralPitch, double dTurningCount, double dAngleOfPose, uint dwIsInnerDirection, double dVelocity, double dAcceleration, double dDeceleration);
	
	/**
     * @brief 필렛 이동 수행
     *
     * @param lCoord 좌표계 번호
     * @param dPosition 위치 배열
     * @param dFirstVector 첫 번째 벡터 배열
     * @param dSecondVector 두 번째 벡터 배열
     * @param dMaxVelocity 최대 속도
     * @param dMaxAccel 최대 가속도
     * @param dMaxDecel 최대 감속도
     * @param dRadius 반지름
	 *
	 * @warning 해당 기능은 유료 서비스입니다. 라이선스 구매 후 사용 가능합니다.
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxmFilletMove(int lCoord, double[] dPosition, double[] dFirstVector, double[] dSecondVector, double dMaxVelocity, double dMaxAccel, double dMaxDecel, double dRadius);

    // Combined 구동 함수 (PCIe-RxxIF-ECAT 전용 함수)
    
	/**
     * @brief 입력한 다수의 목표점을 차례로 직선과 원호를 조합하여 등속 보간 구동
     *
     * @param nCoordNo 좌표계 번호
     * @param nBaseAxisSize 기준 축 크기
     * @param upBaseAxisNo 기준 축 번호 배열
     * @param nPosSize 목표점 개수
     * @param dpPos 목표점 배열
     * @param dVel 등속으로 구동할 목표 벡터 속도
     * @param dAccel 최대 가속도
     * @param dDecel 최대 감속도
     * @param dRadius 원호 보간을 수행할 반지름 크기
	 *
	 * @details
	 * nBaseAxisSize: 2 ~ 3 입력 가능
	 * nPosSize: 최소 2개 이상 입력 필요, MAX: 256(좌표계 축 개수 * 목표점 개수)
	 * dpPos: 배열의 크기는 (좌표계 축 개수 * 목표점 개수) 보다 작을 경우 메모리 참조 에러가 발생할 수 있음
	 * dRadius: 매개 변수를 입력하지 않거나 0 이하의 값을 입력하면 자동으로 반지름 크기를 계산 함
	 *
	 * @note
	 * 단독으로 사용하거나 AxmConti 계열 보간 함수와 함께 조합하여 사용.
	 * 원호 보간을 2 ~ 3차원만 계산할 수 있기 때문에 기준 축(Base axis)을 2 ~ 3개까지 입력할 수 있음.
	 * 기준 축을 포함하여 최대 16축까지 동기 구동에 참여 할 수 있음.
	 * 기준 축은 두 벡터를 내각으로 원호 보간하기 때문에 지정한 목표점을 통과하지 않으나 그 외의 축은 지정한 목표점을 통과 함.
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmContiCombinedMove(int lCoordNo, int lBaseAxisSize, ref int lpBaseAxisNo, int lPosSize, double[] dpPos, double dVel, double dAccel, double dDecel, double dRadius = 0.0);

	/**
     * @brief 지정 축의 Servo-On 신호의 출력 상태 반환
     *
     * @param nAxisNo 축 번호
     * @param upOnOff Servo-On 상태 값 저장
	 *
	 * @note Gantry 구동 상태라도 오직 지정 축의 출력 상태만 반환
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalIsServoOnSingleAxis(int nAxisNo, ref uint upOnOff);
    
    // EtherCAT 전용 함수
	
	/**
     * @brief 지정 축의 ModeOfOperation을 CST Mode로 변경한 후 Source 축의 Actual Torque 값을 지정 축의 Target Torque 값 설정
     *
     * @param nChannelNo 축 번호
     * @param dwLimit Source 소스 축 번호
     * @param dwEnable 설정 여부 (0: DISABLE, 1: ENABLE)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxmMotSetTorqueConnection(int nAxisNo, int nSourceAxisNo, uint uEnable);
	
	/**
     * @brief 지정 축의 ModeOfOperation을 CST Mode로 변경한 후 Source 축의 Actual Torque 값을 지정 축의 Target Torque 값 설정 정보 확인
     *
     * @param nChannelNo 축 번호
     * @param dwLimit Source 소스 축 번호 저장
     * @param dwEnable 설정 여부 정보 저장 (0: DISABLE, 1: ENABLE)
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxmMotGetTorqueConnection(int nAxisNo, ref int npSourceAxisNo, ref uint upEnable);

    /**
     * @brief 지정 축의 최대 토크 값 설정
     *
     * @param lAxisNo          축 번호
     * @param dMaxTorque       모터의 최대 토크 설정치 (Units : %, Range 0.0% ~ 6553.5%)
     *
     * @details
     * EtherCAT 전용 함수(PCIe-RxxIF-ECAT)
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetMaxTorqueECat(int lAxisNo, double dMaxTorque);

    /**
     * @brief 지정 축의 최대 토크 값 반환
     *
     * @param lAxisNo          축 번호
     * @param dpMaxTorque      모터의 최대 토크 설정 반환 값 (Units : %, Range 0.0% ~ 6553.5%)
     *
     * @details
     * EtherCAT 전용 함수(PCIe-RxxIF-ECAT)
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetMaxTorqueECat(int lAxisNo, ref double dpMaxTorque);

    /**
     * @brief 지정 축의 최대 모터 최대 속도 값 설정
     *
     * @param lAxisNo			 축 번호
     * @param dwMaxMotorSpeed    모터의 최대 속도 설정치 (Units : r/min, Range 0 ~ 4294967295)
     *
     * @details
     * EtherCAT 전용 함수(PCIe-RxxIF-ECAT)
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotSetMaxMotorSpeedECat(int lAxisNo, uint dwMaxMotorSpeed);

    /**
     * @brief 지정 축의 모터 최대 속도 값 반환
     *
     * @param lAxisNo			 축 번호
     * @param dwMaxMotorSpeed    모터의 최대 속도 설정 반환값 (Units : r/min, Range 0 ~ 4294967295)
     *
     * @details
     * EtherCAT 전용 함수(PCIe-RxxIF-ECAT)
     * 
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMotGetMaxMotorSpeedECat(int lAxisNo, ref uint dwpMaxMotorSpeed);

    /**
     * @brief 설정한 토크 및 속도 값으로 모터 구동
     *
     * @param lAxisNo          구동할 축의 축 번호
     * @param dTargetTorque    목표 출력 토크 값 (Units : %, Range : -3276.8% ~ 3276.7%)
     *
     * @details
     * EtherCAT 전용 함수(PCIe-RxxIF-ECAT)	 
     * 해당 함수 구동 전 Max Torque, Target Torque, Max Motor Speed는 반드시 PDO에 Mapping되어 있어야 함
     * Operation Modes가 CSP -> CST로 변경되며, 기존에 설정되어 있던 PDO의 Max Torque와 Max Motor Speed로 구동
     * PDO의 Max Motor Speed가 0인 경우 구동하지 않음
     *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveStartTorqueECat(int lAxisNo, double dTargetTorque);

    /**
     * @brief 지정 축의 토크 구동 정지
     *
     * @param nAxisNo 구동 정지할 축 번호
	 *
	 * @details
	 * EtherCAT 전용 함수(PCIe-RxxIF-ECAT)	 	 
	 *
     * @return 함수 호출 성공 시 0 반환, 실패 시 오류 코드 반환
     */
    [DllImport("AXL.dll")] public static extern uint AxmMoveTorqueStopECat(int lAxisNo);


    // Used for CNT_RECAT_SC_10

    /**
     * @brief 트리거 출력 구간 유무 설정 함수
     *
     * @param nChannelNo 채널 번호
     * @param dwLimit 출력 구간 유무 설정 값
	 *
	 * @details
	 * dwLimit
	 *    0: 구간에 상관없이 Trigger 출력 유지
	 *    1: 설정한 츨력 하한 값과 상한 값 사이에서만 출력 유지
	 *
	 * @note Trigger Mode 가 CCGC_CNT_POSITION_ON_OFF_TRIGGER로 설정 되어 있을 경우 유효
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetOnOffTriggerUseLimit(int nChannelNo, uint dwLimit);
	
	/**
     * @brief 트리거 출력 구간 유무 설정 정보 확인
     *
     * @param nChannelNo 채널 번호
     * @param dwLimit 출력 구간 유무 설정 값 저장
	 *
	 * @details
	 * dwLimit
	 *    0: 구간에 상관없이 Trigger 출력 유지
	 *    1: 설정한 츨력 하한 값과 상한 값 사이에서만 출력 유지
	 *
	 * @note Trigger Mode 가 CCGC_CNT_POSITION_ON_OFF_TRIGGER로 설정 되어 있을 경우 유효
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetOnOffTriggerUseLimit(int nChannelNo, ref uint dwLimit);
	
	/**
     * @brief 트리거 출력 구간 유무 설정 함수
     *
     * @param nModuleNo 모듈 번호
     * @param nTablePos 테이블 위치
     * @param dwLimit 출력 구간 유무 설정 값
	 *
	 * @details
	 * dwLimit
	 *    0: 구간에 상관없이 Trigger 출력 유지
	 *    1: 설정한 츨력 하한 값과 상한 값 사이에서만 출력 유지
	 *
	 * @note Trigger Mode 가 CCGC_CNT_POSITION_ON_OFF_TRIGGER로 설정 되어 있을 경우 유효
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxcTableSetOnOffTriggerUseLimit(int nModuleNo, int nTablePos, uint dwLimit);
	
	/**
     * @brief 트리거 출력 구간 유무 설정 정보 확인
     *
     * @param nModuleNo 모듈 번호
     * @param nTablePos 테이블 위치
     * @param dwLimit 출력 구간 유무 설정 값 저장
	 *
	 * @details
	 * dwLimit
	 *    0: 구간에 상관없이 Trigger 출력 유지
	 *    1: 설정한 츨력 하한 값과 상한 값 사이에서만 출력 유지
	 *
	 * @note Trigger Mode 가 CCGC_CNT_POSITION_ON_OFF_TRIGGER로 설정 되어 있을 경우 유효
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
	[DllImport("AXL.dll")] public static extern uint AxcTableGetOnOffTriggerUseLimit(int nModuleNo, int nTablePos, ref uint dwLimit);
	
	/**
     * @brief External device (Modbus TCP/IP 등) 스캔
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlRescanExternalDevice(); 
	
	/**
     * @brief External device (Modbus TCP/IP 등) 정보 반환
     *
     * @param nBoardNo 보드 번호
     * @param devInfo 외부 장치 정보 저장
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxlGetExternalDeviceInfo(int lBoardNo, object devInfo);

    /**
     * @brief Limit 신호 감지 후 초과 구동하여 Limit 신호를 벗어난 경우 Limit이 걸린 것 처럼 동작 설정
     *
     * @param lAxisNo 축 번호
     * @param bHold Limit 걸린 것 처럼 동작 유뮤 설정 (TRUE: 동작 설정, FALSE: 동작 미설정)
     *
     * @note ECAT 05 Type에만 우선 적용 (2024.12.20)
     *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetLimitHold(int lAxisNo, bool bHold);
}
