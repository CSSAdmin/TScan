//--------------------------------------------------------------------------------------------
// <copyright file="F1502.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Function Management. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 9/11/2006   	M.Vijayakumar      Created
//*********************************************************************************/

namespace D1500
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
    /// Class file for F1502
    /// </summary>
    public partial class F1502 : Form
    {
        #region Variable

        /// <summary>
        /// To store the no records present for datagrid
        /// </summary>
        private int accountMgmtgridRowCount;

        /// <summary>
        /// Used to store functionId
        /// </summary>
        private string functionId;

        /// <summary>
        /// This datatable contains the table having semiannualcode and type
        /// </summary>
        private DataTable typeDatatable;

        /// <summary>
        /// This wiil used to store whether unsaved changes exists 
        /// </summary>
        private bool enableButtonStatus;     

        /// <summary>
        /// controller F1502Controller
        /// </summary>
        private F1502Controller form1502Control;

        /// <summary>
        /// To store whether Save is performed
        /// </summary>
        private bool saveStatus;

        /// <summary>
        /// Instance for F1502AccountManagementData
        /// </summary>
        private F1502AccountManagementData accountElementMgmtData = new F1502AccountManagementData();

        /// <summary>
        /// currentRowIndex
        /// </summary>
        private int currentRowIndex;

        /// <summary>
        /// searchFunctionId is to set
        /// </summary>
        private string searchFunctionId;

        /// <summary>
        /// searchDescription is to set
        /// </summary>
        private string searchDescription;

        /// <summary>
        /// searchTypeSemiAnnualCode is set
        /// </summary>
        private int searchTypeSemiAnnualCode;

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        #endregion Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1502"/> class.
        /// </summary>
        public F1502()
        {
            this.enableButtonStatus = true;
            InitializeComponent();
            this.CreateTypeDataTable();
            ////SaveToolStripMenuItem.Click += new EventHandler(this.AccountMgmtSaveButton_Click);           
            this.AcceptButton = this.SearchButton;            
        }

        #endregion Constructor

        #region Property

        /// <summary>
        /// For F8901Control
        /// </summary>
        [CreateNew]
        public F1502Controller Form1502Control
        {
            get { return this.form1502Control as F1502Controller; }
            set { this.form1502Control = value; }
        }

        /// <summary>
        /// Gets or sets the functionValue.
        /// </summary>
        /// <value>The Function.</value>
        public string FunctionIdValue
        {
            get { return this.functionId; }
            set { this.functionId = value; }
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

        #endregion Property

        #region Methods

        /// <summary>
        /// Sets the cancel button.
        /// </summary>
        private void SetCancelButton()
        {
            if (!this.AccountMgmtCancelButton.Enabled)
            {
                this.CancelButton = this.AccountMgmtCloseButton;
            }
            else
            {
                this.CancelButton = this.AccountMgmtCancelButton;
            }
        }

        /// <summary>
        /// To Load or Reload the Entire Form
        /// </summary>
        private void LoadAcctElementMgmtForm()
        {           
            this.ClearFormsearchFilter();           
            this.enableButtonStatus = true;
            this.LoadAccountElementMgmtGrid(null, null, -999);
            this.FunctionTextBox.Focus();
            this.SetCancelButton();
        }

        /// <summary>
        /// Loads the AccountElementMgmtGrid
        /// </summary>
        /// <param name="function">The function.</param>
        /// <param name="description">The description.</param>
        /// <param name="type">The type.</param>
        private void LoadAccountElementMgmtGrid(string function, string description, int type)
        {           
            int emptyRows;
                
            this.accountElementMgmtData = this.form1502Control.WorkItem.F1502_GetAccountElementMgmt(function, description, type);
            this.accountMgmtgridRowCount = this.accountElementMgmtData.GetAccountElementMgmt.Rows.Count;
           
            this.AccountMgmtDataGridView.Columns[this.accountElementMgmtData.GetAccountElementMgmt.FunctionValueColumn.ColumnName].DataPropertyName = this.accountElementMgmtData.GetAccountElementMgmt.FunctionValueColumn.ColumnName;
            this.AccountMgmtDataGridView.Columns[this.accountElementMgmtData.GetAccountElementMgmt.DescriptionColumn.ColumnName].DataPropertyName = this.accountElementMgmtData.GetAccountElementMgmt.DescriptionColumn.ColumnName;
            this.AccountMgmtDataGridView.Columns[this.accountElementMgmtData.GetAccountElementMgmt.SemiAnnualCodeColumn.ColumnName].DataPropertyName = this.accountElementMgmtData.GetAccountElementMgmt.SemiAnnualCodeColumn.ColumnName;
            this.AccountMgmtDataGridView.Columns[this.accountElementMgmtData.GetAccountElementMgmt.FunctionKeyIDColumn.ColumnName].DataPropertyName = this.accountElementMgmtData.GetAccountElementMgmt.FunctionKeyIDColumn.ColumnName;

            (this.AccountMgmtDataGridView.Columns["SemiAnnualCode"] as DataGridViewComboBoxColumn).DataSource = this.typeDatatable;
            (this.AccountMgmtDataGridView.Columns["SemiAnnualCode"] as DataGridViewComboBoxColumn).DisplayMember = "Type";
            (this.AccountMgmtDataGridView.Columns["SemiAnnualCode"] as DataGridViewComboBoxColumn).ValueMember = "SemiAnnualCode";
            
            if (this.accountMgmtgridRowCount > 0)
            {
                ////if the no of visiable rows(grid) is greater than the actual rows from the Datatable ---
                //// --- then a temp datatable with empty rows are merged with GetAccountElementMgmt datatable                    
                if (this.AccountMgmtDataGridView.NumRowsVisible > this.accountMgmtgridRowCount)
                {
                    emptyRows = this.AccountMgmtDataGridView.NumRowsVisible - this.accountMgmtgridRowCount;

                    for (int i = 0; i < emptyRows; i++)
                    {
                        this.accountElementMgmtData.GetAccountElementMgmt.AddGetAccountElementMgmtRow(this.accountElementMgmtData.GetAccountElementMgmt.NewGetAccountElementMgmtRow());                        
                    }                        
                }
                else
                {
                    this.accountElementMgmtData.GetAccountElementMgmt.AddGetAccountElementMgmtRow(this.accountElementMgmtData.GetAccountElementMgmt.NewGetAccountElementMgmtRow());                        
                }                   

                this.AccountMgmtAcceptButton.Enabled = true;
                this.AccountMgmtDataGridView.DataSource = this.accountElementMgmtData.GetAccountElementMgmt;  
                this.AccountMgmtDataGridView.Rows[0].Selected = true;
            }
            else
            {
                for (int i = 0; i < this.AccountMgmtDataGridView.NumRowsVisible; i++)
                {
                    this.accountElementMgmtData.GetAccountElementMgmt.AddGetAccountElementMgmtRow(this.accountElementMgmtData.GetAccountElementMgmt.NewGetAccountElementMgmtRow());                        
                }

                this.AccountMgmtAcceptButton.Enabled = false;
                this.AccountMgmtDataGridView.DataSource = this.accountElementMgmtData.GetAccountElementMgmt;
                this.AccountMgmtDataGridView.Rows[0].Selected = false;                    
            }

            this.AccountMgmtDataGridView.Enabled = true;
            this.AccountMgmtDataGridView.AutoGenerateColumns = false;                              

            /////to enable or disable the vertical scroll bar
            if (this.accountElementMgmtData.GetAccountElementMgmt.Rows.Count > this.AccountMgmtDataGridView.NumRowsVisible)
            {
                this.AccountMgmtVerticalScroll.Visible = false;
            }
            else
            {
                this.AccountMgmtVerticalScroll.Visible = true;
            }

            ////To display the no of display rows in the grid
            this.RecordCountLabel.Text = this.accountMgmtgridRowCount + SharedFunctions.GetResourceString("9101MasterNameSearch");                           
        }

        /// <summary>
        /// Clear Form search Filter
        /// </summary>
        private void ClearFormsearchFilter()
        {
            this.FunctionTextBox.Text = string.Empty;
            this.DescTextBox.Text = string.Empty;
            this.TypeComboBox.SelectedIndex = -1;
            this.SearchButton.Enabled = false;
        }

        /// <summary>
        /// To enable or disable the save,cancel,close and accept buttons
        /// </summary>
        /// <param name="unlock">Boolean Value
        /// when true = controls are enabled
        /// when false = controls are disabled
        /// </param>
        private void EnableSelectButtons(bool unlock)
        {
            this.AccountMgmtSaveButton.Enabled = !unlock;
            this.AccountMgmtCancelButton.Enabled = !unlock;
            this.AccountMgmtCloseButton.Enabled = unlock; 
            this.AccountMgmtAcceptButton.Enabled = unlock;
            this.ClearButton.Enabled = unlock;
            this.SearchButton.Enabled = unlock;
            this.SetCancelButton();
        }

        /// <summary>
        /// Selects the function ID from the account element mgmt grid.
        /// </summary>
        private void SelectFunctionID()
        {
            if (this.accountMgmtgridRowCount >= 0)
            {
                this.GetFunctionId(); 
            }
        }

        /// <summary>
        /// Gets the active work order id.
        /// </summary>
        private void GetFunctionId()
        {
            int rowId = 0;            
            rowId = this.currentRowIndex; 
          
            //// if (this.ActiveWorkRecordDataGridView.Rows.Count > 0)
            if (this.accountMgmtgridRowCount >= 0 && rowId >= 0)
            {
                if (!string.IsNullOrEmpty(this.AccountMgmtDataGridView.Rows[rowId].Cells[this.accountElementMgmtData.GetAccountElementMgmt.FunctionValueColumn.ColumnName].Value.ToString().Trim()))
                {
                    this.functionId = this.AccountMgmtDataGridView.Rows[rowId].Cells[this.accountElementMgmtData.GetAccountElementMgmt.FunctionValueColumn.ColumnName].Value.ToString().Trim();
                    this.commandResult = this.functionId;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    this.DialogResult = DialogResult.None;
                }
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }           
        }       

        /// <summary>
        /// To Create datatable containing SemiAnnualCode and Type columns with data
        /// </summary>
        private void CreateTypeDataTable()
        {
            this.typeDatatable = new DataTable();
            this.typeDatatable.Clear();
            this.typeDatatable.Columns.Add(SharedFunctions.GetResourceString("F1502SemiAnnualCode"), typeof(int));
            this.typeDatatable.Columns.Add(SharedFunctions.GetResourceString("F1502Type"), typeof(string));
            this.typeDatatable.Rows.Add(null, SharedFunctions.GetResourceString("F1502EmptyString"));
            this.typeDatatable.Rows.Add(1, SharedFunctions.GetResourceString("F1502Balancing"));
            this.typeDatatable.Rows.Add(2, SharedFunctions.GetResourceString("F1502Collection"));
            this.typeDatatable.Rows.Add(3, SharedFunctions.GetResourceString("F1502Disbursement"));
            this.typeDatatable.AcceptChanges();           
        }       

        /// <summary>
        /// Inits the type combo box.
        /// </summary>
        private void InitTypeComboBox()
        {           
            ////temp datatable is created   
            DataTable customTypeDatatable = new DataTable();
            customTypeDatatable.Columns.Add(SharedFunctions.GetResourceString("F1502SemiAnnualCode"), typeof(int));
            customTypeDatatable.Columns.Add(SharedFunctions.GetResourceString("F1502Type"), typeof(string));
            customTypeDatatable.Rows.Add(-999, SharedFunctions.GetResourceString("F1502All"));
            customTypeDatatable.Rows.Add(1, SharedFunctions.GetResourceString("F1502Balancing"));
            customTypeDatatable.Rows.Add(2, SharedFunctions.GetResourceString("F1502Collection"));
            customTypeDatatable.Rows.Add(3, SharedFunctions.GetResourceString("F1502Disbursement"));
            customTypeDatatable.AcceptChanges();
            this.TypeComboBox.DataSource = customTypeDatatable;
            this.TypeComboBox.ValueMember = SharedFunctions.GetResourceString("F1502SemiAnnualCode");
            this.TypeComboBox.DisplayMember = SharedFunctions.GetResourceString("F1502Type");
            this.TypeComboBox.SelectedIndex = -1; 
        }

        /// <summary>
        /// Save the Account management items
        /// </summary>
        private void SaveAccountElement()
        {
            int saveConfirm;

            if (this.AccountMgmtSaveButton.Enabled)
            {                
                if (this.ValidateFunctionItems())
                {
                    string accountItems = string.Empty;
                    this.AccountMgmtDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    this.accountElementMgmtData.GetAccountElementMgmt.AcceptChanges();
                    accountItems = TerraScanCommon.GetXmlString(this.accountElementMgmtData.GetAccountElementMgmt);
                    saveConfirm = this.form1502Control.WorkItem.F1502_SaveAccountElementMgmt(accountItems, TerraScanCommon.UserId);

                    if (saveConfirm == -99)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("F1502KeyIdDuplicateValidation1"), ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.saveStatus = false;
                        this.DialogResult = DialogResult.None;
                    }
                    else if (saveConfirm == 0)
                    {
                        ////to reload the entire form afther save function.
                        this.LoadAcctElementMgmtForm();
                        this.EnableSelectButtons(true);
                        this.SearchButton.Enabled = false;
                        this.saveStatus = true;
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.saveStatus = false;
                    this.DialogResult = DialogResult.None;
                }
            }
          
        }

        /// <summary>
        /// Validates the Function data gird items passes true on validation success.
        /// </summary>
        /// <returns>Boolean Value</returns>
        private bool ValidateFunctionItems()
        {
            try
            {
                bool validationResult = true;
                this.AccountMgmtDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                this.accountElementMgmtData.GetAccountElementMgmt.AcceptChanges();
                for (int i = 0; i < this.AccountMgmtDataGridView.Rows.Count; i++)
                {
                    if (validationResult)
                    {
                        if (((!string.IsNullOrEmpty(this.AccountMgmtDataGridView.Rows[i].Cells[0].Value.ToString().Trim())) || (!string.IsNullOrEmpty(this.AccountMgmtDataGridView.Rows[i].Cells[1].Value.ToString().Trim())) || (!string.IsNullOrEmpty(this.AccountMgmtDataGridView.Rows[i].Cells[2].Value.ToString().Trim()))))
                        {
                            if (((string.IsNullOrEmpty(this.AccountMgmtDataGridView.Rows[i].Cells[0].Value.ToString().Trim())) || (string.IsNullOrEmpty(this.AccountMgmtDataGridView.Rows[i].Cells[2].Value.ToString().Trim()))))
                            {
                                validationResult = false;
                            }
                        }
                    }
                }

                return validationResult;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// To verfiy empty rows available
        /// </summary>
        private void CheckEmptyRowsAvaliable()
        {
            if (this.ValidateEmptyRows())
            {
                this.SaveAccountElement();
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.saveStatus = false;
                this.DialogResult = DialogResult.None;
            }
        }

        /// <summary>
        /// To return boolean value whether emprty rows are available
        /// </summary>
        /// <returns>
        /// When false - empty rows are present
        /// when True - No empty rows present
        /// </returns>
        private bool ValidateEmptyRows()
        {
            try
            {
                bool emptyRowAvailable = true;
                this.AccountMgmtDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                this.accountElementMgmtData.GetAccountElementMgmt.AcceptChanges();
                if (this.accountMgmtgridRowCount > 0)
                {
                    for (int i = 0; i < this.accountMgmtgridRowCount; i++)
                    {
                        if (emptyRowAvailable)
                        {
                            if (((string.IsNullOrEmpty(this.AccountMgmtDataGridView.Rows[i].Cells[0].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.AccountMgmtDataGridView.Rows[i].Cells[1].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.AccountMgmtDataGridView.Rows[i].Cells[2].Value.ToString().Trim()))))
                            {
                                emptyRowAvailable = false;
                            }
                        }
                    }     
                }

                return emptyRowAvailable;
            }
            catch (Exception)
            {
                return false;
            } 
        }

        /// <summary>
        /// To show message box for form closing
        /// </summary>
        private void ShowMessageClosing()
        {
            if (this.AccountMgmtSaveButton.Enabled)
            {
                switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.Text + "?", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        {                           
                            ////this.SaveAccountElement();
                            this.CheckEmptyRowsAvaliable();
                            if (this.saveStatus)
                            {
                                this.Close();
                            }
                          
                            break;
                        }

                    case DialogResult.No:
                        {
                            this.DialogResult = DialogResult.Cancel;
                            this.AccountMgmtSaveButton.Enabled = false;
                            this.Close();
                            break;
                        }

                    case DialogResult.Cancel:
                        {
                            this.DialogResult = DialogResult.None;
                            break;
                        }
                }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Handles the Click event of the AccountMgmtCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AccountMgmtCancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.FunctionTextBox.Focus();
                this.EnableSelectButtons(true);                
                ////this.LoadAcctElementMgmtForm();

                ////to disable or enable the search button
                if ((!string.IsNullOrEmpty(this.searchFunctionId)) || (!string.IsNullOrEmpty(this.searchDescription)) || this.searchTypeSemiAnnualCode > 0)
                {
                    this.SearchButton.Enabled = true;
                }
                else
                {
                    this.SearchButton.Enabled = false;
                }

                if (this.searchTypeSemiAnnualCode > 0)
                {
                    this.LoadAccountElementMgmtGrid(this.searchFunctionId, this.searchDescription, this.searchTypeSemiAnnualCode);
                }
                else
                {
                    this.LoadAccountElementMgmtGrid(this.searchFunctionId, this.searchDescription, -999);
                }

                if ((!string.IsNullOrEmpty(this.searchFunctionId)))
                {
                    this.FunctionTextBox.Text = this.searchFunctionId;
                }
                else
                {
                    this.FunctionTextBox.Text = string.Empty;
                }

                if ((!string.IsNullOrEmpty(this.searchDescription)))
                {
                    this.DescTextBox.Text = this.searchDescription;
                }
                else
                {
                    this.DescTextBox.Text = string.Empty;
                }

                if (this.searchTypeSemiAnnualCode == -1)
                {
                    this.TypeComboBox.SelectedIndex = -1;
                }
                else
                {
                    this.TypeComboBox.SelectedValue = this.searchTypeSemiAnnualCode;
                }

                this.FunctionTextBox.Focus();
                this.DialogResult = DialogResult.None;
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the AccountMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AccountMgmtDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && this.AccountMgmtAcceptButton.Enabled)
                {
                    this.SelectFunctionID();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the AccountMgmtAcceptButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AccountMgmtAcceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.accountMgmtgridRowCount >= 0 && this.AccountMgmtAcceptButton.Enabled)
                {
                    this.SelectFunctionID();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the AccountMgmtCloseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AccountMgmtCloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowMessageClosing();
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                // need
                if (!string.IsNullOrEmpty(this.TypeComboBox.Text))
                {
                    this.LoadAccountElementMgmtGrid(this.FunctionTextBox.Text.Trim(), this.DescTextBox.Text.Trim(), Convert.ToInt32(this.TypeComboBox.SelectedValue));
                    this.searchTypeSemiAnnualCode = Convert.ToInt32(this.TypeComboBox.SelectedValue);
                }
                else
                {
                    this.LoadAccountElementMgmtGrid(this.FunctionTextBox.Text.Trim(), this.DescTextBox.Text.Trim(), -999);
                    this.searchTypeSemiAnnualCode = -1;
                }

                this.searchFunctionId = this.FunctionTextBox.Text.Trim();
                this.searchDescription = this.DescTextBox.Text.Trim();

                this.enableButtonStatus = true;
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadAcctElementMgmtForm();

                this.searchFunctionId = string.Empty;
                this.searchDescription = string.Empty;
                this.searchTypeSemiAnnualCode = -1;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Load event of the F1502 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1502_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadAcctElementMgmtForm();
                this.InitTypeComboBox();             
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }       

        /// <summary>
        /// Handles the CellValueChanged event of the AccountMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AccountMgmtDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ////this is to check whether any unsaved changes exisits
                if (!this.enableButtonStatus)
                {
                    this.EnableSelectButtons(false);
                }
                else
                {
                    this.EnableSelectButtons(true);
                    this.enableButtonStatus = true;
                }       
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the AccountMgmtSaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AccountMgmtSaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////this.SaveAccountElement();
                this.CheckEmptyRowsAvaliable();
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }       

        /// <summary>
        /// Handles the KeyDown event of the AccountMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void AccountMgmtDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try              
            {
                this.Cursor = Cursors.WaitCursor;
                if (e.KeyValue == 46)
                {
                    if (!this.AccountMgmtSaveButton.Enabled)
                    {
                        string functionValue;
                        functionValue = this.AccountMgmtDataGridView.Rows[AccountMgmtDataGridView.CurrentRowIndex].Cells[0].Value.ToString().Trim();
                        this.form1502Control.WorkItem.F1502_DeleteAccountElementMgmt(functionValue, TerraScanCommon.UserId);
                        this.FunctionTextBox.Focus();
                        ////to reload the entire form afther delete function.
                        this.LoadAcctElementMgmtForm();
                    }                    
                }
            }
            catch (SoapException ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.Display, this.ParentForm);
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
        /// Handles the CellBeginEdit event of the AccountMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void AccountMgmtDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {          
                if (e.ColumnIndex == this.AccountMgmtDataGridView.Columns[this.accountElementMgmtData.GetAccountElementMgmt.FunctionValueColumn.ColumnName].Index || e.ColumnIndex == this.AccountMgmtDataGridView.Columns[this.accountElementMgmtData.GetAccountElementMgmt.DescriptionColumn.ColumnName].Index || e.ColumnIndex == this.AccountMgmtDataGridView.Columns[this.accountElementMgmtData.GetAccountElementMgmt.SemiAnnualCodeColumn.ColumnName].Index)
                {
                    this.enableButtonStatus = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowHeaderMouseClick event of the AccountMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void AccountMgmtDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.AccountMgmtDataGridView.CurrentCell != null)
                {
                    this.AccountMgmtDataGridView.CurrentCell.ReadOnly = true;
                    this.AccountMgmtDataGridView.Rows[e.RowIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }       

        /// <summary>
        /// Handles the CellClick event of the AccountMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AccountMgmtDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == 0)
                {
                    this.AccountMgmtDataGridView["FunctionValue", e.RowIndex].ReadOnly = false;
                    this.AccountMgmtDataGridView["Description", e.RowIndex].ReadOnly = false;
                    this.AccountMgmtDataGridView["SemiAnnualCode", e.RowIndex].ReadOnly = false;
                    this.AccountMgmtDataGridView.Rows[e.RowIndex].Selected = false;
                }

                bool hasValues = false;
                if (e.RowIndex >= 1)
                {
                    if ((string.IsNullOrEmpty(this.AccountMgmtDataGridView["FunctionValue", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.AccountMgmtDataGridView["Description", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.AccountMgmtDataGridView["SemiAnnualCode", (e.RowIndex - 1)].Value.ToString().Trim())))
                    {
                        if (e.RowIndex + 1 < AccountMgmtDataGridView.RowCount)
                        {
                            for (int i = e.RowIndex; i < AccountMgmtDataGridView.RowCount; i++)
                            {
                                if (!string.IsNullOrEmpty(this.AccountMgmtDataGridView.Rows[i].Cells[0].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.AccountMgmtDataGridView.Rows[i].Cells[2].Value.ToString().Trim()))
                                {
                                    hasValues = true;
                                    break;
                                }
                            }

                            if (hasValues)
                            {
                                this.AccountMgmtDataGridView["FunctionValue", e.RowIndex].ReadOnly = false;
                                this.AccountMgmtDataGridView["Description", e.RowIndex].ReadOnly = false;
                                this.AccountMgmtDataGridView["SemiAnnualCode", e.RowIndex].ReadOnly = false;
                                this.AccountMgmtDataGridView.Rows[e.RowIndex].Selected = false;
                            }
                            else
                            {
                                if ((string.IsNullOrEmpty(this.AccountMgmtDataGridView["FunctionValue", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.AccountMgmtDataGridView["Description", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.AccountMgmtDataGridView["SemiAnnualCode", e.RowIndex].Value.ToString().Trim())))
                                {
                                    this.AccountMgmtDataGridView["FunctionValue", e.RowIndex].ReadOnly = true;
                                    this.AccountMgmtDataGridView["Description", e.RowIndex].ReadOnly = true;
                                    this.AccountMgmtDataGridView["SemiAnnualCode", e.RowIndex].ReadOnly = true;
                                }
                                else
                                {
                                    this.AccountMgmtDataGridView["FunctionValue", e.RowIndex].ReadOnly = false;
                                    this.AccountMgmtDataGridView["Description", e.RowIndex].ReadOnly = false;
                                    this.AccountMgmtDataGridView["SemiAnnualCode", e.RowIndex].ReadOnly = false;
                                    this.AccountMgmtDataGridView.Rows[e.RowIndex].Selected = false;
                                }
                            }
                        }
                        else
                        {
                            this.AccountMgmtDataGridView["FunctionValue", e.RowIndex].ReadOnly = true;
                            this.AccountMgmtDataGridView["Description", e.RowIndex].ReadOnly = true;
                            this.AccountMgmtDataGridView["SemiAnnualCode", e.RowIndex].ReadOnly = true;
                        }
                    }
                    else
                    {
                        this.AccountMgmtDataGridView["FunctionValue", e.RowIndex].ReadOnly = false;
                        this.AccountMgmtDataGridView["Description", e.RowIndex].ReadOnly = false;
                        this.AccountMgmtDataGridView["SemiAnnualCode", e.RowIndex].ReadOnly = false;
                        this.AccountMgmtDataGridView.Rows[e.RowIndex].Selected = false;
                    }
                }

                this.currentRowIndex = e.RowIndex;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the ColumnHeaderMouseClick event of the AccountMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void AccountMgmtDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2)
                {
                    this.AccountMgmtDataGridView.CurrentCell.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }        

        /// <summary>
        /// Handles the CellEndEdit event of the AccountMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AccountMgmtDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (((e.RowIndex + 1) == this.AccountMgmtDataGridView.Rows.Count) && (e.ColumnIndex == 0))
                {
                    if (!string.IsNullOrEmpty(this.AccountMgmtDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Trim()))
                    {
                        this.accountElementMgmtData.GetAccountElementMgmt.Rows.InsertAt(this.accountElementMgmtData.GetAccountElementMgmt.NewGetAccountElementMgmtRow(), this.AccountMgmtDataGridView.Rows.Count);                   
                        (AccountMgmtDataGridView.Rows[e.RowIndex].Cells[this.accountElementMgmtData.GetAccountElementMgmt.SemiAnnualCodeColumn.ColumnName.ToString()] as DataGridViewComboBoxCell).Value = 2;
                        this.AccountMgmtVerticalScroll.Visible = false;
                    }

                    
                }

                if ((e.ColumnIndex == 0) && (string.IsNullOrEmpty(this.AccountMgmtDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString().Trim())))
                {
                    if (!string.IsNullOrEmpty(this.AccountMgmtDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Trim()))
                    {
                        (AccountMgmtDataGridView.Rows[e.RowIndex].Cells[this.accountElementMgmtData.GetAccountElementMgmt.SemiAnnualCodeColumn.ColumnName.ToString()] as DataGridViewComboBoxCell).Value = 2;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }       

        /// <summary>
        /// Handles the RowEnter event of the AccountMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AccountMgmtDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool hasValues = false;
                if (e.RowIndex >= 1)
                {
                    if ((string.IsNullOrEmpty(this.AccountMgmtDataGridView.Rows[(e.RowIndex - 1)].Cells[0].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.AccountMgmtDataGridView.Rows[(e.RowIndex - 1)].Cells[1].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.AccountMgmtDataGridView.Rows[(e.RowIndex - 1)].Cells[2].Value.ToString().Trim()))) 
                    {
                        if (e.RowIndex + 1 < AccountMgmtDataGridView.RowCount)
                        {
                            for (int i = e.RowIndex; i < AccountMgmtDataGridView.RowCount; i++)
                            {
                                if (!string.IsNullOrEmpty(this.AccountMgmtDataGridView.Rows[i].Cells[0].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.AccountMgmtDataGridView.Rows[i].Cells[2].Value.ToString().Trim()))
                                {
                                    hasValues = true;
                                    break;
                                }
                            }

                            if (hasValues)
                            {
                                this.AccountMgmtDataGridView["FunctionValue", e.RowIndex].ReadOnly = false;
                                this.AccountMgmtDataGridView["Description", e.RowIndex].ReadOnly = false;
                                this.AccountMgmtDataGridView["SemiAnnualCode", e.RowIndex].ReadOnly = false;
                                this.AccountMgmtDataGridView.Rows[e.RowIndex].Selected = false;
                            }
                            else
                            {
                                if ((string.IsNullOrEmpty(this.AccountMgmtDataGridView["FunctionValue", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.AccountMgmtDataGridView["Description", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.AccountMgmtDataGridView["SemiAnnualCode", e.RowIndex].Value.ToString().Trim())))
                                {
                                    this.AccountMgmtDataGridView["FunctionValue", e.RowIndex].ReadOnly = true;
                                    this.AccountMgmtDataGridView["Description", e.RowIndex].ReadOnly = true;
                                    this.AccountMgmtDataGridView["SemiAnnualCode", e.RowIndex].ReadOnly = true;
                                }
                                else
                                {
                                    this.AccountMgmtDataGridView["FunctionValue", e.RowIndex].ReadOnly = false;
                                    this.AccountMgmtDataGridView["Description", e.RowIndex].ReadOnly = false;
                                    this.AccountMgmtDataGridView["SemiAnnualCode", e.RowIndex].ReadOnly = false;
                                    this.AccountMgmtDataGridView.Rows[e.RowIndex].Selected = false;
                                }
                            }
                        }
                        else
                        {
                            this.AccountMgmtDataGridView["FunctionValue", e.RowIndex].ReadOnly = true;
                            this.AccountMgmtDataGridView["Description", e.RowIndex].ReadOnly = true;
                            this.AccountMgmtDataGridView["SemiAnnualCode", e.RowIndex].ReadOnly = true;
                        }
                    }
                    else
                    {
                        this.AccountMgmtDataGridView["FunctionValue", e.RowIndex].ReadOnly = false;
                        this.AccountMgmtDataGridView["Description", e.RowIndex].ReadOnly = false;
                        this.AccountMgmtDataGridView["SemiAnnualCode", e.RowIndex].ReadOnly = false;
                        this.AccountMgmtDataGridView.Rows[e.RowIndex].Selected = false;
                    }                    
                }

                if (e.RowIndex == 0)
                {
                    this.AccountMgmtDataGridView["FunctionValue", e.RowIndex].ReadOnly = false;
                    this.AccountMgmtDataGridView["Description", e.RowIndex].ReadOnly = false;
                    this.AccountMgmtDataGridView["SemiAnnualCode", e.RowIndex].ReadOnly = false;
                    this.AccountMgmtDataGridView.Rows[e.RowIndex].Selected = false;
                }

               this.currentRowIndex = e.RowIndex;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }            

        /// <summary>
        /// Handles the RowHeaderMouseDoubleClick event of the AccountMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void AccountMgmtDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.AccountMgmtAcceptButton.Enabled)
                {
                    this.SelectFunctionID();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Editexts the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Editext(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(this.FunctionTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.DescTextBox.Text.Trim())) || (this.TypeComboBox.SelectedIndex > -1))
                {
                    if (this.AccountMgmtSaveButton.Enabled)
                    {
                        this.SearchButton.Enabled = false;
                    }
                    else
                    {
                        this.SearchButton.Enabled = true;
                    }
                }
                else
                {
                    this.SearchButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }       

        /// <summary>
        /// Handles the SelectedValueChanged event of the TypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TypeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(this.FunctionTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.DescTextBox.Text.Trim())) || (this.TypeComboBox.SelectedIndex > -1))
                {
                    if (this.AccountMgmtSaveButton.Enabled)
                    {
                        this.SearchButton.Enabled = false;
                    }
                    else
                    {
                        this.SearchButton.Enabled = true;
                    }
                }
                else
                {
                    this.SearchButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the F1502 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void F1502_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!e.CloseReason.Equals(CloseReason.ApplicationExitCall))
                {
                    if (e.CloseReason.Equals(CloseReason.UserClosing))
                    {
                        if (this.AccountMgmtSaveButton.Enabled)
                        {
                            switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.Text + "?", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                            {
                                case DialogResult.Yes:
                                    {
                                        try
                                        {
                                            ////this.SaveAccountElement();
                                            this.CheckEmptyRowsAvaliable();
                                            if (this.saveStatus)
                                            {
                                                this.DialogResult = DialogResult.Cancel;
                                                e.Cancel = false;
                                            }
                                            else
                                            {
                                                e.Cancel = true;
                                            }
                                        }
                                        catch (SoapException ex)
                                        {
                                            ////TODO : Need to find specific exception and handle it.
                                            ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                                        }
                                        catch (Exception ex)
                                        {
                                            ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                                        }

                                        break;
                                    }

                                case DialogResult.No:
                                    {
                                        this.DialogResult = DialogResult.Cancel;
                                        e.Cancel = false;
                                        break;
                                    }

                                case DialogResult.Cancel:
                                    {
                                        e.Cancel = true;
                                        this.DialogResult = DialogResult.None;
                                        break;
                                    }
                            }
                        }
                    }
                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Tooltip is not needed for Comboboxcolumn in the datagridview
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AccountMgmtDataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ////ColumnIndex = 2 - Comboboxcomlumn in Datagridview
                if (e.ColumnIndex == 2)
                {
                    this.AccountMgmtDataGridView.ShowCellToolTips = false;
                }
                else
                {
                    this.AccountMgmtDataGridView.ShowCellToolTips = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the AccountMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void AccountMgmtDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
                e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                e.Control.Validated += new EventHandler(this.Control_Validated);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validated event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_Validated(object sender, EventArgs e)
        {
            try
            {
                this.AccountMgmtDataGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ////Here the variable/method or whatever which causes the unsaved change to fire can be written
                this.EnableSelectButtons(false);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.FunctionTextBox.Focus();
                this.CheckEmptyRowsAvaliable();
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Events
    }
}