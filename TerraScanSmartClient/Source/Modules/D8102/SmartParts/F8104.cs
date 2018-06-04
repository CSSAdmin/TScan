//--------------------------------------------------------------------------------------------
// <copyright file="F8104.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8104.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 12 Sep 06        JYOTHI              Created
//*********************************************************************************/
namespace D8102
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
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Common;
    using System.Web.Services.Protocols;
    using TerraScan.Utilities;

    /// <summary>
    /// F8104 class file
    /// </summary>
    [SmartPart]
    public partial class F8104 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// tVDetailsDataSet variable is used to get the details of tv Details.
        /// </summary>
        private SanitaryPipeInspectionDetailsData tvdetailsDataSet = new SanitaryPipeInspectionDetailsData();

        /// <summary>
        /// detailTypeDataTable variable is used to get the details of detail type.
        /// </summary>
        private SanitaryPipeInspectionDetailsData.ListEventEngineDetailTypeDataTable detailTypeDataTable = new SanitaryPipeInspectionDetailsData.ListEventEngineDetailTypeDataTable();

        /// <summary>
        /// form8104Control Controller
        /// </summary>
        private F8104Controller form8104Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// used to Store listEventEngineTVDetailsRowsCount
        /// </summary>
        private int listEventEngineTVDetailsRowsCount;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int keyId;

        /// <summary>
        /// recordEdited Local variable.
        /// </summary>
        private bool recordEdited;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// formMasterPermissionEdit Local variable.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// PermissionFields struct 
        /// </summary>
        private PermissionFields permissions;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8104"/> class.
        /// </summary>
        public F8104()
        {
            this.InitializeComponent();
            this.flagLoadOnProcess = false;
        }

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
        public F8104(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.DistrictInfoSecIndicatorPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DistrictInfoSecIndicatorPictureBox.Height, this.DistrictInfoSecIndicatorPictureBox.Width, tabText, red, green, blue);
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

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F8104 control.
        /// </summary>
        /// <value>The F8104 control.</value>
        [CreateNew]
        public F8104Controller Form8104Control
        {
            get { return this.form8104Control as F8104Controller; }
            set { this.form8104Control = value; }
        }
        #endregion

        #region Event Subscription

        /// <summary>
        /// Called when [D9030_ F9030_ enable new method].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, DataEventArgs<int> eventArgs)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.New;
            this.ClearTvDetails();
            SanitaryPipeInspectionDetailsData tempTvdetailsDataSet = new SanitaryPipeInspectionDetailsData();
            this.TVDetailsGridView.DataSource = tempTvdetailsDataSet.ListEventEngineTVDetails;
            this.TVDetailsGridView.CurrentCell = null;
            this.DisableControls(false);
            ////this.LockControls(true);
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
        /// Called when [D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            this.ClearTvDetails();
            this.GetDetailType();
            this.PopulateTvDetails();
            this.recordEdited = false;
            this.DisableControls(true);
            ////this.AddButton.Enabled = this.slicePermissionField.newPermission;
            ////this.DisableHeaderControls(this.slicePermissionField.newPermission);
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        /// <summary>
        /// Called when [D9030_ F9030_ delete slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.PermissionFiled.deletePermission)
            {
                ////call the delete method
                ////SliceFormCloseAlert sliceFormCloseAlert;
                ////sliceFormCloseAlert.FormNo = this.masterFormNo;
                ////sliceFormCloseAlert.FlagFormClose = false;
                ////this.FormSlice_FormCloseAlert(this, new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
            }
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
                    this.SaveGrid();
                    this.recordEdited = false;
                    ////this.AddButton.Enabled = this.slicePermissionField.newPermission;
                    ////this.DisableHeaderControls(this.slicePermissionField.newPermission);
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            else
            {
                this.DisableControls(true);
                this.GetDetailType();
                this.PopulateTvDetails();
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

                    if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
                    {
                        this.LockControls(false);
                    }
                    else
                    {
                        this.LockControls(true);
                    }

                    if (this.listEventEngineTVDetailsRowsCount > 0)
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
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.ClearTvDetails();
                this.GetDetailType();
                this.PopulateTvDetails();
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the F8104 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F8104_Load(object sender, EventArgs e)
        {
            this.FlagSliceForm = true;
            this.flagLoadOnProcess = true;
            this.GetDetailType();
            this.CustomizeTVDetailsGridView();
            this.PopulateTvDetails();
            this.flagLoadOnProcess = false;
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
        /// Handles the Click event of the DistrictInfoSecIndicatorPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictInfoSecIndicatorPictureBox_Click(object sender, EventArgs e)
        {
            this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D8102.F8104"));
        }

        /// <summary>
        /// Handles the DataError event of the TVDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void TVDetailsGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        /// <summary>
        /// Handles the Click event of the AddButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            ////try
            ////{
            ////if (this.recordEdited == true)
            ////{
            ////    DialogResult result = MessageBox.Show("Do you want to save changes in Grid", "TEr", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            ////    if (result == DialogResult.Yes)
            ////    {
            ////        this.SaveGrid();
            ////    }
            ////    else if (result == DialogResult.No)
            ////    {
            ////        this.AddRecord();
            ////    }
            ////}
            ////else
            ////{
            ////    this.AddRecord();
            ////}

            this.AddRecord();
        }

        /// <summary>
        /// Handles the KeyDown event of the TVDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void TVDetailsGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    if (e.KeyValue == 46)
                    {
                        if (this.TVDetailsGridView.SelectedRows.Count > 0)
                        {
                            int selectedRow = this.TVDetailsGridView.SelectedRows[0].Index;

                            if (PermissionFiled.deletePermission)
                            {
                                // Coding added for the issue 106[for Empty row deletion here checking the contion
                                if (!string.IsNullOrEmpty(this.TVDetailsGridView.Rows[selectedRow].Cells[3].Value.ToString()))
                                {
                                    if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteDetailRecord"), ConfigurationWrapper.ApplicationName + " - " + SharedFunctions.GetResourceString("DeleteDetailTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                    {
                                        this.form8104Control.WorkItem.DeleteEventEngineTVDetails(Convert.ToInt32(this.TVDetailsGridView.Rows[selectedRow].Cells[3].Value.ToString()), TerraScanCommon.UserId);
                                        this.PopulateTvDetails();
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
        /// Handles the CellValueChanged event of the TVDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void TVDetailsGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.recordEdited = true)
            {
                this.EditEnabled();
                ////this.AddButton.Enabled = false;
                ////this.DisableHeaderControls(false);
            }
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
        /// Handles the CellParsing event of the TVDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void TVDetailsGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            // Only paint if desired column

            if (e.ColumnIndex == this.TVDetailsGridView.Columns["Length"].Index)
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                // Only paint if text provided, Only paint if desired text is in cell

                if (e.Value != null)
                {
                    string tempvalue = e.Value.ToString().Trim();
                    Decimal outDecimal = 0;

                    if (Decimal.TryParse(tempvalue, System.Globalization.NumberStyles.Currency, null, out outDecimal) && outDecimal <= (decimal)999999.99)
                    {
                        e.Value = outDecimal.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Length should not be greater than 999999.99", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Value = TVDetailsGridView.Rows[e.RowIndex].Cells["Length"].Value;

                        ////e.Value = outDecimal;
                        e.ParsingApplied = true;
                    }

                    ////e.ParsingApplied = true;
                    this.TVDetailsGridView.RefreshEdit();
                }
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the DistrictInfoSecIndicatorPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictInfoSecIndicatorPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.SanitaryPipeInspToolTip.SetToolTip(this.DistrictInfoSecIndicatorPictureBox, "D8102.F8104");
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
                this.AddRecord();
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the TVDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void TVDetailsGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (this.TVDetailsGridView.OriginalRowCount == 0)
            {
                this.TVDetailsGridView.CurrentCell = null;
            }
        }
        #endregion Events

        #region Private Methods

        /// <summary>
        /// Gets the type of the detail.
        /// </summary>
        private void GetDetailType()
        {
            try
            {
                this.detailTypeDataTable = this.form8104Control.WorkItem.ListEventEngineDetailTypes().ListEventEngineDetailType;
                this.DetailTypeComboBox.DataSource = this.detailTypeDataTable.Copy();
                this.DetailTypeComboBox.ValueMember = this.detailTypeDataTable.DetailTypeIDColumn.ColumnName;
                this.DetailTypeComboBox.DisplayMember = this.detailTypeDataTable.DetailTypeColumn.ColumnName;
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// This Method used to  set dataproperty name and column displayindex and paymentsdatatable initialization
        /// CustomizeErrorGridView
        /// </summary>
        private void CustomizeTVDetailsGridView()
        {
            this.TVDetailsGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection columns = this.TVDetailsGridView.Columns;

            columns["Length"].DataPropertyName = this.tvdetailsDataSet.ListEventEngineTVDetails.LengthColumn.ColumnName;
            columns["Detail"].DataPropertyName = this.tvdetailsDataSet.ListEventEngineTVDetails.DetailTypeIDColumn.ColumnName;
            columns["Comment"].DataPropertyName = this.tvdetailsDataSet.ListEventEngineTVDetails.CommentColumn.ColumnName;
            columns["DetailId"].DataPropertyName = this.tvdetailsDataSet.ListEventEngineTVDetails.DetailIDColumn.ColumnName;
            columns["EventId"].DataPropertyName = this.tvdetailsDataSet.ListEventEngineTVDetails.EventIDColumn.ColumnName;

            columns["Length"].DisplayIndex = 0;
            columns["Detail"].DisplayIndex = 1;
            columns["Comment"].DisplayIndex = 2;
            columns["DetailId"].DisplayIndex = 3;
            columns["EventId"].DisplayIndex = 4;

            this.TVDetailsGridView.DataSource = this.tvdetailsDataSet.ListEventEngineTVDetails;

            (this.TVDetailsGridView.Columns["Detail"] as DataGridViewComboBoxColumn).DataSource = this.detailTypeDataTable;
            (this.TVDetailsGridView.Columns["Detail"] as DataGridViewComboBoxColumn).DisplayMember = "DetailType";
            (this.TVDetailsGridView.Columns["Detail"] as DataGridViewComboBoxColumn).ValueMember = "DetailTypeID";
        }

        /// <summary>
        /// Gets the tv details.
        /// </summary>
        private void PopulateTvDetails()
        {
            this.tvdetailsDataSet = this.form8104Control.WorkItem.ListEventEngineTVDetails(this.keyId);
            if (this.tvdetailsDataSet.ListEventEngineTVDetails.Rows.Count > 0)
            {
                this.listEventEngineTVDetailsRowsCount = this.tvdetailsDataSet.ListEventEngineTVDetails.Rows.Count;
                if (this.tvdetailsDataSet.ListEventEngineTVDetails.Rows.Count > 0)
                {
                    (this.TVDetailsGridView.Columns["Detail"] as DataGridViewComboBoxColumn).DataSource = this.detailTypeDataTable;
                    (this.TVDetailsGridView.Columns["Detail"] as DataGridViewComboBoxColumn).DisplayMember = "DetailType";
                    (this.TVDetailsGridView.Columns["Detail"] as DataGridViewComboBoxColumn).ValueMember = "DetailTypeID";
                }

                if (this.tvdetailsDataSet.ListEventEngineTVDetails.Rows.Count > this.TVDetailsGridView.NumRowsVisible)
                {
                    this.TvDetailsGridVscrollBar.Visible = false;
                }
                else
                {
                    this.TvDetailsGridVscrollBar.Visible = true;
                }

                this.TVDetailsGridView.Enabled = true;
                this.TVDetailsGridView.DataSource = this.tvdetailsDataSet.ListEventEngineTVDetails;
            }
            else
            {
                this.TVDetailsGridView.DataSource = this.tvdetailsDataSet.ListEventEngineTVDetails;
                if (this.keyId.Equals(0))
                {
                    this.DisableControls(false);
                }
                else
                {
                    this.DisableControls(true);
                }
            }
        }

        /// <summary>
        /// Clears the event properties.
        /// </summary>
        private void ClearTvDetails()
        {
            this.LengthTextBox.Text = string.Empty;
            this.CommentTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Clears the event properties.
        /// </summary>
        /// <param name="lockControl">if set to <c>true</c> [lock control].</param>
        private void LockControls(bool lockControl)
        {
            ////this.LengthTextBox.LockKeyPress = lockControl;
            ////this.CommentTextBox.LockKeyPress = lockControl;
            ////this.TVDetailsGridView.Enabled = !lockControl;
            this.TVDetailsGridView.ReadOnly = lockControl;
            this.TVDetailsGridView.TabStop = !lockControl;
        }

        /// <summary>
        /// Clears the event properties.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void DisableControls(bool enable)
        {
            this.LengthPanel.Enabled = enable;
            this.CommentPanel.Enabled = enable;
            this.DetailPanel.Enabled = enable;
            this.GridPanel.Enabled = enable;
            this.CommentPanel.Enabled = enable;
            this.AddButton.Enabled = enable;
        }

        /// <summary>
        /// Disables the header controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void DisableHeaderControls(bool enable)
        {
            ////this.LengthPanel.Enabled = enable;            
            ////this.DetailPanel.Enabled = enable;
            ////this.DetailTypeComboBox.Enabled = enable;           
        }

        /// <summary>
        /// Saves the grid.
        /// </summary>
        private void SaveGrid()
        {
            try
            {
                if (TVDetailsGridView.DataSource != null)
                {
                    DataSet ds = new DataSet("Root");
                    DataTable dt = new DataTable("Table");

                    dt = ((DataView)TVDetailsGridView.DataSource).Table;
                    dt.TableName = "Table";
                    ds.Tables.Add(dt.Copy());
                    string tvdetails = ds.GetXml();
                    this.form8104Control.WorkItem.UpdateEventEngineTVDetails(tvdetails, TerraScanCommon.UserId);
                }
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
        /// Adds the record.
        /// </summary>
        private void AddRecord()
        {
            try
            {
                if (LengthTextBox.DecimalTextBoxValue > (decimal)999999.99)
                {
                    MessageBox.Show("Length should not be greater than 999999.99", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LengthTextBox.Text = "0";
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                SanitaryPipeInspectionDetailsData sanitaryPipeInspectionDetailsData = new SanitaryPipeInspectionDetailsData();
                SanitaryPipeInspectionDetailsData.SaveEventEngineTVDetailsRow dr = sanitaryPipeInspectionDetailsData.SaveEventEngineTVDetails.NewSaveEventEngineTVDetailsRow();

                dr.EventID = this.keyId;
                if (!string.IsNullOrEmpty(this.LengthTextBox.Text.Trim()))
                {
                    dr.Length = Convert.ToDecimal(this.LengthTextBox.Text.Trim());
                }
                else
                {
                    dr.Length = 0;
                }

                dr.Comment = this.CommentTextBox.Text.Trim();
                dr.DetailTypeID = Convert.ToInt32(this.DetailTypeComboBox.SelectedValue.ToString());

                sanitaryPipeInspectionDetailsData.SaveEventEngineTVDetails.Rows.Add(dr);
                DataSet tempDataSet = new DataSet("Root");
                tempDataSet.Tables.Add(sanitaryPipeInspectionDetailsData.SaveEventEngineTVDetails.Copy());
                tempDataSet.Tables[0].TableName = "Table";

                this.form8104Control.WorkItem.SaveEventEngineTVDetails(tempDataSet.GetXml(), TerraScanCommon.UserId);
                this.ClearTvDetails();
                this.GetDetailType();
                this.PopulateTvDetails();
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

        #endregion  Private Methods

        /// <summary>
        /// Handles the CellClick event of the TVDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void TVDetailsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex < this.TVDetailsGridView.OriginalRowCount)
                {
                    this.TVDetailsGridView["Length", e.RowIndex].ReadOnly = false;
                    this.TVDetailsGridView["Detail", e.RowIndex].ReadOnly = false;
                    this.TVDetailsGridView["Comment", e.RowIndex].ReadOnly = false;
                    this.TVDetailsGridView["DetailId", e.RowIndex].ReadOnly = false;
                    this.TVDetailsGridView["EventId", e.RowIndex].ReadOnly = true;
                }
                else
                {
                    this.TVDetailsGridView["Length", e.RowIndex].ReadOnly = true;
                    this.TVDetailsGridView["Detail", e.RowIndex].ReadOnly = true;
                    this.TVDetailsGridView["Comment", e.RowIndex].ReadOnly = true;
                    this.TVDetailsGridView["DetailId", e.RowIndex].ReadOnly = true;
                    this.TVDetailsGridView["EventId", e.RowIndex].ReadOnly = true;
                }
            }
        }

        /// <summary>
        /// Handles the Leave event of the CommentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CommentTextBox_Leave(object sender, EventArgs e)
        {
            ////khaja commented code to fix Bug#4214
            ////if (this.tvdetailsDataSet.ListEventEngineTVDetails.Rows.Count > 0)
            ////{
            ////    TVDetailsGridView.Focus();
            ////}
            ////else
            ////{
            ////    TVDetailsGridView.ReadOnly = true;
            ////}
        }
    }
}
