//--------------------------------------------------------------------------------------------
// <copyright file="F29550.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F29550. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 30 Act 07		Ramya.D             Created
// 24 Mar 09        ShanmugaSundaram.A  Issue which is not entered in TSF are fixed for sprint 69
// 21 Apr 09        Sadha Shivudu M     Implemented the TSCO# 6463
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
    /// F35102 class file
    /// </summary>
    [SmartPart]
    public partial class F29550 : BaseSmartPart
    {
        #region variables

        ////int outValue = 0;

        /// <summary>
        /// tempDataTable
        /// </summary>
        private DataTable tempDataTable = new DataTable();

        /// <summary>
        /// masterFormNo Local variable.0
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// outValue
        /// </summary>
        private decimal outValue;

        /// <summary>
        /// outIntValue
        /// </summary>
        private int outIntValue;

        /// <summary>
        /// typeComboData
        /// </summary>
        private CommonData typeComboData = new CommonData();

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int keyId;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int newMode;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int returnValue;

        /// <summary>
        /// ownerId Local variable.
        /// </summary>
        private int ownerId;

        /// <summary>
        /// newParcelId Local variable.
        /// </summary>
        private int newParcelId;

        /// <summary>
        /// parcelId Local variable.
        /// </summary>
        private int parcelId;

        /// <summary>
        /// editParcelId Local variable.
        /// </summary>
        private int editParcelId;

        /// <summary>
        /// saleId Local variable.
        /// </summary>
        private int saleId;

        /// <summary>
        /// ratioAtSale
        /// </summary>
        private decimal ratioAtSale = 0.00M;

        /// <summary>
        /// currentRatio
        /// </summary>
        private decimal currentRatio = 0.00M;

        /// <summary>
        /// newRatio
        /// </summary>
        private decimal newRatio = 0.00M;

        /// <summary>
        /// seniorExemptDataSet
        /// </summary>
        private DataSet tempDataSet = new DataSet("Root");

        /// <summary>
        /// colorValue Local variable.
        /// </summary>
        private int lastColumnWidth;

        /// <summary>
        /// colorValue Local variable.
        /// </summary>
        private int columnWidth;

        /// <summary>
        /// comboSelectedValue Local variable.
        /// </summary>
        private string[] advRGBColor;

        /// <summary>
        /// confiRGBColor Local variable.
        /// </summary>
        private string[] confiRGBColor;

        /// <summary>
        /// riviewRGBColor Local variable.
        /// </summary>
        private string[] riviewRGBColor;

        /// <summary>
        /// comboSelectedValue Local variable.
        /// </summary>
        private string[] confiItem;

        /// <summary>
        /// comboSelectedValue Local variable.
        /// </summary>
        private string[] reviewItem;

        /// <summary>
        /// comboSelectedValue Local variable.
        /// </summary>
        private string[] advItem;

        /// <summary>
        /// localQulificationItem Local variable.
        /// </summary>
        private string[] localQulificationItem;

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
        /// dialogTxt Local variable.
        /// </summary>
        private string dialogTxt;

        /// <summary>
        /// dialogTxt Local variable.
        /// </summary>
        private string menuStripItem;

        /// <summary>
        /// dialogTxt Local variable.
        /// </summary>
        private int dialogBoxId;

        /// <summary>
        /// riviewComboSelectedValue Local variable.
        /// </summary>
        private int rowIndex;

        /// <summary>
        /// userId Local variable.
        /// </summary>
        private int userId;

        /// <summary>
        /// rowBackColor
        /// </summary>
        private Color rowBackColor;

        /// <summary>
        /// parcelTrackingDetailsDataSet
        /// </summary>
        private F29550ParcelSaleTracking parcelTrackingDetailsDataSet = new F29550ParcelSaleTracking();

        /// <summary>
        /// snapshotId
        /// </summary>
        private int snapshotId;

        /// <summary>
        /// PartiesOwnerDetailsData
        /// </summary>
        private F15010ExciseAffidavitData ownerDetailDataSet = new F15010ExciseAffidavitData();

        /// <summary>
        /// getNeighborhoodHeaderData
        /// </summary>
        private F29550ParcelSaleTracking.f29550ListParcelSaleTrackingRow parcelSaleTrackingRow;

        /// <summary>
        /// getNeighborhoodHeaderData
        /// </summary>
        private F29550ParcelSaleTracking.f29550_PushSaleOwnerRow pushOwnerRow;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// scrollEnter
        /// </summary>
        private bool scrollEnter;

        /// <summary>
        /// pushOwnerFlag
        /// </summary>
        private bool tempFlag;

        /// <summary>
        /// pushParcelFlag
        /// </summary>
        private bool cancelOwnerFlag;

        /// <summary>
        /// pushParcelFlag
        /// </summary>
        private bool ownerFlag = true;

        /// <summary>
        /// Instance of F29600 Controller to call the WorkItem
        /// </summary>
        private F29550Controller form29550Controller;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// An object for ContextMenuStrip
        /// </summary>
        private ContextMenuStrip selectedComponentMenuStrip = new ContextMenuStrip();

        /// <summary>
        /// CurrentCoulmnindex
        /// </summary>
        private int currentCoulmnindex;

        /// <summary>
        /// systemSnapShotCount
        /// </summary>
        private int systemSnapShotCount;

        #endregion variables

        #region Constructor

        /// <summary>
        /// F29550
        /// </summary>
        public F29550()
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
        public F29550(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.SaleParcelPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SaleParcelPictureBox.Height, this.SaleParcelPictureBox.Width, SharedFunctions.GetResourceString("SaleHeader"), red, green, blue);
            this.ParcelGidpictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ParcelGidpictureBox.Height, this.ParcelGidpictureBox.Width, SharedFunctions.GetResourceString("ParcelHeader"), red, green, blue);
            this.OwnersPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.OwnersPictureBox.Height, this.OwnersPictureBox.Width, SharedFunctions.GetResourceString("OwnerHeader"), red, green, blue);
            this.ParcelsSoldPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ParcelsSoldPictureBox.Height, this.ParcelsSoldPictureBox.Width, SharedFunctions.GetResourceString("StatsHeader"), red, green, blue);
            ////this.formLoad = false;
            this.tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(SharedFunctions.GetResourceString("SaleOwnerID")), new DataColumn(SharedFunctions.GetResourceString("ParcelSaleOwnerId")), new DataColumn(SharedFunctions.GetResourceString("OwnerColumn")), new DataColumn(SharedFunctions.GetResourceString("IsBuyer")), new DataColumn(SharedFunctions.GetResourceString("Address1")), new DataColumn(SharedFunctions.GetResourceString("Location")), new DataColumn(SharedFunctions.GetResourceString("ParcelID")), new DataColumn(SharedFunctions.GetResourceString("SaleID")) });
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
        /// Gets or sets the F29600 control.
        /// </summary>
        /// <value>The F29600 control.</value>
        [CreateNew]
        public F29550Controller F29550Control
        {
            get { return this.form29550Controller as F29550Controller; }
            set { this.form29550Controller = value; }
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

                    ////if (this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.Rows.Count > 0)

                    if (this.parcelTrackingDetailsDataSet.f29550_ModeTable.Rows.Count > 0 && this.newMode == 2)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = true;
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
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
            this.cancelOwnerFlag = true;
            this.LoadParcelDetails();
            ////this.CustomizeListingParcelDetails();

            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.cancelOwnerFlag = false;
            this.tempDataTable.Clear();
            this.AdvisoryComboBox.Select();
            this.AdvisoryComboBox.Focus();
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
                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
                }
            }
            else
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
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
                this.SaveButtonClick();
                this.tempDataTable.Clear();
                this.AdvisoryComboBox.Focus();
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
                this.LoadLocalQualificationComboBox();
                this.FillComboBoxes();
                this.LoadParcelDetails();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
        }

        /// <summary>
        /// Called when [D9030_ F9033_ system snapshot complete event].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9033_SystemSnapshotCompleteEvent, ThreadOption.UserInterface)]
        public void OnD9030_F9033_SystemSnapshotCompleteEvent(object sender, TerraScan.Infrastructure.Interface.EventArgs<SetSystemSnapShotIdnCount> eventArgs)
        {
            this.snapshotId = eventArgs.Data.SystemSnapShotId;
            this.systemSnapShotCount = eventArgs.Data.SystemSnapShotCount;
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

        #region LoadEvent
        /// <summary>
        /// F29550_Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F29550_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.AdjSalePriceTextBox.ApplyNegativeStandard = false;
                this.FlagSliceForm = true;
                this.LoadLocalQualificationComboBox();
                this.FillComboBoxes();
                this.CustomizeListingOwnerDetails();
                this.LoadParcelDetails();
                this.CreateContextMenu();
                this.ParcelDataGridView.Top = -1;
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
        #endregion LoadEvent

        #region UserDefine Methods

        /// <summary>
        /// ClearParcelSaleControls
        /// </summary>
        private void ClearParcelSaleControls()
        {
            this.ParcelNumberTextBox.Text = string.Empty;
            this.SitusTextBox.Text = string.Empty;
            this.UserTextBox.Text = string.Empty;
            this.DeedTypeTextBox.Text = string.Empty;

            this.BookPageTextBox.Text = string.Empty;

            this.SalesFileTextBox.Text = string.Empty;

            this.SalDateTextBox.Text = string.Empty;

            this.NeighborhoodTextBox.Text = string.Empty;
            this.PropertyClassTextBox.Text = string.Empty;
            this.TaxDistrictTextBox.Text = string.Empty;
            this.SaleNoteTextBox.Text = string.Empty;

            this.LegalTextBox.Text = string.Empty;
            this.OriginalSalePriceTextBox.Text = string.Empty;
            this.OriginalSFTextBox.Text = string.Empty;
            this.AdjustedPPTextBox.Text = string.Empty;
            this.AdjustedFinanceTextBox.Text = string.Empty;
            this.AdjTimeSaleTextBox.Text = string.Empty;
            this.AdjOtherTextBox.Text = string.Empty;
            this.AdjSalePriceTextBox.Text = string.Empty;
            this.AdjustedSFTextBox.Text = string.Empty;
            this.EstimateatSaleTextBox.Text = string.Empty;
            this.RatioatSaleTextBox.Text = string.Empty;
            this.CurrentEstimateTextBox.Text = string.Empty;
            this.CurrentRatioTextBox.Text = string.Empty;
            this.AppraisalEstimateTextBox.Text = string.Empty;
            this.NewRatioTextBox.Text = string.Empty;
        }

        /// <summary>
        /// ChangeTxtBoxBackColor
        /// </summary>
        private void ChangeTxtBoxBackColor()
        {
            this.DeedTypeTextBox.BackColor = System.Drawing.Color.White;
            this.BookPageTextBox.BackColor = System.Drawing.Color.White;
            this.SaleNoteTextBox.BackColor = System.Drawing.Color.White;
            this.SalDateTextBox.BackColor = System.Drawing.Color.White;
            this.SalesFileTextBox.BackColor = System.Drawing.Color.White;
            this.PropertyClassTextBox.BackColor = System.Drawing.Color.White;
            this.LegalTextBox.BackColor = System.Drawing.Color.White;
            this.OriginalSalePriceTextBox.BackColor = System.Drawing.Color.White;
            this.OriginalSFTextBox.BackColor = System.Drawing.Color.White;
            this.AdjustedPPTextBox.BackColor = System.Drawing.Color.White;
            this.AdjustedFinanceTextBox.BackColor = System.Drawing.Color.White;
            this.AdjTimeSaleTextBox.BackColor = System.Drawing.Color.White;
            this.AdjOtherTextBox.BackColor = System.Drawing.Color.White;
            this.AdjSalePriceTextBox.BackColor = System.Drawing.Color.White;
            this.AdjustedSFTextBox.BackColor = System.Drawing.Color.White;
            this.EstimateatSaleTextBox.BackColor = System.Drawing.Color.White;
            this.RatioatSaleTextBox.BackColor = System.Drawing.Color.White;
            this.CurrentEstimateTextBox.BackColor = System.Drawing.Color.White;
            this.CurrentRatioTextBox.BackColor = System.Drawing.Color.White;
            this.AppraisalEstimateTextBox.BackColor = System.Drawing.Color.White;
            this.NewRatioTextBox.BackColor = System.Drawing.Color.White;
        }

        /// <summary>
        /// ControlStatus
        /// </summary>
        /// <param name="show">show</param>
        private void ControlStatus(bool show)
        {
            this.AdvisoryComboBox.Enabled = !show;
            this.ConfidenceComboBox.Enabled = !show;
            this.ReviewStatusComboBox.Enabled = !show;
            this.LocalQualificationComboBox.Enabled = !show;
            this.StateCategorizationComboBox.Enabled = !show;
            this.ParcelDataGridView.Enabled = !show;
            this.OwnerGridView.Enabled = !show;
            ////this.PushOwnerButton.Enabled = !show;
            ////this.PushParcelsButton.Enabled = !show;
        }

        /// <summary>
        /// PanelStatus
        /// </summary>
        /// <param name="status">status</param>
        private void PanelStatus(bool status)
        {
            this.ParcelNumberPanel.Enabled = status;
            this.SitusPanel.Enabled = status;
            this.UserPanel.Enabled = status;
            this.DeedTypePanel.Enabled = status;
            this.BookPagePanel.Enabled = status;
            this.SalesFilePanel.Enabled = status;
            this.SalDatePanel.Enabled = status;
            this.NeighborhoodPanel.Enabled = status;
            this.SaleNotePanel.Enabled = status;
            this.LegalPanel.Enabled = status;
            this.ButtonPanel.Enabled = status;
            this.TaxDistrictPanel.Enabled = status;
            this.propertyClassPanel.Enabled = status;
            this.LocalQualificationPanel.Enabled = status;
            this.OriginalSalePricePanel.Enabled = status;
            this.OriginalSFPanel.Enabled = status;
            this.AdjustedPPPanel.Enabled = status;
            this.AdjustedFinancePanel.Enabled = status;
            this.AdjTimeSalePanel.Enabled = status;
            this.AdjOtherPanel.Enabled = status;
            this.AdjustedSFPanel.Enabled = status;
            this.AdjSalePricePanel.Enabled = status;
            this.EstimateatSalePanel.Enabled = status;
            this.RatioatSalePanel.Enabled = status;
            this.CurrentEstimatePanel.Enabled = status;
            this.CurrentRatioPanel.Enabled = status;
            this.AppraisalEstimatePanel.Enabled = status;
            this.CurrentRatioPanel.Enabled = status;
            this.NewRatioPanel.Enabled = status;
            ////ComboBoxes
            this.AdvisoryPanel.Enabled = status;
            this.ConfidencePanel.Enabled = status;
            this.ReviewStatusPanel.Enabled = status;
            this.LocalQualificationPanel.Enabled = status;
            this.StateCategorizationPanel.Enabled = status;
        }

        /// <summary>
        /// CreateContextMenu
        /// </summary>
        private void CreateContextMenu()
        {
            this.selectedComponentMenuStrip.Items.Add(SharedFunctions.GetResourceString("MenuDelete"));
            this.selectedComponentMenuStrip.Items.Add(SharedFunctions.GetResourceString("MenuCancel"));
            this.selectedComponentMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(this.SelectedComponentMenuStrip_ItemClicked);
            this.selectedComponentMenuStrip.Closed += new ToolStripDropDownClosedEventHandler(this.SelectedComponentMenuStrip_Closed);
        }

        /// <summary>
        /// Load LocalQualificationComboBox
        /// </summary>
        private void LoadLocalQualificationComboBox()
        {
            ////this.LocalQualificationComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;

            System.Collections.Hashtable datas = new System.Collections.Hashtable();

            datas.Add(SharedFunctions.GetResourceString("Hold"), 3);

            datas.Add(SharedFunctions.GetResourceString("No"), 2);

            datas.Add(SharedFunctions.GetResourceString("Yes"), 1);

            this.typeComboData.LoadGeneralComboData(datas);
            this.LocalQualificationComboBox.DisplayMember = this.typeComboData.ComboBoxDataTable.KeyNameColumn.ColumnName;
            this.LocalQualificationComboBox.ValueMember = this.typeComboData.ComboBoxDataTable.KeyIdColumn.ColumnName;
            this.LocalQualificationComboBox.DataSource = this.typeComboData.ComboBoxDataTable;
            this.localQulificationItem = new string[this.typeComboData.ComboBoxDataTable.Rows.Count];
            ////this.typeComboData.ComboBoxDataTable.DefaultView.Sort = "KeyId";
            this.LocalQualificationComboBox.SelectedValue = 1;
            for (int localItem = 0; localItem < this.typeComboData.ComboBoxDataTable.Rows.Count; localItem++)
            {
                this.localQulificationItem[localItem] = this.typeComboData.ComboBoxDataTable.Rows[localItem][this.typeComboData.ComboBoxDataTable.KeyNameColumn.ColumnName].ToString();
            }

            ////this.LocalQualificationComboBox.Items.Insert(0, "Yes");
            ////this.LocalQualificationComboBox.Items.Insert(1, "No");
            ////this.LocalQualificationComboBox.Items.Insert(2, "Hold");
            ////this.LocalQualificationComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// FillComboBoxes
        /// </summary>
        private void FillComboBoxes()
        {
            //// this.AdvisoryComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.parcelTrackingDetailsDataSet = this.F29550Control.WorkItem.F29550_GetParcelSaleTrackingComboDetails();
            if (this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows.Count > 0)
            {
                this.AdvisoryComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
                this.advRGBColor = new string[this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows.Count];
                this.advItem = new string[this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows.Count];
                this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].DefaultView.Sort = SharedFunctions.GetResourceString("SaleAdvisoryID");
                this.AdvisoryComboBox.DisplayMember = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Columns[1].ColumnName.ToString();
                this.AdvisoryComboBox.ValueMember = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Columns[0].ColumnName.ToString();
                this.AdvisoryComboBox.DataSource = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")];
                ////this.advComboSelectedValue = this.ConvertStringtoInt(this.AdvisoryComboBox.SelectedValue.ToString());
                for (int advColor = 0; advColor < this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows.Count; advColor++)
                {
                    this.advItem[advColor] = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows[advColor][this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Columns[1].ColumnName].ToString();
                    this.advRGBColor[advColor] = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows[advColor][this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Columns[2].ColumnName].ToString();
                }
            }
            else
            {
                this.AdvisoryComboBox.DataSource = null;
            }

            if (this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Rows.Count > 0)
            {
                this.ConfidenceComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
                this.confiRGBColor = new string[this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Rows.Count];
                this.confiItem = new string[this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Rows.Count];
                this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].DefaultView.Sort = SharedFunctions.GetResourceString("SaleConfidenceID ASC");
                this.ConfidenceComboBox.DisplayMember = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Columns[1].ColumnName.ToString();
                this.ConfidenceComboBox.ValueMember = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Columns[0].ColumnName.ToString();
                this.ConfidenceComboBox.DataSource = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")];
                for (int confiColor = 0; confiColor < this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Rows.Count; confiColor++)
                {
                    this.confiItem[confiColor] = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Rows[confiColor][this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Columns[1].ColumnName].ToString();
                    this.confiRGBColor[confiColor] = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Rows[confiColor][this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Columns[2].ColumnName].ToString();
                }
            }
            else
            {
                this.ConfidenceComboBox.DataSource = null;
            }

            if (this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleStatusTable")].Rows.Count > 0)
            {
                this.ReviewStatusComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
                this.riviewRGBColor = new string[this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleStatusTable")].Rows.Count];
                this.reviewItem = new string[this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleStatusTable")].Rows.Count];
                this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleStatusTable")].DefaultView.Sort = SharedFunctions.GetResourceString("SaleStatusID ASC");
                this.ReviewStatusComboBox.DisplayMember = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleStatusTable")].Columns[1].ColumnName.ToString();
                this.ReviewStatusComboBox.ValueMember = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleStatusTable")].Columns[0].ColumnName.ToString();
                this.ReviewStatusComboBox.DataSource = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleStatusTable")];
                for (int confiColor = 0; confiColor < this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleStatusTable")].Rows.Count; confiColor++)
                {
                    this.reviewItem[confiColor] = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleStatusTable")].Rows[confiColor][this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleStatusTable")].Columns[1].ColumnName].ToString();
                    this.riviewRGBColor[confiColor] = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleStatusTable")].Rows[confiColor][this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleStatusTable")].Columns[2].ColumnName].ToString();
                }
            }
            else
            {
                this.ReviewStatusComboBox.DataSource = null;
            }

            if (this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("Table3")].Rows.Count > 0)
            {
                ////this.parcelTrackingDetailsDataSet.Tables[12].DefaultView.Sort = " StateCategoryID ASC";
                this.StateCategorizationComboBox.DisplayMember = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("Table3")].Columns[1].ColumnName.ToString();
                this.StateCategorizationComboBox.ValueMember = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("Table3")].Columns[0].ColumnName.ToString();
                this.StateCategorizationComboBox.DataSource = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("Table3")];
            }
            else
            {
                this.StateCategorizationComboBox.DataSource = null;
            }
        }

        /// <summary>
        /// LoadParcelDetails
        /// </summary>
        private void LoadParcelDetails()
        {
            this.AdvisoryComboBox.Focus();
            this.parcelTrackingDetailsDataSet = this.F29550Control.WorkItem.F29550_GetParcelSaleTrackingDetails(this.keyId);

            if (this.parcelTrackingDetailsDataSet.f29550_ModeTable.Rows.Count > 0)
            {
                this.newMode = this.ConvertStringtoInt(this.parcelTrackingDetailsDataSet.f29550_ModeTable.Rows[0][this.parcelTrackingDetailsDataSet.f29550_ModeTable.ModeColumn].ToString());
            }

            if (this.parcelTrackingDetailsDataSet.f29550ListParcelSaleTracking.Rows.Count > 0 && this.newMode != 2)
            {
                this.parcelSaleTrackingRow = (F29550ParcelSaleTracking.f29550ListParcelSaleTrackingRow)this.parcelTrackingDetailsDataSet.f29550ListParcelSaleTracking.Rows[0];
                this.parcelId = this.ConvertStringtoInt(this.parcelSaleTrackingRow.ParcelID.ToString());
                this.ParcelNumberTextBox.Text = this.parcelSaleTrackingRow.ParcelNumber;

                this.SitusTextBox.Text = this.parcelSaleTrackingRow.Situs.ToString();

                if (this.parcelSaleTrackingRow.SaleAdvisoryID.ToString().Equals(SharedFunctions.GetResourceString("Zero")))
                {
                    this.AdvisoryComboBox.SelectedValue = 1;
                }
                else
                {
                    this.AdvisoryComboBox.SelectedValue = this.ConvertStringtoInt(this.parcelSaleTrackingRow.SaleAdvisoryID.ToString());
                }

                if (this.parcelSaleTrackingRow.SaleConfidenceID.ToString().Equals(SharedFunctions.GetResourceString("Zero")))
                {
                    this.ConfidenceComboBox.SelectedValue = 1;
                }
                else
                {
                    this.ConfidenceComboBox.SelectedValue = this.ConvertStringtoInt(this.parcelSaleTrackingRow.SaleConfidenceID.ToString());
                }

                if (this.parcelSaleTrackingRow.SaleStatusID.ToString().Equals(SharedFunctions.GetResourceString("Zero")))
                {
                    this.ReviewStatusComboBox.SelectedValue = 2;
                }
                else
                {
                    this.ReviewStatusComboBox.SelectedValue = this.ConvertStringtoInt(this.parcelSaleTrackingRow.SaleStatusID.ToString());
                }

                if (string.IsNullOrEmpty(this.parcelSaleTrackingRow.User))
                {
                    this.UserTextBox.Text = TerraScanCommon.UserName;
                }
                else
                {
                    this.UserTextBox.Text = this.parcelSaleTrackingRow.User;
                    this.userId = this.ConvertStringtoInt(this.parcelSaleTrackingRow.UserID.ToString());
                }

                this.DeedTypeTextBox.Text = this.parcelSaleTrackingRow.DeedType.ToString();
                this.BookPageTextBox.Text = this.parcelSaleTrackingRow.BookPage;
                this.saleId = this.ConvertStringtoInt(this.parcelSaleTrackingRow.SaleID.ToString());
                this.SalesFileTextBox.Text = this.parcelSaleTrackingRow.SalesFileNumber.ToString();
                if (string.IsNullOrEmpty(this.parcelSaleTrackingRow.SaleDate.ToString()))
                {
                    this.SalDateTextBox.Text = DateTime.Now.ToString();
                }
                else
                {
                    this.SalDateTextBox.Text = this.parcelSaleTrackingRow.SaleDate.ToString();
                }

                this.NeighborhoodTextBox.Text = this.parcelSaleTrackingRow.Nighborhood.ToString();
                this.PropertyClassTextBox.Text = this.parcelSaleTrackingRow.PropertyClass.ToString();
                this.TaxDistrictTextBox.Text = this.parcelSaleTrackingRow.TaxDistict.ToString();
                if (this.parcelSaleTrackingRow.LocalQualification.ToString().Equals(SharedFunctions.GetResourceString("Zero")))
                {
                    this.LocalQualificationComboBox.SelectedValue = 1;
                }
                else
                {
                    this.LocalQualificationComboBox.SelectedValue = this.ConvertStringtoInt(this.parcelSaleTrackingRow.LocalQualification.ToString());
                }

                if (this.parcelSaleTrackingRow.StateCategoryID.ToString().Equals(SharedFunctions.GetResourceString("Zero")))
                {
                    this.StateCategorizationComboBox.SelectedValue = 0;
                }
                else
                {
                    this.StateCategorizationComboBox.SelectedValue = this.ConvertStringtoInt(this.parcelSaleTrackingRow.StateCategoryID.ToString());
                }

                this.SaleNoteTextBox.Text = this.parcelSaleTrackingRow.SaleNote.ToString();
                this.LegalTextBox.Text = this.parcelSaleTrackingRow.Legal.ToString();
                this.OriginalSalePriceTextBox.Text = this.parcelSaleTrackingRow.OriginalSaleprice.ToString();
                this.OriginalSFTextBox.Text = this.parcelSaleTrackingRow.OriginalSquareFeet.ToString();
                this.AdjustedPPTextBox.Text = this.parcelSaleTrackingRow.AdjustPP.ToString();
                this.AdjustedFinanceTextBox.Text = this.parcelSaleTrackingRow.AdjustFinance.ToString();
                this.AdjTimeSaleTextBox.Text = this.parcelSaleTrackingRow.AdjustTimeSale.ToString();
                this.AdjOtherTextBox.Text = this.parcelSaleTrackingRow.AdjustOther.ToString();
                this.AdjustedSFTextBox.Text = this.parcelSaleTrackingRow.AdjustedSquareFeet.ToString();
                this.EstimateatSaleTextBox.Text = this.parcelSaleTrackingRow.EstimateAtSale.ToString();
                this.RatioatSaleTextBox.Text = this.parcelSaleTrackingRow.RatioAtSale.ToString("0.00") + "%";
                this.CurrentEstimateTextBox.Text = this.parcelSaleTrackingRow.CurrentEstimate.ToString();
                this.CurrentRatioTextBox.Text = this.parcelSaleTrackingRow.CurrentRatio.ToString("0.00") + " %";
                this.AppraisalEstimateTextBox.Text = this.parcelSaleTrackingRow.AppraisalEstimate.ToString();
                this.NewRatioTextBox.Text = this.parcelSaleTrackingRow.NewRatio.ToString("0.00") + " %";
                //// Issue fixed by A.Shanmuga Sundaram on 24th Mar'09 for sprint 69
                /*if (this.saleId != 0)
                {
                    this.PushOwnerButton.Enabled = true;
                    this.PushParcelsButton.Enabled = true;
                }
                else
                {
                    this.PushOwnerButton.Enabled = false;
                    this.PushParcelsButton.Enabled = false;
                }*/

                if (this.saleId != 0 && this.parcelSaleTrackingRow.IsOwnerPushed.Equals(false))
                {
                    this.PushOwnerButton.Enabled = true;
                }
                else
                {
                    this.PushOwnerButton.Enabled = false;
                }

                if (this.saleId != 0 && this.parcelSaleTrackingRow.IsParcelPushed.Equals(false))
                {
                    this.PushParcelsButton.Enabled = true;
                }
                else
                {
                    this.PushParcelsButton.Enabled = false;
                }

                this.SetAdjustedFieldColor();
                this.AdjustedSalePriceCalculation();
                this.BindOwnerGridView();
                this.CustomizeListingParcelDetails();
                this.PanelStatus(true);

                ////Permission
                this.LockControl(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                this.ControlStatus(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                this.ChangeTxtBoxBackColor();
                this.AdvisoryComboBox.Focus();
                if (this.saleId == 0)
                {
                    if (this.PermissionFiled.newPermission)
                    {
                        this.LockControl(false);
                        this.ControlStatus(false);
                        this.ChangeTxtBoxBackColor();
                        this.SummaryPanel.Enabled = true;
                    }
                    else
                    {
                        this.LockControl(true);
                        this.ControlStatus(true);
                        this.ChangeTxtBoxBackColor();
                        this.SummaryPanel.Enabled = false;
                    }
                }
            }
            else
            {
                ////this.ParcelDataGridView.DataSource = null;
                this.ParcelDataGridView.Columns[0].Width = 146;
                this.ParcelDataGridView.Columns[1].Width = this.ParcelDataGridView.Width - 146 - 37;
                this.ParcelDataGridView.Columns[1].HeaderText = string.Empty;
                this.ParcelDataGridView.Enabled = false;
                try
                {
                    this.ParcelDataGridView.Rows.Add(5);
                    this.ParcelDataGridView.NumRowsVisible = 5;
                }
                catch (Exception ex)
                {
                }

                this.OwnerGridView.DataSource = null;
                this.OwnerGridView.Enabled = false;
                this.ClearParcelSaleControls();
                this.PanelStatus(false);
                this.AdvisoryComboBox.DataSource = null;
                this.ConfidenceComboBox.DataSource = null;
                this.ReviewStatusComboBox.DataSource = null;
                this.StateCategorizationComboBox.DataSource = null;
                this.StateCategorizationComboBox.BackColor = System.Drawing.Color.White;
                this.StateCategorizationPanel.BackColor = System.Drawing.Color.White;
                this.LocalQualificationComboBox.DataSource = null;
                this.ChangeTxtBoxBackColor();
            }
        }

        /// <summary>
        /// ConvertStringtoInt
        /// </summary>
        /// <param name="inputValue">inputValue</param>
        /// <returns>int</returns>
        private int ConvertStringtoInt(string inputValue)
        {
            this.outIntValue = 0;
            if (!string.IsNullOrEmpty(inputValue))
            {
                int.TryParse(inputValue, out this.outIntValue);
            }

            return this.outIntValue;
        }

        /// <summary>
        /// ConvertStringtoDec
        /// </summary>
        /// <param name="inputValue">inputValue</param>
        /// <returns>decimal</returns>
        private decimal ConvertStringtoDec(string inputValue)
        {
            this.outValue = 0.00M;
            if (!string.IsNullOrEmpty(inputValue))
            {
                if (inputValue.Contains("%"))
                {
                    inputValue = inputValue.Replace("%", "");
                    decimal.TryParse(inputValue, out this.outValue);
                }

                if (inputValue.Contains(","))
                {
                    inputValue = inputValue.Replace("%", "");
                    decimal.TryParse(inputValue, out this.outValue);
                }

                if (inputValue.Contains("("))
                {
                    //inputValue = inputValue.Replace("-", "");
                    if (inputValue.Contains(")"))
                    {
                        inputValue = inputValue.Replace(")", "");
                    }

                    if (inputValue.Contains("("))
                    {
                        inputValue = inputValue.Replace("(", "");
                    }

                    decimal.TryParse(inputValue, out this.outValue);
                    this.outValue = decimal.Negate(this.outValue);
                }
                else
                {
                    decimal.TryParse(inputValue, out this.outValue);
                }
            }

            return this.outValue;
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
        /// DisableGridSorting
        /// </summary>
        /// <param name="dataGridViewSorting">dataGridViewSorting</param>
        /// <param name="disableSort">disableSort</param>
        private void DisableGridSorting(DataGridView dataGridViewSorting, bool disableSort)
        {
            for (int columIndex = 0; columIndex <= dataGridViewSorting.Columns.Count - 1; columIndex++)
            {
                if (columIndex != 4)
                {
                    if (disableSort)
                    {
                        dataGridViewSorting.Columns[columIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    else
                    {
                        dataGridViewSorting.Columns[columIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                    }
                }
            }
        }

        /// <summary>
        /// EditRecord
        /// </summary>
        private void EditRecord()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.DisableGridSorting(ParcelDataGridView, true);
                this.DisableGridSorting(OwnerGridView, true);
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
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
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// SetAdjustedFieldColor
        /// </summary>
        private void SetAdjustedFieldColor()
        {
            if (this.AdjustedPPTextBox.Text.Equals(SharedFunctions.GetResourceString("Zero")))
            {
                this.AdjustedPPTextBox.ForeColor = System.Drawing.Color.Black;
            }
            else if (this.ConvertStringtoDec(this.AdjustedPPTextBox.Text.Trim()) > 0)
            {
                this.AdjustedPPTextBox.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
            }
            else
            {
                this.AdjustedPPTextBox.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
                this.AdjustedPPTextBox.Text = "(" + this.AdjustedPPTextBox.Text + ")";
            }

            ////Set Color to AdjustedFinance TextBox
            if (this.AdjustedFinanceTextBox.Text.Equals(SharedFunctions.GetResourceString("Zero")))
            {
                this.AdjustedFinanceTextBox.ForeColor = System.Drawing.Color.Black;
            }
            else if (this.ConvertStringtoDec(this.AdjustedFinanceTextBox.Text.Trim()) > 0)
            {
                this.AdjustedFinanceTextBox.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
            }
            else
            {
                this.AdjustedFinanceTextBox.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
                this.AdjustedFinanceTextBox.Text = "(" + this.AdjustedFinanceTextBox.Text + ")";
            }

            ////Set Color to AdjTimeSale TextBox
            if (this.AdjTimeSaleTextBox.Text.Equals(SharedFunctions.GetResourceString("Zero")))
            {
                this.AdjTimeSaleTextBox.ForeColor = System.Drawing.Color.Black;
            }
            else if (this.ConvertStringtoDec(this.AdjTimeSaleTextBox.Text.Trim()) > 0)
            {
                this.AdjTimeSaleTextBox.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
            }
            else
            {
                this.AdjTimeSaleTextBox.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
                this.AdjTimeSaleTextBox.Text = "(" + this.AdjTimeSaleTextBox.Text + ")";
            }

            ////Set Color to AdjOther TextBox
            if (this.AdjOtherTextBox.Text.Equals(SharedFunctions.GetResourceString("Zero")))
            {
                this.AdjOtherTextBox.ForeColor = System.Drawing.Color.Black;
            }
            else if (this.ConvertStringtoDec(this.AdjOtherTextBox.Text.Trim()) > 0)
            {
                this.AdjOtherTextBox.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
            }
            else
            {
                this.AdjOtherTextBox.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
                this.AdjOtherTextBox.Text = "(" + this.AdjOtherTextBox.Text + ")";
            }
        }

        /// <summary>
        /// AdjustedSalePriceCalculation
        /// </summary>
        private void AdjustedSalePriceCalculation()
        {
            decimal saleprice = 0.00M;
            saleprice = this.ConvertStringtoDec(this.AdjustedPPTextBox.Text.Trim()) + this.ConvertStringtoDec(this.AdjustedFinanceTextBox.Text.Trim()) + this.ConvertStringtoDec(this.AdjTimeSaleTextBox.Text.Trim()) + this.ConvertStringtoDec(this.AdjOtherTextBox.Text.Trim()) + this.ConvertStringtoDec(this.OriginalSalePriceTextBox.Text.Trim());
            if (saleprice <= 922337203685477.5807M && saleprice >= -922337203685477.5808M)
            {
                this.AdjSalePriceTextBox.Text = saleprice.ToString("#,##0").Trim();
            }
            else
            {
                this.AdjSalePriceTextBox.Text = "0";
            }
        }

        /// <summary>
        /// RatioAtSaleCalculation
        /// </summary>
        private void RatioAtSaleCalculation()
        {
            if (this.ConvertStringtoDec(this.EstimateatSaleTextBox.Text.Trim()) != 0 && this.ConvertStringtoDec(this.OriginalSalePriceTextBox.Text.Trim()) != 0)
            {
                this.ratioAtSale = this.ConvertStringtoDec(this.EstimateatSaleTextBox.Text.Trim()) / this.ConvertStringtoDec(this.OriginalSalePriceTextBox.Text.Trim()) * 100;
            }
            else
            {
                this.ratioAtSale = 0.00M;
            }

            if (this.ratioAtSale <= 100.00M & this.ratioAtSale >= -100.00M)
            {
                this.ratioAtSale = Math.Round(this.ratioAtSale, 2);
                this.RatioatSaleTextBox.Text = this.ratioAtSale.ToString("0.00") + "%";
            }
            else
            {
                this.EstimateatSaleTextBox.Text = "0";
                //// Issue fixed by A.Shanmuga Sundaram on 24th Mar'09 for sprint 69
                ////this.OriginalSalePriceTextBox.Text = "0";
                this.RatioatSaleTextBox.Text = "0" + "%";
            }

            ////this.calculationFlag = true;
        }

        /// <summary>
        /// CurrentRatioCalculation
        /// </summary>
        private void CurrentRatioCalculation()
        {
            if (this.ConvertStringtoDec(this.CurrentEstimateTextBox.Text.Trim()) != 0 && this.ConvertStringtoDec(this.AdjSalePriceTextBox.Text.Trim()) != 0)
            {
                this.currentRatio = this.ConvertStringtoDec(this.CurrentEstimateTextBox.Text.Trim()) / this.ConvertStringtoDec(this.AdjSalePriceTextBox.Text.Trim()) * 100;
            }
            else
            {
                this.currentRatio = 0.00M;
            }

            if (this.currentRatio <= 100.00M && this.currentRatio >= -100.00M)
            {
                this.currentRatio = Math.Round(this.currentRatio, 2);
                this.CurrentRatioTextBox.Text = this.currentRatio.ToString("0.00") + "%";
            }
            else
            {
                this.CurrentEstimateTextBox.Text = "0";
                //// Issue fixed by A.Shanmuga Sundaram on 24th Mar'09 for sprint 69
                ////this.AdjSalePriceTextBox.Text = "0";
                this.CurrentRatioTextBox.Text = "0" + "%";
            }
        }

        /// <summary>
        /// NewRatioCalculation
        /// </summary>
        private void NewRatioCalculation()
        {
            if (this.ConvertStringtoDec(this.AppraisalEstimateTextBox.Text.Trim()) != 0 && this.ConvertStringtoDec(this.AdjSalePriceTextBox.Text.Trim()) != 0)
            {
                this.newRatio = this.ConvertStringtoDec(this.AppraisalEstimateTextBox.Text.Trim()) / this.ConvertStringtoDec(this.AdjSalePriceTextBox.Text.Trim()) * 100;
            }
            else
            {
                this.newRatio = 0.00M;
            }

            if (this.newRatio <= 100.00M && this.newRatio >= -100.00M)
            {
                this.newRatio = Math.Round(this.newRatio, 2);
                this.NewRatioTextBox.Text = this.newRatio.ToString("0.00") + "%";
            }
            else
            {
                this.AppraisalEstimateTextBox.Text = "0";
                //// Issue fixed by A.Shanmuga Sundaram on 24th Mar'09 for sprint 69
                ////this.AdjSalePriceTextBox.Text = "0";
                this.NewRatioTextBox.Text = "0" + "%";
            }
        }

        /// <summary>
        /// ConvertTableToXML
        /// </summary>
        /// <param name="rowCount">rowCount</param>
        /// <param name="parcelIdTable">parcelIdTable</param>
        private void ConvertTableToXML(int rowCount, string parcelIdTable)
        {
            DataTable dt = new DataTable();
            if (parcelIdTable == SharedFunctions.GetResourceString("ParcelID"))
            {
                dt.Columns.AddRange(new DataColumn[] { new DataColumn(SharedFunctions.GetResourceString("ParcelID")), new DataColumn(SharedFunctions.GetResourceString("SaleParcelID")), new DataColumn(SharedFunctions.GetResourceString("SaleID")) });
            }
            else if (parcelIdTable == SharedFunctions.GetResourceString(SharedFunctions.GetResourceString("ParcelSaleOwnerId")))
            {
                DataColumn dc = new DataColumn(parcelIdTable);
                dt.Columns.Add(dc);
            }

            DataRow dr;
            for (int i = 0; i < rowCount; i++)
            {
                dr = dt.NewRow();
                if (parcelIdTable == SharedFunctions.GetResourceString("ParcelID"))
                {
                    if (!this.parcelTrackingDetailsDataSet.Tables.Contains(SharedFunctions.GetResourceString("SaleAdvisoryTable")))
                    {
                        if (this.parcelTrackingDetailsDataSet.Tables[12].Rows[i][SharedFunctions.GetResourceString("GridEmptyRecord")].ToString() == SharedFunctions.GetResourceString("False"))
                        {
                            if (this.ConvertStringtoInt(this.parcelTrackingDetailsDataSet.Tables[12].Rows[i][SharedFunctions.GetResourceString("ParcelID")].ToString()) != this.editParcelId)
                            {
                                dr[0] = this.parcelTrackingDetailsDataSet.Tables[12].Rows[i][parcelIdTable].ToString();

                                if (this.menuStripItem != (SharedFunctions.GetResourceString("Delete")))
                                {
                                    dr[1] = this.parcelTrackingDetailsDataSet.Tables[12].Rows[i][SharedFunctions.GetResourceString("SaleParcelID")].ToString();
                                }

                                dr[2] = this.parcelTrackingDetailsDataSet.Tables[12].Rows[i][SharedFunctions.GetResourceString(SharedFunctions.GetResourceString("SaleID"))].ToString();

                                dt.Rows.Add(dr);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (this.ConvertStringtoInt(this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows[i][SharedFunctions.GetResourceString("ParcelID")].ToString()) != this.editParcelId)
                        {
                            dr[0] = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows[i][parcelIdTable].ToString();
                            if (this.menuStripItem != (SharedFunctions.GetResourceString("Delete")))
                            {
                                dr[1] = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows[i][SharedFunctions.GetResourceString("SaleParcelID")].ToString();
                            }

                            dr[2] = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows[i][SharedFunctions.GetResourceString(SharedFunctions.GetResourceString("SaleID"))].ToString();
                            dt.Rows.Add(dr);
                        }
                    }
                }
                else
                {
                    if (this.ownerFlag || this.cancelOwnerFlag)
                    {
                        if (this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Rows[i][SharedFunctions.GetResourceString("GridEmptyRecord")].ToString() == SharedFunctions.GetResourceString("False"))
                        {
                            if (this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Rows[i][this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.IsBuyerColumn.ColumnName].ToString().Equals(SharedFunctions.GetResourceString("IsBuyer")))
                            {
                                dr[0] = this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Rows[i][parcelIdTable].ToString();
                                dt.Rows.Add(dr);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (this.parcelTrackingDetailsDataSet.Tables.Contains(SharedFunctions.GetResourceString("SaleConfidenceTable")))
                        {
                            if (this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Rows[i][SharedFunctions.GetResourceString("GridEmptyRecord")].ToString() == SharedFunctions.GetResourceString("False"))
                            {
                                if (this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Rows[i][this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Columns[SharedFunctions.GetResourceString("IsBuyer")]].ToString().Equals(SharedFunctions.GetResourceString("Buyer")))
                                {
                                    dr[0] = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Rows[i][parcelIdTable].ToString();
                                    dt.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        else if (this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Rows[i][SharedFunctions.GetResourceString("GridEmptyRecord")].ToString() == SharedFunctions.GetResourceString("False"))
                        {
                            if (this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Rows[i][this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Columns[SharedFunctions.GetResourceString("IsBuyer")]].ToString().Equals(SharedFunctions.GetResourceString("Buyer")))
                            {
                                dr[0] = this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Rows[i][parcelIdTable].ToString();
                                dt.Rows.Add(dr);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }

            dt.AcceptChanges();
            if (this.tempDataSet.Tables.Count > 0)
            {
                this.tempDataSet.Tables.Remove(SharedFunctions.GetResourceString("SaleAdvisoryTable"));
            }

            this.tempDataSet.Tables.Add(dt.Copy());
            this.tempDataSet.Tables[0].TableName = SharedFunctions.GetResourceString("SaleAdvisoryTable");
        }

        #region To Save
        /// <summary>
        /// CheckErrors
        /// </summary>
        /// <param name="formNo">formNo</param>
        /// <returns>bool</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;
            Control requiredControl;
            requiredControl = this.CheckRequiredFields();
            if (requiredControl != null)
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("SaveParcelSaleDetails");
                requiredControl.Focus();
                return sliceValidationFields;
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Checks the required fields.
        /// </summary>
        /// <returns>the Reqried Fields String</returns>
        private Control CheckRequiredFields()
        {
            Control requiredControll = null;

            if (string.IsNullOrEmpty(this.AdvisoryComboBox.Text.Trim()))
            {
                requiredControll = this.AdvisoryComboBox;
            }
            else if (string.IsNullOrEmpty(this.ConfidenceComboBox.Text.Trim()))
            {
                requiredControll = this.ConfidenceComboBox;
            }
            else if (string.IsNullOrEmpty(this.ReviewStatusComboBox.Text.Trim()))
            {
                requiredControll = this.ReviewStatusComboBox;
            }
            else if (string.IsNullOrEmpty(this.SalDateTextBox.Text.Trim()))
            {
                requiredControll = this.SalDateTextBox;
            }
            else if (string.IsNullOrEmpty(this.LocalQualificationComboBox.Text.Trim()))
            {
                requiredControll = this.LocalQualificationComboBox;
            }

            return requiredControll;
        }

        /// <summary>
        /// Save_Click
        /// </summary>
        private void SaveButtonClick()
        {
            this.RatioAtSaleCalculation();
            this.CurrentRatioCalculation();
            this.NewRatioCalculation();
            this.SaveParcelSaleRecords(false);
        }

        /// <summary>
        /// SaveParcelSaleRecords
        /// </summary>
        /// <param name="onclose">onclose</param>
        /// <returns>bool</returns>
        private bool SaveParcelSaleRecords(bool onclose)
        {
            string parcelXML = string.Empty;
            string ownerXML = string.Empty;

            F29550ParcelSaleTracking saveParcelSaleTracking = new F29550ParcelSaleTracking();
            ////F29600SeniorExemptData.saveSeniorExemptDataTableRow seniorExemptRow = saveSeniorExemption.saveSeniorExemptDataTable.NewsaveSeniorExemptDataTableRow();
            F29550ParcelSaleTracking.f29550SaveParcelSaleTrackingRow saveParcelSaleTrackingRow = saveParcelSaleTracking.f29550SaveParcelSaleTracking.Newf29550SaveParcelSaleTrackingRow();
            saveParcelSaleTrackingRow.EventID = this.keyId;
            saveParcelSaleTrackingRow.SaleAdvisoryID = this.ConvertStringtoInt(this.AdvisoryComboBox.SelectedValue.ToString());
            saveParcelSaleTrackingRow.SaleConfidenceID = this.ConvertStringtoInt(this.ConfidenceComboBox.SelectedValue.ToString());
            saveParcelSaleTrackingRow.SaleStatusID = this.ConvertStringtoInt(this.ReviewStatusComboBox.SelectedValue.ToString());
            saveParcelSaleTrackingRow.DeedType = this.DeedTypeTextBox.Text.Trim();
            saveParcelSaleTrackingRow.BookPage = this.BookPageTextBox.Text.Trim();
            saveParcelSaleTrackingRow.SalesFileNumber = this.SalesFileTextBox.Text.Trim();
            saveParcelSaleTrackingRow.SaleDate = this.SalDateTextBox.Text.Trim();
            saveParcelSaleTrackingRow.LocalQualification = this.ConvertStringtoInt(this.LocalQualificationComboBox.SelectedValue.ToString());
            saveParcelSaleTrackingRow.StateCategoryID = this.ConvertStringtoInt(this.StateCategorizationComboBox.SelectedValue.ToString());
            saveParcelSaleTrackingRow.SaleNote = this.SaleNoteTextBox.Text.Trim();
            saveParcelSaleTrackingRow.StateCode = this.LegalTextBox.Text.Trim();
            saveParcelSaleTrackingRow.OriginalSaleprice = this.ConvertStringtoDec(this.OriginalSalePriceTextBox.Text.Trim());
            saveParcelSaleTrackingRow.OriginalSquareFeet = this.ConvertStringtoDec(this.OriginalSFTextBox.Text.Trim());
            saveParcelSaleTrackingRow.AdjustPP = this.ConvertStringtoDec(this.AdjustedPPTextBox.Text.Trim());
            saveParcelSaleTrackingRow.AdjustFinance = this.ConvertStringtoDec(this.AdjustedFinanceTextBox.Text.Trim());
            saveParcelSaleTrackingRow.AdjustTimeSale = this.ConvertStringtoDec(this.AdjTimeSaleTextBox.Text.Trim());
            saveParcelSaleTrackingRow.AdjustOther = this.ConvertStringtoDec(this.AdjOtherTextBox.Text.Trim());
            saveParcelSaleTrackingRow.AdjustedSquareFeet = this.ConvertStringtoDec(this.AdjustedSFTextBox.Text.Trim());
            saveParcelSaleTrackingRow.EstimateAtSale = this.ConvertStringtoDec(this.EstimateatSaleTextBox.Text.Trim());
            saveParcelSaleTrackingRow.RatioAtSale = this.ConvertStringtoDec(this.RatioatSaleTextBox.Text.Trim());
            saveParcelSaleTrackingRow.CurrentEstimate = this.ConvertStringtoDec(this.CurrentEstimateTextBox.Text.Trim());
            saveParcelSaleTrackingRow.CurrentRatio = this.ConvertStringtoDec(this.CurrentRatioTextBox.Text.Trim());
            saveParcelSaleTrackingRow.AppraisalEstimate = this.ConvertStringtoDec(this.AppraisalEstimateTextBox.Text.Trim());
            saveParcelSaleTrackingRow.NewRatio = this.ConvertStringtoDec(this.NewRatioTextBox.Text.Trim());
            saveParcelSaleTracking.f29550SaveParcelSaleTracking.Rows.Add(saveParcelSaleTrackingRow);
            saveParcelSaleTracking.f29550SaveParcelSaleTracking.AcceptChanges();

            ////XML For ParcelHeader And ParcelSold Details
            DataSet tempSaveDataSet = new DataSet("Root");
            tempSaveDataSet.Tables.Add(saveParcelSaleTracking.f29550SaveParcelSaleTracking.Copy());
            tempSaveDataSet.Tables[0].TableName = SharedFunctions.GetResourceString("SaleAdvisoryTable");

            ////XML For ParcelGrid
            this.ParcelDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            if (this.parcelTrackingDetailsDataSet.Tables.Contains(SharedFunctions.GetResourceString("SaleAdvisoryTable")))
            {
                this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].AcceptChanges();
                parcelXML = TerraScanCommon.GetXmlString(this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")]);
            }
            else
            {
                this.parcelTrackingDetailsDataSet.Tables[12].AcceptChanges();
                parcelXML = TerraScanCommon.GetXmlString(this.parcelTrackingDetailsDataSet.Tables[12]);
            }

            ////XML For OwnerGrid
            this.OwnerGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.AcceptChanges();
            if (!this.parcelTrackingDetailsDataSet.Tables.Contains(SharedFunctions.GetResourceString("SaleConfidenceTable")))
            {
                ownerXML = TerraScanCommon.GetXmlString(this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails);
            }
            else
            {
                ownerXML = TerraScanCommon.GetXmlString(this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")]);
            }

            try
            {
                //// DB Call To Save Parcel Sale Details
                this.returnValue = this.form29550Controller.WorkItem.F29550_saveParcelSaleDetails(this.keyId, tempSaveDataSet.GetXml(), parcelXML, ownerXML, TerraScanCommon.UserId);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

            //// To call the currently Saved Record
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            SliceReloadActiveRecord currentKeyIdInfo;
            currentKeyIdInfo.MasterFormNo = this.masterFormNo;
            currentKeyIdInfo.SelectedKeyId = this.returnValue;
            this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentKeyIdInfo));
            ////SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
            ////sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
            ////sliceReloadActiveRecord.SelectedKeyId = this.returnValue;
            ////this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
            if (onclose)
            {
                return true;
            }

            return true;
        }

        #endregion To Save

        /// <summary>
        /// CustomizeListingParcelDetails
        /// </summary>
        private void CustomizeListingParcelDetails()
        {
            this.ParcelDataGridView.Top = 0;
            this.columnWidth = 0;
            int colCount = 0;
            this.ParcelDataGridView.AutoGenerateColumns = true;
            this.ParcelDataGridView.Width = 764;
            this.ParcelDataGridView.Columns[SharedFunctions.GetResourceString("ParcelgridColumn")].DataPropertyName = this.parcelTrackingDetailsDataSet.Tables[12].Columns[SharedFunctions.GetResourceString("ParcelSaleNumber")].ColumnName.ToString();
            this.ParcelDataGridView.Columns["Type"].DataPropertyName = this.parcelTrackingDetailsDataSet.Tables[12].Columns["Type"].ColumnName.ToString();
            this.ParcelDataGridView.PrimaryKeyColumnName = this.parcelTrackingDetailsDataSet.Tables[12].Columns[SharedFunctions.GetResourceString("ParcelID")].ColumnName.ToString();
            DataTable typedatatable = new DataTable();
            if (typedatatable.Columns.Count == 0)
            {
                typedatatable.Columns.Add("Typeid");
                typedatatable.Columns.Add("Type");
            }

            DataRow dr;
            dr = typedatatable.NewRow();
            dr["Typeid"] = 0;
            dr["Type"] = "Both";
            typedatatable.Rows.Add(dr);

            dr = typedatatable.NewRow();
            dr["Typeid"] = 1;
            dr["Type"] = "Land";
            typedatatable.Rows.Add(dr);

            dr = typedatatable.NewRow();
            dr["Typeid"] = 2;
            dr["Type"] = "Improvement";
            typedatatable.Rows.Add(dr);
            (this.Type as DataGridViewComboBoxColumn).DataSource = typedatatable;
            (this.Type as DataGridViewComboBoxColumn).DisplayMember = typedatatable.Columns[1].ColumnName;
            (this.Type as DataGridViewComboBoxColumn).ValueMember = typedatatable.Columns[0].ColumnName;

            if (this.parcelTrackingDetailsDataSet.Tables[12].Columns.Count > 8)
            {
                this.ParcelDataGridView.DataSource = this.parcelTrackingDetailsDataSet.Tables[12].DefaultView;
            }
            else
            {
                DataColumn dc = new DataColumn(SharedFunctions.GetResourceString("EmptyColumn"));
                if (this.parcelTrackingDetailsDataSet.Tables[12].Columns.Contains(SharedFunctions.GetResourceString("EmptyColumn")))
                {
                    this.parcelTrackingDetailsDataSet.Tables[12].Columns.Remove(SharedFunctions.GetResourceString("EmptyColumn"));
                }

                this.parcelTrackingDetailsDataSet.Tables[12].Columns.Add(dc);
                this.ParcelDataGridView.DataSource = this.parcelTrackingDetailsDataSet.Tables[12].DefaultView;
            }
            //// this.originalRowCount = this.ParcelDataGridView.OriginalRowCount;

            colCount = this.ParcelDataGridView.Columns.Count;
            ////his.isFolder = false;
            ////this.isFolder = true;

            ////Set Dynamic Column Width
            for (int i = 0; i < colCount - 1; i++)
            {
                ////if (i < 9)
                ////{
                this.ParcelDataGridView.Columns[i].Width = 115;
                ////}
                ////else
                ////{
                ////    this.ParcelDataGridView.Columns[i].Width = 0;
                ////}
                this.ParcelDataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.ParcelDataGridView.Columns[i].Resizable = DataGridViewTriState.False;
                this.columnWidth = this.ParcelDataGridView.Columns[i].Width + this.columnWidth;
                if (i != 4)
                {
                    this.ParcelDataGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            this.ParcelDataGridView.Columns[SharedFunctions.GetResourceString("ParcelID")].Visible = false;
            this.ParcelDataGridView.Columns[SharedFunctions.GetResourceString("SaleID")].Visible = false;
            this.ParcelDataGridView.Columns[SharedFunctions.GetResourceString("SaleParcelID")].Visible = false;
            this.ParcelDataGridView.Columns[SharedFunctions.GetResourceString("GridEmptyRecord")].Visible = false;
            this.ParcelDataGridView.Columns[SharedFunctions.GetResourceString("ParcelgridColumn")].Width = 152;
            this.ParcelDataGridView.Columns[SharedFunctions.GetResourceString("ParcelgridColumn")].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            ////Fill Empty Space
            ////if (colCount > 10)
            if (this.parcelTrackingDetailsDataSet.Tables[12].Columns.Count > 8)
            {
                this.ParcelGridHScrollBar.Visible = false;
            }
            else
            {
                this.ParcelGridHScrollBar.Visible = true;
                this.ParcelGridHScrollBar.Enabled = false;
                this.ParcelDataGridView.Columns[SharedFunctions.GetResourceString("EmptyColumn")].HeaderText = string.Empty;

                this.lastColumnWidth = (this.ParcelDataGridView.Width - 41) - (this.columnWidth - (4 * 120)) - 52;
                this.ParcelDataGridView.Columns[SharedFunctions.GetResourceString("EmptyColumn")].Width = this.lastColumnWidth;
            }

            int emptyRecordCount = 0;
            for (int i = 0; i < this.parcelTrackingDetailsDataSet.Tables[12].Rows.Count; i++)
            {
                if (this.parcelTrackingDetailsDataSet.Tables[12].Rows[i][SharedFunctions.GetResourceString("GridEmptyRecord")].ToString() == SharedFunctions.GetResourceString("False"))
                {
                    emptyRecordCount++;
                }
                else
                {
                    break;
                }
            }

            if (emptyRecordCount >= 5)
            {
                this.parcelTrackingDetailsDataSet.Tables[12].Rows.InsertAt(this.parcelTrackingDetailsDataSet.Tables[12].NewRow(), this.ParcelDataGridView.Rows.Count);
                this.ParcelGridVScrollBar.Visible = false;
                this.parcelTrackingDetailsDataSet.Tables[12].Rows[this.ParcelDataGridView.Rows.Count - 1][SharedFunctions.GetResourceString("GridEmptyRecord")] = true;
                this.ParcelDataGridView.Columns[SharedFunctions.GetResourceString("ParcelgridColumn")].Width = 152;
                ////Added for Scroll    
                this.ParcelDataGridView.Width = 764;
            }
            else
            {
                this.ParcelGridVScrollBar.Visible = true;
                this.ParcelGridVScrollBar.Enabled = false;

                if (this.ParcelDataGridView.Columns.Contains(this.ParcelDataGridView.Columns[SharedFunctions.GetResourceString("EmptyColumn")]))
                {
                    this.ParcelDataGridView.Columns[SharedFunctions.GetResourceString("EmptyColumn")].Width = 114;
                }

                ////Added for Scroll
                this.ParcelDataGridView.Width = 748;
                //// this.ParcelDataGridView.BorderStyle = BorderStyle.FixedSingle;
            }

            this.CreateTextBox(colCount, emptyRecordCount);
            this.ParcelDataGridView.Top = -1;
        }

        /// <summary>
        /// CreateTextBox
        /// </summary>
        /// <param name="colCount">colCount</param>
        /// <param name="emptyLessRow">emptyLessRow</param>
        private void CreateTextBox(int colCount, int emptyLessRow)
        {
            TerraScanTextBox[] parcelTxtBox = new TerraScanTextBox[colCount + 1];
            this.SummaryPanel.Controls.Clear();
            int leftPos = 0;
            decimal totals = 0.00M;
            for (int i = 4; i < colCount - 1; i++)
            {
                parcelTxtBox[i] = new TerraScanTextBox();
                parcelTxtBox[i].Text = i.ToString();
                if (i == 4)
                {
                    parcelTxtBox[i].Width = this.ParcelDataGridView.Columns[SharedFunctions.GetResourceString("ParcelgridColumn")].Width + this.ParcelDataGridView.Columns[2].DividerWidth + 122;
                }
                else
                {
                    if (i != 0 && i != 1 && i != 2 && i != 3 && i != colCount - 1)
                    {
                        for (int gridRow = 0; gridRow < emptyLessRow; gridRow++)
                        {
                            totals = totals + this.ConvertStringtoDec(this.ParcelDataGridView.Rows[gridRow].Cells[i].Value.ToString());
                        }

                        parcelTxtBox[i].Text = totals.ToString(" #,##0").Trim() + " ";
                        parcelTxtBox[i].TextAlign = HorizontalAlignment.Right;
                        totals = 0.00M;
                    }

                    parcelTxtBox[i].Width = this.ParcelDataGridView.Columns[i].Width + this.ParcelDataGridView.Columns[2].DividerWidth + 1;
                }

                parcelTxtBox[i].Top = -1;
                parcelTxtBox[i].Left = leftPos;
                if (i == 4)
                {
                    ////    if (!this.ParcelGridHScrollBar.Visible)
                    ////    {
                    ////        leftPos = leftPos + ParcelDataGridView.Columns[SharedFunctions.GetResourceString("ParcelgridColumn")].Width + ParcelDataGridView.Columns[1].DividerWidth + 17;
                    ////    }
                    ////    else
                    ////    {
                    leftPos = leftPos + ParcelDataGridView.Columns[SharedFunctions.GetResourceString("ParcelgridColumn")].Width + ParcelDataGridView.Columns[2].DividerWidth + 132;
                    ////}
                }
                else
                {
                    leftPos = leftPos + ParcelDataGridView.Columns[i].Width + ParcelDataGridView.Columns[2].DividerWidth;
                }

                //// Set TextBox Property
                parcelTxtBox[i].TextAlign = HorizontalAlignment.Right;
                parcelTxtBox[i].BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
                parcelTxtBox[i].ForeColor = System.Drawing.Color.White;
                parcelTxtBox[i].ReadOnly = true;
                parcelTxtBox[i].LockKeyPress = true;
                parcelTxtBox[i].BorderStyle = BorderStyle.FixedSingle;
                parcelTxtBox[i].Font = new Font(SharedFunctions.GetResourceString("Arial"), 10, (System.Drawing.FontStyle)System.Drawing.FontStyle.Bold);

                ////Add TextBox to Panel
                this.SummaryPanel.Controls.Add(parcelTxtBox[i]);
            }

            this.SummaryPanel.AutoScroll = true;
            this.SummaryPanel.HorizontalScroll.Visible = true;
            this.SummaryPanel.VerticalScroll.Visible = false;
            parcelTxtBox[4].Width = 285;

            parcelTxtBox[4].Text = SharedFunctions.GetResourceString("Totals") + " ";
            parcelTxtBox[4].TextAlign = HorizontalAlignment.Right;
            if (colCount > 10)
            {
                this.ParcelGridHScrollBar.Visible = false;
            }
            else
            {
                this.SummaryPanel.Controls.Add(this.ParcelGridHScrollBar);
                this.SummaryPanel.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
                this.ParcelGridHScrollBar.Visible = true;
                this.ParcelGridHScrollBar.Enabled = false;
                this.SummaryPanel.AutoScroll = false;
                this.ParcelGridHScrollBar.BringToFront();
            }
        }

        /// <summary>
        /// CustomizeListingOwnerDetails
        /// </summary>
        private void CustomizeListingOwnerDetails()
        {
            this.OwnerGridView.AutoGenerateColumns = false;
            this.OwnerGridView.Columns[SharedFunctions.GetResourceString("OwnerField")].DataPropertyName = this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.OwnerColumn.ToString();
            this.OwnerGridView.Columns[SharedFunctions.GetResourceString("IsBuyer")].DataPropertyName = this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.IsBuyerColumn.ToString();
            this.OwnerGridView.Columns[SharedFunctions.GetResourceString("OwnerAddress")].DataPropertyName = this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Address1Column.ToString();
            this.OwnerGridView.Columns[SharedFunctions.GetResourceString("Location")].DataPropertyName = this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.LocationColumn.ToString();
            this.OwnerGridView.Columns[SharedFunctions.GetResourceString("OwnerIdField")].DataPropertyName = this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.OwnerIDColumn.ToString();
            this.OwnerGridView.Columns[SharedFunctions.GetResourceString("ParcelSaleOwnerId")].DataPropertyName = this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.SaleOwnerIDColumn.ToString();
            this.OwnerGridView.Columns[SharedFunctions.GetResourceString("SaleIdField")].DataPropertyName = this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.SaleIDColumn.ToString();
            this.OwnerGridView.Columns[SharedFunctions.GetResourceString("OwnerField")].DisplayIndex = 0;
            this.OwnerGridView.Columns[SharedFunctions.GetResourceString("IsBuyer")].DisplayIndex = 1;
            this.OwnerGridView.Columns[SharedFunctions.GetResourceString("OwnerAddress")].DisplayIndex = 2;
            this.OwnerGridView.Columns[SharedFunctions.GetResourceString("Location")].DisplayIndex = 3;
            this.OwnerGridView.Columns[SharedFunctions.GetResourceString("OwnerIdField")].Visible = false;
            this.OwnerGridView.Columns[SharedFunctions.GetResourceString("ParcelSaleOwnerId")].Visible = false;
            this.OwnerGridView.Columns[SharedFunctions.GetResourceString("SaleIdField")].Visible = false;
            this.OwnerGridView.PrimaryKeyColumnName = this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.OwnerIDColumn.ToString();
            this.OwnerGridView.Height = 134;
            //// this.OwnerGridView.Columns[SharedFunctions.GetResourceString("Location")].Width = 158;
            this.OwnerGridVScrollBar.Height = 128;
            this.OwnerGridView.BackgroundColor = System.Drawing.Color.FromArgb(64, 64, 64);
        } 

        /// <summary>
        /// Get XML for Deleting selected Row
        /// </summary>
        /// <param name="parcelRowCount">parcelRowCount</param>
        /// <param name="deletedIndex">deletedIndex</param>
        private void DeleteOwner(int parcelRowCount, int deletedIndex)
        {
            DataTable parceltable = new DataTable();

            ////Create Columns
            DataColumn parcelColumn = new DataColumn(SharedFunctions.GetResourceString("ParcelID"));
            DataColumn parcelSaleColumn = new DataColumn(SharedFunctions.GetResourceString("SaleParcelID"));
            DataColumn saleIdColumn = new DataColumn(SharedFunctions.GetResourceString("SaleID"));
            DataColumn typeColumn = new DataColumn("Type");

            ////Add Columns to Table
            parceltable.Columns.Add(parcelColumn);
            parceltable.Columns.Add(parcelSaleColumn);
            parceltable.Columns.Add(saleIdColumn);
            parceltable.Columns.Add(typeColumn);

            ////Creating Row For Table
            DataRow dr;
            for (int i = 0; i < parcelRowCount; i++)
            {
                dr = parceltable.NewRow();
                if (!this.parcelTrackingDetailsDataSet.Tables.Contains(SharedFunctions.GetResourceString("SaleAdvisoryTable")))
                {
                    if (this.parcelTrackingDetailsDataSet.Tables[12].Rows[i][SharedFunctions.GetResourceString("GridEmptyRecord")].ToString() == SharedFunctions.GetResourceString("False") && !string.IsNullOrEmpty(this.parcelTrackingDetailsDataSet.Tables[12].Rows[i][SharedFunctions.GetResourceString("ParcelSaleNumber")].ToString()) && (this.ConvertStringtoInt(this.parcelTrackingDetailsDataSet.Tables[12].Rows[i][SharedFunctions.GetResourceString("ParcelID")].ToString()) != deletedIndex))
                    {
                        dr[0] = this.parcelTrackingDetailsDataSet.Tables[12].Rows[i][SharedFunctions.GetResourceString("ParcelID")].ToString();
                        dr[1] = this.parcelTrackingDetailsDataSet.Tables[12].Rows[i][SharedFunctions.GetResourceString("SaleParcelID")].ToString();
                        dr[2] = this.parcelTrackingDetailsDataSet.Tables[12].Rows[i][SharedFunctions.GetResourceString("SaleID")].ToString();
                        dr[3] = this.parcelTrackingDetailsDataSet.Tables[12].Rows[i]["Type"].ToString();
                        parceltable.Rows.Add(dr);
                    }
                    else
                    {
                    }
                }
                else
                {
                    if (this.parcelTrackingDetailsDataSet.Tables[12].Rows[i][SharedFunctions.GetResourceString("GridEmptyRecord")].ToString() == SharedFunctions.GetResourceString("False"))
                    {
                        dr[0] = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows[i][SharedFunctions.GetResourceString("ParcelID")].ToString();
                        dr[1] = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows[i][SharedFunctions.GetResourceString("SaleParcelID")].ToString();
                        dr[2] = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows[i][SharedFunctions.GetResourceString("SaleID")].ToString();
                        dr[3] = this.parcelTrackingDetailsDataSet.Tables[12].Rows[i]["Type"].ToString();
                        parceltable.Rows.Add(dr);
                    }
                    else
                    {
                    }
                }
            }

            parceltable.AcceptChanges();
            if (this.tempDataSet.Tables.Count > 0)
            {
                this.tempDataSet.Tables.Remove(SharedFunctions.GetResourceString("SaleAdvisoryTable"));
            }

            this.tempDataSet.Tables.Add(parceltable.Copy());
            this.tempDataSet.Tables[0].TableName = SharedFunctions.GetResourceString("SaleAdvisoryTable");
        }

        /// <summary>
        /// BindOwnerGridView
        /// </summary>
        private void BindOwnerGridView()
        {
            if (this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails != null)
            {
                this.OwnerGridView.DataSource = this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.DefaultView;
                this.ConvertTableToXML(this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Rows.Count, SharedFunctions.GetResourceString("SaleOwnerID"));

                //// Adding Empty Row While Loading the OwnerDetails
                if (this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Rows.Count > 0)
                {
                    int emptyRecordCount = 0;
                    ////VerticalScroll
                    for (int i = 0; i <= this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Rows.Count - 1; i++)
                    {
                        if (this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Rows[i][SharedFunctions.GetResourceString("GridEmptyRecord")].ToString() == SharedFunctions.GetResourceString("False"))
                        {
                            emptyRecordCount++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (emptyRecordCount >= 5)
                    {
                        this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Rows.InsertAt(this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Newf29550ListOwnerDetailsRow(), this.OwnerGridView.Rows.Count);
                        this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Rows[this.OwnerGridView.Rows.Count - 1][SharedFunctions.GetResourceString("GridEmptyRecord")] = true;
                        this.OwnerGridView.ScrollBars = ScrollBars.Vertical;
                        this.OwnerGridVScrollBar.Visible = false;
                        ////this.OwnerGridView.Columns[SharedFunctions.GetResourceString("Location")].Width = 156;
                    }
                    else
                    {
                        this.OwnerGridVScrollBar.Visible = true;
                        this.OwnerGridVScrollBar.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// LockControl
        /// </summary>
        /// <param name="lockKey">lockKey</param>
        private void LockControl(bool lockKey)
        {
            this.DeedTypeTextBox.LockKeyPress = lockKey;
            this.BookPageTextBox.LockKeyPress = lockKey;
            this.SalesFileTextBox.LockKeyPress = lockKey;
            this.SalDateTextBox.LockKeyPress = lockKey;
            this.TaxDistrictTextBox.LockKeyPress = true;
            this.SaleNoteTextBox.LockKeyPress = lockKey;
            this.OriginalSalePriceTextBox.LockKeyPress = lockKey;
            this.OriginalSFTextBox.LockKeyPress = lockKey;
            this.AdjustedPPTextBox.LockKeyPress = lockKey;
            this.AdjustedFinanceTextBox.LockKeyPress = lockKey;
            this.AdjTimeSaleTextBox.LockKeyPress = lockKey;
            this.AdjOtherTextBox.LockKeyPress = lockKey;
            this.AdjustedSFTextBox.LockKeyPress = lockKey;
            this.EstimateatSaleTextBox.LockKeyPress = lockKey;
            this.CurrentEstimateTextBox.LockKeyPress = lockKey;
            this.AppraisalEstimateTextBox.LockKeyPress = lockKey;
        }

        #endregion UserDefine Methods

        #region CalendarEvents
        /// <summary>
        /// SaleDateCalendar Click Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SaleDateCalendar_Click(object sender, EventArgs e)
        {
            this.ParentForm.AcceptButton = null;
            try
            {
                SaleDateCalendar.SendToBack();
                this.SaleDateTimePicker.BringToFront();
                if (!string.IsNullOrEmpty(this.SalDateTextBox.Text.Trim()))
                {
                    this.SaleDateTimePicker.Value = Convert.ToDateTime(this.SaleDateTimePicker.Text);
                }
                else
                {
                    this.SaleDateTimePicker.Value = DateTime.Today;
                }

                SendKeys.Send("{F4}");
                SaleDateTimePicker.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// SaleDateTimePicker CloseUp Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SaleDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.SalDateTextBox.Text = this.SaleDateTimePicker.Text;
                this.SalDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// SaleDateTimePicker keyPress Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SaleDateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
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
        #endregion CalendarEvents

        #region ownerGrid Events
        /// <summary>
        /// OwnerGridView_RowEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void OwnerGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    this.OwnerGridView.Rows[e.RowIndex].ReadOnly = true;
                    for (int i = 0; i < this.OwnerGridView.Rows.Count; i++)
                    {
                        TerraScanTextAndImageCell ownerCell = (TerraScanTextAndImageCell)this.OwnerGridView[SharedFunctions.GetResourceString("OwnerField"), i];

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
                        //// this.OwnerGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        //// this.OwnerGridView.RefreshEdit();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// OwnerGridView_DataBindingComplete
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
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
        /// To Select Owner from Owner Selection Form
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void OwnerGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                ////To call Owner Selction Form
                if (e.ColumnIndex != -1)
                {
                    if ((this.OwnerGridView.Columns[e.ColumnIndex].Name == SharedFunctions.GetResourceString("OwnerField")) && (e.RowIndex >= 0))
                    {
                        if (this.OwnerGridView.Rows[e.RowIndex].ReadOnly && string.IsNullOrEmpty(this.OwnerGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("SaleIdField")].Value.ToString()) && !string.IsNullOrEmpty(this.OwnerGridView.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value.ToString()))
                        {
                            if ((e.X >= 153) && (e.X <= 171) && (e.Y >= 1) && (e.Y <= (22 - 1)))
                            {
                                Form parcelF9101 = new Form();
                                parcelF9101 = this.form29550Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9101, null, this.form29550Controller.WorkItem);

                                if (parcelF9101 != null)
                                {
                                    if (parcelF9101.ShowDialog() == DialogResult.Yes)
                                    {
                                        this.EditRecord();
                                        this.ownerId = Convert.ToInt32(TerraScanCommon.GetValue(parcelF9101, "MasterNameOwnerId"));
                                        this.ownerDetailDataSet = this.form29550Controller.WorkItem.F15010_GetOwnerDetails(this.ownerId);
                                        if (this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows.Count > 0)
                                        {
                                            ////Check DuplicateOwner
                                            DataView ownerDataView = null;
                                            if (!this.parcelTrackingDetailsDataSet.Tables.Contains(SharedFunctions.GetResourceString("SaleConfidenceTable")))
                                            {
                                                ownerDataView = new DataView(this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails);
                                            }
                                            else
                                            {
                                                ownerDataView = new DataView(this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")]);
                                            }

                                            string s = "OwnerID=" + this.ownerId + " AND " + this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.IsBuyerColumn.ColumnName + "='Grantee'";
                                            ownerDataView.RowFilter = s;
                                            if (ownerDataView.Count <= 0)
                                            {
                                                if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && !string.IsNullOrEmpty(this.OwnerGridView[SharedFunctions.GetResourceString("OwnerField"), e.RowIndex].Value.ToString()))
                                                {
                                                    int id = this.ConvertStringtoInt(this.OwnerGridView[SharedFunctions.GetResourceString("OwnerIdField"), e.RowIndex].Value.ToString());
                                                    if (!this.parcelTrackingDetailsDataSet.Tables.Contains(SharedFunctions.GetResourceString("SaleConfidenceTable")))
                                                    {
                                                        F29550ParcelSaleTracking.f29550ListOwnerDetailsRow dr = (F29550ParcelSaleTracking.f29550ListOwnerDetailsRow)this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Rows[e.RowIndex];
                                                        dr.Owner = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.NameOwnerColumn].ToString().Trim();
                                                        dr.OwnerID = this.ownerId.ToString();
                                                        dr.IsBuyer = SharedFunctions.GetResourceString("Grantee");
                                                        dr.Address1 = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.Address1Column].ToString() + ", " + this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.Address2Column].ToString();
                                                        dr.Location = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.CityColumn].ToString() + ", " + this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.StateColumn].ToString();
                                                        this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Rows[e.RowIndex].AcceptChanges();
                                                    }
                                                    else
                                                    {
                                                        DataRow ownerTempRow = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Rows[e.RowIndex];
                                                        if (ownerTempRow != null)
                                                        {
                                                            ownerTempRow[SharedFunctions.GetResourceString("OwnerColumn")] = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.NameOwnerColumn].ToString().Trim();
                                                            ownerTempRow[SharedFunctions.GetResourceString("SaleOwnerID")] = this.ownerId.ToString();
                                                            ownerTempRow[SharedFunctions.GetResourceString("IsBuyer")] = SharedFunctions.GetResourceString("Grantee");
                                                            ownerTempRow[SharedFunctions.GetResourceString("Address1")] = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.Address1Column].ToString() + ", " + this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.Address2Column].ToString();
                                                            ownerTempRow[SharedFunctions.GetResourceString("Location")] = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.CityColumn].ToString() + ", " + this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.StateColumn].ToString();
                                                        }
                                                    }

                                                    DataRow[] tempRow = this.tempDataTable.Select("OwnerID=" + id);
                                                    if (tempRow.Length > 0)
                                                    {
                                                        this.tempFlag = true;
                                                        tempRow[0][SharedFunctions.GetResourceString("OwnerColumn")] = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.NameOwnerColumn].ToString().Trim();
                                                        tempRow[0][SharedFunctions.GetResourceString("SaleOwnerID")] = this.ownerId.ToString();
                                                        tempRow[0][SharedFunctions.GetResourceString("IsBuyer")] = SharedFunctions.GetResourceString("Grantee");
                                                        tempRow[0][SharedFunctions.GetResourceString("Address1")] = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.Address1Column].ToString() + ", " + this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.Address2Column].ToString();
                                                        tempRow[0][SharedFunctions.GetResourceString("Location")] = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.CityColumn].ToString() + ", " + this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.StateColumn].ToString();
                                                        this.tempDataTable.AcceptChanges();
                                                    }
                                                }

                                                //// To add Selected Owner to OwnerGrid
                                                DataRow ownerGridRow = this.tempDataTable.NewRow();
                                                this.OwnerGridView[SharedFunctions.GetResourceString("OwnerField"), e.RowIndex].Value = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.NameOwnerColumn].ToString().Trim();
                                                ownerGridRow[2] = this.OwnerGridView[SharedFunctions.GetResourceString("OwnerField"), e.RowIndex].Value;
                                                this.OwnerGridView[SharedFunctions.GetResourceString("IsBuyer"), e.RowIndex].Value = SharedFunctions.GetResourceString("Grantee");
                                                ownerGridRow[3] = this.OwnerGridView[SharedFunctions.GetResourceString("IsBuyer"), e.RowIndex].Value;
                                                this.OwnerGridView[SharedFunctions.GetResourceString("OwnerAddress"), e.RowIndex].Value = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.Address1Column].ToString() + ", " + this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.Address2Column].ToString();
                                                ownerGridRow[4] = this.OwnerGridView[SharedFunctions.GetResourceString("OwnerAddress"), e.RowIndex].Value;
                                                this.OwnerGridView[SharedFunctions.GetResourceString("Location"), e.RowIndex].Value = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.CityColumn].ToString() + ", " + this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.StateColumn].ToString();
                                                ownerGridRow[5] = this.OwnerGridView[SharedFunctions.GetResourceString("Location"), e.RowIndex].Value;
                                                this.OwnerGridView[1, e.RowIndex].Value = this.parcelId.ToString();
                                                ownerGridRow[6] = this.OwnerGridView[1, e.RowIndex].Value;
                                                this.OwnerGridView[SharedFunctions.GetResourceString("OwnerIdField"), e.RowIndex].Value = this.ownerId.ToString();
                                                ownerGridRow[0] = this.OwnerGridView[SharedFunctions.GetResourceString("OwnerIdField"), e.RowIndex].Value;

                                                ////Add Owners to Temp Table 
                                                if (!this.tempFlag)
                                                {
                                                    this.tempDataTable.Rows.Add(ownerGridRow);
                                                    this.tempDataTable.Copy();
                                                    this.tempDataTable.AcceptChanges();
                                                }

                                                this.tempFlag = false;
                                            }
                                            else
                                            {
                                                if (MessageBox.Show(SharedFunctions.GetResourceString("ExsistOwner"), "Terrascan T2 - Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                                {
                                                    return;
                                                }
                                            }

                                            OwnerGridView.ScrollBars = ScrollBars.Vertical;
                                        }
                                        ////Add Empty Row to Owner Grid
                                        if (!this.parcelTrackingDetailsDataSet.Tables.Contains(SharedFunctions.GetResourceString("SaleConfidenceTable")))
                                        {
                                            if ((((e.RowIndex) + 1) == this.OwnerGridView.Rows.Count))
                                            {
                                                if ((!string.IsNullOrEmpty(this.OwnerGridView.Rows[e.RowIndex].Cells[3].Value.ToString().Trim())))
                                                {
                                                    this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Rows.InsertAt(this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Newf29550ListOwnerDetailsRow(), this.OwnerGridView.Rows.Count);
                                                    this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Rows[this.OwnerGridView.Rows.Count - 1][SharedFunctions.GetResourceString("GridEmptyRecord")] = SharedFunctions.GetResourceString("True");
                                                    this.OwnerGridVScrollBar.Visible = false;
                                                    this.ConvertTableToXML(this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Rows.Count, SharedFunctions.GetResourceString("SaleOwnerID"));
                                                    ////this.OwnerGridView.Columns[SharedFunctions.GetResourceString("Location")].Width = 158;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ////int emptyRecordCount = 0;

                                            ////VerticalScroll
                                            ////for (int i = 0; i < this.parcelTrackingDetailsDataSet.Tables["Table1"].Rows.Count; i++)
                                            ////{
                                            ////    if (this.parcelTrackingDetailsDataSet.Tables["Table1"].Rows[i]["EmptyRecord$"].ToString() == "False" && !string.IsNullOrEmpty(this.parcelTrackingDetailsDataSet.Tables["Table1"].Rows[i]["OwnerID"].ToString()))
                                            ////    {
                                            ////        emptyRecordCount++;
                                            ////    }
                                            ////    else
                                            ////    {
                                            ////        break;
                                            ////    }
                                            ////}

                                            ////if (emptyRecordCount >= 5)
                                            ////{
                                            if ((((e.RowIndex) + 1) == this.OwnerGridView.Rows.Count))
                                            {
                                                this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Rows.InsertAt(this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].NewRow(), this.OwnerGridView.Rows.Count);
                                                this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Rows[this.OwnerGridView.Rows.Count - 1][SharedFunctions.GetResourceString("GridEmptyRecord")] = SharedFunctions.GetResourceString("True");
                                                this.OwnerGridVScrollBar.Visible = false;
                                                //// this.OwnerGridView.Columns[SharedFunctions.GetResourceString("Location")].Width = 158;
                                            }
                                            ////}
                                        }

                                        this.EditRecord();
                                        this.DisableGridSorting(this.ParcelDataGridView, true);
                                        this.DisableGridSorting(this.OwnerGridView, true);
                                    }
                                }
                            }
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
        /// OwnerGridView_CellEndEdit
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void OwnerGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((((e.RowIndex) + 1) == this.OwnerGridView.Rows.Count) && (e.ColumnIndex == 3))
                {
                    if ((!string.IsNullOrEmpty(this.OwnerGridView.Rows[e.RowIndex].Cells[3].Value.ToString().Trim())))
                    {
                        this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Rows.InsertAt(this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Newf29550ListOwnerDetailsRow(), this.OwnerGridView.Rows.Count);
                        this.OwnerGridVScrollBar.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// OwnerGridView_CellFormatting
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void OwnerGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.OwnerGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("IsBuyer")].Value != null && !string.IsNullOrEmpty(this.OwnerGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("IsBuyer")].Value.ToString()))
                {
                    if (this.OwnerGridView.Columns[e.ColumnIndex].Name == SharedFunctions.GetResourceString("OwnerField") && this.OwnerGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("IsBuyer")].Value.ToString() == SharedFunctions.GetResourceString("Grantor"))
                    {
                        this.OwnerGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("IsBuyer")].Style.ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
                    }
                    else
                    {
                        ////this.OwnerGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("IsBuyer")].Style.ForeColor = System.Drawing.Color.Black;
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion ownerGrid Events

        #region ParcelGrid Events
        /// <summary>
        /// ParcelDataGridView_RowEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    ////this.ParcelDataGridView.ReadOnly = true;

                    for (int i = 0; i < this.ParcelDataGridView.Rows.Count; i++)
                    {
                        TerraScanLinkAndImageCell folderCell = (TerraScanLinkAndImageCell)this.ParcelDataGridView[SharedFunctions.GetResourceString("ParcelgridColumn"), i];
                        folderCell.ImagexLocation = 115;
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

                    this.currentCoulmnindex = e.ColumnIndex;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// ParcelDataGridView_DataBindingComplete
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
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
        /// ParcelDataGridView_CellMouseClick
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////this.rowIndex = e.RowIndex;
                this.scrollEnter = true;
                int previousRow = 0;
                if (e.RowIndex != 0)
                {
                    previousRow = e.RowIndex - 1;
                }

                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Button != MouseButtons.Right)
                {
                    if (!string.IsNullOrEmpty(this.ParcelDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString()))
                    {
                        ////To call Parcel Selection Form
                        if (this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("ParcelID")].Value.ToString() == this.parcelId.ToString())
                        {
                            if (e.ColumnIndex != 4)
                            {
                                this.ParcelDataGridView[e.ColumnIndex, e.RowIndex].ReadOnly = true;
                            }
                            else
                            {
                                this.ParcelDataGridView[e.ColumnIndex, e.RowIndex].ReadOnly = false;
                            }
                        }
                    }
                    else
                    {
                        this.ParcelDataGridView[e.ColumnIndex, e.RowIndex].ReadOnly = true;
                    }

                    ////else
                    ////{
                    ////    this.ParcelDataGridView[e.ColumnIndex, e.RowIndex].ReadOnly = true;
                    ////}

                    int totalcount = this.ParcelDataGridView.OriginalRowCount;

                    if (e.RowIndex == totalcount - 1)
                    {
                        if (!string.IsNullOrEmpty(this.ParcelDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()))
                        {
                            if (e.ColumnIndex != 4)
                            {
                                this.ParcelDataGridView[e.ColumnIndex, e.RowIndex].ReadOnly = true;
                            }
                            else
                            {
                                this.ParcelDataGridView[e.ColumnIndex, e.RowIndex].ReadOnly = false;
                            }
                        }
                        else
                        {
                            this.ParcelDataGridView[e.ColumnIndex, e.RowIndex].ReadOnly = true;
                        }
                    }

                    ////if ((e.ColumnIndex == 3 || e.ColumnIndex == 1) && (e.RowIndex >= 0) && (e.ColumnIndex >= 3 || e.ColumnIndex >= 1))
                    ////{
                    ////if (this.ParcelDataGridView.Rows[e.RowIndex].Cells["ParcelID"].Value.ToString() != this.parcelId.ToString() && (this.ParcelDataGridView.Rows[e.RowIndex].Cells["Column1"].Value == null||string.IsNullOrEmpty(this.ParcelDataGridView.Rows[e.RowIndex].Cells["Column1"].Value.ToString())))
                    ////{
                    ////    if (this.ParcelDataGridView.Rows[e.RowIndex].Cells["Column1"].Value != null && (!string.IsNullOrEmpty(this.ParcelDataGridView.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value.ToString())) && this.ParcelDataGridView.Rows[e.RowIndex].Cells["ParcelID"].Value.ToString() != this.parcelId.ToString())
                    ////{
                    if ((this.ParcelDataGridView.Columns[e.ColumnIndex].Name == SharedFunctions.GetResourceString("ParcelgridColumn")) && (e.RowIndex >= 0) && this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("ParcelID")].Value.ToString() != this.parcelId.ToString())
                    {
                        ////if (this.OwnerGridView.Rows[e.RowIndex].ReadOnly && (this.OwnerGridView.Rows[e.RowIndex].Cells["OwnerField"].Value.ToString() == null || string.IsNullOrEmpty(this.OwnerGridView.Rows[e.RowIndex].Cells["OwnerField"].Value.ToString())) && this.OwnerGridView.Rows[e.RowIndex].Cells["SaleIDField"].Value.ToString() == string.Empty)
                        ////if (this.ParcelDataGridView.Rows[e.RowIndex].ReadOnly && (this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("SaleID")].Value.ToString() == string.Empty || this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("SaleID")].Value.ToString().Equals(SharedFunctions.GetResourceString("Zero"))) && !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[previousRow].Cells[e.ColumnIndex].Value.ToString()))
                        if ((string.IsNullOrEmpty(this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("SaleID")].Value.ToString()) || this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("SaleID")].Value.ToString().Equals(SharedFunctions.GetResourceString("Zero"))) && !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[previousRow].Cells[e.ColumnIndex].Value.ToString()))
                        {
                            if ((e.X >= 115) && (e.X <= 136) && (e.Y >= 1) && (e.Y <= (22 - 1)))
                            {
                                Form subfundForm = new Form();
                                object[] optionalParameter = new object[] { };
                                subfundForm = this.form29550Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1401, optionalParameter, this.form29550Controller.WorkItem);
                                if (subfundForm != null)
                                {
                                    if (subfundForm.ShowDialog() == DialogResult.OK)
                                    {
                                        this.ownerFlag = false;
                                        this.newParcelId = this.ConvertStringtoInt(TerraScanCommon.GetValue(subfundForm, SharedFunctions.GetResourceString("ParcelID")));

                                        if (this.newParcelId > 0)
                                        {
                                            ////Remove Duplicate Record
                                            DataView parcelDataView;
                                            if (this.parcelTrackingDetailsDataSet.Tables.Contains(SharedFunctions.GetResourceString("SaleAdvisoryTable")))
                                            {
                                                parcelDataView = new DataView(this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")]);
                                            }
                                            else
                                            {
                                                parcelDataView = new DataView(this.parcelTrackingDetailsDataSet.Tables[12]);
                                            }

                                            parcelDataView.RowFilter = "ParcelID=" + this.newParcelId;
                                            if (parcelDataView.Count <= 0)
                                            {
                                                if (this.parcelTrackingDetailsDataSet.Tables.Contains(SharedFunctions.GetResourceString("SaleAdvisoryTable")))
                                                {
                                                    if (!string.IsNullOrEmpty(this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows[e.RowIndex][SharedFunctions.GetResourceString("ParcelSaleNumber")].ToString()))
                                                    {
                                                        this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows.RemoveAt(e.RowIndex);
                                                    }

                                                    this.ConvertTableToXML(this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows.Count, SharedFunctions.GetResourceString("ParcelID"));
                                                }
                                                else
                                                {
                                                    if (!string.IsNullOrEmpty(this.parcelTrackingDetailsDataSet.Tables[12].Rows[e.RowIndex][SharedFunctions.GetResourceString("ParcelSaleNumber")].ToString()))
                                                    {
                                                        this.parcelTrackingDetailsDataSet.Tables[12].Rows.RemoveAt(e.RowIndex);
                                                    }

                                                    this.ConvertTableToXML(this.parcelTrackingDetailsDataSet.Tables[12].Rows.Count, SharedFunctions.GetResourceString("ParcelID"));
                                                }

                                                this.parcelTrackingDetailsDataSet = this.F29550Control.WorkItem.F29550_GetParcelDetails(this.tempDataSet.GetXml(), this.newParcelId, this.saleId);

                                                this.editParcelId = 0;
                                                if (this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows.Count > 0)
                                                {
                                                    this.ParcelDataGridView.AutoGenerateColumns = true;
                                                    this.ParcelDataGridView.Columns[SharedFunctions.GetResourceString("ParcelgridColumn")].DataPropertyName = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Columns[1].ColumnName.ToString();
                                                    this.ParcelDataGridView.DataSource = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].DefaultView;

                                                    //////// Get ColumnCount 
                                                    ////int emptyRecordCount = 0;

                                                    ////////VerticalScroll
                                                    ////for (int i = 0; i < this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows.Count; i++)
                                                    ////{
                                                    ////    if (this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows[i][SharedFunctions.GetResourceString("GridEmptyRecord")].ToString() == SharedFunctions.GetResourceString("False"))
                                                    ////    {
                                                    ////        emptyRecordCount++;
                                                    ////    }
                                                    ////    else
                                                    ////    {
                                                    ////        break;
                                                    ////    }
                                                    ////}

                                                    ////this.CreateTextBox(this.parcelTrackingDetailsDataSet.Tables["Table"].Columns.Count, emptyRecordCount);
                                                    this.EditRecord();
                                                }

                                                if (this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Rows.Count > 0)
                                                {
                                                    if (this.tempDataTable.Rows.Count > 0)
                                                    {
                                                        this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Merge(this.tempDataTable);
                                                    }

                                                    //// this.OwnerGridView.DataSource = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")];
                                                    if (this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Rows.Count >= 5)
                                                    {
                                                        this.OwnerGridView.ScrollBars = ScrollBars.Vertical;
                                                        this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Rows.InsertAt(this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].NewRow(), this.OwnerGridView.Rows.Count);
                                                        ////this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Rows[this.OwnerGridView.Rows.Count - 1][SharedFunctions.GetResourceString("GridEmptyRecord")] = SharedFunctions.GetResourceString("True");
                                                        this.OwnerGridVScrollBar.Visible = false;
                                                        ////this.OwnerGridView.Columns[SharedFunctions.GetResourceString("Location")].Width = 156;
                                                    }

                                                    this.OwnerGridView.DataSource = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].DefaultView;
                                                }
                                            }
                                            else
                                            {
                                                if (MessageBox.Show(SharedFunctions.GetResourceString("ExsistParcel"), "Terrascan T2 - Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                                {
                                                    return;
                                                }
                                            }
                                        }

                                        this.CustomizeListingParcelDetails();
                                        this.DisableGridSorting(this.ParcelDataGridView, true);
                                        this.DisableGridSorting(this.OwnerGridView, true);
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
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// OwnerGridVScrollBar_Scroll
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void OwnerGridVScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                ////int rowsheight = 0;
                ////int scrollHeight = 0;
                ////rowsheight = this.OwnerGridView.Rows.GetRowsHeight(DataGridViewElementStates.None);
                ////scrollHeight = this.ConvertStringtoInt(this.OwnerGridVScrollBar.MaximumSize.ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Set Grid Horizontal value to ParcelGridHScrollBar
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelGridHScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                this.ParcelDataGridView.HorizontalScrollingOffset = e.NewValue;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ParcelDataGridView_CellFormatting
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != 4)
                {
                    if (this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("ParcelgridColumn")].Value != null && !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("ParcelgridColumn")].Value.ToString()))
                    {
                        if (this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("ParcelID")].Value.ToString().Equals(this.parcelId.ToString()))
                        {
                            e.CellStyle.Font = new Font(SharedFunctions.GetResourceString("Arial"), 8, ((System.Drawing.FontStyle)(System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline)));
                        }

                        //// Undeline To ParcelNumber
                        if (this.ParcelDataGridView.Columns[e.ColumnIndex].Name == SharedFunctions.GetResourceString("ParcelgridColumn"))
                        {
                            if (this.ParcelDataGridView.Rows[e.RowIndex].Selected || this.ParcelDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                            {
                                ////(this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("ParcelgridColumn")] as TerraScanLinkAndImageCell).ActiveLinkColor = System.Drawing.Color.White;
                                (this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("ParcelgridColumn")] as TerraScanLinkAndImageCell).LinkColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                (this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("ParcelgridColumn")] as TerraScanLinkAndImageCell).LinkColor = System.Drawing.Color.Blue;
                            }

                            if (this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("ParcelID")].Value.ToString() != (this.parcelId.ToString()))
                            {
                                this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("ParcelgridColumn")].Style.Font = new Font(SharedFunctions.GetResourceString("Arial"), 8, ((System.Drawing.FontStyle)(System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline)));
                            }
                        }
                    }

                    if (this.ParcelDataGridView.Columns[e.ColumnIndex].Name != SharedFunctions.GetResourceString("ParcelID") && this.ParcelDataGridView.Columns[e.ColumnIndex].Name != SharedFunctions.GetResourceString("ParcelgridColumn") && this.ParcelDataGridView.Columns[e.ColumnIndex].Name != SharedFunctions.GetResourceString("SaleParcelID") && this.ParcelDataGridView.Columns[e.ColumnIndex].Name != SharedFunctions.GetResourceString("SaleID"))
                    {
                        if (e.RowIndex < 0)
                        {
                            return;
                        }

                        if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                        {
                            string val = e.Value.ToString().Trim();
                            Decimal outDecimal;
                            if (Decimal.TryParse(val, out outDecimal))
                            {
                                string currenTcellvalue;
                                if (outDecimal.ToString().Contains("."))
                                {
                                    currenTcellvalue = outDecimal.ToString("#0.00");
                                }
                                else
                                {
                                    currenTcellvalue = outDecimal.ToString("#,##0");
                                }

                                e.Value = string.Concat(currenTcellvalue);
                                e.FormattingApplied = true;
                            }
                            else
                            {
                                e.Value = "0";
                            }
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
        /// ParcelDataGridView_MouseUp
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelDataGridView_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                DataGridView.HitTestInfo hti = this.ParcelDataGridView.HitTest(e.X, e.Y);
                this.rowIndex = hti.RowIndex;
                if (e.X <= 747)
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        if (this.slicePermissionField.deletePermission)
                        {
                            if (this.rowIndex >= 0 && !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[this.rowIndex].Cells[SharedFunctions.GetResourceString("ParcelgridColumn")].Value.ToString()) && this.ParcelDataGridView.Rows[this.rowIndex].Cells[SharedFunctions.GetResourceString("ParcelID")].Value.ToString() != this.parcelId.ToString() && this.scrollEnter)
                            {
                                this.rowBackColor = this.ParcelDataGridView.Rows[this.rowIndex].DefaultCellStyle.BackColor;
                                TerraScanCommon.SetDataGridViewPosition(this.ParcelDataGridView, this.rowIndex);
                                this.selectedComponentMenuStrip.Show(this.ParcelDataGridView, new Point(e.X, e.Y));
                            }
                        }
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ParcelDataGridView_Scroll
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelDataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation.ToString() != "VerticalScroll")
            {
                this.SummaryPanel.AutoScrollPosition = new Point(e.NewValue, 0);
            }
        }

        /// <summary>
        /// ParcelDataGridView_CellContentClick
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (this.ParcelDataGridView.Columns[e.ColumnIndex].Name == SharedFunctions.GetResourceString("ParcelgridColumn"))
                    {
                        ////if (this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("ParcelgridColumn")].Value.ToString() != null || !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("ParcelgridColumn")].Value.ToString()) || this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("ParcelgridColumn")].Value.ToString() != string.Empty)
                        if (this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("ParcelgridColumn")].Value.ToString() != null || !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("ParcelgridColumn")].Value.ToString()))
                        {
                            int parcelHeaderId = this.ConvertStringtoInt(this.ParcelDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("ParcelID")].Value.ToString());
                            ////this.Cursor = Cursors.WaitCursor;
                            if (parcelHeaderId > 0)
                            {
                                FormInfo formInfo;
                                formInfo = TerraScanCommon.GetFormInfo(20000);
                                formInfo.optionalParameters = new object[1];
                                formInfo.optionalParameters[0] = parcelHeaderId;
                                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                            }
                        }
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion ParcelGrid Events

        #region MenuStrip
        /// <summary>
        /// Selecteds the component menu strip_ item clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void SelectedComponentMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (this.rowIndex >= 0)
                {
                    if (e.ClickedItem.Text == SharedFunctions.GetResourceString("Delete"))
                    {
                        this.menuStripItem = e.ClickedItem.Text;
                        this.selectedComponentMenuStrip.Visible = false;
                        if (this.parcelTrackingDetailsDataSet.Tables.Contains(SharedFunctions.GetResourceString("SaleAdvisoryTable")))
                        {
                            if (this.ConvertStringtoInt(this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows[this.rowIndex][SharedFunctions.GetResourceString("ParcelID")].ToString()) != this.parcelId)
                            {
                                int deleteRow = this.ConvertStringtoInt(this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows[this.rowIndex][SharedFunctions.GetResourceString("ParcelID")].ToString());
                                this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows.RemoveAt(this.rowIndex);

                                ////Load ParcelGrid After Delete
                                this.ParcelDataGridView.AutoGenerateColumns = true;
                                this.ParcelDataGridView.Columns[SharedFunctions.GetResourceString("ParcelgridColumn")].DataPropertyName = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Columns[1].ColumnName.ToString();
                                this.ParcelDataGridView.DataSource = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].DefaultView;

                                ////Set Empty Row To parcelGrid
                                /* int emptyRecordCount = 0;
                                 ////VerticalScroll
                                 for (int i = 0; i < this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows.Count; i++)
                                 {
                                     if (this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows[i][SharedFunctions.GetResourceString("GridEmptyRecord")].ToString() == SharedFunctions.GetResourceString("False") && !string.IsNullOrEmpty(this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows[i][SharedFunctions.GetResourceString("ParcelID")].ToString()))
                                     {
                                         emptyRecordCount++;
                                     }
                                     else
                                     {
                                         break;
                                     }
                                 }

                                 if (emptyRecordCount >= 5)
                                 {
                                     if (this.rowIndex == this.ParcelDataGridView.Rows.Count)
                                     {
                                         this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows.InsertAt(this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].NewRow(), ParcelDataGridView.Rows.Count);
                                         this.ParcelGridVScrollBar.Visible = false;
                                     }
                                 }
                                 else
                                 {
                                     this.ParcelGridVScrollBar.Visible = true;
                                     this.ParcelGridVScrollBar.Enabled = false;
                                 }*/

                                ////this.folderFlag = false;
                                this.DeleteOwner(this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleAdvisoryTable")].Rows.Count, deleteRow);
                                ////this.folderFlag = true;

                                ////Load OwnerGrid After Delete
                                this.parcelTrackingDetailsDataSet = this.form29550Controller.WorkItem.F29550_GetParcelDetails(this.tempDataSet.GetXml(), 0, this.saleId);
                                if (this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Rows.Count > 0)
                                {
                                    if (this.tempDataTable.Rows.Count > 0)
                                    {
                                        this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Merge(this.tempDataTable);
                                    }

                                    this.OwnerGridView.DataSource = this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].DefaultView;

                                    ////Add Empty Row to OwnerGrid
                                    int recordCount = 0;
                                    ////VerticalScroll
                                    for (int i = 0; i < this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Rows.Count; i++)
                                    {
                                        if (this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Rows[i][SharedFunctions.GetResourceString("GridEmptyRecord")].ToString() == SharedFunctions.GetResourceString("False"))
                                        {
                                            recordCount++;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                    if (recordCount >= 5)
                                    {
                                        this.OwnerGridView.ScrollBars = ScrollBars.Vertical;
                                        this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Rows.InsertAt(this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].NewRow(), this.OwnerGridView.Rows.Count);
                                        this.parcelTrackingDetailsDataSet.Tables[SharedFunctions.GetResourceString("SaleConfidenceTable")].Rows[this.OwnerGridView.Rows.Count - 1][SharedFunctions.GetResourceString("GridEmptyRecord")] = true;
                                        this.OwnerGridVScrollBar.Visible = false;
                                    }
                                    else
                                    {
                                        this.OwnerGridVScrollBar.Visible = true;
                                        this.OwnerGridVScrollBar.Enabled = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (this.ConvertStringtoInt(this.parcelTrackingDetailsDataSet.Tables[this.parcelTrackingDetailsDataSet.Tables.Count - 1].Rows[this.rowIndex][SharedFunctions.GetResourceString("ParcelID")].ToString()) != this.parcelId)
                            {
                                int deleteRow = this.ConvertStringtoInt(this.parcelTrackingDetailsDataSet.Tables[this.parcelTrackingDetailsDataSet.Tables.Count - 1].Rows[this.rowIndex][SharedFunctions.GetResourceString("ParcelID")].ToString());
                                this.parcelTrackingDetailsDataSet.Tables[this.parcelTrackingDetailsDataSet.Tables.Count - 1].Rows.RemoveAt(this.rowIndex);
                                this.DeleteOwner(this.parcelTrackingDetailsDataSet.Tables[this.parcelTrackingDetailsDataSet.Tables.Count - 1].Rows.Count, deleteRow);

                                ////Load ParcelGrid After Delete
                                this.parcelTrackingDetailsDataSet.AcceptChanges();
                                this.ParcelDataGridView.AutoGenerateColumns = true;
                                this.ParcelDataGridView.Columns[SharedFunctions.GetResourceString("ParcelgridColumn")].DataPropertyName = this.parcelTrackingDetailsDataSet.Tables[this.parcelTrackingDetailsDataSet.Tables.Count - 1].Columns[SharedFunctions.GetResourceString("ParcelSaleNumber")].ColumnName.ToString();
                                this.ParcelDataGridView.DataSource = this.parcelTrackingDetailsDataSet.Tables[this.parcelTrackingDetailsDataSet.Tables.Count - 1].DefaultView;

                                /*int emptyRecordCount = 0;
                                ////VerticalScroll
                                for (int i = 0; i < this.parcelTrackingDetailsDataSet.Tables[this.parcelTrackingDetailsDataSet.Tables.Count - 1].Rows.Count; i++)
                                {
                                    if (this.parcelTrackingDetailsDataSet.Tables[this.parcelTrackingDetailsDataSet.Tables.Count - 1].Rows[i][SharedFunctions.GetResourceString("GridEmptyRecord")].ToString() == SharedFunctions.GetResourceString("False"))
                                    {
                                        emptyRecordCount++;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                if (emptyRecordCount >= 5)
                                {
                                    this.parcelTrackingDetailsDataSet.Tables[this.parcelTrackingDetailsDataSet.Tables.Count - 1].Rows.InsertAt(this.parcelTrackingDetailsDataSet.Tables[this.parcelTrackingDetailsDataSet.Tables.Count - 1].NewRow(), this.ParcelDataGridView.Rows.Count);
                                    this.ParcelGridVScrollBar.Visible = false;
                                }
                                else
                                {
                                    this.ParcelGridVScrollBar.Visible = true;
                                    this.ParcelGridVScrollBar.Enabled = false;
                                }*/

                                ////this.folderFlag = false;
                                ////DataTable dt = new DataTable();
                                ////this.folderFlag = true;
                                ////Load OwnerGrid After Delete
                                this.parcelTrackingDetailsDataSet = this.form29550Controller.WorkItem.F29550_GetParcelDetails(this.tempDataSet.GetXml(), 0, this.saleId);
                                if (!this.parcelTrackingDetailsDataSet.Tables.Contains(SharedFunctions.GetResourceString("SaleConfidenceTable")))
                                {
                                    if (this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Rows.Count > 0)
                                    {
                                        if (this.tempDataTable.Rows.Count > 0)
                                        {
                                            this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Merge(this.tempDataTable);
                                        }

                                        this.OwnerGridView.DataSource = this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.DefaultView;

                                        if (this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Rows.Count >= 5)
                                        {
                                            this.OwnerGridView.ScrollBars = ScrollBars.Vertical;
                                            this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Rows.InsertAt(this.parcelTrackingDetailsDataSet.f29550ListOwnerDetails.Newf29550ListOwnerDetailsRow(), this.OwnerGridView.Rows.Count);
                                            this.OwnerGridVScrollBar.Visible = false;
                                        }
                                        else
                                        {
                                            this.OwnerGridVScrollBar.Visible = true;
                                            this.OwnerGridVScrollBar.Enabled = false;
                                        }
                                    }
                                }
                                else
                                {
                                    if (this.parcelTrackingDetailsDataSet.Tables[this.parcelTrackingDetailsDataSet.Tables.Count - 1].Rows.Count > 0)
                                    {
                                        if (this.tempDataTable.Rows.Count > 0)
                                        {
                                            this.parcelTrackingDetailsDataSet.Tables[this.parcelTrackingDetailsDataSet.Tables.Count - 1].Merge(this.tempDataTable);
                                        }

                                        this.OwnerGridView.DataSource = this.parcelTrackingDetailsDataSet.Tables[this.parcelTrackingDetailsDataSet.Tables.Count - 1].DefaultView;
                                        int emptyRecord = 0;
                                        ////VerticalScroll
                                        for (int i = 0; i < this.parcelTrackingDetailsDataSet.Tables[this.parcelTrackingDetailsDataSet.Tables.Count - 1].Rows.Count - 1; i++)
                                        {
                                            if (this.parcelTrackingDetailsDataSet.Tables[this.parcelTrackingDetailsDataSet.Tables.Count - 1].Rows[i][SharedFunctions.GetResourceString("GridEmptyRecord")].ToString() == SharedFunctions.GetResourceString("False") && !string.IsNullOrEmpty(this.parcelTrackingDetailsDataSet.Tables[this.parcelTrackingDetailsDataSet.Tables.Count - 1].Rows[i][SharedFunctions.GetResourceString("GridEmptyRecord")].ToString()))
                                            {
                                                emptyRecord++;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }

                                        if (emptyRecord >= 5)
                                        {
                                            this.parcelTrackingDetailsDataSet.Tables[this.parcelTrackingDetailsDataSet.Tables.Count - 1].Rows.InsertAt(this.parcelTrackingDetailsDataSet.Tables[this.parcelTrackingDetailsDataSet.Tables.Count - 1].NewRow(), this.OwnerGridView.Rows.Count - 1);
                                            this.OwnerGridVScrollBar.Visible = false;
                                        }
                                        else
                                        {
                                            this.OwnerGridVScrollBar.Visible = true;
                                            this.OwnerGridVScrollBar.Enabled = false;
                                        }
                                    }
                                }
                            }
                        }

                        this.EditRecord();

                        this.CustomizeListingParcelDetails();
                        this.DisableGridSorting(this.ParcelDataGridView, true);
                        this.DisableGridSorting(this.OwnerGridView, true);
                    }
                    else
                    {
                        this.selectedComponentMenuStrip.Visible = false;
                        this.ParcelDataGridView.Rows[this.rowIndex].DefaultCellStyle.BackColor = this.rowBackColor;
                        this.ParcelDataGridView.Rows[this.rowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                        ////this.CustomizeListingParcelDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Closed event of the SelectedComponentMenuStrip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ToolStripDropDownClosedEventArgs"/> instance containing the event data.</param>
        private void SelectedComponentMenuStrip_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            try
            {
                if (this.rowIndex >= 0)
                {
                    this.ParcelDataGridView.Rows[this.rowIndex].DefaultCellStyle.BackColor = this.rowBackColor;
                    this.ParcelDataGridView.Rows[this.rowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion MenuStrip

        #region PictureBoxEvents
        /// <summary>
        /// SaleHeaderPictureBox_MouseEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SaleHeaderPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.SeniorExemptionToolTip.SetToolTip(this.SaleParcelPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ParcelPictureBox_MouseEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.SeniorExemptionToolTip.SetToolTip(this.ParcelGidpictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// OwnersPictureBox_MouseEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void OwnersPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.SeniorExemptionToolTip.SetToolTip(this.OwnersPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ParcelsSoldPictureBox_MouseEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelsSoldPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.SeniorExemptionToolTip.SetToolTip(this.ParcelsSoldPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ParcelPictureBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(SharedFunctions.GetResourceString("SaleParcelPictureBox")));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// SaleParcelPictureBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SaleParcelPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(SharedFunctions.GetResourceString("SaleParcelPictureBox")));
                this.ParcelPictureBox_Click(sender, e);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion PictureBoxEvents

        #region Panel's Scroll Event
        /// <summary>
        /// SummaryPanel_Scroll
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SummaryPanel_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                this.ParcelDataGridView.HorizontalScrollingOffset = e.NewValue;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ParcelDataGridView_CellEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.SummaryPanel.HorizontalScroll.Value = this.ParcelDataGridView.HorizontalScrollingOffset;

                if ((this.ParcelDataGridView.Rows[e.RowIndex].Cells[0].Value != null) && (!string.IsNullOrEmpty(this.ParcelDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString())))
                {
                    if (e.ColumnIndex != 4)
                    {
                        this.ParcelDataGridView[e.ColumnIndex, e.RowIndex].ReadOnly = true;
                    }
                    else
                    {
                        this.ParcelDataGridView[e.ColumnIndex, e.RowIndex].ReadOnly = false;
                    }
                }

                int totalcount = this.ParcelDataGridView.OriginalRowCount;

                if (e.RowIndex == totalcount - 1)
                {
                    if (e.ColumnIndex != 4)
                    {
                        this.ParcelDataGridView[e.ColumnIndex, e.RowIndex].ReadOnly = true;
                    }
                    else
                    {
                        this.ParcelDataGridView[e.ColumnIndex, e.RowIndex].ReadOnly = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion Panel's Scroll Event

        #region ButtonEvent
        /// <summary>
        /// PushOwnerButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void PushOwnerButton_Click(object sender, EventArgs e)
        {
            try
            {
                //// this.pushOwnerFlag = true;

                this.parcelTrackingDetailsDataSet.Merge(this.form29550Controller.WorkItem.F29550_GetPushOwner(this.saleId));
                if (this.parcelTrackingDetailsDataSet.f29550_PushSaleOwner.Rows.Count > 0)
                {
                    this.pushOwnerRow = (F29550ParcelSaleTracking.f29550_PushSaleOwnerRow)this.parcelTrackingDetailsDataSet.f29550_PushSaleOwner.Rows[0];
                    this.dialogTxt = this.pushOwnerRow.DialogText.ToString();
                    this.dialogBoxId = this.ConvertStringtoInt(this.pushOwnerRow.PrimaryKeyID.ToString());
                    if (this.dialogBoxId == 1)
                    {
                        if (MessageBox.Show(this.dialogTxt, SharedFunctions.GetResourceString("PushOwnerSuccess"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            ////this.ConvertTableToXML(
                            int keyParcelID = 0; ////Added by Biju on 02-Dec-2010 to fix #8792
                            this.F29550Control.WorkItem.RootWorkItem.State["22006SystemSnapShotLoaded"] = true;
                            DataTable tempDataTable = new DataTable();
                            DataColumn tempDataTableColumn = new DataColumn("KeyID");
                            tempDataTable.Columns.Add("KeyID");

                            for (int item = 0; item < this.parcelTrackingDetailsDataSet.f29550ListParcelSaleTracking.Rows.Count; item++)
                            {
                                DataRow tempDataRow = tempDataTable.NewRow();
                                tempDataRow[0] = this.parcelTrackingDetailsDataSet.f29550ListParcelSaleTracking.Rows[item][0].ToString();
                                ////Added by Biju on 02-Dec-2010 to fix #8792
                                if (item.Equals(0))
                                    keyParcelID = Convert.ToInt32(tempDataRow[0].ToString());
                                ////till here
                                tempDataTable.Rows.Add(tempDataRow);
                            }

                            LoadSystemSnapShotDetails currentLoadSystemSnapShotDetails;
                            currentLoadSystemSnapShotDetails.MasterFormNO = 24550;
                            currentLoadSystemSnapShotDetails.RecordsetType = 1;
                            currentLoadSystemSnapShotDetails.IsSystemSnapShotLoaded = true;
                            currentLoadSystemSnapShotDetails.KeyIdColumnName = "ParcelID"; //// this.ownerStatusData.OwnerStatusDetailsTable.OwnerIDColumn.ColumnName;
                            currentLoadSystemSnapShotDetails.SnapShotXML = TerraScanCommon.GetXmlString(tempDataTable);
                            // this.OnD9030_F9033_SetSystemSnapshotEvent(new TerraScan.Infrastructure.Interface.EventArgs<LoadSystemSnapShotDetails>(currentLoadSystemSnapShotDetails));
                            this.snapshotId = this.form29550Controller.WorkItem.F9033_InsertSnapShotItems(TerraScanCommon.UserId, TerraScanCommon.GetXmlString(tempDataTable));

                            this.F29550Control.WorkItem.RootWorkItem.State["22006SystemSnapShotId"] = this.snapshotId;
                            //// this.snapshotId = this.form29550Controller.WorkItem.F9033_InsertSnapShotItems(this.userId, this.tempDataSet.GetXml().ToString());
                            if (this.snapshotId > 0)
                            {
                                try
                                {
                                    ////this.Cursor = Cursors.WaitCursor;
                                    FormInfo formInfo;
                                    formInfo = TerraScanCommon.GetFormInfo(22006);
                                    formInfo.optionalParameters = new object[1];
                                    formInfo.optionalParameters[0] = keyParcelID;////this.snapshotId;////Commented by Biju on 02-Dec-2010 to fix #8792
                                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                                    this.F29550Control.WorkItem.RootWorkItem.State["22006SystemSnapShotId"] = null;
                                }
                                catch (Exception e1)
                                {
                                    ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                                }
                                finally
                                {
                                    this.Cursor = Cursors.Default;
                                }
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (this.dialogBoxId == -1)
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("PushOwnerErrorMessage"), SharedFunctions.GetResourceString("PushOwner"), MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                        {
                            return;
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
        /// PushParcelsButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void PushParcelsButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////this.pushParcelFlag = true;
                this.parcelTrackingDetailsDataSet.Merge(this.form29550Controller.WorkItem.F29550_GetPushOwner(this.saleId));
                if (this.parcelTrackingDetailsDataSet.f29550_PushSaleOwner.Rows.Count > 0)
                {
                    this.pushOwnerRow = (F29550ParcelSaleTracking.f29550_PushSaleOwnerRow)this.parcelTrackingDetailsDataSet.f29550_PushSaleOwner.Rows[0];
                    this.dialogTxt = this.pushOwnerRow.DialogText.ToString();
                    this.dialogBoxId = this.ConvertStringtoInt(this.pushOwnerRow.PrimaryKeyID.ToString());
                    if (this.dialogBoxId == 1)
                    {
                        if (MessageBox.Show(this.dialogTxt, SharedFunctions.GetResourceString("PushOwnerSuccess"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion ButtonEvent

        #region ComboBox Events
        /// <summary>
        /// ConfidenceComboBox_DrawItem
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ConfidenceComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                e.DrawBackground();
                for (int i = 0; i < 4; i++)
                {
                    if (e.Index != i)
                    {
                        if (i == 0 && e.Index >= 0)
                        {
                            if (e.Index == i)
                            {
                                this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.confiRGBColor[i].ToString()));
                                this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                                e.Graphics.DrawString(this.confiItem[i], this.comboFont, this.sb, new RectangleF(0, 0, e.Bounds.Width, e.Bounds.Height));
                            }
                            else
                            {
                                if ((sender as TerraScanComboBox).ContainsFocus && (sender as TerraScanComboBox).SelectedIndex == 0)
                                {
                                    this.sb = new SolidBrush(e.BackColor);
                                    this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                                    e.Graphics.DrawString(this.confiItem[i], this.comboFont, this.sb, new RectangleF(0, 0, e.Bounds.Width, e.Bounds.Height));
                                }
                                else
                                {
                                    this.sb = new SolidBrush(e.BackColor);
                                    this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                                    e.Graphics.DrawString(this.confiItem[i], this.comboFont, this.sb, new RectangleF(0, 0, e.Bounds.Width, e.Bounds.Height));
                                }
                            }
                        }
                        else if (i == 1 && e.Index >= 0)
                        {
                            this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.confiRGBColor[i].ToString()));
                            this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                            e.Graphics.DrawString(this.confiItem[i], this.comboFont, this.sb, new RectangleF(0, 18, e.Bounds.Width, e.Bounds.Height));
                        }
                        else if (i == 2 && e.Index >= 0)
                        {
                            this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.confiRGBColor[i].ToString()));
                            this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                            e.Graphics.DrawString(this.confiItem[i], this.comboFont, this.sb, new RectangleF(0, 36, e.Bounds.Width, e.Bounds.Height));
                        }
                        else if (i == 3 && e.Index >= 0)
                        {
                            this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.confiRGBColor[i].ToString()));
                            this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                            e.Graphics.DrawString(this.confiItem[i], this.comboFont, this.sb, new RectangleF(0, 54, e.Bounds.Width, e.Bounds.Height));
                        }
                    }
                }

                //// SolidBrush sb = new SolidBrush(System.Drawing.Color.Black);
                if (e.Index >= 0)
                {
                    switch (e.Index)
                    {
                        case 0:
                            this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.confiRGBColor[e.Index].ToString()));
                            if ((sender as TerraScanComboBox).ContainsFocus && (sender as TerraScanComboBox).SelectedIndex == 0)
                            {
                                this.sb = new SolidBrush(Color.White);
                            }

                            this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                            break;

                        case 1:
                            this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.confiRGBColor[e.Index].ToString()));
                            if ((sender as TerraScanComboBox).ContainsFocus && (sender as TerraScanComboBox).SelectedIndex == 1)
                            {
                                this.sb = new SolidBrush(Color.White);
                            }

                            this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                            break;

                        case 2:
                            this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.confiRGBColor[e.Index].ToString()));
                            if ((sender as TerraScanComboBox).ContainsFocus && (sender as TerraScanComboBox).SelectedIndex == 2)
                            {
                                this.sb = new SolidBrush(Color.White);
                            }

                            this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                            break;

                        case 3:
                            this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.confiRGBColor[e.Index].ToString()));
                            if ((sender as TerraScanComboBox).ContainsFocus && (sender as TerraScanComboBox).SelectedIndex == 3)
                            {
                                this.sb = new SolidBrush(Color.White);
                            }

                            this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                            break;
                    }

                    this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                    e.Graphics.DrawString(this.confiItem[e.Index], this.comboFont, this.sb, new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
                }
            }
            catch (IndexOutOfRangeException iex)
            {
                ExceptionManager.ManageException(iex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// To change the color(Coming from DB) to ComboBox Item
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AdvisoryComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                e.DrawBackground();
                for (int i = 0; i < 3; i++)
                {
                    if (e.Index != i)
                    {
                        if (i == 0 && e.Index >= 0)
                        {
                            if (e.Index == i)
                            {
                                this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.advRGBColor[i].ToString()));
                                this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                                e.Graphics.DrawString(this.advItem[i], this.comboFont, this.sb, new RectangleF(0, 0, e.Bounds.Width, e.Bounds.Height));
                            }
                            else
                            {
                                if ((sender as TerraScanComboBox).ContainsFocus && (sender as TerraScanComboBox).SelectedIndex == 0)
                                {
                                    this.sb = new SolidBrush(e.BackColor);
                                    this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                                    e.Graphics.DrawString(this.advItem[i], this.comboFont, this.sb, new RectangleF(0, 0, e.Bounds.Width, e.Bounds.Height));
                                }
                                else
                                {
                                    this.sb = new SolidBrush(e.BackColor);
                                    this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                                    e.Graphics.DrawString(this.advItem[i], this.comboFont, this.sb, new RectangleF(0, 0, e.Bounds.Width, e.Bounds.Height));
                                }
                            }
                        }
                        else if (i == 1 && e.Index >= 0)
                        {
                            this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.advRGBColor[i].ToString()));
                            this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                            e.Graphics.DrawString(this.advItem[i], this.comboFont, this.sb, new RectangleF(0, 18, e.Bounds.Width, e.Bounds.Height));
                        }
                        else if (i == 2 && e.Index >= 0)
                        {
                            this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.advRGBColor[i].ToString()));
                            this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                            e.Graphics.DrawString(this.advItem[i], this.comboFont, this.sb, new RectangleF(0, 36, e.Bounds.Width, e.Bounds.Height));
                        }
                    }
                }

                if (e.Index >= 0)
                {
                    switch (e.Index)
                    {
                        case 0:
                            this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.advRGBColor[e.Index].ToString()));
                            if ((sender as TerraScanComboBox).ContainsFocus && (sender as TerraScanComboBox).SelectedIndex == 0)
                            {
                                this.sb = new SolidBrush(Color.White);
                            }

                            this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                            break;

                        case 1:

                            this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.advRGBColor[e.Index].ToString()));
                            if ((sender as TerraScanComboBox).ContainsFocus && (sender as TerraScanComboBox).SelectedIndex == 1)
                            {
                                this.sb = new SolidBrush(Color.White);
                            }

                            this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                            break;

                        case 2:

                            this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.advRGBColor[e.Index].ToString()));
                            if ((sender as TerraScanComboBox).ContainsFocus && (sender as TerraScanComboBox).SelectedIndex == 2)
                            {
                                this.sb = new SolidBrush(Color.White);
                            }

                            this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                            break;
                    }

                    this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                    e.Graphics.DrawString(this.advItem[e.Index], this.comboFont, this.sb, new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ReviewStatusComboBox_DrawItem
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ReviewStatusComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                e.DrawBackground();

                for (int i = 0; i < 3; i++)
                {
                    if (e.Index != i)
                    {
                        if (i == 0 && e.Index >= 0)
                        {
                            if (e.Index == i)
                            {
                                this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.riviewRGBColor[i].ToString()));
                                this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                                e.Graphics.DrawString(this.reviewItem[i], this.comboFont, this.sb, new RectangleF(0, 0, e.Bounds.Width, e.Bounds.Height));
                            }
                            else
                            {
                                if ((sender as TerraScanComboBox).ContainsFocus && (sender as TerraScanComboBox).SelectedIndex == 0)
                                {
                                    this.sb = new SolidBrush(e.BackColor);
                                    this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                                    e.Graphics.DrawString(this.reviewItem[i], this.comboFont, this.sb, new RectangleF(0, 0, e.Bounds.Width, e.Bounds.Height));
                                }
                                else
                                {
                                    this.sb = new SolidBrush(e.BackColor);
                                    this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                                    e.Graphics.DrawString(this.reviewItem[i], this.comboFont, this.sb, new RectangleF(0, 0, e.Bounds.Width, e.Bounds.Height));
                                }
                            }
                        }
                        else if (i == 1)
                        {
                            this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.riviewRGBColor[i].ToString()));
                            this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                            e.Graphics.DrawString(this.reviewItem[i], this.comboFont, this.sb, new RectangleF(0, 18, e.Bounds.Width, e.Bounds.Height));
                        }
                        else if (i == 2)
                        {
                            this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.riviewRGBColor[i].ToString()));
                            this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                            e.Graphics.DrawString(this.reviewItem[i], this.comboFont, this.sb, new RectangleF(0, 36, e.Bounds.Width, e.Bounds.Height));
                        }
                    }
                }

                //// SolidBrush sb = new SolidBrush(System.Drawing.Color.Black);
                if (e.Index >= 0)
                {
                    switch (e.Index)
                    {
                        case 0:
                            this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.riviewRGBColor[e.Index].ToString()));
                            if ((sender as TerraScanComboBox).ContainsFocus && (sender as TerraScanComboBox).SelectedIndex == 0)
                            {
                                this.sb = new SolidBrush(Color.White);
                            }

                            this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                            break;

                        case 1:
                            this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.riviewRGBColor[e.Index].ToString()));
                            if ((sender as TerraScanComboBox).ContainsFocus && (sender as TerraScanComboBox).SelectedIndex == 1)
                            {
                                this.sb = new SolidBrush(Color.White);
                            }

                            this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                            break;

                        case 2:
                            this.sb = new SolidBrush((System.Drawing.Color)this.colorConv.ConvertFromString(this.riviewRGBColor[e.Index].ToString()));
                            if ((sender as TerraScanComboBox).ContainsFocus && (sender as TerraScanComboBox).SelectedIndex == 2)
                            {
                                this.sb = new SolidBrush(Color.White);
                            }

                            this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                            break;
                    }

                    this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                    e.Graphics.DrawString(this.reviewItem[e.Index], this.comboFont, this.sb, new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
                }
            }
            catch (IndexOutOfRangeException iex)
            {
                ExceptionManager.ManageException(iex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// LocalQualificationComboBox_DrawItem
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void LocalQualificationComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                ////SolidBrush sbrush = new SolidBrush(System.Drawing.Color.Black);
                ////e.DrawBackground();
                ////if (e.Index >= 0)
                ////{
                ////    StringFormat textAlign = new StringFormat();
                ////    textAlign.Alignment = StringAlignment.Center;
                ////    this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Bold);
                ////    e.Graphics.DrawString(this.localQulificationItem[e.Index], this.comboFont, sbrush, new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), textAlign);
                ////}
            }
            catch (IndexOutOfRangeException exp)
            {
                ExceptionManager.ManageException(exp, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the ParcelDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void ParcelDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    ((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(this.F29550_SelectionChangeCommitted);
                }
            }
            catch (ArithmeticException exp)
            {
                ExceptionManager.ManageException(exp, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the F29550 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F29550_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.EditRecord();
        }

        #endregion ComboBox Events

        #region TextBox Events
        /// <summary>
        /// OriginalSalePriceTextBox_Validating
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void OriginalSalePriceTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                this.AdjustedSalePriceCalculation();
                this.RatioAtSaleCalculation();
                this.CurrentRatioCalculation();
                this.NewRatioCalculation();

                ////SetColor
                if (this.AdjustedPPTextBox.Text.Equals(SharedFunctions.GetResourceString("Zero")))
                {
                    this.AdjustedPPTextBox.ForeColor = System.Drawing.Color.Black;
                }
                else if (this.ConvertStringtoDec(this.AdjustedPPTextBox.Text.Trim()) > 0)
                {
                    this.AdjustedPPTextBox.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
                }

                ////Set Color to AdjustedFinance TextBox
                if (this.AdjustedFinanceTextBox.Text.Equals(SharedFunctions.GetResourceString("Zero")))
                {
                    this.AdjustedFinanceTextBox.ForeColor = System.Drawing.Color.Black;
                }
                else if (this.ConvertStringtoDec(this.AdjustedFinanceTextBox.Text.Trim()) > 0)
                {
                    this.AdjustedFinanceTextBox.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
                }

                ////Set Color to AdjTimeSale TextBox
                if (this.AdjTimeSaleTextBox.Text.Equals(SharedFunctions.GetResourceString("Zero")))
                {
                    this.AdjTimeSaleTextBox.ForeColor = System.Drawing.Color.Black;
                }
                else if (this.ConvertStringtoDec(this.AdjTimeSaleTextBox.Text.Trim()) > 0)
                {
                    this.AdjTimeSaleTextBox.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
                }

                ////Set Color to AdjOther TextBox
                if (this.AdjOtherTextBox.Text.Equals(SharedFunctions.GetResourceString("Zero")))
                {
                    this.AdjOtherTextBox.ForeColor = System.Drawing.Color.Black;
                }
                else if (this.ConvertStringtoDec(this.AdjOtherTextBox.Text.Trim()) > 0)
                {
                    this.AdjOtherTextBox.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
                }
            }
            catch (ArithmeticException exp)
            {
                ExceptionManager.ManageException(exp, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// EstimateatSaleTextBox_Validating
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void EstimateatSaleTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                this.RatioAtSaleCalculation();
            }
            catch (ArithmeticException aex)
            {
                ExceptionManager.ManageException(aex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// CurrentEstimateTextBox_Validating
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CurrentEstimateTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                this.CurrentRatioCalculation();
            }
            catch (ArithmeticException aex)
            {
                ExceptionManager.ManageException(aex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// AdjSalePriceTextBox_Validating
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AdjSalePriceTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                this.CurrentRatioCalculation();
                this.NewRatioCalculation();
            }
            catch (ArithmeticException aex)
            {
                ExceptionManager.ManageException(aex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// AppraisalEstimateTextBox_Validating
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AppraisalEstimateTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                this.NewRatioCalculation();
            }
            catch (ArithmeticException aex)
            {
                ExceptionManager.ManageException(aex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// SalDateTextBox_Validating
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SalDateTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.SalDateTextBox.Text.ToString().Trim()))
                {
                    if (Convert.ToDateTime(this.SalDateTextBox.Text) > System.DateTime.Today.Date)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("Date Validation"), SharedFunctions.GetResourceString("Log"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.SalDateTextBox.Focus();
                        this.SalDateTextBox.Text = System.DateTime.Today.Date.ToString();
                    }
                }
                else
                {
                    this.SalDateTextBox.Text = System.DateTime.Today.Date.ToString();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// OriginalSFTextBox_Validating
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void OriginalSFTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                decimal originalSF = this.ConvertStringtoDec(this.OriginalSFTextBox.Text.ToString());
                this.OriginalSFTextBox.Text = originalSF.ToString("#0.00");
                int i = OriginalSFTextBox.Text.IndexOf(".");
                if (i >= 12)
                {
                    this.OriginalSFTextBox.Text = 0.ToString("#0.00");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// OriginalSalePriceTextBox_KeyPress
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void OriginalSalePriceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                this.EditRecord();
            }
            catch (ArithmeticException exp)
            {
                ExceptionManager.ManageException(exp, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// AdjustedSFTextBox_Validating_1
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AdjustedSFTextBox_Validating_1(object sender, CancelEventArgs e)
        {
            try
            {
                decimal originalSF = this.ConvertStringtoDec(this.AdjustedSFTextBox.Text.ToString());
                this.AdjustedSFTextBox.Text = originalSF.ToString("#0.00");
                int i = AdjustedSFTextBox.Text.IndexOf(".");
                if (i >= 12)
                {
                    this.AdjustedSFTextBox.Text = 0.ToString("#0.00");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion TextBox Events


  #region Coding Added for Quickfind Issue

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the ReviewStatusComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ReviewStatusComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.EditRecord(); 
        }
  #endregion

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
