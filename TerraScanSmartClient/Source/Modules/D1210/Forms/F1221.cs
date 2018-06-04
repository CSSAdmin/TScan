//--------------------------------------------------------------------------------------------
// <copyright file="F1221.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1221.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10 Oct 06       RANJANI              Created
// 14 Nov          Guhan                Included Change Request
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
    using System.Web.Services.Protocols;

    /// <summary>
    /// F1221 Form
    /// </summary>
    public partial class F1221 : Form
    {
        #region Private Variables

        /// <summary>
        /// f1221Control Variable
        /// </summary>
        private F1221Controller form1221Control;   

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


        /// <summary>
        /// used to check whether checkno textBox  changed 
        /// </summary>
        private bool checkNoChange;

        #endregion   

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1221"/> class.
        /// </summary>
        /// <param name="clid">The clid.</param>
        public F1221(int clid)
        {
            this.InitializeComponent();
            this.SetMaxLength();
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
        public F1221Controller F1221Control
        {
            get { return this.form1221Control as F1221Controller; }
            set { this.form1221Control = value; }
        }  

        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F1221 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1221_Load(object sender, EventArgs e)
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
            this.checkDetail = this.F1221Control.WorkItem.F1226_GetCashLedger(this.currentclid);
            
            if (this.checkDetail.GetCheckDetail.Rows.Count > 0)
            {
                this.CLIDTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.KeyIDColumn].ToString();
                this.PayableToTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.PayableToColumn].ToString();
                this.CheckAmountTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.AmountColumn].ToString();
                this.NameTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.NameColumn].ToString();
                this.RefundInterestTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.RefundInterestColumn].ToString();
                this.CheckDateTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.EntryDateColumn].ToString();
                this.CheckNumberTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.CheckNumberColumn].ToString();
                this.AddressLine1TextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.Address1Column].ToString();
                this.AddressLine2TextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.Address2Column].ToString();
                this.CityTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.CityColumn].ToString();
                this.StateTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.StateColumn].ToString();
                this.ZipTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.ZipColumn].ToString();
                this.MemoTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.MemoColumn].ToString();               
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
            this.NameTextBox.Text = String.Empty;
            this.RefundInterestTextBox.Text = String.Empty;
            this.CheckDateTextBox.Text = String.Empty;
            this.CheckNumberTextBox.Text = String.Empty;
            this.AddressLine1TextBox.Text = String.Empty;
            this.AddressLine2TextBox.Text = String.Empty;
            this.CityTextBox.Text = String.Empty;
            this.StateTextBox.Text = String.Empty;
            this.ZipTextBox.Text = String.Empty;
            this.MemoTextBox.Text = String.Empty;
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

            int checknoExist;
            if (this.SaveCLButton.Enabled)
            {
                this.SaveCLButton.Focus();
                ////Check For Required Fields
                if (String.IsNullOrEmpty(this.PayableToTextBox.Text.Trim()))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.PayableToTextBox.Focus();
                    return;
                }

                if (String.IsNullOrEmpty(this.CheckDateTextBox.Text.Trim()))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.CheckDateTextBox.Focus();
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                ////update case ledger

                this.checkDetail.SaveCheckDetail.Rows.Clear();
                CheckDetailData.SaveCheckDetailRow checkDetailDataRow = this.checkDetail.SaveCheckDetail.NewSaveCheckDetailRow();

                checkDetailDataRow.PayableTo = this.PayableToTextBox.Text.Trim();
                checkDetailDataRow.Name = this.NameTextBox.Text.Trim();
                ////date validate in custom control
                checkDetailDataRow.EntryDate = this.CheckDateTextBox.Text.Trim();
                checkDetailDataRow.CheckNumber = this.CheckNumberTextBox.Text.Trim();
                checkDetailDataRow.Address1 = this.AddressLine1TextBox.Text.Trim();
                checkDetailDataRow.Address2 = this.AddressLine2TextBox.Text.Trim();
                checkDetailDataRow.City = this.CityTextBox.Text.Trim();
                checkDetailDataRow.State = this.StateTextBox.Text.Trim();
                checkDetailDataRow.Zip = this.ZipTextBox.Text.Trim();
                checkDetailDataRow.Memo = this.MemoTextBox.Text.Trim();
                checkDetailDataRow.UserID = TerraScanCommon.UserId;

                this.checkDetail.SaveCheckDetail.Rows.Add(checkDetailDataRow);

                DataTable tempCheckDetailDataTable = this.checkDetail.SaveCheckDetail.Copy();
                try
                {
                    checknoExist = this.form1221Control.WorkItem.F1221_UpdateCashLedger(this.currentclid, 0, Utility.GetXmlString(tempCheckDetailDataTable), TerraScanCommon.UserId);
                    if (this.checkNoChange)
                    {
                        if (checknoExist == 1)
                        {
                            //SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave
                            if (MessageBox.Show(SharedFunctions.GetResourceString("F1221CheckExist"),ConfigurationWrapper.ApplicationDuplicateCheck, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                checknoExist = this.form1221Control.WorkItem.F1221_UpdateCashLedger(this.currentclid, 1, Utility.GetXmlString(tempCheckDetailDataTable), TerraScanCommon.UserId);
                                this.Cursor = Cursors.Default;
                                ////modified flag 
                                this.DialogResult = DialogResult.Yes;
                                this.Close();
                                this.checkNoChange = false;
                            }
                            
                        }
                        else
                        {
                            this.Cursor = Cursors.Default;
                            ////modified flag 
                            this.DialogResult = DialogResult.Yes;
                            this.Close();
                        }
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
        }

        #endregion        

        #region Private Methods

        /// <summary>
        /// Sets the max length for the editable textboxes.
        /// </summary>
        private void SetMaxLength()
        {            
            this.PayableToTextBox.MaxLength = this.checkDetail.GetCheckDetail.PayableToColumn.MaxLength;
            this.NameTextBox.MaxLength = this.checkDetail.GetCheckDetail.NameColumn.MaxLength;
            this.CheckDateTextBox.MaxLength = this.checkDetail.GetCheckDetail.EntryDateColumn.MaxLength;
            this.CheckNumberTextBox.MaxLength = this.checkDetail.GetCheckDetail.CheckNumberColumn.MaxLength;
            this.AddressLine1TextBox.MaxLength = this.checkDetail.GetCheckDetail.Address1Column.MaxLength;
            this.AddressLine2TextBox.MaxLength = this.checkDetail.GetCheckDetail.Address2Column.MaxLength;
            this.CityTextBox.MaxLength = this.checkDetail.GetCheckDetail.CityColumn.MaxLength;
            this.StateTextBox.MaxLength = this.checkDetail.GetCheckDetail.StateColumn.MaxLength;
            this.ZipTextBox.MaxLength = this.checkDetail.GetCheckDetail.ZipColumn.MaxLength;
            this.MemoTextBox.MaxLength = this.checkDetail.GetCheckDetail.MemoColumn.MaxLength;            
        }

        /// <summary>
        /// Handles the TextChanged event of the editable control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EditTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.pageLoadStatus && !this.SaveCLButton.Enabled)
            {
                this.SaveCLButton.Enabled = true;
                //// Assign checkno Change to true 
                this.checkNoChange = true;
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
            this.CheckDateMonthCalendar.FocusRemovedFrom = false;
            this.CheckDateButton.Focus();
            this.CheckDateTextBox.Text = dateSelected;            
        }
     
        /// <summary>
        /// Shows the CheckDate calender in particular location.
        /// </summary>
        private void ShowCheckDateCalender()
        {
            this.CheckDateMonthCalendar.Visible = true;
            this.CheckDateMonthCalendar.ScrollChange = 1;
            //// Display the Calender control near the Calender Picture box.           
            this.CheckDateMonthCalendar.Focus();
            this.CheckDateMonthCalendar.FocusRemovedFrom = true;
            if (!string.IsNullOrEmpty(this.CheckDateTextBox.Text))
            {
                this.CheckDateMonthCalendar.SetDate(Convert.ToDateTime(this.CheckDateTextBox.Text));
            }
        }
      
        /// <summary>
        /// Handles the DateSelected event of the CheckDateMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void CheckDateMonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            ////set date to textbox control
            this.SetSeletedDate(e.Start.ToString("MM/dd/yyyy"));
        }

        /// <summary>
        /// Handles the Validating event of the CheckDateMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void CheckDateMonthCalendar_Validating(object sender, CancelEventArgs e)
        {
            ////focus next available control
            e.Cancel = true;
            if (this.CheckDateMonthCalendar.FocusRemovedFrom)
            {
                this.CheckNumberTextBox.Focus();
            }

            e.Cancel = false;
        }

        /// <summary>
        /// Handles the Click event of the CheckDateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CheckDateButton_Click(object sender, EventArgs e)
        {
            // Calls the method to show the calender control.
            this.ShowCheckDateCalender();
        }

        /// <summary>
        /// Handles the KeyDown event of the CheckDateMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void CheckDateMonthCalendar_KeyDown(object sender, KeyEventArgs e)
        {
            ////select date on enter key press
            if (e.KeyCode == Keys.Enter)
            {
                this.SetSeletedDate(this.CheckDateMonthCalendar.SelectionStart.ToString("MM/dd/yyyy"));
            }
        }

        #endregion        
    }
}