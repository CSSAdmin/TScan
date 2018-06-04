//--------------------------------------------------------------------------------------------
// <copyright file="F49910.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F49910.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 31 Jan 2008      Ramya.D             Created
// 22 Jul 2010      Manoj               modified To implement #7399 - TSCO -  Field level error message For Instrument Number
// 28 JUN 2011      Manoj          Change the tender Type default 'check'.
// 12 SEP 2011      Manoj          Remove the tender Type default 'check'.  
//*********************************************************************************/
namespace D49910
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Web.Services.Protocols;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using System.Text.RegularExpressions;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using Infragistics.Win.UltraWinCalcManager.FormulaBuilder;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win.UltraWinCalcManager;

    /// <summary>
    /// F35100 class file
    /// </summary>
    [SmartPart]
    public partial class F49910 : BaseSmartPart
    {
        #region Variable

        /// <summary>
        /// Checks the copy button Clicked
        /// </summary>
        private bool copyButtonClick;

        /// <summary>
        /// keyId
        /// </summary>
        private int keyId;

        /// <summary>
        /// instrumentId
        /// </summary>
        private int instrumentId;

        /// <summary>
        /// instrumentTypeId
        /// </summary>
        private int instrumentTypeId;

        /// <summary>
        /// instrumentChangeId
        /// </summary>
        private bool instrumentChangeId = false;

        /// <summary>
        /// customerId
        /// </summary>
        private int customerId;

        /// <summary>
        /// exemptionTypeId
        /// </summary>
        private int exemptionTypeId;

        /// <summary>
        /// wolId
        /// </summary>
        private int wolId;

        /// <summary>
        /// userId
        /// </summary>
        private int userId;

        /// <summary>
        /// insTypeId
        /// </summary>
        private int insTypeId;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        private decimal receiptBalance;

        /// <summary>
        /// typeComboData
        /// </summary>
        private CommonData typeComboData = new CommonData();

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// Instance of F49910Controller to call the WorkItem
        /// </summary>
        private F49910Controller form49910Controller;

        /// <summary>
        /// instrumentHeaderDataSetObject
        /// </summary>
        private F49910InstrumentHeaderDataSet instrumentHeaderDataSetObject = new F49910InstrumentHeaderDataSet();

        /// <summary>
        /// recordertableRow
        /// </summary>
        private F49910InstrumentHeaderDataSet.f49901RecorderDetailsDataTableRow recordertableRow;

        /// <summary>
        /// Used to store the unsavedChangeExists
        /// </summary>
        private bool unsavedChangeExists;

        /// <summary>
        /// Used to store the vformula
        /// </summary>
        private string vformula = string.Empty;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// newInstrument
        /// </summary>
        private bool newInstrument;

        /// <summary>
        /// Used to store the unsavedChangeExists
        /// </summary>
        private bool formMasterNew;

        /// <summary>
        /// Used to hold the instrument header details to be saved
        /// </summary>
        private DataSet tempDataSet = new DataSet(SharedFunctions.GetResourceString("Root"));


        #region Feel Label ToolTip Controls

        System.Windows.Forms.ToolTip ToolTipLabel = new System.Windows.Forms.ToolTip();

        #endregion

        #region DecimalPlace

        /// <summary>
        /// fee1DecimalPlace
        /// </summary>
        private int fee1DecimalPlace;

        /// <summary>
        /// fee2DecimalPlace
        /// </summary>
        private int fee2DecimalPlace;

        /// <summary>
        /// fee3DecimalPlace
        /// </summary>
        private int fee3DecimalPlace;

        /// <summary>
        /// fee4DecimalPlace
        /// </summary>
        private int fee4DecimalPlace;

        /// <summary>
        /// fee1DecimalPlace
        /// </summary>
        private int fee5DecimalPlace;

        /// <summary>
        /// fee6DecimalPlace
        /// </summary>
        private int fee6DecimalPlace;

        #endregion DecimalPlace

        #region FormulaVariable

        /// <summary>
        /// fee1Formula
        /// </summary>
        private string fee1Formula;

        /// <summary>
        /// fee2Formula
        /// </summary>
        private string fee2Formula;

        /// <summary>
        /// fee3Formula
        /// </summary>
        private string fee3Formula;

        /// <summary>
        /// fee4Formula
        /// </summary>
        private string fee4Formula;

        /// <summary>
        /// fee5Formula
        /// </summary>
        private string fee5Formula;

        /// <summary>
        /// fee6Formula
        /// </summary>
        private string fee6Formula;

        #endregion FormulaVariable


        /// <summary>
        /// tempCustomer
        /// </summary>
        private int tempCustomer;

        /// <summary>
        /// tempExemption
        /// </summary>
        private int tempExemption;

        /// <summary>
        /// tempReviewFrom
        /// </summary>
        private int tempReviewFrom;


        #endregion Variable

        #region Constructor

        /// <summary>
        /// F49910
        /// </summary>
        public F49910()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F49910"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F49910(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            ////this.PaymentEngineUserControl.InstrumentPayment = true;
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.InstrumentHeaderPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.InstrumentHeaderPictureBox.Height, this.InstrumentHeaderPictureBox.Width, tabText, red, green, blue);
            this.FileDateTextBox.MaxLength = 10;
            this.InstrumentDateTextBox.MaxLength = 10;
            this.CopyButton.Enabled = false;
        }
        #endregion Constructor

        #region Eventpublication

        /// <summary>
        /// Event for SetActiveRecordButtons
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecordButtons, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int[]>> SetActiveRecordButtons;

        /// <summary>
        /// Event for SetRecordCount
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetRecordCount, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetRecordCount;

        /// <summary>
        /// Event for SetActiveRecord
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetActiveRecord;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

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
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Declare the event D84700_F84722_OnSave_SetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84700_F84722_OnSave_SetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> FormSlice_OnSave_SetKeyId;

        /// <summary>
        /// Declare the event D84700_F84722_OnSave_SetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D49910_F49910_OnCopy_SetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadParaMeterActiveRecord>> FormSlice_OnCopy_SetKeyId;

        /// <summary>
        /// Declare the event for raising new operation in form master        
        /// </summary> 
        [EventPublication(EventTopicNames.D9030_F9030_RaiseFormMasterNew, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<int>> D9030_F9030_RaiseFormMasterNew;

        #endregion Eventpublication

        #region Properties

        /// <summary>
        /// Gets or sets the F49910Control.
        /// </summary>
        /// <value>The F49910Control.</value>
        [CreateNew]
        public F49910Controller F49910Control
        {
            get { return this.form49910Controller as F49910Controller; }
            set { this.form49910Controller = value; }
        }

        /// <summary>
        /// Gets or sets the InstrumentId.
        /// </summary>
        /// <value>InstrumentId</value>
        public int InstrumentId
        {
            get { return this.keyId; }
            set { this.keyId = value; }
        }

        public decimal balanceAmount
        {
            get { return this.receiptBalance; }
            set { this.receiptBalance = value; }
        }

        #endregion Properties

        #region EventSubscription

        #region GetSlicePermission
        /// <summary>
        /// Event Subscription SetSlicePermission
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The Event.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            try
            {
                if (this != null && this.IsDisposed != true)
                {
                    if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                    {
                        this.formMasterNew = this.GetFormMasterNewPermission();

                        this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                        this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                        this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                        this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;


                        if (this.instrumentHeaderDataSetObject.f49901RecorderDetailsDataTable.Rows.Count > 0)
                        {
                            eventArgs.Data.FlagInvalidSliceKey = false;
                            // Added permission for the copy button

                            this.Setpermission();

                            this.CopyButton.Enabled = this.formMasterNew;
                            //if (this.formMasterNew)
                            //{
                            //    this.CopyButton.Enabled = true;
                            //}
                            //else
                            //{
                            //    this.CopyButton.Enabled = this.slicePermissionField.newPermission ;
                            //}
                        }
                        else
                        {
                            //// Coding Added for the issue 4212 0n 30/5/2009.
                            //// Last Slice does not have a record also it will not return invalid slice
                            if (eventArgs.Data.FlagInvalidSliceKey)
                            {
                                eventArgs.Data.FlagInvalidSliceKey = true;
                            }
                            // Added permission for the copy button


                            // Disable the form if record count is 0
                            this.CopyButton.Enabled = false;

                        }


                    }
                }
                this.InstrumentTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion GetSlicePermission

        #region Form Master Edit Enable

        /// <summary>
        /// When the Form Master edit enable is called the this evnt is subscribed
        /// </summary>
        /// <remarks>if form master goes edit mode(Save and cancel button is enabled in form master) 
        /// then copy button should be disabled
        /// </remarks>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.FormSlice_EditEnabled, ThreadOption.UserInterface)]
        public void OnFormSlice_EditEnabled(object sender, DataEventArgs<int> eventArgs)
        {
            if (eventArgs.Data.Equals(this.masterFormNo) && this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                this.CopyButton.Enabled = false;

                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
            }
        }

        #endregion End of Form Master Edit Enable


        #region EnableNewMethod

        /// <summary>
        /// Called when [D9030_ F9030_ enable new method].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, DataEventArgs<int> eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    this.InstrumentTextBox.Focus();

                    this.ReviewedFromComboBox1.SelectedValueChanged -= new System.EventHandler(this.ReviewedFromComboBox1_SelectionEvent);

                    if (this.slicePermissionField.newPermission)
                    {
                        this.NewButton();
                        ////int.TryParse(this.InstrumentTypeComboBox.SelectedValue.ToString(), out this.insTypeId);
                        ////this.GetFeeDetails(this.insTypeId);
                        this.CopyButton.Enabled = false;
                        this.ReviwedLinkLabel.Enabled = true;
                        this.ReviwedLinkLabel.Text = SharedFunctions.GetResourceString("No");
                        this.Fee1TextBox.LockKeyPress = true;
                        this.Fee2TextBox.LockKeyPress = true;
                        this.Fee3TextBox.LockKeyPress = true;
                        this.Fee4TextBox.LockKeyPress = true;
                        this.Fee5TextBox.LockKeyPress = true;
                        this.Fee6TextBox.LockKeyPress = true;
                        this.ClearFeeTextBoxes();
                        this.EmptyLable();
                        this.InstrumentTextBox.Focus();
                        this.pageMode = TerraScanCommon.PageModeTypes.New;

                    }
                    else
                    {
                        this.BlockComboBox(false);
                        this.BlockPanel(false);
                        this.BlockTextbox(true);
                        this.BlockButton(false);
                        this.ClearInstrumentHeadetTextBoxControls();
                        this.AssignDefaultValue();
                        ////disable paymentengine
                        this.PaymentEngineUserControl.TabStop = false;
                        this.PaymentEngineUserControl.Locked = true;
                        this.PaymentEngineUserControl.LoadPaymentGrid(0);
                        this.PaymentEngineUserControl.AmountTotal = 0;
                        this.PaymentEngineUserControl.SetDefaultSelection = false;

                        this.pageMode = TerraScanCommon.PageModeTypes.New;
                    }

                    this.ReviewedFromComboBox1.SelectedValueChanged += new System.EventHandler(this.ReviewedFromComboBox1_SelectionEvent);
                    this.ReviwedLinkLabel.Text = SharedFunctions.GetResourceString("No");
                    this.UserTextBox.Text = TerraScanCommon.UserName.ToString();
                    this.InstrumentTextBox.Focus();
                    this.ActiveControl = this.InstrumentTextBox;
                    this.ActiveControl.Focus();
                    // Disable the copy button for new mode
                    this.CopyButton.Enabled = false;
                    

                }
            }
            ////For Permission
            //// this.Setpermission();
        }

        #endregion EnableNewMethod

        #region SaveSliceInformation

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                {
                    if (this.slicePermissionField.editPermission)
                    {
                        SliceValidationFields sliceValidationFields = new SliceValidationFields();
                        sliceValidationFields.FormNo = eventArgs.Data;
                        this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
                    }
                }
                else
                {
                    SliceValidationFields sliceValidationFields = new SliceValidationFields();
                    sliceValidationFields.FormNo = eventArgs.Data;
                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
                }
            }
        }

        #endregion SaveSliceInformation

        #region SaveConfirmed

        /// <summary>
        /// Called when [D9030_ F9030_ save confirmed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, DataEventArgs<int> eventArgs)
        {
           if (this != null && this.IsDisposed != true)
            {
                // if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                {
                    this.SaveRecorder(false);
                    this.newInstrument = false;
                    // Added permission for the copy button
                    //if (this.slicePermissionField.newPermission && this.formMasterNew)
                    //{
                    //    this.CopyButton.Enabled = true;
                    //}
                    //else
                    //{
                    //    this.CopyButton.Enabled = false;
                    //}
                    this.CopyButton.Enabled = this.formMasterNew;
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
                else if (this.copyButtonClick)
                {
                    this.SaveRecorder(false);
                    this.newInstrument = false;
                    this.CopyButton.Enabled = this.formMasterNew;
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.copyButtonClick = false;
                }
                else
                {
                    SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                    sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceUpdateActiveRecord.SelectedKeyId = this.keyId;
                    this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
        }

        #endregion SaveConfirmed

        #region CancelSliceInformation

        /// <summary>
        /// Called when [D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                this.flagLoadOnProcess = true;
                this.newInstrument = false;
                this.PaymentEngineUserControl.PaymentAmountChangeEvent += new TerraScan.PaymentEngine.PaymentEngineUserControl.PaymentAmountChangeEventHandler(this.PaymentEngineUserControl_PaymentAmountChangeEvent);
                //this.PaymentEngineUserControl.PaymentItemChangeEvent += new TerraScan.PaymentEngine.PaymentEngineUserControl.PaymentItemChangeEventHandler(this.PaymentEngineUserControl_PaymentItemChangeEvent);     
                this.FlagSliceForm = true;
                ////this.InstrumentTextBox.Focus();
                this.LoadWOLComboBox();
                this.LoadInstrumentTypeComboBox();
                this.LoadInstrumentHeader();
                ////this.CopyButton.Enabled = true;
                this.InstrumentTextBox.Focus();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.flagLoadOnProcess = false;  
            }
        }

        #endregion CancelSliceInformation

        #region DeleteSliceInformation

        /// <summary>
        /// Called when [D9030_ F9030_ delete slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                if (this.slicePermissionField.deletePermission)
                {
                    int deletedInsId = this.form49910Controller.WorkItem.F49910_DeleteInstrumentHeader(this.keyId, TerraScanCommon.UserId);
                    if (deletedInsId > 0)
                    {
                        this.CopyButton.Enabled = false;
                    }
                }
            }
        }

        #endregion DeleteSliceInformation

        #region FormSlice_OnSave_GetKeyId

        /// <summary>
        /// Event Subscription D84700_F84721_OnSave_GetKeyId.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D84700_F84721_OnSave_GetKeyId, ThreadOption.UserInterface)]
        public void FormSlice_OnSave_GetKeyId(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.LoadInstrumentHeader();
                }
            }
        }

        #endregion FormSlice_OnSave_GetKeyId

        #region LoadSliceDetails

        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.flagLoadOnProcess = true;

                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.PaymentEngineUserControl.PaymentAmountChangeEvent += new TerraScan.PaymentEngine.PaymentEngineUserControl.PaymentAmountChangeEventHandler(this.PaymentEngineUserControl_PaymentAmountChangeEvent);
                    this.FlagSliceForm = true;
                    this.InstrumentTextBox.Focus();
                    this.LoadWOLComboBox();
                    this.LoadInstrumentTypeComboBox();
                    this.LoadInstrumentHeader();
                    this.flagLoadOnProcess = false;
                    this.InstrumentTextBox.Focus(); 
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    // this.CopyButton.Enabled = true;
                }
            }
        }

        #endregion LoadSliceDetails

        #region ReloadAfterSave

        /// <summary>
        /// Called when [D9030_ F9030_ reload after save].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_ReloadAfterSave(TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                if (this.D9030_F9030_ReloadAfterSave != null)
                {
                    this.D9030_F9030_ReloadAfterSave(this, eventArgs);
                }
            }
        }

        #endregion ReloadAfterSave

        /// <summary>
        /// Called when [D9030_ F9030_ raise form master new].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_RaiseFormMasterNew(TerraScan.Infrastructure.Interface.EventArgs<int> eventArgs)
        {
            if (this.D9030_F9030_RaiseFormMasterNew != null)
            {
                this.D9030_F9030_RaiseFormMasterNew(this, eventArgs);
            }
        }

        #endregion EventSubscription

        #region UserDefinedmethod

        #region LoadInstrumentHeader
        /// <summary>
        /// LoadInstrumentHeader
        /// </summary>
        private void LoadInstrumentHeader()
        {
            this.ReviewedFromComboBox1.SelectedValueChanged -= new System.EventHandler(this.ReviewedFromComboBox1_SelectionEvent);

            this.panel1.Width = 688;
            ////this.PaymentEngineUserControl.InstrumentPayment = true;
            this.instrumentHeaderDataSetObject = this.form49910Controller.WorkItem.F49910_GetInstrumentHeaderDetails(this.keyId);
            if (this.instrumentHeaderDataSetObject.f49901RecorderDetailsDataTable.Rows.Count > 0)
            {
                ////this.flagLoadOnProcess = true;
                this.recordertableRow = (F49910InstrumentHeaderDataSet.f49901RecorderDetailsDataTableRow)this.instrumentHeaderDataSetObject.f49901RecorderDetailsDataTable.Rows[0];
                int.TryParse(this.recordertableRow.InstID.ToString(), out this.instrumentId);

                this.InstrumentTextBox.Text = this.recordertableRow.InstNum.ToString();
                this.BookandPageTextBox.Text = this.recordertableRow.BookPage.ToString();
                this.FileDateTextBox.Text = this.recordertableRow.FileDate.ToString();
                if (this.recordertableRow.IsFileTimeNull() || this.recordertableRow.FileTime.Trim().Equals("00:00:00"))
                {
                    this.fileTimeTextBox.Text = string.Empty;
                }
                else
                {
                    DateTime tempDateTime;
                    tempDateTime = Convert.ToDateTime(this.recordertableRow.FileTime.ToString());
                    this.fileTimeTextBox.Text = tempDateTime.ToString("hh:mm tt");

                    //// this.fileTimeTextBox.Text = this.recordertableRow.FileTime.ToString();
                }
                this.InstrumentDateTextBox.Text = this.recordertableRow.InstrumentDate.ToString();
                this.UserTextBox.Text = this.recordertableRow.Name_Display.ToString();

                int.TryParse(this.recordertableRow.UserID.ToString(), out this.userId);
                int.TryParse(this.recordertableRow.ITypeID.ToString(), out this.instrumentTypeId);
                int.TryParse(this.recordertableRow.CustomerID.ToString(), out this.customerId);
                int.TryParse(this.recordertableRow.ETypeID.ToString(), out this.exemptionTypeId);
                int.TryParse(this.recordertableRow.WOL.ToString(), out this.wolId);

                this.DeliverToTextBox.Text = this.recordertableRow.ReturnedTo.ToString();
                this.SerialNumberTextBox.Text = this.recordertableRow.SerialNum.ToString();

                this.FeeCalculation(this.recordertableRow, null);

                if (this.recordertableRow.IsReviewed.ToString().Equals(SharedFunctions.GetResourceString("True")))
                {
                    this.ReviwedLinkLabel.Text = SharedFunctions.GetResourceString("Yes");
                }
                else
                {
                    this.ReviwedLinkLabel.Text = SharedFunctions.GetResourceString("No");
                }

                if (this.instrumentTypeId != 0)
                {
                    this.InstrumentTypeComboBox.SelectedValue = this.instrumentTypeId;
                    this.insTypeId = (int)this.InstrumentTypeComboBox.SelectedValue;
                }
                else
                {
                    this.InstrumentTypeComboBox.SelectedIndex = 1;
                }

                if (this.customerId != 0)
                {
                    //this.CustomerComboBox.SelectedValue = 0;
                    //if (this.CustomerComboBox.SelectedValue != null)
                    //{
                        this.CustomerComboBox.SelectedValue = this.customerId;
                        //if (this.CustomerComboBox.SelectedValue != null)
                        //{
                            this.tempCustomer = (int)this.CustomerComboBox.SelectedValue;
                        //}
                        //else
                        //{
                        //    this.tempCustomer = 0;
                        //}
                    //}
                    //else
                    //{
                    //    this.CustomerComboBox.SelectedValue = 0;
                    //    this.tempCustomer = (int)this.CustomerComboBox.SelectedValue;   
                    //}
                                   
                }
                else
                {
                    this.CustomerComboBox.SelectedIndex = 0;
                }

                if (this.exemptionTypeId != 0)
                {
                    this.ExemptionComboBox.SelectedValue = this.exemptionTypeId;
                    this.tempExemption = (int)this.ExemptionComboBox.SelectedValue;
                }
                else
                {
                    this.ExemptionComboBox.SelectedIndex = 0;
                }

                if (this.recordertableRow.IsRcvFromNull())
                {
                    this.ReviewedFromComboBox1.SelectedText = string.Empty;
                }
                else if (string.IsNullOrEmpty(this.recordertableRow.RcvFrom.ToString()))
                {
                    this.ReviewedFromComboBox1.SelectedValue = 0;
                }
                else
                {
                    this.ReviewedFromComboBox1.Text = this.recordertableRow.RcvFrom.ToString().Trim();
                    this.tempReviewFrom = (int)this.ReviewedFromComboBox1.SelectedValue;
                }

                if (this.recordertableRow.WOL.ToString().Equals(SharedFunctions.GetResourceString("True")))
                {
                    this.WOLComboBox.SelectedValue = 1;
                }
                else
                {
                    this.WOLComboBox.SelectedValue = 0;
                }

                this.PaymentEngineUserControl.LoadPaymentGrid(this.keyId);
                this.BlockButton(true);
                // Added permission for the copy button
                //if (this.slicePermissionField.newPermission ||  this.formMasterNew)
                //{
                //    this.CopyButton.Enabled = true;
                //}
                //else
                //{
                //    this.CopyButton.Enabled = false;
                //}
                ////For Permission
                this.Setpermission();
                this.CopyButton.Enabled = this.formMasterNew;

                //////For copy Functionality
                //if (this.applyCopyButton)
                //{
                //    this.applyCopyButton = false;
                //    this.ClearFeeTextBoxes();
                //    this.EmptyLable();
                //    this.ClearInstrumentHeadetTextBoxControls();
                //    this.PaymentEngineUserControl.LoadPaymentGrid(this.keyId);
                //    this.ReviwedLinkLabel.Text = string.Empty;
                //    this.UserTextBox.Text = string.Empty;
                //    this.InstrumentDateTextBox.Text = string.Empty;
                //    this.InstrumentTypeComboBox.Text = string.Empty;
                //    this.ExemptionComboBox.Text = "<<Select>>";
                //    this.CopyButton.Enabled = false;
                //    this.DeliverToTextBox.Text = this.recordertableRow.ReturnedTo.ToString();
                //    this.WOLComboBox.SelectedValue = 2;

                //}

            }
            else
            {
                this.ClearFeeTextBoxes();
                this.EmptyLable();
                this.ClearInstrumentHeadetTextBoxControls();
                this.BlockPanel(false);
                this.BlockButton(false);
                this.SetbackColor();
                this.ClearComboBox();
                this.PaymentEngineUserControl.LoadPaymentGrid(this.keyId);
                this.ReviwedLinkLabel.Text = string.Empty;
                this.UserTextBox.Text = string.Empty;
                this.FileDateTextBox.Text = string.Empty;
                this.InstrumentDateTextBox.Text = string.Empty;
                this.CopyButton.Enabled = false;

            }

            this.ReviewedFromComboBox1.SelectedValueChanged += new System.EventHandler(this.ReviewedFromComboBox1_SelectionEvent);
        }

        #endregion LoadInstrumentHeader

        #region NewButton

        private void NewButton()
        {
            this.InstrumentTextBox.Focus();
            this.newInstrument = true;
            this.pageMode = TerraScanCommon.PageModeTypes.New;
            this.BlockComboBox(false);
            this.BlockPanel(true);
            this.BlockTextbox(false);
            this.BlockButton(true);
            this.ClearInstrumentHeadetTextBoxControls();
            this.AssignDefaultValue();
            this.PaymentEngineUserControl.TabStop = true;
            this.PaymentEngineUserControl.Locked = false;
            this.PaymentEngineUserControl.Enabled = true;
            this.PaymentEngineUserControl.LoadPaymentGrid(0);
            this.PaymentEngineUserControl.AmountTotal = 0;
            this.PaymentEngineUserControl.SetDefaultSelection = true;
            this.Fee1TextBox.LockKeyPress = true;
            this.Fee2TextBox.LockKeyPress = true;
            this.Fee3TextBox.LockKeyPress = true;
            this.Fee4TextBox.LockKeyPress = true;
            this.Fee5TextBox.LockKeyPress = true;
            this.Fee6TextBox.LockKeyPress = true;
            this.ClearFeeTextBoxes();
            this.EmptyLable();
        }

        #endregion NewButton

        #region GetFeeDetailsinNewMode

        /// <summary>
        /// GetFeeDetails
        /// </summary>
        private void GetFeeDetails(int typeId)
            {
            if (typeId > 0)
            {
                this.instrumentHeaderDataSetObject = this.form49910Controller.WorkItem.F49910_GetFeeDetails(typeId);
                if (this.instrumentHeaderDataSetObject.f49910feeDetailsTable.Rows.Count > 0)
                {
                    F49910InstrumentHeaderDataSet.f49910feeDetailsTableRow feeTableRow = (F49910InstrumentHeaderDataSet.f49910feeDetailsTableRow)this.instrumentHeaderDataSetObject.f49910feeDetailsTable.Rows[0];
                    this.FeeCalculation(null, feeTableRow);
                }
            }
        }

        #endregion GetFeeDetailsinNewMode

        #region ClearFeeTextBoxes

        private void ClearFeeTextBoxes()
        {
            this.Fee1TextBox.TextCustomFormat = SharedFunctions.GetResourceString("TextFormat5");
            this.Fee1TextBox.Precision = 0;
            this.Fee1TextBox.Text = string.Empty;

            this.Fee2TextBox.TextCustomFormat = SharedFunctions.GetResourceString("TextFormat5");
            this.Fee2TextBox.Precision = 0;
            this.Fee2TextBox.Text = string.Empty;

            this.Fee3TextBox.TextCustomFormat = SharedFunctions.GetResourceString("TextFormat5");
            this.Fee3TextBox.Precision = 0;
            this.Fee3TextBox.Text = string.Empty;

            this.Fee4TextBox.TextCustomFormat = SharedFunctions.GetResourceString("TextFormat5");
            this.Fee4TextBox.Precision = 0;
            this.Fee4TextBox.Text = string.Empty;

            this.Fee5TextBox.TextCustomFormat = SharedFunctions.GetResourceString("TextFormat5");
            this.Fee5TextBox.Precision = 0;
            this.Fee5TextBox.Text = string.Empty;

            this.Fee6TextBox.TextCustomFormat = SharedFunctions.GetResourceString("TextFormat5");
            this.Fee6TextBox.Precision = 0;
            this.Fee6TextBox.Text = string.Empty;
        }

        #endregion ClearFeeTextBoxes

        #region FeePart

        #region FeeCalculation

        private void FeeCalculation(F49910InstrumentHeaderDataSet.f49901RecorderDetailsDataTableRow instrumentHeaderRow, F49910InstrumentHeaderDataSet.f49910feeDetailsTableRow feeTableRow)
        {
            if (instrumentHeaderRow != null)
            {
                this.AssignValueToFeeLable(instrumentHeaderRow, null);
                this.SetFormulaTovariable(instrumentHeaderRow, null);
                this.SetDecimalVariable(instrumentHeaderRow, null);

                ////SetTextFormat to textBoxes
                this.ToSetDecimalPlaces(this.Fee1TextBox, this.fee1DecimalPlace);
                this.ToSetDecimalPlaces(this.Fee2TextBox, this.fee2DecimalPlace);
                this.ToSetDecimalPlaces(this.Fee3TextBox, this.fee3DecimalPlace);
                this.ToSetDecimalPlaces(this.Fee4TextBox, this.fee4DecimalPlace);
                this.ToSetDecimalPlaces(this.Fee5TextBox, this.fee5DecimalPlace);
                this.ToSetDecimalPlaces(this.Fee6TextBox, this.fee6DecimalPlace);

                this.AssignValueToFeeTextBox(instrumentHeaderRow, null);

                this.CheckLabelValues();

                ////TextBoxValidation
                this.ValidateEFfieldsTextBoxs(this.Fee1TextBox, this.fee1Formula, this.Fee1Label.Text.Trim(), this.PermissionFiled.editPermission, this.formMasterPermissionEdit);
                this.ValidateEFfieldsTextBoxs(this.Fee2TextBox, this.fee2Formula, this.Fee2Label1.Text.Trim(), this.PermissionFiled.editPermission, this.formMasterPermissionEdit);
                this.ValidateEFfieldsTextBoxs(this.Fee3TextBox, this.fee3Formula, this.Fee3Label.Text.Trim(), this.PermissionFiled.editPermission, this.formMasterPermissionEdit);
                this.ValidateEFfieldsTextBoxs(this.Fee4TextBox, this.fee4Formula, this.Fee4Label.Text.Trim(), this.PermissionFiled.editPermission, this.formMasterPermissionEdit);
                this.ValidateEFfieldsTextBoxs(this.Fee5TextBox, this.fee5Formula, this.Fee5Label.Text.Trim(), this.PermissionFiled.editPermission, this.formMasterPermissionEdit);
                this.ValidateEFfieldsTextBoxs(this.Fee6TextBox, this.fee6Formula, this.Fee6Label.Text.Trim(), this.PermissionFiled.editPermission, this.formMasterPermissionEdit);

                this.LoadEFCalucaltionGrid(instrumentHeaderRow, null);
            }
            else if (feeTableRow != null)
            {
                this.AssignValueToFeeLable(null, feeTableRow);
                this.SetFormulaTovariable(null, feeTableRow);
                this.SetDecimalVariable(null, feeTableRow);

                ////SetTextFormat to textBoxes
                this.ToSetDecimalPlaces(this.Fee1TextBox, this.fee1DecimalPlace);
                this.ToSetDecimalPlaces(this.Fee2TextBox, this.fee2DecimalPlace);
                this.ToSetDecimalPlaces(this.Fee3TextBox, this.fee3DecimalPlace);
                this.ToSetDecimalPlaces(this.Fee4TextBox, this.fee4DecimalPlace);
                this.ToSetDecimalPlaces(this.Fee5TextBox, this.fee5DecimalPlace);
                this.ToSetDecimalPlaces(this.Fee6TextBox, this.fee6DecimalPlace);

                this.AssignValueToFeeTextBox(null, feeTableRow);

                this.CheckLabelValues();

                ////TextBoxValidation
                this.ValidateEFfieldsTextBoxs(this.Fee1TextBox, this.fee1Formula, this.Fee1Label.Text.Trim(), this.PermissionFiled.editPermission, this.formMasterPermissionEdit);
                this.ValidateEFfieldsTextBoxs(this.Fee2TextBox, this.fee2Formula, this.Fee2Label1.Text.Trim(), this.PermissionFiled.editPermission, this.formMasterPermissionEdit);
                this.ValidateEFfieldsTextBoxs(this.Fee3TextBox, this.fee3Formula, this.Fee3Label.Text.Trim(), this.PermissionFiled.editPermission, this.formMasterPermissionEdit);
                this.ValidateEFfieldsTextBoxs(this.Fee4TextBox, this.fee4Formula, this.Fee4Label.Text.Trim(), this.PermissionFiled.editPermission, this.formMasterPermissionEdit);
                this.ValidateEFfieldsTextBoxs(this.Fee5TextBox, this.fee5Formula, this.Fee5Label.Text.Trim(), this.PermissionFiled.editPermission, this.formMasterPermissionEdit);
                this.ValidateEFfieldsTextBoxs(this.Fee6TextBox, this.fee6Formula, this.Fee6Label.Text.Trim(), this.PermissionFiled.editPermission, this.formMasterPermissionEdit);

                this.LoadEFCalucaltionGrid(null, feeTableRow);
            }
        }

        #endregion FeeCalculation

        #region AssignValueToFeeLable

        /// <summary>
        /// AssignValueToFeeLable
        /// </summary>
        private void AssignValueToFeeLable(F49910InstrumentHeaderDataSet.f49901RecorderDetailsDataTableRow recorderRow, F49910InstrumentHeaderDataSet.f49910feeDetailsTableRow feeRow)
        {
            if (recorderRow != null)
            {
                if (string.IsNullOrEmpty(recorderRow.L01.ToString()))
                {
                    this.Fee1Label.Text = string.Empty;

                }
                else
                {
                    this.Fee1Label.Text = recorderRow.L01.ToString() + ":";


                }

                this.SetToolTip(this.ToolTipLabel, this.Fee1Label);

                if (string.IsNullOrEmpty(recorderRow.L02.ToString()))
                {
                    this.Fee2Label1.Text = string.Empty;
                }
                else
                {
                    this.Fee2Label1.Text = recorderRow.L02.ToString() + ":";
                }


                this.SetToolTip(this.ToolTipLabel, this.Fee2Label1);


                if (string.IsNullOrEmpty(recorderRow.L03.ToString()))
                {
                    this.Fee3Label.Text = string.Empty;
                }
                else
                {
                    this.Fee3Label.Text = recorderRow.L03.ToString() + ":";
                }

                this.SetToolTip(this.ToolTipLabel, this.Fee3Label);

                if (string.IsNullOrEmpty(recorderRow.L04.ToString()))
                {
                    this.Fee4Label.Text = string.Empty;
                }
                else
                {
                    this.Fee4Label.Text = recorderRow.L04.ToString() + ":";
                }

                this.SetToolTip(this.ToolTipLabel, this.Fee4Label);

                if (string.IsNullOrEmpty(recorderRow.L05.ToString()))
                {
                    this.Fee5Label.Text = string.Empty;
                }
                else
                {
                    this.Fee5Label.Text = recorderRow.L05.ToString() + ":";
                }
                this.SetToolTip(this.ToolTipLabel, this.Fee5Label);

                if (string.IsNullOrEmpty(recorderRow.L06.ToString()))
                {
                    this.Fee6Label.Text = string.Empty;
                }
                else
                {
                    this.Fee6Label.Text = recorderRow.L06.ToString() + ":";
                }
                this.SetToolTip(this.ToolTipLabel, this.Fee6Label);
            }
            else
            {
                if (feeRow.IsL01Null())
                {
                    this.Fee1Label.Text = string.Empty;
                }
                else
                {
                    this.Fee1Label.Text = feeRow.L01.ToString() + ":";
                }

                if (feeRow.IsL02Null())
                {
                    this.Fee2Label1.Text = string.Empty;
                }
                else
                {
                    this.Fee2Label1.Text = feeRow.L02.ToString() + ":";
                }

                if (feeRow.IsL03Null())
                {
                    this.Fee3Label.Text = string.Empty;
                }
                else
                {
                    this.Fee3Label.Text = feeRow.L03.ToString() + ":";
                }

                if (feeRow.IsL04Null())
                {
                    this.Fee4Label.Text = string.Empty;
                }
                else
                {
                    this.Fee4Label.Text = feeRow.L04.ToString() + ":";
                }

                if (feeRow.IsL05Null())
                {
                    this.Fee5Label.Text = string.Empty;
                }
                else
                {
                    this.Fee5Label.Text = feeRow.L05.ToString() + ":";
                }

                if (feeRow.IsL06Null())
                {
                    this.Fee6Label.Text = string.Empty;
                }
                else
                {
                    this.Fee6Label.Text = feeRow.L06.ToString() + ":";
                }
            }
        }

        #endregion AssignValueToFeeLable

        #region SetFormulaTovariable

        /// <summary>
        /// SetFormulaTovariable
        /// </summary>
        private void SetFormulaTovariable(F49910InstrumentHeaderDataSet.f49901RecorderDetailsDataTableRow recorderRow, F49910InstrumentHeaderDataSet.f49910feeDetailsTableRow feeRow)
        {
            if (recorderRow != null)
            {
                this.fee1Formula = recorderRow.F01;
                this.fee2Formula = recorderRow.F02;
                this.fee3Formula = recorderRow.F03;
                this.fee4Formula = recorderRow.F04;
                this.fee5Formula = recorderRow.F05;
                this.fee6Formula = recorderRow.F06;
                this.vformula = recorderRow.VFormula;
            }
            else
            {
                if (feeRow.IsF01Null())
                {
                    this.fee1Formula = string.Empty;
                }
                else
                {
                    this.fee1Formula = feeRow.F01;
                }

                if (feeRow.IsF02Null())
                {
                    this.fee2Formula = string.Empty;
                }
                else
                {
                    this.fee2Formula = feeRow.F02;
                }

                if (feeRow.IsF03Null())
                {
                    this.fee3Formula = string.Empty;
                }
                else
                {
                    this.fee3Formula = feeRow.F03;
                }

                if (feeRow.IsF04Null())
                {
                    this.fee4Formula = string.Empty;
                }
                else
                {
                    this.fee4Formula = feeRow.F04;
                }

                if (feeRow.IsF05Null())
                {
                    this.fee5Formula = string.Empty;
                }
                else
                {
                    this.fee5Formula = feeRow.F05;
                }

                if (feeRow.IsF06Null())
                {
                    this.fee6Formula = string.Empty;
                }
                else
                {
                    this.fee6Formula = feeRow.F06;
                }

                if (string.IsNullOrEmpty(feeRow.VFormula))
                {
                    this.vformula = string.Empty;
                }
                else
                {
                    this.vformula = feeRow.VFormula;
                }
            }
        }

        #endregion SetFormulaTovariable

        #region  SetDecimalVariable

        /// <summary>
        /// SetDecimalVariable
        /// </summary>
        private void SetDecimalVariable(F49910InstrumentHeaderDataSet.f49901RecorderDetailsDataTableRow recorderRow, F49910InstrumentHeaderDataSet.f49910feeDetailsTableRow feeRow)
        {
            if (recorderRow != null)
            {
                int.TryParse(recorderRow.D01.ToString(), out this.fee1DecimalPlace);
                int.TryParse(recorderRow.D02.ToString(), out this.fee2DecimalPlace);
                int.TryParse(recorderRow.D03.ToString(), out this.fee3DecimalPlace);
                int.TryParse(recorderRow.D04.ToString(), out this.fee4DecimalPlace);
                int.TryParse(recorderRow.D05.ToString(), out this.fee5DecimalPlace);
                int.TryParse(recorderRow.D06.ToString(), out this.fee6DecimalPlace);
            }
            else
            {
                if (feeRow.IsD01Null())
                {
                    this.fee1DecimalPlace = 0;
                }
                else
                {
                    int.TryParse(feeRow.D01.ToString(), out this.fee1DecimalPlace);
                }

                if (feeRow.IsD02Null())
                {
                    this.fee2DecimalPlace = 0;
                }
                else
                {
                    int.TryParse(feeRow.D02.ToString(), out this.fee2DecimalPlace);
                }

                if (feeRow.IsD03Null())
                {
                    this.fee3DecimalPlace = 0;
                }
                else
                {
                    int.TryParse(feeRow.D03.ToString(), out this.fee3DecimalPlace);
                }

                if (feeRow.IsD04Null())
                {
                    this.fee4DecimalPlace = 0;
                }
                else
                {
                    int.TryParse(feeRow.D04.ToString(), out this.fee4DecimalPlace);
                }

                if (feeRow.IsD05Null())
                {
                    this.fee5DecimalPlace = 0;
                }
                else
                {
                    int.TryParse(feeRow.D05.ToString(), out this.fee5DecimalPlace);
                }

                if (feeRow.IsD06Null())
                {
                    this.fee6DecimalPlace = 0;
                }
                else
                {
                    int.TryParse(feeRow.D06.ToString(), out this.fee6DecimalPlace);
                }
            }
        }

        #endregion  SetDecimalVariable

        #region ToSetDecimalPlaces

        /// <summary>
        /// Toes the set decimal places.
        /// </summary>
        /// <param name="textBoxControls">The text box controls.</param>
        /// <param name="decimalPlacesValue">The decimal places value.</param>
        private void ToSetDecimalPlaces(Control textBoxControls, int decimalPlacesValue)
        {
            if (textBoxControls.GetType() == typeof(TerraScanTextBox))
            {
                TerraScanTextBox currentTextBox = (TerraScanTextBox)textBoxControls;

                switch (decimalPlacesValue)
                {
                    case 1:
                        currentTextBox.TextCustomFormat = SharedFunctions.GetResourceString("TextFormat1");
                        currentTextBox.Precision = 1;
                        break;
                    case 2:
                        currentTextBox.TextCustomFormat = SharedFunctions.GetResourceString("TextFormat2");
                        currentTextBox.Precision = 2;
                        break;
                    case 3:
                        currentTextBox.TextCustomFormat = SharedFunctions.GetResourceString("TextFormat3");
                        currentTextBox.Precision = 3;
                        break;
                    case 4:
                        currentTextBox.TextCustomFormat = SharedFunctions.GetResourceString("TextFormat4");
                        currentTextBox.Precision = 4;
                        break;
                    default:
                        currentTextBox.TextCustomFormat = SharedFunctions.GetResourceString("TextFormat5");
                        currentTextBox.Precision = 0;
                        break;
                }
            }
        }

        #endregion ToSetDecimalPlaces

        #region AssignValueToFeeTextBox

        /// <summary>
        /// AssignValueToFeeTextBox
        /// </summary>
        private void AssignValueToFeeTextBox(F49910InstrumentHeaderDataSet.f49901RecorderDetailsDataTableRow recorderRow, F49910InstrumentHeaderDataSet.f49910feeDetailsTableRow feeRow)
        {
            this.Fee1TextBox.BackColor = Color.White;
            this.Fee2TextBox.BackColor = Color.White;
            this.Fee3TextBox.BackColor = Color.White;
            this.Fee4TextBox.BackColor = Color.White;
            this.Fee5TextBox.BackColor = Color.White;
            this.Fee6TextBox.BackColor = Color.White;
            decimal d = 0.00M;
            if (recorderRow != null)
            {
                if (recorderRow.IsFeeNull())
                {
                    this.FeeTotalTextBox.Text = string.Empty;
                }
                else
                {
                    this.FeeTotalTextBox.Text = recorderRow.Fee.ToString();
                }

                if (recorderRow.IsV01Null())
                {
                    this.Fee1TextBox.Text = string.Empty;
                }
                else
                {
                    this.Fee1TextBox.Text = recorderRow.V01.ToString();
                }

                if (recorderRow.IsV02Null())
                {
                    this.Fee2TextBox.Text = string.Empty;
                }
                else
                {
                    this.Fee2TextBox.Text = recorderRow.V02.ToString();
                }

                if (recorderRow.IsV03Null())
                {
                    this.Fee3TextBox.Text = string.Empty;
                }
                else
                {
                    this.Fee3TextBox.Text = recorderRow.V03.ToString();
                }

                if (recorderRow.IsV04Null())
                {
                    this.Fee4TextBox.Text = string.Empty;
                }
                else
                {
                    this.Fee4TextBox.Text = recorderRow.V04.ToString();
                }

                if (recorderRow.IsV05Null())
                {
                    this.Fee5TextBox.Text = string.Empty;
                }
                else
                {
                    this.Fee5TextBox.Text = recorderRow.V05.ToString();
                }

                if (recorderRow.IsV06Null())
                {
                    this.Fee6TextBox.Text = string.Empty;
                }
                else
                {
                    this.Fee6TextBox.Text = recorderRow.V06.ToString();
                }
            }
            else
            {
                //this.Fee1TextBox.Text = string.Empty;
                //this.Fee2TextBox.Text = string.Empty;
                //this.Fee3TextBox.Text = string.Empty;
                //this.Fee4TextBox.Text = string.Empty;
                //this.Fee5TextBox.Text = string.Empty;
                //this.Fee6TextBox.Text = string.Empty;
                this.ClearFeeTextBoxes();
                this.FeeTotalTextBox.Text = string.Empty;
            }
        }

        #endregion AssignValueToFeeTextBox

        #region ValidateEFfieldsTextBoxs

        /// <summary>
        /// Validate Eevent Fee TextBoxs.
        /// </summary>
        /// <param name="eventFieldstextBoxControls">eventFieldstextBoxControls</param>
        /// <param name="eventFeeFieldsFormula">eventFeeFieldsFormula</param>
        /// <param name="eventFeeLabelText">eventFeeLabelText</param>
        /// <param name="userPermission">userPermission</param>
        /// <param name="masterFormPermission">masterFormPermission</param>
        private void ValidateEFfieldsTextBoxs(Control eventFieldstextBoxControls, string eventFeeFieldsFormula, string eventFeeLabelText, bool userPermission, bool masterFormPermission)
        {
            if (eventFieldstextBoxControls.GetType() == typeof(TerraScanTextBox))
            {
                TerraScanTextBox currentTextBox = (TerraScanTextBox)eventFieldstextBoxControls;

                ////here based on the permission and formula value we have to edit the textbox controls
                if (userPermission && masterFormPermission)
                {
                    if (!string.IsNullOrEmpty(eventFeeFieldsFormula))
                    {
                        currentTextBox.LockKeyPress = true;
                        currentTextBox.ForeColor = System.Drawing.Color.DarkGray;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(eventFeeLabelText))
                        {
                            currentTextBox.LockKeyPress = true;
                        }
                        else
                        {
                            currentTextBox.LockKeyPress = false;
                        }

                        currentTextBox.ForeColor = System.Drawing.Color.Black;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(eventFeeFieldsFormula))
                    {
                        currentTextBox.ForeColor = System.Drawing.Color.DarkGray;
                    }
                    else
                    {
                        currentTextBox.ForeColor = System.Drawing.Color.Black;
                    }

                    currentTextBox.LockKeyPress = true;
                }
            }
        }

        #endregion ValidateEFfieldsTextBoxs

        #region ToCheckReqFieldeinFormula

        /// <summary>
        /// To check req fields are as in formula.
        /// </summary>
        /// <param name="currentFormula">The current formula.</param>
        /// <returns>returns a boolean value</returns>
        private bool ToCheckReqFieldeinFormula(string currentFormula)
        {
            bool validValue = false;
            int validText = 1;

            if (((currentFormula.IndexOf(SharedFunctions.GetResourceString("V01")) >= 0) || (currentFormula.IndexOf(SharedFunctions.GetResourceString("V02")) >= 0) || (currentFormula.IndexOf(SharedFunctions.GetResourceString("V03")) >= 0) || (currentFormula.IndexOf(SharedFunctions.GetResourceString("V04")) >= 0) || (currentFormula.IndexOf(SharedFunctions.GetResourceString("V05")) >= 0) || (currentFormula.IndexOf(SharedFunctions.GetResourceString("V06")) >= 0)))
            {
                if (currentFormula.IndexOf(SharedFunctions.GetResourceString("V01")) >= 0)
                {
                    if (!string.IsNullOrEmpty(this.Fee1TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                    }
                    else
                    {
                        validText = -1;
                        return false;
                    }
                }

                if (currentFormula.IndexOf(SharedFunctions.GetResourceString("V02")) >= 0)
                {
                    if (!string.IsNullOrEmpty(this.Fee2TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                    }
                    else
                    {
                        validText = -1;
                        return false;
                    }
                }

                if (currentFormula.IndexOf(SharedFunctions.GetResourceString("V03")) >= 0)
                {
                    if (!string.IsNullOrEmpty(this.Fee3TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                    }
                    else
                    {
                        validText = -1;
                        return false;
                    }
                }

                if (currentFormula.IndexOf(SharedFunctions.GetResourceString("V04")) >= 0)
                {
                    if (!string.IsNullOrEmpty(this.Fee4TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                    }
                    else
                    {
                        validText = -1;
                        return false;
                    }
                }

                if (currentFormula.IndexOf(SharedFunctions.GetResourceString("V05")) >= 0)
                {
                    if (!string.IsNullOrEmpty(this.Fee5TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                    }
                    else
                    {
                        validText = -1;
                        return false;
                    }
                }

                if (currentFormula.IndexOf(SharedFunctions.GetResourceString("V06")) >= 0)
                {
                    if (!string.IsNullOrEmpty(this.Fee6TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                    }
                    else
                    {
                        validText = -1;
                        return false;
                    }
                }

                return validValue;
            }
            else
            {
                return true;
            }
        }

        #endregion ToCheckReqFieldeinFormula

        #region TextBoxChange

        /// <summary>
        /// Texts the box change.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TextBoxChange(object sender, EventArgs e)
        {
            try
            {

                this.ToSetDecimalPlaces(this.Fee1TextBox, this.fee1DecimalPlace);
                this.ToSetDecimalPlaces(this.Fee2TextBox, this.fee2DecimalPlace);
                this.ToSetDecimalPlaces(this.Fee3TextBox, this.fee3DecimalPlace);
                this.ToSetDecimalPlaces(this.Fee4TextBox, this.fee4DecimalPlace);
                this.ToSetDecimalPlaces(this.Fee5TextBox, this.fee5DecimalPlace);
                this.ToSetDecimalPlaces(this.Fee6TextBox, this.fee6DecimalPlace);

                TerraScanTextBox tempTextBox = (TerraScanTextBox)sender;
                if (!this.flagLoadOnProcess)
                {
                    if (!string.IsNullOrEmpty(tempTextBox.Text.Trim().Replace(" ", "")) && !string.IsNullOrEmpty(tempTextBox.Tag.ToString().Trim().Replace(" ", "")))
                    {
                        if (tempTextBox.Text.Trim().Contains("$"))
                        {
                            decimal decValue;
                            decimal.TryParse(tempTextBox.Text.Trim().Replace("$", "").Replace(" ", ""), out decValue);
                            if (this.EventTotalFeeCalucationGrid.Rows.Count != 0)
                            {
                                this.EventTotalFeeCalucationGrid.Rows[0].Cells[tempTextBox.Tag.ToString()].Value = decValue;
                            }
                        }
                        else
                        {
                            decimal decValue1;
                            decimal.TryParse(tempTextBox.Text.Trim().Replace("$", "").Replace(" ", ""), out decValue1);
                            if (this.EventTotalFeeCalucationGrid.Rows.Count > 0)
                            {
                                this.EventTotalFeeCalucationGrid.Rows[0].Cells[tempTextBox.Tag.ToString()].Value = decValue1;
                            }
                        }
                    }
                    else
                    {
                        this.EventTotalFeeCalucationGrid.Rows[0].Cells[tempTextBox.Tag.ToString()].Value = DBNull.Value;
                    }

                    this.SetFormula();
                    this.EditRecord();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion TextBoxChange

        #region SetFormula

        /// <summary>
        /// SetFormula
        /// </summary>
        private void SetFormula()
        {
            this.unsavedChangeExists = true;
            if (this.EventTotalFeeCalucationGrid.Rows.Count != 0)
            {
                this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns[SharedFunctions.GetResourceString("CellV01")].Formula = this.fee1Formula;
                this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns[SharedFunctions.GetResourceString("CellV02")].Formula = this.fee2Formula;
                this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns[SharedFunctions.GetResourceString("CellV03")].Formula = this.fee3Formula;
                this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns[SharedFunctions.GetResourceString("CellV04")].Formula = this.fee4Formula;
                this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns[SharedFunctions.GetResourceString("CellV05")].Formula = this.fee5Formula;
                this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns[SharedFunctions.GetResourceString("CellV06")].Formula = this.fee6Formula;
                this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns[SharedFunctions.GetResourceString("VFormula")].Formula = this.vformula;
            }
        }

        #endregion SetFormula

        #region EFLabelTextBoxLeave

        /// <summary>
        /// EFLabelTextBoxLeave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void EFLabelTextBoxLeave(object sender, EventArgs e)
        {
            try
            {
                this.flagLoadOnProcess = true;
                if (this.unsavedChangeExists)
                {
                    this.AssignCalculatedValue();
                }

                this.flagLoadOnProcess = false;
                this.CheckLabelValues();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion EFLabelTextBoxLeave

        #region AssignCalculatedValue

        /// <summary>
        /// Assigns the calculated value.
        /// </summary>
        private void AssignCalculatedValue()
        {
            if (!string.IsNullOrEmpty(this.fee1Formula))
            {
                if (this.ToCheckReqFieldeinFormula(this.fee1Formula))
                {
                    this.Fee1TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV01")].Value.ToString();
                }
                else
                {
                    this.Fee1TextBox.Text = string.Empty;
                }

                this.Fee1TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.Fee1TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV01")].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.fee2Formula))
            {
                if (this.ToCheckReqFieldeinFormula(this.fee2Formula))
                {
                    this.Fee2TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV02")].Value.ToString();
                }
                else
                {
                    this.Fee2TextBox.Text = string.Empty;
                }

                this.Fee2TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.Fee2TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV02")].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.fee3Formula))
            {
                if (this.ToCheckReqFieldeinFormula(this.fee3Formula))
                {
                    this.Fee3TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV03")].Value.ToString();
                }
                else
                {
                    this.Fee3TextBox.Text = string.Empty;
                }

                this.Fee3TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.Fee3TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV03")].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.fee4Formula))
            {
                if (this.ToCheckReqFieldeinFormula(this.fee4Formula))
                {
                    this.Fee4TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV04")].Value.ToString();
                }
                else
                {
                    this.Fee4TextBox.Text = string.Empty;
                }

                this.Fee4TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.Fee4TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV04")].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.fee5Formula))
            {
                if (this.ToCheckReqFieldeinFormula(this.fee5Formula))
                {
                    this.Fee5TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV05")].Value.ToString();
                }
                else
                {
                    this.Fee5TextBox.Text = string.Empty;
                }

                this.Fee5TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.Fee5TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV05")].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.fee6Formula))
            {
                if (this.ToCheckReqFieldeinFormula(this.fee6Formula))
                {
                    this.Fee6TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV06")].Value.ToString();
                }
                else
                {
                    this.Fee6TextBox.Text = string.Empty;
                }

                this.Fee6TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.Fee6TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV06")].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.vformula))
            {
                if (this.ToCheckReqFieldeinFormula(this.vformula))
                {
                    this.FeeTotalTextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("VFormula")].Value.ToString();
                    //Removed the tender Type default 'check'. TSCO #13410
                    //Change the tender Type default 'check'.
                    //this.PaymentTotalTextBox.Text = this.FeeTotalTextBox.Text;
                    //decimal bal;
                    //decimal.TryParse(this.PaymentTotalTextBox.Text, out bal);  
                    //this.PaymentEngineUserControl.LoadInstrumentDefaultValue(string.Empty, bal);
                    this.CalculateBalance();
                }
                else
                {
                    this.FeeTotalTextBox.Text = string.Empty;
                }
            }

            this.unsavedChangeExists = false;
        }

        #endregion AssignCalculatedValue

        #region LoadEFCalucaltionGrid

        /// <summary>
        /// Used to Load the Event calucaltion Grid
        /// </summary>
        private void LoadEFCalucaltionGrid(F49910InstrumentHeaderDataSet.f49901RecorderDetailsDataTableRow recorderRow, F49910InstrumentHeaderDataSet.f49910feeDetailsTableRow feeRow)
        {
            DataSet tempdataset = new DataSet();
            DataTable eventFeeDataTable = new DataTable(SharedFunctions.GetResourceString("ListMiscImprovements"));
            eventFeeDataTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("CellV01"), typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("CellV02"), typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("CellV03"), typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("CellV04"), typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("CellV05"), typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("CellV06"), typeof(decimal)));

            eventFeeDataTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("VFormula"), typeof(decimal)));

            DataRow dr = eventFeeDataTable.NewRow();

            eventFeeDataTable.Rows.Add(dr);

            tempdataset.Tables.Add(eventFeeDataTable);

            this.EventTotalFeeCalucationGrid.DataSource = tempdataset;

            if (recorderRow != null)
            {
                if (recorderRow.IsV01Null())
                {
                    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV01")].Value = 0.00;
                }
                else
                {
                    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV01")].Value = recorderRow.V01;
                }

                if (recorderRow.IsV02Null())
                {
                    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV02")].Value = 0.00;
                }
                else
                {
                    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV02")].Value = recorderRow.V02;
                }

                if (recorderRow.IsV03Null())
                {
                    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV03")].Value = 0.00;
                }
                else
                {
                    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV03")].Value = recorderRow.V03;
                }

                if (recorderRow.IsV04Null())
                {
                    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV04")].Value = 0.00;
                }
                else
                {
                    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV04")].Value = recorderRow.V04;
                }

                if (recorderRow.IsV05Null())
                {
                    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV05")].Value = 0.00;
                }
                else
                {
                    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV05")].Value = recorderRow.V05;
                }

                if (recorderRow.IsV06Null())
                {
                    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV06")].Value = 0.00;
                }
                else
                {
                    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV06")].Value = recorderRow.V06;
                }
            }
            else
            {
                ////if (feeRow.IsV01Null())
                ////{
                ////    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV01")].Value = 0.00;
                ////}
                ////else
                ////{
                ////    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV01")].Value = feeRow.V01;
                ////}

                ////if (feeRow.IsV02Null())
                ////{
                ////    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV02")].Value = 0.00;
                ////}
                ////else
                ////{
                ////    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV02")].Value = feeRow.V02;
                ////}

                ////if (feeRow.IsV03Null())
                ////{
                ////    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV03")].Value = 0.00;
                ////}
                ////else
                ////{
                ////    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV03")].Value = feeRow.V03;
                ////}

                ////if (feeRow.IsV04Null())
                ////{
                ////    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV04")].Value = 0.00;
                ////}
                ////else
                ////{
                ////    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV04")].Value = feeRow.V04;
                ////}

                ////if (feeRow.IsV05Null())
                ////{
                ////    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV05")].Value = 0.00;
                ////}
                ////else
                ////{
                ////    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV05")].Value = feeRow.V05;
                ////}

                ////if (feeRow.IsV06Null())
                ////{
                ////    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV06")].Value = 0.00;
                ////}
                ////else
                ////{
                ////    this.EventTotalFeeCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("CellV06")].Value = feeRow.V06;
                ////}
            }
        }

        #endregion LoadEFCalucaltionGrid

        #region CheckLabelValues

        /// <summary>
        /// checking empty labels and making the respective textbox empty
        /// </summary>
        private void CheckLabelValues()
        {
            if (string.IsNullOrEmpty(this.Fee1Label.Text.Trim()))
            {
                this.Fee1TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.Fee2Label1.Text.Trim()))
            {
                this.Fee2TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.Fee3Label.Text.Trim()))
            {
                this.Fee3TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.Fee4Label.Text.Trim()))
            {
                this.Fee4TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.Fee5Label.Text.Trim()))
            {
                this.Fee5TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.Fee6Label.Text.Trim()))
            {
                this.Fee6TextBox.Text = string.Empty;
            }
        }

        #endregion CheckLabelValues

        #endregion FeePart

        #region CustomizePaymentItemsGridView

        /// <summary>
        /// This Method used to  set dataproperty name and column displayindex and paymentsdatatable initialization
        /// CustomizeErrorGridView
        /// </summary>
        private void CustomizePaymentItemsGridView()
        {
        }

        #endregion CustomizePaymentItemsGridView

        #region WOLComboBox

        /// <summary>
        /// LoadWOLComboBox
        /// </summary>
        private void LoadWOLComboBox()
        {
            System.Collections.Hashtable datas = new System.Collections.Hashtable();

            datas.Add(SharedFunctions.GetResourceString("No"), 0);

            datas.Add(SharedFunctions.GetResourceString("Yes"), 1);

            this.typeComboData.LoadGeneralComboData(datas);
            this.WOLComboBox.DisplayMember = this.typeComboData.ComboBoxDataTable.KeyNameColumn.ColumnName;
            this.WOLComboBox.ValueMember = this.typeComboData.ComboBoxDataTable.KeyIdColumn.ColumnName;
            this.WOLComboBox.DataSource = this.typeComboData.ComboBoxDataTable;
        }

        #endregion WOLComboBox

        #region LoadInstrumentTypeComboBox

        /// <summary>
        /// LoadInstrumentTypeComboBox
        /// </summary>
        private void LoadInstrumentTypeComboBox()
        {
            this.ReviewedFromComboBox1.SelectedValueChanged -= new System.EventHandler(this.ReviewedFromComboBox1_SelectionEvent);

            ////this.instrumentHeaderDataSetObject.EnforceConstraints = true;
            //// this.InstrumentTypeComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ////this.InstrumentTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.instrumentHeaderDataSetObject = this.form49910Controller.WorkItem.F49910_GetInstrumentTypeDetails();
            if (this.instrumentHeaderDataSetObject.f49910InstrumentTypeDataTable.Rows.Count > 0)
            {
                this.InstrumentTypeComboBox.DisplayMember = this.instrumentHeaderDataSetObject.f49910InstrumentTypeDataTable.InstrumentTypeColumn.ToString();
                this.InstrumentTypeComboBox.ValueMember = this.instrumentHeaderDataSetObject.f49910InstrumentTypeDataTable.ITypeIDColumn.ToString();
                this.InstrumentTypeComboBox.DataSource = this.instrumentHeaderDataSetObject.f49910InstrumentTypeDataTable;
                //// this.InstrumentTypeComboBox.SelectedText = "<Select>";
                this.InstrumentTypeComboBox.SelectedValue = -1;
            }

            if (this.instrumentHeaderDataSetObject.f49910_pclst_CustomerValue.Rows.Count > 0)
            {
                this.CustomerComboBox.DisplayMember = this.instrumentHeaderDataSetObject.f49910_pclst_CustomerValue.NameColumn.ToString();
                this.CustomerComboBox.ValueMember = this.instrumentHeaderDataSetObject.f49910_pclst_CustomerValue.CustomerIDColumn.ToString();
                this.CustomerComboBox.DataSource = this.instrumentHeaderDataSetObject.f49910_pclst_CustomerValue;
                ////this.CustomerComboBox.SelectedText = "<<Select>>";
                this.CustomerComboBox.SelectedValue = 0;
            }

            if (this.instrumentHeaderDataSetObject.f49910_pclst_GrantList.Rows.Count > 0)
            {
                this.ReviewedFromComboBox1.DisplayMember = this.instrumentHeaderDataSetObject.f49910_pclst_GrantList.NameColumn.ToString();
                this.ReviewedFromComboBox1.ValueMember = this.instrumentHeaderDataSetObject.f49910_pclst_GrantList.GrantIDColumn.ToString();
                this.ReviewedFromComboBox1.DataSource = this.instrumentHeaderDataSetObject.f49910_pclst_GrantList;
                ////this.ReviewedFromComboBox1.SelectedText = "<Select>";
                this.ReviewedFromComboBox1.SelectedValue = 0;
            }

            if (this.instrumentHeaderDataSetObject.f49910_pclst_ExemptionType.Rows.Count > 0)
            {
                this.ExemptionComboBox.DisplayMember = this.instrumentHeaderDataSetObject.f49910_pclst_ExemptionType.ExemptionTypeColumn.ToString();
                this.ExemptionComboBox.ValueMember = this.instrumentHeaderDataSetObject.f49910_pclst_ExemptionType.ETypeIDColumn.ToString();
                this.ExemptionComboBox.DataSource = this.instrumentHeaderDataSetObject.f49910_pclst_ExemptionType;
                ////this.ExemptionComboBox.SelectedText = "<Select>";
                this.ExemptionComboBox.SelectedValue = 0;
            }

            this.ReviewedFromComboBox1.SelectedValueChanged += new System.EventHandler(this.ReviewedFromComboBox1_SelectionEvent);
        }

        #endregion LoadInstrumentTypeComboBox

        #region blockControls

        #region BlockPanel
        /// <summary>
        /// blockPanel
        /// </summary>
        /// <param name="show">show</param>
        private void BlockPanel(bool show)
        {
            this.InstrumentPanel.Enabled = show;
            this.BookandPagePanel.Enabled = show;
            this.FileDatePanel.Enabled = show;
            this.FileTimePanel.Enabled = show;
            this.InstrumentDatePanel.Enabled = show;
            this.UserPanel.Enabled = show;
            this.ReviewedPanel.Enabled = show;
            this.InstrumentTypePanel.Enabled = show;
            this.CustomerPanel.Enabled = show;
            this.ReviewedFrompanel.Enabled = show;
            this.ExemptionTypePanel.Enabled = show;
            this.DeliverToPanel.Enabled = show;
            this.SerialNumberPanel.Enabled = show;
            this.WOLPanel.Enabled = show;
            this.Fee1Panel.Enabled = show;
            this.Fee2Panel.Enabled = show;
            this.Fee3Panel.Enabled = show;
            this.Fee4Panel.Enabled = show;
            this.Fee5Panel.Enabled = show;
            this.Fee6Panel.Enabled = show;
            this.PaymentEnginePanel.Enabled = show;
        }

        #endregion BlockPanel

        #region BlockTextbox

        /// <summary>
        /// BlockTextbox
        /// </summary>
        /// <param name="show">show</param>
        private void BlockTextbox(bool show)
        {
            this.InstrumentTextBox.LockKeyPress = show;
            this.BookandPageTextBox.LockKeyPress = show;
            this.FileDateTextBox.LockKeyPress = show;
            this.fileTimeTextBox.LockKeyPress = show;
            this.InstrumentDateTextBox.LockKeyPress = show;
            this.DeliverToTextBox.LockKeyPress = show;
            this.SerialNumberTextBox.LockKeyPress = show;
        }

        #endregion BlockTextbox

        #region BlockComboBox

        /// <summary>
        /// BlockComboBox
        /// </summary>
        /// <param name="show">show</param>
        private void BlockComboBox(bool show)
        {
            this.InstrumentTypeComboBox.Enabled = !show;
            this.ReviewedFromComboBox1.Enabled = !show;
            this.CustomerComboBox.Enabled = !show;
            this.ExemptionComboBox.Enabled = !show;
            this.WOLComboBox.Enabled = !show;
        }

        #endregion BlockComboBox

        #region BlockButton

        /// <summary>
        /// BlockButton
        /// </summary>
        /// <param name="show">show</param>
        private void BlockButton(bool show)
        {


            // Added permission for the copy button
            //if (this.slicePermissionField.newPermission || this.formMasterNew && !show)
            //{
            //    this.CopyButton.Enabled = true;
            //}
            //else
            //{
            //    this.CopyButton.Enabled = false;
            //}
            this.CopyButton.Enabled = this.formMasterNew;


            this.FileDateCalendar.Enabled = show;
            this.InstrumentDateCalendar.Enabled = show;
            this.InstrumentTypeButton.Enabled = show;
            this.CustomerButton.Enabled = show;
            this.ReviewedButton.Enabled = show;
            this.ExemptionTypeButton.Enabled = show;
        }

        #endregion BlockButton

        #region EmptyLable

        /// <summary>
        /// EmptyLable
        /// </summary>
        private void EmptyLable()
        {
            this.Fee1Label.Text = string.Empty;
            this.Fee2Label1.Text = string.Empty;
            this.Fee3Label.Text = string.Empty;
            this.Fee4Label.Text = string.Empty;
            this.Fee5Label.Text = string.Empty;
            this.Fee6Label.Text = string.Empty;
        }

        #endregion EmptyLable

        #endregion blockControls

        #region SetbackColor

        /// <summary>
        /// SetbackColor
        /// </summary>
        private void SetbackColor()
        {
            //this.InstrumentTextBox.BackColor = Color.White;
            this.BookandPageTextBox.BackColor = Color.White;
            this.FileDateTextBox.BackColor = Color.White;
            this.fileTimeTextBox.BackColor = Color.White;
            this.InstrumentDateTextBox.BackColor = Color.White;
            this.ReviewedFromComboBox1.BackColor = Color.White;
            this.DeliverToTextBox.BackColor = Color.White;
            this.SerialNumberTextBox.BackColor = Color.White;
            this.Fee1TextBox.BackColor = Color.White;
            this.Fee2TextBox.BackColor = Color.White;
            this.Fee3TextBox.BackColor = Color.White;
            this.Fee4TextBox.BackColor = Color.White;
            this.Fee5TextBox.BackColor = Color.White;
            this.Fee6TextBox.BackColor = Color.White;
        }

        #endregion SetbackColor

        #region CheckErrors
        /// <summary>
        /// CheckErrors
        /// </summary>
        /// <param name="formNo">formNo</param>
        /// <returns>bool</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            if (this.CheckMaxValue())
            {
                Control requiredControl;
                requiredControl = this.CheckRequiredFields();
                if (requiredControl != null)
                {
                    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("Recorder");
                    requiredControl.Focus();
                    return sliceValidationFields;
                }

                // To validate whether the save can be made
                this.SaveInstrumentHeader();

                int ppaymentId = 0;

                if (this.newInstrument)
                {
                    ppaymentId = this.form49910Controller.WorkItem.F49910CheckInstrumentHeaderDetails(0, tempDataSet.GetXml(), null, TerraScanCommon.UserId);
                }
                else
                {
                    ppaymentId = this.form49910Controller.WorkItem.F49910CheckInstrumentHeaderDetails(this.keyId, tempDataSet.GetXml(), null, TerraScanCommon.UserId);
                }

                if (ppaymentId == -1)
                {
                    MessageBox.Show("Instrument number should be unique", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    sliceValidationFields.ErrorMessage = string.Empty;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.DisableNewMethod = true;
                    return sliceValidationFields;
                }




                //if(this.FeeTotalTextBox.Text.ToString()!=this.PaymentTotalTextBox.Text.ToString())
                //{
                //    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("PaymentField");
                //    this.PaymentEngineUserControl.Focus();
                //    return sliceValidationFields;
                //}

                return sliceValidationFields;
            }
            else
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F49910MaxLimit");
                return sliceValidationFields;
            }
        }

        #endregion CheckErrors

        #region CheckMaxValue

        /// <summary>
        /// CheckMaxValue
        /// </summary>
        /// <returns>bool</returns>
        private bool CheckMaxValue()
        {
            decimal value = 0;

            // Commented coding check value instead of its length


            if (this.Fee1TextBox.Text.Trim() != null)
            {
                decimal.TryParse(this.Fee1TextBox.Text.Trim().Replace(",", "").Replace("$", ""), out value);

                if (Decimal.MaxValue < value)
                {
                    return false;
                }
                //if (Math.Floor(value).ToString().Length > 14)
                //{
                //    return false;
                //}
            }

            if (this.Fee2TextBox.Text.Trim() != null)
            {
                decimal.TryParse(this.Fee2TextBox.Text.Trim().Replace(",", "").Replace("$", ""), out value);

                if (Decimal.MaxValue < value)
                {
                    return false;
                }
                //if (Math.Floor(value).ToString().Length > 14)
                //{
                //    return false;
                //}
            }

            if (this.Fee3TextBox.Text.Trim() != null)
            {
                decimal.TryParse(this.Fee3TextBox.Text.Trim().Replace(",", "").Replace("$", ""), out value);

                if (Decimal.MaxValue < value)
                {
                    return false;
                }
                //if (Math.Floor(value).ToString().Length > 14)
                //{
                //    return false;
                //}
            }

            if (this.Fee4TextBox.Text.Trim() != null)
            {
                decimal.TryParse(this.Fee4TextBox.Text.Trim().Replace(",", "").Replace("$", ""), out value);

                if (Decimal.MaxValue < value)
                {
                    return false;
                }

                //if (Math.Floor(value).ToString().Length > 14)
                //{
                //    return false;
                //}
            }

            if (this.Fee5TextBox.Text.Trim() != null)
            {
                decimal.TryParse(this.Fee5TextBox.Text.Trim().Replace(",", "").Replace("$", ""), out value);

                if (Decimal.MaxValue < value)
                {
                    return false;
                }
                //if (Math.Floor(value).ToString().Length > 14)
                //{
                //    return false;
                //}
            }

            if (this.Fee6TextBox.Text.Trim() != null)
            {
                decimal.TryParse(this.Fee6TextBox.Text.Trim().Replace(",", "").Replace("$", ""), out value);
                if (Decimal.MaxValue < value)
                {
                    return false;
                }
                //if (Math.Floor(value).ToString().Length > 14)
                //{
                //    return false;
                //}
            }

            Double value1 = 0.0;
            if (this.PaymentTotalTextBox.Text.Trim() != null)
            {
                double.TryParse(this.PaymentTotalTextBox.Text.Trim().Replace(",", ""), out value1);

                if (Math.Floor(value1).ToString().Length > 14)
                {
                    if (value1 > 922337203685478)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion

        #region CheckRequiredFields

        /// <summary>
        /// CheckRequiredFields
        /// </summary>
        /// <returns>Control</returns>
        private Control CheckRequiredFields()
        {
            Control requiredControll = null;
            if (string.IsNullOrEmpty(this.InstrumentTextBox.Text.Trim()))
            {
                requiredControll = this.InstrumentTextBox;
            }
            else if (string.IsNullOrEmpty(this.FileDateTextBox.Text.Trim()))
            {
                requiredControll = this.FileDateTextBox;
            }
            else if (string.IsNullOrEmpty(this.InstrumentDateTextBox.Text.Trim()))
            {
                requiredControll = this.InstrumentDateTextBox;
            }
            else if (string.IsNullOrEmpty(this.UserTextBox.Text.Trim()))
            {
                requiredControll = this.UserTextBox;
            }
            else if (string.IsNullOrEmpty(this.ReviwedLinkLabel.Text.Trim()))
            {
                requiredControll = this.ReviwedLinkLabel;
            }
            else if (string.IsNullOrEmpty(this.InstrumentTypeComboBox.Text.Trim()))
            {
                requiredControll = this.InstrumentTypeComboBox;
            }
            ////else if (this.PaymentEngineUserControl.ApplyAmountFiled)
            ////{
            ////    requiredControll = this.PaymentEngineUserControl;
            ////}

            return requiredControll;
        }

        #endregion CheckRequiredFields

        #region TimePicker_KeyPress

        /// <summary>
        /// TimePicker_KeyPress
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void TimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int keyCode = (int)e.KeyChar;
                if (keyCode == 9)
                {
                    SendKeys.Send(SharedFunctions.GetResourceString("ESC"));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion TimePicker_KeyPress

        #region TimerImage_Click

        /// <summary>
        /// TimerImage_Click
        /// </summary>
        /// <param name="textControl">textControl</param>
        /// <param name="timePickerControl">timePickerControl</param>
        private void TimerImage_Click(TextBox textControl, DateTimePicker timePickerControl)
        {
            this.ParentForm.AcceptButton = null;
            try
            {
                timePickerControl.BringToFront();
                if (!string.IsNullOrEmpty(textControl.Text.Trim()))
                {
                    timePickerControl.Value = Convert.ToDateTime(textControl.Text);
                }
                else
                {
                    timePickerControl.Value = DateTime.Today;
                }

                SendKeys.Send(SharedFunctions.GetResourceString("F4"));
                timePickerControl.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion TimerImage_Click

        #region SaveRecorder

        /// <summary>
        /// SaveRecorder
        /// </summary>
        /// <param name="onclose">onclose</param>
        /// <returns>bool</returns>
        private bool SaveRecorder(bool onclose)
        {
            this.SaveInstrumentHeader();
            int ppaymentId = 0;

            if (this.newInstrument)
            {
                ppaymentId = this.PaymentEngineUserControl.SaveInstrumentPaymentDetails(0, tempDataSet.GetXml(), TerraScanCommon.UserId);
            }
            else
            {
                ppaymentId = this.PaymentEngineUserControl.SaveInstrumentPaymentDetails(this.keyId, tempDataSet.GetXml(), TerraScanCommon.UserId);
            }

            if (ppaymentId == -1)
            {
                MessageBox.Show("Instrument number should be unique", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.InstrumentTextBox.Text = string.Empty;
                // this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                return false;
            }

            this.keyId = ppaymentId;
            SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
            sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
            sliceUpdateActiveRecord.SelectedKeyId = this.keyId;
            this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
            sliceReloadActiveRecord.SelectedKeyId = this.keyId;
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
            if (onclose)
            {
                return true;
            }

            return true;
        }

        private void SaveInstrumentHeader()
        {
            int customerId = 0;
            F49910InstrumentHeaderDataSet saveObject = new F49910InstrumentHeaderDataSet();
            F49910InstrumentHeaderDataSet.f49910SaveInstrumentHeaderRow saveRecordertableRow = saveObject.f49910SaveInstrumentHeader.Newf49910SaveInstrumentHeaderRow();
            saveRecordertableRow.InstNum = this.InstrumentTextBox.Text.Trim();
            saveRecordertableRow.BookPage = this.BookandPageTextBox.Text.Trim();
            saveRecordertableRow.FileDate = this.FileDateTextBox.Text.Trim();
            saveRecordertableRow.FileTime = this.fileTimeTextBox.Text.Trim();
            saveRecordertableRow.InstDate = this.InstrumentDateTextBox.Text.Trim();

            if (this.ReviwedLinkLabel.Text.Equals(SharedFunctions.GetResourceString("Yes")))
            {
                saveRecordertableRow.IsReviewed = 1;
            }
            else
            {
                saveRecordertableRow.IsReviewed = 0;
            }

            if (this.InstrumentTypeComboBox.SelectedValue != null)
            {
                saveRecordertableRow.ITypeID = Convert.ToInt32(this.InstrumentTypeComboBox.SelectedValue.ToString());
            }
            else
            {
                saveRecordertableRow.ITypeID = 0;
            }
            if (this.CustomerComboBox.SelectedValue != null && this.CustomerComboBox.SelectedValue.ToString() != "0")
            {
                saveRecordertableRow.CustomerID = Convert.ToInt32(this.CustomerComboBox.SelectedValue.ToString());
            }
            else
            {
                ////saveRecordertableRow.CustomerID = 0;
            }

            if (this.ReviewedFromComboBox1.Text != "<<Select>>")
            {
                saveRecordertableRow.RcvFrom = this.ReviewedFromComboBox1.Text;
            }
            ////if (this.ReviewedFromComboBox1.SelectedValue != null)
            ////{
            ////    saveRecordertableRow.RcvFrom = this.ReviewedFromComboBox1.SelectedValue.ToString();
            ////}
            ////else
            ////{
            ////    saveRecordertableRow.RcvFrom = "-99";
            ////}
            saveRecordertableRow.ReturnedTo = this.DeliverToTextBox.Text.Trim();
            if (this.ExemptionComboBox.SelectedValue != null && this.ExemptionComboBox.SelectedValue.ToString() != "0")
            {
                saveRecordertableRow.ETypeID = Convert.ToInt32(this.ExemptionComboBox.SelectedValue.ToString());
            }
            else
            {
                ////saveRecordertableRow.ETypeID = 0;
            }

            if (this.WOLComboBox.SelectedValue.ToString() != null)
            {
                saveRecordertableRow.WOL = Convert.ToInt16(this.WOLComboBox.SelectedValue.ToString());
            }
            else
            {
                saveRecordertableRow.WOL = 0;
            }

            saveRecordertableRow.SerialNum = this.SerialNumberTextBox.Text.Trim();
            decimal v01 = 0.00M;
            decimal v02 = 0.00M;
            decimal v03 = 0.00M;
            decimal v04 = 0.00M;
            decimal v05 = 0.00M;
            decimal v06 = 0.00M;
            decimal fee = 0.00M;
            decimal.TryParse(this.Fee1TextBox.Text.Trim().Replace("$", ""), out v01);
            decimal.TryParse(this.Fee2TextBox.Text.Trim().Replace("$", ""), out v02);
            decimal.TryParse(this.Fee3TextBox.Text.Trim().Replace("$", ""), out v03);
            decimal.TryParse(this.Fee4TextBox.Text.Trim().Replace("$", ""), out v04);
            decimal.TryParse(this.Fee5TextBox.Text.Trim().Replace("$", ""), out v05);
            decimal.TryParse(this.Fee6TextBox.Text.Trim().Replace("$", ""), out v06);
            string s = this.FeeTotalTextBox.Text.Trim().Replace(SharedFunctions.GetResourceString("SpecialChar"), string.Empty);
            decimal.TryParse(s.Replace("$", ""), out fee);
            saveRecordertableRow.V01 = v01;
            saveRecordertableRow.V02 = v02;
            saveRecordertableRow.V03 = v03;
            saveRecordertableRow.V04 = v04;
            saveRecordertableRow.V05 = v05;
            saveRecordertableRow.V06 = v06;
            saveRecordertableRow.Fee = fee;
            saveObject.f49910SaveInstrumentHeader.Rows.Add(saveRecordertableRow);
            saveObject.f49910SaveInstrumentHeader.AcceptChanges();
            tempDataSet.Tables.Clear();
            tempDataSet.Tables.Add(saveObject.f49910SaveInstrumentHeader.Copy());
            tempDataSet.Tables[0].TableName = SharedFunctions.GetResourceString("SaleAdvisoryTable");
        }

        #endregion SaveRecorder

        #region CalculateBalance

        /// <summary>
        /// Calculatings the balance.
        /// </summary>       
        private void CalculateBalance()
        {
            ////calculating total balance
            this.receiptBalance = this.FeeTotalTextBox.DecimalTextBoxValue - this.PaymentTotalTextBox.DecimalTextBoxValue;
            this.BalanceTextBox.Text = this.receiptBalance.ToString();
            this.BalanceTextBox.Text = this.receiptBalance.ToString();
            this.PaymentEngineUserControl.ApplyInstrumentBalanceAmount = this.receiptBalance.ToString(); ////this.ApplyInstrumentBalanceAmount
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

        #endregion CalculateBalance

        #region SetEditRecord

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SetEditRecord(object sender, EventArgs e)
        {
            if (!this.flagLoadOnProcess)
            {
                this.EditRecord();
            }
        }

        #endregion SetEditRecord

        #region EditRecord

        /// <summary>
        /// EditRecord
        /// </summary>
        private void EditRecord()
        {
            if (!this.flagLoadOnProcess && this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                this.CopyButton.Enabled = false;
            }

            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                this.CopyButton.Enabled = false;
            }
            else if (this.pageMode == TerraScanCommon.PageModeTypes.New)
            {

                this.CopyButton.Enabled = false;
            }
            else
            {
                if (!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit)
                {
                    this.BlockButton(false);
                }
                //else
                //{
                //    this.CopyButton.Enabled = this.formMasterNew;
                //}
                // Disable the combo in edit mode
                this.CopyButton.Enabled = false;
            }
        }

        #endregion EditRecord

        #region ClearInstrumentHeadetControls

        /// <summary>
        /// ClearInstrumentHeadetControls
        /// </summary>
        private void ClearInstrumentHeadetTextBoxControls()
        {
            this.InstrumentTextBox.Text = string.Empty;
            this.BookandPageTextBox.Text = string.Empty;
            this.fileTimeTextBox.Text = string.Empty;
            this.DeliverToTextBox.Text = string.Empty;
            this.SerialNumberTextBox.Text = string.Empty;
            //this.Fee1TextBox.Text = string.Empty;
            //this.Fee2TextBox.Text = string.Empty;
            //this.Fee3TextBox.Text = string.Empty;
            //this.Fee4TextBox.Text = string.Empty;
            //this.Fee5TextBox.Text = string.Empty;
            //this.Fee6TextBox.Text = string.Empty;
            this.FeeTotalTextBox.Text = string.Empty;
            this.PaymentTotalTextBox.Text = string.Empty;
            this.BalanceTextBox.Text = string.Empty;
        }

        #endregion ClearInstrumentHeadetControls

        #region AssignDefaultValue

        /// <summary>
        /// AssignDefaultValue
        /// </summary>
        private void AssignDefaultValue()
        {
            this.FileDateTextBox.Text = System.DateTime.Now.ToString();
            this.InstrumentDateTextBox.Text = System.DateTime.Now.ToString();
            this.ReviwedLinkLabel.Text = SharedFunctions.GetResourceString("Yes");
            this.InstrumentTypeComboBox.SelectedValue = -1;
            this.CustomerComboBox.Text = "<<Select>>";
            this.ReviewedFromComboBox1.Text = "<<Select>>";
            this.ExemptionComboBox.Text = "<<Select>>";
            this.WOLComboBox.SelectedValue = 0;
        }

        #endregion AssignDefaultValue

        #region ClearComboBox

        /// <summary>
        /// ClearComboBox
        /// </summary>
        private void ClearComboBox()
        {
            this.InstrumentTypeComboBox.SelectedIndex = -1;
            this.CustomerComboBox.Text = string.Empty;
            this.ReviewedFromComboBox1.Text = string.Empty;
            this.ExemptionComboBox.Text = string.Empty;
            this.WOLComboBox.SelectedIndex = -1;
        }

        #endregion ClearComboBox

        #region Calculation

        #endregion Calculation

        #region Setpermission

        /// <summary>
        /// Setpermission
        /// </summary>
        private void Setpermission()
        {
            this.BlockTextbox(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
            this.BlockComboBox(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);


            if (!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit)
            {
                this.PaymentEngineUserControl.Enabled = false;
                this.ReviwedLinkLabel.Enabled = false;
                this.BlockButton(false);
                this.BlockPanel(false);
                this.SetbackColor();

            }
            else
            {
                this.PaymentEngineUserControl.Enabled = true;
                this.ReviwedLinkLabel.Enabled = true;
                this.BlockButton(true);
                this.BlockComboBox(false);
                this.BlockTextbox(false);
                this.BlockPanel(true);
                this.SetbackColor();

            }
            //if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            //{
            //    this.CopyButton.Enabled = true;
            //}

            // Checks If Edit is enabled


            // check if master form ha


            // Enable the Copy Button if New Permission Exist
            //if (this.PermissionFiled.newPermission || this.formMasterNew)
            //{
            //    this.CopyButton.Enabled = true;
            //}
            //else
            //{
            //    this.CopyButton.Enabled = false;
            //}

            this.CopyButton.Enabled = this.formMasterNew;

        }

        #endregion Setpermission

        #region Set Tool Tip
        private void SetToolTip(ToolTip lblToolTip, Label feeLabel)
        {
            // check length is greater than 
            if (feeLabel.Text.Length > 8)
            {
                lblToolTip.SetToolTip(feeLabel, feeLabel.Text);
            }
            else
            {
                lblToolTip.RemoveAll();
            }
        }

        #endregion

        #endregion UserDefinedmethod

        #region Events

        #region FormLoadEvent

        /// <summary>
        /// F49910_Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F49910_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                // Added permission for the copy button
                this.CopyButton.Enabled = this.formMasterNew;
                //if (this.slicePermissionField.newPermission && this.formMasterNew)
                //{
                //    this.CopyButton.Enabled = true;
                //}
                //else
                //{
                //    this.CopyButton.Enabled = false;
                //}
                ////currentKeyIdInfo.ParameterList = false;
                this.PaymentEngineUserControl.PaymentAmountChangeEvent += new TerraScan.PaymentEngine.PaymentEngineUserControl.PaymentAmountChangeEventHandler(this.PaymentEngineUserControl_PaymentAmountChangeEvent);
                this.FlagSliceForm = true;
                this.LoadWOLComboBox();
                this.LoadInstrumentTypeComboBox();
                this.LoadInstrumentHeader();
                this.flagLoadOnProcess = false;
                PaymentEngineUserControl.Width = 686;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.InstrumentTextBox.Select();
                this.InstrumentTextBox.Focus();
                ////this.CustomizePaymentItemsGridView();
                ////PaymentEngineUserControl.LoadPayment(); 
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion FormLoadEvent

        #region CopyButton

        /// <summary>
        /// CopyButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CopyButton_Click(object sender, EventArgs e)
        {
            try
            {
                Form instrumentCopyForm = new Form();
                SliceReloadParaMeterActiveRecord currentKeyIdInfo;
                object[] optionalParameters = new object[] { };
                instrumentCopyForm = this.form49910Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(4991, optionalParameters, this.form49910Controller.WorkItem);
                if (instrumentCopyForm != null)
                {
                    if (instrumentCopyForm.ShowDialog() == DialogResult.OK)
                    {
                        this.newInstrument = true;
                        string copyXML = TerraScanCommon.GetValue(instrumentCopyForm, SharedFunctions.GetResourceString("InstCopy"));
                        DataSet copyDataSet = new DataSet();
                        copyDataSet.ReadXml(TerraScan.Utilities.SharedFunctions.XmlParser(copyXML));
                        if (copyDataSet.Tables.Count > 0)
                        {
                            if (copyDataSet.Tables[0].Rows.Count > 0)
                            {
                                int insId = 0;
                                int grantorId = 0;
                                int granteeId = 0;
                                int legalId = 0;
                                int.TryParse(copyDataSet.Tables[0].Rows[0][3].ToString(), out insId);
                                int.TryParse(copyDataSet.Tables[0].Rows[0][2].ToString(), out grantorId);
                                int.TryParse(copyDataSet.Tables[0].Rows[0][1].ToString(), out granteeId);
                                int.TryParse(copyDataSet.Tables[0].Rows[0][0].ToString(), out legalId);
                                this.OnD9030_F9030_RaiseFormMasterNew(new TerraScan.Infrastructure.Interface.EventArgs<int>(this.masterFormNo));
                                if (insId == 1)
                                {


                                    //// this.NewButton();
                                    ////  this.InstrumentDateTextBox.Text = string.Empty;
                                    ////this.ReviwedLinkLabel.Text = SharedFunctions.GetResourceString("No");
                                    //// this.UserTextBox.Text = TerraScanCommon.UserName;
                                    if (this.instrumentHeaderDataSetObject.f49901RecorderDetailsDataTable.Rows.Count > 0)
                                    {
                                        this.recordertableRow = (F49910InstrumentHeaderDataSet.f49901RecorderDetailsDataTableRow)this.instrumentHeaderDataSetObject.f49901RecorderDetailsDataTable.Rows[0];

                                        // Code added to fix issue - Avoid to set default value set for null
                                        if (this.recordertableRow.IsFileTimeNull() || this.recordertableRow.FileTime.Trim().Equals("00:00:00"))
                                        {
                                            this.fileTimeTextBox.Text = string.Empty;
                                        }
                                        else
                                        {
                                            DateTime tempDateTime;
                                            tempDateTime = Convert.ToDateTime(this.recordertableRow.FileTime.ToString());
                                            this.fileTimeTextBox.Text = tempDateTime.ToString("hh:mm tt");
                                        }
                                        if (this.customerId != 0)
                                        {
                                            this.CustomerComboBox.SelectedValue = this.customerId;
                                        }
                                        else
                                        {
                                            this.CustomerComboBox.SelectedIndex = 0;
                                        }

                                        this.ReviewedFromComboBox1.SelectedValueChanged -= new System.EventHandler(this.ReviewedFromComboBox1_SelectionEvent);

                                        if (this.recordertableRow.IsRcvFromNull())
                                        {
                                            this.ReviewedFromComboBox1.SelectedText = string.Empty;
                                        }
                                        else
                                        {
                                            this.ReviewedFromComboBox1.Text = this.recordertableRow.RcvFrom.ToString().Trim();
                                        }

                                        this.ReviewedFromComboBox1.SelectedValueChanged += new System.EventHandler(this.ReviewedFromComboBox1_SelectionEvent);

                                        this.DeliverToTextBox.Text = this.recordertableRow.ReturnedTo.ToString();

                                        // Code modified for TFSID:#12301 - F49901 DeedSifter Copy Process changes
                                        this.FileDateTextBox.Text = this.recordertableRow.FileDate.ToString();

                                        int.TryParse(this.recordertableRow.ITypeID.ToString(), out this.instrumentTypeId);
                                        int.TryParse(this.recordertableRow.ETypeID.ToString(), out this.exemptionTypeId);

                                        if (this.instrumentTypeId != 0)
                                        {
                                            this.InstrumentTypeComboBox.SelectedValue = this.instrumentTypeId;
                                            this.insTypeId = (int)this.InstrumentTypeComboBox.SelectedValue;
                                        }
                                        else
                                        {
                                            this.InstrumentTypeComboBox.SelectedIndex = 1;
                                        }

                                        if (this.exemptionTypeId != 0)
                                        {
                                            this.ExemptionComboBox.SelectedValue = this.exemptionTypeId;
                                            this.tempExemption = (int)this.ExemptionComboBox.SelectedValue;
                                        }
                                        else
                                        {
                                            this.ExemptionComboBox.SelectedIndex = 0;
                                        }

                                        this.FeeCalculation(this.recordertableRow, null);

                                        this.PaymentEngineUserControl.LoadPaymentGrid(0);
                                        this.InstrumentTextBox.Focus();

                                        ////Record Count

                                        //this.SetRecordCount(this, new DataEventArgs<int>(0));
                                        //this.SetActiveRecord(this, new DataEventArgs<int>(0));
                                        //int[] currentRecordInfo = new int[2];
                                        //currentRecordInfo[0] = 0;
                                        //currentRecordInfo[1] = 0;
                                        //this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(currentRecordInfo));
                                    }
                                }
                                else
                                {
                                    //this.NewButton();
                                    //this.ReviwedLinkLabel.Text = SharedFunctions.GetResourceString("No");
                                    //this.UserTextBox.Text = TerraScanCommon.UserName;
                                }

                                currentKeyIdInfo.MasterFormNo = this.masterFormNo;
                                currentKeyIdInfo.SelectedKeyId = this.keyId;
                                currentKeyIdInfo.ParameterList = copyXML;
                                this.FormSlice_OnCopy_SetKeyId(this, new DataEventArgs<SliceReloadParaMeterActiveRecord>(currentKeyIdInfo));

                            }
                        }

                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.EditRecord();
                        //this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        // Disbale the Copy Button
                        this.CopyButton.Enabled = false;
                        this.copyButtonClick = true;
                        this.InstrumentTextBox.Focus();
                    }
                }


            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion CopyButton

        #region ReviwedLinkLabel_LinkClicked

        /// <summary>
        /// ReviwedLinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ReviwedLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Form reviewedForm = new Form();
                object[] optionalParameters = new object[] { this.ReviwedLinkLabel.Text.Trim() };
                reviewedForm = this.form49910Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(4990, optionalParameters, this.form49910Controller.WorkItem);
                if (reviewedForm != null)
                {
                    if (reviewedForm.ShowDialog() == DialogResult.OK)
                    {
                        this.ReviwedLinkLabel.Text = TerraScanCommon.GetValue(reviewedForm, SharedFunctions.GetResourceString("InstReviewedStatus"));

                        this.EditRecord();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion ReviwedLinkLabel_LinkClicked

        #region InstrumentTypeButton_Click

        /// <summary>
        /// InstrumentTypeButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void InstrumentTypeButton_Click(object sender, EventArgs e)
        {
            try
            {

                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(49930);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.instrumentId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                //Form instrumentTypeForm = new Form();
                //object[] optionalParameters = new object[] { };
                //instrumentTypeForm = TerraScanCommon.GetForm(49930, optionalParameters, this.form49910Controller.WorkItem);
                //if (instrumentTypeForm != null)
                //{
                //    if (instrumentTypeForm.ShowDialog() == DialogResult.OK)
                //    {
                //    }
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion InstrumentTypeButton_Click

        #region CustomerButton_Click

        /// <summary>
        /// CustomerButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CustomerButton_Click(object sender, EventArgs e)
        {
            try
            {

                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(49931);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.instrumentId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                //Form cutomerForm = new Form();
                //object[] optionalParameters = new object[] { };
                //cutomerForm = TerraScanCommon.GetForm(49931, optionalParameters, this.form49910Controller.WorkItem);
                //if (cutomerForm != null)
                //{
                //    if (cutomerForm.ShowDialog() == DialogResult.OK)
                //    {
                //    }
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion CustomerButton_Click

        #region ReviewedButton_Click

        /// <summary>
        /// ReviewedButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ReviewedButton_Click(object sender, EventArgs e)
        {
            try
            {

                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(49932);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.instrumentId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                //Form grantListForm = new Form();
                //object[] optionalParameters = new object[] { };
                //grantListForm = TerraScanCommon.GetForm(49932, optionalParameters, this.form49910Controller.WorkItem);
                //if (grantListForm != null)
                //{
                //    if (grantListForm.ShowDialog() == DialogResult.OK)
                //    {
                //    }
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion ReviewedButton_Click

        #region ExemptionTypeButton_Click

        /// <summary>
        /// ExemptionTypeButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ExemptionTypeButton_Click(object sender, EventArgs e)
        {
            try
            {

                ////this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(49933);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.instrumentId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));

                ////Form exemptionListForm = new Form();
                ////object[] optionalParameters = new object[] { };
                ////exemptionListForm = TerraScanCommon.GetForm(49933, optionalParameters, this.form49910Controller.WorkItem);
                ////if (exemptionListForm != null)
                ////{
                ////    if (exemptionListForm.ShowDialog() == DialogResult.OK)
                ////    {
                ////    }
                ////}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion ExemptionTypeButton_Click

        #region InstrumentHeaderPictureBox

        /// <summary>
        /// InstrumentHeaderPictureBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void InstrumentHeaderPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion InstrumentHeaderPictureBox

        #region InstrumentHeaderPictureBox_MouseEnter

        /// <summary>
        /// InstrumentHeaderPictureBox_MouseEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void InstrumentHeaderPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.InstrumentHeaderToolTip.SetToolTip(this.InstrumentHeaderPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion InstrumentHeaderPictureBox_MouseEnter

        #region FileDateCalendar_Click

        /// <summary>
        /// FileDateCalendar_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FileDateCalendar_Click(object sender, EventArgs e)
        {
            try
            {
                this.TimerImage_Click(this.FileDateTextBox, this.FileDateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion FileDateCalendar_Click

        #region InstrumentDateCalendar_Click

        /// <summary>
        /// InstrumentDateCalendar_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void InstrumentDateCalendar_Click(object sender, EventArgs e)
        {
            try
            {
                this.TimerImage_Click(this.InstrumentDateTextBox, this.InstrumentDateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion InstrumentDateCalendar_Click

        #region InstrumentDateTimePicker_CloseUp

        /// <summary>
        /// InstrumentDateTimePicker_CloseUp
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void InstrumentDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.InstrumentDateTextBox.Text = this.InstrumentDateTimePicker.Text;
                this.InstrumentDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion InstrumentDateTimePicker_CloseUp

        #region FileDateTimePicker_CloseUp

        /// <summary>
        /// FileDateTimePicker_CloseUp
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FileDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.FileDateTextBox.Text = this.FileDateTimePicker.Text;
                this.FileDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion FileDateTimePicker_CloseUp

        #region PaymentEngineUserControl_PaymentAmountChangeEvent

        /// <summary>
        /// Payments the engine user control_ payment amount change event.
        /// </summary>
        /// <param name="amount">The amount.</param>
        private void PaymentEngineUserControl_PaymentAmountChangeEvent(decimal amount)
        {
            try
            {
                this.PaymentTotalTextBox.Text = amount.ToString();
                this.CalculateBalance();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion PaymentEngineUserControl_PaymentAmountChangeEvent

        #region ReviewedFromComboBox1_SelectionEvent

        /// <summary>
        /// ReviewedFromComboBox1_SelectionEvent
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ReviewedFromComboBox1_SelectionEvent(object sender, EventArgs e)
        {
            try
            {
                this.EditRecord();
                int grantId = 0;

                if (this.ReviewedFromComboBox1.SelectedValue != null)
                {
                    int.TryParse(this.ReviewedFromComboBox1.SelectedValue.ToString(), out grantId);
                }

                this.instrumentHeaderDataSetObject = this.form49910Controller.WorkItem.F49910_GetGranterAddressDetails(grantId);

                if (this.instrumentHeaderDataSetObject.f49910GrantListValues.Rows.Count > 0)
                {
                    F49910InstrumentHeaderDataSet.f49910GrantListValuesRow grantListRow = (F49910InstrumentHeaderDataSet.f49910GrantListValuesRow)this.instrumentHeaderDataSetObject.f49910GrantListValues.Rows[0];
                    if (MessageBox.Show(SharedFunctions.GetResourceString("GrantorAddress"), SharedFunctions.GetResourceString("Log"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        StringBuilder ownerAddress = new StringBuilder();
                        if (!string.IsNullOrEmpty(grantListRow.Name.ToString()))
                        {
                            ownerAddress.Append(grantListRow.Name.ToString());
                        }

                        if (!string.IsNullOrEmpty(grantListRow.Addr1.ToString()))
                        {
                            ownerAddress.Append(SharedFunctions.GetResourceString("NexLine"));
                            ownerAddress.Append(grantListRow.Addr1.ToString());
                        }

                        ////address2
                        if (!string.IsNullOrEmpty(grantListRow.Addr2.ToString()))
                        {
                            ownerAddress.Append(SharedFunctions.GetResourceString("NexLine"));
                            ownerAddress.Append(grantListRow.Addr2.ToString());
                        }

                        ////city
                        if (!string.IsNullOrEmpty(grantListRow.City.ToString()))
                        {
                            ownerAddress.Append(SharedFunctions.GetResourceString("NexLine"));
                            ownerAddress.Append(grantListRow.City.ToString());
                        }

                        ////state
                        if (!string.IsNullOrEmpty(grantListRow.State.ToString()))
                        {
                            if (string.IsNullOrEmpty(grantListRow.City.ToString()))
                            {
                                ownerAddress.Append(SharedFunctions.GetResourceString("NexLine"));
                                ownerAddress.Append(grantListRow.State.ToString());
                            }
                            else
                            {
                                ownerAddress.Append(", ");
                                ownerAddress.Append(grantListRow.State.ToString());
                            }
                        }

                        ////zip
                        if (!string.IsNullOrEmpty(grantListRow.Zip.ToString()))
                        {
                            if (string.IsNullOrEmpty(grantListRow.City.ToString()) && string.IsNullOrEmpty(grantListRow.State.ToString()))
                            {
                                ownerAddress.Append(SharedFunctions.GetResourceString("NexLine"));
                                ownerAddress.Append(grantListRow.Zip.ToString());
                            }
                            else
                            {
                                ownerAddress.Append(", ");
                                ownerAddress.Append(grantListRow.Zip.ToString());
                            }
                        }

                        this.DeliverToTextBox.Text = ownerAddress.ToString();
                        ////Coding Added for the co : 5880(Auto-fill payment grid in instrument header)

                        if (this.PaymentEngineUserControl.OwnerName != null && !this.PaymentEngineUserControl.OwnerName.Trim().Equals("<<Select>>"))
                        {
                            if (!string.IsNullOrEmpty(grantListRow.Name.ToString()))
                            {
                                if (!this.PaymentEngineUserControl.OwnerName.Trim().Equals(grantListRow.Name.Trim()))
                                {
                                    this.PaymentEngineUserControl.OwnerName = this.ReviewedFromComboBox1.Text.Trim();
                                    ////this.PaymentEngineUserControl.ChangedOwnerName = true;
                                }
                            }
                        }
                        ////Ends here

                    }
                    else
                    {
                        this.DeliverToTextBox.Text = string.Empty;

                        this.EditRecord();
                    }
                }
                else
                {
                    this.DeliverToTextBox.Text = string.Empty;
                }
                this.tempReviewFrom = (int)this.ReviewedFromComboBox1.SelectedValue;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion ReviewedFromComboBox1_SelectionEvent

        #region PaymentEngineUserControl_PaymentItemChangeEvent

        /// <summary>
        /// PaymentEngineUserControl_PaymentItemChangeEvent
        /// </summary>
        private void PaymentEngineUserControl_PaymentItemChangeEvent()
        {
            try
            {
                this.EditRecord();

                ////Coding Added for the co : 5880(Auto-fill payment grid in instrument header)

                if (this.ReviewedFromComboBox1.Text.Trim().Equals("<<Select>>"))
                {
                    this.PaymentEngineUserControl.OwnerName = string.Empty;
                }
                else
                {
                    this.PaymentEngineUserControl.OwnerName = this.ReviewedFromComboBox1.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion PaymentEngineUserControl_PaymentItemChangeEvent

        #region ReviewedFromComboBox1_KeyPress

        /// <summary>
        /// ReviewedFromComboBox1_KeyPress
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ReviewedFromComboBox1_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                this.EditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion ReviewedFromComboBox1_KeyPress

        #region InstrumentTypeComboBox_SelectionChangeCommitted

        /// <summary>
        /// InstrumentTypeComboBox_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void InstrumentTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.EditRecord();
                if (this.InstrumentTypeComboBox.SelectedValue != null)
                {
                    int.TryParse(this.InstrumentTypeComboBox.SelectedValue.ToString(), out this.insTypeId);
                    this.GetFeeDetails(this.insTypeId);
                    this.insTypeId = (int)this.InstrumentTypeComboBox.SelectedValue;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion InstrumentTypeComboBox_SelectionChangeCommitted

        #region ReviewedFromComboBox1_KeyDown

        /// <summary>
        /// ReviewedFromComboBox1_KeyDown
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ReviewedFromComboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //// this.EditRecord();
                if (e.KeyValue == 8 || e.KeyValue == 46)
                {
                    //// this.ReviewedFromComboBox1.Text = "<<Selct>>";
                    this.DeliverToTextBox.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion ReviewedFromComboBox1_KeyDown

        #region CustomerComboBox_SelectionChangeCommitted

        /// <summary>
        /// CustomerComboBox_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CustomerComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //// this.pageMode = TerraScanCommon.PageModeTypes.View;
            if (this.CustomerComboBox.SelectedValue != null)
            {
                this.EditRecord();
                this.tempCustomer = (int)this.CustomerComboBox.SelectedValue;
            }
            else 
            {
                this.CustomerComboBox.SelectedValue = 0;
            }

        }

        #endregion CustomerComboBox_SelectionChangeCommitted

        #region WOLComboBox_SelectionChangeCommitted

        /// <summary>
        /// WOLComboBox_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void WOLComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.EditRecord();
        }

        #endregion WOLComboBox_SelectionChangeCommitted

        private void CustomerComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    this.EditRecord();
            //    if (e.KeyValue == 8 || e.KeyValue == 46)
            //    {
            //        //// this.CustomerComboBox.Text="<<Select>>"; 
            //        ////this.DeliverToTextBox.Text = string.Empty;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            //}
        }

        private void ExemptionComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    this.EditRecord();
            //    if (e.KeyValue == 8 || e.KeyValue == 46)
            //    {
            //        //// this.ExemptionComboBox.Text = "<<Select>>"; 
            //        ////this.DeliverToTextBox.Text = string.Empty;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            //}
        }

        private void InstrumentTypeComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    this.EditRecord();

            //    if (e.KeyValue == 13)
            //    {
            //        if (this.InstrumentTypeComboBox.SelectedValue != null)
            //        {
            //            int.TryParse(this.InstrumentTypeComboBox.SelectedValue.ToString(), out this.insTypeId);
            //            this.GetFeeDetails(this.insTypeId);
            //        }
            //        else
            //        {
            //            this.InstrumentTypeComboBox.Text = string.Empty;
            //            this.Fee1TextBox.LockKeyPress = true;
            //            this.Fee2TextBox.LockKeyPress = true;
            //            this.Fee3TextBox.LockKeyPress = true;
            //            this.Fee4TextBox.LockKeyPress = true;
            //            this.Fee5TextBox.LockKeyPress = true;
            //            this.Fee6TextBox.LockKeyPress = true;
            //            this.ClearFeeTextBoxes();
            //            this.EmptyLable();
            //        }
            //    }

            //}

            //catch (Exception ex)
            //{
            //    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            //}
        }

        /// <summary>
        /// Handles the Validating event of the InstrumentTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void InstrumentTypeComboBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
            {
            try
            {
                if (!this.insTypeId.Equals(this.InstrumentTypeComboBox.SelectedValue))
                {
                    this.EditRecord();
                    this.instrumentHeaderDataSetObject = this.form49910Controller.WorkItem.F49910_GetInstrumentTypeDetails();
                    this.instrumentHeaderDataSetObject.f49910InstrumentTypeDataTable.CaseSensitive = false;

                    string instrumentType = this.InstrumentTypeComboBox.Text.Trim().Replace("'", "");

                    DataRow[] temp = this.instrumentHeaderDataSetObject.f49910InstrumentTypeDataTable.Select("InstrumentType ='" + instrumentType + "'");

                    if (temp.Length <= 0)
                    {
                        this.InstrumentTypeComboBox.Text = string.Empty;
                        this.InstrumentTypeComboBox.Text = string.Empty;
                        this.Fee1TextBox.LockKeyPress = true;
                        this.Fee2TextBox.LockKeyPress = true;
                        this.Fee3TextBox.LockKeyPress = true;
                        this.Fee4TextBox.LockKeyPress = true;
                        this.Fee5TextBox.LockKeyPress = true;
                        this.Fee6TextBox.LockKeyPress = true;
                        this.ClearFeeTextBoxes();
                        this.EmptyLable();
                    }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        private void InstrumentTypeComboBox_Leave(object sender, EventArgs e)
        {
            //this.instrumentHeaderDataSetObject = this.form49910Controller.WorkItem.F49910_GetInstrumentTypeDetails();
            //this.instrumentHeaderDataSetObject.f49910InstrumentTypeDataTable.CaseSensitive = false;

            //string instrumentType = this.InstrumentTypeComboBox.Text.Trim().Replace("'", "");

            //DataRow[] temp = this.instrumentHeaderDataSetObject.f49910InstrumentTypeDataTable.Select("InstrumentType ='" + instrumentType + "'");

            //if (temp.Length <= 0)
            //{
            //    this.InstrumentTypeComboBox.Text = string.Empty;
            //    this.InstrumentTypeComboBox.Text = string.Empty;
            //    this.Fee1TextBox.LockKeyPress = true;
            //    this.Fee2TextBox.LockKeyPress = true;
            //    this.Fee3TextBox.LockKeyPress = true;
            //    this.Fee4TextBox.LockKeyPress = true;
            //    this.Fee5TextBox.LockKeyPress = true;
            //    this.Fee6TextBox.LockKeyPress = true;
            //    this.ClearFeeTextBoxes();
            //    this.EmptyLable();
            //}
        }

        #endregion Events

        private void ReviewedFromComboBox1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (this.ReviewedFromComboBox1.SelectedValue != null && (int)this.ReviewedFromComboBox1.SelectedValue !=0)
                {
                    if (!this.tempReviewFrom.Equals(this.ReviewedFromComboBox1.SelectedValue))
                    {
                        this.EditRecord();

                        this.instrumentHeaderDataSetObject = this.form49910Controller.WorkItem.F49910_GetGranterAddressDetails(tempReviewFrom);

                        if (this.instrumentHeaderDataSetObject.f49910GrantListValues.Rows.Count > 0)
                        {
                            F49910InstrumentHeaderDataSet.f49910GrantListValuesRow grantListRow = (F49910InstrumentHeaderDataSet.f49910GrantListValuesRow)this.instrumentHeaderDataSetObject.f49910GrantListValues.Rows[0];
                            if (MessageBox.Show(SharedFunctions.GetResourceString("GrantorAddress"), SharedFunctions.GetResourceString("Log"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                StringBuilder ownerAddress = new StringBuilder();
                                if (!string.IsNullOrEmpty(grantListRow.Name.ToString()))
                                {
                                    ownerAddress.Append(grantListRow.Name.ToString());
                                }

                                if (!string.IsNullOrEmpty(grantListRow.Addr1.ToString()))
                                {
                                    ownerAddress.Append(SharedFunctions.GetResourceString("NexLine"));
                                    ownerAddress.Append(grantListRow.Addr1.ToString());
                                }

                                ////address2
                                if (!string.IsNullOrEmpty(grantListRow.Addr2.ToString()))
                                {
                                    ownerAddress.Append(SharedFunctions.GetResourceString("NexLine"));
                                    ownerAddress.Append(grantListRow.Addr2.ToString());
                                }

                                ////city
                                if (!string.IsNullOrEmpty(grantListRow.City.ToString()))
                                {
                                    ownerAddress.Append(SharedFunctions.GetResourceString("NexLine"));
                                    ownerAddress.Append(grantListRow.City.ToString());
                                }

                                ////state
                                if (!string.IsNullOrEmpty(grantListRow.State.ToString()))
                                {
                                    if (string.IsNullOrEmpty(grantListRow.City.ToString()))
                                    {
                                        ownerAddress.Append(SharedFunctions.GetResourceString("NexLine"));
                                        ownerAddress.Append(grantListRow.State.ToString());
                                    }
                                    else
                                    {
                                        ownerAddress.Append(", ");
                                        ownerAddress.Append(grantListRow.State.ToString());
                                    }
                                }

                                ////zip
                                if (!string.IsNullOrEmpty(grantListRow.Zip.ToString()))
                                {
                                    if (string.IsNullOrEmpty(grantListRow.City.ToString()) && string.IsNullOrEmpty(grantListRow.State.ToString()))
                                    {
                                        ownerAddress.Append(SharedFunctions.GetResourceString("NexLine"));
                                        ownerAddress.Append(grantListRow.Zip.ToString());
                                    }
                                    else
                                    {
                                        ownerAddress.Append(", ");
                                        ownerAddress.Append(grantListRow.Zip.ToString());
                                    }
                                }

                                this.DeliverToTextBox.Text = ownerAddress.ToString();

                                if (this.PaymentEngineUserControl.OwnerName != null && !this.PaymentEngineUserControl.OwnerName.Trim().Equals("<<Select>>"))
                                {
                                    if (!string.IsNullOrEmpty(grantListRow.Name.ToString()))
                                    {
                                        if (!this.PaymentEngineUserControl.OwnerName.Trim().Equals(grantListRow.Name.Trim()))
                                        {
                                            this.PaymentEngineUserControl.OwnerName = this.ReviewedFromComboBox1.Text.Trim();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                this.DeliverToTextBox.Text = string.Empty;
                                this.EditRecord();
                            }
                        }
                        else
                        {
                            this.DeliverToTextBox.Text = string.Empty;
                        }
                        this.tempReviewFrom = (int)this.ReviewedFromComboBox1.SelectedValue;
                    }
                    ////Added by Biju on 23/Jul/2010 to fix the issue of text being selected even after leaving focus
                    this.ReviewedFromComboBox1.SelectionLength = 0;
                    
                }
                else
                {
                    this.EditRecord();
                    ////Commented by Biju on 23/Jul/2010 to implement #7175
                    ////this.DeliverToTextBox.Text = string.Empty;
                    ////this.ReviewedFromComboBox1.Text = string.Empty;
                    ////this.ReviewedFromComboBox1.SelectedValue = 0;
                    ////till here
                    ////Added by Biju on 23/Jul/2010 to implement #7175
                    if (string.IsNullOrEmpty(this.ReviewedFromComboBox1.Text.Trim()))
                        this.ReviewedFromComboBox1.SelectedValue = 0;
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        private void CustomerComboBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (this.CustomerComboBox.SelectedValue != null)
                {
                    if (!this.tempCustomer.Equals(this.CustomerComboBox.SelectedValue))
                    {
                        this.EditRecord();

                        this.tempCustomer = (int)this.CustomerComboBox.SelectedValue;
                    }
                }
                else
                {
                    this.CustomerComboBox.SelectedValue = 0;
                 }

            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void CustomerComboBox_Leave(object sender, EventArgs e)
        {
            //this.instrumentHeaderDataSetObject = this.form49910Controller.WorkItem.F49910_GetInstrumentTypeDetails();
            //this.instrumentHeaderDataSetObject.f49910_pclst_CustomerValue.CaseSensitive = false;

            //string customerName = CustomerComboBox.Text.Trim().Replace("'", "");

            //DataRow[] temp = this.instrumentHeaderDataSetObject.f49910_pclst_CustomerValue.Select("Name ='" + customerName + "'");
            //if (temp.Length <= 0)
            //{
            //    //// CustomerComboBox.Text = string.Empty;
            //}
        }

        private void ReviewedFromComboBox1_Leave(object sender, EventArgs e)
        {
            //this.instrumentHeaderDataSetObject = this.form49910Controller.WorkItem.F49910_GetInstrumentTypeDetails();
            //this.instrumentHeaderDataSetObject.f49910_pclst_GrantList.CaseSensitive = false;

            //string reviewedName = ReviewedFromComboBox1.Text.Trim().Replace("'", "");

            //DataRow[] temp = this.instrumentHeaderDataSetObject.f49910_pclst_GrantList.Select("Name ='" + reviewedName + "'");
            //if (temp.Length <= 0)
            //{
            //    ////ReviewedFromComboBox1.Text = string.Empty;
            //}
        }

        private void ExemptionComboBox_Leave(object sender, EventArgs e)
        {
            //this.instrumentHeaderDataSetObject = this.form49910Controller.WorkItem.F49910_GetInstrumentTypeDetails();
            //this.instrumentHeaderDataSetObject.f49910_pclst_ExemptionType.CaseSensitive = false;
            //string exemption = ExemptionComboBox.Text.Trim().Replace("'", "");

            //DataRow[] temp = this.instrumentHeaderDataSetObject.f49910_pclst_ExemptionType.Select("ExemptionType ='" + exemption + "'");
            //if (temp.Length <= 0)
            //{
            //    ////ExemptionComboBox.Text = string.Empty;
            //}
        }

        private void ExemptionComboBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (this.ExemptionComboBox.SelectedValue != null)
                {
                    if (!this.tempExemption.Equals(this.ExemptionComboBox.SelectedValue))
                    {
                        this.EditRecord();

                        //if (this.ExemptionComboBox.SelectedValue == null)
                        
                        this.tempExemption = (int)this.ExemptionComboBox.SelectedValue;
                    }
                }
                else
                {
                    this.ExemptionComboBox.SelectedValue = 0;
                }

                
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ExemptionComboBox_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            this.EditRecord();
            this.tempExemption = (int)this.ExemptionComboBox.SelectedValue;
        }

        /// <summary>
        /// Gets the form master new permission.
        /// </summary>
        /// <returns>bool</returns>
        private bool GetFormMasterNewPermission()
        {
            if ((this.Parent != null) && (this.Parent.Parent != null) && (this.Parent.Parent.Parent != null))
            {
                if (this.Parent.Parent.Parent is BaseSmartPart)
                {
                    return ((BaseSmartPart)this.Parent.Parent.Parent).PermissionFiled.newPermission;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void InstrumentTypeComboBox_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                this.EditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void CustomerComboBox_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                this.EditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ExemptionComboBox_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                this.EditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        //To implement #7399 - TSCO -  Field level error message For Instrument Number
        private void InstrumentTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.instrumentChangeId)
                {
                    F49910InstrumentHeaderDataSet instrumentid = new F49910InstrumentHeaderDataSet();
                    F49910InstrumentHeaderDataSet.f49910SaveInstrumentHeaderRow saverecord = instrumentid.f49910SaveInstrumentHeader.Newf49910SaveInstrumentHeaderRow();
                    saverecord.InstNum = this.InstrumentTextBox.Text.Trim();
                    instrumentid.f49910SaveInstrumentHeader.Rows.Add(saverecord);
                    instrumentid.DataSetName = "Root";
                    instrumentid.Namespace = string.Empty;
                    instrumentid.f49910SaveInstrumentHeader.TableName = "Table";
                    instrumentid.f49910SaveInstrumentHeader.AcceptChanges();
                    int errorc = 0;
                    if (this.newInstrument)
                    {
                        errorc = this.form49910Controller.WorkItem.F49910CheckInstrumentHeaderDetails(0, instrumentid.GetXml(), null, TerraScanCommon.UserId);
                    }
                    else
                    {
                        errorc = this.form49910Controller.WorkItem.F49910CheckInstrumentHeaderDetails(this.keyId, instrumentid.GetXml(), null, TerraScanCommon.UserId);
                    }

                    if (errorc == -1)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString ("InstrumentUnique"), "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.InstrumentTextBox.Focus();
                        this.InstrumentTextBox.Text = string.Empty;

                    }
                    //this.instrumentChangeId = false;
                }
            }
            catch (Exception eq)
            {
                ExceptionManager.ManageException(eq, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);

            }


        }
       
        
            private void InstrumentTextBox_TextChanged(object sender, EventArgs e)
          {
                try
                {
                    if (!this.flagLoadOnProcess)
                    {
                        this.EditRecord();

                        this.instrumentChangeId = true; 
                    }
                 
                }
                catch (Exception ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }

            }

            private void InstrumentTypeComboBox_SelectedValueChanged(object sender, EventArgs e)
            {
                try
                {
                    if (!this.flagLoadOnProcess)
                    {
                        this.EditRecord();
                        if (this.InstrumentTypeComboBox.SelectedValue != null)
                        {
                            int.TryParse(this.InstrumentTypeComboBox.SelectedValue.ToString(), out this.insTypeId);
                            this.GetFeeDetails(this.insTypeId);
                            this.insTypeId = (int)this.InstrumentTypeComboBox.SelectedValue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
            }

            private void F49910_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
            {
                try
                {
                    //// this.EditRecord();
                    if (e.KeyValue == 27)
                    {
                        this.ReviewedFromComboBox1.Text = "<<Selct>>";
                        this.DeliverToTextBox.Text = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
                //MessageBox.Show("preview Key Down");
            }

         



        }
    }
