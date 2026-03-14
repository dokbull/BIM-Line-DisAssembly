using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum GEIM_CMD
{
    RUN = 9000,
    STOP = 9001,
    PRODUCT_OK = 9002,
    PRODUCT_NG = 9003,
    PRODUCT_IN = 9004,
    CHANGE_LAMP = 9009,
    NG_TRAY = 9010,
    JIG_STATE = 9020,
    MACHINE_INCOUNT = 9200,
}

public enum GEIM_JIG_MSG
{
    START = 1,
    IN = 3,
    OUT = 4,
}

public enum GEIM_JIG_CMD
{
    NONE = 0,
    NORMAL = 1,
    NOT_USE = 2,
    BLOCK = 3,
}

enum GEIM_MSG
{
    MC,
    DATE,
    CODE,
    PACK,
    VER,
    JIG_ID,
    EXTRA,
    MC_TOP_CODE,
    SET_ERROR,
    MAX,
}

public class GEIM
{
    static bool m_useCustomPath = false;
    static string m_customPath = "";
    static bool m_useTopCode = false;
    static string m_customTopCode = "";

    CFileManager m_geim = null;

    public static string mcTopCode()
    {
        if (m_useTopCode)
        {
            return m_customTopCode;
        }

        return "";
    }

    public void setUseTopCode(bool isUse)
    {
        m_useTopCode = isUse;
    }

    public static string savePath()
    {
        if (m_useCustomPath)
        {
            return m_customPath;
        }

        return "C:\\FA\\LOG\\";
    }

    public void setCustomTopCode(string topCode)
    {
        m_useTopCode = true;
        m_customTopCode = topCode;
    }   
    public void setCustomPath(string path)
    {
        m_useCustomPath = true;
        m_customPath = path;
    }

    #region 레거시 코드
    //jig state :: jig 상태(Pack Block(2자리)+BLOCK(2자리)+NOT USE 수(2자리)+전체지그수(2자리) 
    //sub1 :: 1 - 상태보고 / 3 - jig 제품투입 / 4 - jig 제품 배출
    //jigNo:: 투입 지그 번호 (두자리)
    //extra :: jig 투입시에만 사용. 그외 공백
    public void writeGeim(GEIM_CMD cmd, string jigState, string sub1, string jigNo, string extra = "0")  //jig 사용할 때만 적용함.
    {
        if (Conf.USE_GEIM == false)
            return;

        string path = savePath();

        string mc = "";
        string date = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        string code = ((int)cmd).ToString("0000");
        string pack = jigState; 
        string topcode = mcTopCode();
        string modelName = Conf.CURR_MODEL;
        string fileName = mc + "," + date + "," + code + "," + pack + "," + sub1 + "," + jigNo + "," + extra + "," + topcode + "," + "," + ".txt";
        if (sub1 == "4")
            fileName= mc + "," + date + "," + code + "," + pack + "," + sub1 + "," + jigNo + "," + extra + "," + topcode + "," + modelName +"," + ".txt";
        m_geim = new CFileManager(path + fileName);
        m_geim.write(fileName);
    }
    #endregion

    /// <summary>
    /// only using jig status write. (machine start, product in jig, product out jig)
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="packBlock"></param>
    /// <param name="block"></param>
    /// <param name="notUse"></param>
    /// <param name="jigCount"></param>
    /// <param name="jigNo"></param>
    /// <param name="cmd"></param>
    public void writeGeim(GEIM_JIG_MSG msg, int packBlock, int block, int notUse, int jigCount, int jigNo, GEIM_JIG_CMD cmd = GEIM_JIG_CMD.NONE) 
    {
        if (Conf.USE_GEIM == false)
            return;

        string path = savePath();
        string jigState = makeJigState(packBlock, block, notUse, jigCount);

        string[] data = new string[(int)GEIM_MSG.MAX];

        data[(int)GEIM_MSG.MC] = "";
        data[(int)GEIM_MSG.DATE] = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        data[(int)GEIM_MSG.CODE] = GEIM_CMD.JIG_STATE.ToString("0000");
        data[(int)GEIM_MSG.PACK] = jigState;
        data[(int)GEIM_MSG.VER] = ((int)msg).ToString();
        data[(int)GEIM_MSG.JIG_ID] = jigNo.ToString("00");
        data[(int)GEIM_MSG.EXTRA] = ((int)cmd).ToString();
        data[(int)GEIM_MSG.MC_TOP_CODE] = mcTopCode();
        data[(int)GEIM_MSG.SET_ERROR] = ""; 

        if (msg == GEIM_JIG_MSG.OUT)
            data[(int)GEIM_MSG.SET_ERROR] = Conf.CURR_MODEL; // USE MODEL NAME

        string fileName = makeFileName(data);

        m_geim = new CFileManager(path + fileName);
        m_geim.write(fileName);
    }

    /// <summary>
    /// 기존 writeGeim 수정 by Hyeon
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="extra"></param>
    /// <param name="jigState"></param>
    public void writeGeim(GEIM_CMD cmd, string extra = "", string jigState = "00000000") // machine status log
    {
        if (Conf.USE_GEIM == false)
            return;

        string path = savePath();

        string[] data = new string[(int)GEIM_MSG.MAX];

        data[(int)GEIM_MSG.MC] = "";
        data[(int)GEIM_MSG.DATE] = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        data[(int)GEIM_MSG.CODE] = ((int)cmd).ToString("0000");
        data[(int)GEIM_MSG.PACK] = jigState;
        data[(int)GEIM_MSG.VER] = Common.VERSION;
        data[(int)GEIM_MSG.JIG_ID] = "";
        data[(int)GEIM_MSG.EXTRA] = extra;
        data[(int)GEIM_MSG.MC_TOP_CODE] = mcTopCode();
        data[(int)GEIM_MSG.SET_ERROR] = "";

        string fileName = makeFileName(data);

        m_geim = new CFileManager(path + fileName);
        m_geim.write(fileName);
    }

    public void writeGeimProduct(GEIM_CMD cmd, bool isWait, string jigState = "00000000")
    {
        if (Conf.USE_GEIM == false)
            return;

        string path = savePath();
        string waitStr = isWait ? "1" : "0";

        string[] data = new string[(int)GEIM_MSG.MAX];

        data[(int)GEIM_MSG.MC] = "";
        data[(int)GEIM_MSG.DATE] = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        data[(int)GEIM_MSG.CODE] = ((int)cmd).ToString("0000");
        data[(int)GEIM_MSG.PACK] = jigState;
        data[(int)GEIM_MSG.VER] = Common.VERSION;
        data[(int)GEIM_MSG.JIG_ID] = waitStr;
        data[(int)GEIM_MSG.EXTRA] = "";
        data[(int)GEIM_MSG.MC_TOP_CODE] = mcTopCode();
        data[(int)GEIM_MSG.SET_ERROR] = "";

        string fileName = makeFileName(data);

        m_geim = new CFileManager(path + fileName);
        m_geim.write(fileName);
    }

    public void writeGeimNGProduct(string bcr, string jigNo, string error, string jigState = "00000000")
    {
        if (Conf.USE_GEIM == false)
            return;

        string path = savePath();

        string[] data = new string[(int)GEIM_MSG.MAX + 1];  //NG 배출의 경우 예외적으로 11개 사용함.

        data[(int)GEIM_MSG.MC] = "";
        data[(int)GEIM_MSG.DATE] = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        data[(int)GEIM_MSG.CODE] = ((int)GEIM_CMD.PRODUCT_NG).ToString("0000");
        data[(int)GEIM_MSG.PACK] = jigState;
        data[(int)GEIM_MSG.VER] = Common.VERSION;
        data[(int)GEIM_MSG.JIG_ID] = jigNo;
        data[(int)GEIM_MSG.EXTRA] = error;
        data[(int)GEIM_MSG.MC_TOP_CODE] = mcTopCode();
        data[(int)GEIM_MSG.SET_ERROR] = bcr;
        data[(int)GEIM_MSG.MAX] = "spec.";

        string fileName = makeFileName(data);

        m_geim = new CFileManager(path + fileName);
        m_geim.write(fileName);
    }

    public void writeGeimProductOut(int jigNo, string result, string jigState = "00000000")
    {
        if (Conf.USE_GEIM == false)
            return;

        string path = savePath();

        string[] data = new string[(int)GEIM_MSG.MAX];

        data[(int)GEIM_MSG.MC] = "";
        data[(int)GEIM_MSG.DATE] = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        data[(int)GEIM_MSG.CODE] = ((int)GEIM_CMD.JIG_STATE).ToString("0000");
        data[(int)GEIM_MSG.PACK] = jigState;
        data[(int)GEIM_MSG.VER] = "4";
        data[(int)GEIM_MSG.JIG_ID] = jigNo.ToString();
        data[(int)GEIM_MSG.EXTRA] = result;
        data[(int)GEIM_MSG.MC_TOP_CODE] = mcTopCode();
        data[(int)GEIM_MSG.SET_ERROR] = Common.MODEL_INFO.currentModelName();

        string fileName = makeFileName(data);

        m_geim = new CFileManager(path + fileName);
        m_geim.write(fileName);
    }

    /// <summary>
    /// ALARM RISE
    /// </summary>
    /// <param name="alarm"></param>
    /// <param name="jigState"></param>
    public void writeGeim(int alarm, string jigState = "00000000") // machine alarm log
    {
        if (Conf.USE_GEIM == false)
            return;

        string path = savePath();

        string[] data = new string[(int)GEIM_MSG.MAX];

        data[(int)GEIM_MSG.MC] = "";
        data[(int)GEIM_MSG.DATE] = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        data[(int)GEIM_MSG.CODE] = alarm.ToString("0000");
        data[(int)GEIM_MSG.PACK] = jigState;
        data[(int)GEIM_MSG.VER] = Common.VERSION;
        data[(int)GEIM_MSG.JIG_ID] = "";
        data[(int)GEIM_MSG.EXTRA] = "0";
        data[(int)GEIM_MSG.MC_TOP_CODE] = mcTopCode();
        data[(int)GEIM_MSG.SET_ERROR] = "";

        string fileName = makeFileName(data);

        m_geim = new CFileManager(path + fileName);
        m_geim.write(fileName);
    }

    string makeFileName(string[] data)
    {
        string name = "";

        for (int i = 0; i < data.Length; i++)
        {
            name += data[i];
            name += ",";
        }

        name += ".txt";

        return name;
    }

    public string makeJigState(int pack, int block, int notUse, int total)
    {
        string jigState = pack.ToString("00") + block.ToString("00") + notUse.ToString("00") + total.ToString("00");

        return jigState;
    }
}
