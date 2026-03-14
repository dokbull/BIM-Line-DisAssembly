using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NMCMotionSDK;
using static NMCMotionSDK.NMCSDKLib;

using System.IO;

public class RSAMMCELib
{
    bool m_simulation = false;

    public RSAMMCELib()
    {
        string path = Common.PATH + "\\simulation";
        m_simulation = File.Exists(path);

        init();
    }

    bool init()
    {
        Debug.debug("RSAMMCELib::init");

        if (m_simulation)
            return true;

        MC_STATUS ret =  NMCSDKLib.MC_Init();

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCELib::init failed. ret:" + ret);
            return false;
        }
            
        return false;
    }

    public bool open()
    {
        return true;
    }

    public bool close()
    {
        return true;
    }
}
