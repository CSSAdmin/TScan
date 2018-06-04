//--------------------------------------------------------------------------------------------
// <copyright file="F36000.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Owner Recipting.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 28 Mar 07		KARTHIKEYAN V	    Created
//*********************************************************************************/

namespace D36000
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.SmartParts;
    using System.Collections;
    using System.IO;
    using TerraScan.Utilities;
    using System.Configuration;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using System.Web.Services.Protocols;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Helper;
    using TerraScan.UI.Controls;
    using Infragistics.Win.UltraWinGrid;
    using System.Xml;
    using Infrastructure.Interface;

    /// <summary>
    /// F36000
    /// </summary>
    [SmartPart]
    public partial class F36000 : BaseSmartPart
    {
        #region Variable

        /// <summary>
        /// Used to store LowQuality
        /// </summary>
        private double lowQuality;

        /// <summary>
        /// Used to store HighQuality
        /// </summary>
        private double highQuality;

        /// <summary>
        /// An object for ContextMenuStrip
        /// </summary>
        private ContextMenuStrip selectedComponentMenuStrip = new ContextMenuStrip();

        /// <summary>
        /// An object for Dataset - HouseTypeCollectionDataSet
        /// </summary>
        private DataSet houseTypeCollectionDataSet = new DataSet();

        /// <summary>
        /// An object for Dataset - F36000MarshalAndSwiftData
        /// </summary>
        private F36000MarshalAndSwiftData xmlCollectionDataSet = new F36000MarshalAndSwiftData();

        /// <summary>
        /// An object for Dataset - AdjustmentDataSet
        /// </summary>
        private DataSet adjustmentDataSet = new DataSet();

        /// <summary>
        /// An object for Dataset - adjustmentDefaultDataSet
        /// </summary>
        private DataSet adjustmentDefaultDataSet = new DataSet();

        /// <summary>
        /// An object for Dataset - rangeDataSet
        /// </summary>
        private DataSet rangeDataSet = new DataSet();

        /// <summary>
        /// An object for Dataset - TagolongDataTable
        /// </summary>
        private DataTable tagolongDataTable = new DataTable();

        /// <summary>
        /// An object for Dataset - savedSelectedComponentDataTable
        /// </summary>
        private DataTable savedSelectedComponentDataTable = new DataTable();

        /// <summary>
        /// An object for Dataset - savedSectionDataTable
        /// </summary>
        // private DataTable savedSectionDataTable = new DataTable();

        /// <summary>
        /// Used to store ValueSliceId(keyid)
        /// </summary>
        private int valueSliceId;

        /// <summary>
        /// Used to store TypedCodeValue(keyid)
        /// </summary>
        private int typedCodeValue;

        /// <summary>
        /// Used to store TotalFloorMaxValue(keyid)
        /// </summary>
        private int totalFloorMaxValue;

        /// <summary>
        /// Used to store unitValue(keyid)
        /// </summary>
        private int unitValue;

        /// <summary>
        /// Used to store storyHeight(keyid)
        /// </summary>
        private double storyHeightValue;

        /// <summary>
        /// Used to store costMultipler(keyid)
        /// </summary>
        private double costMultiplerValue;

        /// <summary>
        /// Used to store sitePrimaryPercentage(keyid)
        /// </summary>
        private double sitePrimaryPercentage;

        /// <summary>
        /// Used to store siteSecondaryPercentage(keyid)
        /// </summary>
        private double siteSecondaryPercentage;

        /// <summary>
        /// Used to store costMultipler(keyid)
        /// </summary>
        private double manufacturePrimaryPercentage;

        /// <summary>
        /// Used to store manufactureSecondaryPercentage(keyid)
        /// </summary>
        private double manufactureSecondaryPercentage;

        /// <summary>
        /// Used to store TotalFloorMinValue(keyid)
        /// </summary>
        private int totalFloorMinValue;

        /// <summary>
        /// Used to store sectionKeyValue(keyid)
        /// </summary>
        private int sectionKeyValue = 1;

        /// <summary>
        /// Used to store TotalFloorManufactureArea
        /// </summary>
        //// private int totalFloorManufactureArea = 0;

        /// <summary>
        /// totalArea
        /// </summary>
        private double totalFloorArea;

        /// <summary>
        /// Used to store selectedRowNumber(keyid)
        /// </summary>
        //// private int selectedRowNumber;

        /// <summary>
        /// Used to store constructionTypeId(keyid)
        /// </summary>
        private int constructionTypeId;

        /// <summary>
        /// Used to store houseType(keyid)
        /// </summary>
        private int houseType;

        /// <summary>
        /// An object for Dataset - F36000Controller
        /// </summary>
        private F36000Controller form36000Control = new F36000Controller();

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Used to store SectionRowDeleted
        /// </summary>
        private bool sectionRowDeleted;

        /// <summary>
        /// Used to store eventFired
        /// </summary>
        private bool eventFired;

        /// <summary>
        /// Used to store cancelClicked
        /// </summary>
        private bool cancelClicked;

        /// <summary>
        /// Used to store rcnCalculated
        /// </summary>
        private bool rcnCalculated;

        /// <summary>
        /// validTotalFloorArea
        /// </summary>
        // private bool validTotalFloorArea;

        /// <summary>
        /// editModeOnSave
        /// </summary>
        private bool editModeOnSave;

        /// <summary>
        /// Used to store BaseQualityValid
        /// </summary>
        private bool componentTabSelected;

        /// <summary>
        /// Used to store saveProcess
        /// </summary>
        private bool saveProcess;

        /// <summary>
        /// Used to store ComponentFieldValid
        /// </summary>
        private bool componentFieldValid;

        /// <summary>
        /// Used to store TagalongSelected
        /// </summary>
        private bool tagalongSelected;

        /// <summary>
        /// Used to store editMode
        /// </summary>
        private bool editMode;

        /// <summary>
        /// Used to store estimateIdValue
        /// </summary>
        private int estimateIdValue;

        /// <summary>
        /// Used to store FormMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// Used to store ExecutionRequied
        /// </summary>
        private bool executionRequied;

        /// <summary>
        /// Used to store FlagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// Used to store tabProcess
        /// </summary>
        private bool tabProcess;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        // private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// zipCode Local variable.
        /// </summary>
        private string zipCode = string.Empty;

        /// <summary>
        /// An object for DataData - SelectedDataTable
        /// </summary>
        private DataTable selectedComponentDataTable = new DataTable();

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// An object for DataData - SectionDataTable
        /// </summary>
        private DataTable sectionDataTable = new DataTable();

        /// <summary>
        /// An object for DataData - SystemDataTable
        /// </summary>
        private DataTable systemDataTable = new DataTable();

        /// <summary>
        /// An object for DataData - ComponentDataTable
        /// </summary>
        private DataTable componentDataTable = new DataTable();

        /// <summary>
        /// Used to store ResidenceGrouptType
        /// </summary>
        private int residenceGrouptType;

        /// <summary>
        /// Used to store SelectedSystemId
        /// </summary>
        private int selectedSystemId;

        /// <summary>
        /// Used to store ActiveRow
        /// </summary>
        // private int activeRowIndex;

        /// <summary>
        /// Used to store HouseTypeXml
        /// </summary>
        private string houseTypeXml;

        /// <summary>
        /// Used to store SectionReturnXml
        /// </summary>
        private string sectionReturnXml;

        /// <summary>
        /// Used to store SectionReturnXml
        /// </summary>
        private string adjustmentXml;

        /// <summary>
        /// Used to store adjustmentDeafultXml
        /// </summary>
        private string adjustmentDeafultXml;

        /// <summary>
        /// Used to store rangeXml
        /// </summary>
        private string rangeXml;

        /// <summary>
        /// Used to store connectionString
        /// </summary>
        private string connectionString;

        /// <summary>
        /// An object for DataData - CollectionDataSet
        /// </summary>
        // private DataSet collectionDataSet = new DataSet();

        /// <summary>
        /// saveFieldHashTable
        /// </summary>
        private Hashtable saveFieldHashTable = new Hashtable();

        /// <summary>
        /// CalculateRcnHashTable
        /// </summary>
        private Hashtable calculateRcnHashTable = new Hashtable();

        /// <summary>
        /// estimateHashtable
        /// </summary>
        private Hashtable estimateHashtable = new Hashtable();

        /// <summary>
        /// Marshall and Swift ServiceImplimentationClient 
        /// </summary>
        private MSWCFService.ServiceImplimentationClient residenceService = new MSWCFService.ServiceImplimentationClient();

        /// <summary>
        /// Used to store the Ms Version id
        /// </summary>
        private string msversionID = string.Empty;

        /// <summary>
        /// used to store the costMultiplier
        /// </summary>
        private string costMultiplier = string.Empty;

        /// <summary>
        /// rollYear
        /// </summary>
        private int rollYear;

        /// <summary>
        /// calculatedRcn
        /// </summary>
        private string calculatedRcn = string.Empty;

        /// <summary>
        /// deprPercentage
        /// </summary>
        private double deprPercentage;

        /// <summary>
        /// age
        /// </summary>
        private int age;

        /// <summary>
        /// condition
        /// </summary>
        private decimal condition;

        /// <summary>
        /// deprTableId
        /// </summary>
        private int deprTableId;

        /// <summary>
        /// Flag for escape key press
        /// </summary>
        private bool escKeyPressed;

        /// <summary>
        /// Flag to check form master call
        /// </summary>
        private bool isFormMasterCall = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F36000"/> class.
        /// </summary>
        public F36000()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84721"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F36000(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.valueSliceId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.MarshallSwiftPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.MarshallSwiftPictureBox.Height, this.MarshallSwiftPictureBox.Width, tabText, red, green, blue);
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

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
        /// Declared the event SubFormSave of D35000_F35002
        /// </summary>
        [EventPublication(EventTopicNames.D35000_F35002_SubFormSave, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<F35002SubFormSaveEventArgs>> D35000_F35002_SubFormSave;

        #endregion Event Publication

        #region Enum

        /// <summary>
        /// HouseType
        /// </summary>
        private enum HouseType
        {
            /// <summary>
            /// SingleFamilyResidence
            /// </summary>
            SingleFamilyResidence = 1,

            /// <summary>
            /// LowRiseMultiple
            /// </summary>
            LowRiseMultiple = 2,

            /// <summary>
            /// TownHouseEndUnit
            /// </summary>
            TownHouseEndUnit = 3,

            /// <summary>
            /// TownHouseInsideUnit
            /// </summary>
            TownHouseInsideUnit = 4,

            /// <summary>
            /// Duplex
            /// </summary>
            Duplex = 5,

            /// <summary>
            /// ManufacturedHousing
            /// </summary>
            ManufacturedHousing = 6
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the mortgage import template controll.
        /// </summary>
        /// <value>The mortgage import template controll.</value>
        [CreateNew]
        public F36000Controller Form36000Controll
        {
            get { return this.form36000Control as F36000Controller; }
            set { this.form36000Control = value; }
        }

        #endregion

        #region Event Subscription

        /// <summary>
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            if (this.slicePermissionField.newPermission)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
            }
            else
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
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
            if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
            {
                this.cancelClicked = true;
                this.rcnCalculated = true;
                this.escKeyPressed = false;

                if (this.editMode)
                {
                    if (this.estimateHashtable.Count > 0)
                    {
                        this.LoadEstimateValue();
                        this.ClearDepreciationTab();
                        this.LoadDepreciationTab();
                        this.rcnCalculated = true;
                    }
                }
                else
                {
                    this.xmlCollectionDataSet.ListDeprValueDataTable.Clear();
                    this.xmlCollectionDataSet.ListDeprValueDataTable.Merge(this.form36000Control.WorkItem.GetHouseTypeCollection(this.valueSliceId).ListDeprValueDataTable);
                    this.ClearDepreciationTab();
                    this.LoadDepreciationTab();
                    this.rcnCalculated = true;

                    // this.saveFieldHashTable.Clear();
                    this.saveFieldHashTable = (Hashtable)this.calculateRcnHashTable.Clone();
                    this.ReplaceSavedGeneralTabValues();
                    this.ReplaceSavedComponentTabValues();
                }

                this.CalculateTotalArea();
                this.componentFieldValid = false;
                this.cancelClicked = false;
                this.TemplateButtonStatus();
                this.ButtonStatus(false);
                if (this.SectionGridView.OriginalRowCount > this.SectionGridView.NumRowsVisible)
                {
                    this.SectionScrollBar.Visible = false;
                }
                else
                {
                    this.SectionScrollBar.Visible = true;
                }

                if (this.houseType == (int)HouseType.SingleFamilyResidence || this.houseType == (int)HouseType.LowRiseMultiple || this.houseType == (int)HouseType.TownHouseEndUnit || this.houseType == (int)HouseType.TownHouseInsideUnit || this.houseType == (int)HouseType.Duplex)
                {
                    if (this.SecondaryStyleCombo.SelectedValue != null)
                    {
                        if (this.SecondaryStyleCombo.SelectedValue.ToString() == "-1")
                        {
                            this.tagalongSelected = false;
                        }
                    }
                    else
                    {
                        this.tagalongSelected = false;
                    }
                }
                else
                {
                    if (this.ManufacturedHousingTagalongStyleCombo.SelectedValue != null)
                    {
                        if (this.ManufacturedHousingTagalongStyleCombo.SelectedValue.ToString() == "-1")
                        {
                            this.tagalongSelected = false;
                        }
                    }
                    else
                    {
                        this.tagalongSelected = false;
                    }
                }
            }

            this.pageMode = TerraScanCommon.PageModeTypes.View;
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
                    if (!this.eventFired)
                    {
                        this.ComponentInformationLabel.Focus();
                    }

                    SliceValidationFields sliceValidationFields = new SliceValidationFields();
                    sliceValidationFields.FormNo = eventArgs.Data;
                    this.saveProcess = true;
                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
                    this.saveProcess = false;
                }

                this.eventFired = false;
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
            if (this.slicePermissionField.editPermission)
            {
                if (this.ValidateTotalArea())
                {
                    this.Cursor = Cursors.WaitCursor;
                    DataSet saveDataSet = new DataSet();

                    // To get the EStimateID
                    saveDataSet = this.form36000Control.WorkItem.GetHouseTypeCollection(this.valueSliceId);

                    if (saveDataSet.Tables["ListEstimateIDDataTable"].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(saveDataSet.Tables["ListEstimateIDDataTable"].Rows[0]["EstimateID"].ToString()))
                        {
                            int.TryParse(saveDataSet.Tables["ListEstimateIDDataTable"].Rows[0]["EstimateID"].ToString(), out this.estimateIdValue);
                            this.editModeOnSave = true;
                        }
                        else
                        {
                            this.editModeOnSave = false;
                        }
                    }

                    string componentXml = string.Empty;
                    string sectionXml = string.Empty;
                    componentXml = TerraScanCommon.GetXmlString(this.selectedComponentDataTable);
                    sectionXml = this.RemoveSectionXmlEmptyRow();

                    // Populate the values into hashtable to save
                    this.SaveElements();
                    this.editMode = false;

                    this.form36000Control.WorkItem.SaveDepreciationDetails(this.GetDepreciationXml(), this.valueSliceId, TerraScanCommon.UserId);

                    // Pass the HashTable to WebService
                    this.residenceService.F36000_SaveMarshallSwift(this.saveFieldHashTable, componentXml, sectionXml, this.editModeOnSave);

                    this.calculateRcnHashTable = (Hashtable)this.saveFieldHashTable.Clone();

                    #region SaveValueSlice Event

                    // Update Appraisal Summary Table
                    decimal resultAmount;
                    Decimal.TryParse(this.RcnldTextBox.Text.Trim(), System.Globalization.NumberStyles.Currency, null, out resultAmount);

                    F35002SubFormSaveEventArgs subFormSaveEventArgs;
                    subFormSaveEventArgs.type = 5;
                    subFormSaveEventArgs.value = resultAmount;
                    subFormSaveEventArgs.valueSliceId = this.valueSliceId;

                    subFormSaveEventArgs.amount = resultAmount;
                    this.D35000_F35002_SubFormSave(this, new DataEventArgs<F35002SubFormSaveEventArgs>(subFormSaveEventArgs));

                    #endregion SaveValueSlice Event

                    this.Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ alert distinguished slice on close].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_AlertDistinguishedSliceOnClose, ThreadOption.UserInterface)]
        public void OnD9030_F9030_AlertDistinguishedSliceOnClose(object sender, TerraScan.Infrastructure.Interface.EventArgs<AlertSliceOnClose> eventArgs)
        {
            if (eventArgs.Data.FormNo == this.masterFormNo && eventArgs.Data.FlagFormClose)
            {
                if (this.MarshallSwiftTab.SelectedIndex == 0)
                {
                    this.GeneralHeaderLabel.Focus();
                }
                else if (this.MarshallSwiftTab.SelectedIndex == 2)
                {
                    this.ByObjectLabel.Focus();
                }

                if (this.componentTabSelected)
                {
                    eventArgs.Data.FlagFormClose = false;
                }
                else
                {
                    eventArgs.Data.FlagFormClose = true;
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
                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;
                this.GeneralInformationTab.Controls.Owner.Enabled = this.PermissionFiled.editPermission;
                this.DepreciationTab.Controls.Owner.Enabled = this.PermissionFiled.editPermission;
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
                this.flagLoadOnProcess = false;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
        }

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

        #endregion Protected methods

        #region Events

        #region Common

        /// <summary>
        /// Handles the MouseEnter event of the MarshallSwiftPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MarshallSwiftPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.MarshallSwiftToolTip.SetToolTip(this.MarshallSwiftPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Load event of the F36000 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F36000_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;

                // Get HTC xml from Database
                this.xmlCollectionDataSet = this.form36000Control.WorkItem.GetHouseTypeCollection(this.valueSliceId);

                if (this.xmlCollectionDataSet.Tables.Count > 0)
                {
                    // Load GeneralTab
                    this.FormLoad();

                    // Initilize Component Grid
                    this.LoadSelectedComponentRecords();

                    // Load Component Tab
                    this.LoadComponentTab();

                    // Load Depreciation Tab
                    this.LoadDepreciationTab();

                    ////Ms version Id
                    this.msversionID = this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.Rows[0][this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.MSVersionIDColumn.ColumnName].ToString();

                    // Connection String
                    this.connectionString = this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.Rows[0][this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.ConnectionStringColumn.ColumnName].ToString();
                    this.costMultiplier = this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.Rows[0][this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.CostMultiplierColumn.ColumnName].ToString();

                    if (!string.IsNullOrEmpty(this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.Rows[0][this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.CostMultiplierColumn.ColumnName].ToString()))
                    {
                        this.CostMultiplerTextBox.Text = this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.Rows[0][this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.CostMultiplierColumn.ColumnName].ToString();
                    }

                    if (this.xmlCollectionDataSet.Tables["ListEstimateIDDataTable"].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(this.xmlCollectionDataSet.Tables["ListEstimateIDDataTable"].Rows[0]["EstimateID"].ToString()))
                        {
                            int.TryParse(this.xmlCollectionDataSet.Tables["ListEstimateIDDataTable"].Rows[0]["EstimateID"].ToString(), out this.estimateIdValue);
                            this.estimateHashtable = this.residenceService.F36000_GetEstimateObject(this.estimateIdValue, this.connectionString);

                            if (this.estimateHashtable.Count > 0)
                            {
                                // Load Estimate Value
                                this.LoadEstimateValue();
                                this.editMode = true;
                                this.editModeOnSave = true;
                            }
                            else
                            {
                                this.editModeOnSave = false;
                            }
                        }
                    }

                    this.rcnCalculated = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Selecting event of the MarshallSwiftTab control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.TabControlCancelEventArgs"/> instance containing the event data.</param>
        private void MarshallSwiftTab_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                if (this.PermissionFiled.editPermission)
                {
                    if (this.houseTypeCollectionDataSet.Tables.Count > 0)
                    {
                        this.eventFired = false;

                        if (e.TabPageIndex == 1)
                        {
                            if (this.componentTabSelected)
                            {
                                e.Cancel = true;
                                this.tabProcess = true;
                                return;
                            }

                            this.ValidateArea(e);

                            if (this.totalFloorArea >= this.totalFloorMinValue && this.totalFloorArea <= this.totalFloorMaxValue)
                            {
                                this.LoadComponentTab();
                            }
                            else
                            {
                                e.Cancel = true;
                                MessageBox.Show(SharedFunctions.GetResourceString("TotalAreaErrorMsg") + this.totalFloorMinValue + SharedFunctions.GetResourceString("ErrorMsgThrough") + this.totalFloorMaxValue, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            if (this.componentFieldValid)
                            {
                                e.Cancel = true;
                            }
                        }

                        if (e.TabPageIndex == 0)
                        {
                            if (this.componentTabSelected)
                            {
                                e.Cancel = true;
                                this.tabProcess = true;
                                return;
                            }

                            if (this.componentFieldValid)
                            {
                                e.Cancel = true;
                            }
                            else
                            {
                                if (!this.cancelClicked)
                                {
                                    if (!this.CheckMainComponent())
                                    {
                                        e.Cancel = true;
                                    }
                                }
                            }
                        }

                        if (e.TabPageIndex == 2)
                        {
                            if (this.componentTabSelected)
                            {
                                e.Cancel = true;
                                this.tabProcess = true;
                                return;
                            }

                            this.ValidateArea(e);

                            if (this.totalFloorArea >= this.totalFloorMinValue && this.totalFloorArea <= this.totalFloorMaxValue)
                            {
                                // Load the Component Tab Form
                                this.LoadComponentTab();
                            }
                            else
                            {
                                e.Cancel = true;
                                MessageBox.Show(SharedFunctions.GetResourceString("TotalAreaErrorMsg") + this.totalFloorMinValue + SharedFunctions.GetResourceString("ErrorMsgThrough") + this.totalFloorMaxValue, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    this.ComponentTab.Controls.Owner.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DoubleClick event of the MarshallSwiftImagePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MarshallSwiftImagePictureBox_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Residential Estimator Details:\nMS Version ID       :   " + this.msversionID + "\nCost data path     :   " + this.connectionString + "\nMultiplier Value     :   " + this.costMultiplier + "\nCost as of            :", "TerraScan T2 - Marshal & Swift Residential", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the MarshallSwiftPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MarshallSwiftPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D36000.F36000"));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region General Tab

        /// <summary>
        /// Handles the Leave event of the UnitTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UnitTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                int unitLowValue = 0;
                int unitHighValue = 0;
                int.TryParse(this.UnitTextBox.Text.Trim(), out this.unitValue);

                if (this.rangeDataSet.Tables.Count > 0 && this.rangeDataSet.Tables["LowRiseMultipleUnits"].Rows.Count > 0)
                {
                    int.TryParse(this.rangeDataSet.Tables["LowRiseMultipleUnits"].Rows[0]["LowValue"].ToString(), out unitLowValue);
                    int.TryParse(this.rangeDataSet.Tables["LowRiseMultipleUnits"].Rows[0]["HighValue"].ToString(), out unitHighValue);
                }

                if (this.unitValue >= unitLowValue && this.unitValue <= unitHighValue)
                {
                    this.UnitTextBox.Text = this.unitValue.ToString();
                    this.componentTabSelected = false;
                }
                else
                {
                    if (!this.tabProcess)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("UnitErrorMsg") + unitLowValue.ToString() + SharedFunctions.GetResourceString("ErrorMsgThrough") + unitHighValue.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.componentTabSelected = true;
                        this.UnitTextBox.Focus();
                    }
                    else
                    {
                        this.UnitTextBox.Focus();
                        this.tabProcess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the StoryHeightTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StoryHeightTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                double storyHeightLowValue = 0.0;
                double storyHeightHighValue = 0.0;
                double.TryParse(this.StoryHeightTextBox.Text.Trim(), out this.storyHeightValue);

                if (this.rangeDataSet.Tables.Count > 0 && this.rangeDataSet.Tables["StoryHeight"].Rows.Count > 0)
                {
                    double.TryParse(this.rangeDataSet.Tables["StoryHeight"].Rows[0]["LowValue"].ToString(), out storyHeightLowValue);
                    double.TryParse(this.rangeDataSet.Tables["StoryHeight"].Rows[0]["HighValue"].ToString(), out storyHeightHighValue);
                }

                if (this.storyHeightValue >= storyHeightLowValue && this.storyHeightValue <= storyHeightHighValue)
                {
                    this.StoryHeightTextBox.Text = this.storyHeightValue.ToString();
                    this.componentTabSelected = false;
                }
                else
                {
                    if (!this.tabProcess)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("StoryHeightErrorMsg") + storyHeightLowValue.ToString() + SharedFunctions.GetResourceString("ErrorMsgThrough") + storyHeightHighValue.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.componentTabSelected = true;
                        this.StoryHeightTextBox.Focus();
                    }
                    else
                    {
                        this.StoryHeightTextBox.Focus();
                        this.tabProcess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the ManufacturedHousingStoryHeightTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ManufacturedHousingStoryHeightTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                double storyHeightLowValue = 0.0;
                double storyHeightHighValue = 0.0;
                double.TryParse(this.ManufacturedHousingStoryHeightTextBox.Text.Trim(), out this.storyHeightValue);

                if (this.rangeDataSet.Tables.Count > 0 && this.rangeDataSet.Tables["StoryHeight"].Rows.Count > 0)
                {
                    double.TryParse(this.rangeDataSet.Tables["StoryHeight"].Rows[0]["LowValue"].ToString(), out storyHeightLowValue);
                    double.TryParse(this.rangeDataSet.Tables["StoryHeight"].Rows[0]["HighValue"].ToString(), out storyHeightHighValue);
                }

                if (this.storyHeightValue >= storyHeightLowValue && this.storyHeightValue <= storyHeightHighValue)
                {
                    this.StoryHeightTextBox.Text = this.storyHeightValue.ToString();
                    this.componentTabSelected = false;
                }
                else
                {
                    if (!this.tabProcess)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("StoryHeightErrorMsg") + storyHeightLowValue.ToString() + SharedFunctions.GetResourceString("ErrorMsgThrough") + storyHeightHighValue.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.componentTabSelected = true;
                        this.ManufacturedHousingStoryHeightTextBox.Focus();
                    }
                    else
                    {
                        this.ManufacturedHousingStoryHeightTextBox.Focus();
                        this.tabProcess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the CostMultiplerTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CostMultiplerTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                double costMultiplerLowValue = 0;
                double costMultiplerHighValue = 0;
                double.TryParse(this.CostMultiplerTextBox.Text.Trim(), out this.costMultiplerValue);

                if (this.rangeDataSet.Tables.Count > 0 && this.rangeDataSet.Tables["LocalMultiplier"].Rows.Count > 0)
                {
                    double.TryParse(this.rangeDataSet.Tables["LocalMultiplier"].Rows[0]["LowValue"].ToString(), out costMultiplerLowValue);
                    double.TryParse(this.rangeDataSet.Tables["LocalMultiplier"].Rows[0]["HighValue"].ToString(), out costMultiplerHighValue);
                }

                if (this.costMultiplerValue >= costMultiplerLowValue && this.costMultiplerValue <= costMultiplerHighValue)
                {
                    this.CostMultiplerTextBox.Text = this.costMultiplerValue.ToString();
                    this.componentTabSelected = false;
                }
                else
                {
                    if (!this.tabProcess)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("CostMultiplerErrorMsg") + costMultiplerLowValue.ToString() + SharedFunctions.GetResourceString("ErrorMsgThrough") + costMultiplerHighValue.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.componentTabSelected = true;
                        this.CostMultiplerTextBox.Focus();
                    }
                    else
                    {
                        this.CostMultiplerTextBox.Focus();
                        this.tabProcess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the PrimaryAreaTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PrimaryAreaTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                // Calculate the Total Area and Percentage
                this.CalculateTotalArea();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the SingleBaseQualityNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SingleBaseQualityNumberTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                // Checks the Quality Value range
                this.CheckQualityRange((TerraScanTextBox)sender);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the BaseQualityNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void BaseQualityNumberTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                // Checks the Quality Value range
                this.CheckQualityRange((TerraScanTextBox)sender);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the BaseQualityNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void BaseQualityNumberTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.BaseQualityNumberTextBox.Text.Trim()) && this.BaseQualityNumberTextBox.Text != ".")
                {
                    double qualityValue = 0.0;
                    double.TryParse(this.BaseQualityNumberTextBox.Text.Trim().ToString(), out qualityValue);
                    this.BaseQuailtyDescriptionLabel.Text = this.BaseQualityDescription(qualityValue);
                }
                else
                {
                    this.BaseQuailtyDescriptionLabel.Text = string.Empty;
                    this.DefaultHeightTextBox.Text = string.Empty;
                    this.ManufacturedHousingDefaultHeightTextBox.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the SingleBaseQualityNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SingleBaseQualityNumberTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.SingleBaseQualityNumberTextBox.Text.Trim()) && this.BaseQualityNumberTextBox.Text != ".")
                {
                    double singleQualityValue = 0.0;
                    double.TryParse(this.SingleBaseQualityNumberTextBox.Text.Trim().ToString(), out singleQualityValue);
                    this.SinglebaseQualityDescriptionLabel.Text = this.BaseQualityDescription(singleQualityValue);
                }
                else
                {
                    this.SinglebaseQualityDescriptionLabel.Text = string.Empty;
                    this.DefaultHeightTextBox.Text = string.Empty;
                    this.ManufacturedHousingDefaultHeightTextBox.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the ManufacturedHousingPrimaryWidthTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ManufacturedHousingPrimaryWidthTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                // Check for Valid Area
                this.CheckHousingPrimaryAreaValidation((TerraScanTextBox)sender);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the ManufacturedHousingPrimaryLengthTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ManufacturedHousingPrimaryLengthTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                // Check for Valid Area
                this.CheckHousingPrimaryAreaValidation((TerraScanTextBox)sender);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the ManufacturedHousingSecondaryWidthTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ManufacturedHousingSecondaryWidthTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.CheckHousingTagalongAreaValidation((TerraScanTextBox)sender);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the ManufacturedHousingSecondaryLengthTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ManufacturedHousingSecondaryLengthTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.CheckHousingTagalongAreaValidation((TerraScanTextBox)sender);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the SecondaryStyleCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SecondaryStyleCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (this.SecondaryStyleCombo.SelectedValue.ToString() != "-1")
                {
                    int secondarySelectedValue = 0;
                    int.TryParse(this.SecondaryStyleCombo.SelectedValue.ToString(), out secondarySelectedValue);

                    this.tagalongSelected = true;
                    this.RemoveEmptyRow(this.SecondaryStyleCombo);
                    this.SecondaryStyleCombo.SelectedValue = secondarySelectedValue;
                    this.SecondaryAreaTextBox.Enabled = true;
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Component Tab

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the ManufacturedHousingTagalongStyleCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ManufacturedHousingTagalongStyleCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (this.ManufacturedHousingTagalongStyleCombo.SelectedValue.ToString() != "-1")
                {
                    int secondarySelectedValue = 0;
                    int.TryParse(this.ManufacturedHousingTagalongStyleCombo.SelectedValue.ToString(), out secondarySelectedValue);

                    this.tagalongSelected = true;
                    this.ManufacturedHousingSecondaryWidthTextBox.Enabled = true;
                    this.ManufacturedHousingSecondaryLengthTextBox.Enabled = true;
                    this.RemoveEmptyRow(this.ManufacturedHousingTagalongStyleCombo);
                    this.ManufacturedHousingTagalongStyleCombo.SelectedValue = secondarySelectedValue;
                    this.ToEnableEditButtonInMasterForm();

                    if (!string.IsNullOrEmpty(this.ManufacturedHousingSecondaryWidthTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.ManufacturedHousingSecondaryLengthTextBox.Text.Trim()))
                    {
                        this.CheckHousingTagalongAreaValidation(this.ManufacturedHousingSecondaryWidthTextBox);
                        this.CheckHousingTagalongAreaValidation(this.ManufacturedHousingSecondaryLengthTextBox);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the ManufacturedHousingPrimaryStyleCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ManufacturedHousingPrimaryStyleCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.ManufacturedHousingPrimaryWidthTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.ManufacturedHousingPrimaryLengthTextBox.Text.Trim()))
                {
                    this.CheckHousingPrimaryAreaValidation(this.ManufacturedHousingPrimaryWidthTextBox);
                    this.CheckHousingPrimaryAreaValidation(this.ManufacturedHousingPrimaryLengthTextBox);
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the TemplateComponentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TemplateComponentButton_Click(object sender, EventArgs e)
        {
            try
            {
                Form componentForm3100 = new Form();
                DataRowCollection selectedRows = null;
                DataTable templateDataTable = new DataTable();
                object[] optionalParameter = new object[] { this.houseTypeXml };
                componentForm3100 = this.form36000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(3100, optionalParameter, this.form36000Control.WorkItem);

                if (componentForm3100 != null)
                {
                    if (componentForm3100.ShowDialog() == DialogResult.OK)
                    {
                        templateDataTable = (DataTable)TerraScanCommon.GetObject(componentForm3100, SharedFunctions.GetResourceString("TemplateDataTable"));

                        if (templateDataTable != null && templateDataTable.Rows.Count >= 0)
                        {
                            selectedRows = templateDataTable.Rows;
                        }

                        foreach (DataRow row in selectedRows)
                        {
                            this.selectedComponentDataTable.ImportRow(row);
                        }

                        this.SelectedComponentGridView.DataSource = this.selectedComponentDataTable;
                        this.FilterComponentRow(1);
                        this.ToEnableEditButtonInMasterForm();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the NewComponentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NewComponentButton_Click(object sender, EventArgs e)
        {
            try
            {
                Form sectionForm3101 = new Form();
                DataSet sectionSelectedDataSet = new DataSet();
                if (this.SectionGridView.OriginalRowCount > 1)
                {
                    int.TryParse(this.sectionDataTable.Rows[this.SectionGridView.OriginalRowCount - 1]["SectionKey"].ToString(), out this.sectionKeyValue);
                }
                else
                {
                    this.sectionKeyValue = 1;
                }

                this.sectionKeyValue = this.sectionKeyValue + 1;
                object[] optionalParameter = new object[] { this.houseTypeXml, this.sectionKeyValue, this.SectionGridView.OriginalRowCount };
                sectionForm3101 = this.form36000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(3101, optionalParameter, this.form36000Control.WorkItem);

                if (sectionForm3101 != null)
                {
                    if (sectionForm3101.ShowDialog() == DialogResult.OK)
                    {
                        DataRowCollection selectedRows = null;
                        this.sectionReturnXml = TerraScanCommon.GetValue(sectionForm3101, SharedFunctions.GetResourceString("SectionReturnValue"));
                        StringReader stringXmlReader = new StringReader(this.sectionReturnXml);
                        XmlTextReader textReaderHouseType = new XmlTextReader(stringXmlReader);
                        sectionSelectedDataSet.ReadXml(textReaderHouseType);
                        if (sectionSelectedDataSet != null && sectionSelectedDataSet.Tables.Count >= 0)
                        {
                            selectedRows = sectionSelectedDataSet.Tables[0].Rows;
                        }

                        ////Remove Empty Rows for Owner
                        if (this.SectionGridView.OriginalRowCount < this.SectionGridView.NumRowsVisible)
                        {
                            DataRow[] emptyRow = this.sectionDataTable.Select(SharedFunctions.GetResourceString("EmptyRecordValidation"));

                            foreach (DataRow empty in emptyRow)
                            {
                                this.sectionDataTable.Rows.Remove(empty);
                            }
                        }

                        foreach (DataRow row in selectedRows)
                        {
                            this.sectionDataTable.ImportRow(row);
                        }

                        this.ToEnableEditButtonInMasterForm();
                        this.PopulateSectionGrid(this.sectionDataTable);
                        this.TemplateButtonStatus();
                    }
                    else
                    {
                        this.sectionKeyValue--;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the DeleteComponentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeleteComponentButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteErrorMsg"), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (this.SectionGridView.CurrentRow.Index >= 1)
                    {
                        this.ToEnableEditButtonInMasterForm();
                        int deleteKeyValue = 0;
                        int.TryParse(this.sectionDataTable.Rows[this.SectionGridView.CurrentRow.Index]["SectionKey"].ToString(), out deleteKeyValue);

                        this.sectionRowDeleted = true;
                        this.sectionDataTable.Rows.RemoveAt(this.SectionGridView.CurrentRow.Index);

                        DataRow[] componentRows;
                        componentRows = this.selectedComponentDataTable.Select("SectionKeyValue =" + "'" + deleteKeyValue + "'");

                        if (componentRows.Length > 0)
                        {
                            foreach (DataRow row in componentRows)
                            {
                                this.selectedComponentDataTable.Rows.Remove(row);
                            }
                        }

                        this.SectionGridView.DataSource = this.sectionDataTable;
                        this.SectionGridView.Rows[this.SectionGridView.OriginalRowCount - 1].Cells[0].Selected = true;
                        if (this.SectionGridView.CurrentRow.Index >= 0)
                        {
                            this.SectionGridClick(this.SectionGridView.CurrentRow.Index);
                        }

                        this.sectionKeyValue--;

                        if (this.SectionGridView.OriginalRowCount > 1)
                        {
                            this.ButtonStatus(true);
                            this.TemplateButtonStatus();
                        }
                        else
                        {
                            this.ButtonStatus(false);
                            this.TemplateButtonStatus();
                        }

                        if (this.SectionGridView.OriginalRowCount > this.SectionGridView.NumRowsVisible)
                        {
                            this.SectionScrollBar.Visible = false;
                        }
                        else
                        {
                            this.SectionScrollBar.Visible = true;
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
        /// Handles the Click event of the EditComponentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EditComponentButton_Click(object sender, EventArgs e)
        {
            try
            {
                int keyValue = 0;
                int size = 0;
                double quality = 0.0;
                string tagalongWdith = string.Empty;
                string tagalongLength = string.Empty;
                string qualityDescription = string.Empty;
                string sectionDescription = string.Empty;
                string returnValue = string.Empty;
                int groupId = 0;
                DataSet editDataSet = new DataSet();
                Form sectionForm3101 = new Form();
                DataRow tempdataRow = null;

                if (this.SectionGridView.CurrentRow.Index >= 0)
                {
                    int.TryParse(this.sectionDataTable.Rows[this.SectionGridView.CurrentRow.Index]["SectionKey"].ToString().Replace(",", ""), out keyValue);
                    int.TryParse(this.sectionDataTable.Rows[this.SectionGridView.CurrentRow.Index]["SquareFeet"].ToString().Replace(",", ""), out size);
                    double.TryParse(this.sectionDataTable.Rows[this.SectionGridView.CurrentRow.Index]["BaseQuality"].ToString().Replace(",", ""), out quality);
                    tagalongWdith = this.sectionDataTable.Rows[this.SectionGridView.CurrentRow.Index]["TagalongWidth"].ToString();
                    tagalongLength = this.sectionDataTable.Rows[this.SectionGridView.CurrentRow.Index]["TagalongLength"].ToString();
                    qualityDescription = this.sectionDataTable.Rows[this.SectionGridView.CurrentRow.Index]["QualityDescription"].ToString();
                    sectionDescription = this.sectionDataTable.Rows[this.SectionGridView.CurrentRow.Index]["Description"].ToString();
                    int.TryParse(this.sectionDataTable.Rows[this.SectionGridView.CurrentRow.Index]["GroupID"].ToString(), out groupId);
                }

                object[] optionalParameter = new object[] { keyValue, size, quality, qualityDescription, sectionDescription, tagalongWdith, tagalongLength, this.houseTypeXml, groupId };
                sectionForm3101 = this.form36000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(3101, optionalParameter, this.form36000Control.WorkItem);

                if (sectionForm3101 != null)
                {
                    if (sectionForm3101.ShowDialog() == DialogResult.OK)
                    {
                        this.ToEnableEditButtonInMasterForm();
                        returnValue = TerraScanCommon.GetValue(sectionForm3101, SharedFunctions.GetResourceString("SectionReturnValue"));
                        StringReader stringXmlReader = new StringReader(returnValue);
                        XmlTextReader textReaderHouseType = new XmlTextReader(stringXmlReader);
                        editDataSet.ReadXml(textReaderHouseType);
                        if (editDataSet != null && editDataSet.Tables.Count >= 0)
                        {
                            tempdataRow = this.sectionDataTable.NewRow();
                            tempdataRow["Description"] = editDataSet.Tables[0].Rows[0]["Description"].ToString();
                            tempdataRow["ResidenceGroupType_Id"] = editDataSet.Tables[0].Rows[0]["ResidenceGroupType_Id"].ToString();
                            tempdataRow["SectionKey"] = editDataSet.Tables[0].Rows[0]["SectionKey"].ToString();
                            tempdataRow["SquareFeet"] = editDataSet.Tables[0].Rows[0]["SquareFeet"].ToString();
                            tempdataRow["BaseQuality"] = editDataSet.Tables[0].Rows[0]["BaseQuality"].ToString();
                            tempdataRow["QualityDescription"] = editDataSet.Tables[0].Rows[0]["QualityDescription"].ToString();
                            tempdataRow["TagalongWidth"] = editDataSet.Tables[0].Rows[0]["TagalongWidth"].ToString();
                            tempdataRow["TagalongLength"] = editDataSet.Tables[0].Rows[0]["TagalongLength"].ToString();
                            tempdataRow["GroupID"] = editDataSet.Tables[0].Rows[0]["GroupID"].ToString();
                        }

                        if (this.SectionGridView.CurrentRow.Index >= 0)
                        {
                            this.sectionRowDeleted = true;
                            this.sectionDataTable.Rows.RemoveAt(this.SectionGridView.CurrentRow.Index);
                            this.sectionDataTable.Rows.InsertAt(tempdataRow, this.SectionGridView.CurrentRow.Index);
                            this.SectionGridView.DataSource = this.sectionDataTable;
                            this.SectionGridView.Rows[this.SectionGridView.CurrentRow.Index - 1].Cells[0].Selected = true;
                        }
                        else
                        {
                            this.SectionGridView.DataSource = this.sectionDataTable;
                            this.SectionGridView.Rows[this.SectionGridView.CurrentRow.Index - 1].Cells[0].Selected = true;
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
        /// Handles the Closed event of the SelectedComponentMenuStrip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ToolStripDropDownClosedEventArgs"/> instance containing the event data.</param>
        private void SelectedComponentMenuStrip_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            try
            {
                if (this.SelectedComponentGridView.ActiveRow != null)
                {
                    this.RestoreRowBackColor(this.SelectedComponentGridView.ActiveRow.Index);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Selecteds the component menu strip_ item clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void SelectedComponentMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                int keyValue = 0;
                int groupId = 0;
                if (e.ClickedItem.Text == SharedFunctions.GetResourceString("Delete"))
                {
                    this.selectedComponentMenuStrip.Visible = false;
                    if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteMenuErrorMsg"), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int.TryParse(this.selectedComponentDataTable.Rows[this.SelectedComponentGridView.ActiveRow.Index]["SectionKeyValue"].ToString(), out keyValue);

                        if (this.SectionGridView.CurrentRowIndex >= 0)
                        {
                            int.TryParse(this.sectionDataTable.Rows[this.SectionGridView.CurrentRowIndex]["GroupID"].ToString(), out groupId);
                        }

                        this.selectedComponentDataTable.Rows.RemoveAt(this.SelectedComponentGridView.ActiveRow.Index);
                        this.SelectedComponentGridView.DataSource = this.selectedComponentDataTable;
                        this.FilterComponentRow(keyValue);
                        this.ToEnableEditButtonInMasterForm();
                        this.SelectedComponentGridView.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
                    }
                }
                else if (e.ClickedItem.Text == SharedFunctions.GetResourceString("Cancel"))
                {
                    if (this.SelectedComponentGridView.ActiveRow != null)
                    {
                        this.RestoreRowBackColor(this.SelectedComponentGridView.ActiveRow.Index);
                    }
                }

                this.selectedComponentMenuStrip.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the SectionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SectionGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= -1)
                {
                    if (e.RowIndex == 0)
                    {
                        this.ButtonStatus(false);

                        if (this.SelectedComponentGridView.Rows.FilteredInRowCount > 0)
                        {
                            this.TemplateComponentButton.Enabled = false;
                        }
                        else
                        {
                            this.TemplateComponentButton.Enabled = true;
                        }
                    }
                    else
                    {
                        this.ButtonStatus(true);
                        this.TemplateComponentButton.Enabled = false;
                    }

                    this.SectionGridClick(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the SectionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SectionGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!this.sectionRowDeleted)
                {
                    if (e.RowIndex >= 0 && e.ColumnIndex >= -1)
                    {
                        if (e.RowIndex == 0)
                        {
                            this.ButtonStatus(false);

                            if (this.SelectedComponentGridView.Rows.FilteredInRowCount > 0)
                            {
                                this.TemplateComponentButton.Enabled = false;
                            }
                            else
                            {
                                this.TemplateComponentButton.Enabled = true && this.PermissionFiled.editPermission;
                            }
                        }
                        else
                        {
                            this.ButtonStatus(true);
                            this.TemplateComponentButton.Enabled = false;
                        }

                        this.SectionGridClick(e.RowIndex);
                    }
                }

                this.sectionRowDeleted = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the SystemGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SystemGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.SystemGridClick(e.RowIndex);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the SystemGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SystemGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= -1)
                {
                    this.SystemGridClick(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the ComponentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ComponentGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= -1)
                {
                    if (e.RowIndex < this.ComponentGridView.OriginalRowCount)
                    {
                        DataGridViewSelectedRowCollection selectedComponentCollection;
                        this.ComponentGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        selectedComponentCollection = this.ComponentGridView.SelectedRows;

                        if (selectedComponentCollection.Count > 0)
                        {
                            this.CreateSelectedComponentDataTable(selectedComponentCollection);
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
        /// Handles the SelectionChangeCommitted event of the PrimaryStyleCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PrimaryStyleCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the ManufacturedHousingPrimaryWidthTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ManufacturedHousingPrimaryWidthTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #region Infragistis

        /// <summary>
        /// Handles the InitializeLayout event of the SelectedComponentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void SelectedComponentGridView_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                // Allow New Row
                e.Layout.Override.AllowAddNew = AllowAddNew.TemplateOnBottom;

                // Max Length
                this.SelectedComponentGridView.DisplayLayout.Bands[0].Columns["Code"].MaxLength = 4;
                this.SelectedComponentGridView.DisplayLayout.Bands[0].Columns["Units"].MaxLength = 8;
                this.SelectedComponentGridView.DisplayLayout.Bands[0].Columns["Percentage"].MaxLength = 3;
                this.SelectedComponentGridView.DisplayLayout.Bands[0].Columns["QualityID"].MaxLength = 4;

                // Assiging Width
                this.SelectedComponentGridView.DisplayLayout.Bands[0].Columns["Percentage"].Header.Caption = "%";
                this.SelectedComponentGridView.DisplayLayout.Bands[0].Columns["Code"].Width = 44;
                this.SelectedComponentGridView.DisplayLayout.Bands[0].Columns["SelectedSystem"].Width = 122;
                this.SelectedComponentGridView.DisplayLayout.Bands[0].Columns["SelectedSystemDescription"].Width = 250;
                this.SelectedComponentGridView.DisplayLayout.Bands[0].Columns["Units"].Width = 50;
                this.SelectedComponentGridView.DisplayLayout.Bands[0].Columns["Percentage"].Width = 50;
                this.SelectedComponentGridView.DisplayLayout.Bands[0].Columns["QualityID"].Width = 46;
                this.SelectedComponentGridView.DisplayLayout.Bands[0].Columns["QualityDescription"].Width = 140;

                this.SelectedComponentGridView.DisplayLayout.Bands[0].Columns["AllowQualityChangeFlag"].Width = 0;
                this.SelectedComponentGridView.DisplayLayout.Bands[0].Columns["PercentMaximum"].Width = 0;
                this.SelectedComponentGridView.DisplayLayout.Bands[0].Columns["PercentMinimum"].Width = 0;
                this.SelectedComponentGridView.DisplayLayout.Bands[0].Columns["UnitMaximum"].Width = 0;
                this.SelectedComponentGridView.DisplayLayout.Bands[0].Columns["UnitMinimum"].Width = 0;
                this.SelectedComponentGridView.DisplayLayout.Bands[0].Columns["SectionKeyValue"].Width = 0;

                this.SelectedComponentGridView.DisplayLayout.Bands[0].Columns["QualityID"].Format = "$#,##0.00";
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the InitializeRow event of the SelectedComponentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeRowEventArgs"/> instance containing the event data.</param>
        private void SelectedComponentGridView_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            try
            {
                if (!this.executionRequied)
                {
                    if (e.Row != null)
                    {
                        this.SelectedComponentGridView.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.TemplateOnBottom;

                        e.Row.Cells["SelectedSystem"].Activation = Activation.Disabled;
                        e.Row.Cells["SelectedSystem"].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);
                        e.Row.Cells["SelectedSystem"].Appearance.ForeColorDisabled = Color.Black;
                        e.Row.Cells["SelectedSystem"].Appearance.ForeColor = Color.Black;
                        e.Row.Cells["SelectedSystemDescription"].Activation = Activation.Disabled;
                        e.Row.Cells["SelectedSystemDescription"].Appearance.ForeColorDisabled = Color.Black;
                        e.Row.Cells["SelectedSystemDescription"].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);
                        e.Row.Cells["QualityDescription"].Activation = Activation.Disabled;
                        e.Row.Cells["QualityDescription"].Appearance.ForeColorDisabled = Color.Black;
                        e.Row.Cells["QualityDescription"].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);

                        if (e.Row.Cells["UnitMaximum"].Value.ToString() == "0" || e.Row.Cells["UnitMaximum"].Value == DBNull.Value || string.IsNullOrEmpty(e.Row.Cells["UnitMaximum"].Value.ToString()))
                        {
                            e.Row.Cells["Units"].Activation = Activation.Disabled;
                            e.Row.Cells["Units"].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);
                            e.Row.Cells["Units"].Appearance.ForeColorDisabled = Color.Black;
                            e.Row.Cells["Units"].Appearance.ForeColor = Color.Black;
                        }
                        else
                        {
                            e.Row.Cells["Units"].Activation = Activation.AllowEdit;
                            e.Row.Cells["Units"].Appearance.BackColor = Color.White;
                        }

                        if (e.Row.Cells["PercentMaximum"].Value.ToString() == "0" || e.Row.Cells["PercentMaximum"].Value == DBNull.Value || string.IsNullOrEmpty(e.Row.Cells["PercentMaximum"].Value.ToString()))
                        {
                            e.Row.Cells["Percentage"].Activation = Activation.Disabled;
                            e.Row.Cells["Percentage"].Appearance.ForeColorDisabled = Color.Black;
                            e.Row.Cells["Percentage"].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);
                        }
                        else
                        {
                            e.Row.Cells["Percentage"].Activation = Activation.AllowEdit;
                            e.Row.Cells["Percentage"].Appearance.BackColor = Color.White;
                        }

                        if (e.Row.Cells["AllowQualityChangeFlag"].Value.ToString().ToLower().Trim() == "false" || e.Row.Cells["PercentMaximum"].Value == DBNull.Value || string.IsNullOrEmpty(e.Row.Cells["PercentMaximum"].Value.ToString()))
                        {
                            e.Row.Cells["QualityID"].Activation = Activation.Disabled;
                            e.Row.Cells["QualityID"].Appearance.ForeColorDisabled = Color.Black;
                            e.Row.Cells["QualityID"].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);
                        }
                        else
                        {
                            e.Row.Cells["QualityID"].Activation = Activation.AllowEdit;
                            e.Row.Cells["QualityID"].Appearance.BackColor = Color.White;
                        }
                    }
                }

                this.executionRequied = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeExitEditMode event of the SelectedComponentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs"/> instance containing the event data.</param>
        private void SelectedComponentGridView_BeforeExitEditMode(object sender, BeforeExitEditModeEventArgs e)
        {
            try
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.SelectedComponentGridView.ActiveRow;
                Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.SelectedComponentGridView.ActiveCell;

                if (activeCell != null)
                {
                    if (activeCell.Column.Index == 0)
                    {
                        if (!string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                        {
                            this.eventFired = true;
                            string expression = string.Empty;
                            DataTable expressionDataTable = new DataTable();
                            int.TryParse(activeCell.Text.Trim().ToString(), out this.typedCodeValue);
                            expression = this.CheckComponentCode().ToString();
                            DataRow[] expressionCode;
                            expressionCode = this.houseTypeCollectionDataSet.Tables["ResidenceComponent"].Select(expression);
                            expressionDataTable = this.DataRowToDataTable(expressionCode);

                            DataRow[] typedCode;
                            typedCode = expressionDataTable.Select("Key =" + "'" + this.typedCodeValue + "'");

                            if (typedCode.Length == 0)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("ValidCode"), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentFieldValid = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                this.componentFieldValid = false;
                            }
                        }
                        else
                        {
                            if (activeRow.IsAddRow)
                            {
                                this.componentFieldValid = false;
                                this.SelectedComponentGridView.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("ValidCode"), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentFieldValid = true;
                                activeCell.SelText = activeRow.Cells["Code"].Value.ToString();
                                this.SelectedComponentGridView.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
                                e.Cancel = true;
                            }
                        }
                    }

                    if (activeCell.Column.Index == 3)
                    {
                        if (!string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                        {
                            this.eventFired = true;
                            int unitCurrentValue = 0;
                            int unitMaxValue = 0;
                            int unitMinValue = 0;

                            try
                            {
                                unitCurrentValue = Convert.ToInt32(activeCell.Text.Trim().ToString());
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("UnitRange") + activeRow.Cells["UnitMinimum"].Value.ToString() + " through " + activeRow.Cells["UnitMaximum"].Value.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentFieldValid = true;
                                e.Cancel = true;
                                return;
                            }

                            int.TryParse(activeRow.Cells["UnitMinimum"].Value.ToString(), out unitMinValue);
                            int.TryParse(activeRow.Cells["UnitMaximum"].Value.ToString(), out unitMaxValue);

                            if (unitCurrentValue < unitMinValue || unitCurrentValue > unitMaxValue)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("UnitRange") + activeRow.Cells["UnitMinimum"].Value.ToString() + " through " + activeRow.Cells["UnitMaximum"].Value.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentFieldValid = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                this.componentFieldValid = false;
                            }
                        }
                        else
                        {
                            this.componentFieldValid = false;
                        }
                    }

                    if (activeCell.Column.Index == 4)
                    {
                        if (!string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                        {
                            this.eventFired = true;
                            int percentageValue = 0;
                            int percentageMaxValue = 0;
                            int percentageMinValue = 0;

                            try
                            {
                                percentageValue = Convert.ToInt32(activeCell.Text.Trim().ToString());
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("PercentRange") + activeRow.Cells["PercentMinimum"].Value.ToString() + " through " + activeRow.Cells["PercentMaximum"].Value.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentFieldValid = true;
                                e.Cancel = true;
                                return;
                            }

                            int.TryParse(activeRow.Cells["PercentMinimum"].Value.ToString(), out percentageMinValue);
                            int.TryParse(activeRow.Cells["PercentMaximum"].Value.ToString(), out percentageMaxValue);

                            if (percentageValue < percentageMinValue || percentageValue > percentageMaxValue)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("PercentRange") + activeRow.Cells["PercentMinimum"].Value.ToString() + " through " + activeRow.Cells["PercentMaximum"].Value.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentFieldValid = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                this.componentFieldValid = false;
                            }
                        }
                        else
                        {
                            this.componentFieldValid = false;
                        }
                    }

                    if (activeCell.Column.Index == 5)
                    {
                        double qualityValue = 0.0;

                        if (!string.IsNullOrEmpty(activeCell.Text.Trim().ToString()) && activeCell.Text.Trim() != ".")
                        {
                            this.eventFired = true;
                            try
                            {
                                double.TryParse(activeCell.Text.Trim().ToString(), out qualityValue);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("QualityRange") + this.lowQuality + " through " + this.highQuality, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentFieldValid = true;
                                e.Cancel = true;
                                return;
                            }

                            if (qualityValue >= this.lowQuality && qualityValue <= this.highQuality)
                            {
                                activeCell.Value = qualityValue.ToString("##0.00");
                                this.componentFieldValid = false;
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("QualityRange") + this.lowQuality + " through " + this.highQuality, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentFieldValid = true;
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            this.SelectedComponentGridView.Rows[activeRow.VisibleIndex].Cells["QualityDescription"].Value = string.Empty;
                            this.componentFieldValid = false;
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
        /// Handles the KeyUp event of the SelectedComponentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SelectedComponentGridView_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.SelectedComponentGridView.ActiveRow;
                Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.SelectedComponentGridView.ActiveCell;
                this.ToEnableEditButtonInMasterForm();

                if (activeCell != null)
                {
                    if (activeCell.Column.Index == 0)
                    {
                        if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                        {
                            if (string.IsNullOrEmpty(activeCell.Text.ToString()))
                            {
                                if (this.SectionGridView.CurrentRow.Index >= 0)
                                {
                                    int tempValue = 0;
                                    int.TryParse(this.sectionDataTable.Rows[this.SectionGridView.CurrentRow.Index]["SectionKey"].ToString(), out tempValue);

                                    if (activeRow.Index == this.SelectedComponentGridView.Rows.Count - 1)
                                    {
                                        this.SelectedComponentGridView.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.No;
                                    }
                                }
                            }
                        }
                        else if (e.KeyCode == Keys.Tab)
                        {
                            if (activeRow.IsAddRow)
                            {
                                this.SelectedComponentGridView.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.No;
                                this.SelectedComponentGridView.DisplayLayout.TabNavigation = TabNavigation.NextControlOnLastCell;
                            }
                        }
                        else
                        {
                            this.SelectedComponentGridView.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
                        }
                    }

                    if (activeCell.Column.Index == 3)
                    {
                        if (e.KeyCode != Keys.Tab && e.KeyCode != Keys.ShiftKey)
                        {
                            this.SelectedComponentGridView.Rows[activeCell.Row.Index].Cells["Percentage"].Value = DBNull.Value;
                        }
                    }

                    if (activeCell.Column.Index == 4)
                    {
                        if (e.KeyCode != Keys.Tab && e.KeyCode != Keys.ShiftKey)
                        {
                            this.SelectedComponentGridView.Rows[activeCell.Row.Index].Cells["Units"].Value = string.Empty;
                        }
                    }

                    if (activeCell.Column.Index == 5)
                    {
                        if (!string.IsNullOrEmpty(activeCell.Text) && activeCell.Text.Trim() != ".")
                        {
                            double qualityValue = 0.0;
                            double.TryParse(activeCell.Text, out qualityValue);

                            if (qualityValue >= this.lowQuality && qualityValue <= this.highQuality)
                            {
                                this.SelectedComponentGridView.Rows[activeCell.Row.Index].Cells["QualityDescription"].Value = this.BaseQualityDescription(qualityValue);
                            }
                            else
                            {
                                this.SelectedComponentGridView.Rows[activeCell.Row.Index].Cells["QualityDescription"].Value = string.Empty;
                            }
                        }
                        else
                        {
                            this.SelectedComponentGridView.Rows[activeCell.Row.Index].Cells["QualityDescription"].Value = string.Empty;
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
        /// Handles the AfterCellUpdate event of the SelectedComponentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.CellEventArgs"/> instance containing the event data.</param>
        private void SelectedComponentGridView_AfterCellUpdate(object sender, CellEventArgs e)
        {
            try
            {
                string expression = string.Empty;
                DataTable expressionDataTable = new DataTable();
                UltraGridCell activeCell = this.SelectedComponentGridView.ActiveCell;

                if (activeCell != null)
                {
                    if (e.Cell.Column.Index == 0)
                    {
                        if (!string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                        {
                            int.TryParse(activeCell.Text.Trim().ToString(), out this.typedCodeValue);
                            expression = this.CheckComponentCode().ToString();

                            DataRow[] expressionCode;
                            expressionCode = this.houseTypeCollectionDataSet.Tables["ResidenceComponent"].Select(expression);
                            expressionDataTable = this.DataRowToDataTable(expressionCode);
                            DataRow[] typedCode;
                            typedCode = expressionDataTable.Select("Key =" + "'" + this.typedCodeValue + "'");

                            if (typedCode.Length > 0)
                            {
                                this.SelectedComponentGridView.UpdateData();
                                this.CreateTypedComponentDataTable(typedCode);
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
        /// Handles the MouseUp event of the SelectedComponentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void SelectedComponentGridView_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (!this.componentTabSelected)
                {
                    Infragistics.Win.UIElement elementPoint = this.SelectedComponentGridView.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));

                    if (elementPoint != null)
                    {
                        UltraGridRow activeRow = (UltraGridRow)elementPoint.GetContext(typeof(UltraGridRow));
                        UltraGridCell activeCell = (UltraGridCell)elementPoint.GetContext(typeof(UltraGridCell));

                        if (activeCell != null)
                        {
                            if (activeRow.Index <= this.SelectedComponentGridView.Rows.Count && activeRow.Index != -1 && !string.IsNullOrEmpty(this.SelectedComponentGridView.Rows[activeRow.Index].Cells["Code"].Value.ToString()))
                            {
                                if (e.Button == MouseButtons.Right)
                                {
                                    this.SelectedComponentGridView.Rows[activeRow.Index].Activate();
                                    this.ChangeRowBackColor(activeRow.Index);
                                    this.selectedComponentMenuStrip.Show(this.SelectedComponentGridView, new Point(e.X, e.Y));
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

        #endregion

        #endregion

        #region Calculate RCN

        /// <summary>
        /// Validates the style.
        /// </summary>
        /// <returns>bool</returns>
        private bool ValidateStyle()
        {
            if (this.tagalongSelected)
            {
                if (this.houseType == (int)HouseType.SingleFamilyResidence || this.houseType == (int)HouseType.LowRiseMultiple || this.houseType == (int)HouseType.TownHouseEndUnit || this.houseType == (int)HouseType.TownHouseInsideUnit || this.houseType == (int)HouseType.Duplex)
                {
                    if (string.IsNullOrEmpty(this.PrimaryAreaTextBox.Text.Trim()) || this.PrimaryAreaTextBox.Text.Trim() == "0")
                    {
                        MessageBox.Show("PrimaryArea should not be zero.", SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (string.IsNullOrEmpty(this.SecondaryAreaTextBox.Text.Trim()) || this.SecondaryAreaTextBox.Text.Trim() == "0")
                    {
                        MessageBox.Show("SecondaryArea should not be zero.", SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    return true;
                }
                else
                {
                    if (string.IsNullOrEmpty(this.ManufacturedHousingPrimaryAreaTextBox.Text.Trim()) || this.ManufacturedHousingPrimaryAreaTextBox.Text.Trim() == "0")
                    {
                        MessageBox.Show("PrimaryArea should not be zero.", SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (string.IsNullOrEmpty(this.ManufacturedHousingSecondaryAreaTextBox.Text.Trim()) || this.ManufacturedHousingSecondaryAreaTextBox.Text.Trim() == "0")
                    {
                        MessageBox.Show("TagalongArea should not be zero.", SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Handles the Click event of the CalculateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CalculateButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Check the TotalArea value Range
                if (this.ValidateTotalArea())
                {
                    // Check the Style value Range
                    if (this.ValidateStyle())
                    {
                        // Checks for ExteriorWall and Roofing components
                        if (this.CheckMainComponent())
                        {
                            DataRow[] rcnComponent;

                            this.Cursor = Cursors.WaitCursor;
                            if (this.estimateIdValue > 0)
                            {
                                this.editModeOnSave = true;
                            }
                            else
                            {
                                this.editModeOnSave = false;
                            }

                            rcnComponent = this.selectedComponentDataTable.Select("SectionKeyValue = 1");

                            if (rcnComponent.Length > 0)
                            {
                                this.rcnCalculated = true;
                                string componentXml = string.Empty;
                                string sectionXml = string.Empty;
                                componentXml = TerraScanCommon.GetXmlString(this.selectedComponentDataTable);
                                sectionXml = this.RemoveSectionXmlEmptyRow();

                                // Populate the values into HashTable to CalculateRCN
                                this.SaveElements();
                                this.calculatedRcn = this.residenceService.F36000_CalculateRCN(this.saveFieldHashTable, componentXml, sectionXml, this.editModeOnSave);
                                if (!string.IsNullOrEmpty(this.calculatedRcn))
                                {
                                    this.CalculateRcnTextBox.Text = this.calculatedRcn;
                                    this.CalculateDeprProcess();
                                }

                                ////this.saveFieldHashTable.Clear();
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("ExteriorWallsRoofingMissing"), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
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

        #endregion Calculate RCN

        #region Depreciation Tab

        /// <summary>
        /// Handles the CheckedChanged event of the ByObjectCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ByObjectCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.CalculateDeprProcess();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the PropertyQualityTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PropertyQualityTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                int quality = 0;
                int deprId = 0;
                int.TryParse(this.PropertyQualityTextBox.Text, out quality);

                if (quality >= 1 && quality <= 6)
                {
                    deprId = this.form36000Control.WorkItem.GetDeprTableNameId(this.valueSliceId, quality);
                    this.DeprTableIDTextBox.Text = deprId.ToString();
                    DataRow[] tempRow = this.xmlCollectionDataSet.ListDeprTable.Select("DeprTableID = " + deprId);

                    if (tempRow.Length > 0)
                    {
                        this.DepreciationTableTextBox.Text = tempRow[0].ItemArray[1].ToString();
                    }

                    this.CalculateDeprProcess();
                    this.componentTabSelected = false;
                }
                else
                {
                    if (!this.tabProcess)
                    {
                        MessageBox.Show("Property Quality must be 1 " + SharedFunctions.GetResourceString("ErrorMsgThrough") + "6", SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.componentTabSelected = true;
                        this.PropertyQualityTextBox.Focus();
                    }
                    else
                    {
                        this.PropertyQualityTextBox.Focus();
                        this.tabProcess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the DeprYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeprYearTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                int deprYear = 0;
                int ageValue = 0;

                int.TryParse(this.DeprYearTextBox.Text, out deprYear);
                ageValue = this.rollYear - deprYear;

                if (ageValue >= 0 && ageValue <= 100)
                {
                    this.AgeTextBox.Text = ageValue.ToString();
                    this.CalculateDeprProcess();
                    this.componentTabSelected = false;
                }
                else
                {
                    if (!this.tabProcess)
                    {
                        MessageBox.Show("Age must be 0 " + SharedFunctions.GetResourceString("ErrorMsgThrough") + "100", SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.componentTabSelected = true;
                        this.DeprYearTextBox.Focus();
                    }
                    else
                    {
                        this.DeprYearTextBox.Focus();
                        this.tabProcess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the ConditionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ConditionTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!this.escKeyPressed)
                {
                    double conditionValue = 0.0;
                    double.TryParse(this.ConditionTextBox.Text, out conditionValue);

                    if (conditionValue >= 1.0 && conditionValue <= 6.0)
                    {
                        this.ConditionTextBox.Text = conditionValue.ToString();
                        this.CalculateDeprProcess();
                        this.componentTabSelected = false;
                    }
                    else
                    {
                        if (!this.tabProcess)
                        {
                            MessageBox.Show("Condition must be 1.0 " + SharedFunctions.GetResourceString("ErrorMsgThrough") + "6.0", SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.componentTabSelected = true;
                            this.ConditionTextBox.Focus();
                        }
                        else
                        {
                            this.ConditionTextBox.Focus();
                            this.tabProcess = false;
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
        /// Handles the TextChanged event of the DeprYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeprYearTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the ConditionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ConditionTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the PropertyQualityTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PropertyQualityTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the DeprTableComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeprTableComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();
                this.CalculateDeprProcess();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckStateChanged event of the ByObjectCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ByObjectCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #endregion Events

        #region Methods

        #region Common Methods

        /// <summary>
        /// Validates the area.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.TabControlCancelEventArgs"/> instance containing the event data.</param>
        private void ValidateArea(TabControlCancelEventArgs e)
        {
            if (this.tagalongSelected)
            {
                if (this.houseType == (int)HouseType.SingleFamilyResidence || this.houseType == (int)HouseType.LowRiseMultiple || this.houseType == (int)HouseType.TownHouseEndUnit || this.houseType == (int)HouseType.TownHouseInsideUnit || this.houseType == (int)HouseType.Duplex)
                {
                    if (string.IsNullOrEmpty(this.PrimaryAreaTextBox.Text.Trim()) || this.PrimaryAreaTextBox.Text.Trim() == "0")
                    {
                        MessageBox.Show("PrimaryArea should not be zero.", SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.Cancel = true;
                        return;
                    }

                    if (string.IsNullOrEmpty(this.SecondaryAreaTextBox.Text.Trim()) || this.SecondaryAreaTextBox.Text.Trim() == "0")
                    {
                        MessageBox.Show("SecondaryArea should not be zero.", SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.Cancel = true;
                        return;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(this.ManufacturedHousingPrimaryAreaTextBox.Text.Trim()) || this.ManufacturedHousingPrimaryAreaTextBox.Text.Trim() == "0")
                    {
                        MessageBox.Show("PrimaryArea should not be zero.", SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.Cancel = true;
                        return;
                    }

                    if (string.IsNullOrEmpty(this.ManufacturedHousingSecondaryAreaTextBox.Text.Trim()) || this.ManufacturedHousingSecondaryAreaTextBox.Text.Trim() == "0")
                    {
                        MessageBox.Show("TagalongArea should not be zero.", SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        /* /// <summary>
        /// Controls the lock.
        /// </summary>
        /// <param name="status">if set to <c>true</c> [status].</param>
        private void ControlLock(bool status)
        {
            this.InformationSectionPanel.Enabled = !status;
            this.ManufacturedHousingMainPanel.Enabled = !status;
            this.AdjustmentPanel.Enabled = !status;
            this.CostMultiplerPanel.Enabled = !status;
            this.BaseQualityPanel.Enabled = !status;
            this.WallEnergyAdjustmentPanel.Enabled = !status;
            this.CalculateButton.Enabled = !status;
            this.CalculatePanel.Enabled = !status;
        } */

        /// <summary>
        /// Calculates the primary secondary area.
        /// </summary>
        /// <param name="totalArea">The total area.</param>
        /// <param name="percentage">The percentage.</param>
        /// <returns>double</returns>
        private double CalculatePrimarySecondaryArea(double totalArea, double percentage)
        {
            double area = 0.0;
            area = (totalArea * percentage) / 100;
            return area;
        }

        /// <summary>
        /// LoadEstimateValue
        /// </summary>
        private void LoadEstimateValue()
        {
            if (this.estimateHashtable.Count > 0)
            {
                // Local Variables for estimateid values
                int estimateConstType = 0;
                int estimateHouseType = 0;
                int estimatePrimaryStyleKey = 0;
                int estimateSecondaryStyleKey = 0;
                int estimateEnergyZone = 0;
                int estimateFoundationZone = 0;
                int estimateHillsideZone = 0;
                int estimateSeismicZone = 0;
                int estimateWindZone = 0;
                int estimateWallEnergyZone = 0;
                int totalArea = 0;
                double primaryPercentage = 0.0;
                double secondaryPercentage = 0.0;
                int primaryWidth = 0;
                int primaryLength = 0;
                int secondaryWidth = 0;
                int secondaryLength = 0;
                double costMultiplierValue = 0.0;

                // Construction Type
                int.TryParse(this.estimateHashtable["ConstructionType"].ToString(), out estimateConstType);

                // House Type
                int.TryParse(this.estimateHashtable["HouseType"].ToString(), out estimateHouseType);

                // RCN
                if (this.estimateHashtable["RCNValue"] != null)
                {
                    this.rcnCalculated = true;
                    this.CalculateRcnTextBox.Text = this.estimateHashtable["RCNValue"].ToString();
                }
                else
                {
                    this.CalculateRcnTextBox.Text = string.Empty;
                }

                // Story Height
                double stroyHeight = 0.0;

                if (estimateConstType == 1)
                {
                    // Total Area
                    this.TotalAreaTextBox.Text = this.estimateHashtable["FloorArea"].ToString();
                    int.TryParse(this.estimateHashtable["FloorArea"].ToString(), out totalArea);
                    this.totalFloorArea = totalArea;

                    // PrimaryStyle                    
                    int.TryParse(this.estimateHashtable["PrimaryStyleValue"].ToString(), out estimatePrimaryStyleKey);
                    this.PrimaryStyleCombo.SelectedValue = estimatePrimaryStyleKey;

                    // PrimaryStylePercentage 
                    double.TryParse(this.estimateHashtable["PrimaryStylePercent"].ToString(), out primaryPercentage);
                    this.PrimaryPercentageTextBox.Text = Convert.ToString(Math.Round(primaryPercentage));
                    this.PrimaryAreaTextBox.Text = (Math.Round(this.CalculatePrimarySecondaryArea(totalArea, primaryPercentage))).ToString("#,##0");

                    if (primaryPercentage != 100)
                    {
                        // SecondaryStyle                    
                        int.TryParse(this.estimateHashtable["SecondaryStyleKey"].ToString(), out estimateSecondaryStyleKey);
                        this.SecondaryStyleCombo.SelectedValue = estimateSecondaryStyleKey;

                        // SecondaryStylePercentage 
                        double.TryParse(this.estimateHashtable["SecondaryStylePercent"].ToString(), out secondaryPercentage);
                        this.rcnCalculated = true;
                        this.SecondaryPercentageTextBox.Text = Convert.ToString(Math.Round(secondaryPercentage));
                        this.SecondaryAreaTextBox.Text = (Math.Round(this.CalculatePrimarySecondaryArea(totalArea, secondaryPercentage))).ToString("#,##0");
                        this.SecondaryAreaTextBox.Enabled = true;
                        this.tagalongSelected = true;
                    }
                    else
                    {
                        this.LoadTagalongStyle(this.SecondaryStyleCombo);
                        this.rcnCalculated = true;
                        this.SecondaryPercentageTextBox.Text = string.Empty;
                        this.SecondaryAreaTextBox.Text = string.Empty;
                        this.SecondaryAreaTextBox.Enabled = false;
                    }

                    // Unit                     
                    if (estimateHouseType == 2)
                    {
                        this.UnitTextBox.Text = this.estimateHashtable["LowRiseMultiplieUnits"].ToString();
                    }

                    // Base Quality
                    double baseQuality = 0.0;
                    double.TryParse(this.estimateHashtable["QualityValue"].ToString(), out baseQuality);

                    if (baseQuality > 0.0)
                    {
                        this.SingleBaseQualityNumberTextBox.Text = this.estimateHashtable["QualityValue"].ToString();
                        this.SinglebaseQualityDescriptionLabel.Text = this.estimateHashtable["QualityDescription"].ToString();
                    }
                    else
                    {
                        this.SingleBaseQualityNumberTextBox.Text = string.Empty;
                        this.SinglebaseQualityDescriptionLabel.Text = string.Empty;
                    }

                    // Stroy Height
                    double.TryParse(this.estimateHashtable["StoryHeight"].ToString(), out stroyHeight);

                    if (stroyHeight > 0.0)
                    {
                        this.StoryHeightTextBox.Text = this.estimateHashtable["StoryHeight"].ToString();
                    }
                    else
                    {
                        this.StoryHeightTextBox.EmptyDecimalValue = true;
                        this.StoryHeightTextBox.Text = string.Empty;
                    }

                    // Adjustments
                    // EnergyZone
                    int.TryParse(this.estimateHashtable["EnergyAdjustmentKey"].ToString(), out estimateEnergyZone);
                    this.EnergyAdjustmentComboBox.SelectedValue = estimateEnergyZone;

                    // FoundationZone
                    int.TryParse(this.estimateHashtable["FoundationAdjustmentKey"].ToString(), out estimateFoundationZone);
                    this.FoundationAdjustmentCombo.SelectedValue = estimateFoundationZone;

                    // HillSideZone
                    int.TryParse(this.estimateHashtable["HillSideAdjustmentKey"].ToString(), out estimateHillsideZone);
                    this.HillSideAdjustmentCombo.SelectedValue = estimateHillsideZone;

                    // SeismicZone
                    int.TryParse(this.estimateHashtable["SeismicAdjustmentKey"].ToString(), out estimateSeismicZone);
                    this.SeismicAdjustmentCombo.SelectedValue = estimateSeismicZone;

                    // WindZone
                    int.TryParse(this.estimateHashtable["WindAdjustmentKey"].ToString(), out estimateWindZone);
                    this.WindAdjustmentCombo.SelectedValue = estimateWindZone;

                    // CostMultiplier
                    double.TryParse(this.estimateHashtable["CostMultipler"].ToString(), out costMultiplierValue);
                    if (costMultiplierValue > 0.0)
                    {
                        this.CostMultiplerTextBox.Text = this.estimateHashtable["CostMultipler"].ToString();
                    }
                    else
                    {
                        this.CostMultiplerTextBox.EmptyDecimalValue = true;
                        this.CostMultiplerTextBox.Text = string.Empty;
                    }
                }
                else
                {
                    // Total Area
                    int.TryParse(this.estimateHashtable["FloorArea"].ToString(), out totalArea);
                    this.ManufacturedHousingTotalAreaTextBox.Text = this.estimateHashtable["FloorArea"].ToString();
                    this.totalFloorArea = totalArea;

                    // PrimaryStyle                    
                    int.TryParse(this.estimateHashtable["PrimaryStyleKey"].ToString(), out estimatePrimaryStyleKey);
                    this.ManufacturedHousingPrimaryStyleCombo.SelectedValue = estimatePrimaryStyleKey;

                    // Primary Length and Width
                    this.ManufacturedHousingPrimaryWidthTextBox.Text = this.estimateHashtable["PrimaryStyleWidth"].ToString();
                    this.ManufacturedHousingPrimaryLengthTextBox.Text = this.estimateHashtable["PrimaryStyleLength"].ToString();
                    int.TryParse(this.estimateHashtable["PrimaryStyleWidth"].ToString(), out primaryWidth);
                    int.TryParse(this.estimateHashtable["PrimaryStyleLength"].ToString(), out primaryLength);

                    // PrimaryStylePercentage 
                    double.TryParse(this.estimateHashtable["PrimaryStylePercent"].ToString(), out primaryPercentage);
                    this.ManufacturedHousingPrimaryPercentageTextBox.Text = Convert.ToString(Math.Round(primaryPercentage));
                    this.ManufacturedHousingPrimaryAreaTextBox.Text = (primaryWidth * primaryLength).ToString("#,##0");

                    if (primaryPercentage != 100)
                    {
                        // SecondaryStyle                    
                        int.TryParse(this.estimateHashtable["SecondaryStyleKey"].ToString(), out estimateSecondaryStyleKey);
                        this.ManufacturedHousingTagalongStyleCombo.SelectedValue = estimateSecondaryStyleKey;

                        // Secondary Length and Width
                        this.ManufacturedHousingSecondaryWidthTextBox.Text = this.estimateHashtable["SecondaryStyleWidth"].ToString();
                        this.ManufacturedHousingSecondaryLengthTextBox.Text = this.estimateHashtable["SecondaryStyleLength"].ToString();
                        int.TryParse(this.estimateHashtable["SecondaryStyleWidth"].ToString(), out secondaryWidth);
                        int.TryParse(this.estimateHashtable["SecondaryStyleLength"].ToString(), out secondaryLength);

                        // SecondaryStylePercentage 
                        double.TryParse(this.estimateHashtable["SecondaryStylePercent"].ToString(), out secondaryPercentage);
                        this.ManufacturedHousingSecondaryPercentageTextBox.Text = Convert.ToString(Math.Round(secondaryPercentage));
                        this.ManufacturedHousingSecondaryAreaTextBox.Text = (secondaryWidth * secondaryLength).ToString("#,##0");
                        this.ManufacturedHousingSecondaryWidthTextBox.Enabled = true;
                        this.ManufacturedHousingSecondaryLengthTextBox.Enabled = true;
                        this.tagalongSelected = true;
                    }
                    else
                    {
                        this.LoadTagalongStyle(this.ManufacturedHousingTagalongStyleCombo);
                        this.ManufacturedHousingSecondaryWidthTextBox.Text = string.Empty;
                        this.ManufacturedHousingSecondaryLengthTextBox.Text = string.Empty;
                        this.ManufacturedHousingSecondaryPercentageTextBox.Text = string.Empty;
                        this.ManufacturedHousingSecondaryAreaTextBox.Text = string.Empty;
                        this.ManufacturedHousingSecondaryWidthTextBox.Enabled = false;
                        this.ManufacturedHousingSecondaryLengthTextBox.Enabled = false;
                    }

                    // Story Height
                    double.TryParse(this.estimateHashtable["StoryHeight"].ToString(), out stroyHeight);

                    if (stroyHeight > 0.0)
                    {
                        this.ManufacturedHousingStoryHeightTextBox.Text = this.estimateHashtable["StoryHeight"].ToString();
                    }
                    else
                    {
                        this.ManufacturedHousingStoryHeightTextBox.Text = string.Empty;
                    }

                    // Base Quality
                    this.BaseQualityNumberTextBox.Text = this.estimateHashtable["QualityValue"].ToString();
                    this.BaseQuailtyDescriptionLabel.Text = this.estimateHashtable["QualityDescription"].ToString();

                    // WindZone
                    int.TryParse(this.estimateHashtable["WallEnergyAdjustmentKey"].ToString(), out estimateWallEnergyZone);
                    this.WallEnergyAdjustmentCombo.SelectedValue = estimateWallEnergyZone;

                    // CostMultiplier
                    double.TryParse(this.estimateHashtable["CostMultipler"].ToString(), out costMultiplierValue);
                    if (costMultiplierValue > 0.0)
                    {
                        this.CostMultiplerTextBox.Text = this.estimateHashtable["CostMultipler"].ToString();
                    }
                    else
                    {
                        this.CostMultiplerTextBox.EmptyDecimalValue = true;
                        this.CostMultiplerTextBox.Text = string.Empty;
                    }
                }

                // Populate Section grid
                this.SectionGridView.DataSource = this.SelectedGroupEstimateValue();
                if (this.SectionGridView.OriginalRowCount > 1)
                {
                    this.ButtonStatus(true);
                }
                else
                {
                    this.ButtonStatus(false);
                }

                if (this.SectionGridView.OriginalRowCount > this.SectionGridView.NumRowsVisible)
                {
                    this.SectionScrollBar.Visible = false;
                }
                else
                {
                    this.SectionScrollBar.Visible = true;
                }

                this.SectionGridView.Rows[0].Selected = true;

                // Populate SelectedComponentGridView
                this.SelectedComponentGridView.DataSource = this.SelectedComponentEstimateValue();
            }
        }

        /// <summary>
        /// SelectedGroupEstimateValue
        /// </summary>
        /// <returns>DataTable</returns>
        private DataTable SelectedGroupEstimateValue()
        {
            string groupXml = string.Empty;
            DataSet estimategroupDataSet = new DataSet();
            groupXml = this.estimateHashtable["SelectedGroup"].ToString();

            StringReader stringEstimateGroup = new StringReader(groupXml);
            XmlTextReader textReaderEstimateGroup = new XmlTextReader(stringEstimateGroup);
            estimategroupDataSet.ReadXml(textReaderEstimateGroup);

            if (estimategroupDataSet.Tables.Count > 0)
            {
                // Getting ResidenceGroupType_Id
                for (int keyCount = 0; keyCount < estimategroupDataSet.Tables[0].Rows.Count; keyCount++)
                {
                    int groupTypeKey = 0;
                    int.TryParse(estimategroupDataSet.Tables[0].Rows[keyCount]["GroupID"].ToString(), out groupTypeKey);
                    DataRow[] groupTypeID;
                    groupTypeID = this.houseTypeCollectionDataSet.Tables["ResidenceGroupType"].Select("Key = " + "'" + groupTypeKey + "'");

                    int residenceGroupTypeId = 0;
                    if (groupTypeID.Length > 0)
                    {
                        int.TryParse(groupTypeID[0]["ResidenceGroupType_Id"].ToString(), out residenceGroupTypeId);
                    }

                    estimategroupDataSet.Tables[0].Rows[keyCount]["ResidenceGroupType_Id"] = residenceGroupTypeId;
                }

                this.sectionDataTable = estimategroupDataSet.Tables[0];
            }

            return this.sectionDataTable;
        }

        /// <summary>
        /// SelectedComponentEstimate
        /// </summary>
        /// <returns>DataTable</returns>
        private DataTable SelectedComponentEstimateValue()
        {
            string componentXml = string.Empty;
            DataSet estimateComponentDataSet = new DataSet();
            componentXml = this.estimateHashtable["SelectedComponent"].ToString();

            StringReader stringEstimateComponent = new StringReader(componentXml);
            XmlTextReader textReaderEstimateComponent = new XmlTextReader(stringEstimateComponent);
            estimateComponentDataSet.ReadXml(textReaderEstimateComponent);

            if (estimateComponentDataSet.Tables.Count > 0)
            {
                this.selectedComponentDataTable = estimateComponentDataSet.Tables[0];
            }
            else
            {
                this.selectedComponentDataTable.Clear();
            }

            return this.selectedComponentDataTable;
        }

        /// <summary>
        /// Replaces the saved values.
        /// </summary>
        private void ReplaceSavedGeneralTabValues()
        {
            double primaryPercentageDouble = 0.0;
            double secondaryPercentageDouble = 0.0;
            double manufacturedPrimaryPercentageDouble = 0.0;
            double manufacturedSecondaryPercentageDouble = 0.0;

            if (this.saveFieldHashTable.Count > 0)
            {
                if (this.houseType == (int)HouseType.SingleFamilyResidence || this.houseType == (int)HouseType.LowRiseMultiple || this.houseType == (int)HouseType.TownHouseEndUnit || this.houseType == (int)HouseType.TownHouseInsideUnit || this.houseType == (int)HouseType.Duplex)
                {
                    this.PrimaryStyleCombo.SelectedValue = this.saveFieldHashTable["PrimaryStyleValue"].ToString();
                    double.TryParse(this.saveFieldHashTable["PrimaryStylePercent"].ToString(), out primaryPercentageDouble);
                    this.PrimaryPercentageTextBox.Text = Convert.ToString(Math.Round(primaryPercentageDouble));
                    this.PrimaryAreaTextBox.Text = this.saveFieldHashTable["SinglePrimaryArea"].ToString();

                    if (this.saveFieldHashTable["SecondaryStyleValue"].ToString() != "-1")
                    {
                        this.SecondaryStyleCombo.SelectedValue = this.saveFieldHashTable["SecondaryStyleValue"].ToString();
                    }
                    else
                    {
                        this.SecondaryStyleCombo.SelectedValue = this.saveFieldHashTable["SecondaryStyleValue"].ToString();
                        this.SecondaryAreaTextBox.Enabled = false;
                    }

                    double.TryParse(this.saveFieldHashTable["SecondaryStylePercent"].ToString(), out secondaryPercentageDouble);
                    this.SecondaryPercentageTextBox.Text = Convert.ToString(Math.Round(secondaryPercentageDouble));
                    this.SecondaryAreaTextBox.Text = this.saveFieldHashTable["SingleSecondaryArea"].ToString();

                    if (this.houseType == (int)HouseType.LowRiseMultiple)
                    {
                        this.UnitTextBox.Text = this.saveFieldHashTable["LowRiseMultiplieUnits"].ToString();
                    }
                    else
                    {
                        this.UnitTextBox.Text = "0";
                    }

                    this.DefaultHeightTextBox.Text = this.saveFieldHashTable["SingleDefaultHeight"].ToString();
                    this.StoryHeightTextBox.Text = this.saveFieldHashTable["SingleStoryHeight"].ToString();
                    this.TotalAreaTextBox.Text = this.saveFieldHashTable["FloorArea"].ToString();
                    this.SingleBaseQualityNumberTextBox.Text = this.saveFieldHashTable["QualityValue"].ToString();
                    this.SinglebaseQualityDescriptionLabel.Text = this.saveFieldHashTable["QualityDescription"].ToString();
                    this.CostMultiplerTextBox.Text = this.saveFieldHashTable["CostMultipler"].ToString();

                    this.EnergyAdjustmentComboBox.SelectedItem = this.saveFieldHashTable["EnergyAdjustment"].ToString();
                    this.FoundationAdjustmentCombo.SelectedItem = this.saveFieldHashTable["FoundationAdjustment"].ToString();
                    this.HillSideAdjustmentCombo.SelectedItem = this.saveFieldHashTable["HillSideAdjustment"].ToString();
                    this.SeismicAdjustmentCombo.SelectedItem = this.saveFieldHashTable["SelsmicAdjustment"].ToString();
                    this.WindAdjustmentCombo.SelectedItem = this.saveFieldHashTable["WindAdjustment"].ToString();
                }
                else
                {
                    this.ManufacturedHousingPrimaryStyleCombo.SelectedValue = this.saveFieldHashTable["PrimaryStyleValue"].ToString();
                    this.ManufacturedHousingPrimaryWidthTextBox.Text = this.saveFieldHashTable["PrimaryStyleWidth"].ToString();
                    this.ManufacturedHousingPrimaryLengthTextBox.Text = this.saveFieldHashTable["PrimaryStyleLength"].ToString();
                    this.ManufacturedHousingPrimaryAreaTextBox.Text = this.saveFieldHashTable["ManufacturePrimaryArea"].ToString();

                    double.TryParse(this.saveFieldHashTable["PrimaryStylePercent"].ToString(), out manufacturedPrimaryPercentageDouble);
                    this.ManufacturedHousingPrimaryPercentageTextBox.Text = Convert.ToString(Math.Round(manufacturedPrimaryPercentageDouble));

                    if (this.saveFieldHashTable["SecondaryStyleValue"].ToString() != "-1")
                    {
                        this.ManufacturedHousingTagalongStyleCombo.SelectedItem = this.saveFieldHashTable["SecondaryStyleValue"].ToString();
                    }
                    else
                    {
                        this.ManufacturedHousingTagalongStyleCombo.SelectedItem = this.saveFieldHashTable["SecondaryStyleValue"].ToString();
                        this.ManufacturedHousingSecondaryWidthTextBox.Enabled = false;
                        this.ManufacturedHousingSecondaryLengthTextBox.Enabled = false;
                    }

                    this.ManufacturedHousingSecondaryWidthTextBox.Text = this.saveFieldHashTable["SecondaryStyleWidth"].ToString();
                    this.ManufacturedHousingSecondaryLengthTextBox.Text = this.saveFieldHashTable["SecondaryStyleLength"].ToString();
                    this.ManufacturedHousingSecondaryAreaTextBox.Text = this.saveFieldHashTable["ManufactureSecondaryArea"].ToString();

                    double.TryParse(this.saveFieldHashTable["SecondaryStylePercent"].ToString(), out manufacturedSecondaryPercentageDouble);
                    this.ManufacturedHousingSecondaryPercentageTextBox.Text = Convert.ToString(Math.Round(manufacturedSecondaryPercentageDouble));

                    this.ManufacturedHousingDefaultHeightTextBox.Text = this.saveFieldHashTable["ManufactureDefaultHeight"].ToString();
                    this.ManufacturedHousingStoryHeightTextBox.Text = this.saveFieldHashTable["ManufactureStoryHeight"].ToString();
                    this.ManufacturedHousingTotalAreaTextBox.Text = this.saveFieldHashTable["FloorArea"].ToString();
                    this.BaseQualityNumberTextBox.Text = this.saveFieldHashTable["QualityValue"].ToString();
                    this.BaseQuailtyDescriptionLabel.Text = this.saveFieldHashTable["QualityDescription"].ToString();
                    this.CostMultiplerTextBox.Text = this.saveFieldHashTable["CostMultipler"].ToString();

                    this.WallEnergyAdjustmentCombo.SelectedItem = this.saveFieldHashTable["WallEnergyAdjustment"].ToString();
                }
            }
            else
            {
                this.LoadGeneralInformation();
                this.MarshallSwiftTab.SelectedIndex = 0;
                this.totalFloorArea = 0;

                if (this.houseType == (int)HouseType.SingleFamilyResidence || this.houseType == (int)HouseType.LowRiseMultiple || this.houseType == (int)HouseType.TownHouseEndUnit || this.houseType == (int)HouseType.TownHouseInsideUnit || this.houseType == (int)HouseType.Duplex)
                {
                    this.PrimaryPercentageTextBox.Text = string.Empty;
                    this.PrimaryAreaTextBox.Text = "0";
                    this.SecondaryPercentageTextBox.Text = string.Empty;
                    this.SecondaryAreaTextBox.Text = "0";
                    this.TotalAreaTextBox.Text = "0";
                    this.StoryHeightTextBox.EmptyDecimalValue = true;
                    this.CostMultiplerTextBox.EmptyDecimalValue = true;
                    this.StoryHeightTextBox.Text = string.Empty;
                    this.CostMultiplerTextBox.Text = string.Empty;
                    this.CalculateRcnTextBox.Text = string.Empty;

                    this.EnergyAdjustmentComboBox.SelectedIndex = 0;
                    this.FoundationAdjustmentCombo.SelectedIndex = 0;
                    this.HillSideAdjustmentCombo.SelectedIndex = 0;
                    this.SeismicAdjustmentCombo.SelectedIndex = 0;
                    this.WindAdjustmentCombo.SelectedIndex = 0;
                    this.PropertyQualityTextBox.Text = this.houseTypeCollectionDataSet.Tables["ResidenceTypeQuality"].Rows[2]["Key"].ToString();
                }
                else
                {
                    this.ManufacturedHousingPrimaryWidthTextBox.Text = string.Empty;
                    this.ManufacturedHousingPrimaryLengthTextBox.Text = string.Empty;
                    this.ManufacturedHousingPrimaryAreaTextBox.Text = "0";
                    this.ManufacturedHousingPrimaryPercentageTextBox.Text = string.Empty;
                    this.ManufacturedHousingSecondaryWidthTextBox.Text = string.Empty;
                    this.ManufacturedHousingSecondaryLengthTextBox.Text = string.Empty;
                    this.ManufacturedHousingSecondaryAreaTextBox.Text = "0";
                    this.ManufacturedHousingSecondaryPercentageTextBox.Text = string.Empty;
                    this.ManufacturedHousingTotalAreaTextBox.Text = "0";
                    this.CostMultiplerTextBox.EmptyDecimalValue = true;
                    this.ManufacturedHousingStoryHeightTextBox.EmptyDecimalValue = true;
                    this.CostMultiplerTextBox.Text = string.Empty;
                    this.ManufacturedHousingStoryHeightTextBox.Text = string.Empty;
                    this.CalculateRcnTextBox.Text = string.Empty;
                    this.WallEnergyAdjustmentCombo.SelectedIndex = 0;
                    this.PropertyQualityTextBox.Text = this.houseTypeCollectionDataSet.Tables["ResidenceTypeQuality"].Rows[2]["Key"].ToString();
                }
            }
        }

        /// <summary>
        /// Replaces the saved component tab values.
        /// </summary>
        private void ReplaceSavedComponentTabValues()
        {
            if (this.saveFieldHashTable.Count > 0)
            {
                DataSet savedsectionTable = new DataSet();
                DataSet savedSelectedComponentTable = new DataSet();

                string sectionXml = this.saveFieldHashTable["SectionTableXml"].ToString();
                string selectedComponentXml = this.saveFieldHashTable["SelectedComponentTableXml"].ToString();

                StringReader savedSectionXmlReader = new StringReader(sectionXml);
                System.Xml.XmlTextReader savedSectionTextReader = new System.Xml.XmlTextReader(savedSectionXmlReader);

                StringReader savedSelectedComponentXmlReader = new StringReader(selectedComponentXml);
                System.Xml.XmlTextReader savedSelectedComponentTextReader = new System.Xml.XmlTextReader(savedSelectedComponentXmlReader);

                savedsectionTable.ReadXml(savedSectionTextReader);
                savedSelectedComponentTable.ReadXml(savedSelectedComponentTextReader);

                if (savedsectionTable != null && savedSelectedComponentTable != null)
                {
                    if (savedsectionTable.Tables.Count > 0 && savedSelectedComponentTable.Tables.Count > 0)
                    {
                        this.sectionDataTable = savedsectionTable.Tables[0];
                        this.savedSelectedComponentDataTable = savedSelectedComponentTable.Tables[0];
                        this.selectedComponentDataTable = this.savedSelectedComponentDataTable;
                        this.SectionGridView.DataSource = this.sectionDataTable;
                        this.SelectedComponentGridView.DataSource = this.selectedComponentDataTable;
                    }
                    else
                    {
                        this.selectedComponentDataTable.Clear();
                        this.SelectedComponentGridView.DataSource = this.selectedComponentDataTable;
                        this.SectionGridClick(0);
                        this.TemplateButtonStatus();
                    }
                }
                else
                {
                    this.selectedComponentDataTable.Clear();
                    this.SelectedComponentGridView.DataSource = this.selectedComponentDataTable;
                    this.SectionGridClick(0);
                    this.TemplateButtonStatus();
                }
            }
            else
            {
                this.sectionDataTable.Clear();
                this.selectedComponentDataTable.Clear();
                this.SelectedComponentGridView.DataSource = this.selectedComponentDataTable;
                this.SectionGridClick(0);
                this.TemplateButtonStatus();
            }
        }

        /// <summary>
        /// Saves the elements.
        /// </summary>
        private void SaveElements()
        {
            this.CalculateTotalArea();
            this.saveFieldHashTable.Clear();
            this.saveFieldHashTable.Add("ConnectionString", this.connectionString);
            this.saveFieldHashTable.Add("ValueSliceID", this.valueSliceId);
            this.saveFieldHashTable.Add("EstimateID", this.estimateIdValue);
            this.saveFieldHashTable.Add("ZipCode", this.zipCode.ToString());
            this.saveFieldHashTable.Add("UserID", TerraScanCommon.UserId.ToString());
            this.saveFieldHashTable.Add("RCNValue", this.CalculateRcnTextBox.Text.ToString().Replace("$", "").Replace(",", ""));
            this.saveFieldHashTable.Add("ConstructionType", this.constructionTypeId.ToString());

            // Populate Default Components
            this.saveFieldHashTable.Add("DefaultComponentAppliance", TerraScanCommon.GetXmlString(this.houseTypeCollectionDataSet.Tables["DefaultAppliance"]));
            this.saveFieldHashTable.Add("DefaultComponentFloorCover", TerraScanCommon.GetXmlString(this.houseTypeCollectionDataSet.Tables["DefaultFloorCover"]));
            this.saveFieldHashTable.Add("DefaultComponentFoundation", TerraScanCommon.GetXmlString(this.houseTypeCollectionDataSet.Tables["DefaultFoundation"]));
            this.saveFieldHashTable.Add("DefaultComponentHeating", TerraScanCommon.GetXmlString(this.houseTypeCollectionDataSet.Tables["DefaultHeating"]));
            this.saveFieldHashTable.Add("DefaultComponentRoofing", TerraScanCommon.GetXmlString(this.houseTypeCollectionDataSet.Tables["DefaultRoofing"]));
            this.saveFieldHashTable.Add("DefaultComponentRoughIn", TerraScanCommon.GetXmlString(this.houseTypeCollectionDataSet.Tables["DefaultRoughIn"]));
            this.saveFieldHashTable.Add("SectionTableXml", TerraScanCommon.GetXmlString(this.sectionDataTable));

            this.saveFieldHashTable.Add("SelectedComponentTableXml", TerraScanCommon.GetXmlString(this.selectedComponentDataTable));

            if (this.houseType == (int)HouseType.SingleFamilyResidence || this.houseType == (int)HouseType.LowRiseMultiple || this.houseType == (int)HouseType.TownHouseEndUnit || this.houseType == (int)HouseType.TownHouseInsideUnit || this.houseType == (int)HouseType.Duplex)
            {
                // Single Primary Area
                this.saveFieldHashTable.Add("SinglePrimaryArea", this.PrimaryAreaTextBox.Text.Trim());
                this.saveFieldHashTable.Add("SingleSecondaryArea", this.SecondaryAreaTextBox.Text.Trim());
                this.saveFieldHashTable.Add("FloorArea", this.TotalAreaTextBox.Text.Trim());
            }
            else
            {
                // Manufacture Primary Area
                this.saveFieldHashTable.Add("ManufacturePrimaryArea", this.ManufacturedHousingPrimaryAreaTextBox.Text.Trim());
                this.saveFieldHashTable.Add("ManufactureSecondaryArea", this.ManufacturedHousingSecondaryAreaTextBox.Text.Trim());
                this.saveFieldHashTable.Add("FloorArea", this.ManufacturedHousingTotalAreaTextBox.Text.Trim());
            }

            this.saveFieldHashTable.Add("StoryHeight", this.StoryHeightTextBox.Text.Trim());
            this.saveFieldHashTable.Add("HouseType", this.houseType);

            // LowRiseMultiple
            if (this.houseType == (int)HouseType.LowRiseMultiple)
            {
                this.saveFieldHashTable.Add("LowRiseMultiplieUnits", this.UnitTextBox.Text.Trim());
            }
            else
            {
                this.saveFieldHashTable.Add("LowRiseMultiplieUnits", string.Empty);
            }

            if (this.houseType == (int)HouseType.SingleFamilyResidence || this.houseType == (int)HouseType.LowRiseMultiple || this.houseType == (int)HouseType.TownHouseEndUnit || this.houseType == (int)HouseType.TownHouseInsideUnit || this.houseType == (int)HouseType.Duplex)
            {
                // Populate PrimaryStyle Values
                if (this.PrimaryStyleCombo.SelectedValue != null)
                {
                    this.saveFieldHashTable.Add("PrimaryStyleValue", this.PrimaryStyleCombo.SelectedValue.ToString());
                }
                else
                {
                    this.saveFieldHashTable.Add("PrimaryStyleValue", "0");
                }

                this.saveFieldHashTable.Add("PrimaryStyle", this.PrimaryStyleCombo.Text.Trim());
                this.saveFieldHashTable.Add("PrimaryStyleLength", string.Empty);
                this.saveFieldHashTable.Add("PrimaryStylePercent", this.sitePrimaryPercentage);
                this.saveFieldHashTable.Add("PrimaryStyleSize", string.Empty);
                this.saveFieldHashTable.Add("PrimaryStyleWidth", string.Empty);
                this.saveFieldHashTable.Add("SecondarStyleSize", string.Empty);

                // Populate SecondaryStyle Values
                if (this.SecondaryStyleCombo.SelectedValue != null)
                {
                    this.saveFieldHashTable.Add("SecondaryStyleValue", this.SecondaryStyleCombo.SelectedValue.ToString());
                }
                else
                {
                    this.saveFieldHashTable.Add("SecondaryStyleValue", "-1");
                }

                this.saveFieldHashTable.Add("SecondaryStyle", this.SecondaryStyleCombo.Text.Trim());
                this.saveFieldHashTable.Add("SecondaryStyleLength", string.Empty);
                this.saveFieldHashTable.Add("SecondaryStylePercent", this.siteSecondaryPercentage);
                this.saveFieldHashTable.Add("SecondaryStyleWidth", string.Empty);
                this.saveFieldHashTable.Add("SingleDefaultHeight", this.DefaultHeightTextBox.Text.Trim());
                this.saveFieldHashTable.Add("SingleStoryHeight", this.StoryHeightTextBox.Text.Trim());
                this.saveFieldHashTable.Add("QualityDescription", this.SinglebaseQualityDescriptionLabel.Text.Trim());
                this.saveFieldHashTable.Add("QualityValue", this.SingleBaseQualityNumberTextBox.Text.Trim());

                // EnergyAdjustment
                this.saveFieldHashTable.Add("EnergyAdjustment", this.EnergyAdjustmentComboBox.Text.Trim());
                this.saveFieldHashTable.Add("EnergyAdjustmentKey", this.EnergyAdjustmentComboBox.SelectedValue.ToString().Trim());

                // FoundationAdjustment
                this.saveFieldHashTable.Add("FoundationAdjustment", this.FoundationAdjustmentCombo.Text.Trim());
                this.saveFieldHashTable.Add("FoundationAdjustmentKey", this.FoundationAdjustmentCombo.SelectedValue.ToString().Trim());

                // HillSideAdjustment
                this.saveFieldHashTable.Add("HillSideAdjustment", this.HillSideAdjustmentCombo.Text.Trim());
                this.saveFieldHashTable.Add("HillSideAdjustmentKey", this.HillSideAdjustmentCombo.SelectedValue.ToString().Trim());

                // SelsmicAdjustment
                this.saveFieldHashTable.Add("SelsmicAdjustment", this.SeismicAdjustmentCombo.Text.Trim());
                this.saveFieldHashTable.Add("SelsmicAdjustmentKey", this.SeismicAdjustmentCombo.SelectedValue.ToString().Trim());

                // WindAdjustment
                this.saveFieldHashTable.Add("WindAdjustment", this.WindAdjustmentCombo.Text.Trim());
                this.saveFieldHashTable.Add("WindAdjustmentKey", this.WindAdjustmentCombo.SelectedValue.ToString().Trim());

                // CostMultipler
                this.saveFieldHashTable.Add("CostMultipler", this.CostMultiplerTextBox.Text.Trim());
            }
            else
            {
                // Populate PrimaryStyle Values
                if (this.ManufacturedHousingPrimaryStyleCombo.SelectedValue != null)
                {
                    this.saveFieldHashTable.Add("PrimaryStyleValue", this.ManufacturedHousingPrimaryStyleCombo.SelectedValue.ToString());
                }
                else
                {
                    this.saveFieldHashTable.Add("PrimaryStyleValue", "0");
                }

                this.saveFieldHashTable.Add("PrimaryStyle", this.ManufacturedHousingPrimaryStyleCombo.Text.Trim());
                this.saveFieldHashTable.Add("PrimaryStyleLength", this.ManufacturedHousingPrimaryLengthTextBox.Text.Trim());
                this.saveFieldHashTable.Add("PrimaryStylePercent", this.manufacturePrimaryPercentage);
                this.saveFieldHashTable.Add("PrimaryStyleSize", string.Empty);
                this.saveFieldHashTable.Add("PrimaryStyleWidth", this.ManufacturedHousingPrimaryWidthTextBox.Text.Trim());
                this.saveFieldHashTable.Add("SecondarStyleSize", string.Empty);

                // Populate SecondaryStyle Values
                if (this.ManufacturedHousingTagalongStyleCombo.SelectedValue != null)
                {
                    this.saveFieldHashTable.Add("SecondaryStyleValue", this.ManufacturedHousingTagalongStyleCombo.SelectedValue.ToString());
                }
                else
                {
                    this.saveFieldHashTable.Add("SecondaryStyleValue", "-1");
                }

                this.saveFieldHashTable.Add("SecondaryStyle", this.ManufacturedHousingTagalongStyleCombo.Text.Trim());
                this.saveFieldHashTable.Add("SecondaryStyleLength", this.ManufacturedHousingSecondaryLengthTextBox.Text.Trim());
                this.saveFieldHashTable.Add("SecondaryStylePercent", this.manufactureSecondaryPercentage);
                this.saveFieldHashTable.Add("SecondaryStyleWidth", this.ManufacturedHousingSecondaryWidthTextBox.Text.Trim());
                this.saveFieldHashTable.Add("ManufactureDefaultHeight", this.ManufacturedHousingDefaultHeightTextBox.Text.Trim());
                this.saveFieldHashTable.Add("ManufactureStoryHeight", this.ManufacturedHousingStoryHeightTextBox.Text.Trim());
                this.saveFieldHashTable.Add("QualityDescription", this.BaseQuailtyDescriptionLabel.Text.Trim());
                this.saveFieldHashTable.Add("QualityValue", this.BaseQualityNumberTextBox.Text.Trim());

                // WallEnergyAdjustment
                this.saveFieldHashTable.Add("WallEnergyAdjustment", this.WallEnergyAdjustmentCombo.Text.Trim());
                this.saveFieldHashTable.Add("WallEnergyAdjustmentKey", this.WallEnergyAdjustmentCombo.SelectedValue.ToString().Trim());

                // CostMultipler
                this.saveFieldHashTable.Add("CostMultipler", this.CostMultiplerTextBox.Text.Trim());
            }
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;


            if (!this.componentTabSelected)
            {
                if (this.componentFieldValid)
                {
                    sliceValidationFields.DisableNewMethod = true;
                    sliceValidationFields.RequiredFieldMissing = false;
                    return sliceValidationFields;
                }

                if (this.houseType == (int)HouseType.SingleFamilyResidence || this.houseType == (int)HouseType.TownHouseEndUnit || this.houseType == (int)HouseType.TownHouseInsideUnit || this.houseType == (int)HouseType.Duplex)
                {
                    this.isFormMasterCall = true;
                    if (this.CheckMainComponent())
                    {
                        this.isFormMasterCall = false;

                        if (this.ValidateDepreciation())
                        {
                            this.CalculateTotalArea();

                            if (string.IsNullOrEmpty(this.PrimaryAreaTextBox.Text.Trim()) || this.PrimaryAreaTextBox.Text.Trim() == "0")
                            {
                                this.PrimaryAreaTextBox.Focus();
                                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("Total Area must be 300 through 25000");
                                sliceValidationFields.RequiredFieldMissing = true;
                                return sliceValidationFields;
                            }

                            if (this.tagalongSelected)
                            {
                                if (string.IsNullOrEmpty(this.SecondaryAreaTextBox.Text.Trim()) || this.SecondaryAreaTextBox.Text.Trim() == "0")
                                {
                                    this.SecondaryAreaTextBox.Focus();
                                    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("Total Area must be 300 through 25000");
                                    sliceValidationFields.RequiredFieldMissing = true;
                                    return sliceValidationFields;
                                }
                            }

                            if (this.totalFloorArea >= this.totalFloorMinValue && this.totalFloorArea <= this.totalFloorMaxValue)
                            {
                                return sliceValidationFields;
                            }
                            else
                            {
                                this.PrimaryAreaTextBox.Focus();
                                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("Total Area must be 300 through 25000");
                                sliceValidationFields.RequiredFieldMissing = true;
                                return sliceValidationFields;
                            }
                        }
                        else
                        {
                            sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredMsg");
                            sliceValidationFields.RequiredFieldMissing = true;
                            return sliceValidationFields;
                        }
                    }
                    else
                    {
                        this.isFormMasterCall = false;
                        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("ExteriorWallsRoofingMissing");
                        sliceValidationFields.RequiredFieldMissing = true;
                        return sliceValidationFields;
                    }
                    this.isFormMasterCall = false;
                }
                else if (this.houseType == (int)HouseType.LowRiseMultiple)
                {
                    if (this.ValidateDepreciation() && this.CheckMainComponent())
                    {
                        this.CalculateTotalArea();

                        if (string.IsNullOrEmpty(this.PrimaryAreaTextBox.Text.Trim()) || this.PrimaryAreaTextBox.Text.Trim() == "0")
                        {
                            this.ManufacturedHousingPrimaryAreaTextBox.Focus();
                            sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("Total Area must be 600 through 300000");
                            sliceValidationFields.RequiredFieldMissing = true;
                            return sliceValidationFields;
                        }

                        if (this.tagalongSelected)
                        {
                            if (string.IsNullOrEmpty(this.SecondaryAreaTextBox.Text.Trim()) || this.SecondaryAreaTextBox.Text.Trim() == "0")
                            {
                                this.SecondaryAreaTextBox.Focus();
                                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("Total Area must be 600 through 300000");
                                sliceValidationFields.RequiredFieldMissing = true;
                                return sliceValidationFields;
                            }
                        }

                        if (this.totalFloorArea >= this.totalFloorMinValue && this.totalFloorArea <= this.totalFloorMaxValue)
                        {
                            if (this.unitValue >= 3 && this.unitValue <= 150)
                            {
                                return sliceValidationFields;
                            }
                            else
                            {
                                this.UnitTextBox.Focus();
                                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredMsg");
                                sliceValidationFields.RequiredFieldMissing = true;
                                return sliceValidationFields;
                            }
                        }
                        else
                        {
                            this.PrimaryAreaTextBox.Focus();
                            sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("Total Area must be 600 through 300000");
                            sliceValidationFields.RequiredFieldMissing = true;
                            return sliceValidationFields;
                        }
                    }
                    else
                    {
                        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredMsg");
                        sliceValidationFields.RequiredFieldMissing = true;
                        return sliceValidationFields;
                    }
                }
                else
                {
                    if (this.ValidateDepreciation())
                    {
                        if (this.tagalongSelected)
                        {
                            if (string.IsNullOrEmpty(this.ManufacturedHousingPrimaryWidthTextBox.Text.Trim()) || string.IsNullOrEmpty(this.ManufacturedHousingPrimaryLengthTextBox.Text.Trim()) || string.IsNullOrEmpty(this.ManufacturedHousingSecondaryWidthTextBox.Text.Trim()) || string.IsNullOrEmpty(this.ManufacturedHousingSecondaryLengthTextBox.Text.Trim()))
                            {
                                this.ManufacturedHousingSecondaryWidthTextBox.Focus();
                                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredMsg");
                                sliceValidationFields.RequiredFieldMissing = true;
                                return sliceValidationFields;
                            }
                            else
                            {
                                this.CheckHousingTagalongAreaValidation(this.ManufacturedHousingSecondaryWidthTextBox);
                                this.CheckHousingTagalongAreaValidation(this.ManufacturedHousingSecondaryLengthTextBox);

                                if (this.totalFloorArea >= this.totalFloorMinValue && this.totalFloorArea <= this.totalFloorMaxValue)
                                {
                                    return sliceValidationFields;
                                }
                                else
                                {
                                    this.ManufacturedHousingSecondaryWidthTextBox.Focus();
                                    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredMsg");
                                    sliceValidationFields.RequiredFieldMissing = true;
                                    return sliceValidationFields;
                                }
                            }
                        }
                        else
                        {
                            this.CheckHousingPrimaryAreaValidation(this.ManufacturedHousingPrimaryWidthTextBox);
                            this.CheckHousingPrimaryAreaValidation(this.ManufacturedHousingPrimaryLengthTextBox);

                            if (this.totalFloorArea >= this.totalFloorMinValue && this.totalFloorArea <= this.totalFloorMaxValue)
                            {
                                return sliceValidationFields;
                            }
                            else
                            {
                                this.ManufacturedHousingPrimaryWidthTextBox.Focus();
                                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredMsg");
                                sliceValidationFields.RequiredFieldMissing = true;
                                return sliceValidationFields;
                            }
                        }
                    }
                    else
                    {
                        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredMsg");
                        sliceValidationFields.RequiredFieldMissing = true;
                        return sliceValidationFields;
                    }
                }
            }
            else
            {
                sliceValidationFields.DisableNewMethod = true;
                sliceValidationFields.RequiredFieldMissing = false;
                return sliceValidationFields;
            }
        }

        /// <summary>
        /// Validates the depreciation.
        /// </summary>
        /// <returns>bool</returns>
        private bool ValidateDepreciation()
        {
            // Depreciation Validation
            if (this.ByObjectCheckBox.Checked)
            {
                double tempCondition = 0.0;
                double.TryParse(this.ConditionTextBox.Text, out tempCondition);
                if (string.IsNullOrEmpty(this.DeprYearTextBox.Text.Trim()) || string.IsNullOrEmpty(this.ConditionTextBox.Text.Trim()) || tempCondition == 0.0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this.PropertyQualityTextBox.Text.Trim()))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Forms the load.
        /// </summary>
        private void FormLoad()
        {
            if (this.xmlCollectionDataSet.Tables.Count > 0)
            {
                if (this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.Rows.Count > 0)
                {
                    this.houseTypeXml = this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.Rows[0][this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.HTCXMLColumn.ColumnName].ToString();
                    this.adjustmentXml = this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.Rows[0][this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.AdjustmentXMLColumn.ColumnName].ToString();
                    this.adjustmentDeafultXml = this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.Rows[0][this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.AdjustmentDefaultXMLColumn.ColumnName].ToString();
                    this.rangeXml = this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.Rows[0][this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.RangeXMLColumn.ColumnName].ToString();
                    this.zipCode = this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.Rows[0][this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.ZipCodeColumn.ColumnName].ToString();
                    int.TryParse(this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.Rows[0][this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.ConstructionTypeIDColumn.ColumnName].ToString(), out this.constructionTypeId);

                    // Loading houseTypeXml dataset
                    StringReader stringReaderHouse = new StringReader(this.houseTypeXml);
                    XmlTextReader textReaderHouseType = new XmlTextReader(stringReaderHouse);
                    this.houseTypeCollectionDataSet.ReadXml(textReaderHouseType);

                    // Loading adjustmentXml dataset
                    StringReader stringReaderAdjustment = new StringReader(this.adjustmentXml);
                    XmlTextReader textReaderAdjustment = new XmlTextReader(stringReaderAdjustment);
                    this.adjustmentDataSet.ReadXml(textReaderAdjustment);

                    // Loading adjustmentDefaultXml dataset
                    StringReader stringReaderAdjustmentDefault = new StringReader(this.adjustmentDeafultXml);
                    XmlTextReader textReaderAdjustmentDefault = new XmlTextReader(stringReaderAdjustmentDefault);
                    this.adjustmentDefaultDataSet.ReadXml(textReaderAdjustmentDefault);

                    // Loading rangeXml dataset
                    StringReader stringReaderRange = new StringReader(this.rangeXml);
                    XmlTextReader textReaderRange = new XmlTextReader(stringReaderRange);
                    this.rangeDataSet.ReadXml(textReaderRange);

                    // Get the default values from the houseType xml
                    int.TryParse(this.houseTypeCollectionDataSet.Tables["ResidenceType"].Rows[0]["Key"].ToString(), out this.houseType);
                    double.TryParse(this.houseTypeCollectionDataSet.Tables["LowQuality"].Rows[0]["ID"].ToString(), out this.lowQuality);
                    double.TryParse(this.houseTypeCollectionDataSet.Tables["HighQuality"].Rows[0]["ID"].ToString(), out this.highQuality);
                    int.TryParse(this.houseTypeCollectionDataSet.Tables["ResidenceType"].Rows[0]["MinSize"].ToString(), out this.totalFloorMinValue);
                    int.TryParse(this.houseTypeCollectionDataSet.Tables["ResidenceType"].Rows[0]["MaxSize"].ToString(), out this.totalFloorMaxValue);

                    // Label Header
                    this.GeneralHeaderLabel.Text = this.houseTypeCollectionDataSet.Tables["ResidenceType"].Rows[0]["Description"].ToString();
                    this.ComponentHeaderLabel.Text = this.houseTypeCollectionDataSet.Tables["ResidenceType"].Rows[0]["Description"].ToString();

                    // General Information Tab
                    this.LoadGeneralInformation();

                    // Component Tab Customize Grid
                    this.CustomizeAllGridView();

                    // Assiging menu value for Contextmenustrip                    
                    this.selectedComponentMenuStrip.Items.Add(SharedFunctions.GetResourceString("MenuDelete"));
                    this.selectedComponentMenuStrip.Items.Add(SharedFunctions.GetResourceString("MenuCancel"));
                    this.selectedComponentMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(this.SelectedComponentMenuStrip_ItemClicked);
                    this.selectedComponentMenuStrip.Closed += new ToolStripDropDownClosedEventHandler(this.SelectedComponentMenuStrip_Closed);

                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.flagLoadOnProcess = false;
                }
                else
                {
                    this.ControlStatus(false);
                }
            }
            else
            {
                this.ControlStatus(false);
            }
        }

        /// <summary>
        /// Controls the status.
        /// </summary>
        /// <param name="status">if set to <c>true</c> [status].</param>
        private void ControlStatus(bool status)
        {
            this.CalculateButton.Enabled = status;
            this.PrimaryPercentageTextBox.Enabled = status;
            this.PrimaryAreaTextBox.Enabled = status;
            this.PrimaryStyleCombo.Enabled = status;
            this.SecondaryStyleCombo.Enabled = status;
            this.SecondaryPercentageTextBox.Enabled = status;
            this.SecondaryAreaTextBox.Enabled = status;
            this.UnitTextBox.Enabled = status;
            this.DefaultHeightTextBox.Enabled = status;
            this.StoryHeightTextBox.Enabled = status;
            this.SingleBaseQualityNumberTextBox.Enabled = status;

            this.ManufacturedHousingPrimaryStyleCombo.Enabled = status;
            this.ManufacturedHousingPrimaryWidthTextBox.Enabled = status;
            this.ManufacturedHousingPrimaryLengthTextBox.Enabled = status;
            this.ManufacturedHousingPrimaryAreaTextBox.Enabled = status;
            this.ManufacturedHousingPrimaryPercentageTextBox.Enabled = status;
            this.ManufacturedHousingTagalongStyleCombo.Enabled = status;
            this.ManufacturedHousingSecondaryWidthTextBox.Enabled = status;
            this.ManufacturedHousingSecondaryLengthTextBox.Enabled = status;
            this.ManufacturedHousingSecondaryAreaTextBox.Enabled = status;
            this.ManufacturedHousingSecondaryPercentageTextBox.Enabled = status;
            this.ManufacturedHousingDefaultHeightTextBox.Enabled = status;
            this.ManufacturedHousingStoryHeightTextBox.Enabled = status;
            this.ManufacturedHousingTotalAreaTextBox.Enabled = status;
            this.BaseQualityNumberTextBox.Enabled = status;
            this.CalculateButton.Enabled = status;

            this.EnergyAdjustmentComboBox.Enabled = status;
            this.FoundationAdjustmentCombo.Enabled = status;
            this.HillSideAdjustmentCombo.Enabled = status;
            this.SeismicAdjustmentCombo.Enabled = status;
            this.WindAdjustmentCombo.Enabled = status;
            this.CostMultiplerTextBox.Enabled = status;
            this.CalculateRcnTextBox.Enabled = status;
            this.WallEnergyAdjustmentCombo.Enabled = status;
        }

        /// <summary>
        /// Clears the fileds.
        /// </summary>
        private void ClearGeneralFileds()
        {
            if (this.houseType == (int)HouseType.SingleFamilyResidence || this.houseType == (int)HouseType.LowRiseMultiple || this.houseType == (int)HouseType.TownHouseEndUnit || this.houseType == (int)HouseType.TownHouseInsideUnit || this.houseType == (int)HouseType.Duplex)
            {
                this.PrimaryPercentageTextBox.Text = "0";
                this.PrimaryAreaTextBox.Text = "0";
                this.SecondaryPercentageTextBox.Text = "0";
                this.SecondaryAreaTextBox.Text = "0";
                this.TotalAreaTextBox.Text = "0";
                this.CalculateRcnTextBox.Text = "0";
                this.totalFloorArea = 0.0;
                this.SecondaryAreaTextBox.Enabled = false;
            }
            else
            {
                this.CalculateRcnTextBox.Text = "0";
                this.ManufacturedHousingPrimaryWidthTextBox.Text = string.Empty;
                this.ManufacturedHousingPrimaryLengthTextBox.Text = string.Empty;
                this.ManufacturedHousingPrimaryAreaTextBox.Text = "0";
                this.ManufacturedHousingPrimaryPercentageTextBox.Text = "0";
                this.ManufacturedHousingSecondaryWidthTextBox.Text = string.Empty;
                this.ManufacturedHousingSecondaryLengthTextBox.Text = string.Empty;
                this.ManufacturedHousingSecondaryAreaTextBox.Text = "0";
                this.ManufacturedHousingSecondaryPercentageTextBox.Text = "0";
                this.ManufacturedHousingTotalAreaTextBox.Text = "0";
                this.totalFloorArea = 0.0;
                this.ManufacturedHousingSecondaryWidthTextBox.Enabled = false;
                this.ManufacturedHousingSecondaryLengthTextBox.Enabled = false;
            }
        }

        /// <summary>
        /// Clears the component fields.
        /// </summary>
        private void ClearComponentFields()
        {
            this.sectionDataTable.Clear();
            this.systemDataTable.Clear();
            this.componentDataTable.Clear();
            this.selectedComponentDataTable.Clear();
        }

        #endregion

        #region General Tab

        /// <summary>
        /// ValidateTotalArea 
        /// </summary>
        /// <returns>bool</returns>
        private bool ValidateTotalArea()
        {
            if (this.totalFloorArea >= this.totalFloorMinValue && this.totalFloorArea <= this.totalFloorMaxValue)
            {
                return true;
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("TotalAreaErrorMsg") + this.totalFloorMinValue + SharedFunctions.GetResourceString("ErrorMsgThrough") + this.totalFloorMaxValue, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        /// <summary>
        /// Toes the enable edit button in master form.
        /// </summary>
        private void ToEnableEditButtonInMasterForm()
        {
            if (!this.flagLoadOnProcess)
            {
                this.EditEnabled();

                if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                {
                    if (!this.rcnCalculated)
                    {
                        this.CalculateRcnTextBox.EmptyDecimalValue = true;
                        this.CalculateRcnTextBox.Text = string.Empty;
                        this.calculatedRcn = string.Empty;

                        this.RcnldTextBox.EmptyDecimalValue = true;
                        this.RcnldTextBox.Text = string.Empty;

                        this.DepreciationCalculateRcnTextBox.EmptyDecimalValue = true;
                        this.DepreciationCalculateRcnTextBox.Text = string.Empty;

                        this.DepreciationAmountTextBox.EmptyDecimalValue = true;
                        this.DepreciationAmountTextBox.Text = string.Empty;

                        this.RcnLessDeprTextBox.EmptyDecimalValue = true;
                        this.RcnLessDeprTextBox.Text = string.Empty;

                        this.CalculateDepreciationTextBox.EmptyDecimalValue = true;
                        this.CalculateDepreciationTextBox.Text = string.Empty;
                    }
                    else
                    {
                        this.rcnCalculated = false;
                    }
                }
            }
        }

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        /// <summary>
        /// Checks the housing area validation.
        /// </summary>
        /// <param name="currentTextBox">The current text box.</param>
        private void CheckHousingPrimaryAreaValidation(TerraScanTextBox currentTextBox)
        {
            double primaryWidthValue = 0.0;
            double typedWidthValue = 0.0;
            double maxWidth = 0.0;
            double minWidth = 0.0;
            string widthDescription = string.Empty;
            double.TryParse(this.ManufacturedHousingPrimaryStyleCombo.SelectedValue.ToString(), out primaryWidthValue);
            double.TryParse(currentTextBox.Text.Trim().ToString(), out typedWidthValue);
            DataRow[] primaryWidthDataRow;
            primaryWidthDataRow = this.houseTypeCollectionDataSet.Tables["ResidenceStyle"].Select("key = " + "'" + primaryWidthValue + "'" + "AND Styles_Id = " + "'" + this.houseTypeCollectionDataSet.Tables["Styles"].Rows[0]["Styles_Id"] + "'");

            if (primaryWidthDataRow.Length > 0)
            {
                foreach (DataRow widthRow in primaryWidthDataRow)
                {
                    if (currentTextBox.Tag.ToString() == "PrimaryWidth")
                    {
                        double.TryParse(widthRow.ItemArray[3].ToString(), out maxWidth);
                        double.TryParse(widthRow.ItemArray[4].ToString(), out minWidth);
                        widthDescription = widthRow.ItemArray[1].ToString();
                    }
                    else
                    {
                        double.TryParse(widthRow.ItemArray[5].ToString(), out maxWidth);
                        double.TryParse(widthRow.ItemArray[6].ToString(), out minWidth);
                        widthDescription = widthRow.ItemArray[1].ToString();
                    }
                }

                if (typedWidthValue >= minWidth && typedWidthValue <= maxWidth)
                {
                    this.componentTabSelected = false;

                    if (!string.IsNullOrEmpty(this.ManufacturedHousingPrimaryWidthTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.ManufacturedHousingPrimaryLengthTextBox.Text.Trim()))
                    {
                        this.CalculatePrimaryManufactureAreaTotal();
                    }
                }
                else
                {
                    if (!this.tabProcess)
                    {
                        if (currentTextBox.Tag.ToString() == "PrimaryWidth")
                        {
                            if (!this.saveProcess)
                            {
                                MessageBox.Show(widthDescription + " Width must be " + minWidth + " through " + maxWidth, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            if (!this.saveProcess)
                            {
                                MessageBox.Show(widthDescription + " Length must be " + minWidth + " through " + maxWidth, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                        this.componentTabSelected = true;
                        currentTextBox.Focus();
                        return;
                    }
                    else
                    {
                        currentTextBox.Focus();
                        this.tabProcess = false;
                    }
                }
            }
        }

        /// <summary>
        /// Checks the housing tagalong area validation.
        /// </summary>
        /// <param name="currentTextBox">The current text box.</param>
        private void CheckHousingTagalongAreaValidation(TerraScanTextBox currentTextBox)
        {
            double tagalongWidthValue = 0;
            double typedTagalongWidthValue = 0;
            double maxWidth = 0;
            double minWidth = 0;
            string tagalongWidthDescription = string.Empty;
            double.TryParse(this.ManufacturedHousingTagalongStyleCombo.SelectedValue.ToString(), out tagalongWidthValue);
            double.TryParse(currentTextBox.Text.Trim().ToString(), out typedTagalongWidthValue);
            DataRow[] tagalongWidthDataRow;
            tagalongWidthDataRow = this.houseTypeCollectionDataSet.Tables["ResidenceStyle"].Select("key = " + "'" + tagalongWidthValue + "'" + "AND SecondaryStyles_Id = " + "'" + this.houseTypeCollectionDataSet.Tables["SecondaryStyles"].Rows[0]["SecondaryStyles_Id"] + "'");

            if (tagalongWidthDataRow.Length > 0)
            {
                foreach (DataRow widthRow in tagalongWidthDataRow)
                {
                    if (currentTextBox.Tag.ToString() == "TagalongWidth")
                    {
                        double.TryParse(widthRow.ItemArray[3].ToString(), out maxWidth);
                        double.TryParse(widthRow.ItemArray[4].ToString(), out minWidth);
                        tagalongWidthDescription = widthRow.ItemArray[1].ToString();
                    }
                    else
                    {
                        double.TryParse(widthRow.ItemArray[5].ToString(), out maxWidth);
                        double.TryParse(widthRow.ItemArray[6].ToString(), out minWidth);
                        tagalongWidthDescription = widthRow.ItemArray[1].ToString();
                    }
                }

                if (typedTagalongWidthValue >= minWidth && typedTagalongWidthValue <= maxWidth)
                {
                    this.componentTabSelected = false;

                    if (!string.IsNullOrEmpty(this.ManufacturedHousingSecondaryWidthTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.ManufacturedHousingSecondaryLengthTextBox.Text.Trim()))
                    {
                        this.CalculateTagolongManufactureAreaTotal();
                    }
                }
                else
                {
                    if (!this.tabProcess)
                    {
                        if (currentTextBox.Tag.ToString() == "TagalongWidth")
                        {
                            if (!this.saveProcess)
                            {
                                MessageBox.Show(tagalongWidthDescription + " Width must be " + minWidth + " through " + maxWidth, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            if (!this.saveProcess)
                            {
                                MessageBox.Show(tagalongWidthDescription + " Length must be " + minWidth + " through " + maxWidth, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                        this.componentTabSelected = true;
                        currentTextBox.Focus();
                        return;
                    }
                    else
                    {
                        currentTextBox.Focus();
                        this.tabProcess = false;
                    }
                }
            }
        }

        /// <summary>
        /// Checks the quality range.
        /// </summary>
        /// <param name="selectedTextBox">The selected text box.</param>
        private void CheckQualityRange(TerraScanTextBox selectedTextBox)
        {
            selectedTextBox.TextCustomFormat = "#,##0.00";
            selectedTextBox.ApplyCFGFormat = false;
            double leaveQuality = 0.0;
            double.TryParse(selectedTextBox.Text.Trim(), out leaveQuality);

            // Checks the Quality Value range
            if (leaveQuality >= this.lowQuality && leaveQuality <= this.highQuality)
            {
                this.componentTabSelected = false;
            }
            else
            {
                if (!this.tabProcess)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("QualityErrorMsg") + this.lowQuality + SharedFunctions.GetResourceString("ErrorMsgThrough") + this.highQuality, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.componentTabSelected = true;
                    selectedTextBox.Focus();
                    return;
                }
                else
                {
                    selectedTextBox.Focus();
                    this.tabProcess = false;
                }
            }
        }

        /// <summary>
        /// Bases the quality description.
        /// </summary>
        /// <param name="keyValue">The key value.</param>
        /// <returns>BaseQualityDescription</returns>
        private string BaseQualityDescription(double keyValue)
        {
            DataRow[] qualityRow;
            DataRow[] decimalQualityRow;
            DataTable qualityDataTable = new DataTable();
            DataTable decimalQualityDataTable = new DataTable();

            string description = string.Empty;
            int numericPart = Convert.ToInt32(Math.Floor(keyValue));
            double decimalPart = keyValue - Math.Floor(keyValue);

            if (keyValue >= this.lowQuality && keyValue <= this.highQuality)
            {
                if (decimalPart > 0)
                {
                    qualityRow = this.houseTypeCollectionDataSet.Tables["ResidenceTypeQuality"].Select("Key = " + "'" + numericPart + "'");
                    decimalQualityRow = this.houseTypeCollectionDataSet.Tables["ResidenceTypeQuality"].Select("Key = " + "'" + (numericPart + 1) + "'");
                    qualityDataTable = this.DataRowToDataTable(qualityRow);
                    decimalQualityDataTable = this.DataRowToDataTable(decimalQualityRow);
                    description = qualityDataTable.Rows[0]["Description"].ToString() + " / " + decimalQualityDataTable.Rows[0]["Description"].ToString();
                    if (this.houseType == (int)HouseType.SingleFamilyResidence || this.houseType == (int)HouseType.LowRiseMultiple || this.houseType == (int)HouseType.TownHouseEndUnit || this.houseType == (int)HouseType.TownHouseInsideUnit || this.houseType == (int)HouseType.Duplex)
                    {
                        this.DefaultHeightTextBox.Text = qualityDataTable.Rows[0]["DefaultHeight"].ToString();
                    }
                    else
                    {
                        this.ManufacturedHousingDefaultHeightTextBox.Text = qualityDataTable.Rows[0]["DefaultHeight"].ToString();
                    }

                    return description;
                }
                else
                {
                    qualityRow = this.houseTypeCollectionDataSet.Tables["ResidenceTypeQuality"].Select("Key = " + "'" + numericPart + "'");
                    qualityDataTable = this.DataRowToDataTable(qualityRow);
                    description = qualityDataTable.Rows[0]["Description"].ToString();
                    if (this.houseType == (int)HouseType.SingleFamilyResidence || this.houseType == (int)HouseType.LowRiseMultiple || this.houseType == (int)HouseType.TownHouseEndUnit || this.houseType == (int)HouseType.TownHouseInsideUnit || this.houseType == (int)HouseType.Duplex)
                    {
                        this.DefaultHeightTextBox.Text = qualityDataTable.Rows[0]["DefaultHeight"].ToString();
                    }
                    else
                    {
                        this.ManufacturedHousingDefaultHeightTextBox.Text = qualityDataTable.Rows[0]["DefaultHeight"].ToString();
                    }

                    return description;
                }
            }
            else
            {
                this.DefaultHeightTextBox.Text = string.Empty;
                this.ManufacturedHousingDefaultHeightTextBox.Text = string.Empty;
                return description;
            }
        }

        /// <summary>
        /// Calculates the total area.
        /// </summary>
        private void CalculateTotalArea()
        {
            double primaryArea = 0.0;
            double secondaryArea = 0.0;

            if (this.houseType == (int)HouseType.SingleFamilyResidence || this.houseType == (int)HouseType.LowRiseMultiple || this.houseType == (int)HouseType.TownHouseEndUnit || this.houseType == (int)HouseType.TownHouseInsideUnit || this.houseType == (int)HouseType.Duplex)
            {
                double.TryParse(this.PrimaryAreaTextBox.Text.Trim().Replace(",", ""), out primaryArea);
                double.TryParse(this.SecondaryAreaTextBox.Text.Trim().Replace(",", ""), out secondaryArea);

                if (primaryArea != 0.0)
                {
                    this.PrimaryAreaTextBox.Text = primaryArea.ToString("#,##0");
                }

                if (secondaryArea != 0.0)
                {
                    this.SecondaryAreaTextBox.Text = secondaryArea.ToString("#,##0");
                }

                this.TotalAreaTextBox.Text = Convert.ToString(primaryArea + secondaryArea);
            }
            else if (this.houseType == (int)HouseType.ManufacturedHousing)
            {
                double.TryParse(this.ManufacturedHousingPrimaryAreaTextBox.Text.Trim().Replace(",", ""), out primaryArea);
                double.TryParse(this.ManufacturedHousingSecondaryAreaTextBox.Text.Trim().Replace(",", ""), out secondaryArea);

                if (primaryArea != 0.0)
                {
                    this.ManufacturedHousingPrimaryAreaTextBox.Text = primaryArea.ToString("#,##0");
                }

                if (secondaryArea != 0.0)
                {
                    this.ManufacturedHousingSecondaryAreaTextBox.Text = secondaryArea.ToString("#,##0");
                }

                this.ManufacturedHousingTotalAreaTextBox.Text = Convert.ToString(primaryArea + secondaryArea);
            }

            this.totalFloorArea = primaryArea + secondaryArea;
            if (this.totalFloorArea != 0)
            {
                if (this.houseType == (int)HouseType.SingleFamilyResidence || this.houseType == (int)HouseType.LowRiseMultiple || this.houseType == (int)HouseType.TownHouseEndUnit || this.houseType == (int)HouseType.TownHouseInsideUnit || this.houseType == (int)HouseType.Duplex)
                {
                    this.sitePrimaryPercentage = ((primaryArea * 100) / this.totalFloorArea);
                    this.siteSecondaryPercentage = ((secondaryArea * 100) / this.totalFloorArea);
                    this.PrimaryPercentageTextBox.Text = Convert.ToString(Math.Round((primaryArea * 100) / this.totalFloorArea));
                    this.SecondaryPercentageTextBox.Text = Convert.ToString(Math.Round((secondaryArea * 100) / this.totalFloorArea));
                }
                else if (this.houseType == (int)HouseType.ManufacturedHousing)
                {
                    this.manufacturePrimaryPercentage = ((primaryArea * 100) / this.totalFloorArea);
                    this.manufactureSecondaryPercentage = ((secondaryArea * 100) / this.totalFloorArea);
                    this.ManufacturedHousingPrimaryPercentageTextBox.Text = Convert.ToString(Math.Round((primaryArea * 100) / this.totalFloorArea));
                    this.ManufacturedHousingSecondaryPercentageTextBox.Text = Convert.ToString(Math.Round((secondaryArea * 100) / this.totalFloorArea));
                }
            }
            else
            {
                this.PrimaryPercentageTextBox.Text = "0";
                this.SecondaryPercentageTextBox.Text = "0";
            }
        }

        /// <summary>
        /// LoadAdjustmentDefaultValue
        /// </summary>
        private void LoadAdjustmentDefaultValue()
        {
            if (this.adjustmentDefaultDataSet.Tables.Count > 0 && this.adjustmentDefaultDataSet.Tables[0].Rows.Count > 0)
            {
                this.CostDefaultLabel.Text = this.adjustmentDefaultDataSet.Tables[0].Rows[0]["LocalMultiplier"].ToString();

                if (this.houseType == (int)HouseType.SingleFamilyResidence || this.houseType == (int)HouseType.LowRiseMultiple || this.houseType == (int)HouseType.TownHouseEndUnit || this.houseType == (int)HouseType.TownHouseInsideUnit || this.houseType == (int)HouseType.Duplex)
                {
                    this.EnergyZoneLabel.Text = this.adjustmentDefaultDataSet.Tables[0].Rows[0]["EnergyZone"].ToString();
                    this.FoundationLabel.Text = this.adjustmentDefaultDataSet.Tables[0].Rows[0]["FoundationZone"].ToString();
                    this.HillSideLabel.Text = this.adjustmentDefaultDataSet.Tables[0].Rows[0]["HillsideZone"].ToString();
                    this.SelsmicLabel.Text = this.adjustmentDefaultDataSet.Tables[0].Rows[0]["SeismicZone"].ToString();
                    this.WindLabel.Text = this.adjustmentDefaultDataSet.Tables[0].Rows[0]["WindZone"].ToString();
                }
                else
                {
                    this.ManufacturedHousingAdjustmentWallEnergyLabel.Text = this.adjustmentDefaultDataSet.Tables[0].Rows[0]["WallEnergyZone"].ToString();
                }
            }
        }

        /// <summary>
        /// Loads the adjustment.
        /// </summary>
        private void LoadAdjustment()
        {
            if (this.houseType == (int)HouseType.SingleFamilyResidence || this.houseType == (int)HouseType.LowRiseMultiple || this.houseType == (int)HouseType.TownHouseEndUnit || this.houseType == (int)HouseType.TownHouseInsideUnit || this.houseType == (int)HouseType.Duplex)
            {
                // Load Energy
                this.EnergyAdjustmentComboBox.DataSource = this.adjustmentDataSet.Tables["_EnergyZone"];
                this.EnergyAdjustmentComboBox.DisplayMember = this.adjustmentDataSet.Tables["_EnergyZone"].Columns["Description"].ColumnName;
                this.EnergyAdjustmentComboBox.ValueMember = this.adjustmentDataSet.Tables["_EnergyZone"].Columns["Key"].ColumnName;

                // Load Foundation
                this.FoundationAdjustmentCombo.DataSource = this.adjustmentDataSet.Tables["FoundationZone"];
                this.FoundationAdjustmentCombo.DisplayMember = this.adjustmentDataSet.Tables["FoundationZone"].Columns["Description"].ColumnName;
                this.FoundationAdjustmentCombo.ValueMember = this.adjustmentDataSet.Tables["FoundationZone"].Columns["Key"].ColumnName;

                // Load Hillside 
                this.HillSideAdjustmentCombo.DataSource = this.adjustmentDataSet.Tables["HillsideZone"];
                this.HillSideAdjustmentCombo.DisplayMember = this.adjustmentDataSet.Tables["HillsideZone"].Columns["Description"].ColumnName;
                this.HillSideAdjustmentCombo.ValueMember = this.adjustmentDataSet.Tables["HillsideZone"].Columns["Key"].ColumnName;

                // Load Seismic 
                this.SeismicAdjustmentCombo.DataSource = this.adjustmentDataSet.Tables["SeismicZone"];
                this.SeismicAdjustmentCombo.DisplayMember = this.adjustmentDataSet.Tables["SeismicZone"].Columns["Description"].ColumnName;
                this.SeismicAdjustmentCombo.ValueMember = this.adjustmentDataSet.Tables["SeismicZone"].Columns["Key"].ColumnName;

                // Load Wind 
                this.WindAdjustmentCombo.DataSource = this.adjustmentDataSet.Tables["WindZone"];
                this.WindAdjustmentCombo.DisplayMember = this.adjustmentDataSet.Tables["WindZone"].Columns["Description"].ColumnName;
                this.WindAdjustmentCombo.ValueMember = this.adjustmentDataSet.Tables["WindZone"].Columns["Key"].ColumnName;
            }
            else
            {
                // Load WallEnergy 
                this.WallEnergyAdjustmentCombo.DataSource = this.adjustmentDataSet.Tables["WallEnergyZone"];
                this.WallEnergyAdjustmentCombo.DisplayMember = this.adjustmentDataSet.Tables["WallEnergyZone"].Columns["Description"].ColumnName;
                this.WallEnergyAdjustmentCombo.ValueMember = this.adjustmentDataSet.Tables["WallEnergyZone"].Columns["Key"].ColumnName;
            }
        }

        /// <summary>
        /// Loads the general information.
        /// </summary>
        private void LoadGeneralInformation()
        {
            if (this.houseType == (int)HouseType.SingleFamilyResidence || this.houseType == (int)HouseType.LowRiseMultiple || this.houseType == (int)HouseType.TownHouseEndUnit || this.houseType == (int)HouseType.TownHouseInsideUnit || this.houseType == (int)HouseType.Duplex)
            {
                this.ZipCodeHeaderLabel.Visible = true;
                this.InformationSectionPanel.Visible = true;
                this.ManufacturedHousingMainPanel.Visible = false;
                this.AdjustmentPanel.Visible = true;
                this.WallEnergyAdjustmentPanel.Visible = false;
                this.AdjustmentTextPanel.Visible = true;
                this.ManufacturedHousingAdjustmentPanel.Visible = false;

                this.BaseQualityPanel.Visible = true;

                this.LoadAdjustment();
                this.LoadAdjustmentDefaultValue();
                this.LoadPrimaryStyle(this.PrimaryStyleCombo);
                this.LoadTagalongStyle(this.SecondaryStyleCombo);
                this.ZipCodeLabel.Text = this.zipCode.ToString();
                this.SingleBaseQualityNumberTextBox.Text = this.houseTypeCollectionDataSet.Tables["ResidenceTypeQuality"].Rows[2]["Key"].ToString();
                this.PropertyQualityTextBox.Text = this.houseTypeCollectionDataSet.Tables["ResidenceTypeQuality"].Rows[2]["Key"].ToString();
                this.SinglebaseQualityDescriptionLabel.Text = this.houseTypeCollectionDataSet.Tables["ResidenceTypeQuality"].Rows[2]["Description"].ToString();
                this.DefaultHeightTextBox.Text = this.houseTypeCollectionDataSet.Tables["ResidenceTypeQuality"].Rows[2]["DefaultHeight"].ToString();

                if (this.houseType == (int)HouseType.LowRiseMultiple)
                {
                    this.UnitTextBox.Text = this.houseTypeCollectionDataSet.Tables["ResidenceType"].Rows[0]["DefaultUnits"].ToString();
                    int.TryParse(this.UnitTextBox.Text.Trim(), out this.unitValue);
                    this.UnitPanel.Enabled = true;
                }
                else
                {
                    this.UnitTextBox.Text = "0";
                    this.UnitPanel.Enabled = false;
                }
            }
            else if (this.houseType == (int)HouseType.ManufacturedHousing)
            {
                this.ZipCodeHeaderLabel.Visible = false;
                this.ManufacturedHousingMainPanel.Visible = true;
                this.InformationSectionPanel.Visible = false;
                this.AdjustmentPanel.Visible = false;
                this.WallEnergyAdjustmentPanel.Visible = true;
                this.AdjustmentTextPanel.Visible = false;
                this.ManufacturedHousingAdjustmentPanel.Visible = true;

                this.BaseQualityPanel.Visible = false;
                this.HousingZipCodeLabel.Text = this.zipCode.ToString();
                this.BaseQualityNumberTextBox.Text = this.houseTypeCollectionDataSet.Tables["ResidenceTypeQuality"].Rows[2]["Key"].ToString();
                this.PropertyQualityTextBox.Text = this.houseTypeCollectionDataSet.Tables["ResidenceTypeQuality"].Rows[2]["Key"].ToString();
                this.BaseQuailtyDescriptionLabel.Text = this.houseTypeCollectionDataSet.Tables["ResidenceTypeQuality"].Rows[2]["Description"].ToString();
                this.ManufacturedHousingDefaultHeightTextBox.Text = this.houseTypeCollectionDataSet.Tables["ResidenceTypeQuality"].Rows[2]["DefaultHeight"].ToString();

                this.LoadAdjustment();
                this.LoadAdjustmentDefaultValue();
                this.LoadPrimaryStyle(this.ManufacturedHousingPrimaryStyleCombo);
                this.LoadTagalongStyle(this.ManufacturedHousingTagalongStyleCombo);
            }
        }

        /// <summary>
        /// Loads the primary style.
        /// </summary>
        /// <param name="styleCombo">The style combo.</param>
        private void LoadPrimaryStyle(ComboBox styleCombo)
        {
            DataRow[] primaryStyleDataRow;
            primaryStyleDataRow = this.houseTypeCollectionDataSet.Tables["ResidenceStyle"].Select("Styles_Id = " + "'" + this.houseTypeCollectionDataSet.Tables["Styles"].Rows[0]["Styles_Id"].ToString() + "'");
            styleCombo.DataSource = this.DataRowToDataTable(primaryStyleDataRow);
            styleCombo.DisplayMember = this.houseTypeCollectionDataSet.Tables["ResidenceStyle"].Columns["Description"].ColumnName;
            styleCombo.ValueMember = this.houseTypeCollectionDataSet.Tables["ResidenceStyle"].Columns["Key"].ColumnName;
            styleCombo.SelectedIndex = 0;
        }

        /// <summary>
        /// Loads the secondary style.
        /// </summary>
        /// <param name="secondStyleCombo">The second style combo.</param>
        private void LoadSecondaryStyle(ComboBox secondStyleCombo)
        {
            DataRow[] secondaryStyleDataRow;
            secondaryStyleDataRow = this.houseTypeCollectionDataSet.Tables["ResidenceStyle"].Select("SecondaryStyles_Id = " + "'" + this.houseTypeCollectionDataSet.Tables["Styles"].Rows[0]["Styles_Id"].ToString() + "'");
            secondStyleCombo.DataSource = this.DataRowToDataTable(secondaryStyleDataRow);
            secondStyleCombo.DisplayMember = this.houseTypeCollectionDataSet.Tables["ResidenceStyle"].Columns["Description"].ColumnName;
            secondStyleCombo.ValueMember = this.houseTypeCollectionDataSet.Tables["ResidenceStyle"].Columns["Key"].ColumnName;
            secondStyleCombo.SelectedIndex = 0;
        }

        /// <summary>
        /// Loads the secondary style.
        /// </summary>
        /// <param name="secondStyleCombo">The second style combo.</param>
        private void LoadTagalongStyle(ComboBox secondStyleCombo)
        {
            this.tagolongDataTable = this.houseTypeCollectionDataSet.Tables["ResidenceStyle"].Clone();
            DataRow[] secondaryStyleDataRow;
            secondaryStyleDataRow = this.houseTypeCollectionDataSet.Tables["ResidenceStyle"].Select("SecondaryStyles_Id = " + "'" + this.houseTypeCollectionDataSet.Tables["Styles"].Rows[0]["Styles_Id"].ToString() + "'");

            secondStyleCombo.DisplayMember = this.houseTypeCollectionDataSet.Tables["ResidenceStyle"].Columns["Description"].ColumnName;
            secondStyleCombo.ValueMember = this.houseTypeCollectionDataSet.Tables["ResidenceStyle"].Columns["Key"].ColumnName;
            DataRow tagolongDataRow = this.tagolongDataTable.NewRow();
            tagolongDataRow[this.houseTypeCollectionDataSet.Tables["ResidenceStyle"].Columns["Description"].ColumnName] = "";
            tagolongDataRow[this.houseTypeCollectionDataSet.Tables["ResidenceStyle"].Columns["Key"].ColumnName] = -1;
            this.tagolongDataTable.Rows.Add(tagolongDataRow);
            this.tagolongDataTable.Merge(this.DataRowToDataTable(secondaryStyleDataRow));

            secondStyleCombo.DataSource = this.tagolongDataTable;
            secondStyleCombo.SelectedIndex = 0;
        }

        /// <summary>
        /// Datas the row to data table.
        /// </summary>
        /// <param name="tempDataRow">The temp data row.</param>
        /// <returns>DataTable</returns>
        private DataTable DataRowToDataTable(DataRow[] tempDataRow)
        {
            DataSet convertedDataSet = new DataSet();
            convertedDataSet.Merge(tempDataRow);
            return convertedDataSet.Tables[0];
        }

        #endregion

        #region Component Tab

        /*  /// <summary>
        /// Locks the component tab.
        /// </summary>
        private void LockComponentTab()
        {
            this.validTotalFloorArea = true;
            this.LoadComponentTab();
            this.SectionGridView.Enabled = false;
            this.SystemGridView.Enabled = false;
            this.ComponentGridView.Enabled = false;
            this.NewComponentButton.Enabled = false;
            this.DeleteComponentButton.Enabled = false;
            this.EditComponentButton.Enabled = false;
            this.TemplateComponentButton.Enabled = false;
            this.SelectedComponentGridView.Enabled = false;
        } */

        /// <summary>
        /// RemoveSectionXmlEmptyRow
        /// </summary>
        /// <returns>xml</returns>
        private string RemoveSectionXmlEmptyRow()
        {
            DataTable emptyRowSection = new DataTable();
            emptyRowSection = this.sectionDataTable.Copy();
            DataRow[] emptyRow = emptyRowSection.Select(SharedFunctions.GetResourceString("EmptyRecordValidation"));

            foreach (DataRow empty in emptyRow)
            {
                emptyRowSection.Rows.Remove(empty);
            }

            return TerraScanCommon.GetXmlString(emptyRowSection);
        }

        /// <summary>
        /// Changes the color of the row back.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void ChangeRowBackColor(int rowIndex)
        {
            this.SelectedComponentGridView.Rows[rowIndex].Cells["Code"].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.SelectedComponentGridView.Rows[rowIndex].Cells["SelectedSystem"].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);
            this.SelectedComponentGridView.Rows[rowIndex].Cells["SelectedSystemDescription"].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);
            this.SelectedComponentGridView.Rows[rowIndex].Cells["Units"].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.SelectedComponentGridView.Rows[rowIndex].Cells["Units"].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);
            this.SelectedComponentGridView.Rows[rowIndex].Cells["Percentage"].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.SelectedComponentGridView.Rows[rowIndex].Cells["Percentage"].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);
            this.SelectedComponentGridView.Rows[rowIndex].Cells["QualityID"].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.SelectedComponentGridView.Rows[rowIndex].Cells["QualityID"].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);
            this.SelectedComponentGridView.Rows[rowIndex].Cells["QualityDescription"].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);

            this.SelectedComponentGridView.Rows[rowIndex].Cells["Code"].Appearance.ForeColor = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells["SelectedSystem"].Appearance.ForeColorDisabled = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells["SelectedSystemDescription"].Appearance.ForeColorDisabled = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells["Units"].Appearance.ForeColor = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells["Units"].Appearance.ForeColorDisabled = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells["Percentage"].Appearance.ForeColor = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells["Percentage"].Appearance.ForeColorDisabled = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells["QualityID"].Appearance.ForeColor = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells["QualityID"].Appearance.ForeColorDisabled = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells["QualityDescription"].Appearance.ForeColorDisabled = Color.White;
        }

        /// <summary>
        /// Restores the color of the row back.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void RestoreRowBackColor(int rowIndex)
        {
            this.SelectedComponentGridView.Rows[rowIndex].Cells["Code"].Appearance.BackColor = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells["SelectedSystem"].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);
            this.SelectedComponentGridView.Rows[rowIndex].Cells["SelectedSystemDescription"].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);

            if (this.SelectedComponentGridView.Rows[rowIndex].Cells["Units"].Activation.ToString() == "AllowEdit")
            {
                this.SelectedComponentGridView.Rows[rowIndex].Cells["Units"].Appearance.BackColor = Color.White;
            }
            else
            {
                this.SelectedComponentGridView.Rows[rowIndex].Cells["Units"].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);
            }

            if (this.SelectedComponentGridView.Rows[rowIndex].Cells["Percentage"].Activation.ToString() == "AllowEdit")
            {
                this.SelectedComponentGridView.Rows[rowIndex].Cells["Percentage"].Appearance.BackColor = Color.White;
            }
            else
            {
                this.SelectedComponentGridView.Rows[rowIndex].Cells["Percentage"].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);
            }

            if (this.SelectedComponentGridView.Rows[rowIndex].Cells["QualityID"].Activation.ToString() == "AllowEdit")
            {
                this.SelectedComponentGridView.Rows[rowIndex].Cells["QualityID"].Appearance.BackColor = Color.White;
            }
            else
            {
                this.SelectedComponentGridView.Rows[rowIndex].Cells["QualityID"].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);
            }

            this.SelectedComponentGridView.Rows[rowIndex].Cells["QualityDescription"].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);

            this.SelectedComponentGridView.Rows[rowIndex].Cells["Code"].Appearance.ForeColor = Color.Black;
            this.SelectedComponentGridView.Rows[rowIndex].Cells["SelectedSystem"].Appearance.ForeColorDisabled = Color.Black;
            this.SelectedComponentGridView.Rows[rowIndex].Cells["SelectedSystemDescription"].Appearance.ForeColorDisabled = Color.Black;
            this.SelectedComponentGridView.Rows[rowIndex].Cells["Units"].Appearance.ForeColor = Color.Black;
            this.SelectedComponentGridView.Rows[rowIndex].Cells["Units"].Appearance.ForeColorDisabled = Color.Black;
            this.SelectedComponentGridView.Rows[rowIndex].Cells["Percentage"].Appearance.ForeColor = Color.Black;
            this.SelectedComponentGridView.Rows[rowIndex].Cells["Percentage"].Appearance.ForeColorDisabled = Color.Black;
            this.SelectedComponentGridView.Rows[rowIndex].Cells["QualityID"].Appearance.ForeColor = Color.Black;
            this.SelectedComponentGridView.Rows[rowIndex].Cells["QualityID"].Appearance.ForeColorDisabled = Color.Black;
            this.SelectedComponentGridView.Rows[rowIndex].Cells["QualityDescription"].Appearance.ForeColorDisabled = Color.Black;
        }

        /// <summary>
        /// Checks the component code.
        /// </summary>
        /// <returns>StringBuilder</returns>
        private StringBuilder CheckComponentCode()
        {
            DataRow[] componentCodeRows;
            StringBuilder codeExpression = new StringBuilder();
            ArrayList codeList = new ArrayList();
            if (this.systemDataTable.Rows.Count > 0)
            {
                for (int count = 0; count < this.systemDataTable.Rows.Count; count++)
                {
                    componentCodeRows = this.houseTypeCollectionDataSet.Tables["Components"].Select("Components_Id = " + this.systemDataTable.Rows[count]["ResidenceSystem_Id"].ToString());

                    if (componentCodeRows.Length > 0)
                    {
                        foreach (DataRow row in componentCodeRows)
                        {
                            codeList.Add(row[1].ToString());
                        }
                    }
                }

                for (int list = 0; list < codeList.Count; list++)
                {
                    if (list != codeList.Count - 1)
                    {
                        codeExpression.Append("Components_Id = " + "'" + codeList[list].ToString() + "'" + " OR ");
                    }
                    else
                    {
                        codeExpression.Append("Components_Id = " + "'" + codeList[list].ToString() + "'");
                    }
                }
            }

            return codeExpression;
        }

        /// <summary>
        /// Removes the empty row.
        /// </summary>
        /// <param name="currentCombo">The current combo.</param>
        private void RemoveEmptyRow(ComboBox currentCombo)
        {
            DataRow[] removeRow;
            removeRow = this.tagolongDataTable.Select("Key = '-1'");

            if (removeRow.Length > 0)
            {
                this.tagolongDataTable.Rows.RemoveAt(0);
                currentCombo.DataSource = this.tagolongDataTable;
                currentCombo.DisplayMember = this.houseTypeCollectionDataSet.Tables["ResidenceStyle"].Columns["Description"].ColumnName;
                currentCombo.ValueMember = this.houseTypeCollectionDataSet.Tables["ResidenceStyle"].Columns["Key"].ColumnName;
            }
        }

        /// <summary>
        /// Calculates the manufacture area total.
        /// </summary>
        private void CalculatePrimaryManufactureAreaTotal()
        {
            double primaryWidthManufacture = 0;
            double primaryLengthManufacture = 0;
            double.TryParse(this.ManufacturedHousingPrimaryWidthTextBox.Text.Trim(), out primaryWidthManufacture);
            double.TryParse(this.ManufacturedHousingPrimaryLengthTextBox.Text.Trim(), out primaryLengthManufacture);

            this.ManufacturedHousingPrimaryAreaTextBox.Text = Convert.ToString(Math.Round(primaryWidthManufacture * primaryLengthManufacture));
            this.CalculateTotalFloor();
        }

        /// <summary>
        /// Calculates the Tagolong manufacture area total.
        /// </summary>
        private void CalculateTagolongManufactureAreaTotal()
        {
            double tagolongWidthManufacture = 0;
            double tagolongLengthManufacture = 0;
            double.TryParse(this.ManufacturedHousingSecondaryWidthTextBox.Text.Trim(), out tagolongWidthManufacture);
            double.TryParse(this.ManufacturedHousingSecondaryLengthTextBox.Text.Trim(), out tagolongLengthManufacture);

            this.ManufacturedHousingSecondaryAreaTextBox.Text = Convert.ToString(Math.Round(tagolongWidthManufacture * tagolongLengthManufacture));
            this.CalculateTotalFloor();
        }

        /// <summary>
        /// Calculates the total floor.
        /// </summary>
        private void CalculateTotalFloor()
        {
            double primaryArea = 0.0;
            double tagolongArea = 0.0;
            double.TryParse(this.ManufacturedHousingPrimaryAreaTextBox.Text.Trim(), out primaryArea);
            double.TryParse(this.ManufacturedHousingSecondaryAreaTextBox.Text.Trim(), out tagolongArea);

            this.totalFloorArea = primaryArea + tagolongArea;
            this.ManufacturedHousingTotalAreaTextBox.Text = Convert.ToString(primaryArea + tagolongArea);
            this.manufacturePrimaryPercentage = ((primaryArea * 100) / this.totalFloorArea);
            this.manufactureSecondaryPercentage = ((tagolongArea * 100) / this.totalFloorArea);
            this.ManufacturedHousingPrimaryPercentageTextBox.Text = Convert.ToString(Math.Round((primaryArea * 100) / this.totalFloorArea));
            this.ManufacturedHousingSecondaryPercentageTextBox.Text = Convert.ToString(Math.Round((tagolongArea * 100) / this.totalFloorArea));
        }

        /// <summary>
        /// Populates the section grid.
        /// </summary>
        /// <param name="selectedSectionDataTable">The selected section data table.</param>
        private void PopulateSectionGrid(DataTable selectedSectionDataTable)
        {
            this.SectionGridView.DataSource = selectedSectionDataTable;
            this.SectionGridView.Rows[this.SectionGridView.OriginalRowCount - 1].Cells[0].Selected = true;

            if (this.SectionGridView.OriginalRowCount > 1)
            {
                this.ButtonStatus(true);
            }
            else
            {
                this.ButtonStatus(false);
            }

            if (this.SectionGridView.OriginalRowCount > this.SectionGridView.NumRowsVisible)
            {
                this.SectionScrollBar.Visible = false;
            }
            else
            {
                this.SectionScrollBar.Visible = true;
            }
        }

        /// <summary>
        /// Buttons the status.
        /// </summary>
        /// <param name="status">if set to <c>true</c> [status].</param>
        private void ButtonStatus(bool status)
        {
            this.DeleteComponentButton.Enabled = status;
            this.EditComponentButton.Enabled = status;
        }

        /// <summary>
        /// Loads the selected component records.
        /// </summary>
        private void LoadSelectedComponentRecords()
        {
            if (this.selectedComponentDataTable.Rows.Count == 0)
            {
                DataColumn[] selectedComponentColumn = new DataColumn[] { new DataColumn("Code"), new DataColumn("SelectedSystem"), new DataColumn("SelectedSystemDescription"), new DataColumn("Units"), new DataColumn("Percentage"), new DataColumn("QualityID"), new DataColumn("QualityDescription"), new DataColumn("AllowQualityChangeFlag"), new DataColumn("PercentMaximum"), new DataColumn("PercentMinimum"), new DataColumn("UnitMaximum"), new DataColumn("UnitMinimum"), new DataColumn("SectionKeyValue"), new DataColumn("SystemKeyValue"), new DataColumn("SectionGroupID") };
                this.selectedComponentDataTable.Columns.AddRange(selectedComponentColumn);
                this.SelectedComponentGridView.DataSource = this.selectedComponentDataTable;
            }
        }

        /// <summary>
        /// Creates the typed component data table.
        /// </summary>
        /// <param name="typedRowArray">The typed row array.</param>
        private void CreateTypedComponentDataTable(DataRow[] typedRowArray)
        {
            int sectionKey = 0;
            int sectionGroupId = 0;
            int activeRow = this.SelectedComponentGridView.ActiveRow.Index;
            if (this.selectedComponentDataTable.Rows.Count > 0)
            {
                for (int rowCount = 0; rowCount < this.selectedComponentDataTable.Rows.Count; rowCount++)
                {
                    if (string.IsNullOrEmpty(this.selectedComponentDataTable.Rows[rowCount]["Code"].ToString()))
                    {
                        this.selectedComponentDataTable.Rows[rowCount].Delete();
                    }
                }
            }

            DataRow selectedDataRow = this.selectedComponentDataTable.NewRow();

            foreach (DataRow typedRow in typedRowArray)
            {
                if (this.SectionGridView.CurrentRow.Index >= 0)
                {
                    int.TryParse(this.sectionDataTable.Rows[this.SectionGridView.CurrentRow.Index]["SectionKey"].ToString(), out sectionKey);
                    selectedDataRow["SectionKeyValue"] = this.sectionDataTable.Rows[this.SectionGridView.CurrentRow.Index]["SectionKey"].ToString();

                    int.TryParse(this.sectionDataTable.Rows[this.SectionGridView.CurrentRow.Index]["GroupID"].ToString(), out sectionGroupId);
                    selectedDataRow["SectionGroupID"] = sectionGroupId;
                }

                selectedDataRow["Code"] = typedRow.ItemArray[0].ToString();
                if (this.SystemGridView.CurrentRowIndex >= 0)
                {
                    DataRow[] selectedSystemDataRow;
                    DataTable selectedSystemDataTable = new DataTable();
                    selectedSystemDataRow = this.houseTypeCollectionDataSet.Tables["ResidenceSystem"].Select("ResidenceSystem_Id = " + "'" + typedRow.ItemArray[13].ToString() + "'");

                    if (selectedSystemDataRow.Length > 0)
                    {
                        selectedSystemDataTable = this.DataRowToDataTable(selectedSystemDataRow);
                        selectedDataRow["SelectedSystem"] = selectedSystemDataTable.Rows[0]["Description"].ToString();
                        selectedDataRow["SystemKeyValue"] = selectedSystemDataTable.Rows[0]["Key"].ToString();
                    }
                }

                selectedDataRow["SelectedSystemDescription"] = typedRow.ItemArray[1].ToString();
                selectedDataRow["Units"] = string.Empty;
                selectedDataRow["Percentage"] = string.Empty;
                selectedDataRow["QualityID"] = string.Empty;
                selectedDataRow["QualityDescription"] = string.Empty;
                selectedDataRow["AllowQualityChangeFlag"] = typedRow.ItemArray[5].ToString();
                selectedDataRow["PercentMaximum"] = typedRow.ItemArray[8].ToString();
                selectedDataRow["PercentMinimum"] = typedRow.ItemArray[9].ToString();
                selectedDataRow["UnitMaximum"] = typedRow.ItemArray[10].ToString();
                selectedDataRow["UnitMinimum"] = typedRow.ItemArray[11].ToString();
            }

            if (this.selectedComponentDataTable.Rows.Count > 0)
            {
                if (this.selectedComponentDataTable.Rows.Count == activeRow)
                {
                    this.selectedComponentDataTable.Rows.RemoveAt(activeRow - 1);
                    this.selectedComponentDataTable.Rows.InsertAt(selectedDataRow, activeRow - 1);
                    this.SelectedComponentGridView.Rows[activeRow - 1].Cells[0].Activated = true;
                }
                else
                {
                    this.selectedComponentDataTable.Rows.RemoveAt(activeRow);
                    this.selectedComponentDataTable.Rows.InsertAt(selectedDataRow, activeRow);
                    this.SelectedComponentGridView.Rows[activeRow].Cells[0].Activated = true;
                }
            }

            this.ToEnableEditButtonInMasterForm();
            this.SelectedComponentGridView.DataSource = this.selectedComponentDataTable;
            this.FilterComponentRow(sectionKey);
            this.executionRequied = true;
        }

        /// <summary>
        /// Filters the component row.
        /// </summary>
        /// <param name="keyValue">The key value.</param>
        private void FilterComponentRow(int keyValue)
        {
            FilterCondition filterCondition = new FilterCondition();
            filterCondition.CompareValue = keyValue;
            this.SelectedComponentGridView.DisplayLayout.Bands[0].ColumnFilters["SectionKeyValue"].FilterConditions.Clear();
            this.SelectedComponentGridView.DisplayLayout.Bands[0].ColumnFilters["SectionKeyValue"].FilterConditions.Add(filterCondition);
            this.TemplateButtonStatus();
        }

        /// <summary>
        /// Creates the selected component data table.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void CreateSelectedComponentDataTable(DataGridViewSelectedRowCollection selectedRow)
        {
            int sectionKey = 0;
            int sectionGroupId = 0;
            if (this.selectedComponentDataTable.Rows.Count > 0)
            {
                for (int rowCount = 0; rowCount < this.selectedComponentDataTable.Rows.Count; rowCount++)
                {
                    if (string.IsNullOrEmpty(this.selectedComponentDataTable.Rows[rowCount]["Code"].ToString()))
                    {
                        this.selectedComponentDataTable.Rows[rowCount].Delete();
                    }
                }
            }

            for (int selectedRowCount = 0; selectedRowCount < selectedRow.Count; selectedRowCount++)
            {
                DataRow selectedDataRow = this.selectedComponentDataTable.NewRow();

                if (this.SectionGridView.CurrentRow.Index >= 0)
                {
                    int.TryParse(this.sectionDataTable.Rows[this.SectionGridView.CurrentRow.Index]["SectionKey"].ToString(), out sectionKey);
                    selectedDataRow["SectionKeyValue"] = this.sectionDataTable.Rows[this.SectionGridView.CurrentRow.Index]["SectionKey"].ToString();

                    int.TryParse(this.sectionDataTable.Rows[this.SectionGridView.CurrentRow.Index]["GroupID"].ToString(), out sectionGroupId);
                    selectedDataRow["SectionGroupID"] = sectionGroupId;
                }

                selectedDataRow["Code"] = selectedRow[selectedRowCount].Cells["ComponentKey"].Value.ToString();
                if (this.SystemGridView.CurrentRowIndex >= 0)
                {
                    selectedDataRow["SelectedSystem"] = this.SystemGridView.Rows[this.SystemGridView.CurrentRowIndex].Cells["SystemDescription"].Value.ToString();
                    selectedDataRow["SystemKeyValue"] = this.SystemGridView.Rows[this.SystemGridView.CurrentRowIndex].Cells["SystemKeyValue"].Value.ToString();
                }

                selectedDataRow["SelectedSystemDescription"] = selectedRow[selectedRowCount].Cells["ComponentDescription"].Value.ToString();
                selectedDataRow["Units"] = string.Empty;
                selectedDataRow["Percentage"] = string.Empty;
                selectedDataRow["QualityID"] = string.Empty;
                selectedDataRow["QualityDescription"] = string.Empty;
                selectedDataRow["AllowQualityChangeFlag"] = selectedRow[selectedRowCount].Cells["ComponentAllowQualityChangeFlag"].Value.ToString();
                selectedDataRow["PercentMaximum"] = selectedRow[selectedRowCount].Cells["ComponentPercentMaximum"].Value.ToString();
                selectedDataRow["PercentMinimum"] = selectedRow[selectedRowCount].Cells["ComponentPercentMinimum"].Value.ToString();
                selectedDataRow["UnitMaximum"] = selectedRow[selectedRowCount].Cells["ComponentUnitMaximum"].Value.ToString();
                selectedDataRow["UnitMinimum"] = selectedRow[selectedRowCount].Cells["ComponentUnitMinimum"].Value.ToString();

                this.selectedComponentDataTable.Rows.InsertAt(selectedDataRow, this.SelectedComponentGridView.Rows.Count);
                this.SelectedComponentGridView.Rows[this.SelectedComponentGridView.Rows.Count - 1].Activate();
            }

            this.ToEnableEditButtonInMasterForm();
            this.SelectedComponentGridView.DataSource = this.selectedComponentDataTable;
            this.FilterComponentRow(sectionKey);
        }

        /// <summary>
        /// Loads the selected component grid.
        /// </summary>
        /// <param name="newSelectedDataTable">The new selected data table.</param>
        ////private void LoadSelectedComponentGrid(int keyValue)
        ////{
        ////    DataRow[] selectedComponentDataRow;
        ////    DataTable tempSelectedComponentDataTable = new DataTable();
        ////    tempSelectedComponentDataTable = this.selectedComponentDataTable.Clone();
        ////    selectedComponentDataRow = this.selectedComponentDataTable.Select("SectionKeyValue = " + "'" + keyValue + "'");

        ////    if (selectedComponentDataRow.Length > 0)
        ////    {
        ////        tempSelectedComponentDataTable = this.DataRowToDataTable(selectedComponentDataRow);
        ////        this.SelectedComponentGridView.DataSource = null;
        ////        this.SelectedComponentGridView.DataSource = tempSelectedComponentDataTable;
        ////        this.SelectedComponentGridView.Rows[this.SelectedComponentGridView.Rows.Count - 1].Selected = true;
        ////    }
        ////    else
        ////    {
        ////        this.SelectedComponentGridView.DataSource = tempSelectedComponentDataTable;
        ////    }

        ////    this.TemplateButtonStatus();

        ////    ////this.SelectedComponentGridView.Rows[this.SelectedComponentGridView.Rows.Count - 1].Update();
        ////    ////this.SelectedComponentGridView.Rows[this.SelectedComponentGridView.Rows.Count - 1].Cells[0].Selected = true;                       
        ////}

        /// <summary>
        /// Templates the button status.
        /// </summary>
        private void TemplateButtonStatus()
        {
            if (this.SectionGridView.CurrentRow != null)
            {
                if (this.SectionGridView.CurrentRow.Index >= 0)
                {
                    if (this.SectionGridView.CurrentRow.Index == 0)
                    {
                        if (this.SelectedComponentGridView.Rows.FilteredInRowCount > 0)
                        {
                            this.TemplateComponentButton.Enabled = false;
                        }
                        else
                        {
                            this.TemplateComponentButton.Enabled = true;
                        }
                    }
                    else
                    {
                        this.TemplateComponentButton.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Customizes all grid view.
        /// </summary>
        private void CustomizeAllGridView()
        {
            this.CustomizeSectionGrid();
            this.CustomizeSystemGrid();
            this.CustomizeComponentGrid();
        }

        /// <summary>
        /// Loads the component tab.
        /// </summary>
        private void LoadComponentTab()
        {
            this.LoadSectionsGrid();
        }

        /// <summary>
        /// Customizes the section grid.
        /// </summary>
        private void CustomizeSectionGrid()
        {
            this.SectionGridView.AllowUserToResizeColumns = false;
            this.SectionGridView.AutoGenerateColumns = false;
            this.SectionGridView.AllowUserToResizeRows = false;
            this.SectionGridView.StandardTab = true;
            this.SectionGridView.Columns["Description"].DataPropertyName = "Description";
            this.SectionGridView.Columns["ResidenceGroupType_Id"].DataPropertyName = "ResidenceGroupType_Id";
            this.SectionGridView.Columns["SectionKey"].DataPropertyName = "Key";
            this.SectionGridView.Columns["SquareFeet"].DataPropertyName = "SquareFeet";
            this.SectionGridView.Columns["BaseQuality"].DataPropertyName = "BaseQuality";
            this.SectionGridView.Columns["QualityDescription"].DataPropertyName = "QualityDescription";
        }

        /// <summary>
        /// Customizes the system grid.
        /// </summary>
        private void CustomizeSystemGrid()
        {
            this.SystemGridView.AllowUserToResizeColumns = false;
            this.SystemGridView.AutoGenerateColumns = false;
            this.SystemGridView.AllowUserToResizeRows = false;
            this.SystemGridView.StandardTab = true;
            this.SystemGridView.Columns["SystemDescription"].DataPropertyName = "Description";
            this.SystemGridView.Columns["ResidenceSystem_Id"].DataPropertyName = "ResidenceSystem_Id";
            this.SystemGridView.Columns["SystemKeyValue"].DataPropertyName = "Key";
        }

        /// <summary>
        /// Customizes the system grid.
        /// </summary>
        private void CustomizeComponentGrid()
        {
            this.ComponentGridView.AllowUserToResizeColumns = false;
            this.ComponentGridView.AutoGenerateColumns = false;
            this.ComponentGridView.AllowUserToResizeRows = false;
            this.ComponentGridView.StandardTab = true;
            this.ComponentGridView.MultiSelect = true;
            this.ComponentGridView.Columns["ListBoxDisplay"].DataPropertyName = "ListBoxDisplay";
            this.ComponentGridView.Columns["ComponentKey"].DataPropertyName = "Key";
            this.ComponentGridView.Columns["ComponentDescription"].DataPropertyName = "Description";
            this.ComponentGridView.Columns["ComponentAllowQualityChangeFlag"].DataPropertyName = "AllowQualityChangeFlag";
            this.ComponentGridView.Columns["ComponentPercentMaximum"].DataPropertyName = "PercentMaximum";
            this.ComponentGridView.Columns["ComponentPercentMinimum"].DataPropertyName = "PercentMinimum";
            this.ComponentGridView.Columns["ComponentUnitMaximum"].DataPropertyName = "UnitMaximum";
            this.ComponentGridView.Columns["ComponentUnitMinimum"].DataPropertyName = "UnitMinimum";
        }

        /// <summary>
        /// Loads the sections grid.
        /// </summary>
        private void LoadSectionsGrid()
        {
            DataRow[] sectionDataRow;
            DataTable mainDataTable = new DataTable();
            sectionDataRow = this.houseTypeCollectionDataSet.Tables["ResidenceGroupType"].Select("Key = 1");
            mainDataTable = this.DataRowToDataTable(sectionDataRow);

            if (this.sectionDataTable.Columns.Count <= 0)
            {
                if (this.sectionDataTable.Rows.Count == 0)
                {
                    DataColumn[] sectionColumn = new DataColumn[] { new DataColumn("Description"), new DataColumn("ResidenceGroupType_Id"), new DataColumn("SectionKey"), new DataColumn("SquareFeet"), new DataColumn("BaseQuality"), new DataColumn("QualityDescription"), new DataColumn("TagalongWidth"), new DataColumn("TagalongLength"), new DataColumn("GroupID") };
                    this.sectionDataTable.Columns.AddRange(sectionColumn);
                }
            }

            DataRow sectionMainDataRow = this.sectionDataTable.NewRow();
            sectionMainDataRow["GroupID"] = "1";
            sectionMainDataRow["Description"] = mainDataTable.Rows[0]["Description"].ToString();
            sectionMainDataRow["ResidenceGroupType_Id"] = mainDataTable.Rows[0]["ResidenceGroupType_Id"].ToString();
            sectionMainDataRow["SectionKey"] = mainDataTable.Rows[0]["Key"].ToString();

            if (this.houseType == (int)HouseType.SingleFamilyResidence || this.houseType == (int)HouseType.LowRiseMultiple || this.houseType == (int)HouseType.TownHouseEndUnit || this.houseType == (int)HouseType.TownHouseInsideUnit || this.houseType == (int)HouseType.Duplex)
            {
                sectionMainDataRow["SquareFeet"] = this.TotalAreaTextBox.Text.Trim();
                sectionMainDataRow["BaseQuality"] = this.SingleBaseQualityNumberTextBox.Text.Trim();
                sectionMainDataRow["QualityDescription"] = this.SinglebaseQualityDescriptionLabel.Text.Trim();
            }
            else
            {
                sectionMainDataRow["SquareFeet"] = this.ManufacturedHousingTotalAreaTextBox.Text.Trim();
                sectionMainDataRow["BaseQuality"] = this.BaseQualityNumberTextBox.Text.Trim();
                sectionMainDataRow["QualityDescription"] = this.BaseQuailtyDescriptionLabel.Text.Trim();
            }

            if (this.SectionGridView.OriginalRowCount == 0)
            {
                this.sectionDataTable.Rows.InsertAt(sectionMainDataRow, 0);
            }
            else
            {
                this.sectionDataTable.Rows[0]["SquareFeet"] = sectionMainDataRow["SquareFeet"].ToString();
                this.sectionDataTable.Rows[0]["BaseQuality"] = sectionMainDataRow["BaseQuality"].ToString();
                this.sectionDataTable.Rows[0]["QualityDescription"] = sectionMainDataRow["QualityDescription"].ToString();
            }

            this.SectionGridView.DataSource = this.sectionDataTable;

            if (this.SectionGridView.OriginalRowCount > this.SectionGridView.NumRowsVisible)
            {
                this.SectionScrollBar.Visible = false;
            }
            else
            {
                this.SectionScrollBar.Visible = true;
            }

            if (this.SectionGridView.CurrentRow.Index >= 0)
            {
                this.ComponentInformationLabel.Text = this.sectionDataTable.Rows[this.SectionGridView.CurrentRow.Index]["SquareFeet"].ToString() + " SF Quality: " + this.sectionDataTable.Rows[this.SectionGridView.CurrentRow.Index]["BaseQuality"].ToString() + " - " + this.sectionDataTable.Rows[this.SectionGridView.CurrentRow.Index]["QualityDescription"].ToString();
            }
        }

        /// <summary>
        /// Sections the grid click.
        /// </summary>
        /// <param name="sectionRowIndex">Index of the section row.</param>
        private void SectionGridClick(int sectionRowIndex)
        {
            if (this.SectionGridView.OriginalRowCount > 0)
            {
                if (sectionRowIndex >= 0)
                {
                    int keyValue = 0;
                    int sectionGroupId = 0;
                    int.TryParse(this.SectionGridView.Rows[sectionRowIndex].Cells["ResidenceGroupType_Id"].Value.ToString(), out this.residenceGrouptType);
                    this.LoadSystemGrid(this.residenceGrouptType);
                    int.TryParse(this.sectionDataTable.Rows[sectionRowIndex]["SectionKey"].ToString(), out keyValue);
                    int.TryParse(this.sectionDataTable.Rows[sectionRowIndex]["GroupID"].ToString(), out sectionGroupId);
                    this.SelectedComponentGridView.DataSource = this.selectedComponentDataTable;
                    if (this.SelectedComponentGridView.Rows.Count > 0)
                    {
                        this.FilterComponentRow(keyValue);
                    }

                    this.ComponentInformationLabel.Text = this.sectionDataTable.Rows[sectionRowIndex]["SquareFeet"].ToString() + " SF Quality: " + this.sectionDataTable.Rows[sectionRowIndex]["BaseQuality"].ToString() + " - " + this.sectionDataTable.Rows[sectionRowIndex]["QualityDescription"].ToString();
                }
            }
        }

        /// <summary>
        /// Sections the grid click.
        /// </summary>
        /// <param name="systemRowIndex">Index of the system row.</param>
        private void SystemGridClick(int systemRowIndex)
        {
            if (this.SystemGridView.OriginalRowCount > 0)
            {
                if (systemRowIndex >= 0)
                {
                    int.TryParse(this.SystemGridView.Rows[systemRowIndex].Cells["ResidenceSystem_Id"].Value.ToString(), out this.selectedSystemId);
                    this.LoadComponentGrid(this.selectedSystemId);
                }
            }
        }

        /// <summary>
        /// Loads the system grid.
        /// </summary>
        /// <param name="residenceType">Type of the residence.</param>
        private void LoadSystemGrid(int residenceType)
        {
            // Getting ResidenceSystem
            DataRow[] systemDataRow;
            systemDataRow = this.houseTypeCollectionDataSet.Tables["ResidenceSystem"].Select("Systems_Id = " + "'" + this.houseTypeCollectionDataSet.Tables["Systems"].Rows[residenceType]["Systems_Id"].ToString() + "'");
            this.systemDataTable = this.DataRowToDataTable(systemDataRow);
            this.SystemGridView.DataSource = this.systemDataTable;

            if (this.SystemGridView.OriginalRowCount > this.SystemGridView.NumRowsVisible)
            {
                this.SystemScrollBar.Visible = false;
            }
            else
            {
                this.SystemScrollBar.Visible = true;
            }
        }

        /// <summary>
        /// Loads the selected componet grid.
        /// </summary>
        /// <param name="keyValue">The key value.</param>
        private void LoadSelectedComponetGrid(int keyValue)
        {
            DataRow[] systemDataRow;
            DataTable tempDataTable = new DataTable();
            tempDataTable = this.selectedComponentDataTable.Clone();
            systemDataRow = this.selectedComponentDataTable.Select("SectionKeyValue = " + "'" + keyValue + "'");

            if (systemDataRow.Length > 0)
            {
                tempDataTable = this.DataRowToDataTable(systemDataRow);
                this.SelectedComponentGridView.DataSource = tempDataTable;
            }
            else
            {
                this.SelectedComponentGridView.DataSource = tempDataTable;
            }
        }

        /// <summary>
        /// Loads the component grid.
        /// </summary>
        /// <param name="systemId">The system id.</param>
        private void LoadComponentGrid(int systemId)
        {
            DataRow[] componentDataRow;
            componentDataRow = this.houseTypeCollectionDataSet.Tables["ResidenceComponent"].Select("Components_Id = " + "'" + this.houseTypeCollectionDataSet.Tables["Components"].Rows[systemId]["Components_Id"].ToString() + "'");
            this.componentDataTable = this.DataRowToDataTable(componentDataRow);
            this.ComponentGridView.DataSource = this.componentDataTable;

            if (this.ComponentGridView.OriginalRowCount > this.ComponentGridView.NumRowsVisible)
            {
                this.ComponentScrollBar.Visible = false;
            }
            else
            {
                this.ComponentScrollBar.Visible = true;
            }
        }

        /// <summary>
        /// Checks the main component.
        /// </summary>
        /// <returns>Return</returns>
        private bool CheckMainComponent()
        {
            DataTable recordDataTable = new DataTable();
            recordDataTable = this.selectedComponentDataTable.Clone();

            DataRow[] componentRecords;
            componentRecords = this.selectedComponentDataTable.Select("SectionKeyValue = 1");

            if (componentRecords.Length > 0)
            {
                recordDataTable = this.DataRowToDataTable(componentRecords);
            }

            if (recordDataTable.Rows.Count > 0)
            {
                // Check Exterior Wall                
                int percentageTotal = 0;
                int percentageValue = 0;
                int roofingTotal = 0;
                int roofingValue = 0;

                for (int countPercentage = 0; countPercentage < recordDataTable.Rows.Count; countPercentage++)
                {
                    if (recordDataTable.Rows[countPercentage]["SelectedSystem"].ToString() == "Exterior Walls")
                    {
                        int.TryParse(recordDataTable.Rows[countPercentage]["Percentage"].ToString(), out percentageValue);
                        percentageTotal = percentageTotal + percentageValue;
                    }

                    if (recordDataTable.Rows[countPercentage]["SelectedSystem"].ToString() == "Roofing")
                    {
                        int.TryParse(recordDataTable.Rows[countPercentage]["Percentage"].ToString(), out roofingValue);
                        roofingTotal = roofingTotal + roofingValue;
                    }
                }


                if (!isFormMasterCall)
                {
                    if (percentageTotal != 100)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("ExteriorWallsRoofingMissing"), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }

                    if (roofingTotal != 100)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("ExteriorWallsRoofingMissing"), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                else
                {
                    if (percentageTotal != 100)
                    {
                        return false;
                    }

                    if (roofingTotal != 100)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion

        #region Depreciation Tab

        /// <summary>
        /// Gets the depreciation XML.
        /// </summary>
        /// <returns>string</returns>
        private string GetDepreciationXml()
        {
            DataTable tempDeprXml = new DataTable();
            DataColumn[] tempColumn = new DataColumn[] { new DataColumn(this.xmlCollectionDataSet.ListDeprValueDataTable.VSDeprecitionIDColumn.ColumnName), new DataColumn(this.xmlCollectionDataSet.ListDeprValueDataTable.IsByObjectColumn.ColumnName), new DataColumn(this.xmlCollectionDataSet.ListDeprValueDataTable.DeprYearColumn.ColumnName), new DataColumn(this.xmlCollectionDataSet.ListDeprValueDataTable.AgeColumn.ColumnName), new DataColumn(this.xmlCollectionDataSet.ListDeprValueDataTable.ConditionColumn.ColumnName), new DataColumn(this.xmlCollectionDataSet.ListDeprValueDataTable.DeprTableIDColumn.ColumnName), new DataColumn(this.xmlCollectionDataSet.ListDeprValueDataTable.DeprNameColumn.ColumnName), new DataColumn(this.xmlCollectionDataSet.ListDeprValueDataTable.QualityColumn.ColumnName), new DataColumn(this.xmlCollectionDataSet.ListDeprValueDataTable.ObjectConditionColumn.ColumnName), new DataColumn(this.xmlCollectionDataSet.ListDeprValueDataTable.DepreciationTableColumn.ColumnName), new DataColumn(this.xmlCollectionDataSet.ListDeprValueDataTable.EffectiveAgeColumn.ColumnName), new DataColumn(this.xmlCollectionDataSet.ListDeprValueDataTable.OriginalValueColumn.ColumnName), new DataColumn(this.xmlCollectionDataSet.ListDeprValueDataTable.DepreciationPercentColumn.ColumnName), new DataColumn(this.xmlCollectionDataSet.ListDeprValueDataTable.DepreciationAmountColumn.ColumnName), new DataColumn(this.xmlCollectionDataSet.ListDeprValueDataTable.AmountColumn.ColumnName) };
            tempDeprXml.Columns.AddRange(tempColumn);
            DataRow tempRow = tempDeprXml.NewRow();
            tempRow[this.xmlCollectionDataSet.ListDeprValueDataTable.VSDeprecitionIDColumn.ColumnName] = string.Empty;
            tempRow[this.xmlCollectionDataSet.ListDeprValueDataTable.IsByObjectColumn.ColumnName] = this.ByObjectCheckBox.Checked.ToString();
            tempRow[this.xmlCollectionDataSet.ListDeprValueDataTable.DeprYearColumn.ColumnName] = this.DeprYearTextBox.Text.Trim();
            tempRow[this.xmlCollectionDataSet.ListDeprValueDataTable.AgeColumn.ColumnName] = this.AgeTextBox.Text.Trim();
            tempRow[this.xmlCollectionDataSet.ListDeprValueDataTable.ConditionColumn.ColumnName] = this.ConditionTextBox.Text.Trim();
            tempRow[this.xmlCollectionDataSet.ListDeprValueDataTable.DeprTableIDColumn.ColumnName] = this.DeprTableComboBox.SelectedValue.ToString();
            tempRow[this.xmlCollectionDataSet.ListDeprValueDataTable.DeprNameColumn.ColumnName] = this.DeprTableComboBox.SelectedText.ToString();
            tempRow[this.xmlCollectionDataSet.ListDeprValueDataTable.QualityColumn.ColumnName] = this.PropertyQualityTextBox.Text.Trim();
            tempRow[this.xmlCollectionDataSet.ListDeprValueDataTable.ObjectConditionColumn.ColumnName] = this.ObjectConditionTextBox.Text.Trim();
            tempRow[this.xmlCollectionDataSet.ListDeprValueDataTable.DepreciationTableColumn.ColumnName] = this.DepreciationTableTextBox.Text.Trim();
            tempRow[this.xmlCollectionDataSet.ListDeprValueDataTable.EffectiveAgeColumn.ColumnName] = this.EffectiveAgeTextBox.Text.Trim();
            tempRow[this.xmlCollectionDataSet.ListDeprValueDataTable.OriginalValueColumn.ColumnName] = this.DepreciationCalculateRcnTextBox.Text.Trim();
            tempRow[this.xmlCollectionDataSet.ListDeprValueDataTable.DepreciationPercentColumn.ColumnName] = this.DepreciationPercentageTextBox.Text.Trim().Replace("%", "");
            tempRow[this.xmlCollectionDataSet.ListDeprValueDataTable.DepreciationAmountColumn.ColumnName] = this.DepreciationAmountTextBox.Text.Trim();
            tempRow[this.xmlCollectionDataSet.ListDeprValueDataTable.AmountColumn.ColumnName] = this.RcnLessDeprTextBox.Text.Trim();
            tempDeprXml.Rows.Add(tempRow);

            return TerraScanCommon.GetXmlString(tempDeprXml);
        }

        /// <summary>
        /// Clears the depreciation tab.
        /// </summary>
        private void ClearDepreciationTab()
        {
            this.DeprYearTextBox.Text = string.Empty;
            this.AgeTextBox.Text = string.Empty;
            this.ConditionTextBox.EmptyDecimalValue = true;
            this.ConditionTextBox.Text = string.Empty;
            this.PropertyQualityTextBox.Text = string.Empty;
            this.DepreciationCalculateRcnTextBox.Text = string.Empty;
            this.DepreciationPercentageTextBox.Text = "0.00%";
            this.DepreciationAmountTextBox.Text = string.Empty;
            this.RcnLessDeprTextBox.Text = string.Empty;
            this.DeprTableIDTextBox.Text = string.Empty;

            if (this.DeprTableComboBox.SelectedIndex > 0)
            {
                this.DeprTableComboBox.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Resets the depr value.
        /// </summary>
        private void ResetDeprValue()
        {
            this.age = -1;
            this.condition = 0;
            this.deprTableId = 0;
        }

        /// <summary>
        /// Calculates the depr process.
        /// </summary>
        private void CalculateDeprProcess()
        {
            if (this.ByObjectCheckBox.Checked)
            {
                this.ByObjectCheckedStatus(true);
                this.ResetDeprValue();
                this.CalculateCheckedDeprPercentage();
                this.CalculateRcnDepreciationValues();
            }
            else
            {
                this.ByObjectCheckedStatus(false);
                this.ResetDeprValue();
                this.CalculateUnCheckedDeprPercentage();
                this.CalculateRcnDepreciationValues();
            }
        }

        /// <summary>
        /// Calculates the RCN depreciation values.
        /// </summary>
        private void CalculateRcnDepreciationValues()
        {
            double rcnValue = 0.0;
            double rcnLess = 0.0;
            double depreciationAmount = 0.0;
            double.TryParse(this.calculatedRcn, out rcnValue);

            if (rcnValue > 0.0)
            {
                depreciationAmount = (rcnValue * (this.deprPercentage / 100)) * (-1);
                this.DepreciationAmountTextBox.Text = depreciationAmount.ToString();
                this.CalculateDepreciationTextBox.Text = depreciationAmount.ToString();

                rcnLess = rcnValue + depreciationAmount;
                this.RcnLessDeprTextBox.Text = rcnLess.ToString();
                this.DepreciationCalculateRcnTextBox.Text = this.calculatedRcn;

                if (rcnLess.ToString() != "0")
                {
                    this.RcnldTextBox.EmptyDecimalValue = false;
                    this.RcnldTextBox.Text = rcnLess.ToString();
                }
                else
                {
                    this.RcnldTextBox.EmptyDecimalValue = true;
                    this.RcnldTextBox.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Loads the depreciation tab.
        /// </summary>
        private void LoadDepreciationTab()
        {
            if (this.xmlCollectionDataSet.ListDeprTable.Rows.Count > 0)
            {
                this.DeprTableComboBox.DataSource = this.xmlCollectionDataSet.ListDeprTable;
                this.DeprTableComboBox.DisplayMember = this.xmlCollectionDataSet.ListDeprTable.DeprNameColumn.ColumnName;
                this.DeprTableComboBox.ValueMember = this.xmlCollectionDataSet.ListDeprTable.DeprTableIDColumn.ColumnName;
            }

            if (this.xmlCollectionDataSet.ListDeprValueDataTable.Rows.Count > 0)
            {
                int.TryParse(this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.Rows[0][this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.RollYearColumn.ColumnName].ToString(), out this.rollYear);

                if (!string.IsNullOrEmpty(this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.IsByObjectColumn.ColumnName].ToString()))
                {
                    this.ByObjectCheckBox.Checked = Convert.ToBoolean(this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.IsByObjectColumn.ColumnName].ToString());
                }

                this.DeprYearTextBox.Text = this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.DeprYearColumn.ColumnName].ToString();
                this.AgeTextBox.Text = this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.AgeColumn.ColumnName].ToString();
                this.ConditionTextBox.Text = this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.ConditionColumn.ColumnName].ToString();
                this.PropertyQualityTextBox.Text = this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.QualityColumn.ColumnName].ToString();
                this.DepreciationTableTextBox.Text = this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.DepreciationTableColumn.ColumnName].ToString();
                this.EffectiveAgeTextBox.Text = this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.EffectiveAgeColumn.ColumnName].ToString();

                if (!string.IsNullOrEmpty(this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.ObjectConditionColumn.ColumnName].ToString()))
                {
                    this.ObjectConditionTextBox.Text = this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.ObjectConditionColumn.ColumnName].ToString();
                }
                else
                {
                    this.ObjectConditionTextBox.EmptyDecimalValue = true;
                    this.ObjectConditionTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.OriginalValueColumn.ColumnName].ToString()))
                {
                    this.DepreciationCalculateRcnTextBox.Text = this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.OriginalValueColumn.ColumnName].ToString();
                }
                else
                {
                    this.DepreciationCalculateRcnTextBox.EmptyDecimalValue = true;
                    this.DepreciationCalculateRcnTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.DepreciationPercentColumn.ColumnName].ToString()))
                {
                    this.DepreciationPercentageTextBox.Text = this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.DepreciationPercentColumn.ColumnName].ToString() + "%";
                }
                else
                {
                    this.DepreciationPercentageTextBox.Text = "0.00%";
                }

                if (!string.IsNullOrEmpty(this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.DepreciationAmountColumn.ColumnName].ToString()))
                {
                    this.DepreciationAmountTextBox.Text = this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.DepreciationAmountColumn.ColumnName].ToString();
                }
                else
                {
                    this.DepreciationAmountTextBox.EmptyDecimalValue = true;
                    this.DepreciationAmountTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.AmountColumn.ColumnName].ToString()))
                {
                    this.RcnLessDeprTextBox.Text = this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.AmountColumn.ColumnName].ToString();
                }
                else
                {
                    this.RcnLessDeprTextBox.EmptyDecimalValue = true;
                    this.RcnLessDeprTextBox.Text = string.Empty;
                }

                this.DeprTableIDTextBox.Text = this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.DepreciationTableIDColumn.ColumnName].ToString();

                this.rcnCalculated = true;
                int selectedItem = 0;
                int.TryParse(this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.DeprTableIDColumn.ColumnName].ToString(), out selectedItem);
                this.DeprTableComboBox.SelectedValue = selectedItem;
                this.CalculateDepreciationPercentageLabel.Text = this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.DepreciationPercentColumn.ColumnName].ToString() + "%";

                if (!string.IsNullOrEmpty(this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.DepreciationAmountColumn.ColumnName].ToString()))
                {
                    this.CalculateDepreciationTextBox.Text = this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.DepreciationAmountColumn.ColumnName].ToString();
                }
                else
                {
                    this.CalculateDepreciationTextBox.EmptyDecimalValue = true;
                    this.CalculateDepreciationTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.AmountColumn.ColumnName].ToString()))
                {
                    this.RcnldTextBox.Text = this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.AmountColumn.ColumnName].ToString();
                }
                else
                {
                    this.RcnldTextBox.EmptyDecimalValue = true;
                    this.RcnldTextBox.Text = string.Empty;
                }

                this.rcnCalculated = true;
                if (!string.IsNullOrEmpty(this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.OriginalValueColumn.ColumnName].ToString()))
                {
                    this.CalculateRcnTextBox.Text = this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.OriginalValueColumn.ColumnName].ToString();
                }
                else
                {
                    this.CalculateRcnTextBox.EmptyDecimalValue = true;
                    this.CalculateRcnTextBox.Text = string.Empty;
                }
            }
            else
            {
                int.TryParse(this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.Rows[0][this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.RollYearColumn.ColumnName].ToString(), out this.rollYear);
                this.EffectiveAgeTextBox.Text = this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.Rows[0][this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.EffectiveAgeColumn.ColumnName].ToString();

                if (!string.IsNullOrEmpty(this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.Rows[0][this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.ObjectConditionColumn.ColumnName].ToString()))
                {
                    this.ObjectConditionTextBox.Text = this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.Rows[0][this.xmlCollectionDataSet.ListHouseTypeCollectionDatatable.ObjectConditionColumn.ColumnName].ToString();
                }
                else
                {
                    this.ObjectConditionTextBox.EmptyDecimalValue = true;
                    this.ObjectConditionTextBox.Text = string.Empty;
                }

                this.ByObjectCheckBox.Checked = false;
            }

            if (this.ByObjectCheckBox.Checked)
            {
                this.ByObjectCheckedStatus(true);
            }
            else
            {
                this.ByObjectCheckedStatus(false);
            }
        }

        /// <summary>
        /// Bies the object checked status.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        private void ByObjectCheckedStatus(bool value)
        {
            this.DeprYearTextBox.Enabled = value;
            this.ConditionTextBox.Enabled = value;
            this.DeprTableComboBox.Enabled = value;
            this.AgeTextBox.Enabled = value;
            this.PropertyQualityTextBox.Enabled = !value;

            if (!value)
            {
                this.DeprYearTextBox.Text = string.Empty;
                this.ConditionTextBox.EmptyDecimalValue = true;
                this.ConditionTextBox.Text = string.Empty;
                this.AgeTextBox.Text = string.Empty;

                if (this.DeprTableComboBox.SelectedIndex > -1)
                {
                    this.DeprTableComboBox.SelectedIndex = 0;
                }
            }
            else
            {
                this.PropertyQualityTextBox.Text = string.Empty;
                this.DepreciationTableTextBox.Text = string.Empty;
                this.DeprTableIDTextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// Calculates the un checked depr percentage.
        /// </summary>
        private void CalculateUnCheckedDeprPercentage()
        {
            int.TryParse(this.EffectiveAgeTextBox.Text, out this.age);
            decimal.TryParse(this.ObjectConditionTextBox.Text, out this.condition);
            int.TryParse(this.DeprTableIDTextBox.Text, out this.deprTableId);

            if (this.age >= 0 && this.condition > 0 && this.deprTableId > 0)
            {
                double.TryParse(this.form36000Control.WorkItem.GetDeprPercentage(this.age, this.condition, this.deprTableId).ToString(), out this.deprPercentage);
                this.DepreciationPercentageTextBox.Text = this.deprPercentage.ToString() + "%";
                this.CalculateDepreciationPercentageLabel.Text = this.deprPercentage.ToString() + "%";
            }
            else
            {
                this.DepreciationPercentageTextBox.Text = "0.00%";
                this.CalculateDepreciationPercentageLabel.Text = "0.00%";
            }
        }

        /// <summary>
        /// Calculates the checked depr percentage.
        /// </summary>
        private void CalculateCheckedDeprPercentage()
        {
            int.TryParse(this.AgeTextBox.Text, out this.age);
            decimal.TryParse(this.ConditionTextBox.Text, out this.condition);
            int.TryParse(this.DeprTableComboBox.SelectedValue.ToString(), out this.deprTableId);

            if (this.age >= 0 && this.condition > 0 && this.deprTableId > 0)
            {
                double.TryParse(this.form36000Control.WorkItem.GetDeprPercentage(this.age, this.condition, this.deprTableId).ToString(), out this.deprPercentage);
                this.DepreciationPercentageTextBox.Text = this.deprPercentage.ToString() + "%";
                this.CalculateDepreciationPercentageLabel.Text = this.deprPercentage.ToString() + "%";
            }
            else
            {
                this.DepreciationPercentageTextBox.Text = "0.00%";
                this.CalculateDepreciationPercentageLabel.Text = "0.00%";
            }
        }

        #region Esc key Functionality
        /// <summary>
        /// Processes the CMD key.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="keyData">The key data.</param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;
            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                if (keyData.Equals(Keys.Escape))
                {
                    this.escKeyPressed = true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion region Esc key Functionality

        #endregion

        #endregion
    }
}
