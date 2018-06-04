// --------------------------------------------------------------------------------------------
// <copyright file="F96000.cs" company="Congruent">
// Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
// This file contains methods for the F96000.
// </summary>
// ----------------------------------------------------------------------------------------------
// Change History
// **********************************************************************************
// Date             Author             Description
// ----------      ---------       ---------------------------------------------------------
//  19-Dec-2008      khaja              made changes to fix the bug 3920
//  09-Feb-2009      khaja              made changes to fix the bugs 4476,4481,4478  3919
// *********************************************************************************/

namespace D91000
{
    #region NameSpace

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Reflection;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.SmartParts;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;

    #endregion NameSpace

    /// <summary>
    /// F96000 class
    /// </summary>
    public partial class F96000 : BaseSmartPart
    {
        #region Private Members

        /// <summary>
        /// Set Date Format
        /// </summary>
        private readonly string dateFormat = "M/d/yyyy";

        /// <summary>
        /// Form Number of the masterFormNo
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyId;

        /// <summary>
        /// formOwnerID from the Form Master
        /// </summary>
        private int formOwnerID;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// TerraScanCommon.PageStatus Enum - used to find normal or filtered mose
        /// </summary>
        private TerraScanCommon.PageStatus pageStatus;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        ///  Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails;

        /// <summary>
        /// F96000 Controller
        /// </summary>
        private F96000Controller form96000Control;

        /// <summary>
        /// F96000 OwnerManagement Data
        /// </summary>
        private F96000OwnerManagementData form96000OwnerManagementData;

        /// <summary>
        /// local variable for f96000ListOwnerStatusTypeData
        /// </summary>
        private F96000OwnerManagementData.F96000ListOwnerStatusTypeDataTable form96000ListOwnerStatusTypeData; 

        /// <summary>
        /// form Master PermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

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
        /// flag Load On Process
        /// </summary>
        private bool flagLoadOnProcess;
        
        /// <summary>
        /// the typed dataset for yes no combo box
        /// </summary>
        private CommonData activeComboData = new CommonData();

        /// <summary>
        /// the typed dataset for yes no combo box
        /// </summary>
        private CommonData activeComboGridData = new CommonData();

        /// <summary>
        /// the typed dataset for yes no combo box
        /// </summary>
        private CommonData priorityComboGridData = new CommonData();        

        /// <summary>
        /// DatePicker object
        /// </summary>
        private System.Windows.Forms.DateTimePicker validDate = new System.Windows.Forms.DateTimePicker();

        /// <summary>
        /// Local variable.
        /// </summary>
        private bool flagFormLoad;

        /// <summary>
        /// variable holds the CurrentRow Index
        /// </summary>
        private int gridCurrentRowIndex = -1;

        /// <summary>
        /// variable holds the CurrentColumn Index.
        /// </summary>
        private int gridCurrentColumnIndex = -1;

        /// <summary>
        /// variable holds the gridHeight.
        /// </summary>
        private int gridHeight = -1;

        /// <summary>
        /// the rowindex selected for delete
        /// </summary>
        private int selectedGridRowId;

        /// <summary>
        /// check Status Id
        /// </summary>
        private bool checkStatusId;

        /// <summary>
        /// owner Status Id
        /// </summary>
        private int ownerStatusId;

        /// <summary>
        /// delete Keydown
        /// </summary>
        private bool deleteKeydown = false;

        /// <summary>
        /// is Validated
        /// </summary>
        private bool isValidated = false;

        private int countryId;

        F96000OwnerManagementData CountryDetailsData = new F96000OwnerManagementData();

        #endregion Private Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F96000"/> class.
        /// </summary>
        public F96000()
        {
            InitializeComponent();            
            this.getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F96000"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red color.</param>
        /// <param name="green">The green color.</param>
        /// <param name="blue">The blue color.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F96000(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();            
            this.masterFormNo = masterform;            
            this.Tag = formNo;
            this.keyId = keyID;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.OwnersPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.OwnersPictureBox.Height, this.OwnersPictureBox.Width, this.sectionIndicatorText, 28, 81, 128);
            this.StatusListpicturebox.Image = ExtendedGraphics.GenerateVerticalImage(this.StatusListpicturebox.Height, this.StatusListpicturebox.Width, "Status List", 0, 51, 0);
            this.form96000OwnerManagementData = new F96000OwnerManagementData();
            this.getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();
            this.validDate.CustomFormat = this.dateFormat; //// "m/d/yyyy";
            this.validDate.MaxDate = new System.DateTime(2079, 6, 6, 0, 0, 0, 0);
            this.validDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.formMasterPermissionEdit = permissionEdit;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F96000"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red color.</param>
        /// <param name="green">The green color.</param>
        /// <param name="blue">The blue color.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        /// <param name="featureClassID">The featureClassID</param>
        public F96000(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassID)
        {
            this.InitializeComponent();            
            this.masterFormNo = masterform;            
            this.Tag = formNo;
            this.keyId = keyID;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.OwnersPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.OwnersPictureBox.Height, this.OwnersPictureBox.Width, this.sectionIndicatorText, 28, 81, 128);
            this.StatusListpicturebox.Image = ExtendedGraphics.GenerateVerticalImage(this.StatusListpicturebox.Height, this.StatusListpicturebox.Width, "Status List", 0, 51, 0);
            this.form96000OwnerManagementData = new F96000OwnerManagementData();
            this.getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();
            this.formMasterPermissionEdit = permissionEdit;
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// event publication for getting the form status
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_GetFormStatus, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<string>> GetFormStatus;

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
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the form96000Control.
        /// </summary>
        /// <value>The form96000Control.</value>
        [CreateNew]
        public F96000Controller Form96000Controller
        {
            get { return this.form96000Control as F96000Controller; }
            set { this.form96000Control = value; }
        }

        /// <summary>
        /// Gets or sets the page mode.
        /// </summary>
        /// <value>The page mode.</value>
        private TerraScanCommon.PageModeTypes PageMode
        {
            get
            {
                return this.pageMode;
            }

            set
            {
                this.pageMode = value;
            }
        }

        /// <summary>
        /// Gets or sets the page status.
        /// </summary>
        /// <value>The page status.</value>
        private TerraScanCommon.PageStatus PageStatus
        {
            get { return this.pageStatus; }
            set { this.pageStatus = value; }
        }
        #endregion Property

        #region Event Subscription

        /// <summary>
        /// Event Subscription LoadSliceDetails
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The Event.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.FlagSliceForm = true;
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.flagFormLoad = true;
                    this.LoadOwnerManagementDetails();
                    if (this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows.Count > 0)
                    {
                        this.StatusListDataGridView.DataSource = this.form96000OwnerManagementData.F96000GetStatusList.DefaultView;
                    }
                    else
                    {
                        this.form96000OwnerManagementData.F96000GetStatusList.Clear();
                        this.StatusListDataGridView.DataSource = this.form96000OwnerManagementData.F96000GetStatusList.DefaultView;
                    }

                    if (this.form96000OwnerManagementData.F96000GetStatusList.Rows.Count > this.StatusListDataGridView.NumRowsVisible)
                    {
                        this.OwnerManagementVscrollBar.Enabled = true;
                        this.OwnerManagementVscrollBar.Visible = false;
                    }
                    else
                    {
                        this.OwnerManagementVscrollBar.Enabled = false;
                        this.OwnerManagementVscrollBar.Visible = true;
                    }

                    this.LoadOwnerDetails();
                    this.flagFormLoad = false;

                    if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                    {
                        this.LockControls(false);
                    }
                    else
                    {
                        this.LockControls(true);
                    }
                }

                ////code to add a new row in the grid,during form load(if in the case only the immediate next row is editable.
                if (this.StatusListDataGridView.OriginalRowCount >= this.StatusListDataGridView.NumRowsVisible)
                {
                    if (!Convert.ToBoolean(this.form96000OwnerManagementData.F96000GetStatusList.Rows[this.form96000OwnerManagementData.F96000GetStatusList.Rows.Count - 1][this.StatusListDataGridView.EmptyRecordColumnName]))
                    {
                        this.form96000OwnerManagementData.F96000GetStatusList.Rows.Add(this.form96000OwnerManagementData.F96000GetStatusList.NewRow());
                        this.form96000OwnerManagementData.F96000GetStatusList.Rows[this.form96000OwnerManagementData.F96000GetStatusList.Rows.Count - 1][this.StatusListDataGridView.EmptyRecordColumnName] = true;
                        this.OwnerManagementVscrollBar.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

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
            //// code updated to fix Bug #3920 by khaja on 19-12-08            
            if (this.slicePermissionField.newPermission)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.formOwnerID = 0;
                this.CustomizeCombobox();
                this.LoadCountryComboBox();
                this.LoadDefalutCountry();
               // this.keyId = -99;
                this.ClearOwnerManagementDetails();
                this.ClearStatusListGrid();
                this.LoadOwnerManagementDetails();
                this.LockControls(false);
                this.LastNameTextBox.Focus();
                this.CopyButton.Enabled = false;
            }
            else
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearOwnerManagementDetails();
                this.LockControls(true);
            }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            try
            {
                if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                {                    
                        SliceValidationFields sliceValidationFields = new SliceValidationFields();
                        sliceValidationFields.FormNo = eventArgs.Data;
                        this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));                  
                }
                else
                {
                    SliceValidationFields sliceValidationFields = new SliceValidationFields();
                    sliceValidationFields.FormNo = eventArgs.Data;
                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
                }   
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                {
                    
                        int currentKeyId = this.SaveOwnerManagementDetails();
                        if (currentKeyId != -1)
                        {
                            SliceReloadActiveRecord currentSliceInfo;
                            currentSliceInfo.MasterFormNo = this.masterFormNo;
                            currentSliceInfo.SelectedKeyId = currentKeyId;

                            ////to reload the form with the current keyid(this.valveId)
                            ////to refresh the master form with the return keyid                
                            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                        }  
                }  
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.getFormDetailsDataDetails = this.form96000Control.WorkItem.GetFormDetails(Convert.ToInt32(this.Tag), eventArgs.Data.SelectUserId);
                    if (this.getFormDetailsDataDetails.Rows.Count > 0)
                    {
                        this.slicePermissionField.deletePermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][this.getFormDetailsDataDetails.IsPermissionDeleteColumn.ColumnName].ToString());
                        this.slicePermissionField.editPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][this.getFormDetailsDataDetails.IsPermissionEditColumn.ColumnName].ToString());
                        this.slicePermissionField.newPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][this.getFormDetailsDataDetails.IsPermissionAddColumn.ColumnName].ToString());
                        this.slicePermissionField.openPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][this.getFormDetailsDataDetails.IsPermissionOpenColumn.ColumnName].ToString());
                    }

                    if (this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows.Count > 0)
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

                if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                {
                    if (this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows.Count > 0)
                    {
                        this.LockControls(false);
                    }
                    else
                    {
                        this.LockControls(true);
                    }
                }
                else
                {
                    this.LockControls(true);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.flagFormLoad = true;
            this.OwnerManagementGridViewpanel.Focus();
            this.ClearOwnerManagementDetails();
            this.LoadOwnerDetails();
            this.LoadOwnerManagementDetails(); 
            this.EnableGridSorting();
            this.PopulateDataGridView();            
            this.flagFormLoad = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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

        #region Event Handlers

        /// <summary>
        /// Handles the Load event of the F96000 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F96000_Load(object sender, EventArgs e)
        {
            try 
            {
                this.deleteKeydown = false;
                this.flagFormLoad = true;
                this.FlagSliceForm = true;
                this.pageMode = TerraScanCommon.PageModeTypes.View;                
                this.CustomizeCombobox();
                this.LoadCountryComboBox();
                this.LoadOwnerManagementDetails();
                this.LoadOwnerDetails();
                this.CustomizeOwnerStatusGrid();
                this.PopulateDataGridView();
                this.flagFormLoad = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the AllTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AllTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    if (!this.deleteKeydown)
                    {
                        if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                        {
                            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                            this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                            this.DisableGridSorting();
                            this.CopyButton.Enabled = false;
                        }

                        this.deleteKeydown = false;
                    }
                }                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Event Handlers

        #region StatusList Grid View Events 

        /// <summary>
        /// Handles the RowEnter event of the StatusListDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void StatusListDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ////code to make the rows other than the editable row as read-only.
                bool hasValues = false;
                if (e.RowIndex >= 1)
                {
                    if (string.IsNullOrEmpty(this.StatusListDataGridView["Status", (e.RowIndex - 1)].Value.ToString().Trim()) && string.IsNullOrEmpty(this.StatusListDataGridView["BeginDate", (e.RowIndex - 1)].Value.ToString().Trim()) && string.IsNullOrEmpty(this.StatusListDataGridView["EndDate", (e.RowIndex - 1)].Value.ToString().Trim()) && string.IsNullOrEmpty(this.StatusListDataGridView["Active", (e.RowIndex - 1)].Value.ToString().Trim()) && string.IsNullOrEmpty(this.StatusListDataGridView["Priority", (e.RowIndex - 1)].Value.ToString().Trim()) && string.IsNullOrEmpty(this.StatusListDataGridView["Note", (e.RowIndex - 1)].Value.ToString().Trim()))
                    {
                        if (e.RowIndex + 1 < StatusListDataGridView.RowCount)
                        {
                            for (int i = e.RowIndex; i < StatusListDataGridView.RowCount; i++)
                            {
                                if (!string.IsNullOrEmpty(this.StatusListDataGridView.Rows[i].Cells["Status"].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.StatusListDataGridView.Rows[i].Cells["BeginDate"].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.StatusListDataGridView.Rows[i].Cells["EndDate"].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.StatusListDataGridView.Rows[i].Cells["Active"].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.StatusListDataGridView.Rows[i].Cells["Priority"].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.StatusListDataGridView.Rows[i].Cells["Note"].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.StatusListDataGridView.Rows[i].Cells["Status"].Value.ToString().Trim()))
                                {
                                    hasValues = true;
                                    break;
                                }
                            }

                            if (hasValues)
                            {
                                this.StatusListDataGridView["Status", e.RowIndex].ReadOnly = false;
                                this.StatusListDataGridView["BeginDate", e.RowIndex].ReadOnly = false;
                                this.StatusListDataGridView["EndDate", e.RowIndex].ReadOnly = false;
                                this.StatusListDataGridView["Active", e.RowIndex].ReadOnly = false;
                                this.StatusListDataGridView["Priority", e.RowIndex].ReadOnly = false;
                                this.StatusListDataGridView["Note", e.RowIndex].ReadOnly = false;
                                this.StatusListDataGridView.Rows[e.RowIndex].Selected = false;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(this.StatusListDataGridView["Status", e.RowIndex].Value.ToString().Trim()) && string.IsNullOrEmpty(this.StatusListDataGridView["BeginDate", e.RowIndex].Value.ToString().Trim()) && string.IsNullOrEmpty(this.StatusListDataGridView["EndDate", e.RowIndex].Value.ToString().Trim()) && string.IsNullOrEmpty(this.StatusListDataGridView["Active", e.RowIndex].Value.ToString().Trim()) && string.IsNullOrEmpty(this.StatusListDataGridView["Priority", e.RowIndex].Value.ToString().Trim()) && string.IsNullOrEmpty(this.StatusListDataGridView["Note", e.RowIndex].Value.ToString().Trim()))
                                {
                                    this.StatusListDataGridView["Status", e.RowIndex].ReadOnly = true;
                                    this.StatusListDataGridView["BeginDate", e.RowIndex].ReadOnly = true;
                                    this.StatusListDataGridView["EndDate", e.RowIndex].ReadOnly = true;
                                    this.StatusListDataGridView["Active", e.RowIndex].ReadOnly = true;
                                    this.StatusListDataGridView["Priority", e.RowIndex].ReadOnly = true;
                                    this.StatusListDataGridView["Note", e.RowIndex].ReadOnly = true;
                                    this.StatusListDataGridView.Rows[e.RowIndex].Selected = true;
                                }
                                else
                                {
                                    this.StatusListDataGridView["Status", e.RowIndex].ReadOnly = false;
                                    this.StatusListDataGridView["BeginDate", e.RowIndex].ReadOnly = false;
                                    this.StatusListDataGridView["EndDate", e.RowIndex].ReadOnly = false;
                                    this.StatusListDataGridView["Active", e.RowIndex].ReadOnly = false;
                                    this.StatusListDataGridView["Priority", e.RowIndex].ReadOnly = false;
                                    this.StatusListDataGridView["Note", e.RowIndex].ReadOnly = false;
                                    this.StatusListDataGridView.Rows[e.RowIndex].Selected = false;
                                }
                            }
                        }
                        else
                        {
                            this.StatusListDataGridView["Status", e.RowIndex].ReadOnly = true;
                            this.StatusListDataGridView["BeginDate", e.RowIndex].ReadOnly = true;
                            this.StatusListDataGridView["EndDate", e.RowIndex].ReadOnly = true;
                            this.StatusListDataGridView["Active", e.RowIndex].ReadOnly = true;
                            this.StatusListDataGridView["Priority", e.RowIndex].ReadOnly = true;
                            this.StatusListDataGridView["Note", e.RowIndex].ReadOnly = true;
                        }
                    }
                    else
                    {
                        this.StatusListDataGridView["Status", e.RowIndex].ReadOnly = false;
                        this.StatusListDataGridView["BeginDate", e.RowIndex].ReadOnly = false;
                        this.StatusListDataGridView["EndDate", e.RowIndex].ReadOnly = false;
                        this.StatusListDataGridView["Active", e.RowIndex].ReadOnly = false;
                        this.StatusListDataGridView["Priority", e.RowIndex].ReadOnly = false;
                        this.StatusListDataGridView["Note", e.RowIndex].ReadOnly = false;
                        this.StatusListDataGridView.Rows[e.RowIndex].Selected = false;
                    }
                }

                if (e.RowIndex == 0)
                {
                    this.StatusListDataGridView["Status", e.RowIndex].ReadOnly = false;
                    this.StatusListDataGridView["BeginDate", e.RowIndex].ReadOnly = false;
                    this.StatusListDataGridView["EndDate", e.RowIndex].ReadOnly = false;
                    this.StatusListDataGridView["Active", e.RowIndex].ReadOnly = false;
                    this.StatusListDataGridView["Priority", e.RowIndex].ReadOnly = false;
                    this.StatusListDataGridView["Note", e.RowIndex].ReadOnly = false;
                    this.StatusListDataGridView.Rows[e.RowIndex].Selected = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowHeaderMouseClick event of the StatusListDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void StatusListDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.StatusListDataGridView.CurrentCell != null)
                {
                    TerraScanCommon.SetDataGridViewPosition(this.StatusListDataGridView, e.RowIndex);
                }

                foreach (DataGridViewRow dataRowCollection in this.StatusListDataGridView.Rows)
                {
                    TerraScanTextAndImageCell beginingCell = (TerraScanTextAndImageCell)dataRowCollection.Cells[this.StatusListDataGridView.Columns["BeginDate"].Index];
                    TerraScanTextAndImageCell endingCell = (TerraScanTextAndImageCell)dataRowCollection.Cells[this.StatusListDataGridView.Columns["EndDate"].Index];

                    beginingCell.ImagexLocation = 90;
                    beginingCell.ImageyLocation = 3;

                    endingCell.ImagexLocation = 90;
                    endingCell.ImageyLocation = 3;

                    if (dataRowCollection.Index == e.RowIndex)
                    {
                        beginingCell.Image = Properties.Resources.calendarImage;
                        endingCell.Image = Properties.Resources.calendarImage;
                    }
                    else
                    {
                        if (e.RowIndex >= 0)
                        {
                            try
                            {
                                beginingCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.StatusListDataGridView[this.StatusListDataGridView.Columns["BeginDate"].Index, dataRowCollection.Index].InheritedStyle.BackColor.R), Convert.ToInt32(this.StatusListDataGridView[this.StatusListDataGridView.Columns["BeginDate"].Index, dataRowCollection.Index].InheritedStyle.BackColor.G), Convert.ToInt32(this.StatusListDataGridView[this.StatusListDataGridView.Columns["BeginDate"].Index, dataRowCollection.Index].InheritedStyle.BackColor.B));
                                endingCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.StatusListDataGridView[this.StatusListDataGridView.Columns["EndDate"].Index, dataRowCollection.Index].InheritedStyle.BackColor.R), Convert.ToInt32(this.StatusListDataGridView[this.StatusListDataGridView.Columns["EndDate"].Index, dataRowCollection.Index].InheritedStyle.BackColor.G), Convert.ToInt32(this.StatusListDataGridView[this.StatusListDataGridView.Columns["EndDate"].Index, dataRowCollection.Index].InheritedStyle.BackColor.B));
                            }
                            catch (Exception ex)
                            {
                                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                            }
                        }
                    }
                }

                this.StatusListDataGridView.Refresh();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }            
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the StatusListDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void StatusListDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.StatusListDataGridView.Rows.Count > 0)
                {
                    this.StatusListDataGridView.Rows[0].Selected = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the StatusListDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void StatusListDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {                
                e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
                e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                e.Control.Validated += new EventHandler(this.Control_Validated);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validated event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_Validated(object sender, EventArgs e)
        {
            try
            {
                this.StatusListDataGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ////Here the variable/method or whatever which causes the unsaved change to fire can be written                
                if (!this.flagFormLoad)
                {
                    if (!this.deleteKeydown)
                    {
                        if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                        {
                            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                            this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                            this.DisableGridSorting();
                        }

                        this.deleteKeydown = false;
                    }                   
                }              
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StatusListDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            { 
                if (((e.RowIndex + 1) == this.StatusListDataGridView.Rows.Count) && (e.ColumnIndex != 4) && (e.ColumnIndex != 5))
                {
                    if (this.StatusListDataGridView.Rows[e.RowIndex].Cells[0].Value != null && !string.IsNullOrEmpty(this.StatusListDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.StatusListDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString().Trim()))
                    {
                        (StatusListDataGridView.Rows[e.RowIndex].Cells[this.form96000OwnerManagementData.F96000GetStatusList.IsActiveColumn.ColumnName.ToString()] as DataGridViewComboBoxCell).Value = 2;                        
                    }
                }

                ////code to add a row and to make it editable only that row.
                if (this.form96000OwnerManagementData.F96000GetStatusList.Rows.Count == e.RowIndex + 1)
                {
                    if (!string.IsNullOrEmpty(this.StatusListDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString().Trim()))
                    {
                        this.form96000OwnerManagementData.F96000GetStatusList.AddF96000GetStatusListRow(this.form96000OwnerManagementData.F96000GetStatusList.NewF96000GetStatusListRow());
                        this.form96000OwnerManagementData.F96000GetStatusList.Rows[this.form96000OwnerManagementData.F96000GetStatusList.Rows.Count - 1][this.StatusListDataGridView.EmptyRecordColumnName] = true;
                        this.PopulateDataGridView();
                    }
                }                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// To enable the savebutton status
        /// </summary>
        private void EnableSaveStatus()
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.deleteKeydown)
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                        this.DisableGridSorting();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }  

        #region Added By Shiva

        /// <summary>
        /// Handles the CellClick event of the StatusListDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void StatusListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {               
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    foreach (DataGridViewRow dataRowCollection in this.StatusListDataGridView.Rows)
                    {
                        TerraScanTextAndImageCell beginingCell = (TerraScanTextAndImageCell)dataRowCollection.Cells[this.StatusListDataGridView.Columns["BeginDate"].Index];
                        TerraScanTextAndImageCell endingCell = (TerraScanTextAndImageCell)dataRowCollection.Cells[this.StatusListDataGridView.Columns["EndDate"].Index];

                        beginingCell.ImagexLocation = 90;
                        beginingCell.ImageyLocation = 3;

                        endingCell.ImagexLocation = 90;
                        endingCell.ImageyLocation = 3;

                        if (dataRowCollection.Index == e.RowIndex)
                        {
                            beginingCell.Image = Properties.Resources.calendarImage;
                            endingCell.Image = Properties.Resources.calendarImage;                            
                        }
                        else
                        {
                            if (e.RowIndex >= 0)
                            {
                                try
                                {
                                    beginingCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.StatusListDataGridView[this.StatusListDataGridView.Columns["BeginDate"].Index, dataRowCollection.Index].InheritedStyle.BackColor.R), Convert.ToInt32(this.StatusListDataGridView[this.StatusListDataGridView.Columns["BeginDate"].Index, dataRowCollection.Index].InheritedStyle.BackColor.G), Convert.ToInt32(this.StatusListDataGridView[this.StatusListDataGridView.Columns["BeginDate"].Index, dataRowCollection.Index].InheritedStyle.BackColor.B));
                                    endingCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.StatusListDataGridView[this.StatusListDataGridView.Columns["EndDate"].Index, dataRowCollection.Index].InheritedStyle.BackColor.R), Convert.ToInt32(this.StatusListDataGridView[this.StatusListDataGridView.Columns["EndDate"].Index, dataRowCollection.Index].InheritedStyle.BackColor.G), Convert.ToInt32(this.StatusListDataGridView[this.StatusListDataGridView.Columns["EndDate"].Index, dataRowCollection.Index].InheritedStyle.BackColor.B));
                                }
                                catch (Exception ex)
                                {
                                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                                }
                            }
                        }
                    }
                }

                this.StatusListDataGridView.Refresh();                   

                bool hasValues = false;
                if (e.RowIndex >= 1)
                {
                    if (string.IsNullOrEmpty(this.StatusListDataGridView["Status", (e.RowIndex - 1)].Value.ToString().Trim()) && string.IsNullOrEmpty(this.StatusListDataGridView["BeginDate", (e.RowIndex - 1)].Value.ToString().Trim()) && string.IsNullOrEmpty(this.StatusListDataGridView["EndDate", (e.RowIndex - 1)].Value.ToString().Trim()) && string.IsNullOrEmpty(this.StatusListDataGridView["Active", (e.RowIndex - 1)].Value.ToString().Trim()) && string.IsNullOrEmpty(this.StatusListDataGridView["Priority", (e.RowIndex - 1)].Value.ToString().Trim()) && string.IsNullOrEmpty(this.StatusListDataGridView["Note", (e.RowIndex - 1)].Value.ToString().Trim()))
                    {
                        if (e.RowIndex + 1 < StatusListDataGridView.RowCount)
                        {
                            for (int i = e.RowIndex; i < StatusListDataGridView.RowCount; i++)
                            {
                                if (!string.IsNullOrEmpty(this.StatusListDataGridView.Rows[i].Cells["Status"].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.StatusListDataGridView.Rows[i].Cells["BeginDate"].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.StatusListDataGridView.Rows[i].Cells["EndDate"].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.StatusListDataGridView.Rows[i].Cells["Active"].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.StatusListDataGridView.Rows[i].Cells["Priority"].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.StatusListDataGridView.Rows[i].Cells["Note"].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.StatusListDataGridView.Rows[i].Cells["Status"].Value.ToString().Trim()))
                                {
                                    hasValues = true;
                                    break;
                                }
                            }

                            if (hasValues)
                            {
                                this.StatusListDataGridView["Status", e.RowIndex].ReadOnly = false;
                                this.StatusListDataGridView["BeginDate", e.RowIndex].ReadOnly = false;
                                this.StatusListDataGridView["EndDate", e.RowIndex].ReadOnly = false;
                                this.StatusListDataGridView["Active", e.RowIndex].ReadOnly = false;
                                this.StatusListDataGridView["Priority", e.RowIndex].ReadOnly = false;
                                this.StatusListDataGridView["Note", e.RowIndex].ReadOnly = false;
                                this.StatusListDataGridView.Rows[e.RowIndex].Selected = false;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(this.StatusListDataGridView["Status", e.RowIndex].Value.ToString().Trim()) && string.IsNullOrEmpty(this.StatusListDataGridView["BeginDate", e.RowIndex].Value.ToString().Trim()) && string.IsNullOrEmpty(this.StatusListDataGridView["EndDate", e.RowIndex].Value.ToString().Trim()) && string.IsNullOrEmpty(this.StatusListDataGridView["Active", e.RowIndex].Value.ToString().Trim()) && string.IsNullOrEmpty(this.StatusListDataGridView["Priority", e.RowIndex].Value.ToString().Trim()) && string.IsNullOrEmpty(this.StatusListDataGridView["Note", e.RowIndex].Value.ToString().Trim()))
                                {
                                    this.StatusListDataGridView["Status", e.RowIndex].ReadOnly = true;
                                    this.StatusListDataGridView["BeginDate", e.RowIndex].ReadOnly = true;
                                    this.StatusListDataGridView["EndDate", e.RowIndex].ReadOnly = true;
                                    this.StatusListDataGridView["Active", e.RowIndex].ReadOnly = true;
                                    this.StatusListDataGridView["Priority", e.RowIndex].ReadOnly = true;
                                    this.StatusListDataGridView["Note", e.RowIndex].ReadOnly = true;
                                    this.StatusListDataGridView.Rows[e.RowIndex].Selected = true;
                                }
                                else
                                {
                                    this.StatusListDataGridView["Status", e.RowIndex].ReadOnly = false;
                                    this.StatusListDataGridView["BeginDate", e.RowIndex].ReadOnly = false;
                                    this.StatusListDataGridView["EndDate", e.RowIndex].ReadOnly = false;
                                    this.StatusListDataGridView["Active", e.RowIndex].ReadOnly = false;
                                    this.StatusListDataGridView["Priority", e.RowIndex].ReadOnly = false;
                                    this.StatusListDataGridView["Note", e.RowIndex].ReadOnly = false;
                                    this.StatusListDataGridView.Rows[e.RowIndex].Selected = false;
                                }
                            }
                        }
                        else
                        {
                            this.StatusListDataGridView["Status", e.RowIndex].ReadOnly = true;
                            this.StatusListDataGridView["BeginDate", e.RowIndex].ReadOnly = true;
                            this.StatusListDataGridView["EndDate", e.RowIndex].ReadOnly = true;
                            this.StatusListDataGridView["Active", e.RowIndex].ReadOnly = true;
                            this.StatusListDataGridView["Priority", e.RowIndex].ReadOnly = true;
                            this.StatusListDataGridView["Note", e.RowIndex].ReadOnly = true;
                        }
                    }
                    else
                    {
                        this.StatusListDataGridView["Status", e.RowIndex].ReadOnly = false;
                        this.StatusListDataGridView["BeginDate", e.RowIndex].ReadOnly = false;
                        this.StatusListDataGridView["EndDate", e.RowIndex].ReadOnly = false;
                        this.StatusListDataGridView["Active", e.RowIndex].ReadOnly = false;
                        this.StatusListDataGridView["Priority", e.RowIndex].ReadOnly = false;
                        this.StatusListDataGridView["Note", e.RowIndex].ReadOnly = false;
                        this.StatusListDataGridView.Rows[e.RowIndex].Selected = false;
                    }
                }

                if (e.RowIndex == 0)
                {
                    this.StatusListDataGridView["Status", e.RowIndex].ReadOnly = false;
                    this.StatusListDataGridView["BeginDate", e.RowIndex].ReadOnly = false;
                    this.StatusListDataGridView["EndDate", e.RowIndex].ReadOnly = false;
                    this.StatusListDataGridView["Active", e.RowIndex].ReadOnly = false;
                    this.StatusListDataGridView["Priority", e.RowIndex].ReadOnly = false;
                    this.StatusListDataGridView["Note", e.RowIndex].ReadOnly = false;
                    this.StatusListDataGridView.Rows[e.RowIndex].Selected = false;                   
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellMouseClick event of the StatusListDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void StatusListDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
               //// this.isValidated = false; 
                if ((e.ColumnIndex.Equals(this.StatusListDataGridView.Columns["BeginDate"].Index) || e.ColumnIndex.Equals(this.StatusListDataGridView.Columns["EndDate"].Index)) && (e.RowIndex >= 0) && (e.ColumnIndex >= 0))
                {
                    if (!this.StatusListDataGridView.Rows[e.RowIndex].ReadOnly)
                    {                        
                        if (!Convert.ToBoolean(this.form96000OwnerManagementData.F96000GetStatusList.Rows[e.RowIndex][this.StatusListDataGridView.EmptyRecordColumnName]))
                        {
                            if ((e.X >= 91) && (e.X <= (110 - 4)) && (e.Y >= 3) && (e.Y <= (22 - 3)))
                            {
                                if (e.ColumnIndex.Equals(this.StatusListDataGridView.Columns["BeginDate"].Index))
                                {
                                    this.OwnerDateTimePicker.Left = this.OwnerManagementGridViewpanel.Left + this.StatusListDataGridView.RowHeadersWidth + this.StatusListDataGridView.Columns[0].Width + 122;
                                    this.OwnerDateTimePicker.Top = this.OwnerManagementGridViewpanel.Top + this.gridHeight - e.Y; ////+ (22 - e.Y); 

                                    this.gridCurrentColumnIndex = e.ColumnIndex;
                                    this.gridCurrentRowIndex = e.RowIndex;
                                    this.ShowOwnerDateTimePicker();
                                }
                                else if (e.ColumnIndex.Equals(this.StatusListDataGridView.Columns["EndDate"].Index))
                                {
                                    this.OwnerDateTimePicker.Left = this.OwnerManagementGridViewpanel.Left + this.StatusListDataGridView.RowHeadersWidth + this.StatusListDataGridView.Columns[0].Width + this.StatusListDataGridView.Columns[1].Width + 98;
                                    this.OwnerDateTimePicker.Top = this.OwnerManagementGridViewpanel.Top + this.gridHeight - e.Y; // +(22 - e.Y);

                                    this.gridCurrentColumnIndex = e.ColumnIndex;
                                    this.gridCurrentRowIndex = e.RowIndex;
                                    this.ShowOwnerDateTimePicker();
                                }
                            }
                       }
                    }                     
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Handles the CellParsing event of the StatusListDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void StatusListDataGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {  
            try
            {
                if (e.ColumnIndex.Equals(this.StatusListDataGridView.Columns["BeginDate"].Index) || e.ColumnIndex.Equals(this.StatusListDataGridView.Columns["EndDate"].Index))
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    //// Only paint if text provided, Only paint if desired text is in cell

                    if (e.Value != null)
                    {
                        string tempvalue = e.Value.ToString().Trim();
   
                        try
                        {
                            if (!string.IsNullOrEmpty(tempvalue))
                            {
                                this.StatusListDataGridView.RefreshEdit();
                                DateTime outDate;
                                DateTime.TryParse(tempvalue, out outDate);
                                this.validDate.Value = outDate;
                                this.StatusListDataGridView[e.ColumnIndex, this.StatusListDataGridView.CurrentRowIndex].Value = this.validDate.Value.ToString(this.dateFormat);
                            }
                        }
                        catch
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("ValidDate") + "\n" + SharedFunctions.GetResourceString("Allowed") + "\n" + SharedFunctions.GetResourceString("MinDate") + this.validDate.MinDate.ToShortDateString() + "\n" + SharedFunctions.GetResourceString("MaxDate") + this.validDate.MaxDate.ToShortDateString(), SharedFunctions.GetResourceString("DateValidation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            e.Value = DateTime.Now.ToString(this.dateFormat);
                            this.StatusListDataGridView[e.ColumnIndex, this.StatusListDataGridView.CurrentRowIndex].Value = DateTime.Now.ToString(this.dateFormat);
                            //// TODO
                        }

                        e.ParsingApplied = true;
                        this.StatusListDataGridView.RefreshEdit();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataError event of the StatusListDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void StatusListDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellValidated event of the StatusListDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void StatusListDataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex.Equals(this.StatusListDataGridView.Columns["BeginDate"].Index) || e.ColumnIndex.Equals(this.StatusListDataGridView.Columns["EndDate"].Index))
                {
                    if (!string.IsNullOrEmpty(this.StatusListDataGridView[e.ColumnIndex, this.StatusListDataGridView.CurrentRowIndex].Value.ToString().Trim()))
                    {
                        DateTime tempValue = new DateTime();
                        if (DateTime.TryParse(this.StatusListDataGridView[e.ColumnIndex, this.StatusListDataGridView.CurrentRowIndex].Value.ToString().Trim(), null, System.Globalization.DateTimeStyles.RoundtripKind, out tempValue))
                        {
                            try
                            {
                                this.validDate.Value = DateTime.Parse(this.StatusListDataGridView[e.ColumnIndex, this.StatusListDataGridView.CurrentRowIndex].Value.ToString());
                                this.StatusListDataGridView[e.ColumnIndex, this.StatusListDataGridView.CurrentRowIndex].Value = this.validDate.Value.ToString(this.dateFormat);
                            }
                            catch
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("ValidDate") + "\n" + SharedFunctions.GetResourceString("Allowed") + "\n" + SharedFunctions.GetResourceString("MinDate") + this.validDate.MinDate.ToShortDateString() + "\n" + SharedFunctions.GetResourceString("MaxDate") + this.validDate.MaxDate.ToShortDateString(), SharedFunctions.GetResourceString("DateValidation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.StatusListDataGridView[e.ColumnIndex, this.StatusListDataGridView.CurrentRowIndex].Value = DateTime.Now.ToString(this.dateFormat);
                            }
                        }
                        else
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("ValidDate") + "\n" + SharedFunctions.GetResourceString("Allowed") + "\n" + SharedFunctions.GetResourceString("MinDate") + this.validDate.MinDate.ToShortDateString() + "\n" + SharedFunctions.GetResourceString("MaxDate") + this.validDate.MaxDate.ToShortDateString(), SharedFunctions.GetResourceString("DateValidation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.StatusListDataGridView[e.ColumnIndex, this.StatusListDataGridView.CurrentRowIndex].Value = DateTime.Now.ToString(this.dateFormat);
                        }
                    }
                }
                ////added by biju
                if (e.ColumnIndex.Equals(3))
                {
                    DateTime endDate;
                    DateTime beginDate;
                    if (!String.IsNullOrEmpty(StatusListDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString()) && (!String.IsNullOrEmpty(StatusListDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString())))
                    {
                        this.StatusListDataGridView[e.ColumnIndex, e.RowIndex].Value = this.StatusListDataGridView[e.ColumnIndex, e.RowIndex].Value;
                        DateTime.TryParse(StatusListDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString(), out endDate);

                        DateTime.TryParse(StatusListDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(), out beginDate);

                        if (DateTime.Compare(endDate, beginDate) < 0)
                        {
                            MessageBox.Show("End date should not be lesser than begin date", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (e.ColumnIndex > 0 && e.RowIndex >= 0)
                            {
                                this.StatusListDataGridView[e.ColumnIndex, e.RowIndex].Value = string.Empty;
                            }
                        }
                    }
                }
                else if (e.ColumnIndex.Equals(2))
                {
                    if (!String.IsNullOrEmpty(StatusListDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString()) && !String.IsNullOrEmpty(StatusListDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString()))
                    {
                        this.StatusListDataGridView[e.ColumnIndex, this.StatusListDataGridView.CurrentRowIndex].Value = StatusListDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                        DateTime endDate;
                        DateTime.TryParse(StatusListDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString(), out endDate);
                        DateTime beginDate;
                        DateTime.TryParse(StatusListDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(), out beginDate);

                        if (DateTime.Compare(endDate, beginDate) < 0)
                        {
                            MessageBox.Show("Begin date should not be greater than end date", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (e.ColumnIndex > 0 && e.RowIndex >= 0)
                            {
                                this.StatusListDataGridView[e.ColumnIndex, e.RowIndex].Value = string.Empty;
                            }
                        }
                    }
                } 
                ////else if (e.ColumnIndex.Equals(1) & !this.isValidated)
                ////{
                ////    ////Coding added for the issue 1108 
                ////    if (e.RowIndex > 0)
                ////    {
                ////        this.isValidated = true;
                ////        this.StatusListDataGridView.CurrentCell = this.StatusListDataGridView[e.ColumnIndex + 1, e.RowIndex];
                ////    }
                ////}
                ////////Ends here
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseClick event of the StatusListDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void StatusListDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.gridHeight = e.Y;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the StatusListDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void StatusListDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
             {
                 if (this.StatusListDataGridView.Rows.Count > 0)
                 {
                     string ownerStatus = (this.StatusListDataGridView.Rows[e.RowIndex].Cells["status"] as DataGridViewComboBoxCell).Value.ToString();

                     if (!string.IsNullOrEmpty(ownerStatus))
                     {
                         this.checkStatusId = true;
                     }
                     else
                     {
                         this.checkStatusId = false;
                     }

                     if (!string.IsNullOrEmpty(this.StatusListDataGridView.Rows[e.RowIndex].Cells["Status"].Value.ToString()) || !string.IsNullOrEmpty(this.StatusListDataGridView.Rows[e.RowIndex].Cells["BeginDate"].Value.ToString()) || !string.IsNullOrEmpty(this.StatusListDataGridView.Rows[e.RowIndex].Cells["EndDate"].Value.ToString()) || !string.IsNullOrEmpty(this.StatusListDataGridView.Rows[e.RowIndex].Cells["Active"].Value.ToString()) || !string.IsNullOrEmpty(this.StatusListDataGridView.Rows[e.RowIndex].Cells["Priority"].Value.ToString()) || !string.IsNullOrEmpty(this.StatusListDataGridView.Rows[e.RowIndex].Cells["Note"].Value.ToString()))
                     {
                         if (((this.StatusListDataGridView.Rows[e.RowIndex].Cells["Priority"] as DataGridViewComboBoxCell).Value.ToString() != "1") && ((this.StatusListDataGridView.Rows[e.RowIndex].Cells["Priority"] as DataGridViewComboBoxCell).Value.ToString() != "0"))
                         {
                             (this.StatusListDataGridView.Rows[e.RowIndex].Cells["Priority"] as DataGridViewComboBoxCell).Value = 0;
                             this.EnableSaveStatus();
                         }

                         if (((this.StatusListDataGridView.Rows[e.RowIndex].Cells["Active"] as DataGridViewComboBoxCell).Value.ToString() != "1") && ((this.StatusListDataGridView.Rows[e.RowIndex].Cells["Active"] as DataGridViewComboBoxCell).Value.ToString() != "0"))
                         {
                             (this.StatusListDataGridView.Rows[e.RowIndex].Cells["Active"] as DataGridViewComboBoxCell).Value = 1;
                             this.EnableSaveStatus();
                         }

                         // Bug ID 1121 Fixed 
                         // Addded && this.deleteKeydown
                         if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit && this.deleteKeydown)
                         {
                             this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                             this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                             this.DisableGridSorting();
                         }
                     }
                 }
                 else
                 {
                     this.pageMode = TerraScanCommon.PageModeTypes.View;
                 }
            }               
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #endregion

        #region OwnerDate Calendar Events

        /// <summary>
        /// Handles the KeyPress event of the OwnerDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void OwnerDateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
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
        /// Handles the CloseUp event of the OwnerDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OwnerDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                //// Assign the Date to the Current Cell
                if (!this.gridCurrentRowIndex.Equals(-1) && !this.gridCurrentColumnIndex.Equals(-1))
                {
                    if (this.StatusListDataGridView[this.gridCurrentColumnIndex, this.gridCurrentRowIndex].Value != null)
                    {
                        if (this.gridCurrentColumnIndex == this.StatusListDataGridView.Columns["BeginDate"].Index)
                        {
                            ////Begindate
                            if (this.StatusListDataGridView[this.StatusListDataGridView.Columns["EndDate"].Index, this.gridCurrentRowIndex].Value.ToString() != string.Empty)
                            {
                                if (Convert.ToDateTime(this.StatusListDataGridView[this.StatusListDataGridView.Columns["EndDate"].Index, this.gridCurrentRowIndex].Value.ToString()) < Convert.ToDateTime(this.OwnerDateTimePicker.Text))
                                {
                                    MessageBox.Show("Begin date should not be greater than end date", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    this.StatusListDataGridView[this.gridCurrentColumnIndex, this.gridCurrentRowIndex].Value = string.Empty;
                                }
                                else
                                {
                                    this.StatusListDataGridView[this.gridCurrentColumnIndex, this.gridCurrentRowIndex].Value = Convert.ToDateTime(this.OwnerDateTimePicker.Text);
                                }
                            }
                            else
                            {
                                this.StatusListDataGridView[this.gridCurrentColumnIndex, this.gridCurrentRowIndex].Value = Convert.ToDateTime(this.OwnerDateTimePicker.Text);
                            }
                        }
                        else if (this.gridCurrentColumnIndex == this.StatusListDataGridView.Columns["EndDate"].Index)
                        {
                            //// Enddate
                            if (this.StatusListDataGridView[this.StatusListDataGridView.Columns["BeginDate"].Index, this.gridCurrentRowIndex].Value.ToString() != string.Empty)
                            {
                                if (Convert.ToDateTime(this.StatusListDataGridView[this.StatusListDataGridView.Columns["BeginDate"].Index, this.gridCurrentRowIndex].Value.ToString()) > Convert.ToDateTime(this.OwnerDateTimePicker.Text))
                                {
                                    MessageBox.Show("End date should not be lesser than begin date", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    this.StatusListDataGridView[this.gridCurrentColumnIndex, this.gridCurrentRowIndex].Value = string.Empty;
                                }
                                else
                                {
                                    this.StatusListDataGridView[this.gridCurrentColumnIndex, this.gridCurrentRowIndex].Value = Convert.ToDateTime(this.OwnerDateTimePicker.Text);
                                }
                            }
                            else
                            {
                                this.StatusListDataGridView[this.gridCurrentColumnIndex, this.gridCurrentRowIndex].Value = Convert.ToDateTime(this.OwnerDateTimePicker.Text);
                            }
                        }

                        this.Control_TextChanged(sender, e);
                        this.StatusListDataGridView.Focus();
                        this.StatusListDataGridView[this.gridCurrentColumnIndex, this.gridCurrentRowIndex].Selected = true;
                    }
                }

                this.OwnerDateTimePicker.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }        
        #endregion

        #region PrivateMethods

        /// <summary>
        /// Get OwnerManagement Dataset[OwnerDetails , OwnerStatus List]
        /// </summary>
        private void LoadOwnerManagementDetails()
        {
            this.form96000OwnerManagementData = this.form96000Control.WorkItem.F96000_GetOwnerManagementDetails(this.keyId);
        }

        /// <summary>
        /// Loads the OwnerDetails
        /// </summary>
        private void LoadOwnerDetails()
        {
            try
            {
             if (this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows.Count > 0)
             {
                this.formOwnerID = Convert.ToInt32(this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.OwnerIDColumn].ToString());
                if (!string.IsNullOrEmpty(this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.LastNameColumn].ToString()))
                {
                    this.LastNameTextBox.Text = this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.LastNameColumn].ToString();
                }
                else
                {
                    this.LastNameTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.FirstNameColumn].ToString()))
                {
                    this.FirstNameTextBox.Text = this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.FirstNameColumn].ToString();
                }
                else
                {
                    this.FirstNameTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.OwnerCodeColumn].ToString()))
                {
                    this.OwnerCodeTextBox.Text = this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.OwnerCodeColumn].ToString();
                }
                else
                {
                    this.OwnerCodeTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.Address1Column].ToString()))
                {
                    this.Address1TextBox.Text = this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.Address1Column].ToString();
                }
                else
                {
                    this.Address1TextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.Address2Column].ToString()))
                {
                    this.Address2TextBox.Text = this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.Address2Column].ToString();
                }
                else
                {
                    this.Address2TextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.CityColumn].ToString()))
                {
                    this.CityTextBox.Text = this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.CityColumn].ToString();
                }
                else
                {
                    this.CityTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.StateColumn].ToString()))
                {
                    this.StateTextBox.Text = this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.StateColumn].ToString();
                }
                else
                {
                    this.StateTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.ZipColumn].ToString()))
                {
                    this.ZipCodeTextBox.Text = this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.ZipColumn].ToString();
                }
                else
                {
                    this.ZipCodeTextBox.Text = string.Empty;
                }

                this.LoadActiveCombo();

                if (!string.IsNullOrEmpty(this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.PhoneColumn].ToString()))
                {
                    this.PhoneNumberTextBox.Text = this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.PhoneColumn].ToString();
                }
                else
                {
                    this.PhoneNumberTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.EmailColumn].ToString()))
                {
                    this.EmailAddressTextBox.Text = this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.EmailColumn].ToString();
                }
                else
                {
                    this.EmailAddressTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.CountryIDColumn].ToString()))
                {
                    this.CountryComboBox.SelectedValue = this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.CountryIDColumn];
                }
                else
                {
                    this.CountryComboBox.SelectedIndex = -1;
                    this.CountryComboBox.Text = string.Empty;
                    this.CountryComboBox.SelectedText = string.Empty;
                }
                ////code added to fix the bug#4476 by khaja.  
                this.LastNameTextBox.Focus();
                this.LockControls(false);
                this.CopyButton.Enabled = true;
            }
            else
            {
                this.LockControls(true);
            }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Loads the ActiveCombo box
        /// </summary>
        private void LoadActiveCombo()
        {          
              if (string.Compare(this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.IsActiveColumn.ColumnName].ToString().ToUpperInvariant(), SharedFunctions.GetResourceString("FALSEValue")) == 0 || string.Compare(this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.IsActiveColumn.ColumnName].ToString().ToUpperInvariant(), SharedFunctions.GetResourceString("TRUEValue")) == 0)
                {
                    if (string.Compare(this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows[0][this.form96000OwnerManagementData.F96000GetOwnerDetails.IsActiveColumn.ColumnName].ToString().ToUpperInvariant(), SharedFunctions.GetResourceString("FALSEValue")) == 0)
                    {
                        this.ActiveCombo.SelectedValue = 0;
                    }
                    else
                    {
                        this.ActiveCombo.SelectedValue = 1;
                    }
                }
        }

        /// <summary>
        /// This Method used to bind datasource and displaymember
        /// CustomizeCombobox
        /// </summary>
        private void CustomizeCombobox()
        {
            ////which loads yes, no value to the DataSet
            this.activeComboData.LoadYesNoValue();
            this.activeComboGridData.LoadYesNoValue();
            this.priorityComboGridData.LoadYesNoValue();
            this.ActiveCombo.DataSource = this.activeComboData.ComboBoxDataTable;
            this.ActiveCombo.ValueMember = this.activeComboData.ComboBoxDataTable.KeyIdColumn.ColumnName;
            this.ActiveCombo.DisplayMember = this.activeComboData.ComboBoxDataTable.KeyNameColumn.ColumnName;
            this.ActiveCombo.SelectedValue = 1;     
        }

        private void LoadCountryComboBox()
        {
            this.CountryDetailsData = this.form96000Control.WorkItem.F96000_CountryComboDetails();
            if (this.CountryDetailsData.f96000_pclst_Country.Rows.Count > 0)
            {
                this.CountryComboBox.DataSource = this.CountryDetailsData.f96000_pclst_Country;
                this.CountryComboBox.ValueMember = this.CountryDetailsData.f96000_pclst_Country.CountryIDColumn.ColumnName;
                this.CountryComboBox.DisplayMember = this.CountryDetailsData.f96000_pclst_Country.CountryNameColumn.ColumnName;

                this.CountryComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                this.CountryComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
               
            }
        }

        private void LoadDefalutCountry()
        {
            if (this.CountryDetailsData.LoadDefaultCountry.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.CountryDetailsData.LoadDefaultCountry.Rows[0]["CountryID"].ToString()))
                {
                    this.CountryComboBox.SelectedValue = this.CountryDetailsData.LoadDefaultCountry.Rows[0]["CountryID"];
                }
            }
        }


        /// <summary>
        /// Clears the OwnerManagementDetails
        /// </summary>
        private void ClearOwnerManagementDetails()
        {            
            this.LastNameTextBox.Text = string.Empty;
            this.FirstNameTextBox.Text = string.Empty;
            this.OwnerCodeTextBox.Text = string.Empty;
            this.Address1TextBox.Text = string.Empty;
            this.Address2TextBox.Text = string.Empty;
            this.CityTextBox.Text = string.Empty;
            this.StateTextBox.Text = string.Empty;
            this.ZipCodeTextBox.Text = string.Empty;            
            this.PhoneNumberTextBox.Text = string.Empty;
            this.EmailAddressTextBox.Text = string.Empty;

            // Reset the active combo to set default value.
            this.ActiveCombo.SelectedValue = 1;
           // this.CountryComboBox.Text = string.Empty;
           
        }

        /// <summary>
        /// Clears the StatusListGrid
        /// </summary>
        private void ClearStatusListGrid()
        {
            this.form96000OwnerManagementData.F96000GetStatusList.Clear();
            this.flagFormLoad = true;
            this.StatusListDataGridView.DataSource = this.form96000OwnerManagementData.F96000GetStatusList.DefaultView;
            this.flagFormLoad = false;
            this.StatusListDataGridView.Refresh();
            if (this.form96000OwnerManagementData.F96000GetStatusList.Rows.Count > this.StatusListDataGridView.NumRowsVisible)
            {
                this.OwnerManagementVscrollBar.Enabled = true;
                this.OwnerManagementVscrollBar.Visible = false;
            }
            else
            {
                this.OwnerManagementVscrollBar.Enabled = false;
                this.OwnerManagementVscrollBar.Visible = true;
            }
        }

        /// <summary>
        /// Customizes the StatusListGrid
        /// </summary>
        private void CustomizeOwnerStatusGrid()
        {
            this.form96000ListOwnerStatusTypeData = this.form96000Control.WorkItem.F96000_ListOwnerStatusType();
            this.StatusListDataGridView.AllowUserToResizeColumns = false;
            this.StatusListDataGridView.AllowUserToResizeRows = false;
            this.StatusListDataGridView.AutoGenerateColumns = false;
            this.StatusListDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.Status.DataPropertyName = this.form96000OwnerManagementData.F96000GetStatusList.OwnerStatusTypeIDColumn.ColumnName;
            this.BeginDate.DataPropertyName = this.form96000OwnerManagementData.F96000GetStatusList.BeginDateColumn.ColumnName;
            this.EndDate.DataPropertyName = this.form96000OwnerManagementData.F96000GetStatusList.EndDateColumn.ColumnName;
            this.Note.DataPropertyName = this.form96000OwnerManagementData.F96000GetStatusList.NoteColumn.ColumnName;
            this.OwnerID.DataPropertyName = this.form96000OwnerManagementData.F96000GetStatusList.OwnerIDColumn.ColumnName;
            this.Priority.DataPropertyName = this.form96000OwnerManagementData.F96000GetStatusList.IsPriorityColumn.ColumnName;
            this.Active.DataPropertyName = this.form96000OwnerManagementData.F96000GetStatusList.IsActiveColumn.ColumnName;

            this.Status.DisplayIndex = 0;
            this.BeginDate.DisplayIndex = 1;
            this.EndDate.DisplayIndex = 2;
            this.Active.DisplayIndex = 3;
            this.Priority.DisplayIndex = 4;
            this.Note.DisplayIndex = 5;

            (this.StatusListDataGridView.Columns["Status"] as DataGridViewComboBoxColumn).DataSource = this.form96000ListOwnerStatusTypeData;
            (this.StatusListDataGridView.Columns["Status"] as DataGridViewComboBoxColumn).DisplayMember = "OwnerStatusType";
            (this.StatusListDataGridView.Columns["Status"] as DataGridViewComboBoxColumn).ValueMember = "OwnerStatusTypeID";
 
            (this.StatusListDataGridView.Columns["Active"] as DataGridViewComboBoxColumn).DataSource = this.activeComboGridData.ComboBoxDataTable;
            (this.StatusListDataGridView.Columns["Active"] as DataGridViewComboBoxColumn).DisplayMember = this.activeComboGridData.ComboBoxDataTable.KeyNameColumn.ColumnName;
            (this.StatusListDataGridView.Columns["Active"] as DataGridViewComboBoxColumn).ValueMember = this.activeComboGridData.ComboBoxDataTable.KeyIdColumn.ColumnName;            

            (this.StatusListDataGridView.Columns["Priority"] as DataGridViewComboBoxColumn).DataSource = this.priorityComboGridData.ComboBoxDataTable;
            (this.StatusListDataGridView.Columns["Priority"] as DataGridViewComboBoxColumn).DisplayMember = this.priorityComboGridData.ComboBoxDataTable.KeyNameColumn.ColumnName;
            (this.StatusListDataGridView.Columns["Priority"] as DataGridViewComboBoxColumn).ValueMember = this.priorityComboGridData.ComboBoxDataTable.KeyIdColumn.ColumnName;            
        }

        /// <summary>
        /// Populates the StatusListGrid
        /// </summary>
        private void PopulateDataGridView()
        {
            if (this.form96000OwnerManagementData.F96000GetStatusList.Rows.Count >= 0)
            {
                this.flagFormLoad = true;
                this.StatusListDataGridView.DataSource = this.form96000OwnerManagementData.F96000GetStatusList.DefaultView;
                this.flagFormLoad = false;
            }
            else
            {
                this.ClearOwnerManagementDetails();
                this.ClearStatusListGrid();
            }

            if (this.form96000OwnerManagementData.F96000GetStatusList.Rows.Count > this.StatusListDataGridView.NumRowsVisible)
            {
                this.OwnerManagementVscrollBar.Enabled = true;
                this.OwnerManagementVscrollBar.Visible = false;
            }
            else
            {
                this.OwnerManagementVscrollBar.Enabled = false;
                this.OwnerManagementVscrollBar.Visible = true;
            }            

            ////code to add a new row in the grid,during form load(if in the case only the immediate next row is editable.
            if (this.StatusListDataGridView.OriginalRowCount >= this.StatusListDataGridView.NumRowsVisible)
            {                
                if (!Convert.ToBoolean(this.form96000OwnerManagementData.F96000GetStatusList.Rows[this.form96000OwnerManagementData.F96000GetStatusList.Rows.Count - 1][this.StatusListDataGridView.EmptyRecordColumnName]))
                {
                    this.form96000OwnerManagementData.F96000GetStatusList.Rows.Add(this.form96000OwnerManagementData.F96000GetStatusList.NewRow());
                    this.form96000OwnerManagementData.F96000GetStatusList.Rows[this.form96000OwnerManagementData.F96000GetStatusList.Rows.Count - 1][this.StatusListDataGridView.EmptyRecordColumnName] = true;
                    this.OwnerManagementVscrollBar.Visible = false;
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
                this.DisableGridSorting();
            }
        }

        /// <summary>
        /// To the enable edit buttons in master form.
        /// </summary>
        private void ToEnableEditButtonInMasterForm()
        {
            if (!this.flagLoadOnProcess)
            {
                this.EditEnabled();
            }
        }

        /// <summary>
        /// To Validate OwnerDetails
        /// </summary>
        /// <returns>validation Result</returns>
        private bool ValidateOwnerDetails()
        {
            bool validationResult = true;
            if (validationResult)
            {
                if (string.IsNullOrEmpty(this.LastNameTextBox.Text.Trim()))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("You cannot save this Owner record because it is missing required fields"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.LastNameTextBox.Focus();
                }
                else
                {
                    if (string.IsNullOrEmpty(this.FirstNameTextBox.Text.Trim()))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("You cannot save this Owner record because it is missing required fields"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.FirstNameTextBox.Focus();
                    }
                }
            }

            return validationResult;
        }

        /// <summary>
        /// To Save OwnerManagementDetails
        /// </summary>
        /// <returns>return Value</returns>
        private int SaveOwnerManagementDetails()
        {                     
            int returnValue = -1;                
            string tempOwnerDetails = string.Empty;
            string statusListDetails = string.Empty;

            F96000OwnerManagementData ownerManagementDetailsdata = new F96000OwnerManagementData();
            F96000OwnerManagementData.F96000GetOwnerDetailsRow dr = ownerManagementDetailsdata.F96000GetOwnerDetails.NewF96000GetOwnerDetailsRow();
            dr.OwnerID = this.formOwnerID;
            dr.LastName = this.LastNameTextBox.Text.Trim();
            dr.FirstName = this.FirstNameTextBox.Text.Trim();
            dr.OwnerCode = this.OwnerCodeTextBox.Text.Trim();
            dr.Address1 = this.Address1TextBox.Text.Trim();
            dr.Address2 = this.Address2TextBox.Text.Trim();
            dr.City = this.CityTextBox.Text.Trim();
            dr.State = this.StateTextBox.Text.Trim();
            dr.Zip = this.ZipCodeTextBox.Text.Trim();
            dr.IsActive = Convert.ToBoolean(this.ActiveCombo.SelectedValue);
            dr.Phone = this.PhoneNumberTextBox.Text.Trim();
            dr.Email = this.EmailAddressTextBox.Text.Trim();
            if (this.CountryComboBox.SelectedValue != null)
            {
                dr.CountryID = Convert.ToInt32(this.CountryComboBox.SelectedValue);
            }
           
            ownerManagementDetailsdata.F96000GetOwnerDetails.Rows.Add(dr);
            DataSet tempOwnerDetailsDataset = new DataSet("Root");
            tempOwnerDetailsDataset.Tables.Add(ownerManagementDetailsdata.F96000GetOwnerDetails.Copy());
            tempOwnerDetailsDataset.Tables[0].TableName = "Table";
            tempOwnerDetails = TerraScanCommon.GetXmlString(ownerManagementDetailsdata.F96000GetOwnerDetails);

            this.StatusListDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            this.form96000OwnerManagementData.F96000GetStatusList.AcceptChanges();
            statusListDetails = TerraScanCommon.GetXmlString(this.form96000OwnerManagementData.F96000GetStatusList);

            returnValue = this.Form96000Controller.WorkItem.F96000_InsertOwnerManagementDetails(this.formOwnerID, tempOwnerDetails, statusListDetails, TerraScanCommon.UserId);
            if (returnValue > 0)
            {
                this.PopulateDataGridView();
            }

            this.EnableGridSorting();
            this.CopyButton.Enabled = true;
            return returnValue;          
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>Slice Validation Fields</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            string messageMissingReqField = "You cannot save this Owner record because it is missing required fields.";
            sliceValidationFields.FormNo = formNo;
            sliceValidationFields.ErrorMessage = string.Empty;  
            sliceValidationFields.RequiredFieldMissing = false;

            if (string.IsNullOrEmpty(this.LastNameTextBox.Text.Trim())) 
            {                
                this.LastNameTextBox.Focus();
                sliceValidationFields.ErrorMessage = messageMissingReqField; 
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }

            this.StatusListDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            this.form96000OwnerManagementData.F96000GetStatusList.AcceptChanges();

            ////to validate the missing fields in GRID.
            if (this.form96000OwnerManagementData.F96000GetStatusList.Rows.Count > 0)
            {
                DataRow[] selecteList = this.form96000OwnerManagementData.F96000GetStatusList.Select(" (((OwnerStatusTypeID IS NULL) OR (BeginDate IS NULL OR BeginDate = '')) AND (IsPriority IS NOT NULL AND IsActive IS NOT NULL)) ");               
                if (selecteList.Length > 0)
                {
                    sliceValidationFields.ErrorMessage = messageMissingReqField;
                    sliceValidationFields.RequiredFieldMissing = true;
                    return sliceValidationFields;
                }
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Shows the owner date time picker.
        /// </summary>
        private void ShowOwnerDateTimePicker()
        {
            //// Set the date according to the Current Column Index 
            if (!this.gridCurrentRowIndex.Equals(-1) && !this.gridCurrentColumnIndex.Equals(-1))
            {
                if (!string.IsNullOrEmpty(this.StatusListDataGridView[this.gridCurrentColumnIndex, this.gridCurrentRowIndex].Value.ToString()))
                {
                    this.OwnerDateTimePicker.Value = Convert.ToDateTime(this.StatusListDataGridView[this.gridCurrentColumnIndex, this.gridCurrentRowIndex].Value);
                }
                else
                {
                    this.OwnerDateTimePicker.Value = DateTime.Now;
                }
            }

            this.OwnerDateTimePicker.Visible = true;
            this.OwnerDateTimePicker.Focus();
            SendKeys.Send("{F4}");
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="lockValue">if set to <c>true</c> [lock value].</param>
        private void LockControls(bool lockValue)
        {
            this.LastNameTextBox.LockKeyPress = lockValue;
            this.FirstNameTextBox.LockKeyPress = lockValue;
            this.OwnerCodeTextBox.LockKeyPress = lockValue;
            this.Address1TextBox.LockKeyPress = lockValue;
            this.Address2TextBox.LockKeyPress = lockValue;
            this.CityTextBox.LockKeyPress = lockValue;
            this.StateTextBox.LockKeyPress = lockValue;
            this.ZipCodeTextBox.LockKeyPress = lockValue;
            this.ActiveCombo.Enabled = !lockValue;
            //To Enable/Disable Combo purushotham
            this.CountryComboBox.Enabled = !lockValue;
            this.CountryPanel.Enabled = !lockValue;
            this.PhoneNumberTextBox.LockKeyPress = lockValue;
            this.EmailAddressTextBox.LockKeyPress = lockValue;            
            this.LastNamepanel.Enabled = !lockValue;
            this.FirstNamepanel.Enabled = !lockValue;
            this.OwnerCodePanel.Enabled = !lockValue;
            this.Address1panel.Enabled = !lockValue;
            this.Address2panel.Enabled = !lockValue;
            this.Citypanel.Enabled = !lockValue;
            this.Statepanel.Enabled = !lockValue;
            this.ZipCodepanel.Enabled = !lockValue;
            this.Activepanel.Enabled = !lockValue;
            this.PhoneNumberpanel.Enabled = !lockValue;
            this.EmailAddresspanel.Enabled = !lockValue;            
            this.StatusListDataGridView.Enabled = !lockValue;
            this.OwnerManagementGridViewpanel.Enabled = !lockValue;
        }

        /// <summary>
        /// Enables the grid sorting.
        /// </summary>
        private void EnableGridSorting()
        {
            DataGridViewColumnCollection enableGridSortColumn = this.StatusListDataGridView.Columns;
            enableGridSortColumn["Status"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enableGridSortColumn["BeginDate"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enableGridSortColumn["EndDate"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enableGridSortColumn["Active"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enableGridSortColumn["Priority"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enableGridSortColumn["Note"].SortMode = DataGridViewColumnSortMode.Programmatic;
        }

        /// <summary>
        /// Disables the grid sorting.
        /// </summary>
        private void DisableGridSorting()
        {
            DataGridViewColumnCollection disableGridSortColumn = this.StatusListDataGridView.Columns;
            disableGridSortColumn["Status"].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn["BeginDate"].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn["EndDate"].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn["Active"].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn["Priority"].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn["Note"].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        /// <summary>
        /// Handles the CellMouseClick event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void StatusListDataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.ColumnIndex == 4) || (e.ColumnIndex == 5))
                {
                    this.StatusListDataGridView.ShowCellToolTips = false;
                }
                else
                {
                    this.StatusListDataGridView.ShowCellToolTips = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the StatusListDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void StatusListDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                ////delete key
                if (e.KeyCode == Keys.Delete && this.StatusListDataGridView.CurrentCell != null && this.StatusListDataGridView.Rows[this.StatusListDataGridView.CurrentCell.RowIndex].Selected)
                {
                    this.deleteKeydown = true;
                    int rowIndex;
                    rowIndex = this.StatusListDataGridView.CurrentCell.RowIndex;
                    int.TryParse(this.form96000OwnerManagementData.F96000GetStatusList.Rows[rowIndex][this.form96000OwnerManagementData.F96000GetStatusList.OwnerStatusIDColumn].ToString(), out this.ownerStatusId);
                    if (this.ownerStatusId != 0)
                    {
                        this.Form96000Controller.WorkItem.F96000_DeleteData(this.ownerStatusId);
                        this.form96000OwnerManagementData.F96000GetStatusList.Rows.RemoveAt(this.StatusListDataGridView.CurrentCell.RowIndex);
                        this.deleteKeydown = true;
                        this.form96000OwnerManagementData.F96000GetStatusList.AcceptChanges();
                        if (this.form96000OwnerManagementData.F96000GetOwnerDetails.Rows.Count > 0)
                        {
                            this.StatusListDataGridView.DataSource = this.form96000OwnerManagementData.F96000GetStatusList.DefaultView;
                        }
                        else
                        {
                            this.form96000OwnerManagementData.F96000GetStatusList.Clear();
                            this.StatusListDataGridView.DataSource = this.form96000OwnerManagementData.F96000GetStatusList.DefaultView;
                        }

                        if (this.form96000OwnerManagementData.F96000GetStatusList.Rows.Count > this.StatusListDataGridView.NumRowsVisible)
                        {
                            this.OwnerManagementVscrollBar.Enabled = true;
                            this.OwnerManagementVscrollBar.Visible = false;
                        }
                        else
                        {
                            this.OwnerManagementVscrollBar.Enabled = false;
                            this.OwnerManagementVscrollBar.Visible = true;
                        }

                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        StatusListDataGridView.Refresh();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }

                    if (this.ownerStatusId == 0)
                    {
                        int rowindex = this.StatusListDataGridView.CurrentCell.RowIndex;
                        this.StatusListDataGridView.Rows[rowindex].Cells["BeginDate"].Value = string.Empty;
                        this.StatusListDataGridView.Rows[rowindex].Cells["status"].Value = string.Empty;
                        this.StatusListDataGridView.Rows[rowindex].Cells["EndDate"].Value = string.Empty;
                        this.StatusListDataGridView.Rows[rowindex].Cells["Active"].Value = string.Empty;
                        this.StatusListDataGridView.Rows[rowindex].Cells["Priority"].Value = string.Empty;
                        this.StatusListDataGridView.Rows[rowindex].Cells["Note"].Value = string.Empty;
                    }
                }                 
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                //// khaja added code to fix Bug#4549
                this.deleteKeydown = false;
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the StatusListDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void StatusListDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {            
        }

        /// <summary>
        /// Handles the CellLeave event of the StatusListDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void StatusListDataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            this.StatusListDataGridView.UpdateCellValue(0, e.RowIndex);
            string ownerStatus = (this.StatusListDataGridView.Rows[e.RowIndex].Cells["status"] as DataGridViewComboBoxCell).Value.ToString();
            string a = this.StatusListDataGridView.Rows[e.RowIndex].Cells["BeginDate"].Value.ToString();
        }
              
        #endregion PrivateMethods        

        private void CopyButton_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable CopyDataTable = new DataTable();
                CopyDataTable = this.form96000OwnerManagementData.F96000GetOwnerDetails.Copy();
                Form districtF15122 = new Form();
                object[] optionalParameter = new object[] { CopyDataTable, this.ParentFormId };//,this.scheduleId
                districtF15122 = TerraScanCommon.GetForm(9105, optionalParameter, this.form96000Control.WorkItem);
                DialogResult districtDialog;
                if (districtF15122 != null)
                {
                    districtDialog = districtF15122.ShowDialog();

                    if (districtDialog == DialogResult.OK)
                    {
                        string Id = TerraScanCommon.GetValue(districtF15122, "CommandResult");
                        int.TryParse(Id, out this.keyId);
                        if (MessageBox.Show("Do you want to see this copied record?", "TerraScan – Copy Name/Address", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            FormInfo formInfo;
                            formInfo = TerraScanCommon.GetFormInfo(91000);
                            formInfo.optionalParameters = new object[1];
                            formInfo.optionalParameters[0] = this.keyId;
                            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo)); 
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
    }
}

