using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public enum TEACH_POS
{
    PICK_PP_WAIT,
    PICK_PP_PICK,
    PICK_PP_PLACE,

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
    public bool use;

    public double x = 0;
    public double y = 0;
    public double z = 0;

    // using mold pp
    public double zL = 0;
    public double zR = 0;

    // using base
    public double xB = 0;

    public double vel = 0.0d;
}

public class ModelInfo
{
    int m_index = 0;
    string m_modelName = "DEFAULT_MODEL";

    CSettings m_setting = null;

    POS[] m_teachPos = new POS[(int)TEACH_POS.MAX];

    public event EventHandler changedModel;

    public ModelInfo(int index, string name = "NONE")
    {
        m_index = index;

        if (m_index >= 90 && name.Contains("TT_") == false)
            name = "TT_" + name;

        m_setting = new CSettings(Common.MODEL_PATH + m_index.ToString());

        m_modelName = name;
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

    public bool copy(string src, string dest)
    {
        if (src == "" || dest == "")
            return false;

        if (File.Exists(dest) == false)
            return false;

        //if (File.Exists(src))
        File.Copy(src, dest, true);

        return true;
    }

    public bool save(string name)
    {
        return true;
    }

    public bool delete(string name)
    {
        if (name == "")
            return false;

        if (!File.Exists(name))
            return false;

        File.Delete(name);
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

    public void saveTeachPos(POS pos)
    {
        makeBackup();
        m_setting.setValue("POSITION", pos.name + "_X", pos.x);
        m_setting.setValue("POSITION", pos.name + "_Y", pos.y);
        m_setting.setValue("POSITION", pos.name + "_Z", pos.z);
        m_setting.setValue("POSITION", pos.name + "_ZL", pos.zL);
        m_setting.setValue("POSITION", pos.name + "_ZR", pos.zR);
        m_setting.setValue("POSITION", pos.name + "_XB", pos.xB);
        m_setting.setValue("POSITION", pos.name + "_VEL", pos.vel);
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
