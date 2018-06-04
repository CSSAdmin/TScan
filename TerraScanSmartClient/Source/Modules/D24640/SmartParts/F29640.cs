// -------------------------------------------------------------------------------------------
// <copyright file="F29640.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// </summary>
// Release history
// ********************************************************************************************
// Date               Author            Description
// ----------        ---------     -------------------------------------------------------
// 
// -------------------------------------------------------------------------------------------
namespace D24640
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Infrastructure.Interface;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.SmartParts;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;

    /// <summary>
    /// F29640 Class
    /// </summary>
    [SmartPart]
    public partial class F29640 : BaseSmartPart
    {
        #region Variable
        /// <summary>
        /// Master Form number
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// Current Key Id[EventId] of the form
        /// </summary>
        private int keyId;

        /// <summary>
        /// The Frozen ID
        /// </summary>
        private int frozenId;

        /// <summary>
        /// Slice Permission for the form
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Form master edit permission
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// F29640 Controller
        /// </summary>
        private F29640Controller form29640Controller;

        /// <summary>
        /// The Page Mode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// F29640FrozenValueData Typed DataSet
        /// </summary>
        private F29640FrozenValueData frozenDataTable = new F29640FrozenValueData();

        /// <summary>
        /// F29640FrozenValueData Data Row
        /// </summary>
        private F29640FrozenValueData.GetFrozenValuesRow currentRow;

        /// <summary>
        /// flag LoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        #endregion Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F29640"/> class.
        /// </summary>
        public F29640()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F29640"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red (R).</param>
        /// <param name="green">The green (G).</param>
        /// <param name="blue">The blue (B).</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F29640(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.FrozenPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.FrozenPictureBox.Height, this.FrozenPictureBox.Width, string.Empty, red, green, blue);
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

        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the form29640 controller.
        /// </summary>
        /// <value>The form29640 controller.</value>
        [CreateNew]
        public F29640Controller Form29640Controller
        {
            get { return this.form29640Controller as F29640Controller; }
            set { this.form29640Controller = value; }
        }

        #endregion Property

        #region Event Subscription

        /// <summary>
        /// Event Subscription for cancel slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
        {
            // Lock key press based on Edit permission
            if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
            {
                this.LockControls(false);
            }
            else
            {
                this.LockControls(true);
            }

            this.ClearControls();
            // Load frozen details based on keyid
            this.LoadFrozenValueDetails();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            // Set focus on first editable field
            this.FrozenValueTextBox.Focus();
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission))
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
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission))
            {
                // Save Frozen
                this.SaveFrozenDetails();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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
                    // Get form slice permission
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;
                    // Lock controls keypress based on Edit permission
                    this.LockControls(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);

                    if (this.frozenDataTable.GetFrozenValues.Rows.Count > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
                        ////this.FrozenValueTextBox.Focus();
                    }
                    else
                    {
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            eventArgs.Data.FlagInvalidSliceKey = true;
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
        /// Event Subscription for delete slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            if (this.slicePermissionField.deletePermission)
            {
                this.Cursor = Cursors.WaitCursor;
                // DB call for Delete
                this.form29640Controller.WorkItem.DeleteFrozenDetails(this.keyId, this.frozenId, TerraScanCommon.UserId);
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
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.flagLoadOnProcess = true;
                    this.ClearControls();
                    // Lock key press based on Edit permission
                    this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                    // Load Frozen details
                    this.LoadFrozenValueDetails();
                    this.FrozenValueTextBox.Focus();
                    this.flagLoadOnProcess = false;
                }
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

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F29640 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F29640_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                // Load Frozen Details
                this.LoadFrozenValueDetails();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                // Set focus on first editable field
                this.FrozenValueTextBox.Focus();
                this.flagLoadOnProcess = false;
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

        #endregion Form Load

        #region Events

        /// <summary>
        /// Enables the edit button in master form.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EnableEditButtonInMasterForm(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    // Enable Form Master Save/Cancel button
                    this.EditEnabled();
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
        /// Handles the Click event of the FrozenPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FrozenPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                // Expand/Collapse Form Slice
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the FrozenPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FrozenPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                // Set tool tip
                this.FrozenToolTip.SetToolTip(this.FrozenPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Events

        #region Methods

        #region Controls Visibility

        /// <summary>
        /// Clears the controls.
        /// </summary>
        private void ClearControls()
        {
            this.FrozenValueTextBox.Text = string.Empty;
            this.NoteTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="hasLocked">if set to <c>true</c> [has locked].</param>
        private void LockControls(bool hasLocked)
        {
            this.FrozenValueTextBox.LockKeyPress = hasLocked;
            this.NoteTextBox.LockKeyPress = hasLocked;
        }

        /// <summary>
        /// Enables the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableControls(bool enable)
        {
            this.FrozenValuePanel.Enabled = enable;
            this.NotePanel.Enabled = enable;
        }

        #endregion Controls Visibility

        #region Enable Edit

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                // Event publication for enable Save,Cancel button in Form Master
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        #endregion Enable Edit

        #region Load Frozen Details
        /// <summary>
        /// Loads the frozen value details.
        /// </summary>
        private void LoadFrozenValueDetails()
        {
            if (this.keyId > 0)
            {
                // DB call for f29640_pcget_FrozenValues(EventId)
                this.frozenDataTable = this.form29640Controller.WorkItem.GetFrozenValue(this.keyId);
            }

            // Set retrived values on appropriate controls
            this.SetControlValues();
        }

        /// <summary>
        /// Sets the Frozen Details on appropriate controls
        /// </summary>
        private void SetControlValues()
        {
            if (this.frozenDataTable.GetFrozenValues.Rows.Count > 0)
            {
                // Enable all controls
                this.EnableControls(true);

                this.currentRow = (F29640FrozenValueData.GetFrozenValuesRow)this.frozenDataTable.GetFrozenValues.Rows[0];

                if (!this.currentRow.IsFrozenValueNull())
                {
                    this.FrozenValueTextBox.Text = this.currentRow.FrozenValue.ToString();
                }
                else
                {
                    this.FrozenValueTextBox.Text = string.Empty;
                }

                if (!this.currentRow.IsNoteNull())
                {
                    this.NoteTextBox.Text = this.currentRow.Note;
                }
                else
                {
                    this.NoteTextBox.Text = string.Empty;
                }

                this.frozenId = this.currentRow.FrozenID;
            }
            else
            {
                // Disable Controls
                this.EnableControls(false);
            }
        }

        #endregion Load Frozen Details

        #region Validation

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>Slice validation Fields</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();

            if (string.IsNullOrEmpty(this.FrozenValueTextBox.Text.Trim())) // Required field validation
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.FormNo = formNo;
                sliceValidationFields.RequiredFieldMissing = false;
            }
            else
            {
                // Money value validation
                decimal maxMoneyValue = 922337203685477.5807M;
                if (this.FrozenValueTextBox.DecimalTextBoxValue > maxMoneyValue)
                {
                    sliceValidationFields.ErrorMessage = String.Concat("Frozen Value exceeds maximum limit.", "\n");
                    this.FrozenValueTextBox.Focus();
                }
                else
                {
                    sliceValidationFields.ErrorMessage = string.Empty;
                }

                sliceValidationFields.FormNo = formNo;
                sliceValidationFields.RequiredFieldMissing = false;
            }

            return sliceValidationFields;
        }

        #endregion Validation

        #region Save Frozen Value

        /// <summary>
        /// Saves the frozen details.
        /// </summary>
        private void SaveFrozenDetails()
        {
            F29640FrozenValueData.SaveFrozenValuesDataTable frozenTable = new F29640FrozenValueData.SaveFrozenValuesDataTable();
            F29640FrozenValueData.SaveFrozenValuesRow frozenRow = frozenTable.NewSaveFrozenValuesRow();

            frozenRow.EventID = this.keyId;

            if (this.frozenId > 0)
            {
                frozenRow.FrozenID = this.frozenId;
            }
            else
            {
                frozenRow.FrozenID = 0;
            }

            frozenRow.FrozenValue = this.FrozenValueTextBox.DecimalTextBoxValue;

            if (!string.IsNullOrEmpty(this.NoteTextBox.Text.Trim()))
            {
                frozenRow.Note = this.NoteTextBox.Text.Trim();
            }
            else
            {
                frozenRow.Note = string.Empty;
            }

            frozenTable.Rows.Add(frozenRow);

            // DB call for save
            this.form29640Controller.WorkItem.SaveFrozenDetails(TerraScanCommon.GetXmlString(frozenTable), TerraScanCommon.UserId);
        }

        #endregion Save Frozen Value

        #endregion Methods
    }
}
