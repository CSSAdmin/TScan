//--------------------------------------------------------------------------------------------
// <copyright file="F9503.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F9503 SubFund Management Functionality
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 17-11-2006       Shiva              Created
//*********************************************************************************/

namespace D9500
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
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.Utilities;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using TerraScan.BusinessEntities;
    using System.Web.Services.Protocols;
    using TerraScan.Common.Reports;
    using System.Collections;
    using System.Configuration;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.UI.Controls;

    /// <summary>
    /// F9503 SubFund Management User Interface
    /// </summary>
    [SmartPart]
    public partial class F9503 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// Variable Instance for F9503Controller
        /// </summary>
        private F9503Controller form9503Controll;

        /// <summary>
        /// operation SmartPart
        /// </summary>
        private OperationSmartPart operationSmartPart = new OperationSmartPart();

        /// <summary>
        /// additionalOperationSmartPart
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart = new AdditionalOperationSmartPart();

        /// <summary>
        /// statusBarSmartPart
        /// </summary>
        private StatusBarSmartPart statusBarSmartPart = new StatusBarSmartPart();

        /// <summary>
        /// Variable Holds the Instance of the F9503SubFundMgmtDataSet
        /// </summary>
        private F9503SubFundManagementData subFundMgmtDataSet = new F9503SubFundManagementData();

        /// <summary>
        /// Variable Holds the PageMode Status
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Variable Holds the subFundId Default value is Null
        /// </summary>
        private int? subFundId = null;

        /// <summary>
        /// Variable Holds the fundId value Default value is -1
        /// </summary>
        private int? fundId = null;

        /// <summary>
        /// Variable Holds the flag Value for PageLoad
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// Variable Holds the Total Count for the SubFundIds
        /// </summary>
        private int totalSubFundIdsCount;

        /// <summary>
        /// Variable Holds the CurrentRecord Id
        /// </summary>
        private int currentRecord;

        /// <summary>
        /// Array Variable Holds the RecordPointer Values
        /// </summary>
        private int[] recordPointerArray = new int[2];

        /// <summary>
        /// Variable Holds the agencyId
        /// </summary>
        private int? agencyId;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9503"/> class.
        /// </summary>
        public F9503()
        {
            InitializeComponent();
            this.CustomizeDisbursementHistoryGridView();
            this.SubFundsListPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SubFundsListPictureBox.Height, this.SubFundsListPictureBox.Width, "SubFund", 28, 81, 128);
            this.pictureBox1.Image = ExtendedGraphics.GenerateVerticalImage(this.pictureBox1.Height, this.pictureBox1.Width, "Disbursement History", 174, 150, 94);
        }

        #endregion

        #region Published Events

        /// <summary>
        /// Declare the event setFormHeader        
        /// </summary>   
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// Event Publication for PageStatusActivated
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> PageStatusActivated;

        /// <summary>
        /// Event Publication for SetActiveRecordButtons
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecordButtons, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int[]>> SetActiveRecordButtons;

        /// <summary>
        /// Event Publication for SetRecordCount
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetRecordCount, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetRecordCount;

        /// <summary>
        /// Event Publication for SetActiveRecord
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetActiveRecord;

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

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F1226 control.
        /// </summary>
        /// <value>The F1226 control.</value>
        [CreateNew]
        public F9503Controller Form9503Controll
        {
            get { return this.form9503Controll as F9503Controller; }
            set { this.form9503Controll = value; }
        }

        /// <summary>
        /// Gets or sets the sub fund ID.
        /// </summary>
        /// <value>The sub fund ID.</value>
        public int? SubFundIdentity
        {
            get
            {
                return this.subFundId;
            }

            set
            {
                this.subFundId = value;
                this.additionalOperationSmartPart.KeyId = Convert.ToInt32(this.subFundId);
            }
        }

        /// <summary>
        /// Gets or sets the fund id.
        /// </summary>
        /// <value>The fund id.</value>
        public int? FundId
        {
            get { return this.fundId; }
            set { this.fundId = value; }
        }

        /// <summary>
        /// Gets or sets the agency id.
        /// </summary>
        /// <value>The agency id.</value>
        public int? AgencyId
        {
            get { return this.agencyId; }
            set { this.agencyId = value; }
        }

        #endregion

        #region SubScribed Events

        /// <summary>
        /// Displays the navigated record.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_DisplayNavigatedRecord, Thread = ThreadOption.UserInterface)]
        public void DisplayNavigatedRecord(object sender, DataEventArgs<RecordNavigationEntity> e)
        {
            RecordNavigationEntity recordNavigationEntity = e.Data;
            this.pageLoadStatus = true;    
            this.PopulateSubFundDetails(null, recordNavigationEntity.RecordIndex);
            this.pageLoadStatus = false;
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_CheckPageStatus, Thread = ThreadOption.UserInterface)]
        public void CheckPageStatus(object sender, DataEventArgs<Button> e)
        {
            if (this.CheckPageStatus())
            {
                this.PageStatusActivated(this, new DataEventArgs<Button>(e.Data));
            }
        }

        /// <summary>
        /// Operations the button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_OperationButtonClick, Thread = ThreadOption.UserInterface)]
        public void OperationButtonClick(object sender, DataEventArgs<string> e)
        {
            switch (e.Data)
            {
                case "NEW":
                    this.NewButton_Click();
                    break;
                case "SAVE":
                    this.SaveButton_Click();
                    break;
                case "CANCEL":
                    this.CancelButton_Click();
                    break;
            }
        }

        ///// <summary>
        ///// Clears the filter button click.
        ///// </summary>
        ///// <param name="sender">The sender.</param>
        ///// <param name="e">The e.</param>
        ////[EventSubscription(EventTopics.D9001_TerrascanSmartParts_ToolBoxSmartPart_ClearFilterButtonClick, Thread = ThreadOption.UserInterface)]
        ////public void ClearFilterButtonClick(object sender, DataEventArgs<Button> e)
        ////{
        ////    if (this.CheckPageStatus())
        ////    {
        ////        this.ClearFilterFunction(this, new DataEventArgs<Button>(e.Data));
        ////    }
        ////}

        /// <summary>
        /// Gets the form status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The source of the event</param>
        [EventSubscription(EventTopics.D9001_ShellForm_GetFormStatus, Thread = ThreadOption.UserInterface)]
        public void GetFormStatus(object sender, DataEventArgs<string> e)
        {
            if (e.Data == this.GetType().Name)
            {
                this.form9503Controll.WorkItem.State["FormStatus"] = this.CheckPageStatus();
            }
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
        /// Sends the optional parameters.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        ///[EventSubscription("topic://TerraScan.UI.CAB/MainForm/SendOptionalParameters", Thread = ThreadOption.UserInterface)]
        [EventSubscription(EventTopics.D9001_ShellForm_SendOptionalParameters, Thread = ThreadOption.UserInterface)]
        public void SendOptionalParameters(object sender, DataEventArgs<object[]> e)
        {
            object[] optionalParams = e.Data;
            if (optionalParams.Length > 0 && optionalParams[optionalParams.Length - 1].Equals(this.ParentFormId))
            {
                try
                {
                    int recordIndex = -1;
                    this.SubFundIdentity = Convert.ToInt32(optionalParams[0]);
                    this.pageLoadStatus = true;
                    if (this.SubFundIdentity.HasValue)
                    {
                        recordIndex = this.RetrieveRecordIndex(Convert.ToInt32(this.SubFundIdentity));
                    }

                    if (recordIndex != -1)
                    {
                        this.PopulateSubFundDetails(this.SubFundIdentity, recordIndex);
                    }
                    else
                    {
                        this.PopulateSubFundDetails(null, recordIndex);
                    }

                    this.pageLoadStatus = false;
                }
                catch (Exception ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
            }
        }

        #endregion

        #region Form Events

        /// <summary>
        /// Handles the Click event of the FundButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FundButton_Click(object sender, EventArgs e)
        {
            try
            {
                Form fundSelectionForm = new Form();
                object[] optionalParameter = new object[0];
                fundSelectionForm = this.form9503Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9104, optionalParameter, this.form9503Controll.WorkItem);
                if (fundSelectionForm != null)
                {
                    if (fundSelectionForm.ShowDialog() == DialogResult.OK)
                    {
                        this.fundId = Convert.ToInt32(TerraScanCommon.GetValue(fundSelectionForm, "FundId"));
                        this.FundLinkLabel.Text = TerraScanCommon.GetValue(fundSelectionForm, "FundItem").ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the AgencyButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AgencyButton_Click(object sender, EventArgs e)
        {
            try
            {
                Form agencySelectionForm = new Form();
                agencySelectionForm = this.form9503Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9102, null, this.form9503Controll.WorkItem);
                if (agencySelectionForm != null)
                {
                    if (agencySelectionForm.ShowDialog() == DialogResult.OK)
                    {
                        this.agencyId = Convert.ToInt32(TerraScanCommon.GetValue(agencySelectionForm, "AgencyId"));
                        this.AgencyLinkLabel.Text = TerraScanCommon.GetValue(agencySelectionForm, "agencyName").ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the FundLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void FundLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormInfo formInfo;
            if (this.FundId != null)
            {
                formInfo = TerraScanCommon.GetFormInfo(11003);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.FundId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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
                ////if (!this.pageLoadStatus)
                ////{
                ////    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                ////    this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                ////}
                this.SetEditRecord();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Handles the Leave event of the LevyRateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LevyRateTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                TerraScanTextBox tempControl = (TerraScanTextBox)sender;
                if (!String.IsNullOrEmpty(tempControl.Text))
                {
                    tempControl.Text = tempControl.Text;
                    
                    if (this.ValidateDecimalTextBox(tempControl.Text.ToString(), tempControl.Name.ToString()))
                    {
                        tempControl.Text = string.Empty;
                        tempControl.Focus();
                    }
                }
                else
                {
                    tempControl.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PreviewButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////changed the parameter type from string to int
                TerraScanCommon.ShowReport(950301, Report.ReportType.Preview);
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
        /// Handles the LinkClicked event of the HelplinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void HelplinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.ParentFormId.ToString()))
                {
                    HelpEngine.Show(ParentForm.Text, this.ParentFormId.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the SubFundManagementAuditLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void SubFundManagementAuditLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Hashtable reportFileIdHashTable = new Hashtable();
                this.Cursor = Cursors.WaitCursor;
                if (this.SubFundIdentity.HasValue)
                {
                    reportFileIdHashTable.Clear();
                    reportFileIdHashTable.Add("SubFundID", this.SubFundIdentity);
                    ////changed the parameter type from string to int
                    TerraScanCommon.ShowReport(950390, TerraScan.Common.Reports.Report.ReportType.Preview, reportFileIdHashTable);
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("+"), "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        /// <summary>
        /// Handles the LinkClicked event of the AgencyLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void AgencyLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormInfo formInfo;
            if (this.AgencyId != null)
            {
                formInfo = TerraScanCommon.GetFormInfo(9504);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.AgencyId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
        }

        #endregion

        #region Private Methods

        #region DisbursementHistory GridView Methods

        /// <summary>
        /// Customizes the transactions grid.
        /// </summary>
        private void CustomizeDisbursementHistoryGridView()
        {
            this.DisbursementHistoryGridView.AllowUserToResizeColumns = false;
            this.DisbursementHistoryGridView.AutoGenerateColumns = false;
            this.DisbursementHistoryGridView.AllowUserToResizeRows = false;
            this.DisbursementHistoryGridView.StandardTab = true;
            this.DisbursementHistoryGridView.PrimaryKeyColumnName = this.subFundMgmtDataSet.ListDisbursementHistory.SubFundIDColumn.ColumnName.ToString();

            this.DisbursementHistoryGridView.Columns["AccountName"].DataPropertyName = this.subFundMgmtDataSet.ListDisbursementHistory.AccountNameColumn.ColumnName.ToString();
            this.DisbursementHistoryGridView.Columns["PayableTo"].DataPropertyName = this.subFundMgmtDataSet.ListDisbursementHistory.PayableToColumn.ColumnName.ToString();
            this.DisbursementHistoryGridView.Columns["EntryDate"].DataPropertyName = this.subFundMgmtDataSet.ListDisbursementHistory.EntryDateColumn.ColumnName.ToString();
            this.DisbursementHistoryGridView.Columns["Amount"].DataPropertyName = this.subFundMgmtDataSet.ListDisbursementHistory.AmountColumn.ColumnName.ToString();
            this.DisbursementHistoryGridView.Columns["SubFundId"].DataPropertyName = this.subFundMgmtDataSet.ListDisbursementHistory.SubFundIDColumn.ColumnName.ToString();

            this.DisbursementHistoryGridView.Columns["AccountName"].Width = 245;
            this.DisbursementHistoryGridView.Columns["PayableTo"].Width = 245;
            this.DisbursementHistoryGridView.Columns["EntryDate"].Width = 110;
            this.DisbursementHistoryGridView.Columns["Amount"].Width = 149;
            this.DisbursementHistoryGridView.Columns["SubFundId"].Width = 100;
        }

        /// <summary>
        /// Handles the CellFormatting event of the DisbursementHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void DisbursementHistoryGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;
            DateTime outDateTime;
            try
            {
                //// Only paint if desired, formattable column

                if (e.ColumnIndex == this.DisbursementHistoryGridView.Columns["Amount"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.DisbursementHistoryGridView.Rows[e.RowIndex].Cells["Amount"].Value.ToString().Trim()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            if (outDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                                e.CellStyle.ForeColor = Color.Green;
                            }
                            else
                            {
                                e.Value = outDecimal.ToString("#,##0.00");
                                e.FormattingApplied = true;
                            }
                        }
                        else
                        {
                            e.Value = "0.00 ";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }

                //// date fomatting

                if (e.ColumnIndex == this.DisbursementHistoryGridView.Columns["EntryDate"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value))
                    {
                        string val = e.Value.ToString();
                        if (DateTime.TryParse(val, out outDateTime))
                        {
                            e.Value = outDateTime.ToString("MM/dd/yyyy");
                            e.FormattingApplied = true;
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

        #endregion

        #region General Methods

        /// <summary>
        /// Inits the voter approved combo box.
        /// </summary>
        private void InitVoterApprovedComboBox()
        {
            CommonData commonData = new CommonData();
            ////which loads yes, no value to the ComboBoxDataTable
            commonData.LoadYesNoValue();
            this.VoterApprovedComboBox.DataSource = commonData.ComboBoxDataTable;
            this.VoterApprovedComboBox.ValueMember = commonData.ComboBoxDataTable.KeyIdColumn.ToString();
            this.VoterApprovedComboBox.DisplayMember = commonData.ComboBoxDataTable.KeyNameColumn.ToString();
        }

        /// <summary>
        /// Inits the type combo box.
        /// </summary>
        private void InitTypeComboBox()
        {
            try
            {
                    this.TypeComboBox.DataSource = null;
                    if (this.subFundMgmtDataSet.ListSubFundType.Rows.Count > 0)
                    {
                        this.TypeComboBox.DataSource = this.subFundMgmtDataSet.ListSubFundType;
                        this.TypeComboBox.ValueMember = this.subFundMgmtDataSet.ListSubFundType.SubFundTypeIDColumn.ColumnName;
                        this.TypeComboBox.DisplayMember = this.subFundMgmtDataSet.ListSubFundType.SubFundTypeColumn.ColumnName;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Populates the sub fund details.
        /// </summary>
        /// <param name="subFundIdentifier">The sub fund identifier.</param>
        /// <param name="currentRowIndex">Index of the current row.</param>
        private void PopulateSubFundDetails(int? subFundIdentifier, int currentRowIndex)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.subFundMgmtDataSet = this.form9503Controll.WorkItem.F9503_GetSubFundManagementDetails(subFundIdentifier);
                this.InitTypeComboBox();
                if (currentRowIndex > 0)
                {
                    this.currentRecord = currentRowIndex;
                }
                else
                {
                    if (this.subFundMgmtDataSet.ListSubFundIds.Rows.Count > 0)
                    {
                        this.currentRecord = 1;
                    }
                    else
                    {
                        this.currentRecord = 0;
                    }
                }

                //// RecordNavigation Implementation Code

                this.totalSubFundIdsCount = this.subFundMgmtDataSet.ListSubFundIds.Rows.Count;

                if (this.totalSubFundIdsCount > 0)
                {
                    this.subFundId = Convert.ToInt32(this.subFundMgmtDataSet.ListSubFundIds[this.currentRecord - 1][0].ToString());
                    this.RecordNavigatorDeckWorkspace.Enabled = true;
                    this.AddtionalOperationDeckWorkspace.Enabled = true;
                    this.SetActiveRecord(this, new DataEventArgs<int>(currentRowIndex));
                    this.SetRecordCount(this, new DataEventArgs<int>(this.totalSubFundIdsCount));
                    this.recordPointerArray[0] = currentRowIndex;
                    this.recordPointerArray[1] = this.totalSubFundIdsCount;
                    this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(this.recordPointerArray));
                    this.SubFundManagementAuditLink.Text = SharedFunctions.GetResourceString("9503SubFundManagementAuditLink") + " " + this.SubFundIdentity.ToString();
                    this.SubFundManagementAuditLink.Enabled = true;
                    this.GetSubFundHeaderDetails(this.RetrieveSubFundId(currentRowIndex));
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    this.SetAttachmentCommentsCount();
                }
                else
                {
                    this.SubFundManagementAuditLink.Enabled = false;
                    this.SetActiveRecord(this, new DataEventArgs<int>(0));
                    this.SetRecordCount(this, new DataEventArgs<int>(this.totalSubFundIdsCount));
                    this.recordPointerArray[0] = 0;
                    this.recordPointerArray[1] = this.totalSubFundIdsCount;
                    this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(this.recordPointerArray));
                    this.RecordNavigatorDeckWorkspace.Enabled = false;
                    this.PopulateDisbursementHistoryGridView();
                    this.NullRecords = true;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
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

        /// <summary>
        /// Populates the disbursement history grid view.
        /// </summary>
        private void PopulateDisbursementHistoryGridView()
        {
            int disbursementHistoryRowCount;

            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                {
                    this.subFundMgmtDataSet.ListDisbursementHistory.Rows.Clear();
                }

                disbursementHistoryRowCount = this.subFundMgmtDataSet.ListDisbursementHistory.Rows.Count;
                this.DisbursementHistoryGridView.DataSource = this.subFundMgmtDataSet.ListDisbursementHistory;

                if (disbursementHistoryRowCount > 0)
                {
                    this.DisbursementHistoryGridView.Enabled = true;
                    this.DisbursementHistoryGridView.Rows[0].Selected = true;
                }
                else
                {
                    this.DisbursementHistoryGridView.Rows[0].Selected = false;
                    this.DisbursementHistoryGridView.Enabled = false;
                }

                if (disbursementHistoryRowCount > this.DisbursementHistoryGridView.NumRowsVisible)
                {
                    this.SubFundMgmtVScorrlBar.Visible = false;
                }
                else
                {
                    this.SubFundMgmtVScorrlBar.Visible = true;
                }
            }
            catch (Exception ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Clears the sub fund header.
        /// </summary>
        private void ClearSubFundHeader()
        {
            this.fundId = null;
            this.agencyId = null;
            this.FundLinkLabel.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.SubFundTextBox.Text = string.Empty;
            this.VoterApprovedComboBox.SelectedValue = 1;
            this.TypeComboBox.SelectedValue = 1;
            this.LevyRateTextBox.Text = string.Empty;
            this.CommissionRateTextBox.Text = string.Empty;
            this.AgencyLinkLabel.Text = string.Empty;
            this.FactorTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.DisbursementBalanceTextBox.Text = string.Empty;
            this.SubFundManagementAuditLink.Enabled = true;
        }

        /// <summary>
        /// Sets the text box max lenght.
        /// </summary>
        private void SetTextBoxMaxLenght()
        {
            this.RollYearTextBox.MaxLength = this.subFundMgmtDataSet.SubFundDetails.RollYearColumn.MaxLength;
            this.SubFundTextBox.MaxLength = this.subFundMgmtDataSet.SubFundDetails.SubFundColumn.MaxLength;
            this.LevyRateTextBox.MaxLength = this.subFundMgmtDataSet.SubFundDetails.RateColumn.MaxLength;
            this.CommissionRateTextBox.MaxLength = this.subFundMgmtDataSet.SubFundDetails.CommissionRateColumn.MaxLength;
            this.FactorTextBox.MaxLength = this.subFundMgmtDataSet.SubFundDetails.FactorColumn.MaxLength;
            this.DescriptionTextBox.MaxLength = this.subFundMgmtDataSet.SubFundDetails.DescriptionColumn.MaxLength;
            this.DisbursementBalanceTextBox.MaxLength = this.subFundMgmtDataSet.SubFundDetails.DisbursementBalanceColumn.MaxLength;
        }

        /// <summary>
        /// Retrieves the sub fund id.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>returnt the SubFundId</returns>
        private int RetrieveSubFundId(int index)
        {
            int tempSubFundId = 0;

            if (index > 0)
            {
                if (index <= this.subFundMgmtDataSet.ListSubFundIds.Rows.Count)
                {
                    if (!string.IsNullOrEmpty(this.subFundMgmtDataSet.ListSubFundIds.Rows[index - 1][0].ToString()))
                    {
                        tempSubFundId = int.Parse(this.subFundMgmtDataSet.ListSubFundIds.Rows[index - 1][0].ToString());
                    }
                }
            }
            else
            {
                this.SetActiveRecord(this, new DataEventArgs<int>(1));
                this.SetRecordCount(this, new DataEventArgs<int>(this.subFundMgmtDataSet.ListSubFundIds.Rows.Count));
                this.recordPointerArray[0] = 1;
                this.recordPointerArray[1] = this.subFundMgmtDataSet.ListSubFundIds.Rows.Count;
                this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(this.recordPointerArray));
                tempSubFundId = int.Parse(this.subFundMgmtDataSet.ListSubFundIds.Rows[0][0].ToString()); ////toverify
            }

            return tempSubFundId;
        }

        /// <summary>
        /// Gets the sub fund header details.
        /// </summary>
        /// <param name="subFundIdentifier">The sub fund identifier.</param>
        private void GetSubFundHeaderDetails(int subFundIdentifier)
        {
            try
            {
                this.subFundMgmtDataSet = this.form9503Controll.WorkItem.F9503_GetSubFundManagementDetails(subFundIdentifier);
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

            if (this.subFundMgmtDataSet.SubFundDetails.Rows.Count > 0)
            {
                int tempAgencyId = -1;
                int tempFundId = -1;
                Int32.TryParse(this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.FundIDColumn.ColumnName].ToString(), out tempFundId);
                Int32.TryParse(this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.AgencyIDColumn.ColumnName].ToString(), out tempAgencyId);
                this.fundId = tempFundId;
                this.agencyId = tempAgencyId;
                this.FundLinkLabel.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.FundColumn.ColumnName].ToString();
                this.RollYearTextBox.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.RollYearColumn.ColumnName].ToString();
                this.SubFundTextBox.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.SubFundColumn.ColumnName].ToString();
                this.VoterApprovedComboBox.SelectedValue = Convert.ToInt32(this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.IsVoterApprovedColumn.ColumnName]);
                this.TypeComboBox.SelectedValue = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.SubFundTypeIDColumn.ColumnName];
                this.LevyRateTextBox.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.RateColumn.ColumnName].ToString();
                this.CommissionRateTextBox.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.CommissionRateColumn.ColumnName].ToString();
                this.AgencyLinkLabel.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.AgencyNameColumn.ColumnName].ToString();
                this.FactorTextBox.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.FactorColumn.ColumnName].ToString();
                this.DescriptionTextBox.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.DescriptionColumn.ColumnName].ToString();
                this.DisbursementBalanceTextBox.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.DisbursementBalanceColumn.ColumnName].ToString();
                this.PopulateDisbursementHistoryGridView();
            }
            else
            {
                this.ClearSubFundHeader();
            }
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <returns>pageStatus Bool</returns>
        private bool CheckPageStatus()
        {
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    bool status = this.SaveSubFundDetails();
                    if (status)
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    }

                    return status;
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.CancelButton_Click();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the year.
        /// </summary>
        private void GetYear()
        {
            try
            {
                CommentsData getYearDataSet = new CommentsData();
                getYearDataSet = this.form9503Controll.WorkItem.GetConfigDetails("TR_RollYear");
                this.RollYearTextBox.Text = getYearDataSet.GetCommentsConfigDetails.Rows[0][getYearDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString();
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Retrieves the index of the record.
        /// </summary>
        /// <param name="tempSubFundId">The temp sub fund id.</param>
        /// <returns>index of the current record</returns>
        private int RetrieveRecordIndex(int tempSubFundId)
        {
            int tempIndex = -1;

            try
            {
                DataTable tempDataTable = this.subFundMgmtDataSet.ListSubFundIds.Copy();
                tempDataTable.DefaultView.RowFilter = string.Concat(this.subFundMgmtDataSet.ListSubFundIds.SubFundIDColumn.ColumnName, " = ", tempSubFundId);

                if (tempDataTable.DefaultView.Count > 0)
                {
                    tempIndex = tempDataTable.Rows.IndexOf(tempDataTable.DefaultView[0].Row) + 1;
                }

                return tempIndex;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                return tempIndex;
            }
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.PermissionEdit)
            {
                if (!this.pageLoadStatus)   //// && this.pageMode == TerraScanCommon.PageModeTypes.View
                {
                    if (!string.Equals(this.pageMode, TerraScanCommon.PageModeTypes.Edit))
                    {
                        this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
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
            this.FundButton.Enabled = !lockValue;
            this.RollYearTextBox.LockKeyPress = lockValue;
            this.SubFundTextBox.LockKeyPress = lockValue;
            this.VoterApprovedComboBox.Enabled = !lockValue;
            this.TypeComboBox.Enabled = !lockValue;
            this.LevyRateTextBox.LockKeyPress = lockValue;
            this.CommissionRateTextBox.LockKeyPress = lockValue;
            this.AgencyButton.Enabled = !lockValue;
            this.FactorTextBox.LockKeyPress = lockValue;
            this.DescriptionTextBox.LockKeyPress = lockValue;
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
                    MessageBox.Show(SharedFunctions.GetResourceString("InvalidFieldYear") + " \n", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.RollYearTextBox.Focus();
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
        /// Validates the decimal text box.
        /// </summary>
        /// <param name="validateString">The validate string.</param>
        /// <param name="controlName">Name of the control.</param>
        /// <returns>Valid Status</returns>
        private bool ValidateDecimalTextBox(string validateString, string controlName)
        {
            string[] validString = new string[2];
            validString = validateString.Split('.');
            if (validString[0].Length > 3 || Convert.ToInt16(validString[0].ToString()) > 100)
            {
                MessageBox.Show(controlName + "value should not be greater that 100 %", ConfigurationWrapper.ApplicationName + " - " + "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else if (validString[0].ToString() == "100")
            {
                if (Convert.ToInt32(validString[1].Replace("%", "")) > 0)
                {
                    MessageBox.Show(controlName + "value should not be greater that 100 %", ConfigurationWrapper.ApplicationName + " - " + "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return true;
                }

                return false;
            }

            return false;
        }

        #endregion

        #region OperationSmartPart Methods

        /// <summary>
        /// News the button_ click.
        /// </summary>
        private void NewButton_Click()
        {
            ////TODO: New Button Functionality
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.pageLoadStatus = true;
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearSubFundHeader();
                this.GetYear();
                this.PopulateDisbursementHistoryGridView();
                this.SetActiveRecord(this, new DataEventArgs<int>(0));
                this.SetRecordCount(this, new DataEventArgs<int>(0));
                this.recordPointerArray[0] = 0;
                this.recordPointerArray[1] = 0;
                this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(this.recordPointerArray));
                this.AddtionalOperationDeckWorkspace.Enabled = false;
                this.subFundId = null;
                this.SubFundManagementAuditLink.Text = SharedFunctions.GetResourceString("9503SubFundManagementAuditLink");
                this.LockControls(false);
                this.SubFundManagementAuditLink.Enabled = false;
                this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
                this.pageLoadStatus = false;
                this.FundButton.Focus();
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
        /// Saves the button_ click.
        /// </summary>
        private void SaveButton_Click()
        {
            this.SaveSubFundDetails();   
        }

        /// <summary>
        /// Saves the sub fund details.
        /// </summary>
        /// <returns>the status</returns>
        private bool SaveSubFundDetails()
        {
            Control requiredControl;

            requiredControl = this.CheckRequiredFields();

            if (requiredControl != null)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredFieldMissing"), ConfigurationWrapper.ApplicationName + " - " + SharedFunctions.GetResourceString("RequiredFieldMissingTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                requiredControl.Focus();
                return false;
            }
            else
            {
                try
                {
                    int returnValue = -1;
                    this.Cursor = Cursors.WaitCursor;
                    if (!this.ValidateRollYear())
                    {
                        F9503SubFundManagementData saveSubFundMgmtDataSet = new F9503SubFundManagementData();
                        F9503SubFundManagementData.SubFundDetailsRow dr = saveSubFundMgmtDataSet.SubFundDetails.NewSubFundDetailsRow();
                        if (this.FundId.HasValue)
                        {
                            dr.FundID = Convert.ToInt32(this.FundId);
                        }

                        dr.RollYear = Convert.ToInt16(this.RollYearTextBox.Text.Trim());
                        dr.SubFund = this.SubFundTextBox.Text.Trim();
                        dr.Description = this.DescriptionTextBox.Text.Trim();
                        dr.Rate = this.LevyRateTextBox.DecimalTextBoxValue;
                        dr.IsVoterApproved = Convert.ToBoolean(Convert.ToInt32(this.VoterApprovedComboBox.SelectedValue.ToString()));
                        dr.CommissionRate = this.CommissionRateTextBox.DecimalTextBoxValue;
                        dr.IsCash = false;
                        dr.Factor = this.FactorTextBox.DecimalTextBoxValue;
                        dr.SubFundTypeID = Convert.ToByte(this.TypeComboBox.SelectedValue);
                        if (this.AgencyId.HasValue)
                        {
                            dr.AgencyID = Convert.ToInt32(this.AgencyId);
                        }

                        saveSubFundMgmtDataSet.SubFundDetails.Rows.Add(dr);
                        DataSet tempDataSet = new DataSet("Root");
                        tempDataSet.Tables.Add(saveSubFundMgmtDataSet.SubFundDetails.Copy());
                        tempDataSet.Tables[0].TableName = "Table";
                        string tempxml = string.Empty;
                        tempxml = tempDataSet.GetXml();
                        returnValue = this.form9503Controll.WorkItem.F9503_CreateOrEditSubFund(this.SubFundIdentity, tempxml,TerraScanCommon.UserId );
                        if (returnValue != -1)
                        {
                            ////this.SubFundID = returnValue;
                            ////this.form9503Controll.WorkItem.F9503_GetSubFundManagementDetails(this.SubFundID);
                            int recordIndex = -1;
                            this.subFundMgmtDataSet = this.form9503Controll.WorkItem.F9503_GetSubFundManagementDetails(null);
                            this.SubFundIdentity = returnValue;
                            if (this.SubFundIdentity.HasValue)
                            {
                                recordIndex = this.RetrieveRecordIndex(Convert.ToInt32(this.SubFundIdentity));
                            }

                            if (recordIndex != -1)
                            {
                                this.PopulateSubFundDetails(this.SubFundIdentity, recordIndex);
                            }
                            else
                            {
                                this.PopulateSubFundDetails(null, recordIndex);
                            }

                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                            this.SetButtons(TerraScanCommon.ButtonActionMode.SaveMode);
                            if (!this.PermissionEdit)
                            {
                                this.LockControls(true);
                            }

                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Updated Process Failed, please check the data u have entered", ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (SoapException ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                    return false;
                }
                catch (Exception ex1)
                {
                    ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                    return false;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Checks the required fields.
        /// </summary>
        /// <returns>the Reqried Fields String</returns>
        private Control CheckRequiredFields()
        {
            Control requiredControll = null;
            if (string.IsNullOrEmpty(this.FundLinkLabel.Text.Trim()))
            {
                requiredControll = this.FundButton;
            }
            else if (string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                requiredControll = this.RollYearTextBox;
            }
            else if (string.IsNullOrEmpty(this.SubFundTextBox.Text.Trim()))
            {
                requiredControll = this.SubFundTextBox;
            }
            else if (string.IsNullOrEmpty(this.LevyRateTextBox.Text.Trim()))
            {
                requiredControll = this.LevyRateTextBox;
            }

            return requiredControll;
        }

        /// <summary>
        /// Cancels the button_ click.
        /// </summary>
        private void CancelButton_Click()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.PopulateSubFundDetails(this.SubFundIdentity, this.currentRecord);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                this.SubFundManagementAuditLink.Enabled = true;
                if (!this.PermissionEdit)
                {
                    this.LockControls(true);
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

        #endregion

        #region RecordNavigation Methods

        #endregion

        #region AttachmentComments Methods

        /// <summary>
        /// Sets the attachment comments count.
        /// </summary>
        private void SetAttachmentCommentsCount()
        {
            ////Display Comments Count in the Comments Buttons  and attachment count in the attachment Buttons       
            try
            {
                AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);

                if (this.SubFundIdentity != -999)
                {
                    this.additionalOperationSmartPart.KeyId = Convert.ToInt32(this.SubFundIdentity);
                    additionalOperationCountEntity.AttachmentCount = this.form9503Controll.WorkItem.GetAttachmentCount(this.ParentFormId, Convert.ToInt32(this.SubFundIdentity), TerraScanCommon.UserId);
                    CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.form9503Controll.WorkItem.GetCommentsCount(this.ParentFormId, Convert.ToInt32(this.SubFundIdentity), TerraScanCommon.UserId);
                    if (commentsCountDataTable.Rows.Count > 0)
                    {
                        additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                        additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                    }
                }

                this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex1)
            {
                ////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #endregion

        #region FormLoad

        /// <summary>
        /// Handles the Load event of the F9503 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9503_Load(object sender, EventArgs e)
        {
            try
            {
            this.pageLoadStatus = true;
            this.LoadWorkSpaces();
            this.InitVoterApprovedComboBox();
            //// TODO:: Need to Get Confirmation
            ////this.SetTextBoxMaxLenght();
            this.PopulateSubFundDetails(null, -1);
            if (!this.PermissionEdit)
            {
                this.LockControls(true);
            }

            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.pageLoadStatus = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }    
        }

        /// <summary>
        /// Method to Load All SmartParts in UI WorkSpaces
        /// </summary>
        private void LoadWorkSpaces()
        {
            ////Load FormHeaderSmartPart to FormHeaderDeckWorkspace
            if (this.Form9503Controll.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.FormHeaderDeckWorkspace.Show(this.Form9503Controll.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.FormHeaderDeckWorkspace.Show(this.Form9503Controll.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            ////sets form header
            this.SetFormHeader(this, new DataEventArgs<string[]>(new string[] { SharedFunctions.GetResourceString("9503SubFundManagement"), string.Empty }));

            ////Load RecordNavigatorSmartPart to RecordNavigatorSmartPartdeckWorkspace
            if (this.Form9503Controll.WorkItem.SmartParts.Contains(SmartPartNames.RecordNavigatorSmartPart))
            {
                this.RecordNavigatorDeckWorkspace.Show(this.Form9503Controll.WorkItem.SmartParts.Get(SmartPartNames.RecordNavigatorSmartPart));
            }
            else
            {
                this.RecordNavigatorDeckWorkspace.Show(this.Form9503Controll.WorkItem.SmartParts.AddNew<RecordNavigatorSmartPart>(SmartPartNames.RecordNavigatorSmartPart));
            }

            ////Load StatusBarSmartPart to StatusBarSmartPartdeckWorkspace
            if (this.Form9503Controll.WorkItem.SmartParts.Contains(SmartPartNames.StatusBarSmartPart))
            {
                this.statusBarSmartPart = (StatusBarSmartPart)this.Form9503Controll.WorkItem.SmartParts.Get(SmartPartNames.StatusBarSmartPart);
            }
            else
            {
                this.statusBarSmartPart = this.Form9503Controll.WorkItem.SmartParts.AddNew<StatusBarSmartPart>(SmartPartNames.StatusBarSmartPart);
            }

            this.StatusBarDeckWorkspace.Show(this.statusBarSmartPart);

            // To Load AdditionalOperationSmartPart into CommentsdeckWorkspace
            if (this.Form9503Controll.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.Form9503Controll.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart);
            }
            else
            {
                this.additionalOperationSmartPart = this.Form9503Controll.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart);
            }

            this.AddtionalOperationDeckWorkspace.Show(this.additionalOperationSmartPart);

            // To Load OperationSmartPart into operationSmartPartWorkSpace
            if (this.Form9503Controll.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
            {
                this.operationSmartPart = (OperationSmartPart)this.Form9503Controll.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart);
            }
            else
            {
                this.operationSmartPart = this.Form9503Controll.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart);
            }

            this.operationSmartPartWorkSpace.Show(this.operationSmartPart);

            ////set required variable - attachment and comment
            this.additionalOperationSmartPart.ParentWorkItem = this.Form9503Controll.WorkItem;
            this.additionalOperationSmartPart.ParentFormId = this.ParentFormId;
            this.additionalOperationSmartPart.CurrntFormId = this.ParentFormId;
            ////set enable/visible property to the controls
            this.operationSmartPart.DeleteButtonVisible = false;
            this.statusBarSmartPart.VisibleDelinquentButton = false;
            this.statusBarSmartPart.VisibleAutoPrintButton = false;
        }

        #endregion
    }
}
