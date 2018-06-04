//--------------------------------------------------------------------------------------------
// <copyright file="F15035.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F15035 Suspend Payments Functionality
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09-05-2007       Shiva              Created
// 21/5/2009        Malliga            Modified for the issue 5192 
// 28/6/2011        Manoj Kumar        Modified to set tender Type default 'check'.
// 12 SEP 2011      Manoj Kumar        Removed the tender Type default 'check' TSCO #13410.
//*********************************************************************************/
namespace D11035
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
    
    
   

    /// <summary>
    /// F15035 Slice User InterFace.
    /// </summary>
    [SmartPart]
    public partial class F15035 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// variable holds the form1013Controller.
        /// </summary>
        private F15035Controller form15035Controll;

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
        /// datatable contains the formDetails.
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// PartiesOwnerDetailsData
        /// </summary>
        private PartiesOwnerDetailsData ownerDetailDataSet = new PartiesOwnerDetailsData();

        /// <summary>
        /// variable to Hold the ReceiptId value.
        /// </summary>
        private int receiptId;

        /// <summary>
        /// variable to holds the ownerId value.
        /// </summary>
        private int ownerId = -1;

        /// <summary>
        /// assigining report Number - default value 0
        /// </summary>
        private int reportNumber;

        /// <summary>
        /// variable to Hold the Suspended Payment DataSet.
        /// </summary>
        private F15035SuspendedPaymentsData suspendedPaymentDataSet = new F15035SuspendedPaymentsData();

        /// <summary>
        /// variable Holds the recieptDate value.
        /// </summary>
        private DateTimePicker receiptDate = new DateTimePicker();

        /// <summary>
        /// variable holds the postId.
        /// </summary>
        private int postId = -1;

        /// <summary>
        /// variable holds the status id.
        /// </summary>
        private int statusId = -1;

        ///<summary>
        /// get the BackGroundColor
        /// </summary>
        private string statusBackGroundColor;

        ///<summary>
        ///get the TextColor
        /// </summary>
        private string statusTextColor;

        ///<summary>
        ///get the hyperlinkText 
       /// </summary>
        private string hyperLinkText;

        ///<summary>
        ///get the hyperlinkId
        /// </summary>
        private int hyperlinkId;

        /// <summary>
        /// Set Date Format
        /// </summary>
        private string dateFormat = "M/d/yyyy";

        ///<summary>
        /// Holds the PPaymentID for Load Payment Engine
        /// </summary>
        private int paymentID;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15035"/> class.
        /// </summary>
        public F15035()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15035"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F15035(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.keyId = keyID;
            ////Coding Added for the issue 5192 on 21/5/2009 by malliga
            //// form load it should display configured section indicaot name
            this.SuspendedPaymentsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SuspendedPaymentsPictureBox.Height, this.SuspendedPaymentsPictureBox.Width, tabText, 28, 81, 128);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15035"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        /// <param name="featureClassID">The feature class ID.</param>
        public F15035(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassID)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.keyId = keyID;
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form15035 controll.
        /// </summary>
        /// <value>The form15035 controll.</value>
        [CreateNew]
        public F15035Controller Form15035Controll
        {
            get { return this.form15035Controll as F15035Controller; }
            set { this.form15035Controll = value; }
        }

        /// <summary>
        /// Gets or sets the receipt id.
        /// </summary>
        /// <value>The receipt id.</value>
        public int ReceiptId
        {
            get { return this.receiptId; }
            set { this.receiptId = value; }
        }

        #endregion

        #region Event SubScription

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
                    
                    if(this.suspendedPaymentDataSet.GetSuspendedPayment.Rows.Count > 0)
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
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                if (this.slicePermissionField.newPermission)
                {
                    this.ValidateSliceForm(eventArgs);
                }
            }
            else
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save confirmed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, DataEventArgs<int> eventArgs)
        {
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                {
                    if (this.slicePermissionField.newPermission)
                    {
                        this.SaveSuspendedPaymentReceipt();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
                else
                {
                    this.LockControls(true);
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ enable new method].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            {
                if (this.slicePermissionField.newPermission)
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    this.ClearSuspendedPaymentsHeader();
                    //this.ReceiptDateTextBox.Text = this.receiptDate.Value.Date.ToString("M/d/yyyy");
                    this.LockControls(true);
                    //this.StatusLabel1.BringToFront();
                    this.SetStatusButtonsProperty(0);
                    //this.StatusLabel1.Text = "This payment is Available.";
                    //this.StatusLabel1.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
                    //// Enable Payment Engine.
                    //this.PaymentEngineUserControl.PayeeDetails = null; 
                    this.PaymentEngineUserControl.LoadPayment();
                    this.PaymentEngineUserControl.OwnerName=string.Empty;
                    this.PaymentEngineUserControl.PayeeDetails = null; 
                    //this.PaymentEngineUserControl.PayeeDetails = new PaymentEngineData();
                    this.PaymentEngineUserControl.TabStop = true;
                    this.PaymentEngineUserControl.Locked = false;
                    this.PaymentEngineUserControl.AmountTotal = 0;
                    this.PaymentEngineUserControl.SetDefaultSelection = true;
                    this.OwnerNameSearchButton.Select();
                    this.ownerDetailDataSet.Clear();  
                    this.ReceiptDateTextBox.Text = System.DateTime.Today.ToShortDateString();    
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    this.LockControls(false);
                    ////disable paymentengine
                    this.PaymentEngineUserControl.TabStop = false;
                    this.PaymentEngineUserControl.Locked = true;
                    this.PaymentEngineUserControl.SetDefaultSelection = false;
                }
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            this.Cursor = Cursors.WaitCursor;
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.receiptId = this.keyId;
            this.LoadSuspendedPayment(this.keyId);
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.receiptId = this.keyId;
                    this.LoadSuspendedPayment(this.keyId);
                    ////// Enable Payment Engine.
                    //this.PaymentEngineUserControl.TabStop = false;
                    //this.PaymentEngineUserControl.Locked = true;
                    //this.PaymentEngineUserControl.ApplyReadonlyColumn = false;
                    
                    //this.PaymentEngineUserControl.SetDefaultSelection = false;
                    //this.PaymentEngineUserControl.SetDefaultSelection = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Event Subscription for delete slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            try
            {
                if (this.slicePermissionField.deletePermission && this.postId.Equals(0) && this.statusId.Equals(0))
                {
                    this.form15035Controll.WorkItem.F15035_DeleteSuspendedPayment(this.keyId, TerraScanCommon.UserId);
                }
                else
                {
                    MessageBox.Show("Record can not be deleted because it is applied or refunded.", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Protected Methods

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

        #endregion

        #region Form Events

        /// <summary>
        /// Handles the Load event of the F15035 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F15035_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                ////subcribe PaymentAmountChangeEvent
                this.PaymentEngineUserControl.IsAutoPayment = true; 
                this.PaymentEngineUserControl.ParentWorkItem = this.form15035Controll.WorkItem;
                //this.PaymentEngineUserControl.IsAutoPayment = true;
               this.PaymentEngineUserControl.PaymentAmountChangeEvent += new TerraScan.PaymentEngine.PaymentEngineUserControl.PaymentAmountChangeEventHandler(this.PaymentEngineUserControl_PaymentAmountChangeEvent);
                this.PaymentEngineUserControl.PaymentItemChangeEvent +=new TerraScan.PaymentEngine.PaymentEngineUserControl.PaymentItemChangeEventHandler(PaymentEngineUserControl_PaymentItemChangeEvent); 
                this.LoadSuspendedPayment(this.keyId);
            }
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                //this.PaymentsTotalTextBox.Text = amount.ToString();
                this.PaymentEngineUserControl.TotalDue = amount; 
                this.TotalAmountTextBox.Text = amount.ToString();
                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        /// <summary>
        /// Payments the engine user control_ payment item change event.
        /// </summary>
        private void PaymentEngineUserControl_PaymentItemChangeEvent()
        {
            //this.PaymentEngineUserControl.TotalDue = this.ReceiptTotalTextBox.DecimalTextBoxValue;
            if ( this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows.Count > 0)
            {
                int ownerID;

                this.PaymentEngineUserControl.OwnerName = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0]["Name"].ToString();
                //this.PaymentEngineUserControl.OwnerPayeeID = this.ownerDataTable.Rows[0]["OwnerID"].ToString();
                int.TryParse(this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0]["OwnerID"].ToString(), out ownerID);
                if (this.PaymentEngineUserControl.PayeeDetails == null)
                {
                    if (ownerID > 0)
                    {
                        this.PaymentEngineUserControl.PayeeDetails = this.form15035Controll.WorkItem.F1019_GetPayeeDetails(ownerID);
                    }
                    else
                    {
                        this.PaymentEngineUserControl.PayeeDetails = new PaymentEngineData();
                    }
                }
                // int.TryParse(this.ownerDataTable.Rows[0]["OwnerID"].ToString(), out this.PaymentEngineUserControl.OwnerpayeeID);   
            }
        }

        /// <summary>
        /// Handles the Click event of the OwnerNameSearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OwnerNameSearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                Form masterNameSearchForm = new Form();
                object[] optionalParameter = new object[0];
                masterNameSearchForm = this.form15035Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9101, optionalParameter, this.Form15035Controll.WorkItem);
                if (masterNameSearchForm != null)
                {
                    if (masterNameSearchForm.ShowDialog() == DialogResult.Yes)
                    {
                        this.ownerId = Convert.ToInt32(TerraScanCommon.GetValue(masterNameSearchForm, "MasterNameOwnerId"));
                        this.ownerDetailDataSet = this.form15035Controll.WorkItem.GetOwnerDetails(this.ownerId);
                        if (this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows.Count > 0)
                        {
                            this.SetOwnerControlValues(this.GetSelectedOwnerRow(0));
                        }
                        //Change the tender Type default 'check'.
                        int ownerID;
                        int.TryParse(this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0]["OwnerID"].ToString(), out ownerID);
                        if (ownerID > 0)
                        {
                            this.PaymentEngineUserControl.PayeeDetails = this.form15035Controll.WorkItem.F1019_GetPayeeDetails(ownerID);
                        }
                        else
                        {
                            this.PaymentEngineUserControl.PayeeDetails = new PaymentEngineData();
                        }
                        //Removed the tender Type default 'check' TSCO #13410.
                        //decimal amt=0.00M;
                      //  this.PaymentEngineUserControl.LoadDefultValue(this.PaymentEngineUserControl.OwnerName, amt);
                    }
                    this.NamePanel.Focus();
                    this.OwnerNameSearchButton.Focus();
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

                this.form15035Controll.WorkItem.SaveAutoPrint(this.ParentFormId, TerraScanCommon.UserId, this.AutoPrintOnButton.EnableAutoPrint);
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the OwnerNameLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void OwnerNameLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                if (this.ownerId != -1)
                {
                    formInfo = TerraScanCommon.GetFormInfo(91000);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = this.ownerId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the ReceiptNumberLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ReceiptNumberLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11001);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.keyId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the ReceiptDateLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ReceiptDateLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11001);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.keyId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SuspendedPaymentsPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SuspendedPaymentsPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the MouseHover event of the SuspendedPaymentsPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SuspendedPaymentsPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.SuspendedPaymentToolTip.SetToolTip(this.SuspendedPaymentsPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads the suspended payment.
        /// </summary>
        /// <param name="receiptIdentity">The receipt identity.</param>
        private void LoadSuspendedPayment(int receiptIdentity)
        {
            this.suspendedPaymentDataSet = this.form15035Controll.WorkItem.F15035_GetSuspendedPaymentDetials(receiptIdentity);

            if (this.suspendedPaymentDataSet.GetSuspendedPayment.Rows.Count > 0)
            {
                this.SetControlValues(this.GetSelectedRow(0));
                this.LockControls(false);
            }
            else
            {
                this.LockControls(false);
                this.ClearSuspendedPaymentsHeader();
            }
            //this.PaymentEngineUserControl.TabStop = false;
            //this.PaymentEngineUserControl.Locked = true;
            //this.PaymentEngineUserControl.ApplyReadonlyColumn = false;
            //this.PaymentEngineUserControl.SetDefaultSelection = false;
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        /// <summary>
        /// Gets the selected row.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns>the SuspendedPaymentRow.</returns>
        private F15035SuspendedPaymentsData.GetSuspendedPaymentRow GetSelectedRow(int rowIndex)
        {
            return (F15035SuspendedPaymentsData.GetSuspendedPaymentRow)this.suspendedPaymentDataSet.GetSuspendedPayment.Rows[rowIndex];
        }

        // check assign your controls with values from the typeddataset row

        /// <summary>
        /// Sets the control values.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void SetControlValues(F15035SuspendedPaymentsData.GetSuspendedPaymentRow selectedRow)
        {
            //StringBuilder ownerAddress = new StringBuilder();
            this.ownerId = selectedRow.OwnerID ;
            if (!selectedRow.IsNameNull() )
            {
                this.OwnerNameLinkLabel.Text = selectedRow.Name.ToString();
            }
            else
            {
                this.OwnerNameLinkLabel.Text = string.Empty;  
            }
            if (!selectedRow.IsReceiptDateNull ())
            {
            DateTime  receiptDate;
            receiptDate = DateTime.Parse(selectedRow.ReceiptDate);
            this.ReceiptDateTextBox.Text = receiptDate.ToString(this.dateFormat); 
            }
            else
            {
                this.ReceiptDateTextBox.Text = string.Empty; 
            }
             
            if (!selectedRow.IsReceiptNumberNull() )
            {
              this.ReceiptNumberLinkLabel.Text = selectedRow.ReceiptNumber;
            }
            else
            {
                this.ReceiptNumberLinkLabel.Text = string.Empty;  
            }
            if (!selectedRow.IsAddress1Null())
            {
              this.Address1TextBox.Text = selectedRow.Address1.ToString();
            }
            else
            {
                this.Address1TextBox.Text = string.Empty;  
            }
                if (!selectedRow.IsAddress2Null() )
            {
                this.Address2TextBox.Text = selectedRow.Address2.ToString();
            }
            else
            {
                this.Address2TextBox.Text = string.Empty;  
            }
                if (!selectedRow.IsOwnerCodeNull())
                {
                    this.OwnerCodeTextBox.Text = selectedRow.OwnerCode.ToString();
                }
                else
                {
                    this.OwnerCodeTextBox.Text = string.Empty;  
                }
            if (!selectedRow.IsStateNull())
            {
            this.StateTextBox.Text = selectedRow.State.ToString();
            }
            else
            {
                this.StateTextBox.Text = string.Empty;  
            }
            if (!selectedRow.IsCityNull() )
            {
            this.CityTextBox.Text = selectedRow.City.ToString();
            }
            else
            {
                this.CityTextBox.Text = string.Empty;  
            }
             if (!selectedRow.IsZipNull() )
            {
                this.ZipTextBox.Text = selectedRow.Zip.ToString();
            }
            else
            {
                this.ZipTextBox.Text = string.Empty;  
            }
            if (!selectedRow.IsTotalAmountNull ())
            {
            this.TotalAmountTextBox.Text = selectedRow.TotalAmount.ToString();
            }
            else
            {
                this.TotalAmountTextBox.Text = string.Empty;  
            }
            if (!selectedRow.IsNoteNull ())
            {
              this.NoteTextBox.Text = selectedRow.Note.ToString();
            }
            else
            {
                this.NoteTextBox.Text = string.Empty;  
            }
            if (!selectedRow.IsStateNull ())
            {
            this.StateTextBox.Text = selectedRow.State.ToString();
            }
            else
            {
                 this.StateTextBox.Text = string.Empty;
            }
            if (!selectedRow.IsStatusBackgroundColorNull())
            {
                this.statusBackGroundColor = selectedRow.StatusBackgroundColor;
            }
            if (!selectedRow.IsStatusTextColorNull())
            {
                this.statusTextColor = selectedRow.StatusTextColor;
            }
            
            if (!selectedRow.IsHyperlinkReceiptIDNull())
            {
                this.hyperlinkId = selectedRow.HyperlinkReceiptID;
            }
            if (!selectedRow.IsStatusTextNull())
            {
                this.StatusLabel1.Text = selectedRow.StatusText.ToString();    
            }
            string[] backcolorArr = null;
                int undividedId=0;
                int RColor;
                int GColor;
                int BColor;
                if (!string.IsNullOrEmpty(this.statusBackGroundColor))
                {
                    char[] splitchar = { ',' };
                    backcolorArr = this.statusBackGroundColor.Split(splitchar);
                    if (backcolorArr.Length.Equals(3))
                    {
                        ////Getting Red Color
                        if (string.IsNullOrEmpty(backcolorArr[0]))
                        {
                            RColor = 255;
                        }
                        else
                        {
                            int.TryParse(backcolorArr[0], out RColor);
                        }
                        ////Getting Green Color
                        if (string.IsNullOrEmpty(backcolorArr[1]))
                        {
                            GColor = 255;
                        }
                        else
                        {
                            int.TryParse(backcolorArr[1], out GColor);
                        }
                        ////Getting Blue Color
                        if (string.IsNullOrEmpty(backcolorArr[2]))
                        {
                            BColor = 255;
                        }
                        else
                        {
                            int.TryParse(backcolorArr[2], out BColor);
                        }
                        ////Assign RGB value to form backcolor
                        if (RColor < 0 || RColor > 255 || GColor < 0 || GColor > 255 || BColor < 0 || BColor > 255)
                        {
                            RColor = 255;
                            GColor = 255;
                            BColor = 255;
                        }
                        this.SuspendedReceiptPanel.BackColor = Color.FromArgb(RColor, GColor, BColor);
                       // this.SuspendedReceiptTextBox.ForeColor = Color.FromArgb(RColor, GColor, BColor);
                    }
                    else
                    {
                        this.SuspendedReceiptPanel.BackColor = Color.White;
                       // this.SuspendedReceiptTextBox.ForeColor = Color.White;
                    }
                }
                if (!string.IsNullOrEmpty(this.statusTextColor))
                {
                    char[] splitchar = { ',' };
                    backcolorArr = this.statusTextColor.Split(splitchar);
                    if (backcolorArr.Length.Equals(3))
                    {
                        ////Getting Red Color
                        if (string.IsNullOrEmpty(backcolorArr[0]))
                        {
                            RColor = 255;
                        }
                        else
                        {
                            int.TryParse(backcolorArr[0], out RColor);
                        }
                        ////Getting Green Color
                        if (string.IsNullOrEmpty(backcolorArr[1]))
                        {
                            GColor = 255;
                        }
                        else
                        {
                            int.TryParse(backcolorArr[1], out GColor);
                        }
                        ////Getting Blue Color
                        if (string.IsNullOrEmpty(backcolorArr[2]))
                        {
                            BColor = 255;
                        }
                        else
                        {
                            int.TryParse(backcolorArr[2], out BColor);
                        }
                        ////Assign RGB value to form backcolor
                        if (RColor < 0 || RColor > 255 || GColor < 0 || GColor > 255 || BColor < 0 || BColor > 255)
                        {
                            RColor = 255;
                            GColor = 255;
                            BColor = 255;
                        }
                       // this.SuspendedReceiptTextBox.BackColor = Color.FromArgb(RColor, GColor, BColor);
                        this.StatusLabel1.ForeColor = Color.FromArgb(RColor, GColor, BColor);
                    }
                    else
                    {
                        //this.SuspendedReceiptTextBox.BackColor = Color.White;
                        this.StatusLabel1.ForeColor = Color.White;
                    }
                }
          /*  //this.AddressTextBox.Text = selectedRow.Address1 + "\n" + selectedRow.Address2 + "\n" + selectedRow.City + ", " + selectedRow.State + ", " + selectedRow.Zip;

            //address1
            if (!string.IsNullOrEmpty(selectedRow.Address1.ToString()))
            {
                ownerAddress.Append(selectedRow.Address1.ToString());
            }

            //address2
            if (!string.IsNullOrEmpty(selectedRow.Address2.ToString()))
            {
                ownerAddress.Append("\n");
                ownerAddress.Append(selectedRow.Address2.ToString());
            }

            //city
            if (!string.IsNullOrEmpty(selectedRow.City.ToString()))
            {
                ownerAddress.Append("\n");
                ownerAddress.Append(selectedRow.City.ToString());
            }

            //state
            if (!string.IsNullOrEmpty(selectedRow.State.ToString()))
            {
                if (string.IsNullOrEmpty(selectedRow.City.ToString()))
                {
                    ownerAddress.Append("\n");
                    ownerAddress.Append(selectedRow.State.ToString());
                }
                else
                {
                    ownerAddress.Append(", ");
                    ownerAddress.Append(selectedRow.State.ToString());
                }
            }

            //zip
            if (!string.IsNullOrEmpty(selectedRow.Zip.ToString()))
            {
                if (string.IsNullOrEmpty(selectedRow.City.ToString()) && string.IsNullOrEmpty(selectedRow.State.ToString()))
                {
                    ownerAddress.Append("\n");
                    ownerAddress.Append(selectedRow.Zip.ToString());
                }
                else
                {
                    ownerAddress.Append(", ");
                    ownerAddress.Append(selectedRow.Zip.ToString());
                }
            }

            this.Address1TextBox.Text = ownerAddress.ToString();*/

            //this.StatusLabel1.Text = selectedRow.StatusText;
            //int statusId = selectedRow.Status;
            //this.statusId = selectedRow.Status;
            this.postId = selectedRow.PostID;
            int ppaymentId = selectedRow.PPaymentID;
            this.PaymentEngineUserControl.Locked = true;
            this.PaymentEngineUserControl.LoadPayment(ppaymentId);
            this.AutoPrintOnButton.EnableAutoPrint = Convert.ToBoolean(this.form15035Controll.WorkItem.GetAutoPrintStatus(this.ParentFormId, TerraScanCommon.UserId));
            //this.SetStatusButtonsProperty(selectedRow.PostID);
            this.PaymentEngineUserControl.TabStop = true;
            //this.PaymentEngineUserControl.Locked = false;
           // this.PaymentEngineUserControl.ApplyReadonlyColumn = false;
            this.PaymentEngineUserControl.TotalReceiptAmount = Decimal.Zero;
            //this.PaymentEngineUserControl.OwnerName = this.ReceivedFromTextBox.Text;
            this.PaymentEngineUserControl.SetDefaultSelection = true;
            if (string.IsNullOrEmpty(selectedRow.ReceiptReport.ToString()))
            {
                this.reportNumber = 0;
            }
            else
            {
                this.reportNumber = selectedRow.ReceiptReport;
            }

            if (!selectedRow.IsHyperlinkTextNull())
            {
                this.hyperLinkText = selectedRow.HyperlinkText.ToString();
               
                if (!string.IsNullOrEmpty(this.StatusLabel1.Text.Trim()))
                 {
                     this.StatusLabel2.BringToFront();
                     //this.ReceiptDateLinkLabel.BringToFront();
                     string tempRecieptDate = string.Empty;

                     if (this.StatusLabel1.Text.Trim().Contains("This payment was Applied on:"))
                     {
                         //tempRecieptDate = this.StatusLabel1.Text.Trim().Substring(this.StatusLabel1.Text.Trim().IndexOf(":") + 1);
                         this.StatusLabel2.Left = this.StatusLabel1.Width + 23;
                       //  this.ReceiptDateLinkLabel.Left = 230;
                         this.StatusLabel2.Text = selectedRow.HyperlinkText.ToString();;
                       //  this.ReceiptDateLinkLabel.Text = tempRecieptDate.Trim();
                        // this.StatusLabel1.Text = this.StatusLabel1.Text.Trim().Remove(this.StatusLabel1.Text.Trim().IndexOf(":"));
                        // this.StatusLabel1.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
                     }
                     else if (this.StatusLabel1.Text.Trim().Contains("This payment was Refunded on:"))
                     {
                         //tempRecieptDate = this.StatusLabel1.Text.Trim().Substring(this.StatusLabel1.Text.Trim().IndexOf(":") + 1);
                         this.StatusLabel2.Left = this.StatusLabel1.Width + 23;
                         // this.ReceiptDateLinkLabel.Left = 240;
                         this.StatusLabel2.Text = selectedRow.HyperlinkText.ToString(); ;
                         // this.ReceiptDateLinkLabel.Text = tempRecieptDate.Trim();
                         //this.StatusLabel1.Text = this.StatusLabel1.Text.Trim().Remove(this.StatusLabel1.Text.Trim().IndexOf(":"));
                         //this.StatusLabel1.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
                     }
                     else
                     {
                         this.StatusLabel2.Left = this.StatusLabel1.Width + 23;
                         this.StatusLabel2.Text = selectedRow.HyperlinkText.ToString(); ;
                         
                     }
                }
                 }
             }
            /* else
             {
                 //this.StatusLabel1.BringToFront();
                 //this.StatusLabel2.Text = string.Empty;
                 this.ReceiptDateLinkLabel.Text = string.Empty;
                 //this.StatusLabel1.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
             */
        

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            if (string.IsNullOrEmpty(this.OwnerNameLinkLabel.Text.Trim()))
            {
                this.OwnerNameSearchButton.Focus();
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }
            else if (this.PaymentEngineUserControl.AmountTotal == 0)
            {
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }
            else if (!this.CheckSuspenededAccount())
            {
                DialogResult result = MessageBox.Show(SharedFunctions.GetResourceString("SuspendedAccountError"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    sliceValidationFields.DisableNewMethod = true;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                }
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Checks the suspeneded account.
        /// </summary>
        /// <returns>error Status</returns>
        private bool CheckSuspenededAccount()
        {
            try
            {
                int errorId = 0;

                errorId = this.form15035Controll.WorkItem.F15035_CheckSuspendedAccounts();

                if (errorId.Equals(1))
                {
                    return true;
                }

                return false;
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return false;
            }
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="lockValue">if set to <c>true</c> [lock value].</param>
        private void LockControls(bool lockValue)
        {
            this.OwnerNameSearchButton.Enabled = lockValue;
            this.PaymentEngineUserControlPanel.Enabled = true;
            this.NoteTextBox.LockKeyPress   = !lockValue;
            this.ReceiptDateCalendarButton.Enabled = lockValue;
           
        }


        /// <summary>
        /// Clears the suspended payments header.
        /// </summary>
        private void ClearSuspendedPaymentsHeader()
        {
            this.PaymentEngineUserControl.LoadPayment();  
            this.PaymentEngineUserControl.BalanceAmount = 0;
           this.OwnerNameLinkLabel.Text = string.Empty;
            this.OwnerCodeTextBox.Text = string.Empty;   
            this.ReceiptDateTextBox.Text = string.Empty;
            this.ReceiptNumberLinkLabel.Text = string.Empty;
            this.Address1TextBox.Text = string.Empty;
            this.Address2TextBox.Text = string.Empty;
            this.NoteTextBox.Text = string.Empty;
            this.CityTextBox.Text = string.Empty;
            this.StateTextBox.Text = string.Empty;   
            this.ZipTextBox.Text = string.Empty;
            this.StatusLabel1.Text = string.Empty;
            this.StatusLabel2.Text = string.Empty;
            this.TotalAmountTextBox.Text = string.Empty;
            this.SuspendedReceiptPanel.BackColor = Color.White;
           
        }

        /// <summary>
        /// Validates the slice form.
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        private void ValidateSliceForm(DataEventArgs<int> eventArgs)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = eventArgs.Data;
            this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
        }

        /// <summary>
        /// Gets the selected owner row.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns>owner detials Row</returns>
        private PartiesOwnerDetailsData.ListPartiesOwnerDetailRow GetSelectedOwnerRow(int rowIndex)
        {
            return (PartiesOwnerDetailsData.ListPartiesOwnerDetailRow)this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[rowIndex];
        }

        //// check assign your controls with values from the typeddataset row

        /// <summary>
        /// Sets the control values.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void SetOwnerControlValues(PartiesOwnerDetailsData.ListPartiesOwnerDetailRow selectedRow)
        {
            StringBuilder ownerAddress = new StringBuilder();
            this.OwnerNameLinkLabel.Text = selectedRow.Name;
            this.PaymentEngineUserControl.OwnerName = this.OwnerNameLinkLabel.Text.Trim();
            if (!selectedRow.IsAddress1Null())
            {
                this.Address1TextBox.Text = selectedRow.Address1;
            }
            else
            {
                this.Address1TextBox.Text = string.Empty;  
            }
            if (!selectedRow.IsAddress2Null())
            {
                this.Address2TextBox.Text = selectedRow.Address2;
            }
            else
            {
                this.Address2TextBox.Text = string.Empty;  
            }
            if (!selectedRow.IsStateNull())
            {
                this.StateTextBox.Text = selectedRow.State;
            }
            else
            {
                this.StateTextBox.Text = string.Empty;  
            }
            if (!selectedRow.IsCityNull())
            {
                this.CityTextBox.Text = selectedRow.City;
            }
            else
            {
                this.CityTextBox.Text = string.Empty; 
            }
            if (!selectedRow.IsZipNull())
            {
                this.ZipTextBox.Text = selectedRow.Zip;
            }
            else
            {
                this.ZipTextBox.Text = string.Empty;   
            }
            if(!selectedRow.IsOwnerCodeNull()) 
            {
            this.OwnerCodeTextBox.Text = selectedRow.OwnerCode;  
            }
            else
            {
                this.OwnerCodeTextBox.Text = string.Empty; 
            }
            //this.TotalAmountTextBox.Text = selectedRow.TotalAmount.ToString();
            //this.NoteTextBox.Text = selectedRow.Note;
            //this.StateTextBox.Text = selectedRow.StatusText;
            //this.statusBackGroundColor = selectedRow.StatusBackgroundColor;
            //this.statusTextColor = selectedRow.StatusTextColor;
            //this.hyperLinkText = selectedRow.HyperlinkText;
            //this.hyperlinkId = selectedRow.HyperlinkReceiptID; 
   
          /*  //address1
            if (!string.IsNullOrEmpty(selectedRow.Address1.ToString()))
            {
                ownerAddress.Append(selectedRow.Address1.ToString());
            }

            //address2
            if (!string.IsNullOrEmpty(selectedRow.Address2.ToString()))
            {
                ownerAddress.Append("\n");
                ownerAddress.Append(selectedRow.Address2.ToString());
            }

            //city
            if (!string.IsNullOrEmpty(selectedRow.City.ToString()))
            {
                ownerAddress.Append("\n");
                ownerAddress.Append(selectedRow.City.ToString());
            }

            //state
            if (!string.IsNullOrEmpty(selectedRow.State.ToString()))
            {
                if (string.IsNullOrEmpty(selectedRow.City.ToString()))
                {
                    ownerAddress.Append("\n");
                    ownerAddress.Append(selectedRow.State.ToString());
                }
                else
                {
                    ownerAddress.Append(", ");
                    ownerAddress.Append(selectedRow.State.ToString());
                }
            }

            //zip
            if (!string.IsNullOrEmpty(selectedRow.Zip.ToString()))
            {
                if (string.IsNullOrEmpty(selectedRow.City.ToString()) && string.IsNullOrEmpty(selectedRow.State.ToString()))
                {
                    ownerAddress.Append("\n");
                    ownerAddress.Append(selectedRow.Zip.ToString());
                }
                else
                {
                    ownerAddress.Append(", ");
                    ownerAddress.Append(selectedRow.Zip.ToString());
                }
            }
            //this.AddressTextBox.Text = selectedRow.Address1.ToString() + "\n" + selectedRow.Address2.ToString() + "\n" + selectedRow.City.ToString() + "," + selectedRow.State.ToString() + "," + selectedRow.Zip.ToString();

            this.Address1TextBox.Text = ownerAddress.ToString();*/
        }

        /// <summary>
        /// Saves the suspended payment receipt.
        /// </summary>
        private void SaveSuspendedPaymentReceipt()
        {
            DateTime outReceiptDate;
            int ppaymentId = 0;
            ////Check For Required Fields
            if (this.ownerId.Equals(-1))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.OwnerNameSearchButton.Focus();
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            ////get the receiptdate                 
            outReceiptDate = DateTime.Parse(this.ReceiptDateTextBox.Text);
            ////checks for valid receipt
            //string validResult = this.form15035Controll.WorkItem.F11035_GetValidReceiptTest(this.keyId, outReceiptDate);
            string validResult = this.form15035Controll.WorkItem.F11035_GetValidReceiptTest(-98, outReceiptDate);
            if (!String.IsNullOrEmpty(validResult))
            {
                ////TODO title hardcode - needs some clarification
                MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("InvalidReceiptHeader"), validResult), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("InvalidReceipt")), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ////Checking the Amount > 0               
            if (this.PaymentEngineUserControl.AmountTotal > 0)
            {
                ////create payment
                ppaymentId = this.PaymentEngineUserControl.CreatePayment(this.ownerId);
            }
            else
            {
                return;
            }
            this.paymentID = ppaymentId; 
            ////save receipt
            if (/*this.PaymentsTotalTextBox.DecimalTextBoxValue > 0 && */this.TotalAmountTextBox.DecimalTextBoxValue >0   )
            {
                this.suspendedPaymentDataSet.SaveSuspendedPaymentReceipt.Rows.Clear();
                F15035SuspendedPaymentsData.SaveSuspendedPaymentReceiptRow dr = this.suspendedPaymentDataSet.SaveSuspendedPaymentReceipt.NewSaveSuspendedPaymentReceiptRow();

                dr.UserID = TerraScanCommon.UserId;
                dr.ReceiptDate = outReceiptDate.ToShortDateString();
                dr.PPaymentID = ppaymentId;
                dr.PostTypeID = 50;
                dr.Amount = this.PaymentEngineUserControl.AmountTotal;
                dr.Note = this.NoteTextBox.Text;  

                this.suspendedPaymentDataSet.SaveSuspendedPaymentReceipt.Rows.Add(dr);

                //// receipt soureceid - 5
                int savedReceiptId = this.form15035Controll.WorkItem.F15035_CreateSuspendedPaymentReceipt(0, 5, Utility.GetXmlString(this.suspendedPaymentDataSet.SaveSuspendedPaymentReceipt), null);
                ////receipt saved
                if (savedReceiptId > 0)
                {
                    ////get autoprint status
                    this.AutoPrintOnButton.EnableAutoPrint = Convert.ToBoolean(this.form15035Controll.WorkItem.GetAutoPrintStatus(this.ParentFormId, TerraScanCommon.UserId));
                    if (this.AutoPrintOnButton.EnableAutoPrint)
                    {
                        Hashtable reportOptionalParameter = new Hashtable();
                        reportOptionalParameter.Add("ReceiptID", savedReceiptId);
                        ////changed the parameter type from string to int
                        TerraScanCommon.ShowReport(this.reportNumber, Report.ReportType.PrintDefault, reportOptionalParameter);
                    }
                }

                SliceReloadActiveRecord currentSliceInfo;
                currentSliceInfo.MasterFormNo = this.masterFormNo;
                currentSliceInfo.SelectedKeyId = savedReceiptId;
                ////to refresh the master form with the return keyid
                this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));

                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Sets the status buttons property.
        /// </summary>
        /// <param name="postId">The post id.</param>
        private void SetStatusButtonsProperty(int postId)
        {
            if (postId != 0)
            {
                this.NotPostedButton.StatusIndicator = true;
            }
            else
            {
                this.NotPostedButton.StatusIndicator = false;
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
            this.ReceiptMonthCalender.Top = this.ReceiptDateCalendarButton.Top + this.ReceiptDatePanel.Top-10;
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

        #endregion

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

        private void StatusLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
               formInfo = TerraScanCommon.GetFormInfo(11001);
               formInfo.optionalParameters = new object[1];
               formInfo.optionalParameters[0] = this.hyperlinkId;
               this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
    }
}
