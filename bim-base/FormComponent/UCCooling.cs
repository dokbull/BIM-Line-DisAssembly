using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bim_base
{
    public partial class UCCooling : UserControl
    {
        ProcessMain main;
        public bool pCoolIn = false;
        public void setMain(ProcessMain procMain)
        {
            main = procMain;

        }
        public UCCooling(ProcessMain main)
        {
            InitializeComponent();
            this.main = main;
        }
        public UCCooling()
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
                main.processTransferUD().CVRun,
                main.processTransferUD().CVAlarm,
                main.processTransferUD().CVInterLock,
                main.processTransferUD().CVRunComplete,
                main.processTransferUD().CVStandby,
                main.processTransferUD().CVStop
                );
#endif
        }
    }
}
