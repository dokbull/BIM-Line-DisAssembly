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
    public partial class UCTransferUD : UserControl
    {
        ProcessMain main;
        public void setMain(ProcessMain procMain)
        {
            main = procMain;

        }
        public UCTransferUD(ProcessMain main)
        {
            InitializeComponent();
            this.main = main;
        }
        public UCTransferUD()
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
            buttonSDVStopperFRDOWN.setEventState(
                main.processTransferUD().StopperDownFR,
                main.processTransferUD().StopperAlarm,
                main.processTransferUD().StopperDOWNInterlock,
                main.processTransferUD().IsStopperDownFR,
                main.processTransferUD().IsStopperUpFR
                );
            buttonSDVStopperFRUP.setEventState(
                main.processTransferUD().StopperUpFR,
                main.processTransferUD().StopperAlarm,
                main.processTransferUD().StopperUPFRInterlock,
                main.processTransferUD().IsStopperUpFR,
                main.processTransferUD().IsStopperDownFR
                );
            buttonSDVStopperRRDOWN.setEventState(
                main.processTransferUD().StopperDownRR,
                main.processTransferUD().StopperAlarm,
                main.processTransferUD().StopperDOWNInterlock,
                main.processTransferUD().IsStopperDownRR,
                main.processTransferUD().IsStopperUpRR
                );
            buttonSDVStopperRRUP.setEventState(
                main.processTransferUD().StopperUpRR,
                main.processTransferUD().StopperAlarm,
                main.processTransferUD().StopperUPRRInterlock,
                main.processTransferUD().IsStopperUpRR,
                main.processTransferUD().IsStopperDownRR
                );
#endif
        }

    }
}
