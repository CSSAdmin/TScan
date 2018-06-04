//--------------------------------------------------------------------------------------------
// <copyright file="F29630.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F29620 Agland Application.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 01/10/2008        D.LathaMaheswari  Created
//***********************************************************************************************/

namespace D24630
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
    using TerraScan.Utilities;
    using System.Collections;
    using TerraScan.Infrastructure.Interface.Constants;
    using Infrastructure.Interface;

    /// <summary>
    /// 29630
    /// </summary>
    [SmartPart]
    public partial class F29630 : BaseSmartPart
    {
        #region Variable

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// Used to store the form slice BOE id
        /// </summary>
        private int boeId;

        /// <summary>
        /// Used to store the form slice event id
        /// </summary>
        private int keyId;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// controller F29630
        /// </summary>
        private F29630Controller form29630Control;

        /// <summary>
        /// BoardOfEqualizationData DataSet
        /// </summary>
        private F29630BoardOfEqualizationData boardOfEqualizationData = new F29630BoardOfEqualizationData();

        /// <summary>
        /// User DataSet
        /// </summary>
        private UserManagementData userManagementData = new UserManagementData();

        /// <summary>
        /// Set Date Format
        /// </summary>
        private string dateFormat = "M/d/yyyy";

        /// <summary>
        /// Flag for Form Load
        /// </summary>
        private bool flagFormLoad;

        /// <summary>
        /// Store the sum of Assessed Value
        /// </summary>
        private Double assessedSumValue;

        /// <summary>
        /// Store the sum of Apellants Value
        /// </summary>
        private Double apellantsSumValue;

        /// <summary>
        /// Store the sum of Stipulated Value
        /// </summary>
        private Double stipulatedSumValue;

        /// <summary>
        /// Store the sum of BOE Value
        /// </summary>
        private Double boeSumValue;

        #endregion Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F29630"/> class.
        /// </summary>
        public F29630()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F29620"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F29630(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.EqualizationpictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.EqualizationpictureBox.Height, this.EqualizationpictureBox.Width, "Protest", red, green, blue);
            this.ValuesPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ValuesPictureBox.Height, this.ValuesPictureBox.Width, "Values", red, green, blue);
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
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;
        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the form29630 control.
        /// </summary>
        /// <value>The form29630 control.</value>
        [CreateNew]
        public F29630Controller Form29630Control
        {
            get { return this.form29630Control as F29630Controller; }
            set { this.form29630Control = value; }
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
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;
                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);

                if (this.boardOfEqualizationData.GetBOEDetails.Rows.Count > 0)
                {
                    eventArgs.Data.FlagInvalidSliceKey = false;
                    ////khaja commented code to fix Bug#4870
                    ////this.ActiveControl = this.ProtestedByTextBox;
                    ////this.ActiveControl.Focus();
                    this.GridPanel.Enabled = this.permissionFields.editPermission;
                }
                else
                {
                    //// Coding Added for the issue 4212 0n 30/5/2009.
                    //// Last Slice does not have a record also it will not return invalid slice
                    if (eventArgs.Data.FlagInvalidSliceKey)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = true;
                    }
                    this.GridPanel.Enabled = false;
                }
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
            if (this.PermissionFiled.newPermission)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.PushValueButton.Enabled = false;
                this.ClearControls();
                this.LockControls(true);
                this.ControlLock(false);
                this.ProtestedByTextBox.Focus();
            }
            else
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.PushValueButton.Enabled = false;
                this.ClearControls();
                this.LockControls(false);
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
            if (this.formMasterPermissionEdit && this.PermissionFiled.editPermission)
            {
                this.ControlLock(false);
            }
            else
            {
                this.ControlLock(true);
            }

            this.LockControls(true);
            this.flagFormLoad = true;
            this.ClearControls();
            this.GetBoardOfEqualizationDetails();
            this.CalculatedValue();
            this.flagFormLoad = false;
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.PushValueButton.Enabled = true;
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
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
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
            {
                this.SaveEqualization();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.PushValueButton.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ delete slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            if (this.slicePermissionField.deletePermission)
            {
                this.Cursor = Cursors.WaitCursor;
                // UserId has been added as a parameter for #8272 CO
                this.form29630Control.WorkItem.F29630DeleteBoardOfEqualizationDetails(this.boeId, TerraScanCommon.UserId);
                SliceFormCloseAlert sliceFormCloseAlert;
                sliceFormCloseAlert.FormNo = this.masterFormNo;
                sliceFormCloseAlert.FlagFormClose = false;
                this.OnFormSlice_FormCloseAlert(new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
                this.Cursor = Cursors.Default;
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
                this.keyId = eventArgs.Data.SelectedKeyId;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.PushValueButton.Enabled = true;
                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                this.flagFormLoad = true;
                this.GetBoardOfEqualizationDetails();
                this.CalculatedValue();
                this.ProtestedByTextBox.Focus();
                this.AssessedLabel.ForeColor = Color.White;
                this.flagFormLoad = false;
                this.ActiveControl = this.ProtestedByTextBox;
                this.ActiveControl.Focus();
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

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F29620 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F29630_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagFormLoad = true;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.PushValueButton.Enabled = true;
                ////this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                this.CustomizeGridView();
                this.LoadAppraiserCombo();
                this.GetBoardOfEqualizationDetails();
                this.GridPanel.Enabled = this.permissionFields.editPermission;
                this.CalculatedValue();
                this.AssessedLabel.ForeColor = Color.White;

                ////khaja added code to fix Bug#487
                if (this.boardOfEqualizationData.GetBOEDetails.Rows.Count > 0)
                {
                    this.ActiveControl = this.ProtestedByTextBox;
                    this.ActiveControl.Focus();
                }

                this.flagFormLoad = false;
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Form Load

        #region Events

        #region SectionIndicator
        /// <summary>
        /// Handles the MouseEnter event of the EqualizationpictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EqualizationpictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.EqualizationToolTip.SetToolTip(this.EqualizationpictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the ValuesPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ValuesPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.EqualizationToolTip.SetToolTip(this.ValuesPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion SectionIndicator

        #region Calender

        /// <summary>
        /// Handles the DateSelected event of the HearingDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void HearingDateCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.HearingDateTextBox.Text = e.Start.ToString(this.dateFormat);
                this.HearingDateCalender.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the HearingDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void HearingDateCalender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.HearingDateTextBox.Text = this.HearingDateCalender.SelectionStart.ToString(this.dateFormat);
                    this.HearingDateCalender.Visible = false;
                    this.HearingDateTextBox.Focus();
                }

                if (e.KeyCode == Keys.Escape)
                {
                    this.HearingDateCalender.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the ActionDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ActionDateCalender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.ActionDateTextBox.Text = this.ActionDateCalender.SelectionStart.ToString(this.dateFormat);
                    this.ActionDateCalender.Visible = false;
                    this.ActionDateTextBox.Focus();
                }

                if (e.KeyCode == Keys.Escape)
                {
                    this.ActionDateCalender.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the ActionDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void ActionDateCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.ActionDateTextBox.Text = e.Start.ToString(this.dateFormat);
                this.ActionDateCalender.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the ClosedDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void ClosedDateCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.ClosedDateTextBox.Text = e.Start.ToString(this.dateFormat);
                this.ClosedDateCalender.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the ClosedDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ClosedDateCalender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.ClosedDateTextBox.Text = this.ClosedDateCalender.SelectionStart.ToString(this.dateFormat);
                    this.ClosedDateCalender.Visible = false;
                    this.ClosedDateTextBox.Focus();
                }

                if (e.KeyCode == Keys.Escape)
                {
                    this.ClosedDateCalender.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the FillingDatebutton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FillingDatebutton_Click(object sender, EventArgs e)
        {
            try
            {
                this.HearingDateCalender.Visible = true;
                this.HearingDateCalender.ScrollChange = 1;

                // Display the Calender control near the Calender Picture box.
                //this.HearingDateCalender.Left = this.HearingDatepanel.Left + this.FillingDatebutton.Left + this.FillingDatebutton.Width;
                //this.HearingDateCalender.Top = this.HearingDatepanel.Top + this.FillingDatebutton.Top;

                this.HearingDateCalender.Left = this.HearingDatepanel.Left;
                this.HearingDateCalender.Top = this.HearingDatepanel.Top + this.FillingDatebutton.Bottom;

                this.HearingDateCalender.Tag = this.FillingDatebutton.Tag;
                this.HearingDateCalender.BringToFront();
                this.HearingDateCalender.Focus();
                if (!string.IsNullOrEmpty(this.HearingDateTextBox.Text))
                {
                    this.HearingDateCalender.SetDate(Convert.ToDateTime(this.HearingDateTextBox.Text));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ActionDateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ActionDateButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ActionDateCalender.Visible = true;
                this.ActionDateCalender.ScrollChange = 1;

                // Display the Calender control near the Calender Picture box.
                this.ActionDateCalender.Left = this.ActionDatePanel.Left;// +this.ActionDateButton.Left + this.ActionDateButton.Width;
                this.ActionDateCalender.Top = this.ActionDatePanel.Top + this.ActionDateButton.Bottom;
                this.ActionDateCalender.Tag = this.ActionDateButton.Tag;
                this.ActionDateCalender.BringToFront();
                this.ActionDateCalender.Focus();
                if (!string.IsNullOrEmpty(this.ActionDateTextBox.Text))
                {
                    this.ActionDateCalender.SetDate(Convert.ToDateTime(this.ActionDateTextBox.Text));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ClosedDateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClosedDateButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ClosedDateCalender.Visible = true;
                this.ClosedDateCalender.ScrollChange = 1;

                //// Display the Calender control near the Calender Picture box.
                //this.ClosedDateCalender.Left = 523;
                //this.ClosedDateCalender.Top = this.ClosedDatePanel.Top + this.ClosedDateButton.Top;

                this.ClosedDateCalender.Left = this.ClosedDatePanel.Left - 50;// +this.ActionDateButton.Left + this.ActionDateButton.Width;
                this.ClosedDateCalender.Top = this.ClosedDatePanel.Top + this.ClosedDateButton.Bottom;

                this.ClosedDateCalender.Tag = this.ClosedDateButton.Tag;
                this.ClosedDateCalender.BringToFront();
                this.ClosedDateCalender.Focus();
                if (!string.IsNullOrEmpty(this.ClosedDateTextBox.Text))
                {
                    this.ClosedDateCalender.SetDate(Convert.ToDateTime(this.ClosedDateTextBox.Text));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Calender

        #region Push Value

        /// <summary>
        /// Handles the Click event of the PushValueButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PushValueButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // UserId has been added as a parameter for #8093 CO
                this.form29630Control.WorkItem.F29630PushBoardOfEqualizationDetails(this.boeId, TerraScanCommon.UserId);

                // Mesagebox has been shown for implementing CO TFSID:#12965
                MessageBox.Show("Board Ruling values have been pushed.", "Values pushed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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

        #endregion Push Value

        #region Grid Events

        /// <summary>
        /// Handles the CellFormatting event of the EqualizationGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void EqualizationGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Double outValue;
            if (e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.AssessedValue.Name].Index) || e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.ApellantsValue.Name].Index) || e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.StipulatedValue.Name].Index) || e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.BOEValue.Name].Index))
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                if (e.Value != null && !String.IsNullOrEmpty(e.Value.ToString()))
                {
                    string val = e.Value.ToString();
                    if (Double.TryParse(val, out outValue))
                    {
                        e.Value = "$ " + outValue.ToString("#,##0.00");
                        e.FormattingApplied = true;
                    }
                    else
                    {
                        e.Value = "$ 0.00";
                    }
                }
                else
                {
                   // e.Value = "$ 0.00";
                    e.Value = string.Empty;
                }
            }

            if (e.RowIndex.Equals(4))
            {
                this.EqualizationGridView.Rows[4].Cells[this.AssessedValue.Name].ReadOnly = true;
                this.EqualizationGridView.Rows[4].Cells[this.ValueBreakDown.Name].ReadOnly = true;
                this.EqualizationGridView.Rows[4].Cells[this.ApellantsValue.Name].ReadOnly = true;
                this.EqualizationGridView.Rows[4].Cells[this.StipulatedValue.Name].ReadOnly = true;
                this.EqualizationGridView.Rows[4].Cells[this.BOEValue.Name].ReadOnly = true;
                this.EqualizationGridView.Rows[4].Cells[this.IsRoll.Name].ReadOnly = true;

                this.EqualizationGridView.Rows[4].Cells[this.AssessedValue.Name].Value = string.Empty;
                this.EqualizationGridView.Rows[4].Cells[this.ValueBreakDown.Name].Value = string.Empty;
                this.EqualizationGridView.Rows[4].Cells[this.ApellantsValue.Name].Value = string.Empty;
                this.EqualizationGridView.Rows[4].Cells[this.StipulatedValue.Name].Value = string.Empty;
                this.EqualizationGridView.Rows[4].Cells[this.BOEValue.Name].Value = string.Empty;
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the EqualizationGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void EqualizationGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                e.Control.Validated += new EventHandler(this.Control_Validated);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event of the EqualizationGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void EqualizationGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.AssessedValue.Name].Index))
                {
                    this.assessedSumValue = 0;

                    string rpvalue = this.boardOfEqualizationData.GetBOEValues.Rows[e.RowIndex][this.boardOfEqualizationData.GetBOEValues.AssessedValueColumn.ColumnName].ToString().Replace("$", "").Replace(",", "").Trim();
                    int rplen = rpvalue.Length;
                    int rppos = rpvalue.IndexOf(".");
                    if (rplen > 15)
                    {
                        if (rppos > 15)
                        {
                            this.boardOfEqualizationData.GetBOEValues.Rows[e.RowIndex][this.boardOfEqualizationData.GetBOEValues.AssessedValueColumn.ColumnName] = 0;
                        }

                        if (rppos == -1)
                        {
                            this.boardOfEqualizationData.GetBOEValues.Rows[e.RowIndex][this.boardOfEqualizationData.GetBOEValues.AssessedValueColumn.ColumnName] = 0;
                        }
                    }

                    for (int i = 0; i < this.boardOfEqualizationData.GetBOEValues.Rows.Count; i++)
                    {

                        if (i == 0)
                        {
                            Double tempDecimal;
                            Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[0][this.boardOfEqualizationData.GetBOEValues.AssessedValueColumn.ColumnName].ToString().Replace("$", ""), out tempDecimal);
                            Double outDecimal = 0;
                            Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.AssessedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                            this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.AssessedValueColumn.ColumnName] = outDecimal;
                            this.assessedSumValue = this.assessedSumValue + Convert.ToDouble(outDecimal);
                            if (tempDecimal < 0 || tempDecimal > 0)
                            {
                                i = 1;
                            }
                        }
                        else
                        {
                            Double outDecimal = 0;
                            Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.AssessedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                            this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.AssessedValueColumn.ColumnName] = outDecimal;
                            this.assessedSumValue = this.assessedSumValue + Convert.ToDouble(outDecimal);
                        }
                        
                    }

                    this.AssessedLabel.Text = "$" + this.assessedSumValue.ToString("#,##0.00");
                    this.AssessedLabel.ForeColor = Color.White;
                }
                else if (e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.ApellantsValue.Name].Index))
                {
                    this.apellantsSumValue = 0;
                    string rpvalue = this.boardOfEqualizationData.GetBOEValues.Rows[e.RowIndex][this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName].ToString().Replace("$", "").Replace(",", "").Trim();
                    int rplen = rpvalue.Length;
                    int rppos = rpvalue.IndexOf(".");
                    if (rplen > 15)
                    {
                        if (rppos > 15)
                        {
                            this.boardOfEqualizationData.GetBOEValues.Rows[e.RowIndex][this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName] = 0;
                        }

                        if (rppos == -1)
                        {
                            this.boardOfEqualizationData.GetBOEValues.Rows[e.RowIndex][this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName] = 0;
                        }
                    }

                    for (int i = 0; i < this.boardOfEqualizationData.GetBOEValues.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName].ToString().Trim()))
                        {
                            if (i == 0)
                            {
                                Double tempDecimal;
                                Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[0][this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName].ToString().Replace("$", ""), out tempDecimal);
                                Double outDecimal = 0;
                                Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                                this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName] = outDecimal;
                                this.apellantsSumValue = this.apellantsSumValue + Convert.ToDouble(outDecimal);
                                if (tempDecimal < 0 || tempDecimal > 0)
                                {
                                    i = 1;
                                }
                            }
                            else
                            {
                                Double outDecimal = 0;
                                Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                                this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName] = outDecimal;
                                this.apellantsSumValue = this.apellantsSumValue + Convert.ToDouble(outDecimal);
                            }
                    
                            //Double outDecimal = 0;
                            //Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                            //this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName] = outDecimal;
                            ////if (this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.ValueBreakdownColumn.ColumnName].ToString() != "Market Land Value")
                            ////{
                            //    this.apellantsSumValue = this.apellantsSumValue + Convert.ToDouble(outDecimal);
                            //    if (i == 1)
                            //    {
                            //        Double tempDecimal;
                            //        Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[1][this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName].ToString().Replace("$", ""), out tempDecimal);
                            //        if (tempDecimal < 0 || tempDecimal > 0)
                            //        {
                            //            Double tempvalue;
                            //            Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[0][this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName].ToString().Replace("$", ""), out tempvalue);
                            //            this.apellantsSumValue = this.apellantsSumValue - tempvalue;
                            //        }

                            //    }
                            ////}
                        }
                    }

                    this.AppellantLabel.Text = "$" + this.apellantsSumValue.ToString("#,##0.00");
                }
                else if (e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.StipulatedValue.Name].Index))
                {
                    this.stipulatedSumValue = 0;
                    string rpvalue = this.boardOfEqualizationData.GetBOEValues.Rows[e.RowIndex][this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName].ToString().Replace("$", "").Replace(",", "").Trim();
                    int rplen = rpvalue.Length;
                    int rppos = rpvalue.IndexOf(".");
                    if (rplen > 15)
                    {
                        if (rppos > 15)
                        {
                            this.boardOfEqualizationData.GetBOEValues.Rows[e.RowIndex][this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName] = 0;
                        }

                        if (rppos == -1)
                        {
                            this.boardOfEqualizationData.GetBOEValues.Rows[e.RowIndex][this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName] = 0;
                        }
                    }

                    for (int i = 0; i < this.boardOfEqualizationData.GetBOEValues.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName].ToString().Trim()))
                        {
                            if (i == 0)
                            {
                                Double tempDecimal;
                                Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[0][this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName].ToString().Replace("$", ""), out tempDecimal);
                                Double outDecimal = 0;
                                Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                                this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName] = outDecimal;
                                this.stipulatedSumValue = this.stipulatedSumValue + Convert.ToDouble(outDecimal);
                                if (tempDecimal < 0 || tempDecimal > 0)
                                {
                                    i = 1;
                                }
                            }
                            else
                            {
                                Double outDecimal = 0;
                                Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                                this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName] = outDecimal;
                                this.stipulatedSumValue = this.stipulatedSumValue + Convert.ToDouble(outDecimal);
                            }
                        
                            //Double outDecimal = 0;
                            //Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                            //this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName] = outDecimal;
                            ////if (this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.ValueBreakdownColumn.ColumnName].ToString() != "Market Land Value")
                            ////{
                            //    this.stipulatedSumValue = this.stipulatedSumValue + Convert.ToDouble(outDecimal);
                            ////}
                            //    if (i == 1)
                            //    {
                            //        Double tempDecimal;
                            //        Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[1][this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName].ToString().Replace("$", ""), out tempDecimal);
                            //        if (tempDecimal < 0 || tempDecimal > 0)
                            //        {
                            //            Double tempvalue;
                            //            Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[0][this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName].ToString().Replace("$", ""), out tempvalue);
                            //            this.stipulatedSumValue = this.stipulatedSumValue - tempvalue;
                            //        }

                            //    }
                        }
                    }

                    this.StipulatedLabel.Text = "$" + this.stipulatedSumValue.ToString("#,##0.00");
                }
                else if (e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.BOEValue.Name].Index))
                {
                    this.boeSumValue = 0;
                    string rpvalue = this.boardOfEqualizationData.GetBOEValues.Rows[e.RowIndex][this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName].ToString().Replace("$", "").Replace(",", "").Trim();
                    int rplen = rpvalue.Length;
                    int rppos = rpvalue.IndexOf(".");
                    if (rplen > 15)
                    {
                        if (rppos > 15)
                        {
                            this.boardOfEqualizationData.GetBOEValues.Rows[e.RowIndex][this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName] = 0;
                        }

                        if (rppos == -1)
                        {
                            this.boardOfEqualizationData.GetBOEValues.Rows[e.RowIndex][this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName] = 0;
                        }
                    }

                    for (int i = 0; i < this.boardOfEqualizationData.GetBOEValues.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName].ToString().Trim()))
                        {
                            if (i == 0)
                            {
                                Double tempDecimal;
                                Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[0][this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName].ToString().Replace("$", ""), out tempDecimal);
                                Double outDecimal = 0;
                                Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                                this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName] = outDecimal;
                                this.boeSumValue = this.boeSumValue + Convert.ToDouble(outDecimal);
                                if (tempDecimal < 0 || tempDecimal > 0)
                                {
                                    i = 1;
                                }
                            }
                            else
                            {
                                Double outDecimal = 0;
                                Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                                this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName] = outDecimal;
                                this.boeSumValue = this.boeSumValue + Convert.ToDouble(outDecimal);
                            }
                            /*Change in the Sum if the First value Present Exclude the Second Value
                               If first Value Empty include the Second Value*/
                            //Double outDecimal = 0;
                            //Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                            //this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName] = outDecimal;
                            //this.boeSumValue = this.boeSumValue + Convert.ToDouble(outDecimal);
                            //if (i == 1)
                            //{
                            //    Double tempDecimal;
                            //    Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[1][this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName].ToString().Replace("$", ""), out tempDecimal);
                            //    if (tempDecimal < 0 || tempDecimal > 0)
                            //    {
                            //        Double tempvalue;
                            //        Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[0][this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName].ToString().Replace("$", ""), out tempvalue);
                            //        this.boeSumValue = this.boeSumValue - tempvalue;
                            //    }

                            //}
                        }
                    }

                    this.BOELabel.Text = "$" + this.boeSumValue.ToString("#,##0.00");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the EqualizationGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void EqualizationGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.IsRoll.Name].Index))
                {
                    this.Control_TextChanged(this.EqualizationGridView.Columns[this.IsRoll.Name], e);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Grid Events

        #region Parcel Link
        /// <summary>
        /// Handles the LinkClicked event of the ParcelNumberLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ParcelNumberLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(30000);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.boardOfEqualizationData.GetBOEParcelDetails.Rows[0][this.boardOfEqualizationData.GetBOEParcelDetails.ParcelIDColumn].ToString();
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the PropertyOwnerLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void PropertyOwnerLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(91000);
                formInfo.optionalParameters = new object[1];
                ////formInfo.optionalParameters[0] = this.boardOfEqualizationData.GetBOEParcelDetails.Rows[0][this.boardOfEqualizationData.GetBOEParcelDetails.PhoneNumberColumn].ToString();

                //// Implemented CO:4868 on feb12th 2009 by A.Shanmuga Sundaram
                formInfo.optionalParameters[0] = this.boardOfEqualizationData.GetBOEParcelDetails.Rows[0][this.boardOfEqualizationData.GetBOEParcelDetails.OwnerIDColumn].ToString();
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion Parcel Link

        #region ToolTip

        /// <summary>
        /// Handles the MouseHover event of the AssessedLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AssessedLabel_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.ValueToolTip.RemoveAll();
                this.ValueToolTip.SetToolTip(this.AssessedLabel, this.AssessedLabel.Text.Trim());
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the AppellantLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AppellantLabel_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.ValueToolTip.RemoveAll();
                this.ValueToolTip.SetToolTip(this.AppellantLabel, this.AppellantLabel.Text.Trim());
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the StipulatedLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StipulatedLabel_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.ValueToolTip.RemoveAll();
                this.ValueToolTip.SetToolTip(this.StipulatedLabel, this.StipulatedLabel.Text.Trim());
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the BOELabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void BOELabel_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.ValueToolTip.RemoveAll();
                this.ValueToolTip.SetToolTip(this.BOELabel, this.BOELabel.Text.Trim());
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion ToolTip

        #endregion Events

        #region Private Methods

        #region Clear Controls
        /// <summary>
        /// Clears the controls.
        /// </summary>
        private void ClearControls()
        {
            ////Empty TextBox controls
            this.ProtestedByTextBox.Text = string.Empty;
            this.AddressOneTextBox.Text = string.Empty;
            this.AddresTwoTextBox.Text = string.Empty;
            this.CityTextBox.Text = string.Empty;
            this.StateTextBox.Text = string.Empty;
            this.ZipTextBox.Text = string.Empty;
            this.PhoneNumberTextBox.Text = string.Empty;
            this.ProtestNumberTextBox.Text = string.Empty;
            this.EmailAddressTextBox.Text = string.Empty;
            this.ParcelNumberLinkLabel.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.MapNumberTextBox.Text = string.Empty;
            this.PropertyOwnerLinkLabel.Text = string.Empty;
            this.PhoneNoTextBox.Text = string.Empty;
            this.SitusTextBox.Text = string.Empty;
            this.DORCodeTextBox.Text = string.Empty;
            this.LegalTextBox.Text = string.Empty;
            this.ZipTextBox.Text = string.Empty;
            this.HearingDateTextBox.Text = string.Empty;
            this.ActionDateTextBox.Text = string.Empty;
            this.ClosedDateTextBox.Text = string.Empty;
            ////Deselect Combo
            this.AppraiserCombo.SelectedIndex = 0;
        }
        #endregion Clear Controls

        #region Edit Mode
        /// <summary>
        /// Texts the value changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TextValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.flagFormLoad == false && this.pageMode == TerraScanCommon.PageModeTypes.View && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
                {
                    ////Make visible the save and cancel button
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    this.PushValueButton.Enabled = false;
                    this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion Edit Mode

        #region Enable Controls
        /// <summary>
        /// To Disable All the Controls
        /// </summary>
        /// <param name="lockControl">Boolean value</param>
        private void LockControls(bool lockControl)
        {
            this.ProtestedByPanel.Enabled = lockControl;
            this.AddressOnePanel.Enabled = lockControl;
            this.AddresTwoPanel.Enabled = lockControl;
            this.Citypanel.Enabled = lockControl;
            this.StatePanel.Enabled = lockControl;
            this.ZipPanel.Enabled = lockControl;
            this.PhoneNumberpanel.Enabled = lockControl;
            this.EmailAddressPanel.Enabled = lockControl;
            this.Appraiserpanel.Enabled = lockControl;
            this.ProtestNumberpanel.Enabled = lockControl;
            this.HearingDatepanel.Enabled = lockControl;
            this.ActionDatePanel.Enabled = lockControl;
            this.ClosedDatePanel.Enabled = lockControl;
            this.ParcelNumberPanel.Enabled = lockControl;
            this.PropertyOwnerPanel.Enabled = lockControl;

            this.RollYearPanel.Enabled = lockControl;
            this.MapNumberPanel.Enabled = lockControl;
            this.PhoneNoPanel.Enabled = lockControl;
            this.SitusPanel.Enabled = lockControl;
            this.DORCodePanel.Enabled = lockControl;
            this.LegalPanel.Enabled = lockControl;

            this.PushValuePanel.Enabled = lockControl;
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLock">Boolean value</param>
        private void ControlLock(bool controlLock)
        {
            this.ProtestedByTextBox.LockKeyPress = controlLock;
            this.AddressOneTextBox.LockKeyPress = controlLock;
            this.AddresTwoTextBox.LockKeyPress = controlLock;
            this.CityTextBox.LockKeyPress = controlLock;
            this.StateTextBox.LockKeyPress = controlLock;
            this.ZipTextBox.LockKeyPress = controlLock;
            this.PhoneNumberTextBox.LockKeyPress = controlLock;
            this.EmailAddressTextBox.LockKeyPress = controlLock;
            this.ProtestNumberTextBox.LockKeyPress = controlLock;
            this.HearingDateTextBox.LockKeyPress = controlLock;
            this.ActionDateTextBox.LockKeyPress = controlLock;
            this.ClosedDateTextBox.LockKeyPress = controlLock;
            this.ParcelNumberLinkLabel.Enabled = !controlLock;
            this.PropertyOwnerLinkLabel.Enabled = !controlLock;
            ////this.GridPanel.Enabled = controlLock;
            ////this.EqualizationGridView.Enabled = controlLock;
            this.AppraiserCombo.Enabled = !controlLock;
            this.FillingDatebutton.Enabled = !controlLock;
            this.ActionDateButton.Enabled = !controlLock;
            this.ClosedDateButton.Enabled = !controlLock;
            this.PushValueButton.Enabled = !controlLock;
        }

        #endregion Enable Controls

        #region CustomizeGridView

        /// <summary>
        /// Customizes the grid view.
        /// </summary>
        private void CustomizeGridView()
        {
            this.EqualizationGridView.AutoGenerateColumns = false;
            this.EqualizationGridView.AllowSorting = false;

            DataGridViewColumnCollection columns = this.EqualizationGridView.Columns;
            columns[this.boardOfEqualizationData.GetBOEValues.ValueBreakdownColumn.ColumnName].DataPropertyName = this.boardOfEqualizationData.GetBOEValues.ValueBreakdownColumn.ColumnName;
            columns[this.boardOfEqualizationData.GetBOEValues.AssessedValueColumn.ColumnName].DataPropertyName = this.boardOfEqualizationData.GetBOEValues.AssessedValueColumn.ColumnName;
            columns[this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName].DataPropertyName = this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName;
            columns[this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName].DataPropertyName = this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName;
            columns[this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName].DataPropertyName = this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName;
            columns[this.boardOfEqualizationData.GetBOEValues.IsRollColumn.ColumnName].DataPropertyName = this.boardOfEqualizationData.GetBOEValues.IsRollColumn.ColumnName;

            columns[this.boardOfEqualizationData.GetBOEValues.ValueBreakdownColumn.ColumnName].DisplayIndex = 0;
            columns[this.boardOfEqualizationData.GetBOEValues.AssessedValueColumn.ColumnName].DisplayIndex = 1;
            columns[this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName].DisplayIndex = 2;
            columns[this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName].DisplayIndex = 3;
            columns[this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName].DisplayIndex = 4;
            columns[this.boardOfEqualizationData.GetBOEValues.IsRollColumn.ColumnName].DisplayIndex = 5;
        }

        #endregion CustomizeGridView

        #region Bind Controls

        /// <summary>
        /// Binds the control values.
        /// </summary>
        private void BindControlValues()
        {
            if (this.boardOfEqualizationData.GetBOEDetails.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.boardOfEqualizationData.GetBOEDetails.Rows[0][this.boardOfEqualizationData.GetBOEDetails.BOEIDColumn.ColumnName].ToString()))
                {
                    this.boeId = Convert.ToInt32(this.boardOfEqualizationData.GetBOEDetails.Rows[0][this.boardOfEqualizationData.GetBOEDetails.BOEIDColumn.ColumnName].ToString());
                }

                this.ProtestedByTextBox.Text = this.boardOfEqualizationData.GetBOEDetails.Rows[0][this.boardOfEqualizationData.GetBOEDetails.ProtestedByColumn.ColumnName].ToString();
                this.AddressOneTextBox.Text = this.boardOfEqualizationData.GetBOEDetails.Rows[0][this.boardOfEqualizationData.GetBOEDetails.Address1Column.ColumnName].ToString();
                this.AddresTwoTextBox.Text = this.boardOfEqualizationData.GetBOEDetails.Rows[0][this.boardOfEqualizationData.GetBOEDetails.Address2Column.ColumnName].ToString();
                this.CityTextBox.Text = this.boardOfEqualizationData.GetBOEDetails.Rows[0][this.boardOfEqualizationData.GetBOEDetails.CityColumn.ColumnName].ToString();
                this.StateTextBox.Text = this.boardOfEqualizationData.GetBOEDetails.Rows[0][this.boardOfEqualizationData.GetBOEDetails.StateColumn.ColumnName].ToString();
                this.ZipTextBox.Text = this.boardOfEqualizationData.GetBOEDetails.Rows[0][this.boardOfEqualizationData.GetBOEDetails.ZipColumn.ColumnName].ToString();
                this.PhoneNumberTextBox.Text = this.boardOfEqualizationData.GetBOEDetails.Rows[0][this.boardOfEqualizationData.GetBOEDetails.PhoneNumberColumn.ColumnName].ToString();
                this.EmailAddressTextBox.Text = this.boardOfEqualizationData.GetBOEDetails.Rows[0][this.boardOfEqualizationData.GetBOEDetails.EmailAddressColumn.ColumnName].ToString();
                this.ProtestNumberTextBox.Text = this.boardOfEqualizationData.GetBOEDetails.Rows[0][this.boardOfEqualizationData.GetBOEDetails.ProtestNumberColumn.ColumnName].ToString();
                this.HearingDateTextBox.Text = this.boardOfEqualizationData.GetBOEDetails.Rows[0][this.boardOfEqualizationData.GetBOEDetails.HearingDateColumn.ColumnName].ToString();
                this.ActionDateTextBox.Text = this.boardOfEqualizationData.GetBOEDetails.Rows[0][this.boardOfEqualizationData.GetBOEDetails.ActionDateColumn.ColumnName].ToString();
                this.ClosedDateTextBox.Text = this.boardOfEqualizationData.GetBOEDetails.Rows[0][this.boardOfEqualizationData.GetBOEDetails.ClosedDateColumn.ColumnName].ToString();
                this.AppraiserCombo.SelectedValue = this.boardOfEqualizationData.GetBOEDetails.Rows[0][this.boardOfEqualizationData.GetBOEDetails.UserIDColumn.ColumnName].ToString();
                //// this.PhoneNoTextBox.Text = this.boardOfEqualizationData.GetBOEDetails.Rows[0][this.boardOfEqualizationData.GetBOEDetails.PhoneNumberColumn.ColumnName].ToString();

                if (this.boardOfEqualizationData.GetBOEParcelDetails.Rows.Count > 0)
                {
                    this.ParcelNumberLinkLabel.Text = this.boardOfEqualizationData.GetBOEParcelDetails.Rows[0][this.boardOfEqualizationData.GetBOEParcelDetails.ParcelNumberColumn.ColumnName].ToString();
                    this.RollYearTextBox.Text = this.boardOfEqualizationData.GetBOEParcelDetails.Rows[0][this.boardOfEqualizationData.GetBOEParcelDetails.RollYearColumn.ColumnName].ToString();
                    this.MapNumberTextBox.Text = this.boardOfEqualizationData.GetBOEParcelDetails.Rows[0][this.boardOfEqualizationData.GetBOEParcelDetails.MID2Column.ColumnName].ToString();
                    this.PropertyOwnerLinkLabel.Text = this.boardOfEqualizationData.GetBOEParcelDetails.Rows[0][this.boardOfEqualizationData.GetBOEParcelDetails.PrimaryOwnerColumn.ColumnName].ToString();
                    this.PhoneNoTextBox.Text = this.boardOfEqualizationData.GetBOEParcelDetails.Rows[0][this.boardOfEqualizationData.GetBOEParcelDetails.PhoneNumberColumn.ColumnName].ToString();
                    this.SitusTextBox.Text = this.boardOfEqualizationData.GetBOEParcelDetails.Rows[0][this.boardOfEqualizationData.GetBOEParcelDetails.SitusColumn.ColumnName].ToString();
                    this.DORCodeTextBox.Text = this.boardOfEqualizationData.GetBOEParcelDetails.Rows[0][this.boardOfEqualizationData.GetBOEParcelDetails.DORColumn.ColumnName].ToString();
                    this.LegalTextBox.Text = this.boardOfEqualizationData.GetBOEParcelDetails.Rows[0][this.boardOfEqualizationData.GetBOEParcelDetails.LegalColumn.ColumnName].ToString();
                }
                else
                {
                    this.ParcelNumberLinkLabel.Text = string.Empty;
                    this.RollYearTextBox.Text = string.Empty;
                    this.MapNumberTextBox.Text = string.Empty;
                    this.PropertyOwnerLinkLabel.Text = string.Empty;
                    ////this.PhoneNoTextBox.Text = string.Empty;
                    this.SitusTextBox.Text = string.Empty;
                    this.DORCodeTextBox.Text = string.Empty;
                    this.LegalTextBox.Text = string.Empty;
                }
                this.LockControls(true);
            }
            else
            {
                this.ClearControls();
                this.LockControls(false);
            }

            this.EqualizationGridView.DataSource = this.boardOfEqualizationData.GetBOEValues.DefaultView;
        }

        #endregion Bind Controls

        #region Load AppraiserCombo

        /// <summary>
        /// Loads the appraiser combo.
        /// </summary>
        private void LoadAppraiserCombo()
        {
            this.userManagementData = this.form29630Control.WorkItem.F9002_GetUserDetails(TerraScanCommon.ApplicationId);
            this.AppraiserCombo.DataSource = this.userManagementData;
            this.AppraiserCombo.DataSource = this.userManagementData.ListUserDetail;
            this.AppraiserCombo.DisplayMember = this.userManagementData.ListUserDetail.DisplayNameColumn.ColumnName;
            this.AppraiserCombo.ValueMember = this.userManagementData.ListUserDetail.UserIDColumn.ColumnName;
        }

        #endregion Load AppraiserCombo

        #region Get BoardOfEqualizationDetails

        /// <summary>
        /// Gets the board of equalization details.
        /// </summary>
        private void GetBoardOfEqualizationDetails()
        {
            this.boardOfEqualizationData = this.form29630Control.WorkItem.F29630GetBoardOfEqualizationDetails(this.keyId);
            this.BindControlValues();
        }

        #endregion GetBoardOfEqualizationDetails

        #region Grid Methods

        /// <summary>
        /// Handles the Validated event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_Validated(object sender, EventArgs e)
        {
            try
            {
                this.EqualizationGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
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
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.PushValueButton.Enabled = false;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Grid Methods

        #region Validation

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>SliceValidationFields</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();

            if (string.IsNullOrEmpty(this.ProtestedByTextBox.Text.Trim()))
            {
                sliceValidationFields.ErrorMessage = (SharedFunctions.GetResourceString("RequiredField"));
                sliceValidationFields.FormNo = formNo;
                sliceValidationFields.RequiredFieldMissing = false;
            }
            else
            {
                sliceValidationFields.ErrorMessage = string.Empty;
                sliceValidationFields.FormNo = formNo;
                sliceValidationFields.RequiredFieldMissing = false;
            }

            return sliceValidationFields;
        }

        #endregion Validation

        #region Save

        /// <summary>
        /// Saves the equalization.
        /// </summary>
        private void SaveEqualization()
        {
            this.boardOfEqualizationData.SaveBOEDetails.Rows.Clear();
            F29630BoardOfEqualizationData.SaveBOEDetailsRow dr = this.boardOfEqualizationData.SaveBOEDetails.NewSaveBOEDetailsRow();

            dr.EventID = this.keyId;
            dr.BOEID = this.boeId;

            if (!string.IsNullOrEmpty(this.ProtestedByTextBox.Text.Trim()))
            {
                dr.ProtestedBy = this.ProtestedByTextBox.Text.ToString().Trim();
            }

            if (!string.IsNullOrEmpty(this.AddressOneTextBox.Text.Trim()))
            {
                dr.Address1 = this.AddressOneTextBox.Text.ToString().Trim();
            }

            if (!string.IsNullOrEmpty(this.AddresTwoTextBox.Text.Trim()))
            {
                dr.Address2 = this.AddresTwoTextBox.Text.ToString().Trim();
            }

            if (!string.IsNullOrEmpty(this.CityTextBox.Text.Trim()))
            {
                dr.City = this.CityTextBox.Text.ToString().Trim();
            }

            if (!string.IsNullOrEmpty(this.StateTextBox.Text.Trim()))
            {
                dr.State = this.StateTextBox.Text.ToString().Trim();
            }

            if (!string.IsNullOrEmpty(this.ZipTextBox.Text.Trim()))
            {
                dr.Zip = this.ZipTextBox.Text.ToString().Trim();
            }

            if (!string.IsNullOrEmpty(this.PhoneNumberTextBox.Text.Trim()))
            {
                dr.PhoneNumber = this.PhoneNumberTextBox.Text.ToString().Trim();
            }

            if (!string.IsNullOrEmpty(this.EmailAddressTextBox.Text.Trim()))
            {
                dr.EmailAddress = this.EmailAddressTextBox.Text.ToString().Trim();
            }

            if (this.AppraiserCombo.SelectedIndex > -1)
            {
                dr.UserID = Convert.ToInt32(this.AppraiserCombo.SelectedValue);
            }

            if (!string.IsNullOrEmpty(this.ProtestNumberTextBox.Text.Trim()))
            {
                dr.ProtestNumber = this.ProtestNumberTextBox.Text.ToString().Trim();
            }

            if (!string.IsNullOrEmpty(this.HearingDateTextBox.Text.Trim()))
            {
                dr.HearingDate = this.HearingDateTextBox.Text.ToString().Trim();
            }

            if (!string.IsNullOrEmpty(this.ActionDateTextBox.Text.Trim()))
            {
                dr.ActionDate = this.ActionDateTextBox.Text.ToString().Trim();
            }

            if (!string.IsNullOrEmpty(this.ClosedDateTextBox.Text.Trim()))
            {
                dr.ClosedDate = this.ClosedDateTextBox.Text.ToString().Trim();
            }

            this.boardOfEqualizationData.SaveBOEDetails.Rows.Add(dr);

            string boeElements = TerraScanCommon.GetXmlString(this.boardOfEqualizationData.SaveBOEDetails);
            string boeValues = TerraScanCommon.GetXmlString(this.boardOfEqualizationData.GetBOEValues);

            // UserId has been added as a parameter for #8272 CO
            this.form29630Control.WorkItem.F29630SaveBoardOfEqualizationDetails(boeElements, boeValues, TerraScanCommon.UserId);
        }

        #endregion Save

        #region CalculatedValue

        /// <summary>
        /// Calculateds the value.
        /// </summary>
        private void CalculatedValue()
        {
            try
            {
                ////Decimal.TryParse(this.boardOfEqualizationData.GetBOEValues.Compute("SUM(" + this.EqualizationGridView.Columns[this.AssessedValue.Name] + ")", string.Empty).ToString(), out assessedSumValue);
                ////this.AssessedLabel.Text = this.assessedSumValue.ToString();

                ////Decimal.TryParse(this.boardOfEqualizationData.GetBOEValues.Compute("SUM(" + this.EqualizationGridView.Columns[this.ApellantsValue.Name] + ")", string.Empty).ToString(), out apellantsSumValue);
                ////this.AppellantLabel.Text = this.apellantsSumValue.ToString();

                ////Decimal.TryParse(this.boardOfEqualizationData.GetBOEValues.Compute("SUM(" + this.EqualizationGridView.Columns[this.StipulatedValue.Name] + ")", string.Empty).ToString(), out stipulatedSumValue);
                ////this.StipulatedLabel.Text = this.stipulatedSumValue.ToString();

                ////Decimal.TryParse(this.boardOfEqualizationData.GetBOEValues.Compute("SUM(" + this.EqualizationGridView.Columns[this.BOEValue.Name] + ")", string.Empty).ToString(), out boeSumValue);
                ////this.BOELabel.Text = this.boeSumValue.ToString();

                this.assessedSumValue = 0;
                this.apellantsSumValue = 0;
                this.stipulatedSumValue = 0;
                this.boeSumValue = 0;

                for (int i = 0; i < this.boardOfEqualizationData.GetBOEValues.Rows.Count; i++)
                {
                    //if (this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.ValueBreakdownColumn.ColumnName].ToString() != "Market Land Value")
                    //{
                    if (!string.IsNullOrEmpty(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.AssessedValueColumn.ColumnName].ToString()))
                    {
                        if (i == 0)
                        {
                            Double tempDecimal;
                            Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[0][this.boardOfEqualizationData.GetBOEValues.AssessedValueColumn.ColumnName].ToString().Replace("$", ""), out tempDecimal);
                            Double outDecimal = 0;
                            Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.AssessedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                            this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.AssessedValueColumn.ColumnName] = outDecimal;
                            this.assessedSumValue = this.assessedSumValue + Convert.ToDouble(outDecimal);
                            if (tempDecimal < 0 || tempDecimal > 0)
                            {
                                i = 1;
                            }
                        }
                        else
                        {
                            Double outDecimal = 0;
                            Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.AssessedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                            this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.AssessedValueColumn.ColumnName] = outDecimal;
                            this.assessedSumValue = this.assessedSumValue + Convert.ToDouble(outDecimal);
                        }
                    }
                }

                    
                    
                for (int i = 0; i < this.boardOfEqualizationData.GetBOEValues.Rows.Count; i++)
                {

                    if (!string.IsNullOrEmpty(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName].ToString()))
                    {
                     
                        if (i == 0)
                        {
                            Double tempDecimal;
                            Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[0][this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName].ToString().Replace("$", ""), out tempDecimal);
                            Double outDecimal = 0;
                            Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                            this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName] = outDecimal;
                            this.apellantsSumValue = this.apellantsSumValue + Convert.ToDouble(outDecimal);
                            if (tempDecimal < 0 || tempDecimal > 0)
                            {
                                i = 1;
                            }
                        }
                        else
                        {
                            Double outDecimal = 0;
                            Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                            this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.ApellantsValueColumn.ColumnName] = outDecimal;
                            this.apellantsSumValue = this.apellantsSumValue + Convert.ToDouble(outDecimal);
                        }
                    
                        
                    
                      }
                }
                for (int i = 0; i < this.boardOfEqualizationData.GetBOEValues.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName].ToString()))
                    {
                        if (i == 0)
                        {
                            Double tempDecimal;
                            Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[0][this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName].ToString().Replace("$", ""), out tempDecimal);
                            Double outDecimal = 0;
                            Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                            this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName] = outDecimal;
                            this.stipulatedSumValue = this.stipulatedSumValue + Convert.ToDouble(outDecimal);
                            if (tempDecimal < 0 || tempDecimal > 0)
                            {
                                i = 1;
                            }
                        }
                        else
                        {
                            Double outDecimal = 0;
                            Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                            this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.StipulatedValueColumn.ColumnName] = outDecimal;
                            this.stipulatedSumValue = this.stipulatedSumValue + Convert.ToDouble(outDecimal);
                        }
                        
                    }
                }
                for (int i = 0; i < this.boardOfEqualizationData.GetBOEValues.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName].ToString()))
                    {
                        if (i == 0)
                        {
                            Double tempDecimal;
                            Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[0][this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName].ToString().Replace("$", ""), out tempDecimal);
                            Double outDecimal = 0;
                            Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                            this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName] = outDecimal;
                            this.boeSumValue = this.boeSumValue + Convert.ToDouble(outDecimal);
                            if (tempDecimal < 0 || tempDecimal > 0)
                            {
                                i = 1;
                            }
                        }
                        else
                        {
                            Double outDecimal = 0;
                            Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                            this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.BOEValueColumn.ColumnName] = outDecimal;
                            this.boeSumValue = this.boeSumValue + Convert.ToDouble(outDecimal);
                        }

                     }
                }
                

                this.AssessedLabel.Text = "$" + this.assessedSumValue.ToString("#,##0.00");
                this.AppellantLabel.Text = "$" + this.apellantsSumValue.ToString("#,##0.00");
                this.StipulatedLabel.Text = "$" + this.stipulatedSumValue.ToString("#,##0.00");
                this.BOELabel.Text = "$" + this.boeSumValue.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #endregion Private Methods
    }
}
