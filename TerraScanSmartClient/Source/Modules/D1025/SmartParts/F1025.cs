//----------------------------------------------------------------------------------------------
// <copyright file="F1025.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1025-AutoFundTransfer.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 4/5/2009         Sadha Shivudu M    Implemented TSCO# 6520 Added New Source Column
//**********************************************************************************************

namespace D1025
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
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
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Infrastructure.Interface.Constants;
    using System.Web.Services.Protocols;
    using System.Globalization;

    /// <summary>
    /// UserControl for F1025
    /// </summary>
    public partial class F1025 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// form1025Control Variable 
        /// </summary> 
        private F1025Controller form1025Controller;

        /// <summary>
        /// AutoFundTransferData variable
        /// </summary>
        private F1025AutoFundTransferData autoFundData = new F1025AutoFundTransferData();

        /// <summary>
        /// SubFundManagementData variable
        /// </summary>
        private F9503SubFundManagementData subFundManagementData = new F9503SubFundManagementData();

        /// <summary>
        /// Variable for Record count
        /// </summary>
        private int autoFundRecordCount;

        /// <summary>
        /// rollYear variable
        /// </summary>
        private short rollYear;

        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyId;

        /// <summary>
        /// additionalOperationSmartPart
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart;

        /// <summary>
        /// operationSmartPart
        /// </summary>
        private OperationSmartPart operationSmartPart;

        /// <summary>
        /// footerSmartPart
        /// </summary>
        private FooterSmartPart footerSmartPart;

        /// <summary>
        /// reportActionSmartPart
        /// </summary>
        private ReportActionSmartPart reportActionSmartPart;

        /// <summary>
        /// TerraScanCommon.PageStatus Enum - used to find normal or filtered mose
        /// </summary>
        private TerraScanCommon.PageStatus pageStatus;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// the rowindex selected for delete
        /// </summary>
        private int selectedFundRowId;

        /// <summary>
        /// the id selected for delete
        /// </summary>
        private int autoTransferId;

        /// <summary>
        /// auto complete string collection
        /// </summary>
        private AutoCompleteStringCollection scautoComplete = new AutoCompleteStringCollection();

        /// <summary>
        /// variable to get the selected roll year
        /// </summary>
        private short selectedRollYear;

        /// <summary>
        /// variable holds the Invalid Percent Flag.
        /// </summary>
        private bool flagInvalidPercent = true;

        /// <summary>
        /// variable holds the MissingRequired field Flag.
        /// </summary>
        private bool flagMissingRequiredField = true;

        /// <summary>
        /// variable holds the form load
        /// </summary>
        private bool formLoad = false;

        /// <summary>
        /// variable for deletd row
        /// </summary>
        private bool rowDeleted;

        /// <summary>
        /// tempcol
        /// </summary>
        private int tempcol;

        /// <summary>
        /// Instance for F1515FundSelectionData
        /// </summary>
        private F1515SubFundSelectionData subFundSelectionData = new F1515SubFundSelectionData();

        /// <summary>
        /// rollYearValue
        /// </summary>
        private int rollYearValue;

        /// <summary>
        /// To NewRow
        /// </summary>
        private bool newRecord;

        /// <summary>
        /// This datatable contains the table having Source types
        /// </summary>
        private DataTable sourceDatatable;

        /// <summary>
        /// variable to hold the current row SourceKeyId value
        /// </summary>
        private int currentRowSourceKeyId;

        /// <summary>
        /// variable to hold the current row SourceKeyName value
        /// </summary>
        private string currentRowSourceKeyName;

        /// <summary>
        /// variable to hold the current row IsSubFund value.
        /// </summary>
        private byte currentRowIsSubFundValue;

        #endregion Member Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1025"/> class.
        /// </summary>
        public F1025()
        {
            this.InitializeComponent();
            this.CreatesourceDatatable();
            this.AutoFundPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AutoFundPictureBox.Height, this.AutoFundPictureBox.Width, "Account Detail", 28, 81, 128);
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        #endregion Constructor

        #region EventPublication

        /// <summary>
        /// Event Publication for SetFormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// Event Publication for PageStatusActivated
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> PageStatusActivated;

        /// <summary>
        /// Get Cancel Button
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> GetCancelButton;

        /// <summary>
        /// event publication for SetCancelButton
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_SetCancelButton, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<Button>> SetCancelButton;

        #endregion EventPublication

        #region Property

        /// <summary>
        /// Gets or sets the form1025 controller.
        /// </summary>
        /// <value>The form1025 controller.</value>
        [CreateNew]
        public F1025Controller Form1025Controller
        {
            get { return this.form1025Controller as F1025Controller; }
            set { this.form1025Controller = value; }
        }

        /// <summary>
        /// Gets or sets the roll year.
        /// </summary>
        /// <value>The roll year.</value>
        private short RollYear
        {
            get
            {
                return this.rollYear;
            }

            set
            {
                this.rollYear = value;
            }
        }

        /// <summary>
        /// Gets or sets the AutoTransferId.
        /// </summary>
        /// <value>The AutoTransferId.</value>
        private int AutoTransferId
        {
            get
            {
                return this.autoTransferId;
            }

            set
            {
                this.autoTransferId = value;
                this.additionalOperationSmartPart.KeyId = this.autoTransferId;
            }
        }

        /// <summary>
        /// Gets or sets the page mode.
        /// </summary>
        /// <value>The page mode.</value>
        private TerraScanCommon.PageModeTypes PageMode
        {
            get
            {
                return this.pageMode;
            }

            set
            {
                this.pageMode = value;
            }
        }

        /// <summary>
        /// Gets or sets the page status.
        /// </summary>
        /// <value>The page status.</value>
        private TerraScanCommon.PageStatus PageStatus
        {
            get { return this.pageStatus; }
            set { this.pageStatus = value; }
        }
        #endregion Property

        #region EventSubscription

        /// <summary>
        /// Handles the Button Click Event For the WorkSpace
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_OperationButtonClick, Thread = ThreadOption.UserInterface)]
        public void OperationButtonClick(object sender, DataEventArgs<string> e)
        {
            switch (e.Data)
            {
                case "SAVE":
                    this.SaveButton_Click();
                    break;
                case "CANCEL":
                    this.CancelButton_Click();
                    break;
            }
        }

        /// <summary>
        /// Called when [audit link click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.AuditLinkClick, ThreadOption.UserInterface)]
        public void OnAuditLinkClick(object sender, DataEventArgs<LinkLabel> eventArgs)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////// calling  Common Function For Report
                Hashtable reportOptionalParameter = new Hashtable();
                reportOptionalParameter.Add("AutoTransferId", this.autoTransferId.ToString());
                TerraScanCommon.ShowReport(102590, Report.ReportType.Preview, reportOptionalParameter);
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
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_PreviewButtonClick, Thread = ThreadOption.UserInterface)]
        public void PreviewButtonClick(object sender, DataEventArgs<Button> e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Hashtable ht = new Hashtable();
                ht.Add("KeyID", this.keyId);
                TerraScanCommon.ShowReport(102510, TerraScan.Common.Reports.Report.ReportType.Preview, ht);
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
        /// Checks the page status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_CheckPageStatus, Thread = ThreadOption.UserInterface)]
        public void CheckPageStatus(object sender, DataEventArgs<Button> e)
        {
            try
            {
                if (this.CheckPageStatus())
                {
                    this.PageStatusActivated(this, new DataEventArgs<Button>(e.Data));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Gets the form status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The source of the event</param>
        [EventSubscription(EventTopics.D9001_ShellForm_GetFormStatus, Thread = ThreadOption.UserInterface)]
        public void GetFormStatus(object sender, DataEventArgs<string> e)
        {
            try
            {
                if (e.Data == this.GetType().Name)
                {
                    this.form1025Controller.WorkItem.State["FormStatus"] = this.CheckPageStatus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        ////khaja added code to fix Bug#4598

        /// <summary>
        /// Sets the form cancel button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_ShellForm_SetFormCancelButton, Thread = ThreadOption.UserInterface)]
        public void SetFormCancelButton(object sender, DataEventArgs<string> e)
        {
            this.GetCancelButton(this, new DataEventArgs<string>(string.Empty));
        }

        /// <summary>
        /// Loads the cancel button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_LoadCancelButton, Thread = ThreadOption.UserInterface)]
        public void LoadCancelButton(object sender, DataEventArgs<Button> e)
        {
            this.SetCancelButton(this, new DataEventArgs<Button>(e.Data));
        }
        ////
        #endregion EventSubscription

        #region Events

        /// <summary>
        /// Handles the Load event of the F1025 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1025_Load(object sender, EventArgs e)
        {
            try
            {
                this.formLoad = true;
                this.LoadWorkSpaces();
                this.CustomizeItemListingGridView();
                this.InitializeRollYearCombo();
                this.PopulateAutoFundTransferDataGridView();
                this.formLoad = true;
                if (this.operationSmartPart != null)
                {
                    this.ParentForm.CancelButton = this.operationSmartPart.RetrieveCancelButton;
                }

                this.AutoFundMenu.Visible = false;
                this.RollYearCombo.Focus();
                this.formLoad = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the RollYearCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RollYearCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    ////Auto coumplete code added by Sriparameswari
                    string subFundValue = string.Empty;
                    string descriptionValue = string.Empty;
                    this.rollYearValue = Convert.ToInt32(this.RollYearCombo.SelectedValue);
                    this.subFundSelectionData = this.form1025Controller.WorkItem.F1515_GetSubFundSelection(subFundValue, descriptionValue, this.rollYearValue, 1);
                    this.scautoComplete.Clear();
                    if (this.subFundSelectionData.GetSubFundSelection.Rows.Count > 0)
                    {
                        for (int i = 0; i <= this.subFundSelectionData.GetSubFundSelection.Rows.Count - 1; i++)
                        {
                            this.scautoComplete.Add(this.subFundSelectionData.GetSubFundSelection.Rows[i][this.subFundSelectionData.GetSubFundSelection.SubFundColumn.ColumnName].ToString());
                        }
                    }

                    this.ComboChange();
                }
                else
                {
                    ////Auto coumplete code added by Sriparameswari
                    string subFundValue = string.Empty;
                    string descriptionValue = string.Empty;
                    this.rollYearValue = Convert.ToInt32(this.RollYearCombo.SelectedValue);
                    this.scautoComplete.Clear();
                    this.subFundSelectionData = this.form1025Controller.WorkItem.F1515_GetSubFundSelection(subFundValue, descriptionValue, this.rollYearValue, 1);
                    this.scautoComplete.Clear();
                    if (this.subFundSelectionData.GetSubFundSelection.Rows.Count > 0)
                    {
                        for (int i = 0; i <= this.subFundSelectionData.GetSubFundSelection.Rows.Count - 1; i++)
                        {
                            this.scautoComplete.Add(this.subFundSelectionData.GetSubFundSelection.Rows[i][this.subFundSelectionData.GetSubFundSelection.SubFundColumn.ColumnName].ToString());
                        }
                    }

                    if (this.RollYearCombo.SelectedValue.ToString().Trim() != this.selectedRollYear.ToString().Trim())
                    {
                        DialogResult alertOnChange = MessageBox.Show("Do you want to discard the changes", "TerraScan T2", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (alertOnChange == DialogResult.Yes)
                        {
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                            this.ComboChange();
                        }
                        else
                        {
                            if (this.selectedRollYear > 0)
                            {
                                this.RollYearCombo.SelectedValue = this.selectedRollYear;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Comboes the change.
        /// </summary>
        private void ComboChange()
        {
            try
            {
                this.PopulateAutoFundTransferDataGridView();
                this.operationSmartPart.SaveButtonEnable = false;
                this.operationSmartPart.CancelButtonEnable = false;
                short.TryParse(this.RollYearCombo.SelectedValue.ToString(), out this.selectedRollYear);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Saves the button_ click.
        /// </summary>
        private void SaveButton_Click()
        {
            try
            {
                if (this.operationSmartPart.SaveButtonEnable == true)
                {
                    this.SaveAutoFundTransferDetails();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Cancels the button_ click.
        /// </summary>
        private void CancelButton_Click()
        {
            try
            {
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.PopulateAutoFundTransferDataGridView();
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                this.RollYearCombo.Focus();
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
                this.AutoFundTransferDataGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
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
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    ////this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    ////this.SetEditSaveRecord();                   
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the picBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PicBox1_Click(object sender, EventArgs e)
        {
            try
            {
                byte subFundValue;
                int tempcol = this.AutoFundTransferDataGridView.CurrentCell.ColumnIndex;
                int temprow = this.AutoFundTransferDataGridView.CurrentCell.RowIndex;

                if (byte.TryParse(this.AutoFundTransferDataGridView[this.autoFundData.ListAutoFundAccountTransferDetails.IsSubFundColumn.ColumnName, temprow].Value.ToString(), out subFundValue))
                {
                    if (subFundValue.Equals(0))
                    {
                        bool tempAccountStatus;
                        int accountId;
                        object[] optionalParameter = new object[] { this.RollYearCombo.SelectedValue, 0 };
                        Form accountSelectionForm = this.form1025Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1345, optionalParameter, this.form1025Controller.WorkItem);
                        if (accountSelectionForm != null)
                        {
                            if (accountSelectionForm.ShowDialog() == DialogResult.OK)
                            {
                                short rollYear;
                                int.TryParse(TerraScanCommon.GetValue(accountSelectionForm, "AccountId").ToString(), out accountId);
                                short.TryParse(TerraScanCommon.GetValue(accountSelectionForm, "RollYearValue"), out rollYear);
                                ExciseTaxRateData accountNameDataSet = new ExciseTaxRateData();
                                accountNameDataSet = this.form1025Controller.WorkItem.GetAccountName(accountId);
                                if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                                {
                                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                                    this.SetEditSaveRecord();
                                    tempAccountStatus = Convert.ToBoolean(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString());
                                    this.AutoFundTransferDataGridView[tempcol, temprow].Value = accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctColumn].ToString();
                                    this.AutoFundTransferDataGridView[this.IsDestinationAcctPending.Index, temprow].Value = tempAccountStatus;
                                    this.AutoFundTransferDataGridView[this.DestinationAccountID.Index, temprow].Value = accountId;
                                    this.AutoFundTransferDataGridView[this.DestinationRollyear.Index, temprow].Value = rollYear;
                                    if (tempAccountStatus)
                                    {
                                        this.AutoFundTransferDataGridView[tempcol, temprow].Style.BackColor = Color.FromArgb(187, 222, 173);
                                    }
                                    else
                                    {
                                        ////khaja made changes to fix Bug#5286
                                        ////this.AutoFundTransferDataGridView[e.ColumnIndex, temprow].Style.BackColor = this.AutoFundTransferDataGridView[e.ColumnIndex, temprow].Style.BackColor;                                            
                                        this.AutoFundTransferDataGridView[tempcol, temprow].Style.BackColor = this.AutoFundTransferDataGridView[this.SubFundColumn.Name, temprow].Style.BackColor;
                                    }

                                    TerraScanTextAndImageCell imgCell = (TerraScanTextAndImageCell)this.AutoFundTransferDataGridView[this.SubFundColumn.Name, temprow];
                                    imgCell.ReadOnly = true;
                                }

                                this.AutoFundTransferDataGridView.RefreshEdit();
                            }
                        }
                    }
                    else
                    {
                        Form subfundForm = new Form();
                        object[] optionalParameter = new object[] { this.RollYearCombo.SelectedValue, 1 };
                        subfundForm = this.form1025Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1515, optionalParameter, this.form1025Controller.WorkItem);
                        if (subfundForm != null)
                        {
                            if (subfundForm.ShowDialog() == DialogResult.OK)
                            {
                                string subFund = string.Empty;
                                subFund = TerraScanCommon.GetValue(subfundForm, "SubFundItem").ToString();
                                if (!string.IsNullOrEmpty(subFund.Trim()))
                                {
                                    short rollYear;
                                    short.TryParse(TerraScanCommon.GetValue(subfundForm, "RollYearValue"), out rollYear);
                                    this.subFundManagementData = this.form1025Controller.WorkItem.F9503_GetSubFundItems(subFund.Trim(), rollYear);
                                    if (this.subFundManagementData.getSubFundItems.Rows.Count > 0)
                                    {
                                        this.currentRowSourceKeyName = subFund.Trim();
                                        this.currentRowSourceKeyId = Convert.ToInt32(this.subFundManagementData.getSubFundItems.Rows[0]["SubFundID"]);
                                        this.AutoFundTransferDataGridView[tempcol, temprow].Value = subFund.Trim();
                                        this.AutoFundTransferDataGridView[this.SubFundID.Name, temprow].Value = Convert.ToInt32(this.subFundManagementData.getSubFundItems.Rows[0]["SubFundID"]);
                                        this.AutoFundTransferDataGridView[this.SourceSubFundID.Name, temprow].Value = Convert.ToInt32(this.subFundManagementData.getSubFundItems.Rows[0]["SubFundID"]);
                                        this.AutoFundTransferDataGridView[this.SourceRollyear.Name, temprow].Value = rollYear;
                                        this.AutoFundTransferDataGridView[this.IsSourceAcctPending.Name, temprow].Value = false;

                                        this.SetEditSaveRecord();
                                        this.AutoFundTransferDataGridView.RefreshEdit();
                                    }
                                }
                                else
                                {
                                    ////khaja made changes to fix Bug#4017
                                    this.AutoFundTransferDataGridView[tempcol, temprow].Value = string.Empty;
                                    this.AutoFundTransferDataGridView[this.SubFundID.Name, temprow].Value = 0;
                                    this.AutoFundTransferDataGridView[this.SourceSubFundID.Name, temprow].Value = 0;
                                    this.AutoFundTransferDataGridView.RefreshEdit();
                                }

                                TerraScanTextAndImageCell imgCell = (TerraScanTextAndImageCell)this.AutoFundTransferDataGridView[this.SubFundColumn.Name, temprow];
                                imgCell.ReadOnly = false;
                                this.AutoFundTransferDataGridView.RefreshEdit();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the HelpLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void HelpLinkLabel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Tag.ToString()))
                {
                    HelpEngine.Show(ParentForm.Text, this.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the RollYearCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RollYearCombo_Leave(object sender, EventArgs e)
        {
            try
            {
                this.AutoFundTransferDataGridView.Select();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Events

        #region GridView Events

        /// <summary>
        /// Handles the CellMouseClick event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void AutoFundTransferDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if ((e.ColumnIndex.Equals(this.SubFundColumn.Index)) && (e.RowIndex >= 0) && (e.ColumnIndex >= 0))
                {
                    if (!this.AutoFundTransferDataGridView.Rows[e.RowIndex].ReadOnly)
                    {
                        if ((e.X >= 170) && (e.X <= 186) && (e.Y >= 1) && (e.Y <= 21))
                        {
                            byte subFundValue;
                            if (byte.TryParse(this.AutoFundTransferDataGridView[this.autoFundData.ListAutoFundAccountTransferDetails.IsSubFundColumn.ColumnName, e.RowIndex].Value.ToString(), out subFundValue))
                            {
                                if (subFundValue.Equals(0))
                                {
                                    this.OpenAccountSelectionForm(e);
                                }
                                else
                                {
                                    this.OpenSubFundSelectionForm(e);
                                }
                            }
                        }
                    }
                }

                if ((e.ColumnIndex.Equals(this.AccountNameColumn.Index)) && (e.RowIndex >= 0) && (e.ColumnIndex >= 0))
                {
                    if (!this.AutoFundTransferDataGridView.Rows[e.RowIndex].ReadOnly)
                    {
                        if ((e.X >= 240) && (e.X <= (281 - 16)) && (e.Y >= 3) && (e.Y <= (18 - 3)))
                        {
                            this.OpenAccountSelectionForm(e);
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
        /// Handles the RowHeaderMouseClick event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void AutoFundTransferDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.AutoFundTransferDataGridView.CurrentCell != null)
                {
                    if (!Convert.ToBoolean(this.autoFundData.ListAutoFundAccountTransferDetails.Rows[e.RowIndex][this.AutoFundTransferDataGridView.EmptyRecordColumnName]))
                    {
                        this.selectedFundRowId = e.RowIndex;
                        if (!string.IsNullOrEmpty(this.autoFundData.ListAutoFundAccountTransferDetails.Rows[e.RowIndex][this.autoFundData.ListAutoFundAccountTransferDetails.AutoTransferIDColumn].ToString()))
                        {
                            this.autoTransferId = Convert.ToInt32(this.autoFundData.ListAutoFundAccountTransferDetails.Rows[e.RowIndex][this.autoFundData.ListAutoFundAccountTransferDetails.AutoTransferIDColumn]);
                            this.footerSmartPart.KeyId = this.autoTransferId;
                            this.additionalOperationSmartPart.KeyId = this.autoTransferId;
                        }
                        else
                        {
                            this.autoTransferId = 0;
                            this.selectedFundRowId = e.RowIndex;
                        }
                    }
                    else
                    {
                        this.autoTransferId = -99;
                        this.selectedFundRowId = e.RowIndex;
                    }
                }
                else
                {
                    this.selectedFundRowId = -99;
                }

                TerraScanCommon.SetDataGridViewPosition(this.AutoFundTransferDataGridView, e.RowIndex);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void AutoFundTransferDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int tempId = 0;

                if (e.KeyValue == 46 && this.autoTransferId != -99)
                {
                    if ((this.AutoFundTransferDataGridView.CurrentCell.RowIndex == this.selectedFundRowId) && ((this.AutoFundTransferDataGridView[this.AutoTransferIDColumn.Index, this.AutoFundTransferDataGridView.CurrentCell.RowIndex].Value.ToString() == this.autoTransferId.ToString()) || (this.autoTransferId == 0)))
                    {
                        this.rowDeleted = true;
                        try
                        {
                            tempId = this.autoTransferId;
                            ////this.autoFundData.ListAutoFundAccountTransferDetails.Rows[this.selectedFundRowId].Delete();
                            this.autoFundData.ListAutoFundAccountTransferDetails.Rows[this.selectedFundRowId].Delete();
                            this.autoFundData.ListAutoFundAccountTransferDetails.AcceptChanges();
                            ////this.AutoFundTransferDataGridView.RefreshEdit();                                                    
                        }
                        catch (Exception e2)
                        {
                        }

                        ////this.AutoFundTransferDataGridView.Rows.RemoveAt(this.selectedFundRowId);
                        ////this.autoFundData.ListAutoFundAccountTransferDetails.AcceptChanges();

                        ////khaja added code to fix Bug#4550                        
                        if (this.autoFundData.ListAutoFundAccountTransferDetails.Rows.Count < this.AutoFundTransferDataGridView.NumRowsVisible)
                        {
                            this.autoFundData.ListAutoFundAccountTransferDetails.AddListAutoFundAccountTransferDetailsRow(this.autoFundData.ListAutoFundAccountTransferDetails.NewListAutoFundAccountTransferDetailsRow());
                            this.autoFundData.ListAutoFundAccountTransferDetails.Rows[this.autoFundData.ListAutoFundAccountTransferDetails.Rows.Count - 1][this.AutoFundTransferDataGridView.EmptyRecordColumnName] = true;
                        }

                        if (this.autoFundData.ListAutoFundAccountTransferDetails.Rows.Count > this.AutoFundTransferDataGridView.NumRowsVisible)
                        {
                            this.AutoFundGridVscrollBar.Visible = false;
                        }
                        else
                        {
                            this.AutoFundGridVscrollBar.Visible = true;
                        }

                        if (tempId != 0)
                        {
                            this.form1025Controller.WorkItem.F1025_DeleteAutoFundTransferDetails(tempId, TerraScanCommon.UserId);
                        }
                        ////code added to fix the bug #3916 - > to disable save & cancel buttons
                        ////khaja Dt.18-Dec-08
                        this.selectedFundRowId = -99;
                    }
                }
            }
            catch (SoapException ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.Display, this.ParentForm);
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
        /// Handles the RowEnter event of the AutoFundTransferDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AutoFundTransferDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && !this.rowDeleted)
                {
                    if (e.RowIndex.Equals(0))
                    {
                        this.MakeAutoFundTransferGridReadOnlyColumns(e);
                        this.SetAttachmentCommentsCount();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(this.AutoFundTransferDataGridView[this.IsSubFund.Index, e.RowIndex - 1].Value.ToString().Trim()) &&
                            string.IsNullOrEmpty(this.AutoFundTransferDataGridView[this.SubFundColumn.Index, e.RowIndex - 1].Value.ToString().Trim()) &&
                            string.IsNullOrEmpty(this.AutoFundTransferDataGridView[this.AccountNameColumn.Index, e.RowIndex - 1].Value.ToString().Trim()) &&
                            string.IsNullOrEmpty(this.AutoFundTransferDataGridView[this.TransaferRate.Index, e.RowIndex - 1].Value.ToString().Trim()))
                        {
                            this.AutoFundTransferDataGridView.Rows[e.RowIndex].ReadOnly = true;
                        }
                        else
                        {
                            this.MakeAutoFundTransferGridReadOnlyColumns(e);
                        }
                    }

                    if (!string.IsNullOrEmpty(this.AutoFundTransferDataGridView[this.AutoTransferIDColumn.Index, e.RowIndex].Value.ToString()))
                    {
                        this.footerSmartPart.KeyId = Convert.ToInt32(this.AutoFundTransferDataGridView[this.AutoTransferIDColumn.Index, e.RowIndex].Value.ToString());
                        this.CommentsdeckWorkspace.Enabled = true;
                        this.additionalOperationSmartPart.KeyId = this.autoTransferId;
                    }
                    else
                    {
                        this.footerSmartPart.KeyId = null;
                        this.CommentsdeckWorkspace.Enabled = false;
                    }

                    if (!string.IsNullOrEmpty(this.AutoFundTransferDataGridView[this.AutoTransferIDColumn.Index, e.RowIndex].Value.ToString().Trim()))
                    {
                        this.autoTransferId = Convert.ToInt32(this.autoFundData.ListAutoFundAccountTransferDetails.Rows[e.RowIndex][this.autoFundData.ListAutoFundAccountTransferDetails.AutoTransferIDColumn]);
                    }

                    this.SetAttachmentCommentsCount();
                }

                for (int i = 0; i < this.AutoFundTransferDataGridView.Rows.Count; i++)
                {
                    TerraScanTextAndImageCell folderCell = (TerraScanTextAndImageCell)this.AutoFundTransferDataGridView[this.SubFundColumn.Index, i];
                    TerraScanTextAndImageCell imgCell = (TerraScanTextAndImageCell)this.AutoFundTransferDataGridView[this.AccountNameColumn.Index, i];
                    imgCell.ImagexLocation = 250;
                    imgCell.ImageyLocation = 3;
                    folderCell.ImagexLocation = 170;

                    if (e.RowIndex == i)
                    {
                        byte tempIsSubFundValue;
                        if (byte.TryParse(this.AutoFundTransferDataGridView[this.IsSubFund.Index, e.RowIndex].Value.ToString(), out tempIsSubFundValue))
                        {
                            if (tempIsSubFundValue.Equals(0))
                            {
                                folderCell.ImageyLocation = 3;
                                folderCell.Image = Properties.Resources.Abutton;
                            }
                            else
                            {
                                folderCell.ImageyLocation = -2;
                                folderCell.Image = Properties.Resources.FilePathImage;
                            }
                        }
                        else
                        {
                            folderCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.AutoFundTransferDataGridView[this.IsSubFund.Index, e.RowIndex].InheritedStyle.BackColor.R), Convert.ToInt32(this.AutoFundTransferDataGridView[this.IsSubFund.Index, e.RowIndex].InheritedStyle.BackColor.G), Convert.ToInt32(this.AutoFundTransferDataGridView[this.IsSubFund.Index, e.RowIndex].InheritedStyle.BackColor.B));
                        }

                        imgCell.Image = Properties.Resources.Abutton;
                    }
                    else
                    {
                        if (e.RowIndex >= 0)
                        {
                            try
                            {
                                imgCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.AutoFundTransferDataGridView[this.IsSubFund.Index, e.RowIndex].InheritedStyle.BackColor.R), Convert.ToInt32(this.AutoFundTransferDataGridView[this.IsSubFund.Index, e.RowIndex].InheritedStyle.BackColor.G), Convert.ToInt32(this.AutoFundTransferDataGridView[this.IsSubFund.Index, e.RowIndex].InheritedStyle.BackColor.B));
                                folderCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.AutoFundTransferDataGridView[this.IsSubFund.Index, e.RowIndex].InheritedStyle.BackColor.R), Convert.ToInt32(this.AutoFundTransferDataGridView[this.IsSubFund.Index, e.RowIndex].InheritedStyle.BackColor.G), Convert.ToInt32(this.AutoFundTransferDataGridView[this.IsSubFund.Index, e.RowIndex].InheritedStyle.BackColor.B));
                            }
                            catch
                            {
                            }
                        }
                    }
                }

                this.AutoFundTransferDataGridView.Refresh();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Makes the auto fund transfer grid read only columns.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void MakeAutoFundTransferGridReadOnlyColumns(DataGridViewCellEventArgs e)
        {
            if (byte.TryParse(this.AutoFundTransferDataGridView[this.IsSubFund.Index, e.RowIndex].Value.ToString(), out this.currentRowIsSubFundValue))
            {
                int.TryParse(this.AutoFundTransferDataGridView[this.SourceSubFundID.Index, e.RowIndex].Value.ToString(), out this.currentRowSourceKeyId);

                if (!string.IsNullOrEmpty(this.AutoFundTransferDataGridView[this.SubFundColumn.Index, e.RowIndex].Value.ToString()))
                {
                    this.currentRowSourceKeyName = this.AutoFundTransferDataGridView[this.SubFundColumn.Index, e.RowIndex].Value.ToString();
                }

                if (this.currentRowIsSubFundValue.Equals(0))
                {
                    this.AutoFundTransferDataGridView[this.IsSubFund.Index, e.RowIndex].ReadOnly = false;
                    this.AutoFundTransferDataGridView[this.SubFundColumn.Index, e.RowIndex].ReadOnly = true;
                    this.AutoFundTransferDataGridView[this.TransaferRate.Index, e.RowIndex].ReadOnly = false;
                }
                else
                {
                    this.AutoFundTransferDataGridView[this.IsSubFund.Index, e.RowIndex].ReadOnly = false;
                    this.AutoFundTransferDataGridView[this.SubFundColumn.Index, e.RowIndex].ReadOnly = false;
                    this.AutoFundTransferDataGridView[this.TransaferRate.Index, e.RowIndex].ReadOnly = false;
                }
            }
            else
            {
                this.currentRowSourceKeyId = 0;
                this.currentRowSourceKeyName = string.Empty;
                this.AutoFundTransferDataGridView[this.IsSubFund.Index, e.RowIndex].ReadOnly = false;
                this.AutoFundTransferDataGridView[this.SubFundColumn.Index, e.RowIndex].ReadOnly = true;
                this.AutoFundTransferDataGridView[this.TransaferRate.Index, e.RowIndex].ReadOnly = false;
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the AutoFundTransferDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void AutoFundTransferDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.AutoFundTransferDataGridView.Rows.Count > 0)
                {
                    this.AutoFundTransferDataGridView.Rows[0].Selected = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the AutoFundTransferDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void AutoFundTransferDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                e.Control.Validated += new EventHandler(this.Control_Validated);
                e.Control.LocationChanged += new EventHandler(this.Control_LocationChanged);

                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    ((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(this.AutoFundTransferDataGridView_SourceSelectionChangeCommitted);
                }

                if (this.AutoFundTransferDataGridView.CurrentCell.ColumnIndex.Equals(this.SubFundColumn.Index))
                {
                    TextBox txt = e.Control as TextBox;
                    txt.AutoCompleteCustomSource = this.scautoComplete;
                    txt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    txt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
                else if (!this.AutoFundTransferDataGridView.CurrentCell.ColumnIndex.Equals(this.IsSubFund.Index))
                {
                    TextBox txt = e.Control as TextBox;
                    txt.AutoCompleteCustomSource = this.scautoComplete;
                    txt.AutoCompleteMode = AutoCompleteMode.None;
                    txt.AutoCompleteSource = AutoCompleteSource.CustomSource;
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
        /// Handles the LocationChanged event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Control_LocationChanged(object sender, EventArgs e)
        {
            this.tempcol = this.AutoFundTransferDataGridView.CurrentCell.ColumnIndex;
            if (this.tempcol.Equals(this.SubFundColumn.Index) && this.tempcol != this.TransaferRate.Index)
            {
                Panel p1 = ((sender as TextBox).Parent as Panel);
                PictureBox picBox1 = new PictureBox();
                byte subFundValue;
                int currentRowIndex;
                int currentColumnIndex;
                currentRowIndex = this.AutoFundTransferDataGridView.CurrentCell.RowIndex;
                currentColumnIndex = this.AutoFundTransferDataGridView.CurrentCell.ColumnIndex;

                if (byte.TryParse(this.AutoFundTransferDataGridView[this.IsSubFund.Index, currentRowIndex].Value.ToString(), out subFundValue))
                {
                    if (subFundValue.Equals(0))
                    {
                        picBox1.Image = Properties.Resources.Abutton;
                        picBox1.Location = new System.Drawing.Point(170, 3);
                    }
                    else
                    {
                        picBox1.Image = Properties.Resources.FilePathImage;
                        picBox1.Location = new System.Drawing.Point(170, -2);
                    }
                }
                else
                {
                    picBox1.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.AutoFundTransferDataGridView[this.IsSubFund.Index, currentRowIndex].InheritedStyle.BackColor.R), Convert.ToInt32(this.AutoFundTransferDataGridView[this.IsSubFund.Index, currentRowIndex].InheritedStyle.BackColor.G), Convert.ToInt32(this.AutoFundTransferDataGridView[this.IsSubFund.Index, currentRowIndex].InheritedStyle.BackColor.B));
                    picBox1.Location = new System.Drawing.Point(170, 3);
                }

                picBox1.Height = 30;
                picBox1.Width = 23;
                p1.Controls.Add(picBox1);
                picBox1.BringToFront();
                picBox1.Click += new EventHandler(this.PicBox1_Click);
                this.AutoFundTransferDataGridView.RefreshEdit();
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event of the AutoFundTransferDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AutoFundTransferDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.autoFundData.ListAutoFundAccountTransferDetails.Rows.Count == e.RowIndex + 1 && !this.rowDeleted)
                {
                    ////khaja added code to fix Bug#4550
                    if (!string.IsNullOrEmpty(this.AutoFundTransferDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString().Trim()))
                    {
                        this.autoFundData.ListAutoFundAccountTransferDetails.AddListAutoFundAccountTransferDetailsRow(this.autoFundData.ListAutoFundAccountTransferDetails.NewListAutoFundAccountTransferDetailsRow());
                        this.autoFundData.ListAutoFundAccountTransferDetails.Rows[this.autoFundData.ListAutoFundAccountTransferDetails.Rows.Count - 1][this.AutoFundTransferDataGridView.EmptyRecordColumnName] = true;
                        ////this.autoFundData.ListAutoFundAccountTransferDetails.AcceptChanges();
                        this.newRecord = true;
                        this.BindSourceToAutoFundGridView();
                        this.newRecord = false;
                    }
                }

                this.rowDeleted = false;

                if ((e.RowIndex >= 0) && (e.ColumnIndex.Equals(this.SubFundColumn.Index)))
                {
                    if (string.IsNullOrEmpty(this.AutoFundTransferDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString().Trim()))
                    {
                        this.AutoFundTransferDataGridView[e.ColumnIndex, e.RowIndex].Value = string.Empty;
                    }
                    else
                    {
                        if (!string.Equals(this.AutoFundTransferDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString(), this.currentRowSourceKeyName))
                        {
                            this.subFundManagementData = this.form1025Controller.WorkItem.F9503_GetSubFundItems(this.AutoFundTransferDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString().Trim(), this.selectedRollYear);

                            if (this.subFundManagementData.getSubFundItems.Rows.Count > 0)
                            {
                                this.AutoFundTransferDataGridView[this.SubFundColumn.Name, e.RowIndex].Value = this.AutoFundTransferDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString().Trim();
                                this.AutoFundTransferDataGridView[this.SubFundID.Name, e.RowIndex].Value = Convert.ToInt32(this.subFundManagementData.getSubFundItems.Rows[0]["SubFundID"]);
                                this.AutoFundTransferDataGridView[this.SourceSubFundID.Name, e.RowIndex].Value = Convert.ToInt32(this.subFundManagementData.getSubFundItems.Rows[0]["SubFundID"]);
                                this.AutoFundTransferDataGridView[this.SourceRollyear.Name, e.RowIndex].Value = this.selectedRollYear;
                                this.AutoFundTransferDataGridView[this.IsSourceAcctPending.Name, e.RowIndex].Value = false;
                                this.SetEditSaveRecord();
                                this.AutoFundTransferDataGridView.RefreshEdit();
                            }
                            else
                            {
                                this.AutoFundTransferDataGridView[this.SubFundColumn.Name, e.RowIndex].Value = string.Empty;
                                this.AutoFundTransferDataGridView[this.SubFundID.Name, e.RowIndex].Value = 0;
                                this.AutoFundTransferDataGridView[this.SourceSubFundID.Name, e.RowIndex].Value = 0;
                                this.AutoFundTransferDataGridView[this.SourceRollyear.Name, e.RowIndex].Value = this.selectedRollYear;
                                this.AutoFundTransferDataGridView[this.IsSourceAcctPending.Name, e.RowIndex].Value = false;
                                this.currentRowSourceKeyName = this.AutoFundTransferDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString().Trim();
                                this.SetEditSaveRecord();
                                this.AutoFundTransferDataGridView.RefreshEdit();
                            }
                        }
                    }
                }

                if (e.ColumnIndex.Equals(this.TransaferRate.Index))
                {
                    if (this.AutoFundTransferDataGridView[this.TransaferRate.Index, e.RowIndex].Value != null && !string.IsNullOrEmpty(this.AutoFundTransferDataGridView[this.TransaferRate.Index, e.RowIndex].Value.ToString()))
                    {
                        Decimal defaultValue = 0;
                        try
                        {
                            defaultValue = (Decimal)this.AutoFundTransferDataGridView[this.TransaferRate.Index, e.RowIndex].Value;
                        }
                        catch (InvalidCastException invCastExp)
                        {
                            defaultValue = 1;
                        }
                        catch
                        {
                            defaultValue = 0;
                        }

                        if ((defaultValue > 0) && (defaultValue <= 100))
                        {
                            this.AutoFundTransferDataGridView[this.TransaferRate.Index, e.RowIndex].Value = defaultValue;
                        }
                        else
                        {
                            this.AutoFundTransferDataGridView[this.TransaferRate.Index, e.RowIndex].Value = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the AutoFundTransferDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AutoFundTransferDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                    {
                        if (!this.formLoad)
                        {
                            if (e.ColumnIndex != this.SubFundColumn.Index)
                            {
                                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                                this.SetEditSaveRecord();
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(this.AutoFundTransferDataGridView[this.SubFundColumn.Index, e.RowIndex].Value.ToString().Trim()))
                                {
                                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                                    this.SetEditSaveRecord();
                                }
                                else
                                {
                                    if (e.RowIndex + 1 < this.AutoFundTransferDataGridView.Rows.Count)
                                    {
                                        ////khaja made changes to Enable Smart Controls.
                                        ////if ((!string.IsNullOrEmpty(this.AutoFundTransferDataGridView[0, e.RowIndex + 1].Value.ToString()) || !string.IsNullOrEmpty(this.AutoFundTransferDataGridView[6, e.RowIndex + 1].Value.ToString())) && string.IsNullOrEmpty(this.AutoFundTransferDataGridView[0, e.RowIndex].Value.ToString()))
                                        if ((!string.IsNullOrEmpty(this.AutoFundTransferDataGridView[this.SubFundColumn.Index, e.RowIndex + 1].Value.ToString()) || !string.IsNullOrEmpty(this.AutoFundTransferDataGridView[this.SubFundID.Index, e.RowIndex].Value.ToString())) && string.IsNullOrEmpty(this.AutoFundTransferDataGridView[this.SubFundColumn.Index, e.RowIndex].Value.ToString()))
                                        {
                                            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                                            this.SetEditSaveRecord();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the AutoFundTransferDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void AutoFundTransferDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                Decimal outDecimal;

                if (e.RowIndex >= 0)
                {
                    //// Only paint if desired, formattable column
                    if (e.ColumnIndex.Equals(this.TransaferRate.Index))
                    {
                        //// Only paint if text provided, Only paint if desired text is in cell 

                        if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                        {
                            string val = e.Value.ToString();
                            if (Decimal.TryParse(val, out outDecimal))
                            {
                                if (outDecimal.ToString().Contains("-"))
                                {
                                    e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.0000"), ")");
                                    e.CellStyle.ForeColor = Color.Green;
                                }
                                else
                                {
                                    e.Value = outDecimal.ToString("#,##0.0000");
                                }

                                e.FormattingApplied = true;
                            }
                            else
                            {
                                e.Value = "0.0000";
                            }
                        }
                        else
                        {
                            e.Value = string.Empty;
                        }
                    }

                    if (e.ColumnIndex.Equals(this.AccountNameColumn.Index))
                    {
                        //// Only paint if text provided, Only paint if desired text is in cell 
                        if (!string.IsNullOrEmpty(this.AutoFundTransferDataGridView[this.IsDestinationAcctPending.Name, e.RowIndex].Value.ToString()))
                        {
                            if (Convert.ToBoolean(this.AutoFundTransferDataGridView[this.IsDestinationAcctPending.Name, e.RowIndex].Value))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(187, 222, 173);
                            }

                            e.FormattingApplied = true;
                        }
                    }

                    if (e.ColumnIndex.Equals(this.SubFundColumn.Index))
                    {
                        this.tempcol = e.ColumnIndex;
                        //// Only paint if text provided, Only paint if desired text is in cell 
                        if (!string.IsNullOrEmpty(this.AutoFundTransferDataGridView[this.IsSourceAcctPending.Name, e.RowIndex].Value.ToString()))
                        {
                            if (Convert.ToBoolean(this.AutoFundTransferDataGridView[this.IsSourceAcctPending.Name, e.RowIndex].Value))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(187, 222, 173);
                            }

                            e.FormattingApplied = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellParsing event of the AutoFundTransferDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void AutoFundTransferDataGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                Decimal outDecimal;

                //// Only paint if desired column
                if (e.ColumnIndex.Equals(this.TransaferRate.Index))
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    //// Only paint if text provided, Only paint if desired text is in cell
                    if (e.Value != null)
                    {
                        string tempvalue = e.Value.ToString().Trim();

                        if (tempvalue.EndsWith("."))
                        {
                            tempvalue = string.Concat(tempvalue, "0");
                        }

                        if (Decimal.TryParse(tempvalue, NumberStyles.Currency, null, out outDecimal))
                        {
                            tempvalue = outDecimal.ToString();
                        }

                        e.Value = outDecimal;
                        e.ParsingApplied = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CropCodeSelectionChangeCommitted event of the F36041 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AutoFundTransferDataGridView_SourceSelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                ComboBox combo = (ComboBox)sender;
                int currentRowIndex;
                int currentColumnIndex;
                byte subFundValue;

                if (this.AutoFundTransferDataGridView.CurrentCell != null)
                {
                    currentRowIndex = this.AutoFundTransferDataGridView.CurrentCell.RowIndex;
                    currentColumnIndex = this.AutoFundTransferDataGridView.CurrentCell.ColumnIndex;
                    if (currentRowIndex >= 0 && currentColumnIndex.Equals(this.IsSubFund.Index))
                    {
                        TerraScanTextAndImageCell imgCell = (TerraScanTextAndImageCell)this.AutoFundTransferDataGridView[this.SubFundColumn.Index, currentRowIndex];
                        if (byte.TryParse(combo.SelectedValue.ToString(), out subFundValue))
                        {
                            if (!subFundValue.Equals(this.currentRowIsSubFundValue) && !string.IsNullOrEmpty(this.currentRowSourceKeyName) && this.currentRowSourceKeyId > 0)
                            {
                                this.currentRowSourceKeyId = 0;
                                this.currentRowSourceKeyName = string.Empty;
                                this.AutoFundTransferDataGridView[this.SubFundColumn.Index, currentRowIndex].Value = string.Empty;
                                this.AutoFundTransferDataGridView[this.SubFundID.Index, currentRowIndex].Value = 0;
                                this.AutoFundTransferDataGridView[this.SourceSubFundID.Index, currentRowIndex].Value = 0;
                                this.AutoFundTransferDataGridView[this.IsSourceAcctPending.Index, currentRowIndex].Value = false;
                                this.AutoFundTransferDataGridView[this.SubFundColumn.Index, currentRowIndex].Style.BackColor = this.AutoFundTransferDataGridView[this.TransaferRate.Index, currentRowIndex].Style.BackColor;
                            }
                        }
                        else
                        {
                            this.currentRowSourceKeyId = 0;
                            this.currentRowSourceKeyName = string.Empty;
                            this.AutoFundTransferDataGridView[this.SubFundColumn.Index, currentRowIndex].Value = string.Empty;
                            this.AutoFundTransferDataGridView[this.SubFundID.Index, currentRowIndex].Value = 0;
                            this.AutoFundTransferDataGridView[this.SourceSubFundID.Index, currentRowIndex].Value = 0;
                            this.AutoFundTransferDataGridView[this.IsSourceAcctPending.Index, currentRowIndex].Value = false;
                            this.AutoFundTransferDataGridView[this.SubFundColumn.Index, currentRowIndex].Style.BackColor = this.AutoFundTransferDataGridView[this.TransaferRate.Index, currentRowIndex].Style.BackColor;
                        }

                        if (!string.IsNullOrEmpty(combo.Text.Trim()))
                        {
                            if (combo.Text.Equals(SharedFunctions.GetResourceString("F1025Account")))
                            {
                                imgCell.ImageyLocation = 3;
                                imgCell.Image = Properties.Resources.Abutton;
                                this.AutoFundTransferDataGridView[this.SubFundColumn.Index, currentRowIndex].ReadOnly = true;
                            }
                            else if (combo.Text.Equals(SharedFunctions.GetResourceString("F1025SubFund")))
                            {
                                imgCell.ImageyLocation = -2;
                                imgCell.Image = Properties.Resources.FilePathImage;
                                this.AutoFundTransferDataGridView[this.SubFundColumn.Index, currentRowIndex].ReadOnly = false;
                            }
                        }
                        else
                        {
                            this.AutoFundTransferDataGridView[this.SubFundColumn.Index, currentRowIndex].ReadOnly = true;
                            imgCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.AutoFundTransferDataGridView[this.IsSubFund.Index, currentRowIndex].InheritedStyle.BackColor.R), Convert.ToInt32(this.AutoFundTransferDataGridView[this.IsSubFund.Index, currentRowIndex].InheritedStyle.BackColor.G), Convert.ToInt32(this.AutoFundTransferDataGridView[this.IsSubFund.Index, currentRowIndex].InheritedStyle.BackColor.B));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion GridView Events

        #region Private Methods

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            //// Load ExciseTaxActionButtons SmartPart into ExciseTaxActionButtonsdeckWorkspace
            if (this.form1025Controller.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
            {
                this.OperationSmartpartDeckWorkSpace.Show(this.form1025Controller.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart));
            }
            else
            {
                this.OperationSmartpartDeckWorkSpace.Show(this.form1025Controller.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart));
            }

            this.operationSmartPart = (OperationSmartPart)this.form1025Controller.WorkItem.SmartParts[SmartPartNames.OperationSmartPart];
            this.operationSmartPart.NewButtonVisible = false;
            this.operationSmartPart.DeleteButtonVisible = false;
            //// To Load FormHeaderSmartPart into formHeaderSmartPartdeckWorkspace
            if (this.form1025Controller.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1025Controller.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1025Controller.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            string[] formLabelInfo = new string[2];
            formLabelInfo[0] = "Automatic Fund Transfers"; ////Properties.Resources.FormName;
            formLabelInfo[1] = string.Empty;

            this.SetFormHeader(this, new DataEventArgs<string[]>(formLabelInfo));

            if (this.form1025Controller.WorkItem.SmartParts.Contains(SmartPartNames.ReportActionSmartPart))
            {
                this.ReportActionDeckWorkspace.Show(this.form1025Controller.WorkItem.SmartParts.Get(SmartPartNames.ReportActionSmartPart));
            }
            else
            {
                this.ReportActionDeckWorkspace.Show(this.form1025Controller.WorkItem.SmartParts.AddNew<ReportActionSmartPart>(SmartPartNames.ReportActionSmartPart));
            }

            this.reportActionSmartPart = (ReportActionSmartPart)this.form1025Controller.WorkItem.SmartParts[SmartPartNames.ReportActionSmartPart];

            this.reportActionSmartPart.PreviewButtonVisible = true;
            this.reportActionSmartPart.EmailButtonVisible = false;
            this.reportActionSmartPart.PrintButtonVisible = false;

            //// To Load AdditionalOperationSmartPart into CommentsdeckWorkspace
            if (this.form1025Controller.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.CommentsdeckWorkspace.Show(this.form1025Controller.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart));
            }
            else
            {
                this.CommentsdeckWorkspace.Show(this.form1025Controller.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart));
            }

            this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.form1025Controller.WorkItem.SmartParts[SmartPartNames.AdditionalOperationSmartPart];
            this.additionalOperationSmartPart.ParentWorkItem = this.form1025Controller.WorkItem;
            this.additionalOperationSmartPart.ParentFormId = this.ParentFormId;
            this.additionalOperationSmartPart.CurrntFormId = this.ParentFormId;

            //// To Load FooterSmartPart into FooterWorkspace
            ////this.footerSmartPart.KeyId = this.autoTransferId;
            if (this.form1025Controller.WorkItem.SmartParts.Contains(SmartPartNames.FooterSmartPart))
            {
                this.FooterWorkspace.Show(this.form1025Controller.WorkItem.SmartParts.Get(SmartPartNames.FooterSmartPart));
            }
            else
            {
                this.FooterWorkspace.Show(this.form1025Controller.WorkItem.SmartParts.AddNew<FooterSmartPart>(SmartPartNames.FooterSmartPart));
            }

            this.footerSmartPart = (FooterSmartPart)this.form1025Controller.WorkItem.SmartParts[SmartPartNames.FooterSmartPart];
            this.footerSmartPart.ParentWorkItem = this.form1025Controller.WorkItem;
            this.footerSmartPart.FormId = this.ParentFormId.ToString();
            this.footerSmartPart.AuditLinkText = SharedFunctions.GetResourceString("tTR_AutoTransfer [AutoTransferID] ");
            this.footerSmartPart.VisibleHelpButton = false;
            this.footerSmartPart.VisibleHelpLinkButton = true;
            this.footerSmartPart.TabStop = true;
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditSaveRecord()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.PermissionFiled.editPermission)
            {
                this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                this.AutoFundTransferDataGridView.TabStop = false;
            }
        }

        /// <summary>
        /// Populates the auto fund transfer data grid view.
        /// </summary>
        private void PopulateAutoFundTransferDataGridView()
        {
            int rolYearValue = 0;
            this.autoFundData.ListAutoFundAccountTransferDetails.Rows.Clear();
            int.TryParse(this.RollYearCombo.Text, out rolYearValue);
            DataTable tempRollYear = new DataTable();
            tempRollYear.Merge(this.autoFundData.ListAutoFundRollYear);
            this.autoFundData = this.form1025Controller.WorkItem.F1025_ListAutoFundTransferDetails(rolYearValue);
            this.BindSourceToAutoFundGridView();

            ////khaja added code to fix bug#4550
            if (this.AutoFundTransferDataGridView.OriginalRowCount >= this.AutoFundTransferDataGridView.NumRowsVisible)
            {
                this.autoFundData.ListAutoFundAccountTransferDetails.AddListAutoFundAccountTransferDetailsRow(this.autoFundData.ListAutoFundAccountTransferDetails.NewListAutoFundAccountTransferDetailsRow());
                this.autoFundData.ListAutoFundAccountTransferDetails.Rows[this.autoFundData.ListAutoFundAccountTransferDetails.Rows.Count - 1][this.AutoFundTransferDataGridView.EmptyRecordColumnName] = true;
            }

            if (this.autoFundData.ListAutoFundAccountTransferDetails.Rows.Count > this.AutoFundTransferDataGridView.NumRowsVisible)
            {
                this.AutoFundGridVscrollBar.Visible = false;
            }
            else
            {
                this.AutoFundGridVscrollBar.Visible = true;
            }

            this.autoFundData.ListAutoFundRollYear.Merge(tempRollYear);
        }

        /// <summary>
        /// Binds the souce to auto fund grid view.
        /// </summary>
        private void BindSourceToAutoFundGridView()
        {
            this.formLoad = true;
            this.autoFundRecordCount = this.autoFundData.ListAutoFundAccountTransferDetails.Rows.Count;
            if (this.autoFundData.ListAutoFundAccountTransferDetails != null)
            {
                this.AutoFundTransferDataGridView.DataSource = this.autoFundData.ListAutoFundAccountTransferDetails;
            }
            else
            {
                this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                this.footerSmartPart.FormId = null;
                this.reportActionSmartPart.Enabled = false;
                this.CommentsdeckWorkspace.Enabled = false;
            }

            if (this.autoFundRecordCount > 0)
            {
                if (!this.newRecord)
                {
                    this.reportActionSmartPart.Enabled = true;
                    this.CommentsdeckWorkspace.Enabled = true;
                }

                if (!string.IsNullOrEmpty(this.autoFundData.ListAutoFundAccountTransferDetails.Rows[0][this.autoFundData.ListAutoFundAccountTransferDetails.AutoTransferIDColumn].ToString()))
                {
                    this.autoTransferId = Convert.ToInt32(this.autoFundData.ListAutoFundAccountTransferDetails.Rows[0][this.autoFundData.ListAutoFundAccountTransferDetails.AutoTransferIDColumn]);
                    this.additionalOperationSmartPart.KeyId = this.autoTransferId;
                }
                else
                {
                    this.autoTransferId = -99;
                    this.selectedFundRowId = -99;
                }

                this.SetAttachmentCommentsCount();

                ////Auto coumplete code added by Sriparameswari
                this.scautoComplete.Clear();
                //// code comented by khaja to Fix Bug #3961
                ////for (int rowIndex = 0; rowIndex < this.autoFundData.ListAutoFundAccountTransferDetails.Rows.Count; rowIndex++)
                ////{ ///checkit
                string subFundValue = string.Empty;
                string descriptionValue = string.Empty;
                this.rollYearValue = Convert.ToInt32(this.RollYearCombo.SelectedValue);

                this.subFundSelectionData = this.form1025Controller.WorkItem.F1515_GetSubFundSelection(subFundValue, descriptionValue, this.rollYearValue, 1);
                if (this.subFundSelectionData.GetSubFundSelection.Rows.Count > 0)
                {
                    for (int i = 0; i <= this.subFundSelectionData.GetSubFundSelection.Rows.Count - 1; i++)
                    {
                        this.scautoComplete.Add(this.subFundSelectionData.GetSubFundSelection.Rows[i][this.subFundSelectionData.GetSubFundSelection.SubFundColumn.ColumnName].ToString());
                    }
                }
                ////}

                if (this.autoFundData.ListAutoFundAccountTransferDetails.Rows.Count > this.AutoFundTransferDataGridView.NumRowsVisible)
                {
                    this.AutoFundGridVscrollBar.Visible = false;
                }
                else
                {
                    this.AutoFundGridVscrollBar.Visible = true;
                }

                if (this.autoFundRecordCount > 0)
                {
                    this.AutoFundTransferDataGridView.Enabled = true;
                    ////Code commented by khaja to fix bug#4551
                    ////this.AutoFundTransferDataGridView.Focus();
                    ////
                    this.SetAttachmentCommentsCount();
                    ////TerraScanCommon.SetDataGridViewPosition(this.AutoFundTransferDataGridView, 0);
                }
                else
                {
                    this.AutoFundTransferDataGridView.Rows[0].Selected = false;
                    this.AutoFundTransferDataGridView.Enabled = false;
                }

                if (!string.IsNullOrEmpty(this.autoFundData.ListAutoFundAccountTransferDetails.Rows[0][this.autoFundData.ListAutoFundAccountTransferDetails.AutoTransferIDColumn].ToString()))
                {
                    //// this.autoTransferId = Convert.ToInt32(this.autoFundData.ListAutoFundAccountTransferDetails.Rows[0][this.autoFundData.ListAutoFundAccountTransferDetails.AutoTransferIDColumn]);
                    this.footerSmartPart.KeyId = this.autoTransferId;
                    this.additionalOperationSmartPart.KeyId = this.autoTransferId;
                }
            }
            else
            {
                this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                this.reportActionSmartPart.Enabled = true;
                this.CommentsdeckWorkspace.Enabled = false;
                if (this.operationSmartPart != null)
                {
                    this.operationSmartPart.SaveButtonEnable = false;
                    this.operationSmartPart.CancelButtonEnable = false;
                }

                this.footerSmartPart.KeyId = null;
            }

            this.formLoad = false;
        }

        /// <summary>
        /// Customizes the item listing grid view.
        /// </summary>
        private void CustomizeItemListingGridView()
        {
            try
            {
                this.AutoFundTransferDataGridView.AutoGenerateColumns = false;
                this.IsSubFund.DataPropertyName = this.autoFundData.ListAutoFundAccountTransferDetails.IsSubFundColumn.ColumnName;
                this.SubFundColumn.DataPropertyName = this.autoFundData.ListAutoFundAccountTransferDetails.SourceKeyNameColumn.ColumnName;
                this.AccountNameColumn.DataPropertyName = this.autoFundData.ListAutoFundAccountTransferDetails.AccountNameColumn.ColumnName;
                this.TransaferRate.DataPropertyName = this.autoFundData.ListAutoFundAccountTransferDetails.TransaferRateColumn.ColumnName;
                this.DestinationAccountID.DataPropertyName = this.autoFundData.ListAutoFundAccountTransferDetails.DestinationAccountIDColumn.ColumnName;
                this.SourceSubFundID.DataPropertyName = this.autoFundData.ListAutoFundAccountTransferDetails.SourceKeyIDColumn.ColumnName;
                this.SourceRollyear.DataPropertyName = this.autoFundData.ListAutoFundAccountTransferDetails.SourceRollyearColumn.ColumnName;
                this.DestinationRollyear.DataPropertyName = this.autoFundData.ListAutoFundAccountTransferDetails.DestinationRollyearColumn.ColumnName;
                this.SubFundID.DataPropertyName = this.autoFundData.ListAutoFundAccountTransferDetails.SourceKeyColumn.ColumnName;
                this.AutoTransferIDColumn.DataPropertyName = this.autoFundData.ListAutoFundAccountTransferDetails.AutoTransferIDColumn.ColumnName;
                this.IsSourceAcctPending.DataPropertyName = this.autoFundData.ListAutoFundAccountTransferDetails.IsSourceAcctPendingColumn.ColumnName;
                this.IsDestinationAcctPending.DataPropertyName = this.autoFundData.ListAutoFundAccountTransferDetails.IsDestinationAcctPendingColumn.ColumnName;

                this.IsSubFund.DisplayIndex = 0;
                this.SubFundColumn.DisplayIndex = 1;
                this.AccountNameColumn.DisplayIndex = 2;
                this.TransaferRate.DisplayIndex = 3;

                (this.AutoFundTransferDataGridView.Columns[this.autoFundData.ListAutoFundAccountTransferDetails.IsSubFundColumn.ColumnName] as DataGridViewComboBoxColumn).DataSource = this.sourceDatatable;
                (this.AutoFundTransferDataGridView.Columns[this.autoFundData.ListAutoFundAccountTransferDetails.IsSubFundColumn.ColumnName] as DataGridViewComboBoxColumn).DisplayMember = "SourceType";
                (this.AutoFundTransferDataGridView.Columns[this.autoFundData.ListAutoFundAccountTransferDetails.IsSubFundColumn.ColumnName] as DataGridViewComboBoxColumn).ValueMember = this.autoFundData.ListAutoFundAccountTransferDetails.IsSubFundColumn.ColumnName;

                this.AutoFundTransferDataGridView.DataSource = this.autoFundData.ListAutoFundAccountTransferDetails;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Initializes the combo.
        /// </summary>
        private void InitializeRollYearCombo()
        {
            this.autoFundData.ListAutoFundRollYear.Clear();
            this.autoFundData = this.form1025Controller.WorkItem.F1025_ListRollYear();
            if (this.autoFundData.ListAutoFundRollYear.Rows.Count > 0)
            {
                this.RollYearCombo.ValueMember = this.autoFundData.ListAutoFundRollYear.RollYearColumn.ColumnName;
                this.RollYearCombo.DisplayMember = this.autoFundData.ListAutoFundRollYear.RollYearColumn.ColumnName;
                this.RollYearCombo.DataSource = this.autoFundData.ListAutoFundRollYear;
                this.GetYear();
            }
            else
            {
                CommentsData getYearDataSet = new CommentsData();
                getYearDataSet = this.form1025Controller.WorkItem.GetConfigDetails("TR_RollYear");
                if (getYearDataSet == null)
                {
                    //// write code to close the form
                }
                else
                {
                    if (getYearDataSet.GetCommentsConfigDetails.Rows.Count > 0)
                    {
                        F1025AutoFundTransferData.ListAutoFundRollYearRow defaultRollYear = this.autoFundData.ListAutoFundRollYear.NewListAutoFundRollYearRow();
                        defaultRollYear.RollYear = Convert.ToInt16(getYearDataSet.GetCommentsConfigDetails.Rows[0][getYearDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString());
                        this.autoFundData.ListAutoFundRollYear.AddListAutoFundRollYearRow(defaultRollYear);
                        this.RollYearCombo.DataSource = this.autoFundData.ListAutoFundRollYear;
                        this.RollYearCombo.ValueMember = this.autoFundData.ListAutoFundRollYear.RollYearColumn.ColumnName;
                        this.RollYearCombo.DisplayMember = this.autoFundData.ListAutoFundRollYear.RollYearColumn.ColumnName;
                        this.RollYearCombo.SelectedValue = Convert.ToInt16(getYearDataSet.GetCommentsConfigDetails.Rows[0][getYearDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString());
                        short.TryParse(this.RollYearCombo.SelectedValue.ToString(), out this.selectedRollYear);
                    }
                    else
                    {
                        // write code to close the form
                    }
                }
            }
        }

        /// <summary>
        /// Gets the year.
        /// </summary>
        private void GetYear()
        {
            CommentsData getYearDataSet = new CommentsData();
            getYearDataSet = this.form1025Controller.WorkItem.GetConfigDetails("TR_RollYear");
            if (getYearDataSet == null)
            {
                this.RollYearCombo.SelectedValue = string.Empty;
                short.TryParse(this.RollYearCombo.SelectedValue.ToString(), out this.selectedRollYear);
            }
            else
            {
                if (getYearDataSet.GetCommentsConfigDetails.Rows.Count > 0)
                {
                    DataRow[] selectedYear;
                    string uniqueExpression;
                    if (this.autoFundData.ListAutoFundRollYear.Rows.Count > 0)
                    {
                        uniqueExpression = (this.autoFundData.ListAutoFundRollYear.RollYearColumn.ColumnName + "=" + getYearDataSet.GetCommentsConfigDetails.Rows[0][getYearDataSet.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString());
                        selectedYear = this.autoFundData.ListAutoFundRollYear.Select(uniqueExpression);

                        if (selectedYear.Length > 0)
                        {
                            this.RollYearCombo.SelectedValue = Convert.ToInt16(getYearDataSet.GetCommentsConfigDetails.Rows[0][getYearDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString());
                            short.TryParse(this.RollYearCombo.SelectedValue.ToString(), out this.selectedRollYear);
                        }
                        else
                        {
                            DataRow[] selectedRow = this.autoFundData.ListAutoFundRollYear.Select(this.autoFundData.ListAutoFundRollYear.RollYearColumn.ColumnName + "= MAX(" + this.autoFundData.ListAutoFundRollYear.RollYearColumn.ColumnName + ")");
                            if (selectedRow.Length > 0)
                            {
                                this.RollYearCombo.SelectedValue = Convert.ToInt16(selectedRow[0][0].ToString());
                                short.TryParse(this.RollYearCombo.SelectedValue.ToString(), out this.selectedRollYear);
                            }
                        }
                    }
                }
                else
                {
                    short.TryParse(this.RollYearCombo.SelectedValue.ToString(), out this.selectedRollYear);
                }
            }
            ////this.RollYearCombo.SelectedValue = Convert.ToInt16(getYearDataSet.GetCommentsConfigDetails.Rows[0][getYearDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString());            
        }

        /// <summary>
        /// Saves the auto fund transfer details.
        /// </summary>
        /// <returns>saveResult</returns>
        private bool SaveAutoFundTransferDetails()
        {
            bool saveResult = false;
            try
            {
                int saveConfirm = -1;
                string errorMessage = string.Empty;

                this.Cursor = Cursors.WaitCursor;
                if (this.ValidateAutoFundDetails())
                {
                    string autoFundItemsDetails = string.Empty;

                    this.AutoFundTransferDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

                    autoFundItemsDetails = TerraScanCommon.GetXmlString(this.autoFundData.ListAutoFundAccountTransferDetails);
                    saveConfirm = this.form1025Controller.WorkItem.F1025_UpdateAutoFundTransferDetails(autoFundItemsDetails, TerraScanCommon.UserId);

                    if (saveConfirm == 0)
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                        this.PopulateAutoFundTransferDataGridView();
                        this.RollYearCombo.Focus();
                        saveResult = true;
                    }
                    else if (saveConfirm == -1)
                    {
                        errorMessage = "This record cannot be saved because the Source SubFund\\Account and Destination Account \ndo not have the same Roll Year.";
                        MessageBox.Show(errorMessage, "TerraScan – Invalid Records Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        saveResult = false;
                    }
                    else if (saveConfirm == -2)
                    {
                        errorMessage = "This record cannot be saved because the selected combination already exists.";
                        MessageBox.Show(errorMessage, ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        saveResult = false;
                    }
                    else if (saveConfirm == -3)
                    {
                        errorMessage = "The entered SubFund\\Account does not exists for this Roll Year.";
                        MessageBox.Show(errorMessage, "TerraScan – Invalid SubFund\\Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        saveResult = false;
                    }
                }
                else
                {
                    if (!this.flagMissingRequiredField)
                    {
                        errorMessage = "This record cannot be saved because it is missing required fields.";
                    }
                    else if (!this.flagInvalidPercent)
                    {
                        errorMessage = "Invalid Percentage.";
                    }

                    MessageBox.Show(errorMessage, ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    saveResult = false;
                }
            }
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.Display, this.ParentForm);
                saveResult = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                saveResult = false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return saveResult;
        }

        /// <summary>
        /// Validates the auto fund details.
        /// </summary>
        /// <returns>validationResult</returns>
        private bool ValidateAutoFundDetails()
        {
            bool validationResult = true;
            this.flagInvalidPercent = true;
            this.flagMissingRequiredField = true;

            this.AutoFundTransferDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            ////this.autoFundData.ListAutoFundAccountTransferDetails.AcceptChanges();  

            for (int i = 0; i < this.AutoFundTransferDataGridView.Rows.Count; i++)
            {
                if (validationResult)
                {
                    if (((!string.IsNullOrEmpty(this.AutoFundTransferDataGridView.Rows[i].Cells[1].Value.ToString().Trim())) || (!string.IsNullOrEmpty(this.AutoFundTransferDataGridView.Rows[i].Cells[this.AccountNameColumn.Index].Value.ToString().Trim())) || (!string.IsNullOrEmpty(this.AutoFundTransferDataGridView.Rows[i].Cells[this.TransaferRate.Index].Value.ToString().Trim()))))
                    {
                        ////khaja made changes to fix Bug#4622
                        ////if (((string.IsNullOrEmpty(this.AutoFundTransferDataGridView.Rows[i].Cells[0].Value.ToString().Trim())) || (string.IsNullOrEmpty(this.AutoFundTransferDataGridView.Rows[i].Cells[2].Value.ToString().Trim()))))
                        if (((string.IsNullOrEmpty(this.AutoFundTransferDataGridView.Rows[i].Cells[this.SubFundColumn.Name].Value.ToString().Trim())) || (string.IsNullOrEmpty(this.AutoFundTransferDataGridView.Rows[i].Cells[this.AccountNameColumn.Index].Value.ToString().Trim())) || (string.IsNullOrEmpty(this.AutoFundTransferDataGridView.Rows[i].Cells[this.TransaferRate.Index].Value.ToString().Trim()))))
                        {
                            validationResult = false;
                            this.flagMissingRequiredField = false;
                            this.flagInvalidPercent = true;
                            break;
                        }
                        else if (((decimal)this.AutoFundTransferDataGridView.Rows[i].Cells[this.TransaferRate.Index].Value <= 0))
                        {
                            this.flagMissingRequiredField = true;
                            this.flagInvalidPercent = false;
                            validationResult = false;
                            break;
                        }
                    }
                }
            }

            return validationResult;
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <returns>the status of the page</returns>
        private bool CheckPageStatus()
        {
            DialogResult dialogResult;
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    return this.SaveAutoFundTransferDetails();
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    return true;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Sets the attachment comments count.
        /// </summary>       
        private void SetAttachmentCommentsCount()
        {
            ////Display Comments Count in the Comments Buttons  and attachment count in the attachment Buttons       
            try
            {
                AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);

                if (this.autoTransferId != -99)
                {
                    this.additionalOperationSmartPart.KeyId = this.autoTransferId;
                    additionalOperationCountEntity.AttachmentCount = this.form1025Controller.WorkItem.GetAttachmentCount(this.ParentFormId, this.autoTransferId, TerraScanCommon.UserId);
                    CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.form1025Controller.WorkItem.GetCommentsCount(this.ParentFormId, this.autoTransferId, TerraScanCommon.UserId);
                    if (commentsCountDataTable.Rows.Count > 0)
                    {
                        additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                        additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                    }
                }
                else
                {
                }

                this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;
            }
            catch (Exception ex)
            {
                ////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Createsources the datatable.
        /// </summary>
        private void CreatesourceDatatable()
        {
            this.sourceDatatable = new DataTable();
            this.sourceDatatable.Clear();
            this.sourceDatatable.Columns.Add(this.autoFundData.ListAutoFundAccountTransferDetails.IsSubFundColumn.ColumnName, typeof(byte));
            this.sourceDatatable.Columns.Add(SharedFunctions.GetResourceString("F1025SourceTypeColumn"), typeof(string));
            this.sourceDatatable.Rows.Add(null, string.Empty);
            this.sourceDatatable.Rows.Add(0, SharedFunctions.GetResourceString("F1025Account"));
            this.sourceDatatable.Rows.Add(1, SharedFunctions.GetResourceString("F1025SubFund"));
            this.sourceDatatable.AcceptChanges();
        }

        /// <summary>
        /// Opens the sub fund selection form.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void OpenSubFundSelectionForm(DataGridViewCellMouseEventArgs e)
        {
            Form subfundForm = new Form();

            object[] optionalParameter = new object[] { this.RollYearCombo.SelectedValue, 1 };
            subfundForm = this.form1025Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1515, optionalParameter, this.form1025Controller.WorkItem);
            if (subfundForm != null)
            {
                if (subfundForm.ShowDialog() == DialogResult.OK)
                {
                    string subFund = string.Empty;
                    subFund = TerraScanCommon.GetValue(subfundForm, "SubFundItem").ToString();
                    if (!string.IsNullOrEmpty(subFund.Trim()))
                    {
                        short rollYear;
                        short.TryParse(TerraScanCommon.GetValue(subfundForm, "RollYearValue"), out rollYear);
                        this.subFundManagementData = this.form1025Controller.WorkItem.F9503_GetSubFundItems(subFund.Trim(), rollYear);
                        if (this.subFundManagementData.getSubFundItems.Rows.Count > 0)
                        {
                            this.currentRowSourceKeyName = subFund.Trim();
                            this.currentRowSourceKeyId = Convert.ToInt32(this.subFundManagementData.getSubFundItems.Rows[0]["SubFundID"]);
                            this.AutoFundTransferDataGridView[e.ColumnIndex, e.RowIndex].Value = subFund.Trim();
                            this.AutoFundTransferDataGridView[this.SubFundID.Index, e.RowIndex].Value = Convert.ToInt32(this.subFundManagementData.getSubFundItems.Rows[0]["SubFundID"]);
                            this.AutoFundTransferDataGridView[this.SourceRollyear.Name, e.RowIndex].Value = rollYear;
                            this.AutoFundTransferDataGridView[this.IsSourceAcctPending.Name, e.RowIndex].Value = false;
                            this.SetEditSaveRecord();
                            this.AutoFundTransferDataGridView.RefreshEdit();
                        }
                    }
                    else
                    {
                        this.AutoFundTransferDataGridView[e.ColumnIndex, e.RowIndex].Value = string.Empty;
                        this.AutoFundTransferDataGridView[this.SubFundID.Index, e.RowIndex].Value = 0;
                        this.AutoFundTransferDataGridView.RefreshEdit();
                    }
                }
            }
        }

        /// <summary>
        /// Opens the account selection form.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void OpenAccountSelectionForm(DataGridViewCellMouseEventArgs e)
        {
            bool tempAccountStatus;
            int accountId;
            object[] optionalParameter = new object[] { this.RollYearCombo.SelectedValue, 0 };
            Form accountSelectionForm = this.form1025Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1345, optionalParameter, this.form1025Controller.WorkItem);
            if (accountSelectionForm != null)
            {
                if (accountSelectionForm.ShowDialog() == DialogResult.OK)
                {
                    short rollYear;
                    int.TryParse(TerraScanCommon.GetValue(accountSelectionForm, "AccountId").ToString(), out accountId);
                    short.TryParse(TerraScanCommon.GetValue(accountSelectionForm, "RollYearValue"), out rollYear);
                    ExciseTaxRateData accountNameDataSet = new ExciseTaxRateData();
                    accountNameDataSet = this.form1025Controller.WorkItem.GetAccountName(accountId);
                    if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.SetEditSaveRecord();
                        tempAccountStatus = Convert.ToBoolean(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString());
                        this.AutoFundTransferDataGridView[e.ColumnIndex, e.RowIndex].Value = accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctColumn].ToString();
                        if (e.ColumnIndex.Equals(this.SubFundColumn.Index))
                        {
                            this.AutoFundTransferDataGridView[this.IsSourceAcctPending.Name, e.RowIndex].Value = tempAccountStatus;
                            this.AutoFundTransferDataGridView[this.SourceSubFundID.Name, e.RowIndex].Value = accountId;
                            this.AutoFundTransferDataGridView[this.SubFundID.Name, e.RowIndex].Value = accountId;
                            this.AutoFundTransferDataGridView[this.SourceRollyear.Name, e.RowIndex].Value = rollYear;
                        }
                        else
                        {
                            this.AutoFundTransferDataGridView[this.IsDestinationAcctPending.Name, e.RowIndex].Value = tempAccountStatus;
                            this.AutoFundTransferDataGridView[this.DestinationAccountID.Name, e.RowIndex].Value = accountId;
                            this.AutoFundTransferDataGridView[this.DestinationRollyear.Name, e.RowIndex].Value = rollYear;
                        }

                        if (tempAccountStatus)
                        {
                            this.AutoFundTransferDataGridView[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.FromArgb(187, 222, 173);
                        }
                        else
                        {
                            this.AutoFundTransferDataGridView[e.ColumnIndex, e.RowIndex].Style.BackColor = this.AutoFundTransferDataGridView[this.TransaferRate.Index, e.RowIndex].Style.BackColor;
                        }

                        TerraScanTextAndImageCell imgCell = (TerraScanTextAndImageCell)this.AutoFundTransferDataGridView[this.AccountNameColumn.Index, e.RowIndex];
                        imgCell.ReadOnly = true;
                    }

                    this.AutoFundTransferDataGridView.RefreshEdit();
                }
            }
        }

        #endregion Private Methods
    }
}
