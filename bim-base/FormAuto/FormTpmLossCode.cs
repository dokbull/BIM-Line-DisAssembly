using bim_base.data.CIM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormTpmLossCode : Form
    {
        public FormTpmLossCode()
        {
            InitializeComponent();
        }

        private enum EnumTPItemList
        {
            None,
            DOWN,

            BM,
            BM_Manual,
            BM_Manual_Code,

            PM,
            PM_Scheduled,
            PM_Scheduled_Code,
            PM_Unscheduled,
            PM_Unscheduled_QC_Code,
            PM_Unscheduled_QC,
            PM_Unscheduled_EQ_Code,
            PM_Unscheduled_EQ,

            CM,
            CM_Setup_Code,
            CM_Setup,
            CM_EI_Code,
            CM_EI,
            CM_EE_Code,
            CM_EE,

            ModelChange,
            ModelChange_Diff_Code,
            ModelChange_Diff,
            ModelChange_Same_Code,
            ModelChange_Same,

            Materials,
            Materials_Down_Code,
            Materials_Down,
            Materials_Change_Code,
            Materials_Change,
        }

        private EnumTPItemList m_CurrentMousePosition = EnumTPItemList.None;

        internal CIMEnumeric.EnumTpCode SelectedTpCode { get; set; } = CIMEnumeric.EnumTpCode.None;
        internal string SelectedTpDescription { get; set; } = string.Empty;

        private CIMEnumeric.EnumTpCode IsSelectedCode(EnumTPItemList enumTPItemList)
        {
            switch (enumTPItemList)
            {
                case EnumTPItemList.DOWN:
                case EnumTPItemList.BM:
                case EnumTPItemList.PM:
                case EnumTPItemList.PM_Unscheduled:
                case EnumTPItemList.CM:
                case EnumTPItemList.ModelChange:
                case EnumTPItemList.Materials:
                default:
                    return CIMEnumeric.EnumTpCode.None; 

                case EnumTPItemList.BM_Manual:
                case EnumTPItemList.BM_Manual_Code:
                    return CIMEnumeric.EnumTpCode.BM_Manual_Code;

                case EnumTPItemList.PM_Scheduled:
                case EnumTPItemList.PM_Scheduled_Code:
                    return CIMEnumeric.EnumTpCode.PM_Scheduled_Code;

                case EnumTPItemList.PM_Unscheduled_QC_Code:
                case EnumTPItemList.PM_Unscheduled_QC:
                    return CIMEnumeric.EnumTpCode.PM_Unscheduled_QC_Code;

                case EnumTPItemList.PM_Unscheduled_EQ_Code:
                case EnumTPItemList.PM_Unscheduled_EQ:
                    return CIMEnumeric.EnumTpCode.PM_Unscheduled_EQ_Code;

                case EnumTPItemList.CM_Setup_Code:
                case EnumTPItemList.CM_Setup:
                    return CIMEnumeric.EnumTpCode.CM_Setup_Code;

                case EnumTPItemList.CM_EI_Code:
                case EnumTPItemList.CM_EI:
                    return CIMEnumeric.EnumTpCode.CM_EI_Code;

                case EnumTPItemList.CM_EE_Code:
                case EnumTPItemList.CM_EE:
                    return CIMEnumeric.EnumTpCode.CM_EE_Code;

                case EnumTPItemList.ModelChange_Diff_Code:
                case EnumTPItemList.ModelChange_Diff:
                    return CIMEnumeric.EnumTpCode.ModelChange_Diff_Code;

                case EnumTPItemList.ModelChange_Same_Code:
                case EnumTPItemList.ModelChange_Same:
                    return CIMEnumeric.EnumTpCode.ModelChange_Same_Code;

                case EnumTPItemList.Materials_Down_Code:
                case EnumTPItemList.Materials_Down:
                    return CIMEnumeric.EnumTpCode.Materials_Down_Code;

                case EnumTPItemList.Materials_Change_Code:
                case EnumTPItemList.Materials_Change:
                    return CIMEnumeric.EnumTpCode.Materials_Change_Code;
            }
        }

        private EnumTPItemList CheckLabel(object obj, out System.Windows.Forms.Label _Label)
        {
            _Label = null;

            if ((obj is System.Windows.Forms.Label) == false) return EnumTPItemList.None;
            System.Windows.Forms.Label lbl = obj as System.Windows.Forms.Label;

            if (lbl.Tag == null) return EnumTPItemList.None;
            if (Enum.TryParse(lbl.Tag.ToString(), out EnumTPItemList tpItem) == false) return EnumTPItemList.None;

            _Label = lbl;

            return tpItem;
        }

        private EnumTPItemList CheckLabel(object label)
        {
            return this.CheckLabel(label, out System.Windows.Forms.Label _);
        }

        private void SetLabelColor(System.Windows.Forms.Label _Label, bool _IsOn)
        {
            _Label.BackColor = (_IsOn ? Color.Lime : Color.White);
        }

        private void tmrRedraw_Tick(object sender, EventArgs e)
        {
            this.tmrRedraw.Stop();

            switch (this.m_CurrentMousePosition)
            {
                case EnumTPItemList.None:
                default:
                    foreach (var ctrl in this.pnlTable.Controls)
                    {
                        EnumTPItemList item = this.CheckLabel(ctrl, out System.Windows.Forms.Label _lbl);

                        switch (item)
                        {
                            case EnumTPItemList.None: continue;
                            default: this.SetLabelColor(_lbl, false); break;
                        }

                    }
                    break;
                case EnumTPItemList.DOWN:
                    foreach (var ctrl in this.pnlTable.Controls)
                    {
                        EnumTPItemList item = this.CheckLabel(ctrl, out System.Windows.Forms.Label _lbl);
                        if (item == EnumTPItemList.None) continue;

                        switch (item)
                        {
                            case EnumTPItemList.None: continue;
                            case EnumTPItemList.DOWN: this.SetLabelColor(_lbl, true); break;
                            default: this.SetLabelColor(_lbl, false); break;
                        }
                    }
                    break;
                case EnumTPItemList.BM:
                    foreach (var ctrl in this.pnlTable.Controls)
                    {
                        EnumTPItemList item = this.CheckLabel(ctrl, out System.Windows.Forms.Label _lbl);
                        if (item == EnumTPItemList.None) continue;

                        switch (item)
                        {
                            case EnumTPItemList.None: continue;
                            case EnumTPItemList.DOWN:
                            case EnumTPItemList.BM: this.SetLabelColor(_lbl, true); break;
                            default: this.SetLabelColor(_lbl, false); break;
                        }
                    }
                    break;
                case EnumTPItemList.BM_Manual:
                case EnumTPItemList.BM_Manual_Code:
                    foreach (var ctrl in this.pnlTable.Controls)
                    {
                        EnumTPItemList item = this.CheckLabel(ctrl, out System.Windows.Forms.Label _lbl);
                        if (item == EnumTPItemList.None) continue;

                        switch (item)
                        {
                            case EnumTPItemList.None: continue;
                            case EnumTPItemList.DOWN:
                            case EnumTPItemList.BM:
                            case EnumTPItemList.BM_Manual:
                            case EnumTPItemList.BM_Manual_Code: this.SetLabelColor(_lbl, true); break;
                            default: this.SetLabelColor(_lbl, false); break;
                        }
                    }
                    break;
                case EnumTPItemList.PM:
                    foreach (var ctrl in this.pnlTable.Controls)
                    {
                        EnumTPItemList item = this.CheckLabel(ctrl, out System.Windows.Forms.Label _lbl);
                        if (item == EnumTPItemList.None) continue;

                        switch (item)
                        {
                            case EnumTPItemList.None: continue;
                            case EnumTPItemList.DOWN:
                            case EnumTPItemList.PM:this.SetLabelColor(_lbl, true); break;
                            default: this.SetLabelColor(_lbl, false); break;
                        }
                    }
                    break;
                case EnumTPItemList.PM_Scheduled:
                case EnumTPItemList.PM_Scheduled_Code:
                    foreach (var ctrl in this.pnlTable.Controls)
                    {
                        EnumTPItemList item = this.CheckLabel(ctrl, out System.Windows.Forms.Label _lbl);
                        if (item == EnumTPItemList.None) continue;

                        switch (item)
                        {
                            case EnumTPItemList.None: continue;
                            case EnumTPItemList.DOWN:
                            case EnumTPItemList.PM:
                            case EnumTPItemList.PM_Scheduled:
                            case EnumTPItemList.PM_Scheduled_Code: this.SetLabelColor(_lbl, true); break;
                            default: this.SetLabelColor(_lbl, false); break;
                        }
                    }
                    break;
                case EnumTPItemList.PM_Unscheduled:
                    foreach (var ctrl in this.pnlTable.Controls)
                    {
                        EnumTPItemList item = this.CheckLabel(ctrl, out System.Windows.Forms.Label _lbl);
                        if (item == EnumTPItemList.None) continue;

                        switch (item)
                        {
                            case EnumTPItemList.None: continue;
                            case EnumTPItemList.DOWN:
                            case EnumTPItemList.PM:
                            case EnumTPItemList.PM_Unscheduled: this.SetLabelColor(_lbl, true); break;
                            default: this.SetLabelColor(_lbl, false); break;
                        }
                    }
                    break;
                case EnumTPItemList.PM_Unscheduled_QC:
                case EnumTPItemList.PM_Unscheduled_QC_Code:
                    foreach (var ctrl in this.pnlTable.Controls)
                    {
                        EnumTPItemList item = this.CheckLabel(ctrl, out System.Windows.Forms.Label _lbl);
                        if (item == EnumTPItemList.None) continue;

                        switch (item)
                        {
                            case EnumTPItemList.None: continue;
                            case EnumTPItemList.DOWN:
                            case EnumTPItemList.PM:
                            case EnumTPItemList.PM_Unscheduled:
                            case EnumTPItemList.PM_Unscheduled_QC:
                            case EnumTPItemList.PM_Unscheduled_QC_Code: this.SetLabelColor(_lbl, true); break;
                            default: this.SetLabelColor(_lbl, false); break;
                        }
                    }
                    break;
                case EnumTPItemList.PM_Unscheduled_EQ:
                case EnumTPItemList.PM_Unscheduled_EQ_Code:
                    foreach (var ctrl in this.pnlTable.Controls)
                    {
                        EnumTPItemList item = this.CheckLabel(ctrl, out System.Windows.Forms.Label _lbl);
                        if (item == EnumTPItemList.None) continue;

                        switch (item)
                        {
                            case EnumTPItemList.None: continue;
                            case EnumTPItemList.DOWN:
                            case EnumTPItemList.PM:
                            case EnumTPItemList.PM_Unscheduled:
                            case EnumTPItemList.PM_Unscheduled_EQ:
                            case EnumTPItemList.PM_Unscheduled_EQ_Code: this.SetLabelColor(_lbl, true); break;
                            default: this.SetLabelColor(_lbl, false); break;
                        }
                    }
                    break;
                case EnumTPItemList.CM:
                    foreach (var ctrl in this.pnlTable.Controls)
                    {
                        EnumTPItemList item = this.CheckLabel(ctrl, out System.Windows.Forms.Label _lbl);
                        if (item == EnumTPItemList.None) continue;

                        switch (item)
                        {
                            case EnumTPItemList.None: continue;
                            case EnumTPItemList.DOWN:
                            case EnumTPItemList.CM: this.SetLabelColor(_lbl, true); break;
                            default: this.SetLabelColor(_lbl, false); break;
                        }
                    }
                    break;
                case EnumTPItemList.CM_Setup_Code:
                case EnumTPItemList.CM_Setup:
                    foreach (var ctrl in this.pnlTable.Controls)
                    {
                        EnumTPItemList item = this.CheckLabel(ctrl, out System.Windows.Forms.Label _lbl);
                        if (item == EnumTPItemList.None) continue;

                        switch (item)
                        {
                            case EnumTPItemList.None: continue;
                            case EnumTPItemList.DOWN:
                            case EnumTPItemList.CM:
                            case EnumTPItemList.CM_Setup:
                            case EnumTPItemList.CM_Setup_Code: this.SetLabelColor(_lbl, true); break;
                            default: this.SetLabelColor(_lbl, false); break;
                        }
                    }
                    break;
                case EnumTPItemList.CM_EI_Code:
                case EnumTPItemList.CM_EI:
                    foreach (var ctrl in this.pnlTable.Controls)
                    {
                        EnumTPItemList item = this.CheckLabel(ctrl, out System.Windows.Forms.Label _lbl);
                        if (item == EnumTPItemList.None) continue;

                        switch (item)
                        {
                            case EnumTPItemList.None: continue;
                            case EnumTPItemList.DOWN:
                            case EnumTPItemList.CM:
                            case EnumTPItemList.CM_EI:
                            case EnumTPItemList.CM_EI_Code: this.SetLabelColor(_lbl, true); break;
                            default: this.SetLabelColor(_lbl, false); break;
                        }
                    }
                    break;
                case EnumTPItemList.CM_EE_Code:
                case EnumTPItemList.CM_EE:
                    foreach (var ctrl in this.pnlTable.Controls)
                    {
                        EnumTPItemList item = this.CheckLabel(ctrl, out System.Windows.Forms.Label _lbl);
                        if (item == EnumTPItemList.None) continue;

                        switch (item)
                        {
                            case EnumTPItemList.None: continue;
                            case EnumTPItemList.DOWN:
                            case EnumTPItemList.CM:
                            case EnumTPItemList.CM_EE:
                            case EnumTPItemList.CM_EE_Code: this.SetLabelColor(_lbl, true); break;
                            default: this.SetLabelColor(_lbl, false); break;
                        }
                    }
                    break;
                case EnumTPItemList.ModelChange:
                    foreach (var ctrl in this.pnlTable.Controls)
                    {
                        EnumTPItemList item = this.CheckLabel(ctrl, out System.Windows.Forms.Label _lbl);
                        if (item == EnumTPItemList.None) continue;

                        switch (item)
                        {
                            case EnumTPItemList.None: continue;
                            case EnumTPItemList.DOWN:
                            case EnumTPItemList.ModelChange:  this.SetLabelColor(_lbl, true); break;
                            default: this.SetLabelColor(_lbl, false); break;
                        }
                    }
                    break;
                case EnumTPItemList.ModelChange_Diff_Code:
                case EnumTPItemList.ModelChange_Diff:
                    foreach (var ctrl in this.pnlTable.Controls)
                    {
                        EnumTPItemList item = this.CheckLabel(ctrl, out System.Windows.Forms.Label _lbl);
                        if (item == EnumTPItemList.None) continue;

                        switch (item)
                        {
                            case EnumTPItemList.None: continue;
                            case EnumTPItemList.DOWN:
                            case EnumTPItemList.ModelChange:
                            case EnumTPItemList.ModelChange_Diff:
                            case EnumTPItemList.ModelChange_Diff_Code: this.SetLabelColor(_lbl, true); break;
                            default: this.SetLabelColor(_lbl, false); break;
                        }
                    }
                    break;
                case EnumTPItemList.ModelChange_Same_Code:
                case EnumTPItemList.ModelChange_Same:
                    foreach (var ctrl in this.pnlTable.Controls)
                    {
                        EnumTPItemList item = this.CheckLabel(ctrl, out System.Windows.Forms.Label _lbl);
                        if (item == EnumTPItemList.None) continue;

                        switch (item)
                        {
                            case EnumTPItemList.None: continue;
                            case EnumTPItemList.DOWN:
                            case EnumTPItemList.ModelChange:
                            case EnumTPItemList.ModelChange_Same:
                            case EnumTPItemList.ModelChange_Same_Code: this.SetLabelColor(_lbl, true); break;
                            default: this.SetLabelColor(_lbl, false); break;
                        }
                    }
                    break;
                case EnumTPItemList.Materials:
                    foreach (var ctrl in this.pnlTable.Controls)
                    {
                        EnumTPItemList item = this.CheckLabel(ctrl, out System.Windows.Forms.Label _lbl);
                        if (item == EnumTPItemList.None) continue;

                        switch (item)
                        {
                            case EnumTPItemList.None: continue;
                            case EnumTPItemList.DOWN:
                            case EnumTPItemList.Materials: this.SetLabelColor(_lbl, true); break;
                            default: this.SetLabelColor(_lbl, false); break;
                        }
                    }
                    break;
                case EnumTPItemList.Materials_Down_Code:
                case EnumTPItemList.Materials_Down:
                    foreach (var ctrl in this.pnlTable.Controls)
                    {
                        EnumTPItemList item = this.CheckLabel(ctrl, out System.Windows.Forms.Label _lbl);
                        if (item == EnumTPItemList.None) continue;

                        switch (item)
                        {
                            case EnumTPItemList.None: continue;
                            case EnumTPItemList.DOWN:
                            case EnumTPItemList.Materials:
                            case EnumTPItemList.Materials_Down:
                            case EnumTPItemList.Materials_Down_Code: this.SetLabelColor(_lbl, true); break;
                            default: this.SetLabelColor(_lbl, false); break;
                        }
                    }
                    break;
                case EnumTPItemList.Materials_Change_Code:
                case EnumTPItemList.Materials_Change:
                    foreach (var ctrl in this.pnlTable.Controls)
                    {
                        EnumTPItemList item = this.CheckLabel(ctrl, out System.Windows.Forms.Label _lbl);
                        if (item == EnumTPItemList.None) continue;

                        switch (item)
                        {
                            case EnumTPItemList.None: continue;
                            case EnumTPItemList.DOWN:
                            case EnumTPItemList.Materials:
                            case EnumTPItemList.Materials_Change:
                            case EnumTPItemList.Materials_Change_Code: this.SetLabelColor(_lbl, true); break;
                            default: this.SetLabelColor(_lbl, false); break;
                        }
                    }
                    break;
            }

            this.tmrRedraw.Start();
        }

        private void lblTPCode_MouseEnter(object sender, EventArgs e)
        {
            EnumTPItemList tpItem = this.CheckLabel(sender);

            this.m_CurrentMousePosition = tpItem;
        }

        private void lblTPCode_MouseLeave(object sender, EventArgs e)
        {
            EnumTPItemList tpItem = this.CheckLabel(sender);
            if (tpItem == EnumTPItemList.None) return;

            this.m_CurrentMousePosition = EnumTPItemList.None;
        }

        private void lblTPCode_Click(object sender, EventArgs e)
        {
            EnumTPItemList tpItem = this.CheckLabel(sender);
            if (tpItem == EnumTPItemList.None) return;

            CIMEnumeric.EnumTpCode tpCode = this.IsSelectedCode(tpItem);

            if(tpCode == CIMEnumeric.EnumTpCode.None) return;

            this.SelectedTpCode = tpCode;

            switch (tpCode)
            {
                case CIMEnumeric.EnumTpCode.BM_Manual_Code:
                    this.SelectedTpDescription = "BREAKDOWN_MENUAL";
                    break;
                case CIMEnumeric.EnumTpCode.PM_Scheduled_Code:
                    this.SelectedTpDescription = "REGULAR_PM";
                    break;
                case CIMEnumeric.EnumTpCode.PM_Unscheduled_EQ_Code:
                    this.SelectedTpDescription = "CHECK_EQUIPMENT";
                    break;
                case CIMEnumeric.EnumTpCode.PM_Unscheduled_QC_Code:
                    this.SelectedTpDescription = "CHECK_QUALITY";
                    break;
                case CIMEnumeric.EnumTpCode.CM_EE_Code:
                    this.SelectedTpDescription = "IMPROVE_EQUIPMENT_EE";
                    break;
                case CIMEnumeric.EnumTpCode.CM_EI_Code:
                    this.SelectedTpDescription = "IMPROVE_PROCESSING_EI";
                    break;
                case CIMEnumeric.EnumTpCode.CM_Setup_Code:
                    this.SelectedTpDescription = "SETUP_NEW_PRODUCT";
                    break;
                case CIMEnumeric.EnumTpCode.ModelChange_Same_Code:
                    this.SelectedTpDescription = "CHANGE_SAME_MODEL";
                    break;
                case CIMEnumeric.EnumTpCode.ModelChange_Diff_Code:
                    this.SelectedTpDescription = "CHANGE_DIFFERENT_MODEL";
                    break;
                case CIMEnumeric.EnumTpCode.Materials_Change_Code:
                    this.SelectedTpDescription = "CHANGE_MATERIAL";
                    break;
                case CIMEnumeric.EnumTpCode.Materials_Down_Code:
                    this.SelectedTpDescription = "DOWN_MATERIAL";
                    break;
                default:
                    this.SelectedTpDescription = string.Empty;
                    break;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void FormTpmLossCode_Load(object sender, EventArgs e)
        {
            this.lblDown.Tag = EnumTPItemList.DOWN;
            this.lblPM_Unscheduled.Tag = EnumTPItemList.PM_Unscheduled;
            this.lblPM_Scheduled_Code.Tag = EnumTPItemList.PM_Scheduled_Code;
            this.lblPM_Scheduled.Tag = EnumTPItemList.PM_Scheduled;
            this.lblPM.Tag = EnumTPItemList.PM;
            this.lblBM_Manual_Code.Tag = EnumTPItemList.BM_Manual_Code;
            this.lblBM_Manual.Tag = EnumTPItemList.BM_Manual;
            this.lblBM.Tag = EnumTPItemList.BM;
            this.lblMaterials_Down_Code.Tag = EnumTPItemList.Materials_Down_Code;
            this.lblMaterials_Down.Tag = EnumTPItemList.Materials_Down;
            this.lblMaterials_Change_Code.Tag = EnumTPItemList.Materials_Change_Code;
            this.lblMaterials_Change.Tag = EnumTPItemList.Materials_Change;
            this.lblMaterials.Tag = EnumTPItemList.Materials;
            this.lblModelChange_Diff_Code.Tag = EnumTPItemList.ModelChange_Diff_Code;
            this.lblModelChange_Diff.Tag = EnumTPItemList.ModelChange_Diff;
            this.lblModelChange_Same_Code.Tag = EnumTPItemList.ModelChange_Same_Code;
            this.lblModelChange_Same.Tag = EnumTPItemList.ModelChange_Same;
            this.lblModelChange.Tag = EnumTPItemList.ModelChange;
            this.lblCM_Setup_Code.Tag = EnumTPItemList.CM_Setup_Code;
            this.lblCM_Setup.Tag = EnumTPItemList.CM_Setup;
            this.lblCM_EI_Code.Tag = EnumTPItemList.CM_EI_Code;
            this.lblCM_EI.Tag = EnumTPItemList.CM_EI;
            this.lblCM_EE_Code.Tag = EnumTPItemList.CM_EE_Code;
            this.lblCM.Tag = EnumTPItemList.CM;
            this.lblPM_Unscheduled_QC_Code.Tag = EnumTPItemList.PM_Unscheduled_QC_Code;
            this.lblPM_Unscheduled_QC.Tag = EnumTPItemList.PM_Unscheduled_QC;
            this.lblPM_Unscheduled_EQ_Code.Tag = EnumTPItemList.PM_Unscheduled_EQ_Code;
            this.lblPM_Unscheduled_EQ.Tag = EnumTPItemList.PM_Unscheduled_EQ;
            this.lblCM_EE.Tag = EnumTPItemList.CM_EE;


            this.tmrRedraw.Start();

        }

    }
}
