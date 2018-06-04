//--------------------------------------------------------------------------------------------
// <copyright file="F1013.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for D1013 Batch Payment Mgmt Functionality
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09-05-2007       Sadha Shivudu      Created
// 16-02-2008       Kuppu              Modified for Bug #4494
// 03-04-2009       Sadha Shivudu      TSCO 5749 - Add AutoPrint to this form  
//                  Manoj Kumar        TSCO 9420 - Add ReceiptDate and Fix Sort Order.
// 22 Oct 2017      Priyadharshini     Implemented TSCO # 21947 - Add Comment All Button   
//*********************************************************************************/
namespace D1013
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
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using System.Web.Services.Protocols;
    using System.Collections;
    using TerraScan.Common.Reports;
    using TerraScan.SmartParts;

    /// <summary>
    /// F1013 Batch Payment Mgmt User Interface.
    /// </summary>
    [SmartPart]
    public partial class F1013 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// variable holds the form1013Controller.
        /// </summary>
        private F1013Controller form1013Controll;

        /// <summary>
        /// Variable holds the batchPaymes DataSet.
        /// </summary>
        private F1013BatchPaymentMgmtData batchPaymentsDataSet = new F1013BatchPaymentMgmtData();

        private F1013BatchPaymentMgmtData.ListUnpaidReceiptsDataTable  listunpaidreceipt; 
        /// <summary>
        /// variable holds the unpaidReceipts rows count.
        /// </summary>
        private int unpaidReceiptRowCount;

        /// <summary>
        /// SmartPart instance for accessing the properties
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart;


        /// <summary>
        /// Used to Hold the arguments to update Attachments and comments for 9999
        /// </summary>
        private AdditionalOperationCountEntity additionalOperationCountEnt = new AdditionalOperationCountEntity(-99999, -99999, false);

        /// <summary>
        /// Used to Hold the arguments to update Attachments and comments for 2550
        /// </summary>
        private AdditionalOperationCountEntity additionalOperationCount = new AdditionalOperationCountEntity(-99999, -99999, false);

        /// <summary>
        ///  Variable Holds the defaultAttachmentButtonBackColor
        /// </summary>
        private Color defaultCommentButtonBackColor = Color.FromArgb(174, 150, 94);
        /// <summary>
        ///  Variable Holds the defaultAttachmentButtonBackColor
        /// </summary>
        private Color highPriorityCommentColor = Color.FromArgb(255, 0, 0);

        /// <summary>
        /// variable holds the receipt form for the current row.
        /// </summary>
        private int currentRowReceiptForm;

        /// <summary>
        /// variable holds the current row reciept id.
        /// </summary>
        private int currentRowReceiptId;

        /// <summary>
        /// variable holds the selected receipts total amount.
        /// </summary>
        private decimal selectedReceiptsBalance;

        /// <summary>
        /// variable holds the selectedReceiptIds.
        /// </summary>
        private List<int> selectedReceiptIds;

         /// <summary>
        /// variable holds the selectedReceiptIds.
        /// </summary>
        private List<int> readonlyRowIndex;

        /// <summary>
        /// variable holds the selectedrReceiptsIdsIndex values.
        /// </summary>
        private List<int> selectedReceiptIdsIndex = new List<int>();

        /// <summary>
        /// variable holds the selected receipt ids xml string.
        /// </summary>
        private string selectedReceiptIdsXml = string.Empty;

        /// <summary>
        /// used to store the userId.
        /// </summary>
        private int? userId = null;

        /// <summary>
        /// used to store receiptGridBindingSource
        /// </summary>
        private BindingSource receiptGridBindingSource = new BindingSource();

        /// <summary>
        /// used to store the isapplyPrintssaved
        /// </summary>
        private bool isapplyPrintssaved;

        /// <summary>
        /// flag to identify sort direction
        /// </summary>
        private bool flagRateSortDirection;

        /// <summary>
        /// Used to store the snapShotId
        /// </summary>
        private int snapShotId;

        /// <summary>
        /// Flag for grid content click
        /// </summary>
        private bool gridContentCliked;

        private bool notload = true;

        private bool ascsort;

        /// <summary>
        ///lastgridclick
        /// </summary>
        private int lastgridclick;

        /// <summary>
        /// Used to store the allowReceiptCreatedByComboSelctionChange
        /// </summary>
        private bool allowReceiptCreatedByComboSelctionChange;

        /// <summary>
        /// Store PaymentID
        /// </summary>
        private int ppaymentId;

        /// <summary>
        /// assigining report Number - default value 0
        /// </summary>
        private int reportNumber = 0;

    
        /// <summary>
        /// Set Date Format
        /// </summary>
        private string dateFormat = "M/d/yyyy";

        /// <summary>
        /// getAutoPrintOnValue
        /// </summary>
        private CommentsData getAutoPrintOnValue = new CommentsData();

        /// <summary>
        /// autoprintonoff
        /// </summary>
        private bool autoPrintOnOff = false;

        private SortOrder sortGlypDirection;

        private string paymentValue = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1013"/> class.
        /// </summary>
        public F1013()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Published Events

        /// <summary>
        /// EventPublication for FormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form1013 controll.
        /// </summary>
        /// <value>The form1013 controll.</value>
        [CreateNew]
        public F1013Controller Form1013Controll
        {
            get { return this.form1013Controll as F1013Controller; }
            set { this.form1013Controll = value; }
        }

        #endregion

        #region Subscribed Events

        #region Reports

        /// <summary>
        /// Handles the Click event of the PrintButton control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_PrintButtonClick, Thread = ThreadOption.UserInterface)]
        public void PrintButtonClick(object sender, DataEventArgs<Button> e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Hashtable unpaidReceipts = new Hashtable();
                unpaidReceipts.Add(SharedFunctions.GetResourceString("UserID"), TerraScanCommon.UserId);
                unpaidReceipts.Add(SharedFunctions.GetResourceString("SnapshotID"), this.snapShotId);
                TerraScanCommon.ShowReport(10131, Report.ReportType.Print, unpaidReceipts);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_PreviewButtonClick, Thread = ThreadOption.UserInterface)]
        public void PreviewButtonClick(object sender, DataEventArgs<Button> e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Hashtable unpaidReceipts = new Hashtable();
                unpaidReceipts.Add(SharedFunctions.GetResourceString("UserID"), TerraScanCommon.UserId);
                unpaidReceipts.Add(SharedFunctions.GetResourceString("SnapshotID"), this.snapShotId);
                TerraScanCommon.ShowReport(10131, Report.ReportType.Preview, unpaidReceipts);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the EMailButton control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_EmailButtonClick, Thread = ThreadOption.UserInterface)]
        public void EmailButtonClick(object sender, DataEventArgs<Button> e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Hashtable unpaidReceipts = new Hashtable();
                unpaidReceipts.Add(SharedFunctions.GetResourceString("UserID"), TerraScanCommon.UserId);
                unpaidReceipts.Add(SharedFunctions.GetResourceString("SnapshotID"), this.snapShotId);
                TerraScanCommon.ShowReport(10131, Report.ReportType.Email, unpaidReceipts);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion reports

        #endregion

        #region Protected Events

        /// <summary>
        /// Processes the CMD key.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="keyData">The key data.</param>
        /// <returns>Boolean</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                const int WM_KEYDOWN = 0x100;
                const int WM_SYSKEYDOWN = 0x104;
                if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
                {
                    if (keyData.Equals(Keys.Escape))
                    {
                        if (this.BatchPaymentItemsGridView != null && this.BatchPaymentItemsGridView.Rows.Count > 0)
                        {
                            this.batchPaymentsDataSet.AcceptChanges();
                            this.SetPaymentRecordColor();
                        }
                    }
                }

                return base.ProcessCmdKey(ref msg, keyData);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                return false;
            }
        }

        #endregion

        #region Form Events

        /// <summary>
        /// Handles the Load event of the F1013 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1013_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkSpace();
                this.CustomizeUnpaidReceiptsGrid();
                this.notload = true;
                this.flagRateSortDirection = false;
                this.allowReceiptCreatedByComboSelctionChange = true;
                this.InitReceiptCreatedByComboBox();
                this.allowReceiptCreatedByComboSelctionChange = false;
                this.PaymentEngineUserControl.ParentWorkItem = this.form1013Controll.WorkItem;   
                this.PaymentEngineUserControl.IsAutoPayment = true;
                this.PaymentEngineUserControl.TotalDue = this.SelectedReceiptsBalaceTextBox.DecimalTextBoxValue;
                this.LoadUnpaidReceipsGrid();
                this.LoadPaymentEngineGrid();
                //this.ReceiptCheckBox.Checked = false;
                //this.ReceiptDateCalendarButton.Enabled = false;
                //this.ReceiptDateTextBox.Enabled = false;
               
                ////Get AutoPrintOnButton status
                this.AutoPrintOnButton.EnableAutoPrint = Convert.ToBoolean(this.form1013Controll.WorkItem.GetAutoPrintStatus(this.ParentFormId, TerraScanCommon.UserId));

                this.BatchPaymentsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.BatchPaymentsPictureBox.Height, this.BatchPaymentsPictureBox.Width, "Unpaid Receipts", 28, 81, 128);
                this.PaymentEnginePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PaymentEnginePictureBox.Height, this.PaymentEnginePictureBox.Width, "Payment", 174, 150, 94);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ImportSuspendedPmtsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ImportSuspendedPmtsButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ImportSuspendedPmtsButton.Select();
                FormInfo suspendedSelectionFormInfo = TerraScanCommon.GetFormInfo(1060);
                if (suspendedSelectionFormInfo.openPermission.Equals(1))
                {
                    Form suspendedPaymentSelectionForm = new Form();
                    object[] optionalParameter = new object[0];
                    suspendedPaymentSelectionForm = this.form1013Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1060, optionalParameter, this.form1013Controll.WorkItem);
                    if (suspendedPaymentSelectionForm != null)
                    {
                        if (suspendedPaymentSelectionForm.ShowDialog() == DialogResult.OK)
                        {
                            string paymentIdXml = TerraScanCommon.GetValue(suspendedPaymentSelectionForm, "PaymentIdsXml").ToString();
                            this.PaymentEngineUserControl.LoadMultiplePayment(paymentIdXml);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("FormOpenPermission") + suspendedSelectionFormInfo.visibleName + " Form", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SelectAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            if (this.unpaidReceiptRowCount > 0)
            {               
                 for (int count = 0; count < this.unpaidReceiptRowCount; count++)
                {
                    ////Modified for Bug #4494 - Kuppusamy
                    int recId;
                    int.TryParse(this.BatchPaymentItemsGridView.Rows[count].Cells[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptIDColumn.ColumnName].Value.ToString(), out recId);

                    if (!this.selectedReceiptIdsIndex.Contains(recId))
                    {
                        this.BatchPaymentItemsGridView.Rows[count].Cells[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName].Value = "True";
                    }
                }

                this.BatchPaymentItemsGridView.RefreshEdit();
                this.CalculateSelectedReceipts();
                this.UpdateGridRowsToReadOnly();
                this.CalculateBalance();
            }
        }

        /// <summary>
        /// Handles the Click event of the UnSelectAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UnSelectAllButton_Click(object sender, EventArgs e)
        {
            if (this.unpaidReceiptRowCount > 0)
            {
                for (int count = 0; count < this.unpaidReceiptRowCount; count++)
                {
                    ////Modified for Bug #4494 - Kuppusamy
                    int recId;
                    int.TryParse(this.BatchPaymentItemsGridView.Rows[count].Cells[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptIDColumn.ColumnName].Value.ToString(), out recId);

                    if (!this.selectedReceiptIdsIndex.Contains(recId))
                    {
                        this.BatchPaymentItemsGridView.Rows[count].Cells[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName].Value = "False";
                    }
                }

                this.BatchPaymentItemsGridView.RefreshEdit();
                this.CalculateSelectedReceipts();
                this.UpdateGridRowsToReadOnly();
                this.CalculateBalance();
            }
        }

        /// <summary>
        /// Handles the Click event of the ApplyPmtsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ApplyPmtsButton_Click(object sender, EventArgs e)
        {
            try
            {

                ///Modified for the CO: 9420
                ///used for checking the checkbox in receipt Date
                if (ReceiptCheckBox.Checked.Equals(true))
                {
                    if (this.ReceiptDateTextBox.Text.Equals(string.Empty))
                    {
                        MessageBox.Show("You should select Receipt Date Override.", "TerraScan T2 - Receipt Date Override", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.ReceiptDateTextBox.Focus();
                        
                    }
                    else
                    {

                    DialogResult check = MessageBox.Show("You are about to change the Receipt Date of all selected Receipts.\r\n\r\nAre you sure you want to continue?", "TerraScan T2 – Change Receipt Date", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (check.Equals(DialogResult.Yes))
                    {
                        this.ApplyButton();
                        //this.ReceiptCheckBox.Checked = false; 
                        this.ReceiptDateTextBox.Enabled = false;
                        this.ReceiptDateCalendarButton.Enabled = false;  
                     }
                    else
                    {
                        ////returns to unpaidreceipt form.
                    }
                   }
                }
                else
                {
                    this.ApplyButton(); 
                }
                this.BatchPaymentItemsGridView.Focus();

            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Payments the engine user control_ payment amount change event.
        /// </summary>
        /// <param name="amount">The amount.</param>
        private void PaymentEngineUserControl_PaymentAmountChangeEvent(decimal amount)
        {
            try
            {
                paymentValue = amount.ToString();
                this.PaymentsTotalTextBox.Text = amount.ToString();
                this.CalculateBalance();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the ReceiptCreatedByComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptCreatedByComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!this.allowReceiptCreatedByComboSelctionChange)
                {
                    this.ReceiptCreatedBYComboBoxSelectionChanged();
                    this.notload = true;
                    this.LoadUnpaidReceipsGrid();
                    this.BatchPaymentItemsGridView.RefreshEdit();
                    this.CalculateSelectedReceipts();
                    this.UpdateGridRowsToReadOnly();
                    this.CalculateBalance();
                    ////this.SelectedReceiptsBalaceTextBox.Text = string.Empty;
                    ////this.PaymentsTotalTextBox.Text = string.Empty;
                    ////this.BalanceAmountTextBox.Text = string.Empty;
                    ////this.PaymentEngineUserControl.LoadPayment();
                    ////this.ApplyPmtsButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SnapShotLoadButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SnapShotLoadButton_Click(object sender, EventArgs e)
        {
            try
            {
                object[] optionalParameter;
                /**
                 * here F9040 snapshot utility is called and all permission is sent as false since  --
                 * -- no new snapshot is created or updated
                **/
                PermissionFields currentPermissioFields = new PermissionFields();
                optionalParameter = new object[] { this.ParentFormId, currentPermissioFields };
                ////optionalParameter = new object[] { 15020, currentPermissioFields };
                Form form9040snapShotForm = new Form();
                form9040snapShotForm = this.form1013Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9040, optionalParameter, this.form1013Controll.WorkItem);

                if (form9040snapShotForm != null)
                {
                    DialogResult dialogResult = form9040snapShotForm.ShowDialog();
                    if (dialogResult.Equals(DialogResult.OK))
                    {
                        this.snapShotId = Convert.ToInt32(TerraScanCommon.GetValue(form9040snapShotForm, "CurrentSnapShotId"));
                        this.BatchSnapShotTextBox.Text = TerraScanCommon.GetValue(form9040snapShotForm, "CurrentSnapShotName").ToString();

                        F1013BatchPaymentMgmtData form1013BatchPaymentMgmtData = new F1013BatchPaymentMgmtData();
                        form1013BatchPaymentMgmtData = this.form1013Controll.WorkItem.F1013_ListSnapShotItems(this.snapShotId);

                        StringBuilder selectSnapshotReceiptIds = new StringBuilder();
                        F1013BatchPaymentMgmtData.ListUnpaidReceiptsDataTable tempListUnpaidReceipts = new F1013BatchPaymentMgmtData.ListUnpaidReceiptsDataTable();
                        for (int i = 0; i < form1013BatchPaymentMgmtData.ListSnapShotItems.Rows.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(form1013BatchPaymentMgmtData.ListSnapShotItems.Rows[i][form1013BatchPaymentMgmtData.ListSnapShotItems.KeyIDColumn.ColumnName].ToString()))
                            {
                                ////for first and last row value the "," is not appended
                                if (form1013BatchPaymentMgmtData.ListSnapShotItems.Rows.Count == 1 || i == form1013BatchPaymentMgmtData.ListSnapShotItems.Rows.Count - 1)
                                {
                                    selectSnapshotReceiptIds.Append(form1013BatchPaymentMgmtData.ListSnapShotItems.Rows[i][form1013BatchPaymentMgmtData.ListSnapShotItems.KeyIDColumn.ColumnName]);
                                }
                                else
                                {
                                    selectSnapshotReceiptIds.Append(form1013BatchPaymentMgmtData.ListSnapShotItems.Rows[i][form1013BatchPaymentMgmtData.ListSnapShotItems.KeyIDColumn.ColumnName]);
                                    selectSnapshotReceiptIds.Append(",");
                                }
                            }
                        }

                        if (selectSnapshotReceiptIds.Length > 0)
                        {
                            string selectCondtionWherevalue = this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptIDColumn.ColumnName + " IN (" + selectSnapshotReceiptIds.ToString() + ")";
                            DataRow[] unpaidReceipts = this.batchPaymentsDataSet.ListUnpaidReceipts.Select(selectCondtionWherevalue);
                            foreach (DataRow currentUnpaidReceiptsRows in unpaidReceipts)
                            {
                                bool ispaid = false;
                                for (int count = 0; count < this.selectedReceiptIdsIndex.Count; count++)
                                {
                                    if (this.selectedReceiptIdsIndex[count].ToString().Equals(currentUnpaidReceiptsRows.ItemArray[0].ToString()))
                                    {
                                        ispaid = true;
                                        break;
                                    }
                                }

                                if (!ispaid)
                                {
                                    tempListUnpaidReceipts.ImportRow(currentUnpaidReceiptsRows);
                                    tempListUnpaidReceipts.Rows[tempListUnpaidReceipts.Rows.Count - 1][this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName] = "False";
                                }
                            }
                        }

                        /* 
                         * here the ReceiptCreatedByComboBox value is set to option "ALL"
                         * and the combox box is disabled.
                         */
                        this.allowReceiptCreatedByComboSelctionChange = true;

                        this.ReceiptCreatedByComboBox.SelectedValue = 0;
                        this.ReceiptCreatedBYComboBoxSelectionChanged();
                        this.TypePanel.Enabled = false;
                        this.SnapShotClearButton.Enabled = true;
                        this.allowReceiptCreatedByComboSelctionChange = false;
                        this.batchPaymentsDataSet.ListUnpaidReceipts.Clear();
                        this.batchPaymentsDataSet.ListUnpaidReceipts.Merge(tempListUnpaidReceipts);
                        /* the selected receipts are cleared*/
                        if (this.selectedReceiptIds != null)
                        {
                            this.selectedReceiptIds.Clear();
                        }
                        ///used for sorting.
                        this.notload = true;
                        this.PopulateUnpaidReceiptGridView();
                        this.CalculateSelectedReceipts();
                        this.UpdateGridRowsToReadOnly();
                        this.CalculateBalance();
                   
                        this.LoadPaymentEngineGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SnapShotClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SnapShotClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.BatchSnapShotTextBox.Text = string.Empty;
                this.ReceiptCreatedByComboBox.SelectedValue = 0;
                this.TypePanel.Enabled = true;
                this.SnapShotClearButton.Enabled = false;
                this.ReceiptCreatedBYComboBoxSelectionChanged();
                /* the selected receipts are cleared*/
                if (this.selectedReceiptIds != null)
                {
                    this.selectedReceiptIds.Clear();
                }

                this.LoadUnpaidReceipsGrid();
                this.CalculateSelectedReceipts();
                this.UpdateGridRowsToReadOnly();
                this.CalculateBalance();
                this.LoadPaymentEngineGrid();
             }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                DataTable receiptTable = new DataTable();
                receiptTable.Columns.AddRange(new DataColumn[] { new DataColumn("ReceiptId") });
                DataRow[] checkedRow = null;
                string expression = "ReceiptSelected=True";
                this.batchPaymentsDataSet.ListUnpaidReceipts.AcceptChanges();
                ////Get the collection of checked rows
                checkedRow = this.batchPaymentsDataSet.ListUnpaidReceipts.Select(expression);

                if (checkedRow.Length > 0)
                {
                    for (int i = 0; i < checkedRow.Length; i++)
                    {
                        DataRow row = receiptTable.NewRow();
                        row["ReceiptId"] = checkedRow[i]["ReceiptID"].ToString();
                        receiptTable.Rows.Add(row);
                    }
                                                     
                    string receiptItems = TerraScanCommon.GetXmlString(receiptTable);
                    int isdeleted = 0;
                    try
                    {
                        isdeleted = this.form1013Controll.WorkItem.F1013_DeleteReceiptItems(this.ppaymentId, receiptItems, TerraScanCommon.UserId);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                    }

                    if (isdeleted == 1)
                    {
                        ////Remove Deleted Records from Grid
                        for (int i = 0; i < this.BatchPaymentItemsGridView.OriginalRowCount; i++)
                        {
                            if (Convert.ToBoolean(this.BatchPaymentItemsGridView.Rows[i].Cells["ReceiptSelected"].Value.ToString()))
                            {
                                this.BatchPaymentItemsGridView.Rows.RemoveAt(i);
                                if (i == 1)
                                {
                                    i = i - 1;
                                }
                                else if (i == 0)
                                {
                                    i = -1;
                                }
                                else
                                {
                                    i = i - 2;
                                }
                            }
                        }

                        this.batchPaymentsDataSet.ListUnpaidReceipts.AcceptChanges();
                        this.CalculateSelectedReceipts();
                        this.UpdateGridRowsToReadOnly();
                        this.CalculateBalance();
                        
                        this.SetPaymentRecordColor();
                      
                        ////Coding Added for the Issue for 4516 on 27/2/2009 by Malliga
                        this.SelectedReceiptsBalaceTextBox.Text = string.Empty;
                        ////Ends here
                        ////this.LoadUnpaidReceipsGrid();
                        ////this.LoadPaymentEngineGrid();
                    }
                }
                else
                {
                    this.SetPaymentRecordColor();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the AutoPrintOnButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AutoPrintOnButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.AutoPrintOnButton.EnableAutoPrint)
                {
                    this.AutoPrintOnButton.EnableAutoPrint = false;
                }
                else
                {
                    this.AutoPrintOnButton.EnableAutoPrint = true;
                }

                this.form1013Controll.WorkItem.SaveAutoPrint(this.ParentFormId, TerraScanCommon.UserId, this.AutoPrintOnButton.EnableAutoPrint);
            }
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        
        #endregion

        #region Grid Events

        /// <summary>
        /// Customizes the unpaid receipts grid.
        /// </summary>
        private void CustomizeUnpaidReceiptsGrid()
        {
            this.BatchPaymentItemsGridView.AutoGenerateColumns = false;
            this.BatchPaymentItemsGridView.PrimaryKeyColumnName = this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptIDColumn.ColumnName;
            this.BatchPaymentItemsGridView.AllowUserToResizeColumns = false;
            this.BatchPaymentItemsGridView.AllowUserToResizeRows = false;
            this.ReceiptSelected.DataPropertyName = this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName;
            this.ReceiptID.DataPropertyName = this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptIDColumn.ColumnName;
            this.ReceiptNaumber.DataPropertyName = this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptNumberColumn.ColumnName;
            this.StatementID.DataPropertyName = this.batchPaymentsDataSet.ListUnpaidReceipts.StatementIDColumn.ColumnName;
            this.StatementNumber.DataPropertyName = this.batchPaymentsDataSet.ListUnpaidReceipts.StatementNumberColumn.ColumnName;
            this.TaxAmount.DataPropertyName = this.batchPaymentsDataSet.ListUnpaidReceipts.TaxAmountColumn.ColumnName;
            this.TotalAmount.DataPropertyName = this.batchPaymentsDataSet.ListUnpaidReceipts.TotalAmountColumn.ColumnName;
            this.ReceiptDate.DataPropertyName = this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptDateColumn.ColumnName;
            this.PPaymentID.DataPropertyName = this.batchPaymentsDataSet.ListUnpaidReceipts.PPaymentIDColumn.ColumnName;
            this.PostTypeID.DataPropertyName = this.batchPaymentsDataSet.ListUnpaidReceipts.PostTypeIDColumn.ColumnName;
            this.PostName.DataPropertyName = this.batchPaymentsDataSet.ListUnpaidReceipts.PostNameColumn.ColumnName;
            this.ReceiptForm.DataPropertyName = this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptFormColumn.ColumnName;
        }

        /// <summary>
        /// Handles the CellFormatting event of the BatchPaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void BatchPaymentItemsGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outTaxDecimal;
            Decimal outTotalDecimal;
            try
            {
                //// Only paint if desired, formattable column  Tax Amount Column.
                if (e.ColumnIndex == this.BatchPaymentItemsGridView.Columns["TaxAmount"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["TaxAmount"].Value.ToString().Trim()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outTaxDecimal))
                        {
                            if (outTaxDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outTaxDecimal).ToString("#,##0.00"), ")");
                                e.CellStyle.ForeColor = Color.Green;
                            }
                            else
                            {
                                e.Value = outTaxDecimal.ToString("#,##0.00");
                                e.FormattingApplied = true;
                            }
                        }
                        else
                        {
                            e.Value = "0.00 ";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }

                //// Only paint if desired, formattable column  Total Amount Column.

                if (e.ColumnIndex == this.BatchPaymentItemsGridView.Columns["TotalAmount"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["TotalAmount"].Value.ToString().Trim()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outTotalDecimal))
                        {
                            if (outTotalDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outTotalDecimal).ToString("#,##0.00"), ")");
                                e.CellStyle.ForeColor = Color.Green;
                            }
                            else
                            {
                                e.Value = outTotalDecimal.ToString("#,##0.00");
                                e.FormattingApplied = true;
                            }
                        }
                        else
                        {
                            e.Value = "0.00 ";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }

                ////Receipt Number Link Format
                if (e.ColumnIndex == this.BatchPaymentItemsGridView.Columns["ReceiptNaumber"].Index)
                {
                    if (this.BatchPaymentItemsGridView.Rows[e.RowIndex].Selected || this.BatchPaymentItemsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                    {
                        (BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["ReceiptNaumber"] as DataGridViewLinkCell).LinkColor = Color.White;
                        (BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["ReceiptNaumber"] as DataGridViewLinkCell).ActiveLinkColor = Color.White;
                        (BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["ReceiptNaumber"] as DataGridViewLinkCell).VisitedLinkColor = Color.White;
                    }
                    else
                    {
                        (BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["ReceiptNaumber"] as DataGridViewLinkCell).LinkColor = Color.Blue;
                        (BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["ReceiptNaumber"] as DataGridViewLinkCell).ActiveLinkColor = Color.Red;
                        (BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["ReceiptNaumber"] as DataGridViewLinkCell).VisitedLinkColor = Color.Blue;
                    }
                }

                ////Statement Number Link Format
                if (e.ColumnIndex == this.BatchPaymentItemsGridView.Columns["StatementNumber"].Index)
                {
                    if (this.BatchPaymentItemsGridView.Rows[e.RowIndex].Selected || this.BatchPaymentItemsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                    {
                        (BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["StatementNumber"] as DataGridViewLinkCell).LinkColor = Color.White;
                        (BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["StatementNumber"] as DataGridViewLinkCell).ActiveLinkColor = Color.White;
                        (BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["StatementNumber"] as DataGridViewLinkCell).VisitedLinkColor = Color.White;
                    }
                    else
                    {
                        (BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["StatementNumber"] as DataGridViewLinkCell).LinkColor = Color.Blue;
                        (BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["StatementNumber"] as DataGridViewLinkCell).ActiveLinkColor = Color.Red;
                        (BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["StatementNumber"] as DataGridViewLinkCell).VisitedLinkColor = Color.Blue;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the BatchPaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void BatchPaymentItemsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.gridContentCliked = true;

            if (e.RowIndex >= 0 && e.RowIndex < this.BatchPaymentItemsGridView.OriginalRowCount)
            {
                this.SetControlValues(this.GetSelectedRow(e.RowIndex));
                
                //// Receipt Item CheckBox Checked/Unchecked.
                if (e.ColumnIndex.Equals(this.BatchPaymentItemsGridView.Columns["ReceiptSelected"].Index))
                {
                    ////Code added for bug fixing #4494 - Kuppu - From here                     
                    int recId;
                    int.TryParse(this.BatchPaymentItemsGridView.Rows[e.RowIndex].Cells[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptIDColumn.ColumnName].Value.ToString(), out recId);

                    /*Till Here*/

                    //// UnSelecting Applied Payments.
                    if (this.selectedReceiptIdsIndex.Contains(recId))
                    {
                        this.BatchPaymentItemsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;

                        /*Commented By Ramya.D For BugID 2092*/

                        //// this.batchPaymentsDataSet.ListUnpaidReceipts.AcceptChanges();
                        //// this.BatchPaymentItemsGridView.RefreshEdit();

                        /*Till Here*/
                    }
                    else
                    {
                        if (this.BatchPaymentItemsGridView.Rows[e.RowIndex].Cells[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName].Value != null && !string.IsNullOrEmpty(this.BatchPaymentItemsGridView.Rows[e.RowIndex].Cells[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName].Value.ToString()))
                        {
                            if (Convert.ToBoolean(this.BatchPaymentItemsGridView.Rows[e.RowIndex].Cells[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName].Value))
                            {
                                this.BatchPaymentItemsGridView.Rows[e.RowIndex].Cells[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName].Value = false;
                            }
                            else
                            {
                                if (!this.BatchPaymentItemsGridView[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName, e.RowIndex].ReadOnly)
                                 {
                                    this.BatchPaymentItemsGridView.Rows[e.RowIndex].Cells[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName].Value = true;
                                }
                            }
                        }
                        else
                        {
                            this.BatchPaymentItemsGridView.Rows[e.RowIndex].Cells[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName].Value = false;
                        }
                    }

                    this.notload = false;
                    ////this.batchPaymentsDataSet.ListUnpaidReceipts.AcceptChanges();
                    this.CalculateSelectedReceipts();
                    this.UpdateGridRowsToReadOnly();
                    this.CalculateBalance();
                    
                    /////* to select the currently selected row index of the grid*/
                    this.receiptGridBindingSource.DataSource = this.BatchPaymentItemsGridView.DataSource;
                    int currentselectedReceiptId = -99;
                    ////to get the last seleted receipt id
                    if (this.selectedReceiptIds != null)
                    {
                        if (this.BatchPaymentItemsGridView.Rows[e.RowIndex].Cells[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName].Value.Equals(true))
                        {
                            TerraScanCommon.SetDataGridViewPosition(this.BatchPaymentItemsGridView, e.RowIndex);
                        }
                    }
                }

                //// Receipt Number HyperLink click.    
                if (e.ColumnIndex.Equals(this.BatchPaymentItemsGridView.Columns["ReceiptNaumber"].Index) && !string.IsNullOrEmpty(this.BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["ReceiptNaumber"].Value.ToString()))
                {
                    if (!string.IsNullOrEmpty(this.BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["ReceiptID"].Value.ToString()))
                    {
                        int.TryParse(this.BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["ReceiptID"].Value.ToString(), out this.currentRowReceiptId);
                    }

                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(11001);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = this.currentRowReceiptId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }

                ////Statement Number Hyperlink Click.
                if (e.ColumnIndex.Equals(this.BatchPaymentItemsGridView.Columns["StatementNumber"].Index) && !string.IsNullOrEmpty(this.BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["StatementNumber"].Value.ToString()))
                {
                    if (!string.IsNullOrEmpty(this.BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["StatementID"].Value.ToString()))
                    {
                        int.TryParse(this.BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["StatementID"].Value.ToString(), out this.currentRowReceiptId);
                    }

                    if (!string.IsNullOrEmpty(this.BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["ReceiptForm"].Value.ToString()))
                    {
                        int.TryParse(this.BatchPaymentItemsGridView.Rows[e.RowIndex].Cells["ReceiptForm"].Value.ToString(), out this.currentRowReceiptForm);
                    }

                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(this.currentRowReceiptForm);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = this.currentRowReceiptId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
            }
        }

        /// <summary>
        /// Gets the selected row.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns>the SuspendedPaymentRow.</returns>
        private F1013BatchPaymentMgmtData.ListUnpaidReceiptsRow GetSelectedRow(int rowIndex)
        {
            return (F1013BatchPaymentMgmtData.ListUnpaidReceiptsRow)this.batchPaymentsDataSet.ListUnpaidReceipts.Rows[rowIndex];
        }

        //// check assign your controls with values from the typeddataset row

        /// <summary>
        /// Sets the control values.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void SetControlValues(F1013BatchPaymentMgmtData.ListUnpaidReceiptsRow selectedRow)
        {
            this.currentRowReceiptId = selectedRow.ReceiptID;
            this.currentRowReceiptForm = selectedRow.ReceiptForm;
        }
        //public  virtual void sort(DataGridViewColumn newcolumn, ListSortDirection direction)
        //{

            
        //}
        
        //private void sort(int column , string direction)
        //{
              
        //    column = this.lastgridclick; 
            
        //    if(direction =="Ascending")
        //    {
        //        direction = "Descending";
        //    }
        //    else
        //    {
        //        direction = "Ascending";
        //    }
        //}

        /// <summary>
        /// Handles the ColumnHeaderMouseClick event of the BatchPaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void BatchPaymentItemsGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //DataGridViewColumn newColumn= this.BatchPaymentItemsGridView.Columns.GetColumnCount(DataGridViewElementStates.Selected)==1 ? this.BatchPaymentItemsGridView.SelectedColumns[0]:null ;
           
           DataGridViewColumn newColumn = BatchPaymentItemsGridView.Columns[e.ColumnIndex];
 
           if (e.ColumnIndex.Equals(3) || e.ColumnIndex.Equals(4) || e.ColumnIndex.Equals(5) || e.ColumnIndex.Equals(7) || e.ColumnIndex.Equals(8) || e.ColumnIndex.Equals(9))
           {
               this.flagRateSortDirection = !this.flagRateSortDirection;
               this.lastgridclick = e.ColumnIndex;
               var column = this.BatchPaymentItemsGridView.Columns[e.ColumnIndex];
               var sortGlyph = column.HeaderCell.SortGlyphDirection;
               sortGlypDirection = column.HeaderCell.SortGlyphDirection;  
              //////changed in PopulateUnpaidReceiptGridView 
               //switch (sortGlyph)
               //{
               //    case SortOrder.None:
               //    case SortOrder.Ascending:
               //        //this.BatchPaymentItemsGridView.Sort(column, ListSortDirection.Ascending);
               //        column.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
               //        //this.batchPaymentsDataSet.ListUnpaidReceipts.DefaultView.Sort = column.DataPropertyName + " ASC";
               //        break;
               //    case SortOrder.Descending:
               //        //this.BatchPaymentItemsGridView.Sort(column, ListSortDirection.Descending);
               //        column.HeaderCell.SortGlyphDirection = SortOrder.Descending;
               //        //this.batchPaymentsDataSet.ListUnpaidReceipts.DefaultView.Sort = column.DataPropertyName + " DESC";
               //        break;
               //}
               //if (sortGlyph == SortOrder.Descending)
               //{
               //    //this.BatchPaymentItemsGridView.Columns[this.lastgridclick].HeaderCell.SortGlyphDirection = SortOrder.Descending;
               //    column.HeaderCell.SortGlyphDirection = SortOrder.Descending;
               //}
               //if (sortGlyph == SortOrder.Ascending)
               //{
               //    //this.BatchPaymentItemsGridView.Columns[this.lastgridclick].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
               //    column.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
               //}
              ////If condition has been added for avoid null Exception
               if (this.selectedReceiptIds != null)
               {
                   if (this.selectedReceiptIds.Count.Equals(0))
                   {
                       for (int count = 0; count < this.selectedReceiptIdsIndex.Count; count++)
                       {
                           ////this.BatchPaymentItemsGridView.Rows[this.selectedReceiptIdsIndex[count]].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(159, 191, 147);
                           ////this.BatchPaymentItemsGridView[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName, this.selectedReceiptIdsIndex[count]].ReadOnly = true;
                           ////this.BatchPaymentItemsGridView[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName, this.selectedReceiptIdsIndex[count]].Value = false;
                           this.readonlyRowIndex = new List<int>();
                           int readonlyRowIndexID = 0;
                           ////int selectedRowIndex = 0;
                           int rowIndex = 0;
                           for (rowIndex = 0; rowIndex < this.BatchPaymentItemsGridView.Rows.Count; rowIndex++)
                           {
                               if (!string.IsNullOrEmpty(this.BatchPaymentItemsGridView.Rows[rowIndex].Cells["ReceiptID"].Value.ToString()))
                               {
                                   int.TryParse(this.BatchPaymentItemsGridView.Rows[rowIndex].Cells["ReceiptID"].Value.ToString(), out readonlyRowIndexID);

                                   if (readonlyRowIndexID == this.selectedReceiptIdsIndex[count])
                                   {
                                       this.readonlyRowIndex.Add(rowIndex);
                                   }
                               }
                           }

                           for (int i = 0; i < this.readonlyRowIndex.Count; i++)
                           {

                               this.BatchPaymentItemsGridView.Rows[this.readonlyRowIndex[i]].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(159, 191, 147);
                               this.BatchPaymentItemsGridView[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName, this.readonlyRowIndex[i]].ReadOnly = true;
                               this.BatchPaymentItemsGridView[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName, this.readonlyRowIndex[i]].Value = false;
                           }
                       }
                   }
                   else
                   {
                       for (int rowCount = 0; rowCount < this.selectedReceiptIds.Count; rowCount++)
                       {
                           this.receiptGridBindingSource.DataSource = this.BatchPaymentItemsGridView.DataSource;

                           /*Commented By Ramya.D For BugID 2092*/
                           ////this.receiptGridBindingSource.Sort = this.BatchPaymentItemsGridView.Columns[e.ColumnIndex].DataPropertyName + " " + this.BatchPaymentItemsGridView.BaseSortOrder;

                           /* to get row index of the grid based on receipt id*/
                           int currentselectedReceiptId;
                           currentselectedReceiptId = this.selectedReceiptIds[rowCount];

                           int currentDatagridRowIndex = -1;
                           currentDatagridRowIndex = this.receiptGridBindingSource.Find(this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptIDColumn.ColumnName, currentselectedReceiptId);
                           if (this.isapplyPrintssaved && currentDatagridRowIndex >= 0 && currentDatagridRowIndex < this.batchPaymentsDataSet.ListUnpaidReceipts.Rows.Count)
                           {
                               this.readonlyRowIndex = new List<int>();
                               int readonlyRowIndexID = 0;
                               int selectedRowIndex = 0;
                               int rowIndex = 0;
                               for (int count = 0; count < this.selectedReceiptIdsIndex.Count; count++)
                               {
                                   for (rowIndex = 0; rowIndex < this.BatchPaymentItemsGridView.Rows.Count; rowIndex++)
                                   {
                                       if (!string.IsNullOrEmpty(this.BatchPaymentItemsGridView.Rows[rowIndex].Cells["ReceiptID"].Value.ToString()))
                                       {
                                           int.TryParse(this.BatchPaymentItemsGridView.Rows[rowIndex].Cells["ReceiptID"].Value.ToString(), out readonlyRowIndexID);

                                           if (readonlyRowIndexID == this.selectedReceiptIdsIndex[count])
                                           {
                                               this.readonlyRowIndex.Add(rowIndex);
                                           }
                                       }
                                   }
                               }

                               for (int count = 0; count < this.readonlyRowIndex.Count; count++)
                               {
                                   this.BatchPaymentItemsGridView.Rows[this.readonlyRowIndex[count]].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(159, 191, 147);
                                   this.BatchPaymentItemsGridView[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName, this.readonlyRowIndex[count]].ReadOnly = true;
                                   this.BatchPaymentItemsGridView[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName, this.readonlyRowIndex[count]].Value = false;
                               }
                           }
                       }
                   }
               }
           }
        }

        /// <summary>
        /// Payments the engine user control_ payment item change event.
        /// </summary>
        private void PaymentEngineUserControl_PaymentItemChangeEvent()
        {
            this.PaymentEngineUserControl.TotalDue = this.SelectedReceiptsBalaceTextBox.DecimalTextBoxValue;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads the payment engine grid.
        /// </summary>
        private void LoadPaymentEngineGrid()
        {
            this.PaymentEngineUserControl.PaymentAmountChangeEvent += new TerraScan.PaymentEngine.PaymentEngineUserControl.PaymentAmountChangeEventHandler(this.PaymentEngineUserControl_PaymentAmountChangeEvent);
            this.PaymentEngineUserControl.LoadPayment();
            this.ApplyPmtsButton.Enabled = false;
            this.SelectedReceiptsBalaceTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Loads the work space.
        /// </summary>
        private void LoadWorkSpace()
        {
            if (this.form1013Controll.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1013Controll.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1013Controll.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            this.SetFormHeader(this, new DataEventArgs<string[]>(new string[] { SharedFunctions.GetResourceString("F1013UnpaidReceipts"), string.Empty }));

            if (this.form1013Controll.WorkItem.SmartParts.Contains(SmartPartNames.ReportActionSmartPart))
            {
                this.ReportActionSmartPartDeckWorkspace.Show(this.form1013Controll.WorkItem.SmartParts.Get(SmartPartNames.ReportActionSmartPart));
            }
            else
            {
                this.ReportActionSmartPartDeckWorkspace.Show(this.form1013Controll.WorkItem.SmartParts.AddNew<ReportActionSmartPart>(SmartPartNames.ReportActionSmartPart));
            }
            // To Load AdditionalOperationSmartPart into CommentsdeckWorkspace
            if (this.form1013Controll.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.CommentWorkSpace.Show(this.form1013Controll.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart));
            }
            else
            {
                this.CommentWorkSpace.Show(this.form1013Controll.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart));
            }
            //////set required variable - attachment and comment
            this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.form1013Controll.WorkItem.SmartParts[SmartPartNames.AdditionalOperationSmartPart];
            this.additionalOperationSmartPart.ParentWorkItem = this.form1013Controll.WorkItem;
            this.additionalOperationSmartPart.ParentFormId = Convert.ToInt32(this.Tag);
            this.additionalOperationSmartPart.CurrntFormId = Convert.ToInt32(this.Tag);
        }

        /// <summary>
        /// Inits the receipt created by combo box.
        /// </summary>
        private void InitReceiptCreatedByComboBox()
        {
            F1013BatchPaymentMgmtData.ListUnpaidReceiptUsersDataTable recieptUserDataTable = new F1013BatchPaymentMgmtData.ListUnpaidReceiptUsersDataTable();
            DataRow customRow = recieptUserDataTable.NewRow();
            recieptUserDataTable.Clear();

            ////All is addded
            customRow[this.batchPaymentsDataSet.ListUnpaidReceiptUsers.UserIDColumn.ColumnName] = "0";
            customRow[this.batchPaymentsDataSet.ListUnpaidReceiptUsers.DisplayNameColumn.ColumnName] = "All";
            recieptUserDataTable.Rows.Add(customRow);

            this.ReceiptCreatedByComboBox.DataSource = null;
            this.batchPaymentsDataSet = this.form1013Controll.WorkItem.F1013_ListUnpaidReceipts(null);
            this.ReceiptCreatedByComboBox.ValueMember = this.batchPaymentsDataSet.ListUnpaidReceiptUsers.UserIDColumn.ColumnName;
            this.ReceiptCreatedByComboBox.DisplayMember = this.batchPaymentsDataSet.ListUnpaidReceiptUsers.DisplayNameColumn.ColumnName;

            ////The original datatable and temp datatable is merged
            recieptUserDataTable.Merge(this.batchPaymentsDataSet.ListUnpaidReceiptUsers);
            this.ReceiptCreatedByComboBox.DataSource = recieptUserDataTable;

            ////Checking for the Active User.
            if (this.batchPaymentsDataSet.ListUnpaidReceiptUsers.Rows.Count > 0)
            {
                foreach (F1013BatchPaymentMgmtData.ListUnpaidReceiptUsersRow userRow in this.batchPaymentsDataSet.ListUnpaidReceiptUsers.Rows)
                {
                    if (userRow.UserID.Equals(TerraScanCommon.UserId))
                    {
                        this.userId = userRow.UserID;
                        break;
                    }
                    else
                    {
                        this.userId = null;
                    }
                }
            }

            if (this.userId.Equals(null))
            {
                this.ReceiptCreatedByComboBox.SelectedValue = 0;
            }
            else
            {
                this.ReceiptCreatedByComboBox.SelectedValue = this.userId;
            }
            
        }

        /// <summary>
        /// Loads the unpaid receips grid.
        /// </summary>
        private void LoadUnpaidReceipsGrid()
        {
            try
            {
                this.gridContentCliked = false;
                this.batchPaymentsDataSet = this.form1013Controll.WorkItem.F1013_ListUnpaidReceipts(this.userId);
                ////Added by purushotham due to implemnetCO#20630
                if (this.batchPaymentsDataSet.ListDateReciepts.Rows.Count > 0)
                {
                    var isReturned = this.batchPaymentsDataSet.ListDateReciepts[0].IsDateOverrideChecked.ToString();
                    if (!string.IsNullOrEmpty(isReturned) && isReturned.Equals("1"))
                    {
                        var selectedDate = this.batchPaymentsDataSet.ListDateReciepts[0].DefaultDate.ToString();
                        this.ReceiptDateTextBox.Enabled = true;
                        this.ReceiptDateCalendarButton.Enabled = true;
                        this.ReceiptDatePanel.Enabled = true;
                        this.ReceiptCheckBox.Checked = true;
                        this.ReceiptDateTextBox.Text = selectedDate;
                    }
                    else
                    {
                        this.ReceiptDateTextBox.SelectedText = null;
                        this.ReceiptCheckBox.Checked = false;
                        this.ReceiptDateTextBox.Enabled = false;
                        this.ReceiptDateCalendarButton.Enabled = false;
                    }
                }
                
                
                ///used for the grid view sorting fix
                this.PopulateUnpaidReceiptGridView();
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Populates the unpaid receipt grid view.
        /// </summary>
        private void PopulateUnpaidReceiptGridView()
        {
            try
            {
                if (this.lastgridclick != 0)
                {
                    if (this.notload)
                    {
                        ///clear the table for repopulating the data
                        //this.batchPaymentsDataSet.ListUnpaidReceipts.Clear(); 
                        
                        
                        if (this.BatchPaymentItemsGridView.Columns.Count > 0)
                        {
                            var column = this.BatchPaymentItemsGridView.Columns[this.lastgridclick];
                            //var sortGlyph = column.HeaderCell.SortGlyphDirection;
                             F1013BatchPaymentMgmtData.ListUnpaidReceiptsRow[] tempTable;
                             ListSortDirection direction;
                            if (sortGlypDirection.Equals(SortOrder.Descending))   //(this.flagRateSortDirection)
                            {
                                tempTable = (F1013BatchPaymentMgmtData.ListUnpaidReceiptsRow[])this.batchPaymentsDataSet.ListUnpaidReceipts.Copy().Select("1=1", this.BatchPaymentItemsGridView.Columns[this.lastgridclick].DataPropertyName + SharedFunctions.GetResourceString("DESC"));
                                //this.BatchPaymentItemsGridView.Columns[this.lastgridclick].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                                this.batchPaymentsDataSet.ListUnpaidReceipts.DefaultView.Sort = column.DataPropertyName + " DESC";
                                direction = ListSortDirection.Descending;
                            }
                            else  
                            {
                                tempTable = (F1013BatchPaymentMgmtData.ListUnpaidReceiptsRow[])this.batchPaymentsDataSet.ListUnpaidReceipts.Copy().Select("1=1", this.BatchPaymentItemsGridView.Columns[this.lastgridclick].DataPropertyName + SharedFunctions.GetResourceString("ASC"));
                                //this.BatchPaymentItemsGridView.Columns[this.lastgridclick].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                                this.batchPaymentsDataSet.ListUnpaidReceipts.DefaultView.Sort = column.DataPropertyName + " ASC";
                                direction=ListSortDirection.Ascending; 

                            }
                            this.batchPaymentsDataSet.ListUnpaidReceipts.Clear();
                            //// add the rows after sorting
                            foreach (F1013BatchPaymentMgmtData.ListUnpaidReceiptsRow myrow in tempTable)
                            {
                                this.batchPaymentsDataSet.ListUnpaidReceipts.ImportRow(myrow);
                            }
                           
                         }
                        this.BatchPaymentItemsGridView.DataSource = this.batchPaymentsDataSet.ListUnpaidReceipts.DefaultView;
                        ListSortDirection direction1;
                        var column1 = this.BatchPaymentItemsGridView.Columns[this.lastgridclick];
                        if(sortGlypDirection.Equals(SortOrder.Descending))
                        {
                            direction1 = ListSortDirection.Descending;
                            column1.HeaderCell.SortGlyphDirection = SortOrder.Descending;    
                         }
                        else
                        {
                            direction1 = ListSortDirection.Ascending;
                            column1.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                        }
                        //// add a default empty row for RateDetailsDataGrid
                        //this.batchPaymentsDataSet.ListUnpaidReceipts.AddListUnpaidReceiptsRow(this.listunpaidreceipt.NewListUnpaidReceiptsRow());

                        int row;
                        for (row = this.batchPaymentsDataSet.ListUnpaidReceipts.Rows.Count; row < this.BatchPaymentItemsGridView.NumRowsVisible; row++)
                        {
                            this.batchPaymentsDataSet.ListUnpaidReceipts.AddListUnpaidReceiptsRow(this.batchPaymentsDataSet.ListUnpaidReceipts.NewListUnpaidReceiptsRow());
                        }
                        this.notload = false;
                    }
                }
                else
                {
                    this.BatchPaymentItemsGridView.DataSource = this.batchPaymentsDataSet.ListUnpaidReceipts.DefaultView;  
                }
                /// comments not working while no of rows 
                /// filled less than no of rows visible
                    
                //if (this.notload)
                //{
                    //this.BatchPaymentItemsGridView.DataSource = this.batchPaymentsDataSet.ListUnpaidReceipts;
                   //DataRow[] sortaft = this.batchPaymentsDataSet.ListUnpaidReceipts.Select("EmptyRecord$=False");
                    //if (sortaft.Length > 0)
                    //{
                   
                    //    if (this.lastgridclick != 0)
                    //    {
                    //        var column = this.BatchPaymentItemsGridView.Columns[this.lastgridclick];
                    //        if (this.ascsort)
                    //        {
                    //              this.BatchPaymentItemsGridView.Sort(column, ListSortDirection.Ascending);
                    //        }
                    //        else
                    //        {
                    //           this.BatchPaymentItemsGridView.Sort(column, ListSortDirection.Descending);
                    //        }
                    //       this.notload = false;
                    //    }
                    //}
                //}
               //this.BatchPaymentItemsGridView.Sort(this.BatchPaymentItemsGridView.Columns[this.lastgridclick],           
                this.unpaidReceiptRowCount = this.BatchPaymentItemsGridView.OriginalRowCount;

                if (this.unpaidReceiptRowCount > 0)
                {
                  
                    ////this.BatchPaymentItemsGridView.DataSource = this.batchPaymentsDataSet.ListUnpaidReceipts;
                    ////this.BatchPaymentItemsGridView.NumRowsVisible = 14;
                    this.BatchPaymentItemsGridView.Enabled = true;
                    ////khaja set focus to fix Bug#5901
                    this.BatchPaymentItemsGridView.Focus();
                    //// Enable Payment Engine.
                    this.PaymentEngineUserControl.SetDefaultSelection = true;
                    this.ImportSuspendedPmtsButton.Enabled = this.IsAppliedAllPayments();
               

                    ////if (this.BatchPaymentItemsGridView.CurrentRowIndex > 0)
                    ////{
                        this.BatchPaymentItemsGridView.Rows[this.BatchPaymentItemsGridView.CurrentRowIndex].Selected = true;
                    ////}
                    this.PaymentEngineUserControl.TabStop = true;
                    this.PaymentEngineUserControl.Enabled = true;
                    //this.ReceiptCreatedByComboBox.Focus();
                    this.AutoPrintOnButton.EnableAutoPrint = Convert.ToBoolean(this.form1013Controll.WorkItem.GetAutoPrintStatus(this.ParentFormId, TerraScanCommon.UserId));                    
                }
                else
                {
                    ////this.BatchPaymentItemsGridView.DataSource = null;
                    ////this.BatchPaymentItemsGridView.NumRowsVisible = 14; 
                    this.BatchPaymentItemsGridView.Rows[0].Selected = false;
                    this.BatchPaymentItemsGridView.Enabled = false;
                    //// Enable Payment Engine.
                    this.PaymentEngineUserControl.TabStop = false;
                    this.PaymentEngineUserControl.Enabled = false;
                    this.PaymentEngineUserControl.SetDefaultSelection = false;
                    this.ImportSuspendedPmtsButton.Enabled = false;
                    this.AutoPrintOnButton.EnableAutoPrint = false;
                }

                if (this.unpaidReceiptRowCount > this.BatchPaymentItemsGridView.NumRowsVisible)
                {
                    this.BatchPaymentItmesVScrollBar.Visible = false;
                }
                else
                {
                    this.BatchPaymentItmesVScrollBar.Visible = true;
                }
                //var column1 = this.BatchPaymentItemsGridView.Columns[this.lastgridclick];
                //var sortGlyph = column1.HeaderCell.SortGlyphDirection;  
                //this.flagLoadOnProcess = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Calculates the selected receipts.
        /// </summary>
        private void CalculateSelectedReceipts()
        {
            try
            {
                this.selectedReceiptIds = new List<int>();
                this.selectedReceiptsBalance = 0;
                ////this.batchPaymentsDataSet.ListUnpaidReceipts.AcceptChanges();
                this.BatchPaymentItemsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                ///used for stop sorting
                this.PopulateUnpaidReceiptGridView();
                ////-1 has been removed - have to check
                for (int count = 0; count < this.BatchPaymentItemsGridView.OriginalRowCount; count++)
                {
                    if (Convert.ToBoolean(this.BatchPaymentItemsGridView[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName, count].Value))
                    {
                        this.selectedReceiptIds.Add(Convert.ToInt32(this.BatchPaymentItemsGridView[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptIDColumn.ColumnName, count].Value));
                        try
                        {
                            if (!string.IsNullOrEmpty(this.BatchPaymentItemsGridView[this.batchPaymentsDataSet.ListUnpaidReceipts.TotalAmountColumn.ColumnName, count].Value.ToString()))
                            {
                                this.selectedReceiptsBalance += Convert.ToDecimal(this.BatchPaymentItemsGridView[this.batchPaymentsDataSet.ListUnpaidReceipts.TotalAmountColumn.ColumnName, count].Value);
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                        }
                    }
                }

                this.SelectedReceiptsBalaceTextBox.Text = this.selectedReceiptsBalance.ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Gets the selected receipt ids XML.
        /// </summary>
        private void GetSelectedReceiptIdsXml()
        {
            this.selectedReceiptIdsXml = string.Empty;
            DataTable tempXMLdataTable = new DataTable();
            foreach (DataColumn column in this.batchPaymentsDataSet.ListUnpaidReceipts.Columns)
            {
                if (column.ColumnName == this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptIDColumn.ColumnName)
                {
                    tempXMLdataTable.Columns.Add(new DataColumn(column.ColumnName));
                }
            }

            for (int item = 0; item < this.selectedReceiptIds.Count; item++)
            {
                DataRow tempXMLDataRow = tempXMLdataTable.NewRow();
                tempXMLDataRow[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptIDColumn.ColumnName] = this.selectedReceiptIds[item].ToString();
                tempXMLdataTable.Rows.Add(tempXMLDataRow);
            }

            this.selectedReceiptIdsXml = TerraScanCommon.GetXmlString(tempXMLdataTable);
        }

        /// <summary>
        /// Updates the grid rows.
        /// </summary>
        private void UpdateGridRowsToReadOnly()
        {
            this.readonlyRowIndex = new List<int>();
            int readonlyRowIndexID = 0;
            int selectedRowIndex = 0;
            int rowIndex = 0;
            for (int count = 0; count < this.selectedReceiptIdsIndex.Count; count++)
            {
                ////Coding modified for 4515 on 27/2/2009 by Malliga
                ////for (rowIndex = 0; rowIndex < this.BatchPaymentItemsGridView.Rows.Count; rowIndex++)
                for (rowIndex = 0; rowIndex < this.BatchPaymentItemsGridView.Rows.Count; rowIndex++)
                {
                    if (!string.IsNullOrEmpty(this.BatchPaymentItemsGridView.Rows[rowIndex].Cells["ReceiptID"].Value.ToString()))
                    {
                        int.TryParse(this.BatchPaymentItemsGridView.Rows[rowIndex].Cells["ReceiptID"].Value.ToString(), out readonlyRowIndexID);

                        if (readonlyRowIndexID == this.selectedReceiptIdsIndex[count])
                        {
                            this.readonlyRowIndex.Add(rowIndex);
                        }
                    }
                }

                string s = "tertery";
            }

            for (int count = 0; count < this.readonlyRowIndex.Count; count++)
            {
                this.BatchPaymentItemsGridView.Rows[this.readonlyRowIndex[count]].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(159, 191, 147);
                this.BatchPaymentItemsGridView[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName, this.readonlyRowIndex[count]].ReadOnly = true;
                this.BatchPaymentItemsGridView[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName, this.readonlyRowIndex[count]].Value = false;
            }
        }

        /// <summary>
        /// Gets the index of the selected receipt ids.
        /// </summary>
        private void GetSelectedReceiptIdsIndex()
        {
            for (int rowCount = 0; rowCount < this.selectedReceiptIds.Count; rowCount++)
            {
                this.selectedReceiptIdsIndex.Add(this.selectedReceiptIds[rowCount]);

                ////this.selectedReceiptIdsIndex.Add(this.RetrieveRecordIndex(this.selectedReceiptIds[rowCount]));
            }
        }

        /// <summary>
        /// Retrieves the index of the record.
        /// </summary>
        /// <param name="tempReceiptId">The temp receipt id.</param>
        /// <returns>record index for the receiptId.</returns>
        private int RetrieveRecordIndex(int tempReceiptId)
        {
            int tempIndex = -1;
            try
            {
                DataTable tempDataTable1 = this.batchPaymentsDataSet.ListUnpaidReceipts.Copy();
                string findExp = tempReceiptId.ToString();
                tempDataTable1.DefaultView.RowFilter = string.Concat(this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptIDColumn.ColumnName, " = ", "'" + findExp + "'");
                if (tempDataTable1.DefaultView.Count > 0)
                {
                    tempIndex = tempDataTable1.Rows.IndexOf(tempDataTable1.DefaultView[0].Row);
                }

                return tempIndex;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                return tempIndex;
            }
        }

          /// <summary>
        /// Sets the seleted date.
        /// </summary>
        /// <param name="selectedDate">The selected date.</param>
        private void SetSeletedDate(DateTime selectedDate)
        {
            if (String.Compare(this.ReceiptMonthCalender.Tag.ToString(), this.ReceiptDateTextBox.Name, true) == 0)
            {
                ////assign date to the BeginDate and textbox
                this.ReceiptDateTextBox.Text = selectedDate.ToString(this.dateFormat);
                this.ReceiptDateTextBox.Focus();  
                this.ReceiptMonthCalender.Visible = false;
            }
        }
        /// <summary>
        /// Calculatings the balance.
        /// </summary>       
        private void CalculateBalance()
        {
            ////calculating total balance
            decimal receiptBalance = this.SelectedReceiptsBalaceTextBox.DecimalTextBoxValue - this.PaymentsTotalTextBox.DecimalTextBoxValue;
            this.BalanceAmountTextBox.Text = receiptBalance.ToString();
            this.PaymentEngineUserControl.TotalReceiptAmount = this.SelectedReceiptsBalaceTextBox.DecimalTextBoxValue;
            this.PaymentEngineUserControl.BalanceAmount = this.BalanceAmountTextBox.DecimalTextBoxValue;
            ////changing colors of the paid and notposted flags, depending on balance
            if (receiptBalance == 0)
            {
                this.BalanceAmountTextBox.BackColor = Color.FromArgb(130, 189, 140);
                this.BalanceAmountTextBox.ForeColor = Color.Black;
            }
            else
            {
                this.BalanceAmountTextBox.BackColor = Color.FromArgb(128, 0, 0);
                this.BalanceAmountTextBox.ForeColor = Color.White;
            }

            if (receiptBalance.Equals(0) && !this.SelectedReceiptsBalaceTextBox.DecimalTextBoxValue.Equals(0))
            {
                this.ApplyPmtsButton.Enabled = true && this.PermissionFiled.newPermission;
            }
            else
            {
                this.ApplyPmtsButton.Enabled = false;
                if (this.SelectedReceiptsBalaceTextBox.DecimalTextBoxValue.Equals(0) && this.PaymentsTotalTextBox.DecimalTextBoxValue.Equals(0)&& this.selectedReceiptIds!=null )
                {
                    if (selectedReceiptIds.Count >= 2 && !string.IsNullOrEmpty(paymentValue) && (paymentValue=="0.00"))
                    {
                        this.ApplyPmtsButton.Enabled = true && this.PermissionFiled.newPermission;
                    }
                }
            }
        }



        private void ApplyButton()
        {
            this.GetSelectedReceiptIdsIndex();
            this.ppaymentId = this.PaymentEngineUserControl.CreatePayment();
            int saveBatch = -1;
            string receiptDate;
            this.GetSelectedReceiptIdsXml();
            string suspendedPaymentXmlString = this.PaymentEngineUserControl.GetSuspendedPayments();
            if (string.IsNullOrEmpty(suspendedPaymentXmlString))
            {
                suspendedPaymentXmlString = "<Root></Root>";
            }
            if (this.ReceiptCheckBox.Checked)
            {
                receiptDate = this.ReceiptDateTextBox.Text;
            }
            else
            {
                receiptDate =null;          
            }
         
            if (this.ppaymentId > 0)
            {
                saveBatch = this.form1013Controll.WorkItem.F1013_SaveBatchPayment(this.ppaymentId, TerraScanCommon.UserId, suspendedPaymentXmlString, this.selectedReceiptIdsXml, receiptDate);
            }

            if (saveBatch >= 0)
            {
                this.isapplyPrintssaved = true;
                this.PaymentEngineUserControl.LoadPayment();
                this.UpdateGridRowsToReadOnly();
                this.SelectedReceiptsBalaceTextBox.Text = string.Empty;
                this.PaymentsTotalTextBox.Text = string.Empty;
                this.BalanceAmountTextBox.Text = string.Empty;
                this.BalanceAmountTextBox.BackColor = System.Drawing.Color.FromArgb(130, 189, 140);
                this.ApplyPmtsButton.Enabled = false;
                this.ImportSuspendedPmtsButton.Enabled = this.IsAppliedAllPayments();
            }
            else
            {
                this.selectedReceiptIdsIndex.Clear();
            }

            ////TSCO# 5749 - 1013 Unpaid Receipts - Add AutoPrint to this form
            CommentsData unpaidReceiptCommentsData = new CommentsData();
            unpaidReceiptCommentsData = this.form1013Controll.WorkItem.GetConfigDetails("TR_UnpaidReceiptReport");
            this.AutoPrintOnButton.EnableAutoPrint = Convert.ToBoolean(this.form1013Controll.WorkItem.GetAutoPrintStatus(this.ParentFormId, TerraScanCommon.UserId));
            if (unpaidReceiptCommentsData.GetCommentsConfigDetails.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(unpaidReceiptCommentsData.GetCommentsConfigDetails[0][unpaidReceiptCommentsData.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString()))
                {
                    int.TryParse(unpaidReceiptCommentsData.GetCommentsConfigDetails[0][unpaidReceiptCommentsData.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString(), out this.reportNumber);
                }
            }

            if (this.reportNumber > 0 && this.AutoPrintOnButton.EnableAutoPrint)
            {
                this.getAutoPrintOnValue = this.form1013Controll.WorkItem.GetConfigDetails("TR_AutoprintOn");

                if (this.getAutoPrintOnValue.GetCommentsConfigDetails.Rows.Count > 0)
                {
                    this.autoPrintOnOff = bool.Parse(this.getAutoPrintOnValue.GetCommentsConfigDetails[0][this.getAutoPrintOnValue.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString());
                }

                Hashtable reportOptionalParameter = new Hashtable();
                reportOptionalParameter.Add("PaymentID", this.ppaymentId);

                if (this.autoPrintOnOff)
                {
                    try
                    {
                        TerraScanCommon.ShowReport(this.reportNumber, Report.ReportType.PrintDefault, reportOptionalParameter);
                    }
                    catch (Exception ex)
                    {
                        
                        
                    }
                }
                else
                {
                    TerraScanCommon.ShowReport(this.reportNumber, Report.ReportType.Preview, reportOptionalParameter);
                }
            }
        }

        /// <summary>
        /// Determines whether [is applied all payments].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is applied all payments]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsAppliedAllPayments()
        {
            bool flagApplied = true;
            try
            {
                if (this.selectedReceiptIdsIndex.Count.Equals(this.BatchPaymentItemsGridView.OriginalRowCount))
                {
                    flagApplied = false;
                }

                return flagApplied;
            }
            catch
            {
                return flagApplied;
            }
        }

        /// <summary>
        /// When the ReceiptCreatedBYComboBox selection is changed
        /// </summary>
        private void ReceiptCreatedBYComboBoxSelectionChanged()
        {
            int outUserId;
            int.TryParse(this.ReceiptCreatedByComboBox.SelectedValue.ToString(), out outUserId);
            this.selectedReceiptIdsIndex.Clear();
            if (outUserId > 0)
            {
                this.userId = outUserId;
            }
            else
            {
                this.userId = null;
            }
        }



        /// <summary>
        /// Shows the Receipt date calender.
        /// </summary>
        private void ShowReceiptDateCalender()
        {
              
            this.ReceiptMonthCalender.Visible = true;
            // Set the calendar to move one month at a time when navigating using the arrows.
            this.ReceiptMonthCalender.ScrollChange = 1;

            // Set the calendar location.
            this.ReceiptMonthCalender.Left = this.ReceiptDatePanel.Left + this.ReceiptDateCalendarButton.Left + this.ReceiptDateCalendarButton.Width;
            this.ReceiptMonthCalender.Top = this.ReceiptDateCalendarButton.Top + this.ReceiptDatePanel.Top;
            this.ReceiptMonthCalender.Tag = this.ReceiptDateCalendarButton.Tag;
            this.ReceiptMonthCalender.Focus();
            if (!string.IsNullOrEmpty(this.ReceiptDateTextBox.Text))
            {
                this.ReceiptMonthCalender.SetDate(this.ReceiptDateTextBox.DateTextBoxValue);
            }
            //this.ReceiptMonthCalender.Left = this.ReceiptDatePanel.Left + this.ReceiptDateCalenderButton.Left + this.ReceiptDateCalenderButton.Width;
            //this.DateMonthCalender.Right = this.BeginDatepanel.Right; // +this.ReceiptDateCalenderButton.Right + this.ReceiptDateCalenderButton.Width;
            //this.DateMonthCalender.Top = this.BeginDateCalenderButton.Bottom;//this.ReceiptDatePanel.Top + this.ReceiptDateCalenderButton.Top;
            //this.DateMonthCalender.Focus();

        }


        /// <summary>
        /// Sets the color of the payment record.
        /// </summary>
        private void SetPaymentRecordColor()
        {
            if (this.selectedReceiptIdsIndex != null)
            {
                this.readonlyRowIndex = new List<int>();
                int readonlyRowIndexID = 0;
                int rowIndex = 0;
                for (int count = 0; count < this.selectedReceiptIdsIndex.Count; count++)
                {
                    for (rowIndex = 0; rowIndex < this.BatchPaymentItemsGridView.Rows.Count; rowIndex++)
                    {
                        if (!string.IsNullOrEmpty(this.BatchPaymentItemsGridView.Rows[rowIndex].Cells["ReceiptID"].Value.ToString()))
                        {
                            int.TryParse(this.BatchPaymentItemsGridView.Rows[rowIndex].Cells["ReceiptID"].Value.ToString(), out readonlyRowIndexID);

                            if (readonlyRowIndexID == this.selectedReceiptIdsIndex[count])
                            {
                                this.readonlyRowIndex.Add(rowIndex);
                            }
                        }
                    }
                }

                for (int count = 0; count < this.readonlyRowIndex.Count; count++)
                {
                    this.BatchPaymentItemsGridView.Rows[this.readonlyRowIndex[count]].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(159, 191, 147);
                    this.BatchPaymentItemsGridView[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName, this.readonlyRowIndex[count]].ReadOnly = true;
                    this.BatchPaymentItemsGridView[this.batchPaymentsDataSet.ListUnpaidReceipts.ReceiptSelectedColumn.ColumnName, this.readonlyRowIndex[count]].Value = false;
                }
            }
        }

        #endregion        

        private void ReceiptDateCalendarButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ReceiptDateCalendarButton.Focus();  
                this.ShowReceiptDateCalender();
                this.ReceiptDateTextBox.SetFocusColor = Color.FromArgb(255, 255, 121);    
                
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

        private void ReceiptMonthCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.SetSeletedDate(e.Start);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void ReceiptCheckBox_Click(object sender, EventArgs e)
        {
            if (ReceiptCheckBox.Checked.Equals(true))
            {
                this.ReceiptDateTextBox.Enabled = true;
                this.ReceiptDateCalendarButton.Enabled = true;
                this.ReceiptDateTextBox.Focus();  
                    
            }
            else
            {
                this.ReceiptDateTextBox.Enabled = false;
                this.ReceiptDateCalendarButton.Enabled = false;
                

            }
        }

        private void BatchPaymentItemsGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void ReceiptDatePanel_Leave(object sender, EventArgs e)
        {
            if (this.ReceiptCheckBox.Checked)
            {
                
            }
        }

        private void CommentAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                object[] optionalParameter;
                this.additionalOperationSmartPart.KeyId = this.currentRowReceiptId;
                if (Convert.ToBoolean(TerraScanCommon.GetFormInfo(1010).openPermission))
                {
                    optionalParameter = new object[] { 99999, 0, 99999 };

                    Form commentForm = new Form();
                    commentForm = this.form1013Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9075, optionalParameter, this.additionalOperationSmartPart.ParentWorkItem);
                    commentForm.Tag = this.additionalOperationSmartPart.CurrntFormId; //9999;

                    if (commentForm != null)
                    {
                        commentForm.ShowDialog();

                        // Code Need to be Modified to Set the Text For Attachmnent/Comment Button (Get the Count,Flag From Attachment/Comment Form Makin Public Propertis.
                        ////AdditionalOperationCountEntity additionalOperationCountEnt;
                        ////this.additionalOperationCountEnt = new AdditionalOperationCountEntity(-999, -999, false);
                        this.additionalOperationCountEnt.CommentCount = Convert.ToInt32(TerraScanCommon.GetValue(commentForm, "CommentCount"));
                        this.additionalOperationCountEnt.HighPriority = Convert.ToBoolean(TerraScanCommon.GetValue(commentForm, "HighPriorityFlag"));
                        this.SetText(this.additionalOperationCountEnt);
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("OpenPermission"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <param name="additionalOperationCountEntity">The additional operation count entity.</param>
        private void SetText(AdditionalOperationCountEntity additionalOperationCountEntity)
        {

            if (additionalOperationCountEntity.CommentCount != -999)
            {
                if (additionalOperationCountEntity.CommentCount <= 0)
                {
                    this.CommentAllButton.Text = "Comment All";
                }
                else
                {
                    this.CommentAllButton.Text = "Comment All" + "(" + additionalOperationCountEntity.CommentCount + ")";
                }

                if (additionalOperationCountEntity.HighPriority)
                {
                    this.CommentAllButton.BackColor = this.highPriorityCommentColor;
                    this.CommentAllButton.CommentPriority = true;
                }
                else
                {
                    this.CommentAllButton.BackColor = this.defaultCommentButtonBackColor;
                    this.CommentAllButton.CommentPriority = false;
                }
            }
        }

              
    }
}
