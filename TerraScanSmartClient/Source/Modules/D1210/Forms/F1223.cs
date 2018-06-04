//--------------------------------------------------------------------------------------------
// <copyright file="F1223.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1223.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10 Oct 06       RANJANI              Created
// 2  Feb 07       RANJANI              1223- 11.1, 11.2, 11.3 issue fixed
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
    /// F1223 Form
    /// </summary>
    public partial class F1223 : Form
    {
        #region Private Variables

        /// <summary>
        /// f12221Control Variable
        /// </summary>
        private F1223Controller form1223Control;

        /// <summary>
        /// DataSet Contains Check Detail 
        /// </summary>
        private CheckDetailData checkDetail = new CheckDetailData();  

        /// <summary>
        /// contains cl id
        /// </summary>
        private int currentclid;

        #endregion   

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1223"/> class.
        /// </summary>
        /// <param name="clid">The clid.</param>
        public F1223(int clid)
        {
            this.InitializeComponent();
            this.currentclid = clid;
            this.CancelButton = this.CancelCLButton;
            this.SaveMenuToolStripMenuItem.Click += new EventHandler(this.SaveCLButton_Click);
        }

        #endregion  
      
        #region Properties

        /// <summary>
        /// Gets or sets the F1223 control.
        /// </summary>
        /// <value>The F1223 control.</value>
        [CreateNew]
        public F1223Controller F1223Control
        {
            get { return this.form1223Control as F1223Controller; }
            set { this.form1223Control = value; }
        }

        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F1223 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1223_Load(object sender, EventArgs e)
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
            this.checkDetail.Clear();
            this.checkDetail = this.F1223Control.WorkItem.F1226_GetCashLedger(this.currentclid);

            if (this.checkDetail.GetCheckDetail.Rows.Count > 0)
            {
                this.CLIDTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.KeyIDColumn].ToString();
                this.PayableToTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.PayableToColumn].ToString();
                this.CheckAmountTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.AmountColumn].ToString();
                this.CheckDateTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.EntryDateColumn].ToString();

                if (!string.IsNullOrEmpty(this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.VoidByColumn].ToString()) && !string.IsNullOrEmpty(this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.VoidDateColumn].ToString()))
                {
                    this.VoidCheckBox.Checked = true;
                    this.VoidTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.VoidDateColumn].ToString();
                }
                else
                {
                    this.VoidCheckBox.Checked = false;
                    this.VoidTextBox.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    this.VoidTextBox.Enabled = false;
                    this.VoidButton.Enabled = false;
                    this.SaveCLButton.Enabled = false;
                }
            }
            else
            {
                this.ClearCheckDetail();
            }           
        }

        #endregion

        #region Clear CheckDetail

        /// <summary>
        /// Method will Clear the CheckDetail Header
        /// </summary>       
        private void ClearCheckDetail()
        {
            ////Check Detail related fields
            this.CLIDTextBox.Text = String.Empty;
            this.PayableToTextBox.Text = String.Empty;
            this.CheckAmountTextBox.Text = String.Empty;
            this.CheckDateTextBox.Text = String.Empty;
            this.VoidCheckBox.Enabled = false;
            this.VoidTextBox.Enabled = false;
            this.VoidButton.Enabled = false;
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
                ////check if void checked
                if (this.VoidCheckBox.Checked)
                {
                    ////required field validation
                    if (String.IsNullOrEmpty(this.VoidTextBox.Text.Trim()))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.VoidTextBox.Focus();
                        return;
                    }

                    ////assign current userid
                    userid = TerraScanCommon.UserId;
                }

                this.Cursor = Cursors.WaitCursor;
                ////update void status to the db 
                this.form1223Control.WorkItem.F1226_UpdateCashLedgerStatus(this.currentclid, userid, this.VoidTextBox.DateTextBoxValue, (int)TerraScanCommon.CheckStatusOrder.Void, TerraScanCommon.UserId);
                this.Cursor = Cursors.Default;
                ////modified flag 
                this.DialogResult = DialogResult.Yes;
                this.Close();
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
            this.VoidMonthCalendar.FocusRemovedFrom = false;
            this.VoidButton.Focus();
            this.VoidTextBox.Text = dateSelected;
        }

        /// <summary>
        /// Shows the void date calender in particular location.
        /// </summary>
        private void ShowVoidMonthCalender()
        {
            this.VoidMonthCalendar.Visible = true;
            this.VoidMonthCalendar.ScrollChange = 1;
            //// Display the Calender control near the Calender Picture box.            
            this.VoidMonthCalendar.Focus();
            this.VoidMonthCalendar.FocusRemovedFrom = true;
            if (!string.IsNullOrEmpty(this.VoidTextBox.Text))
            {
                this.VoidMonthCalendar.SetDate(this.VoidTextBox.DateTextBoxValue);
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the CheckDateMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void VoidMonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            ////set date to textbox control
            this.SetSeletedDate(e.Start.ToString("MM/dd/yyyy"));
        }

        /// <summary>
        /// Handles the Validating event of the CheckDateMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void VoidMonthCalendar_Validating(object sender, CancelEventArgs e)
        {
            ////focus next available control
            e.Cancel = true;
            if (this.VoidMonthCalendar.FocusRemovedFrom)
            {
                this.VoidCheckBox.Focus();
            }

            e.Cancel = false;
        }

        /// <summary>
        /// Handles the Click event of the CheckDateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void VoidButton_Click(object sender, EventArgs e)
        {
            // Calls the method to show the calender control.
            this.ShowVoidMonthCalender();
        }

        /// <summary>
        /// Handles the KeyDown event of the CheckDateMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void VoidMonthCalendar_KeyDown(object sender, KeyEventArgs e)
        {
            ////select date on enter key press
            if (e.KeyCode == Keys.Enter)
            {
                this.SetSeletedDate(this.VoidMonthCalendar.SelectionStart.ToString("MM/dd/yyyy"));
            }
        }

        #endregion 

        #region Private Methods

        /// <summary>
        /// Handles the CheckedChanged event of the VoidCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void VoidCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ////checked enable void textbox
            if (this.VoidCheckBox.Checked)
            {                
                this.VoidTextBox.Enabled = true;
                this.VoidButton.Enabled = true;
                this.SaveCLButton.Enabled = true;
            }
            else
            {               
                this.VoidTextBox.Enabled = false;
                this.VoidButton.Enabled = false;
                this.SaveCLButton.Enabled = false;
            }
        }

        #endregion
    }
}