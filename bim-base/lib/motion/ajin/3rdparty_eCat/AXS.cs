/**
 * @file AXS.cs
 * 
 * @brief 아진엑스텍 Serial 라이브러리 헤더 파일
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

using System;
using System.Runtime.InteropServices;

public class CAXS
{
    // 보드 및 모듈 확인 함수 (Information)
	
    /**
     * @brief 해당 포트의 보드 번호, 모듈 위치, 모듈 ID 반환
     * 
     * @param nPortNo 포트 번호
	 * @param npBoardNo 보드 번호 저장 
	 * @param npModulePos 모듈 위치 저장
	 * @param upModuleID 모듈 ID 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxsInfoGetPort(int nPortNo, ref int npBoardNo, ref int npModulePos, ref uint upModuleID);
	
	/**
     * @brief 지정한 모듈 번호로 해당 모듈의 Sub ID, 모듈 Name, 모듈 설명 확인 (지원 제품: EtherCAT)
     * 
     * @param nPortNo 포트 번호
	 * @param upModuleSubID EtherCAT 모듈을 구분하기 위한 SubID
	 * @param szModuleName 모듈 모델명(50 Byte)
	 * @param szModuleDescription 모듈 설명(80 Byte)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxsInfoGetPortEx(int nPortNo, ref uint upModuleSubID, System.Text.StringBuilder szModuleName, System.Text.StringBuilder szModuleDescription);

    /**
     * @brief 시리얼 모듈 존재 여부 반환
     * 
     * @param upStatus (0: 없음, 1: 있음)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxsInfoIsSerialModule(ref uint upStatus);
    
	/**
     * @brief 해당 포트가 유효한지 반환
     * 
     * @param nPortNo 포트 번호
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxsInfoIsInvalidPortNo(int nPortNo);
    
	/**
     * @brief 해당 포트가 제어 가능 상태인지 반환
     * 
     * @param nPortNo 포트 번호
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxsInfoGetPortStatus(int nPortNo);
    
	/**
     * @brief 시스템 내 유효한 통신 포트 개수 반환
     * 
     * @param npPortCount 포트 개수 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxsInfoGetPortCount(ref int npPortCount);
    
	/**
     * @brief 해당 보드/모듈의 첫번째 포트 번호 반환
     * 
     * @param nPortNo 보드 번호
	 * @param nModulePos 모듈 위치
	 * @param npPortNo 포트 번호 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxsInfoGetFirstPortNo(int nBoardNo, int nModulePos, ref int npPortNo);
    
	/**
     * @brief 해당 보드의 첫번째 통신 포트 번호 반환
     * 
     * @param nPortNo 보드 번호
	 * @param nModulePos 모듈 위치
	 * @param npPortNo 포트 번호 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxsInfoGetBoardFirstPortNo(int nBoardNo, int nModulePos, ref int lpPortNo);
    
	// 시리얼 통신함수(Port)
    
	/**
     * @brief 통신 포트 Open. AxsPortOpen은 하나의 응용프로그램에서만 할 수 있음
     * 
     * @param nPortNo 포트 번호
	 * @param nBaudRate Baud Rate (300, 600, 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200)
	 * @param nDataBits 데이터 비트 (7, 8)
	 * @param nStopBits 정지 비트 (1, 2)
	 * @param nParity 패리티 (0: None, 1: Even, 2: Odd)
	 * @param dwFlagsAndAttributes Reserved
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxsPortOpen(int nPortNo, int nBaudRate, int nDataBits, int nStopBits, int nParity, uint dwFlagsAndAttributes);
    
	/**
     * @brief 통신 포트 Close
     * 
     * @param nPortNo 포트 번호
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxsPortClose(int nPortNo);
     
	/**
     * @brief 통신 포트 설정
     * 
     * @param nPortNo 포트 번호
	 * @param lpDCB 통신 포트 설정 구조체
	 *
	 * @details
	 * lpDCB
	 *    lpDCB->BaudRate Baud Rate (300, 600, 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200)
	 *    lpDCB->ByteSize 데이터 비트 (7, 8)
	 *    lpDCB->StopBits 정지 비트 (1, 2)
	 *    lpDCB->Parity 패리티 (0: None, 1: Even, 2: Odd)
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("kernel32.dll")] public static extern uint AxsPortSetCommState(int nPortNo, ref DCB lpDCB);
    
	/**
     * @brief 통신 포트 설정값 확인
     * 
     * @param nPortNo 포트 번호
	 * @param lpDCB 통신 포트 설정 구조체 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("kernel32.dll")] public static extern uint AxsPortGetCommState(int nPortNo, ref DCB lpDCB);
    
	/**
     * @brief 통신 포트의 타임아웃 값 설정
     * 
     * @param nPortNo 포트 번호
	 * @param lpCommTimeouts 타임아웃 값
	 *
	 * @details
	 * lpCommTimeouts (단위: mSec)
	 *    lpCommTimeouts->ReadIntervalTimeout          : 문자열 입력이 시작된 후 문자열간 Timeout 시간 설정
	 *    lpCommTimeouts->ReadTotalTimeoutMultiplier   : 읽기 동작 시 설정한 통신 속도에서 하나의 문자열에 대한 Timeout 시간 설정
	 *    lpCommTimeouts->ReadTotalTimeoutConstant     : 입력 받을 문자수에 대한 Timeout을 제외한 Timeout 시간 설정
	 *    lpCommTimeouts->WriteTotalTimeoutMultiplier  : 쓰기 동작 시 설정한 통신 속도에서 하나의 문자열에 대한 Timeout 시간 설정
	 *    lpCommTimeouts->WriteTotalTimeoutConstant    : 전송할 문자수에 대한 Timeout을 제외한 Timeout 시간 설정
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("kernel32.dll")] public static extern uint AxsPortSetCommTimeouts(int nPortNo, ref COMMTIMEOUTS lpCommTimeouts);
    
	/**
     * @brief 통신 포트의 타임아웃 값 설정 확인
     * 
     * @param nPortNo 포트 번호
	 * @param lpCommTimeouts 타임아웃 값 저장
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("kernel32.dll")] public static extern uint AxsPortGetCommTimeouts(int nPortNo, ref COMMTIMEOUTS lpCommTimeouts);
    
	/**
     * @brief 장치의 오류 Flag를 지우거나 송수신 된 데이타의 개수 확인
     * 
     * @param nPortNo 포트 번호
	 * @param lpErrors 오류 정보 저장
	 * @param lpStat 데이터 개수 저장
	 *
	 * @details
	 * lpErrors
	 *    [1]CE_RXOVER:     수신 버퍼 Overflow 발생
	 *    [2]CE_OVERRUN:    수신 버퍼 Overrun 발생
	 *    [4]CE_RXPARITY:   수신 데이타 패리티비트 에러발생
	 *    [8]CE_FRAME:      수신 Framing 에러발생
	 * lpStat
	 *    lpStat->cbInQue : 수신버퍼에 입력된 데이타 갯수
	 *    lpStat->cbOutQue: 송신버퍼에 남은 데이타 갯수
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("kernel32.dll")] public static extern uint AxsPortClearCommError(int nPortNo, out uint lpErrors, ref COMSTAT lpStat);
    
	/**
     * @brief 데이타 송신 멈춤
     * 
     * @param nPortNo 포트 번호
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxsPortSetCommBreak(int nPortNo);
    
	/**
     * @brief 데이타 송신 재개
     * 
     * @param nPortNo 포트 번호
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxsPortClearCommBreak(int nPortNo);	    
    
	/**
     * @brief 송수신 정지 or 버퍼 지움
     * 
     * @param nPortNo 포트 번호
	 * @param dwFlags 설정 값
	 *
	 * @details
	 * dwFlags
	 *    [1]PURGE_TXABORT: 쓰기 작업 정지
	 *    [2]PURGE_RXABORT: 읽기 작업 정지
	 *    [4]PURGE_TXCLEAR: 송신 버퍼에 데이타 있을 경우 지움
	 *    [8]PURGE_RXCLEAR: 수신 버퍼에 데이타 있을 경우 지움
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxsPortPurgeComm(int nPortNo, uint dwFlags);
    
	/**
     * @brief 시리얼 포트에 데이타 쓰기
     * 
     * @param nPortNo 포트 번호
	 * @param lpBuffer 장치에 쓸 데이터를 담는 버퍼의 포인트값
	 * @param nNumberOfBytesToWrite lpBuffer에 담긴 실제 데이터의 바이트 수
	 * @param lpNumberOfBytesWritten 실제로 쓰여진 바이트 수 반환 (None Overrapped 일경우)
	 * @param lpOverlapped 비동기를 위한 OVERLAPPED 구조체의 포인트 값
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("kernel32.dll")] public static extern uint AxsPortWriteFile(int nPortNo, IntPtr lpBuffer, uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten, ref OVERLAPPED lpOverlapped);
    
	/**
     * @brief 시리얼 포트의 데이터 읽음
     * 
     * @param nPortNo 포트 번호
	 * @param lpBuffer 장치에 쓸 데이터를 담는 버퍼의 포인트값
	 * @param nNumberOfBytesToRead lpBuffer가 가리키는 버퍼의 크기를 바이트로 지정
	 * @param lpNumberOfBytesRead 실제로 읽혀진 바이트 수 반환(None Overrapped 일경우)
	 * @param lpOverlapped 비동기를 위한 OVERLAPPED 구조체의 포인트 값
     * 
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("kernel32.dll")] public static extern uint AxsPortReadFile(int nPortNo, IntPtr lpBuffer, uint nNumberOfBytesToRead, out uint lpNumberOfBytesRead, ref OVERLAPPED lpOverlapped);

    /**
     * @brief 시리얼 포트 Overlapped 작업 결과 반환
     * 
     * @param nPortNo 포트 번호
	 * @param lpOverlapped 비동기를 위한 OVERLAPPED 구조체의 포인트 값
	 * @param lpNumberOfBytesTransferred 실제 전송이 완료된 바이트 크기를 얻기 위한 변수 포인트
	 * @param bWait I/O 연산이 끝나지않은 상황에서의 처리를 결정
     * 
	 * @details
	 * lpOverlapped->hEvent: 전송이 완료된 후 시그널 될 이벤트 핸들. AxsPortWriteFile, AxsPortReadFile 함수를 사용하기전에 이 값을 설정.
	 * bWait
	 *    [0]: I/O 연산이 끝날때까지 기다림
	 *    [1]: I/O 연산이 끝나지 않아도 반환함
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxsPortGetOverlappedResult(int nPortNo, ref OVERLAPPED lpOverlapped, out uint lpNumberOfBytesTransferred, bool bWait);
    
    /**
     * @brief 시리얼 포트에 발생한 최종 에러 코드 반환
     * 
     * @param nPortNo 포트 번호
	 * @param dwpErrCode 에러 코드 저장
     * 
	 * @details
	 * dwpErrCode
	 *    [  0]ERROR_SUCCESS          : 에러 없음
	 *    [  5]ERROR_ACCESS_DENIED    : 통신 포트가 사용중 일 경우
	 *    [  6]ERROR_INVALID_HANDLE   : 통신 포트가 유효하지 않을 경우 (네트워크 연결 오류 포함)
	 *    [ 87]ERROR_INVALID_PARAMETER: 잘못된 파라메타 설정   
	 *    [995]ERROR_OPERATION_ABORTED: The I/O operation has been aborted because of either a thread exit or an application request.
	 *    [996]ERROR_IO_INCOMPLETE    : Overrapped 모드일 때 쓰기 작업이 끝나지 않은 경우나 Timeout이 발생한 경우
	 *    [997]ERROR_IO_PENDING       : Overrapped 모드일 때 I/O 작업이 진행 중인 경우
	 *    [998]ERROR_NOACCESS
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxsPortGetLastError(int nPortNo, ref uint dwpErrCode);
	
	/**
     * @brief 시리얼 포트에 발생한 최종 에러 코드 설정
     * 
     * @param nPortNo 포트 번호
	 * @param dwpErrCode 에러 코드
	 *
     * @return 함수 호출 성공 시 0을 반환하며, 실패 시 오류 코드를 반환합니다.
     */
    [DllImport("AXL.dll")] public static extern uint AxsPortSetLastError(int nPortNo, uint dwErrCode);
    
}
