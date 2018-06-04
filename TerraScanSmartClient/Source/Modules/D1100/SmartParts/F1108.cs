//--------------------------------------------------------------------------------------------
// <copyright file="F1108.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1108.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09 Nov 06		KARTHIKEYAN V	    Created
//*********************************************************************************/

namespace D1100
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
    using TerraScan.Infrastructure.Interface.Constants;
    using System.Xml;
    using System.Net;
    using System.Web.Services.Description;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Reflection;
    using System.Security.Permissions;
    using System.Xml.Serialization;

    /// <summary>
    /// F1108
    /// </summary>
    [SmartPart]
    public partial class F1108 : BaseSmartPart
    {
        #region Variable

        /// <summary>
        /// Set Date Format
        /// </summary>
        private readonly string dateFormat = SharedFunctions.GetResourceString("GDocWaterDateFormat");

        /// <summary>
        /// Keep Track of No sumbit
        /// </summary>
        private int submitCount;

        /// <summary>
        /// Keep Track of no affidavit 
        /// </summary>
        private int affidavitCount;

        /// <summary>
        /// Keep Track of No sumbit success Affidavit  
        /// </summary>
        private int selectedCount;

        /// <summary>
        /// Created Instance for f1105Controller
        /// </summary>
        private F1108Controller form1108Control;

        /// <summary>
        /// formLabelInfo string array
        /// </summary>
        private string[] formLabelInfo = new string[2];

        /// <summary>
        /// Created instance for Typed dataset AffidavitWorkQueue
        /// </summary>
        private SubmittalQueueData affidavitWorkQueueDataSet = new SubmittalQueueData();

        /// <summary>
        /// Created Boolean to Find emptyRecord
        /// </summary>        
        private bool emptyRecord;

        /// <summary>
        /// reportOptionalParameter
        /// </summary>
        private Hashtable reportFileIdHashTable = new Hashtable();

        /// <summary>
        /// reportOptionalParameter
        /// </summary>
        private int rowCount;

        /// <summary>
        /// AffidavitFormSmartPart
        /// </summary>
        private AffidavitFormSmartPart affidavitSmartPart;

        /// <summary>
        /// REETA
        /// </summary>
        private REETA submitDataset = new REETA();

        /// <summary>
        /// xmlFilePath
        /// </summary>
        private string xmlFilePath = string.Empty;

        /// <summary>
        /// reetSubmitValue
        /// </summary>
        private string reetSubmitValue = string.Empty;

        /// <summary>
        /// DataTable
        /// </summary>
        private DataTable configurationDatatable = new DataTable();

        /// <summary>
        /// Status of Sumbit
        /// </summary>
        private Boolean submitPass;

        /// <summary>
        /// validSaleDate
        /// </summary>
        private string validSaleDate = string.Empty;

        /// <summary>
        /// IsValidDate
        /// </summary>
        private bool validDateFormat;

        /// <summary>
        /// doubleOperatorArrayList
        /// </summary>
        private ArrayList doubleOperatorArrayList = new ArrayList();

        /// <summary>
        /// singleOperatorArrayList
        /// </summary>
        private ArrayList singleOperatorArrayList = new ArrayList();

        /// <summary>
        /// DatePicker object
        /// </summary>
        private System.Windows.Forms.DateTimePicker validDate = new System.Windows.Forms.DateTimePicker();

        /// <summary>
        /// Grid Scroll Position
        /// </summary>
        private int scrollPosition;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:f1107"/> class.
        /// </summary>
        public F1108()
        {
            this.InitializeComponent();
            this.DetailsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DetailsPictureBox.Height, this.DetailsPictureBox.Width, SharedFunctions.GetResourceString("Details"), 174, 150, 94);
            this.AffidavitPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AffidavitPictureBox.Height, this.AffidavitPictureBox.Width, SharedFunctions.GetResourceString("AffidavitList"), 28, 81, 128);
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// EventPublication for FormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// EventPublication for AffidavitFormSmartPart ButtonStatus
        /// </summary>
        [EventPublication(EventTopics.D1100_F1107_AffidavitFormSmartPart_DisableButtons, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> DisableButtons;

        /// <summary>
        /// EventPublication for AffidavitFormSmartPart ButtonStatus
        /// </summary>
        [EventPublication(EventTopics.D1100_F1107_AffidavitFormSmartPart_EnableButtons, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> EnableButtons;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the mortgage import template controll.
        /// </summary>
        /// <value>The mortgage import template controll.</value>
        [CreateNew]
        public F1108Controller Form1108Controll
        {
            get { return this.form1108Control as F1108Controller; }
            set { this.form1108Control = value; }
        }

        #endregion

        #region Event Scbscription

        /// <summary>
        /// Affidavits the form button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D1100_F1107_AffidavitFormSmartPart_AffidavitFormButtonClick, Thread = ThreadOption.UserInterface)]
        public void AffidavitFormButtonClick(object sender, DataEventArgs<string> e)
        {
            try
            {
                ////int currentRowId = this.GetRowIndex();
                int currentRowId = 0;
                if (this.AffidavitListDataGrid.CurrentCell != null)
                {
                    currentRowId = this.AffidavitListDataGrid.CurrentCell.RowIndex;
                }

                FormInfo formInfo = TerraScanCommon.GetFormInfo(11010);
                if (!string.IsNullOrEmpty(this.AffidavitListDataGrid.Rows[currentRowId].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.StatementIDColumn.ColumnName].Value.ToString()))
                {
                    formInfo.optionalParameters = new object[] { this.AffidavitListDataGrid.Rows[currentRowId].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.StatementIDColumn.ColumnName].Value };
                }

                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Receipts the form button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D1100_F1107_AffidavitFormSmartPart_ReceiptFormButtonClick, Thread = ThreadOption.UserInterface)]
        public void ReceiptFormButtonClick(object sender, DataEventArgs<string> e)
        {
            try
            {
                ////int currentRowId = this.GetRowIndex();
                int currentRowId = 0;
                if (this.AffidavitListDataGrid.CurrentCell != null)
                {
                    currentRowId = this.AffidavitListDataGrid.CurrentCell.RowIndex;
                }

                FormInfo formInfo = TerraScanCommon.GetFormInfo(11011);
                if (!string.IsNullOrEmpty(this.AffidavitListDataGrid.Rows[currentRowId].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.StatementIDColumn.ColumnName].Value.ToString()))
                {
                    formInfo.optionalParameters = new object[] { this.AffidavitListDataGrid.Rows[currentRowId].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.StatementIDColumn.ColumnName].Value };
                }

                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Views the afdvt button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D1100_F1107_AffidavitFormSmartPart_ViewAfdvtButtonClick, Thread = ThreadOption.UserInterface)]
        public void ViewAfdvtButtonClick(object sender, DataEventArgs<string> e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string reportAuditId = string.Empty;
                ////int currentRowId = this.GetRowIndex();
                int currentRowId = 0;
                if (this.AffidavitListDataGrid.CurrentCell != null)
                {
                    currentRowId = this.AffidavitListDataGrid.CurrentCell.RowIndex;
                }

                if (!string.IsNullOrEmpty(this.AffidavitListDataGrid.Rows[currentRowId].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.StatementIDColumn.ColumnName].Value.ToString()))
                {
                    reportAuditId = this.AffidavitListDataGrid.Rows[currentRowId].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.StatementIDColumn.ColumnName].Value.ToString();
                }

                this.reportFileIdHashTable.Clear();
                ////this.reportFileIdHashTable.Add("KeyName", "ReportFileID");
                ////this.reportFileIdHashTable.Add(SharedFunctions.GetResourceString("MIInputFileStatementID"), reportAuditId);
                this.reportFileIdHashTable.Add(SharedFunctions.GetResourceString("KeyID"), reportAuditId);

                // Shows the report form.
                // changed the parameter type from string to int
                TerraScanCommon.ShowReport(110101, TerraScan.Common.Reports.Report.ReportType.Preview, this.reportFileIdHashTable);
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
        /// Reports the button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D1100_F1108_AffidavitFormSmartPart_ReportButtonClick, Thread = ThreadOption.UserInterface)]
        public void ReportButtonClick(object sender, DataEventArgs<string> e)
        {
            try
            {
                // Shows the report for current statementid
                TerraScanCommon.ShowReport(110810, TerraScan.Common.Reports.Report.ReportType.Preview);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Sends the optional parameters.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        ///[EventSubscription("topic://TerraScan.UI.CAB/MainForm/SendOptionalParameters", Thread = ThreadOption.UserInterface)]
        [EventSubscription(EventTopics.D9001_ShellForm_SendOptionalParameters, Thread = ThreadOption.UserInterface)]
        public void SendOptionalParameters(object sender, DataEventArgs<object[]> e)
        {
            object[] optionalParams = e.Data;
            if (optionalParams.Length > 0 && optionalParams[optionalParams.Length - 1].Equals(this.Tag))
            {
                int foundIndex = 0;
                BindingSource source = new BindingSource();
                source.DataSource = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable;
                if (optionalParams[0] != null)
                {
                    foundIndex = source.Find(SharedFunctions.GetResourceString("MIInputFileStatementID"), optionalParams[0].ToString());
                    if (foundIndex >= 0)
                    {
                        TerraScanCommon.SetDataGridViewPosition(this.AffidavitListDataGrid, foundIndex);
                        this.SetDataBindingValue(foundIndex);
                    }
                }
                else
                {
                    this.AffidavitListDataGrid.DataSource = null;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the CMD key.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="keyData">The key data.</param>
        /// <returns>Bool</returns>
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            if (this.ActiveControl == this.AffidavitListDataGrid)
            {
                if ((keyData == (Keys.Back | Keys.Space | Keys.Shift)) || (keyData == (Keys.RButton | Keys.MButton | Keys.Space | Keys.Shift)))
                {
                    ////return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Loads the work space.
        /// </summary>
        private void LoadWorkSpace()
        {
            if (this.form1108Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.FormHeaderWorkSpace.Show(this.form1108Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.FormHeaderWorkSpace.Show(this.form1108Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            if (this.form1108Control.WorkItem.SmartParts.Contains(D1100SmartPartNames.AffidavitFormSmartPart))
            {
                this.AfdvtButtonWorkSpace.Show(this.affidavitSmartPart);
            }
            else
            {
                this.affidavitSmartPart = this.form1108Control.WorkItem.SmartParts.AddNew<AffidavitFormSmartPart>(D1100SmartPartNames.AffidavitFormSmartPart);
                this.AfdvtButtonWorkSpace.Show(this.affidavitSmartPart);
            }

            this.affidavitSmartPart.SubmitQueueButtonVisible = false;
            this.formLabelInfo[0] = SharedFunctions.GetResourceString("F1108Header");
            this.formLabelInfo[1] = string.Empty;

            this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));
        }

        /// <summary>
        /// Loads the afiidavit list grid.
        /// </summary>
        private void LoadAfiidavitListGrid()
        {
            this.affidavitWorkQueueDataSet = this.form1108Control.WorkItem.GetWorkQueueSearchResult(null, null, null, null, null, null, null);
            this.rowCount = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.Rows.Count;
            this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.Columns.Add(SharedFunctions.GetResourceString("ValidStatus"), System.Type.GetType("System.Boolean"));
            this.AffidavitListDataGrid.DataSource = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.DefaultView;
            this.SelectUnSelectAll("True");
            this.SetRecordCount();
            if (this.rowCount == 0)
            {
                this.emptyRecord = true;
                this.DisableControls();
                this.ClearNonEditableFields();
                this.WorkQueueAuditLink.Enabled = false;
                this.AffidavitListDataGrid.Enabled = false;
                this.WorkQueueAuditLink.Text = SharedFunctions.GetResourceString("1107WorkQueueAuditLink");
                this.DisableButtons(this, new DataEventArgs<string>("0"));
            }
            else
            {
                this.SubmitDORButton.Enabled = true && this.PermissionFiled.editPermission;
                this.emptyRecord = false;
                this.WorkQueueAuditLink.Text = SharedFunctions.GetResourceString("1107WorkQueueAuditLink") + " " + this.AffidavitListDataGrid.Rows[0].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.StatementIDColumn.ToString()].Value.ToString();
                TerraScanCommon.SetDataGridViewPosition(this.AffidavitListDataGrid, 0);
                this.WorkQueueAuditLink.Enabled = true;
                this.AffidavitListDataGrid.Enabled = true;
                this.EnableButtons(this, new DataEventArgs<string>("0"));
            }

            if (this.rowCount > this.AffidavitListDataGrid.NumRowsVisible)
            {
                this.WorkQueueVScrollBar.Enabled = true;
                this.WorkQueueVScrollBar.Visible = false;
                this.AffidavitListDataGrid.Width = 786;
            }
            else
            {
                this.WorkQueueVScrollBar.Enabled = false;
                this.WorkQueueVScrollBar.Visible = true;
                this.AffidavitListDataGrid.Width = 769;
            }
        }

        /// <summary>
        /// Disables the controls.
        /// </summary>
        private void DisableControls()
        {
            this.SearchButton.Enabled = false;
            this.SubmitDORButton.Enabled = false;
        }

        /// <summary>
        /// Clears the non editable fields.
        /// </summary>
        private void ClearNonEditableFields()
        {
            this.StatementIdTextBox.Text = string.Empty;
            this.StreetAddressTextBox.Text = string.Empty;
            this.LocationTextBox.Text = string.Empty;
            this.SaledateTextBox.Text = string.Empty;
            this.TaxCodeTextBox.Text = string.Empty;
            this.TaxableSalePriceTextBox.Text = string.Empty;
            this.TotalDueTextBox.Text = string.Empty;
            this.GrantorTextBox.Text = string.Empty;
            this.GranteeTextBox.Text = string.Empty;
            this.ParcelNumberTextBox.Text = string.Empty;
            this.TreasurerTextBox.Text = string.Empty;
            this.AssessorTextBox.Text = string.Empty;
            this.ReciptNumberTextBox.Text = string.Empty;
            this.UseCodesTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizeDataGrid()
        {
            /* --- Resize the columz --- */
            this.AffidavitListDataGrid.AllowUserToResizeColumns = true;
            ////this.AffidavitListDataGrid.ScrollBars = ScrollBars.Both;
           this.AffidavitListDataGrid.MultiSelect = true;
            /*---End here --- */
            this.AffidavitListDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AffidavitListDataGrid.AutoGenerateColumns = false;
            this.AffidavitListDataGrid.AllowUserToResizeRows = false;
            this.AffidavitListDataGrid.StandardTab = true;
            this.AffidavitListDataGrid.PrimaryKeyColumnName = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.StatementIDColumn.ColumnName;

            this.AffidavitListDataGrid.Columns[SharedFunctions.GetResourceString("ValidStatus")].DataPropertyName = SharedFunctions.GetResourceString("ValidStatus");
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.ParcelNumberColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.ParcelNumberColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.NameColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.NameColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.GrantorColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.GrantorColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.DocumentDateColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.DocumentDateColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.StreetAddressColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.StreetAddressColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.IsExemptColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.IsExemptColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.TreasurerColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.TreasurerColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.AssessorColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.AssessorColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.StatementIDColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.StatementIDColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.LocationColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.LocationColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.TaxableSalePriceColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.TaxableSalePriceColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.TotalDueColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.TotalDueColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.ReceiptNumberColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.ReceiptNumberColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.UseCodeColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.UseCodeColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.GranteeColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.GranteeColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.StatementNumberColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.StatementNumberColumn.ColumnName;
        }

        /// <summary>
        /// Sets the data binding value.
        /// </summary>
        /// <param name="rowId">The row id.</param>
        private void SetDataBindingValue(int rowId)
        {
            if (!this.emptyRecord)
            {
                if (rowId >= 0)
                {
                    decimal taxableSalePrice = 0;
                    decimal totalDue = 0;
                    this.StatementIdTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.StatementNumberColumn.ColumnName].Value.ToString();
                    this.StreetAddressTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.StreetAddressColumn.ColumnName].Value.ToString();
                    this.LocationTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.LocationColumn.ColumnName].Value.ToString();
                    this.SaledateTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.DocumentDateColumn.ColumnName].Value.ToString();
                    this.TaxCodeTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.IsExemptColumn.ColumnName].Value.ToString();
                    decimal.TryParse(this.AffidavitListDataGrid.Rows[rowId].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.TaxableSalePriceColumn.ColumnName].Value.ToString(), out taxableSalePrice);
                    this.TaxableSalePriceTextBox.Text = taxableSalePrice.ToString("$ #,##0.00");
                    decimal.TryParse(this.AffidavitListDataGrid.Rows[rowId].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.TotalDueColumn.ColumnName].Value.ToString(), out totalDue);
                    this.TotalDueTextBox.Text = totalDue.ToString("$ #,##0.00");
                    this.GrantorTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.GrantorColumn.ColumnName].Value.ToString();
                    this.GranteeTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.GranteeColumn.ColumnName].Value.ToString();
                    this.ParcelNumberTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.ParcelNumberColumn.ColumnName].Value.ToString();
                    this.TreasurerTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.TreasurerColumn.ColumnName].Value.ToString();
                    this.AssessorTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.AssessorColumn.ColumnName].Value.ToString();
                    this.ReciptNumberTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.ReceiptNumberColumn.ColumnName].Value.ToString();
                    this.UseCodesTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.UseCodeColumn.ColumnName].Value.ToString();
                }
            }
        }

        /// <summary>
        /// Selects the un select all.
        /// </summary>
        /// <param name="status">The status.</param>
        private void SelectUnSelectAll(string status)
        {
            if (this.rowCount > 0)
            {
                for (int count = 0; count < this.rowCount; count++)
                {
                    this.AffidavitListDataGrid.Rows[count].Cells[SharedFunctions.GetResourceString("ValidStatus")].Value = status;
                }
            }

            this.AffidavitListDataGrid.RefreshEdit();
        }

        /// <summary>
        /// Enables the search.
        /// </summary>
        private void EnableSearch()
        {
            if (!string.IsNullOrEmpty(this.SearchParcelTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.SearchNameTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.SearchSaleDateTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.SearchAddressTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.SearchTaxCodeTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.SearchReceiptNumberTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.SearchStatementTextBox.Text.Trim()))
            {
                this.SearchButton.Enabled = true;
            }
            else
            {
                this.SearchButton.Enabled = false;
            }
        }

        /// <summary>
        /// Clears the fields.
        /// </summary>
        private void ClearFields()
        {
            this.SearchStatementTextBox.Text = string.Empty;
            this.SearchParcelTextBox.Text = string.Empty;
            this.SearchNameTextBox.Text = string.Empty;
            this.SearchSaleDateTextBox.Text = string.Empty;
            this.SearchAddressTextBox.Text = string.Empty;
            this.SearchTaxCodeTextBox.Text = string.Empty;
            this.SearchReceiptNumberTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Grids the click.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void GridClick(int rowIndex)
        {
            if (this.rowCount > 0)
            {
                if (rowIndex >= 0)
                {
                    //if (this.AffidavitListDataGrid.Rows[rowIndex].Selected)
                    //{
                        this.SetDataBindingValue(rowIndex);
                        this.WorkQueueAuditLink.Text = SharedFunctions.GetResourceString("1107WorkQueueAuditLink") + " " + this.AffidavitListDataGrid.Rows[rowIndex].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.StatementIDColumn.ToString()].Value.ToString();
                    //}
                }
            }
        }

        /// <summary>
        /// Checkeds the column.
        /// </summary>
        private void CheckedColumn()
        {
            this.AffidavitListDataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.AcceptChanges();
            DataRow[] checkedRow;
            checkedRow = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.Select(SharedFunctions.GetResourceString("ValidStatusExpression"));

            if (checkedRow.Length > 0)
            {
                this.SubmitDORButton.Enabled = true && this.PermissionFiled.editPermission;
            }
            else
            {
                this.SubmitDORButton.Enabled = false;
            }
        }

        /// <summary>
        /// Creates the XML file.
        /// </summary>
        private void CreateXmlFile()
        {
            StreamWriter sw = new StreamWriter(this.xmlFilePath);
            sw.Write(this.reetSubmitValue);
            sw.Close();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this.xmlFilePath);
            XmlNodeList firstNode = xmlDoc.GetElementsByTagName(SharedFunctions.GetResourceString("REETA"));

            foreach (XmlNode nodeList in firstNode)
            {
                foreach (XmlNode nodeList1 in nodeList)
                {
                    foreach (XmlNode nodeList2 in nodeList1)
                    {
                        if (nodeList2.Name == SharedFunctions.GetResourceString("StatementIDValue"))
                        {
                            nodeList1.RemoveChild(nodeList2);
                        }
                    }
                }
            }

            XmlNodeList individualNodeList = xmlDoc.GetElementsByTagName(SharedFunctions.GetResourceString("INDIVIDUAL"));

            XmlNodeList xmlnode = xmlDoc.GetElementsByTagName(SharedFunctions.GetResourceString("INDIVIDUAL"));
            foreach (XmlElement node1 in xmlnode)
            {
                foreach (XmlElement node2 in node1)
                {
                    if (node2.Name == SharedFunctions.GetResourceString("IndividualType"))
                    {
                        string test = node2.InnerText;
                        node1.SetAttribute(SharedFunctions.GetResourceString("type"), test);
                    }

                    if (node2.Name == SharedFunctions.GetResourceString("IndividualType"))
                    {
                        node1.RemoveChild(node2);
                    }
                }

                foreach (XmlElement node2 in node1)
                {
                    if (node2.Name == SharedFunctions.GetResourceString("StatementIDValue"))
                    {
                        node1.RemoveChild(node2);
                    }
                }
            }

            XmlNodeList parcelxmlnode = xmlDoc.GetElementsByTagName(SharedFunctions.GetResourceString("PARCEL"));
            string keyId = string.Empty;
            foreach (XmlElement node1 in parcelxmlnode)
            {
                foreach (XmlElement node2 in node1)
                {
                    if (node2.Name == SharedFunctions.GetResourceString("StatementIDValue"))
                    {
                        keyId = node2.InnerText;
                        node1.RemoveChild(node2);
                    }
                }
            }

            XmlNodeList useCodeNode = xmlDoc.GetElementsByTagName(SharedFunctions.GetResourceString("USE_CODES"));
            XmlNode newEmployee;

            DataRow[] usecodeRow;
            string findExpression;
            string nextSibling = string.Empty;
            findExpression = "keyId";

            XmlNodeList useCodeNodes = xmlDoc.GetElementsByTagName(SharedFunctions.GetResourceString("USE_CODES"));

            foreach (XmlElement node1 in useCodeNodes)
            {
                foreach (XmlElement node2 in node1)
                {
                    if (node2.Name == "StatementID")
                    {
                        keyId = node2.InnerText;

                        if (node2.NextSibling != null)
                        {
                            nextSibling = node2.NextSibling.Name;
                        }
                    }
                    else if (node2.Name == nextSibling)
                    {
                        usecodeRow = this.submitDataset.USE_CODES.Select(this.submitDataset.USE_CODES.StatementIDColumn.ColumnName + "=" + keyId);
                        node1.RemoveAll();
                        newEmployee = xmlDoc.CreateElement("USE_CODE", null); //// CreateNode(XmlNodeType.Element, "USE_CODE",string.Empty);
                        ////newEmployee.InnerText = usecodeRow[0][1].ToString();

                        if (!string.IsNullOrEmpty(usecodeRow[0][1].ToString().Trim()) && usecodeRow[0][1].ToString().Trim() != "0" && usecodeRow[0][1].ToString().Trim() != "00")
                        {
                            newEmployee.InnerText = usecodeRow[0][1].ToString();
                        }
                        else
                        {
                            newEmployee.InnerText = string.Empty;
                        }

                        ////XmlNode x = xmlDoc.GetElementsByTagName("USE_CODE")[0].ChildNodes[0] ;
                        node1.AppendChild(newEmployee);
                        newEmployee = xmlDoc.CreateElement("USE_CODE", null); //// CreateNode(XmlNodeType.Element, "USE_CODE",string.Empty);
                        ////newEmployee.InnerText = usecodeRow[0][2].ToString();

                        if (!string.IsNullOrEmpty(usecodeRow[0][2].ToString().Trim()) && usecodeRow[0][2].ToString().Trim() != "0" && usecodeRow[0][2].ToString().Trim() != "00")
                        {
                            newEmployee.InnerText = usecodeRow[0][2].ToString();
                        }
                        else
                        {
                            newEmployee.InnerText = string.Empty;
                        }
                        
                        ////XmlNode x = xmlDoc.GetElementsByTagName("USE_CODE")[0].ChildNodes[0] ;
                        node1.AppendChild(newEmployee);
                        newEmployee = xmlDoc.CreateElement("USE_CODE", null); //// CreateNode(XmlNodeType.Element, "USE_CODE",string.Empty);
                        ////newEmployee.InnerText = usecodeRow[0][3].ToString();

                        if (!string.IsNullOrEmpty(usecodeRow[0][3].ToString().Trim()) && usecodeRow[0][2].ToString().Trim() != "0" && usecodeRow[0][3].ToString().Trim() != "00")
                        {
                            newEmployee.InnerText = usecodeRow[0][3].ToString();
                        }
                        else
                        {
                            newEmployee.InnerText = string.Empty;
                        }
                        
                        ////XmlNode x = xmlDoc.GetElementsByTagName("USE_CODE")[0].ChildNodes[0] ;
                        node1.AppendChild(newEmployee);
                    }
                }
            }

            foreach (XmlElement node1 in useCodeNode)
            {
                foreach (XmlElement node2 in node1)
                {
                    if (node2.Name == SharedFunctions.GetResourceString("StatementIDValue"))
                    {
                        node1.RemoveChild(node2);
                    }
                }
            }

            XmlNodeList supplementalxmlnode = xmlDoc.GetElementsByTagName(SharedFunctions.GetResourceString("SUPPLEMENTAL"));
            foreach (XmlElement node1 in supplementalxmlnode)
            {
                foreach (XmlElement node2 in node1)
                {
                    if (node2.Name == SharedFunctions.GetResourceString("StatementIDValue"))
                    {
                        node1.RemoveChild(node2);
                    }
                }
            }

            #region Removing Optional Tag

            XmlNodeList affidavitChildNodeList = null;
            XmlNodeList individualChildNodeList = null;
            XmlNodeList parcelChildNodeList = null;
            XmlNodeList useCodeChildNodeList = null;
            XmlNodeList supplimentalChildNodeList = null;
            XmlNodeList nodeNewList = xmlDoc.GetElementsByTagName("AFFIDAVIT");
            ArrayList affidavitArrayList = new ArrayList();
            affidavitArrayList.Add("COUNTY_NAME");
            affidavitArrayList.Add("EXEMPT_PROPERTY");
            affidavitArrayList.Add("FORESTLAND");
            affidavitArrayList.Add("OPEN_SPACE");
            affidavitArrayList.Add("HISTORIC");
            affidavitArrayList.Add("DOC_TYPE");
            affidavitArrayList.Add("DOC_DATE");
            affidavitArrayList.Add("GROSS_SELL_PRICE");
            affidavitArrayList.Add("TAXABLE_SELL_PRICE");
            affidavitArrayList.Add("EXCISE_TAX_STATE");
            affidavitArrayList.Add("EXCISE_TAX_LOCAL");
            affidavitArrayList.Add("STATE_TECH_FEE");
            affidavitArrayList.Add("TOTAL_DUE");
            affidavitArrayList.Add("RECEIPT_DATE");
            affidavitArrayList.Add("RECEIPT_NUM");

            ArrayList individualArrayList = new ArrayList();
            individualArrayList.Add("NAME");
            ArrayList parcelArrayList = new ArrayList();
            parcelArrayList.Add("NUMBER");
            parcelArrayList.Add("ASSESSED_VALUE");

            foreach (XmlNode node in nodeNewList)
            {
                if (node.HasChildNodes)
                {
                    affidavitChildNodeList = node.ChildNodes;
                }

                if (affidavitChildNodeList != null)
                {
                    for (int count = 0; count < affidavitChildNodeList.Count; count++)
                    {
                        if (affidavitChildNodeList[count].InnerText == "" && !affidavitChildNodeList[count].HasChildNodes)
                        {
                            // if (affidavitArrayList.BinarySearch(affidavitChildNodeList[count].Name) != 0)
                            if (!affidavitArrayList.Contains(affidavitChildNodeList[count].Name))
                            {
                                string nodeName = affidavitChildNodeList[count].Name;
                                node.RemoveChild(affidavitChildNodeList[count]);
                                count--;
                            }
                        }

                        if (affidavitChildNodeList[count].Name == "INDIVIDUAL")
                        {
                            if (affidavitChildNodeList[count].HasChildNodes)
                            {
                                individualChildNodeList = affidavitChildNodeList[count].ChildNodes;
                            }

                            for (int count1 = 0; count1 < individualChildNodeList.Count; count1++)
                            {
                                if (individualChildNodeList[count1].InnerText.Trim() == "" && !individualChildNodeList[count1].HasChildNodes)
                                {
                                    // if (individualArrayList.BinarySearch(individualChildNodeList[count1].Name) != 0)
                                    if (!individualArrayList.Contains(individualChildNodeList[count1].Name))
                                    {
                                        string nodeName = individualChildNodeList[count1].Name;
                                        affidavitChildNodeList[count].RemoveChild(individualChildNodeList[count1]);
                                        count1--;
                                    }
                                }
                            }
                        }
                        else if (affidavitChildNodeList[count].Name == "PARCEL")
                        {
                            if (affidavitChildNodeList[count].HasChildNodes)
                            {
                                parcelChildNodeList = affidavitChildNodeList[count].ChildNodes;
                            }

                            for (int count2 = 0; count2 < parcelChildNodeList.Count; count2++)
                            {
                                if (parcelChildNodeList[count2].InnerText.Trim() == "" && !parcelChildNodeList[count2].HasChildNodes)
                                {
                                    if (!parcelArrayList.Contains(parcelChildNodeList[count2].Name))
                                    {
                                        string nodeName = parcelChildNodeList[count2].Name;
                                        affidavitChildNodeList[count].RemoveChild(parcelChildNodeList[count2]);
                                        count2--;
                                    }
                                }
                            }
                        }
                        else if (affidavitChildNodeList[count].Name == "USE_CODES")
                        {
                            if (affidavitChildNodeList[count].HasChildNodes)
                            {
                                useCodeChildNodeList = affidavitChildNodeList[count].ChildNodes;
                            }

                            for (int count3 = 0; count3 < useCodeChildNodeList.Count; count3++)
                            {
                                if (useCodeChildNodeList[count3].InnerText.Trim() == "")
                                {
                                    affidavitChildNodeList[count].RemoveChild(useCodeChildNodeList[count3]);
                                    count3--;
                                }
                            }
                        }
                        else if (affidavitChildNodeList[count].Name == "SUPPLEMENTAL")
                        {
                            if (affidavitChildNodeList[count].HasChildNodes)
                            {
                                supplimentalChildNodeList = affidavitChildNodeList[count].ChildNodes;
                            }

                            for (int count4 = 0; count4 < supplimentalChildNodeList.Count; count4++)
                            {
                                if (supplimentalChildNodeList[count4].InnerText.Trim() == "" && !supplimentalChildNodeList[count4].HasChildNodes)
                                {
                                    string nodeName = supplimentalChildNodeList[count4].Name;
                                    affidavitChildNodeList[count].RemoveChild(supplimentalChildNodeList[count4]);
                                    count4--;
                                }
                            }
                        }
                    }
                }
            }
           
            #endregion

            xmlDoc.Save(this.xmlFilePath);
        }

        /// <summary>
        /// Removes the URL extension.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>return Url</returns>
        private string RemoveUrlExtension(string url)
        {
            string tempUrl = string.Empty;
            int length = 0;             
            length = url.LastIndexOf(".");
            tempUrl = url.Substring(0, length);
            return tempUrl;
        }

        /// <summary>
        /// Reets the service.
        /// </summary>
        /// <param name="webServiceAsmxUrl">The web service asmx URL.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="reetXml">The reet XML.</param>
        /// <param name="amend">if set to <c>true</c> [amend].</param>
        /// <returns>Returns replyXml</returns>
        private string ReetService(string webServiceAsmxUrl, string methodName, string userName, string password, string reetXml, bool amend)
        {
            try
            {
                string serviceName = string.Empty;
                object submitValue = null;
                string clientUrl = this.RemoveUrlExtension(webServiceAsmxUrl);

                WebClient client = new WebClient();

                // -Connect To the web service
                //// System.IO.Stream stream = client.OpenRead(webServiceAsmxUrl + "?wsdl");
                Stream stream = client.OpenRead(clientUrl + ".asmx?wsdl");

                // --Now read the WSDL file describing a // service.
                ServiceDescription description = ServiceDescription.Read(stream);
                serviceName = description.Services[0].Name;

                // --Initialize a service description importer.
                ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
                importer.ProtocolName = "Soap12"; // Use SOAP 1.2.
                ////importer.AddServiceDescription(description, webServiceAsmxUrl, wsdlUrl);
                importer.AddServiceDescription(description, null, null);

                // --Generate a proxy client. 
                importer.Style = ServiceDescriptionImportStyle.Client;

                // --Generate properties to represent pri // mitive values.
                importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties;

                // --Initialize a Code-DOM tree into which we will import the service.
                CodeNamespace codeNameSpace = new CodeNamespace();
                CodeCompileUnit compileUnit = new CodeCompileUnit();
                compileUnit.Namespaces.Add(codeNameSpace);

                // --Import the service into the Code-DOM tree. 

                // --This creates proxy code that uses the service.
                ServiceDescriptionImportWarnings warning = importer.Import(codeNameSpace, compileUnit);
                if (warning == 0) // --If zero then we are good to go
                {
                    // --Generate the proxy code 
                    CodeDomProvider provider1 = CodeDomProvider.CreateProvider("CSharp");

                    // --Compile the assembly proxy with the appropriate references
                    string[] assemblyReferences = new string[3] { "System.Web.Services.dll", "System.Xml.dll", "System.Configuration.dll" };
                    CompilerParameters parms = new CompilerParameters(assemblyReferences);
                    CompilerResults results = provider1.CompileAssemblyFromDom(parms, compileUnit);

                    ////foreach (CompilerError oops in results.Errors)
                    ////{
                    ////    MessageBox.Show(oops.ErrorText);
                    ////}

                    ////Invoke the web service method
                    object reetsubmitActual = results.CompiledAssembly.CreateInstance(serviceName);
                    Type reetsubmitType = results.CompiledAssembly.GetType(serviceName);

                    ////string actualWebserviceUrl = "https://test-fortress.wa.gov/dor/demo/content/DoingBusiness/MyAccount/Reeta/ReetSubmit.asmx";
                    ////string actualWebserviceUrl = this.webserviceUrl;

                    //// Acquiring the PropertyInfo of Url
                    PropertyInfo propertyInfo = reetsubmitType.GetProperty("Url");
                    Type urltype = propertyInfo.GetType();

                    //// If exists, set its Url property to new Url
                    if (propertyInfo != null)
                    {
                        //// MessageBox.Show(propertyInfo.Name);                
                        object webserviceUrlData = clientUrl + ".asmx";
                        propertyInfo.SetValue(reetsubmitActual, webserviceUrlData, null);
                    }

                    object[] args = new object[] { userName, password, reetXml, amend };
                    MethodInfo methodInfo = reetsubmitType.GetMethod(methodName);

                    if (methodInfo != null)
                    {
                        //// object res = methodInfo.Invoke(o, args1);
                        ////MessageBox.Show(Convert.ToString(methodInfo.Invoke(reetsubmitActual, args)));
                        submitValue = methodInfo.Invoke(reetsubmitActual, args);
                    }

                    return submitValue.ToString();

                    //// --Finally, Invoke the web service method 
                    ////object wsvcClass = results.CompiledAssembly.CreateInstance(serviceName);
                    ////MethodInfo mi = wsvcClass.GetType().GetMethod(methodName);

                    ////////wsvcClass.GetType().Namespace  
                    ////((WebClientProtocol)wsvcClass).Url = webServiceAsmxUrl;
                    ////submitValue = mi.Invoke(wsvcClass, args);
                    ////return submitValue.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Deletes the XML file.
        /// </summary>
        private void DeleteXmlFile()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + SharedFunctions.GetResourceString("TempDOR"));

            if (dirInfo.Exists)
            {
                FileInfo[] fileList = dirInfo.GetFiles();

                foreach (FileInfo file in fileList)
                {
                    if (file.Name != SharedFunctions.GetResourceString("ThumbsdbValue"))
                    {
                        System.IO.File.Delete(file.FullName);
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + SharedFunctions.GetResourceString("TempDOR"));
            }
        }

        /// <summary>
        /// Processes the reet XML.
        /// </summary>
        /// <returns>Returns Xml</returns>
        private string ProcessReetXml()
        {
            this.xmlFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + SharedFunctions.GetResourceString("TempDOR") + "\\" + "DOR" + DateTime.Now.Ticks + ".xml";
            string statementIdValue = string.Empty;

            if (this.submitCount == 0)
            {
                DataSet submitDataSet = new DataSet();
                DataRow[] submitDataRow;
                this.affidavitWorkQueueDataSet.AcceptChanges();
                submitDataRow = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.Select(SharedFunctions.GetResourceString("ValidStatusExpression"));
                submitDataSet.Merge(submitDataRow);

                ////Create a temp datatable which contain statementid column
                DataRow row;
                DataTable dataTable = new DataTable();
                dataTable.Columns.AddRange(new DataColumn[] { new DataColumn(this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.StatementIDColumn.ColumnName) });

                for (int i = 0; i < submitDataRow.Length; i++)
                {
                    row = dataTable.NewRow();
                    row[0] = submitDataSet.Tables[0].Rows[i][0];
                    dataTable.Rows.Add(row);
                }

                statementIdValue = TerraScanCommon.GetXmlString(dataTable);
                this.submitDataset = this.form1108Control.WorkItem.GetSubmitAffidavit(statementIdValue);
                this.affidavitCount = this.submitDataset.AFFIDAVIT.Rows.Count;
            }
            else
            {
                this.selectedCount = this.submitDataset.AFFIDAVIT.Rows.Count;
            }

            this.reetSubmitValue = this.submitDataset.GetXml();
            this.CreateXmlFile();

            StringBuilder finalData = new StringBuilder();
            StreamReader xmlData = new StreamReader(this.xmlFilePath);
            xmlData.BaseStream.Seek(0, SeekOrigin.Begin);
            while (xmlData.Peek() > -1)
            {
                finalData.Append(xmlData.ReadLine());
            }

            xmlData.Close();
            return finalData.ToString();
        }

        /// <summary>
        /// Validates the sale date.
        /// </summary>
        private void ValidateSaleDate()
        {
            string saleDateOperator = string.Empty;
            string expression1 = string.Empty;
            saleDateOperator = this.SearchSaleDateTextBox.Text.Trim();

            if (saleDateOperator.Length > 2)
            {
                expression1 = saleDateOperator.Substring(0, 2).ToString();
            }

            string expression2 = saleDateOperator.Substring(0, 1).ToString();

            if (this.doubleOperatorArrayList.Contains(expression1))
            {
                this.validSaleDate = saleDateOperator.Substring(2).Trim();
                this.ValidateDate(this.validSaleDate, expression1);
            }
            else if (this.singleOperatorArrayList.Contains(expression2))
            {
                this.validSaleDate = saleDateOperator.Substring(1).Trim();
                this.ValidateDate(this.validSaleDate, expression2);
            }
            else
            {
                this.ValidateDate(saleDateOperator, null);
            }
        }

        /// <summary>
        /// Validates the date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="expression">The expression.</param>
        private void ValidateDate(string date, string expression)
        {
            try
            {
                ////this.validSaleDate = DateTime.Parse(date).ToString(this.dateFormat); 
                this.validDate.Value = DateTime.Parse(date);
                this.SearchSaleDateTextBox.Text = expression + this.validDate.Value.ToString(this.dateFormat);
                this.validDateFormat = true;
            }
            catch (Exception)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("ExciseSaleDate"), ConfigurationManager.AppSettings["ApplicationName"].ToString() + " - Receipt Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.SearchSaleDateTextBox.Focus();
                this.validDateFormat = false;
            }
        }

        /// <summary>
        /// Initializes the operator.
        /// </summary>
        private void InitializeOperator()
        {
            // doubleOperatorArrayList            
            this.doubleOperatorArrayList.Add("<=");
            this.doubleOperatorArrayList.Add(">=");
            this.doubleOperatorArrayList.Add("!=");
            this.doubleOperatorArrayList.Add("<>");

            // singleOperatorArrayList            
            this.singleOperatorArrayList.Add("<");
            this.singleOperatorArrayList.Add(">");
            this.singleOperatorArrayList.Add("=");
        }

        /// <summary>
        /// Searches the record.
        /// </summary>
        private void SearchRecord()
        {
            // this.GetWhereClause();
            if (this.SearchButton.Enabled)
            {
                if (!string.IsNullOrEmpty(this.SearchSaleDateTextBox.Text.Trim()))
                {
                    this.ValidateSaleDate();

                    if (!this.validDateFormat)
                    {
                        return;
                    }
                }

                //// this.affidavitWorkQueueDataSet = F1107WorkItem.GetWorkQueueSearchResult(this.SearchParcelTextBox.Text.Trim(), this.SearchNameTextBox.Text.Trim(), this.SearchSaleDateTextBox.Text.Trim(), this.SearchAddressTextBox.Text.Trim(), this.SearchTaxCodeTextBox.Text.Trim(), this.SearchTreasurerTextBox.Text.Trim(), this.SearchAssessorTextBox.Text.Trim());                    
                this.affidavitWorkQueueDataSet = this.form1108Control.WorkItem.GetWorkQueueSearchResult(this.SearchParcelTextBox.Text.Trim(), this.SearchNameTextBox.Text.Trim(), this.SearchSaleDateTextBox.Text.Trim(), this.SearchAddressTextBox.Text.Trim(), this.SearchTaxCodeTextBox.Text.Trim(), this.SearchReceiptNumberTextBox.Text.Trim(), this.SearchStatementTextBox.Text.Trim());
                this.rowCount = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.Rows.Count;

                if (this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.Rows.Count > 0)
                {
                    this.AffidavitListDataGrid.Enabled = true;
                    this.AffidavitListDataGrid.Rows[0].Selected = true;
                    this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.Columns.Add(SharedFunctions.GetResourceString("ValidStatus"), System.Type.GetType("System.Boolean"));
                    this.AffidavitListDataGrid.DataSource = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.DefaultView;
                    this.SetRecordCount();
                    this.SelectUnSelectAll("True");
                    this.AffidavitListDataGrid.Focus();
                    this.WorkQueueAuditLink.Enabled = true;
                    this.EnableButtons(this, new DataEventArgs<string>("0"));
                    this.SubmitDORButton.Enabled = true && this.PermissionFiled.editPermission;
                }
                else
                {
                    this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.Columns.Add(SharedFunctions.GetResourceString("ValidStatus"), System.Type.GetType("System.Boolean"));
                    this.AffidavitListDataGrid.DataSource = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.DefaultView;
                    this.SetRecordCount();
                    this.SelectUnSelectAll("False");
                    this.AffidavitListDataGrid.Rows[0].Selected = false;
                    this.AffidavitListDataGrid.CurrentCell = null;
                    this.AffidavitListDataGrid.Enabled = false;
                    this.WorkQueueAuditLink.Enabled = false;
                    this.WorkQueueAuditLink.Text = SharedFunctions.GetResourceString("1107WorkQueueAuditLink");
                    this.ClearNonEditableFields();
                    this.DisableButtons(this, new DataEventArgs<string>("0"));
                    this.SubmitDORButton.Enabled = false;
                }

                if (this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.Rows.Count > this.AffidavitListDataGrid.NumRowsVisible)
                {
                    this.WorkQueueVScrollBar.Enabled = true;
                    this.WorkQueueVScrollBar.Visible = false;
                    this.AffidavitListDataGrid.Width = 786;
                }
                else
                {
                    this.WorkQueueVScrollBar.Enabled = false;
                    this.WorkQueueVScrollBar.Visible = true;
                    this.AffidavitListDataGrid.Width = 769;
                }
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the f1107 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1108_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkSpace();
                this.CustomizeDataGrid();

                this.configurationDatatable = this.form1108Control.WorkItem.ListConfigurationDetail;

                this.LoadAfiidavitListGrid();
                this.InitializeOperator();
                this.SearchButton.Enabled = false;

                if (!this.emptyRecord)
                {
                    this.SetDataBindingValue(0);
                    this.AffidavitListDataGrid.Rows[0].Selected = true;
                    this.AffidavitListDataGrid.Enabled = true;
                    this.ActiveControl = this.AffidavitListDataGrid;
                    this.ActiveControl.Focus();
                }
                else
                {
                    this.AffidavitListDataGrid.Rows[0].Selected = false;
                    this.AffidavitListDataGrid.Enabled = false;
                    this.ActiveControl = this.SearchStatementTextBox;
                    this.ActiveControl.Focus();
                }

                if (this.PermissionFiled.editPermission && !this.emptyRecord)
                {
                    this.SubmitDORButton.Enabled = true && this.PermissionFiled.editPermission;
                }
                else
                {
                    this.SubmitDORButton.Enabled = false;
                }

                if (this.AffidavitListDataGrid.OriginalRowCount == 0)
                {
                    this.AffidavitListDataGrid.RemoveDefaultSelection = true;
                }
                else
                {
                    this.AffidavitListDataGrid.RemoveDefaultSelection = false;
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
        /// Handles the Click event of the ClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadAfiidavitListGrid();

                ////if (!this.emptyRecord)
                ////{
                ////    this.SetDataBindingValue(0);
                ////    this.AffidavitListDataGrid.Rows[0].Selected = true;
                ////    ///this.AffidavitListDataGrid.Enabled = true;
                ////    ///this.ActiveControl = this.AffidavitListDataGrid;
                ////   ////this.ActiveControl.Focus();
                ////    this.AffidavitListDataGrid.Focus();
                ////}
                ////else
                ////{
                ////    this.AffidavitListDataGrid.Rows[0].Selected = false;
                ////    ////this.AffidavitListDataGrid.Enabled = false;
                ////    ////this.ActiveControl = this.SearchStatementTextBox;
                ////   ////this.ActiveControl.Focus();
                ////    this.SearchStatementTextBox.Focus();
                ////}
                 
                this.ClearFields();
                this.SearchButton.Enabled = false;
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
        /// Handles the CellClick event of the AffidavitListDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AffidavitListDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                 this.GridClick(e.RowIndex);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the AffidavitListDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AffidavitListDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < this.rowCount)
                {
                    if (e.ColumnIndex == 0)
                    {
                        ////Get the scroll position 
                        this.scrollPosition = this.AffidavitListDataGrid.FirstDisplayedScrollingRowIndex;
                        this.CheckedColumn();
                        ////Restore the scroll position    
                        this.AffidavitListDataGrid.FirstDisplayedScrollingRowIndex = this.scrollPosition;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the AffidavitListDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AffidavitListDataGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.GridClick(e.RowIndex);
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
                this.SearchRecord();
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
        /// Handles the KeyUp event of the SearchParcelTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SearchParcelTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Space:
                        {
                            break;
                        }

                    case Keys.Tab:
                        {
                            break;
                        }

                    default:
                        {
                            this.EnableSearch();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the SearchSaleDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SearchSaleDateTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        {
                            this.SearchRecord();
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Reets the service call.
        /// </summary>
        /// <param name="webServiceAsmxUrl">The web service asmx URL.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="reetXml">The reet XML.</param>
        /// <param name="amend">if set to <c>true</c> [amend].</param>
        /// <returns>Returns reply Xml</returns>
        private string ReetServiceCall(string webServiceAsmxUrl, string methodName, string userName, string password, string reetXml, bool amend)
        {
            try
            {
                string replyXml = string.Empty;
                replyXml = this.ReetService(webServiceAsmxUrl, methodName, userName, password, reetXml, amend);

                if (!string.IsNullOrEmpty(replyXml))
                {
                    int keyvalue = this.form1108Control.WorkItem.F1108_SaveReplyReetXml(reetXml, replyXml, TerraScanCommon.UserId);
                    return replyXml;
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("ReplyXml"), SharedFunctions.GetResourceString("SubmitDOR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            catch (UriFormatException)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("InvalidUrlFormat"), SharedFunctions.GetResourceString("SubmitDOR"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            catch (WebException webexp)
            {
                if (webexp.Message.Contains("404"))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("InvalidUrl"), SharedFunctions.GetResourceString("SubmitDOR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (webexp.Message.Contains("403"))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("InvalidUrl"), SharedFunctions.GetResourceString("SubmitDOR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (webexp.Message.Equals(SharedFunctions.GetResourceString("TimeOut")))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("TimeOut"), SharedFunctions.GetResourceString("SubmitDOR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (webexp.Message.Contains("500"))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("InvalidUrl"), SharedFunctions.GetResourceString("SubmitDOR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return null;
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return null;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                return null;
            }
        }

        /// <summary>
        /// Handles the Click event of the SubmitDORButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [SecurityPermissionAttribute(SecurityAction.Demand, Unrestricted = true)]
        private void SubmitDORButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string asmxUrl = string.Empty;                
                bool wrongReceiptNo = false;
                string errorStatus = string.Empty;
                string webMethod = string.Empty;
                string userName = string.Empty;
                string password = string.Empty;
                bool amendFlag = false;
                this.submitCount = 0;

                this.DeleteXmlFile();
                string processedData = string.Empty;
                string replyXmlValue = string.Empty;

                processedData = this.ProcessReetXml();

                ////StreamReader sr = new StreamReader(@"C:\Documents and Settings\karthikeyanv\Desktop\Sam.xml");
                ////processedData = sr.ReadToEnd();

                // To be removed
                /* DirectoryInfo dir = new DirectoryInfo("C:\\TempDOR");
                if (!dir.Exists)
                {
                    Directory.CreateDirectory("C:\\TempDOR");
                }
                string filepath = "C:\\TempDOR\\OneDOR" + DateTime.Now.Ticks + ".xml";
                StreamWriter sw1 = new StreamWriter(filepath);
                sw1.Write(processedData);
                sw1.Close(); */

                if (this.configurationDatatable != null)
                {
                    if (this.configurationDatatable.Rows.Count > 0)
                    {
                        asmxUrl = this.configurationDatatable.Rows[0][this.affidavitWorkQueueDataSet.ListConfigurationDetail.WebServiceURLColumn.ColumnName].ToString().Trim();                        
                        webMethod = this.configurationDatatable.Rows[0][this.affidavitWorkQueueDataSet.ListConfigurationDetail.MethodNameColumn.ColumnName].ToString().Trim();
                        userName = this.configurationDatatable.Rows[0][this.affidavitWorkQueueDataSet.ListConfigurationDetail.UserNameColumn.ColumnName].ToString().Trim();
                        password = this.configurationDatatable.Rows[0][this.affidavitWorkQueueDataSet.ListConfigurationDetail.PasswordColumn.ColumnName].ToString().Trim();
                        amendFlag = Convert.ToBoolean(this.configurationDatatable.Rows[0][this.affidavitWorkQueueDataSet.ListConfigurationDetail.AmendColumn.ColumnName]);
                    }
                }

                this.submitCount = this.submitCount + 1;
                ////StreamReader s = new StreamReader(@"c:\reet.xml");
                ////processedData = s.ReadToEnd(); 
                replyXmlValue = this.ReetServiceCall(asmxUrl, webMethod, userName, password, processedData, amendFlag);
                DataSet replyDataSet = new DataSet();
                if (replyXmlValue != null)
                {
                    replyDataSet.ReadXml(SharedFunctions.XmlParser(replyXmlValue));
                    replyDataSet.Namespace = string.Empty;
                    replyXmlValue = replyDataSet.GetXml();

                    //// To be removed
                    /* string filepath11 = "C:\\TempDOR\\OneDORReply" + DateTime.Now.Ticks + ".xml";
                    StreamWriter sw11 = new StreamWriter(filepath11);
                    sw11.Write(replyXml);
                    sw11.Close();  */
                    
                    if (!string.IsNullOrEmpty(replyXmlValue))
                    {
                        this.submitDataset = this.form1108Control.WorkItem.F1108_GetSubmitAffidavitReply(replyXmlValue, TerraScanCommon.UserId);

                        if (this.submitDataset.ErrorStatusDataTable.Rows.Count > 0)
                        {
                            errorStatus = this.submitDataset.ErrorStatusDataTable.Rows[0][this.submitDataset.ErrorStatusDataTable.ErrorStatusColumn.ColumnName].ToString();
                        }

                        this.submitDataset.Tables.Remove(this.submitDataset.ErrorStatusDataTable.TableName);

                        ////if(this.submitDataset.Tables[this.submitDataset.ErrorStatusDataTable.TableName].[this.submitDataset.ErrorStatusDataTable.ErrorStatusColumn.ColumnName]
                    }
                    else
                    {
                        return;
                    }

                    if (replyDataSet != null && replyDataSet.Tables["AffiDavit"].Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in replyDataSet.Tables["AffiDavit"].Rows)
                        {
                            if (String.Equals(dr1["RECEIPT_NUM"], "N/A"))
                            {
                                wrongReceiptNo = true;
                                break;
                            }
                            else
                            {
                                wrongReceiptNo = false;
                            }
                        }
                    }

                    //// IF ROW COUNT ZERO THEN THERE IS  NO ERROR
                    if (!string.IsNullOrEmpty(errorStatus))
                    {
                        if (errorStatus == "False")
                        {
                            if (this.submitDataset.AFFIDAVIT.Rows.Count == 0)
                            {
                                this.submitPass = true;
                                this.selectedCount = this.affidavitCount;
                                this.submitCount = 0;
                            }
                        }
                        else
                        {
                            if (this.submitDataset.AFFIDAVIT.Rows.Count > 0)
                            {
                                processedData = this.ProcessReetXml();

                                // To be removed
                                /* string filepath2 = "C:\\TempDOR\\TwoDOR" + DateTime.Now.Ticks + ".xml";
                                StreamWriter sw2 = new StreamWriter(filepath2);
                                sw2.Write(processedData);
                                sw2.Close(); */

                                this.submitCount = this.submitCount + 1;

                                replyXmlValue = this.ReetServiceCall(asmxUrl, webMethod, userName, password, processedData, false);

                                DataSet ds1 = new DataSet();
                                ds1.ReadXml(SharedFunctions.XmlParser(replyXmlValue));
                                ds1.Namespace = string.Empty;
                                replyXmlValue = ds1.GetXml();

                                // ds.ReadXml(;
                                // To be removed
                                /* string filepath12 = "C:\\TempDOR\\TwoDORReply" + DateTime.Now.Ticks + ".xml";
                                StreamWriter sw12 = new StreamWriter(filepath12);
                                sw12.Write(replyXml);
                                sw12.Close(); */

                                if (!string.IsNullOrEmpty(replyXmlValue))
                                {
                                    this.submitDataset = this.form1108Control.WorkItem.F1108_GetSubmitAffidavitReply(replyXmlValue, TerraScanCommon.UserId);

                                    if (this.submitDataset.ErrorStatusDataTable.Rows.Count > 0)
                                    {
                                        errorStatus = this.submitDataset.ErrorStatusDataTable.Rows[0][this.submitDataset.ErrorStatusDataTable.ErrorStatusColumn.ColumnName].ToString();
                                    }
                                }
                                else
                                {
                                    return;
                                }

                                this.submitPass = true;
                                this.submitCount = 0;
                            }
                            else
                            {
                                ////MessageBox.Show("The submission process failed.", "TerraScan - SubmitDOR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ////return;

                                if (MessageBox.Show(SharedFunctions.GetResourceString("FailSumbit") + " " + this.affidavitCount.ToString() + SharedFunctions.GetResourceString("ViewDORSumbit"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("FailExcise")), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    Hashtable dorSumbitDetails = new Hashtable();
                                    dorSumbitDetails.Add(SharedFunctions.GetResourceString("UserID"), TerraScanCommon.UserId);
                                    dorSumbitDetails.Add(SharedFunctions.GetResourceString("Update"), DateTime.Now);
                                    TerraScanCommon.ShowReport(110815, TerraScan.Common.Reports.Report.ReportType.Preview, dorSumbitDetails);
                                }

                                return;
                            }
                        }
                    }

                    //// IF NO ERROR OCCURED IN ( II )  PROCESS THEN
                    if (wrongReceiptNo)
                    {
                        MessageBox.Show("Invalid ReceiptNo Return !!", "TerraScan T2 - SubmitDOR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (errorStatus == "False" && this.submitDataset.AFFIDAVIT.Rows.Count == 0 && this.submitPass)
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("SuccessfullSumbit") + this.selectedCount.ToString() + SharedFunctions.GetResourceString("TotalDOR") + " " + this.affidavitCount.ToString() + SharedFunctions.GetResourceString("ViewDORSumbit"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("SuccessfullExcise")), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Hashtable dorSumbitDetails = new Hashtable();
                            dorSumbitDetails.Add(SharedFunctions.GetResourceString("UserID"), TerraScanCommon.UserId);
                            dorSumbitDetails.Add(SharedFunctions.GetResourceString("Update"), DateTime.Now);
                            TerraScanCommon.ShowReport(110815, TerraScan.Common.Reports.Report.ReportType.Preview, dorSumbitDetails);
                        }

                        this.LoadAfiidavitListGrid();
                    }
                    else
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("FailSumbit") + " " + this.affidavitCount.ToString() + SharedFunctions.GetResourceString("ViewDORSumbit"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("FailExcise")), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Hashtable dorSumbitDetails = new Hashtable();
                            dorSumbitDetails.Add(SharedFunctions.GetResourceString("UserID"), TerraScanCommon.UserId);
                            dorSumbitDetails.Add(SharedFunctions.GetResourceString("Update"), DateTime.Now);
                            TerraScanCommon.ShowReport(110815, TerraScan.Common.Reports.Report.ReportType.Preview, dorSumbitDetails);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Handles the Click event of the SelectAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.rowCount > 0 && this.AffidavitListDataGrid.SelectedRows.Count == 1)
                {
                    this.SelectUnSelectAll("True");
                    this.SubmitDORButton.Enabled = true && this.PermissionFiled.editPermission;
                }
                else if (this.rowCount > 0 && this.AffidavitListDataGrid.SelectedRows.Count > 1)
                {
                    for (int count = 0; count < this.rowCount; count++)
                    {
                        if (this.AffidavitListDataGrid.Rows[count].Selected)
                        {
                            this.AffidavitListDataGrid.Rows[count].Cells[SharedFunctions.GetResourceString("ValidStatus")].Value = "True";
                        }
                    }

                    this.AffidavitListDataGrid.RefreshEdit();
                    this.SubmitDORButton.Enabled = true && this.PermissionFiled.editPermission;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the UnselectAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UnselectAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.rowCount > 0 && this.AffidavitListDataGrid.SelectedRows.Count == 1)
                {
                    this.SelectUnSelectAll("False");
                    this.SubmitDORButton.Enabled = false;
                }
                else if (this.rowCount > 0 && this.AffidavitListDataGrid.SelectedRows.Count > 1)
                {
                    for (int count = 0; count < this.rowCount; count++)
                    {
                        if (this.AffidavitListDataGrid.Rows[count].Selected)
                        {
                            this.AffidavitListDataGrid.Rows[count].Cells[SharedFunctions.GetResourceString("ValidStatus")].Value = "False";
                        }
                    }

                    this.AffidavitListDataGrid.RefreshEdit();
                    this.SubmitDORButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the WorkQueueAuditLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkQueueAuditLink_Click(object sender, EventArgs e)
        {
            try
            {
                int auditLinkStatementID = 0;
                int.TryParse(this.AffidavitListDataGrid.Rows[0].Cells[this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.StatementIDColumn.ToString()].Value.ToString(), out auditLinkStatementID);
                
                if (auditLinkStatementID > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(90101);
                    formInfo.optionalParameters = new object[2];
                    formInfo.optionalParameters[0] = this.Tag;
                    formInfo.optionalParameters[1] = auditLinkStatementID;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }

                // Shows the report form.
                // changed the parameter type from string to int
                ////TerraScanCommon.ShowReport(110890, TerraScan.Common.Reports.Report.ReportType.Preview);
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
        /// Handles the TextChanged event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_TextChanged(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(this.SearchStatementTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SearchParcelTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SearchNameTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SearchSaleDateTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SearchAddressTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SearchTaxCodeTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SearchReceiptNumberTextBox.Text.Trim())))
            {
                this.EnableSearch();
            }
        }

        #region ChangeOrder
        /// <summary>
        /// Enable/Disable Vertical scrollbar based on the Column width changes
        /// -- Change Order Version4 --- Added by Latha
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void AffidavitListDataGrid_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (this.AffidavitListDataGrid.Height > 322)
            {
                this.WorkQueueVScrollBar.Enabled = true;
                this.WorkQueueVScrollBar.Visible = false;
            }
            else
            {
                this.WorkQueueVScrollBar.Enabled = false;
                this.WorkQueueVScrollBar.Visible = true;
            }
        }
        #endregion ChangeOrder

        #region IssueFixed - BugID 1304 Checking of check boxes causes grid to scroll
        ////Added by Latha
        
        /// <summary>
        /// Get Grid scroll position
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void AffidavitListDataGrid_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation.ToString() != "VerticalScroll")
            {
                this.DORPanel.AutoScrollPosition = new Point(e.NewValue, 0);
            }
        }
        #endregion IssueFixed - BugID 1304

        /// <summary>
        /// Handles the Scroll event of the DORPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ScrollEventArgs"/> instance containing the event data.</param>
        private void DORPanel_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                this.AffidavitListDataGrid.HorizontalScrollingOffset = e.NewValue;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        
        #endregion

        /// <summary>
        /// Sets the record count.
        /// </summary>
        private void SetRecordCount()
        {
            if (this.AffidavitListDataGrid.OriginalRowCount > 0)
            {
                if (this.AffidavitListDataGrid.OriginalRowCount > 1)
                {
                    this.RecordCountLabel.Text = "(" + this.AffidavitListDataGrid.OriginalRowCount + " records)";
                }
                else
                {
                    this.RecordCountLabel.Text = "(" + this.AffidavitListDataGrid.OriginalRowCount + " record)";
                }
            }
            else
            {
                this.RecordCountLabel.Text = string.Empty;
            }
        }
    }
}
