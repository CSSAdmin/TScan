//--------------------------------------------------------------------------------------------
// <copyright file="F1401.cs" company="Congruent">
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
// 14 Aug 07		karthikeyan V	            Created
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
    /// F1401
    /// </summary>
    public partial class F1401 : BasePage
    {
        #region Variable

        /// <summary>
        /// Created Instance for F1401Controller
        /// </summary>
        private F1401Controller form1401Control;

        /// <summary>
        /// parcelId
        /// </summary>
        private int parcelId;

        /// <summary>
        /// rollYear
        /// </summary>
        private int rollYear;

        /// <summary>
        ///callingForm 
        /// </summary>
        private int callingForm;

        /// <summary>
        /// parcelType
        /// </summary>
        private int parcelType;

        /// <summary>
        /// parcelSectionDataSet
        /// </summary>
        private F1401ParcelSearch parcelSectionDataSet = new F1401ParcelSearch();

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        /// <summary>
        /// For Selected value
        /// </summary>
        private string commandValue = string.Empty;

        private string formNum = string.Empty;

        #endregion Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1401"/> class.
        /// </summary>
        public F1401()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1401"/> class.
        /// </summary>
        /// <param name="rollYearParameter">The roll year parameter.</param>
        public F1401(int rollYearParameter)
        {
            this.InitializeComponent();
            this.rollYear = rollYearParameter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1401"/> class.
        /// </summary>
        /// <param name="rollYearParameter">The roll year parameter.</param>
        /// <param name="parcelTypeParameter">The parcel type parameter.</param>
        public F1401(int rollYearParameter, int parcelTypeParameter)
        {
            this.InitializeComponent();
            this.rollYear = rollYearParameter;
            this.parcelType = parcelTypeParameter;
        }
        public F1401(int rollYearParameter, string masterFormNo)
        {           
                this.InitializeComponent();
                this.formNum = masterFormNo.ToString();
                this.rollYear = rollYearParameter;            
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the F1401 controll.
        /// </summary>
        /// <value>The F1401 controll.</value>
        [CreateNew]
        public F1401Controller F1401Controll
        {
            get { return this.form1401Control as F1401Controller; }
            set { this.form1401Control = value; }
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
        /// Gets or Sets the rollYear
        /// </summary>
        /// <value> Roll Year</value>
        public int RollYearId
        {
            get { return this.rollYear; }
            set { this.rollYear = value; }
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

            //modified to implement Readonly for Rollyear panel by priyadharshini
            if (!formNum.Equals("24510"))
            {
                this.RollYearTextBox.Text = string.Empty;
            }
            else
            {
                this.RollYearTextBox.Text = Convert.ToString(this.rollYear);
            }

            if (!formNum.Equals("10041"))
            {
                this.RollYearTextBox.Text = string.Empty;
            }
            else
            {
                this.RollYearTextBox.Text = Convert.ToString(this.rollYear);
            }

            //if (this.rollYear > 0)
            //{
            //    this.RollYearTextBox.Text = this.rollYear.ToString();
            //}
            //else
            //{
            //    this.RollYearTextBox.Text = string.Empty;
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
            this.ParcelTypeComboBox.SelectedIndex = 0;
            if (formNum.Equals("3602") || formNum.Equals("30000") || formNum.Equals("30050"))
            {
                this.RollYearTextBox.Text = Convert.ToString(this.rollYear);
            }
        }

        /// <summary>
        /// Forms the load.
        /// </summary>
        private void FormLoad()
        {
            this.ParcelSearchDataGridView.DataSource = null;
            this.ParcelSearchDataGridView.Enabled = false;
            this.ParcelSearchDataGridView.Rows[0].Selected = false;
            this.ClearFields();
            this.DisableButtons();
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
            //Modifed to implement #21450 CO by purushotham 
            if (this.rollYear > 0)
            {
                this.RollYearTextBox.Text = this.rollYear.ToString();
            }
            else
            {
                this.RollYearTextBox.Text = string.Empty;
            }

            if (this.callingForm > 0)
            {
                this.RollYearTextBox.Text = this.rollYear.ToString();
                this.RollYearTextBox.Enabled = false;
                this.ClearButton.Enabled = false;
            }

            //modified to implement Readonly for Rollyear panel by priyadharshini
            if (this.formNum.Equals("24510"))
            {
                if (!string.IsNullOrEmpty(formNum))
                {
                    this.RollYearTextBox.Enabled = false;
                    this.RollYearTextBox.LockKeyPress = true;
                }
                else
                {
                    this.RollYearTextBox.Enabled = true;
                    this.RollYearTextBox.LockKeyPress = false;
                }
            }

            //modified to implement Readonly for Rollyear panel by priyadharshini
            if (this.formNum.Equals("3602"))
            {
                if (!string.IsNullOrEmpty(formNum))
                {
                    this.RollYearTextBox.Enabled = false;
                    this.RollYearTextBox.LockKeyPress = true;
                    this.ClearButton.Enabled = false;
                }
                else
                {
                    this.RollYearTextBox.Enabled = true;
                    this.RollYearTextBox.LockKeyPress = false;
                    this.ClearButton.Enabled = true;
                }
            }

            if (this.formNum.Equals("30000"))
            {
                if (!string.IsNullOrEmpty(formNum))
                {
                    this.RollYearTextBox.Enabled = false;
                    this.RollYearTextBox.LockKeyPress = true;
                    this.ClearButton.Enabled = false;
                }
                else
                {
                    this.RollYearTextBox.Enabled = true;
                    this.RollYearTextBox.LockKeyPress = false;
                    this.ClearButton.Enabled = true;
                }
            }
            //modified to implement Readonly for Rollyear panel by priyadharshini
            if (this.formNum.Equals("10041"))
            {
                if (!string.IsNullOrEmpty(formNum))
                {
                    this.RollYearTextBox.Enabled = false;
                    this.RollYearTextBox.LockKeyPress = true;
                    this.ClearButton.Enabled = false;
                }
                else
                {
                    this.RollYearTextBox.Enabled = true;
                    this.RollYearTextBox.LockKeyPress = false;
                    this.ClearButton.Enabled = true;
                }
            }

            if (this.formNum.Equals("30050"))
            {
                if (!string.IsNullOrEmpty(formNum))
                {
                    this.RollYearTextBox.Enabled = false;
                    this.RollYearTextBox.LockKeyPress = true;
                    this.ClearButton.Enabled = false;
                }
                else
                {
                    this.RollYearTextBox.Enabled = true;
                    this.RollYearTextBox.LockKeyPress = false;
                    this.ClearButton.Enabled = true;
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
                if (this.ParcelSearchDataGridView.CurrentRow != null)
                {
                    if (!string.IsNullOrEmpty(this.ParcelSearchDataGridView.Rows[this.ParcelSearchDataGridView.CurrentRowIndex].Cells["ParcelSearchID"].Value.ToString()))
                    {
                        int.TryParse(this.ParcelSearchDataGridView.Rows[this.ParcelSearchDataGridView.CurrentRowIndex].Cells["ParcelSearchID"].Value.ToString(), out this.parcelId);
                        int.TryParse(this.ParcelSearchDataGridView.Rows[this.ParcelSearchDataGridView.CurrentRowIndex].Cells["RollYear"].Value.ToString(), out this.rollYear);         
                        this.commandResult = this.parcelId.ToString();

                        if (!string.IsNullOrEmpty(this.ParcelSearchDataGridView.Rows[this.ParcelSearchDataGridView.CurrentRowIndex].Cells["ParcelNumber"].Value.ToString()))
                        {
                            this.commandValue = this.ParcelSearchDataGridView.Rows[this.ParcelSearchDataGridView.CurrentRowIndex].Cells["ParcelNumber"].Value.ToString();
                        }
                        else
                        {
                            this.commandValue = string.Empty;
                        }

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
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
        /// Handles the Load event of the F1401 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1401_Load(object sender, EventArgs e)
        {
            try
            {
                // Populate ParcelType Combo
                this.parcelSectionDataSet = this.form1401Control.WorkItem.F1401_GetParcelType(null);
                this.SearchButton.Enabled = true;
                this.ParcelCancelButton.Enabled = true;
                if (this.parcelSectionDataSet != null)
                {
                    if (this.parcelSectionDataSet.ParcelTypeDataTable.Rows.Count > 0)
                    {
                        this.ParcelTypeComboBox.DataSource = this.parcelSectionDataSet.ParcelTypeDataTable;
                        this.ParcelTypeComboBox.DisplayMember = this.parcelSectionDataSet.ParcelTypeDataTable.ParcelTypeColumn.ColumnName;
                        this.ParcelTypeComboBox.ValueMember = this.parcelSectionDataSet.ParcelTypeDataTable.ParcelTypeIDColumn.ColumnName;
                    }
                }

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
                //Modifed to implement #21450 CO by purushotham 
                //this.FormLoad();
                /* Modified to change the Rollyear Field */
                if (!string.IsNullOrEmpty(formNum) && (this.formNum.Equals("3602") || this.formNum.Equals("10041")))
                {
                    this.RollYearTextBox.Enabled = false;
                }
                else
                {
                    this.RollYearTextBox.Text = string.Empty;
                }
                this.ParcelSearchDataGridView.DataSource = null;
                this.ParcelSearchDataGridView.Enabled = false;
                this.ParcelSearchDataGridView.Rows[0].Selected = false;
                this.ClearFields();
                this.DisableButtons();
                this.RecordCountLabel.Text = string.Empty;
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
                    this.parcelSectionDataSet.ParcelSearchDataTable.Clear();
                    this.parcelSectionDataSet.Merge(this.form1401Control.WorkItem.F1401_GetSearchResult(this.GetSearchXml()));

                    this.ParcelSearchDataGridView.DataSource = this.parcelSectionDataSet.ParcelSearchDataTable;
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