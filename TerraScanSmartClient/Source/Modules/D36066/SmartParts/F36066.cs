// -------------------------------------------------------------------------------------------
// <copyright file="F36066.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// </summary>
// Release history
// ********************************************************************************************
// Date               Author            Description
// ----------        ---------     -------------------------------------------------------
// 27/07/2009        D.LathaMaheswari    Created
// -------------------------------------------------------------------------------------------
namespace D36066
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
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
    /// F36066 smartpart
    /// </summary>
    [SmartPart]
    public partial class F36066 : BaseSmartPart
    {
        #region Variable
        /// <summary>
        /// Master Form number
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// Current Key Id[TrendId] of the form
        /// </summary>
        private int keyId;

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
        private F36066Controller form36066Controller;

        /// <summary>
        /// The Page Mode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// flag LoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// An object for ContextMenuStrip
        /// </summary>
        private ContextMenuStrip selectedItemMenuStrip = new ContextMenuStrip();

        /// <summary>
        /// DataSet for Trend details
        /// </summary>
        private F36066TrendData trendData = new F36066TrendData();

        /// <summary>
        /// DataSet for Trend details
        /// </summary>
        private F36066TrendData.GetTrendListDataTable changeSet;

        /// <summary>
        /// Flag for cell focus
        /// </summary>
        private bool isCellEnter;

        #endregion Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F36066"/> class.
        /// </summary>
        public F36066()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F36066"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="redColor">Color of the red.</param>
        /// <param name="greenColor">Color of the green.</param>
        /// <param name="blueColor">Color of the blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F36066(int masterform, int formNo, int keyId, int redColor, int greenColor, int blueColor, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyId;
            this.formMasterPermissionEdit = permissionEdit;
            this.HeaderPictureBox.Image = ExtendedGraphics.GenerateHorizontalImage(this.HeaderPictureBox.Height, this.HeaderPictureBox.Width, string.Empty, redColor, greenColor, blueColor);
            this.TrendPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.TrendPictureBox.Height, this.TrendPictureBox.Width, tabText, redColor, greenColor, blueColor);
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
        /// Gets or sets the form36066 controller.
        /// </summary>
        /// <value>The form36066 controller.</value>
        [CreateNew]
        public F36066Controller Form36066Controller
        {
            get { return this.form36066Controller as F36066Controller; }
            set { this.form36066Controller = value; }
        }

        #endregion Property

        #region Event Subscription

        /// <summary>
        /// Called when [D9030_ F9030_ enable new method].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            if (this.slicePermissionField.newPermission)
            {
                // Clear all the controls value and make it enable
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearControls();
                this.trendData.Clear();
                this.TrendGridView.DataSource = this.trendData.GetTrendList.DefaultView;
                this.trendData.AcceptChanges();
                this.EnableControls(true);
                this.LockControls(false);
                // Set focus on firs editanle field
                this.RollYearTextBox.Focus();
            }
            else
            {
                // Clear all the controls and make it disable
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearControls();
                this.trendData.Clear();
                this.TrendGridView.DataSource = this.trendData.GetTrendList.DefaultView;
                this.trendData.AcceptChanges();
                this.EnableControls(false);
                this.LockControls(true);
                this.TrendGridView.Focus();
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
            // Clear all the controls value
            this.ClearControls();

            // Load Trend details
            this.LoadTrendDetails();

            // Lock/Unlock controls based on the edit permission 
            this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit || this.trendData.GetTrendDetails.Rows.Count == 0);
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if ((this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit) && this.slicePermissionField.editPermission)
                 || (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New) && this.slicePermissionField.newPermission))
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
            if ((this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit) && this.slicePermissionField.editPermission)
                || (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New) && this.slicePermissionField.newPermission))
            {
                this.SaveTrendDetails();
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
                if (this.masterFormNo.Equals(eventArgs.Data.MasterFormNo))
                {
                    // Get Form slice permission
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                    // Validate keyid of the form slice
                    if (this.trendData.GetTrendDetails.Rows.Count > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;

                        // Lock/Unlock controls based on the edit permission 
                        this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                        this.RollYearTextBox.Focus();

                        if (this.TrendGridView.OriginalRowCount > 0)
                        {
                            this.TrendGridView.Rows[0].Selected = true;
                        }
                        else
                        {
                            this.TrendGridView.Rows[0].Selected = false;
                        }
                    }
                    else
                    {
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            eventArgs.Data.FlagInvalidSliceKey = true;
                        }

                        this.EnableControls(false);
                    }
                }
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
                if (this.masterFormNo.Equals(eventArgs.Data.MasterFormNo))
                {
                    this.flagLoadOnProcess = true;

                    // Get current keyid
                    this.keyId = eventArgs.Data.SelectedKeyId;

                    //this.EnableControls(!this.slicePermissionField.editPermission && !this.formMasterPermissionEdit);
                    // Load Trend Details
                    this.LoadTrendDetails();
                    // Lock controls based on edit permission
                    this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
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
        /// Called when [D9030_ F9030_ delete slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            if (this.slicePermissionField.deletePermission)
            {
                this.Cursor = Cursors.WaitCursor;

                // DB call for Delete
                this.form36066Controller.WorkItem.DeleteTrend(this.keyId, this.SelectedTrendIdCollection(), TerraScanCommon.UserId);
                SliceFormCloseAlert sliceFormCloseAlert;
                sliceFormCloseAlert.FormNo = this.masterFormNo;
                sliceFormCloseAlert.FlagFormClose = false;
                this.OnFormSlice_FormCloseAlert(new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
                this.Cursor = Cursors.Default;
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
        /// Handles the Load event of the F36066 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F36066_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                this.Cursor = Cursors.WaitCursor;

                // Customize the Trend Grid
                this.CustomizeGrid();

                // Load Trend Details
                this.LoadTrendDetails();

                // Load Context Menu
                this.LoadContextMenu();

                this.pageMode = TerraScanCommon.PageModeTypes.View;

                // Set focus on first editable field
                this.RollYearTextBox.Focus();
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
            finally
            {
                this.Cursor = Cursors.Default;
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
                // Enable Form Master Save/Cancel button
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #region Menu Strip Events

        /// <summary>
        /// Handles the ItemClicked event of the SelectedItemMenuStrip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ToolStripItemClickedEventArgs"/> 
        /// instance containing the event data.</param>
        private void SelectedItemMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                this.selectedItemMenuStrip.Visible = false;
                if (e.ClickedItem.Text.Equals(SharedFunctions.GetResourceString("Delete")))
                {
                    // Delete Trend details based on the selected records
                    this.DeleteTrendItems();
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
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Grid Events

        /// <summary>
        /// Handles the CellEndEdit event of the TrendGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void TrendGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Add New Row 
                if (e.RowIndex.Equals(this.TrendGridView.Rows.Count - 1) && this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                {
                    if (this.TrendGridView.Rows[e.RowIndex].Cells[this.Year.Name].Value != null
                        && !string.IsNullOrEmpty(this.TrendGridView.Rows[e.RowIndex].Cells[this.Year.Name].Value.ToString().Trim())
                        && this.TrendGridView.Rows[e.RowIndex].Cells[this.Trend.Name].Value != null
                        && !string.IsNullOrEmpty(this.TrendGridView.Rows[e.RowIndex].Cells[this.Trend.Name].Value.ToString().Trim()))
                    {
                        F36066TrendData.GetTrendListRow newRow = this.trendData.GetTrendList.NewGetTrendListRow();
                        newRow["EmptyRecord$"] = "True";
                        this.trendData.GetTrendList.AddGetTrendListRow(newRow);
                        this.TrendGridScroll.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellParsing event of the TrendGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void TrendGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.Value.Equals(null))
                {
                    return;
                }

                if (e.ColumnIndex.Equals(this.Year.Index))
                {
                    if (!string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        // Year field validation
                        // Allow to enter the maximum of short value
                        short tempYear;
                        short.TryParse(e.Value.ToString().Trim(), out tempYear);
                        if (tempYear.Equals(0))
                        {
                            e.Value = string.Empty; 
                        }
                        else
                        {
                            e.Value = tempYear.ToString();
                        }

                        e.ParsingApplied = true;
                    }
                    else
                    {
                        e.Value = string.Empty;
                    }
                }

                if (e.ColumnIndex.Equals(this.Trend.Index))
                {
                    string tempvalue = e.Value.ToString().Trim();
                    if (!string.IsNullOrEmpty(tempvalue))
                    {
                        decimal outDecimal;

                        // If the entered value ends with '.' append 00
                        if (tempvalue.EndsWith("."))
                        {
                            tempvalue = string.Concat(tempvalue, "00");
                        }

                        if (decimal.TryParse(tempvalue, NumberStyles.Currency, null, out outDecimal))
                        {
                            tempvalue = outDecimal.ToString();

                            if (tempvalue.Contains("-"))
                            {
                                // Restrict negative values
                                outDecimal = decimal.Zero;
                            }

                            if (outDecimal >= 1000)
                            {
                                outDecimal = decimal.Zero;
                            }
                        }

                        e.Value = outDecimal.ToString("#,##0.00");
                        e.ParsingApplied = true;
                    }
                    else
                    {
                        e.Value = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseUp event of the TrendGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        void TrendGridView_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (e.Button.Equals(MouseButtons.Right) && this.slicePermissionField.deletePermission
                    && this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    if (this.TrendGridView.CurrentRow != null && this.TrendGridView.CurrentRow.Index >= 0
                        && this.TrendGridView.CurrentRow.Selected)
                    {
                        this.selectedItemMenuStrip.Show(this.TrendGridView, new Point(e.X, e.Y));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the TrendGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void TrendGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Delete))
                {
                    // Delete Trend details based on the record selection in Grid
                    this.DeleteTrendItems();
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Handles the CellFormatting event of the TrendGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void TrendGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.Value.Equals(null) || !e.ColumnIndex.Equals(this.Trend.Index))
                {
                    return;
                }

                // Format for Trend column
                if (!String.IsNullOrEmpty(this.TrendGridView.Rows[e.RowIndex].Cells[this.Trend.Name].Value.ToString()))
                {
                    decimal trendValue;
                    if (decimal.TryParse(e.Value.ToString(), out trendValue))
                    {
                        e.Value = trendValue.ToString("#,##0.00");
                        e.FormattingApplied = true;
                    }
                    else
                    {
                        e.Value = "0.00";
                    }
                }
                else
                {
                    e.Value = string.Empty;
                    e.FormattingApplied = true;
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the TrendGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void TrendGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                // Grid TextBox controls
                if (e.Control is DataGridViewTextBoxEditingControl)
                {
                    e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                    e.Control.Validated += new EventHandler(this.Control_Validated);
                }
                this.isCellEnter = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validated event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_Validated(object sender, EventArgs e)
        {
            try
            {
                this.TrendGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
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
                if (!this.isCellEnter)
                {
                    this.EditEnabled();
                }
                this.TrendGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void TrendGridView_CellBeginEdit(object sender, System.Windows.Forms.DataGridViewCellCancelEventArgs e)
        {
            try
            {
                this.isCellEnter = true;
                //this.EditEnabled();
                if (this.TrendGridView.EditingControl != null)
                {
                    this.TrendGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        /// <summary>
        /// Handles the RowEnter event of the TrendGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void TrendGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.SetReadOnly(e.RowIndex);
              
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Enter event of the TrendGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TrendGridView_Enter(object sender, EventArgs e)
        {
            try
            {
                if (this.TrendGridView.OriginalRowCount > 0)
                {
                    this.TrendGridView.Rows[0].Cells[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void TrendGridView_ColumnHeaderMouseClick(object sender, System.Windows.Forms.DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //if (this.TrendGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection.Equals(SortOrder.Ascending))
                //{
                //    this.TrendGridView.Sort(this.TrendGridView.Columns[e.ColumnIndex], ListSortDirection.Ascending);
                //}
                //else
                //{
                //    this.TrendGridView.Sort(this.TrendGridView.Columns[e.ColumnIndex], ListSortDirection.Descending);
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Grid Events

        #endregion Events

        #region Methods

        #region Clear Controls

        /// <summary>
        /// Clears the controls.
        /// </summary>
        private void ClearControls()
        {
            this.RollYearTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
        }

        #endregion Clear Controls

        #region EnableControls

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="hasLocked">if set to <c>true</c> [has locked].</param>
        private void LockControls(bool hasLocked)
        {
            this.RollYearTextBox.LockKeyPress = hasLocked;
            this.DescriptionTextBox.LockKeyPress = hasLocked;
            this.TrendGridPanel.Enabled = !hasLocked;
        }

        /// <summary>
        /// Enables the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableControls(bool enable)
        {
            this.HeaderPanel.Enabled = enable;
            this.TrendGridPanel.Enabled = enable;
            this.TrendGridPanel.Enabled = enable;
        }

        #endregion Enable Controls

        #region Enable Edit

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (!this.flagLoadOnProcess && this.pageMode.Equals(TerraScanCommon.PageModeTypes.View) 
                && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;

                // Event publication for enable Save,Cancel button in Form Master
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                this.TrendGridView.AllowSorting = false;
            }
        }

        #endregion Enable Edit

        #region Grid Customization

        /// <summary>
        /// Customizes the grid.
        /// </summary>
        private void CustomizeGrid()
        {
            this.TrendGridView.AllowUserToResizeColumns = false;
            this.TrendGridView.AllowUserToResizeRows = false;
            this.TrendGridView.AutoGenerateColumns = false;

            this.TrendID.DataPropertyName = this.trendData.GetTrendList.TrendIDColumn.ColumnName;
            this.TrendYearId.DataPropertyName = this.trendData.GetTrendList.TrendYearIDColumn.ColumnName;
            this.Year.DataPropertyName = this.trendData.GetTrendList.YearColumn.ColumnName;
            this.Trend.DataPropertyName = this.trendData.GetTrendList.TrendColumn.ColumnName;
            this.Description.DataPropertyName = this.trendData.GetTrendList.DescriptionColumn.ColumnName;
            this.Description.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.TrendGridView.PrimaryKeyColumnName = this.trendData.GetTrendList.TrendIDColumn.ColumnName;
        }

        #endregion Grid Customization

        #region Read Only

        /// <summary>
        /// Sets the read only.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void SetReadOnly(int rowIndex)
        {
            if (rowIndex > 0)
            {
                if ((string.IsNullOrEmpty(this.TrendGridView[this.Year.Name, rowIndex - 1].Value.ToString().Trim())
                    && string.IsNullOrEmpty(this.TrendGridView[this.Trend.Name, rowIndex - 1].Value.ToString().Trim())))
                {
                    if (rowIndex < this.TrendGridView.OriginalRowCount)
                    {
                        this.TrendGridView.Rows[rowIndex].ReadOnly = false;
                    }
                    else
                    {
                        this.TrendGridView.Rows[rowIndex].ReadOnly = true;
                    }
                }
                else
                {
                    this.TrendGridView.Rows[rowIndex].ReadOnly = false;
                }
            }
            else if (rowIndex.Equals(0))
            {
                this.TrendGridView.Rows[rowIndex].ReadOnly = false;
            }
        }

        #endregion Read Only

        #region Load Trend Details

        /// <summary>
        /// Gets the trend details.
        /// </summary>
        private void LoadTrendDetails()
        {
            this.TrendGridView.Sort(this.TrendGridView.Columns[this.Year.Name], ListSortDirection.Ascending);
            // DB call for get trend details for specific keyId
            this.trendData = this.form36066Controller.WorkItem.GetTrendDetails(this.keyId);

            // Assign controls on appropriate controls
            this.SetControlValue();
            this.trendData.AcceptChanges();
             
            if (this.TrendGridView.OriginalRowCount > 0)
            {
                this.TrendGridView.Rows[0].Selected = true;
                //this.TrendGridView.Sort(this.TrendGridView.Columns[this.Year.Name], ListSortDirection.Ascending);
            }
            else
            {
                this.TrendGridView.Rows[0].Selected = false;

            }
            this.TrendGridView.AllowSorting = true;
        }

        #endregion Load Trend Details

        #region Control Value

        /// <summary>
        /// Sets the control value.
        /// </summary>
        private void SetControlValue()
        {
            // Assign value on appropriate controls
            //this.trendData.GetTrendList.DefaultView.Sort = this.Year.Name;
            //this.trendData.GetTrendList. = (F36066TrendData.GetTrendListDataTable)this.trendData.GetTrendList.DefaultView.ToTable();
            this.TrendGridView.DataSource = this.trendData.GetTrendList.DefaultView;
           
            // On load if Grid in edit status remove textchanged event
            if (this.TrendGridView.EditingControl != null)
            {
                this.TrendGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
            }

            if (this.trendData.GetTrendDetails.Rows.Count > 0)
            {
                F36066TrendData.GetTrendDetailsRow currentRow = (F36066TrendData.GetTrendDetailsRow)this.trendData.GetTrendDetails.Rows[0];
                this.RollYearTextBox.Text = currentRow.RollYear.ToString();
                this.DescriptionTextBox.Text = currentRow.Description.ToString();
                this.RollYearTextBox.Focus();

                if (this.TrendGridView.OriginalRowCount > 0)
                {
                    if (this.TrendGridView.OriginalRowCount >= this.TrendGridView.NumRowsVisible)
                    {
                        F36066TrendData.GetTrendListRow newRow = this.trendData.GetTrendList.NewGetTrendListRow();
                        newRow["EmptyRecord$"] = "True";
                        this.trendData.GetTrendList.AddGetTrendListRow(newRow);
                    }
                }
                this.EnableControls(true);
            }
            else
            {
                // Clear all the controls and make it disable
                this.ClearControls();
                this.EnableControls(false);
            }

            this.SetScrollBarVisibility();
        }

        #endregion Control Value

        #region ScrollBar Visibility

        /// <summary>
        /// Sets the scroll bar visibility.
        /// </summary>
        private void SetScrollBarVisibility()
        {
            if (this.trendData.GetTrendList.Rows.Count <= this.TrendGridView.NumRowsVisible)
            {
                this.TrendGridScroll.Visible = true;
            }
            else
            {
                this.TrendGridScroll.Visible = false;
            }
        }

        #endregion ScrollBar Visibility

        #region Context Menu

        /// <summary>
        /// Loads the context menu.
        /// </summary>
        private void LoadContextMenu()
        {
            // Assiging menu value(Delete and Cancel) for Contextmenustrip
            this.selectedItemMenuStrip.Items.Add(SharedFunctions.GetResourceString("MenuDelete"));
            this.selectedItemMenuStrip.Items.Add(SharedFunctions.GetResourceString("MenuCancel"));

            // Event handler for Context Menu
            this.selectedItemMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(this.SelectedItemMenuStrip_ItemClicked);
        }

        #endregion Context Menu

        #region Validation

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>Slice Validation Fields</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;
            string formulaString = string.Empty;
            DataRow[] invalidRows = null;

            //this.changeSet = (F36066TrendData.GetTrendListDataTable)this.trendData.GetTrendList.GetChanges();
            this.changeSet = (F36066TrendData.GetTrendListDataTable)this.trendData.GetTrendList.Copy();

            if (changeSet != null)
            {
                changeSet.Columns["EmptyRecord$"].Expression = this.SetExpression();

                // Filter string for required values not presents in rows
                formulaString = " (" + this.trendData.GetTrendList.YearColumn.ColumnName + " IS NULL or "
                                       + this.trendData.GetTrendList.YearColumn.ColumnName + " = '' or  "
                                       + this.trendData.GetTrendList.TrendColumn.ColumnName + " IS NULL or "
                                       + this.trendData.GetTrendList.TrendColumn.ColumnName + " = '') and ("
                                       + "EmptyRecord$ = False)";

                invalidRows = changeSet.Select(formulaString);
            }
           
            //DataRow[] invalidRows = this.trendData.GetTrendList.Select(formulaString);
            short tempRollYear;
            short.TryParse(this.RollYearTextBox.Text.Trim(), out tempRollYear);

            if ((invalidRows != null && invalidRows.Length > 0) || tempRollYear.Equals(0))
            {
                // if invalid rows presents in grid and invalid RollYear 
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
            }
            else if (this.IsDuplicateTrendYearExist())
            {
                // If repeated values presents in 'Year' column
                sliceValidationFields.ErrorMessage = "Trend Year must be unique.";
                sliceValidationFields.RequiredFieldMissing = true;
            }
            else if (this.IsRollYearExist(tempRollYear))
            {
                // If record already exists in the table tAA_PPTrendYear for entered rollYear
                sliceValidationFields.ErrorMessage = "There is already an existing record for the entered Roll Year.";
                sliceValidationFields.RequiredFieldMissing = true;
            }
            else
            {
                sliceValidationFields.ErrorMessage = string.Empty;
                sliceValidationFields.RequiredFieldMissing = false;
            }

            return sliceValidationFields;
        }

        #endregion Validation

        #region Get Trend Details

        /// <summary>
        /// Gets the trend year details.
        /// </summary>
        /// <returns>XMLString contains Trend Year details</returns>
        private string GetTrendYearDetails()
        {
            F36066TrendData.GetTrendDetailsDataTable trendYearTable = new F36066TrendData.GetTrendDetailsDataTable();
            F36066TrendData.GetTrendDetailsRow trendYearRow = trendYearTable.NewGetTrendDetailsRow();

            short rollYear;
            short.TryParse(this.RollYearTextBox.Text.Trim(), out rollYear);

            trendYearRow.RollYear = rollYear;
            trendYearRow.Description = this.DescriptionTextBox.Text.Trim();
            trendYearTable.Rows.Add(trendYearRow);
            return TerraScanCommon.GetXmlString(trendYearTable);
        }

        #endregion Get Trend Details

        #region Save Trend

        /// <summary>
        /// Saves the trend details.
        /// </summary>
        private void SaveTrendDetails()
        {
            // XMLString for Trend year details 
            string trendYearDetails = this.GetTrendYearDetails();
            DataTable changeSets = null;
            // Get modified/(newly added) row collecion
            if (this.trendData.GetTrendList.GetChanges() != null)
            {
                changeSets = this.trendData.GetTrendList.GetChanges().Copy();
            }
           
            // XMLString for Trend details (from datagridview)
            string trendItemsDetails;
            if (changeSets != null)
            {
                changeSets.Columns["EmptyRecord$"].Expression = this.SetExpression(); 
                changeSets.DefaultView.RowFilter = "EmptyRecord$ = False";
                DataTable changesToSave = changeSets.DefaultView.ToTable();
                trendItemsDetails = TerraScanCommon.GetXmlString(changesToSave);
            }
            else
            {
                trendItemsDetails = null;
            }

            // DB call for Save
            int returnValue;
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
            {
                returnValue = this.form36066Controller.WorkItem.SaveTrend(null, trendYearDetails, trendItemsDetails, TerraScanCommon.UserId);
            }
            else
            {
                returnValue = this.form36066Controller.WorkItem.SaveTrend(this.keyId, trendYearDetails, trendItemsDetails, TerraScanCommon.UserId);
            }

            this.keyId = returnValue;

            // Reload form after save
            SliceReloadActiveRecord sliceReloadActiveRecord;
            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
            sliceReloadActiveRecord.SelectedKeyId = returnValue;
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
        }

        #endregion Save Trend

        #region Trend ID Table

        /// <summary>
        /// Selecteds the trend id collection.
        /// </summary>
        /// <returns>XMLString contains selected rows trenids</returns>
        private string SelectedTrendIdCollection()
        {
            DataTable trendTable = new DataTable();
            trendTable.Columns.Add(this.TrendID.Name, typeof(int));
            DataGridViewSelectedRowCollection selectedRows = this.TrendGridView.SelectedRows ;
            
            foreach (DataGridViewRow dataRow in selectedRows)
            {
                if (dataRow.Cells[this.TrendID.Name].Value != null 
                    && !string.IsNullOrEmpty(dataRow.Cells[this.TrendID.Name].Value.ToString()))
                {
                    DataRow newRow = trendTable.NewRow();
                    newRow[this.TrendID.Name] = dataRow.Cells[this.TrendID.Name].Value.ToString();
                    trendTable.Rows.Add(newRow);
                }
            }

            if (trendTable.Rows.Count > 0)
            {
                return TerraScanCommon.GetXmlString(trendTable);
            }

            return null;
        }

        #endregion Trend ID Table

        #region Duplicate Record

        /// <summary>
        /// Determines whether [is duplicate year exist].
        /// </summary>
        /// <returns>
        /// <c>true</c> if [is duplicate year exist]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsDuplicateTrendYearExist()
        {
            // Create a dataview from the source GetTrendDetails 
            DataView trendView = new DataView(this.changeSet);
            
            // Filter view
            trendView.RowFilter = "EmptyRecord$ = false";

            // set the output column array of the destination table 
            string[] strColumns = { this.trendData.GetTrendList.YearColumn.ColumnName };

            // true = yes, to get distinct values. 
            DataTable distinctTable = trendView.ToTable(true, strColumns);
            
            // If distinct values count lesser than original row count return 'true'
            if (distinctTable.Rows.Count < trendView.Count)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether [is roll year exist] [the specified roll year].
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>
        /// <c>true</c> if [is roll year exist] [the specified roll year]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsRollYearExist(int rollYear)
        {
            int returnValue = 0;

            // Check whether the RollYear exists
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
            {
                returnValue = this.form36066Controller.WorkItem.CheckTrendRollYear(null, rollYear);
            }
            else
            {
                returnValue = this.form36066Controller.WorkItem.CheckTrendRollYear(this.keyId, rollYear);
            }


            // If the entered rollyear already exists return as 'true'
            if (returnValue.Equals(1))
            {
                return true;
            }

            return false;
        }

        #endregion Duplicate Record

        #region SetExpression

        /// <summary>
        /// Sets the expression.
        /// </summary>
        /// <returns>Expression String</returns>
        private string SetExpression()
        {
            // Replace EmptyRecord$ value as True for empty records
            string expressionString =   "IIF ((" + changeSet.YearColumn.ColumnName + " IS NULL or "
                                               + changeSet.YearColumn.ColumnName + " = '') and  ( "
                                               + changeSet.TrendColumn.ColumnName + " IS NULL or "
                                               + changeSet.TrendColumn.ColumnName + " = '') and "
                                               + changeSet.TrendIDColumn.ColumnName + " Is NULL, True, False) ";
            return expressionString;
        }

        #endregion SetExpression

        #region Delete

        /// <summary>
        /// Deletes the trend items.
        /// </summary>
        private void DeleteTrendItems()
        {
            if (this.TrendGridView.CurrentCell != null && this.slicePermissionField.deletePermission
                && this.TrendGridView.OriginalRowCount > 0 && this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                // DB call for delete
                this.form36066Controller.WorkItem.DeleteTrend(null, this.SelectedTrendIdCollection(), TerraScanCommon.UserId);

                // Reload form
                this.LoadTrendDetails();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                if (this.TrendGridView.OriginalRowCount <= 0)
                {
                    this.RollYearTextBox.Focus();
                }
            }
        }

        #endregion Delete

        

        #endregion Methods
    }
}
