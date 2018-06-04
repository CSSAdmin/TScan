//--------------------------------------------------------------------------------------------
// <copyright file="F3510.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F3510.cs.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 6/12/2007       R.Malliga            Created// 
//*********************************************************************************/

namespace D35100
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
    /// Class file for F3510
    /// </summary>
    public partial class F3510 : Form
    {
        #region Variables
          /// <summary>
        /// controller F3510Controller
        /// </summary>
        private F3510Controller form3510Control;

        /// <summary>
        /// Used to store the no of rows in the grid
        /// </summary>
        private int neighborhoodSelectionGridRowCount;

        /// <summary>
        /// NeighborhoodID
        /// </summary>
        private string neighborhoodId;

        /// <summary>
        /// NeighborhoodName
        /// </summary>
        private string neighborhoodName;

        /// <summary>
        /// RollYear
        /// </summary>
        private string rollYear;
                
        /// <summary>
        /// F3510NeighborhoodSelectionData
        /// </summary>
        private F3510NeighborhoodSelectionData neighborhoodSelectionData = new F3510NeighborhoodSelectionData();

        /// <summary>
        /// F3510NeighborhoodTypeData
        /// </summary>
        private F3510NeighborhoodSelectionData neighborhoodTypeData = new F3510NeighborhoodSelectionData();

        /// <summary>
        /// Property RollYear
        /// </summary>
        private string prollYear;

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        #endregion
        
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1513"/> class.
        /// </summary>
        public F3510()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F3510"/> class.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        public F3510(string rollYear)
        {
            this.InitializeComponent();
            this.rollYear = rollYear;
            if (!string.IsNullOrEmpty(rollYear))
            {
                this.RollYearTextBox.Text = rollYear.ToString();
                this.RollYearTextBox.Enabled = false;
            }
            else
            {
                this.RollYearTextBox.Text = string.Empty;
                this.RollYearTextBox.Enabled = true;
            }
        }
       #endregion Constructor

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// For F3510Control
        /// </summary>
        [CreateNew]
        public F3510Controller Form3510Control
        {
            get { return this.form3510Control as F3510Controller; }
            set { this.form3510Control = value; }
        }

        /// <summary>
        /// Gets or sets the neighborhoodID.
        /// </summary>
        /// <value>NeighboorhoodID Value.</value>
        public string NeighborId
        {
            get { return this.neighborhoodId; }
            set { this.neighborhoodId = value; }
        }

        /// <summary>
        /// Gets or sets the neighborhood.
        /// </summary>
        /// <value>Neighboorhood Value.</value>
        public string NeighborName
        {
            get { return this.neighborhoodName; }
            set { this.neighborhoodName = value; }
        }

        /// <summary>
        /// Gets or sets the RollYear.
        /// </summary>
        /// <value>RollYear Value.</value>
        public string RollYear
        {
            get { return this.prollYear; }
            set { this.prollYear = value; }
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
        
        #region Button Events

        /// <summary>
        /// Handles the Click event of the NeighborhoodAcceptButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NeighborhoodAcceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.neighborhoodSelectionGridRowCount  > 0)
                {
                    this.SelectNeighborhoodId();
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the NeighborhoodSearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NeighborhoodSearchButton_Click(object sender, EventArgs e)
        {
            try
            {
              this.LoadNeighborhoodSelectionGrid(this.NeighborhoodTextBox.Text.Trim(), this.ChildOfTextBox.Text.Trim(), this.RollYearTextBox.Text.Trim(), this.TypeComboBox.Text.Trim(), this.DescriptionTextBox.Text.Trim());
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the NeighborhoodClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NeighborhoodClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ClearNeighborhoodSelection();
                this.ClearNeighborhoodselectionGrid();
                this.DisableButtons();
                this.NeighborhoodTextBox.Focus();
                this.DialogResult = DialogResult.None;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the NeighborhoodCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NeighborhoodCancelButton_Click(object sender, EventArgs e)
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

        #endregion

        #region LinkLabel Events

        /// <summary>
        /// Handles the LinkClicked event of the NeighborhoodLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void NeighborhoodLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo = TerraScanCommon.GetFormInfo(30100);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
      
        /// <summary>
        /// Handles the PreviewKeyDown event of the NeighborhoodLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void NeighborhoodLinkLabel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyValue.Equals((int)Keys.Up) || e.KeyValue.Equals((int)Keys.Down) || e.KeyValue.Equals((int)Keys.Left) || e.KeyValue.Equals((int)Keys.Right))
                {
                    if (NeighborhoodDataGridView.OriginalRowCount > 0)
                    {
                        this.NeighborhoodDataGridView.Focus();
                    }
                    else
                    {
                        this.NeighborhoodTextBox.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        #region Grid Events

        /// <summary>
        /// Handles the CellDoubleClick event of the NeighborhoodDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void NeighborhoodDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.NeighborhoodAcceptButton.Enabled)
                {
                  this.SelectNeighborhoodId();
                  this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        
        /// <summary>
        /// Handles the RowHeaderMouseDoubleClick event of the NeighborhoodDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void NeighborhoodDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            { 
                if (e.RowIndex >= 0 && this.NeighborhoodAcceptButton.Enabled)
                {
                    this.SelectNeighborhoodId();
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        #region Form Load
        /// <summary>
        /// Handles the Load event of the F3510 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F3510_Load(object sender, EventArgs e)
        {
            try
            {
                this.ClearNeighborhoodSelection();
               this.CustomizeNeighborhoodSelectionGrid();
               this.NeighborhoodDataGridView.DataSource = null;
               this.NeighborhoodDataGridView.Enabled = false; 
               this.NeighborhoodDataGridView.Rows[0].Selected = false;

                ////For Combox Box - Neighborhood Type
                this.neighborhoodTypeData = this.form3510Control.WorkItem.F3510_ListNeighborhoodType();
                this.TypeComboBox.DataSource = this.neighborhoodTypeData.GetNeighborhoodType;
                this.TypeComboBox.DisplayMember = this.neighborhoodTypeData.GetNeighborhoodType.TypeColumn.ColumnName;
                this.TypeComboBox.ValueMember = this.neighborhoodTypeData.GetNeighborhoodType.NBHDTypeColumn.ColumnName;
                this.TypeComboBox.SelectedIndex = -1;  

                this.CustomizeNeighborhoodSelectionGrid();
                this.DisableButtons();

                // If condition has been added for avoid excepion while opening through query update form
                if (this.rollYear != null)
                {
                    this.RollYearTextBox.Text = this.rollYear;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Custimizes the Neighborhood selection grid.
        /// </summary>
        private void CustomizeNeighborhoodSelectionGrid()
        {
            this.NeighborhoodDataGridView.AutoGenerateColumns = false;

            DataGridViewColumnCollection columns = this.NeighborhoodDataGridView.Columns;

            columns[this.neighborhoodSelectionData.GetNeighborhoodSelection.NBHDIDColumn.ColumnName].DataPropertyName = this.neighborhoodSelectionData.GetNeighborhoodSelection.NBHDIDColumn.ColumnName;
            columns[this.neighborhoodSelectionData.GetNeighborhoodSelection.NeighborhoodColumn.ColumnName].DataPropertyName = this.neighborhoodSelectionData.GetNeighborhoodSelection.NeighborhoodColumn.ColumnName;
            columns[this.neighborhoodSelectionData.GetNeighborhoodSelection.ChildOfColumn.ColumnName].DataPropertyName = this.neighborhoodSelectionData.GetNeighborhoodSelection.ChildOfColumn.ColumnName;
            columns[this.neighborhoodSelectionData.GetNeighborhoodSelection.yearColumn.ColumnName].DataPropertyName = this.neighborhoodSelectionData.GetNeighborhoodSelection.yearColumn.ColumnName;
            columns[this.neighborhoodSelectionData.GetNeighborhoodSelection.TypeColumn.ColumnName].DataPropertyName = this.neighborhoodSelectionData.GetNeighborhoodSelection.TypeColumn.ColumnName;
            columns[this.neighborhoodSelectionData.GetNeighborhoodSelection.NBHDTypeColumn.ColumnName].DataPropertyName = this.neighborhoodSelectionData.GetNeighborhoodSelection.NBHDTypeColumn.ColumnName;
            columns[this.neighborhoodSelectionData.GetNeighborhoodSelection.DescriptionColumn.ColumnName].DataPropertyName = this.neighborhoodSelectionData.GetNeighborhoodSelection.DescriptionColumn.ColumnName;

            columns[this.neighborhoodSelectionData.GetNeighborhoodSelection.NBHDIDColumn.ColumnName].DisplayIndex = 0;
            columns[this.neighborhoodSelectionData.GetNeighborhoodSelection.NeighborhoodColumn.ColumnName].DisplayIndex = 1;
            columns[this.neighborhoodSelectionData.GetNeighborhoodSelection.ChildOfColumn.ColumnName].DisplayIndex = 2;
            columns[this.neighborhoodSelectionData.GetNeighborhoodSelection.yearColumn.ColumnName].DisplayIndex = 3;
            columns[this.neighborhoodSelectionData.GetNeighborhoodSelection.TypeColumn.ColumnName].DisplayIndex = 4;
            columns[this.neighborhoodSelectionData.GetNeighborhoodSelection.NBHDTypeColumn.ColumnName].DisplayIndex = 5;
            columns[this.neighborhoodSelectionData.GetNeighborhoodSelection.DescriptionColumn.ColumnName].DisplayIndex = 6;
         }
               
        /// <summary>
        /// Clears the Neighborhood selection DataGridView
        /// </summary>
        private void ClearNeighborhoodselectionGrid()
        {
            this.NeighborhoodDataGridView.Enabled = false;
            this.neighborhoodSelectionData.GetNeighborhoodSelection.Rows.Clear();
            this.NeighborhoodDataGridView.DataSource = this.neighborhoodSelectionData.GetNeighborhoodSelection;
            this.NeighborhoodDataGridView.Rows[0].Selected = false;
            this.NeighborhoodSelectionVerticalScroll.Visible = true;
            this.NeighborhoodAcceptButton.Enabled = false;
        }
        
        /// <summary>
        /// Clears the FundTextBox,DescTextBox and RecordCountLabel
        /// </summary>
        private void ClearNeighborhoodSelection()
        {
            this.NeighborhoodTextBox.Text = string.Empty;
            this.ChildOfTextBox.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.TypeComboBox.SelectedIndex = -1;
            this.DescriptionTextBox.Text = string.Empty;
            this.RecordCountLabel.Text = string.Empty;
        }

        /// <summary>
        /// To disable the accept,search, and clear buttons
        /// </summary>        
        private void DisableButtons()
        {
            this.NeighborhoodSearchButton.Enabled = false;
            this.NeighborhoodAcceptButton.Enabled = false;
            this.NeighborhoodClearButton.Enabled = false;
        }

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        /// <returns>integer value of grid row index</returns>
        private int GetRowIndex()
        {
            try
            {
                if (this.neighborhoodSelectionGridRowCount > 0)
                {
                    if (this.NeighborhoodDataGridView.CurrentCell != null)
                    {
                        return this.NeighborhoodDataGridView.CurrentCell.RowIndex;
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

        /// <summary>
        /// Loads the NeighborhoodSelectionDataGridView
        /// </summary>
        /// <param name="neighborhood">The neighborhood value form NeighborhoodTextBox</param>
        /// <param name="childof">The childof form childoftextBox </param>
        /// <param name="year">The year form childoftextBox </param>
        /// <param name="type">The type form typecomboBox </param>
        /// <param name="description">The description form descriptiontextBox </param>
        private void LoadNeighborhoodSelectionGrid(string neighborhood, string childof, string year, string type, string description)
        {
            ////For Grid
            this.neighborhoodSelectionData = this.form3510Control.WorkItem.F3510_ListNeighborhoodSelectionDetails(neighborhood, childof, year, type, description);
            this.neighborhoodSelectionGridRowCount = this.neighborhoodSelectionData.GetNeighborhoodSelection.Rows.Count;

            if (this.neighborhoodSelectionGridRowCount > 0)
            {
                this.NeighborhoodDataGridView.Enabled = true;

                this.NeighborhoodDataGridView.DataSource = this.neighborhoodSelectionData.GetNeighborhoodSelection;
                this.NeighborhoodDataGridView.Focus();
                this.NeighborhoodDataGridView.Rows[0].Selected = true;

                this.NeighborhoodAcceptButton.Enabled = true;
            }
            else
            {
                this.ClearNeighborhoodselectionGrid();
            }

            ////to enable or disable the vertical scroll bar
            if (this.neighborhoodSelectionGridRowCount > this.NeighborhoodDataGridView.NumRowsVisible)
            {
                this.NeighborhoodSelectionVerticalScroll.Visible = false;
            }
            else
            {
                this.NeighborhoodSelectionVerticalScroll.Visible = true;
            }

            ////To display the no of display rows in the grid
            this.RecordCountLabel.Text = this.neighborhoodSelectionGridRowCount + SharedFunctions.GetResourceString("9101MasterNameSearch");
        }
        
        /// <summary>
        /// Enables the search button.
        /// </summary>
        private void EnableSearchButton()
        {
            if (!string.IsNullOrEmpty(this.NeighborhoodTextBox.Text.Trim().Replace("'", "''")) || !string.IsNullOrEmpty(this.ChildOfTextBox.Text.Trim().Replace("'", "''")) || !string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim().Replace("'", "''")) || !string.IsNullOrEmpty(this.TypeComboBox.Text.Trim().Replace("'", "''")) || !string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim().Replace("'", "''")))
            {
                this.NeighborhoodSearchButton.Enabled = true;
                this.NeighborhoodClearButton.Enabled = true;
            }
            else
            {
                this.NeighborhoodSearchButton.Enabled = false;

                if (this.neighborhoodSelectionGridRowCount <= 0)
                {
                    this.NeighborhoodAcceptButton.Enabled = false;
                    this.NeighborhoodClearButton.Enabled = false;
                }
                else
                {
                    this.NeighborhoodAcceptButton.Enabled = true;
                    this.NeighborhoodClearButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Selects the NeighborhoodID from the Neighborhood Selection grid.
        /// </summary>
        private void SelectNeighborhoodId()
        {
            int rowId = 0;

            ////To get the Row index for Fund Neighborhood DataGridView
            rowId = this.GetRowIndex();

            if (this.neighborhoodSelectionGridRowCount  > 0 && rowId >= 0)
            {
                if (!string.IsNullOrEmpty(this.NeighborhoodDataGridView.Rows[rowId].Cells[this.neighborhoodSelectionData.GetNeighborhoodSelection.NBHDIDColumn.ColumnName].Value.ToString()))
                {
                    this.neighborhoodId = this.NeighborhoodDataGridView.Rows[rowId].Cells[this.neighborhoodSelectionData.GetNeighborhoodSelection.NBHDIDColumn.ColumnName].Value.ToString();
                    this.commandResult = this.neighborhoodId;
                }

                if (!string.IsNullOrEmpty(this.NeighborhoodDataGridView.Rows[rowId].Cells[this.neighborhoodSelectionData.GetNeighborhoodSelection.NeighborhoodColumn.ColumnName].Value.ToString()))
                {
                    this.neighborhoodName = this.NeighborhoodDataGridView.Rows[rowId].Cells[this.neighborhoodSelectionData.GetNeighborhoodSelection.NeighborhoodColumn.ColumnName].Value.ToString();
                }

                if (!string.IsNullOrEmpty(this.NeighborhoodDataGridView.Rows[rowId].Cells[this.neighborhoodSelectionData.GetNeighborhoodSelection.yearColumn.ColumnName].Value.ToString()))
                {
                    this.prollYear = this.NeighborhoodDataGridView.Rows[rowId].Cells[this.neighborhoodSelectionData.GetNeighborhoodSelection.yearColumn.ColumnName].Value.ToString();
                }

                this.DialogResult = DialogResult.OK;

                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }
        }
        
        /// <summary>
        /// Editexts the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NeighborhoodTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(this.NeighborhoodTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.ChildOfTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.TypeComboBox.Text.Trim())) || (!string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim())))
                {
                    this.EnableSearchButton();
                }
                else
                {
                    if (this.neighborhoodSelectionGridRowCount  > 0)
                    {
                        this.NeighborhoodSearchButton.Enabled = false;
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

   #endregion
  }
}