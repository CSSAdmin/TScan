//--------------------------------------------------------------------------------------------
// <copyright file="f16030.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10 JUN 07	    Suganth Mani       Created
// 22 JUN 07        Suganth Mani       Modified 
// 19 Sep 11        Manoj Kumar P      Modified for District Info Fields Slice TSCO #12469
//*********************************************************************************/
namespace D10030
{
    #region NameSpace

    using System;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;

    #endregion NameSpace

    /// <summary>
    /// Class for special district definition
    /// </summary>
    [SmartPart]
    public partial class F16030 : BaseSmartPart
    {
        #region Private Member Fields

        /// <summary>
        /// F1030 controller
        /// </summary>
        private F16030Controller form1030control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// f1030DistrictDefinitionData
        /// </summary>
        private F1030SpecialDistrictDefinitionData form1030DistrictDefinitionData;

        /// <summary>
        /// bindedDistrictRateDetailsDataTable
        /// </summary>
        private F1030SpecialDistrictDefinitionData.GetDistrictRateDetailsDataTable bindedDistrictRateDetailsDataTable;

        /// <summary>
        /// bindedDistrictDistributionDetailsDataTable
        /// </summary>
        private F1030SpecialDistrictDefinitionData.GetDistrictDistributionDetailsDataTable bindedDistrictDistributionDetailsDataTable;

        /// <summary>
        /// used to keep trakc of SelectedRateItem
        /// </summary>
        private BindingSource rateSource = new BindingSource();

        /// <summary>
        /// used to keep trakc of SelectedDistributionItem
        /// </summary>
        private BindingSource distributionSource = new BindingSource();

        /// <summary>
        /// To hold active record
        /// </summary>
        private int activeRecord;

        /// <summary>
        /// selectedRateItem
        /// </summary>
        private int selectedRateItem;

        /// <summary>
        /// the distribution item selcted for delete
        /// </summary>
        private int selectedDistributionItem;

        /// <summary>
        /// flagCopyDistrict
        /// </summary>
        private bool flagCopyDistrict;

        /// <summary>
        /// the typed dataset for yes no combo box
        /// </summary>
        private CommonData commonData;

        /// <summary>
        /// tha actual items count of rategrid
        /// </summary>
        private int rateGridActualRowCount;

        /// <summary>
        /// the actual item count of distributiongrid
        /// </summary>
        private int distributionGridActualRowCount;

        /// <summary>
        /// flag to identify the form is being loading
        /// </summary>
        private bool flagFormLoad = true;

        /// <summary>
        /// flag to identify sort direction
        /// </summary>
        private bool flagRateSortDirection;

        /// <summary>
        /// flag to identify distribution item sort direction
        /// </summary>
        private bool flagDistributionSortDirection;

        /// <summary>
        /// the column no selected at rategrid view
        /// </summary>
        private int rateGridViewSelectColumnNo;

        /// <summary>
        /// the column no selected at distribution grid view
        /// </summary>
        private int distributionGridViewSelectColumnNo;

        /// <summary>
        /// default roll year variable 
        /// </summary>
        private int defaultRollYear;

        /// <summary>
        /// flag to identify row header delete in rate grid
        /// </summary>
        private bool flagRateItemDeleteEnabled;

        /// <summary>
        /// flag to identify row header delete in distribution grid
        /// </summary>
        private bool flagDistributionItemDeleteEnabled;

        /// <summary>
        /// flag to show account form.
        /// </summary>
        private bool flagAccountShow;

        /// <summary>
        /// cellValue When Editing
        /// </summary>
        private string cellValueWhenEditing;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// flag to identify overwrite or not
        /// </summary>
        private bool flagOverwrite;

        /// <summary>
        /// To get the Roll year from General Configuration Value
        /// </summary>
        private CommentsData getRollYearConfigurationValue = new CommentsData();

        /// <summary>
        /// revert the changes as duplicate
        /// </summary>
        private bool flagRevokeChangesOnDuplicate;

        /// <summary>
        /// flag to identify form master new permission
        /// </summary>
        private bool flagMasterNew;

        /// <summary>
        /// texteditflag
        /// </summary>
        private bool texteditflag = false;

        ////Created private member to fix Bug#5183

        /// <summary>
        /// percentage header flag
        /// </summary>
        private bool flagPercentageHeader;

        private bool isDistrictclicked = false;

        #endregion Private Member Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1030"/> class.
        /// </summary>
        public F16030()
        {
            InitializeComponent();
            this.flagCopyDistrict = false;
            this.form1030DistrictDefinitionData = new F1030SpecialDistrictDefinitionData();
            this.bindedDistrictRateDetailsDataTable = new F1030SpecialDistrictDefinitionData.GetDistrictRateDetailsDataTable();
            this.bindedDistrictDistributionDetailsDataTable = new F1030SpecialDistrictDefinitionData.GetDistrictDistributionDetailsDataTable();
            this.commonData = new CommonData();
            this.rateGridActualRowCount = 0;
            this.flagFormLoad = false;
            this.flagRateSortDirection = false;
            this.flagDistributionItemDeleteEnabled = false;
            this.flagRateItemDeleteEnabled = false;
            this.flagAccountShow = false;
            this.cellValueWhenEditing = string.Empty;
            this.rateGridViewSelectColumnNo = 0;
            this.distributionGridViewSelectColumnNo = 0;
            this.defaultRollYear = DateTime.Now.Year;
            this.SetMaxLength();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F16030"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F16030(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            InitializeComponent();
            this.flagCopyDistrict = false;
            this.form1030DistrictDefinitionData = new F1030SpecialDistrictDefinitionData();
            this.bindedDistrictRateDetailsDataTable = new F1030SpecialDistrictDefinitionData.GetDistrictRateDetailsDataTable();
            this.bindedDistrictDistributionDetailsDataTable = new F1030SpecialDistrictDefinitionData.GetDistrictDistributionDetailsDataTable();
            this.commonData = new CommonData();
            this.rateGridActualRowCount = 0;
            this.flagFormLoad = false;
            this.flagRateSortDirection = false;
            this.flagDistributionItemDeleteEnabled = false;
            this.flagRateItemDeleteEnabled = false;
            this.flagAccountShow = false;
            this.flagRevokeChangesOnDuplicate = false;
            this.cellValueWhenEditing = string.Empty;
            this.rateGridViewSelectColumnNo = 0;
            this.distributionGridViewSelectColumnNo = 0;
            this.defaultRollYear = DateTime.Now.Year;
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.activeRecord = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.DistrictCopyButton.Enabled = true;
            this.SetMaxLength();
        }

        #endregion Constructor

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
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Declare the event PageStatusActivated        
        /// </summary> 
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> PageStatusActivated;

        /// <summary>
        /// Declare the event for raising new operation in form master        
        /// </summary> 
        [EventPublication(EventTopicNames.D9030_F9030_RaiseFormMasterNew, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<int>> D9030_F9030_RaiseFormMasterNew;

        #endregion Event Publication

        #region Field encapsulation

        /// <summary>
        /// Gets or sets the form1030control.
        /// </summary>
        /// <value>The form9030control.</value>
        [CreateNew]
        public F16030Controller Form9030control
        {
            get { return this.form1030control; }
            set { this.form1030control = value; }
        }

        #endregion Field encapsulation

        #region Event Subscription

        /// <summary>
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            if (this.slicePermissionField.newPermission && (this.Controls.Count > 0))
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.EnableControls(true);
                this.LockControls(false);
                this.NewButtonClick();
            }
            else
            {
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.NewButtonClick();
                this.LockControls(true);
                this.EnableControls(false);
                //////// todo: this.ClearWaterValve();
                //////// todo: this.SetNewComboIndex();
                //////// todo: this.LockControls(false);
            }
        }

        /// <summary>
        /// Event Subscription for cancel slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
        {
            if (this.Controls.Count > 0)
            {
                ////if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                ////{
                this.ClearSpecialDistrictDetails();
                ////}

                if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                {
                    this.LockControls(false);
                    this.EnableControls(true);
                }
                else
                {
                    this.EnableControls(false);
                    this.LockControls(true);
                }

                this.FillDetails(this.activeRecord);

                foreach (DataGridViewColumn currentColumn in this.RateDetailsDataGridView.Columns)
                {
                    currentColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                }

                foreach (DataGridViewColumn currentColumn in this.DistributionGridView.Columns)
                {
                    currentColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                }

                this.flagCopyDistrict = false;

                ////if (this.slicePermissionField.newPermission && this.flagMasterNew)
                ////{
                ////    this.DistrictCopyButton.Enabled = true;
                ////}
                ////else
                ////{
                ////    this.DistrictCopyButton.Enabled = false;
                ////}

                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit || this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                if (this.slicePermissionField.newPermission || this.slicePermissionField.editPermission)
                {
                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
                }
            }
            else
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
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
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit || this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                if (this.slicePermissionField.editPermission || this.slicePermissionField.newPermission)
                {
                    if (this.flagRevokeChangesOnDuplicate)
                    {
                        if (this.slicePermissionField.newPermission && this.flagMasterNew)
                        {
                            this.DistrictCopyButton.Enabled = true;
                        }
                        else
                        {
                            this.DistrictCopyButton.Enabled = false;
                        }

                        this.flagCopyDistrict = false;
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.flagRevokeChangesOnDuplicate = false;
                    }
                    else
                    {
                        bool status = this.SaveSpecialDistrictDefinition();
                        if (status)
                        {
                            if (this.slicePermissionField.newPermission && this.flagMasterNew)
                            {
                                this.DistrictCopyButton.Enabled = true;
                            }
                            else
                            {
                                this.DistrictCopyButton.Enabled = false;
                            }

                            this.flagCopyDistrict = false;
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                        }
                    }

                    this.ClearSpecialDistrictDetails();
                    this.EnableControls(true);
                }
                else
                {
                    this.EnableControls(false);
                }
            }
            else
            {
                if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
                {
                    this.EnableControls(true);
                }
                else
                {
                    this.EnableControls(false);
                }

                if (this.slicePermissionField.newPermission && this.flagMasterNew)
                {
                    this.DistrictCopyButton.Enabled = true;
                }
                else
                {
                    this.DistrictCopyButton.Enabled = false;
                }

                this.flagCopyDistrict = false;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
        }

        /// <summary>
        /// Event Subscription SetSlicePermission
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The Event.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.flagFormLoad = true;
                this.flagMasterNew = this.GetFormMasterNewPermission();

                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                if (this.slicePermissionField.newPermission && this.flagMasterNew)
                {
                    this.DistrictCopyButton.Enabled = true;
                }
                else
                {
                    this.DistrictCopyButton.Enabled = false;
                }

                this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                {
                    this.EnableControls(true);
                }
                else
                {
                    this.EnableControls(false);
                }

                if (this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows.Count > 0)
                {
                    if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                    {
                        if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictStatementColumn].ToString()))
                        {
                            if (this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictStatementColumn].ToString() == "1")
                            {
                                this.DistrictNumberTextBox.LockKeyPress = false;
                                this.RollYearTextBox.LockKeyPress = true;
                                this.MinimumDistrictFeeTextBox.LockKeyPress = true;
                                this.DistrictNameTextBox.LockKeyPress = false;
                                this.TypeComboBox.Enabled = false;
                                this.RateDetailsDataGridView.ReadOnly = true;
                                this.DistributionGridView.ReadOnly = true;
                                this.flagAccountShow = false;
                               // this.DistrictCopyButton.Enabled = false;

                                this.DistrictCopyButton.Enabled = true;
                            }
                            else
                            {
                                this.DistrictNumberTextBox.LockKeyPress = false;
                                this.RollYearTextBox.LockKeyPress = false;
                                this.MinimumDistrictFeeTextBox.LockKeyPress = false;
                                this.DistrictNameTextBox.LockKeyPress = false;
                                this.TypeComboBox.Enabled = true;
                                this.RateDetailsDataGridView.ReadOnly = false;
                                this.DistributionGridView.ReadOnly = false;
                                this.flagAccountShow = true;
                            }
                        }
                    }
                    else
                    {
                        this.EnableControls(false);
                        this.LockControls(true);
                    }

                    eventArgs.Data.FlagInvalidSliceKey = false;
                }
                else
                {
                    this.LockControls(true);
                    this.EnableControls(false);
                    //// Coding Added for the issue 4212 0n 30/5/2009.
                    //// Last Slice does not have a record also it will not return invalid slice
                    if (eventArgs.Data.FlagInvalidSliceKey)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = true;
                    }
                }
            }

            this.DistrictNumberTextBox.Focus();
            this.flagFormLoad = false;
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
                this.flagFormLoad = true;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.EnableControls(true);
                this.activeRecord = eventArgs.Data.SelectedKeyId;
                this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                {
                    this.EnableControls(true);
                    this.ClearSpecialDistrictDetails();
                }
                else
                {
                    this.EnableControls(false);
                }

                this.FillDetails(this.activeRecord);
                foreach (DataGridViewColumn currentColumn in this.RateDetailsDataGridView.Columns)
                {
                    currentColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                }

                foreach (DataGridViewColumn currentColumn in this.DistributionGridView.Columns)
                {
                    currentColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                }

                if (this.slicePermissionField.newPermission)
                {
                    this.DistrictCopyButton.Enabled = true;
                }
                else
                {
                    this.DistrictCopyButton.Enabled = false;
                }

                this.flagFormLoad = false;
            }
        }

        #region Event Subscription

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_CheckPageStatus, Thread = ThreadOption.UserInterface)]
        public void CheckPageStatus(object sender, DataEventArgs<Button> e)
        {
            if (this.CheckPageStatus())
            {
                this.PageStatusActivated(this, new DataEventArgs<Button>(e.Data));
            }
        }

        /// <summary>
        /// Gets the form status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The source of the event</param>
        [EventSubscription(EventTopics.D9001_ShellForm_GetFormStatus, Thread = ThreadOption.UserInterface)]
        public void GetFormStatus(object sender, DataEventArgs<string> e)
        {
            if (e.Data == this.GetType().Name)
            {
                this.form1030control.WorkItem.State["FormStatus"] = this.CheckPageStatus();
            }
        }

        #endregion Events Subscription

        #endregion Event Subscription

        #region Protected methods

        /// <summary>
        /// Called when [form slice_ form close alert].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_FormCloseAlert(DataEventArgs<SliceFormCloseAlert> eventArgs)
        {
            if (this.FormSlice_FormCloseAlert != null)
            {
                this.FormSlice_FormCloseAlert(this, eventArgs);
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

        /// <summary>
        /// Called when [D9030_ F9030_ raise form master new].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_RaiseFormMasterNew(TerraScan.Infrastructure.Interface.EventArgs<int> eventArgs)
        {
            if (this.D9030_F9030_RaiseFormMasterNew != null)
            {
                this.D9030_F9030_RaiseFormMasterNew(this, eventArgs);
            }
        }

        #endregion Protected methods

        #region Events

        #region general form events

        /// <summary>
        /// Handles the Load event of the F1030 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1030_Load(object sender, EventArgs e)
        {
            try
            {
                this.flagFormLoad = true;
                this.FlagSliceForm = true;
                this.LoadComboxValue();
                if (!this.PermissionEdit)
                {
                    this.LockControls(true);
                }

                this.EnableControls(true);
                this.SetDefaultRollYear();
                this.DistrictHeaderPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DistrictHeaderPictureBox.Height, this.DistrictHeaderPictureBox.Width, SharedFunctions.GetResourceString("District"), 174, 150, 94);
                this.RateItemPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.RateItemPictureBox.Height, this.RateItemPictureBox.Width, SharedFunctions.GetResourceString("Rates"), 28, 81, 128);
                this.DistributionItemPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DistributionItemPictureBox.Height, this.DistributionItemPictureBox.Width, SharedFunctions.GetResourceString("Distribution"), 0, 64, 0);
                this.DistrictInfoPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DistrictInfoPictureBox.Height, this.DistrictInfoPictureBox.Width, SharedFunctions.GetResourceString("District Info"), 28, 81, 128);         
                this.LoadListInformation();
                this.FillDetails(this.activeRecord);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.flagFormLoad = false;
                this.RemoveDefaultSelection();

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Loads the combox value.
        /// </summary>
        private void LoadComboxValue()
        {
            try
            {
                this.commonData.ComboKeyStringDataTable.Clear();
                CommonData.ComboKeyStringDataTableRow comboBoxDataTableRow = this.commonData.ComboKeyStringDataTable.NewComboKeyStringDataTableRow();
                comboBoxDataTableRow.KeyId = SharedFunctions.GetResourceString("Yes");
                comboBoxDataTableRow.KeyName = SharedFunctions.GetResourceString("Yes");
                this.commonData.ComboKeyStringDataTable.AddComboKeyStringDataTableRow(comboBoxDataTableRow);
                comboBoxDataTableRow = this.commonData.ComboKeyStringDataTable.NewComboKeyStringDataTableRow();
                comboBoxDataTableRow.KeyId = SharedFunctions.GetResourceString("No");
                comboBoxDataTableRow.KeyName = SharedFunctions.GetResourceString("No");
                this.commonData.ComboKeyStringDataTable.AddComboKeyStringDataTableRow(comboBoxDataTableRow);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the DistrictCopyButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictCopyButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.isDistrictclicked = true;
                this.flagCopyDistrict = true;
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.OnD9030_F9030_RaiseFormMasterNew(new TerraScan.Infrastructure.Interface.EventArgs<int>(this.masterFormNo));
                this.DistrictNumberTextBox.LockKeyPress = false;
                if (!this.RollYearTextBox.IsDisposed)
                {
                    this.RollYearTextBox.LockKeyPress = false;
                    this.MinimumDistrictFeeTextBox.LockKeyPress = false;
                    this.DistrictNameTextBox.LockKeyPress = false;
                }
                this.TypeComboBox.Enabled = true;
                this.RateDetailsDataGridView.ReadOnly = false;
                this.DistributionGridView.ReadOnly = false;
                this.flagAccountShow = true;
                this.DistrictCopyButton.Enabled = false;
                this.DistrictNumberTextBox.Focus();

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Edits the enabled event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EditEnabledEvent(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    this.EnableEditMode();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion general form events

        #region RateDetailsDataGridView Events

        /// <summary>
        /// Handles the CellBeginEdit event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="send er">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                this.flagRateItemDeleteEnabled = false;
                if (e.ColumnIndex == 2)
                {
                    if (this.RateDetailsDataGridView[e.ColumnIndex - 1, e.RowIndex].Value.ToString() == "2")
                    {
                        e.Cancel = true;
                        return;
                    }
                }

                // this.EnableEditMode();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int count = 0;
                if (this.flagRateItemDeleteEnabled)
                {
                    if (e.KeyValue == 46)
                    {
                        ////if (this.RateDetailsDataGridView.CurrentCell.RowIndex.Equals(this.rateGridActualRowCount))
                        ////{
                        ////    if (Convert.ToBoolean(this.bindedDistrictRateDetailsDataTable.Rows[this.rateGridActualRowCount][this.RateDetailsDataGridView.EmptyRecordColumnName]))
                        ////    {
                        ////        return;
                        ////    }
                        ////made changes to fix Bug#5716 & #5715
                        if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                        {
                            for (int i = 0; i <= this.bindedDistrictRateDetailsDataTable.Rows.Count - 1; i++)
                            {
                                if (this.bindedDistrictRateDetailsDataTable.Rows[i][this.bindedDistrictRateDetailsDataTable.SARateItemIDColumn] != null)
                                {
                                    if (!string.IsNullOrEmpty(this.bindedDistrictRateDetailsDataTable.Rows[i][this.bindedDistrictRateDetailsDataTable.SARateItemIDColumn].ToString()))
                                    {
                                        count = count + 1;
                                    }
                                }
                            }

                            if (count > 1)
                            {
                                this.flagRateItemDeleteEnabled = false;
                                //// this.RefreshDetails(this.activeRecord);////commeted Already
                                this.bindedDistrictRateDetailsDataTable.Rows[this.rateGridActualRowCount].Delete();
                                if (this.rateGridActualRowCount < this.form1030DistrictDefinitionData.GetDistrictRateDetails.Rows.Count)
                                {
                                    this.form1030DistrictDefinitionData.GetDistrictRateDetails.Rows[this.rateGridActualRowCount].Delete();
                                    this.form1030control.WorkItem.F16030_DeleteDistrictDefinitionRate(this.selectedRateItem, TerraScanCommon.UserId);
                                }

                                this.bindedDistrictRateDetailsDataTable.AddGetDistrictRateDetailsRow(this.bindedDistrictRateDetailsDataTable.NewGetDistrictRateDetailsRow());
                                ////this.form1030control.WorkItem.F16030_DeleteDistrictDefinitionRate(this.selectedRateItem, TerraScanCommon.UserId);

                                ////this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                                ////this.EnableEditMode();
                                //// this.form1030DistrictDefinitionData.Merge(this.form1030control.WorkItem.F16030_GetDistrictDefinitionDetails(this.activeRecord), true);                           
                            }
                            else
                            {
                                MessageBox.Show("District Definition should have atleast one Rate/Fee Item", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else if (this.pageMode == TerraScanCommon.PageModeTypes.Edit || this.pageMode == TerraScanCommon.PageModeTypes.New)
                        {
                            ////((this.RateDetailsDataGridView.CurrentCell != null) && (this.selectedRateItem.Equals(0)) && (this.RateDetailsDataGridView.Rows[this.RateDetailsDataGridView.CurrentCell.RowIndex].Selected))
                            if (this.selectedRateItem.Equals(0))
                            {
                                this.bindedDistrictRateDetailsDataTable.Rows[this.rateGridActualRowCount].Delete();
                                this.bindedDistrictRateDetailsDataTable.AddGetDistrictRateDetailsRow(this.bindedDistrictRateDetailsDataTable.NewGetDistrictRateDetailsRow());
                            }
                            else
                            {
                                MessageBox.Show("Saved Records can not be deleted in Edit Mode", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (SoapException ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.Display, this.ParentForm);
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
        /// Handles the RowHeaderMouseClick event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.RateDetailsDataGridView.CurrentCell != null)
                {
                    this.flagRateItemDeleteEnabled = true;
                    this.rateGridActualRowCount = e.RowIndex;

                    if (this.bindedDistrictRateDetailsDataTable.Rows[e.RowIndex][this.bindedDistrictRateDetailsDataTable.SARateItemIDColumn] != null)
                    {
                        if (!string.IsNullOrEmpty(this.bindedDistrictRateDetailsDataTable.Rows[e.RowIndex][this.bindedDistrictRateDetailsDataTable.SARateItemIDColumn].ToString()))
                        {
                            this.selectedRateItem = Convert.ToInt32(this.bindedDistrictRateDetailsDataTable.Rows[e.RowIndex][this.bindedDistrictRateDetailsDataTable.SARateItemIDColumn]);
                        }
                        else
                        {
                            this.selectedRateItem = 0;
                        }
                    }

                    this.RateDetailsDataGridView.CurrentCell.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.flagRateItemDeleteEnabled = false;
                if (e.RowIndex >= this.RateDetailsDataGridView.NumRowsVisible - 1)
                {
                    if ((e.RowIndex + 1) == this.bindedDistrictRateDetailsDataTable.Rows.Count)
                    {
                        this.rateGridActualRowCount++;
                        this.bindedDistrictRateDetailsDataTable.AddGetDistrictRateDetailsRow(this.bindedDistrictRateDetailsDataTable.NewGetDistrictRateDetailsRow());
                        this.RateDetailsScrollBar.Visible = false;
                    }
                }

                if (e.ColumnIndex == 1)
                {
                    if (this.RateDetailsDataGridView[e.ColumnIndex, e.RowIndex].Value != null && !string.IsNullOrEmpty(this.RateDetailsDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString()))
                    {
                        if (Convert.ToInt32(this.RateDetailsDataGridView[e.ColumnIndex, e.RowIndex].Value) == 2)
                        {
                            if (this.RateDetailsDataGridView[e.ColumnIndex + 1, e.RowIndex].Value != null && !string.IsNullOrEmpty(this.RateDetailsDataGridView[e.ColumnIndex + 1, e.RowIndex].Value.ToString().Trim()))
                            {
                                this.RateDetailsDataGridView[e.ColumnIndex + 1, e.RowIndex].Value = "";
                            }

                            this.RateDetailsDataGridView[e.ColumnIndex + 1, e.RowIndex].ReadOnly = true;
                        }
                        else
                        {
                            this.RateDetailsDataGridView[e.ColumnIndex + 1, e.RowIndex].ReadOnly = false;
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
        /// Handles the CellValueChanged event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    this.flagRateItemDeleteEnabled = false;
                    this.EnableEditMode();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the ColumnHeaderMouseClick event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                this.flagRateItemDeleteEnabled = false;
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    this.flagRateSortDirection = !this.flagRateSortDirection;
                    this.rateGridViewSelectColumnNo = e.ColumnIndex;
                    if (this.flagRateSortDirection)
                    {
                        this.RateDetailsDataGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                    else
                    {
                        this.RateDetailsDataGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }

                    this.PopulateRateDetailsGridView();
                    ////added code to fix Bug#4192
                    int itemFound = this.rateSource.Find("SARateItemID", this.selectedRateItem);
                    TerraScanCommon.SetDataGridViewPosition(this.RateDetailsDataGridView, itemFound);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.RateDetailsDataGridView.Rows.Count > 0)
                {
                    this.RateDetailsDataGridView.Rows[0].Selected = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellParsing event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.RateDetailsDataGridView.Columns[SharedFunctions.GetResourceString("Amount")].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    // Only paint if text provided, Only paint if desired text is in cell

                    if (e.Value != null)
                    {
                        string tempvalue = e.Value.ToString().Trim();
                        Decimal outDecimal = 0.00M;

                        if (tempvalue.EndsWith("."))
                        {
                            tempvalue = string.Concat(tempvalue, SharedFunctions.GetResourceString("Zero"));
                        }

                        if (Decimal.TryParse(tempvalue, NumberStyles.Currency, null, out outDecimal))
                        {
                            tempvalue = outDecimal.ToString();

                            if (!tempvalue.Contains("."))
                            {
                                outDecimal = outDecimal / 100;
                            }

                            e.Value = outDecimal;
                        }
                        else
                        {
                            e.Value = "0.0";
                        }

                        e.ParsingApplied = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataError event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;
            try
            {
                // Only paint if desired, formattable column

                if (e.ColumnIndex == this.RateDetailsDataGridView.Columns[SharedFunctions.GetResourceString("Amount")].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    // Only paint if text provided, Only paint if desired text is in cell 

                    if (e.Value != null && !String.IsNullOrEmpty(this.RateDetailsDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("Amount")].Value.ToString()))
                    {
                        string val = e.Value.ToString();

                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            if (outDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00##"), ")");
                                e.CellStyle.ForeColor = Color.Green;
                            }
                            else
                            {
                                e.Value = outDecimal.ToString("#,##0.00##");
                                e.FormattingApplied = true;
                            }
                        }
                        else
                        {
                            e.Value = "0.00";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }

                    e.FormattingApplied = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.RowIndex.Equals(0))
                    {
                        this.RateDetailsDataGridView.Rows[0].ReadOnly = false;
                    }

                    if (e.RowIndex > 1)
                    {
                        if (string.IsNullOrEmpty(this.RateDetailsDataGridView[0, e.RowIndex - 1].Value.ToString().Trim()) && string.IsNullOrEmpty(this.RateDetailsDataGridView[1, e.RowIndex - 1].Value.ToString().Trim()) && string.IsNullOrEmpty(this.RateDetailsDataGridView[2, e.RowIndex - 1].Value.ToString().Trim()) && string.IsNullOrEmpty(this.RateDetailsDataGridView[3, e.RowIndex - 1].Value.ToString().Trim()) && (e.RowIndex > 0))
                        {
                            this.RateDetailsDataGridView.Rows[e.RowIndex].ReadOnly = true;
                        }
                        else
                        {
                            this.RateDetailsDataGridView.Rows[e.RowIndex].ReadOnly = false;
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
        /// Handles the Enter event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_Enter(object sender, EventArgs e)
        {
            try
            {
                if (this.RateDetailsDataGridView.OriginalRowCount > 0)
                {
                    this.RateDetailsDataGridView.Rows[0].Cells[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.RateDetailsDataGridView.CurrentCell != null)
                {
                    this.RateDetailsDataGridView.CurrentCell.Selected = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (this.flagFormLoad)
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 255, 255);
                }

                e.Control.TextChanged += new EventHandler(this.RateDetailDataGridControl_TextChanged);
                e.Control.Validated += new EventHandler(this.RateDetailsDataGridViewControl_Validated);
                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    ((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(this.F16030_SelectionChangeCommitted);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the F16030 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F16030_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ////if (this.RateDetailsDataGridView[1, 0].Value  == 3)
            ////{
            ////    this.RateDetailsDataGridView[2, 0].Value = SharedFunctions.GetResourceString("No");
            ////}
        }

        /// <summary>
        /// Handles the Validated event of the RateDetailsDataGridViewControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridViewControl_Validated(object sender, EventArgs e)
        {
            try
            {
                this.RateDetailsDataGridView.EditingControl.TextChanged -= new EventHandler(this.RateDetailDataGridControl_TextChanged);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the RateDetailDataGridControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RateDetailDataGridControl_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    this.EnableEditMode();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion RateDetailsDataGridView Events

        #region DistributionGridView Events

        /// <summary>
        /// Handles the CellBeginEdit event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
                {
                    e.Cancel = true;
                }

                this.cellValueWhenEditing = this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value.ToString();
                //// this.EnableEditMode();
                this.flagDistributionItemDeleteEnabled = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ////(this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value.ToString() != string.Empty))
                if (!this.flagFormLoad)
                {
                    if (e.ColumnIndex == 1)
                    {
                        if ((this.pageMode == TerraScanCommon.PageModeTypes.View) && (!string.IsNullOrEmpty(this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value.ToString())))
                        {
                            this.EnableEditMode();
                            this.DistributionGridView.Columns[SharedFunctions.GetResourceString("Account")].ReadOnly = true;
                            this.flagDistributionItemDeleteEnabled = false;
                        }
                    }
                    else
                    {
                        this.EnableEditMode();
                        this.DistributionGridView.Columns[SharedFunctions.GetResourceString("Account")].ReadOnly = true;
                        this.flagDistributionItemDeleteEnabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                double taxcount = 0;
                double taxcount1 = 0.0;
                double tintcount = 0;
                double tintcount1 = 0.0;
                int tcount = 0;
                int icount = 0;
                if (this.flagDistributionItemDeleteEnabled)
                {
                    if (e.KeyValue == 46)
                    {
                        ////made changes to fix Bug#5716 & #5715
                        if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                        {
                            if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
                            {
                                for (int i = 0; i <= this.bindedDistrictDistributionDetailsDataTable.Rows.Count - 1; i++)
                                {
                                    ////if ((this.bindedDistrictDistributionDetailsDataTable.Rows[i][this.bindedDistrictDistributionDetailsDataTable.SARateItemIDColumn] != null) || (this.bindedDistrictDistributionDetailsDataTable.Rows[i][this.bindedDistrictDistributionDetailsDataTable.ItemTypeIDColumn] != null))
                                    if (this.bindedDistrictDistributionDetailsDataTable.Rows[i][this.bindedDistrictDistributionDetailsDataTable.SARateItemIDColumn] != null)
                                    {
                                        ////if ((!string.IsNullOrEmpty(this.bindedDistrictDistributionDetailsDataTable.Rows[i][this.bindedDistrictDistributionDetailsDataTable.SARateItemIDColumn].ToString())) || (!string.IsNullOrEmpty(this.bindedDistrictDistributionDetailsDataTable.Rows[i][this.bindedDistrictDistributionDetailsDataTable.ItemTypeIDColumn].ToString())))
                                        if (!string.IsNullOrEmpty(this.bindedDistrictDistributionDetailsDataTable.Rows[i][this.bindedDistrictDistributionDetailsDataTable.SARateItemIDColumn].ToString()))
                                        {
                                            if (!string.IsNullOrEmpty(this.bindedDistrictDistributionDetailsDataTable.Rows[i][this.bindedDistrictDistributionDetailsDataTable.ItemTypeIDColumn].ToString()))
                                            {
                                                if (this.bindedDistrictDistributionDetailsDataTable.Rows[i][this.bindedDistrictDistributionDetailsDataTable.ItemTypeIDColumn].ToString() == "1")
                                                {
                                                    double.TryParse(this.bindedDistrictDistributionDetailsDataTable.Rows[i][this.bindedDistrictDistributionDetailsDataTable.PercentageColumn].ToString(), out taxcount1);
                                                    taxcount = taxcount + taxcount1;
                                                    tcount = tcount + 1;
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(this.bindedDistrictDistributionDetailsDataTable.Rows[i][this.bindedDistrictDistributionDetailsDataTable.ItemTypeIDColumn].ToString()))
                                            {
                                                if (this.bindedDistrictDistributionDetailsDataTable.Rows[i][this.bindedDistrictDistributionDetailsDataTable.ItemTypeIDColumn].ToString() == "4")
                                                {
                                                    double.TryParse(this.bindedDistrictDistributionDetailsDataTable.Rows[i][this.bindedDistrictDistributionDetailsDataTable.PercentageColumn].ToString(), out tintcount1);
                                                    tintcount = tintcount + tintcount1;
                                                    icount = icount + 1;
                                                }
                                            }
                                        }
                                    }
                                }

                                if (tcount > 1 || icount > 1)
                                {
                                    ////if (tintcount == 1.0 && taxcount == 1.0)
                                    ////{
                                    this.flagDistributionItemDeleteEnabled = false;
                                    //// this.RefreshDetails(this.activeRecord);
                                    this.bindedDistrictDistributionDetailsDataTable.Rows[this.distributionGridActualRowCount].Delete();
                                    if (this.distributionGridActualRowCount < this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.Rows.Count)
                                    {
                                        this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.Rows[this.distributionGridActualRowCount].Delete();
                                        this.form1030control.WorkItem.F16030_DeleteDistrictDefinitionRate(this.selectedDistributionItem, TerraScanCommon.UserId);
                                    }

                                    this.bindedDistrictDistributionDetailsDataTable.AddGetDistrictDistributionDetailsRow(this.bindedDistrictDistributionDetailsDataTable.NewGetDistrictDistributionDetailsRow());
                                    ////this.form1030control.WorkItem.F16030_DeleteDistrictDefinitionRate(this.selectedDistributionItem, TerraScanCommon.UserId);

                                    ////}
                                    ////else
                                    ////{
                                    ////    MessageBox.Show("Record cannot be deleted");
                                    ////}
                                    ////this.EnableEditMode();                                
                                }
                                else
                                {
                                    MessageBox.Show("District Definition should have atleast one Interest and one Tax Item", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        else if (this.pageMode == TerraScanCommon.PageModeTypes.Edit || this.pageMode == TerraScanCommon.PageModeTypes.New)
                        {
                            if (this.selectedDistributionItem.Equals(0))
                            {
                                this.bindedDistrictDistributionDetailsDataTable.Rows[this.distributionGridActualRowCount].Delete();
                                this.bindedDistrictDistributionDetailsDataTable.AddGetDistrictDistributionDetailsRow(this.bindedDistrictDistributionDetailsDataTable.NewGetDistrictDistributionDetailsRow());
                            }
                            else
                            {
                                MessageBox.Show("Saved Records can not be deleted in Edit Mode", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (SoapException ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.Display, this.ParentForm);
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
        /// Handles the RowHeaderMouseClick event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.DistributionGridView.CurrentCell != null)
                {
                    this.flagDistributionItemDeleteEnabled = true;
                    this.distributionGridActualRowCount = e.RowIndex;

                    if (this.bindedDistrictDistributionDetailsDataTable.Rows[e.RowIndex][this.bindedDistrictDistributionDetailsDataTable.SARateItemIDColumn] != null)
                    {
                        if (!string.IsNullOrEmpty(this.bindedDistrictDistributionDetailsDataTable.Rows[e.RowIndex][this.bindedDistrictDistributionDetailsDataTable.SARateItemIDColumn].ToString()))
                        {
                            this.selectedDistributionItem = Convert.ToInt32(this.bindedDistrictDistributionDetailsDataTable.Rows[e.RowIndex][this.bindedDistrictDistributionDetailsDataTable.SARateItemIDColumn]);
                        }
                        else
                        {
                            this.selectedDistributionItem = 0;
                        }
                    }

                    this.DistributionGridView.CurrentCell.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.flagDistributionItemDeleteEnabled = false;
                if (e.RowIndex >= this.DistributionGridView.NumRowsVisible - 1)
                {
                    if ((e.RowIndex + 1) == this.bindedDistrictDistributionDetailsDataTable.Rows.Count)
                    {
                        this.bindedDistrictDistributionDetailsDataTable.AddGetDistrictDistributionDetailsRow(this.bindedDistrictDistributionDetailsDataTable.NewGetDistrictDistributionDetailsRow());
                        this.DistributionDetailsVscrollBar.Visible = false;
                    }
                }

                if (e.ColumnIndex == 0)
                {
                    if (this.DistributionGridView[1, e.RowIndex].Value != null && string.IsNullOrEmpty(this.DistributionGridView[1, e.RowIndex].Value.ToString()))
                    {
                        if (!string.IsNullOrEmpty(this.DistributionGridView[0, e.RowIndex].Value.ToString()))
                        {
                            Decimal defaultValue = 0;
                            if (!string.IsNullOrEmpty(this.DistributionGridView[0, e.RowIndex].Value.ToString()))
                            {
                                decimal.TryParse(this.bindedDistrictDistributionDetailsDataTable.Compute("SUM(" + this.DistributionGridView.Columns[1].DataPropertyName + ")", (this.DistributionGridView.Columns[0] as DataGridViewComboBoxColumn).ValueMember + "=" + this.DistributionGridView[0, e.RowIndex].Value.ToString()).ToString(), out defaultValue);
                            }
                            
                            ////defaultValue = (Decimal)this.bindedDistrictDistributionDetailsDataTable.Compute("SUM(" + this.DistributionGridView.Columns[1].DataPropertyName + ")", (this.DistributionGridView.Columns[0] as DataGridViewComboBoxColumn).ValueMember + "=" + this.DistributionGridView[0, e.RowIndex].Value.ToString());
                            ////decimal.TryParse(this.bindedDistrictDistributionDetailsDataTable.Compute("SUM(" + this.DistributionGridView.Columns[1].DataPropertyName + ")", (this.DistributionGridView.Columns[0] as DataGridViewComboBoxColumn).ValueMember + "=" + this.DistributionGridView[0, e.RowIndex].Value.ToString()), out defaultValue);
                            defaultValue = 1 - defaultValue;

                            if ((defaultValue > 0) && (defaultValue <= 100))
                            {
                                this.DistributionGridView[1, e.RowIndex].Value = defaultValue;
                                ////Commented by Biju on 07-Dec-2010 to fix bug found while fixing #9845
                                ////if (this.activeRecord == -99)
                                ////{
                                ////    this.DistributionGridView[1, e.RowIndex].Value = string.Empty;
                                ////}
                                ////else
                                ////{
                                ////    this.DistributionGridView[1, e.RowIndex].Value = defaultValue;
                                ////}
                                ////till here
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(this.DistributionGridView[0, e.RowIndex].Value.ToString()))
                                {
                                    this.DistributionGridView[1, e.RowIndex].Value = string.Empty;
                                }
                                else
                                {
                                    this.DistributionGridView[1, e.RowIndex].Value = 0;
                                }
                            }
                        }
                    }
                }

                if (e.ColumnIndex == 1)
                {
                    if (string.IsNullOrEmpty(this.cellValueWhenEditing))
                    {
                        if (!string.IsNullOrEmpty(this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value.ToString()))
                        {
                            this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value = Convert.ToDecimal(this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value) / 100;
                        }
                        else
                        {
                            this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value = string.Empty;
                        }
                    }
                    else
                    {
                        ////added this condition to fix Bug#5183
                        if (!this.flagPercentageHeader)
                        {
                            if (this.cellValueWhenEditing != this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value.ToString())
                            {
                                if (!string.IsNullOrEmpty(this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value.ToString()))
                                {
                                    this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value = Convert.ToDecimal(this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value) / 100;
                                }
                                else
                                {
                                    this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value = string.Empty;
                                }
                            }
                        }

                        this.flagPercentageHeader = false;
                    }
                }
                ////  }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the ColumnHeaderMouseClick event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                this.flagDistributionItemDeleteEnabled = false;
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    this.flagPercentageHeader = true;
                    this.flagDistributionSortDirection = !this.flagDistributionSortDirection;
                    this.distributionGridViewSelectColumnNo = e.ColumnIndex;
                    this.PopulateDistributionDetailsGridView();

                    ////added code to fix Bug#4192
                    int itemFoundDist = this.distributionSource.Find("SARateItemID", this.selectedDistributionItem);
                    TerraScanCommon.SetDataGridViewPosition(this.DistributionGridView, itemFoundDist);

                    if (this.flagDistributionSortDirection)
                    {
                        this.DistributionGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                    else
                    {
                        this.DistributionGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }


                    this.flagPercentageHeader = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.flagDistributionItemDeleteEnabled = false;
                for (int i = 0; i < this.DistributionGridView.Rows.Count; i++)
                {
                    TerraScanTextAndImageCell imgCell = (TerraScanTextAndImageCell)this.DistributionGridView[2, i];
                    imgCell.ImagexLocation = 295;
                    imgCell.ImageyLocation = 3;
                    if (e.RowIndex == i)
                    {
                        imgCell.Image = Properties.Resources.Abutton;
                    }
                    else
                    {
                        if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0) && (e.RowIndex < (this.RateDetailsDataGridView.RowCount - 1)))
                        {
                            imgCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.RateDetailsDataGridView[e.ColumnIndex, e.RowIndex].InheritedStyle.BackColor.R), Convert.ToInt32(this.RateDetailsDataGridView[e.ColumnIndex, e.RowIndex].InheritedStyle.BackColor.G), Convert.ToInt32(this.RateDetailsDataGridView[e.ColumnIndex, e.RowIndex].InheritedStyle.BackColor.B));
                        }
                    }

                    this.DistributionGridView.Refresh();
                }
                // Initialise the rowIndex 
                int rowIndex = -1;

                // Checks the rowindex is valid
                if (e.RowIndex >= 0 && this.DistributionGridView.CurrentCell != null)
                {
                    // Set the current RowIndex
                    rowIndex = e.RowIndex;
                }
                else if (this.DistributionGridView.CurrentRowIndex >= 0 && this.DistributionGridView.CurrentCell != null)
                {
                    // Set the Current rowindex
                    rowIndex = DistributionGridView.CurrentRowIndex;
                }

                if (rowIndex >= 0)
                {
                    if (this.bindedDistrictDistributionDetailsDataTable.Rows[rowIndex][this.bindedDistrictDistributionDetailsDataTable.SARateItemIDColumn] != null)
                    {
                        if (!string.IsNullOrEmpty(this.bindedDistrictDistributionDetailsDataTable.Rows[rowIndex][this.bindedDistrictDistributionDetailsDataTable.SARateItemIDColumn].ToString()))
                        {
                            this.selectedDistributionItem = Convert.ToInt32(this.bindedDistrictDistributionDetailsDataTable.Rows[rowIndex][this.bindedDistrictDistributionDetailsDataTable.SARateItemIDColumn]);
                        }
                        else
                        {
                            this.selectedDistributionItem = 0;
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
        /// Handles the DataBindingComplete event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (this.DistributionGridView.Rows.Count > 0)
            {
                try
                {
                    this.flagDistributionItemDeleteEnabled = false;
                    for (int i = 0; i < this.DistributionGridView.Rows.Count; i++)
                    {
                        TerraScanTextAndImageCell imgCell = (TerraScanTextAndImageCell)this.DistributionGridView[2, i];
                        imgCell.ImagexLocation = 295;
                        imgCell.ImageyLocation = 3;
                        imgCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.DistributionGridView[0, i].InheritedStyle.BackColor.R), Convert.ToInt32(this.DistributionGridView[0, i].InheritedStyle.BackColor.G), Convert.ToInt32(this.DistributionGridView[0, i].InheritedStyle.BackColor.B));
                    }

                    // Set the cell color to lightgreen when account status equals 1
                    foreach (DataGridViewRow currentRow in this.DistributionGridView.Rows)
                    {
                        if (currentRow.Cells[SharedFunctions.GetResourceString("AccountStatus")].Value.ToString() == "1")
                        {
                            currentRow.Cells[SharedFunctions.GetResourceString("Account")].Style.BackColor = Color.FromArgb(204, 255, 204);
                        }
                    }

                    this.DistributionGridView.Refresh();
                }
                catch (Exception ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }

                this.DistributionGridView.Rows[0].Selected = false;
            }
        }

        /// <summary>
        /// Handles the CellMouseClick event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (this.flagAccountShow)
                {
                    this.DistributionGridView.Columns[SharedFunctions.GetResourceString("Account")].ReadOnly = true;
                    if ((e.ColumnIndex == 2) && (e.RowIndex >= 0) && (e.ColumnIndex >= 0))
                    {
                        if (!this.DistributionGridView.Rows[e.RowIndex].ReadOnly)
                        {
                            if ((e.X >= 295) && (e.X <= (326 - 16)) && (e.Y >= 3) && (e.Y <= (22 - 3)))
                            {
                                bool tempAccountStatus;
                                int accountId = 0;
                                int rollYear;
                                bool flagrollYear;
                                string selectedAccountName = string.Empty;
                                object[] optionalParameter = new object[1];
                                flagrollYear = int.TryParse(this.RollYearTextBox.Text, out rollYear);
                                if (flagrollYear)
                                {
                                    optionalParameter[0] = rollYear;
                                }
                                else
                                {
                                    optionalParameter[0] = this.defaultRollYear;
                                }

                                Form accountSelectionForm = TerraScanCommon.GetForm(1345, optionalParameter, this.form1030control.WorkItem);
                                if (accountSelectionForm != null) ////&& accountSelectionForm.ShowDialog() == DialogResult.Yes)
                                {
                                    if (accountSelectionForm.ShowDialog() == DialogResult.OK)
                                    {
                                        ////int.TryParse(TerraScanCommon.GetValue(accountSelectionForm, SharedFunctions.GetResourceString("AccountID")).ToString(), out accountId);
                                        int.TryParse(TerraScanCommon.GetValue(accountSelectionForm, "AccountId").ToString(), out accountId);
                                        ExciseTaxRateData accountNameDataSet = new ExciseTaxRateData();
                                        accountNameDataSet = this.form1030control.WorkItem.GetAccountName(accountId);

                                        if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                                        {
                                            tempAccountStatus = Convert.ToBoolean(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString());
                                            selectedAccountName = accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctColumn].ToString();
                                            this.DistributionGridView[3, e.RowIndex].Value = 0;
                                            if (tempAccountStatus)
                                            {
                                                this.DistributionGridView[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.FromArgb(204, 255, 204);
                                                this.DistributionGridView[3, e.RowIndex].Value = 1;
                                            }
                                            else
                                            {
                                                this.DistributionGridView[e.ColumnIndex, e.RowIndex].Style.BackColor = this.DistributionGridView[0, e.RowIndex].Style.BackColor;
                                                this.DistributionGridView[3, e.RowIndex].Value = 0;
                                            }

                                            this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value = selectedAccountName;
                                            this.DistributionGridView[4, e.RowIndex].Value = accountId;

                                            if (e.RowIndex >= this.DistributionGridView.NumRowsVisible - 1)
                                            {
                                                if ((e.RowIndex + 1) == this.bindedDistrictDistributionDetailsDataTable.Rows.Count)
                                                {
                                                    this.bindedDistrictDistributionDetailsDataTable.AddGetDistrictDistributionDetailsRow(this.bindedDistrictDistributionDetailsDataTable.NewGetDistrictDistributionDetailsRow());
                                                    this.DistributionDetailsVscrollBar.Visible = false;
                                                }
                                            }
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
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellParsing event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                Decimal outDecimal;

                // Only paint if desired column
                if (e.ColumnIndex == this.DistributionGridView.Columns[SharedFunctions.GetResourceString("F36000PercentageColumnName")].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    //// Only paint if text provided, Only paint if desired text is in cell
                    if (e.Value != null)
                    {
                        string tempvalue = e.Value.ToString().Trim();
                        if (tempvalue != "")
                        {
                            if (tempvalue.Contains("%"))
                            {
                                tempvalue = tempvalue.Replace("%", "");
                            }

                            if (tempvalue.EndsWith("."))
                            {
                                tempvalue = string.Concat(tempvalue, SharedFunctions.GetResourceString("Zero"));
                            }

                            if (Decimal.TryParse(tempvalue, NumberStyles.Currency, null, out outDecimal))
                            {
                                tempvalue = outDecimal.ToString();
                            }

                            if (outDecimal.ToString().Contains("-"))
                            {
                                outDecimal = 0;
                            }

                            e.Value = outDecimal;
                            e.ParsingApplied = true;
                        }
                        else
                        {
                            e.Value = decimal.Parse("0");
                            e.ParsingApplied = true;
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
        /// Handles the Enter event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_Enter(object sender, EventArgs e)
        {
            try
            {
                if (this.DistributionGridView.OriginalRowCount > 0)
                {
                    this.DistributionGridView.Rows[0].Cells[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.DistributionGridView.CurrentCell != null)
                {
                    this.DistributionGridView.CurrentCell.Selected = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.RowIndex.Equals(0))
                    {
                        this.DistributionGridView.Rows[0].ReadOnly = false;
                    }

                    if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                    {
                        if (e.RowIndex > 1)
                        {
                            if (string.IsNullOrEmpty(this.DistributionGridView[0, e.RowIndex - 1].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DistributionGridView[1, e.RowIndex - 1].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DistributionGridView[2, e.RowIndex - 1].Value.ToString().Trim()) && (e.RowIndex > 0))
                            {
                                this.DistributionGridView.Rows[e.RowIndex].ReadOnly = true;
                            }
                            else
                            {
                                this.DistributionGridView.Rows[e.RowIndex].ReadOnly = false;
                            }
                        }
                    }
                    else
                    {
                        if (e.RowIndex >= 1)
                        {
                            if (string.IsNullOrEmpty(this.DistributionGridView[0, e.RowIndex - 1].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DistributionGridView[1, e.RowIndex - 1].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DistributionGridView[2, e.RowIndex - 1].Value.ToString().Trim()) && (e.RowIndex > 0))
                            {
                                this.DistributionGridView.Rows[e.RowIndex].ReadOnly = true;
                            }
                            else
                            {
                                this.DistributionGridView.Rows[e.RowIndex].ReadOnly = false;
                            }
                        }
                        else
                        {
                            if (this.DistributionGridView.Rows.Count >= 2)
                            {
                                this.DistributionGridView.Rows[1].ReadOnly = true;
                            }
                        }
                    }
                }

                if (!this.flagFormLoad)
                {
                    this.flagDistributionItemDeleteEnabled = false;
                    for (int i = 0; i < this.DistributionGridView.Rows.Count; i++)
                    {
                        TerraScanTextAndImageCell imgCell = (TerraScanTextAndImageCell)this.DistributionGridView[2, i];
                        imgCell.ImagexLocation = 295;
                        imgCell.ImageyLocation = 3;
                        if (e.RowIndex == i)
                        {
                            imgCell.Image = Properties.Resources.Abutton;
                        }
                        else
                        {
                            if ((e.RowIndex >= 0) && (e.ColumnIndex > 0))
                            {
                                imgCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.RateDetailsDataGridView[e.ColumnIndex, e.RowIndex].InheritedStyle.BackColor.R), Convert.ToInt32(this.RateDetailsDataGridView[e.ColumnIndex, e.RowIndex].InheritedStyle.BackColor.G), Convert.ToInt32(this.RateDetailsDataGridView[e.ColumnIndex, e.RowIndex].InheritedStyle.BackColor.B));
                            }
                        }
                    }

                    this.DistributionGridView.Refresh();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.JustLog, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (this.flagFormLoad)
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 255, 255);
                }

                e.Control.TextChanged += new EventHandler(this.DistributionGridViewControl_TextChanged);
                e.Control.Validated += new EventHandler(this.DistributionGridViewControl_Validated);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validated event of the DistributionGridViewControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistributionGridViewControl_Validated(object sender, EventArgs e)
        {
            try
            {
                this.DistributionGridView.EditingControl.TextChanged -= new EventHandler(this.DistributionGridViewControl_TextChanged);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the DistributionGridViewControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistributionGridViewControl_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    this.texteditflag = true;
                    this.EnableEditMode();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion DistributionGridView Events

        #endregion Events

        #region Private Member Methods

        /// <summary>
        /// Loads the list information.
        /// </summary>
        private void LoadListInformation()
        {
            foreach (DataTable dataTable in this.form1030DistrictDefinitionData.Tables)
            {
                dataTable.Rows.Clear();
            }

            this.form1030DistrictDefinitionData = this.form1030control.WorkItem.F16030_ListDistrictDefinitionType();
            this.TypeComboBox.DisplayMember = this.form1030DistrictDefinitionData.DistrictPostingList.PostNameColumn.ColumnName;
            this.TypeComboBox.ValueMember = this.form1030DistrictDefinitionData.DistrictPostingList.PostTypeIDColumn.ColumnName;

            if (this.form1030DistrictDefinitionData.DistrictPostingList.Rows.Count > 0)
            {
                this.TypeComboBox.DataSource = this.form1030DistrictDefinitionData.DistrictPostingList.Copy();
                this.TypeComboBox.SelectedValue = 12;
            }

            if (this.form1030DistrictDefinitionData.ListDistrictDefinitionID.Rows.Count <= 0)
            {
                this.DisableOnEmptyRecord(false);
            }
            else
            {
                this.DisableOnEmptyRecord(true);
            }
        }

        /// <summary>
        /// Fills the details.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        private void FillDetails(int keyId)
        {
            this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Clear();
            this.form1030DistrictDefinitionData.GetDistrictRateDetails.Clear();
            this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.Clear();
            this.bindedDistrictDistributionDetailsDataTable.Clear();
            this.bindedDistrictRateDetailsDataTable.Clear();
            this.form1030DistrictDefinitionData.Merge(this.form1030control.WorkItem.F16030_GetDistrictDefinitionDetails(keyId), true);

            if (this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictColumn].ToString()))
                {
                    this.DistrictNumberTextBox.Text = this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictColumn].ToString();
                }

                if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.RollYearColumn].ToString()))
                {
                    this.defaultRollYear = Convert.ToInt32(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.RollYearColumn]);
                    this.RollYearTextBox.Text = this.defaultRollYear.ToString();
                }

                if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.MinimumDistrictFeeColumn].ToString()))
                {
                    this.MinimumDistrictFeeTextBox.Text = Convert.ToDecimal(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.MinimumDistrictFeeColumn]).ToString("$ #,##0.00");
                }

                if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.PostTypeIDColumn].ToString()))
                {
                    this.TypeComboBox.SelectedValue = this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.PostTypeIDColumn];
                }

                if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.SANameColumn].ToString()))
                {
                    this.DistrictNameTextBox.Text = this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.SANameColumn].ToString();
                }
                ///Used to Show  the District Info Fields Saved.
                if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoA1Column].ToString()))
                {
                    this.DistrictInfoA1TextBox.Text = this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoA1Column].ToString();
                }
                if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoA2Column].ToString()))
                {
                    this.DistrictInfoA2TextBox.Text = this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoA2Column].ToString();
                }
                if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoA3Column].ToString()))
                {
                    this.DistrictInfoA3TextBox.Text = this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoA3Column].ToString();
                }
                if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoA4Column].ToString()))
                {
                    this.DistrictInfoA4TextBox.Text = this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoA4Column].ToString();
                }
                if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoA5Column].ToString()))
                {
                    this.DistrictInfoA5TextBox.Text = this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoA5Column].ToString();
                }
                if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoA6Column].ToString()))
                {
                    this.DistrictInfoA6TextBox.Text = this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoA6Column].ToString();
                }
                if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoB1Column].ToString()))
                {
                    this.DistrictInfoB1TextBox.Text = this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoB1Column].ToString();
                }
                if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoB2Column].ToString()))
                {
                    this.DistrictInfoB2TextBox.Text = this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoB2Column].ToString();
                }
                if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoB3Column].ToString()))
                {
                    this.DistrictInfoB3TextBox.Text = this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoB3Column].ToString();
                }
                if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoB4Column].ToString()))
                {
                    this.DistrictInfoB4TextBox.Text = this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoB4Column].ToString();
                }
                if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoB5Column].ToString()))
                {
                    this.DistrictInfoB5TextBox.Text = this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoB5Column].ToString();
                }
                if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoB6Column].ToString()))
                {
                    this.DistrictInfoB6TextBox.Text = this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictInfoB6Column].ToString();
                }
                if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.IsHalfPaymentAllowedColumn].ToString()))
                {
                    if (this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.IsHalfPaymentAllowedColumn].ToString().ToLower().Equals("true"))
                    {
                        this.AllowHalfpaymentComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        this.AllowHalfpaymentComboBox.SelectedIndex = 1;
                    }
                }
                else
                {
                    this.AllowHalfpaymentComboBox.SelectedIndex = 0;
                }

              if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                {
                    this.EnableControls(true);

                    if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictStatementColumn].ToString()))
                    {
                        if (this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictStatementColumn].ToString() == "1")
                        {
                            this.DistrictInfoA1TextBox.LockKeyPress = false;
                            this.DistrictInfoA2TextBox.LockKeyPress = false;
                            this.DistrictInfoA3TextBox.LockKeyPress = false;
                            this.DistrictInfoA4TextBox.LockKeyPress = false;
                            this.DistrictInfoA5TextBox.LockKeyPress = false;
                            this.DistrictInfoA6TextBox.LockKeyPress = false;
                            this.DistrictInfoB1TextBox.LockKeyPress = false;
                            this.DistrictInfoB2TextBox.LockKeyPress = false;
                            this.DistrictInfoB3TextBox.LockKeyPress = false;
                            this.DistrictInfoB4TextBox.LockKeyPress = false;
                            this.DistrictInfoB5TextBox.LockKeyPress = false;
                            this.DistrictInfoB6TextBox.LockKeyPress = false;
                            this.DistrictNumberTextBox.LockKeyPress = false;
                            this.RollYearTextBox.LockKeyPress = true;
                            this.MinimumDistrictFeeTextBox.LockKeyPress = true;
                            this.DistrictNameTextBox.LockKeyPress = false;
                            this.TypeComboBox.Enabled = false;
                            this.RateDetailsDataGridView.ReadOnly = true;
                            this.DistributionGridView.ReadOnly = true;
                            this.flagAccountShow = false;
                            this.DistrictCopyButton.Enabled = false;
                        }
                        else
                        {
                            this.DistrictInfoA1TextBox.LockKeyPress = false;
                            this.DistrictInfoA2TextBox.LockKeyPress = false;
                            this.DistrictInfoA3TextBox.LockKeyPress = false;
                            this.DistrictInfoA4TextBox.LockKeyPress = false;
                            this.DistrictInfoA5TextBox.LockKeyPress = false;
                            this.DistrictInfoA6TextBox.LockKeyPress = false;
                            this.DistrictInfoB1TextBox.LockKeyPress = false;
                            this.DistrictInfoB2TextBox.LockKeyPress = false;
                            this.DistrictInfoB3TextBox.LockKeyPress = false;
                            this.DistrictInfoB4TextBox.LockKeyPress = false;
                            this.DistrictInfoB5TextBox.LockKeyPress = false;
                            this.DistrictInfoB6TextBox.LockKeyPress = false;
                            this.DistrictNumberTextBox.LockKeyPress = false;
                            this.DistrictNumberTextBox.LockKeyPress = false;
                            this.RollYearTextBox.LockKeyPress = false;
                            this.MinimumDistrictFeeTextBox.LockKeyPress = false;
                            this.DistrictNameTextBox.LockKeyPress = false;
                            this.TypeComboBox.Enabled = true;
                            this.RateDetailsDataGridView.ReadOnly = false;
                            this.DistributionGridView.ReadOnly = false;
                            this.flagAccountShow = true;
                        }
                    }
                }
                else
                {
                    this.EnableControls(false);
                    this.LockControls(true);
                }
            }
            else
            {
                this.EnableControls(false);
                this.LockControls(true);
            }

            this.CustomizeGridView();
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <returns>the status of the page</returns>
        private bool CheckPageStatus()
        {
            DialogResult dialogResult;
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    return this.SaveSpecialDistrictDefinition();
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    return true;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Saves the special district definition.
        /// </summary>
        /// <returns>result of save operation</returns>
        private bool SaveSpecialDistrictDefinition()
        {
            bool result = true;
            if (string.IsNullOrEmpty(this.MinimumDistrictFeeTextBox.Text.Replace("$", "")))
            {
                this.MinimumDistrictFeeTextBox.Text = SharedFunctions.GetResourceString("Zero");
            }

            F1030SpecialDistrictDefinitionData.GetDistrictDefinitionDetailsDataTable districtDefinitionDetailsDataTable = new F1030SpecialDistrictDefinitionData.GetDistrictDefinitionDetailsDataTable();
            F1030SpecialDistrictDefinitionData.GetDistrictDefinitionDetailsRow saveDistrictDefinitionDetailsRow;
            saveDistrictDefinitionDetailsRow = districtDefinitionDetailsDataTable.NewGetDistrictDefinitionDetailsRow();
            string districtItem = string.Empty, rateItem = string.Empty, distributionItem = string.Empty, primaryKeyID = string.Empty;
            int districtNo = 0;

            saveDistrictDefinitionDetailsRow.District = Convert.ToInt32(this.DistrictNumberTextBox.Text);

            if (string.IsNullOrEmpty(this.MinimumDistrictFeeTextBox.Text))
            {
                saveDistrictDefinitionDetailsRow.MinimumDistrictFee = 0;
            }
            else
            {
                saveDistrictDefinitionDetailsRow.MinimumDistrictFee = Convert.ToDecimal(this.MinimumDistrictFeeTextBox.Text.Replace('$', ' ').Trim());
            }

            saveDistrictDefinitionDetailsRow.PostTypeID = Convert.ToInt16(this.TypeComboBox.SelectedValue);
            saveDistrictDefinitionDetailsRow.RollYear = Convert.ToInt16(this.RollYearTextBox.Text);

            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                saveDistrictDefinitionDetailsRow.SADistrictID = Convert.ToInt32(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.SADistrictIDColumn]);
            }
            else
            {
                saveDistrictDefinitionDetailsRow.SADistrictID = 0;
            }

            saveDistrictDefinitionDetailsRow.SAName = this.DistrictNameTextBox.Text.Trim();
            ///Used to Save  the District Info Fields
            saveDistrictDefinitionDetailsRow.DistrictInfoA1 = this.DistrictInfoA1TextBox.Text.Trim();
            saveDistrictDefinitionDetailsRow.DistrictInfoA2 = this.DistrictInfoA2TextBox.Text.Trim();
            saveDistrictDefinitionDetailsRow.DistrictInfoA3 = this.DistrictInfoA3TextBox.Text.Trim();
            saveDistrictDefinitionDetailsRow.DistrictInfoA4 = this.DistrictInfoA4TextBox.Text.Trim();
            saveDistrictDefinitionDetailsRow.DistrictInfoA5 = this.DistrictInfoA5TextBox.Text.Trim();
            saveDistrictDefinitionDetailsRow.DistrictInfoA6 = this.DistrictInfoA6TextBox.Text.Trim();
            saveDistrictDefinitionDetailsRow.DistrictInfoB1 = this.DistrictInfoB1TextBox.Text.Trim();
            saveDistrictDefinitionDetailsRow.DistrictInfoB2 = this.DistrictInfoB2TextBox.Text.Trim();
            saveDistrictDefinitionDetailsRow.DistrictInfoB3 = this.DistrictInfoB3TextBox.Text.Trim();
            saveDistrictDefinitionDetailsRow.DistrictInfoB4 = this.DistrictInfoB4TextBox.Text.Trim();
            saveDistrictDefinitionDetailsRow.DistrictInfoB5 = this.DistrictInfoB5TextBox.Text.Trim();
            saveDistrictDefinitionDetailsRow.DistrictInfoB6 = this.DistrictInfoB6TextBox.Text.Trim();
            if (this.AllowHalfpaymentComboBox.SelectedItem != null)
            {
                if (this.AllowHalfpaymentComboBox.SelectedItem.ToString().ToLower().Equals("yes"))
                {
                    saveDistrictDefinitionDetailsRow.IsHalfPaymentAllowed = true;
                }
                else
                {
                    saveDistrictDefinitionDetailsRow.IsHalfPaymentAllowed = false;
                }
            }
            else
            {
                saveDistrictDefinitionDetailsRow.IsHalfPaymentAllowed = true;
            }
            districtDefinitionDetailsDataTable.Rows.Add(saveDistrictDefinitionDetailsRow);

            if (this.flagCopyDistrict)
            {
                districtNo = 0;
            }
            else
            {
                districtNo = Convert.ToInt32(this.DistrictNumberTextBox.Text);
            }

            districtItem = Utility.GetXmlString(districtDefinitionDetailsDataTable);
            rateItem = Utility.GetXmlString(this.bindedDistrictRateDetailsDataTable);
            distributionItem = Utility.GetXmlString(this.bindedDistrictDistributionDetailsDataTable);
            distributionItem = distributionItem.Replace("%", "");
            if (this.flagOverwrite)
            {
                primaryKeyID = this.form1030control.WorkItem.F16030_SaveDistrictDefinition(districtNo, districtItem, rateItem, distributionItem, true, false, TerraScanCommon.UserId);
            }
            else
            {
                primaryKeyID = this.form1030control.WorkItem.F16030_SaveDistrictDefinition(districtNo, districtItem, rateItem, distributionItem, false, false, TerraScanCommon.UserId);
            }

            result = int.TryParse(primaryKeyID, out this.activeRecord);
            if (result)
            {
                SliceReloadActiveRecord currentSliceInfo;
                currentSliceInfo.MasterFormNo = this.masterFormNo;
                currentSliceInfo.SelectedKeyId = this.activeRecord;
                this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.DistrictCopyButton.Enabled = false;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// News the button_ click.
        /// </summary>
        private void NewButtonClick()
        {
            if (!this.flagCopyDistrict)
            {
                this.ClearSpecialDistrictDetails();
                this.DisableOnEmptyRecord(true);
                this.EnableNewMode();
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.MinimumDistrictFeeTextBox.Text = 0.ToString("$ #,##0.00");
                if (this.RateDetailsDataGridView.Rows.Count > 0)
                {
                    this.RateDetailsDataGridView[1, 0].Value = 3;
                    this.RateDetailsDataGridView[2, 0].Value = SharedFunctions.GetResourceString("No");
                }
            }

            this.DistrictNumberTextBox.LockKeyPress = false;
            this.RollYearTextBox.LockKeyPress = false;
            this.MinimumDistrictFeeTextBox.LockKeyPress = false;
            this.DistrictNameTextBox.LockKeyPress = false;
            this.TypeComboBox.Enabled = true;
            this.RateDetailsDataGridView.ReadOnly = false;
            this.DistributionGridView.ReadOnly = false;
            this.DistrictNumberTextBox.Focus();            
            this.flagAccountShow = true;
            if (!isDistrictclicked)
            {
                this.AllowHalfpaymentComboBox.SelectedIndex = 0;
            }
            else
            {
                isDistrictclicked = false;
            }
        }

        /// <summary>
        /// Clears the special district details.
        /// </summary>
        private void ClearSpecialDistrictDetails()
        {
            this.TypeComboBox.SelectedValue = 12;
            this.DistrictNumberTextBox.Text = string.Empty;
            this.SetDefaultRollYear();
            this.MinimumDistrictFeeTextBox.Text = string.Empty;
            this.DistrictNameTextBox.Text = string.Empty;
            this.DistrictInfoA1TextBox.Text = string.Empty;
            this.DistrictInfoA2TextBox.Text = string.Empty;
            this.DistrictInfoA3TextBox.Text = string.Empty;
            this.DistrictInfoA4TextBox.Text = string.Empty;
            this.DistrictInfoA5TextBox.Text = string.Empty;
            this.DistrictInfoA6TextBox.Text = string.Empty;
            this.DistrictInfoB1TextBox.Text = string.Empty;
            this.DistrictInfoB2TextBox.Text = string.Empty;
            this.DistrictInfoB3TextBox.Text = string.Empty;
            this.DistrictInfoB4TextBox.Text = string.Empty;
            this.DistrictInfoB5TextBox.Text = string.Empty;
            this.DistrictInfoB6TextBox.Text = string.Empty;
            this.bindedDistrictDistributionDetailsDataTable.Clear();
            this.bindedDistrictRateDetailsDataTable.Clear();
        }

        /// <summary>
        /// Enables the new mode.
        /// </summary>
        private void EnableNewMode()
        {
            this.SetGridColumnSettings();
            this.bindedDistrictDistributionDetailsDataTable.Clear();
            this.bindedDistrictRateDetailsDataTable.Clear();
            for (int row = 0; row < this.RateDetailsDataGridView.NumRowsVisible; row++)
            {
                this.bindedDistrictRateDetailsDataTable.AddGetDistrictRateDetailsRow(this.bindedDistrictRateDetailsDataTable.NewGetDistrictRateDetailsRow());
                this.bindedDistrictDistributionDetailsDataTable.AddGetDistrictDistributionDetailsRow(this.bindedDistrictDistributionDetailsDataTable.NewGetDistrictDistributionDetailsRow());
            }

            if (this.form1030DistrictDefinitionData.ListDistrictDefinitionID.Rows.Count <= 0)
            {
                this.RateDetailsDataGridView.AutoGenerateColumns = false;
                this.DistributionGridView.AutoGenerateColumns = false;
                if (this.bindedDistrictRateDetailsDataTable.Rows.Count > 0)
                {
                    DistrictCopyButton.Enabled = true;
                }
                else
                {
                    DistrictCopyButton.Enabled = false;
                }
                ////added code to fix Bug#4192
                this.RateDetailsDataGridView.DataSource = this.bindedDistrictRateDetailsDataTable.DefaultView;
                ////Commented by Biju on 07-Dec-2010 to fix #9845
                ////int itemFound = this.rateSource.Find("RateItemID", this.selectedRateItem);

                this.DistributionGridView.DataSource = this.bindedDistrictDistributionDetailsDataTable.DefaultView;
                int itemFoundDist = this.distributionSource.Find("SARateItemID", this.selectedDistributionItem);

            }

            this.DistrictCopyButton.Enabled = false;
        }

        /// <summary>
        /// Enables the edit mode.
        /// </summary>
        private void EnableEditMode()
        {
            if (this.PermissionEdit && (this.pageMode == TerraScanCommon.PageModeTypes.View))
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                this.DistrictCopyButton.Enabled = false;
            }
        }

        /// <summary>
        /// This Method used to  Allign The Header width , Column width , And Font Size for 
        /// CommentsDataGridView
        /// </summary>
        private void CustomizeGridView()
        {
            this.SetGridColumnSettings();
            this.PopulateRateDetailsGridView();
            this.PopulateDistributionDetailsGridView();
        }

        /// <summary>
        /// Sets the grid column settings.
        /// </summary>
        private void SetGridColumnSettings()
        {
            // RateDetailsDataGrid Properties
            this.RateDetailsDataGridView.AutoGenerateColumns = false;
            this.RateDetailsDataGridView.ApplyStandardBehaviour = true;

            // RateDetailsDataGrid Column Properties
            DataGridViewColumnCollection rateDetailsColumns = this.RateDetailsDataGridView.Columns;
            if (rateDetailsColumns.Count > 0)
            {
                rateDetailsColumns[SharedFunctions.GetResourceString("RateDescription")].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictRateDetails.DescriptionColumn.ColumnName;
                rateDetailsColumns[SharedFunctions.GetResourceString("RateType")].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictRateDetails.ItemTypeIDColumn.ColumnName;
                rateDetailsColumns[SharedFunctions.GetResourceString("OneAcreMin")].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictRateDetails.HasMinimumColumn.ColumnName;
                rateDetailsColumns[SharedFunctions.GetResourceString("Amount")].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictRateDetails.AmountColumn.ColumnName;
                rateDetailsColumns[SharedFunctions.GetResourceString("SARateItemIDRate")].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictRateDetails.SARateItemIDColumn.ColumnName;
                rateDetailsColumns[SharedFunctions.GetResourceString("SADistrictIDRate")].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictRateDetails.SADistrictIDColumn.ColumnName;
                rateDetailsColumns[SharedFunctions.GetResourceString("RateDescription")].DisplayIndex = 0;
                rateDetailsColumns[SharedFunctions.GetResourceString("RateType")].DisplayIndex = 1;
                rateDetailsColumns[SharedFunctions.GetResourceString("OneAcreMin")].DisplayIndex = 2;
                rateDetailsColumns[SharedFunctions.GetResourceString("Amount")].DisplayIndex = 3;

                // RateDetailsDataGrid RateType Column Properties for data binding
                (this.RateDetailsDataGridView.Columns[SharedFunctions.GetResourceString("RateType")] as DataGridViewComboBoxColumn).DataSource = this.form1030DistrictDefinitionData.DistrictRateList;
                (this.RateDetailsDataGridView.Columns[SharedFunctions.GetResourceString("RateType")] as DataGridViewComboBoxColumn).DisplayMember = this.form1030DistrictDefinitionData.DistrictRateList.ItemTypeColumn.ColumnName;
                (this.RateDetailsDataGridView.Columns[SharedFunctions.GetResourceString("RateType")] as DataGridViewComboBoxColumn).ValueMember = this.form1030DistrictDefinitionData.DistrictRateList.ItemTypeIDColumn.ColumnName;

                // RateDetailsDataGrid OneAcreMin Column Properties for data binding
                (this.RateDetailsDataGridView.Columns[SharedFunctions.GetResourceString("OneAcreMin")] as DataGridViewComboBoxColumn).DataSource = this.commonData.ComboKeyStringDataTable;
                (this.RateDetailsDataGridView.Columns[SharedFunctions.GetResourceString("OneAcreMin")] as DataGridViewComboBoxColumn).DisplayMember = this.commonData.ComboKeyStringDataTable.KeyNameColumn.ColumnName;
                (this.RateDetailsDataGridView.Columns[SharedFunctions.GetResourceString("OneAcreMin")] as DataGridViewComboBoxColumn).ValueMember = this.commonData.ComboKeyStringDataTable.KeyIdColumn.ColumnName;

                DataGridViewColumnCollection distributionDetailsColumns = this.DistributionGridView.Columns;
                this.DistributionGridView.AutoGenerateColumns = false;
                this.DistributionGridView.ApplyStandardBehaviour = true;
                distributionDetailsColumns[SharedFunctions.GetResourceString("DistributionType")].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.ItemTypeIDColumn.ColumnName;
                distributionDetailsColumns[SharedFunctions.GetResourceString("F36000PercentageColumnName")].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.PercentageColumn.ColumnName;
                distributionDetailsColumns[SharedFunctions.GetResourceString("Account")].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.AccountColumn.ColumnName;
                distributionDetailsColumns[SharedFunctions.GetResourceString("AccountStatus")].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.AccountStatusColumn.ColumnName;
                distributionDetailsColumns[SharedFunctions.GetResourceString("AccountID")].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.AccountIDColumn.ColumnName;
                distributionDetailsColumns[SharedFunctions.GetResourceString("SARateItemID")].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.SARateItemIDColumn.ColumnName;
                distributionDetailsColumns[SharedFunctions.GetResourceString("SADistrictID")].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.SADistrictIDColumn.ColumnName;
                distributionDetailsColumns[SharedFunctions.GetResourceString("DistributionType")].DisplayIndex = 0;
                distributionDetailsColumns[SharedFunctions.GetResourceString("F36000PercentageColumnName")].DisplayIndex = 1;
                distributionDetailsColumns[SharedFunctions.GetResourceString("Account")].DisplayIndex = 2;
                (this.DistributionGridView.Columns[SharedFunctions.GetResourceString("DistributionType")] as DataGridViewComboBoxColumn).DataSource = this.form1030DistrictDefinitionData.DistrictDistributionTypeList;
                (this.DistributionGridView.Columns[SharedFunctions.GetResourceString("DistributionType")] as DataGridViewComboBoxColumn).DisplayMember = this.form1030DistrictDefinitionData.DistrictDistributionTypeList.DistributionTypeColumn.ColumnName;
                (this.DistributionGridView.Columns[SharedFunctions.GetResourceString("DistributionType")] as DataGridViewComboBoxColumn).ValueMember = this.form1030DistrictDefinitionData.DistrictDistributionTypeList.ItemTypeIDColumn.ColumnName;
            }
        }

        /// <summary>
        /// Populates the rate details grid view.
        /// </summary>
        private void PopulateRateDetailsGridView()
        {
            // clear the table for repopulating the data
            this.bindedDistrictRateDetailsDataTable.Clear();
            // Bud ID 558 fixed
            if (this.RateDetailsDataGridView.Columns.Count > 0)
            {
                // create form level datatable for ratelist information
                F1030SpecialDistrictDefinitionData.GetDistrictRateDetailsRow[] tempTable;
                if (this.flagRateSortDirection)
                {
                    tempTable = (F1030SpecialDistrictDefinitionData.GetDistrictRateDetailsRow[])this.form1030DistrictDefinitionData.GetDistrictRateDetails.Copy().Select("1=1", this.RateDetailsDataGridView.Columns[this.rateGridViewSelectColumnNo].DataPropertyName + SharedFunctions.GetResourceString("DESC"));
                    this.RateDetailsDataGridView.Columns[this.rateGridViewSelectColumnNo].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                }
                else
                {
                    tempTable = (F1030SpecialDistrictDefinitionData.GetDistrictRateDetailsRow[])this.form1030DistrictDefinitionData.GetDistrictRateDetails.Copy().Select("1=1", this.RateDetailsDataGridView.Columns[this.rateGridViewSelectColumnNo].DataPropertyName + SharedFunctions.GetResourceString("ASC"));
                    this.RateDetailsDataGridView.Columns[this.rateGridViewSelectColumnNo].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                }
                //// add the rows after sorting
                foreach (F1030SpecialDistrictDefinitionData.GetDistrictRateDetailsRow myrow in tempTable)
                {
                    this.bindedDistrictRateDetailsDataTable.ImportRow(myrow);
                }
            }

            // bind the table values to the grid
            if (this.bindedDistrictRateDetailsDataTable.Rows.Count > 0 && this.slicePermissionField.newPermission)
            {
                DistrictCopyButton.Enabled = true;
            }
            else
            {
                DistrictCopyButton.Enabled = false;
            }

            this.RateDetailsDataGridView.DataSource = this.bindedDistrictRateDetailsDataTable.DefaultView;
            ////added code to fix Bug#4192
            this.rateSource.DataSource = this.bindedDistrictRateDetailsDataTable.DefaultView;
            int itemFound = this.rateSource.Find("SARateItemID", this.selectedRateItem);

            // add a default empty row for RateDetailsDataGrid
            this.bindedDistrictRateDetailsDataTable.AddGetDistrictRateDetailsRow(this.bindedDistrictRateDetailsDataTable.NewGetDistrictRateDetailsRow());

            // add necessary empty rows
            int row;
            for (row = this.bindedDistrictRateDetailsDataTable.Rows.Count; row < this.RateDetailsDataGridView.NumRowsVisible; row++)
            {
                this.bindedDistrictRateDetailsDataTable.AddGetDistrictRateDetailsRow(this.bindedDistrictRateDetailsDataTable.NewGetDistrictRateDetailsRow());
            }

            // set the actual row count for rate details grid
            if (this.form1030DistrictDefinitionData.GetDistrictRateDetails.Rows.Count < this.RateDetailsDataGridView.NumRowsVisible)
            {
                this.rateGridActualRowCount = this.RateDetailsDataGridView.NumRowsVisible - this.form1030DistrictDefinitionData.GetDistrictRateDetails.Rows.Count + 1;
                this.RateDetailsScrollBar.Visible = true;
            }
            else
            {
                this.rateGridActualRowCount = this.form1030DistrictDefinitionData.GetDistrictRateDetails.Rows.Count + 1;
                this.RateDetailsScrollBar.Visible = false;
            }
        }

        /// <summary>
        /// Populates the distribution details grid view.
        /// </summary>
        private void PopulateDistributionDetailsGridView()
        {
            // clear the table for repopulating the data
            this.bindedDistrictDistributionDetailsDataTable.Clear();

            if (this.DistributionGridView.Columns.Count > 0)
            {
                //// create form level datatable for ratelist information
                //// this.bindedDistrictDistributionDetailsDataTable = (F1030SpecialDistrictDefinitionData.GetDistrictDistributionDetailsDataTable)this.f1030DistrictDefinitionData.GetDistrictDistributionDetails.Copy();
                F1030SpecialDistrictDefinitionData.GetDistrictDistributionDetailsRow[] tempTable;
                if (this.flagDistributionSortDirection)
                {
                    tempTable = (F1030SpecialDistrictDefinitionData.GetDistrictDistributionDetailsRow[])this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.Copy().Select("1=1", this.DistributionGridView.Columns[this.distributionGridViewSelectColumnNo].DataPropertyName + SharedFunctions.GetResourceString("DESC"));
                    this.DistributionGridView.Columns[this.distributionGridViewSelectColumnNo].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                }
                else
                {
                    tempTable = (F1030SpecialDistrictDefinitionData.GetDistrictDistributionDetailsRow[])this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.Copy().Select("1=1", this.DistributionGridView.Columns[this.distributionGridViewSelectColumnNo].DataPropertyName + SharedFunctions.GetResourceString("ASC"));
                    this.DistributionGridView.Columns[this.distributionGridViewSelectColumnNo].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                }

                //// add the rows after sorting
                foreach (F1030SpecialDistrictDefinitionData.GetDistrictDistributionDetailsRow myrow in tempTable)
                {
                    this.bindedDistrictDistributionDetailsDataTable.ImportRow(myrow);
                }
            }

            //// bind the table values to the grid
            ////added code to fix Bug#4192
            this.DistributionGridView.DataSource = this.bindedDistrictDistributionDetailsDataTable.DefaultView;
            this.distributionSource.DataSource = this.bindedDistrictDistributionDetailsDataTable.DefaultView;
            int itemFoundDist = this.distributionSource.Find("SARateItemID", this.selectedDistributionItem);

            //// add a default empty row for DistributionDetailsDataGrid
            this.bindedDistrictDistributionDetailsDataTable.AddGetDistrictDistributionDetailsRow(this.bindedDistrictDistributionDetailsDataTable.NewGetDistrictDistributionDetailsRow());

            //// add necessary empty rows
            for (int row = this.bindedDistrictDistributionDetailsDataTable.Rows.Count; row < this.DistributionGridView.NumRowsVisible; row++)
            {
                this.bindedDistrictDistributionDetailsDataTable.AddGetDistrictDistributionDetailsRow(this.bindedDistrictDistributionDetailsDataTable.NewGetDistrictDistributionDetailsRow());
            }

            // set the actual row count for distribution details grid
            if (this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.Rows.Count < this.RateDetailsDataGridView.NumRowsVisible)
            {
                this.distributionGridActualRowCount = this.DistributionGridView.NumRowsVisible - this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.Rows.Count + 1;
                this.DistributionDetailsVscrollBar.Visible = true;
            }
            else
            {
                this.distributionGridActualRowCount = this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.Rows.Count + 1;
                this.DistributionDetailsVscrollBar.Visible = false;
            }

            // Set the cell color to lightgreen when account status equals 1
            foreach (DataGridViewRow currentRow in this.DistributionGridView.Rows)
            {
                if (currentRow.Cells[SharedFunctions.GetResourceString("AccountStatus")].Value.ToString() == "1")
                {
                    currentRow.Cells[SharedFunctions.GetResourceString("Account")].Style.BackColor = Color.FromArgb(204, 255, 204);
                }
            }
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void LockControls(bool enable)
        {
            this.DistrictInfoA1TextBox.LockKeyPress = enable;
            this.DistrictInfoA2TextBox.LockKeyPress = enable;
            this.DistrictInfoA3TextBox.LockKeyPress = enable;
            this.DistrictInfoA4TextBox.LockKeyPress = enable;
            this.DistrictInfoA5TextBox.LockKeyPress = enable;
            this.DistrictInfoA6TextBox.LockKeyPress = enable;
            this.DistrictInfoB1TextBox.LockKeyPress = enable;
            this.DistrictInfoB2TextBox.LockKeyPress = enable;
            this.DistrictInfoB3TextBox.LockKeyPress = enable;
            this.DistrictInfoB4TextBox.LockKeyPress = enable;
            this.DistrictInfoB5TextBox.LockKeyPress = enable;
            this.DistrictInfoB6TextBox.LockKeyPress = enable;
            this.DistrictNumberTextBox.LockKeyPress = enable;
            this.RollYearTextBox.LockKeyPress = enable;
            this.MinimumDistrictFeeTextBox.LockKeyPress = enable;
            this.DistrictNameTextBox.LockKeyPress = enable;
            this.TypeComboBox.Enabled = !enable;
            this.RateDetailsDataGridView.ReadOnly = enable;
            this.DistributionGridView.ReadOnly = enable;
        }

        /// <summary>
        /// Disables the on empty record.
        /// </summary>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        private void DisableOnEmptyRecord(bool enabled)
        {
            this.DistrictDefinitionlPanel.Enabled = enabled;
            this.RateItemPanel.Enabled = enabled;
            this.DistributionPanel.Enabled = enabled;
        }

        /// <summary>
        /// Sets the length of the max.
        /// </summary>
        private void SetMaxLength()
        {
            this.DistrictNumberTextBox.MaxLength = 8;
            this.RollYearTextBox.MaxLength = 4;
            this.DistrictNameTextBox.MaxLength = 50;
        }

        /// <summary>
        /// Validates the fields(returns true on validation success.
        /// </summary>
        /// <returns>validation result</returns>
        private bool ValidateFields()
        {
            bool validationResult = true;
            if (((string.IsNullOrEmpty(this.DistrictNumberTextBox.Text.Trim())) || (string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim())) || (string.IsNullOrEmpty(this.DistrictNameTextBox.Text.Trim()))) || (!this.ValidateRateItems()) || (!this.ValidateDistributionItems()))
            {
                validationResult = false;
                return validationResult;
            }

            return validationResult;
        }

        /// <summary>
        /// Validates the rate items passes true on validation success.
        /// </summary>
        /// <returns>validation result</returns>
        private bool ValidateRateItems()
        {
            bool validationResult = true;

            bool validTypeId1 = true;

            // bool validTypeId2 = true;

            for (int currentRow = 0; currentRow < this.RateDetailsDataGridView.Rows.Count; currentRow++)
            {
                if (validationResult)
                {
                    if (!string.IsNullOrEmpty(this.RateDetailsDataGridView[3, currentRow].Value.ToString().Trim()) || !string.IsNullOrEmpty(this.RateDetailsDataGridView[1, currentRow].Value.ToString().Trim()) || !string.IsNullOrEmpty(this.RateDetailsDataGridView[0, currentRow].Value.ToString().Trim()) || !string.IsNullOrEmpty(this.RateDetailsDataGridView[2, currentRow].Value.ToString().Trim()))
                    {
                        if (string.IsNullOrEmpty(this.RateDetailsDataGridView[3, currentRow].Value.ToString().Trim()) || string.IsNullOrEmpty(this.RateDetailsDataGridView[1, currentRow].Value.ToString().Trim()) || string.IsNullOrEmpty(this.RateDetailsDataGridView[0, currentRow].Value.ToString().Trim()))
                        {
                            validationResult = false;
                        }

                        if (this.RateDetailsDataGridView[3, currentRow].Value.ToString().Trim() == "0.0" || this.RateDetailsDataGridView[3, currentRow].Value.ToString().Trim() == "0")
                        {
                            validationResult = false;
                        }

                        ////added code to fix Bug#5195
                        if (this.RateDetailsDataGridView[1, currentRow].Value.ToString() == "3" && string.IsNullOrEmpty(this.RateDetailsDataGridView[2, currentRow].Value.ToString().Trim()))
                        {
                            validationResult = false;
                        }

                        if (validTypeId1)
                        {
                            if (this.RateDetailsDataGridView[1, currentRow].Value.ToString() == "2" || this.RateDetailsDataGridView[1, currentRow].Value.ToString() == "3")
                            {
                                validTypeId1 = false;
                            }
                        }

                        ////if (validTypeId2)
                        ////{
                        ////    if (this.RateDetailsDataGridView[1, currentRow].Value.ToString() == "3")
                        ////    {
                        ////        validTypeId2 = false;
                        ////    }
                        ////}
                    }
                }
            }

            validTypeId1 = !validTypeId1;
            //// validTypeId2 = !validTypeId2;
            ////return (validationResult && validTypeId1 && validTypeId2);
            return (validationResult && validTypeId1);
        }

        /// <summary>
        /// Validates the distribution items passes true on validation success.
        /// </summary>
        /// <returns>validation result</returns>
        private bool ValidateDistributionItems()
        {
            bool validationResult = true;

            bool validTypeId1 = true;

            bool validTypeId2 = true;

            for (int currentRow = 0; currentRow < this.DistributionGridView.Rows.Count; currentRow++)
            {
                if (validationResult)
                {
                    if (!string.IsNullOrEmpty(this.DistributionGridView[2, currentRow].Value.ToString()) || !string.IsNullOrEmpty(this.DistributionGridView[1, currentRow].Value.ToString()) || !string.IsNullOrEmpty(this.DistributionGridView[0, currentRow].Value.ToString()))
                    {
                        if (string.IsNullOrEmpty(this.DistributionGridView[2, currentRow].Value.ToString()) || string.IsNullOrEmpty(this.DistributionGridView[2, currentRow].Value.ToString()) || string.IsNullOrEmpty(this.DistributionGridView[0, currentRow].Value.ToString()))
                        {
                            validationResult = false;
                        }

                        if (this.DistributionGridView[1, currentRow].Value.ToString().Trim() == "0")
                        {
                            validationResult = false;
                        }

                        if (validTypeId1)
                        {
                            if (this.DistributionGridView[0, currentRow].Value.ToString() == "1")
                            {
                                validTypeId1 = false;
                            }
                        }

                        if (validTypeId2)
                        {
                            if (this.DistributionGridView[0, currentRow].Value.ToString() == "4")
                            {
                                validTypeId2 = false;
                            }
                        }
                    }
                }
            }

            validTypeId1 = !validTypeId1;
            validTypeId2 = !validTypeId2;
            return (validationResult && validTypeId1 && validTypeId2);
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>SliceValidationFields</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            this.flagRevokeChangesOnDuplicate = false;
            bool result = true;
            int distictId;
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;
            if (this.ValidateFields())
            {
                if (string.IsNullOrEmpty(this.MinimumDistrictFeeTextBox.Text.Replace("$", "")))
                {
                    this.MinimumDistrictFeeTextBox.Text = SharedFunctions.GetResourceString("Zero");
                }

                if ((Convert.ToDouble(this.MinimumDistrictFeeTextBox.Text.Replace("$", "")) > Convert.ToDouble("214748.36")) || (Convert.ToDouble(this.DistrictNumberTextBox.Text) > Convert.ToDouble("2147483647")) || (Convert.ToInt16(this.RollYearTextBox.Text) == 0))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("DistrictSaveMessage"), SharedFunctions.GetResourceString("DistrictHeaderMessage"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.MinimumDistrictFeeTextBox.Focus();
                    sliceValidationFields.DisableNewMethod = true;
                }
                else
                {
                    F1030SpecialDistrictDefinitionData.GetDistrictDefinitionDetailsDataTable districtDefinitionDetailsDataTable = new F1030SpecialDistrictDefinitionData.GetDistrictDefinitionDetailsDataTable();
                    F1030SpecialDistrictDefinitionData.GetDistrictDefinitionDetailsRow saveDistrictDefinitionDetailsRow;
                    saveDistrictDefinitionDetailsRow = districtDefinitionDetailsDataTable.NewGetDistrictDefinitionDetailsRow();
                    string districtItem = string.Empty, rateItem = string.Empty, distributionItem = string.Empty, primaryKeyID = string.Empty;
                    int districtNo = 0;

                    saveDistrictDefinitionDetailsRow.District = Convert.ToInt32(this.DistrictNumberTextBox.Text);

                    if (string.IsNullOrEmpty(this.MinimumDistrictFeeTextBox.Text))
                    {
                        saveDistrictDefinitionDetailsRow.MinimumDistrictFee = 0;
                    }
                    else
                    {
                        saveDistrictDefinitionDetailsRow.MinimumDistrictFee = Convert.ToDecimal(this.MinimumDistrictFeeTextBox.Text.Replace('$', ' ').Trim());
                    }

                    saveDistrictDefinitionDetailsRow.PostTypeID = Convert.ToInt16(this.TypeComboBox.SelectedValue);
                    saveDistrictDefinitionDetailsRow.RollYear = Convert.ToInt16(this.RollYearTextBox.Text);
                    if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                    {
                        saveDistrictDefinitionDetailsRow.SADistrictID = Convert.ToInt32(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.SADistrictIDColumn]);
                    }
                    else
                    {
                        saveDistrictDefinitionDetailsRow.SADistrictID = 0;
                    }

                    saveDistrictDefinitionDetailsRow.SAName = this.DistrictNameTextBox.Text;

                    ///Used to Save  the District Info Fields
                    saveDistrictDefinitionDetailsRow.DistrictInfoA1 = this.DistrictInfoA1TextBox.Text.Trim();
                    saveDistrictDefinitionDetailsRow.DistrictInfoA2 = this.DistrictInfoA2TextBox.Text.Trim();
                    saveDistrictDefinitionDetailsRow.DistrictInfoA3 = this.DistrictInfoA3TextBox.Text.Trim();
                    saveDistrictDefinitionDetailsRow.DistrictInfoA4 = this.DistrictInfoA4TextBox.Text.Trim();
                    saveDistrictDefinitionDetailsRow.DistrictInfoA5 = this.DistrictInfoA5TextBox.Text.Trim();
                    saveDistrictDefinitionDetailsRow.DistrictInfoA6 = this.DistrictInfoA6TextBox.Text.Trim();
                    saveDistrictDefinitionDetailsRow.DistrictInfoB1 = this.DistrictInfoB1TextBox.Text.Trim();
                    saveDistrictDefinitionDetailsRow.DistrictInfoB2 = this.DistrictInfoB2TextBox.Text.Trim();
                    saveDistrictDefinitionDetailsRow.DistrictInfoB3 = this.DistrictInfoB3TextBox.Text.Trim();
                    saveDistrictDefinitionDetailsRow.DistrictInfoB4 = this.DistrictInfoB4TextBox.Text.Trim();
                    saveDistrictDefinitionDetailsRow.DistrictInfoB5 = this.DistrictInfoB5TextBox.Text.Trim();
                    saveDistrictDefinitionDetailsRow.DistrictInfoB6 = this.DistrictInfoB6TextBox.Text.Trim();
                    if (this.AllowHalfpaymentComboBox.SelectedItem!=null)
                    {
                        if (this.AllowHalfpaymentComboBox.SelectedItem.ToString().ToLower().Equals("yes"))
                        {
                            saveDistrictDefinitionDetailsRow.IsHalfPaymentAllowed = true;
                        }
                        else
                        {
                            saveDistrictDefinitionDetailsRow.IsHalfPaymentAllowed = false;
                        }
                    }
                    else
                    {
                        saveDistrictDefinitionDetailsRow.IsHalfPaymentAllowed = true;
                    }
                    districtDefinitionDetailsDataTable.Rows.Add(saveDistrictDefinitionDetailsRow);

                    if (this.flagCopyDistrict)
                    {
                        districtNo = 0;
                    }
                    else
                    {
                        districtNo = Convert.ToInt32(this.DistrictNumberTextBox.Text);
                    }

                    //// show validation alert if zero item present for 
                    if ((this.bindedDistrictDistributionDetailsDataTable.Rows.Count <= 0) || (this.bindedDistrictRateDetailsDataTable.Rows.Count <= 0))
                    {
                        MessageBox.Show(@primaryKeyID, SharedFunctions.GetResourceString("ExciseTaxAffDvtMissingHeader"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.DistrictNumberTextBox.Focus();
                        sliceValidationFields.DisableNewMethod = true;
                    }

                    if (this.DistrictNumberTextBox.Text.Trim() == SharedFunctions.GetResourceString("Zero"))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("DistrictNumber"), SharedFunctions.GetResourceString("ExciseTaxAffDvtMissingHeader"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.DistrictNumberTextBox.Focus();
                        sliceValidationFields.DisableNewMethod = true;
                        return sliceValidationFields;
                    }

                    districtItem = Utility.GetXmlString(districtDefinitionDetailsDataTable);
                    rateItem = Utility.GetXmlString(this.bindedDistrictRateDetailsDataTable);
                    distributionItem = Utility.GetXmlString(this.bindedDistrictDistributionDetailsDataTable);
                    distributionItem = distributionItem.Replace("%", "");
                    primaryKeyID = this.form1030control.WorkItem.F16030_SaveDistrictDefinition(districtNo, districtItem, rateItem, distributionItem, false, true, TerraScanCommon.UserId);
                    result = int.TryParse(primaryKeyID, out distictId);
                    if (result && distictId.Equals(0))
                    {
                        sliceValidationFields.DisableNewMethod = false;
                        sliceValidationFields.ErrorMessage = string.Empty;
                        sliceValidationFields.RequiredFieldMissing = false;
                    }
                    else
                    {
                        if (primaryKeyID.Contains(SharedFunctions.GetResourceString("ExistingDistrict")))
                        {
                            DialogResult currentResult = MessageBox.Show(primaryKeyID + SharedFunctions.GetResourceString("SaveDistrictMessage"), SharedFunctions.GetResourceString("DuplicateDistrictHeader"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (DialogResult.Yes == currentResult)
                            {
                                this.flagOverwrite = true;
                                sliceValidationFields.DisableNewMethod = false;
                                sliceValidationFields.ErrorMessage = string.Empty;
                                sliceValidationFields.RequiredFieldMissing = false;
                                //// this.DistrictCopyButton.Enabled = false;
                            }
                            else if (DialogResult.No == currentResult)
                            {
                                this.flagRevokeChangesOnDuplicate = true;
                                sliceValidationFields.DisableNewMethod = false;
                                sliceValidationFields.ErrorMessage = string.Empty;
                                sliceValidationFields.RequiredFieldMissing = false;
                                return sliceValidationFields;
                            }
                            else
                            {
                                sliceValidationFields.DisableNewMethod = true;
                                this.DistrictNumberTextBox.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show(primaryKeyID, SharedFunctions.GetResourceString("DistrictMessageHeader"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.DistrictNumberTextBox.Focus();
                            sliceValidationFields.DisableNewMethod = true;
                            sliceValidationFields.ErrorMessage = string.Empty;
                            sliceValidationFields.RequiredFieldMissing = false;
                        }
                    }
                }
            }
            else
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("ExciseTaxAffDvtMissing");
                sliceValidationFields.RequiredFieldMissing = true;
                this.DistrictNumberTextBox.Focus();
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Sets the default roll year.
        /// </summary>
        private void SetDefaultRollYear()
        {
            this.getRollYearConfigurationValue.GetCommentsConfigDetails.Clear();
            this.getRollYearConfigurationValue = this.form1030control.WorkItem.GetConfigDetails(SharedFunctions.GetResourceString("DefaultRollYear"));
            if (this.getRollYearConfigurationValue.GetCommentsConfigDetails.Rows.Count > 0)
            {
                if (int.TryParse(this.getRollYearConfigurationValue.GetCommentsConfigDetails[0][this.getRollYearConfigurationValue.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString(), out this.defaultRollYear))
                {
                    this.RollYearTextBox.Text = this.defaultRollYear.ToString();
                }
            }
        }

        /// <summary>
        /// Disables the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableControls(bool enable)
        {
            this.DistrictInfoA1.Enabled = enable;
            this.DistrictInfoA2.Enabled = enable;
            this.DistrictInfoA3.Enabled = enable;
            this.DistrictInfoA4.Enabled = enable;
            this.DistrictInfoA5.Enabled = enable;
            this.DistrictInfoA6.Enabled = enable;
            this.DistrictInfoB1.Enabled = enable;
            this.DistrictInfoB2.Enabled = enable;
            this.DistrictInfoB3.Enabled = enable;
            this.DistrictInfoB4.Enabled = enable;
            this.DistrictInfoB5.Enabled = enable;
            this.DistrictInfoB6.Enabled = enable;
            this.ClearedDatePanel.Enabled = enable;
            this.TypePanel.Enabled = enable;
            //this.TypeComboBox.Enabled = enable;
            this.FromAccountPanel.Enabled = enable;
            this.UserPanel.Enabled = enable;
            this.AgencyPanel.Enabled = enable;
            this.DistributionPanel.Enabled = enable;
            this.RateItemPanel.Enabled = enable;

            //this.DistrictCopyButton.Enabled = enable;
        }

        /// <summary>
        /// Gets the form master new permission.
        /// </summary>
        /// <returns>bool</returns>
        private bool GetFormMasterNewPermission()
        {
            if ((this.Parent != null) && (this.Parent.Parent != null) && (this.Parent.Parent.Parent != null))
            {
                if (this.Parent.Parent.Parent is BaseSmartPart)
                {
                    return ((BaseSmartPart)this.Parent.Parent.Parent).PermissionFiled.newPermission;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Removes the default selection.
        /// </summary>
        private void RemoveDefaultSelection()
        {
            if (this.RateDetailsDataGridView.OriginalRowCount == 0)
            {
                this.RateDetailsDataGridView.RemoveDefaultSelection = true;
            }
            else
            {
                this.RateDetailsDataGridView.RemoveDefaultSelection = false;
            }

            if (this.DistributionGridView.OriginalRowCount == 0)
            {
                this.DistributionGridView.RemoveDefaultSelection = true;
            }
            else
            {
                this.DistributionGridView.RemoveDefaultSelection = false;
            }
        }
        #endregion Private Member Methods

        /// <summary>
        /// TypeComboBox_SelectedIndexChanged
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    this.EnableEditMode();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// DistrictNumberTextBox_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void DistrictNumberTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                FromAccountPanel.BackColor = Color.White;
                this.DistrictNumberTextBox.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Initialise the rowIndex 
            int rowIndex = -1;

            // Checks the rowindex is valid
            if (e.RowIndex >= 0 && this.RateDetailsDataGridView.CurrentCell != null)
            {
                // Set the current RowIndex
                rowIndex = e.RowIndex;
            }
            else if (this.RateDetailsDataGridView.CurrentRowIndex >= 0 && this.RateDetailsDataGridView.CurrentCell != null)
            {
                // Set the Current rowindex
                rowIndex = RateDetailsDataGridView.CurrentRowIndex;
            }

            if (rowIndex >= 0)
            {

                if (this.bindedDistrictRateDetailsDataTable.Rows[rowIndex][this.bindedDistrictRateDetailsDataTable.SARateItemIDColumn] != null)
                {
                    if (!string.IsNullOrEmpty(this.bindedDistrictRateDetailsDataTable.Rows[rowIndex][this.bindedDistrictRateDetailsDataTable.SARateItemIDColumn].ToString()))
                    {
                        this.selectedRateItem = Convert.ToInt32(this.bindedDistrictRateDetailsDataTable.Rows[rowIndex][this.bindedDistrictRateDetailsDataTable.SARateItemIDColumn]);
                    }
                    else
                    {
                        this.selectedRateItem = 0;
                    }
                }
            }
        }

        private void AllowHalfpaymentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    this.EnableEditMode();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

    }
}
