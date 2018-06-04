// --------------------------------------------------------------------------------------------
// <copyright file="F1430.cs" company="Congruent">
//     Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
// This file contains methods for the F1430.
// </summary>
// **********************************************************************************
// Date               Author            Description
// ----------        ------------       -------------------------------------
// 06 Dec 07         Jaya prakash       Created
// 21 Aug 09         Sadha Shivudu      Implemented TSCO #
// *********************************************************************************/

namespace D11020
{
    #region Namespace

    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;

    #endregion Namespace

    /// <summary>
    /// Inheriting Base page functionality
    /// </summary>
    public partial class F1430 : Form
    {
        #region Variables

        /// <summary>
        /// instance variable to hold the date format value
        /// </summary>
        private string dateFormat = "M/d/yyyy";

        /// <summary>
        /// instance variable to hold the tax amount value.
        /// </summary>
        private decimal taxAmount;

        /// <summary>
        /// instance variable to hold the fee amount value.
        /// </summary>
        private decimal feeAmount;

        /// <summary>
        /// instance variable to hold the interest amount value
        /// </summary>
        private decimal interestAmount;

        /// <summary>
        /// instance variable to hold the penalty amount value
        /// </summary>
        private decimal penaltyAmount;

        /// <summary>
        /// instance variable to hold the total amount value
        /// </summary>
        private decimal totalAmount;

        /// <summary>
        /// instance variable to hold the interest date value
        /// </summary>
        private DateTime interestDate = DateTime.Now;

        private decimal firstHalf;

        private DateTime graceDate;

        /// <summary>
        /// instance variable to hold the statementId value
        /// </summary>
        private int statementId;

        /// <summary>
        /// instance variable to hold the interest calculator data set
        /// </summary>
        private F1430InterestCalculatorData interestCalcDataSet = new F1430InterestCalculatorData();

        /// <summary>
        /// instance variable to hold the form controller value
        /// </summary>
        private F1430Controller form1430Control;

        /// <summary>
        /// instance variable to hold the either tax or interest date changed flag.
        /// </summary>
        private bool taxOrinterestDateChanged;

        /// <summary>
        /// instance variable to hold the page load status.
        /// </summary>
        private bool pageLoadStatus;

        #endregion Variables

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F1430"/> class.
        /// </summary>
        public F1430()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F1430"/> class.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        public F1430(int statementId)
        {
            InitializeComponent();
            this.statementId = statementId;
        }

        #endregion  Constructor

        #region Property

        /// <summary>
        /// Gets or sets the form F1430 control.
        /// </summary>
        /// <value>The form F1430 control.</value>
        [CreateNew]
        public F1430Controller FormF1430Control
        {
            get { return this.form1430Control as F1430Controller; }
            set { this.form1430Control = value; }
        }

        /// <summary>
        /// Gets or sets the tax amount.
        /// </summary>
        /// <value>The tax amount.</value>
        public decimal TaxAmount
        {
            get { return this.taxAmount; }
            set { this.taxAmount = value; }
        }

        /// <summary>
        /// Gets or sets the fee amount.
        /// </summary>
        /// <value>The fee amount.</value>
        public decimal FeeAmount
        {
            get { return this.feeAmount; }
            set { this.feeAmount = value; }
        }

        /// <summary>
        /// Gets or sets the interest amount.
        /// </summary>
        /// <value>The interest amount.</value>
        public decimal InterestAmount
        {
            get { return this.interestAmount; }
            set { this.interestAmount = value; }
        }

        /// <summary>
        /// Gets or sets the penalty amount.
        /// </summary>
        /// <value>The penalty amount.</value>
        public decimal PenaltyAmount
        {
            get { return this.penaltyAmount; }
            set { this.penaltyAmount = value; }
        }

        /// <summary>
        /// Gets or sets the interest date.
        /// </summary>
        /// <value>The interest date.</value>
        public DateTime InterestDate
        {
            get { return this.interestDate; }
            set { this.interestDate = value; }
        }

        public decimal FirstHalf
        {
            get { return this.firstHalf; }
            set { this.firstHalf = value; }
        }

        public DateTime GraceDate
        {
            get { return this.graceDate; }
            set { this.graceDate = value; }
        }

        /// <summary>
        /// Gets or sets the interest total.
        /// </summary>
        /// <value>The interest total.</value>
        public decimal InterestTotal
        {
            get { return this.totalAmount; }
            set { this.totalAmount = value; }
        }

        #endregion Property

        #region Events

        /// <summary>
        /// Handles the Load event of the F1430 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F1430_Load(object sender, EventArgs e)
        {
            try
            {
                this.pageLoadStatus = true;
                this.PopulateInterestCalculatorFields();
                this.ActiveControl = this.PayComboBox;
                this.pageLoadStatus = false;
                this.PayComboBox.Focus();
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
        /// Handles the SelectionChangeCommitted event of the PayComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PayComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.PayComboBox.SelectedValue.ToString())
                    && this.interestCalcDataSet.GetPayDetails.Rows.Count > 0)
                {
                    DataRow[] payDetailsRow;
                    payDetailsRow = this.interestCalcDataSet.GetPayDetails.Select(this.interestCalcDataSet.GetPayDetails.PayTypeColumn.ColumnName + " = " + "'" + this.PayComboBox.SelectedValue.ToString() + "'");

                    if (payDetailsRow.Length > 0)
                    {
                        decimal.TryParse(payDetailsRow[0][this.interestCalcDataSet.GetPayDetails.PayAmountColumn.ColumnName].ToString(), out this.taxAmount);
                        this.TaxTextBox.Text = this.taxAmount.ToString();
                    }

                    // get the intrest details
                    this.GetInterestAndPenaltyDetails();

                    // calculate the total
                    this.CalculateFieldsTotalValue(sender);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the PayComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PayComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.pageLoadStatus)
                {
                    if (!string.IsNullOrEmpty(this.PayComboBox.Text.Trim())
                           && this.interestCalcDataSet.GetPayDetails.Rows.Count > 0)
                    {
                        DataRow[] payDetailsRow;
                        payDetailsRow = this.interestCalcDataSet.GetPayDetails.Select(this.interestCalcDataSet.GetPayDetails.PayLabelColumn.ColumnName + " = " + "'" + this.PayComboBox.Text.Trim().Replace("'", "''") + "'");

                        if (payDetailsRow.Length > 0)
                        {
                            decimal.TryParse(payDetailsRow[0][this.interestCalcDataSet.GetPayDetails.PayAmountColumn.ColumnName].ToString(), out this.taxAmount);
                            this.TaxTextBox.Text = this.taxAmount.ToString();
                        }

                        // get the intrest details
                        this.GetInterestAndPenaltyDetails();

                        // calculate the total
                        this.CalculateFieldsTotalValue(sender);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the TaxTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Control sourceControl = (Control)sender;

                // check for the control type of terrascanTextbox
                if (sourceControl.GetType().Equals(typeof(TerraScanTextBox)))
                {
                    if (sourceControl.Name.Equals(this.TaxTextBox.Name)
                        || sourceControl.Name.Equals(this.InterestDateTextBox.Name))
                    {
                        this.taxOrinterestDateChanged = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void TextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Control sourceControl = (Control)sender;

                // check for the control type of terrascanTextbox
                if (sourceControl.GetType().Equals(typeof(TerraScanTextBox)))
                {
                    if (sourceControl.Name.Equals(this.TaxTextBox.Name)
                        || sourceControl.Name.Equals(this.InterestTextBox.Name)
                        || sourceControl.Name.Equals(this.FeesTextBox.Name)
                        || sourceControl.Name.Equals(this.GraceDateTextBox.Name))
                    {
                        decimal moneyMaxValue = 922337203685477.5807M;
                        decimal sourceControlValue;
                        if (decimal.TryParse(sourceControl.Text, out sourceControlValue))
                        {
                            if (sourceControlValue > moneyMaxValue)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("MaxLimitValidationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                sourceControl.Text = decimal.Zero.ToString();
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
        /// Handles the Validated event of the TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TextBox_Validated(object sender, EventArgs e)
        {
            try
            {
                Control sourceControl = (Control)sender;

                // check for the control type of terrascanTextbox
                if (sourceControl.GetType().Equals(typeof(TerraScanTextBox)))
                {
                    if (sourceControl.Name.Equals(this.InterestDateTextBox.Name)
                        || sourceControl.Name.Equals(this.TaxTextBox.Name))
                    {
                        if (this.taxOrinterestDateChanged)
                        {
                            // get the interest and penalty details
                            this.GetInterestAndPenaltyDetails();

                            // reset the flag once interest details fetch
                            this.taxOrinterestDateChanged = false;
                        }
                    }

                    // calculate the total and display
                    this.CalculateFieldsTotalValue(sender);
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
        }

        /// <summary>
        /// Handles the Click event of the InterestDateCalenderButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void InterestDateCalenderButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.InterestMonthCalender.Visible = true;
                this.InterestMonthCalender.BringToFront();
                this.InterestMonthCalender.Focus();

                if (!string.IsNullOrEmpty(this.InterestDateTextBox.Text))
                {
                    this.InterestMonthCalender.SetDate(Convert.ToDateTime(this.InterestDateTextBox.Text));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the InterestMonthCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void InterestMonthCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                ////Set the selected date on TextBox
                this.SetSeletedDate(e.Start.ToString(this.dateFormat));
                this.InterestDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the InterestMonthCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void InterestMonthCalender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.SetSeletedDate(this.InterestMonthCalender.SelectionStart.ToString(this.dateFormat));
                }

                if (e.KeyCode == Keys.Escape)
                {
                    this.InterestMonthCalender.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the CalculateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CalculateButton_Click(object sender, EventArgs e)
        {
            try
            {
                // refreshes the interest and penality details
                this.GetInterestAndPenaltyDetails();

                // re calculate the fields total vlaue
                this.CalculateFieldsTotalValue(sender);
                this.PayComboBox.Focus();
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
        /// Handles the Click event of the UseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UseButton_Click(object sender, EventArgs e)
        {
            try
            {
                // set the calculator property values
                this.SetCalculatorPropertyValues();

                // set the calling form dialog result status
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the CloseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                // set the calculator property values
                this.SetCalculatorPropertyValues();
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Events

        #region Private Methods

        /// <summary>
        /// Initializes the pay combo box.
        /// </summary>
        private void InitializePayComboBox()
        {
            this.PayComboBox.DisplayMember = this.interestCalcDataSet.GetPayDetails.PayLabelColumn.ColumnName;
            this.PayComboBox.ValueMember = this.interestCalcDataSet.GetPayDetails.PayTypeColumn.ColumnName;
            this.PayComboBox.DataSource = this.interestCalcDataSet.GetPayDetails;

            this.PayComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Gets the calculator row.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns>Data Row containing calculator fields</returns>
        private F1430InterestCalculatorData.CalculatorFieldsRow GetCalculatorRow(int rowIndex)
        {
            return (F1430InterestCalculatorData.CalculatorFieldsRow)this.interestCalcDataSet.CalculatorFields.Rows[rowIndex];
        }

        /// <summary>
        /// Gets the interest row.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns>Data Row containing interest amount and deliquency</returns>
        private F1430InterestCalculatorData.InterestDescriptionRow GetInterestRow(int rowIndex)
        {
            return (F1430InterestCalculatorData.InterestDescriptionRow)this.interestCalcDataSet.InterestDescription.Rows[rowIndex];
        }

        /// <summary>
        /// Sets the calculator details.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void SetCalculatorDetails(F1430InterestCalculatorData.CalculatorFieldsRow selectedRow)
        {
            if (!selectedRow.IsTaxAmountNull())
            {
                this.TaxTextBox.Text = selectedRow.TaxAmount.ToString();
                this.taxAmount = selectedRow.TaxAmount;
            }
            else
            {
                this.TaxTextBox.Text = string.Empty;
                this.taxAmount = 0;
            }

            if (!selectedRow.IsFeeNull())
            {
                this.FeesTextBox.Text = selectedRow.Fee.ToString();
                this.feeAmount = selectedRow.Fee;
            }
            else
            {
                this.FeesTextBox.Text = string.Empty;
                this.feeAmount = 0;
            }

            if (!selectedRow.IsInterestDateNull())
            {
                this.InterestDateTextBox.Text = selectedRow.InterestDate.ToString();
                this.interestDate = selectedRow.InterestDate;
            }
            else
            {
                this.InterestDateTextBox.Text = string.Empty;
            }
            if (!selectedRow.IsFirstHalfNull())
            {
                //decimal first;
                //decimal.TryParse(selectedRow.FirstHalf.ToString(), out first);
                this.FirstHalfTextBox.Text = selectedRow.FirstHalf.ToString();
                this.firstHalf = selectedRow.FirstHalf;
            }
            else
            {
                this.FirstHalfTextBox.Text = string.Empty;
            }
            if (!selectedRow.IsGraceDateNull())
            {
                this.GraceDateTextBox.Text = selectedRow.GraceDate.ToString();
                this.graceDate = selectedRow.GraceDate;
            }
            else
            {
                this.GraceDateTextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// Sets the interest and deliquency details.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void SetInterestDetails(F1430InterestCalculatorData.InterestDescriptionRow selectedRow)
        {
            if (!selectedRow.IsInterestAmountNull())
            {
                this.InterestTextBox.Text = selectedRow.InterestAmount.ToString();
                this.interestAmount = selectedRow.InterestAmount;
            }
            else
            {
                this.InterestTextBox.Text = string.Empty;
                this.interestAmount = 0;
            }

            if (!selectedRow.IsDelinquencyDescriptionNull())
            {
                this.Delinquencylabel.Text = selectedRow.DelinquencyDescription;
            }
            else
            {
                this.Delinquencylabel.Text = string.Empty;
            }

            if (!selectedRow.IsDelinquentPenaltyNull())
            {
                //Commented due to removing of penality text box
                //this.GraceDateTextBox.Text = selectedRow.DelinquentPenalty.ToString();
            }
            else
            {
                //this.GraceDateTextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// Sets the seleted date.
        /// </summary>
        /// <param name="dateSelected">The date selected.</param>
        private void SetSeletedDate(string dateSelected)
        {
            try
            {
                this.InterestDateTextBox.Text = dateSelected;
                this.InterestMonthCalender.Visible = false;
                this.InterestDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Clears the control values.
        /// </summary>
        private void ClearControlValues()
        {
            this.InterestDateTextBox.Text = string.Empty;
            this.TaxTextBox.Text = string.Empty;
            this.InterestTextBox.Text = string.Empty;
            this.FeesTextBox.Text = string.Empty;
            this.GraceDateTextBox.Text = string.Empty;
            this.TotalTextBox.Text = string.Empty;
            this.FirstHalfTextBox.Text = string.Empty;
            this.GraceDateTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Populates the interest calculator fields.
        /// </summary>
        private void PopulateInterestCalculatorFields()
        {
            // clear the form control values
            this.ClearControlValues();
            this.interestCalcDataSet.GetPayDetails.Clear();
            this.interestCalcDataSet.CalculatorFields.Clear();

            this.interestCalcDataSet.Merge(this.form1430Control.WorkItem.F1430_GetCalculatorDetails(this.statementId));

            // initialize the pay combo
            this.InitializePayComboBox();

            if (this.interestCalcDataSet.CalculatorFields.Rows.Count > 0)
            {
                this.SetCalculatorDetails(this.GetCalculatorRow(0));
            }

            // get the interest and penalty details
            this.GetInterestAndPenaltyDetails();

            // calculate the total fields value
            this.CalculateFieldsTotalValue(null);
        }

        /// <summary>
        /// Gets the interest and penalty details.
        /// </summary>
        private void GetInterestAndPenaltyDetails()
        {
            DateTime tempInterestDateValue;
            DateTime.TryParse(InterestDateTextBox.Text.Trim(), out tempInterestDateValue);
            this.interestCalcDataSet.InterestDescription.Clear();

            // get the interest and penalty detais
            this.interestCalcDataSet.Merge(this.form1430Control.WorkItem.F1430_GetInterestDetails(this.statementId, tempInterestDateValue, this.TaxTextBox.DecimalTextBoxValue));

            if (this.interestCalcDataSet.InterestDescription.Rows.Count > 0)
            {
                this.SetInterestDetails(this.GetInterestRow(0));
            }
        }

        /// <summary>
        /// Calculates the fields total value.
        /// </summary>
        private void CalculateFieldsTotalValue(object sender)
        {
            decimal tempCalculatedTotalValue;

            tempCalculatedTotalValue = this.TaxTextBox.DecimalTextBoxValue + this.InterestTextBox.DecimalTextBoxValue
                                        + this.FeesTextBox.DecimalTextBoxValue; //Commented to removal of penality field //+ this.GraceDateTextBox.DecimalTextBoxValue;
             
            if (sender != null)
            {       
                if (this.CheckMaxLimitValidation(sender, tempCalculatedTotalValue.ToString()))
                {
                    this.TotalTextBox.Text = decimal.Zero.ToString();
                }
                else
                {
                    this.TotalTextBox.Text = tempCalculatedTotalValue.ToString();
                }
            }
            else
            {
                this.TotalTextBox.Text = tempCalculatedTotalValue.ToString();
            }
        }

        /// <summary>
        /// Sets the calculator property values.
        /// </summary>
        private void SetCalculatorPropertyValues()
        {
            this.TaxAmount = this.TaxTextBox.DecimalTextBoxValue;
            this.FeeAmount = this.FeesTextBox.DecimalTextBoxValue;
            this.InterestAmount = this.InterestTextBox.DecimalTextBoxValue;
            //this.PenaltyAmount = this.GraceDateTextBox.DecimalTextBoxValue;
            this.FirstHalf = this.FirstHalfTextBox.DecimalTextBoxValue;            
            DateTime.TryParse(this.GraceDateTextBox.Text.Trim(), out this.graceDate);
            DateTime.TryParse(InterestDateTextBox.Text.Trim(), out this.interestDate);
            this.InterestTotal = this.TotalTextBox.DecimalTextBoxValue;
        }

        /// <summary>
        /// Checks the max limit validation.
        /// </summary>
        /// <param name="sourceTextBox">The source text box.</param>
        /// <param name="validatedTextBox">The validated text box.</param>
        /// <returns>The validated status.</returns>
        private bool CheckMaxLimitValidation(object sender, string validatedTextBoxText)
        {
            Control sourceControl = (Control)sender;
            decimal moneyMaxValue = 922337203685477.5807M;
            decimal validatedTextBoxValue;

            if (decimal.TryParse(validatedTextBoxText, out validatedTextBoxValue))
            {
                if (validatedTextBoxValue > moneyMaxValue)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("MaxLimitValidationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (sourceControl.GetType().Equals(typeof(TerraScanTextBox)))
                    {
                        sourceControl.Text = decimal.Zero.ToString();
                    }

                    sourceControl.Focus();
                    return true;
                }
            }

            return false;
        }

        #endregion Private Methods
    }
}