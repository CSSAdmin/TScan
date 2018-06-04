//--------------------------------------------------------------------------------------------
// <copyright file="F15011.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15011.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 24 JAN 07        Ranjani             Created
// 17 Mar 09        Ramya.D             Modified for BugID 3879,805
// 12 AUG 2011      Manoj Kumar         Removed the tender type default check. TSCO: #13410.
//*********************************************************************************/
namespace D1100
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Infrastructure.Interface;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.SmartParts;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;

    /// <summary>
    /// Form F15011
    /// </summary>
    [SmartPart]
    public partial class F15011 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// form15011Control Controller
        /// </summary>
        private F15011Controller form15011Control;

        /// <summary>
        /// Unique keyId from the Form Master - statement id
        /// </summary>
        private int keyId;

        /// <summary>
        /// Form Number of the masterFormNo
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// enum variable used to find PageMode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// assigining receipt id - default value null
        /// </summary>
        private int? currentReceiptId = null;

        /// <summary>
        /// assigining report Number - default value 0
        /// </summary>
        private int reportNumber;

        /// <summary>
        /// receiptengine dataset
        /// </summary>
        private F11011ExciseStatementData exciseStatement = new F11011ExciseStatementData();

        /// <summary>
        /// operationSmartPart
        /// </summary>
        private OperationSmartPart operationSmartPart = new OperationSmartPart();

        /// <summary>
        /// additionalOperationSmartPart
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart = new AdditionalOperationSmartPart();

        /// <summary>
        /// Used to store the batchButtonSmartPart
        /// </summary>
        private BatchButtonSmartPart batchButtonSmartPart = new BatchButtonSmartPart();

        /// <summary>
        /// Declaring the receiptDate
        /// </summary>
        private DateTime receiptDate;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15011"/> class.
        /// </summary>
        public F15011()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15011"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F15011(int masterform, int formNo, int keyId, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.keyId = keyId;
            this.ParentFormId = formNo;
            ////statement section with brown color
            this.StatementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StatementPanel.Height, this.StatementPictureBox.Width, "Statement", 174, 150, 94);
            ////receipt section with blue color
            this.ReceiptPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReceiptPanel.Height, this.ReceiptPictureBox.Width, "Receipt", 28, 81, 128);
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Set Cancel Button
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_SetCancelButton, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<Button>> SetCancelButton;

        /// <summary>
        /// Get Cancel Button
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> GetCancelButton;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Declare the event FormSlice_CancelEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_CancelEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_CancelEnabled;

        ///<summary>
        ///Declare the event D11011_F15011_ReceiptId
        /// </summary>
        [EventPublication(EventTopicNames.D11011_F15011_ReceiptId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int []>> D11011_F15011_ReceiptId;


        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F15011 control.
        /// </summary>
        /// <value>The F15011 control.</value>
        [CreateNew]
        public F15011Controller Form15011Control
        {
            get { return this.form15011Control as F15011Controller; }
            set { this.form15011Control = value; }
        }

        /// <summary>
        /// Gets or sets the current excise tax receipt id.
        /// </summary>
        /// <value>The current excise tax receipt id.</value>
        public int? CurrentExciseReceiptId
        {
            get
            {
                return this.currentReceiptId;
            }

            set
            {
                this.currentReceiptId = value;
                ////sets additionalOperationSmartPart keyid - required for attachment and comment
                this.additionalOperationSmartPart.KeyId = this.currentReceiptId ?? -1;
            }
        }

        #endregion

        #region Event Subscription

        /// <summary>
        /// Called when [D9030_ F9030_ set slice permission].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                    // check wether the form is populated with records
                    // based on thekeyid
                    if (this.exciseStatement.GetExciseStatement.Rows.Count > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
                    }
                    else
                    {
                        //// Coding Added for the issue 4212 0n 30/5/2009.
                        //// Last Slice does not have a record also it will not return invalid slice
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            eventArgs.Data.FlagInvalidSliceKey = true;
                        }
                    }

                    if (this.exciseStatement.GetExciseStatement.Rows.Count > 0)
                    {
                        this.ReceiptNumberLinkLabel.Focus();
                    }
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
        /// Loads the slice details.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> e)
        {
            try
            {
                if (e.Data.MasterFormNo == this.masterFormNo)
                {
                    this.keyId = e.Data.SelectedKeyId;
                    ////populate excise statement details with the new key id           
                    this.GetStatementDetails();
                  

                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Operations the button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_OperationButtonClick, Thread = ThreadOption.UserInterface)]
        public void OperationButtonClick(object sender, DataEventArgs<string> e)
        {
            switch (e.Data)
            {
                case "NEW":
                    this.NewButton_Click();
                    break;
                case "SAVE":
                    this.SaveButton_Click();
                    break;
                case "CANCEL":
                    this.CancelButton_Click();
                    break;
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ alert distinguished slice on close].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_AlertDistinguishedSliceOnClose, ThreadOption.UserInterface)]
        public void OnD9030_F9030_AlertDistinguishedSliceOnClose(object sender, TerraScan.Infrastructure.Interface.EventArgs<AlertSliceOnClose> eventArgs)
        {
            if (eventArgs.Data.FormNo == this.masterFormNo && eventArgs.Data.FlagFormClose)
            {
                eventArgs.Data.FlagFormClose = this.CheckPageStatus(!eventArgs.Data.FlagForQueryEngine);
            }
        }

        /// <summary>
        /// Loads the cancel button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_LoadCancelButton, Thread = ThreadOption.UserInterface)]
        public void LoadCancelButton(object sender, DataEventArgs<Button> e)
        {
            this.SetCancelButton(this, new DataEventArgs<Button>(e.Data));
        }

        /// <summary>
        /// Sets the form cancel button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_ShellForm_SetFormCancelButton, Thread = ThreadOption.UserInterface)]
        public void SetFormCancelButton(object sender, DataEventArgs<string> e)
        {
            this.GetCancelButton(this, new DataEventArgs<string>(string.Empty));
        }

        #endregion

        //// Shortcut key for cancel(Esc) Button --- BugID 805 --- Ramya.D
        #region Prortected Method
        /// <summary>
        /// Shortcut key for cancel
        /// </summary>
        /// <param name="msg">msg</param>
        /// <param name="keyData">keyData</param>
        /// <returns>bool</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;
            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                switch (keyData)
                {
                    case Keys.Escape:

                        this.CancelButton_Click();
                        break;
                    default:
                        break;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion Prortected Method

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F15011 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F15011_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.LoadWorkSpaces();
                ////Customize Excise ReceiptGridView
                this.CustomizeExciseReceiptGridView();
                ////subcribe PaymentAmountChangeEvent
                this.PaymentEngineUserControl.IsAutoPayment = true;
                this.PaymentEngineUserControl.ParentWorkItem = this.form15011Control.WorkItem;      
                this.PaymentEngineUserControl.PaymentAmountChangeEvent += new TerraScan.PaymentEngine.PaymentEngineUserControl.PaymentAmountChangeEventHandler(this.PaymentEngineUserControl_PaymentAmountChangeEvent);
                this.PaymentEngineUserControl.PaymentItemsGridEditingControlShowing += new TerraScan.PaymentEngine.PaymentEngineUserControl.PaymenItemControlShowingEventHandler(this.PaymentEngineUserControl_PaymentItemsGridEditingControlShowing);
                ////getStatement function is used to load the Statement and receipt details
                this.GetStatementDetails();
                ////check for total due amount and receipt creation
                if (this.TotalDueTextBox.DecimalTextBoxValue <= 0 || this.currentReceiptId.HasValue)
                {
                    this.DisableAllControls = true;
                }

                this.PaymentEngineUserControl.TotalDue = this.TotalDueTextBox.DecimalTextBoxValue;
                if (this.exciseStatement.GetExciseStatement.Rows.Count == 0)
                {
                    this.PaidButton.StatusIndicator = true;
                    this.NotPostedButton.StatusIndicator = true;
                    this.PaidButton.StatusOffColor = Color.FromArgb(150, 150, 150);
                    this.NotPostedButton.StatusOffColor = Color.FromArgb(150, 150, 150);
                    this.PaidButton.StatusIndicator = false;
                    this.NotPostedButton.StatusIndicator = false;
                }
                else
                {
                    this.PaidButton.StatusOffColor = Color.FromArgb(128, 0, 0);
                    this.NotPostedButton.StatusOffColor = Color.FromArgb(128, 0, 0);
                }

                ////to set the Save button text based on paid /Unpaid conditions
                if (this.PaidButton.StatusIndicator)
                {
                    this.operationSmartPart.SaveButtonText = SharedFunctions.GetResourceString("SavePaid");
                }
                else
                {
                    this.operationSmartPart.SaveButtonText = SharedFunctions.GetResourceString("SaveUNPaid");
                }
                ////this.ReceiptNumberTextBox.Enabled = false;

                //// To set focus on ReceiptLinklabel --- BugID 3879 --- Ramya.D
                this.ReceiptNumberLinkLabel.Focus();
                this.ReceiptNumberLinkLabel.Select();
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the PaymentItemsGridEditingControlShowing event of the PaymentEngineUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void PaymentEngineUserControl_PaymentItemsGridEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(this.F15011_SelectionChangeCommitted);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the F15011 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F15011_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ////this.PaymentEngineUserControl.BalanceAmount = this.BalanceAmountTextBox.DecimalTextBoxValue;
        }

        /// <summary>
        /// Method to Load All SmartParts in UI WorkSpaces
        /// </summary>
        private void LoadWorkSpaces()
        {
            ////To Load OperationSmartPart into OperationSmartPartWorkSpace
            if (this.form15011Control.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
            {
                this.operationSmartPart = (OperationSmartPart)this.form15011Control.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart);
            }
            else
            {
                this.operationSmartPart = this.form15011Control.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart);
            }

            this.OperationSmartPartWorkSpace.Show(this.operationSmartPart);

            // To Load AdditionalOperationSmartPart into AddtionalOperationDeckWorkspace
            if (this.form15011Control.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.form15011Control.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart);
            }
            else
            {
                this.additionalOperationSmartPart = this.form15011Control.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart);
            }

            this.AdditionalOperationWorkspace.Show(this.additionalOperationSmartPart);

            ////set required variable - attachment and comment
            this.additionalOperationSmartPart.ParentWorkItem = this.form15011Control.WorkItem;
            this.additionalOperationSmartPart.ParentFormId = this.ParentFormId;
            this.additionalOperationSmartPart.CurrntFormId = this.ParentFormId;
            this.additionalOperationSmartPart.VerticalButtons = true;
            this.additionalOperationSmartPart.BackColor = this.ReceiptPanel.BackColor;
            this.additionalOperationSmartPart.CommentButtonType = TerraScanButton.ButtonType.CommandButton;
            this.additionalOperationSmartPart.AttachmentButtonType = TerraScanButton.ButtonType.CommandButton;
            ////set visible property to the controls
            this.operationSmartPart.DeleteButtonVisible = false;
            ////this.operationSmartPart.SaveButtonText = SharedFunctions.GetResourceString("Save/BatchName");

            // To Load BatchButtonSmartPart into BatchButtonSmartPart
            if (this.form15011Control.WorkItem.SmartParts.Contains(SmartPartNames.BatchButtonSmartPart))
            {
                this.batchButtonSmartPart = (BatchButtonSmartPart)this.form15011Control.WorkItem.SmartParts.Get(SmartPartNames.BatchButtonSmartPart);
            }
            else
            {
                this.batchButtonSmartPart = this.form15011Control.WorkItem.SmartParts.AddNew<BatchButtonSmartPart>(SmartPartNames.BatchButtonSmartPart);
            }

            this.batchButtonSmartPart.ParentWorkItem = this.form15011Control.WorkItem;
            this.batchButtonSmartPart.FormSliceFormNo = this.ParentFormId;
            this.batchButtonSmartPart.CurrentBatchButtonUserId = TerraScanCommon.UserId;
            this.batchButtonSmartPart.CurrentParentFormPermission = true;
            this.batchButtonSmartPart.CurrentFormSlicePermission = this.PermissionFiled;
            this.batchButtonSmartPart.CurrentParentFormBatchButtonStatusMode = BatchButtonSmartPart.BatchButtonStatusMode.Load;
            this.BatchButtonDecWorkspace.Show(this.batchButtonSmartPart);
        }

        #endregion

        #region Private Methods

        #region StatementDetails

        /// <summary>
        /// Gets the excise statement details and fill excise statement summary accordingly.
        /// </summary>
        private void GetStatementDetails()
        {
            this.exciseStatement.Clear();
            this.exciseStatement = this.form15011Control.WorkItem.F15011_GetExciseStatement(this.keyId);
            ////change pagemode
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            ////this.FormSlice_CancelEnabled(this, new DataEventArgs<int>(this.masterFormNo));

            if (this.exciseStatement.GetExciseStatement.Rows.Count > 0)
            {
                ////enable statement panel
                this.StatementPanel.Enabled = true;
                ////enable receipt panel
                this.ReceiptPanel.Enabled = true;
                ////set autoprint status
                this.AutoPrintOnButton.EnableAutoPrint = Convert.ToBoolean(this.form15011Control.WorkItem.GetAutoPrintStatus(this.ParentFormId, TerraScanCommon.UserId));
                ////get statement header details
                this.StatementIDTextBox.Text = this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.StatementIDColumn].ToString();
                this.StatementNumbersTextBox.Text = this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.StatementNumberColumn].ToString();
                this.ParcelNumberTextBox.Text = this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.ParcelNumberColumn].ToString();
                this.SaleDateTextBox.Text = this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.SaleDateColumn].ToString();
                this.PaymentDateTextBox.Text = this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.PaymentDateColumn].ToString();
                this.FormDateTextBox.Text = this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.FormDateColumn].ToString();
                this.MobileHomeTextBox.Text = this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.MobileHomeColumn].ToString();
                this.SaleAmountTextBox.Text = this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.TaxableSalePriceColumn].ToString();
                this.TaxCodeTextBox.Text = this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.TaxCodeColumn].ToString();
                ////receipt
                if (!string.IsNullOrEmpty(this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.ReceiptIDColumn].ToString()))
                {
                    this.ReceiptNumberLinkLabel.Text = this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.ReceiptNumberColumn].ToString();
                }
                else
                {
                    this.ReceiptNumberLinkLabel.Text = string.Empty;
                }

                this.ReceiptByTextBox.Text = this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.ReceiptByColumn].ToString();

                ////district
                if (!string.IsNullOrEmpty(this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.DistrictColumn].ToString()))
                {
                    this.DistrictLinkLabel.Text = this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.DistrictColumn].ToString();
                }
                else
                {
                    this.DistrictLinkLabel.Text = string.Empty;
                }

                ////grantee
                if (!string.IsNullOrEmpty(this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.GranteeOwnerIDColumn].ToString()))
                {
                    this.GranteeLinkLabel.Text = this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.GranteeColumn].ToString();
                }
                else
                {
                    this.GranteeLinkLabel.Text = string.Empty;
                    this.PaymentEngineUserControl.OwnerName = string.Empty;
                }

                ////grantor
                if (!string.IsNullOrEmpty(this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.GrantorOwnerIDColumn].ToString()))
                {
                    this.GrantorLinkLabel.Text = this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.GrantorColumn].ToString();
                    this.PaymentEngineUserControl.OwnerName = this.GrantorLinkLabel.Text;
                }
                else
                {
                    this.GrantorLinkLabel.Text = string.Empty;
                    this.PaymentEngineUserControl.OwnerName = string.Empty;
                }

                ////by default ppaymentid is zero
                int ppaymentId = int.Parse(this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.PPaymentIDColumn].ToString());
                ////get receipt id
                if (String.IsNullOrEmpty(this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.ReceiptIDColumn].ToString()))
                {
                    this.CurrentExciseReceiptId = null;
                }
                else
                {
                    this.CurrentExciseReceiptId = int.Parse(this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.ReceiptIDColumn].ToString());
                }
                ////refresh tax receipt
                if (this.exciseStatement.GetExciseReceipt.Rows.Count == 0)
                {
                    this.exciseStatement.InitializeExciseReceipt();
                }
                ////load receipt
                this.ExciseReceiptGridView.DataSource = this.exciseStatement.GetExciseReceipt.DefaultView;
                ////get receipt report                    
                if (string.IsNullOrEmpty(this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.ReceiptReportColumn].ToString()))
                {
                    this.reportNumber = 0;
                }
                else
                {
                    this.reportNumber = int.Parse(this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.ReceiptReportColumn].ToString());
                }
                ////edit enable/disable depends on the submit to dor of affidavit statement
                this.EditlinkLabel.Enabled = Convert.ToBoolean(this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.IsEditableColumn]);
                ////calculate total due
                this.CalculateTotalDue();
                ////load payment
                this.PaymentEngineUserControl.LoadPayment(ppaymentId);
                ////sets property with pagemode
                this.SetPageModeProperty();
                ////reset PersistDefaultColor
                this.StatusTotalTextBox.PersistDefaultColor = true;
                this.LocalTotalTextBox.PersistDefaultColor = true;
                this.FeeTatalTextBox.PersistDefaultColor = true;
            }
            else
            {
                this.ClearExciseStatement();
            }
        }

        /// <summary>
        /// Clears the Excise Statement Summary
        /// </summary>
        private void ClearExciseStatement()
        {
            ////clear statement header
            this.StatementIDTextBox.Text = string.Empty;
            this.StatementNumbersTextBox.Text = string.Empty;
            this.ParcelNumberTextBox.Text = string.Empty;
            this.SaleDateTextBox.Text = string.Empty;
            this.PaymentDateTextBox.Text = string.Empty;
            this.FormDateTextBox.Text = string.Empty;
            this.MobileHomeTextBox.Text = string.Empty;
            this.ReceiptNumberLinkLabel.Text = string.Empty;
            this.ReceiptByTextBox.Text = string.Empty;
            this.DistrictLinkLabel.Text = string.Empty;
            this.SaleAmountTextBox.Text = string.Empty;
            this.TaxCodeTextBox.Text = string.Empty;
            this.GranteeLinkLabel.Text = string.Empty;
            this.GrantorLinkLabel.Text = string.Empty;
            this.StatementPanel.Enabled = false;
            ////clear receipt grid
            this.exciseStatement.InitializeExciseReceipt();
            this.ExciseReceiptGridView.DataSource = this.exciseStatement.GetExciseReceipt.DefaultView;
            ////clear payment
            this.PaymentEngineUserControl.LoadPayment();
                       
            ////clear calculation fields
            this.StatusTotalTextBox.Text = string.Empty;
            this.LocalTotalTextBox.Text = string.Empty;
            this.FeeTatalTextBox.Text = string.Empty;
            this.TotalDueTextBox.Text = string.Empty;
            this.BalanceAmountTextBox.Text = string.Empty;
            this.PaymentEngineUserControl.BalanceAmount = 0;
            this.PaymentsTotalTextBox.Text = string.Empty;
            ////set PersistDefaultColor
            this.StatusTotalTextBox.PersistDefaultColor = false;
            this.LocalTotalTextBox.PersistDefaultColor = false;
            this.FeeTatalTextBox.PersistDefaultColor = false;
            ////disable additionla opration smartpart
            this.AdditionalOperationWorkspace.Enabled = false;
            this.SetAdditionalOperationCount(false);
            ////clear audit receipt link
            this.ExciseReceiptAuditLinkLabel.Text = SharedFunctions.GetResourceString("ReceiptAuditLink");
            this.ExciseReceiptAuditLinkLabel.Enabled = false;
            ////reset autoprint
            this.AutoPrintOnButton.EnableAutoPrint = false;
            this.ReceiptPanel.Enabled = false;
        }

        /// <summary>
        /// To set the Status Buttons Property
        /// </summary>
        /// <param name="ppaymentId">ppaymentId</param>
        /// <param name="postId">postId</param>
        private void SetStatusButtonsProperty(int ppaymentId, int postId)
        {
            if (this.exciseStatement.GetExciseStatement.Rows.Count == 0)
            {
                this.PaidButton.StatusOffColor = Color.FromArgb(150, 150, 150);
                this.NotPostedButton.StatusOffColor = Color.FromArgb(150, 150, 150);
            }
            else
            {
                ppaymentId = Convert.ToInt32(this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.PPaymentIDColumn]);
                postId = Convert.ToInt32(this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.PostIDColumn]);
                ////changing colors of the paid and notposted flags, depending on balance
                if (this.currentReceiptId.HasValue)
                {
                    this.PaidButton.StatusOffColor = Color.FromArgb(128, 0, 0);
                    this.NotPostedButton.StatusOffColor = Color.FromArgb(128, 0, 0);
                }
                else
                {
                    this.PaidButton.StatusOffColor = Color.FromArgb(150, 150, 150);
                    this.NotPostedButton.StatusOffColor = Color.FromArgb(150, 150, 150);
                }

                if (ppaymentId != 0)
                {
                    this.PaidButton.Text = "Paid";
                    this.PaidButton.StatusIndicator = true;

                    if (postId != 0)
                    {
                        this.NotPostedButton.Text = "Posted";
                        this.NotPostedButton.StatusIndicator = true;
                    }
                    else
                    {
                        this.NotPostedButton.Text = "Not Posted";
                        this.NotPostedButton.StatusIndicator = false;
                    }
                }
                else
                {
                    this.PaidButton.Text = "Not Paid";
                    this.PaidButton.StatusIndicator = false;
                    this.NotPostedButton.Text = "Not Posted";
                    this.NotPostedButton.StatusIndicator = false;
                }
            }

            ////to set the Save button text based on paid /Unpaid conditions
            if (this.PaidButton.StatusIndicator)
            {
                this.operationSmartPart.SaveButtonText = SharedFunctions.GetResourceString("SavePaid");
            }
            else
            {
                this.operationSmartPart.SaveButtonText = SharedFunctions.GetResourceString("SaveUNPaid");
            }
        }
        #endregion

        #region Payment Engine Events

        /// <summary>
        /// Payments the engine user control_ payment amount change event.
        /// </summary>
        /// <param name="amount">The amount.</param>
        private void PaymentEngineUserControl_PaymentAmountChangeEvent(decimal amount)
        {
            try
            {
                this.PaymentsTotalTextBox.Text = amount.ToString();
                this.CalculateBalance();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region New Receipt

        /// <summary>
        /// Handles the Click event of the NewButton control.
        /// </summary>
        private void NewButton_Click()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////setting the pagemode
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                this.CurrentExciseReceiptId = null;
                ////change controls property
                this.SetPageModeProperty();
                this.Cursor = Cursors.Default;
                ////commented by Biju on 24/May/2010 to fix #7094
                ////if (D9030.F9030.lastreceiptdate != null)
                ////{
                ////    this.PaymentDateTextBox.Text = D9030.F9030.lastreceiptdate;
                ////}

                ////if (D9030.F9030.lastinterestdate != null)
                ////{
                ////    this.FormDateTextBox.Text = D9030.F9030.lastinterestdate;
                ////}
                ////till here
                string ownerName = string.Empty;
                if (this.exciseStatement.GetExciseStatement.Rows.Count > 0)
                {
                    ownerName = this.exciseStatement.GetExciseStatement.Rows[0]["OwnerName"].ToString();
                
                }
                int ownerID;
                int.TryParse(this.exciseStatement.GetExciseStatement.Rows[0]["GrantorOwnerID"].ToString(), out ownerID);
                if (this.PaymentEngineUserControl.PayeeDetails == null)
                {
                    if (ownerID > 0)
                    {
                        this.PaymentEngineUserControl.PayeeDetails = this.form15011Control.WorkItem.F1019_GetPayeeDetails(ownerID);
                    }
                    else
                    {
                        this.PaymentEngineUserControl.PayeeDetails = new PaymentEngineData();
                    }
                }
               this.PaymentEngineUserControl.OwnerName = this.GrantorLinkLabel.Text;
                this.PaymentEngineUserControl.TotalDue = this.TotalDueTextBox.DecimalTextBoxValue;
                //Removed the tender type default check. TSCO: #13410.
               // this.PaymentEngineUserControl.LoadDefultValue(this.GrantorLinkLabel.Text, this.BalanceAmountTextBox.DecimalTextBoxValue);
                this.PaymentEngineUserControl.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Save

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        private void SaveButton_Click()
        {
            try
            {
                this.SaveReceipt(false);
                this.FormSlice_CancelEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Saves the receipt.
        /// </summary>
        /// <param name="onclose">if set to <c>true</c> [on close].</param>
        /// <returns>true - sucessfull save</returns>
        private bool SaveReceipt(bool onclose)
        {
            try
            {
                DateTime outFormDate;
                int ppaymentId = 0;
                ////Check For Required Fields
                if (String.IsNullOrEmpty(this.PaymentDateTextBox.Text.Trim()))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.PaymentDateTextBox.Focus();
                    return false;
                }

                if (String.IsNullOrEmpty(this.FormDateTextBox.Text.Trim()))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.FormDateTextBox.Focus();
                    return false;
                }

                this.Cursor = Cursors.WaitCursor;

                ////get the receiptdate                 
                outFormDate = this.FormDateTextBox.DateTextBoxValue;
                ////checks for valid receipt
                string validResult = this.form15011Control.WorkItem.F1009_GetValidReceiptTest(this.keyId, outFormDate);
                if (!String.IsNullOrEmpty(validResult))
                {
                    ////TODO title hardcode - needs some clarification
                    MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("InvalidReceiptHeader"), validResult), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("InvalidReceipt")), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                ////checks finding save/batch   
                if (!String.IsNullOrEmpty(this.BalanceAmountTextBox.Text))
                {
                    if (this.BalanceAmountTextBox.DecimalTextBoxValue != 0)
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("BatchSave"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", "Unpaid Receipt(s)"), MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        ////create payment
                        ppaymentId = this.PaymentEngineUserControl.CreatePayment();
                    }
                }
                ////save receipt
                if (this.TotalDueTextBox.DecimalTextBoxValue > 0)
                {
                    this.exciseStatement.SaveExciseReceipt.Rows.Clear();
                    F11011ExciseStatementData.SaveExciseReceiptRow dr = this.exciseStatement.SaveExciseReceipt.NewSaveExciseReceiptRow();

                    dr.StatementID = this.keyId;
                    dr.ReceiptDate = this.PaymentDateTextBox.DateTextBoxValue.ToString();
                    dr.InterestDate = outFormDate.ToShortDateString();
                    D9030.F9030.lastreceiptdate = this.PaymentDateTextBox.DateTextBoxValue.ToString();
                    D9030.F9030.lastinterestdate = outFormDate.ToShortDateString();

                    dr.UserID = TerraScanCommon.UserId;
                    dr.PPaymentID = ppaymentId;

                    this.exciseStatement.SaveExciseReceipt.Rows.Add(dr);
                    //// receipt soureceid - 1 for receipt creation
                    int savedReceiptId = this.form15011Control.WorkItem.F1405_SaveMasterReceipt(this.keyId, 1, Utility.GetXmlString(this.exciseStatement.SaveExciseReceipt), null);
                    ////receipt saved
                  
                    if (savedReceiptId > 0)
                    {
                        ////get autoprint status
                        this.AutoPrintOnButton.EnableAutoPrint = Convert.ToBoolean(this.form15011Control.WorkItem.GetAutoPrintStatus(this.ParentFormId, TerraScanCommon.UserId));
                        if (this.AutoPrintOnButton.EnableAutoPrint)
                        {
                            Hashtable reportOptionalParameter = new Hashtable();
                            reportOptionalParameter.Add("ReceiptID", savedReceiptId);
                            ////changed the parameter type from string to int
                            TerraScanCommon.ShowReport(this.reportNumber, Report.ReportType.PrintDefault, reportOptionalParameter);
                        }

                        if (ppaymentId > 0 && this.PaymentEngineUserControl.RefundNow && MessageBox.Show(SharedFunctions.GetResourceString("RefundNow"), SharedFunctions.GetResourceString("RefundTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            ////refund management form
                            FormInfo formInfo = TerraScanCommon.GetFormInfo(1214);
                            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                        }

                        ////saved receiptid is sent to the batch button control(on batch Button run mode, saved recipt id is added in the snapshot collection)
                        ////added by vijayakumar as per client request for Dec 2007
                        this.batchButtonSmartPart.CurrentreceiptId = savedReceiptId;
                    }

                    if (onclose)
                    {
                        this.FormSlice_CancelEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                        return true;
                    }

                    ////populate with the new values        
                    this.GetStatementDetails();
                    if (this.keyId != -1)
                    {
                        int[] tempArgs = new int[2];
                        tempArgs[0] = this.keyId;
                        tempArgs[1] = 15010;                //master form no.
                        this.D11011_F15011_ReceiptId(this, new DataEventArgs<int[]>(tempArgs));
                    }
                }

                return true;
            }
            catch (SoapException ex)
            {
                ////ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return false;
            }
            catch (Exception ex)
            {
                ////ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Cancel

        /// <summary>
        ///  Handles the Click event of the cancel control.
        /// </summary>
        private void CancelButton_Click()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.PaymentEngineUserControl.PayeeDetails = null;
                this.FormSlice_CancelEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                this.GetStatementDetails();
                this.Cursor = Cursors.Default;
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Date Related Calender Controls Events

        /// <summary>
        /// Sets the seleted date.
        /// </summary>
        /// <param name="dateSelected">The date selected.</param>
        private void SetSeletedDate(string dateSelected)
        {
            // Assign the selected date to the DateTextbox.
            if (String.Compare(this.ExciseStatementMonthCalendar.Tag.ToString(), this.PaymentDateTextBox.Name, true) == 0)
            {
                ////this.PaymentDateButton.Focus();
                this.PaymentDateTextBox.Text = dateSelected;
            }
            else if (String.Compare(this.ExciseStatementMonthCalendar.Tag.ToString(), this.FormDateTextBox.Name, true) == 0)
            {
                ////this.FormDateButton.Focus();
                this.FormDateTextBox.Text = dateSelected;
            }

            this.ExciseStatementMonthCalendar.Tag = string.Empty;
            this.ExciseStatementMonthCalendar.Visible = false;
        }

        /// <summary>
        /// Shows the InterestDate calender in particular location.
        /// </summary>
        private void ShowFormDateCalender()
        {
            this.ExciseStatementMonthCalendar.Visible = true;

            // Set the calendar to move one month at a time when navigating using the arrows.
            this.ExciseStatementMonthCalendar.ScrollChange = 1;
            //// Display the Calender control near the Calender Picture box. +2 for alignment            
            ////this.ExciseStatementMonthCalendar.Left = this.StatementPanel.Left + this.FromDatePanel.Left + this.FormDateButton.Right - this.ExciseStatementMonthCalendar.Width + 2;
            ////this.ExciseStatementMonthCalendar.Top = this.StatementPanel.Top + this.FromDatePanel.Top + this.FormDateButton.Bottom;
            this.ExciseStatementMonthCalendar.Tag = this.FormDateTextBox.Name;
            this.ExciseStatementMonthCalendar.Focus();
            if (!string.IsNullOrEmpty(this.FormDateTextBox.Text))
            {
                this.ExciseStatementMonthCalendar.SetDate(Convert.ToDateTime(this.FormDateTextBox.Text));
            }
        }

        /// <summary>
        /// Shows the RecieptDate calender in particular location.
        /// </summary>
        private void ShowPaymentDateCalender()
        {
            this.ExciseStatementMonthCalendar.Visible = true;

            // Set the calendar to move one month at a time when navigating using the arrows.
            this.ExciseStatementMonthCalendar.ScrollChange = 1;
            //// Display the Calender control near the Calender Picture box.
            ////this.ExciseStatementMonthCalendar.Left = this.StatementPanel.Left + this.PaymentDatePanel.Left + this.PaymentDateButton.Left;
            ////this.ExciseStatementMonthCalendar.Top = this.StatementPanel.Top + this.PaymentDatePanel.Top + this.PaymentDateButton.Bottom;
            this.ExciseStatementMonthCalendar.Tag = this.PaymentDateTextBox.Name;
            this.ExciseStatementMonthCalendar.Focus();
            if (!string.IsNullOrEmpty(this.PaymentDateTextBox.Text))
            {
                this.ExciseStatementMonthCalendar.SetDate(Convert.ToDateTime(this.PaymentDateTextBox.Text));
            }
        }

        /// <summary>
        /// Handles the Click event of the PaymentDateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        ////private void PaymentDateButton_Click(object sender, EventArgs e)
        ////{
        ////    // Calls the method to show the calender control.
        ////    this.ShowPaymentDateCalender();
        ////}

        /// <summary>
        /// Handles the Click event of the FormDateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        ////private void FormDateButton_Click(object sender, EventArgs e)
        ////{
        ////    // Calls the method to show the calender control.
        ////    this.ShowFormDateCalender();
        ////}   

        /// <summary>
        /// Handles the DateSelected event of the ExciseTaxStatementMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void ExciseStatementMonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.SetSeletedDate(e.Start.ToShortDateString());
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the ExciseTaxStatementMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ExciseStatementMonthCalendar_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.SetSeletedDate(this.ExciseStatementMonthCalendar.SelectionStart.ToShortDateString());
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region User Defined Methods

        /// <summary>
        /// This Method used to set dataproperty name
        /// CustomizeExciseReceiptGridView
        /// </summary>
        private void CustomizeExciseReceiptGridView()
        {
            DataGridViewColumnCollection columns = this.ExciseReceiptGridView.Columns;

            columns["Item"].DataPropertyName = this.exciseStatement.GetExciseReceipt.ItemColumn.ColumnName;
            columns["StateRate"].DataPropertyName = this.exciseStatement.GetExciseReceipt.StateRateColumn.ColumnName;
            columns["StateAmount"].DataPropertyName = this.exciseStatement.GetExciseReceipt.StateAmountColumn.ColumnName;
            columns["LocalRate"].DataPropertyName = this.exciseStatement.GetExciseReceipt.LocalRateColumn.ColumnName;
            columns["LocalAmount"].DataPropertyName = this.exciseStatement.GetExciseReceipt.LocalAmountColumn.ColumnName;
            columns["FeeType"].DataPropertyName = this.exciseStatement.GetExciseReceipt.FeeTypeColumn.ColumnName;
            columns["FeeAmount"].DataPropertyName = this.exciseStatement.GetExciseReceipt.FeeAmountColumn.ColumnName;

            columns["Item"].DisplayIndex = 0;
            columns["StateRate"].DisplayIndex = 1;
            columns["StateAmount"].DisplayIndex = 2;
            columns["LocalRate"].DisplayIndex = 3;
            columns["LocalAmount"].DisplayIndex = 4;
            columns["FeeType"].DisplayIndex = 5;
            columns["FeeAmount"].DisplayIndex = 6;

            this.exciseStatement.InitializeExciseReceipt();
            this.ExciseReceiptGridView.DataSource = this.exciseStatement.GetExciseReceipt.DefaultView;
        }

        /// <summary>
        /// Check the page Status when the New Reciept is Cancelled
        /// </summary>
        /// <param name="onclose">onclose</param>
        /// <returns>boolean Value</returns>
        private bool CheckPageStatus(bool onclose)
        {
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    return this.SaveReceipt(onclose);
                }
                else if (dialogResult == DialogResult.No)
                {
                    if (!onclose)
                    {
                        this.CancelButton_Click();
                    }

                    this.FormSlice_CancelEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                    return true;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Sets the attachment and comments count.
        /// </summary>
        /// <param name="onload">if set to <c>true</c> [onload].</param>
        private void SetAdditionalOperationCount(bool onload)
        {
            ////Display Comments Count in the Comments Buttons  and attachment count in the attachment Buttons       
            if (this.additionalOperationSmartPart != null)
            {
                AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);
                if (onload && this.currentReceiptId.HasValue)
                {
                    additionalOperationCountEntity.AttachmentCount = this.form15011Control.WorkItem.GetAttachmentCount(this.ParentFormId, this.currentReceiptId.Value, TerraScanCommon.UserId);
                    CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.form15011Control.WorkItem.GetCommentsCount(this.ParentFormId, this.currentReceiptId.Value, TerraScanCommon.UserId);
                    if (commentsCountDataTable.Rows.Count > 0)
                    {
                        additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                        additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                    }
                }

                this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;
            }
        }

        /// <summary>
        /// Sets the page mode property.
        /// </summary>
        private void SetPageModeProperty()
        {
            switch (this.pageMode)
            {
                case TerraScanCommon.PageModeTypes.New:
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
                    ////unlock payment and form date
                    ////enable paymentengine
                    this.PaymentEngineUserControl.TabStop = true;
                    this.PaymentEngineUserControl.Locked = false;
                    this.PaymentEngineUserControl.TotalReceiptAmount = this.TotalDueTextBox.DecimalTextBoxValue;
                    this.PaymentEngineUserControl.SetDefaultSelection = true;
                    ////attachment and comments button 
                    this.AdditionalOperationWorkspace.Enabled = false;
                    ////Audit link
                    this.ExciseReceiptAuditLinkLabel.Text = SharedFunctions.GetResourceString("ReceiptAuditLink");
                    this.ExciseReceiptAuditLinkLabel.Enabled = false;

                    break;
                default:
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    if (this.TotalDueTextBox.DecimalTextBoxValue <= 0 || this.currentReceiptId.HasValue)
                    {
                        this.operationSmartPart.NewButtonEnable = false;
                        this.SetStatusButtonsProperty(0, 0);
                    }
                    else
                    {
                        this.operationSmartPart.NewButtonEnable = this.PermissionFiled.newPermission;
                        this.SetStatusButtonsProperty(Convert.ToInt32(this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.PPaymentIDColumn]), Convert.ToInt32(this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.PostIDColumn]));
                    }
                    ////lock payment and form date
                    this.PaymentDateTextBox.LockKeyPress = true;
                    this.FormDateTextBox.LockKeyPress = true;
                    ////this.PaymentDateButton.Enabled = false;
                    ////this.FormDateButton.Enabled = false; 
                    ////disable paymentengine
                    this.PaymentEngineUserControl.TabStop = false;
                    this.PaymentEngineUserControl.Locked = true;
                    this.PaymentEngineUserControl.SetDefaultSelection = false;
                    ////Audit link     
                    if (this.currentReceiptId.HasValue)
                    {
                        this.ExciseReceiptAuditLinkLabel.Text = string.Concat(SharedFunctions.GetResourceString("ReceiptAuditLink"), this.currentReceiptId);
                        this.ExciseReceiptAuditLinkLabel.Enabled = true;
                        ////attachment and comments button 
                        this.AdditionalOperationWorkspace.Enabled = true;
                    }
                    else
                    {
                        this.ExciseReceiptAuditLinkLabel.Text = SharedFunctions.GetResourceString("ReceiptAuditLink");
                        this.ExciseReceiptAuditLinkLabel.Enabled = false;
                        ////attachment and comments button 
                        this.AdditionalOperationWorkspace.Enabled = false;
                    }

                    break;
            }

            this.SetAdditionalOperationCount(true);
        }

        #region Calculation

        /// <summary>
        /// Calculatings the balance.
        /// </summary>
        private void CalculateBalance()
        {
            decimal balance = 0;
            ////calculating total balance
            balance = this.TotalDueTextBox.DecimalTextBoxValue - this.PaymentsTotalTextBox.DecimalTextBoxValue;
            this.BalanceAmountTextBox.Text = balance.ToString();
            this.PaymentEngineUserControl.BalanceAmount = this.BalanceAmountTextBox.DecimalTextBoxValue;
            ////changing colors of the BalanceAmountTextBox background, depending on balance
            if (balance == 0)
            {
                this.BalanceAmountTextBox.BackColor = Color.FromArgb(130, 189, 140);
                this.BalanceAmountTextBox.ForeColor = Color.Black;
            }
            else
            {
                this.BalanceAmountTextBox.BackColor = Color.FromArgb(128, 0, 0);
                this.BalanceAmountTextBox.ForeColor = Color.White;
            }
        }

        /// <summary>
        /// Calculating the Total Due values.
        /// </summary>
        private void CalculateTotalDue()
        {
            decimal totalDue = 0;
            ////calculating total due
            this.StatusTotalTextBox.Text = this.exciseStatement.GetExciseReceipt.Compute(string.Concat("SUM(", this.exciseStatement.GetExciseReceipt.StateAmountColumn.ColumnName, ")"), "").ToString();
            this.LocalTotalTextBox.Text = this.exciseStatement.GetExciseReceipt.Compute(string.Concat("SUM(", this.exciseStatement.GetExciseReceipt.LocalAmountColumn.ColumnName, ")"), "").ToString();
            this.FeeTatalTextBox.Text = this.exciseStatement.GetExciseReceipt.Compute(string.Concat("SUM(", this.exciseStatement.GetExciseReceipt.FeeAmountColumn.ColumnName, ")"), "").ToString();
            totalDue = this.StatusTotalTextBox.DecimalTextBoxValue + this.LocalTotalTextBox.DecimalTextBoxValue + this.FeeTatalTextBox.DecimalTextBoxValue;
            this.TotalDueTextBox.Text = totalDue.ToString();
        }

        #endregion

        #endregion

        #region Events

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

                this.form15011Control.WorkItem.SaveAutoPrint(this.ParentFormId, TerraScanCommon.UserId, this.AutoPrintOnButton.EnableAutoPrint);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the ExciseReceiptGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void ExciseReceiptGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                Decimal outDecimal;
                //// Only paint if desired, formattable column
                if (e.ColumnIndex == this.ExciseReceiptGridView.Columns["StateRate"].Index || e.ColumnIndex == this.ExciseReceiptGridView.Columns["LocalRate"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }
                    ////Only paint if text provided Only paint if desired text is in cell
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            outDecimal = outDecimal / 100;
                            e.Value = outDecimal.ToString("0.00 %");
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            e.Value = "0.00 %";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }

                if (e.ColumnIndex == this.ExciseReceiptGridView.Columns["StateAmount"].Index || e.ColumnIndex == this.ExciseReceiptGridView.Columns["LocalAmount"].Index || e.ColumnIndex == this.ExciseReceiptGridView.Columns["FeeAmount"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }
                    ////Only paint if text provided, Only paint if desired text is in cell
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            e.Value = outDecimal.ToString("#,##0.00");
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            e.Value = "0.00";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the ExciseReceiptAuditLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ExciseReceiptAuditLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (this.currentReceiptId > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(11001);
                    formInfo.optionalParameters = new object[1];
                    //// formInfo.optionalParameters[0] = this.Tag;
                    formInfo.optionalParameters[0] = this.currentReceiptId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
                ////this.Cursor = Cursors.WaitCursor;
                ////if (this.currentReceiptId.HasValue)
                ////{
                ////    ////// calling  Common Function For Report
                ////    Hashtable reportOptionalParameter = new Hashtable();
                ////    reportOptionalParameter.Add("ReceiptID", this.currentReceiptId);
                ////    TerraScanCommon.ShowReport(90101, Report.ReportType.Preview, reportOptionalParameter);
                ////}
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

        #region Links

        /// <summary>
        /// Handles the LinkClicked event of the ReceiptNumberLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ReceiptNumberLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////get form info of 1001 - receipt form
                FormInfo formInfo = TerraScanCommon.GetFormInfo(11001);
                ////set optionalparameters -- 
                formInfo.optionalParameters = new object[] { this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.ReceiptIDColumn] };
                ////shows form in mdi window
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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
        /// Handles the LinkClicked event of the GrantorLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void GrantorLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////get form info of 9100 - owner form
                FormInfo formInfo = TerraScanCommon.GetFormInfo(91000);
                ////set optionalparameters -- grantor link is displayed only if the records exist in GetExciseStatement table 
                formInfo.optionalParameters = new object[] { this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.GrantorOwnerIDColumn] };
                ////shows form in mdi window
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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
        /// Handles the LinkClicked event of the GranteeLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void GranteeLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////get form info of 9100 - owner form
                FormInfo formInfo = TerraScanCommon.GetFormInfo(91000);
                ////set optionalparameters -- grantee link is displayed only if the records exist in GetExciseStatement table 
                formInfo.optionalParameters = new object[] { this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.GranteeOwnerIDColumn] };
                ////shows form in mdi window
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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
        /// Handles the LinkClicked event of the EditlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void EditlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ////check for valid value
                if (this.keyId > 0)
                {
                    this.Cursor = Cursors.WaitCursor;

                    ////get form info of 11010 - affidavit form
                    FormInfo formInfo = TerraScanCommon.GetFormInfo(11010);
                    ////set optionalparameters
                    if (!string.IsNullOrEmpty(this.ReceiptNumberLinkLabel.Text))
                    {
                        formInfo.optionalParameters = new object[] { this.keyId, 1 };
                    }
                    else
                    {
                        formInfo.optionalParameters = new object[] { this.keyId, 0 };
                    }
                    ////shows form in mdi window
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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
        /// DistrictLinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">ebent</param>
        private void DistrictLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////get form info of 9500 - district form
                FormInfo formInfo = TerraScanCommon.GetFormInfo(11013);
                ////set optionalparameters -- district link is displayed only if the records exist in GetExciseStatement table 
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.ExciseRateIDColumn].ToString();
                ////formInfo.optionalParameters[0] = this.exciseStatement.GetExciseStatement.Rows[0][this.exciseStatement.GetExciseStatement.DistrictIDColumn].ToString();
                ////shows form in mdi window
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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

        #endregion links

        /// <summary>
        /// Payments the engine user control_ editing control showing.
        /// </summary>
        private void PaymentEngineUserControl_EditingControlShowing()
        {
            try
            {
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion

        #endregion
    }
}
