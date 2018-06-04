// -------------------------------------------------------------------------------------------
// <copyright file="F36065.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// </summary>
// Release history
// ********************************************************************************************
// Date               Author            Description
// ----------        ---------     -------------------------------------------------------
// 11/08/2009        D.LathaMaheswari    Created
// -------------------------------------------------------------------------------------------
namespace D36065
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
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
    /// F36065 class
    /// </summary>
    [SmartPart]
    public partial class F36065 : BaseSmartPart
    {
        #region Variable
        /// <summary>
        /// Master Form number
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// Current Key Id[DepreciationYearId] of the form
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
        private F36065Controller form36065Controller;

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
        /// DataSet for Depreciation details
        /// </summary>
        private F36065PersonalDeprData depreciationData = new F36065PersonalDeprData();

        /// <summary>
        /// Flag for cell focus
        /// </summary>
        private bool isCellEnter;

        /// <summary>
        /// DataSet for Trend details
        /// </summary>
        private F36065PersonalDeprData.DepreciationTableDataTable changeSet;

        #endregion Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F36065"/> class.
        /// </summary>
        public F36065()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F36065"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="redColor">Color of the red.</param>
        /// <param name="greenColor">Color of the green.</param>
        /// <param name="blueColor">Color of the blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F36065(int masterform, int formNo, int keyId, int redColor, int greenColor, int blueColor, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyId;
            this.formMasterPermissionEdit = permissionEdit;
            this.HeaderPictureBox.Image = ExtendedGraphics.GenerateHorizontalImage(this.HeaderPictureBox.Height, this.HeaderPictureBox.Width, string.Empty, redColor, greenColor, blueColor);
            this.DepreciationPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DepreciationPictureBox.Height, this.DepreciationPictureBox.Width, tabText, redColor, greenColor, blueColor);
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
        /// Gets or sets the form36065 controller.
        /// </summary>
        /// <value>The form36065 controller.</value>
        [CreateNew]
        public F36065Controller Form36065Controller
        {
            get { return this.form36065Controller as F36065Controller; }
            set { this.form36065Controller = value; }
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
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearControls();
                this.depreciationData.Clear();
                this.DepreciationGridView.DataSource = this.depreciationData.DepreciationTable.DefaultView;
                this.depreciationData.AcceptChanges();
                this.EnableControls(true);
                this.LockControls(false);
                this.SetScrollBarVisibility();
                
                this.RollYearTextBox.Focus();
            }
            else
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearControls();
                this.depreciationData.Clear();
                this.DepreciationGridView.DataSource = this.depreciationData.DepreciationTable.DefaultView;
                this.depreciationData.AcceptChanges();
                this.LockControls(true);
                this.EnableControls(false);
                this.SetScrollBarVisibility();
            }
            this.DepreciationGridView.ClearSelection();
            // Reset the horizontal scroll position
            this.DepreciationGridView.HorizontalScrollingOffset = 0;
        }

        /// <summary>
        /// Event Subscription for cancel slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
        {
            this.ClearControls();
            this.flagLoadOnProcess = true;
            this.LoadDepreciationDetails();

            // Lock/Unlock controls based on the edit permission 
            this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit || this.depreciationData.DepreciationYearTable.Rows.Count == 0);
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.flagLoadOnProcess = false;
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
                this.SaveDepreciationDetails();
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
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                    if (this.depreciationData.DepreciationYearTable.Rows.Count > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
                        // Lock/Unlock controls based on the edit permission 
                        this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);

                        if (this.DepreciationGridView.OriginalRowCount > 0)
                        {
                            this.DepreciationGridView.Rows[0].Selected = true;
                        }
                        else
                        {
                            this.DepreciationGridView.Rows[0].Selected = false;
                        }

                        this.ActiveControl = this.RollYearTextBox;
                        this.ActiveControl.Focus();
                        //this.RollYearTextBox.Focus();
                    }
                    else
                    {
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            eventArgs.Data.FlagInvalidSliceKey = true;
                        }

                        this.DepreciationGridView.Rows[0].Selected = false;
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
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.flagLoadOnProcess = true;
                    this.LoadDepreciationDetails();
                    this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                    this.RollYearTextBox.Focus();
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
                this.form36065Controller.WorkItem.F36065_DeleteDepreciattion(this.keyId, this.SelectedRowsCollection(), TerraScanCommon.UserId);
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

        #region Events

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
                this.Cursor = Cursors.WaitCursor;
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;

                // Customize the Depreciation Grid
                this.CustomizeGrid();

                // Load Depreciation Details
                this.LoadDepreciationDetails();
                this.pageMode = TerraScanCommon.PageModeTypes.View;

                // Load Context Menu
                this.LoadContextMenu();

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

        #region Edit Enable

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

        #endregion Edit Enable

        #region Menu Strip Events

        /// <summary>
        /// Handles the ItemClicked event of the SelectedItemMenuStrip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.ToolStripItemClickedEventArgs"/> instance containing the event data.</param>
        private void SelectedItemMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (e.ClickedItem.Text.Equals(SharedFunctions.GetResourceString("Delete")))
                {
                    this.selectedItemMenuStrip.Visible = false;
                    if (this.DepreciationGridView.CurrentCell != null
                        && this.DepreciationGridView.Rows[this.DepreciationGridView.CurrentCell.RowIndex].Selected
                        && this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                    {
                        this.form36065Controller.WorkItem.F36065_DeleteDepreciattion(null, this.SelectedRowsCollection(), TerraScanCommon.UserId);
                        this.LoadDepreciationDetails();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
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
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Grid Events

        /// <summary>
        /// Handles the CellEndEdit event of the DepreciationGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DepreciationGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Add New Row 
                if (e.RowIndex.Equals(this.DepreciationGridView.Rows.Count - 1) && (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit) || this.pageMode.Equals(TerraScanCommon.PageModeTypes.New)))
                {
                    if (this.DepreciationGridView.Rows[e.RowIndex].Cells[this.PPDeprName.Name].Value != null
                        && !string.IsNullOrEmpty(this.DepreciationGridView.Rows[e.RowIndex].Cells[this.PPDeprName.Name].Value.ToString().Trim())
                        && this.DepreciationGridView.Rows[e.RowIndex].Cells[this.LifeExpectancy.Name].Value != null
                        && !string.IsNullOrEmpty(this.DepreciationGridView.Rows[e.RowIndex].Cells[this.LifeExpectancy.Name].Value.ToString().Trim()))
                    {
                        F36065PersonalDeprData.DepreciationTableRow newRow = this.depreciationData.DepreciationTable.NewDepreciationTableRow();
                        newRow["EmptyRecord$"] = "True";
                        this.depreciationData.DepreciationTable.AddDepreciationTableRow(newRow);
                        //this.depreciationData.DepreciationTable.AddDepreciationTableRow(this.depreciationData.DepreciationTable.NewDepreciationTableRow());
                        this.DepreciationGridScroll.Visible = false;
                        this.DepreciationGridView.Width = 769;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellParsing event of the DepreciationGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void DepreciationGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.Value.Equals(null))
                {
                    return;
                }

                if (e.ColumnIndex.Equals(this.LifeExpectancy.Index))
                {
                    // LifeExpectancy field validation
                    // Allow to enter the maximum of int value
                    if (!string.IsNullOrEmpty(e.Value.ToString().Trim()))
                    {
                        short outValue;
                        short.TryParse(e.Value.ToString().Trim(), out outValue);
                        if (outValue > short.MaxValue || outValue <= 0)
                        {
                            e.Value = (short)0;
                        }
                        else
                        {
                            e.Value = outValue;
                            //e.Value = DBNull.Value;
                        }

                    }
                    else
                    {
                        e.Value = DBNull.Value;
                    }
                    e.ParsingApplied = true;
                }

                if (this.IsMatch(this.DepreciationGridView.Columns[e.ColumnIndex].Name))
                {
                    string tempvalue = e.Value.ToString().Trim();
                    if (!string.IsNullOrEmpty(e.Value.ToString().Trim()))
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
                                // Allow non negative values
                                outDecimal = decimal.Zero;
                            }
                         
                            if (outDecimal >= 1000)
                            {
                                outDecimal = decimal.Zero;
                            }
                        }

                        e.Value = outDecimal;
                        e.ParsingApplied = true;
                    }
                    else
                    {
                        e.Value = e.Value.ToString().Trim();
                        e.Value = DBNull.Value;
                    }
                    e.ParsingApplied = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the DepreciationGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void DepreciationGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                // Grid TextBox controls
                if (e.Control is DataGridViewTextBoxEditingControl)
                {
                    e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                    e.Control.Validating -= new CancelEventHandler(this.Control_Validated);
                    e.Control.Validating += new CancelEventHandler(this.Control_Validated);
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
        private void Control_Validated(object sender, CancelEventArgs e)
        {
            try
            {
                this.DepreciationGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
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
                    // Enable Form Master Save/Cancel button
                    this.EditEnabled();
                }
                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void DepreciationGridView_CellBeginEdit(object sender, System.Windows.Forms.DataGridViewCellCancelEventArgs e)
        {
            try
            {
                this.isCellEnter = true;
                //this.EditEnabled();
                if (this.DepreciationGridView.EditingControl != null)
                {
                    this.DepreciationGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the DepreciationGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void DepreciationGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals((Keys.Back | Keys.Space)) && e.Shift)
                {
                    this.DepreciationGridView.CurrentRow.Selected = true;
                }

                if (e.KeyCode.Equals(Keys.Delete) && this.DepreciationGridView.CurrentCell != null
                    && this.slicePermissionField.deletePermission && this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.form36065Controller.WorkItem.F36065_DeleteDepreciattion(null, this.SelectedRowsCollection(), TerraScanCommon.UserId);
                    this.LoadDepreciationDetails();
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
        /// Handles the MouseUp event of the DepreciationGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void DepreciationGridView_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (e.Button.Equals(MouseButtons.Right) && this.slicePermissionField.deletePermission
                    && this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    if (this.DepreciationGridView.CurrentRow != null && this.DepreciationGridView.CurrentRow.Index >= 0
                    && this.DepreciationGridView.CurrentRow.Selected)// && !string.IsNullOrEmpty(this.DepreciationGridView[this.PPDeprTableId.Name, this.DepreciationGridView.CurrentRow.Index].Value.ToString().Trim()))
                    {
                        this.selectedItemMenuStrip.Show(this.DepreciationGridView, new Point(e.X, e.Y));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the DepreciationGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DepreciationGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
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
        /// Handles the CellFormatting event of the DepreciationGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void DepreciationGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.Value.Equals(null))
                {
                    return;
                }

                // Format for year columns (Year1 to Year50)
                if (this.IsMatch(this.DepreciationGridView.Columns[e.ColumnIndex].Name))
                {
                    if (!String.IsNullOrEmpty(e.Value.ToString()))
                    {
                        decimal outValue;
                        if (decimal.TryParse(e.Value.ToString(), out outValue))
                        {
                            e.Value = outValue.ToString("#,##0.00");
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
                    }
                }
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
            //this.depreciationData.Clear();
            //this.DepreciationGridView.DataSource = this.depreciationData.DepreciationTable.DefaultView;
            //this.depreciationData.AcceptChanges();
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
            this.DepreciationGridPanel.Enabled = !hasLocked;
        }

        /// <summary>
        /// Enables the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableControls(bool enable)
        {
            this.HeaderPanel.Enabled = enable;
            this.DepreciationGridPanel.Enabled = enable;
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
                this.DepreciationGridView.AllowSorting = false;
            }
        }

        #endregion Enable Edit

        #region Grid Customization

        /// <summary>
        /// Customizes the grid.
        /// </summary>
        private void CustomizeGrid()
        {
            this.DepreciationGridView.AutoGenerateColumns = false;
            this.PPDeprTableId.DataPropertyName = this.depreciationData.DepreciationTable.PPDeprTableIDColumn.ColumnName;
            this.DeprYearId.DataPropertyName = this.depreciationData.DepreciationTable.DeprYearIDColumn.ColumnName;
            this.LifeExpectancy.DataPropertyName = this.depreciationData.DepreciationTable.LifeExpectancyColumn.ColumnName;
            this.PPDeprName.DataPropertyName = this.depreciationData.DepreciationTable.PPDeprNameColumn.ColumnName;

            // Assigning data property name for 50 year columns (year1 to year50)
            for (int yearCount = 1; yearCount <= 50; yearCount++)
            {
                string yearColumn = "Year" + yearCount;
                this.DepreciationGridView.Columns[yearColumn].DataPropertyName = yearColumn;
            }

            this.DepreciationGridView.PrimaryKeyColumnName = this.depreciationData.DepreciationTable.PPDeprTableIDColumn.ColumnName;
            this.DepreciationGridView.ScrollBars = ScrollBars.Both;
        }

        #endregion Grid Customization

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

        #region ScrollBar Visibility

        /// <summary>
        /// Sets the scroll bar visibility.
        /// </summary>
        private void SetScrollBarVisibility()
        {
            if (this.depreciationData.DepreciationTable.Rows.Count <= this.DepreciationGridView.NumRowsVisible)
            {
                this.DepreciationGridScroll.Visible = true;
                this.DepreciationGridView.Width = 754;
            }
            else
            {
                this.DepreciationGridScroll.Visible = false;
                this.DepreciationGridView.Width = 769;
            }
        }

        #endregion ScrollBar Visibility

        #region Read Only

        /// <summary>
        /// Sets the read only.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void SetReadOnly(int rowIndex)
        {
            if (rowIndex > 0)
            {
                if ((string.IsNullOrEmpty(this.DepreciationGridView[this.PPDeprName.Name, rowIndex - 1].Value.ToString().Trim())
                    && string.IsNullOrEmpty(this.DepreciationGridView[this.LifeExpectancy.Name, rowIndex - 1].Value.ToString().Trim())))
                {
                    if (rowIndex < this.DepreciationGridView.OriginalRowCount)
                    {
                        this.DepreciationGridView.Rows[rowIndex].ReadOnly = false;
                    }
                    else
                    {
                        this.DepreciationGridView.Rows[rowIndex].ReadOnly = true;
                    }
                }
                else
                {
                    this.DepreciationGridView.Rows[rowIndex].ReadOnly = false;
                }
            }
            else if (rowIndex.Equals(0))
            {
                this.DepreciationGridView.Rows[rowIndex].ReadOnly = false;
            }
        }

        #endregion Read Only

        #region Load Depreciation Details

        /// <summary>
        /// Gets the Depreciation details.
        /// </summary>
        private void LoadDepreciationDetails()
        {
            this.depreciationData = this.form36065Controller.WorkItem.F36065_GetDeprDetails(this.keyId);
            this.SetControlValue();
            this.depreciationData.AcceptChanges();

            if (this.DepreciationGridView.OriginalRowCount > 0)
            {
                this.RollYearTextBox.Focus();
                this.DepreciationGridView.Rows[0].Selected = true;
            }
            else
            {
                this.DepreciationGridView.ClearSelection();
                this.RollYearTextBox.Focus();
                //this.DepreciationGridView.Rows[0].Selected = false;
            }

            this.DepreciationGridView.AllowSorting = true;
            // Reset the horizontal scroll position
            this.DepreciationGridView.HorizontalScrollingOffset = 0;
        }

        #endregion Load Depreciation Details

        #region Control Value

        /// <summary>
        /// Sets the control value.
        /// </summary>
        private void SetControlValue()
        {
            // Assign value on appropriate controls
            this.DepreciationGridView.DataSource = this.depreciationData.DepreciationTable.DefaultView;
            if (this.depreciationData.DepreciationYearTable.Rows.Count > 0)
            {
                F36065PersonalDeprData.DepreciationYearTableRow currentRow = (F36065PersonalDeprData.DepreciationYearTableRow)this.depreciationData.DepreciationYearTable.Rows[0];
                this.RollYearTextBox.Text = currentRow.RollYear.ToString();

                if (!currentRow.IsDescriptionNull() && !string.IsNullOrEmpty(currentRow.Description))
                {
                    this.DescriptionTextBox.Text = currentRow.Description.ToString();
                }
                else
                {
                    this.DescriptionTextBox.Text = string.Empty;
                }

                if (this.DepreciationGridView.OriginalRowCount > 0)
                {
                    if (this.DepreciationGridView.OriginalRowCount >= this.DepreciationGridView.NumRowsVisible)
                    {
                        F36065PersonalDeprData.DepreciationTableRow newRow = this.depreciationData.DepreciationTable.NewDepreciationTableRow();
                        newRow["EmptyRecord$"] = "True";
                        this.depreciationData.DepreciationTable.AddDepreciationTableRow(newRow);
                    }
                }

                this.EnableControls(true);
            }
            else
            {
                this.ClearControls();
                this.EnableControls(false);
            }

            this.SetScrollBarVisibility();
        }

        #endregion Control Value

        #region Validation

        /// <summary>
        /// Determines whether [is roll year exist] [the specified roll year].
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>
        ///  <c>true</c> if [is roll year exist] [the specified roll year]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsRollYearExist(int rollYear)
        {
            int returnValue = 0;

            // Check whether the RollYear exists
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
            {
                returnValue = this.form36065Controller.WorkItem.F36065_CheckDeprRollYear(null, rollYear);
            }
            else
            {
                returnValue = this.form36065Controller.WorkItem.F36065_CheckDeprRollYear(this.keyId, rollYear);
            }

            // If the entered rollyear already exists return as 'true'
            if (returnValue.Equals(1))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether [is duplicate name exist].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is duplicate name exist]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsDuplicateNameExist()
        {
            // Create a dataview from the source GetTrendDetails 
            DataView deprView = new DataView(this.changeSet);

            // Filter view
            deprView.RowFilter = "EmptyRecord$ = false";

            // set the output column array of the destination table 
            string[] strColumns = { this.depreciationData.DepreciationTable.PPDeprNameColumn.ColumnName };

            // true = yes, to get distinct values. 
            DataTable distinctTable = deprView.ToTable(true, strColumns);

            // If distinct values count lesser than original row count return 'true'
            if (distinctTable.Rows.Count < deprView.Count)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified value is match.
        /// </summary>
        /// <param name="textToCompare">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is match; otherwise, <c>false</c>.
        /// </returns>
        private bool IsMatch(string textToCompare)
        {
            // Match the string which starts with and contains only the text as 'Year' and ends with numeric
            // Don't allow any string other than 'Year'and numeric
            // Regular expression for the above condition is '^Year[0-5]?[0-9]$'
            System.Text.RegularExpressions.Regex objRegex = new System.Text.RegularExpressions.Regex("^Year[0-5]?[0-9]$");

            // If the text(textToCompare) matches with the regular expression return true
            if (objRegex.IsMatch(textToCompare))
            {
                return true;
            }
         
            return false;
        }

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

            this.changeSet = (F36065PersonalDeprData.DepreciationTableDataTable)this.depreciationData.DepreciationTable.Copy();
            if (changeSet != null)
            {
                changeSet.Columns["EmptyRecord$"].Expression = this.SetExpression();

                // Filter string for required values not presents in rows
                formulaString = " (" + this.depreciationData.DepreciationTable.PPDeprNameColumn.ColumnName + " IS NULL or "
                                        + this.depreciationData.DepreciationTable.PPDeprNameColumn.ColumnName + " = '' or "
                                      + this.depreciationData.DepreciationTable.LifeExpectancyColumn.ColumnName + " IS NULL or "
                                      + this.depreciationData.DepreciationTable.LifeExpectancyColumn.ColumnName + " = 0 "
                                      + ") and EmptyRecord$ = False";
                invalidRows = changeSet.Select(formulaString);
            }

            try
            {
                //invalidRows = this.depreciationData.DepreciationTable.Select(formulaString);
                short tempRollYear;
                short.TryParse(this.RollYearTextBox.Text.Trim(), out tempRollYear);

                if ((invalidRows != null && invalidRows.Length > 0) || tempRollYear.Equals(0))
                {
                    // if invalid rows presents in grid and
                    // invalid RollYear presents in RollYearTextBox
                    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                    sliceValidationFields.RequiredFieldMissing = true;
                }
                else if (this.IsDuplicateNameExist())
                {
                    // If repeated values presents in 'Year' column
                    sliceValidationFields.ErrorMessage = "Name must be unique.";
                    sliceValidationFields.RequiredFieldMissing = true;
                }
                else if (this.IsRollYearExist(tempRollYear))
                {
                    // If record already exists in the table tAA_PPDeprYear for entered rollYear
                    sliceValidationFields.ErrorMessage = "There is already an existing record for the entered Roll Year.";
                    sliceValidationFields.RequiredFieldMissing = true;
                }
                else
                {
                    sliceValidationFields.ErrorMessage = string.Empty;
                    sliceValidationFields.RequiredFieldMissing = false;
                }
            }
            catch (Exception ex)
            {
            }

            return sliceValidationFields;
        }

        #endregion Validation

        #region Save Depreciation

        /// <summary>
        /// Saves the trend details.
        /// </summary>
        private void SaveDepreciationDetails()
        {
            // XMLString for Description year details 
            string deprYearDetails = this.GetDeprYearDetails();
           
            // Get modified/(newly added) row collecion
            //DataTable changeSet = this.depreciationData.DepreciationTable.GetChanges();

            DataTable changeSets = null;
            // Get modified/(newly added) row collecion
            if (this.depreciationData.DepreciationTable.GetChanges() != null)
            {
                changeSets = this.depreciationData.DepreciationTable.GetChanges().Copy();
            }

            // XMLString for Depreciation details (from datagridview)
            string depreciationItems;
            if (changeSets != null)
            {
                changeSets.Columns["EmptyRecord$"].Expression = this.SetExpression();
                changeSets.DefaultView.RowFilter = "EmptyRecord$ = False";
                DataTable changesToSave = changeSets.DefaultView.ToTable();
                depreciationItems = TerraScanCommon.GetXmlString(changesToSave);
            }
            else
            {
                depreciationItems = null;
            }

            // DB call for Save
            int returnValue;
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
            {
                returnValue = this.form36065Controller.WorkItem.F36065_SaveDepreciation(null, deprYearDetails, depreciationItems, TerraScanCommon.UserId);
            }
            else
            {
                returnValue = this.form36065Controller.WorkItem.F36065_SaveDepreciation(this.keyId, deprYearDetails, depreciationItems, TerraScanCommon.UserId);
            }

            this.keyId = returnValue;

            // Reload form after save
            SliceReloadActiveRecord sliceReloadActiveRecord;
            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
            sliceReloadActiveRecord.SelectedKeyId = returnValue;
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
        }

        #endregion Save Depreciation

        #region Get Depreciation Details

        /// <summary>
        /// Gets the depr year details.
        /// </summary>
        /// <returns>Depreciation Year details</returns>
        private string GetDeprYearDetails()
        {
            F36065PersonalDeprData.DepreciationYearTableDataTable deprYearTable = new F36065PersonalDeprData.DepreciationYearTableDataTable();
            F36065PersonalDeprData.DepreciationYearTableRow deprYearRow = deprYearTable.NewDepreciationYearTableRow();

            short rollYear;
            short.TryParse(this.RollYearTextBox.Text.Trim(), out rollYear);

            deprYearRow.RollYear = rollYear;
            deprYearRow.Description = this.DescriptionTextBox.Text.Trim();
            deprYearTable.Rows.Add(deprYearRow);
            return TerraScanCommon.GetXmlString(deprYearTable);
        }

        #endregion Get Depreciation Details

        #region Trend ID Table

        /// <summary>
        /// Selecteds the rows collection.
        /// </summary>
        /// <returns>Selected rows yearid</returns>
        private string SelectedRowsCollection()
        {
            DataTable deprTable = new DataTable();
            deprTable.Columns.Add("PPDeprTableID", typeof(int));
            ////DataGridViewRow[] selectedRows = new DataGridViewRow[this.DepreciationGridView.SelectedRows.Count];
            ////this.DepreciationGridView.SelectedRows.CopyTo(selectedRows, 0);

            DataGridViewSelectedRowCollection selectedRows = this.DepreciationGridView.SelectedRows;
          
            foreach (DataGridViewRow dataRow in selectedRows)
            {
                if (dataRow.Cells[this.PPDeprTableId.Name].Value != null
                    && !string.IsNullOrEmpty(dataRow.Cells[this.PPDeprTableId.Name].Value.ToString()))
                {
                    DataRow newRow = deprTable.NewRow();
                    newRow["PPDeprTableID"] = dataRow.Cells[this.PPDeprTableId.Name].Value.ToString();
                    deprTable.Rows.Add(newRow);
                }
            }

            if (deprTable.Rows.Count > 0)
            {
                return TerraScanCommon.GetXmlString(deprTable);
            }

            return null;
        }

        #endregion Trend ID Table

        #region SetExpression

        /// <summary>
        /// Sets the expression.
        /// </summary>
        /// <returns>Expression String</returns>
        private string SetExpression()
        {
            // Replace EmptyRecord$ value as True for empty records
            string expressionString = "IIF ((" + changeSet.PPDeprNameColumn.ColumnName + " IS NULL or "
                                               + changeSet.PPDeprNameColumn.ColumnName + " = '') and  ( "
                                               + changeSet.LifeExpectancyColumn.ColumnName + " IS NULL or "
                                               + changeSet.LifeExpectancyColumn.ColumnName + " = 0) and "
                                               + changeSet.Year1Column.ColumnName + " IS NULL and "
                                               + changeSet.Year2Column.ColumnName + " IS NULL and "
                                               + changeSet.Year3Column.ColumnName + " IS NULL and "
                                               + changeSet.Year4Column.ColumnName + " IS NULL and "
                                               + changeSet.Year5Column.ColumnName + " IS NULL and "
                                               + changeSet.Year6Column.ColumnName + " IS NULL and "
                                               + changeSet.Year7Column.ColumnName + " IS NULL and "
                                               + changeSet.Year8Column.ColumnName + " IS NULL and "
                                               + changeSet.Year9Column.ColumnName + " IS NULL and "
                                               + changeSet.Year10Column.ColumnName + " IS NULL and "
                                               + changeSet.Year11Column.ColumnName + " IS NULL and "
                                               + changeSet.Year12Column.ColumnName + " IS NULL and "
                                               + changeSet.Year13Column.ColumnName + " IS NULL and "
                                               + changeSet.Year14Column.ColumnName + " IS NULL and "
                                               + changeSet.Year15Column.ColumnName + " IS NULL and "
                                               + changeSet.Year16Column.ColumnName + " IS NULL and "
                                               + changeSet.Year17Column.ColumnName + " IS NULL and "
                                               + changeSet.Year18Column.ColumnName + " IS NULL and "
                                               + changeSet.Year19Column.ColumnName + " IS NULL and "
                                               + changeSet.Year20Column.ColumnName + " IS NULL and "
                                               + changeSet.Year21Column.ColumnName + " IS NULL and "
                                               + changeSet.Year22Column.ColumnName + " IS NULL and "
                                               + changeSet.Year23Column.ColumnName + " IS NULL and "
                                               + changeSet.Year24Column.ColumnName + " IS NULL and "
                                               + changeSet.Year25Column.ColumnName + " IS NULL and "
                                               + changeSet.Year26Column.ColumnName + " IS NULL and "
                                               + changeSet.Year27Column.ColumnName + " IS NULL and "
                                               + changeSet.Year28Column.ColumnName + " IS NULL and "
                                               + changeSet.Year29Column.ColumnName + " IS NULL and "
                                               + changeSet.Year30Column.ColumnName + " IS NULL and "
                                               + changeSet.Year31Column.ColumnName + " IS NULL and "
                                               + changeSet.Year32Column.ColumnName + " IS NULL and "
                                               + changeSet.Year33Column.ColumnName + " IS NULL and "
                                               + changeSet.Year34Column.ColumnName + " IS NULL and "
                                               + changeSet.Year35Column.ColumnName + " IS NULL and "
                                               + changeSet.Year36Column.ColumnName + " IS NULL and "
                                               + changeSet.Year37Column.ColumnName + " IS NULL and "
                                               + changeSet.Year38Column.ColumnName + " IS NULL and "
                                               + changeSet.Year39Column.ColumnName + " IS NULL and "
                                               + changeSet.Year40Column.ColumnName + " IS NULL and "
                                               + changeSet.Year41Column.ColumnName + " IS NULL and "
                                               + changeSet.Year42Column.ColumnName + " IS NULL and "
                                               + changeSet.Year43Column.ColumnName + " IS NULL and "
                                               + changeSet.Year44Column.ColumnName + " IS NULL and "
                                               + changeSet.Year45Column.ColumnName + " IS NULL and "
                                               + changeSet.Year46Column.ColumnName + " IS NULL and "
                                               + changeSet.Year47Column.ColumnName + " IS NULL and "
                                               + changeSet.Year48Column.ColumnName + " IS NULL and "
                                               + changeSet.Year49Column.ColumnName + " IS NULL and "
                                               + changeSet.Year50Column.ColumnName + " IS NULL and "
                                               + changeSet.PPDeprTableIDColumn.ColumnName + " Is NULL, True, False) ";
            return expressionString;
        }

        #endregion SetExpression
        
        #endregion Methods
    }
}
