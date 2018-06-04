//--------------------------------------------------------------------------------------------
// <copyright file="F35101.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F35101.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 16 May 07        KARTHIKEYAN.B      Created
//*********************************************************************************/

namespace D35100
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

    /// <summary>
    /// This contains methods for the F35101
    /// </summary>
    public partial class F35101 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// Form35101 Controller
        /// </summary>
        private F35101Controller form35101Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int keyId;

        /// <summary>
        /// formMasterPermissionEdit Local variable.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// An instance for F35101NeighborhoodGroupHeaderData
        /// </summary>
        private F35101NeighborhoodGroupHeaderData neighborhoodGroupHeaderData = new F35101NeighborhoodGroupHeaderData();

        /// <summary>
        /// An instance for F35100NeighborhoodHeaderData
        /// </summary>
        private F35100NeighborhoodHeaderData neighborhoodHeaderData = new F35100NeighborhoodHeaderData();

        #endregion

        #region Variable For ComboBox Value Members

        /// <summary>
        /// Reviewed By Id
        /// </summary>
        private int reviewedById;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F35101"/> class.
        /// </summary>
        public F35101()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F35101"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F35101(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.NeighborhoodGroupPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.NeighborhoodGroupPictureBox.Height, this.NeighborhoodGroupPictureBox.Width, tabText, red, green, blue);
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
        /// Declare the event D84700_F84722_OnSave_SetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84700_F84722_OnSave_SetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> FormSlice_OnSave_SetKeyId;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F35101 control.
        /// </summary>
        /// <value>The F35101 control.</value>
        [CreateNew]
        public F35101Controller Form35101Control
        {
            get { return this.form35101Control as F35101Controller; }
            set { this.form35101Control = value; }
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
                this.keyId = 0;
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.ClearNeighborhoodGroupHeaderControls();
                this.SetNewComboIndex();
                this.LockControls(true);
                this.ControlLock(false);
            }
            else
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearNeighborhoodGroupHeader();
                this.SetNewComboIndex();
                this.LockControls(false);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
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
                    SliceValidationFields sliceValidationFields = new SliceValidationFields();
                    sliceValidationFields.FormNo = eventArgs.Data;
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
        /// Called when [D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
            {
                this.ControlLock(false);
            }
            else
            {
                this.ControlLock(true);
            }

            this.LockControls(true);
            this.CustomizeNeighborhoodGroupHeader();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save confirmed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                if (this.slicePermissionField.editPermission)
                {
                    this.SaveNeighborhoodGroupHeader();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            else
            {
                this.LockControls(true);
                this.ControlLock(false);
                this.CustomizeNeighborhoodGroupHeader();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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
                this.DeleteNeighborhoodGroupHeader();

                SliceFormCloseAlert sliceFormCloseAlert;
                sliceFormCloseAlert.FormNo = this.masterFormNo;
                sliceFormCloseAlert.FlagFormClose = true;
                this.FormSlice_FormCloseAlert(this, new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
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

                    if (this.neighborhoodGroupHeaderData.GetNeighborhoodGroupHeader.Rows.Count > 0)
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
                    this.FlagSliceForm = true;
                    this.flagLoadOnProcess = true;
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.CustomizeNeighborhoodGroupHeader();
                    this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.flagLoadOnProcess = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Event Subscription D84700_F84721_OnSave_GetKeyId.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D84700_F84721_OnSave_GetKeyId, ThreadOption.UserInterface)]
        public void FormSlice_OnSave_GetKeyId(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.keyId = eventArgs.Data.SelectedKeyId;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Protected Methods

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

        #endregion

        #region Form Control Events

        /// <summary>
        /// F35101 Form Load
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F35101_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                ////Set the Max Length for editable fields
                this.SetMaxLength();
                this.CustomizeNeighborhoodGroupHeader();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                this.flagLoadOnProcess = false;
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
        /// Handles the Click event of the Neighborhood Group PictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NeighborhoodGroupPictureBox_Click(object sender, EventArgs e)
        {
            this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
        }

        /// <summary>
        /// Handles the MouseEnter event of the PropertiesPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NeighborhoodGroupPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.NeighborhoodGroupHeaderToolTip.SetToolTip(this.NeighborhoodGroupPictureBox, Utility.GetFormNameSpace(this.Name));
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the ReviewedBy ComboBox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Check the page mode to enable or disable the save, cancel Buttons for "Change Events In Text Box/CheckBox"        
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EnableSaveCancelButton(object sender, EventArgs e)
        {
            this.ToEnableEditButtonInMasterForm();
        }

        /// <summary>
        /// To handle keypress event of the Textboxes, for not allowing the "Enter" Key
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)13:
                    {
                        e.Handled = true;
                        break;
                    }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>sliceValidationFields</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;
            if (string.IsNullOrEmpty(this.NeighborhoodGroupTextBox.Text.Trim()))
            {
                this.NeighborhoodGroupTextBox.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredFields");
                this.NeighborhoodGroupTextBox.Focus();
            }
            else if (string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                this.RollYearTextBox.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredFields");
                this.RollYearTextBox.Focus();
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Sets the max length for the editable textboxes and comboboxes.
        /// </summary>
        private void SetMaxLength()
        {
            this.NeighborhoodGroupTextBox.MaxLength = this.neighborhoodGroupHeaderData.GetNeighborhoodGroupHeader.GroupNameColumn.MaxLength;
            this.NeighborhoodGroupDescriptionTextBox.MaxLength = this.neighborhoodGroupHeaderData.GetNeighborhoodGroupHeader.DescriptionColumn.MaxLength;
            this.NeighborhoodGroupMarketReviewTextBox.MaxLength = this.neighborhoodGroupHeaderData.GetNeighborhoodGroupHeader.MarketReviewColumn.MaxLength;
            this.ReviewedByComboBox.MaxLength = this.neighborhoodHeaderData.f35100GetUserDetailsDataTable.UserNameColumn.MaxLength;
        }

        /// <summary>
        /// To set index for combo on new mode  
        /// </summary>
        private void SetNewComboIndex()
        {
            this.ReviewedByComboBox.SelectedIndex = -1;
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">Control Lock</param>
        private void ControlLock(bool controlLook)
        {
            this.NeighborhoodGroupTextBox.LockKeyPress = controlLook;
            this.RollYearTextBox.LockKeyPress = controlLook;
            this.NeighborhoodGroupDescriptionTextBox.LockKeyPress = controlLook;
            this.NeighborhoodGroupMarketReviewTextBox.LockKeyPress = controlLook;

            this.ReviewedByComboBox.Enabled = !controlLook;
        }

        /// <summary>
        /// To Enable/Disable All the Controls
        /// </summary>
        /// <param name="lockControl">Lock Control</param>
        private void LockControls(bool lockControl)
        {
            this.NeighborhoodGroupPanel.Enabled = lockControl;
            this.RollYearPanel.Enabled = lockControl;
            this.ReviewedByPanel.Enabled = lockControl;
            this.NeighborhoodGroupDescriptionPanel.Enabled = lockControl;
            this.NeighborhoodGroupMarketReviewPanel.Enabled = lockControl;
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
        /// To enable the Edit button in nthe Master Form
        /// </summary>
        private void ToEnableEditButtonInMasterForm()
        {
            if (!this.flagLoadOnProcess)
            {
                this.EditEnabled();
            }
        }

        /// <summary>
        /// To load all the values in Neighborhood Group Header Form
        /// </summary>
        private void CustomizeNeighborhoodGroupHeader()
        {
            this.neighborhoodGroupHeaderData = this.form35101Control.WorkItem.F35101_GetNeighborhoodGroupHeader(this.keyId);
            if (this.neighborhoodGroupHeaderData.GetNeighborhoodGroupHeader.Rows.Count > 0)
            {
                this.NeighborhoodGroupTextBox.Text = this.neighborhoodGroupHeaderData.GetNeighborhoodGroupHeader.Rows[0][this.neighborhoodGroupHeaderData.GetNeighborhoodGroupHeader.GroupNameColumn].ToString();
                this.RollYearTextBox.Text = this.neighborhoodGroupHeaderData.GetNeighborhoodGroupHeader.Rows[0][this.neighborhoodGroupHeaderData.GetNeighborhoodGroupHeader.RollYearColumn].ToString();
                this.NeighborhoodGroupDescriptionTextBox.Text = this.neighborhoodGroupHeaderData.GetNeighborhoodGroupHeader.Rows[0][this.neighborhoodGroupHeaderData.GetNeighborhoodGroupHeader.DescriptionColumn].ToString();
                this.NeighborhoodGroupMarketReviewTextBox.Text = this.neighborhoodGroupHeaderData.GetNeighborhoodGroupHeader.Rows[0][this.neighborhoodGroupHeaderData.GetNeighborhoodGroupHeader.MarketReviewColumn].ToString();

                Int32.TryParse(this.neighborhoodGroupHeaderData.GetNeighborhoodGroupHeader.Rows[0][this.neighborhoodGroupHeaderData.GetNeighborhoodGroupHeader.ReviewedByColumn].ToString(), out this.reviewedById);
            }
            else
            {
                this.ClearNeighborhoodGroupHeader();
                this.LockControls(false);
            }

            ////To Load The ComboBox
            this.LoadComboBox();
        }

        /// <summary>
        /// Load the Reviewed By Combobox
        /// </summary>
        private void LoadComboBox()
        {
            F35100NeighborhoodHeaderData.f35100GetUserDetailsDataTableDataTable listUserDetails = new F35100NeighborhoodHeaderData.f35100GetUserDetailsDataTableDataTable();

            DataRow customRow = listUserDetails.NewRow();
            listUserDetails.Clear();
            customRow[this.neighborhoodHeaderData.f35100GetUserDetailsDataTable.UserNameColumn.ColumnName] = string.Empty;
            customRow[this.neighborhoodHeaderData.f35100GetUserDetailsDataTable.UserIDColumn.ColumnName] = "0";
            listUserDetails.Rows.Add(customRow);
            this.neighborhoodHeaderData = this.form35101Control.WorkItem.F35100_GetNeighborhoodHeaderUserDetails(TerraScanCommon.ApplicationId);
            listUserDetails.Merge(this.neighborhoodHeaderData.f35100GetUserDetailsDataTable);

            if (this.neighborhoodHeaderData.f35100GetUserDetailsDataTable.Rows.Count > 0)
            {
                this.ReviewedByComboBox.DataSource = listUserDetails;
                this.ReviewedByComboBox.DisplayMember = this.neighborhoodHeaderData.f35100GetUserDetailsDataTable.UserNameColumn.ColumnName;
                this.ReviewedByComboBox.ValueMember = this.neighborhoodHeaderData.f35100GetUserDetailsDataTable.UserIDColumn.ColumnName;
                if (this.reviewedById > 0)
                {
                    this.ReviewedByComboBox.SelectedValue = this.reviewedById;
                }
                else
                {
                    this.ReviewedByComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Clears the All the comboboxes in the form.
        /// </summary>
        /// <param name="currentControl">The current control.</param>
        private void ClearComboBox(Control currentControl)
        {
            if (currentControl.HasChildren)
            {
                foreach (Control childControl in currentControl.Controls)
                {
                    this.ClearComboBox(childControl);
                }
            }
            else
            {
                if (currentControl.GetType() == typeof(TerraScanComboBox))
                {
                    TerraScanComboBox currentComboBox = (TerraScanComboBox)currentControl;
                    currentComboBox.DataSource = null;
                    currentComboBox.Items.Clear();
                    currentComboBox.Refresh();
                }
            }
        }

        /// <summary>
        /// To Clear all the Textboxes in the form
        /// </summary>
        private void ClearNeighborhoodGroupHeaderControls()
        {
            this.NeighborhoodGroupTextBox.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.NeighborhoodGroupDescriptionTextBox.Text = string.Empty;
            this.NeighborhoodGroupMarketReviewTextBox.Text = string.Empty;
        }

        /// <summary>
        /// To Clear the entire Neighborhood Group Header Form
        /// </summary>
        private void ClearNeighborhoodGroupHeader()
        {
            this.ClearComboBox(this);
            this.ClearNeighborhoodGroupHeaderControls();
        }

        ///<summary>
        ///To save all the values in the Form
        ///</summary>
        private void SaveNeighborhoodGroupHeader()
        {
            short tempRollYear;

            this.neighborhoodGroupHeaderData.GetNeighborhoodGroupHeader.Rows.Clear();
            F35101NeighborhoodGroupHeaderData.GetNeighborhoodGroupHeaderRow dr = this.neighborhoodGroupHeaderData.GetNeighborhoodGroupHeader.NewGetNeighborhoodGroupHeaderRow();

            dr.NBHDGroupID = this.keyId;

            #region String Textboxes

            if (!string.IsNullOrEmpty(this.NeighborhoodGroupTextBox.Text.Trim()))
            {
                dr.GroupName = this.NeighborhoodGroupTextBox.Text.Trim();
            }
            else
            {
                dr.GroupName = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.NeighborhoodGroupDescriptionTextBox.Text.Trim()))
            {
                dr.Description = this.NeighborhoodGroupDescriptionTextBox.Text.Trim();
            }
            else
            {
                dr.Description = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.NeighborhoodGroupMarketReviewTextBox.Text.Trim()))
            {
                dr.MarketReview = this.NeighborhoodGroupMarketReviewTextBox.Text.Trim();
            }
            else
            {
                dr.MarketReview = string.Empty;
            }

            #endregion

            #region Numeric Textboxes

            if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                short.TryParse(this.RollYearTextBox.Text.Trim(), out tempRollYear);
                dr.RollYear = tempRollYear;
            }

            #endregion

            #region ComboBoxes

            if (!string.IsNullOrEmpty(this.ReviewedByComboBox.Text.Trim()))
            {
                dr.ReviewedBy = Convert.ToInt32(this.ReviewedByComboBox.SelectedValue);
            }
            else
            {
                dr.ReviewedBy = 0;
            }

            #endregion

            this.neighborhoodGroupHeaderData.GetNeighborhoodGroupHeader.Rows.Add(dr);
            this.keyId = this.form35101Control.WorkItem.F35101_SaveNeighborhoodGroupHeader(this.keyId, (Utility.GetXmlString(this.neighborhoodGroupHeaderData.GetNeighborhoodGroupHeader.Copy())), TerraScan.Common.TerraScanCommon.UserId);

            if (this.keyId == -99)
            {
                ////Duplicate Record
                MessageBox.Show(SharedFunctions.GetResourceString("DuplicateRecord"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SliceReloadActiveRecord currentSliceInfo;
                currentSliceInfo.MasterFormNo = this.masterFormNo;
                currentSliceInfo.SelectedKeyId = this.keyId;
                this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                ////to refresh the master form with the return keyid
                this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
            }
        }

        /// <summary>
        /// To delete the values in Neighborhood Group Header Form
        /// </summary>
        private void DeleteNeighborhoodGroupHeader()
        {
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    this.form35101Control.WorkItem.F35101_DeleteNeighborhoodGroupHeader(this.keyId);
            //}
            //catch (SoapException e)
            //{
            //    ExceptionManager.ManageException(e, ExceptionManager.ActionType.Display, this.ParentForm);
            //}
            //catch (Exception ex)
            //{
            //    //////TODO : Need to find specific exception and handle it.
            //    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            //}
            //finally
            //{
            //    this.Cursor = Cursors.Default;
            //}
        }

        #endregion
    }
}
