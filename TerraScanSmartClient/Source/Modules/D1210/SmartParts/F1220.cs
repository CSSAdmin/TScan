//--------------------------------------------------------------------------------------------
// <copyright file="F1220.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F1220 Account Register Functionality
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09-10-2006       Shiva              Created
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
    /// F1220 Account Register User Interface
    /// </summary>
    [SmartPart]
    public partial class F1220 : BaseSmartPart
    {
        #region Private Variables

        /// <summary>
        /// Variable Instance for F1210Controller
        /// </summary>
        private F1220Controller form1220Controll;

        /// <summary>
        /// account register DataSet
        /// </summary>
        private F1220AccountRegisterData accountRegisterDataSet = new F1220AccountRegisterData();

        /// <summary>
        /// Register Id
        /// </summary>
        private int registerId;

        /// <summary>
        /// beginning Date
        /// </summary>
        private DateTime beginningDate;

        /// <summary>
        /// Variable Holds the CLID
        /// </summary>
        private int clid;

        /// <summary>
        /// Created Boolean to Find emptyRecord
        /// </summary>        
        private bool emptyRecord;

        /// <summary>
        /// beginnig Balace
        /// </summary>
        private decimal beginningBalance;

        /// <summary>
        /// clTypeId
        /// </summary>
        private int cltypeId;

        /// <summary>
        /// postDateChanged variable is used to find the postdate is changed. 
        /// </summary>   
        private bool postDateChanged;

        /// <summary>
        /// pageLoadStatus variable is used to hold the State of Page Load
        /// </summary>   
        private bool pageLoadStatus;

        /// <summary>
        /// isshift
        /// </summary>
        private bool isshift;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1220"/> class.
        /// </summary>
        public F1220()
        {
            InitializeComponent();
            this.registerId = -1;
            this.clid = -1;
            this.cltypeId = -1;
            this.AccountRegisterHeaderPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AccountRegisterHeaderPictureBox.Height, this.AccountRegisterHeaderPictureBox.Width, "", 0, 51, 0);
            this.PaymentGridPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PaymentGridPictureBox.Height, this.PaymentGridPictureBox.Width, "Account Transactions", 28, 81, 128);
        }

        #endregion

        #region Published Events

        /// <summary>
        /// EventPublication for FormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form1210 controll.
        /// </summary>
        /// <value>The form1210 controll.</value>
        [CreateNew]
        public F1220Controller Form1220Controll
        {
            get { return this.form1220Controll as F1220Controller; }
            set { this.form1220Controll = value; }
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
        /// Gets or sets the beginning date.
        /// </summary>
        /// <value>The beginning date.</value>
        public DateTime BeginningDate
        {
            get { return this.beginningDate; }
            set { this.beginningDate = value; }
        }

        /// <summary>
        /// Gets or sets the clid.
        /// </summary>
        /// <value>The clid.</value>
        public int Clid
        {
            get
            {
                return this.clid;
            }

            set
            {
                this.clid = value;
            }
        }

        /// <summary>
        /// Gets or sets the beginning balance.
        /// </summary>
        /// <value>The beginning balance.</value>
        public decimal BeginningBalance
        {
            get { return this.beginningBalance; }
            set { this.beginningBalance = value; }
        }

        /// <summary>
        /// Gets or sets the cltype id.
        /// </summary>
        /// <value>The cltype id.</value>
        public int CltypeId
        {
            get { return this.cltypeId; }
            set { this.cltypeId = value; }
        }

        #endregion

        #region SubScribed Events

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
            if (optionalParams.Length > 0 && optionalParams[optionalParams.Length - 1].Equals(this.Tag))
            {
                try
                {
                    this.RegisterId = Convert.ToInt32(optionalParams[0]);
                    this.InitAccountNameCombo();
                    DateTime beginDate = new DateTime(0001, 01, 01);
                    this.PopulateAccountRegisterDetails(this.RegisterId, beginDate);
                    if (!this.emptyRecord)
                    {
                        this.TransactionsGridView.Rows[0].Selected = true;
                        this.TransactionsGridView.Focus();
                    }
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
        /// Handles the Load event of the F1220 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1220_Load(object sender, EventArgs e)
        {
            try
            {
                this.pageLoadStatus = true;
                DateTime beginDate = new DateTime(0001, 01, 01);
                this.LoadWorkSpaces();
                this.CustomizeTransactionsGrid();
                this.InitAccountNameCombo();
                this.PopulateAccountRegisterDetails(this.RegisterId, beginDate);
                if (!this.emptyRecord)
                {
                    this.TransactionsGridView.Rows[0].Selected = true;
                    this.TransactionsGridView.Focus();
                }

                this.AccountNameComboBox.Select();
                this.AccountNameComboBox.Focus();
                this.pageLoadStatus = false;
            }
            catch (SoapException SoapEx)
            {
                ExceptionManager.ManageException(SoapEx, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                    DateTime beginDate = new DateTime(0001, 01, 01);
                    this.RegisterId = Convert.ToInt32(this.AccountNameComboBox.SelectedValue);
                    this.PopulateAccountRegisterDetails(this.RegisterId, beginDate);
                }
                else
                {
                    this.RegisterId = -1;
                }
            }
            catch (SoapException soapEx)
            {
                ExceptionManager.ManageException(soapEx, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception Ex)
            {
                ExceptionManager.ManageException(Ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the TransactionsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void TransactionsGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;
            try
            {
                //// Only paint if desired, formattable column

                if (e.ColumnIndex == this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.AmountColumn.ColumnName.ToString()].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.TransactionsGridView.Rows[e.RowIndex].Cells[this.accountRegisterDataSet.ListAccountRegister.AmountColumn.ColumnName.ToString()].Value.ToString().Trim()))
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

                if (e.ColumnIndex == this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.BalanceColumn.ColumnName.ToString()].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.TransactionsGridView.Rows[e.RowIndex].Cells[this.accountRegisterDataSet.ListAccountRegister.BalanceColumn.ColumnName.ToString()].Value.ToString().Trim()))
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

                if (e.ColumnIndex == this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.EntryDateColumn.ColumnName].Index)
                {
                        if (this.TransactionsGridView.Rows[e.RowIndex].Selected || this.TransactionsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                        {
                            (TransactionsGridView.Rows[e.RowIndex].Cells[this.accountRegisterDataSet.ListAccountRegister.EntryDateColumn.ColumnName.ToString()] as DataGridViewLinkCell).LinkColor = Color.White;
                            (TransactionsGridView.Rows[e.RowIndex].Cells[this.accountRegisterDataSet.ListAccountRegister.EntryDateColumn.ColumnName.ToString()] as DataGridViewLinkCell).ActiveLinkColor = Color.White;
                            (TransactionsGridView.Rows[e.RowIndex].Cells[this.accountRegisterDataSet.ListAccountRegister.EntryDateColumn.ColumnName.ToString()] as DataGridViewLinkCell).VisitedLinkColor = Color.White;
                        }
                        else
                        {
                            (TransactionsGridView.Rows[e.RowIndex].Cells[this.accountRegisterDataSet.ListAccountRegister.EntryDateColumn.ColumnName.ToString()] as DataGridViewLinkCell).LinkColor = Color.Blue;
                            (TransactionsGridView.Rows[e.RowIndex].Cells[this.accountRegisterDataSet.ListAccountRegister.EntryDateColumn.ColumnName.ToString()] as DataGridViewLinkCell).ActiveLinkColor = Color.Red;
                            (TransactionsGridView.Rows[e.RowIndex].Cells[this.accountRegisterDataSet.ListAccountRegister.EntryDateColumn.ColumnName.ToString()] as DataGridViewLinkCell).VisitedLinkColor = Color.Blue;
                        }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the AccountRegisterAuditLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void AccountRegisterAuditLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (this.Clid > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(90101);
                    formInfo.optionalParameters = new object[2];
                    formInfo.optionalParameters[0] = this.Tag;
                    formInfo.optionalParameters[1] = this.Clid;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }

                ////Hashtable reportFileIdHashTable = new Hashtable();
                ////this.Cursor = Cursors.WaitCursor;
                ////if (this.Clid != -1)
                ////{
                ////    reportFileIdHashTable.Clear();
                ////    reportFileIdHashTable.Add("CLID", this.Clid);
                ////    ////changed the parameter type from string to int
                ////    TerraScanCommon.ShowReport(122090, TerraScan.Common.Reports.Report.ReportType.Preview, reportFileIdHashTable);
                ////}
                ////else
                ////{
                ////    MessageBox.Show(SharedFunctions.GetResourceString("+"), "Terrascan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////}
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
        /// Handles the CellClick event of the TransactionsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void TransactionsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.GridClick(e.RowIndex);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the TransactionsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void TransactionsGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.GridClick(e.RowIndex);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the TransactionsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void TransactionsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.GridClick(e.RowIndex);
                if (e.RowIndex >= 0 && e.RowIndex < this.TransactionsGridView.OriginalRowCount && e.ColumnIndex == 1)
                {
                    if (this.CltypeId != -1 && this.Clid != -1)
                    {
                        FormInfo formInfo;
                        if (this.CltypeId == 1 || this.CltypeId == 2)
                        {
                            formInfo = TerraScanCommon.GetFormInfo(1226);
                            formInfo.optionalParameters = new object[1];
                            formInfo.optionalParameters[0] = this.Clid;
                            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                        }
                        else if (this.CltypeId == 3)
                        {
                            formInfo = TerraScanCommon.GetFormInfo(1229);
                            formInfo.optionalParameters = new object[1];
                            formInfo.optionalParameters[0] = this.Clid;
                            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                        }
                        else if (this.CltypeId == 4)
                        {
                            formInfo = TerraScanCommon.GetFormInfo(1228);
                            formInfo.optionalParameters = new object[1];
                            formInfo.optionalParameters[0] = this.Clid;
                            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                        }
                        else
                        {
                            formInfo = TerraScanCommon.GetFormInfo(1227);
                            formInfo.optionalParameters = new object[1];
                            formInfo.optionalParameters[0] = this.Clid;
                            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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
        /// Handles the Click event of the ReportButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReportButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Hashtable reportOptionalParameter = new Hashtable();
                ////// calling  Common Function For Report                    
                reportOptionalParameter.Add("UserID", TerraScanCommon.UserId);
                ////changed the parameter type from string to int
                TerraScanCommon.ShowReport(122001, Report.ReportType.Preview, reportOptionalParameter);
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

        #region Date Related Methods

        /// <summary>
        /// Handles the DateSelected event of the AccountRegisterMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void AccountRegisterMonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.BeginningDate = e.Start;
                this.SetSeletedDate(e.Start.ToString("MM/dd/yyyy"));
                InterestDatePanel.BackColor = Color.Yellow;
                BeginningDateTextBox.BackColor = Color.Yellow;
                //this.ParentForm.ActiveControl = BeginningDateTextBox;
                //this.ParentForm.ActiveControl.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the AccountRegisterMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void AccountRegisterMonthCalendar_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.SetSeletedDate(this.AccountRegisterMonthCalendar.SelectionStart.ToString("MM/dd/yyyy"));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            isshift = e.Shift;
        }

        /// <summary>
        /// Sets the seleted date.
        /// </summary>
        /// <param name="dateSelected">The date selected.</param>
        private void SetSeletedDate(string dateSelected)
        {
            this.AccountRegisterMonthCalendar.FocusRemovedFrom = false;
            this.InterestDateButton.Focus();
            this.BeginningDateTextBox.Text = dateSelected;
            this.BeginningDateTextBox_Leave(this.BeginningDateTextBox, EventArgs.Empty);
        }

        /// <summary>
        /// Handles the Click event of the InterestDateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InterestDateButton_Click(object sender, EventArgs e)
        {
            // Calls the method to show the calender control.
            try
            {
                this.ShowAccountRegisterMonthCalendar();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the BeginningDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void BeginningDateTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.postDateChanged)
                {
                    this.pageLoadStatus = true;
                    if (!DateTime.TryParse(this.BeginningDateTextBox.Text.Trim(), out this.beginningDate))
                    {
                        this.beginningDate = DateTime.Now;
                        this.BeginningDateTextBox.Text = this.beginningDate.ToString("MM/dd/yyyy");
                    }

                    if (this.beginningDate > DateTime.Now)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("PostDateValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.beginningDate = DateTime.Now;
                        this.BeginningDateTextBox.Text = this.beginningDate.ToString("MM/dd/yyyy");
                    }

                    this.pageLoadStatus = false;
                    this.postDateChanged = false;
                    this.PopulateAccountRegisterDetails(this.RegisterId, this.BeginningDate);
                }
                InterestDatePanel.BackColor = Color.White;
                BeginningDateTextBox.BackColor = Color.White;

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the BeginningDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void BeginningDateTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.pageLoadStatus)
                {
                    this.postDateChanged = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            if (this.form1220Controll.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1220Controll.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1220Controll.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            this.SetFormHeader(this, new DataEventArgs<string[]>(new string[] { SharedFunctions.GetResourceString("1220AccountRegister"), string.Empty }));
        }

        /// <summary>
        /// Shows the AccountRegister date calender.
        /// </summary>
        private void ShowAccountRegisterMonthCalendar()
        {
            this.AccountRegisterMonthCalendar.Visible = true;
            this.AccountRegisterMonthCalendar.ScrollChange = 1;

            // Display the Calender control near the Calender Picture box.
            this.AccountRegisterMonthCalendar.Left = this.AccountRegisterHeaderPanel.Left + this.InterestDatePanel.Left - this.InterestDateButton.Width;
            this.AccountRegisterMonthCalendar.Top = this.AccountRegisterHeaderPanel.Top + this.InterestDatePanel.Top + this.InterestDateButton.Height + this.InterestDateButton.Top;
            this.AccountRegisterMonthCalendar.Focus();
            this.AccountRegisterMonthCalendar.FocusRemovedFrom = true;
            if (!string.IsNullOrEmpty(this.BeginningDateTextBox.Text))
            {
                this.AccountRegisterMonthCalendar.SetDate(Convert.ToDateTime(this.BeginningDateTextBox.Text));
            }
        }

        /// <summary>
        /// Fills the account name combo.
        /// </summary>
        private void InitAccountNameCombo()
        {
            F1220AccountRegisterData.ListAccountNamesDataTable listAccountNames = new F1220AccountRegisterData.ListAccountNamesDataTable();
            listAccountNames = this.form1220Controll.WorkItem.F1220_AccountNames();
            this.AccountNameComboBox.ValueMember = this.accountRegisterDataSet.ListAccountNames.RegisterIDColumn.ColumnName;
            this.AccountNameComboBox.DisplayMember = this.accountRegisterDataSet.ListAccountNames.AccountNameColumn.ColumnName;
            this.AccountNameComboBox.DataSource = listAccountNames;

            if (this.RegisterId == -1)
            {
                DataRow[] dataRow;
                bool defaultValue = true;
                string findExp = this.accountRegisterDataSet.ListAccountNames.IsDefaultColumn.ColumnName.ToString() + " =" + defaultValue.ToString();
                dataRow = listAccountNames.Select(findExp);

                if (dataRow.Length > 0)
                {
                    int rowIndex = listAccountNames.Rows.IndexOf(dataRow[0]);
                    this.RegisterId = Convert.ToInt32(dataRow[0][this.accountRegisterDataSet.ListAccountNames.RegisterIDColumn.ColumnName.ToString()]);
                    this.AccountNameComboBox.SelectedIndex = rowIndex;
                    this.LockControls(true);
                }
                else
                {
                    this.AccountNameComboBox.SelectedValue = this.RegisterId;
                }
            }
            else
            {
                this.LockControls(true);
                this.AccountNameComboBox.SelectedValue = this.RegisterId;
                DataRow[] dataRow;
                int defaultValue = this.RegisterId;
                string findExp = this.accountRegisterDataSet.ListAccountNames.RegisterIDColumn.ColumnName.ToString() + " =" + defaultValue.ToString();
                dataRow = listAccountNames.Select(findExp);

                if (dataRow.Length <= 0)
                {
                    this.RegisterId = -1;
                }
            }
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="lockValue">if set to <c>true</c> [lock value].</param>
        private void LockControls(bool lockValue)
        {
            this.AccountNameComboBox.Enabled = lockValue;
            this.LastReconciledTextBox.Enabled = lockValue;
            this.BeginningBalanceTextBox.Enabled = lockValue;
            this.BeginningDateTextBox.Enabled = lockValue;
            this.InterestDateButton.Enabled = lockValue;
            this.TransactionsGridView.Enabled = lockValue;
        }

        /// <summary>
        /// Customizes the transactions grid.
        /// </summary>
        private void CustomizeTransactionsGrid()
        {
            this.TransactionsGridView.AllowUserToResizeColumns = false;
            this.TransactionsGridView.AutoGenerateColumns = false;
            this.TransactionsGridView.AllowUserToResizeRows = false;
            this.TransactionsGridView.StandardTab = true;
            this.TransactionsGridView.PrimaryKeyColumnName = this.accountRegisterDataSet.ListAccountRegister.CLIDColumn.ColumnName;

            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.EntryDateColumn.ColumnName.ToString()].Resizable = DataGridViewTriState.False;

            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.CLIDColumn.ColumnName.ToString()].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.EntryDateColumn.ColumnName.ToString()].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.CLTypeIDColumn.ColumnName.ToString()].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.CLTypeColumn.ColumnName.ToString()].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.CheckNumberColumn.ColumnName.ToString()].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.PayableToColumn.ColumnName.ToString()].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.ClearedDateColumn.ColumnName.ToString()].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.AmountColumn.ColumnName.ToString()].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.BalanceColumn.ColumnName.ToString()].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.CLIDColumn.ColumnName.ToString()].DataPropertyName = this.accountRegisterDataSet.ListAccountRegister.CLIDColumn.ColumnName.ToString();
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.EntryDateColumn.ColumnName.ToString()].DataPropertyName = this.accountRegisterDataSet.ListAccountRegister.EntryDateColumn.ColumnName.ToString();
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.CLTypeIDColumn.ColumnName.ToString()].DataPropertyName = this.accountRegisterDataSet.ListAccountRegister.CLTypeIDColumn.ColumnName.ToString();
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.CLTypeColumn.ColumnName.ToString()].DataPropertyName = this.accountRegisterDataSet.ListAccountRegister.CLTypeColumn.ColumnName.ToString();
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.CheckNumberColumn.ColumnName.ToString()].DataPropertyName = this.accountRegisterDataSet.ListAccountRegister.CheckNumberColumn.ColumnName.ToString();
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.PayableToColumn.ColumnName.ToString()].DataPropertyName = this.accountRegisterDataSet.ListAccountRegister.PayableToColumn.ColumnName.ToString();
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.ClearedDateColumn.ColumnName.ToString()].DataPropertyName = this.accountRegisterDataSet.ListAccountRegister.ClearedDateColumn.ColumnName.ToString();
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.AmountColumn.ColumnName.ToString()].DataPropertyName = this.accountRegisterDataSet.ListAccountRegister.AmountColumn.ColumnName.ToString();
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.BalanceColumn.ColumnName.ToString()].DataPropertyName = this.accountRegisterDataSet.ListAccountRegister.BalanceColumn.ColumnName.ToString();

            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.EntryDateColumn.ColumnName.ToString()].Width = 85;
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.CLTypeColumn.ColumnName.ToString()].Width = 90;
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.CheckNumberColumn.ColumnName.ToString()].Width = 90;
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.PayableToColumn.ColumnName.ToString()].Width = 170;
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.ClearedDateColumn.ColumnName.ToString()].Width = 85;
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.AmountColumn.ColumnName.ToString()].Width = 113;
            this.TransactionsGridView.Columns[this.accountRegisterDataSet.ListAccountRegister.BalanceColumn.ColumnName.ToString()].Width = 115;
        }

        /// <summary>
        /// Populates the account register details.
        /// </summary>
        /// <param name="regId">The reg id.</param>
        /// <param name="beginDate">The begin date.</param>
        private void PopulateAccountRegisterDetails(int regId, DateTime beginDate)
        {
            int accountRegisterRowCount = 0;

            if (this.RegisterId != -1)
            {
                this.accountRegisterDataSet.ListAccountRegister.Rows.Clear();

                this.accountRegisterDataSet = this.form1220Controll.WorkItem.F1220_GetAccountRegisterDetails(regId, beginDate);

                this.LastReconciledTextBox.Text = this.accountRegisterDataSet.ReconciledDetails.Rows[0][this.accountRegisterDataSet.ReconciledDetails.LastReconciledDateColumn.ToString()].ToString();
                this.beginningBalance = Convert.ToDecimal(this.accountRegisterDataSet.ReconciledDetails.Rows[0][this.accountRegisterDataSet.ReconciledDetails.BeginningBalanceColumn.ToString()]);
                this.BeginningBalanceTextBox.Text = this.accountRegisterDataSet.ReconciledDetails.Rows[0][this.accountRegisterDataSet.ReconciledDetails.BeginningBalanceColumn.ToString()].ToString();
                this.BeginningDateTextBox.Text = this.accountRegisterDataSet.ReconciledDetails.Rows[0][this.accountRegisterDataSet.ReconciledDetails.BeginningDateColumn.ToString()].ToString();
                this.DisplayEndigBalance();
                accountRegisterRowCount = this.accountRegisterDataSet.ListAccountRegister.Rows.Count;
                ////this.TransactionsGridView.DataSource = this.accountRegisterDataSet.ListAccountRegister;
            }
            else
            {
                this.ClearFields();
                this.accountRegisterDataSet = new F1220AccountRegisterData();
                ////this.TransactionsGridView.DataSource = this.accountRegisterDataSet.ListAccountRegister;
            }

            if (accountRegisterRowCount > 0)
            {
                this.emptyRecord = false;
                this.TransactionsGridView.DataSource = this.accountRegisterDataSet.ListAccountRegister;
                this.TransactionsGridView.Enabled = true;
                this.TransactionsGridView.Rows[0].Selected = true;
                this.AccountRegisterAuditLink.Text = SharedFunctions.GetResourceString("1213DepositHistoryAuditLink") + " " + this.TransactionsGridView.Rows[0].Cells[this.accountRegisterDataSet.ListAccountRegister.CLIDColumn.ToString()].Value.ToString();
                this.AccountRegisterAuditLink.Enabled = true;
            }
            else
            {
                this.emptyRecord = true;
                this.TransactionsGridView.DataSource = this.accountRegisterDataSet.ListAccountRegister;
                this.TransactionsGridView.CurrentCell = null;
                this.TransactionsGridView.Rows[0].Selected = false;
                this.TransactionsGridView.Enabled = false;
                this.AccountRegisterAuditLink.Text = SharedFunctions.GetResourceString("1213DepositHistoryAuditLink");
                this.AccountRegisterAuditLink.Enabled = false;
            }

            if (accountRegisterRowCount > this.TransactionsGridView.NumRowsVisible)
            {
                this.TransactionsGridVScrollBar.Visible = false;
            }
            else
            {
                this.TransactionsGridVScrollBar.Visible = true;
            }
        }

        /// <summary>
        /// Grids the click.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void GridClick(int rowIndex)
        {
            if (!this.emptyRecord)
            {
                if (rowIndex >= 0)
                {
                    this.AccountRegisterAuditLink.Text = SharedFunctions.GetResourceString("1213DepositHistoryAuditLink") + " " + this.TransactionsGridView.Rows[rowIndex].Cells[this.accountRegisterDataSet.ListAccountRegister.CLIDColumn.ToString()].Value.ToString();
                    this.clid = Convert.ToInt32(this.TransactionsGridView.Rows[rowIndex].Cells[this.accountRegisterDataSet.ListAccountRegister.CLIDColumn.ToString()].Value);
                    this.cltypeId = Convert.ToInt32(this.TransactionsGridView.Rows[rowIndex].Cells[this.accountRegisterDataSet.ListAccountRegister.CLTypeIDColumn.ToString()].Value);
                }
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the ToolTip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ToolTip_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Control tempControl = (Control)sender;
                if (!String.IsNullOrEmpty(tempControl.Text))
                {
                    if (tempControl.Text.Length > 20)
                    {
                        this.AccountNameToolTip.RemoveAll();
                        this.AccountNameToolTip.SetToolTip(tempControl, tempControl.Text);
                    }
                    else
                    {
                        this.AccountNameToolTip.RemoveAll();
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.ManageException(Ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Displays the endig balance.
        /// </summary>
        private void DisplayEndigBalance()
        {
            decimal endingBalance = 0;
            decimal balanceAmount = 0;
            decimal totalEndingBalance = 0;
            string tempTotalBalance = string.Empty;
            object amountValue;

            if (this.accountRegisterDataSet.ListAccountRegister.Rows.Count > 0)
            {
                amountValue = this.accountRegisterDataSet.ListAccountRegister.Compute("SUM(" + this.accountRegisterDataSet.ListAccountRegister.AmountColumn.ColumnName + ")", "");
                if (amountValue != null)
                {
                    endingBalance = Convert.ToDecimal(amountValue);
                }
                else
                {
                    endingBalance = 0;
                }

                if (this.accountRegisterDataSet.ListAccountRegister.Rows[0][this.accountRegisterDataSet.ListAccountRegister.AmountColumn.ColumnName.ToString()] != DBNull.Value)
                {
                    balanceAmount = this.beginningBalance + Convert.ToDecimal(this.accountRegisterDataSet.ListAccountRegister.Rows[0][this.accountRegisterDataSet.ListAccountRegister.AmountColumn.ColumnName.ToString()]);
                    this.accountRegisterDataSet.ListAccountRegister.Rows[0][this.accountRegisterDataSet.ListAccountRegister.BalanceColumn.ColumnName.ToString()] = balanceAmount;
                }

                for (int i = 1; i < this.accountRegisterDataSet.ListAccountRegister.Rows.Count; i++)
                {
                    if (this.accountRegisterDataSet.ListAccountRegister.Rows[i][this.accountRegisterDataSet.ListAccountRegister.AmountColumn.ColumnName.ToString()] != DBNull.Value)
                    {
                        balanceAmount = balanceAmount + Convert.ToDecimal(this.accountRegisterDataSet.ListAccountRegister.Rows[i][this.accountRegisterDataSet.ListAccountRegister.AmountColumn.ColumnName.ToString()]);
                        this.accountRegisterDataSet.ListAccountRegister.Rows[i][this.accountRegisterDataSet.ListAccountRegister.BalanceColumn.ColumnName.ToString()] = balanceAmount;
                    }
                }
            }

            totalEndingBalance = this.beginningBalance + endingBalance;
            tempTotalBalance = totalEndingBalance.ToString("#,##0.00");
            ////check for negative value
            if (tempTotalBalance.StartsWith("-"))
            {
                tempTotalBalance = string.Concat("(", tempTotalBalance.Replace("-", ""), ")");
                this.EndingBalanceTextBox.ForeColor = Color.Green;
            }
            else
            {
                this.EndingBalanceTextBox.ForeColor = Color.Black;
            }

            this.EndingBalanceTextBox.Text = tempTotalBalance;
        }

        /// <summary>
        /// Clears the fields.
        /// </summary>
        private void ClearFields()
        {
            this.LastReconciledTextBox.Text = string.Empty;
            this.beginningBalance = 0;
            this.BeginningBalanceTextBox.Text = string.Empty;
            this.BeginningDateTextBox.Text = string.Empty;
            this.EndingBalanceTextBox.Text = string.Empty;
        }

        #endregion

        private void AccountRegisterMonthCalendar_Leave(object sender, EventArgs e)
        {
            if (isshift)
            {
                this.ParentForm.ActiveControl = BeginningDateTextBox;
                this.ParentForm.ActiveControl.Focus();
               
            }
            else
            {
                this.ParentForm.ActiveControl = AccountRegisterHelpSmartPart;
                this.ParentForm.ActiveControl.Focus();
            }
          
        }
    }
}
