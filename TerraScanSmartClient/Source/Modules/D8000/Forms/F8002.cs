//--------------------------------------------------------------------------------------------
// <copyright file="F8002.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1100.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 07/09/2006       M.Vijaya Kumar     Created// 
//*********************************************************************************/

namespace D8000
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

    /// <summary>
    /// F8002 class file
    /// </summary>
    public partial class F8002 : Form
    {
        #region Variables

        /// <summary>
        /// used to store recordCount
        /// </summary>
        private int recordCount;

        /// <summary>
        /// activeWorkOrderId
        /// </summary>
        private int activeWorkOrderId = -1;

        /// <summary>
        /// gdocWorkOrderData
        /// </summary>
        private GDocWorkOrderData gdocWorkOrderData = new GDocWorkOrderData();

        /// <summary>
        /// The selected row index value
        /// </summary>
        private int dataGridSelectedValue = -1;

        /// <summary>
        /// systemId
        /// </summary>
        private int featureClassId;

        /// <summary>
        /// form8002Control
        /// </summary>
        private F8002Controller form8002Control;

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8002"/> class.
        /// </summary>
        public F8002()
        {
            this.InitializeComponent();
            this.CancelButton = this.CloseButton;
            this.AcceptButton = this.AcceptActiveRecordButton;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8002"/> class.
        /// </summary>
        /// <param name="featureClassId">The feature class id.</param>
        public F8002(int featureClassId)
        {
            this.InitializeComponent();
            this.featureClassId = featureClassId;
            this.CancelButton = this.CloseButton;
            this.AcceptButton = this.AcceptActiveRecordButton;
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// Declare the event ShowForm        
        /// </summary> 
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the mortgage import template controll.
        /// </summary>
        /// <value>The mortgage import template controll.</value>
        [CreateNew]
        public F8002Controller F8002Controll
        {
            get { return this.form8002Control as F8002Controller; }
            set { this.form8002Control = value; }
        }

        /// <summary>
        /// Gets or sets the active work order id.
        /// </summary>
        /// <value>The active work order id.</value>
        public int ActiveWorkOrderId
        {
            get { return this.activeWorkOrderId; }
            set { this.activeWorkOrderId = value; }
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

        #region Methods

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizeDataGrid()
        {
            // to check condition to fill datagrid
            if (this.gdocWorkOrderData.GetWorkOrderDetails.Rows.Count > 0)
            {
                // to make datagrid visiable when there are records

                this.ActiveWorkRecordDataGridView.Columns[this.gdocWorkOrderData.GetWorkOrderDetails.WOIDColumn.ColumnName].DataPropertyName = this.gdocWorkOrderData.GetWorkOrderDetails.WOIDColumn.ColumnName;
                this.ActiveWorkRecordDataGridView.Columns[this.gdocWorkOrderData.GetWorkOrderDetails.WODateColumn.ColumnName].DataPropertyName = this.gdocWorkOrderData.GetWorkOrderDetails.WODateColumn.ColumnName;
                this.ActiveWorkRecordDataGridView.Columns[this.gdocWorkOrderData.GetWorkOrderDetails.WOTypeColumn.ColumnName].DataPropertyName = this.gdocWorkOrderData.GetWorkOrderDetails.WOTypeColumn.ColumnName;
                this.ActiveWorkRecordDataGridView.Columns[this.gdocWorkOrderData.GetWorkOrderDetails.CommentsColumn.ColumnName].DataPropertyName = this.gdocWorkOrderData.GetWorkOrderDetails.CommentsColumn.ColumnName;
                                                
                this.recordCount = this.gdocWorkOrderData.GetWorkOrderDetails.Rows.Count;
                this.ActiveWorkRecordDataGridView.Enabled = true;
                  
                this.ActiveWorkRecordDataGridView.DataSource = this.gdocWorkOrderData.GetWorkOrderDetails;
                this.ActiveWorkRecordDataGridView.Rows[0].Selected = true;
                this.ActiveWorkRecordDataGridView.Focus();
                this.RecordMatchLabel.Text = this.recordCount + SharedFunctions.GetResourceString("ActiveWorkOrders");  //// to display how many records are matched

                // to customize the datagrid

                this.ActiveWorkRecordDataGridView.AllowUserToResizeColumns = false;
                this.ActiveWorkRecordDataGridView.AutoGenerateColumns = false;
                this.ActiveWorkRecordDataGridView.AllowUserToResizeRows = false;
                this.ActiveWorkRecordDataGridView.StandardTab = true;
                this.ActiveWorkRecordDataGridView.PrimaryKeyColumnName = this.gdocWorkOrderData.GetWorkOrderDetails.WOIDColumn.ColumnName;

                // to bind the datas to the datagrid

                this.DisableVScrollBar();
                this.AcceptActiveRecordButton.Focus();
            }
            else
            {
                // to make datagrid visiable false when there are no records
                this.ActiveWorkRecordDataGridView.Enabled = false;
                ////this.ActiveWorkRecordDataGridView.DataSource = this.gdocWorkOrderData.GetWorkOrderDetails;
                this.ActiveWorkRecordDataGridView.DataSource = null;
                this.ActiveWorkRecordDataGridView.Rows[0].Selected = false;
                this.RecordMatchLabel.Visible = false;

                this.DisableVScrollBar();
                this.AcceptActiveRecordButton.Enabled = false;
                this.ManagementButton.Enabled = true;
            }
        }

        /// <summary>
        /// Disables or enables the Vertical Scroll bar.
        /// </summary>
        private void DisableVScrollBar()
        {
            if (this.gdocWorkOrderData.GetWorkOrderDetails.Rows.Count > this.ActiveWorkRecordDataGridView.NumRowsVisible)
            {
                this.ActiveRecordVerticalScroll.Enabled = true;
                this.ActiveRecordVerticalScroll.Visible = false;
                this.ScrollPanel.SendToBack();
            }
            else
            {
                this.ActiveRecordVerticalScroll.Enabled = false;
                this.ActiveRecordVerticalScroll.Visible = true;
                this.ActiveRecordVerticalScroll.BringToFront();
            }
        }

        /// <summary>
        /// Gets the active work order id.
        /// </summary>
        private void GetActiveWorkOrderId()
        {
            int rowId = 0;
            rowId = this.GetRowIndex();

            //// if (this.ActiveWorkRecordDataGridView.Rows.Count > 0)
            if (this.recordCount > 0 && rowId >= 0)
            {
                if (!string.IsNullOrEmpty(this.ActiveWorkRecordDataGridView.Rows[rowId].Cells["WOID"].Value.ToString()))
                {
                    this.ActiveWorkOrderId = Convert.ToInt32(this.ActiveWorkRecordDataGridView.Rows[rowId].Cells["WOID"].Value.ToString());

                    this.commandResult = this.ActiveWorkOrderId.ToString();
                }
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
                if (this.recordCount > 0)
                {
                    if (this.ActiveWorkRecordDataGridView.SelectedRows.Count > 0)
                    {
                        this.dataGridSelectedValue = this.ActiveWorkRecordDataGridView.SelectedRows[0].Index;
                    }
                    else if (this.ActiveWorkRecordDataGridView.SelectedCells.Count > 0)
                    {
                        this.dataGridSelectedValue = this.ActiveWorkRecordDataGridView.CurrentCell.RowIndex;
                    }
                    else
                    {
                        this.dataGridSelectedValue = -1;
                    }

                    return this.dataGridSelectedValue;
                }
                else
                {
                    this.dataGridSelectedValue = -1;
                    return this.dataGridSelectedValue;
                }
            }
            catch (Exception)
            {
                this.dataGridSelectedValue = -1;
                return this.dataGridSelectedValue;
            }
        }

        /// <summary>
        /// Action to select the work order
        /// </summary>
        private void SelectedWorkOrder()
        {
            if (this.recordCount > 0)
            {
                this.GetActiveWorkOrderId();
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Handles the Load event of the F8002 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F8002_Load(object sender, EventArgs e)
        {
            try
            {
                this.gdocWorkOrderData = this.form8002Control.WorkItem.GetWorkOrderDetails(this.featureClassId);
                this.AcceptButton = this.AcceptActiveRecordButton;
                this.CancelButton = this.CloseButton;
                this.AcceptActiveRecordButton.Focus();
                this.CustomizeDataGrid();
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }        

        /// <summary>
        /// Handles the CellDoubleClick event of the ActiveWorkRecordDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ActiveWorkRecordDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && !string.IsNullOrEmpty(this.ActiveWorkRecordDataGridView.Rows[e.RowIndex].Cells[this.gdocWorkOrderData.GetWorkOrderDetails.WOIDColumn.ColumnName].Value.ToString()))
                {
                    this.SelectedWorkOrder();
                }
            }          
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the CloseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ManagementButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ManagementButton_Click(object sender, EventArgs e)
        {
            this.GetActiveWorkOrderId();
            try
            {
                FormInfo formInfo = TerraScanCommon.GetFormInfo(8010);
                if (this.activeWorkOrderId != -1) // to be made
                {
                    formInfo.optionalParameters = new object[] { this.activeWorkOrderId };
                    this.DialogResult = DialogResult.Ignore;
                }

                this.Close();
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }
       
        /// <summary>
        /// Handles the KeyDown event of the ActiveWorkRecordDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ActiveWorkRecordDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                int currentRowIndex = this.ActiveWorkRecordDataGridView.CurrentCell.RowIndex;                

                if (e.KeyCode == Keys.Enter && currentRowIndex >= 0 && !string.IsNullOrEmpty(this.ActiveWorkRecordDataGridView.Rows[currentRowIndex].Cells[this.gdocWorkOrderData.GetWorkOrderDetails.WOIDColumn.ColumnName].Value.ToString()))
                {
                    e.Handled = true;

                    this.SelectedWorkOrder();                
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }          

        /// <summary>
        /// Handles the Click event of the AcceptActiveRecordButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AcceptActiveRecordButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.recordCount > 0)
                {
                    this.SelectedWorkOrder();
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