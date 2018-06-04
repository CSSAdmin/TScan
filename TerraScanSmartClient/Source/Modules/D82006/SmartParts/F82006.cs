//--------------------------------------------------------------------------------------------
// <copyright file="F82006.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F82006.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 25 Nov 06        Sadha Shivudu M    Created
// 07 May 09        Khaja              Added UserId Parameter Bug No.4471
//*********************************************************************************/

namespace D82006
{
    #region namespace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using Infrastructure.Interface;
    using TerraScan.BusinessEntities;
    using System.Text.RegularExpressions;
    using System.Web.Services.Protocols;

    #endregion namespace

    /// <summary>
    /// F82006 smartpart
    /// </summary>
    [SmartPart]
    public partial class F82006 : BaseSmartPart
    {
        #region instance variables

        /// <summary>
        /// instance variable to hold the F82006Controller
        /// </summary>
        private F82006Controller form82006Control;

        /// <summary>
        /// instance variable to hold the page mode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// instance variable to hold the form keyId value
        /// </summary>
        private int keyId;

        /// <summary>
        /// instance variable to hold the pageLoadStatus
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// instance variable to hold the contractor management typed dataset
        /// </summary>
        private F82006ContractManagementData contractorManagementData = new F82006ContractManagementData();

        /// <summary>
        /// instacnce variable to hold the grid edit status
        /// </summary>
        private bool edit;

        /// <summary>
        /// instacnce variable to hold the contractor identity
        /// </summary>
        private int? contractorIdentity = null;

        /// <summary>
        /// instance variable to hold the gird current row index
        /// </summary>
        private int gridCurrentRowIndex;

        /// <summary>
        /// instance variable to hold the gird last edited row index
        /// </summary>
        private int lastEditedRowIndex;

        #endregion instance variables

        #region formslice instance variables

        /// <summary>
        /// instance variable to hold the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        /// <summary>
        ///  instance variable to hold the red color value
        /// </summary>
        private int redColor;

        /// <summary>
        ///  instance variable to hold the green color value
        /// </summary>
        private int greenColor;

        /// <summary>
        ///  instance variable to hold the blue color value
        /// </summary>
        private int blueColor;

        /// <summary>
        /// instance variable to hold the master form number
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// instance variable to hold the form master edit permission value
        /// </summary>
        private bool formMasterPermissionEdit;

        #endregion formslice instance variables

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F82006"/> class.
        /// </summary>
        public F82006()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F82006"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F82006(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;

            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;

            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.ContractorManagementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ContractorManagementPictureBox.Height, this.ContractorManagementPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
        }

        #endregion constructor

        #region event publication

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
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion event publication

        #region property

        /// <summary>
        /// Gets or sets the form82006 control.
        /// </summary>
        /// <value>The form82006 control.</value>
        [CreateNew]
        public F82006Controller Form82006Control
        {
            get
            {
                return this.form82006Control as F82006Controller;
            }

            set
            {
                this.form82006Control = value;
            }
        }

        /// <summary>
        /// Gets or sets the contractor identity.
        /// </summary>
        /// <value>The contractor identity.</value>
        public int? ContractorIdentity
        {
            get
            {
                return this.contractorIdentity;
            }

            set
            {
                this.contractorIdentity = value;
            }
        }

        #endregion property

        #region event subscription

        /// <summary>
        /// Called when [D9030_ F9030_ alert resizable slice].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_AlertResizableSlice, ThreadOption.UserInterface)]
        public void OnD9030_F9030_AlertResizableSlice(object sender, DataEventArgs<int> eventArgs)
        {
            if (eventArgs.Data == this.masterFormNo)
            {
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                this.Height = this.ContractorManagementPictureBox.Height;
                sliceResize.SliceFormHeight = this.ContractorManagementPictureBox.Height;
                this.ContractorManagementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ContractorManagementPictureBox.Height, this.ContractorManagementPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
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
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                if (this.contractorManagementData.GetContractorList.Rows.Count > 0)
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

            this.ContractorNameTextBox.Focus();
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
                if (!this.keyId.Equals(eventArgs.Data.SelectedKeyId))
                {
                    this.lastEditedRowIndex = 0;
                }

                this.keyId = eventArgs.Data.SelectedKeyId;
                this.ContractorIdentity = this.keyId;
                this.CustomizeEmployeeListingGridView();
                this.PopulateContractorEmployeeDetails();
                this.ContractorNameTextBox.Focus();
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
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View) && this.permissionFields.newPermission)
            {
                this.Cursor = Cursors.WaitCursor;
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.SetGridSortMode(false);
                this.ClearContractorManagementHeader();
                this.ContractorIdentity = null;
                this.lastEditedRowIndex = 0;
                this.EnableControls(false);
                this.ControlLock(false);
                this.ContractorNameTextBox.Focus();
                this.Cursor = Cursors.Default;
            }
            else
            {
                this.EnableControls(true);
                this.ControlLock(true);
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
            if (this.PermissionFiled.deletePermission && this.keyId > 0)
            {
                this.form82006Control.WorkItem.F82006_DeleteContractorList(this.keyId, TerraScanCommon.UserId);
            }
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
            {
                if (this.permissionFields.newPermission || this.permissionFields.editPermission)
                {
                    this.ValidateSliceForm(eventArgs);
                }
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
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
            {
                if (this.permissionFields.editPermission || this.permissionFields.newPermission)
                {
                    this.SaveContractorEmployeeDitails();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.SetGridSortMode(true);
                }
            }
            else
            {
                this.ControlLock(true);
                this.EnableControls(true);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SetGridSortMode(true);
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
            this.Cursor = Cursors.WaitCursor;
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.lastEditedRowIndex = 0;
            this.SetGridSortMode(true);
            this.ContractorIdentity = this.keyId;
            this.PopulateContractorEmployeeDetails();
            this.ContractorNameTextBox.Focus();
            this.Cursor = Cursors.Default;
        }

        #endregion event subscription

        #region protected methods

        /// <summary>
        /// Called when [form slice_ resize].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_Resize(DataEventArgs<SliceResize> eventArgs)
        {
            if (this.FormSlice_Resize != null)
            {
                this.FormSlice_Resize(this, eventArgs);
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

        #endregion protected methods

        #region event handler methods

        /// <summary>
        /// Handles the Load event of the F82006 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F82006_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.CustomizeEmployeeListingGridView();
                this.ContractorIdentity = this.keyId;
                this.PopulateContractorEmployeeDetails();
                this.ContractorNameTextBox.Focus();
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ContractorManagementPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ContractorManagementPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the ContractorManagementPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ContractorManagementPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.ContractorManagementToolTip.SetToolTip(this.ContractorManagementPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #region gridview events

        /// <summary>
        /// Handles the RowEnter event of the EmployeeListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void EmployeeListingGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool hasValues = false;
                int currentRowIndex = e.RowIndex;
                this.gridCurrentRowIndex = currentRowIndex;
                if (currentRowIndex >= 1)
                {
                    if ((string.IsNullOrEmpty(this.EmployeeListingGridView.Rows[(currentRowIndex - 1)].Cells[this.contractorManagementData.ListContractorEmployee.EmployeeIDColumn.ColumnName].Value.ToString().Trim())))
                    {
                        if (currentRowIndex + 1 < this.EmployeeListingGridView.RowCount)
                        {
                            for (int i = currentRowIndex; i < this.EmployeeListingGridView.RowCount; i++)
                            {
                                if (this.EmployeeListingGridView.Rows[i].Cells[this.contractorManagementData.ListContractorEmployee.EmployeeIDColumn.ColumnName].Value != null && !String.IsNullOrEmpty(this.EmployeeListingGridView.Rows[i].Cells[this.contractorManagementData.ListContractorEmployee.EmployeeIDColumn.ColumnName].Value.ToString().Trim()))
                                {
                                    hasValues = true;
                                    break;
                                }
                            }

                            if (hasValues)
                            {
                                this.MakeReadOnlyColumns(currentRowIndex, false);
                            }
                            else
                            {
                                this.MakeReadOnlyColumns(currentRowIndex, true);
                            }
                        }
                        else
                        {
                            this.MakeReadOnlyColumns(currentRowIndex, true);
                        }
                    }
                    else
                    {
                        this.MakeReadOnlyColumns(currentRowIndex, false);
                    }

                    this.EmployeeListingGridView.Rows[currentRowIndex].Selected = true;
                }
                //// Need to Get Confirmation
                if (currentRowIndex == 0)
                {
                    this.MakeReadOnlyColumns(currentRowIndex, false);
                    this.EmployeeListingGridView.Rows[currentRowIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellBeginEdit event of the EmployeeListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void EmployeeListingGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                this.edit = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataError event of the EmployeeListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void EmployeeListingGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the EmployeeListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void EmployeeListingGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
            e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
            e.Control.Validated += new EventHandler(this.Control_Validated);
        }

        /// <summary>
        /// Handles the CellValueChanged event of the EmployeeListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void EmployeeListingGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.edit)
            {
                try
                {
                    if (e.ColumnIndex == this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.LastNameColumn.ColumnName].Index ||
                        e.ColumnIndex == this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.FirstNameColumn.ColumnName].Index ||
                        e.ColumnIndex == this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.PhoneColumn.ColumnName].Index ||
                        e.ColumnIndex == this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.ExtColumn.ColumnName].Index ||
                        e.ColumnIndex == this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.CellularColumn.ColumnName].Index ||
                        e.ColumnIndex == this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.EmailColumn.ColumnName].Index ||
                        e.ColumnIndex == this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.CommentsColumn.ColumnName].Index)
                    {
                        int currentEmpId;
                        this.contractorManagementData.ListContractorEmployee.DefaultView.Sort = string.Empty;
                        int.TryParse(this.EmployeeListingGridView[this.contractorManagementData.ListContractorEmployee.EmployeeIDColumn.ColumnName, e.RowIndex].Value.ToString(), out currentEmpId);
                        if (currentEmpId.Equals(0))
                        {
                            this.contractorManagementData.ListContractorEmployee.Rows[e.RowIndex][this.contractorManagementData.ListContractorEmployee.EmployeeIDColumn.ColumnName] = 0;
                        }

                        this.edit = false;
                        this.EmployeeListingGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        this.lastEditedRowIndex = this.gridCurrentRowIndex;

                        if (this.EmployeeListingGridView.OriginalRowCount >= this.EmployeeListingGridView.NumRowsVisible)
                        {
                            if (!Convert.ToBoolean(this.contractorManagementData.ListContractorEmployee.Rows[this.contractorManagementData.ListContractorEmployee.Rows.Count - 1][this.EmployeeListingGridView.EmptyRecordColumnName]))
                            {
                                this.contractorManagementData.ListContractorEmployee.Rows.Add(this.contractorManagementData.ListContractorEmployee.NewRow());
                                this.contractorManagementData.ListContractorEmployee.Rows[this.contractorManagementData.ListContractorEmployee.Rows.Count - 1][this.EmployeeListingGridView.EmptyRecordColumnName] = true;
                                this.EmployeeListingGridViewVScrollBar.Visible = false;
                            }
                        }
                    }
                }
                catch (DataException ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                }
                catch (Exception ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                }
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the EmployeeListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void EmployeeListingGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && !Convert.ToBoolean(this.contractorManagementData.ListContractorEmployee.Rows[e.RowIndex][this.EmployeeListingGridView.EmptyRecordColumnName]))
                {
                    int currentEmpId;
                    int.TryParse(this.EmployeeListingGridView[this.contractorManagementData.ListContractorEmployee.EmployeeIDColumn.ColumnName, e.RowIndex].Value.ToString(), out currentEmpId);
                    if (currentEmpId > 0)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        FormInfo formInfo = TerraScanCommon.GetFormInfo(82107);
                        formInfo.optionalParameters = new object[] { currentEmpId };
                        this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                        this.Cursor = Cursors.Default;
                    }
                }
            }
            catch (DataException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellParsing event of the EmployeeListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void EmployeeListingGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.LastNameColumn.ColumnName].Index ||
                           e.ColumnIndex == this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.FirstNameColumn.ColumnName].Index ||
                           e.ColumnIndex == this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.PhoneColumn.ColumnName].Index ||
                           e.ColumnIndex == this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.CellularColumn.ColumnName].Index ||
                           e.ColumnIndex == this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.EmailColumn.ColumnName].Index ||
                           e.ColumnIndex == this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.CommentsColumn.ColumnName].Index)
                    {
                        if (e.Value != null)
                        {
                            string cellValue = e.Value.ToString().Trim();
                            if (!string.IsNullOrEmpty(cellValue))
                            {
                                e.Value = cellValue;
                                this.edit = true;
                            }
                            else
                            {
                                e.Value = string.Empty;
                                this.edit = false;
                            }

                            e.ParsingApplied = true;
                            this.EmployeeListingGridView.RefreshEdit();
                        }
                    }

                    Int16 outInteger;
                    if (e.ColumnIndex == this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.ExtColumn.ColumnName].Index)
                    {
                        if (e.Value != null)
                        {
                            string tempvalue = e.Value.ToString().Trim();
                            Int16.TryParse(tempvalue, System.Globalization.NumberStyles.Integer, null, out outInteger);
                            e.Value = outInteger;
                            e.ParsingApplied = true;
                            this.EmployeeListingGridView.RefreshEdit();
                        }
                    }
                }
            }
            catch (DataException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the EmployeeListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void EmployeeListingGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 &&
                    e.ColumnIndex == this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.LastNameColumn.ColumnName].Index ||
                    e.ColumnIndex == this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.FirstNameColumn.ColumnName].Index ||
                    e.ColumnIndex == this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.PhoneColumn.ColumnName].Index ||
                    e.ColumnIndex == this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.ExtColumn.ColumnName].Index ||
                    e.ColumnIndex == this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.CellularColumn.ColumnName].Index ||
                    e.ColumnIndex == this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.EmailColumn.ColumnName].Index ||
                    e.ColumnIndex == this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.CommentsColumn.ColumnName].Index)
                {
                    int currentEmpId;
                    int.TryParse(this.EmployeeListingGridView[this.contractorManagementData.ListContractorEmployee.EmployeeIDColumn.ColumnName, e.RowIndex].Value.ToString(), out currentEmpId);
                    if (currentEmpId > 0)
                    {
                        (EmployeeListingGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewTextBoxCell).ToolTipText = "EmployeeID: " + currentEmpId.ToString();
                    }
                    else
                    {
                        (EmployeeListingGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewTextBoxCell).ToolTipText = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Enter event of the EmployeeListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EmployeeListingGridView_Enter(object sender, EventArgs e)
        {
            ////try
            ////{
            ////    this.EmployeeListingGridView.CurrentCell = this.EmployeeListingGridView[this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.LastNameColumn.ColumnName].Index, 0];
            ////    this.EmployeeListingGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            ////    this.EmployeeListingGridView.Rows[0].Selected = true;
            ////}
            ////catch (Exception ex)
            ////{
            ////    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            ////}
        }

        /// <summary>
        /// Handles the KeyDown event of the EmployeeListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void EmployeeListingGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.gridCurrentRowIndex >= 0 && !Convert.ToBoolean(this.contractorManagementData.ListContractorEmployee.Rows[this.gridCurrentRowIndex][this.EmployeeListingGridView.EmptyRecordColumnName]))
                {
                    if (e.KeyCode == Keys.Delete && this.EmployeeListingGridView.CurrentCell != null && this.EmployeeListingGridView.Rows[this.EmployeeListingGridView.CurrentCell.RowIndex].Selected)
                    {
                        int empId;
                        this.lastEditedRowIndex = 0;
                        int rowIndex = this.EmployeeListingGridView.CurrentCell.RowIndex;
                        int.TryParse(this.contractorManagementData.ListContractorEmployee.Rows[rowIndex][this.contractorManagementData.ListContractorEmployee.EmployeeIDColumn.ColumnName].ToString(), out empId);
                        if (empId > 0 && this.keyId > 0)
                        {
                            this.form82006Control.WorkItem.F82006_DeleteContractorEmployee(this.keyId, empId, TerraScanCommon.UserId);
                        }

                        this.SetEditRecord();
                        this.contractorManagementData.ListContractorEmployee.Rows.RemoveAt(rowIndex);
                        this.contractorManagementData.ListContractorEmployee.AcceptChanges();
                        this.PopulateEmployeeDetailsGridView();
                    }
                }
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
                this.EmployeeListingGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
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
                this.SetEditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion gridview events

        #endregion event handler methods

        #region private methods

        /// <summary>
        /// Makes the read only columns.
        /// </summary>
        /// <param name="currentRowIndex">Index of the current row.</param>
        /// <param name="flagReadOnly">if set to <c>true</c> [flag read only].</param>
        private void MakeReadOnlyColumns(int currentRowIndex, bool flagReadOnly)
        {
            this.EmployeeListingGridView[this.contractorManagementData.ListContractorEmployee.EmployeeIDColumn.ColumnName, currentRowIndex].ReadOnly = flagReadOnly;
            this.EmployeeListingGridView[this.contractorManagementData.ListContractorEmployee.LastNameColumn.ColumnName, currentRowIndex].ReadOnly = flagReadOnly;
            this.EmployeeListingGridView[this.contractorManagementData.ListContractorEmployee.FirstNameColumn.ColumnName, currentRowIndex].ReadOnly = flagReadOnly;
            this.EmployeeListingGridView[this.contractorManagementData.ListContractorEmployee.PhoneColumn.ColumnName, currentRowIndex].ReadOnly = flagReadOnly;
            this.EmployeeListingGridView[this.contractorManagementData.ListContractorEmployee.ExtColumn.ColumnName, currentRowIndex].ReadOnly = flagReadOnly;
            this.EmployeeListingGridView[this.contractorManagementData.ListContractorEmployee.CellularColumn.ColumnName, currentRowIndex].ReadOnly = flagReadOnly;
            this.EmployeeListingGridView[this.contractorManagementData.ListContractorEmployee.EmailColumn.ColumnName, currentRowIndex].ReadOnly = flagReadOnly;
            this.EmployeeListingGridView[this.contractorManagementData.ListContractorEmployee.CommentsColumn.ColumnName, currentRowIndex].ReadOnly = flagReadOnly;
            this.EmployeeListingGridView[this.contractorManagementData.ListContractorEmployee.PrimaryKeyIDColumn.ColumnName, currentRowIndex].ReadOnly = flagReadOnly;
        }

        /// <summary>
        /// Clears the value slice header.
        /// </summary>
        private void ClearContractorManagementHeader()
        {
            this.ContractorNameTextBox.Text = string.Empty;
            this.PhoneNumberTextBox.Text = string.Empty;
            this.AddressTextBox.Text = string.Empty;
            this.CityTextBox.Text = string.Empty;
            this.StateTextBox.Text = string.Empty;
            this.ZipTextBox.Text = string.Empty;
            this.contractorManagementData.ListContractorEmployee.Clear();
            this.EmployeeListingGridView.DataSource = this.contractorManagementData.ListContractorEmployee.DefaultView;
        }

        /// <summary>
        /// Customizes the employee listing grid view.
        /// </summary>
        private void CustomizeEmployeeListingGridView()
        {
            this.EmployeeListingGridView.PrimaryKeyColumnName = this.contractorManagementData.ListContractorEmployee.PrimaryKeyIDColumn.ColumnName.ToString();
            this.EmployeeListingGridView.AutoGenerateColumns = false;

            this.LastName.DataPropertyName = this.contractorManagementData.ListContractorEmployee.LastNameColumn.ColumnName;
            this.FirstName.DataPropertyName = this.contractorManagementData.ListContractorEmployee.FirstNameColumn.ColumnName;
            this.Phone.DataPropertyName = this.contractorManagementData.ListContractorEmployee.PhoneColumn.ColumnName;
            this.Ext.DataPropertyName = this.contractorManagementData.ListContractorEmployee.ExtColumn.ColumnName;
            this.Cellular.DataPropertyName = this.contractorManagementData.ListContractorEmployee.CellularColumn.ColumnName;
            this.Email.DataPropertyName = this.contractorManagementData.ListContractorEmployee.EmailColumn.ColumnName;
            this.Comments.DataPropertyName = this.contractorManagementData.ListContractorEmployee.CommentsColumn.ColumnName;
            this.EmployeeID.DataPropertyName = this.contractorManagementData.ListContractorEmployee.EmployeeIDColumn.ColumnName;
            this.PrimaryKeyID.DataPropertyName = this.contractorManagementData.ListContractorEmployee.PrimaryKeyIDColumn.ColumnName;

            this.EmployeeListingGridView.DataSource = this.contractorManagementData.ListContractorEmployee.DefaultView;
        }

        /// <summary>
        /// Populates the contractor employee details.
        /// </summary>
        private void PopulateContractorEmployeeDetails()
        {
            this.pageLoadStatus = true;
            this.ClearContractorManagementHeader();
            this.contractorManagementData = this.Form82006Control.WorkItem.F82006_GetContractorList(this.keyId);

            if (this.contractorManagementData.GetContractorList.Rows.Count > 0)
            {
                this.ControlLock(false || !this.permissionFields.editPermission || !this.formMasterPermissionEdit);
                this.EnableControls(false || !this.permissionFields.editPermission || !this.formMasterPermissionEdit);
                this.ContractorNameTextBox.Text = this.contractorManagementData.GetContractorList.Rows[0][this.contractorManagementData.GetContractorList.ContractorNameColumn.ColumnName].ToString();
                this.PhoneNumberTextBox.Text = this.contractorManagementData.GetContractorList.Rows[0][this.contractorManagementData.GetContractorList.ContractorPhoneColumn.ColumnName].ToString();
                this.AddressTextBox.Text = this.contractorManagementData.GetContractorList.Rows[0][this.contractorManagementData.GetContractorList.ContractorAddressColumn.ColumnName].ToString();
                this.CityTextBox.Text = this.contractorManagementData.GetContractorList.Rows[0][this.contractorManagementData.GetContractorList.ContractorCityColumn.ColumnName].ToString();
                this.StateTextBox.Text = this.contractorManagementData.GetContractorList.Rows[0][this.contractorManagementData.GetContractorList.ContractorStateColumn.ColumnName].ToString();
                this.ZipTextBox.Text = this.contractorManagementData.GetContractorList.Rows[0][this.contractorManagementData.GetContractorList.ContractorZipColumn.ColumnName].ToString();
            }
            else
            {
                this.EnableControls(true);
                this.ControlLock(true);
            }

            this.PopulateEmployeeDetailsGridView();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.SetGridSortMode(true);
            this.pageLoadStatus = false;
        }

        /// <summary>
        /// Populates the employee details grid view.
        /// </summary>
        private void PopulateEmployeeDetailsGridView()
        {
            this.EmployeeListingGridView.DataSource = this.contractorManagementData.ListContractorEmployee;
            this.contractorManagementData.ListContractorEmployee.DefaultView.Sort = string.Empty;
            if (this.EmployeeListingGridView.OriginalRowCount > 0)
            {
                this.EmployeeListingGridView.RemoveDefaultSelection = false;
                if (this.lastEditedRowIndex > 0)
                {
                    this.EmployeeListingGridView.CurrentCell = this.EmployeeListingGridView[this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.LastNameColumn.ColumnName].Index, this.lastEditedRowIndex];
                    this.EmployeeListingGridView.Rows[this.EmployeeListingGridView.CurrentRowIndex].Selected = false;
                    this.EmployeeListingGridView.Rows[this.lastEditedRowIndex].Selected = true;
                }
                else
                {
                    this.EmployeeListingGridView.CurrentCell = this.EmployeeListingGridView[this.EmployeeListingGridView.Columns[this.contractorManagementData.ListContractorEmployee.LastNameColumn.ColumnName].Index, 0];
                    this.EmployeeListingGridView.Rows[0].Selected = true;
                }
            }
            else
            {
                this.EmployeeListingGridView.CurrentCell = null;
                this.EmployeeListingGridView.Rows[0].Selected = false;
                this.EmployeeListingGridView.RemoveDefaultSelection = true;
            }

            if (this.EmployeeListingGridView.OriginalRowCount >= this.EmployeeListingGridView.NumRowsVisible)
            {
                if (!Convert.ToBoolean(this.contractorManagementData.ListContractorEmployee.Rows[this.contractorManagementData.ListContractorEmployee.Rows.Count - 1][this.EmployeeListingGridView.EmptyRecordColumnName]))
                {
                    this.contractorManagementData.ListContractorEmployee.Rows.Add(this.contractorManagementData.ListContractorEmployee.NewRow());
                    this.contractorManagementData.ListContractorEmployee.Rows[this.contractorManagementData.ListContractorEmployee.Rows.Count - 1][this.EmployeeListingGridView.EmptyRecordColumnName] = true;
                }

                this.EmployeeListingGridViewVScrollBar.Visible = false;
            }
            else
            {
                this.EmployeeListingGridViewVScrollBar.Visible = true;
            }
        }

        /// <summary>
        /// Enables the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableControls(bool enable)
        {
            this.ContractorManagementHeaderPanel.Enabled = !enable;
            this.EmployeeListingGridpanel.Enabled = !enable;
        }

        /// <summary>
        /// Controls the lock.
        /// </summary>
        /// <param name="controlLock">if set to <c>true</c> [control lock].</param>
        private void ControlLock(bool controlLock)
        {
            this.ContractorNameTextBox.LockKeyPress = controlLock;
            this.PhoneNumberTextBox.LockKeyPress = controlLock;
            this.AddressTextBox.LockKeyPress = controlLock;
            this.CityTextBox.LockKeyPress = controlLock;
            this.StateTextBox.LockKeyPress = controlLock;
            this.ZipTextBox.LockKeyPress = controlLock;
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.permissionFields.editPermission && this.formMasterPermissionEdit)
            {
                if (!this.pageLoadStatus)
                {
                    if (!string.Equals(this.pageMode, TerraScanCommon.PageModeTypes.Edit))
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.SetGridSortMode(false);
                        this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                    }
                }
            }
        }

        /// <summary>
        /// Sets the grid sort mode.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void SetGridSortMode(bool enable)
        {
            this.EmployeeListingGridView.AllowSorting = enable;
        }

        /// <summary>
        /// Validates the slice form.
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        private void ValidateSliceForm(DataEventArgs<int> eventArgs)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = eventArgs.Data;
            this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            if (string.IsNullOrEmpty(this.ContractorNameTextBox.Text.Trim()))
            {
                this.ContractorNameTextBox.Focus();
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F82006RequiredFieldMissing");
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Saves the contractor employee ditails.
        /// </summary>
        private void SaveContractorEmployeeDitails()
        {
            int returnValue = -1;
            this.ConstructContractorRow();
            string contractorXml = this.GetContractorXmlString();
            string contractorEmployeeXml = this.GetContractorEmployeeXmlString();
            returnValue = this.form82006Control.WorkItem.F82006_SaveContractorList(this.ContractorIdentity, contractorXml, contractorEmployeeXml, TerraScanCommon.UserId);
            if (returnValue != -1)
            {
                this.ContractorIdentity = returnValue;
            }

            SliceReloadActiveRecord currentSliceInfo;
            currentSliceInfo.MasterFormNo = this.masterFormNo;
            currentSliceInfo.SelectedKeyId = returnValue;
            ////to refresh the master form with the return keyid
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
        }

        /// <summary>
        /// Constructs the contractor row.
        /// </summary>
        private void ConstructContractorRow()
        {
            if (this.contractorManagementData.GetContractorList.Rows.Count > 0)
            {
                this.contractorManagementData.GetContractorList.Rows[0][this.contractorManagementData.GetContractorList.ContractorNameColumn.ColumnName] = this.ContractorNameTextBox.Text.Trim();
                this.contractorManagementData.GetContractorList.Rows[0][this.contractorManagementData.GetContractorList.ContractorPhoneColumn.ColumnName] = this.PhoneNumberTextBox.Text.Trim();
                this.contractorManagementData.GetContractorList.Rows[0][this.contractorManagementData.GetContractorList.ContractorAddressColumn.ColumnName] = this.AddressTextBox.Text.Trim();
                this.contractorManagementData.GetContractorList.Rows[0][this.contractorManagementData.GetContractorList.ContractorCityColumn.ColumnName] = this.CityTextBox.Text.Trim();
                this.contractorManagementData.GetContractorList.Rows[0][this.contractorManagementData.GetContractorList.ContractorStateColumn.ColumnName] = this.StateTextBox.Text.Trim();
                this.contractorManagementData.GetContractorList.Rows[0][this.contractorManagementData.GetContractorList.ContractorZipColumn.ColumnName] = this.ZipTextBox.Text.Trim();
            }
            else
            {
                DataRow newContractorRow = this.contractorManagementData.GetContractorList.NewRow();
                newContractorRow[this.contractorManagementData.GetContractorList.ContractorNameColumn.ColumnName] = this.ContractorNameTextBox.Text.Trim();
                newContractorRow[this.contractorManagementData.GetContractorList.ContractorPhoneColumn.ColumnName] = this.PhoneNumberTextBox.Text.Trim();
                newContractorRow[this.contractorManagementData.GetContractorList.ContractorAddressColumn.ColumnName] = this.AddressTextBox.Text.Trim();
                newContractorRow[this.contractorManagementData.GetContractorList.ContractorCityColumn.ColumnName] = this.CityTextBox.Text.Trim();
                newContractorRow[this.contractorManagementData.GetContractorList.ContractorStateColumn.ColumnName] = this.StateTextBox.Text.Trim();
                newContractorRow[this.contractorManagementData.GetContractorList.ContractorZipColumn.ColumnName] = this.ZipTextBox.Text.Trim();
                this.contractorManagementData.GetContractorList.Rows.Add(newContractorRow);
                this.contractorManagementData.GetContractorList.AcceptChanges();
            }
        }

        /// <summary>
        /// Gets the contractor XML string.
        /// </summary>
        /// <returns>returns contractor xml string</returns>
        private string GetContractorXmlString()
        {
            string contractorXml = string.Empty;
            DataSet contractorDataSet = new DataSet("Root");
            contractorDataSet.Merge(this.contractorManagementData.GetContractorList);
            contractorDataSet.Tables[this.contractorManagementData.GetContractorList.TableName].TableName = "Table";
            contractorDataSet.Tables["Table"].Columns.Remove(this.contractorManagementData.GetContractorList.ContractorIDColumn.ColumnName);
            contractorXml = contractorDataSet.GetXml();
            return contractorXml;
        }

        /// <summary>
        /// Gets the contractor employee XML string.
        /// </summary>
        /// <returns>returns contractor employee xml string</returns>
        private string GetContractorEmployeeXmlString()
        {
            string contractorEmployeeXml = string.Empty;
            DataSet contractorEmployeeDataSet = new DataSet("Root");
            // Coding added for the issue 4338 on 29/5/2009 by Malliga
            this.contractorManagementData.ListContractorEmployee.AcceptChanges();
            // Ends here for 4338
            contractorEmployeeDataSet.Merge(this.contractorManagementData.ListContractorEmployee);
            contractorEmployeeDataSet.Tables[this.contractorManagementData.ListContractorEmployee.TableName].TableName = "Table";
            contractorEmployeeDataSet.Tables["Table"].Columns.Remove(this.contractorManagementData.ListContractorEmployee.PrimaryKeyIDColumn.ColumnName);
            contractorEmployeeXml = contractorEmployeeDataSet.GetXml();
            return contractorEmployeeXml;
        }

        #endregion private methods
    }
}
