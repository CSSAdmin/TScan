//--------------------------------------------------------------------------------------------
// <copyright file="F36001.cs" company="Congruent">
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
// 08 Jun 07		KARTHIKEYAN V	    Created
//*********************************************************************************/

namespace D36001
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
    using Infragistics.Win;
    using Infrastructure.Interface;

    /// <summary>
    /// F36001
    /// </summary>
    [SmartPart]
    public partial class F36001 : BaseSmartPart
    {
        #region Variable

        /// <summary>
        /// Marshall and Swift ServiceImplimentationClient 
        /// </summary>
        private MSWCFService.ServiceImplimentationClient marshallService = new MSWCFService.ServiceImplimentationClient();

        /// <summary>
        /// An object for Dataset - F36001Controller
        /// </summary>
        private F36001Controller form36001Control = new F36001Controller();

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// Used to store ValueSliceId(keyid)
        /// </summary>
        private int valueSliceId;

        /// <summary>
        /// Used to store estimateIdValue(keyid)
        /// </summary>
        private int estimateIdValue;

        /// <summary>
        /// Used to store componentUniqueId(keyid)
        /// </summary>
        private int componentUniqueId;

        /// <summary>
        /// Used to store rcnCalculated
        /// </summary>
        private bool rcnCalculated;

        /// <summary>
        /// eventFired
        /// </summary>
        private bool eventFired;

        /// <summary>
        /// Used to store formLoad
        /// </summary>
        private bool formLoad;

        /// <summary>
        /// costConnectionString Local variable.
        /// </summary>
        private string costConnectionString;

        /// <summary>
        /// Used to store FormMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// zipCode Local variable.
        /// </summary>
        private string zipCode;

        /// <summary>
        /// occupancyDescription Local variable.
        /// </summary>
        private string occupancyDescription;

        /// <summary>
        /// validationErrorString Local variable.
        /// </summary>
        private string validationErrorString;

        /// <summary>
        /// totalRcnValue Local variable.
        /// </summary>
        private string totalRcnValue;

        /// <summary>
        /// Used to store editComponent
        /// </summary>
        private bool editComponent;

        /// <summary>
        /// Used to store ExecutionRequied
        /// </summary>
        private bool executionRequied;

        /// <summary>
        /// Used to store occupancyExecutionRequied
        /// </summary>
        private bool occupancyExecutionRequied;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Used to store BaseQualityValid
        /// </summary>
        private bool componentTabSelected;

        /// <summary>
        /// Used to store occupancyCodeModified
        /// </summary>
        private bool occupancyCodeModified;

        /// <summary>
        /// An object for ContextMenuStrip
        /// </summary>
        private ContextMenuStrip selectedOccupancyMenuStrip = new ContextMenuStrip();

        /// <summary>
        /// An object for selectedComponentMenuStrip
        /// </summary>
        private ContextMenuStrip selectedComponentMenuStrip = new ContextMenuStrip();

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// commercialHtcXmlDataSet
        /// </summary>
        private DataSet commercialHtcXmlDataSet = new DataSet();

        /// <summary>
        /// defaultDataTable
        /// </summary>
        private DataTable defaultDataTable = new DataTable();

        /// <summary>
        /// occupancyCodeDataTable
        /// </summary>
        private DataTable occupancyCodeDataTable = new DataTable();

        /// <summary>
        /// occupancyCodeDataTable
        /// </summary>
        private DataTable selectedOccupancyCodeDataTable = new DataTable();

        /// <summary>
        /// selectedComponentCodeDataTable
        /// </summary>
        private DataTable selectedComponentCodeDataTable = new DataTable();

        /// <summary>
        /// occupancyClassDataTable
        /// </summary>
        private DataTable occupancyClassDataTable = new DataTable();

        /// <summary>
        /// occupancyTypeDataTable
        /// </summary>
        private DataTable occupancyTypeDataTable = new DataTable();

        /// <summary>
        /// componentCodeDataTable
        /// </summary>
        private DataTable componentCodeDataTable = new DataTable();

        /// <summary>
        /// componetSystemDataTable
        /// </summary>
        private DataTable componetSystemDataTable = new DataTable();

        /// <summary>
        /// editComponentHashTable
        /// </summary>
        private Hashtable editComponentHashTable = new Hashtable();

        /// <summary>
        /// xmlCollectionDataSet
        /// </summary>
        private F36001MarshalAndSwiftCommercialData xmlCollectionDataSet = new F36001MarshalAndSwiftCommercialData();

        /// <summary>
        /// sectionTypeValue
        /// </summary>
        private int sectionTypeValue;

        /// <summary>
        /// occupancyId
        /// </summary>
        private int occupancyId;

        /// <summary>
        /// typedOccupancyCode
        /// </summary>
        private int typedOccupancyCode;

        /// <summary>
        /// typedComponentCode
        /// </summary>
        private int typedComponentCode;

        /// <summary>
        /// classValueList
        /// </summary>
        private string classValueList;

        /// <summary>
        /// typeValueList
        /// </summary>
        private string typeValueList;

        /// <summary>
        /// commercialXml
        /// </summary>
        private string commercialXml;

        /// <summary>
        /// classValueList
        /// </summary>
        private bool rowSelected;

        /// <summary>
        /// typedOccupancyRow
        /// </summary>
        private bool typedOccupancyRow;

        /// <summary>
        /// tabProcess
        /// </summary>
        private bool tabProcess;

        /// <summary>
        /// Used to store LowQuality
        /// </summary>
        private double lowQuality;

        /// <summary>
        /// Used to store HighQuality
        /// </summary>
        private double highQuality;

        /// <summary>
        /// minMultiplier
        /// </summary>
        private double minMultiplier;

        /// <summary>
        /// maxMultiplier
        /// </summary>
        private double maxMultiplier;

        /// <summary>
        /// minRounding
        /// </summary>
        private int minRounding;

        /// <summary>
        /// maxRounding
        /// </summary>
        private int maxRounding;

        /// <summary>
        /// minStorySection
        /// </summary>
        private double minStory;

        /// <summary>
        /// maxStorySection
        /// </summary>
        private double maxStory;

        /// <summary>
        /// minArea
        /// </summary>
        private int minArea;

        /// <summary>
        /// maxArea
        /// </summary>
        private int maxArea;

        /// <summary>
        /// minPerimeter
        /// </summary>
        private int minPerimeter;

        /// <summary>
        /// maxPerimeter
        /// </summary>
        private int maxPerimeter;

        /// <summary>
        /// minShape
        /// </summary>
        private double minShape;

        /// <summary>
        /// maxShape
        /// </summary>
        private double maxShape;

        /// <summary>
        /// minBasementLevel
        /// </summary>
        private double minBasementLevel;

        /// <summary>
        /// maxBasementLevel
        /// </summary>
        private double maxBasementLevel;

        /// <summary>
        /// defaultRankValue
        /// </summary>
        private double defaultRankValue;

        /// <summary>
        /// defaultlocalMultiplier
        /// </summary>
        private double defaultLocalMultiplier;

        /// <summary>
        /// defaultarchitectFee
        /// </summary>
        private double defaultArchitectFee;

        /// <summary>
        /// defaultShape
        /// </summary>
        private double defaultShape;

        /// <summary>
        /// defaultbuildingStories
        /// </summary>
        private double defaultBuildingStories;

        /// <summary>
        /// defaultRounding
        /// </summary>
        private int defaultRounding;

        /// <summary>
        /// Used to store the msversionId
        /// </summary>
        private string msversionId = string.Empty;

        /// <summary>
        /// used to store the costMultiplier
        /// </summary>
        private string costMultiplier = string.Empty;

        /// <summary>
        /// rollYear
        /// </summary>
        private int rollYear;

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

        #endregion

        #region Constructor

        /// <summary>
        /// F36001
        /// </summary>
        public F36001()
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
        public F36001(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
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

        /// <summary>
        /// Event publication for cancel triggerring in slice
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_CancelSliceInformation, PublicationScope.Descendants)]
        public event EventHandler<DataEventArgs<int>> D9030_F9030_CancelSliceInformation;

        #endregion Event Publication

        #region Enum

        /// <summary>
        /// SectionType
        /// </summary>
        private enum SectionType
        {
            /// <summary>
            /// Basement
            /// </summary>
            Basement = 1,

            /// <summary>
            /// Section1
            /// </summary>
            Section1 = 2
        }

        #endregion Enum

        #region Property

        /// <summary>
        /// Gets or sets the mortgage import template controll.
        /// </summary>
        /// <value>The mortgage import template controll.</value>
        [CreateNew]
        public F36001Controller Form36001Controll
        {
            get { return this.form36001Control as F36001Controller; }
            set { this.form36001Control = value; }
        }

        #endregion

        #region Event Subscription

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
                this.formLoad = true;
                this.rcnCalculated = true;
                
                this.ClearMarshalSwiftCommercial();

                // Load Xml
                this.xmlCollectionDataSet = this.form36001Control.WorkItem.F36001_GetMarshalAndSwiftCommercial(this.valueSliceId);
                this.commercialXml = this.xmlCollectionDataSet.GetMarshallSwiftCommercial.Rows[0][this.xmlCollectionDataSet.GetMarshallSwiftCommercial.HTCXMLColumn.ColumnName].ToString();

                // Loading HTCXml dataset
                StringReader stringReaderHtc = new StringReader(this.commercialXml);
                XmlTextReader textReaderHtc = new XmlTextReader(stringReaderHtc);
                this.commercialHtcXmlDataSet.ReadXml(textReaderHtc);
                ////Added by Latha
                if (this.xmlCollectionDataSet.GetEstimate.Rows.Count == 0)
                {
                    F36001MarshalAndSwiftCommercialData.GetEstimateRow estimateRow = (F36001MarshalAndSwiftCommercialData.GetEstimateRow)this.xmlCollectionDataSet.GetEstimate.NewRow();
                    estimateRow["SectionID"] = "2";
                    estimateRow["StorySection"] = "1";
                    estimateRow["StoryShape"] = "2.00";
                    estimateRow["BasementShape"] = "2.00";
                    estimateRow["BasementLevel"] = "1.00";
                    estimateRow["ValueSliceID"] = this.valueSliceId.ToString();

                    if (this.xmlCollectionDataSet != null)
                    {
                        if (this.xmlCollectionDataSet.Tables.Count > 0)
                        {
                            this.costConnectionString = this.xmlCollectionDataSet.Tables[0].Rows[0]["ConnectionString"].ToString();

                            if (!string.IsNullOrEmpty(this.xmlCollectionDataSet.GetMarshallSwiftCommercial.Rows[0][this.xmlCollectionDataSet.GetMarshallSwiftCommercial.CostMultiplierColumn.ColumnName].ToString()))
                            {
                                if (this.xmlCollectionDataSet.GetEstimate.Rows.Count > 0 && !string.IsNullOrEmpty(this.xmlCollectionDataSet.GetEstimate.Rows[0]["LocalMultiplier"].ToString()))
                                {
                                    estimateRow["LocalMultiplier"] = this.xmlCollectionDataSet.GetEstimate.Rows[0]["CostMultiplier"].ToString();
                                }
                                else
                                {
                                    estimateRow["LocalMultiplier"] = this.xmlCollectionDataSet.GetMarshallSwiftCommercial.Rows[0][this.xmlCollectionDataSet.GetMarshallSwiftCommercial.CostMultiplierColumn.ColumnName].ToString();
                                }
                            }
                        }
                    }
                    this.xmlCollectionDataSet.GetEstimate.AddGetEstimateRow(estimateRow);
                    this.xmlCollectionDataSet.GetEstimate.AcceptChanges();
                }
                else
                {
                    if (this.xmlCollectionDataSet != null)
                    {
                        if (this.xmlCollectionDataSet.Tables.Count > 0)
                        {
                            this.costConnectionString = this.xmlCollectionDataSet.Tables[0].Rows[0]["ConnectionString"].ToString();

                            if (!string.IsNullOrEmpty(this.xmlCollectionDataSet.GetMarshallSwiftCommercial.Rows[0][this.xmlCollectionDataSet.GetMarshallSwiftCommercial.CostMultiplierColumn.ColumnName].ToString()))
                            {
                                if (this.xmlCollectionDataSet.GetEstimate.Rows.Count > 0 && !string.IsNullOrEmpty(this.xmlCollectionDataSet.GetEstimate.Rows[0]["LocalMultiplier"].ToString()))
                                {
                                    this.LocalMultiplierTextBox.Text = this.xmlCollectionDataSet.GetEstimate.Rows[0]["LocalMultiplier"].ToString();
                                }
                                else
                                {
                                    this.LocalMultiplierTextBox.Text = this.xmlCollectionDataSet.GetMarshallSwiftCommercial.Rows[0][this.xmlCollectionDataSet.GetMarshallSwiftCommercial.CostMultiplierColumn.ColumnName].ToString();
                                }
                            }
                        }
                    }
                }
                
                this.SetSectionComboValue();
                ////Ends here
                this.GetMinMaxValue();
                this.LoadMarshalSwiftCommercial();
                this.ClearDepreciationTab();
                this.LoadDepreciationTab();                
                this.SectionMainPanel.Visible = true;
                this.BasementMailPanel.Visible = false;
                this.OccupancyNameGridClick(0);
                if (this.xmlCollectionDataSet.GetEstimate.Rows.Count > 0)
                {
                    if (this.xmlCollectionDataSet.GetEstimate.Rows.Count > 0 && !string.IsNullOrEmpty(this.xmlCollectionDataSet.GetEstimate.Rows[0]["LocalMultiplier"].ToString()))
                    {
                        this.LocalMultiplierTextBox.Text = this.xmlCollectionDataSet.GetEstimate.Rows[0]["LocalMultiplier"].ToString();
                    }
                    else
                    {
                        this.LocalMultiplierTextBox.Text = this.xmlCollectionDataSet.GetMarshallSwiftCommercial.Rows[0][this.xmlCollectionDataSet.GetMarshallSwiftCommercial.CostMultiplierColumn.ColumnName].ToString();
                    }
                }

                this.CalculateDepreciationPercentageLabel.Text = DepreciationPercentageTextBox.Text;
                this.formLoad = false;
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
                    if (!this.componentTabSelected)
                    {
                        if (!this.eventFired)
                        {
                            if (this.MarshallSwiftCommercialTab.SelectedIndex == 0)
                            {
                                this.SectionTypeLabel.Focus();
                            }
                            else
                            {
                                this.SectionTypeComponentLabel.Focus();
                            }
                        }
                        else
                        {
                            this.SelectedOccupancyGrid.UpdateData();
                            this.selectedOccupancyCodeDataTable.AcceptChanges();
                            this.selectedComponentCodeDataTable.AcceptChanges();
                        }
                    }

                    SliceValidationFields sliceValidationFields = new SliceValidationFields();
                    sliceValidationFields.FormNo = eventArgs.Data;
                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
                }

                this.eventFired = false;
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
            if (this.slicePermissionField.editPermission)
            {
                this.Cursor = Cursors.WaitCursor;
                string occupancyXml = string.Empty;
                string componentXml = string.Empty;
                string buildingDataXml = string.Empty;
                occupancyXml = TerraScanCommon.GetXmlString(this.selectedOccupancyCodeDataTable);
                componentXml = TerraScanCommon.GetXmlString(this.selectedComponentCodeDataTable);
                buildingDataXml = this.CreateBuildingDataXml();

                // Save Commercial values
                try
                {
                    this.estimateIdValue = this.form36001Control.WorkItem.F36001_SaveMarshalAndSwiftCommercial(this.valueSliceId, buildingDataXml, occupancyXml, componentXml, this.GetDepreciationXml(), TerraScanCommon.UserId);
                }
                catch (Exception ex)
                {
                }

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
                this.slicePermissionField.deletePermission = eventArgs.Data.DeletePermission;
                this.slicePermissionField.editPermission = eventArgs.Data.EditPermission;
                this.slicePermissionField.newPermission = eventArgs.Data.NewPermission;
                this.slicePermissionField.openPermission = eventArgs.Data.OpenPermission;

                this.OccupancyTab.Controls.Owner.Enabled = eventArgs.Data.EditPermission;
                this.DepreciationTab.Controls.Owner.Enabled = eventArgs.Data.EditPermission;
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
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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
                if (this.MarshallSwiftCommercialTab.SelectedIndex == 0)
                {
                    this.SectionTypeLabel.Focus();
                }
                else if (this.MarshallSwiftCommercialTab.SelectedIndex == 2)
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

        #endregion Event Subscription

        #region Protected methods

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

        #region CommonEvents

        /// <summary>
        /// Handles the Load event of the F36001 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F36001_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.formLoad = true;
                this.CustomizeAllOccupancyGrid();
                this.CustomizeAllComponentGrid();

                // Load Xml
                this.xmlCollectionDataSet = this.form36001Control.WorkItem.F36001_GetMarshalAndSwiftCommercial(this.valueSliceId);
                this.commercialXml = this.xmlCollectionDataSet.GetMarshallSwiftCommercial.Rows[0][this.xmlCollectionDataSet.GetMarshallSwiftCommercial.HTCXMLColumn.ColumnName].ToString();

                if (this.xmlCollectionDataSet.GetEstimate.Rows.Count == 0)
                {
                    F36001MarshalAndSwiftCommercialData.GetEstimateRow estimateRow = (F36001MarshalAndSwiftCommercialData.GetEstimateRow)this.xmlCollectionDataSet.GetEstimate.NewRow();
                    estimateRow["SectionID"] = "2";
                    estimateRow["StorySection"] = "1";
                    estimateRow["StoryShape"] = "2.00";
                    estimateRow["BasementShape"] = "2.00";
                    estimateRow["BasementLevel"] = "1.00";
                    estimateRow["ValueSliceID"] = this.valueSliceId.ToString();
                    this.xmlCollectionDataSet.GetEstimate.AddGetEstimateRow(estimateRow);
                }
                this.xmlCollectionDataSet.GetEstimate.AcceptChanges();
                // Loading HTCXml dataset
                StringReader stringReaderHtc = new StringReader(this.commercialXml);
                XmlTextReader textReaderHtc = new XmlTextReader(stringReaderHtc);
                this.commercialHtcXmlDataSet.ReadXml(textReaderHtc);

                // LM
                this.DefaultMultiplierTextBox.Text = this.defaultLocalMultiplier.ToString();

                // Connection String
                if (this.xmlCollectionDataSet != null)
                {
                    if (this.xmlCollectionDataSet.Tables.Count > 0)
                    {
                        this.costConnectionString = this.xmlCollectionDataSet.Tables[0].Rows[0]["ConnectionString"].ToString();

                        if (!string.IsNullOrEmpty(this.xmlCollectionDataSet.GetMarshallSwiftCommercial.Rows[0][this.xmlCollectionDataSet.GetMarshallSwiftCommercial.CostMultiplierColumn.ColumnName].ToString()))
                        {
                            if (this.xmlCollectionDataSet.GetEstimate.Rows.Count > 0 && !string.IsNullOrEmpty(this.xmlCollectionDataSet.GetEstimate.Rows[0]["LocalMultiplier"].ToString()))
                            {
                                this.LocalMultiplierTextBox.Text = this.xmlCollectionDataSet.GetEstimate.Rows[0]["LocalMultiplier"].ToString();
                            }
                            else
                            {
                                this.LocalMultiplierTextBox.Text = this.xmlCollectionDataSet.GetMarshallSwiftCommercial.Rows[0][this.xmlCollectionDataSet.GetMarshallSwiftCommercial.CostMultiplierColumn.ColumnName].ToString();
                            }
                        }

                        ////Added by Latha
                        this.SetSectionComboValue();
                    }
                }

                this.GetMinMaxValue();
                this.LoadMarshalSwiftCommercial();
                this.LoadDepreciationTab();
                this.formLoad = false;
                this.rcnCalculated = false;

                // Assign the Occupancy menus
                this.selectedOccupancyMenuStrip.Items.Add(SharedFunctions.GetResourceString("MenuDelete"));
                this.selectedOccupancyMenuStrip.Items.Add(SharedFunctions.GetResourceString("MenuCancel"));
                this.selectedOccupancyMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(this.SelectedOccupancyMenuStrip_ItemClicked);
                this.selectedOccupancyMenuStrip.Closed += new ToolStripDropDownClosedEventHandler(this.SelectedOccupancyMenuStrip_Closed);

                // Assign the Component menus
                this.selectedComponentMenuStrip.Items.Add(SharedFunctions.GetResourceString("MenuDelete"));
                this.selectedComponentMenuStrip.Items.Add(SharedFunctions.GetResourceString("MenuEdit"));
                this.selectedComponentMenuStrip.Items.Add(SharedFunctions.GetResourceString("MenuCancel"));
                this.selectedComponentMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(this.SelectedComponentMenuStrip_ItemClicked);
                this.selectedComponentMenuStrip.Closed += new ToolStripDropDownClosedEventHandler(this.SelectedComponentMenuStrip_Closed);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Selecting event of the MarshallSwiftCommercialTab control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.TabControlCancelEventArgs"/> instance containing the event data.</param>
        private void MarshallSwiftCommercialTab_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                if (this.slicePermissionField.editPermission)
                {
                    if (this.commercialHtcXmlDataSet.Tables.Count > 0)
                    {
                        this.eventFired = false;

                        if (e.TabPageIndex == 1)
                        {
                            if (this.componentTabSelected)
                            {
                                this.tabProcess = true;
                                e.Cancel = true;
                                return;
                            }

                            this.LoadComponentTab();
                            this.FilterComponentRow(this.sectionTypeValue);
                        }
                        else if (e.TabPageIndex == 0)
                        {
                            if (this.componentTabSelected)
                            {
                                this.tabProcess = true;
                                e.Cancel = true;
                                return;
                            }

                            if (this.sectionTypeValue == (int)SectionType.Section1)
                            {
                                // Customize Section Grid
                                this.CustomizeOccupancySectionGrid();
                                this.FilterOccupancyRow(this.sectionTypeValue);
                                this.SectionMainPanel.Visible = true;
                                this.BasementMailPanel.Visible = false;
                            }
                            else if (this.sectionTypeValue == (int)SectionType.Basement)
                            {
                                // Customize Basement Grid
                                this.CustomizeOccupancyBasementGrid();
                                this.FilterOccupancyRow(this.sectionTypeValue);
                                this.SectionMainPanel.Visible = false;
                                this.BasementMailPanel.Visible = true;
                            }
                            else
                            {
                                // Customize Section Grid
                                this.CustomizeOccupancySectionGrid();
                                this.FilterOccupancyRow(this.sectionTypeValue);
                                this.SectionMainPanel.Visible = true;
                                this.BasementMailPanel.Visible = false;
                            }
                        }
                        else if (e.TabPageIndex == 2)
                        {
                            if (this.componentTabSelected)
                            {
                                e.Cancel = true;
                                this.tabProcess = true;
                                return;
                            }

                            this.LoadComponentTab();
                            this.FilterComponentRow(this.sectionTypeValue);
                        }
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
        /// Handles the Click event of the CalculateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CalculateButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.ToEnableEditButtonInMasterForm();
                int rowCount = 0;
                if (this.ValidateOccupancy() && this.ValidateBasementArea() && this.ValidateTotalArea() && this.ValidateSectionPerimeterShape() && this.ValidateBasementPerimeterShape() && this.ValidateLevelsValue() && this.ValidateStories() && this.ValidateOccupancyPercentage())
                {
                    bool validPercentage = true;
                    for (int i = 1; i < this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows.Count; i++)
                    {
                        if (this.CalculateTotalpercentage(this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows[i]["Key"].ToString()) != 100)
                        {
                            validPercentage = false;
                            rowCount = i;
                            break;
                        }
                    }
                    //if (this.CalculateTotalpercentage() == 100)
                    if (validPercentage)
                    {
                        this.rcnCalculated = true;
                        string occupancyXml = string.Empty;
                        string componentXml = string.Empty;
                        string buildingDataXml = string.Empty;
                        string defaultXml = string.Empty;
                        double rcnValue = 0.0;

                        //for (int i = 0; i < this.selectedOccupancyCodeDataTable.Rows.Count; i++)
                        //{
                        //    if (!string.IsNullOrEmpty(this.selectedOccupancyCodeDataTable.Rows[i]["BasementType"].ToString()))
                        //    {
                        //        this.selectedOccupancyCodeDataTable.Rows[i]["BasementType"] = string.Empty;
                        //    }
                        //}
                        this.selectedOccupancyCodeDataTable.AcceptChanges();
                        occupancyXml = TerraScanCommon.GetXmlString(this.selectedOccupancyCodeDataTable);
                        componentXml = TerraScanCommon.GetXmlString(this.selectedComponentCodeDataTable);
                        buildingDataXml = this.CreateBuildingDataXml();
                        this.defaultDataTable = this.commercialHtcXmlDataSet.Tables["Default"];
                        defaultXml = TerraScanCommon.GetXmlString(this.defaultDataTable);

                        Hashtable saveElements = new Hashtable();
                        saveElements.Add("OccupancyXml", occupancyXml);
                        saveElements.Add("ComponentXml", componentXml);
                        saveElements.Add("BuildingDataXml", buildingDataXml);
                        saveElements.Add("ZipCode", this.zipCode);
                        saveElements.Add("DefaultXml", defaultXml);
                        saveElements.Add("ConnectionString", this.costConnectionString);

                        this.totalRcnValue = this.marshallService.F36001_CalculateRcn(saveElements);

                        double.TryParse(this.totalRcnValue, out rcnValue);
                        if (rcnValue > 0.0)
                        {
                            this.CalculatePerSf();
                            this.rcnCalculated = true;
                            this.CalculateRcnTextBox.Text = this.totalRcnValue;
                            this.CalculateDeprProcess();
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(this.totalRcnValue))
                            {
                                MessageBox.Show(this.totalRcnValue, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Section " + rowCount.ToString() + SharedFunctions.GetResourceString("OccupancyPercentageErrorMsg"), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(this.validationErrorString, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.validationErrorString = string.Empty;
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

        /// <summary>
        /// Handles the DoubleClick event of the MarshallSwiftImagePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MarshallSwiftImagePictureBox_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Commerical Estimator Details:\nMS Version ID            :   " + this.msversionId + "\nCost data path          :   " + this.costConnectionString + "\nMultiplier Value          :   " + this.costMultiplier + "\nCost as of                 :    ", "TerraScan T2 - Marshal & Swift Commerical", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D36001.F36001"));
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

        #endregion CommonEvents

        #region Occupancy

        /// <summary>
        /// Handles the TextChanged event of the LocalMultiplierTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LocalMultiplierTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();
                if (this.xmlCollectionDataSet.GetEstimate.Rows.Count > 0)
                {
                    this.SetSectionToTable("LocalMultiplier", this.LocalMultiplierTextBox.Text, "2");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the LocalMultiplierTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LocalMultiplierTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                double multiplierValue = 0.0;
                double.TryParse(this.LocalMultiplierTextBox.Text.Trim(), out multiplierValue);

                if (multiplierValue >= this.minMultiplier && multiplierValue <= this.maxMultiplier)
                {
                    this.LocalMultiplierTextBox.Text = multiplierValue.ToString();
                    this.componentTabSelected = false;
                }
                else
                {
                    if (!this.tabProcess)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("LocalMultiplierErrorMsg") + this.minMultiplier.ToString() + SharedFunctions.GetResourceString("ErrorMsgThrough") + this.maxMultiplier.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.componentTabSelected = true;
                        this.LocalMultiplierTextBox.Focus();
                    }
                    else
                    {
                        this.LocalMultiplierTextBox.Focus();
                        this.tabProcess = false;
                    }
                }
                this.SetSectionToTable("LocalMultiplier", this.LocalMultiplierTextBox.Text, "2");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the ArchFeeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ArchFeeTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.ArchFeeTextBox.Text.Trim()))
                {
                    double minArchFee = 0.0;
                    double maxArchFee = 99.9;
                    double archFeeValue = 0.0;

                    double.TryParse(this.ArchFeeTextBox.Text.Trim(), out archFeeValue);

                    if (archFeeValue >= minArchFee && archFeeValue <= maxArchFee)
                    {
                        this.ArchFeeTextBox.Text = archFeeValue.ToString();
                        this.componentTabSelected = false;
                    }
                    else
                    {
                        if (!this.tabProcess)
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("ArchFeeErrorMsg") + minArchFee.ToString() + SharedFunctions.GetResourceString("ErrorMsgThrough") + maxArchFee.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.componentTabSelected = true;
                            this.ArchFeeTextBox.Focus();
                        }
                        else
                        {
                            this.ArchFeeTextBox.Focus();
                            this.tabProcess = false;
                        }
                    }
                }
                else
                {
                    this.componentTabSelected = false;
                }

                this.SetSectionToTable("ArchFeePercentage", this.ArchFeeTextBox.Text, "2");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /* /// <summary>
        /// Handles the Leave event of the RoundingTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RoundingTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                int roundingValue = 0;
                int.TryParse(this.RoundingTextBox.Text.Trim(), out roundingValue);

                if (roundingValue >= this.minRounding && roundingValue <= this.maxRounding)
                {
                    this.RoundingTextBox.Text = roundingValue.ToString();
                    this.componentTabSelected = false;
                }
                else
                {
                    if (!this.tabProcess)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("RoundingErrorMsg") + this.minRounding.ToString() + SharedFunctions.GetResourceString("ErrorMsgThrough") + this.maxRounding.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.componentTabSelected = true;
                        this.RoundingTextBox.Focus();
                    }
                    else
                    {
                        this.RoundingTextBox.Focus();
                        this.tabProcess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        } */

        /// <summary>
        /// Handles the Leave event of the StorySectionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StorySectionTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                double storySectionValue = 0;
                double.TryParse(this.StorySectionTextBox.Text.Trim(), out storySectionValue);

                if (storySectionValue >= this.minStory && storySectionValue <= this.maxStory)
                {
                    this.StorySectionTextBox.Text = storySectionValue.ToString();
                    this.componentTabSelected = false;
                }
                else
                {
                    if (!this.tabProcess)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("StorySectionErrorMsg") + this.minStory.ToString() + SharedFunctions.GetResourceString("ErrorMsgThrough") + this.maxStory.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.componentTabSelected = true;
                        this.StorySectionTextBox.Focus();
                    }
                    else
                    {
                        this.StorySectionTextBox.Focus();
                        this.tabProcess = false;
                    }
                }

                this.SetSectionToTable("StorySection", this.StorySectionTextBox.Text, this.sectionTypeValue.ToString());
                //int estimateRow = 0;
                //for (int i = 0; i < this.xmlCollectionDataSet.GetEstimate.Rows.Count; i++)
                //{
                //    if (this.xmlCollectionDataSet.GetEstimate.Rows[i]["SectionID"].ToString().Trim() == this.sectionTypeValue.ToString().Trim())
                //    {
                //        estimateRow = i;
                //        break;
                //    }
                //}
                //this.xmlCollectionDataSet.GetEstimate.Rows[estimateRow]["StorySection"] = this.StorySectionTextBox.Text;
                //this.xmlCollectionDataSet.GetEstimate.AcceptChanges();

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the StoryBuildingTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StoryBuildingTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                double storyBuildingValue = 0;
                double.TryParse(this.StoryBuildingTextBox.Text.Trim(), out storyBuildingValue);

                if (storyBuildingValue >= this.minStory && storyBuildingValue <= this.maxStory)
                {
                    this.StoryBuildingTextBox.Text = storyBuildingValue.ToString();
                    this.componentTabSelected = false;
                }
                else
                {
                    if (!this.tabProcess)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("StoryBuildingErrorMsg") + this.minStory.ToString() + SharedFunctions.GetResourceString("ErrorMsgThrough") + this.maxStory.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.componentTabSelected = true;
                        this.StoryBuildingTextBox.Focus();
                    }
                    else
                    {
                        this.StoryBuildingTextBox.Focus();
                        this.tabProcess = false;
                    }
                }

                this.SetSectionToTable("StoryBuilding", this.StoryBuildingTextBox.Text, this.sectionTypeValue.ToString());
                //int estimateRow = 0;
                //for (int i = 0; i < this.xmlCollectionDataSet.GetEstimate.Rows.Count; i++)
                //{
                //    if (this.xmlCollectionDataSet.GetEstimate.Rows[i]["SectionID"].ToString().Trim() == this.sectionTypeValue.ToString().Trim())
                //    {
                //        estimateRow = i;
                //        break;
                //    }
                //}
                //this.xmlCollectionDataSet.GetEstimate.Rows[estimateRow]["StoryBuilding"] = this.StoryBuildingTextBox.Text;
                //this.xmlCollectionDataSet.GetEstimate.AcceptChanges();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the SectionAreaTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SectionAreaTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                int areaValue = 0;
                int.TryParse(this.SectionAreaTextBox.Text.Trim().Replace(",", ""), out areaValue);

                if (areaValue >= this.minArea && areaValue <= this.maxArea)
                {
                    this.SectionAreaTextBox.Text = areaValue.ToString();
                    this.componentTabSelected = false;
                }
                else
                {
                    if (!this.tabProcess)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("AreaErrorMsg") + this.minArea.ToString() + SharedFunctions.GetResourceString("ErrorMsgThrough") + this.maxArea.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.componentTabSelected = true;
                        this.SectionAreaTextBox.Focus();
                    }
                    else
                    {
                        this.SectionAreaTextBox.Focus();
                        this.tabProcess = false;
                    }
                }

                this.SetSectionToTable("SectionArea", this.SectionAreaTextBox.Text, this.sectionTypeValue.ToString());
                //int estimateRow = 0;
                //for (int i = 0; i < this.xmlCollectionDataSet.GetEstimate.Rows.Count; i++)
                //{
                //    if (this.xmlCollectionDataSet.GetEstimate.Rows[i]["SectionID"].ToString().Trim() == this.sectionTypeValue.ToString().Trim())
                //    {
                //        estimateRow = i;
                //        break;
                //    }
                //}
                //this.xmlCollectionDataSet.GetEstimate.Rows[estimateRow]["SectionArea"] = this.SectionAreaTextBox.Text;
                //this.xmlCollectionDataSet.GetEstimate.AcceptChanges();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the StoryPerimeterTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StoryPerimeterTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                int storyPerimeterValue = 0;

                if (!string.IsNullOrEmpty(this.StoryPerimeterTextBox.Text.Trim()))
                {
                    int.TryParse(this.StoryPerimeterTextBox.Text.Trim(), out storyPerimeterValue);

                    if (storyPerimeterValue >= this.minPerimeter && storyPerimeterValue <= this.maxPerimeter)
                    {
                        this.StoryPerimeterTextBox.Text = storyPerimeterValue.ToString();
                        this.componentTabSelected = false;

                        if (!string.IsNullOrEmpty(this.StoryShapeTextBox.Text.Trim()))
                        {
                            this.StoryShapeTextBox.EmptyDecimalValue = true;
                            this.StoryShapeTextBox.Text = string.Empty;
                        }
                    }
                    else
                    {
                        if (!this.tabProcess)
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("StoryPerimeterErrorMsg") + this.minPerimeter.ToString() + SharedFunctions.GetResourceString("ErrorMsgThrough") + this.maxPerimeter.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.componentTabSelected = true;
                            this.StoryPerimeterTextBox.Focus();
                        }
                        else
                        {
                            this.StoryPerimeterTextBox.Focus();
                            this.tabProcess = false;
                        }
                    }
                }

                this.SetSectionToTable("StoryPerimeter", this.StoryPerimeterTextBox.Text, this.sectionTypeValue.ToString());
                //int estimateRow = 0;
                //for (int i = 0; i < this.xmlCollectionDataSet.GetEstimate.Rows.Count; i++)
                //{
                //    if (this.xmlCollectionDataSet.GetEstimate.Rows[i]["SectionID"].ToString().Trim() == this.sectionTypeValue.ToString().Trim())
                //    {
                //        estimateRow = i;
                //        break;
                //    }
                //}
                //this.xmlCollectionDataSet.GetEstimate.Rows[estimateRow]["StoryPerimeter"] = this.StoryPerimeterTextBox.Text;
                //this.xmlCollectionDataSet.GetEstimate.AcceptChanges();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the StoryShapeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StoryShapeTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.StoryShapeTextBox.Text.Trim()))
                {
                    double storyShapeValue = 0.0;
                    double.TryParse(this.StoryShapeTextBox.Text.Trim(), out storyShapeValue);

                    if (storyShapeValue >= this.minShape && storyShapeValue <= this.maxShape)
                    {
                        this.StoryShapeTextBox.Text = storyShapeValue.ToString();
                        this.componentTabSelected = false;

                        if (!string.IsNullOrEmpty(this.StoryPerimeterTextBox.Text.Trim()))
                        {
                            this.StoryPerimeterTextBox.Text = string.Empty;
                        }
                    }
                    else
                    {
                        if (!this.tabProcess)
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("StoryShapeErrorMsg") + this.minShape.ToString() + SharedFunctions.GetResourceString("ErrorMsgThrough") + this.maxShape.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.componentTabSelected = true;
                            this.StoryShapeTextBox.Focus();
                        }
                        else
                        {
                            this.StoryShapeTextBox.Focus();
                            this.tabProcess = false;
                        }
                    }
                }
                this.SetSectionToTable("StoryShape", this.StoryShapeTextBox.Text, this.sectionTypeValue.ToString());
                //int estimateRow = 0;
                //for (int i = 0; i < this.xmlCollectionDataSet.GetEstimate.Rows.Count; i++)
                //{
                //    if (this.xmlCollectionDataSet.GetEstimate.Rows[i]["SectionID"].ToString().Trim() == this.sectionTypeValue.ToString().Trim())
                //    {
                //        estimateRow = i;
                //        break;
                //    }
                //}
                //this.xmlCollectionDataSet.GetEstimate.Rows[estimateRow]["StoryShape"] = this.StoryShapeTextBox.Text;
                //this.xmlCollectionDataSet.GetEstimate.AcceptChanges();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the BasementPerimeterTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void BasementPerimeterTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.BasementPerimeterTextBox.Text.Trim()))
                {
                    int basementPerimeterValue = 0;
                    int.TryParse(this.BasementPerimeterTextBox.Text.Trim(), out basementPerimeterValue);

                    if (basementPerimeterValue >= this.minPerimeter && basementPerimeterValue <= this.maxPerimeter)
                    {
                        this.BasementPerimeterTextBox.Text = basementPerimeterValue.ToString();
                        this.componentTabSelected = false;

                        if (!string.IsNullOrEmpty(this.BasementShapeTextBox.Text.Trim()))
                        {
                            this.BasementShapeTextBox.EmptyDecimalValue = true;
                            this.BasementShapeTextBox.Text = string.Empty;
                        }
                    }
                    else
                    {
                        if (!this.tabProcess)
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("StoryPerimeterErrorMsg") + this.minPerimeter.ToString() + SharedFunctions.GetResourceString("ErrorMsgThrough") + this.maxPerimeter.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.componentTabSelected = true;
                            this.BasementPerimeterTextBox.Focus();
                        }
                        else
                        {
                            this.BasementPerimeterTextBox.Focus();
                            this.tabProcess = false;
                        }
                    }
                }
                this.SetSectionToTable("BasementPerimeter", this.BasementPerimeterTextBox.Text, "2");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the BasementShapeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void BasementShapeTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.BasementShapeTextBox.Text.Trim()))
                {
                    double basementShapeValue = 0.0;
                    double.TryParse(this.BasementShapeTextBox.Text.Trim(), out basementShapeValue);

                    if (basementShapeValue >= this.minShape && basementShapeValue <= this.maxShape)
                    {
                        this.BasementShapeTextBox.Text = basementShapeValue.ToString();
                        this.componentTabSelected = false;

                        if (!string.IsNullOrEmpty(this.BasementPerimeterTextBox.Text.Trim()))
                        {
                            this.BasementPerimeterTextBox.Text = string.Empty;
                        }
                    }
                    else
                    {
                        if (!this.tabProcess)
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("StoryShapeErrorMsg") + this.minShape.ToString() + SharedFunctions.GetResourceString("ErrorMsgThrough") + this.maxShape.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.componentTabSelected = true;
                            this.BasementShapeTextBox.Focus();
                        }
                        else
                        {
                            this.BasementShapeTextBox.Focus();
                            this.tabProcess = false;
                        }
                    }
                }
                this.SetSectionToTable("BasementShape", this.BasementShapeTextBox.Text, "2");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the BasementLevelTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void BasementLevelTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                double basementLevelValue = 0.0;
                double.TryParse(this.BasementLevelTextBox.Text.Trim(), out basementLevelValue);

                if (basementLevelValue >= this.minBasementLevel && basementLevelValue <= this.maxBasementLevel)
                {
                    this.BasementLevelTextBox.Text = basementLevelValue.ToString();
                    this.componentTabSelected = false;
                }
                else
                {
                    if (!this.tabProcess)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("BasementLevelsErrorMsg") + this.minBasementLevel.ToString() + SharedFunctions.GetResourceString("ErrorMsgThrough") + this.maxBasementLevel.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.componentTabSelected = true;
                        this.BasementLevelTextBox.Focus();
                    }
                    else
                    {
                        this.BasementLevelTextBox.Focus();
                        this.tabProcess = false;
                    }
                }
                this.SetSectionToTable("BasementLevel", this.BasementLevelTextBox.Text, "2");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the OccupancyGroupsGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void OccupancyGroupsGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= -1)
            {
                this.OccupancyNameGridClick(e.RowIndex);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the SectionTypeOccupancyCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SectionTypeOccupancyCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.SectionTypeOccupancyCombo.SelectedIndex != -1)
            {                
                int.TryParse(this.SectionTypeOccupancyCombo.SelectedValue.ToString(), out this.sectionTypeValue);
                if (this.sectionTypeValue == (int)SectionType.Section1)
                {
                    // Customize Section Grid
                    this.CustomizeOccupancySectionGrid();
                    this.FilterOccupancyRow(this.sectionTypeValue);
                    this.SectionMainPanel.Visible = true;
                    this.BasementMailPanel.Visible = false;
                }
                else
                {
                    // Customize Basement Grid
                    this.CustomizeOccupancyBasementGrid();
                    this.FilterOccupancyRow(this.sectionTypeValue);
                    this.SectionMainPanel.Visible = false;
                    this.BasementMailPanel.Visible = true;
                }
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the OccupancyCodeGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void OccupancyCodeGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= -1)
            {
                if (e.RowIndex < this.OccupancyCodeGrid.OriginalRowCount)
                {
                    DataGridViewSelectedRowCollection selectedOccupancyCodeCollection;
                    this.OccupancyCodeGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    selectedOccupancyCodeCollection = this.OccupancyCodeGrid.SelectedRows;

                    if (selectedOccupancyCodeCollection.Count > 0)
                    {                        
                        this.CustomizeSelectedOccupancyRow(selectedOccupancyCodeCollection);
                    }
                }
            }
        }

        /// <summary>
        /// Selecteds the component menu strip_ item clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void SelectedOccupancyMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                int keyValue = 0;

                if (e.ClickedItem.Text == SharedFunctions.GetResourceString("Delete"))
                {                    
                    this.selectedOccupancyMenuStrip.Visible = false;
                    if (this.SelectedOccupancyGrid.ActiveRow != null)
                    {
                        this.SelectedOccupancyGrid.ActiveRow.Appearance.ForeColor = Color.Black;
                    }

                    if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteMenuErrorMsg"), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int.TryParse(this.selectedOccupancyCodeDataTable.Rows[this.SelectedOccupancyGrid.ActiveRow.Index][SharedFunctions.GetResourceString("SectionTypeColumnName")].ToString(), out keyValue);
                        this.selectedOccupancyCodeDataTable.Rows.RemoveAt(this.SelectedOccupancyGrid.ActiveRow.Index);
                        this.SelectedOccupancyGrid.DataSource = this.selectedOccupancyCodeDataTable;
                        this.FilterOccupancyRow(keyValue);
                        this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
                    }

                    this.rcnCalculated = false;
                    this.ToEnableEditButtonInMasterForm();
                }
                else if (e.ClickedItem.Text == SharedFunctions.GetResourceString("Cancel"))
                {
                    if (this.SelectedOccupancyGrid.ActiveRow != null)
                    {
                        this.RestoreBackColorOccupancyRow(this.SelectedOccupancyGrid.ActiveRow.Index);
                    }
                }

                this.selectedOccupancyMenuStrip.Visible = false;
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
        private void SelectedOccupancyMenuStrip_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            try
            {
                if (this.SelectedOccupancyGrid.ActiveRow != null)
                {
                    this.RestoreBackColorOccupancyRow(this.SelectedOccupancyGrid.ActiveRow.Index);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #region Infragistics

        /// <summary>
        /// Handles the InitializeLayout event of the SelectedOccupancyGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void SelectedOccupancyGrid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                // Allow New Row
                e.Layout.Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Loads the classes saved record.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeRowEventArgs"/> instance containing the event data.</param>
        private void LoadSavedRecordClasses(object sender, InitializeRowEventArgs e)
        {
            if (this.selectedOccupancyCodeDataTable.Rows.Count > 0)
            {
                int sectionType = 0;
                int.TryParse(this.selectedOccupancyCodeDataTable.Rows[e.Row.Index][SharedFunctions.GetResourceString("SectionTypeColumnName")].ToString(), out sectionType);

                // To get Quality Description                
                double qualityValue = 0.0;
                double.TryParse(e.Row.Cells[SharedFunctions.GetResourceString("QualityIDColumnName")].Value.ToString(), out qualityValue);
                e.Row.Cells[SharedFunctions.GetResourceString("QualityDescriptionColumnName")].Value = this.BaseQualityDescription(qualityValue);

                int code = 0;
                int.TryParse(e.Row.Cells["Code"].Value.ToString(), out code);
                this.AddClassToSelectedRow(code);
                e.Row.Cells[SharedFunctions.GetResourceString("ClassColumnName")].ValueList = this.SelectedOccupancyGrid.DisplayLayout.ValueLists[this.classValueList];
                e.Row.Cells[SharedFunctions.GetResourceString("ClassColumnName")].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;

                if (e.Row.Cells[SharedFunctions.GetResourceString("ClassColumnName")].ValueList.ItemCount > 0)
                {                    
                    e.Row.Cells[SharedFunctions.GetResourceString("ClassColumnName")].Value = this.selectedOccupancyCodeDataTable.Rows[e.Row.Index][SharedFunctions.GetResourceString("ClassColumnName")].ToString();
                }

                if (sectionType == (int)SectionType.Basement)
                {
                    this.AddTypeToSelectedRow();
                    e.Row.Cells[SharedFunctions.GetResourceString("BasementTypeColumnName")].ValueList = this.SelectedOccupancyGrid.DisplayLayout.ValueLists[this.typeValueList];
                    e.Row.Cells[SharedFunctions.GetResourceString("BasementTypeColumnName")].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;

                    if (e.Row.Cells[SharedFunctions.GetResourceString("BasementTypeColumnName")].ValueList.ItemCount > 0)
                    {                        
                        e.Row.Cells[SharedFunctions.GetResourceString("BasementTypeColumnName")].Value = this.selectedOccupancyCodeDataTable.Rows[e.Row.Index][SharedFunctions.GetResourceString("BasementTypeColumnName")].ToString();
                    }
                }

                e.Row.Activate();
            }
        }

        /// <summary>
        /// Handles the InitializeRow event of the SelectedOccupancyGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeRowEventArgs"/> instance containing the event data.</param>
        private void SelectedOccupancyGrid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            try
            {
                if (this.formLoad)
                {
                    this.LoadSavedRecordClasses(sender, e);
                }

                if (this.rowSelected)
                {
                    if (this.selectedOccupancyCodeDataTable.Rows.Count > 0)
                    {
                        int code = 0;
                        int.TryParse(e.Row.Cells["Code"].Value.ToString(), out code);
                        this.AddClassToSelectedRow(code);
                        e.Row.Cells[SharedFunctions.GetResourceString("ClassColumnName")].ValueList = this.SelectedOccupancyGrid.DisplayLayout.ValueLists[this.classValueList];
                        e.Row.Cells[SharedFunctions.GetResourceString("ClassColumnName")].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;

                        if (e.Row.Cells[SharedFunctions.GetResourceString("ClassColumnName")].ValueList.ItemCount > 0)
                        {
                            e.Row.Cells[SharedFunctions.GetResourceString("ClassColumnName")].Value = e.Row.Cells[SharedFunctions.GetResourceString("ClassColumnName")].ValueList.GetValue(0);
                        }

                        if (this.sectionTypeValue == (int)SectionType.Basement)
                        {
                            this.AddTypeToSelectedRow();
                            e.Row.Cells[SharedFunctions.GetResourceString("BasementTypeColumnName")].ValueList = this.SelectedOccupancyGrid.DisplayLayout.ValueLists[this.typeValueList];
                            e.Row.Cells[SharedFunctions.GetResourceString("BasementTypeColumnName")].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;

                            if (e.Row.Cells[SharedFunctions.GetResourceString("BasementTypeColumnName")].ValueList.ItemCount > 0)
                            {
                                e.Row.Cells[SharedFunctions.GetResourceString("BasementTypeColumnName")].Value = e.Row.Cells[SharedFunctions.GetResourceString("BasementTypeColumnName")].ValueList.GetValue(2);
                            }
                        }
                    }
                }

                if (!this.occupancyExecutionRequied)
                {
                    if (e.Row != null)
                    {
                        this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.TemplateOnBottom;

                        if (e.Row.Cells[SharedFunctions.GetResourceString("CodeColumnName")].Value != null)
                        {
                            if (e.Row.Cells[SharedFunctions.GetResourceString("CodeColumnName")].Value == DBNull.Value || string.IsNullOrEmpty(e.Row.Cells[SharedFunctions.GetResourceString("CodeColumnName")].Value.ToString()))
                            {
                                e.Row.Cells[SharedFunctions.GetResourceString("SystemDescriptionColumnName")].Activation = Activation.Disabled;
                                e.Row.Cells[SharedFunctions.GetResourceString("SystemDescriptionColumnName")].Appearance.ForeColorDisabled = Color.Black;
                                e.Row.Cells[SharedFunctions.GetResourceString("SystemDescriptionColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);

                                e.Row.Cells[SharedFunctions.GetResourceString("OccupancyPercentageColumnName")].Activation = Activation.Disabled;
                                e.Row.Cells[SharedFunctions.GetResourceString("OccupancyPercentageColumnName")].Appearance.ForeColorDisabled = Color.Black;
                                e.Row.Cells[SharedFunctions.GetResourceString("OccupancyPercentageColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);

                                e.Row.Cells[SharedFunctions.GetResourceString("BasementTypeColumnName")].Activation = Activation.Disabled;
                                e.Row.Cells[SharedFunctions.GetResourceString("BasementTypeColumnName")].Appearance.ForeColorDisabled = Color.Black;
                                e.Row.Cells[SharedFunctions.GetResourceString("BasementTypeColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);

                                e.Row.Cells[SharedFunctions.GetResourceString("BasementAreaColumnName")].Activation = Activation.Disabled;
                                e.Row.Cells[SharedFunctions.GetResourceString("BasementAreaColumnName")].Appearance.ForeColorDisabled = Color.Black;
                                e.Row.Cells[SharedFunctions.GetResourceString("BasementAreaColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);

                                e.Row.Cells[SharedFunctions.GetResourceString("ClassColumnName")].Activation = Activation.Disabled;
                                e.Row.Cells[SharedFunctions.GetResourceString("ClassColumnName")].Appearance.ForeColorDisabled = Color.Black;
                                e.Row.Cells[SharedFunctions.GetResourceString("ClassColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);

                                e.Row.Cells[SharedFunctions.GetResourceString("DefaultHeightColumnName")].Activation = Activation.Disabled;
                                e.Row.Cells[SharedFunctions.GetResourceString("DefaultHeightColumnName")].Appearance.ForeColorDisabled = Color.Black;
                                e.Row.Cells[SharedFunctions.GetResourceString("DefaultHeightColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);

                                e.Row.Cells[SharedFunctions.GetResourceString("DefaultDepthColumnName")].Activation = Activation.Disabled;
                                e.Row.Cells[SharedFunctions.GetResourceString("DefaultDepthColumnName")].Appearance.ForeColorDisabled = Color.Black;
                                e.Row.Cells[SharedFunctions.GetResourceString("DefaultDepthColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);

                                e.Row.Cells[SharedFunctions.GetResourceString("QualityIDColumnName")].Activation = Activation.Disabled;
                                e.Row.Cells[SharedFunctions.GetResourceString("QualityIDColumnName")].Appearance.ForeColorDisabled = Color.Black;
                                e.Row.Cells[SharedFunctions.GetResourceString("QualityIDColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);

                                e.Row.Cells[SharedFunctions.GetResourceString("QualityDescriptionColumnName")].Activation = Activation.Disabled;
                                e.Row.Cells[SharedFunctions.GetResourceString("QualityDescriptionColumnName")].Appearance.ForeColorDisabled = Color.Black;
                                e.Row.Cells[SharedFunctions.GetResourceString("QualityDescriptionColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);

                                e.Row.Cells[SharedFunctions.GetResourceString("SectionTypeColumnName")].Activation = Activation.Disabled;
                                e.Row.Cells[SharedFunctions.GetResourceString("SectionTypeColumnName")].Appearance.ForeColorDisabled = Color.Black;
                                e.Row.Cells[SharedFunctions.GetResourceString("SectionTypeColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);
                            }
                            else
                            {
                                e.Row.Cells[SharedFunctions.GetResourceString("SystemDescriptionColumnName")].Activation = Activation.AllowEdit;
                                e.Row.Cells[SharedFunctions.GetResourceString("SystemDescriptionColumnName")].Appearance.BackColor = Color.White;
                                e.Row.Cells[SharedFunctions.GetResourceString("SystemDescriptionColumnName")].Appearance.ForeColor = Color.Black;

                                e.Row.Cells[SharedFunctions.GetResourceString("OccupancyPercentageColumnName")].Activation = Activation.AllowEdit;
                                e.Row.Cells[SharedFunctions.GetResourceString("OccupancyPercentageColumnName")].Appearance.BackColor = Color.White;
                                e.Row.Cells[SharedFunctions.GetResourceString("OccupancyPercentageColumnName")].Appearance.ForeColor = Color.Black;

                                e.Row.Cells[SharedFunctions.GetResourceString("BasementTypeColumnName")].Activation = Activation.AllowEdit;
                                e.Row.Cells[SharedFunctions.GetResourceString("BasementTypeColumnName")].Appearance.BackColor = Color.White;
                                e.Row.Cells[SharedFunctions.GetResourceString("BasementTypeColumnName")].Appearance.ForeColor = Color.Black;

                                e.Row.Cells[SharedFunctions.GetResourceString("BasementAreaColumnName")].Activation = Activation.AllowEdit;
                                e.Row.Cells[SharedFunctions.GetResourceString("BasementAreaColumnName")].Appearance.BackColor = Color.White;
                                e.Row.Cells[SharedFunctions.GetResourceString("BasementAreaColumnName")].Appearance.ForeColor = Color.Black;

                                e.Row.Cells[SharedFunctions.GetResourceString("ClassColumnName")].Activation = Activation.AllowEdit;
                                e.Row.Cells[SharedFunctions.GetResourceString("ClassColumnName")].Appearance.BackColor = Color.White;
                                e.Row.Cells[SharedFunctions.GetResourceString("ClassColumnName")].Appearance.ForeColor = Color.Black;

                                e.Row.Cells[SharedFunctions.GetResourceString("DefaultHeightColumnName")].Activation = Activation.AllowEdit;
                                e.Row.Cells[SharedFunctions.GetResourceString("DefaultHeightColumnName")].Appearance.BackColor = Color.White;
                                e.Row.Cells[SharedFunctions.GetResourceString("DefaultHeightColumnName")].Appearance.ForeColor = Color.Black;

                                e.Row.Cells[SharedFunctions.GetResourceString("DefaultDepthColumnName")].Activation = Activation.AllowEdit;
                                e.Row.Cells[SharedFunctions.GetResourceString("DefaultDepthColumnName")].Appearance.BackColor = Color.White;
                                e.Row.Cells[SharedFunctions.GetResourceString("DefaultDepthColumnName")].Appearance.ForeColor = Color.Black;

                                e.Row.Cells[SharedFunctions.GetResourceString("QualityIDColumnName")].Activation = Activation.AllowEdit;
                                e.Row.Cells[SharedFunctions.GetResourceString("QualityIDColumnName")].Appearance.BackColor = Color.White;
                                e.Row.Cells[SharedFunctions.GetResourceString("QualityIDColumnName")].Appearance.ForeColor = Color.Black;

                                e.Row.Cells[SharedFunctions.GetResourceString("QualityDescriptionColumnName")].Activation = Activation.AllowEdit;
                                e.Row.Cells[SharedFunctions.GetResourceString("QualityDescriptionColumnName")].Appearance.BackColor = Color.White;
                                e.Row.Cells[SharedFunctions.GetResourceString("QualityDescriptionColumnName")].Appearance.ForeColor = Color.Black;

                                e.Row.Cells[SharedFunctions.GetResourceString("SectionTypeColumnName")].Activation = Activation.AllowEdit;
                                e.Row.Cells[SharedFunctions.GetResourceString("SectionTypeColumnName")].Appearance.BackColor = Color.White;
                                e.Row.Cells[SharedFunctions.GetResourceString("SectionTypeColumnName")].Appearance.ForeColor = Color.Black;
                            }
                        }
                    }
                }

                this.selectedOccupancyCodeDataTable.AcceptChanges();
                this.occupancyExecutionRequied = false;
                this.rowSelected = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeExitEditMode event of the SelectedOccupancyGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs"/> instance containing the event data.</param>
        private void SelectedOccupancyGrid_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
        {
            try
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.SelectedOccupancyGrid.ActiveRow;
                Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.SelectedOccupancyGrid.ActiveCell;

                if (activeCell != null)
                { 
                    if (activeCell.Column.Index == 0)
                    {
                        if (!string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                        {
                            this.eventFired = true;
                            int.TryParse(activeCell.Text.Trim().ToString(), out this.typedOccupancyCode);
                            DataRow[] typedCode;
                            typedCode = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("OccupancyTableName")].Select(SharedFunctions.GetResourceString("OccupancyIDColumnName") + " = '" + this.typedOccupancyCode + "'");

                            if (typedCode.Length == 0)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("ValidCode"), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentTabSelected = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                this.componentTabSelected = false;
                            }
                        }
                        else
                        {
                            if (activeRow.IsAddRow)
                            {
                                this.componentTabSelected = false;
                                this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("ValidOccupancyCode"), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentTabSelected = true;
                                activeCell.SelText = activeRow.Cells[SharedFunctions.GetResourceString("CodeColumnName")].Value.ToString();
                                this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
                                e.Cancel = true;
                            }
                        }
                    }

                    if (activeCell.Column.Index == 2)
                    {                        
                        if (!string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                        {
                            this.eventFired = true;
                            int percentageValue = 0;
                            int percentageMaxValue = 100;
                            int percentageMinValue = 1;

                            try
                            {
                                int.TryParse(activeCell.Text.Trim().ToString(), out percentageValue);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("PercentRange") + percentageMinValue + " through " + percentageMaxValue, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentTabSelected = true;
                                e.Cancel = true;
                                return;
                            }

                            if (percentageValue < percentageMinValue || percentageValue > percentageMaxValue)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("PercentRange") + percentageMinValue + " through " + percentageMaxValue, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentTabSelected = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                this.componentTabSelected = false;
                            }
                        }
                    }

                    if (activeCell.Column.Index == 4)
                    {                        
                        if (!string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                        {
                            this.eventFired = true;
                            int basementAreaValue = 0;
                            int basementAreaMaxValue = 0;
                            int basementAreaMinValue = 0;

                            DataRow[] areaDataRow;
                            DataTable areaDataTable = new DataTable();
                            areaDataRow = this.commercialHtcXmlDataSet.Tables["Range"].Select("Key = 'BAREA'");
                            areaDataTable = this.DataRowToDataTable(areaDataRow);
                            int.TryParse(areaDataTable.Rows[0]["MinValue"].ToString(), out basementAreaMinValue);
                            int.TryParse(areaDataTable.Rows[0]["MaxValue"].ToString(), out basementAreaMaxValue);

                            try
                            {                                                            
                                int.TryParse(activeCell.Text.Trim().ToString(), out basementAreaValue);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("BasementAreaRange") + basementAreaMinValue + " through " + basementAreaMaxValue, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentTabSelected = true;
                                e.Cancel = true;
                                return;
                            }

                            if (basementAreaValue < basementAreaMinValue || basementAreaValue > basementAreaMaxValue)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("BasementAreaRange") + basementAreaMinValue + " through " + basementAreaMaxValue, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentTabSelected = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                this.componentTabSelected = false;
                            }
                        }
                    }

                    if (activeCell.Column.Index == 6)
                    {                        
                        if (!string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                        {
                            this.eventFired = true;
                            double defalutHeightValue = 0.0;
                            double defalutHeightMaxValue = 0.0;
                            double defalutHeightMinValue = 0.0;

                            DataRow[] areaDataRow;
                            DataTable areaDataTable = new DataTable();
                            areaDataRow = this.commercialHtcXmlDataSet.Tables["Range"].Select("Key = 'HEIGHT'");
                            areaDataTable = this.DataRowToDataTable(areaDataRow);
                            double.TryParse(areaDataTable.Rows[0]["MinValue"].ToString(), out defalutHeightMinValue);
                            double.TryParse(areaDataTable.Rows[0]["MaxValue"].ToString(), out defalutHeightMaxValue);

                            try
                            {                                                              
                                double.TryParse(activeCell.Text.Trim().ToString(), out defalutHeightValue);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("DefaultHeightRange") + defalutHeightMinValue + " through " + defalutHeightMaxValue, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentTabSelected = true;
                                e.Cancel = true;
                                return;
                            }

                            if (defalutHeightValue < defalutHeightMinValue || defalutHeightValue > defalutHeightMaxValue)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("DefaultHeightRange") + defalutHeightMinValue + " through " + defalutHeightMaxValue, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentTabSelected = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                activeCell.Value = defalutHeightValue.ToString("##0.00");
                                this.componentTabSelected = false;
                            }
                        }
                        else
                        {
                            activeCell.SelText = activeRow.Cells["TempDefaultHeight"].Value.ToString();
                        }
                    }

                    if (activeCell.Column.Index == 7)
                    {                        
                        if (!string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                        {
                            this.eventFired = true;
                            double defalutDepthValue = 0.0;
                            double defalutDepthMaxValue = 0.0;
                            double defalutDepthMinValue = 0.0;

                            DataRow[] areaDataRow;
                            DataTable areaDataTable = new DataTable();
                            areaDataRow = this.commercialHtcXmlDataSet.Tables["Range"].Select("Key = 'DEPTH'");
                            areaDataTable = this.DataRowToDataTable(areaDataRow);
                            double.TryParse(areaDataTable.Rows[0]["MinValue"].ToString(), out defalutDepthMinValue);
                            double.TryParse(areaDataTable.Rows[0]["MaxValue"].ToString(), out defalutDepthMaxValue);

                            try
                            {
                                double.TryParse(activeCell.Text.Trim().ToString(), out defalutDepthValue);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("DefaultDepthRange") + defalutDepthMinValue + " through " + defalutDepthMaxValue, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentTabSelected = true;
                                e.Cancel = true;
                                return;
                            }

                            if (defalutDepthValue < defalutDepthMinValue || defalutDepthValue > defalutDepthMaxValue)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("DefaultDepthRange") + defalutDepthMinValue + " through " + defalutDepthMaxValue, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentTabSelected = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                activeCell.Value = defalutDepthValue.ToString("##0.00");
                                this.componentTabSelected = false;
                            }
                        }
                        else
                        {
                            activeCell.SelText = activeRow.Cells["TempDefaultDepth"].Value.ToString();
                        }
                    }

                    if (activeCell.Column.Index == 8)
                    {
                        if (!string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                        {
                            this.eventFired = true;
                            double qualityValue = 0.0;

                            try
                            {
                                double.TryParse(activeCell.Text.Trim().ToString(), out qualityValue);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("QualityRange") + this.lowQuality + " through " + this.highQuality, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentTabSelected = true;
                                e.Cancel = true;
                                return;
                            }

                            if (qualityValue < this.lowQuality || qualityValue > this.highQuality)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("QualityRange") + this.lowQuality + " through " + this.highQuality, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentTabSelected = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                activeCell.Value = qualityValue.ToString("##0.0");
                                this.componentTabSelected = false;
                            }
                        }
                        else
                        {
                            activeCell.SelText = this.defaultRankValue.ToString();
                            activeRow.Cells["QualityDescription"].Value = this.BaseQualityDescription(this.defaultRankValue);
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
        /// Handles the AfterCellUpdate event of the SelectedOccupancyGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.CellEventArgs"/> instance containing the event data.</param>
        private void SelectedOccupancyGrid_AfterCellUpdate(object sender, CellEventArgs e)
        {
            try
            {                
                UltraGridCell activeCell = this.SelectedOccupancyGrid.ActiveCell;

                if (activeCell != null)
                {
                    if (e.Cell.Column.Index == 0)
                    {
                        this.PopulateOccupancyCode(activeCell);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseUp event of the SelectedOccupancyGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void SelectedOccupancyGrid_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (!this.componentTabSelected)
                {
                    Infragistics.Win.UIElement elementPoint = this.SelectedOccupancyGrid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));

                    if (elementPoint != null)
                    {
                        UltraGridRow activeRow = (UltraGridRow)elementPoint.GetContext(typeof(UltraGridRow));
                        UltraGridCell activeCell = (UltraGridCell)elementPoint.GetContext(typeof(UltraGridCell));

                        if (activeCell != null)
                        {
                            if (activeRow.Index <= this.SelectedOccupancyGrid.Rows.Count && activeRow.Index != -1 && !string.IsNullOrEmpty(this.SelectedOccupancyGrid.Rows[activeRow.Index].Cells["Code"].Value.ToString()))
                            {
                                if (e.Button == MouseButtons.Right)
                                {
                                    this.SelectedOccupancyGrid.Rows[activeRow.Index].Activate();
                                    this.ChangeBackColorOccupancyRow(activeRow.Index);
                                    this.selectedOccupancyMenuStrip.Show(this.SelectedOccupancyGrid, new Point(e.X, e.Y));
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
        /// Handles the KeyUp event of the SelectedOccupancyGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SelectedOccupancyGrid_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.SelectedOccupancyGrid.ActiveRow;
                Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.SelectedOccupancyGrid.ActiveCell;
                this.rcnCalculated = false;
                this.ToEnableEditButtonInMasterForm();

                if (activeCell != null)
                {
                    if (activeCell.Column.Index == 0)
                    {
                        if (e.KeyCode != Keys.Tab)
                        {
                            this.occupancyCodeModified = true;
                        }
 
                        if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                        {
                            if (string.IsNullOrEmpty(activeCell.Text.ToString()))
                            {
                                if (activeRow.Index == this.SelectedOccupancyGrid.Rows.Count - 1)
                                {
                                    this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.No;
                                }
                            }
                        }
                        else if (e.KeyCode == Keys.Tab)
                        {
                            if (activeRow.IsAddRow)
                            {
                                this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.No;
                                this.SelectedOccupancyGrid.DisplayLayout.TabNavigation = TabNavigation.NextControlOnLastCell;
                            }
                        }
                        else
                        {
                            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
                        }
                    }

                    if (activeCell.Column.Index == 8)
                    {
                        if (!string.IsNullOrEmpty(activeCell.Text) && activeCell.Text.Trim() != ".")
                        {
                            double qualityValue = 0.0;
                            double.TryParse(activeCell.Text, out qualityValue);

                            if (qualityValue >= this.lowQuality && qualityValue <= this.highQuality)
                            {
                                this.SelectedOccupancyGrid.Rows[activeCell.Row.Index].Cells[SharedFunctions.GetResourceString("QualityDescriptionColumnName")].Value = this.BaseQualityDescription(qualityValue);
                            }
                            else
                            {
                                this.SelectedOccupancyGrid.Rows[activeCell.Row.Index].Cells[SharedFunctions.GetResourceString("QualityDescriptionColumnName")].Value = string.Empty;
                            }
                        }
                        else
                        {
                            this.SelectedOccupancyGrid.Rows[activeCell.Row.Index].Cells[SharedFunctions.GetResourceString("QualityDescriptionColumnName")].Value = string.Empty;
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
        /// Handles the BeforeEnterEditMode event of the SelectedOccupancyGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void SelectedOccupancyGrid_BeforeEnterEditMode(object sender, CancelEventArgs e)
        {
            try
            {
                UltraGridCell activeCell = this.SelectedOccupancyGrid.ActiveCell;

                if (activeCell != null)
                {
                    if (activeCell.Column.Index == 1)
                    {
                        if (!string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                        {
                            this.occupancyDescription = activeCell.Text.Trim().ToString();
                        }
                    }

                    if (activeCell.Column.Index == 3)
                    {
                        this.rcnCalculated = false;
                        this.ToEnableEditButtonInMasterForm();
                    }

                    if (activeCell.Column.Index == 5)
                    {
                        this.rcnCalculated = false;
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
        /// Handles the AfterExitEditMode event of the SelectedOccupancyGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SelectedOccupancyGrid_AfterExitEditMode(object sender, EventArgs e)
        {
            try
            {                
                UltraGridCell activeCell = this.SelectedOccupancyGrid.ActiveCell;

                if (activeCell != null)
                {
                    if (activeCell.Column.Index == 0)
                    {
                        if (this.occupancyCodeModified)
                        {
                            this.PopulateOccupancyCode(activeCell);
                            this.occupancyCodeModified = false;
                        }
                    }

                    if (activeCell.Column.Index == 1)
                    {
                        if (string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                        {
                            activeCell.Value = this.occupancyDescription;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Infragestics

        #endregion Occupancy

        #region Components

        /// <summary>
        /// Handles the ItemClicked event of the SelectedComponentMenuStrip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ToolStripItemClickedEventArgs"/> instance containing the event data.</param>
        private void SelectedComponentMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                int keyValue = 0;

                if (e.ClickedItem.Text == SharedFunctions.GetResourceString("Delete"))
                {                    
                    this.selectedComponentMenuStrip.Visible = false;
                    if (this.SelectedComponentGridView.ActiveRow != null)
                    {
                        this.SelectedComponentGridView.ActiveRow.Appearance.ForeColor = Color.Black;
                    }

                    if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteMenuErrorMsg"), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int.TryParse(this.selectedComponentCodeDataTable.Rows[this.SelectedComponentGridView.ActiveRow.Index][SharedFunctions.GetResourceString("SectionTypeColumnName")].ToString(), out keyValue);
                        this.selectedComponentCodeDataTable.Rows.RemoveAt(this.SelectedComponentGridView.ActiveRow.Index);
                        this.SelectedComponentGridView.DataSource = this.selectedComponentCodeDataTable;
                        this.FilterComponentRow(keyValue);
                        this.SelectedComponentGridView.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
                    }

                    this.rcnCalculated = false;
                    this.ToEnableEditButtonInMasterForm();
                }
                else if (e.ClickedItem.Text == SharedFunctions.GetResourceString("MenuEdit"))
                {
                    this.ToEnableEditButtonInMasterForm();
                    if (this.SelectedComponentGridView.ActiveRow != null)
                    {
                        int code = 0;
                        int componentsId = 0;
                        this.editComponentHashTable.Add("Units", this.SelectedComponentGridView.Rows[this.SelectedComponentGridView.ActiveRow.Index].Cells[SharedFunctions.GetResourceString("F36000UnitsColumnName")].Value.ToString());
                        this.editComponentHashTable.Add("Percentage", this.SelectedComponentGridView.Rows[this.SelectedComponentGridView.ActiveRow.Index].Cells[SharedFunctions.GetResourceString("F36000PercentageColumnName")].Value.ToString());
                        this.editComponentHashTable.Add("Other2", this.SelectedComponentGridView.Rows[this.SelectedComponentGridView.ActiveRow.Index].Cells[SharedFunctions.GetResourceString("Other2ColumnName")].Value.ToString());
                        this.editComponentHashTable.Add("Quality", this.SelectedComponentGridView.Rows[this.SelectedComponentGridView.ActiveRow.Index].Cells["ComponentQualityID"].Value.ToString());
                        int.TryParse(this.SelectedComponentGridView.Rows[this.SelectedComponentGridView.ActiveRow.Index].Cells["ComponentUniqueID"].Value.ToString(), out this.componentUniqueId);

                        int.TryParse(this.SelectedComponentGridView.Rows[this.SelectedComponentGridView.ActiveRow.Index].Cells[SharedFunctions.GetResourceString("SystemCodeColumnName")].Value.ToString(), out code);
                        int.TryParse(this.SelectedComponentGridView.Rows[this.SelectedComponentGridView.ActiveRow.Index].Cells["ConstructionSystemID"].Value.ToString(), out componentsId);

                        this.editComponent = true;

                        // Load 3601 form
                        this.LoadEditComponentForm(code, componentsId);
                    }
                }
                else
                {
                    if (this.SelectedComponentGridView.ActiveRow != null)
                    {
                        this.RestoreBackColorComponentRow(this.SelectedComponentGridView.ActiveRow.Index);
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
                    this.RestoreBackColorComponentRow(this.SelectedComponentGridView.ActiveRow.Index);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the ComponentSystemGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ComponentSystemGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= -1)
                {
                    this.ComponentSystemGridClick(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the ComponentCodeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ComponentCodeGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= -1)
                {
                    if (e.RowIndex < this.ComponentCodeGridView.OriginalRowCount)
                    {
                        int componentCode = 0;
                        int componentsId = 0;
                        DataGridViewSelectedRowCollection selectedComponentCodeCollection;
                        this.ComponentCodeGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        selectedComponentCodeCollection = this.ComponentCodeGridView.SelectedRows;

                        if (selectedComponentCodeCollection[0].Cells[SharedFunctions.GetResourceString("ComponentIDColumnName")].Value != null)
                        {
                            int.TryParse(selectedComponentCodeCollection[0].Cells[SharedFunctions.GetResourceString("ComponentIDColumnName")].Value.ToString(), out componentCode);

                            if (this.ComponentSystemGridView.CurrentRow != null)
                            {                                
                                int.TryParse(this.ComponentSystemGridView.Rows[this.ComponentSystemGridView.CurrentRow.Index].Cells[SharedFunctions.GetResourceString("ConstructionSystemIDColumnName")].Value.ToString(), out componentsId);
                            }

                            // Load Edit Component Form 
                            this.LoadEditComponentForm(componentCode, componentsId);
                        }

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
        /// Handles the SelectionChangeCommitted event of the SectionTypeComponentCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SectionTypeComponentCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                int.TryParse(this.SectionTypeComponentCombo.SelectedValue.ToString(), out this.sectionTypeValue);
                this.LoadComponentTab();
                this.FilterComponentRow(this.sectionTypeValue);
                this.SectionTypeComponentCombo.SelectedValue = this.sectionTypeValue;
                this.LoadDataToTextBox();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #region Infragistics

        /// <summary>
        /// Handles the InitializeLayout event of the SelectedComponentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void SelectedComponentGridView_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                // Allow New Row
                e.Layout.Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
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

                if (activeCell != null)
                {
                    this.ToEnableEditButtonInMasterForm();

                    if (activeCell.Column.Index == 0)
                    {
                        if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                        {
                            if (string.IsNullOrEmpty(activeCell.Text.ToString()))
                            {
                                if (activeRow.Index == this.SelectedComponentGridView.Rows.Count - 1)
                                {
                                    this.SelectedComponentGridView.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.No;
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
                            if (activeRow.Index <= this.SelectedComponentGridView.Rows.Count && activeRow.Index != -1 && !string.IsNullOrEmpty(this.SelectedComponentGridView.Rows[activeRow.Index].Cells[SharedFunctions.GetResourceString("SystemCodeColumnName")].Value.ToString()))
                            {
                                if (e.Button == MouseButtons.Right)
                                {
                                    this.SelectedComponentGridView.Rows[activeRow.Index].Activate();
                                    this.ChangeBackColorComponentRow(activeRow.Index);
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

        /// <summary>
        /// Handles the BeforeExitEditMode event of the SelectedComponentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs"/> instance containing the event data.</param>
        private void SelectedComponentGridView_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
        {
            try
            {
                string expression = string.Empty;
                DataTable expressionDataTable = new DataTable();
                Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.SelectedComponentGridView.ActiveRow;
                Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.SelectedComponentGridView.ActiveCell;

                if (activeCell != null)
                { 
                    if (activeCell.Column.Index == 0)
                    {
                        if (!string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                        {
                            this.eventFired = true;
                            int.TryParse(activeCell.Text.Trim().ToString(), out this.typedComponentCode);
                            expression = this.CheckComponentCode().ToString();

                            DataRow[] expressionCode;                            
                            expressionCode = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Select(expression);
                            expressionDataTable = this.DataRowToDataTable(expressionCode);
                            DataRow[] typedCode;
                            typedCode = expressionDataTable.Select(SharedFunctions.GetResourceString("ComponentIDColumnName") + " =" + "'" + this.typedComponentCode + "'");

                            if (typedCode.Length == 0)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("ValidCode"), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);                                
                                this.componentTabSelected = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                this.componentTabSelected = false;                                
                            }
                        }
                        else
                        {
                            if (activeRow.IsAddRow)
                            {
                                this.componentTabSelected = false;                                
                                this.SelectedComponentGridView.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("ValidOccupancyCode"), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentTabSelected = true;
                                activeCell.SelText = activeRow.Cells["SystemCode"].Value.ToString();
                                this.SelectedComponentGridView.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
                                e.Cancel = true;
                            }
                        }
                    }

                    if (activeCell.Column.Index == 4)
                    {
                        if (!string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                        {
                            this.eventFired = true;
                            int percentageValue = 0;
                            int percentageMaxValue = 100;
                            int percentageMinValue = 1;

                            try
                            {                                
                                percentageValue = Convert.ToInt32(activeCell.Text.Trim().ToString());
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("PercentRange") + percentageMinValue + " through " + percentageMaxValue, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentTabSelected = true;
                                e.Cancel = true;
                                return;
                            }

                            if (percentageValue < percentageMinValue || percentageValue > percentageMaxValue)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("PercentRange") + percentageMinValue + " through " + percentageMaxValue, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentTabSelected = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                this.componentTabSelected = false;
                            }

                            if (!string.IsNullOrEmpty(activeRow.Cells[SharedFunctions.GetResourceString("F36000UnitsColumnName")].Value.ToString()))
                            {
                                activeRow.Cells[SharedFunctions.GetResourceString("F36000UnitsColumnName")].Value = string.Empty;
                            }
                        }
                        else
                        {
                            this.componentTabSelected = false;
                        }
                    }

                    if (activeCell.Column.Index == 14)
                    {
                        if (!string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                        {
                            this.eventFired = true;
                            double qualityValue = 0.0;

                            try
                            {
                                double.TryParse(activeCell.Text.Trim().ToString(), out qualityValue);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("QualityRange") + this.lowQuality + " through " + this.highQuality, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentTabSelected = true;
                                e.Cancel = true;
                                return;
                            }

                            if (qualityValue < this.lowQuality || qualityValue > this.highQuality)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("QualityRange") + this.lowQuality + " through " + this.highQuality, SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentTabSelected = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                activeCell.Value = qualityValue.ToString("##0.0");
                                this.componentTabSelected = false;
                            }
                        }
                        else
                        {
                            this.componentTabSelected = false;
                        }
                    }

                    if (activeCell.Column.Index == 3)
                    {
                        if (!string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                        {
                            this.eventFired = true;
                            int unitValue = 0;
                            int unitMaxValue = 0;
                            int unitMinValue = 0;

                            try
                            {
                                unitValue = Convert.ToInt32(activeCell.Text.Trim().ToString());
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("UnitRange") + activeRow.Cells[SharedFunctions.GetResourceString("Min_ColumnName")].Value.ToString() + " through " + activeRow.Cells[SharedFunctions.GetResourceString("Max_ColumnName")].Value.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentTabSelected = true;
                                e.Cancel = true;
                                return;
                            }

                            int.TryParse(activeRow.Cells[SharedFunctions.GetResourceString("Min_ColumnName")].Value.ToString(), out unitMinValue);
                            int.TryParse(activeRow.Cells[SharedFunctions.GetResourceString("Max_ColumnName")].Value.ToString(), out unitMaxValue);

                            if (unitValue < unitMinValue || unitValue > unitMaxValue)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("UnitRange") + activeRow.Cells[SharedFunctions.GetResourceString("Min_ColumnName")].Value.ToString() + " through " + activeRow.Cells[SharedFunctions.GetResourceString("Max_ColumnName")].Value.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentTabSelected = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                this.componentTabSelected = false;
                            }

                            if (!string.IsNullOrEmpty(activeRow.Cells[SharedFunctions.GetResourceString("F36000PercentageColumnName")].Value.ToString()))
                            {
                                activeRow.Cells[SharedFunctions.GetResourceString("F36000PercentageColumnName")].Value = string.Empty;
                            }
                        }
                        else
                        {
                            this.componentTabSelected = false;
                        }
                    }

                    if (activeCell.Column.Index == 6)
                    {
                        this.eventFired = true;
                        if (!string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                        {
                            double otherValue = 0.0;
                            double otherMaxValue = 0.0;
                            double otherMinValue = 0.0;

                            try
                            {
                                double.TryParse(activeCell.Text.Trim().ToString(), out otherValue);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("UnitRange") + activeRow.Cells[SharedFunctions.GetResourceString("OtherMinColumnName")].Value.ToString() + " through " + activeRow.Cells[SharedFunctions.GetResourceString("OtherMaxColumnName")].Value.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentTabSelected = true;
                                e.Cancel = true;
                                return;
                            }

                            double.TryParse(activeRow.Cells[SharedFunctions.GetResourceString("OtherMinColumnName")].Value.ToString(), out otherMinValue);
                            double.TryParse(activeRow.Cells[SharedFunctions.GetResourceString("OtherMaxColumnName")].Value.ToString(), out otherMaxValue);

                            if (otherValue < otherMinValue || otherValue > otherMaxValue)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("UnitRange") + activeRow.Cells[SharedFunctions.GetResourceString("OtherMinColumnName")].Value.ToString() + " through " + activeRow.Cells[SharedFunctions.GetResourceString("OtherMaxColumnName")].Value.ToString(), SharedFunctions.GetResourceString("ResidentialEstimator"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.componentTabSelected = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                activeCell.Value = otherValue.ToString("##0.00");
                                this.componentTabSelected = false;
                            }
                        }
                        else
                        {
                            this.componentTabSelected = false;
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
                            int.TryParse(activeCell.Text.Trim().ToString(), out this.typedComponentCode);
                            expression = this.CheckComponentCode().ToString();

                            DataRow[] expressionCode;                            
                            expressionCode = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Select(expression);
                            expressionDataTable = this.DataRowToDataTable(expressionCode);
                            DataRow[] typedCode;
                            typedCode = expressionDataTable.Select(SharedFunctions.GetResourceString("ComponentIDColumnName") + " =" + "'" + this.typedComponentCode + "'");

                            if (typedCode.Length > 0)
                            {
                                this.SelectedComponentGridView.UpdateData();
                                DataTable typedDataTable = new DataTable();
                                typedDataTable = this.DataRowToDataTable(typedCode);
                                this.CreateTypedComponentDataTable(typedDataTable);
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

                        e.Row.Cells[SharedFunctions.GetResourceString("SelectedSystemColumnName")].Activation = Activation.Disabled;
                        e.Row.Cells[SharedFunctions.GetResourceString("SelectedSystemColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);
                        e.Row.Cells[SharedFunctions.GetResourceString("SelectedSystemColumnName")].Appearance.ForeColorDisabled = Color.Black;
                        e.Row.Cells[SharedFunctions.GetResourceString("SelectedSystemColumnName")].Appearance.ForeColor = Color.Black;

                        e.Row.Cells[SharedFunctions.GetResourceString("ComponentColumnName")].Activation = Activation.Disabled;
                        e.Row.Cells[SharedFunctions.GetResourceString("ComponentColumnName")].Appearance.ForeColorDisabled = Color.Black;
                        e.Row.Cells[SharedFunctions.GetResourceString("ComponentColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);

                        e.Row.Cells[SharedFunctions.GetResourceString("Other1ColumnName")].Activation = Activation.Disabled;
                        e.Row.Cells[SharedFunctions.GetResourceString("Other1ColumnName")].Appearance.ForeColorDisabled = Color.Black;
                        e.Row.Cells[SharedFunctions.GetResourceString("Other1ColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);

                        if (e.Row.Cells[SharedFunctions.GetResourceString("SystemCodeColumnName")].Value == DBNull.Value || string.IsNullOrEmpty(e.Row.Cells[SharedFunctions.GetResourceString("SystemCodeColumnName")].Value.ToString()))
                        {
                            e.Row.Cells["ComponentQualityID"].Activation = Activation.Disabled;
                            e.Row.Cells["ComponentQualityID"].Appearance.ForeColorDisabled = Color.Black;
                            e.Row.Cells["ComponentQualityID"].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);
                        }
                        else
                        {
                            e.Row.Cells["ComponentQualityID"].Activation = Activation.AllowEdit;
                            e.Row.Cells["ComponentQualityID"].Appearance.BackColor = Color.White;
                        }

                        if (e.Row.Cells[SharedFunctions.GetResourceString("OtherMaxColumnName")].Value.ToString().Trim() == "0" || e.Row.Cells[SharedFunctions.GetResourceString("OtherMaxColumnName")].Value == DBNull.Value || string.IsNullOrEmpty(e.Row.Cells[SharedFunctions.GetResourceString("OtherMaxColumnName")].Value.ToString()))
                        {
                            e.Row.Cells[SharedFunctions.GetResourceString("Other2ColumnName")].Activation = Activation.Disabled;
                            e.Row.Cells[SharedFunctions.GetResourceString("Other2ColumnName")].Appearance.ForeColorDisabled = Color.Black;
                            e.Row.Cells[SharedFunctions.GetResourceString("Other2ColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);
                        }
                        else
                        {
                            e.Row.Cells[SharedFunctions.GetResourceString("Other2ColumnName")].Activation = Activation.AllowEdit;
                            e.Row.Cells[SharedFunctions.GetResourceString("Other2ColumnName")].Appearance.BackColor = Color.White;
                        }

                        if (e.Row.Cells[SharedFunctions.GetResourceString("PercentageAllowedColumnName")].Value.ToString().Trim() == "0" || e.Row.Cells[SharedFunctions.GetResourceString("PercentageAllowedColumnName")].Value == DBNull.Value || string.IsNullOrEmpty(e.Row.Cells[SharedFunctions.GetResourceString("PercentageAllowedColumnName")].Value.ToString()))
                        {
                            e.Row.Cells[SharedFunctions.GetResourceString("F36000PercentageColumnName")].Activation = Activation.Disabled;
                            e.Row.Cells[SharedFunctions.GetResourceString("F36000PercentageColumnName")].Appearance.ForeColorDisabled = Color.Black;
                            e.Row.Cells[SharedFunctions.GetResourceString("F36000PercentageColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);
                        }
                        else
                        {
                            e.Row.Cells[SharedFunctions.GetResourceString("F36000PercentageColumnName")].Activation = Activation.AllowEdit;
                            e.Row.Cells[SharedFunctions.GetResourceString("F36000PercentageColumnName")].Appearance.BackColor = Color.White;
                        }

                        if (e.Row.Cells[SharedFunctions.GetResourceString("UnitsAllowedColumnName")].Value.ToString().Trim() == "0" || e.Row.Cells[SharedFunctions.GetResourceString("UnitsAllowedColumnName")].Value == DBNull.Value || string.IsNullOrEmpty(e.Row.Cells[SharedFunctions.GetResourceString("UnitsAllowedColumnName")].Value.ToString()))
                        {
                            e.Row.Cells[SharedFunctions.GetResourceString("F36000UnitsColumnName")].Activation = Activation.Disabled;
                            e.Row.Cells[SharedFunctions.GetResourceString("F36000UnitsColumnName")].Appearance.ForeColorDisabled = Color.Black;
                            e.Row.Cells[SharedFunctions.GetResourceString("F36000UnitsColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);
                        }
                        else
                        {
                            e.Row.Cells[SharedFunctions.GetResourceString("F36000UnitsColumnName")].Activation = Activation.AllowEdit;
                            e.Row.Cells[SharedFunctions.GetResourceString("F36000UnitsColumnName")].Appearance.BackColor = Color.White;
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

        #endregion

        #endregion Components

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
                if (!string.IsNullOrEmpty(this.PropertyQualityTextBox.Text.Trim()))
                {
                    if (quality >= 1 && quality <= 6)
                    {
                        deprId = this.form36001Control.WorkItem.GetDeprTableNameId(this.valueSliceId, quality);
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
                else
                {
                    this.componentTabSelected = false;
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
                if (!string.IsNullOrEmpty(this.DeprYearTextBox.Text.Trim()))
                {
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
                else
                {
                    this.AgeTextBox.Text = string.Empty;
                    this.CalculateDeprProcess();
                    this.componentTabSelected = false;
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

                    if (!string.IsNullOrEmpty(this.ConditionTextBox.Text.Trim()))
                    {
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
                    else
                    {
                        this.ConditionTextBox.Text = string.Empty;
                        this.CalculateDeprProcess();
                        this.componentTabSelected = false;
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

        #region CommonMethods

        /// <summary>
        /// Loads the marshal swift commercial.
        /// </summary>
        private void LoadMarshalSwiftCommercial()
        {
            if (this.xmlCollectionDataSet.Tables.Count > 0)
            {
                this.msversionId = this.xmlCollectionDataSet.GetMarshallSwiftCommercial.Rows[0][this.xmlCollectionDataSet.GetMarshallSwiftCommercial.MSVersionIDColumn.ColumnName].ToString();
                this.costMultiplier = this.xmlCollectionDataSet.GetMarshallSwiftCommercial.Rows[0][this.xmlCollectionDataSet.GetMarshallSwiftCommercial.CostMultiplierColumn.ColumnName].ToString();
                this.zipCode = this.xmlCollectionDataSet.GetMarshallSwiftCommercial.Rows[0][this.xmlCollectionDataSet.GetMarshallSwiftCommercial.ZipCodeColumn.ColumnName].ToString();
                this.DefaultMultiplierTextBox.Text = this.defaultLocalMultiplier.ToString();
                this.LoadFormData();
                this.LoadBuildingData();
                this.FilterOccupancyRow((int)SectionType.Section1);
                this.FilterComponentRow((int)SectionType.Section1);
                this.CustomizeOccupancySectionGrid();
            }
        }

        /// <summary>
        /// Clears the marshal swift commercial.
        /// </summary>
        private void ClearMarshalSwiftCommercial()
        {
            this.xmlCollectionDataSet.Clear();
            this.commercialXml = string.Empty;
            this.commercialHtcXmlDataSet.Clear();
            this.commercialHtcXmlDataSet = new DataSet();
            this.componetSystemDataTable.Clear();
            this.selectedComponentCodeDataTable.Clear();
            this.selectedOccupancyCodeDataTable.Clear();
            this.ClearTextBoxValues();
        }

        /// <summary>
        /// Clears the text box values.
        /// </summary>
        private void ClearTextBoxValues()
        {
            this.LocalMultiplierTextBox.EmptyDecimalValue = true;
            this.LocalMultiplierTextBox.Text = string.Empty;
            this.DefaultMultiplierTextBox.EmptyDecimalValue = true;
            this.DefaultMultiplierTextBox.Text = string.Empty;
            this.ArchFeeTextBox.EmptyDecimalValue = true;
            this.ArchFeeTextBox.Text = string.Empty;
            this.StorySectionTextBox.EmptyDecimalValue = true;
            this.StorySectionTextBox.Text = string.Empty;
            this.StoryBuildingTextBox.EmptyDecimalValue = true;
            this.StoryBuildingTextBox.Text = string.Empty;
            this.SectionAreaTextBox.EmptyDecimalValue = true;
            this.SectionAreaTextBox.Text = string.Empty;
            this.StoryPerimeterTextBox.EmptyDecimalValue = true;
            this.StoryPerimeterTextBox.Text = string.Empty;
            this.StoryShapeTextBox.EmptyDecimalValue = true;
            this.StoryShapeTextBox.Text = string.Empty;
            this.SectionPerSfTextBox.EmptyDecimalValue = true;
            this.SectionPerSfTextBox.Text = string.Empty;
            this.BasementPerimeterTextBox.EmptyDecimalValue = true;
            this.BasementPerimeterTextBox.Text = string.Empty;
            this.BasementShapeTextBox.EmptyDecimalValue = true;
            this.BasementShapeTextBox.Text = string.Empty;
            this.BasementLevelTextBox.EmptyDecimalValue = true;
            this.BasementLevelTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Loads the building data.
        /// </summary>
        private void LoadBuildingData()
        {
            if (this.xmlCollectionDataSet.Tables.Count > 0)
            {
                int.TryParse(this.xmlCollectionDataSet.GetMarshallSwiftCommercial.Rows[0][this.xmlCollectionDataSet.GetMarshallSwiftCommercial.EstimateIDColumn.ColumnName].ToString(), out this.estimateIdValue);

                if (this.estimateIdValue > 0)
                {                 
                    this.LoadDataToTextBox();

                    if (this.xmlCollectionDataSet != null)
                    {
                        if (this.xmlCollectionDataSet.Tables.Count > 0)
                        {
                            // Get Component Values
                            this.selectedComponentCodeDataTable = this.xmlCollectionDataSet.GetComponent;
                            this.SelectedComponentGridView.DataSource = this.selectedComponentCodeDataTable;

                            // Get Occupancy Values
                            this.selectedOccupancyCodeDataTable = this.xmlCollectionDataSet.GetOccupancy;
                            this.SelectedOccupancyGrid.DataSource = this.selectedOccupancyCodeDataTable;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Loads the data to text box.
        /// </summary>
        private void LoadDataToTextBox()
        {
            // Load Occpancuy Building Data
            if (this.xmlCollectionDataSet != null)
            {
                if (this.xmlCollectionDataSet.Tables.Count > 0)
                {
                    if (this.xmlCollectionDataSet.GetEstimate.Rows.Count > 0)
                    {
                        int estimateRow = -1;
                        string valueSlice = string.Empty;
                        for (int i = 0; i < this.xmlCollectionDataSet.GetEstimate.Rows.Count; i++)
                        {
                            string selectedValue;
                            if (this.sectionTypeValue.ToString() == "1")
                            {
                                selectedValue = "2";
                            }
                            else
                            {
                                selectedValue = this.sectionTypeValue.ToString();
                            }

                            if (this.xmlCollectionDataSet.GetEstimate.Rows[i]["SectionID"].ToString().Trim() == selectedValue)
                            {
                                estimateRow = i;
                                valueSlice = this.xmlCollectionDataSet.GetEstimate.Rows[i]["ValueSliceID"].ToString().Trim();
                                break;
                            }
                        }
                        if (estimateRow >= 0 && !string.IsNullOrEmpty(valueSlice))
                        {
                            this.StorySectionTextBox.Text = this.xmlCollectionDataSet.GetEstimate.Rows[estimateRow]["StorySection"].ToString();
                            this.StoryBuildingTextBox.Text = this.xmlCollectionDataSet.GetEstimate.Rows[estimateRow]["StoryBuilding"].ToString();
                            this.SectionAreaTextBox.Text = this.xmlCollectionDataSet.GetEstimate.Rows[estimateRow]["SectionArea"].ToString();
                            this.SectionPerSfTextBox.Text = this.xmlCollectionDataSet.GetEstimate.Rows[0]["SectionPerSf"].ToString();
                            if ((string.Compare(this.xmlCollectionDataSet.GetEstimate.Rows[estimateRow]["StoryPerimeter"].ToString(), string.Empty, true) == 0))
                            {
                                this.StoryShapeTextBox.Text = this.xmlCollectionDataSet.GetEstimate.Rows[estimateRow]["StoryShape"].ToString();
                                this.StoryPerimeterTextBox.Text = string.Empty;
                            }
                            else
                            {
                                this.StoryShapeTextBox.Text = string.Empty;
                                this.StoryPerimeterTextBox.Text = this.xmlCollectionDataSet.GetEstimate.Rows[estimateRow]["StoryPerimeter"].ToString();
                            }

                          
                            if ((string.Compare(this.xmlCollectionDataSet.GetEstimate.Rows[0]["BasementPerimeter"].ToString(), string.Empty, true) == 0))
                            {
                                this.BasementPerimeterTextBox.Text = string.Empty;
                                this.BasementShapeTextBox.Text = this.xmlCollectionDataSet.GetEstimate.Rows[0]["BasementShape"].ToString();
                            }
                            else
                            {
                                this.BasementPerimeterTextBox.Text = this.xmlCollectionDataSet.GetEstimate.Rows[0]["BasementPerimeter"].ToString();
                                this.BasementShapeTextBox.Text = string.Empty;
                            }
                            this.LocalMultiplierTextBox.Text = this.xmlCollectionDataSet.GetEstimate.Rows[0][SharedFunctions.GetResourceString("LocalMultiplierColumnName")].ToString();
                            this.ArchFeeTextBox.Text = this.xmlCollectionDataSet.GetEstimate.Rows[0]["ArchFeePercentage"].ToString();
                            this.BasementLevelTextBox.Text = this.xmlCollectionDataSet.GetEstimate.Rows[0]["BasementLevel"].ToString();
                          
                            this.totalRcnValue = this.xmlCollectionDataSet.GetEstimate.Rows[0]["RcnValue"].ToString();
                            this.CalculateRcnTextBox.Text = this.totalRcnValue;
                            this.CalculatePerSf();

                            // Get Component Values
                          /*  this.selectedComponentCodeDataTable = this.xmlCollectionDataSet.GetComponent;
                            this.SelectedComponentGridView.DataSource = this.selectedComponentCodeDataTable;

                            // Get Occupancy Values
                            this.selectedOccupancyCodeDataTable = this.xmlCollectionDataSet.GetOccupancy;
                            this.rowSelected = true;
                            //this.SelectedOccupancyGrid.DataSource = null;
                            this.SelectedOccupancyGrid.DataSource = this.selectedOccupancyCodeDataTable;*/
                           // this.rowSelected = true;
                        }
                        else
                        {
                            this.LocalMultiplierTextBox.Text = this.xmlCollectionDataSet.GetEstimate.Rows[0][SharedFunctions.GetResourceString("LocalMultiplierColumnName")].ToString();
                            this.ArchFeeTextBox.Text = this.xmlCollectionDataSet.GetEstimate.Rows[0]["ArchFeePercentage"].ToString();
                            this.StoryBuildingTextBox.Text = string.Empty;
                            this.SectionAreaTextBox.Text = string.Empty;
                            if (estimateRow >= 0)
                            {
                                this.SectionAreaTextBox.Text = this.xmlCollectionDataSet.GetEstimate.Rows[estimateRow]["SectionArea"].ToString();
                            }
                            else
                            {
                                this.SectionAreaTextBox.Text = string.Empty;
                            }

                            this.StoryPerimeterTextBox.Text = string.Empty;
                            this.SectionPerSfTextBox.Text = string.Empty;
                            this.StorySectionTextBox.Text = "1";
                            this.StoryShapeTextBox.Text = "2.00";
                            this.totalRcnValue = string.Empty;
                            this.CalculateRcnTextBox.Text = this.totalRcnValue;
                            this.RcnldTextBox.Text = this.totalRcnValue;
                            this.CalculateDepreciationTextBox.Text = string.Empty;
                            this.CalculatePerSf();
                        }
                      
                    }
                    else
                    {
                        this.StorySectionTextBox.Text = "1";
                        this.StoryShapeTextBox.Text = "2.00";
                        this.BasementLevelTextBox.Text = "1.00";
                        this.BasementShapeTextBox.Text = "2.00";
                    }
                  /*  // Get Component Values
                    this.selectedComponentCodeDataTable = this.xmlCollectionDataSet.GetComponent;
                    this.SelectedComponentGridView.DataSource = this.selectedComponentCodeDataTable;

                    // Get Occupancy Values
                    this.selectedOccupancyCodeDataTable = this.xmlCollectionDataSet.GetOccupancy;
                    this.SelectedOccupancyGrid.DataSource = this.selectedOccupancyCodeDataTable;*/
                }
            }
        }

        /// <summary>
        /// Loads the form data.
        /// </summary>
        private void LoadFormData()
        {
            this.LoadOccupancyTab();
            this.LoadComponentTab();
        }

        /// <summary>
        /// Validates the occupancy percentage.
        /// </summary>
        /// <returns>bool</returns>
        private bool ValidateOccupancyPercentage()
        {
            for (int i = 1; i < this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows.Count; i++)
            {
                DataRow[] percentageRow;
                DataTable percentageDataTable = new DataTable();
                percentageRow = this.selectedOccupancyCodeDataTable.Select(SharedFunctions.GetResourceString("SectionTypeColumnName") + " ='" + this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows[i]["Key"].ToString() + "'");

                if (percentageRow.Length > 0)
                {
                    percentageDataTable = this.DataRowToDataTable(percentageRow);

                    for (int count = 0; count < percentageDataTable.Rows.Count; count++)
                    {
                        if (string.IsNullOrEmpty(percentageDataTable.Rows[count][SharedFunctions.GetResourceString("OccupancyPercentageColumnName")].ToString()) || percentageDataTable.Rows[count][SharedFunctions.GetResourceString("OccupancyPercentageColumnName")].ToString() == "0")
                        {
                            this.validationErrorString = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows[i]["Description"].ToString() + SharedFunctions.GetResourceString("OccupancyPercentageErrorMsg");
                            return false;
                        }
                    }

                    //return true;
                }
                //else
                //{
                //    return true;
                //}
            }
            return true;
        }

        /// <summary>
        /// Validates the stories.
        /// </summary>
        /// <returns>bool</returns>
        private bool ValidateStories()
        {
            for (int i = 0; i < this.xmlCollectionDataSet.GetEstimate.Rows.Count; i++)
            {
                int section = 0;
                int building = 0;
                //int.TryParse(this.StorySectionTextBox.Text.Trim(), out section);
                //int.TryParse(this.StoryBuildingTextBox.Text.Trim(), out building);
                int.TryParse(this.xmlCollectionDataSet.GetEstimate.Rows[i]["StorySection"].ToString(), out section);
                int.TryParse(this.xmlCollectionDataSet.GetEstimate.Rows[i]["StoryBuilding"].ToString(), out building);
                if (building < section && building != 0)
                {
                    this.validationErrorString = SharedFunctions.GetResourceString("SectionBuildingValueErrorMsg");
                    return false;
                }
                //else
                //{
                //    return true;
                //}
            }
            return true;
        }

        /// <summary>
        /// Validates the perimeter shape.
        /// </summary>
        /// <returns>bool</returns>
        private bool ValidateBasementPerimeterShape()
        {
            DataRow[] basementAreaRow;            
            basementAreaRow = this.selectedOccupancyCodeDataTable.Select(SharedFunctions.GetResourceString("SectionTypeColumnName") + " ='" + (int)SectionType.Basement + "'");

            if (basementAreaRow.Length > 0)
            {
                if (string.IsNullOrEmpty(this.BasementPerimeterTextBox.Text.Trim()) && string.IsNullOrEmpty(this.BasementShapeTextBox.Text.Trim()))
                {
                    this.validationErrorString = SharedFunctions.GetResourceString("BasementPerimeterShapeErrorMsg");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Validates the perimeter shape.
        /// </summary>
        /// <returns>bool</returns>
        private bool ValidateSectionPerimeterShape()
        {
            for (int i = 0; i < this.xmlCollectionDataSet.GetEstimate.Rows.Count; i++)
            {
                //if (string.IsNullOrEmpty(this.StoryPerimeterTextBox.Text.Trim()) && string.IsNullOrEmpty(this.StoryShapeTextBox.Text.Trim()))
                if (string.IsNullOrEmpty(this.xmlCollectionDataSet.GetEstimate.Rows[i]["StoryPerimeter"].ToString()) && string.IsNullOrEmpty(this.xmlCollectionDataSet.GetEstimate.Rows[i]["StoryShape"].ToString()))
                {
                    this.validationErrorString = SharedFunctions.GetResourceString("SectionPerimeterShapeErrorMsg");
                    return false;
                }
                //else
                //{
                //    return true;
                //}
            }
            return true;
        }

        /// <summary>
        /// Validates the basement area.
        /// </summary>
        /// <returns>bool</returns>
        private bool ValidateBasementArea()
        {
            DataRow[] basementAreaRow;
            DataTable validateAreaDataTable = new DataTable();
            basementAreaRow = this.selectedOccupancyCodeDataTable.Select(SharedFunctions.GetResourceString("SectionTypeColumnName") + " ='" + (int)SectionType.Basement + "'");

            if (basementAreaRow.Length > 0)
            {
                validateAreaDataTable = this.DataRowToDataTable(basementAreaRow);

                for (int count = 0; count < validateAreaDataTable.Rows.Count; count++)
                {
                    if (string.IsNullOrEmpty(validateAreaDataTable.Rows[count][SharedFunctions.GetResourceString("BasementAreaColumnName")].ToString()))
                    {
                        this.validationErrorString = SharedFunctions.GetResourceString("BasementAreaErrorMsg");
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Validates the levels value.
        /// </summary>
        /// <returns>bool</returns>
        private bool ValidateLevelsValue()
        {
            DataRow[] basementAreaRow;            
            basementAreaRow = this.selectedOccupancyCodeDataTable.Select(SharedFunctions.GetResourceString("SectionTypeColumnName") + " ='" + (int)SectionType.Basement + "'");

            if (basementAreaRow.Length > 0)
            {
                if (string.IsNullOrEmpty(this.BasementLevelTextBox.Text.Trim()) && string.IsNullOrEmpty(this.BasementLevelTextBox.Text.Trim()))
                {
                    this.validationErrorString = SharedFunctions.GetResourceString("BasementLevelErrorMsg");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Validates the occupancy.
        /// </summary>
        /// <returns>Occupancy</returns>
        private bool ValidateOccupancy()
        {
            for (int i = 1; i < this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows.Count; i++)
            {
                DataRow[] validateDataRow;
//                validateDataRow = this.selectedOccupancyCodeDataTable.Select(SharedFunctions.GetResourceString("SectionTypeColumnName") + " ='" + (int)SectionType.Section1 + "'");
                validateDataRow = this.selectedOccupancyCodeDataTable.Select(SharedFunctions.GetResourceString("SectionTypeColumnName") + " ='" + this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows[i]["Key"].ToString() + "'");
                //if (validateDataRow.Length > 0)
                //{
                //    return true;
                //}
                //else
                if (validateDataRow.Length <= 0)
                {
                    this.validationErrorString = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows[i]["Description"].ToString() + SharedFunctions.GetResourceString("OccupancyRequiredErrorMsg");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Validates the total area.
        /// </summary>
        /// <returns>area</returns>
        private bool ValidateTotalArea()
        {
            for (int i = 0; i < this.xmlCollectionDataSet.GetEstimate.Rows.Count; i++)
            {
                int area = 0;
                //int.TryParse(this.SectionAreaTextBox.Text.Trim().Replace(",", "").ToString(), out area);
                int.TryParse(this.xmlCollectionDataSet.GetEstimate.Rows[i]["SectionArea"].ToString(), out area);
                //if (area >= this.minArea && area <= this.maxArea)
                //{
                //    return true;
                //}
                //else
                //if (area < this.minArea && area > this.maxArea)
                if (area < this.minArea || area > this.maxArea)
                {
                    this.validationErrorString = SharedFunctions.GetResourceString("AreaErrorMsg") + this.minArea.ToString() + SharedFunctions.GetResourceString("ErrorMsgThrough") + this.maxArea.ToString();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Gets the min max value.
        /// </summary>
        private void GetMinMaxValue()
        {
            if (this.commercialHtcXmlDataSet != null)
            {
                if (this.commercialHtcXmlDataSet.Tables.Count > 0)
                {
                    // Get Quailty Range
                    if (this.commercialHtcXmlDataSet.Tables["Rank"].Rows.Count > 0)
                    {
                        // Quality Value
                        double.TryParse(this.commercialHtcXmlDataSet.Tables["Rank"].Rows[0][SharedFunctions.GetResourceString("RankIDColumnName")].ToString(), out this.lowQuality);
                        double.TryParse(this.commercialHtcXmlDataSet.Tables["Rank"].Rows[this.commercialHtcXmlDataSet.Tables["Rank"].Rows.Count - 1][SharedFunctions.GetResourceString("RankIDColumnName")].ToString(), out this.highQuality);
                    }

                    // Get Multiplier Range  
                    this.minMultiplier = this.GetMinValue("LM");
                    this.maxMultiplier = this.GetMaxValue("LM");

                    // Get Rounding Range  
                    int.TryParse(this.GetMinValue("ROUND").ToString(), out this.minRounding);
                    int.TryParse(this.GetMaxValue("ROUND").ToString(), out this.maxRounding);

                    // Get Story Range  
                    double.TryParse(this.GetMinValue("STORY").ToString(), out this.minStory);
                    double.TryParse(this.GetMaxValue("STORY").ToString(), out this.maxStory);

                    // Get BAREA Range  
                    int.TryParse(this.GetMinValue("BAREA").ToString(), out this.minArea);
                    int.TryParse(this.GetMaxValue("BAREA").ToString(), out this.maxArea);

                    // Get PERIMETER Range  
                    int.TryParse(this.GetMinValue("PERIM").ToString(), out this.minPerimeter);
                    int.TryParse(this.GetMaxValue("PERIM").ToString(), out this.maxPerimeter);

                    // Get Shape Range  
                    this.minShape = this.GetMinValue("SHAPE");
                    this.maxShape = this.GetMaxValue("SHAPE");

                    // Get Level Range  
                    double.TryParse(this.GetMinValue("LEVELS").ToString(), out this.minBasementLevel);
                    double.TryParse(this.GetMaxValue("LEVELS").ToString(), out this.maxBasementLevel);

                    this.defaultDataTable = this.commercialHtcXmlDataSet.Tables["Default"];

                    // Default Values
                    if (this.defaultDataTable.Rows.Count > 0)
                    {
                        // Rank Default Value                    
                        double.TryParse(this.defaultDataTable.Rows[0]["Rank"].ToString(), out this.defaultRankValue);

                        // LM                    
                        double.TryParse(this.defaultDataTable.Rows[0]["LocalMultiplier"].ToString(), out this.defaultLocalMultiplier);

                        // ArchFee                    
                        double.TryParse(this.defaultDataTable.Rows[0]["ArchitectFee"].ToString(), out this.defaultArchitectFee);

                        // Shape                    
                        double.TryParse(this.defaultDataTable.Rows[0]["Shape"].ToString(), out this.defaultShape);

                        // buildingStories                    
                        double.TryParse(this.defaultDataTable.Rows[0]["BuildingStories"].ToString(), out this.defaultBuildingStories);

                        // Rounding                    
                        int.TryParse(this.defaultDataTable.Rows[0]["Rounding"].ToString(), out this.defaultRounding);
                    }
                }
            }
        }

        /// <summary>
        /// Datas the row to data table.
        /// </summary>
        /// <param name="tempDataRow">The temp data row.</param>
        /// <returns>DataTable</returns>
        private DataTable DataRowToDataTable(DataRow[] tempDataRow)
        {
            DataSet convertedDataSet = new DataSet();
            DataTable emptyDataTable = new DataTable();

            if (tempDataRow.Length > 0)
            {
                convertedDataSet.Merge(tempDataRow);
                return convertedDataSet.Tables[0];
            }
            else
            {
                return emptyDataTable;
            }
        }

        /*
        /// <summary>
        /// Datas the row to data table.
        /// </summary>
        /// <param name="tempDataRow">The temp data row.</param>
        /// <returns>DataTable</returns>
        private DataTable DataRowsToDataTable(DataRow tempDataRow)
        {
            DataSet convertedDataSet = new DataSet();
            DataTable emptyDataTable = new DataTable();

            if (tempDataRow.Length > 0)
            {
                convertedDataSet.Merge(tempDataRow);
                return convertedDataSet.Tables[0];
            }
            else
            {
                return emptyDataTable;
            }
        }
        */

        /// <summary>
        /// Toes the enable edit button in master form.
        /// </summary>
        private void ToEnableEditButtonInMasterForm()
        {
            this.EditEnabled();
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                if (!this.rcnCalculated)
                {
                    this.CalculateRcnTextBox.EmptyDecimalValue = true;
                    this.CalculateRcnTextBox.Text = string.Empty;
                    this.totalRcnValue = string.Empty;

                    ////this.SectionPerSfTextBox.EmptyDecimalValue = false;
                    this.SectionPerSfTextBox.Text = string.Empty;

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
        /// Creates the building data XML.
        /// </summary>
        /// <returns>Building Data Xml</returns>
        private string CreateBuildingDataXml()
        {
            string buildingDataXml = string.Empty;
            DataTable bulidingDataDataTable = new DataTable();
            DataColumn[] buildingDataColumn = new DataColumn[] { new DataColumn("LocalMultiplier"), new DataColumn("ArchFeePercentage"), new DataColumn("Rounding"), new DataColumn("StorySection"), new DataColumn("StoryBuilding"), new DataColumn("SectionArea"), new DataColumn("StoryPerimeter"), new DataColumn("StoryShape"), new DataColumn("SectionPerSf"), new DataColumn("BasementPerimeter"), new DataColumn("BasementShape"), new DataColumn("BasementLevel"), new DataColumn("RcnValue"), new DataColumn("SectionID") };
            bulidingDataDataTable.Columns.AddRange(buildingDataColumn);

            for (int i = 0; i < this.xmlCollectionDataSet.GetEstimate.Rows.Count; i++)
            {
                DataRow buildingDataRow = bulidingDataDataTable.NewRow();
                if (i == 0)
                {
                    buildingDataRow["LocalMultiplier"] = this.LocalMultiplierTextBox.Text.Trim();
                    buildingDataRow["ArchFeePercentage"] = this.ArchFeeTextBox.Text.Trim();
                    buildingDataRow["Rounding"] = string.Empty;
                    buildingDataRow["BasementPerimeter"] = this.BasementPerimeterTextBox.Text.Trim();
                    buildingDataRow["BasementShape"] = this.BasementShapeTextBox.Text.Trim();
                    buildingDataRow["BasementLevel"] = this.BasementLevelTextBox.Text.Trim();
                    buildingDataRow["RcnValue"] = this.CalculateRcnTextBox.Text.Replace("$", "").Replace(",", "").Trim();
                }
                //buildingDataRow["StorySection"] = this.StorySectionTextBox.Text.Trim();
                //buildingDataRow["StoryBuilding"] = this.StoryBuildingTextBox.Text.Trim();
                //buildingDataRow["SectionArea"] = this.SectionAreaTextBox.Text.Trim().Replace(",", "");
                //buildingDataRow["StoryPerimeter"] = this.StoryPerimeterTextBox.Text.Trim();
                //buildingDataRow["StoryShape"] = this.StoryShapeTextBox.Text.Trim();
                //buildingDataRow["SectionPerSf"] = this.SectionPerSfTextBox.Text.Trim().Replace("$", "");
                buildingDataRow["StorySection"] = this.xmlCollectionDataSet.GetEstimate.Rows[i]["StorySection"].ToString().Trim();
                buildingDataRow["StoryBuilding"] = this.xmlCollectionDataSet.GetEstimate.Rows[i]["StoryBuilding"].ToString().Trim();
                buildingDataRow["SectionArea"] = this.xmlCollectionDataSet.GetEstimate.Rows[i]["SectionArea"].ToString().Trim().Replace(",", "");
                buildingDataRow["StoryPerimeter"] = this.xmlCollectionDataSet.GetEstimate.Rows[i]["StoryPerimeter"].ToString().Trim();
                buildingDataRow["StoryShape"] = this.xmlCollectionDataSet.GetEstimate.Rows[i]["StoryShape"].ToString().Trim();
                buildingDataRow["SectionPerSf"] = this.xmlCollectionDataSet.GetEstimate.Rows[i]["SectionPerSf"].ToString().Trim().Replace("$", "");
                buildingDataRow["SectionID"] = this.xmlCollectionDataSet.GetEstimate.Rows[i]["SectionID"].ToString().Trim();

                bulidingDataDataTable.Rows.Add(buildingDataRow);
            }
            buildingDataXml = TerraScanCommon.GetXmlString(bulidingDataDataTable);
            return buildingDataXml;
        }

        /// <summary>
        /// Checks the Main valve Id exists.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            if (!this.componentTabSelected)
            {
                //if (this.ValidateDepreciation())
                //{
                    if (this.ValidateOccupancy() && this.ValidateBasementArea() && this.ValidateTotalArea() && this.ValidateSectionPerimeterShape() && this.ValidateBasementPerimeterShape() && this.ValidateLevelsValue() && this.ValidateStories() && this.ValidateOccupancyPercentage())
                    {
                        
                            //if (this.CalculateTotalpercentage() != 100)
                        for (int i = 1; i < this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows.Count; i++)
                        {
                            if (this.CalculateTotalpercentage(this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows[i]["Key"].ToString()) != 100)
                            {
                                sliceValidationFields.RequiredFieldMissing = false;
                                sliceValidationFields.ErrorMessage = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows[i]["Description"].ToString() + SharedFunctions.GetResourceString("OccupancyPercentageErrorMsg");
                                break;
                            }
                        }
                        
                    }
                    else
                    {
                        sliceValidationFields.RequiredFieldMissing = false;
                        sliceValidationFields.ErrorMessage = this.validationErrorString;
                        this.validationErrorString = string.Empty;
                    }
                //}
                //else
                //{
                //    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredMsg");
                //    sliceValidationFields.RequiredFieldMissing = true;
                //    return sliceValidationFields;
                //}
            }
            else
            {
                sliceValidationFields.DisableNewMethod = true;
                sliceValidationFields.RequiredFieldMissing = false;               
                return sliceValidationFields;
            }

            return sliceValidationFields;
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

        #endregion CommonMethods

        #region Occupancy

        /// <summary>
        /// Populates the occupancy code.
        /// </summary>
        /// <param name="activeCell">The active cell.</param>
        private void PopulateOccupancyCode(UltraGridCell activeCell)
        {
            if (!string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
            {
                int.TryParse(activeCell.Text.Trim().ToString(), out this.typedOccupancyCode);                
                DataRow[] typedCode;
                typedCode = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("OccupancyTableName")].Select(SharedFunctions.GetResourceString("OccupancyIDColumnName") + " = " + "'" + this.typedOccupancyCode + "'");

                if (typedCode.Length > 0)
                {
                    this.SelectedOccupancyGrid.UpdateData();
                    this.typedOccupancyRow = true;
                    DataTable typedDataTable = new DataTable();
                    typedDataTable = this.DataRowToDataTable(typedCode);
                    this.CreateTypedOccupancyDataTable(typedDataTable);
                }
            }
        }

        /// <summary>
        /// Assigns the default values.
        /// </summary>
        private void AssignDefaultValues()
        {
            this.StorySectionTextBox.Text = "1";
            this.StoryShapeTextBox.Text = "2.00";
            this.BasementLevelTextBox.Text = "1.00";
            this.BasementShapeTextBox.Text = "2.00";
        }

        /// <summary>
        /// Calculates the per sf.
        /// </summary>
        private void CalculatePerSf()
        {
            // To caluculate PerSf
            double rcnValue = 0.0;
            double area = 0.0;
            double.TryParse(this.totalRcnValue, out rcnValue);
            double.TryParse(this.SectionAreaTextBox.Text.Trim().Replace(",", ""), out area);
            this.rcnCalculated = true;
            this.SectionPerSfTextBox.Text = (rcnValue / area).ToString();
            this.xmlCollectionDataSet.GetEstimate.Rows[0]["SectionPerSf"] = this.SectionPerSfTextBox.Text;
        }

        /// <summary>
        /// Gets the min value.
        /// </summary>
        /// <param name="rangeValue">The range value.</param>
        /// <returns>double</returns>
        private double GetMinValue(string rangeValue)
        {
            double minValue = 0;
            DataRow[] minDataRow;
            DataTable minDataTable = new DataTable();
            minDataRow = this.commercialHtcXmlDataSet.Tables["Range"].Select("Key = '" + rangeValue + "'");
            minDataTable = this.DataRowToDataTable(minDataRow);
            double.TryParse(minDataTable.Rows[0]["MinValue"].ToString(), out minValue);
            return minValue;
        }

        /// <summary>
        /// Gets the max value.
        /// </summary>
        /// <param name="rangeValue">The range value.</param>
        /// <returns>double</returns>
        private double GetMaxValue(string rangeValue)
        {
            double maxValue = 0;
            DataRow[] maxDataRow;
            DataTable maxDataTable = new DataTable();
            maxDataRow = this.commercialHtcXmlDataSet.Tables["Range"].Select("Key = '" + rangeValue + "'");
            maxDataTable = this.DataRowToDataTable(maxDataRow);
            double.TryParse(maxDataTable.Rows[0]["MaxValue"].ToString(), out maxValue);
            return maxValue;
        }

        /// <summary>
        /// Changes the back color occupancy row.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void ChangeBackColorOccupancyRow(int rowIndex)
        {
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells["Code"].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("SystemDescriptionColumnName")].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("SystemDescriptionColumnName")].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("OccupancyPercentageColumnName")].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("OccupancyPercentageColumnName")].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("BasementTypeColumnName")].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("BasementTypeColumnName")].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("BasementAreaColumnName")].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("BasementAreaColumnName")].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("ClassColumnName")].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("ClassColumnName")].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("DefaultHeightColumnName")].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("DefaultHeightColumnName")].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("DefaultDepthColumnName")].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("DefaultDepthColumnName")].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("QualityIDColumnName")].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("QualityIDColumnName")].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("QualityDescriptionColumnName")].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("QualityDescriptionColumnName")].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("SectionTypeColumnName")].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("SectionTypeColumnName")].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);

            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("Code")].Appearance.ForeColor = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("SystemDescriptionColumnName")].Appearance.ForeColor = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("SystemDescriptionColumnName")].Appearance.ForeColorDisabled = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("OccupancyPercentageColumnName")].Appearance.ForeColor = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("OccupancyPercentageColumnName")].Appearance.ForeColorDisabled = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("BasementTypeColumnName")].Appearance.ForeColor = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("BasementTypeColumnName")].Appearance.ForeColorDisabled = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("BasementAreaColumnName")].Appearance.ForeColor = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("BasementAreaColumnName")].Appearance.ForeColorDisabled = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("ClassColumnName")].Appearance.ForeColor = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("ClassColumnName")].Appearance.ForeColorDisabled = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("DefaultHeightColumnName")].Appearance.ForeColor = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("DefaultHeightColumnName")].Appearance.ForeColorDisabled = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("DefaultDepthColumnName")].Appearance.ForeColor = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("DefaultDepthColumnName")].Appearance.ForeColorDisabled = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("QualityIDColumnName")].Appearance.ForeColor = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("QualityIDColumnName")].Appearance.ForeColorDisabled = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("QualityDescriptionColumnName")].Appearance.ForeColor = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("QualityDescriptionColumnName")].Appearance.ForeColorDisabled = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("SectionTypeColumnName")].Appearance.ForeColor = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("SectionTypeColumnName")].Appearance.ForeColorDisabled = Color.White;
        }

        /// <summary>
        /// Restores the back color occupancy row.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void RestoreBackColorOccupancyRow(int rowIndex)
        {
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells["Code"].Appearance.BackColor = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("SystemDescriptionColumnName")].Appearance.BackColor = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("OccupancyPercentageColumnName")].Appearance.BackColor = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("BasementTypeColumnName")].Appearance.BackColor = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("BasementAreaColumnName")].Appearance.BackColor = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("ClassColumnName")].Appearance.BackColor = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("DefaultHeightColumnName")].Appearance.BackColor = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("DefaultDepthColumnName")].Appearance.BackColor = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("QualityIDColumnName")].Appearance.BackColor = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("QualityDescriptionColumnName")].Appearance.BackColor = Color.White;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("SectionTypeColumnName")].Appearance.BackColor = Color.White;

            this.SelectedOccupancyGrid.Rows[rowIndex].Cells["Code"].Appearance.ForeColor = Color.Black;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("SystemDescriptionColumnName")].Appearance.ForeColor = Color.Black;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("OccupancyPercentageColumnName")].Appearance.ForeColor = Color.Black;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("BasementTypeColumnName")].Appearance.ForeColor = Color.Black;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("BasementAreaColumnName")].Appearance.ForeColor = Color.Black;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("ClassColumnName")].Appearance.ForeColor = Color.Black;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("DefaultHeightColumnName")].Appearance.ForeColor = Color.Black;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("DefaultDepthColumnName")].Appearance.ForeColor = Color.Black;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("QualityIDColumnName")].Appearance.ForeColor = Color.Black;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("QualityDescriptionColumnName")].Appearance.ForeColor = Color.Black;
            this.SelectedOccupancyGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("SectionTypeColumnName")].Appearance.ForeColor = Color.Black;
        }

        /// <summary>
        /// Customizes the selected occupancy row.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void CustomizeSelectedOccupancyRow(DataGridViewSelectedRowCollection selectedRow)
        {
            DataTable selectedRowDataTable = new DataTable();
            selectedRowDataTable = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("OccupancyTableName")].Clone();
            //selectedRowDataTable = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("OccupancyTableName")].Copy();
            for (int count = 0; count < selectedRow.Count; count++)
            {
                DataRow newRow = selectedRowDataTable.NewRow();

                newRow["OccupancyID"] = selectedRow[count].Cells[SharedFunctions.GetResourceString("OccupancyIDColumnName")].Value.ToString();
                newRow["Name"] = selectedRow[count].Cells["OccupancyCodeName"].Value.ToString();
                newRow["DefaultHeight"] = selectedRow[count].Cells[SharedFunctions.GetResourceString("DefaultHeightColumnName")].Value.ToString();
                newRow["DefaultDepth"] = selectedRow[count].Cells[SharedFunctions.GetResourceString("DefaultDepthColumnName")].Value.ToString();
                newRow["Occupancy_Id"] = selectedRow[count].Cells[SharedFunctions.GetResourceString("Occupancy_IdColumnName")].Value.ToString();

                selectedRowDataTable.Rows.Add(newRow);
            }

            this.CreateTypedOccupancyDataTable(selectedRowDataTable);
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
            double decimalPart = keyValue - Math.Floor(keyValue);

            if (keyValue >= this.lowQuality && keyValue <= this.highQuality)
            {
                if (decimalPart > 0)
                {
                    qualityRow = this.commercialHtcXmlDataSet.Tables["Rank"].Select("RankID = " + "'" + keyValue + "'");
                    if (qualityRow.Length > 0)
                    {
                        qualityDataTable = this.DataRowToDataTable(qualityRow);
                        description = qualityDataTable.Rows[0]["Name"].ToString();
                        return description;
                    }

                    qualityRow = this.commercialHtcXmlDataSet.Tables["Rank"].Select("RankID >= " + "'" + keyValue + "'");
                    decimalQualityRow = this.commercialHtcXmlDataSet.Tables["Rank"].Select("RankID < " + "'" + keyValue + "'");
                    qualityDataTable = this.DataRowToDataTable(qualityRow);

                    if (decimalQualityRow.Length > 0)
                    {
                        decimalQualityDataTable = this.DataRowToDataTable(decimalQualityRow);
                        description = decimalQualityDataTable.Rows[decimalQualityDataTable.Rows.Count - 1]["Name"].ToString() + " / " + qualityDataTable.Rows[0]["Name"].ToString();
                        return description;
                    }
                    else
                    {
                        description = qualityDataTable.Rows[0]["Name"].ToString();
                        return description;
                    }
                }
                else
                {
                    qualityRow = this.commercialHtcXmlDataSet.Tables["Rank"].Select("RankID >= " + "'" + keyValue + "'");
                    qualityDataTable = this.DataRowToDataTable(qualityRow);
                    description = qualityDataTable.Rows[0]["Name"].ToString();
                    return description;
                }
            }
            else
            {
                return description;
            }
        }

        /// <summary>
        /// Loads the name of the occupancy.
        /// </summary>
        private void LoadOccupancyNameGrid()
        {
            if (this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("OccupancyNameTableName")] != null)
            {
                this.OccupancyGroupsGrid.DataSource = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("OccupancyNameTableName")];

                if (this.OccupancyGroupsGrid.OriginalRowCount > this.OccupancyGroupsGrid.NumRowsVisible)
                {
                    this.OccupancyNameScrollBar.Visible = false;
                }
                else
                {
                    this.OccupancyNameScrollBar.Visible = true;
                }
            }
        }

        /// <summary>
        /// Loads the occupancy tab.
        /// </summary>
        private void LoadOccupancyTab()
        {
            if (this.commercialHtcXmlDataSet.Tables.Count > 0)
            {
                if (this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")] != null)
                {
                    // Load SectionType Combo
                    this.SectionTypeOccupancyCombo.DataSource = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")];
                    this.SectionTypeOccupancyCombo.DisplayMember = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Columns[SharedFunctions.GetResourceString("DescriptionColumnName")].ColumnName;
                    this.SectionTypeOccupancyCombo.ValueMember = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Columns[SharedFunctions.GetResourceString("KeyColumnName")].ColumnName;
                    this.SectionTypeOccupancyCombo.SelectedValue = (int)SectionType.Section1;

                    // Load Component Setion Type
                    this.SectionTypeComponentCombo.DataSource = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")];
                    this.SectionTypeComponentCombo.DisplayMember = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Columns[SharedFunctions.GetResourceString("DescriptionColumnName")].ColumnName;
                    this.SectionTypeComponentCombo.ValueMember = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Columns[SharedFunctions.GetResourceString("KeyColumnName")].ColumnName;
                    this.SectionTypeComponentCombo.SelectedValue = (int)SectionType.Section1;

                    // Default Property Quality
                    this.PropertyQualityTextBox.Text = this.commercialHtcXmlDataSet.Tables["Rank"].Rows[3]["RankID"].ToString();

                    int.TryParse(this.SectionTypeOccupancyCombo.SelectedValue.ToString(), out this.sectionTypeValue);
                }

                this.AssignDefaultValues();
                this.LoadOccupancyNameGrid();
            }
        }

        /// <summary>
        /// Occupancies the name grid click.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void OccupancyNameGridClick(int rowIndex)
        {
            if (this.OccupancyGroupsGrid.OriginalRowCount > 0)
            {
                if (rowIndex >= 0)
                {
                    int nameId = 0;
                    int.TryParse(this.OccupancyGroupsGrid.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("OccupancyName_IdColumnName")].Value.ToString(), out nameId);
                    this.LoadOccupancyCodeGrid(nameId);
                }
            }
        }

        /// <summary>
        /// Occupancies the code grid.
        /// </summary>
        /// <param name="occupancyNameId">The occupancy name id.</param>
        private void LoadOccupancyCodeGrid(int occupancyNameId)
        {
            DataRow[] occupancyCodeDataRow;
            occupancyCodeDataRow = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("OccupancyTableName")].Select(SharedFunctions.GetResourceString("OccupancyName_IdColumnName") + " = " + "'" + occupancyNameId + "'");
            this.occupancyCodeDataTable = this.DataRowToDataTable(occupancyCodeDataRow);
            this.OccupancyCodeGrid.DataSource = this.occupancyCodeDataTable;

            if (this.OccupancyCodeGrid.OriginalRowCount > this.OccupancyCodeGrid.NumRowsVisible)
            {
                this.OccupancyCodeScrollBar.Visible = false;
            }
            else
            {
                this.OccupancyCodeScrollBar.Visible = true;
            }
        }

        /// <summary>
        /// Customizes all occupancy grid.
        /// </summary>
        private void CustomizeAllOccupancyGrid()
        {
            this.CustomizeOccupancyNameGrid();
            this.CustomizeOccupancyCodeGrid();
            this.CustomizeOccupancySectionGrid();
            this.LoadSelectedOccupancyColumnName();
        }

        /// <summary>
        /// Loads the name of the selected occupancy column.
        /// </summary>
        private void LoadSelectedOccupancyColumnName()
        {
            if (this.selectedOccupancyCodeDataTable.Rows.Count == 0)
            {
                DataColumn[] selectedOccupancyColumn = new DataColumn[] { new DataColumn(SharedFunctions.GetResourceString("CodeColumnName")), new DataColumn(SharedFunctions.GetResourceString("SystemDescriptionColumnName")), new DataColumn(SharedFunctions.GetResourceString("OccupancyPercentageColumnName")), new DataColumn(SharedFunctions.GetResourceString("BasementTypeColumnName")), new DataColumn(SharedFunctions.GetResourceString("BasementAreaColumnName")), new DataColumn(SharedFunctions.GetResourceString("ClassColumnName")), new DataColumn(SharedFunctions.GetResourceString("DefaultHeightColumnName")), new DataColumn(SharedFunctions.GetResourceString("DefaultDepthColumnName")), new DataColumn(SharedFunctions.GetResourceString("QualityIDColumnName")), new DataColumn(SharedFunctions.GetResourceString("QualityDescriptionColumnName")), new DataColumn(SharedFunctions.GetResourceString("SectionTypeColumnName")), new DataColumn("OccupancyUniqueID"), new DataColumn("TempDefaultHeight"), new DataColumn("TempDefaultDepth") };
                this.selectedOccupancyCodeDataTable.Columns.AddRange(selectedOccupancyColumn);
                this.SelectedOccupancyGrid.DataSource = this.selectedOccupancyCodeDataTable;
            }
        }

        /// <summary>
        /// Customizes the occupancy name grid.
        /// </summary>
        private void CustomizeOccupancyNameGrid()
        {
            this.OccupancyGroupsGrid.AllowUserToResizeColumns = false;
            this.OccupancyGroupsGrid.AutoGenerateColumns = false;
            this.OccupancyGroupsGrid.AllowUserToResizeRows = false;
            this.OccupancyGroupsGrid.StandardTab = true;
            this.OccupancyGroupsGrid.Columns["OccupancyGroupName"].DataPropertyName = SharedFunctions.GetResourceString("NameColumnName");
            this.OccupancyGroupsGrid.Columns[SharedFunctions.GetResourceString("OccupancyName_IdColumnName")].DataPropertyName = SharedFunctions.GetResourceString("OccupancyName_IdColumnName");
            this.OccupancyGroupsGrid.Columns["NameID"].DataPropertyName = "ID";
        }

        /// <summary>
        /// Customizes the occupancy code grid.
        /// </summary>
        private void CustomizeOccupancyCodeGrid()
        {
            this.OccupancyCodeGrid.AllowUserToResizeColumns = false;
            this.OccupancyCodeGrid.AutoGenerateColumns = false;
            this.OccupancyCodeGrid.AllowUserToResizeRows = false;
            this.OccupancyCodeGrid.StandardTab = true;
            this.OccupancyCodeGrid.Columns[SharedFunctions.GetResourceString("OccupancyIDColumnName")].DataPropertyName = SharedFunctions.GetResourceString("OccupancyIDColumnName");
            this.OccupancyCodeGrid.Columns["OccupancyCodeName"].DataPropertyName = SharedFunctions.GetResourceString("NameColumnName");
            this.OccupancyCodeGrid.Columns[SharedFunctions.GetResourceString("Occupancy_IdColumnName")].DataPropertyName = SharedFunctions.GetResourceString("Occupancy_IdColumnName");
            this.OccupancyCodeGrid.Columns[SharedFunctions.GetResourceString("DefaultHeightColumnName")].DataPropertyName = SharedFunctions.GetResourceString("DefaultHeightColumnName");
            this.OccupancyCodeGrid.Columns[SharedFunctions.GetResourceString("DefaultDepthColumnName")].DataPropertyName = SharedFunctions.GetResourceString("DefaultDepthColumnName");
        }

        /// <summary>
        /// Customizes the occupancy section grid.
        /// </summary>
        private void CustomizeOccupancySectionGrid()
        {
            // Assigning width
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["Code"].Width = 44;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["SystemDescription"].Width = 350;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["OccupancyPercentage"].Width = 42;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["BasementType"].Width = 0;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["BasementArea"].Width = 0;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["Class"].Width = 52;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["DefaultHeight"].Width = 49;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["DefaultDepth"].Width = 0;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["QualityID"].Width = 49;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["QualityDescription"].Width = 116;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["SectionType"].Width = 0;

            // Make Cell ReadOnly
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["QualityDescription"].CellActivation = Activation.NoEdit;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["QualityDescription"].TabStop = false;

            // Hiding some columns
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["BasementType"].Hidden = true;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["BasementArea"].Hidden = true;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["DefaultDepth"].Hidden = true;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["SectionType"].Hidden = true;

            // Combo Style
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["Class"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;

            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["OccupancyPercentage"].Hidden = false;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["DefaultHeight"].Hidden = false;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["SectionType"].Hidden = false;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["SectionType"].TabStop = false;
        }

        /// <summary>
        /// Customizes the occupancy basement grid.
        /// </summary>
        private void CustomizeOccupancyBasementGrid()
        {
            // Assigning width
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["Code"].Width = 44;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["SystemDescription"].Width = 233;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["OccupancyPercentage"].Width = 0;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["BasementType"].Width = 84;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["BasementArea"].Width = 68;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["Class"].Width = 47;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["DefaultHeight"].Width = 0;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["DefaultDepth"].Width = 50;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["QualityID"].Width = 59;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["QualityDescription"].Width = 116;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["SectionType"].Width = 0;

            // Make Cell ReadOnly
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["QualityDescription"].CellActivation = Activation.NoEdit;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["QualityDescription"].TabStop = false;

            // Hiding some columns
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["BasementType"].Hidden = false;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["BasementArea"].Hidden = false;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["DefaultDepth"].Hidden = false;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["SectionType"].Hidden = false;

            // Combo Style
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["BasementType"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["Class"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;

            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["OccupancyPercentage"].Hidden = true;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["DefaultHeight"].Hidden = true;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["SectionType"].Hidden = true;
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].Columns["SectionType"].TabStop = false;
        }

        /// <summary>
        /// Calculates the totalpercentage.
        /// </summary>
        /// <returns>int</returns>
        private int CalculateTotalpercentage(string rowCount)
        {
            int totalPercent = 0;
            
                DataRow[] percentageRow;
                DataTable percentageDataTable = new DataTable();
                percentageRow = this.selectedOccupancyCodeDataTable.Select(SharedFunctions.GetResourceString("SectionTypeColumnName") + " ='" + rowCount + "'");

                if (percentageRow.Length > 0)
                {
                    percentageDataTable = this.DataRowToDataTable(percentageRow);
                    int percentageTotal = 0;
                    int percentageValue = 0;
                    for (int count = 0; count < percentageDataTable.Rows.Count; count++)
                    {
                        //if (this.selectedOccupancyCodeDataTable.Rows.Count > 0)
                        //{
                        //    for (int j = 0; j < this.selectedOccupancyCodeDataTable.Rows.Count; j++)
                        //    {
                        //int.TryParse(this.selectedOccupancyCodeDataTable.Rows[count][SharedFunctions.GetResourceString("OccupancyPercentageColumnName")].ToString(), out percentageValue);
                        int.TryParse(percentageDataTable.Rows[count][SharedFunctions.GetResourceString("OccupancyPercentageColumnName")].ToString(), out percentageValue);
                        percentageTotal = percentageValue + percentageTotal;
                        //    }
                        //}
                    }
                    if (percentageTotal == 100)
                    {
                         //return 100;
                        totalPercent = 100;
                    }
                    else
                    {
                       // return percentageTotal;
                        totalPercent = percentageTotal;
                    }
                   
                }

               return totalPercent;
        }

        /// <summary>
        /// Filters the component row.
        /// </summary>
        /// <param name="keyValue">The key value.</param>
        private void FilterOccupancyRow(int keyValue)
        {
            FilterCondition filterCondition = new FilterCondition();
            //if (keyValue <= 2)
            //{
                filterCondition.CompareValue = keyValue;
            //}
            //else
            //{
            //    filterCondition.CompareValue = 2;
            //}
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].ColumnFilters[SharedFunctions.GetResourceString("SectionTypeColumnName")].FilterConditions.Clear();
            this.SelectedOccupancyGrid.DisplayLayout.Bands[0].ColumnFilters[SharedFunctions.GetResourceString("SectionTypeColumnName")].FilterConditions.Add(filterCondition);
        }

        /// <summary>
        /// Validates the type of the manufacturing.
        /// </summary>
        private void ValidateManufacturingType()
        {
            int occupancyNameId = 0;

            if (this.OccupancyGroupsGrid.CurrentRow != null)
            {
                int.TryParse(this.OccupancyGroupsGrid.Rows[this.OccupancyGroupsGrid.CurrentRow.Index].Cells["NameID"].Value.ToString(), out occupancyNameId);
            }
        }

        /// <summary>
        /// Creates the typed occupancy data table.
        /// </summary>
        /// <param name="typedRowDataTable">The typed row data table.</param>
        private void CreateTypedOccupancyDataTable(DataTable typedRowDataTable)
        {
            int activeRow = 0;
            if (this.SelectedOccupancyGrid.ActiveRow != null)
            {
                activeRow = this.SelectedOccupancyGrid.ActiveRow.Index;
            }

            // Remove the empty row
            if (this.selectedOccupancyCodeDataTable.Rows.Count > 0)
            {
                for (int rowCount = 0; rowCount < this.selectedOccupancyCodeDataTable.Rows.Count; rowCount++)
                {
                    if (string.IsNullOrEmpty(this.selectedOccupancyCodeDataTable.Rows[rowCount][SharedFunctions.GetResourceString("CodeColumnName")].ToString()))
                    {
                        this.selectedOccupancyCodeDataTable.Rows[rowCount].Delete();
                    }
                }
            }

            DataRow selectedDataRow = this.selectedOccupancyCodeDataTable.NewRow();

            selectedDataRow[SharedFunctions.GetResourceString("CodeColumnName")] = typedRowDataTable.Rows[0]["OccupancyID"].ToString();
            selectedDataRow[SharedFunctions.GetResourceString("SystemDescriptionColumnName")] = typedRowDataTable.Rows[0]["Name"].ToString();

            //if (this.sectionTypeValue == (int)SectionType.Section1)
            if (this.sectionTypeValue >= (int)SectionType.Section1)
            {                
                int percentage = 0;
                percentage = 100 - this.CalculateTotalpercentage(this.sectionTypeValue.ToString());
                if (percentage >= 0 && percentage <= 100)
                {
                    selectedDataRow[SharedFunctions.GetResourceString("OccupancyPercentageColumnName")] = percentage.ToString();
                }
                else
                {
                    selectedDataRow[SharedFunctions.GetResourceString("OccupancyPercentageColumnName")] = "0";
                }

                double tempHeight = 0.0;
                double.TryParse(typedRowDataTable.Rows[0]["DefaultHeight"].ToString(), out tempHeight);
                if (tempHeight > 0.0)
                {                    
                    selectedDataRow[SharedFunctions.GetResourceString("DefaultHeightColumnName")] = tempHeight.ToString("##0.00");
                }
                else
                {
                    selectedDataRow[SharedFunctions.GetResourceString("DefaultHeightColumnName")] = string.Empty;
                }
                                
                selectedDataRow[SharedFunctions.GetResourceString("BasementTypeColumnName")] = string.Empty;
                selectedDataRow[SharedFunctions.GetResourceString("BasementAreaColumnName")] = string.Empty;
            }
            else
            {
                double tempDepth = 0.0;
                double.TryParse(typedRowDataTable.Rows[0]["DefaultDepth"].ToString(), out tempDepth);
                if (tempDepth > 0.0)
                {                    
                    selectedDataRow[SharedFunctions.GetResourceString("DefaultDepthColumnName")] = tempDepth.ToString("##0.00");
                }
                else
                {
                    selectedDataRow[SharedFunctions.GetResourceString("DefaultDepthColumnName")] = string.Empty;
                }

                selectedDataRow[SharedFunctions.GetResourceString("OccupancyPercentageColumnName")] = string.Empty;                
                selectedDataRow[SharedFunctions.GetResourceString("BasementTypeColumnName")] = string.Empty;
                selectedDataRow[SharedFunctions.GetResourceString("BasementAreaColumnName")] = string.Empty;
            }

            // To add class combo in grid
            int.TryParse(typedRowDataTable.Rows[0]["OccupancyID"].ToString(), out this.occupancyId);            
            selectedDataRow[SharedFunctions.GetResourceString("ClassColumnName")] = string.Empty;
            selectedDataRow[SharedFunctions.GetResourceString("QualityIDColumnName")] = this.defaultRankValue.ToString("##0.0"); 
            selectedDataRow[SharedFunctions.GetResourceString("QualityDescriptionColumnName")] = this.BaseQualityDescription(this.defaultRankValue);
            selectedDataRow[SharedFunctions.GetResourceString("SectionTypeColumnName")] = this.sectionTypeValue.ToString();
            selectedDataRow["TempDefaultHeight"] = typedRowDataTable.Rows[0]["DefaultHeight"].ToString();
            selectedDataRow["TempDefaultDepth"] = typedRowDataTable.Rows[0]["DefaultDepth"].ToString();
            selectedDataRow["OccupancyUniqueID"] = string.Empty;

            this.rowSelected = true;

            if (this.typedOccupancyRow)
            {
                if (this.selectedOccupancyCodeDataTable.Rows.Count > 0)
                {
                    if (this.selectedOccupancyCodeDataTable.Rows.Count == activeRow)
                    {
                        this.selectedOccupancyCodeDataTable.Rows.RemoveAt(activeRow - 1);
                        this.selectedOccupancyCodeDataTable.Rows.InsertAt(selectedDataRow, activeRow - 1);
                        this.SelectedOccupancyGrid.Rows[activeRow - 1].Activate();                        
                    }
                    else
                    {
                        this.selectedOccupancyCodeDataTable.Rows.RemoveAt(activeRow);
                        this.selectedOccupancyCodeDataTable.Rows.InsertAt(selectedDataRow, activeRow);
                        this.SelectedOccupancyGrid.Rows[activeRow].Activate();                        
                    }

                    this.occupancyExecutionRequied = true;
                }
            }
            else
            {
                this.selectedOccupancyCodeDataTable.Rows.InsertAt(selectedDataRow, this.SelectedOccupancyGrid.Rows.Count);
            }

            this.selectedOccupancyCodeDataTable.AcceptChanges();

            ////
           /* if (this.selectedOccupancyCodeDataTable.Rows.Count > 0)
            {
               // this.xmlCollectionDataSet.GetOccupancy.Rows.Clear();
                this.xmlCollectionDataSet.GetOccupancy.Merge(selectedOccupancyCodeDataTable);
            }*/
            ////
            this.rcnCalculated = false;
            this.typedOccupancyRow = false;
            this.SelectedOccupancyGrid.DataSource = this.selectedOccupancyCodeDataTable;

            if (this.SelectedOccupancyGrid.Rows.Count > 0)
            {               
                this.SelectedOccupancyGrid.Rows[this.SelectedOccupancyGrid.Rows.Count - 1].Cells[0].Activated = true;
            }

            this.ToEnableEditButtonInMasterForm();
        }

        /// <summary>
        /// Adds the class to selected row.
        /// </summary>
        /// <param name="occupancyIdValue">The occupancy id value.</param>
        private void AddClassToSelectedRow(int occupancyIdValue)
        {
            this.classValueList = System.Guid.NewGuid().ToString();

            if (this.SelectedOccupancyGrid.DisplayLayout.ValueLists.Exists(this.classValueList))
            {
                return;
            }

            ValueList objValueList = this.SelectedOccupancyGrid.DisplayLayout.ValueLists.Add(this.classValueList);

            // To filter Classes ID
            int classesId = 0;
            DataRow[] classesDataRow;
            classesDataRow = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("ClassesTableName")].Select(SharedFunctions.GetResourceString("OccupancyIDColumnName") + " = " + "'" + occupancyIdValue + "'");

            foreach (DataRow dr in classesDataRow)
            {
                int.TryParse(dr[1].ToString(), out classesId);
            }

            // To filter class datatable            
            DataRow[] classDataRow;
            classDataRow = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("ClassTableName")].Select(SharedFunctions.GetResourceString("Classes_IdColumnName") + " = " + "'" + classesId + "'");
            this.occupancyClassDataTable = this.DataRowToDataTable(classDataRow);

            if (this.occupancyClassDataTable.Rows.Count > 0)
            {
                // Iterates to Add DisplayMember and DisplayValue from Table
                for (int count = 0; count < this.occupancyClassDataTable.Rows.Count; count++)
                {                    
                    objValueList.ValueListItems.Add(this.occupancyClassDataTable.Rows[count][SharedFunctions.GetResourceString("ClassIDColumnName")].ToString());
                }
            }
        }

        /// <summary>
        /// Adds the type to selected row.
        /// </summary>
        private void AddTypeToSelectedRow()
        {
            this.typeValueList = System.Guid.NewGuid().ToString();

            if (this.SelectedOccupancyGrid.DisplayLayout.ValueLists.Exists(this.typeValueList))
            {
                return;
            }

            ValueList objValueList = this.SelectedOccupancyGrid.DisplayLayout.ValueLists.Add(this.typeValueList);

            this.occupancyTypeDataTable = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("BasementTypeTableName")];

            if (this.occupancyTypeDataTable.Rows.Count > 0)
            {
                // Iterates to Add DisplayMember and DisplayValue from Table
                for (int count = 0; count < this.occupancyTypeDataTable.Rows.Count; count++)
                {                    
                    objValueList.ValueListItems.Add(this.occupancyTypeDataTable.Rows[count][SharedFunctions.GetResourceString("NameColumnName")].ToString());
                }
            }
        }

        #endregion Occupancy

        #region Components

        /// <summary>
        /// Gets the construction system id.
        /// </summary>
        /// <param name="currentComponentsId">The current components id.</param>
        /// <returns>string</returns>
        private string GetConstructionSystemId(int currentComponentsId)
        {
            string constructionsystem_Id = string.Empty;
            string findConstructionSystem_Id = "Components_Id = '" + currentComponentsId + "'";

            DataRow[] findConstructionSystem_IdDataRow = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("ComponentsTableName")].Select(findConstructionSystem_Id);
            constructionsystem_Id = findConstructionSystem_IdDataRow[0][SharedFunctions.GetResourceString("ConstructionSystem_IdColumnName")].ToString();

            string findconstructionsystemId = "ConstructionSystem_Id = '" + constructionsystem_Id + "'";
            DataRow[] findconstructionsystemIdDataRow = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("ConstructionSystemTableName")].Select(findconstructionsystemId);
            return findconstructionsystemIdDataRow[0][SharedFunctions.GetResourceString("ConstructionSystemIDColumnName")].ToString();
        }

        /// <summary>
        /// Creates the typed component data table.
        /// </summary>
        /// <param name="typedRowDataTable">The typed row data table.</param>
        private void CreateTypedComponentDataTable(DataTable typedRowDataTable)
        {
            int activeRow = 0;
            if (this.SelectedComponentGridView.ActiveRow != null)
            {
                activeRow = this.SelectedComponentGridView.ActiveRow.Index;
            }

            // Remove the empty row
            if (this.selectedComponentCodeDataTable.Rows.Count > 0)
            {
                for (int rowCount = 0; rowCount < this.selectedComponentCodeDataTable.Rows.Count; rowCount++)
                {
                    if (string.IsNullOrEmpty(this.selectedComponentCodeDataTable.Rows[rowCount][SharedFunctions.GetResourceString("SystemCodeColumnName")].ToString()))
                    {
                        this.selectedComponentCodeDataTable.Rows[rowCount].Delete();
                    }
                }
            }

            DataRow selectedDataRow = this.selectedComponentCodeDataTable.NewRow();
            int otherMax = 0;

            selectedDataRow[SharedFunctions.GetResourceString("SystemCodeColumnName")] = typedRowDataTable.Rows[0][SharedFunctions.GetResourceString("ComponentIDColumnName")].ToString();

            if (this.ComponentSystemGridView.CurrentRow.Index >= 0)
            {                
                DataRow[] selectedComponentIdDataRow;
                DataRow[] selectedSystemDataRow;
                DataTable selectedSystemDataTable = new DataTable();                
                selectedComponentIdDataRow = this.commercialHtcXmlDataSet.Tables["Components"].Select("Components_Id = " + "'" + typedRowDataTable.Rows[0]["Components_Id"].ToString() + "'");

                if (selectedComponentIdDataRow.Length > 0)
                {
                    selectedSystemDataRow = this.commercialHtcXmlDataSet.Tables["ConstructionSystem"].Select("ConstructionSystem_Id = " + "'" + selectedComponentIdDataRow[0].ItemArray[1].ToString() + "'");

                    if (selectedSystemDataRow.Length > 0)
                    {
                        selectedSystemDataTable = this.DataRowToDataTable(selectedSystemDataRow);
                        selectedDataRow[SharedFunctions.GetResourceString("SelectedSystemColumnName")] = selectedSystemDataTable.Rows[0]["Name"].ToString();
                    }
                }
            }

            selectedDataRow[SharedFunctions.GetResourceString("ComponentColumnName")] = typedRowDataTable.Rows[0][SharedFunctions.GetResourceString("NameColumnName")].ToString();
            selectedDataRow[SharedFunctions.GetResourceString("F36000UnitsColumnName")] = string.Empty;
            selectedDataRow[SharedFunctions.GetResourceString("F36000PercentageColumnName")] = string.Empty;

            int.TryParse(typedRowDataTable.Rows[0][SharedFunctions.GetResourceString("OtherMaxColumnName")].ToString(), out otherMax);

            if (otherMax == 0)
            {
                selectedDataRow[SharedFunctions.GetResourceString("Other1ColumnName")] = string.Empty;
            }
            else
            {
                selectedDataRow[SharedFunctions.GetResourceString("Other1ColumnName")] = typedRowDataTable.Rows[0][SharedFunctions.GetResourceString("OtherTextColumnName")].ToString();
            }

            selectedDataRow[SharedFunctions.GetResourceString("Other2ColumnName")] = string.Empty;
            selectedDataRow[SharedFunctions.GetResourceString("Min_ColumnName")] = typedRowDataTable.Rows[0][SharedFunctions.GetResourceString("Min_ColumnName")].ToString();
            selectedDataRow[SharedFunctions.GetResourceString("Max_ColumnName")] = typedRowDataTable.Rows[0][SharedFunctions.GetResourceString("Max_ColumnName")].ToString();
            selectedDataRow[SharedFunctions.GetResourceString("PercentageAllowedColumnName")] = typedRowDataTable.Rows[0][SharedFunctions.GetResourceString("PercentageAllowedColumnName")].ToString();
            selectedDataRow[SharedFunctions.GetResourceString("UnitsAllowedColumnName")] = typedRowDataTable.Rows[0][SharedFunctions.GetResourceString("UnitsAllowedColumnName")].ToString();
            selectedDataRow[SharedFunctions.GetResourceString("OtherMinColumnName")] = typedRowDataTable.Rows[0][SharedFunctions.GetResourceString("OtherMinColumnName")].ToString();
            selectedDataRow[SharedFunctions.GetResourceString("OtherMaxColumnName")] = typedRowDataTable.Rows[0][SharedFunctions.GetResourceString("OtherMaxColumnName")].ToString();
            selectedDataRow[SharedFunctions.GetResourceString("SectionTypeColumnName")] = this.sectionTypeValue.ToString();
            selectedDataRow["ComponentQualityID"] = string.Empty;
            selectedDataRow["ComponentUniqueID"] = string.Empty;

            int componentKey = 0;
            int componentsId = 0;
            int.TryParse(typedRowDataTable.Rows[0][SharedFunctions.GetResourceString("ComponentIDColumnName")].ToString(), out componentKey);
            int.TryParse(typedRowDataTable.Rows[0][SharedFunctions.GetResourceString("Components_IdColumnName")].ToString(), out componentsId);
            selectedDataRow[SharedFunctions.GetResourceString("ConstructionSystemID")] = this.GetConstructionSystemId(componentsId);

            if (this.selectedComponentCodeDataTable.Rows.Count > 0)
            {
                if (this.selectedComponentCodeDataTable.Rows.Count == activeRow)
                {
                    this.selectedComponentCodeDataTable.Rows.RemoveAt(activeRow - 1);
                    this.selectedComponentCodeDataTable.Rows.InsertAt(selectedDataRow, activeRow - 1);                    
                    this.SelectedComponentGridView.Rows[activeRow - 1].Cells[0].Activated = true;
                }
                else
                {
                    this.selectedComponentCodeDataTable.Rows.RemoveAt(activeRow);
                    this.selectedComponentCodeDataTable.Rows.InsertAt(selectedDataRow, activeRow);                    
                    this.SelectedComponentGridView.Rows[activeRow].Cells[0].Activated = true;
                }
            }

            this.SelectedComponentGridView.DataSource = this.selectedComponentCodeDataTable;
            this.executionRequied = true;
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

            if (this.componetSystemDataTable.Rows.Count > 0)
            {
                for (int count = 0; count < this.componetSystemDataTable.Rows.Count; count++)
                {
                    componentCodeRows = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("ConstructionSystemTableName")].Select(SharedFunctions.GetResourceString("ConstructionSystem_IdColumnName") + " = '" + this.componetSystemDataTable.Rows[count][SharedFunctions.GetResourceString("ConstructionSystem_IdColumnName")].ToString() + "'");

                    if (componentCodeRows.Length > 0)
                    {
                        foreach (DataRow row in componentCodeRows)
                        {
                            codeList.Add(row[5].ToString());
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
        /// Filters the component row.
        /// </summary>
        /// <param name="keyValue">The key value.</param>
        private void FilterComponentRow(int keyValue)
        {
            FilterCondition filterCondition = new FilterCondition();
            filterCondition.CompareValue = keyValue;
            this.SelectedComponentGridView.DisplayLayout.Bands[0].ColumnFilters[SharedFunctions.GetResourceString("SectionTypeColumnName")].FilterConditions.Clear();
            this.SelectedComponentGridView.DisplayLayout.Bands[0].ColumnFilters[SharedFunctions.GetResourceString("SectionTypeColumnName")].FilterConditions.Add(filterCondition);
        }

        /// <summary>
        /// Loads the edit component form.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="componentsId">The components id.</param>
        private void LoadEditComponentForm(int code, int componentsId)
        {
            string returnValue = string.Empty;
            DataRow tempdataRow = null;
            DataSet editDataSet = new DataSet();
            Form componentForm3601 = new Form();
            object[] optionalParameter;
            if (this.sectionTypeValue <= 2)
            {
                optionalParameter  = new object[] { code, componentsId, this.sectionTypeValue, this.commercialXml, this.editComponentHashTable };
            }
            else
            {
                optionalParameter = new object[] { code, componentsId, 2, this.commercialXml, this.editComponentHashTable };
            }
            componentForm3601 = this.form36001Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(3601, optionalParameter, this.form36001Control.WorkItem);

            if (componentForm3601 != null)
            {
                if (componentForm3601.ShowDialog() == DialogResult.OK)
                {
                    returnValue = TerraScanCommon.GetValue(componentForm3601, "EditEnterComponentsFormXmlValue");
                    StringReader stringXmlReader = new StringReader(returnValue);
                    XmlTextReader textReaderHouseType = new XmlTextReader(stringXmlReader);
                    editDataSet.ReadXml(textReaderHouseType);

                    if (editDataSet != null && editDataSet.Tables.Count >= 0)
                    {                        
                        tempdataRow = this.selectedComponentCodeDataTable.NewRow();
                        tempdataRow[SharedFunctions.GetResourceString("SystemCodeColumnName")] = editDataSet.Tables[0].Rows[0][SharedFunctions.GetResourceString("SystemCodeColumnName")].ToString();
                        tempdataRow[SharedFunctions.GetResourceString("SelectedSystemColumnName")] = editDataSet.Tables[0].Rows[0][SharedFunctions.GetResourceString("SelectedSystemColumnName")].ToString();
                        tempdataRow[SharedFunctions.GetResourceString("ComponentColumnName")] = editDataSet.Tables[0].Rows[0][SharedFunctions.GetResourceString("ComponentColumnName")].ToString();
                        tempdataRow[SharedFunctions.GetResourceString("F36000UnitsColumnName")] = editDataSet.Tables[0].Rows[0][SharedFunctions.GetResourceString("F36000UnitsColumnName")].ToString();
                        tempdataRow[SharedFunctions.GetResourceString("F36000PercentageColumnName")] = editDataSet.Tables[0].Rows[0][SharedFunctions.GetResourceString("F36000PercentageColumnName")].ToString();
                        tempdataRow[SharedFunctions.GetResourceString("Other1ColumnName")] = editDataSet.Tables[0].Rows[0][SharedFunctions.GetResourceString("Other1ColumnName")].ToString();

                        double tempOther2 = 0.0;
                        double.TryParse(editDataSet.Tables[0].Rows[0][SharedFunctions.GetResourceString("Other2ColumnName")].ToString(), out tempOther2);
                        if (tempOther2 > 0.0)
                        {
                            tempdataRow[SharedFunctions.GetResourceString("Other2ColumnName")] = tempOther2.ToString("##0.00");
                        }
                        else
                        {
                            tempdataRow[SharedFunctions.GetResourceString("Other2ColumnName")] = string.Empty;
                        }

                        tempdataRow[SharedFunctions.GetResourceString("Min_ColumnName")] = editDataSet.Tables[0].Rows[0][SharedFunctions.GetResourceString("Min_ColumnName")].ToString();
                        tempdataRow[SharedFunctions.GetResourceString("Max_ColumnName")] = editDataSet.Tables[0].Rows[0][SharedFunctions.GetResourceString("Max_ColumnName")].ToString();
                        tempdataRow[SharedFunctions.GetResourceString("PercentageAllowedColumnName")] = editDataSet.Tables[0].Rows[0][SharedFunctions.GetResourceString("PercentageAllowedColumnName")].ToString();
                        tempdataRow[SharedFunctions.GetResourceString("UnitsAllowedColumnName")] = editDataSet.Tables[0].Rows[0][SharedFunctions.GetResourceString("UnitsAllowedColumnName")].ToString();
                        tempdataRow[SharedFunctions.GetResourceString("OtherMinColumnName")] = editDataSet.Tables[0].Rows[0][SharedFunctions.GetResourceString("OtherMinColumnName")].ToString();
                        tempdataRow[SharedFunctions.GetResourceString("OtherMaxColumnName")] = editDataSet.Tables[0].Rows[0][SharedFunctions.GetResourceString("OtherMaxColumnName")].ToString();
                        tempdataRow[SharedFunctions.GetResourceString("SectionTypeColumnName")] = this.sectionTypeValue.ToString();

                        double tempRank = 0.0;
                        double.TryParse(editDataSet.Tables[0].Rows[0]["ComponentQualityID"].ToString(), out tempRank);
                        if (tempRank > 0.0)
                        {                            
                            tempdataRow["ComponentQualityID"] = tempRank.ToString("##0.0");
                        }
                        else
                        {
                            tempdataRow["ComponentQualityID"] = string.Empty;
                        }

                        tempdataRow["ConstructionSystemID"] = editDataSet.Tables[0].Rows[0]["ConstructionSystemID"].ToString();
                        tempdataRow["ComponentUniqueID"] = this.componentUniqueId.ToString();
                    }

                    if (this.selectedComponentCodeDataTable.Rows.Count > 0)
                    {
                        int activeRow = 0;
                        if (this.SelectedComponentGridView.ActiveRow != null)
                        {
                            activeRow = this.SelectedComponentGridView.ActiveRow.Index;
                        }

                        if (this.editComponent)
                        {
                            this.selectedComponentCodeDataTable.Rows.RemoveAt(activeRow);
                            this.selectedComponentCodeDataTable.Rows.InsertAt(tempdataRow, activeRow);
                            this.SelectedComponentGridView.Rows[activeRow].Activate();
                            this.editComponent = false;
                        }
                        else
                        {
                            this.selectedComponentCodeDataTable.Rows.InsertAt(tempdataRow, this.selectedComponentCodeDataTable.Rows.Count);
                            this.SelectedComponentGridView.Rows[this.selectedComponentCodeDataTable.Rows.Count - 1].Activate();
                        }
                    }
                    else
                    {
                        this.selectedComponentCodeDataTable.Rows.Add(tempdataRow);
                        this.SelectedComponentGridView.DataSource = this.selectedComponentCodeDataTable;
                    }
                }
            }
            /*
            if (this.selectedComponentCodeDataTable.Rows.Count > 0)
            {
                // this.xmlCollectionDataSet.GetOccupancy.Rows.Clear();
                this.xmlCollectionDataSet.GetComponent.Merge(selectedComponentCodeDataTable);
            }*/
            this.editComponentHashTable.Clear();
            this.ToEnableEditButtonInMasterForm();
        }

        /// <summary>
        /// Changes the color of the row back.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void ChangeBackColorComponentRow(int rowIndex)
        {
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("SystemCodeColumnName")].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("SelectedSystemColumnName")].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("ComponentColumnName")].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("F36000UnitsColumnName")].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("F36000UnitsColumnName")].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("F36000PercentageColumnName")].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("F36000PercentageColumnName")].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("ComponentQualityIDColumnName")].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("ComponentQualityIDColumnName")].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("Other1ColumnName")].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("Other1ColumnName")].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("Other2ColumnName")].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("Other2ColumnName")].Appearance.BackColorDisabled = Color.FromArgb(0, 0, 128);

            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("SystemCodeColumnName")].Appearance.ForeColor = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("SelectedSystemColumnName")].Appearance.ForeColorDisabled = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("ComponentColumnName")].Appearance.ForeColorDisabled = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("F36000UnitsColumnName")].Appearance.ForeColor = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("F36000UnitsColumnName")].Appearance.ForeColorDisabled = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("F36000PercentageColumnName")].Appearance.ForeColor = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("F36000PercentageColumnName")].Appearance.ForeColorDisabled = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("ComponentQualityID")].Appearance.ForeColor = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("ComponentQualityID")].Appearance.ForeColorDisabled = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("Other1ColumnName")].Appearance.ForeColor = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("Other1ColumnName")].Appearance.ForeColorDisabled = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("Other2ColumnName")].Appearance.ForeColor = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("Other2ColumnName")].Appearance.ForeColorDisabled = Color.White;
        }

        /// <summary>
        /// Restores the color of the row back.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void RestoreBackColorComponentRow(int rowIndex)
        {
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("SystemCodeColumnName")].Appearance.BackColor = Color.White;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("SelectedSystemColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("ComponentColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);

            if (this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("F36000UnitsColumnName")].Activation.ToString() == "AllowEdit")
            {
                this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("F36000UnitsColumnName")].Appearance.BackColor = Color.White;
            }
            else
            {
                this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("F36000UnitsColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);
            }

            if (this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("F36000PercentageColumnName")].Activation.ToString() == "AllowEdit")
            {
                this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("F36000PercentageColumnName")].Appearance.BackColor = Color.White;
            }
            else
            {
                this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("F36000PercentageColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);
            }

            if (this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("ComponentQualityID")].Activation.ToString() == "AllowEdit")
            {
                this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("ComponentQualityID")].Appearance.BackColor = Color.White;
            }
            else
            {
                this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("ComponentQualityID")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);
            }

            if (this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("Other2ColumnName")].Activation.ToString() == "AllowEdit")
            {
                this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("Other2ColumnName")].Appearance.BackColor = Color.White;
            }
            else
            {
                this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("Other2ColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);
            }

            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("Other1ColumnName")].Appearance.BackColorDisabled = Color.FromArgb(226, 226, 226);

            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("SystemCodeColumnName")].Appearance.ForeColor = Color.Black;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("SelectedSystemColumnName")].Appearance.ForeColorDisabled = Color.Black;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("ComponentColumnName")].Appearance.ForeColorDisabled = Color.Black;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("F36000UnitsColumnName")].Appearance.ForeColor = Color.Black;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("F36000UnitsColumnName")].Appearance.ForeColorDisabled = Color.Black;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("F36000PercentageColumnName")].Appearance.ForeColor = Color.Black;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("F36000PercentageColumnName")].Appearance.ForeColorDisabled = Color.Black;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("ComponentQualityID")].Appearance.ForeColor = Color.Black;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("ComponentQualityID")].Appearance.ForeColorDisabled = Color.Black;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("Other1ColumnName")].Appearance.ForeColorDisabled = Color.Black;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("Other2ColumnName")].Appearance.ForeColor = Color.Black;
            this.SelectedComponentGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("Other2ColumnName")].Appearance.ForeColorDisabled = Color.Black;
        }

        /// <summary>
        /// Customizes all component grid.
        /// </summary>
        private void CustomizeAllComponentGrid()
        {
            this.CustomizeComponentSystemGrid();
            this.CustomizeComponentCodeGrid();
            this.LoadSelectedComponentColumnName();
        }

        /// <summary>
        /// Customizes the component system grid.
        /// </summary>
        private void CustomizeComponentSystemGrid()
        {
            this.ComponentSystemGridView.AllowUserToResizeColumns = false;
            this.ComponentSystemGridView.AutoGenerateColumns = false;
            this.ComponentSystemGridView.AllowUserToResizeRows = false;
            this.ComponentSystemGridView.StandardTab = true;
            this.ComponentSystemGridView.Columns["SystemName"].DataPropertyName = SharedFunctions.GetResourceString("NameColumnName");
            this.ComponentSystemGridView.Columns[SharedFunctions.GetResourceString("ConstructionSystem_IdColumnName")].DataPropertyName = SharedFunctions.GetResourceString("ConstructionSystem_IdColumnName");
            this.ComponentSystemGridView.Columns[SharedFunctions.GetResourceString("ConstructionSystemIDColumnName")].DataPropertyName = SharedFunctions.GetResourceString("ConstructionSystemIDColumnName");
        }

        /// <summary>
        /// Customizes the component code grid.
        /// </summary>
        private void CustomizeComponentCodeGrid()
        {
            this.ComponentCodeGridView.AllowUserToResizeColumns = false;
            this.ComponentCodeGridView.AutoGenerateColumns = false;
            this.ComponentCodeGridView.AllowUserToResizeRows = false;
            this.ComponentCodeGridView.StandardTab = true;
            this.ComponentCodeGridView.Columns[SharedFunctions.GetResourceString("ComponentIDColumnName")].DataPropertyName = SharedFunctions.GetResourceString("ComponentIDColumnName");
            this.ComponentCodeGridView.Columns["ComponentName"].DataPropertyName = SharedFunctions.GetResourceString("NameColumnName");
            this.ComponentCodeGridView.Columns[SharedFunctions.GetResourceString("Min_ColumnName")].DataPropertyName = SharedFunctions.GetResourceString("Min_ColumnName");
            this.ComponentCodeGridView.Columns[SharedFunctions.GetResourceString("Max_ColumnName")].DataPropertyName = SharedFunctions.GetResourceString("Max_ColumnName");
            this.ComponentCodeGridView.Columns[SharedFunctions.GetResourceString("PercentageAllowedColumnName")].DataPropertyName = SharedFunctions.GetResourceString("PercentageAllowedColumnName");
            this.ComponentCodeGridView.Columns[SharedFunctions.GetResourceString("UnitsAllowedColumnName")].DataPropertyName = SharedFunctions.GetResourceString("UnitsAllowedColumnName");
            this.ComponentCodeGridView.Columns[SharedFunctions.GetResourceString("OtherMinColumnName")].DataPropertyName = SharedFunctions.GetResourceString("OtherMinColumnName");
            this.ComponentCodeGridView.Columns[SharedFunctions.GetResourceString("OtherMaxColumnName")].DataPropertyName = SharedFunctions.GetResourceString("OtherMaxColumnName");
            this.ComponentCodeGridView.Columns[SharedFunctions.GetResourceString("Components_IdColumnName")].DataPropertyName = SharedFunctions.GetResourceString("Components_IdColumnName");
        }

        /// <summary>
        /// Loads the name of the selected component column.
        /// </summary>
        private void LoadSelectedComponentColumnName()
        {
            if (this.selectedComponentCodeDataTable.Rows.Count == 0)
            {
                DataColumn[] selectedComponentColumn = new DataColumn[] { new DataColumn(SharedFunctions.GetResourceString("SystemCodeColumnName")), new DataColumn(SharedFunctions.GetResourceString("SelectedSystemColumnName")), new DataColumn(SharedFunctions.GetResourceString("ComponentColumnName")), new DataColumn(SharedFunctions.GetResourceString("F36000UnitsColumnName")), new DataColumn(SharedFunctions.GetResourceString("F36000PercentageColumnName")), new DataColumn(SharedFunctions.GetResourceString("Other1ColumnName")), new DataColumn(SharedFunctions.GetResourceString("Other2ColumnName")), new DataColumn(SharedFunctions.GetResourceString("Min_ColumnName")), new DataColumn(SharedFunctions.GetResourceString("Max_ColumnName")), new DataColumn(SharedFunctions.GetResourceString("PercentageAllowedColumnName")), new DataColumn(SharedFunctions.GetResourceString("UnitsAllowedColumnName")), new DataColumn(SharedFunctions.GetResourceString("OtherMinColumnName")), new DataColumn(SharedFunctions.GetResourceString("OtherMaxColumnName")), new DataColumn(SharedFunctions.GetResourceString("SectionTypeColumnName")), new DataColumn(SharedFunctions.GetResourceString("ComponentQualityID")), new DataColumn("ConstructionSystemID"), new DataColumn("ComponentUniqueID") };
                this.selectedComponentCodeDataTable.Columns.AddRange(selectedComponentColumn);
                this.SelectedComponentGridView.DataSource = this.selectedComponentCodeDataTable;
            }
        }

        /// <summary>
        /// Loads the component tab.
        /// </summary>
        private void LoadComponentTab()
        {
            if (this.commercialHtcXmlDataSet.Tables.Count > 0)
            {
                if (this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("ConstructionSystemTableName")] != null)
                {
                    int sectionTypeId = 0;

                    // To get SectionType_ID
                    DataRow[] sectionTypeRow;// = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].NewRow();
                    //DataRow sectionTypeRow = null;
                    DataTable sectionTypeDataTable = new DataTable();
                    
                   // this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].AcceptChanges();
                    if (this.sectionTypeValue <= 2)
                    {
                        sectionTypeRow = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Select("Key = '" + this.sectionTypeValue +"'");
                    }
                    else
                    {
                        sectionTypeRow = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Select("Key = '2'");
                    }

                    sectionTypeDataTable = this.DataRowToDataTable(sectionTypeRow);

                    int.TryParse(sectionTypeDataTable.Rows[0]["SectionType_Id"].ToString(), out sectionTypeId);

                        // To get Systems
                    DataRow[] systemDataRow;
                    systemDataRow = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("ConstructionSystemTableName")].Select(SharedFunctions.GetResourceString("ConstructionSystems_IdColumnName") + " = '" + sectionTypeId + "'");
                    this.componetSystemDataTable = this.DataRowToDataTable(systemDataRow);

                    this.ComponentSystemGridView.DataSource = this.componetSystemDataTable;

                    if (this.ComponentSystemGridView.OriginalRowCount > this.ComponentSystemGridView.NumRowsVisible)
                    {
                        this.SystemComponentScrollBar.Visible = false;
                    }
                    else
                    {
                        this.SystemComponentScrollBar.Visible = true;
                    }
                    
                }
            }
        }

        /// <summary>
        /// Components the system grid.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void ComponentSystemGridClick(int rowIndex)
        {
            if (this.ComponentSystemGridView.OriginalRowCount > 0)
            {
                if (rowIndex >= 0)
                {
                    int nameId = 0;
                    int.TryParse(this.ComponentSystemGridView.Rows[rowIndex].Cells[SharedFunctions.GetResourceString("ConstructionSystem_IdColumnName")].Value.ToString(), out nameId);
                    this.LoadComponetCodeGrid(nameId);
                }
            }
        }

        /// <summary>
        /// Loads the componet code grid.
        /// </summary>
        /// <param name="componentCode">The component code.</param>
        private void LoadComponetCodeGrid(int componentCode)
        {
            int componentId = 0;

            // To get ComponetID
            DataRow[] constructionSystemDataRow;
            DataTable constructionSystemdataTable = new DataTable();
            constructionSystemDataRow = this.componetSystemDataTable.Select(SharedFunctions.GetResourceString("ConstructionSystem_IdColumnName") + " = '" + componentCode + "'");
            constructionSystemdataTable = this.DataRowToDataTable(constructionSystemDataRow);
            int.TryParse(constructionSystemdataTable.Rows[0][SharedFunctions.GetResourceString("ConstructionSystem_IdColumnName")].ToString(), out componentId);

            // To get ComponentCode datatable
            DataRow[] componentCodeDataRow;
            componentCodeDataRow = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Select(SharedFunctions.GetResourceString("Components_IdColumnName") + " = '" + componentId + "'");
            this.componentCodeDataTable = this.DataRowToDataTable(componentCodeDataRow);

            this.ComponentCodeGridView.DataSource = this.componentCodeDataTable;

            if (this.ComponentCodeGridView.OriginalRowCount > this.ComponentCodeGridView.NumRowsVisible)
            {
                this.ComponentScrollBar.Visible = false;
            }
            else
            {
                this.ComponentScrollBar.Visible = true;
            }
        }

        #endregion Components              

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
            double.TryParse(this.totalRcnValue, out rcnValue);

            if (rcnValue > 0.0)
            {
                depreciationAmount = (rcnValue * (this.deprPercentage / 100)) * (-1);

                this.DepreciationAmountTextBox.Text = depreciationAmount.ToString();
                this.CalculateDepreciationTextBox.Text = depreciationAmount.ToString();

                rcnLess = rcnValue + depreciationAmount;
                this.RcnLessDeprTextBox.Text = rcnLess.ToString();
                this.DepreciationCalculateRcnTextBox.Text = this.totalRcnValue;

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
                int.TryParse(this.xmlCollectionDataSet.GetMarshallSwiftCommercial.Rows[0][this.xmlCollectionDataSet.GetMarshallSwiftCommercial.RollYearColumn.ColumnName].ToString(), out this.rollYear);

                if (!string.IsNullOrEmpty(this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.IsByObjectColumn.ColumnName].ToString()))
                {
                    this.ByObjectCheckBox.Checked = Convert.ToBoolean(this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.IsByObjectColumn.ColumnName].ToString());
                }
                ///
                int selectedItem = 0;
                int.TryParse(this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.DeprTableIDColumn.ColumnName].ToString(), out selectedItem);
                this.DeprTableComboBox.SelectedValue = selectedItem;
                ///

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

                //int selectedItem = 0;
                //int.TryParse(this.xmlCollectionDataSet.ListDeprValueDataTable.Rows[0][this.xmlCollectionDataSet.ListDeprValueDataTable.DeprTableIDColumn.ColumnName].ToString(), out selectedItem);
                //this.DeprTableComboBox.SelectedValue = selectedItem;
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
                int.TryParse(this.xmlCollectionDataSet.GetMarshallSwiftCommercial.Rows[0][this.xmlCollectionDataSet.GetMarshallSwiftCommercial.RollYearColumn.ColumnName].ToString(), out this.rollYear);
                this.EffectiveAgeTextBox.Text = this.xmlCollectionDataSet.GetMarshallSwiftCommercial.Rows[0][this.xmlCollectionDataSet.GetMarshallSwiftCommercial.EffectiveAgeColumn.ColumnName].ToString();

                if (!string.IsNullOrEmpty(this.xmlCollectionDataSet.GetMarshallSwiftCommercial.Rows[0][this.xmlCollectionDataSet.GetMarshallSwiftCommercial.ObjectConditionColumn.ColumnName].ToString()))
                {
                    this.ObjectConditionTextBox.Text = this.xmlCollectionDataSet.GetMarshallSwiftCommercial.Rows[0][this.xmlCollectionDataSet.GetMarshallSwiftCommercial.ObjectConditionColumn.ColumnName].ToString();
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
                double.TryParse(this.form36001Control.WorkItem.GetDeprPercentage(this.age, this.condition, this.deprTableId).ToString(), out this.deprPercentage);
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
                double.TryParse(this.form36001Control.WorkItem.GetDeprPercentage(this.age, this.condition, this.deprTableId).ToString(), out this.deprPercentage);
                this.DepreciationPercentageTextBox.Text = this.deprPercentage.ToString() + "%";
                this.CalculateDepreciationPercentageLabel.Text = this.deprPercentage.ToString() + "%";
            }
            else
            {
                this.DepreciationPercentageTextBox.Text = "0.00%";
                this.CalculateDepreciationPercentageLabel.Text = "0.00%";
            }
        }

        #endregion

        /// <summary>
        /// Handles the Click event of the AddButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////Add new section in ComboBox
                string sectionName = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows[this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows.Count - 1]["Description"].ToString();
                int sectionId = Convert.ToInt32(sectionName.Substring(7)) + 1;
                int sectionKeyId = Convert.ToInt32(this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows[this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows.Count - 1]["Key"].ToString()) + 1;
                int sectionTypeId = Convert.ToInt32(this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows[this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows.Count - 1]["SectionType_Id"].ToString()) + 1;
                sectionName = "Section" + " " + sectionId;
                DataRow newRow = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].NewRow();
                newRow["Key"] = sectionKeyId;
                newRow["Description"] = sectionName;
                newRow["SectionType_Id"] = sectionTypeId;
                newRow["Sections_Id"] = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows[this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows.Count - 1]["Sections_Id"].ToString();

                this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows.Add(newRow);
                this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].AcceptChanges();
                this.SectionTypeComponentCombo.DataSource = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")];
                this.SectionTypeComponentCombo.DisplayMember = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Columns[SharedFunctions.GetResourceString("DescriptionColumnName")].ColumnName;
                this.SectionTypeComponentCombo.ValueMember = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Columns[SharedFunctions.GetResourceString("KeyColumnName")].ColumnName;
                
                this.SectionTypeComponentCombo.SelectedValue = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows[this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows.Count - 1]["Key"];
                ////Repopulate Form values
                F36001MarshalAndSwiftCommercialData.GetEstimateRow estimateRow = (F36001MarshalAndSwiftCommercialData.GetEstimateRow)this.xmlCollectionDataSet.GetEstimate.NewRow();
                estimateRow["SectionID"] = sectionKeyId;
                estimateRow["StorySection"] = "1";
                estimateRow["StoryShape"] = "2.00";
                estimateRow["BasementShape"] = "2.00";
                estimateRow["BasementLevel"] = "1.00";
                this.xmlCollectionDataSet.GetEstimate.AddGetEstimateRow(estimateRow);
                //this.xmlCollectionDataSet.GetEstimate.Rows.Add
                this.sectionTypeValue = sectionKeyId;
                int.TryParse(this.SectionTypeComponentCombo.SelectedValue.ToString(), out this.sectionTypeValue);
                this.LoadComponentTab();
                this.FilterComponentRow(this.sectionTypeValue);
                this.LoadDataToTextBox();
              /*  if (this.xmlCollectionDataSet != null)
                {
                    if (this.xmlCollectionDataSet.Tables.Count > 0)
                    {
                        // Get Component Values
                        this.selectedComponentCodeDataTable = this.xmlCollectionDataSet.GetComponent;
                        this.SelectedComponentGridView.DataSource = this.selectedComponentCodeDataTable;

                        // Get Occupancy Values
                        this.selectedOccupancyCodeDataTable = this.xmlCollectionDataSet.GetOccupancy;
                        this.SelectedOccupancyGrid.DataSource = this.selectedOccupancyCodeDataTable;
                    }
                }*/
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Methods

        /// <summary>
        /// Handles the Click event of the RemoveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.sectionTypeValue > 2)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this section?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {

                        ////Remove from Component Grid
                        for (int compCount = 0; compCount < this.selectedComponentCodeDataTable.Rows.Count; compCount++)
                        {
                            if (this.selectedComponentCodeDataTable.Rows[compCount]["SectionType"].ToString() == this.sectionTypeValue.ToString())
                            {
                                this.selectedComponentCodeDataTable.Rows.RemoveAt(compCount);
                                this.selectedComponentCodeDataTable.AcceptChanges();
                            }
                        }

                        ////Remove from Occupancy Grid
                        for (int occupancyCount = 0; occupancyCount < this.selectedOccupancyCodeDataTable.Rows.Count; occupancyCount++)
                        {
                            if (this.selectedOccupancyCodeDataTable.Rows[occupancyCount]["SectionType"].ToString() == this.sectionTypeValue.ToString())
                            {
                                this.selectedOccupancyCodeDataTable.Rows.RemoveAt(occupancyCount);
                                this.selectedOccupancyCodeDataTable.AcceptChanges();
                            }
                        }

                        ////Remove from Estimate Table
                        for (int estimateCount = 0; estimateCount < this.xmlCollectionDataSet.GetEstimate.Rows.Count; estimateCount++)
                        {
                            if (this.xmlCollectionDataSet.GetEstimate.Rows[estimateCount]["SectionID"].ToString() == this.sectionTypeValue.ToString())
                            {
                                this.xmlCollectionDataSet.GetEstimate.Rows.RemoveAt(estimateCount);
                                this.xmlCollectionDataSet.GetEstimate.AcceptChanges();
                            }
                        }

                        ////Remove Sections from ComboBox
                        for (int i = 0; i < this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows.Count; i++)
                        {
                            if (this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows[i]["Key"].ToString() == this.sectionTypeValue.ToString())
                            {
                                this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows.RemoveAt(i);
                                this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].AcceptChanges();
                                this.SectionTypeComponentCombo.SelectedValue = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows[this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows.Count - 1]["Key"];
                                break;
                            }
                        }
                        this.sectionTypeValue = Convert.ToInt32(this.SectionTypeComponentCombo.SelectedValue);
                        this.LoadComponentTab();
                        this.FilterComponentRow(this.sectionTypeValue);
                        this.LoadDataToTextBox();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the section combo value.
        /// </summary>
        private void SetSectionComboValue()
        {
            if (this.xmlCollectionDataSet.GetEstimate.Rows.Count > 1)
            {
                for (int i = 1; i < this.xmlCollectionDataSet.GetEstimate.Rows.Count; i++)
                {
                    string sectionName = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows[i]["Description"].ToString();
                    int sectionKeyId = Convert.ToInt32(this.xmlCollectionDataSet.GetEstimate.Rows[i]["SectionID"].ToString());
                    int sectionId = sectionKeyId - 1;
                    int sectionTypeId = Convert.ToInt32(this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows[this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows.Count - 1]["SectionType_Id"].ToString()) + 1;
                    sectionName = "Section" + " " + sectionId;
                    DataRow newRow = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].NewRow();
                    newRow["Key"] = sectionKeyId;
                    newRow["Description"] = sectionName;
                    newRow["SectionType_Id"] = sectionTypeId;
                    newRow["Sections_Id"] = this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows[i]["Sections_Id"].ToString();
                    this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Rows.Add(newRow);
                    this.commercialHtcXmlDataSet.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].AcceptChanges();
                }
            }
        }

        /// <summary>
        /// Sets the TextBox values to EstimateTable based on the Section Type.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="columnValue">The column value.</param>
        private void SetSectionToTable(string columnName, string columnValue, string sectionType)
        {
            int estimateRow = 0;
            for (int i = 0; i < this.xmlCollectionDataSet.GetEstimate.Rows.Count; i++)
            {
                if (this.xmlCollectionDataSet.GetEstimate.Rows[i]["SectionID"].ToString().Trim() == sectionType)
                {
                    estimateRow = i;
                    break;
                }
            }
            this.xmlCollectionDataSet.GetEstimate.Rows[estimateRow][columnName] = columnValue;
            this.xmlCollectionDataSet.GetEstimate.AcceptChanges();
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
                   //this.D9030_F9030_CancelSliceInformation(this, new DataEventArgs<int>(this.valueSliceId));
                    this.escKeyPressed = true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion region Esc key Functionality
    }
}
