using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

public enum TEACH_POS
{
    PICK_PP_WAIT,
    PICK_PP_PICK,
    PICK_PP_PLACE,
    PICK_PP_NG_OUT,

    MOLD_PP_WAIT,
    MOLD_PP_LEFT,
    MOLD_PP_RIGHT,

    UB_PP_WAIT,
    UB_PP_PICK,
    UB_PP_PLACE_REAR,
    UB_PP_PLACE_FRONT,

    NONE,
    MAX,
}

public class POS
{
    public string name;

    public double x = 0;
    public double y = 0;
    public double z = 0;

    // using mold pp
    public double zL = 0;
    public double zR = 0;

    // using base
    public double xB = 0;

    public double vel = 0.0d;

    public void resetPos()
    {
        x = 0;
        y = 0;
        z = 0;
        zL = 0;
        zR = 0;
        xB = 0;
        vel = 0;
    }
}

public class ModelInfo
{
    int m_index = 0;
    bool m_use = true;

    string m_modelName = "EMPTY";

    CSettings m_setting = null;

    POS[] m_teachPos = new POS[(int)TEACH_POS.MAX];

    public event EventHandler changedModel;

    public ModelInfo(int index)
    {
        m_index = index;

        m_setting = new CSettings(Common.MODEL_PATH + m_index.ToString());
        m_modelName = loadModelName();

        init();
    }

    private void init()
    {
        for (int i = 0; i < (int)TEACH_POS.MAX; i++)
        {
            m_teachPos[i] = new POS();
            m_teachPos[i].name = ((TEACH_POS)i).ToString();
        }
    }

    public void setTeachPos(int index, POS pos)
    {
        m_teachPos[index] = pos;
    }

    public void setTeachPos(POS[] pos)
    {
        m_teachPos = pos;
    }

    public string teachPosString(int teachPos)
    {
        return teachPosString((TEACH_POS)teachPos);
    }

    public string teachPosString(TEACH_POS teachPos)
    {
        string retn = "";
        Type enumType = typeof(TEACH_POS);
        MemberInfo[] memberInfos = enumType.GetMember(teachPos.ToString());
        if (memberInfos.Length > 0)
            retn = teachPos.ToString();

        return retn;
    }

    public bool existModelCheck(string name)
    {
        bool ret = false;
        DirectoryInfo dirInfo = new DirectoryInfo(Common.MODEL_PATH);
        FileInfo[] fileInfo = dirInfo.GetFiles();
        for (int i = 0; i < fileInfo.Length; i++)
        {
            if (fileInfo[i].Name.ToUpper() == name.ToUpper())
            {
                ret = true;
                break;
            }
        }

        return ret;
    }


    public void load()
    {
        loadModelName(); 
        loadModelUse();

        for (int i = 0; i < m_teachPos.Length; i++)
        {
            string name = m_teachPos[i].name;

            m_teachPos[i].x = m_setting.getValue("POSITION", name + "_X", 0.0d);
            m_teachPos[i].y = m_setting.getValue("POSITION", name + "_Y", 0.0d);
            m_teachPos[i].z = m_setting.getValue("POSITION", name + "_Z", 0.0d);
            m_teachPos[i].zL = m_setting.getValue("POSITION", name + "_ZL", 0.0d);
            m_teachPos[i].zR = m_setting.getValue("POSITION", name + "_ZR", 0.0d);
            m_teachPos[i].xB = m_setting.getValue("POSITION", name + "_XB", 0.0d);
            m_teachPos[i].vel = m_setting.getValue("POSITION", name + "_VEL", 0.0d);
        }
    }

    public bool load(string name)
    {
        string path = Common.MODEL_PATH + name;

        FileInfo fileInfo = new FileInfo(path);

        if (fileInfo.Directory.Exists == false)
            fileInfo.Directory.Create();

        if (!fileInfo.Exists)
        {
            fileInfo.Create();
        }

        return true;
    }

    public string modelName()
    {
        return m_modelName;
    }

    public POS teachData(TEACH_POS pos)
    {
        return m_teachPos[(int)pos];
    }

    public void makeBackup()
    {
        string path = Common.MODEL_PATH + m_modelName;

        string backupPath = Common.MODEL_PATH + "\\backup\\";

        if (Directory.Exists(backupPath) == false)
            Directory.CreateDirectory(backupPath);

        if (File.Exists(path) == true)
        {
            DateTime now = DateTime.Now;
            File.Copy(path, backupPath + m_modelName + "_" + now.ToString("yyMMdd_HHmmss_fff"));
        }
    }

    public int index()
    {
        return m_index;
    }

    public void saveModelUse(bool value)
    {
        m_use = value;
        m_setting.setValue("MODEL", "USE", value);
    }


    public void loadModelUse()
    {
        m_use = m_setting.getValue("MODEL", "USE", true);
    }

    public bool isUse()
    {
        return m_use;
    }

    public void saveModelName(string name)
    {
        m_modelName = name;

        m_setting.setValue("MODEL", "NAME", name);
    }

    public string loadModelName()
    {
        m_modelName = m_setting.getValue("MODEL", "NAME", "EMPTY");

        if (m_modelName == "EMPTY")
        {
            m_modelName = "MODEL_" + m_index.ToString();

            if (m_index >= 90 && m_modelName.PadLeft(3) != "TT_")
                m_modelName = "TT_" + m_modelName;

            saveModelName(m_modelName);
        }

        return m_modelName;
    }

    public void saveTeachPos(POS pos)
    {
        makeBackup();
        string name = pos.name;

        m_setting.setValue("POSITION", pos.name + "_X", pos.x);
        m_setting.setValue("POSITION", pos.name + "_Y", pos.y);
        m_setting.setValue("POSITION", pos.name + "_Z", pos.z);
        m_setting.setValue("POSITION", pos.name + "_ZL", pos.zL);
        m_setting.setValue("POSITION", pos.name + "_ZR", pos.zR);
        m_setting.setValue("POSITION", pos.name + "_XB", pos.xB);
        m_setting.setValue("POSITION", pos.name + "_VEL", pos.vel);
    }

    public bool createModel(string modelName, bool resetValue)
    {
        if (m_use == true)
            return false;

        saveModelName(modelName);

        saveModelUse(true);

        if (resetValue)
            clearTeachPos();

        return true;
    }

    public bool deleteModel(string modelName, bool resetValue)
    {
        if (modelName != m_modelName)
            return false;

        saveModelUse(false);

        if (resetValue)
            clearTeachPos();

        return true;
    }

    public void clearTeachPos()
    {
        for (int i = 0; i < (int)TEACH_POS.MAX; i++)
        {
            m_teachPos[i].resetPos();
            saveTeachPos(m_teachPos[i]);
        }
    }

    public POS teachPos(TEACH_POS posEnum)
    {
        return teachPos((int)posEnum);
    }

    public POS teachPos(int index)
    {
        return m_teachPos[index];
    }
}
