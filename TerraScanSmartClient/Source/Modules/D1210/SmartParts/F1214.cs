//--------------------------------------------------------------------------------------------
// <copyright file="F1214.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F1210 Refund Management Functionality
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11-10-2006       Krishna Abburi       Created
// 3-2-2007         ranjani              1213 - 8.2, 8.3, 8.4, 8.5 - issues fixed
//*********************************************************************************/
namespace D1210
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

    /// <summary>
    /// F1214 refund Management User InterFace
    /// </summary>
    [SmartPart]
    public partial class F1214 : BaseSmartPart
    {
        #region Private Variables

        /// <summary>
        /// Variable Instance for F1214Controller
        /// </summary>
        private F1214Controller form1214Controll;

        /// <summary>
        /// formLabelInfo string array
        /// </summary>
        private string[] formLabelInfo = new string[2];

        /// <summary>
        ///  Used To Store ownerId
        /// </summary>
        private int ownerId;

        /// <summary>
        /// PartiesOwnerDetailsData
        /// </summary>
        private PartiesOwnerDetailsData ownerDetailDataSet = new PartiesOwnerDetailsData();

        /// <summary>
        /// RefundManagementData
        /// </summary>
        private RefundManagementData refundManagementDataSet;

        /// <summary>
        /// emptyString
        /// </summary>
        private StringBuilder emptyString = new StringBuilder();

        /// <summary>
        /// reportOptionalParameter
        /// </summary>
        private Hashtable reportFileIdHashTable = new Hashtable();

        /// <summary>
        /// register Id
        /// </summary>
        private int registerId = -1;

        /// <summary>
        /// Total Record Count
        /// </summary>
        private int totalRecCount;

        /// <summary>
        /// Selected Record Count
        /// </summary>
        private int selectedRecCount;

        /// <summary>
        /// Total Payment Amount
        /// </summary>
        private decimal totalPaymentAmount;

        /// <summary>
        /// Selected Payment Amount
        /// </summary>
        private decimal selectedPaymentAmount;

        /// <summary>
        /// Account Table Row Count
        /// </summary>
        private int accountRowCount;

        /// <summary>
        /// Binding Source
        /// </summary>
        private BindingSource bindingSource;

        /// <summary>
        /// interestDate
        /// </summary>
        private DateTime interestDate = DateTime.Today.Date;

        /// <summary>
        /// All Interest Amounts
        /// </summary>
        private Decimal allInterestAmounts;

        /// <summary>
        /// CurrentChecked InterestAmount
        /// </summary>
        private int currentCheckInterestAmount;

        /// <summary>
        /// check whether the record set - reduced or not
        /// </summary>
        private bool reducedRecordSet;

        /// <summary>
        /// isShift
        /// </summary>
        private bool isShift;

        /// <summary>
        /// isShifts
        /// </summary>
        private bool isShifts;

        /// <summary>
        /// tempDataRow for AgencyId
        /// </summary>
        private DataRow tempPaymentItemsDataRow;

        /// <summary>
        /// tempDataTable for Get AgencyIds
        /// </summary>
        private DataTable tempPaymentItemsDataTable;

        /// <summary>
        /// array which contains comparision operators
        /// </summary>
        private String[] comparisionOperatorsCombinations = new String[] { "=", ">=", "<=", ">", "<" };

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1214"/> class.
        /// </summary>
        public F1214()
        {
            InitializeComponent();
            this.AgencyHeaderPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AgencyHeaderPictureBox.Height, this.AgencyHeaderPictureBox.Width, "", 0, 51, 0);
            this.DepositHistoryPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DepositHistoryPictureBox.Height, this.DepositHistoryPictureBox.Width, "Available Payment Items", 28, 81, 128);
        }

        #endregion

        #region Published Events

        /// <summary>
        /// EventPublication for FormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// event publication for Show the child form 
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form1214 controll.
        /// </summary>
        /// <value>The form1214 controll.</value>
        [CreateNew]
        public F1214Controller Form1214Controll
        {
            get { return this.form1214Controll as F1214Controller; }
            set { this.form1214Controll = value; }
        }

        /// <summary>
        /// Gets or sets the owner id.
        /// </summary>
        /// <value>The owner id.</value>
        public int OwnerId
        {
            get { return this.ownerId; }
            set { this.ownerId = value; }
        }

        /// <summary>
        /// Gets or sets the post date.
        /// </summary>
        /// <value>The post date.</value>
        public DateTime InterestDate
        {
            get { return this.interestDate; }
            set { this.interestDate = value; }
        }

        /// <summary>
        /// Gets or sets the selected rec count.
        /// </summary>
        /// <value>The selected rec count.</value>
        public int SelectedRecCount
        {
            get { return this.selectedRecCount; }
            set { this.selectedRecCount = value; }
        }

        /// <summary>
        /// Gets or sets the total rec count.
        /// </summary>
        /// <value>The total rec count.</value>
        public int TotalRecCount
        {
            get { return this.totalRecCount; }
            set { this.totalRecCount = value; }
        }

        /// <summary>
        /// Gets or sets all interest amounts.
        /// </summary>
        /// <value>All interest amounts.</value>
        public decimal AllInterestAmounts
        {
            get { return this.allInterestAmounts; }
            set { this.allInterestAmounts = value; }
        }

        /// <summary>
        /// Gets or sets the current check interest amount.
        /// </summary>
        /// <value>The current check interest amount.</value>
        public int CurrentCheckInterestAmount
        {
            get { return this.currentCheckInterestAmount; }
            set { this.currentCheckInterestAmount = value; }
        }

        /// <summary>
        /// Gets or sets the total payment amount.
        /// </summary>
        /// <value>The total payment amount.</value>
        public decimal TotalPaymentAmount
        {
            get { return this.totalPaymentAmount; }
            set { this.totalPaymentAmount = value; }
        }

        /// <summary>
        /// Gets or sets the selected payment amount.
        /// </summary>
        /// <value>The selected payment amount.</value>
        public decimal SelectedPaymentAmount
        {
            get { return this.selectedPaymentAmount; }
            set { this.selectedPaymentAmount = value; }
        }

        /// <summary>
        /// Gets or sets the register id.
        /// </summary>
        /// <value>The register id.</value>
        public int RegisterId
        {
            get { return this.registerId; }
            set { this.registerId = value; }
        }

        /// <summary>
        /// Gets or sets the account row count.
        /// </summary>
        /// <value>The account row count.</value>
        public int AccountRowCount
        {
            get { return this.accountRowCount; }
            set { this.accountRowCount = value; }
        }

        #endregion

        #region Form Events

        /// <summary>
        /// Handles the Load event of the F1214 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1214_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkSpaces();
                this.InterestDateTextBox.Text = this.InterestDate.ToShortDateString();
                this.PopulateRefundPaymentGrid(this.emptyString, true);
                this.InitAccountNameCombo();
                this.DisableControls();
                // SuspendesOwnerTextBox.Focus();
                this.ParentForm.ActiveControl = this.AccountNameComboBox;
                AccountNameComboBox.Focus();
                //panel3.Focus();

                PaymentDateTextBox.Focus();
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
        /// Handles the Click event of the PictureParcel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PictureParcel_Click(object sender, EventArgs e)
        {
            Form parcelF9101 = new Form();
            parcelF9101 = this.form1214Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9101, null, this.Form1214Controll.WorkItem);

            if (parcelF9101 != null)
            {
                if (parcelF9101.ShowDialog() == DialogResult.Yes)
                {
                    try
                    {
                        this.OwnerId = Convert.ToInt32(TerraScanCommon.GetValue(parcelF9101, "MasterNameOwnerId"));
                        this.ownerDetailDataSet = this.Form1214Controll.WorkItem.GetOwnerDetails(this.OwnerId);
                        this.SuspendesOwnerTextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.NameColumn].ToString();
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the PaymentItemsGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void RefundItemsDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.RefundItemsDataGrid.Columns["RefundItemCheck"].Index)
                {
                    this.RefundItemsDataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    this.refundManagementDataSet.ListRefundPayments.AcceptChanges();
                    this.SetGridSummeries();
                }
            }
            catch (DataException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.GetWhereClause();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.PaymentDateTextBox.Text = "";
                this.PaidBYTextBox.Text = "";
                this.CheckNoTextBox.Text = "";
                this.UserTextBox.Text = "";
                this.AmountTextBox.Text = "";
                this.PopulateRefundPaymentGrid(this.emptyString, true);
                this.ClearButton.Enabled = false;
                this.reducedRecordSet = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Edits the text.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EditText(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(this.PaymentDateTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.PaidBYTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.CheckNoTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.UserTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.AmountTextBox.Text.Trim())))
                {
                    this.EnableControls();
                }
                else
                {
                    this.DisableControls();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the RefundItemsDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void RefundItemsDataGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.RefundManagementAuditLink.Text = SharedFunctions.GetResourceString("PaymentIDLink") + " " + this.RefundItemsDataGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the RefundItemsDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RefundItemsDataGrid_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.RefundItemsDataGrid.CurrentCell != null)
                {
                    this.RefundItemsDataGrid.CurrentCell = null;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the RefundManagementAuditLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void RefundManagementAuditLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                int reportAuditId = 0;
                reportAuditId = Convert.ToInt32(this.RefundItemsDataGrid.Rows[0].Cells[0].Value.ToString());
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(90101);
                formInfo.optionalParameters = new object[2];
                formInfo.optionalParameters[0] = this.Tag;
                formInfo.optionalParameters[1] = reportAuditId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));

                ////this.Cursor = Cursors.WaitCursor;
                ////reportAuditId = Convert.ToInt32(this.RefundItemsDataGrid.Rows[0].Cells[0].Value.ToString());
                ////this.reportFileIdHashTable.Clear();
                ////this.reportFileIdHashTable.Add("ReportFileID", reportAuditId);

                //////// Shows the report form.
                ////////changed the parameter type from string to int
                ////TerraScanCommon.ShowReport(101890, TerraScan.Common.Reports.Report.ReportType.Preview, this.reportFileIdHashTable);
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
        /// Handles the Click event of the ReportButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReportButton_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable userTabReport = new Hashtable();
                userTabReport.Add("ReportNumber", "121201");
                userTabReport.Add("UserID", TerraScanCommon.UserId);
                ////changed the parameter type from string to int
                TerraScanCommon.ShowReport(121201, TerraScan.Common.Reports.Report.ReportType.Preview, userTabReport);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the InterestDateCalenderButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InterestDateCalenderButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Calls the method to show the calender control.
                this.ShowInterestDateCalender();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the InterestDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void InterestDateCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.InterestDateTextBox.Text = e.Start.ToShortDateString();
                this.InterestDate = e.Start;
                this.InterestDateTextBox.Focus();
                this.InterestDateCalender.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the InterestDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void InterestDateCalender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    this.InterestDateTextBox.Text = this.InterestDateCalender.SelectionStart.ToShortDateString();
                    this.InterestDateCalender.Visible = false;
                    this.InterestDateTextBox.Focus();
                }
                isShift = e.Shift;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SelectAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < this.RefundItemsDataGrid.OriginalRowCount; i++)
                {
                    this.RefundItemsDataGrid.Rows[i].Cells["RefundItemCheck"].Value = "True";
                }

                this.RefundItemsDataGrid.RefreshEdit();
                this.SetGridSummeries();
                AccountNameComboBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the UnSelectAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UnSelectAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < this.RefundItemsDataGrid.OriginalRowCount; i++)
                {
                    this.RefundItemsDataGrid.Rows[i].Cells["RefundItemCheck"].Value = "False";
                }

                this.RefundItemsDataGrid.RefreshEdit();
                this.SetGridSummeries();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellParsing event of the RefundItemsDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void RefundItemsDataGrid_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                Decimal outDecimal;
                ////Int64 outInt;

                //// Only paint if desired column

                if (e.ColumnIndex == this.RefundItemsDataGrid.Columns["Interest"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    //// Only paint if text provided, Only paint if desired text is in cell

                    if (e.Value != null)
                    {
                        string tempvalue = e.Value.ToString().Trim();
                        Decimal.TryParse(tempvalue, System.Globalization.NumberStyles.Currency, null, out outDecimal);
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
        /// Handles the CellFormatting event of the RefundItemsDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void RefundItemsDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                Decimal outDecimal;

                if (e.ColumnIndex == this.RefundItemsDataGrid.Columns["Interest"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !String.IsNullOrEmpty(this.RefundItemsDataGrid.Rows[e.RowIndex].Cells["Interest"].Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            if (outDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                                e.CellStyle.ForeColor = Color.FromArgb(0, 128, 0);

                                //// e.CellStyle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            }
                            else
                            {
                                e.Value = outDecimal.ToString("#,##0.00");
                                e.FormattingApplied = true;
                            }
                        }
                        else
                        {
                            e.Value = "0.00";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }

                if (e.ColumnIndex == this.RefundItemsDataGrid.Columns["Amount"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.RefundItemsDataGrid.Rows[e.RowIndex].Cells["Amount"].Value.ToString().Trim()))
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
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the AccountNameComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AccountNameComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(this.AccountNameComboBox.SelectedValue) != 0)
                {
                    this.RegisterId = Convert.ToInt32(this.AccountNameComboBox.SelectedValue);
                }
                else
                {
                    this.RegisterId = -1;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the PrepareChecksButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PrepareChecksButton_Click(object sender, EventArgs e)
        {
            try
            {
                string paymentItemsXML = string.Empty;
                int returnValue;
                paymentItemsXML = this.GetPaymentItems();
                if (!string.IsNullOrEmpty(paymentItemsXML.Trim()) && this.registerId != -1)
                {
                    returnValue = this.Form1214Controll.WorkItem.F1214_PrepareChecks(this.RegisterId, this.OwnerId, this.InterestDate, TerraScanCommon.UserId, paymentItemsXML);
                    if (returnValue > 0)
                    {
                        ////ErrorList Form
                        Form errorListingForm = this.form1214Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1206, null, this.Form1214Controll.WorkItem);
                        if (errorListingForm != null)
                        {
                            errorListingForm.ShowDialog();
                        }
                    }
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

            this.PopulateRefundPaymentGrid(this.emptyString, true);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            if (this.form1214Controll.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1214Controll.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1214Controll.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            this.formLabelInfo[0] = SharedFunctions.GetResourceString("1214RefundManagement"); ////Properties.Resources.FormName;
            this.formLabelInfo[1] = string.Empty;
            this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));
        }

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizeDataGrid()
        {
            this.RefundItemsDataGrid.AllowUserToResizeColumns = false;
            this.RefundItemsDataGrid.AutoGenerateColumns = false;
            this.RefundItemsDataGrid.AllowUserToResizeRows = false;
            this.RefundItemsDataGrid.StandardTab = true;
            ////this.RefundItemsDataGrid.Columns[4].Resizable = DataGridViewTriState.False;
            this.RefundItemsDataGrid.Columns["Interest"].DefaultCellStyle.Font = new Font("Courier New", 8.25F, FontStyle.Bold);
            this.RefundItemsDataGrid.Columns["Interest"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.RefundItemsDataGrid.Columns["Amount"].DefaultCellStyle.Font = new Font("Courier New", 8.25F, FontStyle.Bold);
            this.RefundItemsDataGrid.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.RefundItemsDataGrid.Columns[0].DataPropertyName = "PaymentID";
            this.RefundItemsDataGrid.Columns[1].DataPropertyName = "PPaymentID";
            this.RefundItemsDataGrid.Columns[2].DataPropertyName = "UserID";
            this.RefundItemsDataGrid.Columns[3].DataPropertyName = "RefundItemCheck";
            this.RefundItemsDataGrid.Columns[4].DataPropertyName = "PaymentDate";
            this.RefundItemsDataGrid.Columns[5].DataPropertyName = "PaidBy";
            this.RefundItemsDataGrid.Columns[6].DataPropertyName = "CheckNumber";
            this.RefundItemsDataGrid.Columns[7].DataPropertyName = "Name_Display";
            this.RefundItemsDataGrid.Columns[8].DataPropertyName = "Interest";
            this.RefundItemsDataGrid.Columns[9].DataPropertyName = "Amount";
            this.RefundItemsDataGrid.Columns[10].DataPropertyName = "TenderTypeID";
            this.RefundItemsDataGrid.Columns[11].DataPropertyName = "ModuleID";
            this.RefundItemsDataGrid.Columns[12].DataPropertyName = "OwnerID";
        }

        /// <summary>
        /// Inits the account name combo.
        /// </summary>
        private void InitAccountNameCombo()
        {
            RefundManagementData.ListAccountNamesDataTable listAccountNames = new RefundManagementData.ListAccountNamesDataTable();

            listAccountNames = this.Form1214Controll.WorkItem.F1214_AccountNames();

            if (listAccountNames.Rows.Count > 0)
            {
                this.AccountNameComboBox.ValueMember = this.refundManagementDataSet.ListAccountNames.RegisterIDColumn.ColumnName;
                this.AccountNameComboBox.DisplayMember = this.refundManagementDataSet.ListAccountNames.AccountNameColumn.ColumnName;
                this.AccountNameComboBox.DataSource = listAccountNames;

                DataRow[] dataRow;
                bool defaultValue = true;
                string findExp = "IsDefault =" + defaultValue.ToString();
                dataRow = listAccountNames.Select(findExp);

                if (dataRow.Length > 0)
                {
                    int rowIndex = listAccountNames.Rows.IndexOf(dataRow[0]);
                    this.RegisterId = Convert.ToInt32(dataRow[0][this.refundManagementDataSet.ListAccountNames.RegisterIDColumn.ColumnName.ToString()]);
                    this.AccountNameComboBox.SelectedIndex = rowIndex;
                }
                else
                {
                    this.AccountNameComboBox.SelectedIndex = -1;
                }

                this.PrepareChecksButton.Enabled = true && this.PermissionFiled.newPermission;
            }
            else
            {
                this.PrepareChecksButton.Enabled = false;
            }
            AccountNameComboBox.Focus();
        }

        /// <summary>
        /// Gets the where clause.
        /// </summary>
        private void GetWhereClause()
        {
            ////set reducedRecordSet
            this.reducedRecordSet = true;
            string returnValue = String.Empty;
            StringBuilder whereClause = new StringBuilder(String.Empty);
            bool previousValueExists = false;
            bool loadRefundPayment = true;

            Control[] controlArray = new Control[] { this.PaymentDateTextBox, this.PaidBYTextBox, this.CheckNoTextBox, this.UserTextBox, this.AmountTextBox };

            for (int i = 0; i < controlArray.Length; i++)
            {
                Control queryControl = controlArray.GetValue(i) as Control;

                if (!String.IsNullOrEmpty(queryControl.Text.Trim()))
                {
                    if (string.Equals(queryControl.Name, this.PaymentDateTextBox.Name) || string.Equals(queryControl.Name, this.AmountTextBox.Name))
                    {
                        ////get field data type
                        Type fieldDataType = this.refundManagementDataSet.ListRefundPayments.Columns[queryControl.Tag.ToString()].DataType;
                        returnValue = queryControl.Text.Trim().ToUpper();
                        ////return true if validation succeed
                        if (this.FormatSqlWhereConditions(ref returnValue, fieldDataType))
                        {
                            //// ParseSqlWhereCondition returns string containing parsed query value 

                            if (!String.IsNullOrEmpty(returnValue))
                            {
                                if (previousValueExists)
                                {
                                    whereClause.Append(" AND ");
                                }

                                previousValueExists = true;
                                whereClause.Append("(");
                                if (fieldDataType.Equals(typeof(DateTime)))
                                {
                                    ////format datatype for comparison
                                    whereClause.Append(string.Concat(new string[] { "CAST(CONVERT(VARCHAR(10), ", queryControl.Tag.ToString(), ", 101)AS SMALLDATETIME)", " ", returnValue }));
                                }
                                else
                                {
                                    whereClause.Append(string.Concat(queryControl.Tag.ToString(), " ", returnValue));
                                }
                                ////whereClause.Append(returnValue);                        
                                whereClause.Append(")");
                            }
                        }
                        else
                        {
                            ////clear record set when validation fails
                            loadRefundPayment = false;
                            break;
                        }
                    }
                    else
                    {
                        if (previousValueExists)
                        {
                            whereClause.Append(" AND ");
                        }

                        previousValueExists = true;
                        whereClause.Append("(");
                        whereClause.Append(string.Concat(queryControl.Tag.ToString(), " LIKE '%", queryControl.Text.Trim(), "%'"));
                        ////whereClause.Append(returnValue);                        
                        whereClause.Append(")");
                    }
                }
            }

            ////populate record set
            this.PopulateRefundPaymentGrid(whereClause, loadRefundPayment);
        }

        /// <summary>
        /// Formats the SQL where conditions.
        /// </summary>
        /// <param name="inputValue">The input value.</param>
        /// <param name="fieldDataType">Type of the field data.</param>
        /// <returns>the bool - true if validation succeed else false to clear record set</returns>
        private bool FormatSqlWhereConditions(ref string inputValue, Type fieldDataType)
        {
            string returnValue = inputValue;
            string comparisionOperator = string.Empty;

            ////used to separate operator and value
            for (int i = 0; i < this.comparisionOperatorsCombinations.Length; i++)
            {
                comparisionOperator = this.comparisionOperatorsCombinations[i];
                if (returnValue.StartsWith(comparisionOperator))
                {
                    returnValue = returnValue.Remove(0, comparisionOperator.Length);
                    break;
                }
                else
                {
                    comparisionOperator = string.Empty;
                }
            }

            ////validate the input value
            if (fieldDataType.Equals(typeof(decimal)))
            {
                ////validation required for 8.4 change request
                decimal tempDecimalValue;
                if (decimal.TryParse(returnValue, System.Globalization.NumberStyles.Currency, null, out tempDecimalValue))
                {
                    returnValue = tempDecimalValue.ToString();
                }
                else
                {
                    return false;
                }
            }
            else if (fieldDataType.Equals(typeof(DateTime)))
            {
                ////validation required for formatting date - 8.3 issue
                DateTime tempDateTime;
                if (DateTime.TryParse(returnValue, out tempDateTime))
                {
                    returnValue = tempDateTime.ToString("MM/dd/yyyy");
                }
                else
                {
                    return false;
                }
            }

            ////check for existence of comparisionOperator
            if (string.IsNullOrEmpty(comparisionOperator))
            {
                inputValue = string.Concat("= '", returnValue, "'");
            }
            else
            {
                inputValue = string.Concat(comparisionOperator, " '", returnValue, "'");
            }

            return true;
        }

        /// <summary>
        /// Populates the refund payment grid.
        /// </summary>
        /// <param name="whereClause">The where clause.</param>
        /// <param name="loadRefundPayment">if set to <c>true</c> [load refund payment] else clear record set.</param>
        private void PopulateRefundPaymentGrid(StringBuilder whereClause, bool loadRefundPayment)
        {
            AccountNameComboBox.Focus();
            int recordsCount = 0;
            this.accountRowCount = 0;
            this.refundManagementDataSet = new RefundManagementData();
            this.bindingSource = new BindingSource();
            this.CustomizeDataGrid();
            ////this.refundManagementDataSet.ListRefundPayments.Rows.Clear();
            ////true to load with where string
            if (loadRefundPayment)
            {
                this.refundManagementDataSet.Merge(this.Form1214Controll.WorkItem.ListRefundPayments(1214, whereClause.ToString()));
            }
            else
            {
                this.refundManagementDataSet.ListRefundPayments.Clear();
            }

            this.AddCheckBoxColumn();
            recordsCount = this.refundManagementDataSet.ListRefundPayments.Rows.Count;
            this.RefundItemsDataGrid.DataSource = this.refundManagementDataSet.ListRefundPayments;
            this.bindingSource.DataSource = this.refundManagementDataSet.ListRefundPayments.Copy();
            this.AccountRowCount = recordsCount;
            this.SetGridSummeries();

            if (recordsCount > 0)
            {
                ////this.WorkQueueAuditLink.Text = SharedFunctions.GetResourceString("1107WorkQueueAuditLink") + " " + this.AffidavitListDataGrid.Rows[0].Cells[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.StatementIDColumn.ToString()].Value.ToString();
                ////this.EnableButtons(this, new DataEventArgs<string>("0"));
                this.RefundItemsDataGrid.Enabled = true;
                TerraScanCommon.SetDataGridViewPosition(this.RefundItemsDataGrid, 0);
                this.RefundItemsDataGrid.Focus();
                this.RefundManagementAuditLink.Enabled = true;
                this.RefundManagementAuditLink.Text = SharedFunctions.GetResourceString("PaymentIDLink") + " " + this.RefundItemsDataGrid.Rows[0].Cells[0].Value.ToString();
            }
            else
            {
                //// this.DisableButtons(this, new DataEventArgs<string>("0"));
                this.RefundItemsDataGrid.Rows[0].Selected = false;
                this.RefundItemsDataGrid.Enabled = false;
                RefundManagementAuditLink.Enabled = false;
                this.RefundManagementAuditLink.Text = SharedFunctions.GetResourceString("PaymentIDLink") + " ";
            }

            this.DisableVScrollBar(recordsCount);
        }

        /// <summary>
        /// Disables the V scroll bar.
        /// </summary>
        /// <param name="recordsCount">The records count.</param>
        private void DisableVScrollBar(int recordsCount)
        {
            if (recordsCount > 18)
            {
                this.RefundVScrollBar.Enabled = true;
                this.RefundVScrollBar.Visible = false;
            }
            else
            {
                this.RefundVScrollBar.Enabled = false;
                this.RefundVScrollBar.Visible = true;
            }
        }

        /// <summary>
        /// Enables the controls.
        /// </summary>
        private void EnableControls()
        {
            this.SearchButton.Enabled = true;
            this.ClearButton.Enabled = true;
        }

        /// <summary>
        /// Disables the controls.
        /// </summary>
        private void DisableControls()
        {
            this.SearchButton.Enabled = false;
            ////disabled if already cleared
            if (!this.reducedRecordSet)
            {
                this.ClearButton.Enabled = false;
            }

            this.CalcInterestButton.Enabled = false;
            this.SuspendPaymentButton.Enabled = false;
            this.PrepareChecksButton.Enabled = false;
        }

        /// <summary>
        /// Sets the grid summeries.
        /// </summary>
        private void SetGridSummeries()
        {
            this.selectedRecCount = 0;
            this.selectedPaymentAmount = 0;
            this.totalPaymentAmount = 0;
            this.TotalRecCount = this.accountRowCount;
            for (int i = 0; i < this.refundManagementDataSet.ListRefundPayments.Rows.Count; i++)
            {
                if (this.refundManagementDataSet.ListRefundPayments.Rows[i]["RefundItemCheck"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(this.refundManagementDataSet.ListRefundPayments.Rows[i]["RefundItemCheck"]))
                    ////if (this.RefundItemsDataGrid.Rows[i].Cells[3].Value.ToString() == "True")
                    {
                        this.selectedRecCount += 1;
                        this.selectedPaymentAmount += Convert.ToDecimal(this.refundManagementDataSet.ListRefundPayments.Rows[i]["Amount"]);
                    }

                    this.totalPaymentAmount += Convert.ToDecimal(this.refundManagementDataSet.ListRefundPayments.Rows[i]["Amount"]);
                }
            }

            RecordCountLabel.Text = this.SelectedRecCount + "  /  " + this.TotalRecCount;
            this.SelectedAmountTextBox.Text = this.SelectedPaymentAmount.ToString();
            this.totalAmountTextBox.Text = this.TotalPaymentAmount.ToString();
            if (this.SelectedRecCount > 0 && this.PermissionFiled.newPermission && this.accountRowCount > 0)
            {
                this.PrepareChecksButton.Enabled = true && this.PermissionFiled.newPermission;
            }
            else
            {
                this.PrepareChecksButton.Enabled = false;
            }

            this.allInterestAmounts = 0;
            this.selectedPaymentAmount = 0;

            for (int i = 0; i < this.RefundItemsDataGrid.OriginalRowCount; i++)
            {
                if (this.RefundItemsDataGrid.Rows[i].Cells[8].Value != null)
                {
                    ////this.allInterestAmounts = this.allInterestAmounts + Convert.ToDecimal(this.RefundItemsDataGrid.Rows[i].Cells[8].Value.ToString());
                    this.allInterestAmounts += Convert.ToDecimal(this.refundManagementDataSet.ListRefundPayments.Rows[i]["Interest"]);

                    if (this.RefundItemsDataGrid.Rows[i].Cells[3].Value.ToString() == "True")
                    {
                        // this.selectedPaymentAmount = this.selectedPaymentAmount + Convert.ToDecimal(this.RefundItemsDataGrid.Rows[i].Cells[8].Value.ToString());
                        this.selectedPaymentAmount += Convert.ToDecimal(this.refundManagementDataSet.ListRefundPayments.Rows[i]["Interest"]);
                    }
                }
            }
        }

        /// <summary>
        /// Adds the check box column.
        /// </summary>
        private void AddCheckBoxColumn()
        {
            this.refundManagementDataSet.ListRefundPayments.Columns.Add("RefundItemCheck", typeof(bool));
            foreach (DataRow row in this.refundManagementDataSet.ListRefundPayments.Rows)
            {
                row["RefundItemCheck"] = false;
            }
        }

        /// <summary>
        /// Shows the interest date calender.
        /// </summary>
        private void ShowInterestDateCalender()
        {
            this.InterestDateCalender.Visible = true;
            this.InterestDateCalender.ScrollChange = 1;

            //// Display the Calender control near the Calender Picture box.
            ////this.InterestDateCalender.Left = this.RefundHeaderPanel.Left + this.InterestDatePanel.Left + this.InterestDateCalenderButton.Left + this.InterestDateCalenderButton.Width;
            ////this.InterestDateCalender.Top = this.RefundHeaderPanel.Top + this.InterestDatePanel.Top + this.InterestDateCalenderButton.Top;
            this.InterestDateCalender.Focus();

            if (!string.IsNullOrEmpty(this.InterestDateTextBox.Text))
            {
                this.InterestDateCalender.SetDate(Convert.ToDateTime(this.InterestDateTextBox.Text));
            }
        }

        /// <summary>
        /// Gets the payment items.
        /// </summary>
        /// <returns>Payment details</returns>
        private string GetPaymentItems()
        {
            this.tempPaymentItemsDataTable = new DataTable();
            string agencyIds = string.Empty;

            foreach (DataColumn column in this.refundManagementDataSet.ListRefundPayments.Columns)
            {
                if (column.ColumnName == this.refundManagementDataSet.ListRefundPayments.PaymentIDColumn.ColumnName)
                {
                    this.tempPaymentItemsDataTable.Columns.Add(new DataColumn(column.ColumnName));
                }

                if (column.ColumnName == this.refundManagementDataSet.ListRefundPayments.PPaymentIDColumn.ColumnName)
                {
                    this.tempPaymentItemsDataTable.Columns.Add(new DataColumn(column.ColumnName));
                }

                if (column.ColumnName == this.refundManagementDataSet.ListRefundPayments.AmountColumn.ColumnName)
                {
                    this.tempPaymentItemsDataTable.Columns.Add(new DataColumn(column.ColumnName));
                }

                if (column.ColumnName == this.refundManagementDataSet.ListRefundPayments.InterestColumn.ColumnName)
                {
                    this.tempPaymentItemsDataTable.Columns.Add(new DataColumn(column.ColumnName));
                }

                if (column.ColumnName == this.refundManagementDataSet.ListRefundPayments.PaidByColumn.ColumnName)
                {
                    this.tempPaymentItemsDataTable.Columns.Add(new DataColumn(column.ColumnName));
                }

                if (column.ColumnName == this.refundManagementDataSet.ListRefundPayments.CheckNumberColumn.ColumnName)
                {
                    this.tempPaymentItemsDataTable.Columns.Add(new DataColumn(column.ColumnName));
                }
            }

            foreach (DataRow dr in this.refundManagementDataSet.ListRefundPayments.Rows)
            {
                this.tempPaymentItemsDataRow = this.tempPaymentItemsDataTable.NewRow();

                if (dr["RefundItemCheck"] != DBNull.Value && (Convert.ToBoolean(dr["RefundItemCheck"])))
                {
                    ////if (Convert.ToBoolean(dr["RefundItemCheck"]))
                    ////{
                    foreach (DataColumn column in this.refundManagementDataSet.ListRefundPayments.Columns)
                    {
                        if (column.ColumnName == this.refundManagementDataSet.ListRefundPayments.PaymentIDColumn.ColumnName)
                        {
                            this.tempPaymentItemsDataRow[column.ColumnName] = dr[column.ColumnName];
                        }

                        if (column.ColumnName == this.refundManagementDataSet.ListRefundPayments.PPaymentIDColumn.ColumnName)
                        {
                            this.tempPaymentItemsDataRow[column.ColumnName] = dr[column.ColumnName];
                        }

                        if (column.ColumnName == this.refundManagementDataSet.ListRefundPayments.AmountColumn.ColumnName)
                        {
                            this.tempPaymentItemsDataRow[column.ColumnName] = dr[column.ColumnName];
                        }

                        if (column.ColumnName == this.refundManagementDataSet.ListRefundPayments.InterestColumn.ColumnName)
                        {
                            this.tempPaymentItemsDataRow[column.ColumnName] = dr[column.ColumnName];
                        }

                        if (column.ColumnName == this.refundManagementDataSet.ListRefundPayments.PaidByColumn.ColumnName)
                        {
                            this.tempPaymentItemsDataRow[column.ColumnName] = dr[column.ColumnName];
                        }

                        if (column.ColumnName == this.refundManagementDataSet.ListRefundPayments.CheckNumberColumn.ColumnName)
                        {
                            this.tempPaymentItemsDataRow[column.ColumnName] = dr[column.ColumnName];
                        }
                    }

                    this.tempPaymentItemsDataTable.Rows.Add(this.tempPaymentItemsDataRow);
                    ////}
                }
            }

            if (this.tempPaymentItemsDataTable.Rows.Count > 0)
            {
                agencyIds = TerraScanCommon.GetXmlString(this.tempPaymentItemsDataTable);
            }

            return agencyIds;
        }

        #endregion

        private void PaymentDateTextBox_Leave(object sender, EventArgs e)
        {
            if (isShifts)
            {
                InterestDateCalenderButton.Focus();
                isShifts = false;
            }
            else
            {
                this.ParentForm.ActiveControl = PaidBYTextBox;
                this.ParentForm.ActiveControl.Focus();
            }
            PaymentDateTextBox.BackColor = Color.White;
            EntryDatePanel.BackColor = Color.White;
            

        }

        private void InterestDateCalender_Leave(object sender, EventArgs e)
        {
            if (isShift)
            {
                InterestDateTextBox.Focus();
            }
            else
            {
                PaymentDateTextBox.Focus();
            }
        }

        private void SuspendesOwnerTextBox_Leave(object sender, EventArgs e)
        {
            SuspendesOwnerTextBox.BackColor = Color.White;
            panel4.BackColor = Color.White;
        }

        private void InterestDateTextBox_Leave(object sender, EventArgs e)
        {
            InterestDateTextBox.BackColor = Color.White;
            InterestDatePanel.BackColor = Color.White;
        }

        private void PaidBYTextBox_Leave(object sender, EventArgs e)
        {
            AccountPanel.BackColor = Color.White;
            PaidBYTextBox.BackColor = Color.White;
        }

        private void CheckNoTextBox_Leave(object sender, EventArgs e)
        {
            CLIDPanel.BackColor = Color.White;
            CheckNoTextBox.BackColor = Color.White;
        }

        private void UserTextBox_Leave(object sender, EventArgs e)
        {
            UserTextBox.BackColor = Color.White;
            UserPanel.BackColor = Color.White;
        }

        private void AmountTextBox_Leave(object sender, EventArgs e)
        {
            AmountTextBox.BackColor = Color.White;
            AmountPanel.BackColor = Color.White;
        }

        private void PaymentDateTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            isShifts = e.Shift;
        }
    }
}
