//--------------------------------------------------------------------------------------------
// <copyright file="F1022.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1022.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 21 Feb 07        RANJANI              Created
//*********************************************************************************/
namespace D11018
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
    using Infragistics.Win.UltraWinGrid;

    /// <summary>
    /// F1022 Form
    /// </summary>
    public partial class F1022 : BasePage
    {
        #region Private Variables

        /// <summary>
        /// form1022Control Variable
        /// </summary>
        private F1022Controller form1022Control;

        /// <summary>
        /// miscTempalteId - key value for tempalte
        /// </summary>
        private int miscTempalteId;

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1022"/> class.
        /// </summary>
        /// <param name="parentFormId">The parent form id.</param>
        public F1022(int parentFormId)
        {
            this.InitializeComponent();
            this.ParentFormId = parentFormId;
            this.CancelButton = this.CancelMiscTemplateButton;
            this.AcceptButton = this.AcceptMiscTemplateButton;
             ////Set form name
            this.Text = string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("SelectMiscTemplateName"));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form1022 control.
        /// </summary>
        /// <value>The form1022 control.</value>
        [CreateNew]
        public F1022Controller F1022Control
        {
            get { return this.form1022Control as F1022Controller; }
            set { this.form1022Control = value; }
        }

        /// <summary>
        /// Gets or sets the misc tempalte id.
        /// </summary>
        /// <value>The misc tempalte id.</value>
        public int MiscTempalteId
        {
            get { return this.miscTempalteId; }
            set { this.miscTempalteId = value; }
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

        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F1022 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1022_Load(object sender, EventArgs e)
        {
            ////load form record set
            this.PopulateMiscTemplateGrid();
        }

        #endregion    

        #region Private Methods

        /// <summary>
        /// Handles the Click event of the AcceptMiscTemplateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AcceptMiscTemplateButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////get template and close the form
                this.GetMiscTemplateRecord(true);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        } 

        /// <summary>
        /// Handles the Click event of the DeleteButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Commented to implement TFS#21152 Item by purushotham 

                //int rowIndex = -1;
                //if (this.MiscTemplatesGridView.ActiveRow != null)
                //{
                //    rowIndex = this.MiscTemplatesGridView.ActiveRow.Index;
                //}
                //////check for selected row
                //if (rowIndex >= 0)
                //{
                //    {
                //        if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteRecord"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //        {
                //            ////get template
                //            this.GetMiscTemplateRecord(false);
                //            ////check for valid value
                //            if (this.miscTempalteId > -999)
                //            {
                //                ////delete misc template
                //                // Modified to implement TFS#21152 Item
                //               // this.form1022Control.WorkItem.F1022_DeleteMiscReceiptTemplate(this.miscTempalteId, TerraScanCommon.UserId);
                //                ////resettemplateid
                //                this.miscTempalteId = -999;
                //                ////reload the form record set
                //                this.PopulateMiscTemplateGrid();
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }

        /// <summary>
        /// Gets the current misc template record and close the form.
        /// </summary>
        /// <param name="closeForm">if set true to set MiscTempalteId and close form.</param>
        private void GetMiscTemplateRecord(bool closeForm)
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
                if (this.MiscTemplatesGridView.ActiveRow.Cells["MiscTemplateID"].Value != null && !string.IsNullOrEmpty(this.MiscTemplatesGridView.ActiveRow.Cells["MiscTemplateID"].Value.ToString()))
                {
                    ////set template id - accessed by calling form
                    this.MiscTempalteId = int.Parse(this.MiscTemplatesGridView.ActiveRow.Cells["MiscTemplateID"].Value.ToString());
                    this.commandResult = this.MiscTempalteId.ToString();

                    if (closeForm)
                    {
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                }
            }

            /*if (this.MiscTemplateGridView.CurrentCell != null)
            {
                rowIndex = this.MiscTemplateGridView.CurrentCell.RowIndex;
            }
            ////check for selected row
            if (rowIndex >= 0)
            {
                ////check for templated value existence
                if (this.MiscTemplateGridView[this.TemplateId.Name, rowIndex].Value != null && !string.IsNullOrEmpty(this.MiscTemplateGridView[this.TemplateId.Name, rowIndex].Value.ToString()))
                {
                    ////set template id - accessed by calling form
                    this.MiscTempalteId = int.Parse(this.MiscTemplateGridView[this.TemplateId.Name, rowIndex].Value.ToString());
                    this.commandResult = this.MiscTempalteId.ToString();

                    if (closeForm)
                    {
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                }
            }*/
        }

        /// <summary>
        /// Populates the misc template grid.
        /// </summary>
        private void PopulateMiscTemplateGrid()
        {            
            F11018MiscReceiptData.ListMiscReceiptTemplateDataTable miscReceiptTemplateDataTable = new F11018MiscReceiptData.ListMiscReceiptTemplateDataTable();
            miscReceiptTemplateDataTable.Clear();
            miscReceiptTemplateDataTable.Merge(this.form1022Control.WorkItem.F1022_ListMiscReceiptTemplate().ListMiscReceiptTemplate);
            ////load grid
            this.MiscTemplatesGridView.DataSource = miscReceiptTemplateDataTable;

            int rowIndex = this.MiscTemplatesGridView.Rows.Count;
            if (rowIndex > 0)
            {
                this.MiscTemplatesGridView.Enabled = true;
                this.AcceptMiscTemplateButton.Enabled = true;
               // Modified to implement TFS#21152 Item
               // this.DeleteButton.Enabled = this.DeleteButton.ActualPermission;
                rowIndex = 0;
                if (this.MiscTemplatesGridView.ActiveRow == null)
                {
                    ////select first row
                    this.MiscTemplatesGridView.ActiveCell = this.MiscTemplatesGridView.Rows[rowIndex].Cells[miscReceiptTemplateDataTable.TemplateNameColumn.ColumnName];
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
                // Modified to implement TFS#21152 Item
                //this.DeleteButton.Enabled = false;
            }
        }

   
        #endregion

        /// <summary>
        /// To handle Enter key press
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void MiscTemplateGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    this.GetMiscTemplateRecord(true);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }

        private void MiscTemplatesGridView_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                F11018MiscReceiptData.ListMiscReceiptTemplateDataTable miscReceiptTemplateDataTable = new F11018MiscReceiptData.ListMiscReceiptTemplateDataTable();
                UltraGridBand currentBand = this.MiscTemplatesGridView.DisplayLayout.Bands[0];
                currentBand.Columns[miscReceiptTemplateDataTable.MiscTemplateIDColumn.ColumnName].Header.VisiblePosition = 0;
                currentBand.Columns[miscReceiptTemplateDataTable.TemplateNameColumn.ColumnName].Header.VisiblePosition = 1;
                currentBand.Columns[miscReceiptTemplateDataTable.UserIDColumn.ColumnName].Header.VisiblePosition = 2;
                currentBand.Columns[miscReceiptTemplateDataTable.UserNameColumn.ColumnName].Header.VisiblePosition = 3;
                currentBand.Columns[miscReceiptTemplateDataTable.ReceivedFromColumn.ColumnName].Header.VisiblePosition = 4;
                currentBand.Columns[miscReceiptTemplateDataTable.AmountColumn.ColumnName].Header.VisiblePosition = 5;
                currentBand.Columns[miscReceiptTemplateDataTable.CodeColumn.ColumnName].Header.VisiblePosition = 6;

                currentBand.Columns[miscReceiptTemplateDataTable.MiscTemplateIDColumn.ColumnName].Hidden = true;
                currentBand.Columns[miscReceiptTemplateDataTable.TemplateNameColumn.ColumnName].Hidden = false;
                currentBand.Columns[miscReceiptTemplateDataTable.UserIDColumn.ColumnName].Hidden = true;
                currentBand.Columns[miscReceiptTemplateDataTable.UserNameColumn.ColumnName].Hidden = true;
                currentBand.Columns[miscReceiptTemplateDataTable.ReceivedFromColumn.ColumnName].Hidden = false;
                currentBand.Columns[miscReceiptTemplateDataTable.AmountColumn.ColumnName].Hidden = false;
                currentBand.Columns[miscReceiptTemplateDataTable.AmountColumn.ColumnName].Format = "#,##0.00";
                currentBand.Columns[miscReceiptTemplateDataTable.CodeColumn.ColumnName].Hidden = true;
                this.MiscTemplatesGridView.DisplayLayout.Override.AllowColSizing = AllowColSizing.None;
                
            }
            catch (Exception ex)
            {
            }
        }

        private void MiscTemplatesGridView_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            try
            {
                if (e.Cell.Row.Index >= 0 && e.Cell.Row.Index < this.MiscTemplatesGridView.Rows.Count)
                {
                    ////get template and close the form
                    this.GetMiscTemplateRecord(true);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }
    }
}