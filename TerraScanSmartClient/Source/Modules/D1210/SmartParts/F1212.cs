//--------------------------------------------------------------------------------------------
// <copyright file="F1212.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F1212 MakeDeposits and Functionality
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 05-09-2006       Shiva              Created
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
    using TerraScan.BusinessEntities;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using TerraScan.Utilities;
    using System.Web.Services.Protocols;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// F1212 Class 
    /// </summary>
    [SmartPart] 
    public partial class F1212 : BaseSmartPart
    {
        #region Private Variables

        /// <summary>
        /// form1212Controll Variable 
        /// </summary>
        private F1212Controller form1212Controll;

        /// <summary>
        /// variable MakeDepositsData
        /// </summary>
        private MakeDepositsData makeDepositsDataSet = new MakeDepositsData();

        /// <summary>
        /// formLabelInfo string array
        /// </summary>
        private string[] formLabelInfo = new string[2];

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
        /// register Id
        /// </summary>
        private int regId = -1;

        /// <summary>
        /// tempDataTable for SaveDeposit
        /// </summary>
        private DataTable tempListPaymentItemsDataTable;

        /// <summary>
        /// tempDataRow for SaveDeposit
        /// </summary>
        private DataRow tempListPaymentItemsDataRow;

        /// <summary>
        /// Payment Item Table Row Count
        /// </summary>
        private int paymentRowCount;

        /// <summary>
        /// Account Table Row Count
        /// </summary>
        private int accountRowCount;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for F1212
        /// </summary>
        public F1212()
        {
            InitializeComponent();
            this.PaymentGridPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PaymentGridPictureBox.Height, this.PaymentGridPictureBox.Width, "Available Payment Items", 28, 81, 128);
            this.AccountGridPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AccountGridPictureBox.Height, this.AccountGridPictureBox.Width, "Accounts", 174, 150, 94);
        }

        #endregion

        #region Published Events

        /// <summary>
        /// EventPublication for FormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// Declare the event ShowForm        
        /// </summary> 
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form1212 controll.
        /// </summary>
        /// <value>The form1212 controll.</value>
        [CreateNew]
        public F1212Controller Form1212Controll
        {
            get { return this.form1212Controll as F1212Controller; }
            set { this.form1212Controll = value; }
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
        public int RegId
        {
            get { return this.regId; }
            set { this.regId = value; }
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

        /// <summary>
        /// Gets or sets the payment row count.
        /// </summary>
        /// <value>The payment row count.</value>
        public int PaymentRowCount
        {
            get { return this.paymentRowCount; }
            set { this.paymentRowCount = value; }
        }

        #endregion

        #region FormEvents

        /// <summary>
        /// Handles the Load event of the F1212 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1212_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkSpace();
                this.LoadPaymentItemsGrid();
                if (this.paymentRowCount == 0)
                {
                    this.DepositButton.Enabled = false;
                    this.PaymentItemsGrid.Enabled = false;
                    this.MakeDepositHelpSmartPart.Focus();
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
        /// Handles the Click event of the HistoryButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void HistoryButton_Click(object sender, EventArgs e)
        {
            try
            {
                FormInfo formInfo = TerraScanCommon.GetFormInfo(1213);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the PaymentItemsGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
                {
                    if (this.PaymentRowCount > 0)
                    {
                        this.PaymentItemsGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        this.SetGridSummeries();
                    }
                }
            }
            catch (DataException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the DepositButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DepositButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string paymentItemDetails = string.Empty;
                paymentItemDetails = this.GetPaymentIds();
                if (!string.IsNullOrEmpty(paymentItemDetails.Trim()) && this.RegId > 0 && this.selectedPaymentAmount > 0)
                {
                    this.Form1212Controll.WorkItem.SavePaymentItemsDetails(this.regId, this.selectedPaymentAmount, TerraScanCommon.UserId, paymentItemDetails);
                    this.regId = -1;
                    this.LoadPaymentItemsGrid();
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
        /// Handles the CellContentClick event of the AccountsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AccountsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.AccountsGridClick(e);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the AccountsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AccountsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.AccountsGridClick(e);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the PaymentItemsGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;
            try
            {
                //// Only paint if desired, formattable column

                if (e.ColumnIndex == this.PaymentItemsGrid.Columns["Amount"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.PaymentItemsGrid.Rows[e.RowIndex].Cells[this.makeDepositsDataSet.ListPaymentItemTable.AmountColumn.ColumnName.ToString()].Value.ToString().Trim()))
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
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the AccountsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void AccountsGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;
            try
            {
                //// Only paint if desired, formattable column

                if (e.ColumnIndex == this.AccountsGridView.Columns["AccountBalance"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.AccountsGridView.Rows[e.RowIndex].Cells[this.makeDepositsDataSet.ListAccountsGridTable.AccountBalanceColumn.ColumnName.ToString()].Value.ToString().Trim()))
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
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads the work space.
        /// </summary>
        private void LoadWorkSpace()
        {
            if (this.form1212Controll.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1212Controll.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1212Controll.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            this.formLabelInfo[0] = SharedFunctions.GetResourceString("1212MakeDepositsFormName");
            this.formLabelInfo[1] = string.Empty;

            this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));
        }        

        /// <summary>
        /// Loads the afiidavit list grid.
        /// </summary>
        private void LoadPaymentItemsGrid()
        {
            this.paymentRowCount = 0;
            this.accountRowCount = 0;

            this.makeDepositsDataSet = this.Form1212Controll.WorkItem.GetPaymentItemsDetails();

            this.PaymentRowCount = this.makeDepositsDataSet.ListPaymentItemTable.Rows.Count;
            this.AccountRowCount = this.makeDepositsDataSet.ListAccountsGridTable.Rows.Count;

            this.CustomizePaymentItemsDataGrid();
            this.AddCheckBoxColumn();
            this.totalRecCount = this.makeDepositsDataSet.ListPaymentItemTable.Rows.Count;

            this.PaymentItemsGrid.DataSource = this.makeDepositsDataSet.ListPaymentItemTable;
            this.AccountsGridView.DataSource = this.makeDepositsDataSet.ListAccountsGridTable;

            this.SetGridSummeries();

            if (this.PaymentRowCount == 0)
            {
                this.DepositButton.Enabled = false;
                this.PaymentItemsGrid.Enabled = false;
                this.MakeDepositHelpSmartPart.Focus();
                ////this.PaymentItemsGrid.Rows[0].Selected = false;
                ////this.PaymentItemsGrid.CurrentCell = null;
            }
            else
            {
                this.PaymentItemsGrid.Enabled = true;
                this.PaymentItemsGrid.Rows[0].Selected = true;
            }

            if (this.makeDepositsDataSet.ListPaymentItemTable.Rows.Count > this.PaymentItemsGrid.NumRowsVisible)
            {
                this.PaymentItmesVScrollBar.Enabled = true;
                this.PaymentItmesVScrollBar.Visible = false;
            }
            else
            {
                this.PaymentItmesVScrollBar.Enabled = false;
                this.PaymentItmesVScrollBar.Visible = true;
            }

            if (this.AccountRowCount == 0)
            {
                this.DepositButton.Enabled = false;
            }
            else
            {
                DataRow[] dataRow;
                bool defaultValue = true;
                string findExp = "IsDefault =" + defaultValue.ToString();
                dataRow = this.makeDepositsDataSet.ListAccountsGridTable.Select(findExp);
                if (dataRow.Length > 0)
                {
                    int rowIndex = this.makeDepositsDataSet.ListAccountsGridTable.Rows.IndexOf(dataRow[0]);
                    TerraScanCommon.SetDataGridViewPosition(this.AccountsGridView, rowIndex);
                    this.AccountsGridView.CurrentRow.Selected = true;
                    this.regId = Convert.ToInt32(dataRow[0][this.makeDepositsDataSet.ListAccountsGridTable.RegisterIDColumn]);
                }
                else
                {
                    TerraScanCommon.SetDataGridViewPosition(this.AccountsGridView, 0);
                    this.regId = Convert.ToInt32(this.makeDepositsDataSet.ListAccountsGridTable.Rows[0][this.makeDepositsDataSet.ListAccountsGridTable.RegisterIDColumn]);
                }
            }

            if (this.makeDepositsDataSet.ListAccountsGridTable.Rows.Count > this.AccountsGridView.NumRowsVisible)
            {
                this.AccountsVScorrlBar.Enabled = true;
                this.AccountsVScorrlBar.Visible = false;
            }
            else
            {
                this.AccountsVScorrlBar.Enabled = false;
                this.AccountsVScorrlBar.Visible = true;
            }
        }

        /// <summary>
        /// Adds the check box column.
        /// </summary>
        private void AddCheckBoxColumn()
        {
            this.makeDepositsDataSet.ListPaymentItemTable.Columns.Add("PaymentCheck", typeof(bool));
            foreach (DataRow row in this.makeDepositsDataSet.ListPaymentItemTable.Rows)
            {
                row["PaymentCheck"] = true;
            }
        }

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizePaymentItemsDataGrid()
        {
            this.PaymentItemsGrid.AllowUserToResizeColumns = false;
            this.PaymentItemsGrid.AutoGenerateColumns = false;
            this.PaymentItemsGrid.AllowUserToResizeRows = false;
            this.PaymentItemsGrid.StandardTab = true;
            this.PaymentItemsGrid.Columns[2].Resizable = DataGridViewTriState.False;
            this.AccountsGridView.PrimaryKeyColumnName = this.makeDepositsDataSet.ListAccountsGridTable.RegisterIDColumn.ColumnName.ToString();

            this.PaymentItemsGrid.Columns[0].DataPropertyName = "PaymentID";
            this.PaymentItemsGrid.Columns[1].DataPropertyName = "pPaymentID";
            this.PaymentItemsGrid.Columns[2].DataPropertyName = "PaymentCheck";
            this.PaymentItemsGrid.Columns[3].DataPropertyName = "PaymentDate";
            this.PaymentItemsGrid.Columns[4].DataPropertyName = "TenderTypeID";
            this.PaymentItemsGrid.Columns[5].DataPropertyName = "TenderType";
            this.PaymentItemsGrid.Columns[6].DataPropertyName = "PaidBy";
            this.PaymentItemsGrid.Columns[7].DataPropertyName = "CheckNumber";
            this.PaymentItemsGrid.Columns[8].DataPropertyName = "Name_Display";
            this.PaymentItemsGrid.Columns[9].DataPropertyName = "Amount";
            this.PaymentItemsGrid.Columns[10].DataPropertyName = "UserID";

            ////AccountsGridView Properties to Customize

            this.AccountsGridView.AllowUserToResizeColumns = false;
            this.AccountsGridView.AutoGenerateColumns = false;
            this.AccountsGridView.AllowUserToResizeRows = false;
            this.AccountsGridView.StandardTab = true;

            this.AccountsGridView.Columns[0].DataPropertyName = "RegisterID";
            this.AccountsGridView.Columns[1].DataPropertyName = "AccountName";
            this.AccountsGridView.Columns[2].DataPropertyName = "AccountNumber";
            this.AccountsGridView.Columns[3].DataPropertyName = "AccountBalance";
            this.AccountsGridView.Columns[4].DataPropertyName = "IsDefault";
        }

        /// <summary>
        /// Sets the grid summeries.
        /// </summary>
        private void SetGridSummeries()
        {
            this.selectedRecCount = 0;
            this.selectedPaymentAmount = 0;
            this.totalPaymentAmount = 0;

            for (int i = 0; i < this.makeDepositsDataSet.ListPaymentItemTable.Rows.Count; i++)
            {
                if (this.makeDepositsDataSet.ListPaymentItemTable.Rows[i]["PaymentCheck"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(this.makeDepositsDataSet.ListPaymentItemTable.Rows[i]["PaymentCheck"]))
                    {
                        this.selectedRecCount += 1;
                        this.selectedPaymentAmount += Convert.ToDecimal(this.makeDepositsDataSet.ListPaymentItemTable.Rows[i]["Amount"]);
                    }

                    this.totalPaymentAmount += Convert.ToDecimal(this.makeDepositsDataSet.ListPaymentItemTable.Rows[i]["Amount"]);
                }
            }

            RecordCountLabel.Text = this.SelectedRecCount + " / " + this.TotalRecCount;
            this.SelectedAmountTextBox.Text = this.SelectedPaymentAmount.ToString();
            this.totalAmountTextBox.Text = this.TotalPaymentAmount.ToString();
            if (this.SelectedRecCount > 0 && this.PermissionFiled.newPermission && this.accountRowCount > 0)
            {
                this.DepositButton.Enabled = true;
            }
            else
            {
                this.DepositButton.Enabled = false;
            }
            }

        /// <summary>
        /// Gets the payment ids.
        /// </summary>
        /// <returns>XML String as PaymentDetails</returns>
        private string GetPaymentIds()
        {
            this.tempListPaymentItemsDataTable = new DataTable();
            string paymentIds = string.Empty;

            foreach (DataColumn column in this.makeDepositsDataSet.ListPaymentItemTable.Columns)
            {
                if (column.ColumnName == this.makeDepositsDataSet.ListPaymentItemTable.PaymentIDColumn.ColumnName)
                {
                    this.tempListPaymentItemsDataTable.Columns.Add(new DataColumn(column.ColumnName));
                }
            }

            foreach (DataRow dr in this.makeDepositsDataSet.ListPaymentItemTable.Rows)
            {
                this.tempListPaymentItemsDataRow = this.tempListPaymentItemsDataTable.NewRow();

                if (dr["PaymentCheck"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(dr["PaymentCheck"]))
                    {
                        foreach (DataColumn column in this.makeDepositsDataSet.ListPaymentItemTable.Columns)
                        {
                            if (column.ColumnName == this.makeDepositsDataSet.ListPaymentItemTable.PaymentIDColumn.ColumnName)
                            {
                                this.tempListPaymentItemsDataRow[column.ColumnName] = dr[column.ColumnName];
                            }
                        }

                        this.tempListPaymentItemsDataTable.Rows.Add(this.tempListPaymentItemsDataRow);
                    }
                }
            }

            paymentIds = TerraScanCommon.GetXmlString(this.tempListPaymentItemsDataTable);
            return paymentIds;
        }

        /// <summary>
        /// Accountses the grid click.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AccountsGridClick(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.regId = Convert.ToInt32(this.makeDepositsDataSet.ListAccountsGridTable.Rows[e.RowIndex][this.makeDepositsDataSet.ListAccountsGridTable.RegisterIDColumn]);
            }
        }

        #endregion

        /// <summary>
        /// Handles the DataBindingComplete event of the PaymentItemsGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.PaymentItemsGrid.OriginalRowCount == 0)
                {
                    this.PaymentItemsGrid.CurrentCell = null;
                }
            }
            catch (Exception)
            { 
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the AccountsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void AccountsGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.AccountsGridView.OriginalRowCount == 0)
                {
                    this.AccountsGridView.CurrentCell = null;
                }
            }
            catch (Exception)
            { 
            }
        }
    }
}
