using System;
using System.Text;
using System.Collections.Generic;

namespace lib.lightController.Jlail.DCV_35024
{
    /// <summary>
    /// Packet builder for JL-DCV-35024-16-B serial protocol.
    /// Compatible with C# 6.0.
    /// </summary>
    public static class PacketBuilder
    {
        /// <summary>
        /// Helper struct for (channel, value) pairs (C# 6.0 compatible).
        /// </summary>
        public struct ChannelValue
        {
            public int Channel { get; set; }
            public int Value { get; set; }
            public ChannelValue(int channel, int value) : this()
            {
                Channel = channel;
                Value = value;
            }
        }

        // ----------------- 기본 명령 -----------------

        public static string Open(int channel)
        {
            return BuildWithChannel('1', channel, 0);
        }

        public static string Close(int channel)
        {
            return BuildWithChannel('2', channel, 0);
        }

        public static string SetBrightness(int channel, int value)
        {
            return BuildWithChannel('3', channel, value);
        }

        public static string ReadBrightness(int channel)
        {
            return BuildWithChannel('4', channel, 0);
        }

        public static string ReadSwitchStatus(int channel)
        {
            return BuildWithChannel('5', channel, 0);
        }

        public static string QuickSetMask(ushort mask)
        {
            string maskHex = mask.ToString("X4");
            string six = "$" + '6' + maskHex;
            string cs = ComputeChecksum(six);
            return six + cs;
        }

        public static string FastReadBrightness(int channel)
        {
            return BuildWithChannel('9', channel, 0);
        }

        // ----------------- 다채널/브로드캐스트 -----------------

        public static string SetBrightnessAll(int value)
        {
            return BuildWithChannel('3', 0, value);
        }

        public static string[] SetBrightnessMany(params ChannelValue[] items)
        {
            if (items == null || items.Length == 0) return new string[0];
            var list = new string[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                list[i] = SetBrightness(items[i].Channel, items[i].Value);
            }
            return list;
        }

        public static string[] ReadBrightnessMany(params int[] channels)
        {
            if (channels == null || channels.Length == 0) return new string[0];
            var list = new string[channels.Length];
            for (int i = 0; i < channels.Length; i++)
            {
                list[i] = ReadBrightness(channels[i]);
            }
            return list;
        }

        public static string[] ReadSwitchStatusMany(params int[] channels)
        {
            if (channels == null || channels.Length == 0) return new string[0];
            var list = new string[channels.Length];
            for (int i = 0; i < channels.Length; i++)
            {
                list[i] = ReadSwitchStatus(channels[i]);
            }
            return list;
        }

        public static string[] FastReadBrightnessMany(params int[] channels)
        {
            if (channels == null || channels.Length == 0) return new string[0];
            var list = new string[channels.Length];
            for (int i = 0; i < channels.Length; i++)
            {
                list[i] = FastReadBrightness(channels[i]);
            }
            return list;
        }

        public static ushort BuildMaskFromChannels(IEnumerable<int> onChannels)
        {
            if (onChannels == null) return 0;
            ushort mask = 0;
            foreach (var ch in onChannels)
            {
                if (ch < 1 || ch > 16)
                    throw new ArgumentOutOfRangeException("onChannels", "Channel must be 1..16.");
                mask |= (ushort)(1 << (ch - 1));
            }
            return mask;
        }

        public static string QuickSetChannels(IEnumerable<int> onChannels)
        {
            return QuickSetMask(BuildMaskFromChannels(onChannels));
        }

        public static string QuickSetFromCurrent(ushort currentMask, IEnumerable<int> turnOn, IEnumerable<int> turnOff)
        {
            ushort target = currentMask;
            if (turnOn != null)
            {
                foreach (var ch in turnOn)
                {
                    if (ch < 1 || ch > 16) throw new ArgumentOutOfRangeException("turnOn");
                    target |= (ushort)(1 << (ch - 1));
                }
            }
            if (turnOff != null)
            {
                foreach (var ch in turnOff)
                {
                    if (ch < 1 || ch > 16) throw new ArgumentOutOfRangeException("turnOff");
                    target &= (ushort)~(1 << (ch - 1));
                }
            }
            return QuickSetMask(target);
        }

        // ----------------- 내부 처리 -----------------

        private static string BuildWithChannel(char cmd, int channel, int value)
        {
            char ch = ChannelToChar(channel, (cmd == '1' || cmd == '2' || cmd == '3'));

            string data;
            if (value < 0 || value > 255)
                throw new ArgumentOutOfRangeException("value", "Brightness value must be 0..255.");
            data = value.ToString("X3");

            if (cmd == '4' || cmd == '5' || cmd == '9') data = "000";

            string six = "$" + cmd + ch + data;
            string cs = ComputeChecksum(six);
            return six + cs;
        }

        private static char ChannelToChar(int channel, bool allowZero)
        {
            if (channel == 0)
            {
                if (!allowZero)
                    throw new ArgumentOutOfRangeException("channel", "Channel 0 (ALL) is not permitted for this command.");
                return '0';
            }
            if (channel < 1 || channel > 16)
                throw new ArgumentOutOfRangeException("channel", "Channel must be 1..16 (or 0 for ALL when allowed).");

            if (channel <= 9) return (char)('0' + channel);
            return (char)('A' + (channel - 10));
        }

        private static string ComputeChecksum(string sixChars)
        {
            if (sixChars.Length != 6)
                throw new ArgumentException("Checksum source must be exactly 6 characters.", "sixChars");

            int x = 0;
            foreach (char c in sixChars)
            {
                x ^= (byte)c;
            }
            return x.ToString("X2");
        }
    }
}
