//#define USE_CCLINK

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;


#if USE_CCLINK
using MelsecLibrary;
using cclink = MelsecLibrary.Melsec;

namespace lib.plc
{
    public class MelsecCCLink : IPLC
    {
        // 초기화 시 반드시 해당 변수의 값은 맞춰주어야 한다.
        // ChannelNo - PC 상에서 세팅된 채널 No
        // NetworkNo - PLC 와 PC 가 사용하는 NetworkNo
        // StationNo - 255 가 자기자신.
        public short ChannelNo { get; set; }
        public short NetworkNo { get; set; }
        public short StationNo { get; set; }

        bool m_debug = false; // 디버그 변수, 디버깅이 필요할때에만 true 하여 console 출력 명령어를 본다.
        bool m_connect = false;

        int m_path = 0;

        bool m_simulation = false;

        public MelsecCCLink()
        {
            if (File.Exists(Common.PATH + "\\simulation"))
            {
                m_simulation = true;
            }
        }

        public bool open()
        {
            close();

            Debug.debug("MelsecCCLink::open networkNo:" + NetworkNo + " StationNo:" + StationNo + " ChannelNo:" + ChannelNo);

            try
            {
                int ret = cclink.mdOpen(ChannelNo, -1, ref m_path);

                if (ret == 0)
                {
                    m_connect = true;
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.debug("MelsecCCLink::open failed. reason:" + e.Message);
            }

            return false;
        }

        public bool close()
        {
            Debug.debug("MelsecCCLink::close channelNo:" + ChannelNo);

            try
            {
                short ret = cclink.mdClose(ChannelNo);

                if (ret == 0)
                {
                    m_connect = false;
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.debug("MelsecCCLink::close close failed. reason:" + e.Message);
            }

            return false;
        }

        public bool isConnected()
        {
            return m_connect;
        }

        /// <summary>
        /// B 영역은 한번에 최대 512개 까지 밖에 못 일어옵
        /// 512 / 16 = 32 Byte
        /// </summary>
        /// <returns></returns>
        bool read34Byte(int address, int count, ref int[] buffer, int offset)
        {
            int devCount = 1;

            int[] dev = new int[devCount * 4 + 1];

            dev[0] = devCount; // 3개의 인자 반복 count
            dev[1] = Melsec.DevB;
            dev[2] = address;

            if (count > 512)
                count = 512;

            dev[3] = count;

            int ret = 0;
            ret = cclink.mdRandREx(ChannelNo, NetworkNo, StationNo, 
                ref dev[0], ref buffer[offset], count);

            if (ret == 0)
                return true;

            return false;
        }

        public bool read(Addr type, int address, int count, ref int[] buffer)
        {
            if (m_simulation)
                return true;

#if false
            if (type == Addr.B)
            {
                int loopCount = count / 512;

                bool result = true;

                for (int i=0; i<loopCount; i++)
                {
                    result &= read34Byte(address + (i*512), count, ref buffer, i * 32);
                }

                return result;
            }
#endif

            int devCount = 1;

            int[] dev = new int[devCount * 4 + 1];

            dev[0] = devCount; // 3개의 인자 반복 count

            switch (type)
            {
                case Addr.D: dev[1] = Melsec.DevD; break;
                case Addr.W: dev[1] = Melsec.DevW; break;
                case Addr.B: dev[1] = Melsec.DevB; break;
                case Addr.X: dev[1] = Melsec.DevX; break;
                case Addr.Y: dev[1] += Melsec.DevY; break;
                default:
                    throw new ArgumentOutOfRangeException("지원하지 않는 타입입니다.");
            }

            dev[2] = address;
            dev[3] = count * 4;

            int ret = 0;
            ret = cclink.mdReceiveEx(ChannelNo, 0, StationNo, dev[1], dev[2], ref dev[3], ref buffer[0]);

            if (ret == 0)
                return true;

            return false;
        }

        /// <summary>
        /// 다른 Station 접속용
        /// </summary>
        public bool readEx(Addr type, int address, int count, ref int[] buffer, int networkNo, int stationNo)
        {
            int devCount = 1;

            int[] dev = new int[devCount * 4 + 1];

            dev[0] = devCount; // 3개의 인자 반복 count

            switch (type)
            {
                case Addr.D: dev[1] = Melsec.DevD; break;
                case Addr.W: dev[1] = Melsec.DevW; break;
                case Addr.B: dev[1] = Melsec.DevB; break;
                case Addr.X: dev[1] = Melsec.DevX; break;
                case Addr.Y: dev[1] += Melsec.DevY; break;
                default:
                    throw new ArgumentOutOfRangeException("지원하지 않는 타입입니다.");
            }

            dev[2] = address;
            dev[3] = count;

            int ret = 0;
            ret = cclink.mdRandREx(ChannelNo, 0, stationNo, ref dev[0], ref buffer[0], buffer.Length);

            if (ret == 0)
                return true;

            return true;
        }

        public bool write(Addr type, int address, int count, int[] value)
        {
            int devCount = 1;

            int[] dev = new int[devCount * 4 + 1];

            dev[0] = devCount;

            switch (type)
            {
                case Addr.D: dev[1] = Melsec.DevD; break;
                case Addr.W: dev[1] = Melsec.DevW; break;
                case Addr.B: dev[1] = Melsec.DevB; break;
                case Addr.X: dev[1] = Melsec.DevX; break;
                case Addr.Y: dev[1] += Melsec.DevY; break;
                default:
                    throw new ArgumentOutOfRangeException("지원하지 않는 타입입니다.");
            }

            dev[2] = address;
            dev[3] = count;

            if (dev[3] % 2 != 0)
                dev[3]++;

            int ret = 0;

            if (type == Addr.B)
            {
                ret = cclink.mdSendEx(ChannelNo, 0, StationNo, dev[1], dev[2], ref dev[3], ref value[0]);
            }
            if (type == Addr.W)
            {
                int[] sendBuff = new int[value.Length / 2];

                for (int i = 0; i < sendBuff.Length; i++)
                {
                    int low = value[i * 2];
                    int high = (i * 2 + 1 < value.Length) ? value[i * 2 + 1] : 0;

                    sendBuff[i] = (high << 16) | (low & 0xFFFF);
                }

                ret = cclink.mdSendEx(ChannelNo, 0, StationNo, dev[1], dev[2], ref dev[3], ref sendBuff[0]);
            }

            if (ret == 0)
                return true;

            return false;
        }

        /// <summary>
        /// 다른 STATION 접속 용
        /// </summary>
        public bool writeEx(Addr type, int address, int count, int[] value, int networkNo, int stationNo)
        {
            int devCount = 1;

            int[] dev = new int[devCount * 4 + 1];

            dev[0] = devCount;

            switch (type)
            {
                case Addr.D: dev[1] = Melsec.DevD; break;
                case Addr.W: dev[1] = Melsec.DevW; break;
                case Addr.B: dev[1] = Melsec.DevB; break;
                case Addr.X: dev[1] = Melsec.DevX; break;
                case Addr.Y: dev[1] += Melsec.DevY; break;
                default:
                    throw new ArgumentOutOfRangeException("지원하지 않는 타입입니다.");
            }

            dev[2] = address;
            dev[3] = count;

            int ret = 0;

            ret = cclink.mdRandWEx(ChannelNo, 0, stationNo, ref dev[0], ref value[0], value.Length);

            //ret = cclink.mdSendEx(m_path, networkNo, stationNo, dev[1], dev[2], ref count, ref value[0]);
            if (ret == 0)
                return true;

            return false;
        }

        private void debug(string funcName, string text)
        {
            if (m_debug == false)
                return;

            Console.WriteLine("MelsecCCLink::" + funcName + " "  + text);
        }
    }
}
#endif