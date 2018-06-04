//--------------------------------------------------------------------------------------------
// <copyright file="F1222.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1222.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10 Oct 06        RANJANI              Created
// 02 Feb 07        RANJANI              1222- 10.1 issue fixed
//*********************************************************************************/
namespace D1210
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.SmartParts;
    using TerraScan.Utilities;
    using TerraScan.Common;
    using System.Configuration;

    /// <summary>
    /// F1222 Form
    /// </summary>
    public partial class F1222 : Form
    {
        #region Private Variables

        /// <summary>
        /// f12221Control Variable
        /// </summary>
        private F1222Controller form1222Control;

        /// <summary>
        /// DataSet Contains Check Detail 
        /// </summary>
        private CheckDetailData checkDetail = new CheckDetailData();

        /// <summary>
        /// pageLoadStatus variable is used to find the postdate is changed. 
        /// </summary>   
        private bool pageLoadStatus;

        /// <summary>
        /// contains cl id
        /// </summary>
        private int currentclid;

        #endregion   

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1222"/> class.
        /// </summary>
        /// <param name="clid">The clid.</param>
        public F1222(int clid)
        {
            this.InitializeComponent();
            this.currentclid = clid;
            this.CancelButton = this.CancelCLButton;
            this.SaveMenuToolStripMenuItem.Click += new EventHandler(this.SaveCLButton_Click);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F1221 control.
        /// </summary>
        /// <value>The F1221 control.</value>
        [CreateNew]
        public F1222Controller F1222Control
        {
            get { return this.form1222Control as F1222Controller; }
            set { this.form1222Control = value; }
        }

        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F1222 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1222_Load(object sender, EventArgs e)
        {
            if (this.currentclid > 0)
            {
                this.GetCheckDetail();
            }
        }

        #endregion

        #region Get Cash Ledger

        /// <summary>
        /// Gets the Check detail and fill Subfund item grid
        /// </summary>
        private void GetCheckDetail()
        {
            ////set pageLoadStatus - suppress textchanged event
            this.pageLoadStatus = true;
            this.checkDetail.Clear();
            this.checkDetail = this.F1222Control.WorkItem.F1226_GetCashLedger(this.currentclid);

            if (this.checkDetail.GetCheckDetail.Rows.Count > 0)
            {
                this.CLIDTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.KeyIDColumn].ToString();
                this.PayableToTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.PayableToColumn].ToString();
                this.CheckAmountTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.AmountColumn].ToString();                
                this.CheckDateTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.EntryDateColumn].ToString();
                ////load mailedby combo
                UserManagementData userManagementData = new UserManagementData();
                userManagementData = this.form1222Control.WorkItem.F9002_GetUserDetails(TerraScanCommon.ApplicationId);
                this.MailedByComboBox.DataSource = userManagementData.ListUserDetail;                
                this.MailedByComboBox.DisplayMember = userManagementData.ListUserDetail.DisplayNameColumn.ColumnName;
                this.MailedByComboBox.ValueMember = userManagementData.ListUserDetail.UserIDColumn.ColumnName;

                if (!string.IsNullOrEmpty(this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.MailedByColumn].ToString()) && !string.IsNullOrEmpty(this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.MailedDateColumn].ToString()))
                {
                    this.MailedCheckBox.Checked = true;
                    this.MailedByComboBox.SelectedValue = Convert.ToInt32(this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.MailedByIDColumn].ToString());
                    this.MailedOnTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.MailedDateColumn].ToString();                    
                }
                else
                {
                    this.MailedCheckBox.Checked = false;
                    this.MailedByComboBox.SelectedIndex = -1;
                    this.MailedOnTextBox.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    this.MailedByComboBox.Enabled = false;
                    this.MailedOnTextBox.Enabled = false;
                    this.MailedOnButton.Enabled = false;
                }                
            }
            else
            {
                this.ClearCheckDetail();
            }

            ////reset pageLoadStatus - trigger textchanged event
            this.pageLoadStatus = false;
        }

        #endregion

        #region Clear CheckDetail

        /// <summary>
        /// Method will Clear the ExciseTaxStatement Header
        /// </summary>       
        private void ClearCheckDetail()
        {
            ////Check Detail related fields
            this.CLIDTextBox.Text = String.Empty;
            this.PayableToTextBox.Text = String.Empty;
            this.CheckAmountTextBox.Text = String.Empty;            
            this.CheckDateTextBox.Text = String.Empty;
            this.MailedCheckBox.Enabled = false;
            this.MailedByComboBox.Enabled = false;
            this.MailedOnTextBox.Enabled = false;
            this.MailedOnButton.Enabled = false;
        }

        #endregion

        #region Save

        /// <summary>
        /// Handles the Click event of the CLSaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveCLButton_Click(object sender, EventArgs e)
        {
            if (this.SaveCLButton.Enabled)
            {
                this.SaveCLButton.Focus();
                int userid = -999;
                ////check if mailed checked
                if (this.MailedCheckBox.Checked)
                {
                    ////required field validation  
                    if (String.IsNullOrEmpty(this.MailedByComboBox.Text.Trim()))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.MailedByComboBox.Focus();
                        return;
                    }
                    else
                    {
                        ////check for validation
                        if (this.MailedByComboBox.SelectedIndex < 0)
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("MailedByValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.MailedByComboBox.Focus();
                            return;
                        }
                    }

                    if (String.IsNullOrEmpty(this.MailedOnTextBox.Text.Trim()))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.MailedOnTextBox.Focus();
                        return;
                    }

                    ////assign combo value to the userid
                    userid = Convert.ToInt32(this.MailedByComboBox.SelectedValue);
                }

                this.Cursor = Cursors.WaitCursor;
                ////update printed status to the db - cleared status validation checked
                this.form1222Control.WorkItem.F1226_UpdateCashLedgerStatus(this.currentclid, userid, this.MailedOnTextBox.DateTextBoxValue, (int)TerraScanCommon.CheckStatusOrder.Mailed, TerraScanCommon.UserId);
                this.Cursor = Cursors.Default;
                ////modified flag 
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

        #endregion

        #region Private Methods     

        /// <summary>
        /// Handles the TextUpdate event of the MailedByComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MailedValue_TextUpdate(object sender, EventArgs e)
        {
            if (!this.pageLoadStatus && !this.SaveCLButton.Enabled)
            {
                this.SaveCLButton.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the MailedCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MailedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.pageLoadStatus)
            {
                this.SaveCLButton.Enabled = true;
            }

            ////checked enable mailedbycombo and mailedon textbox
            if (this.MailedCheckBox.Checked)
            {
                this.MailedByComboBox.Enabled = true;
                this.MailedOnTextBox.Enabled = true;
                this.MailedOnButton.Enabled = true;
                this.SaveCLButton.Enabled = true;
            }
            else
            {
                this.MailedByComboBox.Enabled = false;
                this.MailedOnTextBox.Enabled = false;
                this.MailedOnButton.Enabled = false;
                this.SaveCLButton.Enabled = false;
            }
        }

        #endregion

        #region Date Related Calender Controls Events

        /// <summary>
        /// Sets the seleted date.
        /// </summary>
        /// <param name="dateSelected">The date selected.</param>
        private void SetSeletedDate(string dateSelected)
        {
            this.MailedOnMonthCalendar.FocusRemovedFrom = false;
            this.MailedOnButton.Focus();
            this.MailedOnTextBox.Text = dateSelected;
        }

        /// <summary>
        /// Shows the mailed date calender in particular location.
        /// </summary>
        private void ShowMailedOnCalender()
        {
            this.MailedOnMonthCalendar.Visible = true;
            this.MailedOnMonthCalendar.ScrollChange = 1;
            //// Display the Calender control near the Calender Picture box.            
            this.MailedOnMonthCalendar.Focus();
            this.MailedOnMonthCalendar.FocusRemovedFrom = true;
            if (!string.IsNullOrEmpty(this.MailedOnTextBox.Text))
            {
                this.MailedOnMonthCalendar.SetDate(this.MailedOnTextBox.DateTextBoxValue);
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the CheckDateMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void MailedOnMonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            ////set date to textbox control
            this.SetSeletedDate(e.Start.ToString("MM/dd/yyyy"));
        }

        /// <summary>
        /// Handles the Validating event of the CheckDateMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void MailedOnMonthCalendar_Validating(object sender, CancelEventArgs e)
        {
            ////focus next available control
            e.Cancel = true;
            if (this.MailedOnMonthCalendar.FocusRemovedFrom)
            {
                this.MailedCheckBox.Focus();
            }

            e.Cancel = false;
        }

        /// <summary>
        /// Handles the Click event of the CheckDateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MailedOnButton_Click(object sender, EventArgs e)
        {
            // Calls the method to show the calender control.
            this.ShowMailedOnCalender();
        }

        /// <summary>
        /// Handles the KeyDown event of the CheckDateMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void MailedOnMonthCalendar_KeyDown(object sender, KeyEventArgs e)
        {
            ////select date on enter key press
            if (e.KeyCode == Keys.Enter)
            {
                this.SetSeletedDate(this.MailedOnMonthCalendar.SelectionStart.ToString("MM/dd/yyyy"));
            }
        }

        #endregion     
    }
}