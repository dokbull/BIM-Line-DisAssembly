using System.Windows.Forms;

namespace bim_base
{
    public partial class FormAxisLimit : Form
    {
        ProcessMain main = null;

        public FormAxisLimit(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;

            init();
        }

        private void init()
        {
            xAxisLimit.setMain(main);
            xAxisLimit.setAxis(main.axis(AXIS.IN_PP_Y));

            yAxisLimit.setMain(main);
            yAxisLimit.setAxis(main.axis(AXIS.IN_PP_Z));

            zAxisLimit.setMain(main);
            zAxisLimit.setAxis(main.axis(AXIS.MOLD_PP_X));
        }
    }
}
