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
    public partial class DarkRadioButton : RadioButton, iDarkMode
    {
        #region Construct

        public DarkRadioButton()
        {
            InitializeComponent();

            this.DarkLevel = this.m_DarkLevel;
            this.Cursor = Cursors.Arrow;
        }

        #endregion

        #region Member

        protected int m_DarkLevel = 30;
        private (bool isHighLight, int DarkLevel) m_OriginDarkLevel = (false, 30);

        #endregion

        #region Properties

        [Category("Dark Mode")]
        [DisplayName("Dark Level")]
        public int DarkLevel
        {
            get { return this.m_DarkLevel; }
            set { this.m_DarkLevel = DarkModeFunc.SetDarkMode(value, this); }
        }

        #endregion

        #region Private Method

        private void SetHighLight()
        {
            if (this.Appearance != Appearance.Button) return;
            if (this.m_OriginDarkLevel.isHighLight) return;

            this.m_OriginDarkLevel.isHighLight = true;
            this.m_OriginDarkLevel.DarkLevel = this.m_DarkLevel;
            this.DarkLevel = this.m_DarkLevel * 2;
        }

        private void ResetHighLight()
        {
            if (this.Appearance != Appearance.Button) return;
            if (this.m_OriginDarkLevel.isHighLight == false) return;

            this.m_OriginDarkLevel.isHighLight = false;
            this.DarkLevel = this.m_OriginDarkLevel.DarkLevel;
        }

        #endregion

        #region Event

        private void DarkRadioButton_MouseEnter(object sender, EventArgs e)
        {

            this.SetHighLight();
        }

        private void DarkRadioButton_MouseLeave(object sender, EventArgs e)
        {
            this.ResetHighLight();
        }


        private void DarkRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Appearance != Appearance.Button) return;

            switch (this.FlatStyle)
            {
                case FlatStyle.Flat:
                case FlatStyle.Popup:
                    this.FlatStyle = (this.Checked ? FlatStyle.Flat : FlatStyle.Popup);
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
