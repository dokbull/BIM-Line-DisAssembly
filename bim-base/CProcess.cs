using System;
using System.Collections.Generic;

namespace bim_base
{
    public class CProcess
    {
        protected ProcessMain main = null;

        protected bool m_isRun = false;
        protected bool m_complete = false;
        protected bool m_pause = false;

        protected List<CElaspedTimer> listTimer = new List<CElaspedTimer>();

        public CProcess(ProcessMain procMain)
        {
            main = procMain;
        }

        public virtual bool start()
        {
            m_isRun = true;
            return true;
        }

        public virtual void stop()
        {
            m_isRun = false;
        }

        public bool isRun()
        {
            return m_isRun;
        }

        public bool isComplete()
        {
            return m_complete;
        }

        public virtual void pause()
        {
            m_pause = true;

            foreach (CElaspedTimer timer in listTimer)
            {
                timer.pause();
            }
        }

        public virtual void resume()
        {
            m_pause = false;

            foreach (CElaspedTimer timer in listTimer)
            {
                timer.resume();
            }
        }

        public virtual void run()
        {

        }

        public virtual void setTime()
        {

        }

        public void setOutput(OUTPUT output, bool value)
        {
            main.setOutput(output, value);
        }

        public bool output(OUTPUT output)
        {
            return main.output(output);
        }

        public bool input(INPUT input)
        {
            return main.input(input);
        }

        public void addAlarm(ALARM alarm, ALARM_TYPE type = ALARM_TYPE.NONE, string text = "")
        {
            main.addAlarm(alarm, type, text, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

    }
}