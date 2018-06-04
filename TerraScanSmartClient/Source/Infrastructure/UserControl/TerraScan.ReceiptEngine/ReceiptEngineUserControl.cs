// -------------------------------------------------------------------------------------------------
// <copyright file="ReceiptEngineUserControl.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// User Control 
// </summary>
// -------------------------------------------------------------------------------------------------

[assembly: System.CLSCompliant(false)]
namespace TerraScan.ReceiptEngine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Helper;
    using System.Collections;
    using System.Configuration;
    using System.Globalization;
    using TerraScan.Utilities;
    using System.Web.Services.Protocols;   
    using TerraScan.UI.Controls;
    using TerraScan.UI;
    using TerraScan.Common;
    using TerraScan.BusinessEntities;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Receipt Engine User Control
    /// </summary>
    public partial class ReceiptEngineUserControl : TerraScan.Common.UserControlBasePage
    {
        #region Member Variables

        /// <summary>
        /// Declaring the receiptDate
        /// </summary>
        private string receiptDate;

        /// <summary>
        /// assigining Empty or NUll Value to String
        /// </summary>
        private int? currentReceiptId = null;

        /// <summary>
        /// Perform Cancel button event on receipts
        /// </summary>
        private bool cancelReceipt;

        /// <summary>
        /// Assiging Empty to StateMentID
        /// </summary>
        private int? statementId = null;

        /// <summary>
        /// Assiging Empty to ownerName
        /// </summary>
        private string ownerName = String.Empty;

        /// <summary>
        /// Declaring the statementFees
        /// </summary>
        private decimal statementFees;   

        /// <summary>
        /// Assigning Empty to FormId
        /// </summary>
        private string formId = String.Empty;

        /// <summary>
        /// Assigning Empty to parentWorkItem
        /// </summary>
        private WorkItem parentWorkItem;           

        /// <summary>
        /// enum variable used to find PageMode
        /// </summary>
        private PageModeTypes pageMode;

        /// <summary>
        /// create variable for paymentoptiontypes
        /// </summary>
        private PaymentOptionTypes paymentOption;        

        /// <summary>
        /// Declaring the Receipt balance
        /// </summary>
        private decimal receiptBalance;

        /// <summary>
        /// Actual Row Count.
        /// </summary>
        private int actualRowCount;

        /// <summary>
        /// PreviousRowIndex.
        /// </summary>
        private int previousRowIndex;

        /// <summary>
        /// Contain OverUnderMaxAmount from configruation
        /// </summary>
        private decimal overUnderMaxAmount;

        /// <summary>
        /// Contain OverUnderMinAmount from configruation
        /// </summary>
        private decimal overUnderMinAmount;

        /// <summary>
        /// checks history grid row validation cancelled or not
        /// </summary>
        private bool processRowEnter = true;

        /// <summary>
        /// Boolean Value
        /// </summary>        
        private bool interestDateChanged; 

        /// <summary>
        /// receiptengine dataset
        /// </summary>
        private ReceiptEngineData receiptEngine = new ReceiptEngineData();

        #endregion

        #region constructor

        /// <summary>
        /// ReceiptEngineUserControl constructor
        /// </summary>
        public ReceiptEngineUserControl()
        {
            this.InitializeComponent();

            //// Assigning Current Date to ReceiptDate Variable.

            this.ReceiptDate = DateTime.Today.ToShortDateString();

            ////Binding Database Columns to History Grid.

            this.ReceiptHistoryGridBindColumns();
            this.GetTenderTypeConfiguration();

            ////Binding Database Columns to Payment Grid.
            this.CustomizePaymentItemsGridView();

            // Setting tag property to select option controls
            this.MinDueRadioButton.Tag = PaymentOptionTypes.MinDue;
            this.BalanceRadioButton.Tag = PaymentOptionTypes.Balance;
            this.PartialRadioButton.Tag = PaymentOptionTypes.Partial;            
        }

        #endregion

        #region delegateDeclaration
        /// <summary>
        /// Declare delegate for save button clicked.
        /// </summary>
        /// <param name="receiptId">saved receiptid</param>       
        public delegate void SavedEventHandler(int receiptId);

        /// <summary>
        /// Declare delegate for PageModeChange
        /// </summary>
        /// <param name="pageMode">changed pagemode</param> 
        public delegate void PageModeChangedEventHandler(string pageMode);

        /// <summary>
        /// Declare delegate for Delinquency
        /// </summary>
        /// <param name="isDelinquent">Delinquent status</param> 
        public delegate void DelinquencyChangedEventHandler(bool isDelinquent);

        #endregion

        #region eventDecalration
        /// <summary>
        /// Declare the event, which is associated with the
        /// delegate SavedEventHandler(int).  
        /// </summary>          
        [Description("Fires when the receipt is saved")]
        public event SavedEventHandler SavedEvent;

        /// <summary>
        /// Declare the event, which is associated with the delegate PageModeChangedHandler(string).      
        /// </summary>      
        [Description("Fires when the page mode is changed")]
        public event PageModeChangedEventHandler PageModeChangedEvent;

        /// <summary>
        /// Declare the event, which is associated with the delegate DelinquencyChangedEventHandler(bool).      
        /// </summary>      
        [Description("Fires when the deinquency is changed")]
        public event DelinquencyChangedEventHandler DelinquencyChangedEvent;

        #endregion

        #region Enum

        /// <summary>
        /// Enumerator PaymentOption
        /// </summary>
        public enum PaymentOptionTypes
        {
            /// <summary>
            /// MinDue  = 0.
            /// </summary>
            MinDue = 0,

            /// <summary>
            /// Balance = 1.
            /// </summary>
            Balance,

            /// <summary>
            /// Partial = 2.
            /// </summary>
            Partial
        }

        /// <summary>
        /// Enumerator PageMode
        /// </summary>
        public enum PageModeTypes
        {
            /// <summary>
            /// Viewing  = 0.
            /// </summary>
            Viewing = 0,

            /// <summary>
            /// New Receipt = 1.
            /// </summary>
            NewReceipt,

            /// <summary>
            /// Query By Form = 2.
            /// </summary>
            QueryByForm
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the prev receipt ID.
        /// </summary>
        /// <value>The prev receipt ID.</value>
        public int? CurrentReceiptId
        {
            get
            {
                return this.currentReceiptId;
            }

            set
            {
                this.currentReceiptId = value;
            }
        }

        /// <summary>
        /// Gets or sets the parent work item.
        /// </summary>
        /// <value>The parent work item.</value>
        public WorkItem ParentWorkItem
        {
            get { return this.parentWorkItem; }
            set { this.parentWorkItem = value; }
        }

        /// <summary>
        /// Gets or sets the prev receipt ID.
        /// </summary>
        /// <value>The prev receipt ID.</value>
        public bool CancelReceipt
        {
            get
            {
                return this.cancelReceipt;
            }

            set
            {
                if (value == true)
                {
                    this.PerformCancel();
                }

                this.cancelReceipt = value;
            }
        }

        /// <summary>
        /// Gets or sets the Owner Name.
        /// </summary>
        /// <value>contains statement ownername.</value>
        public string OwnerName
        {
            get { return this.ownerName; }
            set { this.ownerName = value; }
        }        

        /// <summary>
        /// Gets or sets the Statement Fees.
        /// </summary>
        /// <value>statement Fees</value>
        public decimal StatementFees
        {
            get { return this.statementFees; }
            set { this.statementFees = value; }
        }

        /// <summary>
        /// Gets or sets the FormCancelButton.
        /// </summary>
        /// <value>The FormCancelButton.</value> 
        public TerraScanButton ReceiptEngineCancelButton
        {
            get { return this.CancelReceiptButton; }
            set { this.CancelReceiptButton = value; }
        }  

        #region StatementId

        /// <summary>
        /// Gets or sets the statement ID.
        /// </summary>
        /// <value>The statement ID.</value>
        [Description("Display Data based on Statement ID.")]
        public int? StatementId
        {
            get
            {
                return this.statementId;
            }

            set
            {
                this.statementId = value;
                if (value.HasValue)
                {
                    this.PopulateHistoryGrid();
                }
                else
                {
                    ////empty history grid
                    this.ReceiptHistoryGridView.CurrentCell = null;
                    this.PaymentItemsGridView.CurrentCell = null;
                    this.StatementBalanceTextBox.Text = "$0.00";
                    this.receiptEngine.Clear();
                    this.ReceiptHistoryGridView.DataSource = this.receiptEngine.ListHistoryGrid;
                    ////when statementid is empty - eg in querybyform
                    this.ClearReceiptDetails(true);                    
                }
            }
        }

        #endregion

        #region Form Id

        /// <summary>
        /// Gets or sets the name of the form.
        /// </summary>
        /// <value>The name of the form.</value>
        [Description("Display Data based on form Name.")]
        public string FormId
        {
            get
            {
                return this.formId;
            }

            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.formId = value;
                }
            }
        }

        #endregion

        #region Page Status

        /// <summary>
        /// Gets or sets the page mode.
        /// </summary>
        /// <value>The page mode.</value>
        private PageModeTypes PageMode
        {
            get
            {
                return this.pageMode;
            }

            set
            {
                this.pageMode = value;
                if (this.pageMode.Equals(PageModeTypes.Viewing))
                {
                    this.processRowEnter = true;
                }

                this.OnPageModeChanged(this.pageMode.ToString());
            }
        }

        /// <summary>
        /// Gets or sets the payment option.
        /// </summary>
        /// <value>The payment option.</value>
        private PaymentOptionTypes PaymentOption
        {
            get
            {
                return this.paymentOption;
            }

            set
            {
                this.paymentOption = value;
            }
        }

        #region Receipt Date
        /// <summary>
        /// Last saved ReceiptDate
        /// </summary>
        /// <value>ReceiptDate</value>
        private string ReceiptDate
        {
            get { return this.receiptDate; }
            set { this.receiptDate = value; }
        }

        #endregion
        #endregion

        /// <summary>
        /// Gets or sets the Receipt Balance.
        /// </summary>
        /// <value>The Receipt Balance.</value>
        private decimal ReceiptBalance
        {
            get { return this.receiptBalance; }
            set { this.receiptBalance = value; }
        }

        /// <summary>
        /// Gets or sets the Over Under MaxAmount.
        /// </summary>
        /// <value>The OverUnder MaxAmount.</value>
        private decimal OverUnderMaxAmount
        {
            get { return this.overUnderMaxAmount; }
            set { this.overUnderMaxAmount = value; }
        }

        /// <summary>
        /// Gets or sets the OverUnder MinAmount.
        /// </summary>
        /// <value>The OverUnder MinAmount.</value>
        private decimal OverUnderMinAmount
        {
            get { return this.overUnderMinAmount; }
            set { this.overUnderMinAmount = value; }
        }

        #endregion

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <param name="onclose">if set to <c>true</c> [on close].</param>
        /// <returns>true - for continuing/false - leave unsaved changes</returns>
        public bool CheckPageStatus(bool onclose)
        {
            DialogResult dialogResult;
            bool returnValue = false;

            if (this.PageMode.Equals(PageModeTypes.NewReceipt))
            {
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " Real Property?"), ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    returnValue = this.SaveReceipt(onclose);

                    return returnValue;
                }
                else if (dialogResult == DialogResult.No)
                {
                    if (!onclose)
                    {
                        this.CancelReceipt = true;
                    }

                    return true;
                }

                return false;
            }

            return true;
        }

        #region Delegetes

        /// <summary>
        /// Add a protected method called OnSavedEvent(). 
        /// </summary>
        /// <param name="receiptId">saved receiptid</param>         
        protected virtual void OnSavedEvent(int receiptId)
        {
            // If an event has no subscribers registerd, it will 
            // evaluate to null. The test checks that the value is not
            // null, ensuring that there are subsribers before
            // calling the event itself.
            if (this.SavedEvent != null)
            {
                this.SavedEvent(receiptId);  // Notify Subscribers
            }
        }

        /// <summary>
        ///  Add a protected method called OnPageModeChanged().     
        /// </summary>
        /// <param name="pageModeValue">changed pagemode</param>       
        protected virtual void OnPageModeChanged(string pageModeValue)
        {
            /* If an event has no subscribers registerd, it will 
             evaluate to null. The test checks that the value is not
             null, ensuring that there are subsribers before
             calling the event itself.*/
            if (this.PageModeChangedEvent != null)
            {
                this.PageModeChangedEvent(pageModeValue);  // Notify Subscribers
            }
        }

        /// <summary>
        ///  Add a protected method called OnDelinquentChanged(dool).     
        /// </summary>
        /// <param name="isdelinquent">changed Delinquent</param>       
        protected virtual void OnDelinquentChanged(bool isdelinquent)
        {
            /* If an event has no subscribers registerd, it will 
             evaluate to null. The test checks that the value is not
             null, ensuring that there are subsribers before
             calling the event itself.*/
            if (this.DelinquencyChangedEvent != null)
            {
                this.DelinquencyChangedEvent(isdelinquent);  // Notify Subscribers
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// parse the value and negate if necessary.
        /// </summary>
        /// <param name="amountValue">value to be parsed</param>
        /// <returns>validated decimal value</returns>
        private static string ParseDecimalValue(string amountValue)
        {
            decimal outDecimalValue;
            Decimal.TryParse(amountValue, out outDecimalValue);
            if (outDecimalValue.ToString().Contains("-"))
            {
                outDecimalValue = Decimal.Negate(outDecimalValue);
            }

            return outDecimalValue.ToString();
        }

        /// <summary>
        /// checks for tax due and interestdue amount
        /// </summary>
        /// <param name="sourceTextBox">The source text box to be validated.</param>
        /// <returns>true if value gets validated</returns>
        private static bool CheckAmountDue(TextBox sourceTextBox)
        {
            ////validation stands for newreceipt only

            decimal outTextBoxValue;
            string tempStatementBalance;
            decimal maxMoneyValue = (decimal)int.MaxValue;

            ////validating and changing values for display

            tempStatementBalance = sourceTextBox.Text.Replace(",", "").Trim();
            if (tempStatementBalance.EndsWith("."))
            {
                tempStatementBalance = string.Concat(tempStatementBalance, "0");
            }

            decimal.TryParse(tempStatementBalance, NumberStyles.Currency, null, out outTextBoxValue);
            tempStatementBalance = outTextBoxValue.ToString();
            if (!tempStatementBalance.Contains("."))
            {
                outTextBoxValue = outTextBoxValue / 100;
            }

            // checks for - smallmoney datatype range
            maxMoneyValue = maxMoneyValue / 10000;

            if (maxMoneyValue < outTextBoxValue)
            {
                ////TODO title hardcode - needs some clarification

                MessageBox.Show(String.Concat(sourceTextBox.Tag, " ", SharedFunctions.GetResourceString("InvalidAmount")), ConfigurationManager.AppSettings["ApplicationName"], MessageBoxButtons.OK, MessageBoxIcon.Information);
                sourceTextBox.Text = "0.00";
                sourceTextBox.Focus();
                return false;
            }

            sourceTextBox.Text = outTextBoxValue.ToString();

            return true;
        }

        #endregion

        #region Private Methods

        #region History Grid

        /// <summary>
        /// Sets the data grid view position.
        /// </summary>
        /// <param name="currentRow">The current row.</param>
        private void SetDataGridViewPosition(int currentRow)
        {
            if (this.ReceiptHistoryGridView.Rows.Count > 0 && currentRow >= 0)
            {
                this.ReceiptHistoryGridView.Rows[Convert.ToInt32(currentRow)].Selected = true;
                this.ReceiptHistoryGridView.CurrentCell = this.ReceiptHistoryGridView[0, Convert.ToInt32(currentRow)];
            }
        }

        /// <summary>
        /// Populates the history grid.
        /// </summary>
        private void PopulateHistoryGrid()
        {
            this.PageMode = PageModeTypes.Viewing;
            this.PaymentItemsGridView.CurrentCell = null;
            ////Load All receipt for the current Statment to History Grid.
            this.receiptEngine.Clear();
            this.receiptEngine.ListHistoryGrid.Merge(WSHelper.ListHistoryGrid(this.statementId.Value).ListHistoryGrid);
            if (this.receiptEngine.ListHistoryGrid.Rows.Count > 1)
            {
                this.ReceiptHistoryGridView.Enabled = true;
            }
            else
            {
                this.ReceiptHistoryGridView.Enabled = false;
            }

            this.ReceiptHistoryGridView.DataSource = this.receiptEngine.ListHistoryGrid;
            this.actualRowCount = this.ReceiptHistoryGridView.OriginalRowCount;
            this.ReceiptHistoryGridView.DefaultRowIndex = this.actualRowCount - 1;
            decimal tempStatementBalance = 0;
            if (this.actualRowCount > 0)
            {
                this.StatementFees = Convert.ToDecimal(this.ReceiptHistoryGridView.Rows[0].Cells[this.receiptEngine.ListHistoryGrid.FeeColumn.ColumnName].Value);
                tempStatementBalance = this.StatementFees + Convert.ToDecimal(this.ReceiptHistoryGridView.Rows[0].Cells[this.receiptEngine.ListHistoryGrid.TaxColumn.ColumnName].Value);
                this.ReceiptHistoryGridView.Rows[0].Cells[this.receiptEngine.ListHistoryGrid.BalanceColumn.ColumnName].Value = tempStatementBalance;
                for (int counter = 1; counter < this.actualRowCount; counter++)
                {
                    // calculating Statement Balance from the Receipt.
                    if (!String.IsNullOrEmpty(this.ReceiptHistoryGridView.Rows[counter].Cells[this.receiptEngine.ListHistoryGrid.ItemColumn.ColumnName].Value.ToString().Trim()))
                    {
                        this.StatementFees += Convert.ToDecimal(this.ReceiptHistoryGridView.Rows[counter].Cells[this.receiptEngine.ListHistoryGrid.FeeColumn.ColumnName].Value);
                        ////The following method violates the DoNotCallPropertiesThatCloneValuesInLoops rule but the 
                        ////violation should be excluded because the DataGridViewRow 
                        ////of identical DataGridViewRowCollection instances is intended.
                        tempStatementBalance = Convert.ToDecimal(this.ReceiptHistoryGridView.Rows[counter - 1].Cells[this.receiptEngine.ListHistoryGrid.BalanceColumn.ColumnName].Value) + Convert.ToDecimal(this.ReceiptHistoryGridView.Rows[counter].Cells[this.receiptEngine.ListHistoryGrid.FeeColumn.ColumnName].Value) + Convert.ToDecimal(this.ReceiptHistoryGridView.Rows[counter].Cells[this.receiptEngine.ListHistoryGrid.TaxColumn.ColumnName].Value);
                        this.ReceiptHistoryGridView.Rows[counter].Cells[this.receiptEngine.ListHistoryGrid.BalanceColumn.ColumnName].Value = tempStatementBalance;
                    }
                }

                this.ReceiptHistoryGridView.Rows[0].Frozen = true;
                ///// Assigning Statement Balance to StatementBalanceTextBox.3

                this.StatementBalanceTextBox.Text = tempStatementBalance.ToString();
                if (this.actualRowCount > 1)
                {
                    this.ReceiptHistoryGridView.TabStop = true;
                    //// Populating Payment based on selected Receipt.
                    ////this.PopulatePayment(this.actualRowCount - 1);
                    if (!this.ReceiptHistoryGridView.ThroughMouseClick)
                    {
                        this.SetDataGridViewPosition(this.actualRowCount - 1);
                    }
                    //// Assigning ReceiptId to Audit Link.

                    ////this.AuditLinkLabel.Text = "tTR_Rcpt [ReceiptID] " + this.ReceiptHistoryGridView.Rows[historydetailsDataTable.Rows.Count - 1].Cells["ID"].Value.ToString();
                    ////this.AuditLinkLabel.Enabled = true;
                }
                else
                {
                    this.ReceiptHistoryGridView.TabStop = false;
                    this.previousRowIndex = 0;
                    ReceiptHistoryGridView.Rows[Convert.ToInt32(previousRowIndex)].Selected = false;
                    ReceiptHistoryGridView.CurrentCell = null;
                    //// Clear all controls
                    this.ClearReceiptDetails(false);
                }
            }

            if (ReceiptHistoryGridView.Rows.Count == 4)
            {
                this.HistoryGridVscrollBar.Visible = true;
            }
            else
            {
                this.HistoryGridVscrollBar.Visible = false;
            }

            this.FocusRequiredInputField();
        }

        /// <summary>
        /// Handles the RowEnter event of the ReceiptHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ReceiptHistoryGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.processRowEnter && e.RowIndex != 0)
            {
                this.Cursor = Cursors.WaitCursor;
                //// Assigning value to PageMode property.

                this.PageMode = PageModeTypes.Viewing;

                // Populating Payment based on selected Receipt.

                this.PopulatePayment(e.RowIndex);
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the MouseDown event of the ReceiptHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void ReceiptHistoryGridView_MouseDown(object sender, MouseEventArgs e)
        {
            int rowIndex = this.ReceiptHistoryGridView.HitTest(e.X, e.Y).RowIndex;
            if (this.ReceiptHistoryGridView.CurrentCell == null && rowIndex < 1)
            {
                this.processRowEnter = false;
                this.ReceiptHistoryGridView.DeselectCurrentCell = true;
                return;
            }

            if (!this.CheckPageStatus(false))
            {
                this.processRowEnter = false;
                this.ReceiptHistoryGridView.DeselectCurrentCell = true;                
                this.PaymentItemsGridView.Focus();
                return;
            }

            this.SetDataGridViewPosition(rowIndex);
            this.ReceiptHistoryGridView.DeselectCurrentCell = false;
            this.processRowEnter = true;
        }

        /// <summary>
        /// Displays the details.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void DisplayDetails(int rowIndex)
        {
            if (rowIndex > 0 && rowIndex < this.actualRowCount)
            {
                if (this.PageMode.Equals(PageModeTypes.NewReceipt))
                {
                    ////TODO title hardcode - needs some clarification

                    if (MessageBox.Show(SharedFunctions.GetResourceString("Cancel"), ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        UserControlCommon.SetDataGridViewPosition(this.ReceiptHistoryGridView, this.previousRowIndex);
                        return;
                    }
                }

                this.Cursor = Cursors.WaitCursor;
                this.previousRowIndex = rowIndex;
                //// Assigning value to PageMode property.

                this.PageMode = PageModeTypes.Viewing;

                // Populating Payment based on selected Receipt.

                this.PopulatePayment(rowIndex);
                this.Cursor = Cursors.Default;
            }
            else
            {
                if (this.previousRowIndex == 0)
                {
                    ReceiptHistoryGridView.Rows[Convert.ToInt32(previousRowIndex)].Selected = false;
                    ReceiptHistoryGridView.CurrentCell = null;
                    NewReceiptButton.Focus();
                }
                else
                {
                    UserControlCommon.SetDataGridViewPosition(this.ReceiptHistoryGridView, this.previousRowIndex);
                }
            }
        }

        /// <summary>
        /// Receipts the history grid bind columns.
        /// </summary>
        private void ReceiptHistoryGridBindColumns()
        {
            this.ReceiptHistoryGridView.AutoGenerateColumns = false;
            this.ReceiptHistoryGridView.Columns[0].DataPropertyName = "Date";
            this.ReceiptHistoryGridView.Columns[1].DataPropertyName = "Item";
            this.ReceiptHistoryGridView.Columns[2].DataPropertyName = "Number";
            this.ReceiptHistoryGridView.Columns[3].DataPropertyName = "Fee";
            this.ReceiptHistoryGridView.Columns[4].DataPropertyName = "Tax";
            this.ReceiptHistoryGridView.Columns[5].DataPropertyName = "Balance";
            this.ReceiptHistoryGridView.Columns[6].DataPropertyName = "Interest";
            this.ReceiptHistoryGridView.Columns[7].DataPropertyName = "ID";
        }

        /// <summary>
        /// Handles the CellFormatting event of the  ReceiptHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void ReceiptHistoryGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;

            //// Only paint if desired, formattable column

            if (e.ColumnIndex == this.ReceiptHistoryGridView.Columns[this.receiptEngine.ListHistoryGrid.FeeColumn.ColumnName].Index || e.ColumnIndex == this.ReceiptHistoryGridView.Columns[this.receiptEngine.ListHistoryGrid.TaxColumn.ColumnName].Index || e.ColumnIndex == this.ReceiptHistoryGridView.Columns[this.receiptEngine.ListHistoryGrid.BalanceColumn.ColumnName].Index || e.ColumnIndex == this.ReceiptHistoryGridView.Columns[this.receiptEngine.ListHistoryGrid.InterestColumn.ColumnName].Index)
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                /* Only paint if text provided
                 Only paint if desired text is in cell */
                if (e.Value != null && !String.IsNullOrEmpty(this.ReceiptHistoryGridView.Rows[e.RowIndex].Cells[this.receiptEngine.ListHistoryGrid.IDColumn.ColumnName].Value.ToString()))
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

            if (e.ColumnIndex == this.ReceiptHistoryGridView.Columns[this.receiptEngine.ListHistoryGrid.NumberColumn.ColumnName].Index)
            {
                ////remove link for first row

                if (e.RowIndex != 0)
                {
                    if (this.ReceiptHistoryGridView.Rows[e.RowIndex].Selected || this.ReceiptHistoryGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                    {
                        (ReceiptHistoryGridView.Rows[e.RowIndex].Cells[2] as DataGridViewLinkCell).LinkColor = Color.White;
                        (ReceiptHistoryGridView.Rows[e.RowIndex].Cells[2] as DataGridViewLinkCell).ActiveLinkColor = Color.White;
                        (ReceiptHistoryGridView.Rows[e.RowIndex].Cells[2] as DataGridViewLinkCell).VisitedLinkColor = Color.White;
                    }
                    else
                    {
                        (ReceiptHistoryGridView.Rows[e.RowIndex].Cells[2] as DataGridViewLinkCell).LinkColor = Color.Blue;
                        (ReceiptHistoryGridView.Rows[e.RowIndex].Cells[2] as DataGridViewLinkCell).ActiveLinkColor = Color.Red;
                        (ReceiptHistoryGridView.Rows[e.RowIndex].Cells[2] as DataGridViewLinkCell).VisitedLinkColor = Color.Blue;
                    }
                }
                else
                {
                    (ReceiptHistoryGridView.Rows[e.RowIndex].Cells[2] as DataGridViewLinkCell).LinkBehavior = LinkBehavior.NeverUnderline;
                    (ReceiptHistoryGridView.Rows[e.RowIndex].Cells[2] as DataGridViewLinkCell).LinkColor = Color.Black;
                    (ReceiptHistoryGridView.Rows[e.RowIndex].Cells[2] as DataGridViewLinkCell).ActiveLinkColor = Color.Black;
                    (ReceiptHistoryGridView.Rows[e.RowIndex].Cells[2] as DataGridViewLinkCell).VisitedLinkColor = Color.Black;
                }
            }
        }

        /// <summary>
        /// Handles the DataError event of the ReceiptHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void ReceiptHistoryGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        /// <summary>
        /// Determines whether [is A non header link cell] [the specified cell event].
        /// </summary>
        /// <param name="cellEvent">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        /// <returns>
        /// 	<c>true</c> if [is A non header link cell] [the specified cell event]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsANonHeaderLinkCell(DataGridViewCellEventArgs cellEvent)
        {
            if (this.ReceiptHistoryGridView.Columns[cellEvent.ColumnIndex] is
                DataGridViewLinkColumn &&
                cellEvent.RowIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region ReceiptEngineUserControl functions

        /// <summary>
        /// Handles the Click event of the ReceiptEngineUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptEngineUserControl_Click(object sender, EventArgs e)
        {
            this.ReceiptMonthCalender.Visible = false;
        }

        /// <summary>
        /// Handles the Load event of the ReceiptEngineUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptEngineUserControl_Load(object sender, EventArgs e)
        {           
            this.NewMenu.Click += new EventHandler(this.NewReceiptButton_Click);
            this.SaveMenu.Click += new EventHandler(this.SaveButton_Click);
            if (this.pageMode != PageModeTypes.NewReceipt)
            {
                if (!String.IsNullOrEmpty(this.StatementBalanceTextBox.Text) && Decimal.Parse(this.StatementBalanceTextBox.Text, NumberStyles.Currency) > 0)
                {
                    this.NewReceiptButton.Enabled = true && this.NewReceiptButton.ActualPermission;
                }
                else
                {
                    this.NewReceiptButton.Enabled = false;
                }
            }
        }

        #endregion

        #region Header Details
        /// <summary>
        /// Displays the header details.
        /// </summary>
        private void DisplayHeaderDetails()
        {
            if (this.PageMode.Equals(PageModeTypes.NewReceipt))
            {
                ////Assigning Default Value.

                this.ReceiptNumberTextBox.Text = "";
                this.ReceiptDateTextBox.Text = this.ReceiptDate;
                this.ReceivedbyTerraScanTextBox.Text = TerraScanCommon.UserName;
                this.InterestDateTextBox.Text = this.ReceiptDate;
                this.InterestDueTextBox.Text = "0.00";
                this.TaxDueTextBox.Text = "0.00";
                //// Sets Status Flag
                this.SetStatusButtonsProperty(0, 0);
            }
            else
            {
                this.ReceiptNumberTextBox.Text = this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.ReceiptNumberColumn].ToString();
                this.ReceiptDateTextBox.Text = this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.ReceiptDateColumn].ToString();
                this.ReceivedbyTerraScanTextBox.Text = this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.UserNameColumn].ToString();
                this.InterestDateTextBox.Text = this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.InterestDateColumn].ToString();
                this.DisplayAmountDueDetails(this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.TotalAmountColumn].ToString());
                //// Sets Status Flag
                if (!Object.Equals(System.DBNull.Value, this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.PPaymentIDColumn]))
                {
                    int postid = 0;
                    if (!string.IsNullOrEmpty(this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.PostIDColumn].ToString()))
                    {
                        postid = Convert.ToInt32(this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.PostIDColumn]);
                    }

                    this.SetStatusButtonsProperty(Convert.ToInt32(this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.PPaymentIDColumn]), postid);
                }
                else
                {
                    this.SetStatusButtonsProperty(0, 0);
                }
            }
        }

        /// <summary>
        /// Shows the interest date calender.
        /// </summary>
        private void ShowInterestDateCalender()
        {
            this.ReceiptMonthCalender.Visible = true;

            // Set the calendar to move one month at a time when navigating using the arrows.

            this.ReceiptMonthCalender.ScrollChange = 1;

            // Set the calendar location.
            this.ReceiptMonthCalender.Left = this.InterestDatePanel.Left + this.InterestDateCalenderButton.Left + this.InterestDateCalenderButton.Width;
            this.ReceiptMonthCalender.Top = this.InterestDatePanel.Top + this.InterestDateCalenderButton.Top;
            this.ReceiptMonthCalender.Tag = this.InterestDateCalenderButton.Tag;
            this.ReceiptMonthCalender.Focus();
            if (!string.IsNullOrEmpty(this.InterestDateTextBox.Text))
            {
                this.ReceiptMonthCalender.SetDate(Convert.ToDateTime(this.InterestDateTextBox.Text));
            }
        }

        /// <summary>
        /// Shows the receipt date calender.
        /// </summary>
        private void ShowReceiptDateCalender()
        {
            this.ReceiptMonthCalender.Visible = true;

            // Set the calendar to move one month at a time when navigating using the arrows.

            this.ReceiptMonthCalender.ScrollChange = 1;

            // Set the calendar location.

            this.ReceiptMonthCalender.Left = this.ReceiptDatePanel.Left + this.ReceiptDateCalenderButton.Left + this.ReceiptDateCalenderButton.Width;
            this.ReceiptMonthCalender.Top = this.ReceiptDatePanel.Top + this.ReceiptDateCalenderButton.Top;
            this.ReceiptMonthCalender.Tag = this.ReceiptDateCalenderButton.Tag;
            this.ReceiptMonthCalender.Focus();
            if (!string.IsNullOrEmpty(this.ReceiptDateTextBox.Text))
            {
                this.ReceiptMonthCalender.SetDate(Convert.ToDateTime(this.ReceiptDateTextBox.Text));
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the ReceiptMonthCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void ReceiptMonthCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.SetSeletedDate(e.Start.ToShortDateString());
        }

        /// <summary>
        /// Sets the seleted date.
        /// </summary>
        /// <param name="selectedDate">The selected date.</param>
        private void SetSeletedDate(string selectedDate)
        {
            if (this.ReceiptMonthCalender.Tag.ToString() == "ReceiptDateTextBox")
            {
                this.ReceiptDateTextBox.Text = selectedDate;
                this.ReceiptMonthCalender.Visible = false;
                if (this.PageMode.Equals(PageModeTypes.NewReceipt))
                {
                    this.ReceiptDate = this.ReceiptDateTextBox.Text;
                }

                this.ReceiptDateCalenderButton.Focus();
            }
            else if (this.ReceiptMonthCalender.Tag.ToString() == "InterestDateTextBox")
            {
                this.InterestDateTextBox.Text = selectedDate;
                this.InterestDateTextBox_Leave(this.InterestDateTextBox, EventArgs.Empty);
                this.ReceiptMonthCalender.Visible = false;
                this.InterestDateCalenderButton.Focus();
            }
        }

        /// <summary>
        /// Handles the Leave event of the ReceiptMonthCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptMonthCalender_Leave(object sender, EventArgs e)
        {
            this.ReceiptMonthCalender.Visible = false;
        }

        #endregion

        #region Payment Details

        /// <summary>
        /// This Method used to  set dataproperty name and column displayindex and paymentsdatatable initialization
        /// CustomizeErrorGridView
        /// </summary>
        private void CustomizePaymentItemsGridView()
        {
            this.PaymentItemsGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection columns = this.PaymentItemsGridView.Columns;

            columns[this.receiptEngine.PaymentItems.TenderTypeColumn.ColumnName].DataPropertyName = this.receiptEngine.PaymentItems.TenderTypeColumn.ColumnName;
            columns[this.receiptEngine.PaymentItems.PaidByColumn.ColumnName].DataPropertyName = this.receiptEngine.PaymentItems.PaidByColumn.ColumnName;
            columns[this.receiptEngine.PaymentItems.CheckNumberColumn.ColumnName].DataPropertyName = this.receiptEngine.PaymentItems.CheckNumberColumn.ColumnName;
            columns[this.receiptEngine.PaymentItems.AmountColumn.ColumnName].DataPropertyName = this.receiptEngine.PaymentItems.AmountColumn.ColumnName;
            columns[this.receiptEngine.PaymentItems.PaymentIDColumn.ColumnName].DataPropertyName = this.receiptEngine.PaymentItems.PaymentIDColumn.ColumnName;
            columns[this.receiptEngine.PaymentItems.PPaymentIDColumn.ColumnName].DataPropertyName = this.receiptEngine.PaymentItems.PPaymentIDColumn.ColumnName;

            columns[this.receiptEngine.PaymentItems.TenderTypeColumn.ColumnName].DisplayIndex = 0;
            columns[this.receiptEngine.PaymentItems.PaidByColumn.ColumnName].DisplayIndex = 1;
            columns[this.receiptEngine.PaymentItems.CheckNumberColumn.ColumnName].DisplayIndex = 2;
            columns[this.receiptEngine.PaymentItems.AmountColumn.ColumnName].DisplayIndex = 3;
            columns[this.receiptEngine.PaymentItems.PaymentIDColumn.ColumnName].DisplayIndex = 4;
            columns[this.receiptEngine.PaymentItems.PPaymentIDColumn.ColumnName].DisplayIndex = 5;

            this.PaymentItemsGridView.DataSource = this.receiptEngine.PaymentItems;

            DataTable tenderTypeDataTable = WSHelper.ListTenderType(true).ListTenderType;
            DataTable tempDataTable = new DataTable();
            tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("TenderType"), new DataColumn("TenderTypeID") });
            tempDataTable.Rows.Add(new object[] { "", "" });
            for (int i = 0; i < tenderTypeDataTable.Rows.Count; i++)
            {
                tempDataTable.Rows.Add(new object[] { tenderTypeDataTable.Rows[i]["TenderType"].ToString(), tenderTypeDataTable.Rows[i]["TenderTypeID"].ToString() });
            }

            (this.PaymentItemsGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).DataSource = tempDataTable;
            (this.PaymentItemsGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).DisplayMember = "TenderType";
            (this.PaymentItemsGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).ValueMember = "TenderTypeID";
        }

        /// <summary>
        /// Populates the payment grid.
        /// </summary>
        /// <param name="clearPaymentItems">if set to <c>true</c> [clear payment items].</param>
        private void PopulatePaymentGrid(bool clearPaymentItems)
        {
            ////fill paymentsDatatable with the value from the database

            this.PaymentItemsGridView.CurrentCell = null;
            if (!this.PageMode.Equals(PageModeTypes.NewReceipt))
            {
                this.SetReadOnlyToGrid(true);
            }

            if (clearPaymentItems)
            {
                this.receiptEngine.PaymentItems.Clear();
            }

            this.FillpaymentsDataTable();
            this.PaymentItemsGridView.Refresh();

            ////this.PaymentItemsGridView.DataSource = this.paymentsDataTable;
            ////this.PaymentItemsGridView.AutoGenerateColumns = false;

            ////DataTable tenderTypeDataTable = ((DataSet)WSHelper.ListTenderType()).Tables[0];
            ////DataTable tempDataTable = new DataTable();
            ////tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("TenderType"), new DataColumn("TenderTypeID") });
            ////tempDataTable.Rows.Add(new object[] { "", "" });
            ////for (int i = 0; i < tenderTypeDataTable.Rows.Count; i++)
            ////{
            ////    tempDataTable.Rows.Add(new object[] { tenderTypeDataTable.Rows[i]["TenderType"].ToString(), tenderTypeDataTable.Rows[i]["TenderTypeID"].ToString() });
            ////}
            ////(this.PaymentItemsGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).DataSource = tempDataTable.Copy();           
            ////(this.PaymentItemsGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).DisplayMember = "TenderType";
            ////(this.PaymentItemsGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).ValueMember = "TenderTypeID";             
        }

        /// <summary>
        /// Gets the tender type configuration.
        /// </summary>
        private void GetTenderTypeConfiguration()
        {
            DataSet tendertypeDataset = WSHelper.GetTenderTypeConfiguration();
            if (tendertypeDataset.Tables.Count > 0 && tendertypeDataset.Tables[0].Rows.Count > 0)
            {
                this.OverUnderMaxAmount = Convert.ToDecimal(tendertypeDataset.Tables[0].Rows[0]["OverUnderMaxAmount"]);
                this.OverUnderMinAmount = Convert.ToDecimal(tendertypeDataset.Tables[0].Rows[0]["OverUnderMinAmount"]);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_EditingControlShowing(object sender, System.Windows.Forms.DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).SelectionChangeCommitted -= new EventHandler(this.ReceiptEngine_SelectionChangeCommitted);
                ((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(this.ReceiptEngine_SelectionChangeCommitted);
                ((ComboBox)e.Control).Validating -= new CancelEventHandler(this.ReceiptEngineUserControl_ComboBoxValidating);
                ((ComboBox)e.Control).Validating += new CancelEventHandler(this.ReceiptEngineUserControl_ComboBoxValidating);
            }
        }

        /// <summary>
        /// Handles the validating event of the PaymentItemsGridView combobox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param> 
        private void ReceiptEngineUserControl_ComboBoxValidating(object sender, CancelEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            int rowIndex = -1;
            if (this.PaymentItemsGridView.CurrentCell != null)
            {
                rowIndex = this.PaymentItemsGridView.CurrentCell.RowIndex;
            }
            ////change property for combobox change
            if (rowIndex >= 0)
            {
                if (!Object.Equals(combo.SelectedValue, this.receiptEngine.PaymentItems.Rows[rowIndex]["TenderType"]))
                {
                    this.ChangeTenderTypeRelatedFields(combo);
                }                
            }

            ////check refund item
            this.CheckTenderTypeCombo(combo);
        }

        /// <summary>
        /// Checks the tender type combo.
        /// </summary>
        /// <param name="combo">The combo.</param>
        private void CheckTenderTypeCombo(ComboBox combo)
        {
            int rowIndex = -1;
            if (this.PaymentItemsGridView.CurrentCell != null)
            {
                rowIndex = this.PaymentItemsGridView.CurrentCell.RowIndex;
            }

            if (rowIndex >= 0 && Object.Equals(this.receiptEngine.PaymentItems.Rows[rowIndex]["TenderTypeID"], 2))
            {
                int refundCount = 0;
                for (int itemCount = 0; itemCount < this.PaymentItemsGridView.Rows.Count; itemCount++)
                {
                    if (string.Equals(this.PaymentItemsGridView["TenderType", itemCount].Value, "2"))
                    {
                        refundCount = refundCount + 1;
                    }
                }
              
                if (refundCount > 1)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("RefundAllowed"), string.Concat(ConfigurationManager.AppSettings["ApplicationName"], " - ", SharedFunctions.GetResourceString("TenderTypeError")), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ////reset tendertype to default check type
                    combo.SelectedValue = "3";
                    combo.Focus();
                    this.receiptEngine.PaymentItems.Rows[rowIndex]["TenderTypeID"] = 3;
                    this.receiptEngine.PaymentItems.Rows[rowIndex]["TenderType"] = "3";
                }
            }
        }

        /// <summary>
        /// Change the tender type related fields.
        /// </summary>
        /// <param name="combo">The source control.</param>
        private void ChangeTenderTypeRelatedFields(ComboBox combo)
        {
            int rowIndex = 0;
            if (this.PaymentItemsGridView.CurrentCell != null)
            {
                rowIndex = this.PaymentItemsGridView.CurrentCell.RowIndex;
            }

            ////change property for combobox change
            if (combo.SelectedIndex > 0 && combo.SelectedValue != null)
            {
                if (String.IsNullOrEmpty(this.receiptEngine.PaymentItems.Rows[rowIndex]["TenderType"].ToString()))
                {
                    this.PaymentItemsGridView["TenderType", rowIndex].Value = combo.SelectedValue.ToString();
                    this.receiptEngine.PaymentItems.Rows[rowIndex]["TenderTypeID"] = combo.SelectedValue;
                    //// Paid by should be preview paid by a value or it should be owner name if it is first record
                    if (this.PaymentItemsGridView.CurrentCell != null && PaymentItemsGridView.CurrentCell.RowIndex > 0)
                    {
                        this.receiptEngine.PaymentItems.Rows[rowIndex]["PaidBy"] = this.receiptEngine.PaymentItems.Rows[PaymentItemsGridView.CurrentCell.RowIndex - 1]["PaidBy"];
                    }
                    else
                    {
                        this.receiptEngine.PaymentItems.Rows[rowIndex]["PaidBy"] = this.OwnerName;
                    }

                    this.receiptEngine.PaymentItems.Rows[rowIndex]["Amount"] = this.ReceiptBalance;
                }
                else
                {
                    this.PaymentItemsGridView["TenderType", rowIndex].Value = combo.SelectedValue.ToString();
                    this.receiptEngine.PaymentItems.Rows[rowIndex]["TenderTypeID"] = combo.SelectedValue;
                    //// Paid by value should change only if it is null 
                    if (string.IsNullOrEmpty(this.receiptEngine.PaymentItems.Rows[rowIndex]["PaidBy"].ToString()))
                    {
                        if (this.PaymentItemsGridView.CurrentCell != null && this.PaymentItemsGridView.CurrentCell.RowIndex > 0)
                        {
                            this.receiptEngine.PaymentItems.Rows[rowIndex]["PaidBy"] = this.receiptEngine.PaymentItems.Rows[PaymentItemsGridView.CurrentCell.RowIndex - 1]["PaidBy"];
                        }
                        else
                        {
                            this.receiptEngine.PaymentItems.Rows[rowIndex]["PaidBy"] = this.OwnerName;
                        }
                    }
                }

                this.PaymentItemsGridView["PaidBy", rowIndex].ReadOnly = false;
                this.PaymentItemsGridView["Amount", rowIndex].ReadOnly = false;
                this.PaymentItemsGridView["CheckNumber", rowIndex].ReadOnly = false;
            }
            else
            {
                this.PaymentItemsGridView["TenderType", rowIndex].Value = "";
                this.receiptEngine.PaymentItems.Rows[rowIndex]["TenderTypeID"] = DBNull.Value;
                this.receiptEngine.PaymentItems.Rows[rowIndex]["PaidBy"] = "";
                this.receiptEngine.PaymentItems.Rows[rowIndex]["Amount"] = DBNull.Value;
                this.receiptEngine.PaymentItems.Rows[rowIndex]["CheckNumber"] = "";
                this.PaymentItemsGridView["PaidBy", rowIndex].ReadOnly = true;
                this.PaymentItemsGridView["CheckNumber", rowIndex].ReadOnly = true;
                this.PaymentItemsGridView["Amount", rowIndex].ReadOnly = true;
            }

            this.PaymentItemsGridView.Refresh();
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the PaymentItemsGridView combobox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>        
        private void ReceiptEngine_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;

            ////change property for combobox change

            this.ChangeTenderTypeRelatedFields(combo);

            ////check refund item
            this.CheckTenderTypeCombo(combo);
        }

        /// <summary>
        /// Handles the CellEndEdit event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            ////checks only if amount or tendertype field

            if (e.ColumnIndex == this.PaymentItemsGridView.Columns["Amount"].Index || e.ColumnIndex == this.PaymentItemsGridView.Columns["TenderType"].Index)
            {
                ////calculating related values for new values

                this.CalculatePaymentTotal();
                if (this.PaymentOption.Equals(PaymentOptionTypes.Partial))
                {
                    this.CalculateDueAmount(false, false);
                }

                this.CalculateBalance();
            }
        }

        /// <summary>
        /// Checks the over under max and min amount.
        /// </summary>
        /// <param name="overUnderValue">The over under value.</param>
        private void CheckOverUnder(decimal overUnderValue)
        {
            if (overUnderValue > this.OverUnderMaxAmount)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("OverUnderGreater") + this.OverUnderMaxAmount, string.Concat(ConfigurationManager.AppSettings["ApplicationName"], " - ", SharedFunctions.GetResourceString("TenderTypeError")), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (this.ReceiptTotalTextBox.DecimalTextBoxValue < this.OverUnderMinAmount)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("OverUnderLesser") + this.OverUnderMinAmount, String.Concat(ConfigurationManager.AppSettings["ApplicationName"], " - ", SharedFunctions.GetResourceString("TenderTypeError")), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_CellFormatting(object sender, System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;

            // Only paint if desired, formattable column

            if (e.ColumnIndex == this.PaymentItemsGridView.Columns[this.receiptEngine.PaymentItems.AmountColumn.ColumnName].Index)
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                // Only paint if text provided, Only paint if desired text is in cell 

                if (e.Value != null && !String.IsNullOrEmpty(this.PaymentItemsGridView.Rows[e.RowIndex].Cells[this.receiptEngine.PaymentItems.TenderTypeColumn.ColumnName].Value.ToString()))
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
                        e.Value = "0.00";
                    }
                }
                else
                {
                    e.Value = "";
                }
            }

            if (e.ColumnIndex == this.PaymentItemsGridView.Columns[this.receiptEngine.PaymentItems.TenderTypeColumn.ColumnName].Index)
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                if (e.Value != null && String.IsNullOrEmpty(e.Value.ToString()))
                {
                    this.PaymentItemsGridView[this.receiptEngine.PaymentItems.PaidByColumn.ColumnName, e.RowIndex].ReadOnly = true;
                    this.PaymentItemsGridView[this.receiptEngine.PaymentItems.CheckNumberColumn.ColumnName, e.RowIndex].ReadOnly = true;
                    this.PaymentItemsGridView[this.receiptEngine.PaymentItems.AmountColumn.ColumnName, e.RowIndex].ReadOnly = true;
                }

                ////this.PaymentItemsGridView["TenderTypeID", e.RowIndex].ReadOnly = true;
                this.PaymentItemsGridView[this.receiptEngine.PaymentItems.PaymentIDColumn.ColumnName, e.RowIndex].ReadOnly = true;
                this.PaymentItemsGridView[this.receiptEngine.PaymentItems.PPaymentIDColumn.ColumnName, e.RowIndex].ReadOnly = true;
            }
        }

        /// <summary>
        /// Handles the CellParsing event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_CellParsing(object sender, System.Windows.Forms.DataGridViewCellParsingEventArgs e)
        {
            // Only paint if desired column

            if (e.ColumnIndex == this.PaymentItemsGridView.Columns[this.receiptEngine.PaymentItems.AmountColumn.ColumnName].Index)
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                // Only paint if text provided, Only paint if desired text is in cell

                if (e.Value != null)
                {
                    string tempvalue = e.Value.ToString().Trim();
                    Decimal outDecimal;

                    if (tempvalue.EndsWith("."))
                    {
                        tempvalue = string.Concat(tempvalue, "0");
                    }

                    if (Decimal.TryParse(tempvalue, NumberStyles.Currency, null, out outDecimal))
                    {
                        tempvalue = outDecimal.ToString();

                        if (!tempvalue.Contains("."))
                        {
                            outDecimal = outDecimal / 100;
                        }
                    }

                    e.Value = outDecimal;
                    e.ParsingApplied = true;

                    ////Check - Over/Under
                    if (string.Equals(this.PaymentItemsGridView[this.PaymentItemsGridView.Columns[this.receiptEngine.PaymentItems.TenderTypeColumn.ColumnName].Index, e.RowIndex].Value.ToString(), "1"))
                    {
                        this.CheckOverUnder(outDecimal);
                    }
                }
            }
            else if (e.ColumnIndex == this.PaymentItemsGridView.Columns[this.receiptEngine.PaymentItems.CheckNumberColumn.ColumnName].Index)
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                if (e.Value != null)
                {
                    string val = e.Value.ToString();
                    Int64 outInt;

                    if (Int64.TryParse(val, out outInt))
                    {
                        e.ParsingApplied = true;
                    }
                    else
                    {
                        e.Value = "";
                        e.ParsingApplied = true;
                    }
                }
            }
            else if (e.ColumnIndex == this.PaymentItemsGridView.Columns[this.receiptEngine.PaymentItems.TenderTypeColumn.ColumnName].Index)
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                ////Check - Over/Under
                if (string.Equals(this.PaymentItemsGridView[e.ColumnIndex, e.RowIndex].Value.ToString(), "1"))
                {
                    decimal outDecimal;

                    if (decimal.TryParse(this.PaymentItemsGridView[this.PaymentItemsGridView.Columns[this.receiptEngine.PaymentItems.AmountColumn.ColumnName].Index, e.RowIndex].Value.ToString(), NumberStyles.Currency, null, out outDecimal))
                    {
                        this.CheckOverUnder(outDecimal);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the DataError event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_DataError(object sender, System.Windows.Forms.DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        /// <summary>
        /// Set readonly to grid
        /// </summary>
        /// <param name="readOnlyGrid">Set Readonly to cells.</param>        
        private void SetReadOnlyToGrid(bool readOnlyGrid)
        {
            ////set readonly

            if (readOnlyGrid)
            {
                this.PaymentItemsGridView.Columns[this.receiptEngine.PaymentItems.TenderTypeColumn.ColumnName].ReadOnly = true;
                this.PaymentItemsGridView.Columns[this.receiptEngine.PaymentItems.PaidByColumn.ColumnName].ReadOnly = true;
                this.PaymentItemsGridView.Columns[this.receiptEngine.PaymentItems.CheckNumberColumn.ColumnName].ReadOnly = true;
                this.PaymentItemsGridView.Columns[this.receiptEngine.PaymentItems.AmountColumn.ColumnName].ReadOnly = true;
            }
            else
            {
                this.PaymentItemsGridView.Columns[this.receiptEngine.PaymentItems.TenderTypeColumn.ColumnName].ReadOnly = false;
                this.PaymentItemsGridView.Columns[this.receiptEngine.PaymentItems.PaidByColumn.ColumnName].ReadOnly = false;
                this.PaymentItemsGridView.Columns[this.receiptEngine.PaymentItems.CheckNumberColumn.ColumnName].ReadOnly = false;
                this.PaymentItemsGridView.Columns[this.receiptEngine.PaymentItems.AmountColumn.ColumnName].ReadOnly = false;
            }
        }

        #endregion

        #region Amount Due Calculation

        /// <summary>
        /// Displays the AmountDue details.
        /// </summary>
        /// <param name="receiptTotalAmount">receipt TotalAmount</param>
        private void DisplayAmountDueDetails(string receiptTotalAmount)
        {
            this.TaxDueTextBox.Text = Decimal.Zero.ToString();
            this.InterestDueTextBox.Text = Decimal.Zero.ToString();
            this.MinDueRadioButton.Checked = true;
            this.PaymentOption = PaymentOptionTypes.MinDue;
            this.ReceiptTotalTextBox.Text = ParseDecimalValue(receiptTotalAmount);
        }

        /// <summary>
        /// Handles the Leave event of the InterestDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InterestDateTextBox_Leave(object sender, EventArgs e)
        {
            if (interestDateChanged && this.PageMode.Equals(PageModeTypes.NewReceipt) && !this.PaymentOption.Equals(PaymentOptionTypes.Partial))
            {
                interestDateChanged = false;
                ////calculating related values

                if (this.PaymentOption.Equals(PaymentOptionTypes.Balance))
                {
                    this.CalculateDueAmount(false, true);
                }
                else
                {
                    this.CalculateDueAmount(true, true);
                }

                this.CalculateBalance();
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the InterestDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InterestDateTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.PageMode.Equals(PageModeTypes.NewReceipt) && !this.PaymentOption.Equals(PaymentOptionTypes.Partial))
            {
                interestDateChanged = true;
            }
        }

        /// <summary>
        /// Gets Minimum Tax Due Value for a statement from the database 
        /// </summary>
        /// <returns> mintaxdue value</returns>
        private string GetMinTaxDueValue()
        {
            if (this.statementId.HasValue)
            {
                return WSHelper.GetMinTaxDue(this.statementId.Value, this.ReceiptDate).ToString();
            }

            return "0.00";
        }

        /// <summary>
        /// Amounts the due calculation.
        /// </summary>
        /// <param name="calculateTaxDue">if set to <c>true</c> [allow - tax due calculation].</param>
        /// <param name="calculateInterestDue">if set to <c>true</c> [allow - interest due calculation].</param>
        private void CalculateDueAmount(bool calculateTaxDue, bool calculateInterestDue)
        {
            decimal receiptTotal = 0;
            decimal taxDue = 0;

            ////check for paymentoption

            switch (this.PaymentOption)
            {
                case PaymentOptionTypes.MinDue:

                    ////whether text changed or min tax due calculation

                    if (calculateTaxDue)
                    {
                        this.TaxDueTextBox.Text = this.GetMinTaxDueValue();
                    }

                    ////else default value

                    if (calculateInterestDue)
                    {
                        this.InterestDueTextBox.Text = this.CalculateInterestDue();
                    }

                    ////else default value

                    this.CalcIntButton.Enabled = true;

                    receiptTotal = this.TaxDueTextBox.DecimalTextBoxValue + this.InterestDueTextBox.DecimalTextBoxValue;
                    this.ReceiptTotalTextBox.Text = receiptTotal.ToString();

                    break;
                case PaymentOptionTypes.Balance:

                    ////whether text changed or for balance calculation

                    if (calculateTaxDue)
                    {
                        this.TaxDueTextBox.Text = this.StatementBalanceTextBox.Text;
                    }
                    ////else default value

                    if (calculateInterestDue)
                    {
                        this.InterestDueTextBox.Text = this.CalculateInterestDue();
                    }

                    ////else default value

                    this.CalcIntButton.Enabled = true;

                    receiptTotal = this.TaxDueTextBox.DecimalTextBoxValue + this.InterestDueTextBox.DecimalTextBoxValue;
                    this.ReceiptTotalTextBox.Text = receiptTotal.ToString();

                    break;
                case PaymentOptionTypes.Partial:
                    this.ReceiptTotalTextBox.Text = this.PaymentTotalTextBox.Text;
                    if (calculateInterestDue)
                    {
                        this.InterestDueTextBox.Text = "0";
                    }

                    this.CalcIntButton.Enabled = false;

                    taxDue = this.PaymentTotalTextBox.DecimalTextBoxValue - this.InterestDueTextBox.DecimalTextBoxValue;
                    this.TaxDueTextBox.Text = taxDue.ToString();

                    break;
            }
        }    

        /// <summary>
        /// Handles the Leave event of the TaxDueTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TaxDueTextBox_Leave(object sender, EventArgs e)
        {
            if (this.PageMode.Equals(PageModeTypes.NewReceipt) && !this.PaymentOption.Equals(PaymentOptionTypes.Partial))
            {
                TextBox sourceTextBox = sender as TextBox;

                if (CheckAmountDue(sourceTextBox))
                {
                    if (this.StatementBalanceTextBox.DecimalTextBoxValue < this.TaxDueTextBox.DecimalTextBoxValue)
                    {
                        ////TODO title hardcode - needs some clarification

                        MessageBox.Show(SharedFunctions.GetResourceString("CompareTaxDue"), ConfigurationManager.AppSettings["ApplicationName"], MessageBoxButtons.OK, MessageBoxIcon.Information);
                        sourceTextBox.Text = "0.00";
                        sourceTextBox.Focus();
                    }
                }

                ////calculating related values

                this.CalculateDueAmount(false, true);
                this.CalculateBalance();
            }
        }

        /// <summary>
        /// Handles the Leave event of the InterestDueTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InterestDueTextBox_Leave(object sender, EventArgs e)
        {
            if (this.PageMode.Equals(PageModeTypes.NewReceipt))
            {
                TextBox sourceTextBox = sender as TextBox;

                CheckAmountDue(sourceTextBox);

                ////calculating related values

                this.CalculateDueAmount(false, false);
                this.CalculateBalance();
            }
        }

        #endregion

        #region New Receipt

        /// <summary>
        /// Handles the Click event of the NewReceiptButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NewReceiptButton_Click(object sender, EventArgs e)
        {
            if (this.NewReceiptButton.Enabled)
            {
                this.NewReceiptButton.Focus();
                this.Cursor = Cursors.WaitCursor;
                this.SetButtons(this, (int)UserControlCommon.ButtonActionMode.NewMode);
                ////setting the pagemode and prevreceipt id
                this.PageMode = PageModeTypes.NewReceipt;
                this.CurrentReceiptId = null;

                ////display default header and payment details for new receipt
                this.DisplayHeaderDetails();
                this.CalculateDueAmount(true, true);
                this.PopulatePaymentGrid(true);
                this.AttachmentCount();
                this.CommentCount();

                ////set default controls property for new receipt
                this.TaxDueTextBox.TabStop = true;
                this.InterestDueTextBox.TabStop = true;
                this.ReceiptDateTextBox.TabStop = true;
                this.InterestDateTextBox.TabStop = true;
                this.SetButtonsProperty();
                this.Cursor = Cursors.Default;
                this.TaxDueTextBox.Enabled = true;

                this.ReceiptHistoryGridView.CurrentCell = null;
                this.AuditLinkLabel.Text = "tTR_Rcpt[ReceiptID]";
                this.AuditLinkLabel.Enabled = false;
                ////focus - checknum field of first row
                this.PaymentItemsGridView.CurrentCell = this.PaymentItemsGridView[this.PaymentItemsGridView.Columns[this.receiptEngine.PaymentItems.CheckNumberColumn.ColumnName].Index, 0];
            }
        }

        #endregion

        #region Save Engine

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (this.SaveButton.Enabled)
            {
                this.SaveButton.Focus();
                this.SaveReceipt(false);
            }
        }

        /// <summary>
        /// Saves the receipt.
        /// </summary>
        /// <param name="onclose">if set to <c>true</c> [on close].</param>
        /// <returns>true - sucessfull save</returns>
        private bool SaveReceipt(bool onclose)
        {
            DateTime outReceiptDate;
            DateTime outInterestDate;           
            string paymentItems = null;
            decimal totalAmount;
            decimal interestAmount;
            decimal taxDueAmount;
            DataView tempDataView = new DataView();

            if (String.IsNullOrEmpty(this.ReceiptDateTextBox.Text.Trim()))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationManager.AppSettings["ApplicationName"], MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ReceiptDateTextBox.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(this.InterestDateTextBox.Text.Trim()))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationManager.AppSettings["ApplicationName"], MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InterestDateTextBox.Focus();
                return false;
            }

            ////checks the receiptdate 
            if (!DateTime.TryParse(this.ReceiptDateTextBox.Text.Trim(), out outReceiptDate))
            {
                outReceiptDate = System.DateTime.Today;
            }

            ////checks for valid taxdue and finding save/batch
            decimal.TryParse(this.TaxDueTextBox.Text.Trim(), NumberStyles.Currency, null, out taxDueAmount);
            if (!String.IsNullOrEmpty(this.StatementBalanceTextBox.Text) && taxDueAmount >= 0)
            {
                if (taxDueAmount < this.StatementFees)
                {
                    ////TODO title hardcode - needs some clarification
                    MessageBox.Show(SharedFunctions.GetResourceString("CompareFees"), ConfigurationManager.AppSettings["ApplicationName"], MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                if (decimal.Parse(this.StatementBalanceTextBox.Text, NumberStyles.Currency) < taxDueAmount)
                {
                    ////TODO title hardcode - needs some clarification
                    MessageBox.Show(SharedFunctions.GetResourceString("CompareTaxDue"), ConfigurationManager.AppSettings["ApplicationName"], MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                if (!String.IsNullOrEmpty(this.BalanceTextBox.Text))
                {
                    if (decimal.Parse(this.BalanceTextBox.Text, NumberStyles.Currency) != 0)
                    {
                        ////TODO title hardcode - needs some clarification

                        if (MessageBox.Show(SharedFunctions.GetResourceString("BatchSave"), string.Concat(ConfigurationManager.AppSettings["ApplicationName"], " - ", "Unpaid Receipt(s)"), MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return false;
                        }
                    }
                    else  ////getting paymentitems into xmlstring
                    {
                        DataSet tempDataSet = new DataSet("Root");
                        this.receiptEngine.PaymentItems.AcceptChanges();
                        tempDataView = new DataView(this.receiptEngine.PaymentItems.Copy(), string.Concat(this.receiptEngine.PaymentItems.TenderTypeIDColumn.ColumnName, " IS NOT NULL AND ", this.receiptEngine.PaymentItems.AmountColumn.ColumnName, " <> '0.00'"), "", DataViewRowState.CurrentRows);
                        DataTable tempDataTable = tempDataView.ToTable();
                        tempDataTable.TableName = "Table";
                        tempDataSet.Tables.Add(tempDataTable);
                        paymentItems = tempDataSet.GetXml();
                        tempDataView = tempDataTable.DefaultView;
                        tempDataView.RowFilter = "TenderTypeID = 2";
                    }
                }
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("InvalidTaxDue"), ConfigurationManager.AppSettings["ApplicationName"], MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            this.Cursor = Cursors.WaitCursor;
            ////checks for valid receipt
            string validResult = WSHelper.GetValidReceiptTest(this.statementId.Value, outReceiptDate);
            if (!String.IsNullOrEmpty(validResult))
            {
                ////TODO title hardcode - needs some clarification
                MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("InvalidReceiptHeader"), validResult), string.Concat(ConfigurationManager.AppSettings["ApplicationName"], " - ", "Invalid Receipt"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
                return false;
            }            

            ////checks valid interestdate

            if (!DateTime.TryParse(this.InterestDateTextBox.Text, out outInterestDate))
            {
                outInterestDate = System.DateTime.Today;
            }

            decimal.TryParse(this.ReceiptTotalTextBox.Text, NumberStyles.Currency, null, out totalAmount);
            decimal.TryParse(this.InterestDueTextBox.Text.Trim(), NumberStyles.Currency, null, out interestAmount);

            ////save receipt

            if (this.PageMode.Equals(PageModeTypes.NewReceipt))
            {
                if (totalAmount > 0)
                {
                    this.receiptEngine.SaveReceipt.Rows.Clear();
                    ReceiptEngineData.SaveReceiptRow dr = this.receiptEngine.SaveReceipt.NewSaveReceiptRow();
                    dr.StatementID = this.StatementId.Value;
                    dr.ReceiptDate = outReceiptDate.ToString();
                    dr.UserID = TerraScanCommon.UserId;
                    dr.InterestDate = outInterestDate.ToString();
                    dr.InterestAmount = interestAmount;
                    dr.TotalAmount = totalAmount;
                    this.receiptEngine.SaveReceipt.Rows.Add(dr);

                    DataSet tempDataSet = new DataSet("Root");
                    tempDataSet.Tables.Add(this.receiptEngine.SaveReceipt.Copy());
                    tempDataSet.Tables[0].TableName = "Table";

                    this.receiptEngine.SaveReceiptResult.Clear();
                    this.receiptEngine.SaveReceiptResult.Merge(WSHelper.SaveReceipt(tempDataSet.GetXml(), paymentItems, TerraScanCommon.UserId).SaveReceiptResult);

                    if (this.receiptEngine.SaveReceiptResult.Rows.Count > 0)
                    {
                        ////alert for refund
                        if (tempDataView.Count > 0)
                        {
                            if (MessageBox.Show(SharedFunctions.GetResourceString("RefundNow"), String.Concat(ConfigurationManager.AppSettings["ApplicationName"], " - ", SharedFunctions.GetResourceString("RefundTitle")), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                object[] optionalParameter = new object[] { this.receiptEngine.SaveReceiptResult.Rows[0][this.receiptEngine.SaveReceiptResult.PPaymentIDColumn] };
                                ////object[] optionalParameter = new object[] { this.PPaymentId };
                                Form refundManagementform = TerraScan.Common.TerraScanCommon.GetForm(1070, optionalParameter, this.parentWorkItem);
                                if (refundManagementform != null)
                                {
                                    refundManagementform.MdiParent = TerraScan.Common.TerraScanCommon.mdiparent;
                                    refundManagementform.Show();
                                }
                            }
                        }

                        ////triggering event for print
                        this.OnSavedEvent(Convert.ToInt32(this.receiptEngine.SaveReceiptResult.Rows[0][this.receiptEngine.SaveReceiptResult.ReceiptIDColumn]));

                        ////triggering event for delinquency
                        this.OnDelinquentChanged(Convert.ToBoolean(this.receiptEngine.SaveReceiptResult.Rows[0][this.receiptEngine.SaveReceiptResult.IsDelinquentColumn]));
                    }
                }
                else
                {
                    ////TODO title hardcode - needs some clarification
                    MessageBox.Show(SharedFunctions.GetResourceString("InvalidReceiptTotal"), ConfigurationManager.AppSettings["ApplicationName"], MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return false;
                }

                this.PageMode = PageModeTypes.Viewing;
                if (onclose)
                {
                    return true;
                }
            }

            ////populate historygrid with the newly saved receipt
            this.SetButtons(this, (int)UserControlCommon.ButtonActionMode.SaveMode);
            this.PopulateHistoryGrid();
            this.Cursor = Cursors.Default;

            return true;
        }

        #endregion

        #region Cancel

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.CancelReceiptButton.Focus();
            ////prompt for canceling the receipt, - //TODO title hardcode - needs some clarification
            ////if (MessageBox.Show(SharedFunctions.GetResourceString("Cancel"), ConfigurationManager.AppSettings["ApplicationName"], MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            this.Cursor = Cursors.WaitCursor;
            this.PerformCancel();
            this.Cursor = Cursors.Default;

            this.FocusRequiredInputField();
        }

        /// <summary>
        /// Performs the cancel.
        /// </summary>
        private void PerformCancel()
        {
            this.PageMode = PageModeTypes.Viewing;
            int recCount = this.ReceiptHistoryGridView.OriginalRowCount;
            ////for (recCount = this.ReceiptHistoryGridView.Rows.Count; recCount > 1; recCount--)
            ////{
            ////    if (!String.IsNullOrEmpty(this.ReceiptHistoryGridView.Rows[recCount - 1].Cells["ID"].Value.ToString()))
            ////    {
            ////        break;
            ////    }
            ////}

            if (recCount > 1)
            {
                if (this.ReceiptHistoryGridView.CurrentCell == null || this.ReceiptHistoryGridView.CurrentCell.RowIndex == 0)
                {
                    this.SetDataGridViewPosition(recCount - 1);
                }
                else
                {
                    if (this.ReceiptHistoryGridView.CurrentRowIndex > 0)
                    {
                        this.PopulatePayment(this.ReceiptHistoryGridView.CurrentRowIndex);
                    }
                    else
                    {
                        this.PopulatePayment(recCount - 1);
                    }
                }
            }
            else
            {
                this.SetButtons(this, (int)UserControlCommon.ButtonActionMode.CancelMode);
                this.ClearReceiptDetails(false);
            }
        }
        #endregion

        #region Interest Calculation

        /// <summary>
        /// Handles the Click event of the CalcIntButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CalcIntButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            this.CalculateDueAmount(false, true);
            this.CalculateBalance();

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Gets Calculated Interest value Value for a statement from the database 
        /// </summary>
        /// <returns> interest due value</returns>
        private string CalculateInterestDue()
        {
            if (this.statementId.HasValue)
            {
                return WSHelper.GetInterestAmount(this.StatementId.Value, this.InterestDateTextBox.Text, this.TaxDueTextBox.DecimalTextBoxValue).ToString();
            }

            return "0.00";
        }

        #endregion

        #region User Defined Function

        /// <summary>
        /// set focus to the next/previous input field
        /// </summary>
        private void FocusRequiredInputField()
        {
            if (this.ReceiptHistoryGridView.OriginalRowCount > 1)
            {
                this.ReceiptHistoryGridView.Focus();
            }
            else if (this.NewReceiptButton.Enabled)
            {
                this.ActiveControl = this.NewReceiptButton;
            }
        }

        /// <summary>
        /// Populates the payment.
        /// </summary>
        /// <param name="rowIndex">current rowindex of the grid</param>        
        private void PopulatePayment(int rowIndex)
        {
            if (rowIndex > 0)
            {
                this.previousRowIndex = rowIndex;
                ////populating header and paymentgrid depending on the stored values.
                if (!String.IsNullOrEmpty(this.ReceiptHistoryGridView.Rows[rowIndex].Cells[this.receiptEngine.ListHistoryGrid.ItemColumn.ColumnName].Value.ToString().Trim()) && String.Compare(this.ReceiptHistoryGridView.Rows[rowIndex].Cells[this.receiptEngine.ListHistoryGrid.ItemColumn.ColumnName].Value.ToString().Trim(), "STATEMENT", true) != 0)
                {
                    this.CurrentReceiptId = Convert.ToInt32(this.ReceiptHistoryGridView.Rows[rowIndex].Cells[this.receiptEngine.ListHistoryGrid.IDColumn.ColumnName].Value);
                    this.receiptEngine.GetReceiptDetails.Clear();
                    this.PaymentItemsGridView.CurrentCell = null;
                    this.receiptEngine.PaymentItems.Clear();
                    this.receiptEngine.Merge(WSHelper.GetReceiptDetails(this.currentReceiptId.Value));

                    ////triggering event for print
                    ////this.OnSavedEvent(Convert.ToInt32(this.PreviousReceiptId));

                    if (this.receiptEngine.GetReceiptDetails.Rows.Count > 0)
                    {
                        this.DisplayHeaderDetails();
                        this.PopulatePaymentGrid(false);
                        this.SetButtonsProperty();
                    }

                    this.AttachmentCount();
                    this.CommentCount();
                    this.AuditLinkLabel.Text = "tTR_Rcpt[ReceiptID] " + this.ReceiptHistoryGridView.Rows[rowIndex].Cells["ID"].Value.ToString();
                    this.AuditLinkLabel.Enabled = true;
                }
                else
                {
                    ////clearing receipt details

                    this.ClearReceiptDetails(false);
                }
            }
        }

        /// <summary>
        /// Filldts the payments.
        /// </summary>
        private void FillpaymentsDataTable()
        {
            decimal paymentTotal = 0;
            decimal amountValue = 0;

            for (int counter = 0; counter < this.receiptEngine.PaymentItems.Rows.Count; counter++)
            {
                this.receiptEngine.PaymentItems.Rows[counter][this.receiptEngine.PaymentItems.TenderTypeColumn] = this.receiptEngine.PaymentItems.Rows[counter][this.receiptEngine.PaymentItems.TenderTypeIDColumn];
                ////calculate paymenttotal
                decimal.TryParse(this.receiptEngine.PaymentItems.Rows[counter][this.receiptEngine.PaymentItems.AmountColumn].ToString(), out amountValue);
                paymentTotal += amountValue;
            }

            if (this.PageMode.Equals(PageModeTypes.NewReceipt))
            {
                this.receiptEngine.PaymentItems.Clear();
                ReceiptEngineData.PaymentItemsRow dr = this.receiptEngine.PaymentItems.NewPaymentItemsRow();

                dr.TenderType = "3";
                dr.PaidBy = this.ownerName;
                dr.Amount = this.ReceiptTotalTextBox.DecimalTextBoxValue;
                dr.TenderTypeID = 3;
                dr.ModuleID = this.ParentFormId;
                this.receiptEngine.PaymentItems.Rows.Add(dr);
                paymentTotal = this.ReceiptTotalTextBox.DecimalTextBoxValue;
            }

            ////assigns paymenttotal
            this.PaymentTotalTextBox.Text = paymentTotal.ToString();
            ////returns calculated balance value
            this.CalculateBalance();

            for (int counter = this.receiptEngine.PaymentItems.Rows.Count; counter < 3; counter++)
            {
                ReceiptEngineData.PaymentItemsRow dr = this.receiptEngine.PaymentItems.NewPaymentItemsRow();
                dr.ModuleID = this.ParentFormId;
                this.receiptEngine.PaymentItems.Rows.Add(dr);
            }

            if (this.receiptEngine.PaymentItems.Rows.Count == 3)
            {
                this.PaymentGridVscrollBar.Visible = true;
            }
            else
            {
                this.PaymentGridVscrollBar.Visible = false;
            }
        }

        /// <summary>
        /// sets status buttons color with PPaymentId
        /// </summary>
        /// <param name="ppaymentId">balance-zero represents paid else batched receipt</param>
        /// <param name="postId">The post id.</param>
        private void SetStatusButtonsProperty(int ppaymentId, int postId)
        {
            ////changing colors of the paid and notposted flags, depending on balance

            if (ppaymentId != 0)
            {
                this.PaidButton.Text = "Paid";
                this.PaidButton.BackColor = Color.FromArgb(71, 133, 85);

                if (postId != 0)
                {
                    this.NotPostedButton.Text = "Posted";
                    this.NotPostedButton.BackColor = Color.FromArgb(71, 133, 85);
                }
                else
                {
                    this.NotPostedButton.Text = "Not Posted";
                    this.NotPostedButton.BackColor = Color.FromArgb(128, 0, 0);
                }
            }
            else
            {
                this.PaidButton.Text = "Not Paid";
                this.PaidButton.BackColor = Color.FromArgb(128, 0, 0);

                this.NotPostedButton.Text = "Not Posted";
                this.NotPostedButton.BackColor = Color.FromArgb(128, 0, 0);
            }
        }

        /// <summary>
        /// Calculatings the balance.
        /// </summary>       
        private void CalculateBalance()
        {
            ////calculating total balance

            this.ReceiptBalance = this.ReceiptTotalTextBox.DecimalTextBoxValue - this.PaymentTotalTextBox.DecimalTextBoxValue;
            this.BalanceTextBox.Text = this.ReceiptBalance.ToString();

            ////changing colors of the paid and notposted flags, depending on balance

            if (this.ReceiptBalance == 0)
            {
                this.BalanceTextBox.BackColor = Color.FromArgb(130, 189, 140);
                this.BalanceTextBox.ForeColor = Color.Black;
            }
            else
            {
                this.BalanceTextBox.BackColor = Color.FromArgb(128, 0, 0);
                this.BalanceTextBox.ForeColor = Color.White;
            }
        }

        /// <summary>
        /// enbling/disabling buttons
        /// </summary>
        private void SetButtonsProperty()
        {
            ////setting controls property depending on pagemode

            switch (this.PageMode)
            {
                case PageModeTypes.NewReceipt:
                    this.NewReceiptButton.Enabled = false;
                    this.CancelReceiptButton.Enabled = true;
                    this.CalcIntButton.Enabled = true;
                    this.SelectPaymentPanel.Enabled = true;
                    this.ReceiptHeaderPanel.Enabled = true;
                    this.ReceiptDateCalenderButton.Enabled = true;
                    this.InterestDateCalenderButton.Enabled = true;
                    this.InterestDateTextBox.LockKeyPress = false;
                    this.ReceiptDateTextBox.LockKeyPress = false;
                    this.CommentsButton.Enabled = false;
                    this.AttachmentsButton.Enabled = false;
                    if (!String.IsNullOrEmpty(this.ReceiptTotalTextBox.Text) && Decimal.Parse(this.ReceiptTotalTextBox.Text, NumberStyles.Currency) > 0)
                    {
                        this.SaveButton.Enabled = true;
                    }
                    else
                    {
                        this.SaveButton.Enabled = false;
                    }

                    this.SetReadOnlyToGrid(false);
                    this.TaxDueTextBox.TabStop = true;
                    this.InterestDueTextBox.TabStop = true;
                    this.ReceiptDateTextBox.TabStop = true;
                    this.InterestDateTextBox.TabStop = true;
                    this.PaymentItemsGridView.TabStop = true;
                    this.ReceiptHistoryGridView.TabStop = false;
                    ////this.PaymentItemsGridView.Focus();
                    break;

                default:
                    if (!String.IsNullOrEmpty(this.StatementBalanceTextBox.Text) && Decimal.Parse(this.StatementBalanceTextBox.Text, NumberStyles.Currency) > 0)
                    {
                        this.NewReceiptButton.Enabled = true && this.NewReceiptButton.ActualPermission;
                    }
                    else
                    {
                        this.NewReceiptButton.Enabled = false;
                    }

                    this.CancelReceiptButton.Enabled = false;
                    this.SaveButton.Enabled = false;
                    this.SelectPaymentPanel.Enabled = false;
                    this.ReceiptHeaderPanel.Enabled = false;
                    this.ReceiptDateCalenderButton.Enabled = false;
                    this.InterestDateCalenderButton.Enabled = false;
                    this.InterestDateTextBox.LockKeyPress = true;
                    this.ReceiptDateTextBox.LockKeyPress = true;
                    this.CalcIntButton.Enabled = false;

                    if (this.CurrentReceiptId.HasValue)
                    {
                        ////this.CommentsButton.Enabled = true && this.CommentsButton.ActualPermission;
                        ////this.AttachmentsButton.Enabled = true && this.AttachmentsButton.ActualPermission;
                        this.CommentsButton.Enabled = true;
                        this.AttachmentsButton.Enabled = true;
                    }
                    else
                    {
                        this.CommentsButton.Enabled = false;
                        this.AttachmentsButton.Enabled = false;
                    }

                    ////this.SetReadOnlyToGrid(true);
                    this.TaxDueTextBox.TabStop = false;
                    this.InterestDueTextBox.TabStop = false;
                    this.ReceiptDateTextBox.TabStop = false;
                    this.InterestDateTextBox.TabStop = false;
                    this.PaymentItemsGridView.TabStop = false;
                    this.ReceiptHistoryGridView.TabStop = true;
                    break;
            }
        }

        /// <summary>
        /// Calculatings the payment total.
        /// </summary>        
        private void CalculatePaymentTotal()
        {
            decimal outDecimalValue;
            decimal paymentsTotal = 0;
            int paymentCount = 0;

            for (int counter = 0; counter < this.receiptEngine.PaymentItems.Rows.Count; counter++)
            {
                if (!String.IsNullOrEmpty(this.receiptEngine.PaymentItems.Rows[counter]["TenderType"].ToString()) && Decimal.TryParse(this.receiptEngine.PaymentItems.Rows[counter]["Amount"].ToString(), out outDecimalValue))
                {
                    paymentsTotal += outDecimalValue;
                    if (outDecimalValue != 0)
                    {
                        paymentCount++;
                    }
                }
            }

            this.PaymentTotalTextBox.Text = paymentsTotal.ToString();

            if (paymentCount == this.receiptEngine.PaymentItems.Rows.Count)
            {
                ReceiptEngineData.PaymentItemsRow dr = this.receiptEngine.PaymentItems.NewPaymentItemsRow();
                dr.ModuleID = this.ParentFormId;
                this.receiptEngine.PaymentItems.Rows.Add(dr);
                this.PaymentGridVscrollBar.Visible = false;
                this.PaymentItemsGridView.Refresh();
            }
        }

        /// <summary>
        /// Clears the receipt details.
        /// </summary>
        /// <param name="isqueryByForm">indicates query by form</param>
        private void ClearReceiptDetails(bool isqueryByForm)
        {
            ////setting default values

            this.CurrentReceiptId = null;
            this.receiptEngine.GetReceiptDetails.Clear();
            this.PaymentItemsGridView.CurrentCell = null;
            this.receiptEngine.PaymentItems.Clear();

            ////clearing receipt header

            this.ReceiptNumberTextBox.Text = "";
            this.ReceiptDateTextBox.Text = "";
            this.ReceivedbyTerraScanTextBox.Text = "";
            this.InterestDateTextBox.Text = "";

            ////clearing amount due fields

            if (isqueryByForm)
            {
                this.TaxDueTextBox.Text = "";
                this.InterestDueTextBox.Text = "";
                this.ReceiptTotalTextBox.Text = "";
                this.MinDueRadioButton.Checked = true;
                this.PaymentOption = PaymentOptionTypes.MinDue;
                this.PageMode = PageModeTypes.QueryByForm;
            }
            else
            {
                this.DisplayAmountDueDetails(null);
                this.PageMode = PageModeTypes.Viewing;
            }

            this.PaymentTotalTextBox.Text = "";
            this.BalanceTextBox.Text = "";
            this.AuditLinkLabel.Text = "tTR_Rcpt [ReceiptID]";
            this.AuditLinkLabel.Enabled = false;

            ////clearing payments grid

            this.PopulatePaymentGrid(true);
            this.SetButtonsProperty();
            //// Sets Status Flag
            this.SetStatusButtonsProperty(0, 0);
            this.AttachmentCount();
            this.CommentCount();
        }
        #endregion

        #region Select Payment

        /// <summary>
        /// Handles the Click event of the SelectPaymentRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SelectPaymentRadioButton_Click(object sender, EventArgs e)
        {
            Object sourceObject = ((RadioButton)sender).Tag;

            ////code for the selected payment option
            if (Equals(sourceObject.GetType(), typeof(PaymentOptionTypes)))
            {
                this.PaymentOption = (PaymentOptionTypes)sourceObject;
            }
            else
            {
                this.PaymentOption = PaymentOptionTypes.MinDue;
            }

            this.CalculateDueAmount(true, true);
            ////Calculating balance for the selected payment option
            this.CalculateBalance();
        }

        /// <summary>
        /// Handles the CellEnter event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 6)
            {
                SendKeys.Send("{TAB}");
            }
        }

        /// <summary>
        /// Handles the Leave event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_Leave(object sender, EventArgs e)
        {
            // this.PaymentItemsGridView.CurrentCell = this.PaymentItemsGridView[0, 0];
            this.PaymentItemsGridView.CurrentCell = null;
        }

        /// <summary>
        /// Handles the CellClick event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.pageMode.Equals(PageModeTypes.NewReceipt) && e.RowIndex >= 0 && this.PaymentItemsGridView["TenderType", e.RowIndex].Value != null && !string.IsNullOrEmpty(this.PaymentItemsGridView["TenderType", e.RowIndex].Value.ToString()))
            {
                this.PaymentItemsGridView["TenderType", e.RowIndex].ReadOnly = false;
                this.PaymentItemsGridView["PaidBy", e.RowIndex].ReadOnly = false;
                this.PaymentItemsGridView["CheckNumber", e.RowIndex].ReadOnly = false;
                this.PaymentItemsGridView["Amount", e.RowIndex].ReadOnly = false;
            }
        }

        /// <summary>
        /// Handles the RowHeaderMouseClick event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && this.PaymentItemsGridView.CurrentCell != null)
            {
                this.PaymentItemsGridView.CurrentCell.ReadOnly = true;
            }
        }

        #endregion

        #region Comments

        /// <summary>
        /// Gets the Comments count from DataBase.
        /// </summary>
        private void CommentCount()
        {
            CommentsData commentsData = new CommentsData();
            if (this.CurrentReceiptId.HasValue)
            {
                commentsData = WSHelper.GetCommentsCount(Convert.ToInt32(this.Tag), this.currentReceiptId.Value, TerraScanCommon.UserId);
                if (commentsData.GetCommentsCount.Rows.Count > 0)
                {
                    if (Convert.ToInt32(commentsData.GetCommentsCount.Rows[0][commentsData.GetCommentsCount.CommentCountColumn]) > 0)
                    {
                        this.CommentsButton.Text = string.Concat("Comment(", commentsData.GetCommentsCount.Rows[0][commentsData.GetCommentsCount.CommentCountColumn], ")");
                    }
                    else
                    {
                        this.CommentsButton.Text = "Comment";
                    }

                    if (Convert.ToBoolean(commentsData.GetCommentsCount.Rows[0][commentsData.GetCommentsCount.PriorityFlagColumn]))
                    {
                        this.CommentsButton.BackColor = Color.FromArgb(255, 0, 0);
                        this.CommentsButton.CommentPriority = true;
                    }
                    else
                    {
                        this.CommentsButton.BackColor = Color.FromArgb(28, 81, 128);
                        this.CommentsButton.CommentPriority = false;
                    }
                }
                else
                {
                    this.CommentsButton.Text = "Comment";
                    this.CommentsButton.BackColor = Color.FromArgb(28, 81, 128);
                    this.CommentsButton.CommentPriority = false;
                }
            }
            else
            {
                this.CommentsButton.Text = "Comment";
                this.CommentsButton.BackColor = Color.FromArgb(28, 81, 128);
                this.CommentsButton.CommentPriority = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the CommentsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CommentsButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (this.CurrentReceiptId.HasValue)
                {
                    object[] optionalParameter = new object[] { 1000, this.CurrentReceiptId.Value, this.ParentFormId };
                    Form comments = new Form();
                    comments = TerraScanCommon.GetForm(9075, optionalParameter, this.ParentWorkItem);
                    if (comments != null)
                    {
                        comments.ShowDialog();
                        this.CommentCount();
                    }
                }
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

        #endregion

        #region Attachment

        /// <summary>
        /// Handles the Click event of the AttachmentsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AttachmentsButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (this.CurrentReceiptId.HasValue)
                {
                    object[] optionalParameter = new object[] { 1000, this.CurrentReceiptId.Value, this.ParentFormId };
                    Form attachment = new Form();
                    attachment = TerraScanCommon.GetForm(9005, optionalParameter, this.parentWorkItem);
                    if (attachment != null)
                    {
                        attachment.ShowDialog();
                        this.AttachmentCount();
                    }
                }
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
        /// Gets the Attachments count from DataBase.
        /// </summary>
        private void AttachmentCount()
        {
            int attachmentCount = 0;
            if (this.CurrentReceiptId.HasValue)
            {
                attachmentCount = WSHelper.GetAttachmentCount(Convert.ToInt32(this.Tag), this.currentReceiptId.Value, TerraScanCommon.UserId);
                if (attachmentCount > 0)
                {
                    this.AttachmentsButton.Text = string.Concat("Attachment(", attachmentCount, ")");
                }
                else
                {
                    this.AttachmentsButton.Text = "Attachment";
                }
            }
            else
            {
                this.AttachmentsButton.Text = "Attachment";
            }
        }

        #endregion

        #region TextBox Validation

        /// <summary>
        /// Reads the onlycontrol.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ReadOnlycontrol(object sender, KeyEventArgs e)
        {
            string sourceTextBoxValue = (sender as TextBox).Tag as string;

            if (!this.PageMode.Equals(PageModeTypes.NewReceipt))
            {
                e.SuppressKeyPress = true;
            }
            else
            {
                if (String.Compare(sourceTextBoxValue, "TAXDUE", true) == 0 && this.PaymentOption.Equals(PaymentOptionTypes.Partial))
                {
                    e.SuppressKeyPress = true;
                }
                else
                {
                    e.SuppressKeyPress = false;
                }
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the axDueTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TaxDueTextBox_TextChanged(object sender, EventArgs e)
        {
            //// changes the interestdue textbox value when tax due value changes

            if (!Enum.Equals(this.PaymentOption, PaymentOptionTypes.Partial))
            {
                this.InterestDueTextBox.Text = Decimal.Zero.ToString();
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the ReceiptTotalTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptTotalTextBox_TextChanged(object sender, System.EventArgs e)
        {
            string sourceTextBoxValue = ((TextBox)sender).Text;

            if (this.PageMode.Equals(PageModeTypes.NewReceipt) && !String.IsNullOrEmpty(sourceTextBoxValue) && Decimal.Parse(sourceTextBoxValue, NumberStyles.Currency) > 0)
            {
                this.SaveButton.Enabled = true;
            }
            else
            {
                this.SaveButton.Enabled = false;
            }
        }

        #endregion

        #region Grid Selection

        ///// <summary>
        ///// Handles the CellClick event of the ReceiptHistoryGridView control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        ////private void ReceiptHistoryGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        ////{
        ////    this.DisplayDetails(e.RowIndex);
        ////}

        /// <summary>
        /// Handles the KeyDown event of the ReceiptHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ReceiptHistoryGridView_KeyDown(object sender, KeyEventArgs e)
        {
            ////if (e.KeyData == Keys.Tab)
            ////{
            ////    e.Handled = false;
            ////}
            ////else
            ////{
            ////    e.Handled = true;
            ////}
            ////if (this.NewReceiptButton.Enabled == true)
            ////{
            ////    this.NewReceiptButton.Focus();
            ////}
            ////else
            ////{
            ////    this.TaxDueTextBox.Focus();
            ////}
        }

        #endregion

        /// <summary>
        /// Handles the CellContentClick event of the ReceiptHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ReceiptHistoryGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (e.ColumnIndex == 2 && e.RowIndex > 0 && this.CurrentReceiptId.HasValue)
                {
                    object[] optionalParameter = new object[] { this.CurrentReceiptId };
                    Form receipt = new Form();
                    receipt = TerraScanCommon.GetForm(1001, optionalParameter, this.ParentWorkItem);
                    if (receipt != null)
                    {
                        receipt.ShowDialog();
                    }
                }
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

        #endregion

        /// <summary>
        /// Handles the LinkClicked event of the AuditLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void AuditLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.currentReceiptId.HasValue)
                {
                    TerraScanCommon.ShowAuditReport("ReceiptID", this.CurrentReceiptId.Value.ToString());
                }
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
        /// Handles the KeyDown event of the ReceiptMonthCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ReceiptMonthCalender_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SetSeletedDate(ReceiptMonthCalender.SelectionStart.ToShortDateString());
            }
        }

        /// <summary>
        /// Handles the CellMouseMove event of the ReceiptHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void ReceiptHistoryGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == 0)
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the ReceiptDateCalenderButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptDateCalenderButton_Click(object sender, EventArgs e)
        {
            this.ShowReceiptDateCalender();
        }

        /// <summary>
        /// Handles the Click event of the InterestDateCalenderButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InterestDateCalenderButton_Click(object sender, EventArgs e)
        {
            this.ShowInterestDateCalender();
        }        
    }
}
