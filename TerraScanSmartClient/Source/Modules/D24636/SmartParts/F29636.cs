
namespace D24636
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.SmartParts;
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.UI.Controls;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using TerraScan.BusinessEntities;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Collections;
    using System.Runtime;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using System.Web.Services.Protocols;

    [SmartPart]
    public partial class F29636 : BaseSmartPart
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
        private F29636Controller form29636Control;

        /// <summary>
        /// BoardOfEqualizationData DataSet
        /// </summary>
        private F29636BOEData boardOfEqualizationData = new F29636BOEData();

        private F29636BOEData.f29636_BOETypeComboTableDataTable TypeComboDataTable = new F29636BOEData.f29636_BOETypeComboTableDataTable();

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

        private string validString = string.Empty;
        private string convertedTime = string.Empty;

        #endregion Variable

        #region Constructor

        public F29636(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.Tag = formNo;
            this.EqualizationpictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.EqualizationpictureBox.Height, this.EqualizationpictureBox.Width, tabText, red, green, blue);
            this.ValuesPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ValuesPictureBox.Height, this.ValuesPictureBox.Width, "Values", red, green, blue);
        }
        public F29636()
        {
            InitializeComponent();
        }


        #endregion

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
        /// For F25000Control
        /// </summary>
        [CreateNew]
        public F29636Controller Form29636Control
        {
            get { return this.form29636Control as F29636Controller; }
            set { this.form29636Control = value; }
        }
        #endregion Property

        private void F29636_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagFormLoad = true;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.PushValueButton.Enabled = true;
                this.CustomizeGridView();
                this.LoadAppraiserCombo();
                this.LoadTypeCombo();
                this.GetBoardOfEqualizationDetails();
                this.GridPanel.Enabled = this.permissionFields.editPermission;
                this.CalculatedValue();
                this.AssessedLabel.ForeColor = Color.White;

                ////khaja added code to fix Bug#487
                if (this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows.Count > 0)
                {
                    // this.ActiveControl = this.ProtestedByTextBox;
                    // this.ActiveControl.Focus();
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

                if (this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows.Count > 0)
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
                this.form29636Control.WorkItem.F29636_DeleteBOEDetails(this.boeId, TerraScanCommon.UserId);
                SliceFormCloseAlert sliceFormCloseAlert;
                sliceFormCloseAlert.FormNo = this.masterFormNo;
                sliceFormCloseAlert.FlagFormClose = false;
                this.OnFormSlice_FormCloseAlert(new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
                this.Cursor = Cursors.Default;
            }
        }
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
                //this.ActiveControl = this.ProtestedByTextBox;
                // this.ActiveControl.Focus();
            }
        }

        #endregion Event Subscription


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

        #region Private Methods

        private void CustomizeGridView()
        {
            this.EqualizationGridView.AutoGenerateColumns = false;
            this.EqualizationGridView.AllowSorting = false;

            DataGridViewColumnCollection columns = this.EqualizationGridView.Columns;
            columns[this.boardOfEqualizationData.f29636_GetBOEGridTable.ValueBreakdownColumn.ColumnName].DataPropertyName = this.boardOfEqualizationData.f29636_GetBOEGridTable.ValueBreakdownColumn.ColumnName;
            columns[this.boardOfEqualizationData.f29636_GetBOEGridTable.AppraisedValueColumn.ColumnName].DataPropertyName = this.boardOfEqualizationData.f29636_GetBOEGridTable.AppraisedValueColumn.ColumnName;
            columns[this.boardOfEqualizationData.f29636_GetBOEGridTable.RequestedValueColumn.ColumnName].DataPropertyName = this.boardOfEqualizationData.f29636_GetBOEGridTable.RequestedValueColumn.ColumnName;
            columns[this.boardOfEqualizationData.f29636_GetBOEGridTable.RecommendedValueColumn.ColumnName].DataPropertyName = this.boardOfEqualizationData.f29636_GetBOEGridTable.RecommendedValueColumn.ColumnName;
            columns[this.boardOfEqualizationData.f29636_GetBOEGridTable.CBEValueColumn.ColumnName].DataPropertyName = this.boardOfEqualizationData.f29636_GetBOEGridTable.CBEValueColumn.ColumnName;
            columns[this.boardOfEqualizationData.f29636_GetBOEGridTable.IsRollColumn.ColumnName].DataPropertyName = this.boardOfEqualizationData.f29636_GetBOEGridTable.IsRollColumn.ColumnName;

            columns[this.boardOfEqualizationData.f29636_GetBOEGridTable.ValueBreakdownColumn.ColumnName].DisplayIndex = 0;
            columns[this.boardOfEqualizationData.f29636_GetBOEGridTable.AppraisedValueColumn.ColumnName].DisplayIndex = 1;
            columns[this.boardOfEqualizationData.f29636_GetBOEGridTable.RequestedValueColumn.ColumnName].DisplayIndex = 2;
            columns[this.boardOfEqualizationData.f29636_GetBOEGridTable.RecommendedValueColumn.ColumnName].DisplayIndex = 3;
            columns[this.boardOfEqualizationData.f29636_GetBOEGridTable.CBEValueColumn.ColumnName].DisplayIndex = 4;
            columns[this.boardOfEqualizationData.f29636_GetBOEGridTable.IsRollColumn.ColumnName].DisplayIndex = 5;
        }

        /// <summary>
        /// Loads the type combo.
        /// </summary>
        private void LoadTypeCombo()
        {
            this.TypeComboDataTable = this.form29636Control.WorkItem.F29636_BOETypeDetails().f29636_BOETypeComboTable;
            this.TypeComboBox.DataSource = this.TypeComboDataTable;
            this.TypeComboBox.DisplayMember = this.TypeComboDataTable.BOETypeColumn.ColumnName;
            this.TypeComboBox.ValueMember = this.TypeComboDataTable.BOETypeIDColumn.ColumnName;
        }

        /// <summary>
        /// Loads the appraiser combo.
        /// </summary>
        private void LoadAppraiserCombo()
        {
            this.userManagementData = this.form29636Control.WorkItem.F9002_GetUserDetails(TerraScanCommon.ApplicationId);
            this.AppraiserCombo.DataSource = this.userManagementData;
            this.AppraiserCombo.DataSource = this.userManagementData.ListUserDetail;
            this.AppraiserCombo.DisplayMember = this.userManagementData.ListUserDetail.DisplayNameColumn.ColumnName;
            this.AppraiserCombo.ValueMember = this.userManagementData.ListUserDetail.UserIDColumn.ColumnName;
        }

        private void SaveEqualization()
        {
            try
            {
                this.boardOfEqualizationData.SaveBOEDetails.Rows.Clear();
                F29636BOEData.SaveBOEDetailsRow dr = this.boardOfEqualizationData.SaveBOEDetails.NewSaveBOEDetailsRow();
                string time = string.Empty;
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
                if (this.TypeComboBox.SelectedIndex > -1)
                {
                    dr.BOETypeID = this.TypeComboBox.SelectedValue.ToString();
                }

                if (!string.IsNullOrEmpty(this.convertedTime.Replace(":", "").Trim()))
                {
                    time = convertedTime;
                   // dr.HearingTime = convertedTime;
                }
                else
                {
                   // dr.HearingTime = string.Empty;
                    time = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.ProtestNumberTextBox.Text.Trim()))
                {
                    dr.ProtestNumber = this.ProtestNumberTextBox.Text.ToString().Trim();
                }

                if (!string.IsNullOrEmpty(this.HearingDateTextBox.Text.Trim()))
                {
                    dr.HearingDate = this.HearingDateTextBox.Text.ToString().Trim();

                    dr.HearingDate = this.HearingDateTextBox.Text.ToString().Trim() + ' ' + time;
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
                string boeValues = TerraScanCommon.GetXmlString(this.boardOfEqualizationData.f29636_GetBOEGridTable);
                this.form29636Control.WorkItem.F29636_SaveBOEDetails(boeElements, boeValues, TerraScanCommon.UserId);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex,ExceptionManager.ActionType.Display, this.ParentForm);
            }

        }

        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();

            if (string.IsNullOrEmpty(this.ProtestedByTextBox.Text.Trim()))
            {
                sliceValidationFields.ErrorMessage = (SharedFunctions.GetResourceString("RequiredField"));
                sliceValidationFields.FormNo = formNo;
                sliceValidationFields.RequiredFieldMissing = false;
            }
            else if(this.TypeComboBox.SelectedIndex == -1)
            {
                sliceValidationFields.ErrorMessage = (SharedFunctions.GetResourceString("RequiredField"));
                sliceValidationFields.FormNo = formNo;
                sliceValidationFields.RequiredFieldMissing = false;
                this.TypeComboBox.Focus();
            }
            else
            {
                sliceValidationFields.ErrorMessage = string.Empty;
                sliceValidationFields.FormNo = formNo;
                sliceValidationFields.RequiredFieldMissing = false;
            }

            return sliceValidationFields;
        }
        private void GetBoardOfEqualizationDetails()
        {
            this.boardOfEqualizationData = this.form29636Control.WorkItem.F29636_GetBOEDetails(this.keyId);

            this.BindControlValues();
        }

        private void CalculatedValue()
        {
            try
            {
                this.assessedSumValue = 0;
                this.apellantsSumValue = 0;
                this.stipulatedSumValue = 0;
                this.boeSumValue = 0;

                for (int i = 0; i < this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows.Count; i++)
                {
                    //if (this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.ValueBreakdownColumn.ColumnName].ToString() != "Market Land Value")
                    //{
                    if (!string.IsNullOrEmpty(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.AppraisedValueColumn.ColumnName].ToString()))
                    {
                        if (i == 0)
                        {
                            Double tempDecimal;
                            Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEGridTable.AppraisedValueColumn.ColumnName].ToString().Replace("$", ""), out tempDecimal);
                            Double outDecimal = 0;
                            Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.AppraisedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                            this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.AppraisedValueColumn.ColumnName] = outDecimal;
                            this.assessedSumValue = this.assessedSumValue + Convert.ToDouble(outDecimal);
                            //if (tempDecimal < 0 || tempDecimal > 0)
                            //{
                            //    i = 1;
                            //}
                        }
                        else
                        {
                            if (i != 3)
                            {
                                Double outDecimal = 0;
                                Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.AppraisedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                                this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.AppraisedValueColumn.ColumnName] = outDecimal;
                                this.assessedSumValue = this.assessedSumValue + Convert.ToDouble(outDecimal);
                            }

                        }
                    }
                }



                for (int i = 0; i < this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows.Count; i++)
                {

                    if (!string.IsNullOrEmpty(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.RequestedValueColumn.ColumnName].ToString()))
                    {

                        if (i == 0)
                        {
                            Double tempDecimal;
                            Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEGridTable.RequestedValueColumn.ColumnName].ToString().Replace("$", ""), out tempDecimal);
                            Double outDecimal = 0;
                            Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.RequestedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                            this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.RequestedValueColumn.ColumnName] = outDecimal;
                            this.apellantsSumValue = this.apellantsSumValue + Convert.ToDouble(outDecimal);

                            //Commented by purushotham 
                            //if (tempDecimal < 0 || tempDecimal > 0)
                            //{
                            //    i = 1;
                            //}
                        }
                        else
                        {
                            if (i != 3)
                            {
                                Double outDecimal = 0;
                                Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.RequestedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                                this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.RequestedValueColumn.ColumnName] = outDecimal;
                                this.apellantsSumValue = this.apellantsSumValue + Convert.ToDouble(outDecimal);
                            }
                        }



                    }
                }
                for (int i = 0; i < this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.RecommendedValueColumn.ColumnName].ToString()))
                    {
                        if (i == 0)
                        {
                            Double tempDecimal;
                            Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEGridTable.RecommendedValueColumn.ColumnName].ToString().Replace("$", ""), out tempDecimal);
                            Double outDecimal = 0;
                            Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.RecommendedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                            this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.RecommendedValueColumn.ColumnName] = outDecimal;
                            this.stipulatedSumValue = this.stipulatedSumValue + Convert.ToDouble(outDecimal);
                            //if (tempDecimal < 0 || tempDecimal > 0)
                            //{
                            //    i = 1;
                            //}
                        }
                        else
                        {
                            if (i != 3)
                            {
                                Double outDecimal = 0;
                                Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.RecommendedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                                this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.RecommendedValueColumn.ColumnName] = outDecimal;
                                this.stipulatedSumValue = this.stipulatedSumValue + Convert.ToDouble(outDecimal);
                            }
                        }

                    }
                }
                for (int i = 0; i < this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.CBEValueColumn.ColumnName].ToString()))
                    {
                        if (i == 0)
                        {
                            Double tempDecimal;
                            Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEGridTable.CBEValueColumn.ColumnName].ToString().Replace("$", ""), out tempDecimal);
                            Double outDecimal = 0;
                            Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.CBEValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                            this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.CBEValueColumn.ColumnName] = outDecimal;
                            this.boeSumValue = this.boeSumValue + Convert.ToDouble(outDecimal);
                            //if (tempDecimal < 0 || tempDecimal > 0)
                            //{
                            //    i = 1;
                            //}
                        }
                        else
                        {
                            if (i != 3)
                            {
                                Double outDecimal = 0;
                                Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.CBEValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                                this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.CBEValueColumn.ColumnName] = outDecimal;
                                this.boeSumValue = this.boeSumValue + Convert.ToDouble(outDecimal);
                            }
                        }

                    }
                }


                this.AssessedLabel.Text = this.assessedSumValue.ToString("$#,##0");
                this.AppellantLabel.Text = this.apellantsSumValue.ToString("$#,##0");
                this.StipulatedLabel.Text = this.stipulatedSumValue.ToString("$#,##0");
                this.BOELabel.Text = this.boeSumValue.ToString("$#,##0");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void BindControlValues()
        {
            this.validString = string.Empty;
            if (this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.BOEIDColumn.ColumnName].ToString()))
                {
                    this.boeId = Convert.ToInt32(this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.BOEIDColumn.ColumnName].ToString());
                }
                string value = string.Empty; 
                 string tt=string.Empty;
                if (!string.IsNullOrEmpty(this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.HearingDateColumn.ColumnName].ToString()))
                {
                    DateTime tTime = Convert.ToDateTime(this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.HearingDateColumn.ColumnName].ToString());
                    value = tTime.ToString("hh:mm:ss");
                    tt = tTime.ToString("tt");
                }
                
                //this.HearingDateTextBox_TextChanged(null,null);
               // this.HearingDateTextBox.TextChanged += new System.EventHandler(this.HearingDateTextBox_TextChanged);

                this.ProtestedByTextBox.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.ProtestedByColumn.ColumnName].ToString();
                this.AddressOneTextBox.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Address1Column.ColumnName].ToString();
                this.AddresTwoTextBox.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Address2Column.ColumnName].ToString();
                this.CityTextBox.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.CityColumn.ColumnName].ToString();
                this.StateTextBox.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.StateColumn.ColumnName].ToString();
                this.ZipTextBox.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.ZipColumn.ColumnName].ToString();
                this.PhoneNumberTextBox.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.PhoneNumberColumn.ColumnName].ToString();
                this.EmailAddressTextBox.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.EmailAddressColumn.ColumnName].ToString();
                this.ProtestNumberTextBox.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.ProtestNumberColumn.ColumnName].ToString();
                this.HearingDateTextBox.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.HearingDateColumn.ColumnName].ToString();
                this.ActionDateTextBox.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.ActionDateColumn.ColumnName].ToString();
                this.ClosedDateTextBox.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.ClosedDateColumn.ColumnName].ToString();
                this.AppraiserCombo.SelectedValue = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.UserIDColumn.ColumnName].ToString();
                if (!string.IsNullOrEmpty(this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.BOETypeIDColumn.ColumnName].ToString()))
                {
                    this.TypeComboBox.SelectedValue = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.BOETypeIDColumn.ColumnName].ToString();
                }
                else
                {
                    this.TypeComboBox.SelectedIndex = -1;
                }

                this.ParcelNumberLinkLabel.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.ParcelNumberColumn.ColumnName].ToString();
                this.RollYearTextBox.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.RollYearColumn.ColumnName].ToString();
                this.MapNumberTextBox.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.MapNumberColumn.ColumnName].ToString();
                if (!string.IsNullOrEmpty(this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.PrimaryOwnerColumn.ColumnName].ToString()))
                {
                    this.PropertyOwnerLinkLabel.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.PrimaryOwnerColumn.ColumnName].ToString();
                }
                else
                {
                    this.PropertyOwnerLinkLabel.Text = "««  »»";
                }
                this.PhoneNoTextBox.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.OwnerPhoneNumberColumn.ColumnName].ToString();
                this.SitusTextBox.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.SitusColumn.ColumnName].ToString();
                this.ClassCodeTextBox.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.ClassCodeColumn.ColumnName].ToString();
                this.LegalTextBox.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.LegalColumn.ColumnName].ToString();
                if (!string.IsNullOrEmpty(value))
               // if (!string.IsNullOrEmpty(this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.HearingTimeColumn.ColumnName].ToString()))
                { 
                    //FROM DB FUNCTIONLAITY                    
                   // string[] HearingTimeFromDB = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.HearingTimeColumn.ColumnName].ToString().Split(':');

                    this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.HearingTimeColumn.ColumnName] = value;
                    string[] HearingTimeFromDB = value.ToString().Split(':');
                    int tempTime;
                    int.TryParse(HearingTimeFromDB[0].Trim(), out tempTime);
                    if (tempTime == 12)
                    {
                        this.PMRadioButton.Checked = true;
                        this.HearingTimeTextBox.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.HearingTimeColumn.ColumnName].ToString();
                    }
                    else if (tempTime == 0)
                    {
                        this.AMRadioButton.Checked = true;
                        this.HearingTimeTextBox.Text = "12" + ":" + HearingTimeFromDB[1].ToString().Trim() + ":" + HearingTimeFromDB[2].ToString().Trim();
                    }
                    else if (tempTime <= 11)
                    {
                        this.AMRadioButton.Checked = true;
                        this.HearingTimeTextBox.Text = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.HearingTimeColumn.ColumnName].ToString();
                    }
                    else if (tempTime > 12)
                    {
                        this.PMRadioButton.Checked = true;
                        tempTime = tempTime - 12;
                        string exactTime = tempTime.ToString();
                        if (tempTime < 10)
                        {
                            exactTime = "0" + tempTime;
                        }
                        this.HearingTimeTextBox.Text = exactTime + ":" + HearingTimeFromDB[1].ToString().Trim() + ":" + HearingTimeFromDB[2].ToString().Trim();
                    }
                }
                else
                {

                    this.HearingTimeTextBox.Text = string.Empty;
                }
                this.validString = this.HearingTimeTextBox.Text;
               this.convertedTime = this.validString;
                this.EnablingTime();
                //this.HearingDateTextBox_TextChanged(null, null);
                if (!string.IsNullOrEmpty(tt) && tt.Trim().ToUpper().Equals("PM"))
                {
                    this.PMRadioButton.Checked = true;
                }
                else if (!string.IsNullOrEmpty(tt) && tt.Trim().ToUpper().Equals("AM"))
                {
                    this.AMRadioButton.Checked = true;
                }
                this.LockControls(true);
            }
            else
            {
                this.ClearControls();
                this.LockControls(false);
            }

            this.EqualizationGridView.DataSource = this.boardOfEqualizationData.f29636_GetBOEGridTable.DefaultView;
        }

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

            this.TypePanel.Enabled = lockControl;
            this.HearingTimePanel.Enabled = lockControl;

            this.RollYearPanel.Enabled = lockControl;
            this.MapNumberPanel.Enabled = lockControl;
            this.PhoneNoPanel.Enabled = lockControl;
            this.SitusPanel.Enabled = lockControl;
            this.ClassCodePanel.Enabled = lockControl;
            this.LegalPanel.Enabled = lockControl;

            this.PushValuePanel.Enabled = lockControl;
            //this.HearingDateTextBox_TextChanged(null, null);
            this.EnablingTime();
        }


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
            this.ClassCodeTextBox.Text = string.Empty;
            this.LegalTextBox.Text = string.Empty;
            this.ZipTextBox.Text = string.Empty;
            this.HearingDateTextBox.Text = string.Empty;
            this.ActionDateTextBox.Text = string.Empty;
            this.ClosedDateTextBox.Text = string.Empty;
            ////Deselect Combo
            this.AppraiserCombo.SelectedIndex = 0;

            this.HearingTimeTextBox.Text = string.Empty;
            ////Deselect Combo
            this.TypeComboBox.SelectedIndex = -1;
        }

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

            this.TypePanel.Enabled = !controlLock;
            this.HearingTimePanel.Enabled = !controlLock;
            this.EnablingTime();
        }

        #endregion

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


        #region Grid Events

        /// <summary>
        /// Handles the CellFormatting event of the EqualizationGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void EqualizationGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Double outValue;
            if (e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.AppraisedValue.Name].Index) || e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.RequestedValue.Name].Index) || e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.RecommendedValue.Name].Index) || e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.CBEValue.Name].Index))
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
                      e.Value = outValue.ToString("$#,##0");
                       
                        e.FormattingApplied = true;
                    }
                    else
                    {
                        e.Value = "$ 0";
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
                this.EqualizationGridView.Rows[4].Cells[this.AppraisedValue.Name].ReadOnly = true;
                this.EqualizationGridView.Rows[4].Cells[this.ValueBreakDown.Name].ReadOnly = true;
                this.EqualizationGridView.Rows[4].Cells[this.RequestedValue.Name].ReadOnly = true;
                this.EqualizationGridView.Rows[4].Cells[this.RecommendedValue.Name].ReadOnly = true;
                this.EqualizationGridView.Rows[4].Cells[this.CBEValue.Name].ReadOnly = true;
                this.EqualizationGridView.Rows[4].Cells[this.IsRoll.Name].ReadOnly = true;

                this.EqualizationGridView.Rows[4].Cells[this.AppraisedValue.Name].Value = string.Empty;
                this.EqualizationGridView.Rows[4].Cells[this.ValueBreakDown.Name].Value = string.Empty;
                this.EqualizationGridView.Rows[4].Cells[this.RequestedValue.Name].Value = string.Empty;
                this.EqualizationGridView.Rows[4].Cells[this.RecommendedValue.Name].Value = string.Empty;
                this.EqualizationGridView.Rows[4].Cells[this.CBEValue.Name].Value = string.Empty;
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
                //Modifed to 
                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                if (EqualizationGridView.CurrentCell.ColumnIndex > 0) //Desired Column
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
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
                if (e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.AppraisedValue.Name].Index))
                {
                    this.assessedSumValue = 0;

                    string rpvalue = this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[e.RowIndex][this.boardOfEqualizationData.f29636_GetBOEGridTable.AppraisedValueColumn.ColumnName].ToString().Replace("$", "").Replace(",", "").Trim();
                    int rplen = rpvalue.Length;
                    int rppos = rpvalue.IndexOf(".");
                    if (rplen > 15)
                    {
                        if (rppos > 15)
                        {
                            this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[e.RowIndex][this.boardOfEqualizationData.f29636_GetBOEGridTable.AppraisedValueColumn.ColumnName] = 0;
                        }

                        if (rppos == -1)
                        {
                            this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[e.RowIndex][this.boardOfEqualizationData.f29636_GetBOEGridTable.AppraisedValueColumn.ColumnName] = 0;
                        }
                    }

                    for (int i = 0; i < this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows.Count; i++)
                    {

                        if (i == 0)
                        {
                            Double tempDecimal;
                            Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEGridTable.AppraisedValueColumn.ColumnName].ToString().Replace("$", ""), out tempDecimal);
                            Double outDecimal = 0;
                            Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.AppraisedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                            this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.AppraisedValueColumn.ColumnName] = outDecimal;
                            this.assessedSumValue = this.assessedSumValue + Convert.ToDouble(outDecimal);
                            //if (tempDecimal < 0 || tempDecimal > 0)
                            //{
                            //    i = 1;
                            //}
                        }
                        else
                        {
                            if (i != 3)
                            {
                                Double outDecimal = 0;
                                Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.AppraisedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                                this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.AppraisedValueColumn.ColumnName] = outDecimal;
                                this.assessedSumValue = this.assessedSumValue + Convert.ToDouble(outDecimal);
                            }
                        }

                    }

                    this.AssessedLabel.Text = this.assessedSumValue.ToString("$#,##0");
                    
                    this.AssessedLabel.ForeColor = Color.White;
                }
                else if (e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.RequestedValue.Name].Index))
                {
                    this.apellantsSumValue = 0;
                    string rpvalue = this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[e.RowIndex][this.boardOfEqualizationData.f29636_GetBOEGridTable.RequestedValueColumn.ColumnName].ToString().Replace("$", "").Replace(",", "").Trim();
                    int rplen = rpvalue.Length;
                    int rppos = rpvalue.IndexOf(".");
                    if (rplen > 15)
                    {
                        if (rppos > 15)
                        {
                            this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[e.RowIndex][this.boardOfEqualizationData.f29636_GetBOEGridTable.RequestedValueColumn.ColumnName] = 0;
                        }

                        if (rppos == -1)
                        {
                            this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[e.RowIndex][this.boardOfEqualizationData.f29636_GetBOEGridTable.RequestedValueColumn.ColumnName] = 0;
                        }
                    }

                    for (int i = 0; i < this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.RequestedValueColumn.ColumnName].ToString().Trim()))
                        {
                            if (i == 0)
                            {
                                Double tempDecimal;
                                Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEGridTable.RequestedValueColumn.ColumnName].ToString().Replace("$", ""), out tempDecimal);
                                Double outDecimal = 0;
                                Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.RequestedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                                this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.RequestedValueColumn.ColumnName] = outDecimal;
                                this.apellantsSumValue = this.apellantsSumValue + Convert.ToDouble(outDecimal);
                                //if (tempDecimal < 0 || tempDecimal > 0)
                                //{
                                //    i = 1;
                                //}
                            }
                            else
                            {
                                Double outDecimal = 0;
                                Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.RequestedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                                this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.RequestedValueColumn.ColumnName] = outDecimal;
                                this.apellantsSumValue = this.apellantsSumValue + Convert.ToDouble(outDecimal);
                            }

                            //Double outDecimal = 0;
                            //Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.RequestedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                            //this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.RequestedValueColumn.ColumnName] = outDecimal;
                            ////if (this.boardOfEqualizationData.GetBOEValues.Rows[i][this.boardOfEqualizationData.GetBOEValues.ValueBreakdownColumn.ColumnName].ToString() != "Market Land Value")
                            ////{
                            //    this.apellantsSumValue = this.apellantsSumValue + Convert.ToDouble(outDecimal);
                            //    if (i == 1)
                            //    {
                            //        Double tempDecimal;
                            //        Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[1][this.boardOfEqualizationData.GetBOEValues.RequestedValueColumn.ColumnName].ToString().Replace("$", ""), out tempDecimal);
                            //        if (tempDecimal < 0 || tempDecimal > 0)
                            //        {
                            //            Double tempvalue;
                            //            Double.TryParse(this.boardOfEqualizationData.GetBOEValues.Rows[0][this.boardOfEqualizationData.GetBOEValues.RequestedValueColumn.ColumnName].ToString().Replace("$", ""), out tempvalue);
                            //            this.apellantsSumValue = this.apellantsSumValue - tempvalue;
                            //        }

                            //    }
                            ////}
                        }
                    }

                    this.AppellantLabel.Text =  this.apellantsSumValue.ToString("$#,##0");
                }
                else if (e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.RecommendedValue.Name].Index))
                {
                    this.stipulatedSumValue = 0;
                    string rpvalue = this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[e.RowIndex][this.boardOfEqualizationData.f29636_GetBOEGridTable.RecommendedValueColumn.ColumnName].ToString().Replace("$", "").Replace(",", "").Trim();
                    int rplen = rpvalue.Length;
                    int rppos = rpvalue.IndexOf(".");
                    if (rplen > 15)
                    {
                        if (rppos > 15)
                        {
                            this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[e.RowIndex][this.boardOfEqualizationData.f29636_GetBOEGridTable.RecommendedValueColumn.ColumnName] = 0;
                        }

                        if (rppos == -1)
                        {
                            this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[e.RowIndex][this.boardOfEqualizationData.f29636_GetBOEGridTable.RecommendedValueColumn.ColumnName] = 0;
                        }
                    }

                    for (int i = 0; i < this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.RecommendedValueColumn.ColumnName].ToString().Trim()))
                        {
                            if (i == 0)
                            {
                                Double tempDecimal;
                                Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEGridTable.RecommendedValueColumn.ColumnName].ToString().Replace("$", ""), out tempDecimal);
                                Double outDecimal = 0;
                                Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.RecommendedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                                this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.RecommendedValueColumn.ColumnName] = outDecimal;
                                this.stipulatedSumValue = this.stipulatedSumValue + Convert.ToDouble(outDecimal);
                                //if (tempDecimal < 0 || tempDecimal > 0)
                                //{
                                //    i = 1;
                                //}
                            }
                            else
                            {
                                Double outDecimal = 0;
                                Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.RecommendedValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                                this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.RecommendedValueColumn.ColumnName] = outDecimal;
                                this.stipulatedSumValue = this.stipulatedSumValue + Convert.ToDouble(outDecimal);
                            }                           
                        }
                    }

                    this.StipulatedLabel.Text =  this.stipulatedSumValue.ToString("$#,##0");
                }
                else if (e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.CBEValue.Name].Index))
                {
                    this.boeSumValue = 0;
                    string rpvalue = this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[e.RowIndex][this.boardOfEqualizationData.f29636_GetBOEGridTable.CBEValueColumn.ColumnName].ToString().Replace("$", "").Replace(",", "").Trim();
                    int rplen = rpvalue.Length;
                    int rppos = rpvalue.IndexOf(".");
                    if (rplen > 15)
                    {
                        if (rppos > 15)
                        {
                            this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[e.RowIndex][this.boardOfEqualizationData.f29636_GetBOEGridTable.CBEValueColumn.ColumnName] = 0;
                        }

                        if (rppos == -1)
                        {
                            this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[e.RowIndex][this.boardOfEqualizationData.f29636_GetBOEGridTable.CBEValueColumn.ColumnName] = 0;
                        }
                    }

                    for (int i = 0; i < this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.CBEValueColumn.ColumnName].ToString().Trim()))
                        {
                            if (i == 0)
                            {
                                Double tempDecimal;
                                Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEGridTable.CBEValueColumn.ColumnName].ToString().Replace("$", ""), out tempDecimal);
                                Double outDecimal = 0;
                                Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.CBEValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                                this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.CBEValueColumn.ColumnName] = outDecimal;
                                this.boeSumValue = this.boeSumValue + Convert.ToDouble(outDecimal);
                                //if (tempDecimal < 0 || tempDecimal > 0)
                                //{
                                //    i = 1;
                                //}
                            }
                            else
                            {
                                Double outDecimal = 0;
                                Double.TryParse(this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.CBEValueColumn.ColumnName].ToString().Replace("$", ""), out outDecimal);
                                this.boardOfEqualizationData.f29636_GetBOEGridTable.Rows[i][this.boardOfEqualizationData.f29636_GetBOEGridTable.CBEValueColumn.ColumnName] = outDecimal;
                                this.boeSumValue = this.boeSumValue + Convert.ToDouble(outDecimal);
                            }                           
                        }
                    }

                    this.BOELabel.Text = this.boeSumValue.ToString("$#,##0");
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
                string returnMessage = string.Empty;
                returnMessage = this.form29636Control.WorkItem.F29636_PushBOEDetails(this.boeId, TerraScanCommon.UserId);
                if (!string.IsNullOrEmpty(returnMessage))
                {
                    MessageBox.Show(returnMessage, "Values pushed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
                formInfo.optionalParameters[0] = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.ParcelIDColumn].ToString();
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
                formInfo.optionalParameters[0] = this.boardOfEqualizationData.f29636_GetBOEDetailsTable.Rows[0][this.boardOfEqualizationData.f29636_GetBOEDetailsTable.OwnerIDColumn].ToString();
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion Parcel Link

        private void HearingTimeTextBox_TextChanged(object sender, EventArgs e)
        {
            this.TextValueChanged(sender, e);             

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
        /// <summary>
        /// PMRadioButton_CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PMRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (PMRadioButton.Checked)
            {
                AMRadioButton.Checked = false;
            }
            PMRadioButton.TabStop = true;
            this.FormatInputTime();
            this.TextValueChanged(sender, e);
        }
        /// <summary>
        /// AMRadioButton_CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AMRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (AMRadioButton.Checked)
            {
                PMRadioButton.Checked = false;
            }
            AMRadioButton.TabStop = true;
            this.FormatInputTime();
            this.TextValueChanged(sender, e);
        }

        internal void EnablingTime()
        {
            if (!string.IsNullOrEmpty(this.HearingDateTextBox.Text))
            {
                this.HearingTimePanel.Enabled = true;
            }
            else
            {
                this.HearingTimePanel.Enabled = false;
            }
            this.HearingTimeTextBox.Text = this.validString.Trim();
        }
        /// <summary>
        /// HearingDateTextBox_TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HearingDateTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.HearingDateTextBox.Text))
            {
                this.HearingTimePanel.Enabled = true;               
            }
            else
            {
                this.HearingTimePanel.Enabled = false;               
            }
            this.FormatInputTime();
            this.HearingTimeTextBox.Text = this.validString.Trim();
            this.TextValueChanged(sender, e);
        }
        /// <summary>
        /// HearingTimeTextBox_Leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HearingTimeTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.HearingTimeTextBox.Text.Trim()))
                {

                    bool returnValue = ValidHearingTime();
                    if (returnValue)
                    {
                        MessageBox.Show("Please enter Time between 01:00 to 12:59", "Terrascan T2 -InValid Time Formt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.HearingTimeTextBox.Text = string.Empty;
                        this.HearingTimeTextBox.Focus();
                    }
                    else
                    {
                        this.HearingTimeTextBox.Text = this.validString.Trim();
                    }

                    this.FormatInputTime();
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// FormatInputTime
        /// </summary>
        private void FormatInputTime()
        {
            if (!string.IsNullOrEmpty(this.HearingTimeTextBox.Text.Replace(":", "").Trim()))
            {
                this.convertedTime = string.Empty;
                string[] HearingTime = this.HearingTimeTextBox.Text.Split(':');
                this.convertedTime = this.HearingTimeTextBox.Text;
                if (this.AMRadioButton.Checked)
                {
                    if (!string.IsNullOrEmpty(HearingTime[0]))
                    {
                        if (HearingTime[0].ToString().Trim() == "12")
                        {
                            //Commented for issue TSCO - 24636 NE Board Ruling - Remove seconds from Hearing Time 
                            // this.convertedTime = "00:" + HearingTime[1].ToString().Trim() + ":" + HearingTime[2].ToString().Trim();
                            this.convertedTime = "00:" + HearingTime[1].ToString().Trim();
                        }
                    }
                }
                else if (this.PMRadioButton.Checked)
                {
                    if (!string.IsNullOrEmpty(HearingTime[0]))
                    {
                        int time;
                        int.TryParse(HearingTime[0].Trim(), out time);
                        if (time != 12)
                        {
                            time = time + 12;
                            //Commented for issue TSCO - 24636 NE Board Ruling - Remove seconds from Hearing Time 
                            //this.convertedTime = time + ":" + HearingTime[1].ToString().Trim() + ":" + HearingTime[2].ToString().Trim();
                            this.convertedTime = time + ":" + HearingTime[1].ToString().Trim();
                        }
                    }
                }

            }
            else
            {
                this.convertedTime = string.Empty;
            }
        }
        /// <summary>
        /// ValidHearingTime
        /// </summary>
        /// <returns></returns>
        private bool ValidHearingTime()
        {
            bool isInvalid = false;
            char[] delimiterChars = { ':' };
            string[] words = this.HearingTimeTextBox.Text.Split(delimiterChars);
            var leng = words.Length;
            var text = this.HearingTimeTextBox.Text;
            // Modified for the issue TSCO - 24636 NE Board Ruling - Remove seconds from Hearing Time 
            int validHours, validMins;
            if (words.Length.Equals(2))
            {
                if (words[0].ToString().Trim().Length > 0)
                {
                    string hours = string.Empty;
                    if (words[0].ToString().Trim().Length == 2)
                    {
                        hours = words[0].ToString();
                    }
                    else
                    {
                        hours = "0" + words[0].ToString().Trim();
                    }
                    int.TryParse(hours, out validHours);
                    if (validHours > 0 && validHours <= 12)
                    {                        
                        validString = hours.Trim();
                    }
                    else
                    {
                        return isInvalid = true;
                    }
                }
                else
                {
                    return isInvalid = true;
                }
                if (words[1].ToString().Trim().Length > 0)
                {
                    string mins = string.Empty;
                    if (words[1].ToString().Trim().Length == 2)
                    {
                        mins = words[1].ToString().Trim();
                    }
                    else
                    {
                        mins = "0" + words[1].ToString().Trim();
                    }
                    int.TryParse(mins, out validMins);
                    if (validMins >= 0 && validMins < 60)
                    {
                        validString = validString + ":" + mins.Trim();
                    }
                    else
                    {
                        return isInvalid = true;
                    }
                }
                else
                {
                    return isInvalid = true;
                }
                // Commented for the issue TSCO - 24636 NE Board Ruling - Remove seconds from Hearing Time 
                //if (words[2].ToString().Trim().Length > 0)
                //{
                //    string sec = string.Empty;
                //    if (words[2].ToString().Trim().Length == 2)
                //    {
                //        sec = words[2].ToString();
                //    }
                //    else
                //    {
                //        sec = "0" + words[2].ToString().Trim();
                //    }
                //    int.TryParse(sec, out validSec);
                //    if (validSec >= 0 && validSec < 60)
                //    {
                //        validString = validString + ":" + sec.Trim();
                //    }
                //    else
                //    {
                //        return isInvalid = true;
                //    }
                //}
                //else
                //{
                //    return isInvalid = true;
                //}

            }
            return false;
        }
        /// <summary>
        /// EqualizationGridView_CellEnter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EqualizationGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.EqualizationGridView.Rows.Count > 0)
                {
                    if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                    {
                        if (!string.IsNullOrEmpty(this.EqualizationGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                        {
                            
                        }
                        if (e.RowIndex == 3)
                        {
                            if (e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.RequestedValue.Name].Index) || e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.RecommendedValue.Name].Index) || e.ColumnIndex.Equals(this.EqualizationGridView.Columns[this.CBEValue.Name].Index))
                            {
                                this.EqualizationGridView.Rows[3].Cells[e.ColumnIndex].ReadOnly = true;
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void EqualizationGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
                if (this.EqualizationGridView.Rows.Count > 0)
                {
                    //if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                    //{
                    //    if (!string.IsNullOrEmpty(this.EqualizationGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                    //    {
                    //      string value= this.EqualizationGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    //      value = value.Replace("$", "");
                    //      this.EqualizationGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Convert.ToDouble(this.EqualizationGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    //     // this.EqualizationGridView.Columns[this.boardOfEqualizationData.f29636_GetBOEGridTable.CBEValueColumn.ColumnName].HasDefaultCellStyle("#,##0");
                    //    }
                    //}
                }
        
        }

        //Added to select Complete Text on enter
        private void HearingTimeTextBox_Enter(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate { SetMaskedTextBoxSelectAll((MaskedTextBox)sender); });
        }

        private void SetMaskedTextBoxSelectAll(MaskedTextBox txtbox)
        {
           var txt= txtbox.Text.Replace("_","");
           txt = txt.Replace(":", "");
           if (!string.IsNullOrEmpty(txt.Trim()))
           {
               txtbox.SelectAll();
           }
           
        }
        //Added to not allow strings
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

    }
}
