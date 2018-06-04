//----------------------------------------------------------------------------------
// <copyright file="F29600.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Senior Exemption
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------
// 15 Sep 07	    Ramya.D            Created
// 27 Mar 09        Shanmuga Sundaram.A Modified for CO:5648
// 29 Apr 09        Shanmuga Sundaram.A Modified for CO:6916
// 2  Aug 10        Manoj Kumar         Modified for CO:8049  
// 20 Oct 20        Biju I.G.           To implement CO:8874
//*********************************************************************************/

namespace D24600
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Configuration;
    using System.Windows.Forms;
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;
    using TerraScan.SmartParts;
    using System.Collections;
    using System.Web.Services.Protocols;

    /// <summary>
    /// F29600 class file
    /// </summary>
    public partial class F29600 : BaseSmartPart
    {
        #region variables

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private bool saveChanged;
        private bool CDItxtChanged;


        private bool manualChange=false;

        private decimal manualValue;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private bool formLoad;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private bool setSlice;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int keyId;

        /// <summary>
        /// ownerId Local variable.
        /// </summary>
        private int ownerId;

        /// <summary>
        /// ownerId Local variable.
        /// </summary>
        private int datasetCount;

        /// <summary>
        /// mode Local variable.
        /// </summary>
        private string mode;

        /// <summary>
        /// subTotalIncome
        /// </summary>
        private decimal subTotalIncome = 0.00M;

        /// <summary>
        /// subTotalIncome
        /// </summary>
        private decimal subTotalDeduction = 0.00M;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool saveMode;

        /// <summary>
        /// newMode
        /// </summary>
        private bool autoSelect=false;

        /// <summary>
        /// seniorExemptDataSet
        /// </summary>
        private F29600SeniorExemptData seniorExemptDataSet = new F29600SeniorExemptData();

        /// <summary>
        /// seniorExemptDataSet
        /// </summary>
        private F29600SeniorExemptData seniorExemptDataSet1 = new F29600SeniorExemptData();

        /// <summary>
        /// PartiesOwnerDetailsData
        /// </summary>
        private F15010ExciseAffidavitData ownerDetailDataSet = new F15010ExciseAffidavitData();

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// ownerShipType Local variable.
        /// </summary>
        private string exemptCode = string.Empty;

        /// <summary>
        /// ownerShipType Local variable.
        /// </summary>
        private string ownerShipType = string.Empty;

        /// <summary>
        /// marketNonSRImprovement
        /// </summary>
        private string marketNonSRImprovement = string.Empty;

        /// <summary>
        /// marketNonSRLand
        /// </summary>
        private string marketNonSRLand = string.Empty;

        /// <summary>
        /// marketNonSRCrop
        /// </summary>
        private string marketNonSRCrop = string.Empty;

        /// <summary>
        /// NonSRInstruction
        /// </summary>
        private string NonSRInstruction = string.Empty;

        ///// <summary>
        ///// useNonSRLand
        ///// </summary>
        //private string useNonSRLand = string.Empty;

        ///// <summary>
        ///// useNonSRCrop
        ///// </summary>
        //private string useNonSRCrop = string.Empty;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Instance of F29600 Controller to call the WorkItem
        /// </summary>
        private F29600Controller form29600Controller;

        /// <summary>
        /// typeComboData
        /// </summary>
        private CommonData residencelsComboData = new CommonData();

        /// <summary>
        /// typeComboData
        /// </summary>
        private CommonData propertyIncludesComboData = new CommonData();

        /// <summary>
        /// getNeighborhoodHeaderData
        /// </summary>
        private F29600SeniorExemptData.f29600ListSeniorExemptionDataTableRow getSeniorExemptData;

        /// <summary>
        /// instance variable to hold the pageLoadStatus
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// mode Local variable.
        /// </summary>
        private string rollyear;

        /// <summary>
        /// acresValue
        /// </summary>
        private string acresValue;

        #endregion variables

        #region InitializeComponent

        /// <summary>
        /// Initializes instance
        /// </summary>
        public F29600()
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
        public F29600(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.SeniorExemptPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SeniorExemptPictureBox.Height, this.SeniorExemptPictureBox.Width, tabText, red, green, blue);
            this.formLoad = false;
        }

        #endregion InitializeComponent

        #region Eventpublication

        /////// <summary>
        /////// display record id
        /////// </summary>
        ////[EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_DisplayNavigatedRecord, PublicationScope.Global)]
        ////public event EventHandler<DataEventArgs<RecordNavigationEntity>> DisplayNavigatedRecord;

        /////// <summary>
        /////// Declare the event SetActiveRecord        
        /////// </summary> 
        ////[EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord, PublicationScope.WorkItem)]
        ////public event EventHandler<DataEventArgs<int>> SetActiveRecord;

        /////// <summary>
        /////// event publication for getting the form status
        /////// </summary>
        ////[EventPublication(EventTopics.D9001_ShellForm_GetFormStatus, PublicationScope.Global)]
        ////public event EventHandler<DataEventArgs<string>> GetFormStatus;

        /////// <summary>
        /////// event publication for panel link label click
        /////// </summary>
        ////[EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        ////public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /////// <summary>
        /////// Declare the event FormSlice_FormCloseAlert        
        /////// </summary>
        ////[EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        ////public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /////// <summary>
        /////// Get Cancel Button
        /////// </summary>
        ////[EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, PublicationScope.WorkItem)]
        ////public event EventHandler<DataEventArgs<string>> GetCancelButton;

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

        #endregion Eventpublication

        /// <summary>
        /// Enumerator PageStauts
        /// </summary>
        public enum OwnerShipType
        {
            /// <summary>
            /// Leaseforlife = 1.
            /// </summary>
            Leaseforlife = 1,

            /// <summary>
            /// LifeEstate = 2.
            /// </summary>
            LifeEstate = 2,

            /// <summary>
            /// owner = 2
            /// </summary>
            owner = 3,
        }

        #region Properties

        /// <summary>
        /// Gets or sets the F29600 control.
        /// </summary>
        /// <value>The F29600 control.</value>
        [CreateNew]
        public F29600Controller F29600Control
        {
            get { return this.form29600Controller as F29600Controller; }
            set { this.form29600Controller = value; }
        }
        #endregion Properties

        #region EventSubcription

        /// <summary>
        /// Called when [D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            ////this.FillResidenceIsCombo();
            ////this.FillpropertyIncludesCombo();
            this.LoadSeniorExemptionDetails();
            this.FillExemptCodeComboBox();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.saveChanged = false;
            this.OwnerComboBox.Select();
            this.OwnerComboBox.Focus();
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
                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                ////if (this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.Rows.Count > 0)
                if (this.datasetCount > 0 || this.mode == "1")
                {
                    eventArgs.Data.FlagInvalidSliceKey = false;
                }
                else
                {
                    eventArgs.Data.FlagInvalidSliceKey = true;
                    this.setSlice = false;
                }
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
                this.pageLoadStatus = true;
                this.formLoad = false;
                this.keyId = eventArgs.Data.SelectedKeyId;
                this.LoadSeniorExemptionDetails();
                this.formLoad = false;
                this.FillExemptCodeComboBox();
                this.formLoad = true;
                //if (this.autoSelect)
                //{
                //    this.FillExemptCodeComboBox(); 
                //    this.autoSelection();
                //    this.autoSelect = false; 
                //}
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.pageLoadStatus = false;
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
            this.ClearControl();
            this.OwnerRadioButton.Checked = true;
            this.BlockPanelControl(false);
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
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
        /// Called when [D9030_ F9030_ save confirmed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission)
            {
                this.saveMode = true;
                this.Save();
                this.saveChanged = false;
            }
        }

        /////// <summary>
        /////// Called when [D9030_ F9030_ delete slice information].
        /////// </summary>
        /////// <param name="sender">The sender.</param>
        /////// <param name="eventArgs">The event args.</param>
        ////[EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        ////public void OnD9030_F9030_DeleteSliceInformation(object sender, DataEventArgs<int> eventArgs)
        ////{   
        ////}

        /// <summary>
        /// Called when [D9030_ F9030_ reload after save].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_ReloadAfterSave(TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.D9030_F9030_ReloadAfterSave != null)
            {
                this.D9030_F9030_ReloadAfterSave(this, eventArgs);
                this.autoSelect = true;
            }
        }
        #endregion EventSubcription

        #region Methods

        ///<summary>
        /// AutomaticProgramSelection
        /// </summary>
        private void autoSelection()
        {
            decimal disposeIncome;
          
                if (this.DisposableIncomeTextBox.Text.Contains("("))
                {
                    string sas = this.DisposableIncomeTextBox.Text.Replace("$", "").Replace(",", "").Replace("(", "").Replace(")", "").Trim();

                    decimal.TryParse(sas.Trim(), out disposeIncome);
                    DataRow[] codeExempt1 = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable.Select("-"+disposeIncome + ">=IncomeMin AND " +"-"+ disposeIncome + "<=IncomeMax");
                    if (codeExempt1.Length > 0)
                    {
                        this.ExemptionCodeComboBox.Enabled = true;
                        this.ExemptionCodeComboBox.DisplayMember = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable.ExemptionCodeColumn.ToString();
                        this.ExemptionCodeComboBox.ValueMember = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable.ExemptionCodeColumn.ToString();
                        this.ExemptionCodeComboBox.DataSource = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable;
                        this.ExemptionCodeComboBox.Text = codeExempt1[0].ItemArray[0].ToString();
                        this.QualifiedComboBox.SelectedIndex = 1;
                    }
                    else
                    {
                        this.ExemptionCodeComboBox.Enabled = false;
                        this.QualifiedComboBox.SelectedIndex = 0;
                        this.ExemptionCodeComboBox.DataSource = null;
                        this.ExemptionCodeComboBox.Items.Clear();
                        
                    }
                }
                else
                {

                decimal.TryParse(this.DisposableIncomeTextBox.Text.Replace("$", "").Replace(",", "").Trim(), out disposeIncome);
                DataRow[] codeExempt = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable.Select(disposeIncome + ">=IncomeMin AND " + disposeIncome + "<=IncomeMax");
                if (codeExempt.Length > 0)
                {
                    this.ExemptionCodeComboBox.Enabled = true;
                    this.ExemptionCodeComboBox.DisplayMember = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable.ExemptionCodeColumn.ToString();
                    this.ExemptionCodeComboBox.ValueMember = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable.ExemptionCodeColumn.ToString();
                    this.ExemptionCodeComboBox.DataSource = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable;
                    this.ExemptionCodeComboBox.Text = codeExempt[0].ItemArray[0].ToString();
                    this.QualifiedComboBox.SelectedIndex = 1;
                }
                else
                {
                    this.ExemptionCodeComboBox.Enabled = false;
                    this.QualifiedComboBox.SelectedIndex = 0;
                    this.ExemptionCodeComboBox.DataSource = null;
                    this.ExemptionCodeComboBox.Items.Clear();
                    
                }
            }
        }


        ///<summary>
        /// AutomaticProgramSelection
        /// </summary>
        private void autoSelections(decimal Income)
        {
            if (Income < 0)
            {

                DataRow[] codeExempt1 = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable.Select(Income + ">=IncomeMin AND " + Income + "<=IncomeMax");
                if (codeExempt1.Length > 0)
                {
                    this.ExemptionCodeComboBox.Enabled = true;
                    this.ExemptionCodeComboBox.DisplayMember = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable.ExemptionCodeColumn.ToString();
                    this.ExemptionCodeComboBox.ValueMember = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable.ExemptionCodeColumn.ToString();
                    this.ExemptionCodeComboBox.DataSource = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable;
                    this.ExemptionCodeComboBox.Text = codeExempt1[0].ItemArray[0].ToString();
                    this.QualifiedComboBox.SelectedIndex = 1;
                }
                else
                {
                    this.ExemptionCodeComboBox.Enabled = false;
                    this.QualifiedComboBox.SelectedIndex = 0;
                    this.ExemptionCodeComboBox.DataSource = null;
                    this.ExemptionCodeComboBox.Items.Clear();
                    

                }
            }
            else
            {
                DataRow[] codeExempt = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable.Select(Income + ">=IncomeMin AND " + Income + "<=IncomeMax");
                if (codeExempt.Length > 0)
                {
                    this.ExemptionCodeComboBox.Enabled = true;
                    this.ExemptionCodeComboBox.DisplayMember = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable.ExemptionCodeColumn.ToString();
                    this.ExemptionCodeComboBox.ValueMember = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable.ExemptionCodeColumn.ToString();
                    this.ExemptionCodeComboBox.DataSource = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable;
                    this.ExemptionCodeComboBox.Text = codeExempt[0].ItemArray[0].ToString();
                    this.QualifiedComboBox.SelectedIndex = 1;
                }
                else
                {
                    this.ExemptionCodeComboBox.Enabled = false;
                    this.QualifiedComboBox.SelectedIndex = 0;
                    this.ExemptionCodeComboBox.DataSource = null;
                    this.ExemptionCodeComboBox.Items.Clear();
                   

                }
            }

        }
        /// <summary>
        /// FillExemptCodeComboBox
        /// </summary>
        private void FillExemptCodeComboBox()
        {
            F29600SeniorExemptData exemptCodeData = new F29600SeniorExemptData();
            F29600SeniorExemptData.f29600ExemptCodeDataTableRow seniorExemptCodeRow = exemptCodeData.f29600ExemptCodeDataTable.Newf29600ExemptCodeDataTableRow();
            if (!string.IsNullOrEmpty(this.rollyear))
            {
                seniorExemptCodeRow.RollYear = this.rollyear.Trim();
                exemptCodeData.f29600ExemptCodeDataTable.Rows.Add(seniorExemptCodeRow);
                exemptCodeData.f29600ExemptCodeDataTable.AcceptChanges();
                DataSet tempDataSet = new DataSet(SharedFunctions.GetResourceString("Root"));
                tempDataSet.Tables.Add(exemptCodeData.f29600ExemptCodeDataTable.Copy());
                tempDataSet.Tables[0].TableName = SharedFunctions.GetResourceString("Table");
                this.seniorExemptDataSet1 = this.F29600Control.WorkItem.F29600_GetSeniorExemptionCode(tempDataSet.GetXml());
                if (this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable.Rows.Count > 0)
                {
                    this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable.DefaultView.Sort = SharedFunctions.GetResourceString("F29600SortingOrder");
                    this.ExemptionCodeComboBox.DisplayMember = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable.ExemptionCodeColumn.ToString();
                    this.ExemptionCodeComboBox.ValueMember = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable.ExemptionCodeColumn.ToString();
                    this.ExemptionCodeComboBox.DataSource = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable;

                }

                if (!string.IsNullOrEmpty(this.exemptCode))
                {
                    try
                    {

                        DataRow[] combovalue = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable.Select("ExemptionCode = '" + this.exemptCode + "'");
                        if (combovalue.Length > 0)
                        {
                            // this.ExemptionCodeComboBox.Items.Insert(0, this.exemptCode);
                            this.ExemptionCodeComboBox.SelectedValue = this.exemptCode;
                            this.QualifiedComboBox.SelectedIndex = 1; 
                        }
                        else
                        {
                            F29600SeniorExemptData.f29600ListExemptionCodeDataTableRow seniorRow = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable.Newf29600ListExemptionCodeDataTableRow();
                            if (this.exemptCode != "0")
                            {
                                seniorRow.ExemptionCode = this.exemptCode;
                                this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable.Rows.Add(this.exemptCode);
                                this.ExemptionCodeComboBox.DataSource = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable;
                                this.ExemptionCodeComboBox.DisplayMember = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable.ExemptionCodeColumn.ToString();
                                this.ExemptionCodeComboBox.ValueMember = this.seniorExemptDataSet1.f29600ListExemptionCodeDataTable.ExemptionCodeColumn.ToString();
                                //this.QualifiedComboBox.SelectedIndex = 1; 
                                this.ExemptionCodeComboBox.SelectedValue = this.exemptCode;
                                this.ExemptionCodeComboBox.SelectedText = this.exemptCode;
                            }
                            //F15010ExciseAffidavitData.ExciseDeedTypeRow existingDeedRow = this.exciseIndividualtype.ExciseDeedType.NewExciseDeedTypeRow();
                            //existingDeedRow.DeedTypeID = int.MaxValue;
                            //existingDeedRow.DeedType = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.DocumentTypeColumn].ToString();
                            //this.exciseIndividualtype.ExciseDeedType.Rows.Add(existingDeedRow);
                            //this.ExemptionCodeComboBox.Text = this.exemptCode;
                            //this.ExemptionCodeComboBox.SelectedIndex = -1;
                        }
                    }
                    catch (Exception ex)
                    {
                       
                    }

                }
                //else
                //{
                //    this.ExemptionCodeComboBox.Enabled = false;
                //    this.ExemptionCodeComboBox.DataSource = null;
                //    //this.QualifiedComboBox.SelectedIndex = 1; 
                //}


                if (this.QualifiedComboBox.SelectedIndex == 0)
                {
                    this.ExemptionCodeComboBox.Enabled = false;
                    this.ExemptionCodeComboBox.DataSource = null;
                }
                else
                {
                    if (this.PermissionFiled.editPermission)
                    {
                        this.ExemptionCodeComboBox.Enabled = true;
                    }
                    else
                    {
                        this.ExemptionCodeComboBox.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// FillResidenceIsCombo
        /// </summary>
        private void FillResidenceIsCombo()
        {
            Hashtable datas = new Hashtable();
            datas.Add(SharedFunctions.GetResourceString("F29600SingleDwelling"), 1);
            datas.Add(SharedFunctions.GetResourceString("F29600MultiDwelling"), 2);
            datas.Add(SharedFunctions.GetResourceString("CooperativeHousing"), 3);
            datas.Add(SharedFunctions.GetResourceString("MobileHome"), 4);
            this.residencelsComboData.LoadGeneralComboData(datas);
            this.ResidenceComboBox.DisplayMember = this.residencelsComboData.ComboBoxDataTable.KeyNameColumn.ColumnName;
            this.ResidenceComboBox.ValueMember = this.residencelsComboData.ComboBoxDataTable.KeyIdColumn.ColumnName;
            this.ResidenceComboBox.DataSource = this.residencelsComboData.ComboBoxDataTable;
            this.residencelsComboData.ComboBoxDataTable.DefaultView.Sort = SharedFunctions.GetResourceString("KeyId");
            this.ResidenceComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// FillResidenceIsCombo
        /// </summary>
        private void FillpropertyIncludesCombo()
        {
            Hashtable datas = new Hashtable();
            datas.Add(SharedFunctions.GetResourceString("UpToOneAcre"), 1);
            datas.Add(SharedFunctions.GetResourceString("MoreThanOneAcre"), 2);
            datas.Add(SharedFunctions.GetResourceString("MoreThanOneResidence"), 3);
            this.propertyIncludesComboData.LoadGeneralComboData(datas);
            this.PropertyIncludesComboBox.DisplayMember = this.propertyIncludesComboData.ComboBoxDataTable.KeyNameColumn.ColumnName;
            this.PropertyIncludesComboBox.ValueMember = this.propertyIncludesComboData.ComboBoxDataTable.KeyIdColumn.ColumnName;
            this.PropertyIncludesComboBox.DataSource = this.propertyIncludesComboData.ComboBoxDataTable;
            this.propertyIncludesComboData.ComboBoxDataTable.DefaultView.Sort = SharedFunctions.GetResourceString("KeyId");
            this.PropertyIncludesComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// To fill QualifiedCombobox
        /// </summary>
        private void FillQualifiedCombobox()
        {
            this.QualifiedComboBox.Items.Insert(0, SharedFunctions.GetResourceString("No"));
            this.QualifiedComboBox.Items.Insert(1, SharedFunctions.GetResourceString("Yes"));
            this.QualifiedComboBox.SelectedIndex = 0;
        }
        /// <summary>
        /// Added by Biju on 20-Oct-2010 to implement #8874
        /// To fill Continuation combo box
        /// </summary>
        private void FillContinuationCombobox()
        {
            this.ContinuationComboBox.Items.Insert(0, SharedFunctions.GetResourceString("No"));
            this.ContinuationComboBox.Items.Insert(1, SharedFunctions.GetResourceString("Yes"));
            this.ContinuationComboBox.SelectedIndex = 0;
        }
        //removed due to co:8049

        ///// <summary>
        ///// To Fill ActiveComboBox
        ///// </summary>
        //private void FillActiveComboBox()
        //{
        //    this.ActiveComboBox.Items.Insert(0, SharedFunctions.GetResourceString("No"));
        //    this.ActiveComboBox.Items.Insert(1, SharedFunctions.GetResourceString("Yes"));
        //    this.ActiveComboBox.SelectedIndex = 1;
        //}

        /// <summary>
        /// Change ResidenceIsCombo
        /// </summary>
        private void ChangeResidenceIsCombo()
        {
            if (this.ResidenceComboBox.SelectedValue != null)
            {
                if (this.ConvertIntToString(this.ResidenceComboBox.SelectedValue.ToString()) != 4)
                {
                    this.MHYearTextBox.Enabled = false;
                    this.MHYearTextBox.Text = string.Empty;
                    this.Yearlabel.ForeColor = Color.FromArgb(115, 115, 115);
                    this.MobileHomeModelTextBox.Enabled = false;
                    this.MobileHomeModelTextBox.Text = string.Empty;
                    this.MobileHomeModellabel.ForeColor = Color.FromArgb(115, 115, 115);
                    this.MobileHomeLocationTextBox.Enabled = false;
                    this.MobileHomeLocationTextBox.Text = string.Empty;
                    this.MobileHomeLocationlabel.ForeColor = Color.FromArgb(115, 115, 115);
                    this.MobileHomeMakeTextBox.Enabled = false;
                    this.MobileHomeMakeTextBox.Text = string.Empty;
                    this.MobileHomeMakellabel.ForeColor = Color.FromArgb(115, 115, 115);
                }
                else
                {
                    this.MHYearTextBox.Enabled = true;
                    if (this.MHYearTextBox.Text == "")
                    {
                        this.MHYearTextBox.Text = string.Empty;
                    }

                    this.Yearlabel.ForeColor = Color.FromArgb(51, 51, 153);
                    this.MobileHomeModelTextBox.Enabled = true;
                    this.MobileHomeModellabel.ForeColor = Color.FromArgb(51, 51, 153);
                    this.MobileHomeLocationTextBox.Enabled = true;
                    this.MobileHomeLocationlabel.ForeColor = Color.FromArgb(51, 51, 153);
                    this.MobileHomeMakeTextBox.Enabled = true;
                    this.MobileHomeMakellabel.ForeColor = Color.FromArgb(51, 51, 153);
                }
            }
            else
            {
                this.MHYearTextBox.Enabled = true;
                this.Yearlabel.ForeColor = Color.FromArgb(51, 51, 153);
                this.MobileHomeModelTextBox.Enabled = true;
                this.MobileHomeModellabel.ForeColor = Color.FromArgb(51, 51, 153);
                this.MobileHomeLocationTextBox.Enabled = true;
                this.MobileHomeLocationlabel.ForeColor = Color.FromArgb(51, 51, 153);
                this.MobileHomeMakeTextBox.Enabled = true;
                this.MobileHomeMakellabel.ForeColor = Color.FromArgb(51, 51, 153);
            }
        }

        /// <summary>
        /// Change PropertyIncludesCombo
        /// </summary>
        private void ChangePropertyIncludesCombo()
        {
            if (this.PropertyIncludesComboBox.SelectedIndex != 1)
            {
                this.AcresTextBox.Enabled = false;
                this.AcresTextBox.Text = string.Empty;
                this.Acreslabel.ForeColor = Color.FromArgb(115, 115, 115);
            }
            else
            {
                if (this.acresValue != null)
                {
                    this.AcresTextBox.Text = this.acresValue;
                }

                this.AcresTextBox.Enabled = true;
                this.Acreslabel.ForeColor = Color.FromArgb(51, 51, 153);
            }
        }

        /// <summary>
        /// LoadSeniorExemptionDetails
        /// </summary>
        private void LoadSeniorExemptionDetails()
        {
            this.OwnerComboBox.Focus();
            this.SetNullToDataSet();
            this.seniorExemptDataSet = this.F29600Control.WorkItem.F29600_GetSeniorExemptionDetails(this.keyId, TerraScanCommon.UserId);
            this.mode = this.seniorExemptDataSet.f29600GetSeniorExemptionMode.Rows[0][this.seniorExemptDataSet.f29600GetSeniorExemptionMode.ModeColumn].ToString();
            this.rollyear = this.seniorExemptDataSet.f29600_pcget_SeniorExemptionRollYear.Rows[0][this.seniorExemptDataSet.f29600_pcget_SeniorExemptionRollYear.RollYearColumn].ToString();
            //// To Load the OwnerCombo
            if (this.seniorExemptDataSet.f29600GetSeniorExemptionOwnerComboDataTable.Rows.Count > 0)
            {
                this.OwnerComboBox.DataSource = this.seniorExemptDataSet.f29600GetSeniorExemptionOwnerComboDataTable;
                this.OwnerComboBox.DisplayMember = this.seniorExemptDataSet.f29600GetSeniorExemptionOwnerComboDataTable.OwnerNameColumn.ColumnName;
                this.OwnerComboBox.ValueMember = this.seniorExemptDataSet.f29600GetSeniorExemptionOwnerComboDataTable.OwnerIDColumn.ColumnName;
            }

            if (this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.Rows.Count > 0 && (this.seniorExemptDataSet.f29600GetSeniorExemptionMode.Rows[0][this.seniorExemptDataSet.f29600GetSeniorExemptionMode.ModeColumn].ToString() == "2"))
            {
                this.saveMode = true;
                this.datasetCount = this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.Rows.Count;
                this.getSeniorExemptData = (F29600SeniorExemptData.f29600ListSeniorExemptionDataTableRow)this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.Rows[0];
                this.OwnerComboBox.Text = this.getSeniorExemptData.AppliedOwner.ToString().Trim();
                this.OwnerComboBox.SelectedValue = this.ConvertIntToString(this.getSeniorExemptData.OwnerID.ToString());
                this.OwnerAddresslabel.Text = this.getSeniorExemptData.OwnerAddress.ToString();
                this.CityZiplabel.Text = this.getSeniorExemptData.CityStateZip.ToString();
                this.IsCertifyAgecheckBox.Checked = this.getSeniorExemptData.IsCertifyAge;
                this.IsCertifyDisabilitycheckBox.Checked = this.getSeniorExemptData.IsCertifyDisability;
                this.IsCertifyVeterancheckBox.Checked = this.getSeniorExemptData.IsCertifyVeteran;
                this.IsCertifySpousecheckBox.Checked = this.getSeniorExemptData.IsCertifySpouse;
                if (!this.getSeniorExemptData.IsApplicantBdayDateNull())
                {
                    this.ApplicantBDayTextBox.Text = this.getSeniorExemptData.ApplicantBdayDate.ToString();
                }
                else
                {
                    this.ApplicantBDayTextBox.Text = string.Empty;
                }

                if (!this.getSeniorExemptData.IsSpouseBdayDateNull())
                {
                    this.SpouseBdayTextBox.Text = this.getSeniorExemptData.SpouseBdayDate.ToString();
                }
                else
                {
                    this.SpouseBdayTextBox.Text = string.Empty;
                }

                if (!this.getSeniorExemptData.IsPropertyPurchasedDateNull())
                {
                    this.PropertyTextBox.Text = this.getSeniorExemptData.PropertyPurchasedDate.ToString();
                }
                else
                {
                    this.PropertyTextBox.Text = string.Empty;
                }
                if (!this.getSeniorExemptData.IsPropertyOccupiedDateNull())
                {
                    this.PropertyOccupiedTextBox.Text = this.getSeniorExemptData.PropertyOccupiedDate.ToString();
                }
                else
                {
                    this.PropertyOccupiedTextBox.Text = string.Empty;
                }
                this.ownerShipType = this.getSeniorExemptData.OwnershipType.ToString();
                if (this.getSeniorExemptData.Residencels.ToString() != "0")
                {
                    this.ResidenceComboBox.SelectedValue = this.getSeniorExemptData.Residencels;
                }
                else
                {
                    this.ResidenceComboBox.SelectedIndex = 1;
                }

                if (this.getSeniorExemptData.MHYear.ToString() == "0")
                {
                    this.MHYearTextBox.Text = string.Empty;
                }
                else
                {
                    this.MHYearTextBox.Text = this.getSeniorExemptData.MHYear.ToString();
                }

                this.MobileHomeMakeTextBox.Text = this.getSeniorExemptData.MHMake;
                this.MobileHomeModelTextBox.Text = this.getSeniorExemptData.MHModel.ToString();
                this.MobileHomeLocationTextBox.Text = this.getSeniorExemptData.MHLocation.ToString();
                ////To change Year,MHL and MHM lable color 
                this.ChangeResidenceIsCombo();
                this.ParcelNoTextBox.Text = this.getSeniorExemptData.ParcelNumber.ToString();
                this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.SitusColumn.AllowDBNull = true;
                this.SitusTextBox.Text = this.getSeniorExemptData.Situs.ToString();
                this.LegalTextBox.Text = this.getSeniorExemptData.Legal.ToString();
                if (this.getSeniorExemptData.PropertyIncludes.ToString() != "0")
                {
                    this.PropertyIncludesComboBox.SelectedValue = this.getSeniorExemptData.PropertyIncludes;
                }
                else
                {
                    this.PropertyIncludesComboBox.SelectedIndex = 1;
                }

                ////To change Acres  lable Color 
                this.ChangePropertyIncludesCombo();
                this.AcresTextBox.Text = this.getSeniorExemptData.Acres.ToString();
                this.acresValue = this.getSeniorExemptData.Acres.ToString();
                this.GrossIncomeTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.AdjustedGross.ToString()).ToString("#,##0").Trim();
                this.MilitaryandVeteransTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.MilitaryPay.ToString()).ToString("#,##0").Trim();
                this.OtherIncomeTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.OtherIncome.ToString()).ToString("#,##0").Trim();
                this.LessPrescriptionDrugsTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.LessPrescription.ToString()).ToString("#,##0").Trim();
                this.CapitalGainsTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.CapitalGains.ToString()).ToString("#,##0").Trim();
                this.DividendsandInterestTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.Dividends.ToString()).ToString("#,##0").Trim();
                this.LessInsurancePremiumsTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.LessInsurance.ToString()).ToString("#,##0").Trim();
                this.DeductionsTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.Deductions.ToString()).ToString("#,##0").Trim();
                this.TaxableIRATextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.TaxableIRA.ToString()).ToString("#,##0").Trim();
                this.HomeTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.LessHome.ToString()).ToString("#,##0").Trim();
                this.InstructionTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.LessOther.ToString()).ToString("#,##0").Trim();
                this.PensionTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.Pensions.ToString()).ToString("#,##0").Trim();
                //Co:8049

                //if (!this.getSeniorExemptData.IsPensions2Null())
                //{
                    this.PensionAnnuity2TextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.Pensions2.ToString()).ToString("#,##0").Trim();
                //}
                //else
                //{
                //    this.PensionAnnuity2TextBox.Text = string.Empty;
                //}
                this.SocialSecurity2TextBox.Text =Convert.ToDecimal(this.getSeniorExemptData.SocialSecurityDisability2.ToString()).ToString("#,##0").Trim();  
                this.OtherIncome2TextBox.Text =Convert.ToDecimal(this.getSeniorExemptData.OtherIncome2.ToString()).ToString("#,##0").Trim();
                this.OtherIncome3TextBox.Text =Convert.ToDecimal (this.getSeniorExemptData.OtherIncome3.ToString()).ToString ("#,##0").Trim();
                this.LessOther2TextBox.Text =Convert.ToDecimal (this.getSeniorExemptData.LessOther2.ToString()).ToString("#,##0").Trim();
               
                
                this.DisabilityTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.SocialSecurityDisability.ToString()).ToString("#,##0").Trim();
                this.HomeCareTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.LessInHome.ToString()).ToString("#,##0").Trim();
                this.DisposableIncomeTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.CombinedDisposable.ToString()).ToString("$ #,##0").Trim();

                //this.GrossIncomeTextBox.Text = this.getSeniorExemptData.AdjustedGross.ToString().Trim();
                //this.MilitaryandVeteransTextBox.Text = this.getSeniorExemptData.MilitaryPay.ToString("#,##0").Trim();
                //this.OtherIncomeTextBox.Text = this.getSeniorExemptData.OtherIncome.ToString("#,##0").Trim();
                //this.LessPrescriptionDrugsTextBox.Text = this.getSeniorExemptData.LessPrescription.ToString("#,##0").Trim();
                //this.CapitalGainsTextBox.Text = this.getSeniorExemptData.CapitalGains.ToString("#,##0").Trim();
                //this.DividendsandInterestTextBox.Text = this.getSeniorExemptData.Dividends.ToString("#,##0").Trim();
                //this.LessInsurancePremiumsTextBox.Text = this.getSeniorExemptData.LessInsurance.ToString("#,##0").Trim();
                //this.DeductionsTextBox.Text = this.getSeniorExemptData.Deductions.ToString("#,##0").Trim();
                //this.TaxableIRATextBox.Text = this.getSeniorExemptData.TaxableIRA.ToString("#,##0").Trim();
                //this.HomeTextBox.Text = this.getSeniorExemptData.LessHome.ToString("#,##0").Trim();
                //this.InstructionTextBox.Text = this.getSeniorExemptData.LessOther.ToString("#,##0").Trim();
                //this.PensionTextBox.Text = this.getSeniorExemptData.Pensions.ToString("#,##0").Trim();
                //this.DisabilityTextBox.Text = this.getSeniorExemptData.SocialSecurityDisability.ToString("#,##0").Trim();
                //this.HomeCareTextBox.Text = this.getSeniorExemptData.LessInHome.ToString("#,##0").Trim();
                //this.DisposableIncomeTextBox.Text = this.getSeniorExemptData.CombinedDisposable.ToString("$ #,##0").Trim();

                if (this.QualifiedComboBox.SelectedIndex >= 0)
                {
                    if (this.getSeniorExemptData.IsQualified)
                    {

                        this.QualifiedComboBox.SelectedIndex = 1;

                    }
                    else
                    {
                        this.QualifiedComboBox.SelectedIndex = 0;
                    }
                }

                if (this.getSeniorExemptData.IsEffectiveDateNull())
                {
                    this.EffectiveDateTextBox.Text = System.DateTime.Now.ToString();
                }
                else
                {
                    this.EffectiveDateTextBox.Text = this.getSeniorExemptData.EffectiveDate.ToString().Trim();
                }
                if (this.getSeniorExemptData.IsCancelDateNull())
                {
                    this.CancelDateTextBox.Text = string.Empty;
                }
                else
                {
                    this.CancelDateTextBox.Text = this.getSeniorExemptData.CancelDate.ToString().Trim();
                }
                this.exemptCode = this.getSeniorExemptData.ExemptionCode.ToString().Trim();
                this.OwnerPercentTextBox.Text  =this.getSeniorExemptData.OwnerPercent.ToString() ;
                if (this.getSeniorExemptData.IsContinuation)
                {
                    ////this.ContinuationTextBox.Text = SharedFunctions.GetResourceString("Yes");
                    ////Added by Biju on 20-Oct-2010 to implement #8874
                    this.ContinuationComboBox.SelectedIndex = 1;
                }
                else
                {
                    ////this.ContinuationTextBox.Text = SharedFunctions.GetResourceString("No");
                    ////Added by Biju on 20-Oct-2010 to implement #8874
                    this.ContinuationComboBox.SelectedIndex = 0;
                }

                this.AdjustedFrozenValuelabel.Text = this.getSeniorExemptData.FrozenLabel.ToString().Trim() + ":";
                this.FinalAssessedValueTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.FrozenValue.ToString()).ToString("$ #,##0").Trim();
                this.NewConstructionTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.NewConstruction.ToString()).ToString("$ #,##0").Trim();
                this.ResultingOwnerValueTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.ResultingMarket.ToString()).ToString("$ #,##0").Trim();
                this.ReductionOfvalueTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.ValueLoss.ToString()).ToString("$ #,##0").Trim();
                this.ResultingTaxableValueTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.ResultingTaxable.ToString()).ToString("$ #,##0").Trim();

                //// Added as per CO#5648 by A.Shanmuga Sundaram on 25th March'09 for sprint 68
                this.MarketNonSRCropTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.MarketCrop.ToString()).ToString("$ #,##0").Trim();
                this.MarketNonSRImprovementTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.MarketImprovement.ToString()).ToString("$ #,##0").Trim();
                this.MarketNonSRLandTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.MarketLand.ToString()).ToString("$ #,##0").Trim();
                //CO:8049
                this.NonSRNewValueTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.NonSRNewConstruction.ToString()).ToString("$ #,##0").Trim();       



                //removed for CO:8049

                //this.UseNonSRCropTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.UseCrop.ToString()).ToString("$ #,##0").Trim();
                //this.UseNonSRImprovementTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.UseImprovement.ToString()).ToString("$ #,##0").Trim();
                //this.UseNonSRLandTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.UseLand.ToString()).ToString("$ #,##0").Trim();
                this.OriginalFrozenValueTextBox.Text = Convert.ToDecimal(this.getSeniorExemptData.OriginalFrozenValue.ToString()).ToString("$ #,##0").Trim();

                this.marketNonSRCrop = this.MarketNonSRCropTextBox.Text.Replace("$", "");
                this.marketNonSRLand = this.MarketNonSRLandTextBox.Text.Replace("$", "");
                this.marketNonSRImprovement = this.MarketNonSRImprovementTextBox.Text.Replace("$", "");
                this.NonSRInstruction =this.NonSRNewValueTextBox.Text.Replace("$","");    
                
                
                
                //removed for CO:8049




                
                //this.useNonSRCrop = this.UseNonSRCropTextBox.Text.Replace("$", "");
                //this.useNonSRLand = this.UseNonSRLandTextBox.Text.Replace("$", "");
                //this.useNonSRImprovement = this.UseNonSRImprovementTextBox.Text.Replace("$", "");

                //if (this.ActiveComboBox.SelectedIndex >= 0)
                //{
                //    if (this.getSeniorExemptData.IsActive)
                //    {
                //        this.ActiveComboBox.SelectedIndex = 1;
                //    }
                //    else
                //    {
                //        this.ActiveComboBox.SelectedIndex = 0;
                //    }
                //}

                //// To Calculate SubTotal and To Select OwnershipType
                this.CalculateSubTotalIncome();
                this.CalculateSubtotalDeduction();
                this.SelectOwnershipType();
                this.BlockPanelControl(true);
                this.BlockTextBoxControl(true);
                this.ChangeResidenceIsCombo();
                this.ChangePropertyIncludesCombo();
                this.DisposableIncomeTextBox.Text = (this.subTotalIncome - this.subTotalDeduction).ToString("$ #,##0");
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.QualifiedCheckBoxStatus();
                this.ControlKeypress(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                if (!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit)
                {
                    this.ExemptionCodeComboBox.Enabled = false;
                    this.ContinuationComboBox.Enabled = false;
                }

                ////Coding commented for the issue 4653 by malliga
                //this.ControlKeypress(!this.getSeniorExemptData.IsActive);
            }
            else if (this.seniorExemptDataSet.f29600GetSeniorExemptionMode.Rows[0][this.seniorExemptDataSet.f29600GetSeniorExemptionMode.ModeColumn].ToString() == "1")
            {
                if (this.PermissionFiled.newPermission || this.formMasterPermissionEdit)
                {
                    this.OwnerComboBox.Focus();
                    this.BlockPanelControl(true);
                    this.BlockTextBoxControl(true);
                    this.ClearControl();
                    //// this.FillResidenceIsCombo();
                    ////this.FillpropertyIncludesCombo();
                    ////this.FillQualifiedCombobox();
                    ////this.FillActiveComboBox();
                    this.ChangeResidenceIsCombo();
                    this.ChangePropertyIncludesCombo();
                    this.QualifiedCheckBoxStatus();
                    this.OwnerRadioButton.Checked = true;
                    this.saveMode = true;
                }
                else
                {
                    this.ClearControl();
                    this.BlockPanelControl(false);
                    this.ExemptionCodeComboBox.BackColor = Color.White;
                    this.ExemptionCodepanel.BackColor = Color.White;
                }

                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
            else
            {
                if (!this.setSlice)
                {
                    this.ClearControl();
                    this.OwnerRadioButton.Checked = true;
                    this.BlockPanelControl(false);
                    this.LeaseLifeRadioButton.Checked = false;
                    this.ExemptionCodepanel.BackColor = Color.White;
                    this.ExemptionCodeComboBox.BackColor = Color.White;
                    //this.ActiveComboBox.BackColor = Color.White;
                    //this.Activepanel.BackColor = Color.White;
                    this.setSlice = true;
                }
                else
                {
                    //// Coding added for the issue 4497  by maliga on 29/3/2009
                    this.ClearControl();
                    this.OwnerRadioButton.Checked = true;
                    this.BlockPanelControl(false);
                    this.BlockTextBoxControl(false);
                    this.LeaseLifeRadioButton.Checked = false;
                    this.ExemptionCodepanel.BackColor = Color.White;
                    this.ExemptionCodeComboBox.BackColor = Color.White;
                    //removed for cO:8049

                    //this.ActiveComboBox.BackColor = Color.White;
                    //this.Activepanel.BackColor = Color.White;
                    ////// End here for 4497
                }
            }

            this.formLoad = true;
        }

        /// <summary>
        /// QualifiedCheckBoxStatus
        /// </summary>
        private void QualifiedCheckBoxStatus()
        {
            if (this.QualifiedComboBox.SelectedIndex == 1)
            {
                this.DisposableIncomeTextBox.ForeColor = Color.FromArgb(192, 0, 0);
                this.QualifiedComboBox.ForeColor = Color.FromArgb(192, 0, 0);
                this.ExemptionCodeComboBox.Enabled = true;
                ////Added by Biju on 20-Oct-2010 to implement #8874
                ////this.ContinuationTextBox.ForeColor = Color.FromArgb(0, 0, 0);
                ////this.ContinuationTextBox.Enabled = true;
                this.ContinuationComboBox.ForeColor = Color.FromArgb(0, 0, 0);
                this.ContinuationComboBox.Enabled = true;
            }
            else
            {
                this.DisposableIncomeTextBox.ForeColor = Color.FromArgb(0, 0, 0);
                this.QualifiedComboBox.ForeColor = Color.FromArgb(0, 0, 0);
                this.ExemptionCodeComboBox.Enabled = false;
                ////Added by Biju on 20-Oct-2010 to implement #8874
                ////this.ContinuationTextBox.Enabled = false;
                this.ContinuationComboBox.Enabled = false;
                
            }
        }

        /// <summary>
        /// ConvertIntToString
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>int</returns>
        private int ConvertIntToString(string value)
        {
            int outValue = 0;
            if (!string.IsNullOrEmpty(value))
            {
                int.TryParse(value, out outValue);
            }

            return outValue;
        }

        private void CombinedDisposableIncome()
        {
            decimal CDI=this.DisposableIncomeTextBox.DecimalTextBoxValue; 
            this.CalculateSubtotalDeduction();
            this.CalculateSubTotalIncome();
            this.DisposableIncomeTextBox.Text = (this.subTotalIncome - this.subTotalDeduction).ToString("$ #,##0");
            if (!this.DisposableIncomeTextBox.DecimalTextBoxValue.Equals(CDI))
            {
                this.autoSelections(this.DisposableIncomeTextBox.DecimalTextBoxValue);
            }
            this.CDItxtChanged = false;
        }

        /// <summary>
        /// CalculateSubTotalIncome
        /// </summary>
        private void CalculateSubTotalIncome()
        {
            decimal adjustedGross = 0.00M;
            decimal capitalGains = 0.00M;
            decimal deduction = 0.00M;
            decimal pensions = 0.00M;
            decimal military = 0.00M;
            decimal dividence = 0.00M;
            decimal taxableIRA = 0.00M;
            decimal other = 0.00M;
            decimal disability = 0.00M;
            //CO:8049
            decimal pensions2 = 0.00M;
            decimal Other2 = 0.00M;
            decimal Other3 = 0.00M;
            decimal disability2 = 0.00M;

            decimal.TryParse(this.GrossIncomeTextBox.Text.Trim(), out adjustedGross);
            decimal.TryParse(this.CapitalGainsTextBox.Text.Trim(), out capitalGains);
            decimal.TryParse(this.DeductionsTextBox.Text.Trim(), out deduction);
            decimal.TryParse(this.PensionTextBox.Text.Trim(), out pensions);
            //CO:8049
            decimal.TryParse(this.PensionAnnuity2TextBox.Text.Trim(), out pensions2);
            decimal.TryParse(this.SocialSecurity2TextBox.Text.Trim(), out disability2);
            decimal.TryParse(this.OtherIncome2TextBox.Text.Trim(), out Other2);
            decimal.TryParse(this.OtherIncome3TextBox.Text.Trim(), out Other3);
    
           
             
            decimal.TryParse(this.MilitaryandVeteransTextBox.Text.Trim(), out military);
            decimal.TryParse(this.DividendsandInterestTextBox.Text.Trim(), out dividence);
            decimal.TryParse(this.TaxableIRATextBox.Text.Trim(), out taxableIRA);
            decimal.TryParse(this.OtherIncomeTextBox.Text.Trim(), out other);
            decimal.TryParse(this.DisabilityTextBox.Text.Trim(), out disability);

            this.subTotalIncome = adjustedGross + capitalGains + deduction + pensions +pensions2+disability2 +Other2+Other3 + military + dividence + taxableIRA + other + disability;
            this.SubtotalIncomeTextBox.Text = Convert.ToDecimal(this.subTotalIncome.ToString()).ToString("#,##0");
        }

        /// <summary>
        /// To Calculate SubtotalDeduction
        /// </summary>
        private void CalculateSubtotalDeduction()
        {
            decimal lessHome = 0.00M;
            decimal lessInHome = 0.00M;
            decimal lessPresciption = 0.00M;
            decimal lessInsurance = 0.00M;
            decimal lessOther = 0.00M;
            //CO:8049
            decimal lessOther2 = 0.00M;
            decimal.TryParse(this.LessOther2TextBox.Text.Trim(), out lessOther2);    
           
            decimal.TryParse(this.HomeTextBox.Text.Trim(), out lessHome);
            decimal.TryParse(this.HomeCareTextBox.Text.Trim(), out lessInHome);
            decimal.TryParse(this.LessPrescriptionDrugsTextBox.Text.Trim(), out lessPresciption);
            decimal.TryParse(this.LessInsurancePremiumsTextBox.Text.Trim(), out lessInsurance);
            decimal.TryParse(this.InstructionTextBox.Text.Trim(), out lessOther);
            this.subTotalDeduction = lessHome + lessInHome + lessPresciption + lessInsurance + lessOther+lessOther2;
            this.SubtotalDeductionTextBox.Text = Convert.ToDecimal(this.subTotalDeduction.ToString()).ToString("#,##0");
            this.SubtotalDeductionTextBox.ForeColor = Color.FromArgb(115, 115, 115);
        }

        /// <summary>
        /// SelectOwnershipType
        /// </summary>
        private void SelectOwnershipType()
        {
            if (!string.IsNullOrEmpty(this.ownerShipType))
            {
                if (this.ConvertIntToString(this.ownerShipType) == (int)OwnerShipType.Leaseforlife)
                {
                    this.LeaseLifeRadioButton.Checked = true;
                }
                else if (this.ConvertIntToString(this.ownerShipType) == (int)OwnerShipType.LifeEstate)
                {
                    this.LifeEstateRadioButton.Checked = true;
                }
                else
                {
                    this.OwnerRadioButton.Checked = true;
                }
            }
        }

        /// <summary>
        /// To ClearControl
        /// </summary>
        private void ClearControl()
        {
            this.OwnerAddresslabel.Text = string.Empty;
            this.CityZiplabel.Text = string.Empty;
            this.IsCertifyAgecheckBox.Checked = false;
            this.IsCertifyDisabilitycheckBox.Checked = false;
            this.IsCertifyVeterancheckBox.Checked = false;
            this.IsCertifySpousecheckBox.Checked = false;
            this.ApplicantBDayTextBox.Text = string.Empty;
            this.SpouseBdayTextBox.Text = string.Empty;
            this.PropertyTextBox.Text = string.Empty;
            this.PropertyOccupiedTextBox.Text = string.Empty;
            this.LeaseLifeRadioButton.Checked = false;
            this.LifeEstateRadioButton.Checked = false;

            this.ExemptionCodeComboBox.DataSource = null;
            if (this.mode == "3" || (this.mode == "1" && !this.PermissionFiled.newPermission))
            {
                this.ResidenceComboBox.DataSource = null;
                this.PropertyIncludesComboBox.DataSource = null;
                this.QualifiedComboBox.Items.Clear();
                //this.ActiveComboBox.Items.Clear();
            }
            ////Added by Biju on 20-Oct-2010 to implement #8874
            this.ContinuationComboBox.Items.Clear();
            this.MHYearTextBox.Text = string.Empty;
            this.MobileHomeMakeTextBox.Text = string.Empty;
            this.MobileHomeModelTextBox.Text = string.Empty;
            this.MobileHomeLocationTextBox.Text = string.Empty;
            this.ParcelNoTextBox.Text = string.Empty;
            this.SitusTextBox.Text = string.Empty;
            this.LegalTextBox.Text = string.Empty;
            this.AcresTextBox.Text = string.Empty;
            this.GrossIncomeTextBox.Text = string.Empty;
            this.MilitaryandVeteransTextBox.Text = string.Empty;
            this.OtherIncomeTextBox.Text = string.Empty;
            this.LessPrescriptionDrugsTextBox.Text = string.Empty;
            this.CapitalGainsTextBox.Text = string.Empty;
            this.DividendsandInterestTextBox.Text = string.Empty;
            this.SubtotalIncomeTextBox.Text = string.Empty;
            this.LessInsurancePremiumsTextBox.Text = string.Empty;
            this.DeductionsTextBox.Text = string.Empty;
            this.TaxableIRATextBox.Text = string.Empty;
            this.HomeTextBox.Text = string.Empty;
            this.InstructionTextBox.Text = string.Empty;
            this.PensionTextBox.Text = string.Empty;
            //CO:8049
            this.PensionAnnuity2TextBox.Text = string.Empty;
            this.SocialSecurity2TextBox.Text = string.Empty;
            this.OtherIncome2TextBox.Text = string.Empty;
            this.OtherIncome3TextBox.Text = string.Empty;
            this.LessOther2TextBox.Text = string.Empty;  
 
 
            this.DisabilityTextBox.Text = string.Empty;
            this.HomeCareTextBox.Text = string.Empty;
            this.SubtotalDeductionTextBox.Text = string.Empty;
            this.DisposableIncomeTextBox.Text = string.Empty;
            this.EffectiveDateTextBox.Text = string.Empty;
            this.CancelDateTextBox.Text = string.Empty;
            this.OwnerPercentTextBox.Text = string.Empty;
            ////this.ContinuationTextBox.Text = string.Empty;
            this.FinalAssessedValueTextBox.Text = string.Empty;
            this.NewConstructionTextBox.Text = string.Empty;
            this.ResultingOwnerValueTextBox.Text = string.Empty;
            this.ReductionOfvalueTextBox.Text = string.Empty;
            this.ResultingTaxableValueTextBox.Text = string.Empty;
            //// Added as per CO#5648 by A.Shanmuga Sundaram on 25th March'09 for sprint 68
            this.MarketNonSRCropTextBox.Text = string.Empty;
            this.MarketNonSRImprovementTextBox.Text = string.Empty;
            this.MarketNonSRLandTextBox.Text = string.Empty;
            this.NonSRNewValueTextBox.Text = string.Empty;  
            //this.UseNonSRCropTextBox.Text = string.Empty;
            //this.UseNonSRImprovementTextBox.Text = string.Empty;
            //this.UseNonSRLandTextBox.Text = string.Empty;
            this.OwnerComboBox.SelectedIndex = -1;
            this.OriginalFrozenValueTextBox.Text = string.Empty;
        }

        /// <summary>
        /// ControlKeypress
        /// </summary>
        /// <param name="lockKey">lockKey</param>
        private void ControlKeypress(bool lockKey)
        {
            this.ApplicantBDayTextBox.LockKeyPress = lockKey;
            this.SpouseBdayTextBox.LockKeyPress = lockKey;
            this.PropertyTextBox.LockKeyPress = lockKey;
            this.PropertyOccupiedTextBox.LockKeyPress = lockKey;
            this.MHYearTextBox.LockKeyPress = lockKey;
            this.MobileHomeMakeTextBox.LockKeyPress = lockKey;
            this.MobileHomeModelTextBox.LockKeyPress = lockKey;
            this.MobileHomeLocationTextBox.LockKeyPress = lockKey;
            this.AcresTextBox.LockKeyPress = lockKey;
            this.GrossIncomeTextBox.LockKeyPress = lockKey;
            this.MilitaryandVeteransTextBox.LockKeyPress = lockKey;
            this.OtherIncomeTextBox.LockKeyPress = lockKey;
            this.LessPrescriptionDrugsTextBox.LockKeyPress = lockKey;
            this.CapitalGainsTextBox.LockKeyPress = lockKey;
            this.DividendsandInterestTextBox.LockKeyPress = lockKey;
            this.LessInsurancePremiumsTextBox.LockKeyPress = lockKey;
            this.DeductionsTextBox.LockKeyPress = lockKey;
            this.TaxableIRATextBox.LockKeyPress = lockKey;
            this.HomeTextBox.LockKeyPress = lockKey;
            this.InstructionTextBox.LockKeyPress = lockKey;
            this.PensionTextBox.LockKeyPress = lockKey;
            //CO:8049
            this.PensionAnnuity2TextBox.LockKeyPress = lockKey;
            this.SocialSecurity2TextBox.LockKeyPress = lockKey;
            this.OtherIncome2TextBox.LockKeyPress = lockKey;
            this.OtherIncome3TextBox.LockKeyPress = lockKey;
            this.LessOther2TextBox.LockKeyPress = lockKey;
            this.NonSRNewValueTextBox.LockKeyPress = lockKey;   
 
            this.DisabilityTextBox.LockKeyPress = lockKey;
            this.HomeCareTextBox.LockKeyPress = lockKey;
            this.EffectiveDateTextBox.LockKeyPress = lockKey;
            this.CancelDateTextBox.LockKeyPress = lockKey;
            //// Added as per CO#5648 by A.Shanmuga Sundaram on 25th March'09 for sprint 68
            this.MarketNonSRCropTextBox.LockKeyPress = lockKey;
            this.MarketNonSRImprovementTextBox.LockKeyPress = lockKey;
            this.MarketNonSRLandTextBox.LockKeyPress = lockKey;
            //this.UseNonSRCropTextBox.LockKeyPress = lockKey;
            //this.UseNonSRImprovementTextBox.LockKeyPress = lockKey;
            //this.UseNonSRLandTextBox.LockKeyPress = lockKey;
            this.OriginalFrozenValueTextBox.LockKeyPress = lockKey;

            this.OwnerComboBox.Enabled = !lockKey;
            this.IsCertifyAgecheckBox.Enabled = !lockKey;
            this.IsCertifyDisabilitycheckBox.Enabled = !lockKey;
            this.IsCertifyVeterancheckBox.Enabled = !lockKey;
            this.IsCertifySpousecheckBox.Enabled = !lockKey;
            this.ApplicantDatePict.Enabled = !lockKey;
            this.SpouseDatePict.Enabled = !lockKey;
            this.SpouseDatePict.Enabled = !lockKey;
            this.PropertyDatePict.Enabled = !lockKey;
            this.PropertyOccupiedDatepict.Enabled = !lockKey;
            this.LeaseLifeRadioButton.Enabled = !lockKey;
            this.LifeEstateRadioButton.Enabled = !lockKey;
            this.OwnerRadioButton.Enabled = !lockKey;
            this.ResidenceComboBox.Enabled = !lockKey;
            this.PropertyIncludesComboBox.Enabled = !lockKey;
            this.QualifiedComboBox.Enabled = !lockKey;
            this.EffectiveDatePict.Enabled = !lockKey;
            this.CancelDateButton.Enabled = !lockKey;
            //this.ActiveComboBox.Enabled = !lockKey;
        }

        /////// <summary>
        /////// BlockControl
        /////// </summary>
        /////// <param name="enable">enable</param>
        ////private void BlockControl(bool enable)
        ////{
        ////}

        /// <summary>
        /// BlockTextBoxControl
        /// </summary>
        /// <param name="lockKey">lockKey</param>
        private void BlockTextBoxControl(bool lockKey)
        {
            this.ApplicantBDayTextBox.Enabled = lockKey;
            this.SpouseBdayTextBox.Enabled = lockKey;
            this.PropertyTextBox.Enabled = lockKey;
            this.PropertyOccupiedTextBox.Enabled = lockKey;
            this.MHYearTextBox.Enabled = lockKey;
            this.MobileHomeMakeTextBox.Enabled = lockKey;
            this.MobileHomeModelTextBox.Enabled = lockKey;
            this.MobileHomeLocationTextBox.Enabled = lockKey;
            this.ParcelNoTextBox.Enabled = lockKey;
            this.SitusTextBox.Enabled = lockKey;
            this.LegalTextBox.Enabled = lockKey;
            this.AcresTextBox.Enabled = lockKey;
            this.GrossIncomeTextBox.Enabled = lockKey;
            this.MilitaryandVeteransTextBox.Enabled = lockKey;
            this.OtherIncomeTextBox.Enabled = lockKey;
            this.LessPrescriptionDrugsTextBox.Enabled = lockKey;
            this.CapitalGainsTextBox.Enabled = lockKey;
            this.DisabilityTextBox.Enabled = lockKey;
            this.LessInsurancePremiumsTextBox.Enabled = lockKey;
            this.DeductionsTextBox.Enabled = lockKey;
            this.TaxableIRATextBox.Enabled = lockKey;
            this.HomeTextBox.Enabled = lockKey;
            this.InstructionTextBox.Enabled = lockKey;
            this.PensionTextBox.Enabled = lockKey;
            //CO:8049
            this.PensionAnnuity2TextBox.Enabled = lockKey;
            this.SocialSecurity2TextBox.Enabled = lockKey;
            this.OtherIncome2TextBox.Enabled = lockKey;
            this.OtherIncome3TextBox.Enabled = lockKey;
            this.LessOther2TextBox.Enabled = lockKey;
 

            this.DisabilityTextBox.Enabled = lockKey;
            this.HomeCareTextBox.Enabled = lockKey;
            this.DisposableIncomeTextBox.Enabled = lockKey;
            this.EffectiveDateTextBox.Enabled = lockKey;
            this.CancelDateTextBox.Enabled = lockKey;
            this.OwnerPercentTextBox.Enabled = lockKey;
            ////this.ContinuationTextBox.Enabled = lockKey;
            this.FinalAssessedValueTextBox.Enabled = lockKey;
            this.NewConstructionTextBox.Enabled = lockKey;
            this.ResultingOwnerValueTextBox.Enabled = lockKey;
            this.ReductionOfvalueTextBox.Enabled = lockKey;
            this.ResultingTaxableValueTextBox.Enabled = lockKey;
            this.LeaseLifeRadioButton.Enabled = lockKey;
            this.LifeEstateRadioButton.Enabled = lockKey;
            this.OwnerRadioButton.Enabled = lockKey;
            //// Added as per CO#5648 by A.Shanmuga Sundaram on 25th March'09 for sprint 68
            this.MarketNonSRCropTextBox.Enabled = lockKey;
            this.MarketNonSRImprovementTextBox.Enabled = lockKey;
            this.MarketNonSRLandTextBox.Enabled = lockKey;
            this.NonSRNewValueTextBox.Enabled = lockKey;   
            //this.UseNonSRCropTextBox.Enabled = lockKey;
            //this.UseNonSRImprovementTextBox.Enabled = lockKey;
            //this.UseNonSRLandTextBox.Enabled = lockKey;
            this.OriginalFrozenValueTextBox.Enabled = lockKey;
        }

        /// <summary>
        /// BlockPanelControl
        /// </summary>
        /// <param name="enable">enable</param>
        private void BlockPanelControl(bool enable)
        {
            this.Ownerpanel.Enabled = enable;
            this.OwnerAddresspanel.Enabled = enable;
            this.CityZipCodepanel.Enabled = enable;
            this.ApplicantCertifiespanel.Enabled = enable;
            this.Applicantpanel.Enabled = enable;
            this.Spousepanel.Enabled = enable;
            this.propertyPurchasedpanel.Enabled = enable;
            this.PropertyOccupiedpanel.Enabled = enable;
            this.TypeofOwnershippanel.Enabled = enable;
            this.Residencepanel.Enabled = enable;
            this.Yearpanel.Enabled = enable;
            this.MObileHomeMakepanel.Enabled = enable;
            this.MobileHomeModelpanel.Enabled = enable;
            this.MobileHomeLocationpanel.Enabled = enable;
            this.ParcelNopanel.Enabled = enable;
            this.Situspanel.Enabled = enable;
            this.Legalpanel.Enabled = enable;
            this.PropertyIncludespanel.Enabled = enable;
            this.Acrespanel.Enabled = enable;
            this.GrossIncomepanel.Enabled = enable;
            this.MilitaryandVeteranspanel.Enabled = enable;
            this.OtherIncomepanel.Enabled = enable;
            this.LessPrescriptionDrugspanel.Enabled = enable;
            this.CapitalGainspanel.Enabled = enable;
            this.DividendsandInterestpanel.Enabled = enable;
            this.SubtotalIncomepanel.Enabled = enable;
            this.LessInsurancePremiumspanel.Enabled = enable;
            this.Deductionspanel.Enabled = enable;
            this.TaxableIRApanel.Enabled = enable;
            this.Homepanel.Enabled = enable;
            this.Instructionpanel.Enabled = enable;
            this.Pensionpanel.Enabled = enable;
            this.Disabilitypanel.Enabled = enable;
            this.HomeCarepanel.Enabled = enable;
            this.SubtotalDeductionpanel.Enabled = enable;
            this.DisposableIncomepanel.Enabled = enable;
            this.Qualifiedpanel.Enabled = enable;
            this.EffectiveDatepanel.Enabled = enable;
            this.CancelDatepanel.Enabled = enable;
            this.ExemptionCodepanel.Enabled = enable;
            //this.Activepanel.Enabled = enable;
            this.OwnerPercentpanel.Enabled = enable;
            this.Continuationpanel.Enabled = enable;
            this.OriginalFrozenValuepanel.Enabled = enable;
            this.FinalAssessedValuepanel.Enabled = enable;
            this.NewConstructionpanel.Enabled = enable;
            this.ResultingOwnerValuepanel.Enabled = enable;
            this.ResultingTaxableValuepanel.Enabled = enable;
            this.ReductionOfvaluepanel.Enabled = enable;
            //// Added as per CO#5648 by A.Shanmuga Sundaram on 25th March'09 for sprint 68
            //this.UseNonSRLandpanel.Enabled = enable;
            //this.UseNonSRImprovementpanel.Enabled = enable;
            //this.UseNonSRCroppanel.Enabled = enable;
            this.MarketNonSRLandpanel.Enabled = enable;
            this.MarketNonSRImprovementpanel.Enabled = enable;
            this.MarketNonSRCroppanel.Enabled = enable;
            //CO:8049
            this.pensionannuities2panel.Enabled = enable;
            this.Socialsecurity2panel.Enabled = enable;
            this.otherIncome2panel.Enabled = enable;
            this.otherincome3.Enabled = enable;
            this.LessOther2Panel.Enabled = enable;
            this.NonSRValuePanel.Enabled = enable;  
 
        }

        /// <summary>
        /// SetNullToDataSet
        /// </summary>
        private void SetNullToDataSet()
        {
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.ExemptionIDColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.OwnerIDColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.ApplicantBdayDateColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.SpouseBdayDateColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.PropertyPurchasedDateColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.OwnershipTypeColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.ResidencelsColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.MHYearColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.MHMakeColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.MHModelColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.MHLocationColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.ParcelNumberColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.LegalColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.PropertyIncludesColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.AcresColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.AdjustedGrossColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.MilitaryPayColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.OtherIncomeColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.LessPrescriptionColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.CapitalGainsColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.DividendsColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.LessInsuranceColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.DeductionsColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.TaxableIRAColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.LessHomeColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.LessOtherColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.PensionsColumn.AllowDBNull = true;
            //CO:8049
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.Pensions2Column.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.SocialSecurityDisability2Column.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.OtherIncome2Column.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.OtherIncome3Column.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.LessOther2Column.AllowDBNull = true;   
            
  


            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.SocialSecurityDisabilityColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.LessInHomeColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.CombinedDisposableColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.EffectiveDateColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.CancelDateColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.OwnerPercentColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.FrozenValueColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.MarketCropColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.MarketImprovementColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.MarketLandColumn.AllowDBNull = true;
            this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.NonSRNewConstructionColumn.AllowDBNull = true;  
            //this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.UseCropColumn.AllowDBNull = true;
            //this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.UseImprovementColumn.AllowDBNull = true;
            //this.seniorExemptDataSet.f29600ListSeniorExemptionDataTable.UseLandColumn.AllowDBNull = true;
        }

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
            string validationmessage = string.Empty;
            requiredControl = this.CheckRequiredFields();
            if (requiredControl != null)
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("SeniorExemption");
                requiredControl.Focus();
                return sliceValidationFields;
            }

            if (!string.IsNullOrEmpty(this.EffectiveDateTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.CancelDateTextBox.Text.Trim()))
            {
                if (DateTime.Parse(this.EffectiveDateTextBox.Text) > DateTime.Parse(this.CancelDateTextBox.Text))
                {
                    validationmessage = "Cancel date cannot be less than Effective date";
                    sliceValidationFields.ErrorMessage = validationmessage;
                    this.CanceldateTimePicker.Focus();
                    return sliceValidationFields;
                }
            }
            ////Added by Biju to fix #876 on 17-Jun-09
            if (Convert.ToDecimal(this.MarketNonSRImprovementTextBox.Text.Replace("$", "").Replace(",", "")) > 922337203685477)
            {
                validationmessage = "Market Non-SR Improvement value exceeds the maximum limit";
                sliceValidationFields.ErrorMessage = validationmessage;
                this.MarketNonSRImprovementTextBox.Focus();
                return sliceValidationFields;
            }
            else if (Convert.ToDecimal(this.MarketNonSRLandTextBox.Text.Replace("$", "").Replace(",", "")) > 922337203685477)
            {
                validationmessage = "Market Non- SR Land value exceeds the maximum limit";
                sliceValidationFields.ErrorMessage = validationmessage;
                this.MarketNonSRLandTextBox.Focus();
                return sliceValidationFields;
            }
            else if (Convert.ToDecimal(this.MarketNonSRCropTextBox.Text.Replace("$", "").Replace(",", "")) > 922337203685477)
            {
                validationmessage = "Market Non- SR Crop value exceeds the maximum limit";
                sliceValidationFields.ErrorMessage = validationmessage;
                this.MarketNonSRCropTextBox.Focus();
                return sliceValidationFields;
            }

            else if (Convert.ToDecimal(this.NonSRNewValueTextBox.Text.Replace("$", "").Replace(",", "")) > 922337203685477)
            {
                validationmessage = " Non- SR Instruction value exceeds the maximum limit";
                sliceValidationFields.ErrorMessage = validationmessage;
                this.NonSRNewValueTextBox.Focus();
                return sliceValidationFields;
            }

            //else if (Convert.ToDecimal(this.UseNonSRImprovementTextBox.Text.Replace("$", "").Replace(",", "")) > 922337203685477)
            //{
            //    validationmessage = "Use Non- SR Improvement value exceeds the maximum limit";
            //    sliceValidationFields.ErrorMessage = validationmessage;
            //    this.UseNonSRImprovementTextBox.Focus();
            //    return sliceValidationFields;
            //}
            //else if (Convert.ToDecimal(this.UseNonSRLandTextBox.Text.Replace("$", "").Replace(",", "")) > 922337203685477)
            //{
            //    validationmessage = "Use Non- SR Land value exceeds the maximum limit";
            //    sliceValidationFields.ErrorMessage = validationmessage;
            //    this.UseNonSRLandTextBox.Focus();
            //    return sliceValidationFields;
            //}
            //else if (Convert.ToDecimal(this.UseNonSRLandTextBox.Text.Replace("$", "").Replace(",", "")) > 922337203685477)
            //{
            //    validationmessage = "Use Non- SR Land value exceeds the maximum limit";
            //    sliceValidationFields.ErrorMessage = validationmessage;
            //    this.UseNonSRLandTextBox.Focus();
            //    return sliceValidationFields;
            //}
            //else if (Convert.ToDecimal(this.UseNonSRCropTextBox.Text.Replace("$", "").Replace(",", "")) > 922337203685477)
            //{
            //    validationmessage = "Use Non- SR Crop value exceeds the maximum limit";
            //    sliceValidationFields.ErrorMessage = validationmessage;
            //    this.UseNonSRCropTextBox.Focus();
            //    return sliceValidationFields;
            //}
            ////till here
            else if (Convert.ToDecimal(this.OriginalFrozenValueTextBox.Text.Replace("$", "").Replace(",", "")) > 922337203685477)
            {
                validationmessage = "Original Frozen value exceeds the maximum limit";
                sliceValidationFields.ErrorMessage = validationmessage;
                this.OriginalFrozenValueTextBox.Focus();
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
            if (this.OwnerComboBox.SelectedIndex.Equals(-1))
            {
                requiredControll = this.OwnerComboBox;
            }
            else if ((!this.IsCertifyAgecheckBox.Checked) && (!this.IsCertifyDisabilitycheckBox.Checked) && (!this.IsCertifySpousecheckBox.Checked) && (!this.IsCertifyVeterancheckBox.Checked))
            {
                requiredControll = this.IsCertifyAgecheckBox;
            }

            return requiredControll;
        }

        /// <summary>
        /// SaveButton_Click
        /// </summary>
        private void Save()
        {
            this.SaveSeniorExemptRecords(false);
        }

        /// <summary>
        /// SaveSeniorExemptRecords
        /// </summary>
        /// <param name="onclose">onclose</param>
        /// <returns> bool </returns>
        private bool SaveSeniorExemptRecords(bool onclose)
        {
            ////int eventId = 0;
            int returnValue = 0;
            int intValue;
            short shortvalue;
            decimal decValue;
            ////DateTime dateVaue;

            F29600SeniorExemptData saveSeniorExemption = new F29600SeniorExemptData();

            saveSeniorExemption.f29600ExemptCodeDataTable.RollYearColumn.AllowDBNull = true;
            F29600SeniorExemptData.saveSeniorExemptDataTableRow seniorExemptRow = saveSeniorExemption.saveSeniorExemptDataTable.NewsaveSeniorExemptDataTableRow();

            int.TryParse(this.OwnerComboBox.SelectedValue.ToString(), out intValue);
            seniorExemptRow.OwnerID = intValue;
            seniorExemptRow.IsCertifyAge = this.IsCertifyAgecheckBox.Checked;
            seniorExemptRow.IsCertifyDisability = this.IsCertifyDisabilitycheckBox.Checked;
            seniorExemptRow.IsCertifyVeteran = this.IsCertifyVeterancheckBox.Checked;
            seniorExemptRow.IsCertifySpouse = this.IsCertifySpousecheckBox.Checked;
            seniorExemptRow.ApplicantBdayDate = this.ApplicantBDayTextBox.Text.Trim();

            seniorExemptRow.SpouseBdayDate = this.SpouseBdayTextBox.Text.Trim();
            seniorExemptRow.PropertyPurchasedDate = this.PropertyTextBox.Text.Trim();
            seniorExemptRow.PropertyOccupiedDate = this.PropertyOccupiedTextBox.Text.Trim();
            if (this.LeaseLifeRadioButton.Checked)
            {
                seniorExemptRow.OwnershipType = (int)OwnerShipType.Leaseforlife;
            }
            else if (this.LifeEstateRadioButton.Checked)
            {
                seniorExemptRow.OwnershipType = (int)OwnerShipType.LifeEstate;
            }
            else
            {
                seniorExemptRow.OwnershipType = (int)OwnerShipType.owner;
            }

            short.TryParse(this.ResidenceComboBox.SelectedValue.ToString(), out shortvalue);
            seniorExemptRow.Residencels = shortvalue;
            short.TryParse(this.MHYearTextBox.Text.Trim(), out shortvalue);
            seniorExemptRow.MHYear = shortvalue;
            seniorExemptRow.MHMake = this.MobileHomeMakeTextBox.Text.Trim();
            seniorExemptRow.MHModel = this.MobileHomeModelTextBox.Text.Trim();
            seniorExemptRow.MHLocation = this.MobileHomeLocationTextBox.Text.Trim();
            Int16.TryParse(this.PropertyIncludesComboBox.SelectedValue.ToString(), out shortvalue);
            seniorExemptRow.PropertyIncludes = shortvalue;
            Int16.TryParse(this.AcresTextBox.Text.Trim(), out shortvalue);
            seniorExemptRow.Acres = shortvalue;
            decimal.TryParse(this.GrossIncomeTextBox.Text.Trim(), out decValue);
            seniorExemptRow.AdjustedGross = decValue;
            decimal.TryParse(this.MilitaryandVeteransTextBox.Text.Trim(), out decValue);
            seniorExemptRow.MilitaryPay = decValue;
            decimal.TryParse(this.OtherIncomeTextBox.Text.Trim(), out decValue);
            seniorExemptRow.OtherIncome = decValue;
            decimal.TryParse(this.LessPrescriptionDrugsTextBox.Text.Trim(), out decValue);
            seniorExemptRow.LessPrescription = decValue;
            decimal.TryParse(this.CapitalGainsTextBox.Text.Trim(), out decValue);
            seniorExemptRow.CapitalGains = decValue;
            decimal.TryParse(this.DividendsandInterestTextBox.Text.Trim(), out decValue);
            seniorExemptRow.Dividends = decValue;

            decimal.TryParse(this.LessInsurancePremiumsTextBox.Text.Trim(), out decValue);
            seniorExemptRow.LessInsurance = decValue;
            decimal.TryParse(this.DeductionsTextBox.Text.Trim(), out decValue);
            seniorExemptRow.Deductions = decValue;
            decimal.TryParse(this.TaxableIRATextBox.Text.Trim(), out decValue);
            seniorExemptRow.TaxableIRA = decValue;
            decimal.TryParse(this.HomeTextBox.Text.Trim(), out decValue);
            seniorExemptRow.LessHome = decValue;
            decimal.TryParse(this.InstructionTextBox.Text.Trim(), out decValue);
            seniorExemptRow.LessOther = decValue;
            decimal.TryParse(this.PensionTextBox.Text.Trim(), out decValue);
            seniorExemptRow.Pensions = decValue;
            
            
            //CO:8049
            decimal.TryParse(this.PensionAnnuity2TextBox.Text.Trim(), out decValue);
            seniorExemptRow.Pensions2 = decValue;
            decimal.TryParse(this.SocialSecurity2TextBox.Text.Trim(), out decValue);
            seniorExemptRow.SocialSecurityDisability2  = decValue;
            decimal.TryParse(this.OtherIncome2TextBox.Text.Trim(), out decValue);
            seniorExemptRow.OtherIncome2  = decValue;
            decimal.TryParse(this.OtherIncome3TextBox.Text.Trim(), out decValue);
            seniorExemptRow.OtherIncome3  = decValue;
            decimal.TryParse(this.LessOther2TextBox.Text.Trim(), out decValue);
            seniorExemptRow.LessOther2 = decValue;

            
                
                
                
            decimal.TryParse(this.DisabilityTextBox.Text.Trim(), out decValue);
            seniorExemptRow.SocialSecurityDisability = decValue;
            decimal.TryParse(this.HomeCareTextBox.Text.Trim(), out decValue);
            seniorExemptRow.LessInHome = decValue;
            this.manualValue = this.DisposableIncomeTextBox.DecimalTextBoxValue;  
            ////Added by Biju to fix #878 on 17-Jun-09
            //this.CalculateSubTotalIncome();
            //this.CalculateSubtotalDeduction();
            ////till here
            seniorExemptRow.CombinedDisposable = this.DisposableIncomeTextBox.DecimalTextBoxValue; //this.subTotalIncome - this.subTotalDeduction;
            //if (this.manualValue != seniorExemptRow.CombinedDisposable)
            //{
            //    this.manualChange = false; 
            //}
            //if (!this.manualChange)
            //{
            //    this.autoSelections(seniorExemptRow.CombinedDisposable);
            //}
            if (this.QualifiedComboBox.SelectedIndex == 0)
            {
                seniorExemptRow.IsQualified = false;
            }
            else if (this.QualifiedComboBox.SelectedIndex == 1)
            {
                seniorExemptRow.IsQualified = true;
            }

            seniorExemptRow.EffectiveDate = this.EffectiveDateTextBox.Text.Trim();
            seniorExemptRow.CancelDate = this.CancelDateTextBox.Text.Trim();
            if (this.ExemptionCodeComboBox.SelectedValue != null)
            {
                seniorExemptRow.ExemptionCode = this.ExemptionCodeComboBox.SelectedValue.ToString();
            }
            else
            {
                seniorExemptRow.ExemptionCode = string.Empty;
            }
            /// Added by Biju on 20-Oct-2010 to implement #8874
            if (this.ContinuationComboBox.SelectedIndex == 0)
            {
                seniorExemptRow.IsContinuation  = false;
            }
            else if (this.ContinuationComboBox.SelectedIndex == 1)
            {
                seniorExemptRow.IsContinuation = true;
            }
            //removed for the co:8049

            
            //if (this.ActiveComboBox.SelectedIndex == 0)
            //{
            //    seniorExemptRow.IsActive = false;
            //}
            //else if (this.ActiveComboBox.SelectedIndex == 1)
            //{
            //    seniorExemptRow.IsActive = true;
            //}

            //this.FinalAssessedValueTextBox.Text = this.FinalAssessedValueTextBox.Text.Replace("&", "");
            decimal.TryParse(this.FinalAssessedValueTextBox.Text.Replace("$", "").Trim(), out decValue);
            seniorExemptRow.FrozenValue = decValue;
            //this.NewConstructionTextBox.Text = this.NewConstructionTextBox.Text.Replace("&", "");
            decimal.TryParse(this.NewConstructionTextBox.Text.Replace("$", "").Trim(), out decValue);
            seniorExemptRow.NewConstruction = decValue;
            //this.ResultingOwnerValueTextBox.Text = this.ResultingOwnerValueTextBox.Text.Replace("&", "");
            decimal.TryParse(this.ResultingOwnerValueTextBox.Text.Replace("$", "").Trim(), out decValue);
            seniorExemptRow.ResultingMarket = decValue;
            //this.ReductionOfvalueTextBox.Text = this.ReductionOfvalueTextBox.Text.Replace("&", "");
            decimal.TryParse(this.ReductionOfvalueTextBox.Text.Replace("$", "").Trim(), out decValue);
            seniorExemptRow.ValueLoss = decValue;
            //this.ResultingTaxableValueTextBox.Text = this.ResultingTaxableValueTextBox.Text.Replace("$", "");
            decimal.TryParse(this.ResultingTaxableValueTextBox.Text.Replace("$", "").Trim(), out decValue);
            seniorExemptRow.ResultingTaxable = decValue;
            //// Added as per CO#5648 by A.Shanmuga Sundaram on 25th March'09 for sprint 68
            //this.MarketNonSRLandTextBox.Text = this.MarketNonSRLandTextBox.Text.Replace("$", "");
            //decimal.TryParse(this.MarketNonSRLandTextBox.Text.Trim(), out decValue);
            decimal.TryParse(this.MarketNonSRLandTextBox.Text.Replace("$", "").Trim(), out decValue);
            seniorExemptRow.MarketLand = decValue;
            //this.MarketNonSRImprovementTextBox.Text = this.MarketNonSRImprovementTextBox.Text.Replace("$", "");
            //decimal.TryParse(this.MarketNonSRImprovementTextBox.Text.Trim(), out decValue);
            decimal.TryParse(this.MarketNonSRImprovementTextBox.Text.Replace("$", "").Trim(), out decValue);
            seniorExemptRow.MarketImprovement = decValue;
            //this.MarketNonSRCropTextBox.Text = this.MarketNonSRCropTextBox.Text.Replace("$", "");
            //decimal.TryParse(this.MarketNonSRCropTextBox.Text.Trim(), out decValue);
            decimal.TryParse(this.MarketNonSRCropTextBox.Text.Replace("$", "").Trim(), out decValue);
            seniorExemptRow.MarketCrop = decValue;
            //CO:8049
            decimal.TryParse(this.NonSRNewValueTextBox.Text.Replace("$", "").Trim(), out decValue);
            seniorExemptRow.NonSRNewConstruction = decValue;  
            
            
            //this.UseNonSRLandTextBox.Text = this.UseNonSRLandTextBox.Text.Replace("$", "");
            //decimal.TryParse(this.UseNonSRLandTextBox.Text.Trim(), out decValue);
            
            //removed for the cO:8049
            
            //decimal.TryParse(this.UseNonSRLandTextBox.Text.Replace("$", "").Trim(), out decValue);
            //seniorExemptRow.UseLand = decValue;
            ////this.UseNonSRImprovementTextBox.Text = this.UseNonSRImprovementTextBox.Text.Replace("$", "");
            ////decimal.TryParse(this.UseNonSRImprovementTextBox.Text.Trim(), out decValue);
            //decimal.TryParse(this.UseNonSRImprovementTextBox.Text.Replace("$", "").Trim(), out decValue);
            //seniorExemptRow.UseImprovement = decValue;
            ////this.UseNonSRCropTextBox.Text = this.UseNonSRCropTextBox.Text.Replace("$", "");
            ////decimal.TryParse(this.UseNonSRCropTextBox.Text.Trim(), out decValue);
            //decimal.TryParse(this.UseNonSRCropTextBox.Text.Replace("$", "").Trim(), out decValue);
            //seniorExemptRow.UseCrop = decValue;
            ////// Added as per the TSCO# 7398 for 75th sprint
            //this.OriginalFrozenValueTextBox.Text = this.OriginalFrozenValueTextBox.Text.Replace("$", "");
            //decimal.TryParse(this.OriginalFrozenValueTextBox.Text.Trim(), out decValue);
            decimal.TryParse(this.OriginalFrozenValueTextBox.Text.Replace("$", "").Trim(), out decValue);
            seniorExemptRow.OriginalFrozenValue = decValue;
            saveSeniorExemption.saveSeniorExemptDataTable.Rows.Add(seniorExemptRow);
            saveSeniorExemption.saveSeniorExemptDataTable.AcceptChanges();
            DataSet tempDataSet = new DataSet("Root");
            tempDataSet.Tables.Add(saveSeniorExemption.saveSeniorExemptDataTable.Copy());
            tempDataSet.Tables[0].TableName = "Table";
            ////DB call for save 
            returnValue = this.F29600Control.WorkItem.F29600_saveSeniorExemptionDetails(this.keyId, tempDataSet.GetXml(), TerraScanCommon.UserId);
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            SliceReloadActiveRecord currentKeyIdInfo;
            currentKeyIdInfo.MasterFormNo = this.masterFormNo;
            currentKeyIdInfo.SelectedKeyId = returnValue;
            this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentKeyIdInfo));
            SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
            sliceReloadActiveRecord.SelectedKeyId = returnValue;
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
            if (onclose)
            {
                return true;
            }

            return true;
        }

        /// <summary>
        /// Validate Date
        /// </summary>
        private void ValidateDate()
        {
            DateTime purchasedate;
            DateTime occupiedDate;
            DateTime.TryParse(this.PropertyTextBox.Text.Trim(), out purchasedate);
            DateTime.TryParse(this.PropertyOccupiedTextBox.Text.Trim(), out occupiedDate);
            if ((purchasedate > occupiedDate) && (!string.IsNullOrEmpty(this.PropertyTextBox.Text.Trim())) && (!string.IsNullOrEmpty(this.PropertyOccupiedTextBox.Text.Trim())))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("F29600_InvalidOccupiedDate"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.PropertyOccupiedTextBox.Focus();
                this.PropertyOccupiedTextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// EnableExemptCodeCombo
        /// </summary>
        private void EnableExemptCodeCombo()
        {
            if ((this.QualifiedComboBox.SelectedIndex == 1) && (!string.IsNullOrEmpty(this.rollyear))) // && (!string.IsNullOrEmpty(this.EffectiveDateTextBox.Text.Trim())))
            {
                this.ExemptionCodeComboBox.Enabled = true;
                this.FillExemptCodeComboBox();
            }
            else
            {
                this.ExemptionCodeComboBox.Enabled = false;
                this.ExemptionCodeComboBox.DataSource = null;
            }
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SetEditRecord(object sender, EventArgs e)
        {
             // For Bug Fix 877
            // IF included to avoid forms goes to Edit mode on load
            if (this.formLoad)
            {
                this.saveChanged = true;
                this.EditRecord();
            }
        }
        /// <summary>
        /// Sets the Combined Disposable Income Calculation
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CalculateCdiEdit(object sender, EventArgs e)
        {
            if (this.formLoad)
            {
                this.CDItxtChanged = true;
            if(this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
            }
        }

        /// <summary>
        /// Sets the Combined Disposable Income Calculation
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CalculateCDI(object sender, EventArgs e)
        {
            
                
            // For Bug Fix 877
            // IF included to avoid forms goes to Edit mode on load
            try
            {
                if ((sender as TerraScanTextBox).LockKeyPress)
                {
                    this.saveChanged = false;
                }
                if (this.CDItxtChanged)
                {
                    this.CombinedDisposableIncome();
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
          
        }
        /// <summary>
        /// EditRecord
        /// </summary>
        private void EditRecord()
        {
            if (this.saveChanged && this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        /// <summary>
        /// TimePicker_KeyPress
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void TimePicker_KeyPress(object sender, KeyPressEventArgs e)
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

                SendKeys.Send("{F4}");
                timePickerControl.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// F29600_Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F29600_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.pageLoadStatus = true;
                this.FillResidenceIsCombo();
                this.FillpropertyIncludesCombo();
                this.FillQualifiedCombobox();
                ////Added by Biju on 20-Oct-2010 to implement #8874
                this.FillContinuationCombobox();
                //this.FillActiveComboBox();
                this.LoadSeniorExemptionDetails();
                this.FillExemptCodeComboBox();
                this.pageLoadStatus = false;
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// SeniorExemptPictureBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SeniorExemptPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// SeniorExemptPictureBox_MouseEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SeniorExemptPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.SeniorExemptionToolTip.SetToolTip(this.SeniorExemptPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// PropertyOccupiedTextBox_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void PropertyOccupiedTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.ValidateDate();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ResidenceComboBox_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ResidenceComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.ChangeResidenceIsCombo();
                this.saveChanged = true;
                this.EditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// EffectiveDateTextBox_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void EffectiveDateTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!this.ExemptionCodeComboBox.Enabled)
                {
                    this.EnableExemptCodeCombo();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// QualifiedComboBox_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void QualifiedComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.EnableExemptCodeCombo();
                this.QualifiedCheckBoxStatus();
                this.manualChange = true; 
                this.saveChanged = true;
                this.EditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ApplicantdateTimePicker_CloseUp
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ApplicantdateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.ApplicantBDayTextBox.Text = this.ApplicantdateTimePicker.Text;
                this.ApplicantBDayTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// SpousedateTimePicker_CloseUp
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SpousedateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.SpouseBdayTextBox.Text = this.SpousedateTimePicker.Text;
                this.SpouseBdayTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// PropertyPurchaseddateTimePicker_CloseUp
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void PropertyPurchaseddateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.PropertyTextBox.Text = this.PropertyPurchaseddateTimePicker.Text;
                this.PropertyTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// PropertyOccupieddateTimePicker_CloseUp
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void PropertyOccupieddateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.PropertyOccupiedTextBox.Text = this.PropertyOccupieddateTimePicker.Text;
                this.PropertyOccupiedTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// EffectivedateTimePicker_CloseUp
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void EffectivedateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.EffectiveDateTextBox.Text = this.EffectivedateTimePicker.Text;
                this.EffectiveDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ApplicantDatePict_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ApplicantDatePict_Click(object sender, EventArgs e)
        {
            try
            {
                this.saveChanged = true;
                this.TimerImage_Click(this.ApplicantBDayTextBox, this.ApplicantdateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// SpouseDatePict_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SpouseDatePict_Click(object sender, EventArgs e)
        {
            try
            {
                this.saveChanged = true;
                this.TimerImage_Click(this.SpouseBdayTextBox, this.SpousedateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// PropertyDatePict_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void PropertyDatePict_Click(object sender, EventArgs e)
        {
            try
            {
                this.saveChanged = true;
                this.TimerImage_Click(this.PropertyTextBox, this.PropertyPurchaseddateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// PropertyOccupiedDatepict_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void PropertyOccupiedDatepict_Click(object sender, EventArgs e)
        {
            try
            {
                this.saveChanged = true;
                this.TimerImage_Click(this.PropertyOccupiedTextBox, this.PropertyOccupieddateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// EffectiveDatePict_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void EffectiveDatePict_Click(object sender, EventArgs e)
        {
            try
            {
                this.saveChanged = true;
                this.TimerImage_Click(this.EffectiveDateTextBox, this.EffectivedateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// PropertyOccupiedTextBox_Validating
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void PropertyOccupiedTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                this.ValidateDate();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// PropertyIncludesComboBox_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void PropertyIncludesComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.saveChanged = true;
                this.ChangePropertyIncludesCombo();
                this.EditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// LeaseLifeRadioButton_CheckedChanged
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void LeaseLifeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.formLoad)
                {
                    this.saveChanged = true;
                    this.EditRecord();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// LifeEstateRadioButton_CheckedChanged
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void LifeEstateRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.formLoad)
                {
                    this.saveChanged = true;
                    this.EditRecord();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// OwnerRadioButton_CheckedChanged
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void OwnerRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.formLoad)
                {
                    this.saveChanged = true;
                    this.EditRecord();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ActiveComboBox_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ActiveComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.saveChanged = true;
                this.EditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ExemptionCodeComboBox_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ExemptionCodeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.manualChange = true;
                this.saveChanged = true;
                this.EditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                    this.saveChanged = true;
                    this.EditRecord();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// SetKeyPress
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SetKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //if (e.KeyChar == 3)
                //{                    
                //    return;
                //}

                //if ((sender as TerraScanTextBox).LockKeyPress)
                //{
                //    this.saveChanged = false;
                //}
                //else
                //{
                //    this.saveChanged = true;
                //    this.EditRecord();
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Texts the box leave.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TextBoxLeave(object sender, EventArgs e)
        {
            try
            {
                if ((sender as TerraScanTextBox).LockKeyPress)
                {
                    this.saveChanged = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Handles the Leave event of the DisposableIncomeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DisposableIncomeTextBox_Leave(object sender, EventArgs e)
        {
            if (this.QualifiedComboBox.SelectedIndex == 1)
            {
                this.DisposableIncomeTextBox.ForeColor = Color.FromArgb(192, 0, 0);
            }
            else
            {
                this.DisposableIncomeTextBox.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the DisposableIncomeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DisposableIncomeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.QualifiedComboBox.SelectedIndex == 1)
            {
                this.DisposableIncomeTextBox.ForeColor = Color.FromArgb(192, 0, 0);
            }
            else
            {
                this.DisposableIncomeTextBox.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        /// <summary>
        /// Handles the Click event of the CancelDateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CancelDateButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.saveChanged = true;
                this.TimerImage_Click(this.CancelDateTextBox, this.CanceldateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CloseUp event of the CanceldateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CanceldateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.CancelDateTextBox.Text = this.CanceldateTimePicker.Text;
                this.CancelDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the CanceldateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void CanceldateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
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

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the OwnerComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OwnerComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int tempOwnerID;
            int.TryParse(this.OwnerComboBox.SelectedValue.ToString(), out tempOwnerID);
            this.ownerDetailDataSet = this.form29600Controller.WorkItem.F15010_GetOwnerDetails(tempOwnerID);
            if (this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows.Count > 0)
            {
                this.OwnerAddresslabel.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.Address1Column].ToString() + ", " + this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.Address2Column].ToString();
                this.CityZiplabel.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.CityColumn].ToString() + ", " + this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.StateColumn].ToString() + " " + this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.ZipColumn].ToString();
                this.saveChanged = true;
                this.EditRecord();
            }
        }

        #endregion Events

        private void F29600_Resize(object sender, EventArgs e)
        {
            this.Height = 763;//this.SeniorExemptPanel.Height;

        }

        private void ResultingTaxableValueTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ContinuationComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.saveChanged = true;
                this.EditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


       

        
    }
}