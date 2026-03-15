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
    public partial class DarkComboBox : ComboBox, iDarkMode
    {
        public DarkComboBox()
        {
            InitializeComponent();

            this.DarkLevel = this.m_DarkLevel;
            this.Cursor = Cursors.Arrow;
            this.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        protected int m_DarkLevel = 30;

        [Category("Dark Mode")]
        [DisplayName("Dark Level")]
        public int DarkLevel
        {
            get { return this.m_DarkLevel; }
            set { this.m_DarkLevel = DarkModeFunc.SetDarkMode(value, this); }
        }
    }
}
