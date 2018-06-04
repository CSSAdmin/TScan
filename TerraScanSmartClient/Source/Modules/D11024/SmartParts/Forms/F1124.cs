//--------------------------------------------------------------------------------------------
// <copyright file="F1124.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1021.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//*********************************************************************************/
namespace D11024
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using TerraScan.Common;
    using System.Configuration;
    using System.Web.Services.Protocols;
    using System.Collections;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win;

    /// <summary>
    /// 
    /// </summary>
    public partial class F1124 :Form
    {

        /// <summary>
        /// form1021Control Variable
        /// </summary>
        private F1124Controller form1124Control;

        /// <summary>
        /// miscTempalteId - key value for tempalte
        /// </summary>
        private int tempalteId;        

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        private string rollyear;

        F11024MultiplejournalEntryData.f1124_SelectJournalEntryTemplateDataTable SearchTemplateDataTable = new F11024MultiplejournalEntryData.f1124_SelectJournalEntryTemplateDataTable();


        /// <summary>
        /// Initializes a new instance of the <see cref="F1124"/> class.
        /// </summary>
        public F1124()
        {
            InitializeComponent();
        }

        #region Properties

        /// <summary>
        /// Gets or sets the form1021 control.
        /// </summary>
        /// <value>The form1024 control.</value>
        [CreateNew]
        public F1124Controller F1124Control
        {
            get { return this.form1124Control as F1124Controller; }
            set { this.form1124Control = value; }
        }

        public int TempalteId
        {
            get { return this.tempalteId; }
            set { this.tempalteId = value; }
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

        public string RollYear
        {
            get { return rollyear; }
            set { rollyear = value; }
        }
        
        #endregion

        /// <summary>
        /// Handles the Load event of the F1124 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F1124_Load(object sender, EventArgs e)
        {
           this.CustomizeGrid();
           this.LoadSearchGrid();
        }

        /// <summary>
        /// Loads the search grid.
        /// </summary>
        private void LoadSearchGrid()
        {
            this.SearchTemplateDataTable.Clear();
            this.SearchTemplateDataTable = this.form1124Control.WorkItem.F11024_SearchTemplateDetails().f1124_SelectJournalEntryTemplate;
            this.MiscTemplatesGridView.DataSource = this.SearchTemplateDataTable.DefaultView;
            int rowIndex = this.MiscTemplatesGridView.Rows.Count;
            if (rowIndex > 0)
            {
                this.MiscTemplatesGridView.Enabled = true;
                this.AcceptMiscTemplateButton.Enabled = true;
                rowIndex = 0;
                if (this.MiscTemplatesGridView.ActiveRow == null)
                {
                    ////select first row
                    this.MiscTemplatesGridView.ActiveCell = this.MiscTemplatesGridView.Rows[rowIndex].Cells[SearchTemplateDataTable.TemplateNameColumn.ColumnName];
                }
                ////select current cell
                this.MiscTemplatesGridView.ActiveCell.Activate();
                this.MiscTemplatesGridView.ActiveRow.Selected = true;
            }
            else
            {
                this.MiscTemplatesGridView.ActiveCell = null;
                this.MiscTemplatesGridView.Enabled = false;
                this.AcceptMiscTemplateButton.Enabled = false;
            }
        }

        /// <summary>
        /// Customizes the grid.
        /// </summary>
        private void CustomizeGrid()
        {
            UltraGridBand currentBand = this.MiscTemplatesGridView.DisplayLayout.Bands[0];
            currentBand.Override.SelectTypeRow = SelectType.Single;

            currentBand.Override.RowSelectors = DefaultableBoolean.True;
            this.MiscTemplatesGridView.DisplayLayout.BorderStyle = UIElementBorderStyle.None;
            this.MiscTemplatesGridView.DisplayLayout.TabNavigation = TabNavigation.NextCell;

            currentBand.Columns[this.SearchTemplateDataTable.TemplateIDColumn.ColumnName].Hidden = true;
            if (this.MiscTemplatesGridView.Rows.Count < 4)
            {
                ////to assgin empty row at the end of the gird
                this.MiscTemplatesGridView.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
                this.MiscTemplatesGridView.DisplayLayout.EmptyRowSettings.Style = EmptyRowStyle.AlignWithDataRows;
            }
            else
            {
                this.MiscTemplatesGridView.DisplayLayout.EmptyRowSettings.ShowEmptyRows = false;
            }
        }

        /// <summary>
        /// Gets the template id.
        /// </summary>
        /// <param name="closeForm">if set to <c>true</c> [close form].</param>
        private void GetTemplateId(bool closeForm)
        {
            ////getCurrent row index
            int rowIndex = -1;
            if (this.MiscTemplatesGridView.ActiveRow != null)
            {
                rowIndex = this.MiscTemplatesGridView.ActiveRow.Index;
            }
            ////check for selected row
            if (rowIndex >= 0)
            {
                ////check for templated value existence
                if (this.MiscTemplatesGridView.ActiveRow.Cells["TemplateID"].Value != null && !string.IsNullOrEmpty(this.MiscTemplatesGridView.ActiveRow.Cells["TemplateID"].Value.ToString()))
                {
                    ////set template id - accessed by calling form
                    this.TempalteId = int.Parse(this.MiscTemplatesGridView.ActiveRow.Cells["TemplateID"].Value.ToString());
                    this.commandResult = this.TempalteId.ToString();
                    this.rollyear = this.MiscTemplatesGridView.ActiveRow.Cells["RollYear"].Value.ToString();
                    
                    if (closeForm)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
        }


        /// <summary>
        /// Handles the Click event of the AcceptTemplateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AcceptTemplateButton_Click(object sender, EventArgs e)
        {
            try
            {                
                this.GetTemplateId(true);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the CancelMiscTemplateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CancelMiscTemplateButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }


        /// <summary>
        /// Handles the DoubleClickCell event of the MiscTemplatesGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.DoubleClickCellEventArgs"/> instance containing the event data.</param>
        private void MiscTemplatesGridView_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            try
            {
                if (e.Cell.Row.Index >= 0 && e.Cell.Row.Index < this.MiscTemplatesGridView.Rows.Count)
                {                    
                    this.GetTemplateId(true);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }
                 
    }
}
