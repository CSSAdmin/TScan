//--------------------------------------------------------------------------------------------
// <copyright file="F28000.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F28000 Discretionary Exemption.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 26/04/2011        D.LathaMaheswari  Created
//***********************************************************************************************/

namespace D23000
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
    /// 28000
    /// </summary>
    [SmartPart]
    public partial class F28000 : BaseSmartPart
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
        /// Instance of 28000 Controller to call the WorkItem
        /// </summary>
        private F28000Controller form28000Control;

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
        /// flag LoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// DataSet for Discretionary details
        /// </summary>
        private F28000DiscretionaryData discretionaryData = new F28000DiscretionaryData();

        /// <summary>
        /// DataSet for Discretionary details
        /// </summary>
        private F28000DiscretionaryData.DiscretionaryDetailsDataTable changeSet;

        /// <summary>
        /// An object for ContextMenuStrip
        /// </summary>
        private ContextMenuStrip selectedItemMenuStrip = new ContextMenuStrip();

        /// <summary>
        /// To store current Discretionary ID
        /// </summary>
        private int discretionaryId = 0;

        #endregion Variables

        #region constructor

        public F28000()
        {
            InitializeComponent();
        }

        /// <summary>
        /// F28000 constructor
        /// </summary>
        /// <param name="masterform">Master Form Number</param>
        /// <param name="formNo">Form number</param>
        /// <param name="keyID">the Key Id</param>
        /// <param name="red">The Red Color</param>
        /// <param name="green">The Green Color</param>
        /// <param name="blue">the Blue Color</param>
        /// <param name="tabText">Tab texr</param>
        /// <param name="permissionEdit">Edit permission</param>
        public F28000(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.Tag = formNo;
            this.ExemptionPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ExemptionPictureBox.Height, this.ExemptionPictureBox.Width, tabText, red, green, blue);
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
        /// Gets or sets the form28000 controller.
        /// </summary>
        /// <value>The form28000 controller.</value>
        [CreateNew]
        public F28000Controller Form28000Controller
        {
            get { return this.form28000Control as F28000Controller; }
            set { this.form28000Control = value; }
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
            //if (this.slicePermissionField.newPermission)
            //{
            //    // Clear all the controls value and make it enable
            //    this.pageMode = TerraScanCommon.PageModeTypes.New;
            //    //this.ClearControls();
            //    this.discretionaryData.Clear();
            //    this.ExemptionGridView.DataSource = this.discretionaryData.DiscretionaryDetails.DefaultView;
            //    this.discretionaryData.AcceptChanges();
            //    this.EnableControls(true);
            //    this.LockControls(false);
            //    //// Set focus on firs editanle field
            //    //this.RollYearTextBox.Focus();
            //}
            //else
            //{
            //    // Clear all the controls and make it disable
            //    this.pageMode = TerraScanCommon.PageModeTypes.New;
            //    //this.ClearControls();
            //    this.discretionaryData.Clear();
            //    this.ExemptionGridView.DataSource = this.discretionaryData.DiscretionaryDetails.DefaultView;
            //    this.discretionaryData.AcceptChanges();
            //    this.EnableControls(false);
            //    this.LockControls(true);
            //    //this.TrendGridView.Focus();
            //}
        }

        /// <summary>
        /// Event Subscription for cancel slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
        {
            //// Clear all the controls value
            //this.ClearControls();

            //// Load Discretionary details
            this.LoadDiscretionaryDetails();

            //// Lock/Unlock controls based on the edit permission 
            this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit || this.discretionaryData.Discretionary.Rows.Count == 0);
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
                //this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
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
                this.SaveDiscretionaryDetails();
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
                    if (this.discretionaryData.Discretionary.Rows.Count > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;

                        // Lock/Unlock controls based on the edit permission 
                        this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                        this.RollYearTextBox.Focus();

                        if (this.ExemptionGridView.OriginalRowCount > 0)
                        {
                            this.ExemptionGridView.Rows[0].Selected = true;
                        }
                        else
                        {
                            this.ExemptionGridView.Rows[0].Selected = false;
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

                    //// Load Discretionary Details
                    this.LoadDiscretionaryDetails();
                    //// Lock controls based on edit permission
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

        ///// <summary>
        ///// Called when [D9030_ F9030_ delete slice information].
        ///// </summary>
        ///// <param name="sender">The sender.</param>
        ///// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        //[EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        //public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        //{
        //    if (this.slicePermissionField.deletePermission)
        //    {
        //        this.Cursor = Cursors.WaitCursor;
        //        int discretionaryId = 0;
        //        if (this.discretionaryData.Discretionary.Rows.Count > 0 && this.discretionaryData.Discretionary.Rows[0][this.discretionaryData.Discretionary.DiscretionaryIDColumn.ColumnName] != null
        //            && !string.IsNullOrEmpty(this.discretionaryData.Discretionary.Rows[0][this.discretionaryData.Discretionary.DiscretionaryIDColumn.ColumnName].ToString()))
        //        {
        //            int.TryParse(this.discretionaryData.Discretionary.Rows[0][this.discretionaryData.Discretionary.DiscretionaryIDColumn.ColumnName].ToString(), out discretionaryId);
        //        }

        //        //// DB call for Delete
        //        this.form28000Control.WorkItem.F28000_DeletediscretionaryDetails(discretionaryId, this.SelectedDetailIdCollection(), TerraScanCommon.UserId);
        //        SliceFormCloseAlert sliceFormCloseAlert;
        //        sliceFormCloseAlert.FormNo = this.masterFormNo;
        //        sliceFormCloseAlert.FlagFormClose = false;
        //        this.OnFormSlice_FormCloseAlert(new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
        //        this.Cursor = Cursors.Default;
        //    }
        //}

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

        #region Form load

        private void F28000_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                this.Cursor = Cursors.WaitCursor;

                // Customize the Detail Grid
                this.CustomizeGridView();

                this.LoadContextMenu();
                // Load Discretionary Details
                this.LoadDiscretionaryDetails();

                this.pageMode = TerraScanCommon.PageModeTypes.View;

                //// Set focus on first editable field
                //this.RollYearTextBox.Focus();
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
                    // Delete Discretionary details based on the selected records
                    this.DeleteDiscretionaryItems();
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

        #endregion;

        #region Grid Events

        private void ExemptionGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Add New Row 
                if (e.RowIndex.Equals(this.ExemptionGridView.Rows.Count - 1) && this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                {
                    //if (this.ExemptionGridView.Rows[e.RowIndex].Cells[this.Year.Name].Value != null
                    //    && !string.IsNullOrEmpty(this.ExemptionGridView.Rows[e.RowIndex].Cells[this.Year.Name].Value.ToString().Trim())
                    //    && this.ExemptionGridView.Rows[e.RowIndex].Cells[this.Trend.Name].Value != null
                    //    && !string.IsNullOrEmpty(this.ExemptionGridView.Rows[e.RowIndex].Cells[this.Trend.Name].Value.ToString().Trim()))
                    //{
                        F28000DiscretionaryData.DiscretionaryDetailsRow newRow = this.discretionaryData.DiscretionaryDetails.NewDiscretionaryDetailsRow();
                        newRow["EmptyRecord$"] = "True";
                        this.discretionaryData.DiscretionaryDetails.AddDiscretionaryDetailsRow(newRow);
                        this.ExemptionVscrollBar.Visible = false;
                    //}
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void ExemptionGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.Value.Equals(null))
                {
                    return;
                }

                if (e.ColumnIndex.Equals(this.ExemptionYear.Index))
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
                            if (tempYear > 1753 && tempYear <= 9999)
                            {
                                e.Value = tempYear.ToString();
                            }
                            else
                            {
                                e.Value = string.Empty;
                            }
                        }

                        e.ParsingApplied = true;
                    }
                    else
                    {
                        e.Value = string.Empty;
                    }
                }

                if (e.ColumnIndex.Equals(this.SubjectAmount.Index))
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

                        if (decimal.TryParse(tempvalue, out outDecimal))
                        {
                            tempvalue = outDecimal.ToString();

                            if (tempvalue.Contains("-"))
                            {
                                // Restrict negative values
                                outDecimal = decimal.Zero;
                            }

                            //if (outDecimal >= 1000)
                            //{
                            //    outDecimal = decimal.Zero;
                            //}
                        }

                        e.Value = outDecimal.ToString("#,##0");
                        e.ParsingApplied = true;
                    }
                    else
                    {
                        e.Value = string.Empty;
                    }
                }

                    if (e.ColumnIndex.Equals(this.ExemptionYear.Index) || e.ColumnIndex.Equals(this.SubjectAmount.Index))
                    {
                        int exemptionYear = 0;
                        if (e.ColumnIndex.Equals(this.ExemptionYear.Index))
                        {
                            int.TryParse(e.Value.ToString(), out exemptionYear);
                        }
                        else
                        {
                            int.TryParse(this.ExemptionGridView.Rows[e.RowIndex].Cells[this.ExemptionYear.Name].Value.ToString(), out exemptionYear);
                        }

                        int subjectAmount = 0;
                        if (e.ColumnIndex.Equals(this.SubjectAmount.Index))
                        {
                            int.TryParse(e.Value.ToString().Replace(",", "").Trim(), out subjectAmount);
                        }
                        else
                        {
                            int.TryParse(this.ExemptionGridView.Rows[e.RowIndex].Cells[this.SubjectAmount.Name].Value.ToString().Replace(",","").Replace(".0000","").Trim(), out subjectAmount);
                        }

                        int rollYear = 0;
                        int.TryParse(this.RollYearTextBox.Text.Trim(), out rollYear);
                        this.discretionaryData.ExemptionAmount.Clear();
                        this.discretionaryData.Merge(this.form28000Control.WorkItem.F28000_GetExemptionAmount(rollYear, exemptionYear, subjectAmount).ExemptionAmount);
                        if (this.discretionaryData.ExemptionAmount.Rows.Count > 0)
                        {
                            if (this.discretionaryData.ExemptionAmount.Rows[0][this.discretionaryData.ExemptionAmount.ExemptionAmountColumn.ColumnName] != null
                                && !string.IsNullOrEmpty(this.discretionaryData.ExemptionAmount.Rows[0][this.discretionaryData.ExemptionAmount.ExemptionAmountColumn.ColumnName].ToString()))
                            {
                                this.ExemptionGridView.Rows[e.RowIndex].Cells[this.ExemptionAmount.Name].Value = decimal.Parse(this.discretionaryData.ExemptionAmount.Rows[0][this.discretionaryData.ExemptionAmount.ExemptionAmountColumn.ColumnName].ToString());
                            }
                            else
                            {
                                this.ExemptionGridView.Rows[e.RowIndex].Cells[this.ExemptionAmount.Name].Value = string.Empty;
                            }
                        }
                        else
                        {
                            this.ExemptionGridView.Rows[e.RowIndex].Cells[this.ExemptionAmount.Name].Value = string.Empty;
                        }

                        this.CalculateExemptionAmount();
                    }
                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void ExemptionGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.Value.Equals(null))// || !e.ColumnIndex.Equals(this.ExemptionAmount.Index) || !e.ColumnIndex.Equals(this.SubjectAmount.Index))
                {
                    return;
                }

                // Format for SubjectAmount column
                if (e.ColumnIndex.Equals(this.SubjectAmount.Index))
                {
                    if (!String.IsNullOrEmpty(this.ExemptionGridView.Rows[e.RowIndex].Cells[this.SubjectAmount.Name].Value.ToString()))
                    {
                        decimal trendValue;
                        if (decimal.TryParse(e.Value.ToString(), out trendValue))
                        {
                            e.Value = trendValue.ToString("#,##0");
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

                // Format for Exemption Amount column
                if (e.ColumnIndex.Equals(this.ExemptionAmount.Index))
                {
                    if (!String.IsNullOrEmpty(this.ExemptionGridView.Rows[e.RowIndex].Cells[this.ExemptionAmount.Name].Value.ToString()))
                    {
                        decimal trendValue;
                        if (decimal.TryParse(e.Value.ToString(), out trendValue))
                        {
                            e.Value = trendValue.ToString("#,##0");
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
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ExemptionGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Delete) || e.KeyCode.Equals(Keys.Back))
                {
                    // Delete Trend details based on the record selection in Grid
                    this.DeleteDiscretionaryItems();
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

        private void ExemptionGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                // Grid TextBox controls
                if (e.Control is DataGridViewTextBoxEditingControl)
                {
                    e.Control.TextChanged -= new EventHandler(this.Control_TextChanged);
                    e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                    e.Control.Validated -= new EventHandler(this.Control_Validated);
                    e.Control.Validated += new EventHandler(this.Control_Validated);
                }
                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    ((ComboBox)e.Control).SelectionChangeCommitted -= new EventHandler(this.StateSelectionChangeCommitted);
                    ((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(this.StateSelectionChangeCommitted);
                }
                //this.isCellEnter = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        /// <summary>
        /// Handles the CropCodeSelectionChangeCommitted event of the F36041 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StateSelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (((ComboBox)sender).SelectedValue != null)
                {
                    this.discretionaryData.ClassDetail.Clear();
                    this.discretionaryData.Merge(this.form28000Control.WorkItem.F28000_GetClass(int.Parse(((ComboBox)sender).SelectedValue.ToString()), this.keyId).ClassDetail);

                    if (this.discretionaryData.ClassDetail.Rows.Count > 0)
                    {
                        if (this.discretionaryData.ClassDetail.Rows[0][this.discretionaryData.ClassDetail.ClassColumn.ColumnName] != null
                            && !string.IsNullOrEmpty(this.discretionaryData.ClassDetail.Rows[0][this.discretionaryData.ClassDetail.ClassColumn.ColumnName].ToString()))
                        {
                            this.ExemptionGridView.Rows[this.ExemptionGridView.CurrentRowIndex].Cells[this.discretionaryData.DiscretionaryDetails.ClassColumn.ColumnName].Value = this.discretionaryData.ClassDetail.Rows[0][0].ToString();
                        }
                        else
                        {
                            this.ExemptionGridView.Rows[this.ExemptionGridView.CurrentRowIndex].Cells[this.discretionaryData.DiscretionaryDetails.ClassColumn.ColumnName].Value = string.Empty;
                        }
                    }
                    else
                    {
                        this.ExemptionGridView.Rows[this.ExemptionGridView.CurrentRowIndex].Cells[this.discretionaryData.DiscretionaryDetails.ClassColumn.ColumnName].Value = string.Empty;
                    }
                }

                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        private void ExemptionGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
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
        /// Handles the Validated event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_Validated(object sender, EventArgs e)
        {
            try
            {
                this.ExemptionGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
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
                //if (!this.isCellEnter)
                //{
                    this.EditEnabled();
                //}
                this.ExemptionGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void ExemptionGridView_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button.Equals(MouseButtons.Right) && this.slicePermissionField.deletePermission
                    && this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    if (this.ExemptionGridView.CurrentRow != null && this.ExemptionGridView.CurrentRow.Index >= 0
                        && this.ExemptionGridView.CurrentRow.Selected)
                    {
                        this.selectedItemMenuStrip.Show(this.ExemptionGridView, new Point(e.X, e.Y));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion Grid Events

        #region PictureBox Events

        private void ExemptionPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.DiscretionaryToolTip.SetToolTip(this.ExemptionPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion PictureBox Events

        #endregion Events

        #region Methods

        #region Clear Controls

        /// <summary>
        /// Clears the controls.
        /// </summary>
        private void ClearControls()
        {
            this.RollYearTextBox.Text = string.Empty;
            this.AgricultureTextBox.Text = string.Empty;
            this.NonAgricultureTextBox.Text = string.Empty;
        }

        #endregion Clear Controls

        #region EnableControls

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="hasLocked">if set to <c>true</c> [has locked].</param>
        private void LockControls(bool hasLocked)
        {
            this.ExemptionPanel.Enabled = !hasLocked;
        }

        /// <summary>
        /// Enables the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableControls(bool enable)
        {
            //this.TrendGridPanel.Enabled = enable;
            //this.TrendGridPanel.Enabled = enable;
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
                this.ExemptionGridView.AllowSorting = false;
            }
        }

        #endregion Enable Edit

        #region Customize GridView

        private void CustomizeGridView()
        {
            this.ExemptionGridView.AllowUserToResizeColumns = false;
            this.ExemptionGridView.AllowUserToResizeRows = false;
            this.ExemptionGridView.AutoGenerateColumns = false;

            this.DetailID.DataPropertyName = discretionaryData.DiscretionaryDetails.DetailIDColumn.ColumnName;
            this.ExemptionYear.DataPropertyName = discretionaryData.DiscretionaryDetails.ExemptionYearColumn.ColumnName;
            this.State.DataPropertyName = discretionaryData.DiscretionaryDetails.StateColumn.ColumnName;
            this.Class.DataPropertyName = discretionaryData.DiscretionaryDetails.ClassColumn.ColumnName;
            this.ExemptionAmount.DataPropertyName = discretionaryData.DiscretionaryDetails.ExemptionAmountColumn.ColumnName;
            this.SubjectAmount.DataPropertyName = discretionaryData.DiscretionaryDetails.SubjectAmountColumn.ColumnName;
        }

        #endregion Customize GridView

        #region Read Only

        /// <summary>
        /// Sets the read only.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void SetReadOnly(int rowIndex)
        {
            if (rowIndex > 0)
            {
                if (string.IsNullOrEmpty(this.ExemptionGridView[this.ExemptionYear.Name, rowIndex - 1].Value.ToString().Trim())
                    && string.IsNullOrEmpty(this.ExemptionGridView[this.State.Name, rowIndex - 1].Value.ToString().Trim())
                    && string.IsNullOrEmpty(this.ExemptionGridView[this.Class.Name, rowIndex - 1].Value.ToString().Trim())
                    && string.IsNullOrEmpty(this.ExemptionGridView[this.SubjectAmount.Name, rowIndex - 1].Value.ToString().Trim())
                    && string.IsNullOrEmpty(this.ExemptionGridView[this.ExemptionAmount.Name, rowIndex - 1].Value.ToString().Trim()))
                {
                    if (rowIndex < this.ExemptionGridView.OriginalRowCount)
                    {
                        this.ExemptionGridView.Rows[rowIndex].ReadOnly = false;
                    }
                    else
                    {
                        this.ExemptionGridView.Rows[rowIndex].ReadOnly = true;
                    }
                }
                else
                {
                    this.ExemptionGridView.Rows[rowIndex].ReadOnly = false;
                }
            }
            else if (rowIndex.Equals(0))
            {
                this.ExemptionGridView.Rows[rowIndex].ReadOnly = false;
            }
        }

        #endregion Read Only

        #region Load Discretionary Details

        /// <summary>
        /// Gets the trend details.
        /// </summary>
        private void LoadDiscretionaryDetails()
        {
            //this.ExemptionGridView.Sort(this.ExemptionGridView.Columns[this.Year.Name], ListSortDirection.Ascending);
            // DB call for get trend details for specific keyId
            this.discretionaryData = this.form28000Control.WorkItem.F28000_GetDiscretionaryDetails(this.keyId);
            this.LoadStateCombo();
            this.ExemptionGridView.DataSource = this.discretionaryData.DiscretionaryDetails.DefaultView;

            this.LoadStateCombo();
            // Assign controls on appropriate controls
            this.SetControlValues();

            this.discretionaryData.AcceptChanges();

            if (this.ExemptionGridView.OriginalRowCount > 0)
            {
                if (this.ExemptionGridView.OriginalRowCount > 0)
                {
                    if (this.ExemptionGridView.OriginalRowCount >= this.ExemptionGridView.NumRowsVisible)
                    {
                        F28000DiscretionaryData.DiscretionaryDetailsRow newRow = this.discretionaryData.DiscretionaryDetails.NewDiscretionaryDetailsRow();
                        newRow["EmptyRecord$"] = "True";
                        this.discretionaryData.DiscretionaryDetails.AddDiscretionaryDetailsRow(newRow);
                    }
                }

                this.ExemptionGridView.Rows[0].Selected = true;
            }
            else
            {
                this.ExemptionGridView.Rows[0].Selected = false;
            }

            this.SetScrollBarVisibility();

            this.CalculateExemptionAmount();
            this.ExemptionGridView.AllowSorting = true;
        }

        private void LoadStateCombo()
        {
            (this.State as DataGridViewComboBoxColumn).DataSource = this.discretionaryData.StateList.Copy();
            (this.State as DataGridViewComboBoxColumn).DisplayMember = this.discretionaryData.StateList.StateDescriptionColumn.ColumnName;
            (this.State as DataGridViewComboBoxColumn).ValueMember = this.discretionaryData.StateList.StateColumn.ColumnName;
        }


        private void SetControlValues()
        {
            if (this.discretionaryData.Discretionary.Rows.Count > 0)
            {
                F28000DiscretionaryData.DiscretionaryRow discretionaryRow = (F28000DiscretionaryData.DiscretionaryRow)this.discretionaryData.Discretionary.Rows[0];

                if (!discretionaryRow.IsRollyearNull())
                {
                    this.RollYearTextBox.Text = discretionaryRow.Rollyear.ToString();
                }
                else
                {
                    this.RollYearTextBox.Text = string.Empty;
                }

                if (!discretionaryRow.IsPriorAgExemptionAmountNull())
                {
                    this.AgricultureTextBox.Text = discretionaryRow.PriorAgExemptionAmount.ToString();
                }
                else
                {
                    this.AgricultureTextBox.Text = "0.00";
                }

                if (!discretionaryRow.IsPriorNonAgExemptionAmountNull())
                {
                    this.NonAgricultureTextBox.Text = discretionaryRow.PriorNonAgExemptionAmount.ToString();
                }
                else
                {
                    this.NonAgricultureTextBox.Text = "0.00";
                }

                if (!discretionaryRow.IsDiscretionaryIDNull())
                {
                    int.TryParse(discretionaryRow.DiscretionaryID.ToString(), out this.discretionaryId);
                }
                else
                {
                    this.discretionaryId = 0;
                }
            }
            else
            {
                this.RollYearTextBox.Text = string.Empty;
                this.AgricultureTextBox.Text = "0.00";
                this.NonAgricultureTextBox.Text = "0.00";
                this.discretionaryId = 0;
            }
        }

        #endregion Load Discretionary Details

        #region ScrollBar Visibility

        /// <summary>
        /// Sets the scroll bar visibility.
        /// </summary>
        private void SetScrollBarVisibility()
        {
            if (this.discretionaryData.DiscretionaryDetails.Rows.Count <= this.ExemptionGridView.NumRowsVisible)
            {
                this.ExemptionVscrollBar.Visible = true;
            }
            else
            {
                this.ExemptionVscrollBar.Visible = false;
            }
        }

        #endregion ScrollBar Visibility

        #region Detail ID Table

        /// <summary>
        /// Selecteds the detail id collection.
        /// </summary>
        /// <returns>XMLString contains selected rows detail Ids</returns>
        private string SelectedDetailIdCollection()
        {
            DataTable detailTable = new DataTable();
            detailTable.Columns.Add(this.DetailID.Name, typeof(int));
            DataGridViewSelectedRowCollection selectedRows = this.ExemptionGridView.SelectedRows;

            foreach (DataGridViewRow dataRow in selectedRows)
            {
                if (dataRow.Cells[this.DetailID.Name].Value != null
                    && !string.IsNullOrEmpty(dataRow.Cells[this.DetailID.Name].Value.ToString()))
                {
                    DataRow newRow = detailTable.NewRow();
                    newRow[this.DetailID.Name] = dataRow.Cells[this.DetailID.Name].Value.ToString();
                    detailTable.Rows.Add(newRow);
                }
            }

            if (detailTable.Rows.Count > 0)
            {
                return TerraScanCommon.GetXmlString(detailTable);
            }

            return null;
        }

        #endregion Detail ID Table

        #region SetExpression

        /// <summary>
        /// Sets the expression.
        /// </summary>
        /// <returns>Expression String</returns>
        private string SetExpression()
        {
            // Replace EmptyRecord$ value as True for empty records
            string expressionString = "IIF (" + this.discretionaryData.DiscretionaryDetails.DetailIDColumn.ColumnName + " IS NULL and "
                                               + this.discretionaryData.DiscretionaryDetails.ExemptionYearColumn.ColumnName + " IS NULL and "
                                               + this.discretionaryData.DiscretionaryDetails.StateColumn.ColumnName + " IS NULL  and ("
                                               + this.discretionaryData.DiscretionaryDetails.ClassColumn.ColumnName + " IS NULL or "
                                               + this.discretionaryData.DiscretionaryDetails.ClassColumn.ColumnName + " = '') and "
                                               + this.discretionaryData.DiscretionaryDetails.SubjectAmountColumn.ColumnName + " Is NULL, True, False) ";
            return expressionString;
        }

        #endregion SetExpression

        #region Delete

        /// <summary>
        /// Deletes the trend items.
        /// </summary>
        private void DeleteDiscretionaryItems()
        {
            if (this.ExemptionGridView.CurrentCell != null && this.slicePermissionField.deletePermission
                && this.ExemptionGridView.OriginalRowCount > 0 && this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                if (MessageBox.Show("Do you want to delete these records?", "Delete Records", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // DB call for delete
                    this.form28000Control.WorkItem.F28000_DeletediscretionaryDetails(null, this.SelectedDetailIdCollection(), TerraScanCommon.UserId);

                    // Reload form
                    this.LoadDiscretionaryDetails();

                    this.CalculateExemptionAmount();

                    this.ExemptionGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
                //if (this.ExemptionGridView.OriginalRowCount <= 0)
                //{
                //    this.RollYearTextBox.Focus();
                //}
            }
        }

        #endregion Delete

        #region Save Trend

        /// <summary>
        /// Saves the trend details.
        /// </summary>
        private void SaveDiscretionaryDetails()
        {
            DataTable changeSets = null;
            // Get modified/(newly added) row collecion
            if (this.discretionaryData.DiscretionaryDetails.GetChanges() != null)
            {
                changeSets = this.discretionaryData.DiscretionaryDetails.GetChanges().Copy();
            }

            // XMLString for Trend details (from datagridview)
            string discretionaryItemDetails;
            if (changeSets != null)
            {
                try
                {
                    changeSets.Columns["EmptyRecord$"].Expression = this.SetExpression();
                }
                catch (Exception ex)
                {
                }
                changeSets.DefaultView.RowFilter = "EmptyRecord$ = False";
                DataTable changesToSave = changeSets.DefaultView.ToTable();
                discretionaryItemDetails = TerraScanCommon.GetXmlString(changesToSave);
            }
            else
            {
                discretionaryItemDetails = null;
            }

            // DB call for Save
            int returnValue;
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
            {
                returnValue = this.form28000Control.WorkItem.F28000_SaveDiscretionaryDetail(this.keyId,  null, discretionaryItemDetails, TerraScanCommon.UserId);
            }
            else
            {
                int discretionaryId = 0;
                if (this.discretionaryData.Discretionary.Rows.Count > 0 && this.discretionaryData.Discretionary.Rows[0][this.discretionaryData.Discretionary.DiscretionaryIDColumn.ColumnName] != null
                    && !string.IsNullOrEmpty(this.discretionaryData.Discretionary.Rows[0][this.discretionaryData.Discretionary.DiscretionaryIDColumn.ColumnName].ToString()))
                {
                    int.TryParse(this.discretionaryData.Discretionary.Rows[0][this.discretionaryData.Discretionary.DiscretionaryIDColumn.ColumnName].ToString(), out discretionaryId);
                }

                returnValue = this.form28000Control.WorkItem.F28000_SaveDiscretionaryDetail(this.keyId, discretionaryId, discretionaryItemDetails, TerraScanCommon.UserId);
            }

            //this.keyId = returnValue;

            // Reload form after save
            SliceReloadActiveRecord sliceReloadActiveRecord;
            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
            sliceReloadActiveRecord.SelectedKeyId = this.keyId;
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
        }

        #endregion Save Trend

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

        #region Exemption Sum

        private void CalculateExemptionAmount()
        {
            int exemptiontotal = 0;
            int.TryParse(this.discretionaryData.DiscretionaryDetails.Compute(string.Concat("SUM(", this.discretionaryData.DiscretionaryDetails.ExemptionAmountColumn.ColumnName, ")"), "").ToString(), out exemptiontotal);
            this.ExemptionTotalTextBox.Text = exemptiontotal.ToString("#,##0");
            this.ExemptionSumLabel.Text = this.ExemptionTotalTextBox.Text;
        }

        #endregion Exemption Sum
      
        #endregion Methods
    }
}
