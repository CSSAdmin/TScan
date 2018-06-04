//--------------------------------------------------------------------------------------------
// <copyright file="F1405.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1405.cs.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 1/11/2010       P.Manoj Kumar            Created
//*********************************************************************************/

namespace D2000
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
    /// F1405
    /// </summary>
    public partial class F1405 : Form
    {
        #region variables

        /// <summary>
        /// controller F1404Controller
        /// </summary>
        private F1405Controller form1405Control;

        /// <summary>
        /// parcelsearchDatatable
        /// </summary>
        private DataTable statesearchDatatable = new DataTable();

        /// <summary>
        /// parcelSectionDataSet
        /// </summary>
        private F1403ParcelSearch parcelSectionDataSet = new F1403ParcelSearch();

        /// <summary>
        /// parcelsearchDatatable
        /// </summary>
        private DataTable statesearchValueDatatable = new DataTable();

        /// <summary>
        /// F1405StateSelectionData
        /// </summary>
        private F1405StateSelectionData stateSelectionData = new F1405StateSelectionData();

        /// <summary>
        /// Used to store the no of rows in the grid
        /// </summary>
        private int stateSelectionGridRowCount = 0;


        /// <summary>
        /// F1405ScheduleSelectionData
        /// </summary>

        ////used for obtain the datatable for state search
        //private F1405StateSelectionData StateSelectionData = new F1405StateSelectionData();

        /// <summary>
        /// RollYear
        /// </summary>
        private int rollYear;

        /// <summary>
        /// parcelsearchvaluedatarow
        /// </summary>
        private DataRow statesearchValuedatarow;

        /// <summary>
        /// parcelsearchdatarow
        /// </summary>
        private DataRow statesearchdatarow;

        /// <summary>
        /// ScheduleID
        /// </summary>
        private string stateID;

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        /// <summary>
        /// For Selected value
        /// </summary>
        private string commandValue;

        #endregion variables

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1405"/> class.
        /// </summary>
        public F1405()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1404"/> class.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        public F1405(int rollYear)
        {
            this.InitializeComponent();
            this.rollYear = rollYear;
        }

        #endregion

        #region Property
        /// <summary>
        /// For F1404Control
        /// </summary>
        [CreateNew]
        public F1405Controller Form1405Control
        {
            get { return this.form1405Control as F1405Controller; }
            set { this.form1405Control = value; }
        }

        /// <summary>
        /// Gets or sets the schedule id.
        /// </summary>
        /// <value>The schedule id.</value>
        public string StateID
        {
            get { return this.stateID; }
            set { this.stateID = value; }
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

        /// <summary>
        /// Gets or sets the command value.
        /// </summary>
        /// <value>The command value.</value>
        public string CommandValue
        {
            get { return this.commandValue; }
            set { this.CommandValue = value; }
        }

        #endregion

        #region FormLoad
        /// <summary>
        /// Handles the Load event of the F1405 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1405_Load(object sender, EventArgs e)
        {
            //this.ClearStateSearch();

            this.SearchButton.Enabled = true;
            this.StateCancelButton.Enabled = true;

            //this.stateSelectionData = this.form1405Control.WorkItem.F1405_ListStateSearch(null);    
            ////used to display the county configuration value.
            this.parcelSectionDataSet = this.form1405Control.WorkItem.F1403_GetParcelType(null);
            if (this.parcelSectionDataSet != null)
            {
                if (this.parcelSectionDataSet.ParcelRollYearDataTable.Rows.Count > 0)
                {
                    this.RollYearTextBox.Text = this.parcelSectionDataSet.ParcelRollYearDataTable.Rows[0][0].ToString();
                }
            }
            //this.StateSearchDataGridView.DataSource = null;
            //this.StateSearchDataGridView.Enabled = false;
            //this.StateSearchDataGridView.Rows[0].Selected = false;
            //this.DisableButtons();
            this.CustomizeStateSelectionGrid();
            this.FormLoad();
            //this.stateSelectionData = this.form1405Control.WorkItem.F1405_GetStateType(null);
            //if (this.stateSelectionData != null)
            //{
            //    if (this.stateSelectionData.StateRollYearDataTable.Rows.Count > 0)
            //    {
            //        this.RollYearTextBox.Text = this.stateSelectionData.StateRollYearDataTable.Rows[0][0].ToString();
            //    }
            //}

        }
        #endregion Load

        #region ButtonEvents

        /// <summary>
        /// Handles the Click event of the ClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormLoad();
                this.RollYearTextBox.Text = string.Empty;
                this.RecordCountLabel.Text = string.Empty;
                this.ClearButton.Enabled = false;  
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the AcceptButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StateAcceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                
                    this.selectStateId();
                    //this.DialogResult = DialogResult.OK;
                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void stateCancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                this.DialogResult = DialogResult.Cancel;
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
                if (this.SearchButton.Enabled)
                {
                    if (this.RollYearTextBox.Text != "")
                    {
                        int rollyear;
                        int.TryParse(this.RollYearTextBox.Text, out rollyear);

                        if (rollyear == 0)
                        {
                            MessageBox.Show("Year should be greater than 1899 and lesser than 2080", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.RollYearTextBox.Text = "";
                            return;
                        }

                        if (Convert.ToInt32(this.RollYearTextBox.Text) < 1900 || Convert.ToInt32(this.RollYearTextBox.Text) > 2079)
                        {
                            MessageBox.Show("Year should be greater than 1899 and lesser than 2080", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.RollYearTextBox.Text = "";
                            return;
                        }
                    }
                    this.stateSelectionData.GetStateSelection.Clear();
                    this.Cursor = Cursors.WaitCursor;
                    this.stateSelectionData.Merge(this.Form1405Control.WorkItem.F1405_ListStateSearch(this.GetSearchXml()));
                    this.StateSearchDataGridView.DataSource = this.stateSelectionData.GetStateSelection.DefaultView;
                    this.Cursor = Cursors.Default;
                    this.RecordCountLabel.Text = this.StateSearchDataGridView.OriginalRowCount + SharedFunctions.GetResourceString("9101MasterNameSearch");

                    if (this.StateSearchDataGridView.OriginalRowCount > 0)
                    {
                        this.StateSearchDataGridView.Rows[0].Selected = true;
                        this.EnableSearchButton();
                        this.StateAcceptButton.Enabled = true;
                        this.StateSearchDataGridView.Enabled = true;
                        this.StateSearchDataGridView.FirstDisplayedScrollingColumnIndex = 0;                        
                     }
                    else
                    {
                        this.StateAcceptButton.Enabled = false;
                        this.StateSearchDataGridView.Enabled = false;
                        this.StateSearchDataGridView.FirstDisplayedScrollingColumnIndex = 0;
                    }

                    //this.StateSearchDataGridView.Rows[0].Selected = true;
                
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
        }

        #endregion ButtonEvents

        #region Methods
        /// <summary>
        /// Clears the state search.
        /// </summary>
        private void ClearStateSearch()
        {
            try
            {
                this.ParcelNumberTextBox.Text = string.Empty;
                this.FirstNameTextBox.Text = string.Empty;
                this.LastNameTextBox.Text = string.Empty;
                this.CompanyNumberTextBox.Text = string.Empty;
                //this.RollYearTextBox.Text = string.Empty;
                this.DistrictTextBox.Text = string.Empty;
                this.RecordCountLabel.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

 
        /// <summary>
        /// Gets the search XML.
        /// </summary>
        /// <returns>string</returns>
        private string GetSearchXml()
        {
            int rollYearSearch = 0;
            DataTable searchValueDataTable = new DataTable();
            string searchValueXml = string.Empty;
            searchValueDataTable = (DataTable)this.stateSelectionData.GetStateSelection.Clone();

            DataRow searchRow;
            searchRow = searchValueDataTable.NewRow();

            int.TryParse(this.RollYearTextBox.Text.Trim(), out rollYearSearch);
            searchRow[this.stateSelectionData.GetStateSelection.RollYearColumn.ColumnName] = rollYearSearch;
            searchRow[this.stateSelectionData.GetStateSelection.ParcelNumberColumn.ColumnName] = this.ParcelNumberTextBox.Text.Trim();
            searchRow[this.stateSelectionData.GetStateSelection.FirstNameColumn.ColumnName] = this.FirstNameTextBox.Text.Trim();
            searchRow[this.stateSelectionData.GetStateSelection.LastNameColumn.ColumnName] = this.LastNameTextBox.Text.Trim();
            searchRow[this.stateSelectionData.GetStateSelection.CompanyNumberColumn.ColumnName] = this.CompanyNumberTextBox.Text.Trim();
            searchRow[this.stateSelectionData.GetStateSelection.DistrictColumn.ColumnName] = this.DistrictTextBox.Text.Trim();
            searchValueDataTable.Rows.Add(searchRow);

          searchValueXml = TerraScanCommon.GetXmlString(searchValueDataTable);
            return searchValueXml; 

        }
        /// <summary>
        /// Clears the schedule search grid.
        /// </summary>
        private void ClearStateSearchGrid()
        {
            this.StateSearchDataGridView.Enabled = false;
            this.StateSearchDataGridView.DataSource = null;
            this.StateSearchDataGridView.Rows[0].Selected = false;
            this.StateAcceptButton.Enabled = false;
            ////this.ScheduleSelectionVerticalScroll.Visible = true; 
        }


        /// <summary>
        /// Customizes the schedule selection grid.
        /// </summary>
        private void CustomizeStateSelectionGrid()
        {
            try
            {
                this.StateSearchDataGridView.ScrollBars = ScrollBars.Both;  
                this.StateSearchDataGridView.IsMultiSelect = true;
                this.StateSearchDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
                this.StateSearchDataGridView.AllowUserToResizeColumns=false;
                this.StateSearchDataGridView.AutoGenerateColumns = false;
                this.StateSearchDataGridView.AllowUserToResizeRows = false;
                this.StateSearchDataGridView.PrimaryKeyColumnName = this.stateSelectionData.GetStateSelection.StateIDColumn.ColumnName;

                this.StateSearchDataGridView.Columns[this.StateSearchID.Name].DataPropertyName = this.stateSelectionData.GetStateSelection.StateIDColumn.ColumnName;          
                this.StateSearchDataGridView.Columns[this.stateSelectionData.GetStateSelection.ParcelNumberColumn.ColumnName].DataPropertyName = this.stateSelectionData.GetStateSelection.ParcelNumberColumn.ColumnName;
                this.StateSearchDataGridView.Columns[this.OwnerFirstName.Name].DataPropertyName = this.stateSelectionData.GetStateSelection.FirstNameColumn.ColumnName;
                this.StateSearchDataGridView.Columns[this.OwnerLastName.Name].DataPropertyName = this.stateSelectionData.GetStateSelection.LastNameColumn.ColumnName;
                this.StateSearchDataGridView.Columns[this.stateSelectionData.GetStateSelection.CompanyNumberColumn.ColumnName].DataPropertyName = this.stateSelectionData.GetStateSelection.CompanyNumberColumn.ColumnName;
                this.StateSearchDataGridView.Columns[this.stateSelectionData.GetStateSelection.DistrictColumn.ColumnName].DataPropertyName = this.stateSelectionData.GetStateSelection.DistrictColumn.ColumnName;
                this.StateSearchDataGridView.Columns[this.RollYeartext.Name].DataPropertyName = this.stateSelectionData.GetStateSelection.RollYearColumn.ColumnName;         
                //DataGridViewColumnCollection columns = this.StateSearchDataGridView.Columns
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }

        /// <summary>
        /// Settings the read only for schedule grid.
        /// </summary>
        private void SettingReadOnlyForStateGrid()
        {
            try
            {
                this.StateSearchDataGridView.ScrollBars = ScrollBars.Both;
                DataGridViewColumnCollection columns = this.StateSearchDataGridView.Columns;
                columns[this.stateSelectionData.GetStateSelection.StateIDColumn.ColumnName].ReadOnly = true;
                columns[this.stateSelectionData.GetStateSelection.ParcelNumberColumn.ColumnName].ReadOnly = true;
                columns[this.stateSelectionData.GetStateSelection.RollYearColumn.ColumnName].ReadOnly = true;
                columns[this.stateSelectionData.GetStateSelection.FirstNameColumn.ColumnName].ReadOnly = true;
                columns[this.stateSelectionData.GetStateSelection.LastNameColumn.ColumnName].ReadOnly = true;
                columns[this.stateSelectionData.GetStateSelection.CompanyNumberColumn.ColumnName].ReadOnly = true;
                columns[this.stateSelectionData.GetStateSelection.DistrictColumn.ColumnName].ReadOnly = true;
                columns[this.stateSelectionData.GetStateSelection.RollYearColumn.ColumnName].ReadOnly = true;

                columns[this.stateSelectionData.GetStateSelection.StateIDColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                columns[this.stateSelectionData.GetStateSelection.ParcelNumberColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                columns[this.stateSelectionData.GetStateSelection.RollYearColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                columns[this.stateSelectionData.GetStateSelection.FirstNameColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                columns[this.stateSelectionData.GetStateSelection.LastNameColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                columns[this.stateSelectionData.GetStateSelection.CompanyNumberColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                columns[this.stateSelectionData.GetStateSelection.DistrictColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                columns[this.stateSelectionData.GetStateSelection.RollYearColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;

                columns[this.stateSelectionData.GetStateSelection.StateIDColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                columns[this.stateSelectionData.GetStateSelection.ParcelNumberColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                columns[this.stateSelectionData.GetStateSelection.RollYearColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                columns[this.stateSelectionData.GetStateSelection.FirstNameColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                columns[this.stateSelectionData.GetStateSelection.LastNameColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                columns[this.stateSelectionData.GetStateSelection.CompanyNumberColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                columns[this.stateSelectionData.GetStateSelection.DistrictColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                columns[this.stateSelectionData.GetStateSelection.RollYearColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }

         /// <summary>
        /// Forms the load.
        /// </summary>
        private void FormLoad()
        {
            //// this.DisableButtons();
            this.StateSearchDataGridView.DataSource = null;
            this.StateSearchDataGridView.Enabled = false;
            //this.StateSearchDataGridView.Rows[0].Selected = false;
            this.ClearStateSearch();
            if (this.StateSearchDataGridView.OriginalRowCount > 0)
            {
                this.StateAcceptButton.Enabled = true;
            }
            else
            {
                this.StateAcceptButton.Enabled = false;
            }
            this.ParcelNumberTextBox.Focus();
        }

        /// <summary>
        /// To disable the accept,search, and clear buttons
        /// </summary>        
        private void DisableButtons()
        {
            this.SearchButton.Enabled = false;
            this.StateAcceptButton.Enabled = false;
            this.ClearButton.Enabled = false;
        }

        /// <summary>
        /// Enables the search button.
        /// </summary>
        private void EnableSearchButton()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.ParcelNumberTextBox.Text.Trim().Replace("'", "''")) || !string.IsNullOrEmpty(this.FirstNameTextBox.Text.Trim().Replace("'", "''")) || !string.IsNullOrEmpty(this.LastNameTextBox.Text.Trim().Replace("'", "''")) || !string.IsNullOrEmpty(this.CompanyNumberTextBox.Text.Trim().Replace("'", "''")) || !string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim().Replace("'", "''")) || !string.IsNullOrEmpty(this.DistrictTextBox.Text.Trim().Replace("'", "''")))
                {
                    this.SearchButton.Enabled = true;
                    this.ClearButton.Enabled = true;
                }
                else
                {
                    this.SearchButton.Enabled = false;

                    if (this.stateSelectionGridRowCount <= 0)
                    {
                        this.StateAcceptButton.Enabled = false;
                        this.ClearButton.Enabled = false;
                    }
                    else
                    {
                        this.StateAcceptButton.Enabled = true;
                        this.ClearButton.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        /// <summary>
        /// Selects the state id.
        /// </summary>
        private void selectStateId()
        {
            
            if (this.stateSelectionData.GetStateSelection.Rows.Count > 0 )
            {
                ////to obtain single Id (or) multiple State Ids 
                if (this.statesearchDatatable.Columns.Count == 0)
                {
                    this.statesearchDatatable.Columns.Add("StateID");
                }
                //if (this.statesearchValueDatatable.Columns.Count == 0)
                //{
                //    this.statesearchValueDatatable.Columns.Add("ScheduleNumber");
                //}
                DataGridViewSelectedRowCollection selectedRows = this.StateSearchDataGridView.SelectedRows;
                foreach (DataGridViewRow dataRow in selectedRows)
                {
                    if (!string.IsNullOrEmpty(dataRow.Cells["StateSearchID"].Value.ToString().Trim()))
                    {
                        ////Adding StateId
                        this.statesearchdatarow = this.statesearchDatatable.NewRow();
                        this.statesearchdatarow["StateID"] = dataRow.Cells["StateSearchID"].Value.ToString().Trim();
                        this.statesearchDatatable.Rows.Add(this.statesearchdatarow);

                        //////Adding Parcel Number
                        //this.statesearchValuedatarow = this.statesearchValueDatatable.NewRow();
                        //this.statesearchValuedatarow["ParcelNumber"] = dataRow.Cells["ParcelNumber"].Value.ToString().Trim();
                        //this.statesearchValueDatatable.Rows.Add(this.statesearchValuedatarow);
                    }

                }

                ////used to get the xml and display in the TaxRollCorrection form grid view. 
                this.commandResult = TerraScanCommon.GetXmlString(this.statesearchDatatable);
                //this.commandValue = TerraScanCommon.GetXmlString(this.statesearchValueDatatable);
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            else
            {
                this.DialogResult = DialogResult.None;
            }

        }
        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        /// <returns>integer value of grid row index</returns>
        private int GetRowIndex()
        {
            try
            {
                if (this.stateSelectionGridRowCount > 0)
                {
                    if (this.StateSearchDataGridView.CurrentCell != null)
                    {
                        return this.StateSearchDataGridView.CurrentCell.RowIndex;
                    }

                    return -1;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

       
        #endregion Methods

        #region gridview events

        /// <summary>
        /// StateSearchGridView-CellDoubleClick.
        /// </summary>
        /// <param name="CommandResult">CommandResult XML.</param>
        /// <param name="CommandValue">CommandValue XML.</param> 
        private void StateSearchDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.StateAcceptButton.Enabled)
                {
                    this.selectStateId(); 
                    //        if (this.stateSelectionGridRowCount > 0)
                    //        {
                    //            ////used to remove after selection removed. 
                    //            if (!string.IsNullOrEmpty(this.StateSearchDataGridView.Rows[e.RowIndex].Cells[this.stateSelectionData.GetStateSelection.StateIDColumn.ColumnName].Value.ToString()))
                    //            {
                    //                ////To get a Multiple or single schedule Id(s)
                    //            if (this.statesearchDatatable.Columns.Count == 0)
                    //            {
                    //                this.statesearchDatatable.Columns.Add("StateID");
                    //            }

                    //            if (this.statesearchValueDatatable.Columns.Count == 0)
                    //            {
                    //                this.statesearchValueDatatable.Columns.Add("ParcelNumber");
                    //            }

                    //            ////Adding StateId
                    //            this.statesearchdatarow = this.statesearchDatatable.NewRow();
                    //            this.statesearchdatarow["StateID"] = this.StateSearchDataGridView.Rows[e.RowIndex].Cells["StateID"].Value.ToString().Trim();
                    //            this.statesearchDatatable.Rows.Add(this.statesearchdatarow);

                    //            ////Adding parcel Number
                    //            this.statesearchValuedatarow = this.statesearchValueDatatable.NewRow();
                    //            this.statesearchValuedatarow["ParcelNumber"] = this.StateSearchDataGridView.Rows[e.RowIndex].Cells["ParcelNumber"].Value.ToString().Trim();
                    //            this.statesearchValueDatatable.Rows.Add(this.statesearchValuedatarow);

                    //            this.commandResult = TerraScanCommon.GetXmlString(this.statesearchDatatable);
                    //            this.commandValue = TerraScanCommon.GetXmlString(this.statesearchValueDatatable);

                    //            ////this.scheduleID = this.ScheduleSearchDataGridView.Rows[e.RowIndex].Cells[this.scheduleSelectionData.GetScheduleSelection.ScheduleIDColumn.ColumnName].Value.ToString();
                    //            ////this.commandResult = this.scheduleID;

                    //            ////if (!string.IsNullOrEmpty(this.ScheduleSearchDataGridView.Rows[e.RowIndex].Cells[this.scheduleSelectionData.GetScheduleSelection.ScheduleNumberColumn.ColumnName].Value.ToString()))
                    //            ////{
                    //            ////    this.commandValue = this.ScheduleSearchDataGridView.Rows[e.RowIndex].Cells[this.scheduleSelectionData.GetScheduleSelection.ScheduleNumberColumn.ColumnName].Value.ToString();
                    //            ////}
                    //            ////else
                    //            ////{
                    //            ////    this.commandValue = string.Empty;
                    //            ////}

                    //            this.DialogResult = DialogResult.OK;
                    //            this.Close();
                    //        }
                    //        else
                    //        {
                    //            this.stateID = "";
                    //            this.commandResult = this.stateID;
                    //            this.commandValue = string.Empty;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        this.DialogResult = DialogResult.None;
                    //    }
                    //    //// this.SelectScheduleId();
                    //    //// this.DialogResult = DialogResult.OK;
                    //}
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }
        #endregion gridview events

        #region textchange & keyevents
        /// <summary>
        /// Handles the TextChanged event of the ScheduleNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.ParcelNumberTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.FirstNameTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.LastNameTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.CompanyNumberTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.DistrictTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
                {
                    this.EnableSearchButton();
                }
                else
                {
                    if (this.stateSelectionGridRowCount >= 0)
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

        /// <summary>
        /// Handles the KeyUp event of the ScheduleNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ParcelNumberTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.SearchButton.Enabled)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Enter:
                            {
                                this.SearchButton_Click(sender, e);
                                break;
                            }
                        default:
                            {
                                break;
                            }

                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }
        #endregion textchange & keyevents


    }
}
