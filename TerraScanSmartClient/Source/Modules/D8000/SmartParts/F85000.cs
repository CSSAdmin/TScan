//--------------------------------------------------------------------------------------------
// <copyright file="F85000.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F85000.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09 Aug 2007      Ramya.D             Created
//*********************************************************************************/

namespace D8000
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using System.Collections;
    using TerraScan.Utilities;
    using TerraScan.UI.Controls;
    using System.Configuration;
    using Infragistics.Win;
    using Infragistics.Win.UltraWinGrid;
    using TerraScan.SmartParts;
    using TerraScan.Common.Reports;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// F85000 class file
    /// </summary>
    [SmartPart]
    public partial class F85000 : BaseSmartPart
    {
        #region Variable
        /// <summary>
        /// Set Date Format
        /// </summary>
        private readonly string dateFormat = "MM/dd/yyyy";

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// eventFeatureClassID Local variable.
        /// </summary>
        private int eventFeatureClassID;

        /// <summary>
        ///  Used to Store CurrentRow
        /// </summary>
        private int currentRowSelected;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        ///  Used to Store CurrentRow
        /// </summary>
        private string currentRowLevel;

        /// <summary>
        /// used to save status
        /// </summary>
        private bool saveStatus;

        /// <summary>
        /// eventRowNO
        /// </summary>
        private int eventRowNo;

        /// <summary>
        /// FeatureID
        /// </summary>
        private int featureId;

        /// <summary>
        /// used to store EventID
        /// </summary>
        private int newEventID;

        /// <summary>
        ///  Used to Store totalRecords
        /// </summary>
        private int totalDataCount;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        ///  used to Convert Color From string
        /// </summary>
        private ColorConverter colorConv = new ColorConverter();

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int keyId;

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
        ///  used to store selected parentrow eventid
        /// </summary>
        private string currentParentEventId;

        /// <summary>
        /// Used to store  typeCount
        /// </summary>
        private int typeCount;

        /// <summary>
        /// used to dataChangeStatus
        /// </summary>
        private bool invalidKey;

        /// <summary>
        /// Used to store  statusCount
        /// </summary>
        private int statusCount;

        /// <summary>
        /// Used to Store FeatureClassid
        /// </summary>
        private int featureClassId;

        /// <summary>
        /// used to dataChangeStatus
        /// </summary>
        private bool dataChangeStatus;

        /// <summary>
        /// Instance of F85000 Controller to call the WorkItem
        /// </summary>
        private F85000Controller form85000Controller;

        /// <summary>
        /// DatePicker object
        /// </summary>
        private System.Windows.Forms.DateTimePicker eventEngineValidDate = new System.Windows.Forms.DateTimePicker();

        /// <summary>
        /// gDocEventEngineData
        /// </summary>
        private GDocEventEngineData eventEngineData = new GDocEventEngineData();

        /// <summary>
        /// used to store EventEngineSource
        /// </summary>
        private BindingSource eventEngineSource = new BindingSource();

        /// <summary>
        /// Used to Create Insert XML Data
        /// </summary>
        private GDocF8001InsertXMLData gdocF8001InsertXMLData = new GDocF8001InsertXMLData();

        /// <summary>
        /// USed to Store Event Type / Status Data
        /// </summary>
        private GDocEventEngineTypeStatusData eventEngineTypeStatusData = new GDocEventEngineTypeStatusData();

        #endregion Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F85000"/> class.
        /// </summary>
        public F85000()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F85000"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F85000(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;

            this.EventEnginepictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.EventEnginepictureBox.Height, this.EventEnginepictureBox.Width, tabText, red, green, blue);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F85000"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        /// <param name="featureClasId">featureClasId</param>
        public F85000(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, string featureClasId)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            int.TryParse(featureClasId, out this.featureClassId);
            this.featureId = this.keyId;
            this.EventEnginepictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.EventEnginepictureBox.Height, this.EventEnginepictureBox.Width, tabText, red, green, blue);
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
        /// <param name="featureClassId">The feature class id.</param>
        public F85000(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassId)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.featureClassId = featureClassId;
            ////int.TryParse(featureClasId, out this.featureClassId);
            this.featureId = this.keyId;
            this.EventEnginepictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.EventEnginepictureBox.Height, this.EventEnginepictureBox.Width, tabText, red, green, blue);
        }

        #endregion Constructor

        #region Eventpublication

        /// <summary>
        /// display record id
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_DisplayNavigatedRecord, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<RecordNavigationEntity>> DisplayNavigatedRecord;

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

        /////// <summary>
        /////// Declare the event D9030_F9030_ReloadAfterSave
        /////// </summary>
        ////[EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        ////public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /////// <summary>
        /////// Declare the event FormSlice_ValidationAlert        
        /////// </summary>
        ////[EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        ////public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

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

        /////// <summary>
        /////// Declare the event D84700_F84722_OnSave_SetKeyId
        /////// </summary>
        ////[EventPublication(EventTopicNames.D84700_F84722_OnSave_SetKeyId, PublicationScope.Global)]
        ////public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> FormSlice_OnSave_SetKeyId;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        #endregion Eventpublication

        #region Properties

        /// <summary>
        /// Gets or sets the F1105 control.
        /// </summary>
        /// <value>The F1105 control.</value>
        [CreateNew]
        public F85000Controller F85000Control
        {
            get { return this.form85000Controller as F85000Controller; }
            set { this.form85000Controller = value; }
        }

        #endregion Properties

        /// <summary>
        /// Loads the event engine.
        /// </summary>
        public void LoadEventEngine()
        {
            this.LoadEventEngineData();
            this.GDocEventEngineCalenderControl.Visible = false;
            this.SetNewMode();
            if (this.statusCount > 0 && this.typeCount > 0)
            {
                if (!this.PermissionFiled.newPermission)
                {
                    this.PermissionDisable();
                }
            }
            else
            {
                ////MessageBox.Show("s");  
                this.Disablecontrol(false);
            }
        }

        #region EventSubcription

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
                    this.LoadEventEngine();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Called when [D9030_F9030_LoadParaMeterized Slice Details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadParaMeterizedSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadParaMeterizedSliceDetails(object sender, DataEventArgs<SliceReloadParaMeterizedActiveRecord> eventArgs)
        {
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.featureId = eventArgs.Data.SelectedKeyId;
                    if ((eventArgs.Data.ParameterList != null) && (eventArgs.Data.ParameterList.Count > 0) && (eventArgs.Data.ParameterList[0] != null))
                    {
                        int.TryParse(eventArgs.Data.ParameterList[0], out this.featureClassId);
                    }

                    this.LoadEventEngine();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
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

                    if (this.eventEngineData.ListValidKeyID.Rows.Count > 0)
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
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #region Reports

        /// <summary>
        /// Handles the Click event of the PrintButton control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_PrintButtonClick, Thread = ThreadOption.UserInterface)]
        public void PrintButtonClick(object sender, DataEventArgs<Button> e)
        {
            // TODO : Genralized 
            try
            {
                ////this.Cursor = Cursors.WaitCursor;
                //// Calling the Common Function for Report
                Hashtable ht = new Hashtable();
                ht.Add("FeatureClassID", this.featureClassId);
                ht.Add("FeatureID", this.featureId);
                /////changed the parameter type from string to int
                TerraScanCommon.ShowReport(800101, Report.ReportType.Print, ht);
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
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_PreviewButtonClick, Thread = ThreadOption.UserInterface)]
        public void PreviewButtonClick(object sender, DataEventArgs<Button> e)
        {
            try
            {
                ////this.Cursor = Cursors.WaitCursor;
                //// Calling the Common Function for Report
                Hashtable ht = new Hashtable();
                ht.Add("FeatureClassID", this.featureClassId);
                ht.Add("FeatureID", this.featureId);
                ////changed the parameter type from string to int
                TerraScanCommon.ShowReport(800101, Report.ReportType.Preview, ht);
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
        /// Handles the Click event of the EMailButton control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_EmailButtonClick, Thread = ThreadOption.UserInterface)]
        public void EmailButtonClick(object sender, DataEventArgs<Button> e)
        {
            try
            {
                //// this.Cursor = Cursors.WaitCursor;
                ////// calling  Common Function For Report
                ////this.Cursor = Cursors.WaitCursor;
                //// Calling the Common Function for Report
                Hashtable ht = new Hashtable();
                ht.Add("FeatureClassID", this.featureClassId);
                ht.Add("FeatureID", this.featureId);
                ////changed the parameter type from string to int
                TerraScanCommon.ShowReport(800101, Report.ReportType.Email, ht);
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

        #endregion reports

        #region OptionalParameter
        /// <summary>
        /// Sends the optional parameters.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        ///[EventSubscription("topic://TerraScan.UI.CAB/MainForm/SendOptionalParameters", Thread = ThreadOption.UserInterface)]
        [EventSubscription(EventTopics.D9001_ShellForm_SendOptionalParameters, Thread = ThreadOption.UserInterface)]
        public void SendOptionalParameters(object sender, DataEventArgs<object[]> e)
        {
            object[] optionalParams = e.Data;
            if (optionalParams.Length > 0 && optionalParams[optionalParams.Length - 1].Equals(this.Tag))
            {
                if (optionalParams[0] != null && string.Equals(optionalParams[0].ToString(), "RecordDeleted"))
                {
                    ////this.featureClassId = eventEngineFeatureClassId;
                    ////this.featureId = eventEnginefeatureId;
                    this.LoadEventEngine();
                }
                else
                {
                    if (optionalParams[0] != null && optionalParams[1] != null && !string.IsNullOrEmpty(optionalParams[0].ToString()) && !string.IsNullOrEmpty(optionalParams[1].ToString()))
                    {
                        if (!string.IsNullOrEmpty(optionalParams[optionalParams.Length - 3].ToString()) && optionalParams[optionalParams.Length - 3] != null)
                        {
                            this.PermissionFiled = ((PermissionFields)optionalParams[optionalParams.Length - 3]);
                        }

                        this.featureClassId = Convert.ToInt32(optionalParams[0].ToString());
                        this.featureId = Convert.ToInt32(optionalParams[1].ToString());
                        this.LoadEventEngine();
                    }
                    else
                    {
                        this.featureClassId = 0;
                        this.featureId = 0;
                        this.LoadEventEngine();
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// Called when [form slice_ resize].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_Resize(DataEventArgs<SliceResize> eventArgs)
        {
            try
            {
                if (this.FormSlice_Resize != null)
                {
                    this.FormSlice_Resize(this, eventArgs);
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion EventSubcription

        /// <summary>
        ///  EventEnginepictureBox_MouseEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void EventEnginepictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.EventEngineToolTip.SetToolTip(this.EventEnginepictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// EventEnginepictureBox click Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void EventEnginepictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D8000.F85000"));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// F85000_Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F85000_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadEventEngine();
                this.GDocEventEngineCalenderControl.Visible = false;
                this.eventEngineValidDate.CustomFormat = "mm/dd/yyyy";
                this.eventEngineValidDate.MaxDate = new System.DateTime(2075, 12, 31, 0, 0, 0, 0);
                this.eventEngineValidDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// GDocEventEngineDateTextBox_KeyPress
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GDocEventEngineDateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                switch (e.KeyChar)
                {
                    case (char)13:
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(this.GDocEventEngineDateTextBox.Text.Trim()))
                                {
                                    this.eventEngineValidDate.Value = DateTime.Parse(this.GDocEventEngineDateTextBox.Text.Trim());
                                    ////this.EventEngineKeyEnter(e);
                                    return;
                                }
                                else
                                {
                                    ////this.EventEngineKeyEnter(e);
                                }
                            }
                            catch
                            {
                                this.GDocEventEngineDateTextBox.Text = string.Empty;
                            }

                            break;
                        }
                }

                this.dataChangeStatus = true;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// GdocEventEngineStatusComboBox_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GdocEventEngineStatusComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.dataChangeStatus = true;
                this.SetStatusColor(this.GdocEventEngineStatusComboBox.SelectedValue.ToString());
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// GdocEventEngineDateImage_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GdocEventEngineDateImage_Click(object sender, EventArgs e)
        {
            this.ParentForm.AcceptButton = null;
            try
            {
                EventEnginedateTimePicker.BringToFront();
                if (!string.IsNullOrEmpty(this.GDocEventEngineDateTextBox.Text.Trim()))
                {
                    this.EventEnginedateTimePicker.Value = Convert.ToDateTime(GDocEventEngineDateTextBox.Text);
                }
                else
                {
                    EventEnginedateTimePicker.Value = DateTime.Today;
                }

                EventEnginedateTimePicker.Focus();
                SendKeys.Send("{F4}");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// GdocEventEngineCompleteCheckBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GdocEventEngineCompleteCheckBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.dataChangeStatus = true;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// GdocEventEngineWorkOrderTextBox_KeyPress
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GdocEventEngineWorkOrderTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                this.dataChangeStatus = true;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// GdocEventEngineWorkOrderTextBox_MouseEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GdocEventEngineWorkOrderTextBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                if (this.GdocEventEngineTypeComboBox.Text.Length > 20)
                {
                    this.EventEngineToolTip.RemoveAll();
                    this.EventEngineToolTip.SetToolTip(this.GdocEventEngineTypeComboBox, this.GdocEventEngineTypeComboBox.Text);
                }
                else
                {
                    this.EventEngineToolTip.RemoveAll();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// GdocEventEngineWorkOrderTextBox_MouseHover
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GdocEventEngineWorkOrderTextBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                if (this.GdocEventEngineWorkOrderTextBox.Text.Trim().Length > 4)
                {
                    this.EventEngineToolTip.SetToolTip(this.GdocEventEngineWorkOrderTextBox, this.GdocEventEngineWorkOrderTextBox.Text);
                }
                else
                {
                    this.EventEngineToolTip.RemoveAll();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// GdocEventEngineWorkOrderPictureBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GdocEventEngineWorkOrderPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowWorkOrderForm();
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// GDocEventEngineCalenderControl_KeyDown
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GDocEventEngineCalenderControl_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    this.GDocEventEngineDateTextBox.Text = this.GDocEventEngineCalenderControl.SelectionStart.ToShortDateString();
                    this.GDocEventEngineCalenderControl.Visible = false;
                    this.GDocEventEngineDateTextBox.Focus();
                    this.dataChangeStatus = true;
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// GDocEventEngineCalenderControl_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GDocEventEngineCalenderControl_Leave(object sender, EventArgs e)
        {
            try
            {
                this.GDocEventEngineCalenderControl.Visible = false;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// GdocEventEngineTypeComboBox_MouseMove
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GdocEventEngineTypeComboBox_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.GdocEventEngineTypeComboBox.Text.Length > 10)
                {
                    this.EventEngineToolTip.SetToolTip(this.GdocEventEngineTypeComboBox, this.GdocEventEngineTypeComboBox.Text);
                }
                else
                {
                    this.EventEngineToolTip.RemoveAll();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// GdocEventEngineTypeComboBox_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GdocEventEngineTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.dataChangeStatus = true;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// GdocEventEngineRowAddLabel_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GdocEventEngineRowAddLabel_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveEventEngine();
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the GDocEventEngineDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void GDocEventEngineDataGridView_AfterCellActivate(object sender, EventArgs e)
        {
            this.currentRowSelected = this.GDocEventEngineDataGridView.ActiveCell.Row.Index;
            int currentColumn = this.GDocEventEngineDataGridView.ActiveCell.Column.Index;
            this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Selected = false;
            try
            {
                if (currentColumn == 1)
                {
                    if (!this.invalidKey)
                    {
                        this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Activated = false;
                        this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Selected = true;
                        this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Activated = false;
                        this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Selected = false;
                        string showFrmId;
                        string showEventID;
                        this.currentRowLevel = this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.levelsColumn.ColumnName].Value.ToString().Trim();
                        showFrmId = this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.EventTypeIDColumn.ColumnName].Value.ToString().Trim();
                        showEventID = this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.EventIDColumn.ColumnName].Value.ToString().Trim();
                        this.ShowEventForm(showFrmId, showEventID);
                    }
                    else
                    {
                        this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Activated = false;
                        this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Selected = true;
                        this.invalidKey = false;
                    }
                }
                else if (currentColumn == 0)
                {
                    this.currentRowLevel = this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.levelsColumn.ColumnName].Value.ToString().Trim();
                    if (!string.Equals(this.currentRowLevel, "3") && this.PermissionFiled.newPermission)
                    {
                        this.CreateGdocEventEngineNewRow();
                        if (this.saveStatus)
                        {
                            this.LoadEventEngineData();
                            this.SetNewMode();
                            this.FindShowId(this.newEventID.ToString());
                        }
                        else if (this.newEventID == -1)
                        {
                            this.GDocEventEngineDataGridView.ActiveCell.Activated = false;
                            MessageBox.Show(SharedFunctions.GetResourceString("WorkOrder") + this.GdocEventEngineWorkOrderTextBox.Text + SharedFunctions.GetResourceString("WorkOrderDoesnotExist"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("WorkOrderValidation")), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            this.GDocEventEngineDataGridView.ActiveCell.Activated = false;
                        }
                    }
                }
                else
                {
                    this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.EventTypeIDColumn.ColumnName].Selected = true;
                }
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

        /// <summary>
        /// GDocEventEngineDataGridView_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GDocEventEngineDataGridView_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.GDocEventEngineDataGridView.ActiveCell != null)
                {
                    if (this.GDocEventEngineDataGridView.ActiveCell.Row.Index >= 0)
                    {
                        if (string.Equals(this.GDocEventEngineDataGridView.ActiveCell.Column.Key, this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName))
                        {
                            if (!string.IsNullOrEmpty(this.GDocEventEngineDataGridView.Rows[this.GDocEventEngineDataGridView.ActiveCell.Row.Index].Cells[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Text.Trim()))
                            {
                                this.GDocEventEngineDataGridView.Rows[this.GDocEventEngineDataGridView.ActiveCell.Row.Index].Cells[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Selected = true;
                                this.GDocEventEngineDataGridView.DisplayLayout.Override.SelectedCellAppearance.ForeColor = Color.White;
                                this.WorkOrderShowForm(this.GDocEventEngineDataGridView.Rows[this.GDocEventEngineDataGridView.ActiveCell.Row.Index].Cells[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Text.Trim());
                                this.GDocEventEngineDataGridView.ActiveCell.Activated = false;
                            }
                        }
                        else if (this.GDocEventEngineDataGridView.ActiveCell.Column.Index == 0)
                        {
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// GDocEventEngineDataGridView_InitializeLayout
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GDocEventEngineDataGridView_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                this.GDocEventEngineDataGridView.DisplayLayout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Image;

                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].CellAppearance.ForeColor = Color.Blue;
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].CellAppearance.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].CellAppearance.Cursor = Cursors.Hand;

                ////Commented and code added by Jayanthi
                ////this.GDocEventEngineDataGridView.DisplayLayout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].CellAppearance.ForeColor = Color.Blue;
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].CellAppearance.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].CellAppearance.Cursor = Cursors.Hand;
                ////Till here                

                //// Code Added By Shiva for Change Request Srpint 22
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.TempColorColumn.ColumnName].Header.Caption = "";
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.AddRowColumn.ColumnName].Header.Caption = "";
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Header.Caption = "Work Order";
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.EventDateColumn.ColumnName].Header.Caption = "Date";
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.IsCompleteColumn.ColumnName].Header.Caption = "Complete";
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.EventNumberColumn.ColumnName].Header.Caption = "Event";
                ////Till Here}
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

        /// <summary>
        /// GDocEventEngineDataGridView_KeyDown
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GDocEventEngineDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyValue)
                {
                    case 37:
                        {
                            this.invalidKey = true;
                            break;
                        }

                    case 38:
                        {
                            this.invalidKey = true;
                            break;
                        }

                    case 39:
                        {
                            this.invalidKey = true;
                            break;
                        }

                    case 40:
                        {
                            this.invalidKey = true;
                            break;
                        }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// GDocEventEngineDataGridView_KeyPress
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GDocEventEngineDataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                switch (e.KeyChar)
                {
                    case (char)13:
                        {
                            this.invalidKey = true;
                            break;
                        }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// GDocEventEngineDataGridView_MouseMove
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GDocEventEngineDataGridView_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                Point mouseCursorPoint = new Point(e.X, e.Y);
                UIElement element = ((UltraGrid)sender).DisplayLayout.UIElement.ElementFromPoint(mouseCursorPoint);
                if (element != null)
                {
                    UltraGridColumn col = element.GetContext(typeof(UltraGridColumn)) as UltraGridColumn;
                    UltraGridRow row = element.GetContext(typeof(UltraGridRow)) as UltraGridRow;
                    if (col != null && row != null)
                    {
                        if (row.Index == -1)
                        {
                            this.EventEngineToolTip.RemoveAll();
                        }

                        if (col.Index == 2 && row.Index >= 0)
                        {
                            this.EventEngineToolTip.SetToolTip(this.GDocEventEngineDataGridView, this.GDocEventEngineDataGridView.Rows[row.Index].Cells[this.eventEngineData.GDocEventEngineDataTable.StatusColumn.ColumnName].Value.ToString());
                        }
                        else if (col.Index == 14 && row.Index >= 0)
                        {
                            this.EventEngineToolTip.SetToolTip(this.GDocEventEngineDataGridView, this.GDocEventEngineDataGridView.Rows[row.Index].Cells[this.eventEngineData.GDocEventEngineDataTable.EventIDColumn.ColumnName].Value.ToString());
                        }
                        else
                        {
                            this.EventEngineToolTip.RemoveAll();
                        }
                    }
                    else
                    {
                        this.EventEngineToolTip.RemoveAll();
                    }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #region User Defined Method

        /// <summary>
        /// Shows the attachment calender in particular location.
        /// </summary>
        private void ShowAttachmentCalender()
        {
            this.GDocEventEngineCalenderControl.Visible = true;

            this.GDocEventEngineCalenderControl.ScrollChange = 1;
            this.GDocEventEngineCalenderControl.Left = 525; ////this.GDocEventEngineHeaderPanel.Left + this.GdocEventEngineDate.Left + this.GdocEventEngineDateImage.Left + this.GdocEventEngineDateImage.Width;
            this.GDocEventEngineCalenderControl.Top = 27; //// this.GDocEventEngineHeaderPanel.Top + this.GdocEventEngineDate.Top + this.GdocEventEngineDateImage.Top;
            this.GDocEventEngineCalenderControl.Tag = this.GdocEventEngineDateImage.Tag;
            this.GDocEventEngineCalenderControl.Focus();
            if (!string.IsNullOrEmpty(this.GDocEventEngineDateTextBox.Text))
            {
                this.GDocEventEngineCalenderControl.SetDate(Convert.ToDateTime(this.GDocEventEngineDateTextBox.Text));
            }

            this.MainContentPanel.Height = 493;
            this.GDocEventEngineCalenderControl.BringToFront();
        }

        /// <summary>
        /// Loads the event type combo box.
        /// </summary>
        private void LoadEventTypeAndStatusComboBox()
        {
            this.eventEngineTypeStatusData = this.form85000Controller.WorkItem.ListEventTypeStatusDetails(this.featureClassId);
            DataRow dr = this.eventEngineTypeStatusData.ListEventEngineTypeTable.NewRow();
            dr[this.eventEngineTypeStatusData.ListEventEngineTypeTable.EventTypeIDColumn.ColumnName] = 0;
            dr[this.eventEngineTypeStatusData.ListEventEngineTypeTable.EventTypeColumn.ColumnName] = "<Select one>";
            this.eventEngineTypeStatusData.ListEventEngineTypeTable.Rows.Add(dr);

            this.statusCount = this.eventEngineTypeStatusData.ListEventStatusTypeTable.Rows.Count;
            this.typeCount = this.eventEngineTypeStatusData.ListEventEngineTypeTable.Rows.Count;
            DataView orderView = new DataView(this.eventEngineTypeStatusData.ListEventEngineTypeTable);
            orderView.Sort = "EventType ASC";

            this.GdocEventEngineTypeComboBox.DataSource = orderView;
            this.GdocEventEngineTypeComboBox.ValueMember = this.eventEngineTypeStatusData.ListEventEngineTypeTable.EventTypeIDColumn.ColumnName;
            this.GdocEventEngineTypeComboBox.DisplayMember = this.eventEngineTypeStatusData.ListEventEngineTypeTable.EventTypeColumn.ColumnName;
            this.GdocEventEngineStatusComboBox.DataSource = this.eventEngineTypeStatusData.ListEventStatusTypeTable;
            this.GdocEventEngineStatusComboBox.ValueMember = this.eventEngineTypeStatusData.ListEventStatusTypeTable.StatusIDColumn.ColumnName;
            this.GdocEventEngineStatusComboBox.DisplayMember = this.eventEngineTypeStatusData.ListEventStatusTypeTable.StatusColumn.ColumnName;
            if (this.GdocEventEngineStatusComboBox.SelectedValue != null)
            {
                this.SetStatusColor(this.GdocEventEngineStatusComboBox.SelectedValue.ToString());
            }

            this.GdocEventEngineTypeComboBox.Focus();
        }

        /// <summary>
        /// Sets the color of the status.
        /// </summary>
        /// <param name="selectedStatus">The selected status.</param>
        private void SetStatusColor(string selectedStatus)
        {
            string statusId = "StatusID =" + selectedStatus;
            DataRow[] statusRow;
            statusRow = this.eventEngineTypeStatusData.ListEventStatusTypeTable.Select(statusId);
            if (statusRow.Length > 0)
            {
                this.StatusColorLabel.BackColor = (System.Drawing.Color)this.colorConv.ConvertFromInvariantString(statusRow[0][2].ToString());
            }
        }

        /// <summary>
        /// Shows the work order form.
        /// </summary>
        private void ShowWorkOrderForm()
        {
            string selectedWorkOrder;
            Form workOrderF8002 = new Form();
            object[] optionalParameter = new object[] { this.featureClassId };
            workOrderF8002 = this.form85000Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(8002, optionalParameter, this.form85000Controller.WorkItem);
            if (workOrderF8002 != null)
            {
                DialogResult dialogResult = workOrderF8002.ShowDialog();
                if (dialogResult != DialogResult.Ignore && dialogResult != DialogResult.Cancel)
                {
                    selectedWorkOrder = TerraScanCommon.GetValue(workOrderF8002, SharedFunctions.GetResourceString("F8001WorkOrder"));
                    this.GdocEventEngineWorkOrderTextBox.Text = selectedWorkOrder;
                    this.dataChangeStatus = true;
                }
                else
                {
                    ////this.GdocEventEngineWorkOrderTextBox.Text = string.Empty;
                }

                this.GdocEventEngineWorkOrderTextBox.Focus();
            }
        }

        /// <summary>
        /// Shows the event form.
        /// </summary>
        /// <param name="eventTypeId">The event type id.</param>
        /// <param name="eventId">The event id.</param>
        private void ShowEventForm(string eventTypeId, string eventId)
        {
            //// this.Cursor = Cursors.WaitCursor;
            FormInfo formInfo;
            int shfrmId;
            if (!string.IsNullOrEmpty(eventTypeId))
            {
                shfrmId = Convert.ToInt32(eventTypeId);
                formInfo = TerraScanCommon.GetFormInfo(shfrmId);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = eventId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
        }

        /// <summary>
        /// Creates the gdoc event engine new row.
        /// </summary>
        private void CreateGdocEventEngineNewRow()
        {
            if (this.PermissionFiled.newPermission)
            {
                if (this.CheckMandatoryField())
                {
                    this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.Clear();
                    if (string.Equals(this.currentRowLevel, "-1"))
                    {
                        this.currentParentEventId = "-1";
                    }
                    else if (string.Equals(this.currentRowLevel, "1"))
                    {
                        this.currentParentEventId = this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.EventIDColumn.ColumnName].Text;
                    }
                    else if (string.Equals(this.currentRowLevel, "2"))
                    {
                        this.currentParentEventId = this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.EventIDColumn.ColumnName].Text;
                    }

                    DataRow gdocEventEngineNewRow = this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.NewRow();
                    if (string.Equals(this.currentParentEventId, "-1"))
                    {
                        gdocEventEngineNewRow[this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.EventIDColumn.ColumnName] = DBNull.Value;
                    }
                    else
                    {
                        gdocEventEngineNewRow[this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.EventIDColumn.ColumnName] = this.currentParentEventId;
                    }

                    gdocEventEngineNewRow[this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.EventTypeColumn.ColumnName] = this.GdocEventEngineTypeComboBox.SelectedValue.ToString();
                    gdocEventEngineNewRow[this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.StatusColumn.ColumnName] = this.GdocEventEngineStatusComboBox.SelectedValue.ToString();
                    gdocEventEngineNewRow[this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.CompleteColumn.ColumnName] = this.GdocEventEngineCompleteCheckBox.Checked;
                    gdocEventEngineNewRow[this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.DateColumn.ColumnName] = this.GDocEventEngineDateTextBox.Text.Trim();
                    gdocEventEngineNewRow[this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.WorkOrderColumn.ColumnName] = this.GdocEventEngineWorkOrderTextBox.Text.Trim();
                    gdocEventEngineNewRow[this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.FeatureIDColumn.ColumnName] = this.keyId;
                    this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.Rows.Add(gdocEventEngineNewRow);
                    string xmlString = TerraScanCommon.GetXmlString(this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable);
                    this.newEventID = this.form85000Controller.WorkItem.InsertGDocEventEngineData(xmlString, TerraScan.Common.TerraScanCommon.UserId);
                    if (this.newEventID == -1)
                    {
                        this.saveStatus = false;
                    }
                    else
                    {
                        this.saveStatus = true;
                        this.dataChangeStatus = false;
                    }
                }
                else
                {
                    this.saveStatus = false;
                    //// this.GDocEventEngineDateTextBox.Focus(); 
                    MessageBox.Show(SharedFunctions.GetResourceString("ExciseTaxAffDvtMissing"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Loads the event engine data.
        /// </summary>
        private void LoadEventEngineData()
        {
            this.FlagSliceForm = true;
            ////this.Cursor = Cursors.WaitCursor;
            ////Added by Ramya.D --- Change Order
            ////commented by vijayakumar to fix the bug 1513.
            this.featureClassId = this.form85000Controller.WorkItem.GetGDocEventEngineFeatureClassId(this.masterFormNo);
            ////if (featureClassId > 0)
            ////{
            this.eventEngineData = this.form85000Controller.WorkItem.LoadEventEngineData(this.keyId, this.featureClassId);

            this.GDocEventEngineDataGridView.DataSource = this.eventEngineData.GDocEventEngineDataTable;
            this.eventEngineSource.DataSource = this.eventEngineData.GDocEventEngineDataTable;
            this.totalDataCount = this.eventEngineData.GDocEventEngineDataTable.Rows.Count;
            if (this.totalDataCount < 20)
            {
                this.GDocEventEngineDataGridView.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToLastItem;
            }
            else
            {
                this.GDocEventEngineDataGridView.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
            }

            ////}
            this.CustomiseDataGrid();

            //// Load Type and status Combo Box
            this.LoadEventTypeAndStatusComboBox();
            this.SetSmartPartHeight(this.totalDataCount);
            if (!this.flagLoadOnProcess)
            {
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);

                sliceResize.SliceFormHeight = this.EventEnginepictureBox.Height;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                this.EventEnginepictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.EventEnginepictureBox.Height, this.EventEnginepictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            }

            if (this.statusCount == 0 && this.typeCount == 1)
            {
                this.Disablecontrol(false);
            }
        }

        /// <summary>
        /// Sets the new mode.
        /// </summary>
        private void SetNewMode()
        {
            if (this.typeCount > 1)
            {
                this.GdocEventEngineTypeComboBox.SelectedIndex = 0;
                this.GDocEventEngineDateTextBox.Text = DateTime.Now.ToString(this.dateFormat);
            }

            if (this.statusCount > 0)
            {
                this.GdocEventEngineStatusComboBox.SelectedIndex = 0;
                this.SetStatusColor(this.GdocEventEngineStatusComboBox.SelectedValue.ToString());
            }

            this.GdocEventEngineWorkOrderTextBox.Text = string.Empty;
            ////Modified by Biju on 18/May/2010 to implement #6876
            this.GdocEventEngineCompleteCheckBox.Checked = false ;
        }

        /// <summary>
        /// Finds the show id.
        /// </summary>
        /// <param name="findRowId">The find row id.</param>
        private void FindShowId(string findRowId)
        {
            int rowId;
            rowId = this.eventEngineSource.Find("EventID", findRowId);
            if (rowId >= 0)
            {
                this.ShowEventForm(this.eventEngineData.GDocEventEngineDataTable.Rows[rowId][this.eventEngineData.GDocEventEngineDataTable.EventTypeIDColumn.ColumnName].ToString(), this.eventEngineData.GDocEventEngineDataTable.Rows[rowId][this.eventEngineData.GDocEventEngineDataTable.EventIDColumn.ColumnName].ToString());
            }
        }

        /// <summary>
        /// Works the order show form.
        /// </summary>
        /// <param name="workOrderId">The work order id.</param>
        private void WorkOrderShowForm(string workOrderId)
        {
            Form workOrderF8010 = new Form();
            object[] optionalParameter = new object[] { workOrderId };
            workOrderF8010 = this.form85000Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(8010, optionalParameter, this.form85000Controller.WorkItem);
            if (workOrderF8010 != null)
            {
                workOrderF8010.ShowDialog();
            }
        }

        /// <summary>
        /// Checks the mandatory field.
        /// </summary>
        /// <returns> True if all mandatoryFields Are Filed  else false
        /// </returns>
        private bool CheckMandatoryField()
        {
            if (!string.IsNullOrEmpty(this.GDocEventEngineDateTextBox.Text.Trim()) && this.GdocEventEngineTypeComboBox.SelectedIndex > 0 && this.GdocEventEngineStatusComboBox.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Customises the data grid.
        /// </summary>
        private void CustomiseDataGrid()
        {
            this.eventRowNo = 0;
            for (int eventrow = 0; eventrow < this.GDocEventEngineDataGridView.Rows.Count; eventrow++)
            {
                ////this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[this.eventEngineData.GDocEventEngineDataTable.AddRowColumn.ColumnName].Appearance.Image = this.EventEngineRowHeader.Images[0];
                if (string.Equals(this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[this.eventEngineData.GDocEventEngineDataTable.levelsColumn.ColumnName].Value.ToString(), "1"))
                {
                    this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Appearance.Image = this.EventEngineParentImage.Images[0];

                    //// ((Button)(this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[0].Appearance.Image)).Click += new EventHandler(F8001_Click);
                    /////    this.gDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[0].Appearance.Image = this.EventEngineSChildImage.Images[0];
                }
                else if (string.Equals(this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[this.eventEngineData.GDocEventEngineDataTable.levelsColumn.ColumnName].Value.ToString(), "2"))
                {
                    ////PictureBox tempPictureBox = new PictureBox();
                    ////tempPictureBox.Image = this.EventEngineFChildImage.Images[0];
                    //////this.gDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[0].Appearance.Image = tempPictureBox;
                    this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Appearance.Image = this.EventEngineFChildImage.Images[0];
                }
                else if (string.Equals(this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[this.eventEngineData.GDocEventEngineDataTable.levelsColumn.ColumnName].Value.ToString(), "3"))
                {
                    this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Appearance.Image = this.EventEngineSChildImage.Images[0];
                }

                this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[this.eventEngineData.GDocEventEngineDataTable.TempColorColumn.ColumnName].Appearance.BackColor = (System.Drawing.Color)this.colorConv.ConvertFromString(this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[this.eventEngineData.GDocEventEngineDataTable.StatusColorColumn.ColumnName].Value.ToString());
                if (Convert.ToDateTime(this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells["EventDate"].Value.ToString()) > DateTime.Now)
                {
                    this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells["EventDate"].Appearance.BackColor = Color.FromArgb(252, 215, 116);
                }

                ////Coding added for the CO(2838) by malliga on 31/8/2009
                ////For Level'3' Plus sign must be removed
                if (!string.Equals(this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[this.eventEngineData.GDocEventEngineDataTable.levelsColumn.ColumnName].Value.ToString(), "3"))
                {
                    this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells["AddRow"].Appearance.ImageBackground = Properties.Resources.PlusSign; 
                    this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells["AddRow"].Appearance.ImageBackgroundStyle = ImageBackgroundStyle.Stretched;
                }
                else
                {
                    this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells["AddRow"].Appearance.BackColor = Color.Silver;   
                    this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells["AddRow"].Appearance.ImageBackgroundStyle = ImageBackgroundStyle.Default;
                }

                this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells["EventDate"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
                ////this.GDocEventEngineDataGridView.DisplayLayout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
                this.eventRowNo = this.eventRowNo + 1;
            }

            Infragistics.Win.UltraWinGrid.BandsCollection bands = this.GDocEventEngineDataGridView.DisplayLayout.Bands;

            for (int j = 0; j < bands.Count; j++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridBand band = bands[j];
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.AddRowColumn.ColumnName].Width = 25;
                ////band.Columns[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Width = 277;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.TempColorColumn.ColumnName].Width = 26;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.StatusColumn.ColumnName].Width = 117;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.EventDateColumn.ColumnName].Width = 115;
                ////band.Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Width = 96;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.IsCompleteColumn.ColumnName].Width = 67;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.EventNumberColumn.ColumnName].Width = 93;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.Child2Column.ColumnName].Hidden = true;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.Child1Column.ColumnName].Hidden = true;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.ParentColumn.ColumnName].Hidden = true;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.StatusColorColumn.ColumnName].Hidden = true;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.levelsColumn.ColumnName].Hidden = true;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.EventTypeIDColumn.ColumnName].Hidden = true;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.EventIDColumn.ColumnName].Hidden = true;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.IsWorkOrderColumn.ColumnName].Hidden = true;

                ////Code Added By Shiva for Change Request Sprint 22
                if (this.eventEngineData.GDocEventEngineDataTable.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(this.eventEngineData.GDocEventEngineDataTable.Rows[0][this.eventEngineData.GDocEventEngineDataTable.IsWorkOrderColumn.ColumnName]))
                    {
                        band.Columns[this.eventEngineData.GDocEventEngineDataTable.AddRowColumn.ColumnName].Width = 26;
                        band.Columns[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Width = 299;
                        band.Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Width = 116;
                        band.Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Hidden = false;
                        this.GdocEventEngineWorkOrderPanel.Visible = true;
                        this.EventTypePanel.Size = new System.Drawing.Size(300, 30);
                        this.GdocEventEngineTypeComboBox.Size = new System.Drawing.Size(285, 24);
                        this.StatusColorLabel.Location = new System.Drawing.Point(324, 0);
                        this.StatusPanel.Location = new System.Drawing.Point(350, 0);
                        this.GdocEventEngineDate.Location = new System.Drawing.Point(467, 0);
                        this.GdocEventEngineCompletePanel.Location = new System.Drawing.Point(582, 0);
                    }
                    else
                    {
                        band.Columns[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Width = 410;
                        band.Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Hidden = true;
                        band.Columns[this.eventEngineData.GDocEventEngineDataTable.StatusColumn.ColumnName].Width = 120;
                        band.Columns[this.eventEngineData.GDocEventEngineDataTable.EventDateColumn.ColumnName].Width = 120;
                        this.GdocEventEngineWorkOrderPanel.Visible = false;
                        this.EventTypePanel.Size = new System.Drawing.Size(410, 30);
                        this.GdocEventEngineTypeComboBox.Size = new System.Drawing.Size(401, 24);
                        this.StatusColorLabel.Location = new System.Drawing.Point(434, 0);
                        this.StatusPanel.Size = new System.Drawing.Size(200, 30);
                        this.GdocEventEngineStatusComboBox.Size = new System.Drawing.Size(110, 30);
                        this.StatusPanel.Location = new System.Drawing.Point(460, 0);
                        this.GdocEventEngineDate.Size = new System.Drawing.Size(300, 30);
                        this.GdocEventEngineDate.Location = new System.Drawing.Point(580, 0);
                        this.GdocEventEngineCompletePanel.Size = new System.Drawing.Size(80, 30);
                        this.GdocEventEngineCompletePanel.Location = new System.Drawing.Point(700, 0);
                    }
                }
                else
                {
                    ////band.Columns[this.eventEngineData.GDocEventEngineDataTable.AddRowColumn.ColumnName].Width = 26;
                    ////band.Columns[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Width = 410;
                    ////band.Columns[this.eventEngineData.GDocEventEngineDataTable.StatusColumn.ColumnName].Width = 120;
                    ////band.Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Width = 116;
                    ////band.Columns[this.eventEngineData.GDocEventEngineDataTable.IsCompleteColumn.ColumnName].Width = 67;

                    band.Columns[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Width = 410;
                    band.Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Hidden = true;
                    band.Columns[this.eventEngineData.GDocEventEngineDataTable.StatusColumn.ColumnName].Width = 120;
                    band.Columns[this.eventEngineData.GDocEventEngineDataTable.EventDateColumn.ColumnName].Width = 120;
                    this.GdocEventEngineWorkOrderPanel.Visible = false;
                    this.EventTypePanel.Size = new System.Drawing.Size(410, 30);
                    this.GdocEventEngineTypeComboBox.Size = new System.Drawing.Size(401, 24);
                    this.StatusColorLabel.Location = new System.Drawing.Point(434, 0);
                    this.StatusPanel.Size = new System.Drawing.Size(200, 30);
                    this.GdocEventEngineStatusComboBox.Size = new System.Drawing.Size(110, 30);
                    this.StatusPanel.Location = new System.Drawing.Point(460, 0);
                    this.GdocEventEngineDate.Size = new System.Drawing.Size(300, 30);
                    this.GdocEventEngineDate.Location = new System.Drawing.Point(580, 0);
                    this.GdocEventEngineCompletePanel.Size = new System.Drawing.Size(80, 30);
                    this.GdocEventEngineCompletePanel.Location = new System.Drawing.Point(700, 0);
                }
                //// Till Here
            }

            this.GDocEventEngineDataGridView.DisplayLayout.Override.RowSelectorHeaderStyle = RowSelectorHeaderStyle.ExtendFirstColumn;
            ////this.GDocEventEngineDataGridView.DisplayLayout.Override.DefaultColWidth = 2400;
        }

        /// <summary>
        /// Disablecontrols the specified status.
        /// </summary>
        /// <param name="status">if set to <c>true</c> [status].</param>
        private void Disablecontrol(bool status)
        {
            this.GDocEventEngineDataGridView.Enabled = status;
            this.GdocEventEngineStatusComboBox.Enabled = status;
            this.GdocEventEngineTypeComboBox.Enabled = status;
            this.GdocEventEngineCompleteCheckBox.Enabled = status;
            this.GdocEventEngineWorkOrderTextBox.Enabled = status;
            this.GdocEventEngineDate.Enabled = status;
            this.GdocEventEngineWorkOrderPictureBox.Enabled = status;
            this.GdocEventEngineDateImage.Enabled = status;
            this.GdocEventEngineRowAddLabel.Enabled = status;
        }

        /// <summary>
        /// Permissions the disable.
        /// </summary>
        private void PermissionDisable()
        {
            this.GdocEventEngineStatusComboBox.Enabled = false;
            this.GdocEventEngineTypeComboBox.Enabled = false;
            this.GdocEventEngineCompleteCheckBox.Enabled = false;
            this.GdocEventEngineWorkOrderTextBox.Enabled = false;
            this.GdocEventEngineDate.Enabled = false;
            this.GdocEventEngineRowAddLabel.Enabled = false;
            this.GDocEventEngineDataGridView.Focus();
            this.GdocEventEngineWorkOrderPictureBox.Enabled = false;
            this.GdocEventEngineDateImage.Enabled = false;
        }

        /// <summary>
        /// Sets the height of the smart part.
        /// </summary>
        /// <param name="recordCount">The record count.</param>
        private void SetSmartPartHeight(int recordCount)
        {
            int increment = (recordCount * 22);
            this.GDocEventEngineDataGridView.Height = increment + 50;
            this.EventEnginepictureBox.Height = this.GDocEventEngineDataGridView.Height - (recordCount - 1);
            if (recordCount > 20)
            {
                this.MainContentPanel.Height = this.EventEnginepictureBox.Height;
            }

            this.Height = this.EventEnginepictureBox.Height;
        }

        /// <summary>
        /// Saves the event engine.
        /// </summary>
        private void SaveEventEngine()
        {
            ////this.Cursor = Cursors.WaitCursor;
            this.currentRowLevel = "-1";
            this.CreateGdocEventEngineNewRow();
            if (this.saveStatus)
            {
                this.LoadEventEngineData();
                if (!this.flagLoadOnProcess)
                {
                    SliceResize sliceResize;
                    sliceResize.MasterFormNo = this.masterFormNo;
                    sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                    sliceResize.SliceFormHeight = this.EventEnginepictureBox.Height;
                    this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                    this.EventEnginepictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.EventEnginepictureBox.Height, this.EventEnginepictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                }

                if (this.newEventID > 0)
                {
                    this.FindShowId(this.newEventID.ToString());
                    this.SetNewMode();
                }
                else
                {
                    this.FindShowId("0");
                }

                this.GdocEventEngineTypeComboBox.Focus();
            }
            else if (this.newEventID == -1)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("WorkOrder") + this.GdocEventEngineWorkOrderTextBox.Text + SharedFunctions.GetResourceString("WorkOrderDoesnotExist"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("WorkOrderValidation")), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion User Defined Method

        /// <summary>
        /// GDocEventEngineCalenderControl_DateSelected
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GDocEventEngineCalenderControl_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                //// Assign the selected date to the DateTextbox.
                this.dataChangeStatus = true;
                this.GDocEventEngineDateTextBox.Text = e.Start.ToShortDateString();
                this.GDocEventEngineDateTextBox.Focus();
                this.GDocEventEngineCalenderControl.Visible = false;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// F85000_Resize
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F85000_Resize(object sender, EventArgs e)
        {
            try
            {
                this.Height = this.EventEnginepictureBox.Height;
                this.EventEnginepictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.EventEnginepictureBox.Height, this.EventEnginepictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// GdocEventEngineTypeComboBox_KeyPress
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GdocEventEngineTypeComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                this.EventEngineKeyEnter(e);
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Gs the doc key enter.
        /// </summary>
        /// <param name="e1">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void EventEngineKeyEnter(KeyPressEventArgs e1)
        {
            try
            {
                switch (e1.KeyChar)
                {
                    case (char)13:
                        {
                            this.SaveEventEngine();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// GDocEventEngineDataGridView_BeforeRowsDeleted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GDocEventEngineDataGridView_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            try
            {
                e.Cancel = true;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// EventEnginedateTimePicker_KeyPress
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void EventEnginedateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int keyCode = (int)e.KeyChar;
                if (keyCode == 9)
                {
                    SendKeys.Send("{Esc}");
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

            ////WorkOrderLinkLabel.Focus();
        }

        /// <summary>
        /// EventEnginedateTimePicker_CloseUp
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void EventEnginedateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.GDocEventEngineDateTextBox.Text = this.EventEnginedateTimePicker.Text;
                this.GdocEventEngineDateImage.BringToFront();
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #region Combo Box[Event Status,Event Type Events] 
        //// Coding added for the issue 2838
        /// <summary>
        /// Handles the Validating event of the GdocEventEngineStatusComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void GdocEventEngineStatusComboBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ////EventStatus combo box checking
                if (this.GdocEventEngineStatusComboBox.SelectedValue == null)
                {
                    this.GdocEventEngineStatusComboBox.Text = string.Empty;  
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the GdocEventEngineTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void GdocEventEngineTypeComboBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ////EventType combo box checking
                if (this.GdocEventEngineTypeComboBox.SelectedValue == null)
                {
                    this.GdocEventEngineTypeComboBox.Text = string.Empty;  
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion
    }
}
