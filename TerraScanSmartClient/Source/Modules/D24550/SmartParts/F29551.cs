//--------------------------------------------------------------------------------------------
// <copyright file="F29551.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F29551. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 21 June 2011     LathaMaheswari.D    Created to implement Parcel Sale Tracking functionality
// 18 April 13      Purushotham.A       Brand name changed
//*********************************************************************************/
namespace D24550
{
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

    /// <summary>
    /// F29551 class file
    /// </summary>                                       
    [SmartPart]
    public partial class F29551 : BaseSmartPart
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
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// Instance of F29600 Controller to call the WorkItem
        /// </summary>
        private F29551Controller form29551Controller;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// An object for ContextMenuStrip
        /// </summary>
        private ContextMenuStrip selectedComponentMenuStrip = new ContextMenuStrip();

        /// <summary>
        /// Table to hold Parcel Sale Tracking Details
        /// </summary>
        private F29551ParcelSaleTrackingData parcelSaleData = new F29551ParcelSaleTrackingData();

        /// <summary>
        /// tempDataTable
        /// </summary>
        private DataTable tempDataTable = new DataTable();

        /// <summary>
        /// comboFont
        /// </summary>
        private System.Drawing.Font comboFont;

        /// <summary>
        ///  used to Convert Color From string
        /// </summary>
        private ColorConverter colorConv = new ColorConverter();

        /// <summary>
        ///  used to fill the Color 
        /// </summary>
        private SolidBrush sb = new SolidBrush(System.Drawing.Color.Black);

        /// <summary>
        /// PartiesOwnerDetailsData
        /// </summary>
        private F15010ExciseAffidavitData ownerDetailDataSet = new F15010ExciseAffidavitData();
        F29551ParcelSaleTrackingData parcelDataSet = new F29551ParcelSaleTrackingData();
        F29551ParcelSaleTrackingData.ParcelDetailsDataTable ListDataTable = new F29551ParcelSaleTrackingData.ParcelDetailsDataTable();
        F29551ParcelSaleTrackingData.OwnerDetailsDataTable newOwnersTable = new F29551ParcelSaleTrackingData.OwnerDetailsDataTable();
        F1403ParcelSearch.SaleTrakingRollYearDataTable SaleTrakingRollYearTable = new F1403ParcelSearch.SaleTrakingRollYearDataTable();

        /// <summary>
        /// Flag to identify whether the loaded record for the eventid is valid or not
        /// </summary>
        private int validRecord = 0;

        /// <summary>
        /// saleId Local variable.
        /// </summary>
        private int? saleId;

        /// <summary>
        /// ownerId Local variable.
        /// </summary>
        private int? ownerId;

        /// <summary>
        /// parcelId Local variable.
        /// </summary>
        private int? parcelId;

        private bool flagLoadOnProcess = false;

        private DataTable parcelTable = new DataTable();

        private bool isSaleVersionCreated = false;
        private string parcelIds = string.Empty;
        private bool isNewParcel = false;
        private int rollYear;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// F29551
        /// </summary>
        public F29551()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F29600"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F29551(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.SaleParcelPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SaleParcelPictureBox.Height, this.SaleParcelPictureBox.Width, SharedFunctions.GetResourceString("SaleHeader"), red, green, blue);
            //this.tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(SharedFunctions.GetResourceString("SaleOwnerID")), new DataColumn(SharedFunctions.GetResourceString("ParcelSaleOwnerId")), new DataColumn(SharedFunctions.GetResourceString("OwnerColumn")), new DataColumn(SharedFunctions.GetResourceString("IsBuyer")), new DataColumn(SharedFunctions.GetResourceString("Address1")), new DataColumn(SharedFunctions.GetResourceString("Location")), new DataColumn(SharedFunctions.GetResourceString("ParcelID")), new DataColumn(SharedFunctions.GetResourceString("SaleID")) });

            if (parcelTable.Columns.Count > 0)
            {
                parcelTable.Columns.Clear();
            }

            DataColumn ParcelNumber = new DataColumn("ParcelNumber");
            parcelTable.Columns.Add(ParcelNumber);
        }

        #endregion Constructor

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
        /// Gets or sets the F29551 control.
        /// </summary>
        /// <value>The F29511 control.</value>
        [CreateNew]
        public F29551Controller F29551Control
        {
            get { return this.form29551Controller as F29551Controller; }
            set { this.form29551Controller = value; }
        }
        #endregion Properties

        #region EventSubscription

        /// <summary>
        /// Event Subscription SetSlicePermission
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The Event.</param>
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



                    if (validRecord.Equals(0))
                    {
                        eventArgs.Data.FlagInvalidSliceKey = true;
                    }
                    else
                    {
                        //// Coding Added for the issue 4212 0n 30/5/2009.
                        //// Last Slice does not have a record also it will not return invalid slice
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            eventArgs.Data.FlagInvalidSliceKey = false;
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
            //if (this.parcelTable.Rows.Count > 0)
            //{
            this.GetSelectedParcels();
            //}

            // this.cancelOwnerFlag = true;
            // Get value to bind Gridview and other controls
            this.GetParcelSaleDetails();

            // Bind values in appropriate controls
            this.BindValues();

            this.CalculateGridTotalValues();

            if (this.isSaleVersionCreated && this.parcelTable.Rows.Count > 0)
            {
                this.SetSelectedParcels();
            }

            this.pageMode = TerraScanCommon.PageModeTypes.View;
            //this.cancelOwnerFlag = false;
            //this.tempDataTable.Clear();

            this.SalePriceTextBox.Focus();
            this.DisableGridSorting(ParcelDataGridView, false);
            this.DisableGridSorting(OwnerGridView, false);
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                if (this.slicePermissionField.editPermission)
                {
                    SliceValidationFields sliceValidationFields = new SliceValidationFields();
                    sliceValidationFields.FormNo = eventArgs.Data;
                    //this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
                }
            }
            else
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                //this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
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
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission)
            {
                //if (this.parcelTable.Rows.Count > 0)
                //{
                this.GetSelectedParcels();
                //}
                this.SaveParcelSaleRecords();
                this.tempDataTable.Clear();
                this.SalePriceTextBox.Focus();
                this.DisableGridSorting(ParcelDataGridView, false);
                this.DisableGridSorting(OwnerGridView, false);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                ////this.formLoad = false;
                this.keyId = eventArgs.Data.SelectedKeyId;
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;

                // Get value to bind Gridview and other controls
                this.GetParcelSaleDetails();


                // Bind values in appropriate controls
                this.BindValues();

                this.CalculateGridTotalValues();

                if (this.isSaleVersionCreated && this.parcelTable.Rows.Count > 0)
                {
                    this.SetSelectedParcels();
                }

                this.flagLoadOnProcess = false;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SalePriceTextBox.Focus();
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

        #region Form Load

        /// <summary>
        /// Form Loading Event
        /// </summary>
        /// <param name="sender">The Object which trigger event</param>
        /// <param name="e">The event args</param>
        private void F29551_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;

                // Load values for the current keyid
                this.LoadParcelSaleDetails();
                //modifed to load sale tracking RollYear by Purushotham
                this.LoadSaleTrackingRollYear();
                this.DisableGridSorting(ParcelDataGridView, false);
                this.DisableGridSorting(OwnerGridView, false);
                this.flagLoadOnProcess = false;
                this.SalePriceTextBox.Focus();
                // this.Height = 852;
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

        #region User Defined Methods

        #region Controls Behavior
        /// <summary>
        /// Clear All the TextBox controls 
        /// </summary>
        private void ClearTextBoxControls()
        {
            // Clear Sale Details
            this.SalePriceTextBox.Text = string.Empty;
            this.RecordingFeeTextBox.Text = string.Empty;
            this.ExciseAffidavitTextBox.Text = string.Empty;
            this.SaleDateTextBox.Text = string.Empty;
            this.FillingDateTextBox.Text = string.Empty;
            this.RecordingFeeTextBox.Text = string.Empty;
            this.MortgageTextBox.Text = string.Empty;
            this.LoanTypeTextBox.Text = string.Empty;
            this.LoanAmountTextBox.Text = string.Empty;
            this.SaleNoteTextBox.Text = string.Empty;

            // Clear Sale Adjustment Details
            this.PrimaryImprovementTextBox.Text = string.Empty;
            this.PrimaryLandTypeTextBox.Text = string.Empty;
            this.NeighborhoodTextBox.Text = string.Empty;
            this.OriginalSalePriceTextBox.Text = string.Empty;
            this.SalePerSqFtTextBox.Text = string.Empty;
            this.AdjTimeSaleTextBox.Text = string.Empty;
            this.AdjustedFinanceTextBox.Text = string.Empty;
            this.AdjustedPPTextBox.Text = string.Empty;
            this.AdjOtherTextBox.Text = string.Empty;
            this.AdjSalePriceTextBox.Text = string.Empty;
            this.AdjustedSFTextBox.Text = string.Empty;
            this.AdjPersPropTextBox.Text = string.Empty;
            this.AdjustedSqFtTextBox.Text = string.Empty;

            // Clear Sale/Review Details
            this.LastYearMarketEstimateTextBox.Text = string.Empty;
            this.LastYearPerSqTextBox.Text = string.Empty;
            this.LastYearRatioTextBox.Text = string.Empty;
            this.CurrentEstimateTextBox.Text = string.Empty;
            this.CurrentPerSqFtTextBox.Text = string.Empty;
            this.CurrentRatioTextBox.Text = string.Empty;
            this.ReviewDateTextBox.Text = string.Empty;
            this.ReviewNoteTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Clear all the selected values in Cokboboxs
        /// </summary>
        private void ClearComboControls()
        {
            // Clear Sale Details
            if (this.DeedTypeComboBox.Items.Count >= 1)
            {
                this.DeedTypeComboBox.SelectedIndex = 0;
            }
            else
            {
                this.DeedTypeComboBox.SelectedIndex = -1;
            }

            if (this.StateQualificationComboBox.Items.Count >= 1)
            {
                this.StateQualificationComboBox.SelectedIndex = 0;
            }
            else
            {
                this.StateQualificationComboBox.SelectedIndex = -1;
            }

            if (this.LandOnlySaleComboBox.Items.Count >= 1)
            {
                this.LandOnlySaleComboBox.SelectedIndex = 0;
            }
            else
            {
                this.LandOnlySaleComboBox.SelectedIndex = -1;
            }

            // Clear Sale/Review Details

            if (this.AdvisoryComboBox.Items.Count >= 1)
            {
                this.AdvisoryComboBox.SelectedIndex = 0;
            }
            else
            {
                this.AdvisoryComboBox.SelectedIndex = -1;
            }

            if (this.AssignmentComboBox.Items.Count >= 1)
            {
                this.AssignmentComboBox.SelectedIndex = 0;
            }
            else
            {
                this.AssignmentComboBox.SelectedIndex = -1;
            }

            if (this.ReviewStatusComboBox.Items.Count >= 1)
            {
                this.ReviewStatusComboBox.SelectedIndex = 0;
            }
            else
            {
                this.ReviewStatusComboBox.SelectedIndex = -1;
            }

            if (this.ReviewAppraiserComboBox.Items.Count >= 1)
            {
                this.ReviewAppraiserComboBox.SelectedIndex = 0;
            }
            else
            {
                this.ReviewAppraiserComboBox.SelectedIndex = -1;
            }

            if (this.LocalQualificationComboBox.Items.Count >= 1)
            {
                this.LocalQualificationComboBox.SelectedIndex = 0;
            }
            else
            {
                this.LocalQualificationComboBox.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Enable/Disable all the Combo controls
        /// </summary>
        /// <param name="isEnable"></param>
        private void ControlStatus(bool isEnable)
        {
            // Enable/Disable Sale Details
            this.DeedTypeComboBox.Enabled = !isEnable; ;
            this.StateQualificationComboBox.Enabled = !isEnable; ;
            this.LandOnlySaleComboBox.Enabled = !isEnable; ;

            // Enable/Disable Sale/Review Details
            this.AdvisoryComboBox.Enabled = !isEnable;
            this.AssignmentComboBox.Enabled = !isEnable; ;
            this.ReviewStatusComboBox.Enabled = !isEnable; ;
            this.ReviewAppraiserComboBox.Enabled = !isEnable; ;
            this.LocalQualificationComboBox.Enabled = !isEnable; ;

            this.ParcelDataGridView.Enabled = !isEnable;
            this.OwnerGridView.Enabled = !isEnable;
        }

        /// <summary>
        /// Enable/Disable all teh panels 
        /// </summary>
        /// <param name="isEnable"></param>
        private void PanelStatus(bool isEnable)
        {
            // Enable/Disable Sale Details
            this.SalePricePanel.Enabled = isEnable;
            this.RecordingPanel.Enabled = isEnable;
            this.ExciseAffidavitPanel.Enabled = isEnable;
            this.SalePricePanel.Enabled = isEnable;
            this.SaleDatePanel.Enabled = isEnable;
            this.FillingDatePanel.Enabled = isEnable;
            this.RecordingFeePanel.Enabled = isEnable;
            this.MortgagePanel.Enabled = isEnable;
            this.LoanTypePanel.Enabled = isEnable;
            this.LoanAmountPanel.Enabled = isEnable;
            this.SaleNotePanel.Enabled = isEnable;

            this.HeaderParcelsPanel.Enabled = isEnable;
            this.ParcelPanel.Enabled = isEnable;
            this.HeaderOwnerPanel.Enabled = isEnable;
            this.OwnerPanel.Enabled = isEnable;

            // Enable/Disable Sale Adjustment Details
            this.PrimaryImprovementPanel.Enabled = isEnable;
            this.PrimaryLandTypePanel.Enabled = isEnable;
            this.NeighborhoodPanel.Enabled = isEnable;
            this.OriginalSalePricePanel.Enabled = isEnable;
            this.SalePerSqFtPanel.Enabled = isEnable;
            this.AdjTimeSalePanel.Enabled = isEnable;
            this.AdjustedFinancePanel.Enabled = isEnable;
            this.AdjustedPPPanel.Enabled = isEnable;
            this.AdjOtherPanel.Enabled = isEnable;
            this.AdjSalePricePanel.Enabled = isEnable;
            this.AdjustedSFPanel.Enabled = isEnable;
            this.DeedTypePanel.Enabled = isEnable;
            this.StateQualificationPanel.Enabled = isEnable;
            this.LandOnlySalePanel.Enabled = isEnable;

            // Enable/Disable Sale/Review Details
            this.LastYearMarketEstimatePanel.Enabled = isEnable;
            this.LastYearPerSqPanel.Enabled = isEnable;
            this.LastYearRatioPanel.Enabled = isEnable;
            this.CurrentEstimatePanel.Enabled = isEnable;
            this.CurrentPerSqFtPanel.Enabled = isEnable;
            this.CurrentRatioPanel.Enabled = isEnable;
            this.AdvisoryPanel.Enabled = isEnable;
            this.ReviewDatePanel.Enabled = isEnable;
            this.ReviewNotePanel.Enabled = isEnable;
            this.AssignmentPanel.Enabled = isEnable;
            this.ReviewStatusPanel.Enabled = isEnable;
            this.ReviewAppraiserPanel.Enabled = isEnable;
            this.LocalQualificationPanel.Enabled = isEnable;
        }

        /// <summary>
        /// Lock/UnLock key press on textbox controls
        /// </summary>
        /// <param name="lockKey"></param>
        private void LockControls(bool lockKey)
        {
            // Clear Sale Details
            this.SalePriceTextBox.LockKeyPress = lockKey;
            this.RecordingFeeTextBox.LockKeyPress = lockKey;
            this.ExciseAffidavitTextBox.LockKeyPress = lockKey;
            this.SalePriceTextBox.LockKeyPress = lockKey;
            this.FillingDateTextBox.LockKeyPress = lockKey;
            this.RecordingFeeTextBox.LockKeyPress = lockKey;
            this.MortgageTextBox.LockKeyPress = lockKey;
            this.LoanTypeTextBox.LockKeyPress = lockKey;
            this.LoanAmountTextBox.LockKeyPress = lockKey;
            this.SaleNoteTextBox.LockKeyPress = lockKey;

            // Clear Sale Adjustment Details
            //this.PrimaryImprovementTextBox.LockKeyPress = lockKey;
            //this.PrimaryLandTypeTextBox.LockKeyPress = lockKey;
            //this.NeighborhoodTextBox.LockKeyPress = lockKey;
            //this.OriginalSalePriceTextBox.LockKeyPress = lockKey;
            this.SalePerSqFtTextBox.LockKeyPress = lockKey;
            this.AdjTimeSaleTextBox.LockKeyPress = lockKey;
            this.AdjustedFinanceTextBox.LockKeyPress = lockKey;
            this.AdjustedPPTextBox.LockKeyPress = lockKey;
            this.AdjOtherTextBox.LockKeyPress = lockKey;
            //this.AdjSalePriceTextBox.LockKeyPress = lockKey;
            this.AdjustedSFTextBox.LockKeyPress = lockKey;

            // Clear Sale/Review Details
            //this.LastYearMarketEstimateTextBox.LockKeyPress = lockKey;
            this.LastYearPerSqTextBox.LockKeyPress = lockKey;
            //this.LastYearRatioTextBox.LockKeyPress = lockKey;
            //this.CurrentEstimateTextBox.LockKeyPress = lockKey;
            this.CurrentPerSqFtTextBox.LockKeyPress = lockKey;
            //this.CurrentRatioTextBox.LockKeyPress = lockKey;
            this.ReviewDateTextBox.LockKeyPress = lockKey;
            this.ReviewNoteTextBox.LockKeyPress = lockKey;
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SetEditRecord(object sender, EventArgs e)
        {
            this.EditRecord();
        }

        /// <summary>
        /// Set Edit mode
        /// </summary>
        private void EditRecord()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.DisableGridSorting(this.ParcelDataGridView, true);
                this.DisableGridSorting(this.OwnerGridView, true);
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                this.CreateSaleVersionButton.Enabled = false;
                this.TransferOwnershipButton.Enabled = false;
                this.RefreshValuesButton.Enabled = false;
            }
        }

        private void TextBox_Validating(object sender, CancelEventArgs e)
        {
            if (((TerraScanTextBox)sender).DecimalTextBoxValue > int.MaxValue)
            {
                ((TerraScanTextBox)sender).Text = "0";
            }

            this.CalculateAdjustedSalePrice();
            // this.EditRecord();
        }

        private void SalePrice_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if ((double)((TerraScanTextBox)sender).DecimalTextBoxValue > 922337203685477.5807)
                {
                    ((TerraScanTextBox)sender).Text = "0";
                }

                this.CalculateAdjustedSalePrice();
            }
            catch (Exception ex)
            {
            }
        }

        private void Money_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if ((double)((TerraScanTextBox)sender).DecimalTextBoxValue > 922337203685477.5807)
                {
                    ((TerraScanTextBox)sender).Text = "0";
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void Decimal_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if ((double)((TerraScanTextBox)sender).DecimalTextBoxValue > 99999.99)
                {
                    ((TerraScanTextBox)sender).Text = "0";
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void DecimalMax_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if ((double)((TerraScanTextBox)sender).DecimalTextBoxValue > 9999999999999.99)
                {
                    ((TerraScanTextBox)sender).Text = "0";
                }

            }
            catch (Exception ex)
            {
            }
        }


        #endregion Controls Behavior

        private void LoadParcelSaleDetails()
        {
            // Get values to bind combobox
            this.GetComboDetails();

            // Get value to bind Gridview and other controls
            this.GetParcelSaleDetails();

            // Populate all combo
            this.LoadCombo();

            // Customize Grid columns
            this.CustomizeParcelGrid();
            this.CustomizeOwnerGrid();

            // Bind values in appropriate controls
            this.BindValues();

            // Calculate sum of parcel grid values
            this.CalculateGridTotalValues();
        }

        #region Data Fetching

        private void GetParcelSaleDetails()
        {
            this.parcelSaleData.SaleDetails.Clear();
            this.parcelSaleData.ParcelDetails.Clear();
            this.parcelSaleData.OwnerDetails.Clear();
            this.parcelSaleData.ReturnMessage.Clear();
            // this.parcelSaleData=this.F29551Control.WorkItem.F29551_GetParcelSaleTrackingDetails(this.keyId, TerraScanCommon.UserId);
            this.parcelSaleData.Merge(this.F29551Control.WorkItem.F29551_GetParcelSaleTrackingDetails(this.keyId, TerraScanCommon.UserId));
        }

        private void GetComboDetails()
        {
            this.parcelSaleData = this.F29551Control.WorkItem.F29551_GetParcelSaleComboDetails(TerraScanCommon.UserId);
        }

        private F29551ParcelSaleTrackingData GetParcelDetails(int parcelId)
        {
            //this.parcelSaleData.ParcelDetails.Clear();
            //this.parcelSaleData.OwnerDetails.Clear();
            DataTable dummyTable = new DataTable();
            DataColumn keyID = new DataColumn("KeyID");
            dummyTable.Columns.Add(keyID);
            string keyXML = TerraScanCommon.GetXmlString(dummyTable);

            /*for (int i = 0; i < this.ParcelDataGridView.OriginalRowCount; i++)
              {
                  if (this.ParcelDataGridView.Rows[i].Cells[this.parcelSaleData.ParcelDetails.ParcelIDColumn.ColumnName].Value != null 
                      && !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[i].Cells[this.parcelSaleData.ParcelDetails.ParcelIDColumn.ColumnName].Value.ToString().Trim()))
                  {
                      DataRow existRow = dummyTable.NewRow();
                      existRow[0] = this.ParcelDataGridView.Rows[i].Cells[this.parcelSaleData.ParcelDetails.ParcelIDColumn.ColumnName].ToString();
                      dummyTable.Rows.Add(existRow);
                  }
              }

              DataRow newRow = dummyTable.NewRow();
              newRow[0] = parcelId.ToString();
              dummyTable.Rows.Add(newRow);*/

            DataSet ds = new DataSet();
            ds.Tables.Add(dummyTable);
            ds.RemotingFormat = SerializationFormat.Binary;
            return this.F29551Control.WorkItem.F29551_GetParcelOwnerDetails(parcelId, keyXML, null, TerraScanCommon.UserId);
        }

        private void GetOwnerDetails()
        {
            this.parcelSaleData.OwnerDetails.Clear();
            this.parcelSaleData.OwnerDetails.Merge(this.F29551Control.WorkItem.F29551_GetOwnerDetails(this.saleId, null, null, TerraScanCommon.UserId).OwnerDetails);
        }

        #endregion Data Fetching

        #region Binding Controls

        /// <summary>
        /// Bind Sales details on appropriate controls
        /// </summary>
        private void BindValues()
        {
            if (this.parcelSaleData.ValidRecord.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.parcelSaleData.ValidRecord.Rows[0][this.parcelSaleData.ValidRecord.IsValidColumn.ColumnName].ToString().Trim()))
                {
                    int.TryParse(this.parcelSaleData.ValidRecord.Rows[0][this.parcelSaleData.ValidRecord.IsValidColumn.ColumnName].ToString().Trim(), out validRecord);
                }
            }

            if (this.parcelSaleData.SaleDetails.Rows.Count > 0 && validRecord > 0)
            {
                F29551ParcelSaleTrackingData.SaleDetailsRow saleDetails = (F29551ParcelSaleTrackingData.SaleDetailsRow)this.parcelSaleData.SaleDetails.Rows[0];

                if (!saleDetails.IsSaleIDNull())
                {
                    this.saleId = saleDetails.SaleID;
                }
                else
                {
                    this.saleId = null;
                }

                if (!saleDetails.IsParcelIDNull())
                {
                    this.parcelId = saleDetails.ParcelID;
                }
                else
                {
                    this.parcelId = null;
                }

                if (!saleDetails.IsParcelIDNull())
                {
                    this.parcelId = saleDetails.ParcelID;
                }
                else
                {
                    this.parcelId = null;
                }

                // Bind values under Sale Details header
                if (!saleDetails.IsOriginalSalepriceNull())
                {
                    this.SalePriceTextBox.Text = saleDetails.OriginalSaleprice.ToString();
                }
                else
                {
                    this.SalePriceTextBox.EmptyDecimalValue = true;
                    this.SalePriceTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsDeedTypeNull())
                {
                    this.DeedTypeComboBox.Text = saleDetails.DeedType.Trim();
                }
                else
                {
                    if (this.DeedTypeComboBox.Items.Count >= 1)
                    {
                        this.DeedTypeComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        this.DeedTypeComboBox.SelectedIndex = -1;
                    }
                }

                if (!saleDetails.IsRecordingNumberNull())
                {
                    this.RecordingNumberTextBox.Text = saleDetails.RecordingNumber.Trim();
                }
                else
                {
                    this.RecordingNumberTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsExciseNumberNull())
                {
                    this.ExciseAffidavitTextBox.Text = saleDetails.ExciseNumber.Trim();
                }
                else
                {
                    this.ExciseAffidavitTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsStateCategoryIDNull())
                {
                    this.StateQualificationComboBox.SelectedValue = saleDetails.StateCategoryID;
                }
                else
                {
                    if (this.StateQualificationComboBox.Items.Count >= 1)
                    {
                        this.StateQualificationComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        this.StateQualificationComboBox.SelectedIndex = -1;
                    }
                }

                if (!saleDetails.IsSaleDateNull())
                {
                    this.SaleDateTextBox.Text = saleDetails.SaleDate.Trim();
                }
                else
                {
                    this.SaleDateTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsFilingDateNull())
                {
                    this.FillingDateTextBox.Text = saleDetails.FilingDate.Trim();
                }
                else
                {
                    this.FillingDateTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsRecordingFeeNull())
                {
                    this.RecordingFeeTextBox.Text = saleDetails.RecordingFee.ToString();
                }
                else
                {
                    this.RecordingFeeTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsMortgageInstrumentNumberNull())
                {
                    this.MortgageTextBox.Text = saleDetails.MortgageInstrumentNumber.Trim();
                }
                else
                {
                    this.MortgageTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsLoanTypeNull())
                {
                    this.LoanTypeTextBox.Text = saleDetails.LoanType.Trim();
                }
                else
                {
                    this.LoanTypeTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsLoanAmountNull())
                {
                    this.LoanAmountTextBox.Text = saleDetails.LoanAmount.ToString();
                }
                else
                {
                    this.LoanAmountTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsSaleNoteNull())
                {
                    this.SaleNoteTextBox.Text = saleDetails.SaleNote.Trim();
                }
                else
                {
                    this.SaleNoteTextBox.Text = string.Empty;
                }

                // Defaule Blank/Null, Yes, No
                if (!saleDetails.IsIsLandOnlySaleNull())
                {
                    if (saleDetails.IsLandOnlySale.ToUpper().Equals("TRUE"))
                    {
                        this.LandOnlySaleComboBox.SelectedValue = 1;
                    }
                    else if (saleDetails.IsLandOnlySale.ToUpper().Equals("FALSE"))
                    {
                        this.LandOnlySaleComboBox.SelectedValue = 0;
                    }
                    else
                    {
                        // Set selected text as '--Select--'
                        this.LandOnlySaleComboBox.SelectedValue = 2;
                    }
                }
                else
                {
                    // Set selected text as '--Select--'
                    this.LandOnlySaleComboBox.SelectedValue = 2;
                }

                // Bind valus on controls under Sale Adjustment header
                if (!saleDetails.IsPrimaryImprovementNull())
                {
                    this.PrimaryImprovementTextBox.Text = saleDetails.PrimaryImprovement.Trim();
                }
                else
                {
                    this.PrimaryImprovementTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsPrimaryLandTypeNull())
                {
                    this.PrimaryLandTypeTextBox.Text = saleDetails.PrimaryLandType.Trim();
                }
                else
                {
                    this.PrimaryLandTypeTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsNighborhoodNull())
                {
                    this.NeighborhoodTextBox.Text = saleDetails.Nighborhood.Trim();
                }
                else
                {
                    this.NeighborhoodTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsOriginalSalepriceNull())
                {
                    this.OriginalSalePriceTextBox.Text = saleDetails.OriginalSaleprice.ToString();
                }
                else
                {
                    this.OriginalSalePriceTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsOriginalSquareFeetNull())
                {
                    this.SalePerSqFtTextBox.Text = saleDetails.OriginalSquareFeet.ToString();
                }
                else
                {
                    this.SalePerSqFtTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsAdjTimeSaleNull())
                {
                    this.AdjTimeSaleTextBox.Text = saleDetails.AdjTimeSale.ToString();
                }
                else
                {
                    this.AdjTimeSaleTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsAdjFinanceNull())
                {
                    this.AdjustedFinanceTextBox.Text = saleDetails.AdjFinance.ToString();
                }
                else
                {
                    this.AdjustedFinanceTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsAdjPersPropNull())
                {
                    this.AdjPersPropTextBox.Text = saleDetails.AdjPersProp.ToString().Trim();
                }
                else
                {
                    this.AdjPersPropTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsAdjustOtherNull())
                {
                    this.AdjOtherTextBox.Text = saleDetails.AdjustOther.ToString().Trim();
                }
                else
                {
                    this.AdjOtherTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsAdjustedSquareFeetNull())
                {
                    this.AdjustedSqFtTextBox.Text = saleDetails.AdjustedSquareFeet.ToString().Trim();
                }
                else
                {
                    this.AdjustedSqFtTextBox.Text = string.Empty;
                }

                // Bind values on controls under Sale Review Header
                if (!saleDetails.IsCurrentEstimateNull())
                {
                    this.LastYearMarketEstimateTextBox.Text = saleDetails.CurrentEstimate.ToString().Trim();
                }
                else
                {
                    this.LastYearMarketEstimateTextBox.Text = string.Empty;
                }

                /* if (!saleDetails.IsRollYearNull())
                 {
                     string lastYear = (saleDetails.RollYear - 1).ToString();
                     this.LastYearMarketEstimateLabel.Text = lastYear.Trim() + " Market Estimate:";
                     this.CurrentEstimateLabel.Text = saleDetails.RollYear.ToString().Trim() + " Market Estimate:";
                 }
                 else
                 {
                     this.LastYearMarketEstimateLabel.Text = "Market Estimate:";
                     this.CurrentEstimateLabel.Text = "Market Estimate:";
                 }*/

                if (!saleDetails.IsCurrentPerSqFtNull())
                {
                    this.LastYearPerSqTextBox.Text = saleDetails.CurrentPerSqFt.ToString().Trim();
                }
                else
                {
                    this.LastYearPerSqTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsCurrentRatioNull())
                {
                    this.LastYearRatioTextBox.Text = saleDetails.CurrentRatio.ToString().Trim();
                }
                else
                {
                    this.LastYearRatioTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsAppraisalEstimateNull())
                {
                    this.CurrentEstimateTextBox.Text = saleDetails.AppraisalEstimate.ToString().Trim();
                }
                else
                {
                    this.CurrentEstimateTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsAppraisalPerSqFtNull())
                {
                    this.CurrentPerSqFtTextBox.Text = saleDetails.AppraisalPerSqFt.ToString().Trim();
                }
                else
                {
                    this.CurrentPerSqFtTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsAppraisalRatioNull())
                {
                    this.CurrentRatioTextBox.Text = saleDetails.AppraisalRatio.ToString().Trim();
                }
                else
                {
                    this.CurrentRatioTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsAdvisoryIDNull())
                {
                    this.AdvisoryComboBox.SelectedValue = saleDetails.AdvisoryID;
                }
                else
                {
                    if (this.AdvisoryComboBox.Items.Count >= 1)
                    {
                        this.AdvisoryComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        this.AdvisoryComboBox.SelectedIndex = -1;
                    }
                }

                if (!saleDetails.IsAssignmentIDNull())
                {
                    this.AssignmentComboBox.SelectedValue = saleDetails.AssignmentID;
                }
                else
                {
                    if (this.AssignmentComboBox.Items.Count >= 1)
                    {
                        this.AssignmentComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        this.AssignmentComboBox.SelectedIndex = -1;
                    }
                }

                if (!saleDetails.IsStatusIDNull())
                {
                    this.ReviewStatusComboBox.SelectedValue = saleDetails.StatusID;
                }
                else
                {
                    if (this.ReviewStatusComboBox.Items.Count >= 1)
                    {
                        this.ReviewStatusComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        this.ReviewStatusComboBox.SelectedIndex = -1;
                    }
                }

                if (!saleDetails.IsAppraiserIDNull())
                {
                    this.ReviewAppraiserComboBox.SelectedValue = saleDetails.AppraiserID;
                }
                else
                {
                    if (this.ReviewAppraiserComboBox.Items.Count >= 1)
                    {
                        this.ReviewAppraiserComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        this.ReviewAppraiserComboBox.SelectedIndex = -1;
                    }
                }

                if (!saleDetails.IsReviewDateNull())
                {
                    this.ReviewDateTextBox.Text = saleDetails.ReviewDate.ToString(); ;
                }
                else
                {
                    this.ReviewDateTextBox.Text = string.Empty;
                }

                if (!saleDetails.IsLocalQualificationIDNull())
                {
                    this.LocalQualificationComboBox.SelectedValue = saleDetails.LocalQualificationID;
                }
                else
                {
                    if (this.LocalQualificationComboBox.Items.Count >= 1)
                    {
                        this.LocalQualificationComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        this.LocalQualificationComboBox.SelectedIndex = -1;
                    }
                }

                if (!saleDetails.IsReviewNoteNull())
                {
                    this.ReviewNoteTextBox.Text = saleDetails.ReviewNote.Trim();
                }
                else
                {
                    this.ReviewNoteTextBox.Text = string.Empty;
                }

                this.CalculateAdjustedSalePrice();

                this.PanelStatus(true);

                this.LockControls(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                this.ControlStatus(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);

                if (this.saleId == 0)
                {
                    if (this.PermissionFiled.newPermission)
                    {
                        this.LockControls(false);
                        this.ControlStatus(false);
                        this.SummaryPanel.Enabled = true;
                    }
                    else
                    {
                        this.LockControls(true);
                        this.ControlStatus(true);
                        this.SummaryPanel.Enabled = false;
                    }
                }
            }
            else
            {
                this.ClearTextBoxControls();
                this.PanelStatus(false);
                this.ClearComboControls();
            }

            //if (this.parcelSaleData.ParcelDetails.Rows.Count > 0)
            //{
            this.ParcelDataGridView.DataSource = this.parcelSaleData.ParcelDetails.DefaultView;
            if (this.ParcelDataGridView.Rows.Count == this.ParcelDataGridView.OriginalRowCount)
            {
                F29551ParcelSaleTrackingData.ParcelDetailsRow newRow = this.parcelSaleData.ParcelDetails.NewParcelDetailsRow();
                newRow["EmptyRecord$"] = "True";
                this.parcelSaleData.ParcelDetails.Rows.Add(newRow);
                this.ParcelDataGridView.DataSource = this.parcelSaleData.ParcelDetails.DefaultView;
            }

            // this.ParcelDataGridView.DataSource = this.parcelSaleData.ParcelDetails.DefaultView;
            //}

            //if (this.parcelSaleData.OwnerDetails.Rows.Count > 0)
            //{
            DataTable buyerDatatable = new DataTable();
            if (buyerDatatable.Columns.Count == 0)
            {
                buyerDatatable.Columns.Add(this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName, typeof(int));
                buyerDatatable.Columns.Add("Grantee", typeof(string));
            }

            DataRow dr;
            dr = buyerDatatable.NewRow();
            dr[this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName] = 1;
            dr["Grantee"] = "Grantee";
            buyerDatatable.Rows.Add(dr);

            dr = buyerDatatable.NewRow();
            dr[this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName] = 0;
            dr["Grantee"] = "Grantor";
            buyerDatatable.Rows.Add(dr);
            this.OwnerGridView.DataSource = this.parcelSaleData.OwnerDetails.DefaultView;
            (this.OwnerGridView.Columns[this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName] as DataGridViewComboBoxColumn).DataSource = buyerDatatable.Copy();
            (this.OwnerGridView.Columns[this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName] as DataGridViewComboBoxColumn).DisplayMember = buyerDatatable.Columns[1].ToString();
            (this.OwnerGridView.Columns[this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName] as DataGridViewComboBoxColumn).ValueMember = buyerDatatable.Columns[0].ToString();
            (this.OwnerGridView.Columns[this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName] as DataGridViewComboBoxColumn).ReadOnly = false;

            // If the last record got edited add new row at the bottom of the grid
            if (this.OwnerGridView.Rows.Count == this.OwnerGridView.OriginalRowCount)
            {
                F29551ParcelSaleTrackingData.OwnerDetailsRow newRow = this.parcelSaleData.OwnerDetails.NewOwnerDetailsRow();
                newRow["EmptyRecord$"] = "True";
                this.parcelSaleData.OwnerDetails.Rows.Add(newRow);
                this.OwnerGridView.DataSource = this.parcelSaleData.OwnerDetails.DefaultView;
            }
            //}

            if (this.ParcelDataGridView.Rows.Count > this.ParcelDataGridView.NumRowsVisible)
            {
                this.ParcelGridVScrollBar.Visible = false;
            }
            else
            {
                this.ParcelGridVScrollBar.Visible = true;
            }

            if (this.OwnerGridView.Rows.Count > this.OwnerGridView.NumRowsVisible)
            {
                this.OwnerGridVScrollBar.Visible = false;
            }
            else
            {
                this.OwnerGridVScrollBar.Visible = true;
            }
            this.CreateSaleVersionButton.Enabled = true;
            this.TransferOwnershipButton.Enabled = true;
            this.RefreshValuesButton.Enabled = true;

            this.ParcelRecordCount();
        }

        #endregion Binding Controls

        #region Calculation

        private void CalculateAdjustedSalePrice()
        {
            decimal originalSalePrice = 0;
            if (!string.IsNullOrEmpty(this.SalePriceTextBox.Text.Trim()))
            {
                originalSalePrice = this.SalePriceTextBox.DecimalTextBoxValue;
            }

            this.OriginalSalePriceTextBox.Text = this.SalePriceTextBox.Text;

            int adjustedTimeSale = 0;
            if (!string.IsNullOrEmpty(this.AdjTimeSaleTextBox.Text.Trim()))
            {
                int.TryParse(this.AdjTimeSaleTextBox.DecimalTextBoxValue.ToString(), out adjustedTimeSale);
            }

            int adjustedFinance = 0;
            if (!string.IsNullOrEmpty(this.AdjustedFinanceTextBox.Text.Trim()))
            {
                int.TryParse(this.AdjustedFinanceTextBox.DecimalTextBoxValue.ToString(), out adjustedFinance);
            }

            int adjustedPersProp = 0;
            if (!string.IsNullOrEmpty(this.AdjPersPropTextBox.Text.Trim()))
            {
                int.TryParse(this.AdjPersPropTextBox.DecimalTextBoxValue.ToString(), out adjustedPersProp);
            }

            int adjustedOther = 0;
            if (!string.IsNullOrEmpty(this.AdjOtherTextBox.Text.Trim()))
            {
                int.TryParse(this.AdjOtherTextBox.DecimalTextBoxValue.ToString(), out adjustedOther);
            }

            decimal adjustedSalePrice = originalSalePrice + adjustedTimeSale + adjustedFinance + adjustedPersProp + adjustedOther;

            if (adjustedSalePrice > 0)
            {
                this.AdjSalePriceTextBox.Text = adjustedSalePrice.ToString().Trim();
            }
            else
            {
                this.AdjSalePriceTextBox.Text = "0";
            }
        }

        #endregion Calculation

        #region Combo Methods

        private void LoadCombo()
        {
            // Load Deed Type Combo
            this.DeedTypeComboBox.DisplayMember = this.parcelSaleData.DeedType.DeedTypeColumn.ColumnName;
            this.DeedTypeComboBox.ValueMember = this.parcelSaleData.DeedType.DeedTypeIDColumn.ColumnName;
            this.DeedTypeComboBox.DataSource = this.parcelSaleData.DeedType;

            // Load State Qualification Combo
            this.StateQualificationComboBox.DisplayMember = this.parcelSaleData.StateList.StateQualificationColumn.ColumnName;
            this.StateQualificationComboBox.ValueMember = this.parcelSaleData.StateList.StateQualificationIDColumn.ColumnName;
            this.StateQualificationComboBox.DataSource = this.parcelSaleData.StateList;

            // Load Advisoy Combo
            this.AdvisoryComboBox.DisplayMember = this.parcelSaleData.Advisory.AdvisoryColumn.ColumnName;
            this.AdvisoryComboBox.ValueMember = this.parcelSaleData.Advisory.AdvisoryIDColumn.ColumnName;
            this.AdvisoryComboBox.DataSource = this.parcelSaleData.Advisory;

            // Load Assignment combo
            this.AssignmentComboBox.DisplayMember = this.parcelSaleData.Assignment.AssignmentColumn.ColumnName;
            this.AssignmentComboBox.ValueMember = this.parcelSaleData.Assignment.AssignmentIDColumn.ColumnName;
            this.AssignmentComboBox.DataSource = this.parcelSaleData.Assignment;

            // Load Status combo
            this.ReviewStatusComboBox.DisplayMember = this.parcelSaleData.Status.StatusColumn.ColumnName;
            this.ReviewStatusComboBox.ValueMember = this.parcelSaleData.Status.StatusIDColumn.ColumnName;
            this.ReviewStatusComboBox.DataSource = this.parcelSaleData.Status;

            // Load Appraiser Combo
            this.ReviewAppraiserComboBox.DisplayMember = this.parcelSaleData.Apprasiser.AppraiserColumn.ColumnName;
            this.ReviewAppraiserComboBox.ValueMember = this.parcelSaleData.Apprasiser.AppraiserIDColumn.ColumnName;
            this.ReviewAppraiserComboBox.DataSource = this.parcelSaleData.Apprasiser;

            // Load Local Qualification Combo
            this.LocalQualificationComboBox.DisplayMember = this.parcelSaleData.LocalQualification.LocalQualificationColumn.ColumnName;
            this.LocalQualificationComboBox.ValueMember = this.parcelSaleData.LocalQualification.LocalQualificationIDColumn.ColumnName;
            this.LocalQualificationComboBox.DataSource = this.parcelSaleData.LocalQualification;

            DataTable LandOnlySaleDatatable = new DataTable();
            if (LandOnlySaleDatatable.Columns.Count == 0)
            {
                LandOnlySaleDatatable.Columns.Add("LandOnlyId", typeof(int));
                LandOnlySaleDatatable.Columns.Add("LandOnly", typeof(string));
            }

            // Load Land Only Sale Combo
            DataRow dr;
            dr = LandOnlySaleDatatable.NewRow();
            dr["LandOnlyId"] = 2;
            dr["LandOnly"] = "--Select--";
            LandOnlySaleDatatable.Rows.Add(dr);

            dr = LandOnlySaleDatatable.NewRow();
            dr["LandOnlyId"] = 1;
            dr["LandOnly"] = "Yes";
            LandOnlySaleDatatable.Rows.Add(dr);

            dr = LandOnlySaleDatatable.NewRow();
            dr["LandOnlyId"] = 0;
            dr["LandOnly"] = "No";
            LandOnlySaleDatatable.Rows.Add(dr);

            this.LandOnlySaleComboBox.DisplayMember = LandOnlySaleDatatable.Columns[1].ColumnName;
            this.LandOnlySaleComboBox.ValueMember = LandOnlySaleDatatable.Columns[0].ColumnName;
            this.LandOnlySaleComboBox.DataSource = LandOnlySaleDatatable;
        }

        #endregion Combo Methods

        #region Grid Methods

        /// <summary>
        /// Customize Parcel Grid
        /// </summary>
        private void CustomizeParcelGrid()
        {
            try
            {
                this.ParcelDataGridView.AutoGenerateColumns = false;
                this.ParcelDataGridView.Columns[this.parcelSaleData.ParcelDetails.SaleParcelIDColumn.ColumnName].DataPropertyName = this.parcelSaleData.ParcelDetails.SaleParcelIDColumn.ColumnName;
                this.ParcelDataGridView.Columns[this.parcelSaleData.ParcelDetails.IsBaseParcelColumn.ColumnName].DataPropertyName = this.parcelSaleData.ParcelDetails.IsBaseParcelColumn.ColumnName;
                this.ParcelDataGridView.Columns[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName].DataPropertyName = this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName;
                this.ParcelDataGridView.Columns[this.parcelSaleData.ParcelDetails.AcresColumn.ColumnName].DataPropertyName = this.parcelSaleData.ParcelDetails.AcresColumn.ColumnName;
                this.ParcelDataGridView.Columns[this.parcelSaleData.ParcelDetails.LandColumn.ColumnName].DataPropertyName = this.parcelSaleData.ParcelDetails.LandColumn.ColumnName;
                this.ParcelDataGridView.Columns[this.parcelSaleData.ParcelDetails.ImprColumn.ColumnName].DataPropertyName = this.parcelSaleData.ParcelDetails.ImprColumn.ColumnName;
                this.ParcelDataGridView.Columns[this.parcelSaleData.ParcelDetails.CropColumn.ColumnName].DataPropertyName = this.parcelSaleData.ParcelDetails.CropColumn.ColumnName;
                this.ParcelDataGridView.Columns[this.parcelSaleData.ParcelDetails.TotalsColumn.ColumnName].DataPropertyName = this.parcelSaleData.ParcelDetails.TotalsColumn.ColumnName;
                this.ParcelDataGridView.Columns[this.parcelSaleData.ParcelDetails.ParcelIDColumn.ColumnName].DataPropertyName = this.parcelSaleData.ParcelDetails.ParcelIDColumn.ColumnName;
                this.ParcelDataGridView.Columns[this.parcelSaleData.ParcelDetails.DORColumn.ColumnName].DataPropertyName = this.parcelSaleData.ParcelDetails.DORColumn.ColumnName;
                this.ParcelDataGridView.Columns[this.parcelSaleData.ParcelDetails.DORTooltipColumn.ColumnName].DataPropertyName = this.parcelSaleData.ParcelDetails.DORTooltipColumn.ColumnName;

                this.ParcelDataGridView.PrimaryKeyColumnName = this.parcelSaleData.ParcelDetails.ParcelIDColumn.ToString();

            }
            catch (Exception ex)
            {
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
                this.OwnerGridView.Columns[this.parcelSaleData.OwnerDetails.OwnerNameColumn.ColumnName].DataPropertyName = this.parcelSaleData.OwnerDetails.OwnerNameColumn.ColumnName;
                this.OwnerGridView.Columns[this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName].DataPropertyName = this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName;
                this.OwnerGridView.Columns[this.parcelSaleData.OwnerDetails.OwnerAddressColumn.ColumnName].DataPropertyName = this.parcelSaleData.OwnerDetails.OwnerAddressColumn.ColumnName;
                this.OwnerGridView.Columns[this.parcelSaleData.OwnerDetails.OwnerLocationColumn.ColumnName].DataPropertyName = this.parcelSaleData.OwnerDetails.OwnerLocationColumn.ColumnName;
                this.OwnerGridView.Columns[this.parcelSaleData.OwnerDetails.OwnerIDColumn.ColumnName].DataPropertyName = this.parcelSaleData.OwnerDetails.OwnerIDColumn.ColumnName;

                this.OwnerGridView.Columns[this.parcelSaleData.OwnerDetails.OwnerNameColumn.ColumnName].DisplayIndex = 0;
                this.OwnerGridView.Columns[this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName].DisplayIndex = 1;
                this.OwnerGridView.Columns[this.parcelSaleData.OwnerDetails.OwnerAddressColumn.ColumnName].DisplayIndex = 2;
                this.OwnerGridView.Columns[this.parcelSaleData.OwnerDetails.OwnerLocationColumn.ColumnName].DisplayIndex = 3;
                this.OwnerGridView.Columns[this.parcelSaleData.OwnerDetails.OwnerIDColumn.ColumnName].DisplayIndex = 4;

                this.OwnerGridView.PrimaryKeyColumnName = this.parcelSaleData.OwnerDetails.OwnerIDColumn.ToString();
                //this.OwnerGridView.Height = 134;

                // this.OwnerGridVScrollBar.Height = 128;
                //this.OwnerGridView.BackgroundColor = System.Drawing.Color.FromArgb(64, 64, 64);

                DataTable buyerDatatable = new DataTable();
                if (buyerDatatable.Columns.Count == 0)
                {
                    buyerDatatable.Columns.Add(this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName, typeof(int));
                    buyerDatatable.Columns.Add("Grantee", typeof(string));
                }

                DataRow dr;
                dr = buyerDatatable.NewRow();
                dr[this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName] = 1;
                dr["Grantee"] = "Grantee";
                buyerDatatable.Rows.Add(dr);

                dr = buyerDatatable.NewRow();
                dr[this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName] = 0;
                dr["Grantee"] = "Grantor";
                buyerDatatable.Rows.Add(dr);

                (this.OwnerGridView.Columns[this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName] as DataGridViewComboBoxColumn).DataSource = buyerDatatable.Copy();
                (this.OwnerGridView.Columns[this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName] as DataGridViewComboBoxColumn).DisplayMember = buyerDatatable.Columns[1].ToString();
                (this.OwnerGridView.Columns[this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName] as DataGridViewComboBoxColumn).ValueMember = buyerDatatable.Columns[0].ToString();
                //(this.OwnerGridView.Columns[this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName] as DataGridViewComboBoxColumn).ReadOnly = false;

            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Disable Grid Sorting
        /// </summary>
        /// <param name="dataGridViewSorting">dataGridViewSorting</param>
        /// <param name="disableSort">disableSort</param>
        private void DisableGridSorting(TerraScan.UI.Controls.TerraScanDataGridView dataGridViewSorting, bool disableSort)
        {
            // Code commented and set allowsorting property to fix TFS Issue #12886: TSBG - 29551 Sales Tracking - Column Heading Labels shift
            /* for (int columIndex = 0; columIndex <= dataGridViewSorting.Columns.Count - 1; columIndex++)
             {
                 //if (columIndex != 4)
                 //{
                     if (disableSort)
                     {
                         dataGridViewSorting.Columns[columIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                     }
                     else
                     {
                         //if (dataGridViewSorting.Columns[columIndex].Name.Equals("ParcelNumber") || dataGridViewSorting.Columns[columIndex].Name.Equals("OwnerName"))
                         //{
                         //    dataGridViewSorting.Columns[columIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                         //}
                         //else
                         //{
                             dataGridViewSorting.Columns[columIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                         //}
                     }
                 //}
             }*/
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
        /// SetDeletekey
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SetDeletekey(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    this.EditRecord();
                    this.ParcelRecordCount();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion Grid Methods

        #region Context Menu
        /// <summary>
        /// CreateContextMenu
        /// </summary>
        private void CreateContextMenu()
        {
            this.selectedComponentMenuStrip.Items.Add(SharedFunctions.GetResourceString("MenuDelete"));
            this.selectedComponentMenuStrip.Items.Add(SharedFunctions.GetResourceString("MenuCancel"));
        }
        #endregion Context Menu

        #region Validate Color

        /// <summary>
        /// Validate the mentioned combo color is valid RGB format
        /// </summary>
        /// <param name="comboForeColor">Combo list font color</param>
        /// <returns>Valid RGB color</returns>
        private Color LoadBackGroundColor(string comboForeColor)
        {
            string foreColor;
            string[] foreColorArr = null;
            int RColor;
            int GColor;
            int BColor;

            ////assigning backcolor value
            foreColor = comboForeColor;
            if (foreColor != null && !string.IsNullOrEmpty(foreColor))
            {
                char[] splitchar = { ',' };
                foreColorArr = foreColor.Split(splitchar);
                if (foreColorArr.Length.Equals(3))
                {
                    ////Getting Red Color
                    if (string.IsNullOrEmpty(foreColorArr[0]))
                    {
                        RColor = 0;
                    }
                    else
                    {
                        int.TryParse(foreColorArr[0], out RColor);
                    }

                    ////Getting Green Color
                    if (string.IsNullOrEmpty(foreColorArr[1]))
                    {
                        GColor = 0;
                    }
                    else
                    {
                        int.TryParse(foreColorArr[1], out GColor);
                    }

                    ////Getting Blue Color
                    if (string.IsNullOrEmpty(foreColorArr[2]))
                    {
                        BColor = 0;
                    }
                    else
                    {
                        int.TryParse(foreColorArr[2], out BColor);
                    }

                    ////Assign RGB value to form backcolor
                    if (RColor < 0 || RColor > 255 || GColor < 0 || GColor > 255 || BColor < 0 || BColor > 255)
                    {
                        RColor = 0;
                        GColor = 0;
                        BColor = 0;
                    }

                    return Color.FromArgb(RColor, GColor, BColor);

                }
                else
                {
                    return Color.FromArgb(0, 0, 0);
                }
            }

            return Color.FromArgb(0, 0, 0);
        }

        #endregion Validate Color

        #region Save

        private void SaveParcelSaleRecords()
        {
            F29551ParcelSaleTrackingData.SaleDetailsDataTable parcelSaleTable = new F29551ParcelSaleTrackingData.SaleDetailsDataTable();
            F29551ParcelSaleTrackingData.SaleDetailsRow parcelSaleTrackingRow = parcelSaleTable.NewSaleDetailsRow();

            // Sale Header
            if (!string.IsNullOrEmpty(this.SalePriceTextBox.Text.Trim()))
            {
                parcelSaleTrackingRow.OriginalSaleprice = decimal.Parse(this.SalePriceTextBox.Text.Trim());
            }

            if (this.DeedTypeComboBox.SelectedIndex > 0)
            {
                parcelSaleTrackingRow.DeedType = this.DeedTypeComboBox.Text;
            }
            else
            {
                parcelSaleTrackingRow.DeedType = string.Empty;
            }

            parcelSaleTrackingRow.RecordingNumber = this.RecordingNumberTextBox.Text.Trim();
            parcelSaleTrackingRow.ExciseNumber = this.ExciseAffidavitTextBox.Text.Trim();

            if (this.StateQualificationComboBox.SelectedIndex > 0)
            {
                parcelSaleTrackingRow.StateCategoryID = int.Parse(this.StateQualificationComboBox.SelectedValue.ToString());
            }
            else
            {
                parcelSaleTrackingRow.StateCategoryID = 0;
            }

            //if (!string.IsNullOrEmpty(this.SaleDateTextBox.Text.Trim()))
            //{
            parcelSaleTrackingRow.SaleDate = this.SaleDateTextBox.Text.Trim();
            //}

            //if (!string.IsNullOrEmpty(this.FillingDateTextBox.Text.Trim()))
            //{
            parcelSaleTrackingRow.FilingDate = this.FillingDateTextBox.Text.Trim();
            //}

            if (!string.IsNullOrEmpty(this.RecordingFeeTextBox.Text.Trim()))
            {
                parcelSaleTrackingRow.RecordingFee = this.RecordingFeeTextBox.DecimalTextBoxValue;
            }

            parcelSaleTrackingRow.MortgageInstrumentNumber = this.MortgageTextBox.Text.Trim();
            parcelSaleTrackingRow.LoanType = this.LoanTypeTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(this.LoanAmountTextBox.Text.Trim()))
            {
                parcelSaleTrackingRow.LoanAmount = this.LoanAmountTextBox.DecimalTextBoxValue;
            }

            parcelSaleTrackingRow.SaleNote = this.SaleNoteTextBox.Text.Trim();

            if (this.LandOnlySaleComboBox.SelectedIndex > 0)
            {
                if (this.LandOnlySaleComboBox.SelectedValue.ToString().Trim().Equals("1"))
                {
                    parcelSaleTrackingRow.IsLandOnlySale = "true";
                }
                else
                {
                    parcelSaleTrackingRow.IsLandOnlySale = "false";
                }
            }
            else
            {
                parcelSaleTrackingRow.IsLandOnlySale = string.Empty; ;
            }

            // Sale Adjustment
            if (!string.IsNullOrEmpty(this.SalePerSqFtTextBox.Text.Trim()))
            {
                parcelSaleTrackingRow.OriginalSquareFeet = this.SalePerSqFtTextBox.DecimalTextBoxValue;
            }
            else
            {
                parcelSaleTrackingRow.OriginalSquareFeet = 0;
            }

            if (!string.IsNullOrEmpty(this.AdjTimeSaleTextBox.Text.Trim()))
            {
                parcelSaleTrackingRow.AdjTimeSale = int.Parse(this.AdjTimeSaleTextBox.DecimalTextBoxValue.ToString().Trim());
            }

            if (!string.IsNullOrEmpty(this.AdjustedFinanceTextBox.Text.Trim()))
            {
                parcelSaleTrackingRow.AdjFinance = int.Parse(this.AdjustedFinanceTextBox.DecimalTextBoxValue.ToString().Trim());
            }

            if (!string.IsNullOrEmpty(this.AdjPersPropTextBox.Text.Trim()))
            {
                parcelSaleTrackingRow.AdjPersProp = int.Parse(this.AdjPersPropTextBox.DecimalTextBoxValue.ToString().Trim());
            }

            if (!string.IsNullOrEmpty(this.AdjOtherTextBox.Text.Trim()))
            {
                parcelSaleTrackingRow.AdjustOther = int.Parse(this.AdjOtherTextBox.DecimalTextBoxValue.ToString().Trim());
            }

            if (!string.IsNullOrEmpty(this.AdjustedSqFtTextBox.Text.Trim()))
            {
                parcelSaleTrackingRow.AdjustedSquareFeet = this.AdjustedSqFtTextBox.DecimalTextBoxValue;
            }
            else
            {
                parcelSaleTrackingRow.AdjustedSquareFeet = 0;
            }

            // Sale Review
            if (!string.IsNullOrEmpty(this.LastYearPerSqTextBox.Text.Trim()))
            {
                parcelSaleTrackingRow.CurrentPerSqFt = this.LastYearPerSqTextBox.DecimalTextBoxValue;
            }
            else
            {
                parcelSaleTrackingRow.CurrentPerSqFt = 0;
            }

            if (!string.IsNullOrEmpty(this.CurrentPerSqFtTextBox.Text.Trim()))
            {
                parcelSaleTrackingRow.AppraisalPerSqFt = this.CurrentPerSqFtTextBox.DecimalTextBoxValue;
            }
            else
            {
                parcelSaleTrackingRow.AppraisalPerSqFt = 0;
            }

            if (this.AdvisoryComboBox.SelectedIndex >= 0)
            {
                parcelSaleTrackingRow.AdvisoryID = int.Parse(this.AdvisoryComboBox.SelectedValue.ToString());
            }
            else
            {
                parcelSaleTrackingRow.AdvisoryID = 0;
            }

            if (this.AssignmentComboBox.SelectedIndex >= 0)
            {
                parcelSaleTrackingRow.AssignmentID = int.Parse(this.AssignmentComboBox.SelectedValue.ToString());
            }
            else
            {
                parcelSaleTrackingRow.AssignmentID = 0;
            }

            if (this.ReviewStatusComboBox.SelectedIndex >= 0)
            {
                parcelSaleTrackingRow.StatusID = int.Parse(this.ReviewStatusComboBox.SelectedValue.ToString());
            }
            else
            {
                parcelSaleTrackingRow.StatusID = 0;
            }

            if (this.ReviewAppraiserComboBox.SelectedIndex > 0)
            {
                parcelSaleTrackingRow.AppraiserID = int.Parse(this.ReviewAppraiserComboBox.SelectedValue.ToString());
            }
            else
            {
                parcelSaleTrackingRow.AppraiserID = 0;
            }

            //if (!string.IsNullOrEmpty(this.ReviewDateTextBox.Text.Trim()))
            //{
            parcelSaleTrackingRow.ReviewDate = this.ReviewDateTextBox.Text.Trim();
            //}

            if (this.LocalQualificationComboBox.SelectedIndex > 0)
            {
                parcelSaleTrackingRow.LocalQualificationID = byte.Parse(this.LocalQualificationComboBox.SelectedValue.ToString());
            }
            else
            {
                parcelSaleTrackingRow.LocalQualificationID = 0;
            }

            parcelSaleTrackingRow.ReviewNote = this.ReviewNoteTextBox.Text.Trim();

            if (this.saleId != null)
            {
                parcelSaleTrackingRow.SaleID = (int)this.saleId;
            }

            if (this.parcelId != null)
            {
                parcelSaleTrackingRow.ParcelID = (int)this.parcelId;
            }

            parcelSaleTable.Rows.Add(parcelSaleTrackingRow);
            string saleXML = TerraScanCommon.GetXmlString(parcelSaleTable);
            string parcelXML = TerraScanCommon.GetXmlString(this.parcelSaleData.ParcelDetails);
            this.parcelSaleData.ParcelDetails.AcceptChanges();
            /// fixed the Bug id:14029 Save the Buyer field to DB
            this.parcelSaleData.OwnerDetails.AcceptChanges();
            string ownerXML = TerraScanCommon.GetXmlString(this.parcelSaleData.OwnerDetails);

            int returnValue = this.form29551Controller.WorkItem.F29551_SaveParcelSaleDetails(this.keyId, saleXML, parcelXML, ownerXML, TerraScanCommon.UserId);
        }

        #endregion Save

        #region Delete

        /// <summary>
        /// Deletes the parcel details.
        /// </summary>
        private void DeleteParcel(int rowIndex)
        {
            if (this.ParcelDataGridView.CurrentCell != null && this.slicePermissionField.deletePermission
                && this.ParcelDataGridView.OriginalRowCount > 0 && this.ParcelDataGridView.Rows[rowIndex].Selected)
            {
                if (this.ParcelDataGridView.Rows[rowIndex].Cells[this.parcelSaleData.ParcelDetails.IsBaseParcelColumn.ColumnName].Value != null
                    && !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[rowIndex].Cells[this.parcelSaleData.ParcelDetails.IsBaseParcelColumn.ColumnName].Value.ToString().Trim()))
                {
                    int isBaseParcel = 0;
                    int.TryParse(this.ParcelDataGridView.Rows[rowIndex].Cells[this.parcelSaleData.ParcelDetails.IsBaseParcelColumn.ColumnName].Value.ToString(), out isBaseParcel);
                    if (isBaseParcel.Equals(0))
                    {
                        this.GetSelectedParcels();
                        this.parcelSaleData.ParcelDetails.Rows.RemoveAt(rowIndex);
                        this.parcelSaleData.ParcelDetails.AcceptChanges();
                        this.EditRecord();
                        this.ParcelDataGridView.Focus();
                        if (this.parcelTable.Rows.Count > 0)
                        {
                            this.SetSelectedParcels();
                        }
                        if (this.ParcelDataGridView.Rows.Count > 0)
                        {
                            if (rowIndex > 0)
                            {
                                TerraScanCommon.SetDataGridViewPosition(this.ParcelDataGridView, rowIndex - 1);
                                this.ParcelDataGridView.Rows[rowIndex - 1].Selected = true;
                            }
                            else
                            {
                                TerraScanCommon.SetDataGridViewPosition(this.ParcelDataGridView, 0);
                                this.ParcelDataGridView.Rows[0].Selected = true;
                            }
                        }

                        // If the last record got edited add new row at the bottom of the grid
                        if (this.ParcelDataGridView.Rows.Count < this.ParcelDataGridView.NumRowsVisible)
                        {
                            if (this.ParcelDataGridView.OriginalRowCount == 4)
                            {
                                this.parcelSaleData.ParcelDetails.Rows.RemoveAt(this.ParcelDataGridView.OriginalRowCount-1);
                            }
                            // F29551ParcelSaleTrackingData.ParcelDetailsRow newRow = this.parcelSaleData.ParcelDetails.NewParcelDetailsRow();
                            //newRow["EmptyRecord$"] = "True";
                            //this.parcelSaleData.ParcelDetails.Rows.Add(newRow);
                            this.ParcelDataGridView.DataSource = this.parcelSaleData.ParcelDetails.DefaultView;
                        }

                        if (this.parcelTable.Rows.Count > 0)
                        {
                            this.SetSelectedParcels();
                        }

                        if (this.ParcelDataGridView.Rows.Count > this.ParcelDataGridView.NumRowsVisible)
                        {
                            this.ParcelGridVScrollBar.Visible = false;
                        }
                        else
                        {
                            this.ParcelGridVScrollBar.Visible = true;
                        }


                        this.CalculateGridTotalValues();
                        this.ParcelRecordCount();
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
                if (this.OwnerGridView.Rows[rowIndex].Cells[this.parcelSaleData.OwnerDetails.OwnerNameColumn.ColumnName].Value != null
                   && !string.IsNullOrEmpty(this.OwnerGridView.Rows[rowIndex].Cells[this.parcelSaleData.OwnerDetails.OwnerNameColumn.ColumnName].Value.ToString().Trim()))
                {
                    this.parcelSaleData.OwnerDetails.Rows.RemoveAt(rowIndex);
                    this.parcelSaleData.OwnerDetails.AcceptChanges();
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
                        F29551ParcelSaleTrackingData.OwnerDetailsRow newRow = this.parcelSaleData.OwnerDetails.NewOwnerDetailsRow();
                        newRow["EmptyRecord$"] = "True";
                        this.parcelSaleData.OwnerDetails.Rows.Add(newRow);
                        this.OwnerGridView.DataSource = this.parcelSaleData.OwnerDetails.DefaultView;
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

        #region Grid Total Values

        private void CalculateGridTotalValues()
        {
            try
            {
                double acresSumValue = 0;
                double landSumValue = 0;
                double imprSumValue = 0;
                double cropSumValue = 0;
                double totalValue = 0;
                this.parcelSaleData.ParcelDetails.AcceptChanges();
                for (int i = 0; i < this.parcelSaleData.ParcelDetails.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(this.parcelSaleData.ParcelDetails.Rows[i][this.parcelSaleData.ParcelDetails.AcresColumn.ColumnName].ToString()))
                    {
                        double acresValue = 0;
                        double.TryParse(this.parcelSaleData.ParcelDetails.Rows[i][this.parcelSaleData.ParcelDetails.AcresColumn.ColumnName].ToString(), out acresValue);
                        acresSumValue = acresSumValue + acresValue; //Double.Parse(this.parcelSaleData.ParcelDetails.Rows[i][this.parcelSaleData.ParcelDetails.AcresColumn.ColumnName].ToString());
                    }

                    if (!string.IsNullOrEmpty(this.parcelSaleData.ParcelDetails.Rows[i][this.parcelSaleData.ParcelDetails.LandColumn.ColumnName].ToString()))
                    {
                        landSumValue = landSumValue + Double.Parse(this.parcelSaleData.ParcelDetails.Rows[i][this.parcelSaleData.ParcelDetails.LandColumn.ColumnName].ToString());
                    }

                    if (!string.IsNullOrEmpty(this.parcelSaleData.ParcelDetails.Rows[i][this.parcelSaleData.ParcelDetails.ImprColumn.ColumnName].ToString()))
                    {
                        imprSumValue = imprSumValue + Double.Parse(this.parcelSaleData.ParcelDetails.Rows[i][this.parcelSaleData.ParcelDetails.ImprColumn.ColumnName].ToString());
                    }

                    if (!string.IsNullOrEmpty(this.parcelSaleData.ParcelDetails.Rows[i][this.parcelSaleData.ParcelDetails.CropColumn.ColumnName].ToString()))
                    {
                        cropSumValue = cropSumValue + Double.Parse(this.parcelSaleData.ParcelDetails.Rows[i][this.parcelSaleData.ParcelDetails.CropColumn.ColumnName].ToString());
                    }

                    if (!string.IsNullOrEmpty(this.parcelSaleData.ParcelDetails.Rows[i][this.parcelSaleData.ParcelDetails.TotalsColumn.ColumnName].ToString()))
                    {
                        totalValue = totalValue + Double.Parse(this.parcelSaleData.ParcelDetails.Rows[i][this.parcelSaleData.ParcelDetails.TotalsColumn.ColumnName].ToString());
                    }
                }
                //this.parcelSaleData.ParcelDetails.AcceptChanges();
                this.AcresLabel.Text = acresSumValue.ToString("#,##0.00");
                this.LandSumLabel.Text = landSumValue.ToString("#,##0");
                this.CropSumLabel.Text = cropSumValue.ToString("#,##0");
                this.ImprSumLabel.Text = imprSumValue.ToString("#,##0");
                this.TotalSumLabel.Text = totalValue.ToString("#,##0");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Grid Total Values

        #region Label Text

        private void ParcelRecordCount()
        {
            if (this.ParcelDataGridView.OriginalRowCount > 0)
            {
                if (this.ParcelDataGridView.OriginalRowCount >= 5)
                {
                    this.ParcelHeaderLabel.Text = "Included Parcels (" + (this.ParcelDataGridView.OriginalRowCount) + ")";
                }
                else
                {
                    this.ParcelHeaderLabel.Text = "Included Parcels (" + (this.ParcelDataGridView.OriginalRowCount) + ")";
                }
            }
            else
            {
                this.ParcelHeaderLabel.Text = "Included Parcels";
            }
        }

        #endregion Label Text

        #region Validation


        #endregion Validation

        /// <summary>
        /// Handles the TextChanged event of the SubdivisionComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.EditRecord();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        /// <summary>
        /// Handles the Validating event of the SubdivisionComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
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
        #endregion User Defined Methods

        #region Calendar Events

        /// <summary>
        /// Review Date TimePicker CloseUp Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ReviewDatePicker_CloseUp(object sender, System.EventArgs e)
        {
            try
            {
                this.ReviewDateTextBox.Text = this.ReviewDatePicker.Text;
                this.ReviewDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Review Date Button Click Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ReviewDateButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.ParentForm.AcceptButton = null;
                this.ReviewDateButton.SendToBack();
                this.ReviewDatePicker.BringToFront();
                if (!string.IsNullOrEmpty(this.ReviewDateTextBox.Text.Trim()))
                {
                    this.ReviewDatePicker.Value = Convert.ToDateTime(this.ReviewDatePicker.Text);
                }
                else
                {
                    this.ReviewDatePicker.Value = DateTime.Today;
                }

                SendKeys.Send("{F4}");
                this.ReviewDatePicker.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Filling Date Button Click Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FillingDateButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.ParentForm.AcceptButton = null;
                this.FillingDateButton.SendToBack();
                this.FillingDatePicker.BringToFront();
                if (!string.IsNullOrEmpty(this.FillingDateTextBox.Text.Trim()))
                {
                    this.FillingDatePicker.Value = Convert.ToDateTime(this.FillingDatePicker.Text);
                }
                else
                {
                    this.FillingDatePicker.Value = DateTime.Today;
                }

                SendKeys.Send("{F4}");
                this.FillingDatePicker.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Filling Date TimePicker CloseUp Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FillingDatePicker_CloseUp(object sender, System.EventArgs e)
        {
            try
            {
                this.FillingDateTextBox.Text = this.FillingDatePicker.Text;
                this.FillingDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// SaleDate TimePicker CloseUp Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SaleDateTimePicker_CloseUp(object sender, System.EventArgs e)
        {
            try
            {
                this.SaleDateTextBox.Text = this.SaleDateTimePicker.Text;
                this.SaleDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// SaleDate Button Click Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SaleDateCalendar_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.ParentForm.AcceptButton = null;
                this.SaleDateCalendar.SendToBack();
                this.SaleDateTimePicker.BringToFront();
                if (!string.IsNullOrEmpty(this.SaleDateTextBox.Text.Trim()))
                {
                    this.SaleDateTimePicker.Value = Convert.ToDateTime(this.SaleDateTimePicker.Text);
                }
                else
                {
                    // this.SaleDateTimePicker.Value = DateTime.Today;
                }

                SendKeys.Send("{F4}");
                this.SaleDateTimePicker.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// DateTime Picker keyPress Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void DateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
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
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Calendar Events

        #region Combo Events

        public void LocalQualificationComboBox_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            try
            {
                e.DrawBackground();
                if (e.Index < this.parcelSaleData.LocalQualification.Rows.Count && e.Index >= 0)
                {
                    this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.parcelSaleData.LocalQualification.Rows[e.Index][this.parcelSaleData.LocalQualification.LocalQualificationRGBColumn.ColumnName].ToString()));
                    this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                    e.Graphics.DrawString(this.parcelSaleData.LocalQualification.Rows[e.Index][this.parcelSaleData.LocalQualification.LocalQualificationColumn.ColumnName].ToString(), this.comboFont, this.sb, new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        public void ReviewStatusComboBox_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            try
            {
                e.DrawBackground();
                if (e.Index < this.parcelSaleData.Status.Rows.Count && e.Index >= 0)
                {
                    this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.parcelSaleData.Status.Rows[e.Index][this.parcelSaleData.Status.StatusRGBColumn.ColumnName].ToString()));
                    this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                    e.Graphics.DrawString(this.parcelSaleData.Status.Rows[e.Index][this.parcelSaleData.Status.StatusColumn.ColumnName].ToString(), this.comboFont, this.sb, new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        public void AdvisoryComboBox_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            try
            {
                e.DrawBackground();
                if (e.Index < this.parcelSaleData.Advisory.Rows.Count && e.Index >= 0)
                {
                    this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.parcelSaleData.Advisory.Rows[e.Index][this.parcelSaleData.Advisory.AdvisoryRGBColumn.ColumnName].ToString()));
                    this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                    e.Graphics.DrawString(this.parcelSaleData.Advisory.Rows[e.Index][this.parcelSaleData.Advisory.AdvisoryColumn.ColumnName].ToString(), this.comboFont, this.sb, new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Combo Events

        #region Button Events

        /// <summary>
        /// Create Sale Version
        /// </summary>
        /// <param name="sender">The Object which trigger event</param>
        /// <param name="e">The event args</param>
        private void CreateSaleVersionButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool proceedCreation = true;
                for (int i = 0; i < this.ParcelDataGridView.OriginalRowCount; i++)
                {
                    if (this.ParcelDataGridView.Rows[i].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName].Value != null &&
                        (this.ParcelDataGridView.Rows[i].Cells["IsChecked"].Value != null && this.ParcelDataGridView.Rows[i].Cells["IsChecked"].Value.ToString().Equals("True"))
                        && this.ParcelDataGridView.Rows[i].Cells["SaleParcelID"].Value != null && !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[i].Cells["SaleParcelID"].Value.ToString().Trim()))
                    {
                        if (MessageBox.Show("You are about to delete and replace one or more existing Parcel \n Time-Of-Sale Version records. This action cannot be undone, and any \n previously saved changes to the existing records will be lost.\n  Are you sure you want to continue?", SharedFunctions.GetResourceString("F29551CreateSaleHeader"), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            proceedCreation = true;
                        }
                        else
                        {
                            proceedCreation = false;
                        }

                        break;
                    }
                }

                if (proceedCreation)
                {
                    // DataRow timeofRecord = this.parcelSaleData.ParcelDetails.
                    this.GetSelectedParcels();
                    string parcelXML = TerraScanCommon.GetXmlString(this.parcelTable);
                    string returnedMessage = this.form29551Controller.WorkItem.F29551_CreateSaleVersions(this.keyId, TerraScanCommon.UserId, parcelXML);
                    this.isSaleVersionCreated = true;
                    if (!string.IsNullOrEmpty(returnedMessage.Trim()))
                    {
                        MessageBox.Show(returnedMessage.Trim(), "TerraScan – Create Sale Version", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //if (MessageBox.Show(returnedMessage.Trim(), "TerraScan – Create Sale Version", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.Yes)
                        //{
                        //    return;
                        //}
                    }

                    this.ReloadParcelDetails();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void GetSelectedParcels()
        {
            this.parcelTable.Clear();

            for (int i = 0; i < this.ParcelDataGridView.OriginalRowCount; i++)
            {
                if (this.ParcelDataGridView.Rows[i].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName].Value != null &&
                    (this.ParcelDataGridView.Rows[i].Cells["IsChecked"].Value != null && this.ParcelDataGridView.Rows[i].Cells["IsChecked"].Value.ToString().ToLower().Equals("true")))
                {
                    DataRow parcelRow = this.parcelTable.NewRow();
                    parcelRow[0] = this.ParcelDataGridView.Rows[i].Cells["ParcelNumber"].Value.ToString();
                    this.parcelTable.Rows.Add(parcelRow);
                }
            }

            // string parcelXML = TerraScanCommon.GetXmlString(this.parcelTable);
        }

        private void SetSelectedParcels()
        {
            if (this.parcelTable.Rows.Count > 0)
            {
                for (int i = 0; i < this.ParcelDataGridView.OriginalRowCount; i++)
                {
                    if (this.ParcelDataGridView.Rows[i].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName].Value != null
                        && !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[i].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName].Value.ToString().Trim()))
                    {
                        DataRow[] selectedRows = this.parcelTable.Select("ParcelNumber = '" + this.ParcelDataGridView.Rows[i].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName].Value.ToString().Trim() + "'");
                        if (selectedRows.Length > 0)
                        {
                            this.ParcelDataGridView.Rows[i].Cells["IsChecked"].Value = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Transfer Ownership
        /// </summary>
        /// <param name="sender">The Object which trigger event</param>
        /// <param name="e">The event args</param>
        private void TransferOwnershipButton_Click(object sender, EventArgs e)
        {
            try
            {
                string returnedMessage = this.form29551Controller.WorkItem.F29551_TransferOwnership(this.keyId, TerraScanCommon.UserId);

                if (!string.IsNullOrEmpty(returnedMessage.Trim()))
                {
                    if (MessageBox.Show(returnedMessage.Trim(), "TerraScan – Transfer Ownership", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Button Events

        #region Grid Events

        #region Parcel Grid Events
        /// <summary>
        /// Handles the CellContentClick event of the ParcelDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ParcelDataGridView_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (this.ParcelDataGridView.Columns[e.ColumnIndex].Name.Equals(this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName))
                    {
                        ////if (this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("ParcelgridColumn")].Value.ToString() != null || !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("ParcelgridColumn")].Value.ToString()) || this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("ParcelgridColumn")].Value.ToString() != string.Empty)
                        if (this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName].Value.ToString() != null
                            || !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName].Value.ToString()))
                        {
                            if (this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.SaleParcelIDColumn.ColumnName].Value != null
                                && !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.SaleParcelIDColumn.ColumnName].Value.ToString().Trim()))
                            {
                                int parcelHeaderId = 0;
                                int.TryParse(this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelIDColumn.ColumnName].Value.ToString(), out parcelHeaderId);
                                ////this.Cursor = Cursors.WaitCursor;
                                if (parcelHeaderId > 0)
                                {
                                    FormInfo formInfo;
                                    formInfo = TerraScanCommon.GetFormInfo(30000);
                                    formInfo.optionalParameters = new object[1];
                                    formInfo.optionalParameters[0] = parcelHeaderId;
                                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                                }
                            }
                        }
                    }
                    else if (this.ParcelDataGridView.Columns[e.ColumnIndex].Name.Equals("IsChecked"))
                    {
                        //this.ParcelDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        //this.GetSelectedParcels();
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the ParcelDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void ParcelDataGridView_DataBindingComplete(object sender, System.Windows.Forms.DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.ParcelDataGridView.Rows.Count > 0)
                {
                    this.ParcelDataGridView.Rows[0].Selected = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the ParcelDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void ParcelDataGridView_CellFormatting(object sender, System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.ParcelDataGridView.Columns[e.ColumnIndex].Name.Equals(this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName))
                {
                    if (this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName].Value != null
                        && !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName].Value.ToString().Trim()))
                    {
                        if (this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.SaleParcelIDColumn.ColumnName].Value == null
                           || string.IsNullOrEmpty(this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.SaleParcelIDColumn.ColumnName].Value.ToString().Trim()))
                        {
                            if (this.ParcelDataGridView.Rows[e.RowIndex].Selected || this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName].Selected)
                            {
                                (this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName] as TerraScanLinkAndImageCell).LinkColor = System.Drawing.Color.White;
                                (this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName] as TerraScanLinkAndImageCell).VisitedLinkColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                (this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName] as TerraScanLinkAndImageCell).LinkColor = System.Drawing.Color.Black;
                                (this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName] as TerraScanLinkAndImageCell).VisitedLinkColor = System.Drawing.Color.Black;
                            }
                            //(this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName] as TerraScanLinkAndImageCell).VisitedLinkColor = System.Drawing.Color.Black;
                            (this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName] as TerraScanLinkAndImageCell).LinkBehavior = LinkBehavior.NeverUnderline;
                        }
                        else
                        {
                            if (this.ParcelDataGridView.Rows[e.RowIndex].Selected || this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName].Selected)
                            {
                                (this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName] as TerraScanLinkAndImageCell).LinkColor = System.Drawing.Color.White;
                                (this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName] as TerraScanLinkAndImageCell).VisitedLinkColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                (this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName] as TerraScanLinkAndImageCell).LinkColor = System.Drawing.Color.Blue;
                                (this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName] as TerraScanLinkAndImageCell).VisitedLinkColor = System.Drawing.Color.Blue;
                            }

                            (this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName] as TerraScanLinkAndImageCell).LinkBehavior = LinkBehavior.AlwaysUnderline;
                        }
                    }
                }


                Double outDecimalValue;
                if (e.ColumnIndex.Equals(this.ParcelDataGridView.Columns[this.Acres.Name].Index))
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }
                    if (this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName].Value != null
                        && !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName].Value.ToString().Trim()))
                    {

                        if (e.Value != null && !String.IsNullOrEmpty(e.Value.ToString()))
                        {
                            string val = e.Value.ToString();
                            if (Double.TryParse(val, out outDecimalValue))
                            {
                                e.Value = outDecimalValue.ToString("#,##0.00");
                                e.FormattingApplied = true;
                            }
                            else
                            {
                                e.Value = "0.00";
                            }
                        }
                        else
                        {
                            e.Value = "0.00";
                        }
                    }
                }

                Double outValue;
                if (e.ColumnIndex.Equals(this.ParcelDataGridView.Columns[this.Land.Name].Index) || e.ColumnIndex.Equals(this.ParcelDataGridView.Columns[this.Impr.Name].Index)
                    || e.ColumnIndex.Equals(this.ParcelDataGridView.Columns[this.Crop.Name].Index) || e.ColumnIndex.Equals(this.ParcelDataGridView.Columns[this.Totals.Name].Index))
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }
                    if (this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName].Value != null
                        && !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName].Value.ToString().Trim()))
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

                if (e.ColumnIndex.Equals(this.ParcelDataGridView.Columns[this.DOR.Name].Index))
                {
                    if (this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.DORTooltipColumn.ColumnName].Value != null)
                    {
                        this.ParcelDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.DORTooltipColumn.ColumnName].Value.ToString();
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
        private void ParcelDataGridView_RowEnter(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    for (int i = 0; i < this.ParcelDataGridView.Rows.Count; i++)
                    {
                        TerraScanLinkAndImageCell folderCell = (TerraScanLinkAndImageCell)this.ParcelDataGridView[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName, i];
                        folderCell.ImagexLocation = 154;
                        folderCell.ImageyLocation = 1;
                        if (e.RowIndex == i)
                        {
                            folderCell.Image = Properties.Resources.FilePathImage;
                        }
                        else
                        {
                            if (e.RowIndex >= 0)
                            {
                                folderCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.ParcelDataGridView[0, e.RowIndex].InheritedStyle.BackColor.R), Convert.ToInt32(this.ParcelDataGridView[0, e.RowIndex].InheritedStyle.BackColor.G), Convert.ToInt32(this.ParcelDataGridView[0, e.RowIndex].InheritedStyle.BackColor.B));
                            }
                        }

                        this.ParcelDataGridView.Refresh();
                    }

                    if (this.ParcelDataGridView.Columns[e.ColumnIndex].Name.Equals("IsChecked"))
                    {
                        if (this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName].Value == null
                            || string.IsNullOrEmpty(this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName].Value.ToString().Trim()))
                        {
                            this.ParcelDataGridView.Rows[e.RowIndex].Cells["IsChecked"].ReadOnly = true;
                        }
                        else
                        {
                            this.ParcelDataGridView.Rows[e.RowIndex].Cells["IsChecked"].ReadOnly = false;
                        }
                    }


                    // this.currentCoulmnindex = e.ColumnIndex;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        private void AddParcelDetails(int currentRowIndex)
        {
            int checkedIndex = this.ParcelDataGridView.CurrentCell.RowIndex;
            //// this.currentScheduleId = null;
            if (currentRowIndex >= 0)
            {

                // F2550TaxRollCorrectionData.ListParcelDetailsTableDataTable dt = new F2550TaxRollCorrectionData.ListParcelDetailsTableDataTable();

                ListDataTable.Clear();
                //// dt = this.form2550Control.WorkItem.F2550_ListParcelDetails(this.CurrentParcelId, null).ListParcelDetailsTable;
                ///dt = this.form2550Control.WorkItem.F2550_ListParcelDetails(this.parcelIds, null).ListParcelDetailsTable;
                ///used for stateID
                this.ListDataTable = this.form29551Controller.WorkItem.F29551_GetParcelOwnerDetails(null, parcelIds, saleId, TerraScanCommon.UserId).ParcelDetails;

                string strTemp = this.ParcelDataGridView.CurrentCell.FormattedValue.ToString();
                if (!string.IsNullOrEmpty(strTemp))
                {
                    this.ParcelDataGridView.Rows.RemoveAt(checkedIndex);
                    this.parcelSaleData.ParcelDetails.AcceptChanges();

                }

                if (this.ParcelDataGridView.OriginalRowCount < this.ParcelDataGridView.NumRowsVisible)
                {
                    for (int i = this.ParcelDataGridView.RowCount; i >= this.ParcelDataGridView.OriginalRowCount + 1; i--)
                    {
                        this.parcelSaleData.ParcelDetails.Rows.RemoveAt(i - 1);
                    }
                }
                if (ListDataTable.Rows.Count > 0)
                {
                    int maxrowcount = this.ParcelDataGridView.OriginalRowCount;
                    //if (maxrowcount >= 5)
                    //{
                    //    this.parcelSaleData.ParcelDetails.Rows.RemoveAt(maxrowcount - 1);
                    //    //this.parcelSaleData.ParcelDetails.Merge(ListDataTable);
                    //    //DataRow dr = ListDataTable.NewRow();
                    //    //ListDataTable.Rows.Add(dr);
                    //}
                    this.parcelSaleData.ParcelDetails.Merge(ListDataTable);

                    int newRowCount = this.ParcelDataGridView.OriginalRowCount;
                    DataView parcelView = this.parcelSaleData.ParcelDetails.DefaultView;
                    parcelView.RowFilter = "EmptyRecord$='True'";
                    if (parcelView.Count > 0)
                    {
                        for (int i = parcelView.Count - 1; i >= 0; i--)
                        {
                            parcelView.Delete(i);
                        }
                    }
                    parcelView.RowFilter = string.Empty;
                    //if (newRowCount >= 5)
                    //{
                    //    //F29551ParcelSaleTrackingData.ParcelDetailsRow parcelRow = (F29551ParcelSaleTrackingData.ParcelDetailsRow)parcelSaleData.ParcelDetails.Rows[0];
                    //    DataRow dr = parcelSaleData.ParcelDetails.NewRow();
                    //    parcelSaleData.ParcelDetails.Rows.Add(dr);
                    //}
                    this.ParcelDataGridView.DataSource = this.parcelSaleData.ParcelDetails.DefaultView;
                    if (this.ParcelDataGridView.Rows.Count == this.ParcelDataGridView.OriginalRowCount)
                    {
                        F29551ParcelSaleTrackingData.ParcelDetailsRow newRow = this.parcelSaleData.ParcelDetails.NewParcelDetailsRow();
                        newRow["EmptyRecord$"] = "True";
                        this.parcelSaleData.ParcelDetails.Rows.Add(newRow);
                        this.ParcelDataGridView.DataSource = this.parcelSaleData.ParcelDetails.DefaultView;
                        this.parcelSaleData.ParcelDetails.AcceptChanges();
                    }
                    this.ParcelDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    this.CreateSaleVersionButton.Enabled = false;
                    this.EditRecord();
                    this.ParcelDataGridView.Focus();



                }
                else
                {

                    this.ParcelDataGridView.DataSource = this.parcelSaleData.ParcelDetails.DefaultView;

                }


            }
            else
            {
                this.ParcelDataGridView.DataSource = this.parcelSaleData.ParcelDetails.DefaultView;
                
                ////this.ClearParcelDetails();
                ////this.EnableFormControls(false);
            }
            
        }
        /// <summary>
        /// Handles the CellMouseClick event of the ParcelDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void ParcelDataGridView_CellMouseClick(object sender, System.Windows.Forms.DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.ColumnIndex.Equals(3))
                {
                    int previousRow = 0;
                    if (e.RowIndex != 0)
                    {
                        previousRow = e.RowIndex - 1;
                    }

                    bool validRow = true;
                    if (e.RowIndex > 0)
                    {
                        if (string.IsNullOrEmpty(this.ParcelDataGridView.Rows[previousRow].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName].Value.ToString()))
                        {
                            validRow = false;
                        }
                    }

                    if (this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelIDColumn.ColumnName].Value != null
                                 && this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelIDColumn.ColumnName].Value.ToString() == this.parcelId.ToString())
                    {
                        validRow = false;
                    }

                    if (this.ParcelDataGridView.Columns[e.ColumnIndex].Name.Equals(this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName) && (e.RowIndex >= 0) && validRow)
                    {
                        int isBaseParcel = 0;
                        if (this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.IsBaseParcelColumn.ColumnName].Value != null
                         && !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.IsBaseParcelColumn.ColumnName].Value.ToString().Trim()))
                        {
                            int.TryParse(this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.IsBaseParcelColumn.ColumnName].Value.ToString(), out isBaseParcel);
                        }
                        //if (string.IsNullOrEmpty(this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.SaleParcelIDColumn.ColumnName].Value.ToString()))
                        //|| this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.SaleParcelIDColumn.ColumnName].Value.ToString().Equals(SharedFunctions.GetResourceString("Zero")))
                        //&& !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[previousRow].Cells[e.ColumnIndex].Value.ToString()))
                        if (isBaseParcel.Equals(0))
                        {
                            if ((e.X >= 154) && (e.X <= 175) && (e.Y >= 1) && (e.Y <= (22 - 1)))
                            {
                                Form subfundForm = new Form();
                                // added "this.rollYear" parameter for the CR 21613 
                                object[] optionalParameter = new object[] { this.rollYear, this.masterFormNo.ToString() };
                                subfundForm = this.form29551Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1403, optionalParameter, this.form29551Controller.WorkItem);
                                if (subfundForm != null)
                                {
                                    if (subfundForm.ShowDialog() == DialogResult.OK)
                                    {
                                        this.parcelIds = TerraScanCommon.GetValue(subfundForm, "CommandResult");

                                        ////Bining mulitple parcels
                                        DataSet currentparcelDataTable = new DataSet();
                                        currentparcelDataTable.ReadXml(TerraScan.Utilities.SharedFunctions.XmlParser(this.parcelIds));

                                        if (currentparcelDataTable.Tables[0].Rows.Count > 0)
                                        {

                                            DataRow[] listparcelDataRow = currentparcelDataTable.Tables[0].Select();

                                            foreach (DataRow parcel in listparcelDataRow)
                                            {
                                                if (!string.IsNullOrEmpty(parcel.ItemArray[0].ToString()))
                                                {
                                                    DataRow[] parcelId = this.parcelSaleData.ParcelDetails.Select("ParcelID=" + parcel.ItemArray[0].ToString());
                                                    isNewParcel = true;
                                                    if (parcelId.Length > 0)
                                                    {
                                                        if (MessageBox.Show(SharedFunctions.GetResourceString("ExsistParcel"), "Terrascan T2 - Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                                        {
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                            //   this.GetSelectedParcels();
                                            //// this.PopulateParcelDetails(this.ParcelDetailsGridView.CurrentCell.RowIndex);
                                            this.AddParcelDetails(this.ParcelDataGridView.CurrentCell.RowIndex);
                                            CalculateGridTotalValues();
                                            this.ParcelRecordCount();
                                            if (parcelSaleData.OwnerDetails.Rows.Count > 0)
                                            {
                                                newOwnersTable.Clear();
                                                newOwnersTable = this.form29551Controller.WorkItem.F29551_GetParcelOwnerDetails(null, parcelIds, saleId, TerraScanCommon.UserId).OwnerDetails;
                                                DataTable dt = new DataTable();
                                              //  dt = parcelSaleData.OwnerDetails.Copy();
                                                dt.Clear();
                                                if (newOwnersTable.Rows.Count > 0)
                                                {
                                                    this.parcelSaleData.OwnerDetails.Merge((newOwnersTable));
                                                    DataView ownersView = this.parcelSaleData.OwnerDetails.DefaultView;
                                                    ownersView.RowFilter = "EmptyRecord$='True'";
                                                    if (ownersView.Count > 0)
                                                    {
                                                        for (int i = ownersView.Count - 1; i >= 0; i--)
                                                        {
                                                            ownersView.Delete(i);
                                                        }
                                                    }
                                                    ownersView.RowFilter = string.Empty;
                                                   // DataView ownerDataView = this.parcelSaleData.OwnerDetails.DefaultView;
                                                    dt = ownersView.ToTable(true, "OwnerID", "OwnerName", "IsBuyer", "OwnerAddress", "OwnerLocation", "EmptyRecord$");
                                                    this.parcelSaleData.OwnerDetails.Clear();
                                                    this.parcelSaleData.OwnerDetails.Merge(dt);
                                                    this.parcelSaleData.OwnerDetails.AcceptChanges();
                                                    this.OwnerGridView.DataSource = this.parcelSaleData.OwnerDetails.DefaultView;
                                                        //DataRow[] result = dt.Select(OwnerID);
                                                    ////DataRow[] tempArray = dt.Select();
                                                    ////if (tempArray.Length > 0)
                                                    ////{
                                                    ////    //foreach (DataRow drow in tempArray)
                                                    ////    //{
                                                    ////    //    DataRow[] Id = dt.Select("OwnerID=" + drow.ItemArray[0].ToString());
                                                    ////    //    if (Id.Length > 0)
                                                    ////    //    {
                                                    ////    //        dt.Rows.Remove(drow);
                                                                
                                                    ////    //    }
                                                          
                                                    ////  //  }
                                                    ////}
                                                    if (this.OwnerGridView.Rows.Count == this.OwnerGridView.OriginalRowCount)
                                                    {
                                                        F29551ParcelSaleTrackingData.OwnerDetailsRow ownerRow = this.parcelSaleData.OwnerDetails.NewOwnerDetailsRow();
                                                        ownerRow["EmptyRecord$"] = "True";
                                                        this.parcelSaleData.OwnerDetails.Rows.Add(ownerRow);
                                                        this.OwnerGridView.DataSource = this.parcelSaleData.OwnerDetails.DefaultView;
                                                    }
                                                }
                                            }
                                                   // foreach(DataRow dr in dt.Rows)
                                                   // {
                                                   //     string id = dr["OwnerID"].ToString();
                                                        
                                                   //     DataTable dttemp=new DataTable();
                                                   //   //  dt=parcelSaleData.OwnerDetails.Select("dt");
                                                   //    dttemp = ownersView.ToTable(false,"ownerID", "OwnerName", "IsBuyer", "OwnerAddress", "OwnerLocation", "EmptyRecord$");
                                                   // }
                                                   //// dt.Select("Distinct",false,"
                                                   // //, "OwnerName", "IsBuyer", "OwnerAddress", "OwnerLocation","EmptyRecord$");
                                                   // for (int i = 0; i < parcelSaleData.OwnerDetails.Rows.Count; i++)
                                                   // {
                                                   //     F29551ParcelSaleTrackingData.OwnerDetailsRow newRow = (F29551ParcelSaleTrackingData.OwnerDetailsRow)parcelSaleData.OwnerDetails.Copy().Rows[i];
                                                   //     int ownerId = 0;
                                                   //     int isBuyer = 0;
                                                   //     if (!newRow.IsIsBuyerNull() && !string.IsNullOrEmpty(newRow.IsBuyer.ToString()))
                                                   //     {
                                                   //         isBuyer = newRow.IsBuyer;
                                                   //     }

                                                   //     if (!newRow.IsOwnerIDNull())
                                                   //     {
                                                   //         ownerId = newRow.OwnerID;
                                                   //     }

                                                   //     DataView ownerDataView = null;
                                                   //     if (this.parcelSaleData.OwnerDetails.Rows.Count > 0)
                                                   //     {
                                                   //         ownerDataView = new DataView(this.parcelSaleData.OwnerDetails);
                                                   //     }

                                                   //     ownerDataView.RowFilter = this.parcelSaleData.OwnerDetails.OwnerIDColumn.ColumnName + " = " + ownerId + " AND "
                                                   //                               + this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName + " = " + isBuyer;

                                                   //     if (ownerDataView.Count <= 0)
                                                   //     {
                                                   //        // dt.Clear();
                                                   //        // dt.ImportRow(newRow);
                                                   //         //this.parcelSaleData.OwnerDetails.AcceptChanges();
                                                   //         //this.OwnerGridView.DataSource = this.parcelSaleData.OwnerDetails.DefaultView;
                                                   //     }
                                                   //     else
                                                   //     {
                                                   //         dt.Clear();
                                                   //        // dt = parcelSaleData.OwnerDetails;

                                                   //       //  dt = ownerDataView.ToTable();
                                                   //       //  dt.Select("Distinct",ownerDataView.RowFilter);
                                                   //         ////parcelSaleData.OwnerDetails.RemoveOwnerDetailsRow(newRow);
                                                   //         ////DataTable dtt = new DataTable();
                                                   //         ////dtt.ImportRow(newRow);

                                                   //     }
                                                   //     //dt=   ownerDataView.ToTable("DistinctTable", true, "OwnerID","IsBuyer");
                                                   //     //DataTable dt2 = new DataTable();
                                                   //     //dt2.Rows.Add(newRow);

                                                   // }
                                                    

                                                   
                                                    //this.parcelSaleData.OwnerDetails.Merge((newOwnersTable));
                                             //   }
                                         //   }
                                            //for (int i = 0; i < parcelSaleData.OwnerDetails.Rows.Count; i++)
                                            //{
                                            //    F29551ParcelSaleTrackingData.OwnerDetailsRow newRow = (F29551ParcelSaleTrackingData.OwnerDetailsRow)parcelSaleData.OwnerDetails.Copy().Rows[i];
                                            //    int ownerId = 0;
                                            //    int isBuyer = 0;
                                            //    if (!newRow.IsIsBuyerNull() && !string.IsNullOrEmpty(newRow.IsBuyer.ToString()))
                                            //    {
                                            //        isBuyer = newRow.IsBuyer;
                                            //    }

                                            //    if (!newRow.IsOwnerIDNull())
                                            //    {
                                            //        ownerId = newRow.OwnerID;
                                            //    }

                                            //    DataView ownerDataView = null;
                                            //    if (this.parcelSaleData.OwnerDetails.Rows.Count > 0)
                                            //    {
                                            //        ownerDataView = new DataView(this.parcelSaleData.OwnerDetails);
                                            //    }

                                            //    ownerDataView.RowFilter = this.parcelSaleData.OwnerDetails.OwnerIDColumn.ColumnName + " = " + ownerId + " AND "
                                            //                              + this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName + " = " + isBuyer;

                                            //    if (ownerDataView.Count <= 0)
                                            //    {
                                            //        try
                                            //        {
                                            //            newOwnersTable.ImportRow(newRow);
                                            //        }
                                            //        catch (Exception ex)
                                            //        {
                                            //        }
                                            //        newOwnersTable.Rows.Add(newRow);
                                            //    }
                                            //    else
                                            //    {
                                            //        newOwnersTable.RemoveOwnerDetailsRow(newRow);
                                            //    }
                                            //}
                                            //this.parcelSaleData.OwnerDetails.Merge((newOwnersTable));
                                            //if (newOwnersTable.Rows.Count > 0)
                                            //{
                                            //    DataView ownersView = this.parcelSaleData.OwnerDetails.DefaultView;
                                            //    ownersView.RowFilter = "EmptyRecord$='True'";

                                            //    if (ownersView.Count > 0)
                                            //    {
                                            //        for (int i = ownersView.Count - 1; i >= 0; i--)
                                            //        {
                                            //            ownersView.Delete(i);
                                            //        }
                                            //    }
                                            //    ownersView.RowFilter = string.Empty;
                                            //    this.parcelSaleData.OwnerDetails.Merge((newOwnersTable));
                                            //}
                                             // this.parcelSaleData.OwnerDetails.Merge(parcelSaleData.OwnerDetails);
                                           // this.parcelSaleData.OwnerDetails.DefaultView.RowFilter = string.Empty;
                                           // this.parcelSaleData.OwnerDetails.AcceptChanges();
                                            //this.OwnerGridView.DataSource = this.parcelSaleData.OwnerDetails.DefaultView;

                                            //if (this.OwnerGridView.Rows.Count == this.OwnerGridView.OriginalRowCount)
                                            //{
                                            //    F29551ParcelSaleTrackingData.OwnerDetailsRow ownerRow = this.parcelSaleData.OwnerDetails.NewOwnerDetailsRow();
                                            //    ownerRow["EmptyRecord$"] = "True";
                                            //    this.parcelSaleData.OwnerDetails.Rows.Add(ownerRow);
                                            //    this.OwnerGridView.DataSource = this.parcelSaleData.OwnerDetails.DefaultView;
                                            //}




                                            // this.GetSelectedParcels();

                                            if (this.parcelSaleData.ParcelDetails.DefaultView == null || currentparcelDataTable.Tables[0].Rows.Count <= 0)
                                            {

                                                //if (this.ParcelDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null
                                                //&& !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim()))
                                                //{
                                                this.EditRecord();

                                                F29551ParcelSaleTrackingData.ParcelDetailsRow parcelRow = (F29551ParcelSaleTrackingData.ParcelDetailsRow)parcelSaleData.ParcelDetails.Rows[0];

                                                if (isSaleVersionCreated)
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells["IsChecked"].Value = true;
                                                }


                                                if (!parcelRow.IsSaleParcelIDNull())
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.SaleParcelIDColumn.ColumnName].Value = parcelRow.SaleParcelID;
                                                }
                                                else
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.SaleParcelIDColumn.ColumnName].Value = string.Empty;
                                                }

                                                if (!parcelRow.IsParcelIDNull())
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelIDColumn.ColumnName].Value = parcelRow.ParcelID;
                                                }
                                                else
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelIDColumn.ColumnName].Value = string.Empty;
                                                }

                                                if (!parcelRow.IsParcelNumberNull())
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName].Value = parcelRow.ParcelNumber.Trim();
                                                }
                                                else
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelNumberColumn.ColumnName].Value = string.Empty;
                                                }

                                                if (!parcelRow.IsAcresNull())
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.AcresColumn.ColumnName].Value = parcelRow.Acres.Trim();
                                                }
                                                else
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.AcresColumn.ColumnName].Value = string.Empty;
                                                }

                                                if (!parcelRow.IsIsBaseParcelNull())
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.IsBaseParcelColumn.ColumnName].Value = parcelRow.IsBaseParcel;
                                                }
                                                else
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.IsBaseParcelColumn.ColumnName].Value = 0;
                                                }

                                                if (!parcelRow.IsLandNull())
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.LandColumn.ColumnName].Value = parcelRow.Land;
                                                }
                                                else
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.LandColumn.ColumnName].Value = null;
                                                }

                                                if (!parcelRow.IsImprNull())
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ImprColumn.ColumnName].Value = parcelRow.Impr;

                                                }
                                                else
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.ImprColumn.ColumnName].Value = null;
                                                }

                                                if (!parcelRow.IsCropNull())
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.CropColumn.ColumnName].Value = parcelRow.Crop;
                                                }
                                                else
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.CropColumn.ColumnName].Value = null;
                                                }

                                                if (!parcelRow.IsTotalsNull())
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.TotalsColumn.ColumnName].Value = parcelRow.Totals;
                                                }
                                                else
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.TotalsColumn.ColumnName].Value = null;
                                                }

                                                if (!parcelRow.IsDORNull())
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.DORColumn.ColumnName].Value = parcelRow.DOR;
                                                }
                                                else
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.DORColumn.ColumnName].Value = string.Empty;
                                                }

                                                if (!parcelRow.IsDORTooltipNull())
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.DORTooltipColumn.ColumnName].Value = parcelRow.DORTooltip;
                                                }
                                                else
                                                {
                                                    this.ParcelDataGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.ParcelDetails.DORTooltipColumn.ColumnName].Value = string.Empty;
                                                }

                                                // If the last record got edited add new row at the bottom of the grid
                                                if (this.ParcelDataGridView.Rows.Count.Equals(e.RowIndex + 1))
                                                {
                                                    this.ParcelDataGridView.DataSource = this.parcelSaleData.ParcelDetails.DefaultView;
                                                    F29551ParcelSaleTrackingData.ParcelDetailsRow newRow = this.parcelSaleData.ParcelDetails.NewParcelDetailsRow();
                                                    newRow["EmptyRecord$"] = "True";
                                                    this.parcelSaleData.ParcelDetails.Rows.Add(newRow);
                                                    this.ParcelDataGridView.DataSource = this.parcelSaleData.ParcelDetails.DefaultView;

                                                    if (this.isSaleVersionCreated && this.parcelTable.Rows.Count > 0)
                                                    {
                                                        this.SetSelectedParcels();
                                                    }

                                                    if (isSaleVersionCreated)
                                                    {
                                                        this.ParcelDataGridView.Rows[e.RowIndex].Cells["IsChecked"].Value = true;
                                                        this.ParcelDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                                                        this.ParcelDataGridView.RefreshEdit();
                                                    }

                                                }

                                                this.CalculateGridTotalValues();
                                                this.ParcelRecordCount();
                                                if (parcelDataSet.OwnerDetails.Rows.Count > 0)
                                                {
                                                   // F29551ParcelSaleTrackingData.OwnerDetailsDataTable newOwnersTable = new F29551ParcelSaleTrackingData.OwnerDetailsDataTable();
                                                    for (int i = 0; i < parcelDataSet.OwnerDetails.Rows.Count; i++)
                                                    {
                                                        F29551ParcelSaleTrackingData.OwnerDetailsRow newRow = (F29551ParcelSaleTrackingData.OwnerDetailsRow)parcelDataSet.OwnerDetails.Copy().Rows[i];
                                                        int ownerId = 0;
                                                        int isBuyer = 0;
                                                        if (!newRow.IsIsBuyerNull() && !string.IsNullOrEmpty(newRow.IsBuyer.ToString()))
                                                        {
                                                            isBuyer = newRow.IsBuyer;
                                                        }

                                                        if (!newRow.IsOwnerIDNull())
                                                        {
                                                            ownerId = newRow.OwnerID;
                                                        }

                                                        DataView ownerDataView = null;
                                                        if (this.parcelSaleData.OwnerDetails.Rows.Count > 0)
                                                        {
                                                            ownerDataView = new DataView(this.parcelSaleData.OwnerDetails);
                                                        }

                                                        ownerDataView.RowFilter = this.parcelSaleData.OwnerDetails.OwnerIDColumn.ColumnName + " = " + ownerId + " AND "
                                                                                  + this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName + " = " + isBuyer;

                                                        if (ownerDataView.Count <= 0)
                                                        {
                                                            try
                                                            {
                                                                newOwnersTable.ImportRow(newRow);
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                            }
                                                            // newOwnersTable.Rows.Add(newRow);
                                                        }
                                                    }

                                                    if (newOwnersTable.Rows.Count > 0)
                                                    {
                                                        DataView ownersView = this.parcelSaleData.OwnerDetails.DefaultView;
                                                        ownersView.RowFilter = "EmptyRecord$='True'";

                                                        if (ownersView.Count > 0)
                                                        {
                                                            for (int i = ownersView.Count - 1; i >= 0; i--)
                                                            {
                                                                ownersView.Delete(i);
                                                            }
                                                        }
                                                        ownersView.RowFilter = string.Empty;
                                                        this.parcelSaleData.OwnerDetails.Merge((newOwnersTable));
                                                    }
                                                }

                                                this.parcelSaleData.OwnerDetails.Merge(parcelSaleData.OwnerDetails);
                                                this.parcelSaleData.OwnerDetails.DefaultView.RowFilter = string.Empty;
                                                this.parcelSaleData.OwnerDetails.AcceptChanges();
                                                this.OwnerGridView.DataSource = this.parcelSaleData.OwnerDetails.DefaultView;

                                                if (this.OwnerGridView.Rows.Count == this.OwnerGridView.OriginalRowCount)
                                                {
                                                    F29551ParcelSaleTrackingData.OwnerDetailsRow ownerRow = this.parcelSaleData.OwnerDetails.NewOwnerDetailsRow();
                                                    ownerRow["EmptyRecord$"] = "True";
                                                    this.parcelSaleData.OwnerDetails.Rows.Add(ownerRow);
                                                    this.OwnerGridView.DataSource = this.parcelSaleData.OwnerDetails.DefaultView;
                                                }

                                                //}
                                                //else
                                                //{
                                                //    DataView parcelsView = this.parcelSaleData.ParcelDetails.DefaultView;
                                                //    parcelsView.RowFilter = "EmptyRecord$='False'";
                                                //    this.parcelSaleData.ParcelDetails.Merge(parcelDataSet.ParcelDetails);
                                                //    parcelsView.RowFilter = string.Empty;
                                                //}
                                                //this.ParcelDataGridView.DataSource = this.parcelSaleData.ParcelDetails.DefaultView;

                                                if (parcelSaleData.OwnerDetails.Rows.Count > 0)
                                                {
                                                   // F29551ParcelSaleTrackingData.OwnerDetailsDataTable newOwnersTable = new F29551ParcelSaleTrackingData.OwnerDetailsDataTable();
                                                    for (int i = 0; i < parcelSaleData.OwnerDetails.Rows.Count; i++)
                                                    {
                                                        F29551ParcelSaleTrackingData.OwnerDetailsRow newRow = (F29551ParcelSaleTrackingData.OwnerDetailsRow)parcelSaleData.OwnerDetails.Copy().Rows[i];
                                                        int ownerId = 0;
                                                        int isBuyer = 0;
                                                        if (!newRow.IsIsBuyerNull() && !string.IsNullOrEmpty(newRow.IsBuyer.ToString()))
                                                        {
                                                            isBuyer = newRow.IsBuyer;
                                                        }

                                                        if (!newRow.IsOwnerIDNull())
                                                        {
                                                            ownerId = newRow.OwnerID;
                                                        }

                                                        DataView ownerDataView = null;
                                                        if (this.parcelSaleData.OwnerDetails.Rows.Count > 0)
                                                        {
                                                            ownerDataView = new DataView(this.parcelSaleData.OwnerDetails);
                                                        }

                                                        ownerDataView.RowFilter = this.parcelSaleData.OwnerDetails.OwnerIDColumn.ColumnName + " = " + ownerId + " AND "
                                                                                  + this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName + " = " + isBuyer;

                                                        if (ownerDataView.Count <= 0)
                                                        {
                                                            try
                                                            {
                                                                newOwnersTable.ImportRow(newRow);
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                            }
                                                            // newOwnersTable.Rows.Add(newRow);
                                                        }
                                                    }

                                                    if (newOwnersTable.Rows.Count > 0)
                                                    {
                                                        DataView ownersView = this.parcelSaleData.OwnerDetails.DefaultView;
                                                        ownersView.RowFilter = "EmptyRecord$='True'";

                                                        if (ownersView.Count > 0)
                                                        {
                                                            for (int i = ownersView.Count - 1; i >= 0; i--)
                                                            {
                                                                ownersView.Delete(i);
                                                            }
                                                        }
                                                        ownersView.RowFilter = string.Empty;
                                                        this.parcelSaleData.OwnerDetails.Merge((newOwnersTable));
                                                    }
                                                }

                                                this.parcelSaleData.OwnerDetails.Merge(parcelSaleData.OwnerDetails);
                                                this.parcelSaleData.OwnerDetails.DefaultView.RowFilter = string.Empty;
                                                this.parcelSaleData.OwnerDetails.AcceptChanges();
                                                this.OwnerGridView.DataSource = this.parcelSaleData.OwnerDetails.DefaultView;

                                                if (this.OwnerGridView.Rows.Count == this.OwnerGridView.OriginalRowCount)
                                                {
                                                    F29551ParcelSaleTrackingData.OwnerDetailsRow ownerRow = this.parcelSaleData.OwnerDetails.NewOwnerDetailsRow();
                                                    ownerRow["EmptyRecord$"] = "True";
                                                    this.parcelSaleData.OwnerDetails.Rows.Add(ownerRow);
                                                    this.OwnerGridView.DataSource = this.parcelSaleData.OwnerDetails.DefaultView;
                                                }
                                                //this.OwnerGridView.DataSource = this.parcelSaleData.OwnerDetails.DefaultView;
                                            }
                                            else
                                            {
                                                //this.ParcelDataGridView.DataSource = this.parcelSaleData.ParcelDetails.DefaultView;

                                                //if (this.isSaleVersionCreated && this.parcelTable.Rows.Count > 0)
                                                //{
                                                //    this.SetSelectedParcels();
                                                //}

                                                this.OwnerGridView.DataSource = this.parcelSaleData.OwnerDetails.DefaultView;
                                                //  this.ParcelGridVScrollBar.Enabled = true;
                                                //if (MessageBox.Show(SharedFunctions.GetResourceString("ExsistParcel"), "Terrascan - Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                                //{
                                                //    return;
                                                //}
                                            }

                                        }
                                        else
                                        {
                                            isNewParcel = false;
                                            this.AddParcelDetails(this.ParcelDataGridView.CurrentCell.RowIndex);
                                            CalculateGridTotalValues();
                                            //this.ParcelDataGridView.DataSource = this.parcelSaleData.ParcelDetails.DefaultView;
                                            //if (this.isSaleVersionCreated && this.parcelTable.Rows.Count > 0)
                                            //{
                                            //    this.SetSelectedParcels();
                                            //}
                                        }
                                        this.OwnerGridView.DataSource = this.parcelSaleData.OwnerDetails.DefaultView;

                                        if (this.ParcelDataGridView.Rows.Count > this.ParcelDataGridView.NumRowsVisible)
                                        {
                                            this.ParcelGridVScrollBar.Visible = false;
                                        }
                                        else
                                        {
                                            this.ParcelGridVScrollBar.Visible = true;
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
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion Parcel Grid Events

        #region Owner Grid Events

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
                    bool validRow = true;
                    if (e.RowIndex > 0)
                    {
                        if (this.OwnerGridView.Rows[e.RowIndex - 1].Cells[this.parcelSaleData.OwnerDetails.OwnerNameColumn.ColumnName].Value == null
                            || string.IsNullOrEmpty(this.OwnerGridView.Rows[e.RowIndex - 1].Cells[this.parcelSaleData.OwnerDetails.OwnerNameColumn.ColumnName].Value.ToString()))
                        {
                            validRow = false;
                        }
                    }
                    if (this.OwnerGridView.Columns[e.ColumnIndex].Name.Equals(this.parcelSaleData.OwnerDetails.OwnerNameColumn.ColumnName) && (e.RowIndex >= 0) && validRow)
                    {
                        if ((e.X >= 153) && (e.X <= 171) && (e.Y >= 1) && (e.Y <= (22 - 1)))
                        {
                            Form parcelF9101 = new Form();
                            parcelF9101 = this.form29551Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9101, null, this.form29551Controller.WorkItem);

                            if (parcelF9101 != null)
                            {
                                if (parcelF9101.ShowDialog() == DialogResult.Yes)
                                {
                                    int ownerId;
                                    int.TryParse(TerraScanCommon.GetValue(parcelF9101, "MasterNameOwnerId"), out ownerId);
                                    if (ownerId > 0)
                                    {
                                        F29551ParcelSaleTrackingData.OwnerDetailsDataTable ownerTable = new F29551ParcelSaleTrackingData.OwnerDetailsDataTable();
                                        DataTable dtTemp = new DataTable();
                                        dtTemp = parcelSaleData.OwnerDetails.Copy();
                                        ownerTable = this.form29551Controller.WorkItem.F29551_GetOwnerDetails(null, ownerId, null, TerraScanCommon.UserId).OwnerDetails;
                                        if (ownerTable.Rows.Count > 0)
                                        {
                                            F29551ParcelSaleTrackingData.OwnerDetailsRow newRow = (F29551ParcelSaleTrackingData.OwnerDetailsRow)ownerTable.Rows[0];
                                            int isBuyer = 0;
                                            if (!newRow.IsIsBuyerNull() && !string.IsNullOrEmpty(newRow.IsBuyer.ToString()))
                                            {
                                                isBuyer = newRow.IsBuyer;
                                            }

                                            DataView ownerDataView = null;
                                            if (ownerTable.Rows.Count > 0)
                                            {
                                                DataView ownersView = this.parcelSaleData.OwnerDetails.DefaultView;
                                                ownersView.RowFilter = "EmptyRecord$='True'";

                                                if (ownersView.Count > 0)
                                                {
                                                    for (int i = ownersView.Count - 1; i >= 0; i--)
                                                    {
                                                        ownersView.Delete(i);
                                                    }
                                                }
                                                ownersView.RowFilter = string.Empty;
                                              //  this.parcelSaleData.OwnerDetails.Merge((ownerTable));
                                            }
                                            if (this.parcelSaleData.OwnerDetails.Rows.Count > 0)
                                            {
                                                ownerDataView = new DataView(this.parcelSaleData.OwnerDetails);
                                            }

                                            ownerDataView.RowFilter = this.parcelSaleData.OwnerDetails.OwnerIDColumn.ColumnName + " = " + ownerId + " AND "
                                                                         + this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName + " = " + isBuyer; ;
                                           
                                            if (ownerDataView.Count <= 0)
                                            {
                                                this.EditRecord();
                                                this.parcelSaleData.OwnerDetails.Merge((ownerTable));
                                                //this.parcelSaleData.OwnerDetails.Clear();
                                                //if (this.OwnerGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null
                                                //    && !string.IsNullOrEmpty(this.OwnerGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim()))
                                                //{
                                                //this.OwnerGridView.Rows[e.RowIndex].Cells[ownerTable.OwnerIDColumn.ColumnName].Value = newRow.OwnerID;

                                                //if (!newRow.IsOwnerNameNull())
                                                //{
                                                //    this.OwnerGridView.Rows[e.RowIndex].Cells[ownerTable.OwnerNameColumn.ColumnName].Value = newRow.OwnerName;
                                                //}
                                                //else
                                                //{
                                                //    this.OwnerGridView.Rows[e.RowIndex].Cells[ownerTable.OwnerNameColumn.ColumnName].Value = string.Empty;
                                                //}

                                                //if (!newRow.IsOwnerAddressNull())
                                                //{
                                                //    this.OwnerGridView.Rows[e.RowIndex].Cells[ownerTable.OwnerAddressColumn.ColumnName].Value = newRow.OwnerAddress;
                                                //}
                                                //else
                                                //{
                                                //    this.OwnerGridView.Rows[e.RowIndex].Cells[ownerTable.OwnerAddressColumn.ColumnName].Value = string.Empty;
                                                //}

                                                //if (!newRow.IsOwnerLocationNull())
                                                //{
                                                //    this.OwnerGridView.Rows[e.RowIndex].Cells[ownerTable.OwnerLocationColumn.ColumnName].Value = newRow.OwnerLocation;
                                                //}
                                                //else
                                                //{
                                                //    this.OwnerGridView.Rows[e.RowIndex].Cells[ownerTable.OwnerLocationColumn.ColumnName].Value = string.Empty;
                                                //}

                                                //if (!newRow.IsIsBuyerNull())
                                                //{
                                                //    this.OwnerGridView.Rows[e.RowIndex].Cells[ownerTable.IsBuyerColumn.ColumnName].Value = newRow.IsBuyer;
                                                //}
                                                //else
                                                //{
                                                //    this.OwnerGridView.Rows[e.RowIndex].Cells[ownerTable.IsBuyerColumn.ColumnName].Value = 1;
                                                //}
                                                
                                                this.parcelSaleData.OwnerDetails.AcceptChanges();
                                                this.OwnerGridView.DataSource = this.parcelSaleData.OwnerDetails.DefaultView;
                                                if (this.OwnerGridView.Rows.Count.Equals(e.RowIndex + 1))
                                                {
                                                    F29551ParcelSaleTrackingData.OwnerDetailsRow ownerRow = this.parcelSaleData.OwnerDetails.NewOwnerDetailsRow();
                                                    ownerRow["EmptyRecord$"] = "True";
                                                    this.parcelSaleData.OwnerDetails.Rows.Add(ownerRow);
                                                    this.OwnerGridView.DataSource = this.parcelSaleData.OwnerDetails.DefaultView;
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
                                                if (MessageBox.Show(SharedFunctions.GetResourceString("ExsistOwner"), "Terrascan T2 - Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
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
                        //}
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
        private void OwnerGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    //this.OwnerGridView.Rows[e.RowIndex].ReadOnly = true;
                    for (int i = 0; i < this.OwnerGridView.Rows.Count; i++)
                    {
                        TerraScanTextAndImageCell ownerCell = (TerraScanTextAndImageCell)this.OwnerGridView[this.parcelSaleData.OwnerDetails.OwnerNameColumn.ColumnName, i];
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
        /// Handles the CellFormatting event of the OwnerGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void OwnerGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.OwnerGridView.Columns[e.ColumnIndex].Name.Equals(this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName))
            {
                if (this.OwnerGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.OwnerDetails.OwnerNameColumn.ColumnName].Value == null
                    || string.IsNullOrEmpty(this.OwnerGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.OwnerDetails.OwnerNameColumn.ColumnName].Value.ToString().Trim()))
                {
                    this.OwnerGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName].ReadOnly = true;
                }
                else
                {
                    this.OwnerGridView.Rows[e.RowIndex].Cells[this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName].ReadOnly = false;
                }
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

        #endregion Owner Grid Events

        /// <summary>
        /// Handles the KeyDown event of the ParcelDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ParcelDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.ParcelDataGridView.CurrentRowIndex >= 0 && e.KeyCode.Equals(Keys.Delete))
                {
                    if (this.ParcelDataGridView.Rows[this.ParcelDataGridView.CurrentRowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelIDColumn.ColumnName].Value != null
                                && this.ParcelDataGridView.Rows[this.ParcelDataGridView.CurrentRowIndex].Cells[this.parcelSaleData.ParcelDetails.ParcelIDColumn.ColumnName].Value.ToString() != this.parcelId.ToString())
                    {

                        this.DeleteParcel(this.ParcelDataGridView.CurrentRowIndex);
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
        #endregion Grid Events

        #region PictureBox Events
        /// <summary>
        /// Handles the Click event of the SaleParcelPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SaleParcelPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D24550.F29551"));
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
        private void SaleParcelPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.SaleToolTip.SetToolTip(this.SaleParcelPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion PictureBox Events

        /// <summary>
        /// Handles the EditingControlShowing event of the OwnerGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
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
                //int.TryParse(((DataGridViewComboBoxEditingControl)sender).Text Value.ToString().Trim(), out isBuyer);

                if (((System.Windows.Forms.DataGridViewComboBoxEditingControl)sender).Text.Trim().Equals("Grantor"))
                {
                    isBuyer = 0;
                }
                else
                {
                    isBuyer = 1;
                }

                int ownerId = 0;
                if (this.OwnerGridView.Rows[this.OwnerGridView.CurrentRowIndex].Cells[this.parcelSaleData.OwnerDetails.OwnerIDColumn.ColumnName].Value != null)
                {
                    int.TryParse(this.OwnerGridView.Rows[this.OwnerGridView.CurrentRowIndex].Cells[this.parcelSaleData.OwnerDetails.OwnerIDColumn.ColumnName].Value.ToString().Trim(), out ownerId);
                }

                if (ownerId > 0)
                {
                    DataView ownerDataView = null;
                    if (this.parcelSaleData.OwnerDetails.Rows.Count > 0)
                    {
                        ownerDataView = new DataView(this.parcelSaleData.OwnerDetails);
                    }

                    ownerDataView.RowFilter = this.parcelSaleData.OwnerDetails.OwnerIDColumn.ColumnName + " = " + ownerId + " AND "
                                                 + this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName + " = " + isBuyer; ;

                    if (ownerDataView.Count > 0)
                    {
                        if (isBuyer.Equals(0))
                        {
                            this.OwnerGridView.Rows[this.OwnerGridView.CurrentRowIndex].Cells[this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName].Value = 1;
                        }
                        else
                        {
                            this.OwnerGridView.Rows[this.OwnerGridView.CurrentRowIndex].Cells[this.parcelSaleData.OwnerDetails.IsBuyerColumn.ColumnName].Value = 0;
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
        /// Handles the Resize event of the F29551 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F29551_Resize(object sender, EventArgs e)
        {
            // Set height on form resize event to avoid form partial blank on load
            this.Height = this.SaleParcelPictureBox.Height + 22;
        }

        /// <summary>
        /// Handles the Click event of the RefreshValuesButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RefreshValuesButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Sp call to recalculate values
                string returnedMessage = this.form29551Controller.WorkItem.F29551_UpdateSaleParcel(this.keyId, TerraScanCommon.UserId);

                if (returnedMessage != null && !string.IsNullOrEmpty(returnedMessage.Trim()))
                {
                    MessageBox.Show(returnedMessage, SharedFunctions.GetResourceString("F29551SaleTrackingHeader"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.ReloadParcelDetails();

                this.RefreshValuesButton.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Reloads the parcel details.
        /// </summary>
        private void ReloadParcelDetails()
        {
            // Reload page
            // Get value to bind Gridview and other controls
            this.GetParcelSaleDetails();

            // Bind values in appropriate controls
            this.BindValues();

            this.CalculateGridTotalValues();

            if (this.isSaleVersionCreated && this.parcelTable.Rows.Count > 0)
            {
                this.SetSelectedParcels();
            }

            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.SalePriceTextBox.Focus();
            this.DisableGridSorting(ParcelDataGridView, false);
            this.DisableGridSorting(OwnerGridView, false);
        }

        //Added to implement TFS#CO 21613 to return Event based roll year
        /// <summary>
        /// Loads the sale tracking roll year.
        /// </summary>
        private void LoadSaleTrackingRollYear()
        {
            this.SaleTrakingRollYearTable = this.form29551Controller.WorkItem.F1403_GetSaleTrackingRollYear(this.keyId).SaleTrakingRollYear;
            if (this.SaleTrakingRollYearTable.Rows.Count > 0)
            {
                this.rollYear = Int32.Parse(this.SaleTrakingRollYearTable.Rows[0][0].ToString());
            }
        }

    }
}
