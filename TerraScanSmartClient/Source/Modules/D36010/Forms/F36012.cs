// -------------------------------------------------------------------------------------------
// <copyright file="F36012.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F36012 MI Selection Form.
// </summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 19/2/2009        M.Sadha Shivudu    ///Created
// -------------------------------------------------------------------------------------------

namespace D36010
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using System.Configuration;

    /// <summary>
    /// F36012 Class
    /// </summary>
    public partial class F36012 : BasePage
    {
        #region Instance Variable

        /// <summary>
        /// form36012Control
        /// </summary>
        private F36012Controller form36012Control;

        /// <summary>
        /// miscImprovementCatalogChoiceDataSet
        /// </summary>
        private F36010MiscImprovementCatalog miscImprovementCatalogChoiceDataSet;

        /// <summary>
        /// currentFieldCatalogChoiceDataSet
        /// </summary>
        private F36010MiscImprovementCatalog currentFieldCatalogChoiceDataSet;

        /// <summary>
        /// nonCurrentFieldCatalogChoiceDataSet
        /// </summary>
        private F36010MiscImprovementCatalog nonCurrentFieldCatalogChoiceDataSet;

        /// <summary>
        /// deletedRowsCatalogChoiceDataSet
        /// </summary>
        private F36010MiscImprovementCatalog deletedRowsCatalogChoiceDataSet = new F36010MiscImprovementCatalog();

        /// <summary>
        /// pageMode variable used to set the mode of page new/edit/view.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// miscImprovementCatalogPermissionFields
        /// </summary>
        private PermissionFields miscImprovementCatalogPermissionFields;

        /// <summary>
        /// pageLoadStatus variable is used to store the flag value for PageLoad.
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// field number
        /// </summary>
        private string fieldNum = string.Empty;

        /// <summary>
        /// miscCodeId
        /// </summary>
        private int miscCodeId;

        /// <summary>
        /// edit variable is used to store the fundandsubfundgridview edit status.
        /// </summary>
        private bool edit;

        /// <summary>
        /// selectedRowIndex
        /// </summary>
        private int selectedRowIndex = -1;

        /// <summary>
        /// recordUpdated
        /// </summary>
        private bool recordUpdated;

        #endregion Instance Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F36012"/> class.
        /// </summary>
        public F36012()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F36012"/> class.
        /// </summary>
        /// <param name="miscCodeId">The misc code id.</param>
        /// <param name="fieldNum">The field num.</param>
        /// <param name="catalogChoiceDataTable">The catalog choice data table.</param>
        /// <param name="miscImprovementCatalogPermissionFields">The misc improvement catalog permission fields.</param>
        public F36012(int miscCodeId, string fieldNum, F36010MiscImprovementCatalog.GetMiscCatalogChoiceDataTable catalogChoiceDataTable, PermissionFields miscImprovementCatalogPermissionFields)
        {
            InitializeComponent();
            this.miscImprovementCatalogChoiceDataSet = new F36010MiscImprovementCatalog();
            this.miscImprovementCatalogChoiceDataSet.Merge(catalogChoiceDataTable);
            this.miscCodeId = miscCodeId;
            this.fieldNum = fieldNum;
            this.miscImprovementCatalogPermissionFields = miscImprovementCatalogPermissionFields;
        }

        #endregion Constructor

        #region Property

        /// <summary>
        /// Gets or sets the form36012 control.
        /// </summary>
        /// <value>The form36012 control.</value>
        [CreateNew]
        public F36012Controller Form36012Control
        {
            get { return this.form36012Control as F36012Controller; }
            set { this.form36012Control = value; }
        }

        /// <summary>
        /// Gets or sets the misc improvement catalog choice data set.
        /// </summary>
        /// <value>The misc improvement catalog choice data set.</value>
        public F36010MiscImprovementCatalog MiscImprovementCatalogChoiceDataSet
        {
            get
            {
                return this.miscImprovementCatalogChoiceDataSet;
            }

            set
            {
                this.miscImprovementCatalogChoiceDataSet = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [record updated].
        /// </summary>
        /// <value><c>true</c> if [record updated]; otherwise, <c>false</c>.</value>
        public bool RecordUpdated
        {
            get
            {
                return this.recordUpdated;
            }

            set
            {
                this.recordUpdated = value;
            }
        }

        #endregion Property

        #region Form Events

        /// <summary>
        /// Handles the Load event of the F36012 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F36012_Load(object sender, EventArgs e)
        {
            try
            {
                this.pageLoadStatus = true;
                this.CustomizeMISelectionGridView();
                this.LoadCatalogChoiceGrid(this.fieldNum);
                this.MiscSelectionSaveButton.Enabled = false;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.pageLoadStatus = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the MiscSelectionSaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MiscSelectionSaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.CheckErrors())
                {
                    this.CommitMiscCatalogSelectionChanges();
                    this.MiscSelectionSaveButton.Enabled = false;
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the MiscSelectionCloseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MiscSelectionCloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the F36012 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void F36012_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!e.CloseReason.Equals(CloseReason.ApplicationExitCall))
                {
                    if (e.CloseReason.Equals(CloseReason.UserClosing))
                    {
                        if (this.MiscSelectionSaveButton.Enabled)
                        {
                            switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.Text + " ?", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                            {
                                case DialogResult.Yes:
                                    {
                                        if (this.CheckErrors())
                                        {
                                            e.Cancel = true;
                                        }
                                        else
                                        {
                                            this.CommitMiscCatalogSelectionChanges();
                                            this.UpdateMiscCatalogSelectionDataSet();
                                            this.DialogResult = DialogResult.OK;
                                            e.Cancel = false;
                                        }

                                        break;
                                    }

                                case DialogResult.No:
                                    {
                                        this.RollBackMiscCatalogSelectionChanges();
                                        this.UpdateMiscCatalogSelectionDataSet();
                                        this.DialogResult = DialogResult.OK;
                                        e.Cancel = false;
                                        break;
                                    }

                                case DialogResult.Cancel:
                                    {
                                        e.Cancel = true;
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            this.UpdateMiscCatalogSelectionDataSet();
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Form Events

        #region GridView Events

        /// <summary>
        /// Handles the RowEnter event of the MISelectionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void MISelectionGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool hasValues = false;
                if (e.RowIndex >= 1)
                {
                    if ((string.IsNullOrEmpty(this.MISelectionGridView.Rows[(e.RowIndex - 1)].Cells[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemNameColumn.ColumnName].Value.ToString().Trim())))
                    {
                        if (e.RowIndex + 1 < this.MISelectionGridView.RowCount)
                        {
                            for (int i = e.RowIndex; i < this.MISelectionGridView.RowCount; i++)
                            {
                                if (this.MISelectionGridView.Rows[i].Cells[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemNameColumn.ColumnName].Value != null && !String.IsNullOrEmpty(this.MISelectionGridView.Rows[i].Cells[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemNameColumn.ColumnName].Value.ToString().Trim()))
                                {
                                    hasValues = true;
                                    break;
                                }
                            }

                            if (hasValues)
                            {
                                this.MISelectionGridView[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemNameColumn.ColumnName, e.RowIndex].ReadOnly = false;
                                this.MISelectionGridView[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemValueColumn.ColumnName, e.RowIndex].ReadOnly = false;
                                this.MISelectionGridView.Rows[e.RowIndex].Selected = true;
                            }
                            else
                            {
                                this.MISelectionGridView[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemNameColumn.ColumnName, e.RowIndex].ReadOnly = true;
                                this.MISelectionGridView[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemValueColumn.ColumnName, e.RowIndex].ReadOnly = true;
                                this.MISelectionGridView.Rows[e.RowIndex].Selected = true;
                            }
                        }
                        else
                        {
                            this.MISelectionGridView[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemNameColumn.ColumnName, e.RowIndex].ReadOnly = true;
                            this.MISelectionGridView[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemValueColumn.ColumnName, e.RowIndex].ReadOnly = true;
                            this.MISelectionGridView.Rows[e.RowIndex].Selected = true;
                        }
                    }
                    else
                    {
                        this.MISelectionGridView[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemNameColumn.ColumnName, e.RowIndex].ReadOnly = false;
                        this.MISelectionGridView[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemValueColumn.ColumnName, e.RowIndex].ReadOnly = false;
                        this.MISelectionGridView.Rows[e.RowIndex].Selected = true;
                    }
                }

                //// Need to Get Confirmation
                if (e.RowIndex == 0)
                {
                    this.MISelectionGridView[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemNameColumn.ColumnName, e.RowIndex].ReadOnly = false;
                    this.MISelectionGridView[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemValueColumn.ColumnName, e.RowIndex].ReadOnly = false;
                    this.MISelectionGridView.Rows[e.RowIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellBeginEdit event of the MISelectionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void MISelectionGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                this.edit = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the MISelectionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void MISelectionGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
                e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                e.Control.Validated += new EventHandler(this.Control_Validated);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validated event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_Validated(object sender, EventArgs e)
        {
            try
            {
                this.MISelectionGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the MISelectionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void MISelectionGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.edit)
                {
                    this.edit = false;

                    this.MISelectionGridView.Rows[e.RowIndex].Cells[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.FieldNumColumn.ColumnName].Value = Convert.ToByte(this.fieldNum);
                    this.MISelectionGridView.Rows[e.RowIndex].Cells[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.MICodeIDColumn.ColumnName].Value = this.miscCodeId;
                    this.MISelectionGridView.Rows[e.RowIndex].Cells[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.IsCommitedColumn.ColumnName].Value = false;

                    if (string.IsNullOrEmpty(this.MISelectionGridView.Rows[e.RowIndex].Cells[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.MIChoiceIDColumn.ColumnName].Value.ToString()))
                    {
                        this.MISelectionGridView.Rows[e.RowIndex].Cells[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.MIChoiceIDColumn.ColumnName].Value = 0;
                    }

                    if (string.IsNullOrEmpty(this.MISelectionGridView.Rows[e.RowIndex].Cells[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemValueColumn.ColumnName].Value.ToString()))
                    {
                        this.MISelectionGridView.Rows[e.RowIndex].Cells[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemValueColumn.ColumnName].Value = 0;
                    }
                    else
                    {
                        decimal outDecimal;
                        decimal maxItemValue = 9999999999999999.99M;
                        decimal.TryParse(this.MISelectionGridView.Rows[e.RowIndex].Cells[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemValueColumn.ColumnName].Value.ToString(), out outDecimal);
                        if (outDecimal > maxItemValue)
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("F36012_MaxValue"), SharedFunctions.GetResourceString("Log"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.MISelectionGridView.Rows[e.RowIndex].Cells[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemValueColumn.ColumnName].Value = 0;
                        }
                    }

                    if (this.MISelectionGridView.OriginalRowCount >= this.MISelectionGridView.NumRowsVisible)
                    {
                        if (!Convert.ToBoolean(this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Rows[this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Rows.Count - 1][this.MISelectionGridView.EmptyRecordColumnName]))
                        {
                            this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Rows.Add(this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.NewRow());
                            this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Rows[this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Rows.Count - 1][this.MISelectionGridView.EmptyRecordColumnName] = true;
                            this.MISelectionGridVerticalScroll.Visible = false;
                            TerraScanCommon.SetDataGridViewPosition(this.MISelectionGridView, e.RowIndex + 1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellParsing event of the MISelectionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void MISelectionGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    //// Check for the ItemName
                    if (e.ColumnIndex.Equals(this.MISelectionGridView.Columns[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemNameColumn.ColumnName].Index))
                    {
                        //// Only paint if text provided, Only paint if desired text is in cell
                        if (e.Value != null)
                        {
                            string tempvalue = e.Value.ToString().Trim();
                            if (string.IsNullOrEmpty(tempvalue))
                            {
                                e.Value = string.Empty;
                            }

                            e.Value = tempvalue;
                            e.ParsingApplied = true;
                            this.MISelectionGridView.RefreshEdit();
                        }
                    }

                    //// Check for the valid ItemValue 
                    decimal outDecimal;
                    if (e.ColumnIndex.Equals(this.MISelectionGridView.Columns[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemValueColumn.ColumnName].Index))
                    {
                        //// Only paint if text provided, Only paint if desired text is in cell
                        if (e.Value != null)
                        {
                            decimal.TryParse(e.Value.ToString(), out outDecimal);
                            e.Value = outDecimal;
                            e.ParsingApplied = true;
                            this.MISelectionGridView.RefreshEdit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the MISelectionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void MISelectionGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                decimal outDecimal;

                if (e.ColumnIndex.Equals(this.MISelectionGridView.Columns[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemValueColumn.ColumnName].Index))
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null)
                    {
                        if (!e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.MISelectionGridView.Rows[e.RowIndex].Cells[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemValueColumn.ColumnName].Value.ToString().Trim()))
                        {
                            string val = e.Value.ToString();
                            if (Decimal.TryParse(val, out outDecimal))
                            {
                                if (outDecimal.ToString().Contains("-"))
                                {
                                    e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                                    e.CellStyle.ForeColor = Color.Green;
                                }
                                else
                                {
                                    e.Value = outDecimal.ToString("#,##0.00");
                                    e.FormattingApplied = true;
                                }
                            }
                            else
                            {
                                e.Value = "0.00 ";
                            }
                        }
                        else
                        {
                            e.Value = "";
                        }
                    }
                }

                if (MISelectionGridView.Rows[e.RowIndex].Cells[this.MIChoiceID.Name].Value != null)
                {
                    int miscChoiceId;
                    int.TryParse(MISelectionGridView.Rows[e.RowIndex].Cells[this.MIChoiceID.Name].Value.ToString(), out miscChoiceId);
                    if (miscChoiceId > 0)
                    {
                        (MISelectionGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewTextBoxCell).ToolTipText = "MIChoiceID [" + miscChoiceId.ToString() + "]";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowHeaderMouseClick event of the MISelectionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void MISelectionGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.MISelectionGridView.CurrentCell != null)
                {
                    this.selectedRowIndex = e.RowIndex;
                }
                else
                {
                    this.selectedRowIndex = -1;
                }

                TerraScanCommon.SetDataGridViewPosition(this.MISelectionGridView, e.RowIndex);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the MISelectionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void MISelectionGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (e.KeyValue == 46 && this.selectedRowIndex != -1)
                {
                    if (this.MISelectionGridView.CurrentCell.RowIndex.Equals(this.selectedRowIndex))
                    {
                        if (Convert.ToBoolean(this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Rows[this.selectedRowIndex][this.MISelectionGridView.EmptyRecordColumnName]))
                        {
                            return;
                        }

                        //// move the deleted row into deltedDataSet for retaining on cancel
                        int miscChoiceId;
                        string itemName;
                        decimal itemValue;

                        this.SetEditRecord();

                        int.TryParse(this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Rows[this.selectedRowIndex][this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.MIChoiceIDColumn.ColumnName].ToString(), out miscChoiceId);
                        decimal.TryParse(this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Rows[this.selectedRowIndex][this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.ItemValueColumn.ColumnName].ToString(), out itemValue);
                        itemName = this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Rows[this.selectedRowIndex][this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.ItemNameColumn.ColumnName].ToString();
                        this.deletedRowsCatalogChoiceDataSet.GetMiscCatalogChoice.AddGetMiscCatalogChoiceRow(miscChoiceId, this.miscCodeId, Convert.ToByte(this.fieldNum), itemName, itemValue, true);

                        this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Rows[this.selectedRowIndex].Delete();
                        this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.AcceptChanges();

                        if (this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Rows.Count < this.MISelectionGridView.NumRowsVisible)
                        {
                            this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.AddGetMiscCatalogChoiceRow(this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.NewGetMiscCatalogChoiceRow());
                            this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Rows[this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Rows.Count - 1][this.MISelectionGridView.EmptyRecordColumnName] = true;
                        }

                        if (this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Rows.Count > this.MISelectionGridView.NumRowsVisible)
                        {
                            this.MISelectionGridVerticalScroll.Visible = false;
                        }
                        else
                        {
                            this.MISelectionGridVerticalScroll.Visible = true;
                        }

                        if (this.selectedRowIndex > 0)
                        {
                            TerraScanCommon.SetDataGridViewPosition(this.MISelectionGridView, this.selectedRowIndex - 1);
                        }
                        else
                        {
                            TerraScanCommon.SetDataGridViewPosition(this.MISelectionGridView, 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion GridView Events

        #region Private Methods

        /// <summary>
        /// Customizes the MI selection grid view.
        /// </summary>
        private void CustomizeMISelectionGridView()
        {
            this.MISelectionGridView.AutoGenerateColumns = false;

            this.MISelectionGridView.Columns.Add(this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.IsCommitedColumn.ColumnName, this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.IsCommitedColumn.ColumnName);
            this.MISelectionGridView.Columns[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.IsCommitedColumn.ColumnName].Visible = false;
            this.MISelectionGridView.Columns[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.IsCommitedColumn.ColumnName].DataPropertyName = this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.IsCommitedColumn.ColumnName;

            this.MIChoiceID.DataPropertyName = this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.MIChoiceIDColumn.ColumnName;
            this.MICodeID.DataPropertyName = this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.MICodeIDColumn.ColumnName;
            this.FieldNum.DataPropertyName = this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.FieldNumColumn.ColumnName;
            this.ItemName.DataPropertyName = this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemNameColumn.ColumnName;
            this.ItemValue.DataPropertyName = this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemValueColumn.ColumnName;
        }

        /// <summary>
        /// Loads the catalog choice grid.
        /// </summary>
        /// <param name="fieldNum">The field num.</param>
        private void LoadCatalogChoiceGrid(string fieldNum)
        {
            string currentFieldCatalogChoiceCond = this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.FieldNumColumn.ColumnName + " = " + fieldNum;
            DataRow[] currentFieldCatalogChoiceRows = this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.Select(currentFieldCatalogChoiceCond);
            this.currentFieldCatalogChoiceDataSet = new F36010MiscImprovementCatalog();

            if (currentFieldCatalogChoiceRows.Length > 0)
            {
                this.currentFieldCatalogChoiceDataSet.Merge(currentFieldCatalogChoiceRows);
            }

            string nonCurrentFieldCatalogChoiceCond = this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.FieldNumColumn.ColumnName + " <> " + fieldNum;
            DataRow[] nonCurrentFieldCatalogChoiceRows = this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.Select(nonCurrentFieldCatalogChoiceCond);

            this.nonCurrentFieldCatalogChoiceDataSet = new F36010MiscImprovementCatalog();

            if (nonCurrentFieldCatalogChoiceRows.Length > 0)
            {
                this.nonCurrentFieldCatalogChoiceDataSet.Merge(nonCurrentFieldCatalogChoiceRows);
            }

            this.MISelectionGridView.DataSource = this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.DefaultView;

            if (this.MISelectionGridView.OriginalRowCount >= this.MISelectionGridView.NumRowsVisible)
            {
                this.MISelectionGridVerticalScroll.Visible = false;
            }
            else
            {
                this.MISelectionGridVerticalScroll.Visible = true;
            }

            if (this.MISelectionGridView.OriginalRowCount >= this.MISelectionGridView.NumRowsVisible)
            {
                if (!Convert.ToBoolean(this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Rows[this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Rows.Count - 1][this.MISelectionGridView.EmptyRecordColumnName]))
                {
                    this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Rows.Add(this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.NewRow());
                    this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Rows[this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Rows.Count - 1][this.MISelectionGridView.EmptyRecordColumnName] = true;
                    this.MISelectionGridVerticalScroll.Visible = false;
                }
            }
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.miscImprovementCatalogPermissionFields.editPermission)
            {
                if (!this.pageLoadStatus)
                {
                    if (!string.Equals(this.pageMode, TerraScanCommon.PageModeTypes.Edit))
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.MiscSelectionSaveButton.Enabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <returns>error status</returns>
        private bool CheckErrors()
        {
            foreach (DataRow row in this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Rows)
            {
                bool flagIsEmptyRecord;
                bool.TryParse(row[this.MISelectionGridView.EmptyRecordColumnName].ToString(), out flagIsEmptyRecord);
                if (!flagIsEmptyRecord)
                {
                    if (string.IsNullOrEmpty(row[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemNameColumn.ColumnName].ToString())
                        || string.IsNullOrEmpty(row[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.ItemValueColumn.ColumnName].ToString()))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("RequiredFieldMissing"), ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Commits the misc catalog selection changes.
        /// </summary>
        private void CommitMiscCatalogSelectionChanges()
        {
            foreach (DataRow row in this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Rows)
            {
                bool flagIsCommited;
                bool flagIsEmptyRecord;
                bool.TryParse(row[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.IsCommitedColumn.ColumnName].ToString(), out flagIsCommited);
                bool.TryParse(row[this.MISelectionGridView.EmptyRecordColumnName].ToString(), out flagIsEmptyRecord);

                if (!flagIsCommited && !flagIsEmptyRecord)
                {
                    row[this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.IsCommitedColumn.ColumnName] = true;
                }
            }

            this.recordUpdated = true;
            this.deletedRowsCatalogChoiceDataSet.GetMiscCatalogChoice.Clear();
            this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.AcceptChanges();
        }

        /// <summary>
        /// Rolls the back misc catalog selection changes.
        /// </summary>
        private void RollBackMiscCatalogSelectionChanges()
        {
            this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.RejectChanges();
            this.currentFieldCatalogChoiceDataSet.Merge(this.deletedRowsCatalogChoiceDataSet);
        }

        /// <summary>
        /// Saves the misc catalog selection.
        /// </summary>
        private void UpdateMiscCatalogSelectionDataSet()
        {
            this.miscImprovementCatalogChoiceDataSet.GetMiscCatalogChoice.Clear();
            this.miscImprovementCatalogChoiceDataSet.Merge(this.nonCurrentFieldCatalogChoiceDataSet);
            this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Columns.Remove(this.MISelectionGridView.EmptyRecordColumnName);

            ////get the committed rows only
            string commitedRowsFilterCond = this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.IsCommitedColumn.ColumnName + " = " + bool.TrueString;
            DataRow[] committedRows = this.currentFieldCatalogChoiceDataSet.GetMiscCatalogChoice.Select(commitedRowsFilterCond);

            if (committedRows.Length > 0)
            {
                this.miscImprovementCatalogChoiceDataSet.Merge(committedRows);
            }
        }

        #endregion Private Methods
    }
}