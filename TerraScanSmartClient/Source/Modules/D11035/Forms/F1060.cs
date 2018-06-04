//--------------------------------------------------------------------------------------------
// <copyright file="F1060.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F1060 Suspend Payments Selection Form Functionality
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09-05-2007       Shiva              Created
//*********************************************************************************/
namespace D11035
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Web.Services.Protocols;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
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
    using TerraScan.Common.Reports;
    using TerraScan.Utilities;

    /// <summary>
    /// F1060 Suspend Payment Selection Form User Interface.
    /// </summary>
    public partial class F1060 : Form
    {
        #region Member Variables

        /// <summary>
        /// Set Date Format
        /// </summary>
        private readonly string dateFormat = "M/d/yyyy";

        /// <summary>
        /// variable holds the form1013Controller.
        /// </summary>
        private F1060Controller form1060Controll;

        /// <summary>
        /// variable holds the suspendedPaymentsSelection DataSet.
        /// </summary>
        private F1060SudpendedPaymentSelectionData sudpendedPaymentSelectionData = new F1060SudpendedPaymentSelectionData();

         
        /// <summary>
        /// variable holds the paymentIds Xml String.
        /// </summary>
        private string paymentIdsXml;

        /// <summary>
        /// variable holds the grid items row Count.
        /// </summary>
        private int suspendedItemsRowCount;

        /// <summary>
        /// Variable Holds the Selected Payment Items Count.
        /// </summary>
        private int selectedPaymentItemsCount;

        /// <summary>
        /// variable holds the paymentId's array.
        /// </summary>
        private int[] paymentIds;

        /// <summary>
        /// doubleOperatorArrayList
        /// </summary>
        private ArrayList doubleOperatorArrayList = new ArrayList();

        /// <summary>
        /// singleOperatorArrayList
        /// </summary>
        private ArrayList singleOperatorArrayList = new ArrayList();

        /// <summary>
        /// validSaleDate
        /// </summary>
        private string validSaleDate = string.Empty;

        /// <summary>
        /// IsValidDate
        /// </summary>
        private bool validDateFormat;

        /// <summary>
        /// DatePicker object
        /// </summary>
        private System.Windows.Forms.DateTimePicker validDate = new System.Windows.Forms.DateTimePicker();

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        /// <summary>
        /// new search Table 
        /// </summary>
        private DataTable searchTable = new DataTable(); 

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1060"/> class.
        /// </summary>
        public F1060()
        {
            InitializeComponent();
        }

        #endregion

        #region eventPublication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form15035 controll.
        /// </summary>
        /// <value>The form15035 controll.</value>
        [CreateNew]
        public F1060Controller Form1060Controll
        {
            get { return this.form1060Controll as F1060Controller; }
            set { this.form1060Controll = value; }
        }

        /// <summary>
        /// Gets or sets the payment ids XML.
        /// </summary>
        /// <value>The payment ids XML.</value>
        public string PaymentIdsXml
        {
            get
            {
                return this.paymentIdsXml;
            }

            set
            {
                this.paymentIdsXml = value;
            }
        }

        /// <summary>
        /// Gets or sets the command result.
        /// </summary>
        /// <value>The command result.</value>
        public string CommandResult
        {
            get { return commandResult; }
            set { commandResult = value; }
        }

        #endregion

        #region Form Events

        /// <summary>
        /// Handles the Load event of the F1060 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1060_Load(object sender, EventArgs e)
        {
            try
            {
                this.ClearSuspendedPaymentSelection();
                ////To Custimize SubFund Selection Grid
                this.CustimizeSuspendedPaymentSelectionGrid();
                this.ClearSuspendedPaymentSelectionGrid();
                this.EnableButtons(false);
                this.InitializeOperator();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the SuspendedPaymentClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SuspendedPaymentClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ClearSuspendedPaymentSelection();
                this.ClearSuspendedPaymentSelectionGrid();
                this.EnableButtons(false);
                this.LastNameTextBox.Focus();
                this.DialogResult = DialogResult.None;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the SuspendedPaymentCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SuspendedPaymentCancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the SuspendedPaymentLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void SuspendedPaymentLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11035);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the SuspendedPaymentAcceptButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SuspendedPaymentAcceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.suspendedItemsRowCount > 0)
                {
                    this.SelectedPaymentIds();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the SuspendedPaymentSearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SuspendedPaymentSearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.SearchRecord();
            }
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Searches the record.
        /// </summary>
        private void SearchRecord()
        {
            /// Date Field removed from the form
            
            //if (!string.IsNullOrEmpty(this.DateTextBox.Text.Trim()))
            //{
            //    this.ValidateSaleDate();

            //    if (!this.validDateFormat)
            //    {
            //        return;
            //    }
            //}

            Decimal moneyValue;
            F1060SudpendedPaymentSelectionData.SearchSuspendedPaymentRow searchrow = sudpendedPaymentSelectionData.SearchSuspendedPayment.NewSearchSuspendedPaymentRow();
            if (this.Address1TextBox.Text != string.Empty)
            {
                searchrow.Address1  = this.Address1TextBox.Text;
            }
            if (this.Address2TextBox.Text != string.Empty)
            {
                searchrow.Address2 = this.Address2TextBox.Text;   
            }
            if (this.AmountTextBox.Text != string.Empty)
            {
                if (this.AmountTextBox.Text == "0")
                {
                    //no value
                    searchrow.Amount =0; 
                }
                else
                {
                    Decimal.TryParse(this.AmountTextBox.Text, out moneyValue);
                    searchrow.Amount = moneyValue;
                }
            }
            if (this.LastNameTextBox.Text != string.Empty)
            {
                searchrow.LastName = this.LastNameTextBox.Text;
            }
            if (this.FirstNameTextBox.Text != string.Empty)
            {
                searchrow.FirstName = this.FirstNameTextBox.Text;
            }
            if (this.ReceiptNumberTextBox.Text != string.Empty)
            {
                searchrow.ReceiptNumber = this.ReceiptNumberTextBox.Text;
            }
            if (this.CityTextBox.Text != string.Empty)
            {
                searchrow.City = this.CityTextBox.Text;
            }
           if ( this.OwnerCodeTextBox.Text != string.Empty)
            {
                searchrow.OwnerCode = this.OwnerCodeTextBox.Text;  
            }
           sudpendedPaymentSelectionData.SearchSuspendedPayment.Rows.Add(searchrow);
           sudpendedPaymentSelectionData.SearchSuspendedPayment.AcceptChanges();
           DataSet searchDetail = new DataSet("Root");
           searchDetail.Tables.Add(sudpendedPaymentSelectionData.SearchSuspendedPayment.Copy());
           searchDetail.Tables[0].TableName = "Table";
               //pass the xml tempdataset.getXML;
           //returnValue = this.form27081Controll.WorkItem.F27081_SaveDistrictDetails(tifId, tempDataSet.GetXml(), TerraScanCommon.UserId);
           this.LoadSuspendedPaymentSelectionGrid(searchDetail.GetXml());
        }
    

        #endregion

        #region Grid Events

        /// <summary>
        /// Handles the CellClick event of the SuspendedPaymentSelectionDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SuspendedPaymentSelectionDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (Convert.ToBoolean(this.SuspendedPaymentSelectionDataGridView.Rows[e.RowIndex].Cells[this.sudpendedPaymentSelectionData.ListSuspendedPayment.PaymentCheckColumn.ColumnName].Value))
                    {
                        this.SuspendedPaymentSelectionDataGridView.Rows[e.RowIndex].Cells[this.sudpendedPaymentSelectionData.ListSuspendedPayment.PaymentCheckColumn.ColumnName].Value = false;
                    }
                    else
                    {
                        this.SuspendedPaymentSelectionDataGridView.Rows[e.RowIndex].Cells[this.sudpendedPaymentSelectionData.ListSuspendedPayment.PaymentCheckColumn.ColumnName].Value = true;
                    }

                    bool acceptFlag = false;
                    this.selectedPaymentItemsCount = 0;

                    foreach (DataRow dr in this.sudpendedPaymentSelectionData.ListSuspendedPayment.Rows)
                    {
                        if (!Convert.ToBoolean(dr[this.SuspendedPaymentSelectionDataGridView.EmptyRecordColumnName]))
                        {
                            if (Convert.ToBoolean(dr[this.sudpendedPaymentSelectionData.ListSuspendedPayment.PaymentCheckColumn.ColumnName]))
                            {
                                acceptFlag = true;
                                this.selectedPaymentItemsCount += 1;
                            }
                        }
                    }

                    if (acceptFlag)
                    {
                        this.SuspendedPaymentAcceptButton.Enabled = true;
                    }
                    else
                    {
                        this.SuspendedPaymentAcceptButton.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the SuspendedPaymentSelectionDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void SuspendedPaymentSelectionDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outTotalAmountDecimal;
            try
            {
                //// Only paint if desired, formattable column  TotalAmount Column.

                if (e.ColumnIndex == this.SuspendedPaymentSelectionDataGridView.Columns["TotalAmount"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.SuspendedPaymentSelectionDataGridView.Rows[e.RowIndex].Cells["TotalAmount"].Value.ToString().Trim()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outTotalAmountDecimal))
                        {
                            if (outTotalAmountDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outTotalAmountDecimal).ToString("#,##0.00"), ")");
                                e.CellStyle.ForeColor = Color.Green;
                            }
                            else
                            {
                                e.Value = outTotalAmountDecimal.ToString("#,##0.00");
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
        /// Selects the SubFunDID from the account element mgmt grid.
        /// </summary>
        private void SelectedPaymentIds()
        {
            ////TODO: Need to Write the Logic Based on the Items Selected in the Grid.
            if (this.selectedPaymentItemsCount > 0)
            {
                this.paymentIds = new int[this.selectedPaymentItemsCount];
                int currentRowNum = 0;

                foreach (DataRow dr in this.sudpendedPaymentSelectionData.ListSuspendedPayment.Rows)
                {
                    if (!Convert.ToBoolean(dr[this.SuspendedPaymentSelectionDataGridView.EmptyRecordColumnName]))
                    {
                        if (Convert.ToBoolean(dr[this.sudpendedPaymentSelectionData.ListSuspendedPayment.PaymentCheckColumn.ColumnName]))
                        {
                            this.paymentIds[currentRowNum] = Convert.ToInt32(dr[this.sudpendedPaymentSelectionData.ListSuspendedPayment.PPaymentIDColumn.ColumnName]);
                            currentRowNum += 1;
                        }
                    }
                }

                //// XML String 
                this.paymentIdsXml = string.Empty;
                DataTable tempXMLdataTable = new DataTable();

                foreach (DataColumn column in this.sudpendedPaymentSelectionData.ListSuspendedPayment.Columns)
                {
                    if (column.ColumnName == this.sudpendedPaymentSelectionData.ListSuspendedPayment.PPaymentIDColumn.ColumnName)
                    {
                        tempXMLdataTable.Columns.Add(new DataColumn(column.ColumnName));
                    }
                }

                for (int item = 0; item < this.paymentIds.Length; item++)
                {
                    DataRow tempXMLDataRow = tempXMLdataTable.NewRow();
                    tempXMLDataRow[this.sudpendedPaymentSelectionData.ListSuspendedPayment.PPaymentIDColumn.ColumnName] = this.paymentIds[item].ToString();
                    tempXMLdataTable.Rows.Add(tempXMLDataRow);
                }

                this.paymentIdsXml = TerraScanCommon.GetXmlString(tempXMLdataTable);
                this.commandResult = this.paymentIdsXml;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }
        }

        /// <summary>
        /// Loads the suspended payment selection grid.
        /// </summary>
        /// <param name="lastName">Name of the last.</param>
        /// <param name="firstName">Name of the first.</param>
        /// <param name="date">The date.</param>
        private void LoadSuspendedPaymentSelectionGrid(string SearchDetail)
        {
            this.Cursor = Cursors.WaitCursor;
//            this.sudpendedPaymentSelectionData = this.form1060Controll.WorkItem.F1060_ListSuspendedPayment(lastName, firstName, date);
            this.sudpendedPaymentSelectionData = this.form1060Controll.WorkItem.F1060_ListSuspendedPayment(SearchDetail);
            this.suspendedItemsRowCount = this.sudpendedPaymentSelectionData.ListSuspendedPayment.Rows.Count;
            if (this.suspendedItemsRowCount > 0)
            {
                this.SuspendedPaymentSelectionDataGridView.Enabled = true;
                this.SuspendedPaymentSelectionDataGridView.DataSource = this.sudpendedPaymentSelectionData.ListSuspendedPayment.DefaultView;
                this.SuspendedPaymentSelectionDataGridView.Focus();
                this.SuspendedPaymentSelectionDataGridView.Rows[0].Selected = true;
                if (this.suspendedItemsRowCount > this.SuspendedPaymentSelectionDataGridView.NumRowsVisible)
                {
                    this.ReceiptDate.Width = 104;
                }
                else
                {
                    this.ReceiptDate.Width = 105;
                }
            }
            else
            {
                this.ClearSuspendedPaymentSelectionGrid();
            }

            ////to enable or disable the vertical scroll bar
            if (this.suspendedItemsRowCount > this.SuspendedPaymentSelectionDataGridView.NumRowsVisible)
            {
                this.SuspendedPaymentSlectionVerticalScroll.Visible = false;
            }
            else
            {
                this.SuspendedPaymentSlectionVerticalScroll.Visible = true;
            }

            ////To display the no of display rows in the grid
            this.RecordCountLabel.Text = this.suspendedItemsRowCount + SharedFunctions.GetResourceString("9101MasterNameSearch");
            this.Cursor = Cursors.Default;
        }    
       
        /// <summary>
        /// Clears the suspended payment selection grid.
        /// </summary>
        private void ClearSuspendedPaymentSelectionGrid()
        {
            this.SuspendedPaymentSelectionDataGridView.Enabled = false;
            this.sudpendedPaymentSelectionData.ListSuspendedPayment.Clear();
            this.SuspendedPaymentSelectionDataGridView.DataSource = this.sudpendedPaymentSelectionData.ListSuspendedPayment.DefaultView; //this.sudpendedPaymentSelectionData.ListSuspendedPayment;
            this.SuspendedPaymentSelectionDataGridView.Rows[0].Selected = false;
            this.SuspendedPaymentSlectionVerticalScroll.Visible = true;
            this.SuspendedPaymentAcceptButton.Enabled = false;
        }

        /// <summary>
        /// Clears the suspended payment selection.
        /// </summary>
        private void ClearSuspendedPaymentSelection()
        {
            this.LastNameTextBox.Text = string.Empty;
            this.FirstNameTextBox.Text = string.Empty;
            this.AmountTextBox.Text = string.Empty;
            this.Address1TextBox.Text = string.Empty;
            this.Address2TextBox.Text = string.Empty;
            this.CityTextBox.Text = string.Empty;
            this.OwnerCodeTextBox.Text = string.Empty;
            this.ReceiptNumberTextBox.Text = string.Empty;
            this.RecordCountLabel.Text = string.Empty;
            this.suspendedItemsRowCount = 0;
            this.selectedPaymentItemsCount = 0;
            this.ReceiptDate.Width = 105;
        }

        /// <summary>
        /// Custimizes the suspended payment selection grid.
        /// </summary>
        private void CustimizeSuspendedPaymentSelectionGrid()
        {
            this.SuspendedPaymentSelectionDataGridView.AutoGenerateColumns = false;
            this.SuspendedPaymentSelectionDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.ReceiptDate.Width = 105;
            this.LastName.DataPropertyName = this.sudpendedPaymentSelectionData.ListSuspendedPayment.NameColumn.ColumnName;
            this.ReceiptDate.DataPropertyName = this.sudpendedPaymentSelectionData.ListSuspendedPayment.ReceiptDateColumn.ColumnName;
            this.PPaymentID.DataPropertyName = this.sudpendedPaymentSelectionData.ListSuspendedPayment.PPaymentIDColumn.ColumnName;
            this.ReceiptID.DataPropertyName = this.sudpendedPaymentSelectionData.ListSuspendedPayment.ReceiptIDColumn.ColumnName;
            this.TotalAmount.DataPropertyName = this.sudpendedPaymentSelectionData.ListSuspendedPayment.TotalAmountColumn.ColumnName;
            this.Address.DataPropertyName = this.sudpendedPaymentSelectionData.ListSuspendedPayment.AddressColumn.ColumnName;    
            this.PaymentCheck.DataPropertyName = this.sudpendedPaymentSelectionData.ListSuspendedPayment.PaymentCheckColumn.ColumnName;
        }

        /// <summary>
        /// Enables the buttons.
        /// </summary>
        /// <param name="unlock">if set to <c>true</c> [unlock].</param>
        private void EnableButtons(bool unlock)
        {
            this.SuspendedPaymentSearchButton.Enabled = unlock;
            this.SuspendedPaymentAcceptButton.Enabled = unlock;
            this.SuspendedPaymentClearButton.Enabled = unlock;
        }

        /// <summary>
        /// Enables the search button.
        /// </summary>
        private void EnableSearchButton()
        {
            if (!string.IsNullOrEmpty(this.LastNameTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.FirstNameTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.AmountTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.ReceiptNumberTextBox.Text.Trim())
                ||!string.IsNullOrEmpty(this.Address1TextBox.Text.Trim()) ||!string.IsNullOrEmpty(this.Address2TextBox.Text.Trim())||!string.IsNullOrEmpty(this.CityTextBox.Text.Trim()) ||!string.IsNullOrEmpty(this.OwnerCodeTextBox.Text.Trim()))
            {
                this.SuspendedPaymentSearchButton.Enabled = true;
                this.SuspendedPaymentClearButton.Enabled = true;
            }
            else
            {
                this.SuspendedPaymentSearchButton.Enabled = false;

                if (this.suspendedItemsRowCount <= 0)
                {
                    this.SuspendedPaymentAcceptButton.Enabled = false;
                    this.SuspendedPaymentClearButton.Enabled = false;
                }
                else
                {
                    this.SuspendedPaymentClearButton.Enabled = true;
                }
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
                if ((!string.IsNullOrEmpty(this.LastNameTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.FirstNameTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.AmountTextBox.Text.Trim()))||!string.IsNullOrEmpty(this.OwnerCodeTextBox.Text.Trim())
                    ||!string.IsNullOrEmpty(this.Address1TextBox.Text.Trim()) ||!string.IsNullOrEmpty(this.Address2TextBox.Text.Trim())||!string.IsNullOrEmpty(this.CityTextBox.Text.Trim()) ||!string.IsNullOrEmpty(this.ReceiptNumberTextBox.Text.Trim()))
                {
                    this.EnableSearchButton();
                }
                else
                {
                    if (this.suspendedItemsRowCount > 0)
                    {
                        this.SuspendedPaymentSearchButton.Enabled = false;
                    }
                    else
                    {
                        this.EnableButtons(false);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        {
                            this.SearchRecord();
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
            }
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        #endregion

        #region Date Validation Methods

        /// <summary>
        /// Initializes the operator.
        /// </summary>
        private void InitializeOperator()
        {
            // doubleOperatorArrayList            
            this.doubleOperatorArrayList.Add("<=");
            this.doubleOperatorArrayList.Add(">=");
            this.doubleOperatorArrayList.Add("!=");
            this.doubleOperatorArrayList.Add("<>");

            // singleOperatorArrayList            
            this.singleOperatorArrayList.Add("<");
            this.singleOperatorArrayList.Add(">");
            this.singleOperatorArrayList.Add("=");
        }

        /// <summary>
        /// Validates the sale date.
        /// </summary>
        private void ValidateSaleDate()
        {
            string saleDateOperator = string.Empty;
            string expression1 = string.Empty;
            //saleDateOperator = this.DateTextBox.Text.Trim();

            this.validDate.CustomFormat = this.dateFormat; //// "m/d/yyyy";
            this.validDate.MaxDate = new System.DateTime(2079, 6, 6, 0, 0, 0, 0);
            this.validDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);

            if (saleDateOperator.Length > 2)
            {
                expression1 = saleDateOperator.Substring(0, 2).ToString();
            }

            string expression2 = saleDateOperator.Substring(0, 1).ToString();

            if (this.doubleOperatorArrayList.Contains(expression1))
            {
                this.validSaleDate = saleDateOperator.Substring(2).Trim();
                this.ValidateDate(this.validSaleDate, expression1);
            }
            else if (this.singleOperatorArrayList.Contains(expression2))
            {
                this.validSaleDate = saleDateOperator.Substring(1).Trim();
                this.ValidateDate(this.validSaleDate, expression2);
            }
            else
            {
                this.ValidateDate(saleDateOperator, null);
            }
        }

        /// <summary>
        /// Validates the date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="expression">The expression.</param>
        private void ValidateDate(string date, string expression)
        {
            try
            {
                this.validDate.Value = DateTime.Parse(date);
               // this.DateTextBox.Text = expression + this.validDate.Value.ToString(this.dateFormat);
                this.validDateFormat = true;
            }
            catch (Exception)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("ValidDate") + "\n" + SharedFunctions.GetResourceString("Allowed") + "\n" + SharedFunctions.GetResourceString("MinDate") + this.validDate.MinDate.ToShortDateString() + "\n" + SharedFunctions.GetResourceString("MaxDate") + this.validDate.MaxDate.ToShortDateString(), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //this.DateTextBox.Focus();
                this.validDateFormat = false;
            }
        }

        #endregion
    }
}