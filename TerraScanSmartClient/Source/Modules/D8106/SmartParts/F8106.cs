//--------------------------------------------------------------------------------------------
// <copyright file="F8106.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8106.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 Oct 06        JAYANTHI              Created
// 10 Nov 06        JAYANTHI              Modified(Code review issues - fixed)  
//*********************************************************************************/

namespace D8106
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
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using System.Web.Services.Protocols;

    /// <summary>
    /// F8106 UI designer Class to subscribe all individual events
    /// </summary>
    [SmartPart]
    public partial class F8106 : BaseSmartPart
    {
        #region Member Variables
        /// <summary>
        /// To check whether the form is loaded first time or not
        /// </summary>
        private bool flagFormLoad;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;


        /// <summary>
        /// DataSet which is used to get the details of Stoppage event
        /// </summary>
        private StoppageEventData stoppageEventData = new StoppageEventData();

        /// <summary>
        /// Instance of 8106 Controller to call the WorkItem
        /// </summary>
        private F8106Controller form8106Control;

        /// <summary>
        /// To Check the Page Mode whether Edit,View, Delete,New
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyID;

        /// <summary>
        /// Permission of this slice form from the Master Page
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// Form Number of the masterForm 
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// PermissionFields struct 
        /// </summary>
        private PermissionFields permissions;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        #endregion Member Variables

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8106"/> class.
        /// </summary>
        public F8106()
        {
            InitializeComponent();
            this.flagFormLoad = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8102"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F8106(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyID = keyID;
            this.StoppageEventPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(StoppageEventPictureBox.Width, StoppageEventPictureBox.Height, string.Empty, red, green, blue);
        }
        #endregion Constructor

        #region Event Publication
        /// <summary>
        /// event publication for Show the child form 
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

        #endregion Event Publication

        #region Property
        /// <summary>
        /// For F8030Control
        /// </summary>
        [CreateNew]
        public F8106Controller Form8106Control
        {
            get { return this.form8106Control as F8106Controller; }
            set { this.form8106Control = value; }
        }
        #endregion Property

        #region Event Subscription
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
                    this.keyID = eventArgs.Data.SelectedKeyId;
                    this.CallDefaultInLoading();
                }
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
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                if (this.PermissionFiled.editPermission)
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
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                if (this.PermissionFiled.editPermission)
                {
                    this.F8106_SaveDetails();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            else
            {
                this.ShowPanel(true);
                this.ShowControlsByformPermissions();
                this.F8106_GetStoppageEventDetails();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ set slice permission].
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

                    if (this.stoppageEventData.GetEventStoppage.Rows.Count > 0)
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

                this.ShowControlsByformPermissions();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// To call the Cancel button click in Master Form
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="eventArgs">eventArgs</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
        {
            this.ShowControlsByformPermissions();
            this.ShowPanel(true);
            this.F8106_GetStoppageEventDetails();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        /// <summary>
        /// Calls the New Method in Master Form
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="eventArgs">eventArgs</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.New;
            this.ClearControls();
            this.ShowControls(false);
            this.ShowPanel(false);
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Loads the page with the default values
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F8106_Load(object sender, EventArgs e)
        {
            try
            {
                this.CallDefaultInLoading();
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
        /// Handles the CloseUp event of the riskManagerDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RiskManagerDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.RiskManagerDateTextBox.Text = RiskManagerDateTimePicker.Text;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the RiskManagerDatePict control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RiskManagerDatePict_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(RiskManagerDateTextBox.Text.Trim()))
                {
                    RiskManagerDateTimePicker.Value = Convert.ToDateTime(RiskManagerDateTextBox.Text);
                }
                else
                {
                    RiskManagerDateTimePicker.Value = DateTime.Today;
                }

                RiskManagerDateTimePicker.Focus();
                SendKeys.Send("{F4}");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// To enable the Save Button in Master Form, when Key press in this Text Box
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void RiskManagerDateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                this.EnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// To enable the Save Button in Master Form, when check changed in this Check Box
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void DamageCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.EnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// To enable the Save Button in Master Form, when Check changed in this Check Box
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void PicturesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.EnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// To enable the Save Button in Master Form, when text changed in This Text Box
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void RiskManagerDateTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.EnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// On Mouse Enter Displays the tooltip
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void StoppageEventPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.stoppageEventTooltip.SetToolTip(this.StoppageEventPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Key press event to hide month calendar control shown from Datetime picker when Tab is pressed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void RiskManagerDateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int keyCode = (int)e.KeyChar;
                if (keyCode == 9)
                {
                    SendKeys.Send("{Esc}");
                }

                DamageCheckBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion Event Handlers

        #region User Defined Methods
        /// <summary>
        /// Calls the methods while loading
        /// </summary>
        private void CallDefaultInLoading()
        {
            this.FlagSliceForm = true;
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.F8106_GetStoppageEventDetails();
            this.flagFormLoad = true;
            this.ShowControlsByformPermissions();
        }

        /// <summary>
        /// If the form and from master has edit permission, controls are enabled, else disabled
        /// </summary>
        private void ShowControlsByformPermissions()
        {
            // if condition modified for the issue 1104[should allow to edit a form when edit permission is avilable].
            if (this.PermissionFiled.editPermission)
            {
                this.ShowControls(true);
            }
            else
            {
                this.ShowControls(false);
            }
        }

        /// <summary>
        /// On New Click, clears all the controls 
        /// </summary>
        private void ClearControls()
        {
            this.flagFormLoad = false;
            this.RiskManagerDateTextBox.Text = string.Empty;
            this.DamageCheckBox.Checked = false;
            this.PicturesCheckBox.Checked = false;
            this.flagFormLoad = true;
        }

        /// <summary>
        /// Check for the data entered in this slice form whether valid or invalid
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            if (string.IsNullOrEmpty(this.RiskManagerDateTextBox.Text.Trim()))
            {
                this.RiskManagerDateTextBox.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("StoppageEventValidation");
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Gets the Details of the Stoppage Event (GDoc Footer) and assigns to the controls
        /// </summary>
        private void F8106_GetStoppageEventDetails()
        {
            bool damageCheck = false;
            bool picturesCheck = false;
            this.Cursor = Cursors.WaitCursor;
            this.stoppageEventData = this.form8106Control.WorkItem.F8106_GetStoppageEventDetails(this.keyID);

            if (this.stoppageEventData.GetEventStoppage.Rows.Count > 0)
            {
                this.RiskManagerDateTextBox.Text = this.stoppageEventData.GetEventStoppage.Rows[0][this.stoppageEventData.GetEventStoppage.RManagerDateColumn].ToString();
                damageCheck = Convert.ToBoolean(this.stoppageEventData.GetEventStoppage.Rows[0][this.stoppageEventData.GetEventStoppage.DamageColumn]);
                picturesCheck = Convert.ToBoolean(this.stoppageEventData.GetEventStoppage.Rows[0][this.stoppageEventData.GetEventStoppage.PictureColumn]);
                this.AssignCheckBoxValue(DamageCheckBox, damageCheck);
                this.AssignCheckBoxValue(PicturesCheckBox, picturesCheck);
            }
            else
            {
                this.ShowControls(false);
                this.ShowPanel(false);
            }

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Assigns the check box value to true or false accordingly
        /// </summary>
        /// <param name="checkBox">Control Name</param>
        /// <param name="selectedValue">Value to be passed</param>
        private void AssignCheckBoxValue(TerraScanCheckBox checkBox, bool selectedValue)
        {
            try
            {
                checkBox.Checked = selectedValue;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Passes the stoppage Event Details to the Helper Class through Controller Class
        /// </summary>
        private void F8106_SaveDetails()
        {
            this.Cursor = Cursors.WaitCursor;
            this.stoppageEventData.SaveEventStoppage.Rows.Clear();
            StoppageEventData.SaveEventStoppageRow dataRow = this.stoppageEventData.SaveEventStoppage.NewSaveEventStoppageRow();
            dataRow.EventID = this.keyID;
            if (!string.IsNullOrEmpty(this.RiskManagerDateTextBox.Text.Trim()))
            {
                dataRow.RManagerDate = this.RiskManagerDateTextBox.Text.Trim();
            }

            dataRow.Damage = this.DamageCheckBox.Checked;
            dataRow.Picture = this.PicturesCheckBox.Checked;
            this.stoppageEventData.SaveEventStoppage.Rows.Add(dataRow);

            DataSet stoppageEventDataSet = new DataSet("Root");
            stoppageEventDataSet.Tables.Add(this.stoppageEventData.SaveEventStoppage.Copy());
            stoppageEventDataSet.Tables[0].TableName = "Table";

            this.stoppageEventData.Merge(this.form8106Control.WorkItem.F8106_SaveStoppageEventDetails(stoppageEventDataSet.GetXml(), TerraScanCommon.UserId));
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Enables or disables the Panels accordingly
        /// </summary>
        /// <param name="show">bool value to enable/Disable</param>
        private void ShowPanel(bool show)
        {
            this.RiskManagerDatePanel.Enabled = show;
            this.DamagePanel.Enabled = show;
            this.PicturesPanel.Enabled = show;
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="show">if set to <c>true</c> [lock control].</param>
        private void ShowControls(bool show)
        {
            this.RiskManagerDateTextBox.LockKeyPress = !show;
            this.DamageCheckBox.Enabled = show;
            this.PicturesCheckBox.Enabled = show;
            this.RiskManagerDateTimePicker.Enabled = show;
        }

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified
        /// </summary>
        private void EnableEditButtonInMasterForm()
        {
            if (this.flagFormLoad && this.pageMode == TerraScanCommon.PageModeTypes.View && this.PermissionFiled.editPermission)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }
        #endregion User Defined Methods
    }
}
