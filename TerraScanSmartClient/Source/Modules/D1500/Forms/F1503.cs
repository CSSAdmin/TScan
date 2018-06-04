//--------------------------------------------------------------------------------------------
// <copyright file="F1503.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Generic Element Management. 
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
    /// Class file for F1503
    /// </summary>
    public partial class F1503 : Form
    {
        #region Variable

        /// <summary>
        /// Used to store the Grid rows count
        /// </summary>
        private int genericMgmtgridRowCount;

        /// <summary>
        /// Used to store the boolean value to enable or diable the Accept and other Buttons
        /// </summary>
        private bool enableButtonStatus;

        /// <summary>
        /// To store the Element Key Id
        /// </summary>
        private string elementKeyId;

        /// <summary>
        /// Used to store the Form name 
        /// </summary>
        private string formName;

        /// <summary>
        /// To store Whether Save is performed 
        /// </summary>
        private bool saveStatus;

        /// <summary>
        /// controller F1503Controller
        /// </summary>
        private F1503Controller form1503Control;

        /// <summary>
        /// Instances for F1503GenericManagementData
        /// </summary>
        private F1503GenericManagementData genericManagementData = new F1503GenericManagementData();

        /// <summary>
        /// currentRowIndex
        /// </summary>
        private int currentRowIndex;

        /// <summary>
        /// searchKeyId
        /// </summary>
        private string searchKeyId;

        /// <summary>
        /// searchDescription
        /// </summary>
        private string searchDescription;

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        #endregion Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1503"/> class.
        /// </summary>
        public F1503()
        {
            this.enableButtonStatus = true;
            this.InitializeComponent();            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1503"/> class.
        /// </summary>
        /// <param name="formName">Name of the form.</param>
        public F1503(string formName)
        {
            this.enableButtonStatus = true;
            this.InitializeComponent();                        
            this.formName = formName;            
        }

        #endregion Constructor

        #region Property

        /// <summary>
        /// For 1503Control
        /// </summary>
        [CreateNew]
        public F1503Controller Form1503Control
        {
            get { return this.form1503Control as F1503Controller; }
            set { this.form1503Control = value; }
        }

        /// <summary>
        /// Gets or sets the elementKeyId.
        /// </summary>
        /// <value>The ElementKeyId.</value>
        public string ElementKeyId
        {
            get { return this.elementKeyId; }
            set { this.elementKeyId = value; }
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
            if (!this.GenericMgmtCancelButton.Enabled)
            {
                this.CancelButton = this.GenericMgmtCloseButton;
            }
            else
            {
                this.CancelButton = this.GenericMgmtCancelButton;
            }
        }

        /// <summary>
        /// To Load or Reload the F1503 form
        /// </summary>
        private void LoadGenericElementMgmtForm()
        {           
            this.ClearFormsearchFilter();
            this.enableButtonStatus = true;
            this.LoadGenericElementMgmtGrid(null, null, this.formName);
            this.SetFormName();
            this.NameTextBox.Focus();
            this.SetCancelButton();
        }

        /// <summary>
        /// To Clear the Text Boxs
        /// </summary>
        private void ClearFormsearchFilter()
        {
            this.NameTextBox.Text = string.Empty;
            this.DescTextBox.Text = string.Empty;
            ////TO DISABLE THE SEARCH BUTTON
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
            this.GenericMgmtSaveButton.Enabled = !unlock;
            this.GenericMgmtCancelButton.Enabled = !unlock;
            this.GenericMgmtCloseButton.Enabled = unlock;
            this.GenericMgmtAcceptButton.Enabled = unlock;
            this.ClearButton.Enabled = unlock;
            this.SearchButton.Enabled = unlock;
            this.SetCancelButton();
        }

        /// <summary>
        /// To Set the Form Title and Name Label Text
        /// </summary>
        private void SetFormName()
        {
            this.NameLabel.Text = this.formName + ":";
            this.Text = SharedFunctions.GetResourceString("F1503FormNameTitle") + this.formName;            
        }

        /// <summary>
        /// To Load the Generic Element Mgmt Grid
        /// </summary>
        /// <param name="keyValue">The Element Key Value</param>
        /// <param name="description">The Description</param>
        /// <param name="formNameValue">The formNameValue(Element Name)</param>
        private void LoadGenericElementMgmtGrid(string keyValue, string description, string formNameValue)
        {   
            int emptyRows;
            if (this.formName != null)
            {
                this.genericManagementData = this.form1503Control.WorkItem.F1503_GetGenericElementMgmt(keyValue, description, formNameValue);
            }
            this.genericMgmtgridRowCount = this.genericManagementData.GetGenericElementMgmt.Rows.Count;

            this.ElementKeyIDValue.DataPropertyName = this.genericManagementData.GetGenericElementMgmt.ElementKeyNameColumn.ColumnName;
            this.Description.DataPropertyName = this.genericManagementData.GetGenericElementMgmt.DescriptionColumn.ColumnName;
            this.ElementKeyIDValue.DataPropertyName = this.genericManagementData.GetGenericElementMgmt.ElementKeyIDValueColumn.ColumnName;

            //////this.GenericMgmtDataGridView.Columns[this.genericManagementData.GetGenericElementMgmt.ElementKeyNameColumn.ColumnName].DataPropertyName = this.genericManagementData.GetGenericElementMgmt.ElementKeyNameColumn.ColumnName;
            //////this.GenericMgmtDataGridView.Columns[this.genericManagementData.GetGenericElementMgmt.DescriptionColumn.ColumnName].DataPropertyName = this.genericManagementData.GetGenericElementMgmt.DescriptionColumn.ColumnName;
            //////this.GenericMgmtDataGridView.Columns[this.genericManagementData.GetGenericElementMgmt.ElementKeyIDValueColumn.ColumnName].DataPropertyName = this.genericManagementData.GetGenericElementMgmt.ElementKeyIDValueColumn.ColumnName;

            if (this.genericMgmtgridRowCount > 0)
            {
                ////if the no of visiable rows(grid) is greater than the actual rows from the Datatable ---
                //// --- then a temp datatable with empty rows are merged with GetGenericElementMgmt datatable                    
                if (this.GenericMgmtDataGridView.NumRowsVisible > this.genericMgmtgridRowCount)
                {
                    emptyRows = this.GenericMgmtDataGridView.NumRowsVisible - this.genericMgmtgridRowCount;

                    for (int i = 0; i < emptyRows; i++)
                    {
                        this.genericManagementData.GetGenericElementMgmt.AddGetGenericElementMgmtRow(this.genericManagementData.GetGenericElementMgmt.NewGetGenericElementMgmtRow());                        
                    }                       
                }
                else
                {
                    this.genericManagementData.GetGenericElementMgmt.AddGetGenericElementMgmtRow(this.genericManagementData.GetGenericElementMgmt.NewGetGenericElementMgmtRow());                        
                }

                this.GenericMgmtAcceptButton.Enabled = true;
                this.GenericMgmtDataGridView.DataSource = this.genericManagementData.GetGenericElementMgmt;
                this.GenericMgmtDataGridView.Rows[0].Selected = true;
            }
            else
            {
                for (int i = 0; i < this.GenericMgmtDataGridView.NumRowsVisible; i++)
                {
                    this.genericManagementData.GetGenericElementMgmt.AddGetGenericElementMgmtRow(this.genericManagementData.GetGenericElementMgmt.NewGetGenericElementMgmtRow());                        
                }

                this.GenericMgmtAcceptButton.Enabled = false;                    
                this.GenericMgmtDataGridView.DataSource = this.genericManagementData.GetGenericElementMgmt;
                this.GenericMgmtDataGridView.Rows[0].Selected = false;
            }

            this.GenericMgmtDataGridView.Enabled = true;
            this.GenericMgmtDataGridView.AutoGenerateColumns = false;               

            ////to enable or disable the vertical scroll bar
            if (this.genericManagementData.GetGenericElementMgmt.Rows.Count > this.GenericMgmtDataGridView.NumRowsVisible)
            {
                this.GenericMgmtVerticalScroll.Visible = false;
            }
            else
            {
                this.GenericMgmtVerticalScroll.Visible = true;
            }

            ////To display the no of display rows in the grid
            this.RecordCountLabel.Text = this.genericMgmtgridRowCount + SharedFunctions.GetResourceString("9101MasterNameSearch");          
        }      

        /// <summary>
        /// Selects the Element Key Id from the Generic Element Mgmt Grid.
        /// </summary>
        private void SelectElementKeyId()
        {
            if (this.genericMgmtgridRowCount >= 0)
            {
                this.GetElementKeyId();
            }
        }

        /// <summary>
        /// Gets the Element Key id.
        /// </summary>
        private void GetElementKeyId()
        {
            int rowId = 0;           
            rowId = this.currentRowIndex; 
          
            //// if (this.GenericMgmtDataGridView.Rows.Count > 0)
            if (this.genericMgmtgridRowCount >= 0 && rowId >= 0)
            {
                if (!string.IsNullOrEmpty(this.GenericMgmtDataGridView.Rows[rowId].Cells[this.genericManagementData.GetGenericElementMgmt.ElementKeyNameColumn.ColumnName].Value.ToString().Trim()))
                {
                    this.elementKeyId = this.GenericMgmtDataGridView.Rows[rowId].Cells[this.genericManagementData.GetGenericElementMgmt.ElementKeyNameColumn.ColumnName].Value.ToString().Trim();

                    this.commandResult = this.elementKeyId;
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
        /// To save the Generic Elements
        /// </summary>
        private void SaveGenericElement()
        {            
            int saveConfirm;
            if (this.GenericMgmtSaveButton.Enabled)
            {                    
                if (this.ValidateKeyItems())
                {
                    string elementItems = string.Empty;
                    this.GenericMgmtDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    this.genericManagementData.GetGenericElementMgmt.AcceptChanges();
                    elementItems = TerraScanCommon.GetXmlString(this.genericManagementData.GetGenericElementMgmt);
                    saveConfirm = this.form1503Control.WorkItem.F1503_SaveGenericElementMgmt(elementItems, this.formName, TerraScanCommon.UserId);
                    
                    if (saveConfirm == -99)
                    {
                        MessageBox.Show(string.Concat(this.formName + SharedFunctions.GetResourceString("F1503KeyIdDuplicateValidation1") + this.formName + SharedFunctions.GetResourceString("F1503KeyIdDuplicateValidation2")), ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.saveStatus = false;
                        this.DialogResult = DialogResult.None;     
                    }
                    else if (saveConfirm == 0)
                    {
                        ////to reload the entire form afther save function.
                        this.LoadGenericElementMgmtForm();
                        this.EnableSelectButtons(true);
                        ////TO DISABLE THE SEARCH BUTTON
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
        private bool ValidateKeyItems()
        {
            try
            {
                bool validationResult = true;
                this.GenericMgmtDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                this.genericManagementData.GetGenericElementMgmt.AcceptChanges();

                for (int i = 0; i < this.GenericMgmtDataGridView.Rows.Count; i++)
                {
                    if (validationResult)
                    {
                        if (((string.IsNullOrEmpty(this.GenericMgmtDataGridView.Rows[i].Cells[0].Value.ToString().Trim())) && (!string.IsNullOrEmpty(this.GenericMgmtDataGridView.Rows[i].Cells[1].Value.ToString().Trim()))))
                        {
                            validationResult = false;
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
                this.GenericMgmtDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                this.genericManagementData.GetGenericElementMgmt.AcceptChanges();
                if (this.genericMgmtgridRowCount > 0)
                {
                    for (int i = 0; i < this.genericMgmtgridRowCount; i++)
                    {
                        if (emptyRowAvailable)
                        {
                            if (((string.IsNullOrEmpty(this.GenericMgmtDataGridView.Rows[i].Cells[0].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.GenericMgmtDataGridView.Rows[i].Cells[1].Value.ToString().Trim()))))
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
        /// To verfiy empty rows available
        /// </summary>
        private void CheckEmptyRowsAvaliable()
        {
            if (this.ValidateEmptyRows())
            {
                this.SaveGenericElement();                
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.saveStatus = false;
                this.DialogResult = DialogResult.None;
            }
        }

        /// <summary>
        /// To show message box to verfiy condition to save
        /// </summary>
        private void CloseButtonSaveMessage()
        {
            if (this.GenericMgmtSaveButton.Enabled)
            {
                switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.Text + " ?", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        {                           
                            ////this.SaveGenericElement();
                            this.CheckEmptyRowsAvaliable();
                            ////when save is made this form will close
                            if (this.saveStatus)
                            {
                                this.Close();
                            }                           

                            break;
                        }

                    case DialogResult.No:
                        {
                            this.DialogResult = DialogResult.Cancel;
                            this.GenericMgmtSaveButton.Enabled = false;
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
        /// Handles the Load event of the F1503 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1503_Load(object sender, EventArgs e)
        {
            try
            {
                ////SaveToolStripMenuItem.Click += new EventHandler(this.GenericMgmtSaveButton_Click);
                this.CancelButton = this.GenericMgmtCloseButton;
                this.AcceptButton = this.SearchButton;
                this.LoadGenericElementMgmtForm();
            }
            catch (SoapException ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }          

        /// <summary>
        /// Handles the Click event of the GenericMgmtCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GenericMgmtCancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                string genericCancelKeyId;
                string genericDescription;

                this.NameTextBox.Focus();
                this.EnableSelectButtons(true);
                ////this.LoadGenericElementMgmtForm(); 

                ////to enable are disable the search button
                if ((!string.IsNullOrEmpty(this.searchKeyId)) || (!string.IsNullOrEmpty(this.searchDescription)))
                {
                    this.SearchButton.Enabled = true;
                }
                else
                {
                    this.SearchButton.Enabled = false;
                }

                ////to load with current record set
                if ((!string.IsNullOrEmpty(this.searchKeyId)))
                {
                    this.NameTextBox.Text = this.searchKeyId.Replace("'", "''");
                    genericCancelKeyId = this.searchKeyId;
                }
                else
                {
                    this.NameTextBox.Text = string.Empty;
                    genericCancelKeyId = string.Empty;
                }

                if ((!string.IsNullOrEmpty(this.searchDescription)))
                {
                    this.DescTextBox.Text = this.searchDescription.Replace("'", "''");
                    genericDescription = this.searchDescription;
                }
                else
                {
                    this.DescTextBox.Text = string.Empty;
                    genericDescription = this.searchDescription;
                }

                this.LoadGenericElementMgmtGrid(genericCancelKeyId, genericDescription, this.formName);
                this.NameTextBox.Focus();
                this.DialogResult = DialogResult.None;
            }
            catch (SoapException ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the GenericMgmtAcceptButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GenericMgmtAcceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.genericMgmtgridRowCount >= 0 && this.GenericMgmtAcceptButton.Enabled)
                {
                    this.SelectElementKeyId();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the GenericMgmtCloseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GenericMgmtCloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.CloseButtonSaveMessage();
            }
            catch (SoapException ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
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
                this.LoadGenericElementMgmtGrid(this.NameTextBox.Text.Trim().Replace("'", "''"), this.DescTextBox.Text.Trim().Replace("'", "''"), this.formName);
                this.enableButtonStatus = true;

                ////for cancerl method
                this.searchKeyId = this.NameTextBox.Text.Trim();
                this.searchDescription = this.DescTextBox.Text.Trim();
            }
            catch (SoapException ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
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
                this.LoadGenericElementMgmtForm();

                ////for cancel method
                this.searchKeyId = string.Empty;
                this.searchDescription = string.Empty;
            }
            catch (SoapException ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the GenericMgmtSaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GenericMgmtSaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////this.SaveGenericElement();
                this.CheckEmptyRowsAvaliable();
            }
            catch (SoapException ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the GenericMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GenericMgmtDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && this.GenericMgmtAcceptButton.Enabled)
                {
                    this.SelectElementKeyId();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the GenericMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GenericMgmtDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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
        /// Handles the KeyDown event of the GenericMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void GenericMgmtDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (e.KeyValue == 46)
                {
                    if (!this.GenericMgmtSaveButton.Enabled)
                    {
                        string elementKeyValue;
                        elementKeyValue = this.GenericMgmtDataGridView.Rows[GenericMgmtDataGridView.CurrentRowIndex].Cells[0].Value.ToString().Trim();
                        this.form1503Control.WorkItem.F1503_DeleteGenericElementMgmt(elementKeyValue, this.formName, TerraScanCommon.UserId);
                        this.NameTextBox.Focus();
                        ////to reload the entire form afther delete function.
                        this.LoadGenericElementMgmtForm();
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
        /// Handles the CellBeginEdit event of the GenericMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void GenericMgmtDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.GenericMgmtDataGridView.Columns[this.genericManagementData.GetGenericElementMgmt.ElementKeyNameColumn.ColumnName].Index || e.ColumnIndex == this.GenericMgmtDataGridView.Columns[this.genericManagementData.GetGenericElementMgmt.DescriptionColumn.ColumnName].Index)
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
        /// Handles the RowHeaderMouseClick event of the GenericMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void GenericMgmtDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.GenericMgmtDataGridView.CurrentCell != null)
                {
                    this.GenericMgmtDataGridView.CurrentCell.ReadOnly = true;
                    this.GenericMgmtDataGridView.Rows[e.RowIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }       

        /// <summary>
        /// Handles the ColumnHeaderMouseClick event of the GenericMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void GenericMgmtDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
                {
                    this.GenericMgmtDataGridView.CurrentCell.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the GenericMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GenericMgmtDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == 0)
                {
                    this.GenericMgmtDataGridView["ElementKeyName", e.RowIndex].ReadOnly = false;
                    this.GenericMgmtDataGridView["Description", e.RowIndex].ReadOnly = false;
                    this.GenericMgmtDataGridView.Rows[e.RowIndex].Selected = false;
                }

                bool hasValues = false;
                if (e.RowIndex >= 1)
                {
                    if ((string.IsNullOrEmpty(this.GenericMgmtDataGridView["ElementKeyName", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.GenericMgmtDataGridView["Description", (e.RowIndex - 1)].Value.ToString().Trim())))
                    {
                        if (e.RowIndex + 1 < GenericMgmtDataGridView.RowCount)
                        {
                            for (int i = e.RowIndex; i < GenericMgmtDataGridView.RowCount; i++)
                            {
                                if (!string.IsNullOrEmpty(this.GenericMgmtDataGridView.Rows[i].Cells[0].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.GenericMgmtDataGridView.Rows[i].Cells[1].Value.ToString().Trim()))
                                {
                                    hasValues = true;
                                    break;
                                }
                            }

                            if (hasValues)
                            {
                                this.GenericMgmtDataGridView["ElementKeyName", e.RowIndex].ReadOnly = false;
                                this.GenericMgmtDataGridView["Description", e.RowIndex].ReadOnly = false;
                                this.GenericMgmtDataGridView.Rows[e.RowIndex].Selected = false;
                            }
                            else
                            {
                                if ((string.IsNullOrEmpty(this.GenericMgmtDataGridView["ElementKeyName", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.GenericMgmtDataGridView["Description", e.RowIndex].Value.ToString().Trim())))
                                {
                                    this.GenericMgmtDataGridView["ElementKeyName", e.RowIndex].ReadOnly = true;
                                    this.GenericMgmtDataGridView["Description", e.RowIndex].ReadOnly = true;
                                }
                                else
                                {
                                    this.GenericMgmtDataGridView["ElementKeyName", e.RowIndex].ReadOnly = false;
                                    this.GenericMgmtDataGridView["Description", e.RowIndex].ReadOnly = false;
                                    this.GenericMgmtDataGridView.Rows[e.RowIndex].Selected = false;
                                }
                            }
                        }
                        else
                        {
                            this.GenericMgmtDataGridView["ElementKeyName", e.RowIndex].ReadOnly = true;
                            this.GenericMgmtDataGridView["Description", e.RowIndex].ReadOnly = true;
                        }                       
                    }
                    else
                    {
                        this.GenericMgmtDataGridView["ElementKeyName", e.RowIndex].ReadOnly = false;
                        this.GenericMgmtDataGridView["Description", e.RowIndex].ReadOnly = false;
                        this.GenericMgmtDataGridView.Rows[e.RowIndex].Selected = false;
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
        /// Handles the CellEndEdit event of the GenericMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GenericMgmtDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((((e.RowIndex) +1) == this.GenericMgmtDataGridView.Rows.Count) && (e.ColumnIndex == 0))
                {
                    if ((!string.IsNullOrEmpty(this.GenericMgmtDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Trim())))
                    {
                        this.genericManagementData.GetGenericElementMgmt.Rows.InsertAt(this.genericManagementData.GetGenericElementMgmt.NewGetGenericElementMgmtRow(), this.GenericMgmtDataGridView.Rows.Count);
                        this.GenericMgmtVerticalScroll.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the GenericMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GenericMgmtDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool hasValues = false;
                if (e.RowIndex >= 1)
                {
                    if ((string.IsNullOrEmpty(this.GenericMgmtDataGridView["ElementKeyName", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.GenericMgmtDataGridView["Description", (e.RowIndex - 1)].Value.ToString().Trim())))
                    {
                        if (e.RowIndex + 1 < GenericMgmtDataGridView.RowCount)
                        {
                            for (int i = e.RowIndex; i < GenericMgmtDataGridView.RowCount; i++)
                            {
                                if (!string.IsNullOrEmpty(this.GenericMgmtDataGridView.Rows[i].Cells[0].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.GenericMgmtDataGridView.Rows[i].Cells[1].Value.ToString().Trim()))
                                {
                                    hasValues = true;
                                    break;
                                }
                            }

                            if (hasValues)
                            {
                                this.GenericMgmtDataGridView["ElementKeyName", e.RowIndex].ReadOnly = false;
                                this.GenericMgmtDataGridView["Description", e.RowIndex].ReadOnly = false;
                                this.GenericMgmtDataGridView.Rows[e.RowIndex].Selected = false;
                            }
                            else
                            {
                                if ((string.IsNullOrEmpty(this.GenericMgmtDataGridView["ElementKeyName", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.GenericMgmtDataGridView["Description", e.RowIndex].Value.ToString().Trim())))
                                {
                                    this.GenericMgmtDataGridView["ElementKeyName", e.RowIndex].ReadOnly = true;
                                    this.GenericMgmtDataGridView["Description", e.RowIndex].ReadOnly = true;
                                }
                                else
                                {
                                    this.GenericMgmtDataGridView["ElementKeyName", e.RowIndex].ReadOnly = false;
                                    this.GenericMgmtDataGridView["Description", e.RowIndex].ReadOnly = false;
                                    this.GenericMgmtDataGridView.Rows[e.RowIndex].Selected = false;
                                }
                            }
                        }
                        else
                        {
                            this.GenericMgmtDataGridView["ElementKeyName", e.RowIndex].ReadOnly = true;
                            this.GenericMgmtDataGridView["Description", e.RowIndex].ReadOnly = true;
                        }
                    }
                    else
                    {
                        this.GenericMgmtDataGridView["ElementKeyName", e.RowIndex].ReadOnly = false;
                        this.GenericMgmtDataGridView["Description", e.RowIndex].ReadOnly = false;
                        this.GenericMgmtDataGridView.Rows[e.RowIndex].Selected = false;
                    }
                }

                if (e.RowIndex == 0)
                {
                    this.GenericMgmtDataGridView["ElementKeyName", e.RowIndex].ReadOnly = false;
                    this.GenericMgmtDataGridView["Description", e.RowIndex].ReadOnly = false;
                    this.GenericMgmtDataGridView.Rows[e.RowIndex].Selected = false;
                }

                this.currentRowIndex = e.RowIndex;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }            

        /// <summary>
        /// Handles the RowHeaderMouseDoubleClick event of the GenericMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void GenericMgmtDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.GenericMgmtAcceptButton.Enabled)
                {
                    this.SelectElementKeyId();
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
                if ((!string.IsNullOrEmpty(this.NameTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.DescTextBox.Text.Trim())))
                {
                    if (this.GenericMgmtSaveButton.Enabled)
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
        /// Handles the FormClosing event of the F1503 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void F1503_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!e.CloseReason.Equals(CloseReason.ApplicationExitCall))
                {
                    if (e.CloseReason.Equals(CloseReason.UserClosing))
                    {
                        if (this.GenericMgmtSaveButton.Enabled)
                        {
                            switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.Text + " ?", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                            {
                                case DialogResult.Yes:
                                    {
                                        try
                                        {
                                            ////this.SaveGenericElement();
                                            this.CheckEmptyRowsAvaliable();
                                            ////when save is made this form will close
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
            catch (SoapException ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the GenericMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void GenericMgmtDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
                this.GenericMgmtDataGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
            }           
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
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
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
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
                this.NameTextBox.Focus();
                ////this.SaveGenericElement();
                this.CheckEmptyRowsAvaliable();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Events
    }
}