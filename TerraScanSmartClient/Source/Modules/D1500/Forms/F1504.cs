////--------------------------------------------------------------------------------------------
//// <copyright file="F1504.cs" company="Congruent">
////    Copyright (c) Congruent Info-Tech.  All rights reserved.
//// </copyright>
//// <summary>
////   This file contains methods for the Copy Account. 
//// </summary>
////----------------------------------------------------------------------------------------------
//// Change History
////**********************************************************************************************
//// Date               Author            Description
//// ---------         ----------     ------------------------------------------------------------
//// 1/9/2009           R.Malliga         Created
////**********************************************************************************************

namespace D1500
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Utilities;

    /// <summary>
    /// F1504 Form
    /// </summary>
    public partial class F1504 : Form
    {
        #region Variables

        /// <summary>
        /// Used to Store AccountId
        /// </summary>
        private int accountId;

        /// <summary>
        /// controller F1504Controller
        /// </summary>
        private F1504Controller form1504Control;
            
        /// <summary>
        /// copyaccountDataset Variable 
        /// </summary>
        private F1504CopyAccountData copyaccountDataset = new F1504CopyAccountData();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F1504"/> class.
        /// </summary>
        public F1504()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F1504"/> class.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        public F1504(int accountId)
        {
            this.InitializeComponent();
            ////Account Values getting thru constructor
            this.accountId = accountId; 
        }
        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the form1504 control.
        /// </summary>
        /// <value>The form1504 control.</value>
        [CreateNew]
        public F1504Controller Form1504Control
        {
            get { return this.form1504Control as F1504Controller; }
            set { this.form1504Control = value; }
        }

        #endregion

        #region Button Events
        /// <summary>
        /// Handles the Click event of the CopyAccountCreateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CopyAccountCreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////Call the Save method
                this.SaveCopyAccountDetails();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the CopyAccounttCloseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CopyAccounttCloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////Close the current form
                this.Close(); 
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion

        #region Form Load
        /// <summary>
        /// Handles the Load event of the F1504 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F1504_Load(object sender, EventArgs e)
        {
            try
            {
                ////Populate SubFund Combo Value
                this.PopulateSubFundCombo();

                ////Populate Control Values
                this.PopulateControlValues(this.accountId);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion

        #region Text Changed Events
        /// <summary>
        /// Handles the TextChanged event of the TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ////Enable/Disable the CreateButton based on the textbox values
                if ((!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()) && (!string.IsNullOrEmpty(this.SubFundCombo.Text.Trim())))
                    && (!string.IsNullOrEmpty(this.FunctionTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.BarsTextBox.Text.Trim())
                    || !string.IsNullOrEmpty(this.LineTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.ObjectTextBox.Text.Trim())))
                {
                    this.CopyAccountCreateButton.Enabled = true;
                }
                else
                {
                    this.CopyAccountCreateButton.Enabled = false;
                }
             }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Populates the sub fund combo.
        /// </summary>
        private void PopulateSubFundCombo()
        {
            ////populating SubFund Combo
            this.copyaccountDataset = this.form1504Control.WorkItem.F1504_GetCopyAccountSubFund();
            this.SubFundCombo.DataSource = this.copyaccountDataset.F1504_ListSubFund;
            this.SubFundCombo.DisplayMember = this.copyaccountDataset.F1504_ListSubFund.SubFundColumn.ColumnName;
            this.SubFundCombo.ValueMember = this.copyaccountDataset.F1504_ListSubFund.SubFundColumn.ColumnName;
            this.SubFundCombo.SelectedIndex = 0;
        }
        
        /// <summary>
        /// Populates the control values.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        private void PopulateControlValues(int accountId)
        {
            ////Calling DB for the Copy Account Details
            this.copyaccountDataset = this.form1504Control.WorkItem.F1504_GetAccountDetail(accountId);
            this.Cursor = Cursors.WaitCursor;
            if (this.copyaccountDataset.F1504_GetAccountDetail.Rows.Count > 0)
            {
                ////Assigning Values to the control
                this.RollYearTextBox.Text = this.copyaccountDataset.F1504_GetAccountDetail.Rows[0][this.copyaccountDataset.F1504_GetAccountDetail.RollYearColumn].ToString();
                this.SubFundCombo.SelectedValue = this.copyaccountDataset.F1504_GetAccountDetail.Rows[0][this.copyaccountDataset.F1504_GetAccountDetail.SubFundColumn].ToString();
                this.DescriptionTextBox.Text = this.copyaccountDataset.F1504_GetAccountDetail.Rows[0][this.copyaccountDataset.F1504_GetAccountDetail.AcctDescColumn].ToString();

                ////Based on the function flag function textbox will get enable/Disable and populate the value
                if (!string.IsNullOrEmpty(this.copyaccountDataset.F1504_GetAccountDetail.Rows[0][this.copyaccountDataset.F1504_GetAccountDetail.FunctionFlagColumn].ToString())
                    && this.copyaccountDataset.F1504_GetAccountDetail.Rows[0][this.copyaccountDataset.F1504_GetAccountDetail.FunctionFlagColumn].ToString().Equals("True"))
                {
                    this.FunctionTextBox.Enabled = true; 
                    this.FunctionTextBox.Text = this.copyaccountDataset.F1504_GetAccountDetail.Rows[0][this.copyaccountDataset.F1504_GetAccountDetail.FunctionIDColumn].ToString();
                }
                else
                {
                    this.FunctionTextBox.Text = this.copyaccountDataset.F1504_GetAccountDetail.Rows[0][this.copyaccountDataset.F1504_GetAccountDetail.FunctionIDColumn].ToString();
                    this.FunctionTextBox.Enabled = false; 
                }

                ////Based on the Bars flag Bars textbox will get enable/Disable and populate the value
                if (!string.IsNullOrEmpty(this.copyaccountDataset.F1504_GetAccountDetail.Rows[0][this.copyaccountDataset.F1504_GetAccountDetail.BarsFlagColumn].ToString())
                    && this.copyaccountDataset.F1504_GetAccountDetail.Rows[0][this.copyaccountDataset.F1504_GetAccountDetail.BarsFlagColumn].ToString().Equals("True"))
                {
                    this.BarsTextBox.Enabled = true; 
                    this.BarsTextBox.Text = this.copyaccountDataset.F1504_GetAccountDetail.Rows[0][this.copyaccountDataset.F1504_GetAccountDetail.BarIDColumn].ToString();
                }
                else
                {
                    this.BarsTextBox.Text = this.copyaccountDataset.F1504_GetAccountDetail.Rows[0][this.copyaccountDataset.F1504_GetAccountDetail.BarIDColumn].ToString();
                    this.BarsTextBox.Enabled = false;
                }

                ////Based on the Object flag Object textbox will get enable/Disable and populate the value
                if (!string.IsNullOrEmpty(this.copyaccountDataset.F1504_GetAccountDetail.Rows[0][this.copyaccountDataset.F1504_GetAccountDetail.ObjectFlagColumn].ToString())
                    && this.copyaccountDataset.F1504_GetAccountDetail.Rows[0][this.copyaccountDataset.F1504_GetAccountDetail.ObjectFlagColumn].ToString().Equals("True"))
                {
                    this.ObjectTextBox.Enabled = true; 
                    this.ObjectTextBox.Text = this.copyaccountDataset.F1504_GetAccountDetail.Rows[0][this.copyaccountDataset.F1504_GetAccountDetail.ObjectIDColumn].ToString();
                }
                else
                {
                    this.ObjectTextBox.Text = this.copyaccountDataset.F1504_GetAccountDetail.Rows[0][this.copyaccountDataset.F1504_GetAccountDetail.ObjectIDColumn].ToString();
                    this.ObjectTextBox.Enabled = false;
                }

                ////Based on the Line flag Line textbox will get enable/Disable and populate the value
                if (!string.IsNullOrEmpty(this.copyaccountDataset.F1504_GetAccountDetail.Rows[0][this.copyaccountDataset.F1504_GetAccountDetail.LineFlagColumn].ToString())
                    && this.copyaccountDataset.F1504_GetAccountDetail.Rows[0][this.copyaccountDataset.F1504_GetAccountDetail.LineFlagColumn].ToString().Equals("True"))
                {
                    this.LineTextBox.Enabled = true;
                    this.LineTextBox.Text = this.copyaccountDataset.F1504_GetAccountDetail.Rows[0][this.copyaccountDataset.F1504_GetAccountDetail.LineIDColumn].ToString();
                }
                else
                {
                    this.LineTextBox.Text = this.copyaccountDataset.F1504_GetAccountDetail.Rows[0][this.copyaccountDataset.F1504_GetAccountDetail.LineIDColumn].ToString();
                    this.LineTextBox.Enabled = false;
                }
            }
            else
            {
                ////If record is not available then clearing all fields. 
                this.ClearControls(); 
            }

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Clears the controls.
        /// </summary>
        private void ClearControls()
        {
            ////Clearing all controls
            this.RollYearTextBox.Text = string.Empty;
            this.SubFundCombo.SelectedValue = 0;
            this.DescriptionTextBox.Text = string.Empty;
            this.FunctionTextBox.Text = string.Empty;
            this.BarsTextBox.Text = string.Empty;
            this.ObjectTextBox.Text = string.Empty;
            this.LineTextBox.Text = string.Empty;   
        }

        /// <summary>
        /// Saves the copy account details.
        /// </summary>
        private void SaveCopyAccountDetails()
        {
            int rollyear = 0;
         
            ////Checking Rollyear is empty and roll year should not be zero
            if (string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.RollYearTextBox.Focus(); 
                return; 
            }

            if (this.RollYearTextBox.Text.Equals("0"))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.RollYearTextBox.Focus(); 
                return; 
            }

            ////Checking Subfund combo should not be null
            if (string.IsNullOrEmpty(this.SubFundCombo.Text.ToString()))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.SubFundCombo.Focus(); 
                return; 
            }

            ////Try Parse rollyear value
            if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                int.TryParse(this.RollYearTextBox.Text.ToString(), out rollyear);
            }
            
            ////DB Call for save
            this.copyaccountDataset = this.form1504Control.WorkItem.F1504_SaveCopyAccountDetails(rollyear, this.SubFundCombo.Text.Trim(), this.DescriptionTextBox.Text.Trim(), this.FunctionTextBox.Text.Trim(), this.BarsTextBox.Text.Trim(), this.ObjectTextBox.Text.Trim(), this.LineTextBox.Text.Trim(), TerraScanCommon.UserId.ToString());

            ////Message will  be thrown based on the retrun value
            int keyValue = 0;
            if (this.copyaccountDataset.F1504_SaveCopyAccount.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.copyaccountDataset.F1504_SaveCopyAccount.Rows[0][0].ToString()))
                {
                    int.TryParse(this.copyaccountDataset.F1504_SaveCopyAccount.Rows[0][0].ToString(), out keyValue);
                }

                this.ShowMessageBasedOnResultValue(keyValue, this.copyaccountDataset.F1504_SaveCopyAccount.Rows[0][1].ToString());
            }
        }
        
        /// <summary>
        /// Shows the message based on result value.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="returnvalue">The returnvalue.</param>
        private void ShowMessageBasedOnResultValue(int keyId, string returnvalue)
        {
            ////If KeyId Is greater than Zero then do the following lines
            if (keyId > 0)
            {
                ////If Return value is equal to one then the following message should be throen
                if (returnvalue.Equals("True"))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("F1504SubFundDoesNotExist"), SharedFunctions.GetResourceString("F1504InvalidSubFund"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ////After Creating the record populating values to the controls.
                this.CopyAccountCreateButton.Enabled = false;
                this.PopulateControlValues(keyId);
            }
            else if (keyId.Equals(0))
            {
                ////If KeyId Is Zero then the account is already exist message should thrown
                MessageBox.Show(SharedFunctions.GetResourceString("F1504AccountAlreadyExist"), SharedFunctions.GetResourceString("F1504DuplicateAccount"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.RollYearTextBox.Focus();  
                return;
            }
            this.SubFundCombo.BackColor = Color.FromArgb(255, 255, 255);
            this.SubFundPanel.BackColor = Color.FromArgb(255, 255, 255);
        }

        /// <summary>
        /// Enables the disable controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableDisableControls(bool enable)
        {
            ////Enable/Diable Controls
            this.RollYearTextBox.Enabled = enable;
            this.DescriptionTextBox.Enabled = enable;
            this.SubFundCombo.Enabled = enable;
            this.FunctionTextBox.Enabled = enable;
            this.BarsTextBox.Enabled = enable;
            this.ObjectTextBox.Enabled = enable;
        }
        #endregion

        #region Combo Events
        /// <summary>
        /// Handles the Validating event of the SubFundCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void SubFundCombo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ////If SubFund values is not exist in the list then forcolor of the subfund combo box will change.
                if (this.SubFundCombo.SelectedValue == null)
                {
                    this.SubFundCombo.BackColor = Color.FromArgb(237, 205, 203);
                    this.SubFundPanel.BackColor  = Color.FromArgb(237, 205, 203);
                }
                else
                {
                    this.SubFundCombo.BackColor = Color.FromArgb(255, 255, 255);
                    this.SubFundPanel.BackColor = Color.FromArgb(255, 255, 255);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion
    }
}
