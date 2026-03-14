using System;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormTeach : Form
    {
        ProcessMain main = null;
        FormTeachPP FORM_IN_PP = null;
        FormTeachPP FORM_MOLD_PP = null;
        FormTeachPP FORM_UB_PP = null;

        int mSelForm = -1;
        public FormTeach(ProcessMain procMain)
        {
            main = procMain;
            FORM_IN_PP = new FormTeachPP(main);
            FORM_MOLD_PP = new FormTeachPP(main);
            FORM_UB_PP = new FormTeachPP(main);
            InitializeComponent();

            addAllForm();
            HideAllForm(0);

            this.tableLayoutPanel1.RowStyles[0] = new RowStyle(SizeType.Absolute, 0);
        }

        private void FormTeach_Load(object sender, EventArgs e)
        {
            SelForm(0);
        }

        public void SelForm(int index)
        {
            HideAllForm(index);
            mSelForm = index;
            if (mSelForm == 0)
            {
                FORM_IN_PP.Show();
                if (this.Visible)
                    traytab.SetPress = true;
            }
            else if (index == 1)
            {
                FORM_MOLD_PP.Show();
                if (this.Visible)
                    ppTab.SetPress = true;
            }
            else if (index == 2)
            {
                FORM_UB_PP.Show();
                if (this.Visible)
                    ppTab.SetPress = true;
            }
        }

        public void HideAllForm(int _index)
        {
            if (this.Visible)
            {
                traytab.SetPress = false;
                ppTab.SetPress = false;
            }

            if (_index == 0)
                FORM_IN_PP.Hide();
            else if(_index == 1)
                FORM_MOLD_PP.Hide();
            else
                FORM_UB_PP.Hide();

        }

        void addAllForm()
        {
            FORM_IN_PP.TopLevel = false;
            FORM_IN_PP.Top = 0;
            FORM_IN_PP.Left = 0;
            FORM_IN_PP.Dock = DockStyle.Fill;
            TeachPanel.Controls.Add(FORM_IN_PP);
            FORM_IN_PP.Show();

            FORM_MOLD_PP.TopLevel = false;
            FORM_MOLD_PP.Top = 0;
            FORM_MOLD_PP.Left = 0;
            FORM_MOLD_PP.Dock = DockStyle.Fill;
            TeachPanel.Controls.Add(FORM_MOLD_PP);
            FORM_MOLD_PP.Show();

            FORM_UB_PP.TopLevel = false;
            FORM_UB_PP.Top = 0;
            FORM_UB_PP.Left = 0;
            FORM_UB_PP.Dock = DockStyle.Fill;
            TeachPanel.Controls.Add(FORM_UB_PP);
            FORM_UB_PP.Show();

            TeachPanel.Dock = DockStyle.Fill;
            TeachPanel.Padding = new Padding(0, 0, 0, 0);
            TeachPanel.Margin = new Padding(0, 0, 0, 0);
        }

        private void traytab_Click(object sender, EventArgs e)
        {
            SelForm(0);

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"[{GetType().Name}] TeachForm Size {this.Size}");
            System.Diagnostics.Debug.WriteLine($"[{GetType().Name}] TeachPanel Size {TeachPanel.Size}");
#endif
        }

        private void ppTab_Click(object sender, EventArgs e)
        {
            SelForm(1);

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"[{GetType().Name}] TeachForm Size {this.Size}");
            System.Diagnostics.Debug.WriteLine($"[{GetType().Name}] TeachPanel Size {TeachPanel.Size}");
#endif
        }
    }
}
