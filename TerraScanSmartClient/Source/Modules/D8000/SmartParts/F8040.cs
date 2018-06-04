//--------------------------------------------------------------------------------------------
// <copyright file="F8040.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8040.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10 Oct 06        JYOTHI              Created
// 1 Nov 06         JAYANTHI            Event Published for F8046
// 8 Dec 06         JAYANTHI            Bug Fixing
//*********************************************************************************/
namespace D8000
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
    /// F8040 class file
    /// </summary>
    [SmartPart]
    public partial class F8040 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// Used to Store actualTimeDataTableRowsCount
        /// </summary>
        private int actualTimeDataTableRowsCount;

        /// <summary>
        /// dateVal
        /// </summary>
        private bool dateVal; ////(New)

        /// <summary>
        /// isShift
        /// </summary>
        private bool isShift; ////(New)

        /// <summary>
        /// isShifts
        /// </summary>
        private bool isShifts; ////(New)

        /// <summary>
        /// isDateSelected
        /// </summary>
        private bool isDateSelected; ////(New)

        /// <summary>
        /// endisShifts
        /// </summary>
        private bool endisShifts; ////(New)

        /// <summary>
        /// It store eventid or Work order id based on form master
        /// </summary>
        private int keyId;

        /// <summary>
        /// DatePicker object
        /// </summary>
        private System.Windows.Forms.DateTimePicker validDate = new System.Windows.Forms.DateTimePicker();

        /// <summary>
        /// Time grid data source
        /// </summary>
        private F8040TimeData.ListTimeDataTable timeDataTable = new F8040TimeData.ListTimeDataTable();

        /// <summary>
        /// form8104Control Controller
        /// </summary>
        private F8040Controller form8040Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// formMasterPermissionEdit Local variable.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// It contains UserId which was passed from Form master
        /// </summary>
        private int userId;

        /// <summary>
        /// Integer
        /// </summary>
        private int formNo;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// Used to store time resource data
        /// </summary>
        private F8040TimeData.ListTimeResourceDataTable timeResourceDataTable = new F8040TimeData.ListTimeResourceDataTable();

        /// <summary>
        /// To Load all users both active and inactive users in Resource combo
        /// /// </summary>
        private F8040TimeData.ListTimeResourceDataTable allUserDataSet = new F8040TimeData.ListTimeResourceDataTable();
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8104"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F8040(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.formNo = formNo;
            this.keyId = keyID;
            this.TimePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.Height, 42, tabText, red, green, blue);
        }

        #endregion

        #region Event Publication
        /// <summary>
        /// F8042 has to get refeshed when record added in this local grid
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_TimeFooterCountRefresh, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_TimeFooterCountRefresh;

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

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F8040 control.
        /// </summary>
        /// <value>The F8040 control.</value>
        [CreateNew]
        public F8040Controller Form8040Control
        {
            get { return this.form8040Control as F8040Controller; }
            set { this.form8040Control = value; }
        }
        #endregion

        #region Event Subscription

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

                    DataView tempDataView = new DataView(this.timeDataTable.Copy(), string.Concat("EmptyRecord$ = False AND ", this.timeDataTable.TotalHoursColumn.ColumnName, " IS NULL OR ", this.timeDataTable.TotalHoursColumn.ColumnName, " = '0.00'"), "", DataViewRowState.CurrentRows);
                    if (tempDataView.Count > 0)
                    {
                        sliceValidationFields.ErrorMessage = "In time slice hours should be greater than zero";
                    }

                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
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
        /// Called when [D9030_ F9030_ enable new method].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, DataEventArgs<int> eventArgs)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.New;
            this.ClearTimeDetails();
            F8040TimeData tempTimeDetailsDataSet = new F8040TimeData();
            this.TimeGridView.DataSource = tempTimeDetailsDataSet.ListTime;
            if (this.TimeGridView.CurrentCell != null)
            {
                this.TimeGridView.CurrentCell.Selected = false;
            }

            this.DisableControls(false);
        }

        /// <summary>
        /// Called when [D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.TimeGridView.CurrentCell != null)
            {
                this.TimeGridView.CurrentCell.Selected = true;
            }

            this.ClearTimeDetails();
            this.PopulateTime();
            ////this.DisableHeaderControls(this.slicePermissionField.newPermission);
            this.DisableControls(true);
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
                    this.UpdateTimeGrid(sender, eventArgs);
                    ////this.DisableHeaderControls(this.slicePermissionField.newPermission);
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            else
            {
                this.DisableControls(true);
                this.ClearTimeDetails();
                this.PopulateTime();
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

                    this.userId = eventArgs.Data.SelectUserId;
                    this.TimeGridView.ReadOnly = (!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);

                    if (this.actualTimeDataTableRowsCount > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
                    }
                    else
                    {
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            eventArgs.Data.FlagInvalidSliceKey = true;
                        }
                    }

                    ////if (this.timeDataTable.Rows.Count <= 0)
                    ////{
                    ////    this.DisableControls(false);
                    ////    this.DisableHeaderControls(false);
                    ////}
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
        /// Raises the form slice_ time footer count refresh event.
        /// </summary>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnFormSlice_TimeFooterCountRefresh(EventArgs eventArgs)
        {
            if (this.FormSlice_TimeFooterCountRefresh != null)
            {
                this.FormSlice_TimeFooterCountRefresh(this, eventArgs);
            }
        }

        #endregion

        #region Form Load
        /// <summary>
        /// Handles the Load event of the F8040 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F8040_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.validDate.CustomFormat = "mm/dd/yyyy";
                this.validDate.MaxDate = new System.DateTime(2075, 12, 31, 0, 0, 0, 0);
                this.validDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
                this.CustomizeTimeGridView();
                this.PopulateResourceTime();
                this.CommentTextBox.MaxLength = this.timeDataTable.CommentColumn.MaxLength;
                this.dateVal = true;
                this.isDateSelected = false;
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
        #endregion

        /// <summary>
        /// Updates the time grid.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UpdateTimeGrid(object sender, EventArgs e)
        {
            try
            {
                DataSet tempDataSet = new DataSet("Root");
                tempDataSet.Tables.Add(this.timeDataTable.Copy());
                tempDataSet.Tables[0].TableName = "Table";
                this.form8040Control.WorkItem.F8040_UpdateTime(tempDataSet.GetXml(), TerraScan.Common.TerraScanCommon.UserId);
                this.FormSlice_TimeFooterCountRefresh(sender, e);
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
        /// Customizes the time grid view.
        /// </summary>
        private void CustomizeTimeGridView()
        {
            this.TimeGridView.AutoGenerateColumns = false;
            this.TimeGridView.AllowUserToResizeColumns = false;
            DataGridViewColumnCollection columns = this.TimeGridView.Columns;
            columns["Resource"].DataPropertyName = this.timeDataTable.TempResourceIDColumn.ColumnName;
            columns["StartDate"].DataPropertyName = this.timeDataTable.StartDateColumn.ColumnName;
            columns["StartTime"].DataPropertyName = this.timeDataTable.StartTimeColumn.ColumnName;
            columns["EndDate"].DataPropertyName = this.timeDataTable.EndDateColumn.ColumnName;
            columns["EndTime"].DataPropertyName = this.timeDataTable.EndTimeColumn.ColumnName;
            columns["Hours"].DataPropertyName = this.timeDataTable.TotalHoursColumn.ColumnName;
            columns["Comment"].DataPropertyName = this.timeDataTable.CommentColumn.ColumnName;
            columns["TRID"].DataPropertyName = this.timeDataTable.TRIDColumn.ColumnName;
            columns["Resource"].DisplayIndex = 0;
            columns["StartDate"].DisplayIndex = 1;
            columns["StartTime"].DisplayIndex = 2;
            columns["EndDate"].DisplayIndex = 3;
            columns["EndTime"].DisplayIndex = 4;
            columns["Hours"].DisplayIndex = 5;
            columns["Comment"].DisplayIndex = 6;
            columns["TRID"].DisplayIndex = 7;

            this.allUserDataSet = this.form8040Control.WorkItem.F8040_ListTimeResourceInformation(2).ListTimeResource;
            if (this.allUserDataSet.Rows.Count > 0)
            {
                (this.TimeGridView.Columns["Resource"] as DataGridViewComboBoxColumn).DataSource = this.allUserDataSet;
                (this.TimeGridView.Columns["Resource"] as DataGridViewComboBoxColumn).DisplayMember = this.allUserDataSet.ResourceNameColumn.ColumnName;
                (this.TimeGridView.Columns["Resource"] as DataGridViewComboBoxColumn).ValueMember = this.allUserDataSet.TempResourceIDColumn.ColumnName;
                this.PopulateTime();
            }
        }

        #region Populate  Time
        /// <summary>
        /// Populates the Resource.
        /// </summary>
        private void PopulateTime()
        {
            // Issue Fix Code : Starts Here
            int keyIdValid = this.form8040Control.WorkItem.F8040_CheckEventId(this.masterFormNo, this.keyId);

            ////COMMENTED by Jayanthi on Dec 8th - 2006
            if (keyIdValid == 1)
            {
                this.DisableControls(true);
                this.timeDataTable = this.form8040Control.WorkItem.F8040_ListTimeInformation(this.masterFormNo, this.keyId).ListTime;
                this.actualTimeDataTableRowsCount = this.timeDataTable.Rows.Count;
            }
            else
            {
                this.DisableControls(false);
            }

            this.TimeGridView.DataSource = this.timeDataTable;
            if (this.timeDataTable.Rows.Count > TimeGridView.NumRowsVisible)
            {
                this.TimeGridVscrollBar.Visible = false;
            }
            else
            {
                this.TimeGridVscrollBar.Visible = true;
            }

            if (this.TimeGridView.OriginalRowCount == 0)
            {
                this.TimeGridView.CurrentCell = null;
            }

            // Issue Fix Code : Ends Here
        }
        #endregion Populate Time

        #region Populate Resource Time
        /// <summary>
        /// Populates the Resource.
        /// </summary>
        private void PopulateResourceTime()
        {
            this.timeResourceDataTable = this.form8040Control.WorkItem.F8040_ListTimeResourceInformation(1).ListTimeResource;
            if (this.timeResourceDataTable.Rows.Count > 0)
            {
                F8040TimeData.ListTimeResourceDataTable tempResourceList = new F8040TimeData.ListTimeResourceDataTable();
                F8040TimeData.ListTimeResourceRow dr;
                dr = tempResourceList.NewListTimeResourceRow();
                dr.ResourceName = "<Select>";
                dr.ResourceID = 0;
                dr.TempResourceID = 0;
                dr.IsUser = "0";
                tempResourceList.Rows.Add(dr);
                tempResourceList.Merge(this.timeResourceDataTable);
                this.ResourceComboBox.DataSource = tempResourceList;
                this.ResourceComboBox.ValueMember = tempResourceList.TempResourceIDColumn.ColumnName;
                this.ResourceComboBox.DisplayMember = tempResourceList.ResourceNameColumn.ColumnName;
            }
        }

        #endregion Populate Resource Time
        /// <summary>
        /// Disables the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void DisableControls(bool enable)
        {
            this.HeaderPanel.Enabled = enable;
            this.GridPanel.Enabled = enable;
        }

        /// <summary>
        /// Disables the header controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void DisableHeaderControls(bool enable)
        {
            this.HeaderPanel.Enabled = enable;
        }

        /// <summary>
        /// Clears the time details.
        /// </summary>
        private void ClearTimeDetails()
        {
            this.ResourceComboBox.SelectedIndex = 0;
            this.StartDateTextBox.Text = string.Empty;
            this.StartTimeTextBox.Text = string.Empty;
            this.EndDateTextBox.Text = string.Empty;
            this.EndTimeTextBox.Text = string.Empty;
            this.HoursTextBox.Text = string.Empty;
            this.CommentTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Edits the enabled.
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
        /// Adds the record.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AddRecord(object sender, EventArgs e)
        {
            try
            {
                decimal tempHour;
                if (this.HoursTextBox.DecimalTextBoxValue > (decimal)999999.99)
                {
                    MessageBox.Show("Hours should not be greater then 999999.99", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.HoursTextBox.Text = "0";
                    return;
                }
                else if (this.HoursTextBox.DecimalTextBoxValue < (decimal)0.01)
                {
                    MessageBox.Show("Hours should not be less then 0.01", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToInt32(this.ResourceComboBox.SelectedValue) == 0)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("F8040SelectResourceName"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(this.StartDateTextBox.Text.Trim()))
                {
                    this.StartDateTextBox.Text = DateTime.Now.ToShortDateString();
                }

                if (string.IsNullOrEmpty(this.EndDateTextBox.Text.Trim()))
                {
                    this.EndDateTextBox.Text = DateTime.Now.ToShortDateString();
                }

                try
                {
                    this.validDate.Value = DateTime.Parse(this.StartDateTextBox.Text.Trim());
                    if (Convert.ToDateTime(this.StartDateTextBox.Text.Trim()) < this.validDate.MinDate || Convert.ToDateTime(this.StartDateTextBox.Text.Trim()) > this.validDate.MaxDate)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("ValidDate") + "\n" + SharedFunctions.GetResourceString("Allowed") + "\n" + SharedFunctions.GetResourceString("MinDate") + this.validDate.MinDate.ToShortDateString() + "\n" + SharedFunctions.GetResourceString("MaxDate") + this.validDate.MaxDate.ToShortDateString(), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.StartDateTextBox.Text = DateTime.Now.ToShortDateString();
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("ValidDate") + "\n" + SharedFunctions.GetResourceString("Allowed") + "\n" + SharedFunctions.GetResourceString("MinDate") + this.validDate.MinDate.ToShortDateString() + "\n" + SharedFunctions.GetResourceString("MaxDate") + this.validDate.MaxDate.ToShortDateString(), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.StartDateTextBox.Text = DateTime.Now.ToShortDateString();
                    this.StartDateTextBox.Focus();
                    return;
                }

                try
                {
                    this.validDate.Value = DateTime.Parse(this.EndDateTextBox.Text.Trim());
                    if (Convert.ToDateTime(this.EndDateTextBox.Text.Trim()) < this.validDate.MinDate || Convert.ToDateTime(this.EndDateTextBox.Text.Trim()) > this.validDate.MaxDate)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("ValidDate") + "\n" + SharedFunctions.GetResourceString("Allowed") + "\n" + SharedFunctions.GetResourceString("MinDate") + this.validDate.MinDate.ToShortDateString() + "\n" + SharedFunctions.GetResourceString("MaxDate") + this.validDate.MaxDate.ToShortDateString(), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.EndDateTextBox.Text = DateTime.Now.ToShortDateString();
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("ValidDate") + "\n" + SharedFunctions.GetResourceString("Allowed") + "\n" + SharedFunctions.GetResourceString("MinDate") + this.validDate.MinDate.ToShortDateString() + "\n" + SharedFunctions.GetResourceString("MaxDate") + this.validDate.MaxDate.ToShortDateString(), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.EndDateTextBox.Text = DateTime.Now.ToShortDateString();
                    this.EndDateTextBox.Focus();
                    return;
                }

                if ((Convert.ToDateTime(this.EndDateTextBox.Text) < Convert.ToDateTime(this.StartDateTextBox.Text)))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("F8040EndDateGreaterStartDate"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!string.IsNullOrEmpty(this.StartTimeTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.EndTimeTextBox.Text.Trim()))
                {
                    if ((Convert.ToDateTime(this.EndDateTextBox.Text + " " + this.EndTimeTextBox.Text) < Convert.ToDateTime(this.StartDateTextBox.Text + " " + this.StartTimeTextBox.Text)))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("F8040EndtimeGreaterStarttime"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (string.IsNullOrEmpty(this.HoursTextBox.Text.Trim()))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("F8040HoursEmpty"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (!decimal.TryParse(this.HoursTextBox.Text.Trim(), out tempHour))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("F8040InvalidHours"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //// Save Method 
                F8040TimeData.SaveTimeDataTable tempSaveTime = new F8040TimeData.SaveTimeDataTable();
                F8040TimeData.SaveTimeRow tempSaveTimeRow = tempSaveTime.NewSaveTimeRow();
                tempSaveTimeRow[tempSaveTime.KeyIDColumn] = this.keyId;
                tempSaveTimeRow[tempSaveTime.StartDateColumn.ColumnName] = this.StartDateTextBox.Text.Trim();
                tempSaveTimeRow[tempSaveTime.EndDateColumn.ColumnName] = this.EndDateTextBox.Text.Trim();
                tempSaveTimeRow[tempSaveTime.StartTimeColumn.ColumnName] = this.StartTimeTextBox.Text.Trim();
                tempSaveTimeRow[tempSaveTime.EndTimeColumn.ColumnName] = this.EndTimeTextBox.Text.Trim();
                tempSaveTimeRow[tempSaveTime.CommentColumn.ColumnName] = this.CommentTextBox.Text.Trim();
                tempSaveTimeRow[tempSaveTime.MasterFormIDColumn.ColumnName] = this.masterFormNo;
                tempSaveTimeRow[tempSaveTime.IsUserColumn.ColumnName] = this.timeResourceDataTable[this.ResourceComboBox.SelectedIndex - 1][timeResourceDataTable.IsUserColumn].ToString();
                tempSaveTimeRow[tempSaveTime.TotalHoursColumn] = this.HoursTextBox.Text.Trim();
                ////tempSaveTimeRow[tempSaveTime.ResourceIDColumn] = this.ResourceComboBox.SelectedValue;
                tempSaveTimeRow[tempSaveTime.TempResourceIDColumn] = this.ResourceComboBox.SelectedValue;
                tempSaveTime.Rows.Add(tempSaveTimeRow);
                DataSet tempDataSet = new DataSet("Root");
                tempDataSet.Tables.Add(tempSaveTime.Copy());
                tempDataSet.Tables[0].TableName = "Table";
                this.form8040Control.WorkItem.F8040_SaveTime(tempDataSet.GetXml(), TerraScanCommon.UserId);
                this.PopulateTime();
                this.ClearTimeDetails();
                this.StartDateTextBox.Text = DateTime.Now.ToShortDateString();
                this.EndDateTextBox.Text = DateTime.Now.ToShortDateString();
                ////Added by Jayanthi 
                this.FormSlice_TimeFooterCountRefresh(sender, e);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the AddButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.AddRecord(sender, e);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Cals the date time diff.
        /// </summary>
        /// <param name="paraStartDate">The para start date.</param>
        /// <param name="paraEndDate">The para end date.</param>
        /// <param name="paraStartTime">The para start time.</param>
        /// <param name="paraEndTime">The para end time.</param>
        /// <returns>return date diff</returns>
        private double CalDateTimeDiff(string paraStartDate, string paraEndDate, string paraStartTime, string paraEndTime)
        {
            TimeSpan totalTimeDiff;
            DateTime tempStartTime, tempEndTime, tempStartDate, tempEndDate;

            totalTimeDiff = TimeSpan.Zero;
            if (!string.IsNullOrEmpty(paraStartDate) && !string.IsNullOrEmpty(paraEndDate))
            {
                tempStartDate = DateTime.Parse(paraStartDate);
                tempEndDate = DateTime.Parse(paraEndDate);
                totalTimeDiff += tempEndDate - tempStartDate;
            }

            if (!string.IsNullOrEmpty(paraStartTime) && !string.IsNullOrEmpty(paraEndTime))
            {
                tempStartTime = DateTime.Parse(paraStartTime);
                tempEndTime = DateTime.Parse(paraEndTime);

                totalTimeDiff += tempEndTime - tempStartTime;
            }

            return totalTimeDiff.TotalHours;
        }

        /// <summary>
        /// Handles the Validating event of the EndDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void EndDateTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                /////First if condition Added by Jayanthi on Dec 8th - 2006
                if (this.EndDateTextBox.Text != string.Empty && this.StartDateTextBox.Text != string.Empty)
                {
                    if (Convert.ToDateTime(this.EndDateTextBox.Text) < Convert.ToDateTime(this.StartDateTextBox.Text))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("F8040EndDateGreaterStartDate"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the TimeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void TimeGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if ((e.ColumnIndex == TimeGridView.Columns["StartTime"].Index || e.ColumnIndex == TimeGridView.Columns["EndTime"].Index))
                {
                    if (!string.IsNullOrEmpty(e.Value.ToString().Trim()))
                    {
                        // current Date value added because if only am or m is enter it trown expection
                        e.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + e.Value.ToString()).ToShortTimeString();
                    }
                    else
                    {
                        e.Value = string.Empty;
                    }
                }

                if ((e.ColumnIndex == TimeGridView.Columns["StartDate"].Index || e.ColumnIndex == TimeGridView.Columns["EndDate"].Index) && !string.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.Value = Convert.ToDateTime(e.Value.ToString()).ToShortDateString();
                }

                if (e.ColumnIndex == TimeGridView.Columns["Hours"].Index)
                {
                    decimal tempHours;
                    if (string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        e.Value = "";
                    }
                    else
                    {
                        decimal.TryParse(e.Value.ToString(), out tempHours);
                        e.Value = tempHours.ToString("#,##0.00");
                    }
                }

                e.FormattingApplied = true;
            }
            catch (Exception ex)
            {
                ////ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the EndTimeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EndTimeTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.SetHours();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the hours.
        /// </summary>
        private void SetHours()
        {
            if (!string.IsNullOrEmpty(this.StartTimeTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.EndTimeTextBox.Text.Trim()))
            {
                this.HoursTextBox.Text = this.CalDateTimeDiff(this.StartDateTextBox.Text.Trim(), this.EndDateTextBox.Text.Trim(), this.StartTimeTextBox.Text.Trim(), this.EndTimeTextBox.Text.Trim()).ToString("#,##0.00");
            }
            else
            {
            }
        }

        /// <summary>
        /// Handles the Leave event of the StartTimeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StartTimeTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.SetHours();
                this.StartTimeTextBox.BackColor = Color.White;
                this.StartTimePanel.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the StartDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StartDateTextBox_Leave(object sender, EventArgs e)
        {
            ////if (this.isShifts)
            ////{
            ////    this.ResourceComboBox.Focus();
            ////    this.isShifts = false;
            ////}
            ////else
            ////{
            ////    this.StartDatepickerbutton.Focus();
            ////}

            this.SetHours();

            StartDatePanel.BackColor = Color.White;
            StartDateTextBox.BackColor = Color.White;
        }

        /// <summary>
        /// Handles the Leave event of the EndDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EndDateTextBox_Leave(object sender, EventArgs e)
        {
            ////if (this.isShifts)
            ////{
            ////    this.StartTimeTextBox.Focus();
            ////    this.isShifts = false;
            ////}
            ////else
            ////{
            ////    this.EndDatepickerbutton.Focus();
            ////}

            this.SetHours();

            EndDatePanel.BackColor = Color.White;
            EndDateTextBox.BackColor = Color.White;
        }

        /// <summary>
        /// Handles the CellParsing event of the TimeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void TimeGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            DateTime outTempStartDate, outTempEndDate;
            string currentCellDate = string.Empty;
            //// check for Date not equal toempty
            try
            {
                if ((e.ColumnIndex == TimeGridView.Columns["StartDate"].Index || e.ColumnIndex == TimeGridView.Columns["EndDate"].Index) && !string.IsNullOrEmpty(TimeGridView[e.ColumnIndex, e.RowIndex].Value.ToString()))
                {
                    ///// To check Invalid Date
                    if (DateTime.TryParse(e.Value.ToString(), out outTempStartDate))
                    {
                        //// Code Added By Shiva To Fix the Issue 1132.
                        currentCellDate = this.timeDataTable.Rows[e.RowIndex][e.ColumnIndex + 1].ToString();

                        this.validDate.Value = DateTime.Parse(e.Value.ToString());
                        e.Value = this.validDate.Value;
                        ///// If current cell is start date
                        if (e.ColumnIndex == TimeGridView.Columns["StartDate"].Index)
                        {
                            if (Convert.ToDateTime(TimeGridView.Rows[e.RowIndex].Cells["EndDate"].Value.ToString()) < Convert.ToDateTime(e.Value.ToString()))
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("F8040StartDateGreaterEndDate"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                                e.Value = TimeGridView.Rows[e.RowIndex].Cells["StartDate"].Value;
                            }
                        }
                        else/////If current cell is end date 
                        {
                            if (Convert.ToDateTime(TimeGridView.Rows[e.RowIndex].Cells["StartDate"].Value.ToString()) > Convert.ToDateTime(e.Value.ToString()))
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("F8040EndDateGreaterStartDate"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                e.Value = TimeGridView.Rows[e.RowIndex].Cells["EndDate"].Value;
                            }
                        }
                    }
                    else //// If invalid Date format or string
                    {
                        this.ProcessInvalidDate(e);
                    }
                    //// check for Time Validation

                    if (!string.IsNullOrEmpty(TimeGridView.Rows[e.RowIndex].Cells["StartTime"].Value.ToString()) && !string.IsNullOrEmpty(TimeGridView.Rows[e.RowIndex].Cells["EndTime"].Value.ToString()))
                    {
                        if (e.ColumnIndex == TimeGridView.Columns["StartDate"].Index)
                        {
                            DateTime.TryParse(Convert.ToDateTime(e.Value.ToString()).ToShortDateString() + " " + Convert.ToDateTime(TimeGridView.Rows[e.RowIndex].Cells["StartTime"].Value.ToString()).ToShortTimeString(), out outTempStartDate);
                        }
                        else
                        {
                            DateTime.TryParse(Convert.ToDateTime(TimeGridView.Rows[e.RowIndex].Cells["StartDate"].Value.ToString()).ToShortDateString() + " " + Convert.ToDateTime(TimeGridView.Rows[e.RowIndex].Cells["StartTime"].Value.ToString()).ToShortTimeString(), out outTempStartDate);
                        }

                        if (e.ColumnIndex == TimeGridView.Columns["EndDate"].Index)
                        {
                            DateTime.TryParse(Convert.ToDateTime(e.Value.ToString()).ToShortDateString() + " " + Convert.ToDateTime(TimeGridView.Rows[e.RowIndex].Cells["EndTime"].Value.ToString()).ToShortTimeString(), out outTempEndDate);
                        }
                        else
                        {
                            DateTime.TryParse(Convert.ToDateTime(TimeGridView.Rows[e.RowIndex].Cells["EndDate"].Value.ToString()).ToShortDateString() + " " + Convert.ToDateTime(TimeGridView.Rows[e.RowIndex].Cells["EndTime"].Value.ToString()).ToShortTimeString(), out outTempEndDate);
                        }

                        if (outTempStartDate > outTempEndDate)
                        {
                            if (e.ColumnIndex == TimeGridView.Columns["StartDate"].Index)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("F8040StartDateTimeGreaterEndDateTime"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                TimeGridView.Rows[e.RowIndex].Cells["StartTime"].Value = TimeGridView.Rows[e.RowIndex].Cells["EndTime"].Value;
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("F8040EndDateTimelesserStartDatetime"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                TimeGridView.Rows[e.RowIndex].Cells["EndTime"].Value = TimeGridView.Rows[e.RowIndex].Cells["StartTime"].Value;
                            }
                        }
                    }
                }

                ///// To check Invalid Start Time
                if ((e.ColumnIndex == TimeGridView.Columns["StartTime"].Index))
                {
                    if (!string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        if (DateTime.TryParse(Convert.ToDateTime(TimeGridView.Rows[e.RowIndex].Cells["StartDate"].Value.ToString()).ToShortDateString() + " " + e.Value.ToString(), out outTempStartDate))
                        //// if (DateTime.TryParse(TimeGridView.Rows[e.RowIndex].Cells["StartDate"].Value.ToString(), out outTempStartDate))
                        {
                            //// Value assigned because if the value is am or pm it trow an error
                            e.Value = outTempStartDate.ToShortTimeString();
                            if (!string.IsNullOrEmpty(TimeGridView.Rows[e.RowIndex].Cells["EndTime"].Value.ToString()))
                            {
                                if (DateTime.TryParse(Convert.ToDateTime(TimeGridView.Rows[e.RowIndex].Cells["EndDate"].Value.ToString()).ToShortDateString() + " " + Convert.ToDateTime(TimeGridView.Rows[e.RowIndex].Cells["EndTime"].Value.ToString()).ToShortTimeString(), out outTempEndDate))
                                {
                                    if (outTempStartDate > outTempEndDate)
                                    {
                                        MessageBox.Show(SharedFunctions.GetResourceString("F8040StartDateTimeGreaterEndDateTime"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        e.Value = TimeGridView.Rows[e.RowIndex].Cells["EndTime"].Value;
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("F8040InvalidTime"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            e.Value = TimeGridView.Rows[e.RowIndex].Cells["StartTime"].Value.ToString();
                        }
                    }
                }
                else if ((e.ColumnIndex == TimeGridView.Columns["EndTime"].Index))  ////To check Invalid Endtime
                {
                    if (!string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        if (DateTime.TryParse(Convert.ToDateTime(TimeGridView.Rows[e.RowIndex].Cells["EndDate"].Value.ToString()).ToShortDateString() + " " + e.Value.ToString(), out outTempEndDate))
                        {
                            e.Value = outTempEndDate.ToShortTimeString();
                            if (!string.IsNullOrEmpty(TimeGridView.Rows[e.RowIndex].Cells["StartTime"].Value.ToString()) && !string.IsNullOrEmpty(e.Value.ToString()))
                            {
                                if (DateTime.TryParse(Convert.ToDateTime(TimeGridView.Rows[e.RowIndex].Cells["StartDate"].Value.ToString()).ToShortDateString() + " " + Convert.ToDateTime(TimeGridView.Rows[e.RowIndex].Cells["StartTime"].Value.ToString()).ToShortTimeString(), out outTempStartDate))
                                {
                                    if (outTempStartDate > outTempEndDate)
                                    {
                                        MessageBox.Show(SharedFunctions.GetResourceString("F8040EndtimeGreaterStarttime"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        e.Value = TimeGridView.Rows[e.RowIndex].Cells["StartTime"].Value;
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("F8040InvalidTime"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Value = TimeGridView.Rows[e.RowIndex].Cells["EndTime"].Value.ToString();
                        }
                    }
                }
                else if ((e.ColumnIndex == TimeGridView.Columns["Hours"].Index))
                {
                    decimal tempHour;
                    if (string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("F8040HourEmpty"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Value = TimeGridView.Rows[e.RowIndex].Cells["Hours"].Value;
                    }
                    else if (!decimal.TryParse(e.Value.ToString(), out tempHour))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("F8040InvalidHours"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Value = TimeGridView.Rows[e.RowIndex].Cells["Hours"].Value;
                    }
                    else if (tempHour <= 0)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("F8040HoursGreaterZero"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Value = TimeGridView.Rows[e.RowIndex].Cells["Hours"].Value;
                    }

                    if (Convert.ToDecimal(e.Value.ToString()) > (decimal)999999.99)
                    {
                        MessageBox.Show("Hours should not be greater than 999999.99", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Value = TimeGridView.Rows[e.RowIndex].Cells["Hours"].Value;
                    }
                }
            }
            catch (ArgumentException ex)
            {
                ////MessageBox.Show(SharedFunctions.GetResourceString("ValidDate") + "\n" + SharedFunctions.GetResourceString("Allowed") + "\n" + SharedFunctions.GetResourceString("MinDate") + this.validDate.MinDate.ToShortDateString() + "\n" + SharedFunctions.GetResourceString("MaxDate") + this.validDate.MaxDate.ToShortDateString(), SharedFunctions.GetResourceString("DateValidation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show(SharedFunctions.GetResourceString("ValidDate") + "\n" + SharedFunctions.GetResourceString("Allowed") + "\n" + SharedFunctions.GetResourceString("MinDate") + this.validDate.MinDate.ToShortDateString() + "\n" + SharedFunctions.GetResourceString("MaxDate") + this.validDate.MaxDate.ToShortDateString(), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if ((e.ColumnIndex == TimeGridView.Columns["StartDate"].Index))
                {
                    ////e.Value = this.validDate.MinDate.ToShortDateString();
                    //// Code Added By Shiva To Fix the Issue 1132.
                    e.Value = currentCellDate.Trim();
                }
                else if ((e.ColumnIndex == TimeGridView.Columns["EndDate"].Index))
                {
                    ////e.Value = this.validDate.MaxDate.ToShortDateString();
                    //// Code Added By Shiva To Fix the Issue 1132.
                    e.Value = currentCellDate.Trim();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.AssingHoursToGrid(e);
                e.ParsingApplied = true;
            }
        }

        /// <summary>
        /// Processes the invalid date.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void ProcessInvalidDate(DataGridViewCellParsingEventArgs e)
        {
            try
            {
                MessageBox.Show("Invalid Date", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ///// If current cell is start date
                if (e.ColumnIndex == TimeGridView.Columns["StartDate"].Index)
                {
                    e.Value = TimeGridView.Rows[e.RowIndex].Cells["StartDate"].Value;
                }
                else ///// If current cell is End date
                {
                    e.Value = TimeGridView.Rows[e.RowIndex].Cells["EndDate"].Value;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Assings the hours to grid.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void AssingHoursToGrid(DataGridViewCellParsingEventArgs e)
        {
            try
            {
                string tempStartTime, tempEndtime;

                if (e.ColumnIndex == TimeGridView.Columns["EndTime"].Index)
                {
                    tempEndtime = e.Value.ToString();
                }
                else
                {
                    tempEndtime = TimeGridView.Rows[e.RowIndex].Cells["EndTime"].Value.ToString();
                }

                if (e.ColumnIndex == TimeGridView.Columns["StartTime"].Index)
                {
                    tempStartTime = e.Value.ToString();
                }
                else
                {
                    tempStartTime = TimeGridView.Rows[e.RowIndex].Cells["StartTime"].Value.ToString();
                }

                if (!string.IsNullOrEmpty(tempStartTime) && !string.IsNullOrEmpty(tempEndtime))
                {
                    if (e.ColumnIndex.Equals(TimeGridView.Columns["StartDate"].Index))
                    {
                        this.TimeGridView.Rows[e.RowIndex].Cells["Hours"].Value = this.CalDateTimeDiff(e.Value.ToString(), TimeGridView.Rows[e.RowIndex].Cells["EndDate"].Value.ToString(), TimeGridView.Rows[e.RowIndex].Cells["StartTime"].Value.ToString(), TimeGridView.Rows[e.RowIndex].Cells["EndTime"].Value.ToString());
                    }
                    else if (e.ColumnIndex.Equals(TimeGridView.Columns["EndDate"].Index))
                    {
                        this.TimeGridView.Rows[e.RowIndex].Cells["Hours"].Value = this.CalDateTimeDiff(TimeGridView.Rows[e.RowIndex].Cells["StartDate"].Value.ToString(), e.Value.ToString(), TimeGridView.Rows[e.RowIndex].Cells["StartTime"].Value.ToString(), TimeGridView.Rows[e.RowIndex].Cells["EndTime"].Value.ToString());
                    }
                    else if (e.ColumnIndex.Equals(TimeGridView.Columns["StartTime"].Index))
                    {
                        this.TimeGridView.Rows[e.RowIndex].Cells["Hours"].Value = this.CalDateTimeDiff(TimeGridView.Rows[e.RowIndex].Cells["StartDate"].Value.ToString(), TimeGridView.Rows[e.RowIndex].Cells["EndDate"].Value.ToString(), e.Value.ToString(), TimeGridView.Rows[e.RowIndex].Cells["EndTime"].Value.ToString());
                    }
                    else if (e.ColumnIndex.Equals(TimeGridView.Columns["EndTime"].Index))
                    {
                        this.TimeGridView.Rows[e.RowIndex].Cells["Hours"].Value = this.CalDateTimeDiff(TimeGridView.Rows[e.RowIndex].Cells["StartDate"].Value.ToString(), TimeGridView.Rows[e.RowIndex].Cells["EndDate"].Value.ToString(), TimeGridView.Rows[e.RowIndex].Cells["StartTime"].Value.ToString(), e.Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the TimeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void TimeGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    if (e.KeyValue == 46)
                    {
                        if (this.TimeGridView.SelectedRows.Count > 0)
                        {
                            int selectedRow = this.TimeGridView.SelectedRows[0].Index;

                            if (PermissionFiled.deletePermission)
                            {
                                // Coding added for the issue 106[for Empty row deletion here checking the contion
                                if (!string.IsNullOrEmpty(this.TimeGridView.Rows[selectedRow].Cells["TRID"].Value.ToString()))
                                {
                                    if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteTimeItem"), ConfigurationWrapper.ApplicationName + " - " + SharedFunctions.GetResourceString("DeleteTimeTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                    {
                                        this.form8040Control.WorkItem.F8040_DeleteTime(Convert.ToInt32(this.TimeGridView.Rows[selectedRow].Cells["TRID"].Value.ToString()), TerraScanCommon.UserId);
                                        this.PopulateTime();
                                        ////Added by Jayanthi 
                                        this.FormSlice_TimeFooterCountRefresh(sender, e);
                                        //// till here
                                    }
                                }
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

        /// <summary>
        /// Handles the CellValueChanged event of the TimeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void TimeGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.EditEnabled();
                ////this.AddButton.Enabled = false;
                this.DisableHeaderControls(false);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the TimePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TimePictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the TimePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TimePictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.TimeDetailsToolTip.SetToolTip(this.TimePictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Headers the controls key down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void HeaderControlsKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.AddRecord(sender, e);
            }

            this.isShifts = e.Shift;
        }

        ////private void TimeGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        ////{
        ////    if (e.Control is DataGridViewComboBoxEditingControl)
        ////    {
        ////        ////((ComboBox)e.Control).SelectedValueChanged -= new EventHandler(this.TimeResourceGridCombo_SelectedValueChanged);
        ////        ////((ComboBox)e.Control).SelectedValueChanged += new EventHandler(this.TimeResourceGridCombo_SelectedValueChanged);
        ////        ((ComboBox)e.Control).Leave -= new EventHandler(this.TimeResourceGridCombo_Leave);
        ////        ((ComboBox)e.Control).Leave += new EventHandler(this.TimeResourceGridCombo_Leave);
        ////    }
        ////}

        /// <summary>
        /// Handles the Leave event of the TimeResourceGridCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TimeResourceGridCombo_Leave(object sender, EventArgs e)
        {
            try
            {
                ComboBox combo = (ComboBox)sender;
                this.timeDataTable.Rows[this.TimeGridView.CurrentCell.RowIndex][this.timeDataTable.IsUserColumn] = this.allUserDataSet[combo.SelectedIndex][this.allUserDataSet.IsUserColumn].ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the ResourceComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void ResourceComboBox_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.ResourceComboBox.Text.Trim().Length > 15)
                {
                    this.TimeDetailsToolTip.SetToolTip(this.ResourceComboBox, this.ResourceComboBox.Text);
                }
                else
                {
                    this.TimeDetailsToolTip.RemoveAll();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataError event of the TimeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void TimeGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the TimeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void TimeGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.TimeGridView.OriginalRowCount == 0)
                {
                    this.TimeGridView.CurrentCell = null;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the TimeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void TimeGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            ////try
            ////{

            ////    ////code to make the rows other than the editable row as read-only.
            ////    bool hasValues = false;
            ////    if (e.RowIndex >= 1)
            ////    {
            ////        if ((string.IsNullOrEmpty(this.TimeGridView["Resource", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.TimeGridView["StartDate", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.TimeGridView["StartTime", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.TimeGridView["EndDate", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.TimeGridView["EndTime", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.TimeGridView["Hours", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.TimeGridView["Comment", (e.RowIndex - 1)].Value.ToString().Trim())))
            ////        {
            ////            if (e.RowIndex + 1 < TimeGridView.RowCount)
            ////            {
            ////                for (int i = e.RowIndex; i < TimeGridView.RowCount; i++)
            ////                {
            ////                    if (!string.IsNullOrEmpty(this.TimeGridView.Rows[i].Cells["Resource"].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.TimeGridView.Rows[i].Cells["StartDate"].Value.ToString().Trim()) && !(string.IsNullOrEmpty(this.TimeGridView["StartTime", (e.RowIndex - 1)].Value.ToString().Trim())) && !string.IsNullOrEmpty(this.TimeGridView.Rows[i].Cells["EndDate"].Value.ToString().Trim()) && (string.IsNullOrEmpty(this.TimeGridView["EndTime", (e.RowIndex - 1)].Value.ToString().Trim())) && !string.IsNullOrEmpty(this.TimeGridView.Rows[i].Cells["Hours"].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.TimeGridView.Rows[i].Cells["Comment"].Value.ToString().Trim()))
            ////                    {
            ////                        hasValues = true;
            ////                        break;
            ////                    }
            ////                }

            ////                if (hasValues)
            ////                {
            ////                    this.TimeGridView["Resource", e.RowIndex].ReadOnly = false;
            ////                    this.TimeGridView["StartDate", e.RowIndex].ReadOnly = false;
            ////                    this.TimeGridView["StartTime", e.RowIndex].ReadOnly = false;
            ////                    this.TimeGridView["EndDate", e.RowIndex].ReadOnly = false;
            ////                    this.TimeGridView["EndTime", e.RowIndex].ReadOnly = false;
            ////                    this.TimeGridView["Hours", e.RowIndex].ReadOnly = false;
            ////                    this.TimeGridView["Comment", e.RowIndex].ReadOnly = false;
            ////                    this.TimeGridView.Rows[e.RowIndex].Selected = false;
            ////                }
            ////                else
            ////                {
            ////                    if ((string.IsNullOrEmpty(this.TimeGridView["Resource", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.TimeGridView["StartDate", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.TimeGridView["StartTime", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.TimeGridView["EndDate", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.TimeGridView["EndTime", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.TimeGridView["Hours", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.TimeGridView["Comment", e.RowIndex].Value.ToString().Trim())))
            ////                    {
            ////                        this.TimeGridView["Resource", e.RowIndex].ReadOnly = true;
            ////                        this.TimeGridView["StartDate", e.RowIndex].ReadOnly = true;
            ////                        this.TimeGridView["StartTime", e.RowIndex].ReadOnly = true;
            ////                        this.TimeGridView["EndDate", e.RowIndex].ReadOnly = true;
            ////                        this.TimeGridView["EndTime", e.RowIndex].ReadOnly = true;
            ////                        this.TimeGridView["Hours", e.RowIndex].ReadOnly = true;
            ////                        this.TimeGridView["Comment", e.RowIndex].ReadOnly = true;
            ////                        this.TimeGridView.Rows[e.RowIndex].Selected = true;
            ////                    }
            ////                    else
            ////                    {
            ////                        this.TimeGridView["Resource", e.RowIndex].ReadOnly = false;
            ////                        this.TimeGridView["StartDate", e.RowIndex].ReadOnly = false;
            ////                        this.TimeGridView["StartTime", e.RowIndex].ReadOnly = false;
            ////                        this.TimeGridView["EndDate", e.RowIndex].ReadOnly = false;
            ////                        this.TimeGridView["EndTime", e.RowIndex].ReadOnly = false;
            ////                        this.TimeGridView["Hours", e.RowIndex].ReadOnly = false;
            ////                        this.TimeGridView["Comment", e.RowIndex].ReadOnly = false;
            ////                        this.TimeGridView.Rows[e.RowIndex].Selected = false;
            ////                    }
            ////                }
            ////            }
            ////            else
            ////            {
            ////                this.TimeGridView["Resource", e.RowIndex].ReadOnly = true;
            ////                this.TimeGridView["StartDate", e.RowIndex].ReadOnly = true;
            ////                this.TimeGridView["StartTime", e.RowIndex].ReadOnly = true;
            ////                this.TimeGridView["EndDate", e.RowIndex].ReadOnly = true;
            ////                this.TimeGridView["EndTime", e.RowIndex].ReadOnly = true;
            ////                this.TimeGridView["Hours", e.RowIndex].ReadOnly = true;
            ////                this.TimeGridView["Comment", e.RowIndex].ReadOnly = true;
            ////            }
            ////        }
            ////        else
            ////        {
            ////            this.TimeGridView["Resource", e.RowIndex].ReadOnly = false;
            ////            this.TimeGridView["StartDate", e.RowIndex].ReadOnly = false;
            ////            this.TimeGridView["StartTime", e.RowIndex].ReadOnly = false;
            ////            this.TimeGridView["EndDate", e.RowIndex].ReadOnly = false;
            ////            this.TimeGridView["EndTime", e.RowIndex].ReadOnly = false;
            ////            this.TimeGridView["Hours", e.RowIndex].ReadOnly = false;
            ////            this.TimeGridView["Comment", e.RowIndex].ReadOnly = false;
            ////            this.TimeGridView.Rows[e.RowIndex].Selected = false;
            ////        }
            ////    }

            ////    if (e.RowIndex == 0)
            ////    {
            ////        this.TimeGridView["Resource", e.RowIndex].ReadOnly = false;
            ////        this.TimeGridView["StartDate", e.RowIndex].ReadOnly = false;
            ////        this.TimeGridView["StartTime", e.RowIndex].ReadOnly = false;
            ////        this.TimeGridView["EndDate", e.RowIndex].ReadOnly = false;
            ////        this.TimeGridView["EndTime", e.RowIndex].ReadOnly = false;
            ////        this.TimeGridView["Hours", e.RowIndex].ReadOnly = false;
            ////        this.TimeGridView["Comment", e.RowIndex].ReadOnly = false;
            ////        this.TimeGridView.Rows[e.RowIndex].Selected = false;
            ////    }
            ////}
            ////catch (Exception ex)
            ////{
            ////    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            ////}
        }

        /// <summary>
        /// Handles the CellClick event of the TimeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void TimeGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex < this.TimeGridView.OriginalRowCount)
                {
                    this.TimeGridView["Resource", e.RowIndex].ReadOnly = false;
                    this.TimeGridView["StartDate", e.RowIndex].ReadOnly = false;
                    this.TimeGridView["StartTime", e.RowIndex].ReadOnly = false;
                    this.TimeGridView["EndDate", e.RowIndex].ReadOnly = false;
                    this.TimeGridView["EndTime", e.RowIndex].ReadOnly = false;
                    this.TimeGridView["Hours", e.RowIndex].ReadOnly = false;
                    this.TimeGridView["Comment", e.RowIndex].ReadOnly = false;
                    this.TimeGridView.Rows[e.RowIndex].Selected = false;
                }
                else
                {
                    this.TimeGridView["Resource", e.RowIndex].ReadOnly = true;
                    this.TimeGridView["StartDate", e.RowIndex].ReadOnly = true;
                    this.TimeGridView["StartTime", e.RowIndex].ReadOnly = true;
                    this.TimeGridView["EndDate", e.RowIndex].ReadOnly = true;
                    this.TimeGridView["EndTime", e.RowIndex].ReadOnly = true;
                    this.TimeGridView["Hours", e.RowIndex].ReadOnly = true;
                    this.TimeGridView["Comment", e.RowIndex].ReadOnly = true;
                }
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the TimeMonthCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void TimeMonthCalender_KeyPress(object sender, KeyPressEventArgs e)
        {
            ////MessageBox.Show(e.KeyChar.ToString());
        }

        /// <summary>
        /// Handles the Leave event of the ResourceComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ResourceComboBox_Leave(object sender, EventArgs e)
        {
            ResourcePanel.BackColor = Color.White;
            ResourceComboBox.BackColor = Color.White;
        }

        /// <summary>
        /// Handles the RowHeaderMouseClick event of the TimeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void TimeGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            TerraScanCommon.SetDataGridViewPosition(this.TimeGridView, e.RowIndex);
        }
        
     #region Coding for the issue 112
        private void EnddateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.EndDateTextBox.Text = this.EnddateTimePicker.Text;
                this.EndDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        private void StartdateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.StartDateTextBox.Text = this.StartdateTimePicker.Text;
                this.StartDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// TimePicker_KeyPress
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void TimePicker_KeyPress(object sender, KeyPressEventArgs e)
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
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        private void StartDatepickerbutton_Click(object sender, EventArgs e)
        {
            try
            {
                this.TimerImage_Click(this.StartDateTextBox, this.StartdateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void EndDatepickerbutton_Click(object sender, EventArgs e)
        {
            try
            {
                this.TimerImage_Click(this.EndDateTextBox, this.EnddateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// TimerImage_Click
        /// </summary>
        /// <param name="textControl">textControl</param>
        /// <param name="timePickerControl">timePickerControl</param>
        private void TimerImage_Click(TextBox textControl, DateTimePicker timePickerControl)
        {
            this.ParentForm.AcceptButton = null;
            try
            {
                timePickerControl.BringToFront();
                if (!string.IsNullOrEmpty(textControl.Text.Trim()))
                {
                    timePickerControl.Value = Convert.ToDateTime(textControl.Text);
                }
                else
                {
                    timePickerControl.Value = DateTime.Today;
                }

                SendKeys.Send("{F4}");
                timePickerControl.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
    #endregion

    }
}
