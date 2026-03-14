using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

class Util
{
    public static short convertArrayToBit(bool[] array)
    {
        if (array.Length == 0 || array.Length > 15)
        {
            //Debug.warning("Util::convertArrayToBit
        }

        short ret = 0;
        return ret;
    }

    public static void setShort(ref byte[] buffer, int offset, int value)
    {
        buffer[offset + 0] = (byte)(value & 0xff);
        buffer[offset + 1] = (byte)((value >> 8) & 0xff);
    }

    public static int toShort(ref int[] array, int offset, bool value)
    {
        if (value)
            array[offset] = 1;
        else
            array[offset] = 0;

        return offset + 1;
    }

    public static short toShort(bool value)
    {
        if (value)
            return 1;

        return 0;
    }

    public static int toInt(byte[] buffer, int offset)
    {
        int value = (buffer[offset + 1] << 8) | buffer[offset];
        return value;
    }

    public static int toInt(short value1, short value2)
    {
        return (value2 << 16) + value1;
    }

    public static int toInt(bool value1, bool value2)
    {
        short val1 = toShort(value1);
        short val2 = toShort(value2);

        return toInt(val1, val2);
    }

    public static int toInt(bool value1, short value2)
    {
        short val1 = toShort(value1);
        return toInt(val1, value2);
    }

    public static int toInt(short value1, bool value2)
    {
        short val2 = toShort(value2);
        return toInt(value1, val2);
    }

    public static double toDouble(int value1, int value2, int digitCount)
    {
        return value1 + (value2 / Math.Pow(10, digitCount));
    }

    public static long toLong(int value1, int value2)
    {
        return (value2 << 16) | value1;
    }

    public static long toLong(byte[] data, int offset)
    {
        if (data.Length < offset + 4)
            return 0;

        int highWord = (ushort)((data[2] << 8) | data[3]);
        int lowWord = (ushort)((data[4] << 8) | data[5]);

        long raw = (int)((highWord << 16) | lowWord);

        return raw;
    }

    public static int toInt32(CTextBox textbox, int defaultValue = 0)
    {
        return toInt32(textbox.Text, defaultValue);
    }

    // TEXT -> VARIABLE
    public static bool toBool(string text)
    {
        bool value = false;

        try
        {
            value = Convert.ToBoolean(text);
        }
        catch (Exception)
        {
            Debug.warning("Util::toBool failed to convert value:" + text);
        }

        return value;
    }

    public static int toInt32(string text, int defaultValue = 0)
    {
        int value = defaultValue;

        try
        {
            value = Convert.ToInt32(text);
        }
        catch (Exception)
        {
            Debug.warning("Util::toInt32 failed to convert value:" + text);
        }

        return value;
    }

    public static long toInt64(string text, long defaultValue = 0)
    {
        long value = defaultValue;

        try
        {
            value = Convert.ToInt64(text);
        }
        catch (Exception)
        {
            Debug.warning("Util::toInt32 failed to convert value:" + text);
        }

        return value;
    }

    public static float toFloat(string text, float defaultValue = 0.0f)
    {
        float value = defaultValue;

        try
        {
            value = (float)(Convert.ToDouble(text));
        }
        catch (Exception)
        {
            Debug.warning("Util::toDouble failed to convert value:" + text);
        }

        return value;
    }

    public static double toDouble(string text, double defaultValue = 0.0f)
    {
        double value = defaultValue;

        try
        {
            value = Convert.ToDouble(text);
        }
        catch (Exception)
        {
            // Debug.warning("Util::toDouble failed to convert value:" + text);
        }

        return value;
    }

    //tmdwn..DataGridView 의 Cell 같은 값을 활용할 수 있게 제작 
    public static bool toBool(object obj)
    {
        if (obj == null)
            return false;

        return toBool(obj.ToString());
    }
    public static double toDouble(object obj, double defaultValue = 0.0d)
    {
        if (obj == null)
            return defaultValue;

        return toDouble(obj.ToString(), defaultValue);
    }


    public static int wordArrayToInt(int[] array, int offset, bool uInt = false, int length = 1)
    {
        int value = 0;

        if (uInt == false)
        {
            value = array[offset];

            if (value > 32768)
                value = (value - 65536);
        }
        else
        {
            for (int i = 0; i < length; i++)
            {
                value += (array[offset + i] * ((int)Math.Pow(65536, i))); // fix bug
            }
        }

        return value;
    }


    public static float wordArrayToFloat(int[] array, int offset, int decPoint = 2, bool uInt = true)
    {
        float value = 0;

        for (int i = 0; i < 2; i++)
        {
            value += (array[offset + i] * ((int)Math.Pow(65536, i))); // fix bug
        }

        if (uInt == false)
        {
            if (value > 32768)
                value = (value - 65536);
        }

        if (decPoint > 0)
            value = value / ((int)Math.Pow(10, decPoint));

        return value;
    }
    public static string toString(int[] array, int offset, int length, bool isName = false)
    {
        byte[] bytes = new byte[length * 2];

        for (int i = 0; i < length; i++)
        {
            bytes[i * 2 + 0] = (byte)(array[offset + i] & 0xFF);
            bytes[i * 2 + 1] = (byte)(array[offset + i] >> 8);

            if (isName)
            {
                for (int j = 0; j < 2; j++)
                {
                    byte value = bytes[i * 2 + j];

#if false
                    if (value >= 0x30 && value <= 0x39) // 0 ~ 9
                        replace = false;
                    else if (value >= 0x41 && value <= 0x5A) // A ~ Z
                        replace = false;
                    else if (value >= 0x61 && value <= 0x7A) // a ~ z
                        replace = false;
#endif

                    if (value < 0x20 || value > 0x7E)
                        bytes[i * 2 + j] = 0x20; // space
                }
            }
        }

        string text = Encoding.ASCII.GetString(bytes).Trim();

        return text;
    }

    public static string toStringRadix(int[] array, int offset, int length, int radix = 10)
    {
        string result = "";

        for (int i = 0; i < length; i++)
        {
            ushort word = (ushort)(array[offset + i] & 0xFFFF);
            byte low = (byte)(word & 0xFF);
            byte high = (byte)((word >> 8) & 0xFF);

            string lowStr = byteToStringRadix(low, radix);
            string highStr = byteToStringRadix(high, radix);

            result += lowStr + highStr;
        }

        return result.TrimEnd('\n');
    }

    private static string byteToStringRadix(byte b, int radix)
    {
        string result = "";

        if (b == 0)
            return result;

        switch (radix)
        {
            case 2:
                result = Convert.ToString(b, 2).PadLeft(8, '0');
                break;
            case 8:
                result = Convert.ToString(b, 8).PadLeft(3, '0');
                break;
            case 10:
                result = b.ToString();
                break;
            case 16:
                result = b.ToString("X2");
                break;
        }

        return result;
    }

    public static void setString(ref int[] arr, int offset, int length, string text)
    {
        if ((text.Length % 2) == 1)
            text += " ";

        byte[] data = ASCIIEncoding.ASCII.GetBytes(text);

        for (int i = 0; i < length; i++)
        {
            arr[offset + i] = 0x0;
        }

        for (int i = 0; i < data.Length / 2; i++)
        {
            arr[offset + i] = (data[i * 2 + 0]) + (data[i * 2 + 1] << 8);
        }
    }

    public static void setInt(ref byte[] data, int offset, int value, bool reverse = false)
    {
        if (reverse == false)
        {
            data[offset + 0] = (byte)((value >> 24) & 0xFF);
            data[offset + 1] = (byte)((value >> 16) & 0xFF);
            data[offset + 2] = (byte)((value >> 8) & 0xFF);
            data[offset + 3] = (byte)((value) & 0xFF);

            return;
        }

        data[offset + 0] = (byte)((value) & 0xFF);
        data[offset + 1] = (byte)((value >> 8) & 0xFF);
        data[offset + 2] = (byte)((value >> 16) & 0xFF);
        data[offset + 3] = (byte)((value >> 24) & 0xFF);
    }

    public static int getInt(byte[] data, int offset)
    {
        return
            (data[offset + 0] << 24) |
            (data[offset + 1] << 16) |
            (data[offset + 2] << 8) |
            (data[offset + 3]);
    }
    public static int GetLittleEndianIntegerFromByteArray(byte[] data)
    {
        int length = data.Length;
        int result = 0;

        for (int i = length - 1; i >= 0; i--)
        {
            result |= data[i] << i * 8;
        }
        return result;
    }

    public static void memcpy(ref byte[] dest, int destOffset, byte[] src, int srcOffset, int count)
    {
        for (int i = 0; i < count; i++)
        {
            dest[destOffset + i] = src[srcOffset + i];
        }
    }

    public static void memcpy(int[] dest, int destOffset, int[] src, int srcOffset, int count)
    {
        for (int i = 0; i < count; i++)
        {
            dest[destOffset + i] = src[srcOffset + i];
        }
    }

    public static void arrayCopy(ref short[] dest, byte[] src)
    {
        if (dest.Length != (src.Length / 2))
        {
            Debug.warning("Util::arrayCopy invalid dest length. length size:" + dest.Length);
            return;
        }

        for (int i = 0; i < dest.Length; i++)
            dest[i] = (short)((src[i * 2] << 8) | (src[i * 2 + 1]));
    }

    public static void arrayCopy(ref byte[] dest, short[] src)
    {
        if (dest.Length != (src.Length * 2))
        {
            Debug.warning("Util::arrayCopy invalid dest length. length size:" + dest.Length);
            return;
        }

        for (int i = 0; i < src.Length; i++)
        {
            dest[i * 2 + 0] = (byte)((src[i] >> 8) & 0xFF);
            dest[i * 2 + 1] = (byte)(src[i] & 0xFF);
        }
    }

    public static List<bool> toBit(int value)
    {
        List<bool> list = new List<bool>();

        for (int i = 0; i < 16; i++)
        {
            bool ret = ((value >> i) & 0x1) == 1 ? true : false;
            list.Add(ret);
        }

        return list;
    }

    public static void toBit(byte value, bool[] arr, int offset)
    {
        for (int i = 0; i < 8; i++)
        {
            bool ret = ((value >> i) & 0x1) == 1 ? true : false;
            arr[offset + i] = ret;
        }
    }

    public static byte bitToByte(bool[] value, int offset = 0)
    {
        byte result = 0;

        for (int i = 0; i < 8; i++)
        {
            byte val = (byte)((value[i + offset] == true) ? 1 : 0);
            result += (byte)(val << i);
        }

        return result;
    }

    public static int bitToInt(bool[] value, int offset = 0)
    {
        if ((value.Length - offset) < 16)
        {
            throw new ArgumentException("크기가 맞지 않습니다.");
        }

        int result = 0;

        for (int i = 0; i < 16; i++)
        {
            byte val = (byte)((value[i + offset] == true) ? 1 : 0);
            result += (val << i);
        }

        return result;
    }

    public static int bitToWord(bool[] value, int offset = 0)
    {
        int result = 0;

        for (int i = 0; i < 32; i++)
        {
            byte val = (byte)((value[i + offset] == true) ? 1 : 0);
            result += (val << i);
        }

        return result;
    }

    public static int bitToWord16(bool[] value, int offset = 0)
    {
        int result = 0;

        for (int i = 0; i < 16; i++)
        {
            byte val = (byte)((value[i + offset] == true) ? 1 : 0);
            result += (val << i);
        }

        return result;
    }

    public static void intToBit(int value, ref bool[] result)
    {
        intToBit(value, ref result, 0);
    }

    public static void intToBit(int value, ref bool[] result, int offset)
    {
        for (int i = 0; i < 16; i++)
        {
            result[offset + i] = ((value >> i) & 0x1) == 1 ? true : false;
        }
    }

    public static void byteToBit(byte value, ref bool[] result, int offset)
    {
        for (int i = 0; i < 8; i++)
        {
            result[offset + i] = ((value >> i) & 0x01) == 1 ? true : false;
        }
    }

    public static void wordToBit(uint value, ref bool[] result, int offset = 0)
    {
        for (int i = 0; i < 32; i++)
        {
            result[offset + i] = ((value >> i) & 0x1) == 1 ? true : false;
        }
    }

    public static void wordToBit(int value, ref bool[] result, int offset)
    {
        for (int i = 0; i < 32; i++)
        {
            result[offset + i] = ((value >> i) & 0x1) == 1 ? true : false;
        }
    }

    public static void wordToBit16(int value, ref bool[] result, int offset)
    {
        for (int i = 0; i < 16; i++)
        {
            result[offset + i] = ((value >> i) & 0x1) == 1 ? true : false;
        }
    }

    public static void wordToBit8(int value, ref bool[] result, int offset)
    {
        for (int i = 0; i < 8; i++)
        {
            result[offset + i] = ((value >> i) & 0x1) == 1 ? true : false;
        }
    }

#if false
    private static Control findControlByName(string name, Control.ControlCollection parent)
    {
        foreach (Control con in parent)
        {
            if (con.Name == name)
                return con;
        }

        return null;
    }
#endif

    private static Control findControlByName(Control.ControlCollection control, string name)
    {
        foreach (Control con in control)
        {
            if (con.HasChildren)
            {
                Control result = findControlByName(con.Controls, name);

                if (result != null)
                    return result;
            }

            if (con.Name == name)
                return con;
        }

        return null;
    }

    public static Control findControlByName(Control control, string name)
    {
        return findControlByName(control.Controls, name);
    }

    private static bool FindControlsByTypeRecursive<TControl, TList>(TList list, Control.ControlCollection controls)
    where TControl : Control
    where TList : IList<TControl>
    {
        foreach (Control con in controls)
        {
            if (con.HasChildren)
            {
                FindControlsByTypeRecursive<TControl, TList>(list, con.Controls);
            }

            if (con is TControl control)
            {
                Debug.debug("############ con:" + control.Name);
                list.Add(control);
            }
        }

        return true;
    }

    /// <summary>
    /// List<Button> buttonList = new List<Button>();
    /// findControlsByType<Button, List<Button>>(buttonList, this);
    /// </summary>
    /// <typeparam name="TControl"></typeparam>
    /// <typeparam name="TList"></typeparam>
    /// <param name="list"></param>
    /// <param name="control"></param>
    public static void findControlsByType<TControl, TList>(TList list, Control control)
        where TControl : Control
        where TList : IList<TControl>
    {
        FindControlsByTypeRecursive<TControl, TList>(list, control.Controls);
    }

    public static void dataGridView_DoNotSort(DataGridView dataGridView)
    {
        foreach (DataGridViewColumn col in dataGridView.Columns)
            col.SortMode = DataGridViewColumnSortMode.NotSortable;
    }

    public static string getVersion()
    {
        Assembly assemObj = Assembly.GetExecutingAssembly();
        Version v = assemObj.GetName().Version; // 현재 실행되는 어셈블리..dll의 버전 가져오기

        int majorV = v.Major; // 주버전
        int minorV = v.Minor; // 부버전
        int buildV = v.Build; // 빌드번호
        int revisionV = v.Revision; // 수정번호

        return majorV + "." + minorV + "." + buildV + ".r" + revisionV;
    }

    public static DateTime getBuildDate()
    {
        // Version 정보를 가져옴
        Version version = Assembly.GetExecutingAssembly().GetName().Version;

        // 기준 날짜: 2000년 1월 1일 (로컬 시간대)
        DateTime baseDateLocal = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Local);

        // Build 값을 기준으로 날짜 계산
        int buildDays = version.Build; // Build는 2000년 1월 1일부터 경과한 일수
        DateTime buildDateLocal = baseDateLocal.AddDays(buildDays);

        // Revision 값을 시간으로 변환 (Revision은 2초 단위)
        int secondsSinceMidnight = version.Revision * 2;
        TimeSpan buildTime = TimeSpan.FromSeconds(secondsSinceMidnight);

        // 로컬 기준 최종 빌드 날짜 및 시간 계산
        buildDateLocal = buildDateLocal.Add(buildTime);

        return buildDateLocal; // 로컬 시간대 반환
    }

    public static double standardDeviation(List<double> data)
    {
        int n = data.Count;
        double mean = 0.0f, sum_deviation = 0.0f;
        int i;

        for (i = 0; i < n; ++i)
            mean += data[i];

        mean = mean / n;

        for (i = 0; i < n; ++i)
            sum_deviation += (data[i] - mean) * (data[i] - mean);

        return Math.Sqrt(sum_deviation / n);
    }

    public static double standardDeviation(double[] data, int n)
    {
        double mean = 0.0f, sum_deviation = 0.0f;
        int i;

        for (i = 0; i < n; ++i)
            mean += data[i];

        mean = mean / n;

        for (i = 0; i < n; ++i)
            sum_deviation += (data[i] - mean) * (data[i] - mean);

        return Math.Sqrt(sum_deviation / n);
    }

    public static string showInputBox(string text, string caption, string defultValue = "")
    {
        Form prompt = new Form();
        prompt.Width = 500;
        prompt.Height = 200;
        prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
        prompt.Text = caption;
        prompt.StartPosition = FormStartPosition.CenterScreen;

        Label textLabel = new Label() { Left = 50, Top = 20, Text = text, AutoSize = true };
        TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400, Text = defultValue };
        Button confirmation = new Button()
        {
            Text = "Ok",
            Left = 350,
            Width = 100,
            Top = 90,
            Height = 50,
            DialogResult = DialogResult.OK
        };

        confirmation.Click += (sender, e) => { prompt.Close(); };
        prompt.Controls.Add(textBox);
        prompt.Controls.Add(confirmation);
        prompt.Controls.Add(textLabel);
        prompt.AcceptButton = confirmation;

        return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
    }

    public static void folderOpen(string path)
    {
        try
        {
            Process.Start(path);
        }
        catch (Exception ex)
        {
            Debug.warning("Util::folderOpen error. reason:" + ex.Message + " path:" + path);
        }
    }

    public static void waitTick(int tick)
    {
        int start = Environment.TickCount;

        while (true)
        {
            Application.DoEvents();

            if (Environment.TickCount - start > tick)
                break;
        }

    }

    public static void waitTickSleep(int tick)
    {
        int start = Environment.TickCount;

        while (true)
        {
            Application.DoEvents();

            Thread.Sleep(1);

            if (Environment.TickCount - start > tick)
                break;
        }
    }

    public static DateTime stringToDateTime(string dateStr, string format)
    {
        DateTime ret = DateTime.MinValue;

        try
        {
            ret = DateTime.Parse(dateStr);
            return ret;
        }
        catch (Exception /*e*/)
        {
        }

        try
        {
            ret = DateTime.ParseExact(dateStr, format, null);
            return ret;
        }
        catch (Exception e)
        {
            Debug.warning("Util::stringToDateTime string:" + dateStr + " format:" + format + " exception:" + e.Message);
        }

        return ret;
    }

    public static bool checkMutex()
    {
        string productName = Application.ProductName;
        Mutex mtx = new Mutex(true, productName);

        // 1초 동안 뮤텍스를 획득하려 대기  
        TimeSpan tsWait = new TimeSpan(0, 0, 1);
        bool success = mtx.WaitOne(tsWait);

        return success;
    }

    public static bool sqlToDateTime(object obj, ref DateTime dateTime)
    {
        bool ret = false;

        try
        {
            if ((obj is DBNull) == false && obj != null)
            {
                dateTime = (DateTime)obj;
                ret = true;
            }
            else
            {
                ret = false;
            }
        }
        catch (Exception e)
        {
            Debug.debug("Util::sqlToDateTime exception message:" + e.Message);
            return false;
        }

        return ret;
    }

    public static List<string> getPortList()
    {
        List<string> lstPorts = new List<string>();
        RegistryKey rkRoot = Registry.LocalMachine.OpenSubKey("HARDWARE");
        RegistryKey rkSubKey = rkRoot.OpenSubKey("DEVICEMAP\\SERIALCOMM");

        if (rkSubKey == null || rkSubKey.ValueCount == 0)
        {
            lstPorts.Add("none");
        }
        else
        {
            string[] tmpCom = rkSubKey.GetValueNames();
            for (int i = 0; i < rkSubKey.ValueCount; i++)
            {
                lstPorts.Insert(0, (rkSubKey.GetValue(tmpCom[i]).ToString()));
            }
        }
        return lstPorts;
    }

    /// <summary>
    /// 하위폴더 포함 파일 목록 전체 가지고 오는 함수
    /// </summary>
    /// <param name="path"></param>
    /// <param name="list"></param>
    public static void getFileList(string path, List<string> list)
    {
        DirectoryInfo dir = new DirectoryInfo(path);

        DirectoryInfo[] dis = dir.GetDirectories();

        if (dis.Length > 0)
        {
            for (int i = 0; i < dis.Length; i++)
            {
                getFileList(dis[i].FullName, list);
            }
        }

        FileInfo[] fis = dir.GetFiles();

        for (int i = 0; i < fis.Length; i++)
        {
            list.Add(fis[i].FullName);
        }
    }
} // class
