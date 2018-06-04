//--------------------------------------------------------------------------------------------
// <copyright file="F1224.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F1224 Check Print Queue Functionality
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 19-10-2006       Krishna Abburi       Created
//*********************************************************************************/

namespace D1210
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.Utilities;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using TerraScan.BusinessEntities;
    using System.Web.Services.Protocols;
    using TerraScan.Common.Reports;
    using System.Collections;
    using System.Configuration;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// F1224 Check print Queue User InterFace
    /// </summary>
    [SmartPart]
    public partial class F1224 : BaseSmartPart
    {
        #region Private Variables

        /// <summary>
        /// Variable Instance for F1224Controller
        /// </summary>
        private F1224Controller form1224Controll;

        /// <summary>
        /// formLabelInfo string array
        /// </summary>
        private string[] formLabelInfo = new string[2];

        /// <summary>
        /// Variable Holds the disbursementDataset
        /// </summary>
        private F1224CheckPrintQueueData checkPrintQueueDataset = new F1224CheckPrintQueueData();

        /// <summary>
        /// Binding Source
        /// </summary>
        private BindingSource bindingSource;

        /// <summary>
        /// Register Id
        /// </summary>
        private int registerId = -1;

        /// <summary>
        /// Total Record Count
        /// </summary>
        private int numInGrid;

        /// <summary>
        /// Selected Record Count
        /// </summary>
        private int numInSelected;

        /// <summary>
        /// Total Amount
        /// </summary>
        private decimal amountOfGrid;

        /// <summary>
        /// Selected Amount
        /// </summary>
        private decimal amountOfSelected;

        /// <summary>
        /// Selected Amount
        /// </summary>
        private Int64 startingCheckNumber;

        /// <summary>
        /// tempDataTable for Get AgencyIds
        /// </summary>
        private DataTable tempPaymentItemsDataTable;

        /// <summary>
        /// tempDataRow for AgencyId
        /// </summary>
        private DataRow tempPaymentItemsDataRow;

        /// <summary>
        /// minCheckNumError
        /// </summary>
        private Int64 minCheckNumError;

        /// <summary>
        ///  minCLIDErro
        /// </summary>
        private int minCLIDError;

        /// <summary>
        /// reportOptionalParameter
        /// </summary>
        private Hashtable reportFileIdHashTable = new Hashtable();

        /// <summary>
        /// currentCheckCLSId
        /// </summary>
        private int currentCheckCLSId;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1224"/> class.
        /// </summary>
        public F1224()
        {
            InitializeComponent();
            this.AgencyHeaderPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AgencyHeaderPictureBox.Height, this.AgencyHeaderPictureBox.Width, "", 0, 51, 0);
            this.DepositHistoryPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DepositHistoryPictureBox.Height, this.DepositHistoryPictureBox.Width, "UnPrinted Checks", 28, 81, 128);
        }

        #endregion

        #region Published Events

        /// <summary>
        /// EventPublication for FormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// event publication for Show the child form 
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form1224 controll.
        /// </summary>
        /// <value>The form1224 controll.</value>
        [CreateNew]
        public F1224Controller Form1224Controll
        {
            get { return this.form1224Controll as F1224Controller; }
            set { this.form1224Controll = value; }
        }

        /// <summary>
        /// Gets or sets the selected rec count.
        /// </summary>
        /// <value>The selected rec count.</value>
        public int NumInSelected
        {
            get { return this.numInSelected; }
            set { this.numInSelected = value; }
        }

        /// <summary>
        /// Gets or sets the total rec count.
        /// </summary>
        /// <value>The total rec count.</value>
        public int NumInGrid
        {
            get { return this.numInGrid; }
            set { this.numInGrid = value; }
        }

        /// <summary>
        /// Gets or sets the total amount.
        /// </summary>
        /// <value>The total amount.</value>
        public decimal AmountOfGrid
        {
            get { return this.amountOfGrid; }
            set { this.amountOfGrid = value; }
        }

        /// <summary>
        /// Gets or sets the selected amount.
        /// </summary>
        /// <value>The selected amount.</value>
        public decimal AmountOfSelected
        {
            get { return this.amountOfSelected; }
            set { this.amountOfSelected = value; }
        }

        /// <summary>
        /// Gets or sets the register id.
        /// </summary>
        /// <value>The register id.</value>
        public int RegisterId
        {
            get { return this.registerId; }
            set { this.registerId = value; }
        }

        /// <summary>
        /// Gets or sets the starting check number.
        /// </summary>
        /// <value>The starting check number.</value>
        private Int64 StartingCheckNumber
        {
            get { return this.startingCheckNumber; }
            set { this.startingCheckNumber = value; }
        }

        /// <summary>
        /// Gets or sets the min check num error.
        /// </summary>
        /// <value>The min check num error.</value>
        private Int64 MinCheckNumError 
        {
            get { return this.minCheckNumError; }
            set { this.minCheckNumError = value; }
        }

        /// <summary>
        /// Gets or sets the min CLID error.
        /// </summary>
        /// <value>The min CLID error.</value>
        private int MinCLIDError 
        {
            get { return this.minCLIDError; }
            set { this.minCLIDError = value; }
        }

        /// <summary>
        /// Gets or sets the current check CLS id.
        /// </summary>
        /// <value>The current check CLS id.</value>
        private int CurrentCheckCLSId 
        {
            get { return this.currentCheckCLSId; }
            set { this.currentCheckCLSId = value; }
        }

        #endregion

        #region Form Events

        /// <summary>
        /// Handles the Load event of the F1224 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1224_Load(object sender, EventArgs e)
        {
            this.CustomizeDataGrid();
            this.LoadWorkSpaces();
            this.InitAccountNameCombo();
            this.PopulateUnPrintedChecksGrid(this.RegisterId);
            this.AccountNameComboBox.Select();
        }

       

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the AccountNameComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AccountNameComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ////try
            ////{
            ////    if (Convert.ToInt32(this.AccountNameComboBox.SelectedValue) != 0)
            ////    {
            ////        this.RegisterId = Convert.ToInt32(this.AccountNameComboBox.SelectedValue);
            ////        this.PopulateUnPrintedChecksGrid(this.RegisterId);
            ////        this.ComboLinkLabel.Visible = true;
            ////        this.ComboLinkLabel.Text = this.AccountNameComboBox.Text; 
            ////    }
            ////    else
            ////    {
            ////        this.RegisterId = -1;
            ////    }
            ////}
            ////catch (Exception ex)
            ////{
            ////    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            ////}
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
                if (this.NumInGrid > 0)
                {
                    for (int i = 0; i < this.UnPrintedChecksDataGridView.OriginalRowCount; i++)
                    {
                        this.UnPrintedChecksDataGridView.Rows[i].Cells["CheckedItem"].Value = true;
                    }

                    this.UnPrintedChecksDataGridView.RefreshEdit();
                    this.SetGridSummeries();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the UnSelectAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UnSelectAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.NumInGrid > 0)
                {
                    for (int i = 0; i < this.UnPrintedChecksDataGridView.OriginalRowCount; i++)
                    {
                        this.UnPrintedChecksDataGridView.Rows[i].Cells["CheckedItem"].Value = "False";
                    }

                    this.UnPrintedChecksDataGridView.RefreshEdit();
                    this.SetGridSummeries();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the UnPrintedChecksDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void UnPrintedChecksDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.NumInGrid > 0)
                {
                    if (e.ColumnIndex == this.UnPrintedChecksDataGridView.Columns[this.checkPrintQueueDataset.ListUnPrintedChecksTable.CheckedItemColumn.ColumnName].Index)
                    {
                        this.UnPrintedChecksDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        this.checkPrintQueueDataset.ListUnPrintedChecksTable.AcceptChanges();
                        this.CurrentCheckCLSId = Convert.ToInt32(this.UnPrintedChecksDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                        this.SetGridSummeries();
                    }
                }
            }
            catch (DataException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the UnPrintedChecksDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void UnPrintedChecksDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.NumInGrid > 0)
            {
                this.CurrentCheckCLSId = Convert.ToInt32(this.UnPrintedChecksDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                this.CheckPrintQueueAuditLink.Text = SharedFunctions.GetResourceString("CheckPrintQueueIDLink") + " " + this.UnPrintedChecksDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        /// <summary>
        /// Handles the Click event of the PrintButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PrintButton_Click(object sender, EventArgs e)
        {
            this.checkPrintQueueDataset.ListCreateChecksTable.Clear();
            string checkItemsXML = string.Empty;
            checkItemsXML = this.GetCheckedUnprintedItems();

            if (!string.IsNullOrEmpty(checkItemsXML.Trim()) && this.registerId != -1)
            {
                try
                {
                    this.startingCheckNumber = Convert.ToInt64(this.StartingCheckNoTextBox.Text);
                    this.checkPrintQueueDataset.ListCreateChecksTable.Clear();
                    string reportAuditIds = string.Empty;
                    reportAuditIds = checkItemsXML;
                    this.checkPrintQueueDataset.ListCreateChecksTable.Merge(this.Form1224Controll.WorkItem.F1224_CreateChecks(this.RegisterId, TerraScanCommon.UserId, this.StartingCheckNumber, checkItemsXML));
                    if (this.checkPrintQueueDataset.ListCreateChecksTable.Rows.Count > 0)
                    {
                        this.minCLIDError = Convert.ToInt32(this.checkPrintQueueDataset.ListCreateChecksTable[0][0].ToString());

                        if (this.minCLIDError != 0)
                        {
                            this.MinCheckNumError = Convert.ToInt64(this.checkPrintQueueDataset.ListCreateChecksTable[0][3].ToString());
                            if (this.startingCheckNumber != this.minCheckNumError)
                            {
                                this.CallToReportProcess(reportAuditIds);
                                this.CallToErrorDialogBox();
                            }
                            else
                            {
                                string @msgtxt = "Check Numbers are already exist with " + this.startingCheckNumber + " Check Number. Would you like to open the Check Detail form to the record of Check Number " + this.startingCheckNumber + "?";
                                DialogResult dialogResult = MessageBox.Show(@msgtxt, "TerraScan T2 - Duplicate Check Number", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (dialogResult.Equals(DialogResult.Yes))
                                {
                                    this.CallToCheckDetail();
                                }
                            }
                        }
                        else
                        {
                            this.CallToReportProcess(reportAuditIds);
                            ////this.CallToCheckDetail();
                        }
                    }
                }
                catch (SoapException ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
                catch (Exception ex1)
                {
                    ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
                finally 
                {
                    this.PopulateUnPrintedChecksGrid(this.RegisterId);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PreviewButton_Click(object sender, EventArgs e)
        {
            this.checkPrintQueueDataset.ListCreateChecksTable.Clear();
            string checkItemsXML = string.Empty;
            checkItemsXML = this.GetCheckedUnprintedItems();

            if (!string.IsNullOrEmpty(checkItemsXML.Trim()) && this.registerId != -1)
            {
                try
                {
                    this.startingCheckNumber = Convert.ToInt64(this.StartingCheckNoTextBox.Text);
                    this.checkPrintQueueDataset.ListCreateChecksTable.Clear();
                    string reportAuditIds = string.Empty;
                    reportAuditIds = checkItemsXML;
                    this.checkPrintQueueDataset.ListCreateChecksTable.Merge(this.Form1224Controll.WorkItem.F1224_CreateChecks(this.RegisterId, TerraScanCommon.UserId, this.StartingCheckNumber, checkItemsXML));
                    if (this.checkPrintQueueDataset.ListCreateChecksTable.Rows.Count > 0)
                    {
                        this.minCLIDError = Convert.ToInt32(this.checkPrintQueueDataset.ListCreateChecksTable[0][0].ToString());

                        if (this.minCLIDError != 0)
                        {
                            this.MinCheckNumError = Convert.ToInt64(this.checkPrintQueueDataset.ListCreateChecksTable[0][3].ToString());
                            if (this.startingCheckNumber != this.minCheckNumError)
                            {
                                this.CallToReportProcessForPrivewButton(reportAuditIds);
                                this.CallToErrorDialogBox();
                            }
                            else
                            {
                                string @msgtxt = "Check Numbers are already exist with " + this.startingCheckNumber + " Check Number. Do you want show the existing Record?";
                                DialogResult dialogResult = MessageBox.Show(@msgtxt, "TerraScan T2 - Duplicate Check Number", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (dialogResult.Equals(DialogResult.Yes))
                                {
                                    this.CallToCheckDetail();
                                }
                            }
                        }
                        else
                        {
                            this.CallToReportProcessForPrivewButton(reportAuditIds);
                            ////this.CallToCheckDetail();
                        }
                    }
                }
                catch (SoapException ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
                catch (Exception ex1)
                {
                    ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
                finally
                {
                    this.PopulateUnPrintedChecksGrid(this.RegisterId);
                }
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the UnPrintedChecksDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void UnPrintedChecksDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == this.UnPrintedChecksDataGridView.Columns[this.checkPrintQueueDataset.ListUnPrintedChecksTable.EntryDateColumn.ColumnName].Index)
            {
                if (this.UnPrintedChecksDataGridView.Rows[e.RowIndex].Selected || this.UnPrintedChecksDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                {
                    (UnPrintedChecksDataGridView.Rows[e.RowIndex].Cells[this.checkPrintQueueDataset.ListUnPrintedChecksTable.EntryDateColumn.ColumnName.ToString()] as DataGridViewLinkCell).LinkColor = Color.White;
                    (UnPrintedChecksDataGridView.Rows[e.RowIndex].Cells[this.checkPrintQueueDataset.ListUnPrintedChecksTable.EntryDateColumn.ColumnName.ToString()] as DataGridViewLinkCell).ActiveLinkColor = Color.White;
                    (UnPrintedChecksDataGridView.Rows[e.RowIndex].Cells[this.checkPrintQueueDataset.ListUnPrintedChecksTable.EntryDateColumn.ColumnName.ToString()] as DataGridViewLinkCell).VisitedLinkColor = Color.White;
                }
                else
                {
                    (UnPrintedChecksDataGridView.Rows[e.RowIndex].Cells[this.checkPrintQueueDataset.ListUnPrintedChecksTable.EntryDateColumn.ColumnName.ToString()] as DataGridViewLinkCell).LinkColor = Color.Blue;
                    (UnPrintedChecksDataGridView.Rows[e.RowIndex].Cells[this.checkPrintQueueDataset.ListUnPrintedChecksTable.EntryDateColumn.ColumnName.ToString()] as DataGridViewLinkCell).ActiveLinkColor = Color.Red;
                    (UnPrintedChecksDataGridView.Rows[e.RowIndex].Cells[this.checkPrintQueueDataset.ListUnPrintedChecksTable.EntryDateColumn.ColumnName.ToString()] as DataGridViewLinkCell).VisitedLinkColor = Color.Blue;
                }
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the CheckPrintQueueAuditLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void CheckPrintQueueAuditLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (this.CurrentCheckCLSId > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(90101);
                    formInfo.optionalParameters = new object[2];
                    formInfo.optionalParameters[0] = this.Tag;
                    formInfo.optionalParameters[1] = this.CurrentCheckCLSId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }

                ////int reportAuditId = 0;
                ////this.Cursor = Cursors.WaitCursor;

                ////reportAuditId = this.CurrentCheckCLSId;
                ////this.reportFileIdHashTable.Clear();
                ////this.reportFileIdHashTable.Add("ReportFileID", reportAuditId);
                //////// Shows the report form.
                ////////changed the parameter type from string to int
                ////TerraScanCommon.ShowReport(122090, TerraScan.Common.Reports.Report.ReportType.Preview, this.reportFileIdHashTable);
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
        /// Handles the LinkClicked event of the ComboLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ComboLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(1220);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.RegisterId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                this.PopulateUnPrintedChecksGrid(this.RegisterId);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the UnPrintedChecksDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void UnPrintedChecksDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (this.UnPrintedChecksDataGridView.OriginalRowCount == 0)
            {
                this.UnPrintedChecksDataGridView.CurrentCell = null;
            }
            else
            {
                TerraScanCommon.SetDataGridViewPosition(this.UnPrintedChecksDataGridView, 0);
            }
        }

        /// <summary>
        /// Handles the DropDownClosed event of the AccountNameComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AccountNameComboBox_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(this.AccountNameComboBox.SelectedValue) != 0)
                {
                    this.RegisterId = Convert.ToInt32(this.AccountNameComboBox.SelectedValue);
                    this.PopulateUnPrintedChecksGrid(this.RegisterId);
                    this.ComboLinkLabel.Visible = true;
                    this.ComboLinkLabel.Text = this.AccountNameComboBox.Text;
                }
                else
                {
                    this.RegisterId = -1;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            if (this.form1224Controll.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1224Controll.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1224Controll.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            this.formLabelInfo[0] = SharedFunctions.GetResourceString("1224CheckPrintQueue"); ////Properties.Resources.FormName;
            this.formLabelInfo[1] = string.Empty;
            this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));
        }

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizeDataGrid()
        {
            this.UnPrintedChecksDataGridView.AllowUserToResizeColumns = false;
            this.UnPrintedChecksDataGridView.AutoGenerateColumns = false;
            this.UnPrintedChecksDataGridView.AllowUserToResizeRows = false;
            this.UnPrintedChecksDataGridView.StandardTab = true;
            this.UnPrintedChecksDataGridView.Columns["Amount"].DefaultCellStyle.Font = new Font("Courier New", 8.25F, FontStyle.Bold);
            this.UnPrintedChecksDataGridView.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.UnPrintedChecksDataGridView.Columns[0].DataPropertyName = "CLID";
            this.UnPrintedChecksDataGridView.Columns[1].DataPropertyName = "CheckedItem";
            this.UnPrintedChecksDataGridView.Columns[2].DataPropertyName = "EntryDate";
            this.UnPrintedChecksDataGridView.Columns[3].DataPropertyName = "CLType";
            this.UnPrintedChecksDataGridView.Columns[4].DataPropertyName = "PayableTo";
            this.UnPrintedChecksDataGridView.Columns[5].DataPropertyName = "Amount";
        }

        /// <summary>
        /// Disables the V scroll bar.
        /// </summary>
        /// <param name="recordsCount">The records count.</param>
        private void DisableVScrollBar(int recordsCount)
        {
            if (recordsCount > 22)
            {
                this.VScrollBar.Enabled = true;
                this.VScrollBar.Visible = false;
            }
            else
            {
                this.VScrollBar.Enabled = false;
                this.VScrollBar.Visible = true;
            }
        }

        /// <summary>
        /// Inits the account name combo.
        /// </summary>
        private void InitAccountNameCombo()
        {
            try
            {
                F1224CheckPrintQueueData.ListAccountNamesDataTable listAccountNames = new F1224CheckPrintQueueData.ListAccountNamesDataTable();
                listAccountNames = this.Form1224Controll.WorkItem.F1224_AccountNames();

                if (listAccountNames.Rows.Count > 0)
                {
                    this.AccountNameComboBox.DataSource = listAccountNames;
                    this.AccountNameComboBox.ValueMember = listAccountNames.RegisterIDColumn.ColumnName;
                    this.AccountNameComboBox.DisplayMember = listAccountNames.AccountNameColumn.ColumnName;

                    DataRow[] dataRow;
                    bool defaultValue = true;
                    string findExp = "IsDefault =" + defaultValue.ToString();
                    dataRow = listAccountNames.Select(findExp);

                    if (dataRow.Length > 0)
                    {
                        int rowIndex = listAccountNames.Rows.IndexOf(dataRow[0]);
                        this.RegisterId = Convert.ToInt32(dataRow[0][listAccountNames.RegisterIDColumn.ColumnName.ToString()]);
                        this.AccountNameComboBox.SelectedIndex = rowIndex;
                        this.ComboLinkLabel.Visible = true;
                        this.ComboLinkLabel.Text = listAccountNames.Rows[0][this.checkPrintQueueDataset.ListAccountNames.AccountNameColumn.ColumnName].ToString();
                    }
                    else
                    {
                        this.AccountNameComboBox.SelectedIndex = -1;
                        this.ComboLinkLabel.Visible = false;
                        this.ComboLinkLabel.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Fills the check no text box.
        /// </summary>
        /// <param name="registerID">The register ID.</param>
        private void FillCheckNoTextBox(int registerID)
        {
            try
            {
                this.checkPrintQueueDataset.CheckNumberTable.Clear();
                this.checkPrintQueueDataset.Merge(this.Form1224Controll.WorkItem.F1224_GetCheckNumber(registerID));

                if (this.checkPrintQueueDataset.CheckNumberTable.Rows.Count > 0)
                {
                    this.startingCheckNumber = Convert.ToInt64(this.checkPrintQueueDataset.CheckNumberTable[0][0].ToString());
                    this.StartingCheckNoTextBox.Text = this.startingCheckNumber.ToString();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Populates the un printed checks grid.
        /// </summary>
        /// <param name="registerID">The register ID.</param>
        private void PopulateUnPrintedChecksGrid(int registerID)
        {
            try
            {
                this.numInGrid = 0;
                this.bindingSource = new BindingSource();
                this.checkPrintQueueDataset.ListUnPrintedChecksTable.Clear();
                this.checkPrintQueueDataset.Merge(this.Form1224Controll.WorkItem.F1224_ListunPrintedChecks(registerID));
                this.NumInGrid = this.checkPrintQueueDataset.ListUnPrintedChecksTable.Rows.Count;
                this.AddCheckBoxColumn();
                this.UnPrintedChecksDataGridView.DataSource = this.checkPrintQueueDataset.ListUnPrintedChecksTable;
                this.bindingSource.DataSource = this.checkPrintQueueDataset.ListUnPrintedChecksTable.Copy();

                this.SetGridSummeries();
                if (this.NumInGrid > 0)
                {
                    this.UnPrintedChecksDataGridView.Enabled = true;
                    TerraScanCommon.SetDataGridViewPosition(this.UnPrintedChecksDataGridView, 0);
                    this.CheckPrintQueueAuditLink.Enabled = true;
                    this.CheckPrintQueueAuditLink.Text = SharedFunctions.GetResourceString("CheckPrintQueueIDLink") + " " + this.UnPrintedChecksDataGridView.Rows[0].Cells[0].Value.ToString();
                    this.FillCheckNoTextBox(this.RegisterId);
                }
                else
                {
                    this.UnPrintedChecksDataGridView.Enabled = false;
                    this.UnPrintedChecksDataGridView.Rows[0].Selected = false;
                    this.StartingCheckNoTextBox.Text = "";
                    this.CheckPrintQueueAuditLink.Enabled = false;
                    this.CheckPrintQueueAuditLink.Text = SharedFunctions.GetResourceString("CheckPrintQueueIDLink") + " ";
                }

                if (this.UnPrintedChecksDataGridView.OriginalRowCount == 0)
                {
                    this.UnPrintedChecksDataGridView.CurrentCell = null;
                }

                ////this.AccountNameComboBox.Select();
                this.DisableVScrollBar(this.NumInGrid);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Adds the check box column.
        /// </summary>
        private void AddCheckBoxColumn()
        {
            try
            {
                foreach (DataRow row in this.checkPrintQueueDataset.ListUnPrintedChecksTable.Rows)
                {
                    row["CheckedItem"] = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the grid summeries.
        /// </summary>
        private void SetGridSummeries()
       {
           try
           {
               ////Sum of the Total Amount 
               this.amountOfGrid = 0;
               if (this.NumInGrid > 0)
               {
                   this.amountOfGrid = Convert.ToDecimal(this.checkPrintQueueDataset.ListUnPrintedChecksTable.Compute("SUM(" + checkPrintQueueDataset.ListUnPrintedChecksTable.AmountColumn.ColumnName + ")", "").ToString());
               }

               ////Counting sum of Checked Rows Amount
               DataSet chekedDataset = new DataSet();
               DataRow[] checkedRow;
               this.checkPrintQueueDataset.ListUnPrintedChecksTable.AcceptChanges();
               ////checkedRow = this.checkPrintQueueDataset.ListUnPrintedChecksTable.Select("CheckedItem = True");
               checkedRow = this.checkPrintQueueDataset.ListUnPrintedChecksTable.Select(this.checkPrintQueueDataset.ListUnPrintedChecksTable.CheckedItemColumn.ColumnName + "=" + true);
               chekedDataset.Clear();
               chekedDataset.Merge(checkedRow);

               if (checkedRow.Length == 0)
               {
                   this.numInSelected = 0;
                   this.amountOfSelected = 0;
                   this.PrintButton.Enabled = false;
                   this.PreviewButton.Enabled = false;
               }
               else
               {
                   this.numInSelected = chekedDataset.Tables[0].Rows.Count;
                   this.amountOfSelected = Convert.ToDecimal(chekedDataset.Tables[0].Compute("SUM(" + chekedDataset.Tables[0].Columns[4].ColumnName + ")", "").ToString());
                   this.PrintButton.Enabled = true;
                   this.PreviewButton.Enabled = true;
               }

               this.RecordCountLabel.Text = this.NumInSelected + " / " + this.NumInGrid;
               this.SelectedAmountTextBox.Text = this.AmountOfSelected.ToString();
               this.totalAmountTextBox.Text = this.AmountOfGrid.ToString();
           }
           catch (Exception ex)
           {
               ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
           }
        }

        /// <summary>
        /// Gets the checked unprinted items.
        /// </summary>
        /// <returns>UnChecked Print Checks</returns>
        private string GetCheckedUnprintedItems()
        {
            this.tempPaymentItemsDataTable = new DataTable();
            string clsIds = string.Empty;

            foreach (DataColumn column in this.checkPrintQueueDataset.ListUnPrintedChecksTable.Columns)
            {
                if (column.ColumnName == this.checkPrintQueueDataset.ListUnPrintedChecksTable.CLIDColumn.ColumnName)
                {
                    this.tempPaymentItemsDataTable.Columns.Add(new DataColumn(column.ColumnName));
                }
            }

            foreach (DataRow dr in this.checkPrintQueueDataset.ListUnPrintedChecksTable.Rows)
            {
                this.tempPaymentItemsDataRow = this.tempPaymentItemsDataTable.NewRow();

                if (dr["CheckedItem"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(dr["CheckedItem"]))
                    {
                        foreach (DataColumn column in this.checkPrintQueueDataset.ListUnPrintedChecksTable.Columns)
                        {
                            if (column.ColumnName == this.checkPrintQueueDataset.ListUnPrintedChecksTable.CLIDColumn.ColumnName)
                            {
                                this.tempPaymentItemsDataRow[column.ColumnName] = dr[column.ColumnName];
                            }
                        }

                        this.tempPaymentItemsDataTable.Rows.Add(this.tempPaymentItemsDataRow);
                    }
                }
            }

            if (this.tempPaymentItemsDataTable.Rows.Count > 0)
            {
                clsIds = TerraScanCommon.GetXmlString(this.tempPaymentItemsDataTable);
            }

            return clsIds;
        }

        /// <summary>
        /// Calls to error dialog box.
        /// </summary>
        private void CallToErrorDialogBox()
        {
            try
            {
                string @msgText = string.Empty;
                @msgText += "Not all selected checks could be printed because one or more check numbers already exist. Only check numbers ";
                @msgText += this.startingCheckNumber;
                @msgText += " through ";
                @msgText += this.MinCheckNumError - 1;
                @msgText += " Were printed. Would you like to open the Check Detail form to the record of Check Number ";
                @msgText += this.MinCheckNumError + "?";

                DialogResult dialogResult = MessageBox.Show(@msgText, "TerraScan T2 - Duplicate Check Numbers", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult.Equals(DialogResult.Yes))
                {
                    this.CallToCheckDetail();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Calls to check detail.
        /// </summary>
        private void CallToCheckDetail()
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(1226);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.MinCLIDError;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                this.PopulateUnPrintedChecksGrid(this.RegisterId);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Calls to report process.
        /// </summary>
        /// <param name="reportAuditIds">The report audit ids.</param>
        private void CallToReportProcess(string reportAuditIds)
        {
            try
            {
                this.reportFileIdHashTable.Clear();
                this.reportFileIdHashTable.Add("CLIDs", reportAuditIds);
                //// Shows the report form.
                ////changed the parameter type from string to int
                TerraScanCommon.ShowReport(122400, TerraScan.Common.Reports.Report.ReportType.PrintDefault, this.reportFileIdHashTable);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Calls to report process for privew button.
        /// </summary>
        /// <param name="reportAuditIds">The report audit ids.</param>
        private void CallToReportProcessForPrivewButton(string reportAuditIds)
        {
            try
            {
                this.reportFileIdHashTable.Clear();
                this.reportFileIdHashTable.Add("CLIDs", reportAuditIds);
                //// Shows the report form.
                ////changed the parameter type from string to int
                TerraScanCommon.ShowReport(122400, TerraScan.Common.Reports.Report.ReportType.Preview, this.reportFileIdHashTable);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion
    }
}
