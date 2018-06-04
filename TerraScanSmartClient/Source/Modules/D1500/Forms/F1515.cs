//--------------------------------------------------------------------------------------------
// <copyright file="F1515.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1515.cs.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09/11/2006       M.Vijaya Kumar     Created// 
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
    /// Class file for F1515
    /// </summary>
    public partial class F1515 : Form
    {
        #region Variable

        /// <summary>
        /// controller F1515Controller
        /// </summary>
        private F1515Controller form1515Control;

        /// <summary>
        /// Instance for F1515FundSelectionData
        /// </summary>
        private F1515SubFundSelectionData subFundSelectionData = new F1515SubFundSelectionData();

        /// <summary>
        /// Used to store the no of rows in the grid
        /// </summary>
        private int subFundSelectionGridRowCount;

        /// <summary>
        /// Used to store the subFundId
        /// </summary>
        private int subFundId;

        /// <summary>
        /// Used to store the subFund
        /// </summary>
        private string subFundItem = string.Empty;

        /// <summary>
        /// Used to store the roll year
        /// </summary>
        private int rollYear;

        /// <summary>
        /// Used to store IsCash
        /// </summary>
        private int iscash;

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        #endregion Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1515"/> class.
        /// </summary>
        public F1515()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1515"/> class.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        public F1515(int rollYear)
        {
            InitializeComponent();
            this.rollYear = rollYear;
            this.iscash = -999;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1515"/> class.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="iscash">The iscash.</param>
        public F1515(int rollYear, int iscash)
        {
            InitializeComponent();
            this.rollYear = rollYear;
            this.iscash = iscash;
        }

        #endregion Constructor

        #region eventPublication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region Property

        /// <summary>
        /// For F1515Control
        /// </summary>
        [CreateNew]
        public F1515Controller Form1515Control
        {
            get { return this.form1515Control as F1515Controller; }
            set { this.form1515Control = value; }
        }

        /// <summary>
        /// Gets or sets the subFundId.
        /// </summary>
        /// <value>The SubFundId.</value>
        public int SubFundId
        {
            get { return this.subFundId; }
            set { this.subFundId = value; }
        }

        /// <summary>
        /// Gets or sets the SubFund.
        /// </summary>
        /// <value>The SubFund.</value>
        public string SubFundItem
        {
            get { return this.subFundItem; }
            set { this.subFundItem = value; }
        }

        /// <summary>
        /// Gets or sets the roll year value.
        /// </summary>
        /// <value>The roll year value.</value>
        public string RollYearValue
        {
            get { return this.RollYearTextBox.Text; }
            set { this.RollYearTextBox.Text = value; }
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
        /// Selects the SubFunDID from the account element mgmt grid.
        /// </summary>
        private void SelectSubFunDId()
        {
            int rowId = 0;
            int tempRollYear = 0;
            ////to get the current row id of the datagrid
            rowId = this.GetRowIndex();

            if (this.subFundSelectionGridRowCount > 0 && rowId >= 0)
            {
                

                if (!string.IsNullOrEmpty(this.SubFundSelectionDataGridView.Rows[rowId].Cells[this.subFundSelectionData.GetSubFundSelection.SubFundIDColumn.ColumnName].Value.ToString()))
                {
                    int.TryParse(this.SubFundSelectionDataGridView.Rows[rowId].Cells[this.subFundSelectionData.GetSubFundSelection.SubFundIDColumn.ColumnName].Value.ToString(), out this.subFundId);

                    this.commandResult = this.subFundId.ToString();
                }

                if (!string.IsNullOrEmpty(this.SubFundSelectionDataGridView.Rows[rowId].Cells[this.subFundSelectionData.GetSubFundSelection.SubFundColumn.ColumnName].Value.ToString()))
                {
                    this.subFundItem = this.SubFundSelectionDataGridView.Rows[rowId].Cells[this.subFundSelectionData.GetSubFundSelection.SubFundColumn.ColumnName].Value.ToString();
                }

                ////added code to send gridview RollYear
                if (!string.IsNullOrEmpty(this.SubFundSelectionDataGridView.Rows[rowId].Cells[this.subFundSelectionData.GetSubFundSelection.RollYearColumn.ColumnName].Value.ToString()))
                {
                    int.TryParse(this.SubFundSelectionDataGridView.Rows[rowId].Cells[this.subFundSelectionData.GetSubFundSelection.RollYearColumn.ColumnName].Value.ToString(), out tempRollYear);
                    this.RollYearTextBox.Text = tempRollYear.ToString();                    
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
        /// Gets the index of the row.
        /// </summary>
        /// <returns>integer value of grid row index</returns>
        private int GetRowIndex()
        {
            try
            {
                if (this.subFundSelectionGridRowCount > 0)
                {
                    if (this.SubFundSelectionDataGridView.CurrentCell != null)
                    {
                        return this.SubFundSelectionDataGridView.CurrentCell.RowIndex;
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
        /// Custimizes the sub fund selection grid.
        /// </summary>
        private void CustimizeSubFundSelectionGrid()
        {
            this.SubFundSelectionDataGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection columns = this.SubFundSelectionDataGridView.Columns;

            columns[this.subFundSelectionData.GetSubFundSelection.SubFundIDColumn.ColumnName].DataPropertyName = this.subFundSelectionData.GetSubFundSelection.SubFundIDColumn.ColumnName;
            columns[this.subFundSelectionData.GetSubFundSelection.SubFundColumn.ColumnName].DataPropertyName = this.subFundSelectionData.GetSubFundSelection.SubFundColumn.ColumnName;
            columns[this.subFundSelectionData.GetSubFundSelection.DescriptionColumn.ColumnName].DataPropertyName = this.subFundSelectionData.GetSubFundSelection.DescriptionColumn.ColumnName;
            columns[this.subFundSelectionData.GetSubFundSelection.RollYearColumn.ColumnName].DataPropertyName = this.subFundSelectionData.GetSubFundSelection.RollYearColumn.ColumnName;

            columns[this.subFundSelectionData.GetSubFundSelection.SubFundIDColumn.ColumnName].DisplayIndex = 0;
            columns[this.subFundSelectionData.GetSubFundSelection.SubFundColumn.ColumnName].DisplayIndex = 1;
            columns[this.subFundSelectionData.GetSubFundSelection.DescriptionColumn.ColumnName].DisplayIndex = 2;
            columns[this.subFundSelectionData.GetSubFundSelection.RollYearColumn.ColumnName].DisplayIndex = 3;
        }

        /// <summary>
        /// To load the SubFund selection Grid
        /// </summary>
        /// <param name="subFundValue">The subfund value</param>
        /// <param name="descriptionValue">The description value</param>
        /// <param name="rollYearValue">the roll year value</param>
        private void LoadSubFundSelectionGrid(string subFundValue, string descriptionValue, int rollYearValue)
        {   
            this.subFundSelectionData = this.form1515Control.WorkItem.F1515_GetSubFundSelection(subFundValue, descriptionValue, rollYearValue, this.iscash);
            this.subFundSelectionGridRowCount = this.subFundSelectionData.GetSubFundSelection.Rows.Count;
            if (this.subFundSelectionGridRowCount > 0)
            {
                this.SubFundSelectionDataGridView.Enabled = true;

                this.SubFundSelectionDataGridView.DataSource = this.subFundSelectionData.GetSubFundSelection;
                this.SubFundSelectionDataGridView.Focus();
                this.SubFundSelectionDataGridView.Rows[0].Selected = true;

                this.SubFundAcceptButton.Enabled = true;
            }
            else
            {
                this.ClearSubFundSelectionGrid();
            }

            ////to enable or disable the vertical scroll bar
            if (this.subFundSelectionGridRowCount > this.SubFundSelectionDataGridView.NumRowsVisible)
            {
                this.SubFundSlectionVerticalScroll.Visible = false;
            }
            else
            {
                this.SubFundSlectionVerticalScroll.Visible = true;
            }

            ////To display the no of display rows in the grid
            this.RecordCountLabel.Text = this.subFundSelectionGridRowCount + SharedFunctions.GetResourceString("9101MasterNameSearch");          
        }

        /// <summary>
        /// To clear the ubFund selection Grid
        /// </summary>
        private void ClearSubFundSelectionGrid()
        {
            this.SubFundSelectionDataGridView.Enabled = false;
            this.subFundSelectionData.GetSubFundSelection.Clear();
            this.SubFundSelectionDataGridView.DataSource = this.subFundSelectionData.GetSubFundSelection;
            this.SubFundSelectionDataGridView.Rows[0].Selected = false;
            this.SubFundSlectionVerticalScroll.Visible = true;
            this.SubFundAcceptButton.Enabled = false;
        }

        /// <summary>
        /// To Clear the SubFund selection text boxs
        /// </summary>
        private void ClearSubFundSelection()
        {
            this.SubFundTextBox.Text = string.Empty;
            this.DescTextBox.Text = string.Empty;
            this.RecordCountLabel.Text = string.Empty;
        }

        /// <summary>
        /// To enable or disable the accept,search, and clear buttons
        /// </summary>
        /// <param name="unlock">Bollean value</param>
        private void EnableButtons(bool unlock)
        {
            this.SubFundSearchButton.Enabled = unlock;
            this.SubFundAcceptButton.Enabled = unlock;
            this.SubFundClearButton.Enabled = unlock;
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Handles the Load event of the F1515 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1515_Load(object sender, EventArgs e)
        {
            try
            {
                this.ClearSubFundSelection();
                this.ClearSubFundSelectionGrid();
                ////To Custimize SubFund Selection Grid
                this.CustimizeSubFundSelectionGrid();
                this.EnableButtons(false);

                ////to load the rollyear textbox
                if (this.rollYear >= 0)
                {
                    this.RollYearTextBox.Text = this.rollYear.ToString();
                }
                else
                {
                    this.RollYearTextBox.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SubFundSearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SubFundSearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                Int16 rollYearValue;
                if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
                {
                    Int16.TryParse(this.RollYearTextBox.Text.Trim(), out rollYearValue);
                }
                else
                {
                    rollYearValue = -999;
                }

                this.LoadSubFundSelectionGrid(this.SubFundTextBox.Text.Trim(), this.DescTextBox.Text.Trim(), rollYearValue);              
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Enables the search button.
        /// </summary>
        private void EnableSearchButton()
        {
            if (!string.IsNullOrEmpty(this.SubFundTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.DescTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                this.SubFundSearchButton.Enabled = true;
                this.SubFundClearButton.Enabled = true;
                ////khaja added code to fix Bug#5284
                this.SubFundAcceptButton.Enabled = false;
            }
            else
            {
                this.SubFundSearchButton.Enabled = false;

                if (this.subFundSelectionGridRowCount <= 0)
                {
                    this.SubFundAcceptButton.Enabled = false;
                    this.SubFundClearButton.Enabled = false;
                }
                else
                {
                    this.SubFundAcceptButton.Enabled = true;
                    this.SubFundClearButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the SubFundClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SubFundClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ClearSubFundSelection();
                this.RollYearTextBox.Text = string.Empty;
                this.ClearSubFundSelectionGrid();
                this.EnableButtons(false);
                this.SubFundTextBox.Focus();
                this.DialogResult = DialogResult.None;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }     

        /// <summary>
        /// Handles the Click event of the SubFundCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SubFundCancelButton_Click(object sender, EventArgs e)
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
        /// Handles the LinkClicked event of the SubFundManagementLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void SubFundManagementLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11005);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Editexts the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AllSubFundTextBoxs_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(this.SubFundTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.DescTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim())))
                {
                    this.EnableSearchButton();
                }
                else
                {
                    if (this.subFundSelectionGridRowCount > 0)
                    {
                        this.SubFundSearchButton.Enabled = false;
                    }
                    else
                    {
                        this.EnableButtons(false);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SubFundAcceptButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SubFundAcceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.subFundSelectionGridRowCount > 0)
                {
                    this.SelectSubFunDId();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the SubFundSelectionDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SubFundSelectionDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && this.SubFundAcceptButton.Enabled)
                {
                    if (this.SubFundSelectionDataGridView.Rows[e.RowIndex].Cells["SubFundID"].Value != null && !string.IsNullOrEmpty(this.SubFundSelectionDataGridView.Rows[e.RowIndex].Cells["SubFundID"].Value.ToString()))
                    {
                        this.SelectSubFunDId();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowHeaderMouseDoubleClick event of the SubFundSelectionDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void SubFundSelectionDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.SubFundAcceptButton.Enabled)
                {
                    this.SelectSubFunDId();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the SubFundManagementLinkLabel control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubFundManagementLinkLabel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyValue.Equals((int)Keys.Up) || e.KeyValue.Equals((int)Keys.Down) || e.KeyValue.Equals((int)Keys.Left) || e.KeyValue.Equals((int)Keys.Right))
                {
                    if (SubFundSelectionDataGridView.OriginalRowCount > 0)
                    {
                        this.SubFundSelectionDataGridView.Focus();
                    }
                    else
                    {
                        this.RollYearTextBox.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Events
    }
}