using System;
using System.Drawing;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormCylinderDelay : Form
    {
        ProcessMain main = null;

        public FormCylinderDelay(ProcessMain main)
        {
            InitializeComponent();
            this.main = main;
        }

        // Cylinder Delay Data Load
        private void cylinderDelay_Load(object sender, EventArgs e)
        {
            gridInit();
            load();
        }

        // grid Initialize
        private void gridInit()
        {
            CSourceGrid grid = CylinderListGrid;
            string[] cylinderNames = Enum.GetNames(typeof(CYLINDER_DELAY));
            int count = cylinderNames.Length;

            grid.Selection.EnableMultiSelection = false;
            grid.setRowCol(count, 2, true, true);
            grid.setTextAlignment(DevAge.Drawing.ContentAlignment.MiddleCenter);
            grid.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);

            CYLINDER_DELAY[] values = (CYLINDER_DELAY[])Enum.GetValues(typeof(CYLINDER_DELAY));

            for (int i = 0; i < count; i++)
            {
                grid.setValue(i, 0, cylinderNames[i].Replace("_", " "));
                grid.setColors(i, 0, Color.White, Color.Black);
                grid.setValue(i, 1, $"0.0 Sec");
            }
        }

        // Save Button Click
        private void saveButton_Click(object sender, EventArgs e)
        {
            save();
            main.writeBottomHistory("Cylinder Delay parameter change.");
            CMessageBox.showInfo(MessageText.saveMessage);
        }

        // Exit Button Click
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Save
        private void save()
        {

        }

        // Load
        private void load()
        {

        }
    }
}
