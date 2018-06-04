//--------------------------------------------------------------------------------------------
// <copyright file="F82001.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F82001 FS Building Permit
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 12/12/2007       Kuppusamy.B        Created
//***********************************************************************************************/

namespace D82001
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Web.Services.Protocols;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using Infrastructure.Interface;

    /// <summary>
    /// F82001 Class File.
    /// </summary>
    [SmartPart]
    public partial class F82001 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// Set Date Format
        /// </summary>
        private readonly string dateFormat = SharedFunctions.GetResourceString("DefaultAfdvtDateforamt");

        /// <summary>
        /// F82001Controller
        /// </summary>
        private F82001Controller form82001Control;

        /// <summary>
        /// Instance for the BuildingPermitDataset
        /// </summary>
        private F82001BuildingPermitData buildingPermitData = new F82001BuildingPermitData();

        /// <summary>
        /// Instance for the ContractorManagementDataset
        /// </summary>
        private F82002ContractorManagementData contractorManagementData = new F82002ContractorManagementData();

        /// <summary>
        /// Instance for the SupportFormDataset
        /// </summary>
        private SupportFormData userListData = new SupportFormData();

        /// <summary>
        /// Instance for GDocEventHeaderData-To check the Event-Header
        /// </summary>
        private GDocEventHeaderData gdocEventHeaderData = new GDocEventHeaderData();

        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyId;

        /// <summary>
        /// Unique eventID from the Form Master
        /// </summary>
        private int eventID;

        /// <summary>
        /// Unique permitID from the Form Master
        /// </summary>
        private int permitID;

        /// <summary>
        /// int for the returnContractorID
        /// </summary>
        private int returnContractorID;

        /// <summary>
        /// OperationSmartPart Variable
        /// </summary>
        private OperationSmartPart operationSmartPart;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// isShift
        /// </summary>
        private bool issShift;

        #region Form Slice Variables

        /// <summary>
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// Local variable for formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        #endregion Form Slice Variables

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F82001"/> class.
        /// </summary>
        public F82001()
        {
            this.InitializeComponent();
            F82001BuildingPermitData buildingPermitData = new F82001BuildingPermitData();
            F82002ContractorManagementData contractorManagementData = new F82002ContractorManagementData();
            SupportFormData userListData = new SupportFormData();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F82001"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F82001(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.eventID = this.keyId;
            this.sectionIndicatorText = tabText;
            this.formMasterPermissionEdit = permissionEdit;
            F82001BuildingPermitData buildingPermitData = new F82001BuildingPermitData();
            F82002ContractorManagementData contractorManagementData = new F82002ContractorManagementData();
            SupportFormData userListData = new SupportFormData();
            this.BuildingPermitPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.BuildingPermitPictureBox.Height, this.BuildingPermitPictureBox.Width, this.sectionIndicatorText, red, green, blue);
        }

        #endregion Constructor

        #region Event Publication

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
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the form82001 control.
        /// </summary>
        /// <value>The form82001 control.</value>
        [CreateNew]
        public F82001Controller Form82001Control
        {
            get { return this.form82001Control as F82001Controller; }
            set { this.form82001Control = value; }
        }

        /// <summary>
        /// Gets or sets the ContractorID.
        /// </summary>
        /// <value>The ContractorID.</value>
        public int ContractorID
        {
            get { return this.returnContractorID; }
            set { this.returnContractorID = value; }
        }

        #endregion Property

        #region Event Subscription

        /// <summary>
        /// Event Subscription SetSlicePermission
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The Event.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                if (this.Visible)
                {
                    if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                    {
                        this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                        this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                        this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                        this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                        if (this.keyId != eventArgs.Data.KeyId)
                        {
                            ////To check the invalid key id in set slice event subscription db call is set to F82001_ListLandDetails Method to check invalid key id
                            this.buildingPermitData = this.form82001Control.WorkItem.F82001_GetBuildingPermitDetails(eventArgs.Data.KeyId);
                            this.gdocEventHeaderData = this.form82001Control.WorkItem.GetGDocEventHeader(this.keyId);
                        }

                        /*To check the invalid keyid*/
                        if (this.gdocEventHeaderData.GetGDocEventHeader.Rows.Count > 0)
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
                        ////if (this.buildingPermitData.F82001GetBuildingPermitDetails.Rows.Count > 0)
                        /*To enable or disable the controls based on the Eventheader*/
                        if (this.gdocEventHeaderData.GetGDocEventHeader.Rows.Count > 0)
                        {
                            this.ControlLock(false);
                        }
                        else
                        {
                            this.ControlLock(true);
                        }
                    }
                    else
                    {
                        this.ControlLock(true);
                    }
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
                if (this != null && this.IsDisposed != true)
                {
                    if (this.Visible)
                    {
                        if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                        {
                            this.keyId = eventArgs.Data.SelectedKeyId;
                            this.ListBuildingPermitDetails();
                            this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
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
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            ////here form master save is not used but we are using this Event subscription to update the value slice header form slice

            if (this != null && this.IsDisposed != true)
            {
                if (this.Visible)
                {
                    if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                    {
                        if (this.slicePermissionField.editPermission)
                        {
                            this.ValidateSliceForm(eventArgs);
                        }
                    }
                    else
                    {
                        SliceValidationFields sliceValidationFields = new SliceValidationFields();
                        sliceValidationFields.FormNo = eventArgs.Data;
                        this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
                    }
                }
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
            if (this != null && this.IsDisposed != true)
            {
                if (this.Visible)
                {
                    ////here save is only used to update the FS
                    if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                    {
                        if (this.slicePermissionField.editPermission)
                        {
                            this.SaveBuildingPermitDetails();
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                        }
                    }
                    else
                    {
                        this.ControlLock(true);
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
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
            this.Cursor = Cursors.WaitCursor;
            if (this != null && this.IsDisposed != true)
            {
                if (this.Visible)
                {
                    if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                    {
                        this.ControlLock(false);
                    }
                    else
                    {
                        this.ControlLock(true);
                    }

                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.flagLoadOnProcess = true;
                    this.CustomizeAllComboBoxes();
                    this.ListBuildingPermitDetails();
                    this.flagLoadOnProcess = false;
                    this.OccupancyInspectionCalander.Visible = false;
                    this.Cursor = Cursors.Default;
                }
            }
        }

        #endregion Event Subscription

        #region Protected methods

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

        #endregion Protected methods

        #region Event Handlers

        /// <summary>
        /// Handles the Load event of the F82001 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F82001_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                /*Load the Smartpart Workspace*/
                this.LoadWorkSpaces();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.flagLoadOnProcess = true;
                this.CustomizeAllComboBoxes();
                this.ListBuildingPermitDetails();
                this.flagLoadOnProcess = false;
                this.OccupancyInspectionCalander.Visible = false;
                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
            }
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the AllComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AllComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the Click event of the BuildingPermitPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BuildingPermitPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the MouseHover event of the BuildingPermitPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BuildingPermitPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.BuildingPermitFSToolTip.SetToolTip(this.BuildingPermitPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the OccupancyInspectionDateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OccupancyInspectionDateButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowOccupancyInspectionCalender();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Alls the C button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AllCButtonClick(object sender, EventArgs e)
        {
            try
            {
                string buttontext = (sender as Button).Name;
                /*Compare the name of the button clicked and to populate the appropriate contractorID*/
                if (buttontext == this.GeneralContractorButton.Name)
                {
                    if (this.GeneralContractorCombo.SelectedValue != null)
                    {
                        int.TryParse(this.GeneralContractorCombo.SelectedValue.ToString(), out this.returnContractorID);
                    }
                }
                else if (buttontext == this.MechanicalContractorbutton.Name)
                {
                    if (this.MechanicalContractorScanCombo.SelectedValue != null)
                    {
                        int.TryParse(this.MechanicalContractorScanCombo.SelectedValue.ToString(), out this.returnContractorID);
                    }
                }
                else if (buttontext == this.PlumbingContractorbutton.Name)
                {
                    if (this.PlumbingContractorCombo.SelectedValue != null)
                    {
                        int.TryParse(this.PlumbingContractorCombo.SelectedValue.ToString(), out this.returnContractorID);
                    }
                }
                else if (buttontext == this.ElectricalContractorbutton.Name)
                {
                    if (this.ElectricalContractorCombo.SelectedValue != null)
                    {
                        int.TryParse(this.ElectricalContractorCombo.SelectedValue.ToString(), out this.returnContractorID);
                    }
                }
                else if (buttontext == this.OtherContractorbutton.Name)
                {
                    if (this.OtherContractorCombo.SelectedValue != null)
                    {
                        int.TryParse(this.OtherContractorCombo.SelectedValue.ToString(), out this.returnContractorID);
                    }
                }

                /*Shows the form corresponding to the parameters mentioned*/
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(82102);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.returnContractorID;

                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));

                ////to sent the this returned keyid(this.valveId) to 84722
                ////SliceReloadActiveRecord currentSliceInfo;
                ////currentSliceInfo.MasterFormNo = this.masterFormNo;
                ////currentSliceInfo.SelectedKeyId = this.returnContractorID;
                ////this.FormSlice_OnSave_GetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentSliceInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Alltexts the box text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AlltextBoxTextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the DateSelected event of the OccupancyInspectionCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void OccupancyInspectionCalander_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.SetSelectedDate(e.Start.ToString(this.dateFormat));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the OccupancyInspectionCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void OccupancyInspectionCalander_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.SetSelectedDate(this.OccupancyInspectionCalander.SelectionStart.ToString(this.dateFormat));
                    this.OccupancyInspectionCalander.Visible = false;
                }

                this.issShift = e.Shift;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the OccupancyInspectionCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OccupancyInspectionCalander_Leave(object sender, EventArgs e)
        {
            try
            {
                this.OccupancyInspectionCalander.Visible = false;
                if (this.issShift)
                {
                    this.OccupancyInspectionTextBox.Focus();
                }
                else
                {
                    this.ApprovedByCombo.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the LegalDescriptionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void LegalDescriptionTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.slicePermissionField.editPermission)
            {
                switch (e.KeyChar)
                {
                    /*To Prevent the junk value creation on Various Key Combinations*/
                    case (char)13:
                        {
                            e.Handled = true;
                            break;
                        }

                    /* CTRL + ENTER*/
                    case (char)10:
                        {
                            e.Handled = true;
                            break;
                        }

                    /* CTRL + I*/
                    case (char)9:
                        {
                            e.Handled = true;
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the DescriptionOfWorkTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void DescriptionOfWorkTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.slicePermissionField.editPermission)
            {
                switch (e.KeyChar)
                {
                    /*To Prevent the junk value creation on Various Key Combinations*/
                    case (char)13:
                        {
                            e.Handled = true;
                            break;
                        }

                    /* CTRL + ENTER*/
                    case (char)10:
                        {
                            e.Handled = true;
                            break;
                        }

                    /* CTRL + I*/
                    case (char)9:
                        {
                            e.Handled = true;
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Handles the Leave event of the OccupancyInspectionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OccupancyInspectionTextBox_Leave(object sender, EventArgs e)
        {
            this.OccupancyInspectionTextBox.BackColor = Color.White;
            this.OccupancyInspectionpanel.BackColor = Color.White;
        }

        /// <summary>
        /// Handles the Leave event of the ApprovedByCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ApprovedByCombo_Leave(object sender, EventArgs e)
        {
            this.ApprovedByCombo.BackColor = Color.White;
            this.ApprovedBypanel.BackColor = Color.White;
        }

        #endregion Event Handlers

        #region PrivateMethods

        /// <summary>
        /// Lists the building permit details.
        /// </summary>
        private void ListBuildingPermitDetails()
        {
            /*Load the temp dataet with the Source*/
            this.buildingPermitData = this.form82001Control.WorkItem.F82001_GetBuildingPermitDetails(this.eventID);
            this.gdocEventHeaderData = this.form82001Control.WorkItem.GetGDocEventHeader(this.keyId);

            /*Load the Comboboxes*/
            this.LoadAllComboBoxes();

            /*Assign the Values from the sourec to the Controls*/
            if (this.buildingPermitData.F82001GetBuildingPermitDetails.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.LegalDescriptionColumn].ToString()))
                {
                    this.LegalDescriptionTextBox.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.LegalDescriptionColumn].ToString();
                }
                else
                {
                    this.LegalDescriptionTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OwnerNameColumn].ToString()))
                {
                    this.OwnerNameTextBox.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OwnerNameColumn].ToString();
                }
                else
                {
                    this.OwnerNameTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OwnerPhoneColumn].ToString()))
                {
                    this.OwnerPhoneNumberTextBox.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OwnerPhoneColumn].ToString();
                }
                else
                {
                    this.OwnerPhoneNumberTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OwnerAddressColumn].ToString()))
                {
                    this.OwnerAddressTextBox.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OwnerAddressColumn].ToString();
                }
                else
                {
                    this.OwnerAddressTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OwnerCityColumn].ToString()))
                {
                    this.OwnerCityTextBox.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OwnerCityColumn].ToString();
                }
                else
                {
                    this.OwnerCityTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OwnerStateColumn].ToString()))
                {
                    this.StateTextBox.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OwnerStateColumn].ToString();
                }
                else
                {
                    this.StateTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OwnerZipColumn].ToString()))
                {
                    this.ZipCodeTextBox.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OwnerZipColumn].ToString();
                }
                else
                {
                    this.ZipCodeTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.GeneralContractorColumn].ToString()))
                {
                    this.GeneralContractorCombo.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.GeneralContractorColumn].ToString();
                }
                else
                {
                    this.GeneralContractorCombo.SelectedValue = -1;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.MechanicalContractorColumn].ToString()))
                {
                    this.MechanicalContractorScanCombo.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.MechanicalContractorColumn].ToString();
                }
                else
                {
                    this.MechanicalContractorScanCombo.SelectedValue = -1;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.PlumbingContractorColumn].ToString()))
                {
                    this.PlumbingContractorCombo.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.PlumbingContractorColumn].ToString();
                }
                else
                {
                    this.PlumbingContractorCombo.SelectedValue = -1;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.ElectricalContractorColumn].ToString()))
                {
                    this.ElectricalContractorCombo.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.ElectricalContractorColumn].ToString();
                }
                else
                {
                    this.ElectricalContractorCombo.SelectedValue = -1;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OtherContractorColumn].ToString()))
                {
                    this.OtherContractorCombo.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OtherContractorColumn].ToString();
                }
                else
                {
                    this.OtherContractorCombo.SelectedValue = -1;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OccInspDateColumn].ToString()))
                {
                    this.OccupancyInspectionTextBox.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OccInspDateColumn].ToString();
                }
                else
                {
                    this.OccupancyInspectionTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.UserNameColumn].ToString()))
                {
                    this.ApprovedByCombo.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.UserNameColumn].ToString();
                }
                else
                {
                    this.ApprovedByCombo.SelectedValue = -1;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.WorkDescriptionColumn].ToString()))
                {
                    this.DescriptionOfWorkTextBox.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.WorkDescriptionColumn].ToString();
                }
                else
                {
                    this.DescriptionOfWorkTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.BuildingUseColumn].ToString()))
                {
                    this.UseOfBuildingTextBox.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.BuildingUseColumn].ToString();
                }
                else
                {
                    this.UseOfBuildingTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.ConstructionTypeColumn].ToString()))
                {
                    this.TypeOfConstructionTextBox.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.ConstructionTypeColumn].ToString();
                }
                else
                {
                    this.TypeOfConstructionTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OffStreetParkingColumn].ToString()))
                {
                    this.OffStreetParkingTextBox.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OffStreetParkingColumn].ToString();
                }
                else
                {
                    this.OffStreetParkingTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OccupancyGroupColumn].ToString()))
                {
                    this.OccupancyGroupTextBox.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OccupancyGroupColumn].ToString();
                }
                else
                {
                    this.OccupancyGroupTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.DwellingUnitsColumn].ToString()))
                {
                    this.DwellingUnitsTextBox.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.DwellingUnitsColumn].ToString();
                }
                else
                {
                    this.DwellingUnitsTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.ZoneColumn].ToString()))
                {
                    this.ZoneTextBox.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.ZoneColumn].ToString();
                }
                else
                {
                    this.ZoneTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.SprinklerColumn].ToString()))
                {
                    this.SprinklerTextBox.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.SprinklerColumn].ToString();
                }
                else
                {
                    this.SprinklerTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OccupancyLoadColumn].ToString()))
                {
                    this.OccupancyLoadTextBox.Text = this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.OccupancyLoadColumn].ToString();
                }
                else
                {
                    this.OccupancyLoadTextBox.Text = string.Empty;
                }

                /*Get the PermitID*/
                this.permitID = Convert.ToInt32(this.buildingPermitData.F82001GetBuildingPermitDetails.Rows[0][this.buildingPermitData.F82001GetBuildingPermitDetails.PermitIDColumn]);
            }
            else
            {
                if (this.gdocEventHeaderData.GetGDocEventHeader.Rows.Count > 0)
                {
                    this.ShowPanel(true);
                }
                else
                {
                    this.ShowPanel(false);
                }

                this.LegalDescriptionTextBox.Text = string.Empty;
                this.OwnerNameTextBox.Text = string.Empty;
                this.OwnerPhoneNumberTextBox.Text = string.Empty;
                this.OwnerAddressTextBox.Text = string.Empty;
                this.OwnerCityTextBox.Text = string.Empty;
                this.StateTextBox.Text = string.Empty;
                this.ZipCodeTextBox.Text = string.Empty;
                this.GeneralContractorCombo.SelectedValue = -1;
                this.MechanicalContractorScanCombo.SelectedValue = -1;
                this.PlumbingContractorCombo.SelectedValue = -1;
                this.ElectricalContractorCombo.SelectedValue = -1;
                this.OtherContractorCombo.SelectedValue = -1;
                this.OccupancyInspectionTextBox.Text = string.Empty;
                this.ApprovedByCombo.SelectedValue = -1;
                this.DescriptionOfWorkTextBox.Text = string.Empty;
                this.UseOfBuildingTextBox.Text = string.Empty;
                this.TypeOfConstructionTextBox.Text = string.Empty;
                this.OffStreetParkingTextBox.Text = string.Empty;
                this.OccupancyGroupTextBox.Text = string.Empty;
                this.DwellingUnitsTextBox.Text = string.Empty;
                this.ZoneTextBox.Text = string.Empty;
                this.SprinklerTextBox.Text = string.Empty;
                this.OccupancyLoadTextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// Saves the building permit details.
        /// </summary>
        /// <returns>Bool</returns>
        private bool SaveBuildingPermitDetails()
        {
            int generalContractorId = -99;
            int plumbingContractorId, mechanicalContractorId, electricalContractorId, otherContractorId, approvedById;
            int occupancyLoadVal;
            short dwellingUnitsVal;

            /*Check the contractor id's for null and Try parse the values to get the contractor id's*/
            if (this.GeneralContractorCombo.SelectedValue != null)
            {
                int.TryParse(this.GeneralContractorCombo.SelectedValue.ToString(), out generalContractorId);
            }
            else
            {
                generalContractorId = -99;
            }

            if (this.MechanicalContractorScanCombo.SelectedValue != null)
            {
                int.TryParse(this.MechanicalContractorScanCombo.SelectedValue.ToString(), out mechanicalContractorId);
            }
            else
            {
                mechanicalContractorId = -99;
            }

            if (this.PlumbingContractorCombo.SelectedValue != null)
            {
                int.TryParse(this.PlumbingContractorCombo.SelectedValue.ToString(), out plumbingContractorId);
            }
            else
            {
                plumbingContractorId = -99;
            }

            if (this.ElectricalContractorCombo.SelectedValue != null)
            {
                int.TryParse(this.ElectricalContractorCombo.SelectedValue.ToString(), out electricalContractorId);
            }
            else
            {
                electricalContractorId = -99;
            }

            if (this.OtherContractorCombo.SelectedValue != null)
            {
                int.TryParse(this.OtherContractorCombo.SelectedValue.ToString(), out otherContractorId);
            }
            else
            {
                otherContractorId = -99;
            }

            if (this.ApprovedByCombo.SelectedValue != null)
            {
                int.TryParse(this.ApprovedByCombo.SelectedValue.ToString(), out approvedById);
            }
            else
            {
                approvedById = -99;
            }

            Int16.TryParse(this.DwellingUnitsTextBox.Text.ToString(), out dwellingUnitsVal);
            int.TryParse(this.OccupancyLoadTextBox.Text.ToString(), out occupancyLoadVal);

            /*Variable to store the XML*/
            string buildingDetailsxml = string.Empty;

            /*Creating instance for the Dataset and Datatable and Datarow*/
            F82001BuildingPermitData buildingPermitData = new F82001BuildingPermitData();
            F82001BuildingPermitData.F82001GetBuildingPermitDetailsRow buildingPermitDetailsRow = buildingPermitData.F82001GetBuildingPermitDetails.NewF82001GetBuildingPermitDetailsRow();

            /*Bind the row with the updated values inorder to insert into the datatable*/
            buildingPermitDetailsRow.LegalDescription = this.LegalDescriptionTextBox.Text.Trim();
            buildingPermitDetailsRow.OwnerName = this.OwnerNameTextBox.Text.Trim();
            buildingPermitDetailsRow.OwnerPhone = this.OwnerPhoneNumberTextBox.Text.Trim();
            buildingPermitDetailsRow.OwnerAddress = this.OwnerAddressTextBox.Text.Trim();
            buildingPermitDetailsRow.OwnerCity = this.OwnerCityTextBox.Text.Trim();
            buildingPermitDetailsRow.OwnerState = this.StateTextBox.Text.Trim();
            buildingPermitDetailsRow.OwnerZip = this.ZipCodeTextBox.Text.Trim();

            buildingPermitDetailsRow.GeneralContractorID = generalContractorId;
            buildingPermitDetailsRow.MechanicalContractorID = mechanicalContractorId;
            buildingPermitDetailsRow.PlumbingContractorID = plumbingContractorId;
            buildingPermitDetailsRow.ElectricalContractorID = electricalContractorId;
            buildingPermitDetailsRow.OtherContractorID = otherContractorId;
            buildingPermitDetailsRow.ApprovedByID = approvedById;

            buildingPermitDetailsRow.OccInspDate = this.OccupancyInspectionTextBox.Text.Trim();
            buildingPermitDetailsRow.WorkDescription = this.DescriptionOfWorkTextBox.Text.Trim();
            buildingPermitDetailsRow.BuildingUse = this.UseOfBuildingTextBox.Text.Trim();
            buildingPermitDetailsRow.ConstructionType = this.TypeOfConstructionTextBox.Text.Trim();
            buildingPermitDetailsRow.OffStreetParking = this.OffStreetParkingTextBox.Text.Trim();
            buildingPermitDetailsRow.OccupancyGroup = this.OccupancyGroupTextBox.Text.Trim();
            buildingPermitDetailsRow.DwellingUnits = dwellingUnitsVal;
            buildingPermitDetailsRow.Zone = this.ZoneTextBox.Text;
            buildingPermitDetailsRow.Sprinkler = this.SprinklerTextBox.Text.Trim();
            buildingPermitDetailsRow.OccupancyLoad = occupancyLoadVal;

            buildingPermitDetailsRow.EventID = this.eventID;

            /*Add the temporary row into the datatable*/
            buildingPermitData.F82001GetBuildingPermitDetails.Rows.Add(buildingPermitDetailsRow);
            /*Gets the XML string and store it in the variable*/
            buildingDetailsxml = TerraScanCommon.GetXmlString(buildingPermitData.F82001GetBuildingPermitDetails);

            this.form82001Control.WorkItem.F82001_InsertBuildingPermitDetails(this.permitID, buildingDetailsxml, TerraScan.Common.TerraScanCommon.UserId);

            this.flagLoadOnProcess = true;
            this.CustomizeAllComboBoxes();
            this.ListBuildingPermitDetails();
            this.flagLoadOnProcess = false;

            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);

            ////this.LegalDescriptionTextBox.Focus();            

            return true;
        }

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            if (this.form82001Control.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
            {
                this.operationSmartPart = (OperationSmartPart)this.form82001Control.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart);
            }
            else
            {
                this.operationSmartPart = (OperationSmartPart)this.form82001Control.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart);
            }
        }

        /// <summary>
        /// Customizes all combo boxes.
        /// </summary>
        private void CustomizeAllComboBoxes()
        {
            /*Customize the Combo boxes*/
            this.GeneralContractorCombo.MaxLength = this.contractorManagementData.ListContractorManagement.ContractorNameColumn.MaxLength;
            this.GeneralContractorCombo.DisplayMember = this.contractorManagementData.ListContractorManagement.ContractorNameColumn.ColumnName;
            this.GeneralContractorCombo.ValueMember = this.contractorManagementData.ListContractorManagement.ContractorIDColumn.ColumnName;

            this.PlumbingContractorCombo.MaxLength = this.contractorManagementData.ListContractorManagement.ContractorNameColumn.MaxLength;
            this.PlumbingContractorCombo.DisplayMember = this.contractorManagementData.ListContractorManagement.ContractorNameColumn.ColumnName;
            this.PlumbingContractorCombo.ValueMember = this.contractorManagementData.ListContractorManagement.ContractorIDColumn.ColumnName;

            this.MechanicalContractorScanCombo.MaxLength = this.contractorManagementData.ListContractorManagement.ContractorNameColumn.MaxLength;
            this.MechanicalContractorScanCombo.DisplayMember = this.contractorManagementData.ListContractorManagement.ContractorNameColumn.ColumnName;
            this.MechanicalContractorScanCombo.ValueMember = this.contractorManagementData.ListContractorManagement.ContractorIDColumn.ColumnName;

            this.ElectricalContractorCombo.MaxLength = this.contractorManagementData.ListContractorManagement.ContractorNameColumn.MaxLength;
            this.ElectricalContractorCombo.DisplayMember = this.contractorManagementData.ListContractorManagement.ContractorNameColumn.ColumnName;
            this.ElectricalContractorCombo.ValueMember = this.contractorManagementData.ListContractorManagement.ContractorIDColumn.ColumnName;

            this.OtherContractorCombo.MaxLength = this.contractorManagementData.ListContractorManagement.ContractorNameColumn.MaxLength;
            this.OtherContractorCombo.DisplayMember = this.contractorManagementData.ListContractorManagement.ContractorNameColumn.ColumnName;
            this.OtherContractorCombo.ValueMember = this.contractorManagementData.ListContractorManagement.ContractorIDColumn.ColumnName;

            /*Customize the UserList Combo box*/
            this.ApprovedByCombo.MaxLength = this.userListData.ListUsers.Name_DisplayColumn.MaxLength;
            this.ApprovedByCombo.DisplayMember = this.userListData.ListUsers.Name_DisplayColumn.ColumnName;
            this.ApprovedByCombo.ValueMember = this.userListData.ListUsers.UserIDColumn.ColumnName;
        }

        /// <summary>
        /// Loads all combo boxes.
        /// </summary>
        private void LoadAllComboBoxes()
        {
            /*Load the temp dataet with the Source*/
            this.contractorManagementData = this.form82001Control.WorkItem.F82002_ListContractorManagementData(null, null);
            this.userListData = this.form82001Control.WorkItem.ListUserNames();

            /*Binding all the comboboxes with the Source*/
            if (this.contractorManagementData.ListContractorManagement.Rows.Count > 0)
            {
                /*To Create an Empty row in the combobox items*/
                F82002ContractorManagementData.ListContractorManagementDataTable listGeneralContractorComboDatatable = new F82002ContractorManagementData.ListContractorManagementDataTable();
                DataRow listGeneralContractorComboDataRow = listGeneralContractorComboDatatable.NewRow();
                listGeneralContractorComboDataRow[listGeneralContractorComboDatatable.ContractorNameColumn] = string.Empty;
                listGeneralContractorComboDataRow[listGeneralContractorComboDatatable.ContractorIDColumn] = "-99";
                listGeneralContractorComboDatatable.Rows.Add(listGeneralContractorComboDataRow);
                listGeneralContractorComboDatatable.Merge(this.contractorManagementData.ListContractorManagement);
                this.GeneralContractorCombo.DataSource = listGeneralContractorComboDatatable;
            }
            else
            {
                this.GeneralContractorCombo.DataSource = this.contractorManagementData.ListContractorManagement;
            }

            if (this.contractorManagementData.ListContractorManagement.Rows.Count > 0)
            {
                F82002ContractorManagementData.ListContractorManagementDataTable listPlumbingContractorComboDatatable = new F82002ContractorManagementData.ListContractorManagementDataTable();
                DataRow listPlumbingContractorComboDataRow = listPlumbingContractorComboDatatable.NewRow();
                listPlumbingContractorComboDataRow[listPlumbingContractorComboDatatable.ContractorNameColumn] = string.Empty;
                listPlumbingContractorComboDataRow[listPlumbingContractorComboDatatable.ContractorIDColumn] = "-99";
                listPlumbingContractorComboDatatable.Rows.Add(listPlumbingContractorComboDataRow);
                listPlumbingContractorComboDatatable.Merge(this.contractorManagementData.ListContractorManagement);
                this.PlumbingContractorCombo.DataSource = listPlumbingContractorComboDatatable;
            }
            else
            {
                this.PlumbingContractorCombo.DataSource = this.contractorManagementData.ListContractorManagement;
            }

            if (this.contractorManagementData.ListContractorManagement.Rows.Count > 0)
            {
                F82002ContractorManagementData.ListContractorManagementDataTable listMechanicalContractorComboDatatable = new F82002ContractorManagementData.ListContractorManagementDataTable();
                DataRow listMechanicalContractorComboDataRow = listMechanicalContractorComboDatatable.NewRow();
                listMechanicalContractorComboDataRow[listMechanicalContractorComboDatatable.ContractorNameColumn] = string.Empty;
                listMechanicalContractorComboDataRow[listMechanicalContractorComboDatatable.ContractorIDColumn] = "-99";
                listMechanicalContractorComboDatatable.Rows.Add(listMechanicalContractorComboDataRow);
                listMechanicalContractorComboDatatable.Merge(this.contractorManagementData.ListContractorManagement);
                this.MechanicalContractorScanCombo.DataSource = listMechanicalContractorComboDatatable;
            }
            else
            {
                this.MechanicalContractorScanCombo.DataSource = this.contractorManagementData.ListContractorManagement;
            }

            if (this.contractorManagementData.ListContractorManagement.Rows.Count > 0)
            {
                F82002ContractorManagementData.ListContractorManagementDataTable listElectricalContractorComboDatatable = new F82002ContractorManagementData.ListContractorManagementDataTable();
                DataRow listElectricalContractorComboDataRow = listElectricalContractorComboDatatable.NewRow();
                listElectricalContractorComboDataRow[listElectricalContractorComboDatatable.ContractorNameColumn] = string.Empty;
                listElectricalContractorComboDataRow[listElectricalContractorComboDatatable.ContractorIDColumn] = "-99";
                listElectricalContractorComboDatatable.Rows.Add(listElectricalContractorComboDataRow);
                listElectricalContractorComboDatatable.Merge(this.contractorManagementData.ListContractorManagement);
                this.ElectricalContractorCombo.DataSource = listElectricalContractorComboDatatable;
            }
            else
            {
                this.ElectricalContractorCombo.DataSource = this.contractorManagementData.ListContractorManagement;
            }

            if (this.contractorManagementData.ListContractorManagement.Rows.Count > 0)
            {
                F82002ContractorManagementData.ListContractorManagementDataTable listOtherContractorComboDatatable = new F82002ContractorManagementData.ListContractorManagementDataTable();
                DataRow listOtherContractorComboDataRow = listOtherContractorComboDatatable.NewRow();
                listOtherContractorComboDataRow[listOtherContractorComboDatatable.ContractorNameColumn] = string.Empty;
                listOtherContractorComboDataRow[listOtherContractorComboDatatable.ContractorIDColumn] = "-99";
                listOtherContractorComboDatatable.Rows.Add(listOtherContractorComboDataRow);
                listOtherContractorComboDatatable.Merge(this.contractorManagementData.ListContractorManagement);
                this.OtherContractorCombo.DataSource = listOtherContractorComboDatatable;
            }
            else
            {
                this.OtherContractorCombo.DataSource = this.contractorManagementData.ListContractorManagement;
            }

            if (this.userListData.ListUsers.Rows.Count > 0)
            {
                SupportFormData.ListUsersDataTable listApprovedByContractorComboDatatable = new SupportFormData.ListUsersDataTable();
                DataRow listApprovedByContractorComboDataRow = listApprovedByContractorComboDatatable.NewRow();
                listApprovedByContractorComboDataRow[listApprovedByContractorComboDatatable.Name_DisplayColumn] = string.Empty;
                listApprovedByContractorComboDataRow[listApprovedByContractorComboDatatable.UserIDColumn] = "-99";
                listApprovedByContractorComboDatatable.Rows.Add(listApprovedByContractorComboDataRow);
                listApprovedByContractorComboDatatable.Merge(this.userListData.ListUsers);
                this.ApprovedByCombo.DataSource = listApprovedByContractorComboDatatable;
            }
            else
            {
                this.ApprovedByCombo.DataSource = this.userListData.ListUsers;
            }
        }

        /// <summary>
        /// Shows the panel.
        /// </summary>
        /// <param name="show">if set to <c>true</c> [show].</param>
        private void ShowPanel(bool show)
        {
            /*Enabling and Disabling the Panel*/
            this.LegalDescriptionPanel.Enabled = show;
            this.OwnerNamepanel.Enabled = show;
            this.OwnerPhoneNumberpanel.Enabled = show;
            this.OwnerAddresspanel.Enabled = show;
            this.OwnerCitypanel.Enabled = show;
            this.Statepanel.Enabled = show;
            this.ZipCodepanel.Enabled = show;
            this.GeneralContractorPanel.Enabled = show;
            this.PlumbingContractorpanel.Enabled = show;
            this.ElectricalContractorpanel.Enabled = show;
            this.MechanicalContractorpanel.Enabled = show;
            this.OtherContractorpanel.Enabled = show;
            this.OccupancyInspectionpanel.Enabled = show;
            this.ApprovedBypanel.Enabled = show;
            this.DescriptionOfWorkpanel.Enabled = show;
            this.UseOfBuildingpanel.Enabled = show;
            this.TypeOfConstructionpanel.Enabled = show;
            this.OffStreetParkingpanel.Enabled = show;
            this.OccupancyGrouppanel.Enabled = show;
            this.DwellingUnitspanel.Enabled = show;
            this.Zonepanel.Enabled = show;
            this.Sprinklerpanel.Enabled = show;
            this.OccupancyLoadpanel.Enabled = show;
        }

        /// <summary>
        /// Controls the lock.
        /// </summary>
        /// <param name="controlLock">if set to <c>true</c> [control look].</param>
        private void ControlLock(bool controlLock)
        {
            /*Set the LockKeyPress*/
            this.LegalDescriptionTextBox.LockKeyPress = controlLock;
            this.OwnerNameTextBox.LockKeyPress = controlLock;
            this.OwnerPhoneNumberTextBox.LockKeyPress = controlLock;
            this.OwnerAddressTextBox.LockKeyPress = controlLock;
            this.OwnerCityTextBox.LockKeyPress = controlLock;
            this.StateTextBox.LockKeyPress = controlLock;
            this.ZipCodeTextBox.LockKeyPress = controlLock;
            this.GeneralContractorCombo.Enabled = !controlLock;
            this.GeneralContractorButton.Enabled = !controlLock;
            this.PlumbingContractorCombo.Enabled = !controlLock;
            this.PlumbingContractorbutton.Enabled = !controlLock;
            this.MechanicalContractorScanCombo.Enabled = !controlLock;
            this.MechanicalContractorbutton.Enabled = !controlLock;
            this.ElectricalContractorCombo.Enabled = !controlLock;
            this.ElectricalContractorbutton.Enabled = !controlLock;
            this.OtherContractorCombo.Enabled = !controlLock;
            this.OtherContractorbutton.Enabled = !controlLock;
            this.OccupancyInspectionDateButton.Enabled = !controlLock;
            this.OccupancyInspectionCalander.Enabled = !controlLock;
            this.ApprovedByCombo.Enabled = !controlLock;
            this.OccupancyInspectionTextBox.LockKeyPress = controlLock;
            this.DescriptionOfWorkTextBox.LockKeyPress = controlLock;
            this.UseOfBuildingTextBox.LockKeyPress = controlLock;
            this.TypeOfConstructionTextBox.LockKeyPress = controlLock;
            this.OffStreetParkingTextBox.LockKeyPress = controlLock;
            this.OccupancyGroupTextBox.LockKeyPress = controlLock;
            this.DwellingUnitsTextBox.LockKeyPress = controlLock;
            this.ZoneTextBox.LockKeyPress = controlLock;
            this.SprinklerTextBox.LockKeyPress = controlLock;
            this.OccupancyLoadTextBox.LockKeyPress = controlLock;
        }

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            /*Enable on Edit*/
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        /// <summary>
        /// Shows the occupancy inspection calender.
        /// </summary>
        private void ShowOccupancyInspectionCalender()
        {
            /*set the calendar visibility*/
            if (!string.IsNullOrEmpty(this.OccupancyInspectionTextBox.Text))
            {
                this.OccupancyInspectionCalander.SetDate(Convert.ToDateTime(this.OccupancyInspectionTextBox.Text));
            }

            this.OccupancyInspectionCalander.Visible = true;
            this.issShift = false;
            this.OccupancyInspectionCalander.Focus();
        }

        /// <summary>
        /// Sets the selected date.
        /// </summary>
        /// <param name="dateSelected">The date selected.</param>
        private void SetSelectedDate(string dateSelected)
        {
            /*Set the Date on corresponding TextBox*/
            this.OccupancyInspectionCalander.Tag = string.Empty;
            this.OccupancyInspectionCalander.Focus();
            this.OccupancyInspectionCalander.Text = dateSelected;
            this.OccupancyInspectionTextBox.Text = dateSelected;
            this.OccupancyInspectionTextBox.Focus();
            this.OccupancyInspectionCalander.Visible = false;
        }

        /// <summary>
        /// Validates the slice form.
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        private void ValidateSliceForm(DataEventArgs<int> eventArgs)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = eventArgs.Data;
            this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
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
            return sliceValidationFields;
        }

        #endregion PrivateMethods
    }
}

