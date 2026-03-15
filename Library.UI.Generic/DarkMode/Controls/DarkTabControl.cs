using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lib.UI.Generic.ControlFunc;

namespace Lib.UI.Generic.DarkMode.Controls
{
    public partial class DarkTabControl : TabControl, iDarkMode
    {
        public DarkTabControl()
        {
            InitializeComponent();

            this.DarkLevel = this.m_DarkLevel;
            this.Cursor = Cursors.Arrow;
        }

        protected int m_DarkLevel = 30;

        [Category("Dark Mode")]
        [DisplayName("Dark Level")]
        public int DarkLevel
        {
            get { return this.m_DarkLevel; }
            set
            {
                this.m_DarkLevel = DarkModeFunc.SetDarkMode(value, this);
                this.SetDarkLevel();
            }
        }


        private void SetDarkLevel()
        {
            //foreach (Control item in this.Controls)
            //{
            //    DarkModeFunc.SetDarkMode(this.DarkLevel, item);
            //}

            foreach (TabPage tab in this.TabPages)
            {
                DarkModeFunc.SetDarkMode(this.DarkLevel, tab);
            }
        }

        private void DarkTabControl_ControlAdded(object sender, ControlEventArgs e)
        {
            this.SetDarkLevel();
        }
    }
}
