
namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.SmartParts;
    using System.Collections;
    using System.IO;
    using TerraScan.Utilities;
    using System.Configuration;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using System.Web.Services.Protocols;
    using TerraScan.Helper;
    using TerraScan.UI.Controls;

    public partial class F1406 : Form
    {

        private F1406Controller form1406Control;
        private int centralItemIds;
        /// <summary>
        /// centralSearchDatatable
        /// </summary>
        private DataTable centralSearchDatatable = new DataTable();

        /// <summary>
        /// parcelsearchdatarow
        /// </summary>
        private DataRow centralSearchdatarow;
        private string openedForm = string.Empty;


        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        F1406CentralAssessedSearchData GridSearchDataSet = new F1406CentralAssessedSearchData();

        F1406CentralAssessedSearchData.f1406_pcget_PropertyClassDataTable PropertyComboTable = new F1406CentralAssessedSearchData.f1406_pcget_PropertyClassDataTable();

        private F1404ScheduleSelectionData scheduleSelectionData = new F1404ScheduleSelectionData();

        private string centralSearchXML = string.Empty;

        public F1406()
        {
            InitializeComponent();
        }

        public F1406(string callingForm)
        {
            InitializeComponent();
            this.openedForm = callingForm;

        }
        private void F1406_Load(object sender, EventArgs e)
        {
            this.LoadComboBox();
            this.CustomizeGrid();
            this.FormLoad();
            this.ParcelSearchDataGridView.DataSource = null;
            this.ParcelSearchDataGridView.Enabled = false;
            this.ParcelSearchDataGridView.Rows[0].Selected = false;
            this.DisableButtons();
            this.scheduleSelectionData = this.form1406Control.WorkItem.F1404_GetScheduleType(null);
            if (this.scheduleSelectionData != null)
            {
                if (this.scheduleSelectionData.ScheduleRollYearDataTable.Rows.Count > 0)
                {
                    this.RollYearTextBox.Text = this.scheduleSelectionData.ScheduleRollYearDataTable.Rows[0][0].ToString();
                }
            }
        }

        #region Button Events

        private void ParcelAcceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ParcelAcceptButton.Enabled)
                {
                    this.GetCentralIDs();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SearchButton.Enabled)
                {
                    this.GetGridSearchData();
                }
             }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormLoad();
                this.ClearControls();
                this.DisableButtons();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ParcelCancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ParcelCancelButton.Enabled)
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
        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the F1403 controll.
        /// </summary>
        /// <value>The F1403 controll.</value>
        [CreateNew]
        public F1406Controller F1406Controll
        {
            get { return this.form1406Control as F1406Controller; }
            set { this.form1406Control = value; }
        }

        /// <summary>
        /// Gets or sets the parcel id.
        /// </summary>
        /// <value>The parcel id.</value>
        public int CentralItemIDs
        {
            get { return this.centralItemIds; }
            set { this.centralItemIds = value; }
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
        /// Forms the load.
        /// </summary>
        private void FormLoad()
        {
            //// this.DisableButtons();
            this.ParcelSearchDataGridView.DataSource = null;
            this.ParcelSearchDataGridView.Enabled = false;
            this.ParcelSearchDataGridView.Rows[0].Selected = false;
            this.ClearControls();

            if (this.ParcelSearchDataGridView.OriginalRowCount > 0)
            {
                this.ParcelAcceptButton.Enabled = true;
            }
            else
            {
                this.ParcelAcceptButton.Enabled = false;
            }

            if (this.PropertyClassComboBox != null)
            {
                //if (this.parcelType > 0)
                //{
                //    this.PropertyClassComboBox.SelectedValue = this.parcelType;
                //    this.PropertyClassComboBox.Enabled = false;
                //}
                //else
                //{
                //    this.PropertyClassComboBox.SelectedIndex = 0;
                //}
            }

            this.ParcelNumberTextBox.Focus();
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
                if (this.HasSearchValue())
                {
                    this.EnableSearchButton();
                }
                else
                {
                   // this.DisableButtons();                    
                    if (this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.Rows.Count <= 0)
                    {
                        this.DisableButtons();
                    }
                    else
                    {
                        this.SearchButton.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        /// <summary>
        /// Gets the parcel ID.
        /// </summary>
        private void GetCentralIDs()
        {
            if (this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.Rows.Count > 0)
            {
                if (this.centralSearchDatatable.Columns.Count == 0)
                {
                    this.centralSearchDatatable.Columns.Add("CentralItemID");
                }
                DataGridViewSelectedRowCollection selectedRows = this.ParcelSearchDataGridView.SelectedRows;
                if (selectedRows.Count > 0)
                {

                    foreach (DataGridViewRow dataRow in selectedRows)
                    {
                        if (!string.IsNullOrEmpty(dataRow.Cells["CentralItemID"].Value.ToString().Trim()))
                        {
                            this.centralSearchdatarow = this.centralSearchDatatable.NewRow();
                            this.centralSearchdatarow["CentralItemID"] = dataRow.Cells["CentralItemID"].Value.ToString().Trim();
                            this.centralSearchDatatable.Rows.Add(this.centralSearchdatarow);
                        }
                    }
                    //this.commandResult = TerraScanCommon.GetXmlString(this.centralSearchDatatable);
                }
                this.commandResult = TerraScanCommon.GetXmlString(this.centralSearchDatatable);
                this.DialogResult = DialogResult.OK;
                this.Close();
                
            }
        }

        private string GetSearchXml()
        {
            int rollYearSearch = 0;
            DataTable searchValueDataTable = new DataTable();
            string searchValueXml = string.Empty;
            searchValueDataTable = (DataTable)this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.Clone();

            DataRow searchRow;
            searchRow = searchValueDataTable.NewRow();

            searchRow[this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.ParcelNumberColumn.ColumnName] = this.ParcelNumberTextBox.Text.Trim();
            searchRow[this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.StatementNumberColumn.ColumnName] = this.StatementNumberTextBox.Text.Trim();
            searchRow[this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.OwnerNameColumn.ColumnName] = this.OwnerTextBox.Text.Trim();
            int.TryParse(this.RollYearTextBox.Text.Trim(), out rollYearSearch);
            searchRow[this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.RollYearColumn.ColumnName] = rollYearSearch;
            searchRow[this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.CompanyNameColumn.ColumnName] = this.CompanyNameTextBox.Text.Trim();
            searchRow[this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.CompanyNumberColumn.ColumnName] = this.CompanyNumberTextBox.Text.Trim();
            searchRow[this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.BranchLineColumn.ColumnName] = this.BranchLineTextBox.Text.Trim();
            searchRow[this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.PropertyClassColumn.ColumnName] = this.PropertyClassComboBox.SelectedValue;
            searchRow[this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.FundColumn.ColumnName] = this.SubfundTextBox.Text.Trim();

            searchValueDataTable.Rows.Add(searchRow);

            searchValueXml = TerraScanCommon.GetXmlString(searchValueDataTable);
            return searchValueXml;
        }
        /// <summary>
        /// Searches the value.
        /// </summary>
        /// <returns>bool</returns>
        private bool HasSearchValue()
        {
            if ((!string.IsNullOrEmpty(this.ParcelNumberTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.StatementNumberTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.OwnerTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.OwnerTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.CompanyNameTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.CompanyNumberTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.BranchLineTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SubfundTextBox.Text.Trim())) || ((this.PropertyClassComboBox.SelectedIndex>0)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Enables the search button.
        /// </summary>
        private void EnableSearchButton()
        {
            if (this.HasSearchValue())
            {
                this.SearchButton.Enabled = true;
                this.ClearButton.Enabled = true;
            }
            else
            {
                this.SearchButton.Enabled = false;
                if (this.ParcelSearchDataGridView.OriginalRowCount > 0)
                {
                    this.ParcelAcceptButton.Enabled = true;
                    this.ClearButton.Enabled = true;
                }
                else
                {
                    this.ParcelAcceptButton.Enabled = false;
                    this.ClearButton.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Disables the buttons.
        /// </summary>
        private void DisableButtons()
        {
            this.SearchButton.Enabled = false;
            this.ParcelAcceptButton.Enabled = false;
            this.ClearButton.Enabled = false;
        }
        private void CustomizeGrid()
        {
            this.ParcelSearchDataGridView.AutoGenerateColumns = false;
            this.ParcelSearchDataGridView.ScrollBars = ScrollBars.Both;
            this.ParcelSearchDataGridView.IsMultiSelect = true;
            this.ParcelSearchDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            //this.ParcelSearchDataGridView.AllowUserToResizeColumns = false;    
            this.ParcelSearchDataGridView.AllowUserToResizeRows = false;
            this.ParcelSearchDataGridView.StandardTab = true;


            this.ParcelSearchDataGridView.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ParcelSearchDataGridView.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ParcelSearchDataGridView.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ParcelSearchDataGridView.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ParcelSearchDataGridView.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ParcelSearchDataGridView.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ParcelSearchDataGridView.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ParcelSearchDataGridView.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ParcelSearchDataGridView.Columns[9].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.ParcelSearchDataGridView.Columns[this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.CentralItemIDColumn.ColumnName].DataPropertyName = this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.CentralItemIDColumn.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.ParcelNumberColumn.ColumnName].DataPropertyName = this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.ParcelNumberColumn.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.StatementNumberColumn.ColumnName].DataPropertyName = this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.StatementNumberColumn.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.OwnerNameColumn.ColumnName].DataPropertyName = this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.OwnerNameColumn.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.RollYearColumn.ColumnName].DataPropertyName = this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.RollYearColumn.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.CompanyNameColumn.ColumnName].DataPropertyName = this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.CompanyNameColumn.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.CompanyNumberColumn.ColumnName].DataPropertyName = this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.CompanyNumberColumn.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.BranchLineColumn.ColumnName].DataPropertyName = this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.BranchLineColumn.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.PropertyClassColumn.ColumnName].DataPropertyName = this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.PropertyClassColumn.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.FundColumn.ColumnName].DataPropertyName = this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.FundColumn.ColumnName;

            this.ParcelSearchDataGridView.PrimaryKeyColumnName = this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.CentralItemIDColumn.ColumnName;

            this.ParcelSearchDataGridView.DataSource = this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.DefaultView;
        }

        private void GetGridSearchData()
        {
           // this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.Clear();
            this.centralSearchXML = this.GetSearchXml();
            this.GridSearchDataSet = this.form1406Control.WorkItem.F1406_CentralAssessedGridDetails(this.centralSearchXML);
            this.ParcelSearchDataGridView.AutoGenerateColumns = false;
            this.ParcelSearchDataGridView.DataSource = this.GridSearchDataSet.f1406_pclst_CentrallyAssessedGridTable.DefaultView;
            this.RecordCountLabel.Text = this.ParcelSearchDataGridView.OriginalRowCount + SharedFunctions.GetResourceString("RecordsCount");

            if (this.ParcelSearchDataGridView.OriginalRowCount > 0)
            {
                this.ParcelSearchDataGridView.Rows[0].Selected = true;
                this.EnableSearchButton();
                this.ParcelAcceptButton.Enabled = true;
                this.ParcelSearchDataGridView.Enabled = true;
                this.ParcelSearchDataGridView.Rows[0].Selected = true;
            }
            else
            {
                this.ParcelAcceptButton.Enabled = false;
                this.ParcelSearchDataGridView.Enabled = false;
                this.ParcelSearchDataGridView.Rows[0].Selected = false;
            }

            this.ParcelSearchDataGridView.Rows[0].Selected = true;
           
        }
        /// <summary>
        /// Loads the combo box.
        /// </summary>
        private void LoadComboBox()
        {
            this.PropertyComboTable.Clear();
            this.PropertyComboTable = this.form1406Control.WorkItem.F1406_LoadPropertClassCombo().f1406_pcget_PropertyClass;
            if (this.PropertyComboTable.Rows.Count > 0)
            {
                this.PropertyClassComboBox.DataSource = this.PropertyComboTable;
                this.PropertyClassComboBox.ValueMember = this.PropertyComboTable.PropertyClassIDColumn.ColumnName;
                this.PropertyClassComboBox.DisplayMember = this.PropertyComboTable.PropertyClassColumn.ColumnName;
                
            }
        }

        /// <summary>
        /// Clears the controls.
        /// </summary>
        private void ClearControls()
        {
            this.ParcelNumberTextBox.Text = string.Empty;
            this.StatementNumberTextBox.Text = string.Empty;
            this.OwnerTextBox.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.CompanyNameTextBox.Text = string.Empty;
            this.CompanyNumberTextBox.Text = string.Empty;
            this.BranchLineTextBox.Text = string.Empty;
            this.SubfundTextBox.Text = string.Empty;
            this.PropertyClassComboBox.SelectedIndex = -1;
            this.RecordCountLabel.Text = string.Empty;
        }
        
        #endregion

        private void PropertyClassComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.EnableSearchButton();
            //this.EditText(sender, e);
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the ParcelSearchDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ParcelSearchDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (this.ParcelSearchDataGridView.OriginalRowCount > e.RowIndex)
                    {
                        this.GetCentralIDs();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ParcelSearchDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (this.ParcelSearchDataGridView.SelectedRows.Count > 0 && this.ParcelSearchDataGridView.OriginalRowCount > 0)
            {
                this.ParcelAcceptButton.Enabled = true;
            }
            else
            {
                this.ParcelAcceptButton.Enabled = false;
            }
        }

        private void ParcelSearchDataGridView_RowHeaderCellChanged(object sender, DataGridViewRowEventArgs e)
        {
            if (this.ParcelSearchDataGridView.SelectedRows.Count > 0 && this.ParcelSearchDataGridView.OriginalRowCount > 0)
            {
                this.ParcelAcceptButton.Enabled = true;
            }
            else
            {
                this.ParcelAcceptButton.Enabled = false;
            }
        }

        private void ParcelSearchDataGridView_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (this.ParcelSearchDataGridView.SelectedRows.Count > 0 && this.ParcelSearchDataGridView.OriginalRowCount > 0)
            {
                this.ParcelAcceptButton.Enabled = true;
            }
            else
            {
                this.ParcelAcceptButton.Enabled = false;
            }
        }

    }
}
