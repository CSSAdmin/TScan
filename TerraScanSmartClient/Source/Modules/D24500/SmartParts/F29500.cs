//--------------------------------------------------------------------------------------------
// <copyright file="F29500.cs" company="Congruent">
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
// 15 Sep 07		KARTHIKEYAN V	    Created
// 10 Jan 13        Purushotham A       Modified 
// 29 April 13      Purushotham A       Reverted Parcel situs CO TFS#19555
// 07 Jun 13        Purushotham A       Modified to Implement TFS# 19664,19665 and 19666
// 20160712         Priyadharshini      Modified to Implement TFS# 21784
// 20170224         Dhinesh            TSCO - D24500.F29500 Split Parcels form - Display error message when DOR / Class Code field is blank
//************************************************************************************************/

namespace D24500
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;    
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
    using System.Data.SqlClient;
    using Infragistics.Win;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    /// <summary>
    /// F29500
    /// </summary>
    public partial class F29500 : BaseSmartPart
    {
        #region Variable

        /// <summary>
        /// An object for Dataset - F29500ParcelSplitData
        /// </summary>
        private F29500ParcelSplitData parcelSplitDataSet = new F29500ParcelSplitData();

        /// <summary>
        /// An object for Dataset - F29500Controller
        /// </summary>
        private F29500Controller form29500Control = new F29500Controller();

        /// <summary>
        /// foreColor
        /// </summary>
        private Color foreColor = Color.Black;

        /// <summary>
        /// parcelNumberSplitDataSet
        /// </summary>
        private DataSet parcelNumberSplitDataSet;

        /// <summary>
        /// splitParcelBand
        /// </summary>
        private UltraGridBand splitParcelBand;

        /// <summary>
        /// splitParcelGroup
        /// </summary>
        private UltraGridGroup splitParcelGroup = new UltraGridGroup();
       private  Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = null;
       private Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = null;

        /// <summary>
        /// parcelNumber
        /// </summary>
        private int parcelNumber;

        /// <summary>
        /// currentParcel
        /// </summary>
        private int currentParcel;

        /// <summary>
        /// baseParcelId
        /// </summary>
        private int baseParcelId;

        /// <summary>
        /// splitId
        /// </summary>
        private int splitId;

        /// <summary>
        /// eventId
        /// </summary>
        private int eventId;

        /// <summary>
        /// sliceHeight
        /// </summary>
        private int sliceHeight;

        private int splitedParcelCount = 0;

        /// <summary>
        /// splitParcelHeaderDataTable
        /// </summary>
        //// private DataTable splitParcelHeaderDataTable = new DataTable();

        /// <summary>
        /// baseParcelDataSet
        /// </summary>
        private DataSet baseParcelDataSet;

        /// <summary>
        /// navigationClicked
        /// </summary>
        private bool navigationClicked;

        /// <summary>
        /// splitProcessed
        /// </summary>
        private bool splitProcessed;

        /// <summary>
        /// splitPanelScrolled
        /// </summary>
        private bool splitPanelScrolled;

        /// <summary>
        /// basePanelScrolled
        /// </summary>
        private bool basePanelScrolled;

        /// <summary>
        /// parcelSplited
        /// </summary>
        private bool parcelSplited;

        /// <summary>
        /// formLoad
        /// </summary>
        private bool formLoad;

        /// <summary>
        /// cellValueChanged
        /// </summary>
        private bool cellValueChanged;

        /// <summary>
        /// lockStatus
        /// </summary>
        private string lockStatus;

        /// <summary>
        /// lockedBy
        /// </summary>
        private string lockedBy;

        /// <summary>
        /// lockBool
        /// </summary>
        private bool lockBool;

        /// <summary>
        /// lockedDate
        /// </summary>
        private string lockedDate;

        /// <summary>
        /// refreshConfirmed
        /// </summary>
        private bool refreshConfirmed;

        /// <summary>
        /// valueXml
        /// </summary>
        private string valueXml;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// componentAssumptionsDataSet
        /// </summary>
        private DataSet componentAssumptionsDataSet = new DataSet();

        private int editedRecord;

        /// <summary>
        /// splitParcelHeaderDataTable
        /// </summary>
        private F29500ParcelSplitData.ListSplitHeaderDetailDataTable splitParcelHeaderDataTable = new F29500ParcelSplitData.ListSplitHeaderDetailDataTable();
        private F26000ParcelHeaderFormData.f26000ClassCodeDataTable classCodeDataTable = new F26000ParcelHeaderFormData.f26000ClassCodeDataTable();

        F2550TaxRollCorrectionData.ConfiguredStateDataTable StateConfiguredDetailTable = new F2550TaxRollCorrectionData.ConfiguredStateDataTable();
        /// <summary>
        /// isAcceptpressed
        /// </summary>
        private bool acceptpressed;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;
        private string tempString = string.Empty;
        private string tempStr;

        private string stateConfigured = string.Empty;

        private int templength = 0;
        private string tempClassCode;
        private int classCodeConfigValue;
        private string classCode;


        #region Form Slice Variables

        /// <summary>
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        /// <summary>
        ///  Local variable for Red
        /// </summary>
        private int redColor;

        /// <summary>
        ///  Local variable for Green.
        /// </summary>
        private int greenColor;

        /// <summary>
        ///  Local variable for Blue.
        /// </summary>
        private int blueColor;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        #endregion Form Slice Variables

        /// <summary>
        /// IsStatus
        /// </summary>
        private bool IsStatus;

        #endregion

        #region Construtor

        /// <summary>
        /// F29500
        /// </summary>
        public F29500()
        {
            InitializeComponent();
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
        public F29500(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            /* Modified to Implement TFS# 21784 Fixed */
            this.ClassCodeComboBox.FlatStyle = FlatStyle.Popup;
            this.ClassCodeComboBox.DropDown += new EventHandler(ClassCodeComboBox_DropDown);
            this.ClassCodeComboBox.DropDownClosed += new EventHandler(ClassCodeComboBox_DropDownClosed);
            /*end*/
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.formMasterPermissionEdit = permissionEdit;
            this.eventId = keyID;
            this.ParcelSplitPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ParcelSplitPictureBox.Height, this.ParcelSplitPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);

        }

        /// <summary>
        /// ClassCodeComboBox_DropDownClosed <see cref="T:F84721"/> class.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">e.</param>
        public void ClassCodeComboBox_DropDownClosed(object sender, EventArgs e)
        {
            this.ClassCodeComboBox.ForeColor = foreColor;
        }

        /// <summary>
        /// ClassCodeComboBox_DropDownClosed <see cref="T:F84721"/> class.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">e.</param>
        public void ClassCodeComboBox_DropDown(object sender, EventArgs e)
        {
            this.ClassCodeComboBox.ForeColor = Color.Black;
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// Event publication to alert the resizable slices
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F95005_FomrMasterCancel, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> D9030_F95005_FomrMasterCancel;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

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
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Occurs when [D9030_ F9030_ set parcel lock properties].
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_SetParcelLockProperties, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<Parcellockimplimentation>> D9030_F9030_SetParcelLockProperties;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;
        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the mortgage import template controll.
        /// </summary>
        /// <value>The mortgage import template controll.</value>
        [CreateNew]
        public F29500Controller Form29500Controll
        {
            get { return this.form29500Control as F29500Controller; }
            set { this.form29500Control = value; }
        }

        #endregion

        #region Regular Expression

        /// <summary>
        /// Determines whether the specified value is integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value is integer; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInteger(string value)
        {
            return IsMatch(value, @"^([0-9]*|[0-9]*(\.[0-9])[0-9]*)$");
        }

        #endregion Regular Expression

        #region Event Subscription

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

                if (this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows.Count > 0)
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
        }

        /// <summary>
        /// Event Subscription for cancel slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
        {
            if (this.lockBool == false)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.Cursor = Cursors.WaitCursor;
                this.ClearForm();
                this.FormLoad();
                this.Cursor = Cursors.Default;
                if (this.eventId > 0)
                {
                    if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit || (this.PermissionFiled.editPermission && !this.formMasterPermissionEdit))
                    {
                        this.ControlLock(false);
                    }
                    else
                    {
                        this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                    }
                }
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
            if (this.lockBool == false)
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
            this.refreshConfirmed = true;
            if (this.lockBool == false)
            {
                if (this.slicePermissionField.editPermission)
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (this.baseParcelDataSet != null)
                    {
                        this.baseParcelDataSet.AcceptChanges();
                    }

                    if (this.parcelSplitDataSet != null)
                    {
                        this.parcelSplitDataSet.AcceptChanges();
                    }

                    if (this.splitParcelHeaderDataTable != null)
                    {
                        this.splitParcelHeaderDataTable.AcceptChanges();
                        if (!string.IsNullOrEmpty(this.stateConfigured) && this.stateConfigured.ToUpper().Equals("NE"))
                        {
                            if (this.splitParcelHeaderDataTable.Rows.Count > 0)
                            {                               
                                for (int i = 0; i < this.splitParcelHeaderDataTable.Rows.Count; i++)
                                {
                                    if (string.IsNullOrEmpty(this.splitParcelHeaderDataTable.Rows[i]["DOR"].ToString()))
                                    {
                                        this.splitParcelHeaderDataTable.Rows[i]["DOR"] = "««  »»";
                                    }
                                    if (!string.IsNullOrEmpty(this.splitParcelHeaderDataTable.Rows[i]["ClassCode"].ToString()))
                                    {
                                        var customize = this.splitParcelHeaderDataTable.Rows[i]["ClassCode"].ToString().Replace(" ", "");
                                        this.splitParcelHeaderDataTable.Rows[i]["ClassCode"] = customize.ToString();
                                    }
                                }
                            }
                            this.splitParcelHeaderDataTable.AcceptChanges();
                        }
                    }

                    if (this.parcelNumberSplitDataSet != null)
                    {
                        this.parcelNumberSplitDataSet.AcceptChanges();
                    }

                    int parcelSplitText = 0;
                    int.TryParse(this.ParcelSplitTextBox.Text.ToString().Trim(), out parcelSplitText);

                    if (this.parcelSplited)
                    {

                        for (int i = 0; i < this.parcelNumberSplitDataSet.Tables[0].Columns.Count; i++)
                        {
                            if (this.parcelNumberSplitDataSet.Tables[0].Columns[i].ColumnName.Substring(0, 7) == "ObjectS")
                            {
                                for (int j = 0; j < this.parcelNumberSplitDataSet.Tables[0].Rows.Count; j++)
                                {
                                    this.parcelNumberSplitDataSet.Tables[0].Rows[j][i] = null;

                                }
                            }
                        }

                        for (int i = 0; i < this.parcelNumberSplitDataSet.Tables[1].Columns.Count; i++)
                        {
                            if (this.parcelNumberSplitDataSet.Tables[1].Columns[i].ColumnName.Substring(0, 7) == "VSStrin")
                            {
                                for (int j = 0; j < this.parcelNumberSplitDataSet.Tables[1].Rows.Count; j++)
                                {
                                    this.parcelNumberSplitDataSet.Tables[1].Rows[j][i] = null;

                                }
                            }
                        }
                       this.splitId = this.form29500Control.WorkItem.SaveParcelSplit(this.GetSplitDefinitionXml(), TerraScanCommon.GetXmlString(this.splitParcelHeaderDataTable), TerraScanCommon.GetXmlString(this.parcelNumberSplitDataSet.Tables[1]), TerraScanCommon.GetXmlString(this.parcelNumberSplitDataSet.Tables[0]), TerraScanCommon.GetXmlString(this.parcelNumberSplitDataSet.Tables[2]), TerraScanCommon.UserId);
                            // this.parcelNumber = this.splitedParcelCount;

                        this.ProcessButton.Enabled = true;
                    }
                    else
                    {
                        this.splitId = this.form29500Control.WorkItem.SaveParcelSplit(this.GetSplitDefinitionXml(), null, null, null, null, TerraScanCommon.UserId);
                        this.ProcessButton.Enabled = false;
                    }

                    this.refreshConfirmed = false;
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = this.eventId;
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.Cursor = Cursors.Default;
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
            try
            {
                if (this.refreshConfirmed == false)
                {
                    if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                    {
                        this.eventId = eventArgs.Data.SelectedKeyId;
                        this.ClearForm();
                        this.StateAndClassCodeDetails();
                        this.FormLoad();
                        this.lockBool = false;

                        this.valueXml = this.form29500Control.WorkItem.ListRecordLockStatus(Convert.ToInt32(this.Tag), this.baseParcelId).ToString();
                        StringReader stringXmlReader = new StringReader(this.valueXml);
                        System.Xml.XmlTextReader textReaderHouseType1 = new System.Xml.XmlTextReader(stringXmlReader);
                        this.componentAssumptionsDataSet.Clear();
                        this.componentAssumptionsDataSet.ReadXml(textReaderHouseType1);
                        if (this.componentAssumptionsDataSet.Tables[0].Rows.Count > 0)
                        {
                            this.lockStatus = this.componentAssumptionsDataSet.Tables[0].Rows[0]["LockStatus"].ToString();
                            this.lockedBy = this.componentAssumptionsDataSet.Tables[0].Rows[0]["LockedBy"].ToString();
                            this.lockedDate = this.componentAssumptionsDataSet.Tables[0].Rows[0]["LockedDate"].ToString();
                        }

                        if (this.lockStatus == "True")
                        {
                            this.lockBool = true;
                        }
                        else
                        {
                            this.lockBool = false;
                        }

                        Parcellockimplimentation parcellock = new Parcellockimplimentation();
                        if (this.lockBool == true)
                        {
                            parcellock.MasterFormNo = this.masterFormNo;
                            parcellock.SaveButtonEnable = true;
                            parcellock.SaveButtonBackColor = Color.Red;
                            parcellock.SaveButtonText = "Locked";
                            parcellock.SaveButtonForeColor = Color.White;
                            parcellock.CancelButtonEnable = false;
                            StringBuilder ownerAddress = new StringBuilder();

                            ownerAddress.Append("Locked By:" + " " + this.lockedBy);

                            ownerAddress.Append(SharedFunctions.GetResourceString("NexLine"));

                            ownerAddress.Append("Locked On:" + " " + this.lockedDate);

                            parcellock.SaveButtonTooltipText = ownerAddress.ToString();
                            //// parcellock.SaveButtonEnable = false;
                        }
                        else
                        {
                            ////ParcelStatusSaveButton.Enabled = false;
                            parcellock.SaveButtonText = "Save";
                            parcellock.SaveButtonBackColor = Color.FromArgb(28, 81, 128);
                            parcellock.SaveButtonForeColor = Color.White;
                            parcellock.CancelButtonEnable = false;
                            parcellock.MasterFormNo = this.masterFormNo;
                        }

                        this.D9030_F9030_SetParcelLockProperties(this, new DataEventArgs<Parcellockimplimentation>(parcellock));

                        if (this.eventId > 0)
                        {
                            if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit || (this.PermissionFiled.editPermission && !this.formMasterPermissionEdit))
                            {
                                this.ControlLock(false);
                            }
                            else
                            {
                                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                            }
                        }

                        this.BackwardParcelButton.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

            this.refreshConfirmed = false;
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

        #endregion Event Subscription

        #region Protected Methods

        /// <summary>
        /// Called when [form slice_ resize].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_Resize(DataEventArgs<SliceResize> eventArgs)
        {
            if (this.FormSlice_Resize != null)
            {
                this.FormSlice_Resize(this, eventArgs);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ alert resizable slice].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F95005_AlertFomrMasterCancel(DataEventArgs<int> eventArgs)
        {
            if (this.D9030_F95005_FomrMasterCancel != null)
            {
                this.D9030_F95005_FomrMasterCancel(this, eventArgs);
            }
        }

        #endregion Protected Methods

        #region Regular Expression

        /// <summary>
        /// Determines whether the specified value is match.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value is match; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsMatch(string value, string pattern)
        {
            System.Text.RegularExpressions.Regex objRegex = new System.Text.RegularExpressions.Regex(@pattern);
            if (objRegex.IsMatch(value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Events



        private void StateAndClassCodeDetails()
        {
            this.StateConfiguredDetailTable = this.form29500Control.WorkItem.F2550_GetConfiguredState().ConfiguredState;
            if (this.StateConfiguredDetailTable.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.StateConfiguredDetailTable.Rows[0][this.StateConfiguredDetailTable.StateColumn].ToString()))
                {
                    this.stateConfigured = this.StateConfiguredDetailTable.Rows[0][this.StateConfiguredDetailTable.StateColumn].ToString();
                }
                if (!string.IsNullOrEmpty(this.StateConfiguredDetailTable.Rows[0][this.StateConfiguredDetailTable.AutoCompleteValueColumn].ToString()))
                {
                    this.classCodeConfigValue = Convert.ToInt16(this.StateConfiguredDetailTable.Rows[0][this.StateConfiguredDetailTable.AutoCompleteValueColumn].ToString());
                }
            }
        }
        #region Common Events

        /// <summary>
        /// Handles the Load event of the F29500 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        ///
        private void F29500_Load(object sender, EventArgs e)
        {
            try
            {
                this.refreshConfirmed = false;
                this.Cursor = Cursors.WaitCursor;
                this.StateAndClassCodeDetails();
                this.FormLoad();

                this.lockBool = false;

                this.valueXml = this.form29500Control.WorkItem.ListRecordLockStatus(Convert.ToInt32(this.Tag), this.baseParcelId).ToString();
                StringReader stringXmlReader = new StringReader(this.valueXml);
                System.Xml.XmlTextReader textReaderHouseType1 = new System.Xml.XmlTextReader(stringXmlReader);
                this.componentAssumptionsDataSet.Clear();
                this.componentAssumptionsDataSet.ReadXml(textReaderHouseType1);
                if (this.componentAssumptionsDataSet.Tables[0].Rows.Count > 0)
                {
                    this.lockStatus = this.componentAssumptionsDataSet.Tables[0].Rows[0]["LockStatus"].ToString();
                    this.lockedBy = this.componentAssumptionsDataSet.Tables[0].Rows[0]["LockedBy"].ToString();
                    this.lockedDate = this.componentAssumptionsDataSet.Tables[0].Rows[0]["LockedDate"].ToString();
                }

                if (this.lockStatus == "True")
                {
                    this.lockBool = true;
                }
                else
                {
                    this.lockBool = false;
                }

                Parcellockimplimentation parcellock = new Parcellockimplimentation();
                if (this.lockBool == true)
                {
                    parcellock.MasterFormNo = this.masterFormNo;
                    parcellock.SaveButtonEnable = true;
                    parcellock.SaveButtonBackColor = Color.Red;
                    parcellock.SaveButtonText = "Locked";
                    parcellock.SaveButtonForeColor = Color.White;
                    parcellock.CancelButtonEnable = false;

                    StringBuilder ownerAddress = new StringBuilder();

                    ownerAddress.Append("Locked By:" + " " + this.lockedBy);

                    ownerAddress.Append(SharedFunctions.GetResourceString("NexLine"));

                    ownerAddress.Append("Locked On:" + " " + this.lockedDate);

                    parcellock.SaveButtonTooltipText = ownerAddress.ToString();
                    //// parcellock.SaveButtonEnable = false;
                }
                else
                {
                    parcellock.MasterFormNo = this.masterFormNo;
                    ////ParcelStatusSaveButton.Enabled = false;
                    parcellock.SaveButtonBackColor = Color.FromArgb(28, 81, 128);
                    parcellock.SaveButtonForeColor = Color.White;
                    parcellock.SaveButtonText = "Save";
                    parcellock.CancelButtonEnable = false;
                }

                this.D9030_F9030_SetParcelLockProperties(this, new DataEventArgs<Parcellockimplimentation>(parcellock));

                #region BugId 5959
                if (this.eventId > 0)
                {
                    if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit || (this.PermissionFiled.editPermission && !this.formMasterPermissionEdit))
                    {
                        this.ControlLock(false);
                    }
                    else
                    {
                        this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                    }
                }

                #endregion BugId 5959

                this.BackwardParcelButton.Enabled = false;
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Handles the Click event of the SetButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SetButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int.TryParse(this.ParcelSplitTextBox.Text.Trim(), out this.parcelNumber);

                if (this.parcelNumber > 0 && this.parcelNumber <= 200)
                {
                    this.baseParcelDataSet = new DataSet();
                    this.baseParcelDataSet = this.form29500Control.WorkItem.GetBaseParcelValue(this.baseParcelId);
                    this.baseParcelDataSet.Relations.Add(this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListParcelSplitObject.TableName].Columns[this.parcelSplitDataSet.ListParcelSplitObject.ObjectIDColumn.ColumnName], this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListParcelSplitValueSlices.TableName].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.ObjectIDColumn.ColumnName]);
                    this.baseParcelDataSet.Relations.Add(this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListParcelSplitValueSlices.TableName].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.ValueSliceIDColumn.ColumnName], this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListParcelSplitCrop.TableName].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.ValueSliceIDColumn.ColumnName]);
                    this.BaseParcelGrid.DataSource = this.baseParcelDataSet;
                    this.CreateDynamicParcelSplit();
                    this.SetControlOnFormLoad();
                    this.parcelSplited = true;
                    this.CheckDorCodeLink();
                    this.ParcelNumberLabel.Focus();
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("ParcelRange"), SharedFunctions.GetResourceString("ParcelSplitHeader"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ParcelSplitTextBox.Focus();
                }

                this.EditEnabled();
                Parcellockimplimentation parcellock = new Parcellockimplimentation();
                if (this.lockBool == true)
                {
                    parcellock.MasterFormNo = this.masterFormNo;
                    parcellock.SaveButtonEnable = true;
                    parcellock.SaveButtonBackColor = Color.Red;
                    parcellock.SaveButtonText = "Locked";
                    parcellock.SaveButtonForeColor = Color.White;
                    parcellock.CancelButtonEnable = false;
                    StringBuilder ownerAddress = new StringBuilder();

                    ownerAddress.Append("Locked By:" + " " + this.lockedBy);

                    ownerAddress.Append(SharedFunctions.GetResourceString("NexLine"));

                    ownerAddress.Append("Locked On:" + " " + this.lockedDate);

                    parcellock.SaveButtonTooltipText = ownerAddress.ToString();
                    ////  parcellock.SaveButtonEnable = false;
                }
                else
                {
                    ////ParcelStatusSaveButton.Enabled = false;
                    // parcellock.SaveButtonText = "Save";
                    // parcellock.SaveButtonForeColor = Color.Gray;
                    // parcellock.CancelButtonEnable = false;
                    // parcellock.MasterFormNo = this.masterFormNo;
                }
                this.splitedParcelCount = this.parcelNumber;
                this.D9030_F9030_SetParcelLockProperties(this, new DataEventArgs<Parcellockimplimentation>(parcellock));
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Handles the Click event of the ClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(SharedFunctions.GetResourceString("ClearForm"), SharedFunctions.GetResourceString("ClearFormHeader"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.ClearForm();
                    Parcellockimplimentation parcellock = new Parcellockimplimentation();
                    if (this.lockBool == true)
                    {
                        parcellock.MasterFormNo = this.masterFormNo;
                        parcellock.SaveButtonEnable = true;
                        parcellock.SaveButtonBackColor = Color.Red;
                        parcellock.SaveButtonText = "Locked";
                        parcellock.SaveButtonForeColor = Color.White;
                        parcellock.CancelButtonEnable = false;
                        StringBuilder ownerAddress = new StringBuilder();

                        ownerAddress.Append("Locked By:" + " " + this.lockedBy);

                        ownerAddress.Append(SharedFunctions.GetResourceString("NexLine"));

                        ownerAddress.Append("Locked On:" + " " + this.lockedDate);

                        parcellock.SaveButtonTooltipText = ownerAddress.ToString();
                        //// parcellock.SaveButtonEnable = false;
                    }
                    else
                    {
                        ////ParcelStatusSaveButton.Enabled = false;
                        parcellock.SaveButtonText = "Save";
                        parcellock.SaveButtonBackColor = Color.FromArgb(28, 81, 128);
                        parcellock.SaveButtonForeColor = Color.White;
                        parcellock.CancelButtonEnable = false;
                        parcellock.MasterFormNo = this.masterFormNo;
                    }
                    /*Modified to Implement TFS# 21784 Fixed*/
                    this.ClassCodeComboBox.ForeColor = Color.Black;
                    foreColor = Color.Black;
                    /*end*/
                    this.D9030_F9030_SetParcelLockProperties(this, new DataEventArgs<Parcellockimplimentation>(parcellock));
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
        /// Handles the Click event of the ProcessButton control.
        /// </summary>;
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ProcessButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.CheckDorCode())
                {
                    string returnMessage = this.form29500Control.WorkItem.F29500_CreateParcel(this.splitId, TerraScanCommon.UserId);
                    this.splitProcessed = true;
                    this.LockControls(false);
                    this.SetButton.Enabled = false;
                    this.ClearButton.Enabled = false;
                    this.ProcessButton.Enabled = false;

                    if (returnMessage != null && !string.IsNullOrEmpty(returnMessage.Trim()))
                    {
                        MessageBox.Show(returnMessage, SharedFunctions.GetResourceString("F29500SplitProcessHeader"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                   {
                        
                        ////To enable and disable Fields by purushotham.a

                        this.LegalLabelTextBox.Enabled = false;
                        if (this.editedRecord > 0)
                        {
                            this.situsLabelTextBox.Text = this.splitParcelHeaderDataTable.Rows[editedRecord - 1][this.parcelSplitDataSet.ListSplitHeaderDetail.SitusColumn.ColumnName].ToString();
                        }
                        this.SitusLinkLabel.Visible = false;
                        this.situsLabelTextBox.Visible = true;
                        this.situsLabelTextBox.Enabled = false;
                        this.Id2TextBox.Enabled = false;
                        this.ClassCodePanel.Enabled = false;
                        //this.pageMode = TerraScanCommon.PageModeTypes.View;

                        ////To refresh subtitles 
                        SliceReloadActiveRecord currentSliceInfo;
                        currentSliceInfo.MasterFormNo = this.masterFormNo;
                        currentSliceInfo.SelectedKeyId = this.eventId;
                        this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                        /////Modified message box after reload of subtitles by manoj.
                        MessageBox.Show(SharedFunctions.GetResourceString("ProcessSuccess"), SharedFunctions.GetResourceString("ParcelSplitHeader"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //if (this.situsLabelTextBox.Enabled == false)
                        //{
                        //    this.SitusLinkLabel.Text = this.situsLabelTextBox.Text;
                        //    this.situsLabelTextBox.ForeColor = Color.Black;
                        //    this.situsLabelTextBox.Visible = false;
                        //    this.SitusLinkLabel.Visible = true;
                        //}
                    }
                }
                else
                {
                    MessageBox.Show("This Split cannot be processed because some new Parcels have no DOR code defined.", "TerraScan T2 - Cannot Process Split", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Handles the Click event of the BackwardParcelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void BackwardParcelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.navigationClicked = true;
                this.splitParcelHeaderDataTable.AcceptChanges();
                this.GetSplitHeaderDetail(this.currentParcel);
                this.BingingSplitParcelHeader(this.currentParcel - 1);
                this.ParcelSplitGrid.DisplayLayout.Bands[0].Groups[this.currentParcel].Header.Appearance.BackColor = Color.FromArgb(22, 52, 92);
                this.ParcelSplitGrid.DisplayLayout.Bands[0].Groups[this.currentParcel - 1].Header.Appearance.BackColor = Color.FromArgb(119, 47, 40);
                this.ParcelSplitGrid.ActiveColScrollRegion.ScrollGroupIntoView(this.ParcelSplitGrid.DisplayLayout.Bands[0].Groups[this.currentParcel - 1]);
                this.currentParcel--;
                this.SplitParcelOrderTextBox.Text = Convert.ToString(this.currentParcel + 1);
                this.ShowParcelDetailLabel.Text = SharedFunctions.GetResourceString("ShowingParcelNumber") + Convert.ToString(this.currentParcel + 1) + ":";
                this.CheckDorCodeLink();

                if (this.currentParcel == 0)
                {
                    this.ForwardParcelButton.Enabled = true;
                    this.BackwardParcelButton.Enabled = false;
                    this.ShowParcelDetailLabel.Focus();
                }
                else
                {
                    this.ForwardParcelButton.Enabled = true;
                    this.BackwardParcelButton.Enabled = true;
                }

                this.navigationClicked = false;
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
        /// Handles the Click event of the ForwardParcelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ForwardParcelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.navigationClicked = true;
                this.splitParcelHeaderDataTable.AcceptChanges();
                this.GetSplitHeaderDetail(this.currentParcel);
                this.BingingSplitParcelHeader(this.currentParcel + 1);
                this.ParcelSplitGrid.DisplayLayout.Bands[0].Groups[this.currentParcel].Header.Appearance.BackColor = Color.FromArgb(22, 52, 92);
                this.ParcelSplitGrid.DisplayLayout.Bands[0].Groups[this.currentParcel + 1].Header.Appearance.BackColor = Color.FromArgb(119, 47, 40);
                this.ParcelSplitGrid.ActiveColScrollRegion.ScrollGroupIntoView(this.ParcelSplitGrid.DisplayLayout.Bands[0].Groups[this.currentParcel + 1]);
                this.currentParcel++;
                this.SplitParcelOrderTextBox.Text = Convert.ToString(this.currentParcel + 1);
                this.ShowParcelDetailLabel.Text = SharedFunctions.GetResourceString("ShowingParcelNumber") + Convert.ToString(this.currentParcel + 1) + ":";
                this.CheckDorCodeLink();

                if (this.parcelNumber == (this.currentParcel + 1))
                {
                    this.ForwardParcelButton.Enabled = false;
                    this.BackwardParcelButton.Enabled = true;
                    this.ShowParcelDetailLabel.Focus();
                }
                else
                {
                    this.ForwardParcelButton.Enabled = true;
                    this.BackwardParcelButton.Enabled = true;
                }

                this.navigationClicked = false;
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
        /// Handles the Enter event of the SplitParcelNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SplitParcelNumberTextBox_Enter(object sender, EventArgs e)
        {
            try
            {
                if (this.SplitParcelNumberTextBox.Text == "< Default >" && this.PermissionFiled.editPermission)
                {
                    this.SplitParcelNumberTextBox.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the SplitParcelNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SplitParcelNumberTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.SplitParcelNumberTextBox.Text.Trim()))
                {
                    this.SplitParcelNumberTextBox.Text = "< Default >";
                    this.SplitParcelNumberTextBox.ForeColor = Color.FromArgb(115, 115, 115);
                }
                else
                {
                    this.SplitParcelNumberTextBox.ForeColor = Color.Black;
                }

                this.GetSplitHeaderDetail(this.currentParcel);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the SplitDorCodeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SplitDorCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ////Ramya.D
                if (!string.IsNullOrEmpty(this.SplitDorCodeTextBox.Text.Trim()) && !this.acceptpressed)
                {
                    this.SplitDorCodeLinkLabel.Text = this.SplitDorCodeTextBox.Text;
                    this.SplitDorCodeTextBox.ForeColor = Color.Black;
                    this.SplitDorCodeTextBox.Visible = false;
                    this.SplitDorCodeLinkLabel.Visible = true;
                }
                else
                {
                    //TSCO - D24500.F29500 Split Parcels form - Display error message when DOR / Class Code field is blank

                    //this.SplitDorCodeLinkLabel.Text = string.Empty;
                    this.SplitDorCodeTextBox.Visible = true;
                    //this.SplitDorCodeLinkLabel.Visible = false;
                    this.SplitDorCodeTextBox.Select();
                    this.SplitDorCodeTextBox.Focus();
                    this.acceptpressed = false;
                }

                ////int dorCode = 0;
                ////int.TryParse(this.SplitDorCodeTextBox.Text.Trim(), out dorCode);
                ////if (dorCode > 0)
                ////{
                ////this.SplitDorCodeLinkLabel.Text = dorCode.ToString();
                ////this.SplitDorCodeTextBox.ForeColor = Color.Black;
                ////}
                ////else if (!string.IsNullOrEmpty(this.SplitDorCodeTextBox.Text))
                ////{
                ////    this.SplitDorCodeLinkLabel.Text = string.Empty;
                ////    this.SplitDorCodeTextBox.ForeColor = Color.FromArgb(115, 115, 115);
                ////    this.SplitDorCodeTextBox.Visible = true;
                ////    this.SplitDorCodeLinkLabel.Visible = false;
                ////}
                ////  this.SplitDorCodeTextBox.Text = dorCode.ToString(); 
                this.EditEnabled();
                this.valueXml = this.form29500Control.WorkItem.ListRecordLockStatus(Convert.ToInt32(this.Tag), this.baseParcelId).ToString();
                StringReader stringXmlReader = new StringReader(this.valueXml);
                System.Xml.XmlTextReader textReaderHouseType1 = new System.Xml.XmlTextReader(stringXmlReader);
                this.componentAssumptionsDataSet.Clear();
                this.componentAssumptionsDataSet.ReadXml(textReaderHouseType1);
                if (this.componentAssumptionsDataSet.Tables[0].Rows.Count > 0)
                {
                    this.lockStatus = this.componentAssumptionsDataSet.Tables[0].Rows[0]["LockStatus"].ToString();
                    this.lockedBy = this.componentAssumptionsDataSet.Tables[0].Rows[0]["LockedBy"].ToString();
                    this.lockedDate = this.componentAssumptionsDataSet.Tables[0].Rows[0]["LockedDate"].ToString();
                }

                if (this.lockStatus == "True")
                {
                    this.lockBool = true;
                }
                else
                {
                    this.lockBool = false;
                }

                /*Parcellockimplimentation parcellock = new Parcellockimplimentation();
                if (this.lockBool == true)
                {
                    parcellock.MasterFormNo = this.masterFormNo;
                    parcellock.SaveButtonEnable = true;
                    parcellock.SaveButtonBackColor = Color.Red;
                    parcellock.SaveButtonText = "Locked";
                    parcellock.SaveButtonForeColor = Color.White;
                    parcellock.CancelButtonEnable = false;
                    this.refreshRed = true;
                    StringBuilder ownerAddress = new StringBuilder();

                    ownerAddress.Append("Locked By:" + " " + this.lockedBy);

                    ownerAddress.Append(SharedFunctions.GetResourceString("NexLine"));

                    ownerAddress.Append("Locked On:" + " " + this.lockedDate);

                    parcellock.SaveButtonTooltipText = ownerAddress.ToString();
                    //// parcellock.SaveButtonEnable = false;
                }
                else
                {
                    parcellock.MasterFormNo = this.masterFormNo;
                    parcellock.SaveButtonEnable = true;
                    parcellock.SaveButtonBackColor = Color.FromArgb(28, 81, 128);
                    parcellock.SaveButtonForeColor = Color.White;
                    parcellock.SaveButtonText = "Save";
                    parcellock.CancelButtonEnable = true;
                }

                this.D9030_F9030_SetParcelLockProperties(this, new DataEventArgs<Parcellockimplimentation>(parcellock));*/
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the SplitDorCodeLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void SplitDorCodeLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Form statecode = new Form();
                object[] optionalParameters = new object[] { };
                statecode = this.form29500Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2010, optionalParameters, this.form29500Control.WorkItem);
                if (statecode != null)
                {
                    if (statecode.ShowDialog() == DialogResult.OK)
                    {
                        this.acceptpressed = true;

                        #region BugID 5002
                        //// this.SplitDorCodeLinkLabel.Text = TerraScanCommon.GetValue(statecode, "StateCodeReturnValue");
                        this.SplitDorCodeLinkLabel.Text = TerraScanCommon.GetValue(statecode, "StateCodeNameReturnValue");
                        this.SplitDorCodeTextBox.Text = this.SplitDorCodeLinkLabel.Text;
                        #endregion BugID 5002

                        this.splitParcelHeaderDataTable.Rows[this.currentParcel][this.parcelSplitDataSet.ListSplitHeaderDetail.DORColumn.ColumnName] = this.SplitDorCodeTextBox.Text;

                        this.splitParcelHeaderDataTable.AcceptChanges();
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
        /// Handles the Leave event of the SplitDorCodeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SplitDorCodeTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                #region BugID 5002

                if (!string.IsNullOrEmpty(this.SplitDorCodeTextBox.Text.Trim()))
                {
                    this.SplitDorCodeLinkLabel.Text = this.splitParcelHeaderDataTable.Rows[this.currentParcel][this.parcelSplitDataSet.ListSplitHeaderDetail.DORColumn.ColumnName].ToString();
                    this.SplitDorCodeTextBox.ForeColor = Color.Black;
                    this.SplitDorCodeTextBox.Visible = false;
                    this.SplitDorCodeLinkLabel.Visible = true;
                }
                else
                {
                    this.SplitDorCodeLinkLabel.Visible = true;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the ParcelSplitTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelSplitTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.lockBool == false)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ParcelSplitPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelSplitPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the ParcelSplitPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelSplitPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.ParcelSplitToolTip.SetToolTip(this.ParcelSplitPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Enter event of the SplitDorCodeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SplitDorCodeTextBox_Enter(object sender, EventArgs e)
        {
            try
            {
                if (this.SplitDorCodeTextBox.Text == "< Select >")
                {
                    this.SplitDorCodeTextBox.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// SplitId1TextBox_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SplitId1TextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.GetSplitHeaderDetail(this.currentParcel);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// SplitAttachmentCheckBox_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SplitAttachmentCheckBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.GetSplitHeaderDetail(this.currentParcel);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// SplitCommentsCheckBox_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SplitCommentsCheckBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.GetSplitHeaderDetail(this.currentParcel);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void SplitPermitCheckBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.GetSplitHeaderDetail(this.currentParcel);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void SplitAssocCheckBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.GetSplitHeaderDetail(this.currentParcel);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion Common Events

        #region BaseParcelGrid Events

        /// <summary>
        /// Handles the InitializeLayout event of the BaseParcelGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void BaseParcelGrid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                this.CustomizeGrid();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the InitializeRow event of the BaseParcelGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeRowEventArgs"/> instance containing the event data.</param>
        private void BaseParcelGrid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (e.Row != null)
                {
                    if (e.Row.Band.Index == 1)
                    {
                        bool parcelSplitValue = Convert.ToBoolean(e.Row.Cells[this.parcelSplitDataSet.ListParcelSplitValueSlices.IsValueColumn.ColumnName].Value);
                        bool parcelSplitRoll = Convert.ToBoolean(e.Row.Cells[this.parcelSplitDataSet.ListParcelSplitValueSlices.IsRollColumn.ColumnName].Value);

                        if (!parcelSplitValue)
                        {
                            e.Row.Cells[this.parcelSplitDataSet.ListParcelSplitValueSlices.ValueStringColumn.ColumnName].Appearance.BackColor = Color.FromArgb(77, 77, 77);
                            e.Row.Cells[this.parcelSplitDataSet.ListParcelSplitValueSlices.ValueStringColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 128, 128);
                        }

                        if (!parcelSplitRoll)
                        {
                            e.Row.Cells[this.parcelSplitDataSet.ListParcelSplitValueSlices.RollStringColumn.ColumnName].Appearance.BackColor = Color.FromArgb(77, 77, 77);
                            e.Row.Cells[this.parcelSplitDataSet.ListParcelSplitValueSlices.RollStringColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 128, 128);
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
        /// Handles the Enter event of the BaseParcelGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void BaseParcelGrid_Enter(object sender, EventArgs e)
        {
            try
            {
                this.basePanelScrolled = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseClick event of the BaseParcelGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void BaseParcelGrid_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.basePanelScrolled)
                {
                    if (this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                    {
                        ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(e.X, e.Y);
                        this.basePanelScrolled = false;
                    }
                }
                else
                {
                    this.basePanelScrolled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion BaseParcelGrid Events

        #region ParcelSplitGrid Events

        /// <summary>
        /// Handles the InitializeLayout event of the ParcelSplitGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void ParcelSplitGrid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                this.CustomizeParcelSplitGrid();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellChange event of the ParcelSplitGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.CellEventArgs"/> instance containing the event data.</param>
        private void ParcelSplitGrid_CellChange(object sender, CellEventArgs e)
       {
            try
            {
                this.cellValueChanged = true;
                UltraGridRow activeRow = this.ParcelSplitGrid.ActiveRow;
                UltraGridCell activeCell = this.ParcelSplitGrid.ActiveCell;
                this.EditEnabled();
                ////if condition added for check the band ////Latha
                if (e.Cell.Band.Index != 2)
                {
                    if (activeRow.ChildBands != null)
                    {
                        if (activeRow.ChildBands.HasChildRows && e.Cell.Band.Index != 1)
                        {
                            if (activeCell.EditorResolved.Value.ToString().ToLower().Equals("false"))
                            {
                                for (int childRowCount = 0; childRowCount < activeRow.ChildBands[0].Rows.Count; childRowCount++)
                                {
                                    //for checking and unchecking at band0
                                    activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 1].Value = false;
                                }
                            }
                            
                        }
                        else if (e.Cell.Band.Index == 1) /////Else portion has added for Enable/Disable editing in ParcelSplitGrid ////Latha
                        {
                            if (activeCell.EditorResolved.Value.ToString().ToLower().Equals("false"))
                            {
                                for (int childRowCount = 0; childRowCount < activeRow.ChildBands[0].Rows.Count; childRowCount++)
                                {
                                    //for checking and unchecking at band1
                                    if (activeCell.Column.ToString().Equals("Checked1"))
                                    {
                                        activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 1].Value = false;

                                        //commented to de activate the check box
                                        if (!activeRow.ChildBands[0].Rows[childRowCount].Cells["ComponentTypeID"].Value.ToString().Equals("3"))
                                        {
                                            activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 1].SetValue(DBNull.Value, false);
                                        }
                                        else
                                        {
                                            activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 1].Activation = Activation.Disabled;

                                        }
                                    }
                                    else
                                    {
                                        activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 3].Value = false;
                                        if (!activeRow.ChildBands[0].Rows[childRowCount].Cells["ComponentTypeID"].Value.ToString().Equals("3"))
                                        {
                                            activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 3].SetValue(DBNull.Value, false);
                                        }
                                        else
                                        {
                                            activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 3].Activation = Activation.Disabled;

                                        }
                                    }
                                    if (activeCell.Column.Index == 2)
                                    {
                                        activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 2].Activation = Activation.NoEdit;                                        
                                        activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 2].Value = "0";
                                    }
                                    else
                                    { 
                                        activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 4].Activation = Activation.NoEdit;
                                        activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 4].Value = "0";
                                    }
                                }
                            }
                            else
                            {
                                for (int childRowCount = 0; childRowCount < activeRow.ChildBands[0].Rows.Count; childRowCount++)
                                {
                                    //for checking and unchecking at band2
                                    //for checking and unchecking at band1
                                    if (activeCell.Column.ToString().Equals("Checked1"))
                                    {
                                        if (activeRow.ChildBands[0].Rows[childRowCount].Cells["ComponentTypeID"].Value.ToString().Equals("3"))
                                        {
                                            activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 1].Activation = Activation.AllowEdit;
                                            activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 1].Value = true;
                                        }
                                    }
                                    else
                                    {
                                        if (activeRow.ChildBands[0].Rows[childRowCount].Cells["ComponentTypeID"].Value.ToString().Equals("3"))
                                        {
                                            activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 3].Activation = Activation.AllowEdit;
                                            activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 3].Value = true;
                                        }
                                        //else
                                        //{
                                        //    activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 3].SetValue(DBNull.Value, false);
                                        //}

                                    }
                                    if (activeCell.Column.Index == 2)
                                    {
                                        if (activeRow.ChildBands[0].Rows[childRowCount].Cells["ComponentTypeID"].Value.ToString().Equals("3"))
                                        {
                                            activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 2].Activation = Activation.NoEdit;
                                            activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 4].Activation = Activation.AllowEdit;
                                        }
                                        else
                                        {
                                            activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 2].Activation = Activation.AllowEdit;
                                            activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 4].Activation = Activation.AllowEdit;
                                        }
                                    }
                                    else
                                    {
                                        if (activeRow.ChildBands[0].Rows[childRowCount].Cells["ComponentTypeID"].Value.ToString().Equals("3"))
                                        {
                                            activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 4].Activation = Activation.NoEdit;
                                        }
                                        else
                                        {
                                            activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 4].Activation = Activation.AllowEdit;
                                            activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 2].Activation = Activation.AllowEdit;
                                        }
                                    }
                                }
                            }
                        }

                        this.splitParcelHeaderDataTable.AcceptChanges();
                        this.GetSplitHeaderDetail(this.currentParcel);
                        if (e.Cell.Band.Index != 1)
                        {
                            this.BingingSplitParcelHeader(activeCell.Column.Header.Group.Index);

                            this.ParcelSplitGrid.DisplayLayout.Bands[0].Groups[this.currentParcel].Header.Appearance.BackColor = Color.FromArgb(22, 52, 92);
                            this.ParcelSplitGrid.DisplayLayout.Bands[0].Groups[activeCell.Column.Header.Group.Index].Header.Appearance.BackColor = Color.FromArgb(119, 47, 40);

                            this.currentParcel = activeCell.Column.Header.Group.Index;
                        }

                        this.SplitParcelOrderTextBox.Text = Convert.ToString(this.currentParcel + 1);
                        this.ShowParcelDetailLabel.Text = SharedFunctions.GetResourceString("ShowingParcelNumber") + Convert.ToString(this.currentParcel + 1) + ":";
                    }
                    else
                    {
                        activeRow.ParentRow.Cells[activeCell.Column.Index - 1].Value = true;

                        int groupId = 0;
                        int.TryParse(activeCell.Column.Header.Caption.Substring(7, activeCell.Column.Header.Caption.Length - 7), out groupId);

                        this.splitParcelHeaderDataTable.AcceptChanges();
                        this.GetSplitHeaderDetail(this.currentParcel);
                        this.BingingSplitParcelHeader(groupId - 1);
                        this.ParcelSplitGrid.DisplayLayout.Bands[0].Groups[this.currentParcel].Header.Appearance.BackColor = Color.FromArgb(22, 52, 92);
                        this.ParcelSplitGrid.DisplayLayout.Bands[0].Groups[groupId - 1].Header.Appearance.BackColor = Color.FromArgb(119, 47, 40);
                        this.currentParcel = groupId - 1;
                        this.SplitParcelOrderTextBox.Text = Convert.ToString(this.currentParcel + 1);
                        this.ShowParcelDetailLabel.Text = SharedFunctions.GetResourceString("ShowingParcelNumber") + Convert.ToString(this.currentParcel + 1) + ":";
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(e.Cell.Value.ToString()) && IsInteger(e.Cell.Value.ToString()))
                    {
                        Double total = 0;
                        int splitCount = 1;
                        string usage = string.Empty;
                        for (int i = 0; i < activeRow.Cells.Count; i++)
                        {
                            if (activeRow.Cells[i].Column.ToString() == "VSString" + splitCount)
                            {
                                if (!string.IsNullOrEmpty(activeRow.Cells[i].Text))
                                {
                                    total = total + Convert.ToDouble(activeRow.Cells[i].Text);
                                }

                                splitCount = splitCount + 1;
                            }
                        }

                        for (int objectCount = 0; objectCount < this.BaseParcelGrid.Rows.Count; objectCount++)
                        {
                            if (this.BaseParcelGrid.Rows[objectCount].ChildBands.Count > 0)
                            {
                                for (int valueCount = 0; valueCount < this.BaseParcelGrid.Rows[objectCount].ChildBands[0].Rows.Count; valueCount++)
                                {
                                    if (this.BaseParcelGrid.Rows[objectCount].ChildBands[0].Rows[valueCount].ChildBands.Count > 0)
                                    {
                                        for (int cropCount = 0; cropCount < this.BaseParcelGrid.Rows[objectCount].ChildBands[0].Rows[valueCount].ChildBands[0].Rows.Count; cropCount++)
                                        {
                                            if (this.BaseParcelGrid.Rows[objectCount].ChildBands[0].Rows[valueCount].ChildBands[0].Rows[cropCount].Cells["ValueSliceID"].Text == activeRow.Cells["ValueSliceID"].Text && this.BaseParcelGrid.Rows[objectCount].ChildBands[0].Rows[valueCount].ChildBands[0].Rows[cropCount].Cells["ValueSliceID"].Row.Index == activeRow.Index)
                                            {
                                                usage = this.BaseParcelGrid.Rows[objectCount].ChildBands[0].Rows[valueCount].ChildBands[0].Rows[cropCount].Cells["VSString"].Text;
                                                if (!string.IsNullOrEmpty(usage))
                                                {
                                                    string usageValue = usage.Substring(usage.IndexOf("of"));
                                                    this.BaseParcelGrid.Rows[objectCount].ChildBands[0].Rows[valueCount].ChildBands[0].Rows[cropCount].Cells["VSString"].Value = total.ToString() + " " + usageValue;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
              
                if (this.PermissionFiled.editPermission)
                {
                    this.NavigationButtonStatus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeCellCancelUpdate event of the ParcelSplitGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.CancelableCellEventArgs"/> instance containing the event data.</param>
        private void ParcelSplitGrid_BeforeCellCancelUpdate(object sender, CancelableCellEventArgs e)
        {
            try
            {
                this.OnD9030_F95005_AlertFomrMasterCancel(new DataEventArgs<int>(this.masterFormNo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseUp event of the ParcelSplitGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void ParcelSplitGrid_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (!this.cellValueChanged && this.PermissionFiled.editPermission)
                {
                    this.navigationClicked = true;
                    Infragistics.Win.UIElement elementPoint = this.ParcelSplitGrid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));

                    if (elementPoint != null)
                    {
                        UltraGridColumn activeColumn = (UltraGridColumn)elementPoint.GetContext(typeof(UltraGridColumn));
                        GroupHeader activeHeader = (GroupHeader)elementPoint.GetContext(typeof(GroupHeader));
                        UltraGridRow activeRow = (UltraGridRow)elementPoint.GetContext(typeof(UltraGridRow));
                        UltraGridCell activeCell = (UltraGridCell)elementPoint.GetContext(typeof(UltraGridCell));

                        if (activeHeader != null && activeRow != null && activeRow.Band.Index != 1)
                        {
                            //// this.ParcelSplitGrid.Rows[activeHeader.VisiblePosition].Cells[0].Selected = true;
                            this.splitParcelHeaderDataTable.AcceptChanges();
                            this.GetSplitHeaderDetail(this.currentParcel);
                            this.BingingSplitParcelHeader(activeHeader.VisiblePosition);
                            this.ParcelSplitGrid.DisplayLayout.Bands[0].Groups[this.currentParcel].Header.Appearance.BackColor = Color.FromArgb(22, 52, 92);
                            this.ParcelSplitGrid.DisplayLayout.Bands[0].Groups[activeHeader.VisiblePosition].Header.Appearance.BackColor = Color.FromArgb(119, 47, 40);
                            this.currentParcel = activeHeader.VisiblePosition;
                            this.SplitParcelOrderTextBox.Text = Convert.ToString(this.currentParcel + 1);
                            this.ShowParcelDetailLabel.Text = SharedFunctions.GetResourceString("ShowingParcelNumber") + Convert.ToString(this.currentParcel + 1) + ":";
                            if (this.PermissionFiled.editPermission)
                            {
                                this.NavigationButtonStatus();
                            }

                            return;
                        }

                        if (activeRow != null && activeRow.Band.Index != 1)
                        {
                            if (activeRow.ChildBands != null)
                            {
                                if (activeCell != null)
                                {
                                    this.splitParcelHeaderDataTable.AcceptChanges();
                                    this.GetSplitHeaderDetail(this.currentParcel);
                                    this.BingingSplitParcelHeader(activeCell.Column.Header.Group.Index);
                                    this.ParcelSplitGrid.DisplayLayout.Bands[0].Groups[this.currentParcel].Header.Appearance.BackColor = Color.FromArgb(22, 52, 92);
                                    this.ParcelSplitGrid.DisplayLayout.Bands[0].Groups[activeCell.Column.Header.Group.Index].Header.Appearance.BackColor = Color.FromArgb(119, 47, 40);
                                    this.currentParcel = activeCell.Column.Header.Group.Index;
                                    this.SplitParcelOrderTextBox.Text = Convert.ToString(this.currentParcel + 1);
                                    this.ShowParcelDetailLabel.Text = SharedFunctions.GetResourceString("ShowingParcelNumber") + Convert.ToString(this.currentParcel + 1) + ":";

                                    // BugId 5959 -- Ramya.D
                                    if (this.PermissionFiled.editPermission)
                                    {
                                        this.NavigationButtonStatus();
                                    }
                                }
                            }
                            else
                            {
                                if (activeColumn != null)
                                {
                                    int groupId = 0;
                                    //// int.TryParse(activeCell.Column.Header.Caption.Substring(7, activeCell.Column.Header.Caption.Length - 7), out groupId);
                                    int.TryParse(activeColumn.Key.Substring(activeColumn.Key.Length - 1), out groupId);

                                    if (groupId > 0)
                                    {
                                        this.splitParcelHeaderDataTable.AcceptChanges();
                                        this.GetSplitHeaderDetail(this.currentParcel);
                                        this.BingingSplitParcelHeader(groupId - 1);
                                        this.ParcelSplitGrid.DisplayLayout.Bands[0].Groups[this.currentParcel].Header.Appearance.BackColor = Color.FromArgb(22, 52, 92);
                                        this.ParcelSplitGrid.DisplayLayout.Bands[0].Groups[groupId - 1].Header.Appearance.BackColor = Color.FromArgb(119, 47, 40);
                                        this.currentParcel = groupId - 1;
                                        this.SplitParcelOrderTextBox.Text = Convert.ToString(this.currentParcel + 1);
                                        this.ShowParcelDetailLabel.Text = SharedFunctions.GetResourceString("ShowingParcelNumber") + Convert.ToString(this.currentParcel + 1) + ":";

                                        // BugId 5959 -- Ramya.D
                                        if (this.PermissionFiled.editPermission)
                                        {
                                            this.NavigationButtonStatus();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    this.cellValueChanged = false;
                }

                this.navigationClicked = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Enter event of the ParcelSplitGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelSplitGrid_Enter(object sender, EventArgs e)
        {
            try
            {
                this.splitPanelScrolled = true;
                UltraGridCell activeCell = this.ParcelSplitGrid.ActiveCell;

                if (activeCell != null)
                {
                    activeCell.Selected = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseClick event of the ParcelSplitGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void ParcelSplitGrid_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.splitPanelScrolled)
                {
                    if (this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                    {
                        ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(e.X, e.Y + 50);
                        this.splitPanelScrolled = false;
                    }
                }
                else
                {
                    this.splitPanelScrolled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeColRegionScroll event of the ParcelSplitGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.BeforeColRegionScrollEventArgs"/> instance containing the event data.</param>
        private void ParcelSplitGrid_BeforeColRegionScroll(object sender, BeforeColRegionScrollEventArgs e)
        {
            try
            {
                if (this.splitPanelScrolled)
                {
                    if (this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                    {
                        ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.ParcelSplitGrid.Height);
                        this.splitPanelScrolled = false;
                    }
                }
                else
                {
                    this.splitPanelScrolled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the InitializeRow event of the ParcelSplitGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeRowEventArgs"/> instance containing the event data.</param>
        private void ParcelSplitGrid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            try
            {
                if (e.Row.Band.Index == 2)
                {
                    Double total = 0;
                    int splitCount = 1;
                    string usage = string.Empty;
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        if (e.Row.Cells[i].Column.ToString() == "VSString" + splitCount)
                        {
                            if (!string.IsNullOrEmpty(e.Row.Cells[i].Text))
                            {
                                total = total + Convert.ToDouble(e.Row.Cells[i].Text);
                            }

                            splitCount = splitCount + 1;
                        }
                    }

                    for (int objectCount = 0; objectCount < this.BaseParcelGrid.Rows.Count; objectCount++)
                    {
                        if (this.BaseParcelGrid.Rows[objectCount].ChildBands.Count > 0)
                        {
                            for (int valueCount = 0; valueCount < this.BaseParcelGrid.Rows[objectCount].ChildBands[0].Rows.Count; valueCount++)
                            {
                                if (this.BaseParcelGrid.Rows[objectCount].ChildBands[0].Rows[valueCount].ChildBands.Count > 0)
                                {
                                    for (int cropCount = 0; cropCount < this.BaseParcelGrid.Rows[objectCount].ChildBands[0].Rows[valueCount].ChildBands[0].Rows.Count; cropCount++)
                                    {
                                        if (this.BaseParcelGrid.Rows[objectCount].ChildBands[0].Rows[valueCount].ChildBands[0].Rows[cropCount].Cells["ValueSliceID"].Text == e.Row.Cells["ValueSliceID"].Text && this.BaseParcelGrid.Rows[objectCount].ChildBands[0].Rows[valueCount].ChildBands[0].Rows[cropCount].Cells["ValueSliceID"].Row.Index == e.Row.Index)
                                        {
                                            usage = this.BaseParcelGrid.Rows[objectCount].ChildBands[0].Rows[valueCount].ChildBands[0].Rows[cropCount].Cells["VSString"].Text;
                                            if (!string.IsNullOrEmpty(usage))
                                            {
                                                string usageValue = usage.Substring(usage.IndexOf("of"));
                                                this.BaseParcelGrid.Rows[objectCount].ChildBands[0].Rows[valueCount].ChildBands[0].Rows[cropCount].Cells["VSString"].Value = total.ToString() + " " + usageValue;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    splitCount = 1;
                    if (e.Row.Band.Index == 2) /////Else portion has added for Enable/Disable editing in ParcelSplitGrid ////Latha
                    {
                        for (int i = 0; i < e.Row.Cells.Count - 1; i++)
                        {
                            if (e.Row.ParentRow.Cells.Count > i && e.Row.ParentRow.Cells[i].Column.ToString() == "Checked" + splitCount)
                            {
                                if (e.Row.ParentRow.Cells[i].Text == "False")
                                {
                                    if (e.Row.ParentRow.Cells[i].Column.Index == 2)
                                    {
                                        e.Row.Cells[e.Row.Cells[i].Column.Index + 2].Activation = Activation.NoEdit;

                                       e.Row.Cells[e.Row.Cells[i].Column.Index +1].Activation = Activation.Disabled;
                                    }
                                    else
                                    {
                                        e.Row.Cells[e.Row.Cells[i].Column.Index + 4].Activation = Activation.NoEdit;

                                        e.Row.Cells[e.Row.Cells[i].Column.Index + 3].Activation = Activation.Disabled;
                                    }


                                }
                                else
                                {
                                    if (e.Row.ParentRow.Cells[i].Column.Index == 2)
                                    {
                                        if (e.Row.Cells["ComponentTypeID"].Text.Equals("3"))
                                        {
                                            e.Row.Cells[e.Row.Cells[i].Column.Index + 2].Activation = Activation.NoEdit;
                                           // e.Row.Cells[e.Row.Cells[i].Column.Index + 1].Activation = Activation.AllowEdit;
                                        }
                                        else
                                        {
                                            e.Row.Cells[e.Row.Cells[i].Column.Index + 2].Activation = Activation.AllowEdit;
                                        }

                                    }
                                    else
                                    {
                                        if (e.Row.Cells["ComponentTypeID"].Text.Equals("3"))
                                        {
                                            e.Row.Cells[e.Row.Cells[i].Column.Index + 4].Activation = Activation.NoEdit;

                                        }
                                        else
                                        {
                                            e.Row.Cells[e.Row.Cells[i].Column.Index + 4].Activation = Activation.AllowEdit;
                                        }
                                    }
                                }

                                splitCount = splitCount + 1;
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

        #endregion ParcelSplitGrid Events

        #endregion

        #region Methods

        /// <summary>
        /// Navigations the button status.
        /// </summary>
        private void NavigationButtonStatus()
        {
            if (this.parcelNumber > 1)
            {
                if (this.parcelNumber == (this.currentParcel + 1))
                {
                    this.ForwardParcelButton.Enabled = false;
                    this.BackwardParcelButton.Enabled = true;
                }
                else if (this.currentParcel == 0)
                {
                    this.ForwardParcelButton.Enabled = true;
                    this.BackwardParcelButton.Enabled = false;
                }
                else
                {
                    this.ForwardParcelButton.Enabled = true;
                    this.BackwardParcelButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Clears the form.
        /// </summary>
        private void ClearForm()
        {
            this.BaseParcelGrid.DataSource = null;
            this.ParcelSplitGrid.DataSource = null;
            this.currentParcel = 0;
            if (this.splitParcelHeaderDataTable != null)
            {
                this.splitParcelHeaderDataTable.Clear();
            }

            if (this.baseParcelDataSet != null)
            {
                this.baseParcelDataSet.Clear();
            }

            if (this.parcelNumberSplitDataSet != null)
            {
                this.parcelNumberSplitDataSet.Clear();
            }
            if (!string.IsNullOrEmpty(this.stateConfigured) && this.stateConfigured.ToUpper().Trim().Equals("NE"))
            {
                this.ClassCodePanel.SendToBack();
            }
            else
            {
                this.SplitDorCodePanel.SendToBack();
            }
            this.SetFormLoadHeight();
            this.EditEnabled();
            this.EmptySplitParcelPanel.Visible = true;
            this.ControlTabStop(false);
            this.SetButton.Enabled = true;
            this.parcelSplited = false;
            this.ParcelSplitTextBox.ReadOnly = false;
            this.DetailsCheckBox.Enabled = true;
            this.ClearButton.Enabled = false;
            this.ParcelSplitTextBox.Text = string.Empty;
            this.parcelNumber = 0;
            this.ParcelSplitTextBox.Focus();
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="status">if set to <c>true</c> [status].</param>
        private void LockControls(bool status)
        {
            IsStatus = status;
            this.SplitHeaderPanel.Enabled = status;
            this.SplitParcelOrderPanel.Enabled = status;
            this.SplitParcelNumberPanel.Enabled = status;
            this.SplitId1Panel.Enabled = status;
            this.SplitDorCodePanel.Enabled = status;
            this.SplitAttachmentPanel.Enabled = status;
            this.SplitCommentsPanel.Enabled = status;
            this.SitusPanel.Enabled = status;
            this.LegalPanel.Enabled = status;
            this.ID2Panel.Enabled = status;           
            if (!status)
            {
              this.ParcelSplitGrid.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
            }
            else
            {
               this.ParcelSplitGrid.DisplayLayout.Override.CellClickAction = CellClickAction.Edit;
            }
        }

        /// <summary>
        /// Forms the load.
        /// </summary>
        private void FormLoad()
        {
            this.FlagSliceForm = true;
            if (this.eventId > 0)
            {
                this.formLoad = true;
                this.parcelSplitDataSet = this.form29500Control.WorkItem.ParcelSplitLoad(this.eventId);

                if (this.parcelSplitDataSet != null)
                {
                    if (this.parcelSplitDataSet.Tables.Count > 0)
                    {
                        if (this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows.Count > 0)
                        {
                            //Modifed to add Configured state
                            //if (!string.IsNullOrEmpty(this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.ConfiguredStateColumn.ColumnName].ToString()))
                            //{
                            //    this.stateConfigured=this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.ConfiguredStateColumn.ColumnName].ToString();
                            //}

                            int.TryParse(this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.BaseParcelIDColumn.ColumnName].ToString(), out this.baseParcelId);

                            if (!string.IsNullOrEmpty(this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.PSXmlColumn.ColumnName].ToString()))
                            {
                                this.parcelSplited = Convert.ToBoolean(this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.PSXmlColumn.ColumnName]);
                            }
                            else
                            {
                                this.parcelSplited = false;
                            }

                            if (this.parcelSplited)
                            {
                                //// this.parcelSplited = true;
                                if (!string.IsNullOrEmpty(this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.IsSplitProcessedColumn.ColumnName].ToString()))
                                {
                                    this.splitProcessed = Convert.ToBoolean(this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.IsSplitProcessedColumn.ColumnName]);
                                }
                                else
                                {
                                    this.splitProcessed = false;
                                }

                                int.TryParse(this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.NumResultingParcelsColumn.ColumnName].ToString(), out this.splitedParcelCount);

                                int.TryParse(this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.NumResultingParcelsColumn.ColumnName].ToString(), out this.parcelNumber);
                                int.TryParse(this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.SplitIDColumn.ColumnName].ToString(), out this.splitId);
                                this.PopulateSplitDefinition();
                                this.baseParcelDataSet = new DataSet();
                                this.baseParcelDataSet = this.parcelSplitDataSet.Copy();
                                this.baseParcelDataSet.Relations.Add(this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListParcelSplitObject.TableName].Columns[this.parcelSplitDataSet.ListParcelSplitObject.ObjectIDColumn.ColumnName], this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListParcelSplitValueSlices.TableName].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.ObjectIDColumn.ColumnName]);
                                this.baseParcelDataSet.Relations.Add(this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListParcelSplitValueSlices.TableName].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.ValueSliceIDColumn.ColumnName], this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListParcelSplitCrop.TableName].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.ValueSliceIDColumn.ColumnName]);
                                this.BaseParcelGrid.DataSource = this.baseParcelDataSet;

                                // PS Grid                            
                                this.CreateDynamicParcelSplit();

                                this.splitParcelHeaderDataTable = this.parcelSplitDataSet.ListSplitHeaderDetail;
                                this.SetControlOnFormLoad();

                                // Check the edit permission for bugid 5959 -- Ramya.D
                                if (this.PermissionFiled.editPermission)
                                {
                                    if (!this.splitProcessed && this.formMasterPermissionEdit)
                                    {
                                        this.ProcessButton.Enabled = true;
                                    }
                                    else
                                    {
                                        this.LockControls(false);
                                    }
                                }

                                this.CheckDorCodeLink();
                                //// this.ParcelSplitGrid.Focus();
                            }
                            else
                            {
                                this.PopulateSplitDefinition();
                                this.EmptySplitParcelPanel.Visible = true;
                                this.ControlTabStop(false);
                                this.parcelSplited = false;

                                // Check the edit permission for bugid 5959 -- Ramya.D
                                if (this.PermissionFiled.editPermission)
                                {
                                    this.SetButton.Enabled = true;
                                    this.ParcelSplitTextBox.ReadOnly = false;
                                    this.DetailsCheckBox.Enabled = true;
                                    this.splitProcessed = false;
                                    this.ClearButton.Enabled = false;
                                    this.ProcessButton.Enabled = false;
                                }
                            }
                        }
                    }
                }

                this.formLoad = false;
            }
            else
            {
                this.formLoad = true;
                ////Coding added for the issue 4497 by Malliga on 29/3/2009
                this.ClearForm();
                ////End here for 4497
                this.LockControls(false);
                this.ParcelPanel.BackColor = Color.White;
                this.ParcelSplitTextBox.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Checks the dor code.
        /// </summary>
        private void CheckDorCodeLink()
        {
            if (!string.IsNullOrEmpty(this.SplitDorCodeLinkLabel.Text.Trim()))
            {
                this.SplitDorCodeTextBox.Visible = false;
                this.SplitDorCodeLinkLabel.Visible = true;
            }
            else
            {
                this.SplitDorCodeTextBox.Visible = true;
                this.SplitDorCodeLinkLabel.Visible = false;
            }
        }

        /// <summary>
        /// Controls the tab stop.
        /// </summary>
        /// <param name="status">if set to <c>true</c> [status].</param>
        private void ControlTabStop(bool status)
        {
            this.SplitParcelOrderPanel.TabStop = status;
            this.SplitParcelNumberPanel.TabStop = status;
            this.SplitId1Panel.TabStop = status;
            this.SplitDorCodePanel.TabStop = status;
            this.SplitAttachmentPanel.TabStop = status;
            this.SplitCommentsPanel.TabStop = status;
            this.ParcelSplitGrid.TabStop = status;
            this.SplitParcelNumberTextBox.TabStop = status;
            this.SplitId1TextBox.TabStop = status;
            this.SplitDorCodeLinkLabel.TabStop = status;
            this.SplitDorCodeTextBox.TabStop = status;
            this.SplitAttachmentCheckBox.TabStop = status;
            this.SplitAssocCheckBox.TabStop = status;
            this.SplitPermitCheckBox.TabStop = status;
            this.SplitCommentsCheckBox.TabStop = status;
            this.ID2Panel.TabStop = status;
            this.Id2TextBox.TabStop = status;
            this.SitusPanel.TabStop = status;
            situsLabelTextBox.TabStop = status;
            this.LegalPanel.TabStop = status;
            this.LegalLabelTextBox.TabStop = status;
            this.LegalLabel.TabStop = status;
        }

        /// <summary>
        /// Sets the control on form load.
        /// </summary>
        private void SetControlOnFormLoad()
        {
            this.BingingSplitParcelHeader(0);
            this.SetSmartpartHeight();
            this.SplitParcelNumberOrderLabel.Text = "of  " + this.parcelNumber;
            this.SplitParcelOrderTextBox.Text = "1";
            this.ShowParcelDetailLabel.Text = SharedFunctions.GetResourceString("ShowingParcelNumber") + "1:";
            this.EmptySplitParcelPanel.Visible = false;
            this.ControlTabStop(true);
            //// this.SplitParcelPanel.Visible = true;
            this.SetButton.Enabled = false;
            this.ParcelSplitTextBox.ReadOnly = true;
            this.DetailsCheckBox.Enabled = false;
            this.ClearButton.Enabled = true;
            ////this.ProcessButton.Enabled = true;

            this.BackwardParcelButton.Enabled = false;
            if (this.parcelNumber <= 1)
            {
                this.ForwardParcelButton.Enabled = false;
            }
            else
            {
                this.ForwardParcelButton.Enabled = true;
            }
        }

        /// <summary>
        /// Checks the parcel range.
        /// </summary>
        /// <returns>bool</returns>
        private bool CheckParcelRange()
        {
            int.TryParse(this.ParcelSplitTextBox.Text.Trim(), out this.parcelNumber);
            if (this.parcelNumber > 0 && this.parcelNumber <= 200)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        /// TSCO - D24500.F29500 Split Parcels form - Display error message when DOR / Class Code field is blank

        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;
            if (this.parcelSplited)
            {
                if (ClearButton.Enabled)
                {
                    if (!string.IsNullOrEmpty(this.stateConfigured) && this.stateConfigured.ToUpper().Equals("WA"))
                    {
                        var splitParcelCount = splitParcelHeaderDataTable.Rows.Count;

                        int counter = 0;
                        for (int i = 0; i < splitParcelCount; i++)
                        {
                            var parcelNumber = this.splitParcelHeaderDataTable.Rows[i][this.parcelSplitDataSet.ListSplitHeaderDetail.ParcelNumberColumn.ColumnName].ToString();
                           var dorColumnNumber = this.splitParcelHeaderDataTable.Rows[i][this.parcelSplitDataSet.ListSplitHeaderDetail.DORColumn.ColumnName].ToString();

                           if (string.IsNullOrEmpty(parcelNumber) || string.IsNullOrEmpty(dorColumnNumber) || dorColumnNumber == "< Select >" || parcelNumber == "< Default >")
                            {
                                counter++;
                            }                                                     
                        }
                        if (counter > 0)
                        {
                            sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredFields");
                            sliceValidationFields.RequiredFieldMissing = true;
                            return sliceValidationFields;
                        }
                        else
                        {
                            sliceValidationFields.RequiredFieldMissing = false;
                            return sliceValidationFields;
                        }
                    }
                }
                ////if (string.IsNullOrEmpty(this.ParcelSplitTextBox.Text.Trim()) || !this.CheckDorCode())
                if (string.IsNullOrEmpty(this.ParcelSplitTextBox.Text.Trim()))
                {
                    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredMsg");
                    sliceValidationFields.RequiredFieldMissing = true;
                    this.ParcelSplitTextBox.Focus();                  
                    return sliceValidationFields;
                }
                else
                {
                    sliceValidationFields.RequiredFieldMissing = false;
                    return sliceValidationFields;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this.ParcelSplitTextBox.Text.Trim()) || !this.CheckParcelRange())
                {
                    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredMsg");
                    sliceValidationFields.RequiredFieldMissing = true;
                    this.ParcelSplitTextBox.Focus();
                    return sliceValidationFields;
                }
                else
                {
                    sliceValidationFields.RequiredFieldMissing = false;
                    return sliceValidationFields;
                }
            }
        }

        /// <summary>
        /// Checks the dor code.
        /// </summary>
        /// <returns>bool</returns>
        private bool CheckDorCode()
        {
            this.splitParcelHeaderDataTable.AcceptChanges();
            this.GetSplitHeaderDetail(this.currentParcel);

            for (int dorCount = 0; dorCount < this.splitParcelHeaderDataTable.Rows.Count; dorCount++)
            {
                if (string.IsNullOrEmpty(this.splitParcelHeaderDataTable.Rows[dorCount][this.splitParcelHeaderDataTable.DORColumn.ColumnName].ToString()))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            // Check the form master edit permission for bugid 5959
            if (!this.formLoad && !this.splitProcessed && !this.navigationClicked && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;

                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                this.ProcessButton.Enabled = false;
                if (this.lockBool == false)
                {
                    Parcellockimplimentation parcellock = new Parcellockimplimentation();
                    parcellock.MasterFormNo = this.masterFormNo;
                    parcellock.SaveButtonEnable = true;
                    parcellock.CancelButtonEnable = true;
                    parcellock.SaveButtonText = "Save";
                    parcellock.SaveButtonForeColor = Color.White;
                    parcellock.SaveButtonBackColor = Color.FromArgb(28, 81, 128);
                    this.D9030_F9030_SetParcelLockProperties(this, new DataEventArgs<Parcellockimplimentation>(parcellock));
                }

                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
            }
        }

        /// <summary>
        /// Populates the split definition.
        /// </summary>
        private void PopulateSplitDefinition()
        {
            try
            {
                if (this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows.Count > 0)
                {
                    this.ParcelNumberTextBox.Text = this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.ParcelNumberColumn.ColumnName].ToString();
                    this.ParcelSplitTextBox.Text = this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.NumResultingParcelsColumn.ColumnName].ToString();
                    this.OriginalTaxableValueTextBox.Text = this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.O3ValueColumn.ColumnName].ToString();
                    //To implement Class code
                    if (!string.IsNullOrEmpty(this.stateConfigured) && (this.stateConfigured.ToUpper().Equals("NE")))
                    {
                        this.ClassCodeComboBox.Text = this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.ClassCodeColumn.ColumnName].ToString();
                        this.ClassCodeTextBox.Text = this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.ClassCodeColumn.ColumnName].ToString();
                    }


                    if (!string.IsNullOrEmpty(this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.IsDetailsColumn.ColumnName].ToString()))
                    {
                        if (Convert.ToBoolean(this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.IsDetailsColumn.ColumnName].ToString()))
                        {
                            this.DetailsCheckBox.Checked = true;
                        }
                        else
                        {
                            this.DetailsCheckBox.Checked = false;
                        }
                    }
                    else
                    {
                        this.DetailsCheckBox.Checked = true;
                    }

                    if (!string.IsNullOrEmpty(this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.IsAttachmentsColumn.ColumnName].ToString()))
                    {
                        if (Convert.ToBoolean(this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.IsAttachmentsColumn.ColumnName].ToString()))
                        {
                            this.AttachmentsCheckBox.Checked = true;
                        }
                        else
                        {
                            this.AttachmentsCheckBox.Checked = false;
                        }
                    }
                    else
                    {
                        this.AttachmentsCheckBox.Checked = true;
                    }

                    if (!string.IsNullOrEmpty(this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.IsCommentsColumn.ColumnName].ToString()))
                    {
                        if (Convert.ToBoolean(this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.IsCommentsColumn.ColumnName].ToString()))
                        {
                            this.CommentsCheckBox.Checked = true;
                        }
                        else
                        {
                            this.CommentsCheckBox.Checked = false;
                        }
                    }
                    else
                    {
                        this.CommentsCheckBox.Checked = true;
                    }



                    if (!string.IsNullOrEmpty(this.parcelSplitDataSet.ListSplitHeaderDetail.Rows[0][this.parcelSplitDataSet.ListSplitHeaderDetail.IsAssociationColumn.ColumnName].ToString()))
                    {
                        if (Convert.ToBoolean(this.parcelSplitDataSet.ListSplitHeaderDetail.Rows[0][this.parcelSplitDataSet.ListSplitHeaderDetail.IsAssociationColumn.ColumnName].ToString()))
                        {
                            this.SplitAssocCheckBox.Checked = true;
                        }
                        else
                        {
                            this.SplitAssocCheckBox.Checked = false;
                        }
                    }
                    else
                    {
                        this.SplitAssocCheckBox.Checked = true;
                    }

                    if (!string.IsNullOrEmpty(this.parcelSplitDataSet.ListSplitHeaderDetail.Rows[0][this.parcelSplitDataSet.ListSplitHeaderDetail.IsPermitColumn.ColumnName].ToString()))
                    {
                        if (Convert.ToBoolean(this.parcelSplitDataSet.ListSplitHeaderDetail.Rows[0][this.parcelSplitDataSet.ListSplitHeaderDetail.IsPermitColumn.ColumnName].ToString()))
                        {
                            this.SplitPermitCheckBox.Checked = true;
                        }
                        else
                        {
                            this.SplitPermitCheckBox.Checked = false;
                        }
                    }
                    else
                    {
                        this.SplitPermitCheckBox.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Customizes the grid.
        /// </summary>
        private void CustomizeGrid()
        {
            //// Band Header visible
            this.BaseParcelGrid.DisplayLayout.Bands[0].ColHeadersVisible = true;
            this.BaseParcelGrid.DisplayLayout.Bands[1].ColHeadersVisible = false;
            this.BaseParcelGrid.DisplayLayout.Bands[2].ColHeadersVisible = false;
            this.BaseParcelGrid.DisplayLayout.Bands[0].Override.HeaderPlacement = HeaderPlacement.FixedOnTop;

            //// Band Expansion indicator style
            this.BaseParcelGrid.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None;
            this.BaseParcelGrid.DisplayLayout.Override.ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.Never;
            this.BaseParcelGrid.DisplayLayout.Rows.ExpandAll(true);

            //// Band starting point 
            this.BaseParcelGrid.DisplayLayout.Bands[0].Indentation = 0;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Indentation = 0;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Indentation = 0;

            //// Reduce the band spacing
            this.BaseParcelGrid.DisplayLayout.InterBandSpacing = 0;

            //// Band RowSelectors style
            this.BaseParcelGrid.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
            this.BaseParcelGrid.DisplayLayout.Override.SpecialRowSeparator = SpecialRowSeparator.All;
            this.BaseParcelGrid.DisplayLayout.Override.SpecialRowSeparatorHeight = 0;

            this.BaseParcelGrid.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.False;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Override.AllowUpdate = DefaultableBoolean.False;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Override.AllowUpdate = DefaultableBoolean.False;

            this.BaseParcelGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;

            //// Bases parcel column arrangement.
            this.BaseParcelColumnArrangement();

            //// Header font setting
            this.BaseParcelGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListParcelSplitObject.ObjectColumn.ColumnName].Header.Appearance.FontData.SizeInPoints = 9;
            this.BaseParcelGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListParcelSplitObject.DescriptionColumn.ColumnName].Header.Appearance.FontData.SizeInPoints = 9;
            this.BaseParcelGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListParcelSplitObject.ValueStringColumn.ColumnName].Header.Appearance.FontData.SizeInPoints = 8;
            this.BaseParcelGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListParcelSplitObject.RollStringColumn.ColumnName].Header.Appearance.FontData.SizeInPoints = 8;

            this.BaseParcelGrid.DisplayLayout.Bands[0].Override.HeaderAppearance.BackColor = Color.FromArgb(31, 65, 103);
            this.BaseParcelGrid.DisplayLayout.Bands[0].Override.HeaderAppearance.ForeColor = Color.FromArgb(255, 204, 0);
            this.BaseParcelGrid.DisplayLayout.Bands[0].Override.HeaderStyle = HeaderStyle.Standard;
            this.BaseParcelGrid.DisplayLayout.Bands[0].Override.HeaderAppearance.FontData.Name = "Arial";
            this.BaseParcelGrid.DisplayLayout.Bands[0].Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
            this.BaseParcelGrid.DisplayLayout.Bands[0].Override.HeaderAppearance.TextHAlign = HAlign.Left;

            //// Column Sizing            
            this.BaseParcelGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListParcelSplitObject.ObjectColumn.ColumnName].Width = 100;
            this.BaseParcelGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListParcelSplitObject.DescriptionColumn.ColumnName].Width = 150;
            this.BaseParcelGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListParcelSplitObject.ValueStringColumn.ColumnName].Width = 50;
            this.BaseParcelGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListParcelSplitObject.RollStringColumn.ColumnName].Width = 50;
            this.BaseParcelGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListParcelSplitObject.ObjectStringColumn.ColumnName].Width = 95;

            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.VSObjectColumn.ColumnName].Width = 100;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.VSDescriptionColumn.ColumnName].Width = 150;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.ValueStringColumn.ColumnName].Width = 50;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.RollStringColumn.ColumnName].Width = 50;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.VSStringColumn.ColumnName].Width = 95;

            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.VSObjectColumn.ColumnName].Width = 100;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.VSDescriptionColumn.ColumnName].Width = 150;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.ValueStringColumn.ColumnName].Width = 50;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.RollStringColumn.ColumnName].Width = 50;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.VSStringColumn.ColumnName].Width = 74;

            //// Header Caption
            this.BaseParcelGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListParcelSplitObject.ObjectColumn.ColumnName].Header.Caption = "Object";
            this.BaseParcelGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListParcelSplitObject.DescriptionColumn.ColumnName].Header.Caption = "Description";
            this.BaseParcelGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListParcelSplitObject.ValueStringColumn.ColumnName].Header.Caption = "Value";
            this.BaseParcelGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListParcelSplitObject.RollStringColumn.ColumnName].Header.Caption = "Roll";
            this.BaseParcelGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListParcelSplitObject.ObjectStringColumn.ColumnName].Header.Caption = "Usage";

            //// Disable edit mode
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.VSObjectColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.VSDescriptionColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.ValueStringColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.RollStringColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.VSStringColumn.ColumnName].CellActivation = Activation.NoEdit;

            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.VSObjectColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.VSDescriptionColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.ValueStringColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.RollStringColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.VSStringColumn.ColumnName].CellActivation = Activation.AllowEdit;

            //// Change the Row RowAppearance
            this.BaseParcelGrid.DisplayLayout.Bands[0].Override.RowAppearance.BackColor = Color.FromArgb(31, 65, 103);
            this.BaseParcelGrid.DisplayLayout.Bands[0].Override.RowAppearance.BorderColor = Color.FromArgb(31, 65, 103);
            this.BaseParcelGrid.DisplayLayout.Bands[0].Override.RowAppearance.ForeColor = Color.White;
            this.BaseParcelGrid.DisplayLayout.Bands[0].Override.RowAppearance.FontData.SizeInPoints = 10;
            this.BaseParcelGrid.DisplayLayout.Bands[0].Override.RowAppearance.FontData.Bold = DefaultableBoolean.True;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Override.RowAppearance.BackColor = Color.FromArgb(217, 217, 217);
            this.BaseParcelGrid.DisplayLayout.Bands[2].Override.RowAppearance.BackColor = Color.FromArgb(217, 217, 217);

            //// Alternate row appearance
            this.BaseParcelGrid.DisplayLayout.Bands[1].Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.BaseParcelGrid.DisplayLayout.Bands[1].Override.RowAlternateAppearance.BackColor2 = Color.White;

            this.BaseParcelGrid.DisplayLayout.Bands[2].Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.BaseParcelGrid.DisplayLayout.Bands[2].Override.RowAlternateAppearance.BackColor2 = Color.White;

            this.BaseParcelGrid.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;

            ////Crop Row Object column BackColor
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.VSObjectColumn.ColumnName].CellAppearance.BackColor = Color.FromArgb(16, 37, 65);

            ////Crop Row Font Color
            this.BaseParcelGrid.DisplayLayout.Bands[2].Override.RowAppearance.ForeColor = Color.FromArgb(128, 0, 0);
            this.BaseParcelGrid.DisplayLayout.Bands[2].Override.RowAlternateAppearance.ForeColor = Color.FromArgb(128, 0, 0);
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.VSStringColumn.ColumnName].CellAppearance.FontData.SizeInPoints = 7;

            //// Text Alignment of grid Cells
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.VSObjectColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.VSObjectColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.VSDescriptionColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.VSDescriptionColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.ValueStringColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.ValueStringColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.RollStringColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.RollStringColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.VSObjectColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.VSObjectColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.VSDescriptionColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.VSDescriptionColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.ValueStringColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.ValueStringColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.RollStringColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.RollStringColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.VSStringColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.VSStringColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            //// Font size for valueslice table
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.VSObjectColumn.ColumnName].CellAppearance.FontData.SizeInPoints = 9;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.VSDescriptionColumn.ColumnName].CellAppearance.FontData.SizeInPoints = 9;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.ValueStringColumn.ColumnName].CellAppearance.FontData.SizeInPoints = 8;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.RollStringColumn.ColumnName].CellAppearance.FontData.SizeInPoints = 8;

            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.VSObjectColumn.ColumnName].CellAppearance.FontData.SizeInPoints = 9;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.VSDescriptionColumn.ColumnName].CellAppearance.FontData.SizeInPoints = 9;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.ValueStringColumn.ColumnName].CellAppearance.FontData.SizeInPoints = 8;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.RollStringColumn.ColumnName].CellAppearance.FontData.SizeInPoints = 8;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.VSStringColumn.ColumnName].CellAppearance.FontData.SizeInPoints = 8;
        }

        /// <summary>
        /// Customizes the parcel split grid.
        /// </summary>
        private void CustomizeParcelSplitGrid()
        {
            this.ParcelSplitGrid.DisplayLayout.Bands[0].ColHeadersVisible = false;
            this.ParcelSplitGrid.DisplayLayout.Bands[1].ColHeadersVisible = false;
            this.ParcelSplitGrid.DisplayLayout.Bands[2].ColHeadersVisible = false;
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Override.HeaderPlacement = HeaderPlacement.FixedOnTop;

            // Band Expansion indicator style
            this.ParcelSplitGrid.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None;
            this.ParcelSplitGrid.DisplayLayout.Override.ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.Never;
            this.ParcelSplitGrid.DisplayLayout.Rows.ExpandAll(true);

            // Band starting point 
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Indentation = 0;
            this.ParcelSplitGrid.DisplayLayout.Bands[1].Indentation = 0;
            this.ParcelSplitGrid.DisplayLayout.Bands[2].Indentation = 0;

            // Reduce the band spacing
            this.ParcelSplitGrid.DisplayLayout.InterBandSpacing = 0;

            // Band RowSelectors style
            this.ParcelSplitGrid.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
            this.ParcelSplitGrid.DisplayLayout.Override.SpecialRowSeparator = SpecialRowSeparator.All;
            this.ParcelSplitGrid.DisplayLayout.Override.SpecialRowSeparatorHeight = 0;

            if (this.PermissionFiled.editPermission)
            {
                this.ParcelSplitGrid.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
                this.ParcelSplitGrid.DisplayLayout.Bands[1].Override.AllowUpdate = DefaultableBoolean.True;
                this.ParcelSplitGrid.DisplayLayout.Bands[2].Override.AllowUpdate = DefaultableBoolean.True;
            }
            else
            {
                this.ParcelSplitGrid.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.False;
                this.ParcelSplitGrid.DisplayLayout.Bands[1].Override.AllowUpdate = DefaultableBoolean.False;
                this.ParcelSplitGrid.DisplayLayout.Bands[2].Override.AllowUpdate = DefaultableBoolean.False;
            }

            // Hide Column
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListSplitObject.ObjectIDColumn.ColumnName].Hidden = true;
            this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListSplitValuseSlice.ObjectIDColumn.ColumnName].Hidden = true;
            this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListSplitValuseSlice.ValueSliceIDColumn.ColumnName].Hidden = true;
            this.ParcelSplitGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListSplitCrop.ObjectIDColumn.ColumnName].Hidden = true;
            this.ParcelSplitGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListSplitCrop.ValueSliceIDColumn.ColumnName].Hidden = true;
            this.ParcelSplitGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListSplitCrop.SliceTypeIDColumn.ColumnName].Hidden = true;
            this.ParcelSplitGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListSplitCrop.ComponentIDColumn.ColumnName].Hidden = true;
            this.ParcelSplitGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListSplitCrop.ComponentTypeIDColumn.ColumnName].Hidden = true;

            this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListSplitObject.ObjectString1Column.ColumnName].Hidden = false;
            this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListSplitValuseSlice.VSString1Column.ColumnName].Hidden = false;
            //this.ParcelSplitGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListSplitCrop.Checked1Column.ColumnName].Hidden = true;

            // Column sizing
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListSplitObject.Checked1Column.ColumnName].Width = 33;
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListSplitObject.ObjectString1Column.ColumnName].Width = 53;
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListSplitObject.IsValueColumn.ColumnName].Width = 53;
            this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListSplitValuseSlice.Checked1Column.ColumnName].Width = 33;
            this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListSplitValuseSlice.VSString1Column.ColumnName].Width = 53;
            this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListSplitValuseSlice.IsValueColumn.ColumnName].Width = 53;
            this.ParcelSplitGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListSplitCrop.Checked1Column.ColumnName].Width = 33;
            this.ParcelSplitGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListSplitCrop.VSString1Column.ColumnName].Width = 53;

            // Disable edit mode

            this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListSplitObject.ObjectString1Column.ColumnName].CellActivation = Activation.NoEdit;
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListSplitObject.IsValueColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListSplitValuseSlice.VSString1Column.ColumnName].CellActivation = Activation.NoEdit;
            this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListSplitValuseSlice.IsValueColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.ParcelSplitGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListSplitCrop.VSString1Column.ColumnName].CellActivation = Activation.AllowEdit;
            this.ParcelSplitGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListSplitCrop.Checked1Column.ColumnName].CellActivation = Activation.AllowEdit;

            this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListSplitObject.ObjectString1Column.ColumnName].CellDisplayStyle = Infragistics.Win.UltraWinGrid.CellDisplayStyle.FormattedText;
            this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListSplitValuseSlice.VSString1Column.ColumnName].CellDisplayStyle = Infragistics.Win.UltraWinGrid.CellDisplayStyle.FormattedText; 
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListSplitObject.ObjectString1Column.ColumnName].CellAppearance.Cursor = Cursors.Hand;
            this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListSplitValuseSlice.VSString1Column.ColumnName].CellAppearance.Cursor = Cursors.Hand;
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListSplitObject.ObjectString1Column.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;
            this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListSplitValuseSlice.VSString1Column.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListSplitObject.ObjectString1Column.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListSplitValuseSlice.VSString1Column.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            

            this.ParcelSplitGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListSplitCrop.VSString1Column.ColumnName].MaxLength = 15;

            // Change the Row RowAppearance
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Override.RowAppearance.BackColor = Color.FromArgb(140, 140, 140);
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Override.RowAppearance.BorderColor = Color.FromArgb(31, 65, 103);
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Override.RowAppearance.ForeColor = Color.White;
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Override.RowAppearance.FontData.SizeInPoints = 10;
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Override.RowAppearance.FontData.Bold = DefaultableBoolean.True;
            this.ParcelSplitGrid.DisplayLayout.Bands[1].Override.RowAppearance.BackColor = Color.FromArgb(217, 217, 217);
            this.ParcelSplitGrid.DisplayLayout.Bands[2].Override.RowAppearance.BackColor = Color.FromArgb(217, 217, 217);

            // Alternate row appearance
            this.ParcelSplitGrid.DisplayLayout.Bands[1].Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ParcelSplitGrid.DisplayLayout.Bands[1].Override.RowAlternateAppearance.BackColor2 = Color.FromArgb(217, 217, 217);
            this.ParcelSplitGrid.DisplayLayout.Bands[2].Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ParcelSplitGrid.DisplayLayout.Bands[2].Override.RowAlternateAppearance.BackColor2 = Color.FromArgb(217, 217, 217);
        }

        /// <summary>
        /// Bases the parcel column arrangement.
        /// </summary>
        private void BaseParcelColumnArrangement()
        {
            // Hide columns
            this.BaseParcelGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListParcelSplitObject.ObjectIDColumn.ColumnName].Hidden = true;
            this.BaseParcelGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListParcelSplitObject.ObjectTypeIDColumn.ColumnName].Hidden = true;
            this.BaseParcelGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListParcelSplitObject.IsRollColumn.ColumnName].Hidden = true;
            this.BaseParcelGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListParcelSplitObject.IsValueColumn.ColumnName].Hidden = true;
            this.BaseParcelGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListParcelSplitObject.VSCountColumn.ColumnName].Hidden = true;

            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.ObjectIDColumn.ColumnName].Hidden = true;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.ValueSliceIDColumn.ColumnName].Hidden = true;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.SliceTypeIDColumn.ColumnName].Hidden = true;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.IsRollColumn.ColumnName].Hidden = true;
            this.BaseParcelGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListParcelSplitValueSlices.IsValueColumn.ColumnName].Hidden = true;

            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.ObjectIDColumn.ColumnName].Hidden = true;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.ValueSliceIDColumn.ColumnName].Hidden = true;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.SliceTypeIDColumn.ColumnName].Hidden = true;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.IsRollColumn.ColumnName].Hidden = true;
            this.BaseParcelGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListParcelSplitCrop.IsValueColumn.ColumnName].Hidden = true;
        }

        /// <summary>
        /// Creates the dynamic parcel split.
        /// </summary>
        private void CreateDynamicParcelSplit()
        {
            int objectValueCount = 0;
            int objectSliceValueCount = 0;
            this.parcelNumberSplitDataSet = new DataSet();

            this.parcelSplitDataSet.Tables["ListSplitObject"].Columns[this.parcelSplitDataSet.ListSplitObject.IsValueColumn.ColumnName].ColumnName = "IsValue1";
            this.parcelSplitDataSet.Tables["ListSplitValuseSlice"].Columns[this.parcelSplitDataSet.ListSplitValuseSlice.IsValueColumn.ColumnName].ColumnName = "IsValue1";

            this.parcelNumberSplitDataSet.Tables.Add(this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListSplitObject.TableName].Copy());
            this.parcelNumberSplitDataSet.Tables.Add(this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListSplitValuseSlice.TableName].Copy());
            this.parcelNumberSplitDataSet.Tables.Add(this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListSplitCrop.TableName].Copy());
            this.parcelNumberSplitDataSet.Relations.Add(this.parcelNumberSplitDataSet.Tables[0].Columns[this.parcelSplitDataSet.ListSplitObject.ObjectIDColumn.ColumnName], this.parcelNumberSplitDataSet.Tables[1].Columns[this.parcelSplitDataSet.ListSplitValuseSlice.ObjectIDColumn.ColumnName]);
            this.parcelNumberSplitDataSet.Relations.Add(this.parcelNumberSplitDataSet.Tables[1].Columns[this.parcelSplitDataSet.ListSplitValuseSlice.ValueSliceIDColumn.ColumnName], this.parcelNumberSplitDataSet.Tables[2].Columns[this.parcelSplitDataSet.ListSplitCrop.ValueSliceIDColumn.ColumnName]);

            this.parcelNumberSplitDataSet.Tables["ListSplitObject"].Columns[3].ColumnName = "IsValue1";
            this.parcelNumberSplitDataSet.Tables["ListSplitValuseSlice"].Columns[4].ColumnName = "IsValue1";

            
            // For Objects
            if (this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListObjectSavedValue.TableName].Rows.Count > 0)
            {
                for (int i = 0; i < this.parcelNumberSplitDataSet.Tables[0].Columns.Count; i++)
                {
                    if (this.parcelNumberSplitDataSet.Tables[0].Columns[i].ColumnName.Substring(0, 7) == "IsValue")
                    {
                        for (int j = 0; j < this.parcelNumberSplitDataSet.Tables[0].Rows.Count; j++)
                        {
                            if (objectValueCount >= this.parcelSplitDataSet.ListObjectSavedValue.Rows.Count)
                            {

                            }
                            else
                            {
                                this.parcelNumberSplitDataSet.Tables[0].Rows[j][i] = Convert.ToBoolean(this.parcelSplitDataSet.ListObjectSavedValue.Rows[objectValueCount][this.parcelSplitDataSet.ListObjectSavedValue.IsValueColumn.ColumnName]);
                                objectValueCount++;
                            }
                        }
                    }
                }
            }
            //for Value Slice
            if (this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListValueSliceSavedValue.TableName].Rows.Count > 0)
            {
                for (int i = 0; i < this.parcelNumberSplitDataSet.Tables[1].Columns.Count; i++)
                {
                    if (this.parcelNumberSplitDataSet.Tables[1].Columns[i].ColumnName.Substring(0, 7) == "IsValue")
                    {
                        for (int j = 0; j < this.parcelNumberSplitDataSet.Tables[1].Rows.Count; j++)
                        {
                            if (objectSliceValueCount >= this.parcelSplitDataSet.ListValueSliceSavedValue.Rows.Count)
                            {

                            }
                            else
                            {
                                this.parcelNumberSplitDataSet.Tables[1].Rows[j][i] = Convert.ToBoolean(this.parcelSplitDataSet.ListValueSliceSavedValue.Rows[objectSliceValueCount][this.parcelSplitDataSet.ListValueSliceSavedValue.IsValueColumn.ColumnName]);
                                objectSliceValueCount++;
                            }
                        }
                    }
                }
            }

            this.ParcelSplitGrid.DataSource = null;
            this.ParcelSplitGrid.DataSource = this.parcelNumberSplitDataSet;

         


           // this.isCurrentRow = this.parcelNumberSplitDataSet..Rows.IndexOf(rows[0]);

            this.splitParcelBand = this.ParcelSplitGrid.DisplayLayout.Bands[0];
            this.splitParcelGroup = this.splitParcelBand.Groups.Add("Group1", "Parcel 1");
            this.splitParcelGroup.Columns.Add(this.splitParcelBand.Columns[this.parcelSplitDataSet.ListSplitObject.Checked1Column.ColumnName]);
            this.splitParcelGroup.Columns.Add(this.splitParcelBand.Columns[this.parcelSplitDataSet.ListSplitObject.ObjectString1Column.ColumnName]);
            this.splitParcelGroup.Columns.Add(this.splitParcelBand.Columns[this.parcelSplitDataSet.ListSplitObject.IsValueColumn.ColumnName]);
           

            // Header Appearance
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Override.HeaderStyle = HeaderStyle.Standard;
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Override.HeaderAppearance.ForeColor = Color.FromArgb(255, 204, 0);
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Override.HeaderAppearance.BackColor = Color.FromArgb(119, 47, 40);
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Override.HeaderAppearance.FontData.Name = "Arial";
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Override.HeaderAppearance.FontData.SizeInPoints = 9;

            this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns[this.parcelSplitDataSet.ListSplitObject.IsValueColumn.ColumnName].Hidden = true;
            this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns[this.parcelSplitDataSet.ListSplitValuseSlice.IsValueColumn.ColumnName].Hidden = true;
            this.ParcelSplitGrid.DisplayLayout.Bands[2].Columns[this.parcelSplitDataSet.ListSplitCrop.IsValue1Column.ColumnName].Hidden = true;


            foreach (UltraGridRow currentRow in this.ParcelSplitGrid.Rows.GetRowEnumerator(GridRowType.DataRow, this.ParcelSplitGrid.DisplayLayout.Bands[0], null))
            {
                if ((currentRow.Cells["IsValue1"].Value.Equals(true)))
                {
                    currentRow.Cells["ObjectString1"].Appearance.BackColor = Color.FromArgb(31, 65, 103);
                    currentRow.Cells["ObjectString1"].Appearance.BorderColor = Color.FromArgb(0,0,0);
                    currentRow.Cells["ObjectString1"].Appearance.ForeColor = Color.White;
                    currentRow.Cells["ObjectString1"].Appearance.FontData.SizeInPoints = 10;
                    currentRow.Cells["ObjectString1"].Appearance.FontData.Bold = DefaultableBoolean.True;
                    currentRow.Update();

                   
                }
                else
                {
                    currentRow.Cells["ObjectString1"].Appearance.BackColor = Color.FromArgb(77, 77, 77);
                    currentRow.Cells["ObjectString1"].Appearance.BorderColor = Color.FromArgb(0, 0, 0);
                    currentRow.Cells["ObjectString1"].Appearance.ForeColor = Color.White;
                    currentRow.Cells["ObjectString1"].Appearance.FontData.SizeInPoints = 10;
                    currentRow.Cells["ObjectString1"].Appearance.FontData.Bold = DefaultableBoolean.True;
                    currentRow.Cells["ObjectString1"].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                    currentRow.Update();
                }
            }


            foreach (UltraGridRow currentRow in this.ParcelSplitGrid.Rows.GetRowEnumerator(GridRowType.DataRow, this.ParcelSplitGrid.DisplayLayout.Bands[1], null))
            {
                if ((currentRow.Cells["IsValue1"].Value.Equals(true)))
                {
                    currentRow.Cells["VSString1"].Appearance.BackColor = Color.FromArgb(255, 255, 255);
                    currentRow.Cells["VSString1"].Appearance.BorderColor = Color.FromArgb(0, 0, 0);
                    currentRow.Cells["VSString1"].Appearance.ForeColor = Color.Black;
                    currentRow.Update();
                }
                else
                {
                    currentRow.Cells["VSString1"].Appearance.BackColor = Color.FromArgb(77, 77, 77);
                    currentRow.Cells["VSString1"].Appearance.BorderColor = Color.FromArgb(0, 0, 0);
                    currentRow.Cells["VSString1"].Appearance.ForeColor = Color.White;
                    currentRow.Cells["VSString1"].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                    currentRow.Update();
                }
            }

            this.SplitParcel();
        }

        /// <summary>
        /// Splits the parcel.
        /// </summary>
        private void SplitParcel()
        {
            DataColumn objectCheckedColumn;
            DataColumn objectValueColumn;
            DataColumn valueCheckedColumn;
            DataColumn VSValueColumn;
            DataColumn cropCheckedColumn;
            DataColumn objectValueColumn1;
            DataColumn VSValueColumn1;

            int sliceCount = 0;
            int objectCount = 0;
            int objectValueCount = 0;
            int sliceValueCount = 0;
            int isCheckedCount = 0;
            int isUnitCount = 0;
            int cropCount = 0;
            int Count = 0;



            for (int spiltCount = 2; spiltCount <= this.parcelNumber; spiltCount++)
            {
                objectCheckedColumn = new DataColumn();
                objectCheckedColumn.DataType = System.Type.GetType("System.Boolean");
                objectCheckedColumn.ColumnName = "Checked" + spiltCount.ToString();
                objectCheckedColumn.DefaultValue = true;
                this.parcelNumberSplitDataSet.Tables[0].Columns.Add(objectCheckedColumn);

                //this.parcelNumberSplitDataSet.Tables[0].Columns.Add(new DataColumn("ObjectString" + spiltCount.ToString(), System.Type.GetType("System.String")));
                objectValueColumn1 = new DataColumn();
                objectValueColumn1.DataType = System.Type.GetType("System.String");
                objectValueColumn1.ColumnName = "ObjectString" + spiltCount.ToString();
                objectValueColumn1.DefaultValue = "Value";
                this.parcelNumberSplitDataSet.Tables[0].Columns.Add(objectValueColumn1);

                objectValueColumn = new DataColumn();
                objectValueColumn.DataType = System.Type.GetType("System.Boolean");
                objectValueColumn.ColumnName = "IsValue" + spiltCount.ToString();
                this.parcelNumberSplitDataSet.Tables[0].Columns.Add(objectValueColumn);

                valueCheckedColumn = new DataColumn();
                valueCheckedColumn.DataType = System.Type.GetType("System.Boolean");
                valueCheckedColumn.ColumnName = "Checked" + spiltCount.ToString();
                valueCheckedColumn.DefaultValue = true;
                this.parcelNumberSplitDataSet.Tables[1].Columns.Add(valueCheckedColumn);

               // this.parcelNumberSplitDataSet.Tables[1].Columns.Add(new DataColumn("VSString" + spiltCount.ToString(), System.Type.GetType("System.String")));

                VSValueColumn1 = new DataColumn();
                VSValueColumn1.DataType = System.Type.GetType("System.String");
                VSValueColumn1.ColumnName = "VSString" + spiltCount.ToString();
                VSValueColumn1.DefaultValue = "Value";
                this.parcelNumberSplitDataSet.Tables[1].Columns.Add(VSValueColumn1);

                VSValueColumn = new DataColumn();
                VSValueColumn.DataType = System.Type.GetType("System.Boolean");
                VSValueColumn.ColumnName = "IsValue" + spiltCount.ToString();
                this.parcelNumberSplitDataSet.Tables[1].Columns.Add(VSValueColumn);

               

                cropCheckedColumn = new DataColumn();
                cropCheckedColumn.DataType = System.Type.GetType("System.Boolean");
                //cropCheckedColumn.DataType = typeof(object);
                cropCheckedColumn.ColumnName = "Checked" + spiltCount.ToString();
                //cropCheckedColumn.DefaultValue = false;
                this.parcelNumberSplitDataSet.Tables[2].Columns.Add(cropCheckedColumn);
                this.parcelNumberSplitDataSet.Tables[2].Columns.Add(new DataColumn("VSString" + spiltCount.ToString(), System.Type.GetType("System.String")));
                this.parcelNumberSplitDataSet.Tables[2].Columns["VSString" + spiltCount.ToString()].MaxLength = 15;
                this.parcelNumberSplitDataSet.Tables[2].Columns.Add(new DataColumn("IsValue" + spiltCount.ToString(), System.Type.GetType("System.String")));
               
                string groupName = "Group" + spiltCount.ToString();
                string headerName = "Parcel " + spiltCount.ToString();
                this.splitParcelGroup = this.splitParcelBand.Groups.Add(groupName, headerName);
                this.splitParcelGroup.Columns.Add(this.splitParcelBand.Columns["Checked" + spiltCount.ToString()]);
                this.splitParcelGroup.Columns.Add(this.splitParcelBand.Columns["ObjectString" + spiltCount.ToString()]);
                this.splitParcelGroup.Columns.Add(this.splitParcelBand.Columns["IsValue" + spiltCount.ToString()]);
                

                //// Column sizing
                this.ParcelSplitGrid.DisplayLayout.Bands[0].Override.HeaderAppearance.BackColor = Color.FromArgb(22, 52, 92);
                this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns["Checked" + spiltCount.ToString()].Width = 33;
                this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns["ObjectString" + spiltCount.ToString()].Width = 53;
                this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns["IsValue" + spiltCount.ToString()].Width = 53;
                
                this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns["Checked" + spiltCount.ToString()].Width = 33;
                this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns["VSString" + spiltCount.ToString()].Width = 53;
                this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns["IsValue" + spiltCount.ToString()].Width = 53;
                
                this.ParcelSplitGrid.DisplayLayout.Bands[2].Columns["Checked" + spiltCount.ToString()].Width = 33;
                this.ParcelSplitGrid.DisplayLayout.Bands[2].Columns["VSString" + spiltCount.ToString()].Width = 53;

                this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns["Checked" + spiltCount.ToString()].CellActivation = Activation.AllowEdit;
                this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns["ObjectString" + spiltCount.ToString()].CellActivation = Activation.NoEdit;
                //this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns["ObjectString" + spiltCount.ToString()].CellActivation = Activation.Disabled;
                this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns["Checked" + spiltCount.ToString()].CellActivation = Activation.AllowEdit;
                this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns["VSString" + spiltCount.ToString()].CellActivation = Activation.NoEdit;
                ///this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns["VSString" + spiltCount.ToString()].CellActivation = Activation.Disabled;
                this.ParcelSplitGrid.DisplayLayout.Bands[2].Columns["Checked" + spiltCount.ToString()].CellActivation = Activation.AllowEdit;
                this.ParcelSplitGrid.DisplayLayout.Bands[2].Columns["VSString" + spiltCount.ToString()].CellActivation = Activation.AllowEdit;


                this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns["IsValue" + spiltCount.ToString()].Hidden = true;
                this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns["IsValue" + spiltCount.ToString()].Hidden = true;
                this.ParcelSplitGrid.DisplayLayout.Bands[2].Columns["IsValue" + spiltCount.ToString()].Hidden = true;



                this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns["ObjectString" + spiltCount.ToString()].CellDisplayStyle = Infragistics.Win.UltraWinGrid.CellDisplayStyle.FormattedText;
                this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns["VSString" + spiltCount.ToString()].CellDisplayStyle = Infragistics.Win.UltraWinGrid.CellDisplayStyle.FormattedText;
                this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns["ObjectString" + spiltCount.ToString()].CellAppearance.Cursor = Cursors.Hand;
                this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns["VSString" + spiltCount.ToString()].CellAppearance.Cursor = Cursors.Hand;
                this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns["ObjectString" + spiltCount.ToString()].CellAppearance.TextVAlign = VAlign.Middle;
                this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns["VSString" + spiltCount.ToString()].CellAppearance.TextVAlign = VAlign.Middle;
                this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns["ObjectString" + spiltCount.ToString()].CellAppearance.TextHAlign = HAlign.Center;
                this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns["VSString" + spiltCount.ToString()].CellAppearance.TextHAlign = HAlign.Center;

             


                //foreach (UltraGridRow currentRow in this.ParcelSplitGrid.Rows.GetRowEnumerator(GridRowType.DataRow, this.ParcelSplitGrid.DisplayLayout.Bands[1], null))
                //{
                //    if ((currentRow.Cells["IsValue"].Value.Equals(true)))
                //    {
                //        currentRow.Cells["VSString1"].Appearance.BackColor = Color.FromArgb(255, 255, 255);
                //        currentRow.Cells["VSString1"].Appearance.BorderColor = Color.FromArgb(0, 0, 0);
                //        currentRow.Cells["VSString1"].Appearance.ForeColor = Color.Black;
                //        currentRow.Update();
                //    }
                //    else
                //    {
                //        currentRow.Cells["VSString1"].Appearance.BackColor = Color.FromArgb(77, 77, 77);
                //        currentRow.Cells["VSString1"].Appearance.BorderColor = Color.FromArgb(0, 0, 0);
                //        currentRow.Cells["VSString1"].Appearance.ForeColor = Color.White;
                //        currentRow.Update();
                //    }
                //}
            
            }
            
                if (this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListObjectSavedValue.TableName].Rows.Count == 0)
                {
                    for (int i = 3; i < this.parcelNumberSplitDataSet.Tables[0].Columns.Count; i++)
                    {
                        if (this.parcelNumberSplitDataSet.Tables[0].Columns[i].ColumnName.Substring(0, 7) == "IsValue")
                        {
                            for (int j = 0; j < this.parcelNumberSplitDataSet.Tables[0].Rows.Count; j++)
                            {
                                this.parcelNumberSplitDataSet.Tables[0].Rows[j][i] = Convert.ToBoolean(this.parcelSplitDataSet.ListSplitObject.Rows[j][this.parcelSplitDataSet.ListSplitObject.IsValueColumn.ColumnName]);
                               
                            }
                        }
                    }

                    for (int i = 3; i < this.parcelNumberSplitDataSet.Tables[1].Columns.Count; i++)
                    {
                        if (this.parcelNumberSplitDataSet.Tables[1].Columns[i].ColumnName.Substring(0, 7) == "IsValue")
                        {
                            for (int j = 0; j < this.parcelNumberSplitDataSet.Tables[1].Rows.Count; j++)
                            {
                                this.parcelNumberSplitDataSet.Tables[1].Rows[j][i] = Convert.ToBoolean(this.parcelSplitDataSet.ListSplitValuseSlice.Rows[j][this.parcelSplitDataSet.ListSplitValuseSlice.IsValueColumn.ColumnName]);
                               
                            }
                        }
                    }
                }

            
           



            if (this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListObjectSavedValue.TableName].Rows.Count > 0)
            {
                //// Getting values for valueslice
                for (int i = 0; i < this.parcelNumberSplitDataSet.Tables[1].Columns.Count; i++)
                {
                    if (this.parcelNumberSplitDataSet.Tables[1].Columns[i].ColumnName.Substring(0, 7) == "Checked")
                    {
                        for (int j = 0; j < this.parcelNumberSplitDataSet.Tables[1].Rows.Count; j++)
                        {
                            this.parcelNumberSplitDataSet.Tables[1].Rows[j][i] = Convert.ToBoolean(this.parcelSplitDataSet.ListValueSliceSavedValue.Rows[sliceCount][this.parcelSplitDataSet.ListValueSliceSavedValue.IsCheckedColumn.ColumnName]);
                            sliceCount++;
                        }
                    }
                }


                //for (int i = 0; i < this.parcelNumberSplitDataSet.Tables[1].Columns.Count; i++)
                //{
                //    if (this.parcelNumberSplitDataSet.Tables[1].Columns[i].ColumnName.Substring(0, 7) == "IsValue")
                //    {
                //        for (int j = 0; j < this.parcelNumberSplitDataSet.Tables[1].Rows.Count; j++)
                //        {
                //            this.parcelNumberSplitDataSet.Tables[1].Rows[j][i] = Convert.ToBoolean(this.parcelSplitDataSet.ListValueSliceSavedValue.Rows[sliceCount][this.parcelSplitDataSet.ListValueSliceSavedValue.IsValueColumn.ColumnName]);
                //            sliceCount++;
                //        }
                //    }
                //}

                #region Bug #6464: TSCO - 29500 Parcel Split - Object Check box re-checking itself after save

                ////Values for Objects
                for (int i = 0; i < this.parcelNumberSplitDataSet.Tables[0].Columns.Count; i++)
                {
                    if (this.parcelNumberSplitDataSet.Tables[0].Columns[i].ColumnName.Substring(0, 7) == "Checked")
                    {
                        for (int j = 0; j < this.parcelNumberSplitDataSet.Tables[0].Rows.Count; j++)
                        {
                            if (objectCount >= this.parcelSplitDataSet.ListObjectSavedValue.Rows.Count)
                            {

                            }
                            else
                            {
                                this.parcelNumberSplitDataSet.Tables[0].Rows[j][i] = Convert.ToBoolean(this.parcelSplitDataSet.ListObjectSavedValue.Rows[objectCount][this.parcelSplitDataSet.ListObjectSavedValue.IsCheckedColumn.ColumnName]);
                                objectCount++;
                            }
                        }
                    }
                }


                for (int i = 0; i < this.parcelNumberSplitDataSet.Tables[0].Columns.Count; i++)
                {
                    if (this.parcelNumberSplitDataSet.Tables[0].Columns[i].ColumnName.Substring(0, 7) == "IsValue")
                    {
                        for (int j = 0; j < this.parcelNumberSplitDataSet.Tables[0].Rows.Count; j++)
                        {
                            if (objectValueCount >= this.parcelSplitDataSet.ListObjectSavedValue.Rows.Count)
                            {

                            }
                            else
                            {
                                this.parcelNumberSplitDataSet.Tables[0].Rows[j][i] = Convert.ToBoolean(this.parcelSplitDataSet.ListObjectSavedValue.Rows[objectValueCount][this.parcelSplitDataSet.ListObjectSavedValue.IsValueColumn.ColumnName]);
                                objectValueCount++;
                            }
                        }
                    }
                }

                for (int i = 0; i < this.parcelNumberSplitDataSet.Tables[1].Columns.Count; i++)
                {
                    if (this.parcelNumberSplitDataSet.Tables[1].Columns[i].ColumnName.Substring(0, 7) == "IsValue")
                    {
                        for (int j = 0; j < this.parcelNumberSplitDataSet.Tables[1].Rows.Count; j++)
                        {
                            if (sliceValueCount >= this.parcelSplitDataSet.ListValueSliceSavedValue.Rows.Count)
                            {

                            }
                            else
                            {
                                this.parcelNumberSplitDataSet.Tables[1].Rows[j][i] = Convert.ToBoolean(this.parcelSplitDataSet.ListValueSliceSavedValue.Rows[sliceValueCount][this.parcelSplitDataSet.ListValueSliceSavedValue.IsValueColumn.ColumnName]);
                                sliceValueCount++;
                            }
                        }
                    }
                }

               

                #endregion Bug #6464: TSCO - 29500 Parcel Split - Object Check box re-checking itself after save

                if (this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListCropSavedValue.TableName].Rows.Count > 0)
                {
                    for (int i = 0; i < this.parcelNumberSplitDataSet.Tables[2].Columns.Count; i++)
                    {
                        if (this.parcelNumberSplitDataSet.Tables[2].Columns[i].ColumnName.Substring(0, 7) == "Checked")
                        {
                            for (int j = 0; j < this.parcelNumberSplitDataSet.Tables[2].Rows.Count; j++)
                            {
                                if (isCheckedCount >= this.parcelSplitDataSet.ListCropSavedValue.Rows.Count)
                                {
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(this.parcelSplitDataSet.ListCropSavedValue.Rows[isCheckedCount][this.parcelSplitDataSet.ListCropSavedValue.IsCheckedColumn.ColumnName].ToString()))
                                    {
                                        this.parcelNumberSplitDataSet.Tables[2].Rows[j][i] = Convert.ToBoolean(this.parcelSplitDataSet.ListCropSavedValue.Rows[isCheckedCount][this.parcelSplitDataSet.ListCropSavedValue.IsCheckedColumn.ColumnName]);
                                        isCheckedCount++;
                                    }
                                    else
                                    {
                                        this.parcelNumberSplitDataSet.Tables[2].Rows[j][i] = false;
                                        isCheckedCount++;
                                    }
                                }
                            }
                        }
                        if (this.parcelNumberSplitDataSet.Tables[2].Columns[i].ColumnName.Substring(0, 8) == "VSString")
                        {
                            for (int k = 0; k < this.parcelNumberSplitDataSet.Tables[2].Rows.Count; k++)
                            {
                                if (isUnitCount >= this.parcelSplitDataSet.ListCropSavedValue.Rows.Count)
                                {
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(this.parcelSplitDataSet.ListCropSavedValue.Rows[isUnitCount][this.parcelSplitDataSet.ListCropSavedValue.UnitsColumn.ColumnName].ToString()))
                                    {
                                        this.parcelNumberSplitDataSet.Tables[2].Rows[k][i] = this.parcelSplitDataSet.ListCropSavedValue.Rows[isUnitCount][this.parcelSplitDataSet.ListCropSavedValue.UnitsColumn.ColumnName];
                                        isUnitCount++;
                                    }
                                    else
                                    {
                                        isUnitCount++;
                                    }
                                    
                                }
                            }
                        }

                    }
                    
                }
            }

            for (int spiltCount = 2; spiltCount <= this.parcelNumber; spiltCount++)
            {
                foreach (UltraGridRow currentRow in this.ParcelSplitGrid.Rows.GetRowEnumerator(GridRowType.DataRow, this.ParcelSplitGrid.DisplayLayout.Bands[0], null))
                {
                    if ((currentRow.Cells["IsValue" + spiltCount.ToString()].Value.Equals(true)))
                    {
                        currentRow.Cells["ObjectString" + spiltCount.ToString()].Appearance.BackColor = Color.FromArgb(31, 65, 103);
                        currentRow.Cells["ObjectString" + spiltCount.ToString()].Appearance.BorderColor = Color.FromArgb(0, 0, 0);
                        currentRow.Cells["ObjectString" + spiltCount.ToString()].Appearance.ForeColor = Color.White;
                        currentRow.Cells["ObjectString" + spiltCount.ToString()].Appearance.FontData.SizeInPoints = 10;
                        currentRow.Cells["ObjectString" + spiltCount.ToString()].Appearance.FontData.Bold = DefaultableBoolean.True;
                        currentRow.Update();
                    }
                    else
                    {
                        currentRow.Cells["ObjectString" + spiltCount.ToString()].Appearance.BackColor = Color.FromArgb(77, 77, 77);
                        currentRow.Cells["ObjectString" + spiltCount.ToString()].Appearance.BorderColor = Color.FromArgb(0, 0, 0);
                        currentRow.Cells["ObjectString" + spiltCount.ToString()].Appearance.ForeColor = Color.White;
                        currentRow.Cells["ObjectString" + spiltCount.ToString()].Appearance.FontData.SizeInPoints = 10;
                        currentRow.Cells["ObjectString" + spiltCount.ToString()].Appearance.FontData.Bold = DefaultableBoolean.True;
                        currentRow.Cells["ObjectString" + spiltCount.ToString()].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                        currentRow.Update();
                    }
                }
            }
            for (int spiltCount = 2; spiltCount <= this.parcelNumber; spiltCount++)
            {
                foreach (UltraGridRow currentRow in this.ParcelSplitGrid.Rows.GetRowEnumerator(GridRowType.DataRow, this.ParcelSplitGrid.DisplayLayout.Bands[1], null))
                {
                    if ((currentRow.Cells["IsValue" + spiltCount.ToString()].Value.Equals(true)))
                    {
                        currentRow.Cells["VSString" + spiltCount.ToString()].Appearance.BackColor = Color.FromArgb(255, 255, 255);
                        currentRow.Cells["VSString" + spiltCount.ToString()].Appearance.BorderColor = Color.FromArgb(0, 0, 0);
                        currentRow.Cells["VSString" + spiltCount.ToString()].Appearance.ForeColor = Color.Black;
                        currentRow.Cells["VSString" + spiltCount.ToString()].Appearance.FontData.SizeInPoints = 8;
                        currentRow.Cells["VSString" + spiltCount.ToString()].Appearance.FontData.Bold = DefaultableBoolean.True;
                        currentRow.Update();
                    }
                    else
                    {
                        currentRow.Cells["VSString" + spiltCount.ToString()].Appearance.BackColor = Color.FromArgb(77, 77, 77);
                        currentRow.Cells["VSString" + spiltCount.ToString()].Appearance.BorderColor = Color.FromArgb(0, 0, 0);
                        currentRow.Cells["VSString" + spiltCount.ToString()].Appearance.ForeColor = Color.White;
                        currentRow.Cells["VSString" + spiltCount.ToString()].Appearance.FontData.SizeInPoints = 8;
                        currentRow.Cells["VSString" + spiltCount.ToString()].Appearance.FontData.Bold = DefaultableBoolean.True;
                        currentRow.Cells["VSString" + spiltCount.ToString()].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                        currentRow.Update();
                    }
                }
            }
            
            int tempCount = 0;
            int tempparcelId = 0;
            bool nextColumn = false;
            DataTable tempCropData = this.parcelSplitDataSet.ListCropSavedValue.Copy();
            tempCropData.Columns.Add("Added");
          //  if (this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListCropSavedValue.TableName].Rows.Count > 0)
            if (this.baseParcelDataSet.Tables["ListSplitCrop"].Rows.Count > 0)                   // if (this.baseParcelDataSet.Tables["ListSplitCrop"].Rows.Count > 0)
            {
                for (int i = 0; i < this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Columns.Count; i++)
                {
                    if (this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Columns[i].ColumnName.Substring(0, 7) == "Checked")
                    {
                        for (int j = 0; j < this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows.Count; j++)
                        {
                            if (this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows[j]["SliceTypeID"].ToString() == "30")
                            {
                                var num = "1";
                               // this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows[j][i] = true;
                                var columnName = this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Columns[i].ColumnName;
                                if (columnName.Contains("Checked"))
                                {
                                    if (columnName.Length >= 8)
                                    {
                                        var array = columnName.Split('d');
                                        if (array[1].Length != null && (array[1].Length > 0))
                                        {
                                            num = array[1];
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows[j]["Checked" + num].ToString()))
                                    {
                                        this.parcelNumberSplitDataSet.Tables[2].Rows[j][i] = Convert.ToBoolean(this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows[j]["Checked" + num]);
                                        sliceCount++;
                                    }
                                    else
                                    {
                                        this.parcelNumberSplitDataSet.Tables[2].Rows[j][i] = Convert.ToBoolean("False");
                                    }

                                    foreach (UltraGridRow currentRow in this.ParcelSplitGrid.Rows.GetRowEnumerator(GridRowType.DataRow, this.ParcelSplitGrid.DisplayLayout.Bands[2], null))
                                    {
                                        if ((currentRow.Cells["SliceTypeID"].Value.ToString().Equals("30")))
                                        {
                                            currentRow.Cells["Checked" + num].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
                                          //  currentRow.Cells["Checked" + num].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                                            currentRow.Cells["VSString" + num].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;                                            
                                            currentRow.Update();
                                        }
                                        else
                                        {
                                            currentRow.Cells["Checked" + num].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
                                            currentRow.Cells["Checked" + num].SetValue(DBNull.Value, false);
                                            currentRow.Cells["Checked" + num].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                                            currentRow.Cells["VSString" + num].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                                            currentRow.Update();
                                        }
                                    }
                                }

                            }
                            else
                            {
                                // this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows[j][i] = false;
                                var num = "1";
                                var columnName = this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Columns[i].ColumnName;
                                if (columnName.Contains("Checked"))
                                {
                                    if (columnName.Length >= 8)
                                    {
                                        var array = columnName.Split('d');
                                        if (array[1].Length != null && (array[1].Length > 0))
                                        {
                                            num = array[1];
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows[j]["Checked" + num].ToString()))
                                    {
                                        this.parcelNumberSplitDataSet.Tables[2].Rows[j][i] = Convert.ToBoolean(this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows[j]["Checked" + num]);

                                    }
                                    else
                                    {
                                        this.parcelNumberSplitDataSet.Tables[2].Rows[j][i] = Convert.ToBoolean("False");
                                    }
                                    foreach (UltraGridRow current in this.ParcelSplitGrid.Rows.GetRowEnumerator(GridRowType.DataRow, this.ParcelSplitGrid.DisplayLayout.Bands[2], null))
                                    {
                                        if ((!current.Cells["SliceTypeID"].Value.ToString().Equals("30")) && (current.Cells["Checked" + num].Value.ToString().ToLower().Equals("true")))
                                        {
                                            current.Cells["Checked" + num].Value = false;
                                        }
                                        if (!current.Cells["SliceTypeID"].Value.ToString().Equals("30"))
                                        {
                                            current.Cells["Checked" + num].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
                                            current.Cells["Checked" + num].SetValue(DBNull.Value, false);
                                            current.Cells["Checked" + num].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                                            current.Cells["VSString" + num].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                                            current.Update();
                                        }
                                        else
                                        {
                                            current.Cells["Checked" + num].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
                                           // current.Cells["Checked" + num].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                                            current.Cells["VSString" + num].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                                            current.Update();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //if (this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Columns[i].ColumnName.Substring(0, 8) == "VSString")
                    //{
                    //    for (int k = 0; k < tempCropData.Rows.Count; k++)
                    //    {
                    //        nextColumn = false;
                    //        if (string.IsNullOrEmpty(tempCropData.Rows[k]["Added"].ToString()))
                    //        {
                    //            for (int j = 0; j < this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows.Count; j++)
                    //            {
                    //                int parcelId = Convert.ToInt32(this.parcelSplitDataSet.ListCropSavedValue.Rows[k]["ParcelID"].ToString());

                    //                if (this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows[j]["ValueSliceID"].ToString() == tempCropData.Rows[k]["ValueSliceID"].ToString() && this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows[j]["ComponentID"].ToString() == tempCropData.Rows[k]["SplitComponentID"].ToString())
                    //                {
                    //                    //// this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows[j][i] = this.parcelSplitDataSet.ListCropSavedValue.Rows[k][this.parcelSplitDataSet.ListCropSavedValue.UnitsColumn.ColumnName].ToString();
                    //                    this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows[j][i] = tempCropData.Rows[k]["Units"].ToString();
                    //                    tempCropData.Rows[k]["Added"] = "True";

                    //                    if (k + 1 < tempCropData.Rows.Count)
                    //                    {
                    //                        tempparcelId = Convert.ToInt32(tempCropData.Rows[k + 1]["ParcelID"].ToString());
                    //                    }

                    //                    if (parcelId != tempparcelId)
                    //                    {
                    //                        nextColumn = true;
                    //                    }

                    //                    break;
                    //                }
                    //            }
                    //        }

                    //        if (nextColumn == true)
                    //        {
                    //            break;
                    //        }
                    //    }
                    //}

                }
            }
           
            this.ParcelSplitGrid.DisplayLayout.Bands[0].Groups[this.currentParcel].Header.Appearance.BackColor = Color.FromArgb(119, 47, 40);
            this.CreatedSplitParcelHeaderRow();
        }


        private void MySplitParcel()
        {
            DataColumn objectCheckedColumn;
            DataColumn valueCheckedColumn;
            DataColumn cropCheckedColumn;
            int sliceCount = 0;
           
            int objectCount = 0;
            int objectValueCount = 0;
            int cropCount = 0;
            int Count = 0;
            for (int spiltCount = 2; spiltCount <= this.parcelNumber; spiltCount++)
            {
                objectCheckedColumn = new DataColumn();
                objectCheckedColumn.DataType = System.Type.GetType("System.Boolean");
                objectCheckedColumn.ColumnName = "Checked" + spiltCount.ToString();
                objectCheckedColumn.DefaultValue = true;
                this.parcelNumberSplitDataSet.Tables[0].Columns.Add(objectCheckedColumn);
                this.parcelNumberSplitDataSet.Tables[0].Columns.Add(new DataColumn("ObjectString" + spiltCount.ToString(), System.Type.GetType("System.String")));

                valueCheckedColumn = new DataColumn();
                valueCheckedColumn.DataType = System.Type.GetType("System.Boolean");
                valueCheckedColumn.ColumnName = "Checked" + spiltCount.ToString();
                valueCheckedColumn.DefaultValue = true;
                this.parcelNumberSplitDataSet.Tables[1].Columns.Add(valueCheckedColumn);
                this.parcelNumberSplitDataSet.Tables[1].Columns.Add(new DataColumn("VSString" + spiltCount.ToString(), System.Type.GetType("System.String")));

                cropCheckedColumn = new DataColumn();
                cropCheckedColumn.DataType = System.Type.GetType("System.Boolean");
                //cropCheckedColumn.DataType = typeof(object);
                cropCheckedColumn.ColumnName = "Checked" + spiltCount.ToString();
                // cropCheckedColumn.DefaultValue = true;
                this.parcelNumberSplitDataSet.Tables[2].Columns.Add(cropCheckedColumn);
                this.parcelNumberSplitDataSet.Tables[2].Columns.Add(new DataColumn("VSString" + spiltCount.ToString(), System.Type.GetType("System.String")));
                this.parcelNumberSplitDataSet.Tables[2].Columns["VSString" + spiltCount.ToString()].MaxLength = 15;

                string groupName = "Group" + spiltCount.ToString();
                string headerName = "Parcel " + spiltCount.ToString();
                this.splitParcelGroup = this.splitParcelBand.Groups.Add(groupName, headerName);
                this.splitParcelGroup.Columns.Add(this.splitParcelBand.Columns["Checked" + spiltCount.ToString()]);
                this.splitParcelGroup.Columns.Add(this.splitParcelBand.Columns["ObjectString" + spiltCount.ToString()]);

                //// Column sizing
                this.ParcelSplitGrid.DisplayLayout.Bands[0].Override.HeaderAppearance.BackColor = Color.FromArgb(22, 52, 92);
                this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns["Checked" + spiltCount.ToString()].Width = 33;
                this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns["ObjectString" + spiltCount.ToString()].Width = 53;
                this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns["Checked" + spiltCount.ToString()].Width = 33;
                this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns["VSString" + spiltCount.ToString()].Width = 53;
                this.ParcelSplitGrid.DisplayLayout.Bands[2].Columns["Checked" + spiltCount.ToString()].Width = 33;
                this.ParcelSplitGrid.DisplayLayout.Bands[2].Columns["VSString" + spiltCount.ToString()].Width = 53;

                this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns["Checked" + spiltCount.ToString()].CellActivation = Activation.AllowEdit;
                this.ParcelSplitGrid.DisplayLayout.Bands[0].Columns["ObjectString" + spiltCount.ToString()].CellActivation = Activation.NoEdit;
                this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns["Checked" + spiltCount.ToString()].CellActivation = Activation.AllowEdit;
                this.ParcelSplitGrid.DisplayLayout.Bands[1].Columns["VSString" + spiltCount.ToString()].CellActivation = Activation.NoEdit;
                this.ParcelSplitGrid.DisplayLayout.Bands[2].Columns["Checked" + spiltCount.ToString()].CellActivation = Activation.AllowEdit;
                this.ParcelSplitGrid.DisplayLayout.Bands[2].Columns["VSString" + spiltCount.ToString()].CellActivation = Activation.AllowEdit;

            }

            if (this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListObjectSavedValue.TableName].Rows.Count > 0)
            {
                //// Getting values for valueslice
                for (int i = 0; i < this.parcelNumberSplitDataSet.Tables[1].Columns.Count; i++)
                {
                    if (this.parcelNumberSplitDataSet.Tables[1].Columns[i].ColumnName.Substring(0, 7) == "Checked")
                    {
                        for (int j = 0; j < this.parcelNumberSplitDataSet.Tables[1].Rows.Count; j++)
                        {
                            this.parcelNumberSplitDataSet.Tables[1].Rows[j][i] = Convert.ToBoolean(this.parcelSplitDataSet.ListValueSliceSavedValue.Rows[sliceCount][this.parcelSplitDataSet.ListValueSliceSavedValue.IsCheckedColumn.ColumnName]);
                            sliceCount++;
                        }
                    }
                }

                #region Bug #6464: TSCO - 29500 Parcel Split - Object Check box re-checking itself after save

                ////Values for Objects
                for (int i = 0; i < this.parcelNumberSplitDataSet.Tables[0].Columns.Count; i++)
                {
                    if (this.parcelNumberSplitDataSet.Tables[0].Columns[i].ColumnName.Substring(0, 7) == "Checked")
                    {
                        for (int j = 0; j < this.parcelNumberSplitDataSet.Tables[0].Rows.Count; j++)
                        {
                            this.parcelNumberSplitDataSet.Tables[0].Rows[j][i] = Convert.ToBoolean(this.parcelSplitDataSet.ListObjectSavedValue.Rows[objectCount][this.parcelSplitDataSet.ListObjectSavedValue.IsCheckedColumn.ColumnName]);
                            objectCount++;
                        }
                    }
                }
                #endregion Bug #6464: TSCO - 29500 Parcel Split - Object Check box re-checking itself after save
            }

            int tempCount = 0;
            int tempparcelId = 0;

            bool nextColumn = false;
            DataTable tempCropData = this.parcelSplitDataSet.ListCropSavedValue.Copy();
            tempCropData.Columns.Add("Added");
            //  if (this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListCropSavedValue.TableName].Rows.Count > 0)
            if (this.baseParcelDataSet.Tables["ListSplitCrop"].Rows.Count > 0)                   // if (this.baseParcelDataSet.Tables["ListSplitCrop"].Rows.Count > 0)
            {
                for (int i = 0; i < this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Columns.Count; i++)
                {
                    if (this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Columns[i].ColumnName.Substring(0, 7) == "Checked")
                    {
                        for (int j = 0; j < this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows.Count; j++)
                        {
                            if (this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows[j]["SliceTypeID"].ToString() == "30")
                            {
                                var num = "1";
                                this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows[j][i] = true;
                                var columnName = this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Columns[i].ColumnName;
                                if (columnName.Contains("Checked"))
                                {
                                    if (columnName.Length >= 8)
                                    {
                                        var array = columnName.Split('d');
                                        if (array[1].Length != null && (array[1].Length > 0))
                                        {
                                            num = array[1];
                                        }
                                    }
                                    foreach (UltraGridRow currentRow in this.ParcelSplitGrid.Rows.GetRowEnumerator(GridRowType.DataRow, this.ParcelSplitGrid.DisplayLayout.Bands[2], null))
                                    {
                                        if (currentRow.Cells["Checked" + num].Value.ToString().ToLower() == "true")
                                        {
                                            currentRow.Cells["Checked" + num].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                                            currentRow.Update();
                                            this.ParcelSplitGrid.UpdateData();
                                            //this.ParcelSplitGrid.UpdateMode = UpdateMode.OnUpdate;
                                        }
                                    }
                                }
                                cropCount++;
                            }
                            else
                            {
                                this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows[j][i] = false;
                                var num = "1";
                                var columnName = this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Columns[i].ColumnName;
                                if (columnName.Contains("Checked"))
                                {
                                    if (columnName.Length >= 8)
                                    {
                                        var array = columnName.Split('d');
                                        if (array[1].Length != null && (array[1].Length > 0))
                                        {
                                            num = array[1];
                                        }
                                    }
                                    foreach (UltraGridRow current in this.ParcelSplitGrid.Rows.GetRowEnumerator(GridRowType.DataRow, this.ParcelSplitGrid.DisplayLayout.Bands[2], null))
                                    {
                                        if (current.Cells["Checked" + num].Value.ToString().ToLower() == "false")
                                        {
                                            current.Cells["Checked" + num].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
                                            current.Cells["Checked" + num].SetValue(DBNull.Value, false);
                                            current.Update();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Columns[i].ColumnName.Substring(0, 8) == "VSString")
                    {
                        for (int k = 0; k < tempCropData.Rows.Count; k++)
                        {
                            nextColumn = false;
                            if (string.IsNullOrEmpty(tempCropData.Rows[k]["Added"].ToString()))
                            {
                                for (int j = 0; j < this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows.Count; j++)
                                {
                                    int parcelId = Convert.ToInt32(this.parcelSplitDataSet.ListCropSavedValue.Rows[k]["ParcelID"].ToString());

                                    if (this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows[j]["ValueSliceID"].ToString() == tempCropData.Rows[k]["ValueSliceID"].ToString() && this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows[j]["ComponentID"].ToString() == tempCropData.Rows[k]["SplitComponentID"].ToString())
                                    {
                                        //// this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows[j][i] = this.parcelSplitDataSet.ListCropSavedValue.Rows[k][this.parcelSplitDataSet.ListCropSavedValue.UnitsColumn.ColumnName].ToString();
                                        this.parcelNumberSplitDataSet.Tables["ListSplitCrop"].Rows[j][i] = tempCropData.Rows[k]["Units"].ToString();
                                        tempCropData.Rows[k]["Added"] = "True";

                                        if (k + 1 < tempCropData.Rows.Count)
                                        {
                                            tempparcelId = Convert.ToInt32(tempCropData.Rows[k + 1]["ParcelID"].ToString());
                                        }

                                        if (parcelId != tempparcelId)
                                        {
                                            nextColumn = true;
                                        }

                                        break;
                                    }
                                }
                            }

                            if (nextColumn == true)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            this.ParcelSplitGrid.DisplayLayout.Bands[0].Groups[this.currentParcel].Header.Appearance.BackColor = Color.FromArgb(119, 47, 40);
            this.CreatedSplitParcelHeaderRow();
        }


        /// <summary>
        /// Createds the split parcel header row.
        /// </summary>
        private void CreatedSplitParcelHeaderRow()
        {
            for (int splitHeaderCount = 0; splitHeaderCount < this.parcelNumber; splitHeaderCount++)
            {
                DataRow splitHeaderRow = this.splitParcelHeaderDataTable.NewRow();
                this.splitParcelHeaderDataTable.Rows.Add(splitHeaderRow);

                this.splitParcelHeaderDataTable.Rows[splitHeaderCount][this.parcelSplitDataSet.ListSplitHeaderDetail.SplitParcelIDColumn.ColumnName] = splitHeaderCount + 1;
                this.splitParcelHeaderDataTable.Rows[splitHeaderCount][this.parcelSplitDataSet.ListSplitHeaderDetail.IsAttachmentColumn.ColumnName] = "true";
                this.splitParcelHeaderDataTable.Rows[splitHeaderCount][this.parcelSplitDataSet.ListSplitHeaderDetail.IsCommentColumn.ColumnName] = "true";
                this.splitParcelHeaderDataTable.Rows[splitHeaderCount][this.parcelSplitDataSet.ListSplitHeaderDetail.IsAssociationColumn.ColumnName] = "true";
                this.splitParcelHeaderDataTable.Rows[splitHeaderCount][this.parcelSplitDataSet.ListSplitHeaderDetail.IsPermitColumn.ColumnName] = "true";
                /* Modified to Implement TFS# 21784 */
                if (this.DetailsCheckBox.Checked && this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows.Count > 0)
                {
                    //Modifed tox Implement Class code
                    /* Modified to Implement TFS# 21784 */
                    if (this.parcelSplitDataSet.ListSplitHeaderDetail.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(this.stateConfigured) && this.stateConfigured.ToUpper().Equals("NE"))
                        {
                            this.splitParcelHeaderDataTable.Rows[splitHeaderCount][this.parcelSplitDataSet.ListSplitHeaderDetail.ClassCodeColumn.ColumnName] = this.parcelSplitDataSet.ListSplitHeaderDetail.Rows[0][this.parcelSplitDataSet.ListSplitHeaderDetail.ClassCodeColumn.ColumnName];
                            this.splitParcelHeaderDataTable.Rows[splitHeaderCount][this.parcelSplitDataSet.ListSplitHeaderDetail.ClassCodeRGBColumn.ColumnName] = this.parcelSplitDataSet.ListSplitHeaderDetail.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.ClassCodeRGBColumn.ColumnName];
                        }
                        else
                        {
                            this.splitParcelHeaderDataTable.Rows[splitHeaderCount][this.parcelSplitDataSet.ListSplitHeaderDetail.DORColumn.ColumnName] = this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.DORColumn.ColumnName];
                        }
                    }

                    if (!string.IsNullOrEmpty(this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.ID1MaskColumn.ColumnName].ToString()))
                    {
                        this.splitParcelHeaderDataTable.Rows[splitHeaderCount][this.parcelSplitDataSet.ListSplitHeaderDetail.ID1Column.ColumnName] = this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.ID1MaskColumn.ColumnName];
                        this.SplitId1Label.Text = this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.ID1MaskColumn.ColumnName].ToString();
                        //this.label3.Text = this.parcelSplitDataSet.ListSplitDefinitionHeader.ID1MaskColumn.ColumnName.ToString();
                        this.ID2Label.Text = this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.ID2MaskColumn.ColumnName].ToString();
                    }
                    else
                    {
                        this.splitParcelHeaderDataTable.Rows[splitHeaderCount][this.parcelSplitDataSet.ListSplitHeaderDetail.ID1Column.ColumnName] = this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.ID1MaskColumn.ColumnName];
                        this.SplitId1Label.Text = this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.ID1LabelColumn.ColumnName].ToString();
                        this.ID2Label.Text = this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.ID2LabelColumn.ColumnName].ToString();
                    }

                }

                if (this.parcelSplitDataSet.ListSplitHeaderDetail.Rows.Count > 0)
                {
                    this.splitParcelHeaderDataTable.Rows[splitHeaderCount][this.parcelSplitDataSet.ListSplitHeaderDetail.LegalColumn.ColumnName] = this.parcelSplitDataSet.ListSplitHeaderDetail.Rows[0][this.parcelSplitDataSet.ListSplitHeaderDetail.LegalColumn.ColumnName];
                    this.splitParcelHeaderDataTable.Rows[splitHeaderCount][this.parcelSplitDataSet.ListSplitHeaderDetail.SitusColumn.ColumnName] = this.parcelSplitDataSet.ListSplitHeaderDetail.Rows[0][this.parcelSplitDataSet.ListSplitHeaderDetail.SitusColumn.ColumnName];
                    this.splitParcelHeaderDataTable.Rows[splitHeaderCount][this.parcelSplitDataSet.ListSplitHeaderDetail.ID2Column.ColumnName] = this.parcelSplitDataSet.ListSplitHeaderDetail.Rows[0][this.parcelSplitDataSet.ListSplitHeaderDetail.ID2Column.ColumnName];
                }
            }
            
        }

        /// <summary>
        /// Bingings the split parcel header.
        /// </summary>
        /// <param name="parcelId">Parcel ID</param>
        private void BingingSplitParcelHeader(int parcelId)
        {
            this.ClearSplitParcelHeader();

            if (this.splitParcelHeaderDataTable.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.ParcelNumberColumn.ColumnName].ToString()))
                {
                    this.SplitParcelNumberTextBox.Text = "< Default >";
                    this.SplitParcelNumberTextBox.ForeColor = Color.FromArgb(115, 115, 115);
                }
                else
                {
                    this.SplitParcelNumberTextBox.Text = this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.ParcelNumberColumn.ColumnName].ToString();
                    this.SplitParcelNumberTextBox.ForeColor = Color.Black;
                }

                this.SplitId1TextBox.Text = this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.ID1Column.ColumnName].ToString();

                //COmmented by purushotham 

                //if (string.IsNullOrEmpty(this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.DORColumn.ColumnName].ToString()))
                //{
                //    this.SplitDorCodeTextBox.Text = "< Select >";
                //    this.SplitDorCodeLinkLabel.Visible = false;
                //    this.SplitDorCodeTextBox.ForeColor = Color.FromArgb(115, 115, 115);
                //}
                //else
                //{
                //    this.SplitDorCodeLinkLabel.Text = this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.DORColumn.ColumnName].ToString();
                //    this.SplitDorCodeLinkLabel.BringToFront();
                //    this.SplitDorCodeTextBox.Visible = false;
                //    ////this.SplitDorCodeTextBox.Text = this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.DORColumn.ColumnName].ToString();
                //    ////this.SplitDorCodeTextBox.ForeColor = Color.Black;
                //}
                if (!string.IsNullOrEmpty(this.stateConfigured) && this.stateConfigured.ToUpper().Equals("NE"))
                {                    
                    this.SplitDorCodePanel.Visible = false;
                    this.SplitDorCodePanel.SendToBack();
                    this.ClassCodePanel.Visible = true;
                    this.ClassCodePanel.Enabled = true;
                    this.ClassCodePanel.BringToFront();
                    string ClassCodeField = string.Empty;
                    if (!string.IsNullOrEmpty(this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.ClassCodeColumn.ColumnName].ToString()))
                    {
                        ClassCodeField=this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.ClassCodeColumn.ColumnName].ToString();
                        ClassCodeField = ClassCodeField.Replace(" ", "");
                        StringBuilder sb = new StringBuilder();
                        if (!string.IsNullOrEmpty(ClassCodeField))
                        {
                            List<string> result = new List<string>(Regex.Split(ClassCodeField, @"(?<=\G.{2})", RegexOptions.Singleline));
                            var count = result.Count;
                            for (int i = 0; i < count; i++)
                            {
                                if (result[i].Length.Equals(2))
                                {
                                    if (sb.ToString().Length < 17)
                                    {
                                        string temp1 = result[i].Insert(2, " ").ToString();

                                        sb.Append(temp1);
                                    }

                                }
                                else
                                {
                                    if (sb.ToString().Length < 17)
                                    {
                                        sb.Append(result[i].ToString());
                                    }
                                }

                            }
                            ClassCodeField = sb.ToString();

                        }

                        this.ClassCodeComboBox.Text = ClassCodeField;
                        this.ClassCodeTextBox.Text = ClassCodeField;
                        string temp = (this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.ClassCodeRGBColumn.ColumnName].ToString());
                        string[] array = temp.Split(',');
                        if (array.Length > 0)
                        {
                            int R = 0;
                            int G = 0;
                            int B = 0;
                            if (array[0].Length > 0)
                            {
                                R = Convert.ToInt32(array[0]);
                            }
                            if (array[1].Length > 0)
                            {
                                G = Convert.ToInt32(array[1]);
                            }
                            if (array[2].Length > 0)
                            {
                                B = Convert.ToInt32(array[2]);
                            }
                            /* Modified to Implement TFS# 21784 Fixed */
                            foreColor = Color.FromArgb(R, G, B);
                            /*end*/
                            if (foreColor != null)
                            {
                                this.ClassCodeComboBox.ForeColor = foreColor;
                            }
                        }
                    }
                    else
                    {
                        this.ClassCodeTextBox.Text = ClassCodeField;
                        this.ClassCodeComboBox.Text = ClassCodeField;
                    }
                }
                else
                {
                    this.SplitDorCodePanel.Visible = true;
                    this.SplitDorCodePanel.BringToFront();
                    this.SplitDorCodePanel.Enabled = true;

                    if (string.IsNullOrEmpty(this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.DORColumn.ColumnName].ToString()))
                    {
                        this.SplitDorCodeTextBox.Text = "< Select >";
                        this.SplitDorCodeLinkLabel.Visible = false;
                        this.SplitDorCodeTextBox.ForeColor = Color.FromArgb(115, 115, 115);
                    }
                    else
                    {
                        this.SplitDorCodeLinkLabel.Text = this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.DORColumn.ColumnName].ToString();
                        this.SplitDorCodeLinkLabel.BringToFront();
                        this.SplitDorCodeTextBox.Visible = false;
                        ////this.SplitDorCodeTextBox.Text = this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.DORColumn.ColumnName].ToString();
                        ////this.SplitDorCodeTextBox.ForeColor = Color.Black;
                    }

                }
              
                if (this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.IsAttachmentColumn.ColumnName].ToString().ToLower().Equals("true"))
                {
                    this.SplitAttachmentCheckBox.Checked = true;
                }
                else
                {
                    this.SplitAttachmentCheckBox.Checked = false;
                }

                if (this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.IsCommentColumn.ColumnName].ToString().ToLower().Equals("true"))
                {
                    this.SplitCommentsCheckBox.Checked = true;
                }
                else
                {
                    this.SplitCommentsCheckBox.Checked = false;
                }

                if (this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.IsPermitColumn.ColumnName].ToString().ToLower().Equals("true"))
                {
                    this.SplitPermitCheckBox.Checked = true;
                }
                else
                {
                    this.SplitPermitCheckBox.Checked = false;
                }

                if (this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.IsAssociationColumn.ColumnName].ToString().ToLower().Equals("true"))
                {
                    this.SplitAssocCheckBox.Checked = true;
                }
                else
                {
                    this.SplitAssocCheckBox.Checked = false;
                }

                this.LegalLabelTextBox.Text = this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.LegalColumn.ColumnName].ToString();
                this.situsLabelTextBox.Text = this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.SitusColumn.ColumnName].ToString();

                ////Reverted Link label for situs purushotham.a

                if (!string.IsNullOrEmpty(this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.SitusColumn.ColumnName].ToString()))
                {
                    this.SitusLinkLabel.Text = this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.SitusColumn.ColumnName].ToString();
                    this.situsLabelTextBox.Visible = false;
                    this.SitusLinkLabel.Visible = true;
                   // this.SitusLinkLabel.BringToFront();
                }
                else
                {
                    ////Reverted by purushotham
                    this.SitusLinkLabel.Text = "««  »»";
                    this.situsLabelTextBox.Visible = false;
                    this.SitusLinkLabel.Visible = true;
                    // this.situsLabelTextBox.Visible = true;
                }
                this.Id2TextBox.Text = this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.ID2Column.ColumnName].ToString();
            }
        }

        /// <summary>
        /// Gets the split header detail.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        private void GetSplitHeaderDetail(int parcelId)
        {
            if (this.splitParcelHeaderDataTable.Rows.Count > 0)
            {
                if (this.SplitParcelNumberTextBox.Text.Trim() != "< Default >")
                {
                    this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.ParcelNumberColumn.ColumnName] = this.SplitParcelNumberTextBox.Text.Trim();
                }
                else
                {
                    this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.ParcelNumberColumn.ColumnName] = string.Empty;
                }

                this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.ID1Column.ColumnName] = this.SplitId1TextBox.Text.Trim();
                if(!string.IsNullOrEmpty(this.stateConfigured) && this.stateConfigured.ToUpper().Equals("NE"))
                {
                    this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.ClassCodeColumn.ColumnName] = this.ClassCodeComboBox.Text.Trim();
                    //if (string.IsNullOrEmpty(this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.DORColumn.ColumnName].ToString()))
                    //{
                    //    this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.DORColumn.ColumnName] = "««  »»";
                    //}
                }
                else
                {

                this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.DORColumn.ColumnName] = this.SplitDorCodeLinkLabel.Text.Trim();
                }

                if (this.SplitAttachmentCheckBox.Checked)
                {
                    this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.IsAttachmentColumn.ColumnName] = "true";
                }
                else
                {
                    this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.IsAttachmentColumn.ColumnName] = "false";
                }

                if (this.SplitCommentsCheckBox.Checked)
                {
                    this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.IsCommentColumn.ColumnName] = "true";
                }
                else
                {
                    this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.IsCommentColumn.ColumnName] = "false";
                }

                if (this.SplitAssocCheckBox.Checked)
                {
                    this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.IsAssociationColumn.ColumnName] = "true";
                }
                else
                {
                    this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.IsAssociationColumn.ColumnName] = "false";
                }

                if (this.SplitPermitCheckBox.Checked)
                {
                    this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.IsPermitColumn.ColumnName] = "true";
                }
                else
                {
                    this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.IsPermitColumn.ColumnName] = "false";
                }
                this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.LegalColumn.ColumnName] = this.LegalLabelTextBox.Text.Trim();
                this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.SitusColumn.ColumnName] = this.SitusLinkLabel.Text.Trim();//this.situsLabelTextBox.Text.Trim();//this.situsLabelTextBox.Text.Trim();//this.SitusLinkLabel.Text; //this.situsLabelTextBox.Text.Trim();
                ////Reverted for Situs link label by purushotham.A
                if (!string.IsNullOrEmpty(this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.SitusColumn.ColumnName].ToString()))
                {
                    this.SitusLinkLabel.Text = this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.SitusColumn.ColumnName].ToString();
                    this.situsLabelTextBox.Visible = false;
                    this.SitusLinkLabel.Visible = true;
                }
                else
                {
                    this.SitusLinkLabel.Text = "««  »»";
                    this.situsLabelTextBox.Visible = false;
                    this.SitusLinkLabel.Visible = true;
                }
                this.splitParcelHeaderDataTable.Rows[parcelId][this.parcelSplitDataSet.ListSplitHeaderDetail.ID2Column.ColumnName] = this.Id2TextBox.Text.Trim();

                this.splitParcelHeaderDataTable.AcceptChanges();
            }
        }

        /// <summary>
        /// Clears the split parcel header.
        /// </summary>
        private void ClearSplitParcelHeader()
        {
            this.SplitParcelNumberTextBox.Text = string.Empty;
            this.SplitId1TextBox.Text = string.Empty;
            this.SplitDorCodeTextBox.Text = string.Empty;
            this.SplitAttachmentCheckBox.Checked = true;
            this.SplitPermitCheckBox.Checked = true;
            this.SplitAssocCheckBox.Checked = true;
            this.SplitCommentsCheckBox.Checked = true;
            this.SplitDorCodeLinkLabel.Text = string.Empty;
        }

        /// <summary>
        /// Gets the split definition XML.
        /// </summary>
        /// <returns>string</returns>
        private string GetSplitDefinitionXml()
        {
            this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.NumResultingParcelsColumn.ColumnName] = this.ParcelSplitTextBox.Text.Trim();
            //this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.NumResultingParcelsColumn.ColumnName] = this.ParcelSplitTextBox.Text.Trim();
            try
            {
                ////this.parcelSplitDataSet.ListSplitDefinitionHeader.SplitIDColumn.ReadOnly = false;
                //// this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.SplitIDColumn.ColumnName] = this.splitId;
            }
            catch (Exception ex)
            {
            }
            /////this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.SplitIDColumn.ColumnName] = this.splitId;

            if (this.DetailsCheckBox.Checked)
            {
                this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.IsDetailsColumn.ColumnName] = "true";
            }
            else
            {
                this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.IsDetailsColumn.ColumnName] = "false";
            }

            if (this.AttachmentsCheckBox.Checked)
            {
                this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.IsAttachmentsColumn.ColumnName] = "true";
            }
            else
            {
                this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.IsAttachmentsColumn.ColumnName] = "false";
            }

            if (this.CommentsCheckBox.Checked)
            {
                this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.IsCommentsColumn.ColumnName] = "true";
            }
            else
            {
                this.parcelSplitDataSet.ListSplitDefinitionHeader.Rows[0][this.parcelSplitDataSet.ListSplitDefinitionHeader.IsCommentsColumn.ColumnName] = "false";
            }



            //if (this.SplitPermitCheckBox.Checked)
            //{
            //    this.parcelSplitDataSet.ListSplitHeaderDetail.Rows[0][this.parcelSplitDataSet.ListSplitHeaderDetail.IsPermitColumn.ColumnName] = "true";
            //}
            //else
            //{
            //    this.parcelSplitDataSet.ListSplitHeaderDetail.Rows[0][this.parcelSplitDataSet.ListSplitHeaderDetail.IsPermitColumn.ColumnName] = "false";
            //}


            //if (this.SplitAssocCheckBox.Checked)
            //{
            //    this.parcelSplitDataSet.ListSplitHeaderDetail.Rows[0][this.parcelSplitDataSet.ListSplitHeaderDetail.IsAssociationColumn.ColumnName] = "true";
            //}
            //else
            //{
            //    this.parcelSplitDataSet.ListSplitHeaderDetail.Rows[0][this.parcelSplitDataSet.ListSplitHeaderDetail.IsAssociationColumn.ColumnName] = "false";
            //}
            return TerraScanCommon.GetXmlString(this.parcelSplitDataSet.ListSplitDefinitionHeader);
        }

        #region SetSmartpartHeight

        /// <summary>
        /// Sets the height of the form load.
        /// </summary>
        private void SetFormLoadHeight()
        {
            SliceResize sliceResize;
            sliceResize.MasterFormNo = 24500;
            sliceResize.SliceFormName = "D24500.F29500";
            sliceResize.SliceFormHeight = 455;
            this.ParcelSplitPictureBox.Height = 455;
            this.SplitParcelPanel.Height = 455 - this.AttachmentsPanel.Height;
            this.MainParcelSplitpanel.Height = 455;
            this.sliceHeight = 455;
            this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            this.ParcelSplitPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ParcelSplitPictureBox.Height, this.ParcelSplitPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            this.Height = this.sliceHeight;
            this.Show();
        }

        /// <summary>
        /// Grids the resize.
        /// </summary>
        private void SetSmartpartHeight()
        {
            int tempChildHeight;
            int tempParentHeight;
            int childRowHeight, parentRowCount, childRowCount, parentRowHeight;
            int totalRowCount;
            int extraHeight;
            int tempCropRowHeight;
            int cropRowHeight, cropRowCount;

            ////childRowHeight = this.BaseParcelGrid.Rows[0].ChildBands[0].Rows[0].Height;
            ////parentRowHeight = this.BaseParcelGrid.Rows[0].Height;

            childRowHeight = 19;
            parentRowHeight = 23;
            cropRowHeight = 20;

            if (this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListParcelSplitObject.TableName].Rows.Count > 0)
            {
                parentRowCount = this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListParcelSplitObject.TableName].Rows.Count;
                childRowCount = this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListParcelSplitValueSlices.TableName].Rows.Count;
                cropRowCount = this.baseParcelDataSet.Tables[this.parcelSplitDataSet.ListSplitCrop.TableName].Rows.Count;
                totalRowCount = parentRowCount + childRowCount + cropRowCount;

                if (totalRowCount < 10)
                {
                    extraHeight = 5;
                }
                else
                {
                    ////extraHeight = (totalRowCount - (totalRowCount / 2));
                    extraHeight = 0;
                }

                if (childRowHeight != 0)
                {
                    tempChildHeight = childRowCount * childRowHeight;
                    tempParentHeight = parentRowCount * parentRowHeight;
                }
                else
                {
                    tempChildHeight = childRowCount * childRowHeight;
                    tempParentHeight = parentRowCount * parentRowHeight;
                }

                tempCropRowHeight = cropRowCount * cropRowHeight;

                /*To Reduce Extra space in the Bottom*/
                int gridHeight = tempParentHeight + tempChildHeight + tempCropRowHeight + this.BaseParcelGrid.DisplayLayout.Bands[0].Header.Height + extraHeight+this.SitusPanel.Height +42;

                /*Bug fixing for Gap between Grid and ScrollBar*/
                gridHeight += parentRowCount;
                this.BaseParcelGrid.Height = gridHeight + 2;
                this.ParcelSplitGrid.Height = gridHeight + 19;
                /*Bug fixing for Gap between Grid and ScrollBar*/
                this.Height = gridHeight + this.SplitAttachmentPanel.Height + this.ShowParcelDetailLabel.Height + this.AttachmentsPanel.Height + 30;
                this.ParcelSplitPictureBox.Height = this.Height;
                this.SplitParcelPanel.Height = this.Height - this.AttachmentsPanel.Height;
                this.MainParcelSplitpanel.Height = this.Height;
                this.sliceHeight = this.Height;

                SliceResize sliceResize;
                sliceResize.MasterFormNo = 24500;
                sliceResize.SliceFormName = "D24500.F29500";
                sliceResize.SliceFormHeight = this.Height;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                this.ParcelSplitPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ParcelSplitPictureBox.Height, this.ParcelSplitPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                this.Height = this.sliceHeight;
                this.Show();
            }
            ////else
            ////{
            ////    this.AppraisalGridpanel.Height = 38;
            ////    this.AppraisalSummaryGrid.Height = this.AppraisalGridpanel.Height;
            ////    this.AppraisalSummaryPictureBox.Height = this.AppraisalSummaryGrid.Height;
            ////    this.Height = this.AppraisalSummaryGrid.Height;
            ////    SliceResize sliceResize;
            ////    sliceResize.MasterFormNo = this.masterFormNo;
            ////    sliceResize.SliceFormName = "D35000.F35000";
            ////    sliceResize.SliceFormHeight = this.AppraisalSummaryGrid.Height;
            ////    ////if (!this.flagFormLoad)
            ////    ////{
            ////    this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            ////    this.AppraisalSummaryPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AppraisalSummaryPictureBox.Height, this.AppraisalSummaryPictureBox.Width, this.tabText, this.redColorCode, this.greenColorCode, this.blueColorCode);
            ////}
        }

        #region BugId 5959

        /// <summary>
        /// To control the lock keypress for controls
        /// </summary>
        /// <param name="controlLook"> bool true or false</param>
        private void ControlLock(bool controlLook)
        {

            if (!this.splitProcessed && this.PermissionFiled.editPermission)
            {
                this.LockControls(true);
            }

            // For Textbox control
            // this.OriginalTaxableValueTextBox.LockKeyPress = controlLook;
            //this.SplitParcelOrderTextBox.LockKeyPress = controlLook;
            this.SplitParcelOrderTextBox.LockKeyPress = true;
            this.SplitParcelNumberTextBox.LockKeyPress = controlLook;
            this.SplitId1TextBox.LockKeyPress = controlLook;
            this.SplitDorCodeTextBox.LockKeyPress = controlLook;

            //// For Button and Checkbox control
            this.AttachmentsCheckBox.Enabled = !controlLook;
            this.CommentsCheckBox.Enabled = !controlLook;
            this.SplitAttachmentCheckBox.Enabled = !controlLook;
            this.SplitCommentsCheckBox.Enabled = !controlLook;
            this.SplitDorCodeLinkLabel.Enabled = !controlLook;
            //// this.ProcessButton.Enabled = false;

            if (!this.PermissionFiled.editPermission)
            {
                this.SetButton.Enabled = this.PermissionEdit;
                this.ParcelSplitTextBox.ReadOnly = !this.PermissionFiled.editPermission;
                this.DetailsCheckBox.Enabled = this.PermissionFiled.editPermission;
                this.ClearButton.Enabled = this.PermissionFiled.editPermission;
                this.ProcessButton.Enabled = this.PermissionFiled.editPermission;
                //this.ProcessButton.Enabled = this.PermissionFiled.editPermission;
                this.ForwardParcelButton.Enabled = this.PermissionFiled.editPermission;
            }

            this.BackwardParcelButton.Enabled = !controlLook;
        }

        #endregion BugId 5959

        private void ParcelNumberTextBox_Enter(object sender, EventArgs e)
        {

        }

        #endregion SetSmartpartHeight


        #endregion
        ///
        private void EmptySplitParcelPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Id2TextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.GetSplitHeaderDetail(this.currentParcel);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }

        }
        ////Editing situs field comment after using situs link label purushotham.a
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void situsLabelTextBox_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.situsLabelTextBox.Text))
            {
                this.situsLabelTextBox.Enabled = true;
            }
            else
            {
                this.situsLabelTextBox.Enabled = false;
            }
        }

        private void Id2TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.lockBool == false)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void situsLabelTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.lockBool == false)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void LegalLabelTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.lockBool == false)
                {
                    this.EditEnabled();
                   // this.LegalLabelTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
                    //this.LegalPanel.AutoScroll = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void LegalLabelTextBox_Leave(object sender, EventArgs e)
        {
                try
                {
                    this.GetSplitHeaderDetail(this.currentParcel);
                }
                catch (Exception ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                }
            
        }

        private void situsLabelTextBox_Leave(object sender, EventArgs e)
        {
                try
                {
                    this.GetSplitHeaderDetail(this.currentParcel);
                }
                catch (Exception ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                }
            
        }

        private void SitusLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ////Reverted Parcel split situs Edit form TFS#ID:19555 by Purushotham.A

            try
            {
                this.Cursor = Cursors.WaitCursor;
                Form situsEdit = new Form();
                tempStr = this.SplitParcelOrderTextBox.Text;
                if (!string.IsNullOrEmpty(tempStr))
                {
                    this.editedRecord = Convert.ToInt32(tempStr);
                }
                if (this.parcelSplitDataSet.ListSplitHeaderDetail.Rows.Count > 0)
                {
                    object[] optionalParameters = new object[] { this.splitParcelHeaderDataTable, this.editedRecord };
                    situsEdit = this.form29500Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(24501, optionalParameters, this.form29500Control.WorkItem);
                }
                else
                {
                    /* Modified to Implement TFS# 21784 */
                    //object[] optionalParameters = new object[] { };
                    object[] optionalParameters = new object[] { this.splitParcelHeaderDataTable, this.editedRecord };
                    situsEdit = this.form29500Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(24501, optionalParameters, this.form29500Control.WorkItem);
                }

                if (situsEdit != null)
                {
                    if (situsEdit.ShowDialog() == DialogResult.OK)
                    {
                        this.tempString = TerraScanCommon.GetValue(situsEdit, "CommandResult");
                        StringReader stream = null;
                        XmlTextReader reader = null;
                        DataSet xmlDS = new DataSet();
                        DataTable temp = new DataTable();
                        stream = new StringReader(tempString);
                        reader = new XmlTextReader(stream);
                        xmlDS.ReadXml(reader);
                        temp = xmlDS.Tables[0];
                        this.splitParcelHeaderDataTable.Clear();
                        foreach (DataRow r in temp.Select())
                        {
                            this.splitParcelHeaderDataTable.ImportRow(r);
                        }
                        this.splitParcelHeaderDataTable.AcceptChanges();
                        if (!string.IsNullOrEmpty(this.splitParcelHeaderDataTable.Rows[editedRecord - 1][this.parcelSplitDataSet.ListSplitHeaderDetail.SitusColumn.ColumnName].ToString()))
                        {
                            this.SitusLinkLabel.Text = this.splitParcelHeaderDataTable.Rows[editedRecord - 1][this.parcelSplitDataSet.ListSplitHeaderDetail.SitusColumn.ColumnName].ToString();
                        }
                        else
                        {
                            this.SitusLinkLabel.Text = "««  »»";
                        }
                        if (reader != null)
                        {
                            reader.Close();
                        }
                        this.EditEnabled();
                        // this.EditEnabled();
                        //this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        // ConvertXMLToDataSet(tempString);
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

        public DataSet ConvertXMLToDataSet(string xmlData)
        {
            // xmlData = this.tempString;
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                DataTable temp = new DataTable();
                // F29500ParcelSplitData.ListSplitHeaderDetailDataTable tempTable = new F29500ParcelSplitData.ListSplitHeaderDetailDataTable();
                stream = new StringReader(xmlData);
                // Load the XmlTextReader from the stream
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                temp = xmlDS.Tables[0];
                // DatatRow[] tempRow= temp.Select();
                this.splitParcelHeaderDataTable.Clear();

                foreach (DataRow r in temp.Select())
                {
                    this.splitParcelHeaderDataTable.ImportRow(r);
                }
                this.splitParcelHeaderDataTable.AcceptChanges();
                return null;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (reader != null) reader.Close();
            }

        }

        private void LegalPanel_Scroll(object sender, ScrollEventArgs e)
        {
            //VScrollBar vScrollBar1 = new VScrollBar();

            //// Add event handlers for the OnScroll and OnValueChanged events.
            //vScrollBar1.Scroll += new ScrollEventHandler(this.LegalPanel_Scroll);
            //vScrollBar1.ValueChanged += new EventHandler(this.LegalLabelTextBox_TextChanged); 
        }

        private void SplitAttachmentPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ParcelSplitGrid_AfterCellActivate(object sender, EventArgs e)
        {
            UltraGridRow activeRow = this.ParcelSplitGrid.ActiveRow;
            UltraGridCell activeCell = this.ParcelSplitGrid.ActiveCell;
            this.EditEnabled();
            //if (Band.Index == 2)
            //{
            
                //for (int childRowCount = 0; childRowCount < activeRow.ChildBands[0].Rows.Count; childRowCount++)
                //{
                //    //for checking and unchecking at band2
                //    //for checking and unchecking at band1
                //    if (activeCell.Column.ToString().Equals("Checked1"))
                //    {
                //        if (!activeRow.ChildBands[0].Rows[childRowCount].Cells["ComponentTypeID"].Value.ToString().Equals("3"))
                //        {
                //            activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 2].Activation = Activation.AllowEdit;
                //        }
                //        else
                //        {
                //            // activeRow.ChildBands[0].Rows[childRowCount].Cells[activeCell.Column.Index + 1].Value = false;
                //        }

                //    }
                //}
           // }


        }


        private void ClassCodeComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == ' ')
            {

            }
            else
            {
                e.Handled = e.KeyChar != (char)Keys.Back;
            }

        }


        private void ClassCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            //dataSetCollection = new List<DataSetCollection>();
            //DataSet ds = new DataSet();
            int cursorPosition = ClassCodeTextBox.SelectionStart;
            this.tempClassCode = ClassCodeTextBox.Text;
            this.templength = tempClassCode.Length;
            this.tempClassCode = tempClassCode.Replace(" ", "");
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(tempClassCode))
            {
                List<string> result = new List<string>(Regex.Split(tempClassCode, @"(?<=\G.{2})", RegexOptions.Singleline));
                var count = result.Count;
                for (int i = 0; i < count; i++)
                {
                    if (result[i].Length.Equals(2))
                    {
                        if (sb.ToString().Length < 17)
                        {
                            string temp = result[i].Insert(2, " ").ToString();

                            sb.Append(temp);
                        }

                    }
                    else
                    {
                        if (sb.ToString().Length < 17)
                        {
                            sb.Append(result[i].ToString());
                        }
                    }
                }
                //previousCursorindex = cursorPosition;
                var length = sb.ToString().Length;
                ClassCodeTextBox.TextChanged -= new EventHandler(this.ClassCodeTextBox_TextChanged);
                if (length > 17)
                {
                    ClassCodeComboBox.Text = sb.ToString().Remove(17);
                    length = length - 1;
                }
                else
                {
                    ClassCodeComboBox.Text = sb.ToString();
                }
                if (length > templength)
                {
                    ClassCodeTextBox.SelectionStart = cursorPosition + 1;
                }
                else
                {
                    if (cursorPosition != 0)
                    {
                        ClassCodeTextBox.SelectionStart = cursorPosition;
                    }
                }
                if (length.Equals(templength))
                {
                    ClassCodeTextBox.SelectionStart = cursorPosition;
                }
                //ClassCodeRGB
                //AddDataSetValues("DSName", "", "f26000_udf_GetParcelClassCodeRGB");
                //ds = this.form26000Control.WorkItem.ClassCode_RGB(this.dataSetCollection[0].commandText);
                ClassCodeTextBox.TextChanged += new EventHandler(this.ClassCodeTextBox_TextChanged);
                this.EditEnabled();
            }

        }

        private void ClassCodeComboBox_TextChanged(object sender, EventArgs e)
        {
            int cursorPosition = ClassCodeComboBox.SelectionStart;
            this.tempClassCode = ClassCodeComboBox.Text;
            this.templength = tempClassCode.Length;
            this.tempClassCode = tempClassCode.Replace(" ", "");
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(tempClassCode))
            {
                List<string> result = new List<string>(Regex.Split(tempClassCode, @"(?<=\G.{2})", RegexOptions.Singleline));
                var count = result.Count;
                for (int i = 0; i < count; i++)
                {
                    if (result[i].Length.Equals(2))
                    {
                        if (sb.ToString().Length < 17)
                        {
                            string temp = result[i].Insert(2, " ").ToString();

                            sb.Append(temp);
                        }

                    }
                    else
                    {
                        if (sb.ToString().Length < 17)
                        {
                            sb.Append(result[i].ToString());
                        }
                    }
                }
                //previousCursorindex = cursorPosition;
                var length = sb.ToString().Length;
                ClassCodeComboBox.TextChanged -= new EventHandler(this.ClassCodeComboBox_TextChanged);
                if (length > 17)
                {
                    ClassCodeComboBox.Text = sb.ToString().Remove(17);
                    length = length - 1;
                }
                else
                {
                    ClassCodeComboBox.Text = sb.ToString();
                }
                if (length > templength)
                {
                    ClassCodeComboBox.SelectionStart = cursorPosition + 1;
                }
                else
                {
                    if (cursorPosition != 0)
                    {
                        ClassCodeComboBox.SelectionStart = cursorPosition;
                    }
                }
                if (length.Equals(templength))
                {
                    ClassCodeComboBox.SelectionStart = cursorPosition;
                }
                ClassCodeComboBox.TextChanged += new EventHandler(this.ClassCodeComboBox_TextChanged);
                this.EditEnabled();
            }
        }
        private void ClassCodeComboBox_TextUpdate(object sender, EventArgs e)
        {
            this.classCode = this.ClassCodeComboBox.Text;
            if ((!string.IsNullOrEmpty(this.ClassCodeComboBox.Text)) && (classCodeConfigValue > 0) && (classCode.ToString().Replace(" ", "").Length == classCodeConfigValue))
            {
                this.classCodeDataTable = this.form29500Control.WorkItem.F26000_ClassCodeDetails(classCode).f26000ClassCode;
                this.ClassCodeComboBox.DisplayMember = classCodeDataTable.ClassCodeColumn.ColumnName;
                if (classCodeDataTable.Rows.Count > 0)
                {
                    this.ClassCodeComboBox.DataSource = classCodeDataTable.DefaultView;
                    this.classCode = this.ClassCodeComboBox.Text;
                    this.ClassCodeComboBox.Text = this.classCode;
                    this.ClassCodeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
                    this.ClassCodeComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
                    this.ClassCodeComboBox.Text = this.classCode;
                    this.ClassCodeComboBox.Select(this.ClassCodeComboBox.Text.Length, 0);

                }
            }

        }

        private void ClassCodeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //int cursorPosition = ClassCodeComboBox.SelectionStart;
            //this.tempClassCode = ClassCodeComboBox.Text;
            //this.templength = tempClassCode.Length;
            //this.tempClassCode = tempClassCode.Replace(" ", "");
            //StringBuilder sb = new StringBuilder();
            //if (!string.IsNullOrEmpty(tempClassCode))
            //{
            //    List<string> result = new List<string>(Regex.Split(tempClassCode, @"(?<=\G.{2})", RegexOptions.Singleline));
            //    var count = result.Count;
            //    for (int i = 0; i < count; i++)
            //    {
            //        if (result[i].Length.Equals(2))
            //        {
            //            if (sb.ToString().Length < 17)
            //            {
            //                string temp = result[i].Insert(2, " ").ToString();

            //                sb.Append(temp);
            //            }

            //        }
            //        else
            //        {
            //            if (sb.ToString().Length < 17)
            //            {
            //                sb.Append(result[i].ToString());
            //            }
            //        }
            //    }
            //    //previousCursorindex = cursorPosition;
            //    var length = sb.ToString().Length;
            //    ClassCodeComboBox.TextChanged -= new EventHandler(this.ClassCodeComboBox_TextChanged);
            //    if (length > 17)
            //    {
            //        ClassCodeComboBox.Text = sb.ToString().Remove(17);
            //        length = length - 1;
            //    }
            //    else
            //    {
            //        ClassCodeComboBox.Text = sb.ToString();
            //    }
            //    if (length > templength)
            //    {
            //        ClassCodeComboBox.SelectionStart = cursorPosition + 1;
            //    }
            //    else
            //    {
            //        if (cursorPosition != 0)
            //        {
            //            ClassCodeComboBox.SelectionStart = cursorPosition;
            //        }
            //    }
            //    if (length.Equals(templength))
            //    {
            //        ClassCodeComboBox.SelectionStart = cursorPosition;
            //    }
            //    ClassCodeComboBox.TextChanged += new EventHandler(this.ClassCodeComboBox_TextChanged);
            //    this.EditEnabled();
            //}
        }

        private void ClassCodeComboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.GetSplitHeaderDetail(this.currentParcel);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void ParcelSplitGrid_ClickCell(object sender, ClickCellEventArgs e)
        {
            this.EditEnabled();

            if (IsStatus)
            {
                if ((this.ParcelSplitGrid.ActiveRow != null) && (!e.Cell.Row.Activated))
                {
                    e.Cell.Row.Activate();
                }
                if (this.ParcelSplitGrid.ActiveRow != null && this.ParcelSplitGrid.ActiveRow.Band.Index == 0)
                {
                    if (!e.Cell.Row.Activated && (e.Cell.Column.Key.Contains("ObjectString"))) //((e.Cell.Column.Index == 7 || e.Cell.Column.Index == 9 || e.Cell.Column.Index == 11) && !e.Cell.Row.Activated)
                    {
                        e.Cell.Row.Activate();
                    }
                }

                int currentRowIndex = 0;
                string objectstringColumnName = "";
                objectstringColumnName = e.Cell.Column.Key;


                if (e.Cell.Column.Key.Contains("ObjectString") && e.Cell.Row.Band.Index == 0)
                {
                    int count = 0;

                    count = Convert.ToInt32(objectstringColumnName.Substring(12));

                    if (count == 1)
                    {
                        if (this.parcelNumberSplitDataSet.Tables[0].Rows[e.Cell.Row.Index]["IsValue1"].Equals(true))
                        {
                            this.parcelNumberSplitDataSet.Tables[0].Rows[e.Cell.Row.Index]["IsValue1"] = false;
                            e.Cell.Appearance.BackColor = Color.FromArgb(77, 77, 77);
                            e.Cell.Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                        }
                        else
                        {
                            this.parcelNumberSplitDataSet.Tables[0].Rows[e.Cell.Row.Index]["IsValue1"] = true;
                            e.Cell.Appearance.BackColor = Color.FromArgb(31, 65, 103);
                            e.Cell.Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                        }
                    }
                    else
                    {
                        if (this.parcelNumberSplitDataSet.Tables[0].Rows[e.Cell.Row.Index]["IsValue" + count.ToString()].Equals(true))
                        {
                            this.parcelNumberSplitDataSet.Tables[0].Rows[e.Cell.Row.Index]["IsValue" + count.ToString()] = false;
                            e.Cell.Appearance.BackColor = Color.FromArgb(77, 77, 77);
                            e.Cell.Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                        }
                        else
                        {
                            this.parcelNumberSplitDataSet.Tables[0].Rows[e.Cell.Row.Index]["IsValue" + count.ToString()] = true;
                            e.Cell.Appearance.BackColor = Color.FromArgb(31, 65, 103);
                            e.Cell.Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                        }
                    }
                }

                if (e.Cell.Column.Key.Contains("VSString") && e.Cell.Row.Band.Index == 1)
                {

                    if (!string.IsNullOrEmpty(this.ParcelSplitGrid.ActiveRow.Cells["ValueSliceID"].Value.ToString()))
                    {


                        DataRow[] rows = this.parcelNumberSplitDataSet.Tables[1].Select("ValueSliceID = " + this.ParcelSplitGrid.ActiveRow.Cells["ValueSliceID"].Value);
                        if (rows.Length >= 1)
                        {
                            currentRowIndex = this.parcelNumberSplitDataSet.Tables[1].Rows.IndexOf(rows[0]);



                            int count = 0;

                            count = Convert.ToInt32(objectstringColumnName.Substring(8));
                            if (count == 1)
                            {
                                //e.Cell.Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                                //DataRow[] rows = this.parcelNumberSplitDataSet.Tables[1].Select("ValueSliceID = " + this.ParcelSplitGrid.ActiveRow.Cells["ValueSliceID"].Value);

                                if (this.parcelNumberSplitDataSet.Tables[1].Rows[currentRowIndex]["IsValue1"].Equals(true))
                                {
                                    this.parcelNumberSplitDataSet.Tables[1].Rows[currentRowIndex]["IsValue1"] = false;
                                    e.Cell.Appearance.BackColor = Color.FromArgb(77, 77, 77);
                                    e.Cell.Appearance.ForeColor = Color.White;
                                    e.Cell.Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                                }
                                else
                                {
                                    this.parcelNumberSplitDataSet.Tables[1].Rows[currentRowIndex]["IsValue1"] = true;
                                    e.Cell.Appearance.BackColor = Color.FromArgb(255, 255, 255);
                                    e.Cell.Appearance.ForeColor = Color.Black;
                                    e.Cell.Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                                }
                            }
                            else
                            {
                                if (this.parcelNumberSplitDataSet.Tables[1].Rows[currentRowIndex]["IsValue" + count.ToString()].Equals(true))
                                {
                                    this.parcelNumberSplitDataSet.Tables[1].Rows[currentRowIndex]["IsValue" + count.ToString()] = false;
                                    e.Cell.Appearance.BackColor = Color.FromArgb(77, 77, 77);
                                    e.Cell.Appearance.ForeColor = Color.White;
                                    e.Cell.Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                                }
                                else
                                {
                                    this.parcelNumberSplitDataSet.Tables[1].Rows[currentRowIndex]["IsValue" + count.ToString()] = true;
                                    e.Cell.Appearance.BackColor = Color.FromArgb(255, 255, 255);
                                    e.Cell.Appearance.ForeColor = Color.Black;
                                    e.Cell.Activation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
