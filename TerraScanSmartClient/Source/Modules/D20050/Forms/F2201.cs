
namespace D20050
{
     using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Collections;
    using System.IO;
    using System.Configuration;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;
    using TerraScan.Utilities;

    public partial class F2201 : Form
    {
        private F2201Controller form2201Control;

        F2201CentrallyAssessedSearchData SearchDataset = new F2201CentrallyAssessedSearchData();
        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        /// <summary>
        /// For Selected value
        /// </summary>
        private string commandValue;

        private string CodeVal;
        private string DescriptionVal;
        private int selected;
        private bool emptyRecord;
        private bool hasValue = false;
        private string propertyCode = string.Empty;


        public F2201()
        {
            InitializeComponent();
        }

        public F2201(string propCode)
        {
            this.InitializeComponent();
            if (!string.IsNullOrEmpty(propCode))
            {
                this.hasValue = true;
                this.propertyCode = propCode;
            }
        }


        private void F2201_Load(object sender, EventArgs e)
        {
            this.emptyRecord = true;
            this.CustomizeDataGrid();            
            this.ScheduleSearchDataGridView.DataSource = null;
            this.ScheduleSearchDataGridView.Enabled = false;
            this.ScheduleSearchDataGridView.Rows[0].Selected = false;
            this.DisableVScrollBar();
            ////this.ClearFields();
            this.DisableButtons();
            if (hasValue)
            {
                this.RemoveButton.Enabled = true;
            }
            else
            {
                this.RemoveButton.Enabled = false;
            }

        }

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication


        #region properites

        /// <summary>
        /// Gets or sets the F1512 controll.
        /// </summary>
        /// <value>The F1512 controll.</value>
        [CreateNew]
        public F2201Controller F2201Controll
        {
            get { return this.form2201Control as F2201Controller; }
            set { this.form2201Control = value; }
        }

        /// <summary>
        /// Gets or sets the district id.
        /// </summary>
        /// <value>The district id.</value>
        //public int DistrictId
        //{
        //    get { return this.districtID; }
        //    set { this.districtID = value; }
        //}

        /// <summary>
        /// Gets or sets the command result.
        /// </summary>
        /// <value>The command result.</value>
        public string CommandResult
        {
            get { return commandResult; }
            set { commandResult = value; }
        }

        /// <summary>
        /// Gets or sets the command value.
        /// </summary>
        /// <value>The command value.</value>
        public string CommandValue
        {
            get { return this.commandValue; }
            set { this.CommandValue = value; }
        }

        #endregion properites


        #region Methods

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        /// <returns>returns GetRowIndex of selected item</returns>
        private int GetRowIndex()
        {
            try
            {
                if (this.SearchDataset.f2201_PersonalPropertyCodeSelection.Rows.Count > 0)
                {
                    if (this.ScheduleSearchDataGridView.SelectedRows.Count > 0)
                    {
                        this.selected = this.ScheduleSearchDataGridView.SelectedRows[0].Index;
                    }
                    else if (this.ScheduleSearchDataGridView.SelectedCells.Count > 0)
                    {
                        this.selected = this.ScheduleSearchDataGridView.CurrentCell.RowIndex;
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
            //if (this.SearchDataset.f2201_PersonalPropertyCodeSelection.Rows.Count > 5)
            //{
            //    this.DistrictSlectionVerticalScroll.Enabled = true;
            //    this.DistrictSlectionVerticalScroll.Visible = false;
            //}
            //else
            //{
            //    this.DistrictSlectionVerticalScroll.Enabled = false;
            //    this.DistrictSlectionVerticalScroll.Visible = true;
            //    this.DistrictSlectionVerticalScroll.BringToFront();
            //}
        }

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizeDataGrid()
        {
            try
            {
                this.ScheduleSearchDataGridView.AllowUserToResizeColumns = false;
                this.ScheduleSearchDataGridView.AutoGenerateColumns = false;
                this.ScheduleSearchDataGridView.AllowUserToResizeRows = false;
                this.ScheduleSearchDataGridView.StandardTab = true;
                this.ScheduleSearchDataGridView.EnableBinding = false;
                this.ScheduleSearchDataGridView.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.ScheduleSearchDataGridView.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.ScheduleSearchDataGridView.Columns[0].DataPropertyName = "PersonalPropertyCode";
                this.ScheduleSearchDataGridView.Columns[1].DataPropertyName = "Description";
                this.ScheduleSearchDataGridView.PrimaryKeyColumnName = this.ScheduleSearchDataGridView.Columns[0].DataPropertyName;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Enables the search button.
        /// </summary>
        private void EnableSearchButton()
        {
            //!string.IsNullOrEmpty(this.DistrictIdTextBox.Text.Trim()) ||
            if (!string.IsNullOrEmpty(this.CodeTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim()))
            {
                this.SearchButton.Enabled = true;
                this.ClearButton.Enabled = true;
            }
            else
            {
                this.SearchButton.Enabled = false;

                if (this.SearchDataset.f2201_PersonalPropertyCodeSelection.Rows.Count <= 0)
                {
                    this.AcceptButton.Enabled = false;
                    this.ClearButton.Enabled = false;
                }
                else
                {
                    this.AcceptButton.Enabled = true;
                    this.ClearButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Gets the district details.
        /// </summary>
        private void GetSearchDetails()
        {
            int rowId = 0;
            rowId = this.GetRowIndex();

            if (!this.emptyRecord)
            {               
                if (!string.IsNullOrEmpty(this.ScheduleSearchDataGridView.Rows[rowId].Cells["PersonalPropertyCode"].Value.ToString()))
                {
                    this.commandResult = this.ScheduleSearchDataGridView.Rows[rowId].Cells["PersonalPropertyCode"].Value.ToString();                                        
                    this.CodeTextBox.Focus();
                    //   this.DistrictIdTextBox.Focus();
                }
                else
                {
                    this.commandResult = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.ScheduleSearchDataGridView.Rows[rowId].Cells["Description"].Value.ToString()))
                {
                    this.commandValue = this.ScheduleSearchDataGridView.Rows[rowId].Cells["Description"].Value.ToString();
                }
                else
                {
                    this.commandValue = string.Empty;
                }
            }
        }

        /// <summary>
        /// Clears the fields.
        /// </summary>
        private void ClearFields()
        {           
            this.CodeTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.RecordCountLabel.Text = string.Empty;
        }

        /// <summary>
        /// Disables the buttons.
        /// </summary>
        private void DisableButtons()
        {
            this.SearchButton.Enabled = false;
            this.AcceptButton.Enabled = false;
            this.ClearButton.Enabled = false;
            this.RemoveButton.Enabled = false;
        }

        /// <summary>
        /// Checks the key up.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void CheckKeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    {
                        break;
                    }

                case Keys.Tab:
                    {
                        break;
                    }

                default:
                    {
                        this.EnableSearchButton();
                        break;
                    }
            }
        }

        #endregion Methods


        private void ValidateFields()
        {
            try
            {
                if ((!string.IsNullOrEmpty(this.CodeTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim())))
                {
                    this.EnableSearchButton();
                }
                else
                {
                    if (this.SearchDataset.Tables["f2201_PersonalPropertyCodeSelection"].Rows.Count > 0)
                    {
                        this.SearchButton.Enabled = false;
                    }
                    else
                    {
                        this.DisableButtons();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        private void LoadGridResultSet()
        {
            try
            {
                this.CodeVal = this.CodeTextBox.Text;
                this.DescriptionVal = this.DescriptionTextBox.Text;
                int recordCount;
                this.SearchDataset.f2201_PersonalPropertyCodeSelection.Clear();
                this.SearchDataset = this.form2201Control.WorkItem.F2201_GetPersonalPropertySearch(this.CodeTextBox.Text.Trim(), this.DescriptionTextBox.Text.Trim());
                recordCount = this.SearchDataset.f2201_PersonalPropertyCodeSelection.Rows.Count;
                if (SearchDataset.f2201_PersonalPropertyCodeSelection.Rows.Count > 0 && this.ScheduleSearchDataGridView.RowCount > 0)
                {
                    // recordCount = this.SearchDataset.f2201_PersonalPropertyCodeSelection.Rows.Count;
                    this.ScheduleSearchDataGridView.Enabled = true;
                    this.ScheduleSearchDataGridView.DataSource = this.SearchDataset.f2201_PersonalPropertyCodeSelection.DefaultView;
                    this.ScheduleSearchDataGridView.Focus();
                    this.ScheduleSearchDataGridView.Rows[0].Selected = true;
                    this.AcceptButton.Enabled = true;
                    this.emptyRecord = false;                    
                    this.RemoveButton.Enabled = false;
                }
                else
                {
                    // recordCount = this.SearchDataset.f2201_PersonalPropertyCodeSelection.Rows.Count;
                    this.ScheduleSearchDataGridView.DataSource = this.SearchDataset.f2201_PersonalPropertyCodeSelection.DefaultView;
                    this.ScheduleSearchDataGridView.Enabled = false;
                    this.ScheduleSearchDataGridView.Rows[0].Selected = false;
                    this.ScheduleSearchDataGridView.CurrentCell = null;
                    this.emptyRecord = true;
                    this.AcceptButton.Enabled = false;
                }
                this.RecordCountLabel.Text = recordCount + SharedFunctions.GetResourceString("9101MasterNameSearch");
            }

            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
       
        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
         {
            this.ValidateFields();
        }

        private void CodeTextBox_TextChanged(object sender, EventArgs e)
         {
            this.ValidateFields();

        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            this.CodeTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.SearchDataset.f2201_PersonalPropertyCodeSelection.Clear();
            this.ScheduleSearchDataGridView.DataSource = null;
            this.ScheduleSearchDataGridView.Enabled = false;
            this.ScheduleSearchDataGridView.Rows[0].Selected = false;
            this.ScheduleSearchDataGridView.CurrentCell = null;
            this.ClearFields();
            this.DisableButtons();
           // this.DisableVScrollBar();
            this.CodeTextBox.Focus();
            if (hasValue)
            {
                this.RemoveButton.Enabled = true;
            }
            else
            {
                this.RemoveButton.Enabled = false;
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SearchButton.Enabled)
                {
                    this.LoadGridResultSet();
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

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            if (!this.emptyRecord)
            {
                if (this.ScheduleSearchDataGridView.OriginalRowCount > this.ScheduleSearchDataGridView.CurrentRowIndex)
                {
                    this.GetSearchDetails();
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (this.RemoveButton.Enabled)
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (this.CancelButton.Enabled)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void ScheduleSearchDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (this.ScheduleSearchDataGridView.OriginalRowCount > e.RowIndex)
                {
                    this.GetSearchDetails();
                    this.DialogResult = DialogResult.OK;
                }
                
            }
        }
        private void ScheduleSearchDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && this.AcceptButton.Enabled)
            {
                if (this.ScheduleSearchDataGridView.OriginalRowCount > e.RowIndex)
                {
                    this.GetSearchDetails();
                    this.DialogResult = DialogResult.OK;
                }

            }
        }
        private void ScheduleSearchDataGridView_RowHeaderCellChanged(object sender, DataGridViewRowEventArgs e)
        {
            if (this.ScheduleSearchDataGridView.OriginalRowCount > e.Row.Index)
            {

            }
            else
            {

            }
        }

        private void CodeTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyValue.Equals(13))
            {
            if (!string.IsNullOrEmpty(this.CodeTextBox.Text) || !string.IsNullOrEmpty(this.DescriptionTextBox.Text))
            {
                if (this.SearchButton.Enabled)
                {
                    this.LoadGridResultSet();
                }
            }
            }
        }
    }
}
