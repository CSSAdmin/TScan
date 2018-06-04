
//--------------------------------------------------------------------------------------------
// <copyright file="F1411.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Statement Search form.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20160720        priyadharshini      TSCO - 1411 Statement Search - Change Parcel Number to Taxpayer
//*********************************************************************************/

namespace D1410
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.Utilities;
    using System.Web.Services.Protocols;

    public partial class F1411 : Form
    {
        #region Variables

        /// <summary>
        /// Created Instance for F9110Controller
        /// </summary>
        private F1411Controller form1411Control;

        /// <summary>
        /// ParcelStmtSearch instance
        /// </summary>
        private F1411ParcelStatementSearchData parcelStmtSearch = new F1411ParcelStatementSearchData();

        /// <summary>
        /// Created Integer for SavedSuccessfully.
        /// </summary>
        private int selected;


        /// <summary>
        /// masterNameOwnerId
        /// </summary>
        private string masterStatementId;

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        /// <summary>
        /// Created Boolean to Find emptyRecord
        /// </summary>        
        private bool emptyRecord;

        /// <summary>
        /// masternamesearchDatatable
        /// </summary>
        private DataTable parcelstatementsearchDatatable = new DataTable();

        ///<summary>
        /// selected row index table
        ///</summary>
        private DataTable selectedrowDataTable = new DataTable();

        /// <summary>
        /// selectedrowIndexrow
        /// </summary>
        private DataRow selectedrowIndexDatarow;

        /// <summary>
        /// masternamesearchdatarow
        /// </summary>
        private DataRow parcelstatementsearchDataRow;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:f1411"/> class.
        /// </summary>
        public F1411()
        {
            InitializeComponent();
            this.CancelButton = this.MasterCancelButton;
            this.AcceptButton = this.SearchButton; 
          
        }

        #endregion Constructor

        #region Property

        /// <summary>
        /// Gets or sets the mortgage import template controll.
        /// </summary>
        /// <value>The mortgage import template controll.</value>
        [CreateNew]
        public F1411Controller  F1411Controll
        {
            get { return this.form1411Control as F1411Controller; }
            set { this.form1411Control = value; }
        }

        /// <summary>
        /// Gets or sets the master name owner id.
        /// </summary>
        /// <value>The master name owner id.</value>
        public string MasterStatementId
        {
            get { return this.masterStatementId; }
            set { this.masterStatementId = value; }
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
        #endregion Property

        #region EventPublication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion EventPublication

        #region Methods

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        /// <returns>Return RowIndex</returns>
        private void GetRowIndex()
        {
            //try
            //{
                if (this.parcelStmtSearch.ParcelStatementSearchDataTable.Rows.Count > 0)
                {
                    if (this.ParcelStmtSearchGridView.SelectedRows.Count > 0)
                    {
                        if (this.selectedrowDataTable.Columns.Count == 0)
                        {
                            this.selectedrowDataTable.Columns.Add("RowIndex");
                        }

                        for (int i = 0; i <this.ParcelStmtSearchGridView.SelectedRows.Count; i++)
                        {
                            //if (this.ParcelStmtSearchGridView.Rows[i].Selected)
                            //{
                                //if (!string.IsNullOrEmpty(this.ParcelStmtSearchGridView.Rows[i].Cells["StatementID"].Value.ToString().Trim()))
                                //{
                                    this.selectedrowIndexDatarow = this.selectedrowDataTable.NewRow();
                                    this.selectedrowIndexDatarow["RowIndex"] = this.ParcelStmtSearchGridView.SelectedRows[i].Index;
                                    this.selectedrowDataTable.Rows.Add(this.selectedrowIndexDatarow);
                                    //return this.selectedrowDataTable;
                                    // this.selected = this.ParcelStmtSearchGridView.SelectedRows[0].Index;
                                //}
                            //}
                        }
                    }
                }
                
                //    else if (this.ParcelStmtSearchGridView.SelectedCells.Count > 0)
                //    {
                //        this.selected = this.ParcelStmtSearchGridView.CurrentCell.RowIndex;
                //    }

                //    return this.selected;
                //}
                //else
                //{
                //    return 0;
                //}
            //}
            //catch (Exception)
            //{
            //    return 0;
            //}
        }

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizeDataGrid()
        {
            this.ParcelStmtSearchGridView.IsMultiSelect = true;
            this.ParcelStmtSearchGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ParcelStmtSearchGridView.AllowUserToResizeColumns = false;
            this.ParcelStmtSearchGridView.AutoGenerateColumns = false;
            this.ParcelStmtSearchGridView.AllowUserToResizeRows = false;
            this.ParcelStmtSearchGridView.StandardTab = true;
            this.ParcelStmtSearchGridView.PrimaryKeyColumnName = "StatementID";
            this.ParcelStmtSearchGridView.Columns[0].DataPropertyName = "StatementID";
            this.ParcelStmtSearchGridView.Columns[1].DataPropertyName = "StatementNumber";
            /* TSCO - 1411 Statement Search - Change Parcel Number to Taxpayer */
            this.ParcelStmtSearchGridView.Columns[2].DataPropertyName = "Taxpayer";
            /* end */
            this.ParcelStmtSearchGridView.Columns[3].DataPropertyName = "PostType";
            this.ParcelStmtSearchGridView.Columns[4].DataPropertyName = "RollYear";
            this.ParcelStmtSearchGridView.Columns[0].Visible = false;  
        }

        /// <summary>
        /// Enables the search button.
        /// </summary>
        private void EnableSearchButton()
        {
            if (!string.IsNullOrEmpty(this.ParcelStmtTextBox.Text.Trim()))
            {
                this.SearchButton.Enabled = true;
                
            }
            else
            {
                this.SearchButton.Enabled = false;

                if (this.parcelStmtSearch.Tables[0].Rows.Count <= 0)
                {
                    this.MasterAcceptButton.Enabled = false;
                   
                }
                else
                {
                    this.MasterAcceptButton.Enabled = true;
                   
                }
            }
        }
        /// <summary>
        /// Gets all statement id.
        /// </summary>
        private void GetAllStatementId()
        {
            if (!this.emptyRecord)
            {
                if (this.parcelstatementsearchDatatable.Columns.Count == 0)
                {
                    this.parcelstatementsearchDatatable.Columns.Add("StatementID");
                }

                for (int i = 0; i <= this.ParcelStmtSearchGridView.OriginalRowCount - 1; i++)
                {
                        if (!string.IsNullOrEmpty(this.ParcelStmtSearchGridView.Rows[i].Cells["StatementID"].Value.ToString().Trim()))
                        {
                            this.parcelstatementsearchDataRow = this.parcelstatementsearchDatatable.NewRow();
                            this.parcelstatementsearchDataRow["StatementID"] = this.ParcelStmtSearchGridView.Rows[i].Cells["StatementID"].Value.ToString().Trim();
                            this.parcelstatementsearchDatatable.Rows.Add(this.parcelstatementsearchDataRow);
                            this.commandResult = TerraScanCommon.GetXmlString(this.parcelstatementsearchDatatable);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                   
                }
            }
 
        }
        /// <summary>
        /// Gets the statement id.
        /// </summary>
        private void GetStatementId()
        {
            int rowId = 0;
          //  this.GetRowIndex();

            if (!this.emptyRecord)
            {
                if (this.parcelstatementsearchDatatable.Columns.Count == 0)
                {
                    this.parcelstatementsearchDatatable.Columns.Add("StatementID");
                }

                for (int i = 0; i < this.ParcelStmtSearchGridView.SelectedRows.Count; i++)
                {
                    this.parcelstatementsearchDataRow = this.parcelstatementsearchDatatable.NewRow();
                    this.parcelstatementsearchDataRow["StatementID"] = this.ParcelStmtSearchGridView.Rows[this.ParcelStmtSearchGridView.SelectedRows[i].Index].Cells["StatementID"].Value.ToString().Trim();
                    this.parcelstatementsearchDatatable.Rows.Add(this.parcelstatementsearchDataRow);
                    this.commandResult = TerraScanCommon.GetXmlString(this.parcelstatementsearchDatatable);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

                //for (int i = 0; i <= this.ParcelStmtSearchGridView.OriginalRowCount - 1; i++)
                //{
                //    if (this.ParcelStmtSearchGridView.Rows[i].Selected)
                //    {
                //        if (!string.IsNullOrEmpty(this.ParcelStmtSearchGridView.Rows[i].Cells["StatementID"].Value.ToString().Trim()))
                //        {
                //            this.parcelstatementsearchDataRow = this.parcelstatementsearchDatatable.NewRow();
                //            this.parcelstatementsearchDataRow["StatementID"] = this.ParcelStmtSearchGridView.Rows[i].Cells["StatementID"].Value.ToString().Trim();
                //            this.parcelstatementsearchDatatable.Rows.Add(this.parcelstatementsearchDataRow);
                //            this.commandResult = TerraScanCommon.GetXmlString(this.parcelstatementsearchDatatable);
                //            this.DialogResult = DialogResult.OK;
                //            this.Close();
                //        }
                //    }
                //}
            }
        }

        /// <summary>
        /// Disables the V scroll bar.
        /// </summary>
        private void DisableVScrollBar()
        {
            if (this.parcelStmtSearch.Tables[0].Rows.Count > 5)
            {
                this.MasterNameVerticalScroll.Enabled = true;
                this.MasterNameVerticalScroll.Visible = false;
                //this.ScrollPanel.SendToBack();
            }
            else
            {
                this.MasterNameVerticalScroll.Enabled = false;
                this.MasterNameVerticalScroll.Visible = true;
                //this.ScrollPanel.BringToFront();
                this.MasterNameVerticalScroll.BringToFront();
            }
        }


        /// <summary>
        /// Disables the buttons.
        /// </summary>
        private void DisableButtons()
        {
            this.SearchButton.Enabled = false;
            this.MasterAcceptButton.Enabled = false;
            this.AcceptAllButton.Enabled = false; 
        }

        #endregion


        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MasterCancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.MasterCancelButton.Enabled)
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

        /// Handles the Load event of the f9110 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1411_Load(object sender, EventArgs e)
        {
            try
            {
               
                this.emptyRecord = true;
                this.CustomizeDataGrid();
                this.ParcelStmtSearchGridView.DataSource = null;
                this.ParcelStmtSearchGridView.Enabled = false;
                this.ParcelStmtSearchGridView.Rows[0].Selected = false;
                this.RecordCountLabel.Text = string.Empty;   
                this.DisableButtons();
                this.DisableVScrollBar();
                this.ParcelStmtTextBox.Focus();
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
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.SearchButton.Enabled)
                {
                    int recordCount = 0;
                    string searchNumber = this.ParcelStmtTextBox.Text.Trim().Replace("'", "''");
                    if (!(string.IsNullOrEmpty(searchNumber)))
                     {
                         this.parcelStmtSearch = this.form1411Control.WorkItem.f1411ParcelStmtSearch(searchNumber);
                         if (this.parcelStmtSearch.ParcelStatementSearchDataTable.Rows.Count > 0)
                         {
                             recordCount = this.parcelStmtSearch.ParcelStatementSearchDataTable.Rows.Count;
                             this.ParcelStmtSearchGridView.Enabled = true;
                             this.ParcelStmtSearchGridView.DataSource = this.parcelStmtSearch.ParcelStatementSearchDataTable.DefaultView;
                             this.ParcelStmtSearchGridView.Focus();
                             this.ParcelStmtSearchGridView.Rows[0].Selected = true;
                             this.AcceptAllButton.Enabled = true;
                             this.MasterAcceptButton.Enabled = true;
                             this.RecordCountLabel.Text = recordCount + SharedFunctions.GetResourceString("9101MasterNameSearch");
                             ////this.MasterNameDataGridView.Focus();
                             this.emptyRecord = false;
                         }
                         else
                         {
                             this.ParcelStmtSearchGridView.DataSource = this.parcelStmtSearch.ParcelStatementSearchDataTable.DefaultView;
                             this.ParcelStmtSearchGridView.Rows[0].Selected = false;
                             this.ParcelStmtSearchGridView.Enabled = false;
                             this.emptyRecord = true;
                              this.AcceptAllButton.Enabled = false;
                              this.RecordCountLabel.Text = 0 + SharedFunctions.GetResourceString("9101MasterNameSearch");
                              this.MasterAcceptButton.Enabled = false;
                         }
                          this.DisableVScrollBar();
                     }
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

        private void MasterAcceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.emptyRecord)
                {
                    this.GetStatementId();
                }
           }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ParcelStmtTextBox_TextChanged(object sender, EventArgs e)
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

        private void AcceptAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.emptyRecord)
                {
                    this.GetAllStatementId();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void FormLinePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
