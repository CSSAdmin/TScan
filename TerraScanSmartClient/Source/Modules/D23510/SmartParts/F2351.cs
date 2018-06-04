//--------------------------------------------------------------------------------------------
// <copyright file="F2321.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Permit Import Template Selection.
// </summary>
//----------------------------------------------------------------------------------------------
// History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//20160601          Priyadharshini        
//*********************************************************************************/

namespace D23510
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
    /// F2321 Form class
    /// </summary>
    public partial class F2351 : Form
    {
        #region Private Variables

        /// <summary>
        /// DataSet Holds the PermitImportTemplate Details
        /// </summary>
        private ListSnapshotImportTemplateData snapshotImportTemplateDetailsDataSet;

        /// <summary>
        /// Created Boolean to Find emptyRecord
        /// </summary>        
        private bool emptyRecord;

        /// <summary>
        /// Template Id
        /// </summary>
        private int snapshotImportTemplateId;

        /// <summary>
        /// Controller F1015Controller
        /// </summary>
        private F2351Controller form2351Control;

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the snapshotImportTemplateDetailsDataSet class.
        /// </summary>
        public F2351()
        {
            this.InitializeComponent();
            this.SnapshotImportTemplateSelectionPanel.Focus();
            this.ActiveControl = this.TemplateNameTextBox;
            this.snapshotImportTemplateDetailsDataSet = new ListSnapshotImportTemplateData();
            this.snapshotImportTemplateId = -1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Property PermitImportTemplateId 
        /// </summary>
        public int SnapshotImportTemplateId
        {
            set
            {
                this.snapshotImportTemplateId = value;
            }

            get
            {
                return this.snapshotImportTemplateId;
            }
        }

        /// <summary>
        /// Created Property for F2321Control
        /// </summary>
        [CreateNew]
        public F2351Controller F2351Control
        {
            get { return this.form2351Control as F2351Controller; }
            set { this.form2351Control = value; }
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
        /// Disables the V scroll bar.
        /// </summary>
        private void DisableVScrollBar()
        {
            if (this.snapshotImportTemplateDetailsDataSet != null)
            {
                if (this.snapshotImportTemplateDetailsDataSet.Tables[0].Rows.Count > 5)
                {
                    this.ImportSnapshotSelectionVScrollBar.Enabled = true;
                    this.ImportSnapshotSelectionVScrollBar.Visible = false;
                    this.ScrollPanel.SendToBack();
                }
                else
                {
                    this.ImportSnapshotSelectionVScrollBar.Enabled = false;
                    this.ImportSnapshotSelectionVScrollBar.Visible = true;
                    this.ScrollPanel.BringToFront();
                    this.ImportSnapshotSelectionVScrollBar.BringToFront();
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
        /// PermitImportTemplateSelectionGridView
        /// </summary>
        private void CustomizeDataGrid()
        {
            //// TODO All styles Should be take from config file
            this.SnapshotImportTemplateSelectionGridView.AllowUserToResizeColumns = false;
            this.SnapshotImportTemplateSelectionGridView.AutoGenerateColumns = false;
            this.SnapshotImportTemplateSelectionGridView.AllowUserToResizeRows = false;
            DataGridViewColumnCollection columns = this.SnapshotImportTemplateSelectionGridView.Columns;
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
                if (this.snapshotImportTemplateDetailsDataSet.Tables[0].Rows.Count <= 0)
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
                if (this.SnapshotImportTemplateSelectionGridView.Rows.Count > 0)
                {
                    rowIndex = this.SnapshotImportTemplateSelectionGridView.CurrentRow.Index;
                    this.SnapshotImportTemplateSelectionGridView.CurrentRow.Selected = true;
                }
                if (rowIndex >= 0)
                {
                    if (!string.IsNullOrEmpty(this.SnapshotImportTemplateSelectionGridView.Rows[rowIndex].Cells["TemplateId"].Value.ToString()))
                    {
                        this.SnapshotImportTemplateId = Convert.ToInt32(this.SnapshotImportTemplateSelectionGridView.Rows[rowIndex].Cells["TemplateId"].Value.ToString());
                        this.commandResult = this.SnapshotImportTemplateId.ToString();
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
        /// Handles the Load Event for the Form
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void SnapshotImportTemplateSelect_Load(object sender, EventArgs e)
        {
            try
            {
                this.emptyRecord = true;
                this.CustomizeDataGrid();
                this.SnapshotImportTemplateSelectionGridView.DataSource = null;
                this.SnapshotImportTemplateSelectionGridView.Enabled = false;
                this.SnapshotImportTemplateSelectionGridView.Rows[0].Selected = false;
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
        /// Handles the Key Down Event For the GridView
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">KeyEventArgs</param>
        private void SnapshotImportTemplateSelectionGridView_KeyDown(object sender, KeyEventArgs e)
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
        /// Handles the LinkClicked Event For the GridView
        /// </summary>
        /// <param name="sender">sender as object</param>
        /// <param name="e">LinkLabelLinkClickedEventArgs</param>
        private void SnapshotImportTemplateFormLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo = TerraScanCommon.GetFormInfo(23500);
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
                this.SnapshotImportTemplateSelectionGridView.Focus();
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
                    if (this.snapshotImportTemplateDetailsDataSet != null)
                    {
                        if (this.snapshotImportTemplateDetailsDataSet.ListSnapshotImportData.Rows.Count > 0)
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
                    this.snapshotImportTemplateDetailsDataSet = this.form2351Control.WorkItem.GetSnapshotImportTemplateDetails(templateName, description, fileType);

                        if (this.snapshotImportTemplateDetailsDataSet.ListSnapshotImportData.Rows.Count > 0)
                        {
                            recordCount = this.snapshotImportTemplateDetailsDataSet.ListSnapshotImportData.Rows.Count;
                            this.SnapshotImportTemplateSelectionGridView.Enabled = true;
                            this.SnapshotImportTemplateSelectionGridView.DataSource = this.snapshotImportTemplateDetailsDataSet.ListSnapshotImportData;
                            this.SnapshotImportTemplateSelectionGridView.Focus();
                            this.SnapshotImportTemplateSelectionGridView.Rows[0].Selected = true;
                            this.AcceptMasterNameButton.Enabled = true;
                            this.ClearButton.Enabled = true;
                            this.emptyRecord = false;
                        }
                        else
                        {
                            this.SnapshotImportTemplateSelectionGridView.DataSource = this.snapshotImportTemplateDetailsDataSet.ListSnapshotImportData;
                            this.SnapshotImportTemplateSelectionGridView.Rows[0].Selected = false;
                            this.SnapshotImportTemplateSelectionGridView.Enabled = false;
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
                this.SnapshotImportTemplateSelectionGridView.Enabled = false;
                this.SnapshotImportTemplateSelectionGridView.Rows[0].Selected = false;
                this.ClearFields();
                this.DisableButtons();
                if (this.snapshotImportTemplateDetailsDataSet != null)
                {
                    this.snapshotImportTemplateDetailsDataSet.ListSnapshotImportData.Clear();
                    this.SnapshotImportTemplateSelectionGridView.DataSource = null;
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
        /// PermitImportTemplateSelectionGridView_CellDoubleClick
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SnapshotImportTemplateSelectionGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (!string.IsNullOrEmpty(this.SnapshotImportTemplateSelectionGridView.Rows[e.RowIndex].Cells["TemplateId"].Value.ToString()))
                    {
                        this.snapshotImportTemplateId = Convert.ToInt32(this.SnapshotImportTemplateSelectionGridView.Rows[e.RowIndex].Cells["TemplateId"].Value.ToString());
                        this.commandResult = this.snapshotImportTemplateId.ToString();
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
        /// PermitImportTemplateSelectionGridView_KeyPress
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SnapshotImportTemplateSelectionGridView_KeyPress(object sender, KeyPressEventArgs e)
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
        /// AcceptMasterNameButton_Click
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AcceptMasterNameButton_Click(object sender, EventArgs e)
        {
            this.PopulateRecord();
        }
    }
}