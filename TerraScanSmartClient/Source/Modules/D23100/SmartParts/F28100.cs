//--------------------------------------------------------------------------------------------
// <copyright file="F28100.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F28100 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 07/05/2011        D.LathaMaheswari  Created
//***********************************************************************************************/

namespace D23100
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
    using System.Globalization;
   
    /// <summary>
    /// 28100
    /// </summary>
    [SmartPart]
    public partial class F28100 : BaseSmartPart
    {
        #region Variable

        /// <summary>
        /// Form Number of the masterForm 
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Instance of 28100 Controller to call the WorkItem
        /// </summary>
        private F28100Controller form28100Control;

        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyId;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// The Page Mode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Flag for Form Load
        /// </summary>
        private bool flagFormLoad;

        /// <summary>
        /// DataSet for Discretionary details
        /// </summary>
        private F28100BOEData boeData = new F28100BOEData();

        /// <summary>
        /// User DataSet
        /// </summary>
        private UserManagementData userManagementData = new UserManagementData();

        /// <summary>
        /// To store current Discretionary ID
        /// </summary>
        private int boeId = 0;

        /// <summary>
        /// Set Date Format
        /// </summary>
        private string dateFormat = "M/d/yyyy";

        #endregion Variables

        #region Constructor

        public F28100()
        {
            InitializeComponent();
        }

        /// <summary>
        /// F28100 constructor
        /// </summary>
        /// <param name="masterform">Master Form Number</param>
        /// <param name="formNo">Form number</param>
        /// <param name="keyID">the Key Id</param>
        /// <param name="red">The Red Color</param>
        /// <param name="green">The Green Color</param>
        /// <param name="blue">the Blue Color</param>
        /// <param name="tabText">Tab texr</param>
        /// <param name="permissionEdit">Edit permission</param>
        public F28100(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.Tag = formNo;
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
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the form28100 controller.
        /// </summary>
        /// <value>The form28100 controller.</value>
        [CreateNew]
        public F28100Controller Form28100Controller
        {
            get { return this.form28100Control as F28100Controller; }
            set { this.form28100Control = value; }
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

                if (this.boeData.ValidRecord.Rows.Count > 0)
                {
                    int invalidRecord = 0;
                    int.TryParse(this.boeData.ValidRecord.Rows[0][this.boeData.ValidRecord.InValidColumn.ColumnName].ToString(), out invalidRecord);
                    if (invalidRecord > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
                        this.GridPanel.Enabled = this.permissionFields.editPermission;
                    }
                    else
                    {
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            eventArgs.Data.FlagInvalidSliceKey = true;
                        }
                        this.GridPanel.Enabled = false;
                    }
                }
                else
                {
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
           // this.CalculatedValue();
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

        ///// <summary>
        ///// Called when [D9030_ F9030_ delete slice information].
        ///// </summary>
        ///// <param name="sender">The sender.</param>
        ///// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        //[EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        //public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        //{
        //    if (this.slicePermissionField.deletePermission)
        //    {
        //        this.Cursor = Cursors.WaitCursor;
        //        this.form28100Control.WorkItem.F28100_DeleteBOEDetails(this.boeId, TerraScanCommon.UserId);
        //        SliceFormCloseAlert sliceFormCloseAlert;
        //        sliceFormCloseAlert.FormNo = this.masterFormNo;
        //        sliceFormCloseAlert.FlagFormClose = false;
        //        this.OnFormSlice_FormCloseAlert(new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
        //        this.Cursor = Cursors.Default;
        //    }
        //}

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
                //this.CalculatedValue();
                this.ProtestedByTextBox.Focus();
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

        #region Events

        #region Form Load
        
        private void F28100_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagFormLoad = true;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.PushValueButton.Enabled = true;
                
                this.CustomizeGridView();
                this.LoadAppraiserCombo();
                this.GetBoardOfEqualizationDetails();
                this.GridPanel.Enabled = this.permissionFields.editPermission;
                //this.CalculatedValue();
 
                if (this.boeData.BOEValues.Rows.Count > 0)
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
                this.HearingDateTextBox.Focus();
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
                this.ActionDateTextBox.Focus();
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
                this.ClosedDateTextBox.Focus();
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
        private void HearingDatebutton_Click(object sender, EventArgs e)
        {
            try
            {
                this.HearingDateCalender.Visible = true;
                this.HearingDateCalender.ScrollChange = 1;

                // Display the Calender control near the Calender Picture box.
                this.HearingDateCalender.Left = this.HearingDatepanel.Left + this.HearingDatebutton.Left + this.HearingDatebutton.Width;
                this.HearingDateCalender.Top = this.HearingDatepanel.Top + this.HearingDatebutton.Top;
                this.HearingDateCalender.Tag = this.HearingDatebutton.Tag;
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
                this.ClosedDateCalender.Left = 523;
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
                if (!string.IsNullOrEmpty(this.boeData.BOEValues.Rows[0][this.boeData.BOEValues.ParcelIDColumn.ColumnName].ToString()))
                {
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(30000);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = this.boeData.BOEValues.Rows[0][this.boeData.BOEValues.ParcelIDColumn.ColumnName].ToString();
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
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
                if (!string.IsNullOrEmpty(this.boeData.BOEValues.Rows[0][this.boeData.BOEValues.OwnerIDColumn.ColumnName].ToString()))
                {
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(91000);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = this.boeData.BOEValues.Rows[0][this.boeData.BOEValues.OwnerIDColumn.ColumnName].ToString();
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion Parcel Link

        #region Grid Events

        /// <summary>
        /// Handles the CellFormatting event of the EqualizationGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void EqualizationGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Double outValue;
            if (e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.AssessedValue.Name].Index)
                || e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.LocalValue.Name].Index)
                || e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.CountyValue.Name].Index)
                || e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.StateValue.Name].Index))
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
                        e.Value = "$ " + outValue.ToString("#,##0");
                        e.FormattingApplied = true;
                    }
                    else
                    {
                        e.Value = "$ 0";
                    }
                }
                else
                {
                    e.Value = "$ 0";
                }
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
                if (e.Control is DataGridViewTextBoxEditingControl)
                {
                    e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                    e.Control.Validated += new EventHandler(this.Control_Validated);
                }

                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                    ((ComboBox)e.Control).AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                    ((ComboBox)e.Control).TextUpdate -= new EventHandler(this.Control_TextChanged);
                    ((ComboBox)e.Control).TextUpdate += new EventHandler(this.Control_TextChanged);
                    ((ComboBox)e.Control).SelectionChangeCommitted -= new EventHandler(this.Control_TextChanged);
                    ((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(this.Control_TextChanged);
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

        #endregion Events

        #region private Methods

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
            this.PropertyOwnerLinkLabel.Text = string.Empty;
            this.PhoneNoTextBox.Text = string.Empty;
            this.SitusTextBox.Text = string.Empty;
            this.LegalTextBox.Text = string.Empty;
            this.ZipTextBox.Text = string.Empty;
            this.DistrictLinkLabel.Text = string.Empty;
            this.HearingDateTextBox.Text = DateTime.Today.ToShortDateString();
            this.ActionDateTextBox.Text = DateTime.Today.ToShortDateString();
            this.ClosedDateTextBox.Text = DateTime.Today.ToShortDateString();
            
            ////Deselect Combo
            this.AppraiserCombo.SelectedIndex = 0;

            this.AglandExemptionTextBox.Text = string.Empty;
            this.NonAglandExemptionTextBox.Text = string.Empty;
            this.ImprExemptionTextBox.Text = string.Empty;
            this.NonImprExemptionTextBox.Text = string.Empty;
            this.ResidentExemptionTextBox.Text = string.Empty;
            this.AssessedTotalTextBox.Text = string.Empty;
            this.LocalTotalTextBox.Text = string.Empty;
            this.CountyTotalTextBox.Text = string.Empty;
            this.StateTotalTextBox.Text = string.Empty;
            this.FinalTotalTextBox.Text = string.Empty;

            this.LocalCheckBox.Checked = false;
            this.CountyCheckBox.Checked = false;
            this.StateCheckBox.Checked = false;
        }

        private void ClearTotalControls()
        {
            this.AssessedTotalTextBox.Text = string.Empty;
            this.LocalTotalTextBox.Text = string.Empty;
            this.CountyTotalTextBox.Text = string.Empty;
            this.StateTotalTextBox.Text = string.Empty;
            this.FinalTotalTextBox.Text = string.Empty;
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
            this.PhoneNoPanel.Enabled = lockControl;
            this.SitusPanel.Enabled = lockControl;
            this.DORCodePanel.Enabled = lockControl;
            this.LegalPanel.Enabled = lockControl;

            this.AgExemptionPanel.Enabled = lockControl;
            this.NonAgExemptionPanel.Enabled = lockControl;
            this.AgImprPanel.Enabled = lockControl;
            this.NonImprExemptionPanel.Enabled = lockControl;
            this.ResidentExemptionPanel.Enabled = lockControl;
            this.AssessedTotalPanel.Enabled = lockControl;
            this.LocalTotalPanel.Enabled = lockControl;
            this.CountyPanel.Enabled = lockControl;
            this.StateTotalPanel.Enabled = lockControl;
            this.FinalTotalPanel.Enabled = lockControl;

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
            this.HearingDatebutton.Enabled = !controlLock;
            this.ActionDateButton.Enabled = !controlLock;
            this.ClosedDateButton.Enabled = !controlLock;
            this.PushValueButton.Enabled = !controlLock;
        }

        #endregion Enable Controls

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

        /// <summary>
        /// Load Grid cls combos
        /// </summary>
        private void LoadClsCombos()
        {
            this.LoadComboList(this.AssessedCls);
            this.LoadComboList(this.LocalCls);
            this.LoadComboList(this.CountyCls);
            this.LoadComboList(this.StateCls);
        }

        /// <summary>
        /// Load Combo list
        /// </summary>
        /// <param name="comboColumn">Combobox column</param>
        private void LoadComboList(DataGridViewComboBoxColumn comboColumn)
        {
            (comboColumn as DataGridViewComboBoxColumn).DataSource = this.boeData.ClassTypes.Copy();
            (comboColumn as DataGridViewComboBoxColumn).DisplayMember = this.boeData.ClassTypes.ClassColumn.ColumnName;
            (comboColumn as DataGridViewComboBoxColumn).ValueMember = this.boeData.ClassTypes.ClassIDColumn.ColumnName;
        }

        #endregion Grid Methods

        #region Load AppraiserCombo

        /// <summary>
        /// Loads the appraiser combo.
        /// </summary>
        private void LoadAppraiserCombo()
        {
            this.userManagementData = this.form28100Control.WorkItem.F9002_GetUserDetails(TerraScanCommon.ApplicationId);
            this.AppraiserCombo.DataSource = this.userManagementData;
            this.AppraiserCombo.DataSource = this.userManagementData.ListUserDetail;
            this.AppraiserCombo.DisplayMember = this.userManagementData.ListUserDetail.DisplayNameColumn.ColumnName;
            this.AppraiserCombo.ValueMember = this.userManagementData.ListUserDetail.UserIDColumn.ColumnName;
        }

        #endregion Load AppraiserCombo

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

        #region Get BoardOfEqualization Details

        /// <summary>
        /// Gets the board of equalization details.
        /// </summary>
        private void GetBoardOfEqualizationDetails()
        {
            this.boeData = this.form28100Control.WorkItem.F28100_GetBOEDetails(this.keyId);
            this.BindControlValues();
        }

        #endregion Get BoardOfEqualization Details

        #region Customize GridView

        /// <summary>
        /// Customizes the grid view.
        /// </summary>
        private void CustomizeGridView()
        {
            this.EqualizationGridView.AutoGenerateColumns = false;
            this.EqualizationGridView.AllowSorting = false;

            // Assign Data Property for each column
            DataGridViewColumnCollection columns = this.EqualizationGridView.Columns;
            columns[this.boeData.AssessedGridValues.ValueBreakdownColumn.ColumnName].DataPropertyName = this.boeData.AssessedGridValues.ValueBreakdownColumn.ColumnName;
            columns[this.boeData.AssessedGridValues.AssessedClsColumn.ColumnName].DataPropertyName = this.boeData.AssessedGridValues.AssessedClsColumn.ColumnName;
            columns[this.boeData.AssessedGridValues.AssessedValueColumn.ColumnName].DataPropertyName = this.boeData.AssessedGridValues.AssessedValueColumn.ColumnName;
            columns[this.boeData.AssessedGridValues.LocalClsColumn.ColumnName].DataPropertyName = this.boeData.AssessedGridValues.LocalClsColumn.ColumnName;
            columns[this.boeData.AssessedGridValues.LocalValueColumn.ColumnName].DataPropertyName = this.boeData.AssessedGridValues.LocalValueColumn.ColumnName;
            columns[this.boeData.AssessedGridValues.CountyClsColumn.ColumnName].DataPropertyName = this.boeData.AssessedGridValues.CountyClsColumn.ColumnName;
            columns[this.boeData.AssessedGridValues.CountyValueColumn.ColumnName].DataPropertyName = this.boeData.AssessedGridValues.CountyValueColumn.ColumnName;
            columns[this.boeData.AssessedGridValues.StateClsColumn.ColumnName].DataPropertyName = this.boeData.AssessedGridValues.StateClsColumn.ColumnName;
            columns[this.boeData.AssessedGridValues.StateValueColumn.ColumnName].DataPropertyName = this.boeData.AssessedGridValues.StateValueColumn.ColumnName;
            columns[this.boeData.AssessedGridValues.IsRollColumn.ColumnName].DataPropertyName = this.boeData.AssessedGridValues.IsRollColumn.ColumnName;

            // Assign display index for each column
            columns[this.boeData.AssessedGridValues.ValueBreakdownColumn.ColumnName].DisplayIndex = 0;
            columns[this.boeData.AssessedGridValues.AssessedClsColumn.ColumnName].DisplayIndex = 1;
            columns[this.boeData.AssessedGridValues.AssessedValueColumn.ColumnName].DisplayIndex = 2;
            columns[this.boeData.AssessedGridValues.LocalClsColumn.ColumnName].DisplayIndex = 3;
            columns[this.boeData.AssessedGridValues.LocalValueColumn.ColumnName].DisplayIndex = 4;
            columns[this.boeData.AssessedGridValues.CountyClsColumn.ColumnName].DisplayIndex = 5;
            columns[this.boeData.AssessedGridValues.CountyValueColumn.ColumnName].DisplayIndex = 6;
            columns[this.boeData.AssessedGridValues.StateClsColumn.ColumnName].DisplayIndex = 7;
            columns[this.boeData.AssessedGridValues.StateValueColumn.ColumnName].DisplayIndex = 8;
            columns[this.boeData.AssessedGridValues.IsRollColumn.ColumnName].DisplayIndex = 9;
        }

        #endregion Customize GridView

        #region Bind Controls

        /// <summary>
        /// Binds the control values.
        /// </summary>
        private void BindControlValues()
        {
            if (this.boeData.BOEValues.Rows.Count > 0)
            {
                F28100BOEData.BOEValuesRow boeValues = (F28100BOEData.BOEValuesRow)this.boeData.BOEValues.Rows[0];
                if (!boeValues.IsBoeIdNull() && !string.IsNullOrEmpty(boeValues.BoeId.ToString()))
                {
                    this.boeId = Convert.ToInt32(boeValues.BoeId.ToString());
                }
                else
                {
                    this.boeId = 0;
                }

                if (!boeValues.IsProtestedByNull())
                {
                    this.ProtestedByTextBox.Text = boeValues.ProtestedBy.ToString();
                }
                else
                {
                    this.ProtestedByTextBox.Text = string.Empty;
                }

                if (!boeValues.IsAddress1Null())
                {
                    this.AddressOneTextBox.Text = boeValues.Address1.ToString();
                }
                else
                {
                    this.AddressOneTextBox.Text = string.Empty;
                }

                if (!boeValues.IsAddress2Null())
                {
                    this.AddresTwoTextBox.Text = boeValues.Address2.ToString();
                }
                else
                {
                    this.AddresTwoTextBox.Text = string.Empty;
                }

                if (!boeValues.IsCityNull())
                {
                    this.CityTextBox.Text = boeValues.City.ToString();
                }
                else
                {
                    this.CityTextBox.Text = string.Empty;
                }

                if (!boeValues.IsStateNull())
                {
                    this.StateTextBox.Text = boeValues.State.ToString();
                }
                else
                {
                    this.StateTextBox.Text = string.Empty;
                }

                if (!boeValues.IsZipNull())
                {
                    this.ZipTextBox.Text = boeValues.Zip.ToString();
                }
                else
                {
                    this.ZipTextBox.Text = string.Empty;
                }

                if (!boeValues.IsPhoneNumberNull())
                {
                    this.PhoneNumberTextBox.Text = boeValues.PhoneNumber.ToString();
                }
                else
                {
                    this.PhoneNumberTextBox.Text = string.Empty;
                }

                if (!boeValues.IsEmailAddressNull())
                {
                    this.EmailAddressTextBox.Text = boeValues.EmailAddress.ToString();
                }
                else
                {
                    this.EmailAddressTextBox.Text = string.Empty;
                }

                if (!boeValues.IsProtestedByNull())
                {
                    this.ProtestNumberTextBox.Text = boeValues.ProtestNumber.ToString();
                }
                else
                {
                    this.ProtestNumberTextBox.Text = string.Empty;
                }

                if (!boeValues.IsHearingDateNull() && !string.IsNullOrEmpty(boeValues.HearingDate.ToString()))
                {
                    this.HearingDateTextBox.Text = boeValues.HearingDate.ToString();
                }
                else
                {
                    this.HearingDateTextBox.Text = DateTime.Today.ToShortDateString();
                }

                if (!boeValues.IsActionDateNull() && !string.IsNullOrEmpty(boeValues.ActionDate.ToString()))
                {
                    this.ActionDateTextBox.Text = boeValues.ActionDate.ToString();
                }
                else
                {
                    this.ActionDateTextBox.Text = DateTime.Today.ToShortDateString();
                }

                if (!boeValues.IsClosedDateNull() && !string.IsNullOrEmpty(boeValues.ClosedDate.ToString()))
                {
                    this.ClosedDateTextBox.Text = boeValues.ClosedDate.ToString();
                }
                else
                {
                    this.ClosedDateTextBox.Text = DateTime.Today.ToShortDateString();
                }

                if (!boeValues.IsUserIDNull())
                {
                    this.AppraiserCombo.SelectedValue = boeValues.UserID.ToString();
                }
                else
                {
                    this.AppraiserCombo.SelectedIndex = -1;
                }

                if (!boeValues.IsParcelNumberNull())
                {
                    this.ParcelNumberLinkLabel.Text = boeValues.ParcelNumber.ToString();
                }
                else
                {
                    this.ParcelNumberLinkLabel.Text = string.Empty;
                }

                if (!boeValues.IsRollYearNull())
                {
                    this.RollYearTextBox.Text = boeValues.RollYear.ToString();
                }
                else
                {
                    this.RollYearTextBox.Text = string.Empty;
                }

                if (!boeValues.IsOwnerNameNull())
                {
                    this.PropertyOwnerLinkLabel.Text = boeValues.OwnerName.ToString();
                }
                else
                {
                    this.PropertyOwnerLinkLabel.Text = string.Empty;
                }

                if (!boeValues.IsOwnerPhoneNumberNull())
                {
                    this.PhoneNoTextBox.Text = boeValues.OwnerPhoneNumber.ToString();
                }
                else
                {
                    this.PhoneNoTextBox.Text = string.Empty;
                }

                if (!boeValues.IsSitusNull())
                {
                    this.SitusTextBox.Text = boeValues.Situs.ToString();
                }
                else
                {
                    this.SitusTextBox.Text = string.Empty;
                }

                if (!boeValues.IsDistrictNull())
                {
                    this.DistrictLinkLabel.Text = boeValues.District.ToString();
                }
                else
                {
                    this.DistrictLinkLabel.Text = string.Empty;
                }

                if (!boeValues.IsUserLegalNull())
                {
                    this.LegalTextBox.Text = boeValues.UserLegal.ToString();
                }
                else
                {
                    this.LegalTextBox.Text = string.Empty;
                }

                if (!boeValues.IsAglandExemptionNull())
                {
                    this.AglandExemptionTextBox.Text = boeValues.AglandExemption.ToString();
                }
                else
                {
                    //this.AglandExemptionTextBox.Text = string.Empty;
                    this.AglandExemptionTextBox.Text = "0";
                }

                if (!boeValues.IsNonAglandExemptionNull())
                {
                    this.NonAglandExemptionTextBox.Text = boeValues.NonAglandExemption.ToString();
                }
                else
                {
                    //this.NonAglandExemptionTextBox.Text = string.Empty;
                    this.NonAglandExemptionTextBox.Text = "0";
                }

                if (!boeValues.IsAgImprExemptionNull())
                {
                    this.ImprExemptionTextBox.Text = boeValues.AgImprExemption.ToString();
                }
                else
                {
                    //this.ImprExemptionTextBox.Text = string.Empty;
                    this.ImprExemptionTextBox.Text = "0";
                }

                if (!boeValues.IsNonAgImprExemptionNull())
                {
                    this.NonImprExemptionTextBox.Text = boeValues.NonAgImprExemption.ToString();
                }
                else
                {
                    //this.NonImprExemptionTextBox.Text = string.Empty;
                    this.NonImprExemptionTextBox.Text = "0";
                }

                if (!boeValues.IsResidentialExemptionNull())
                {
                    this.ResidentExemptionTextBox.Text = boeValues.ResidentialExemption.ToString();
                }
                else
                {
                    //this.ResidentExemptionTextBox.Text = string.Empty;
                    this.ResidentExemptionTextBox.Text = "0";
                }

                if (!boeValues.IsIsLocalNull())
                {
                    this.LocalCheckBox.Checked = boeValues.IsLocal;
                }
                else
                {
                    this.LocalCheckBox.Checked = false;
                }

                if (!boeValues.IsIsCountryNull())
                {
                    this.CountyCheckBox.Checked = boeValues.IsCountry;
                }
                else
                {
                    this.CountyCheckBox.Checked = false;
                }

                if (!boeValues.IsIsStateNull())
                {
                    this.StateCheckBox.Checked = boeValues.IsState;
                }
                else
                {
                     this.StateCheckBox.Checked = false;
                }

               // this.LockControls(true);
            }
            else
            {
                this.ClearControls();
                //this.LockControls(false);
            }

            this.BindTotalValues();
            this.LoadClsCombos();
            this.EqualizationGridView.DataSource = this.boeData.AssessedGridValues.DefaultView;
            this.RefreshLocalValues();
            this.RefreshCountyValues();
            this.RefreshStateValues();
        }


        private void BindTotalValues()
        {
            if (this.boeData.TotalsValues.Rows.Count > 0)
            {
                F28100BOEData.TotalsValuesRow totalValues = (F28100BOEData.TotalsValuesRow)this.boeData.TotalsValues.Rows[0];

                if (!totalValues.IsAssessedTotalNull())
                {
                    this.AssessedTotalTextBox.Text = totalValues.AssessedTotal.ToString();
                }
                else
                {
                    this.AssessedTotalTextBox.Text = "0";
                }

                if (!totalValues.IsLocalTotalNull())
                {
                    this.LocalTotalTextBox.Text = totalValues.LocalTotal.ToString();
                }
                else
                {
                    this.LocalTotalTextBox.Text = "0";
                }

                if (!totalValues.IsCountyTotalNull())
                {
                    this.CountyTotalTextBox.Text = totalValues.CountyTotal.ToString();
                }
                else
                {
                    this.CountyTotalTextBox.Text = "0";
                }

                if (!totalValues.IsStateTotalNull())
                {
                    this.StateTotalTextBox.Text = totalValues.StateTotal.ToString();
                }
                else
                {
                    this.StateTotalTextBox.Text = "0";
                }

                if (!totalValues.IsFinalTotalNull())
                {
                    this.FinalTotalTextBox.Text = totalValues.FinalTotal.ToString();
                }
                else
                {
                    this.FinalTotalTextBox.Text = "0";
                }
            }
            else
            {
                this.ClearTotalControls();
            }
        }

        #endregion Bind Controls

        #region Save

        /// <summary>
        /// Saves the equalization.
        /// </summary>
        private void SaveEqualization()
        {
            F28100BOEData.BOEValuesDataTable boeData = new F28100BOEData.BOEValuesDataTable();
            F28100BOEData.BOEValuesRow newRow = boeData.NewBOEValuesRow();

            //newRow.EventID = this.keyId;
            newRow.BoeId = this.boeId;

            if (!string.IsNullOrEmpty(this.ProtestedByTextBox.Text.Trim()))
            {
                newRow.ProtestedBy = this.ProtestedByTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.AddressOneTextBox.Text.Trim()))
            {
                newRow.Address1 = this.AddressOneTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.AddresTwoTextBox.Text.Trim()))
            {
                newRow.Address2 = this.AddresTwoTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.CityTextBox.Text.Trim()))
            {
                newRow.City = this.CityTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.StateTextBox.Text.Trim()))
            {
                newRow.State = this.StateTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.ZipTextBox.Text.Trim()))
            {
                newRow.Zip = this.ZipTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.PhoneNumberTextBox.Text.Trim()))
            {
                newRow.PhoneNumber = this.PhoneNumberTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.EmailAddressTextBox.Text.Trim()))
            {
                newRow.EmailAddress = this.EmailAddressTextBox.Text.Trim();
            }

            if (this.AppraiserCombo.SelectedIndex > -1)
            {
                newRow.UserID = Convert.ToInt32(this.AppraiserCombo.SelectedValue);
            }

            if (!string.IsNullOrEmpty(this.ProtestNumberTextBox.Text.Trim()))
            {
                newRow.ProtestNumber = this.ProtestNumberTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.HearingDateTextBox.Text.Trim()))
            {
                newRow.HearingDate = this.HearingDateTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.ActionDateTextBox.Text.Trim()))
            {
                newRow.ActionDate = this.ActionDateTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.ClosedDateTextBox.Text.Trim()))
            {
                newRow.ClosedDate = this.ClosedDateTextBox.Text.Trim();
            }

            newRow.IsLocal = this.LocalCheckBox.Checked;
            newRow.IsCountry = this.CountyCheckBox.Checked;
            newRow.IsState = this.StateCheckBox.Checked;

            //if (!string.IsNullOrEmpty(this.AglandExemptionTextBox.Text.Trim()))
            //{
            //    newRow.AglandExemption = decimal.Parse(this.AglandExemptionTextBox.Text.Trim());
            //}

            //if (!string.IsNullOrEmpty(this.NonAglandExemptionTextBox.Text.Trim()))
            //{
            //    newRow.NonAglandExemption = int.Parse(this.NonAglandExemptionTextBox.Text.Trim());
            //}

            //if (!string.IsNullOrEmpty(this.ImprExemptionTextBox.Text.Trim()))
            //{
            //    newRow.AgImprExemption = decimal.Parse(this.ImprExemptionTextBox.Text.Trim());
            //}

            //if (!string.IsNullOrEmpty(this.NonImprExemptionTextBox.Text.Trim()))
            //{
            //    newRow.NonAgImprExemption = decimal.Parse(this.NonImprExemptionTextBox.Text.Trim());
            //}
           
            //if (!string.IsNullOrEmpty(this.ResidentExemptionTextBox.Text.Trim()))
            //{
            //    newRow.ResidentialExemption = int.Parse(this.ResidentExemptionTextBox.Text.Trim());
            //}
            boeData.Rows.Add(newRow);

            string boeElements = TerraScanCommon.GetXmlString(boeData);
            string boeValues = TerraScanCommon.GetXmlString(this.boeData.AssessedGridValues);
            try
            {
                this.form28100Control.WorkItem.F28100_SaveBOEDetails(this.keyId, boeElements, boeValues, TerraScanCommon.UserId);
            }
            catch (Exception ex)
            {
            }
            //this.form29630Control.WorkItem.F29630SaveBoardOfEqualizationDetails(boeElements, boeValues, TerraScanCommon.UserId);
        }

        #endregion Save

        private void EqualizationGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.Value.Equals(null))
                {
                    return;
                }

                if (e.ColumnIndex.Equals(this.AssessedValue.Index)
                    || e.ColumnIndex.Equals(this.LocalValue.Index)
                    || e.ColumnIndex.Equals(this.CountyValue.Index)
                    || e.ColumnIndex.Equals(this.StateValue.Index))
                {
                    string tempvalue = e.Value.ToString().Trim();
                    if (!string.IsNullOrEmpty(tempvalue))
                    {
                        decimal outDecimal;

                        //// If the entered value ends with '.' append 00
                        //if (tempvalue.EndsWith("."))
                        //{
                        //    tempvalue = string.Concat(tempvalue, "00");
                        //}

                        if (decimal.TryParse(tempvalue.Replace("$","").Replace(",","").Trim(), out outDecimal))
                        {
                            tempvalue = outDecimal.ToString();

                            if (tempvalue.Contains("-"))
                            {
                                // Restrict negative values
                                outDecimal = decimal.Zero;
                            }

                            if (outDecimal > 922337203685477)
                            {
                                outDecimal = decimal.Zero;
                            }
                        }

                        e.Value = outDecimal.ToString("#,##0");
                        e.ParsingApplied = true;
                    }
                    else
                    {
                        e.Value = string.Empty;
                    }
                }

                if (e.ColumnIndex.Equals(this.LocalValue.Index) || e.ColumnIndex.Equals(this.CountyValue.Index)
                    || e.ColumnIndex.Equals(this.StateValue.Index))
                {
                    this.boeData.TotalsValues.Clear();

                    if (e.ColumnIndex.Equals(this.LocalValue.Index))
                    {
                        this.boeData.AssessedGridValues.Rows[e.RowIndex][this.LocalValue.Name] = e.Value;
                    }

                    if (e.ColumnIndex.Equals(this.CountyValue.Index))
                    {
                        this.boeData.AssessedGridValues.Rows[e.RowIndex][this.CountyValue.Name] = e.Value;
                    }

                    if (e.ColumnIndex.Equals(this.StateValue.Index))
                    {
                        this.boeData.AssessedGridValues.Rows[e.RowIndex][this.StateValue.Name] = e.Value;
                    }

                    //this.boeData.AssessedGridValues.AcceptChanges();
                    string boeValues = TerraScanCommon.GetXmlString(this.boeData.AssessedGridValues);
                    this.boeData.Merge(this.form28100Control.WorkItem.F28100_GetTotalAmount(boeId, this.keyId, boeValues).TotalsValues);
                    this.BindTotalValues();
                 }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        #endregion Private Methods

        #region CheckBox Event

        private void LocalCheckBox_CheckedChanged(object sender, EventArgs e)
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

                if (!this.flagFormLoad)
                {
                    this.RefreshLocalValues();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void RefreshLocalValues()
        {
            if (this.LocalCheckBox.Checked)
            {
                this.ChangeComboStyle(this.boeData.AssessedGridValues.LocalClsColumn.ColumnName, this.boeData.AssessedGridValues.LocalValueColumn.ColumnName, true);
                if (!this.flagFormLoad)
                {
                    this.boeData.AssessedValues.Clear();
                    string boeValues = TerraScanCommon.GetXmlString(this.boeData.AssessedGridValues);
                    this.boeData.Merge(this.form28100Control.WorkItem.F28100_GetLocalValues(boeValues).AssessedValues);
                    this.AssignAssessedValues(this.boeData.AssessedGridValues.LocalClsColumn.ColumnName, this.boeData.AssessedGridValues.LocalValueColumn.ColumnName);
                    this.boeData.AssessedGridValues.AcceptChanges();

                    this.boeData.TotalsValues.Clear();
                    string assessValues = TerraScanCommon.GetXmlString(this.boeData.AssessedGridValues);
                    this.boeData.Merge(this.form28100Control.WorkItem.F28100_GetTotalAmount(boeId, this.keyId, assessValues).TotalsValues);
                    this.BindTotalValues();
                }
            }
            else
            {
                this.ChangeComboStyle(this.boeData.AssessedGridValues.LocalClsColumn.ColumnName, this.boeData.AssessedGridValues.LocalValueColumn.ColumnName, false);
                this.ClearColumnValues(this.boeData.AssessedGridValues.LocalClsColumn.ColumnName, this.boeData.AssessedGridValues.LocalValueColumn.ColumnName);

                //if (!this.flagFormLoad)
                //{
                    this.boeData.AssessedGridValues.AcceptChanges();

                    this.boeData.TotalsValues.Clear();
                    string assessValues = TerraScanCommon.GetXmlString(this.boeData.AssessedGridValues);
                    this.boeData.Merge(this.form28100Control.WorkItem.F28100_GetTotalAmount(boeId, this.keyId, assessValues).TotalsValues);
                    this.BindTotalValues();
                //}
            }
        }

        private void CountyCheckBox_CheckedChanged(object sender, EventArgs e)
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

                if (!this.flagFormLoad)
                {
                    this.RefreshCountyValues();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void RefreshCountyValues()
        {
            if (this.CountyCheckBox.Checked)
            {
                this.ChangeComboStyle(this.boeData.AssessedGridValues.CountyClsColumn.ColumnName, this.boeData.AssessedGridValues.CountyValueColumn.ColumnName, true);
                if (!this.flagFormLoad)
                {
                    this.boeData.AssessedValues.Clear();
                    string boeValues = TerraScanCommon.GetXmlString(this.boeData.AssessedGridValues);
                    this.boeData.Merge(this.form28100Control.WorkItem.F28100_GetCountyValues(LocalCheckBox.Checked, boeValues).AssessedValues);
                    this.AssignAssessedValues(this.boeData.AssessedGridValues.CountyClsColumn.ColumnName, this.boeData.AssessedGridValues.CountyValueColumn.ColumnName);
                    this.boeData.AssessedGridValues.AcceptChanges();

                    this.boeData.TotalsValues.Clear();
                    string assessValues = TerraScanCommon.GetXmlString(this.boeData.AssessedGridValues);
                    this.boeData.Merge(this.form28100Control.WorkItem.F28100_GetTotalAmount(boeId, this.keyId, assessValues).TotalsValues);
                    this.BindTotalValues();
                }
            }
            else
            {
                this.ChangeComboStyle(this.boeData.AssessedGridValues.CountyClsColumn.ColumnName, this.boeData.AssessedGridValues.CountyValueColumn.ColumnName, false);
                this.ClearColumnValues(this.boeData.AssessedGridValues.CountyClsColumn.ColumnName, this.boeData.AssessedGridValues.CountyValueColumn.ColumnName);

                //if (!this.flagFormLoad)
                //{
                    this.boeData.AssessedGridValues.AcceptChanges();

                    this.boeData.TotalsValues.Clear();
                    string assessValues = TerraScanCommon.GetXmlString(this.boeData.AssessedGridValues);
                    this.boeData.Merge(this.form28100Control.WorkItem.F28100_GetTotalAmount(boeId, this.keyId, assessValues).TotalsValues);
                    this.BindTotalValues();
                //}
            }
        }

        private void StateCheckBox_CheckedChanged(object sender, EventArgs e)
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

                if (!this.flagFormLoad)
                {
                    this.RefreshStateValues();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void RefreshStateValues()
        {
            if (this.StateCheckBox.Checked)
            {
                this.ChangeComboStyle(this.boeData.AssessedGridValues.StateClsColumn.ColumnName, this.boeData.AssessedGridValues.StateValueColumn.ColumnName, true);
                if (!this.flagFormLoad)
                {
                    this.boeData.AssessedValues.Clear();
                    string boeValues = TerraScanCommon.GetXmlString(this.boeData.AssessedGridValues);
                    this.boeData.Merge(this.form28100Control.WorkItem.F28100_GetStateValues(LocalCheckBox.Checked, CountyCheckBox.Checked, boeValues).AssessedValues);
                    this.AssignAssessedValues(this.boeData.AssessedGridValues.StateClsColumn.ColumnName, this.boeData.AssessedGridValues.StateValueColumn.ColumnName);
                    this.boeData.AssessedGridValues.AcceptChanges();

                    this.boeData.TotalsValues.Clear();
                    string assessValues = TerraScanCommon.GetXmlString(this.boeData.AssessedGridValues);
                    this.boeData.Merge(this.form28100Control.WorkItem.F28100_GetTotalAmount(boeId, this.keyId, assessValues).TotalsValues);
                    this.BindTotalValues();
                }
            }
            else
            {
                this.ChangeComboStyle(this.boeData.AssessedGridValues.StateClsColumn.ColumnName, this.boeData.AssessedGridValues.StateValueColumn.ColumnName, false);
                this.ClearColumnValues(this.boeData.AssessedGridValues.StateClsColumn.ColumnName, this.boeData.AssessedGridValues.StateValueColumn.ColumnName);
                //if (!this.flagFormLoad)
                //{
                    this.boeData.AssessedGridValues.AcceptChanges();
                    this.boeData.TotalsValues.Clear();
                    string assessValues = TerraScanCommon.GetXmlString(this.boeData.AssessedGridValues);
                    this.boeData.Merge(this.form28100Control.WorkItem.F28100_GetTotalAmount(boeId, this.keyId, assessValues).TotalsValues);
                    this.BindTotalValues();
                //}
            }
        }

        private void ClearColumnValues(string clsColumnName, string valueColumnName)
        {
            for (int i = 0; i < this.EqualizationGridView.Rows.Count; i++)
            {
                this.EqualizationGridView.Rows[i].Cells[clsColumnName].Value = string.Empty;
                this.EqualizationGridView.Rows[i].Cells[valueColumnName].Value = string.Empty;
            }
        }

        private void AssignAssessedValues(string clsColumnName, string valueColumnName)
        {
            if (this.boeData.AssessedValues.Rows.Count > 0)
            {
                int rowNo = 0;
                foreach (F28100BOEData.AssessedValuesRow valueRow in this.boeData.AssessedValues)
                {
                    this.EqualizationGridView.Rows[rowNo].Cells[clsColumnName].Value = valueRow.ClassID;
                    this.EqualizationGridView.Rows[rowNo].Cells[valueColumnName].Value = valueRow.Value;
                    rowNo++;
                }
            }
        }

        private void ChangeComboStyle(string clsColumnName, string valueColumnName, bool enableStyle)
        {
            if (enableStyle)
            {
                (this.EqualizationGridView.Columns[clsColumnName] as DataGridViewComboBoxColumn).DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                this.EqualizationGridView.Columns[clsColumnName].ReadOnly = false;
                this.EqualizationGridView.Columns[valueColumnName].ReadOnly = false;
                this.EqualizationGridView.Columns[clsColumnName].DefaultCellStyle.ForeColor = Color.Black;
                this.EqualizationGridView.Columns[valueColumnName].DefaultCellStyle.ForeColor = Color.Black;
            }
            else
            {
                (this.EqualizationGridView.Columns[clsColumnName] as DataGridViewComboBoxColumn).DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                this.EqualizationGridView.Columns[clsColumnName].ReadOnly = true;
                this.EqualizationGridView.Columns[valueColumnName].ReadOnly = true;
                this.EqualizationGridView.Columns[clsColumnName].DefaultCellStyle.ForeColor = Color.Gray;
                this.EqualizationGridView.Columns[valueColumnName].DefaultCellStyle.ForeColor = Color.Gray;
            }
        }

        #endregion CheckBox Event

        #region Button Event

        private void PushValueButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.form28100Control.WorkItem.F28100_PushBOEDetails(this.boeId, TerraScanCommon.UserId);
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

        #endregion Button Event

        private void DistrictLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.boeData.BOEValues.Rows[0][this.boeData.BOEValues.DistrictIDColumn.ColumnName].ToString()))
                {
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(11002);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = this.boeData.BOEValues.Rows[0][this.boeData.BOEValues.DistrictIDColumn.ColumnName].ToString();
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void EqualizationGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.LocalValue.Name].Index)
               || e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.CountyValue.Name].Index)
               || e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.StateValue.Name].Index))
            {
                this.boeData.AssessedGridValues.AcceptChanges();
            }
        }
    }
}
