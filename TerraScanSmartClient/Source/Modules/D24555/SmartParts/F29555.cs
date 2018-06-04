//--------------------------------------------------------------------------------------------
// <copyright file="F29555.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F29555. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 23 September 2015     Priyadharshini.R    Created to implement Schedule Sale Tracking functionality
//*********************************************************************************/
namespace D24555
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.Common;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common.Reports;
    using System.Web.Services.Protocols;
    using System.Globalization;
    using TerraScan.Infrastructure.Interface.Constants;
    using D24555.Properties; 
    #endregion

    #region Usercontrol
    [SmartPart]
    public partial class F29555 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// masterFormNo Local variable
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int keyId;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// dsScheduleSaleTrackingDataSet
        /// </summary>
        F29555PersonalPropertySaleData dsScheduleSaleTrackingDataSet = new F29555PersonalPropertySaleData();

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// Instance of F29555 Controller to call the WorkItem
        /// </summary>
        private F29555Controller form29555Controller;

        /// <summary>
        /// isshift
        /// </summary>
        private bool isshift;

        /// <summary>
        /// saleId
        /// </summary>
        private int? saleId;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;
        /// <summary>
        /// dtComboBox
        /// </summary>
        F29555PersonalPropertySaleData.f29555_pclst_DeedTypesDataTable dtComboBox = new F29555PersonalPropertySaleData.f29555_pclst_DeedTypesDataTable();
        /// <summary>
        /// scheduleId
        /// </summary>
        private int? scheduleId;
        /// <summary>
        /// isNewSchedule
        /// </summary>
        private bool isNewSchedule = false;
        /// <summary>
        /// isNewLoad
        /// </summary>
        private bool isNewLoad = true;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;
        /// <summary>
        /// rollYear
        /// </summary>
        private int rollYear;
        /// <summary>
        /// ScheduleIDs
        /// </summary>
        private string ScheduleIDs = string.Empty;
        /// <summary>
        /// scheduleTable
        /// </summary>
        private DataTable scheduleTable = new DataTable();
        /// <summary>
        /// ListDataTable
        /// </summary>
        F29555PersonalPropertySaleData.f29555_pcget_SaleSchedulesAndOwnersDataTable ListDataTable = new F29555PersonalPropertySaleData.f29555_pcget_SaleSchedulesAndOwnersDataTable();
        /// <summary>
        /// ScheduleRollYearTable
        /// </summary>
        F1404ScheduleSelectionData.ScheduleRollYearDataTableDataTable ScheduleRollYearTable = new F1404ScheduleSelectionData.ScheduleRollYearDataTableDataTable();
        /// <summary>
        /// newOwnersTable
        /// </summary>
        F29555PersonalPropertySaleData.f29555_pcget_PSaleOwnersDataTable newOwnersTable = new F29555PersonalPropertySaleData.f29555_pcget_PSaleOwnersDataTable();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="F29555"/> class.
        /// </summary>
        public F29555()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F29555"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F29555(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.PersonalPropertySalePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PersonalPropertySalePictureBox.Height, this.PersonalPropertySalePictureBox.Width, tabText, red, green, blue);

        }
        #endregion

        #region Eventpublication

        /// <summary>
        /// display record id
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_DisplayNavigatedRecord, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<RecordNavigationEntity>> DisplayNavigatedRecord;

        /// <summary>
        /// Declare the event SetActiveRecord        
        /// </summary> 
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetActiveRecord;

        /// <summary>
        /// event publication for getting the form status
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_GetFormStatus, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<string>> GetFormStatus;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

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
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// Get Cancel Button
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> GetCancelButton;

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Declare the event D84700_F84722_OnSave_SetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84700_F84722_OnSave_SetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> FormSlice_OnSave_SetKeyId;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Event publication to intimate and set system snapshot in query engine
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9033_SetSystemSnapshotEvent, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<LoadSystemSnapShotDetails>> D9030_F9033_SetSystemSnapshotEvent;

        #endregion Eventpublication

        #region Properties

        /// <summary>
        /// Gets or sets the F29555 control.
        /// </summary>
        /// <value>The F29555 control.</value>
        [CreateNew]
        public F29555Controller F29555Control
        {
            get { return this.form29555Controller as F29555Controller; }
            set { this.form29555Controller = value; }
        }
        #endregion Properties

        #region End Date Events
        /// <summary>
        /// Handles the CloseUp event of the EnddatedateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EnddatedateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.SaleDateTextBox.Text = this.EnddatedateTimePicker.Text;
                this.ParentForm.ActiveControl = SaleDateTextBox;
                this.ParentForm.ActiveControl.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the EnddatedateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void EnddatedateTimePicker_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.isshift = e.Shift;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the EnddatedateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void EnddatedateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int keyCode = (int)e.KeyChar;
                if (keyCode == 9)
                {
                    SendKeys.Send("{Esc}");
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the EnddatePicturebox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EnddatePicturebox_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(SaleDateTextBox.Text.Trim()))
                {
                    this.EnddatedateTimePicker.Value = Convert.ToDateTime(SaleDateTextBox.Text);
                }
                else
                {
                    this.EnddatedateTimePicker.Value = DateTime.Today;
                }

                this.EnddatedateTimePicker.Focus();
                SendKeys.Send("{F4}");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the EnddateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EnddateTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.EditRecord();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void OwnerGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    ((ComboBox)e.Control).SelectionChangeCommitted -= new EventHandler(this.OwnerGrid_SelectionChangeCommitted);
                    ((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(this.OwnerGrid_SelectionChangeCommitted);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the DistributionGridView combobox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>        
        private void OwnerGrid_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.EditRecord();
                int isBuyer = 0;
                // int.TryParse(((DataGridViewComboBoxEditingControl)sender).Text Value.ToString().Trim(), out isBuyer);


                if (((System.Windows.Forms.DataGridViewComboBoxEditingControl)sender).Text.Trim().Equals("Grantor"))
                {
                    isBuyer = 0;
                }
                else
                {
                    isBuyer = 1;
                }

                int ownerId = 0;
                if (this.OwnerGridView.Rows[this.OwnerGridView.CurrentRowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerIDColumn.ColumnName].Value != null)
                {
                    int.TryParse(this.OwnerGridView.Rows[this.OwnerGridView.CurrentRowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerIDColumn.ColumnName].Value.ToString().Trim(), out ownerId);
                }

                if (ownerId > 0)
                {
                    DataView ownerDataView = null;
                    if (this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Rows.Count > 0)
                    {
                        ownerDataView = new DataView(this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners);
                    }

                    ownerDataView.RowFilter = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerIDColumn.ColumnName + " = " + ownerId + " AND "
                                                 + this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName + " = " + isBuyer; ;

                    if (ownerDataView.Count > 0)
                    {
                        if (isBuyer.Equals(0))
                        {
                            this.OwnerGridView.Rows[this.OwnerGridView.CurrentRowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName].Value = 1;
                        }
                        else
                        {
                            this.OwnerGridView.Rows[this.OwnerGridView.CurrentRowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName].Value = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void ComboBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (((ComboBox)sender).SelectedValue == null)
                {
                    if (((ComboBox)sender).Items.Count >= 1)
                    {
                        ((ComboBox)sender).SelectedIndex = 0;
                    }
                    else
                    {
                        ((ComboBox)sender).Text = string.Empty;
                    }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        /// <summary>
        /// Handles the KeyPress event of the EnddateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void EnddateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //if (!this.flagLoadOnProcess)
                //{
                //    this.EditEnabled();
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        #region Event Subscription

        /// <summary>
        /// Called when [D9030_ F9030_ set slice permission].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;TerraScan.Common.SlicePermissionReload&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                    if (this.dsScheduleSaleTrackingDataSet.Tables[0].Rows.Count > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
                    }
                    else
                    {
                        //// Last Slice does not have a record also it will not return invalid slice
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            eventArgs.Data.FlagInvalidSliceKey = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            try
            {

                this.ClearControls();
                this.LoadSaleTrackingRollYear();
                this.LoadScheduleSaleDetails();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.CustomizeScheduleGrid();
                this.CustomizeOwnerGrid();
                this.CalculateGridTotalValues();
                this.DisableGridSorting(ScheduleDataGridView, false);
                this.DisableGridSorting(OwnerGridView, false);
                this.DeedTypeComboBox.Focus();
                this.ValidateTransferButton();
            }
            catch (Exception ex)
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
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
            }
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns></returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;
            if (VerifyGrantee())
            {
                sliceValidationFields.ErrorMessage = "Cannot assign more than one owner as Grantee";
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }
            return sliceValidationFields;
        }

        /// <summary>
        /// Verifies the grantee.
        /// </summary>
        /// <returns></returns>
        private bool VerifyGrantee()
        {
            bool valid = false;

            this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.AcceptChanges();

            int tempcount = 0;

            for (int i = 0; i < dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Rows.Count; i++)
            {
                if (this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Rows[i]["IsBuyer"].Equals(true))
                {
                    tempcount = tempcount + 1;
                    if (tempcount > 1)
                    {
                        valid = true;
                        break;
                    }
                }
            }

            if (valid)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Event Subscription for save confirmed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, EventArgs eventArgs)
        {
            try
            {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission)
            {

                string ownerXML = TerraScanCommon.GetXmlString(this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners);
                this.SaveScheduleSaleRecords();
                this.DeedTypeComboBox.Focus();
                this.DisableGridSorting(ScheduleDataGridView, false);
                this.DisableGridSorting(OwnerGridView, false);
            }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        /// <summary>
        /// OnD9030_F9030_LoadSliceDetails
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="eventArgs">eventArgs</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.keyId = eventArgs.Data.SelectedKeyId;
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                this.LoadComboBox();
                this.LoadScheduleSaleDetails();
                this.CalculateGridTotalValues();
                this.flagLoadOnProcess = false;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.DeedTypeComboBox.Focus();
                this.ValidateTransferButton();
            }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion EventSubscription

        #region Protected Methods

        /// <summary>
        /// SetSystemSnapshot Event
        /// </summary>
        /// <param name="eventArgs">eventArgs</param>
        protected virtual void OnD9030_F9033_SetSystemSnapshotEvent(TerraScan.Infrastructure.Interface.EventArgs<LoadSystemSnapShotDetails> eventArgs)
        {
            if (this.D9030_F9033_SetSystemSnapshotEvent != null)
            {
                this.D9030_F9033_SetSystemSnapshotEvent(this, eventArgs);
            }
        }

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

        #endregion Protected Methods

        #region Binding Controls

        /// <summary>
        /// Bind Schedule Sales details on appropriate controls
        /// </summary>
        private void BindValues()
        {
            this.LoadSaleTrackingDetails();

            if (this.ScheduleDataGridView.Rows.Count == this.ScheduleDataGridView.OriginalRowCount)
            {
                F29555PersonalPropertySaleData.f29555_pcget_SaleSchedulesAndOwnersRow newRow = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.Newf29555_pcget_SaleSchedulesAndOwnersRow();
                newRow["EmptyRecord$"] = "True";
                this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.Rows.Add(newRow);
                this.ScheduleDataGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.DefaultView;
            }
            DataTable buyerDatatable = new DataTable();
            if (buyerDatatable.Columns.Count == 0)
            {
                buyerDatatable.Columns.Add(this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName, typeof(bool));
                buyerDatatable.Columns.Add("Grantee", typeof(string));
            }

            DataRow dr;
            dr = buyerDatatable.NewRow();
            dr[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName] = 1;
            dr["Grantee"] = "Grantee";
            buyerDatatable.Rows.Add(dr);

            dr = buyerDatatable.NewRow();
            dr[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName] = 0;
            dr["Grantee"] = "Grantor";
            buyerDatatable.Rows.Add(dr);
            this.OwnerGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.DefaultView;
            (this.OwnerGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName] as DataGridViewComboBoxColumn).DataSource = buyerDatatable.Copy();
            (this.OwnerGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName] as DataGridViewComboBoxColumn).DisplayMember = buyerDatatable.Columns[1].ToString();
            (this.OwnerGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName] as DataGridViewComboBoxColumn).ValueMember = buyerDatatable.Columns[0].ToString();
            (this.OwnerGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName] as DataGridViewComboBoxColumn).ReadOnly = false;

            // If the last record got edited add new row at the bottom of the grid
            if (this.OwnerGridView.Rows.Count == this.OwnerGridView.OriginalRowCount)
            {
                F29555PersonalPropertySaleData.f29555_pcget_PSaleOwnersRow newRow = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Newf29555_pcget_PSaleOwnersRow();
                newRow["EmptyRecord$"] = "True";
                this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Rows.Add(newRow);
                this.OwnerGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.DefaultView;

            }

            if (this.ScheduleDataGridView.Rows.Count > this.ScheduleDataGridView.NumRowsVisible)
            {
                this.ScheduleGridVScrollBar.Visible = false;
            }
            else
            {
                this.ScheduleGridVScrollBar.Visible = true;
            }

            if (this.OwnerGridView.Rows.Count > this.OwnerGridView.NumRowsVisible)
            {
                this.OwnerGridVScrollBar.Visible = false;
            }
            else
            {
                this.OwnerGridVScrollBar.Visible = true;
            }

            this.TransferOwnershipButton.Enabled = true;


            this.ScheduleRecordCount();
        }

        #endregion Binding Controls

        #region Form Load

        /// <summary>
        /// Form Loading Event
        /// </summary>
        /// <param name="sender">The Object which trigger event</param>
        /// <param name="e">The event args</param>
        private void F29555_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                this.LoadSaleTrackingRollYear();
                this.LoadScheduleSaleDetails();
                this.DisableGridSorting(ScheduleDataGridView, false);
                this.DisableGridSorting(OwnerGridView, false);
                this.flagLoadOnProcess = false;
                this.isNewLoad = true;
               this. ValidateTransferButton();
            }
            catch (SoapException soapEx)
            {
                ExceptionManager.ManageException(soapEx, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion Form Load

        #region Schedule Grid Events
       
        /// <summary>
        /// Handles the DataBindingComplete event of the ScheduleDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void ScheduleDataGridView_DataBindingComplete(object sender, System.Windows.Forms.DataGridViewBindingCompleteEventArgs e)
        {
            try
            {

                if (this.ScheduleDataGridView.Rows.Count > 0)
                {
                    this.ScheduleDataGridView.Rows[0].Selected = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the ScheduleDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void ScheduleDataGridView_CellFormatting(object sender, System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
        {
            try
            {

                Double outValue;
                if (e.ColumnIndex.Equals(this.ScheduleDataGridView.Columns[this.TaxableValue.Name].Index)
                    || e.ColumnIndex.Equals(this.ScheduleDataGridView.Columns[this.AssessedValue.Name].Index))
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }
                    if (this.ScheduleDataGridView.Rows[e.RowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.ScheduleNumberColumn.ColumnName].Value != null
                        && !string.IsNullOrEmpty(this.ScheduleDataGridView.Rows[e.RowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.ScheduleNumberColumn.ColumnName].Value.ToString().Trim()))
                    {

                        if (e.Value != null && !String.IsNullOrEmpty(e.Value.ToString()))
                        {
                            string val = e.Value.ToString();
                            if (Double.TryParse(val, out outValue))
                            {
                                e.Value = outValue.ToString("#,##0");
                                e.FormattingApplied = true;
                            }
                            else
                            {
                                e.Value = "0";
                            }
                        }
                        else
                        {
                            e.Value = "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        /// <summary>
        /// Handles the RowEnter event of the ParcelDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ScheduleDataGridView_RowEnter(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {

                    for (int i = 0; i < this.ScheduleDataGridView.Rows.Count; i++)
                    {

                        TerraScanTextAndImageCell ownerCell = (TerraScanTextAndImageCell)this.ScheduleDataGridView[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.ScheduleNumberColumn.ColumnName, i];
                        ownerCell.ImagexLocation = 260;
                        ownerCell.ImageyLocation = 1;

                        if (e.RowIndex == i)
                        {
                            ownerCell.Image = Properties.Resources.FilePathImage;
                        }
                        else
                        {
                            if (e.RowIndex >= 0)
                            {
                                try
                                {
                                    ownerCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.ScheduleDataGridView[0, e.RowIndex].InheritedStyle.BackColor.R), Convert.ToInt32(this.ScheduleDataGridView[0, e.RowIndex].InheritedStyle.BackColor.G), Convert.ToInt32(this.ScheduleDataGridView[0, e.RowIndex].InheritedStyle.BackColor.B));
                                }
                                catch
                                {
                                }
                            }
                        }

                        this.ScheduleDataGridView.Refresh();
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }



        }

        /// <summary>
        /// Adds the schedule details.
        /// </summary>
        /// <param name="currentRowIndex">Index of the current row.</param>
        private void AddScheduleDetails(int currentRowIndex)
        {
            int checkedIndex = this.ScheduleDataGridView.CurrentCell.RowIndex;
            if (currentRowIndex >= 0)
            {
                ListDataTable.Clear();
                this.ListDataTable = this.form29555Controller.WorkItem.F29555_GetSalesScheduleandOwners(0, ScheduleIDs, 0, TerraScanCommon.UserId).f29555_pcget_SaleSchedulesAndOwners;

                string strTemp = this.ScheduleDataGridView.CurrentCell.FormattedValue.ToString();
                if (!string.IsNullOrEmpty(strTemp))
                {
                    this.ScheduleDataGridView.Rows.RemoveAt(checkedIndex);
                    this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.AcceptChanges();

                }

                if (this.ScheduleDataGridView.OriginalRowCount < this.ScheduleDataGridView.NumRowsVisible)
                {
                    for (int i = this.ScheduleDataGridView.RowCount; i >= this.ScheduleDataGridView.OriginalRowCount + 1; i--)
                    {
                        this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.Rows.RemoveAt(i - 1);
                    }
                }
                if (ListDataTable.Rows.Count > 0)
                {
                    int maxrowcount = this.ScheduleDataGridView.OriginalRowCount;
                    this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.Merge(ListDataTable);

                    int newRowCount = this.ScheduleDataGridView.OriginalRowCount;
                    DataView scheduleView = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.DefaultView;
                    scheduleView.RowFilter = "EmptyRecord$='True'";
                    if (scheduleView.Count > 0)
                    {
                        for (int i = scheduleView.Count - 1; i >= 0; i--)
                        {
                            scheduleView.Delete(i);
                        }
                    }
                    scheduleView.RowFilter = string.Empty;
                    this.ScheduleDataGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.DefaultView;
                    if (this.ScheduleDataGridView.Rows.Count == this.ScheduleDataGridView.OriginalRowCount)
                    {
                        F29555PersonalPropertySaleData.f29555_pcget_SaleSchedulesAndOwnersRow newRow = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.Newf29555_pcget_SaleSchedulesAndOwnersRow();
                        newRow["EmptyRecord$"] = "True";
                        this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.Rows.Add(newRow);
                        this.ScheduleDataGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.DefaultView;
                        this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.AcceptChanges();
                    }
                    this.ScheduleDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    this.EditRecord();
                    this.ScheduleDataGridView.Focus();



                }
                else
                {

                    this.ScheduleDataGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.DefaultView;

                }


            }
            else
            {
                this.ScheduleDataGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.DefaultView;

            }

        }

        /// <summary>
        /// Disable Grid Sorting
        /// </summary>
        /// <param name="dataGridViewSorting">dataGridViewSorting</param>
        /// <param name="disableSort">disableSort</param>
        private void DisableGridSorting(TerraScan.UI.Controls.TerraScanDataGridView dataGridViewSorting, bool disableSort)
        {
            if (disableSort)
            {
                dataGridViewSorting.AllowSorting = false;
            }
            else
            {
                dataGridViewSorting.AllowSorting = true;
            }
        }
        /// <summary>
        /// Handles the CellMouseClick event of the ScheduleDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void ScheduleDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e != null)
                {
                    if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.ColumnIndex.Equals(1))
                    {
                        int previousRow = 0;
                        if (e.RowIndex != 0)
                        {
                            previousRow = e.RowIndex - 1;
                        }

                        bool validRow = true;
                        if (e.RowIndex > 0)
                        {
                            if (string.IsNullOrEmpty(this.ScheduleDataGridView.Rows[previousRow].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.ScheduleNumberColumn.ColumnName].Value.ToString()))
                            {
                                validRow = false;
                            }
                        }
                        if (this.ScheduleDataGridView.Rows[e.RowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.ScheduleIDColumn.ColumnName].Value != null
                                && this.ScheduleDataGridView.Rows[e.RowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.ScheduleIDColumn.ColumnName].Value.ToString() == this.scheduleId.ToString())
                        {
                            validRow = false;
                        }

                        if (this.ScheduleDataGridView.Columns[e.ColumnIndex].Name.Equals(this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.ScheduleNumberColumn.ColumnName) && (e.RowIndex >= 0) && validRow)
                        {
                            int isBaseSchedule = 0;
                            if (this.ScheduleDataGridView.Rows[e.RowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.IsBaseScheduleColumn.ColumnName].Value != null
                             && !string.IsNullOrEmpty(this.ScheduleDataGridView.Rows[e.RowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.IsBaseScheduleColumn.ColumnName].Value.ToString().Trim()))
                            {
                                int.TryParse(this.ScheduleDataGridView.Rows[e.RowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.IsBaseScheduleColumn.ColumnName].Value.ToString(), out isBaseSchedule);
                            }
                            if (isBaseSchedule.Equals(0))
                            {
                                if ((e.X >= 260) && (e.X <= 290) && (e.Y >= 1) && (e.Y <= (22 - 1)))
                                {
                                    Form subfundForm = new Form();
                                    object[] optionalParameter = new object[] { this.rollYear, this.masterFormNo.ToString() };
                                    subfundForm = this.form29555Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1404, optionalParameter, this.form29555Controller.WorkItem);
                                    if (subfundForm != null)
                                    {
                                        if (subfundForm.ShowDialog() == DialogResult.OK)
                                        {
                                            this.ScheduleIDs = TerraScanCommon.GetValue(subfundForm, "CommandResult");
                                            DataSet currentScheduleDataTable = new DataSet();
                                            currentScheduleDataTable.ReadXml(TerraScan.Utilities.SharedFunctions.XmlParser(this.ScheduleIDs));

                                            if (currentScheduleDataTable.Tables[0].Rows.Count > 0)
                                            {

                                                DataRow[] listscheduleDataRow = currentScheduleDataTable.Tables[0].Select();

                                                foreach (DataRow schedule in listscheduleDataRow)
                                                {
                                                    if (!string.IsNullOrEmpty(schedule.ItemArray[0].ToString()))
                                                    {
                                                        DataRow[] scheduleId = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.Select("ScheduleID=" + schedule.ItemArray[0].ToString());
                                                        isNewSchedule = true;
                                                        if (scheduleId.Length > 0)
                                                        {
                                                            if (MessageBox.Show(SharedFunctions.GetResourceString("ExistSched"), "Terrascan T2 - Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                                            {
                                                                return;
                                                            }
                                                        }
                                                    }
                                                }
                                                this.AddScheduleDetails(this.ScheduleDataGridView.CurrentCell.RowIndex);
                                                CalculateGridTotalValues();
                                                this.ScheduleRecordCount();
                                                this.ScheduleGridBind();
                                                if (this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.DefaultView == null || currentScheduleDataTable.Tables[0].Rows.Count <= 0)
                                                {
                                                    this.EditRecord();

                                                    F29555PersonalPropertySaleData.f29555_pcget_SaleSchedulesAndOwnersRow scheduleRow = (F29555PersonalPropertySaleData.f29555_pcget_SaleSchedulesAndOwnersRow)dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.Rows[0];


                                                    if (!scheduleRow.IsIsBaseScheduleNull())
                                                    {
                                                        this.ScheduleDataGridView.Rows[e.RowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.IsBaseScheduleColumn.ColumnName].Value = scheduleRow.IsBaseSchedule;
                                                    }
                                                    else
                                                    {
                                                        this.ScheduleDataGridView.Rows[e.RowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.IsBaseScheduleColumn.ColumnName].Value = 0;
                                                    }


                                                    // If the last record got edited add new row at the bottom of the grid
                                                    if (this.ScheduleDataGridView.Rows.Count.Equals(e.RowIndex + 1))
                                                    {
                                                        this.ScheduleDataGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.DefaultView;
                                                        F29555PersonalPropertySaleData.f29555_pcget_SaleSchedulesAndOwnersRow newRow = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.Newf29555_pcget_SaleSchedulesAndOwnersRow();
                                                        newRow["EmptyRecord$"] = "True";
                                                        this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.Rows.Add(newRow);
                                                        this.ScheduleDataGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.DefaultView;
                                                    }

                                                    this.CalculateGridTotalValues();
                                                    this.ScheduleRecordCount();
                                                    this.OwnerGridBind();
                                                    this.AddOwnerDefaultRows();
                                                }
                                                else
                                                {
                                                    this.OwnerGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.DefaultView;

                                                }
                                            }
                                            else
                                            {
                                                isNewSchedule = false;
                                                this.AddScheduleDetails(this.ScheduleDataGridView.CurrentCell.RowIndex);
                                                CalculateGridTotalValues();
                                            }

                                            this.OwnerGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.DefaultView;

                                            if (this.ScheduleDataGridView.Rows.Count > this.ScheduleDataGridView.NumRowsVisible)
                                            {
                                                this.ScheduleGridVScrollBar.Visible = false;
                                            }
                                            else
                                            {
                                                this.ScheduleGridVScrollBar.Visible = true;
                                            }

                                            if (this.OwnerGridView.Rows.Count > this.OwnerGridView.NumRowsVisible)
                                            {
                                                this.OwnerGridVScrollBar.Visible = false;
                                            }
                                            else
                                            {
                                                this.OwnerGridVScrollBar.Visible = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    this.ScheduleDataGridView.Rows[0].Cells["ScheduleNumber"].Selected = true;
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }

        }



        /// <summary>
        /// Handles the KeyDown event of the ParcelDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ScheduleDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.ScheduleDataGridView.CurrentRowIndex >= 0 && e.KeyCode.Equals(Keys.Delete))
                {
                    if (this.ScheduleDataGridView.Rows[this.ScheduleDataGridView.CurrentRowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.ScheduleIDColumn.ColumnName].Value != null
                                && this.ScheduleDataGridView.Rows[this.ScheduleDataGridView.CurrentRowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.ScheduleIDColumn.ColumnName].Value.ToString() != this.scheduleId.ToString())
                    {

                        this.DeleteSchedule(this.ScheduleDataGridView.CurrentRowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        #endregion Schedule Grid Events

        #region Owner grid Events

        /// <summary>
        /// Handles the CellMouseClick event of the OwnerGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void OwnerGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != -1 && e.RowIndex >= 0)
                {
                    this.isNewLoad = false;


                    bool validRow = true;
                    if (e.RowIndex > 0)
                    {
                        if (this.OwnerGridView.Rows[e.RowIndex - 1].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerNameColumn.ColumnName].Value == null
                            || string.IsNullOrEmpty(this.OwnerGridView.Rows[e.RowIndex - 1].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerNameColumn.ColumnName].Value.ToString()))
                        {
                            validRow = false;
                        }
                    }
                    if (this.OwnerGridView.Columns[e.ColumnIndex].Name.Equals(this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerNameColumn.ColumnName) && (e.RowIndex >= 0) && validRow)
                    {
                        if ((e.X >= 150) && (e.X <= 172) && (e.Y >= 1) && (e.Y <= (22 - 1)))
                        {
                            Form parcelF9101 = new Form();
                            parcelF9101 = this.form29555Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9101, null, this.form29555Controller.WorkItem);

                            if (parcelF9101 != null)
                            {
                                if (parcelF9101.ShowDialog() == DialogResult.Yes)
                                {
                                    int ownerId;
                                    int.TryParse(TerraScanCommon.GetValue(parcelF9101, "MasterNameOwnerId"), out ownerId);
                                    if (ownerId > 0)
                                    {
                                        F29555PersonalPropertySaleData.f29555_pcget_PSaleOwnersDataTable ownerTable = new F29555PersonalPropertySaleData.f29555_pcget_PSaleOwnersDataTable();
                                        DataTable dtTemp = new DataTable();
                                        dtTemp = dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Copy();
                                        ownerTable = this.form29555Controller.WorkItem.F29555_GetPersonalSalesOwner(null, ownerId, null, TerraScanCommon.UserId, string.Empty).f29555_pcget_PSaleOwners;
                                        if (ownerTable.Rows.Count > 0)
                                        {
                                            F29555PersonalPropertySaleData.f29555_pcget_PSaleOwnersRow newRow = (F29555PersonalPropertySaleData.f29555_pcget_PSaleOwnersRow)ownerTable.Rows[0];
                                            bool isBuyer = false;
                                            if (!newRow.IsIsBuyerNull() && !string.IsNullOrEmpty(newRow.IsBuyer.ToString()))
                                            {
                                                isBuyer = newRow.IsBuyer;
                                            }

                                            DataView ownerDataView = null;
                                            if (ownerTable.Rows.Count > 0)
                                            {
                                                DataView ownersView = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.DefaultView;
                                                ownersView.RowFilter = "EmptyRecord$='True'";

                                                if (ownersView.Count > 0)
                                                {
                                                    for (int i = ownersView.Count - 1; i >= 0; i--)
                                                    {
                                                        ownersView.Delete(i);
                                                    }
                                                }
                                                ownersView.RowFilter = string.Empty;
                                            }
                                            if (this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Rows.Count > 0)
                                            {
                                                ownerDataView = new DataView(this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners);
                                            }

                                            ownerDataView.RowFilter = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerIDColumn.ColumnName + " = " + ownerId + " AND "
                                                                         + this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName + " = " + isBuyer; ;

                                            if (ownerDataView.Count <= 0)
                                            {
                                                this.EditRecord();
                                                this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Merge((ownerTable));
                                                this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.AcceptChanges();
                                                this.OwnerGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.DefaultView;
                                                if (this.OwnerGridView.Rows.Count.Equals(e.RowIndex + 1))
                                                {
                                                    F29555PersonalPropertySaleData.f29555_pcget_PSaleOwnersRow ownerRow = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Newf29555_pcget_PSaleOwnersRow();
                                                    ownerRow["EmptyRecord$"] = "True";
                                                    this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Rows.Add(ownerRow);
                                                    this.OwnerGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.DefaultView;
                                                }

                                                if (this.OwnerGridView.Rows.Count > this.OwnerGridView.NumRowsVisible)
                                                {
                                                    this.OwnerGridVScrollBar.Visible = false;
                                                }
                                                else
                                                {
                                                    this.OwnerGridVScrollBar.Visible = false;
                                                }
                                            }
                                            else
                                            {
                                                DataView dvTemp = dtTemp.DefaultView;
                                                this.OwnerGridView.DataSource = dvTemp;
                                                if (MessageBox.Show(SharedFunctions.GetResourceString("OwnerExixts"), "Terrascan T2 - Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                                {
                                                    return;
                                                }
                                            }

                                        }

                                    }

                                    if (this.OwnerGridView.OriginalRowCount >= this.OwnerGridView.NumRowsVisible)
                                    {
                                        this.OwnerGridVScrollBar.Visible = false;
                                    }
                                }
                            }
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
        /// Handles the RowEnter event of the OwnerGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>

        /// <summary>
        /// Handles the CellFormatting event of the OwnerGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void OwnerGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
            if (this.OwnerGridView.Columns[e.ColumnIndex].Name.Equals(this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName))
            {
                if (this.OwnerGridView.Rows[e.RowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerNameColumn.ColumnName].Value == null
                    || string.IsNullOrEmpty(this.OwnerGridView.Rows[e.RowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerNameColumn.ColumnName].Value.ToString().Trim()))
                {
                    this.OwnerGridView.Rows[e.RowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName].ReadOnly = true;
                }
                else
                {
                    this.OwnerGridView.Rows[e.RowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName].ReadOnly = false;
                }
            }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }

        }
        /// <summary>
        /// Handles the DataBindingComplete event of the OwnerGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void OwnerGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.OwnerGridView.Rows.Count > 0)
                {
                    this.OwnerGridView.Rows[0].Selected = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the OwnerGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void OwnerGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    for (int i = 0; i < this.OwnerGridView.Rows.Count; i++)
                    {
                        TerraScanTextAndImageCell ownerCell = (TerraScanTextAndImageCell)this.OwnerGridView[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerNameColumn.ColumnName, i];
                        ownerCell.ImagexLocation = 150;
                        ownerCell.ImageyLocation = 1;

                        if (e.RowIndex == i)
                        {
                            ownerCell.Image = Properties.Resources.Owner;
                        }
                        else
                        {
                            if (e.RowIndex >= 0)
                            {
                                try
                                {
                                    ownerCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.OwnerGridView[0, e.RowIndex].InheritedStyle.BackColor.R), Convert.ToInt32(this.OwnerGridView[0, e.RowIndex].InheritedStyle.BackColor.G), Convert.ToInt32(this.OwnerGridView[0, e.RowIndex].InheritedStyle.BackColor.B));
                                }
                                catch
                                {
                                }
                            }
                        }

                        this.OwnerGridView.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        /// <summary>
        /// Handles the KeyDown event of the OwnerGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void OwnerGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Delete) && this.OwnerGridView.CurrentRowIndex >= 0)
                {
                    this.DeleteOwner(this.OwnerGridView.CurrentRowIndex);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Button Events

        /// <summary>
        /// Transfer Ownership
        /// </summary>
        /// <param name="sender">The Object which trigger event</param>
        /// <param name="e">The event args</param>
        private void TransferOwnershipButton_Click(object sender, EventArgs e)
        {
            try
            {
                string returnedMessage = this.form29555Controller.WorkItem.F29555_TransferOwnership(this.keyId, TerraScanCommon.UserId);

                if (!string.IsNullOrEmpty(returnedMessage.Trim()))
                {
                    if (MessageBox.Show(returnedMessage.Trim(), "TerraScan T2 – Transfer Ownership", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {

                        if (!string.IsNullOrEmpty(returnedMessage) && (returnedMessage.Equals("There are more than one buyer associated with this Sale, so the Ownership cannot be transfered.  Please associate only one buyer with this Sale and then run the Transfer Ownership process again.")) || returnedMessage.Equals("There are no buyers associated with this Sale, so there is no Ownership to transfer.  Please associate buyers with this Sale and then run the Transfer Ownership process again."))
                        {
                            this.TransferOwnershipButton.Enabled = true;
                        }
                        else
                        {
                            this.TransferOwnershipButton.Enabled = false;
                        }
                        return;
                    }

                }


            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        #region Picture box


        /// <summary>
        /// Handles the Click event of the PersonalPropertySalePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PersonalPropertySalePictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D24555.F29555"));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the SaleParcelPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PersonalPropertySalePictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.PersonalSaleToolTip.SetToolTip(this.PersonalPropertySalePictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Private methods


        #region Binding Controls
        /// <summary>
        /// Loads the schedule sale details.
        /// </summary>
        private void LoadScheduleSaleDetails()
        {

            // Get value to bind Gridview and other controls
            this.F29555LoadFormDetails();

            // Populate all combobox
            this.LoadComboBox();

            // Customize Grid columns
            this.CustomizeScheduleGrid();
            this.CustomizeOwnerGrid();

            // Bind values in appropriate controls
            this.BindValues();

            // Calculate sum of schedule grid values
            this.CalculateGridTotalValues();

        }

        /// <summary>
        /// F29555 the load form details.
        /// </summary>
        private void F29555LoadFormDetails()
        {
            this.dsScheduleSaleTrackingDataSet.f29555_pcget_ScheduleSaleTracking.Clear();
            this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.Clear();
            this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Clear();

            this.dsScheduleSaleTrackingDataSet.Merge(this.form29555Controller.WorkItem.F29555_ScheduleSaleTracking(this.keyId, TerraScanCommon.UserId));

        }

        /// <summary>
        /// Loads the sale tracking roll year.
        /// </summary>
        private void LoadSaleTrackingRollYear()
        {
            this.ScheduleRollYearTable = this.form29555Controller.WorkItem.F1404_GetScheduleTrackingRollYear(this.keyId).ScheduleRollYearDataTable;
            if (this.ScheduleRollYearTable.Rows.Count > 0)
            {
                this.rollYear = Int32.Parse(this.ScheduleRollYearTable.Rows[0][0].ToString());
            }
        }

        #endregion

        #region Customize grids
        /// <summary>
        /// Customize Schedule Grid
        /// </summary>
        private void CustomizeScheduleGrid()
        {
            try
            {
                this.ScheduleDataGridView.AutoGenerateColumns = false;

                this.ScheduleDataGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.IsBaseScheduleColumn.ColumnName].DataPropertyName = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.IsBaseScheduleColumn.ColumnName;
                this.ScheduleDataGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.ScheduleNumberColumn.ColumnName].DataPropertyName = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.ScheduleNumberColumn.ColumnName;
                this.ScheduleDataGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.AssessedValueColumn.ColumnName].DataPropertyName = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.AssessedValueColumn.ColumnName;
                this.ScheduleDataGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.TaxableValueColumn.ColumnName].DataPropertyName = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.TaxableValueColumn.ColumnName;
                this.ScheduleDataGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.ScheduleIDColumn.ColumnName].DataPropertyName = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.ScheduleIDColumn.ColumnName;

                this.ScheduleDataGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.IsBaseScheduleColumn.ColumnName].Visible = false;
                this.ScheduleDataGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.ScheduleIDColumn.ColumnName].Visible = false;

                this.ScheduleDataGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.IsBaseScheduleColumn.ColumnName].DisplayIndex = 0;
                this.ScheduleDataGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.ScheduleNumberColumn.ColumnName].DisplayIndex = 1;
                this.ScheduleDataGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.AssessedValueColumn.ColumnName].DisplayIndex = 2;
                this.ScheduleDataGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.TaxableValueColumn.ColumnName].DisplayIndex = 3;
                this.ScheduleDataGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.ScheduleIDColumn.ColumnName].DisplayIndex = 4;

                this.ScheduleDataGridView.PrimaryKeyColumnName = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.ScheduleIDColumn.ToString();
                // this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.AcceptChanges();
                this.ScheduleDataGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.DefaultView;


            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Customize Owner Grid
        /// </summary>
        private void CustomizeOwnerGrid()
        {
            try
            {
                this.OwnerGridView.AutoGenerateColumns = false;
                this.OwnerGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerNameColumn.ColumnName].DataPropertyName = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerNameColumn.ColumnName;
                this.OwnerGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName].DataPropertyName = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName;
                this.OwnerGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerAddressColumn.ColumnName].DataPropertyName = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerAddressColumn.ColumnName;
                this.OwnerGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerLocationColumn.ColumnName].DataPropertyName = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerLocationColumn.ColumnName;
                this.OwnerGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerIDColumn.ColumnName].DataPropertyName = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerIDColumn.ColumnName;

                this.OwnerGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerNameColumn.ColumnName].DisplayIndex = 0;
                this.OwnerGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName].DisplayIndex = 1;
                this.OwnerGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerAddressColumn.ColumnName].DisplayIndex = 2;
                this.OwnerGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerLocationColumn.ColumnName].DisplayIndex = 3;
                this.OwnerGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerIDColumn.ColumnName].DisplayIndex = 4;

                this.OwnerGridView.PrimaryKeyColumnName = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerIDColumn.ToString();
                //this.OwnerGridView.Height = 134;

                // this.OwnerGridVScrollBar.Height = 128;
                //this.OwnerGridView.BackgroundColor = System.Drawing.Color.FromArgb(64, 64, 64);

                DataTable buyerDatatable = new DataTable();
                if (buyerDatatable.Columns.Count == 0)
                {
                    buyerDatatable.Columns.Add(this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName, typeof(bool));
                    buyerDatatable.Columns.Add("Grantee", typeof(string));
                }

                DataRow dr;
                dr = buyerDatatable.NewRow();
                dr[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName] = 1;
                dr["Grantee"] = "Grantee";
                buyerDatatable.Rows.Add(dr);

                dr = buyerDatatable.NewRow();
                dr[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName] = 0;
                dr["Grantee"] = "Grantor";
                buyerDatatable.Rows.Add(dr);

                (this.OwnerGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName] as DataGridViewComboBoxColumn).DataSource = buyerDatatable.Copy();
                (this.OwnerGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName] as DataGridViewComboBoxColumn).DisplayMember = buyerDatatable.Columns[1].ToString();
                (this.OwnerGridView.Columns[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName] as DataGridViewComboBoxColumn).ValueMember = buyerDatatable.Columns[0].ToString();
                //(this.OwnerGridView.Columns[this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName] as DataGridViewComboBoxColumn).ReadOnly = false;

            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region Grid Total Values

        /// <summary>
        /// Calculates the grid total values.
        /// </summary>
        private void CalculateGridTotalValues()
        {
            try
            {
                double assessedSumValue = 0;
                double taxSumValue = 0;

                this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.AcceptChanges();
                for (int i = 0; i < this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.Rows[i][this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.AssessedValueColumn.ColumnName].ToString()))
                    {
                        double AssessedValue = 0;
                        double.TryParse(this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.Rows[i][this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.AssessedValueColumn.ColumnName].ToString(), out AssessedValue);
                        assessedSumValue = assessedSumValue + AssessedValue; //Double.Parse(this.parcelSaleData.ParcelDetails.Rows[i][this.parcelSaleData.ParcelDetails.AcresColumn.ColumnName].ToString());
                    }

                    if (!string.IsNullOrEmpty(this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.Rows[i][this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.TaxableValueColumn.ColumnName].ToString()))
                    {
                        taxSumValue = taxSumValue + Double.Parse(this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.Rows[i][this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.TaxableValueColumn.ColumnName].ToString());
                    }

                }
                //Need to modify to apply format to label
                this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.AcceptChanges();
                this.AssesedSumLabel.Text = assessedSumValue.ToString("#,##0");
                this.TaxableSumLabel.Text = taxSumValue.ToString("#,##0");

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Schedules the record count.
        /// </summary>
        private void ScheduleRecordCount()
        {
            if (this.ScheduleDataGridView.OriginalRowCount > 0)
            {
                if (this.ScheduleDataGridView.OriginalRowCount >= 5)
                {
                    this.ScheduleHeaderLabel.Text = "Included Schedules (" + (this.ScheduleDataGridView.OriginalRowCount) + ")";
                }
                else
                {
                    this.ScheduleHeaderLabel.Text = "Included Schedules (" + (this.ScheduleDataGridView.OriginalRowCount) + ")";
                }
            }
            else
            {
                this.ScheduleHeaderLabel.Text = "Included Schedules";
            }
        }

        #endregion Grid Total Values

        /// <summary>
        /// Adds the owner default rows.
        /// </summary>
        private void AddOwnerDefaultRows()
        {
            if (dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Rows.Count > 0)
            {
                for (int i = 0; i < dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Rows.Count; i++)
                {
                    F29555PersonalPropertySaleData.f29555_pcget_PSaleOwnersRow newRow = (F29555PersonalPropertySaleData.f29555_pcget_PSaleOwnersRow)dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Copy().Rows[i];
                    int ownerId = 0;
                    bool isBuyer = false;
                    if (!newRow.IsIsBuyerNull() && !string.IsNullOrEmpty(newRow.IsBuyer.ToString()))
                    {
                        isBuyer = newRow.IsBuyer;
                    }

                    if (!newRow.IsOwnerIDNull())
                    {
                        ownerId = newRow.OwnerID;
                    }

                    DataView ownerDataView = null;
                    if (this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Rows.Count > 0)
                    {
                        ownerDataView = new DataView(this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners);
                    }

                    ownerDataView.RowFilter = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerIDColumn.ColumnName + " = " + ownerId + " AND "
                                              + this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName + " = " + isBuyer;

                    if (ownerDataView.Count <= 0)
                    {
                        try
                        {
                            newOwnersTable.ImportRow(newRow);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }

                if (newOwnersTable.Rows.Count > 0)
                {
                    DataView ownersView = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.DefaultView;
                    ownersView.RowFilter = "EmptyRecord$='True'";

                    if (ownersView.Count > 0)
                    {
                        for (int i = ownersView.Count - 1; i >= 0; i--)
                        {
                            ownersView.Delete(i);
                        }
                    }
                    ownersView.RowFilter = string.Empty;
                    this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Merge((newOwnersTable));
                }
            }

            this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Merge(dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners);
            this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.DefaultView.RowFilter = string.Empty;
            this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.AcceptChanges();
            this.OwnerGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.DefaultView;

            if (this.OwnerGridView.Rows.Count == this.OwnerGridView.OriginalRowCount)
            {
                F29555PersonalPropertySaleData.f29555_pcget_PSaleOwnersRow ownerRow = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Newf29555_pcget_PSaleOwnersRow();
                ownerRow["EmptyRecord$"] = "True";
                this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Rows.Add(ownerRow);
                this.OwnerGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.DefaultView;
            }
        }
        #region Delete
        /// <summary>
        /// Deletes the schedule details.
        /// </summary>
        private void DeleteSchedule(int rowIndex)
        {
            if (this.ScheduleDataGridView.CurrentCell != null && this.slicePermissionField.deletePermission
                && this.ScheduleDataGridView.OriginalRowCount > 0 && this.ScheduleDataGridView.Rows[rowIndex].Selected)
            {
                if (this.ScheduleDataGridView.Rows[rowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.IsBaseScheduleColumn.ColumnName].Value != null
                    && !string.IsNullOrEmpty(this.ScheduleDataGridView.Rows[rowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.IsBaseScheduleColumn.ColumnName].Value.ToString().Trim()))
                {
                    int isBaseParcel = 0;
                    int.TryParse(this.ScheduleDataGridView.Rows[rowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.IsBaseScheduleColumn.ColumnName].Value.ToString(), out isBaseParcel);
                    if (isBaseParcel.Equals(0))
                    {
                        //this.GetSelectedParcels();
                        this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.Rows.RemoveAt(rowIndex);
                        this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.AcceptChanges();
                        this.EditRecord();
                        this.ScheduleDataGridView.Focus();
                        if (this.ScheduleDataGridView.Rows.Count > 0)
                        {
                            if (rowIndex > 0)
                            {
                                TerraScanCommon.SetDataGridViewPosition(this.ScheduleDataGridView, rowIndex - 1);
                                this.ScheduleDataGridView.Rows[rowIndex - 1].Selected = true;
                            }
                            else
                            {
                                TerraScanCommon.SetDataGridViewPosition(this.ScheduleDataGridView, 0);
                                this.ScheduleDataGridView.Rows[0].Selected = true;
                            }
                        }

                        // If the last record got edited add new row at the bottom of the grid
                        if (this.ScheduleDataGridView.Rows.Count < this.ScheduleDataGridView.NumRowsVisible)
                        {
                            if (this.ScheduleDataGridView.OriginalRowCount == 4)
                            {
                                this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.Rows.RemoveAt(this.ScheduleDataGridView.OriginalRowCount - 1);
                            }
                            
                            this.ScheduleDataGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.DefaultView;
                        }

                        if (this.ScheduleDataGridView.Rows.Count > this.ScheduleDataGridView.NumRowsVisible)
                        {
                            this.ScheduleGridVScrollBar.Visible = false;
                        }
                        else
                        {
                            this.ScheduleGridVScrollBar.Visible = true;
                        }


                        this.CalculateGridTotalValues();
                        this.ScheduleRecordCount();
                    }
                }
            }
        }

        /// <summary>
        /// Delete owner details
        /// </summary>
        private void DeleteOwner(int rowIndex)
        {
            if (this.OwnerGridView.CurrentCell != null && this.slicePermissionField.deletePermission
                && this.OwnerGridView.OriginalRowCount > 0)
            {
                if (this.OwnerGridView.Rows[rowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerNameColumn.ColumnName].Value != null
                   && !string.IsNullOrEmpty(this.OwnerGridView.Rows[rowIndex].Cells[this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerNameColumn.ColumnName].Value.ToString().Trim()))
                {
                    this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Rows.RemoveAt(rowIndex);
                    this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.AcceptChanges();
                    this.EditRecord();
                    this.OwnerGridView.Focus();
                    if (this.OwnerGridView.Rows.Count > 0)
                    {
                        if (rowIndex > 0)
                        {
                            TerraScanCommon.SetDataGridViewPosition(this.OwnerGridView, rowIndex - 1);
                            this.OwnerGridView.Rows[rowIndex - 1].Selected = true;
                        }
                        else
                        {
                            TerraScanCommon.SetDataGridViewPosition(this.OwnerGridView, 0);
                            this.OwnerGridView.Rows[0].Selected = true;
                        }
                    }


                    // If the last record got edited add new row at the bottom of the grid
                    if (this.OwnerGridView.Rows.Count < this.OwnerGridView.NumRowsVisible)
                    {
                        F29555PersonalPropertySaleData.f29555_pcget_PSaleOwnersRow newRow = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Newf29555_pcget_PSaleOwnersRow();
                        newRow["EmptyRecord$"] = "True";
                        this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Rows.Add(newRow);
                        this.OwnerGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.DefaultView;
                    }

                    if (this.OwnerGridView.Rows.Count > this.OwnerGridView.NumRowsVisible)
                    {
                        this.OwnerGridVScrollBar.Visible = false;
                    }
                    else
                    {
                        this.OwnerGridVScrollBar.Visible = true;
                    }
                }
            }
        }

        #endregion Delete

        #region Save

        /// <summary>
        /// Saves the schedule sale records.
        /// </summary>
        private void SaveScheduleSaleRecords()
        {
            F29555PersonalPropertySaleData.f29555_pcget_ScheduleSaleTrackingDataTable ScheduleTrackingTable = new F29555PersonalPropertySaleData.f29555_pcget_ScheduleSaleTrackingDataTable();
            F29555PersonalPropertySaleData.f29555_pcget_ScheduleSaleTrackingRow ScheduleSaleTrackingRow = ScheduleTrackingTable.Newf29555_pcget_ScheduleSaleTrackingRow();

            if (this.DeedTypeComboBox.SelectedIndex > 0)
            {
                ScheduleSaleTrackingRow.DeedType = this.DeedTypeComboBox.Text;
            }
            else
            {
                ScheduleSaleTrackingRow.DeedType = string.Empty;
            }
            if (!string.IsNullOrEmpty(this.SaleDateTextBox.Text.Trim()))
            {

                // ScheduleSaleTrackingRow.SaleDate = tempDate.Date;
                ScheduleSaleTrackingRow.SaleDate = this.SaleDateTextBox.Text;
            }
            else
            {
                //ScheduleSaleTrackingRow.SaleDate = null;
            }
            ScheduleTrackingTable.Rows.Add(ScheduleSaleTrackingRow);
            ScheduleTrackingTable.AcceptChanges();
            string scheduleTrackingXML = TerraScanCommon.GetXmlString(ScheduleTrackingTable);


            this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.AcceptChanges();
            string scheduleXML = TerraScanCommon.GetXmlString(this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners);


            this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.AcceptChanges();
            string ownerXML = TerraScanCommon.GetXmlString(this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners);

            int returnValue = this.form29555Controller.WorkItem.F29555_SaveScheduleSaleTracking(this.keyId, scheduleTrackingXML, scheduleXML, ownerXML, TerraScanCommon.UserId);
        }

        #endregion Save

        #region Clear Controls
        /// <summary>
        /// Clears the controls.
        /// </summary>
        private void ClearControls()
        {
            this.DeedTypeComboBox.SelectedIndex = 0;
            this.SaleDateTextBox.Text = string.Empty;
            this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Clear();
            this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.Clear();
            this.ScheduleDataGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.DefaultView;
            this.OwnerGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.DefaultView;
        }

        #endregion

        #region Edit
        /// <summary>
        /// Set Edit mode
        /// </summary>
        private void EditRecord()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.DisableGridSorting(this.ScheduleDataGridView, true);
                this.DisableGridSorting(this.OwnerGridView, true);
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                this.TransferOwnershipButton.Enabled = false;
            }
        }

        #endregion

        #region ComboBox Load
        /// <summary>
        /// Loads the combo box.
        /// </summary>
        private void LoadComboBox()
        {
            this.dtComboBox = this.form29555Controller.WorkItem.F29555_DeedtypeComboBox().f29555_pclst_DeedTypes;
            this.DeedTypeComboBox.DataSource = this.dtComboBox.DefaultView;
            this.DeedTypeComboBox.DisplayMember = this.dtComboBox.DeedTypeColumn.ColumnName;
            this.DeedTypeComboBox.ValueMember = this.dtComboBox.DeedTypeIDColumn.ColumnName;
        }
        #endregion

        /// <summary>
        /// Loads the sale tracking details.
        /// </summary>
        private void LoadSaleTrackingDetails()
        {
            if (this.dsScheduleSaleTrackingDataSet.f29555_pcget_ScheduleSaleTracking.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.dsScheduleSaleTrackingDataSet.f29555_pcget_ScheduleSaleTracking.Rows[0]["DeedType"].ToString()))
                {
                    this.DeedTypeComboBox.Text = this.dsScheduleSaleTrackingDataSet.f29555_pcget_ScheduleSaleTracking.Rows[0]["DeedType"].ToString();
                }
                else
                {
                    this.DeedTypeComboBox.SelectedIndex = 0;
                }

                this.SaleDateTextBox.Text = this.dsScheduleSaleTrackingDataSet.f29555_pcget_ScheduleSaleTracking.Rows[0]["SaleDate"].ToString();


                if (this.dsScheduleSaleTrackingDataSet.f29555_pcget_ScheduleSaleTracking.Rows[0]["IsOwnerPushed"].ToString().ToLower().Equals("true"))
                {
                    this.TransferOwnershipButton.Enabled = false;
                }
                else
                {
                    this.TransferOwnershipButton.Enabled = true;
                }
            }
            this.scheduleId = Convert.ToInt32(this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.Rows[0]["ScheduleID"]);
            this.ScheduleDataGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_SaleSchedulesAndOwners.DefaultView;
        }
        /// <summary>
        /// Validates the transfer button.
        /// </summary>
        private void ValidateTransferButton()
        {
            if (this.dsScheduleSaleTrackingDataSet.f29555_pcget_ScheduleSaleTracking.Rows.Count > 0)
            {
                if (this.dsScheduleSaleTrackingDataSet.f29555_pcget_ScheduleSaleTracking.Rows[0]["IsOwnerPushed"].ToString().ToLower().Equals("true"))
                {
                    this.TransferOwnershipButton.Enabled = false;
                }
                else
                {
                    this.TransferOwnershipButton.Enabled = true;
                }
            }
        }
        /// <summary>
        /// Owners the grid bind.
        /// </summary>
        private void OwnerGridBind()
        {
            if (dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Rows.Count > 0)
            {
                for (int i = 0; i < dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Rows.Count; i++)
                {
                    F29555PersonalPropertySaleData.f29555_pcget_PSaleOwnersRow newRow = (F29555PersonalPropertySaleData.f29555_pcget_PSaleOwnersRow)dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Copy().Rows[i];
                    int ownerId = 0;
                    bool isBuyer = false;
                    if (!newRow.IsIsBuyerNull() && !string.IsNullOrEmpty(newRow.IsBuyer.ToString()))
                    {
                        isBuyer = newRow.IsBuyer;
                    }

                    if (!newRow.IsOwnerIDNull())
                    {
                        ownerId = newRow.OwnerID;
                    }

                    DataView ownerDataView = null;
                    if (this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Rows.Count > 0)
                    {
                        ownerDataView = new DataView(this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners);
                    }

                    ownerDataView.RowFilter = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.OwnerIDColumn.ColumnName + " = " + ownerId + " AND "
                                              + this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.IsBuyerColumn.ColumnName + " = " + isBuyer;

                    if (ownerDataView.Count <= 0)
                    {
                        try
                        {
                            newOwnersTable.ImportRow(newRow);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }

                if (newOwnersTable.Rows.Count > 0)
                {
                    DataView ownersView = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.DefaultView;
                    ownersView.RowFilter = "EmptyRecord$='True'";

                    if (ownersView.Count > 0)
                    {
                        for (int i = ownersView.Count - 1; i >= 0; i--)
                        {
                            ownersView.Delete(i);
                        }
                    }
                    ownersView.RowFilter = string.Empty;
                    this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Merge((newOwnersTable));
                }
            }

            this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Merge(dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners);
            this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.DefaultView.RowFilter = string.Empty;
            this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.AcceptChanges();
            this.OwnerGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.DefaultView;

            if (this.OwnerGridView.Rows.Count == this.OwnerGridView.OriginalRowCount)
            {
                F29555PersonalPropertySaleData.f29555_pcget_PSaleOwnersRow ownerRow = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Newf29555_pcget_PSaleOwnersRow();
                ownerRow["EmptyRecord$"] = "True";
                this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Rows.Add(ownerRow);
                this.OwnerGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.DefaultView;
            }

        }
        /// <summary>
        /// Schedules the grid bind.
        /// </summary>
        private void ScheduleGridBind()
        {
            if (dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Rows.Count > 0)
            {
                newOwnersTable.Clear();
                newOwnersTable = this.form29555Controller.WorkItem.F29555_GetSalesScheduleandOwners(scheduleId, this.ScheduleIDs, saleId, TerraScanCommon.UserId).f29555_pcget_PSaleOwners;
                DataTable dt = new DataTable();
                dt.Clear();
                if (newOwnersTable.Rows.Count > 0)
                {
                    this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Merge((newOwnersTable));
                    DataView ownersView = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.DefaultView;
                    ownersView.RowFilter = "EmptyRecord$='True'";
                    if (ownersView.Count > 0)
                    {
                        for (int i = ownersView.Count - 1; i >= 0; i--)
                        {
                            ownersView.Delete(i);
                        }
                    }
                    ownersView.RowFilter = string.Empty;
                    dt = ownersView.ToTable(true, "OwnerID", "OwnerName", "IsBuyer", "OwnerAddress", "OwnerLocation", "EmptyRecord$");
                    this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Clear();
                    this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Merge(dt);
                    this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.AcceptChanges();
                    this.OwnerGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.DefaultView;

                    if (this.OwnerGridView.Rows.Count == this.OwnerGridView.OriginalRowCount)
                    {
                        F29555PersonalPropertySaleData.f29555_pcget_PSaleOwnersRow ownerRow = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Newf29555_pcget_PSaleOwnersRow();
                        ownerRow["EmptyRecord$"] = "True";
                        this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.Rows.Add(ownerRow);
                        this.OwnerGridView.DataSource = this.dsScheduleSaleTrackingDataSet.f29555_pcget_PSaleOwners.DefaultView;
                    }
                }
            }
        }

        #endregion Private methods

    } 
    #endregion
}
