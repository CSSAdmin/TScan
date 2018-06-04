// --------------------------------------------------------------------------------------------
// <copyright file="F1345.cs" company="Congruent">
//      Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//  This file contains methods for the Account Selection.
// </summary>
// ----------------------------------------------------------------------------------------------
// Change History
// **********************************************************************************
// Date               Author            Description
// ----------        ---------          ---------------------------------------------------------
// 26 July 06      KRISHNA ABBURI       Created
// 31 Aug  09      Sadha Shivudu M      Implemented TSCO # 3372 Add new field function. 
// *********************************************************************************/

namespace D1500
{
    using System;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Utilities;

    /// <summary>
    /// F1345 class
    /// </summary> 
    public partial class F1345 : Form
    {
        #region Variables

        /// <summary>
        /// instance variable to hold the account selection dataset.
        /// </summary>
        private AccountSelectionData accountSlectionDataset = new AccountSelectionData();

        /// <summary>
        /// instance variable to hold the empty record status.
        /// </summary>  
        private bool emptyRecord;

        /// <summary>
        /// instance variable to hold the selected row index value.
        /// </summary>
        private int selected;

        /// <summary>
        /// instance variable to hold the f1345 form controller.
        /// </summary>
        private F1345Controller form1345Control;

        /// <summary>
        /// instance variable to hold the account id value.
        /// </summary>
        private int accountId;

        /// <summary>
        /// int rollyear
        /// </summary>
        private int rollYearParameter;

        /// <summary>
        /// instance variable to hold the selected account name value.
        /// </summary>
        private string selectedAccountName = string.Empty;

        /// <summary>
        /// instance variable to hold the cash value.
        /// </summary>
        private int iscash;

        /// <summary>
        /// instance variable to hold the commnad result value.
        /// </summary>
        private string commandResult;

        #endregion Variables

        #region Constructor
        
        /// <summary>
        /// Initializes a new instance of the <see cref="F1345"/> class.
        /// </summary>
        public F1345()
        {
            this.InitializeComponent();
            this.CancelButton = this.AccountCancelButton;
            this.AcceptButton = this.AccountSearchButton;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="F1345"/> class.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        public F1345(string rollYear)
        {
            this.InitializeComponent();
            this.CancelButton = this.AccountCancelButton;
            this.AcceptButton = this.AccountSearchButton;
            this.RollYearTextBox.Text = rollYear;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="F1345"/> class.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        public F1345(int rollYear)
        {
            this.InitializeComponent();
            rollYearParameter = rollYear;
            this.RollYearTextBox.Text = rollYear.ToString();
            this.CancelButton = this.AccountCancelButton;
            this.AcceptButton = this.AccountSearchButton;       
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F1345"/> class.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="accountType">Type of the account.</param>
        public F1345(int rollYear, int accountType)
        {
            this.InitializeComponent();
            this.CancelButton = this.AccountCancelButton;
            this.AcceptButton = this.AccountSearchButton;
            this.iscash = accountType;
            this.RollYearTextBox.Text = rollYear.ToString();
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// Occurs when [show form].
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region properites

        /// <summary>
        /// Gets or sets the F1345 controll.
        /// </summary>
        /// <value>The F1345 controll.</value>
        [CreateNew]
        public F1345Controller F1345Controll
        {
            get { return this.form1345Control as F1345Controller; }
            set { this.form1345Control = value; }
        }

        /// <summary>
        /// Gets or sets the account id.
        /// </summary>
        /// <value>The account id.</value>
        public int AccountId
        {
            get { return this.accountId; }
            set { this.accountId = value; }
        }

        /// <summary>
        /// Gets or sets the is cash.
        /// </summary>
        /// <value>The is cash.</value>
        public int IsCash
        {
            get { return this.iscash; }
            set { this.iscash = value; }
        }

        /// <summary>
        /// Gets or sets the name of the selected account.
        /// </summary>
        /// <value>The name of the selected account.</value>
        public string SelectedAccountName
        {
            get { return this.selectedAccountName; }
            set { this.selectedAccountName = value; }
        }

        /// <summary>
        /// Gets or sets the roll year value.
        /// </summary>
        /// <value>The roll year value.</value>
        public string RollYearValue
        {
            get { return this.RollYearTextBox.Text; }
            set { this.RollYearTextBox.Text = value; }
        }

        /// <summary>
        /// Gets or sets the command result.
        /// </summary>
        /// <value>The command result.</value>
        public string CommandResult
        {
            get { return this.commandResult; }
            set { this.commandResult = value; }
        }

        #endregion properites

        #region Events

        /// <summary>
        /// Handles the Load event of the F1345 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F1345_Load(object sender, EventArgs e)
        {
            try
            {
                this.emptyRecord = true;
                this.CustomizeDataGrid();
                this.AccountSelectionDataGridView.DataSource = null;
                this.AccountSelectionDataGridView.Enabled = false;
                if (!string.IsNullOrEmpty(this.RollYearTextBox.Text))
                {
                   // this.RollYearTextBox.Select();
                    this.AccountClearButton.Enabled = true;
                    this.AccountSearchButton.Enabled = true;
                    this.SubFundTextBox.Focus();
                }
                else
                {
                    this.SubFundTextBox.Focus();
                    this.AccountClearButton.Enabled = false;
                    this.AccountSearchButton.Enabled = false;
                }

                this.AccountSlectionVerticalScroll.Visible = true;
                this.AccountSlectionVerticalScroll.Enabled = false;
                ////this.PopulateAccountDetailsDatagrid();
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the AccountSearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AccountSearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.PopulateAccountDetailsDatagrid();
            }
            catch (SoapException exc1)
            {
                ExceptionManager.ManageException(exc1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the AccountCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AccountCancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.AccountCancelButton.Enabled)
                {
                    this.DialogResult = DialogResult.No;
                    this.Close();
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the AccountClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AccountClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.accountSlectionDataset.ListAccountSelection.Clear();
                this.AccountSelectionDataGridView.DataSource = null;
                this.AccountSelectionDataGridView.Enabled = false;
                this.AccountSelectionDataGridView.Rows[0].Selected = false;
                this.ClearFields();
                this.DisableButtons();
                this.DisableVScrollBar();
                this.SubFundTextBox.Focus();
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the AccountSelectionDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AccountSelectionDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!this.emptyRecord)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (!string.IsNullOrEmpty(this.AccountSelectionDataGridView.Rows[e.RowIndex].Cells["ID"].Value.ToString()))
                        {
                            int.TryParse(this.AccountSelectionDataGridView.Rows[e.RowIndex].Cells["ID"].Value.ToString(), out this.accountId);
                            this.commandResult = this.accountId.ToString();
                            this.selectedAccountName = this.AccountSelectionDataGridView.Rows[e.RowIndex].Cells["AccountName"].Value.ToString();
                            this.SubFundTextBox.Focus();
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the AccountAcceptButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AccountAcceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.emptyRecord)
                {
                    this.GetAccountDetails();
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the AccountManagementLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void AccountManagementLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11007);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        
        /// <summary>
        /// Edits the text.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EditText(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(this.SubFundTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.BarsTextBox.Text.Trim())) 
                    || (!string.IsNullOrEmpty(this.LineTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.ObjectTextBox.Text.Trim())) 
                    || (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim()))
                    || (!string.IsNullOrEmpty(this.FunctionTextBox.Text.Trim())))
                {
                    this.EnableSearchButton();
                }
                else
                {
                    if (this.accountSlectionDataset.ListAccountSelection.Rows.Count > 0)
                    {
                        this.AccountSearchButton.Enabled = false;
                    }
                    else
                    {
                        this.DisableButtons();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        
        /// <summary>
        /// Handles the PreviewKeyDown event of the AccountManagementLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void AccountManagementLinkLabel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyValue.Equals((int)Keys.Up) || e.KeyValue.Equals((int)Keys.Down) || e.KeyValue.Equals((int)Keys.Left) || e.KeyValue.Equals((int)Keys.Right))
                {
                    if (AccountSelectionDataGridView.OriginalRowCount > 0)
                    {
                        this.AccountSelectionDataGridView.Focus();
                    }
                    else
                    {
                        this.DescriptionTextBox.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Events

        #region Methods

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        /// <returns>The selected row index.</returns>
        private int GetRowIndex()
        {
            try
            {
                if (this.accountSlectionDataset.ListAccountSelection.Rows.Count > 0)
                {
                    if (this.AccountSelectionDataGridView.SelectedRows.Count > 0)
                    {
                        this.selected = this.AccountSelectionDataGridView.SelectedRows[0].Index;
                    }
                    else if (this.AccountSelectionDataGridView.SelectedCells.Count > 0)
                    {
                        this.selected = this.AccountSelectionDataGridView.CurrentCell.RowIndex;
                    }

                    return this.selected;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        
        /// <summary>
        /// Disables the V scroll bar.
        /// </summary>
        private void DisableVScrollBar()
        {
            if (this.accountSlectionDataset.ListAccountSelection.Rows.Count > 5)
            {
                this.AccountSlectionVerticalScroll.Enabled = true;
                this.AccountSlectionVerticalScroll.Visible = false;
            }
            else
            {
                this.AccountSlectionVerticalScroll.Enabled = false;
                this.AccountSlectionVerticalScroll.Visible = true;
                this.AccountSlectionVerticalScroll.BringToFront();
            }
        }

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizeDataGrid()
        {
            this.AccountSelectionDataGridView.AllowUserToResizeColumns = false;
            this.AccountSelectionDataGridView.AutoGenerateColumns = false;
            this.AccountSelectionDataGridView.AllowUserToResizeRows = false;
            this.AccountSelectionDataGridView.StandardTab = true;

            this.AccountName.DataPropertyName = this.accountSlectionDataset.ListAccountSelection.AccountNameColumn.ColumnName;
            this.Description.DataPropertyName = this.accountSlectionDataset.ListAccountSelection.DescriptionColumn.ColumnName;
            this.RollYear.DataPropertyName = this.accountSlectionDataset.ListAccountSelection.RollYearColumn.ColumnName;
            this.ID.DataPropertyName = this.accountSlectionDataset.ListAccountSelection.AccountIDColumn.ColumnName;
        }

        /// <summary>
        /// Enables the search button.
        /// </summary>
        private void EnableSearchButton()
        {
            if (!string.IsNullOrEmpty(this.SubFundTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.BarsTextBox.Text.Trim())
                || !string.IsNullOrEmpty(this.ObjectTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.LineTextBox.Text.Trim())
                || !string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim())
                || !string.IsNullOrEmpty(this.FunctionTextBox.Text.Trim()))
            {
                this.AccountSearchButton.Enabled = true;
                this.AccountClearButton.Enabled = true;
            }
            else
            {
                this.AccountSearchButton.Enabled = false;

                if (this.accountSlectionDataset.ListAccountSelection.Rows.Count <= 0)
                {
                    this.AccountAcceptButton.Enabled = false;
                    this.AccountClearButton.Enabled = false;
                }
                else
                {
                    this.AccountAcceptButton.Enabled = true;
                    this.AccountClearButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Gets the account details.
        /// </summary>
        private void GetAccountDetails()
        {
            this.Cursor = Cursors.WaitCursor;
            int rowId = 0;
            int tempRollYear = 0;
            rowId = this.GetRowIndex();

            if (!this.emptyRecord)
            {
                if (!string.IsNullOrEmpty(this.AccountSelectionDataGridView.Rows[rowId].Cells["ID"].Value.ToString()))
                {
                    int.TryParse(this.AccountSelectionDataGridView.Rows[rowId].Cells["ID"].Value.ToString(), out this.accountId);
                    this.commandResult = this.accountId.ToString();

                    this.SubFundTextBox.Focus();
                    if (!string.IsNullOrEmpty(this.AccountSelectionDataGridView.Rows[rowId].Cells["AccountName"].Value.ToString()))
                    {
                        this.SelectedAccountName = this.AccountSelectionDataGridView.Rows[rowId].Cells["AccountName"].Value.ToString();
                        this.SubFundTextBox.Focus();
                    }

                    ////added code to send gridview RollYear
                    if (!string.IsNullOrEmpty(this.AccountSelectionDataGridView.Rows[rowId].Cells["RollYear"].Value.ToString()))
                    {
                        int.TryParse(this.AccountSelectionDataGridView.Rows[rowId].Cells["RollYear"].Value.ToString(), out tempRollYear);
                        this.RollYearTextBox.Text = tempRollYear.ToString();
                    }
                }
            }

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Clears the fields.
        /// </summary>
        private void ClearFields()
        {
            this.SubFundTextBox.Text = string.Empty;
            this.BarsTextBox.Text = string.Empty;
            this.FunctionTextBox.Text = string.Empty;
            this.ObjectTextBox.Text = string.Empty;
            this.LineTextBox.Text = string.Empty;
            if (RollYearTextBox.Text == RollYearValue || RollYearTextBox.Text == Convert.ToString(rollYearParameter))
            {
                this.RollYearTextBox.Text = rollYearParameter.ToString();
            }
            else
            {
                this.RollYearTextBox.Text = string.Empty;
            }
            this.DescriptionTextBox.Text = string.Empty;
            this.RecordCountLabel.Text = string.Empty;
        }

        /// <summary>
        /// Disables the buttons.
        /// </summary>
        private void DisableButtons()
        {
            this.AccountSearchButton.Enabled = false;
            this.AccountAcceptButton.Enabled = false;
            this.AccountClearButton.Enabled = false;
        }

        /// <summary>
        /// Populates the account details datagrid.
        /// </summary>
        private void PopulateAccountDetailsDatagrid()
        {
            this.Cursor = Cursors.WaitCursor;
            if (this.AccountSearchButton.Enabled)
            {
                int recordCount = 0;
                int rollYear = 0;

                string subFund = this.SubFundTextBox.Text.Trim().Replace("'", "''");
                string bars = this.BarsTextBox.Text.Trim().Replace("'", "''");
                string function = this.FunctionTextBox.Text.Trim().Replace("'", "''");
                string objectt = this.ObjectTextBox.Text.Trim().Replace("'", "''");
                string line = this.LineTextBox.Text.Trim().Replace("'", "''");
                string desc = this.DescriptionTextBox.Text.Trim().Replace("'", "''");

                if (!(string.IsNullOrEmpty(subFund) && string.IsNullOrEmpty(bars)
                    && string.IsNullOrEmpty(objectt) && string.IsNullOrEmpty(line)
                    && string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim())
                    && string.IsNullOrEmpty(function) && string.IsNullOrEmpty(desc)))
                {
                    if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
                    {
                        int.TryParse(this.RollYearTextBox.Text.Trim(), out rollYear);
                    }
                    else
                    {
                        rollYear = -999;
                    }

                    this.accountSlectionDataset = F1345WorkItem.GetAccountSelectionData(subFund, bars, function, objectt, line, rollYear, desc, this.iscash);

                    if (this.accountSlectionDataset.ListAccountSelection.Rows.Count > 0)
                    {
                        recordCount = this.accountSlectionDataset.ListAccountSelection.Rows.Count;
                        this.AccountSelectionDataGridView.Enabled = true;
                        this.AccountSelectionDataGridView.DataSource = this.accountSlectionDataset.ListAccountSelection;
                        this.AccountAcceptButton.Enabled = true;
                        this.AccountSelectionDataGridView.Focus();
                        this.AccountSelectionDataGridView.Rows[0].Selected = true;
                        this.emptyRecord = false;
                    }
                    else
                    {
                        this.AccountSelectionDataGridView.DataSource = this.accountSlectionDataset.ListAccountSelection;
                        this.AccountSelectionDataGridView.Enabled = false;
                        this.AccountSelectionDataGridView.Rows[0].Selected = false;
                        this.emptyRecord = true;
                        this.AccountAcceptButton.Enabled = false;
                    }

                    this.RecordCountLabel.Text = recordCount + SharedFunctions.GetResourceString("9101MasterNameSearch");
                    this.DisableVScrollBar();
                }
                else
                {
                    this.RecordCountLabel.Text = recordCount + SharedFunctions.GetResourceString("9101MasterNameSearch");
                    this.DisableVScrollBar();
                    this.AccountSelectionDataGridView.DataSource = null;
                    this.AccountSelectionDataGridView.Enabled = false;
                    this.AccountSelectionDataGridView.Rows[0].Selected = false;
                    this.DisableVScrollBar();
                    this.ClearFields();
                    this.DisableButtons();
                    this.SubFundTextBox.Focus();
                }
            }

            this.Cursor = Cursors.Default;
        }

        #endregion Methods
    }
}