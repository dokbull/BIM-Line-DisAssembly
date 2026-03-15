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
using System.Runtime.CompilerServices;

namespace Lib.UI.Generic.DarkMode.Controls
{
    public partial class DarkPropertyGrid : PropertyGrid, iDarkMode
    {
        public DarkPropertyGrid()
        {
            InitializeComponent();

            this.DarkLevel = this.m_DarkLevel;
            this.Cursor = Cursors.Arrow;
        }

        protected int m_DarkLevel = 30;
        private int m_NameColumnWidth = 100;

        [Category("Dark Mode")]
        [DisplayName("Dark Level")]
        public int DarkLevel
        {
            get { return this.m_DarkLevel; }
            set
            {
                this.m_DarkLevel = DarkModeFunc.SetDarkMode(value, this);

                (Color BackColor, Color ForeColor) defaultColorSet = DarkModeFunc.GetDarkModeColor(value );
                this.ViewBackColor = defaultColorSet.BackColor;
                this.ViewForeColor = defaultColorSet.ForeColor;

                this.HelpBackColor = defaultColorSet.BackColor;
                this.HelpForeColor = defaultColorSet.ForeColor;

                this.CategorySplitterColor = defaultColorSet.ForeColor;
                this.CategoryForeColor = defaultColorSet.ForeColor;

                this.SelectedItemWithFocusBackColor = defaultColorSet.ForeColor;
                this.SelectedItemWithFocusForeColor = defaultColorSet.BackColor;

                (Color BackColor, Color ForeColor) partsColorSet = DarkModeFunc.GetDarkModeColor(value * 2);
                this.LineColor = partsColorSet.BackColor;

            }
        }


        [Category("Dark Property Grid")]
        [DisplayName("Name Column Width")]
        public int NameColumnWidth
        {
            get { return this.m_NameColumnWidth; }
            set { this.m_NameColumnWidth = value; }
        }

        private void DarkPropertyGrid_SelectedObjectsChanged(object sender, EventArgs e)
        {

            if (this.SelectedObject == null) return;

            PropertyGridFunc.SetNameColumnWidth(this, this.m_NameColumnWidth, true);

        }
    }
}
