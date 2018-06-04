//--------------------------------------------------------------------------------------------
// <copyright file="F16040.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Improvement District Definition.
// </summary>
//----------------------------------------------------------------------------------------------
// History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//20170614          Dhineshkumar        
//*********************************************************************************/

namespace D10040
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Collections.Specialized;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.SmartParts;
    using TerraScan.Utilities;
    using TerraScan.UI.Controls;
    
    [SmartPart]
    public partial class F16040 : BaseSmartPart
    {
        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyID;

        /// <summary>
        /// flag to identify form master new permission
        /// </summary>
        private bool flagMasterNew;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        /// <summary>
        /// string primary values.
        /// </summary>
        private string primaryKeyID;

        /// <summary>
        /// flag insert/update.
        /// </summary>
        private bool valInsertUpdate;

        /// <summary>
        ///  Local variable for Red
        /// </summary>
        private int redColor;

        /// <summary>
        /// save result.
        /// </summary>
        public bool saveResult;

        /// <summary>
        /// selected int
        /// </summary>
        private short selectedTypeid;

        /// <summary>
        ///  Local variable for Green.
        /// </summary>
        private int greenColor;

        /// <summary>
        /// roll year.
        /// </summary>
        private int rollYear;

        /// <summary>
        /// flag to identify overwrite or not
        /// </summary>
        private bool flagUpdate;

        /// <summary>
        ///  Local variable for Blue.
        /// </summary>
        private int blueColor;

        /// <summary>
        /// District ID
        /// </summary>
        private int districtID = 0;

        /// <summary>
        /// revert the changes as duplicate
        /// </summary>
        private bool flagRevokeChangesOnDuplicate;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// Used to store the IncomeSourceID(KeyID)
        /// </summary>
        private int? IncomeSourceID;

        /// <summary>
        /// user id.
        /// </summary>
        private int userid = TerraScanCommon.UserId;

        /// <summary>
        /// type id selected.
        /// </summary>
        private int selectedOnloadTypeid;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// flag to identify the form is being loading
        /// </summary>
        private bool flagFormLoad = true;

        /// <summary>
        /// texteditflag
        /// </summary>
        private bool texteditflag = false;

        /// <summary>
        /// Boolean Value
        /// </summary>        
        private bool receiptDateChanged;

        /// <summary>
        /// flag to show account form.
        /// </summary>
        private bool flagAccountShow;

        /// <summary>
        /// To hold active record
        /// </summary>
        private int activeRecord;

        /// <summary>
        /// Instance of F14060 Controller to call the WorkItem
        /// </summary>
        private F16040Controller form16040Control;

        /// <summary>
        /// flag to identify distribution item sort direction
        /// </summary>
        private bool flagDistributionSortDirection;

        /// <summary>
        /// To get the Roll year from General Configuration Value
        /// </summary>
        private CommentsData getRollYearConfigurationValue = new CommentsData();

        /// <summary>
        /// the column no selected at distribution grid view
        /// </summary>
        private int distributionGridViewSelectColumnNo;

        /// <summary>
        /// distribution list Rowcount.
        /// </summary>
        private int distributionListRowCount;

        /// <summary>
        /// the distribution item selcted for delete
        /// </summary>
        private int selectedDistributionItem;

        /// <summary>
        /// cellValue When Editing
        /// </summary>
        private string cellValueWhenEditing;

        /// <summary>
        /// concadinatedString
        /// </summary>
        private string concadinatedString;

        /// <summary>
        /// nonZero
        /// </summary>
        private string nonZero;

        /// <summary>
        /// Declaring the receiptDate
        /// </summary>
        private DateTime receiptDate;

        /// <summary>
        /// default roll year variable 
        /// </summary>
        private int defaultRollYear;

        private int configRollYear;

        /// <summary>
        /// Set Date Format
        /// </summary>
        private string dateFormat = "M/d/yyyy";

        /// <summary>
        /// percentage header flag
        /// </summary>
        private bool flagPercentageHeader;

        /// <summary>
        /// tempYear
        /// </summary>
        private int tempYear;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// flag to identify row header delete in distribution grid
        /// </summary>
        private bool flagDistributionItemDeleteEnabled;

        /// <summary>
        /// used to keep trakc of SelectedDistributionItem
        /// </summary>
        private BindingSource distributionSource = new BindingSource();

        /// <summary>
        /// distribution details table.s
        /// </summary>
        private F16040ImprovementDistrictDefinition.GetDistributionDetailsDataTable bindedDistributionDetailsTable;

        /// <summary>
        /// Improvement District Definition Details.
        /// </summary>
        private F16040ImprovementDistrictDefinition getImprovementDistrictDefinition = new F16040ImprovementDistrictDefinition();

        /// <summary>
        /// Get interest calc details.
        /// </summary>
        private F16040ImprovementDistrictDefinition.DelqInterestCalcTableDataTable getInterestCalcDetails = new F16040ImprovementDistrictDefinition.DelqInterestCalcTableDataTable();

        /// <summary>
        /// Get interest Method details.
        /// </summary>
        private F16040ImprovementDistrictDefinition.InterestMethodTableDataTable getInterestMethodDetails = new F16040ImprovementDistrictDefinition.InterestMethodTableDataTable();

        /// <summary>
        /// Get Distribution Details.
        /// </summary>
        private F16040ImprovementDistrictDefinition.GetDistributionDetailsDataTable getDistributionDetails = new F16040ImprovementDistrictDefinition.GetDistributionDetailsDataTable();

        private F16040ImprovementDistrictDefinition.DistributionItemTypeDataTable distributionTypeDatatable = new F16040ImprovementDistrictDefinition.DistributionItemTypeDataTable();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F25011"/> class.
        /// </summary>
        public F16040()
        {
           InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84721"/> class.
        /// </summary>
        /// <param name="masterForm">masterForm</param>
        /// <param name="formNo">formNO</param>
        /// <param name="keyID">KeyID</param>
        /// <param name="red">red</param>
        /// <param name="green">green</param>
        /// <param name="blue">blue</param>
        /// <param name="tabText">tabText</param>
        /// <param name="permissionEdit">permissionEdit</param>
        public F16040(int masterForm ,int formNo,int keyID,int red,int green,int blue,string tabText,bool permissionEdit)
        {
            InitializeComponent();
            this.bindedDistributionDetailsTable = new F16040ImprovementDistrictDefinition.GetDistributionDetailsDataTable();
            this.masterFormNo = masterForm;
            this.Tag = formNo;
            this.activeRecord = keyID;
            this.districtID = keyID;
            this.IncomeSourceID = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.ParcelGridButton.BackColor = Color.Purple;
        }

        /// <summary>
        /// Gets or sets the form1030control.
        /// </summary>
        /// <value>The form9030control.</value>
        [CreateNew]
        public F16040Controller Form10040control
        {
            get { return this.form16040Control; }
            set { this.form16040Control = value; }
        }

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

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;
        
        #endregion Event Publication

        #region Event Subscription

        /// <summary>
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            try
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
                }
            }
            catch (Exception expe)
            {
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
            try
            {
                this.flagUpdate = false;
                if (this.Controls.Count > 0)
                {
                    this.ClearImprovementDistrictDetails();
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
                    this.LoadDistributionComboExist();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    if (!this.flagUpdate)
                    {
                        this.DistributionPanel.Enabled = false;
                        this.EnableNewMode();
                    }
                }
            }
            catch (Exception expr)
            {
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
                    if (!this.flagRevokeChangesOnDuplicate)
                    {
                        if (saveResult)
                        {         
                            if (saveResult)
                            {
                                SliceReloadActiveRecord currentSliceInfo;
                                currentSliceInfo.MasterFormNo = this.masterFormNo;
                                currentSliceInfo.SelectedKeyId = this.activeRecord;
                                this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                            }
                            else
                            {
                                saveResult = false;
                            }
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                        }
                    }
                }
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

                this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);

                if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                {
                    this.EnableControls(true);
                }
                else
                {
                    this.EnableControls(false);
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
            this.flagFormLoad = false;
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
                this.activeRecord = eventArgs.Data.SelectedKeyId;
                
                this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                {
                    this.ClearImprovementDistrictDetails();
                    this.EnableControls(true);
                    this.FillDetails(this.activeRecord);
                }
                else
                {
                    this.FillDetails(this.activeRecord);
                    this.EnableControls(false);
                }
                this.LoadDistributionComboExist();
                this.flagFormLoad = false;
            }
        }

        private void LoadDistributionComboExist()
        {
            var masterLists = this.form16040Control.WorkItem.GetDistributionDetails();
            DataGridViewColumnCollection distributionDetailsColumns = this.DistributionGridView.Columns;
            this.DistributionGridView.AutoGenerateColumns = false;
            this.DistributionGridView.ApplyStandardBehaviour = true;
            distributionDetailsColumns[SharedFunctions.GetResourceString("DistributionType")].DataPropertyName = masterLists.GetDistributionDetails.ItemTypeIDColumn.ColumnName;
            (this.DistributionGridView.Columns[SharedFunctions.GetResourceString("DistributionType")] as DataGridViewComboBoxColumn).DataSource = masterLists.GetDistributionDetails;
            (this.DistributionGridView.Columns[SharedFunctions.GetResourceString("DistributionType")] as DataGridViewComboBoxColumn).DisplayMember = masterLists.GetDistributionDetails.DistributionTypeColumn.ColumnName;
            (this.DistributionGridView.Columns[SharedFunctions.GetResourceString("DistributionType")] as DataGridViewComboBoxColumn).ValueMember = masterLists.GetDistributionDetails.ItemTypeIDColumn.ColumnName;
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
                this.form16040Control.WorkItem.State["FormStatus"] = this.CheckPageStatus();
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


        private void F16040_Load(object sender, EventArgs e)
        {
            this.flagFormLoad = true;
            this.FlagSliceForm = true;
            this.ParcelGridButton.BackColor = Color.FromArgb(128, 0, 128);
            this.flagRevokeChangesOnDuplicate = false;
            this.EnableControls(true);
            this.DistrictHeaderPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DistrictHeaderPictureBox.Height, this.DistrictHeaderPictureBox.Width, SharedFunctions.GetResourceString("District"), 28, 81, 128);
            this.DistributionItemPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DistributionItemPictureBox.Height, this.DistributionItemPictureBox.Width, SharedFunctions.GetResourceString("Distribution"), 174, 150, 94);
            this.SummaryDetailsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SummaryDetailsPictureBox.Height, this.SummaryDetailsPictureBox.Width, SharedFunctions.GetResourceString("Summary"), 0, 64, 0);
            this.LoadGraceFlagComboBox();
            this.LoadInterestMethodCombo();
            this.LoadDelqInterCalqCombo();
            this.LoadDistributionDetails();
            this.SetDefaultRollYear();
            this.FillDetails(this.activeRecord);
            if (this.activeRecord != -99)
            {
                this.LoadDistributionComboExist();
            }
            this.RemoveDefaultSelection();
            if (this.activeRecord > 0)
            {
                this.ParcelGridButton.Enabled = true;
            }
            else { this.ParcelGridButton.Enabled = false; }

            this.flagFormLoad = false;
            this.IDNumberTextBox.Focus();
        }

        /// <summary>
        /// Load Grace Flag combobox.
        /// </summary>
        private void LoadGraceFlagComboBox()
        {
           this.GraceFlagComboBox.Items.Clear();
           this.GraceFlagComboBox.Items.Insert(0, SharedFunctions.GetResourceString("YESValue"));
           this.GraceFlagComboBox.Items.Insert(1, SharedFunctions.GetResourceString("NOValue"));
        }

        /// <summary>
        /// Load Interest Method combobox value.
        /// </summary>
        private void LoadInterestMethodCombo()
        {
            this.getImprovementDistrictDefinition = this.form16040Control.WorkItem.ListInterestMethod();
            this.InterestMethodComboBox.DisplayMember = this.getImprovementDistrictDefinition.InterestMethodTable.InterestMethodColumn.ColumnName;
            this.InterestMethodComboBox.ValueMember = this.getImprovementDistrictDefinition.InterestMethodTable.InterestMethodIDColumn.ColumnName;
            this.InterestMethodComboBox.DataSource = this.getImprovementDistrictDefinition.InterestMethodTable;
            this.InterestMethodComboBox.SelectedIndex = -1;
        }

        /// <summary>
        /// Load Interest Calq Combobox Value.
        /// </summary>
        private void LoadDelqInterCalqCombo()
        {
            this.getImprovementDistrictDefinition = this.form16040Control.WorkItem.ListInterestDelqDetails();
            this.DelqInterestCalqComboBox.DisplayMember = this.getImprovementDistrictDefinition.DelqInterestCalcTable.DelqInterestCalcColumn.ColumnName;
            this.DelqInterestCalqComboBox.ValueMember = this.getImprovementDistrictDefinition.DelqInterestCalcTable.DelqInterestCalcIDColumn.ColumnName;
            this.DelqInterestCalqComboBox.DataSource = this.getImprovementDistrictDefinition.DelqInterestCalcTable;
            this.DelqInterestCalqComboBox.SelectedIndex = -1;                
        }

        /// <summary>
        /// Get Distribution Type Details.
        /// </summary>
        private void LoadDistributionDetails()
        {
            this.getImprovementDistrictDefinition = this.form16040Control.WorkItem.GetDistributionDetails();
            this.SetGridColumns();
            this.PopulateDistributionDetailsGridView();
        }

        /// <summary>
        /// Fill details.
        /// </summary>
        /// <param name="keyId"></param>
        private void FillDetails(int keyId)
        {
            this.getImprovementDistrictDefinition.GetDistrictDetails.Clear();
            this.getImprovementDistrictDefinition.GetSummaryDetails.Clear();
            this.bindedDistributionDetailsTable.Clear();
            this.getImprovementDistrictDefinition = this.form16040Control.WorkItem.GetDistrictDefinitionDetails(keyId);
            if (this.getImprovementDistrictDefinition.GetDistrictDetails.Rows.Count > 0 || this.getImprovementDistrictDefinition.GetDistributionDetails.Rows.Count > 0)
            {
                if (districtID > 0)
                {
                    this.flagUpdate = true;
                }
                this.PopulateDistrictDetails();
                this.SetGridColumns();
                this.PopulateDistributionDetailsGridView();
                this.PopulateSummaryDetails();   
            }
            else
            {
                this.EnableControls(false);
                this.LockControls(true);
            }
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
                    return this.SaveImprovementDistrictDefinition();
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
        /// Save Improvement District Defintion.
        /// </summary>
        private bool SaveImprovementDistrictDefinition()
        {
            try
            {       
                string districtItem = string.Empty;
                string distributionItem;
                short idTypeId = 0;
                int graceperiod = 0;
                int districtNo = 0;
                int payments=0;
                int selectedinterestMethodId = 0;
                int selectedinterestCalcMethodId = 0;
                bool selectedFlag=false;
                decimal penaltyRate = 0;
                decimal decimalcommissionRate = 0;
                decimal decimalcomPenRate = 0;
                decimal decimalinterestRate = 0;
                decimal decimaldelqInterestRate = 0;

                if (this.InterestMethodComboBox.SelectedValue != null)
                {
                    int.TryParse(this.InterestMethodComboBox.SelectedValue.ToString(), out selectedinterestMethodId);
                }

                if (this.DelqInterestCalqComboBox.SelectedValue != null)
                {
                    int.TryParse(this.DelqInterestCalqComboBox.SelectedValue.ToString(), out selectedinterestCalcMethodId);
                }

                if (this.GraceFlagComboBox.SelectedItem != null)
                {
                    if (GraceFlagComboBox.SelectedItem.ToString() == "YES")
                    {
                        selectedFlag = true;
                    }
                    else
                    {
                        selectedFlag = false;
                    }
                }

                F16040ImprovementDistrictDefinition getdistrictHeaderData = new F16040ImprovementDistrictDefinition();
                F16040ImprovementDistrictDefinition.DistrictHeaderTableRow savedistrictDefintionRow;
                savedistrictDefintionRow = getdistrictHeaderData.DistrictHeaderTable.NewDistrictHeaderTableRow();

                savedistrictDefintionRow.SAName = this.ImpDistrictNameTextBox.Text.Trim();

                if (this.IDNumberTextBox.Text != string.Empty)
                {
                    savedistrictDefintionRow.District = Convert.ToInt32(this.IDNumberTextBox.Text.Trim());
                }
                else
                {
                    savedistrictDefintionRow.District = districtNo;
                }
                if (this.RollYearTextBox.Text != string.Empty)
                {
                    savedistrictDefintionRow.RollYear = Convert.ToInt32(this.RollYearTextBox.Text.Trim());
                }
                else
                {
                    savedistrictDefintionRow.RollYear = this.defaultRollYear;
                }
                if (this.PaymentsNoTextBox.Text != string.Empty)
                {
                    savedistrictDefintionRow.NumPayments = Convert.ToInt16(this.PaymentsNoTextBox.Text.Trim());
                }
                else
                {
                    savedistrictDefintionRow.NumPayments = Convert.ToInt16(payments);
                }

                if (this.LevyDateTextBox.Text != string.Empty)
                {
                    savedistrictDefintionRow.LevyDate = LevyDateTextBox.Text.Trim();
                }
                if (this.FirstDueTextBox.Text != string.Empty)
                {
                    savedistrictDefintionRow.FirstDue = this.FirstDueTextBox.Text.Trim();
                }
                if (this.SecondDueTextBox.Text != string.Empty)
                {
                    savedistrictDefintionRow.SecondDue = this.SecondDueTextBox.Text.Trim();
                }
                if (this.GracePeriodTextBox.Text != string.Empty)
                {
                    savedistrictDefintionRow.GracePeriod = Convert.ToInt32(this.GracePeriodTextBox.Text.Trim());
                }
                else
                {
                    savedistrictDefintionRow.GracePeriod = graceperiod;
                }

                if (this.GraceFlagComboBox.Text != string.Empty)
                {
                    savedistrictDefintionRow.HasGrace = (bool)selectedFlag;
                }

                if (this.PenaltyTextBox.Text != string.Empty)
                {
                    double dmarketcollection = 0;
                    double.TryParse(this.PenaltyTextBox.Text.Replace("%", string.Empty).ToString(), out dmarketcollection);
                    dmarketcollection = dmarketcollection * 0.01;
                    penaltyRate = Convert.ToDecimal(dmarketcollection);
                    savedistrictDefintionRow.PenaltyRate = penaltyRate;
                }
                else
                {
                    savedistrictDefintionRow.PenaltyRate = penaltyRate;
                }

                if (this.CommissionTextBox.Text != string.Empty)
                {
                    double dmarketcollection = 0;
                    double.TryParse(this.CommissionTextBox.Text.Replace("%", string.Empty).ToString(), out dmarketcollection);
                    dmarketcollection = dmarketcollection * 0.01;
                    decimalcommissionRate = Convert.ToDecimal(dmarketcollection);
                    savedistrictDefintionRow.CommissionRate = decimalcommissionRate;
                }
                else
                {
                    savedistrictDefintionRow.CommissionRate = decimalcommissionRate;
                }

                if (this.CommissionOnPenTextBox.Text != string.Empty)
                {
                    double dmarketcollection = 0;
                    double.TryParse(this.CommissionOnPenTextBox.Text.Replace("%", string.Empty).ToString(), out dmarketcollection);
                    dmarketcollection = dmarketcollection * 0.01;
                    decimalcomPenRate = Convert.ToDecimal(dmarketcollection);
                    savedistrictDefintionRow.CommPenRate = decimalcomPenRate;
                }
                else
                {
                    savedistrictDefintionRow.CommPenRate = decimalcomPenRate;
                }
                if (this.InterestTextBox.Text != string.Empty)
                {
                    double dmarketcollection = 0;
                    double.TryParse(this.InterestTextBox.Text.Replace("%", string.Empty).ToString(), out dmarketcollection);
                    dmarketcollection = dmarketcollection * 0.01;
                    decimalinterestRate = Convert.ToDecimal(dmarketcollection);
                    savedistrictDefintionRow.InterestRate = decimalinterestRate;
                }
                else
                {
                    savedistrictDefintionRow.InterestRate = decimalinterestRate;
                }

                if (this.DelqInterestTextBox.Text != string.Empty)
                {
                    double dmarketcollection = 0;
                    double.TryParse(this.DelqInterestTextBox.Text.Replace("%", string.Empty).ToString(), out dmarketcollection);
                    dmarketcollection = dmarketcollection * 0.01;
                    decimaldelqInterestRate = Convert.ToDecimal(dmarketcollection);
                    savedistrictDefintionRow.DelqInterestRate = decimaldelqInterestRate;
                }
                else
                {
                    savedistrictDefintionRow.DelqInterestRate = decimaldelqInterestRate;
                }

                savedistrictDefintionRow.InterestMethodID = selectedinterestMethodId.ToString();
                savedistrictDefintionRow.DelqInterestCalcID = selectedinterestCalcMethodId.ToString();
                
                if (this.selectedTypeid == 0)
                {
                    selectedTypeid = Convert.ToInt16(selectedOnloadTypeid);
                }

                if (this.IdTypeTextBox.Text != string.Empty)
                {
                    savedistrictDefintionRow.IDTypeID = this.selectedTypeid;
                }
                else
                {
                    savedistrictDefintionRow.IDTypeID = idTypeId;
                }

                if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                {
                    savedistrictDefintionRow.SADistrictID = Convert.ToInt16(getImprovementDistrictDefinition.GetDistrictDetails[0]["SADistrictID"].ToString()); 
                }
                else
                {
                    savedistrictDefintionRow.SADistrictID = 0;
                }

                getdistrictHeaderData.DistrictHeaderTable.Rows.Clear();
                getdistrictHeaderData.DistrictHeaderTable.Rows.Add(savedistrictDefintionRow);

                DataSet tempDataSet = new DataSet("Root");
                tempDataSet.Tables.Add(getdistrictHeaderData.DistrictHeaderTable.Copy());
                tempDataSet.Tables[0].TableName = "Table";


                if (this.IDNumberTextBox.Text != string.Empty)
                {
                    districtNo = Convert.ToInt32(this.IDNumberTextBox.Text);
                }
                else
                {
                    districtNo = 0;
                }

                districtItem = tempDataSet.GetXml();

                DataSet gridDataSet = new DataSet("Root");
                gridDataSet.Tables.Add(bindedDistributionDetailsTable.Copy());
                gridDataSet.Tables[0].TableName = "Table";
                DataTable dtNew = gridDataSet.Tables[0].Copy();
                distributionItem = gridDataSet.GetXml();


                if (!this.flagUpdate)
                {
                    primaryKeyID = this.form16040Control.WorkItem.F16040_SaveImproveDistrictDefinition(districtItem, distributionItem, TerraScanCommon.UserId);
                }
                else
                {
                    primaryKeyID = this.form16040Control.WorkItem.F16040_UpdateImproveDistrictDefinition(districtNo, districtItem, distributionItem, TerraScanCommon.UserId);
                }


                int bvaltrue = 0;
                saveResult = int.TryParse(primaryKeyID, out bvaltrue);

                int distictId = 0;
                int.TryParse(primaryKeyID, out distictId);
                if (!distictId.Equals(0))
                {
                    this.activeRecord = Convert.ToInt32(primaryKeyID);
                }
            }
            catch (Exception ex)
            {
            }
            return saveResult;
        }

        /// <summary>
        /// News the button_ click.
        /// </summary>
        private void NewButtonClick()
        {
            try
            {
                this.flagUpdate = false;
                this.ClearImprovementDistrictDetails();
                this.EnableNewMode();
                this.DisableOnEmptyRecord(true);
                this.RollDistrictButton.Enabled = false;
                this.IDNumberTextBox.Focus();
                this.ParcelGridButton.Enabled = false;
                this.pageMode = TerraScanCommon.PageModeTypes.New;
            }
            catch (Exception exp)
            {
            }
        }

        public static string ConvertPercentageFormat(string strval)
        {
            string retval = "";
            decimal outDecimalContractPercentage;
            if (!String.IsNullOrEmpty(strval))
            {
                if (Decimal.TryParse(strval, out outDecimalContractPercentage))
                {
                    outDecimalContractPercentage = outDecimalContractPercentage * 100;
                    retval = outDecimalContractPercentage.ToString("###0.00") + "%";
                }
                else
                {
                    retval = "";
                }
            }
            else
            {
                retval = "";
            }
            return retval;
        }


        /// <summary>
        /// Enables the edit mode.
        /// </summary>
        private void EnableEditMode()
        {
            if (this.PermissionEdit && (this.pageMode == TerraScanCommon.PageModeTypes.View))
            {
                this.flagUpdate = true;
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.RollDistrictButton.Enabled = false;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        /// <summary>
        /// Disables the on empty record.
        /// </summary>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        private void DisableOnEmptyRecord(bool enabled)
        {
            this.DistrictHeaderPanel.Enabled = enabled;
            this.DistributionPanel.Enabled = enabled;
            this.SummaryFieldsPanel.Enabled = enabled;
        }

        /// <summary>
        /// Get District Definition Details.
        /// </summary>
        private void LoadDistrictDefinitionDetails(int districtkey)
        {
            getImprovementDistrictDefinition = this.form16040Control.WorkItem.GetDistrictDefinitionDetails(districtkey);
            if (districtID > 0)
            {
                this.flagUpdate = true;
            }
            this.PopulateSummaryDetails();
            this.PopulateDistributionDetailsGridView();
            this.PopulateDistrictDetails();
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>SliceValidationFields</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            try
            {
                bool validateresult = true;
                int distictId;
                bool reqFieldValidateflag = true;
                int validRowsCount = 0;
                this.flagRevokeChangesOnDuplicate = false;
                sliceValidationFields.FormNo = formNo;
                
                if (bindedDistributionDetailsTable.Rows.Count > 0)
                {
                    for (int i = 0; i < bindedDistributionDetailsTable.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(this.bindedDistributionDetailsTable.Rows[i]["Percentage"].ToString()) || !string.IsNullOrEmpty(this.bindedDistributionDetailsTable.Rows[i]["ItemTypeID"].ToString()) || !string.IsNullOrEmpty(this.bindedDistributionDetailsTable.Rows[i]["Account"].ToString()))
                        {
                            if (!string.IsNullOrEmpty(this.bindedDistributionDetailsTable.Rows[i]["Percentage"].ToString()) && !string.IsNullOrEmpty(this.bindedDistributionDetailsTable.Rows[i]["ItemTypeID"].ToString()) && !string.IsNullOrEmpty(this.bindedDistributionDetailsTable.Rows[i]["Account"].ToString()))
                            {
                                reqFieldValidateflag = true;
                                validRowsCount++;
                            }
                            else
                            {
                                reqFieldValidateflag = false;
                                break;
                            }
                        }
                    }
                    if (validRowsCount == 0)
                    {
                        reqFieldValidateflag = false;                        
                    }
                }

                if (ValidateFields() && reqFieldValidateflag == true)                
                {
                    if (Convert.ToInt16(this.RollYearTextBox.Text) == 0)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("DistrictSaveMessage"), SharedFunctions.GetResourceString("DistrictHeaderMessage"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.IDNumberTextBox.Focus();
                        sliceValidationFields.DisableNewMethod = true;
                    }
                    else
                    {
                        saveResult=this.SaveImprovementDistrictDefinition();
                        saveResult = int.TryParse(primaryKeyID, out distictId);
                        if (saveResult)
                        {
                            sliceValidationFields.DisableNewMethod = false;
                            sliceValidationFields.ErrorMessage = string.Empty;
                            sliceValidationFields.RequiredFieldMissing = false;               
                        }
                        else
                        {
                            //if (primaryKeyID.Contains(SharedFunctions.GetResourceString("ExistingDistrict")))
                            //{
                            //    DialogResult currentResult=MessageBox.Show(primaryKeyID, SharedFunctions.GetResourceString("DistrictDuplicateHeader"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                            //   // DialogResult currentResult = MessageBox.Show(primaryKeyID + SharedFunctions.GetResourceString("SaveDistrictMessage"), SharedFunctions.GetResourceString("DuplicateDistrictHeader"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            //    if (DialogResult.OK == currentResult)
                            //    {
                            //        this.flagRevokeChangesOnDuplicate = true;
                            //        sliceValidationFields.DisableNewMethod = false;
                            //        sliceValidationFields.ErrorMessage = string.Empty;
                            //        sliceValidationFields.RequiredFieldMissing = false;
                            //        primaryKeyID = string.Empty;
                            //       // return sliceValidationFields;
                            //    }
                            //    //else if (DialogResult.No == currentResult)
                            //    //{
                            //    //    this.flagRevokeChangesOnDuplicate = true;
                            //    //    sliceValidationFields.DisableNewMethod = false;
                            //    //    sliceValidationFields.ErrorMessage = string.Empty;
                            //    //    sliceValidationFields.RequiredFieldMissing = false;
                            //    //    primaryKeyID = string.Empty;
                            //    //    return sliceValidationFields;
                                    
                            //    //}
                            //    else
                            //    {
                            //        sliceValidationFields.DisableNewMethod = true;
                            //        this.IDNumberTextBox.Focus();
                            //        primaryKeyID = string.Empty;
                            //    }
                            //}
                            //else
                            //{
                            if (primaryKeyID.Contains(SharedFunctions.GetResourceString("ExistingDistrict")))
                            {
                                MessageBox.Show(primaryKeyID, SharedFunctions.GetResourceString("DistrictDuplicateHeader"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show(primaryKeyID, SharedFunctions.GetResourceString("DistrictMessageHeader"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                                
                                this.IDNumberTextBox.Focus();
                                sliceValidationFields.DisableNewMethod = true;
                                sliceValidationFields.ErrorMessage = string.Empty;
                                sliceValidationFields.RequiredFieldMissing = false;
                                primaryKeyID = string.Empty;
                            //}
                        }
                    }
                }
                else
                {
                    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("ExciseTaxAffDvtMissing");
                    sliceValidationFields.RequiredFieldMissing = true;
                    this.IDNumberTextBox.Focus();
                }
            }
            catch(Exception exp)
            { 
            
            }
           return sliceValidationFields;
        }

        /// <summary>
        /// Validates the fields(returns true on validation success.
        /// </summary>
        /// <returns>validation result</returns>
        private bool ValidateFields()
        {
            bool validationResult = true;
            if (((string.IsNullOrEmpty(this.IDNumberTextBox.Text.Trim())) || (string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim())) || (string.IsNullOrEmpty(this.ImpDistrictNameTextBox.Text.Trim())) || (string.IsNullOrEmpty(this.IdTypeTextBox.Text.Trim())) || (!this.ValidateDistributionItems())))
            {
                validationResult = false;
                return validationResult;
            }

            return validationResult;
        }

        /// <summary>
        /// Sets the default roll year.
        /// </summary>
        private void SetDefaultRollYear()
        {
            this.getRollYearConfigurationValue.GetCommentsConfigDetails.Clear();
            this.getRollYearConfigurationValue = this.form16040Control.WorkItem.GetConfigDetails(SharedFunctions.GetResourceString("DefaultRollYear"));
            if (this.getRollYearConfigurationValue.GetCommentsConfigDetails.Rows.Count > 0)
            {
                if (int.TryParse(this.getRollYearConfigurationValue.GetCommentsConfigDetails[0][this.getRollYearConfigurationValue.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString(), out this.defaultRollYear))
                {
                    this.configRollYear = this.defaultRollYear;
                }
            }
        }

        /// <summary>
        /// Id Type PictureBox click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdTypePictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.selectedTypeid = 0;
                Form districtTypeselect = new Form();
                Form formInfo = new Form();
                object[] optionalParameter = null;
                formInfo = this.form16040Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1044, optionalParameter, this.form16040Control.WorkItem);
                if (formInfo != null)
                {
                    if (formInfo.ShowDialog() == DialogResult.OK)
                    {
                        this.selectedTypeid = Convert.ToInt16(TerraScanCommon.GetValue(formInfo, "CommandResult").ToString());
                        this.IdTypeTextBox.Text = TerraScanCommon.GetValue(formInfo, "CommandValue").ToString();
                    }
                }
            }
            catch(Exception ex)
            {
            }
        }

        /// <summary>
        /// Enables the new mode.
        /// </summary>
        private void EnableNewMode()
        {
            for (int row = 0; row < this.DistributionGridView.NumRowsVisible; row++)
            {             
                this.bindedDistributionDetailsTable.AddGetDistributionDetailsRow(this.bindedDistributionDetailsTable.NewGetDistributionDetailsRow());
            }
        }

        /// <summary>
        /// load summary details
        /// </summary>
        private void PopulateSummaryDetails()
        {
            try
            {
                decimal levyAmt = 0.0m;
                decimal paidAmt = 0.0m;

                this.ParcelNoTextBox.Text = getImprovementDistrictDefinition.GetSummaryDetails[0]["ParcelCount"].ToString();

                decimal.TryParse(getImprovementDistrictDefinition.GetSummaryDetails[0]["TotalLevied"].ToString(), out levyAmt);
                this.LeviedAmountTextBox.Text = levyAmt.ToString();

                decimal.TryParse(getImprovementDistrictDefinition.GetSummaryDetails[0]["TotalPaid"].ToString(), out paidAmt);
                this.PaidAmtTextBox.Text = paidAmt.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Populate District Details.
        /// </summary>
        private void PopulateDistrictDetails()
        {
            try
            {
                if (getImprovementDistrictDefinition.GetDistrictDetails.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(getImprovementDistrictDefinition.GetDistrictDetails[0].SADistrictID.ToString()))
                    {
                        this.IDNumberTextBox.Text = getImprovementDistrictDefinition.GetDistrictDetails[0].District.Trim().ToString();
                    }

                    if (!string.IsNullOrEmpty(getImprovementDistrictDefinition.GetDistrictDetails[0].SAName.ToString()))
                    {
                        this.ImpDistrictNameTextBox.Text = getImprovementDistrictDefinition.GetDistrictDetails[0].SAName.Trim().ToString();
                    }

                    if (!string.IsNullOrEmpty(getImprovementDistrictDefinition.GetDistrictDetails[0].RollYear.ToString()))
                    {
                        this.RollYearTextBox.Text = this.getImprovementDistrictDefinition.GetDistrictDetails[0].RollYear.Trim().ToString();
                    }

                    if (!string.IsNullOrEmpty(getImprovementDistrictDefinition.GetDistrictDetails[0].NumPayments.ToString()))
                    {
                        this.PaymentsNoTextBox.Text = this.getImprovementDistrictDefinition.GetDistrictDetails[0].NumPayments.Trim().ToString();
                    }

                    if (!getImprovementDistrictDefinition.GetDistrictDetails[0].IsLevyDateNull())
                    {
                        if (!string.IsNullOrEmpty(getImprovementDistrictDefinition.GetDistrictDetails[0].LevyDate.ToString()))
                        {
                            this.LevyDateTextBox.Text = this.getImprovementDistrictDefinition.GetDistrictDetails[0].LevyDate.ToString();
                        }
                    }

                    else { this.LevyDateTextBox.Text = string.Empty; }

                    if (!getImprovementDistrictDefinition.GetDistrictDetails[0].IsFirstDueNull())
                    {
                        if (!string.IsNullOrEmpty(getImprovementDistrictDefinition.GetDistrictDetails[0].FirstDue.ToString()))
                        {
                            this.FirstDueTextBox.Text = this.getImprovementDistrictDefinition.GetDistrictDetails[0].FirstDue.ToString();
                        }
                    }
                    else { this.FirstDueTextBox.Text = string.Empty; }

                    if (!getImprovementDistrictDefinition.GetDistrictDetails[0].IsSecondDueNull())
                    {
                        if (!string.IsNullOrEmpty(getImprovementDistrictDefinition.GetDistrictDetails[0].SecondDue.ToString()))
                        {
                            this.SecondDueTextBox.Text = this.getImprovementDistrictDefinition.GetDistrictDetails[0].SecondDue.ToString();
                        }
                    }
                    else { this.SecondDueTextBox.Text = string.Empty; }

                    if (!string.IsNullOrEmpty(getImprovementDistrictDefinition.GetDistrictDetails[0].HasGrace.ToString()))
                    {
                        string graceValue = this.getImprovementDistrictDefinition.GetDistrictDetails[0].HasGrace.ToString();

                        if (graceValue == "False")
                        {
                            this.GraceFlagComboBox.SelectedItem = SharedFunctions.GetResourceString("NOValue");
                        }
                        else
                        {
                            this.GraceFlagComboBox.SelectedItem = SharedFunctions.GetResourceString("YESValue");
                        }
                    }

                    if (!string.IsNullOrEmpty(getImprovementDistrictDefinition.GetDistrictDetails[0].GracePeriod.ToString()))
                    {
                        this.GracePeriodTextBox.Text = this.getImprovementDistrictDefinition.GetDistrictDetails[0].GracePeriod.ToString();
                    }

                    if (!string.IsNullOrEmpty(getImprovementDistrictDefinition.GetDistrictDetails[0].IDType.ToString()))
                    {
                        this.IdTypeTextBox.Text = this.getImprovementDistrictDefinition.GetDistrictDetails[0].IDType.ToString();
                    }

                    if (!string.IsNullOrEmpty(getImprovementDistrictDefinition.GetDistrictDetails[0].InterestMethod.ToString()))
                    {
                        this.InterestMethodComboBox.Text = this.getImprovementDistrictDefinition.GetDistrictDetails[0].InterestMethod.ToString();
                    }

                    if(!string.IsNullOrEmpty(getImprovementDistrictDefinition.GetDistrictDetails[0].DelqInterestCalc.ToString()))
                    {
                        this.DelqInterestCalqComboBox.Text = this.getImprovementDistrictDefinition.GetDistrictDetails[0].DelqInterestCalc.ToString();
                    }

                    if (!getImprovementDistrictDefinition.GetDistrictDetails[0].IsInterestRateNull())
                    {
                        if (!string.IsNullOrEmpty(getImprovementDistrictDefinition.GetDistrictDetails[0].InterestRate.ToString()))
                        {
                            this.InterestTextBox.Text = ConvertPercentageFormat(getImprovementDistrictDefinition.GetDistrictDetails[0].InterestRate.ToString());
                        }
                    }

                    if (!getImprovementDistrictDefinition.GetDistrictDetails[0].IsDelqInterestRateNull())
                    {
                        if (!string.IsNullOrEmpty(getImprovementDistrictDefinition.GetDistrictDetails[0].DelqInterestRate.ToString()))
                        {
                            this.DelqInterestTextBox.Text = ConvertPercentageFormat(getImprovementDistrictDefinition.GetDistrictDetails[0].DelqInterestRate.ToString());
                        }
                    }

                    if (!getImprovementDistrictDefinition.GetDistrictDetails[0].IsPenaltyRateNull())
                    {
                        if (!string.IsNullOrEmpty(getImprovementDistrictDefinition.GetDistrictDetails[0].PenaltyRate.ToString()))
                        {
                            this.PenaltyTextBox.Text = ConvertPercentageFormat(getImprovementDistrictDefinition.GetDistrictDetails[0].PenaltyRate.ToString());
                        }
                    }

                    if (!getImprovementDistrictDefinition.GetDistrictDetails[0].IsCommissionRateNull())
                    {
                        if (!string.IsNullOrEmpty(getImprovementDistrictDefinition.GetDistrictDetails[0].CommissionRate.ToString()))
                        {
                            this.CommissionTextBox.Text = ConvertPercentageFormat(getImprovementDistrictDefinition.GetDistrictDetails[0].CommissionRate.ToString());
                        }
                    }
                    if (!getImprovementDistrictDefinition.GetDistrictDetails[0].IsCommissionRateNull())
                    {
                        if (!string.IsNullOrEmpty(getImprovementDistrictDefinition.GetDistrictDetails[0].CommPenRate.ToString()))
                        {
                            this.CommissionOnPenTextBox.Text = ConvertPercentageFormat(getImprovementDistrictDefinition.GetDistrictDetails[0].CommPenRate.ToString());
                        }
                    }

                    if (selectedTypeid == 0)
                    {
                        selectedOnloadTypeid= 0;
                        int.TryParse(getImprovementDistrictDefinition.GetDistrictDetails[0]["IDTypeID"].ToString(), out selectedOnloadTypeid);
                    }

                    if (this.getImprovementDistrictDefinition.GetDistrictDetails[0].IsRolled == "0")
                    {
                        this.RollDistrictButton.Enabled = true;
                    }
                    else
                    {
                        this.RollDistrictButton.Enabled = false;
                    }

                    if (this.getImprovementDistrictDefinition.GetDistributionDetails.Count > 0)
                    {
                        this.ParcelGridButton.Enabled = true;
                    }
                    else
                    {
                        this.ParcelGridButton.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Clear Improvement District Details.
        /// </summary>
        private void ClearImprovementDistrictDetails() 
        { 
            this.IDNumberTextBox.Text=string.Empty;
            this.ImpDistrictNameTextBox.Text=string.Empty;
            this.PaymentsNoTextBox.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.LevyDateTextBox.Text=string.Empty;
            this.FirstDueTextBox.Text=string.Empty;
            this.SecondDueTextBox.Text =string.Empty;
            this.GraceFlagComboBox.SelectedIndex = -1;
            this.GracePeriodTextBox.Text=string.Empty;
            this.PenaltyTextBox.Text=string.Empty;
            this.CommissionTextBox.Text=string.Empty;
            this.ParcelNoTextBox.Text = "0";
            this.LeviedAmountTextBox.Text = string.Empty;
            this.PaidAmtTextBox.Text = string.Empty;
            this.CommissionOnPenTextBox.Text=string.Empty;
            this.IdTypeTextBox.Text=string.Empty;
            this.InterestMethodComboBox.SelectedIndex = -1;
            this.InterestTextBox.Text=string.Empty;
            this.DelqInterestTextBox.Text=string.Empty;
            this.DelqInterestCalqComboBox.SelectedIndex =-1;
            this.LevyDatePanel.BackColor = Color.White;
            this.LevyDateTextBox.BackColor = Color.White;
            this.LevyDateLabel.BackColor = Color.White;
            this.bindedDistributionDetailsTable.Clear();
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void LockControls(bool enable)
        {
            this.IDNumberTextBox.LockKeyPress = enable;
            this.ImpDistrictNameTextBox.LockKeyPress = enable;
            this.PaymentsNoTextBox.LockKeyPress = enable;
            this.LevyDateTextBox.LockKeyPress = enable;
            this.FirstDueTextBox.LockKeyPress = enable;
            this.SecondDueTextBox.LockKeyPress = enable;
            this.GracePeriodTextBox.LockKeyPress = enable;
            this.PenaltyTextBox.LockKeyPress = enable;
            this.CommissionTextBox.LockKeyPress = enable;
            this.CommissionOnPenTextBox.LockKeyPress = enable;
            this.InterestTextBox.LockKeyPress = enable;
            this.DelqInterestTextBox.LockKeyPress = enable;
            this.DistributionGridView.ReadOnly = enable;
        }

        /// <summary>
        /// Enables / Disables the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableControls(bool enable)
        {
            this.IDNumberTextBox.Enabled = enable;
            this.ImpDistrictNameTextBox.Enabled = enable;
            this.PaymentsNoTextBox.Enabled = enable;
            this.LevyDateTextBox.Enabled = enable;
            this.FirstDueTextBox.Enabled = enable;
            this.SecondDueTextBox.Enabled = enable;
            this.GracePeriodTextBox.Enabled = enable;
            this.PenaltyTextBox.Enabled = enable;
            this.CommissionTextBox.Enabled = enable;
            this.CommissionOnPenTextBox.Enabled = enable;
            this.InterestTextBox.Enabled = enable;
            this.DelqInterestTextBox.Enabled = enable;
            this.DistrictHeaderPanel.Enabled = enable;
            this.TotalLeviedAmtLabel.Enabled = enable;
            this.TotalPaidAmtlabel.Enabled = enable;
            this.ParcelNumberLabel.Enabled = enable;
            this.ParcelGridButton.Enabled = enable;
        }

        /// <summary>
        /// Converts the string to decimal.
        /// </summary>
        /// <param name="textboxValue">The textbox value.</param>
        /// <returns>textboxValue</returns>
        private decimal ConvertStringToDecimal(string textboxValue)
        {
            decimal outValue = 0;
            if (!string.IsNullOrEmpty(textboxValue.Trim()))
            {
                decimal.TryParse(textboxValue.Trim(), out outValue);
            }

            return outValue;
        }
        
        /// <summary>
        /// Click Roll Button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RollDistrictButton_Click(object sender, EventArgs e)
        {
            this.getImprovementDistrictDefinition = this.form16040Control.WorkItem.RollOver_ImprovementDistrict(this.activeRecord, this.userid);
            this.getImprovementDistrictDefinition = this.form16040Control.WorkItem.GetDistrictDefinitionDetails(this.activeRecord);
            if (getImprovementDistrictDefinition.GetDistrictDetails.Rows.Count > 0)
            {
                if (this.getImprovementDistrictDefinition.GetDistrictDetails[0].IsRolled == "0")
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("DistrictRollButton"), SharedFunctions.GetResourceString("DistrictDefinition"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.RollDistrictButton.Enabled = true;
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("DistrictButton"), SharedFunctions.GetResourceString("DistrictDefinition"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.RollDistrictButton.Enabled = false;
                }

            }
            this.IDNumberTextBox.Focus();
            
        }

        /// <summary>
        /// Load District Details.
        /// </summary>
        private void LoadDistrictDetails()
        {
            var loadDistirctDetails = this.form16040Control.WorkItem.GetDistrictDetails(keyID);
        }

        /// <summary>
        /// Set Grid Columns.
        /// </summary>
        private void SetGridColumns()
        {
            DataGridViewColumnCollection distributionDetailsColumns = this.DistributionGridView.Columns;
            this.DistributionGridView.AutoGenerateColumns = false;
            this.DistributionGridView.ApplyStandardBehaviour = true;
            distributionDetailsColumns[SharedFunctions.GetResourceString("DistributionType")].DataPropertyName = this.getImprovementDistrictDefinition.GetDistributionDetails.ItemTypeIDColumn.ColumnName;
            distributionDetailsColumns[SharedFunctions.GetResourceString("F36000PercentageColumnName")].DataPropertyName = this.getImprovementDistrictDefinition.GetDistributionDetails.PercentageColumn.ColumnName;
            distributionDetailsColumns[SharedFunctions.GetResourceString("Account")].DataPropertyName = this.getImprovementDistrictDefinition.GetDistributionDetails.AccountColumn.ColumnName;
            distributionDetailsColumns[SharedFunctions.GetResourceString("AccountStatus")].DataPropertyName = this.getImprovementDistrictDefinition.GetDistributionDetails.AccountStatusColumn.ColumnName;
            distributionDetailsColumns[SharedFunctions.GetResourceString("AccountID")].DataPropertyName = this.getImprovementDistrictDefinition.GetDistributionDetails.AccountIDColumn.ColumnName;
            distributionDetailsColumns[SharedFunctions.GetResourceString("SARateItemID")].DataPropertyName = this.getImprovementDistrictDefinition.GetDistributionDetails.SARateItemIDColumn.ColumnName;
            distributionDetailsColumns[SharedFunctions.GetResourceString("SADistrictID")].DataPropertyName = this.getImprovementDistrictDefinition.GetDistributionDetails.SADistrictIDColumn.ColumnName;
            distributionDetailsColumns[SharedFunctions.GetResourceString("DistributionType")].DisplayIndex = 0;
            distributionDetailsColumns[SharedFunctions.GetResourceString("F36000PercentageColumnName")].DisplayIndex = 1;
            distributionDetailsColumns[SharedFunctions.GetResourceString("Account")].DisplayIndex = 2;
            (this.DistributionGridView.Columns[SharedFunctions.GetResourceString("DistributionType")] as DataGridViewComboBoxColumn).DataSource = this.getImprovementDistrictDefinition.GetDistributionDetails;
            (this.DistributionGridView.Columns[SharedFunctions.GetResourceString("DistributionType")] as DataGridViewComboBoxColumn).DisplayMember = this.getImprovementDistrictDefinition.GetDistributionDetails.DistributionTypeColumn.ColumnName;
            (this.DistributionGridView.Columns[SharedFunctions.GetResourceString("DistributionType")] as DataGridViewComboBoxColumn).ValueMember = this.getImprovementDistrictDefinition.GetDistributionDetails.ItemTypeIDColumn.ColumnName;
        }

        /// <summary>
        /// Populate Distribution details gridview
        /// </summary>
        private void PopulateDistributionDetailsGridView()
        {
            try
            {
                // clear the table for repopulating the data
                this.bindedDistributionDetailsTable.Clear();

                if (this.DistributionGridView.Columns.Count > 0)
                {
                    //// create form level datatable for ratelist information                
                    F16040ImprovementDistrictDefinition.GetDistributionDetailsRow[] tempTable;
                    if (this.flagDistributionSortDirection)
                    {
                        tempTable = (F16040ImprovementDistrictDefinition.GetDistributionDetailsRow[])this.getImprovementDistrictDefinition.GetDistributionDetails.Copy().Select("1=1", this.DistributionGridView.Columns[this.distributionGridViewSelectColumnNo].DataPropertyName + SharedFunctions.GetResourceString("DESC"));
                        this.DistributionGridView.Columns[this.distributionGridViewSelectColumnNo].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                    else
                    {
                        tempTable = (F16040ImprovementDistrictDefinition.GetDistributionDetailsRow[])this.getImprovementDistrictDefinition.GetDistributionDetails.Copy().Select("1=1", this.DistributionGridView.Columns[this.distributionGridViewSelectColumnNo].DataPropertyName + SharedFunctions.GetResourceString("ASC"));
                        this.DistributionGridView.Columns[this.distributionGridViewSelectColumnNo].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }

                    //// add the rows after sorting
                    foreach (F16040ImprovementDistrictDefinition.GetDistributionDetailsRow myrow in tempTable)
                    {
                        this.bindedDistributionDetailsTable.ImportRow(myrow);
                    }
                }

                //// bind the table values to the grid
                ////added code to fix Bug#4192
                this.DistributionGridView.DataSource = this.bindedDistributionDetailsTable;
                this.distributionSource.DataSource = this.bindedDistributionDetailsTable;
                int itemFoundDist = this.distributionSource.Find("SARateItemID", this.selectedDistributionItem);

                //// add a default empty row for DistributionDetailsDataGrid
                this.bindedDistributionDetailsTable.AddGetDistributionDetailsRow(this.bindedDistributionDetailsTable.NewGetDistributionDetailsRow());

                //// add necessary empty rows
                for (int row = this.bindedDistributionDetailsTable.Rows.Count; row < this.DistributionGridView.NumRowsVisible; row++)
                {
                    this.bindedDistributionDetailsTable.AddGetDistributionDetailsRow(this.bindedDistributionDetailsTable.NewGetDistributionDetailsRow());
                }

                // set the actual row count for distribution details grid
                this.DistributionGridView.NumRowsVisible = 7;
                this.distributionListRowCount = this.bindedDistributionDetailsTable.Rows.Count;
                if (this.distributionListRowCount > 7)
                {
                    this.DistributionDetailsVscrollBar.Visible = false;
                }
                else
                {
                    this.DistributionDetailsVscrollBar.Visible = true;
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
            catch (Exception ex)
            {
            }
        }

        #region Distribution Gridview events

        /// <summary>
        /// Handles the Cell Begin Edit Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DistributionGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
                {
                    e.Cancel = true;
                }

                this.cellValueWhenEditing = this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value.ToString();
                this.flagDistributionItemDeleteEnabled = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Cell Click Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0) && (e.RowIndex < (this.DistributionGridView.RowCount - 1)))
                        {
                            imgCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.DistributionGridView[e.ColumnIndex, e.RowIndex].InheritedStyle.BackColor.R), Convert.ToInt32(this.DistributionGridView[e.ColumnIndex, e.RowIndex].InheritedStyle.BackColor.G), Convert.ToInt32(this.DistributionGridView[e.ColumnIndex, e.RowIndex].InheritedStyle.BackColor.B));
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
                    if (this.bindedDistributionDetailsTable.Rows[rowIndex][this.bindedDistributionDetailsTable.SARateItemIDColumn] != null)
                    {
                        if (!string.IsNullOrEmpty(this.bindedDistributionDetailsTable.Rows[rowIndex][this.bindedDistributionDetailsTable.SARateItemIDColumn].ToString()))
                        {
                            this.selectedDistributionItem = Convert.ToInt32(this.bindedDistributionDetailsTable.Rows[rowIndex][this.bindedDistributionDetailsTable.SARateItemIDColumn]);
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
        /// Handles the Cell End Edit event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DistributionGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.flagDistributionItemDeleteEnabled = false;
                if (e.RowIndex >= this.DistributionGridView.NumRowsVisible - 1)
                {
                    if ((e.RowIndex + 1) == this.bindedDistributionDetailsTable.Rows.Count)
                    {
                        this.bindedDistributionDetailsTable.AddGetDistributionDetailsRow(this.bindedDistributionDetailsTable.NewGetDistributionDetailsRow());
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
                                decimal.TryParse(this.bindedDistributionDetailsTable.Compute("SUM(" + this.DistributionGridView.Columns[1].DataPropertyName + ")", (this.DistributionGridView.Columns[0] as DataGridViewComboBoxColumn).ValueMember + "=" + this.DistributionGridView[0, e.RowIndex].Value.ToString()).ToString(), out defaultValue);
                            }
                            defaultValue = 1 - defaultValue;

                            if ((defaultValue > 0) && (defaultValue <= 100))
                            {
                                this.DistributionGridView[1, e.RowIndex].Value = defaultValue;
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
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the cell mouse click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DistributionGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (!this.flagAccountShow)
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
                                    optionalParameter[0] = this.configRollYear;
                                }

                                Form accountSelectionForm = TerraScanCommon.GetForm(1345, optionalParameter, this.form16040Control.WorkItem);
                                if (accountSelectionForm != null)
                                {
                                    if (accountSelectionForm.ShowDialog() == DialogResult.OK)
                                    {
                                        int.TryParse(TerraScanCommon.GetValue(accountSelectionForm, "AccountId").ToString(), out accountId);
                                        ExciseTaxRateData accountNameDataSet = new ExciseTaxRateData();
                                        accountNameDataSet = this.form16040Control.WorkItem.GetAccountName(accountId);

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
                                                if ((e.RowIndex + 1) == this.bindedDistributionDetailsTable.Rows.Count)
                                                {
                                                    this.bindedDistributionDetailsTable.AddGetDistributionDetailsRow(this.bindedDistributionDetailsTable.NewGetDistributionDetailsRow());
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
        /// Handles the event of Cell parsing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Handles the event of Cell Value Changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DistributionGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
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
        /// Handles the event of Header Mouse Click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Handles the event of Data Binding Complete.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    foreach (DataGridViewColumn column in ((DataGridView)sender).Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.Programmatic;
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
        /// Handles the Datagridview Enter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Handles the Gridview Keydown event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        ////if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                        ////{
                        //    if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
                        //    {
                        //        for (int i = 0; i <= this.bindedDistributionDetailsTable.Rows.Count - 1; i++)
                        //        {
                        //            if (this.bindedDistributionDetailsTable.Rows[i][this.bindedDistributionDetailsTable.SARateItemIDColumn] != null)
                        //            {
                        //                if (!string.IsNullOrEmpty(this.bindedDistributionDetailsTable.Rows[i][this.bindedDistributionDetailsTable.SARateItemIDColumn].ToString()))
                        //                {
                        //                    if (!string.IsNullOrEmpty(this.bindedDistributionDetailsTable.Rows[i][this.bindedDistributionDetailsTable.ItemTypeIDColumn].ToString()))
                        //                    {
                        //                        if (this.bindedDistributionDetailsTable.Rows[i][this.bindedDistributionDetailsTable.ItemTypeIDColumn].ToString() == "1")
                        //                        {
                        //                            double.TryParse(this.bindedDistributionDetailsTable.Rows[i][this.bindedDistributionDetailsTable.PercentageColumn].ToString(), out taxcount1);
                        //                            taxcount = taxcount + taxcount1;
                        //                            tcount = tcount + 1;
                        //                        }
                        //                    }

                        //                    if (!string.IsNullOrEmpty(this.bindedDistributionDetailsTable.Rows[i][this.bindedDistributionDetailsTable.ItemTypeIDColumn].ToString()))
                        //                    {
                        //                        if (this.bindedDistributionDetailsTable.Rows[i][this.bindedDistributionDetailsTable.ItemTypeIDColumn].ToString() == "4")
                        //                        {
                        //                            double.TryParse(this.bindedDistributionDetailsTable.Rows[i][this.bindedDistributionDetailsTable.PercentageColumn].ToString(), out tintcount1);
                        //                            tintcount = tintcount + tintcount1;
                        //                            icount = icount + 1;
                        //                        }
                        //                    }
                        //                }
                        //            }
                        //        }


                        //            this.flagDistributionItemDeleteEnabled = false;
                        //            this.bindedDistributionDetailsTable.Rows[this.distributionListRowCount].Delete();
                        //            if (this.distributionListRowCount < this.getImprovementDistrictDefinition.GetDistributionDetails.Rows.Count)
                        //            {
                        //                this.getImprovementDistrictDefinition.GetDistributionDetails.Rows[this.distributionListRowCount].Delete();
                                         

                        //                    if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                        //                    {
                        //                        
                        //                    }
                        //            }

                        //            this.bindedDistributionDetailsTable.AddGetDistributionDetailsRow(this.bindedDistributionDetailsTable.NewGetDistributionDetailsRow());
                        //    }
                        ////}
                         if (this.pageMode == TerraScanCommon.PageModeTypes.Edit || this.pageMode == TerraScanCommon.PageModeTypes.New || this.pageMode == TerraScanCommon.PageModeTypes.View)
                        { 
                                this.bindedDistributionDetailsTable.Rows[this.distributionListRowCount].Delete();
                                this.bindedDistributionDetailsTable.AddGetDistributionDetailsRow(this.bindedDistributionDetailsTable.NewGetDistributionDetailsRow());
                              if(this.pageMode == TerraScanCommon.PageModeTypes.View)
                             {
                                this.EnableEditMode();
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
        /// Handles the distribution gridview leave event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Handles the event of Row enter event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                                imgCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.DistributionGridView[e.ColumnIndex, e.RowIndex].InheritedStyle.BackColor.R), Convert.ToInt32(this.DistributionGridView[e.ColumnIndex, e.RowIndex].InheritedStyle.BackColor.G), Convert.ToInt32(this.DistributionGridView[e.ColumnIndex, e.RowIndex].InheritedStyle.BackColor.B));
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
        /// Handles the Editing Control showing event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DistributionGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (this.flagFormLoad)
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 255, 255);
                }

                e.Control.TextChanged += new EventHandler(this.DistributionGridView_TextChanged);
                e.Control.Validated += new EventHandler(this.DistributionGridView_Validated);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the event of Row Header Mouse Click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DistributionGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.DistributionGridView.CurrentCell != null)
                {
                    this.flagDistributionItemDeleteEnabled = true;
                    this.distributionListRowCount = e.RowIndex;

                    if (this.bindedDistributionDetailsTable.Rows[e.RowIndex][this.bindedDistributionDetailsTable.SARateItemIDColumn] != null)
                    {
                        if (!string.IsNullOrEmpty(this.bindedDistributionDetailsTable.Rows[e.RowIndex][this.bindedDistributionDetailsTable.SARateItemIDColumn].ToString()))
                        {
                            this.selectedDistributionItem = Convert.ToInt32(this.bindedDistributionDetailsTable.Rows[e.RowIndex][this.bindedDistributionDetailsTable.SARateItemIDColumn]);
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
        /// Handles the Validation event of Distribution Gridview control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DistributionGridView_Validated(object sender, EventArgs e)
        {
            try
            {
                this.DistributionGridView.EditingControl.TextChanged -= new EventHandler(this.DistributionGridView_TextChanged);
            }
            catch (Exception ex)
            {
                //ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the DistributionGridViewControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_TextChanged(object sender, EventArgs e)
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

        #endregion Distribution Gridview events.   
  
        /// <summary>
        /// Enables the edit mode.
        /// </summary>
        private void EnableEditMode(object sender, EventArgs e)
        {
            if (this.PermissionEdit && (this.pageMode == TerraScanCommon.PageModeTypes.View))
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        /// <summary>
        /// edits the enabled event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Levy date button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LevyDateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.LevyDateTextBox.Text.Trim()))
                {
                    this.LevyTimePicker.Value = Convert.ToDateTime(LevyDateTextBox.Text);
                }
                else
                {
                    this.LevyTimePicker.Value = DateTime.Today;
                }

                this.LevyTimePicker.Focus();
                SendKeys.Send("{F4}");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// TimerImage_Click
        /// </summary>
        /// <param name="textControl">textControl</param>
        /// <param name="timePickerControl">timePickerControl</param>
        private void TimerImage_Click(TextBox textControl, DateTimePicker timePickerControl)
        {
            this.ParentForm.AcceptButton = null;
            try
            {
                timePickerControl.BringToFront();
                if (!string.IsNullOrEmpty(textControl.Text.Trim()))
                {
                    timePickerControl.Value = Convert.ToDateTime(textControl.Text);
                }
                else
                {
                    timePickerControl.Value = DateTime.Today;
                }

                SendKeys.Send(SharedFunctions.GetResourceString("F4"));
                timePickerControl.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }

        }

        /// <summary>
        /// Shows the levy date calender.
        /// </summary>
        private void ShowLevyDateText()
        {
            this.DistrictMonthCalender.Visible = true;
            // Set the calendar to move one month at a time when navigating using the arrows.
            this.DistrictMonthCalender.ScrollChange = 1;
            // Set the calendar location.
            this.DistrictMonthCalender.Left = this.LevyDatePanel.Left + this.LevyDateButton.Left + this.LevyDateButton.Width;
            this.DistrictMonthCalender.Top = this.LevyDatePanel.Top + this.LevyDateButton.Top;
            this.DistrictMonthCalender.Tag = this.LevyDateButton.Tag;
            this.DistrictMonthCalender.Focus();
            if (!string.IsNullOrEmpty(this.LevyDateTextBox.Text))
            {
                this.DistrictMonthCalender.SetDate(this.LevyDateTextBox.DateTextBoxValue);
            }
        }

        /// <summary>
        /// Shows the First due date calender.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstDueDateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.FirstDueTextBox.Text.Trim()))
                {
                    this.FirstTimePicker.Value = Convert.ToDateTime(this.FirstDueTextBox.Text);
                }
                else
                {
                    this.FirstTimePicker.Value = DateTime.Today;
                }

                this.FirstTimePicker.Focus();
                SendKeys.Send("{F4}");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Shows the Second due date calender.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecondDueDateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.SecondDueTextBox.Text.Trim()))
                {
                    this.SecondTimePicker.Value = Convert.ToDateTime(this.SecondDueTextBox.Text);
                }
                else
                {
                    this.SecondTimePicker.Value = DateTime.Today;
                }

                this.SecondTimePicker.Focus();
                SendKeys.Send("{F4}");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the seleted date.
        /// </summary>
        /// <param name="selectedDate">The selected date.</param>
        private void SetSeletedDate(DateTime selectedDate)
        {
            if (String.Compare(this.DistrictMonthCalender.Tag.ToString(), this.LevyDateTextBox.Name, true) == 0)
            {
                this.DistrictMonthCalender.Tag = string.Empty;
                this.LevyDateTextBox.Text = selectedDate.ToString();
                this.ChangeDateBackGround(this.LevyDateTextBox);
                this.LevyDateButton.Focus();
                this.DistrictMonthCalender.Visible = false;
            }
            else if (String.Compare(this.DistrictMonthCalender.Tag.ToString(), this.FirstDueDateButton.Name, true) == 0)
            {
                this.DistrictMonthCalender.Tag = string.Empty;
                this.FirstDueTextBox.Text = selectedDate.ToString();
                this.ChangeDateBackGround(this.FirstDueTextBox);
                this.FirstDueDateButton.Focus();
                this.DistrictMonthCalender.Visible = false;
            }
            else if (String.Compare(this.DistrictMonthCalender.Tag.ToString(), this.SecondDueDateButton.Name, true) == 0)
            {
                this.DistrictMonthCalender.Tag = string.Empty;
                this.SecondDueTextBox.Text = selectedDate.ToString();
                this.ChangeDateBackGround(this.SecondDueTextBox);
                this.SecondDueDateButton.Focus();
                this.DistrictMonthCalender.Visible = false;
            }
        }

        /// <summary>
        /// Changes the date back ground with today.
        /// </summary>
        /// <param name="sourceTextBox">The source text box.</param>
        private void ChangeDateBackGround(TerraScanTextBox sourceTextBox)
        {
            ////change background color to red if date is not today
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View) || string.IsNullOrEmpty(sourceTextBox.Text) || sourceTextBox.DateTextBoxValue.Equals(System.DateTime.Today))
            {
                sourceTextBox.Parent.BackColor = Color.White;
                sourceTextBox.BackColor = Color.White;
            }
            else
            {
                sourceTextBox.Parent.BackColor = Color.FromArgb(238, 210, 211);
                sourceTextBox.BackColor = Color.FromArgb(238, 210, 211);
            }
        }

        /// <summary>
        /// district month calender date selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DistrictMonthCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.SetSeletedDate(e.Start);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// calender leave event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DistrictMonthCalender_Leave(object sender, EventArgs e)
        {
            this.DistrictMonthCalender.Visible = false;
        }

        /// <summary>
        /// key down district month.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DistrictMonthCalender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.SetSeletedDate(this.DistrictMonthCalender.SelectionStart);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void LevyDateTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    TerraScanTextBox sourceTextBox = sender as TerraScanTextBox;
                    ////change background color with today
                    //this.ChangeDateBackGround(sourceTextBox);

                    if (sourceTextBox.Equals(this.LevyDateTextBox) && this.receiptDateChanged)
                    {
                        ////change the text box value with today and close time
                        //this.LevyDateTextBox.Text = this.GetNextWorkingDay(this.ReceiptDateTextBox.DateTextBoxValue);
                        this.receiptDateChanged = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// text leave.
        /// </summary>
        /// <param name="txtValue"></param>
        /// <param name="txtBoxControl"></param>
        private void TextLeave(string txtValue, TerraScanTextBox txtBoxControl)
        {
            decimal d = 0;
            txtValue = txtValue.Replace("%", "").Trim();
            decimal.TryParse(txtValue, out d);
            if (d != 0)
            {
                if (txtValue.IndexOf('.') != -1)
                {
                    string replaceStr;
                    replaceStr = txtValue.Replace("%", "").Trim();
                   
                    if (replaceStr.Substring(replaceStr.IndexOf('.')).Length > 3)
                    {
                        
                        char[] ch = (replaceStr.Substring(replaceStr.IndexOf('.') + 3).ToString()).ToCharArray();
                        int flag = 0;
                        this.nonZero = null;
                        for (int i = ch.Length; i > 0; i--)
                        {
                            if (ch[i - 1] == '0')
                            {
                                if (flag == 0)
                                {
                                    ch[i - 1].ToString().Replace("0", "");
                                }
                                else
                                {
                                    this.nonZero = ch[i - 1].ToString() + this.nonZero;
                                }
                            }
                            else
                            {
                                flag = 1;
                                this.nonZero = ch[i - 1].ToString() + this.nonZero;
                            }
                        }
                        
                        this.concadinatedString = replaceStr.Substring(0, replaceStr.IndexOf('.') + 3).ToString() + this.nonZero;
                        if (this.concadinatedString.Substring(this.concadinatedString.IndexOf('.') + 1).Length == 3)
                        {
                            txtBoxControl.TextCustomFormat = "0.000 %";
                        }
                        else if (this.concadinatedString.Substring(this.concadinatedString.IndexOf('.') + 1).Length == 4)
                        {
                            txtBoxControl.TextCustomFormat = "0.0000 %";
                        }
                        else if (this.concadinatedString.Substring(this.concadinatedString.IndexOf('.') + 1).Length == 5)
                        {
                            txtBoxControl.TextCustomFormat = "0.00000 %";
                        }
                        else if (this.concadinatedString.Substring(this.concadinatedString.IndexOf('.') + 1).Length == 6)
                        {
                            txtBoxControl.TextCustomFormat = "0.000000 %";
                        }
                        else if (this.concadinatedString.Substring(this.concadinatedString.IndexOf('.') + 1).Length == 7)
                        {
                            txtBoxControl.TextCustomFormat = "0.000000 %";
                        }
                        else
                        {
                            txtBoxControl.TextCustomFormat = "0.00 %";
                        }

                        txtBoxControl.Text = this.concadinatedString;
                    }
                    else
                    {
                        txtBoxControl.TextCustomFormat = "0.00 %";
                        txtBoxControl.Text = txtValue;
                    }
                }
                else
                {
                    txtBoxControl.TextCustomFormat = "0.00 %";
                    txtBoxControl.Text = txtValue;
                }
            }
            else
            {
                txtBoxControl.TextCustomFormat = "0.00 %";
                txtBoxControl.Text = txtValue;
            }
        }

        /// <summary>
        /// penalty textbox leave.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PenaltyTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                 string calculatedValue = MarketPercentageFormat(this.PenaltyTextBox.Text);
                 PenaltyTextBox.Text = Convert.ToString(calculatedValue);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// commission textbox leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommissionTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                string calculatedValue = MarketPercentageFormat(this.CommissionTextBox.Text);
                CommissionTextBox.Text = Convert.ToString(calculatedValue);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        
        /// <summary>
        /// interest textbox leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InterestTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                string calculatedValue = MarketPercentageFormat(this.InterestTextBox.Text);
                InterestTextBox.Text = Convert.ToString(calculatedValue);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// delq interest textbox leave.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelqInterestTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                string calculatedValue = MarketPercentageFormat(this.DelqInterestTextBox.Text);
                DelqInterestTextBox.Text = Convert.ToString(calculatedValue);               
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// commission on penalty textbox leave.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommissionOnPenTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                string calculatedValue = MarketPercentageFormat(this.CommissionOnPenTextBox.Text);
                CommissionOnPenTextBox.Text = Convert.ToString(calculatedValue);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }

        }

        /// <summary>
        /// close up event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LevyTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.LevyDateTextBox.Text = this.LevyTimePicker.Text;
                this.LevyDateTextBox.Focus();
                this.ParentForm.ActiveControl = LevyDateTextBox;
                this.ParentForm.ActiveControl.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// keypress event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LevyTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int keyCode = (int)e.KeyChar;
                if (keyCode == 9)
                {
                    SendKeys.Send(SharedFunctions.GetResourceString("ESC"));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Close up event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.FirstDueTextBox.Text = this.FirstTimePicker.Text;
                this.FirstDueTextBox.Focus();
                this.ParentForm.ActiveControl = FirstDueTextBox;
                this.ParentForm.ActiveControl.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void FirstTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int keyCode = (int)e.KeyChar;
                if (keyCode == 9)
                {
                    SendKeys.Send(SharedFunctions.GetResourceString("ESC"));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void FirstTimePicker_KeyDown(object sender, KeyEventArgs e)
        {

        }

        /// <summary>
        /// second timepicket close up event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecondTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.SecondDueTextBox.Text = this.SecondTimePicker.Text;
                this.SecondDueTextBox.Focus();
                this.ParentForm.ActiveControl = SecondDueTextBox;
                this.ParentForm.ActiveControl.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// second time picker key press.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecondTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int keyCode = (int)e.KeyChar;
                if (keyCode == 9)
                {
                    SendKeys.Send(SharedFunctions.GetResourceString("ESC"));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void SecondTimePicker_KeyDown(object sender, KeyEventArgs e)
        {

        }

        /// <summary>
        /// payments textbox leave event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaymentsNoTextBox_Leave(object sender, EventArgs e)
        {
            int val = 0;
            int.TryParse(this.PaymentsNoTextBox.Text, out val);
            if (val != null)
            {
                if (val > 255)
                {
                    this.PaymentsNoTextBox.Text = "0";
                }
            }
        }

        /// <summary>
        /// grace period textbox leave event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GracePeriodTextBox_Leave(object sender, EventArgs e)
        {
            int val = 0;
            int.TryParse(this.GracePeriodTextBox.Text, out val);
            if (val != null)
            {
                if (val > 32767)
                {
                    this.GracePeriodTextBox.Text = "0";
                }
            }
        }

        /// <summary>
        /// click event of Parcel Grid button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelGridButton_Click(object sender, EventArgs e)
        {
            FormInfo formInfo;
            formInfo = TerraScanCommon.GetFormInfo(10041);
            formInfo.optionalParameters = new object[1];
            formInfo.optionalParameters[0] = this.activeRecord;
            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
        }

        /// <summary>
        /// Text change event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RollYearTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.rollYear = Convert.ToInt32(this.RollYearTextBox.Text.ToString());
                int.TryParse(this.RollYearTextBox.Text, out this.tempYear);

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
        /// District Name Text changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImpDistrictNameTextBox_TextChanged(object sender, EventArgs e)
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
        /// Validates the distribution items passes true on validation success.
        /// </summary>
        /// <returns>validation result</returns>
        private bool ValidateDistributionItems()
        {
            bool validationResult = true;

            bool validTypeId1 = true;

            bool validTypeId2 = true;

            bool validTypeId3 = true;

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

                        if (validTypeId3)
                        {
                            if (this.DistributionGridView[0, currentRow].Value.ToString() == "5")
                            {
                                validTypeId3 = false;
                            }
                        }
                    }
                }
            }

            validTypeId1 = !validTypeId1;
            validTypeId2 = !validTypeId2;
            validTypeId3 = !validTypeId3;
            return (validationResult && validTypeId1 && validTypeId2 && validTypeId3);
        }

        /// <summary>
        /// Removes the default selection.
        /// </summary>
        private void RemoveDefaultSelection()
        {
            if (this.DistributionGridView.OriginalRowCount == 0)
            {
                this.DistributionGridView.RemoveDefaultSelection = true;
            }
            else
            {
                this.DistributionGridView.RemoveDefaultSelection = false;
            }
        }

        /// <summary>
        /// Grace Flag Selection Changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraceFlagComboBox_SelectedIndexChanged(object sender, EventArgs e)
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
        /// Delq interest Combo selecteion changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelqInterestCalqComboBox_SelectedIndexChanged(object sender, EventArgs e)
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
        /// Interest Method Combobox Selection changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InterestMethodComboBox_SelectedIndexChanged(object sender, EventArgs e)
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
        /// penalty textbox changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PenaltyTextBox_TextChanged(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// market percent format.
        /// </summary>
        /// <param name="strval"></param>
        /// <returns></returns>
        public static string MarketPercentageFormat(string strval)
        {
            string retval = "";
            decimal outDecimalMarketPercentage;
            if (!String.IsNullOrEmpty(strval))
            {
                strval = strval.Replace("%", string.Empty).ToString();
                if (Decimal.TryParse(strval, out outDecimalMarketPercentage))
                {
                    if (outDecimalMarketPercentage >= 100)
                    {
                        outDecimalMarketPercentage = 0;
                        retval = outDecimalMarketPercentage.ToString("0.00 %");
                    }
                    else
                    {
                        double roundofval = 0;
                        retval = outDecimalMarketPercentage.ToString("###0.00") + "%";
                        var omitPercent = retval.Replace("%", "");
                        retval = omitPercent.ToString();
                        double.TryParse(retval, out roundofval);

                        if (roundofval >= 100)
                        {
                            outDecimalMarketPercentage = 0;
                            retval = outDecimalMarketPercentage.ToString("0.00 %");
                        }
                    }
                }
                else
                {
                    retval = "";
                }
            }
            else
            {
                retval = "";
            }
            return retval;
        }
    }
}
