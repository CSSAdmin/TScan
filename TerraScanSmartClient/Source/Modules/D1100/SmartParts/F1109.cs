//--------------------------------------------------------------------------------------------
// <copyright file="F1109.cs" company="Congruent">
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
// 10 Nov 06		KARTHIKEYAN V	    Created
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

    /// <summary>
    /// F1108
    /// </summary>
    [SmartPart]
    public partial class F1109 : BaseSmartPart
    {
        #region Variable

        /// <summary>
        /// Set Date Format
        /// </summary>
        private readonly string dateFormat = SharedFunctions.GetResourceString("GDocWaterDateFormat");

        /// <summary>
        /// Created Instance for f1105Controller
        /// </summary>
        private F1109Controller form1109Control;

        /// <summary>
        /// formLabelInfo string array
        /// </summary>
        private string[] formLabelInfo = new string[2];

        /// <summary>
        /// reportOptionalParameter
        /// </summary>
        private Hashtable reportFileIdHashTable = new Hashtable();

        /// <summary>
        /// Created instance for Typed dataset AffidavitWorkQueue
        /// </summary>
        private AffidavitManagementQueue managementWorkQueueDataSet = new AffidavitManagementQueue();

        /// <summary>
        /// reportOptionalParameter
        /// </summary>
        private int rowCount;

        /// <summary>
        /// validSaleDate
        /// </summary>
        private string validSaleDate = string.Empty;

        /// <summary>
        /// IsValidDate
        /// </summary>
        private bool validDateFormat;

        /// <summary>
        /// filterApplied
        /// </summary>
        private bool filterApplied;

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
        /// appliedDataSet
        /// </summary>
        private DataSet appliedDataSet = new DataSet();

        /// <summary>
        /// statusFilterValue
        /// </summary>
        private string statusFilterValue = string.Empty;

        /// <summary>
        /// rollYearFilterValue
        /// </summary>
        private string rollYearFilterValue = string.Empty;

        /// <summary>
        /// fromDateValue
        /// </summary>
        private string fromDateValue = string.Empty;

        /// <summary>
        /// toDateValue
        /// </summary>
        private string dateValueTo = string.Empty;

        /// <summary>
        /// filterXml
        /// </summary>
        private string filterXml = string.Empty;

        /// <summary>
        /// Created Integer for filteredRowCount.
        /// </summary>
        private int filteredRowCount;

        #endregion

        #region Construtor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1109"/> class.
        /// </summary>
        public F1109()
        {
            this.InitializeComponent();
            this.DetailsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DetailsPictureBox.Height, this.DetailsPictureBox.Width, SharedFunctions.GetResourceString("Details"), 174, 150, 94);
            this.AgencyHeaderPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AgencyHeaderPictureBox.Height, this.AgencyHeaderPictureBox.Width, SharedFunctions.GetResourceString("Filter"), 0, 51, 0);
            this.pictureBox1.Image = ExtendedGraphics.GenerateVerticalImage(this.pictureBox1.Height, this.pictureBox1.Width, SharedFunctions.GetResourceString("AffidavitList"), 28, 81, 128);
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
        public F1109Controller Form1109Controll
        {
            get { return this.form1109Control as F1109Controller; }
            set { this.form1109Control = value; }
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
                if (!string.IsNullOrEmpty(this.AffidavitListDataGrid.Rows[currentRowId].Cells[this.managementWorkQueueDataSet.ListManagementQueue.StatementIDColumn.ColumnName].Value.ToString()))
                {
                    formInfo.optionalParameters = new object[] { this.AffidavitListDataGrid.Rows[currentRowId].Cells[this.managementWorkQueueDataSet.ListManagementQueue.StatementIDColumn.ColumnName].Value };
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
                if (!string.IsNullOrEmpty(this.AffidavitListDataGrid.Rows[currentRowId].Cells[this.managementWorkQueueDataSet.ListManagementQueue.StatementIDColumn.ColumnName].Value.ToString()))
                {
                    formInfo.optionalParameters = new object[] { this.AffidavitListDataGrid.Rows[currentRowId].Cells[this.managementWorkQueueDataSet.ListManagementQueue.StatementIDColumn.ColumnName].Value };
                }

                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Submits the queue button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D1100_F1109_AffidavitFormSmartPart_SubmitQueueButtonClick, Thread = ThreadOption.UserInterface)]
        public void SubmitQueueButtonClick(object sender, DataEventArgs<string> e)
        {
            try
            {
                FormInfo formInfo = TerraScanCommon.GetFormInfo(1108);
                formInfo.optionalParameters = null;
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
                string reportAuditId = string.Empty;
                this.Cursor = Cursors.WaitCursor;
                ////int currentRowId = this.GetRowIndex();
                int currentRowId = 0;
                if (this.AffidavitListDataGrid.CurrentCell != null)
                {
                    currentRowId = this.AffidavitListDataGrid.CurrentCell.RowIndex;
                }

                reportAuditId = this.AffidavitListDataGrid.Rows[currentRowId].Cells[this.managementWorkQueueDataSet.ListManagementQueue.StatementIDColumn.ColumnName].Value.ToString();

                this.reportFileIdHashTable.Clear();
                ////this.reportFileIdHashTable.Add("KeyName", "ReportFileID");
                ////this.reportFileIdHashTable.Add(SharedFunctions.GetResourceString("MIInputFileStatementID"), reportAuditId);
                this.reportFileIdHashTable.Add(SharedFunctions.GetResourceString("KeyID"), reportAuditId);

                // Shows the report form.
                // changed the parameter type from string to int
                TerraScanCommon.ShowReport(110500, TerraScan.Common.Reports.Report.ReportType.Preview, this.reportFileIdHashTable);
            }
            catch (SoapException ex)
            {
                // ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);

                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                // ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);

                MessageBox.Show(ex.Message);
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
                // changed the parameter type from string to int
                TerraScanCommon.ShowReport(110910, TerraScan.Common.Reports.Report.ReportType.Preview);
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
                source.DataSource = this.managementWorkQueueDataSet.ListManagementQueue;

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
        /// Loads the work space.
        /// </summary>
        private void LoadWorkSpace()
        {
            if (this.form1109Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.FormHeaderWorkSpace.Show(this.form1109Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.FormHeaderWorkSpace.Show(this.form1109Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            if (this.form1109Control.WorkItem.SmartParts.Contains(D1100SmartPartNames.AffidavitFormSmartPart))
            {
                this.AfdvtButtonWorkSpace.Show(this.form1109Control.WorkItem.SmartParts.Get(D1100SmartPartNames.AffidavitFormSmartPart));
            }
            else
            {
                this.AfdvtButtonWorkSpace.Show(this.form1109Control.WorkItem.SmartParts.AddNew<AffidavitFormSmartPart>("AffidavitFormSmartPart"));
            }

            this.formLabelInfo[0] = SharedFunctions.GetResourceString("F1109Header");
            this.formLabelInfo[1] = string.Empty;

            this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));
        }

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizeDataGrid()
        {
            this.AffidavitListDataGrid.AllowUserToResizeColumns = true;
            this.AffidavitListDataGrid.AutoGenerateColumns = false;
            this.AffidavitListDataGrid.AllowUserToResizeRows = false;
            this.AffidavitListDataGrid.StandardTab = true;
            this.AffidavitListDataGrid.PrimaryKeyColumnName = this.managementWorkQueueDataSet.ListManagementQueue.StatementIDColumn.ColumnName;

            this.AffidavitListDataGrid.Columns[this.managementWorkQueueDataSet.ListManagementQueue.StatementNumberColumn.ColumnName].DataPropertyName = this.managementWorkQueueDataSet.ListManagementQueue.StatementNumberColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.managementWorkQueueDataSet.ListManagementQueue.ParcelNumberColumn.ColumnName].DataPropertyName = this.managementWorkQueueDataSet.ListManagementQueue.ParcelNumberColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.managementWorkQueueDataSet.ListManagementQueue.NameColumn.ColumnName].DataPropertyName = this.managementWorkQueueDataSet.ListManagementQueue.NameColumn.ColumnName;  
            this.AffidavitListDataGrid.Columns[this.managementWorkQueueDataSet.ListManagementQueue.GrantorColumn.ColumnName].DataPropertyName = this.managementWorkQueueDataSet.ListManagementQueue.GrantorColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.managementWorkQueueDataSet.ListManagementQueue.DocumentDateColumn.ColumnName].DataPropertyName = this.managementWorkQueueDataSet.ListManagementQueue.DocumentDateColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.managementWorkQueueDataSet.ListManagementQueue.StreetAddressColumn.ColumnName].DataPropertyName = this.managementWorkQueueDataSet.ListManagementQueue.StreetAddressColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.managementWorkQueueDataSet.ListManagementQueue.IsExemptColumn.ColumnName].DataPropertyName = this.managementWorkQueueDataSet.ListManagementQueue.IsExemptColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.managementWorkQueueDataSet.ListManagementQueue.TreasurerColumn.ColumnName].DataPropertyName = this.managementWorkQueueDataSet.ListManagementQueue.TreasurerColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.managementWorkQueueDataSet.ListManagementQueue.AssessorColumn.ColumnName].DataPropertyName = this.managementWorkQueueDataSet.ListManagementQueue.AssessorColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.managementWorkQueueDataSet.ListManagementQueue.StatementIDColumn.ColumnName].DataPropertyName = this.managementWorkQueueDataSet.ListManagementQueue.StatementIDColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.managementWorkQueueDataSet.ListManagementQueue.StatementNumberColumn.ColumnName].DataPropertyName = this.managementWorkQueueDataSet.ListManagementQueue.StatementNumberColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.managementWorkQueueDataSet.ListManagementQueue.LocationColumn.ColumnName].DataPropertyName = this.managementWorkQueueDataSet.ListManagementQueue.LocationColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.managementWorkQueueDataSet.ListManagementQueue.TaxableSalePriceColumn.ColumnName].DataPropertyName = this.managementWorkQueueDataSet.ListManagementQueue.TaxableSalePriceColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.managementWorkQueueDataSet.ListManagementQueue.TotalDueColumn.ColumnName].DataPropertyName = this.managementWorkQueueDataSet.ListManagementQueue.TotalDueColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.managementWorkQueueDataSet.ListManagementQueue.ReceiptNumberColumn.ColumnName].DataPropertyName = this.managementWorkQueueDataSet.ListManagementQueue.ReceiptNumberColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.managementWorkQueueDataSet.ListManagementQueue.UseCodeColumn.ColumnName].DataPropertyName = this.managementWorkQueueDataSet.ListManagementQueue.UseCodeColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.managementWorkQueueDataSet.ListManagementQueue.GranteeColumn.ColumnName].DataPropertyName = this.managementWorkQueueDataSet.ListManagementQueue.GranteeColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.managementWorkQueueDataSet.ListManagementQueue.SubmittedDateColumn.ColumnName].DataPropertyName = this.managementWorkQueueDataSet.ListManagementQueue.SubmittedDateColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.managementWorkQueueDataSet.ListManagementQueue.SubmittedUserByColumn.ColumnName].DataPropertyName = this.managementWorkQueueDataSet.ListManagementQueue.SubmittedUserByColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.managementWorkQueueDataSet.ListManagementQueue.SubmittedByColumn.ColumnName].DataPropertyName = this.managementWorkQueueDataSet.ListManagementQueue.SubmittedByColumn.ColumnName;            
        }

        /// <summary>
        /// Clears the filter.
        /// </summary>
        private void ClearFilter()
        {
            this.statusFilterValue = string.Empty;
            this.rollYearFilterValue = string.Empty;
            this.fromDateValue = string.Empty;
            this.dateValueTo = string.Empty;
        }

        /// <summary>
        /// Clears the non editable fields.
        /// </summary>
        private void ClearNonEditableFields()
        {
            this.StatementNumberTextBox.Text = string.Empty;
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
            this.SubmittedDateTextBox.Text = string.Empty;
            this.SubmittedByTextBox.Text = string.Empty;
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
            this.SearchAssessorTextBox.Text = string.Empty;
            this.SearchTreasurerTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Sets the data binding value.
        /// </summary>
        /// <param name="rowId">The row id.</param>
        private void SetDataBindingValue(int rowId)
        {
            if (this.rowCount > 0)
            {
                if (rowId >= 0)
                {
                    decimal taxableSalePrice = 0;
                    decimal totalDue = 0;
                    this.StatementNumberTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.managementWorkQueueDataSet.ListManagementQueue.StatementNumberColumn.ColumnName].Value.ToString();
                    this.StreetAddressTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.managementWorkQueueDataSet.ListManagementQueue.StreetAddressColumn.ColumnName].Value.ToString();
                    this.LocationTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.managementWorkQueueDataSet.ListManagementQueue.LocationColumn.ColumnName].Value.ToString();
                    this.SaledateTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.managementWorkQueueDataSet.ListManagementQueue.DocumentDateColumn.ColumnName].Value.ToString();
                    this.TaxCodeTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.managementWorkQueueDataSet.ListManagementQueue.IsExemptColumn.ColumnName].Value.ToString();
                    decimal.TryParse(this.AffidavitListDataGrid.Rows[rowId].Cells[this.managementWorkQueueDataSet.ListManagementQueue.TaxableSalePriceColumn.ColumnName].Value.ToString(), out taxableSalePrice);
                    this.TaxableSalePriceTextBox.Text = taxableSalePrice.ToString("$ #,##0.00");
                    decimal.TryParse(this.AffidavitListDataGrid.Rows[rowId].Cells[this.managementWorkQueueDataSet.ListManagementQueue.TotalDueColumn.ColumnName].Value.ToString(), out totalDue);
                    this.TotalDueTextBox.Text = totalDue.ToString("$ #,##0.00");
                    this.GrantorTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.managementWorkQueueDataSet.ListManagementQueue.GrantorColumn.ColumnName].Value.ToString();
                    this.GranteeTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.managementWorkQueueDataSet.ListManagementQueue.GranteeColumn.ColumnName].Value.ToString();
                    this.ParcelNumberTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.managementWorkQueueDataSet.ListManagementQueue.ParcelNumberColumn.ColumnName].Value.ToString();
                    this.TreasurerTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.managementWorkQueueDataSet.ListManagementQueue.TreasurerColumn.ColumnName].Value.ToString();
                    this.AssessorTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.managementWorkQueueDataSet.ListManagementQueue.AssessorColumn.ColumnName].Value.ToString();
                    this.ReciptNumberTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.managementWorkQueueDataSet.ListManagementQueue.ReceiptNumberColumn.ColumnName].Value.ToString();
                    this.UseCodesTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.managementWorkQueueDataSet.ListManagementQueue.UseCodeColumn.ColumnName].Value.ToString();
                    this.SubmittedByTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.managementWorkQueueDataSet.ListManagementQueue.SubmittedByColumn.ColumnName].Value.ToString();
                    this.SubmittedDateTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells[this.managementWorkQueueDataSet.ListManagementQueue.SubmittedDateColumn.ColumnName].Value.ToString();
                }
            }
        }

        /// <summary>
        /// Populates the filter status combo.
        /// </summary>
        private void PopulateFilterStatus()
        {
            ArrayList ht = new ArrayList();
            ht.Add(SharedFunctions.GetResourceString("SubmittedToState"));
            ht.Add(SharedFunctions.GetResourceString("NotSubmittedToState"));
            ht.Add(SharedFunctions.GetResourceString("ApprovedByTreasurerOnly"));
            ht.Add(SharedFunctions.GetResourceString("ApprovedByAssessorOnly"));
            ht.Add(SharedFunctions.GetResourceString("Approved"));
            ht.Add(SharedFunctions.GetResourceString("Rejected"));
            ht.Add(SharedFunctions.GetResourceString("Unverified"));
            ht.Add(SharedFunctions.GetResourceString("Receipted"));
            ht.Add(SharedFunctions.GetResourceString("NotReceipted"));
            ht.Add("ReceiptDate Range");
            this.StatusFilterComboBox.DataSource = ht;

            DataTable rollYearTable = new DataTable();
            ////DataColumn dc = new DataColumn();
            ////dc.DataType = DbType.Int32;
            DataColumn[] rollYearColumn = { new DataColumn(this.managementWorkQueueDataSet.ListRollYear.RollYearColumn.ColumnName, System.Type.GetType("System.String")), new DataColumn(this.managementWorkQueueDataSet.ListRollYear.CurrentRollYearColumn.ColumnName, System.Type.GetType("System.Int32")) };
            rollYearTable.Columns.AddRange(rollYearColumn);
            DataRow dataRowAll = rollYearTable.NewRow();
            dataRowAll[this.managementWorkQueueDataSet.ListRollYear.RollYearColumn.ColumnName] = SharedFunctions.GetResourceString("AllValue");
            dataRowAll[this.managementWorkQueueDataSet.ListRollYear.CurrentRollYearColumn.ColumnName] = 0;
            rollYearTable.Rows.Add(dataRowAll);
            rollYearTable.Merge(this.form1109Control.WorkItem.ListRollYear);

            if (rollYearTable.Rows.Count > 0)
            {
                this.RollYearFilterComboBox.DisplayMember = this.managementWorkQueueDataSet.ListRollYear.RollYearColumn.ColumnName;
                this.RollYearFilterComboBox.DataSource = rollYearTable;

                DataRow[] dr;
                DataSet tempDataSet = new DataSet();
                dr = rollYearTable.Select(this.managementWorkQueueDataSet.ListRollYear.CurrentRollYearColumn.ColumnName + "= 1");
                tempDataSet.Merge(dr);                

                if (tempDataSet.Tables.Count > 0)
                {
                    this.RollYearFilterComboBox.Text = tempDataSet.Tables[0].Rows[0][0].ToString();
                    ////int.TryParse(this.RollYearFilterComboBox.Text, out this.rollYearValue);
                }
                else
                {
                    ////this.rollYearValue = 0;
                    this.RollYearFilterComboBox.Text = SharedFunctions.GetResourceString("AllValue");
                }
            }

            this.FromDateTextBox.Text = DateTime.Now.ToShortDateString();
            this.ToDateTextBox.Text = DateTime.Now.ToShortDateString();
        }

        /// <summary>
        /// Loads the management queue grid.
        /// </summary>
        private void LoadManagementQueueGrid()
        {
            int rollYearClearValue = 0;
            if (this.RollYearFilterComboBox.SelectedIndex == 0)
            {
                this.managementWorkQueueDataSet = this.form1109Control.WorkItem.ListManagementQueue(null, null, null, null, null, null, null, 0, null);
            }
            else
            {
                int.TryParse(this.RollYearFilterComboBox.Text, out rollYearClearValue);
                this.managementWorkQueueDataSet = this.form1109Control.WorkItem.ListManagementQueue(null, null, null, null, null, null, null, rollYearClearValue, null);
            }

            this.rowCount = this.managementWorkQueueDataSet.ListManagementQueue.Rows.Count;
            this.AffidavitListDataGrid.DataSource = this.managementWorkQueueDataSet.ListManagementQueue.DefaultView;
            this.SetRecordCount();
            if (this.rowCount > 0)
            {
                ////this.PopulateFilterStatus();
                this.WorkQueueAuditLink.Text = SharedFunctions.GetResourceString("1107WorkQueueAuditLink") + " " + this.AffidavitListDataGrid.Rows[0].Cells[this.managementWorkQueueDataSet.ListManagementQueue.StatementIDColumn.ToString()].Value.ToString();
                TerraScanCommon.SetDataGridViewPosition(this.AffidavitListDataGrid, 0);
                this.WorkQueueAuditLink.Enabled = true;
                this.EnableButtons(this, new DataEventArgs<string>("0"));
                this.SetDataBindingValue(0);
                this.AffidavitListDataGrid.Rows[0].Selected = true;
                this.AffidavitListDataGrid.Enabled = true;
                this.AffidavitListDataGrid.Focus();
            }
            else
            {
                ////this.DisableControls();
                this.SearchButton.Enabled = false;
                this.ClearNonEditableFields();
                this.WorkQueueAuditLink.Enabled = false;
                this.WorkQueueAuditLink.Text = SharedFunctions.GetResourceString("1107WorkQueueAuditLink");
                this.DisableButtons(this, new DataEventArgs<string>("0"));
                this.AffidavitListDataGrid.Rows[0].Selected = false;
                this.AffidavitListDataGrid.CurrentCell = null;
                this.AffidavitListDataGrid.Enabled = false;
                this.ApplyButton.Enabled = false;
                this.RemoveButton.Enabled = false;
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
                this.AffidavitListDataGrid.Width = 768;
            }
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
                    this.SetDataBindingValue(rowIndex);
                    this.WorkQueueAuditLink.Text = SharedFunctions.GetResourceString("1107WorkQueueAuditLink") + " " + this.AffidavitListDataGrid.Rows[rowIndex].Cells[this.managementWorkQueueDataSet.ListManagementQueue.StatementIDColumn.ToString()].Value.ToString();
                }
            }
        }

        /// <summary>
        /// Enables the search.
        /// </summary>
        private void EnableSearch()
        {
            if (!string.IsNullOrEmpty(this.SearchParcelTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.SearchNameTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.SearchSaleDateTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.SearchAddressTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.SearchTaxCodeTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.SearchTreasurerTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.SearchAssessorTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.SearchStatementTextBox.Text.Trim()))
            {
                this.SearchButton.Enabled = true;                
            }
            else
            {
                this.SearchButton.Enabled = false;                
            }
        }

        /// <summary>
        /// Shows the attachment calender.
        /// </summary>
        private void ShowFromDateCalender()
        {
            this.FromDateMonthCalander.Visible = true;
            this.FromDateMonthCalander.ScrollChange = 1;
            this.FromDateMonthCalander.BringToFront();
            //// Display the Calender control near the Calender Picture box.
            this.FromDateMonthCalander.Left = this.FilterPanel.Left + this.FromDatepanel.Left + this.FromDateButton.Left + this.FromDateButton.Width;
            this.FromDateMonthCalander.Top = this.FilterPanel.Top + this.FromDatepanel.Top + this.FromDateButton.Top;
            this.FromDateMonthCalander.Tag = this.FromDateButton.Tag;
            this.FromDateMonthCalander.Focus();

            if (!string.IsNullOrEmpty(this.FromDateTextBox.Text))
            {
                this.FromDateMonthCalander.SetDate(Convert.ToDateTime(this.FromDateTextBox.Text));
            }
        }

        /// <summary>
        /// Shows the attachment calender.
        /// </summary>
        private void ShowToDateCalender()
        {
            this.ToDateMonthCalander.Visible = true;
            this.ToDateMonthCalander.ScrollChange = 1;
            this.ToDateMonthCalander.BringToFront();
            //// Display the Calender control near the Calender Picture box.
            this.ToDateMonthCalander.Left = this.FilterPanel.Left + this.ToDatePanel.Left + this.ToDateButton.Left + this.ToDateButton.Width;
            this.ToDateMonthCalander.Top = this.FilterPanel.Top + this.ToDatePanel.Top + this.ToDateButton.Top;
            this.ToDateMonthCalander.Tag = this.ToDateButton.Tag;
            this.ToDateMonthCalander.Focus();

            if (!string.IsNullOrEmpty(this.ToDateTextBox.Text))
            {
                this.ToDateMonthCalander.SetDate(Convert.ToDateTime(this.ToDateTextBox.Text));
            }
        }

        /// <summary>
        /// Loads the filter grid.
        /// </summary>
        private void LoadFilterGrid()
        {
            if (this.rowCount > 0)
            {
                this.AffidavitListDataGrid.Enabled = true;
                this.AffidavitListDataGrid.Rows[0].Selected = true;
                this.AffidavitListDataGrid.DataSource = this.managementWorkQueueDataSet.ListManagementQueue.DefaultView;
                this.SetRecordCount();
                this.AffidavitListDataGrid.Focus();
                this.WorkQueueAuditLink.Enabled = true;
                this.EnableButtons(this, new DataEventArgs<string>("0"));
            }
            else
            {
                ////DataTable tempDataTable = new DataTable();
                ////tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.StatementIDColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.StreetAddressColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.LocationColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.DocumentDateColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.IsExemptColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.TaxableSalePriceColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.TotalDueColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.GrantorColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.GranteeColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.ParcelNumberColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.TreasurerColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.AssessorColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.ReceiptNumberColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.UseCodeColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.SubmittedByColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.SubmittedDateColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.SubmittedUserByColumn.ColumnName) });

                ////this.AffidavitListDataGrid.DataSource = tempDataTable;

                this.AffidavitListDataGrid.DataSource = this.managementWorkQueueDataSet.ListManagementQueue.DefaultView;
                this.SetRecordCount();
                this.AffidavitListDataGrid.Rows[0].Selected = false;
                this.AffidavitListDataGrid.CurrentCell = null;
                this.AffidavitListDataGrid.Enabled = false;
                this.WorkQueueAuditLink.Enabled = false;
                this.WorkQueueAuditLink.Text = SharedFunctions.GetResourceString("1107WorkQueueAuditLink");
                this.ClearNonEditableFields();
                this.DisableButtons(this, new DataEventArgs<string>("0"));
            }

            if (this.AffidavitListDataGrid.Rows.Count > this.AffidavitListDataGrid.NumRowsVisible)
            {
                this.WorkQueueVScrollBar.Enabled = true;
                this.WorkQueueVScrollBar.Visible = false;
                this.AffidavitListDataGrid.Width = 786;
            }
            else
            {
                this.WorkQueueVScrollBar.Enabled = false;
                this.WorkQueueVScrollBar.Visible = true;
                this.AffidavitListDataGrid.Width = 768;
            }
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
        /// Creates the parameter XML.
        /// </summary>
        private void CreateParameterXml()
        {
            DataTable tempDataTable = new DataTable();
            tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(SharedFunctions.GetResourceString("ParcelNumber")), new DataColumn(SharedFunctions.GetResourceString("Name")), new DataColumn("ReceiptDate"), new DataColumn(SharedFunctions.GetResourceString("Address")), new DataColumn(SharedFunctions.GetResourceString("TaxCode")), new DataColumn(SharedFunctions.GetResourceString("Treasurer")), new DataColumn(SharedFunctions.GetResourceString("Assessor")), new DataColumn(SharedFunctions.GetResourceString("StatusFilterID")), new DataColumn(SharedFunctions.GetResourceString("RollYear")), new DataColumn(SharedFunctions.GetResourceString("FromDate")), new DataColumn(SharedFunctions.GetResourceString("ToDate")) });
            DataRow tempDataRow = tempDataTable.NewRow();
            tempDataRow[SharedFunctions.GetResourceString("ParcelNumber")] = this.SearchParcelTextBox.Text;
            tempDataRow[SharedFunctions.GetResourceString("Name")] = this.SearchNameTextBox.Text;
            tempDataRow["ReceiptDate"] = this.SearchSaleDateTextBox.Text;
            tempDataRow[SharedFunctions.GetResourceString("Address")] = this.SearchAddressTextBox.Text;
            tempDataRow[SharedFunctions.GetResourceString("TaxCode")] = this.SearchTaxCodeTextBox.Text;
            tempDataRow[SharedFunctions.GetResourceString("Treasurer")] = this.SearchTreasurerTextBox.Text;
            tempDataRow[SharedFunctions.GetResourceString("Assessor")] = this.SearchAssessorTextBox.Text;
            tempDataRow[SharedFunctions.GetResourceString("StatusFilterID")] = this.statusFilterValue;
            tempDataRow[SharedFunctions.GetResourceString("RollYear")] = this.rollYearFilterValue;
            tempDataRow[SharedFunctions.GetResourceString("FromDate")] = this.fromDateValue;
            tempDataRow[SharedFunctions.GetResourceString("ToDate")] = this.dateValueTo;
            tempDataTable.Rows.Add(tempDataRow);

            this.filterXml = TerraScanCommon.GetXmlString(tempDataTable);
        }

        /// <summary>
        /// Searches the record.
        /// </summary>
        private void SearchRecord()
        {            
            int rollYearSearchValue = 0;
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

                if (this.RollYearFilterComboBox.SelectedIndex == 0)
                {
                    if (this.filterApplied)
                    {
                        this.CreateParameterXml();
                        this.managementWorkQueueDataSet = this.form1109Control.WorkItem.FilterSearchAffidavit(this.filterXml);
                    }
                    else
                    {
                        this.managementWorkQueueDataSet = this.form1109Control.WorkItem.ListManagementQueue(this.SearchParcelTextBox.Text.Trim(), this.SearchNameTextBox.Text.Trim(), this.SearchSaleDateTextBox.Text.Trim(), this.SearchAddressTextBox.Text.Trim(), this.SearchTaxCodeTextBox.Text.Trim(), this.SearchTreasurerTextBox.Text.Trim(), this.SearchAssessorTextBox.Text.Trim(), 0, this.SearchStatementTextBox.Text.Trim());
                    }
                }
                else
                {
                    if (this.filterApplied)
                    {
                        this.CreateParameterXml();
                        this.managementWorkQueueDataSet = this.form1109Control.WorkItem.FilterSearchAffidavit(this.filterXml);
                    }
                    else
                    {
                        int.TryParse(this.RollYearFilterComboBox.Text, out rollYearSearchValue);
                        this.managementWorkQueueDataSet = this.form1109Control.WorkItem.ListManagementQueue(this.SearchParcelTextBox.Text.Trim(), this.SearchNameTextBox.Text.Trim(), this.SearchSaleDateTextBox.Text.Trim(), this.SearchAddressTextBox.Text.Trim(), this.SearchTaxCodeTextBox.Text.Trim(), this.SearchTreasurerTextBox.Text.Trim(), this.SearchAssessorTextBox.Text.Trim(), rollYearSearchValue, this.SearchStatementTextBox.Text.Trim());
                    }
                }

                this.LoadFilterGrid();
            }
        } 

        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the F1109 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1109_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkSpace();
                this.CustomizeDataGrid();

                this.PopulateFilterStatus();
                this.LoadManagementQueueGrid();
                this.InitializeOperator();                

                this.SearchButton.Enabled = false;
                this.RemoveButton.Enabled = false;                
                this.FromDatepanel.Enabled = false;
                this.ToDatePanel.Enabled = false;
                this.ActiveControl = this.StatusFilterComboBox;
                StatusFilterComboBox.Focus();

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
        /// Handles the Click event of the ClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.filterApplied)
                {
                    if (this.appliedDataSet.Tables.Count >= 2)
                    {
                        ////this.AffidavitListDataGrid.DataSource = this.appliedDataSet.Tables[1];
                        this.rowCount = this.filteredRowCount;
                        
                      if (this.filteredRowCount > 0)
                        {
                            this.AffidavitListDataGrid.CurrentCell = null;
                            this.AffidavitListDataGrid.Enabled = true;
                            this.AffidavitListDataGrid.Rows[0].Selected = true;
                            this.AffidavitListDataGrid.DataSource = this.appliedDataSet.Tables[1].DefaultView;
                            this.SetRecordCount();
                            this.AffidavitListDataGrid.Focus();
                            this.WorkQueueAuditLink.Enabled = true;                            
                            this.EnableButtons(this, new DataEventArgs<string>("0"));
                        }
                        else
                        {
                            ////DataTable tempDataTable = new DataTable();
                            ////tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.StatementIDColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.StreetAddressColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.LocationColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.DocumentDateColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.IsExemptColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.TaxableSalePriceColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.TotalDueColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.GrantorColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.GranteeColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.ParcelNumberColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.TreasurerColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.AssessorColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.ReceiptNumberColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.UseCodeColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.SubmittedByColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.SubmittedDateColumn.ColumnName), new DataColumn(this.managementWorkQueueDataSet.ListManagementQueue.SubmittedUserByColumn.ColumnName) });

                            ////this.AffidavitListDataGrid.DataSource = tempDataTable;

                            this.AffidavitListDataGrid.DataSource = this.appliedDataSet.Tables[1].DefaultView;
                            this.SetRecordCount();
                            this.AffidavitListDataGrid.Rows[0].Selected = false;
                            this.AffidavitListDataGrid.CurrentCell = null;
                            this.AffidavitListDataGrid.Enabled = false;
                            this.WorkQueueAuditLink.Enabled = false;
                            this.WorkQueueAuditLink.Text = SharedFunctions.GetResourceString("1107WorkQueueAuditLink");
                            this.ClearNonEditableFields();
                            this.DisableButtons(this, new DataEventArgs<string>("0"));
                        }                        

                       if (this.rowCount > this.AffidavitListDataGrid.NumRowsVisible)
                        {
                            this.WorkQueueVScrollBar.Enabled = true;
                            this.WorkQueueVScrollBar.Visible = false;
                        }
                        else
                        {
                            this.WorkQueueVScrollBar.Enabled = false;
                            this.WorkQueueVScrollBar.Visible = true;
                        }

                        this.LoadFilterGrid();
                    }
                }
                else
                {
                    this.LoadManagementQueueGrid();
                }

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
        /// Handles the Click event of the WorkQueueAuditLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkQueueAuditLink_Click(object sender, EventArgs e)
        {
            try
            {
                int auditLinkStatementID = 0;
                int.TryParse(this.AffidavitListDataGrid.Rows[0].Cells[this.managementWorkQueueDataSet.ListManagementQueue.StatementIDColumn.ToString()].Value.ToString(), out auditLinkStatementID);

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
                ////TerraScanCommon.ShowReport(110990, TerraScan.Common.Reports.Report.ReportType.Preview);
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
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
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
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the FromDateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FromDateButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Calls the method to show the calender control.
                this.ShowFromDateCalender();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the FromDateMonthCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void FromDateMonthCalander_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.FromDateTextBox.Text = e.Start.ToShortDateString();
                this.FromDateMonthCalander.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the ToDateMonthCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void ToDateMonthCalander_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.ToDateTextBox.Text = e.Start.ToShortDateString();
                this.ToDateMonthCalander.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the FromDateMonthCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void FromDateMonthCalander_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    this.FromDateTextBox.Text = this.FromDateMonthCalander.SelectionStart.ToShortDateString();
                    this.FromDateMonthCalander.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the ToDateMonthCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ToDateMonthCalander_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    this.ToDateTextBox.Text = this.ToDateMonthCalander.SelectionStart.ToShortDateString();
                    this.ToDateMonthCalander.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the FromDateMonthCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FromDateMonthCalander_Leave(object sender, EventArgs e)
        {
            this.FromDateMonthCalander.Visible = false;
        }

        /// <summary>
        /// Handles the Leave event of the ToDateMonthCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ToDateMonthCalander_Leave(object sender, EventArgs e)
        {
            this.ToDateMonthCalander.Visible = false;
        }

        /// <summary>
        /// Handles the Click event of the ToDateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ToDateButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Calls the method to show the calender control.
                this.ShowToDateCalender();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ApplyButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ApplyButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int applyRollYear = 0;
                               
                if (this.StatusFilterComboBox.SelectedIndex == 9)
                {
                    this.managementWorkQueueDataSet = this.form1109Control.WorkItem.ManagementQueueFilterResult(this.StatusFilterComboBox.SelectedIndex + 1, 0, this.FromDateTextBox.Text.Trim(), this.ToDateTextBox.Text.Trim());
                    this.rowCount = this.managementWorkQueueDataSet.ListManagementQueue.Rows.Count;
                    this.appliedDataSet = this.managementWorkQueueDataSet;
                    this.filteredRowCount = this.managementWorkQueueDataSet.ListManagementQueue.Rows.Count;
                    this.statusFilterValue = Convert.ToString(this.StatusFilterComboBox.SelectedIndex + 1);
                    this.rollYearFilterValue = null;
                    this.fromDateValue = this.FromDateTextBox.Text.Trim();
                    this.dateValueTo = this.ToDateTextBox.Text.Trim();
                }
                else
                {
                    if (this.RollYearFilterComboBox.SelectedIndex == 0)
                    {
                        // This line is inserted because "All" text in combo box cannot be converted to integer.                        
                        this.managementWorkQueueDataSet = this.form1109Control.WorkItem.ManagementQueueFilterResult(this.StatusFilterComboBox.SelectedIndex + 1, 0, null, null);
                        this.rowCount = this.managementWorkQueueDataSet.ListManagementQueue.Rows.Count;
                        this.appliedDataSet = this.managementWorkQueueDataSet;
                        this.filteredRowCount = this.managementWorkQueueDataSet.ListManagementQueue.Rows.Count;
                        this.statusFilterValue = Convert.ToString(this.StatusFilterComboBox.SelectedIndex + 1); 
                        this.rollYearFilterValue = "0";
                        this.fromDateValue = null;
                        this.dateValueTo = null;
                    }
                    else
                    {
                        int.TryParse(this.RollYearFilterComboBox.Text.Trim(), out applyRollYear);
                        this.managementWorkQueueDataSet = this.form1109Control.WorkItem.ManagementQueueFilterResult(this.StatusFilterComboBox.SelectedIndex + 1, applyRollYear, null, null);
                        this.rowCount = this.managementWorkQueueDataSet.ListManagementQueue.Rows.Count;
                        this.appliedDataSet = this.managementWorkQueueDataSet;
                        this.filteredRowCount = this.managementWorkQueueDataSet.ListManagementQueue.Rows.Count;
                        this.statusFilterValue = Convert.ToString(this.StatusFilterComboBox.SelectedIndex + 1); 
                        this.rollYearFilterValue = this.RollYearFilterComboBox.Text;
                        this.fromDateValue = null;
                        this.dateValueTo = null;
                    }
                }

                this.LoadFilterGrid();
                this.RemoveButton.Enabled = true;
                this.filterApplied = true;
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
        /// Handles the Click event of the RemoveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int rollYearRemove = 0;

                if (this.RollYearFilterComboBox.SelectedIndex == 0)
                {
                    this.managementWorkQueueDataSet = this.form1109Control.WorkItem.ManagementQueueFilterResult(0, 0, null, null);
                }
                else
                {
                    int.TryParse(this.RollYearFilterComboBox.Text, out rollYearRemove);
                    this.managementWorkQueueDataSet = this.form1109Control.WorkItem.ManagementQueueFilterResult(0, rollYearRemove, null, null);
                }

                this.LoadFilterGrid();
                this.ClearFilter();
                this.RemoveButton.Enabled = false;
                this.filterApplied = false;
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
        /// Handles the SelectionChangeCommitted event of the StatusFilterComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StatusFilterComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (this.StatusFilterComboBox.SelectedIndex == 9)
                {
                    this.FromDatepanel.Enabled = true;
                    this.ToDatePanel.Enabled = true;
                    this.RollyearFilterPanel.Enabled = false;
                }
                else
                {
                    this.FromDatepanel.Enabled = false;
                    this.ToDatePanel.Enabled = false;
                    this.RollyearFilterPanel.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_TextChanged(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(this.SearchStatementTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SearchParcelTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SearchNameTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SearchSaleDateTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SearchAddressTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SearchTaxCodeTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SearchTreasurerTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SearchAssessorTextBox.Text.Trim())))
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
            if (this.AffidavitListDataGrid.Height > 300)
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

        /// <summary>
        /// Handles the Scroll event of the AffidavitListDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ScrollEventArgs"/> instance containing the event data.</param>
        private void AffidavitListDataGrid_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation.ToString() != "VerticalScroll")
            {
                this.MainPanel.AutoScrollPosition = new Point(e.NewValue, 0);
            }
        }
        
    #endregion

        /// <summary>
        /// Handles the Scroll event of the MainPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ScrollEventArgs"/> instance containing the event data.</param>
        private void MainPanel_Scroll(object sender, ScrollEventArgs e)
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
