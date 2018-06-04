//--------------------------------------------------------------------------------------------
// <copyright file="F2331.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the MAD Import Template Selection.
// </summary>
//----------------------------------------------------------------------------------------------
// History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//20160712          Priyadharshini        Created
//*********************************************************************************/

namespace D23310
{
    using System;
    using System.Data;
    using System.Windows.Forms;
    using TerraScan.Common;
    using System.Web.Services.Protocols;
    using TerraScan.Utilities;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;

    /// <summary>
    /// F2331 Form class
    /// </summary>
    public partial class F2331 : Form
    {
        #region Private Variables

        /// <summary>
        /// DataSet Holds the MADImportTemplate Details
        /// </summary>
        private ListMADimportTemplateData ListMADimportTemplateDataSet;

        /// <summary>
        /// Created Boolean to Find emptyRecord
        /// </summary>        
        private bool emptyRecord;

        /// <summary>
        /// Template Id
        /// </summary>
        private int MADImportTemplateId;

        /// <summary>
        /// Controller F2331Controller
        /// </summary>
        private F2331Controller form2331Control;

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MADImportTemplateDetailsDataSet class.
        /// </summary>
        public F2331()
        {
            this.InitializeComponent();
            this.MADImportTemplateSelectionPanel.Focus();
            this.ActiveControl = this.TemplateNameTextBox;
            this.ListMADimportTemplateDataSet = new ListMADimportTemplateData();
            this.MADImportTemplateId = -1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Property MADImportTemplateID 
        /// </summary>
        public int MADImportTemplateID
        {
            set
            {
                this.MADImportTemplateId = value;
            }

            get
            {
                return this.MADImportTemplateId;
            }
        }

        /// <summary>
        /// Created Property for F2331Control
        /// </summary>
        [CreateNew]
        public F2331Controller F2331Control
        {
            get { return this.form2331Control as F2331Controller; }
            set { this.form2331Control = value; }
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

        #endregion

        #region EventPublication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion EventPublication

        #region Private Methods

        /// <summary>
        /// Disables the Vertical scroll bar.
        /// </summary>
        private void DisableVScrollBar()
        {
            if (this.ListMADimportTemplateDataSet != null)
            {
                if (this.ListMADimportTemplateDataSet.Tables[0].Rows.Count > 5)
                {
                    this.ImportMADSelectionVScrollBar.Enabled = true;
                    this.ImportMADSelectionVScrollBar.Visible = false;
                    this.ScrollPanel.SendToBack();
                }
                else
                {
                    this.ImportMADSelectionVScrollBar.Enabled = false;
                    this.ImportMADSelectionVScrollBar.Visible = true;
                    this.ScrollPanel.BringToFront();
                    this.ImportMADSelectionVScrollBar.BringToFront();
                }
            }
        }

        /// <summary>
        /// Used to create a empty row in a Datagrid.
        /// </summary>
        /// <param name="sourceDataTable">The source data table.</param>
        /// <param name="maxRowCount">The max row count.</param>
        /// <returns>returns datatable</returns>
        private static DataTable CreateEmptyRows(DataTable sourceDataTable, int maxRowCount)
        {
            int defaultRowsCount = 0;
            DataRow tempRow;

            if (sourceDataTable.Rows.Count < maxRowCount)
            {
                defaultRowsCount = maxRowCount - sourceDataTable.Rows.Count;

                for (int i = 0; i < defaultRowsCount; i++)
                {
                    tempRow = sourceDataTable.NewRow();
                    for (int j = 0; j < sourceDataTable.Columns.Count; j++)
                    {
                        if (sourceDataTable.Columns[j].DataType.ToString() == "System.Int32")
                        {
                            tempRow[j] = DBNull.Value;
                        }
                        else if (sourceDataTable.Columns[j].DataType.ToString() == "System.Int16")
                        {
                            tempRow[j] = DBNull.Value;
                        }
                        else if (sourceDataTable.Columns[j].DataType.ToString() == "System.Boolean")
                        {
                            tempRow[j] = DBNull.Value;
                        }
                        else if (sourceDataTable.Columns[j].DataType.ToString() == "System.Byte")
                        {
                            tempRow[j] = DBNull.Value;
                        }
                        else
                        {
                            tempRow[j] = string.Empty;
                        }
                    }

                    sourceDataTable.Rows.Add(tempRow);
                }
            }

            return sourceDataTable;
        }

        /// <summary>
        /// This Method used to  Allign The Header width , Column width , And Font Size for 
        /// MADImportTemplateSelectionGridView
        /// </summary>
        private void CustomizeDataGrid()
        {
            //// TODO All styles Should be take from config file
            this.MADImportTemplateSelectionGridView.AllowUserToResizeColumns = false;
            this.MADImportTemplateSelectionGridView.AutoGenerateColumns = false;
            this.MADImportTemplateSelectionGridView.AllowUserToResizeRows = false;
            DataGridViewColumnCollection columns = this.MADImportTemplateSelectionGridView.Columns;
            columns["TemplateId"].DataPropertyName = "TemplateId";
            columns["TemplateName"].DataPropertyName = "TemplateName";
            columns["Description"].DataPropertyName = "Description";
            columns["TypeID"].DataPropertyName = "TypeID";
            columns["FileType"].DataPropertyName = "FileType";
            columns["TemplateName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            columns["FileType"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        /// <summary>
        /// Enables the search button.
        /// </summary>
        private void EnableSearchButton()
        {
            if (!string.IsNullOrEmpty(this.TemplateNameTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.DecriptionTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.FileTypeTextBox.Text.Trim()))
            {
                this.SearchButton.Enabled = true;
                this.ClearButton.Enabled = true;
            }
            else
            {
                //this.SearchButton.Enabled = false;

                if (this.ListMADimportTemplateDataSet.Tables[0].Rows.Count <= 0)
                {
                    this.AcceptMasterNameButton.Enabled = false;
                    this.ClearButton.Enabled = false;
                }
                else
                {
                    this.AcceptMasterNameButton.Enabled = true;
                    this.ClearButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Clears the fields.
        /// </summary>
        private void ClearFields()
        {
            try
            {
                this.FileTypeTextBox.Text = string.Empty;
                this.DecriptionTextBox.Text = string.Empty;
                this.TemplateNameTextBox.Text = string.Empty;
                this.RecordCountLabel.Text = string.Empty;
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Disables the buttons.
        /// </summary>
        private void DisableButtons()
        {
            //this.SearchButton.Enabled = false;
            this.AcceptMasterNameButton.Enabled = false;
            //this.ClearButton.Enabled = false;
        }

        /// <summary>
        /// Method to Populate the Current Record and Close the Form
        /// </summary>
        private void PopulateRecord()
        {
            try
            {
                int rowIndex = -1;
                if (this.MADImportTemplateSelectionGridView.Rows.Count > 0)
                {
                    rowIndex = this.MADImportTemplateSelectionGridView.CurrentRow.Index;
                    this.MADImportTemplateSelectionGridView.CurrentRow.Selected = true;
                }
                if (rowIndex >= 0)
                {
                    if (!string.IsNullOrEmpty(this.MADImportTemplateSelectionGridView.Rows[rowIndex].Cells["TemplateId"].Value.ToString()))
                    {
                        this.MADImportTemplateID = Convert.ToInt32(this.MADImportTemplateSelectionGridView.Rows[rowIndex].Cells["TemplateId"].Value.ToString());
                        this.commandResult = this.MADImportTemplateID.ToString();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }

        #endregion

        #region Form Events

        /// <summary>
        /// Handles the Close Button Click Even
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            this.Close();
        }

        /// <summary>
        /// Determines whether  DataTable is Exist or not in  [the specified CMNT data set].
        /// 
        /// </summary>
        /// <param name="tempDataSet">The MITD data set.</param>
        /// <returns>
        /// 	<c>true</c> if [is data table] [the specified MITD data set]; otherwise, <c>false</c>.
        /// </returns>
        private static bool CheckValidDataSet(DataSet tempDataSet)
        {
            if (tempDataSet.Tables.Count > 0 && tempDataSet != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Handles the Key Down Event For the GridView
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">KeyEventArgs</param>
        private void MADImportTemplateSelectionGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    this.PopulateRecord();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }

     
        /// <summary>
        /// Edits the text.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EditText(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(this.TemplateNameTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.DecriptionTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.FileTypeTextBox.Text.Trim())))
                {
                    this.EnableSearchButton();
                }
                else
                {
                    if (this.ListMADimportTemplateDataSet != null)
                    {
                        if (this.ListMADimportTemplateDataSet.ListMADImportData.Rows.Count > 0)
                        {
                            //this.SearchButton.Enabled = false;
                        }
                        else
                        {
                            this.DisableButtons();
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
        /// SearchButton_Click
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.SearchButton.Enabled)
                {
                    int recordCount = 0;
                    //Removed the .Replace("'","''") for Database Execution retrieves nothing
                    string templateName = this.TemplateNameTextBox.Text.Trim();
                    string description = this.DecriptionTextBox.Text.Trim();
                    string fileType = this.FileTypeTextBox.Text.Trim();


                    //if (string.IsNullOrEmpty(templateName) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(fileType))
                    //{
                        this.ListMADimportTemplateDataSet = this.form2331Control.WorkItem.GetMADImportTemplateDetails(templateName, description, fileType);

                        if (this.ListMADimportTemplateDataSet.ListMADImportData.Rows.Count > 0)
                        {
                            recordCount = this.ListMADimportTemplateDataSet.ListMADImportData.Rows.Count;
                            this.MADImportTemplateSelectionGridView.Enabled = true;
                            this.MADImportTemplateSelectionGridView.DataSource = this.ListMADimportTemplateDataSet.ListMADImportData;
                            this.MADImportTemplateSelectionGridView.Focus();
                            this.MADImportTemplateSelectionGridView.Rows[0].Selected = true;
                            this.AcceptMasterNameButton.Enabled = true;
                            this.ClearButton.Enabled = true;
                            this.emptyRecord = false;
                        }
                        else
                        {
                            this.MADImportTemplateSelectionGridView.DataSource = this.ListMADimportTemplateDataSet.ListMADImportData;
                            this.MADImportTemplateSelectionGridView.Rows[0].Selected = false;
                            this.MADImportTemplateSelectionGridView.Enabled = false;
                            this.emptyRecord = true;
                            this.AcceptMasterNameButton.Enabled = false;
                            this.ClearButton.Enabled = true;
                        }

                        this.RecordCountLabel.Text = recordCount + SharedFunctions.GetResourceString("9101MasterNameSearch");
                        this.DisableVScrollBar();
                    //}
                    //else
                    //{
                    //    this.RecordCountLabel.Text = 0 + SharedFunctions.GetResourceString("9101MasterNameSearch");
                    //}
                }
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

        /// <summary>
        /// MasterNameCancelButton_Click
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MasterNameCancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.MasterNameCancelButton.Enabled)
                {
                    this.DialogResult = DialogResult.No;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ClearButton_Click
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.MADImportTemplateSelectionGridView.Enabled = false;
                this.MADImportTemplateSelectionGridView.Rows[0].Selected = false;
                this.ClearFields();
                this.DisableButtons();
                if (this.ListMADimportTemplateDataSet != null)
                {
                    this.ListMADimportTemplateDataSet.ListMADImportData.Clear();
                    this.MADImportTemplateSelectionGridView.DataSource = null;
                    this.ClearButton.Enabled = false;
                }
                this.DisableVScrollBar();
                this.TemplateNameTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        /// <summary>
        /// MADImportTemplateSelectionGridView_CellDoubleClick
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MADImportTemplateSelectionGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (!string.IsNullOrEmpty(this.MADImportTemplateSelectionGridView.Rows[e.RowIndex].Cells["TemplateId"].Value.ToString()))
                    {
                        this.MADImportTemplateID = Convert.ToInt32(this.MADImportTemplateSelectionGridView.Rows[e.RowIndex].Cells["TemplateId"].Value.ToString());
                        this.commandResult = this.MADImportTemplateID.ToString();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }
        #endregion

        /// <summary>
        /// AcceptMasterNameButton_Click
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AcceptMasterNameButton_Click(object sender, EventArgs e)
        {
            this.PopulateRecord();
        }

        /// <summary>
        /// MADImportTemplateSelectionGridView_KeyPress
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MADImportTemplateSelectionGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (int)Keys.Enter)
                {
                    e.Handled = true;
                    this.PopulateRecord();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }

        }

        /// <summary>
        /// Handles the Load Event for the Form
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void MADImportTemplateSelect_Load(object sender, EventArgs e)
        {
            try
            {
                this.emptyRecord = true;
                this.CustomizeDataGrid();
                this.MADImportTemplateSelectionGridView.DataSource = null;
                this.MADImportTemplateSelectionGridView.Enabled = false;
                this.MADImportTemplateSelectionGridView.Rows[0].Selected = false;
                this.ClearFields();
                this.DisableButtons();
                this.DisableVScrollBar();
                this.TemplateNameTextBox.Focus();
                this.ClearButton.Enabled = false;
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
        /// Handles the MADImportTemplateFormLinkLabel_LinkClicked Event for the Form
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void MADImportTemplateFormLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo = TerraScanCommon.GetFormInfo(23300);
                formInfo.optionalParameters = new object[] { null };
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                this.Close();
            }
            catch (Exception)
            {
                ErrorEngine.ShowForm((int)TerraScanCommon.ErrorEngineType.Six);
            }
            finally
            {
                this.MADImportTemplateSelectionGridView.Focus();
            }
        }

        
    }
}