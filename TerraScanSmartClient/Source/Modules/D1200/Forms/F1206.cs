//--------------------------------------------------------------------------------------------
// <copyright file="F1206.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Posting Errors.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 18 Sept 06		KRISHNA ABBURI	    Created
//*********************************************************************************/

namespace D1200
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
    using System.Web.Services.Protocols;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;
    using TerraScan.Utilities;

    /// <summary>
    /// f1206 class
    /// </summary>
    public partial class F1206 : Form
    {
        #region Variables

        /// <summary>
        /// form1206Control varaible 
        /// </summary>
        private F1206Controller form1206Control;

        /// <summary>
        /// postingErrorsDataset Contains Posting Errors Details 
        /// </summary>
        private PostingErrorsData postingErrorsDataset = new PostingErrorsData();

        /// <summary>
        /// Used to store errorTypeId
        /// </summary>
        private int errorTypeId;

        /// <summary>
        /// Used to store the postingErrorsGridCount
        /// </summary>
        private int postingErrorsGridCount;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1206"/> class.
        /// </summary>
        public F1206()
        {
            InitializeComponent();
            this.CancelButton = this.PostingErrorsCloseButton;
        }
        #endregion Constructor

        #region properites

        /// <summary>
        /// Gets or sets the F1206 controll.
        /// </summary>
        /// <value>The F1206 controll.</value>
        [CreateNew]
        public F1206Controller F1206Controll
        {
            get { return this.form1206Control as F1206Controller; }
            set { this.form1206Control = value; }
        }

        #endregion properites

        #region Methods

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizeDataGrid()
        {
            this.PostingErrorsDataGrid.AllowUserToResizeColumns = false;
            this.PostingErrorsDataGrid.AutoGenerateColumns = false;
            this.PostingErrorsDataGrid.AllowUserToResizeRows = false;
            this.PostingErrorsDataGrid.StandardTab = true;

            this.PostingErrorsDataGrid.Columns[0].DataPropertyName = this.postingErrorsDataset.ListPostingErrors.ErrorTypeColumn.ColumnName;
            this.PostingErrorsDataGrid.Columns[1].DataPropertyName = "ReportText";
            this.PostingErrorsDataGrid.Columns[2].DataPropertyName = this.postingErrorsDataset.ListPostingErrors.ErrorTypeIDColumn.ColumnName;
            this.PostingErrorsDataGrid.Columns[4].DataPropertyName = this.postingErrorsDataset.ListPostingErrors.ReportColumn.ColumnName;
        }

        /// <summary>
        /// Fills the post errorsgrid.
        /// </summary>
        private void FillPostErrorsgrid()
        {
            this.postingErrorsDataset = this.F1206Controll.WorkItem.ListpostingErrors(TerraScanCommon.UserId);
            this.postingErrorsGridCount = this.postingErrorsDataset.ListPostingErrors.Rows.Count;
            if (this.postingErrorsGridCount > 0)
            {
                this.PostingErrorsDataGrid.Enabled = true;
                this.postingErrorsDataset.ListPostingErrors.Columns.Add("ReportText", typeof(string));
                foreach (DataRow row in this.postingErrorsDataset.ListPostingErrors.Rows)
                {

                    row["ReportText"] = "Report";
                }
                this.PostingErrorsDataGrid.DataSource = this.postingErrorsDataset.ListPostingErrors;

                this.PostingErrorsDataGrid.Rows[0].Selected = true;
                ////this.DisableVScrollBar();

                ////To Enable or Disable Create account and Create subfund buttons based on error type id
                this.ToDisableEnableAccountButtons(0);
                //////if (Convert.ToBoolean(this.postingErrorsDataset.ListPostingErrorFlag.Rows[0][0]) == true)
                //////{
                //////    this.CreateAccountButton.Enabled = true;
                //////}
                //////else
                //////{
                //////    this.CreateAccountButton.Enabled = false;
                //////}
            }
            else
            {
                this.PostingErrorsDataGrid.DataSource = this.postingErrorsDataset;
                this.PostingErrorsDataGrid.Enabled = false;
                this.PostingErrorsDataGrid.Rows[0].Selected = false;
                ////this.emptyRecord = true;
                ////this.ReverseGLPostButton.Enabled = false;
            }
        }

        /// <summary>
        /// Enables the button.
        /// </summary>
        private void EnableButton()
        {
            ////if (Convert.ToInt32(this.postingErrorsDataset.ListPostingErrorFlag.Rows[0][0].ToString()) == 1) 
            ////{
            ////    this.CreateAccountButton.Enabled = true;
            ////}
            ////else
            ////{
            ////    this.CreateAccountButton.Enabled = false;
            ////}
        }

        /// <summary>
        /// Disables the V scroll bar.
        /// </summary>
        private void DisableVScrollBar()
        {
            if (this.postingErrorsGridCount > this.PostingErrorsDataGrid.NumRowsVisible)
            {
                ////this.PostingErrorsVerticalScroll.Enabled = true;
                this.PostingErrorsVerticalScroll.Visible = false;
                this.ScrollPanel.SendToBack();
            }
            else
            {   
                this.PostingErrorsVerticalScroll.Visible = true;
                this.ScrollPanel.BringToFront();
            }
        }

        /// <summary>
        /// Selects the SubFunDID from the account element mgmt grid.
        /// </summary>
        private void SelectPostingErrorID()
        {
            int rowId = 0;

            try
            {
                ////to get the current row id of the datagrid
                rowId = this.GetRowIndex();

                if (this.postingErrorsGridCount > 0 && rowId >= 0)
                {
                    if (!string.IsNullOrEmpty(this.PostingErrorsDataGrid.Rows[rowId].Cells[this.postingErrorsDataset.ListPostingErrors.ErrorTypeIDColumn.ColumnName].Value.ToString()))
                    {
                        int.TryParse(this.PostingErrorsDataGrid.Rows[rowId].Cells[this.postingErrorsDataset.ListPostingErrors.ErrorTypeIDColumn.ColumnName].Value.ToString(), out this.errorTypeId);
                        /////userIdvalue = TerraScanCommon.UserId;
                        this.F1206Controll.WorkItem.InsertAccount(TerraScanCommon.UserId, this.errorTypeId);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    this.DialogResult = DialogResult.None;
                    return;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
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
                if (this.postingErrorsGridCount > 0)
                {
                    if (this.PostingErrorsDataGrid.CurrentCell != null)
                    {
                        return this.PostingErrorsDataGrid.CurrentCell.RowIndex;
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
        /// To Enable or Disable Create account and Create subfund buttons based on error type id
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void ToDisableEnableAccountButtons(int rowIndex)
        {
            int.TryParse(this.PostingErrorsDataGrid.Rows[rowIndex].Cells[this.postingErrorsDataset.ListPostingErrors.ErrorTypeIDColumn.ColumnName].Value.ToString(), out this.errorTypeId);

            if (this.errorTypeId == 1)
            {
                ////When the error type is 1 create AccountButton is enabled
                this.CreateAccountButton.Enabled = true;
                this.CreateSubFundButton.Enabled = false;
            }
            else if (this.errorTypeId == 3)
            {
                ////When the error type is 3 create SubFundButton is enabled
                this.CreateSubFundButton.Enabled = true;
                this.CreateAccountButton.Enabled = false;
            }
            else
            {
                ////to disable create AccountButton and create SubFundButton
                this.CreateSubFundButton.Enabled = false;
                this.CreateAccountButton.Enabled = false;
            }
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Handles the Load event of the F1206 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1206_Load(object sender, EventArgs e)
        {
            try
            {
                this.CustomizeDataGrid();
                this.FillPostErrorsgrid();
                this.DisableVScrollBar();
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the PostingErrorsCloselButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PostingErrorsCloselButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the PostingErrorsDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void PostingErrorsDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.PostingErrorsDataGrid.Columns["Report"].Index)
                {
                    if (this.PostingErrorsDataGrid.Rows[e.RowIndex].Selected || this.PostingErrorsDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                    {
                        (PostingErrorsDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).LinkColor = Color.White;
                        (PostingErrorsDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).ActiveLinkColor = Color.White;
                        (PostingErrorsDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).VisitedLinkColor = Color.White;
                    }
                    else
                    {
                        (PostingErrorsDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).LinkColor = Color.Blue;
                        (PostingErrorsDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).ActiveLinkColor = Color.Red;
                        (PostingErrorsDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).VisitedLinkColor = Color.Blue;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the PostingErrorsDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PostingErrorsDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ////this.Cursor = Cursors.WaitCursor;
                if (e.ColumnIndex == 1 && e.RowIndex >= 0)
                {
                    Hashtable userTabReport = new Hashtable();
                    int rptNumber = Convert.ToInt32(this.PostingErrorsDataGrid[4, e.RowIndex].Value.ToString());                    
                    userTabReport.Add("UserID", TerraScanCommon.UserId);
                    userTabReport.Add("ReportNumber", rptNumber);
                    ////changed the parameter type from string to int
                    TerraScanCommon.ShowReport(rptNumber, TerraScan.Common.Reports.Report.ReportType.Preview, userTabReport);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the CreateAccountButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CreateAccountButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.SelectPostingErrorID();
                ////this.F1206Controll.WorkItem.InsertAccount(TerraScanCommon.UserId, );           
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }       

        /// <summary>
        /// Handles the CellClick event of the PostingErrorsDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PostingErrorsDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= -1 && e.RowIndex >= 0 && this.postingErrorsGridCount > 0)
                {
                    if (!string.IsNullOrEmpty(this.PostingErrorsDataGrid.Rows[e.RowIndex].Cells[this.postingErrorsDataset.ListPostingErrors.ErrorTypeIDColumn.ColumnName].Value.ToString()))
                    {
                        this.ToDisableEnableAccountButtons(e.RowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the PostingErrorsDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PostingErrorsDataGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= -1 && e.RowIndex >= 0 && this.postingErrorsGridCount > 0)
                {
                    if (!string.IsNullOrEmpty(this.PostingErrorsDataGrid.Rows[e.RowIndex].Cells[this.postingErrorsDataset.ListPostingErrors.ErrorTypeIDColumn.ColumnName].Value.ToString()))
                    {
                        this.ToDisableEnableAccountButtons(e.RowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Events
    }
}