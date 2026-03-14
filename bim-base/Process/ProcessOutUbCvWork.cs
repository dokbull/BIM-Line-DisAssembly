namespace bim_base
{
    public class ProcessOutUbCvWork : CProcess
    {
        CElaspedTimer m_cvRunTimer = new CElaspedTimer(10 * 1000);

        bool m_lock = false;

        public ProcessOutUbCvWork(ProcessMain procMain) : base(procMain)
        {
        }

        public override bool start()
        {
            if (m_isRun == true)
                return false;

            return base.start();
        }

        public override void stop()
        {
            base.stop();
        }

        public void resetWork()
        {
            m_lock = false;
        }

        public void setLock(bool value)
        {
            m_lock = value;
        }

        public override void run()
        {
            if (m_isRun == false)
                return;

            if (main.isAlarm())
            {
                cvRun(false);
                return;
            }

            if (main.isAuto() == false)
            {
                cvRun(false);
                return;
            }

            cvControl();
        }//run

        void cvControl()
        {
            if (m_lock == true)
            {
                m_cvRunTimer.pause();
                cvRun(false);
                return;
            }

            if (input(INPUT.UB_ULD_CV_OUT) == true)
            {
                m_cvRunTimer.pause();
                cvRun(false);
                return;
            }

            if (input(INPUT.UB_ULD_CV_IN_1) == true || input(INPUT.UB_ULD_CV_IN_2) == true)
                m_cvRunTimer.start();

            bool value = false;

            if (m_cvRunTimer.isStart() == true && m_cvRunTimer.isElasped() == false)
                value = true;

            cvRun(value);
        }

        public void cvRun(bool value)
        {
            setOutput(OUTPUT.UB_ULD_CV_RUN, value);
        }

    }//class
}//namespace
