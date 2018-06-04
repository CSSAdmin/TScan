//--------------------------------------------------------------------------------------------
// <copyright file="F1015.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the MortgageImportTemplateSelect
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                              	    
// 19 May 06        Shiva
// 16 June 06       Guhan                Move to separate project
// 04 July 06       Jyothi               Removed empty GridView and placed GridView user control
// 02 Aug 06        Vinoth               CAB Refactoring
//*********************************************************************************/

namespace D1010
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Helper;
    using TerraScan.Common;
    using System.Web.Services.Protocols;
    using System.Reflection;
    using TerraScan.Utilities;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities; 

    /// <summary>
    /// F1015 Form class
    /// </summary>
    public partial class F1015 : Form
    {
        #region Private Variables

        /// <summary>
        /// DataSet Holds the MortgageImportTemplate Details
        /// </summary>
        private MortgageImportTemplateSelectData mortgageImportTemplateDetailsDataSet;

        /// <summary>
        /// Template Id
        /// </summary>
        private int mortgageImportTemplateId;

        /// <summary>
        /// Controller F1015Controller
        /// </summary>
        private F1015Controller form1015Control;

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MortgageImportTemplateSelect class.
        /// </summary>
        public F1015()
        {
            this.InitializeComponent();
            this.mortgageImportTemplateDetailsDataSet = new MortgageImportTemplateSelectData();
            this.mortgageImportTemplateId = -1;
            ////TerraScan.Common.TerraScanCommon.SetSameProperty(this.MortgageImportTemplateSelectionGridView, this.MortgageImportTemplateSelectionEmptyGridView, 6);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Property MortgageImportTemplateId 
        /// </summary>
        public int MortgageImportTemplateId
        {
            set
            {
                this.mortgageImportTemplateId = value;
            }

            get
            {
                return this.mortgageImportTemplateId;
            }
        }

        /// <summary>
        /// Created Property for F1015Controller
        /// </summary>
        [CreateNew]
        public F1015Controller F1015Control
        {
            get { return this.form1015Control as F1015Controller; }
            set { this.form1015Control = value; }
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

        #region Private Methods

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
        /// Method to Get the Details for MortgageImportTemplate
        /// </summary>
        private void LoadMortgageImportTemplateSelectionGridView()
        {
            try
            {              
                this.mortgageImportTemplateDetailsDataSet = this.form1015Control.WorkItem.GetMortgageImportTemplateDetails;
            }
            catch (SoapException ex)
            {
                //////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }

            this.MortgageImportTemplateSelectionGridView.EnableHeadersVisualStyles = true;
            if (F1015.CheckValidDataSet(this.mortgageImportTemplateDetailsDataSet))
            {
                this.MortgageImportTemplateSelectionGridView.Enabled = true;
                this.CustomizeDataGrid();                

                this.MortgageImportTemplateSelectionGridView.DataSource = this.mortgageImportTemplateDetailsDataSet.ListMortgageImportTemplate;
                ////TerraScan.Common.TerraScanCommon.SetGridHeight(this.MortgageImportTemplateSelectionGridView, 6);
                if (this.mortgageImportTemplateDetailsDataSet.ListMortgageImportTemplate.Rows.Count > 0)
                {
                    this.MortgageImportTemplateSelectionGridView.Focus();
                    if (this.MortgageImportTemplateSelectionGridView.CurrentCell != null)
                    {
                        this.MortgageImportTemplateSelectionGridView.CurrentRow.Selected = true;
                    }

                    this.AcceptButton.Enabled = true;
                }
                else
                {
                    this.MortgageImportTemplateSelectionGridView.CurrentCell = null;
                    this.AcceptButton.Enabled = false;
                    this.MortgageImportTemplateSelectionGridView.Enabled = false;
                }

                if (this.mortgageImportTemplateDetailsDataSet.ListMortgageImportTemplate.Rows.Count > 6)
                {
                    this.ImportTempSelectionVScrollBar.Visible = false;
                }
                else
                {
                    this.ImportTempSelectionVScrollBar.Visible = true;
                }
            }
            else
            {
                this.CustomizeDataGrid();
                ////this.CustomizeEmptyDataGrid();
                this.AcceptButton.Enabled = false;
                this.MortgageImportTemplateSelectionGridView.Enabled = false;
            }
        }

        /// <summary>
        /// This Method used to  Allign The Header width , Column width , And Font Size for 
        /// MortgageImportTemplateSelectionGridView
        /// </summary>
        private void CustomizeDataGrid()
        {
            //// TODO All styles Should be take from config file
            this.MortgageImportTemplateSelectionGridView.AllowUserToResizeColumns = false;
            this.MortgageImportTemplateSelectionGridView.AutoGenerateColumns = false;
            this.MortgageImportTemplateSelectionGridView.AllowUserToResizeRows = false;
            ////this.MortgageImportTemplateSelectionGridView.StandardTab = true;
            this.MortgageImportTemplateSelectionGridView.EnableHeadersVisualStyles = false;
            DataGridViewColumnCollection columns = this.MortgageImportTemplateSelectionGridView.Columns;
            columns["TemplateId"].DataPropertyName = "TemplateId";
            columns["TemplateName"].DataPropertyName = "TemplateName";
            columns["FileType"].DataPropertyName = "TypeName";
            columns["TemplateId"].DisplayIndex = 0;
            columns["TemplateName"].DisplayIndex = 1;
            columns["FileType"].DisplayIndex = 2;
            columns["TemplateId"].Width = 90;
            columns["TemplateName"].Width = 240;
            columns["FileType"].Width = 128;
        }

        /// <summary>
        /// Method to Populate the Current Record and Close the Form
        /// </summary>
        private void PopulateRecord()
        {
            try
            {
                int rowIndex = -1;
                if (this.MortgageImportTemplateSelectionGridView.Rows.Count > 0)
                {
                    rowIndex = this.MortgageImportTemplateSelectionGridView.CurrentRow.Index;
                    this.MortgageImportTemplateSelectionGridView.CurrentRow.Selected = true;
                }

                if (rowIndex >= 0)
                {
                    if (!string.IsNullOrEmpty(this.MortgageImportTemplateSelectionGridView.Rows[rowIndex].Cells["TemplateId"].Value.ToString()))
                    {
                        this.MortgageImportTemplateId = Convert.ToInt32(this.MortgageImportTemplateSelectionGridView.Rows[rowIndex].Cells["TemplateId"].Value.ToString());
                        this.commandResult = this.MortgageImportTemplateId.ToString();
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
        /// Handles the Cell Double Click 
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event arguments</param>
        private void MortgageImportTemplateSelectionGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (!string.IsNullOrEmpty(this.MortgageImportTemplateSelectionGridView.Rows[e.RowIndex].Cells["TemplateId"].Value.ToString()))
                    {
                        this.MortgageImportTemplateId = Convert.ToInt32(this.MortgageImportTemplateSelectionGridView.Rows[e.RowIndex].Cells["TemplateId"].Value.ToString());
                        this.commandResult = this.MortgageImportTemplateId.ToString();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }

        /// <summary>
        /// Handles the Click Event for Accept Button
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void AcceptButton_Click(object sender, EventArgs e)
        {
            this.PopulateRecord();
        }

        /// <summary>
        /// Hanles the Activate Event for the Form
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void MortgageImportTemplateSelect_Activated(object sender, EventArgs e)
        {
            this.LoadMortgageImportTemplateSelectionGridView();
        }

        /// <summary>
        /// Handles the Load Event for the Form
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void MortgageImportTemplateSelect_Load(object sender, EventArgs e)
        {
            this.LoadMortgageImportTemplateSelectionGridView();
            this.CancelButton = this.CloseButton;
        }

        /// <summary>
        /// Handles the Key Down Event For the GridView
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">KeyEventArgs</param>
        private void MortgageImportTemplateSelectionGridView_KeyDown(object sender, KeyEventArgs e)
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
        private void MortgageImportTemplateFormLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
        }

        #endregion
    }
}