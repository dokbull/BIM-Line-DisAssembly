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
    public partial class DarkExpandPanel: Panel, iDarkMode
    {
        #region Construct

        public DarkExpandPanel()
        {
            InitializeComponent();

            this.DarkLevel = this.m_DarkLevel;
            this.m_PanelSize = this.Size;
            this.Cursor = Cursors.Arrow;

            this.ButtonDock = DockStyle.Top;
            this.ButtonSize = this.m_ButtonSize;
            this.Controls.Add(this.ExpandButton);

            this.IsExpanded = true;

            this.ControlAdded += DarkExpandPanel_ControlAdded;
        }

        #endregion

        #region Member 

        private int m_DarkLevel = 30;
        private int m_ButtonSize = 20;
        private Size m_PanelSize = new Size(50, 50);

        private DockStyle m_ButtonDock = DockStyle.Top;
        private string m_ButtonText = string.Empty;

        #endregion

        #region Properties

        [Category("Dark Mode")]
        public int DarkLevel
        {
            get { return this.m_DarkLevel; }
            set
            {

                this.m_DarkLevel = DarkModeFunc.SetDarkMode(value, this);
                this.ExpandButton.DarkLevel = DarkModeFunc.SetDarkMode(value * 2, this.ExpandButton);
            }
        }

        [Category("Dark Expand Panel")]
        [DisplayName("Button Dock")]
        public DockStyle ButtonDock
        {
            get { return this.m_ButtonDock; }
            set
            {
                switch (value)
                {
                    case DockStyle.Right:
                    case DockStyle.Top:
                    case DockStyle.Bottom:
                    case DockStyle.Left:
                        this.ExpandButton.Dock = value;
                        break;
                    default:
                        return;
                }

                switch (value)
                {
                    case DockStyle.Left:
                    case DockStyle.Right:
                        this.ExpandButton.Cursor = Cursors.NoMoveHoriz;
                        break;
                    case DockStyle.Top:
                    case DockStyle.Bottom:
                        this.ExpandButton.Cursor = Cursors.NoMoveVert;
                        break;
                    default:
                        return;
                }
                this.m_ButtonDock = this.ExpandButton.Dock;
                this.ButtonSize = this.m_ButtonSize;
            }
        }

        [Category("Dark Expand Panel")]
        [DisplayName("Button Size")]
        public int ButtonSize
        {
            get
            {
                return this.m_ButtonSize;
            }
            set
            {
                switch (this.ButtonDock)
                {
                    case DockStyle.Left:
                    case DockStyle.Right:
                        this.ExpandButton.Width = value;
                        break;

                    case DockStyle.Top:
                    case DockStyle.Bottom:
                        this.ExpandButton.Height = value;
                        break;
                    default:
                        return;
                }

                this.m_ButtonSize = value;

            }
        }

        [Category("Dark Expand Panel")]
        [DisplayName("Button Text")]
        public string ButtonText
        {
            get
            {
                return this.m_ButtonText;
            }
            set
            {
                this.m_ButtonText = value;
                this.ExpandButton.Text = value;
            }
        }


        [Category("Dark Expand Panel")]
        [DisplayName("Panel 확장")]
        public bool IsExpanded
        {
            get
            {
                switch (this.ExpandButton.Dock)
                {
                    case DockStyle.Left:
                    case DockStyle.Right:
                        return (this.Width > this.m_ButtonSize);

                    case DockStyle.Top:
                    case DockStyle.Bottom:
                        return (this.Height > this.m_ButtonSize);

                    default:
                        throw new NotImplementedException($"지원하지 않는 Dock 속성 ({this.ExpandButton.Dock})");
                }
            }
            set
            {
                if (this.IsExpanded)
                {
                    this.SetContractSize();
                }
                else
                {
                    this.SetExpandSize();
                }
            }

        }


        #endregion

        #region Private Method


        private void SetExpandSize()
        {
            if (this.IsExpanded) return;

            this.Size = this.m_PanelSize;
        }

        private void SetContractSize()
        {
            if (this.IsExpanded == false) return;

            switch (this.ButtonDock)
            {
                case DockStyle.Top:
                case DockStyle.Bottom:
                    this.Size = new Size(this.m_PanelSize.Width, this.m_ButtonSize);
                    break;
                case DockStyle.Left:
                case DockStyle.Right:
                    this.Size = new Size(this.m_ButtonSize, this.m_PanelSize.Height);
                    break;
                default:
                    return;
            }
        }


        #endregion

        #region Event


        private void ExpandButton_Click(object sender, EventArgs e)
        {
            this.IsExpanded = !this.IsExpanded;
        }

        private void ExpandPanel_SizeChanged(object sender, EventArgs e)
        {
            if (this.IsExpanded == false) return;

            this.m_PanelSize = this.Size;
        }

        private void DarkExpandPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            this.ExpandButton.SendToBack();
            e.Control.BringToFront();
        }
        #endregion
    }
}
