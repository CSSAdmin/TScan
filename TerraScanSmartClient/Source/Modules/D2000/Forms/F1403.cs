//--------------------------------------------------------------------------------------------
// <copyright file="F1403.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09 oct 09		R.Malliga	     Created
// 20 Dec 16        Priyadharshini      Modified to change the Rollyear Field
//*********************************************************************************/

namespace D2000
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

    /// <summary>
    /// F1403
    /// </summary>
    public partial class F1403 : BasePage 
    {
       
       #region Variable

        /// <summary>
        /// Created Instance for F1403Controller
        /// </summary>
        private F1403Controller form1403Control;

        /// <summary>
        /// parcelId
        /// </summary>
        private int parcelId;

        /// <summary>
        /// rollYear
        /// </summary>
        private int rollYear;

        ///<summary>
        /// calling Form
        /// </summary>
        private int callingForm = 0;

        /// <summary>
        /// parcelType
        /// </summary>
        private int parcelType;

        /// <summary>
        /// parcelSectionDataSet
        /// </summary>
        private F1403ParcelSearch parcelSectionDataSet = new F1403ParcelSearch();

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        /// <summary>
        /// parcelsearchDatatable
        /// </summary>
        private DataTable parcelsearchDatatable = new DataTable();

        /// <summary>
        /// parcelsearchdatarow
        /// </summary>
        private DataRow parcelsearchdatarow;

        #endregion Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1403"/> class.
        /// </summary>
        public F1403()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1403"/> class.
        /// </summary>
        /// <param name="rollYearParameter">The roll year parameter.</param>
        public F1403(int rollYearParameter)
        {
            this.InitializeComponent();
            this.rollYear = rollYearParameter;
        }

        public F1403(int rollYearParameter,string callForm)
        {
            this.InitializeComponent();
            this.rollYear = rollYearParameter;
            this.callingForm = int.Parse(callForm.ToString());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1403"/> class.
        /// </summary>
        /// <param name="rollYearParameter">The roll year parameter.</param>
        public F1403(string callForm)
        {
            this.InitializeComponent();
            int.TryParse(callForm, out this.callingForm);    
            
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1403"/> class.
        /// </summary>
        /// <param name="rollYearParameter">The roll year parameter.</param>
        /// <param name="parcelTypeParameter">The parcel type parameter.</param>
        public F1403(int rollYearParameter, int parcelTypeParameter)
        {
            this.InitializeComponent();
            this.rollYear = rollYearParameter;
            this.parcelType = parcelTypeParameter;
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the F1403 controll.
        /// </summary>
        /// <value>The F1403 controll.</value>
        [CreateNew]
        public F1403Controller F1403Controll
        {
            get { return this.form1403Control as F1403Controller; }
            set { this.form1403Control = value; }
        }

        /// <summary>
        /// Gets or sets the parcel id.
        /// </summary>
        /// <value>The parcel id.</value>
        public int ParcelID
        {
            get { return this.parcelId; }
            set { this.parcelId = value; }
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
        /// Searches the value.
        /// </summary>
        /// <returns>bool</returns>
        private bool HasSearchValue()
        {
            if ((!string.IsNullOrEmpty(this.ParcelNumberTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.StatementNumberTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.OwnerTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SitusTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.LegalTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.ID1TextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.DistrictTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.NeighborhoodTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.DorTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.ID2TextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.ID3TextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.ID4TextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.ID5TextBox.Text.Trim())))
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
            ////this.SearchButton.Enabled = false;
            this.ParcelAcceptButton.Enabled = false;
            this.ClearButton.Enabled = false;
        }

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizeDataGrid()
        {
            this.ParcelSearchDataGridView.ScrollBars = ScrollBars.Both;  
            this.ParcelSearchDataGridView.IsMultiSelect = true;
            this.ParcelSearchDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ParcelSearchDataGridView.AllowUserToResizeColumns = false;
            this.ParcelSearchDataGridView.AutoGenerateColumns = false;
            this.ParcelSearchDataGridView.AllowUserToResizeRows = false;
            this.ParcelSearchDataGridView.StandardTab = true;
            this.ParcelSearchDataGridView.PrimaryKeyColumnName = this.parcelSectionDataSet.ParcelSearchDataTable.ParcelIDColumn.ColumnName;

            this.ParcelSearchDataGridView.Columns[SharedFunctions.GetResourceString("ParcelSearchIDColumnName")].DataPropertyName = this.parcelSectionDataSet.ParcelSearchDataTable.ParcelIDColumn.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.parcelSectionDataSet.ParcelSearchDataTable.ParcelNumberColumn.ColumnName].DataPropertyName = this.parcelSectionDataSet.ParcelSearchDataTable.ParcelNumberColumn.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.parcelSectionDataSet.ParcelSearchDataTable.StatementNumberColumn.ColumnName].DataPropertyName = this.parcelSectionDataSet.ParcelSearchDataTable.StatementNumberColumn.ColumnName;
            this.ParcelSearchDataGridView.Columns[SharedFunctions.GetResourceString("OwnerColumnName")].DataPropertyName = this.parcelSectionDataSet.ParcelSearchDataTable.OwnerNameColumn.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.parcelSectionDataSet.ParcelSearchDataTable.RollYearColumn.ColumnName].DataPropertyName = this.parcelSectionDataSet.ParcelSearchDataTable.RollYearColumn.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.parcelSectionDataSet.ParcelSearchDataTable.ParcelTypeColumn.ColumnName].DataPropertyName = this.parcelSectionDataSet.ParcelSearchDataTable.ParcelTypeColumn.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.parcelSectionDataSet.ParcelSearchDataTable.SitusColumn.ColumnName].DataPropertyName = this.parcelSectionDataSet.ParcelSearchDataTable.SitusColumn.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.parcelSectionDataSet.ParcelSearchDataTable.LegalColumn.ColumnName].DataPropertyName = this.parcelSectionDataSet.ParcelSearchDataTable.LegalColumn.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.parcelSectionDataSet.ParcelSearchDataTable.ID1Column.ColumnName].DataPropertyName = this.parcelSectionDataSet.ParcelSearchDataTable.ID1Column.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.parcelSectionDataSet.ParcelSearchDataTable.ID2Column.ColumnName].DataPropertyName = this.parcelSectionDataSet.ParcelSearchDataTable.ID2Column.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.parcelSectionDataSet.ParcelSearchDataTable.ID3Column.ColumnName].DataPropertyName = this.parcelSectionDataSet.ParcelSearchDataTable.ID3Column.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.parcelSectionDataSet.ParcelSearchDataTable.ID4Column.ColumnName].DataPropertyName = this.parcelSectionDataSet.ParcelSearchDataTable.ID4Column.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.parcelSectionDataSet.ParcelSearchDataTable.ID5Column.ColumnName].DataPropertyName = this.parcelSectionDataSet.ParcelSearchDataTable.ID5Column.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.parcelSectionDataSet.ParcelSearchDataTable.DistrictColumn.ColumnName].DataPropertyName = this.parcelSectionDataSet.ParcelSearchDataTable.DistrictColumn.ColumnName;
            this.ParcelSearchDataGridView.Columns[this.parcelSectionDataSet.ParcelSearchDataTable.NeighborhoodColumn.ColumnName].DataPropertyName = this.parcelSectionDataSet.ParcelSearchDataTable.NeighborhoodColumn.ColumnName;
            this.ParcelSearchDataGridView.Columns[SharedFunctions.GetResourceString("DORColumnName")].DataPropertyName = this.parcelSectionDataSet.ParcelSearchDataTable.StateCodeColumn.ColumnName;
        }

        /// <summary>
        /// Clears the fields.
        /// </summary>
        private void ClearFields()
        {
            this.ParcelNumberTextBox.Text = string.Empty;
            this.StatementNumberTextBox.Text = string.Empty;
            this.OwnerTextBox.Text = string.Empty;
            //if (this.callingForm > 0 && this.callingForm == 24550)
            //{
            //    if (this.rollYear > 0)
            //    {
            //        this.RollYearTextBox.Text = this.rollYear.ToString();
            //    }
            //    else
            //    {
            //        this.RollYearTextBox.Text = string.Empty;
            //    }
            //}
            this.SitusTextBox.Text = string.Empty;
            this.LegalTextBox.Text = string.Empty;
            this.ID1TextBox.Text = string.Empty;
            this.DistrictTextBox.Text = string.Empty;
            this.NeighborhoodTextBox.Text = string.Empty;
            this.DorTextBox.Text = string.Empty;
            this.ID2TextBox.Text = string.Empty;
            this.ID3TextBox.Text = string.Empty;
            this.ID4TextBox.Text = string.Empty;
            this.ID5TextBox.Text = string.Empty;
        }

        /// <summary>
        /// Forms the load.
        /// </summary>
        private void FormLoad()
        {
           //// this.DisableButtons();
            this.ParcelSearchDataGridView.DataSource = null;
            this.ParcelSearchDataGridView.Enabled = false;
            this.ParcelSearchDataGridView.Rows[0].Selected = false;
            this.ClearFields();

            if (this.ParcelSearchDataGridView.OriginalRowCount > 0)
            {
                this.ParcelAcceptButton.Enabled = true;   
            }
            else
            {
                this.ParcelAcceptButton.Enabled = false;
            }

            if (this.ParcelTypeComboBox != null)
            {
                if (this.parcelType > 0)
                {
                    this.ParcelTypeComboBox.SelectedValue = this.parcelType;
                    this.ParcelTypeComboBox.Enabled = false;
                }
                else
                {
                    this.ParcelTypeComboBox.SelectedIndex = 0;
                }
            }

            this.ParcelNumberTextBox.Focus();
        }

        /// <summary>
        /// Gets the parcel ID.
        /// </summary>
        private void GetParcelID()
        {
            if (this.parcelSectionDataSet.ParcelSearchDataTable.Rows.Count > 0)
            {
                if (this.parcelsearchDatatable.Columns.Count == 0)
                {
                    this.parcelsearchDatatable.Columns.Add("ParcelID");
                }

                ////To get a Multiple or single parcel Id(s)
                DataGridViewSelectedRowCollection selectedRows = this.ParcelSearchDataGridView.SelectedRows;
                foreach (DataGridViewRow dataRow in selectedRows)
                {
                    if (!string.IsNullOrEmpty(dataRow.Cells["ParcelSearchID"].Value.ToString().Trim()))
                    {
                        this.parcelsearchdatarow = this.parcelsearchDatatable.NewRow();
                        this.parcelsearchdatarow["ParcelID"] = dataRow.Cells["ParcelSearchID"].Value.ToString().Trim();
                        this.parcelsearchDatatable.Rows.Add(this.parcelsearchdatarow);
                    }
                }

                ////for (int i = 0; i <= this.ParcelSearchDataGridView.OriginalRowCount - 1; i++)
                ////{
                ////    if (this.ParcelSearchDataGridView.Rows[i].Selected)
                ////    {
                ////        if (!string.IsNullOrEmpty(this.ParcelSearchDataGridView.Rows[i].Cells["ParcelSearchID"].Value.ToString().Trim()))
                ////        {
                ////            this.parcelsearchdatarow = this.parcelsearchDatatable.NewRow();
                ////            this.parcelsearchdatarow["ParcelID"] = this.ParcelSearchDataGridView.Rows[i].Cells["ParcelSearchID"].Value.ToString().Trim();
                ////            this.parcelsearchDatatable.Rows.Add(this.parcelsearchdatarow);
                ////        }
                ////    }
                ////}

                this.commandResult = TerraScanCommon.GetXmlString(this.parcelsearchDatatable);
                this.DialogResult = DialogResult.OK;
                this.Close();
                //if (this.ParcelSearchDataGridView.CurrentRow != null)
                //{
                //    if (!string.IsNullOrEmpty(this.ParcelSearchDataGridView.Rows[this.ParcelSearchDataGridView.CurrentRowIndex].Cells["ParcelSearchID"].Value.ToString()))
                //    {
                //        int.TryParse(this.ParcelSearchDataGridView.Rows[this.ParcelSearchDataGridView.CurrentRowIndex].Cells["ParcelSearchID"].Value.ToString(), out this.parcelId);

                //        this.commandResult = this.parcelId.ToString();
                //        this.DialogResult = DialogResult.OK;
                //        this.Close();
                //    }
                //}
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
            searchValueDataTable = (DataTable)this.parcelSectionDataSet.ParcelSearchDataTable.Clone();

            DataRow searchRow;
            searchRow = searchValueDataTable.NewRow();

            searchRow[this.parcelSectionDataSet.ParcelSearchDataTable.ParcelNumberColumn.ColumnName] = this.ParcelNumberTextBox.Text.Trim();
            searchRow[this.parcelSectionDataSet.ParcelSearchDataTable.StatementNumberColumn.ColumnName] = this.StatementNumberTextBox.Text.Trim();
            searchRow[this.parcelSectionDataSet.ParcelSearchDataTable.OwnerNameColumn.ColumnName] = this.OwnerTextBox.Text.Trim();
            int.TryParse(this.RollYearTextBox.Text.Trim(), out rollYearSearch);
            searchRow[this.parcelSectionDataSet.ParcelSearchDataTable.RollYearColumn.ColumnName] = rollYearSearch;
            searchRow[this.parcelSectionDataSet.ParcelSearchDataTable.SitusColumn.ColumnName] = this.SitusTextBox.Text.Trim();
            searchRow[this.parcelSectionDataSet.ParcelSearchDataTable.LegalColumn.ColumnName] = this.LegalTextBox.Text.Trim();
            searchRow[this.parcelSectionDataSet.ParcelSearchDataTable.DistrictColumn.ColumnName] = this.DistrictTextBox.Text.Trim();
            searchRow[this.parcelSectionDataSet.ParcelSearchDataTable.NeighborhoodColumn.ColumnName] = this.NeighborhoodTextBox.Text.Trim();
            searchRow[this.parcelSectionDataSet.ParcelSearchDataTable.StateCodeColumn.ColumnName] = this.DorTextBox.Text.Trim();
            searchRow[this.parcelSectionDataSet.ParcelSearchDataTable.ID1Column.ColumnName] = this.ID1TextBox.Text.Trim();
            searchRow[this.parcelSectionDataSet.ParcelSearchDataTable.ID2Column.ColumnName] = this.ID2TextBox.Text.Trim();
            searchRow[this.parcelSectionDataSet.ParcelSearchDataTable.ID3Column.ColumnName] = this.ID3TextBox.Text.Trim();
            searchRow[this.parcelSectionDataSet.ParcelSearchDataTable.ID4Column.ColumnName] = this.ID4TextBox.Text.Trim();
            searchRow[this.parcelSectionDataSet.ParcelSearchDataTable.ID5Column.ColumnName] = this.ID5TextBox.Text.Trim();
            searchRow[SharedFunctions.GetResourceString("ParcelTypeIDColumnName")] = this.ParcelTypeComboBox.SelectedValue;

            searchValueDataTable.Rows.Add(searchRow);

            searchValueXml = TerraScanCommon.GetXmlString(searchValueDataTable);
            return searchValueXml;
        }

        /// <summary>
        /// Grids the header change.
        /// </summary>
        private void GridHeaderChange()
        {
            if (this.parcelSectionDataSet.ParcelLabelDataTable.Rows.Count > 0)
            {
                // Label Header
                this.ID1Label.Text = this.parcelSectionDataSet.ParcelLabelDataTable.Rows[0][this.parcelSectionDataSet.ParcelSearchDataTable.ID1Column.ColumnName].ToString() + ":";
                this.ID2Label.Text = this.parcelSectionDataSet.ParcelLabelDataTable.Rows[0][this.parcelSectionDataSet.ParcelSearchDataTable.ID2Column.ColumnName].ToString() + ":";
                this.ID3Label.Text = this.parcelSectionDataSet.ParcelLabelDataTable.Rows[0][this.parcelSectionDataSet.ParcelSearchDataTable.ID3Column.ColumnName].ToString() + ":";
                this.ID4Label.Text = this.parcelSectionDataSet.ParcelLabelDataTable.Rows[0][this.parcelSectionDataSet.ParcelSearchDataTable.ID4Column.ColumnName].ToString() + ":";
                this.ID5Label.Text = this.parcelSectionDataSet.ParcelLabelDataTable.Rows[0][this.parcelSectionDataSet.ParcelSearchDataTable.ID5Column.ColumnName].ToString() + ":";

                // Grid Header
                this.ParcelSearchDataGridView.Columns[this.parcelSectionDataSet.ParcelSearchDataTable.ID1Column.ColumnName].HeaderText = this.parcelSectionDataSet.ParcelLabelDataTable.Rows[0][this.parcelSectionDataSet.ParcelSearchDataTable.ID1Column.ColumnName].ToString();
                this.ParcelSearchDataGridView.Columns[this.parcelSectionDataSet.ParcelSearchDataTable.ID2Column.ColumnName].HeaderText = this.parcelSectionDataSet.ParcelLabelDataTable.Rows[0][this.parcelSectionDataSet.ParcelSearchDataTable.ID2Column.ColumnName].ToString();
                this.ParcelSearchDataGridView.Columns[this.parcelSectionDataSet.ParcelSearchDataTable.ID3Column.ColumnName].HeaderText = this.parcelSectionDataSet.ParcelLabelDataTable.Rows[0][this.parcelSectionDataSet.ParcelSearchDataTable.ID3Column.ColumnName].ToString();
                this.ParcelSearchDataGridView.Columns[this.parcelSectionDataSet.ParcelSearchDataTable.ID4Column.ColumnName].HeaderText = this.parcelSectionDataSet.ParcelLabelDataTable.Rows[0][this.parcelSectionDataSet.ParcelSearchDataTable.ID4Column.ColumnName].ToString();
                this.ParcelSearchDataGridView.Columns[this.parcelSectionDataSet.ParcelSearchDataTable.ID5Column.ColumnName].HeaderText = this.parcelSectionDataSet.ParcelLabelDataTable.Rows[0][this.parcelSectionDataSet.ParcelSearchDataTable.ID5Column.ColumnName].ToString();
            }
        }

        #endregion

        #region Events

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
                    if (this.parcelSectionDataSet.ParcelSearchDataTable.Rows.Count <= 0)
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
        /// Handles the SelectionChangeCommitted event of the ParcelTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.EnableSearchButton();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Load event of the F1403 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1403_Load(object sender, EventArgs e)
        {
            try
            {
                // Populate ParcelType Combo
                if (this.callingForm==0)
                {
                    this.parcelSectionDataSet = this.form1403Control.WorkItem.F1403_GetParcelType(null);
                    //this.parcelSectionDataSet = this.form1403Control.WorkItem.F1403_GetParcelType(null,null);
                }
                else
                {
                    this.parcelSectionDataSet = this.form1403Control.WorkItem.F1403_GetParcelType(this.callingForm);
                }
                this.SearchButton.Enabled = true;
                /* Modified to change the Rollyear Field */
                this.ClearButton.Enabled = false;

                this.ParcelCancelButton.Enabled = true;
                if (this.parcelSectionDataSet != null)
                {
                    //Modifed to load SaleTraking Rollyear by purushotham
                    if(this.callingForm>0 && this.callingForm == 24550)
                    {
                        this.RollYearTextBox.Text = this.rollYear.ToString();
                        /* Modified to change the Rollyear Field */
                        this.RollYearTextBox.Enabled = false;
                        this.ClearButton.Enabled = false;
                    }
                    else
                    {
                        if (this.parcelSectionDataSet.ParcelRollYearDataTable.Rows.Count > 0)
                        {
                            this.RollYearTextBox.Text = this.parcelSectionDataSet.ParcelRollYearDataTable.Rows[0][0].ToString();
                        }
                    }
                    //if (this.parcelSectionDataSet.ParcelRollYearDataTable.Rows.Count > 0)
                    //{
                    //    this.RollYearTextBox.Text = this.parcelSectionDataSet.ParcelRollYearDataTable.Rows[0][0].ToString();  
                    //}

                    if (this.parcelSectionDataSet.ParcelTypeDataTable.Rows.Count > 0)
                    {
                        this.ParcelTypeComboBox.DataSource = this.parcelSectionDataSet.ParcelTypeDataTable;
                        this.ParcelTypeComboBox.DisplayMember = this.parcelSectionDataSet.ParcelTypeDataTable.ParcelTypeColumn.ColumnName;
                        this.ParcelTypeComboBox.ValueMember = this.parcelSectionDataSet.ParcelTypeDataTable.ParcelTypeIDColumn.ColumnName;
                    }
                }
                //this.RollYearTextBox.Enabled = false;
                this.CustomizeDataGrid();
                this.FormLoad();
                this.GridHeaderChange();
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
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, EventArgs e)
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
                if (this.parcelSectionDataSet != null)
                {
                    /* Modified to change the Rollyear Field */
                    if (this.callingForm > 0 && this.callingForm == 24550)
                    {
                        this.RollYearTextBox.Enabled = false;
                    }
                    else
                    {
                        this.RollYearTextBox.Text = string.Empty;
                    }
                }
                //this.RollYearTextBox.Text = string.Empty;
                this.RecordCountLabel.Text = string.Empty;
                /* Modified to change the Rollyear Field */
                this.ClearButton.Enabled = false;  
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

                    this.parcelSectionDataSet.ParcelSearchDataTable.Clear();
                    this.Cursor = Cursors.WaitCursor;  
                    this.parcelSectionDataSet.Merge(this.form1403Control.WorkItem.F1403_GetSearchResult(this.GetSearchXml()));
                    this.ParcelSearchDataGridView.DataSource = this.parcelSectionDataSet.ParcelSearchDataTable.DefaultView;
                    this.Cursor = Cursors.Default;  
                    this.RecordCountLabel.Text = this.ParcelSearchDataGridView.OriginalRowCount + SharedFunctions.GetResourceString("RecordsCount");

                    if (this.ParcelSearchDataGridView.OriginalRowCount > 0)
                    {
                        this.ParcelSearchDataGridView.Rows[0].Selected = true;
                        this.EnableSearchButton();
                        this.ParcelAcceptButton.Enabled = true;
                        this.ParcelSearchDataGridView.Enabled = true;
                        this.ParcelSearchDataGridView.FirstDisplayedScrollingColumnIndex = 0;                        
                    }
                    else
                    {
                        this.ParcelAcceptButton.Enabled = false;
                        this.ParcelSearchDataGridView.Enabled = false;
                        this.ParcelSearchDataGridView.FirstDisplayedScrollingColumnIndex = 0;        
                    }

                    this.ParcelSearchDataGridView.Rows[0].Selected = true;
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

        /// <summary>
        /// Handles the Click event of the AcceptButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AcceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.GetParcelID();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
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
                        this.GetParcelID();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion
    }
}
