using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bim_base.FormComponent
{
    public partial class UCLoader : UserControl
    {
        ProcessMain main;
        public void setMain(ProcessMain procMain)
        {
            main = procMain;

        }
        public UCLoader(ProcessMain main)
        {
            InitializeComponent();
            this.main = main;
        }
        public UCLoader()
        {
            InitializeComponent();
        }
        public void timer_Tick()
        {
            if (main == null)
                return;

            buttonSDVCVRun.updateState();
        }

        public void loadActionButton()
        {
#if false
            buttonSDVCVRun.setEventState(
                main.processTransferLD().CVRun,
                main.processTransferLD().CVAlarm,
                main.processTransferLD().CVInterLock,
                main.processTransferLD().CVRunComplete,
                main.processTransferLD().CVStandby,
                main.processTransferLD().CVStop
                );
#endif
        }
    }
}
