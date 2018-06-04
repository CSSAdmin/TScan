//--------------------------------------------------------------------------------------------
// <copyright file="F15003.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F15003 Form Slice - FundMgmt 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 22-12-2006       Shiva              Created
//*********************************************************************************/
namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Infrastructure.Interface.Constants;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.UI.Controls;
    using System.Web.Services.Protocols;
    using TerraScan.Utilities;

    /// <summary>
    /// F15003 SmartPart
    /// </summary>
    [SmartPart]
    public partial class F15003 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// edit variable is used to store the fundandsubfundgridview edit status.
        /// </summary>
        private bool edit = false;

        /// <summary>
        /// F15003Controller variable. 
        /// </summary>
        private F15003Controller form15003Controll;

        /// <summary>
        /// pageMode variable used to set the mode of page new/edit/view.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// formMasterPermissionEdit variable used to store form master editpermission value.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// masterFormNo variable used to store the masterForm Number.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// keyId variable used to srored the keyId value.
        /// </summary>
        private int keyId;

        /// <summary>
        /// slicePermissionField variable used to store the slice permissionFields.
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// datatable contains the formDetails.
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// dataset contains fund management details.
        /// </summary>
        private F15003FundMgmtData fundMgmtDataSet = new F15003FundMgmtData();

        /// <summary>
        /// fundId variable is used to store the fundId value default value is Null.
        /// </summary>
        private int? fundId = null;

        /// <summary>
        /// pageLoadStatus variable is used to store the flag value for PageLoad.
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// flagInvalidSubFunds variable is used to store the InvalidSubFunds Flag.
        /// </summary>
        private bool flagInvalidSubFunds;

        #endregion

        #region Contructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15003"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F15003(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15003"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        /// <param name="featureClassID">The feature class ID.</param>
        public F15003(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassID)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
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

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form15003 controll.
        /// </summary>
        /// <value>The form15003 controll.</value>
        [CreateNew]
        public F15003Controller Form15003Controll
        {
            get { return this.form15003Controll as F15003Controller; }
            set { this.form15003Controll = value; }
        }

        /// <summary>
        /// Gets or sets the fund identity.
        /// </summary>
        /// <value>The fund identity.</value>
        public int? FundIdentity
        {
            get
            {
                return this.fundId;
            }

            set
            {
                this.fundId = value;
            }
        }

        #endregion

        #region Event SubScription

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

                    if (this.fundMgmtDataSet.FundHeader.Rows.Count > 0)
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

                if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                {
                    if (this.fundMgmtDataSet.FundHeader.Rows.Count > 0)
                    {
                        this.LockControls(false);
                    }
                    else
                    {
                        this.LockControls(true);
                    }
                }
                else
                {
                    this.LockControls(true);
                }
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
        /// Called when [D9030_ F9030_ save slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit || this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                if (this.slicePermissionField.newPermission)
                {
                    this.ValidateSliceForm(eventArgs);
                }
                else if (this.slicePermissionField.editPermission)
                {
                    this.ValidateSliceForm(eventArgs);
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
        /// Called when [D9030_ F9030_ save confirmed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit || this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                if (this.slicePermissionField.editPermission || this.slicePermissionField.newPermission)
                {
                    this.SaveFundDitails();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.SetGridSortMode(true);
                }
            }
            else
            {
                this.LockControls(true);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SetGridSortMode(true);
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
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    if (this.slicePermissionField.newPermission)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        this.pageLoadStatus = true;
                        this.pageMode = TerraScanCommon.PageModeTypes.New;
                        this.SetGridSortMode(false);
                        this.ClearFundHeader();
                        this.GetYear();
                        this.fundMgmtDataSet.ListFundAndSubFundItems.Rows.Clear();
                        this.PopulateFundAndSubFundsGridView();
                        if (Convert.ToInt32(this.RollYearTextBox.NumericTextBoxValue) > 0)
                        {
                            this.FillAvailableSubFundsGridView(null, Convert.ToInt32(this.RollYearTextBox.NumericTextBoxValue));
                        }
                        else
                        {
                            this.fundMgmtDataSet.ListAvailableSubFundItems.Rows.Clear();
                            this.PopulateAvailableSubFundsGridView();
                        }

                        this.LockControls(false);
                        this.pageLoadStatus = false;
                        this.FundTextBox.Focus();
                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        this.LockControls(true);
                    }
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            this.Cursor = Cursors.WaitCursor;

            if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
            {
                this.LockControls(false);
            }
            else
            {
                this.LockControls(true);
            }

            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.SetGridSortMode(true);
            this.FundIdentity = this.keyId;
            this.FillFundSubFundDetails(this.keyId);
            this.Cursor = Cursors.Default;
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
                    if (!this.flagInvalidSubFunds)
                    {
                        this.keyId = eventArgs.Data.SelectedKeyId;
                        this.FundIdentity = this.keyId;
                        this.FillFundSubFundDetails(Convert.ToInt32(this.FundIdentity));
                    }
                    else
                    {
                        this.SetEditRecord();
                        this.flagInvalidSubFunds = false;
                    }

                    if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                    {
                        this.LockControls(false);
                    }
                    else
                    {
                        this.LockControls(true);
                    }
                }
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

        #endregion

        #region Protected Methods

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

        #region Form Load Events/Methods

        /// <summary>
        /// Handles the Load event of the F15003 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F15003_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.CustomizeFundAndSubFundsGridView();
                this.CustomizeAvailableSubFundsGridView();
                this.FundandSubFundsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.FundandSubFundsPictureBox.Height, this.FundandSubFundsPictureBox.Width, "Fund and SubFunds", 28, 81, 128);
                this.AvailableSubFundsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AvailableSubFundsPictureBox.Height, this.AvailableSubFundsPictureBox.Width, "Available SubFunds", 0, 51, 0);
                this.FundIdentity = this.keyId;
                this.InitTypeComboBox();
                this.FillFundSubFundDetails(Convert.ToInt32(this.FundIdentity));
                this.FundAndSubFundsGridView.Enter += new EventHandler(this.FundAndSubFundsGridView_Enter);
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
        /// Fills the fund sub fund details.
        /// </summary>
        /// <param name="fundIdentity">The fund identity.</param>
        private void FillFundSubFundDetails(int fundIdentity)
        {
            this.pageLoadStatus = true;
            this.fundMgmtDataSet = this.Form15003Controll.WorkItem.F15003_GetFundSubFundDetails(fundIdentity);
            if (this.fundMgmtDataSet.FundHeader.Rows.Count > 0)
            {
                this.FundTextBox.Text = this.fundMgmtDataSet.FundHeader.Rows[0][this.fundMgmtDataSet.FundHeader.FundColumn.ColumnName].ToString();
                this.RollYearTextBox.Text = this.fundMgmtDataSet.FundHeader.Rows[0][this.fundMgmtDataSet.FundHeader.RollYearColumn.ColumnName].ToString();
                this.DescriptionTextBox.Text = this.fundMgmtDataSet.FundHeader.Rows[0][this.fundMgmtDataSet.FundHeader.DescriptionColumn.ColumnName].ToString();
                this.FundTypeComboBox.SelectedValue = Convert.ToInt32(this.fundMgmtDataSet.FundHeader.Rows[0][this.fundMgmtDataSet.FundHeader.FundGroupIDColumn.ColumnName]);
            }
            else
            {
                this.ClearFundHeader();
                this.fundMgmtDataSet.ListFundAndSubFundItems.Clear();
                this.fundMgmtDataSet.ListAvailableSubFundItems.Clear();
                this.LockControls(true);
            }

            this.PopulateFundAndSubFundsGridView();
            this.PopulateAvailableSubFundsGridView();
            this.FundTextBox.Focus();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.SetGridSortMode(true);
            this.pageLoadStatus = false;
        }

        /// <summary>
        /// Populates the fundandsubfunds gridview.
        /// </summary>
        private void PopulateFundAndSubFundsGridView()
        {
            int subFundsRowCount;
            this.FundAndSubFundsGridView.DataSource = this.fundMgmtDataSet.ListFundAndSubFundItems;
            subFundsRowCount = this.FundAndSubFundsGridView.OriginalRowCount;

            if (subFundsRowCount > 0)
            {
                this.FundAndSubFundsGridView.CurrentCell = this.FundAndSubFundsGridView[1, 0];
                if (this.FundAndSubFundsGridView.CurrentRowIndex > 0)
                {
                    this.FundAndSubFundsGridView.Rows[this.FundAndSubFundsGridView.CurrentRowIndex].Selected = false;
                    this.FundAndSubFundsGridView.Rows[0].Selected = true;
                }
            }
            else
            {
                this.FundAndSubFundsGridView.CurrentCell = null;
                this.FundAndSubFundsGridView.Rows[0].Selected = false;
            }

            if (subFundsRowCount >= this.FundAndSubFundsGridView.NumRowsVisible)
            {
                this.FundSubFundVScrollBar.Visible = false;
            }
            else
            {
                this.FundSubFundVScrollBar.Visible = true;
            }

            if (this.FundAndSubFundsGridView.OriginalRowCount >= this.FundAndSubFundsGridView.NumRowsVisible)
            {
                if (!Convert.ToBoolean(this.fundMgmtDataSet.ListFundAndSubFundItems.Rows[this.fundMgmtDataSet.ListFundAndSubFundItems.Rows.Count - 1][this.FundAndSubFundsGridView.EmptyRecordColumnName]))
                {
                    this.fundMgmtDataSet.ListFundAndSubFundItems.Rows.Add(this.fundMgmtDataSet.ListFundAndSubFundItems.NewRow());
                    this.fundMgmtDataSet.ListFundAndSubFundItems.Rows[this.fundMgmtDataSet.ListFundAndSubFundItems.Rows.Count - 1][this.FundAndSubFundsGridView.EmptyRecordColumnName] = true;
                    this.FundSubFundVScrollBar.Visible = false;
                }
            }
        }

        /// <summary>
        /// Populates the availablesubfunds gridview.
        /// </summary>
        private void PopulateAvailableSubFundsGridView()
        {
            int availableSubFundsRowCount;

            ////availableSubFundsRowCount = this.fundMgmtDataSet.ListAvailableSubFundItems.Rows.Count;
            this.AvailableSubFundsGridView.DataSource = this.fundMgmtDataSet.ListAvailableSubFundItems;
            availableSubFundsRowCount = this.AvailableSubFundsGridView.OriginalRowCount;

            if (availableSubFundsRowCount > 0)
            {
                this.AvailableSubFundsGridView.Enabled = true;
                //this.AvailableSubFundsGridView.CurrentCell = this.AvailableSubFundsGridView[1, 0];
                //if (this.AvailableSubFundsGridView.CurrentRowIndex > 0)
                //{
                //    this.AvailableSubFundsGridView.Rows[this.AvailableSubFundsGridView.CurrentRowIndex].Selected = false;
                //    this.AvailableSubFundsGridView.Rows[0].Selected = true;
                //}
            }
            else
            {
                this.AvailableSubFundsGridView.CurrentCell = null;
                this.AvailableSubFundsGridView.Rows[0].Selected = false;
                this.AvailableSubFundsGridView.Enabled = false;
            }

            if (availableSubFundsRowCount > this.AvailableSubFundsGridView.NumRowsVisible)
            {
                this.AvailableSubFundsVScorrlBar.Visible = false;
            }
            else
            {
                this.AvailableSubFundsVScorrlBar.Visible = true;
            }
        }

        #endregion

        #region Form Events

        /// <summary>
        /// Handles the Click event of the MoveUpButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MoveUpButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.MoveUpButton.Focus();
                this.AssociateSubFund();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the MoveDownButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MoveDownButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////this.MoveDownButton.Focus();
                this.DisAssociateSubFund();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the RollYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RollYearTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.RollYearTextBox.NumericTextBoxValue > 0)
                {
                    this.FillAvailableSubFundsGridView(null, Convert.ToInt32(this.RollYearTextBox.NumericTextBoxValue));
                }
                else
                {
                    this.fundMgmtDataSet.ListAvailableSubFundItems.Rows.Clear();
                    this.PopulateAvailableSubFundsGridView();
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// De associate the sub fund.
        /// </summary>
        private void DisAssociateSubFund()
        {
            if (this.FundAndSubFundsGridView.CurrentRowIndex >= 0)
            {
                int currentRowIndex = this.FundAndSubFundsGridView.CurrentRowIndex;
                DataGridViewSelectedRowCollection selectedSubFundRow;
                this.FundAndSubFundsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                this.FundAndSubFundsGridView.Rows[currentRowIndex].Selected = true;
                selectedSubFundRow = this.FundAndSubFundsGridView.SelectedRows;
                if (selectedSubFundRow.Count > 0)
                {
                    string[] subFunds = new string[selectedSubFundRow.Count];
                    int recIndex = -1;

                    for (int i = 0; i < selectedSubFundRow.Count; i++)
                    {
                        F15003FundMgmtData.ListAvailableSubFundItemsRow tempAvailableSubFundRow = this.fundMgmtDataSet.ListAvailableSubFundItems.NewListAvailableSubFundItemsRow();
                        int tempFundId = -1;
                        int tempSubFundId = -1;
                        Int16 tempRollYear = -1;
                        string tempSubFund = string.Empty;

                        Int32.TryParse(selectedSubFundRow[i].Cells["SubFundID"].Value.ToString(), out tempSubFundId);

                        tempSubFund = selectedSubFundRow[i].Cells["SubFund"].Value.ToString();
                        subFunds[i] = tempSubFund;

                        if (tempSubFundId > 0)
                        {
                            this.SetEditRecord();
                            if (!this.CheckExistingAvailableSubFundItem(tempSubFundId)) //// Need To Verify for Checking in Available SubFund Grid
                            {
                                ////Code To Add the SubFundItem To the AvailableSubFundsGridView
                                Int32.TryParse(selectedSubFundRow[i].Cells["FFundID"].Value.ToString(), out tempFundId);
                                Int16.TryParse(selectedSubFundRow[i].Cells["RollYear"].Value.ToString(), out tempRollYear);

                                tempAvailableSubFundRow.SubFund = tempSubFund;

                                tempAvailableSubFundRow.Description = selectedSubFundRow[i].Cells["Description"].Value.ToString();
                                tempAvailableSubFundRow.SubFundID = tempSubFundId;
                                tempAvailableSubFundRow.FundID = tempFundId;
                                tempAvailableSubFundRow.RollYear = tempRollYear;

                                if (Int16.Equals(tempRollYear, Convert.ToInt16(this.RollYearTextBox.NumericTextBoxValue)))
                                {
                                    if (this.AvailableSubFundsGridView.OriginalRowCount < this.AvailableSubFundsGridView.NumRowsVisible)
                                    {
                                        this.fundMgmtDataSet.ListAvailableSubFundItems.Rows.RemoveAt(this.AvailableSubFundsGridView.OriginalRowCount);
                                        this.fundMgmtDataSet.ListAvailableSubFundItems.AcceptChanges();
                                        this.fundMgmtDataSet.ListAvailableSubFundItems.Rows.InsertAt(tempAvailableSubFundRow, this.AvailableSubFundsGridView.OriginalRowCount);
                                        this.fundMgmtDataSet.ListAvailableSubFundItems.AcceptChanges();
                                    }
                                    else
                                    {
                                        if (Convert.ToBoolean(this.fundMgmtDataSet.ListAvailableSubFundItems.Rows[this.fundMgmtDataSet.ListAvailableSubFundItems.Rows.Count - 1][this.AvailableSubFundsGridView.EmptyRecordColumnName]))
                                        {
                                            this.fundMgmtDataSet.ListAvailableSubFundItems.Rows.RemoveAt(this.AvailableSubFundsGridView.OriginalRowCount);
                                            this.fundMgmtDataSet.ListAvailableSubFundItems.AcceptChanges();
                                        }

                                        this.fundMgmtDataSet.ListAvailableSubFundItems.Rows.InsertAt(tempAvailableSubFundRow, this.AvailableSubFundsGridView.OriginalRowCount);
                                        this.fundMgmtDataSet.ListAvailableSubFundItems.AcceptChanges();
                                    }
                                }
                            }
                        }
                    }

                    //// Code To Remove the SubFundItem From the FundAndSubFundsGridView
                    for (int j = 0; j < subFunds.Length; j++)
                    {
                        recIndex = this.RetrieveFundSubFundRecordIndex(subFunds[j]);
                        if (recIndex >= 0)
                        {
                            this.fundMgmtDataSet.ListFundAndSubFundItems.Rows.RemoveAt(recIndex);
                            this.fundMgmtDataSet.ListFundAndSubFundItems.AcceptChanges();
                        }
                    }
                }

                this.PopulateAvailableSubFundsGridView();
                this.PopulateFundAndSubFundsGridView();
            }
        }

        /// <summary>
        /// Associates the sub fund.
        /// </summary>
        private void AssociateSubFund()
        {
            DataGridViewSelectedRowCollection selectedAvailableSubFundRows;
            selectedAvailableSubFundRows = this.AvailableSubFundsGridView.SelectedRows;
            int[] subFundIds = new int[selectedAvailableSubFundRows.Count];
            int recIndex = -1;
            if (selectedAvailableSubFundRows.Count > 0)
            {
                for (int i = 0; i < selectedAvailableSubFundRows.Count; i++)
                {
                    F15003FundMgmtData.ListFundAndSubFundItemsRow tempSubFundRow = this.fundMgmtDataSet.ListFundAndSubFundItems.NewListFundAndSubFundItemsRow();
                    int tempFundId = -1;
                    int tempSubFundId = -1;
                    Int16 tempRollYear = -1;

                    Int32.TryParse(selectedAvailableSubFundRows[i].Cells["ASubFundID"].Value.ToString(), out tempSubFundId);
                    if (tempSubFundId > 0)
                    {
                        subFundIds[i] = tempSubFundId;
                        this.SetEditRecord();
                        if (!this.CheckExistingFundSubFundItem(tempSubFundId))
                        {
                            Int32.TryParse(selectedAvailableSubFundRows[i].Cells["AFundID"].Value.ToString(), out tempFundId);
                            Int16.TryParse(selectedAvailableSubFundRows[i].Cells["ARollYear"].Value.ToString(), out tempRollYear);

                            tempSubFundRow.SubFund = selectedAvailableSubFundRows[i].Cells["ASubFund"].Value.ToString();
                            tempSubFundRow.Description = selectedAvailableSubFundRows[i].Cells["ADescription"].Value.ToString();
                            tempSubFundRow.SubFundID = tempSubFundId;
                            tempSubFundRow.FundID = tempFundId;
                            tempSubFundRow.RollYear = tempRollYear;

                            if (this.FundAndSubFundsGridView.OriginalRowCount < this.FundAndSubFundsGridView.NumRowsVisible)
                            {
                                this.fundMgmtDataSet.ListFundAndSubFundItems.Rows.RemoveAt(this.FundAndSubFundsGridView.OriginalRowCount);
                                this.fundMgmtDataSet.ListFundAndSubFundItems.AcceptChanges();
                                this.fundMgmtDataSet.ListFundAndSubFundItems.Rows.InsertAt(tempSubFundRow, this.FundAndSubFundsGridView.OriginalRowCount);
                                this.fundMgmtDataSet.ListFundAndSubFundItems.AcceptChanges();
                            }
                            else
                            {
                                if (Convert.ToBoolean(this.fundMgmtDataSet.ListFundAndSubFundItems.Rows[this.fundMgmtDataSet.ListFundAndSubFundItems.Rows.Count - 1][this.FundAndSubFundsGridView.EmptyRecordColumnName]))
                                {
                                    this.fundMgmtDataSet.ListFundAndSubFundItems.Rows.RemoveAt(this.FundAndSubFundsGridView.OriginalRowCount);
                                    this.fundMgmtDataSet.ListFundAndSubFundItems.AcceptChanges();
                                }

                                this.fundMgmtDataSet.ListFundAndSubFundItems.Rows.InsertAt(tempSubFundRow, this.FundAndSubFundsGridView.OriginalRowCount);
                                this.fundMgmtDataSet.ListFundAndSubFundItems.AcceptChanges();
                            }

                            if (this.FundAndSubFundsGridView.OriginalRowCount >= this.FundAndSubFundsGridView.NumRowsVisible)
                            {
                                if (!Convert.ToBoolean(this.fundMgmtDataSet.ListFundAndSubFundItems.Rows[this.fundMgmtDataSet.ListFundAndSubFundItems.Rows.Count - 1][this.FundAndSubFundsGridView.EmptyRecordColumnName]))
                                {
                                    this.fundMgmtDataSet.ListFundAndSubFundItems.Rows.Add(this.fundMgmtDataSet.ListFundAndSubFundItems.NewRow());
                                    this.fundMgmtDataSet.ListFundAndSubFundItems.Rows[this.fundMgmtDataSet.ListFundAndSubFundItems.Rows.Count - 1][this.FundAndSubFundsGridView.EmptyRecordColumnName] = true;
                                    this.PopulateFundAndSubFundsGridView();
                                }
                            }
                        }
                    }
                }
            }

            //// Code To Remove the SubFundItem From the AvailableSubFundsGridView
            for (int j = 0; j < subFundIds.Length; j++)
            {
                recIndex = this.RetrieveAvailableSubFundRecordIndex(subFundIds[j]);
                if (recIndex >= 0)
                {
                    this.fundMgmtDataSet.ListAvailableSubFundItems.Rows.RemoveAt(recIndex);
                    this.fundMgmtDataSet.ListAvailableSubFundItems.AcceptChanges();
                }
            }

            this.PopulateFundAndSubFundsGridView();
            this.PopulateAvailableSubFundsGridView();
        }

        #endregion

        #region FundsAndSubFundsGridView Events/Methods

        /// <summary>
        /// Customizes the fund and sub funds grid view.
        /// </summary>
        private void CustomizeFundAndSubFundsGridView()
        {
            this.FundAndSubFundsGridView.AutoGenerateColumns = false;
            ////this.FundAndSubFundsGridView.MultiSelect = true;
            this.FundAndSubFundsGridView.PrimaryKeyColumnName = this.fundMgmtDataSet.ListFundAndSubFundItems.PrimaryKeyIDColumn.ColumnName.ToString();

            this.SubFund.DataPropertyName = this.fundMgmtDataSet.ListFundAndSubFundItems.SubFundColumn.ColumnName.ToString();
            this.Description.DataPropertyName = this.fundMgmtDataSet.ListFundAndSubFundItems.DescriptionColumn.ColumnName.ToString();
            this.SubFundID.DataPropertyName = this.fundMgmtDataSet.ListFundAndSubFundItems.SubFundIDColumn.ColumnName.ToString();
            this.FFundID.DataPropertyName = this.fundMgmtDataSet.ListFundAndSubFundItems.FundIDColumn.ColumnName.ToString();
            this.RollYear.DataPropertyName = this.fundMgmtDataSet.ListFundAndSubFundItems.RollYearColumn.ColumnName.ToString();
        }

        /// <summary>
        /// Handles the Enter event of the FundAndSubFundsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FundAndSubFundsGridView_Enter(object sender, EventArgs e)
        {
            try
            {
                if (this.FundAndSubFundsGridView.CurrentRow != null)
                {
                    this.FundAndSubFundsGridView.CurrentRow.Cells["SubFund"].Selected = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the FundAndSubFundsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void FundAndSubFundsGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                //// Only paint if desired, formattable column

                if (e.ColumnIndex == this.FundAndSubFundsGridView.Columns["Description"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.FundAndSubFundsGridView.Rows[e.RowIndex].Cells["Description"].Value.ToString().Trim()))
                    {
                        string val = e.Value.ToString();
                        if (!string.IsNullOrEmpty(val))
                        {
                            if (string.Equals(val.ToString(), SharedFunctions.GetResourceString("F15003InvalidFundMessage")))
                            {
                                e.CellStyle.ForeColor = Color.FromArgb(128, 0, 0);
                            }
                            else
                            {
                                e.CellStyle.ForeColor = Color.Black;
                                e.FormattingApplied = true;
                            }
                        }
                        else
                        {
                            e.Value = "";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellParsing event of the FundAndSubFundsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void FundAndSubFundsGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                //// Only paint if desired column

                if (e.ColumnIndex == this.FundAndSubFundsGridView.Columns["SubFund"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    //// Only paint if text provided, Only paint if desired text is in cell

                    if (e.Value != null)
                    {
                        string tempvalue = e.Value.ToString().Trim();
                        if (string.IsNullOrEmpty(tempvalue))
                        {
                            e.Value = string.Empty;
                        }

                        e.ParsingApplied = true;
                        this.FundAndSubFundsGridView.RefreshEdit();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellBeginEdit event of the FundAndSubFundsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void FundAndSubFundsGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            ////this.FundAndSubFundsGridView.AllowSorting = false;
            this.edit = true;
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the FundAndSubFundsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void FundAndSubFundsGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && !Convert.ToBoolean(this.fundMgmtDataSet.ListFundAndSubFundItems.Rows[e.RowIndex][this.FundAndSubFundsGridView.EmptyRecordColumnName]))
                {
                    this.DisAssociateSubFund();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the FundAndSubFundsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void FundAndSubFundsGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.edit)
            {
                try
                {
                    if (e.ColumnIndex == this.FundAndSubFundsGridView.Columns["SubFund"].Index)
                    {
                        string tempUserSubFund = string.Empty;
                        int tempSubFundId = -1;
                        int recIndex = -1;
                        F15003FundMgmtData tempDataSet = new F15003FundMgmtData();
                        tempUserSubFund = this.FundAndSubFundsGridView["SubFund", e.RowIndex].Value.ToString();

                        if (!string.IsNullOrEmpty(tempUserSubFund.Trim()))
                        {
                            tempDataSet = this.form15003Controll.WorkItem.F15003_ListAvailableSubFunds(tempUserSubFund, string.Empty, this.RollYearTextBox.NumericTextBoxValue, null);
                            if (tempDataSet.ListAvailableSubFundItems.Rows.Count > 0)
                            {
                                Int32.TryParse(tempDataSet.ListAvailableSubFundItems.Rows[0][tempDataSet.ListAvailableSubFundItems.SubFundIDColumn.ColumnName].ToString(), out tempSubFundId);
                                if (!this.CheckExistingFundSubFundItem(tempSubFundId))
                                {
                                    this.edit = false;
                                    this.FundAndSubFundsGridView["SubFundID", e.RowIndex].Value = tempDataSet.ListAvailableSubFundItems.Rows[0][tempDataSet.ListAvailableSubFundItems.SubFundIDColumn.ColumnName].ToString();
                                    this.FundAndSubFundsGridView["Description", e.RowIndex].Value = tempDataSet.ListAvailableSubFundItems.Rows[0][tempDataSet.ListAvailableSubFundItems.DescriptionColumn.ColumnName].ToString();
                                    this.FundAndSubFundsGridView["RollYear", e.RowIndex].Value = tempDataSet.ListAvailableSubFundItems.Rows[0][tempDataSet.ListAvailableSubFundItems.RollYearColumn.ColumnName].ToString();

                                    //// Code for Removing the SubFund From Available SubFunds Grid
                                    recIndex = this.RetrieveAvailableSubFundRecordIndex(tempSubFundId);
                                    if (recIndex >= 0)
                                    {
                                        this.fundMgmtDataSet.ListAvailableSubFundItems.Rows.RemoveAt(recIndex);
                                        this.fundMgmtDataSet.ListAvailableSubFundItems.AcceptChanges();
                                        this.PopulateAvailableSubFundsGridView();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("The SubFund entered already Exists", "TerraScan T2 - Duplicate SubFund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.edit = false;
                                    this.FundAndSubFundsGridView["SubFundID", e.RowIndex].Value = string.Empty;
                                    this.FundAndSubFundsGridView["Description", e.RowIndex].Value = string.Empty;
                                    this.FundAndSubFundsGridView["RollYear", e.RowIndex].Value = string.Empty;
                                    if (!Convert.ToBoolean(this.fundMgmtDataSet.ListFundAndSubFundItems.Rows[e.RowIndex][this.FundAndSubFundsGridView.EmptyRecordColumnName]) && string.IsNullOrEmpty(this.fundMgmtDataSet.ListFundAndSubFundItems.Rows[e.RowIndex][this.fundMgmtDataSet.ListFundAndSubFundItems.SubFundIDColumn].ToString()))
                                    {
                                        this.fundMgmtDataSet.ListFundAndSubFundItems.Rows[e.RowIndex][this.FundAndSubFundsGridView.EmptyRecordColumnName] = true;
                                    }
                                }
                            }
                            else
                            {
                                this.edit = false;
                                this.FundAndSubFundsGridView["SubFundID", e.RowIndex].Value = 0;
                                this.FundAndSubFundsGridView["SubFund", e.RowIndex].Value = tempUserSubFund.Replace("'", "\"").Trim();
                                this.FundAndSubFundsGridView["Description", e.RowIndex].Value = SharedFunctions.GetResourceString("F15003InvalidFundMessage");
                            }

                            this.FundAndSubFundsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                            this.fundMgmtDataSet.ListFundAndSubFundItems.AcceptChanges();
                        }
                        else
                        {
                            this.fundMgmtDataSet.ListFundAndSubFundItems.Rows.RemoveAt(this.FundAndSubFundsGridView.CurrentRowIndex);
                            this.fundMgmtDataSet.ListFundAndSubFundItems.AcceptChanges();
                        }

                        this.PopulateFundAndSubFundsGridView();
                    }

                    if (this.FundAndSubFundsGridView.OriginalRowCount >= this.FundAndSubFundsGridView.NumRowsVisible)
                    {
                        if (!Convert.ToBoolean(this.fundMgmtDataSet.ListFundAndSubFundItems.Rows[this.fundMgmtDataSet.ListFundAndSubFundItems.Rows.Count - 1][this.FundAndSubFundsGridView.EmptyRecordColumnName]))
                        {
                            this.fundMgmtDataSet.ListFundAndSubFundItems.Rows.Add(this.fundMgmtDataSet.ListFundAndSubFundItems.NewRow());
                            this.fundMgmtDataSet.ListFundAndSubFundItems.Rows[this.fundMgmtDataSet.ListFundAndSubFundItems.Rows.Count - 1][this.FundAndSubFundsGridView.EmptyRecordColumnName] = true;
                            this.FundSubFundVScrollBar.Visible = false;
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
        /// Handles the RowEnter event of the FundAndSubFundsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void FundAndSubFundsGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool hasValues = false;
                if (e.RowIndex >= 1)
                {
                    if ((string.IsNullOrEmpty(this.FundAndSubFundsGridView.Rows[(e.RowIndex - 1)].Cells["SubFund"].Value.ToString().Trim())))
                    {
                        if (e.RowIndex + 1 < this.FundAndSubFundsGridView.RowCount)
                        {
                            for (int i = e.RowIndex; i < this.FundAndSubFundsGridView.RowCount; i++)
                            {
                                if (this.FundAndSubFundsGridView.Rows[i].Cells["SubFund"].Value != null && !String.IsNullOrEmpty(this.FundAndSubFundsGridView.Rows[i].Cells["SubFund"].Value.ToString().Trim()))
                                {
                                    hasValues = true;
                                    break;
                                }
                            }

                            if (hasValues)
                            {
                                this.FundAndSubFundsGridView["SubFund", e.RowIndex].ReadOnly = false;
                                this.FundAndSubFundsGridView.Rows[e.RowIndex].Selected = true;
                            }
                            else
                            {
                                this.FundAndSubFundsGridView["SubFund", e.RowIndex].ReadOnly = true;
                                this.FundAndSubFundsGridView.Rows[e.RowIndex].Selected = true;
                            }
                        }
                        else
                        {
                            this.FundAndSubFundsGridView["SubFund", e.RowIndex].ReadOnly = true;
                            this.FundAndSubFundsGridView.Rows[e.RowIndex].Selected = true;
                        }
                    }
                    else
                    {
                        this.FundAndSubFundsGridView["SubFund", e.RowIndex].ReadOnly = false;
                        this.FundAndSubFundsGridView.Rows[e.RowIndex].Selected = true;
                    }
                }
                //// Need to Get Confirmation
                if (e.RowIndex == 0)
                {
                    this.FundAndSubFundsGridView["SubFund", e.RowIndex].ReadOnly = false;
                    this.FundAndSubFundsGridView.Rows[e.RowIndex].Selected = true;
                }

                ////this.currentRowIndex = e.RowIndex;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataError event of the FundAndSubFundsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void FundAndSubFundsGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the FundAndSubFundsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void FundAndSubFundsGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
                e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                e.Control.Validated += new EventHandler(this.Control_Validated);
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
                this.FundAndSubFundsGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
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

        #endregion

        #region Available SubFunds GridView Events/Methods

        /// <summary>
        /// Customizes the available sub funds grid view.
        /// </summary>
        private void CustomizeAvailableSubFundsGridView()
        {
            this.AvailableSubFundsGridView.AutoGenerateColumns = false;
            this.AvailableSubFundsGridView.PrimaryKeyColumnName = this.fundMgmtDataSet.ListAvailableSubFundItems.SubFundIDColumn.ColumnName.ToString();
            this.AvailableSubFundsGridView.MultiSelect = true;

            this.AvailableSubFundsGridView.Columns["ASubFund"].DataPropertyName = this.fundMgmtDataSet.ListAvailableSubFundItems.SubFundColumn.ColumnName.ToString();
            this.AvailableSubFundsGridView.Columns["ADescription"].DataPropertyName = this.fundMgmtDataSet.ListAvailableSubFundItems.DescriptionColumn.ColumnName.ToString();
            this.AvailableSubFundsGridView.Columns["ASubFundID"].DataPropertyName = this.fundMgmtDataSet.ListAvailableSubFundItems.SubFundIDColumn.ColumnName.ToString();
            this.AvailableSubFundsGridView.Columns["AFundID"].DataPropertyName = this.fundMgmtDataSet.ListAvailableSubFundItems.FundIDColumn.ColumnName.ToString();
            this.AvailableSubFundsGridView.Columns["ARollYear"].DataPropertyName = this.fundMgmtDataSet.ListAvailableSubFundItems.RollYearColumn.ColumnName.ToString();
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the AvailableSubFundsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AvailableSubFundsGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && !Convert.ToBoolean(this.fundMgmtDataSet.ListAvailableSubFundItems.Rows[e.RowIndex][this.AvailableSubFundsGridView.EmptyRecordColumnName]))
                {
                    this.AssociateSubFund();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Common Methods

        /// <summary>
        /// Gets the year.
        /// </summary>
        private void GetYear()
        {
            CommentsData getYearDataSet = new CommentsData();
            getYearDataSet = this.form15003Controll.WorkItem.GetConfigDetails("TR_RollYear");
            int tempRollYear = -1;
            int.TryParse(getYearDataSet.GetCommentsConfigDetails.Rows[0][getYearDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString(), out tempRollYear);
            if (tempRollYear.Equals(0))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("InvalidFieldYear"), "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.RollYearTextBox.Text = tempRollYear.ToString();
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                if (!this.pageLoadStatus)   //// && this.pageMode == TerraScanCommon.PageModeTypes.View
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
        /// Locks the controls.
        /// </summary>
        /// <param name="lockValue">if set to <c>true</c> [lock value].</param>
        private void LockControls(bool lockValue)
        {
            this.FundTextBox.LockKeyPress = lockValue;
            this.RollYearTextBox.LockKeyPress = lockValue;
            this.DescriptionTextBox.LockKeyPress = lockValue;
            this.FundTypeComboBox.Enabled = !lockValue;
            this.FundPanel.Enabled = !lockValue;
            this.RoleYearPanel.Enabled = !lockValue;
            this.DescriptionPanel.Enabled = !lockValue;
            this.FundTypePanel.Enabled = !lockValue;
            this.FundsAndSubFundsGridPanel.Enabled = !lockValue;
            this.AvailableSubFundsGridPanel.Enabled = !lockValue;
            this.MoveUpButton.Enabled = !lockValue;
            this.MoveDownButton.Enabled = !lockValue;
        }

        /// <summary>
        /// Inits the type combo box.
        /// </summary>
        private void InitTypeComboBox()
        {
            this.pageLoadStatus = true;
            F15003FundMgmtData.ListFundTypeDataTable listFundType = new F15003FundMgmtData.ListFundTypeDataTable();
            listFundType.Merge(this.form15003Controll.WorkItem.F15003_ListFundType());
            this.FundTypeComboBox.ValueMember = this.fundMgmtDataSet.ListFundType.FundGroupIDColumn.ColumnName;
            this.FundTypeComboBox.DisplayMember = this.fundMgmtDataSet.ListFundType.DescriptionColumn.ColumnName;
            this.FundTypeComboBox.DataSource = listFundType;
            if (this.FundTypeComboBox.Items.Count > 0)
            {
                this.FundTypeComboBox.SelectedIndex = 0;
            }

            this.pageLoadStatus = false;
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
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Clears the sub fund header.
        /// </summary>
        private void ClearFundHeader()
        {
            this.fundId = null;
            this.FundTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            if (this.FundTypeComboBox.Items.Count > 0)
            {
                this.FundTypeComboBox.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Validates the roll year.
        /// </summary>
        /// <returns>Validated Status</returns>
        private bool ValidateRollYear()
        {
            try
            {
                short tempRollYear;
                Int16.TryParse(this.RollYearTextBox.Text.Trim(), out tempRollYear);
                if (tempRollYear < 1900 || tempRollYear > 2079)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
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

            if (string.IsNullOrEmpty(this.FundTextBox.Text.Trim()))
            {
                this.FundTextBox.Focus();
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F15003RequiredFieldMissing");
            }
            else if (string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                this.RollYearTextBox.Focus();
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F15003RequiredFieldMissing");
            }
            else if (string.IsNullOrEmpty(this.FundTypeComboBox.Text.Trim()))
            {
                this.FundTypeComboBox.Focus();
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F15003RequiredFieldMissing");
            }
            else if (this.ValidateRollYear())
            {
                this.RollYearTextBox.Focus();
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.RequiredFieldMissing = false;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("InvalidFieldYear");
            }
            ////else if (!this.CheckValidFundNumber())
            ////{
            ////    MessageBox.Show("The given input values violates the maximum / minimum data range", "TerraScan – Invalid Field value", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////    this.FundTextBox.Text = string.Empty;
            ////    this.FundTextBox.Focus();
            ////    sliceValidationFields.DisableNewMethod = true;
            ////    sliceValidationFields.RequiredFieldMissing = false;
            ////    sliceValidationFields.ErrorMessage = string.Empty;
            ////}
            else if (!this.CheckDuplicateFund())
            {
                DialogResult result = MessageBox.Show(SharedFunctions.GetResourceString("F15003DuplicateFundExists"), SharedFunctions.GetResourceString("F15003DuplicateFundTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    this.FundTextBox.Focus();
                    sliceValidationFields.DisableNewMethod = true;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                }
            }
            else if (this.CheckInvalidSubFunds())
            {
                DialogResult result = MessageBox.Show(SharedFunctions.GetResourceString("F15003InvalidRollYearSubFundsText"), SharedFunctions.GetResourceString("F15003InvalidRollYearSubFundTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    sliceValidationFields.DisableNewMethod = false;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                }
                else
                {
                    sliceValidationFields.DisableNewMethod = true;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                }
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Fills the available sub funds grid view.
        /// </summary>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="tempRollYear">The temp roll year.</param>
        private void FillAvailableSubFundsGridView(int? subFund, int? tempRollYear)
        {

            if (!subFund.HasValue)
            {
                this.fundMgmtDataSet.ListAvailableSubFundItems.Rows.Clear();
                this.fundMgmtDataSet.Merge(this.form15003Controll.WorkItem.F15003_ListAvailableSubFunds(string.Empty, string.Empty, tempRollYear, null));
                this.PopulateAvailableSubFundsGridView();
            }
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
        /// Retrieves the index of the fund sub fund record.
        /// </summary>
        /// <param name="tempSubFund">The temp sub fund.</param>
        /// <returns>index of the current record</returns>
        private int RetrieveFundSubFundRecordIndex(string tempSubFund)
        {
            int tempIndex = -1;

            try
            {
                if (!string.IsNullOrEmpty(tempSubFund.Trim()))
                {
                    DataTable tempDataTable = this.fundMgmtDataSet.ListFundAndSubFundItems.Copy();
                    string findExp = tempSubFund.Replace("'", "\''");
                    tempDataTable.DefaultView.RowFilter = string.Concat(this.fundMgmtDataSet.ListFundAndSubFundItems.SubFundColumn.ColumnName, " = ", "'" + findExp + "'");

                    if (tempDataTable.DefaultView.Count > 0)
                    {
                        tempIndex = tempDataTable.Rows.IndexOf(tempDataTable.DefaultView[0].Row);
                    }
                }

                return tempIndex;
            }
            catch (Exception)
            {
                //ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                return tempIndex;
            }
        }

        /// <summary>
        /// Retrieves the index of the available sub fund record.
        /// </summary>
        /// <param name="tempSubFundId">The temp sub fund id.</param>
        /// <returns>returns the Index of the subFundId</returns>
        private int RetrieveAvailableSubFundRecordIndex(int tempSubFundId)
        {
            int tempIndex = -1;

            try
            {
                DataTable tempDataTable = this.fundMgmtDataSet.ListAvailableSubFundItems.Copy();
                tempDataTable.DefaultView.RowFilter = string.Concat(this.fundMgmtDataSet.ListAvailableSubFundItems.SubFundIDColumn.ColumnName, " = ", tempSubFundId);

                if (tempDataTable.DefaultView.Count > 0)
                {
                    tempIndex = tempDataTable.Rows.IndexOf(tempDataTable.DefaultView[0].Row);
                }

                return tempIndex;
            }
            catch (Exception)
            {
                return tempIndex;
            }
        }

        /// <summary>
        /// Checks the existing fund sub fund item.
        /// </summary>
        /// <param name="tempSubFundId">The temp sub fund id.</param>
        /// <returns>returns the Status of FundID Existsance</returns>
        private bool CheckExistingFundSubFundItem(int tempSubFundId)
        {
            DataRow[] dataRow;
            string findExp = "SubFundID =" + tempSubFundId.ToString();
            dataRow = this.fundMgmtDataSet.ListFundAndSubFundItems.Select(findExp);
            if (dataRow.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks the existing available sub fund item.
        /// </summary>
        /// <param name="tempSubFundId">The temp sub fund id.</param>
        /// <returns>returns the status of subFundId Exist</returns>
        private bool CheckExistingAvailableSubFundItem(int tempSubFundId)
        {
            DataRow[] dataRow;
            string findExp = "SubFundID =" + tempSubFundId.ToString();
            dataRow = this.fundMgmtDataSet.ListAvailableSubFundItems.Select(findExp);
            if (dataRow.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the district fund itmes.
        /// </summary>
        /// <returns>string contains the FundItems Xml</returns>
        private string GetFundItmesXml()
        {
            DataTable tempSubFundItemsDataTable = new DataTable();
            string subFundItems = string.Empty;

            foreach (DataColumn column in this.fundMgmtDataSet.ListFundAndSubFundItems.Columns)
            {
                if (column.ColumnName == this.fundMgmtDataSet.ListFundAndSubFundItems.SubFundIDColumn.ColumnName)
                {
                    tempSubFundItemsDataTable.Columns.Add(new DataColumn(column.ColumnName));
                }

                if (column.ColumnName == this.fundMgmtDataSet.ListFundAndSubFundItems.RollYearColumn.ColumnName)
                {
                    tempSubFundItemsDataTable.Columns.Add(new DataColumn(column.ColumnName));
                }
            }

            foreach (DataRow dr in this.fundMgmtDataSet.ListFundAndSubFundItems.Rows)
            {
                DataRow subFundItemsDataRow = tempSubFundItemsDataTable.NewRow();

                if (dr["SubFundID"] != DBNull.Value && dr["RollYear"] != DBNull.Value)
                {
                    if (!string.IsNullOrEmpty((dr["SubFundID"].ToString())) && !string.IsNullOrEmpty((dr["RollYear"].ToString())))
                    {
                        if (string.Equals(this.RollYearTextBox.Text.Trim(), dr["RollYear"].ToString()))
                        {
                            foreach (DataColumn column in this.fundMgmtDataSet.ListFundAndSubFundItems.Columns)
                            {
                                if (column.ColumnName == this.fundMgmtDataSet.ListFundAndSubFundItems.SubFundIDColumn.ColumnName)
                                {
                                    subFundItemsDataRow[column.ColumnName] = dr[column.ColumnName];
                                }

                                if (column.ColumnName == this.fundMgmtDataSet.ListFundAndSubFundItems.RollYearColumn.ColumnName)
                                {
                                    subFundItemsDataRow[column.ColumnName] = dr[column.ColumnName];
                                }
                            }

                            tempSubFundItemsDataTable.Rows.Add(subFundItemsDataRow);
                        }
                    }
                }
            }

            subFundItems = TerraScanCommon.GetXmlString(tempSubFundItemsDataTable);
            return subFundItems;
        }

        /// <summary>
        /// Saves the district fund ditails.
        /// </summary>
        private void SaveFundDitails()
        {
            int returnValue = -1;
            string subFundItemsXml = this.GetFundItmesXml();
            returnValue = this.form15003Controll.WorkItem.F15003_CreateOrEditFundMgmt(this.FundIdentity, this.FundTextBox.Text, Convert.ToInt32(this.RollYearTextBox.NumericTextBoxValue), this.DescriptionTextBox.Text.Trim(), Convert.ToInt32(this.FundTypeComboBox.SelectedValue), subFundItemsXml, TerraScanCommon.UserId);
            if (returnValue != -1)
            {
                this.FundIdentity = returnValue;
            }

            SliceReloadActiveRecord currentSliceInfo;
            currentSliceInfo.MasterFormNo = this.masterFormNo;
            currentSliceInfo.SelectedKeyId = returnValue;
            ////to refresh the master form with the return keyid
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
        }

        /// <summary>
        /// Checks the duplicate district.
        /// </summary>
        /// <returns>duplicate record status</returns>
        private bool CheckDuplicateFund()
        {
            try
            {
                int errorId = -1;
                errorId = this.form15003Controll.WorkItem.F15003_CheckFund(this.FundIdentity, this.FundTextBox.Text.Trim(), Convert.ToInt32(this.RollYearTextBox.NumericTextBoxValue));
                if (errorId != -1)
                {
                    return true;
                }

                return false;
            }
            catch (SoapException ex)
            {
                //ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return false;
            }
        }

        /// <summary>
        /// Checks the invalid funds.
        /// </summary>
        /// <returns>returns the invalid funds status</returns>
        private bool CheckInvalidSubFunds()
        {
            foreach (DataRow invalidRow in this.fundMgmtDataSet.ListFundAndSubFundItems.Rows)
            {
                if (!string.IsNullOrEmpty(invalidRow[this.fundMgmtDataSet.ListFundAndSubFundItems.RollYearColumn.ColumnName].ToString()))
                {
                    if (!string.Equals(this.RollYearTextBox.Text.Trim(), invalidRow[this.fundMgmtDataSet.ListFundAndSubFundItems.RollYearColumn.ColumnName].ToString()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Sets the grid sort mode.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void SetGridSortMode(bool enable)
        {
            this.FundAndSubFundsGridView.AllowSorting = enable;
            this.AvailableSubFundsGridView.AllowSorting = enable;
        }

        ///// <summary>
        ///// Checks the valid fund number.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        ////private bool CheckValidFundNumber()
        ////{
        ////    try
        ////    {
        ////        if (this.FundTextBox.tNumericTextBoxValue > 0 && this.FundTextBox.NumericTextBoxValue <= Int16.MaxValue)
        ////        {
        ////            return true;
        ////        }

        ////        return false;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
        ////        return false;
        ////    }
        ////}

        #endregion
    }
}
