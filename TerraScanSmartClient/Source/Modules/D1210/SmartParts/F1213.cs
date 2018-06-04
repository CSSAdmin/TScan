//--------------------------------------------------------------------------------------------
// <copyright file="F1213.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F1212 DepositHistory and Functionality
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 05-09-2006       Shiva              Created
// 7-2-2007         ranjani            1213 - 7.1 - issues fixed
//*********************************************************************************/

namespace D1210
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.Utilities;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using TerraScan.BusinessEntities;
    using System.Web.Services.Protocols;
    using TerraScan.Common.Reports;
    using System.Collections;
    using System.Configuration;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// F1213 UserInterFace Inherits BaseSmartpart
    /// </summary>
    [SmartPart]
    public partial class F1213 : BaseSmartPart
    {
        #region Private Variables

        /// <summary>
        /// Variable Instance for F1212Controller
        /// </summary>
        private F1213Controller form1213Controll;

        /// <summary>
        /// formLabelInfo string array
        /// </summary>
        private string[] formLabelInfo = new string[2];

        /// <summary>
        /// Holds the AdditionalOperationSmartPart
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart;

        /// <summary>
        /// Variable Holds the DataSet depositHistoryData
        /// </summary>
        private DepositHistoryData depositHistoryDataSet;

        /// <summary>
        /// Variable Holds the SearchDepositHistoryDataSet
        /// </summary>
        private DepositHistoryData searchDepositHistoryDataSet;

        /// <summary>
        /// Binding Source
        /// </summary>
        private BindingSource bindingSource;

        /// <summary>
        /// Search Binding Source
        /// </summary>
        private BindingSource searchBindingSource;

        /// <summary>
        /// Created Boolean to Find emptyRecord
        /// </summary>        
        private bool emptyRecord;

        /// <summary>
        /// reportOptionalParameter
        /// </summary>
        private int rowCount;

        /// <summary>
        /// Holds the Search Value
        /// </summary>
        private bool searchValue;

        /// <summary>
        /// Variable Holds the CLID
        /// </summary>
        private int clid = -1;

        /// <summary>
        /// variable to identify isClear Flag
        /// </summary>
        private bool clearFlag;

        /// <summary>
        /// check whether the record set - reduced or not
        /// </summary>
        private bool reducedRecordSet;

        #endregion

        #region Constructor

        /// <summary>
        /// F1213Constructor
        /// </summary>
        public F1213()
        {
            InitializeComponent();
            this.DepositHistoryPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DepositHistoryPictureBox.Height, this.DepositHistoryPictureBox.Width, "Deposits", 28, 81, 128);
        }

        #endregion

        #region Published Events

        /// <summary>
        /// EventPublication for FormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form1213 controll.
        /// </summary>
        /// <value>The form1213 controll.</value>
        [CreateNew]
        public F1213Controller Form1213Controll
        {
            get { return this.form1213Controll as F1213Controller; }
            set { this.form1213Controll = value; }
        }

        /// <summary>
        /// Gets or sets the clid.
        /// </summary>
        /// <value>The clid.</value>
        public int Clid
        {
            get
            {
                return this.clid;
            }

            set
            {
                this.clid = value;
                this.additionalOperationSmartPart.KeyId = this.clid;
            }
        }

        /// <summary>
        /// Gets or sets the row count.
        /// </summary>
        /// <value>The row count.</value>
        public int RowCount
        {
            get { return this.rowCount; }
            set { this.rowCount = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [search value].
        /// </summary>
        /// <value><c>true</c> if [search value]; otherwise, <c>false</c>.</value>
        public bool SearchValue
        {
            get { return this.searchValue; }
            set { this.searchValue = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [clear flag].
        /// </summary>
        /// <value><c>true</c> if [clear flag]; otherwise, <c>false</c>.</value>
        public bool ClearFlag
        {
            get { return this.clearFlag; }
            set { this.clearFlag = value; }
        }

        #endregion

        #region Form Events

        /// <summary>
        /// Handles the Load event of the F1213 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1213_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkSpaces();
                this.LoadDepositHistoryGrid();
                this.SearchButton.Enabled = false;
                this.ClearButton.Enabled = false;

                if (!this.emptyRecord)
                {
                    this.DepositHistoryDataGrid.Rows[0].Selected = true;
                    this.DepositHistoryDataGrid.Focus();
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the DepositHistoryDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DepositHistoryDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < this.DepositHistoryDataGrid.OriginalRowCount && e.ColumnIndex == 4)
                {
                    int rowid = -1;
                    int clearedBit = -1;
                    
                    if (this.SearchValue)
                    {
                        this.DepositHistoryDataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        rowid = this.searchBindingSource.Find("CLID", this.DepositHistoryDataGrid.Rows[e.RowIndex].Cells[1].Value.ToString());
                        this.clid = Convert.ToInt32(this.searchDepositHistoryDataSet.ListDepositHistoryTable.Rows[rowid][this.searchDepositHistoryDataSet.ListDepositHistoryTable.CLIDColumn.ToString()]);
                        clearedBit = Convert.ToInt32(this.searchDepositHistoryDataSet.ListDepositHistoryTable.Rows[rowid][this.searchDepositHistoryDataSet.ListDepositHistoryTable.IsClearedColumn.ToString()]);
                    }
                    else
                    {
                        this.DepositHistoryDataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        rowid = this.bindingSource.Find("CLID", this.DepositHistoryDataGrid.Rows[e.RowIndex].Cells[1].Value.ToString());
                        clearedBit = Convert.ToInt32(this.depositHistoryDataSet.ListDepositHistoryTable.Rows[rowid][this.depositHistoryDataSet.ListDepositHistoryTable.IsClearedColumn.ToString()]);
                        this.clid = Convert.ToInt32(this.depositHistoryDataSet.ListDepositHistoryTable.Rows[rowid][this.depositHistoryDataSet.ListDepositHistoryTable.CLIDColumn.ToString()]);
                    }

                    if (this.clid != -1 && clearedBit != -1)
                    {
                        if (clearedBit != 0)
                        {
                            this.form1213Controll.WorkItem.UpdateDepositHistory(this.Clid, TerraScanCommon.UserId);
                        }
                        else
                        {
                            this.form1213Controll.WorkItem.UpdateDepositHistory(this.Clid, 0);
                        }
                    }
                }
            }
            catch (SoapException SoapEx)
            {
                ExceptionManager.ManageException(SoapEx, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (DataException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the DepositHistoryDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DepositHistoryDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.GridClick(e.RowIndex);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the DepositHistoryDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DepositHistoryDataGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.GridClick(e.RowIndex);
                this.SetAttachmentCommentsCount();
            }
            catch (SoapException SoapExc)
            {
                ExceptionManager.ManageException(SoapExc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.GetWhereClause();
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ClearFields();
                this.LoadDepositHistoryGrid();
                this.SearchButton.Enabled = false;
                this.ClearButton.Enabled = false;
                this.reducedRecordSet = false;

                if (!this.emptyRecord)
                {
                    this.DepositHistoryDataGrid.Rows[0].Selected = true;
                    this.DepositHistoryDataGrid.Focus();
                }
            }
            catch (SoapException SoapEx)
            {
                ExceptionManager.ManageException(SoapEx, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the SearchAccountNameCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SearchAccountNameCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(this.SearchAccountNameCombo.SelectedValue) != 0)
                {
                    this.SearchRegisterIDTextBox.Text = this.SearchAccountNameCombo.SelectedValue.ToString();
                }
                else
                {
                    this.SearchRegisterIDTextBox.Text = string.Empty;
                }

                this.EnableSearch();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the SearchIsClearedComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SearchIsClearedComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int tempClearedBit = -1;
            try
            {
                switch (Convert.ToInt32(this.SearchIsClearedComboBox.SelectedIndex))
                {
                    case 1:
                        tempClearedBit = 1;
                        break;
                    case 2:
                        tempClearedBit = 0;
                        break;
                    default:
                        break;
                }

                if (tempClearedBit != -1)
                {
                    this.SearchIsClearedTextBox.Text = tempClearedBit.ToString();
                }
                else
                {
                    this.SearchIsClearedTextBox.Text = string.Empty;
                }

                this.clearFlag = true;
                this.EnableSearch();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Handles the Click event of the DepositHistoryButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DepositHistoryButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////changed the parameter type from string to int
                TerraScanCommon.ShowReport(121201, Report.ReportType.Preview);
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

        /// <summary>
        /// Handles the CellFormatting event of the DepositHistoryDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void DepositHistoryDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;
            try
            {
                //// Only paint if desired, formattable column

                if (e.ColumnIndex == this.DepositHistoryDataGrid.Columns["Amount"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            e.Value = outDecimal.ToString("#,##0.00");
                            e.FormattingApplied = true;
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
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the DepositHistoryAuditLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void DepositHistoryAuditLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (this.Clid > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(90101);
                    formInfo.optionalParameters = new object[2];
                    formInfo.optionalParameters[0] = this.Tag;
                    formInfo.optionalParameters[1] = this.Clid;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }

                ////Hashtable reportFileIdHashTable = new Hashtable();
                ////this.Cursor = Cursors.WaitCursor;
                ////if (this.Clid != -1)
                ////{
                ////    reportFileIdHashTable.Clear();
                ////    reportFileIdHashTable.Add("CLID", this.Clid);
                ////    ////changed the parameter type from string to int
                ////    TerraScanCommon.ShowReport(90101, TerraScan.Common.Reports.Report.ReportType.Preview, reportFileIdHashTable);
                ////}
                ////else
                ////{
                ////    MessageBox.Show(SharedFunctions.GetResourceString("+"), "Terrascan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////}
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

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            if (this.form1213Controll.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1213Controll.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1213Controll.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            if (this.form1213Controll.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.CommentsdeckWorkspace.Show(this.form1213Controll.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart));
            }
            else
            {
                this.CommentsdeckWorkspace.Show(this.form1213Controll.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart));
            }

            this.formLabelInfo[0] = SharedFunctions.GetResourceString("1213DepositHistory"); ////Properties.Resources.FormName;
            this.formLabelInfo[1] = string.Empty;
            this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));

            this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.form1213Controll.WorkItem.SmartParts[SmartPartNames.AdditionalOperationSmartPart];
            this.additionalOperationSmartPart.ParentWorkItem = this.form1213Controll.WorkItem;
            this.additionalOperationSmartPart.ParentFormId = 1210;
            this.additionalOperationSmartPart.CurrntFormId = 1210;
        }

        /// <summary>
        /// Loads the afiidavit list grid.
        /// </summary>
        private void LoadDepositHistoryGrid()
        {
            this.CustomizeDataGrid();
            this.depositHistoryDataSet = new DepositHistoryData();
            this.bindingSource = new BindingSource();
            this.depositHistoryDataSet = this.form1213Controll.WorkItem.ListDepositHistoryDetails();
            this.RowCount = this.depositHistoryDataSet.ListDepositHistoryTable.Rows.Count;
            this.DepositHistoryDataGrid.DataSource = this.depositHistoryDataSet.ListDepositHistoryTable;
            this.bindingSource.DataSource = this.depositHistoryDataSet.ListDepositHistoryTable.Copy();
            this.InitAccountNameCombo();
            this.InitIsClearedCombo();

            if (this.RowCount == 0)
            {
                this.emptyRecord = true;
                this.SearchButton.Enabled = false;
                this.DepositHistoryAuditLink.Enabled = false;
                this.additionalOperationSmartPart.Enabled = false;
                this.DepositHistoryDataGrid.Rows[0].Selected = false;
                this.DepositHistoryDataGrid.Enabled = false;
            }
            else
            {
                this.emptyRecord = false;
                this.DepositHistoryAuditLink.Text = SharedFunctions.GetResourceString("1213DepositHistoryAuditLink") + " " + this.DepositHistoryDataGrid.Rows[0].Cells[this.depositHistoryDataSet.ListDepositHistoryTable.CLIDColumn.ToString()].Value.ToString();
                this.SetDataGridViewPosition(0);
                this.DepositHistoryAuditLink.Enabled = true;
                this.additionalOperationSmartPart.Enabled = true;
                this.DepositHistoryDataGrid.Enabled = true;
            }

            if (this.RowCount > this.DepositHistoryDataGrid.NumRowsVisible)
            {
                this.DepositHistoryVScrollBar.Enabled = true;
                this.DepositHistoryVScrollBar.Visible = false;
            }
            else
            {
                this.DepositHistoryVScrollBar.Enabled = false;
                this.DepositHistoryVScrollBar.Visible = true;
            }
        }

        /// <summary>
        /// Sets the data grid view position to firstrow.
        /// </summary>
        /// <param name="firstRow">The first row.</param>
        private void SetDataGridViewPosition(int firstRow)
        {
            if (this.RowCount > 0)
            {
                this.DepositHistoryDataGrid.Rows[Convert.ToInt32(firstRow)].Selected = true;
                this.DepositHistoryDataGrid.CurrentCell = this.DepositHistoryDataGrid[0, Convert.ToInt32(firstRow)];
            }
        }

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizeDataGrid()
        {
            this.DepositHistoryDataGrid.AllowUserToResizeColumns = false;
            this.DepositHistoryDataGrid.AutoGenerateColumns = false;
            this.DepositHistoryDataGrid.AllowUserToResizeRows = false;
            this.DepositHistoryDataGrid.StandardTab = true;
            this.DepositHistoryDataGrid.Columns[4].Resizable = DataGridViewTriState.False;
            this.DepositHistoryDataGrid.Columns["Amount"].DefaultCellStyle.Font = new Font("Courier New", 8.25F, FontStyle.Bold);
            this.DepositHistoryDataGrid.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DepositHistoryDataGrid.PrimaryKeyColumnName = "CLID";

            this.DepositHistoryDataGrid.Columns[0].Width = 128;
            this.DepositHistoryDataGrid.Columns[1].Width = 100;
            this.DepositHistoryDataGrid.Columns[2].Width = 180;
            this.DepositHistoryDataGrid.Columns[3].Width = 131;
            this.DepositHistoryDataGrid.Columns[4].Width = 82;
            this.DepositHistoryDataGrid.Columns[5].Width = 126;

            this.DepositHistoryDataGrid.Columns[0].DataPropertyName = "EntryDate";
            this.DepositHistoryDataGrid.Columns[1].DataPropertyName = "CLID";
            this.DepositHistoryDataGrid.Columns[2].DataPropertyName = "AccountName";
            this.DepositHistoryDataGrid.Columns[3].DataPropertyName = "Name_Display";
            this.DepositHistoryDataGrid.Columns[4].DataPropertyName = "IsCleared";
            this.DepositHistoryDataGrid.Columns[5].DataPropertyName = "Amount";
        }

        /// <summary>
        /// Enables the search.
        /// </summary>
        private void EnableSearch()
        {
            if (this.clearFlag)
            {
                if (!string.IsNullOrEmpty(this.SearchEntryDateTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.SearchCLIDTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.SearchAccountNameCombo.Text.Trim()) || !string.IsNullOrEmpty(this.SearchUserTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.SearchIsClearedComboBox.Text.Trim()) || !string.IsNullOrEmpty(this.SearchAmountTextBox.Text.Trim()))
                {
                    this.SearchButton.Enabled = true;
                    this.ClearButton.Enabled = true;
                }
                else
                {
                    this.SearchButton.Enabled = false;
                    ////disabled if already cleared
                    if (!this.reducedRecordSet)
                    {
                        this.ClearButton.Enabled = false;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(this.SearchEntryDateTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.SearchCLIDTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.SearchAccountNameCombo.Text.Trim()) || !string.IsNullOrEmpty(this.SearchUserTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.SearchAmountTextBox.Text.Trim()))
                {
                    this.SearchButton.Enabled = true;
                    this.ClearButton.Enabled = true;
                }
                else
                {
                    this.SearchButton.Enabled = false;
                    ////disabled if already cleared
                    if (!this.reducedRecordSet)
                    {
                        this.ClearButton.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the SearchTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    {
                        break;
                    }

                case Keys.Tab:
                    {
                        break;
                    }

                default:
                    {
                        this.EnableSearch();
                        break;
                    }
            }
        }

        /// <summary>
        /// Clears the fields.
        /// </summary>
        private void ClearFields()
        {
            this.SearchCLIDTextBox.Text = string.Empty;
            this.SearchEntryDateTextBox.Text = string.Empty;
            this.SearchAmountTextBox.Text = string.Empty;
            this.SearchUserTextBox.Text = string.Empty;
            this.SearchRegisterIDTextBox.Text = string.Empty;
            this.SearchIsClearedTextBox.Text = string.Empty;
            this.SearchValue = false;
        }

        /// <summary>
        /// Grids the click.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void GridClick(int rowIndex)
        {
            if (!this.emptyRecord)
            {
                if (rowIndex >= 0)
                {
                    this.DepositHistoryAuditLink.Text = SharedFunctions.GetResourceString("1213DepositHistoryAuditLink") + " " + this.DepositHistoryDataGrid.Rows[rowIndex].Cells[this.depositHistoryDataSet.ListDepositHistoryTable.CLIDColumn.ToString()].Value.ToString();
                    int.TryParse(this.DepositHistoryDataGrid.Rows[rowIndex].Cells[this.depositHistoryDataSet.ListDepositHistoryTable.CLIDColumn.ToString()].Value.ToString(), out this.clid);
                }
            }
        }

        /// <summary>
        /// Gets the where clause.
        /// </summary>
        private void GetWhereClause()
        {
            ////set reducedRecordSet
            this.reducedRecordSet = true;
            this.searchDepositHistoryDataSet = new DepositHistoryData();
            this.searchBindingSource = new BindingSource();
            string returnValue = String.Empty;
            StringBuilder whereClause = new StringBuilder(String.Empty);
            bool previousValueExists = false;
            bool invalidQuery = false;

            Control[] controlArray = new Control[] { this.SearchCLIDTextBox, this.SearchEntryDateTextBox, this.SearchRegisterIDTextBox, this.SearchUserTextBox, this.SearchIsClearedTextBox, this.SearchAmountTextBox };

            for (int i = 0; i < controlArray.Length; i++)
            {
                Control queryControl = controlArray.GetValue(i) as Control;

                if (!String.IsNullOrEmpty(queryControl.Text.Trim()))
                {
                    //// ParseSqlWhereCondition returns string containing parsed query value 
                    if (!Convert.ToBoolean(string.Compare(queryControl.Name, this.SearchRegisterIDTextBox.Name)))
                    {
                        returnValue = SharedFunctions.FormatSqlWhereCondition(queryControl.Tag.ToString(), "'" + queryControl.Text.Trim().ToUpper() + "'", this.searchDepositHistoryDataSet.ListAccountName.Columns[queryControl.Tag.ToString()].DataType);
                    }
                    else if (!Convert.ToBoolean(string.Compare(queryControl.Name, this.SearchIsClearedTextBox.Name)))
                    {
                        returnValue = SharedFunctions.FormatSqlWhereCondition(queryControl.Tag.ToString(), "'" + queryControl.Text.Trim().ToUpper() + "'", this.searchDepositHistoryDataSet.ListDepositHistoryTable.Columns[queryControl.Tag.ToString()].DataType);
                    }
                    else
                    {
                        returnValue = SharedFunctions.FormatSqlWhereCondition(queryControl.Tag.ToString(), "'" + queryControl.Text.Trim().ToUpper() + "'", this.searchDepositHistoryDataSet.ListDepositHistoryTable.Columns[queryControl.Tag.ToString()].DataType);
                    }

                    if (!String.IsNullOrEmpty(returnValue))
                    {
                        if (previousValueExists)
                        {
                            whereClause.Append(" AND ");
                        }

                        previousValueExists = true;
                        whereClause.Append("(");
                        whereClause.Append(returnValue);
                        whereClause.Append(")");
                    }
                    else
                    {
                        invalidQuery = true;
                        break;
                    }
                }
            }

            try
            {
                int recordsCount = 0;
                if (invalidQuery)
                {
                    this.DepositHistoryDataGrid.DataSource = null;
                    this.searchDepositHistoryDataSet.ListDepositHistoryTable.Clear();
                    recordsCount = this.searchDepositHistoryDataSet.ListDepositHistoryTable.Rows.Count;
                }
                else
                {
                    this.searchDepositHistoryDataSet = this.form1213Controll.WorkItem.GetDepositHistorySearchResult(1213, whereClause.ToString());
                    recordsCount = this.searchDepositHistoryDataSet.ListDepositHistoryTable.Rows.Count;
                    this.DepositHistoryDataGrid.DataSource = this.searchDepositHistoryDataSet.ListDepositHistoryTable;
                    this.searchBindingSource.DataSource = this.searchDepositHistoryDataSet.ListDepositHistoryTable.Copy();
                    this.SearchValue = true;
                }

                if (recordsCount > 0)
                {
                    this.emptyRecord = false;
                    this.DepositHistoryAuditLink.Enabled = true;
                    this.DepositHistoryAuditLink.Text = SharedFunctions.GetResourceString("1213DepositHistoryAuditLink") + " " + this.DepositHistoryDataGrid.Rows[0].Cells[this.depositHistoryDataSet.ListDepositHistoryTable.CLIDColumn.ToString()].Value.ToString();
                    this.DepositHistoryDataGrid.Enabled = true;
                    this.DepositHistoryDataGrid.Rows[0].Selected = true;
                    this.DepositHistoryDataGrid.Focus();
                    this.additionalOperationSmartPart.Enabled = true;
                }
                else
                {
                    this.emptyRecord = true;
                    this.DepositHistoryDataGrid.CurrentCell = null;
                    this.DepositHistoryAuditLink.Enabled = false;
                    this.DepositHistoryDataGrid.Rows[0].Selected = false;
                    this.DepositHistoryDataGrid.Enabled = false;
                    this.additionalOperationSmartPart.Enabled = false;
                }

                if (this.searchDepositHistoryDataSet.ListDepositHistoryTable.Rows.Count > 20)
                {
                    this.DepositHistoryVScrollBar.Enabled = true;
                    this.DepositHistoryVScrollBar.Visible = false;
                }
                else
                {
                    this.DepositHistoryVScrollBar.Enabled = false;
                    this.DepositHistoryVScrollBar.Visible = true;
                }
            }
            catch (SoapException)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("InvalidQuery") + SharedFunctions.GetResourceString("QueryModificationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);                       
            }
        }

        /// <summary>
        /// Fills the account name combo.
        /// </summary>
        private void InitAccountNameCombo()
        {
            DepositHistoryData.ListAccountNameDataTable listAccountNames = new DepositHistoryData.ListAccountNameDataTable();
            DataRow dr = listAccountNames.NewRow();
            dr[this.depositHistoryDataSet.ListAccountName.RegisterIDColumn.ColumnName] = "0";
            dr[this.depositHistoryDataSet.ListAccountName.AccountNameColumn.ColumnName] = string.Empty;
            dr[this.depositHistoryDataSet.ListAccountName.AccountNumberColumn.ColumnName] = "0";
            dr[this.depositHistoryDataSet.ListAccountName.IsDefaultColumn.ColumnName] = false;
            listAccountNames.Rows.Add(dr);
            listAccountNames.Merge(this.form1213Controll.WorkItem.ListAccountNames());
            this.SearchAccountNameCombo.ValueMember = this.depositHistoryDataSet.ListAccountName.RegisterIDColumn.ColumnName;
            this.SearchAccountNameCombo.DisplayMember = this.depositHistoryDataSet.ListAccountName.AccountNameColumn.ColumnName;
            this.SearchAccountNameCombo.DataSource = listAccountNames;
            this.SearchAccountNameCombo.SelectedIndex = 0;
        }

        /// <summary>
        /// Inits the is cleared combo.
        /// </summary>
        private void InitIsClearedCombo()
        {
            this.SearchIsClearedComboBox.Items.Clear();
            this.SearchIsClearedComboBox.Items.Insert(0, "Either");
            this.SearchIsClearedComboBox.Items.Insert(1, "Cleared");
            this.SearchIsClearedComboBox.Items.Insert(2, "Not Cleared");
            this.SearchIsClearedComboBox.SelectedIndex = 0;
            this.clearFlag = false;
        }

        /// <summary>
        /// Sets the attachment comments count.
        /// </summary>
        private void SetAttachmentCommentsCount()
        {
            ////Display Comments Count in the Comments Buttons  and attachment count in the attachment Buttons       
            AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);

            if (this.clid != -999)
            {
                this.additionalOperationSmartPart.KeyId = this.clid;
                additionalOperationCountEntity.AttachmentCount = this.form1213Controll.WorkItem.GetAttachmentCount(1210, this.clid, TerraScanCommon.UserId);
                CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.form1213Controll.WorkItem.GetCommentsCount(1210, this.clid, TerraScanCommon.UserId);
                if (commentsCountDataTable.Rows.Count > 0)
                {
                    additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                    additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                }
            }

            this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;
        }
     
        /// <summary>
        /// Handles the MouseHover event of the ToolTip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ToolTip_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Control tempControl = (Control)sender;
                if (!String.IsNullOrEmpty(tempControl.Text))
                {
                    if (tempControl.Text.Length > 20)
                    {
                        this.DepositHistoryToolTip.RemoveAll();
                        this.DepositHistoryToolTip.SetToolTip(tempControl, tempControl.Text);
                    }
                    else
                    {
                        this.DepositHistoryToolTip.RemoveAll();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        private void SearchAccountNameCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SearchButton.Enabled = true;
            this.ClearButton.Enabled = true;
        }

        private void SearchIsClearedComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SearchButton.Enabled = true;
            this.ClearButton.Enabled = true;
        }
    }
}
