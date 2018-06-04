//--------------------------------------------------------------------------------------------
// <copyright file="F15004.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F15004 Form Slice - AgencyFundMgmt 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 28-12-2006       krishna            Created
//*********************************************************************************/

namespace D1500
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
    using TerraScan.Infrastructure.Interface.Constants;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.UI.Controls;
    using System.Web.Services.Protocols;
    using TerraScan.Utilities;
    using System.Text.RegularExpressions;

    /// <summary>
    /// F15005 FormSlice - SubFundMgmt Functionality
    /// </summary>
    [SmartPart]
    public partial class F15004 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// variable Holds agencyMgmt details
        /// </summary>
        private F15004AgencyManagementData agencyMgmtDataset = new F15004AgencyManagementData();

        /// <summary>
        /// variable Holds the F15004 Controller instance
        /// </summary>
        private F15004Controller form15004Controll;

        /// <summary>
        /// Variable Holds the PageMode Types
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// variable Holds the edit permission value
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// variable holds the masterForm Number
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// variable holds the KeyId
        /// </summary>
        private int keyId;

        /// <summary>
        /// variable holds the slice permissionFields
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// Variable Holds the flag Value for PageLoad
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// Variable Holds the agencyId
        /// </summary>
        private int agencyId;

        /// <summary>
        /// Variable Holds the CLID
        /// </summary>
        private int clid;

        /// <summary>
        /// Used to Check Valid Email
        /// </summary>
        private string validEmail = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

        #endregion

        #region Contructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15004"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F15004(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15004"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        /// <param name="featureClassID">The feature class ID.</param>
        public F15004(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassID)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /////// <summary>
        /////// Declare the event FormSlice_FormCloseAlert        
        /////// </summary>
        ////[EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        ////public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form15004 controll.
        /// </summary>
        /// <value>The form15004 controll.</value>
        [CreateNew]
        public F15004Controller Form15004Controll
        {
            get { return this.form15004Controll as F15004Controller; }
            set { this.form15004Controll = value; }
        }

        /// <summary>
        /// Gets or sets the agency id.
        /// </summary>
        /// <value>The agency id.</value>
        public int AgencyId
        {
            get { return this.agencyId; }
            set { this.agencyId = value; }
        }

        #endregion

        #region Event SubScription

        /// <summary>
        /// Called when [D9030_ F9030_ set slice permission].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            try
            {
                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    if (this.agencyMgmtDataset.GetAgencyDetail.Rows.Count > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
                    }
                    else
                    {
                        //// Coding Added for the issue 4212 0n 30/5/2009.
                        //// Last Slice does not have a record also it will not return invalid slice
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            eventArgs.Data.FlagInvalidSliceKey = true;
                        }
                    }
                }

                if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                {
                    if (this.agencyMgmtDataset.GetAgencyDetail.Rows.Count > 0)
                    {
                        this.LockControls(false);
                    }
                    else
                    {
                        this.LockControls(true);
                    }
                }
                else
                {
                    this.LockControls(true);
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit) || (this.pageMode == TerraScanCommon.PageModeTypes.New))
            {
                if (this.slicePermissionField.editPermission || this.slicePermissionField.newPermission)
                {
                    SliceValidationFields sliceValidationFields = new SliceValidationFields();
                    sliceValidationFields.FormNo = eventArgs.Data;
                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
                }
            }
            else
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
            }
            ////this.AgencyNameTextBox.ReadOnly = true;
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save confirmed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, DataEventArgs<int> eventArgs)
        {
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit) || (this.pageMode == TerraScanCommon.PageModeTypes.New))
            {
                if (this.slicePermissionField.editPermission || this.slicePermissionField.newPermission)
                {
                    bool status = this.SaveAgencyRecord();
                    if (status)
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
            }
            else
            {
                //// ToDo : FormLoad Events should happen (refresh)
                this.LockControls(true);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ enable new method].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            {
                if (this.slicePermissionField.newPermission)
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.pageLoadStatus = true;
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    ////this.EnableControls(true);
                    ////this.AccountInfoPanel.Enabled = true;
                    this.LockControls(false);
                    this.ClearAgencyHeader();
                    this.PopulateDisbursementHistoryGridView();
                    this.agencyId = -1;
                    this.AgencyNameTextBox.Focus();
                    this.pageLoadStatus = false;
                    this.AgencyNameTextBox.Focus();
                    ////this.AgencyNameTextBox.ReadOnly = false;
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    this.LockControls(true);
                }
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            this.Cursor = Cursors.WaitCursor;

            if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
            {
                this.LockControls(false);
            }
            else
            {
                this.LockControls(true);
            }

            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.PopulateAgencyMgmtDetails(this.keyId);
            // Coding added for the issue 3909 by malliga on 21/5/2009
            // While canceling focus goes to extn B field.now it is avioded.
            this.ActiveControl = this.AgencyNamePanel;
            this.AgencyNamePanel.Focus();
            // Coding ends here
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.AgencyId = this.keyId;
                    this.PopulateAgencyMgmtDetails(this.keyId);
                    this.AgencyNameTextBox.Focus();
                    if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                    {
                        this.LockControls(false);
                    }
                    else
                    {
                        this.LockControls(true);
                    }
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Called when [D9030_ F9030_ reload after save].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_ReloadAfterSave(TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.D9030_F9030_ReloadAfterSave != null)
            {
                this.D9030_F9030_ReloadAfterSave(this, eventArgs);
            }
        }

        #endregion

        #region Form Events

        /// <summary>
        /// Handles the Load event of the F15004 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F15004_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.AgencyId = this.keyId;
                this.CustomizeDisbursementHistoryGridView();
                this.PopulateAgencyMgmtDetails(this.AgencyId);
                ////this.AgencyNameTextBox.ReadOnly = true;
                this.TabPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.TabPictureBox.Height, this.TabPictureBox.Width, "Tab", 28, 81, 128);
                this.ContactAPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ContactAPictureBox.Height, this.ContactAPictureBox.Width, "Contact A", 174, 150, 94);
                this.ContactBPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ContactBPictureBox.Height, this.ContactBPictureBox.Width, "Contact B", 174, 150, 94);
                this.DisbursementHistoryPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DisbursementHistoryPictureBox.Height, this.DisbursementHistoryPictureBox.Width, "Disbursements", 0, 51, 0);
                this.AgencyNameTextBox.Focus();
                ////this.SubFundsListPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SubFundsListPictureBox.Height, this.SubFundsListPictureBox.Width, "SubFund", 28, 81, 128);
                ////this.DisbursementHistoryPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DisbursementHistoryPictureBox.Height, this.DisbursementHistoryPictureBox.Width, "Disbursement History", 174, 150, 94);
                ////this.PopulateSubFundDetais();
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
        /// Handles the CellContentClick event of the DisbursementHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DisbursementHistoryGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex >= 0)
                {
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(1226);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = this.clid;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the DisbursementHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DisbursementHistoryGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < this.DisbursementHistoryGridView.OriginalRowCount && e.ColumnIndex == 0)
                {
                    this.clid = Convert.ToInt32(this.DisbursementHistoryGridView.Rows[e.RowIndex].Cells["CashLedgerID"].Value);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the DisbursementHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void DisbursementHistoryGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;
            try
            {
                if (e.ColumnIndex == this.DisbursementHistoryGridView.Columns[this.agencyMgmtDataset.ListDisbursementHistory.AmountColumn.ColumnName.ToString()].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.DisbursementHistoryGridView.Rows[e.RowIndex].Cells[this.agencyMgmtDataset.ListDisbursementHistory.AmountColumn.ColumnName.ToString()].Value.ToString().Trim()))
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

                if (e.ColumnIndex == this.DisbursementHistoryGridView.Columns[this.agencyMgmtDataset.ListDisbursementHistory.EntryDateColumn.ColumnName].Index)
                {
                    if (this.DisbursementHistoryGridView.Rows[e.RowIndex].Selected || this.DisbursementHistoryGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                    {
                        (DisbursementHistoryGridView.Rows[e.RowIndex].Cells[this.agencyMgmtDataset.ListDisbursementHistory.EntryDateColumn.ColumnName.ToString()] as DataGridViewLinkCell).LinkColor = Color.White;
                        (DisbursementHistoryGridView.Rows[e.RowIndex].Cells[this.agencyMgmtDataset.ListDisbursementHistory.EntryDateColumn.ColumnName.ToString()] as DataGridViewLinkCell).ActiveLinkColor = Color.White;
                        (DisbursementHistoryGridView.Rows[e.RowIndex].Cells[this.agencyMgmtDataset.ListDisbursementHistory.EntryDateColumn.ColumnName.ToString()] as DataGridViewLinkCell).VisitedLinkColor = Color.White;
                    }
                    else
                    {
                        (DisbursementHistoryGridView.Rows[e.RowIndex].Cells[this.agencyMgmtDataset.ListDisbursementHistory.EntryDateColumn.ColumnName.ToString()] as DataGridViewLinkCell).LinkColor = Color.Blue;
                        (DisbursementHistoryGridView.Rows[e.RowIndex].Cells[this.agencyMgmtDataset.ListDisbursementHistory.EntryDateColumn.ColumnName.ToString()] as DataGridViewLinkCell).ActiveLinkColor = Color.Red;
                        (DisbursementHistoryGridView.Rows[e.RowIndex].Cells[this.agencyMgmtDataset.ListDisbursementHistory.EntryDateColumn.ColumnName.ToString()] as DataGridViewLinkCell).VisitedLinkColor = Color.Blue;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the DisbursementHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void DisbursementHistoryGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.DisbursementHistoryGridView.OriginalRowCount == 0)
                {
                    this.DisbursementHistoryGridView.CurrentCell = null;
                }
                else
                {
                    TerraScanCommon.SetDataGridViewPosition(this.DisbursementHistoryGridView, 0);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region General Methods

        /// <summary>
        /// Customizes the disbursement history grid view.
        /// </summary>
        private void CustomizeDisbursementHistoryGridView()
        {
            this.DisbursementHistoryGridView.AutoGenerateColumns = false;
            this.DisbursementHistoryGridView.PrimaryKeyColumnName = this.agencyMgmtDataset.ListDisbursementHistory.CLIDColumn.ColumnName.ToString();
            this.DisbursementHistoryGridView.Columns["EntryDate"].DataPropertyName = this.agencyMgmtDataset.ListDisbursementHistory.EntryDateColumn.ColumnName.ToString();
            this.DisbursementHistoryGridView.Columns["Amount"].DataPropertyName = this.agencyMgmtDataset.ListDisbursementHistory.AmountColumn.ColumnName.ToString();
            this.DisbursementHistoryGridView.Columns["CashLedgerID"].DataPropertyName = this.agencyMgmtDataset.ListDisbursementHistory.CLIDColumn.ColumnName.ToString();
        }

        /// <summary>
        /// Populates the agency MGMT details.
        /// </summary>
        /// <param name="agencyIdentifier">The agency identifier.</param>
        private void PopulateAgencyMgmtDetails(int agencyIdentifier)
        {
            this.AgencyId = agencyIdentifier;
            this.pageLoadStatus = true;
            this.agencyMgmtDataset = this.form15004Controll.WorkItem.F15004_GetAgencyDetails(this.agencyId);
            this.GetAgencyDetails();
            this.PopulateDisbursementHistoryGridView();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.pageLoadStatus = false;
        }

        /// <summary>
        /// Populates the disbursement history grid view.
        /// </summary>
        private void PopulateDisbursementHistoryGridView()
        {
            int disbursementHistoryRowCount;

            if (this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                this.agencyMgmtDataset.ListDisbursementHistory.Rows.Clear();
            }

            disbursementHistoryRowCount = this.agencyMgmtDataset.ListDisbursementHistory.Rows.Count;
            this.DisbursementHistoryGridView.DataSource = this.agencyMgmtDataset.ListDisbursementHistory;

            if (disbursementHistoryRowCount > 0)
            {
                this.DisbursementHistoryGridView.Enabled = true;
                TerraScanCommon.SetDataGridViewPosition(this.DisbursementHistoryGridView, 0);
            }
            else
            {
                this.DisbursementHistoryGridView.Rows[0].Selected = false;
                this.DisbursementHistoryGridView.Enabled = false;
            }

            if (disbursementHistoryRowCount > this.DisbursementHistoryGridView.NumRowsVisible)
            {
                this.AgencyMgmtVScorrlBar.Visible = false;
            }
            else
            {
                this.AgencyMgmtVScorrlBar.Visible = true;
            }

            if (this.DisbursementHistoryGridView.OriginalRowCount == 0)
            {
                this.DisbursementHistoryGridView.CurrentCell = null;
            }
        }

        /// <summary>
        /// Saves the agency record.
        /// </summary>
        /// <returns>true or flase</returns>
        private bool SaveAgencyRecord()
        {
            try
            {
                F15004AgencyManagementData saveAgencyData = new F15004AgencyManagementData();
                F15004AgencyManagementData.GetAgencyDetailRow dr = saveAgencyData.GetAgencyDetail.NewGetAgencyDetailRow();
                saveAgencyData.GetAgencyDetail.Clear();

                int primaryKeyId = -1;
                this.Cursor = Cursors.WaitCursor;

                dr.AgencyName = this.AgencyNameTextBox.Text.Trim();
                decimal balance;
                Decimal.TryParse(this.DisbursementBalanceTextBox.Text.ToString().Trim(), out balance);
                dr.DisbursementBalance = balance;
                ////dr.DisbursementBalance = Convert.ToDecimal(this.DisbursementBalanceTextBox.Text.Trim());
                dr.Address1 = this.Address1TextBox.Text.Trim();
                dr.Address2 = this.Address2TextBox.Text.Trim();
                dr.City = this.CityTextBox.Text.Trim();
                dr.State = this.StateTextBox.Text.Trim();
                dr.Zip = this.ZipCodeTextBox.Text.Trim();

                dr.C1First = this.FirstNameContactATextBox.Text.Trim();
                dr.C1Last = this.LastNameContactATextBox.Text.Trim();
                dr.C1Email = this.EmailContactATextBox.Text.Trim();
                dr.C1Phone = this.PhoneContactATextBox.Text.Trim();
                dr.C1Extension = this.ExtnContactATextBox.Text.Trim();

                dr.C2First = this.FirstNameContactBTextBox.Text.Trim();
                dr.C2Last = this.LastNameContBTextBox.Text.Trim();
                dr.C2Email = this.EmailContactBTextBox.Text.Trim();
                dr.C2Phone = this.PhoneContactBTextBox.Text.Trim();
                dr.C2Extension = this.ExtnContactBTextBox.Text.Trim();

                saveAgencyData.GetAgencyDetail.Rows.Add(dr);
                DataSet tempDataSet = new DataSet("Root");
                tempDataSet.Tables.Add(saveAgencyData.GetAgencyDetail.Copy());
                tempDataSet.Tables[0].TableName = "Table";
                string tempxml = string.Empty;
                tempxml = tempDataSet.GetXml();

                if (this.agencyId == -1)
                {
                    primaryKeyId = this.form15004Controll.WorkItem.F15004_CreateOrEditAgencyDetails(-1, tempxml, TerraScanCommon.UserId);
                }
                else
                {
                    primaryKeyId = this.form15004Controll.WorkItem.F15004_CreateOrEditAgencyDetails(this.AgencyId, tempxml, TerraScanCommon.UserId);
                }

                ////this.PopulateAgencyMgmtDetails(primaryKeyId);
                if (primaryKeyId != -1)
                {
                    ////this.AgencyNameTextBox.ReadOnly = true;
                    SliceReloadActiveRecord currentSliceInfo;
                    currentSliceInfo.MasterFormNo = this.masterFormNo;
                    currentSliceInfo.SelectedKeyId = primaryKeyId;
                    ////to refresh the master form with the return keyid
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                }

                return true;
            }
            catch (Exception ex)
            {
                ////ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Gets the agency details.
        /// </summary>
        private void GetAgencyDetails()
        {
            if (this.agencyMgmtDataset.GetAgencyDetail.Rows.Count > 0)
            {
                this.AgencyNameTextBox.Text = this.agencyMgmtDataset.GetAgencyDetail.Rows[0][this.agencyMgmtDataset.GetAgencyDetail.AgencyNameColumn.ColumnName].ToString();
                this.DisbursementBalanceTextBox.Text = this.agencyMgmtDataset.GetAgencyDetail.Rows[0][this.agencyMgmtDataset.GetAgencyDetail.DisbursementBalanceColumn.ColumnName].ToString();
                this.Address1TextBox.Text = this.agencyMgmtDataset.GetAgencyDetail.Rows[0][this.agencyMgmtDataset.GetAgencyDetail.Address1Column.ColumnName].ToString();
                this.Address2TextBox.Text = this.agencyMgmtDataset.GetAgencyDetail.Rows[0][this.agencyMgmtDataset.GetAgencyDetail.Address2Column.ColumnName].ToString();
                this.CityTextBox.Text = this.agencyMgmtDataset.GetAgencyDetail.Rows[0][this.agencyMgmtDataset.GetAgencyDetail.CityColumn.ColumnName].ToString();
                this.StateTextBox.Text = this.agencyMgmtDataset.GetAgencyDetail.Rows[0][this.agencyMgmtDataset.GetAgencyDetail.StateColumn.ColumnName].ToString();
                this.ZipCodeTextBox.Text = this.agencyMgmtDataset.GetAgencyDetail.Rows[0][this.agencyMgmtDataset.GetAgencyDetail.ZipColumn.ColumnName].ToString();

                this.FirstNameContactATextBox.Text = this.agencyMgmtDataset.GetAgencyDetail.Rows[0][this.agencyMgmtDataset.GetAgencyDetail.C1FirstColumn.ColumnName].ToString();
                this.LastNameContactATextBox.Text = this.agencyMgmtDataset.GetAgencyDetail.Rows[0][this.agencyMgmtDataset.GetAgencyDetail.C1LastColumn.ColumnName].ToString();
                this.EmailContactATextBox.Text = this.agencyMgmtDataset.GetAgencyDetail.Rows[0][this.agencyMgmtDataset.GetAgencyDetail.C1EmailColumn.ColumnName].ToString();
                this.PhoneContactATextBox.Text = this.agencyMgmtDataset.GetAgencyDetail.Rows[0][this.agencyMgmtDataset.GetAgencyDetail.C1PhoneColumn.ColumnName].ToString();
                this.ExtnContactATextBox.Text = this.agencyMgmtDataset.GetAgencyDetail.Rows[0][this.agencyMgmtDataset.GetAgencyDetail.C1ExtensionColumn.ColumnName].ToString();

                this.FirstNameContactBTextBox.Text = this.agencyMgmtDataset.GetAgencyDetail.Rows[0][this.agencyMgmtDataset.GetAgencyDetail.C2FirstColumn.ColumnName].ToString();
                this.LastNameContBTextBox.Text = this.agencyMgmtDataset.GetAgencyDetail.Rows[0][this.agencyMgmtDataset.GetAgencyDetail.C2LastColumn.ColumnName].ToString();
                this.EmailContactBTextBox.Text = this.agencyMgmtDataset.GetAgencyDetail.Rows[0][this.agencyMgmtDataset.GetAgencyDetail.C2EmailColumn.ColumnName].ToString();
                this.PhoneContactBTextBox.Text = this.agencyMgmtDataset.GetAgencyDetail.Rows[0][this.agencyMgmtDataset.GetAgencyDetail.C2PhoneColumn.ColumnName].ToString();
                this.ExtnContactBTextBox.Text = this.agencyMgmtDataset.GetAgencyDetail.Rows[0][this.agencyMgmtDataset.GetAgencyDetail.C2ExtensionColumn.ColumnName].ToString();

                this.PopulateDisbursementHistoryGridView();
            }
            else
            {
                this.LockControls(true);
                this.ClearAgencyHeader();
            }

            //if (!this.formMasterPermissionEdit && !this.slicePermissionField.editPermission)
            //{
            //    this.LockControls(true);
            //}
        }

        /// <summary>
        /// Clears the agency header.
        /// </summary>
        private void ClearAgencyHeader()
        {
            this.AgencyNameTextBox.Text = string.Empty;
            this.DisbursementBalanceTextBox.Text = string.Empty;
            this.Address1TextBox.Text = string.Empty;
            this.Address2TextBox.Text = string.Empty;
            this.CityTextBox.Text = string.Empty;
            this.StateTextBox.Text = string.Empty;
            this.ZipCodeTextBox.Text = string.Empty;

            this.FirstNameContactATextBox.Text = string.Empty;
            this.LastNameContactATextBox.Text = string.Empty;
            this.EmailContactATextBox.Text = string.Empty;
            this.PhoneContactATextBox.Text = string.Empty;
            this.ExtnContactATextBox.Text = string.Empty;

            this.FirstNameContactBTextBox.Text = string.Empty;
            this.LastNameContBTextBox.Text = string.Empty;
            this.EmailContactBTextBox.Text = string.Empty;
            this.PhoneContactBTextBox.Text = string.Empty;
            this.ExtnContactBTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>sliceValidationFields</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            if (string.IsNullOrEmpty(this.AgencyNameTextBox.Text.Trim()))
            {
                DialogResult result = MessageBox.Show(SharedFunctions.GetResourceString("15004RequiredFieldMissing"), "TerraScan T2 - Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    this.AgencyNameTextBox.Focus();
                    sliceValidationFields.DisableNewMethod = true;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                }
            }
            else if (!string.IsNullOrEmpty(this.EmailContactATextBox.Text.Trim()) && !this.CheckValidEmailID(this.EmailContactATextBox.Text.Trim()))
            {
                this.EmailContactATextBox.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("EmailValidation");
            }
            else if (!string.IsNullOrEmpty(this.EmailContactBTextBox.Text.Trim()) && !this.CheckValidEmailID(this.EmailContactBTextBox.Text.Trim()))
            {
                this.EmailContactBTextBox.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("EmailValidation");
            }
            else if (!this.CheckDuplicateAgency())
            {
                DialogResult result = MessageBox.Show(SharedFunctions.GetResourceString("DuplicateAgencyExists"), "TerraScan T2 - Duplicate Agency", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    sliceValidationFields.DisableNewMethod = false;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                }
                else
                {
                    this.AgencyNameTextBox.Focus();
                    sliceValidationFields.DisableNewMethod = true;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                }
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Fileds the edit process.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FiledEditProcess(object sender, EventArgs e)
        {
            ////This fires for all Textboxes TextChanged event
            if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                if (!this.pageLoadStatus)   //// && this.pageMode == TerraScanCommon.PageModeTypes.View
                {
                    if (!string.Equals(this.pageMode, TerraScanCommon.PageModeTypes.Edit))
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                    }
                }
            }
        }

        /// <summary>
        /// Checks the duplicate agency.
        /// </summary>
        /// <returns>true or false</returns>
        private bool CheckDuplicateAgency()
        {
            try
            {
                int errorId = -1;
                errorId = this.form15004Controll.WorkItem.F15004_CheckDuplicateAgency(this.agencyId, this.AgencyNameTextBox.Text.Trim());
                if (errorId != -1)
                {
                    return true;
                }

                return false;
            }
            catch (SoapException ex)
            {
                //// ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return false;
            }
        }

        /// <summary>
        /// Checks the valid email ID.
        /// </summary>
        /// <param name="sourceEmailId">The source email id.</param>
        /// <returns>true or flase </returns>
        private bool CheckValidEmailID(string sourceEmailId)
        {
            try
            {
                System.Text.RegularExpressions.Match match = Regex.Match(sourceEmailId, this.validEmail, RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SoapException ex)
            {
                //// ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                return false;
            }
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="lockValue">if set to <c>true</c> [lock value].</param>
        private void LockControls(bool lockValue)
        {
            this.AgencyNameTextBox.Enabled = !lockValue;
            this.AgencyNamePanel.Enabled = !lockValue;
            this.DisbursementBalanceTextBox.Enabled = !lockValue;
            this.DisbursementBalancePanel.Enabled = !lockValue;
            this.DisbursementBalanceTextBox.LockKeyPress = true;
            this.Address1TextBox.Enabled = !lockValue;
            this.Address1Panel.Enabled = !lockValue;
            this.Address2TextBox.Enabled = !lockValue;
            this.Address2Panel.Enabled = !lockValue;
            this.CityTextBox.Enabled = !lockValue;
            this.CityPanel.Enabled = !lockValue;

            this.ZipCodeTextBox.Enabled = !lockValue;
            this.ZipCodePanel.Enabled = !lockValue;
            this.StateTextBox.Enabled = !lockValue;
            this.StatePanel.Enabled = !lockValue;

            this.FirstNameContactATextBox.Enabled = !lockValue;
            this.FirstNameContactAPanel.Enabled = !lockValue;

            this.LastNameContactATextBox.Enabled = !lockValue;
            this.LastNameContactAPanel.Enabled = !lockValue;

            this.EmailContactATextBox.Enabled = !lockValue;
            this.EmailContactAPanel.Enabled = !lockValue;
            this.PhoneContactATextBox.Enabled = !lockValue;
            this.PhoneContactAPanel.Enabled = !lockValue;
            this.ExtnContactATextBox.Enabled = !lockValue;
            this.ExtContactAPanel.Enabled = !lockValue;

            this.FirstNameContactBTextBox.Enabled = !lockValue;
            this.FirstNameContactBPanel.Enabled = !lockValue;

            this.LastNameContBTextBox.Enabled = !lockValue;
            this.LastNameContBPanel.Enabled = !lockValue;
            this.EmailContactBTextBox.Enabled = !lockValue;
            this.EmailContactBPanel.Enabled = !lockValue;
            this.PhoneContactBTextBox.Enabled = !lockValue;
            this.PhoneContactBLabel.Enabled = !lockValue;

            this.ExtnContactBTextBox.Enabled = !lockValue;
            this.ExtContactBPanel.Enabled = !lockValue;

            this.AgencyNameTextBox.LockKeyPress = lockValue;
            this.DisbursementBalanceTextBox.LockKeyPress = true;
            this.Address1TextBox.LockKeyPress = lockValue;
            this.Address2TextBox.LockKeyPress = lockValue;
            this.CityTextBox.LockKeyPress = lockValue;
            this.ZipCodeTextBox.LockKeyPress = lockValue;
            this.StateTextBox.LockKeyPress = lockValue;

            this.FirstNameContactATextBox.LockKeyPress = lockValue;
            this.LastNameContactATextBox.LockKeyPress = lockValue;
            this.EmailContactATextBox.LockKeyPress = lockValue;
            this.PhoneContactATextBox.LockKeyPress = lockValue;
            this.ExtnContactATextBox.LockKeyPress = lockValue;

            this.FirstNameContactBTextBox.LockKeyPress = lockValue;
            this.LastNameContBTextBox.LockKeyPress = lockValue;
            this.EmailContactBTextBox.LockKeyPress = lockValue;
            this.PhoneContactBTextBox.LockKeyPress = lockValue;
            this.ExtnContactBTextBox.LockKeyPress = lockValue;
        }

        #endregion
    }
}
