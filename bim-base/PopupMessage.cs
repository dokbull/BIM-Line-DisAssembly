using System.Collections.Generic;

namespace bim_base
{
    public enum POPUP
    {
        NONE = 0,
        DOOR,
        AXIS_NOT_READY,
        ORIGIN_START,
        ORIGIN_COMPLETE,
        ORIGIN_FAIL,
        EXIT_PROGRAM,
        INPUT_RANGE,
        INVALID_VALUE,
    }

    public class PopupMessage
    {
        static CSettings m_setting = null;

        static Dictionary<string, string> m_msgEngMap = new Dictionary<string, string>();

        public PopupMessage()
        {
        }

        public static void load()
        {
            string path = Common.PATH + "\\data\\popupMessage.dat";
            m_setting = new CSettings(path);

            List<string> msgList = m_setting.getKeys("MESSAGE_ENG");

            if (msgList != null)
            {
                foreach (string text in msgList)
                {
                    m_msgEngMap[text] = m_setting.getValue("MESSAGE_ENG", text, text);
                }
            }
        }

        public static string message(int popup)
        {
            return message((POPUP)popup);
        }

        public static string message(POPUP popup)
        {
            string text = "";

            string codeText = popup.ToString();

            if (m_msgEngMap.ContainsKey(codeText) == true)
                text = m_msgEngMap[codeText];
            else
                text = popup.ToString();

            return text;
        }
    }//class
}//namespace
