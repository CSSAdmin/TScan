// -------------------------------------------------------------------------------------------
// <copyright file="F29650.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// </summary>
// Release history
// ********************************************************************************************
// Date               Author            Description
// ----------        ---------     -------------------------------------------------------
//                D.LathaMaheswari    Created
// -------------------------------------------------------------------------------------------
namespace D24650
{
    using System;
    using System.Data;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;

    /// <summary>
    /// F29650 class
    /// </summary>
    [SmartPart]
    public partial class F29650 : BaseSmartPart
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
        /// Exemption ID
        /// </summary>
        private int exemptionId;

        /// <summary>
        /// Slice Permission for the form
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Form master edit permission
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// F29650 Controller
        /// </summary>
        private F29650Controller form29650Controller;

        /// <summary>
        /// Page Mode type
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// F29650ExemptionData Typed DataSet
        /// </summary>
        private F29650ExemptionData exemptionTable = new F29650ExemptionData();

        /// <summary>
        /// F29650FrozenValueData Data Row
        /// </summary>
        private F29650ExemptionData.GetExemptionRow currentRow;

        /// <summary>
        /// Exemption type table
        /// </summary>
        private F29650ExemptionData exemptionTypeTable = new F29650ExemptionData();

        /// <summary>
        /// flag Load On Process
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// Exemption Event ID
        /// </summary>
        private int exemptionEventId;

        /// <summary>
        /// Exemption Id
        /// </summary>
        private string tempExemptionId = string.Empty;

        #endregion Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F29650"/> class.
        /// </summary>
        public F29650()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F29650"/> class
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red color.</param>
        /// <param name="green">The green color.</param>
        /// <param name="blue">The blue color.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F29650(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.ExemptionPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ExemptionPictureBox.Height, this.ExemptionPictureBox.Width, string.Empty, red, green, blue);
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
        /// Gets or sets the form29650 controller.
        /// </summary>
        /// <value>The form29650 controller.</value>
        [CreateNew]
        public F29650Controller Form29650Controller
        {
            get { return this.form29650Controller as F29650Controller; }
            set { this.form29650Controller = value; }
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
            //// Lock key press based on Edit permission
            if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
            {
                this.LockControls(false);
            }
            else
            {
                this.LockControls(true);
            }

            this.ClearControls();

            // Load Exemption details
            this.LoadExemptionDetails();
            this.pageMode = TerraScanCommon.PageModeTypes.View;

            // set focus on first editable field
            this.ExemptionTypeComboBox.Focus();
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission)
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
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission)
            {
                // Save exemption details
                this.SaveExemptionDetails();
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
                    //// Get form slice permission
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;
                    
                    // Lock controls keypress based on Edit permission
                    this.LockControls(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);

                    if (this.exemptionTable.GetExemption.Rows.Count > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
                        
                        // Set focus on first editable field
                        this.ExemptionTypeComboBox.Focus();
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
                this.form29650Controller.WorkItem.DeleteExemptionDetails(this.keyId, this.exemptionEventId, TerraScanCommon.UserId);
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
                    
                    // Lock controls keypress based on Edit permission
                    this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                    //// Load combo 
                    this.LoadExemptionCombo();
                    //// Load Exemption Details 
                    this.LoadExemptionDetails();
                    this.ExemptionTypeComboBox.Focus();
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
        /// Handles the Load event of the F29650 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F29650_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                //// Load combo 
                this.LoadExemptionCombo();
                //// Load Exemption details
                this.LoadExemptionDetails();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                //// Set focus on first field
                this.ExemptionTypeComboBox.Focus();
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
        /// Handles the SelectionChangeCommitted event of the ExemptionTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ExemptionTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.tempExemptionId = string.Empty;
                if (this.ExemptionTypeComboBox.SelectedValue != null)
                {
                    //// Set Maximum Value
                    this.SetMaximumValue();
                    //// calculate loss value
                    this.CalculateLoss();

                    if ((this.ExemptionTypeComboBox.SelectedValue != null) && !string.IsNullOrEmpty(this.ExemptionTypeComboBox.Text.Trim()))
                    {
                        this.tempExemptionId = this.ExemptionTypeComboBox.SelectedValue.ToString();
                    }
                }

                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the ExemptionTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ExemptionTypeComboBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if ((this.ExemptionTypeComboBox.SelectedValue != null) && !string.IsNullOrEmpty(this.ExemptionTypeComboBox.Text.Trim()))
                {
                    if (this.tempExemptionId != null && !this.tempExemptionId.Equals(this.ExemptionTypeComboBox.SelectedValue.ToString()))
                    {
                        //// Set Maximum Value
                        this.SetMaximumValue();
                        //// calculate loss value
                        this.CalculateLoss();
                        //// Enable edit button in form master
                        this.EditEnabled();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the LossTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LossTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess && this.LossTextBox.DecimalTextBoxValue >= 0)
                {
                    //// Money value validation
                    decimal maxMoneyValue = 922337203685477.5807M;
                    if (this.LossTextBox.DecimalTextBoxValue > maxMoneyValue)
                    {
                        MessageBox.Show("Loss Value exceeds maximum limit.", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.LossTextBox.Text = "0";
                        this.LossTextBox.Focus();
                        return;
                    }
                    else
                    {
                        //// calculate loss value
                        this.CalculateLoss();
                        if ((this.ExemptionTypeComboBox.SelectedValue != null) && !string.IsNullOrEmpty(this.ExemptionTypeComboBox.Text.Trim()))
                        {
                            this.tempExemptionId = this.ExemptionTypeComboBox.SelectedValue.ToString();
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
        /// Enables the edit button in master form.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EnableEditButtonInMasterForm(object sender, EventArgs e)
        {
            try
            {
                // Enable Save, Cancel button in Form master
                this.EditEnabled();
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
        /// Frozen PictureBox Click Ecent
        /// </summary>
        /// <param name="sender">Frozen Picture Box</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ExemptionPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                // Expand/Collapse form slice
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the ExemptionPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ExemptionPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                // Set tool tip
                this.ExemptionToolTip.SetToolTip(this.ExemptionPictureBox, Utility.GetFormNameSpace(this.Name));
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
            this.ExemptionTypeComboBox.SelectedIndex = -1;
            this.MaximumTextBox.Text = string.Empty;
            this.LossTextBox.Text = string.Empty;
            this.ReductionTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="hasLocked">if set to <c>true</c> [has locked].</param>
        private void LockControls(bool hasLocked)
        {
            this.ExemptionTypeComboBox.Enabled = !hasLocked;
            this.LossTextBox.LockKeyPress = hasLocked;
        }

        /// <summary>
        /// Enables the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableControls(bool enable)
        {
            this.ExemptionTypePanel.Enabled = enable;
            this.MaximumPanel.Enabled = enable;
            this.LossPanel.Enabled = enable;
            this.ReductionValuepanel.Enabled = enable;
        }

        #endregion Controls Visibility

        #region Enable Edit

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (!this.flagLoadOnProcess && this.pageMode == TerraScanCommon.PageModeTypes.View
                && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                //// Event for enable Save, Cancel in form master
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        #endregion Enable Edit

        #region Load Combo

        /// <summary>
        /// Load Exemption Type ComboBox
        /// </summary>
        private void LoadExemptionCombo()
        {
            if (this.keyId > 0)
            {
                //// DB call for Get ExcemptionType
                this.exemptionTypeTable = this.form29650Controller.WorkItem.GetExemptionType(this.keyId);
                //// Add new column "ComboValue"(combination of ExemptionID and ExemptionCode) to get unique row
                this.exemptionTypeTable.ListExemptionType.Columns.Add("ComboValue", typeof(string), "ExemptionID +'|'+ExemptionCode");
                //// Load ExemptionType combobox
                this.ExemptionTypeComboBox.DataSource = this.exemptionTypeTable.ListExemptionType;
                this.ExemptionTypeComboBox.DisplayMember = this.exemptionTypeTable.ListExemptionType.ExemptionCodeColumn.ColumnName;
                this.ExemptionTypeComboBox.ValueMember = "ComboValue";
                this.ExemptionTypeComboBox.SelectedIndex = -1;
            }
        }

        #endregion Load Combo

        #region Load Exemption Details

        /// <summary>
        /// Loads the frozen value details.
        /// </summary>
        private void LoadExemptionDetails()
        {
            if (this.keyId > 0)
            {
                // DB call for f29650_pcget_FrozenValues(EventId)
                this.exemptionTable = this.form29650Controller.WorkItem.GetExemptionDetails(this.keyId);
            }

            // Set retrived values on appropriate controls
            this.SetControlValues();

            if ((this.ExemptionTypeComboBox.SelectedValue != null) && !string.IsNullOrEmpty(this.ExemptionTypeComboBox.Text.Trim()))
            {
                this.tempExemptionId = this.ExemptionTypeComboBox.SelectedValue.ToString();
            }
        }

        #endregion Load Exemption Details

        #region Set Value

        /// <summary>
        /// Sets the Exemption values on appropriate controls.
        /// </summary>
        private void SetControlValues()
        {
            if (this.exemptionTable.GetExemption.Rows.Count > 0)
            {
                // Enable all controls
                this.EnableControls(true);

                this.currentRow = (F29650ExemptionData.GetExemptionRow)this.exemptionTable.GetExemption.Rows[0];

                if (!string.IsNullOrEmpty(this.currentRow.ExemptionCode))
                {
                    string filterExpression = "ComboValue = '" + this.currentRow.ExemptionID + "|" + this.currentRow.ExemptionCode + "'";
                    DataRow[] exemptionCode = this.exemptionTypeTable.ListExemptionType.Select(filterExpression);

                    if (exemptionCode.Length > 0)
                    {
                        this.ExemptionTypeComboBox.SelectedValue = exemptionCode[0]["ComboValue"];
                        this.SetMaximumValue();
                    }
                }
                else
                {
                    this.ExemptionTypeComboBox.SelectedIndex = -1;
                    this.MaximumTextBox.Text = string.Empty;
                }

                if (!this.currentRow.IsLossNull())
                {
                    this.LossTextBox.Text = this.currentRow.Loss.ToString();
                }
                else
                {
                    this.LossTextBox.Text = string.Empty;
                }

                if (!this.currentRow.IsReductionOfValueNull())
                {
                    this.ReductionTextBox.Text = this.currentRow.ReductionOfValue.ToString();
                }
                else
                {
                    this.ReductionTextBox.Text = string.Empty;
                }

                if (!this.currentRow.IsExemptionIDNull())
                {
                    this.exemptionId = this.currentRow.ExemptionID;
                }
                else
                {
                    this.exemptionId = 0;
                }

                if (!this.currentRow.IsExemptionEventIDNull())
                {
                    this.exemptionEventId = this.currentRow.ExemptionEventID;
                }
                else
                {
                    this.exemptionEventId = 0;
                }
            }
            else
            {
                // Disable controls
                this.EnableControls(false);
            }
        }

        #endregion Set Value

        #region Maximum Field

        /// <summary>
        /// Sets the maximum value.
        /// </summary>
        private void SetMaximumValue()
        {
            // Get maximum value for the selected Exemption Type
            string filterCondition = "ComboValue = '" + this.ExemptionTypeComboBox.SelectedValue + "'";
            DataRow[] selectedRow = this.exemptionTypeTable.ListExemptionType.Select(filterCondition);

            if (selectedRow.Length > 0)
            {
                this.MaximumTextBox.Text = selectedRow[0].ItemArray[this.exemptionTable.ListExemptionType.MaximumColumn.Ordinal].ToString();
            }
        }

        #endregion Maximum Field

        #region Loss Calculation

        /// <summary>
        /// Calculates the loss.
        /// </summary>
        private void CalculateLoss()
        {
            // DB call for Loss calculation
            this.ReductionTextBox.Text = this.form29650Controller.WorkItem.GetExemptionLoss(
                                                              this.LossTextBox.DecimalTextBoxValue,
                                                              this.MaximumTextBox.DecimalTextBoxValue).ToString();
        }

        #endregion Loss Calculation

        #region Validation

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>Slice validation Fields</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;
            //// Required Field validation
            if (this.ExemptionTypeComboBox.SelectedValue != null) 
            {
                sliceValidationFields.ErrorMessage = string.Empty;
                sliceValidationFields.RequiredFieldMissing = false;
            }
            else
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }

            if (string.IsNullOrEmpty(this.LossTextBox.Text.Trim()))
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields; 
            }
            else
            {
                // Money value validation
                decimal maxMoneyValue = 922337203685477.5807M;
                if (this.LossTextBox.DecimalTextBoxValue > maxMoneyValue)
                {
                    this.LossTextBox.Focus();
                    sliceValidationFields.RequiredFieldMissing = false;
                }

                sliceValidationFields.ErrorMessage = string.Empty;
            }

            return sliceValidationFields;
        }

        #endregion Validation

        #region Save Frozen Value

        /// <summary>
        /// Saves the frozen details.
        /// </summary>
        private void SaveExemptionDetails()
        {
            F29650ExemptionData.SaveExemptionDataTable exemptionInsertTable = new F29650ExemptionData.SaveExemptionDataTable();
            F29650ExemptionData.SaveExemptionRow insertRow = exemptionInsertTable.NewSaveExemptionRow();

            insertRow.EventID = this.keyId;

            if (this.exemptionEventId > 0)
            {
                insertRow.ExemptionEventID = this.exemptionEventId;
            }
            else
            {
                insertRow.ExemptionEventID = 0;
            }

            if (this.ExemptionTypeComboBox.SelectedValue != null)
            {
                string[] exemptionData = this.ExemptionTypeComboBox.SelectedValue.ToString().Split('|');
                insertRow.ExemptionID = Convert.ToInt32(exemptionData[0].Trim());
                insertRow.ExemptionCode = exemptionData[1].Trim();
            }
            else
            {
                insertRow.ExemptionID = 0;
                insertRow.ExemptionCode = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.LossTextBox.Text.Trim()))
            {
                if (this.LossTextBox.DecimalTextBoxValue > 922337203685477.5807M)
                {
                    this.LossTextBox.Text = "0";
                    this.CalculateLoss();
                }

                insertRow.Loss = this.LossTextBox.DecimalTextBoxValue;
            }
            else
            {
                insertRow.Loss = 0;
            }

            if (!string.IsNullOrEmpty(this.ReductionTextBox.Text.Trim()))
            {
                insertRow.ReductionOfValue = this.ReductionTextBox.DecimalTextBoxValue;
            }
            else
            {
                insertRow.ReductionOfValue = 0;
            }

            exemptionInsertTable.Rows.Add(insertRow);

            // DB call for Save
            this.form29650Controller.WorkItem.SaveExemptionDetails(TerraScanCommon.GetXmlString(exemptionInsertTable), TerraScanCommon.UserId);
        }

        #endregion Save Frozen Value

        private void ExemptionTypeComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if ((this.ExemptionTypeComboBox.SelectedValue != null) && !string.IsNullOrEmpty(this.ExemptionTypeComboBox.Text.Trim()))
                {
                    if (this.tempExemptionId != null && !this.tempExemptionId.Equals(this.ExemptionTypeComboBox.SelectedValue.ToString()))
                    {
                        // Enable edit button in form master
                        this.EditEnabled();
                    }
                }
                //if (this.ExemptionTypeComboBox.SelectedValue!=null)// && this.tempExemptionId != null)
                //{
                //    // Enable edit button in form master
                //    this.EditEnabled();
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

       #endregion Methods
    }
}
