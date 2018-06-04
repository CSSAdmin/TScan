//--------------------------------------------------------------------------------------------
// <copyright file="f1107.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 25 July 06		KARTHIKEYAN V	    Created
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
    /// f1107 class
    /// </summary>
    [SmartPart]
    public partial class F1107 : BaseSmartPart
    {
        #region Variable

        /// <summary>
        /// Set Date Format
        /// </summary>
        private readonly string dateFormat = "M/d/yyyy";

        /// <summary>
        /// Created Instance for f1105Controller
        /// </summary>
        private F1107Controller form1107Control;

        /// <summary>
        /// formLabelInfo string array
        /// </summary>
        private string[] formLabelInfo = new string[2];

        /// <summary>
        /// Created instance for Typed dataset AffidavitWorkQueue
        /// </summary>
        private AffidavitWorkQueueData affidavitWorkQueueDataSet = new AffidavitWorkQueueData();

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
        private AffidavitFormSmartPart affidavitSmartpart;

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
        /// Created Integer for SavedSuccessfully.
        /// </summary>
        private int selected;

        /// <summary>
        /// DatePicker object
        /// </summary>
        private System.Windows.Forms.DateTimePicker validDate = new System.Windows.Forms.DateTimePicker();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:f1107"/> class.
        /// </summary>
        public F1107()
        {
            this.InitializeComponent();
            this.DetailsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DetailsPictureBox.Height, this.DetailsPictureBox.Width, "Details", 174, 150, 94);
            this.pictureBox1.Image = ExtendedGraphics.GenerateVerticalImage(this.pictureBox1.Height, this.pictureBox1.Width, "Affidavit List", 28, 81, 128);
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
        public F1107Controller Form1107Controll
        {
            get { return this.form1107Control as F1107Controller; }
            set { this.form1107Control = value; }
        }

        #endregion       

        #region Event Scbscription

        #region HelpEngine

        /// <summary>
        /// Autoes the print on button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_StatusBarSmartPart_HelpRealEstateButtonClick, Thread = ThreadOption.UserInterface)]
        public void HelpRealEstateButtonClick(object sender, DataEventArgs<int> e)
        {
            TerraScan.Common.HelpEngine.Show(ParentForm.Text, "1107");
        }

        #endregion HelpEngine

        /// <summary>
        /// Affidavits the form button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D1100_F1107_AffidavitFormSmartPart_AffidavitFormButtonClick, Thread = ThreadOption.UserInterface)]
        public void AffidavitFormButtonClick(object sender, DataEventArgs<string> e)
        {
            int currentRowId = this.GetRowIndex();
            FormInfo formInfo = TerraScanCommon.GetFormInfo(11010);
            if (!string.IsNullOrEmpty(this.AffidavitListDataGrid.Rows[currentRowId].Cells[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.StatementIDColumn.ColumnName].Value.ToString()))
            {
                formInfo.optionalParameters = new object[] { this.AffidavitListDataGrid.Rows[currentRowId].Cells[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.StatementIDColumn.ColumnName].Value };
            }

            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo)); 
        }

        /// <summary>
        /// Receipts the form button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D1100_F1107_AffidavitFormSmartPart_ReceiptFormButtonClick, Thread = ThreadOption.UserInterface)]
        public void ReceiptFormButtonClick(object sender, DataEventArgs<string> e)
        {
            int currentRowId = this.GetRowIndex();
            FormInfo formInfo = TerraScanCommon.GetFormInfo(11011);
            if (!string.IsNullOrEmpty(this.AffidavitListDataGrid.Rows[currentRowId].Cells[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.StatementIDColumn.ColumnName].Value.ToString()))
            {
                formInfo.optionalParameters = new object[] { this.AffidavitListDataGrid.Rows[currentRowId].Cells[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.StatementIDColumn.ColumnName].Value };
            }

            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));  
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
                int currentRowId = this.GetRowIndex();
                if (!string.IsNullOrEmpty(this.AffidavitListDataGrid.Rows[currentRowId].Cells[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.StatementIDColumn.ColumnName].Value.ToString()))
                {
                    reportAuditId = this.AffidavitListDataGrid.Rows[currentRowId].Cells[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.StatementIDColumn.ColumnName].Value.ToString();
                }
                
                this.reportFileIdHashTable.Clear();
                ////this.reportFileIdHashTable.Add("KeyName", "ReportFileID");
                ////this.reportFileIdHashTable.Add("StatementID", reportAuditId);
                this.reportFileIdHashTable.Add("KeyID", reportAuditId);

                // Shows the report form.
                // changed the parameter type from string to int
                TerraScanCommon.ShowReport(110101, TerraScan.Common.Reports.Report.ReportType.Preview, this.reportFileIdHashTable);
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
                source.DataSource = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue;

                if (optionalParams[0] != null)
                {
                    foundIndex = source.Find("StatementID", optionalParams[0].ToString());
                    if (foundIndex >= 0)
                    {
                        this.SetDataGridViewPosition(foundIndex);
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
            if (this.form1107Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.FormHeaderWorkSpace.Show(this.form1107Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.FormHeaderWorkSpace.Show(this.form1107Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            if (this.form1107Control.WorkItem.SmartParts.Contains(D1100SmartPartNames.AffidavitFormSmartPart))
            {
                this.AfdvtButtonWorkSpace.Show(this.affidavitSmartpart);
            }
            else
            {
                this.affidavitSmartpart = this.form1107Control.WorkItem.SmartParts.AddNew<AffidavitFormSmartPart>(D1100SmartPartNames.AffidavitFormSmartPart);
                this.AfdvtButtonWorkSpace.Show(this.affidavitSmartpart);
            }

            this.affidavitSmartpart.ReportButtonVisible = false;
            this.affidavitSmartpart.SubmitQueueButtonVisible = false;
            this.formLabelInfo[0] = SharedFunctions.GetResourceString("1107WorkQueueFormName");
            this.formLabelInfo[1] = string.Empty;

            this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));
        }

        /// <summary>
        /// Sets the data grid view position to firstrow.
        /// </summary>
        /// <param name="firstRow">The first row.</param>
        private void SetDataGridViewPosition(int firstRow)
        {
            if (this.rowCount > 0)
            { 
                this.AffidavitListDataGrid.Rows[Convert.ToInt32(firstRow)].Selected = true;
                this.AffidavitListDataGrid.CurrentCell = this.AffidavitListDataGrid[0, Convert.ToInt32(firstRow)];
            }
        }

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        /// <returns>Return RowIndex</returns>
        private int GetRowIndex()
        {
            if (this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.Rows.Count > 0)
            {
                if (this.AffidavitListDataGrid.SelectedRows.Count > 0)
                {
                    this.selected = this.AffidavitListDataGrid.SelectedRows[0].Index;
                }
                else if (this.AffidavitListDataGrid.SelectedCells.Count > 0)
                {
                    this.selected = this.AffidavitListDataGrid.CurrentCell.RowIndex;
                }
            }

            return this.selected;
        }

        /// <summary>
        /// Loads the afiidavit list grid.
        /// </summary>
        private void LoadAfiidavitListGrid()
        {
            this.CustomizeDataGrid();
            this.affidavitWorkQueueDataSet = this.form1107Control.WorkItem.GetWorkQueueSearchResult(null, null, null, null, null, null, null, null);
            this.rowCount = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.Rows.Count;

            this.AffidavitListDataGrid.DataSource = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.DefaultView;

            this.SetRecordCount();

            if (this.rowCount == 0)
            {
                this.emptyRecord = true;
                this.DisableControls();
                this.AffidavitListDataGrid.Enabled = false;
                this.ClearNonEditableFields();
                this.WorkQueueAuditLink.Enabled = false;
                this.WorkQueueAuditLink.Text = SharedFunctions.GetResourceString("1107WorkQueueAuditLink");
                this.DisableButtons(this, new DataEventArgs<string>("0"));
            }
            else
            {
                this.emptyRecord = false;
                this.WorkQueueAuditLink.Text = SharedFunctions.GetResourceString("1107WorkQueueAuditLink") + " " + this.AffidavitListDataGrid.Rows[0].Cells[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.StatementIDColumn.ToString()].Value.ToString();
                this.SetDataGridViewPosition(0);
                this.WorkQueueAuditLink.Enabled = true;
                this.AffidavitListDataGrid.Enabled = true;
                this.AffidavitListDataGrid.Focus();
                this.EnableButtons(this, new DataEventArgs<string>("0"));
            }

            if (this.rowCount > this.AffidavitListDataGrid.NumRowsVisible)
            {
                ////this.WorkQueueVScrollBar.Enabled = true;
                this.WorkQueueVScrollBar.Visible = false;
                this.AffidavitListDataGrid.Width = 787;
            }
            else
            {
                ////this.WorkQueueVScrollBar.Enabled = false;
                this.WorkQueueVScrollBar.Visible = true;
                this.AffidavitListDataGrid.Width = 770;
            }
        }

        /// <summary>
        /// Disables the controls.
        /// </summary>
        private void DisableControls()
        {            
            this.SearchButton.Enabled = false;            
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
            this.AffidavitListDataGrid.AllowUserToResizeColumns = true;
            ////this.AffidavitListDataGrid.ScrollBars = ScrollBars.Both;
            this.AffidavitListDataGrid.AutoGenerateColumns = false;
            this.AffidavitListDataGrid.AllowUserToResizeRows = false;
            this.AffidavitListDataGrid.StandardTab = true;
            this.WorkQueuePanel.Height = 358;
            this.AffidavitListDataGrid.Height = 305;  
            this.AffidavitListGridpanel.Height = 305;   
            this.AffidavitListDataGrid.PrimaryKeyColumnName = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.StatementIDColumn.ColumnName;

            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.ParcelNumberColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.ParcelNumberColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.NameColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.NameColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.GrantorColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.GrantorColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.DocumentDateColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.DocumentDateColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.StreetAddressColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.StreetAddressColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.IsExemptColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.IsExemptColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.TreasurerColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.TreasurerColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.AssessorColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.AssessorColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.StatementIDColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.StatementIDColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.LocationColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.LocationColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.TaxableSalePriceColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.TaxableSalePriceColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.TotalDueColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.TotalDueColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.ReceiptNumberColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.ReceiptNumberColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.UseCodeColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.UseCodeColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.GranteeColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.GranteeColumn.ColumnName;
            this.AffidavitListDataGrid.Columns[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.StatementNumberColumn.ColumnName].DataPropertyName = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.StatementNumberColumn.ColumnName;              
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
                    this.StatementIdTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells["StatementNumber"].Value.ToString();
                    this.StreetAddressTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells["StreetAddress"].Value.ToString();
                    this.LocationTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells["Location"].Value.ToString();
                    this.SaledateTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells["DocumentDate"].Value.ToString();
                    this.TaxCodeTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells["IsExempt"].Value.ToString();
                    decimal.TryParse(this.AffidavitListDataGrid.Rows[rowId].Cells["TaxableSalePrice"].Value.ToString(), out taxableSalePrice);
                    this.TaxableSalePriceTextBox.Text = taxableSalePrice.ToString("$ #,##0.00");
                    decimal.TryParse(this.AffidavitListDataGrid.Rows[rowId].Cells["TotalDue"].Value.ToString(), out totalDue);
                    this.TotalDueTextBox.Text = totalDue.ToString("$ #,##0.00");
                    this.GrantorTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells["Grantor"].Value.ToString();
                    this.GranteeTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells["Grantee"].Value.ToString();
                    this.ParcelNumberTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells["ParcelNumber"].Value.ToString();
                    this.TreasurerTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells["Treasurer"].Value.ToString();
                    this.AssessorTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells["Assessor"].Value.ToString();
                    this.ReciptNumberTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells["ReceiptNumber"].Value.ToString();
                    this.UseCodesTextBox.Text = this.AffidavitListDataGrid.Rows[rowId].Cells["UseCode"].Value.ToString();
                }
            }
        }

        /// <summary>
        /// Gets the where clause.
        /// </summary>
        private void GetWhereClause()
        {
            string returnValue = String.Empty;
            StringBuilder whereClause = new StringBuilder(String.Empty);
            bool previousValueExists = false;
            
            Control[] controlArray = new Control[] { this.SearchParcelTextBox, this.SearchNameTextBox, this.SearchSaleDateTextBox, this.SearchAddressTextBox, this.SearchTaxCodeTextBox, this.SearchTreasurerTextBox, this.SearchAssessorTextBox };

            AffidavitWorkQueueData affidaviteWorkQueueFields = new AffidavitWorkQueueData();

            for (int i = 0; i < controlArray.Length; i++)
            {
                Control queryControl = controlArray.GetValue(i) as Control;

                if (!String.IsNullOrEmpty(queryControl.Text.Trim()))
                {
                    //// ParseSqlWhereCondition returns string containing parsed query value 
                    returnValue = SharedFunctions.FormatSqlWhereCondition(queryControl.Tag.ToString(), queryControl.Text.Trim().ToUpper(), affidaviteWorkQueueFields.ListExciseTaxAffidavitWorkQueue.Columns[queryControl.Tag.ToString()].DataType);

                    if (!String.IsNullOrEmpty(returnValue))
                    {
                        if (previousValueExists)
                        {
                            whereClause.Append(" AND ");
                        }

                        previousValueExists = true;
                        whereClause.Append("(");
                        whereClause.Append(returnValue);
                        whereClause.Append(")");
                    }

                    // else
                    // {
                    //    invalidQuery = true;
                    //    break;
                    // }
                }
            }

            try
            {
                int recordsCount = 0;
                //// affidaviteWorkQueueFields = this.form1107Control.WorkItem.GetWorkQueueSearchResult(1107, whereClause.ToString(), null);
                recordsCount = affidaviteWorkQueueFields.ListExciseTaxAffidavitWorkQueue.Rows.Count;
                this.AffidavitListDataGrid.DataSource = affidaviteWorkQueueFields.ListExciseTaxAffidavitWorkQueue.DefaultView;

                this.SetRecordCount();

                if (recordsCount > 0)
                {                    
                    this.emptyRecord = false;
                    this.WorkQueueAuditLink.Text = SharedFunctions.GetResourceString("1107WorkQueueAuditLink") + " " + this.AffidavitListDataGrid.Rows[0].Cells[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.StatementIDColumn.ToString()].Value.ToString();
                    this.EnableButtons(this, new DataEventArgs<string>("0"));
                    this.AffidavitListDataGrid.Enabled = true;
                    this.AffidavitListDataGrid.Rows[0].Selected = true;
                    this.AffidavitListDataGrid.Focus();
                }
                else
                {
                    this.ClearNonEditableFields();
                    this.emptyRecord = true;
                    this.DisableButtons(this, new DataEventArgs<string>("0"));
                    this.AffidavitListDataGrid.Rows[0].Selected = false;
                    this.AffidavitListDataGrid.Enabled = false;
                }

                if (affidaviteWorkQueueFields.ListExciseTaxAffidavitWorkQueue.Rows.Count > this.AffidavitListDataGrid.NumRowsVisible)
                {
                    this.WorkQueueVScrollBar.Visible = false;
                }
                else
                {
                    this.WorkQueueVScrollBar.Visible = true;
                }
            }
            catch (SoapException ex)
            {
                ////TODO : Need to find specific exception and handle it.
                // ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);

                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                // ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Enables the search.
        /// </summary>
        private void EnableSearch()
        {
            // if (this.SearchParcelTextBox.Text.Length > 0 || this.SearchNameTextBox.Text.Length > 0 || this.SearchSaleDateTextBox.Text.Length > 0 || this.SearchAddressTextBox.Text.Length > 0 || this.SearchTaxCodeTextBox.Text.Length > 0 || this.SearchTreasurerTextBox.Text.Length > 0 || this.SearchAssessorTextBox.Text.Length > 0)
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
        /// Grids the click.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void GridClick(int rowIndex)
        {
            if (this.rowCount > 0)
            {
                if (rowIndex >= 0)
                {
                    ////this.selectedRow = rowIndex;
                    this.SetDataBindingValue(rowIndex);
                    this.WorkQueueAuditLink.Text = SharedFunctions.GetResourceString("1107WorkQueueAuditLink") + " " + this.AffidavitListDataGrid.Rows[rowIndex].Cells[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.StatementIDColumn.ToString()].Value.ToString();

                    // this.SetDataGridViewPosition(e.RowIndex);
                    // this.AffidavitListDataGrid.CurrentCell.Selected = true;
                }
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
        /// Searches the record.
        /// </summary>
        private void SearchRecord()
        {
            // this.GetWhereClause();

            this.Cursor = Cursors.WaitCursor;
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

                this.affidavitWorkQueueDataSet = this.form1107Control.WorkItem.GetWorkQueueSearchResult(this.SearchParcelTextBox.Text.Trim(), this.SearchNameTextBox.Text.Trim(), this.SearchSaleDateTextBox.Text.Trim(), this.SearchAddressTextBox.Text.Trim(), this.SearchTaxCodeTextBox.Text.Trim(), this.SearchTreasurerTextBox.Text.Trim(), this.SearchAssessorTextBox.Text.Trim(), this.SearchStatementTextBox.Text.Trim());

                if (this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.Rows.Count > 0)
                {
                    this.AffidavitListDataGrid.Enabled = true;
                    this.AffidavitListDataGrid.Rows[0].Selected = true;
                    this.AffidavitListDataGrid.DataSource = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.DefaultView;
                    this.SetRecordCount();
                    this.AffidavitListDataGrid.Focus();
                    this.WorkQueueAuditLink.Enabled = true;
                    this.EnableButtons(this, new DataEventArgs<string>("0"));
                }
                else
                {
                    this.AffidavitListDataGrid.DataSource = this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.DefaultView;
                    this.SetRecordCount();
                    this.AffidavitListDataGrid.Rows[0].Selected = false;
                    this.AffidavitListDataGrid.Enabled = false;
                    this.WorkQueueAuditLink.Enabled = false;
                    this.WorkQueueAuditLink.Text = SharedFunctions.GetResourceString("1107WorkQueueAuditLink");
                    this.ClearNonEditableFields();
                    this.DisableButtons(this, new DataEventArgs<string>("0"));
                }

                if (this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.Rows.Count > this.AffidavitListDataGrid.NumRowsVisible)
                {
                    this.WorkQueueVScrollBar.Visible = false;
                    this.AffidavitListDataGrid.Width = 787;
                }
                else
                {
                    this.WorkQueueVScrollBar.Visible = true;
                    this.AffidavitListDataGrid.Width = 770;
                }
            }

            this.validDateFormat = false;
            this.Cursor = Cursors.Default;
        }

        #endregion         

        #region Events

        /// <summary>
        /// Handles the Click event of the MasterNameButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MasterNameButton_Click(object sender, EventArgs e)
        {
            try
            {
                // To be removed
                string ownerName = string.Empty;

                Form form9101 = new Form();
                form9101 = this.form1107Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9101, null, this.form1107Control.WorkItem);

                if (form9101 != null)
                {
                    if (form9101.ShowDialog() == DialogResult.Yes)
                    {
                        ownerName = TerraScanCommon.GetValue(form9101, "MasterNameOwnerId");
                        this.SearchNameTextBox.Text = ownerName;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Load event of the f1107 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1107_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkSpace();

                this.LoadAfiidavitListGrid();
                this.InitializeOperator();
                this.SearchButton.Enabled = false;
                this.WorkQueueVScrollBar.BringToFront();
                if (!this.emptyRecord)
                {
                    this.SetDataBindingValue(0);
                    this.AffidavitListDataGrid.Rows[0].Selected = true;
                    this.AffidavitListDataGrid.Focus();
                }
                else
                {
                    this.ActiveControl = this.SearchStatementTextBox;
                    this.ActiveControl.Focus();
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
            finally
            {
                this.Cursor = Cursors.Default;
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
        /// Handles the LinkClicked event of the WorkQueueAuditLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void WorkQueueAuditLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                int auditLinkStatementID = 0;
                int.TryParse(this.AffidavitListDataGrid.Rows[0].Cells[this.affidavitWorkQueueDataSet.ListExciseTaxAffidavitWorkQueue.StatementIDColumn.ToString()].Value.ToString(), out auditLinkStatementID);
                
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
                ////TerraScanCommon.ShowReport(110790, TerraScan.Common.Reports.Report.ReportType.Preview);
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
            if (this.AffidavitListDataGrid.Width > 322)
            {
                this.WorkQueueVScrollBar.Visible = true;
            }
            else
            {
                this.WorkQueueVScrollBar.Visible = true;
            }
        }

        /// <summary>
        /// Handles the Scroll event of the WorkQueuePanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ScrollEventArgs"/> instance containing the event data.</param>
        private void WorkQueuePanel_Scroll(object sender, ScrollEventArgs e)
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
        /// AffidavitListDataGrid_Scroll
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AffidavitListDataGrid_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation.ToString() != "VerticalScroll")
            {
                this.WorkQueuePanel.AutoScrollPosition = new Point(e.NewValue, 0);
            }
        }

        #endregion ChangeOrder

        #region RecordCount
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
        #endregion RecordCount

        #endregion

       
       
    }
}
