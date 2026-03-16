using System;
using System.Drawing;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormTeach : Form
    {
        ProcessMain main = null;
        FormTeachInPP FORM_IN_PP = null;
        FormTeachMoldPP FORM_MOLD_PP = null;
        FormTeachOutPP FORM_UB_PP = null;

        int mSelForm = -1;
        public FormTeach(ProcessMain procMain)
        {
            main = procMain;
            FORM_IN_PP = new FormTeachInPP(main);
            FORM_MOLD_PP = new FormTeachMoldPP(main);
            FORM_UB_PP = new FormTeachOutPP(main);
            InitializeComponent();

            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.Dock = DockStyle.Fill;
        }

        private void FormTeach_Load(object sender, EventArgs e)
        {
            addFormToTab(FORM_IN_PP, tabPage1);
            addFormToTab(FORM_MOLD_PP, tabPage2);
            addFormToTab(FORM_UB_PP, tabPage3);
        }

        private void addFormToTab(Form form, TabPage page)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            page.Controls.Add(form);
            form.Show();
        }

        private void inPPTab_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void moldPPTab_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void outPPTab_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void uiTimer_Tick(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                inPPTab.GradientBottom = Color.Lime;
                inPPTab.GradientTop = Color.Lime;
                moldPPTab.GradientBottom = Color.LightGray;
                moldPPTab.GradientTop = Color.LightGray;
                outPPTab.GradientBottom = Color.LightGray;
                outPPTab.GradientTop = Color.LightGray;
            }

            if (tabControl1.SelectedIndex == 1)
            {
                inPPTab.GradientBottom = Color.LightGray;
                inPPTab.GradientTop = Color.LightGray;
                moldPPTab.GradientBottom = Color.Lime;
                moldPPTab.GradientTop = Color.Lime;
                outPPTab.GradientBottom = Color.LightGray;
                outPPTab.GradientTop = Color.LightGray;
            }

            if (tabControl1.SelectedIndex == 2)
            {
                inPPTab.GradientBottom = Color.LightGray;
                inPPTab.GradientTop = Color.LightGray;
                moldPPTab.GradientBottom = Color.LightGray;
                moldPPTab.GradientTop = Color.LightGray;
                outPPTab.GradientBottom = Color.Lime;
                outPPTab.GradientTop = Color.Lime;
            }
        }

    }
}
