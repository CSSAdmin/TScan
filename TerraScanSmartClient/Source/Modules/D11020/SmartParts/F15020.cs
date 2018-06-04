//--------------------------------------------------------------------------------------------
// <copyright file="F15020.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15020.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 19 Dec 06        Ranjani JG        	    Created// 
// 06 Dec 08        ShanmugaSundaram.A  Modified for Implementing F9025 Validation
// 09 Feb 09        Khaja               Modified to fix the bug #4704
// 25/08/09         Sadha Shivudu      Implemented TSCO # 2803 - Default Interest/Receipt Dates now global
// 12 SEP 2011      Manoj Kumar         Removed the tender Type default 'check' TSCO #13410.
//*********************************************************************************
namespace D11020
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
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
    using TerraScan.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using System.Web.Services.Protocols;
    using System.Globalization;
    using TerraScan.Infrastructure.Interface.Constants;
    using Infrastructure.Interface;

    /// <summary>
    /// Form F15020
    /// </summary>
    [SmartPart]
    public partial class F15020 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// form15020Control Controller
        /// </summary>
        private F15020Controller form15020Control;

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
        /// Declaring the receiptDate
        /// </summary>
        private DateTime receiptDate;

        /// <summary>
        /// receiptId
        /// </summary>
        private int receiptId;

        /// <summary>
        /// assigining receipt id - default value null
        /// </summary>
        private int? currentReceiptId = null;

        /// <summary>
        /// Assiging Empty to ownerName
        /// </summary>
        private string ownerName = String.Empty;

        /// <summary>
        /// Assiging Empty to owner ID
        /// </summary>
        private string ownerId = String.Empty;

        /// <summary>
        /// Used to Store tempInterestDue
        /// </summary>
        private double tempInterestDue;

        /// <summary>
        /// enum variable used to find PageMode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// create variable for paymentoptiontypes
        /// </summary>
        private PaymentOptionTypes paymentOption;

        /// <summary>
        /// checks history grid row validation cancelled or not
        /// </summary>
        private bool processRowEnter = true;

        /// <summary>
        /// Boolean Value
        /// </summary>        
        private bool interestDateChanged;

        /// <summary>
        /// Boolean Value
        /// </summary>        
        private bool receiptDateChanged;

        /// <summary>
        /// pageLoadStatus variable is used to find the pageLoadStatus - default true. 
        /// </summary>   
        private bool pageLoadStatus = true;

        /// <summary>
        /// receiptengine dataset
        /// </summary>
        private F15020ReceiptEngineData receiptEngine = new F15020ReceiptEngineData();

        /// <summary>
        /// operationSmartPart
        /// </summary>
        private OperationSmartPart operationSmartPart = new OperationSmartPart();

        /// <summary>
        /// Used to store the batchButtonSmartPart
        /// </summary>
        private BatchButtonSmartPart batchButtonSmartPart = new BatchButtonSmartPart();

        /// <summary>
        /// additionalOperationSmartPart
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart = new AdditionalOperationSmartPart();

        /// <summary>
        /// Contain OverUnderMaxAmount from configruation
        /// </summary>
        private decimal overUnderMaxAmount;

        /// <summary>
        /// Contain OverUnderMinAmount from configruation
        /// </summary>
        private decimal overUnderMinAmount;

        /// <summary>
        /// Declaring the statementFees
        /// </summary>
        private decimal statementFees;

        /// <summary>
        /// assigining report Number - default value 0
        /// </summary>
        private int reportNumber = 0;

        /// <summary>
        /// Set Date Format
        /// </summary>
        private string dateFormat = "M/d/yyyy";

        /// <summary>
        /// Used to store the current statement Id
        /// </summary>
        private int currentStatementId;

        /// <summary>
        /// Used to store the formSliceNo
        /// </summary>
        private int formSliceNo;

        /// <summary>
        /// loginUserValidation
        /// </summary>
        private bool loginUserValidation;

        /// <summary>
        /// getAutoPrintOnValue
        /// </summary>
        private CommentsData getAutoPrintOnValue = new CommentsData();

        /// <summary>
        /// autoprintonoff
        /// </summary>
        private bool autoprintonoff = false;

        /// <summary>
        /// Variable to store bool value "true" while changing the text in InterestDueTextBox
        /// </summary>
        private bool interestDueIsEdit;

        /// <summary>
        /// Receipt Types Data
        /// </summary>
        F1070ReceiptTypeData receiptTypeTable = new F1070ReceiptTypeData();

        /// <summary>
        /// cancel flag
        /// </summary>
        private bool cancelflag = false;

        private PaymentEngineData payeeDetails = new PaymentEngineData();

        ///<summary>
        /// Used to hold the message
        /// </summary>
        private bool IsOverUnder = false; 

        ///<summary>
        ///Used to identify edit and Leave from cell
        /// </summary>
        private bool IsEditLeave = false; 

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15020"/> class.
        /// </summary>
        public F15020()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15020"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F15020(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.formSliceNo = formNo;
            this.keyId = keyID;
            this.ReceiptEnginePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.Height, this.ReceiptEnginePictureBox.Width, tabText, red, green, blue);
            this.NewMenu.Click += new EventHandler(this.NewMenu_Click);
            this.SaveMenu.Click += new EventHandler(this.SaveMenu_Click);
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

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
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SetViewMode, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_SetViewMode;

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// Event publication for cancel triggerring in slice
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_CancelSliceInformation, PublicationScope.Descendants)]
        public event EventHandler<DataEventArgs<int>> D9030_F9030_CancelSliceInformation;

        /// <summary>
        /// Event publication for save triggerring in slice
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_SaveSliceInformation, PublicationScope.Descendants)]
        public event EventHandler<DataEventArgs<int>> D9030_F9030_SaveSliceInformation;

        #endregion

        #region Enum

        /// <summary>
        /// Enumerator PaymentOption
        /// </summary>
        public enum PaymentOptionTypes
        {
            /// <summary>
            /// MinDue  = 0.
            /// </summary>
            MinDue = 0,

            /// <summary>
            /// Balance = 1.
            /// </summary>
            Balance,

            /// <summary>
            /// Partial = 2.
            /// </summary>
            Partial
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F15020 control.
        /// </summary>
        /// <value>The F15020 control.</value>
        [CreateNew]
        public F15020Controller Form15020Control
        {
            get { return this.form15020Control as F15020Controller; }
            set { this.form15020Control = value; }
        }

        /// <summary>
        /// Gets or sets the page mode.
        /// </summary>
        /// <value>The page mode.</value>
        private TerraScanCommon.PageModeTypes PageMode
        {
            get
            {
                return this.pageMode;
            }

            set
            {
                this.pageMode = value;
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    this.processRowEnter = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets the prev receipt ID.
        /// </summary>
        /// <value>The prev receipt ID.</value>
        private int? CurrentReceiptId
        {
            get
            {
                return this.currentReceiptId;
            }

            set
            {
                this.currentReceiptId = value;

                ////sets additionalOperationSmartPart keyid - required for attachment and comment
                if (this.currentReceiptId.HasValue)
                {
                    this.additionalOperationSmartPart.KeyId = this.currentReceiptId.Value;
                }
                else
                {
                    this.additionalOperationSmartPart.KeyId = -1;
                }
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

                    ////Code commented for client issue - load after invalid error message
                    if (this.receiptEngine.ListHistoryGrid.Rows.Count > 0)// && this.receiptEngine.GetReceiptDetails.Rows.Count > 0)
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
        /// Operations the button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_OperationButtonClick, Thread = ThreadOption.UserInterface)]
        public void OperationButtonClick(object sender, DataEventArgs<string> e)
        {
            try
            {
                switch (e.Data)
                {
                    case "NEW":
                        this.NewReceiptButton_Click();
                        break;
                    case "SAVE":
                        this.SaveButton_Click();
                        break;
                    case "CANCEL":
                        this.CancelReceiptButton_Click();
                        break;
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
                ////eventArgs.Data.FlagFormClose = this.CheckPageStatus(!eventArgs.Data.FlagForQueryEngine);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
        {
            this.cancelflag = true;
            this.PerformCancel();
            this.cancelflag = false;
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = this.formSliceNo;
            this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
            ////bool isSaved = this.SaveReceipt(false);
            ////if (!isSaved)
            ////{
            ////    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredMsg");
            ////    sliceValidationFields.RequiredFieldMissing = true;
            ////}
            ////this.FormSlice_SetViewMode(this, new DataEventArgs<int>(this.masterFormNo));
        }

        /// <summary>
        /// Event Subscription for save confirmed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, EventArgs eventArgs)
        {
            this.PaymentEndEdit(); 
            this.SaveReceipt(false);
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
                    ////populate historygrid with the new key id           
                    this.PopulateHistoryGrid();

                    if (this.StatementBalanceTextBox.DecimalTextBoxValue <= 0)
                    {
                        this.F1430CalculatorLinkLabel.Enabled = false;
                    }
                    else
                    {
                        this.F1430CalculatorLinkLabel.Enabled = true;
                    }

                    this.ReceiptEnginePanel.BackColor = Color.Silver;
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



        #endregion

        #region Protected methods

        /// <summary>
        /// Called when [D9030_ F9030_ reload after save].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_ReloadAfterSave(TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.D9030_F9030_ReloadAfterSave != null)
            {
                this.D9030_F9030_ReloadAfterSave(this, eventArgs);
            }
        }

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

                        this.CancelReceiptButton_Click();
                        break;
                    default:
                        break;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion Protected methods

        #region Static Methods

        /// <summary>
        /// checks for tax due and interestdue amount
        /// </summary>
        /// <param name="sourceTextBox">The source text box to be validated.</param>
        /// <returns>true if value gets validated</returns>
        private static bool CheckAmountDue(TerraScanTextBox sourceTextBox)
        {
            ////validation stands for newreceipt only                     
            decimal maxMoneyValue = (decimal)int.MaxValue;

            // checks for - smallmoney datatype range
            maxMoneyValue = maxMoneyValue / 10000;

            if (maxMoneyValue < sourceTextBox.DecimalTextBoxValue)
            {
                MessageBox.Show(String.Concat(sourceTextBox.Tag, " ", SharedFunctions.GetResourceString("InvalidAmount")), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                sourceTextBox.Text = "0.00";
                sourceTextBox.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F15020 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F15020_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                //// Assigning Current Date to ReceiptDate Variable.
                if (D9030.F9030.lastreceiptdate != null)
                {
                    this.receiptDate = DateTime.Parse(D9030.F9030.lastreceiptdate);
                }
                else
                {
                    this.receiptDate = DateTime.Today;
                }

                this.LoadWorkSpaces();
                ////Binding Database Columns to History Grid.
                this.CustomizeReceiptHistoryGridView();
                ////Binding Database Columns to Payment Grid.
                this.CustomizePaymentItemsGridView();

                // Populate Receipt Types combo
                this.LoadReceiptTypes();

                //// Setting tag property to select option controls
                this.MinDueRadioButton.Tag = PaymentOptionTypes.MinDue;
                this.BalanceRadioButton.Tag = PaymentOptionTypes.Balance;
                this.PartialRadioButton.Tag = PaymentOptionTypes.Partial;
                ////load defauilt values
                this.GetTenderTypeConfiguration();
                ////populate history details
                this.PopulateHistoryGrid();
                ////check for statement balance
                if (this.StatementBalanceTextBox.DecimalTextBoxValue <= 0)
                {
                    this.DisableAllControls = true;
                    this.F1430CalculatorLinkLabel.Enabled = false;
                }
                else
                {
                    this.F1430CalculatorLinkLabel.Enabled = true;
                }

                this.ReceiptEnginePanel.BackColor = Color.Silver;
                ////New Form 9025 Validation
                Form newCommentTemplateForm = new Form();
                newCommentTemplateForm = TerraScanCommon.ShowFormValidation(15020);
                this.loginUserValidation = Convert.ToBoolean(newCommentTemplateForm.Text.ToString());
                ////this.tempInterestDue = Convert.ToDouble(this.InterestDueTextBox.Text.ToString().Trim());                                 
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
        /// Method to Load All SmartParts in UI WorkSpaces
        /// </summary>
        private void LoadWorkSpaces()
        {
            // To Load OperationSmartPart into OperationSmartPartWorkSpace
            if (this.Form15020Control.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
            {
                this.operationSmartPart = (OperationSmartPart)this.Form15020Control.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart);
            }
            else
            {
                this.operationSmartPart = this.Form15020Control.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart);
            }

            this.OperationSmartPartWorkSpace.Show(this.operationSmartPart);

            // To Load AdditionalOperationSmartPart into AddtionalOperationDeckWorkspace
            if (this.form15020Control.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.form15020Control.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart);
            }
            else
            {
                this.additionalOperationSmartPart = this.form15020Control.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart);
            }

            this.AddtionalOperationDeckWorkspace.Show(this.additionalOperationSmartPart);

            ////set required variable - attachment and comment
            this.additionalOperationSmartPart.ParentWorkItem = this.form15020Control.WorkItem;
            this.additionalOperationSmartPart.ParentFormId = this.ParentFormId;
            this.additionalOperationSmartPart.CurrntFormId = this.ParentFormId;
            this.additionalOperationSmartPart.BackColor = Color.Silver;
            this.additionalOperationSmartPart.CommentButtonType = TerraScanButton.ButtonType.CommandButton;
            this.additionalOperationSmartPart.AttachmentButtonType = TerraScanButton.ButtonType.CommandButton;
            this.additionalOperationSmartPart.DefaultAttachmentButtonBackColor = Color.FromArgb(28, 81, 128);
            this.additionalOperationSmartPart.DefaultCommentButtonBackColor = Color.FromArgb(28, 81, 128);
            ////set visible property to the controls
            this.operationSmartPart.DeleteButtonVisible = false;
            ////this.operationSmartPart.SaveButtonText = SharedFunctions.GetResourceString("Save/BatchName");
            this.operationSmartPart.NewButtonText = SharedFunctions.GetResourceString("NewReceiptName");

            // To Load BatchButtonSmartPart into BatchButtonSmartPart
            if (this.form15020Control.WorkItem.SmartParts.Contains(SmartPartNames.BatchButtonSmartPart))
            {
                this.batchButtonSmartPart = (BatchButtonSmartPart)this.form15020Control.WorkItem.SmartParts.Get(SmartPartNames.BatchButtonSmartPart);
            }
            else
            {
                this.batchButtonSmartPart = this.form15020Control.WorkItem.SmartParts.AddNew<BatchButtonSmartPart>(SmartPartNames.BatchButtonSmartPart);
            }

            this.batchButtonSmartPart.ParentWorkItem = this.form15020Control.WorkItem;
            this.batchButtonSmartPart.FormSliceFormNo = this.formSliceNo;
            this.batchButtonSmartPart.CurrentBatchButtonUserId = TerraScanCommon.UserId;
            this.batchButtonSmartPart.CurrentParentFormPermission = true;
            this.batchButtonSmartPart.CurrentFormSlicePermission = this.PermissionFiled;
            this.batchButtonSmartPart.CurrentParentFormBatchButtonStatusMode = BatchButtonSmartPart.BatchButtonStatusMode.Load;
            this.BatchButtonDecWorkspace.Show(this.batchButtonSmartPart);
        }

        #endregion

        #region Private Methods

        #region History Grid

        /// <summary>
        /// Populates the history grid.
        /// </summary>
        private void PopulateHistoryGrid()
        {
            try
            {
                ////suppress any subscribed events
                this.pageLoadStatus = false;
                this.PageMode = TerraScanCommon.PageModeTypes.View;
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                ////reset on form load
                this.CurrentReceiptId = null;
                this.PaymentItemsGridView.CurrentCell = null;
                ////set autoprint status
                this.AutoPrintOnButton.EnableAutoPrint = Convert.ToBoolean(this.form15020Control.WorkItem.GetAutoPrintStatus(this.ParentFormId, TerraScanCommon.UserId));
                ////Load All receipt for the current Statment to History Grid.
                this.receiptEngine.Clear();
                this.receiptEngine = this.form15020Control.WorkItem.F15020_ListHistoryGrid(this.keyId);
                ////check for receipt existence
                if (this.receiptEngine.ListHistoryGrid.Rows.Count > 1)
                {
                    this.ReceiptHistoryGridView.RemoveDefaultSelection = false;
                    this.ReceiptHistoryGridView.Enabled = true;
                }
                else
                {
                    this.ReceiptHistoryGridView.RemoveDefaultSelection = true;
                    this.ReceiptHistoryGridView.Enabled = false;
                }

                this.ReceiptHistoryGridView.DataSource = this.receiptEngine.ListHistoryGrid.DefaultView;
                ////get original row count
                int rowCount = this.ReceiptHistoryGridView.OriginalRowCount;
                this.ReceiptHistoryGridView.DefaultRowIndex = rowCount - 1;
                decimal tempStatementBalance = 0;
                ////checked for statement exist
                if (rowCount > 0)
                {
                    ////enable receipt engine panel
                    this.ReceiptEnginePanel.Enabled = true;
                    ////set owner name
                    this.ownerName = this.receiptEngine.ListHistoryGrid.Rows[0][this.receiptEngine.ListHistoryGrid.OwnerNameColumn].ToString();
                    this.ownerId = this.receiptEngine.ListHistoryGrid.Rows[0][this.receiptEngine.ListHistoryGrid.OwnerIDColumn].ToString();

                    //if (this.payeeDetails.PayeeDetail.Rows.Count <= 0)
                    //{
                        int ownerID;
                        int.TryParse(this.ownerId, out ownerID);
                        if (ownerID > 0)
                        {
                            this.payeeDetails = this.form15020Control.WorkItem.F1019_GetPayeeDetails(ownerID);
                        }
                        else
                        {
                            this.payeeDetails.PayeeDetail.Rows.Clear();
                        }
                    //}

                    ////set receipt report number
                    if (!String.IsNullOrEmpty(this.receiptEngine.ListHistoryGrid.Rows[0][this.receiptEngine.ListHistoryGrid.ReceiptReportColumn].ToString()))
                    {
                        this.reportNumber = int.Parse(this.receiptEngine.ListHistoryGrid.Rows[0][this.receiptEngine.ListHistoryGrid.ReceiptReportColumn].ToString());
                    }

                    ////calculating balance from tax and fees
                    this.statementFees = Convert.ToDecimal(this.ReceiptHistoryGridView.Rows[0].Cells["Fee"].Value);
                    tempStatementBalance = this.statementFees + Convert.ToDecimal(this.ReceiptHistoryGridView.Rows[0].Cells["Tax"].Value);
                    this.ReceiptHistoryGridView.Rows[0].Cells["Balance"].Value = tempStatementBalance;
                    for (int counter = 1; counter < rowCount; counter++)
                    {
                        // calculating Statement Balance from the Receipt.
                        if (!String.IsNullOrEmpty(this.ReceiptHistoryGridView.Rows[counter].Cells["Item"].Value.ToString().Trim()))
                        {
                            this.statementFees += Convert.ToDecimal(this.ReceiptHistoryGridView.Rows[counter].Cells["Fee"].Value);
                            ////The following method violates the DoNotCallPropertiesThatCloneValuesInLoops rule but the 
                            ////violation should be excluded because the DataGridViewRow 
                            ////of identical DataGridViewRowCollection instances is intended.
                            tempStatementBalance = Convert.ToDecimal(this.ReceiptHistoryGridView.Rows[counter - 1].Cells["Balance"].Value) + Convert.ToDecimal(this.ReceiptHistoryGridView.Rows[counter].Cells["Fee"].Value) + Convert.ToDecimal(this.ReceiptHistoryGridView.Rows[counter].Cells["Tax"].Value);
                            this.ReceiptHistoryGridView.Rows[counter].Cells["Balance"].Value = tempStatementBalance;
                        }
                    }

                    ///// Assigning Statement Balance to StatementBalanceTextBox.3
                    this.StatementBalanceTextBox.Text = tempStatementBalance.ToString();
                    ////check for existence of receipts
                    if (rowCount > 1)
                    {
                        this.ReceiptHistoryGridView.TabStop = true;
                        //// Populating Payment based on selected Receipt.
                        this.PopulatePayment(rowCount - 1, false);
                        if (!this.ReceiptHistoryGridView.ThroughMouseClick)
                        {
                            TerraScanCommon.SetDataGridViewPosition(this.ReceiptHistoryGridView, rowCount - 1);
                        }
                    }
                    else
                    {
                        this.ReceiptHistoryGridView.TabStop = false;
                        this.ReceiptHistoryGridView.Rows[0].Selected = false;
                        this.ReceiptHistoryGridView.CurrentCell = null;
                        //// Clear all controls
                        this.ClearReceiptDetails();
                    }
                }
                else
                {
                    this.ClearReceiptDetails();
                }

                if (rowCount <= this.ReceiptHistoryGridView.NumRowsVisible)
                {
                    this.HistoryGridVscrollBar.Visible = true;
                }
                else
                {
                    this.HistoryGridVscrollBar.Visible = false;
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
            finally
            {
                ////reset pageLoadStatus - trigger any subscribed events               
                this.pageLoadStatus = true;
                this.FocusRequiredInputField();
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the ReceiptHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ReceiptHistoryGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.pageLoadStatus && this.processRowEnter && e.RowIndex != 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    //// Assigning value to PageMode property.
                    this.pageMode = TerraScanCommon.PageModeTypes.View;

                    // Populating Payment based on selected Receipt.
                    this.PopulatePayment(e.RowIndex, true);
                    this.Cursor = Cursors.Default;
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
        /// Handles the MouseDown event of the ReceiptHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void ReceiptHistoryGridView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                int rowIndex = this.ReceiptHistoryGridView.HitTest(e.X, e.Y).RowIndex;
                int firstScrollingRowIndex = this.ReceiptHistoryGridView.FirstDisplayedScrollingRowIndex;
                ////check for enabling row enter
                if (this.ReceiptHistoryGridView.CurrentCell == null && rowIndex < 1)
                {
                    this.processRowEnter = false;
                    this.ReceiptHistoryGridView.DeselectCurrentCell = true;
                    return;
                }

                ////check page status
                if (!this.CheckPageStatus(false))
                {
                    this.processRowEnter = false;
                    this.ReceiptHistoryGridView.DeselectCurrentCell = true;
                    this.PaymentItemsGridView.Focus();
                    return;
                }

                ////set previous scrolling index
                this.ReceiptHistoryGridView.FirstDisplayedScrollingRowIndex = firstScrollingRowIndex;
                TerraScanCommon.SetDataGridViewPosition(this.ReceiptHistoryGridView, rowIndex);
                this.ReceiptHistoryGridView.DeselectCurrentCell = false;
                this.processRowEnter = true;
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the  ReceiptHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void ReceiptHistoryGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                Decimal outDecimal;

                //// Only paint if desired, formattable column

                if (e.ColumnIndex == this.ReceiptHistoryGridView.Columns["Fee"].Index || e.ColumnIndex == this.ReceiptHistoryGridView.Columns["Tax"].Index || e.ColumnIndex == this.ReceiptHistoryGridView.Columns["Balance"].Index || e.ColumnIndex == this.ReceiptHistoryGridView.Columns["Interest"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !String.IsNullOrEmpty(this.ReceiptHistoryGridView.Rows[e.RowIndex].Cells[this.receiptEngine.ListHistoryGrid.IDColumn.ColumnName].Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            if (outDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                                e.CellStyle.ForeColor = Color.FromArgb(0, 128, 0);

                                //// e.CellStyle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            }
                            else
                            {
                                e.Value = outDecimal.ToString("#,##0.00");
                                e.FormattingApplied = true;
                            }
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

                if (e.ColumnIndex == this.ReceiptHistoryGridView.Columns["Number"].Index)
                {
                    ////remove link for first row

                    if (e.RowIndex != 0)
                    {
                        if (this.ReceiptHistoryGridView.Rows[e.RowIndex].Selected || this.ReceiptHistoryGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                        {
                            (ReceiptHistoryGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).LinkColor = Color.White;
                            (ReceiptHistoryGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).ActiveLinkColor = Color.White;
                            (ReceiptHistoryGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).VisitedLinkColor = Color.White;
                        }
                        else
                        {
                            (ReceiptHistoryGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).LinkColor = Color.Blue;
                            (ReceiptHistoryGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).ActiveLinkColor = Color.Red;
                            (ReceiptHistoryGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).VisitedLinkColor = Color.Blue;
                        }
                    }
                    else
                    {
                        (ReceiptHistoryGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).LinkBehavior = LinkBehavior.NeverUnderline;
                        (ReceiptHistoryGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).LinkColor = Color.Black;
                        (ReceiptHistoryGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).ActiveLinkColor = Color.Black;
                        (ReceiptHistoryGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).VisitedLinkColor = Color.Black;
                    }
                }

                //// To show tool tip text added
                if (e.ColumnIndex == 0 && e.RowIndex >= 0)
                {
                    if (this.ReceiptHistoryGridView.Rows[e.RowIndex].Cells[this.ReceiptHistoryGridView.Columns["ID"].Index].Value != null)
                    {
                        if (!String.IsNullOrEmpty(this.ReceiptHistoryGridView.Rows[e.RowIndex].Cells[this.ReceiptHistoryGridView.Columns["ID"].Index].Value.ToString()))
                        {
                            DataGridViewCell cell = this.ReceiptHistoryGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                            cell.ToolTipText = "ID = " + this.ReceiptHistoryGridView.Rows[e.RowIndex].Cells[this.ReceiptHistoryGridView.Columns["ID"].Index].Value.ToString();
                        }
                    }
                }
                //// till here
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataError event of the ReceiptHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void ReceiptHistoryGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the ReceiptHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ReceiptHistoryGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (e.ColumnIndex == this.ReceiptHistoryGridView.Columns["Number"].Index && e.RowIndex > 0 && this.CurrentReceiptId.HasValue)
                {
                    FormInfo formInfo = TerraScanCommon.GetFormInfo(11001);
                    formInfo.optionalParameters = new object[] { this.CurrentReceiptId };
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
        /// Handles the CellMouseMove event of the ReceiptHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void ReceiptHistoryGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex == 0)
                {
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the ReceiptHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void ReceiptHistoryGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.ReceiptHistoryGridView.OriginalRowCount == 1 && this.ReceiptHistoryGridView.CurrentCell != null)
                {
                    this.ReceiptHistoryGridView.CurrentCell = null;
                }
            }
            catch (InvalidOperationException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Payment Details

        /// <summary>
        /// Populates the payment grid.
        /// </summary>
        /// <param name="clearPaymentItems">if set to <c>true</c> [clear payment items].</param>
        private void PopulatePaymentGrid(bool clearPaymentItems)
        {
            ////fill paymentsDatatable with the value from the database
            this.PaymentItemsGridView.CurrentCell = null;
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
            {
                this.SetReadOnlyToGrid(true);
            }

            if (clearPaymentItems)
            {
                this.receiptEngine.PaymentItems.Clear();
            }

            this.FillpaymentsDataTable();
            this.PaymentItemsGridView.Refresh();
        }

        /// <summary>
        /// Populates the payment.
        /// </summary>
        /// <param name="rowIndex">current rowindex of the grid</param>
        /// <param name="loadReceipt">if set to <c>true</c> load receipt details from the database</param>
        private void PopulatePayment(int rowIndex, bool loadReceipt)
        {
            if (rowIndex > 0)
            {
                ////populating header and paymentgrid depending on the stored values.
                if (!String.IsNullOrEmpty(this.ReceiptHistoryGridView.Rows[rowIndex].Cells["Item"].Value.ToString().Trim()) && String.Compare(this.ReceiptHistoryGridView.Rows[rowIndex].Cells["Item"].Value.ToString().Trim(), "STATEMENT", true) != 0)
                {
                    ////freeze first statement row
                    this.ReceiptHistoryGridView.Rows[0].Frozen = true;
                    ////display selected receipt
                    if (!this.ReceiptHistoryGridView.Rows[rowIndex].Displayed)
                    {
                        this.ReceiptHistoryGridView.FirstDisplayedScrollingRowIndex = rowIndex;
                    }

                    ////if receipt details loaded then return
                    if (this.currentReceiptId.HasValue && this.currentReceiptId.Value.Equals(this.ReceiptHistoryGridView.Rows[rowIndex].Cells["ID"].Value))
                    {
                        return;
                    }

                    this.CurrentReceiptId = Convert.ToInt32(this.ReceiptHistoryGridView.Rows[rowIndex].Cells["ID"].Value);
                    this.PaymentItemsGridView.CurrentCell = null;

                    ////true - loadReceipt values From database && check for loaded receipt
                    if (loadReceipt || !(this.receiptEngine.GetReceiptDetails.Rows.Count > 0 && this.currentReceiptId.Value.Equals(this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.ReceiptIDColumn])))
                    {
                        this.receiptEngine.GetReceiptDetails.Clear();
                        this.receiptEngine.PaymentItems.Clear();
                        this.receiptEngine.Merge(this.form15020Control.WorkItem.F15020_GetReceiptDetails(this.currentReceiptId.Value));
                    }

                    if (this.receiptEngine.GetReceiptDetails.Rows.Count > 0)
                    {
                        this.DisplayHeaderDetails();
                        this.PopulatePaymentGrid(false);
                        this.SetButtonsProperty();
                    }

                    this.SetAdditionalOperationCount(true);
                    this.AuditLinkLabel.Text = string.Concat(SharedFunctions.GetResourceString("ReceiptAuditLink"), this.ReceiptHistoryGridView.Rows[rowIndex].Cells["ID"].Value.ToString());
                    ////this.AuditLinkLabel.Text = string.Concat(SharedFunctions.GetResourceString("ReceiptAuditLink"), "123456789012");

                    ////Used to get the statement id
                    int.TryParse(this.ReceiptHistoryGridView.Rows[rowIndex].Cells["ID"].Value.ToString(), out this.currentStatementId);

                    this.AuditLinkLabel.Enabled = true;
                }
                else
                {
                    ////clearing receipt details
                    this.ClearReceiptDetails();
                }
            }
        }

        /// <summary>
        /// Filldts the payments.
        /// </summary>
        private void FillpaymentsDataTable()
        {
            decimal paymentTotal = 0;
            decimal amountValue = 0;

            for (int counter = 0; counter < this.receiptEngine.PaymentItems.Rows.Count; counter++)
            {
                this.receiptEngine.PaymentItems.Rows[counter][this.receiptEngine.PaymentItems.TenderTypeColumn] = this.receiptEngine.PaymentItems.Rows[counter][this.receiptEngine.PaymentItems.TenderTypeIDColumn];
                ////calculate paymenttotal
                decimal.TryParse(this.receiptEngine.PaymentItems.Rows[counter][this.receiptEngine.PaymentItems.AmountColumn].ToString(), out amountValue);
                paymentTotal += amountValue;
            }

            ////add new record for new receipt
            if (this.PageMode.Equals(TerraScanCommon.PageModeTypes.New))
            {
                this.receiptEngine.PaymentItems.Clear();
                F15020ReceiptEngineData.PaymentItemsRow dr = this.receiptEngine.PaymentItems.NewPaymentItemsRow();
                
                //Removed the tender Type default 'check' TSCO #13410.
                
                //dr.TenderType = "3"; 
                //dr.PaidBy = this.ownerName;
                //dr.Amount = this.ReceiptTotalTextBox.DecimalTextBoxValue;
                //dr.TenderTypeID = 3;
                //dr.ModuleID = this.ParentFormId;
                //dr.TenderType = "";
                //dr.PaidBy = "";
                //dr.Amount = DBNull.Value;
                //dr.TenderTypeID = ;
                //dr.ModuleID = this.ParentFormId;

                try
                {
                    if (this.payeeDetails.PayeeDetail.Rows.Count > 0)
                    {
                        dr.Address1 = this.payeeDetails.PayeeDetail.Rows[0]["Address1"].ToString();
                        dr.Address2 = this.payeeDetails.PayeeDetail.Rows[0]["Address2"].ToString();
                        dr.City = this.payeeDetails.PayeeDetail.Rows[0]["City"].ToString();
                        dr.State = this.payeeDetails.PayeeDetail.Rows[0]["State"].ToString();
                        dr.Zip = this.payeeDetails.PayeeDetail.Rows[0]["Zip"].ToString();
                        dr.Comment = this.payeeDetails.PayeeDetail.Rows[0]["Comment"].ToString();
                    }
                    else
                    {
                        dr.Address1 = "";
                        dr.Address2 = "";
                        dr.City = "";
                        dr.State = "";
                        dr.Zip = "";
                        dr.Comment = "";
                    }
                }
                catch (Exception ex)
                {
                }

                this.receiptEngine.PaymentItems.Rows.Add(dr);
                paymentTotal = this.ReceiptTotalTextBox.DecimalTextBoxValue;


            }

            ////assigns paymenttotal
            this.PaymentTotalTextBox.Text = paymentTotal.ToString();
            ////returns calculated balance value
            this.CalculateBalance();

            for (int counter = this.receiptEngine.PaymentItems.Rows.Count; counter < 3; counter++)
            {
                F15020ReceiptEngineData.PaymentItemsRow dr = this.receiptEngine.PaymentItems.NewPaymentItemsRow();
                dr.ModuleID = this.ParentFormId;
                this.receiptEngine.PaymentItems.Rows.Add(dr);
            }

            ////check with no of visible rows
            if (this.receiptEngine.PaymentItems.Rows.Count == 3)
            {
                this.PaymentGridVscrollBar.Visible = true;
            }
            else
            {
                this.PaymentGridVscrollBar.Visible = false;
            }

            this.receiptEngine.PaymentItems.AcceptChanges();
            this.PaymentItemsGridView.DataSource = this.receiptEngine.PaymentItems.DefaultView;
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_EditingControlShowing(object sender, System.Windows.Forms.DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                ////set color to editing control
                e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    ((ComboBox)e.Control).SelectionChangeCommitted -= new EventHandler(this.ReceiptEngine_SelectionChangeCommitted);
                    ((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(this.ReceiptEngine_SelectionChangeCommitted);
                    ((ComboBox)e.Control).KeyPress -= new KeyPressEventHandler(this.ReceiptEngine_SelectionChangeCommitted);
                    ((ComboBox)e.Control).KeyPress += new KeyPressEventHandler(this.ReceiptEngine_SelectionChangeCommitted); 
                    ((ComboBox)e.Control).Validating -= new CancelEventHandler(this.ReceiptEngineUserControl_ComboBoxValidating);
                    ((ComboBox)e.Control).Validating += new CancelEventHandler(this.ReceiptEngineUserControl_ComboBoxValidating);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the validating event of the PaymentItemsGridView combobox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param> 
        private void ReceiptEngineUserControl_ComboBoxValidating(object sender, CancelEventArgs e)
        {
            try
            {
                ComboBox combo = (ComboBox)sender;
                int rowIndex = -1;
                if (this.PaymentItemsGridView.CurrentCell != null)
                {
                    rowIndex = this.PaymentItemsGridView.CurrentCell.RowIndex;
                }
                ////change property for combobox change
                if (rowIndex >= 0)
                {
                    string val = combo.SelectedValue.ToString();
                    //if (!string.IsNullOrEmpty(val))  //if (!Object.Equals(combo.SelectedValue, this.receiptEngine.PaymentItems.Rows[rowIndex]["TenderType"]))
                    //{
                        this.ChangeTenderTypeRelatedFields(combo);
                        ////check refund item
                        //this.CheckTenderTypeCombo(combo);

                        //Method called to fix the issue - Payment amount is not correct after interest date change
                        //calculating related values for new values
                        this.CalculatePaymentTotal();

                        if (this.paymentOption.Equals(PaymentOptionTypes.Partial))
                        {
                            this.CalculateDueAmount(false, false);
                        }

                        this.CalculateBalance();
                    
                }
                ////check refund item
                //this.CheckTenderTypeCombo(combo);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Checks the tender type combo.
        /// </summary>
        /// <param name="combo">The combo.</param>
        private void CheckTenderTypeCombo(ComboBox combo)
        {
            int rowIndex = -1;
            if (this.PaymentItemsGridView.CurrentCell != null)
            {
                rowIndex = this.PaymentItemsGridView.CurrentCell.RowIndex;
            }

            if (rowIndex >= 0 && Object.Equals(this.receiptEngine.PaymentItems.Rows[rowIndex]["TenderTypeID"], 2))
            {
                int refundCount = 0;
                for (int itemCount = 0; itemCount < this.PaymentItemsGridView.Rows.Count; itemCount++)
                {
                    if (string.Equals(this.PaymentItemsGridView["TenderType", itemCount].Value, "2"))
                    {
                        refundCount = refundCount + 1;
                    }
                }

                if (refundCount > 1)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("RefundAllowed"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("TenderTypeError")), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ////reset tendertype to default check type
                    combo.SelectedValue = "3";
                    combo.Focus();
                    this.receiptEngine.PaymentItems.Rows[rowIndex]["TenderTypeID"] = 3;
                    this.receiptEngine.PaymentItems.Rows[rowIndex]["TenderType"] = "3";
                }
            }
        }

        /// <summary>
        /// Change the tender type related fields.
        /// </summary>
        /// <param name="combo">The source control.</param>
        private void ChangeTenderTypeRelatedFields(ComboBox combo)
        {
            int rowIndex = 0;
            if (this.PaymentItemsGridView.CurrentCell != null)
            {
                rowIndex = this.PaymentItemsGridView.CurrentCell.RowIndex;
            }

            ////change property for combobox change
                if (combo.SelectedIndex > 0 && combo.SelectedValue != null)
            {
                if (String.IsNullOrEmpty(this.receiptEngine.PaymentItems.Rows[rowIndex]["TenderType"].ToString()))
                {
                    this.PaymentItemsGridView["TenderType", rowIndex].Value = combo.SelectedValue.ToString();
                    if (string.Equals(this.PaymentItemsGridView["TenderType", rowIndex].Value.ToString(), "1") && this.IsEditLeave)
                    {
                        this.IsOverUnder = true;
                    }
                    this.receiptEngine.PaymentItems.Rows[rowIndex]["TenderTypeID"] = combo.SelectedValue;
                    //// Paid by should be preview paid by a value or it should be owner name if it is first record
                    if (this.PaymentItemsGridView.CurrentCell != null && this.PaymentItemsGridView.CurrentCell.RowIndex > 0)
                    {
                        this.receiptEngine.PaymentItems.Rows[rowIndex]["PaidBy"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex - 1]["PaidBy"];
                        this.receiptEngine.PaymentItems.Rows[rowIndex]["Address1"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex - 1]["Address1"];
                        this.receiptEngine.PaymentItems.Rows[rowIndex]["Address2"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex - 1]["Address2"];
                        this.receiptEngine.PaymentItems.Rows[rowIndex]["City"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex - 1]["City"];
                        this.receiptEngine.PaymentItems.Rows[rowIndex]["State"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex - 1]["State"];
                        this.receiptEngine.PaymentItems.Rows[rowIndex]["Zip"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex - 1]["Zip"];
                        this.receiptEngine.PaymentItems.Rows[rowIndex]["Comment"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex - 1]["Comment"];
                    }
                    else
                    {
                        this.receiptEngine.PaymentItems.Rows[rowIndex]["PaidBy"] = this.ownerName;
                        if (this.payeeDetails.PayeeDetail.Rows.Count > 0)
                        {
                            this.receiptEngine.PaymentItems.Rows[rowIndex]["Address1"] = this.payeeDetails.PayeeDetail.Rows[0]["Address1"];
                            this.receiptEngine.PaymentItems.Rows[rowIndex]["Address2"] = this.payeeDetails.PayeeDetail.Rows[0]["Address2"];
                            this.receiptEngine.PaymentItems.Rows[rowIndex]["City"] = this.payeeDetails.PayeeDetail.Rows[0]["City"];
                            this.receiptEngine.PaymentItems.Rows[rowIndex]["State"] = this.payeeDetails.PayeeDetail.Rows[0]["State"];
                            this.receiptEngine.PaymentItems.Rows[rowIndex]["Zip"] = this.payeeDetails.PayeeDetail.Rows[0]["Zip"];
                            this.receiptEngine.PaymentItems.Rows[rowIndex]["Comment"] = this.payeeDetails.PayeeDetail.Rows[0]["Comment"];
                        }
                    }

                    this.receiptEngine.PaymentItems.Rows[rowIndex]["Amount"] = this.BalanceTextBox.DecimalTextBoxValue;
                }
                else
                {
                    this.PaymentItemsGridView["TenderType", rowIndex].Value = combo.SelectedValue.ToString();
                    if (string.Equals(this.PaymentItemsGridView["TenderType", rowIndex].Value.ToString(), "1") && this.IsEditLeave)
                    {
                        this.IsOverUnder = true;
                    }
                    this.receiptEngine.PaymentItems.Rows[rowIndex]["TenderTypeID"] = combo.SelectedValue;
                    //// Paid by value should change only if it is null 
                    if (string.IsNullOrEmpty(this.receiptEngine.PaymentItems.Rows[rowIndex]["PaidBy"].ToString()))
                    {
                        if (this.PaymentItemsGridView.CurrentCell != null && this.PaymentItemsGridView.CurrentCell.RowIndex > 0)
                        {
                            this.receiptEngine.PaymentItems.Rows[rowIndex]["PaidBy"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex - 1]["PaidBy"];
                            this.receiptEngine.PaymentItems.Rows[rowIndex]["Address1"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex - 1]["Address1"];
                            this.receiptEngine.PaymentItems.Rows[rowIndex]["Address2"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex - 1]["Address2"];
                            this.receiptEngine.PaymentItems.Rows[rowIndex]["City"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex - 1]["City"];
                            this.receiptEngine.PaymentItems.Rows[rowIndex]["State"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex - 1]["State"];
                            this.receiptEngine.PaymentItems.Rows[rowIndex]["Zip"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex - 1]["Zip"];
                            this.receiptEngine.PaymentItems.Rows[rowIndex]["Comment"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex - 1]["Comment"];
                        }
                        else
                        {
                            this.receiptEngine.PaymentItems.Rows[rowIndex]["PaidBy"] = this.ownerName;
                            if (this.payeeDetails.PayeeDetail.Rows.Count > 0)
                            {
                                this.receiptEngine.PaymentItems.Rows[rowIndex]["Address1"] = this.payeeDetails.PayeeDetail.Rows[0]["Address1"];
                                this.receiptEngine.PaymentItems.Rows[rowIndex]["Address2"] = this.payeeDetails.PayeeDetail.Rows[0]["Address2"];
                                this.receiptEngine.PaymentItems.Rows[rowIndex]["City"] = this.payeeDetails.PayeeDetail.Rows[0]["City"];
                                this.receiptEngine.PaymentItems.Rows[rowIndex]["State"] = this.payeeDetails.PayeeDetail.Rows[0]["State"];
                                this.receiptEngine.PaymentItems.Rows[rowIndex]["Zip"] = this.payeeDetails.PayeeDetail.Rows[0]["Zip"];
                                this.receiptEngine.PaymentItems.Rows[rowIndex]["Comment"] = this.payeeDetails.PayeeDetail.Rows[0]["Comment"];
                            }
                            else
                            {
                                this.receiptEngine.PaymentItems.Rows[rowIndex]["Address1"] = "";
                                this.receiptEngine.PaymentItems.Rows[rowIndex]["Address2"] = "";
                                this.receiptEngine.PaymentItems.Rows[rowIndex]["City"] = "";
                                this.receiptEngine.PaymentItems.Rows[rowIndex]["State"] = "";
                                this.receiptEngine.PaymentItems.Rows[rowIndex]["Zip"] = "";
                                this.receiptEngine.PaymentItems.Rows[rowIndex]["Comment"] = "";
                            }
                        }
                    }
                }

                this.PaymentItemsGridView["PaidBy", rowIndex].ReadOnly = false;
                this.PaymentItemsGridView["Amount", rowIndex].ReadOnly = false;
                this.PaymentItemsGridView["CheckNumber", rowIndex].ReadOnly = false;
            }
            else
            {
                this.PaymentItemsGridView["TenderType", rowIndex].Value = "";
                this.receiptEngine.PaymentItems.Rows[rowIndex]["TenderTypeID"] = DBNull.Value;
                this.receiptEngine.PaymentItems.Rows[rowIndex]["PaidBy"] = "";
                this.receiptEngine.PaymentItems.Rows[rowIndex]["Amount"] = DBNull.Value;
                this.receiptEngine.PaymentItems.Rows[rowIndex]["CheckNumber"] = "";

                this.receiptEngine.PaymentItems.Rows[rowIndex]["Address1"] = "";
                this.receiptEngine.PaymentItems.Rows[rowIndex]["Address2"] = "";
                this.receiptEngine.PaymentItems.Rows[rowIndex]["City"] = "";
                this.receiptEngine.PaymentItems.Rows[rowIndex]["State"] = "";
                this.receiptEngine.PaymentItems.Rows[rowIndex]["Zip"] = "";
                this.receiptEngine.PaymentItems.Rows[rowIndex]["Comment"] = "";


                this.PaymentItemsGridView["PaidBy", rowIndex].ReadOnly = true;
                this.PaymentItemsGridView["CheckNumber", rowIndex].ReadOnly = true;
                this.PaymentItemsGridView["Amount", rowIndex].ReadOnly = true;
            }

            this.PaymentItemsGridView.Refresh();
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the PaymentItemsGridView combobox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>        
        private void ReceiptEngine_SelectionChangeCommitted(object sender, EventArgs e)
            {
            try
            {
                ComboBox combo = (ComboBox)sender;

                ////change property for combobox change

                this.ChangeTenderTypeRelatedFields(combo);

                ////check refund item
                //this.CheckTenderTypeCombo(combo);

                //Method called to fix the issue - Payment amount is not correct after interest date change
                //calculating related values for new values
                this.CalculatePaymentTotal();

                if (this.paymentOption.Equals(PaymentOptionTypes.Partial))
                {
                    this.CalculateDueAmount(false, false);
                }

                this.CalculateBalance();
                this.PaymentEndEdit(); 
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                ////checks only if amount or tendertype field
                if (e.ColumnIndex == this.PaymentItemsGridView.Columns["Amount"].Index || e.ColumnIndex == this.PaymentItemsGridView.Columns["TenderType"].Index)
                {

                    bool cashrow;

                    ////calculating related values for new values
                    this.CalculatePaymentTotal();
                    if (this.paymentOption.Equals(PaymentOptionTypes.Partial))
                    {
                        this.CalculateDueAmount(false, false);
                    }

                    this.CalculateBalance();

                    ////Code has been added to display CashBack
                    ///Code has been modified to display cash back only for negative amounts
                    if (!string.IsNullOrEmpty(this.PaymentItemsGridView.Rows[e.RowIndex].Cells["TenderType"].Value.ToString()) && this.PaymentItemsGridView.Rows[e.RowIndex].Cells["TenderType"].Value.ToString().Equals("4") && this.BalanceTextBox.DecimalTextBoxValue <  0)
                    {
                        if (string.IsNullOrEmpty(this.PaymentItemsGridView.Rows[e.RowIndex].Cells["Amount"].Value.ToString()))
                        {
                            cashrow=false;  
                        }
                        else
                        {
                            cashrow=true;  
                        }
                        decimal outDecimalValue;
                        decimal paymentsTotal = 0;
                        int paymentCount = 0;

                        for (int counter = 0; counter < this.receiptEngine.PaymentItems.Rows.Count; counter++)
                        {
                            if (!String.IsNullOrEmpty(this.receiptEngine.PaymentItems.Rows[counter]["TenderType"].ToString()) && Decimal.TryParse(this.receiptEngine.PaymentItems.Rows[counter]["Amount"].ToString(), out outDecimalValue))
                            {
                                paymentsTotal += outDecimalValue;
                                if (outDecimalValue != 0)
                                {
                                    paymentCount++;
                                }
                            }
                        }

                        this.PaymentTotalTextBox.Text = paymentsTotal.ToString();

                        if (paymentCount == this.receiptEngine.PaymentItems.Rows.Count)
                        {
                            F15020ReceiptEngineData.PaymentItemsRow dr = this.receiptEngine.PaymentItems.NewPaymentItemsRow();
                            dr.ModuleID = this.ParentFormId;
                            this.receiptEngine.PaymentItems.Rows.Add(dr);
                            this.PaymentGridVscrollBar.Visible = false;
                            this.PaymentItemsGridView.Refresh();
                        }
                        else
                        {
                            if (cashrow)
                            {
                                this.PaymentItemsGridView["TenderType", paymentCount].Value = "11";
                                this.receiptEngine.PaymentItems.Rows[paymentCount]["TenderTypeID"] = "11";
                                
                            }
                            cashrow = false;
                                //// Paid by should be preview paid by a value or it should be owner name if it is first record
                                if (this.PaymentItemsGridView.CurrentCell != null && this.PaymentItemsGridView.CurrentCell.RowIndex >= 0)
                                {
                                    this.receiptEngine.PaymentItems.Rows[paymentCount]["PaidBy"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex]["PaidBy"];
                                    this.receiptEngine.PaymentItems.Rows[paymentCount]["Address1"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex]["Address1"];
                                    this.receiptEngine.PaymentItems.Rows[paymentCount]["Address2"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex]["Address2"];
                                    this.receiptEngine.PaymentItems.Rows[paymentCount]["City"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex]["City"];
                                    this.receiptEngine.PaymentItems.Rows[paymentCount]["State"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex]["State"];
                                    this.receiptEngine.PaymentItems.Rows[paymentCount]["Zip"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex]["Zip"];
                                    this.receiptEngine.PaymentItems.Rows[paymentCount]["Comment"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex]["Comment"];
                                }
                                else
                                {
                                    this.receiptEngine.PaymentItems.Rows[paymentCount]["PaidBy"] = this.ownerName;
                                }

                                this.receiptEngine.PaymentItems.Rows[paymentCount]["Amount"] = this.BalanceTextBox.DecimalTextBoxValue;

                                this.PaymentItemsGridView["PaidBy", paymentCount].ReadOnly = false;
                                this.PaymentItemsGridView["Amount", paymentCount].ReadOnly = false;
                                this.PaymentItemsGridView["CheckNumber", paymentCount].ReadOnly = false;
                                this.PaymentItemsGridView.Refresh();

                                this.CalculatePaymentTotal();
                                if (this.paymentOption.Equals(PaymentOptionTypes.Partial))
                                {
                                    this.CalculateDueAmount(false, false);
                                }

                                this.CalculateBalance();
                           
                        }

                    }
                    else if (!string.IsNullOrEmpty(this.PaymentItemsGridView.Rows[e.RowIndex].Cells["TenderType"].Value.ToString()) && this.BalanceTextBox.DecimalTextBoxValue != 0)
                    {
                        this.PaymentEndEdit();

                    }
                        if (string.Equals(this.PaymentItemsGridView[this.PaymentItemsGridView.Columns["TenderType"].Index, e.RowIndex].Value.ToString(), "1") && this.IsOverUnder)
                    {
                        decimal outDecimal;
                        decimal.TryParse(this.PaymentItemsGridView.Rows[e.RowIndex].Cells["Amount"].Value.ToString(), out outDecimal);
                        this.CheckOverUnder(outDecimal);
                        this.IsOverUnder = false;
                    } 
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        ///<summary>
        /// Used to hold the Calculation in PaymentGrid View
        /// </summary>
        private void PaymentEndEdit()
        {
            decimal outDecimalValue;
            decimal paymentsTotal = 0;
            int paymentCount = 0;

            for (int counter = 0; counter < this.receiptEngine.PaymentItems.Rows.Count; counter++)
            {
                if (!String.IsNullOrEmpty(this.receiptEngine.PaymentItems.Rows[counter]["TenderType"].ToString()) && Decimal.TryParse(this.receiptEngine.PaymentItems.Rows[counter]["Amount"].ToString(), out outDecimalValue))
                {
                    paymentsTotal += outDecimalValue;
                    if (outDecimalValue != 0)
                    {
                        paymentCount++;
                    }
                }
            }

            this.PaymentTotalTextBox.Text = paymentsTotal.ToString();

            if (paymentCount == this.receiptEngine.PaymentItems.Rows.Count)
            {
                F15020ReceiptEngineData.PaymentItemsRow dr = this.receiptEngine.PaymentItems.NewPaymentItemsRow();
                dr.ModuleID = this.ParentFormId;
                this.receiptEngine.PaymentItems.Rows.Add(dr);
                this.PaymentGridVscrollBar.Visible = false;
                this.PaymentItemsGridView.Refresh();
            }
            else
            {
                ///used to check the value for the problem
                //this.PaymentItemsGridView["TenderType", paymentCount].Value = "11";
                //this.receiptEngine.PaymentItems.Rows[paymentCount]["TenderTypeID"] = "11";
                //// Paid by should be preview paid by a value or it should be owner name if it is first record
                if (paymentCount.Equals(this.PaymentItemsGridView.CurrentCell))
                {
                    if (this.PaymentItemsGridView.CurrentCell != null && this.PaymentItemsGridView.CurrentCell.RowIndex >= 0)
                    {
                        this.receiptEngine.PaymentItems.Rows[paymentCount]["PaidBy"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex]["PaidBy"];
                        this.receiptEngine.PaymentItems.Rows[paymentCount]["Address1"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex]["Address1"];
                        this.receiptEngine.PaymentItems.Rows[paymentCount]["Address2"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex]["Address2"];
                        this.receiptEngine.PaymentItems.Rows[paymentCount]["City"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex]["City"];
                        this.receiptEngine.PaymentItems.Rows[paymentCount]["State"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex]["State"];
                        this.receiptEngine.PaymentItems.Rows[paymentCount]["Zip"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex]["Zip"];
                        this.receiptEngine.PaymentItems.Rows[paymentCount]["Comment"] = this.receiptEngine.PaymentItems.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex]["Comment"];
                    }
                    else
                    {
                        this.receiptEngine.PaymentItems.Rows[paymentCount]["PaidBy"] = this.ownerName;
                    }
                }
                this.receiptEngine.PaymentItems.Rows[paymentCount]["Amount"] = this.BalanceTextBox.DecimalTextBoxValue;

                this.PaymentItemsGridView["PaidBy", paymentCount].ReadOnly = false;
                this.PaymentItemsGridView["Amount", paymentCount].ReadOnly = false;
                this.PaymentItemsGridView["CheckNumber", paymentCount].ReadOnly = false;
                this.PaymentItemsGridView.Refresh();

                this.CalculatePaymentTotal();
                if (this.paymentOption.Equals(PaymentOptionTypes.Partial))
                {
                    this.CalculateDueAmount(false, false);
                }

                this.CalculateBalance();
            }

        }


        /// <summary>
        /// Checks the over under max and min amount.
        /// </summary>
        /// <param name="overUnderValue">The over under value.</param>
        private void CheckOverUnder(decimal overUnderValue)
        {
            string tempvalue = overUnderValue.ToString();
            if (tempvalue.Contains("-"))
            {

                if (this.ReceiptTotalTextBox.DecimalTextBoxValue < this.overUnderMinAmount)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("OverUnderLesser") + this.overUnderMinAmount, String.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("TenderTypeError")), MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                ////modified by Biju on 10/May/2010 to implement #6557
                else if (System.Math.Abs(overUnderValue)> this.overUnderMaxAmount)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("OverUnderGreater") + this.overUnderMaxAmount, string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("TenderTypeError")), MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                if (this.ReceiptTotalTextBox.DecimalTextBoxValue < this.overUnderMinAmount)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("OverUnderLesser") + this.overUnderMinAmount, String.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("TenderTypeError")), MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                ////modified by Biju on 10/May/2010 to implement #6557
                else if (Math.Abs(overUnderValue) > this.overUnderMaxAmount)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("OverUnderGreater") + this.overUnderMaxAmount, string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("TenderTypeError")), MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            
            
        }

        /// <summary>
        /// Handles the CellFormatting event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_CellFormatting(object sender, System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                Decimal outDecimal;

                // Only paint if desired, formattable column

                if (e.ColumnIndex == this.PaymentItemsGridView.Columns["Amount"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    // Only paint if text provided, Only paint if desired text is in cell 

                    if (e.Value != null && !String.IsNullOrEmpty(this.PaymentItemsGridView.Rows[e.RowIndex].Cells["TenderType"].Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            if (outDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                                e.CellStyle.ForeColor = Color.Green;
                            }
                            else
                            {
                                e.Value = outDecimal.ToString("#,##0.00");
                                e.FormattingApplied = true;
                            }
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

                if (e.ColumnIndex == this.PaymentItemsGridView.Columns["TenderType"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && String.IsNullOrEmpty(e.Value.ToString()))
                    {
                        this.PaymentItemsGridView["PaidBy", e.RowIndex].ReadOnly = true;
                        this.PaymentItemsGridView["CheckNumber", e.RowIndex].ReadOnly = true;
                        this.PaymentItemsGridView["Amount", e.RowIndex].ReadOnly = true;
                    }
                }

                //// To show tool tip text added
                if (e.ColumnIndex == 0 && e.RowIndex >= 0)
                {
                    if (this.PaymentItemsGridView.Rows[e.RowIndex].Cells[this.PaymentItemsGridView.Columns["PaymentID"].Index].Value != null)
                    {
                        if (!String.IsNullOrEmpty(this.PaymentItemsGridView.Rows[e.RowIndex].Cells[this.PaymentItemsGridView.Columns["PaymentID"].Index].Value.ToString()))
                        {
                            DataGridViewCell cell = this.PaymentItemsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                            cell.ToolTipText = "PaymentID = " + this.PaymentItemsGridView.Rows[e.RowIndex].Cells[this.PaymentItemsGridView.Columns["PaymentID"].Index].Value.ToString();
                        }
                    }
                }
                //// till here
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellParsing event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_CellParsing(object sender, System.Windows.Forms.DataGridViewCellParsingEventArgs e)
        {
            try
            {
                // Only paint if desired column
                if (e.ColumnIndex == this.PaymentItemsGridView.Columns["Amount"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    // Only paint if text provided, Only paint if desired text is in cell
                    if (e.Value != null)
                    {
                        string tempvalue = e.Value.ToString().Trim();
                        Decimal outDecimal;

                        if (tempvalue.EndsWith("."))
                        {
                            tempvalue = string.Concat(tempvalue, "0");
                        }

                        if (Decimal.TryParse(tempvalue, NumberStyles.Currency, null, out outDecimal))
                        {
                            tempvalue = outDecimal.ToString();

                            if (!tempvalue.Contains("."))
                            {
                                outDecimal = outDecimal / 100;
                            }
                        }

                        e.Value = outDecimal;
                        e.ParsingApplied = true;
                        if (string.Equals(this.PaymentItemsGridView[this.PaymentItemsGridView.Columns["TenderType"].Index, e.RowIndex].Value.ToString(), "1"))
                        {
                            this.CheckOverUnder(outDecimal);
                        }
                        this.IsEditLeave = true;
                        //this.PaymentEndEdit();
                        //////////Check - Over/Under
                        //if (string.Equals(this.PaymentItemsGridView[this.PaymentItemsGridView.Columns["TenderType"].Index, e.RowIndex].Value.ToString(), "1"))
                        //{
                        //    this.CheckOverUnder(outDecimal);
                        //}
                    }
                }
                ////else if (e.ColumnIndex == this.PaymentItemsGridView.Columns["CheckNumber"].Index)
                ////{
                ////    if (e.RowIndex < 0)
                ////    {
                ////        return;
                ////    }

                ////    if (e.Value != null)
                ////    {
                ////        string val = e.Value.ToString();
                ////        Int64 outInt;

                ////        if (Int64.TryParse(val, out outInt))
                ////        {
                ////            e.ParsingApplied = true;
                ////        }
                ////        else
                ////        {
                ////            e.Value = "";
                ////            e.ParsingApplied = true;
                ////        }
                ////    }
                ////}
                else if (e.ColumnIndex == this.PaymentItemsGridView.Columns["TenderType"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }
                    this.IsEditLeave = true;
                    if (string.Equals(this.PaymentItemsGridView[e.ColumnIndex, e.RowIndex].Value.ToString(), "1"))
                    {
                        this.IsOverUnder = true;
                     }
           
                    ////////Check - Over/Under
                    //if (string.Equals(this.PaymentItemsGridView[e.ColumnIndex, e.RowIndex].Value.ToString(), "1"))
                    //{
                    //    decimal outDecimal;

                    //    if (decimal.TryParse(this.PaymentItemsGridView[this.PaymentItemsGridView.Columns["Amount"].Index, e.RowIndex].Value.ToString(), NumberStyles.Currency, null, out outDecimal))
                    //    {
                    //        this.CheckOverUnder(outDecimal);
                    //    }
                    //}
                }

                ///// Added to show tool tip 
                if (e.ColumnIndex == 0 && e.RowIndex >= 0)
                {
                    DataGridViewCell cell = this.PaymentItemsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "PaymentID = " + this.PaymentItemsGridView.Rows[e.RowIndex].Cells[this.PaymentItemsGridView.Columns["PaymentID"].Index].Value.ToString();
                }
                //// Till here
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataError event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_DataError(object sender, System.Windows.Forms.DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Set readonly to grid
        /// </summary>
        /// <param name="readOnlyGrid">Set Readonly to cells.</param>        
        private void SetReadOnlyToGrid(bool readOnlyGrid)
        {
            ////set readonly
            if (readOnlyGrid)
            {
                this.PaymentItemsGridView.Columns["TenderType"].ReadOnly = true;
                this.PaymentItemsGridView.Columns["PaidBy"].ReadOnly = true;
                this.PaymentItemsGridView.Columns["CheckNumber"].ReadOnly = true;
                this.PaymentItemsGridView.Columns["Amount"].ReadOnly = true;
            }
            else
            {
                this.PaymentItemsGridView.Columns["TenderType"].ReadOnly = false;
                this.PaymentItemsGridView.Columns["PaidBy"].ReadOnly = false;
                this.PaymentItemsGridView.Columns["CheckNumber"].ReadOnly = false;
                this.PaymentItemsGridView.Columns["Amount"].ReadOnly = false;
            }
        }

        /// <summary>
        /// Handles the Leave event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_Leave(object sender, EventArgs e)
        {
            try
            {
                this.PaymentItemsGridView.CurrentCell = null;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New) && e.RowIndex >= 0 && this.PaymentItemsGridView["TenderType", e.RowIndex].Value != null && !string.IsNullOrEmpty(this.PaymentItemsGridView["TenderType", e.RowIndex].Value.ToString()))
                {
                    this.PaymentItemsGridView["TenderType", e.RowIndex].ReadOnly = false;
                    this.PaymentItemsGridView["PaidBy", e.RowIndex].ReadOnly = false;
                    this.PaymentItemsGridView["CheckNumber", e.RowIndex].ReadOnly = false;
                    this.PaymentItemsGridView["Amount", e.RowIndex].ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowHeaderMouseClick event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.PaymentItemsGridView.CurrentCell != null)
                {
                    this.PaymentItemsGridView.CurrentCell.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Enter event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_Enter(object sender, EventArgs e)
        {
            this.ReceiptEnginePanel.BackColor = Color.Silver;
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the CheckedChanged event of the SelectPaymentRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SelectPaymentRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!cancelflag)
                {
                    RadioButton sourceRadioButton = sender as RadioButton;
                    if (sourceRadioButton.Checked)
                    {
                        Object sourceObject = sourceRadioButton.Tag;

                        ////code for the selected payment option
                        if (Equals(sourceObject.GetType(), typeof(PaymentOptionTypes)))
                        {
                            this.paymentOption = (PaymentOptionTypes)sourceObject;
                        }
                        else
                        {
                            this.paymentOption = PaymentOptionTypes.MinDue;
                        }
                        this.interestDueIsEdit = false;
                        this.CalculateDueAmount(true, true);
                        ////Calculating balance for the selected payment option
                        this.CalculateBalance();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the AuditLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void AuditLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////check for validity
                /* if (this.currentReceiptId.HasValue)
                 {
                     ////// calling  Common Function For Report
                     Hashtable reportOptionalParameter = new Hashtable();
                     reportOptionalParameter.Add("ReceiptID", this.currentReceiptId);
                     TerraScanCommon.ShowReport(90101, Report.ReportType.Preview, reportOptionalParameter);
                 }*/
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11001);
                formInfo.optionalParameters = new object[1];
                // formInfo.optionalParameters[0] = this.formSliceNo;
                formInfo.optionalParameters[0] = this.currentStatementId;
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

                this.form15020Control.WorkItem.SaveAutoPrint(this.ParentFormId, TerraScanCommon.UserId, this.AutoPrintOnButton.EnableAutoPrint);
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

        #region Due Amount and Interest Calculation

        /// <summary>
        /// Gets Calculated Interest value Value for a statement from the database 
        /// </summary>
        /// <returns> interest due value</returns>
        private string CalculateInterestDue()
        {
            ////check for valid value
            if (this.keyId > 0 && !String.IsNullOrEmpty(this.InterestDateTextBox.Text.Trim()))
            {
                return this.form15020Control.WorkItem.F1004_GetInterestAmount(this.keyId, this.InterestDateTextBox.Text, this.TaxDueTextBox.DecimalTextBoxValue).ToString();
            }

            return "0.00";
        }

        /// <summary>
        /// Displays the AmountDue details.
        /// </summary>
        /// <param name="receiptTotalAmount">receipt TotalAmount</param>
        private void DisplayAmountDueDetails(string receiptTotalAmount)
        {
            this.TaxDueTextBox.Text = Decimal.Zero.ToString();
            this.InterestDueTextBox.Text = Decimal.Zero.ToString();
            this.MinDueRadioButton.Checked = true;
            this.paymentOption = PaymentOptionTypes.MinDue;
            ////check for negative amount
            receiptTotalAmount = receiptTotalAmount.Replace("-", "");
            this.ReceiptTotalTextBox.Text = receiptTotalAmount;
        }

        /// <summary>
        /// Calculatings the balance.
        /// </summary>       
        private void CalculateBalance()
        {
            ////calculating total balance
            decimal receiptBalance = this.ReceiptTotalTextBox.DecimalTextBoxValue - this.PaymentTotalTextBox.DecimalTextBoxValue;
            this.BalanceTextBox.Text = receiptBalance.ToString();
            ////changing colors of the paid and notposted flags, depending on balance
            if (receiptBalance == 0)
            {
                this.BalanceTextBox.BackColor = Color.FromArgb(130, 189, 140);
                this.BalanceTextBox.ForeColor = Color.Black;
            }
            else
            {
                this.BalanceTextBox.BackColor = Color.FromArgb(128, 0, 0);
                this.BalanceTextBox.ForeColor = Color.White;
            }
        }

        /// <summary>
        /// Gets the next working day and assign it to the receiptDate variable.
        /// </summary>
        /// <param name="receiptDateTime">The receipt date time.</param>
        /// <returns>the receiptdate string</returns>
        private string GetNextWorkingDay(DateTime receiptDateTime)
        {
            ////get next day if today else update for default date management
            if (receiptDateTime.Equals(DateTime.Today))
            {
                this.receiptDate = this.form15020Control.WorkItem.F9001_GetNextWorkingDay();
                return this.receiptDate.ToShortDateString();
            }
            else if (String.IsNullOrEmpty(this.ReceiptDateTextBox.Text.Trim()) && receiptDateTime.Equals(DateTime.MinValue))
            {
                ////check for valid date - if not return the empty value assigned in text box else validated value
                this.receiptDate = DateTime.Now;
                return String.Empty;
            }

            this.receiptDate = receiptDateTime;
            return this.receiptDate.ToString(this.dateFormat);
        }

        /// <summary>
        /// Gets Minimum Tax Due Value for a statement from the database 
        /// </summary>
        /// <returns> mintaxdue value</returns>
        private string GetMinTaxDueValue()
        {
            ////check for valid value
            if (this.keyId > 0 && !String.IsNullOrEmpty(this.InterestDateTextBox.Text.Trim()))
            {
                return this.form15020Control.WorkItem.F1003_GetMinTaxDue(this.keyId, this.InterestDateTextBox.Text.Trim()).ToString();
            }

            return "0.00";
        }

        /// <summary>
        /// Amounts the due calculation.
        /// </summary>
        /// <param name="calculateTaxDue">if set to <c>true</c> [allow - tax due calculation].</param>
        /// <param name="calculateInterestDue">if set to <c>true</c> [allow - interest due calculation].</param>
        private void CalculateDueAmount(bool calculateTaxDue, bool calculateInterestDue)
        {
            decimal receiptTotal = 0;
            decimal taxDue = 0;

            ////check for paymentoption
            switch (this.paymentOption)
            {
                case PaymentOptionTypes.Partial:
                    this.ReceiptTotalTextBox.Text = this.PaymentTotalTextBox.Text;
                    ////maintain interest due value 
                    ////if (calculateInterestDue)
                    ////{
                    ////    this.InterestDueTextBox.Text = Decimal.Zero.ToString();
                    ////}

                    ////tax due locked for partial
                    this.TaxDueTextBox.LockKeyPress = true;
                    taxDue = this.PaymentTotalTextBox.DecimalTextBoxValue - this.InterestDueTextBox.DecimalTextBoxValue;
                    this.TaxDueTextBox.Text = taxDue.ToString();

                    break;
                default:
                    ////whether text changed or min tax due/balance calculation
                    if (calculateTaxDue)
                    {
                        if (this.paymentOption.Equals(PaymentOptionTypes.MinDue))
                        {
                            this.TaxDueTextBox.Text = this.GetMinTaxDueValue();
                        }
                        else
                        {
                            this.TaxDueTextBox.Text = this.StatementBalanceTextBox.Text;
                        }
                    }

                    ////else default value

                    if (calculateInterestDue && !this.interestDueIsEdit)
                    {
                        this.InterestDueTextBox.Text = this.CalculateInterestDue();
                    }

                    ////else default value   

                    this.TaxDueTextBox.LockKeyPress = false;
                    receiptTotal = this.TaxDueTextBox.DecimalTextBoxValue + this.InterestDueTextBox.DecimalTextBoxValue;
                    this.ReceiptTotalTextBox.Text = receiptTotal.ToString();
                    break;
            }
        }

        /// <summary>
        /// Handles the Validating event of the InterestDateTextBox/receiptdatetextbox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ReceiptInterestDateTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    TerraScanTextBox sourceTextBox = sender as TerraScanTextBox;

                    ////change background color with today
                    this.ChangeDateBackGround(sourceTextBox);

                    if (sourceTextBox.Equals(this.ReceiptDateTextBox) && this.receiptDateChanged)
                    {
                        ////change the text box value with today and close time
                        this.ReceiptDateTextBox.Text = this.GetNextWorkingDay(this.ReceiptDateTextBox.DateTextBoxValue);

                        this.receiptDateChanged = false;
                    }

                    if (sourceTextBox.Equals(this.InterestDateTextBox) && this.interestDateChanged && !this.paymentOption.Equals(PaymentOptionTypes.Partial))
                    {
                        ////calculating related values
                        if (this.paymentOption.Equals(PaymentOptionTypes.Balance))
                        {
                            this.CalculateDueAmount(false, true);
                        }
                        else
                        {
                            this.CalculateDueAmount(true, true);
                        }

                        this.CalculateBalance();

                        this.interestDateChanged = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the InterestDateTextBox/receiptdatetextbox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptInterestDateTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    TerraScanTextBox sourceTextBox = sender as TerraScanTextBox;
                    if (sourceTextBox.Equals(this.ReceiptDateTextBox))
                    {
                        this.receiptDateChanged = true;
                    }

                    if (sourceTextBox.Equals(this.InterestDateTextBox) && !this.paymentOption.Equals(PaymentOptionTypes.Partial))
                    {
                        this.interestDateChanged = true;
                        this.interestDueIsEdit = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the axDueTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TaxDueTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //// changes the interestdue textbox value when tax due value changes
                if (!this.paymentOption.Equals(PaymentOptionTypes.Partial) && !this.interestDueIsEdit)
                {
                    ////this.InterestDueTextBox.Text = Decimal.Zero.ToString();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the ReceiptTotalTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptTotalTextBox_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                ////check for save button enable
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    if (this.ReceiptTotalTextBox.DecimalTextBoxValue > 0)
                    {
                        this.operationSmartPart.SaveButtonEnable = true;
                    }
                    else
                    {
                        this.operationSmartPart.SaveButtonEnable = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the InterestDueTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void InterestDueTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    TerraScanTextBox sourceTextBox = sender as TerraScanTextBox;
                    CheckAmountDue(sourceTextBox);
                    ////calculating related values
                    this.CalculateDueAmount(false, false);
                    this.CalculateBalance();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the TaxDueTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void TaxDueTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New) && !this.paymentOption.Equals(PaymentOptionTypes.Partial))
                {
                    TerraScanTextBox sourceTextBox = sender as TerraScanTextBox;

                    if (CheckAmountDue(sourceTextBox))
                    {
                        if (this.StatementBalanceTextBox.DecimalTextBoxValue < this.TaxDueTextBox.DecimalTextBoxValue)
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("CompareTaxDue"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            sourceTextBox.Text = "0.00";
                            sourceTextBox.Focus();
                        }
                    }

                    ////calculating related values

                    this.CalculateDueAmount(false, true);
                    this.CalculateBalance();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Header Details

        /// <summary>
        /// Displays the header details.
        /// </summary>
        private void DisplayHeaderDetails()
        {
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
            {
                ////Assigning Default Value.
                this.ReceiptNumberTextBox.Text = string.Empty;
                ////assign default date to the this.ReceiptDateTextBox

                // TSCO 2803  General Receipting - Default Interest/Receipt Dates now global
                this.ReceiptDateTextBox.Text = TerraScanCommon.ReceiptDate.ToShortDateString();
                this.InterestDateTextBox.Text = TerraScanCommon.InterestDate.ToShortDateString();

                //this.ReceiptDateTextBox.Text = this.GetNextWorkingDay(this.receiptDate);
                ////if (!string.IsNullOrEmpty(this.receiptDate))
                ////{
                ////   //this.receiptDate = DateTime.Parse(this.ReceiptDateTextBox.Text);
                ////    this.ReceiptDateTextBox.Text = this.receiptDate.ToString();
                ////}
                ////else
                ////{
                ////    this.ReceiptDateTextBox.Text = DateTime.Today.ToString();
                ////    this.receiptDate = DateTime.Parse(this.ReceiptDateTextBox.Text);
                ////}6
                this.ReceivedbyTerraScanTextBox.Text = TerraScanCommon.UserName;
                //this.InterestDateTextBox.Text = this.ReceiptDateTextBox.Text;
                this.InterestDueTextBox.Text = "0.00";
                this.TaxDueTextBox.Text = "0.00";
                //// Sets Status Flag
                this.SetStatusButtonsProperty(0, 0);
                this.ReceiptTypeComboBox.SelectedIndex = 0; 
            }
            else
            {
                this.ReceiptNumberTextBox.Text = this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.ReceiptNumberColumn].ToString();
                this.ReceivedbyTerraScanTextBox.Text = this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.UserNameColumn].ToString();
                this.ReceiptDateTextBox.Text = DateTime.Parse(this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.ReceiptDateColumn].ToString()).ToString(this.dateFormat);
                this.InterestDateTextBox.Text = DateTime.Parse(this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.InterestDateColumn].ToString()).ToString(this.dateFormat);
                this.DisplayAmountDueDetails(this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.TotalAmountColumn].ToString());
                //// Sets Status Flag
                if (!String.IsNullOrEmpty(this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.PPaymentIDColumn].ToString()))
                {
                    int postid = 0;
                    if (!string.IsNullOrEmpty(this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.PostIDColumn].ToString()))
                    {
                        postid = Convert.ToInt32(this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.PostIDColumn]);
                    }

                    this.SetStatusButtonsProperty(Convert.ToInt32(this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.PPaymentIDColumn]), postid);
                }
                else
                {
                    this.SetStatusButtonsProperty(0, 0);
                }

                if (this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.ReceiptTypeIDColumn] != null
                    && !string.IsNullOrEmpty(this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.ReceiptTypeIDColumn].ToString()))
                {
                    this.ReceiptTypeComboBox.SelectedValue = this.receiptEngine.GetReceiptDetails.Rows[0][this.receiptEngine.GetReceiptDetails.ReceiptTypeIDColumn];
                }
                else
                {
                    this.ReceiptTypeComboBox.SelectedIndex = 0;
                }
            }

            ////change interestdatetext box color
            this.ChangeDateBackGround(this.InterestDateTextBox);
            ////change receiptdatetext box color
            this.ChangeDateBackGround(this.ReceiptDateTextBox);
        }

        #endregion

        #region Date Related Calender Controls Events

        /// <summary>
        /// Shows the interest date calender.
        /// </summary>
        private void ShowInterestDateCalender()
        {
            this.ReceiptMonthCalender.Visible = true;

            // Set the calendar to move one month at a time when navigating using the arrows.
            this.ReceiptMonthCalender.ScrollChange = 1;

            //// Set the calendar location.
            this.ReceiptMonthCalender.Left = this.InterestDatePanel.Left + this.InterestDateCalenderButton.Left + this.InterestDateCalenderButton.Width;
            this.ReceiptMonthCalender.Top = this.InterestDatePanel.Top + this.InterestDateCalenderButton.Top;
            this.ReceiptMonthCalender.Tag = this.InterestDateCalenderButton.Tag;
            this.ReceiptMonthCalender.Focus();
            if (!string.IsNullOrEmpty(this.InterestDateTextBox.Text))
            {
                this.ReceiptMonthCalender.SetDate(this.InterestDateTextBox.DateTextBoxValue);
            }
        }

        /// <summary>
        /// Shows the receipt date calender.
        /// </summary>
        private void ShowReceiptDateCalender()
        {
            this.ReceiptMonthCalender.Visible = true;

            // Set the calendar to move one month at a time when navigating using the arrows.
            this.ReceiptMonthCalender.ScrollChange = 1;

            // Set the calendar location.
            this.ReceiptMonthCalender.Left = this.ReceiptDatePanel.Left + this.ReceiptDateCalenderButton.Left + this.ReceiptDateCalenderButton.Width;
            this.ReceiptMonthCalender.Top = this.ReceiptDatePanel.Top + this.ReceiptDateCalenderButton.Top;
            this.ReceiptMonthCalender.Tag = this.ReceiptDateCalenderButton.Tag;
            this.ReceiptMonthCalender.Focus();
            if (!string.IsNullOrEmpty(this.ReceiptDateTextBox.Text))
            {
                this.ReceiptMonthCalender.SetDate(this.ReceiptDateTextBox.DateTextBoxValue);
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
                ////assign date to the receiptDate and textbox
                this.ReceiptDateTextBox.Text = this.GetNextWorkingDay(selectedDate);
                ////change receiptdatetext box color
                this.ChangeDateBackGround(this.ReceiptDateTextBox);
                this.ReceiptDateCalenderButton.Focus();
                this.ReceiptMonthCalender.Visible = false;
            }
            else if (String.Compare(this.ReceiptMonthCalender.Tag.ToString(), this.InterestDateTextBox.Name, true) == 0)
            {
                this.InterestDateTextBox.Text = selectedDate.ToString(this.dateFormat);
                // this.interestDueIsEdit = false;
                this.ReceiptInterestDateTextBox_Validating(this.InterestDateTextBox, new CancelEventArgs());
                this.InterestDateCalenderButton.Focus();
                this.ReceiptMonthCalender.Visible = false;
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the ReceiptMonthCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void ReceiptMonthCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.SetSeletedDate(e.Start);
                ////this.tempInterestDue = Convert.ToDouble(this.InterestDueTextBox.Text.ToString().Trim());
                double.TryParse(this.InterestDueTextBox.Text.Trim(), out this.tempInterestDue);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the ReceiptMonthCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptMonthCalender_Leave(object sender, EventArgs e)
        {
            this.ReceiptMonthCalender.Visible = false;
        }

        /// <summary>
        /// Handles the KeyDown event of the ReceiptMonthCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ReceiptMonthCalender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.SetSeletedDate(this.ReceiptMonthCalender.SelectionStart);
                    ////this.tempInterestDue = Convert.ToDouble(this.InterestDueTextBox.Text.ToString().Trim());
                    double.TryParse(this.InterestDueTextBox.Text.Trim(), out this.tempInterestDue);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseDown event of the ReceiptMonthCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void ReceiptMonthCalender_MouseDown(object sender, MouseEventArgs e)
        {
            ////if (this.ReceiptMonthCalender.HitTest(e.X, e.Y).HitArea.Equals(MonthCalendar.HitArea.Date))
            ////{
            ////    DateTime selectedDate = this.ReceiptMonthCalender.HitTest(e.X, e.Y).Time;
            ////    this.ReceiptMonthCalender.SetDate(selectedDate);
            ////}
        }

        /// <summary>
        /// Handles the Click event of the ReceiptDateCalenderButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptDateCalenderButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowReceiptDateCalender();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the InterestDateCalenderButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InterestDateCalenderButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowInterestDateCalender();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region New Receipt

        /// <summary>
        /// Handles the Click event of the NewReceiptButton control.
        /// </summary>
        private void NewReceiptButton_Click()
        {
            this.NewReceiptEngineMode(false);
            ////this.tempInterestDue = Convert.ToDouble(this.InterestDueTextBox.Text.ToString().Trim()); 
            double.TryParse(this.InterestDueTextBox.Text.Trim(), out this.tempInterestDue);
            ////focus - checknum field of first row
            this.PaymentItemsGridView.CurrentCell = this.PaymentItemsGridView[this.PaymentItemsGridView.Columns[this.receiptEngine.PaymentItems.TenderTypeColumn.ColumnName].Index, 0];

            #region Issue Fixed BugID:1684
            ////Query Engine gets loaded even when Cancel button is clicked in Save Prompt message

            this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));

            #endregion
        }

        /// <summary>
        /// News the receipt engine mode.
        /// </summary>
        /// <param name="fromCalculator">if set to <c>true</c> [from calculator].</param>
        private void NewReceiptEngineMode(bool fromCalculator)
        {
            this.Cursor = Cursors.WaitCursor;
            ////setting the pagemode and prevreceipt id
            this.PageMode = TerraScanCommon.PageModeTypes.New;
            this.CurrentReceiptId = null;
            this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);

            ////display default header and payment details for new receipt
            this.DisplayHeaderDetails();

            if (!fromCalculator)
            {
                this.CalculateDueAmount(true, true);
                this.PopulatePaymentGrid(true);
            }

            ////set default controls property for new receipt            
            this.SetButtonsProperty();
            this.SetAdditionalOperationCount(false);
            this.Cursor = Cursors.Default;
            this.TaxDueTextBox.Enabled = true;

            this.ReceiptHistoryGridView.CurrentCell = null;
            this.AuditLinkLabel.Text = SharedFunctions.GetResourceString("ReceiptAuditLink");

            ////Used to get the statement id
            this.currentStatementId = 0;
            this.AuditLinkLabel.Enabled = false;


        }

        #endregion

        #region Save Engine

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        private void SaveButton_Click()
        {
            ////Khaja added code too fix the bug #4704(2) 
            this.ReceiptDateTextBox.Text = this.GetNextWorkingDay(this.ReceiptDateTextBox.DateTextBoxValue);
            this.InterestDateTextBox.Text = this.GetNextWorkingDay(this.InterestDateTextBox.DateTextBoxValue);

            //// Form F9025 Validation for validating the user as Administrator or not
            if (string.IsNullOrEmpty(this.InterestDueTextBox.Text.ToString().Trim()))
            {
                this.InterestDueTextBox.Text = "0.00";
            }
            ////Khaja added code to fix the bug #4704(1)
            else
            {
                double tempIntDue;
                double.TryParse(this.InterestDueTextBox.Text.Trim(), out tempIntDue);
                this.InterestDueTextBox.Text = tempIntDue.ToString();
            }

            if (((this.tempInterestDue != Convert.ToDouble(this.InterestDueTextBox.DecimalTextBoxValue)) && (this.loginUserValidation.Equals(true))) && (TerraScanCommon.Administrator != true))
            {
                bool value = TerraScanCommon.AdminUserValidationForm(this.form15020Control.WorkItem);
                if (value.Equals(true))
                {
                    bool flagSaved = this.SaveReceipt(false);
                    if (flagSaved)
                    {
                        this.FormSlice_SetViewMode(this, new DataEventArgs<int>(this.masterFormNo));
                        int validatedId = this.form15020Control.WorkItem.F9025SaveValidationDetails(15020, TerraScanCommon.ValidationUserId, this.receiptId);
                    }
                }
            }
            else
            {
                bool flagSaved = this.SaveReceipt(false);
               
                if (flagSaved)
                {
                   
                   this.FormSlice_SetViewMode(this, new DataEventArgs<int>(this.masterFormNo));
                }
            }

            this.interestDueIsEdit = false;
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
                DateTime outReceiptDate;
                decimal taxDueAmount;
                decimal totalAmount;
                ////default value
                int ppaymentId = 0;
                int ownerId = 0;
                DataView tempDataView = new DataView();

                ////required field validation
                if (String.IsNullOrEmpty(this.ReceiptDateTextBox.Text.Trim()))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.ReceiptDateTextBox.Focus();
                    return false;
                }

                if (String.IsNullOrEmpty(this.InterestDateTextBox.Text.Trim()))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.InterestDateTextBox.Focus();
                    return false;
                }

                this.Cursor = Cursors.WaitCursor;
                ////checks for valid taxdue and finding save/batch
                taxDueAmount = this.TaxDueTextBox.DecimalTextBoxValue;
                if (!String.IsNullOrEmpty(this.StatementBalanceTextBox.Text) && taxDueAmount >= 0)
                {
                    if (taxDueAmount < this.statementFees)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("CompareFees"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (this.StatementBalanceTextBox.DecimalTextBoxValue < taxDueAmount)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("CompareTaxDue"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (!String.IsNullOrEmpty(this.BalanceTextBox.Text))
                    {
                        if (this.BalanceTextBox.DecimalTextBoxValue != 0)
                        {
                            if (MessageBox.Show(SharedFunctions.GetResourceString("BatchSave"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", "Unpaid Receipt(s)"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return false;
                            }
                        }
                        else  ////getting paymentitems into xmlstring
                        {
                            this.receiptEngine.PaymentItems.AcceptChanges();
                            tempDataView = new DataView(this.receiptEngine.PaymentItems.Copy(), string.Concat(this.receiptEngine.PaymentItems.TenderTypeIDColumn.ColumnName, " IS NOT NULL AND ", this.receiptEngine.PaymentItems.AmountColumn.ColumnName, " <> '0.00'"), "", DataViewRowState.CurrentRows);
                            DataTable tempDataTable = tempDataView.ToTable();
                            ////get ownerid
                            if (this.ReceiptHistoryGridView.OriginalRowCount > 0)
                            {
                                ownerId = Convert.ToInt32(this.ReceiptHistoryGridView.Rows[0].Cells["OwnerID"].Value);
                            }

                            ////create payment and get payment id
                            ppaymentId = this.form15020Control.WorkItem.F1018_SavePayment(ppaymentId, Utility.GetXmlString(tempDataTable), TerraScanCommon.UserId, ownerId);
                            tempDataView = tempDataTable.DefaultView;
                            tempDataView.RowFilter = "TenderTypeID = 2";
                        }
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("InvalidTaxDue"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                outReceiptDate = this.ReceiptDateTextBox.DateTextBoxValue;
                ////checks for valid receipt
                string validResult = this.form15020Control.WorkItem.F1009_GetValidReceiptTest(this.keyId, outReceiptDate);
                if (!String.IsNullOrEmpty(validResult))
                {
                    MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("InvalidReceiptHeader"), validResult), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("InvalidReceipt")), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Cursor = Cursors.Default;
                    return false;
                }

                totalAmount = this.ReceiptTotalTextBox.DecimalTextBoxValue;
                ////save receipt
                if (totalAmount > 0)
                {
                    this.receiptEngine.SaveReceipt.Rows.Clear();
                    F15020ReceiptEngineData.SaveReceiptRow dr = this.receiptEngine.SaveReceipt.NewSaveReceiptRow();
                    dr.ReceiptDate = outReceiptDate.ToString();
                    this.receiptDate = outReceiptDate;
                    D9030.F9030.lastreceiptdate = outReceiptDate.ToString();
                    dr.UserID = TerraScanCommon.UserId;
                    dr.InterestDate = this.InterestDateTextBox.DateTextBoxValue.ToString();
                    dr.InterestAmount = this.InterestDueTextBox.DecimalTextBoxValue;
                    dr.TotalAmount = totalAmount;
                    dr.PPaymentID = ppaymentId;
                    try
                    {
                        if (this.ReceiptTypeComboBox.SelectedValue != null)
                        {
                            int receiptType = 0;
                            int.TryParse(this.ReceiptTypeComboBox.SelectedValue.ToString(), out receiptType);
                            dr.ReceiptTypeID = receiptType;
                           //dr.ReceiptTypeID = (int)this.ReceiptTypeComboBox.SelectedValue.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    this.receiptEngine.SaveReceipt.Rows.Add(dr);

                    this.receiptEngine.SaveReceiptResult.Clear();
                    //// receipt soureceid - 1 for receipt creation
                    int savedReceiptId = this.form15020Control.WorkItem.F1405_SaveMasterReceipt(this.keyId, 1, Utility.GetXmlString(this.receiptEngine.SaveReceipt.Copy()), null);
                    this.receiptId = savedReceiptId;
                    ////this.pageMode = TerraScanCommon.PageModeTypes.View;
                    ////this.operationSmartPart.NewButtonEnable = false;
                    ////receipt saved
                    if (savedReceiptId > 0)
                    {
                        // TSCO 2803  General Receipting - Default Interest/Receipt Dates now global
                        if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                        {
                            TerraScanCommon.InterestDate = this.InterestDateTextBox.DateTextBoxValue;
                            TerraScanCommon.ReceiptDate = this.ReceiptDateTextBox.DateTextBoxValue;
                        }

                        ////get autoprint status
                        this.AutoPrintOnButton.EnableAutoPrint = Convert.ToBoolean(this.form15020Control.WorkItem.GetAutoPrintStatus(this.ParentFormId, TerraScanCommon.UserId));
                        if (this.AutoPrintOnButton.EnableAutoPrint)
                        {
                            Hashtable reportOptionalParameter = new Hashtable();
                            reportOptionalParameter.Add("ReceiptID", savedReceiptId);
                            ////changed the parameter type from string to int
                            this.getAutoPrintOnValue = this.form15020Control.WorkItem.GetConfigDetails("TR_AutoprintOn");
                            if (this.getAutoPrintOnValue.GetCommentsConfigDetails.Rows.Count > 0)
                            {
                                this.autoprintonoff = bool.Parse(this.getAutoPrintOnValue.GetCommentsConfigDetails[0][this.getAutoPrintOnValue.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString());
                            }

                            if (this.autoprintonoff)
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

                        ////saved receiptid is sent to the batch button control(on batch Button run mode, saved recipt id is added in the snapshot collection)
                        ////added by vijayakumar as per client request for Dec 2007
                        this.batchButtonSmartPart.CurrentreceiptId = savedReceiptId;

                        if (this.ReceiptTypeComboBox.SelectedValue != null)
                        {
                            this.ShowReceiptForm();
                        }

                        ////alert for refund
                        if (tempDataView.Count > 0)
                        {
                            if (MessageBox.Show(SharedFunctions.GetResourceString("RefundNow"), String.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("RefundTitle")), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                ////refund management form
                                FormInfo formInfo = TerraScanCommon.GetFormInfo(1214);
                                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("InvalidReceiptTotal"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Cursor = Cursors.Default;
                    return false;
                }

                if (onclose)
                {
                    return true;
                }

                ////populate historygrid with the newly saved receipt           
                this.PopulateHistoryGrid();

                if (this.StatementBalanceTextBox.DecimalTextBoxValue <= 0)
                {
                    this.F1430CalculatorLinkLabel.Enabled = false;
                }
                else
                {
                    this.F1430CalculatorLinkLabel.Enabled = true;
                }

                return true;
            }
            catch (SoapException)
            {
                ////ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return false;
            }
            catch (Exception)
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
        /// Handles the Click event of the CancelReceiptButton control.
        /// </summary>
        private void CancelReceiptButton_Click()
        {
            this.Cursor = Cursors.WaitCursor;
            this.cancelflag = true;
            this.PerformCancel();
            this.Cursor = Cursors.Default;
            this.cancelflag = false;
            this.FocusRequiredInputField();
            this.interestDueIsEdit = false;
        }

        /// <summary>
        /// Performs the cancel.
        /// </summary>
        private void PerformCancel()
        {
            this.PageMode = TerraScanCommon.PageModeTypes.View;
            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            int recCount = this.ReceiptHistoryGridView.OriginalRowCount;
            this.FormSlice_SetViewMode(this, new DataEventArgs<int>(this.masterFormNo));
            if (recCount > 1)
            {
                if (this.ReceiptHistoryGridView.CurrentCell == null || this.ReceiptHistoryGridView.CurrentCell.RowIndex <= 0)
                {
                    this.ReceiptHistoryGridView.Focus();
                    TerraScanCommon.SetDataGridViewPosition(this.ReceiptHistoryGridView, recCount - 1);
                }
                else
                {
                    this.PopulatePayment(this.ReceiptHistoryGridView.CurrentRowIndex, true);
                }
            }
            else
            {
                this.ClearReceiptDetails();
            }

            if (this.StatementBalanceTextBox.DecimalTextBoxValue <= 0)
            {
                this.F1430CalculatorLinkLabel.Enabled = false;
            }
            else
            {
                this.F1430CalculatorLinkLabel.Enabled = true;
            }
        }

        /// <summary>
        /// Clears the receipt details.
        /// </summary>
        private void ClearReceiptDetails()
        {
            ////setting default values
            this.PageMode = TerraScanCommon.PageModeTypes.View;
            this.CurrentReceiptId = null;
            this.receiptEngine.GetReceiptDetails.Clear();
            this.PaymentItemsGridView.CurrentCell = null;
            this.receiptEngine.PaymentItems.Clear();

            ////clearing receipt header
            this.ReceiptNumberTextBox.Text = String.Empty;
            this.ReceiptDateTextBox.Text = String.Empty;
            this.ReceivedbyTerraScanTextBox.Text = String.Empty;
            this.InterestDateTextBox.Text = String.Empty;
            ////change interestdatetext box color
            this.ChangeDateBackGround(this.InterestDateTextBox);
            ////change receiptdatetext box color
            this.ChangeDateBackGround(this.ReceiptDateTextBox);

            ////clearing amount due fields           
            this.DisplayAmountDueDetails(String.Empty);
            this.PaymentTotalTextBox.Text = String.Empty;
            this.BalanceTextBox.Text = String.Empty;
            this.AuditLinkLabel.Text = SharedFunctions.GetResourceString("ReceiptAuditLink");

            ////Used to get the statement id
            this.currentStatementId = 0;
            this.AuditLinkLabel.Enabled = false;

            ////clearing payments grid
            this.PopulatePaymentGrid(true);
            this.SetButtonsProperty();
            this.SetAdditionalOperationCount(false);
            //// Sets Status Flag
            this.SetStatusButtonsProperty(0, 0);

            if (this.ReceiptHistoryGridView.OriginalRowCount == 0)
            {
                this.StatementBalanceTextBox.Text = Decimal.Zero.ToString();
                this.ReceiptEnginePanel.Enabled = false;
                ////reset autoprint
                this.AutoPrintOnButton.EnableAutoPrint = false;
            }
            else
            {
                this.ReceiptEnginePanel.Enabled = true;
            }
        }

        #endregion

        #region User Defined Methods

        /// <summary>
        /// Changes the date back ground with today.
        /// </summary>
        /// <param name="sourceTextBox">The source text box.</param>
        private void ChangeDateBackGround(TerraScanTextBox sourceTextBox)
        {
            ////change background color to red if date is not today
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View) || string.IsNullOrEmpty(sourceTextBox.Text) || sourceTextBox.DateTextBoxValue.Equals(System.DateTime.Today))
            {
                sourceTextBox.Parent.BackColor = Color.White;
                sourceTextBox.BackColor = Color.White;
            }
            else
            {
                sourceTextBox.Parent.BackColor = Color.FromArgb(238, 210, 211);
                sourceTextBox.BackColor = Color.FromArgb(238, 210, 211);
            }
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <param name="onclose">if set to <c>true</c> [on close].</param>
        /// <returns>true - for continuing/false - leave unsaved changes</returns>
        private bool CheckPageStatus(bool onclose)
        {
            DialogResult dialogResult;

            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
            {
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", SharedFunctions.GetResourceString("RealPropertyName"), "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    bool flagSaved = this.SaveReceipt(onclose);
                    if (flagSaved)
                    {
                        this.FormSlice_SetViewMode(this, new DataEventArgs<int>(this.masterFormNo));
                    }

                    return flagSaved;
                }
                else if (dialogResult == DialogResult.No)
                {
                    if (!onclose)
                    {
                        this.D9030_F9030_CancelSliceInformation(this, new DataEventArgs<int>(this.keyId));
                        ////this.PerformCancel();
                    }

                    return true;
                }
                else
                {
                    this.D9030_F9030_CancelSliceInformation(this, new DataEventArgs<int>(this.keyId));
                    ////this.FormSlice_SetViewMode(this, new DataEventArgs<int>(this.masterFormNo));
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the tender type configuration.
        /// </summary>
        private void GetTenderTypeConfiguration()
        {
            PaymentEngineData.GetTenderTypeConfigurationDataTable tendertypeDataTable = this.form15020Control.WorkItem.GetTenderTypeConfiguration().GetTenderTypeConfiguration;
            if (tendertypeDataTable.Rows.Count > 0)
            {
                //// Fixed for BugID 5198 -- Ramya.D
                decimal.TryParse(tendertypeDataTable.Rows[0]["OverUnderMaxAmount"].ToString(), out this.overUnderMaxAmount);
                decimal.TryParse(tendertypeDataTable.Rows[0]["OverUnderMinAmount"].ToString(), out this.overUnderMinAmount);
                ///this.overUnderMaxAmount = Convert.ToDecimal(tendertypeDataTable.Rows[0]["OverUnderMaxAmount"]);
                ///this.overUnderMinAmount = Convert.ToDecimal(tendertypeDataTable.Rows[0]["OverUnderMinAmount"]);
            }
        }

        /// <summary>
        /// set focus to the next/previous input field
        /// </summary>
        private void FocusRequiredInputField()
        {
            if (this.ReceiptHistoryGridView.OriginalRowCount > 1)
            {
                this.pageLoadStatus = false;
                this.ReceiptHistoryGridView.Focus();
                this.pageLoadStatus = true;
            }
        }

        /// <summary>
        /// Receipts the history grid bind columns.
        /// </summary>
        private void CustomizeReceiptHistoryGridView()
        {
            this.ReceiptHistoryGridView.AutoGenerateColumns = false;
            this.ReceiptHistoryGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewColumnCollection columns = this.ReceiptHistoryGridView.Columns;
            columns["Date"].DataPropertyName = "Date";
            columns["Item"].DataPropertyName = "Item";
            columns["Number"].DataPropertyName = "Number";
            columns["Fee"].DataPropertyName = "Fee";
            columns["Tax"].DataPropertyName = "Tax";
            columns["Balance"].DataPropertyName = "Balance";
            columns["Interest"].DataPropertyName = "Interest";
            columns["ID"].DataPropertyName = "ID";
            columns["OwnerID"].DataPropertyName = "OwnerID";
        }

        /// <summary>
        /// This Method used to  set dataproperty name and column displayindex and paymentsdatatable initialization
        /// CustomizeErrorGridView
        /// </summary>
        private void CustomizePaymentItemsGridView()
        {
            this.PaymentItemsGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection columns = this.PaymentItemsGridView.Columns;

            columns["TenderType"].DataPropertyName = "TenderType";
            columns["PaidBy"].DataPropertyName = "PaidBy";
            columns["CheckNumber"].DataPropertyName = "CheckNumber";
            columns["Amount"].DataPropertyName = "Amount";
            columns["PaymentID"].DataPropertyName = "PaymentID";
            columns["PPaymentID"].DataPropertyName = "PPaymentID";
            columns["Address1"].DataPropertyName = "Address1";
            columns["Address2"].DataPropertyName = "Address2";
            columns["City"].DataPropertyName = "City";
            columns["State"].DataPropertyName = "State";
            columns["Zip"].DataPropertyName = "Zip";
            columns["Comment"].DataPropertyName = "Comment";

            columns["TenderType"].DisplayIndex = 0;
            columns["PaidBy"].DisplayIndex = 1;
            columns["PaidByImage"].DisplayIndex = 2;
            columns["CheckNumber"].DisplayIndex = 3;
            columns["Amount"].DisplayIndex = 4;
            columns["PaymentID"].DisplayIndex = 5;
            columns["PPaymentID"].DisplayIndex = 6;
            columns["Address1"].DisplayIndex = 7;
            columns["Address2"].DisplayIndex = 8;
            columns["City"].DisplayIndex = 9;
            columns["State"].DisplayIndex = 10;
            columns["Zip"].DisplayIndex = 11;
            columns["Comment"].DisplayIndex = 12;

            this.PaymentItemsGridView.DataSource = this.receiptEngine.PaymentItems.DefaultView;

            DataTable tenderTypeDataTable = this.form15020Control.WorkItem.F1018_ListTenderType(true);
            DataTable tempDataTable = new DataTable();
            tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("TenderType"), new DataColumn("TenderTypeID") });
            tempDataTable.Rows.Add(new object[] { "", "" });
            for (int i = 0; i < tenderTypeDataTable.Rows.Count; i++)
            {
                tempDataTable.Rows.Add(new object[] { tenderTypeDataTable.Rows[i]["TenderType"].ToString(), tenderTypeDataTable.Rows[i]["TenderTypeID"].ToString() });
            }

            (this.PaymentItemsGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).DataSource = tempDataTable;
            (this.PaymentItemsGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).DisplayMember = "TenderType";
            (this.PaymentItemsGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).ValueMember = "TenderTypeID";
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
                    additionalOperationCountEntity.AttachmentCount = this.form15020Control.WorkItem.GetAttachmentCount(this.ParentFormId, this.currentReceiptId.Value, TerraScanCommon.UserId);
                    CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.form15020Control.WorkItem.GetCommentsCount(this.ParentFormId, this.currentReceiptId.Value, TerraScanCommon.UserId);
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
        /// sets status buttons color with PPaymentId
        /// </summary>
        /// <param name="ppaymentId">balance-zero represents paid else batched receipt</param>
        /// <param name="postId">The post id.</param>
        private void SetStatusButtonsProperty(int ppaymentId, int postId)
        {
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
                this.PaidButton.StatusIndicator = true;

                if (postId != 0)
                {
                    this.NotPostedButton.StatusIndicator = true;
                }
                else
                {
                    this.NotPostedButton.StatusIndicator = false;
                }
            }
            else
            {
                this.PaidButton.StatusIndicator = false;
                this.NotPostedButton.StatusIndicator = false;
            }

            // Code removed as per specification from client
            ////if (this.PaidButton.StatusIndicator)
            ////{
            ////    this.operationSmartPart.SaveButtonText = SharedFunctions.GetResourceString("SavePaid");
            ////}
            ////else
            ////{
            ////    this.operationSmartPart.SaveButtonText = SharedFunctions.GetResourceString("SaveUNPaid");
            ////}
        }

        /// <summary>
        /// enbling/disabling buttons
        /// </summary>
        private void SetButtonsProperty()
        {
            ////setting controls property depending on pagemode

            switch (this.pageMode)
            {
                case TerraScanCommon.PageModeTypes.New:
                    this.SelectPaymentPanel.Enabled = true;
                    this.ReceiptDateCalenderButton.Enabled = true;
                    this.InterestDateCalenderButton.Enabled = true;
                    this.InterestDateTextBox.LockKeyPress = false;
                    this.ReceiptDateTextBox.LockKeyPress = false;
                    this.TaxDueTextBox.LockKeyPress = false;
                    this.InterestDueTextBox.LockKeyPress = false;
                    this.AddtionalOperationDeckWorkspace.Enabled = false;
                    if (this.ReceiptTotalTextBox.DecimalTextBoxValue > 0)
                    {
                        this.operationSmartPart.SaveButtonEnable = true;
                    }
                    else
                    {
                        this.operationSmartPart.SaveButtonEnable = false;
                    }

                    this.SetReadOnlyToGrid(false);

                    this.PaymentItemsGridView.TabStop = true;
                    this.ReceiptHistoryGridView.TabStop = false;

                    ////when new receipt on progress disable the calculator form
                    this.F1430CalculatorLinkLabel.Enabled = false;

                    this.ReceiptTypePanel.Enabled = true;

                    break;

                default:
                    if (this.StatementBalanceTextBox.DecimalTextBoxValue > 0)
                    {
                        this.operationSmartPart.NewButtonEnable = this.slicePermissionField.newPermission;
                    }
                    else
                    {
                        this.operationSmartPart.NewButtonEnable = false;
                    }

                    this.SelectPaymentPanel.Enabled = false;
                    this.ReceiptDateCalenderButton.Enabled = false;
                    this.InterestDateCalenderButton.Enabled = false;
                    this.InterestDateTextBox.LockKeyPress = true;
                    this.ReceiptDateTextBox.LockKeyPress = true;
                    this.TaxDueTextBox.LockKeyPress = true;
                    this.InterestDueTextBox.LockKeyPress = true;
                    if (this.CurrentReceiptId.HasValue)
                    {
                        this.AddtionalOperationDeckWorkspace.Enabled = true;
                    }
                    else
                    {
                        this.AddtionalOperationDeckWorkspace.Enabled = false;
                    }

                    this.PaymentItemsGridView.TabStop = false;
                    this.ReceiptHistoryGridView.TabStop = true;

                    ////when new receipt on progress disable the calculator form else enable the calculator
                    this.F1430CalculatorLinkLabel.Enabled = true;

                    this.ReceiptTypePanel.Enabled = false;

                    break;
            }
        }

        /// <summary>
        /// Calculatings the payment total.
        /// </summary>        
        private void CalculatePaymentTotal()
        {
            decimal outDecimalValue;
            decimal paymentsTotal = 0;
            int paymentCount = 0;

            for (int counter = 0; counter < this.receiptEngine.PaymentItems.Rows.Count; counter++)
            {
                if (!String.IsNullOrEmpty(this.receiptEngine.PaymentItems.Rows[counter]["TenderType"].ToString()) && Decimal.TryParse(this.receiptEngine.PaymentItems.Rows[counter]["Amount"].ToString(), out outDecimalValue))
                {
                    paymentsTotal += outDecimalValue;
                    if (outDecimalValue != 0)
                    {
                        paymentCount++;
                    }
                }
            }

            this.PaymentTotalTextBox.Text = paymentsTotal.ToString();

            if (paymentCount == this.receiptEngine.PaymentItems.Rows.Count)
            {
                F15020ReceiptEngineData.PaymentItemsRow dr = this.receiptEngine.PaymentItems.NewPaymentItemsRow();
                dr.ModuleID = this.ParentFormId;
                this.receiptEngine.PaymentItems.Rows.Add(dr);
                this.PaymentGridVscrollBar.Visible = false;
                this.PaymentItemsGridView.Refresh();
            }
        }

        #endregion

        #region Section Indicator

        /// <summary>
        /// Handles the Click event of the ReceiptEnginePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptEnginePictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the ReceiptEnginePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptEnginePictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.ReceiptEngineToolTip.SetToolTip(this.ReceiptEnginePictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region ShortCutKeys New/Save Added By Shiva

        /// <summary>
        /// Handles the Click event of the SaveMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveMenu_Click(object sender, EventArgs e)
        {
            if (this.operationSmartPart.SaveButtonEnable && this.operationSmartPart.SaveButtonVisible)
            {
                this.SaveButton_Click();
            }
        }

        /// <summary>
        /// Handles the Click event of the NewMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NewMenu_Click(object sender, EventArgs e)
        {
            if (this.operationSmartPart.NewButtonEnable && this.operationSmartPart.NewButtonVisible)
            {
                this.NewReceiptButton_Click();
            }
        }

        #endregion

        /// <summary>
        /// Handles the LinkClicked event of the F1430CalculatorLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void F1430CalculatorLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (this.keyId > 0)
                {
                    object[] optionalParameter;
                    optionalParameter = new object[] { this.keyId };
                    Form calculatorForm = new Form();
                    calculatorForm = this.form15020Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1430, optionalParameter, this.form15020Control.WorkItem);
                    if (calculatorForm != null)
                    {
                        if (calculatorForm.ShowDialog() == DialogResult.OK)
                        {
                            ////to set to new mode when values are coming for F1430---starts

                            this.NewReceiptEngineMode(true);

                            ////to set to new mode when values are coming for F1430---ends

                            decimal taxAmount, feeAmount, taxDue, interestAmount, penaltyAmount, interestDue;

                            decimal.TryParse(TerraScanCommon.GetValue(calculatorForm, "TaxAmount").ToString(), out taxAmount);
                            decimal.TryParse(TerraScanCommon.GetValue(calculatorForm, "FeeAmount").ToString(), out feeAmount);
                            decimal.TryParse(TerraScanCommon.GetValue(calculatorForm, "InterestAmount").ToString(), out interestAmount);
                            decimal.TryParse(TerraScanCommon.GetValue(calculatorForm, "PenaltyAmount").ToString(), out penaltyAmount);

                            taxDue = taxAmount + feeAmount;
                            this.TaxDueTextBox.Text = taxDue.ToString();

                            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New) && !this.paymentOption.Equals(PaymentOptionTypes.Partial))
                            {
                                if (CheckAmountDue(this.TaxDueTextBox))
                                {
                                    if (this.StatementBalanceTextBox.DecimalTextBoxValue < this.TaxDueTextBox.DecimalTextBoxValue)
                                    {
                                        MessageBox.Show(SharedFunctions.GetResourceString("CompareTaxDue"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        //this.TaxDueTextBox.Text = "0.00";
                                        //this.TaxDueTextBox.Focus();
                                        this.CalculateDueAmount(true, false);
                                    }
                                }

                                ////calculating related values
                                //this.CalculateDueAmount(false, true);
                                //this.CalculateBalance();
                            }

                            interestDue = interestAmount + penaltyAmount;
                            this.InterestDueTextBox.Text = interestDue.ToString();

                            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                            {
                                CheckAmountDue(this.InterestDueTextBox);
                                ////calculating related values
                                //this.CalculateDueAmount(false, false);
                                //this.CalculateBalance();
                            }

                            this.ReceiptTotalTextBox.Text = (this.InterestDueTextBox.DecimalTextBoxValue + this.TaxDueTextBox.DecimalTextBoxValue).ToString();

                            if (!string.IsNullOrEmpty(TerraScanCommon.GetValue(calculatorForm, "InterestTotal").ToString()))
                            {
                                this.PaymentTotalTextBox.Text = TerraScanCommon.GetValue(calculatorForm, "InterestTotal").ToString();
                            }

                            // refresh the payment grid
                            this.PopulatePaymentGrid(true);

                            if (!string.IsNullOrEmpty(TerraScanCommon.GetValue(calculatorForm, "InterestDate").ToString()))
                            {
                                this.InterestDateTextBox.Text = TerraScanCommon.GetValue(calculatorForm, "InterestDate").ToString();

                                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                                {
                                    ////change background color with today
                                    this.ChangeDateBackGround(this.InterestDateTextBox);

                                    //if (this.InterestDateTextBox.Equals(this.InterestDateTextBox) && this.interestDateChanged && !this.paymentOption.Equals(PaymentOptionTypes.Partial))
                                    //{
                                    //    ////calculating related values
                                    //    if (this.paymentOption.Equals(PaymentOptionTypes.Balance))
                                    //    {
                                    //        this.CalculateDueAmount(false, true);
                                    //    }
                                    //    else
                                    //    {
                                    //        this.CalculateDueAmount(true, true);
                                    //    }

                                    //    this.CalculateBalance();

                                    //    this.interestDateChanged = false;
                                    //}
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Enter event of the TaxDueTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TaxDueTextBox_Enter(object sender, EventArgs e)
        {
            ////this.BackColor = Color.White;
            this.ReceiptEnginePanel.BackColor = Color.Silver;
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>Form No</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;
            if (String.IsNullOrEmpty(this.ReceiptDateTextBox.Text.Trim()))
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredMsg");
                sliceValidationFields.RequiredFieldMissing = true;
                this.ReceiptDateTextBox.Focus();
            }
            else if (String.IsNullOrEmpty(this.InterestDateTextBox.Text.Trim()))
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredMsg");
                sliceValidationFields.RequiredFieldMissing = true;
                this.InterestDateTextBox.Focus();
            }
            else
            {
                sliceValidationFields.RequiredFieldMissing = false;
            }

            return sliceValidationFields;
        }

        #endregion

        private void InterestDueTextBox_TextChanged(object sender, EventArgs e)
        {
            this.interestDueIsEdit = true;
        }

        /// <summary>
        /// Handles the Validating event of the ReceiptTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ReceiptTypeComboBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.ReceiptTypeComboBox.SelectedValue == null)
                {
                    this.ReceiptTypeComboBox.SelectedIndex = 0;
                }
                else
                {
                    int selectedValue;
                    int.TryParse(this.ReceiptTypeComboBox.SelectedValue.ToString(), out selectedValue);
                    this.ReceiptTypeComboBox.SelectedIndex = -1;
                    this.ReceiptTypeComboBox.SelectedValue = selectedValue;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Loads the receipt types.
        /// </summary>
        private void LoadReceiptTypes()
        {
            this.receiptTypeTable = this.form15020Control.WorkItem.F15020_GetReceiptTypes(TerraScanCommon.UserId, 1070, this.keyId);
            this.ReceiptTypeComboBox.DataSource = this.receiptTypeTable.ReceiptTypes;
            this.ReceiptTypeComboBox.DisplayMember = this.receiptTypeTable.ReceiptTypes.ReceiptTypeColumn.ColumnName;
            this.ReceiptTypeComboBox.ValueMember = this.receiptTypeTable.ReceiptTypes.ReceiptTypeIDColumn.ColumnName;
            this.ReceiptTypeComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Shows the sub form - F1070.
        /// </summary>
        private void ShowReceiptForm()
        {
            F1070ReceiptTypeData.ReceiptTypesRow selectedRow = this.receiptTypeTable.ReceiptTypes[this.ReceiptTypeComboBox.SelectedIndex];
            if (!selectedRow.IsFormNull() && !string.IsNullOrEmpty(selectedRow.Form.ToString())
                           && selectedRow.Form > 0)
            {
                Form exportForm = new Form();
                int formNumber = selectedRow.Form;
                object[] optionalParameter = new object[] { this.keyId, formNumber, TerraScanCommon.UserId };
                exportForm = this.Form15020Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1070, optionalParameter, this.form15020Control.WorkItem);
                if (exportForm != null)
                {
                    exportForm.ShowDialog();
                }
            }
        }

        private void PaymentItemsGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                for (int i = 0; i < this.PaymentItemsGridView.Rows.Count; i++)
                {


                    // TerraScanTextAndImageCell imgCell = (TerraScanTextAndImageCell)this.PaymentItemsGridView[1, i];
                    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F15020));
                    //imgCell.Image = ((System.Drawing.Image)(resources.GetObject("PaidByImage.Image")));
                    //imgCell.ImagexLocation = 344;
                    //imgCell.ImageyLocation = 1;
                    TerraScanTextAndImageCell imageCell = (TerraScanTextAndImageCell)this.PaymentItemsGridView["PaidByImage", i];
                    imageCell.Image = ((System.Drawing.Image)(resources.GetObject("PaidByImage.Image", CultureInfo.CurrentUICulture))); //Properties.Resources.PaidByImage_Image;
                    imageCell.ImagexLocation = 1;
                    imageCell.ImageyLocation = 1;


                   // imageCell.Style.Padding = new Padding(1, 1, 1, 10);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void PaymentItemsGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //if (!this.nonEditable)
                //{
                if (e.RowIndex >= 0 && e.ColumnIndex.Equals(this.PaymentItemsGridView.Columns["PaidByImage"].Index))
                {
                    if ((e.X >= 2) && (e.X <= (34 - 10)) && (e.Y >= 1) && (e.Y <= (22 - 1)) && !string.IsNullOrEmpty(this.PaymentItemsGridView.Rows[e.RowIndex].Cells["TenderType"].Value.ToString()))//this.PaymentItemsGridView["PaidBy", e.RowIndex].ReadOnly.Equals(false))
                    {
                            
                            DataTable tempTable = new DataTable();
                            string PayeeItemXml;

                            if (tempTable.Columns.Count <= 0)
                            {
                                tempTable.Columns.AddRange(new DataColumn[] { new DataColumn("PaidBy"), new DataColumn("Address1"), new DataColumn("Address2"), new DataColumn("City"), new DataColumn("State"), new DataColumn("Zip"), new DataColumn("Comment") });
                            }
                            ///Column information

                            if (tempTable.Rows.Count.Equals(0))
                            {
                                DataRow tempRow = tempTable.NewRow();
                                tempRow["PaidBy"] = this.PaymentItemsGridView.Rows[e.RowIndex].Cells["PaidBy"].Value.ToString();
                                tempRow["Address1"] = this.PaymentItemsGridView.Rows[e.RowIndex].Cells["Address1"].Value.ToString();
                                tempRow["Address2"] = this.PaymentItemsGridView.Rows[e.RowIndex].Cells["Address2"].Value.ToString();
                                tempRow["City"] = this.PaymentItemsGridView.Rows[e.RowIndex].Cells["City"].Value.ToString();
                                tempRow["State"] = this.PaymentItemsGridView.Rows[e.RowIndex].Cells["State"].Value.ToString();
                                tempRow["Zip"] = this.PaymentItemsGridView.Rows[e.RowIndex].Cells["Zip"].Value.ToString();
                                tempRow["Comment"] = this.PaymentItemsGridView.Rows[e.RowIndex].Cells["Comment"].Value.ToString(); 
                                //tempRow["PaymentID"] = 0; 
                                tempTable.Rows.Add(tempRow);
                            }

                            DataSet tempDataSet = new DataSet("Root");
                            tempDataSet.Tables.Add(tempTable);
                            tempDataSet.Tables[0].TableName = "Table";
                            string payeeIDs = tempDataSet.GetXml();
                            object[] optionalParameter = new object[2];
                            optionalParameter[0] = payeeIDs;
                            optionalParameter[1] = !this.PaymentItemsGridView["PaidBy", e.RowIndex].ReadOnly;

                            Form PayeeDetailsForm = TerraScanCommon.GetForm(1019, optionalParameter, this.form15020Control.WorkItem);
                            if (PayeeDetailsForm != null) ////&& accountSelectionForm.ShowDialog() == DialogResult.Yes)
                            {
                                if (PayeeDetailsForm.ShowDialog() == DialogResult.OK)
                                {
                                    string PayeeInfo = TerraScanCommon.GetValue(PayeeDetailsForm, SharedFunctions.GetResourceString("PayeeID").ToString());
                                    System.IO.StringReader stringReader = new System.IO.StringReader(PayeeInfo);
                                    System.Xml.XmlTextReader textReader = new System.Xml.XmlTextReader(stringReader);
                                    //System.Xml.XmlReader reader = new System.Xml.XmlReader();
                                    DataSet ds = new DataSet();
                                    ds.ReadXml(textReader);

                                    if (ds.Tables.Count > 0)
                                    {
                                        this.PaymentItemsGridView.Rows[e.RowIndex].Cells["PaidBy"].Value = ds.Tables[0].Rows[0][0].ToString();
                                        this.PaymentItemsGridView.Rows[e.RowIndex].Cells["Address1"].Value = ds.Tables[0].Rows[0][1].ToString();
                                        this.PaymentItemsGridView.Rows[e.RowIndex].Cells["Address2"].Value = ds.Tables[0].Rows[0][2].ToString();
                                        this.PaymentItemsGridView.Rows[e.RowIndex].Cells["City"].Value = ds.Tables[0].Rows[0][3].ToString();
                                        this.PaymentItemsGridView.Rows[e.RowIndex].Cells["State"].Value = ds.Tables[0].Rows[0][4].ToString();
                                        this.PaymentItemsGridView.Rows[e.RowIndex].Cells["Zip"].Value = ds.Tables[0].Rows[0][5].ToString();
                                        this.PaymentItemsGridView.Rows[e.RowIndex].Cells["Comment"].Value = ds.Tables[0].Rows[0][6].ToString();
                                    }

                                    this.receiptEngine.PaymentItems.AcceptChanges();
                                    this.PaymentItemsGridView.Rows[e.RowIndex].Cells["PaidBy"].Selected = true;
                                    this.PaymentItemsGridView.CurrentCell = this.PaymentItemsGridView.Rows[e.RowIndex].Cells["PaidBy"];
                                    //this.PaymentItemsGridView.Rows[e.RowIndex].Cells[0].ToString()=      
                                }
                            }
                           // }
                        }

                    }

                //}

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void PaymentItemsGridView_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                int firstColumn = this.PaymentItemsGridView.Columns["PaidBy"].Index;
                int secondColumn = this.PaymentItemsGridView.Columns["PaidByImage"].Index;
                Rectangle r1 = this.PaymentItemsGridView.GetCellDisplayRectangle(firstColumn, -1, true);
                Rectangle r2 = this.PaymentItemsGridView.GetCellDisplayRectangle(secondColumn, -1, true); 
                r1.X += 1;
                r1.Y += 1; 
                r1.Width += r2.Width - 2; 
                r1.Height -= 2;

                // Draw color
                using (SolidBrush br = new SolidBrush(this.PaymentItemsGridView.ColumnHeadersDefaultCellStyle.BackColor))
                {
                    e.Graphics.FillRectangle(br, r1);
                }  
   
                // Draw header text   
                using (SolidBrush br = new SolidBrush(this.PaymentItemsGridView.ColumnHeadersDefaultCellStyle.ForeColor))
                {              
                    StringFormat sf = new StringFormat();
                    // Set X, Y position to display header text
                    r1.X += 160;
                    r1.Y += 4;
                    e.Graphics.DrawString("Paid By", this.PaymentItemsGridView.ColumnHeadersDefaultCellStyle.Font, br, r1, sf);   
                } 
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
    }
}
